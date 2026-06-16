Public Class Form_v6_参数面板_胶片颗粒
    Private Sub Form_v6_参数面板_胶片颗粒_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = FormMain_v6.Icon
        SetControlFont(设置_v6.实例对象.字体, Me, , True)
        If FormMain_v6.ThisIsYourWindow1.AttachedForms.Count > 0 Then
            FormMain_v6.ThisIsYourWindow1.Attach(Me)
            ModernPanel1.BackColor = Color.Transparent
            ModernPanel1.BackColor1 = Color.Transparent
        End If
        MCB_滤镜选择_SelectedIndexChanged(MCB_滤镜选择, EventArgs.Empty)
    End Sub

    Private Sub Form_v6_参数面板_胶片颗粒_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        e.Cancel = True
        Me.Hide()
    End Sub

    Private Sub MCB_胶片颗粒总开关_CheckedChanged(sender As Object, e As EventArgs) Handles MCB_胶片颗粒总开关.CheckedChanged
        If MCB_胶片颗粒总开关.Checked = False Then
            MCB_滤镜选择.SelectedIndex = -1
        End If
    End Sub

    Private Sub MCB_滤镜选择_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_滤镜选择.SelectedIndexChanged
        隐藏全部胶片颗粒参数()

        Select Case MCB_滤镜选择.SelectedIndex
            Case 1  'noise=alls={强度}:allf=t+u:all_seed={随机种子}
                配置胶片颗粒参数(1, "颗粒强度 all_strength 默认 6，范围 0~100", 0, 100, 6, 0, 1)
                配置胶片颗粒参数(2, "随机种子 all_seed 默认 -1 自动随机", -1, 2147483647, -1, 0, 1)

            Case 2  'noise=c0s={亮度强度}:c0f=t+u:c1s={色度强度}:c1f=t+u:c2s={色度强度}:c2f=t+u:all_seed={随机种子}
                配置胶片颗粒参数(1, "亮度颗粒强度 c0_strength 默认 6，范围 0~100", 0, 100, 6, 0, 1)
                配置胶片颗粒参数(2, "色度颗粒强度 c1/c2_strength 默认 2，范围 0~100", 0, 100, 2, 0, 1)
                配置胶片颗粒参数(3, "随机种子 all_seed 默认 -1 自动随机", -1, 2147483647, -1, 0, 1)

            Case 3  'noise=alls={强度}:allf=t+a+u:all_seed={随机种子}
                配置胶片颗粒参数(1, "柔和颗粒强度 all_strength 默认 5，范围 0~100", 0, 100, 5, 0, 1)
                配置胶片颗粒参数(2, "随机种子 all_seed 默认 -1 自动随机", -1, 2147483647, -1, 0, 1)

            Case 4  'libplacebo=apply_filmgrain=true
                配置胶片颗粒参数(1, "应用片源 film grain 元数据 固定 true", 1, 1, 1, 0, 1)
        End Select
    End Sub

    Private Sub 隐藏全部胶片颗粒参数()
        重置胶片颗粒参数(Panel1, HtmlColorLabel1, ETB_胶片颗粒参数1)
        重置胶片颗粒参数(Panel2, HtmlColorLabel2, ETB_胶片颗粒参数2)
        重置胶片颗粒参数(Panel3, HtmlColorLabel3, ETB_胶片颗粒参数3)
        重置胶片颗粒参数(Panel4, HtmlColorLabel4, ETB_胶片颗粒参数4)
    End Sub

    Private Sub 重置胶片颗粒参数(参数面板 As Panel, 参数名称 As LakeUI.HtmlColorLabel, 参数滑块 As LakeUI.ExcellentTrackBar)
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

    Private Sub 配置胶片颗粒参数(序号 As Integer, 参数名称文本 As String, 最小值 As Double, 最大值 As Double, 默认值 As Double, Optional 小数位数 As Integer = 1, Optional 步进 As Double = 0.1)
        Dim 参数面板 As Panel
        Dim 参数滑块 As LakeUI.ExcellentTrackBar
        Dim 参数名称 As LakeUI.HtmlColorLabel
        Select Case 序号
            Case 1
                参数面板 = Panel1
                参数名称 = HtmlColorLabel1
                参数滑块 = ETB_胶片颗粒参数1
            Case 2
                参数面板 = Panel2
                参数名称 = HtmlColorLabel2
                参数滑块 = ETB_胶片颗粒参数2
            Case 3
                参数面板 = Panel3
                参数名称 = HtmlColorLabel3
                参数滑块 = ETB_胶片颗粒参数3
            Case 4
                参数面板 = Panel4
                参数名称 = HtmlColorLabel4
                参数滑块 = ETB_胶片颗粒参数4
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
