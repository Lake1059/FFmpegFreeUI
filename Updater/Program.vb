Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Threading

Module Program

    Private Const 主程序文件名 As String = "FFmpegFreeUI.exe"
    Private Const 更新后缀 As String = "_update"
    Private Const 仓库拥有者 As String = "Lake1059"
    Private Const 仓库名称 As String = "FFmpegFreeUI"
    Private Const 控制台行宽 As Integer = 92
    Private Const 进度条宽度 As Integer = 28
    Private ReadOnly 架构主程序文件名列表 As String() = {
        "FFmpegFreeUI.x64.exe",
        "FFmpegFreeUI.arm64.exe"
    }

#Disable Warning IDE0060 ' 删除未使用的参数
    Function Main(args As String()) As Integer
#Enable Warning IDE0060 ' 删除未使用的参数
        Try
            Console.Title = "FFmpegFreeUI 更新器"
        Catch
        End Try
        Try
            Return 主流程().GetAwaiter().GetResult()
        Catch ex As Exception
            Console.WriteLine()
            Console.WriteLine($"更新器发生未处理异常：{ex.Message}")
            Console.WriteLine("按任意键退出 ...")
            尝试等待按键()
            Return 1
        End Try
    End Function

    Private Async Function 主流程() As Task(Of Integer)
        输出标题()

        Dim 程序目录 = Path.GetFullPath(AppContext.BaseDirectory)
        Dim 主程序路径 = Path.Combine(程序目录, 主程序文件名)
        Dim 本体更新文件路径 = 获取更新文件路径(程序目录, 主程序文件名)
        Dim 主程序已存在 = File.Exists(主程序路径)
        Dim 本体更新文件已存在 = File.Exists(本体更新文件路径)
        Dim 首次安装 As Boolean = Not 主程序已存在
        Dim 强制重启 As Boolean = False

        输出信息($"程序目录：{程序目录}")

        If 首次安装 Then
            强制重启 = True
            If 本体更新文件已存在 Then
                输出警告($"未发现 {主程序文件名}，但已发现 {Path.GetFileName(本体更新文件路径)}，将直接应用。")
                If Not Await 确保主程序运行环境(程序目录) Then
                    Console.WriteLine()
                    输出错误("运行环境未准备就绪，无法启动更新后的 FFmpegFreeUI。按任意键继续 ...")
                    尝试等待按键()
                    Return 2
                End If
            Else
                输出警告($"未发现 {主程序文件名} 或 {Path.GetFileName(本体更新文件路径)}，进入主程序下载流程。")
                输出信息($"下载目录：{程序目录}")
                Console.WriteLine()
                Dim 下载成功 = Await 下载主程序(程序目录)
                If Not 下载成功 Then
                    Console.WriteLine()
                    输出错误("下载未成功，更新器即将退出。按任意键继续 ...")
                    尝试等待按键()
                    Return 2
                End If
            End If
        End If

        If 主程序已存在 AndAlso Not 存在待应用更新文件(程序目录) Then
            Console.WriteLine()
            输出警告("没有发现待应用的更新文件。")
            输出信息("按任意键退出 ...")
            尝试等待按键()
            Return 0
        End If

        If 主程序已存在 Then
            Console.WriteLine()
            等待主程序退出(程序目录)
        End If

        Console.WriteLine()
        Dim 已应用 = 应用更新文件(程序目录)
        Console.WriteLine()

        If 已应用 = 0 Then
            If 强制重启 Then
                输出错误("未能应用任何本体更新文件，无法完成初次安装。")
                输出信息("按任意键退出 ...")
                尝试等待按键()
                Return 3
            End If
            输出警告("没有发现待应用的更新文件。")
            输出信息("按任意键退出 ...")
            尝试等待按键()
            Return 0
        End If

        输出成功($"共应用 {已应用} 个更新文件。")
        清理架构主程序文件(程序目录)
        Console.WriteLine()

        Dim 需要重启 As Boolean = 强制重启 OrElse 询问是否重启()
        If 需要重启 Then 启动主程序(程序目录)

        Return 0
    End Function

    '======================================================================
    ' 下载流程
    '======================================================================
    Private Async Function 下载主程序(程序目录 As String) As Task(Of Boolean)
        输出章节("下载主程序")
        If Not Await 确保主程序运行环境(程序目录) Then
            输出错误(".NET 10 Desktop Runtime 未准备就绪，无法运行下载后的 FFmpegFreeUI。")
            Return False
        End If

        输出信息("正在向 GitHub 查询最新发行版 ...")
        Dim info As LakeUI.GitHub.GitHubReleaseInfo
        Try
            info = Await LakeUI.GitHub.GetLatestReleaseAssetUrlsAsync(仓库拥有者, 仓库名称, True)
        Catch ex As Exception
            输出错误($"查询发行版失败：{ex.Message}")
            Return False
        End Try
        If info Is Nothing OrElse Not info.IsSuccess Then
            输出错误($"查询发行版失败：{If(info?.ErrorMessage, "未知错误")}")
            Return False
        End If
        输出成功($"最新版本：{info.TagName}")

        Dim 目标资源名 = $"FFmpegFreeUI.{获取架构名称()}.exe"
        Dim 下载地址 As String = ""
        If info.Assets IsNot Nothing Then
            For Each a In info.Assets
                If String.Equals(a.FileName, 目标资源名, StringComparison.OrdinalIgnoreCase) Then
                    下载地址 = a.DownloadUrl
                    Exit For
                End If
            Next
        End If
        If String.IsNullOrEmpty(下载地址) Then
            输出错误($"在发行版 {info.TagName} 中未找到资源 {目标资源名}。")
            Return False
        End If

        Dim 保存路径 = 获取更新文件路径(程序目录, 主程序文件名)
        Try
            If File.Exists(保存路径) Then File.Delete(保存路径)
        Catch ex As Exception
            输出错误($"无法清理旧的更新文件 {保存路径}：{ex.Message}")
            Return False
        End Try

        输出信息($"开始下载 {目标资源名} ...")
        Dim 完成源 As New TaskCompletionSource(Of Boolean)(TaskCreationOptions.RunContinuationsAsynchronously)
        Dim 下载器 As New LakeUI.DownloadFile With {.Url = 下载地址, .SavePath = 保存路径, .ThreadCount = 8}
        AddHandler 下载器.ProgressChanged,
            Sub(s, e)
                Dim 百分比 = If(e.TotalBytes > 0, e.BytesDownloaded * 100.0R / e.TotalBytes, 0)
                Dim 文本 = $"  {生成进度条(百分比)} {百分比,5:F1}%  {e.BytesDownloaded / 1048576.0R:F1}M/{e.TotalBytes / 1048576.0R:F1}M  {e.SpeedBytesPerSecond / 1024.0R:F1}K/s  ETA {格式化剩余时间(e.EstimatedTimeRemaining)}"
                写入覆盖行(文本, ConsoleColor.Cyan)
            End Sub
        AddHandler 下载器.DownloadCompleted,
            Sub(s, e)
                Console.WriteLine()
                输出成功("下载完成。")
                完成源.TrySetResult(True)
            End Sub
        AddHandler 下载器.DownloadFailed,
            Sub(s, e)
                Console.WriteLine()
                输出错误($"下载失败：{e.Message}")
                完成源.TrySetResult(False)
            End Sub

        Try
            Await 下载器.StartAsync()
        Catch ex As Exception
            Console.WriteLine()
            输出错误($"下载过程出现异常：{ex.Message}")
            Return False
        End Try
        Return Await 完成源.Task
    End Function

    '======================================================================
    ' 等待主程序退出（仅识别目标目录下的实例，避免误伤其他副本）
    '======================================================================
    Private Sub 等待主程序退出(目标目录 As String)
        输出章节("等待主程序退出")
        Dim 目标主程序 = Path.GetFullPath(Path.Combine(目标目录, 主程序文件名))
        Dim 首次提示 As Boolean = True
        Dim 动画帧 = {"|"c, "/"c, "-"c, "\"c}
        Dim 帧序号 As Integer = 0
        Do
            Dim 运行中 As New List(Of Integer)
            For Each p In Process.GetProcessesByName(Path.GetFileNameWithoutExtension(主程序文件名))
                Try
                    Dim 路径 As String = ""
                    Try
                        路径 = p.MainModule?.FileName
                    Catch
                        路径 = ""
                    End Try
                    If Not String.IsNullOrEmpty(路径) AndAlso
                       String.Equals(Path.GetFullPath(路径), 目标主程序, StringComparison.OrdinalIgnoreCase) Then
                        运行中.Add(p.Id)
                    End If
                Catch
                Finally
                    Try
                        p.Dispose()
                    Catch
                    End Try
                End Try
            Next

            If 运行中.Count = 0 Then
                If Not 首次提示 Then Console.WriteLine()
                输出成功("已确认主程序所有目标实例均已退出。")
                Exit Sub
            End If

            If 首次提示 Then
                输出警告("检测到主程序仍在运行，等待全部目标实例关闭 ...")
                首次提示 = False
            End If
            Dim 信息 = $"  {动画帧(帧序号 Mod 动画帧.Length)} 剩余运行实例数：{运行中.Count}  PID: {String.Join(",", 运行中)}"
            帧序号 += 1
            写入覆盖行(信息, ConsoleColor.Yellow)
            Thread.Sleep(500)
        Loop
    End Sub

    '======================================================================
    ' 应用更新文件：扫描程序目录，去掉 _update 覆盖目标
    '======================================================================
    Private Function 应用更新文件(程序目录 As String) As Integer
        输出章节("应用更新文件")
        输出信息("正在扫描待应用的更新文件 ...")
        Dim 计数 As Integer = 0
        Dim 自身路径 As String = ""
        Try
            自身路径 = Path.GetFullPath(Environment.ProcessPath)
        Catch
        End Try

        Dim 文件列表 As IEnumerable(Of String)
        Try
            输出信息($"扫描目录：{程序目录}")
            文件列表 = Directory.EnumerateFiles(程序目录, "*", SearchOption.TopDirectoryOnly)
        Catch ex As Exception
            输出错误($"无法枚举程序目录：{ex.Message}")
            Return 0
        End Try

        For Each 源文件 In 文件列表
            Dim 文件名 = Path.GetFileName(源文件)
            Dim 目标名 = 获取更新目标文件名(文件名)
            If String.IsNullOrEmpty(目标名) Then Continue For

            Dim 目标路径 = Path.Combine(程序目录, 目标名)

            '不允许更新器自身被覆盖
            If Not String.IsNullOrEmpty(自身路径) AndAlso
               String.Equals(Path.GetFullPath(目标路径), 自身路径, StringComparison.OrdinalIgnoreCase) Then
                输出警告($"跳过：{文件名} 指向更新器自身")
                Continue For
            End If

            Try
                File.Move(源文件, 目标路径, True)
                输出成功($"{文件名}  ->  {目标名}")
                计数 += 1
            Catch ex As Exception
                输出错误($"应用 {文件名} 失败：{ex.Message}")
            End Try
        Next
        Return 计数
    End Function

    Private Sub 清理架构主程序文件(程序目录 As String)
        For Each 文件名 In 架构主程序文件名列表
            Dim 文件路径 = Path.Combine(程序目录, 文件名)
            If Not File.Exists(文件路径) Then Continue For

            Try
                File.Delete(文件路径)
                输出成功($"已删除旧的架构主程序：{文件名}")
            Catch ex As Exception
                输出警告($"无法删除旧的架构主程序 {文件名}：{ex.Message}")
            End Try
        Next
    End Sub

    Private Function 存在待应用更新文件(程序目录 As String) As Boolean
        Try
            Return Directory.EnumerateFiles(程序目录, "*", SearchOption.TopDirectoryOnly).
                Any(Function(文件) Not String.IsNullOrEmpty(获取更新目标文件名(Path.GetFileName(文件))))
        Catch
            Return False
        End Try
    End Function

    Private Function 获取更新目标文件名(文件名 As String) As String
        If String.IsNullOrWhiteSpace(文件名) Then Return ""

        Dim 不含扩展名 = Path.GetFileNameWithoutExtension(文件名)
        If String.IsNullOrWhiteSpace(不含扩展名) OrElse
           Not 不含扩展名.EndsWith(更新后缀, StringComparison.OrdinalIgnoreCase) Then Return ""

        Dim 原始文件名 = 不含扩展名.Substring(0, 不含扩展名.Length - 更新后缀.Length)
        If String.IsNullOrWhiteSpace(原始文件名) Then Return ""

        Return 原始文件名 & Path.GetExtension(文件名)
    End Function

    Private Function 获取更新文件路径(程序目录 As String, 文件名 As String) As String
        Return Path.Combine(程序目录, 获取更新文件名(文件名))
    End Function

    Private Function 获取更新文件名(文件名 As String) As String
        Return Path.GetFileNameWithoutExtension(文件名) & 更新后缀 & Path.GetExtension(文件名)
    End Function

    '======================================================================
    ' 询问是否重启：Y/回车=重启，N=不重启，超时(10s)=不重启
    '======================================================================
    Private Function 询问是否重启() As Boolean
        Const 等待秒数 As Integer = 10
        输出章节("完成")
        输出信息($"是否启动主程序？  [Y]=立即启动 (默认)   [N]=不启动")
        输出警告($"超时 {等待秒数} 秒未操作将不启动并自动退出。")
        For 剩余 = 等待秒数 To 1 Step -1
            Dim 百分比 = 剩余 * 100.0R / 等待秒数
            Dim 提示 = $"  {生成进度条(百分比)} 请选择（剩余 {剩余,2} 秒，直接回车=启动）"
            写入覆盖行(提示, ConsoleColor.Green)
            Dim 截止 = Environment.TickCount + 1000
            Do While Environment.TickCount < 截止
                Try
                    If Console.KeyAvailable Then
                        Dim k = Console.ReadKey(True).Key
                        Console.WriteLine()
                        Select Case k
                            Case ConsoleKey.Y, ConsoleKey.Enter
                                Return True
                            Case ConsoleKey.N
                                Return False
                        End Select
                    End If
                Catch
                    '输入被重定向或不可用，跳过键盘检测
                End Try
                Thread.Sleep(50)
            Loop
        Next
        Console.WriteLine()
        输出警告("超时未操作，将不启动主程序。")
        Return False
    End Function

    '======================================================================
    ' 启动主程序
    '======================================================================
    Private Sub 启动主程序(目标目录 As String)
        Try
            Dim psi As New ProcessStartInfo With {
                .FileName = Path.Combine(目标目录, 主程序文件名),
                .WorkingDirectory = 目标目录,
                .UseShellExecute = True
            }
            Process.Start(psi)
            输出成功("已启动主程序。")
        Catch ex As Exception
            输出错误($"启动主程序失败：{ex.Message}")
        End Try
    End Sub

    '======================================================================
    ' .NET 运行库检查与安装
    '======================================================================
    Private Async Function 确保主程序运行环境(目标目录 As String) As Task(Of Boolean)
        Dim 架构 = 获取架构名称()
        If String.IsNullOrEmpty(架构) Then
            输出错误($"当前系统架构 {RuntimeInformation.OSArchitecture} 不在发行版支持范围内，仅支持 x64 与 arm64。")
            Return False
        End If

        输出信息($"系统架构：{架构}")
        Return Await 确保DotNet10WindowsDesktop运行库(目标目录, 架构)
    End Function

    Private Async Function 确保DotNet10WindowsDesktop运行库(目标目录 As String, 架构 As String) As Task(Of Boolean)
        If 检查DotNet10WindowsDesktop运行库(架构) Then Return True

        输出警告("未检测到 .NET 10 Desktop Runtime，开始自动下载并静默安装。")
        Dim 安装器路径 = Path.Combine(目标目录, $"windowsdesktop-runtime-10.0-win-{架构}.exe")
        Dim 下载地址 = 获取DotNet10WindowsDesktop运行库下载地址(架构)
        If String.IsNullOrEmpty(下载地址) Then
            输出错误($"当前架构 {架构} 没有可用的 .NET 10 Desktop Runtime 下载地址。")
            Return False
        End If

        If Not Await 下载DotNet10WindowsDesktop运行库安装器(下载地址, 安装器路径) Then Return False
        If Not 静默安装DotNet10WindowsDesktop运行库(安装器路径) Then Return False

        输出信息("正在复检 .NET 10 Desktop Runtime ...")
        If 检查DotNet10WindowsDesktop运行库(架构) Then Return True

        输出错误("安装结束后仍未检测到 .NET 10 Desktop Runtime。")
        Return False
    End Function

    Private Function 获取DotNet10WindowsDesktop运行库下载地址(架构 As String) As String
        Select Case 架构
            Case "x64", "arm64"
                Return $"https://aka.ms/dotnet/10.0/windowsdesktop-runtime-win-{架构}.exe"
            Case Else
                Return ""
        End Select
    End Function

    Private Async Function 下载DotNet10WindowsDesktop运行库安装器(下载地址 As String, 保存路径 As String) As Task(Of Boolean)
        Try
            If File.Exists(保存路径) Then File.Delete(保存路径)
        Catch ex As Exception
            输出错误($"无法清理旧的 .NET 运行库安装器：{ex.Message}")
            Return False
        End Try

        输出信息("正在下载 .NET 10 Desktop Runtime 安装器 ...")
        Dim 完成源 As New TaskCompletionSource(Of Boolean)(TaskCreationOptions.RunContinuationsAsynchronously)
        Dim 下载器 As New LakeUI.DownloadFile With {.Url = 下载地址, .SavePath = 保存路径, .ThreadCount = 8}
        AddHandler 下载器.ProgressChanged,
            Sub(s, e)
                Dim 百分比 = If(e.TotalBytes > 0, e.BytesDownloaded * 100.0R / e.TotalBytes, 0)
                Dim 文本 = $"  {生成进度条(百分比)} {百分比,5:F1}%  {e.BytesDownloaded / 1048576.0R:F1}M/{e.TotalBytes / 1048576.0R:F1}M  {e.SpeedBytesPerSecond / 1024.0R:F1}K/s  ETA {格式化剩余时间(e.EstimatedTimeRemaining)}"
                写入覆盖行(文本, ConsoleColor.Cyan)
            End Sub
        AddHandler 下载器.DownloadCompleted,
            Sub(s, e)
                Console.WriteLine()
                输出成功(".NET 10 Desktop Runtime 安装器下载完成。")
                完成源.TrySetResult(True)
            End Sub
        AddHandler 下载器.DownloadFailed,
            Sub(s, e)
                Console.WriteLine()
                输出错误($".NET 10 Desktop Runtime 安装器下载失败：{e.Message}")
                完成源.TrySetResult(False)
            End Sub

        Try
            Await 下载器.StartAsync()
        Catch ex As Exception
            Console.WriteLine()
            输出错误($".NET 10 Desktop Runtime 下载过程出现异常：{ex.Message}")
            Return False
        End Try

        Return Await 完成源.Task
    End Function

    Private Function 静默安装DotNet10WindowsDesktop运行库(安装器路径 As String) As Boolean
        输出信息("正在静默安装 .NET 10 Desktop Runtime ...")
        Try
            Dim psi As New ProcessStartInfo With {
                .FileName = 安装器路径,
                .Arguments = "/install /quiet /norestart",
                .UseShellExecute = True,
                .Verb = "runas"
            }
            Using p = Process.Start(psi)
                If p Is Nothing Then
                    输出错误("无法启动 .NET 10 Desktop Runtime 安装器。")
                    Return False
                End If

                p.WaitForExit()
                Select Case p.ExitCode
                    Case 0
                        输出成功(".NET 10 Desktop Runtime 静默安装完成。")
                        Return True
                    Case 3010, 1641
                        输出成功(".NET 10 Desktop Runtime 静默安装完成，需要重启系统后完全生效。")
                        Return True
                    Case Else
                        输出错误($".NET 10 Desktop Runtime 安装失败，退出代码：{p.ExitCode}")
                        Return False
                End Select
            End Using
        Catch ex As Exception
            输出错误($".NET 10 Desktop Runtime 静默安装启动失败：{ex.Message}")
            Return False
        End Try
    End Function

    Private Function 检查DotNet10WindowsDesktop运行库(架构 As String) As Boolean
        输出信息("正在检查 .NET 10 Desktop Runtime ...")

        If DotNet命令包含DotNet10WindowsDesktop运行库() Then
            输出成功("已通过 dotnet --list-runtimes 检测到 .NET 10 Desktop Runtime。")
            Return True
        End If

        Return False
    End Function

    Private Function DotNet命令包含DotNet10WindowsDesktop运行库() As Boolean
        Try
            Dim psi As New ProcessStartInfo With {
                .FileName = "dotnet",
                .Arguments = "--list-runtimes",
                .UseShellExecute = False,
                .RedirectStandardOutput = True,
                .RedirectStandardError = True,
                .CreateNoWindow = True
            }
            Using p = Process.Start(psi)
                If p Is Nothing Then Return False
                Dim 已找到 As Boolean = False
                Do Until p.StandardOutput.EndOfStream
                    Dim line = p.StandardOutput.ReadLine()
                    If DotNet运行库行是WindowsDesktop10(line) Then
                        已找到 = True
                    End If
                Loop

                If Not p.WaitForExit(3000) Then
                    Try
                        p.Kill(True)
                    Catch
                    End Try
                    Return False
                End If

                Return 已找到
            End Using
        Catch
            Return False
        End Try
    End Function

    Private Function DotNet运行库行是WindowsDesktop10(line As String) As Boolean
        If String.IsNullOrWhiteSpace(line) OrElse Not line.StartsWith("Microsoft.WindowsDesktop.App ", StringComparison.OrdinalIgnoreCase) Then Return False

        Dim parts = line.Split(" "c, StringSplitOptions.RemoveEmptyEntries)
        Return parts.Length >= 2 AndAlso 是DotNet10版本(parts(1))
    End Function

    Private Function 是DotNet10版本(版本文本 As String) As Boolean
        If String.IsNullOrWhiteSpace(版本文本) Then Return False
        Dim 版本主体 = 版本文本.Split("-"c)(0)
        Dim version As Version = Nothing
        Return Version.TryParse(版本主体, version) AndAlso version.Major >= 10
    End Function

    '======================================================================
    ' 控制台输出
    '======================================================================
    Private Sub 输出标题()
        写入彩色行("╔════════════════════════════════════════════════════╗", ConsoleColor.DarkCyan)
        写入彩色行("║              FFmpegFreeUI 自动更新器               ║", ConsoleColor.Cyan)
        写入彩色行("╚════════════════════════════════════════════════════╝", ConsoleColor.DarkCyan)
        Console.WriteLine()
    End Sub

    Private Sub 输出章节(标题 As String)
        写入彩色行($"── {标题} {New String("─"c, Math.Max(0, 48 - 标题.Length))}", ConsoleColor.DarkCyan)
    End Sub

    Private Sub 输出信息(消息 As String)
        写入彩色行($"  • {消息}", ConsoleColor.Gray)
    End Sub

    Private Sub 输出成功(消息 As String)
        写入彩色行($"  √ {消息}", ConsoleColor.Green)
    End Sub

    Private Sub 输出警告(消息 As String)
        写入彩色行($"  ! {消息}", ConsoleColor.Yellow)
    End Sub

    Private Sub 输出错误(消息 As String)
        写入彩色行($"  × {消息}", ConsoleColor.Red)
    End Sub

    Private Sub 写入彩色行(文本 As String, 颜色 As ConsoleColor)
        Dim 原色 = Console.ForegroundColor
        Console.ForegroundColor = 颜色
        Console.WriteLine(文本)
        Console.ForegroundColor = 原色
    End Sub

    Private Sub 写入覆盖行(文本 As String, 颜色 As ConsoleColor)
        Dim 原色 = Console.ForegroundColor
        Console.ForegroundColor = 颜色
        Console.Write(vbCr & 文本.PadRight(控制台行宽))
        Console.ForegroundColor = 原色
    End Sub

    Private Function 生成进度条(百分比 As Double) As String
        Dim 安全百分比 = Math.Clamp(百分比, 0, 100)
        Dim 已完成 = CInt(Math.Round(安全百分比 / 100 * 进度条宽度))
        Return "[" & New String("█"c, 已完成) & New String(" "c, 进度条宽度 - 已完成) & "]"
    End Function

    '======================================================================
    ' 通用工具函数
    '======================================================================

    Private Function 获取架构名称() As String
        Select Case RuntimeInformation.OSArchitecture
            Case Architecture.Arm64
                Return "arm64"
            Case Architecture.X64
                Return "x64"
            Case Architecture.X86
                If Environment.Is64BitOperatingSystem Then Return "x64"
        End Select

        Return ""
    End Function

    Private Function 格式化剩余时间(t As TimeSpan) As String
        If t.Ticks <= 0 OrElse Double.IsNaN(t.TotalSeconds) OrElse Double.IsInfinity(t.TotalSeconds) Then Return "--:--"
        If t.Days > 0 Then Return $"{t.Days}:{t.Hours:D2}:{t.Minutes:D2}:{t.Seconds:D2}"
        If t.Hours > 0 Then Return $"{t.Hours}:{t.Minutes:D2}:{t.Seconds:D2}"
        Return $"{t.Minutes:D2}:{t.Seconds:D2}"
    End Function

    Private Sub 尝试等待按键()
        Try
            Console.ReadKey(True)
        Catch
        End Try
    End Sub

End Module
