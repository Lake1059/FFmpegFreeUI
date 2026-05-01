Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Imports System.Threading

Public Class 端口监听
    Public Shared Property UDP客户端 As UdpClient
    Public Shared Property 监听任务列表 As New List(Of Task)
    Public Shared Property 取消令牌源 As CancellationTokenSource
    Public Shared Property 是否正在运行 As Boolean = False
    Public Shared Property 是否收到响应 As Boolean = False
    Public Shared Property 响应线程数量 As Integer = 1
    Private Shared ReadOnly 锁对象 As New Object()

    Public Shared Sub 启动客户端()
        是否正在运行 = True
        是否收到响应 = False

        Dim p As String = 用户设置.实例对象.监听的端口
        If p = "10590" Then p = "10591"
        If p = "" Then p = "10591"

        Dim 端口号 As Integer = Integer.Parse(p)
        ' 绑定到本地端口进行监听
        UDP客户端 = New UdpClient(端口号)
        UDP客户端.Client.ReceiveTimeout = 10000

        取消令牌源 = New CancellationTokenSource()

        For i As Integer = 1 To 响应线程数量
            Dim 监听任务 As Task = Task.Run(AddressOf 监听消息, 取消令牌源.Token)
            监听任务列表.Add(监听任务)
        Next
    End Sub

    Public Shared Sub 监听消息()
        While 是否正在运行 AndAlso Not 取消令牌源.Token.IsCancellationRequested
            Try
                Dim 远程端点 As IPEndPoint = Nothing
                Dim 数据_接收到的字节 As Byte()
                If UDP客户端 Is Nothing Then Exit While
                数据_接收到的字节 = UDP客户端.Receive(远程端点)
                Dim 数据_文本 = Encoding.UTF8.GetString(数据_接收到的字节)
                界面线程执行(Sub()
                           启动参数响应.处理接收的参数(数据_文本.Split(" "c).ToList())
                       End Sub)
            Catch ex As SocketException When ex.SocketErrorCode = SocketError.TimedOut
                ' 超时继续循环
            Catch ex As ObjectDisposedException
                ' 客户端已关闭，退出循环
                Exit While
            Catch ex As Exception
                ' 其他异常可记录日志
            End Try
        End While
    End Sub

    Public Shared Async Sub 停止客户端()
        是否收到响应 = False
        是否正在运行 = False

        取消令牌源?.Cancel()
        UDP客户端?.Close()

        If 监听任务列表.Count > 0 Then
            Await Task.WhenAll(监听任务列表)
        End If

        取消令牌源?.Dispose()
        取消令牌源 = Nothing
        UDP客户端 = Nothing
        监听任务列表.Clear()
    End Sub

    Public Shared Function 获取本地IPv4() As String
        Dim host = Dns.GetHostEntry(Dns.GetHostName())
        For Each ip In host.AddressList
            If ip.AddressFamily = AddressFamily.InterNetwork Then
                Return ip.ToString()
            End If
        Next
        Return "unknow"
    End Function

    Public Shared Function 获取本地IPv6() As String
        Dim host = Dns.GetHostEntry(Dns.GetHostName())
        For Each ip In host.AddressList
            If ip.AddressFamily = AddressFamily.InterNetworkV6 Then
                Return ip.ToString()
            End If
        Next
        Return "unknow"
    End Function

End Class