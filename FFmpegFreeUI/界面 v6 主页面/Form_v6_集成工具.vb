Imports LakeUI

Public Class Form_v6_集成工具
    Private Sub Form_v6_集成工具_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.ModernTabListControl1.Items(0).BoundControl = Form_v6_集成工具_合并
        绑定选项卡(Form_v6_集成工具_合并.ModernPanel1)
        Me.ModernTabListControl1.Items(1).BoundControl = Form_v6_集成工具_混流
        绑定选项卡(Form_v6_集成工具_混流.ModernPanel1)
        Me.ModernTabListControl1.Items(2).BoundControl = Form_v6_集成工具_抽流
        绑定选项卡(Form_v6_集成工具_抽流.ModernPanel1)
        Me.ModernTabListControl1.Items(4).BoundControl = Form_v6_集成工具_质量评测
        绑定选项卡(Form_v6_集成工具_质量评测.ModernPanel1)
        Me.ModernTabListControl1.Items(5).BoundControl = Form_v6_集成工具_Whisper生成字幕
        绑定选项卡(Form_v6_集成工具_Whisper生成字幕.ModernPanel1)

        If SP_UnLock AndAlso 设置_v6.实例对象.窗口样式 = 2 AndAlso 设置_v6.实例对象.SP_毛玻璃模式 > 0 Then
            Form_v6_集成工具_合并.ModernPanel1.Padding = New Padding(10, 20, 20, 20)
            Form_v6_集成工具_混流.ModernPanel1.Padding = New Padding(10, 20, 20, 20)
            Form_v6_集成工具_抽流.ModernPanel1.Padding = New Padding(10, 20, 20, 20)
            Form_v6_集成工具_质量评测.ModernPanel1.Padding = New Padding(10, 20, 20, 20)
        End If
    End Sub

    Private Sub 绑定选项卡(选项卡的根面板容器 As ModernPanel)
        If SP_UnLock Then
            Select Case 设置_v6.实例对象.SP_毛玻璃模式
                Case > 0
                    选项卡的根面板容器.BackColor = Color.Transparent
                    选项卡的根面板容器.BackColor1 = Color.Transparent
                    选项卡的根面板容器.BackgroundSource = Me.ParentForm
            End Select
        End If
    End Sub

End Class
