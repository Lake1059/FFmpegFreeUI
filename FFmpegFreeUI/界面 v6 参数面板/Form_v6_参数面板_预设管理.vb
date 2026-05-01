Public Class Form_v6_参数面板_预设管理
    Private Sub Form_v6_参数面板_预设管理_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Form_v6_参数面板_预设管理_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged
        Me.ModernListBox1.Width = (Me.Panel2.Width - Me.JustEmptyControl1.Width * 2) / 3
        Me.ModernTextBox1.Width = Me.ModernListBox1.Width


    End Sub
End Class