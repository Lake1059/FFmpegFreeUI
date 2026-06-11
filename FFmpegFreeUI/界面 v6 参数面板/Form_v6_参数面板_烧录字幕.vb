Public Class Form_v6_参数面板_烧录字幕
    Private Sub Form_v6_参数面板_烧录字幕_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = FormMain_v6.Icon
        SetControlFont(设置_v6.实例对象.字体, Me, , True)
        If FormMain_v6.ThisIsYourWindow1.AttachedForms.Count > 0 Then
            FormMain_v6.ThisIsYourWindow1.Attach(Me)
            ModernPanel1.BackColor = Color.Transparent
            ModernPanel1.BackColor1 = Color.Transparent
        End If
    End Sub

    Private Sub Form_v6_参数面板_烧录字幕_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        e.Cancel = True
        Me.Hide()
    End Sub

    Private Sub Form_v6_参数面板_烧录字幕_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged
        Panel1.Width = 320 * DPI()
        MCB_滤镜选择.Width = (Panel1.Width - JustEmptyControl1.Width) * 0.5
        ModernComboBox3.Width = (Panel1.Width - JustEmptyControl2.Width - JustEmptyControl3.Width) / 3
        ModernComboBox2.Width = ModernComboBox3.Width
        Panel2.Width = 350 * DPI()
        Panel3.Width = 150 * DPI()
    End Sub

    Function DPI() As Double
        Return Me.DeviceDpi / 96
    End Function

End Class