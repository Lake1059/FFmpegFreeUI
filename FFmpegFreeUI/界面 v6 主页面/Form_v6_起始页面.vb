Public Class Form_v6_起始页面
    Private Sub Form_v6_起始页面_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Form_v6_起始页面_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged
        Dim a = (Me.ModernPanel1.Width - Me.ModernPanel1.Padding.Left * 2 - Me.JustEmptyControl2.Width * 2) / 3
        Me.ModernPanel4.Width = a
        Me.ModernPanel6.Width = a
    End Sub
End Class