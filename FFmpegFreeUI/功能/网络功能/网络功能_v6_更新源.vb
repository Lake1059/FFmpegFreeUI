Imports System.Threading

Friend NotInheritable Class 网络功能_v6_更新源

    Friend Const 默认本体下载线程数 As Integer = 1
    Friend Const GitHub本体下载线程数 As Integer = 10
    Friend Const 更新器下载线程数 As Integer = 4

    Private Sub New()
    End Sub

    Friend Enum 更新检查状态
        可下载
        已是最新
        失败
    End Enum

    Friend Class 更新检查结果
        Public Property 状态 As 更新检查状态
        Public Property 版本号 As String = ""
        Public Property 下载地址 As String = ""
        Public Property 下载授权Token As String = ""
        Public Property 下载线程数 As Integer = 默认本体下载线程数
        Public Property 状态标题 As String = ""
        Public Property 状态子标题 As String = ""
        Public Property 错误详情 As String = ""

        Public Shared Function 可下载(版本号 As String, 下载地址 As String, 下载线程数 As Integer, Optional 下载授权Token As String = "") As 更新检查结果
            Return New 更新检查结果 With {
                .状态 = 更新检查状态.可下载,
                .版本号 = 版本号,
                .下载地址 = 下载地址,
                .下载授权Token = 下载授权Token,
                .下载线程数 = 下载线程数
            }
        End Function

        Public Shared Function 已是最新(版本号 As String, 标题 As String, 子标题 As String) As 更新检查结果
            Return New 更新检查结果 With {
                .状态 = 更新检查状态.已是最新,
                .版本号 = 版本号,
                .状态标题 = 标题,
                .状态子标题 = 子标题
            }
        End Function

        Public Shared Function 失败(标题 As String, 子标题 As String, 错误详情 As String) As 更新检查结果
            Return New 更新检查结果 With {
                .状态 = 更新检查状态.失败,
                .状态标题 = 标题,
                .状态子标题 = 子标题,
                .错误详情 = 错误详情
            }
        End Function
    End Class

    Friend Shared Async Function 获取GitHub发布信息Async() As Task(Of LakeUI.GitHub.GitHubReleaseInfo)
        Using cts As CancellationTokenSource = 网络功能_v6_通用.创建联网请求取消源()
            Return Await LakeUI.GitHub.GetLatestReleaseAssetUrlsAsync("Lake1059", "FFmpegFreeUI", True, cts.Token)
        End Using
    End Function

    Friend Shared Function 查找GitHub发布文件下载地址(发布信息 As LakeUI.GitHub.GitHubReleaseInfo, 文件名 As String) As String
        If 发布信息?.Assets Is Nothing Then Return ""

        For Each 资源 In 发布信息.Assets
            If String.Equals(资源.FileName, 文件名, StringComparison.OrdinalIgnoreCase) Then
                Return 资源.DownloadUrl
            End If
        Next

        Return ""
    End Function

    Friend Shared Async Function 获取国内镜像源下载授权Async(资产ID As String, cancellationToken As CancellationToken) As Task(Of 网络功能.国内镜像源授权DataInfo)
        If String.IsNullOrWhiteSpace(资产ID) Then Throw New InvalidOperationException("资产 ID 为空。")

        Dim 挑战响应 As 网络功能.国内镜像源挑战数据结构 = Await LakeUI.SRV_JsonSever.PostJsonAsync(Of 网络功能.国内镜像源创建挑战请求, 网络功能.国内镜像源挑战数据结构)(
            获取国内镜像源创建挑战地址(),
            New 网络功能.国内镜像源创建挑战请求 With {.AssetId = 资产ID},
            JsonSO,
            cancellationToken)

        校验国内镜像源挑战数据(挑战响应)

        Dim Nonce As String = Await Task.Run(Function() 求解国内镜像源PowNonce(挑战响应.Data, cancellationToken), cancellationToken)
        Dim 授权响应 As 网络功能.国内镜像源授权数据结构 = Await LakeUI.SRV_JsonSever.PostJsonAsync(Of 网络功能.国内镜像源创建授权请求, 网络功能.国内镜像源授权数据结构)(
            获取国内镜像源创建授权地址(),
            New 网络功能.国内镜像源创建授权请求 With {
                .ChallengeId = 挑战响应.Data.ChallengeId,
                .AssetId = 资产ID,
                .Nonce = Nonce
            },
            JsonSO,
            cancellationToken)

        校验国内镜像源授权数据(授权响应)
        Return 授权响应.Data
    End Function

    Private Shared Sub 校验国内镜像源挑战数据(挑战响应 As 网络功能.国内镜像源挑战数据结构)
        If 挑战响应 Is Nothing Then Throw New InvalidOperationException("未获取到挑战数据。")
        If Not String.Equals(挑战响应.Status, "success", StringComparison.OrdinalIgnoreCase) Then
            Throw New InvalidOperationException(If(String.IsNullOrWhiteSpace(挑战响应.Message), "创建下载挑战失败。", 挑战响应.Message))
        End If

        If 挑战响应.Data Is Nothing OrElse
           Not String.Equals(挑战响应.Data.Algorithm, "sha256", StringComparison.OrdinalIgnoreCase) OrElse
           String.IsNullOrWhiteSpace(挑战响应.Data.ChallengeId) OrElse
           String.IsNullOrWhiteSpace(挑战响应.Data.AssetId) OrElse
           String.IsNullOrWhiteSpace(挑战响应.Data.NonceSeed) OrElse
           挑战响应.Data.LeadingZeroBits <= 0 Then
            Throw New InvalidOperationException("下载挑战数据不完整。")
        End If
    End Sub

    Private Shared Sub 校验国内镜像源授权数据(授权响应 As 网络功能.国内镜像源授权数据结构)
        If 授权响应 Is Nothing Then Throw New InvalidOperationException("未获取到下载授权。")
        If Not String.Equals(授权响应.Status, "success", StringComparison.OrdinalIgnoreCase) Then
            Throw New InvalidOperationException(If(String.IsNullOrWhiteSpace(授权响应.Message), "获取下载授权失败。", 授权响应.Message))
        End If

        If 授权响应.Data Is Nothing OrElse
           String.IsNullOrWhiteSpace(授权响应.Data.DownloadUrl) OrElse
           String.IsNullOrWhiteSpace(授权响应.Data.DownloadToken) Then
            Throw New InvalidOperationException("下载授权数据不完整。")
        End If
    End Sub

    Private Shared Function 求解国内镜像源PowNonce(挑战数据 As 网络功能.国内镜像源挑战DataInfo, cancellationToken As CancellationToken) As String
        Return LakeUI.Sha256ProofOfWork.Solve(
            Function(Nonce) 生成国内镜像源Pow规范文本(挑战数据, Nonce),
            挑战数据.LeadingZeroBits,
            cancellationToken:=cancellationToken)
    End Function

    Private Shared Function 生成国内镜像源Pow规范文本(挑战数据 As 网络功能.国内镜像源挑战DataInfo, Nonce As ULong) As String
        Return $"download.v1:{挑战数据.ChallengeId}:{挑战数据.AssetId}:{挑战数据.NonceSeed}:{Nonce}"
    End Function

    Friend Shared Function 应用GitHub代理(下载地址 As String) As String
        If 设置_v6.实例对象.更新服务器选择 <> 1 Then Return 下载地址
        Return "https://cdn.gh-proxy.org/" & 下载地址
    End Function

    Friend Shared Async Function 获取MirrorChyan数据Async(请求地址 As String) As Task(Of 网络功能.MirrorChyan数据结构)
        Try
            Using cts As CancellationTokenSource = 网络功能_v6_通用.创建联网请求取消源()
                Return Await LakeUI.SRV_JsonSever.GetJsonAsync(Of 网络功能.MirrorChyan数据结构)(请求地址, cancellationToken:=cts.Token)
            End Using
        Catch ex As Exception
            Return New 网络功能.MirrorChyan数据结构 With {
                .Code = -1,
                .Msg = 网络功能_v6_通用.获取异常消息(ex)
            }
        End Try
    End Function

    Friend Shared Function 校验MirrorChyan数据(镜像数据 As 网络功能.MirrorChyan数据结构, 错误标题 As String) As 更新检查结果
        If 镜像数据.Code <> 0 Then
            Return 更新检查结果.失败(错误标题, "点此查看详情", 镜像数据.Msg)
        End If

        If 镜像数据.Data Is Nothing OrElse String.IsNullOrWhiteSpace(镜像数据.Data.Url) Then
            Return 更新检查结果.失败(错误标题, "未获取到有效数据", "未获取到有效数据")
        End If

        Return Nothing
    End Function

    Friend Shared Function 国内镜像源数据有效(镜像数据 As 网络功能.国内镜像源数据结构) As Boolean
        Return 镜像数据 IsNot Nothing AndAlso
               String.Equals(镜像数据.Status, "success", StringComparison.OrdinalIgnoreCase) AndAlso
               镜像数据.Data IsNot Nothing AndAlso
               镜像数据.Data.Assets IsNot Nothing AndAlso
               镜像数据.Data.Assets.Length > 0
    End Function

    Friend Shared Function 获取国内镜像源版本号(镜像数据 As 网络功能.国内镜像源数据结构) As String
        For Each 资源 As 网络功能.国内镜像源AssetInfo In 镜像数据.Data.Assets
            If 资源 Is Nothing OrElse String.IsNullOrWhiteSpace(资源.Version) Then Continue For
            Return 资源.Version
        Next

        Return ""
    End Function

    Friend Shared Function 查找国内镜像下载资源(镜像数据 As 网络功能.国内镜像源数据结构, 文件名 As String) As 网络功能.国内镜像源AssetInfo
        For Each 资源 As 网络功能.国内镜像源AssetInfo In 镜像数据.Data.Assets
            If 资源 Is Nothing OrElse Not 资源.Available Then Continue For
            If Not String.Equals(资源.FileName, 文件名, StringComparison.OrdinalIgnoreCase) Then Continue For

            Return 资源
        Next

        Dim 当前架构 As String = 程序架构.获取自身程序架构
        For Each 资源 As 网络功能.国内镜像源AssetInfo In 镜像数据.Data.Assets
            If 资源 Is Nothing OrElse Not 资源.Available Then Continue For
            If String.Equals(资源.Architecture, 当前架构, StringComparison.OrdinalIgnoreCase) Then Return 资源
        Next

        Return Nothing
    End Function

    Friend Shared Function 获取本体更新文件名() As String
        Return $"FFmpegFreeUI.{程序架构.获取自身程序架构}.exe"
    End Function

    Friend Shared Function 获取国内镜像源本体更新地址() As String
        Return "https://fengyuan.frostlynx.work/api/public/v1/projects/FFmpegFreeUI/assets"
    End Function

    Private Shared Function 获取国内镜像源创建挑战地址() As String
        Return "https://fengyuan.frostlynx.work/api/public/v1/api/challenges"
    End Function

    Private Shared Function 获取国内镜像源创建授权地址() As String
        Return "https://fengyuan.frostlynx.work/api/public/v1/api/authorizations"
    End Function

    Friend Shared Function 获取MirrorChyan本体更新地址() As String
        Return $"https://mirrorchyan.com/api/resources/FFmpegFreeUI/latest?os=win&arch={程序架构.获取自身程序架构}&channel=beta&cdk={设置_v6.实例对象.MirrorChyanCDK}"
    End Function

    Friend Shared Function 获取MirrorChyan更新器地址() As String
        Return $"https://mirrorchyan.com/api/resources/FFmpegFreeUI-Updater/latest?os=win&channel=beta&cdk={设置_v6.实例对象.MirrorChyanCDK}"
    End Function

    Friend Shared Function 获取更新服务器名称() As String
        Select Case 设置_v6.实例对象.更新服务器选择
            Case 0 : Return "GitHub"
            Case 1 : Return "gh-proxy.com"
            Case 2 : Return "枫源镜像"
            Case 3 : Return "Mirror酱"
            Case Else : Return $"未知服务器 {设置_v6.实例对象.更新服务器选择}"
        End Select
    End Function

End Class
