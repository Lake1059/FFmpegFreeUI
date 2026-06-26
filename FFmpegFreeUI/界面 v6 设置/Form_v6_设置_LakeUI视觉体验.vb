Public Class Form_v6_设置_LakeUI视觉体验
    Private Sub MCB_窗口样式_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_窗口样式.SelectedIndexChanged
        设置_v6.实例对象.窗口样式 = MCB_窗口样式.SelectedIndex
    End Sub

    Private Sub MCB_性能计数器_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_性能计数器.SelectedIndexChanged
        设置_v6.实例对象.启用性能计数器 = MCB_性能计数器.SelectedIndex
    End Sub
End Class