Imports System.Globalization
Imports System.Text
Imports System.Text.Json
Imports System.Threading

''' <summary>读取实际包大小。图表保留区间总数据量，只有列表统计除以时间。</summary>
Public NotInheritable Class 比特率可视化数据核心
    Private Sub New()
    End Sub

    Public Class 流信息
        Public Property 索引 As Integer
        Public Property 类型 As String = ""
        Public Property 编码器 As String = ""
        Public Property 起始秒 As Double = Double.NaN
        Public Property 时长秒 As Double = Double.NaN
        Public Property 总帧数 As Long
        Public Property 类型内序号 As Integer
        Public ReadOnly Property 选择符 As String
            Get
                Return $"0:{If(类型 = "video", "v", "a")}:{类型内序号}"
            End Get
        End Property
    End Class

    Public Class 文件信息
        Public Property 流 As New List(Of 流信息)
        Public Property 起始秒 As Double = Double.NaN
        Public Property 时长秒 As Double = Double.NaN
    End Class

    Public Class 包信息
        Public Property 流索引 As Integer
        Public Property 时间秒 As Double
        Public Property 时长秒 As Double = Double.NaN
        Public Property 字节数 As Long
        Public Property 关键帧 As Boolean
    End Class

    Public Class 采样点
        Public Property 时间秒 As Double
        Public Property 结束秒 As Double
        Public Property 千比特 As Double
        Public Property 帧数 As Long
        Public Property 音频包数 As Long
    End Class

    Public Class 分析结果
        Public Property 点 As New List(Of 采样点)
        Public Property 包含视频 As Boolean
        Public Property 包含音频 As Boolean
        Public Property 起始秒 As Double
        Public Property 时长秒 As Double
        Public Property 帧数 As Long
        Public Property 平均Kbps As Double
        Public Property 最低Kbps As Double
        Public Property 最高Kbps As Double
    End Class

    Public Shared Async Function 读取文件信息Async(path As String, token As CancellationToken) As Task(Of 文件信息)
        Dim output = Await 执行简短探测Async({"-v", "error", "-show_entries", "stream=index,codec_type,codec_name,start_time,start_pts,time_base,duration,nb_frames:format=start_time,duration", "-of", "json", path}, token).ConfigureAwait(False)
        Dim result As New 文件信息()
        Using document = JsonDocument.Parse(output)
            Dim root = document.RootElement
            Dim format As JsonElement
            If root.TryGetProperty("format", format) Then
                result.起始秒 = 取浮点(format, "start_time")
                result.时长秒 = 取浮点(format, "duration")
            End If
            Dim streams As JsonElement
            If root.TryGetProperty("streams", streams) Then
                Dim videoOrdinal = 0
                Dim audioOrdinal = 0
                For Each entry In streams.EnumerateArray()
                    Dim kind = 取文本(entry, "codec_type")
                    If kind <> "video" AndAlso kind <> "audio" Then Continue For
                    Dim index As Integer
                    If Not Integer.TryParse(取文本(entry, "index"), NumberStyles.Integer, CultureInfo.InvariantCulture, index) Then Continue For
                    Dim stream As New 流信息 With {
                        .索引 = index, .类型 = kind, .编码器 = 取文本(entry, "codec_name"),
                        .起始秒 = 取浮点(entry, "start_time"), .时长秒 = 取浮点(entry, "duration")
                    }
                    If Not Double.IsFinite(stream.起始秒) Then
                        stream.起始秒 = 取浮点(entry, "start_pts") * 解析时间基(取文本(entry, "time_base"))
                    End If
                    Dim frameCount As Long
                    If Long.TryParse(取文本(entry, "nb_frames"), NumberStyles.Integer, CultureInfo.InvariantCulture, frameCount) Then stream.总帧数 = frameCount
                    If kind = "video" Then
                        stream.类型内序号 = videoOrdinal
                        videoOrdinal += 1
                    Else
                        stream.类型内序号 = audioOrdinal
                        audioOrdinal += 1
                    End If
                    result.流.Add(stream)
                Next
            End If
        End Using
        Return result
    End Function

    Public Shared Async Function 读取包Async(path As String, selected As HashSet(Of Integer), token As CancellationToken,
                                           Optional progress As IProgress(Of Long) = Nothing,
                                           Optional countIndices As HashSet(Of Integer) = Nothing) As Task(Of List(Of 包信息))
        Dim result As New List(Of 包信息)()
        Dim frameCount As Long
        Await 执行逐行探测Async({"-v", "error", "-show_packets", "-show_entries",
            "packet=stream_index,pts_time,dts_time,duration_time,size,flags", "-of", "compact=p=0:nk=0", path},
            Sub(line)
                Dim packet = 解析包(line)
                If packet Is Nothing OrElse Not selected.Contains(packet.流索引) Then Return
                result.Add(packet)
                ' 与最终总帧数口径一致：统计有效视频包；纯音频模式统计有效音频包。
                If countIndices Is Nothing OrElse countIndices.Contains(packet.流索引) Then frameCount += 1
            End Sub, token, progress, Function() frameCount).ConfigureAwait(False)
        Return result
    End Function

    Private Shared Function 解析紧凑字段(line As String) As Dictionary(Of String, String)
        Dim fields As New Dictionary(Of String, String)(StringComparer.Ordinal)
        For Each part In line.Split("|"c)
            Dim separator = part.IndexOf("="c)
            If separator > 0 Then fields(part.Substring(0, separator)) = part.Substring(separator + 1)
        Next
        Return fields
    End Function

    Private Shared Function 解析包(line As String) As 包信息
        Dim fields = 解析紧凑字段(line)
        Dim index As Integer
        Dim bytes As Long
        Dim timestamp As Double
        Dim raw As String = Nothing
        If Not fields.TryGetValue("stream_index", raw) OrElse Not Integer.TryParse(raw, NumberStyles.Integer, CultureInfo.InvariantCulture, index) Then Return Nothing
        If Not fields.TryGetValue("size", raw) OrElse Not Long.TryParse(raw, NumberStyles.Integer, CultureInfo.InvariantCulture, bytes) OrElse bytes < 0 Then Return Nothing
        If Not fields.TryGetValue("pts_time", raw) OrElse Not Double.TryParse(raw, NumberStyles.Float, CultureInfo.InvariantCulture, timestamp) Then
            If Not fields.TryGetValue("dts_time", raw) OrElse Not Double.TryParse(raw, NumberStyles.Float, CultureInfo.InvariantCulture, timestamp) Then Return Nothing
        End If
        If Not Double.IsFinite(timestamp) Then Return Nothing
        Dim packet As New 包信息 With {.流索引 = index, .字节数 = bytes, .时间秒 = timestamp}
        If fields.TryGetValue("duration_time", raw) Then
            Dim duration As Double
            If Double.TryParse(raw, NumberStyles.Float, CultureInfo.InvariantCulture, duration) AndAlso Double.IsFinite(duration) AndAlso duration > 0 Then packet.时长秒 = duration
        End If
        If fields.TryGetValue("flags", raw) Then packet.关键帧 = raw.Contains("K"c)
        Return packet
    End Function

    Private Shared Sub 累加包(point As 采样点, packet As 包信息, audioIndices As HashSet(Of Integer))
        point.千比特 += packet.字节数 * 8.0 / 1000
        If audioIndices.Contains(packet.流索引) Then
            point.音频包数 += 1
        Else
            point.帧数 += 1
        End If
    End Sub

    Public Shared Function 获取有效流(file As 文件信息, selected As HashSet(Of Integer), precision As String, viewKind As String) As List(Of 流信息)
        Return file.流.Where(Function(s) selected.Contains(s.索引) AndAlso
            If(s.类型 = "video", viewKind <> "仅音频", precision = "每秒" AndAlso viewKind <> "仅视频")).ToList()
    End Function

    Public Shared Function 构造序列(file As 文件信息, packets As IEnumerable(Of 包信息), selected As HashSet(Of Integer), precision As String, viewKind As String, Optional token As CancellationToken = Nothing) As 分析结果
        token.ThrowIfCancellationRequested()
        Dim streams = 获取有效流(file, selected, precision, viewKind)
        Dim indices As New HashSet(Of Integer)(streams.Select(Function(s) s.索引))
        Dim ordered = packets.Where(Function(p) indices.Contains(p.流索引)).OrderBy(Function(p) p.时间秒).ToList()
        token.ThrowIfCancellationRequested()
        If ordered.Count = 0 Then Return Nothing
        ' 使用所选流的实际开始时间，保留音视频之间的偏移；不得直接将非零 PTS 当作播放时长。
        Dim origin = streams.Where(Function(s) Double.IsFinite(s.起始秒)).Select(Function(s) s.起始秒).DefaultIfEmpty(Double.NaN).Min()
        If Not Double.IsFinite(origin) Then origin = If(Double.IsFinite(file.起始秒), file.起始秒, ordered(0).时间秒)
        Dim endTime = ordered.Max(Function(p) p.时间秒 + If(Double.IsFinite(p.时长秒), p.时长秒, 0))
        Dim knownEnd = streams.Where(Function(s) Double.IsFinite(s.起始秒) AndAlso Double.IsFinite(s.时长秒) AndAlso s.时长秒 > 0).
            Select(Function(s) s.起始秒 + s.时长秒).DefaultIfEmpty(endTime).Max()
        endTime = Math.Max(endTime, knownEnd)
        If endTime <= origin AndAlso Double.IsFinite(file.时长秒) Then endTime = origin + file.时长秒
        Dim videoIndices As New HashSet(Of Integer)(streams.Where(Function(s) s.类型 = "video").Select(Function(s) s.索引))
        Dim audioIndices As New HashSet(Of Integer)(streams.Where(Function(s) s.类型 = "audio").Select(Function(s) s.索引))
        Dim result As New 分析结果 With {.起始秒 = origin, .时长秒 = Math.Max(0, endTime - origin),
            .包含视频 = videoIndices.Count > 0, .包含音频 = audioIndices.Count > 0,
            .帧数 = If(videoIndices.Count > 0, ordered.LongCount(Function(p) videoIndices.Contains(p.流索引)), ordered.Count)}
        Dim secondBuckets As New SortedDictionary(Of Long, 采样点)
        For Each packet In ordered
            token.ThrowIfCancellationRequested()
            Dim bucket = CLng(Math.Floor(packet.时间秒 - origin + 0.0000001))
            Dim point As 采样点 = Nothing
            If Not secondBuckets.TryGetValue(bucket, point) Then
                point = New 采样点 With {.时间秒 = origin + bucket, .结束秒 = Math.Min(origin + bucket + 1, endTime)}
                secondBuckets.Add(bucket, point)
            End If
            累加包(point, packet, audioIndices)
        Next
        If precision = "每秒" Then
            ' 空秒也属于真实时间线，显示为零而不是跨越空缺连线。
            Dim firstSecond = Math.Min(0L, secondBuckets.Keys.First())
            Dim lastSecond = Math.Max(secondBuckets.Keys.Last(), CLng(Math.Ceiling(result.时长秒)) - 1)
            If lastSecond - firstSecond > 10000000 Then Throw New InvalidOperationException("时间戳跨度异常，无法生成每秒图表")
            For secondIndex As Long = firstSecond To lastSecond
                token.ThrowIfCancellationRequested()
                Dim point As 采样点 = Nothing
                If Not secondBuckets.TryGetValue(secondIndex, point) Then point = New 采样点 With {.时间秒 = origin + secondIndex, .结束秒 = origin + secondIndex + 1}
                result.点.Add(point)
            Next
        ElseIf precision = "每关键帧" Then
            ' 各流独立划分 GOP，关键帧属于它开启的 GOP；最后一个 GOP 同样保留。
            Dim gops As New List(Of 采样点)
            For Each streamPackets In ordered.GroupBy(Function(p) p.流索引)
                Dim current As 采样点 = Nothing
                For Each packet In streamPackets
                    token.ThrowIfCancellationRequested()
                    If current Is Nothing OrElse packet.关键帧 Then
                        If current IsNot Nothing Then current.结束秒 = packet.时间秒
                        current = New 采样点 With {.时间秒 = packet.时间秒, .结束秒 = endTime}
                        gops.Add(current)
                    End If
                    累加包(current, packet, audioIndices)
                Next
            Next
            result.点 = gops.GroupBy(Function(p) p.时间秒).OrderBy(Function(g) g.Key).
                Select(Function(g) New 采样点 With {.时间秒 = g.Key, .结束秒 = g.Max(Function(p) p.结束秒), .千比特 = g.Sum(Function(p) p.千比特), .帧数 = g.Sum(Function(p) p.帧数)}).ToList()
        Else
            result.点 = ordered.GroupBy(Function(p) p.时间秒).
                Select(Function(g)
                           Dim point As New 采样点 With {.时间秒 = g.Key, .结束秒 = g.Max(Function(p) p.时间秒 + If(Double.IsFinite(p.时长秒), p.时长秒, 0))}
                           For Each packet In g
                               累加包(point, packet, audioIndices)
                           Next
                           Return point
                       End Function).ToList()
        End If
        result.平均Kbps = If(result.时长秒 > 0, ordered.Sum(Function(p) p.字节数 * 8.0 / 1000) / result.时长秒, Double.NaN)
        Dim rates = secondBuckets.Values.Where(Function(p) p.结束秒 > p.时间秒).Select(Function(p) p.千比特 / (p.结束秒 - p.时间秒)).ToArray()
        result.最低Kbps = If(rates.Length > 0, rates.Min(), Double.NaN)
        result.最高Kbps = If(rates.Length > 0, rates.Max(), Double.NaN)
        Dim coveredPlaybackSeconds = secondBuckets.Keys.LongCount(Function(i) i >= 0)
        If coveredPlaybackSeconds < Math.Ceiling(result.时长秒) Then result.最低Kbps = 0
        Return result
    End Function

    Private Shared Async Function 执行逐行探测Async(arguments As IEnumerable(Of String), onLine As Action(Of String), token As CancellationToken,
                                                progress As IProgress(Of Long), getCount As Func(Of Long)) As Task
        Dim lastReport = Environment.TickCount64
        Using process As New Process With {.StartInfo = 创建进程(arguments)}
            token.ThrowIfCancellationRequested()
            process.Start()
            Using registration = token.Register(Sub() 终止进程(process))
                Dim stderrTask = process.StandardError.ReadToEndAsync(token)
                Try
                    While True
                        Dim line = Await process.StandardOutput.ReadLineAsync(token).ConfigureAwait(False)
                        If line Is Nothing Then Exit While
                        onLine(line)
                        If progress IsNot Nothing AndAlso Environment.TickCount64 - lastReport >= 250 Then
                            lastReport = Environment.TickCount64
                            progress.Report(getCount())
                        End If
                    End While
                    Await process.WaitForExitAsync(token).ConfigureAwait(False)
                    Dim errorText = Await stderrTask.ConfigureAwait(False)
                    If process.ExitCode <> 0 Then Throw New InvalidOperationException(If(String.IsNullOrWhiteSpace(errorText), "ffprobe 读取数据失败", errorText.Trim()))
                    progress?.Report(getCount())
                Catch
                    终止进程(process)
                    Throw
                End Try
            End Using
        End Using
    End Function

    Private Shared Async Function 执行简短探测Async(arguments As IEnumerable(Of String), token As CancellationToken) As Task(Of String)
        Using process As New Process With {.StartInfo = 创建进程(arguments)}
            token.ThrowIfCancellationRequested()
            process.Start()
            Using registration = token.Register(Sub() 终止进程(process))
                Dim stdoutTask = process.StandardOutput.ReadToEndAsync(token)
                Dim stderrTask = process.StandardError.ReadToEndAsync(token)
                Try
                    Await process.WaitForExitAsync(token).ConfigureAwait(False)
                    Dim output = Await stdoutTask.ConfigureAwait(False)
                    Dim errorText = Await stderrTask.ConfigureAwait(False)
                    If process.ExitCode <> 0 Then Throw New InvalidOperationException(If(String.IsNullOrWhiteSpace(errorText), "ffprobe 读取流失败", errorText.Trim()))
                    Return output
                Catch
                    终止进程(process)
                    Throw
                End Try
            End Using
        End Using
    End Function

    Private Shared Function 创建进程(arguments As IEnumerable(Of String)) As ProcessStartInfo
        Dim result As New ProcessStartInfo With {
            .FileName = 设置_v6.获取FFprobe进程文件名(), .WorkingDirectory = 设置_v6.获取有效工作目录(),
            .UseShellExecute = False, .RedirectStandardOutput = True, .RedirectStandardError = True,
            .StandardOutputEncoding = Encoding.UTF8, .StandardErrorEncoding = Encoding.UTF8, .CreateNoWindow = True
        }
        For Each argument In arguments
            result.ArgumentList.Add(argument)
        Next
        Return result
    End Function

    Private Shared Sub 终止进程(process As Process)
        Try
            If Not process.HasExited Then process.Kill()
        Catch
        End Try
    End Sub

    Private Shared Function 取文本(value As JsonElement, name As String) As String
        Dim propertyValue As JsonElement
        If Not value.TryGetProperty(name, propertyValue) Then Return ""
        Return propertyValue.ToString()
    End Function

    Private Shared Function 取浮点(value As JsonElement, name As String) As Double
        Dim result As Double
        If Double.TryParse(取文本(value, name), NumberStyles.Float, CultureInfo.InvariantCulture, result) AndAlso Double.IsFinite(result) Then Return result
        Return Double.NaN
    End Function

    Private Shared Function 解析时间基(value As String) As Double
        Dim parts = value.Split("/"c)
        Dim numerator As Double
        Dim denominator As Double
        If parts.Length = 2 AndAlso Double.TryParse(parts(0), NumberStyles.Float, CultureInfo.InvariantCulture, numerator) AndAlso
            Double.TryParse(parts(1), NumberStyles.Float, CultureInfo.InvariantCulture, denominator) AndAlso denominator <> 0 Then Return numerator / denominator
        Return Double.NaN
    End Function
End Class
