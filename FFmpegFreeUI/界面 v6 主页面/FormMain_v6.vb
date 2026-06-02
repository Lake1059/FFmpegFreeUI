Imports System.ComponentModel
Imports System.IO
Imports LakeUI

Public Class FormMain_v6
    Private Sub FormMain_v6_Load(sender As Object, e As EventArgs) Handles Me.Load
        设置_v6.启动时读取SP解锁器()
        设置_v6.启动时加载设置()

        设置_v6.加载SP自定义图标()
        设置_v6.加载SP自定义起始页顶栏背景图()
        设置_v6.加载SP自定义背景图()

        Me.ModernTabListControl1.Items(1).BoundControl = Form_v6_起始页面
        绑定选项卡(Form_v6_起始页面.ModernPanel1)
        Me.ModernTabListControl1.Items(2).BoundControl = Form_v6_编码队列
        绑定选项卡(Form_v6_编码队列.ModernPanel1)
        Me.ModernTabListControl1.Items(5).BoundControl = Form_v6_参数面板
        绑定选项卡(Form_v6_参数面板.ModernPanel1)
        Me.ModernTabListControl1.Items(9).BoundControl = Form_v6_媒体信息
        绑定选项卡(Form_v6_媒体信息.ModernPanel1)
        Me.ModernTabListControl1.Items(10).BoundControl = Form_v6_调试播放器
        绑定选项卡(Form_v6_调试播放器.ModernPanel1)
        Me.ModernTabListControl1.Items(11).BoundControl = Form_v6_性能监控
        绑定选项卡(Form_v6_性能监控.ModernPanel1)
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

                        Form_v6_设置.ModernTabListControl1.TabStripBackColor = Color.Transparent
                        Form_v6_设置.ModernTabListControl1.ContentBackColor = Color.Transparent

                        Form_v6_起始页面.ModernPanel1.Padding = New Padding(Form_v6_起始页面.ModernPanel1.Padding.Left, 10 * DeviceDpi / 96, Form_v6_起始页面.ModernPanel1.Padding.Right, Form_v6_起始页面.ModernPanel1.Padding.Bottom)
                        Form_v6_媒体信息.ModernPanel1.Padding = New Padding(Form_v6_媒体信息.ModernPanel1.Padding.Left, 10 * DeviceDpi / 96, Form_v6_媒体信息.ModernPanel1.Padding.Right, Form_v6_媒体信息.ModernPanel1.Padding.Bottom)
                        Form_v6_调试播放器.ModernPanel1.Padding = New Padding(Form_v6_调试播放器.ModernPanel1.Padding.Left, 10 * DeviceDpi / 96, Form_v6_调试播放器.ModernPanel1.Padding.Right, Form_v6_调试播放器.ModernPanel1.Padding.Bottom)
                End Select
        End Select


        Me.ModernTabListControl1.SelectedIndex = 1
        Me.ModernTextBox1.Parent = Me.ModernTabListControl1
    End Sub

    Private Sub FormMain_v6_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Me.ModernTabListControl1.Focus()

        ExOverlayMsgBox(Me, $"{vbCrLf}3FUI 6.0 当前处于开发测试阶段，许多功能都未跟进，请耐心等待项目推进，现在的提前放出是为了公开测试新设计的兼容和性能问题，早发现早解决。现在开始使用 GPU 渲染，将使用显存，有任何问题请及时汇报。{vbCrLf & vbCrLf}请勿汇报首次切换选项卡的渲染等待问题，这是解决不了的，再次切换过去就没事了。要解决这个问题的难度不亚于我当上微软老总，极具挑战。",, "当前版本大量功能未实装，标准流程未实装")

        网络功能.检查软件本体更新()
        网络功能.检查更新器更新()
        网络功能.获取新闻列表()

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

    Private Sub ModernTabListControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ModernTabListControl1.SelectedIndexChanged
        Select Case ModernTabListControl1.SelectedIndex
            Case 11 : Form_v6_性能监控.开始()
            Case Else : Form_v6_性能监控.停止()
        End Select
    End Sub

    Private Sub FormMain_v6_Closing(sender As Object, e As CancelEventArgs) Handles Me.FormClosing
        e.Cancel = False
        设置_v6.退出时保存设置()
        If UpdateAvailable Then
            If FileIO.FileSystem.FileExists(Path.Combine(Application.StartupPath, "Updater.exe")) Then
                Process.Start(Path.Combine(Application.StartupPath, "Updater.exe"))
            Else
                If ExOverlayMsgBox(Me, "程序目录下没有更新器，这是意外情况，仍旧退出？", MsgBoxStyle.YesNo) <> MsgBoxResult.Yes Then e.Cancel = True
            End If
        End If
    End Sub
End Class