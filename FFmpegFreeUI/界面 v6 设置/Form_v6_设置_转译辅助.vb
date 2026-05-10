Public Class Form_v6_设置_转译辅助
    Private Sub MCB_替代进程的文件名_TextChanged(sender As Object, e As EventArgs) Handles MCB_替代进程的文件名.TextChanged
        设置_v6.实例对象.替代进程文件名 = MCB_替代进程的文件名.Text
    End Sub

    Private Sub MTB_覆盖参数传递_TextChanged(sender As Object, e As EventArgs) Handles MTB_覆盖参数传递.TextChanged
        设置_v6.实例对象.覆盖参数传递 = MTB_覆盖参数传递.Text
    End Sub

    Private Sub MCB_转译模式_CheckedChanged(sender As Object, e As EventArgs) Handles MCB_转译模式.CheckedChanged
        设置_v6.实例对象.转译模式 = MCB_转译模式.Checked
    End Sub
End Class