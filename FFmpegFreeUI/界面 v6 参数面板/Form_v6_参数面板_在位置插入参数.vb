Public Class Form_v6_参数面板_在位置插入参数
    Private Sub Form_v6_参数面板_在位置插入参数_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Form_v6_参数面板_在位置插入参数_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged
        Dim a = Me.ModernPanel1.Height - Me.ModernPanel1.Padding.Top - Me.ModernPanel1.Padding.Bottom
        a -= Me.HtmlColorLabel1.Height
        a -= Me.HtmlColorLabel2.Height
        a -= Me.HtmlColorLabel3.Height
        a -= Me.HtmlColorLabel4.Height
        Me.ModernTextBox1.Height = a / 4
        Me.ModernTextBox2.Height = Me.ModernTextBox1.Height
        Me.ModernTextBox3.Height = Me.ModernTextBox1.Height
        Me.ModernTextBox4.Height = Me.ModernTextBox1.Height
    End Sub
End Class