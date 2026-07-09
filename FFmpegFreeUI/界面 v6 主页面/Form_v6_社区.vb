Imports LakeUI

Public Class Form_v6_社区
    Private Sub Form_v6_社区_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.ModernTabListControl1.Items(0).BoundControl = Form_v6_社区_起始页面
        绑定选项卡窗体背景透明(Form_v6_社区_起始页面.ModernPanel1)

        Me.ModernTabListControl1.Items(5).BoundControl = Form_v6_社区_用户登录
        绑定选项卡窗体背景透明(Form_v6_社区_用户登录.ModernPanel1)
        Me.ModernTabListControl1.Items(6).BoundControl = Form_v6_社区_我的消息
        绑定选项卡窗体背景透明(Form_v6_社区_我的消息.ModernPanel1)



        If 设置_v6.实例对象.窗口样式 = 2 AndAlso SP_UnLock AndAlso 设置_v6.实例对象.SP_毛玻璃模式 > 0 Then
            Form_v6_社区_用户登录.ModernPanel1.Padding = New Padding(10 * DeviceDpi / 96, 10 * DeviceDpi / 96, Form_v6_社区_用户登录.ModernPanel1.Padding.Right, Form_v6_社区_用户登录.ModernPanel1.Padding.Bottom)

        End If

    End Sub

    Shared Sub 绑定选项卡窗体背景透明(选项卡的根面板容器 As ModernPanel)
        If SP_UnLock Then
            Select Case 设置_v6.实例对象.SP_毛玻璃模式
                Case > 0
                    选项卡的根面板容器.BackColor = Color.Transparent
                    选项卡的根面板容器.BackColor1 = Color.Transparent
                    选项卡的根面板容器.BackgroundSource = FormMain_v6
            End Select
        End If
    End Sub

    Private Sub Form_v6_社区_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Me.ModernTabListControl1.SelectedIndex = 0
    End Sub
End Class