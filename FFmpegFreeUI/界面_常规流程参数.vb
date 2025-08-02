
Imports System.IO

Public Class 界面_常规流程参数
    Private Sub 界面_常规流程参数_Load(sender As Object, e As EventArgs) Handles Me.Load
        AddHandler UiComboBox编码类别.SelectedIndexChanged, AddressOf 视频编码类别改动事件
        AddHandler UiComboBox具体编码.SelectedIndexChanged, AddressOf 视频具体编码改动事件
        '==============================================
        AddHandler UiComboBox分辨率.TextChanged, AddressOf 视频分辨率变动事件
        AddHandler UiTextBox分辨率自动计算宽度.TextChanged, AddressOf 视频分辨率自动计算宽度变动事件
        AddHandler UiTextBox分辨率自动计算高度.TextChanged, AddressOf 视频分辨率自动计算高度变动事件
        AddHandler UiButton1.Click, Sub() 显示窗体(Form画面裁剪交互窗口, Form1)
        '==============================================
        AddHandler UiButton打开插帧参数窗口.Click, Sub()
                                               显示窗体(Form插帧, Form1)
                                               SetControlFont(Form1.UiComboBox字体名称.Text, Form插帧)
                                           End Sub
        AddHandler UiButton打开动态模糊参数窗口.Click, Sub()
                                                 显示窗体(Form帧混合, Form1)
                                                 SetControlFont(Form1.UiComboBox字体名称.Text, Form帧混合)
                                             End Sub
        '==============================================
        AddHandler UiComboBox比特率控制方式.SelectedIndexChanged, AddressOf 视频比特率控制方式改动事件
        '==============================================
        AddHandler UiComboBox色彩管理处理方式.TextChanged, AddressOf 色彩管理处理方式变动事件
        '==============================================
        AddHandler UiComboBox降噪方式.TextChanged, AddressOf 视频降噪方式变动事件
        '==========================================
        AddHandler UiComboBox音频编码器.TextChanged, AddressOf 音频编码参数变动事件
        AddHandler UiComboBox音频比特率控制方式.TextChanged, AddressOf 音频比特率控制方式参数变动事件
        '==========================================
        AddHandler UiComboBox图片编码器.TextChanged, AddressOf 图片编码器参数变动事件
        '==============================================
        AddHandler UiButton22.Click, AddressOf 预设管理.保存预设到文件
        AddHandler UiButton21.Click, AddressOf 预设管理.从文件读取预设
        AddHandler UiButton13.Click, AddressOf 预设管理.重置全部包含在预设中的设置
        UiComboBox自动命名选项.SelectedIndex = 0

        AddHandler UiComboBox自动加载预设选项.SelectedIndexChanged, Sub()
                                                                用户设置.实例对象.自动加载预设选项 = UiComboBox自动加载预设选项.SelectedIndex
                                                                Select Case UiComboBox自动加载预设选项.SelectedIndex
                                                                    Case 用户设置.自动加载预设选项枚举.不自动加载预设, 用户设置.自动加载预设选项枚举.自动加载上次的全部改动
                                                                        UiTextBox自动加载的预设文件路径.Text = ""
                                                                End Select
                                                            End Sub
        AddHandler UiButton选择加载指定预设文件.Click, Sub()
                                                 Dim d As New OpenFileDialog With {.Filter = "Json|*.json"}
                                                 d.ShowDialog(Form1)
                                                 If Not File.Exists(d.FileName) Then Exit Sub
                                                 UiTextBox自动加载的预设文件路径.Text = d.FileName
                                                 UiComboBox自动加载预设选项.SelectedIndex = 用户设置.自动加载预设选项枚举.自动加载指定的预设文件
                                             End Sub
        AddHandler UiTextBox自动加载的预设文件路径.TextChanged, Sub()
                                                         If UiTextBox自动加载的预设文件路径.Text <> "" AndAlso UiComboBox自动加载预设选项.SelectedIndex = 用户设置.自动加载预设选项枚举.自动加载指定的预设文件 Then
                                                             用户设置.实例对象.自动加载预设文件路径 = UiTextBox自动加载的预设文件路径.Text
                                                         Else
                                                             用户设置.实例对象.自动加载预设文件路径 = ""
                                                         End If
                                                     End Sub

        设置富文本框行高(RichTextBox1, 350)
        界面校准()
    End Sub

    Sub 视频编码类别改动事件()
        UiComboBox具体编码.Items.Clear()
        UiComboBox具体编码.Text = ""
        Select Case Form1.常规流程参数页面.UiComboBox编码类别.SelectedIndex
            Case 0
                UiComboBox编码预设.Items.Clear()
                UiComboBox编码预设.Text = ""
                UiComboBox配置文件.Items.Clear()
                UiComboBox配置文件.Text = ""
                UiComboBox场景优化.Items.Clear()
                UiComboBox场景优化.Text = ""
                UiComboBox像素格式.Items.Clear()
                UiComboBox像素格式.Text = ""
            Case 1
                UiComboBox具体编码.Items.Add("copy")
                UiComboBox具体编码.SelectedIndex = 0
            Case 2    'H.266/VVC
                UiComboBox具体编码.Items.Add("libx266")
                UiComboBox具体编码.Items.Add("libvvenc")
            Case 3    'AV1
                UiComboBox具体编码.Items.Add("libaom-av1")
                UiComboBox具体编码.Items.Add("libsvtav1")
                UiComboBox具体编码.Items.Add("av1_nvenc")
                UiComboBox具体编码.Items.Add("av1_qsv")
                UiComboBox具体编码.Items.Add("av1_amf")
                UiComboBox具体编码.Items.Add("librav1e")
                UiComboBox具体编码.Items.Add("av1_vaapi")
            Case 4    'H.265/HEVC
                UiComboBox具体编码.Items.Add("libx265")
                UiComboBox具体编码.Items.Add("hevc_nvenc")
                UiComboBox具体编码.Items.Add("hevc_qsv")
                UiComboBox具体编码.Items.Add("hevc_amf")
                UiComboBox具体编码.Items.Add("hevc_d3d12va")
                UiComboBox具体编码.Items.Add("hevc_vaapi")
                UiComboBox具体编码.Items.Add("hevc_vulkan")
            Case 5    'H.264/AVC
                UiComboBox具体编码.Items.Add("libx264")
                UiComboBox具体编码.Items.Add("h264_nvenc")
                UiComboBox具体编码.Items.Add("h264_qsv")
                UiComboBox具体编码.Items.Add("h264_amf")
                UiComboBox具体编码.Items.Add("h264_vaapi")
                UiComboBox具体编码.Items.Add("h264_vulkan")
            Case 6    'ProRes
                UiComboBox具体编码.Items.Add("prores_ks")
                UiComboBox具体编码.Items.Add("prores_aw")
            Case 7    'FFV1
                UiComboBox具体编码.Items.Add("ffv1 -level 3")
                UiComboBox具体编码.Items.Add("ffv1 -level 1")
            Case 8    'VP9
                UiComboBox具体编码.Items.Add("libvpx-vp9")
                UiComboBox具体编码.Items.Add("vp9_vaapi")
            Case 9    'RMVB
                UiComboBox具体编码.Items.Add("rv20")
                UiComboBox具体编码.Items.Add("rv10")
            Case 10    'MPEG
                UiComboBox具体编码.Items.Add("mpeg4")
                UiComboBox具体编码.Items.Add("libxvid")
                UiComboBox具体编码.Items.Add("libxeve")
            Case 11    'WMV
                UiComboBox具体编码.Items.Add("wmv2")
                UiComboBox具体编码.Items.Add("wmv1")
            Case 12    '禁用
                UiComboBox具体编码.Items.Add("")
        End Select
        If UiComboBox具体编码.Items.Count > 1 Then UiComboBox具体编码.SelectedIndex = 0
    End Sub
    Sub 视频具体编码改动事件()
        UiComboBox编码预设.Items.Clear()
        UiComboBox编码预设.Text = ""
        UiComboBox配置文件.Items.Clear()
        UiComboBox配置文件.Text = ""
        UiComboBox场景优化.Items.Clear()
        UiComboBox场景优化.Text = ""
        UiComboBox像素格式.Items.Clear()
        UiComboBox像素格式.Text = ""
        If UiComboBox具体编码.SelectedIndex > -1 AndAlso 视频编码器数据库.字典.ContainsKey(UiComboBox具体编码.Text) Then
            UiComboBox编码预设.Items.AddRange(视频编码器数据库.字典(UiComboBox具体编码.Text).Preset.ToArray)
            If UiComboBox具体编码.Text = "libsvtav1" Then
                UiComboBox编码预设.SelectedIndex = 3
            Else
                UiComboBox编码预设.SelectedIndex = 0
            End If
            UiComboBox配置文件.Items.AddRange(视频编码器数据库.字典(UiComboBox具体编码.Text).Profile.ToArray)
            UiComboBox场景优化.Items.AddRange(视频编码器数据库.字典(UiComboBox具体编码.Text).Tune.ToArray)
            UiComboBox像素格式.Items.AddRange(视频编码器数据库.字典(UiComboBox具体编码.Text).Pix_fmt.ToArray)
        End If
    End Sub
    Sub 视频分辨率变动事件()
        If UiComboBox分辨率.Text <> "" Then
            UiTextBox分辨率自动计算宽度.Text = ""
            UiTextBox分辨率自动计算高度.Text = ""
        End If
    End Sub
    Sub 视频分辨率自动计算宽度变动事件()
        If UiTextBox分辨率自动计算宽度.Text <> "" Then
            UiTextBox分辨率自动计算高度.Text = ""
            UiComboBox分辨率.Text = ""
        End If
    End Sub
    Sub 视频分辨率自动计算高度变动事件()
        If UiTextBox分辨率自动计算高度.Text <> "" Then
            UiTextBox分辨率自动计算宽度.Text = ""
            UiComboBox分辨率.Text = ""
        End If
    End Sub
    Sub 视频比特率控制方式改动事件()
        Select Case UiComboBox比特率控制方式.SelectedIndex
            Case 0
                If UiComboBox全局质量控制参数.SelectedIndex = 3 Then UiComboBox全局质量控制参数.SelectedIndex = 0
            Case 1    '动态码率 VBR - 存储首选，硬件加速首选
                UiComboBox全局质量控制参数.SelectedIndex = 1
            Case 2    '动态码率 VBR HQ - 硬件加速专用，极致质量
                UiComboBox全局质量控制参数.SelectedIndex = 1
            Case 3    '恒定质量 CRF - 存储首选，软件编码首选
                UiComboBox全局质量控制参数.SelectedIndex = 3
            Case 4    '恒定量化 CQP - 不推荐，主用于研究和特定场景
                UiComboBox全局质量控制参数.SelectedIndex = 2
            Case 5    '恒定速率 CBR - 流媒体常用，不适合存储
                UiComboBox全局质量控制参数.SelectedIndex = 0
                UiTextBox全局质量控制值.Text = ""
        End Select
    End Sub
    Sub 视频降噪方式变动事件()
        Label129.Visible = False
        Select Case UiComboBox降噪方式.SelectedIndex
            Case 1 'hqdn3d - 时空域降噪，去除普通噪声并保留细节
                Label降噪参数1.Text = "亮度空间强度" & vbCrLf & "luma_spatial"
                UiTextBox降噪参数1.Watermark = "默认 4"
                Label降噪参数2.Text = "色度空间强度" & vbCrLf & "chroma_spatial"
                UiTextBox降噪参数2.Watermark = "默认 3"
                Label降噪参数3.Text = "亮度时间强度" & vbCrLf & "luma_tmp"
                UiTextBox降噪参数3.Watermark = "默认 6"
                Label降噪参数4.Text = "色度时间强度" & vbCrLf & "chroma_tmp"
                UiTextBox降噪参数4.Watermark = "默认 1"
                Panel39.Visible = True
                Panel38.Visible = True
            Case 2    'nlmeans - 高级降噪，效果更好且计算量更大
                Label降噪参数1.Text = "降噪强度" & vbCrLf & "范围 1~10"
                UiTextBox降噪参数1.Watermark = "默认 1"
                Label降噪参数2.Text = "参考像素块大小" & vbCrLf & "必须是奇数"
                UiTextBox降噪参数2.Watermark = "默认 7"
                Label降噪参数3.Text = "色度降噪" & vbCrLf & "启用 = 1 禁用 = 0"
                UiTextBox降噪参数3.Watermark = ""
                Label降噪参数4.Text = "搜索半径" & vbCrLf & "越大越慢"
                UiTextBox降噪参数4.Watermark = "默认 7"
                Panel39.Visible = True
                Panel38.Visible = True
            Case 3  'atadenoise - 轻量级时间域降噪
                Label降噪参数1.Text = "亮度静态帧加权" & vbCrLf & "0a"
                UiTextBox降噪参数1.Watermark = "默认 0.02"
                Label降噪参数2.Text = "亮度动态帧加权" & vbCrLf & "0b"
                UiTextBox降噪参数2.Watermark = "默认 0.04"
                Label降噪参数3.Text = "色度静态加权" & vbCrLf & "1a"
                UiTextBox降噪参数3.Watermark = "默认 0.02"
                Label降噪参数4.Text = "色度动态加权" & vbCrLf & "1b"
                UiTextBox降噪参数4.Watermark = "默认 0.04"
                Panel39.Visible = True
                Panel38.Visible = True
            Case 4    'bm3d - 高质量降噪，适合严重噪声且消耗大量性能
                Label降噪参数1.Text = "噪声强度" & vbCrLf & "sigma"
                UiTextBox降噪参数1.Watermark = "默认 1"
                Label降噪参数2.Text = "块大小 建议 4~8" & vbCrLf & "block"
                UiTextBox降噪参数2.Watermark = "默认 4"
                Label降噪参数3.Text = "块步长" & vbCrLf & "bstep"
                UiTextBox降噪参数3.Watermark = "block/2"
                Label降噪参数4.Text = "相似块数量" & vbCrLf & "group"
                UiTextBox降噪参数4.Watermark = "默认 1"
                Panel39.Visible = True
                Panel38.Visible = True
            Case 5   '外部 AviSynth - 加载 avs 文件
                UiTextBox降噪参数1.Text = ""
                UiTextBox降噪参数2.Text = ""
                UiTextBox降噪参数3.Text = ""
                UiTextBox降噪参数4.Text = ""
                Panel38.Visible = False
                Panel39.Visible = False
                Label129.Visible = True
            Case Else
                UiTextBox降噪参数1.Text = ""
                UiTextBox降噪参数2.Text = ""
                UiTextBox降噪参数3.Text = ""
                UiTextBox降噪参数4.Text = ""
                Panel38.Visible = False
                Panel39.Visible = False
        End Select
    End Sub
    Sub 色彩管理处理方式变动事件()
        If UiComboBox色彩管理处理方式.Text = "" Then
            UiComboBox矩阵系数.Text = ""
            UiComboBox色域.Text = ""
            UiComboBox传输特性.Text = ""
            UiComboBox色彩范围.Text = ""
        End If
    End Sub

    Sub 音频编码参数变动事件()
        Select Case UiComboBox音频编码器.Text
            Case "复制流", "禁用"
                UiComboBox音频比特率控制方式.SelectedIndex = -1
                UiTextBox音频基础比特率.Text = ""
                UiTextBox音频比特率质量值.Text = ""
                UiComboBox声道布局.Text = ""
                UiComboBox采样率.Text = ""
                UiTextBox响度标准化目标响度.Text = ""
                UiTextBox响度标准化动态范围.Text = ""
                UiTextBox响度标准化峰值电平.Text = ""
        End Select
    End Sub

    Sub 音频比特率控制方式参数变动事件()
        If UiComboBox音频比特率控制方式.Text = "" Then
            UiTextBox音频基础比特率.Text = ""
            UiTextBox音频比特率质量值.Text = ""
        End If
    End Sub
    Sub 图片编码器参数变动事件()
        If UiComboBox图片编码器.Text = "" Then
            UiTextBox图片编码器质量.Text = ""
        End If
    End Sub

    Private Sub UiTabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles UiTabControl1.SelectedIndexChanged
        界面校准()
    End Sub
    Sub 界面校准()
        Dim 选项卡 As TabPage = UiTabControl1.SelectedTab
        Select Case True
            Case 选项卡.IsEqual(TabPage解码参数)
                UiComboBox解码器.ItemHeight = 30 * Form1.DPI
                UiComboBox解码数据格式.ItemHeight = 30 * Form1.DPI
                UiComboBox自动命名选项.ItemHeight = 30 * Form1.DPI
                UiCheckBox不使用输出文件参数.CheckBoxSize = 20 * Form1.DPI
            Case 选项卡.IsEqual(TabPage视频参数)
                UiComboBox编码类别.ItemHeight = 30 * Form1.DPI
                UiComboBox具体编码.ItemHeight = 30 * Form1.DPI
                UiComboBox编码预设.ItemHeight = 30 * Form1.DPI
                UiComboBox分辨率.ItemHeight = 30 * Form1.DPI
                UiComboBox帧速率.ItemHeight = 30 * Form1.DPI
                UiComboBox比特率控制方式.ItemHeight = 30 * Form1.DPI
                UiComboBox全局质量控制参数.ItemHeight = 30 * Form1.DPI
                UiComboBox配置文件.ItemHeight = 30 * Form1.DPI
                UiComboBox场景优化.ItemHeight = 30 * Form1.DPI
                UiComboBox像素格式.ItemHeight = 30 * Form1.DPI
            Case 选项卡.IsEqual(TabPage高级视频参数)
                UiComboBox矩阵系数.ItemHeight = 30 * Form1.DPI
                UiComboBox色域.ItemHeight = 30 * Form1.DPI
                UiComboBox传输特性.ItemHeight = 30 * Form1.DPI
                UiComboBox色彩范围.ItemHeight = 30 * Form1.DPI
                UiComboBox色彩管理处理方式.ItemHeight = 30 * Form1.DPI
                UiComboBox降噪方式.ItemHeight = 30 * Form1.DPI
                UiComboBox逐行与隔行处理方式.ItemHeight = 30 * Form1.DPI
            Case 选项卡.IsEqual(TabPage音频参数)
                UiComboBox音频编码器.ItemHeight = 30 * Form1.DPI
                UiComboBox音频比特率控制方式.ItemHeight = 30 * Form1.DPI
                UiComboBox声道布局.ItemHeight = 30 * Form1.DPI
                UiComboBox采样率.ItemHeight = 30 * Form1.DPI
            Case 选项卡.IsEqual(TabPage图片参数)
                UiComboBox图片编码器.ItemHeight = 30 * Form1.DPI
            Case 选项卡.IsEqual(TabPage自定义参数)
            Case 选项卡.IsEqual(TabPage流控制)
                UiCheckBox保留其他视频流.CheckBoxSize = 20 * Form1.DPI
                UiCheckBox保留其他音频流.CheckBoxSize = 20 * Form1.DPI
                UiCheckBox保留内嵌字幕流.CheckBoxSize = 20 * Form1.DPI
                UiCheckBox自动混流同名字幕文件.CheckBoxSize = 20 * Form1.DPI
                UiComboBox元数据选项.ItemHeight = 30 * Form1.DPI
                UiComboBox章节选项.ItemHeight = 30 * Form1.DPI
                UiComboBox附件选项.ItemHeight = 30 * Form1.DPI
                UiComboBox剪辑方法.ItemHeight = 30 * Form1.DPI
            Case 选项卡.IsEqual(TabPage预设管理)
                UiCheckBox额外保存信息.CheckBoxSize = 20 * Form1.DPI
                UiComboBox自动加载预设选项.ItemHeight = 30 * Form1.DPI
                Dim a As New 预设数据类型
                预设管理.储存预设(a)
                RichTextBox1.Size = New Size(RichTextBox1.Parent.Width, RichTextBox1.Parent.Height - RichTextBox1.Parent.Padding.Top * 2)
                RichTextBox1.Text = "ffmpeg " & 预设管理.将预设数据转换为命令行(a, "输入目录\输入文件.后缀", "输出目录\输出文件.后缀")
        End Select

    End Sub

End Class
