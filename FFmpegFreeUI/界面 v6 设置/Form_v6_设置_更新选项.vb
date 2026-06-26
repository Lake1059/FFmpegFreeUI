Imports LakeUI

Public Class Form_v6_设置_更新选项
    Private Sub MCB_更新服务器_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_更新服务器.SelectedIndexChanged
        If MCB_更新服务器.Text = "???" Then
            MCB_更新服务器.SelectedIndex = 0
            ExFloatingTip(sender, "所选服务器未解锁", 2000)
            Exit Sub
        End If
        设置_v6.实例对象.更新服务器选择 = MCB_更新服务器.SelectedIndex
    End Sub
End Class