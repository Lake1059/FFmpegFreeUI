Imports LakeUI

Public Class Form_v6_设置

    Private Sub Form_v6_设置_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim 页面绑定 As (索引 As Integer, 页面 As Control, 面板 As ModernPanel)() = {
            (1, Form_v6_设置_LakeUI性能选项, Form_v6_设置_LakeUI性能选项.ModernPanel1),
            (2, Form_v6_设置_LakeUI视觉体验, Form_v6_设置_LakeUI视觉体验.ModernPanel1),
            (3, Form_v6_设置_LakeUIHDR, Form_v6_设置_LakeUIHDR.ModernPanel1),
            (4, Form_v6_设置_LakeUI许可证, Form_v6_设置_LakeUI许可证.ModernPanel1),
            (7, Form_v6_设置_界面显示, Form_v6_设置_界面显示.ModernPanel1),
            (8, Form_v6_设置_性能调度, Form_v6_设置_性能调度.ModernPanel1),
            (9, Form_v6_设置_功能设定, Form_v6_设置_功能设定.ModernPanel1),
            (10, Form_v6_设置_转译辅助, Form_v6_设置_转译辅助.ModernPanel1),
            (11, Form_v6_设置_更新选项, Form_v6_设置_更新选项.ModernPanel1),
            (12, Form_v6_设置_远程调用, Form_v6_设置_远程调用.ModernPanel1),
            (13, Form_v6_设置_Agent, Form_v6_设置_Agent.ModernPanel1),
            (16, Form_v6_设置_个性化, Form_v6_设置_个性化.ModernPanel1)
        }
        For Each 绑定项 In 页面绑定
            ModernTabListControl1.Items(绑定项.索引).BoundControl = 绑定项.页面
            绑定选项卡窗体背景透明(绑定项.面板)
        Next
    End Sub

    Shared Sub 绑定选项卡窗体背景透明(选项卡的根面板容器 As ModernPanel)
        If 选项卡的根面板容器 Is Nothing OrElse Not SP_UnLock OrElse 设置_v6.实例对象.SP_毛玻璃模式 <= 0 Then Return
        选项卡的根面板容器.BackColor = Color.Transparent
        选项卡的根面板容器.BackColor1 = Color.Transparent
        选项卡的根面板容器.BackgroundSource = FormMain_v6
    End Sub

End Class
