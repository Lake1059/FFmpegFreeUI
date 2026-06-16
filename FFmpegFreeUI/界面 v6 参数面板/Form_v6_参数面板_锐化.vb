Public Class Form_v6_参数面板_锐化
    Private Sub Form_v6_参数面板_锐化_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = FormMain_v6.Icon
        SetControlFont(设置_v6.实例对象.字体, Me, , True)
        If FormMain_v6.ThisIsYourWindow1.AttachedForms.Count > 0 Then
            FormMain_v6.ThisIsYourWindow1.Attach(Me)
            ModernPanel1.BackColor = Color.Transparent
            ModernPanel1.BackColor1 = Color.Transparent
        End If
        MCB_滤镜选择_SelectedIndexChanged(MCB_滤镜选择, EventArgs.Empty)
    End Sub

    Private Sub Form_v6_参数面板_锐化_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        e.Cancel = True
        Me.Hide()
    End Sub

    Private Sub MCB_锐化总开关_CheckedChanged(sender As Object, e As EventArgs) Handles MCB_锐化总开关.CheckedChanged
        If MCB_锐化总开关.Checked = False Then
            MCB_滤镜选择.SelectedIndex = -1
        End If
    End Sub

    Private Sub MCB_滤镜选择_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_滤镜选择.SelectedIndexChanged
        隐藏全部锐化参数()

        Select Case MCB_滤镜选择.SelectedIndex
            Case 1  'cas - Contrast Adaptive Sharpen
                配置锐化参数(1, "锐化强度 strength 默认 0，建议 0.20~0.35", 0, 1, 0.25, 2, 0.01)
                配置锐化参数(2, "处理颜色平面 planes 默认 7，1/2/4 分别对应前三个平面", 0, 15, 7, 0, 1)

            Case 2  'unsharp - 只暴露亮度平面的横向、纵向、强度
                配置锐化参数(1, "横向矩阵尺寸 luma_msize_x 默认 5，范围 3~23", 3, 23, 5, 0, 1)
                配置锐化参数(2, "纵向矩阵尺寸 luma_msize_y 默认 5，范围 3~23", 3, 23, 5, 0, 1)
                配置锐化参数(3, "锐化强度 luma_amount 默认 1，范围 -2~5", -2, 5, 1)

            Case 3  'sharpen_npp - NVIDIA NPP，ffmpegcuda 中只有固定 border_type
                配置锐化参数(1, "边界处理 border_type 固定 replicate=2，范围 2~2", 2, 2, 2, 0, 1)
        End Select
    End Sub

    Private Sub 隐藏全部锐化参数()
        重置锐化参数(Panel1, HtmlColorLabel1, ETB_锐化参数1)
        重置锐化参数(Panel2, HtmlColorLabel2, ETB_锐化参数2)
        重置锐化参数(Panel3, HtmlColorLabel3, ETB_锐化参数3)
    End Sub

    Private Sub 重置锐化参数(参数面板 As Panel, 参数名称 As LakeUI.HtmlColorLabel, 参数滑块 As LakeUI.ExcellentTrackBar)
        参数面板.Visible = False
        参数名称.Text = ""
        参数滑块.Enabled = True
        参数滑块.Minimum = 0
        参数滑块.Maximum = 10
        参数滑块.SmallChange = 0.1
        参数滑块.LargeChange = 1
        参数滑块.ThumbTextDecimalPlaces = 1
        参数滑块.Value = 0
    End Sub

    Private Sub 配置锐化参数(序号 As Integer, 参数名称文本 As String, 最小值 As Double, 最大值 As Double, 默认值 As Double, Optional 小数位数 As Integer = 1, Optional 步进 As Double = 0.1)
        Dim 参数面板 As Panel
        Dim 参数滑块 As LakeUI.ExcellentTrackBar
        Dim 参数名称 As LakeUI.HtmlColorLabel
        Select Case 序号
            Case 1
                参数面板 = Panel1
                参数名称 = HtmlColorLabel1
                参数滑块 = ETB_锐化参数1
            Case 2
                参数面板 = Panel2
                参数名称 = HtmlColorLabel2
                参数滑块 = ETB_锐化参数2
            Case 3
                参数面板 = Panel3
                参数名称 = HtmlColorLabel3
                参数滑块 = ETB_锐化参数3
            Case Else
                Exit Sub
        End Select

        参数面板.Visible = True
        参数名称.Text = 参数名称文本
        参数滑块.Minimum = 最小值
        参数滑块.Maximum = If(最大值 = 最小值, 最小值 + Math.Max(步进, 1), 最大值)
        参数滑块.SmallChange = 步进
        参数滑块.LargeChange = 步进
        参数滑块.ThumbTextDecimalPlaces = 小数位数
        参数滑块.Value = 默认值
        参数滑块.Enabled = 最大值 <> 最小值
    End Sub
End Class
