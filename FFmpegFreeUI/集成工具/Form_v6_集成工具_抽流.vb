Imports System.Globalization
Imports System.IO
Imports System.Text
Imports System.Text.Json
Imports System.Threading
Imports LakeUI

Public Class Form_v6_集成工具_抽流

    Private Const 提取按钮默认文本 As String = "提取所选"

    Private ReadOnly 流列表 As New List(Of 抽流流信息)
    Private ReadOnly 进程锁 As New Object()
    Private 当前文件 As String = ""
    Private 当前总时长 As TimeSpan = TimeSpan.Zero
    Private 当前进程 As Process = Nothing
    Private 取消令牌源 As CancellationTokenSource = Nothing
    Private 正在运行 As Boolean = False

    Private Enum 抽流流类型
        视频
        音频
        字幕
        附件
    End Enum

    Private Class 抽流流信息
        Public Property 全局索引 As Integer
        Public Property 类型 As 抽流流类型
        Public Property 类型序号 As Integer
        Public Property 编码器 As String = ""
        Public Property 编码器完整名称 As String = ""
        Public Property 流语言 As String = ""
        Public Property 元数据标题 As String = ""
        Public Property 附件文件名 As String = ""
        Public Property 附件MimeType As String = ""
        Public Property 主文本 As String = ""
        Public Property 副文本 As String = ""
        Public Property 默认扩展名 As String = "bin"
        Public Property 复选框 As ModernCheckBox
    End Class

    Private Class 进程运行结果
        Public Property ExitCode As Integer
        Public Property Output As String = ""
    End Class

    Private Shared ReadOnly 流扩展名映射 As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase) From {
        {"h264", "h264"}, {"avc", "h264"},
        {"hevc", "h265"}, {"h265", "h265"},
        {"mpeg1video", "m1v"}, {"mpeg2video", "m2v"}, {"mpeg4", "m4v"},
        {"vc1", "vc1"}, {"vp8", "ivf"}, {"vp9", "ivf"}, {"av1", "ivf"},
        {"theora", "ogv"}, {"prores", "mov"}, {"mjpeg", "mjpg"}, {"rawvideo", "raw"},
        {"aac", "aac"}, {"ac3", "ac3"}, {"eac3", "eac3"}, {"truehd", "thd"}, {"mlp", "mlp"},
        {"dts", "dts"}, {"dca", "dts"}, {"mp1", "mp1"}, {"mp2", "mp2"}, {"mp3", "mp3"},
        {"flac", "flac"}, {"opus", "opus"}, {"vorbis", "ogg"}, {"alac", "m4a"},
        {"ape", "ape"}, {"tta", "tta"}, {"wavpack", "wv"}, {"amr_nb", "amr"}, {"amr_wb", "amr"},
        {"pcm_s8", "wav"}, {"pcm_s16le", "wav"}, {"pcm_s16be", "wav"}, {"pcm_s24le", "wav"}, {"pcm_s24be", "wav"},
        {"pcm_s32le", "wav"}, {"pcm_s32be", "wav"}, {"pcm_f32le", "wav"}, {"pcm_f64le", "wav"},
        {"subrip", "srt"}, {"ass", "ass"}, {"ssa", "ssa"}, {"webvtt", "vtt"}, {"mov_text", "srt"},
        {"hdmv_pgs_subtitle", "sup"}, {"dvd_subtitle", "sub"}, {"dvb_subtitle", "dvb"}
    }

    Private Shared ReadOnly 附件Mime扩展名映射 As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase) From {
        {"font/ttf", "ttf"}, {"font/otf", "otf"}, {"font/woff", "woff"}, {"font/woff2", "woff2"},
        {"application/x-truetype-font", "ttf"}, {"application/x-font-ttf", "ttf"}, {"application/vnd.ms-opentype", "otf"},
        {"image/jpeg", "jpg"}, {"image/jpg", "jpg"}, {"image/png", "png"}, {"image/webp", "webp"}, {"image/gif", "gif"},
        {"text/plain", "txt"}, {"text/xml", "xml"}, {"application/json", "json"}
    }

    Private Sub Form_v6_集成工具_抽流_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ModernPanel2.ScrollBarMode = ModernPanel.ScrollMode.Vertical
        MCB_输出位置.SelectedIndex = 0
        绑定路径下拉框拖拽(MCB_输出位置, 路径下拉框拖拽模式.文件夹路径)
        绑定文件拖入(Me)
        绑定文件拖入(ModernPanel1)
        绑定文件拖入(Panel1)
        绑定文件拖入(ModernPanel2)
        绑定文件拖入(MB_打开文件)
        绑定文件拖入(MB_提取所选)
        显示提示("打开或拖入媒体文件后选择要提取的流", Color.Silver)
    End Sub

    Private Sub 绑定文件拖入(target As Control)
        If target Is Nothing Then Exit Sub
        target.AllowDrop = True
        RemoveHandler target.DragEnter, AddressOf 文件拖入事件
        RemoveHandler target.DragDrop, AddressOf 文件放下事件
        AddHandler target.DragEnter, AddressOf 文件拖入事件
        AddHandler target.DragDrop, AddressOf 文件放下事件
    End Sub

    Private Sub 文件拖入事件(sender As Object, e As DragEventArgs)
        e.Effect = If(获取拖入文件(e).Count > 0, DragDropEffects.Copy, DragDropEffects.None)
    End Sub

    Private Async Sub 文件放下事件(sender As Object, e As DragEventArgs)
        Dim files = 获取拖入文件(e)
        If files.Count = 0 Then Exit Sub
        Await 打开媒体文件Async(files(0))
    End Sub

    Private Function 获取拖入文件(e As DragEventArgs) As List(Of String)
        If e Is Nothing OrElse e.Data Is Nothing OrElse Not e.Data.GetDataPresent(DataFormats.FileDrop) Then Return New List(Of String)
        Dim paths = TryCast(e.Data.GetData(DataFormats.FileDrop), String())
        If paths Is Nothing Then Return New List(Of String)
        Dim result As New List(Of String)
        For Each raw In paths
            If String.IsNullOrWhiteSpace(raw) Then Continue For
            Try
                If File.Exists(raw) Then
                    result.Add(raw)
                ElseIf Directory.Exists(raw) Then
                    result.AddRange(Directory.GetFiles(raw, "*", SearchOption.TopDirectoryOnly).OrderBy(Function(x) x, StringComparer.CurrentCultureIgnoreCase))
                End If
            Catch ex As Exception
                ExFloatingTip(ModernPanel2, $"读取路径失败：{ex.Message}", 2200)
            End Try
        Next
        Return result
    End Function

    Private Async Sub MB_打开文件_Click(sender As Object, e As EventArgs) Handles MB_打开文件.Click
        If 正在运行 Then Exit Sub
        Using d As New OpenFileDialog With {.Multiselect = False, .Filter = "所有文件|*.*"}
            If d.ShowDialog(Me) = DialogResult.OK Then Await 打开媒体文件Async(d.FileName)
        End Using
    End Sub

    Private Async Function 打开媒体文件Async(file As String) As Task
        If 正在运行 Then Exit Function
        If String.IsNullOrWhiteSpace(file) OrElse Not IO.File.Exists(file) Then
            ExFloatingTip(MB_打开文件, "文件不存在", 1800)
            Exit Function
        End If

        当前文件 = file
        当前总时长 = TimeSpan.Zero
        流列表.Clear()
        显示提示("正在读取媒体流 ...", Color.CornflowerBlue)
        MB_提取所选.Text = "读取中"

        Try
            Dim result = Await 运行进程读取输出Async(获取FFprobe文件名(), 构建FFprobe参数(file), CancellationToken.None)
            If result.ExitCode <> 0 Then Throw New InvalidOperationException(If(result.Output.Trim() = "", "ffprobe 读取失败", result.Output.Trim()))
            解析FFprobeJson(result.Output)
            呈现流列表()
        Catch ex As Exception
            当前文件 = ""
            当前总时长 = TimeSpan.Zero
            流列表.Clear()
            显示提示("读取失败：" & ex.Message, Color.IndianRed)
        Finally
            MB_提取所选.Text = 提取按钮默认文本
        End Try
    End Function

    Public Function Agent获取状态() As Dictionary(Of String, Object)
        Dim streams = 流列表.Select(Function(x) New Dictionary(Of String, Object) From {
            {"global_index", x.全局索引},
            {"type", x.类型.ToString()},
            {"type_index", x.类型序号},
            {"codec", x.编码器},
            {"language", x.流语言},
            {"title", x.元数据标题},
            {"extension", x.默认扩展名},
            {"selected", x.复选框 IsNot Nothing AndAlso x.复选框.Checked},
            {"text", x.主文本}
        }).Cast(Of Object).ToList()
        Return New Dictionary(Of String, Object) From {
            {"file", 当前文件},
            {"output_location", MCB_输出位置.Text},
            {"running", 正在运行},
            {"streams", streams}
        }
    End Function

    Public Async Function Agent配置Async(file As String, outputLocation As String, selectedStreams As IEnumerable(Of String)) As Task(Of String)
        If Not String.IsNullOrWhiteSpace(file) Then Await 打开媒体文件Async(file)
        If outputLocation IsNot Nothing Then MCB_输出位置.Text = outputLocation

        Dim requested = If(selectedStreams, Enumerable.Empty(Of String)()).
            Select(Function(x) If(x, "").Trim()).
            Where(Function(x) x <> "").
            ToList()
        If requested.Count > 0 Then
            For Each info In 流列表
                If info.复选框 Is Nothing Then Continue For
                info.复选框.Checked = requested.Any(Function(x) Agent匹配流选择(x, info))
            Next
        End If

        Return $"抽流工具：{Path.GetFileName(当前文件)}，已选 {流列表.Where(Function(x) x.复选框 IsNot Nothing AndAlso x.复选框.Checked).Count()} / {流列表.Count} 个流"
    End Function

    Public Async Function Agent运行Async(Optional forceAutoName As Boolean = True) As Task(Of String)
        Await 提取所选Async(forceAutoName)
        Return "已请求执行抽流工具"
    End Function

    Private Function Agent匹配流选择(value As String, info As 抽流流信息) As Boolean
        If info Is Nothing Then Return False
        Dim s = value.Trim().ToLowerInvariant()
        If s = info.全局索引.ToString(Globalization.CultureInfo.InvariantCulture) Then Return True
        If s = 获取类型短名(info.类型) & ":" & info.类型序号.ToString(Globalization.CultureInfo.InvariantCulture) Then Return True
        If s = info.类型.ToString().ToLowerInvariant() & ":" & info.类型序号.ToString(Globalization.CultureInfo.InvariantCulture) Then Return True
        Return False
    End Function

    Private Shared Function 构建FFprobe参数(file As String) As String
        Return "-v error -show_entries format=duration:stream=index,codec_type,codec_name,codec_long_name,width,height,r_frame_rate,avg_frame_rate,bit_rate,sample_rate,channels,channel_layout:stream_tags=language,title,filename,mimetype -of json " & 引用参数(file)
    End Function

    Private Sub 解析FFprobeJson(json As String)
        流列表.Clear()

        Using doc = JsonDocument.Parse(json)
            Dim root = doc.RootElement

            Dim formatElement As JsonElement
            If root.TryGetProperty("format", formatElement) Then
                Dim durationText = 获取Json字符串(formatElement, "duration")
                Dim durationSeconds As Double
                If Double.TryParse(durationText, NumberStyles.Any, CultureInfo.InvariantCulture, durationSeconds) AndAlso durationSeconds > 0 Then
                    当前总时长 = TimeSpan.FromSeconds(durationSeconds)
                End If
            End If

            Dim streams As JsonElement
            If Not root.TryGetProperty("streams", streams) OrElse streams.ValueKind <> JsonValueKind.Array Then Exit Sub

            Dim 类型计数 As New Dictionary(Of 抽流流类型, Integer)
            For Each stream In streams.EnumerateArray()
                Dim codecType = 获取Json字符串(stream, "codec_type").ToLowerInvariant()
                Dim 类型 As 抽流流类型
                Select Case codecType
                    Case "video"
                        类型 = 抽流流类型.视频
                    Case "audio"
                        类型 = 抽流流类型.音频
                    Case "subtitle"
                        类型 = 抽流流类型.字幕
                    Case "attachment"
                        类型 = 抽流流类型.附件
                    Case Else
                        Continue For
                End Select

                Dim value As Integer = Nothing
                If Not 类型计数.TryGetValue(类型, value) Then value = 0
                类型计数(类型) = value
                Dim info As New 抽流流信息 With {
                    .类型 = 类型,
                    .类型序号 = value,
                    .全局索引 = 获取Json整数(stream, "index", value),
                    .编码器 = 规范化编码器(获取Json字符串(stream, "codec_name")),
                    .编码器完整名称 = 获取Json字符串(stream, "codec_long_name"),
                    .流语言 = 获取Json标签(stream, "language"),
                    .元数据标题 = 获取Json标签(stream, "title"),
                    .附件文件名 = 获取Json标签(stream, "filename"),
                    .附件MimeType = 获取Json标签(stream, "mimetype")
                }
                info.默认扩展名 = 获取默认扩展名(info)
                info.主文本 = 构建主文本(info, stream)
                info.副文本 = 构建副文本(info)
                流列表.Add(info)
                类型计数(类型) += 1
            Next
        End Using
    End Sub

    Private Shared Function 构建主文本(info As 抽流流信息, stream As JsonElement) As String
        Dim spec = $"0:{获取类型短名(info.类型)}:{info.类型序号}"
        Dim codec = If(info.编码器 <> "", info.编码器, "unknown")
        Dim text = $"{spec}  {If(info.流语言 <> "", "[" & info.流语言 & "] ", "")}{codec}"

        Select Case info.类型
            Case 抽流流类型.视频
                Dim resolution = 构建分辨率(stream)
                Dim frameRate = 格式化帧率(If(获取Json字符串(stream, "avg_frame_rate") <> "", 获取Json字符串(stream, "avg_frame_rate"), 获取Json字符串(stream, "r_frame_rate")))
                Dim bitrate = 格式化比特率(获取Json字符串(stream, "bit_rate"))
                If resolution <> "" Then text &= " | " & resolution
                If frameRate <> "" Then text &= " | " & frameRate
                If bitrate <> "" Then text &= " | " & bitrate
            Case 抽流流类型.音频
                Dim channelLayout = 获取Json字符串(stream, "channel_layout")
                Dim sampleRate = 获取Json字符串(stream, "sample_rate")
                Dim bitrate = 格式化比特率(获取Json字符串(stream, "bit_rate"))
                If channelLayout <> "" Then text &= " | " & channelLayout
                If sampleRate <> "" Then text &= " | " & sampleRate & "Hz"
                If bitrate <> "" Then text &= " | " & bitrate
            Case 抽流流类型.附件
                If info.附件文件名 <> "" Then text &= " | " & info.附件文件名
                If info.附件MimeType <> "" Then text &= " | " & info.附件MimeType
        End Select

        text &= "  -> ." & info.默认扩展名
        Return text
    End Function

    Private Shared Function 构建副文本(info As 抽流流信息) As String
        Dim parts As New List(Of String)
        If info.元数据标题 <> "" Then parts.Add(info.元数据标题)
        If info.类型 = 抽流流类型.附件 AndAlso info.附件文件名 <> "" AndAlso Not parts.Contains(info.附件文件名) Then parts.Add(info.附件文件名)
        If info.编码器完整名称 <> "" AndAlso Not String.Equals(info.编码器完整名称, info.编码器, StringComparison.OrdinalIgnoreCase) Then parts.Add(info.编码器完整名称)
        Return String.Join("   ", parts)
    End Function

    Private Sub 呈现流列表()
        Dim 控件列表 As New List(Of Control)

        If 流列表.Count = 0 Then
            控件列表.Add(创建说明("未发现可提取的视频、音频、字幕或附件流", Color.IndianRed))
            呈现控件(控件列表)
            Exit Sub
        End If

        控件列表.Add(创建说明(Path.GetFileName(当前文件), Color.Silver, 13.0F, True))
        添加分组(控件列表, "视频流", 抽流流类型.视频)
        添加分组(控件列表, "音频流", 抽流流类型.音频)
        添加分组(控件列表, "字幕流", 抽流流类型.字幕)
        添加分组(控件列表, "附件流", 抽流流类型.附件)
        呈现控件(控件列表)
    End Sub

    Private Sub 添加分组(控件列表 As List(Of Control), 标题 As String, 类型 As 抽流流类型)
        Dim items = 流列表.Where(Function(x) x.类型 = 类型).ToList()
        If items.Count = 0 Then Exit Sub
        控件列表.Add(创建说明(标题, 获取类型颜色(类型), 12.0F, True))
        For Each info In items
            控件列表.Add(创建流复选框(info))
        Next
    End Sub

    Private Function 创建说明(text As String, color As Color, Optional size As Single = 10.0F, Optional 作为标题 As Boolean = False) As HtmlColorLabel
        Return New HtmlColorLabel With {
            .AutoSize = True,
            .AutoSizeMode = AutoSizeMode.GrowAndShrink,
            .AllowDrop = True,
            .Dock = DockStyle.Top,
            .ForeColor = Color.FromArgb(120, 255, 255, 255),
            .Margin = New Padding(2),
            .Padding = If(作为标题, New Padding(0, 10, 0, 10), New Padding(0, 0, 0, 10)),
            .Text = $"<span style=""font-size:{CInt(size)}; color:{ColorTranslator.ToHtml(color)}"">{转义Html(text)}</span>"
        }
    End Function

    Private Function 创建流复选框(info As 抽流流信息) As ModernCheckBox
        Dim cb As New ModernCheckBox With {
            .AllowDrop = True,
            .AutoSize = True,
            .Text = info.主文本,
            .SubText = info.副文本,
            .SubTextForeColor = Color.FromArgb(120, 255, 255, 255),
            .MainSubTextSpacing = 3,
            .Tag = info,
            .Checked = False,
            .ClickAnywhere = True,
            .BoxBorderRadius = 5,
            .BoxBorderSize = 0,
            .BoxCheckedBackColor = 获取类型颜色(info.类型),
            .BoxUncheckedBackColor = Color.FromArgb(40, 220, 220, 220),
            .BoxInnerPadding = 6,
            .BoxSize = 22,
            .BoxTextSpacing = 10,
            .Dock = DockStyle.Top,
            .Padding = New Padding(0, 0, 0, 10)
        }
        info.复选框 = cb
        绑定文件拖入(cb)
        Return cb
    End Function

    Private Sub 呈现控件(控件列表 As List(Of Control))
        ModernPanel2.SuspendLayout()
        Try
            ModernPanel2.Controls.Clear()
            For i = 控件列表.Count - 1 To 0 Step -1
                绑定文件拖入(控件列表(i))
                ModernPanel2.Controls.Add(控件列表(i))
            Next
        Finally
            ModernPanel2.ResumeLayout(False)
            ModernPanel2.PerformLayout()
        End Try
    End Sub

    Private Sub 显示提示(text As String, color As Color)
        呈现控件(New List(Of Control) From {创建说明(text, color, 10.0F, False)})
    End Sub

    Private Async Sub MB_提取所选_MouseUp(sender As Object, e As MouseEventArgs) Handles MB_提取所选.MouseUp
        If 正在运行 Then
            取消当前提取()
            Exit Sub
        End If
        If e.Button <> MouseButtons.Left AndAlso e.Button <> MouseButtons.Right Then Exit Sub
        Await 提取所选Async(e.Button = MouseButtons.Right)
    End Sub

    Private Async Function 提取所选Async(强制自动命名 As Boolean) As Task
        If 当前文件 = "" OrElse Not File.Exists(当前文件) Then
            ExFloatingTip(MB_打开文件, "请先打开媒体文件", 1800)
            Exit Function
        End If

        Dim selected = 流列表.Where(Function(x) x.复选框 IsNot Nothing AndAlso x.复选框.Checked).ToList()
        If selected.Count = 0 Then
            ExFloatingTip(ModernPanel2, "请选择要提取的流", 1800)
            Exit Function
        End If

        Dim outputDir = 获取输出目录()
        If outputDir = "" Then Exit Function
        If Not Directory.Exists(outputDir) Then
            ExFloatingTip(MCB_输出位置, "输出目录不存在", 1800)
            Exit Function
        End If

        Dim 输出表 = 获取输出路径表(selected, outputDir, 强制自动命名)
        If 输出表 Is Nothing OrElse 输出表.Count = 0 Then Exit Function
        If 输出表.Values.Any(Function(x) String.Equals(Path.GetFullPath(x), Path.GetFullPath(当前文件), StringComparison.OrdinalIgnoreCase)) Then
            ExFloatingTip(MB_提取所选, "输出文件不能覆盖源文件", 2200)
            Exit Function
        End If

        取消令牌源 = New CancellationTokenSource()
        设置运行状态(True)

        Try
            Await 运行提取Async(selected, 输出表, 取消令牌源.Token)
            设置提取进度(100)
            ExFloatingTip(MB_提取所选, $"已提取 {selected.Count} 个流", 1600)
        Catch ex As OperationCanceledException
            ExFloatingTip(MB_提取所选, "已取消", 1600)
        Catch ex As Exception
            ExFloatingTip(MB_提取所选, "提取失败：" & ex.Message, 2600)
        Finally
            取消令牌源?.Dispose()
            取消令牌源 = Nothing
            设置运行状态(False)
        End Try
    End Function

    Private Function 获取输出路径表(selected As List(Of 抽流流信息), outputDir As String, 强制自动命名 As Boolean) As Dictionary(Of 抽流流信息, String)
        Dim result As New Dictionary(Of 抽流流信息, String)
        Dim used As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)

        If selected.Count = 1 AndAlso Not 强制自动命名 Then
            Dim info = selected(0)
            Dim suggested = 获取自动文件名(info)
            Using d As New SaveFileDialog With {
                .InitialDirectory = outputDir,
                .FileName = suggested,
                .Filter = 获取保存过滤器(info.默认扩展名),
                .OverwritePrompt = True
            }
                If d.ShowDialog(Me) <> DialogResult.OK Then Return Nothing
                result(info) = d.FileName
            End Using
            Return result
        End If

        For Each info In selected
            Dim path = 获取唯一输出路径(outputDir, 获取自动文件名(info), used)
            used.Add(path)
            result(info) = path
        Next
        Return result
    End Function

    Private Async Function 运行提取Async(selected As List(Of 抽流流信息), 输出表 As Dictionary(Of 抽流流信息, String), token As CancellationToken) As Task
        Dim total = Math.Max(1, selected.Count)
        For i = 0 To selected.Count - 1
            token.ThrowIfCancellationRequested()
            Dim info = selected(i)
            Dim output = 输出表(info)
            Dim baseProgress = i / CDbl(total)
            设置提取进度(baseProgress * 100)

            If info.类型 = 抽流流类型.附件 Then
                Dim args = $"-hide_banner -nostdin -y -dump_attachment:t:{info.类型序号} {引用参数(output)} -i {引用参数(当前文件)} -f null -"
                Dim result = Await 运行FFmpegAsync(args, token, Nothing)
                If result.ExitCode <> 0 Then Throw New InvalidOperationException(提取错误摘要(result.Output))
            Else
                Dim itemProgress As New 编码进度_v6()
                Dim args = 构建普通流提取参数(info, output)
                Dim result = Await 运行FFmpegAsync(args, token, Sub(line)
                                                                  itemProgress.解析FFmpeg输出(line, 当前总时长)
                                                                  Dim p = If(itemProgress.百分比 > 0, itemProgress.百分比, 0)
                                                                  设置提取进度((baseProgress + p / total) * 100)
                                                              End Sub)
                If result.ExitCode <> 0 Then Throw New InvalidOperationException(提取错误摘要(result.Output))
            End If

            设置提取进度(((i + 1) / CDbl(total)) * 100)
        Next
    End Function

    Private Function 构建普通流提取参数(info As 抽流流信息, output As String) As String
        Dim codecPart = "-c copy"
        If info.类型 = 抽流流类型.字幕 AndAlso String.Equals(info.编码器, "mov_text", StringComparison.OrdinalIgnoreCase) Then
            codecPart = "-c:s srt"
        End If
        Return $"-hide_banner -nostdin -i {引用参数(当前文件)} -map 0:{info.全局索引} {codecPart} -map_metadata -1 {引用参数(output)} -y"
    End Function

    Private Shared Function 提取错误摘要(output As String) As String
        Dim lines = If(output, "").Split({vbCrLf, vbLf}, StringSplitOptions.RemoveEmptyEntries).Select(Function(x) x.Trim()).Where(Function(x) x <> "").ToList()
        If lines.Count = 0 Then Return "ffmpeg 执行失败"
        Return lines.Last()
    End Function

    Private Shared Function 获取保存过滤器(ext As String) As String
        ext = 清理扩展名(ext)
        If ext = "" Then Return "所有文件|*.*"
        Return $"{ext.ToUpperInvariant()} 文件|*.{ext}|所有文件|*.*"
    End Function

    Private Function 获取自动文件名(info As 抽流流信息) As String
        If info.类型 = 抽流流类型.附件 AndAlso info.附件文件名 <> "" Then
            Dim attachmentName = 清理文件名(Path.GetFileName(info.附件文件名))
            If Path.GetExtension(attachmentName) = "" Then attachmentName &= "." & info.默认扩展名
            Return attachmentName
        End If

        Dim baseName = 清理文件名(Path.GetFileNameWithoutExtension(当前文件))
        Dim typeName = 获取类型短名(info.类型)
        Dim codecName = 清理文件名(If(info.编码器 <> "", info.编码器, "unknown"))
        Return $"{baseName}_{typeName}{info.类型序号}_{codecName}.{info.默认扩展名}"
    End Function

    Private Shared Function 获取唯一输出路径(outputDir As String, fileName As String, used As HashSet(Of String)) As String
        Dim cleanName = 清理文件名(fileName)
        Dim ext = Path.GetExtension(cleanName)
        Dim nameWithoutExt = If(ext = "", cleanName, Path.GetFileNameWithoutExtension(cleanName))
        If nameWithoutExt = "" Then nameWithoutExt = "stream"
        Dim candidate = Path.Combine(outputDir, nameWithoutExt & ext)
        Dim n = 1
        Do While File.Exists(candidate) OrElse used.Contains(candidate)
            candidate = Path.Combine(outputDir, $"{nameWithoutExt}_{n}{ext}")
            n += 1
        Loop
        Return candidate
    End Function

    Private Function 获取输出目录() As String
        Dim text = If(MCB_输出位置.Text, "").Trim()
        If text = "" OrElse text = "输出到原目录" OrElse text = "浏览 ..." Then
            Return If(当前文件 = "", "", Path.GetDirectoryName(当前文件))
        End If
        If Directory.Exists(text) Then Return text
        If File.Exists(text) Then Return Path.GetDirectoryName(text)
        Return text
    End Function

    Private Sub MCB_输出位置_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_输出位置.SelectedIndexChanged
        If 正在运行 Then Exit Sub
        If MCB_输出位置.SelectedIndex <> 1 Then Exit Sub
        Using d As New FolderBrowserDialog()
            If 当前文件 <> "" AndAlso Directory.Exists(Path.GetDirectoryName(当前文件)) Then d.SelectedPath = Path.GetDirectoryName(当前文件)
            If d.ShowDialog(Me) = DialogResult.OK Then
                MCB_输出位置.Text = 规范化文件夹路径(d.SelectedPath)
            Else
                MCB_输出位置.SelectedIndex = 0
            End If
        End Using
    End Sub

    Private Sub 设置运行状态(running As Boolean)
        正在运行 = running
        MB_提取所选.Text = If(running, "0.0%", 提取按钮默认文本)
        MB_提取所选.ForeColor = If(running, Color.IndianRed, Color.YellowGreen)
        MB_打开文件.Enabled = Not running
        MCB_输出位置.Enabled = Not running
        For Each info In 流列表
            If info.复选框 IsNot Nothing Then info.复选框.Enabled = Not running
        Next
    End Sub

    Private Sub 设置提取进度(percent As Double)
        Dim safePercent = Math.Max(0, Math.Min(100, percent))
        If MB_提取所选.InvokeRequired Then
            Try
                MB_提取所选.BeginInvoke(Sub() 设置提取进度(safePercent))
            Catch
            End Try
            Exit Sub
        End If
        MB_提取所选.Text = $"{safePercent:F1}%"
    End Sub

    Private Sub 取消当前提取()
        Try
            取消令牌源?.Cancel()
            SyncLock 进程锁
                If 当前进程 IsNot Nothing AndAlso Not 当前进程.HasExited Then 当前进程.Kill()
            End SyncLock
        Catch
        End Try
    End Sub

    Private Sub Form_v6_集成工具_抽流_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        取消当前提取()
    End Sub

    Private Async Function 运行进程读取输出Async(fileName As String, arguments As String, token As CancellationToken) As Task(Of 进程运行结果)
        Using process As New Process()
            process.StartInfo.FileName = fileName
            process.StartInfo.WorkingDirectory = If(设置_v6.实例对象.工作目录 <> "", 设置_v6.实例对象.工作目录, "")
            process.StartInfo.Arguments = arguments
            process.StartInfo.UseShellExecute = False
            process.StartInfo.RedirectStandardOutput = True
            process.StartInfo.RedirectStandardError = True
            process.StartInfo.StandardOutputEncoding = Encoding.UTF8
            process.StartInfo.StandardErrorEncoding = Encoding.UTF8
            process.StartInfo.CreateNoWindow = True
            Using registration = token.Register(Sub()
                                                    Try
                                                        If Not process.HasExited Then process.Kill()
                                                    Catch
                                                    End Try
                                                End Sub)
                process.Start()
                Dim stdoutTask = process.StandardOutput.ReadToEndAsync(token)
                Dim stderrTask = process.StandardError.ReadToEndAsync(token)
                Await process.WaitForExitAsync(token)
                Dim output = Await stdoutTask
                Dim [error] = Await stderrTask
                Return New 进程运行结果 With {.ExitCode = process.ExitCode, .Output = output & If([error] <> "", vbCrLf & [error], "")}
            End Using
        End Using
    End Function

    Private Async Function 运行FFmpegAsync(arguments As String, token As CancellationToken, onLine As Action(Of String)) As Task(Of 进程运行结果)
        Dim tcs As New TaskCompletionSource(Of Integer)(TaskCreationOptions.RunContinuationsAsynchronously)
        Dim stdoutClosed As New TaskCompletionSource(Of Boolean)(TaskCreationOptions.RunContinuationsAsynchronously)
        Dim stderrClosed As New TaskCompletionSource(Of Boolean)(TaskCreationOptions.RunContinuationsAsynchronously)
        Dim output As New StringBuilder()
        Dim process As New Process()

        process.StartInfo.FileName = 获取FFmpeg文件名()
        process.StartInfo.WorkingDirectory = If(设置_v6.实例对象.工作目录 <> "", 设置_v6.实例对象.工作目录, "")
        process.StartInfo.Arguments = arguments
        process.StartInfo.UseShellExecute = False
        process.StartInfo.RedirectStandardOutput = True
        process.StartInfo.RedirectStandardError = True
        process.StartInfo.StandardOutputEncoding = Encoding.UTF8
        process.StartInfo.StandardErrorEncoding = Encoding.UTF8
        process.StartInfo.CreateNoWindow = True
        process.EnableRaisingEvents = True

        AddHandler process.OutputDataReceived, Sub(sender, e)
                                                   If e.Data Is Nothing Then
                                                       stdoutClosed.TrySetResult(True)
                                                       Return
                                                   End If
                                                   SyncLock output
                                                       output.AppendLine(e.Data)
                                                   End SyncLock
                                                   onLine?.Invoke(e.Data)
                                               End Sub
        AddHandler process.ErrorDataReceived, Sub(sender, e)
                                                  If e.Data Is Nothing Then
                                                      stderrClosed.TrySetResult(True)
                                                      Return
                                                  End If
                                                  SyncLock output
                                                      output.AppendLine(e.Data)
                                                  End SyncLock
                                                  onLine?.Invoke(e.Data)
                                              End Sub
        AddHandler process.Exited, Sub()
                                       Dim code = -1
                                       Try
                                           code = process.ExitCode
                                       Catch
                                       End Try
                                       tcs.TrySetResult(code)
                                   End Sub

        SyncLock 进程锁
            当前进程 = process
        End SyncLock

        Using registration = token.Register(Sub()
                                                Try
                                                    If Not process.HasExited Then process.Kill()
                                                Catch
                                                End Try
                                                tcs.TrySetCanceled(token)
                                            End Sub)
            Try
                process.Start()
                process.BeginOutputReadLine()
                process.BeginErrorReadLine()
                Dim exitCode = Await tcs.Task
                Await Task.WhenAll(stdoutClosed.Task, stderrClosed.Task)
                Return New 进程运行结果 With {.ExitCode = exitCode, .Output = output.ToString()}
            Finally
                SyncLock 进程锁
                    If Object.ReferenceEquals(当前进程, process) Then 当前进程 = Nothing
                End SyncLock
                Try
                    process.Dispose()
                Catch
                End Try
            End Try
        End Using
    End Function

    Private Shared Function 获取FFmpeg文件名() As String
        Return If(设置_v6.实例对象.替代进程文件名 <> "", 设置_v6.实例对象.替代进程文件名, "ffmpeg")
    End Function

    Private Shared Function 获取FFprobe文件名() As String
        Dim ffmpeg = If(设置_v6.实例对象.替代进程文件名, "").Trim()
        If ffmpeg = "" Then Return "ffprobe"
        Dim fileName = Path.GetFileName(ffmpeg)
        If fileName.IndexOf("ffmpeg", StringComparison.OrdinalIgnoreCase) < 0 Then Return "ffprobe"
        Dim probeFileName = fileName.Replace("ffmpeg", "ffprobe", StringComparison.OrdinalIgnoreCase)
        Dim dir = Path.GetDirectoryName(ffmpeg)
        If dir = "" Then Return probeFileName
        Return Path.Combine(dir, probeFileName)
    End Function

    Private Shared Function 获取默认扩展名(info As 抽流流信息) As String
        If info.类型 = 抽流流类型.附件 Then
            Dim ext = 清理扩展名(Path.GetExtension(info.附件文件名))
            If ext <> "" Then Return ext
            If info.附件MimeType <> "" AndAlso 附件Mime扩展名映射.ContainsKey(info.附件MimeType) Then Return 附件Mime扩展名映射(info.附件MimeType)
            If info.编码器 <> "" AndAlso 流扩展名映射.ContainsKey(info.编码器) Then Return 流扩展名映射(info.编码器)
            Return "bin"
        End If

        If info.编码器 <> "" AndAlso 流扩展名映射.ContainsKey(info.编码器) Then Return 流扩展名映射(info.编码器)

        Select Case info.类型
            Case 抽流流类型.视频 : Return "mkv"
            Case 抽流流类型.音频 : Return "mka"
            Case 抽流流类型.字幕 : Return "mks"
            Case Else : Return "bin"
        End Select
    End Function

    Private Shared Function 规范化编码器(codec As String) As String
        Dim value = If(codec, "").Trim()
        If value = "" Then Return ""
        Dim p = value.IndexOfAny({" "c, "("c, ","c})
        If p > 0 Then value = value.Substring(0, p)
        Return value.Trim().ToLowerInvariant()
    End Function

    Private Shared Function 获取类型短名(类型 As 抽流流类型) As String
        Select Case 类型
            Case 抽流流类型.视频 : Return "v"
            Case 抽流流类型.音频 : Return "a"
            Case 抽流流类型.字幕 : Return "s"
            Case 抽流流类型.附件 : Return "t"
            Case Else : Return "x"
        End Select
    End Function

    Private Shared Function 获取类型颜色(类型 As 抽流流类型) As Color
        Select Case 类型
            Case 抽流流类型.视频 : Return Color.CornflowerBlue
            Case 抽流流类型.音频 : Return Color.MediumPurple
            Case 抽流流类型.字幕 : Return Color.OliveDrab
            Case 抽流流类型.附件 : Return Color.Goldenrod
            Case Else : Return Color.Silver
        End Select
    End Function

    Private Shared Function 构建分辨率(stream As JsonElement) As String
        Dim width = 获取Json整数(stream, "width", 0)
        Dim height = 获取Json整数(stream, "height", 0)
        If width <= 0 OrElse height <= 0 Then Return ""
        Return $"{width}x{height}"
    End Function

    Private Shared Function 格式化帧率(value As String) As String
        value = If(value, "").Trim()
        If value = "" OrElse value = "0/0" Then Return ""
        Dim fps As Double
        If value.Contains("/"c) Then
            Dim parts = value.Split("/"c)
            Dim num, den As Double
            If parts.Length = 2 AndAlso
               Double.TryParse(parts(0), NumberStyles.Any, CultureInfo.InvariantCulture, num) AndAlso
               Double.TryParse(parts(1), NumberStyles.Any, CultureInfo.InvariantCulture, den) AndAlso den <> 0 Then
                fps = num / den
            End If
        Else
            Double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, fps)
        End If
        If fps <= 0 Then Return ""
        If Math.Abs(fps - Math.Round(fps)) < 0.01 Then Return $"{CInt(Math.Round(fps))}fps"
        Return fps.ToString("0.###", CultureInfo.InvariantCulture) & "fps"
    End Function

    Private Shared Function 格式化比特率(value As String) As String
        Dim bitrate As Long
        If Not Long.TryParse(If(value, "").Trim(), bitrate) OrElse bitrate <= 0 Then Return ""
        Return $"{CLng(Math.Round(bitrate / 1000.0R))}kbps"
    End Function

    Private Shared Function 获取Json字符串(element As JsonElement, name As String) As String
        Dim prop As JsonElement
        If Not TryGetJsonProperty(element, name, prop) Then Return ""
        Select Case prop.ValueKind
            Case JsonValueKind.String
                Return If(prop.GetString(), "")
            Case JsonValueKind.Number
                Return prop.GetRawText()
            Case Else
                Return ""
        End Select
    End Function

    Private Shared Function 获取Json整数(element As JsonElement, name As String, fallback As Integer) As Integer
        Dim text = 获取Json字符串(element, name)
        Dim value As Integer
        If Integer.TryParse(text, value) Then Return value
        Return fallback
    End Function

    Private Shared Function 获取Json标签(stream As JsonElement, name As String) As String
        Dim tags As JsonElement
        If Not TryGetJsonProperty(stream, "tags", tags) Then Return ""
        Return 获取Json字符串(tags, name).Trim()
    End Function

    Private Shared Function TryGetJsonProperty(element As JsonElement, name As String, ByRef value As JsonElement) As Boolean
        If element.ValueKind <> JsonValueKind.Object Then Return False
        If element.TryGetProperty(name, value) Then Return True
        For Each prop In element.EnumerateObject()
            If String.Equals(prop.Name, name, StringComparison.OrdinalIgnoreCase) Then
                value = prop.Value
                Return True
            End If
        Next
        Return False
    End Function

    Private Shared Function 清理扩展名(ext As String) As String
        ext = If(ext, "").Trim()
        If ext.StartsWith(".", StringComparison.Ordinal) Then ext = ext.Substring(1)
        Return 清理文件名(ext).Trim("."c).ToLowerInvariant()
    End Function

    Private Shared Function 清理文件名(fileName As String) As String
        Dim value = If(fileName, "").Trim()
        If value = "" Then Return "stream"
        For Each ch In Path.GetInvalidFileNameChars()
            value = value.Replace(ch, "_"c)
        Next
        Return value.Trim()
    End Function

    Private Shared Function 转义Html(text As String) As String
        Return If(text, "").Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("""", "&quot;")
    End Function

    Private Shared Function 引用参数(value As String) As String
        Return $"""{If(value, "").Replace("""", "\""")}"""
    End Function

End Class
