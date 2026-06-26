Public Class Form_v6_参数面板_流自定义参数
    Private Sub Form_v6_参数面板_流自定义参数_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Form_v6_参数面板_流自定义参数_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged
        Me.MTB_视频流自定义参数.Height = (Me.ClientSize.Height - Me.Padding.Top * 2) * 0.5
    End Sub
End Class