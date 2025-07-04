
Public Class Form帧混合
    Private Sub Form帧混合_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Form帧混合_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        UiComboBox混合算法.ItemHeight = 30 * Form1.DPI
    End Sub

    Private Sub Form帧混合_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If UiComboBox混合算法.Text = "" Then
            重置所有选项()
        End If
        e.Cancel = True
        Me.Hide()
        Form1.Focus()
    End Sub

    Sub 重置所有选项()
        UiTextBox降低帧率.Text = ""
        UiComboBox混合算法.Text = ""
        UiTextBox混合比例.Text = ""
    End Sub



End Class