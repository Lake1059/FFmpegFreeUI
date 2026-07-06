Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Imports System.Threading

Public Class 端口监听_v6

    Private Shared UDP客户端 As UdpClient
    Private Shared 监听任务 As Task
    Private Shared 取消令牌源 As CancellationTokenSource
    Private Shared ReadOnly 锁对象 As New Object()

    Public Shared ReadOnly Property 是否正在运行 As Boolean
        Get
            SyncLock 锁对象
                Return UDP客户端 IsNot Nothing AndAlso 取消令牌源 IsNot Nothing AndAlso Not 取消令牌源.IsCancellationRequested
            End SyncLock
        End Get
    End Property

    Public Shared Sub 启动客户端()
        SyncLock 锁对象
            If UDP客户端 IsNot Nothing Then Exit Sub

            Try
                Dim port = 规范化端口(设置_v6.实例对象.监听的端口)
                设置_v6.实例对象.监听的端口 = port.ToString()
                UDP客户端 = New UdpClient(port)
                UDP客户端.Client.ReceiveTimeout = 1000
                取消令牌源 = New CancellationTokenSource()
                监听任务 = Task.Run(Sub() 监听消息(取消令牌源.Token), 取消令牌源.Token)
            Catch ex As Exception
                UDP客户端 = Nothing
                取消令牌源 = Nothing
                监听任务 = Nothing
                设置_v6.实例对象.是否监听端口 = False
                UI线程执行(Sub() MsgBox("远程调用监听启动失败：" & ex.Message, MsgBoxStyle.Exclamation))
            End Try
        End SyncLock
    End Sub

    Public Shared Sub 重启客户端()
        停止客户端()
        启动客户端()
    End Sub

    Public Shared Sub 停止客户端()
        Dim client As UdpClient = Nothing
        Dim cts As CancellationTokenSource = Nothing
        SyncLock 锁对象
            client = UDP客户端
            cts = 取消令牌源
            UDP客户端 = Nothing
            取消令牌源 = Nothing
            监听任务 = Nothing
        End SyncLock

        Try
            cts?.Cancel()
            client?.Close()
        Catch
        Finally
            cts?.Dispose()
            client?.Dispose()
        End Try
    End Sub

    Private Shared Sub 监听消息(token As CancellationToken)
        While Not token.IsCancellationRequested
            Dim client As UdpClient = Nothing
            SyncLock 锁对象
                client = UDP客户端
            End SyncLock
            If client Is Nothing Then Exit While

            Try
                Dim remote As IPEndPoint = Nothing
                Dim bytes = client.Receive(remote)
                Dim text = Encoding.UTF8.GetString(bytes)
                UI线程执行(Sub() 启动参数响应_v6.处理接收的参数(text))
            Catch ex As SocketException When ex.SocketErrorCode = SocketError.TimedOut
            Catch ex As ObjectDisposedException
                Exit While
            Catch
            End Try
        End While
    End Sub

    Private Shared Sub UI线程执行(action As Action)
        If action Is Nothing Then Exit Sub
        Try
            If FormMain_v6 IsNot Nothing AndAlso FormMain_v6.IsHandleCreated AndAlso Not FormMain_v6.IsDisposed Then
                If FormMain_v6.InvokeRequired Then
                    FormMain_v6.BeginInvoke(action)
                Else
                    action()
                End If
            Else
                action()
            End If
        Catch
        End Try
    End Sub

    Private Shared Function 规范化端口(value As String) As Integer
        Dim port As Integer
        If Not Integer.TryParse(If(value, "").Trim(), port) Then port = 10591
        If port = 10590 Then port = 10591
        If port < 1 OrElse port > 65535 Then port = 10591
        Return port
    End Function

    Public Shared Function 获取本地IPv4() As String
        Dim host = Dns.GetHostEntry(Dns.GetHostName())
        For Each ip In host.AddressList
            If ip.AddressFamily = AddressFamily.InterNetwork Then Return ip.ToString()
        Next
        Return "unknown"
    End Function

    Public Shared Function 获取本地IPv6() As String
        Dim host = Dns.GetHostEntry(Dns.GetHostName())
        For Each ip In host.AddressList
            If ip.AddressFamily = AddressFamily.InterNetworkV6 Then Return ip.ToString()
        Next
        Return "unknown"
    End Function

End Class
