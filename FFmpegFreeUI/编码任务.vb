Imports System.IO
Imports System.Text.RegularExpressions

Public Class 编码任务
    Enum 编码状态
        未处理 = 0
        正在处理 = 1
        已暂停 = 2
        已完成 = 10
        已停止 = 20
        错误 = 99
    End Enum

    Public Shared Property 队列 As New List(Of 单片任务)

    Public Shared Sub 检查是否有可以开始的任务()
        Dim a As Integer = 获取正在处理的任务数量()
        If a < 同时运行任务上限 Then 开始还未处理的任务(a)
    End Sub
    Public Shared Function 获取正在处理的任务数量() As Integer
        Dim 任务数量 As Integer = 0
        For Each item As 单片任务 In 队列
            If item.状态 = 编码状态.正在处理 Then
                任务数量 += 1
            End If
        Next
        Return 任务数量
    End Function
    Public Shared Sub 开始还未处理的任务(当前正在运行的任务数量 As Integer)
        Dim 已运行的任务数量 As Integer = 当前正在运行的任务数量
        For Each item As 单片任务 In 队列
            If item.状态 = 编码状态.未处理 Then
                Task.Run(AddressOf item.开始)
                已运行的任务数量 += 1
                If 已运行的任务数量 >= 同时运行任务上限 Then Exit Sub
            End If
        Next

        Task.Run(Sub()
                     If 获取正在处理的任务数量() = 0 Then
                         If 全部任务已完成是否有错误 Then
                             If Form1.使用提示音 Then My.Computer.Audio.Play(My.Resources.Resource1.错误, AudioPlayMode.Background)
                             全部任务已完成是否有错误 = False
                         Else
                             If Form1.使用提示音 Then My.Computer.Audio.Play(My.Resources.Resource1.完成, AudioPlayMode.Background)
                         End If
                         恢复系统状态()
                     End If
                 End Sub)
    End Sub

    Public Shared Property 全部任务已完成是否有错误 As Boolean = False

    Public Class 单片任务

        Public Property 预设数据 As 预设数据类型
        Public Property 输入文件 As String = ""
        Public Property 输出文件 As String = ""
        Public Property 自定义输出位置 As String = ""
        Public Property 命令行 As String = ""
        Public Property 获取_总时长 As TimeSpan = TimeSpan.Zero
        Public Property 已获取到总时长 As Boolean = False
        Public Property 实时_frame As String = ""
        Public Property 实时_fps As String = ""
        Public Property 实时_q As String = ""
        Public Property 实时_size As String = ""
        Public Property 实时_time As TimeSpan = TimeSpan.Zero
        Public Property 实时_bitrate As String = ""
        Public Property 实时_speed As String = ""
        Public Property 状态 As 编码状态 = 编码状态.未处理
        Public Property 列表视图项 As ListViewItem = Nothing

        Public Property 实时输出 As String = ""
        Public Property 错误列表 As New List(Of String)

        Public Property FFmpegProcess As Process
        Public Property 任务耗时计时器 As New Stopwatch
        Public Property 上次刷新界面的时间戳 As TimeSpan = Now.TimeOfDay

        Public Sub 开始()
            Try
                错误列表.Clear()
                状态 = 编码状态.正在处理

                If 预设数据 Is Nothing Then GoTo jx1

                If 预设数据.视频参数_降噪_方式 = "avs" Then
                    If My.Computer.FileSystem.FileExists(Path.Combine(Application.StartupPath, "AviSynth.avs")) Then
                        Dim avs1 As String = File.ReadAllText(Path.Combine(Application.StartupPath, "AviSynth.avs"))
                        avs1 = avs1.Replace("<FilePath>", 输入文件)
                        File.WriteAllText(Path.Combine(Path.GetDirectoryName(输入文件), Path.GetFileNameWithoutExtension(输入文件) & ".avs"), avs1, New Text.UTF8Encoding(False))
                    Else
                        Err.Raise(10001, "", "AviSynth.avs 脚本文件不存在，请检查是否将其放置于程序目录下！")
                    End If
                End If

                If 自定义输出位置 = "" Then
                    输出文件 = 计算输出位置_原目录(输入文件, 预设数据.输出容器)
                Else
                    输出文件 = 计算输出位置_自定义目录(自定义输出位置, 输入文件, 预设数据.输出容器)
                End If

                命令行 = 预设管理.将预设数据转换为命令行(预设数据, 输入文件, 输出文件)

                If 预设数据.流控制_剪辑_入点 <> "" AndAlso 预设数据.流控制_剪辑_出点 <> "" Then
                    Dim t1 = ParseTimeSpan(预设数据.流控制_剪辑_入点)
                    Dim t2 = ParseTimeSpan(预设数据.流控制_剪辑_出点)
                    获取_总时长 = t2 - t1
                    已获取到总时长 = True
                    GoTo 结束剪辑区间计算
                End If
                If 预设数据.流控制_剪辑_入点 = "" AndAlso 预设数据.流控制_剪辑_出点 <> "" Then
                    获取_总时长 = ParseTimeSpan(预设数据.流控制_剪辑_出点)
                    已获取到总时长 = True
                    GoTo 结束剪辑区间计算
                End If

结束剪辑区间计算:
jx1:
                FFmpegProcess = New Process()
                FFmpegProcess.StartInfo.FileName = "ffmpeg"
                FFmpegProcess.StartInfo.WorkingDirectory = If(Form1.FFmpeg自定义工作目录 <> "", Form1.FFmpeg自定义工作目录, "")
                FFmpegProcess.StartInfo.Arguments = 命令行
                FFmpegProcess.StartInfo.UseShellExecute = False
                FFmpegProcess.StartInfo.RedirectStandardOutput = True
                FFmpegProcess.StartInfo.RedirectStandardError = True
                FFmpegProcess.StartInfo.StandardOutputEncoding = Text.Encoding.UTF8
                FFmpegProcess.StartInfo.StandardErrorEncoding = Text.Encoding.UTF8
                FFmpegProcess.StartInfo.CreateNoWindow = True
                FFmpegProcess.EnableRaisingEvents = True
                AddHandler FFmpegProcess.OutputDataReceived, AddressOf FFmpegOutputHandler
                AddHandler FFmpegProcess.ErrorDataReceived, AddressOf FFmpegOutputHandler
                AddHandler FFmpegProcess.Exited, AddressOf FFmpegProcessExited

                FFmpegProcess.Start()
                FFmpegProcess.BeginOutputReadLine()
                FFmpegProcess.BeginErrorReadLine()

                设定系统状态()
                任务耗时计时器.Start()

                If Form1.处理器相关性 <> "" Then
                    Dim coreList() As Integer = Form1.处理器相关性.Split(","c).Select(Function(s) s.Trim()).Where(Function(s) Integer.TryParse(s, Nothing)).Select(Function(s) Integer.Parse(s)).ToArray()
                    FFmpegProcess.ProcessorAffinity = GetAffinityMask(coreList)
                End If

            Catch ex As Exception
                状态 = 编码状态.错误
                界面线程执行(Sub() MsgBox(ex.Message, MsgBoxStyle.Critical))
            End Try
        End Sub

        Public Sub 暂停()
            Try
                If 状态 = 编码状态.正在处理 Then
                    If NtSuspendProcess(FFmpegProcess.Handle) = 0 Then
                        状态 = 编码状态.已暂停
                        任务耗时计时器.Stop()
                        状态刷新统一逻辑()
                    End If
                End If
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical)
            End Try
        End Sub

        Public Sub 恢复()
            Try
                If 状态 = 编码状态.已暂停 Then
                    If NtResumeProcess(FFmpegProcess.Handle) = 0 Then
                        状态 = 编码状态.正在处理
                        任务耗时计时器.Start()
                        状态刷新统一逻辑()
                    End If
                End If
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical)
            End Try
        End Sub

        Dim 手动停止不要尝试启动其他任务 As Boolean = False

        Public Sub 停止()
            Try
                If FFmpegProcess.HasExited = False Then
                    手动停止不要尝试启动其他任务 = True
                    FFmpegProcess.Kill()
                    FFmpegProcess.WaitForExit()
                    状态 = 编码状态.已停止
                    任务耗时计时器.Stop()
                    状态刷新统一逻辑()
                End If
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical)
            End Try
        End Sub

        Public Sub 清除占用()
            Try
                If FFmpegProcess IsNot Nothing Then
                    If Not FFmpegProcess.HasExited Then
                        FFmpegProcess.Kill()
                    End If
                End If
                FFmpegProcess?.Dispose()
                列表视图项?.Remove()
                任务耗时计时器.Stop()
                GC.Collect()
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical)
            End Try
        End Sub

        Public errorKeywords As String() = {"Error", "Invalid", "cannot", "failed", "not supported", "require", "must be", "Could not", "is experimental", "if you want to use it", "Nothing was written"}

        Public Sub FFmpegOutputHandler(sender As Object, e As DataReceivedEventArgs)
            If e.Data Is Nothing Then Exit Sub
            实时输出 = e.Data

            If Not 已获取到总时长 AndAlso e.Data.Contains("Duration") Then
                Dim durationMatch = DurationPattern.Match(e.Data)
                If durationMatch.Success Then
                    If 预设数据.流控制_剪辑_入点 <> "" AndAlso 预设数据.流控制_剪辑_出点 = "" Then
                        获取_总时长 = ParseTimeSpan(durationMatch.Groups(1).Value) - ParseTimeSpan(预设数据.流控制_剪辑_入点)
                    Else
                        获取_总时长 = ParseTimeSpan(durationMatch.Groups(1).Value)
                    End If
                    已获取到总时长 = True
                End If
            End If

            界面线程执行(AddressOf 状态刷新统一逻辑)

            If errorKeywords.Any(Function(keyword) e.Data.Contains(keyword, StringComparison.OrdinalIgnoreCase)) Then
                错误列表.Add(e.Data)
            End If
        End Sub

        Public Sub FFmpegProcessExited(sender As Object, e As EventArgs)
            If FFmpegProcess.ExitCode = 0 Then
                状态 = 编码状态.已完成

                If 预设数据.视频参数_降噪_方式 = "avs" Then
                    If My.Computer.FileSystem.FileExists(Path.Combine(Path.GetDirectoryName(输入文件), Path.GetFileNameWithoutExtension(输入文件) & ".avs")) Then
                        My.Computer.FileSystem.DeleteFile(Path.Combine(Path.GetDirectoryName(输入文件), Path.GetFileNameWithoutExtension(输入文件) & ".avs"))
                    End If
                End If

            Else
                状态 = 编码状态.错误
                If My.Computer.FileSystem.FileExists(输出文件) Then
                    Select Case Path.GetExtension(输出文件).ToLower.Trim
                        Case ".mp4" : My.Computer.FileSystem.DeleteFile(输出文件)
                    End Select
                End If
                全部任务已完成是否有错误 = True
            End If
            任务耗时计时器.Stop()
            GC.Collect()

            界面线程执行(AddressOf 状态刷新统一逻辑)
        End Sub
        Public Sub 状态刷新统一逻辑()
            If 列表视图项 Is Nothing Then Exit Sub
            Select Case 状态
                Case 编码状态.未处理
                    列表视图项.ForeColor = Color.Silver
                    列表视图项.SubItems(1).Text = "未处理"
                Case 编码状态.正在处理
                    列表视图项.ForeColor = Color.YellowGreen
                    列表视图项.SubItems(1).Text = "正在处理"
                    If 实时输出.Contains("="c) AndAlso (Now.TimeOfDay - 上次刷新界面的时间戳).TotalSeconds >= 1 Then
                        Task.Run(Sub()
                                     在实时输出中提取数据(实时输出)
                                     在任务进行中时刷新实时信息()
                                 End Sub)
                        上次刷新界面的时间戳 = Now.TimeOfDay
                    End If
                Case 编码状态.已完成
                    列表视图项.ForeColor = Color.OliveDrab
                    列表视图项.SubItems(1).Text = "已完成"
                    列表视图项.SubItems(2).Text = "100%"
                    Dim 输入文件信息 As New IO.FileInfo(输入文件)
                    Dim 输出文件信息 As New IO.FileInfo(输出文件)
                    Dim sizeText As String = ""
                    If 输出文件信息.Exists Then
                        Dim sizeValue As Long = 输出文件信息.Length / 1024
                        If sizeValue >= 1024 * 1024 Then
                            sizeText = $"{sizeValue / 1024.0 / 1024.0:F2} GB"
                        ElseIf sizeValue >= 1024 Then
                            sizeText = $"{sizeValue / 1024.0:F0} MB"
                        Else
                            sizeText = $"{sizeValue} KB"
                        End If
                    End If
                    列表视图项.SubItems(4).Text = $"{sizeText} ({$"{输出文件信息.Length / 输入文件信息.Length * 100:F0}%"})"
                    列表视图项.SubItems(5).Text = ""
                    Dim elapsed = 任务耗时计时器.Elapsed
                    Dim elapsedParts As New List(Of String)
                    If elapsed.Hours > 0 Then elapsedParts.Add($"{elapsed.Hours} 时")
                    If elapsed.Minutes > 0 OrElse elapsedParts.Count > 0 Then elapsedParts.Add($"{elapsed.Minutes} 分")
                    elapsedParts.Add($"{elapsed.Seconds} 秒")
                    列表视图项.SubItems(7).Text = "耗时 " & String.Join(" ", elapsedParts)
                    If Not 手动停止不要尝试启动其他任务 Then
                        Task.Run(AddressOf 检查是否有可以开始的任务)
                    Else
                        手动停止不要尝试启动其他任务 = True
                    End If
                Case 编码状态.已暂停
                    列表视图项.ForeColor = Color.Goldenrod
                    列表视图项.SubItems(1).Text = "已暂停"
                Case 编码状态.已停止
                    列表视图项.ForeColor = Color.IndianRed
                    列表视图项.SubItems(1).Text = "已停止"
                    列表视图项.SubItems(3).Text = ""
                    列表视图项.SubItems(4).Text = ""
                    列表视图项.SubItems(5).Text = ""
                    列表视图项.SubItems(6).Text = ""
                    列表视图项.SubItems(7).Text = ""
                Case 编码状态.错误
                    列表视图项.ForeColor = Color.IndianRed
                    列表视图项.SubItems(1).Text = "错误"
                    列表视图项.SubItems(5).Text = ""
                    列表视图项.SubItems(7).Text = ""
            End Select
        End Sub
        Public Sub 在任务进行中时刷新实时信息()
            If 列表视图项 Is Nothing Then Exit Sub
            Dim total = 获取_总时长.TotalSeconds, cur = 实时_time.TotalSeconds, progress = 0.0, progressText = "N/A"
            If total > 0 AndAlso cur > 0 Then
                progress = Math.Min(cur / total, 1.0) : progressText = $"{progress * 100:F1}%"
                If progress = 0 Then progressText = "N/A"
            End If
            Dim speedPercent = "N/A", speedVal As Double
            If Not String.IsNullOrWhiteSpace(实时_speed) AndAlso Double.TryParse(实时_speed.Replace("x", "").Trim(), Globalization.NumberStyles.Any, Globalization.CultureInfo.InvariantCulture, speedVal) AndAlso speedVal > 0 Then
                speedPercent = $"{speedVal * 100:F0}%"
            End If
            If progress = 1 AndAlso speedPercent = "N/A" Then speedPercent = ""
            Dim sizeText = "N/A", sizeVal As Long
            If Not String.IsNullOrWhiteSpace(实时_size) AndAlso Long.TryParse(实时_size, sizeVal) AndAlso sizeVal > 0 Then
                If sizeVal >= 1048576 Then
                    sizeText = $"{sizeVal / 1048576.0:F2} GB"
                ElseIf sizeVal >= 1024 Then
                    sizeText = $"{sizeVal / 1024.0:F0} MB"
                Else
                    sizeText = $"{sizeVal} KB"
                End If
            ElseIf 实时_size <> "0" Then
                sizeText = 实时_size
            End If
            Dim estimatedSize = ""
            If progress > 0 AndAlso progress < 1 AndAlso Long.TryParse(实时_size, sizeVal) AndAlso sizeVal > 0 Then
                Dim es = sizeVal / progress
                If es >= 1048576 Then
                    estimatedSize = $" - {es / 1048576.0:F1} GB"
                ElseIf es >= 1024 Then
                    estimatedSize = $" - {es / 1024.0:F0} MB"
                Else
                    estimatedSize = $" - {es:F0} KB"
                End If
            End If
            Dim qText = If(String.IsNullOrWhiteSpace(实时_q) OrElse 实时_q = "0", "N/A", $"{实时_q:F0}")
            Dim bitrateText = If(String.IsNullOrWhiteSpace(实时_bitrate) OrElse 实时_bitrate = "0", "N/A", 实时_bitrate & " kbps")
            If progress = 1 AndAlso bitrateText = "N/A" Then bitrateText = ""
            Dim remainTime = "N/A", rs As Double
            If total > 0 AndAlso cur > 0 AndAlso Double.TryParse(实时_speed.Replace("x", "").Trim(), Globalization.NumberStyles.Any, Globalization.CultureInfo.InvariantCulture, rs) AndAlso rs > 0 Then
                Dim sec = (total - cur) / rs
                If sec > 0 Then
                    Dim h = CInt(Math.Floor(sec / 3600.0))
                    Dim leftover = sec - (h * 3600.0)
                    Dim m = CInt(Math.Floor(leftover / 60.0))
                    Dim s = CInt(Math.Floor(leftover - (m * 60.0)))
                    Dim parts = New List(Of String)
                    If h > 0 Then parts.Add($"{h}h")
                    If m > 0 OrElse h > 0 Then parts.Add($"{m}m")
                    parts.Add($"{s}s")
                    remainTime = String.Join("", parts)
                End If
            End If
            界面线程执行(Sub()
                       Try
                           列表视图项.SubItems(1).Text = 状态.ToString
                           列表视图项.SubItems(2).Text = progressText
                           列表视图项.SubItems(3).Text = speedPercent
                           列表视图项.SubItems(4).Text = sizeText & estimatedSize
                           列表视图项.SubItems(5).Text = qText
                           列表视图项.SubItems(6).Text = bitrateText
                           Dim el = 任务耗时计时器.Elapsed, elapsedParts = New List(Of String)
                           If el.Hours > 0 Then elapsedParts.Add($"{el.Hours}h")
                           If el.Minutes > 0 OrElse elapsedParts.Count > 0 Then elapsedParts.Add($"{el.Minutes}m")
                           elapsedParts.Add($"{el.Seconds}s")
                           列表视图项.SubItems(7).Text = $"{remainTime} - {String.Join("", elapsedParts)}"
                       Catch ex As Exception
                           错误列表.Add($"刷新界面失败 {Now}")
                       End Try
                   End Sub)
        End Sub
        Public Sub 在实时输出中提取数据(line As String)
            If String.IsNullOrEmpty(line) Then Return
            Dim frameStr = ExtractRegexValueAsString(FramePattern, line) : If frameStr <> "" Then 实时_frame = frameStr
            Dim fpsStr = ExtractRegexValueAsString(FpsPattern, line) : If fpsStr <> "" Then 实时_fps = fpsStr
            Dim qStr = ExtractRegexValueAsString(QPattern, line) : If qStr <> "" Then 实时_q = qStr
            Dim sm = SizePattern.Match(line)
            If sm.Success Then
                Dim val = sm.Groups("value").Value, unit = sm.Groups("unit").Value, sz As Long
                实时_size = If(Long.TryParse(val, sz), ConvertToKB(sz, unit).ToString(), val)
            End If
            Dim t = ExtractRegexValueAsString(TimePattern, line) : If t <> "" Then 实时_time = ParseTimeSpan(t)
            Dim br = ExtractRegexValueAsString(BitratePattern, line) : If br <> "" Then 实时_bitrate = br
            Dim sp = ExtractRegexValueAsString(SpeedPattern, line) : If sp <> "" Then 实时_speed = sp
        End Sub

    End Class

    Public Shared ReadOnly DurationPattern As New Regex("Duration:\s*(\d{2}:\d{2}:\d{2}\.\d{2})", RegexOptions.Compiled)

    Public Shared ReadOnly FramePattern As New Regex("frame=\s*(?<value>\d+)", RegexOptions.Compiled)
    Public Shared ReadOnly FpsPattern As New Regex("fps=\s*(?<value>\d+)", RegexOptions.Compiled)
    Public Shared ReadOnly QPattern As New Regex("q=\s*(?<value>[\d\.]+)", RegexOptions.Compiled)
    Public Shared ReadOnly SizePattern As New Regex("size=\s*(?<value>\d+)\s*(?<unit>[KMG]iB)", RegexOptions.Compiled)
    Public Shared ReadOnly TimePattern As New Regex("time=\s*(?<value>\d{2}:\d{2}:\d{2}\.\d{2})", RegexOptions.Compiled)
    Public Shared ReadOnly BitratePattern As New Regex("bitrate=\s*(?<value>[\d\.]+)\s*kbits/s", RegexOptions.Compiled)
    Public Shared ReadOnly SpeedPattern As New Regex("speed=\s*(?<value>[\d\.eE\+\-]+)\s*x", RegexOptions.Compiled)

    Shared Function ParseTimeSpan(timeStr As String) As TimeSpan
        Try
            Dim hours As Integer = 0
            Dim minutes As Integer = 0
            Dim seconds As Integer = 0
            Dim milliseconds As Integer = 0
            Dim timeParts = timeStr.Split(":"c)
            If timeParts.Length < 3 Then Return TimeSpan.Zero
            hours = Integer.Parse(timeParts(0))
            minutes = Integer.Parse(timeParts(1))
            Dim secPart = timeParts(2)
            If secPart.Contains("."c) Then
                Dim secMs = secPart.Split("."c)
                seconds = Integer.Parse(secMs(0))
                Dim msStr = secMs(1).PadRight(3, "0"c)
                milliseconds = Integer.Parse(msStr.Substring(0, 3))
            Else
                seconds = Integer.Parse(secPart)
                milliseconds = 0
            End If
            Return New TimeSpan(0, hours, minutes, seconds, milliseconds)
        Catch
            Return TimeSpan.Zero
        End Try
    End Function

    Shared Function ConvertToKB(value As Long, unit As String) As Long
        Select Case unit.ToUpper()
            Case "KIB" : Return value
            Case "MIB" : Return value * 1024
            Case "GIB" : Return value * 1024 * 1024
            Case Else : Return value
        End Select
    End Function

    Shared Function ExtractRegexValueAsString(pattern As Regex, input As String) As String
        Dim m = pattern.Match(input)
        If m.Success AndAlso m.Groups("value").Success Then
            Return m.Groups("value").Value
        End If
        Return ""
    End Function

    Shared Function 计算输出位置_原目录(输入文件 As String, 容器 As String) As String
        Dim 输出目录 As String = IO.Path.GetDirectoryName(输入文件)
        If Not 容器.StartsWith("."c) Then
            容器 = "." & 容器
        End If
        Dim 输出文件名 As String = IO.Path.GetFileNameWithoutExtension(输入文件) & $"_{Now:yyyy.MM.dd-HH.mm.ss}" & 容器
        Return IO.Path.Combine(输出目录, 输出文件名)
    End Function

    Shared Function 计算输出位置_自定义目录(自定义目录 As String, 输入文件 As String, 容器 As String) As String
        Dim 输出目录 As String = 自定义目录
        If Not 容器.StartsWith("."c) Then
            容器 = "." & 容器
        End If
        Dim 输出文件名 As String = IO.Path.GetFileNameWithoutExtension(输入文件) & $"_{Now:yyyy.MM.dd-HH.mm.ss}" & 容器
        Return IO.Path.Combine(输出目录, 输出文件名)
    End Function

End Class
