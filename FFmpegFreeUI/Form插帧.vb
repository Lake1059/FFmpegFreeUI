Public Class Form插帧
    Private Sub Form插帧_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetControlFont(Form1.Font.Name, Me)
    End Sub

    Private Sub Form插帧_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        校准UiComboBox高DPI(UiComboBox插帧模式)
        校准UiComboBox高DPI(UiComboBox运动估计模式)
        校准UiComboBox高DPI(UiComboBox运动估计算法)
        校准UiComboBox高DPI(UiComboBox运动补偿模式)
        UiCheckBox可变块大小的运动补偿.CheckBoxSize = 20 * Form1.DPI
    End Sub

    Private Sub Form插帧_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If UiTextBox要补到多少帧.Text = "" Or UiComboBox插帧模式.Text = "" Then
            重置所有选项()
        End If
        e.Cancel = True
        Me.Hide()
        Form1.Focus()
    End Sub

    Sub 重置所有选项()
        UiTextBox要补到多少帧.Text = ""
        UiComboBox插帧模式.Text = ""
        UiComboBox运动估计模式.Text = ""
        UiComboBox运动估计算法.Text = ""
        UiComboBox运动补偿模式.Text = ""
        UiCheckBox可变块大小的运动补偿.Checked = False
        UiTextBox块大小.Text = ""
        UiTextBox搜索范围.Text = ""
        UiTextBox场景变化检测强度.Text = ""
    End Sub



End Class