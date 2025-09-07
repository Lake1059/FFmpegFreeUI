Imports System.Text.Json
Imports Sunny.UI
Imports Windows.Devices.Radios

Public Class 界面_常规流程参数_V2

    Private Sub 界面_常规流程参数_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Form1.DPI <> 1 Then UiTabControlMenu1.ItemSize = New Size(200 * Form1.DPI, 40 * Form1.DPI)
        初始化进阶质量控制预制菜单项()
        设置富文本框行高(RichTextBox1, 350)
        设置富文本框行高(RichTextBox2, 350)

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
        AddHandler UiComboBox全局质量控制方式.SelectedIndexChanged, AddressOf 视频比特率控制方式改动事件
        AddHandler UiButton添加进阶质量控制预制项.Click, Sub()
                                                  进阶质量控制预制菜单项.Font = New Font(用户设置.实例对象.字体, 10)
                                                  进阶质量控制预制菜单项.Show(TabPage视频参数质量, New Point(20 * Form1.DPI, 20 * Form1.DPI))
                                              End Sub
        AddHandler UiButton添加进阶质量控制空项.Click, Sub() 创建进阶质量控制项("")
        AddHandler UiButton清除全部进阶质量控制.Click, AddressOf 清除全部进阶质量控制
        '==============================================
        AddHandler UiComboBox色彩管理处理方式.TextChanged, AddressOf 色彩管理处理方式变动事件
        '==============================================
        AddHandler UiComboBox降噪方式.TextChanged, AddressOf 视频降噪方式变动事件
        '==========================================
        AddHandler UiComboBox音频编码器.TextChanged, AddressOf 音频编码参数变动事件
        AddHandler UiComboBox音频质量参数.TextChanged, AddressOf 音频质量参数变动事件
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
                                                 If Not FileIO.FileSystem.FileExists(d.FileName) Then Exit Sub
                                                 UiTextBox自动加载的预设文件路径.Text = d.FileName
                                                 UiComboBox自动加载预设选项.SelectedIndex = 用户设置.自动加载预设选项枚举.自动加载指定的预设文件
                                             End Sub
        AddHandler UiTextBox自动加载的预设文件路径.TextChanged, Sub()
                                                         If UiTextBox自动加载的预设文件路径.Text <> "" Then
                                                             Select Case UiComboBox自动加载预设选项.SelectedIndex
                                                                 Case 用户设置.自动加载预设选项枚举.自动加载最后的预设文件, 用户设置.自动加载预设选项枚举.自动加载指定的预设文件
                                                                     用户设置.实例对象.自动加载预设文件路径 = UiTextBox自动加载的预设文件路径.Text
                                                             End Select
                                                         Else
                                                             用户设置.实例对象.自动加载预设文件路径 = ""
                                                         End If
                                                     End Sub
        '==================================================
        AddHandler UiComboBox自动命名选项.MouseWheel, AddressOf 下拉框鼠标滚轮事件
        '==================================================
        AddHandler UiComboBox解码器.MouseWheel, AddressOf 下拉框鼠标滚轮事件
        AddHandler UiComboBox解码数据格式.MouseWheel, AddressOf 下拉框鼠标滚轮事件
        AddHandler UiComboBox硬件加速解码参数名.MouseWheel, AddressOf 下拉框鼠标滚轮事件
        '==================================================
        AddHandler UiComboBox编码类别.MouseWheel, AddressOf 下拉框鼠标滚轮事件
        AddHandler UiComboBox具体编码.MouseWheel, AddressOf 下拉框鼠标滚轮事件
        AddHandler UiComboBox编码预设.MouseWheel, AddressOf 下拉框鼠标滚轮事件
        AddHandler UiComboBox配置文件.MouseWheel, AddressOf 下拉框鼠标滚轮事件
        AddHandler UiComboBox场景优化.MouseWheel, AddressOf 下拉框鼠标滚轮事件
        '==================================================
        AddHandler UiComboBox分辨率.MouseWheel, AddressOf 下拉框鼠标滚轮事件
        AddHandler UiComboBox帧速率.MouseWheel, AddressOf 下拉框鼠标滚轮事件
        '==================================================
        AddHandler UiComboBox全局质量控制方式.MouseWheel, AddressOf 下拉框鼠标滚轮事件
        AddHandler UiComboBox全局质量控制参数.MouseWheel, AddressOf 下拉框鼠标滚轮事件
        '==================================================
        AddHandler UiComboBox像素格式.MouseWheel, AddressOf 下拉框鼠标滚轮事件
        AddHandler UiComboBox矩阵系数.MouseWheel, AddressOf 下拉框鼠标滚轮事件
        AddHandler UiComboBox色域.MouseWheel, AddressOf 下拉框鼠标滚轮事件
        AddHandler UiComboBox传输特性.MouseWheel, AddressOf 下拉框鼠标滚轮事件
        AddHandler UiComboBox色彩范围.MouseWheel, AddressOf 下拉框鼠标滚轮事件
        '==================================================
        AddHandler UiComboBox降噪方式.MouseWheel, AddressOf 下拉框鼠标滚轮事件
        AddHandler UiComboBox逐行与隔行处理方式.MouseWheel, AddressOf 下拉框鼠标滚轮事件
        AddHandler UiComboBox角度翻转.MouseWheel, AddressOf 下拉框鼠标滚轮事件
        AddHandler UiComboBox镜像翻转.MouseWheel, AddressOf 下拉框鼠标滚轮事件
        '==================================================
        AddHandler UiComboBox音频编码器.MouseWheel, AddressOf 下拉框鼠标滚轮事件
        AddHandler UiComboBox音频质量参数.MouseWheel, AddressOf 下拉框鼠标滚轮事件
        AddHandler UiComboBox声道布局.MouseWheel, AddressOf 下拉框鼠标滚轮事件
        AddHandler UiComboBox采样率.MouseWheel, AddressOf 下拉框鼠标滚轮事件
        '==================================================
        AddHandler UiComboBox图片编码器.MouseWheel, AddressOf 下拉框鼠标滚轮事件
        '==================================================
        AddHandler UiComboBox元数据选项.MouseWheel, AddressOf 下拉框鼠标滚轮事件
        AddHandler UiComboBox章节选项.MouseWheel, AddressOf 下拉框鼠标滚轮事件
        AddHandler UiComboBox附件选项.MouseWheel, AddressOf 下拉框鼠标滚轮事件
        AddHandler UiComboBox剪辑方法.MouseWheel, AddressOf 下拉框鼠标滚轮事件
        '==================================================
        AddHandler UiComboBox自动加载预设选项.MouseWheel, AddressOf 下拉框鼠标滚轮事件
        '==================================================
        Select Case 用户设置.实例对象.自动加载预设选项
            Case 用户设置.自动加载预设选项枚举.自动加载最后的预设文件, 用户设置.自动加载预设选项枚举.自动加载指定的预设文件
                If FileIO.FileSystem.FileExists(用户设置.实例对象.自动加载预设文件路径) Then
                    预设管理.显示预设(JsonSerializer.Deserialize(Of 预设数据类型)(FileIO.FileSystem.ReadAllText(用户设置.实例对象.自动加载预设文件路径)))
                End If
            Case 用户设置.自动加载预设选项枚举.自动加载上次的全部改动
                If 用户设置.实例对象.最后的预设数据 IsNot Nothing Then 预设管理.显示预设(用户设置.实例对象.最后的预设数据)
        End Select
        AddHandler UiButton复制即时命令行显示.Click, Sub() Clipboard.SetText(RichTextBox1.Text)
        界面校准()
    End Sub

    Private Sub 界面_常规流程参数_V2_DpiChangedAfterParent(sender As Object, e As EventArgs) Handles Me.DpiChangedAfterParent
        UiTabControlMenu1.ItemSize = New Size(200 * Form1.DPI, 40 * Form1.DPI)
    End Sub

    Sub 下拉框鼠标滚轮事件(sender As Object, e As MouseEventArgs)
        Select Case e.Delta
            Case > 0 : If sender.SelectedIndex > 0 Then sender.SelectedIndex -= 1
            Case < 0 : If sender.SelectedIndex < sender.Items.Count - 1 Then sender.SelectedIndex += 1
        End Select
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
                UiComboBox具体编码.Items.Add("libvvenc")
                UiComboBox具体编码.Items.Add("libx266")
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
            Case 6    '来自 Apple
                UiComboBox具体编码.Items.Add("prores_ks")
                UiComboBox具体编码.Items.Add("prores_aw")
            Case 7    '来自 Google
                UiComboBox具体编码.Items.Add("libvpx-vp9")
                UiComboBox具体编码.Items.Add("vp9_vaapi")
                UiComboBox具体编码.Items.Add("libvpx")
                UiComboBox具体编码.Items.Add("vp8_vaapi")
            Case 8    'FFv1
                UiComboBox具体编码.Items.Add("ffv1 -level 3")
                UiComboBox具体编码.Items.Add("ffv1 -level 1")
                UiComboBox具体编码.Items.Add("ffv1_vulkan")
            Case 9    '其他现代编码
                UiComboBox具体编码.Items.Add("libxeve")
                UiComboBox具体编码.Items.Add("libxavs2")
            Case 10    '老旧格式
                UiComboBox具体编码.Items.Add("mpeg4")
                UiComboBox具体编码.Items.Add("libxvid")
                UiComboBox具体编码.Items.Add("rv20")
                UiComboBox具体编码.Items.Add("rv10")
                UiComboBox具体编码.Items.Add("wmv2")
                UiComboBox具体编码.Items.Add("wmv1")
            Case 11    '禁用
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

        Dim value As 视频编码器数据库.视频编码器数据单片结构 = Nothing
        If UiComboBox具体编码.SelectedIndex > -1 AndAlso 视频编码器数据库.字典.TryGetValue(UiComboBox具体编码.Text, value) Then
            UiComboBox编码预设.Items.AddRange(value.Preset.ToArray)
            Select Case UiComboBox具体编码.Text
                Case "libvvenc" : UiComboBox编码预设.Text = "faster"
                Case "libaom-av1" : UiComboBox编码预设.Text = "4"
                Case "libsvtav1" : UiComboBox编码预设.Text = "6"
                Case "libx265" : UiComboBox编码预设.Text = "medium"
                Case "libx264" : UiComboBox编码预设.Text = "slow"
                Case Else : UiComboBox编码预设.SelectedIndex = 0
            End Select
            UiComboBox配置文件.Items.AddRange(value.Profile.ToArray)
            UiComboBox场景优化.Items.AddRange(value.Tune.ToArray)
            UiComboBox像素格式.Items.AddRange(value.Pix_fmt.ToArray)
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
        Select Case UiComboBox全局质量控制方式.SelectedIndex
            Case 0
                UiComboBox全局质量控制参数.SelectedIndex = 0
                UiTextBox全局质量控制值.Text = ""
            Case 1    '恒定质量 CRF - 存储首选，软件编码首选
                UiComboBox全局质量控制参数.SelectedIndex = 1
            Case 2    '动态码率 VBR - 存储首选，硬件加速首选
                UiComboBox全局质量控制参数.SelectedIndex = 2
            Case 3    '动态码率 VBR HQ - 硬件加速专用，极致质量
                UiComboBox全局质量控制参数.SelectedIndex = 2
            Case 4    '恒定量化 CQP - 不推荐，主用于研究和特定场景
                UiComboBox全局质量控制参数.SelectedIndex = 3
            Case 5    '恒定速率 CBR - 流媒体常用，不适合存储
                UiComboBox全局质量控制参数.SelectedIndex = 0
                UiTextBox全局质量控制值.Text = ""
        End Select
    End Sub
    Sub 视频降噪方式变动事件()
        Label68.Visible = False
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
                Panel36.Visible = True
                Panel35.Visible = True
            Case 2    'nlmeans - 高级降噪，效果更好且计算量更大
                Label降噪参数1.Text = "降噪强度" & vbCrLf & "范围 1~10"
                UiTextBox降噪参数1.Watermark = "默认 1"
                Label降噪参数2.Text = "参考像素块大小" & vbCrLf & "必须是奇数"
                UiTextBox降噪参数2.Watermark = "默认 7"
                Label降噪参数3.Text = "色度降噪" & vbCrLf & "启用 = 1 禁用 = 0"
                UiTextBox降噪参数3.Watermark = ""
                Label降噪参数4.Text = "搜索半径" & vbCrLf & "越大越慢"
                UiTextBox降噪参数4.Watermark = "默认 7"
                Panel36.Visible = True
                Panel35.Visible = True
            Case 3  'atadenoise - 轻量级时间域降噪
                Label降噪参数1.Text = "亮度静态帧加权" & vbCrLf & "0a"
                UiTextBox降噪参数1.Watermark = "默认 0.02"
                Label降噪参数2.Text = "亮度动态帧加权" & vbCrLf & "0b"
                UiTextBox降噪参数2.Watermark = "默认 0.04"
                Label降噪参数3.Text = "色度静态加权" & vbCrLf & "1a"
                UiTextBox降噪参数3.Watermark = "默认 0.02"
                Label降噪参数4.Text = "色度动态加权" & vbCrLf & "1b"
                UiTextBox降噪参数4.Watermark = "默认 0.04"
                Panel36.Visible = True
                Panel35.Visible = True
            Case 4    'bm3d - 高质量降噪，适合严重噪声且消耗大量性能
                Label降噪参数1.Text = "噪声强度" & vbCrLf & "sigma"
                UiTextBox降噪参数1.Watermark = "默认 1"
                Label降噪参数2.Text = "块大小 建议 4~8" & vbCrLf & "block"
                UiTextBox降噪参数2.Watermark = "默认 4"
                Label降噪参数3.Text = "块步长" & vbCrLf & "bstep"
                UiTextBox降噪参数3.Watermark = "block/2"
                Label降噪参数4.Text = "相似块数量" & vbCrLf & "group"
                UiTextBox降噪参数4.Watermark = "默认 1"
                Panel36.Visible = True
                Panel35.Visible = True
            Case 5   '外部 AviSynth - 加载 avs 文件
                UiTextBox降噪参数1.Text = ""
                UiTextBox降噪参数2.Text = ""
                UiTextBox降噪参数3.Text = ""
                UiTextBox降噪参数4.Text = ""
                Panel35.Visible = False
                Panel36.Visible = False
                Label68.Visible = True
            Case Else
                UiTextBox降噪参数1.Text = ""
                UiTextBox降噪参数2.Text = ""
                UiTextBox降噪参数3.Text = ""
                UiTextBox降噪参数4.Text = ""
                Panel35.Visible = False
                Panel36.Visible = False
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
                UiComboBox音频比特率.Text = ""
                UiComboBox音频质量参数.Text = ""
                UiTextBox音频质量值.Text = ""
                UiComboBox声道布局.Text = ""
                UiComboBox采样率.Text = ""
                UiTextBox响度标准化目标响度.Text = ""
                UiTextBox响度标准化动态范围.Text = ""
                UiTextBox响度标准化峰值电平.Text = ""
        End Select
    End Sub

    Sub 音频质量参数变动事件()
        If UiComboBox音频质量参数.Text = "" Then
            UiComboBox音频比特率.Text = ""
            UiTextBox音频质量值.Text = ""
        End If
    End Sub
    Sub 图片编码器参数变动事件()
        If UiComboBox图片编码器.Text = "" Then
            UiTextBox图片编码器质量.Text = ""
        End If
    End Sub

    Private Sub UiTabControlMenu1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles UiTabControlMenu1.SelectedIndexChanged
        界面校准()
    End Sub
    Sub 界面校准()
        Dim 选项卡 As TabPage = UiTabControlMenu1.SelectedTab
        Select Case True
            Case 选项卡.IsEqual(TabPage参数总览) : 显示参数总览()

            Case 选项卡.IsEqual(TabPage解码设置)
                校准UiComboBox高DPI(UiComboBox解码器)
                校准UiComboBox高DPI(UiComboBox解码数据格式)
                校准UiComboBox高DPI(UiComboBox自动命名选项)

            Case 选项卡.IsEqual(TabPage视频参数编码器)
                校准UiComboBox高DPI(UiComboBox编码类别)
                校准UiComboBox高DPI(UiComboBox具体编码)
                校准UiComboBox高DPI(UiComboBox编码预设)
                校准UiComboBox高DPI(UiComboBox配置文件)
                校准UiComboBox高DPI(UiComboBox场景优化)

            Case 选项卡.IsEqual(TabPage视频参数画面帧)
                校准UiComboBox高DPI(UiComboBox分辨率)
                校准UiComboBox高DPI(UiComboBox帧速率)

            Case 选项卡.IsEqual(TabPage视频参数质量)
                校准UiComboBox高DPI(UiComboBox全局质量控制方式)
                校准UiComboBox高DPI(UiComboBox全局质量控制参数)

            Case 选项卡.IsEqual(TabPage视频参数色彩管理)
                校准UiComboBox高DPI(UiComboBox像素格式)
                校准UiComboBox高DPI(UiComboBox矩阵系数)
                校准UiComboBox高DPI(UiComboBox色域)
                校准UiComboBox高DPI(UiComboBox传输特性)
                校准UiComboBox高DPI(UiComboBox色彩范围)
                校准UiComboBox高DPI(UiComboBox色彩管理处理方式)

            Case 选项卡.IsEqual(TabPage视频参数常见滤镜)
                校准UiComboBox高DPI(UiComboBox降噪方式)
                校准UiComboBox高DPI(UiComboBox逐行与隔行处理方式)
                校准UiComboBox高DPI(UiComboBox角度翻转)
                校准UiComboBox高DPI(UiComboBox镜像翻转)

            Case 选项卡.IsEqual(TabPage音频参数)
                校准UiComboBox高DPI(UiComboBox音频编码器)
                校准UiComboBox高DPI(UiComboBox音频质量参数)
                校准UiComboBox高DPI(UiComboBox声道布局)
                校准UiComboBox高DPI(UiComboBox采样率)

            Case 选项卡.IsEqual(TabPage图片参数)
                校准UiComboBox高DPI(UiComboBox图片编码器)

            Case 选项卡.IsEqual(TabPage自定义参数)
            Case 选项卡.IsEqual(TabPage流控制)
                UiCheckBox保留其他视频流.CheckBoxSize = 20 * Form1.DPI
                UiCheckBox保留其他音频流.CheckBoxSize = 20 * Form1.DPI
                UiCheckBox保留内嵌字幕流.CheckBoxSize = 20 * Form1.DPI
                UiCheckBox自动混流同名字幕文件.CheckBoxSize = 20 * Form1.DPI
                校准UiComboBox高DPI(UiComboBox元数据选项)
                校准UiComboBox高DPI(UiComboBox章节选项)
                校准UiComboBox高DPI(UiComboBox附件选项)
                校准UiComboBox高DPI(UiComboBox剪辑方法)

            Case 选项卡.IsEqual(TabPage方案管理)
                UiCheckBox额外保存信息.CheckBoxSize = 20 * Form1.DPI
                校准UiComboBox高DPI(UiComboBox自动加载预设选项)
                Dim a As New 预设数据类型
                预设管理.储存预设(a)
                RichTextBox1.Text = "ffmpeg " & 预设管理.将预设数据转换为命令行(a, "<输入文件>", "<输出文件>")
        End Select

    End Sub

    Sub 清除全部进阶质量控制()
        For Each c In Me.FlowLayoutPanel1.Controls
            c.Dispose()
        Next
        Me.FlowLayoutPanel1.Controls.Clear()
        GC.Collect()
    End Sub


    Sub 创建进阶质量控制项(text As String)
        Dim a As New UITextBox With {
            .FillColor = Color.FromArgb(48, 48, 48),
            .ForeColor = Color.Silver,
            .RectColor = Color.FromArgb(48, 48, 48),
            .Font = New Font(用户设置.实例对象.字体, 10),
            .Margin = New Padding(0, 0, 10, 10),
            .ButtonFillColor = Color.FromArgb(80, 80, 80),
            .ButtonFillHoverColor = Color.FromArgb(100, 100, 100),
            .ButtonFillPressColor = Color.FromArgb(120, 120, 120),
            .ButtonForeColor = Color.Silver,
            .ButtonForeHoverColor = Color.Silver,
            .ButtonForePressColor = Color.White,
            .ButtonRectColor = Color.FromArgb(80, 80, 80),
            .ButtonRectHoverColor = Color.FromArgb(100, 100, 100),
            .ButtonRectPressColor = Color.FromArgb(120, 120, 120),
            .ButtonSymbol = 10005,
            .ButtonSymbolOffset = New Point(0, 1),
            .ShowButton = True,
            .Height = 30 * Form1.DPI,
            .ButtonWidth = 29 * Form1.DPI,
            .ButtonSymbolSize = 24 * Form1.DPI,
            .Text = text
        }
        a.Width = TextRenderer.MeasureText(text, New Font(用户设置.实例对象.字体, 10)).Width + a.ButtonWidth + 30 * Form1.DPI
        AddHandler a.TextChanged, Sub(sender, e)
                                      Dim s = TextRenderer.MeasureText(sender.Text, sender.Font).Width + sender.ButtonWidth + 30 * Form1.DPI
                                      If s < 50 * Form1.DPI Then
                                          sender.Width = 50 * Form1.DPI
                                      Else
                                          sender.Width = s
                                      End If
                                  End Sub
        AddHandler a.DpiChangedAfterParent, Sub(sender, e)
                                                sender.Height = sender.Height * Form1.DPI
                                                sender.ButtonWidth = 29 * Form1.DPI
                                                sender.ButtonSymbolSize = 24 * Form1.DPI
                                            End Sub
        AddHandler a.ButtonClick, Sub() a.Dispose()
        AddHandler a.KeyDown, Sub(sender, e)
                                  Select Case e.KeyCode
                                      Case Keys.F3
                                          Dim idx As Integer = sender.Parent.Controls.GetChildIndex(sender)
                                          If idx > 0 Then
                                              sender.Parent.Controls.SetChildIndex(sender, idx - 1)
                                          End If
                                      Case Keys.F4
                                          Dim idx As Integer = sender.Parent.Controls.GetChildIndex(sender)
                                          If idx <> sender.Parent.Controls.Count - 1 Then
                                              sender.Parent.Controls.SetChildIndex(sender, idx + 1)
                                          End If
                                  End Select
                              End Sub
        Me.FlowLayoutPanel1.Controls.Add(a)
    End Sub

    Public 进阶质量控制预制菜单项 As New 暗黑上下文菜单 With {.ShowImageMargin = False, .Font = New Font(用户设置.实例对象.字体, 10)}

    Sub 初始化进阶质量控制预制菜单项()
        进阶质量控制预制菜单项.Items.AddRange(New ToolStripItem() {
        New ToolStripSeparator() With {.Tag = "null"},
        New ToolStripMenuItem("-rc-lookahead 前向参考帧数 适用于 Nvidia\libx264", Nothing, Sub(s1, e1) 创建进阶质量控制项("-rc-lookahead ")) With {.ForeColor = Color.YellowGreen},
        New ToolStripMenuItem("-look_ahead_depth 前向参考帧数 适用于 Intel", Nothing, Sub(s1, e1) 创建进阶质量控制项("-look_ahead_depth ")) With {.ForeColor = Color.CornflowerBlue},
        New ToolStripMenuItem("-extbrc=1 启用激进比特率分配 适用于 Intel", Nothing, Sub(s1, e1) 创建进阶质量控制项("-extbrc 1")) With {.ForeColor = Color.CornflowerBlue},
        New ToolStripMenuItem("-g 关键帧 (i) 间隔", Nothing, Sub(s1, e1) 创建进阶质量控制项("-g ")) With {.ForeColor = Color.Silver},
        New ToolStripMenuItem("-bf 双向预测帧 (b) 数量", Nothing, Sub(s1, e1) 创建进阶质量控制项("-bf ")) With {.ForeColor = Color.Silver},
        New ToolStripMenuItem("-level 编码级别 较少使用", Nothing, Sub(s1, e1) 创建进阶质量控制项("-level ")) With {.ForeColor = Color.Gray},
        New ToolStripMenuItem("-qmin 量化最小值（最高画质）", Nothing, Sub(s1, e1) 创建进阶质量控制项("-qmin ")) With {.ForeColor = Color.Silver},
        New ToolStripMenuItem("-qpmin 量化最小值（最高画质）", Nothing, Sub(s1, e1) 创建进阶质量控制项("-qpmin ")) With {.ForeColor = Color.Silver},
        New ToolStripMenuItem("-qmax 量化最大值（最低画质）", Nothing, Sub(s1, e1) 创建进阶质量控制项("-qmax ")) With {.ForeColor = Color.Silver},
        New ToolStripMenuItem("-qpmax 量化最小值（最高画质）", Nothing, Sub(s1, e1) 创建进阶质量控制项("-qpmax ")) With {.ForeColor = Color.Silver},
        New ToolStripMenuItem("-qcomp 量化系数非线性压缩因子 0.0~1.0", Nothing, Sub(s1, e1) 创建进阶质量控制项("-qcomp ")) With {.ForeColor = Color.Silver},
        New ToolStripMenuItem("-aq-mode 自适应量化模式 适用于 libaom-av1", Nothing, Sub(s1, e1) 创建进阶质量控制项("-aq-mode ")) With {.ForeColor = Color.Silver},
        New ToolStripMenuItem("-arnr-strength 自适应降噪强度 0~6 适用于 libaom-av1", Nothing, Sub(s1, e1) 创建进阶质量控制项("-arnr-strength ")) With {.ForeColor = Color.Silver},
        New ToolStripMenuItem("-la_depth 前向参考帧数 适用于 libsvtav1", Nothing, Sub(s1, e1) 创建进阶质量控制项("-la_depth ")) With {.ForeColor = Color.Silver},
        New ToolStripMenuItem("-qp_i 关键帧质量", Nothing, Sub(s1, e1) 创建进阶质量控制项("-qp_i ")) With {.ForeColor = Color.Silver},
        New ToolStripMenuItem("-qp_p 前向参考帧质量", Nothing, Sub(s1, e1) 创建进阶质量控制项("-qp_p ")) With {.ForeColor = Color.Silver},
        New ToolStripMenuItem("-qp_b 双向参考帧质量", Nothing, Sub(s1, e1) 创建进阶质量控制项("-qp_b ")) With {.ForeColor = Color.Silver},
        New ToolStripSeparator() With {.Tag = "null"}})
    End Sub

    Sub 在参数总览输出文本(文本 As String, 颜色 As Color)
        If String.IsNullOrEmpty(文本) Then Exit Sub
        Dim 文本长度 = Len(文本)
        If RichTextBox2.TextLength > 0 Then
            RichTextBox2.AppendText(vbCrLf & 文本)
        Else
            RichTextBox2.AppendText(文本)
        End If
        Dim 添加起始位 As Integer = RichTextBox2.TextLength - 文本长度
        RichTextBox2.Select(添加起始位, 文本长度)
        RichTextBox2.SelectionColor = 颜色
        RichTextBox2.Select(RichTextBox2.TextLength, 0)
    End Sub
    Sub 显示参数总览()
        Dim a As New 预设数据类型
        预设管理.储存预设(a)
        RichTextBox2.Clear()
        If a.自定义参数_完全自己写 <> "" Then
            在参数总览输出文本("正在使用完全自己写参数模式，绝大多数参数均不会生效", Color.IndianRed)
            Exit Sub
        End If

        If a.输出容器 <> "" Then 在参数总览输出文本("输出容器：" & a.输出容器, Color.Gray)
        If a.输出命名_使用自动命名 And a.输出命名_自动命名选项 = 0 Then
            在参数总览输出文本("正在使用自动命名：附加时间戳", Color.Gray)
        ElseIf a.输出命名_使用自动命名 And a.输出命名_自动命名选项 <> 0 Then
            在参数总览输出文本("正在使用其他自动命名", Color.Gray)
        End If
        If a.输出命名_不使用输出文件参数 Then 在参数总览输出文本("警告：已设置不使用输出文件参数，仅用于特殊需求，未指定自定义参数必报错", Color.IndianRed)
        If a.输出命名_开头文本 <> "" Then 在参数总览输出文本("输出文件开头文本：" & a.输出命名_开头文本, Color.Gray)
        If a.输出命名_替代文本 <> "" Then 在参数总览输出文本("输出文件替代文本：" & a.输出命名_替代文本, Color.Gray)
        If a.输出命名_结尾文本 <> "" Then 在参数总览输出文本("输出文件结尾文本：" & a.输出命名_结尾文本, Color.Gray)

        If a.解码参数_解码器 <> "" Then 在参数总览输出文本("解码器：" & a.解码参数_解码器, Color.Silver)
        If a.解码参数_CPU解码线程数 <> "" Then 在参数总览输出文本("CPU 解码线程数：" & a.解码参数_CPU解码线程数, Color.Silver)
        If a.解码参数_解码数据格式 <> "" Then 在参数总览输出文本("解码数据格式：" & a.解码参数_解码数据格式, Color.Silver)
        If a.解码参数_指定硬件的参数名 <> "" Then
            If a.解码参数_指定硬件的参数 <> "" Then
                在参数总览输出文本("指定加速解码硬件：-" & a.解码参数_指定硬件的参数名 & " " & a.解码参数_指定硬件的参数, Color.Silver)
            Else
                在参数总览输出文本("未指定硬件加速解码的参数，将被忽略", Color.IndianRed)
            End If
        End If

        If a.视频参数_编码器_类别 <> "" Then 在参数总览输出文本("视频编码类别：" & a.视频参数_编码器_类别, Color.Silver)
        If a.视频参数_编码器_具体编码 <> "" Then 在参数总览输出文本("视频具体编码器：" & a.视频参数_编码器_具体编码, Color.Silver)
        If a.视频参数_编码器_编码预设 <> "" Then 在参数总览输出文本("视频编码预设：" & a.视频参数_编码器_编码预设, Color.Silver)
        If a.视频参数_编码器_配置文件 <> "" Then 在参数总览输出文本("视频编码配置文件：" & a.视频参数_编码器_配置文件, Color.Silver)
        If a.视频参数_编码器_场景优化 <> "" Then 在参数总览输出文本("视频编码场景优化：" & a.视频参数_编码器_场景优化, Color.Silver)

        '---------------- 视频尺寸 / 帧率 ----------------
        If a.视频参数_分辨率 <> "" Then 在参数总览输出文本("视频分辨率：" & a.视频参数_分辨率, Color.Silver)
        If a.视频参数_分辨率自动计算_宽度 <> "" Then
            在参数总览输出文本("视频分辨率自动计算：宽 = " & a.视频参数_分辨率自动计算_宽度, Color.Silver)
        End If
        If a.视频参数_分辨率自动计算_高度 <> "" Then
            在参数总览输出文本("视频分辨率自动计算：高 = " & a.视频参数_分辨率自动计算_高度, Color.Silver)
        End If

        If a.视频参数_分辨率_裁剪滤镜参数 <> "" Then 在参数总览输出文本("画面裁剪：" & a.视频参数_分辨率_裁剪滤镜参数, Color.Silver)
        If a.视频参数_帧速率 <> "" Then 在参数总览输出文本("输出帧率：" & a.视频参数_帧速率, Color.Silver)
        If a.视频参数_帧速率_抽帧最大变化比例 <> "" Then 在参数总览输出文本("抽帧最大变化比例：" & a.视频参数_帧速率_抽帧最大变化比例, Color.Silver)

        '---------------- 插帧 ----------------
        If a.视频参数_插帧_目标帧率 <> "" Then 在参数总览输出文本("插帧目标帧率：" & a.视频参数_插帧_目标帧率, Color.Silver)
        If a.视频参数_插帧_插帧模式 <> "" Then 在参数总览输出文本("插帧模式：" & a.视频参数_插帧_插帧模式, Color.Silver)
        If a.视频参数_插帧_运动估计模式 <> "" Then 在参数总览输出文本("运动估计模式：" & a.视频参数_插帧_运动估计模式, Color.Silver)
        If a.视频参数_插帧_运动估计算法 <> "" Then 在参数总览输出文本("运动估计算法：" & a.视频参数_插帧_运动估计算法, Color.Silver)
        If a.视频参数_插帧_运动补偿模式 <> "" Then 在参数总览输出文本("运动补偿模式：" & a.视频参数_插帧_运动补偿模式, Color.Silver)
        If a.视频参数_插帧_可变块大小的运动补偿 Then 在参数总览输出文本("插帧：可变块大小运动补偿 已启用", Color.Silver)
        If a.视频参数_插帧_块大小 <> "" Then 在参数总览输出文本("插帧块大小：" & a.视频参数_插帧_块大小, Color.Silver)
        If a.视频参数_插帧_搜索范围 <> "" Then 在参数总览输出文本("插帧搜索范围：" & a.视频参数_插帧_搜索范围, Color.Silver)
        If a.视频参数_插帧_场景变化检测强度 <> "" Then 在参数总览输出文本("场景变化检测强度：" & a.视频参数_插帧_场景变化检测强度, Color.Silver)

        '---------------- 帧混合 ----------------
        If a.视频参数_帧混合_指定帧率 <> "" Then 在参数总览输出文本("帧混合指定帧率：" & a.视频参数_帧混合_指定帧率, Color.Silver)
        If a.视频参数_帧混合_混合模式 <> "" Then 在参数总览输出文本("帧混合模式：" & a.视频参数_帧混合_混合模式, Color.Silver)
        If a.视频参数_帧混合_混合比例 <> "" Then 在参数总览输出文本("帧混合比例：" & a.视频参数_帧混合_混合比例, Color.Silver)

        '---------------- 码率 / 质量控制 ----------------
        If a.视频参数_质量控制_参数名 <> "" Then 在参数总览输出文本("质量控制参数：" & a.视频参数_质量控制_参数名 & " = " & a.视频参数_质量控制_值, Color.Silver)
        If a.视频参数_比特率_基础 <> "" Then 在参数总览输出文本("基础码率：" & a.视频参数_比特率_基础, Color.Silver)
        If a.视频参数_比特率_最低值 <> "" Then 在参数总览输出文本("最低码率：" & a.视频参数_比特率_最低值, Color.Silver)
        If a.视频参数_比特率_最高值 <> "" Then 在参数总览输出文本("最高码率：" & a.视频参数_比特率_最高值, Color.Silver)
        If a.视频参数_比特率_缓冲区 <> "" Then 在参数总览输出文本("缓冲区大小：" & a.视频参数_比特率_缓冲区, Color.Silver)
        If a.视频参数_质量控制_进阶参数集 IsNot Nothing AndAlso a.视频参数_质量控制_进阶参数集.Count > 0 Then
            在参数总览输出文本("进阶质量控制：" & String.Join(" ", a.视频参数_质量控制_进阶参数集), Color.Silver)
        End If

        '---------------- 色彩管理 ----------------

        If a.视频参数_色彩管理_矩阵系数 <> "" Then 在参数总览输出文本("矩阵系数：" & a.视频参数_色彩管理_矩阵系数, Color.Silver)
        If a.视频参数_色彩管理_色域 <> "" Then 在参数总览输出文本("色域：" & a.视频参数_色彩管理_色域, Color.Silver)
        If a.视频参数_色彩管理_像素格式 <> "" Then 在参数总览输出文本("像素格式：" & a.视频参数_色彩管理_像素格式, Color.Silver)
        If a.视频参数_色彩管理_传输特性 <> "" Then 在参数总览输出文本("传输特性：" & a.视频参数_色彩管理_传输特性, Color.Silver)
        If a.视频参数_色彩管理_范围 <> "" Then 在参数总览输出文本("色彩范围：" & a.视频参数_色彩管理_范围, Color.Silver)
        Select Case a.视频参数_色彩管理_处理方式
            Case 1 : 在参数总览输出文本("色彩管理写入元数据并转换", Color.Silver)
            Case 2 : 在参数总览输出文本("色彩管理只写入元数据不转换", Color.Silver)
            Case 3 : 在参数总览输出文本("色彩管理只转换不写元数据", Color.Silver)
        End Select

        If a.视频参数_色彩管理_亮度 <> "" Then 在参数总览输出文本("亮度调整：" & a.视频参数_色彩管理_亮度, Color.Silver)
        If a.视频参数_色彩管理_对比度 <> "" Then 在参数总览输出文本("对比度调整：" & a.视频参数_色彩管理_对比度, Color.Silver)
        If a.视频参数_色彩管理_饱和度 <> "" Then 在参数总览输出文本("饱和度调整：" & a.视频参数_色彩管理_饱和度, Color.Silver)
        If a.视频参数_色彩管理_伽马 <> "" Then 在参数总览输出文本("伽马调整：" & a.视频参数_色彩管理_伽马, Color.Silver)

        '---------------- 常见滤镜 ----------------
        If a.视频参数_降噪_方式 <> "" Then 在参数总览输出文本("降噪方式：" & a.视频参数_降噪_方式, Color.Silver)
        If a.视频参数_降噪_参数1 <> "" Then 在参数总览输出文本("降噪参数1：" & a.视频参数_降噪_参数1, Color.Silver)
        If a.视频参数_降噪_参数2 <> "" Then 在参数总览输出文本("降噪参数2：" & a.视频参数_降噪_参数2, Color.Silver)
        If a.视频参数_降噪_参数3 <> "" Then 在参数总览输出文本("降噪参数3：" & a.视频参数_降噪_参数3, Color.Silver)
        If a.视频参数_降噪_参数4 <> "" Then 在参数总览输出文本("降噪参数4：" & a.视频参数_降噪_参数4, Color.Silver)
        If a.视频参数_锐化_水平尺寸 <> "" OrElse a.视频参数_锐化_垂直尺寸 <> "" OrElse a.视频参数_锐化_锐化强度 <> "" Then
            在参数总览输出文本("锐化：水平 = " & a.视频参数_锐化_水平尺寸 & " 垂直 = " & a.视频参数_锐化_垂直尺寸 & " 强度 = " & a.视频参数_锐化_锐化强度, Color.Silver)
        End If
        If a.视频参数_逐行与隔行_操作 > 0 Then 在参数总览输出文本(UiComboBox逐行与隔行处理方式.Items(a.视频参数_逐行与隔行_操作), Color.Silver)
        If a.视频参数_画面翻转_角度翻转 > 0 Then 在参数总览输出文本(UiComboBox角度翻转.Items(a.视频参数_画面翻转_角度翻转), Color.Silver)
        If a.视频参数_画面翻转_镜像翻转 > 0 Then 在参数总览输出文本(UiComboBox镜像翻转.Items(a.视频参数_画面翻转_镜像翻转), Color.Silver)

        '---------------- 音频参数 ----------------
        If a.音频参数_编码器_具体编码 <> "" Then 在参数总览输出文本("音频编码器：" & a.音频参数_编码器_具体编码, Color.Silver)
        If a.音频参数_比特率 <> "" Then 在参数总览输出文本("音频比特率：" & a.音频参数_比特率, Color.Silver)
        If a.音频参数_质量参数名 <> "" Then 在参数总览输出文本("音频质量控制：" & a.音频参数_质量参数名 & "=" & a.音频参数_质量值, Color.Silver)
        If a.音频参数_声道数 <> "" Then 在参数总览输出文本("声道布局：" & a.音频参数_声道数, Color.Silver)
        If a.音频参数_采样率 <> "" Then 在参数总览输出文本("采样率：" & a.音频参数_采样率, Color.Silver)
        If a.音频参数_响度标准化_目标响度 <> "" Then 在参数总览输出文本("响度标准化目标：" & a.音频参数_响度标准化_目标响度, Color.Silver)
        If a.音频参数_响度标准化_动态范围 <> "" Then 在参数总览输出文本("响度动态范围：" & a.音频参数_响度标准化_动态范围, Color.Silver)
        If a.音频参数_响度标准化_峰值电平 <> "" Then 在参数总览输出文本("响度峰值电平：" & a.音频参数_响度标准化_峰值电平, Color.Silver)

        '---------------- 图片参数 ----------------
        If a.图片参数_编码器_编码名称 <> "" Then 在参数总览输出文本("图片编码器：" & a.图片参数_编码器_编码名称, Color.Silver)
        If a.图片参数_编码器_质量值 <> "" Then 在参数总览输出文本("图片质量：" & a.图片参数_编码器_质量值, Color.Silver)

        '---------------- 自定义参数 ----------------

        If a.自定义参数_开头参数 <> "" Then 在参数总览输出文本("自定义开头参数：" & a.自定义参数_开头参数, Color.Gray)
        If a.自定义参数_之前参数 <> "" Then 在参数总览输出文本("自定义之前参数：" & a.自定义参数_之前参数, Color.Gray)
        If a.自定义参数_视频滤镜 <> "" Then 在参数总览输出文本("自定义视频滤镜：" & a.自定义参数_视频滤镜, Color.Gray)
        If a.自定义参数_音频滤镜 <> "" Then 在参数总览输出文本("自定义音频滤镜：" & a.自定义参数_音频滤镜, Color.Gray)
        If a.自定义参数_filter_complex <> "" Then 在参数总览输出文本("自定义 filter_complex：" & a.自定义参数_filter_complex, Color.Gray)
        If a.自定义参数_视频参数 <> "" Then 在参数总览输出文本("自定义视频参数：" & a.自定义参数_视频参数, Color.Gray)
        If a.自定义参数_音频参数 <> "" Then 在参数总览输出文本("自定义音频参数：" & a.自定义参数_音频参数, Color.Gray)
        If a.自定义参数_之后参数 <> "" Then 在参数总览输出文本("自定义之后参数：" & a.自定义参数_之后参数, Color.Gray)
        If a.自定义参数_最后参数 <> "" Then 在参数总览输出文本("自定义最后参数：" & a.自定义参数_最后参数, Color.Gray)

        '---------------- 流控制 ----------------
        If a.流控制_启用保留其他视频流 Then 在参数总览输出文本("已选择保留其他视频流", Color.Silver)
        If a.流控制_将视频参数应用于指定流 IsNot Nothing AndAlso a.流控制_将视频参数应用于指定流.Length > 0 Then
            在参数总览输出文本("应用视频参数到流：" & String.Join(",", a.流控制_将视频参数应用于指定流), Color.Silver)
        End If
        If a.流控制_启用保留其他音频流 Then 在参数总览输出文本("已选择保留其他音频流", Color.Silver)
        If a.流控制_将音频参数应用于指定流 IsNot Nothing AndAlso a.流控制_将音频参数应用于指定流.Length > 0 Then
            在参数总览输出文本("应用音频参数到流：" & String.Join(",", a.流控制_将音频参数应用于指定流), Color.Silver)
        End If
        If a.流控制_启用保留内嵌字幕流 Then 在参数总览输出文本("已选择保留内嵌字幕流", Color.Silver)
        If a.流控制_启用自动混流同名字幕文件 Then 在参数总览输出文本("已选择自动混流同名字幕文件", Color.Silver)
        Select Case a.流控制_元数据选项
            Case 1 : 在参数总览输出文本("保留元数据", Color.Silver)
            Case 2 : 在参数总览输出文本("清除元数据", Color.Silver)
        End Select
        Select Case a.流控制_章节选项
            Case 1 : 在参数总览输出文本("保留章节", Color.Silver)
            Case 2 : 在参数总览输出文本("清除章节", Color.Silver)
        End Select
        If a.流控制_附件选项 = 1 Then 在参数总览输出文本("保留附件", Color.Silver)
        Select Case a.流控制_剪辑_方法
            Case 1
                在参数总览输出文本("粗剪", Color.Silver)
                在参数总览输出文本("剪辑入点：" & a.流控制_剪辑_入点, Color.Silver)
                在参数总览输出文本("剪辑出点：" & a.流控制_剪辑_出点, Color.Silver)
            Case 2
                在参数总览输出文本("精剪", Color.Silver)
                在参数总览输出文本("剪辑入点：" & a.流控制_剪辑_入点, Color.Silver)
                在参数总览输出文本("剪辑出点：" & a.流控制_剪辑_出点, Color.Silver)
            Case Else
                If a.流控制_剪辑_入点 <> "" OrElse a.流控制_剪辑_出点 <> "" Then
                    在参数总览输出文本("警告：指定了剪辑范围却没有指定剪辑方法，不会进行剪辑", Color.IndianRed)
                End If
        End Select

        '---------------- 其他 ----------------
        If a.输出位置 <> "" Then 在参数总览输出文本("输出位置：" & a.输出位置, Color.Gray)
    End Sub

End Class
