Imports System.ComponentModel

Public Class Form_v6_参数面板_抽帧参数
    Private Sub Form_v6_参数面板_抽帧参数_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = FormMain_v6.Icon
        SetControlFont(设置_v6.实例对象.字体, Me, , True)
        If FormMain_v6.ThisIsYourWindow1.AttachedForms.Count > 0 Then
            FormMain_v6.ThisIsYourWindow1.Attach(Me)
            ModernPanel1.BackColor = Color.Transparent
            ModernPanel1.BackColor1 = Color.Transparent
        End If
    End Sub

    Private Sub Form_v6_参数面板_抽帧参数_Closing(sender As Object, e As CancelEventArgs) Handles Me.FormClosing
        e.Cancel = True
        Me.Hide()
    End Sub

    Private Sub Form_v6_参数面板_抽帧参数_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged
        Panel6.Width = Me.ClientSize.Width * 0.5
    End Sub
End Class