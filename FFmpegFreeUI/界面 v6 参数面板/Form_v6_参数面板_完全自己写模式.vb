Public Class Form_v6_参数面板_完全自己写模式

    Private Sub ModernButton1_Click(sender As Object, e As EventArgs) Handles MB_插入输入文件占位符.Click
        插入占位符("<InputFile>")
    End Sub

    Private Sub ModernButton2_Click(sender As Object, e As EventArgs) Handles MB_插入输出文件占位符.Click
        插入占位符("<OutputFile>")
    End Sub

    Private Sub 插入占位符(text As String)
        MTB_完整命令行参数.SelectedText = text
        MTB_完整命令行参数.Focus()
    End Sub

End Class
