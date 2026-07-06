Imports System.Diagnostics
Imports System.Text.Json
Imports System.Threading

Friend NotInheritable Class 网络功能_v6_新闻列表

    Friend Shared Property 当前是否正在进行获取新闻列表 As Boolean = False

    Private Sub New()
    End Sub

    Friend Shared Async Sub 获取新闻列表()
        If Not 可以开始获取新闻列表() Then Exit Sub

        清空新闻列表()
        当前是否正在进行获取新闻列表 = True

        Try
            Dim 新闻列表 As List(Of 网络功能.新闻单片数据类) = Await 获取新闻列表数据Async()
            If 新闻列表 Is Nothing Then
                当前是否正在进行获取新闻列表 = False
                Exit Sub
            End If

            添加新闻列表到界面(新闻列表)
            当前是否正在进行获取新闻列表 = False
        Catch ex As Exception
            设置新闻列表获取失败(网络功能_v6_通用.获取异常消息(ex))
        End Try
    End Sub

    Friend Shared Sub 创建一个新闻内容(标题 As String, 标题颜色 As String, 副标题 As String, 行为 As String, 内容 As String)
        Dim 按钮 As LakeUI.ModernButton = 创建新闻按钮(标题, 副标题)

        设置新闻按钮高度(按钮, 副标题)
        按钮.ForeColor = 获取新闻标题颜色(标题颜色)
        绑定新闻按钮行为(按钮, 行为, 内容)
        添加新闻按钮到界面(按钮)
    End Sub

    Friend Shared Sub 设置新闻列表获取失败(原因 As String)
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

    Private Shared Async Function 获取新闻列表数据Async() As Task(Of List(Of 网络功能.新闻单片数据类))
        Dim 原始数据 As String

        Try
            Using cts As CancellationTokenSource = 网络功能_v6_通用.创建联网请求取消源()
                原始数据 = Await LakeUI.GitHub.GetFileTextAsync("Lake1059", "FFmpegFreeUI", "News.json", cancellationToken:=cts.Token)
            End Using
        Catch ex As Exception
            设置新闻列表获取失败(网络功能_v6_通用.获取异常消息(ex))
            Return Nothing
        End Try

        If String.IsNullOrWhiteSpace(原始数据) Then
            设置新闻列表获取失败("返回了空数据")
            Return Nothing
        End If

        Try
            Dim 新闻列表 As List(Of 网络功能.新闻单片数据类) = JsonSerializer.Deserialize(Of List(Of 网络功能.新闻单片数据类))(原始数据)
            If 新闻列表 Is Nothing Then 设置新闻列表获取失败("无法转换数据")
            Return 新闻列表
        Catch ex As Exception
            设置新闻列表获取失败(ex.Message)
            Return Nothing
        End Try
    End Function

    Private Shared Sub 添加新闻列表到界面(新闻列表 As IEnumerable(Of 网络功能.新闻单片数据类))
        For Each 新闻 As 网络功能.新闻单片数据类 In 新闻列表
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

End Class
