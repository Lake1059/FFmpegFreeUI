Imports System.Net.Http
Imports System.Net.NetworkInformation
Imports Microsoft.Win32

''' <summary>为 Agent 管理系统代理和直连两套路由，并在网络/代理设置变化时轮换连接池。</summary>
Friend Module Agent网络连接_v6
    Private ReadOnly SyncRoot As New Object
    Private ProxyClient As HttpClient
    Private DirectClient As HttpClient

    Private ReadOnly Initialized As Boolean = Initialize()

    Private Function Initialize() As Boolean
        AddHandler NetworkChange.NetworkAddressChanged, AddressOf 网络状态变化
        AddHandler NetworkChange.NetworkAvailabilityChanged, AddressOf 网络可用性变化
        AddHandler SystemEvents.UserPreferenceChanged, AddressOf 系统偏好变化
        Return True
    End Function

    Friend Function 获取客户端(Optional useProxy As Boolean = True) As HttpClient
        SyncLock SyncRoot
            If useProxy Then
                If ProxyClient Is Nothing Then ProxyClient = 创建客户端(True)
                Return ProxyClient
            End If
            If DirectClient Is Nothing Then DirectClient = 创建客户端(False)
            Return DirectClient
        End SyncLock
    End Function

    Private Function 创建客户端(useProxy As Boolean) As HttpClient
        Dim handler As New SocketsHttpHandler With {
            .UseProxy = useProxy,
            .PooledConnectionLifetime = TimeSpan.FromMinutes(2),
            .PooledConnectionIdleTimeout = TimeSpan.FromMinutes(1)
        }
        Return New HttpClient(handler) With {.Timeout = TimeSpan.FromSeconds(90)}
    End Function

    Private Sub 网络状态变化(sender As Object, e As EventArgs)
        刷新客户端()
    End Sub

    Private Sub 网络可用性变化(sender As Object, e As NetworkAvailabilityEventArgs)
        刷新客户端()
    End Sub

    Private Sub 系统偏好变化(sender As Object, e As UserPreferenceChangedEventArgs)
        ' Windows raises this for internet/proxy preference changes (category varies by Windows version).
        刷新客户端()
    End Sub

    Private Sub 刷新客户端()
        SyncLock SyncRoot
            ' Do not dispose old clients here: in-flight Agent requests may still be using them.
            ' Dropping the references lets those clients be collected after their requests complete.
            ProxyClient = Nothing
            DirectClient = Nothing
        End SyncLock
    End Sub
End Module
