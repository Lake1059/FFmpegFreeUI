Public Class Form_v6_设置
    Public 私有界面_LakeUI性能选项 As New Form_v6_设置_LakeUI性能选项
    Public 私有界面_LakeUI视觉体验 As New Form_v6_设置_LakeUI视觉体验
    Public 私有界面_LakeUI许可信息 As New Form_v6_设置_LakeUI许可证

    Public 私有界面_界面显示 As New Form_v6_设置_界面显示
    Public 私有界面_性能调度 As New Form_v6_设置_性能调度
    Public 私有界面_功能设定 As New Form_v6_设置_功能设定
    Public 私有界面_转译辅助 As New Form_v6_设置_转译辅助
    Public 私有界面_更新选项 As New Form_v6_设置_更新选项
    Public 私有界面_隐私设置 As New Form_v6_设置_隐私设置
    Public 私有界面_远程调用 As New Form_v6_设置_远程调用

    Public 私有界面_个性化 As New Form_v6_设置_个性化

    Private Sub Form_v6_设置_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.ModernTabListControl1.Items(1).BoundControl = 私有界面_LakeUI性能选项
        Me.ModernTabListControl1.Items(2).BoundControl = 私有界面_LakeUI视觉体验
        Me.ModernTabListControl1.Items(3).BoundControl = 私有界面_LakeUI许可信息

        Me.ModernTabListControl1.Items(6).BoundControl = 私有界面_界面显示
        Me.ModernTabListControl1.Items(7).BoundControl = 私有界面_性能调度
        Me.ModernTabListControl1.Items(8).BoundControl = 私有界面_功能设定
        Me.ModernTabListControl1.Items(9).BoundControl = 私有界面_转译辅助
        Me.ModernTabListControl1.Items(10).BoundControl = 私有界面_更新选项
        Me.ModernTabListControl1.Items(11).BoundControl = 私有界面_隐私设置
        Me.ModernTabListControl1.Items(12).BoundControl = 私有界面_远程调用

        Me.ModernTabListControl1.Items(15).BoundControl = 私有界面_个性化
    End Sub

    Private Sub Form_v6_设置_Shown(sender As Object, e As EventArgs) Handles Me.Shown

    End Sub

End Class