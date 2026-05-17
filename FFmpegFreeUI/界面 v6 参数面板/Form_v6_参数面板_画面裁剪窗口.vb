Public Class Form_v6_参数面板_画面裁剪窗口
    Private Sub Form_v6_参数面板_画面裁剪窗口_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If FormMain_v6.ThisIsYourWindow1.AttachedForms.Count > 0 Then
            Me.ModernPanel1.BackColor = Color.Transparent
            FormMain_v6.ThisIsYourWindow1.Attach(Me)
        End If
    End Sub

    Private Sub Form_v6_参数面板_画面裁剪窗口_Shown(sender As Object, e As EventArgs) Handles Me.Shown

    End Sub
End Class