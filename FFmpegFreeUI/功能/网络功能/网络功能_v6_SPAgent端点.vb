Imports System.IO
Imports System.Net.Http
Imports System.Text.Json
Imports System.Threading

Friend NotInheritable Class 网络功能_v6_SPAgent端点

    Private Const SPAgent端点列表地址 As String = "https://example.com/ffmpegfreeui/sp-agent-endpoints.json"
    Private Const SP解锁器文件名 As String = "FFmpegFreeUISupporter_v6.dll"
    Private Const SP解锁器SHA256 As String = "A08B0B3A00ECBF6AD2A83B39BE105D4C385D3AFD8DC4C5CE94D1C7E400D5741E"
    Private Const SP解锁器命名空间 As String = "FFmpegFreeUISupporter"

    Private Shared ReadOnly SPAgent端点Http As New HttpClient With {.Timeout = TimeSpan.FromSeconds(20)}
    Private Shared ReadOnly SPAgent端点同步锁 As New Object()
    Private Shared ReadOnly SPAgent端点内存列表 As New List(Of 网络功能.AgentSpEndpointInfo)
    Private Shared SPAgent端点已启动后台获取 As Boolean = False
    Private Shared SPAgent端点正在获取 As Boolean = False
    Private Shared SPAgent端点最后错误缓存 As String = ""
    Private Shared 当前SPAgent端点索引 As Integer = -1

    Private Sub New()
    End Sub

    Friend Shared ReadOnly Property SPAgent端点最后错误 As String
        Get
            SyncLock SPAgent端点同步锁
                Return SPAgent端点最后错误缓存
            End SyncLock
        End Get
    End Property

    Friend Shared ReadOnly Property 是否正在获取SPAgent端点 As Boolean
        Get
            SyncLock SPAgent端点同步锁
                Return SPAgent端点正在获取
            End SyncLock
        End Get
    End Property

    Friend Shared Sub 启动时后台获取SPAgent端点()
        SyncLock SPAgent端点同步锁
            If SPAgent端点已启动后台获取 Then Exit Sub
            SPAgent端点已启动后台获取 = True
            SPAgent端点正在获取 = True
            SPAgent端点最后错误缓存 = ""
        End SyncLock

        刷新SPAgent端点设置界面()

        Task.Run(Async Function()
                     Try
                         Dim 校验错误 As String = 校验SPAgent端点获取资格()
                         If 校验错误 <> "" Then
                             设置SPAgent端点获取结果(New List(Of 网络功能.AgentSpEndpointInfo), 校验错误)
                             Return
                         End If

                         Dim 端点列表 As List(Of 网络功能.AgentSpEndpointInfo)
                         Using cts As New CancellationTokenSource(TimeSpan.FromSeconds(20))
                             端点列表 = Await 获取SPAgent端点列表数据Async(cts.Token).ConfigureAwait(False)
                         End Using

                         设置SPAgent端点获取结果(端点列表, "")
                     Catch ex As Exception
                         设置SPAgent端点获取结果(New List(Of 网络功能.AgentSpEndpointInfo), 网络功能_v6_通用.获取异常消息(ex))
                     End Try
                 End Function)
    End Sub

    Friend Shared Function 获取SPAgent端点列表() As List(Of 网络功能.AgentSpEndpointInfo)
        SyncLock SPAgent端点同步锁
            Return SPAgent端点内存列表.Select(Function(x) x.Clone()).ToList()
        End SyncLock
    End Function

    Friend Shared Sub 选择SPAgent端点(端点列表索引 As Integer)
        SyncLock SPAgent端点同步锁
            If 端点列表索引 >= 0 AndAlso 端点列表索引 < SPAgent端点内存列表.Count Then
                当前SPAgent端点索引 = 端点列表索引
            Else
                当前SPAgent端点索引 = -1
            End If
        End SyncLock
    End Sub

    Friend Shared Function 获取当前SPAgent端点() As 网络功能.AgentSpEndpointInfo
        SyncLock SPAgent端点同步锁
            If 当前SPAgent端点索引 < 0 OrElse 当前SPAgent端点索引 >= SPAgent端点内存列表.Count Then Return Nothing
            Return SPAgent端点内存列表(当前SPAgent端点索引).Clone()
        End SyncLock
    End Function

    Friend Shared Function 创建Agent端点客户端() As AgentEndpointClient
        Dim spEndpoint As 网络功能.AgentSpEndpointInfo = 获取当前SPAgent端点()
        If spEndpoint IsNot Nothing Then
            Return New AgentEndpointClient(spEndpoint.Address, spEndpoint.ApiKey, spEndpoint.ExtraHeaders, spEndpoint.ExtraBody)
        End If

        Return New AgentEndpointClient(设置_v6.实例对象.AgentEndPoint, 设置_v6.实例对象.AgentApiKey, 设置_v6.实例对象.Agent附加请求头, 设置_v6.实例对象.Agent附加请求Body)
    End Function

    Private Shared Sub 设置SPAgent端点获取结果(端点列表 As List(Of 网络功能.AgentSpEndpointInfo), 错误 As String)
        SyncLock SPAgent端点同步锁
            SPAgent端点内存列表.Clear()
            If 端点列表 IsNot Nothing Then SPAgent端点内存列表.AddRange(端点列表.Select(Function(x) x.Clone()))
            If 当前SPAgent端点索引 >= SPAgent端点内存列表.Count Then 当前SPAgent端点索引 = -1
            SPAgent端点最后错误缓存 = If(错误, "")
            SPAgent端点正在获取 = False
        End SyncLock

        刷新SPAgent端点设置界面()
    End Sub

    Private Shared Sub 刷新SPAgent端点设置界面()
        界面线程执行(
            Sub(state)
                Try
                    Form_v6_设置_Agent.刷新SPAgent端点列表()
                Catch
                End Try
            End Sub)
    End Sub

    Private Shared Function 校验SPAgent端点获取资格() As String
        If Not SP_UnLock Then Return "尚未加载 SP 解锁器。"

        Dim 解锁器路径 As String = Path.Combine(Application.StartupPath, SP解锁器文件名)
        If Not File.Exists(解锁器路径) Then Return "程序目录中不存在 SP 解锁器。"

        Dim 实际SHA256 As String = 网络功能_v6_通用.计算文件SHA256(解锁器路径)
        If Not String.Equals(实际SHA256, SP解锁器SHA256, StringComparison.OrdinalIgnoreCase) Then
            Return "SP 解锁器校验失败。"
        End If

        If Not 当前环境存在SP解锁器命名空间(解锁器路径) Then
            Return "当前运行环境未加载 SP 解锁器。"
        End If

        Return ""
    End Function

    Private Shared Function 当前环境存在SP解锁器命名空间(解锁器路径 As String) As Boolean
        Dim 标准路径 As String = Path.GetFullPath(解锁器路径)

        For Each 程序集 In AppDomain.CurrentDomain.GetAssemblies()
            Dim 程序集路径 As String = ""
            Try
                程序集路径 = 程序集.Location
            Catch
            End Try

            If String.IsNullOrWhiteSpace(程序集路径) Then Continue For
            If Not String.Equals(Path.GetFullPath(程序集路径), 标准路径, StringComparison.OrdinalIgnoreCase) Then Continue For

            For Each 类型 In 网络功能_v6_通用.安全获取程序集类型(程序集)
                If 类型 Is Nothing Then Continue For
                If String.Equals(类型.Namespace, SP解锁器命名空间, StringComparison.Ordinal) Then Return True
            Next
        Next

        Return False
    End Function

    Private Shared Async Function 获取SPAgent端点列表数据Async(cancellationToken As CancellationToken) As Task(Of List(Of 网络功能.AgentSpEndpointInfo))
        Using request As New HttpRequestMessage(HttpMethod.Get, SPAgent端点列表地址)
            request.Headers.TryAddWithoutValidation("User-Agent", $"FFmpegFreeUI/{版本号.获取自身版本号}")

            Using response = Await SPAgent端点Http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(False)
                Dim raw As String = Await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(False)
                If Not response.IsSuccessStatusCode Then
                    Throw New InvalidOperationException($"服务器返回 HTTP {CInt(response.StatusCode)}")
                End If

                Dim 端点列表 = JsonSerializer.Deserialize(Of List(Of 网络功能.AgentSpEndpointInfo))(raw, JsonSO)
                Return 清理SPAgent端点列表(端点列表)
            End Using
        End Using
    End Function

    Private Shared Function 清理SPAgent端点列表(端点列表 As IEnumerable(Of 网络功能.AgentSpEndpointInfo)) As List(Of 网络功能.AgentSpEndpointInfo)
        Dim result As New List(Of 网络功能.AgentSpEndpointInfo)
        If 端点列表 Is Nothing Then Return result

        Dim keys As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
        For Each item In 端点列表
            If item Is Nothing Then Continue For

            Dim displayName As String = If(item.DisplayName, "").Trim()
            Dim address As String = If(item.Address, "").Trim()
            If displayName = "" OrElse address = "" Then Continue For

            If Not Agent端点附加配置有效(item.ExtraHeaders, item.ExtraBody) Then Continue For

            Dim key As String = displayName & ControlChars.NullChar & address
            If Not keys.Add(key) Then Continue For

            result.Add(New 网络功能.AgentSpEndpointInfo With {
                .DisplayName = displayName,
                .Address = address,
                .ApiKey = If(item.ApiKey, "").Trim(),
                .ExtraHeaders = If(item.ExtraHeaders, ""),
                .ExtraBody = If(item.ExtraBody, "")
            })
        Next

        Return result
    End Function

    Private Shared Function Agent端点附加配置有效(extraHeaders As String, extraBody As String) As Boolean
        Try
            AgentEndpointClient.ParseAdditionalHeaders(extraHeaders)
            AgentEndpointClient.ParseExtraBody(extraBody)
            Return True
        Catch
            Return False
        End Try
    End Function

End Class
