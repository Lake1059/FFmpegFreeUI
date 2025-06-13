Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Text.RegularExpressions

Public Class 编码任务
    Public Shared Property 队列 As New List(Of 单片任务)

    Public Shared Sub 检查是否有可以开始的任务()
        If 获取正在处理的任务数量() < 1 Then
            Form1.Invoke(AddressOf 开始排在最前一个未处理的任务)
        End If
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
    Public Shared Sub 开始排在最前一个未处理的任务()
        For Each item As 单片任务 In 队列
            If item.状态 = 编码状态.未处理 Then
                item.开始()
                Exit Sub
            End If
        Next
        SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS)
    End Sub

    Public Class 单片任务
        <DllImport("ntdll.dll", SetLastError:=True)>
        Private Shared Function NtSuspendProcess(processHandle As IntPtr) As Integer
        End Function
        <DllImport("ntdll.dll", SetLastError:=True)>
        Private Shared Function NtResumeProcess(processHandle As IntPtr) As Integer
        End Function

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

        Public Property 计时器 As New Stopwatch

        Public Sub 开始()
            Try

                If 预设数据.视频参数_降噪_方式 = "avs" Then
                    If My.Computer.FileSystem.FileExists(Path.Combine(Application.StartupPath, "AviSynth.avs")) Then
                        Dim avs1 As String = File.ReadAllText(Path.Combine(Application.StartupPath, "AviSynth.avs"))
                        avs1 = avs1.Replace("<FilePath>", 输入文件)
                        System.IO.File.WriteAllText(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(输入文件), System.IO.Path.GetFileNameWithoutExtension(输入文件) & ".avs"), avs1, New System.Text.UTF8Encoding(False))
                    Else
                        Err.Raise(10001, "", "AviSynth.avs 脚本文件不存在，请检查是否将其放置于程序目录下！")
                    End If
                End If

                错误列表.Clear()
                状态 = 编码状态.正在处理
                If 自定义输出位置 = "" Then
                    输出文件 = 计算输出位置_原目录(输入文件, 预设数据.输出容器)
                Else
                    输出文件 = 计算输出位置_自定义目录(自定义输出位置, 输入文件, 预设数据.输出容器)
                End If
                命令行 = 预设管理.将预设数据转换为命令行(预设数据, 输入文件, 输出文件)
                If 预设数据.流控制_快速剪辑_入点 <> "" AndAlso 预设数据.流控制_快速剪辑_出点 <> "" Then
                    Dim t1 = ParseTimeSpan(预设数据.流控制_快速剪辑_入点)
                    Dim t2 = ParseTimeSpan(预设数据.流控制_快速剪辑_出点)
                    获取_总时长 = t2 - t1
                    已获取到总时长 = True
                End If
                FFmpegProcess = New Process()
                FFmpegProcess.StartInfo.FileName = "ffmpeg.exe"
                'FFmpegProcess.StartInfo.WorkingDirectory = Application.StartupPath
                FFmpegProcess.StartInfo.Arguments = 命令行
                FFmpegProcess.StartInfo.UseShellExecute = False
                FFmpegProcess.StartInfo.RedirectStandardOutput = True
                FFmpegProcess.StartInfo.RedirectStandardError = True
                FFmpegProcess.StartInfo.StandardOutputEncoding = System.Text.Encoding.UTF8
                FFmpegProcess.StartInfo.StandardErrorEncoding = System.Text.Encoding.UTF8
                FFmpegProcess.StartInfo.CreateNoWindow = True
                FFmpegProcess.EnableRaisingEvents = True
                AddHandler FFmpegProcess.OutputDataReceived, AddressOf FFmpegOutputHandler
                AddHandler FFmpegProcess.ErrorDataReceived, AddressOf FFmpegOutputHandler
                AddHandler FFmpegProcess.Exited, AddressOf FFmpegProcessExited

                FFmpegProcess.Start()
                FFmpegProcess.BeginOutputReadLine()
                FFmpegProcess.BeginErrorReadLine()
                SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS Or EXECUTION_STATE.ES_SYSTEM_REQUIRED)

                计时器.Start()
                根据状态设置项信息显示()

                Try
                    If Form1.UiTextBox处理器核心.Text <> "" Then
                        Dim coreList() As Integer = Form1.UiTextBox处理器核心.Text.Split(","c).Select(Function(s) s.Trim()).Where(Function(s) Integer.TryParse(s, Nothing)).Select(Function(s) Integer.Parse(s)).ToArray()
                        FFmpegProcess.ProcessorAffinity = GetAffinityMask(coreList)
                    End If
                Catch ex As Exception
                    MsgBox(ex.Message, MsgBoxStyle.Critical)
                End Try

            Catch ex As Exception
                状态 = 编码状态.错误
                MsgBox(ex.Message, MsgBoxStyle.Critical)
            End Try
        End Sub

        Public Sub 暂停()
            Try
                If 状态 = 编码状态.正在处理 Then
                    If NtSuspendProcess(FFmpegProcess.Handle) = 0 Then
                        状态 = 编码状态.已暂停
                        计时器.Stop()
                    End If
                End If
                根据状态设置项信息显示()
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical)
            End Try
        End Sub

        Public Sub 恢复()
            Try
                If 状态 = 编码状态.已暂停 Then
                    If NtResumeProcess(FFmpegProcess.Handle) = 0 Then
                        状态 = 编码状态.正在处理
                        计时器.Start()
                    End If
                End If
                根据状态设置项信息显示()
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
                    计时器.Stop()
                End If
                根据状态设置项信息显示()
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical)
            End Try
        End Sub

        Public Sub 清除占用()
            If FFmpegProcess IsNot Nothing Then
                If Not FFmpegProcess.HasExited Then
                    FFmpegProcess.Kill()
                End If
            End If
            FFmpegProcess?.Dispose()
            列表视图项?.Remove()
            计时器.Stop()
            GC.Collect()
        End Sub

        Public Sub FFmpegOutputHandler(sender As Object, e As DataReceivedEventArgs)
            If e.Data Is Nothing Then Exit Sub
            实时输出 = e.Data

            If e.Data.Contains("Duration") AndAlso Not 已获取到总时长 Then
                Dim durationMatch = DurationPattern.Match(e.Data)
                If durationMatch.Success Then
                    获取_总时长 = ParseTimeSpan(durationMatch.Groups(1).Value)
                    已获取到总时长 = True
                End If
            End If

            If e.Data.Contains("="c) Then
                在实时输出中提取数据(e.Data)
                Form1.重新创建句柄()
                Form1.Invoke(AddressOf 在界面上刷新数据)
                Form1.Invoke(AddressOf 根据状态设置项信息显示)
            End If

            Dim errorKeywords As String() = {"Error", "Invalid", "cannot", "failed", "not supported", "require", "must be"}
            If errorKeywords.Any(Function(keyword) e.Data.Contains(keyword, StringComparison.OrdinalIgnoreCase)) Then
                错误列表.Add(e.Data)
            End If

        End Sub

        Public Sub FFmpegProcessExited(sender As Object, e As EventArgs)
            If FFmpegProcess.ExitCode = 0 Then
                状态 = 编码状态.已完成
            Else
                状态 = 编码状态.错误
            End If
            计时器.Stop()
            Try
                Form1.重新创建句柄()
                Form1.Invoke(AddressOf 根据状态设置项信息显示)
            Catch ex As Exception
                错误列表.Add($"刷新界面失败 {Now}")
            End Try
            If Not 手动停止不要尝试启动其他任务 Then
                检查是否有可以开始的任务()
            Else
                手动停止不要尝试启动其他任务 = True
            End If
            'If 预设数据.视频参数_降噪_方式 = "avs" Then
            '    If My.Computer.FileSystem.FileExists(Path.Combine(Path.GetDirectoryName(输入文件), Path.GetFileNameWithoutExtension(输入文件) & ".avs")) Then
            '        My.Computer.FileSystem.DeleteFile(Path.Combine(Path.GetDirectoryName(输入文件), Path.GetFileNameWithoutExtension(输入文件) & ".avs"))
            '    End If
            'End If
            GC.Collect()
        End Sub

        Public Sub 在界面上刷新数据()
            If 列表视图项 Is Nothing Then Exit Sub

            ' 在新线程中执行主逻辑
            Task.Run(Sub()
                         ' 计算进度
                         Dim progressText As String = "N/A"
                         Dim progress As Double = 0
                         If 获取_总时长.TotalSeconds > 0 AndAlso 实时_time.TotalSeconds > 0 Then
                             progress = Math.Min(实时_time.TotalSeconds / 获取_总时长.TotalSeconds, 1.0)
                             progressText = $"{progress * 100:F1}%"
                             If progress = 0 Then progressText = "N/A"
                         End If

                         ' 计算速度百分比
                         Dim speedPercent As String = "N/A"
                         If Not String.IsNullOrWhiteSpace(实时_speed) Then
                             Dim speedValue As Double
                             If Double.TryParse(实时_speed.Replace("x", "").Trim(), Globalization.NumberStyles.Any, Globalization.CultureInfo.InvariantCulture, speedValue) AndAlso speedValue > 0 Then
                                 speedPercent = $"{speedValue * 100:F0}%"
                             End If
                         End If
                         If progress = 1.0 AndAlso speedPercent = "N/A" Then
                             speedPercent = "" ' 直接去掉回填
                         End If

                         ' 计算文件大小
                         Dim sizeText As String = "N/A"
                         If Not String.IsNullOrWhiteSpace(实时_size) Then
                             Dim sizeValue As Long
                             If Long.TryParse(实时_size, sizeValue) AndAlso sizeValue > 0 Then
                                 If sizeValue >= 1024 * 1024 Then
                                     sizeText = $"{sizeValue / 1024.0 / 1024.0:F2} GB"
                                 ElseIf sizeValue >= 1024 Then
                                     sizeText = $"{sizeValue / 1024.0:F0} MB"
                                 Else
                                     sizeText = $"{sizeValue} KB"
                                 End If
                             ElseIf 实时_size <> "0" Then
                                 sizeText = 实时_size
                             End If
                         End If

                         ' 估算完成后的大小
                         Dim estimatedSizeText As String = ""
                         If progress > 0 AndAlso progress < 1 AndAlso Not String.IsNullOrWhiteSpace(实时_size) Then
                             Dim sizeValue As Long
                             If Long.TryParse(实时_size, sizeValue) AndAlso sizeValue > 0 Then
                                 Dim estimatedSize As Double = sizeValue / progress
                                 If estimatedSize >= 1024 * 1024 Then
                                     estimatedSizeText = $" - {estimatedSize / 1024.0 / 1024.0:F1} GB"
                                 ElseIf estimatedSize >= 1024 Then
                                     estimatedSizeText = $" - {estimatedSize / 1024.0:F0} MB"
                                 Else
                                     estimatedSizeText = $" - {estimatedSize:F0} KB"
                                 End If
                             End If
                         End If

                         ' 计算q
                         Dim qText As String = If(String.IsNullOrWhiteSpace(实时_q) OrElse 实时_q = "0", "N/A", $"{实时_q:F0}")

                         ' 计算码率
                         Dim bitrateText As String
                         If String.IsNullOrWhiteSpace(实时_bitrate) OrElse 实时_bitrate = "0" Then
                             bitrateText = "N/A"
                         Else
                             bitrateText = 实时_bitrate & " kbps"
                         End If
                         If progress = 1.0 AndAlso bitrateText = "N/A" Then
                             bitrateText = "" ' 直接去掉回填
                         End If

                         ' 计算剩余时间
                         Dim remainTimeText As String = "N/A"
                         If 获取_总时长.TotalSeconds > 0 AndAlso 实时_time.TotalSeconds > 0 AndAlso Not String.IsNullOrWhiteSpace(实时_speed) Then
                             Dim speedValue As Double
                             If Double.TryParse(实时_speed.Replace("x", "").Trim(), Globalization.NumberStyles.Any, Globalization.CultureInfo.InvariantCulture, speedValue) AndAlso speedValue > 0 Then
                                 Dim remainSeconds = Math.Max(获取_总时长.TotalSeconds - 实时_time.TotalSeconds, 0) / speedValue
                                 If remainSeconds > 0 Then
                                     Dim hours = CInt(remainSeconds \ 3600)
                                     Dim minutes = CInt((remainSeconds Mod 3600) \ 60)
                                     Dim seconds = CInt(remainSeconds Mod 60)
                                     Dim parts As New List(Of String)
                                     If hours > 0 Then parts.Add($"{hours}h")
                                     If minutes > 0 OrElse hours > 0 Then parts.Add($"{minutes}m")
                                     parts.Add($"{seconds}s")
                                     remainTimeText = String.Join("", parts)
                                 End If
                             End If
                         End If

                         ' 通过Invoke刷新界面
                         Try
                             Form1.重新创建句柄()
                             Application.DoEvents()
                             Form1.Invoke(Sub()
                                              列表视图项.SubItems(1).Text = 状态.ToString
                                              列表视图项.SubItems(2).Text = progressText
                                              列表视图项.SubItems(3).Text = speedPercent
                                              列表视图项.SubItems(4).Text = sizeText & estimatedSizeText
                                              列表视图项.SubItems(5).Text = qText
                                              列表视图项.SubItems(6).Text = bitrateText
                                              Dim elapsed = 计时器.Elapsed
                                              Dim elapsedParts As New List(Of String)
                                              If elapsed.Hours > 0 Then elapsedParts.Add($"{elapsed.Hours}h")
                                              If elapsed.Minutes > 0 OrElse elapsedParts.Count > 0 Then elapsedParts.Add($"{elapsed.Minutes}m")
                                              elapsedParts.Add($"{elapsed.Seconds}s")
                                              Dim elapsedText = String.Join("", elapsedParts)
                                              列表视图项.SubItems(7).Text = $"{remainTimeText} - {elapsedText}"
                                          End Sub)
                         Catch ex As Exception
                             错误列表.Add($"刷新界面失败 {Now}")
                         End Try

                     End Sub)
        End Sub

        Public Sub 在实时输出中提取数据(line As String)
            Dim frameStr = ExtractRegexValueAsString(FramePattern, line)
            If Not String.IsNullOrEmpty(frameStr) Then 实时_frame = frameStr

            Dim fpsStr = ExtractRegexValueAsString(FpsPattern, line)
            If Not String.IsNullOrEmpty(fpsStr) Then 实时_fps = fpsStr

            Dim qStr = ExtractRegexValueAsString(QPattern, line)
            If Not String.IsNullOrEmpty(qStr) Then 实时_q = qStr

            Dim sizeMatch = SizePattern.Match(line)
            If sizeMatch.Success Then
                Dim valueStr = sizeMatch.Groups("value").Value
                Dim unit = sizeMatch.Groups("unit").Value
                Dim sizeValue As Long
                If Long.TryParse(valueStr, sizeValue) Then
                    实时_size = ConvertToKB(sizeValue, unit).ToString()
                Else
                    实时_size = valueStr
                End If
            End If

            Dim timeStr = ExtractRegexValueAsString(TimePattern, line)
            If Not String.IsNullOrEmpty(timeStr) Then
                实时_time = ParseTimeSpan(timeStr)
            End If

            Dim bitrateStr = ExtractRegexValueAsString(BitratePattern, line)
            If Not String.IsNullOrEmpty(bitrateStr) Then 实时_bitrate = bitrateStr

            Dim speedStr = ExtractRegexValueAsString(SpeedPattern, line)
            If Not String.IsNullOrEmpty(speedStr) Then 实时_speed = speedStr
        End Sub

        Public Sub 根据状态设置项信息显示()
            If 列表视图项 Is Nothing Then Exit Sub
            Select Case 状态
                Case 编码状态.未处理
                    列表视图项.ForeColor = Color.Silver
                    列表视图项.SubItems(1).Text = "未处理"
                Case 编码状态.正在处理
                    列表视图项.ForeColor = Color.YellowGreen
                    列表视图项.SubItems(1).Text = "正在处理"
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
                    Dim elapsed = 计时器.Elapsed
                    Dim elapsedParts As New List(Of String)
                    If elapsed.Hours > 0 Then elapsedParts.Add($"{elapsed.Hours} 时")
                    If elapsed.Minutes > 0 OrElse elapsedParts.Count > 0 Then elapsedParts.Add($"{elapsed.Minutes} 分")
                    elapsedParts.Add($"{elapsed.Seconds} 秒")
                    列表视图项.SubItems(7).Text = "耗时 " & String.Join(" ", elapsedParts)
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
                    列表视图项.ForeColor = Color.OrangeRed
                    列表视图项.SubItems(1).Text = "错误"
                    列表视图项.SubItems(5).Text = ""
                    列表视图项.SubItems(7).Text = ""
            End Select
        End Sub

    End Class

    Enum 编码状态
        未处理 = 0
        正在处理 = 1
        已暂停 = 2
        已完成 = 10
        已停止 = 20
        错误 = 99
    End Enum

    Public Shared ReadOnly DurationPattern As New Regex("Duration:\s*(\d{2}:\d{2}:\d{2}\.\d{2})")

    Public Shared ReadOnly FramePattern As New Regex("frame=\s*(?<value>\d+)", RegexOptions.Compiled)
    Public Shared ReadOnly FpsPattern As New Regex("fps=\s*(?<value>\d+)", RegexOptions.Compiled)
    Public Shared ReadOnly QPattern As New Regex("q=(?<value>[\d\.]+)", RegexOptions.Compiled)
    Public Shared ReadOnly SizePattern As New Regex("size=\s*(?<value>\d+)\s*(?<unit>[KMG]iB)", RegexOptions.Compiled)
    Public Shared ReadOnly TimePattern As New Regex("time=(?<value>\d{2}:\d{2}:\d{2}\.\d{2})", RegexOptions.Compiled)
    Public Shared ReadOnly BitratePattern As New Regex("bitrate=(?<value>[\d\.]+)\s*kbits/s", RegexOptions.Compiled)
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
        Dim 输出文件名 As String = IO.Path.GetFileNameWithoutExtension(输入文件) & $"_{Now.Year}.{Now.Month}.{Now.Day}-{Now.Hour}.{Now.Minute}.{Now.Second}" & 容器
        Return IO.Path.Combine(输出目录, 输出文件名)
    End Function

    Shared Function 计算输出位置_自定义目录(自定义目录 As String, 输入文件 As String, 容器 As String) As String
        Dim 输出目录 As String = 自定义目录
        If Not 容器.StartsWith("."c) Then
            容器 = "." & 容器
        End If
        Dim 输出文件名 As String = IO.Path.GetFileNameWithoutExtension(输入文件) & $"_{Now.Year}.{Now.Month}.{Now.Day}-{Now.Hour}.{Now.Minute}.{Now.Second}" & 容器
        Return IO.Path.Combine(输出目录, 输出文件名)
    End Function




End Class
