Imports System.IO
Imports System.Text.Json
Imports System.Text.Json.Serialization

Public Class 网络功能

#Region "新闻列表"

    Public Shared Property 当前是否正在进行获取新闻列表 As Boolean = False

    Public Class 新闻单片数据类
        Public Property Title As String
        Public Property TitleColor As String
        Public Property SubTitle As String
        Public Property Type As String
        Public Property Body As String
    End Class

    Public Shared Async Sub 获取新闻列表()
        If Not 可以开始获取新闻列表() Then Exit Sub

        清空新闻列表()
        当前是否正在进行获取新闻列表 = True

        Dim 新闻列表 As List(Of 新闻单片数据类) = Await 获取新闻列表数据Async()
        If 新闻列表 Is Nothing Then Exit Sub

        添加新闻列表到界面(新闻列表)
        当前是否正在进行获取新闻列表 = False
    End Sub

    Public Shared Sub 创建一个新闻内容(标题 As String, 标题颜色 As String, 副标题 As String, 行为 As String, 内容 As String)
        Dim 按钮 As LakeUI.ModernButton = 创建新闻按钮(标题, 副标题)

        设置新闻按钮高度(按钮, 副标题)
        按钮.ForeColor = 获取新闻标题颜色(标题颜色)
        绑定新闻按钮行为(按钮, 行为, 内容)
        添加新闻按钮到界面(按钮)
    End Sub

    Public Shared Sub 设置新闻列表获取失败(原因 As String)
        Dim 错误标签 As New Label With {
            .Text = 原因,
            .BackColor = Color.Transparent,
            .Dock = DockStyle.Fill,
            .TextAlign = ContentAlignment.MiddleCenter,
            .AutoSize = False
        }

        Form_v6_起始页面.MP_新闻列表.Controls.Add(错误标签)
        当前是否正在进行获取新闻列表 = False
    End Sub

    Private Shared Function 可以开始获取新闻列表() As Boolean
        If 当前是否正在进行获取新闻列表 Then Return False

        If Not My.Computer.Network.IsAvailable Then
            Form_v6_起始页面.HtmlColorLabel6.Text = "[无网络] 新闻列表"
            Return False
        End If

        Return True
    End Function

    Private Shared Sub 清空新闻列表()
        For index As Integer = Form_v6_起始页面.MP_新闻列表.Controls.Count - 1 To 0 Step -1
            Dim 控件 As Control = Form_v6_起始页面.MP_新闻列表.Controls(index)
            If 控件 Is Form_v6_起始页面.HtmlColorLabel6 Then Continue For

            Form_v6_起始页面.MP_新闻列表.Controls.RemoveAt(index)
            控件.Dispose()
        Next
    End Sub

    Private Shared Async Function 获取新闻列表数据Async() As Task(Of List(Of 新闻单片数据类))
        Dim 原始数据 As String

        Try
            原始数据 = Await LakeUI.GitHub.GetFileTextAsync("Lake1059", "FFmpegFreeUI", "News.json")
        Catch ex As Exception
            设置新闻列表获取失败(ex.Message)
            Return Nothing
        End Try

        If String.IsNullOrWhiteSpace(原始数据) Then
            设置新闻列表获取失败("返回了空数据")
            Return Nothing
        End If

        Try
            Dim 新闻列表 As List(Of 新闻单片数据类) = JsonSerializer.Deserialize(Of List(Of 新闻单片数据类))(原始数据)
            If 新闻列表 Is Nothing Then 设置新闻列表获取失败("无法转换数据")
            Return 新闻列表
        Catch ex As Exception
            设置新闻列表获取失败(ex.Message)
            Return Nothing
        End Try
    End Function

    Private Shared Sub 添加新闻列表到界面(新闻列表 As IEnumerable(Of 新闻单片数据类))
        For Each 新闻 As 新闻单片数据类 In 新闻列表
            创建一个新闻内容(新闻.Title, 新闻.TitleColor, 新闻.SubTitle, 新闻.Type, 新闻.Body)
        Next
    End Sub

    Private Shared Function 创建新闻按钮(标题 As String, 副标题 As String) As LakeUI.ModernButton
        Return New LakeUI.ModernButton With {
            .BackColor = Color.Transparent,
            .BackColor1 = Color.Transparent,
            .BorderSize = 0,
            .HoverBackColor1 = Color.FromArgb(40, 220, 220, 220),
            .PressedBackColor1 = Color.FromArgb(60, 220, 220, 220),
            .Text = 标题,
            .SubText = 副标题,
            .SubTextForeColor = Color.DarkGray,
            .BorderRadius = 10,
            .Font = New Font(设置_v6.实例对象.字体, 10),
            .SubTextSize = 9,
            .Dock = DockStyle.Top,
            .TextAlign = LakeUI.ModernButton.TextAlignEnum.Left,
            .AnimationDuration = 0
        }
    End Function

    Private Shared Sub 设置新闻按钮高度(按钮 As LakeUI.ModernButton, 副标题 As String)
        Dim 基础高度 As Integer = If(String.IsNullOrWhiteSpace(副标题), 30, 50)
        按钮.Height = 基础高度 * FormMain_v6.DeviceDpi / 96
    End Sub

    Private Shared Function 获取新闻标题颜色(标题颜色 As String) As Color
        Select Case If(标题颜色, "").ToLower().Trim()
            Case "red"
                Return Color.IndianRed
            Case "orange"
                Return Color.Peru
            Case "yellow"
                Return Color.Gold
            Case "green"
                Return Color.YellowGreen
            Case "blue"
                Return Color.CornflowerBlue
            Case "purple"
                Return Color.MediumPurple
            Case Else
                Return Color.Silver
        End Select
    End Function

    Private Shared Sub 绑定新闻按钮行为(按钮 As LakeUI.ModernButton, 行为 As String, 内容 As String)
        Select Case If(行为, "").ToLower().Trim()
            Case "msgbox"
                AddHandler 按钮.Click, Sub() LakeUI.ExOverlayMsgBox(FormMain_v6, 内容, MsgBoxStyle.OkOnly)
            Case "link"
                AddHandler 按钮.Click, Sub() 打开链接(内容)
        End Select
    End Sub

    Private Shared Sub 打开链接(链接 As String)
        If String.IsNullOrWhiteSpace(链接) Then Exit Sub
        Process.Start(New ProcessStartInfo With {.FileName = 链接, .UseShellExecute = True})
    End Sub

    Private Shared Sub 添加新闻按钮到界面(按钮 As LakeUI.ModernButton)
        Form_v6_起始页面.MP_新闻列表.Controls.Add(按钮)
        按钮.BringToFront()
    End Sub

#End Region

#Region "更新数据结构"

    Private Const 默认本体下载线程数 As Integer = 1
    Private Const GitHub本体下载线程数 As Integer = 10
    Private Const 更新器下载线程数 As Integer = 4

    Public Class 国内镜像源数据结构
        <JsonPropertyName("release")>
        Public Property Release As ReleaseInfo

        <JsonPropertyName("artifact")>
        Public Property Artifact As ArtifactInfo

        Public Class ReleaseInfo
            <JsonPropertyName("version")>
            Public Property Version As String
        End Class

        Public Class ArtifactInfo
            <JsonPropertyName("sources")>
            Public Property Sources As SourceInfo()
        End Class
    End Class

    Public Class SourceInfo
        <JsonPropertyName("name")>
        Public Property Name As String

        <JsonPropertyName("threads")>
        <JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)>
        Public Property Threads As Integer

        <JsonPropertyName("url")>
        Public Property Url As String
    End Class

    Public Class MirrorChyan数据结构
        <JsonPropertyName("code")>
        <JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)>
        Public Property Code As Integer

        <JsonPropertyName("msg")>
        Public Property Msg As String

        <JsonPropertyName("data")>
        Public Property Data As MirrorChyanData
    End Class

    Public Class MirrorChyanData
        <JsonPropertyName("version_name")>
        Public Property VersionName As String

        <JsonPropertyName("url")>
        Public Property Url As String
    End Class

    Private Enum 更新检查状态
        可下载
        已是最新
        失败
    End Enum

    Private Class 更新检查结果
        Public Property 状态 As 更新检查状态
        Public Property 版本号 As String = ""
        Public Property 下载地址 As String = ""
        Public Property 下载线程数 As Integer = 默认本体下载线程数
        Public Property 状态标题 As String = ""
        Public Property 状态子标题 As String = ""
        Public Property 错误详情 As String = ""

        Public Shared Function 可下载(版本号 As String, 下载地址 As String, 下载线程数 As Integer) As 更新检查结果
            Return New 更新检查结果 With {
                .状态 = 更新检查状态.可下载,
                .版本号 = 版本号,
                .下载地址 = 下载地址,
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

#End Region

#Region "软件本体更新"

    Public Shared Property 当前是否正在进行本体更新 As Boolean = False
    Public Shared Property 检查软件本体更新最后一次错误 As String = ""
    Public Shared ReadOnly Property 检查软件本体更新下载位置 As String = Path.Combine(Application.StartupPath, "FFmpegFreeUI_update.exe")

    Public Shared Async Sub 检查软件本体更新()
        If Not 可以开始检查本体更新() Then Exit Sub

        标记本体更新检查开始()

        Dim 检查结果 As 更新检查结果 = Await 获取本体更新信息Async()
        If Not 处理本体更新检查结果(检查结果) Then Exit Sub
        If Not 清理旧本体更新文件() Then Exit Sub

        Await 下载软件本体更新Async(检查结果)
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
        Form_v6_起始页面.MB_软件本体更新.SubText = $"连接到更新服务器：{获取更新服务器名称()}"
        当前是否正在进行本体更新 = True
    End Sub

    Private Shared Async Function 获取本体更新信息Async() As Task(Of 更新检查结果)
        Select Case 设置_v6.实例对象.更新服务器选择
            Case 0, 1
                Return Await 从GitHub获取本体更新信息Async()
            Case 2
                Return Await 从国内镜像源获取本体更新信息Async()
            Case 3
                Return Await 从MirrorChyan获取本体更新信息Async()
            Case Else
                Return 更新检查结果.失败("软件本体更新失败", "获取到的下载地址为空", "获取到的下载地址为空")
        End Select
    End Function

    Private Shared Async Function 从GitHub获取本体更新信息Async() As Task(Of 更新检查结果)
        Dim 发布信息 As LakeUI.GitHub.GitHubReleaseInfo

        Try
            发布信息 = Await 获取GitHub发布信息Async()
        Catch ex As Exception
            Return 更新检查结果.失败("检查软件本体更新失败", "点此查看详情", ex.Message)
        End Try

        If 发布信息 Is Nothing OrElse Not 发布信息.IsSuccess Then
            Return 更新检查结果.失败("检查软件本体更新失败", "点此查看详情", If(发布信息?.ErrorMessage, "未知错误"))
        End If

        If 版本号.CompareVersion(发布信息.TagName, 版本号.获取自身版本号) <= 0 Then
            Return 更新检查结果.已是最新(
                发布信息.TagName,
                $"[{获取更新服务器名称()}] 3FUI 云端版本 {发布信息.TagName}",
                "已是最新版本")
        End If

        Dim 文件名 As String = 获取本体更新文件名()
        Dim 下载地址 As String = 查找GitHub发布文件下载地址(发布信息, 文件名)
        If String.IsNullOrWhiteSpace(下载地址) Then
            Return 更新检查结果.失败("检查软件本体更新失败", "服务器上没有对应的文件", $"发行版 {发布信息.TagName} 中没有对应的文件：{文件名}")
        End If

        Return 更新检查结果.可下载(发布信息.TagName, 应用GitHub代理(下载地址), GitHub本体下载线程数)
    End Function

    Private Shared Async Function 从国内镜像源获取本体更新信息Async() As Task(Of 更新检查结果)
        Dim 镜像数据 As 国内镜像源数据结构

        Try
            镜像数据 = Await LakeUI.SRV_JsonSever.GetJsonAsync(Of 国内镜像源数据结构)(获取国内镜像源本体更新地址(), JsonSO, Nothing, "_ffui-update")
        Catch ex As Exception
            Return 更新检查结果.失败("检查软件本体更新失败", "点此查看详情", ex.Message)
        End Try

        If Not 国内镜像源数据有效(镜像数据) Then
            Return 更新检查结果.失败("检查软件本体更新失败", "未获取到有效数据", "未获取到有效数据")
        End If

        If 版本号.CompareVersion(镜像数据.Release.Version, 版本号.获取自身版本号) <= 0 Then
            Return 更新检查结果.已是最新(
                镜像数据.Release.Version,
                $"[{获取更新服务器名称()}] 3FUI 镜像云版本 {镜像数据.Release.Version}",
                "已是最新版本")
        End If

        Dim 下载源 As SourceInfo = 查找国内镜像下载源(镜像数据, "FrostLynx")
        If 下载源 Is Nothing OrElse String.IsNullOrWhiteSpace(下载源.Url) Then
            Return 更新检查结果.失败("软件本体更新失败", "获取到的下载地址为空", "获取到的下载地址为空")
        End If

        Return 更新检查结果.可下载(镜像数据.Release.Version, 下载源.Url, 获取有效下载线程数(下载源.Threads, 默认本体下载线程数))
    End Function

    Private Shared Async Function 从MirrorChyan获取本体更新信息Async() As Task(Of 更新检查结果)
        Dim 镜像数据 As MirrorChyan数据结构 = Await 获取MirrorChyan数据Async(获取MirrorChyan本体更新地址())
        If 镜像数据 Is Nothing Then
            Return 更新检查结果.失败("检查软件本体更新失败", "未获取到有效数据", "未获取到有效数据")
        End If

        Dim 失败结果 As 更新检查结果 = 校验MirrorChyan数据(镜像数据, "检查软件本体更新失败")
        If 失败结果 IsNot Nothing Then Return 失败结果

        If 版本号.CompareVersion(镜像数据.Data.VersionName, 版本号.获取自身版本号) <= 0 Then
            Return 更新检查结果.已是最新(
                镜像数据.Data.VersionName,
                $"[{获取更新服务器名称()}] 3FUI {镜像数据.Data.VersionName}",
                "已是最新版本")
        End If

        Return 更新检查结果.可下载(镜像数据.Data.VersionName, 镜像数据.Data.Url, 默认本体下载线程数)
    End Function

    Private Shared Function 处理本体更新检查结果(检查结果 As 更新检查结果) As Boolean
        If 检查结果 Is Nothing Then
            设置本体更新失败("检查软件本体更新失败", "未获取到有效数据", "未获取到有效数据")
            Return False
        End If

        Select Case 检查结果.状态
            Case 更新检查状态.失败
                设置本体更新失败(检查结果.状态标题, 检查结果.状态子标题, 检查结果.错误详情)
                Return False
            Case 更新检查状态.已是最新
                显示本体已是最新(检查结果.状态标题, 检查结果.状态子标题)
                Return False
            Case 更新检查状态.可下载
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

    Private Shared Async Function 下载软件本体更新Async(检查结果 As 更新检查结果) As Task
        Dim 下载器 As New LakeUI.DownloadFile With {
            .Url = 检查结果.下载地址,
            .SavePath = 检查软件本体更新下载位置,
            .ThreadCount = 获取有效下载线程数(检查结果.下载线程数, 默认本体下载线程数)
        }

        Form_v6_起始页面.MB_软件本体更新.Text = $"正在下载本体更新 {检查结果.版本号}"
        绑定本体下载事件(下载器, 检查结果.版本号)

        Try
            Await 下载器.StartAsync()
        Catch ex As Exception
            设置本体更新失败("下载软件本体更新失败", "点此查看详情", ex.Message)
        End Try
    End Function

    Private Shared Sub 绑定本体下载事件(下载器 As LakeUI.DownloadFile, 版本号文本 As String)
        AddHandler 下载器.ProgressChanged,
            Sub(s, e)
                Form_v6_起始页面.MB_软件本体更新.SubText = 格式化下载进度(e.BytesDownloaded, e.TotalBytes, e.SpeedBytesPerSecond, e.EstimatedTimeRemaining)
            End Sub

        AddHandler 下载器.DownloadCompleted,
            Sub(s, e)
                UpdateAvailable = True
                Form_v6_起始页面.MB_软件本体更新.Text = $"3FUI 更新 {版本号文本} 已准备就绪"
                Form_v6_起始页面.MB_软件本体更新.SubText = "关闭软件后自动开始更新"
                当前是否正在进行本体更新 = False
                LakeUI.ExOverlayMsgBox(FormMain_v6, "关闭此实例以开始更新，如果还有其他实例在运行，更新控制台会等待所有实例关闭之后再进行更新，关闭此实例不影响其他实例触发更新控制台", MsgBoxStyle.Information, $"3FUI 更新 {版本号文本} 已准备就绪")
            End Sub

        AddHandler 下载器.DownloadFailed,
            Sub(s, e)
                设置本体更新失败("下载软件本体更新失败", "点此查看详情", e.Message)
            End Sub
    End Sub

    Private Shared Sub 设置本体更新失败(标题 As String, 子标题 As String, 错误 As String)
        当前是否正在进行本体更新 = False
        Form_v6_起始页面.MB_软件本体更新.Text = 标题
        Form_v6_起始页面.MB_软件本体更新.SubText = 子标题
        检查软件本体更新最后一次错误 = 错误
    End Sub

#End Region

#Region "更新器更新"

    Public Shared Property 当前是否正在进行更新器更新 As Boolean = False
    Public Shared Property 检查更新器更新最后一次错误 As String = ""
    Public Shared ReadOnly Property 检查更新器更新下载位置 As String = Path.Combine(Application.StartupPath, "Updater.exe")

    Public Shared Async Sub 检查更新器更新(Optional 强制更新 As Boolean = False)
        If Not 可以开始检查更新器更新(强制更新) Then Exit Sub

        标记更新器更新检查开始()

        Dim 检查结果 As 更新检查结果 = Await 获取更新器更新信息Async()
        If Not 处理更新器更新检查结果(检查结果) Then Exit Sub
        If Not 清理旧更新器文件() Then Exit Sub

        Await 下载更新器Async(检查结果.下载地址)
    End Sub

    Private Shared Function 可以开始检查更新器更新(强制更新 As Boolean) As Boolean
        If 当前是否正在进行更新器更新 Then Return False

        If Not 强制更新 AndAlso FileIO.FileSystem.FileExists(检查更新器更新下载位置) Then
            显示本地更新器状态()
            Return False
        End If

        If Not My.Computer.Network.IsAvailable Then
            Form_v6_起始页面.MB_更新器更新.Text = "[无网络] 控制台更新程序"
            Form_v6_起始页面.MB_更新器更新.SubText = "请联网后重试"
            Return False
        End If

        Return True
    End Function

    Private Shared Sub 显示本地更新器状态()
        Dim 本地版本号 As String = 版本号.获取外部程序文件版本号(检查更新器更新下载位置)
        Form_v6_起始页面.MB_更新器更新.Text = $"控制台更新程序 {If(String.IsNullOrWhiteSpace(本地版本号), "本地版本未知", 本地版本号)}"
        Form_v6_起始页面.MB_更新器更新.SubText = "已安装，此组件为手动更新"
    End Sub

    Private Shared Sub 标记更新器更新检查开始()
        Form_v6_起始页面.MB_更新器更新.Text = "正在检查更新器更新 ..."
        Form_v6_起始页面.MB_更新器更新.SubText = $"连接到更新服务器：{获取更新服务器名称()}"
        当前是否正在进行更新器更新 = True
    End Sub

    Private Shared Async Function 获取更新器更新信息Async() As Task(Of 更新检查结果)
        Select Case 设置_v6.实例对象.更新服务器选择
            Case 0, 1, 2
                Return Await 从GitHub获取更新器更新信息Async()
            Case 3
                Return Await 从MirrorChyan获取更新器更新信息Async()
            Case Else
                Return 更新检查结果.失败("更新器更新失败", "获取到的下载地址为空", "获取到的下载地址为空")
        End Select
    End Function

    Private Shared Async Function 从GitHub获取更新器更新信息Async() As Task(Of 更新检查结果)
        Dim 发布信息 As LakeUI.GitHub.GitHubReleaseInfo

        Try
            发布信息 = Await 获取GitHub发布信息Async()
        Catch ex As Exception
            Return 更新检查结果.失败("更新器更新失败", "点此查看详情", ex.Message)
        End Try

        If 发布信息 Is Nothing OrElse Not 发布信息.IsSuccess Then
            Return 更新检查结果.失败("更新器更新失败", "点此查看详情", If(发布信息?.ErrorMessage, "未知错误"))
        End If

        Dim 下载地址 As String = 查找GitHub发布文件下载地址(发布信息, "Updater.exe")
        If String.IsNullOrWhiteSpace(下载地址) Then
            Return 更新检查结果.失败("更新器更新失败", "点此查看详情", $"发行版 {发布信息.TagName} 中没有对应的文件：Updater.exe")
        End If

        Return 更新检查结果.可下载(发布信息.TagName, 应用GitHub代理(下载地址), 更新器下载线程数)
    End Function

    Private Shared Async Function 从MirrorChyan获取更新器更新信息Async() As Task(Of 更新检查结果)
        Dim 镜像数据 As MirrorChyan数据结构 = Await 获取MirrorChyan数据Async(获取MirrorChyan更新器地址())
        If 镜像数据 Is Nothing Then
            Return 更新检查结果.失败("更新器更新失败", "未获取到有效数据", "未获取到有效数据")
        End If

        Dim 失败结果 As 更新检查结果 = 校验MirrorChyan数据(镜像数据, "更新器更新失败")
        If 失败结果 IsNot Nothing Then Return 失败结果

        Return 更新检查结果.可下载(镜像数据.Data.VersionName, 镜像数据.Data.Url, 更新器下载线程数)
    End Function

    Private Shared Function 处理更新器更新检查结果(检查结果 As 更新检查结果) As Boolean
        If 检查结果 Is Nothing Then
            设置更新器更新失败("更新器更新失败", "未获取到有效数据", "未获取到有效数据")
            Return False
        End If

        If 检查结果.状态 = 更新检查状态.失败 Then
            设置更新器更新失败(检查结果.状态标题, 检查结果.状态子标题, 检查结果.错误详情)
            Return False
        End If

        If String.IsNullOrWhiteSpace(检查结果.下载地址) Then
            设置更新器更新失败("更新器更新失败", "获取到的下载地址为空", "获取到的下载地址为空")
            Return False
        End If

        Return True
    End Function

    Private Shared Function 清理旧更新器文件() As Boolean
        Try
            强制关闭目标更新器进程(检查更新器更新下载位置)
            If FileIO.FileSystem.FileExists(检查更新器更新下载位置) Then FileIO.FileSystem.DeleteFile(检查更新器更新下载位置)
            Return True
        Catch ex As Exception
            设置更新器更新失败("更新器更新失败", "点此查看详情", ex.Message)
            Return False
        End Try
    End Function

    Private Shared Async Function 下载更新器Async(下载地址 As String) As Task
        Dim 下载器 As New LakeUI.DownloadFile With {
            .Url = 下载地址,
            .SavePath = 检查更新器更新下载位置,
            .ThreadCount = 更新器下载线程数
        }

        Form_v6_起始页面.MB_更新器更新.Text = "正在下载更新器"
        绑定更新器下载事件(下载器)

        Try
            Await 下载器.StartAsync()
        Catch ex As Exception
            设置更新器更新失败("更新器更新失败", "点此查看详情", ex.Message)
        End Try
    End Function

    Private Shared Sub 绑定更新器下载事件(下载器 As LakeUI.DownloadFile)
        AddHandler 下载器.ProgressChanged,
            Sub(s, e)
                Form_v6_起始页面.MB_更新器更新.SubText = 格式化下载进度(e.BytesDownloaded, e.TotalBytes, e.SpeedBytesPerSecond, e.EstimatedTimeRemaining)
            End Sub

        AddHandler 下载器.DownloadCompleted,
            Sub(s, e)
                If FileIO.FileSystem.FileExists(检查更新器更新下载位置) Then
                    Dim 本地版本号 As String = 版本号.获取外部程序文件版本号(检查更新器更新下载位置)
                    Form_v6_起始页面.MB_更新器更新.Text = $"控制台更新程序 {If(String.IsNullOrWhiteSpace(本地版本号), "已更新", 本地版本号)}"
                    Form_v6_起始页面.MB_更新器更新.SubText = "已完成"
                End If

                当前是否正在进行更新器更新 = False
            End Sub

        AddHandler 下载器.DownloadFailed,
            Sub(s, e)
                设置更新器更新失败("更新器更新失败", "点此查看详情", e.Message)
            End Sub
    End Sub

    Private Shared Sub 设置更新器更新失败(标题 As String, 子标题 As String, 错误 As String)
        当前是否正在进行更新器更新 = False
        Form_v6_起始页面.MB_更新器更新.Text = 标题
        Form_v6_起始页面.MB_更新器更新.SubText = 子标题
        检查更新器更新最后一次错误 = 错误
    End Sub

#End Region

#Region "更新信息获取辅助"

    Private Shared Async Function 获取GitHub发布信息Async() As Task(Of LakeUI.GitHub.GitHubReleaseInfo)
        Return Await LakeUI.GitHub.GetLatestReleaseAssetUrlsAsync("Lake1059", "FFmpegFreeUI", True)
    End Function

    Private Shared Function 查找GitHub发布文件下载地址(发布信息 As LakeUI.GitHub.GitHubReleaseInfo, 文件名 As String) As String
        If 发布信息?.Assets Is Nothing Then Return ""

        For Each 资源 In 发布信息.Assets
            If String.Equals(资源.FileName, 文件名, StringComparison.OrdinalIgnoreCase) Then
                Return 资源.DownloadUrl
            End If
        Next

        Return ""
    End Function

    Private Shared Function 应用GitHub代理(下载地址 As String) As String
        If 设置_v6.实例对象.更新服务器选择 <> 1 Then Return 下载地址
        Return "https://cdn.gh-proxy.org/" & 下载地址
    End Function

    Private Shared Async Function 获取MirrorChyan数据Async(请求地址 As String) As Task(Of MirrorChyan数据结构)
        Try
            Return Await LakeUI.SRV_JsonSever.GetJsonAsync(Of MirrorChyan数据结构)(请求地址)
        Catch ex As Exception
            Return New MirrorChyan数据结构 With {
                .Code = -1,
                .Msg = ex.Message
            }
        End Try
    End Function

    Private Shared Function 校验MirrorChyan数据(镜像数据 As MirrorChyan数据结构, 错误标题 As String) As 更新检查结果
        If 镜像数据.Code <> 0 Then
            Return 更新检查结果.失败(错误标题, "点此查看详情", 镜像数据.Msg)
        End If

        If 镜像数据.Data Is Nothing OrElse String.IsNullOrWhiteSpace(镜像数据.Data.Url) Then
            Return 更新检查结果.失败(错误标题, "未获取到有效数据", "未获取到有效数据")
        End If

        Return Nothing
    End Function

    Private Shared Function 国内镜像源数据有效(镜像数据 As 国内镜像源数据结构) As Boolean
        Return 镜像数据 IsNot Nothing AndAlso
               镜像数据.Release IsNot Nothing AndAlso
               Not String.IsNullOrWhiteSpace(镜像数据.Release.Version) AndAlso
               镜像数据.Artifact IsNot Nothing AndAlso
               镜像数据.Artifact.Sources IsNot Nothing
    End Function

    Private Shared Function 查找国内镜像下载源(镜像数据 As 国内镜像源数据结构, 名称关键字 As String) As SourceInfo
        For Each 下载源 As SourceInfo In 镜像数据.Artifact.Sources
            If 下载源 Is Nothing OrElse String.IsNullOrWhiteSpace(下载源.Name) Then Continue For
            If 下载源.Name.Contains(名称关键字, StringComparison.OrdinalIgnoreCase) Then Return 下载源
        Next

        Return Nothing
    End Function

    Private Shared Function 获取本体更新文件名() As String
        Return $"FFmpegFreeUI.{程序架构.获取自身程序架构}.exe"
    End Function

    Private Shared Function 获取国内镜像源本体更新地址() As String
        Return $"https://bs.frostlynx.work:44500/v1/update/check?current_version=0.0.1&channel=stable&rid=win-{程序架构.获取自身程序架构}&package_variant=single-file&locale=zh-CN&supported_download_types=direct_http,frostlynx"
    End Function

    Private Shared Function 获取MirrorChyan本体更新地址() As String
        Return $"https://mirrorchyan.com/api/resources/FFmpegFreeUI/latest?os=win&arch={程序架构.获取自身程序架构}&channel=beta&cdk={设置_v6.实例对象.MirrorChyanCDK}"
    End Function

    Private Shared Function 获取MirrorChyan更新器地址() As String
        Return $"https://mirrorchyan.com/api/resources/FFmpegFreeUI-Updater/latest?os=win&channel=beta&cdk={设置_v6.实例对象.MirrorChyanCDK}"
    End Function

    Private Shared Function 获取更新服务器名称() As String
        Select Case 设置_v6.实例对象.更新服务器选择
            Case 0 : Return "GitHub"
            Case 1 : Return "gh-proxy.com"
            Case 2 : Return "国内镜像"
            Case 3 : Return "Mirror酱"
            Case Else : Return $"未知服务器 {设置_v6.实例对象.更新服务器选择}"
        End Select
    End Function

#End Region

#Region "下载与进程辅助"

    Private Shared Function 获取有效下载线程数(线程数 As Integer, 默认线程数 As Integer) As Integer
        If 线程数 > 0 Then Return 线程数
        Return 默认线程数
    End Function

    Private Shared Function 格式化下载进度(已下载字节 As Long, 总字节 As Long, 每秒字节 As Double, 剩余时间 As TimeSpan) As String
        Dim 百分比 As Double = If(总字节 > 0, 已下载字节 * 100.0R / 总字节, 0)
        Return $"{百分比:F0}%   {已下载字节 / 1024 / 1024:F1}M/{总字节 / 1024 / 1024:F1}M   {每秒字节 / 1024:F1}K/s   ETA {格式化剩余时间(剩余时间)}"
    End Function

    Private Shared Function 格式化剩余时间(time As TimeSpan) As String
        If time.Days > 0 Then Return $"{time.Days}:{time.Hours}:{time.Minutes}:{time.Seconds}"
        If time.Hours > 0 Then Return $"{time.Hours}:{time.Minutes}:{time.Seconds}"
        If time.Minutes > 0 Then Return $"{time.Minutes}:{time.Seconds}"
        Return time.Seconds.ToString()
    End Function

    Private Shared Sub 强制关闭目标更新器进程(目标路径 As String)
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

#End Region

End Class