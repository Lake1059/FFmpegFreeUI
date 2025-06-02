
Public Class 界面控制_视频参数

    Public Shared Sub 视频编码器参数开关事件()
        If Form1.UiCheckBox视频编码器.Checked Then
            If Form1.UiComboBox编码类别.Text = "" Then Form1.UiComboBox编码类别.SelectedIndex = 0
        Else
            Form1.UiComboBox编码类别.Text = ""
            Form1.UiComboBox具体编码.Items.Clear()
            Form1.UiComboBox具体编码.Text = ""
            Form1.UiComboBox编码预设.Items.Clear()
            Form1.UiComboBox编码预设.Text = ""
        End If
    End Sub
    Public Shared Sub 视频编码类别改动事件()
        Form1.UiComboBox具体编码.Items.Clear()
        Form1.UiComboBox具体编码.Text = ""
        Select Case Form1.UiComboBox编码类别.SelectedIndex
            Case 0
                Form1.UiComboBox具体编码.Items.Add("copy")
                Form1.UiCheckBox视频编码器.Checked = True
                Form1.UiComboBox具体编码.SelectedIndex = 0
            Case 1    'H.266/VVC
                Form1.UiComboBox具体编码.Items.Add("libx266")
                Form1.UiComboBox具体编码.Items.Add("libvvenc")
                Form1.UiCheckBox视频编码器.Checked = True
                Form1.UiComboBox具体编码.SelectedIndex = 0
            Case 2    'AV1
                Form1.UiComboBox具体编码.Items.Add("libaom-av1")
                Form1.UiComboBox具体编码.Items.Add("av1_nvenc")
                Form1.UiComboBox具体编码.Items.Add("av1_qsv")
                Form1.UiComboBox具体编码.Items.Add("av1_amf")
                Form1.UiComboBox具体编码.Items.Add("libsvtav1")
                Form1.UiComboBox具体编码.Items.Add("rav1e")
                Form1.UiCheckBox视频编码器.Checked = True
                Form1.UiComboBox具体编码.SelectedIndex = 0
            Case 3    'H.265/HEVC
                Form1.UiComboBox具体编码.Items.Add("libx265")
                Form1.UiComboBox具体编码.Items.Add("hevc_nvenc")
                Form1.UiComboBox具体编码.Items.Add("hevc_qsv")
                Form1.UiComboBox具体编码.Items.Add("hevc_amf")
                Form1.UiCheckBox视频编码器.Checked = True
                Form1.UiComboBox具体编码.SelectedIndex = 0
            Case 4    'H.264/AVC
                Form1.UiComboBox具体编码.Items.Add("libx264")
                Form1.UiComboBox具体编码.Items.Add("h264_nvenc")
                Form1.UiComboBox具体编码.Items.Add("h264_qsv")
                Form1.UiComboBox具体编码.Items.Add("h264_amf")
                Form1.UiCheckBox视频编码器.Checked = True
                Form1.UiComboBox具体编码.SelectedIndex = 0
            Case 5    'ProRes
                Form1.UiComboBox具体编码.Items.Add("prores_ks")
                Form1.UiCheckBox视频编码器.Checked = True
                Form1.UiComboBox具体编码.SelectedIndex = 0
            Case 6    'VP9
                Form1.UiComboBox具体编码.Items.Add("libvpx-vp9")
                Form1.UiCheckBox视频编码器.Checked = True
                Form1.UiComboBox具体编码.SelectedIndex = 0
        End Select

    End Sub
    Public Shared Sub 视频具体编码改动事件()
        Form1.UiComboBox编码预设.Items.Clear()
        Form1.UiComboBox编码预设.Text = ""
        Form1.UiComboBox配置文件.Items.Clear()
        Form1.UiComboBox配置文件.Text = ""
        Form1.UiComboBox场景优化.Items.Clear()
        Form1.UiComboBox场景优化.Text = ""
        Form1.UiComboBox像素格式.Items.Clear()
        Form1.UiComboBox像素格式.Text = ""

        If Form1.UiComboBox具体编码.SelectedIndex > -1 Then
            Form1.UiComboBox编码预设.Items.AddRange(视频编码器数据库.字典(Form1.UiComboBox具体编码.Text).Preset.ToArray)
            If Form1.UiComboBox具体编码.Text = "libsvtav1" Then
                Form1.UiComboBox编码预设.SelectedIndex = 3
            Else
                Form1.UiComboBox编码预设.SelectedIndex = 0
            End If
            Form1.UiComboBox配置文件.Items.AddRange(视频编码器数据库.字典(Form1.UiComboBox具体编码.Text).Profile.ToArray)
            Form1.UiComboBox场景优化.Items.AddRange(视频编码器数据库.字典(Form1.UiComboBox具体编码.Text).Tune.ToArray)
            Form1.UiComboBox像素格式.Items.AddRange(视频编码器数据库.字典(Form1.UiComboBox具体编码.Text).Pix_fmt.ToArray)
        End If
    End Sub

    Public Shared Sub 视频分辨率参数开关按下事件()
        If Not Form1.UiCheckBox分辨率.Checked Then
            Form1.UiComboBox分辨率.Text = ""
            Form1.UiTextBox分辨率自动计算宽度.Text = ""
            Form1.UiTextBox分辨率自动计算高度.Text = ""
        End If
    End Sub
    Public Shared Sub 视频分辨率文本框文本变动事件()
        If Form1.UiComboBox分辨率.Text = "" Then
            If Form1.UiTextBox分辨率自动计算宽度.Text = "" AndAlso Form1.UiTextBox分辨率自动计算高度.Text = "" Then
                Form1.UiCheckBox分辨率.Checked = False
            End If
        Else
            Form1.UiCheckBox分辨率.Checked = True
            Form1.UiTextBox分辨率自动计算宽度.Text = ""
            Form1.UiTextBox分辨率自动计算高度.Text = ""
        End If
    End Sub
    Public Shared Sub 视频分辨率宽度文本框文本变动事件()
        If Form1.UiTextBox分辨率自动计算宽度.Text <> "" Then
            Form1.UiTextBox分辨率自动计算高度.Text = ""
            Form1.UiCheckBox分辨率.Checked = True
            Form1.UiComboBox分辨率.Text = ""
        Else
            If Form1.UiTextBox分辨率自动计算高度.Text = "" AndAlso Form1.UiComboBox分辨率.Text = "" Then
                Form1.UiCheckBox分辨率.Checked = False
            End If
        End If
    End Sub
    Public Shared Sub 视频分辨率高度文本框文本变动事件()
        If Form1.UiTextBox分辨率自动计算高度.Text <> "" Then
            Form1.UiTextBox分辨率自动计算宽度.Text = ""
            Form1.UiCheckBox分辨率.Checked = True
            Form1.UiComboBox分辨率.Text = ""
        Else
            If Form1.UiTextBox分辨率自动计算宽度.Text = "" AndAlso Form1.UiComboBox分辨率.Text = "" Then
                Form1.UiCheckBox分辨率.Checked = False
            End If
        End If
    End Sub

    Public Shared Sub 视频帧速率参数开关按下事件()
        If Not Form1.UiCheckBox帧速率.Checked Then
            Form1.UiComboBox帧速率.Text = ""
            Form1.UiTextBox智能抽帧阈值.Text = ""
        End If
    End Sub
    Public Shared Sub 视频帧速率改动事件()
        If Form1.UiComboBox帧速率.Text = "" Then
            If Form1.UiTextBox智能抽帧阈值.Text = "" Then
                Form1.UiCheckBox帧速率.Checked = False
            Else
                Form1.UiCheckBox帧速率.Checked = True
            End If
        Else
            Form1.UiCheckBox帧速率.Checked = True
        End If
    End Sub

    Public Shared Sub 视频比特率参数开关按下事件()
        If Not Form1.UiCheckBox比特率.Checked Then
            Form1.UiComboBox比特率控制方式.Text = ""
            Form1.UiTextBox基础比特率.Text = ""
            Form1.UiTextBox最低比特率.Text = ""
            Form1.UiTextBox最高比特率.Text = ""
            Form1.UiTextBox比特率缓冲区.Text = ""
            Form1.UiTextBox硬件加速HQ前瞻分析帧数.Text = ""
            Form1.Panel比特率参数.Visible = False
        End If
    End Sub

    Public Shared Sub 视频比特率控制方式改动事件()
        Select Case Form1.UiComboBox比特率控制方式.SelectedIndex
            Case 0    '动态码率 VBR - 存储首选，硬件加速首选
                Form1.Panel比特率参数.Visible = True
                Form1.UiCheckBox比特率.Checked = True
                If Form1.UiComboBox全局质量控制参数.SelectedIndex = 3 Then Form1.UiComboBox全局质量控制参数.SelectedIndex = 0
            Case 1    '动态码率 VBR HQ - 硬件加速专用，极致质量
                Form1.Panel比特率参数.Visible = True
                Form1.UiCheckBox比特率.Checked = True
                If Form1.UiComboBox全局质量控制参数.SelectedIndex = 3 Then Form1.UiComboBox全局质量控制参数.SelectedIndex = 0
            Case 2    '恒定质量 CRF - 存储首选，软件编码首选
                Form1.Panel比特率参数.Visible = True
                Form1.UiCheckBox比特率.Checked = True

                Form1.UiCheckBox质量控制.Checked = True
                Form1.UiComboBox全局质量控制参数.SelectedIndex = 3
            Case 3    '恒定量化 CQP - 不推荐，主用于研究和特定场景
                Form1.Panel比特率参数.Visible = True
                Form1.UiCheckBox比特率.Checked = True

                Form1.UiCheckBox质量控制.Checked = True
                Form1.UiComboBox全局质量控制参数.SelectedIndex = 2
            Case 4    '平均码率 ABR - 折中方案，在一定范围内波动
                Form1.Panel比特率参数.Visible = True
                Form1.UiCheckBox比特率.Checked = True
                If Form1.UiComboBox全局质量控制参数.SelectedIndex = 3 Then Form1.UiComboBox全局质量控制参数.SelectedIndex = 0
            Case 5    '二次编码 TPE - 花费时间节省比特率
                Form1.Panel比特率参数.Visible = True
                Form1.UiCheckBox比特率.Checked = True
                If Form1.UiComboBox全局质量控制参数.SelectedIndex = 3 Then Form1.UiComboBox全局质量控制参数.SelectedIndex = 0
            Case 6    '恒定速率 CBR - 流媒体常用，不适合存储
                Form1.Panel比特率参数.Visible = True
                Form1.UiCheckBox比特率.Checked = True
                If Form1.UiComboBox全局质量控制参数.SelectedIndex = 3 Then Form1.UiComboBox全局质量控制参数.SelectedIndex = 0
        End Select
    End Sub

    Public Shared Sub 视频质量控制开关按下事件()
        If Not Form1.UiCheckBox质量控制.Checked Then
            Form1.UiComboBox全局质量控制参数.Text = ""
            Form1.UiTextBox全局质量控制值.Text = ""
            Form1.UiCheckBox展开精细控制.Checked = False
            Form1.UiTextBox硬件加速HQ前瞻分析帧数.Text = ""
            Form1.UiTextBox质量最小值.Text = ""
            Form1.UiTextBox质量最大值.Text = ""
            Form1.UiTextBox相邻帧质量变化限制.Text = ""
            Form1.UiTextBox关键帧质量.Text = ""
            Form1.UiTextBox关键帧质量基准.Text = ""
            Form1.UiTextBox关键帧质量固定偏移.Text = ""
            Form1.UiTextBox前向预测帧质量.Text = ""
            Form1.UiTextBoxP对I的比例.Text = ""
            Form1.UiTextBox前向预测帧质量固定偏移.Text = ""
            Form1.UiTextBox双向预测帧质量.Text = ""
            Form1.UiTextBoxB对P的比例.Text = ""
            Form1.UiTextBox双向预测帧质量固定偏移.Text = ""
            Form1.Panel17.Visible = False
            Form1.Panel18.Visible = False
            Form1.Panel20.Visible = False
            Form1.Panel27.Visible = False
        End If
    End Sub
    Public Shared Sub 视频质量控制展开精细控制开关变动事件()
        Select Case Form1.UiCheckBox展开精细控制.Checked
            Case True
                Form1.Panel27.Visible = True
                Form1.Panel20.Visible = True
                Form1.Panel18.Visible = True
                Form1.Panel17.Visible = True
                Form1.Panel硬件加速HQ参数.Visible = True
                Form1.UiCheckBox质量控制.Checked = True
            Case False
                Form1.Panel硬件加速HQ参数.Visible = False
                Form1.Panel17.Visible = False
                Form1.Panel18.Visible = False
                Form1.Panel20.Visible = False
                Form1.Panel27.Visible = False
        End Select
    End Sub


    Public Shared Sub 视频快速剪辑参数开关按下事件()
        If Not Form1.UiCheckBox快速剪辑.Checked Then
            Form1.UiTextBox快速剪辑入点.Text = ""
            Form1.UiTextBox快速剪辑出点.Text = ""
        End If
    End Sub
    Public Shared Sub 视频快速剪辑文本框文本变动事件()
        If Form1.UiTextBox快速剪辑入点.Text = "" AndAlso Form1.UiTextBox快速剪辑出点.Text = "" Then
            Form1.UiCheckBox快速剪辑.Checked = False
        Else
            Form1.UiCheckBox快速剪辑.Checked = True
        End If
    End Sub

    '==============================================

    Public Shared Sub 视频帧排列参数开关按下事件()
        If Not Form1.UiCheckBox帧排列.Checked Then
            Form1.UiTextBox关键帧间隔.Text = ""
            Form1.UiTextBox双向预测帧数量.Text = ""
        End If
    End Sub
    Public Shared Sub 视频帧排列参数变动事件()
        If Form1.UiTextBox关键帧间隔.Text = "" AndAlso Form1.UiTextBox双向预测帧数量.Text = "" Then
            Form1.UiCheckBox帧排列.Checked = False
        Else
            Form1.UiCheckBox帧排列.Checked = True
        End If
    End Sub

    '==============================================

    Public Shared Sub 视频画面规格参数开关按下事件()
        If Not Form1.UiCheckBox画面规格.Checked Then
            Form1.UiComboBox配置文件.Text = ""
            Form1.UiComboBox场景优化.Text = ""
            Form1.UiComboBox像素格式.Text = ""
        End If
    End Sub
    Public Shared Sub 视频画面规格参数变动事件()
        If Form1.UiComboBox像素格式.Text.Contains("="c) Then Form1.UiComboBox像素格式.Text = ""
        If Form1.UiComboBox配置文件.Text = "" AndAlso Form1.UiComboBox场景优化.Text = "" AndAlso Form1.UiComboBox像素格式.Text = "" Then
            Form1.UiCheckBox画面规格.Checked = False
        Else
            Form1.UiCheckBox画面规格.Checked = True
        End If
    End Sub

    '==============================================

    Public Shared Sub 视频色彩管理参数开关按下事件()
        If Not Form1.UiCheckBox画面规格.Checked Then
            Form1.UiComboBox矩阵系数.Text = ""
            Form1.UiComboBox色域.Text = ""
            Form1.UiComboBox传输特性.Text = ""
            Form1.UiComboBox色彩范围.Text = ""
            Form1.UiComboBox色彩管理处理方式.Text = ""
        End If
    End Sub

    Public Shared Sub 视频色彩管理参数变动事件()
        If Form1.UiComboBox矩阵系数.Text <> "" AndAlso
            Form1.UiComboBox色域.Text <> "" AndAlso
            Form1.UiComboBox传输特性.Text <> "" AndAlso
            Form1.UiComboBox色彩范围.Text <> "" AndAlso
            Form1.UiComboBox色彩管理处理方式.Text <> "" Then
            Form1.UiCheckBox画面规格.Checked = True
        End If
    End Sub

    '==============================================

    Public Shared Sub 视频降噪参数开关按下事件()
        If Not Form1.UiCheckBox画面规格.Checked Then
            Form1.UiComboBox降噪方式.Text = ""
            Form1.UiTextBox降噪参数1.Text = ""
            Form1.UiTextBox降噪参数2.Text = ""
            Form1.UiTextBox降噪参数3.Text = ""
            Form1.UiTextBox降噪参数4.Text = ""
            Form1.Panel28.Visible = False
        End If
    End Sub
    Public Shared Sub 视频降噪方式变动事件()
        If Form1.UiComboBox降噪方式.SelectedIndex > -1 Then
            Form1.UiTextBox降噪参数1.Text = ""
            Form1.UiTextBox降噪参数2.Text = ""
            Form1.UiTextBox降噪参数3.Text = ""
            Form1.UiTextBox降噪参数4.Text = ""
            Form1.Panel28.Visible = True
        End If
        Select Case Form1.UiComboBox降噪方式.SelectedIndex
            Case 0 'hqdn3d - 时空域降噪，去除普通噪声并保留细节
                Form1.Label降噪参数1.Text = "亮度空间强度" & vbCrLf & "luma_spatial"
                Form1.UiTextBox降噪参数1.Watermark = "默认 4"
                Form1.Label降噪参数2.Text = "色度空间强度" & vbCrLf & "chroma_spatial"
                Form1.UiTextBox降噪参数2.Watermark = "默认 3"
                Form1.Label降噪参数3.Text = "亮度时间强度" & vbCrLf & "luma_tmp"
                Form1.UiTextBox降噪参数3.Watermark = "默认 6"
                Form1.Label降噪参数4.Text = "色度时间强度" & vbCrLf & "chroma_tmp"
                Form1.UiTextBox降噪参数4.Watermark = "默认 1"
            Case 1    'nlmeans - 高级降噪，效果更好且计算量更大
                Form1.Label降噪参数1.Text = "降噪强度" & vbCrLf & "范围 1~10"
                Form1.UiTextBox降噪参数1.Watermark = "默认 1"
                Form1.Label降噪参数2.Text = "参考像素块大小" & vbCrLf & "必须是奇数"
                Form1.UiTextBox降噪参数2.Watermark = "默认 7"
                Form1.Label降噪参数3.Text = "色度降噪" & vbCrLf & "启用 = 1 禁用 = 0"
                Form1.UiTextBox降噪参数3.Watermark = ""
                Form1.Label降噪参数4.Text = "搜索半径" & vbCrLf & "越大越慢"
                Form1.UiTextBox降噪参数4.Watermark = "默认 7"
            Case 2  'atadenoise - 轻量级时间域降噪
                Form1.Label降噪参数1.Text = "亮度静态帧加权" & vbCrLf & "0a"
                Form1.UiTextBox降噪参数1.Watermark = "默认 0.02"
                Form1.Label降噪参数2.Text = "亮度动态帧加权" & vbCrLf & "0b"
                Form1.UiTextBox降噪参数2.Watermark = "默认 0.04"
                Form1.Label降噪参数3.Text = "色度静态加权" & vbCrLf & "1a"
                Form1.UiTextBox降噪参数3.Watermark = "默认 0.02"
                Form1.Label降噪参数4.Text = "色度动态加权" & vbCrLf & "1b"
                Form1.UiTextBox降噪参数4.Watermark = "默认 0.04"
            Case 3    'bm3d - 高质量降噪，适合严重噪声且消耗大量性能
                Form1.Label降噪参数1.Text = "噪声强度" & vbCrLf & "sigma"
                Form1.UiTextBox降噪参数1.Watermark = "默认 1"
                Form1.Label降噪参数2.Text = "块大小 建议 4~8" & vbCrLf & "block"
                Form1.UiTextBox降噪参数2.Watermark = "默认 4"
                Form1.Label降噪参数3.Text = "块步长" & vbCrLf & "bstep"
                Form1.UiTextBox降噪参数3.Watermark = "block/2"
                Form1.Label降噪参数4.Text = "相似块数量" & vbCrLf & "group"
                Form1.UiTextBox降噪参数4.Watermark = "默认 1"
        End Select
    End Sub

    '==============================================
    Public Shared Sub 视频锐化参数开关按下事件()
        If Not Form1.UiCheckBox锐化.Checked Then
            Form1.UiTextBox锐化水平尺寸.Text = ""
            Form1.UiTextBox锐化垂直尺寸.Text = ""
            Form1.UiTextBox锐化强度.Text = ""
        End If
    End Sub
    Public Shared Sub 视频锐化参数变动事件()
        If Form1.UiTextBox锐化水平尺寸.Text <> "" AndAlso
            Form1.UiTextBox锐化垂直尺寸.Text <> "" AndAlso
            Form1.UiTextBox锐化强度.Text <> "" Then
            Form1.UiCheckBox锐化.Checked = False
        End If
    End Sub

    '==============================================
    Public Shared Sub 视频逐行与隔行参数开关按下事件()
        If Not Form1.UiCheckBox逐行与隔行.Checked Then
            Form1.UiComboBox逐行与隔行处理方式.Text = ""
        End If
    End Sub
    Public Shared Sub 视频逐行与隔行参数变动事件()
        If Form1.UiComboBox逐行与隔行处理方式.Text <> "" Then
            Form1.UiCheckBox逐行与隔行.Checked = False
        End If
    End Sub

    '==============================================

    Public Shared Sub 视频自定义滤镜开关按下事件()
        If Not Form1.UiCheckBox自定义视频滤镜.Checked Then
            Form1.UiTextBox自定义视频滤镜.Text = ""
        End If
    End Sub
    Public Shared Sub 视频自定义滤镜参数变动事件()
        If Form1.UiTextBox自定义视频滤镜.Text = "" Then
            Form1.UiCheckBox自定义视频滤镜.Checked = False
        Else
            Form1.UiCheckBox自定义视频滤镜.Checked = True
        End If
    End Sub
    Public Shared Sub 视频自定义参数开关按下事件()
        If Not Form1.UiCheckBox自定义视频参数.Checked Then
            Form1.UiTextBox自定义视频参数.Text = ""
        End If
    End Sub
    Public Shared Sub 视频自定义参数变动事件()
        If Form1.UiTextBox自定义视频参数.Text = "" Then
            Form1.UiCheckBox自定义视频参数.Checked = False
        Else
            Form1.UiCheckBox自定义视频参数.Checked = True
        End If
    End Sub

End Class
