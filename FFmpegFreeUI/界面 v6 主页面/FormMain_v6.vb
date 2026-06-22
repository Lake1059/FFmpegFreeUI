Imports System.ComponentModel
Imports System.IO
Imports System.Reflection
Imports LakeUI

Public Class FormMain_v6
    Private ReadOnly 插件选项卡页 As New Dictionary(Of String, ModernTabListControl.ModernTabPage)(StringComparer.CurrentCultureIgnoreCase)

    Private Sub FormMain_v6_Load(sender As Object, e As EventArgs) Handles Me.Load
        UI同步上下文 = Threading.SynchronizationContext.Current
        设置_v6.启动时读取SP解锁器()
        设置_v6.启动时加载设置()
        网络功能.启动时后台获取SPAgent端点()

        设置_v6.加载SP自定义图标()
        设置_v6.加载SP自定义起始页顶栏背景图()
        设置_v6.加载SP自定义背景图()

        Me.ModernTabListControl1.Items(1).BoundControl = Form_v6_起始页面
        绑定选项卡(Form_v6_起始页面.ModernPanel1)
        Me.ModernTabListControl1.Items(2).BoundControl = Form_v6_编码队列
        绑定选项卡(Form_v6_编码队列.ModernPanel1)
        Me.ModernTabListControl1.Items(4).BoundControl = Form_v6_准备文件
        绑定选项卡(Form_v6_准备文件.ModernPanel1)
        Me.ModernTabListControl1.Items(5).BoundControl = Form_v6_参数面板
        绑定选项卡(Form_v6_参数面板.ModernPanel1)
        Me.ModernTabListControl1.Items(6).BoundControl = Form_v6_Agent
        绑定选项卡(Form_v6_Agent.ModernPanel1)
        Me.ModernTabListControl1.Items(9).BoundControl = Form_v6_媒体信息
        绑定选项卡(Form_v6_媒体信息.ModernPanel1)
        Me.ModernTabListControl1.Items(10).BoundControl = Form_v6_调试播放器
        绑定选项卡(Form_v6_调试播放器.ModernPanel1)
        Me.ModernTabListControl1.Items(11).BoundControl = Form_v6_性能监控
        绑定选项卡(Form_v6_性能监控.ModernPanel1)
        Me.ModernTabListControl1.Items(12).BoundControl = Form_v6_集成工具
        绑定选项卡(Form_v6_集成工具.ModernPanel1)
        Me.ModernTabListControl1.Items(14).BoundControl = Form_v6_设置
        绑定选项卡(Form_v6_设置.ModernPanel1)
        Me.ModernTabListControl1.Items(15).BoundControl = Form_v6_支持者
        绑定选项卡(Form_v6_支持者.ModernPanel1)

        Select Case 设置_v6.实例对象.窗口样式
            Case 1
                DwmWindowStyle.SetDarkMode(Me.Handle, True)
            Case 2
                Me.ThisIsYourWindow1.Attach(Me)
                If Not SP_UnLock Then Exit Select
                Select Case 设置_v6.实例对象.SP_毛玻璃模式
                    Case > 0
                        ModernTabListControl1.TabStripBackColor = Color.Transparent
                        ModernTabListControl1.ContentBackColor = Color.Transparent
                        Form_v6_参数面板.ModernTabListControl1.TabStripBackColor = Color.Transparent
                        Form_v6_参数面板.ModernTabListControl1.ContentBackColor = Color.Transparent
                        Form_v6_集成工具.ModernTabListControl1.TabStripBackColor = Color.Transparent
                        Form_v6_集成工具.ModernTabListControl1.ContentBackColor = Color.Transparent
                        Form_v6_设置.ModernTabListControl1.TabStripBackColor = Color.Transparent
                        Form_v6_设置.ModernTabListControl1.ContentBackColor = Color.Transparent

                        Form_v6_起始页面.ModernPanel1.Padding = New Padding(10 * DeviceDpi / 96, 10 * DeviceDpi / 96, Form_v6_起始页面.ModernPanel1.Padding.Right, Form_v6_起始页面.ModernPanel1.Padding.Bottom)
                        Form_v6_准备文件.ModernPanel1.Padding = New Padding(10 * DeviceDpi / 96, 10 * DeviceDpi / 96, Form_v6_准备文件.ModernPanel1.Padding.Right, Form_v6_准备文件.ModernPanel1.Padding.Bottom)
                        Form_v6_媒体信息.ModernPanel1.Padding = New Padding(10 * DeviceDpi / 96, 10 * DeviceDpi / 96, Form_v6_媒体信息.ModernPanel1.Padding.Right, Form_v6_媒体信息.ModernPanel1.Padding.Bottom)
                        Form_v6_调试播放器.ModernPanel1.Padding = New Padding(10 * DeviceDpi / 96, 10 * DeviceDpi / 96, Form_v6_调试播放器.ModernPanel1.Padding.Right, Form_v6_调试播放器.ModernPanel1.Padding.Bottom)
                End Select
        End Select

        Me.ModernTabListControl1.SelectedIndex = 1
        Me.ModernTextBox1.Parent = Me.ModernTabListControl1

        其他初始化.执行()

        插件管理.启动时加载插件()
        If 设置_v6.实例对象.是否监听端口 Then 端口监听_v6.启动客户端()

    End Sub

    Private Sub FormMain_v6_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Me.ModernTabListControl1.Focus()
        Application.DoEvents()

        If Not 设置_v6.实例对象.是否询问标记_下载服务器选择 Then
            If Globalization.RegionInfo.CurrentRegion.EnglishName.ToLower.Trim.Contains("china") Then
                If ExOverlayMsgBox(Me, $"{vbCrLf}检测到当前系统区域为国内，是否选择使用国内镜像站作为下载更新服务器？详细信息可前往设置查看。", MsgBoxStyle.YesNo, "建议国内用户使用镜像服务器") = MsgBoxResult.Yes Then
                    设置_v6.实例对象.更新服务器选择 = 2
                    Form_v6_设置_更新选项.MCB_更新服务器.SelectedIndex = 设置_v6.实例对象.更新服务器选择
                End If
                设置_v6.实例对象.是否询问标记_下载服务器选择 = True
            End If
        End If

        网络功能.检查软件本体更新()
        网络功能.检查更新器更新()
        网络功能.获取新闻列表()

        If 设置_v6.实例对象.启用性能计数器 = 0 Then
            MainAppUsageCounter.Start()
            PrecisionTimer1.Start()
        End If
    End Sub

    Sub 绑定选项卡(选项卡的根面板容器 As ModernPanel)
        If SP_UnLock Then
            Select Case 设置_v6.实例对象.SP_毛玻璃模式
                Case > 0
                    选项卡的根面板容器.BackColor = Color.Transparent
                    选项卡的根面板容器.BackColor1 = Color.Transparent
                    选项卡的根面板容器.BackgroundSource = Me
            End Select
        End If
    End Sub

    Public Sub 添加插件选项卡(选项卡标题 As String, 面板 As Control)
        Dim 标题 = If(选项卡标题, "").Trim()
        If 标题 = "" OrElse 面板 Is Nothing Then Exit Sub

        面板.Dock = DockStyle.Fill

        Dim 选项卡 As ModernTabListControl.ModernTabPage = Nothing
        If 插件选项卡页.TryGetValue(标题, 选项卡) Then
            选项卡.BoundControl = 面板
        Else
            选项卡 = New ModernTabListControl.ModernTabPage With {
                .Text = 标题,
                .BoundControl = 面板
            }
            ModernTabListControl1.Items.Insert(获取插件选项卡插入位置(), 选项卡)
            插件选项卡页(标题) = 选项卡
        End If

        Dim 根面板 = 查找可绑定背景映射的插件ModernPanel(面板)
        If 根面板 IsNot Nothing Then 绑定选项卡(根面板)
    End Sub

    Private Function 获取插件选项卡插入位置() As Integer
        Dim 已到插件区域 = False

        For i = 0 To ModernTabListControl1.Items.Count - 1
            Dim item = ModernTabListControl1.Items(i)
            If 已到插件区域 AndAlso item.IsSeparator Then Return i
            If String.Equals(item.Text, "集成的工具", StringComparison.CurrentCultureIgnoreCase) Then 已到插件区域 = True
        Next

        Return ModernTabListControl1.Items.Count
    End Function

    Private Function 查找可绑定背景映射的插件ModernPanel(根控件 As Control) As ModernPanel
        If 根控件 Is Nothing Then Return Nothing

        Dim 根控件类型 As Type = 根控件.GetType()
        While 根控件类型 IsNot Nothing
            Dim 字段 = 根控件类型.GetField("ModernPanel1", BindingFlags.Public Or BindingFlags.NonPublic Or BindingFlags.Instance)
            If 字段 IsNot Nothing Then
                Dim 面板 = TryCast(字段.GetValue(根控件), ModernPanel)
                If 插件ModernPanel可绑定背景映射(面板) Then Return 面板
            End If

            Dim 属性 = 根控件类型.GetProperty("ModernPanel1", BindingFlags.Public Or BindingFlags.NonPublic Or BindingFlags.Instance)
            If 属性 IsNot Nothing Then
                Dim 面板 = TryCast(属性.GetValue(根控件), ModernPanel)
                If 插件ModernPanel可绑定背景映射(面板) Then Return 面板
            End If

            根控件类型 = 根控件类型.BaseType
        End While

        Return 查找子控件中的插件ModernPanel(根控件)
    End Function

    Private Function 查找子控件中的插件ModernPanel(控件 As Control) As ModernPanel
        If 控件 Is Nothing Then Return Nothing

        Dim 面板 = TryCast(控件, ModernPanel)
        If 插件ModernPanel可绑定背景映射(面板) Then Return 面板

        For Each 子控件 As Control In 控件.Controls
            Dim 子面板 = 查找子控件中的插件ModernPanel(子控件)
            If 子面板 IsNot Nothing Then Return 子面板
        Next

        Return Nothing
    End Function

    Private Function 插件ModernPanel可绑定背景映射(面板 As ModernPanel) As Boolean
        Return 面板 IsNot Nothing AndAlso
               String.Equals(面板.Name, "ModernPanel1", StringComparison.Ordinal) AndAlso
               面板.Dock = DockStyle.Fill
    End Function

    Private Sub ModernTabListControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ModernTabListControl1.SelectedIndexChanged
        Select Case ModernTabListControl1.SelectedIndex
            Case 11 : Form_v6_性能监控.开始()
            Case Else : Form_v6_性能监控.停止()
        End Select
    End Sub

    Private Sub FormMain_v6_Closing(sender As Object, e As CancelEventArgs) Handles Me.FormClosing
        e.Cancel = False
        端口监听_v6.停止客户端()
        设置_v6.退出时保存设置()
        If UpdateAvailable Then
            If FileIO.FileSystem.FileExists(Path.Combine(Application.StartupPath, "Updater.exe")) Then
                Process.Start(Path.Combine(Application.StartupPath, "Updater.exe"))
            Else
                If ExOverlayMsgBox(Me, "程序目录下没有更新器，这是意外情况，仍旧退出？", MsgBoxStyle.YesNo) <> MsgBoxResult.Yes Then e.Cancel = True
            End If
        End If
    End Sub

    Private Sub PrecisionTimer1_Tick(sender As Object, e As EventArgs) Handles PrecisionTimer1.Tick
        Dim t1 As String = "<Title>"
        t1 &= $"   |   CPU {MainAppUsageCounter.GetCpuUsagePercent():F1}%"
        t1 &= $"   |   RAM {MainAppUsageCounter.GetActivePrivateWorkingSetBytes() / 1024 / 1024:F0}M / {MainAppUsageCounter.GetCommitSizeBytes() / 1024 / 1024:F0}M"
        t1 &= $"   |   GPU {MainAppUsageCounter.GetGpuUsagePercent():F1}% {MainAppUsageCounter.GetGpuDedicatedMemoryBytes() / 1024 / 1024:F0}M + {MainAppUsageCounter.GetGpuSharedMemoryBytes() / 1024 / 1024:F0}M"
        Me.ThisIsYourWindow1.TitleTextPrivateProtocol = t1
    End Sub
End Class
