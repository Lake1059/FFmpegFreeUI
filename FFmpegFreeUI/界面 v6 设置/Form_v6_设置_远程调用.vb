Public Class Form_v6_设置_远程调用
    Private Sub BooleanSwitch1_CheckedChanged(sender As Object, e As EventArgs) Handles BooleanSwitch1.CheckedChanged
        设置_v6.实例对象.是否监听端口 = BooleanSwitch1.Checked
    End Sub

    Private Sub ModernTextBox1_TextChanged(sender As Object, e As EventArgs) Handles ModernTextBox1.TextChanged
        设置_v6.实例对象.监听的端口 = ModernTextBox1.Text
    End Sub
End Class