Imports System.IO
Imports System.Text.Json
Imports LakeUI

Public Class 网络功能
    Public Shared Property 当前是否正在进行获取新闻列表 As Boolean = False

    Public Class 新闻单片数据类
        Public Property Title As String
        Public Property TitleColor As String
        Public Property SubTitle As String
        Public Property Type As String
        Public Property Body As String
    End Class

    Public Shared Async Sub 获取新闻列表()
        If 当前是否正在进行获取新闻列表 Then Exit Sub
        If Not My.Computer.Network.IsAvailable Then
            Form_v6_起始页面.HtmlColorLabel10.Text = $"[无网络] 新闻列表"
            Exit Sub
        End If
        For Each c In Form_v6_起始页面.MP_新闻列表.Controls
            If c Is Form_v6_起始页面.HtmlColorLabel10 Then Continue For
            Form_v6_起始页面.MP_新闻列表.Controls.Remove(c)
        Next
        当前是否正在进行获取新闻列表 = True
        Dim data As String
        Try
            data = Await LakeUI.GitHub.GetFileTextAsync("Lake1059", "FFmpegFreeUI", "News.json")
        Catch ex As Exception
            设置新闻列表获取失败(ex.Message)
            Exit Sub
        End Try
        If data = "" Then
            设置新闻列表获取失败("返回了空数据")
            Exit Sub
        End If
        Dim JsonData As List(Of 新闻单片数据类)
        Try
            JsonData = JsonSerializer.Deserialize(Of List(Of 新闻单片数据类))(data)
        Catch ex As Exception
            设置新闻列表获取失败(ex.Message)
            Exit Sub
        End Try
        If JsonData Is Nothing Then
            设置新闻列表获取失败("无法转换数据")
            Exit Sub
        End If
        For Each N In JsonData
            创建一个新闻内容(N.Title, N.TitleColor, N.SubTitle, N.Type, N.Body)
        Next
        当前是否正在进行获取新闻列表 = False
    End Sub
    Public Shared Sub 创建一个新闻内容(标题 As String, 标题颜色 As String, 副标题 As String, 行为 As String, 内容 As String)
        Dim a As New LakeUI.ModernButton With {
            .BackColor = Color.Transparent,
            .BackColor1 = Color.Transparent,
            .BorderSize = 0,
            .HoverBackColor1 = Color.FromArgb(40, 220, 220, 220),
            .PressedBackColor1 = Color.FromArgb(60, 220, 220, 220),
            .Text = 标题,
            .SubText = 副标题,
            .SubTextForeColor = Color.DarkGray,
            .BorderRadius = 10,
            .Font = New Font(FormMain_v6.Font.Name, 10),
            .SubTextSize = 9,
            .Dock = DockStyle.Top,
            .TextAlign = ModernButton.TextAlignEnum.Left
        }
        If 副标题 = "" Then
            a.Height = 30 * FormMain_v6.DeviceDpi / 96
        Else
            a.Height = 50 * FormMain_v6.DeviceDpi / 96
        End If
        Select Case 标题颜色.ToLower.Trim
            Case "red" : a.ForeColor = Color.IndianRed
            Case "orange" : a.ForeColor = Color.Peru
            Case "yellow" : a.ForeColor = Color.Gold
            Case "green" : a.ForeColor = Color.YellowGreen
            Case "blue" : a.ForeColor = Color.CornflowerBlue
            Case "purple" : a.ForeColor = Color.MediumPurple
            Case Else : a.ForeColor = Color.Silver
        End Select
        Select Case 行为.ToLower.Trim
            Case "msgbox" : AddHandler a.Click, Sub() ExOverlayMsgBox(FormMain_v6, 内容, MsgBoxStyle.OkOnly)
            Case "link" : AddHandler a.Click, Sub() Process.Start(New ProcessStartInfo With {.FileName = 内容, .UseShellExecute = True})
        End Select
        Form_v6_起始页面.MP_新闻列表.Controls.Add(a)
        a.BringToFront()
    End Sub
    Public Shared Sub 设置新闻列表获取失败(原因 As String)
        Dim err1 As New Label With {.Text = 原因, .BackColor = Color.Transparent, .Dock = DockStyle.Fill, .TextAlign = ContentAlignment.MiddleCenter, .AutoSize = False}
        Form_v6_起始页面.MP_新闻列表.Controls.Add(err1)
        当前是否正在进行获取新闻列表 = False
    End Sub




    Public Class 国内镜像更新数据类
        Public Property Version As String

    End Class

    Public Shared Property 当前是否正在进行本体更新 As Boolean = False
    Public Shared Property 检查软件本体更新最后一次错误 As String = ""
    Public Shared ReadOnly Property 检查软件本体更新下载位置 As String = Path.Combine(Application.StartupPath, "FFmpegFreeUI_update.exe")
    Public Shared Async Sub 检查软件本体更新()
        If 当前是否正在进行本体更新 Then Exit Sub
        If Not My.Computer.Network.IsAvailable Then
            Form_v6_起始页面.MB_软件本体更新.Text = $"[无网络] 3FUI 本地版本 {版本号.获取自身版本号}"
            Form_v6_起始页面.MB_软件本体更新.SubText = "请联网后重试"
            Exit Sub
        End If
        Form_v6_起始页面.MB_软件本体更新.Text = "正在检查本体更新 ..."
        Form_v6_起始页面.MB_软件本体更新.SubText = $"连接到更新服务器：{Form_v6_设置_更新选项.MCB_更新服务器.Items(设置_v6.实例对象.更新服务器选择)}"
        当前是否正在进行本体更新 = True
        Dim 获取到的下载地址 As String = ""
        Dim 获取到的版本号 As String = ""
        Select Case 设置_v6.实例对象.更新服务器选择
            Case 0
                Dim data As LakeUI.GitHub.GitHubReleaseInfo
                data = Await LakeUI.GitHub.GetLatestReleaseAssetUrlsAsync("Lake1059", "FFmpegFreeUI", True)
                If Not data.IsSuccess Then
                    设置本体更新失败("检查软件本体更新失败", "点此查看详情", data.ErrorMessage)
                    Exit Sub
                End If
                If 版本号.CompareVersion(data.TagName, 版本号.获取自身版本号) <= 0 Then
                    当前是否正在进行本体更新 = False
                    Form_v6_起始页面.MB_软件本体更新.Text = $"[{Form_v6_设置_更新选项.MCB_更新服务器.Items(设置_v6.实例对象.更新服务器选择)}] 3FUI 云端版本 {data.TagName}"
                    Form_v6_起始页面.MB_软件本体更新.SubText = "已是最新版本"
                    Exit Sub
                End If
                获取到的版本号 = data.TagName
                For Each a1 In data.Assets
                    If a1.FileName = $"FFmpegFreeUI.{架构.获取自身程序架构}.exe" Then
                        获取到的下载地址 = a1.DownloadUrl
                        Exit Select
                    End If
                Next
                设置本体更新失败("检查软件本体更新失败", "点此查看详情", $"发行版 {data.TagName} 中没有对应的文件：FFmpegFreeUI.{架构.获取自身程序架构}.exe")
                Exit Sub
            Case 1
                Dim data As 国内镜像更新数据类
                Try
                    data = Await LakeUI.SRV_JsonSever.GetJsonAsync(Of 国内镜像更新数据类)("https://example.com/config.json", JsonSO, Nothing, "服务名称")
                Catch ex As Exception
                    设置本体更新失败("检查软件本体更新失败", ex.Message, ex.Message)
                    Exit Sub
                End Try
                If data Is Nothing Then
                    设置本体更新失败("检查软件本体更新失败", "未获取到有效数据", "未获取到有效数据")
                    Exit Sub
                End If



            Case Else
        End Select
        If 获取到的下载地址 = "" Then
            设置本体更新失败("软件本体更新失败", "获取到的下载地址为空", "获取到的下载地址为空")
            Exit Sub
        End If
        Try
            If FileIO.FileSystem.FileExists(检查软件本体更新下载位置) Then FileIO.FileSystem.DeleteFile(检查软件本体更新下载位置)
        Catch ex As Exception
            设置本体更新失败("软件本体更新失败", $"无法清理旧更新文件", ex.Message)
            Exit Sub
        End Try
        Dim dl As New LakeUI.DownloadFile With {.Url = 获取到的下载地址, .SavePath = 检查软件本体更新下载位置, .ThreadCount = 10}
        Form_v6_起始页面.MB_软件本体更新.Text = $"正在下载本体更新 {获取到的版本号}"
        AddHandler dl.ProgressChanged, Sub(s, e) Form_v6_起始页面.MB_软件本体更新.SubText = 格式化下载进度(e.BytesDownloaded, e.TotalBytes, e.SpeedBytesPerSecond, e.EstimatedTimeRemaining)
        AddHandler dl.DownloadCompleted, Sub(s, e)
                                             UpdateAvailable = True
                                             Form_v6_起始页面.MB_软件本体更新.Text = $"3FUI 更新 {获取到的版本号} 已准备就绪"
                                             Form_v6_起始页面.MB_软件本体更新.SubText = "关闭软件后自动开始更新"
                                             当前是否正在进行本体更新 = False
                                             LakeUI.ExOverlayMsgBox(FormMain_v6, $"关闭此实例以开始更新，如果还有其他实例在运行，更新控制台会等待所有实例关闭之后再进行更新，关闭此实例不影响其他实例触发更新控制台", MsgBoxStyle.Information, $"3FUI 更新 {获取到的版本号} 已准备就绪")
                                         End Sub
        AddHandler dl.DownloadFailed, Sub(s, e)
                                          设置本体更新失败("下载软件本体更新失败", "点此查看详情", e.Message)
                                          Exit Sub
                                      End Sub
        Await dl.StartAsync()
    End Sub

    Public Shared Property 当前是否正在进行更新器更新 As Boolean = False
    Public Shared Property 检查更新器更新最后一次错误 As String = ""
    Public Shared ReadOnly Property 检查更新器更新下载位置 As String = Path.Combine(Application.StartupPath, "Updater.exe")
    Public Shared Async Sub 检查更新器更新(Optional 强制更新 As Boolean = False)
        If 当前是否正在进行更新器更新 Then Exit Sub

        If Not 强制更新 AndAlso FileIO.FileSystem.FileExists(检查更新器更新下载位置) Then
            Dim 本地版本号 = 版本号.获取外部程序文件版本号(检查更新器更新下载位置)
            Form_v6_起始页面.MB_更新器更新.Text = $"控制台更新程序 {If(String.IsNullOrWhiteSpace(本地版本号), "本地版本未知", 本地版本号)}"
            Form_v6_起始页面.MB_更新器更新.SubText = "已安装，此组件为手动更新"
            Exit Sub
        End If

        If Not My.Computer.Network.IsAvailable Then
            Form_v6_起始页面.MB_更新器更新.Text = "[无网络] 控制台更新程序"
            Form_v6_起始页面.MB_更新器更新.SubText = "请联网后重试"
            Exit Sub
        End If

        Form_v6_起始页面.MB_更新器更新.Text = "正在检查更新器更新 ..."
        Form_v6_起始页面.MB_更新器更新.SubText = $"连接到更新服务器：{Form_v6_设置_更新选项.MCB_更新服务器.Items(设置_v6.实例对象.更新服务器选择)}"
        当前是否正在进行更新器更新 = True

        Dim 获取到的下载地址 As String = ""

        Select Case 设置_v6.实例对象.更新服务器选择
            Case 0
                Dim data As LakeUI.GitHub.GitHubReleaseInfo
                Try
                    data = Await LakeUI.GitHub.GetLatestReleaseAssetUrlsAsync("Lake1059", "FFmpegFreeUI", True)
                Catch ex As Exception
                    设置更新器更新失败("更新器更新失败", "点此查看详情", ex.Message)
                    Exit Sub
                End Try
                If data Is Nothing OrElse Not data.IsSuccess Then
                    设置更新器更新失败("更新器更新失败", "点此查看详情", If(data?.ErrorMessage, "未知错误"))
                    Exit Sub
                End If

                For Each a1 In data.Assets
                    If String.Equals(a1.FileName, "Updater.exe", StringComparison.OrdinalIgnoreCase) Then
                        获取到的下载地址 = a1.DownloadUrl
                        Exit Select
                    End If
                Next
                设置更新器更新失败("更新器更新失败", "点此查看详情", $"发行版 {data.TagName} 中没有对应的文件：Updater.exe")
                Exit Sub
            Case Else
        End Select

        If 获取到的下载地址 = "" Then
            设置更新器更新失败("更新器更新失败", "获取到的下载地址为空", "获取到的下载地址为空")
            Exit Sub
        End If

        Try
            强制关闭目标更新器进程(检查更新器更新下载位置)
            If FileIO.FileSystem.FileExists(检查更新器更新下载位置) Then FileIO.FileSystem.DeleteFile(检查更新器更新下载位置)
        Catch ex As Exception
            设置更新器更新失败("更新器更新失败", "点此查看详情", ex.Message)
            Exit Sub
        End Try

        Dim dl As New LakeUI.DownloadFile With {.Url = 获取到的下载地址, .SavePath = 检查更新器更新下载位置, .ThreadCount = 4}
        Form_v6_起始页面.MB_更新器更新.Text = "正在下载更新器"
        AddHandler dl.ProgressChanged, Sub(s, e) Form_v6_起始页面.MB_更新器更新.SubText = 格式化下载进度(e.BytesDownloaded, e.TotalBytes, e.SpeedBytesPerSecond, e.EstimatedTimeRemaining)
        AddHandler dl.DownloadCompleted, Sub(s, e)
                                             If FileIO.FileSystem.FileExists(检查更新器更新下载位置) Then
                                                 Dim 本地版本号 = 版本号.获取外部程序文件版本号(检查更新器更新下载位置)
                                                 Form_v6_起始页面.MB_更新器更新.Text = $"控制台更新程序 {If(String.IsNullOrWhiteSpace(本地版本号), "已更新", 本地版本号)}"
                                                 Form_v6_起始页面.MB_更新器更新.SubText = "已完成"
                                             End If
                                             当前是否正在进行更新器更新 = False
                                         End Sub
        AddHandler dl.DownloadFailed, Sub(s, e)
                                          设置更新器更新失败("更新器更新失败", "点此查看详情", e.Message)
                                      End Sub
        Await dl.StartAsync()
    End Sub

    Private Shared Sub 设置本体更新失败(标题 As String, 子标题 As String, 错误 As String)
        当前是否正在进行本体更新 = False
        Form_v6_起始页面.MB_软件本体更新.Text = 标题
        Form_v6_起始页面.MB_软件本体更新.SubText = 子标题
        检查软件本体更新最后一次错误 = 错误
    End Sub

    Private Shared Sub 设置更新器更新失败(标题 As String, 子标题 As String, 错误 As String)
        当前是否正在进行更新器更新 = False
        Form_v6_起始页面.MB_更新器更新.Text = 标题
        Form_v6_起始页面.MB_更新器更新.SubText = 子标题
        检查更新器更新最后一次错误 = 错误
    End Sub

    Private Shared Function 格式化下载进度(已下载字节 As Long, 总字节 As Long, 每秒字节 As Double, 剩余时间 As TimeSpan) As String
        Dim 百分比 = If(总字节 > 0, 已下载字节 * 100.0R / 总字节, 0)
        Return $"{百分比:F0}%   {已下载字节 / 1024 / 1024:F1}M/{总字节 / 1024 / 1024:F1}M   {每秒字节 / 1024:F1}K/s   ETA {格式化剩余时间(剩余时间)}"
    End Function

    Private Shared Sub 强制关闭目标更新器进程(目标路径 As String)
        Dim 目标完整路径 = Path.GetFullPath(目标路径)
        For Each p In Process.GetProcessesByName(Path.GetFileNameWithoutExtension(目标完整路径))
            Try
                Dim 进程路径 As String = ""
                Try
                    进程路径 = p.MainModule?.FileName
                Catch
                    进程路径 = ""
                End Try

                If Not String.IsNullOrWhiteSpace(进程路径) AndAlso
                   String.Equals(Path.GetFullPath(进程路径), 目标完整路径, StringComparison.OrdinalIgnoreCase) Then
                    p.Kill(True)
                    p.WaitForExit(5000)
                End If
            Finally
                p.Dispose()
            End Try
        Next
    End Sub

    Private Shared Function 格式化剩余时间(time As TimeSpan) As String
        If time.Days > 0 Then Return $"{time.Days}:{time.Hours}:{time.Minutes}:{time.Seconds}"
        If time.Hours > 0 Then Return $"{time.Hours}:{time.Minutes}:{time.Seconds}"
        If time.Minutes > 0 Then Return $"{time.Minutes}:{time.Seconds}"
        Return time.Seconds.ToString()
    End Function
End Class
