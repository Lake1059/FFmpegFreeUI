Public Class Form_v6_设置_界面显示
    Private Sub Form_v6_设置_界面显示_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub MCB_全局字体_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_全局字体.SelectedIndexChanged
        设置_v6.实例对象.字体 = MCB_全局字体.Text
        字体控制.更新所有控件字体属性()
    End Sub
End Class
