Imports System.ComponentModel
Imports LakeUI

Public Class FormMain_v6
    Private Sub FormMain_v6_Load(sender As Object, e As EventArgs) Handles Me.Load
        设置_v6.启动时加载设置()



        Select Case 设置_v6.实例对象.SP_毛玻璃模式
            Case 1 : Me.ThisIsYourWindow1.BackdropMode = ThisIsYourWindow.BackdropModeEnum.Image
            Case 2 : Me.ThisIsYourWindow1.BackdropMode = ThisIsYourWindow.BackdropModeEnum.Auto
            Case Else : Me.ThisIsYourWindow1.BackdropMode = ThisIsYourWindow.BackdropModeEnum.None
        End Select
        Select Case 设置_v6.实例对象.窗口样式
            Case 1
                LakeUI.DwmWindowStyle.SetDarkMode(Me.Handle, True)
            Case 2
                If SP_UnLock Then
                    Me.ThisIsYourWindow1.Attach(Me)
                    Select Case 设置_v6.实例对象.SP_毛玻璃模式
                        Case > 0
                            ModernTabListControl1.TabStripBackColor = Color.Transparent
                            ModernTabListControl1.ContentBackColor = Color.Transparent
                            Form_v6_设置.ModernTabListControl1.TabStripBackColor = Color.Transparent
                            Form_v6_设置.ModernTabListControl1.ContentBackColor = Color.Transparent
                    End Select
                End If
        End Select
        Me.ModernTabListControl1.Focus()
        Me.ModernTabListControl1.Items(1).BoundControl = Form_v6_起始页面
        绑定选项卡窗体背景透明(Form_v6_起始页面.ModernPanel1)
        Me.ModernTabListControl1.Items(2).BoundControl = Form_v6_编码队列
        绑定选项卡窗体背景透明(Form_v6_编码队列.ModernPanel1)
        Me.ModernTabListControl1.Items(5).BoundControl = Form_v6_参数面板
        绑定选项卡窗体背景透明(Form_v6_参数面板.ModernPanel1)
        Me.ModernTabListControl1.Items(11).BoundControl = Form_v6_性能监控
        Me.ModernTabListControl1.Items(14).BoundControl = Form_v6_设置
        绑定选项卡窗体背景透明(Form_v6_设置.ModernPanel1)
        Me.ModernTabListControl1.Items(15).BoundControl = Form_v6_支持者
        Me.ModernTabListControl1.SelectedIndex = 1
        Me.ModernTextBox1.Parent = Me.ModernTabListControl1
    End Sub

    Private Sub FormMain_v6_Shown(sender As Object, e As EventArgs) Handles Me.Shown





    End Sub

    Sub 绑定选项卡窗体背景透明(选项卡的根面板容器 As ModernPanel)
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

    Sub 启用透明背景模式()
        ModernTabListControl1.TabStripBackColor = Color.Transparent
        ModernTabListControl1.ContentBackColor = Color.Transparent
        ModernTextBox1.BackColor1 = Color.FromArgb(80, 220, 220, 220)
    End Sub

    Sub 禁用透明背景模式()
        ModernTabListControl1.TabStripBackColor = Color.FromArgb(48, 48, 48)
        ModernTabListControl1.ContentBackColor = Color.FromArgb(48, 48, 48)
        ModernTextBox1.BackColor1 = Color.FromArgb(40, 220, 220, 220)
    End Sub

    Private Sub FormMain_v6_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        设置_v6.退出时保存设置()
    End Sub
End Class