Public Class Form_v6_参数面板_降噪
    Private Sub Form_v6_参数面板_降噪_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = FormMain_v6.Icon
        SetControlFont(设置_v6.实例对象.字体, Me, , True)
        If FormMain_v6.ThisIsYourWindow1.AttachedForms.Count > 0 Then
            FormMain_v6.ThisIsYourWindow1.Attach(Me)
            ModernPanel1.BackColor = Color.Transparent
            ModernPanel1.BackColor1 = Color.Transparent
        End If
        MCB_滤镜选择_SelectedIndexChanged(MCB_滤镜选择, EventArgs.Empty)
    End Sub

    Private Sub Form_v6_参数面板_降噪_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        e.Cancel = True
        Me.Hide()
    End Sub

    Private Sub MCB_插帧总开关_CheckedChanged(sender As Object, e As EventArgs) Handles MCB_插帧总开关.CheckedChanged
        If MCB_插帧总开关.Checked = False Then
            MCB_滤镜选择.SelectedIndex = -1
        End If
    End Sub

    Private Sub MCB_滤镜选择_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_滤镜选择.SelectedIndexChanged
        隐藏全部降噪参数()

        Select Case MCB_滤镜选择.SelectedIndex
            Case 1  'hqdn3d - 时空域降噪，去除普通噪声并保留细节
                配置降噪参数(1, "亮度空间强度 luma_spatial 默认 4", 0, 20, 4)
                配置降噪参数(2, "色度空间强度 chroma_spatial 默认 3", 0, 20, 3)
                配置降噪参数(3, "亮度时间强度 luma_tmp 默认 6", 0, 20, 6)
                配置降噪参数(4, "色度时间强度 chroma_tmp 默认 4.5", 0, 20, 4.5)

            Case 2  'nlmeans - 高级降噪，效果更好且计算量更大
                配置降噪参数(1, "降噪强度 s (strength) 默认 1.0", 1, 30, 1)
                配置降噪参数(2, "参考像素块大小 p (patch size) 默认 7，须奇数", 0, 99, 7, 0, 1)
                配置降噪参数(3, "色度参考像素块大小 pc 默认 0，须奇数", 0, 99, 0, 0, 1)
                配置降噪参数(4, "搜索半径 r (research size) 默认 15", 0, 99, 15, 0, 1)

            Case 3  'atadenoise - 轻量级时间域降噪
                配置降噪参数(1, "亮度静态帧加权 0a 默认 0.02", 0, 0.3, 0.02, 2, 0.01)
                配置降噪参数(2, "亮度动态帧加权 0b 默认 0.04", 0, 5, 0.04, 2, 0.01)
                配置降噪参数(3, "色度静态加权 1a 默认 0.02", 0, 0.3, 0.02, 2, 0.01)
                配置降噪参数(4, "色度动态加权 1b 默认 0.04", 0, 5, 0.04, 2, 0.01)

            Case 4  'bm3d - 高质量降噪，适合严重噪声且消耗大量性能
                配置降噪参数(1, "噪声强度 sigma 默认 1", 0, 100, 1)
                配置降噪参数(2, "块大小 block 默认 16", 8, 64, 16, 0, 1)
                配置降噪参数(3, "块步长 bstep 默认 4", 1, 64, 4, 0, 1)
                配置降噪参数(4, "相似块数量 group 默认 1", 1, 256, 1, 0, 1)

            Case 5  'bilateral_cuda - CUDA 加速双边滤波
                配置降噪参数(1, "空间 sigma sigmaS 默认 0.1", 0.1, 512, 0.1)
                配置降噪参数(2, "范围 sigma sigmaR 默认 0.1", 0.1, 512, 0.1)
                配置降噪参数(3, "邻域窗口大小 window_size 默认 1", 1, 255, 1, 0, 1)
        End Select
    End Sub

    Private Sub 隐藏全部降噪参数()
        重置降噪参数(Panel1, HtmlColorLabel1, ETB_降噪参数1)
        重置降噪参数(Panel2, HtmlColorLabel2, ETB_降噪参数2)
        重置降噪参数(Panel3, HtmlColorLabel3, ETB_降噪参数3)
        重置降噪参数(Panel4, HtmlColorLabel4, ETB_降噪参数4)
    End Sub

    Private Sub 重置降噪参数(参数面板 As Panel, 参数名称 As LakeUI.HtmlColorLabel, 参数滑块 As LakeUI.ExcellentTrackBar)
        参数面板.Visible = False
        参数名称.Text = ""
        参数滑块.Minimum = 0
        参数滑块.Maximum = 10
        参数滑块.SmallChange = 0.1
        参数滑块.LargeChange = 1
        参数滑块.ThumbTextDecimalPlaces = 1
        参数滑块.Value = 0
    End Sub

    Private Sub 配置降噪参数(序号 As Integer, 参数名称文本 As String, 最小值 As Double, 最大值 As Double, 默认值 As Double, Optional 小数位数 As Integer = 1, Optional 步进 As Double = 0.1)
        Dim 参数面板 As Panel
        Dim 参数滑块 As LakeUI.ExcellentTrackBar
        Dim 参数名称 As LakeUI.HtmlColorLabel
        Select Case 序号
            Case 1
                参数面板 = Panel1
                参数名称 = HtmlColorLabel1
                参数滑块 = ETB_降噪参数1
            Case 2
                参数面板 = Panel2
                参数名称 = HtmlColorLabel2
                参数滑块 = ETB_降噪参数2
            Case 3
                参数面板 = Panel3
                参数名称 = HtmlColorLabel3
                参数滑块 = ETB_降噪参数3
            Case 4
                参数面板 = Panel4
                参数名称 = HtmlColorLabel4
                参数滑块 = ETB_降噪参数4
            Case Else
                Exit Sub
        End Select

        参数面板.Visible = True
        参数名称.Text = 参数名称文本
        参数滑块.Minimum = 最小值
        参数滑块.Maximum = 最大值
        参数滑块.SmallChange = 步进
        参数滑块.LargeChange = 步进
        参数滑块.ThumbTextDecimalPlaces = 小数位数
        参数滑块.Value = 默认值
    End Sub
End Class
