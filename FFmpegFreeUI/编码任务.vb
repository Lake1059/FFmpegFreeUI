Imports System.Runtime.InteropServices
Imports System.Text.RegularExpressions

Public Class 编码任务
    Public Shared Property 队列 As New List(Of 单片任务)
    Public Shared 保留时间戳 As Boolean = False

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
                错误列表.Clear()
                状态 = 编码状态.正在处理
                If 自定义输出位置 = "" Then
                    输出文件 = 计算输出位置_原目录(输入文件, 预设数据.输出容器)
                Else
                    输出文件 = 计算输出位置_自定义目录(自定义输出位置, 输入文件, 预设数据.输出容器)
                End If
                命令行 = 预设管理.将预设数据转换为命令行(预设数据, 输入文件, 输出文件)
                FFmpegProcess = New Process()
                FFmpegProcess.StartInfo.FileName = "ffmpeg.exe"
                FFmpegProcess.StartInfo.WorkingDirectory = Application.StartupPath
                FFmpegProcess.StartInfo.Arguments = 命令行
                FFmpegProcess.StartInfo.UseShellExecute = False
                FFmpegProcess.StartInfo.RedirectStandardOutput = True
                FFmpegProcess.StartInfo.RedirectStandardError = True
                FFmpegProcess.StartInfo.CreateNoWindow = True
                FFmpegProcess.EnableRaisingEvents = True
                AddHandler FFmpegProcess.OutputDataReceived, AddressOf FFmpegOutputHandler
                AddHandler FFmpegProcess.ErrorDataReceived, AddressOf FFmpegOutputHandler
                AddHandler FFmpegProcess.Exited, AddressOf FFmpegProcessExited
                FFmpegProcess.Start()
                FFmpegProcess.BeginOutputReadLine()
                FFmpegProcess.BeginErrorReadLine()
                计时器.Start()
                根据状态设置项信息显示()
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

            End Try
        End Sub

        Public Sub 清除占用()
            FFmpegProcess?.Dispose()
            列表视图项?.Remove()
            计时器.Stop()
            GC.Collect()
        End Sub

        Public Sub FFmpegOutputHandler(sender As Object, e As DataReceivedEventArgs)
            If e.Data Is Nothing Then Exit Sub
            Dim line As String = e.Data
            If Not 已获取到总时长 Then
                Dim durationMatch = DurationPattern.Match(line)
                If durationMatch.Success Then
                    获取_总时长 = ParseTimeSpan(durationMatch.Groups(1).Value)
                    已获取到总时长 = True
                End If
            End If
            Form1.重新创建句柄()
            If 在实时输出中提取数据(line) Then
                实时输出 = line
            End If
            If line.Contains("Error") OrElse line.Contains("Invalid") OrElse line.Contains("cannot") OrElse line.Contains("failed") Then
                错误列表.Add(line)
                状态 = 编码状态.错误
            End If
            Form1.Invoke(AddressOf 在界面上刷新数据)
            Form1.Invoke(AddressOf 根据状态设置项信息显示)
        End Sub

        Public Sub FFmpegProcessExited(sender As Object, e As EventArgs)
            If FFmpegProcess.ExitCode = 0 Then
                状态 = 编码状态.已完成
                ' 保留时间戳功能
                If 编码任务.保留时间戳 Then
                    Try
                        If System.IO.File.Exists(输入文件) AndAlso System.IO.File.Exists(输出文件) Then
                            Dim srcInfo As New System.IO.FileInfo(输入文件)
                            System.IO.File.SetCreationTime(输出文件, srcInfo.CreationTime)
                            System.IO.File.SetLastWriteTime(输出文件, srcInfo.LastWriteTime)
                            System.IO.File.SetLastAccessTime(输出文件, srcInfo.LastAccessTime)
                        End If
                    Catch ex As Exception
                        MsgBox($"同步时间戳失败：{ex.Message}", MsgBoxStyle.Exclamation, "时间戳同步失败")
                    End Try
                End If
            Else
                状态 = 编码状态.错误
            End If
            计时器.Stop()
            Form1.重新创建句柄()
            Form1.Invoke(AddressOf 根据状态设置项信息显示)
            If Not 手动停止不要尝试启动其他任务 Then
                检查是否有可以开始的任务()
            Else
                手动停止不要尝试启动其他任务 = True
            End If
        End Sub

        Public Sub 在界面上刷新数据()
            If 列表视图项 Is Nothing Then Exit Sub
            列表视图项.SubItems(1).Text = 状态.ToString
            If 状态 <> 编码状态.正在处理 Then Exit Sub

            Dim progressText As String = "N/A"
            If 获取_总时长.TotalSeconds > 0 AndAlso 实时_time.TotalSeconds > 0 Then
                Dim progress = Math.Min(实时_time.TotalSeconds / 获取_总时长.TotalSeconds, 1.0)
                progressText = $"{progress * 100:F1}%"
                If progress = 0 Then progressText = "N/A"
            End If
            列表视图项.SubItems(2).Text = progressText

            Dim speedPercent As String = "N/A"
            If Not String.IsNullOrWhiteSpace(实时_speed) Then
                Dim speedValue As Double
                If Double.TryParse(实时_speed.Replace("x", "").Trim(), Globalization.NumberStyles.Any, Globalization.CultureInfo.InvariantCulture, speedValue) AndAlso speedValue > 0 Then
                    speedPercent = $"{speedValue * 100:F0}%"
                End If
            End If
            列表视图项.SubItems(3).Text = speedPercent

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
            列表视图项.SubItems(4).Text = sizeText

            列表视图项.SubItems(5).Text = If(String.IsNullOrWhiteSpace(实时_q) OrElse 实时_q = "0", "N/A", $"{实时_q:F0}")

            列表视图项.SubItems(6).Text = If(String.IsNullOrWhiteSpace(实时_bitrate) OrElse 实时_bitrate = "0", "N/A", 实时_bitrate & " kbps")

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
                        If hours > 0 Then parts.Add($"{hours} 小时")
                        If minutes > 0 OrElse hours > 0 Then parts.Add($"{minutes} 分钟")
                        parts.Add($"{seconds} 秒")
                        remainTimeText = String.Join(" ", parts)
                    End If
                End If
            End If
            列表视图项.SubItems(7).Text = remainTimeText
        End Sub

        Public Function 在实时输出中提取数据(line As String) As Boolean
            实时_frame = ExtractRegexValueAsString(FramePattern, line)

            ExtractRegexValueAsString(FramePattern, line)
            实时_fps = ExtractRegexValueAsString(FpsPattern, line)
            实时_q = ExtractRegexValueAsString(QPattern, line)

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
            Else
                实时_size = ""
            End If

            Dim timeStr = ExtractRegexValueAsString(TimePattern, line)
            If Not String.IsNullOrEmpty(timeStr) Then
                实时_time = ParseTimeSpan(timeStr)
            End If

            实时_bitrate = ExtractRegexValueAsString(BitratePattern, line)
            实时_speed = ExtractRegexValueAsString(SpeedPattern, line)
            If line.Contains("frame=") AndAlso line.Contains("speed=") Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Sub 根据状态设置项信息显示()
            If 列表视图项 Is Nothing Then Exit Sub
            Select Case 状态
                Case 编码状态.未处理
                    列表视图项.ForeColor = Color.Silver
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
                    列表视图项.SubItems(7).Text = "耗时 " & 计时器.Elapsed.ToString("hh\:mm\:ss")
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
        Dim parts = timeStr.Split(":"c, "."c)
        Try
            Dim hours = Integer.Parse(parts(0))
            Dim minutes = Integer.Parse(parts(1))
            Dim seconds = Integer.Parse(parts(2))
            Dim milliseconds = Integer.Parse(parts(3)) * 10 ' .34 → 340ms 
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
