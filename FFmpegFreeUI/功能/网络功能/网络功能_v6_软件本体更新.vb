Imports System.IO
Imports System.Threading

Friend NotInheritable Class 网络功能_v6_软件本体更新

    Friend Shared Property 当前是否正在进行本体更新 As Boolean = False
    Friend Shared Property 检查软件本体更新最后一次错误 As String = ""
    Friend Shared ReadOnly Property 检查软件本体更新下载位置 As String = Path.Combine(Application.StartupPath, "FFmpegFreeUI_update.exe")

    Private Sub New()
    End Sub

    Friend Shared Async Sub 检查软件本体更新()
        If Not 可以开始检查本体更新() Then Exit Sub

        标记本体更新检查开始()

        Try
            Dim 检查结果 As 网络功能_v6_更新源.更新检查结果 = Await 获取本体更新信息Async()
            If Not 处理本体更新检查结果(检查结果) Then Exit Sub
            If Not 清理旧本体更新文件() Then Exit Sub

            Await 下载软件本体更新Async(检查结果)
        Catch ex As Exception
            设置本体更新失败("检查软件本体更新失败", "点此查看详情", 网络功能_v6_通用.获取异常消息(ex))
        End Try
    End Sub

    Private Shared Function 可以开始检查本体更新() As Boolean
        If 当前是否正在进行本体更新 Then Return False

        If Not My.Computer.Network.IsAvailable Then
            Form_v6_起始页面.MB_软件本体更新.Text = $"[无网络] 3FUI 本地版本 {版本号.获取自身版本号}"
            Form_v6_起始页面.MB_软件本体更新.SubText = "请联网后重试"
            Return False
        End If

        Return True
    End Function

    Private Shared Sub 标记本体更新检查开始()
        Form_v6_起始页面.MB_软件本体更新.Text = "正在检查本体更新 ..."
        Form_v6_起始页面.MB_软件本体更新.SubText = $"连接到更新服务器：{网络功能_v6_更新源.获取更新服务器名称()}"
        当前是否正在进行本体更新 = True
    End Sub

    Private Shared Async Function 获取本体更新信息Async() As Task(Of 网络功能_v6_更新源.更新检查结果)
        Select Case 设置_v6.实例对象.更新服务器选择
            Case 0, 1
                Return Await 从GitHub获取本体更新信息Async()
            Case 2
                Return Await 从国内镜像源获取本体更新信息Async()
            Case 3
                Return Await 从MirrorChyan获取本体更新信息Async()
            Case Else
                Return 网络功能_v6_更新源.更新检查结果.失败("软件本体更新失败", "获取到的下载地址为空", "获取到的下载地址为空")
        End Select
    End Function

    Private Shared Async Function 从GitHub获取本体更新信息Async() As Task(Of 网络功能_v6_更新源.更新检查结果)
        Dim 发布信息 As LakeUI.GitHub.GitHubReleaseInfo

        Try
            发布信息 = Await 网络功能_v6_更新源.获取GitHub发布信息Async()
        Catch ex As Exception
            Return 网络功能_v6_更新源.更新检查结果.失败("检查软件本体更新失败", "点此查看详情", ex.Message)
        End Try

        If 发布信息 Is Nothing OrElse Not 发布信息.IsSuccess Then
            Return 网络功能_v6_更新源.更新检查结果.失败("检查软件本体更新失败", "点此查看详情", If(发布信息?.ErrorMessage, "未知错误"))
        End If

        If 版本号.CompareVersion(发布信息.TagName, 版本号.获取自身版本号) <= 0 Then
            Return 网络功能_v6_更新源.更新检查结果.已是最新(
                发布信息.TagName,
                $"[{网络功能_v6_更新源.获取更新服务器名称()}] 3FUI 云端版本 {发布信息.TagName}",
                "已是最新版本")
        End If

        Dim 文件名 As String = 网络功能_v6_更新源.获取本体更新文件名()
        Dim 下载地址 As String = 网络功能_v6_更新源.查找GitHub发布文件下载地址(发布信息, 文件名)
        If String.IsNullOrWhiteSpace(下载地址) Then
            Return 网络功能_v6_更新源.更新检查结果.失败("检查软件本体更新失败", "服务器上没有对应的文件", $"发行版 {发布信息.TagName} 中没有对应的文件：{文件名}")
        End If

        Return 网络功能_v6_更新源.更新检查结果.可下载(发布信息.TagName, 网络功能_v6_更新源.应用GitHub代理(下载地址), 网络功能_v6_更新源.GitHub本体下载线程数)
    End Function

    Private Shared Async Function 从国内镜像源获取本体更新信息Async() As Task(Of 网络功能_v6_更新源.更新检查结果)
        Dim 镜像数据 As 网络功能.国内镜像源数据结构

        Try
            Using cts As CancellationTokenSource = 网络功能_v6_通用.创建联网请求取消源()
                镜像数据 = Await LakeUI.SRV_JsonSever.GetJsonAsync(Of 网络功能.国内镜像源数据结构)(网络功能_v6_更新源.获取国内镜像源本体更新地址(), JsonSO, cts.Token, "_ffui-update")
            End Using
        Catch ex As Exception
            Return 网络功能_v6_更新源.更新检查结果.失败("检查软件本体更新失败", "点此查看详情", 网络功能_v6_通用.获取异常消息(ex))
        End Try

        If Not 网络功能_v6_更新源.国内镜像源数据有效(镜像数据) Then
            Dim 错误详情 As String = If(镜像数据 IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(镜像数据.Message), 镜像数据.Message, "未获取到有效数据")
            Return 网络功能_v6_更新源.更新检查结果.失败("检查软件本体更新失败", "未获取到有效数据", 错误详情)
        End If

        Dim 云端版本号 As String = 网络功能_v6_更新源.获取国内镜像源版本号(镜像数据)
        If String.IsNullOrWhiteSpace(云端版本号) Then
            Return 网络功能_v6_更新源.更新检查结果.失败("检查软件本体更新失败", "未获取到有效数据", "未获取到版本号")
        End If

        If 版本号.CompareVersion(云端版本号, 版本号.获取自身版本号) <= 0 Then
            Return 网络功能_v6_更新源.更新检查结果.已是最新(
                云端版本号,
                $"[{网络功能_v6_更新源.获取更新服务器名称()}] 3FUI 云端版本 {云端版本号}",
                "已是最新版本")
        End If

        Dim 下载资源 As 网络功能.国内镜像源AssetInfo = 网络功能_v6_更新源.查找国内镜像下载资源(镜像数据, 网络功能_v6_更新源.获取本体更新文件名())
        If 下载资源 Is Nothing Then
            Return 网络功能_v6_更新源.更新检查结果.失败("软件本体更新失败", "服务器上没有对应的文件", $"镜像源中没有对应的文件：{网络功能_v6_更新源.获取本体更新文件名()}")
        End If

        Form_v6_起始页面.MB_软件本体更新.SubText = "正在完成下载授权挑战 ..."

        Dim 下载授权 As 网络功能.国内镜像源授权DataInfo
        Try
            Using cts As CancellationTokenSource = 网络功能_v6_通用.创建联网请求取消源()
                下载授权 = Await 网络功能_v6_更新源.获取国内镜像源下载授权Async(下载资源.AssetId, cts.Token)
            End Using
        Catch ex As Exception
            Return 网络功能_v6_更新源.更新检查结果.失败("软件本体更新失败", "点此查看详情", 网络功能_v6_通用.获取异常消息(ex))
        End Try

        If 下载授权 Is Nothing OrElse String.IsNullOrWhiteSpace(下载授权.DownloadUrl) OrElse String.IsNullOrWhiteSpace(下载授权.DownloadToken) Then
            Return 网络功能_v6_更新源.更新检查结果.失败("软件本体更新失败", "获取到的下载地址为空", "获取到的下载地址为空")
        End If

        Dim 下载线程数 As Integer = 网络功能_v6_通用.获取有效下载线程数(下载授权.RangeConcurrencyLimit, 网络功能_v6_更新源.默认本体下载线程数)
        Return 网络功能_v6_更新源.更新检查结果.可下载(云端版本号, 下载授权.DownloadUrl, 下载线程数, 下载授权.DownloadToken)
    End Function

    Private Shared Async Function 从MirrorChyan获取本体更新信息Async() As Task(Of 网络功能_v6_更新源.更新检查结果)
        Dim 镜像数据 As 网络功能.MirrorChyan数据结构 = Await 网络功能_v6_更新源.获取MirrorChyan数据Async(网络功能_v6_更新源.获取MirrorChyan本体更新地址())
        If 镜像数据 Is Nothing Then
            Return 网络功能_v6_更新源.更新检查结果.失败("检查软件本体更新失败", "未获取到有效数据", "未获取到有效数据")
        End If

        Dim 失败结果 As 网络功能_v6_更新源.更新检查结果 = 网络功能_v6_更新源.校验MirrorChyan数据(镜像数据, "检查软件本体更新失败")
        If 失败结果 IsNot Nothing Then Return 失败结果

        If 版本号.CompareVersion(镜像数据.Data.VersionName, 版本号.获取自身版本号) <= 0 Then
            Return 网络功能_v6_更新源.更新检查结果.已是最新(
                镜像数据.Data.VersionName,
                $"[{网络功能_v6_更新源.获取更新服务器名称()}] 3FUI {镜像数据.Data.VersionName}",
                "已是最新版本")
        End If

        Return 网络功能_v6_更新源.更新检查结果.可下载(镜像数据.Data.VersionName, 镜像数据.Data.Url, 网络功能_v6_更新源.默认本体下载线程数)
    End Function

    Private Shared Function 处理本体更新检查结果(检查结果 As 网络功能_v6_更新源.更新检查结果) As Boolean
        If 检查结果 Is Nothing Then
            设置本体更新失败("检查软件本体更新失败", "未获取到有效数据", "未获取到有效数据")
            Return False
        End If

        Select Case 检查结果.状态
            Case 网络功能_v6_更新源.更新检查状态.失败
                设置本体更新失败(检查结果.状态标题, 检查结果.状态子标题, 检查结果.错误详情)
                Return False
            Case 网络功能_v6_更新源.更新检查状态.已是最新
                显示本体已是最新(检查结果.状态标题, 检查结果.状态子标题)
                Return False
            Case 网络功能_v6_更新源.更新检查状态.可下载
                If String.IsNullOrWhiteSpace(检查结果.下载地址) Then
                    设置本体更新失败("软件本体更新失败", "获取到的下载地址为空", "获取到的下载地址为空")
                    Return False
                End If
        End Select

        Return True
    End Function

    Private Shared Sub 显示本体已是最新(标题 As String, 子标题 As String)
        当前是否正在进行本体更新 = False
        Form_v6_起始页面.MB_软件本体更新.Text = 标题
        Form_v6_起始页面.MB_软件本体更新.SubText = 子标题
    End Sub

    Private Shared Function 清理旧本体更新文件() As Boolean
        Try
            If FileIO.FileSystem.FileExists(检查软件本体更新下载位置) Then FileIO.FileSystem.DeleteFile(检查软件本体更新下载位置)
            Return True
        Catch ex As Exception
            设置本体更新失败("软件本体更新失败", "无法清理旧更新文件", ex.Message)
            Return False
        End Try
    End Function

    Private Shared Async Function 下载软件本体更新Async(检查结果 As 网络功能_v6_更新源.更新检查结果) As Task
        Dim 下载器 As LakeUI.DownloadFile = 网络功能_v6_通用.创建下载器(
            检查结果.下载地址,
            检查软件本体更新下载位置,
            检查结果.下载线程数,
            网络功能_v6_更新源.默认本体下载线程数,
            检查结果.下载授权Token)

        Form_v6_起始页面.MB_软件本体更新.Text = $"正在下载本体更新 {检查结果.版本号}"
        绑定本体下载事件(下载器, 检查结果.版本号)

        Try
            Await 网络功能_v6_通用.启动下载并监控Async(下载器)
        Catch ex As Exception
            设置本体更新失败("下载软件本体更新失败", "点此查看详情", 网络功能_v6_通用.获取异常消息(ex))
        End Try
    End Function

    Private Shared Sub 绑定本体下载事件(下载器 As LakeUI.DownloadFile, 版本号文本 As String)
        网络功能_v6_通用.绑定下载进度事件(
            下载器,
            Sub(进度文本) Form_v6_起始页面.MB_软件本体更新.SubText = 进度文本,
            Sub(错误消息) 设置本体更新失败("下载软件本体更新失败", "点此查看详情", 错误消息))

        AddHandler 下载器.DownloadCompleted,
            Sub(s, e)
                UpdateAvailable = True
                Form_v6_起始页面.MB_软件本体更新.Text = $"3FUI 更新 {版本号文本} 已准备就绪"
                Form_v6_起始页面.MB_软件本体更新.SubText = "关闭软件后自动开始更新"
                当前是否正在进行本体更新 = False
                LakeUI.ExOverlayMsgBox(FormMain_v6, "关闭此实例以开始更新，如果还有其他实例在运行，更新控制台会等待所有实例关闭之后再进行更新，关闭此实例不影响其他实例触发更新控制台", MsgBoxStyle.Information, $"3FUI 更新 {版本号文本} 已准备就绪")
            End Sub
    End Sub

    Private Shared Sub 设置本体更新失败(标题 As String, 子标题 As String, 错误 As String)
        当前是否正在进行本体更新 = False
        Form_v6_起始页面.MB_软件本体更新.Text = 标题
        Form_v6_起始页面.MB_软件本体更新.SubText = 子标题
        检查软件本体更新最后一次错误 = 错误
    End Sub

End Class
