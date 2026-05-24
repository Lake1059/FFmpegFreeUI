Public Class Form_v6_设置_LakeUI视觉体验
    Private Sub MCB_窗口样式_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_窗口样式.SelectedIndexChanged
        If Not SP_UnLock Then Exit Sub
        设置_v6.实例对象.窗口样式 = MCB_窗口样式.SelectedIndex
    End Sub
End Class