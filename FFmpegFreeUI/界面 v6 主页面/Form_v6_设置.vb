Imports LakeUI

Public Class Form_v6_设置

    Private Sub Form_v6_设置_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.ModernTabListControl1.Items(1).BoundControl = Form_v6_设置_LakeUI性能选项
        绑定选项卡窗体背景透明(Form_v6_设置_LakeUI性能选项.ModernPanel1)
        Me.ModernTabListControl1.Items(2).BoundControl = Form_v6_设置_LakeUI视觉体验
        绑定选项卡窗体背景透明(Form_v6_设置_LakeUI视觉体验.ModernPanel1)
        Me.ModernTabListControl1.Items(3).BoundControl = Form_v6_设置_LakeUIHDR
        绑定选项卡窗体背景透明(Form_v6_设置_LakeUIHDR.ModernPanel1)
        Me.ModernTabListControl1.Items(4).BoundControl = Form_v6_设置_LakeUI许可证
        绑定选项卡窗体背景透明(Form_v6_设置_LakeUI许可证.ModernPanel1)
        Me.ModernTabListControl1.Items(7).BoundControl = Form_v6_设置_界面显示
        绑定选项卡窗体背景透明(Form_v6_设置_界面显示.ModernPanel1)
        Me.ModernTabListControl1.Items(8).BoundControl = Form_v6_设置_性能调度
        绑定选项卡窗体背景透明(Form_v6_设置_性能调度.ModernPanel1)
        Me.ModernTabListControl1.Items(9).BoundControl = Form_v6_设置_功能设定
        绑定选项卡窗体背景透明(Form_v6_设置_功能设定.ModernPanel1)
        Me.ModernTabListControl1.Items(10).BoundControl = Form_v6_设置_转译辅助
        绑定选项卡窗体背景透明(Form_v6_设置_转译辅助.ModernPanel1)
        Me.ModernTabListControl1.Items(11).BoundControl = Form_v6_设置_更新选项
        绑定选项卡窗体背景透明(Form_v6_设置_更新选项.ModernPanel1)
        Me.ModernTabListControl1.Items(12).BoundControl = Form_v6_设置_远程调用
        绑定选项卡窗体背景透明(Form_v6_设置_远程调用.ModernPanel1)
        Me.ModernTabListControl1.Items(13).BoundControl = Form_v6_设置_Agent
        绑定选项卡窗体背景透明(Form_v6_设置_Agent.ModernPanel1)
        Me.ModernTabListControl1.Items(16).BoundControl = Form_v6_设置_个性化
        绑定选项卡窗体背景透明(Form_v6_设置_个性化.ModernPanel1)
    End Sub

    Private Sub Form_v6_设置_Shown(sender As Object, e As EventArgs) Handles Me.Shown

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

End Class
