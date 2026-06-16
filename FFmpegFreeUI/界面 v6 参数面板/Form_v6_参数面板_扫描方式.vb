Public Class Form_v6_参数面板_扫描方式

    Private Sub Form_v6_参数面板_扫描方式_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = FormMain_v6.Icon
        SetControlFont(设置_v6.实例对象.字体, Me, , True)
        If FormMain_v6.ThisIsYourWindow1.AttachedForms.Count > 0 Then
            FormMain_v6.ThisIsYourWindow1.Attach(Me)
            ModernPanel1.BackColor = Color.Transparent
            ModernPanel1.BackColor1 = Color.Transparent
        End If
    End Sub

    Private Sub Form_v6_参数面板_扫描方式_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        e.Cancel = True
        Me.Hide()
    End Sub

    Private Sub MCB_扫描方式总开关_CheckedChanged(sender As Object, e As EventArgs) Handles MCB_扫描方式总开关.CheckedChanged
        If MCB_扫描方式总开关.Checked = False Then
            MCB_扫描方式.SelectedIndex = -1
        End If
    End Sub

    Private Sub MCB_扫描方式_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_扫描方式.SelectedIndexChanged
    End Sub
End Class
