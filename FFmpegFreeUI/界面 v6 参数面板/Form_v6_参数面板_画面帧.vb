Public Class Form_v6_参数面板_画面帧

    Public 所属参数面板对象 As Form_v6_参数面板

    Private Sub Form_v6_参数面板_画面帧_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Form_v6_参数面板_画面帧_Shown(sender As Object, e As EventArgs) Handles Me.Shown

    End Sub

    Private Sub Form_v6_参数面板_画面帧_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged

    End Sub

    Private Sub ModernButton画面裁剪交互_Click(sender As Object, e As EventArgs) Handles ModernButton画面裁剪交互.Click
        所属参数面板对象.弹出画面区域选择窗口(ModernTextBox1, "画面裁剪")
    End Sub

    Public 私有窗口_抽帧参数 As New Form_v6_参数面板_抽帧参数
    Private Sub ModernButton抽帧设置_Click(sender As Object, e As EventArgs) Handles ModernButton抽帧设置.Click
        显示窗体(私有窗口_抽帧参数, FormMain_v6)
    End Sub
End Class