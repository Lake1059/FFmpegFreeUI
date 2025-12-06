Imports System.IO
Imports System.Text.Json
Imports Microsoft.WindowsAPICodePack.Dialogs
Imports Sunny.UI
Public Class 界面_常规流程参数_V2

    Public 是否已初始化 As Boolean = False

    Public 插帧页面 As New Form插帧
    Public 动态模糊页面 As New Form帧混合
    Public 画面裁剪页面 As New Form画面裁剪交互窗口
    Public 超分页面 As New Form超分

    Private Sub 界面_常规流程参数_Load(sender As Object, e As EventArgs) Handles Me.Load
        UiTabControlMenu1.ItemSize = New Size(200 * Form1.DPI, 36 * Form1.DPI)

        暗黑列表视图自绘制.绑定列表视图事件2(ListView2)
        ListView2.SmallImageList = Form1.ImageList1
        初始化进阶质量控制预制菜单项()
        设置富文本框行高(RichTextBox1, 350)
        设置富文本框行高(RichTextBox2, 350)
        设置富文本框行高(RichTextBox3, 300)
        设置富文本框行高(RichTextBox4, 300)

        AddHandler UiButton复制即时命令行显示.Click, Sub() Clipboard.SetText(RichTextBox1.Text)
        '==============================================
        AddHandler UiButton选择容器.MouseDown, AddressOf 选择输出容器鼠标按下事件
        AddHandler UiComboBox输出目录.SelectedIndexChanged, AddressOf 选择输出目录
        UiComboBox输出目录.SelectedIndex = 0
        AddHandler UiComboBox输出目录.DragEnter, Sub(s, e1) e1.Effect = If(e1.Data.GetDataPresent(DataFormats.FileDrop), DragDropEffects.Copy, DragDropEffects.None)
        AddHandler UiComboBox输出目录.DragDrop, Sub(s, e1)
                                                Dim F As String = e1.Data.GetData(DataFormats.FileDrop)(0)
                                                If FileIO.FileSystem.DirectoryExists(F) Then
                                                    UiComboBox输出目录.Text = "  " & F
                                                ElseIf FileIO.FileSystem.FileExists(F) Then
                                                    UiComboBox输出目录.Text = "  " & Path.GetDirectoryName(F)
                                                End If
                                            End Sub
        '==============================================
        AddHandler UiComboBox编码类别.SelectedIndexChanged, AddressOf 视频编码类别改动事件
        AddHandler UiComboBox具体编码.SelectedIndexChanged, AddressOf 视频具体编码改动事件
        '==============================================
        AddHandler UiComboBox分辨率.TextChanged, AddressOf 视频分辨率变动事件
        AddHandler UiTextBox分辨率自动计算宽度.TextChanged, AddressOf 视频分辨率自动计算宽度变动事件
        AddHandler UiTextBox分辨率自动计算高度.TextChanged, AddressOf 视频分辨率自动计算高度变动事件
        AddHandler UiButton裁剪交互窗口.Click, Sub() 显示窗体(Form画面裁剪交互窗口, Form1)
        '==============================================
        AddHandler UiButton打开插帧参数窗口.Click, Sub()
                                               显示窗体(插帧页面, Me.ParentForm)
                                               SetControlFont(用户设置.实例对象.字体, 插帧页面)
                                           End Sub
        AddHandler UiButton打开动态模糊参数窗口.Click, Sub()
                                                 显示窗体(动态模糊页面, Me.ParentForm)
                                                 SetControlFont(用户设置.实例对象.字体, 动态模糊页面)
                                             End Sub
        AddHandler UiButton打开超分参数窗口.Click, Sub()
                                               显示窗体(超分页面, Me.ParentForm)
                                               SetControlFont(用户设置.实例对象.字体, 超分页面)
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
        '==============================================
        AddHandler UiComboBox音频编码器.TextChanged, AddressOf 音频编码参数变动事件
        AddHandler UiComboBox音频比特率.TextChanged, AddressOf 音频比特率参数变动事件
        AddHandler UiComboBox音频质量参数.TextChanged, AddressOf 音频质量参数变动事件
        '==============================================
        AddHandler UiComboBox图片编码器.TextChanged, AddressOf 图片编码器参数变动事件
        '==============================================
        AddHandler UiTextBox将视频参数用于这些流.TextBox.MouseWheel, Sub(s1, e1)
                                                               Select Case e1.Delta
                                                                   Case > 0 : UiTextBox将视频参数用于这些流.Text = "0:v"
                                                                   Case < 0 : UiTextBox将视频参数用于这些流.Text = "0:v:0"
                                                               End Select
                                                           End Sub
        AddHandler UiTextBox将音频参数用于这些流.TextBox.MouseWheel, Sub(s1, e1)
                                                               Select Case e1.Delta
                                                                   Case > 0 : UiTextBox将音频参数用于这些流.Text = "0:a"
                                                                   Case < 0 : UiTextBox将音频参数用于这些流.Text = "0:a:0"
                                                               End Select
                                                           End Sub
        AddHandler UiTextBox使用哪些文件的哪些内嵌字幕.TextBox.MouseWheel, Sub(s1, e1)
                                                                  Select Case e1.Delta
                                                                      Case > 0 : UiTextBox使用哪些文件的哪些内嵌字幕.Text = "0:s"
                                                                      Case < 0 : UiTextBox使用哪些文件的哪些内嵌字幕.Text = "0:s:0"
                                                                  End Select
                                                              End Sub
        '==============================================
        AddHandler UiButton刷新预设列表.Click, AddressOf 刷新预设列表
        AddHandler UiButton保存预设.Click, AddressOf 保存到预设
        AddHandler UiButton读取预设.Click, AddressOf 加载选中预设
        AddHandler UiButton导出预设.Click, AddressOf 保存预设到文件
        AddHandler UiButton导入预设.Click, AddressOf 从文件读取预设

        AddHandler ListView2.SelectedIndexChanged, AddressOf 预设列表视图选中变化事件
        AddHandler ListView2.DoubleClick, AddressOf 加载选中预设
        AddHandler ListView2.BeforeLabelEdit, AddressOf 预设列表视图项文本更改前事件
        AddHandler ListView2.AfterLabelEdit, AddressOf 预设列表视图项文本更改后事件
        AddHandler ListView2.SizeChanged, Sub() ListView2.Columns(0).Width = ListView2.Parent.Width - ListView2.Parent.Padding.Left - SystemInformation.VerticalScrollBarWidth * Form1.DPI
        AddHandler UiButton重置参数面板.Click, Sub() 预设管理.重置全部包含在预设中的设置(Me)
        '==============================================
        UiComboBox自动命名选项.SelectedIndex = 0
        '==============================================
        If Me IsNot Form1.常规流程参数页面 Then
            Panel69.Visible = False
        Else
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
            Select Case 用户设置.实例对象.自动加载预设选项
                Case 用户设置.自动加载预设选项枚举.自动加载最后的预设文件, 用户设置.自动加载预设选项枚举.自动加载指定的预设文件
                    If FileIO.FileSystem.FileExists(用户设置.实例对象.自动加载预设文件路径) Then
                        预设管理.显示预设(JsonSerializer.Deserialize(Of 预设数据类型)(FileIO.FileSystem.ReadAllText(用户设置.实例对象.自动加载预设文件路径)), Me)
                    End If
                Case 用户设置.自动加载预设选项枚举.自动加载上次的全部改动
                    If 用户设置.实例对象.最后的预设数据 IsNot Nothing Then 预设管理.显示预设(用户设置.实例对象.最后的预设数据, Me)
            End Select
        End If

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
        AddHandler UiComboBox剪辑方法.MouseWheel, AddressOf 下拉框鼠标滚轮事件
        AddHandler UiComboBox剪辑向前解码多久秒.MouseWheel, AddressOf 下拉框鼠标滚轮事件
        '==================================================
        AddHandler UiComboBox使用哪些文件的哪些内嵌字幕_如何操作.MouseWheel, AddressOf 下拉框鼠标滚轮事件
        AddHandler UiComboBox元数据选项.MouseWheel, AddressOf 下拉框鼠标滚轮事件
        AddHandler UiComboBox章节选项.MouseWheel, AddressOf 下拉框鼠标滚轮事件
        AddHandler UiComboBox附件选项.MouseWheel, AddressOf 下拉框鼠标滚轮事件
        '==================================================
        AddHandler UiComboBox自动加载预设选项.MouseWheel, AddressOf 下拉框鼠标滚轮事件
        '==================================================



        界面校准()
        是否已初始化 = True
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
        Select Case UiComboBox编码类别.SelectedIndex
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
                UiComboBox具体编码.Items.Add("libsvtav1")
                UiComboBox具体编码.Items.Add("av1_nvenc")
                UiComboBox具体编码.Items.Add("av1_qsv")
                UiComboBox具体编码.Items.Add("av1_amf")
                UiComboBox具体编码.Items.Add("libaom-av1")
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
                UiComboBox具体编码.Items.Add("cfhd")
            Case 10    '老旧格式
                UiComboBox具体编码.Items.Add("mpeg4")
                UiComboBox具体编码.Items.Add("libxvid")
                UiComboBox具体编码.Items.Add("rv20")
                UiComboBox具体编码.Items.Add("rv10")
                UiComboBox具体编码.Items.Add("wmv2")
                UiComboBox具体编码.Items.Add("wmv1")
            Case 11    '禁用
                UiComboBox具体编码.Items.Add("")
            Case 12
                For Each item In 用户设置.实例对象.自定义视频编码器列表
                    UiComboBox具体编码.Items.Add(item)
                Next
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
                UiTextBox降噪参数4.Watermark = "默认 4.5"
                Panel36.Visible = True
                Panel35.Visible = True
            Case 2    'nlmeans - 高级降噪，效果更好且计算量更大
                Label降噪参数1.Text = "降噪强度" & vbCrLf & "s (strength)"
                UiTextBox降噪参数1.Watermark = "默认 1.0"
                Label降噪参数2.Text = "参考像素块大小" & vbCrLf & "p (patch size) 须奇数"
                UiTextBox降噪参数2.Watermark = "默认 7"
                Label降噪参数3.Text = "平面配置" & vbCrLf & "pc (plane config)"
                UiTextBox降噪参数3.Watermark = "默认 0"
                Label降噪参数4.Text = "搜索半径" & vbCrLf & "r (research size)"
                UiTextBox降噪参数4.Watermark = "默认 15"
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
                UiTextBox降噪参数1.Watermark = "默认 10"
                Label降噪参数2.Text = "块大小" & vbCrLf & "block"
                UiTextBox降噪参数2.Watermark = "默认 16"
                Label降噪参数3.Text = "块步长" & vbCrLf & "bstep"
                UiTextBox降噪参数3.Watermark = "默认 4"
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

    Sub 音频比特率参数变动事件()
        If UiComboBox音频比特率.Text = "" Then
            UiComboBox音频质量参数.Text = ""
            UiTextBox音频质量值.Text = ""
        End If
    End Sub

    Sub 音频质量参数变动事件()
        If UiComboBox音频质量参数.Text = "" Then
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
            Case 选项卡.IsEqual(TabPage参数总览)
                Panel72.Width = Panel72.Parent.Width * 0.5
                Dim a As New 预设数据类型
                预设管理.储存预设(a, Me)
                预设管理.显示参数总览(RichTextBox2, a)
                RichTextBox1.Text = "ffmpeg " & 预设管理.将预设数据转换为命令行(a, LanguageManager.GetTranslation("Placeholder.InputFile", "<输入文件>"), LanguageManager.GetTranslation("Placeholder.OutputFile", "<输出文件>"))

            Case 选项卡.IsEqual(TabPage输出文件设置)
                校准UiComboBox视觉(UiComboBox自动命名选项)
                校准UiComboBox视觉(UiComboBox输出目录)
                UiCheckBox保留创建时间.CheckBoxSize = 20 * Form1.DPI
                UiCheckBox保留修改时间.CheckBoxSize = 20 * Form1.DPI
                UiCheckBox保留访问时间.CheckBoxSize = 20 * Form1.DPI

            Case 选项卡.IsEqual(TabPage解码设置)
                校准UiComboBox视觉(UiComboBox解码器)
                校准UiComboBox视觉(UiComboBox解码数据格式)
                校准UiComboBox视觉(UiComboBox硬件加速解码参数名)

            Case 选项卡.IsEqual(TabPage视频参数编码器)
                校准UiComboBox视觉(UiComboBox编码类别)
                校准UiComboBox视觉(UiComboBox具体编码)
                校准UiComboBox视觉(UiComboBox编码预设)
                校准UiComboBox视觉(UiComboBox配置文件)
                校准UiComboBox视觉(UiComboBox场景优化)

            Case 选项卡.IsEqual(TabPage视频参数画面帧)
                校准UiComboBox视觉(UiComboBox分辨率)
                校准UiComboBox视觉(UiComboBox帧速率)

            Case 选项卡.IsEqual(TabPage视频参数质量)
                校准UiComboBox视觉(UiComboBox全局质量控制方式)
                校准UiComboBox视觉(UiComboBox全局质量控制参数)

            Case 选项卡.IsEqual(TabPage视频参数色彩管理)
                校准UiComboBox视觉(UiComboBox像素格式)
                校准UiComboBox视觉(UiComboBox色彩空间滤镜选择)
                校准UiComboBox视觉(UiComboBox矩阵系数)
                校准UiComboBox视觉(UiComboBox色域)
                校准UiComboBox视觉(UiComboBox传输特性)
                校准UiComboBox视觉(UiComboBox色彩范围)
                校准UiComboBox视觉(UiComboBox色调映射算法)
                校准UiComboBox视觉(UiComboBox色彩管理处理方式)

            Case 选项卡.IsEqual(TabPage视频参数常见滤镜)
                校准UiComboBox视觉(UiComboBox降噪方式)
                校准UiComboBox视觉(UiComboBox逐行与隔行处理方式)
                校准UiComboBox视觉(UiComboBox角度翻转)
                校准UiComboBox视觉(UiComboBox镜像翻转)

            Case 选项卡.IsEqual(TabPage音频参数)
                校准UiComboBox视觉(UiComboBox音频编码器)
                校准UiComboBox视觉(UiComboBox音频比特率)
                校准UiComboBox视觉(UiComboBox音频质量参数)
                校准UiComboBox视觉(UiComboBox声道布局)
                校准UiComboBox视觉(UiComboBox采样率)

            Case 选项卡.IsEqual(TabPage图片参数)
                校准UiComboBox视觉(UiComboBox图片编码器)

            Case 选项卡.IsEqual(TabPage自定义参数)
                UiTabControl1.ItemSize = New Size(150 * Form1.DPI, 50 * Form1.DPI)

            Case 选项卡.IsEqual(TabPage剪辑区间)
                校准UiComboBox视觉(UiComboBox剪辑方法)
                校准UiComboBox视觉(UiComboBox剪辑向前解码多久秒)

            Case 选项卡.IsEqual(TabPage流控制)
                UiCheckBox保留其他视频流.CheckBoxSize = 20 * Form1.DPI
                UiCheckBox保留其他音频流.CheckBoxSize = 20 * Form1.DPI
                校准UiComboBox视觉(UiComboBox使用哪些文件的哪些内嵌字幕_如何操作)
                UiCheckBox自动混流SRT.CheckBoxSize = 20 * Form1.DPI
                UiCheckBox自动混流ASS.CheckBoxSize = 20 * Form1.DPI
                UiCheckBox自动混流SSA.CheckBoxSize = 20 * Form1.DPI
                UiCheckBox自动混流字幕转为movtext.CheckBoxSize = 20 * Form1.DPI
                校准UiComboBox视觉(UiComboBox元数据选项)
                校准UiComboBox视觉(UiComboBox章节选项)
                校准UiComboBox视觉(UiComboBox附件选项)

            Case 选项卡.IsEqual(TabPage方案管理)
                UiCheckBox额外保存信息.CheckBoxSize = 20 * Form1.DPI
                校准UiComboBox视觉(UiComboBox自动加载预设选项)
                If ListView2.Items.Count = 0 Then 刷新预设列表()
        End Select

    End Sub

    Sub 刷新预设列表()
        Me.RichTextBox3.Text = ""
        Me.RichTextBox4.Text = ""
        Dim s1 = (Panel77.Width - Panel77.Padding.Left - Panel77.Padding.Right - Label157.Width * 2) / 3
        Panel82.Width = s1
        Panel83.Width = s1
        ListView2.Items.Clear()
        For Each p As String In 扫描单层文件(Path.Combine(Application.StartupPath, "Preset"), "*.json")
            ListView2.Items.Add(Path.GetFileNameWithoutExtension(p))
        Next
    End Sub
    Sub 预设列表视图选中变化事件()
        Select Case ListView2.SelectedItems.Count
            Case 1
                Dim a As 预设数据类型 = JsonSerializer.Deserialize(Of 预设数据类型)(FileIO.FileSystem.ReadAllText(Path.Combine(Application.StartupPath, "Preset", ListView2.SelectedItems(0).Text & ".json")))
                预设管理.显示参数总览(Me.RichTextBox3, a)
                RichTextBox4.Text = "ffmpeg " & 预设管理.将预设数据转换为命令行(a, LanguageManager.GetTranslation("Placeholder.InputFile", "<输入文件>"), LanguageManager.GetTranslation("Placeholder.OutputFile", "<输出文件>"))
            Case Else
                Me.RichTextBox3.Text = ""
                Me.RichTextBox4.Text = ""
        End Select
    End Sub
    Sub 保存到预设()
        Select Case ListView2.SelectedItems.Count
            Case 1
                Dim a As New 预设数据类型
                预设管理.储存预设(a, Me)
                File.WriteAllText(Path.Combine(Application.StartupPath, "Preset", ListView2.SelectedItems(0).Text & ".json"), JsonSerializer.Serialize(a, JsonSO))
                Sunny.UI.UIMessageTip.ShowOk($"{LanguageManager.GetTranslation("Message.Saved", "已保存：")}{ListView2.SelectedItems(0).Text}", 1500, False)
            Case 0
                Dim a As New 预设数据类型
                预设管理.储存预设(a, Me)
                Dim 名称 As String = $"预设-{Now:yyyyMMdd-HHmmss}"
                File.WriteAllText(Path.Combine(Application.StartupPath, "Preset", 名称 & ".json"), JsonSerializer.Serialize(a, JsonSO))
                ListView2.Items.Add(名称)
                Sunny.UI.UIMessageTip.ShowOk($"{LanguageManager.GetTranslation("Message.Saved", "已保存：")}{名称}", 1500, False)
        End Select
    End Sub
    Sub 加载选中预设()
        Select Case ListView2.SelectedItems.Count
            Case 1
                预设管理.显示预设(JsonSerializer.Deserialize(Of 预设数据类型)(FileIO.FileSystem.ReadAllText(Path.Combine(Application.StartupPath, "Preset", ListView2.SelectedItems(0).Text & ".json"))), Me)
                Sunny.UI.UIMessageTip.ShowOk($"{LanguageManager.GetTranslation("Message.Loaded", "已加载：")}{ ListView2.SelectedItems(0).Text}", 1500, False)
        End Select
    End Sub
    Sub 预设列表视图项文本更改前事件(sender As Object, e As LabelEditEventArgs)
        预设列表重命名之前的文本 = ListView2.Items(e.Item).Text
    End Sub
    Dim 预设列表重命名之前的文本 As String
    Sub 预设列表视图项文本更改后事件(sender As Object, e As LabelEditEventArgs)
        If 预设列表重命名之前的文本 = e.Label Then Exit Sub
        If Not FileIO.FileSystem.FileExists(Path.Combine(Application.StartupPath, "Preset", e.Label & ".json")) AndAlso e.Label <> "" AndAlso e.CancelEdit = False Then
            FileIO.FileSystem.RenameFile(Path.Combine(Application.StartupPath, "Preset", 预设列表重命名之前的文本 & ".json"), e.Label & ".json")
        Else
            e.CancelEdit = True
        End If
    End Sub
    Public Sub 保存预设到文件()
        Dim d As New SaveFileDialog With {.Filter = "Json|*.json", .Title = LanguageManager.GetTranslation("Dialog.SavePresetTitle", "将方案保存到单独文件（要加入列表请放到 Preset 文件夹）")}
        If ListView2.SelectedItems.Count > 0 Then Sunny.UI.UIMessageTip.ShowWarning(LanguageManager.GetTranslation("Message.NotSavedToList", "注意这不是保存到列表中"), 1500, False)
        d.ShowDialog(Form1)
        If d.FileName = "" Then Exit Sub
        Dim a As New 预设数据类型
        预设管理.储存预设(a, Me)
        File.WriteAllText(d.FileName, JsonSerializer.Serialize(a, JsonSO))
        Select Case 用户设置.实例对象.自动加载预设选项
            Case 用户设置.自动加载预设选项枚举.自动加载最后的预设文件
                用户设置.实例对象.自动加载预设文件路径 = d.FileName
                Me.UiTextBox自动加载的预设文件路径.Text = d.FileName
        End Select
    End Sub
    Public Sub 从文件读取预设()
        Dim d As New OpenFileDialog With {.Filter = "Json|*.json", .Title = LanguageManager.GetTranslation("Dialog.LoadPresetTitle", "加载外部预设方案文件（此操作不会将其加入列表）")}
        d.ShowDialog(Form1)
        If Not File.Exists(d.FileName) Then Exit Sub
        Dim a As 预设数据类型 = JsonSerializer.Deserialize(Of 预设数据类型)(File.ReadAllText(d.FileName))
        预设管理.显示预设(a, Me)
        Me.RichTextBox1.Text = "ffmpeg " & 预设管理.将预设数据转换为命令行(a, LanguageManager.GetTranslation("Placeholder.InputFile", "<输入文件>"), LanguageManager.GetTranslation("Placeholder.OutputFile", "<输出文件>"))
        Select Case 用户设置.实例对象.自动加载预设选项
            Case 用户设置.自动加载预设选项枚举.自动加载最后的预设文件
                用户设置.实例对象.自动加载预设文件路径 = d.FileName
                Form1.常规流程参数页面.UiTextBox自动加载的预设文件路径.Text = d.FileName
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
        New ToolStripMenuItem("-qpmax 量化最大值（最低画质）", Nothing, Sub(s1, e1) 创建进阶质量控制项("-qpmax ")) With {.ForeColor = Color.Silver},
        New ToolStripMenuItem("-qcomp 量化系数非线性压缩因子 0.0~1.0", Nothing, Sub(s1, e1) 创建进阶质量控制项("-qcomp ")) With {.ForeColor = Color.Silver},
        New ToolStripMenuItem("-aq-mode 自适应量化模式 适用于 libaom-av1", Nothing, Sub(s1, e1) 创建进阶质量控制项("-aq-mode ")) With {.ForeColor = Color.Silver},
        New ToolStripMenuItem("-arnr-strength 自适应降噪强度 0~6 适用于 libaom-av1", Nothing, Sub(s1, e1) 创建进阶质量控制项("-arnr-strength ")) With {.ForeColor = Color.Silver},
        New ToolStripMenuItem("-la_depth 前向参考帧数 适用于 libsvtav1", Nothing, Sub(s1, e1) 创建进阶质量控制项("-la_depth ")) With {.ForeColor = Color.Silver},
        New ToolStripMenuItem("-qp_i 关键帧质量", Nothing, Sub(s1, e1) 创建进阶质量控制项("-qp_i ")) With {.ForeColor = Color.Silver},
        New ToolStripMenuItem("-qp_p 前向参考帧质量", Nothing, Sub(s1, e1) 创建进阶质量控制项("-qp_p ")) With {.ForeColor = Color.Silver},
        New ToolStripMenuItem("-qp_b 双向参考帧质量", Nothing, Sub(s1, e1) 创建进阶质量控制项("-qp_b ")) With {.ForeColor = Color.Silver},
        New ToolStripSeparator() With {.Tag = "null"}})
    End Sub

    Sub 选择输出容器鼠标按下事件(sender As Object, e As MouseEventArgs)

        Dim b As New 暗黑上下文菜单 With {.ShowImageMargin = False, .Font = Form1.Font}
        b.Items.AddRange(New ToolStripItem() {
             New ToolStripSeparator() With {.Tag = "null"},
             New ToolStripMenuItem(".mp4", Nothing, Sub(s1, e1) UiTextBox输出容器.Text = s1.Text) With {.ForeColor = Color.Silver},
             New ToolStripMenuItem(".mkv", Nothing, Sub(s1, e1) UiTextBox输出容器.Text = s1.Text) With {.ForeColor = Color.Silver},
             New ToolStripMenuItem(".flv", Nothing, Sub(s1, e1) UiTextBox输出容器.Text = s1.Text) With {.ForeColor = Color.Silver},
             New ToolStripMenuItem(".mov", Nothing, Sub(s1, e1) UiTextBox输出容器.Text = s1.Text) With {.ForeColor = Color.Silver},
             New ToolStripMenuItem(".webm", Nothing, Sub(s1, e1) UiTextBox输出容器.Text = s1.Text) With {.ForeColor = Color.Silver},
             New ToolStripMenuItem(".wmv", Nothing, Sub(s1, e1) UiTextBox输出容器.Text = s1.Text) With {.ForeColor = Color.Silver},
             New ToolStripMenuItem(".avi", Nothing, Sub(s1, e1) UiTextBox输出容器.Text = s1.Text) With {.ForeColor = Color.Silver},
             New ToolStripMenuItem(".rmvb", Nothing, Sub(s1, e1) UiTextBox输出容器.Text = s1.Text) With {.ForeColor = Color.Silver},
             New ToolStripMenuItem(".ts", Nothing, Sub(s1, e1) UiTextBox输出容器.Text = s1.Text) With {.ForeColor = Color.Silver},
             New ToolStripMenuItem(".3gp", Nothing, Sub(s1, e1) UiTextBox输出容器.Text = s1.Text) With {.ForeColor = Color.Silver},
             New ToolStripSeparator() With {.Tag = "null"}})

        Dim c As New 暗黑上下文菜单 With {.ShowImageMargin = False, .Font = Form1.Font}
        c.Items.AddRange(New ToolStripItem() {
             New ToolStripSeparator() With {.Tag = "null"},
             New ToolStripMenuItem(".mp3", Nothing, Sub(s1, e1) UiTextBox输出容器.Text = s1.Text) With {.ForeColor = Color.Silver},
             New ToolStripMenuItem(".aac", Nothing, Sub(s1, e1) UiTextBox输出容器.Text = s1.Text) With {.ForeColor = Color.Silver},
             New ToolStripMenuItem(".wav", Nothing, Sub(s1, e1) UiTextBox输出容器.Text = s1.Text) With {.ForeColor = Color.Silver},
             New ToolStripMenuItem(".flac", Nothing, Sub(s1, e1) UiTextBox输出容器.Text = s1.Text) With {.ForeColor = Color.Silver},
             New ToolStripMenuItem(".alac", Nothing, Sub(s1, e1) UiTextBox输出容器.Text = s1.Text) With {.ForeColor = Color.Silver},
             New ToolStripMenuItem(".aiff", Nothing, Sub(s1, e1) UiTextBox输出容器.Text = s1.Text) With {.ForeColor = Color.Silver},
             New ToolStripMenuItem(".ac3", Nothing, Sub(s1, e1) UiTextBox输出容器.Text = s1.Text) With {.ForeColor = Color.Silver},
             New ToolStripMenuItem(".ogg", Nothing, Sub(s1, e1) UiTextBox输出容器.Text = s1.Text) With {.ForeColor = Color.Silver},
             New ToolStripMenuItem(".opus", Nothing, Sub(s1, e1) UiTextBox输出容器.Text = s1.Text) With {.ForeColor = Color.Silver},
             New ToolStripMenuItem(".m4a", Nothing, Sub(s1, e1) UiTextBox输出容器.Text = s1.Text) With {.ForeColor = Color.Silver},
             New ToolStripSeparator() With {.Tag = "null"}})

        Dim d As New 暗黑上下文菜单 With {.ShowImageMargin = False, .Font = Form1.Font}
        d.Items.AddRange(New ToolStripItem() {
             New ToolStripSeparator() With {.Tag = "null"},
             New ToolStripMenuItem(".png", Nothing, Sub(s1, e1) UiTextBox输出容器.Text = s1.Text) With {.ForeColor = Color.Silver},
             New ToolStripMenuItem(".jpg", Nothing, Sub(s1, e1) UiTextBox输出容器.Text = s1.Text) With {.ForeColor = Color.Silver},
             New ToolStripMenuItem(".jpeg", Nothing, Sub(s1, e1) UiTextBox输出容器.Text = s1.Text) With {.ForeColor = Color.Silver},
             New ToolStripMenuItem(".webp", Nothing, Sub(s1, e1) UiTextBox输出容器.Text = s1.Text) With {.ForeColor = Color.Silver},
             New ToolStripMenuItem(".avif", Nothing, Sub(s1, e1) UiTextBox输出容器.Text = s1.Text) With {.ForeColor = Color.Silver},
             New ToolStripMenuItem(".bmp", Nothing, Sub(s1, e1) UiTextBox输出容器.Text = s1.Text) With {.ForeColor = Color.Silver},
             New ToolStripMenuItem(".gif", Nothing, Sub(s1, e1) UiTextBox输出容器.Text = s1.Text) With {.ForeColor = Color.Silver},
             New ToolStripMenuItem(".ico", Nothing, Sub(s1, e1) UiTextBox输出容器.Text = s1.Text) With {.ForeColor = Color.Silver},
             New ToolStripSeparator() With {.Tag = "null"}})

        Dim a As New 暗黑上下文菜单 With {.ShowImageMargin = False, .Font = Form1.Font}
        a.Items.AddRange(New ToolStripItem() {
             New ToolStripSeparator() With {.Tag = "null"},
             New ToolStripMenuItem("全部分类") With {.ForeColor = Color.CornflowerBlue, .Enabled = False},
             New ToolStripMenuItem("视频") With {.ForeColor = Color.Silver, .DropDown = b},
             New ToolStripMenuItem("音频") With {.ForeColor = Color.Silver, .DropDown = c},
             New ToolStripMenuItem("图片") With {.ForeColor = Color.Silver, .DropDown = d},
             New ToolStripMenuItem("常用容器") With {.ForeColor = Color.CornflowerBlue, .Enabled = False},
             New ToolStripMenuItem(".mp4", Nothing, Sub(s1, e1) UiTextBox输出容器.Text = s1.Text) With {.ForeColor = Color.Silver},
             New ToolStripMenuItem(".mkv", Nothing, Sub(s1, e1) UiTextBox输出容器.Text = s1.Text) With {.ForeColor = Color.Silver},
             New ToolStripSeparator() With {.Tag = "null"}})

        a.Show(sender, New Point(0, sender.Height))

    End Sub

    Sub 选择输出目录()
        If UiComboBox输出目录.SelectedIndex = 1 Then
            Dim dialog As New CommonOpenFileDialog With {.IsFolderPicker = True}
            If dialog.ShowDialog() = CommonFileDialogResult.Ok Then
                If 用户设置.实例对象.转译模式 Then
                    UiComboBox输出目录.Text = "  " & 转译模式处理路径(dialog.FileName)
                Else
                    UiComboBox输出目录.Text = "  " & dialog.FileName
                End If
            Else
                UiComboBox输出目录.SelectedIndex = 0
            End If
        End If
    End Sub



End Class
