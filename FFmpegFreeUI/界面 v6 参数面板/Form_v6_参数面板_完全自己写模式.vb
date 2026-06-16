Public Class Form_v6_参数面板_完全自己写模式

    Private Sub ModernButton1_Click(sender As Object, e As EventArgs) Handles ModernButton1.Click
        插入占位符("<InputFile>")
    End Sub

    Private Sub ModernButton2_Click(sender As Object, e As EventArgs) Handles ModernButton2.Click
        插入占位符("<OutputFile>")
    End Sub

    Private Sub 插入占位符(text As String)
        ModernTextBox1.SelectedText = text
        ModernTextBox1.Focus()
    End Sub

End Class
