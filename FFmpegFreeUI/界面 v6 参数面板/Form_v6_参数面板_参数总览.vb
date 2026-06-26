Imports LakeUI

Public Class Form_v6_参数面板_参数总览
    Private Sub Form_v6_参数面板_参数总览_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Form_v6_参数面板_参数总览_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged
        Me.Panel1.Width = Me.ClientSize.Width * 0.5
    End Sub

    Private Sub ModernButton1_Click(sender As Object, e As EventArgs) Handles MB_复制参数总览.Click
        If MTB_参数总览.Text <> "" Then Clipboard.SetText(MTB_参数总览.Text)
        ExFloatingTip(MB_复制参数总览, "已复制参数总览", 1200)
    End Sub

    Private Sub ModernButton2_Click(sender As Object, e As EventArgs) Handles MB_复制命令行模板.Click
        If MTB_命令行模板.Text <> "" Then Clipboard.SetText(MTB_命令行模板.Text)
        ExFloatingTip(MB_复制命令行模板, "已复制命令行模板", 1200)
    End Sub

End Class
