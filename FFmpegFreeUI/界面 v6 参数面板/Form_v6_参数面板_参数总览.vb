Imports LakeUI

Public Class Form_v6_参数面板_参数总览
    Private Sub Form_v6_参数面板_参数总览_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Form_v6_参数面板_参数总览_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged
        Me.Panel1.Width = Me.ClientSize.Width * 0.5
    End Sub

    Private Sub ModernButton1_Click(sender As Object, e As EventArgs) Handles ModernButton1.Click
        If ModernTextBox1.Text <> "" Then Clipboard.SetText(ModernTextBox1.Text)
        ExFloatingTip(ModernButton1, "已复制参数总览", 1200)
    End Sub

    Private Sub ModernButton2_Click(sender As Object, e As EventArgs) Handles ModernButton2.Click
        If ModernTextBox2.Text <> "" Then Clipboard.SetText(ModernTextBox2.Text)
        ExFloatingTip(ModernButton2, "已复制命令行模板", 1200)
    End Sub

End Class
