Public Class Form_v6_参数面板_平滑断层
    Private Sub Form_v6_参数面板_平滑断层_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = FormMain_v6.Icon
        SetControlFont(设置_v6.实例对象.字体, Me, , True)
        If FormMain_v6.ThisIsYourWindow1.AttachedForms.Count > 0 Then
            FormMain_v6.ThisIsYourWindow1.Attach(Me)
            ModernPanel1.BackColor = Color.Transparent
            ModernPanel1.BackColor1 = Color.Transparent
        End If
        MCB_滤镜选择_SelectedIndexChanged(MCB_滤镜选择, EventArgs.Empty)
    End Sub

    Private Sub Form_v6_参数面板_平滑断层_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        e.Cancel = True
        Me.Hide()
    End Sub

    Private Sub MCB_平滑断层总开关_CheckedChanged(sender As Object, e As EventArgs) Handles MCB_平滑断层总开关.CheckedChanged
        If MCB_平滑断层总开关.Checked = False Then
            MCB_滤镜选择.SelectedIndex = -1
        End If
    End Sub

    Private Sub MCB_滤镜选择_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_滤镜选择.SelectedIndexChanged
        隐藏全部平滑断层参数()

        Select Case MCB_滤镜选择.SelectedIndex
            Case 1  'deband=1thr={阈值}:2thr={阈值}:3thr={阈值}:range={范围}:direction={方向}:blur=1:coupling={平面耦合}
                配置平滑断层参数(1, "阈值 1thr/2thr/3thr 默认 0.020，范围 0.00003~0.5", 0.00003, 0.5, 0.02, 3, 0.001)
                配置平滑断层参数(2, "采样范围 range 默认 16，可按画面尺寸提高", 1, 128, 16, 0, 1)
                配置平滑断层参数(3, "方向 direction 默认 0，范围 -6.28~6.28", -6.28, 6.28, 0, 2, 0.01)
                配置平滑断层参数(4, "平面耦合 coupling 默认 0，0/1", 0, 1, 0, 0, 1)

            Case 2  'deband=1thr={阈值}:2thr={阈值}:3thr={阈值}:range={范围}:direction={方向}:blur=1:coupling={平面耦合}
                配置平滑断层参数(1, "强力阈值 1thr/2thr/3thr 默认 0.035，范围 0.00003~0.5", 0.00003, 0.5, 0.035, 3, 0.001)
                配置平滑断层参数(2, "强力采样范围 range 默认 32，可按画面尺寸提高", 1, 256, 32, 0, 1)
                配置平滑断层参数(3, "方向 direction 默认 0，范围 -6.28~6.28", -6.28, 6.28, 0, 2, 0.01)
                配置平滑断层参数(4, "平面耦合 coupling 默认 1，0/1", 0, 1, 1, 0, 1)

            Case 3  'gradfun=strength={强度}:radius={半径}
                配置平滑断层参数(1, "平滑强度 strength 默认 1.2，范围 0.51~64", 0.51, 64, 1.2, 2, 0.01)
                配置平滑断层参数(2, "渐变拟合半径 radius 默认 16，范围 4~32", 4, 32, 16, 0, 1)

            Case 4  'libplacebo=deband=true:deband_iterations={迭代}:deband_threshold={阈值}:deband_radius={半径}:deband_grain={颗粒}
                配置平滑断层参数(1, "迭代次数 deband_iterations 默认 1，范围 0~16", 0, 16, 1, 0, 1)
                配置平滑断层参数(2, "阈值 deband_threshold 默认 4，范围 0~1024", 0, 1024, 4)
                配置平滑断层参数(3, "半径 deband_radius 默认 16，范围 0~1024", 0, 1024, 16)
                配置平滑断层参数(4, "补偿颗粒 deband_grain 默认 6，范围 0~1024", 0, 1024, 6)
        End Select
    End Sub

    Private Sub 隐藏全部平滑断层参数()
        重置平滑断层参数(Panel1, HtmlColorLabel1, ETB_平滑断层参数1)
        重置平滑断层参数(Panel2, HtmlColorLabel2, ETB_平滑断层参数2)
        重置平滑断层参数(Panel3, HtmlColorLabel3, ETB_平滑断层参数3)
        重置平滑断层参数(Panel4, HtmlColorLabel4, ETB_平滑断层参数4)
    End Sub

    Private Sub 重置平滑断层参数(参数面板 As Panel, 参数名称 As LakeUI.HtmlColorLabel, 参数滑块 As LakeUI.ExcellentTrackBar)
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

    Private Sub 配置平滑断层参数(序号 As Integer, 参数名称文本 As String, 最小值 As Double, 最大值 As Double, 默认值 As Double, Optional 小数位数 As Integer = 1, Optional 步进 As Double = 0.1)
        Dim 参数面板 As Panel
        Dim 参数滑块 As LakeUI.ExcellentTrackBar
        Dim 参数名称 As LakeUI.HtmlColorLabel
        Select Case 序号
            Case 1
                参数面板 = Panel1
                参数名称 = HtmlColorLabel1
                参数滑块 = ETB_平滑断层参数1
            Case 2
                参数面板 = Panel2
                参数名称 = HtmlColorLabel2
                参数滑块 = ETB_平滑断层参数2
            Case 3
                参数面板 = Panel3
                参数名称 = HtmlColorLabel3
                参数滑块 = ETB_平滑断层参数3
            Case 4
                参数面板 = Panel4
                参数名称 = HtmlColorLabel4
                参数滑块 = ETB_平滑断层参数4
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
