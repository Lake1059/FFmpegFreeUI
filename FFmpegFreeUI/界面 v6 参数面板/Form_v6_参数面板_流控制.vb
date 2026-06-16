Public Class Form_v6_参数面板_流控制

    Public 所属参数面板对象 As Form_v6_参数面板

    Private Sub ModernButton1_Click(sender As Object, e As EventArgs) Handles ModernButton1.Click
        Dim selector As New Form_v6_媒体流选择器(
            视频流文本目标对象:=ModernTextBox1,
            音频流文本目标对象:=ModernTextBox2,
            字幕流文本目标对象:=ModernTextBox3,
            文件索引:="0",
            视频流已选:=ModernTextBox1.Text,
            音频流已选:=ModernTextBox2.Text,
            字幕流已选:=ModernTextBox3.Text)
        selector.Text = "v6 媒体流选择器"
        AddHandler selector.FormClosed, Sub()
                                            If 所属参数面板对象 IsNot Nothing Then 所属参数面板对象.请求刷新参数状态()
                                        End Sub
        显示窗体(selector, FormMain_v6)
    End Sub

End Class
