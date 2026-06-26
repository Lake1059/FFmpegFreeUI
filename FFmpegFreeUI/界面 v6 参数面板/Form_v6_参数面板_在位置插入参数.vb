Public Class Form_v6_参数面板_在位置插入参数
    Private Sub Form_v6_参数面板_在位置插入参数_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Form_v6_参数面板_在位置插入参数_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged
        Dim a = Me.ModernPanel1.Height - Me.ModernPanel1.Padding.Top - Me.ModernPanel1.Padding.Bottom
        a -= Me.HCL_开头参数说明.Height
        a -= Me.HCL_之前参数说明.Height
        a -= Me.HCL_之后参数说明.Height
        a -= Me.HCL_最后参数说明.Height
        Me.MTB_开头参数.Height = a / 4
        Me.MTB_之前参数.Height = Me.MTB_开头参数.Height
        Me.MTB_之后参数.Height = Me.MTB_开头参数.Height
        Me.MTB_最后参数.Height = Me.MTB_开头参数.Height
    End Sub
End Class