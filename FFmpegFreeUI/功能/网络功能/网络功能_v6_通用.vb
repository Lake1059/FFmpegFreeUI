Imports System.Diagnostics
Imports System.IO
Imports System.Reflection
Imports System.Security.Cryptography
Imports System.Threading

Friend NotInheritable Class 网络功能_v6_通用

    Friend Shared ReadOnly 联网请求超时时间 As TimeSpan = TimeSpan.FromSeconds(45)

    Private Shared ReadOnly 下载无进展超时时间 As TimeSpan = TimeSpan.FromSeconds(90)
    Private Shared ReadOnly 下载看门狗间隔 As TimeSpan = TimeSpan.FromSeconds(5)

    Private Sub New()
    End Sub

    Friend Shared Function 创建联网请求取消源() As CancellationTokenSource
        Return New CancellationTokenSource(联网请求超时时间)
    End Function

    Friend Shared Function 获取异常消息(ex As Exception) As String
        If TypeOf ex Is OperationCanceledException Then
            Return $"请求超过 {联网请求超时时间.TotalSeconds:F0} 秒未完成"
        End If

        Return ex.Message
    End Function

    Friend Shared Function 计算文件SHA256(filePath As String) As String
        Using stream As FileStream = File.OpenRead(filePath)
            Using sha = SHA256.Create()
                Return Convert.ToHexString(sha.ComputeHash(stream))
            End Using
        End Using
    End Function

    Friend Shared Function 安全获取程序集类型(程序集 As Assembly) As IEnumerable(Of Type)
        Try
            Return 程序集.GetTypes()
        Catch ex As ReflectionTypeLoadException
            Return ex.Types.Where(Function(x) x IsNot Nothing)
        Catch
            Return Array.Empty(Of Type)()
        End Try
    End Function

    Friend Shared Function 创建下载器(下载地址 As String, 保存路径 As String, 线程数 As Integer, 默认线程数 As Integer, Optional 下载授权Token As String = "") As LakeUI.DownloadFile
        Dim 下载器 As New LakeUI.DownloadFile With {
            .Url = 下载地址,
            .SavePath = 保存路径,
            .ThreadCount = 获取有效下载线程数(线程数, 默认线程数)
        }
        应用下载授权Header(下载器, 下载授权Token)
        Return 下载器
    End Function

    Friend Shared Sub 绑定下载进度事件(下载器 As LakeUI.DownloadFile, 更新进度 As Action(Of String), 下载失败 As Action(Of String))
        If 下载器 Is Nothing Then Return

        AddHandler 下载器.ProgressChanged,
            Sub(s, e)
                更新进度?.Invoke(格式化下载进度(e.BytesDownloaded, e.TotalBytes, e.SpeedBytesPerSecond, e.EstimatedTimeRemaining))
            End Sub

        AddHandler 下载器.DownloadFailed,
            Sub(s, e)
                下载失败?.Invoke(e.Message)
            End Sub
    End Sub

    Friend Shared Async Function 启动下载并监控Async(下载器 As LakeUI.DownloadFile) As Task
        Using cts As New CancellationTokenSource()
            Dim 因无进展取消 As Boolean = False
            Dim 下载任务 As Task = 下载器.StartAsync(cts.Token)

            Dim 监控任务 As Task = Task.Run(Async Function()
                                            Dim 上次字节 As Long = 下载器.BytesDownloaded
                                            Dim 上次变化时间 As DateTime = DateTime.UtcNow

                                            While Not 下载任务.IsCompleted
                                                Try
                                                    Await Task.Delay(下载看门狗间隔, cts.Token).ConfigureAwait(False)
                                                Catch ex As OperationCanceledException
                                                    Exit While
                                                End Try

                                                Dim 当前字节 As Long = 下载器.BytesDownloaded
                                                If 当前字节 <> 上次字节 Then
                                                    上次字节 = 当前字节
                                                    上次变化时间 = DateTime.UtcNow
                                                ElseIf DateTime.UtcNow - 上次变化时间 >= 下载无进展超时时间 Then
                                                    因无进展取消 = True
                                                    cts.Cancel()
                                                    Exit While
                                                End If
                                            End While
                                        End Function)

            Await 下载任务
            cts.Cancel()

            Try
                Await 监控任务.ConfigureAwait(False)
            Catch
            End Try

            If 因无进展取消 Then
                Throw New TimeoutException($"下载超过 {下载无进展超时时间.TotalSeconds:F0} 秒没有收到新数据。")
            End If
        End Using
    End Function

    Friend Shared Sub 应用下载授权Header(下载器 As LakeUI.DownloadFile, 下载授权Token As String)
        If 下载器 Is Nothing OrElse String.IsNullOrWhiteSpace(下载授权Token) Then Return
        下载器.RequestHeaders("Authorization") = $"Bearer {下载授权Token}"
    End Sub

    Friend Shared Function 获取有效下载线程数(线程数 As Integer, 默认线程数 As Integer) As Integer
        If 线程数 > 0 Then Return 线程数
        Return 默认线程数
    End Function

    Friend Shared Function 格式化下载进度(已下载字节 As Long, 总字节 As Long, 每秒字节 As Double, 剩余时间 As TimeSpan) As String
        Dim 百分比 As Double = If(总字节 > 0, 已下载字节 * 100.0R / 总字节, 0)
        Return $"{百分比:F0}%   {格式化下载大小(已下载字节)}/{格式化下载大小(总字节)}   {格式化下载速度(每秒字节)}   ETA {格式化剩余时间(剩余时间)}"
    End Function

    Friend Shared Function 格式化下载速度(每秒字节 As Double) As String
        If 每秒字节 <= 0 Then Return "0 KB/s"
        If 每秒字节 < 1024 Then Return $"{每秒字节:F0} B/s"
        If 每秒字节 < 1024 * 1024 Then Return $"{每秒字节 / 1024:F0} KB/s"
        Return $"{每秒字节 / 1024 / 1024:F1} MB/s"
    End Function

    Private Shared Function 格式化下载大小(字节数 As Long) As String
        Return $"{字节数 / 1024 / 1024:F1}MB"
    End Function

    Private Shared Function 格式化剩余时间(time As TimeSpan) As String
        If time.Days > 0 Then Return $"{time.Days}:{time.Hours}:{time.Minutes}:{time.Seconds}"
        If time.Hours > 0 Then Return $"{time.Hours}:{time.Minutes}:{time.Seconds}"
        If time.Minutes > 0 Then Return $"{time.Minutes}:{time.Seconds}"
        Return time.Seconds.ToString()
    End Function

    Friend Shared Sub 强制关闭目标更新器进程(目标路径 As String)
        Dim 目标完整路径 As String = Path.GetFullPath(目标路径)
        Dim 目标进程名 As String = Path.GetFileNameWithoutExtension(目标完整路径)

        For Each 进程 As Process In Process.GetProcessesByName(目标进程名)
            Try
                If 是目标路径进程(进程, 目标完整路径) Then 关闭进程(进程)
            Finally
                进程.Dispose()
            End Try
        Next
    End Sub

    Private Shared Function 是目标路径进程(进程 As Process, 目标完整路径 As String) As Boolean
        Dim 进程路径 As String = 获取进程路径(进程)
        If String.IsNullOrWhiteSpace(进程路径) Then Return False

        Return String.Equals(Path.GetFullPath(进程路径), 目标完整路径, StringComparison.OrdinalIgnoreCase)
    End Function

    Private Shared Function 获取进程路径(进程 As Process) As String
        Try
            Return 进程.MainModule?.FileName
        Catch
            Return ""
        End Try
    End Function

    Private Shared Sub 关闭进程(进程 As Process)
        进程.Kill(True)
        进程.WaitForExit(5000)
    End Sub

End Class
