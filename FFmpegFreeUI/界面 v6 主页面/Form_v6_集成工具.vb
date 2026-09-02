Imports LakeUI

Public Class Form_v6_集成工具
    Private 已自动刷新质量评测Vmaf模型列表 As Boolean = False

    Private Sub Form_v6_集成工具_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim 页面绑定 As (索引 As Integer, 页面 As Control, 面板 As ModernPanel)() = {
            (0, Form_v6_集成工具_合并, Form_v6_集成工具_合并.ModernPanel1),
            (1, Form_v6_集成工具_混流, Form_v6_集成工具_混流.ModernPanel1),
            (2, Form_v6_集成工具_抽流, Form_v6_集成工具_抽流.ModernPanel1),
            (4, Form_v6_集成工具_质量评测, Form_v6_集成工具_质量评测.ModernPanel1),
            (5, Form_v6_集成工具_Whisper生成字幕, Form_v6_集成工具_Whisper生成字幕.ModernPanel1)
        }
        For Each 绑定项 In 页面绑定
            ModernTabListControl1.Items(绑定项.索引).BoundControl = 绑定项.页面
            绑定选项卡(绑定项.面板)
        Next

        If SP_UnLock AndAlso 设置_v6.实例对象.窗口样式 = 2 AndAlso 设置_v6.实例对象.SP_毛玻璃模式 > 0 Then
            For Each 绑定项 In 页面绑定.Take(4)
                绑定项.面板.Padding = New Padding(10, 20, 20, 20)
            Next
        End If
    End Sub

    Private Sub 绑定选项卡(选项卡的根面板容器 As ModernPanel)
        If 选项卡的根面板容器 Is Nothing OrElse Not SP_UnLock OrElse 设置_v6.实例对象.SP_毛玻璃模式 <= 0 Then Return
        选项卡的根面板容器.BackColor = Color.Transparent
        选项卡的根面板容器.BackColor1 = Color.Transparent
        选项卡的根面板容器.BackgroundSource = ParentForm
    End Sub

    Private Async Sub ModernTabListControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ModernTabListControl1.SelectedIndexChanged
        If ModernTabListControl1.SelectedIndex = 4 AndAlso Not 已自动刷新质量评测Vmaf模型列表 Then
            已自动刷新质量评测Vmaf模型列表 = True
            Await Form_v6_集成工具_质量评测.刷新Vmaf模型列表Async()
        End If
    End Sub

End Class
