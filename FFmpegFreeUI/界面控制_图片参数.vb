Public Class 界面控制_图片参数

    Public Shared Sub 图片编码器参数开关按下事件()
        If Not Form1.UiCheckBox图片编码器.Checked Then
            Form1.UiComboBox图片编码器.Text = ""
            Form1.UiTextBox图片编码器质量.Text = ""
        End If
    End Sub
    Public Shared Sub 图片编码器参数变动事件()
        If Form1.UiComboBox图片编码器.Text <> "" Then
            Form1.UiCheckBox图片编码器.Checked = True
        End If
    End Sub

    Public Shared Sub 图片分辨率参数开关按下事件()
        If Not Form1.UiCheckBox图片分辨率.Checked Then
            Form1.UiTextBox图片分辨率宽度.Text = ""
            Form1.UiTextBox图片分辨率高度.Text = ""
        End If
    End Sub
    Public Shared Sub 图片分辨率参数变动事件()
        If Form1.UiTextBox图片分辨率宽度.Text <> "" AndAlso Form1.UiTextBox图片分辨率高度.Text <> "" Then
            Form1.UiCheckBox图片分辨率.Checked = True
        ElseIf Form1.UiTextBox图片分辨率宽度.Text = "" AndAlso Form1.UiTextBox图片分辨率高度.Text = "" Then
            Form1.UiCheckBox图片分辨率.Checked = False
        End If
    End Sub

    Public Shared Sub 图片自定义参数开关按下事件()
        If Not Form1.UiCheckBox自定义图片参数.Checked Then
            Form1.UiTextBox自定义图片参数.Text = ""
        End If
    End Sub
    Public Shared Sub 图片自定义参数变动事件()
        If Form1.UiTextBox自定义图片参数.Text <> "" Then
            Form1.UiCheckBox自定义图片参数.Checked = True
        Else
            Form1.UiCheckBox自定义图片参数.Checked = False
        End If
    End Sub







End Class
