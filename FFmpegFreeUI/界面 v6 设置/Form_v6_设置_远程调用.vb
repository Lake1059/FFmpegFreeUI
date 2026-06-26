Public Class Form_v6_设置_远程调用
    Private Sub BooleanSwitch1_CheckedChanged(sender As Object, e As EventArgs) Handles BooleanSwitch1.CheckedChanged
        设置_v6.实例对象.是否监听端口 = BooleanSwitch1.Checked
        If BooleanSwitch1.Checked Then
            端口监听_v6.启动客户端()
        Else
            端口监听_v6.停止客户端()
        End If
    End Sub

    Private Sub ModernTextBox1_TextChanged(sender As Object, e As EventArgs) Handles ModernTextBox1.TextChanged
        设置_v6.实例对象.监听的端口 = ModernTextBox1.Text
        If 设置_v6.实例对象.是否监听端口 AndAlso 端口监听_v6.是否正在运行 Then
            端口监听_v6.重启客户端()
        End If
    End Sub
End Class
