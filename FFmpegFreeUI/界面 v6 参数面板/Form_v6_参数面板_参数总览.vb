Public Class Form_v6_参数面板_参数总览
    Private Sub Form_v6_参数面板_参数总览_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Form_v6_参数面板_参数总览_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged
        Me.Panel1.Width = Me.ClientSize.Width * 0.5
    End Sub
End Class