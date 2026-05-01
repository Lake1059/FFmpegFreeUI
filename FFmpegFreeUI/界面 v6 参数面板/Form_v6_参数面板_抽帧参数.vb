Public Class Form_v6_参数面板_抽帧参数
    Private Sub Form_v6_参数面板_抽帧参数_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If FormMain_v6.ThisIsYourWindow1.AttachedForms.Count > 0 Then
            FormMain_v6.ThisIsYourWindow1.Attach(Me)
        End If
    End Sub
End Class