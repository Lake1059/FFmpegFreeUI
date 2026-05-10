Public Class Form_v6_设置_隐私设置
    Private Sub BooleanSwitch1_CheckedChanged(sender As Object, e As EventArgs) Handles BooleanSwitch1.CheckedChanged
        设置_v6.实例对象.是否参与用户统计 = BooleanSwitch1.Checked
    End Sub
End Class