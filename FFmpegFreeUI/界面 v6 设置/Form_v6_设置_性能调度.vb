Public Class Form_v6_设置_性能调度
    Private Sub MTB_处理器线程_TextChanged(sender As Object, e As EventArgs) Handles MTB_处理器线程.TextChanged
        设置_v6.实例对象.指定处理器核心 = MTB_处理器线程.Text
    End Sub

    Private Sub ModernTextBox2_TextChanged(sender As Object, e As EventArgs) Handles ModernTextBox2.TextChanged

    End Sub

    Private Sub MCB_自动开始数量_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_自动开始数量.SelectedIndexChanged
        设置_v6.实例对象.自动同时运行任务数量选项 = MCB_自动开始数量.SelectedIndex
    End Sub

    Private Sub MCB_编码队列刷新速度_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_编码队列刷新速度.SelectedIndexChanged
        设置_v6.实例对象.编码队列刷新速度 = MCB_编码队列刷新速度.SelectedIndex
    End Sub
End Class