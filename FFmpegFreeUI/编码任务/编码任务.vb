Imports System.IO
Imports System.Text.RegularExpressions
Imports Sunny.UI

Public Class 编码任务

    Public Shared Sub 初始化()
        AddHandler 检查并开始新任务的定时器.Tick, AddressOf 检查并开始新任务的定时器_处理过程
    End Sub

    Public Shared Property 错误输出匹配字符串列表 As New List(Of String) From {"Error", "Invalid", "cannot", "failed", "not supported", "require", "must be", "Could not", "is experimental", "if you want to use it", "Nothing was written"}

    Enum 编码状态
        未处理 = 0
        正在处理 = 1
        已暂停 = 2
        已完成 = 10
        已停止 = 20
        错误 = 99
    End Enum

    Public Shared Property 队列 As New List(Of 单片任务)

    Public Shared Property 检查并开始新任务的定时器 As New Timer With {.Interval = 100, .Enabled = False}
    Public Shared Sub 检查并开始新任务的定时器_处理过程()
        If 用户设置.实例对象.自动开始任务选项 <> 0 Then Exit Sub
        Dim 当前正在运行的任务数量 As Integer = 队列.Where(Function(item) item.状态 = 编码状态.正在处理).Count()

        If 当前正在运行的任务数量 >= 同时运行任务上限 Then 检查并开始新任务的定时器.Enabled = False : Exit Sub
        For Each item As 单片任务 In 队列
            If item.状态 = 编码状态.未处理 Then
                Task.Run(AddressOf item.开始)
                当前正在运行的任务数量 += 1
                If 当前正在运行的任务数量 >= 同时运行任务上限 Then 检查并开始新任务的定时器.Enabled = False : Exit Sub
            End If
        Next

        If 当前正在运行的任务数量 = 0 Then
            If 全部任务已完成是否有错误 Then
                If 用户设置.实例对象.提示音选项 = 0 Then
                    Sound_Error.Position = 0
                    My.Computer.Audio.Play(Sound_Error, AudioPlayMode.Background)
                End If
                全部任务已完成是否有错误 = False
            Else
                If 用户设置.实例对象.提示音选项 = 0 Then
                    Sound_Finish.Position = 0
                    My.Computer.Audio.Play(Sound_Finish, AudioPlayMode.Background)
                End If
            End If
        End If

        检查并开始新任务的定时器.Enabled = False
    End Sub

    Public Shared Property 全部任务已完成是否有错误 As Boolean = False

    Public Shared Property 要刷新的项 As New Dictionary(Of ListViewItem, 刷新到界面数据结构)
    Public Shared Property 要刷新的项副本 As Dictionary(Of ListViewItem, 刷新到界面数据结构)
    Public Class 刷新到界面数据结构
        Public Property 进度 As String = ""
        Public Property 效率 As String = ""
        Public Property 输出大小 As String = ""
        Public Property 质量 As String = ""
        Public Property 比特率 As String = ""
        Public Property 时间 As String = ""
    End Class
    Public Shared Sub 用定时器刷新到界面上()
        If 队列.Count = 0 Then Exit Sub
        If 要刷新的项.Count = 0 Then Exit Sub
        SyncLock 要刷新的项
            要刷新的项副本 = New Dictionary(Of ListViewItem, 刷新到界面数据结构)(要刷新的项)
            要刷新的项.Clear()
        End SyncLock
        For Each item As ListViewItem In 要刷新的项副本.Keys
            item.SubItems(2).Text = 要刷新的项副本(item).进度
            item.SubItems(3).Text = 要刷新的项副本(item).效率
            item.SubItems(4).Text = 要刷新的项副本(item).输出大小
            item.SubItems(5).Text = 要刷新的项副本(item).质量
            item.SubItems(6).Text = 要刷新的项副本(item).比特率
            item.SubItems(7).Text = 要刷新的项副本(item).时间
        Next
    End Sub

    Public Class 单片任务
        Public Property 预设数据 As 预设数据类型
        Public Property 输入文件 As String = ""
        Public Property 输入文件的大小 As Long = 0
        Public Property 输出文件 As String = ""
        Public Property 自定义输出位置 As String = ""
        Public Property 命令行 As String = ""
        Public Property 获取_总时长 As TimeSpan = TimeSpan.Zero
        Public Property 已获取到总时长 As Boolean = False
        Public Property 实时_frame As String = ""
        Public Property 实时_fps As String = ""
        Public Property 实时_q As String = ""
        Public Property 实时_size As String = ""
        Public Property 实时_size_数字 As Long = 0
        Public Property 实时_time As TimeSpan = TimeSpan.Zero
        Public Property 实时_bitrate As String = ""
        Public Property 实时_speed As String = ""
        Public Property 状态 As 编码状态 = 编码状态.未处理

        Public Property 列表视图项 As ListViewItem = Nothing

        Public Property 实时输出 As String = ""
        Public Property 错误列表 As New List(Of String)
        Public Property 非进度输出列表 As New List(Of String)
        Public Property 手动停止不要尝试启动其他任务 As Boolean = False
        Public Property FFmpegProcess As Process
        Public Property 任务耗时计时器 As New Stopwatch
        Public Property 上次刷新界面的时间戳 As TimeSpan = Now.TimeOfDay

        Public Property AviSynthCachePath As String = ""
        Public Property VapourSynthCachePath As String = ""


        Public Sub 开始()
            状态 = 编码状态.正在处理
            Try
                错误列表.Clear()
                非进度输出列表.Clear()
                If 预设数据 Is Nothing Then GoTo jx1

                If 预设数据.视频参数_视频帧服务器_使用AviSynth Then
                    If FileIO.FileSystem.FileExists(预设数据.视频参数_视频帧服务器_avs脚本文件) Then
                        Dim avs1 As String = File.ReadAllText(预设数据.视频参数_视频帧服务器_avs脚本文件)
                        avs1 = avs1.Replace("<FilePath>", 输入文件)
                        AviSynthCachePath = Path.Combine(Path.GetDirectoryName(输入文件), Path.GetFileNameWithoutExtension(输入文件) & ".avs")
                        File.WriteAllText(AviSynthCachePath, avs1, New Text.UTF8Encoding(False))
                    Else
                        Err.Raise(10001, "", "AviSynth 脚本模板文件不存在！")
                    End If
                End If
                If 预设数据.视频参数_视频帧服务器_使用VapourSynth Then
                    If FileIO.FileSystem.FileExists(预设数据.视频参数_视频帧服务器_vpy脚本文件) Then
                        Dim vpy1 As String = File.ReadAllText(预设数据.视频参数_视频帧服务器_vpy脚本文件)
                        vpy1 = vpy1.Replace("<FilePath>", 输入文件)
                        VapourSynthCachePath = Path.Combine(Path.GetDirectoryName(输入文件), Path.GetFileNameWithoutExtension(输入文件) & Path.GetExtension(预设数据.视频参数_视频帧服务器_vpy脚本文件))
                        File.WriteAllText(AviSynthCachePath, vpy1, New Text.UTF8Encoding(False))
                    Else
                        Err.Raise(10001, "", "VapourSynth 脚本模板文件不存在！")
                    End If
                End If

                输出文件 = 计算输出位置(输入文件, 预设数据.输出容器, 预设数据, 自定义输出位置)
                命令行 = 预设管理.将预设数据转换为命令行(预设数据, 输入文件, 输出文件)

                If 预设数据.剪辑区间_入点 <> "" AndAlso 预设数据.剪辑区间_出点 <> "" Then
                    Dim t1 = 将时间字符串转换为时间类型(预设数据.剪辑区间_入点)
                    Dim t2 = 将时间字符串转换为时间类型(预设数据.剪辑区间_出点)
                    获取_总时长 = t2 - t1
                    已获取到总时长 = True
                    GoTo 结束剪辑区间计算
                End If
                If 预设数据.剪辑区间_入点 = "" AndAlso 预设数据.剪辑区间_出点 <> "" Then
                    获取_总时长 = 将时间字符串转换为时间类型(预设数据.剪辑区间_出点)
                    已获取到总时长 = True
                    GoTo 结束剪辑区间计算
                End If

结束剪辑区间计算:
jx1:
                If FileIO.FileSystem.FileExists(输入文件) Then
                    Try
                        输入文件的大小 = FileIO.FileSystem.GetFileInfo(输入文件).Length
                    Catch ex As Exception
                    End Try
                End If
                FFmpegProcess = New Process()
                FFmpegProcess.StartInfo.FileName = If(用户设置.实例对象.替代进程文件名 <> "", 用户设置.实例对象.替代进程文件名, "ffmpeg")
                FFmpegProcess.StartInfo.WorkingDirectory = If(用户设置.实例对象.工作目录 <> "", 用户设置.实例对象.工作目录, "")
                FFmpegProcess.StartInfo.Arguments = If(用户设置.实例对象.覆盖参数传递 <> "", 用户设置.实例对象.覆盖参数传递.Replace("<args>", 命令行), 命令行)
                FFmpegProcess.StartInfo.UseShellExecute = False
                FFmpegProcess.StartInfo.RedirectStandardOutput = True
                FFmpegProcess.StartInfo.RedirectStandardError = True
                FFmpegProcess.StartInfo.StandardOutputEncoding = Text.Encoding.UTF8
                FFmpegProcess.StartInfo.StandardErrorEncoding = Text.Encoding.UTF8
                FFmpegProcess.StartInfo.RedirectStandardInput = True
                FFmpegProcess.StartInfo.StandardInputEncoding = Text.Encoding.UTF8
                FFmpegProcess.StartInfo.CreateNoWindow = True
                FFmpegProcess.EnableRaisingEvents = True
                AddHandler FFmpegProcess.OutputDataReceived, AddressOf FFmpegOutputHandler
                AddHandler FFmpegProcess.ErrorDataReceived, AddressOf FFmpegOutputHandler
                AddHandler FFmpegProcess.Exited, AddressOf FFmpegProcessExited

                FFmpegProcess.Start()
                FFmpegProcess.BeginOutputReadLine()
                FFmpegProcess.BeginErrorReadLine()

                If 用户设置.实例对象.指定处理器核心 <> "" Then FFmpegProcess.ProcessorAffinity = GetAffinityMask(用户设置.实例对象.指定处理器核心.Split(","c).Select(Function(s) s.Trim()).Where(Function(s) Integer.TryParse(s, Nothing)).Select(Function(s) Integer.Parse(s)).ToArray())

                设定系统状态()
                任务耗时计时器.Start()

            Catch ex As Exception
                实时输出 = $"[3FUI] {ex.Message}"
                错误列表.Add($"[3FUI] {ex.Message}")
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
                        设定系统状态()
                    End If
                End If
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical)
            End Try
        End Sub

        Public Sub 停止()
            Try
                If FFmpegProcess.HasExited = False Then
                    手动停止不要尝试启动其他任务 = True
                    FFmpegProcess?.Kill()
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

        Public Sub 发送消息(message As String)
            Try
                If FFmpegProcess IsNot Nothing AndAlso Not FFmpegProcess.HasExited Then
                    FFmpegProcess.StandardInput.WriteLine(message)
                    FFmpegProcess.StandardInput.Flush()
                End If
            Catch ex As Exception
                错误列表.Add($"发送消息失败: {ex.Message}")
            End Try
        End Sub



        Public Sub FFmpegOutputHandler(sender As Object, e As DataReceivedEventArgs)
            If e.Data Is Nothing Then Exit Sub
            实时输出 = e.Data

            If Not 已获取到总时长 AndAlso e.Data.Contains("Duration") Then
                Dim durationMatch = DurationPattern.Match(e.Data)
                If durationMatch.Success AndAlso 预设数据 IsNot Nothing Then
                    If 预设数据.剪辑区间_入点 <> "" AndAlso 预设数据.剪辑区间_出点 = "" Then
                        获取_总时长 = 将时间字符串转换为时间类型(durationMatch.Groups(1).Value) - 将时间字符串转换为时间类型(预设数据.剪辑区间_入点)
                    Else
                        获取_总时长 = 将时间字符串转换为时间类型(durationMatch.Groups(1).Value)
                    End If
                    已获取到总时长 = True
                End If
            End If

            If e.Data.Contains("="c) Then
                If (Now.TimeOfDay - 上次刷新界面的时间戳).TotalSeconds >= 1 Then
                    在实时输出中提取数据(e.Data)
                    处理捕获的数据并添加到刷新队列()
                    上次刷新界面的时间戳 = Now.TimeOfDay
                End If
            Else
                非进度输出列表.Add(e.Data)
                If 非进度输出列表.Count > 1000 Then
                    非进度输出列表.RemoveRange(0, 非进度输出列表.Count - 100)
                End If
            End If

            If 错误输出匹配字符串列表.Any(Function(keyword) e.Data.Contains(keyword, StringComparison.OrdinalIgnoreCase)) Then
                错误列表.Add(e.Data)
            End If

            界面线程执行(AddressOf 状态刷新统一逻辑)
        End Sub

        Public Sub FFmpegProcessExited(sender As Object, e As EventArgs)
            SyncLock 要刷新的项
                If 要刷新的项.ContainsKey(列表视图项) Then 要刷新的项.Remove(列表视图项)
            End SyncLock

            If FFmpegProcess.ExitCode = 0 Then
                状态 = 编码状态.已完成

                If 预设数据 IsNot Nothing Then
                    If 预设数据.视频参数_视频帧服务器_使用AviSynth Then
                        If FileIO.FileSystem.FileExists(AviSynthCachePath) Then FileIO.FileSystem.DeleteFile(AviSynthCachePath)
                    End If
                    If 预设数据.视频参数_视频帧服务器_使用VapourSynth Then
                        If FileIO.FileSystem.FileExists(VapourSynthCachePath) Then FileIO.FileSystem.DeleteFile(VapourSynthCachePath)
                    End If
                End If

                Dim 合并任务缓存 = Path.Combine(Application.StartupPath, "ffmpeg_concat_demuxer.txt")
                If FileIO.FileSystem.FileExists(合并任务缓存) Then
                    FileIO.FileSystem.DeleteFile(合并任务缓存)
                End If

                If FileIO.FileSystem.FileExists(输出文件) AndAlso FileIO.FileSystem.FileExists(输入文件) Then
                    If 预设数据.输出命名_保留创建时间 Then File.SetCreationTime(输出文件, File.GetCreationTime(输入文件))
                    If 预设数据.输出命名_保留修改时间 Then File.SetLastWriteTime(输出文件, File.GetLastWriteTime(输入文件))
                    If 预设数据.输出命名_保留访问时间 Then File.SetLastAccessTime(输出文件, File.GetLastAccessTime(输入文件))
                End If

            Else

                全部任务已完成是否有错误 = True
                If Not 手动停止不要尝试启动其他任务 Then 状态 = 编码状态.错误
                If FileIO.FileSystem.FileExists(输出文件) Then
                    Select Case 用户设置.实例对象.任务失败自动删除输出文件
                        Case 0
                            If Path.GetExtension(输出文件).ToLower.Trim = ".mp4" Then
                                If 输出文件.Trim.Equals(输入文件.Trim, StringComparison.CurrentCultureIgnoreCase) Then
                                    实时输出 = $"[3FUI] 你在干什么？！输出文件等于输入文件？"
                                    错误列表.Add($"[3FUI] 你在干什么？！输出文件等于输入文件？")
                                Else
                                    FileIO.FileSystem.DeleteFile(输出文件, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.SendToRecycleBin)
                                End If
                            End If
                        Case 1
                            If 输出文件.Trim.Equals(输入文件.Trim, StringComparison.CurrentCultureIgnoreCase) Then
                                实时输出 = $"[3FUI] 你在干什么？！输出文件等于输入文件？"
                                错误列表.Add($"[3FUI] 你在干什么？！输出文件等于输入文件？")
                            Else
                                FileIO.FileSystem.DeleteFile(输出文件, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.SendToRecycleBin)
                            End If
                        Case 2
                    End Select
                End If

            End If
            任务耗时计时器.Stop()

            界面线程执行(AddressOf 状态刷新统一逻辑)
        End Sub
        Public Sub 状态刷新统一逻辑()
            If 列表视图项 Is Nothing Then Exit Sub
            Select Case 状态
                Case 编码状态.未处理
                    列表视图项.ForeColor = Color.Silver
                    列表视图项.SubItems(1).Text = "未处理"
                    列表视图项.SubItems(4).ForeColor = 列表视图项.ForeColor
                Case 编码状态.正在处理
                    列表视图项.ForeColor = Color.YellowGreen
                    列表视图项.SubItems(1).Text = "正在处理"
                    If 输入文件的大小 > 0 AndAlso 实时_size_数字 > 0 Then
                        Dim 实时大小比值 = 实时_size_数字 / (输入文件的大小 / 1024)
                        Select Case 实时大小比值
                            Case < 1 : 列表视图项.SubItems(4).ForeColor = 列表视图项.ForeColor
                            Case >= 1 : 列表视图项.SubItems(4).ForeColor = Color.IndianRed
                        End Select
                    End If
                Case 编码状态.已完成
                    列表视图项.ForeColor = Color.OliveDrab
                    列表视图项.SubItems(1).Text = "已完成"
                    列表视图项.SubItems(2).Text = "100%"
                    If FileIO.FileSystem.FileExists(输入文件) And FileIO.FileSystem.FileExists(输出文件) Then
                        Dim 输出文件的大小 As Long = FileIO.FileSystem.GetFileInfo(输出文件).Length
                        Dim sizeValue As Long = 输出文件的大小 / 1024
                        Dim sizeText As String
                        If sizeValue >= 1024 * 1024 Then
                            sizeText = $"{sizeValue / 1024.0 / 1024.0:F2} GB"
                        ElseIf sizeValue >= 1024 Then
                            sizeText = $"{sizeValue / 1024.0:F0} MB"
                        Else
                            sizeText = $"{sizeValue} KB"
                        End If
                        Dim 前后大小比值 = 输出文件的大小 / 输入文件的大小
                        列表视图项.SubItems(4).Text = $"{sizeText} ({$"{前后大小比值 * 100:F0}%"})"
                        Select Case 前后大小比值
                            Case >= 1 : 列表视图项.SubItems(4).ForeColor = Color.IndianRed
                            Case < 1 : 列表视图项.SubItems(4).ForeColor = 列表视图项.ForeColor
                        End Select
                        GoTo 结束前后文件大小显示计算
                    End If
                    If Not FileIO.FileSystem.FileExists(输入文件) And FileIO.FileSystem.FileExists(输出文件) Then
                        Dim 输出文件的大小 As Long = FileIO.FileSystem.GetFileInfo(输出文件).Length
                        Dim sizeValue As Long = 输出文件的大小 / 1024
                        Dim sizeText As String
                        If sizeValue >= 1024 * 1024 Then
                            sizeText = $"{sizeValue / 1024.0 / 1024.0:F2} GB"
                        ElseIf sizeValue >= 1024 Then
                            sizeText = $"{sizeValue / 1024.0:F0} MB"
                        Else
                            sizeText = $"{sizeValue} KB"
                        End If
                        列表视图项.SubItems(4).Text = $"{sizeText}"
                        GoTo 结束前后文件大小显示计算
                    End If
                    If Not FileIO.FileSystem.FileExists(输出文件) Then
                        列表视图项.SubItems(4).Text = $"N/A"
                        GoTo 结束前后文件大小显示计算
                    End If
结束前后文件大小显示计算:
                    列表视图项.SubItems(5).Text = ""
                    Dim elapsed = 任务耗时计时器.Elapsed
                    Dim elapsedParts As New List(Of String)
                    If elapsed.Hours > 0 Then elapsedParts.Add($"{elapsed.Hours} 时")
                    If elapsed.Minutes > 0 OrElse elapsedParts.Count > 0 Then elapsedParts.Add($"{elapsed.Minutes} 分")
                    elapsedParts.Add($"{elapsed.Seconds} 秒")
                    列表视图项.SubItems(7).Text = "耗时 " & String.Join(" ", elapsedParts)
                    If Not 手动停止不要尝试启动其他任务 Then 检查并开始新任务的定时器.Enabled = True
                Case 编码状态.已暂停
                    列表视图项.ForeColor = Color.Goldenrod
                    列表视图项.SubItems(1).Text = "已暂停"
                    If 列表视图项.SubItems(4).ForeColor <> Color.IndianRed Then 列表视图项.SubItems(4).ForeColor = 列表视图项.ForeColor
                Case 编码状态.已停止
                    列表视图项.ForeColor = Color.IndianRed
                    列表视图项.SubItems(1).Text = "已停止"
                    列表视图项.SubItems(5).Text = ""
                    列表视图项.SubItems(4).ForeColor = 列表视图项.ForeColor
                Case 编码状态.错误
                    列表视图项.SubItems(4).ForeColor = 列表视图项.ForeColor
                    If 手动停止不要尝试启动其他任务 Then
                        列表视图项.ForeColor = Color.IndianRed
                        列表视图项.SubItems(5).Text = ""
                    Else
                        列表视图项.ForeColor = Color.IndianRed
                        列表视图项.SubItems(1).Text = "错误"
                        检查并开始新任务的定时器.Enabled = True
                    End If
            End Select
        End Sub
        Public Sub 处理捕获的数据并添加到刷新队列()
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

            Dim 信息数据 As New 刷新到界面数据结构 With {
                .进度 = progressText,
                .效率 = speedPercent,
                .输出大小 = sizeText & estimatedSize,
                .质量 = qText,
                .比特率 = bitrateText
            }
            Dim el = 任务耗时计时器.Elapsed, elapsedParts = New List(Of String)
            If el.Hours > 0 Then elapsedParts.Add($"{el.Hours}h")
            If el.Minutes > 0 OrElse elapsedParts.Count > 0 Then elapsedParts.Add($"{el.Minutes}m")
            elapsedParts.Add($"{el.Seconds}s")
            信息数据.时间 = $"{remainTime} - {String.Join("", elapsedParts)}"

            SyncLock 要刷新的项
                要刷新的项(列表视图项) = 信息数据
            End SyncLock
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
                实时_size_数字 = sz
            End If
            Dim t = ExtractRegexValueAsString(TimePattern, line) : If t <> "" Then 实时_time = 将时间字符串转换为时间类型(t)
            Dim br = ExtractRegexValueAsString(BitratePattern, line) : If br <> "" Then 实时_bitrate = br
            Dim sp = ExtractRegexValueAsString(SpeedPattern, line) : If sp <> "" Then 实时_speed = sp
        End Sub

    End Class


    Public Shared ReadOnly DurationPattern As New Regex("Duration:\s*(\d+:\d{2}:\d{2}\.\d{2})", RegexOptions.Compiled)
    Public Shared ReadOnly FramePattern As New Regex("frame=\s*(?<value>\d+)", RegexOptions.Compiled)
    Public Shared ReadOnly FpsPattern As New Regex("fps=\s*(?<value>\d+)", RegexOptions.Compiled)
    Public Shared ReadOnly QPattern As New Regex("q=\s*(?<value>[\d\.]+)", RegexOptions.Compiled)
    Public Shared ReadOnly SizePattern As New Regex("size=\s*(?<value>\d+)\s*(?<unit>[KMG]iB)", RegexOptions.Compiled)
    Public Shared ReadOnly TimePattern As New Regex("time=\s*(?<value>\d+:\d{2}:\d{2}\.\d{2})", RegexOptions.Compiled)
    Public Shared ReadOnly BitratePattern As New Regex("bitrate=\s*(?<value>[\d\.]+)\s*kbits/s", RegexOptions.Compiled)
    Public Shared ReadOnly SpeedPattern As New Regex("speed=\s*(?<value>[\d\.eE\+\-]+)\s*x", RegexOptions.Compiled)

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

    Shared Function 计算输出位置(输入文件 As String, 容器 As String, 预设数据 As 预设数据类型, 自定义目录 As String) As String
        If 预设数据.输出命名_不使用输出文件参数 Then
            Return ""
            Exit Function
        End If
        Dim 输出目录 As String
        If 自定义目录 = "" Then
            输出目录 = Path.GetDirectoryName(输入文件)
        Else
            输出目录 = 自定义目录
        End If
        If Not 容器.StartsWith("."c) Then 容器 = "." & 容器
        Dim 文件名不带后缀 As String = Path.GetFileNameWithoutExtension(输入文件)
        Dim 文件名 As String = 预设数据.输出命名_开头文本
        If 预设数据.输出命名_替代文本 = "" Then
            文件名 &= 文件名不带后缀
        Else
            文件名 &= 预设数据.输出命名_替代文本
        End If
        文件名 &= 预设数据.输出命名_结尾文本
        If 预设数据.输出命名_使用自动命名 Then
            Select Case 预设数据.输出命名_自动命名选项
                Case 0 : 文件名 &= $"_{Now:yyyy.MM.dd-HH.mm.ss}"
                Case 1 : 文件名 &= $"~1"
                Case 2 : 文件名 &= $"_3fui"
                Case 3
                    If 预设数据.视频参数_编码器_具体编码 <> "" Then 文件名 &= $".{预设数据.视频参数_编码器_具体编码}"
                    If 预设数据.视频参数_编码器_编码预设 <> "" Then 文件名 &= $".{预设数据.视频参数_编码器_编码预设}"
                    If 预设数据.视频参数_质量控制_参数名 <> "" AndAlso 预设数据.视频参数_质量控制_值 <> "" Then
                        文件名 &= $".{预设数据.视频参数_质量控制_参数名.Replace("-", "")}"
                        文件名 &= $"{预设数据.视频参数_质量控制_值}"
                    End If
                    If 预设数据.视频参数_比特率_基础 <> "" Then 文件名 &= $".{预设数据.视频参数_比特率_基础}"
                    If 预设数据.视频参数_比特率_最低值 <> "" Then 文件名 &= $".L{预设数据.视频参数_比特率_最低值}"
                    If 预设数据.视频参数_比特率_最高值 <> "" Then 文件名 &= $".H{预设数据.视频参数_比特率_最高值}"
                    If 预设数据.视频参数_比特率_缓冲区 <> "" Then 文件名 &= $".BF{预设数据.视频参数_比特率_缓冲区}"
                Case 4 : 文件名 &= $"_{随机字符串生成(8, True, False, False)}"
                Case 5 : 文件名 &= $"_{随机字符串生成(16, True, False, False)}"
                Case 6 : 文件名 &= $"_{随机字符串生成(8, False, False, True)}"
                Case 7 : 文件名 &= $"_{随机字符串生成(16, False, False, True)}"
                Case 8 : 文件名 &= $"_{随机字符串生成(8, True, False, True)}"
                Case 9 : 文件名 &= $"_{随机字符串生成(16, True, False, True)}"
            End Select
        End If
        文件名 &= 容器
        Dim 输出路径 = Path.Combine(输出目录, 文件名)
        If 用户设置.实例对象.转译模式 Then
            Return 转译模式处理路径(输出路径)
        Else
            Return 输出路径
        End If
    End Function

    Public Shared Sub 选中项刷新信息()
        Try
            Dim errorCount As Integer = 队列(Form1.ListView1.SelectedItems(0).Index).错误列表.Count
            Select Case errorCount
                Case > 0
                    Form1.LinkLabel切换显示输出面板.Text = 翻译("LinkLabel.CatchErrors").Replace("{0}", errorCount)
                    Form1.LinkLabel切换显示输出面板.LinkColor = Color.IndianRed
                Case 0
                    Form1.LinkLabel切换显示输出面板.Text = 翻译("LinkLabel.SwitchOutputDisplay")
                    Form1.LinkLabel切换显示输出面板.LinkColor = Color.YellowGreen
            End Select

            Select Case Form1.UiComboBox输出显示类型.SelectedIndex
                Case 0
                    Dim pretext = String.Join(vbCrLf, 队列(Form1.ListView1.SelectedItems(0).Index).非进度输出列表)
                    If IsRichTextBoxTextDifferent(pretext, Form1.RichTextBox2) Then
                        Form1.RichTextBox2.Clear()
                        For Each s In 队列(Form1.ListView1.SelectedItems(0).Index).非进度输出列表
                            在富文本框输出(Form1.RichTextBox2, s)
                        Next
                    End If
                Case 1
                    Dim pretext = String.Join(vbCrLf, 队列(Form1.ListView1.SelectedItems(0).Index).错误列表)
                    If IsRichTextBoxTextDifferent(pretext, Form1.RichTextBox2) Then
                        Form1.RichTextBox2.Clear()
                        For Each s In 队列(Form1.ListView1.SelectedItems(0).Index).错误列表
                            在富文本框输出(Form1.RichTextBox2, s)
                        Next
                    End If
            End Select

            If Form1.UiCheckBox强制滚动到最后.Checked Then
                Module2.SendMessage(Form1.RichTextBox2.Handle, &H115, 7, 0)
            End If

            Form1.Labelffmpeg实时信息.Text = 队列(Form1.ListView1.SelectedItems(0).Index).实时输出
        Catch ex As Exception
            队列(Form1.ListView1.SelectedItems(0).Index).错误列表.Add($"刷新选中项实时信息时失败 {Now} {ex.Message}")
        Finally
            Select Case 队列(Form1.ListView1.SelectedItems(0).Index).状态
                Case 编码状态.已停止, 编码状态.已完成, 编码状态.已暂停, 编码状态.未处理, 编码状态.错误
                    Form1.选中项刷新信息计时器.Enabled = False
            End Select
        End Try
    End Sub

    Public Shared Sub 切换输出类型时单独刷新()
        Try
            If Form1.ListView1.SelectedItems.Count <> 1 Then Exit Sub
            Form1.RichTextBox2.Clear()
            Select Case Form1.UiComboBox输出显示类型.SelectedIndex
                Case 0
                    Dim pretext = String.Join(vbCrLf, 队列(Form1.ListView1.SelectedItems(0).Index).非进度输出列表)
                    If IsRichTextBoxTextDifferent(pretext, Form1.RichTextBox2) Then
                        For Each s In 队列(Form1.ListView1.SelectedItems(0).Index).非进度输出列表
                            在富文本框输出(Form1.RichTextBox2, s)
                        Next
                    End If
                Case 1
                    Dim pretext = String.Join(vbCrLf, 队列(Form1.ListView1.SelectedItems(0).Index).错误列表)
                    If IsRichTextBoxTextDifferent(pretext, Form1.RichTextBox2) Then
                        For Each s In 队列(Form1.ListView1.SelectedItems(0).Index).错误列表
                            在富文本框输出(Form1.RichTextBox2, s)
                        Next
                    End If
            End Select
        Catch ex As Exception
            队列(Form1.ListView1.SelectedItems(0).Index).错误列表.Add($"切换输出类型时单独刷新时失败 {Now} {ex.Message}")
        End Try
    End Sub
    Shared Function IsRichTextBoxTextDifferent(newText As String, richTextBox As RichTextBox) As Boolean
        Dim currentPlainText As String = richTextBox.Text
        Dim normalizedNewText As String = newText.Replace(vbCrLf, "").Replace(vbLf, "").Replace(vbCr, "")
        Dim normalizedCurrentText As String = currentPlainText.Replace(vbCrLf, "").Replace(vbLf, "").Replace(vbCr, "")
        Return Not String.Equals(normalizedNewText, normalizedCurrentText, StringComparison.CurrentCultureIgnoreCase)
    End Function

End Class
