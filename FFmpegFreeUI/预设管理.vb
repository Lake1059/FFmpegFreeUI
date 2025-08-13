Imports System.IO
Imports System.Text.Json
Imports Sunny.UI
Public Class 预设管理

    Public Shared Sub 显示预设(a As 预设数据类型)
        Form1.UiTextBox输出容器.Text = a.输出容器

        Form1.常规流程参数页面.UiComboBox解码器.Text = a.解码参数_解码器
        Form1.常规流程参数页面.UiTextBoxCPU解码线程数.Text = a.解码参数_threads

        Form1.常规流程参数页面.UiComboBox解码数据格式.Text = a.解码参数_解码数据格式
        Form1.常规流程参数页面.UiTextBoxhwaccel_device.Text = a.解码参数_hwaccel_device
        Form1.常规流程参数页面.UiTextBoxinit_hw_device.Text = a.解码参数_init_hw_device
        Form1.常规流程参数页面.UiTextBoxqsv_device.Text = a.解码参数_qsv_device

        Form1.常规流程参数页面.UiComboBox自动命名选项.SelectedIndex = a.输出命名_自动命名选项
        Form1.常规流程参数页面.UiTextBox开头文本.Text = a.输出命名_开头文本
        Form1.常规流程参数页面.UiTextBox替代文本.Text = a.输出命名_替代文本
        Form1.常规流程参数页面.UiTextBox结尾文本.Text = a.输出命名_结尾文本
        Form1.常规流程参数页面.UiCheckBox不使用输出文件参数.Checked = a.输出命名_不使用输出文件参数

        Form1.常规流程参数页面.UiComboBox编码类别.Text = a.视频参数_编码器_类别
        Form1.常规流程参数页面.UiComboBox具体编码.Text = a.视频参数_编码器_具体编码
        Form1.常规流程参数页面.UiComboBox编码预设.Text = a.视频参数_编码器_质量
        Form1.常规流程参数页面.UiComboBox配置文件.Text = a.视频参数_编码器_配置文件
        Form1.常规流程参数页面.UiComboBox场景优化.Text = a.视频参数_编码器_场景优化
        Form1.常规流程参数页面.UiTextBoxgpu.Text = a.视频参数_编码器_gpu
        Form1.常规流程参数页面.UiTextBoxthreads.Text = a.视频参数_编码器_threads

        Form1.常规流程参数页面.UiComboBox分辨率.Text = a.视频参数_分辨率
        Form1.常规流程参数页面.UiTextBox分辨率自动计算宽度.Text = a.视频参数_分辨率自动计算_宽度
        Form1.常规流程参数页面.UiTextBox分辨率自动计算高度.Text = a.视频参数_分辨率自动计算_高度
        Form1.常规流程参数页面.UiTextBox画面裁剪滤镜参数.Text = a.视频参数_分辨率_裁剪滤镜参数
        Form1.常规流程参数页面.UiComboBox帧速率.Text = a.视频参数_帧速率
        Form1.常规流程参数页面.UiTextBox抽帧最大变化比例.Text = a.视频参数_帧速率_抽帧最大变化比例

        Form插帧.UiTextBox要补到多少帧.Text = a.视频参数_插帧_目标帧率
        Select Case a.视频参数_插帧_插帧模式
            Case "dup" : Form插帧.UiComboBox插帧模式.SelectedIndex = 1
            Case "blend" : Form插帧.UiComboBox插帧模式.SelectedIndex = 2
            Case "mci" : Form插帧.UiComboBox插帧模式.SelectedIndex = 3
        End Select
        Select Case a.视频参数_插帧_运动估计模式
            Case "bidir" : Form插帧.UiComboBox运动估计模式.SelectedIndex = 1
            Case "bilat" : Form插帧.UiComboBox运动估计模式.SelectedIndex = 2
        End Select
        Select Case a.视频参数_插帧_运动估计算法
            Case "esa" : Form插帧.UiComboBox运动估计算法.SelectedIndex = 1
            Case "tss" : Form插帧.UiComboBox运动估计算法.SelectedIndex = 2
            Case "tdls" : Form插帧.UiComboBox运动估计算法.SelectedIndex = 3
            Case "ntss" : Form插帧.UiComboBox运动估计算法.SelectedIndex = 4
            Case "fss" : Form插帧.UiComboBox运动估计算法.SelectedIndex = 5
            Case "ds" : Form插帧.UiComboBox运动估计算法.SelectedIndex = 6
            Case "hexbs" : Form插帧.UiComboBox运动估计算法.SelectedIndex = 7
            Case "epzs" : Form插帧.UiComboBox运动估计算法.SelectedIndex = 8
            Case "umh" : Form插帧.UiComboBox运动估计算法.SelectedIndex = 9
        End Select
        Select Case a.视频参数_插帧_运动补偿模式
            Case "obmc" : Form插帧.UiComboBox运动补偿模式.SelectedIndex = 1
            Case "aobmc" : Form插帧.UiComboBox运动补偿模式.SelectedIndex = 2
        End Select
        Form插帧.UiCheckBox可变块大小的运动补偿.Checked = a.视频参数_插帧_可变块大小的运动补偿
        Form插帧.UiTextBox块大小.Text = a.视频参数_插帧_块大小
        Form插帧.UiTextBox搜索范围.Text = a.视频参数_插帧_搜索范围
        Form插帧.UiTextBox场景变化检测强度.Text = a.视频参数_插帧_场景变化检测强度

        Form帧混合.UiTextBox降低帧率.Text = a.视频参数_帧混合_指定帧率
        Select Case a.视频参数_帧混合_混合模式
            Case "average" : Form帧混合.UiComboBox混合算法.SelectedIndex = 1
            Case "difference" : Form帧混合.UiComboBox混合算法.SelectedIndex = 2
            Case "and" : Form帧混合.UiComboBox混合算法.SelectedIndex = 3
            Case "or" : Form帧混合.UiComboBox混合算法.SelectedIndex = 4
            Case "xor" : Form帧混合.UiComboBox混合算法.SelectedIndex = 5
            Case "add" : Form帧混合.UiComboBox混合算法.SelectedIndex = 6
            Case "multiply" : Form帧混合.UiComboBox混合算法.SelectedIndex = 7
        End Select
        Form帧混合.UiTextBox混合比例.Text = a.视频参数_帧混合_混合比例

        Select Case a.视频参数_比特率_控制方式
            Case "VBR" : Form1.常规流程参数页面.UiComboBox比特率控制方式.SelectedIndex = 1
            Case "VBR HQ" : Form1.常规流程参数页面.UiComboBox比特率控制方式.SelectedIndex = 2
            Case "CRF" : Form1.常规流程参数页面.UiComboBox比特率控制方式.SelectedIndex = 3
            Case "CQP" : Form1.常规流程参数页面.UiComboBox比特率控制方式.SelectedIndex = 4
            Case "CBR" : Form1.常规流程参数页面.UiComboBox比特率控制方式.SelectedIndex = 5
        End Select
        Form1.常规流程参数页面.UiTextBox硬件加速HQ前瞻分析帧数.Text = a.视频参数_质量控制_前瞻分析帧数
        Form1.常规流程参数页面.UiTextBox基础比特率.Text = a.视频参数_比特率_基础
        Form1.常规流程参数页面.UiTextBox最低比特率.Text = a.视频参数_比特率_最低值
        Form1.常规流程参数页面.UiTextBox最高比特率.Text = a.视频参数_比特率_最高值
        Form1.常规流程参数页面.UiTextBox比特率缓冲区.Text = a.视频参数_比特率_缓冲区

        Select Case a.视频参数_质量控制_参数
            Case "cq" : Form1.常规流程参数页面.UiComboBox全局质量控制参数.SelectedIndex = 1
            Case "qp" : Form1.常规流程参数页面.UiComboBox全局质量控制参数.SelectedIndex = 2
            Case "crf" : Form1.常规流程参数页面.UiComboBox全局质量控制参数.SelectedIndex = 3
        End Select
        Form1.常规流程参数页面.UiTextBox全局质量控制值.Text = a.视频参数_质量控制_值
        Form1.常规流程参数页面.UiTextBox质量最小值.Text = a.视频参数_质量控制_qpmin
        Form1.常规流程参数页面.UiTextBox质量最大值.Text = a.视频参数_质量控制_qpmax
        Form1.常规流程参数页面.UiTextBox相邻帧质量变化限制.Text = a.视频参数_质量控制_qpstep
        Form1.常规流程参数页面.UiTextBox关键帧质量.Text = a.视频参数_质量控制_qp_i
        Form1.常规流程参数页面.UiTextBox前向预测帧质量.Text = a.视频参数_质量控制_qp_p
        Form1.常规流程参数页面.UiTextBox双向预测帧质量.Text = a.视频参数_质量控制_qp_b
        Form1.常规流程参数页面.UiTextBox关键帧间隔.Text = a.视频参数_质量控制_关键帧间隔
        Form1.常规流程参数页面.UiTextBox双向预测帧数量.Text = a.视频参数_质量控制_双向预测帧数量

        Form1.常规流程参数页面.UiComboBox像素格式.Text = a.视频参数_色彩管理_像素格式
        Form1.常规流程参数页面.UiComboBox矩阵系数.Text = a.视频参数_色彩管理_矩阵系数
        Form1.常规流程参数页面.UiComboBox色域.Text = a.视频参数_色彩管理_色域
        Form1.常规流程参数页面.UiComboBox传输特性.Text = a.视频参数_色彩管理_传输特性
        Select Case a.视频参数_色彩管理_范围
            Case "pc" : Form1.常规流程参数页面.UiComboBox色彩范围.SelectedIndex = 1
            Case "tv" : Form1.常规流程参数页面.UiComboBox色彩范围.SelectedIndex = 2
        End Select
        Form1.常规流程参数页面.UiComboBox色彩管理处理方式.SelectedIndex = a.视频参数_色彩管理_处理方式

        Select Case a.视频参数_降噪_方式
            Case "hqdn3d" : Form1.常规流程参数页面.UiComboBox降噪方式.SelectedIndex = 1
            Case "nlmeans" : Form1.常规流程参数页面.UiComboBox降噪方式.SelectedIndex = 2
            Case "atadenoise" : Form1.常规流程参数页面.UiComboBox降噪方式.SelectedIndex = 3
            Case "bm3d" : Form1.常规流程参数页面.UiComboBox降噪方式.SelectedIndex = 4
            Case "avs" : Form1.常规流程参数页面.UiComboBox降噪方式.SelectedIndex = 5
            Case Else : Form1.常规流程参数页面.UiComboBox降噪方式.Text = ""
        End Select
        Form1.常规流程参数页面.UiTextBox降噪参数1.Text = a.视频参数_降噪_参数1
        Form1.常规流程参数页面.UiTextBox降噪参数2.Text = a.视频参数_降噪_参数2
        Form1.常规流程参数页面.UiTextBox降噪参数3.Text = a.视频参数_降噪_参数3
        Form1.常规流程参数页面.UiTextBox降噪参数4.Text = a.视频参数_降噪_参数4

        Form1.常规流程参数页面.UiTextBox锐化水平尺寸.Text = a.视频参数_锐化_水平尺寸
        Form1.常规流程参数页面.UiTextBox锐化垂直尺寸.Text = a.视频参数_锐化_垂直尺寸
        Form1.常规流程参数页面.UiTextBox锐化强度.Text = a.视频参数_锐化_锐化强度

        Form1.常规流程参数页面.UiComboBox逐行与隔行处理方式.SelectedIndex = a.视频参数_逐行与隔行_操作

        Form1.常规流程参数页面.UiComboBox角度翻转.SelectedIndex = a.视频参数_画面翻转_角度翻转
        Form1.常规流程参数页面.UiComboBox镜像翻转.SelectedIndex = a.视频参数_画面翻转_镜像翻转

        Select Case a.音频参数_编码器_具体编码
            Case "copy" : Form1.常规流程参数页面.UiComboBox音频编码器.SelectedIndex = 1
            Case "aac" : Form1.常规流程参数页面.UiComboBox音频编码器.SelectedIndex = 2
            Case "libmp3lame" : Form1.常规流程参数页面.UiComboBox音频编码器.SelectedIndex = 3
            Case "flac" : Form1.常规流程参数页面.UiComboBox音频编码器.SelectedIndex = 4
            Case "alac" : Form1.常规流程参数页面.UiComboBox音频编码器.SelectedIndex = 5
            Case "pcm_s16le" : Form1.常规流程参数页面.UiComboBox音频编码器.SelectedIndex = 6
            Case "pcm_s24le" : Form1.常规流程参数页面.UiComboBox音频编码器.SelectedIndex = 7
            Case "pcm_f32le" : Form1.常规流程参数页面.UiComboBox音频编码器.SelectedIndex = 8
            Case "ac3" : Form1.常规流程参数页面.UiComboBox音频编码器.SelectedIndex = 9
            Case "dca" : Form1.常规流程参数页面.UiComboBox音频编码器.SelectedIndex = 10
            Case "libopus" : Form1.常规流程参数页面.UiComboBox音频编码器.SelectedIndex = 11
            Case "libvorbis" : Form1.常规流程参数页面.UiComboBox音频编码器.SelectedIndex = 12
            Case "tta" : Form1.常规流程参数页面.UiComboBox音频编码器.SelectedIndex = 13
            Case "-an" : Form1.常规流程参数页面.UiComboBox音频编码器.SelectedIndex = 14
            Case "libopencore_amrnb" : Form1.常规流程参数页面.UiComboBox音频编码器.SelectedIndex = 15
            Case "libvo_amrwbenc" : Form1.常规流程参数页面.UiComboBox音频编码器.SelectedIndex = 16
            Case Else : Form1.常规流程参数页面.UiComboBox音频编码器.Text = ""
        End Select

        Select Case a.音频参数_比特率_控制方式
            Case "CBR" : Form1.常规流程参数页面.UiComboBox音频比特率控制方式.SelectedIndex = 1
            Case "VBR" : Form1.常规流程参数页面.UiComboBox音频比特率控制方式.SelectedIndex = 2
            Case "FLAC" : Form1.常规流程参数页面.UiComboBox音频比特率控制方式.SelectedIndex = 3
        End Select
        Form1.常规流程参数页面.UiTextBox音频基础比特率.Text = a.音频参数_比特率_基础
        Form1.常规流程参数页面.UiTextBox音频比特率质量值.Text = a.音频参数_比特率_质量值
        Form1.常规流程参数页面.UiComboBox声道布局.Text = a.音频参数_声道数
        Form1.常规流程参数页面.UiComboBox采样率.Text = a.音频参数_采样率
        Form1.常规流程参数页面.UiTextBox响度标准化目标响度.Text = a.音频参数_响度标准化_目标响度
        Form1.常规流程参数页面.UiTextBox响度标准化动态范围.Text = a.音频参数_响度标准化_动态范围
        Form1.常规流程参数页面.UiTextBox响度标准化峰值电平.Text = a.音频参数_响度标准化_峰值电平

        Form1.常规流程参数页面.UiComboBox图片编码器.Text = a.图片参数_编码器_编码名称
        Form1.常规流程参数页面.UiTextBox图片编码器质量.Text = a.图片参数_编码器_参数
        Form1.常规流程参数页面.UiTextBox图片简易调色亮度.Text = a.图片参数_简易调色_亮度
        Form1.常规流程参数页面.UiTextBox图片简易调色对比度.Text = a.图片参数_简易调色_对比度
        Form1.常规流程参数页面.UiTextBox图片简易调色饱和度.Text = a.图片参数_简易调色_饱和度
        Form1.常规流程参数页面.UiTextBox图片简易调色伽马.Text = a.图片参数_简易调色_伽马

        Form1.常规流程参数页面.UiTextBox自定义视频滤镜.Text = a.自定义参数_视频滤镜
        Form1.常规流程参数页面.UiTextBox自定义视频参数.Text = a.自定义参数_视频参数
        Form1.常规流程参数页面.UiTextBox自定义音频滤镜.Text = a.自定义参数_音频滤镜
        Form1.常规流程参数页面.UiTextBox自定义音频参数.Text = a.自定义参数_音频参数
        Form1.常规流程参数页面.UiTextBox开头参数.Text = a.自定义参数_开头参数
        Form1.常规流程参数页面.UiTextBox之前参数.Text = a.自定义参数_之前参数
        Form1.常规流程参数页面.UiTextBox之后参数.Text = a.自定义参数_之后参数
        Form1.常规流程参数页面.UiTextBox最后参数.Text = a.自定义参数_最后参数

        Form1.常规流程参数页面.UiTextBox将视频参数用于这些流.Text = String.Join(",", a.流控制_将视频参数应用于指定流)
        Form1.常规流程参数页面.UiCheckBox保留其他视频流.Checked = a.流控制_启用保留其他视频流
        Form1.常规流程参数页面.UiTextBox将音频参数用于这些流.Text = String.Join(",", a.流控制_将音频参数应用于指定流)
        Form1.常规流程参数页面.UiCheckBox保留其他音频流.Checked = a.流控制_启用保留其他音频流
        Form1.常规流程参数页面.UiCheckBox保留内嵌字幕流.Checked = a.流控制_启用保留内嵌字幕流
        Form1.常规流程参数页面.UiComboBox元数据选项.SelectedIndex = a.流控制_元数据选项
        Form1.常规流程参数页面.UiComboBox章节选项.SelectedIndex = a.流控制_章节选项
        Form1.常规流程参数页面.UiComboBox附件选项.SelectedIndex = a.流控制_附件选项
        Form1.常规流程参数页面.UiComboBox剪辑方法.SelectedIndex = a.流控制_剪辑_方法
        Form1.常规流程参数页面.UiTextBox快速剪辑入点.Text = a.流控制_剪辑_入点
        Form1.常规流程参数页面.UiTextBox快速剪辑出点.Text = a.流控制_剪辑_出点
        Form1.常规流程参数页面.UiCheckBox自动混流同名字幕文件.Checked = a.流控制_启用自动混流同名字幕文件
        Form1.常规流程参数页面.UiTextBoxfilter_complex.Text = a.流控制_filter_complex

    End Sub

    Shared ReadOnly separator As String() = {","}

    Public Shared Sub 储存预设(ByRef a As 预设数据类型)
        a.输出容器 = Form1.UiTextBox输出容器.Text

        a.解码参数_解码器 = Form1.常规流程参数页面.UiComboBox解码器.Text
        a.解码参数_threads = Form1.常规流程参数页面.UiTextBoxCPU解码线程数.Text
        a.解码参数_解码数据格式 = Form1.常规流程参数页面.UiComboBox解码数据格式.Text
        a.解码参数_hwaccel_device = Form1.常规流程参数页面.UiTextBoxhwaccel_device.Text
        a.解码参数_init_hw_device = Form1.常规流程参数页面.UiTextBoxinit_hw_device.Text
        a.解码参数_qsv_device = Form1.常规流程参数页面.UiTextBoxqsv_device.Text

        a.输出命名_自动命名选项 = Form1.常规流程参数页面.UiComboBox自动命名选项.SelectedIndex
        a.输出命名_开头文本 = Form1.常规流程参数页面.UiTextBox开头文本.Text
        a.输出命名_替代文本 = Form1.常规流程参数页面.UiTextBox替代文本.Text
        a.输出命名_结尾文本 = Form1.常规流程参数页面.UiTextBox结尾文本.Text
        a.输出命名_不使用输出文件参数 = Form1.常规流程参数页面.UiCheckBox不使用输出文件参数.Checked

        a.视频参数_编码器_类别 = Form1.常规流程参数页面.UiComboBox编码类别.Text
        a.视频参数_编码器_具体编码 = Form1.常规流程参数页面.UiComboBox具体编码.Text
        a.视频参数_编码器_质量 = Form1.常规流程参数页面.UiComboBox编码预设.Text
        a.视频参数_编码器_配置文件 = Form1.常规流程参数页面.UiComboBox配置文件.Text
        a.视频参数_编码器_场景优化 = Form1.常规流程参数页面.UiComboBox场景优化.Text
        a.视频参数_编码器_gpu = Form1.常规流程参数页面.UiTextBoxgpu.Text
        a.视频参数_编码器_threads = Form1.常规流程参数页面.UiTextBoxthreads.Text

        a.视频参数_分辨率 = Form1.常规流程参数页面.UiComboBox分辨率.Text
        a.视频参数_分辨率自动计算_宽度 = Form1.常规流程参数页面.UiTextBox分辨率自动计算宽度.Text
        a.视频参数_分辨率自动计算_高度 = Form1.常规流程参数页面.UiTextBox分辨率自动计算高度.Text
        a.视频参数_分辨率_裁剪滤镜参数 = Form1.常规流程参数页面.UiTextBox画面裁剪滤镜参数.Text
        a.视频参数_帧速率 = Form1.常规流程参数页面.UiComboBox帧速率.Text
        a.视频参数_帧速率_抽帧最大变化比例 = Form1.常规流程参数页面.UiTextBox抽帧最大变化比例.Text

        a.视频参数_插帧_目标帧率 = Form插帧.UiTextBox要补到多少帧.Text
        Select Case Form插帧.UiComboBox插帧模式.SelectedIndex
            Case 1 : a.视频参数_插帧_插帧模式 = "dup"
            Case 2 : a.视频参数_插帧_插帧模式 = "blend"
            Case 3 : a.视频参数_插帧_插帧模式 = "mci"
        End Select
        Select Case Form插帧.UiComboBox运动估计模式.SelectedIndex
            Case 1 : a.视频参数_插帧_运动估计模式 = "bidir"
            Case 2 : a.视频参数_插帧_运动估计模式 = "bilat"
        End Select
        Select Case Form插帧.UiComboBox运动估计算法.SelectedIndex
            Case 1 : a.视频参数_插帧_运动估计算法 = "esa"
            Case 2 : a.视频参数_插帧_运动估计算法 = "tss"
            Case 3 : a.视频参数_插帧_运动估计算法 = "tdls"
            Case 4 : a.视频参数_插帧_运动估计算法 = "ntss"
            Case 5 : a.视频参数_插帧_运动估计算法 = "fss"
            Case 6 : a.视频参数_插帧_运动估计算法 = "ds"
            Case 7 : a.视频参数_插帧_运动估计算法 = "hexbs"
            Case 8 : a.视频参数_插帧_运动估计算法 = "epzs"
            Case 9 : a.视频参数_插帧_运动估计算法 = "umh"
        End Select
        Select Case Form插帧.UiComboBox运动补偿模式.SelectedIndex
            Case 1 : a.视频参数_插帧_运动补偿模式 = "obmc"
            Case 2 : a.视频参数_插帧_运动补偿模式 = "aobmc"
        End Select
        a.视频参数_插帧_可变块大小的运动补偿 = Form插帧.UiCheckBox可变块大小的运动补偿.Checked
        a.视频参数_插帧_块大小 = Form插帧.UiTextBox块大小.Text
        a.视频参数_插帧_搜索范围 = Form插帧.UiTextBox搜索范围.Text
        a.视频参数_插帧_场景变化检测强度 = Form插帧.UiTextBox场景变化检测强度.Text

        a.视频参数_帧混合_指定帧率 = Form帧混合.UiTextBox降低帧率.Text
        Select Case Form帧混合.UiComboBox混合算法.SelectedIndex
            Case 1 : a.视频参数_帧混合_混合模式 = "average"
            Case 2 : a.视频参数_帧混合_混合模式 = "difference"
            Case 3 : a.视频参数_帧混合_混合模式 = "and"
            Case 4 : a.视频参数_帧混合_混合模式 = "or"
            Case 5 : a.视频参数_帧混合_混合模式 = "xor"
            Case 6 : a.视频参数_帧混合_混合模式 = "add"
            Case 7 : a.视频参数_帧混合_混合模式 = "multiply"
        End Select
        a.视频参数_帧混合_混合比例 = Form帧混合.UiTextBox混合比例.Text

        Select Case Form1.常规流程参数页面.UiComboBox比特率控制方式.SelectedIndex
            Case 1 : a.视频参数_比特率_控制方式 = "VBR"
            Case 2 : a.视频参数_比特率_控制方式 = "VBR HQ"
            Case 3 : a.视频参数_比特率_控制方式 = "CRF"
            Case 4 : a.视频参数_比特率_控制方式 = "CQP"
            Case 5 : a.视频参数_比特率_控制方式 = "CBR"
        End Select
        a.视频参数_比特率_基础 = Form1.常规流程参数页面.UiTextBox基础比特率.Text
        a.视频参数_比特率_最低值 = Form1.常规流程参数页面.UiTextBox最低比特率.Text
        a.视频参数_比特率_最高值 = Form1.常规流程参数页面.UiTextBox最高比特率.Text
        a.视频参数_比特率_缓冲区 = Form1.常规流程参数页面.UiTextBox比特率缓冲区.Text

        Select Case Form1.常规流程参数页面.UiComboBox全局质量控制参数.SelectedIndex
            Case 1 : a.视频参数_质量控制_参数 = "cq"
            Case 2 : a.视频参数_质量控制_参数 = "qp"
            Case 3 : a.视频参数_质量控制_参数 = "crf"
        End Select
        a.视频参数_质量控制_值 = Form1.常规流程参数页面.UiTextBox全局质量控制值.Text
        a.视频参数_质量控制_前瞻分析帧数 = Form1.常规流程参数页面.UiTextBox硬件加速HQ前瞻分析帧数.Text
        a.视频参数_质量控制_qpmin = Form1.常规流程参数页面.UiTextBox质量最小值.Text
        a.视频参数_质量控制_qpmax = Form1.常规流程参数页面.UiTextBox质量最大值.Text
        a.视频参数_质量控制_qpstep = Form1.常规流程参数页面.UiTextBox相邻帧质量变化限制.Text
        a.视频参数_质量控制_qp_i = Form1.常规流程参数页面.UiTextBox关键帧质量.Text
        a.视频参数_质量控制_qp_p = Form1.常规流程参数页面.UiTextBox前向预测帧质量.Text
        a.视频参数_质量控制_qp_b = Form1.常规流程参数页面.UiTextBox双向预测帧质量.Text
        a.视频参数_质量控制_关键帧间隔 = Form1.常规流程参数页面.UiTextBox关键帧间隔.Text
        a.视频参数_质量控制_双向预测帧数量 = Form1.常规流程参数页面.UiTextBox双向预测帧数量.Text

        a.视频参数_色彩管理_像素格式 = Form1.常规流程参数页面.UiComboBox像素格式.Text
        a.视频参数_色彩管理_矩阵系数 = Form1.常规流程参数页面.UiComboBox矩阵系数.Text
        a.视频参数_色彩管理_色域 = Form1.常规流程参数页面.UiComboBox色域.Text
        a.视频参数_色彩管理_传输特性 = Form1.常规流程参数页面.UiComboBox传输特性.Text
        Select Case Form1.常规流程参数页面.UiComboBox色彩范围.SelectedIndex
            Case 1 : a.视频参数_色彩管理_范围 = "pc"
            Case 2 : a.视频参数_色彩管理_范围 = "tv"
        End Select
        a.视频参数_色彩管理_处理方式 = Form1.常规流程参数页面.UiComboBox色彩管理处理方式.SelectedIndex

        Select Case Form1.常规流程参数页面.UiComboBox降噪方式.SelectedIndex
            Case 1 : a.视频参数_降噪_方式 = "hqdn3d"
            Case 2 : a.视频参数_降噪_方式 = "nlmeans"
            Case 3 : a.视频参数_降噪_方式 = "atadenoise"
            Case 4 : a.视频参数_降噪_方式 = "bm3d"
            Case 5 : a.视频参数_降噪_方式 = "avs"
        End Select
        a.视频参数_降噪_参数1 = Form1.常规流程参数页面.UiTextBox降噪参数1.Text
        a.视频参数_降噪_参数2 = Form1.常规流程参数页面.UiTextBox降噪参数2.Text
        a.视频参数_降噪_参数3 = Form1.常规流程参数页面.UiTextBox降噪参数3.Text
        a.视频参数_降噪_参数4 = Form1.常规流程参数页面.UiTextBox降噪参数4.Text

        a.视频参数_锐化_水平尺寸 = Form1.常规流程参数页面.UiTextBox锐化水平尺寸.Text
        a.视频参数_锐化_垂直尺寸 = Form1.常规流程参数页面.UiTextBox锐化垂直尺寸.Text
        a.视频参数_锐化_锐化强度 = Form1.常规流程参数页面.UiTextBox锐化强度.Text

        a.视频参数_逐行与隔行_操作 = Form1.常规流程参数页面.UiComboBox逐行与隔行处理方式.SelectedIndex

        a.视频参数_画面翻转_角度翻转 = Form1.常规流程参数页面.UiComboBox角度翻转.SelectedIndex
        a.视频参数_画面翻转_镜像翻转 = Form1.常规流程参数页面.UiComboBox镜像翻转.SelectedIndex

        a.音频参数_编码器_具体编码 = Form1.常规流程参数页面.UiComboBox音频编码器.Text
        Select Case Form1.常规流程参数页面.UiComboBox音频编码器.SelectedIndex
            Case 1 : a.音频参数_编码器_具体编码 = "copy"
            Case 2 : a.音频参数_编码器_具体编码 = "aac"
            Case 3 : a.音频参数_编码器_具体编码 = "libmp3lame"
            Case 4 : a.音频参数_编码器_具体编码 = "flac"
            Case 5 : a.音频参数_编码器_具体编码 = "alac"
            Case 6 : a.音频参数_编码器_具体编码 = "pcm_s16le"
            Case 7 : a.音频参数_编码器_具体编码 = "pcm_s24le"
            Case 8 : a.音频参数_编码器_具体编码 = "pcm_f32le"
            Case 9 : a.音频参数_编码器_具体编码 = "ac3"
            Case 10 : a.音频参数_编码器_具体编码 = "dca"
            Case 11 : a.音频参数_编码器_具体编码 = "libopus"
            Case 12 : a.音频参数_编码器_具体编码 = "libvorbis"
            Case 13 : a.音频参数_编码器_具体编码 = "tta"
            Case 14 : a.音频参数_编码器_具体编码 = "libopencore_amrnb"
            Case 15 : a.音频参数_编码器_具体编码 = "libvo_amrwbenc"
            Case 16 : a.音频参数_编码器_具体编码 = "-an"
            Case Else : a.音频参数_编码器_具体编码 = ""
        End Select


        Select Case Form1.常规流程参数页面.UiComboBox音频比特率控制方式.SelectedIndex
            Case 1 : a.音频参数_比特率_控制方式 = "CBR"
            Case 2 : a.音频参数_比特率_控制方式 = "VBR"
            Case 3 : a.音频参数_比特率_控制方式 = "FLAC"
        End Select
        a.音频参数_比特率_基础 = Form1.常规流程参数页面.UiTextBox音频基础比特率.Text
        a.音频参数_比特率_质量值 = Form1.常规流程参数页面.UiTextBox音频比特率质量值.Text
        a.音频参数_声道数 = Form1.常规流程参数页面.UiComboBox声道布局.Text
        a.音频参数_采样率 = Form1.常规流程参数页面.UiComboBox采样率.Text
        a.音频参数_响度标准化_目标响度 = Form1.常规流程参数页面.UiTextBox响度标准化目标响度.Text
        a.音频参数_响度标准化_动态范围 = Form1.常规流程参数页面.UiTextBox响度标准化动态范围.Text
        a.音频参数_响度标准化_峰值电平 = Form1.常规流程参数页面.UiTextBox响度标准化峰值电平.Text

        a.图片参数_编码器_编码名称 = Form1.常规流程参数页面.UiComboBox图片编码器.Text
        a.图片参数_编码器_参数 = Form1.常规流程参数页面.UiTextBox图片编码器质量.Text
        a.图片参数_简易调色_亮度 = Form1.常规流程参数页面.UiTextBox图片简易调色亮度.Text
        a.图片参数_简易调色_对比度 = Form1.常规流程参数页面.UiTextBox图片简易调色对比度.Text
        a.图片参数_简易调色_饱和度 = Form1.常规流程参数页面.UiTextBox图片简易调色饱和度.Text
        a.图片参数_简易调色_伽马 = Form1.常规流程参数页面.UiTextBox图片简易调色伽马.Text

        a.自定义参数_视频滤镜 = Form1.常规流程参数页面.UiTextBox自定义视频滤镜.Text
        a.自定义参数_视频参数 = Form1.常规流程参数页面.UiTextBox自定义视频参数.Text
        a.自定义参数_音频滤镜 = Form1.常规流程参数页面.UiTextBox自定义音频滤镜.Text
        a.自定义参数_音频参数 = Form1.常规流程参数页面.UiTextBox自定义音频参数.Text
        a.自定义参数_开头参数 = Form1.常规流程参数页面.UiTextBox开头参数.Text
        a.自定义参数_之前参数 = Form1.常规流程参数页面.UiTextBox之前参数.Text
        a.自定义参数_之后参数 = Form1.常规流程参数页面.UiTextBox之后参数.Text
        a.自定义参数_最后参数 = Form1.常规流程参数页面.UiTextBox最后参数.Text

        a.流控制_将视频参数应用于指定流 = Form1.常规流程参数页面.UiTextBox将视频参数用于这些流.Text.Replace("-", "").Split(separator, StringSplitOptions.RemoveEmptyEntries)
        a.流控制_启用保留其他视频流 = Form1.常规流程参数页面.UiCheckBox保留其他视频流.Checked
        a.流控制_将音频参数应用于指定流 = Form1.常规流程参数页面.UiTextBox将音频参数用于这些流.Text.Replace("-", "").Split(separator, StringSplitOptions.RemoveEmptyEntries)
        a.流控制_启用保留其他音频流 = Form1.常规流程参数页面.UiCheckBox保留其他音频流.Checked
        a.流控制_启用保留内嵌字幕流 = Form1.常规流程参数页面.UiCheckBox保留内嵌字幕流.Checked
        a.流控制_元数据选项 = Form1.常规流程参数页面.UiComboBox元数据选项.SelectedIndex
        a.流控制_章节选项 = Form1.常规流程参数页面.UiComboBox章节选项.SelectedIndex
        a.流控制_附件选项 = Form1.常规流程参数页面.UiComboBox附件选项.SelectedIndex
        a.流控制_剪辑_方法 = Form1.常规流程参数页面.UiComboBox剪辑方法.SelectedIndex
        a.流控制_剪辑_入点 = Form1.常规流程参数页面.UiTextBox快速剪辑入点.Text
        a.流控制_剪辑_出点 = Form1.常规流程参数页面.UiTextBox快速剪辑出点.Text
        a.流控制_启用自动混流同名字幕文件 = Form1.常规流程参数页面.UiCheckBox自动混流同名字幕文件.Checked
        a.流控制_filter_complex = Form1.常规流程参数页面.UiTextBoxfilter_complex.Text
    End Sub

    Public Shared Sub 保存预设到文件()
        Dim d As New SaveFileDialog With {.Filter = "Json|*.json"}
        d.ShowDialog(Form1)
        If d.FileName = "" Then Exit Sub
        Dim a As New 预设数据类型
        储存预设(a)
        If Form1.常规流程参数页面.UiCheckBox额外保存信息.Checked Then
            a.计算机名称 = Environment.MachineName
            a.输出位置 = Form1.UiComboBox输出目录.Text
        End If
        File.WriteAllText(d.FileName, JsonSerializer.Serialize(a, JsonSO))
        Select Case 用户设置.实例对象.自动加载预设选项
            Case 用户设置.自动加载预设选项枚举.自动加载最后的预设文件
                用户设置.实例对象.自动加载预设文件路径 = d.FileName
                Form1.常规流程参数页面.UiTextBox自动加载的预设文件路径.Text = d.FileName
        End Select
    End Sub

    Public Shared Sub 从文件读取预设()
        Dim d As New OpenFileDialog With {.Filter = "Json|*.json"}
        d.ShowDialog(Form1)
        If Not File.Exists(d.FileName) Then Exit Sub
        Dim a As 预设数据类型 = JsonSerializer.Deserialize(Of 预设数据类型)(File.ReadAllText(d.FileName))
        显示预设(a)
        If a.计算机名称 = Environment.MachineName Then
            Form1.UiComboBox输出目录.Text = a.输出位置
        End If
        Form1.常规流程参数页面.RichTextBox1.Text = "ffmpeg " & 预设管理.将预设数据转换为命令行(a, "输入目录\输入文件.后缀", "输出目录\输出文件.后缀")
        Select Case 用户设置.实例对象.自动加载预设选项
            Case 用户设置.自动加载预设选项枚举.自动加载最后的预设文件
                用户设置.实例对象.自动加载预设文件路径 = d.FileName
                Form1.常规流程参数页面.UiTextBox自动加载的预设文件路径.Text = d.FileName
        End Select
    End Sub

    Public Shared Sub 重置全部包含在预设中的设置()
        Form1.UiTextBox输出容器.Text = ""

        ' 解码参数
        Form1.常规流程参数页面.UiComboBox解码器.Text = ""
        Form1.常规流程参数页面.UiTextBoxCPU解码线程数.Text = ""
        Form1.常规流程参数页面.UiComboBox解码数据格式.Text = ""
        Form1.常规流程参数页面.UiTextBoxhwaccel_device.Text = ""
        Form1.常规流程参数页面.UiTextBoxinit_hw_device.Text = ""
        Form1.常规流程参数页面.UiTextBoxqsv_device.Text = ""

        ' 输出命名
        Form1.常规流程参数页面.UiComboBox自动命名选项.SelectedIndex = 0
        Form1.常规流程参数页面.UiTextBox开头文本.Text = ""
        Form1.常规流程参数页面.UiTextBox替代文本.Text = ""
        Form1.常规流程参数页面.UiTextBox结尾文本.Text = ""
        Form1.常规流程参数页面.UiCheckBox不使用输出文件参数.Checked = False

        ' 视频参数
        Form1.常规流程参数页面.UiComboBox编码类别.Text = ""
        Form1.常规流程参数页面.UiComboBox具体编码.Text = ""
        Form1.常规流程参数页面.UiTextBoxgpu.Text = ""
        Form1.常规流程参数页面.UiTextBoxthreads.Text = ""
        Form1.常规流程参数页面.UiComboBox分辨率.Text = ""
        Form1.常规流程参数页面.UiTextBox分辨率自动计算宽度.Text = ""
        Form1.常规流程参数页面.UiTextBox分辨率自动计算高度.Text = ""
        Form1.常规流程参数页面.UiTextBox画面裁剪滤镜参数.Text = ""
        Form1.常规流程参数页面.UiComboBox帧速率.Text = ""
        Form1.常规流程参数页面.UiTextBox抽帧最大变化比例.Text = ""
        Form插帧.重置所有选项()
        Form帧混合.重置所有选项()
        Form1.常规流程参数页面.UiComboBox比特率控制方式.SelectedIndex = -1
        Form1.常规流程参数页面.UiTextBox硬件加速HQ前瞻分析帧数.Text = ""
        Form1.常规流程参数页面.UiTextBox基础比特率.Text = ""
        Form1.常规流程参数页面.UiTextBox最低比特率.Text = ""
        Form1.常规流程参数页面.UiTextBox最高比特率.Text = ""
        Form1.常规流程参数页面.UiTextBox比特率缓冲区.Text = ""
        Form1.常规流程参数页面.UiComboBox全局质量控制参数.SelectedIndex = -1
        Form1.常规流程参数页面.UiTextBox全局质量控制值.Text = ""
        Form1.常规流程参数页面.UiTextBox质量最小值.Text = ""
        Form1.常规流程参数页面.UiTextBox质量最大值.Text = ""
        Form1.常规流程参数页面.UiTextBox相邻帧质量变化限制.Text = ""
        Form1.常规流程参数页面.UiTextBox关键帧质量.Text = ""
        Form1.常规流程参数页面.UiTextBox前向预测帧质量.Text = ""
        Form1.常规流程参数页面.UiTextBox双向预测帧质量.Text = ""
        Form1.常规流程参数页面.UiTextBox关键帧间隔.Text = ""
        Form1.常规流程参数页面.UiTextBox双向预测帧数量.Text = ""
        Form1.常规流程参数页面.UiComboBox配置文件.Text = ""
        Form1.常规流程参数页面.UiComboBox场景优化.Text = ""
        Form1.常规流程参数页面.UiComboBox像素格式.Text = ""
        Form1.常规流程参数页面.UiComboBox矩阵系数.Text = ""
        Form1.常规流程参数页面.UiComboBox色域.Text = ""
        Form1.常规流程参数页面.UiComboBox传输特性.Text = ""
        Form1.常规流程参数页面.UiComboBox色彩范围.SelectedIndex = -1
        Form1.常规流程参数页面.UiComboBox色彩管理处理方式.SelectedIndex = -1
        Form1.常规流程参数页面.UiComboBox降噪方式.SelectedIndex = -1
        Form1.常规流程参数页面.UiTextBox降噪参数1.Text = ""
        Form1.常规流程参数页面.UiTextBox降噪参数2.Text = ""
        Form1.常规流程参数页面.UiTextBox降噪参数3.Text = ""
        Form1.常规流程参数页面.UiTextBox降噪参数4.Text = ""
        Form1.常规流程参数页面.UiTextBox锐化水平尺寸.Text = ""
        Form1.常规流程参数页面.UiTextBox锐化垂直尺寸.Text = ""
        Form1.常规流程参数页面.UiTextBox锐化强度.Text = ""
        Form1.常规流程参数页面.UiComboBox逐行与隔行处理方式.SelectedIndex = -1
        Form1.常规流程参数页面.UiComboBox角度翻转.SelectedIndex = -1
        Form1.常规流程参数页面.UiComboBox镜像翻转.SelectedIndex = -1

        ' 音频参数
        Form1.常规流程参数页面.UiComboBox音频编码器.Text = ""
        Form1.常规流程参数页面.UiComboBox音频比特率控制方式.SelectedIndex = -1
        Form1.常规流程参数页面.UiTextBox音频基础比特率.Text = ""
        Form1.常规流程参数页面.UiTextBox音频比特率质量值.Text = ""
        Form1.常规流程参数页面.UiComboBox声道布局.Text = ""
        Form1.常规流程参数页面.UiComboBox采样率.Text = ""
        Form1.常规流程参数页面.UiTextBox响度标准化目标响度.Text = ""
        Form1.常规流程参数页面.UiTextBox响度标准化动态范围.Text = ""
        Form1.常规流程参数页面.UiTextBox响度标准化峰值电平.Text = ""
        Form1.常规流程参数页面.UiTextBox自定义音频滤镜.Text = ""
        Form1.常规流程参数页面.UiTextBox自定义音频参数.Text = ""

        ' 图片参数
        Form1.常规流程参数页面.UiComboBox图片编码器.Text = ""
        Form1.常规流程参数页面.UiTextBox图片编码器质量.Text = ""
        Form1.常规流程参数页面.UiTextBox图片简易调色亮度.Text = ""
        Form1.常规流程参数页面.UiTextBox图片简易调色对比度.Text = ""
        Form1.常规流程参数页面.UiTextBox图片简易调色饱和度.Text = ""
        Form1.常规流程参数页面.UiTextBox图片简易调色伽马.Text = ""

        ' 自定义参数
        Form1.常规流程参数页面.UiTextBox自定义视频滤镜.Text = ""
        Form1.常规流程参数页面.UiTextBox自定义视频参数.Text = ""
        Form1.常规流程参数页面.UiTextBox自定义音频滤镜.Text = ""
        Form1.常规流程参数页面.UiTextBox自定义音频参数.Text = ""
        Form1.常规流程参数页面.UiTextBox开头参数.Text = ""
        Form1.常规流程参数页面.UiTextBox之前参数.Text = ""
        Form1.常规流程参数页面.UiTextBox之后参数.Text = ""
        Form1.常规流程参数页面.UiTextBox最后参数.Text = ""

        ' 流控制
        Form1.常规流程参数页面.UiTextBox将视频参数用于这些流.Text = ""
        Form1.常规流程参数页面.UiCheckBox保留其他视频流.Checked = False
        Form1.常规流程参数页面.UiTextBox将音频参数用于这些流.Text = ""
        Form1.常规流程参数页面.UiCheckBox保留其他音频流.Checked = False
        Form1.常规流程参数页面.UiCheckBox保留内嵌字幕流.Checked = False
        Form1.常规流程参数页面.UiComboBox元数据选项.SelectedIndex = 0
        Form1.常规流程参数页面.UiComboBox章节选项.SelectedIndex = 0
        Form1.常规流程参数页面.UiComboBox附件选项.SelectedIndex = 0
        Form1.常规流程参数页面.UiComboBox剪辑方法.SelectedIndex = 0
        Form1.常规流程参数页面.UiTextBox快速剪辑入点.Text = ""
        Form1.常规流程参数页面.UiTextBox快速剪辑出点.Text = ""
        Form1.常规流程参数页面.UiCheckBox自动混流同名字幕文件.Checked = False
        Form1.常规流程参数页面.UiTextBoxfilter_complex.Text = ""
    End Sub

    Public Shared Function 将预设数据转换为命令行(a As 预设数据类型, 输入文件 As String, 输出文件 As String) As String
        Dim 视频滤镜参数集 As New List(Of String)
        Dim 音频滤镜参数集 As New List(Of String)
        Dim 滤镜图参数集 As New List(Of String)
        Dim 视频参数 As String = ""
        Dim 音频参数 As String = ""

        Dim 输入文件的文件夹 As String = Path.GetDirectoryName(输入文件)

        Dim arg As String = "-hide_banner -nostdin "

        If a.自定义参数_开头参数 <> "" Then arg &= $"{处理自定义参数的通配字符串(a.自定义参数_开头参数, 输入文件)} "

        If a.解码参数_解码器 <> "" Then arg &= $"-hwaccel {a.解码参数_解码器} "
        If a.解码参数_threads <> "" Then arg &= $"-threads {a.解码参数_threads} "
        If a.解码参数_解码数据格式 <> "" Then arg &= $"-hwaccel_output_format {a.解码参数_解码数据格式} "
        If a.解码参数_hwaccel_device <> "" Then arg &= $"-hwaccel_device {a.解码参数_hwaccel_device} "
        If a.解码参数_init_hw_device <> "" Then arg &= $"-init_hw_device {a.解码参数_init_hw_device} "
        If a.解码参数_qsv_device <> "" Then arg &= $"-qsv_device {a.解码参数_qsv_device} "

        If a.流控制_剪辑_方法 = 1 Then
            If a.流控制_剪辑_入点 <> "" Then arg &= $"-ss {a.流控制_剪辑_入点} "
            If a.流控制_剪辑_出点 <> "" Then arg &= $"-to {a.流控制_剪辑_出点} "
        End If

        'avs 文件在启动任务时创建 
        If a.视频参数_降噪_方式 = "avs" Then
            arg &= $"-i ""{Path.Combine(Path.GetDirectoryName(输入文件), Path.GetFileNameWithoutExtension(输入文件) & ".avs")}"" "
        Else
            arg &= $"-i {""""}{输入文件}{""""} "
        End If

        If a.自定义参数_之前参数 <> "" Then arg &= $"{处理自定义参数的通配字符串(a.自定义参数_之前参数, 输入文件)} "

        If a.视频参数_编码器_类别 = "禁用" Then 视频参数 &= $"-vn "
        If a.视频参数_编码器_具体编码 <> "" Then 视频参数 &= $"-c:v {a.视频参数_编码器_具体编码} "
        If a.视频参数_编码器_threads <> "" Then 视频参数 &= $"-threads {a.视频参数_编码器_threads} "
        If a.视频参数_编码器_质量 <> "" Then
            Select Case a.视频参数_编码器_具体编码
                Case "libaom-av1", "libvpx-vp9"
                    视频参数 &= $"-cpu-used {a.视频参数_编码器_质量} "
                Case Else
                    视频参数 &= $"-preset {a.视频参数_编码器_质量} "
            End Select
        End If
        If a.视频参数_编码器_配置文件 <> "" Then 视频参数 &= $"-profile {a.视频参数_编码器_配置文件} "
        If a.视频参数_编码器_场景优化 <> "" Then
            Select Case a.视频参数_编码器_具体编码
                Case "hevc_amf", "h264_amf"
                    视频参数 &= $"-usage {a.视频参数_编码器_场景优化} "
                Case "libvpx-vp9"
                    视频参数 &= $"-deadline {a.视频参数_编码器_场景优化} "
                Case Else
                    视频参数 &= $"-tune {a.视频参数_编码器_场景优化} "
            End Select
        End If
        If a.视频参数_编码器_gpu <> "" Then arg &= $"-gpu {a.视频参数_编码器_gpu} "

        If a.视频参数_分辨率 <> "" Then
            视频参数 &= $"-s {a.视频参数_分辨率} "
        Else
            If a.视频参数_分辨率自动计算_宽度 <> "" Then
                视频滤镜参数集.Add($"scale={a.视频参数_分辨率自动计算_宽度}:-2")
            ElseIf a.视频参数_分辨率自动计算_高度 <> "" Then
                视频滤镜参数集.Add($"scale=-2:{a.视频参数_分辨率自动计算_高度}")
            End If
        End If
        If a.视频参数_分辨率_裁剪滤镜参数 <> "" Then
            视频滤镜参数集.Add($"crop={a.视频参数_分辨率_裁剪滤镜参数}")
        End If

        If a.视频参数_帧速率 <> "" Then 视频参数 &= $"-r {a.视频参数_帧速率} "
        If a.视频参数_帧速率_抽帧最大变化比例 <> "" Then
            视频滤镜参数集.Add($"mpdecimate=max={a.视频参数_帧速率_抽帧最大变化比例}")
            视频参数 &= "-fps_mode vfr "
        End If

        If a.视频参数_插帧_目标帧率 <> "" AndAlso a.视频参数_插帧_插帧模式 <> "" Then
            Dim s1 As String = $"minterpolate=fps={a.视频参数_插帧_目标帧率}:mi_mode={a.视频参数_插帧_插帧模式}"
            If a.视频参数_插帧_插帧模式 = "mci" AndAlso a.视频参数_插帧_运动补偿模式 <> "" Then
                s1 &= $":mc_mode={a.视频参数_插帧_运动补偿模式}"
            End If
            If a.视频参数_插帧_运动估计模式 <> "" Then s1 &= $":me_mode={a.视频参数_插帧_运动估计模式}"
            If a.视频参数_插帧_运动估计算法 <> "" Then s1 &= $":me={a.视频参数_插帧_运动估计算法}"
            If a.视频参数_插帧_可变块大小的运动补偿 Then s1 &= $":vsbmc=1"
            If a.视频参数_插帧_块大小 <> "" Then s1 &= $":mb_size={a.视频参数_插帧_块大小}"
            If a.视频参数_插帧_搜索范围 <> "" Then s1 &= $":search_param={a.视频参数_插帧_搜索范围}"
            If a.视频参数_插帧_场景变化检测强度 <> "" Then s1 &= $":scd=fdiff:scd_threshold={a.视频参数_插帧_场景变化检测强度}"
            视频滤镜参数集.Add(s1)
        End If

        If a.视频参数_帧混合_混合模式 <> "" Then
            Dim s1 As String = $"tblend=all_mode={a.视频参数_帧混合_混合模式}"
            If a.视频参数_帧混合_指定帧率 <> "" Then s1 = $"fps={a.视频参数_帧混合_指定帧率}," & s1
            If a.视频参数_帧混合_混合比例 <> "" Then s1 &= $":all_opacity={a.视频参数_帧混合_混合比例}"
            视频滤镜参数集.Add(s1)
        End If

        Select Case a.视频参数_比特率_控制方式
            Case "VBR"
                Select Case a.视频参数_编码器_具体编码
                    Case "av1_amf", "hevc_amf", "h264_amf"
                        视频参数 &= $"-rc qvbr "
                    Case Else
                        视频参数 &= $"-rc vbr "
                End Select

            Case "VBR HQ"
                Select Case a.视频参数_编码器_具体编码
                    Case "av1_nvenc", "hevc_nvenc", "h264_nvenc"
                        视频参数 &= $"-rc vbr_hq "
                    Case "av1_amf"
                        视频参数 &= $"-rc hqvbr -quality high_quality "
                    Case "hevc_amf", "h264_amf"
                        视频参数 &= $"-rc hqvbr -quality quality "
                    Case "av1_qsv", "hevc_qsv", "h264_qsv"
                        视频参数 &= $"-rc la_icq "
                End Select

            Case "CRF"
            Case "CQP"
                Select Case a.视频参数_编码器_具体编码
                    Case "av1_nvenc", "hevc_nvenc", "h264_nvenc"
                        视频参数 &= $"-rc constqp "
                    Case "av1_amf", "hevc_amf", "h264_amf"
                        视频参数 &= $"-rc cqp "
                End Select

            Case "CBR"
                视频参数 &= $"-rc cbr "
        End Select
        If a.视频参数_比特率_基础 <> "" Then 视频参数 &= $"-b:v {a.视频参数_比特率_基础} "
        If a.视频参数_比特率_最低值 <> "" Then 视频参数 &= $"-minrate {a.视频参数_比特率_最低值} "
        If a.视频参数_比特率_最高值 <> "" Then 视频参数 &= $"-maxrate {a.视频参数_比特率_最高值} "
        If a.视频参数_比特率_缓冲区 <> "" Then 视频参数 &= $"-bufsize {a.视频参数_比特率_缓冲区} "

        If a.视频参数_质量控制_值 <> "" Then
            Select Case a.视频参数_质量控制_参数
                Case "cq" : 视频参数 &= $"-cq {a.视频参数_质量控制_值} "
                Case "qp" : 视频参数 &= $"-qp {a.视频参数_质量控制_值} "
                Case "crf" : 视频参数 &= $"-crf {a.视频参数_质量控制_值} "
            End Select
        End If
        If a.视频参数_质量控制_前瞻分析帧数 <> "" Then '不支持 AMD
            Select Case a.视频参数_编码器_具体编码
                Case "av1_nvenc", "hevc_nvenc", "h264_nvenc", "libx264", "libx265"
                    视频参数 &= $"-rc-lookahead {a.视频参数_质量控制_前瞻分析帧数} "
                Case "av1_qsv", "hevc_qsv", "h264_qsv"
                    视频参数 &= $"-extbrc 1 -look_ahead_depth {a.视频参数_质量控制_前瞻分析帧数} "
            End Select
        End If
        If a.视频参数_质量控制_qpmin <> "" Then 视频参数 &= $"-qpmin {a.视频参数_质量控制_qpmin} "
        If a.视频参数_质量控制_qpmax <> "" Then 视频参数 &= $"-qpmax {a.视频参数_质量控制_qpmax} "
        If a.视频参数_质量控制_qpstep <> "" Then 视频参数 &= $"-qpstep {a.视频参数_质量控制_qpstep} "
        If a.视频参数_质量控制_qp_i <> "" Then 视频参数 &= $"-qp_i {a.视频参数_质量控制_qp_i} "
        If a.视频参数_质量控制_qp_p <> "" Then 视频参数 &= $"-qp_p {a.视频参数_质量控制_qp_p} "
        If a.视频参数_质量控制_qp_b <> "" Then 视频参数 &= $"-qp_b {a.视频参数_质量控制_qp_b} "
        If a.视频参数_质量控制_关键帧间隔 <> "" Then 视频参数 &= $"-g {a.视频参数_质量控制_关键帧间隔} "
        If a.视频参数_质量控制_双向预测帧数量 <> "" Then 视频参数 &= $"-bf {a.视频参数_质量控制_双向预测帧数量} "

        If a.视频参数_色彩管理_像素格式 <> "" Then 视频参数 &= $"-pix_fmt {a.视频参数_色彩管理_像素格式} "
        Select Case a.视频参数_色彩管理_处理方式
            Case 1 '写入元数据并转换
                视频参数 &= $"-colorspace {a.视频参数_色彩管理_矩阵系数} "
                视频参数 &= $"-color_primaries {a.视频参数_色彩管理_色域} "
                视频参数 &= $"-color_trc {a.视频参数_色彩管理_传输特性} "
                视频参数 &= $"-color_range {a.视频参数_色彩管理_范围} "
                '视频滤镜参数集.Add($"setparams=colorspace={a.视频参数_色彩管理_矩阵系数}:color_primaries={a.视频参数_色彩管理_色域}:color_trc={a.视频参数_色彩管理_传输特性}:range={a.视频参数_色彩管理_范围}")
                Dim zscale As String = $"zscale=matrix={a.视频参数_色彩管理_矩阵系数}:primaries={a.视频参数_色彩管理_色域}:transfer={a.视频参数_色彩管理_传输特性}"
                Select Case a.视频参数_色彩管理_范围
                    Case "pc" : zscale &= ":range=full"
                    Case "tv" : zscale &= ":range=limited"
                End Select
                If a.视频参数_色彩管理_像素格式 <> "" Then zscale &= $",format={a.视频参数_色彩管理_像素格式}"
                视频滤镜参数集.Add(zscale)
            Case 2 '仅写入元数据
                视频参数 &= $"-colorspace {a.视频参数_色彩管理_矩阵系数} "
                视频参数 &= $"-color_primaries {a.视频参数_色彩管理_色域} "
                视频参数 &= $"-color_trc {a.视频参数_色彩管理_传输特性} "
                视频参数 &= $"-color_range {a.视频参数_色彩管理_范围} "
                    '视频滤镜参数集.Add($"setparams=colorspace={a.视频参数_色彩管理_矩阵系数}:color_primaries={a.视频参数_色彩管理_色域}:color_trc={a.视频参数_色彩管理_传输特性}:range={a.视频参数_色彩管理_范围}")
            Case 3 '仅转换
                Dim zscale As String = $"zscale=matrix={a.视频参数_色彩管理_矩阵系数}:primaries={a.视频参数_色彩管理_色域}:transfer={a.视频参数_色彩管理_传输特性}"
                Select Case a.视频参数_色彩管理_范围
                    Case "pc" : zscale &= ":range=full"
                    Case "tv" : zscale &= ":range=limited"
                End Select
                If a.视频参数_色彩管理_像素格式 <> "" Then zscale &= $",format={a.视频参数_色彩管理_像素格式}"
                视频滤镜参数集.Add(zscale)
        End Select

        Select Case a.视频参数_降噪_方式
            Case "hqdn3d" : 视频滤镜参数集.Add($"hqdn3d=luma_spatial={a.视频参数_降噪_参数1}:chroma_spatial={a.视频参数_降噪_参数2}:luma_tmp={a.视频参数_降噪_参数3}:chroma_tmp={a.视频参数_降噪_参数4}")
            Case "nlmeans" : 视频滤镜参数集.Add($"nlmeans=s={a.视频参数_降噪_参数1}:p={a.视频参数_降噪_参数2}:pc={a.视频参数_降噪_参数3}:r={a.视频参数_降噪_参数4}")
            Case "atadenoise" : 视频滤镜参数集.Add($"atadenoise=0a={a.视频参数_降噪_参数1}:0b={a.视频参数_降噪_参数2}:1a={a.视频参数_降噪_参数3}:1b={a.视频参数_降噪_参数4}")
            Case "bm3d" : 视频滤镜参数集.Add($"bm3d=sigma={a.视频参数_降噪_参数1}:block={a.视频参数_降噪_参数2}:bstep={a.视频参数_降噪_参数3}:group={a.视频参数_降噪_参数4}")
            Case "avs" '在后面处理，防止出事
        End Select

        If a.视频参数_锐化_水平尺寸 <> "" AndAlso a.视频参数_锐化_垂直尺寸 <> "" AndAlso a.视频参数_锐化_锐化强度 <> "" Then
            视频滤镜参数集.Add($"unsharp=luma_msize_x={a.视频参数_锐化_水平尺寸}:luma_msize_y={a.视频参数_锐化_垂直尺寸}:luma_amount={a.视频参数_锐化_锐化强度}")
        End If

        Select Case a.视频参数_逐行与隔行_操作
            Case 1 : 视频滤镜参数集.Add($"yadif=0:-1:0")
            Case 2 : 视频滤镜参数集.Add($"yadif=0:0:0")
            Case 3 : 视频滤镜参数集.Add($"yadif=0:1:0")
            Case 4 : 视频滤镜参数集.Add($"tinterlace=4")
            Case 5 : 视频滤镜参数集.Add($"tinterlace=6")
        End Select

        Select Case a.视频参数_画面翻转_角度翻转
            Case 1 : 视频滤镜参数集.AddRange({$"transpose=1"})
            Case 2 : 视频滤镜参数集.AddRange({$"transpose=1", $"transpose=1"})
            Case 3 : 视频滤镜参数集.AddRange({$"transpose=1", $"transpose=1", $"transpose=1"})
            Case 4 : 视频滤镜参数集.AddRange({$"transpose=2"})
            Case 5 : 视频滤镜参数集.AddRange({$"transpose=2", $"transpose=2"})
            Case 6 : 视频滤镜参数集.AddRange({$"transpose=2", $"transpose=2", $"transpose=2"})
        End Select

        Select Case a.视频参数_画面翻转_镜像翻转
            Case 1 : 视频滤镜参数集.Add($"hflip")
            Case 2 : 视频滤镜参数集.Add($"vflip")
        End Select

        If a.流控制_剪辑_方法 = 2 Then
            If a.流控制_剪辑_入点 <> "" Then 视频参数 &= $"-ss {a.流控制_剪辑_入点} "
            If a.流控制_剪辑_出点 <> "" Then 视频参数 &= $"-to {a.流控制_剪辑_出点} "
        End If

        If a.自定义参数_视频滤镜 <> "" Then 视频滤镜参数集.Add(处理自定义参数的通配字符串(a.自定义参数_视频滤镜, 输入文件))
        If 视频滤镜参数集.Count > 0 Then
            Dim vf As String = String.Join(",", 视频滤镜参数集)
            视频参数 &= $"-vf ""{vf}"" "
        End If

        If a.自定义参数_视频参数 <> "" Then 视频参数 &= 处理自定义参数的通配字符串(a.自定义参数_视频参数, 输入文件) & " "

        '=============================================================

        If a.音频参数_编码器_具体编码 <> "" Then
            If a.音频参数_编码器_具体编码 = "-an" Then
                音频参数 &= $"-an "
            Else
                音频参数 &= $"-c:a {a.音频参数_编码器_具体编码} "
            End If
        End If

        Select Case a.音频参数_比特率_控制方式
            Case "CBR"
                If a.音频参数_比特率_基础 <> "" Then 音频参数 &= $"-b:a {a.音频参数_比特率_基础} "
            Case "VBR"
                If a.音频参数_比特率_基础 <> "" Then 音频参数 &= $"-b:a {a.音频参数_比特率_基础} "
                If a.音频参数_比特率_质量值 <> "" Then 音频参数 &= $"-q:a {a.音频参数_比特率_质量值} "
            Case "FLAC"
                If a.音频参数_比特率_质量值 <> "" Then 音频参数 &= $"-compression_level {a.音频参数_比特率_质量值} "
        End Select

        If a.音频参数_声道数 <> "" Then 音频参数 &= $"-channel_layout {a.音频参数_声道数} "
        If a.音频参数_采样率 <> "" Then 音频参数 &= $"-ar {a.音频参数_采样率} "

        If a.音频参数_响度标准化_目标响度 <> "" Then
            音频滤镜参数集.Add($"loudnorm=I={If(a.音频参数_响度标准化_目标响度 <> "", a.音频参数_响度标准化_目标响度, -16)}:LRA={If(a.音频参数_响度标准化_动态范围 <> "", a.音频参数_响度标准化_动态范围, 1)}:tp={If(a.音频参数_响度标准化_峰值电平 <> "", a.音频参数_响度标准化_峰值电平, -1)}")
        End If

        If a.自定义参数_音频滤镜 <> "" Then 音频滤镜参数集.Add(处理自定义参数的通配字符串(a.自定义参数_音频滤镜, 输入文件))
        If 音频滤镜参数集.Count > 0 Then
            Dim vf As String = String.Join(",", 音频滤镜参数集)
            音频参数 &= $"-af ""{vf}"" "
        End If

        If a.自定义参数_音频参数 <> "" Then arg &= 处理自定义参数的通配字符串(a.自定义参数_音频参数, 输入文件) & " "

        '=================================================================

        Select Case a.图片参数_编码器_编码名称
            Case "无损压缩 PNG 已强制最高压缩度"
                视频参数 &= $"-c:v png -compression_level 9 "
            Case "有损压缩 JPEG \ JPG 质量控制越小越高 1~31"
                视频参数 &= $"-c:v mjpeg -q:v {If(a.图片参数_编码器_参数 <> "", a.图片参数_编码器_参数, 1)} "
            Case "有损压缩 WEBP 质量控制越大越高 0~100"
                视频参数 &= $"-c:v libwebp -q:v {If(a.图片参数_编码器_参数 <> "", a.图片参数_编码器_参数, 100)} "
            Case "无损压缩 WEBP"
                视频参数 &= $"-c:v libwebp -lossless 1 "
            Case "无损压缩 TIFF"
                视频参数 &= $"-c:v tiff "
            Case "有损压缩 AVIF 质量控制越大越高 0~100"
                视频参数 &= $"-c:v libavif -q:v {If(a.图片参数_编码器_参数 <> "", a.图片参数_编码器_参数, 100)} "
            Case "无损压缩 AVIF"
                视频参数 &= $"-c:v libavif -lossless 1 "
            Case "传统动画 GIF"
                视频参数 &= $"-c:v gif "
            Case "原画位图 BMP"
                视频参数 &= $"-c:v bmp "
            Case "医学影像 JPEG - LS"
                视频参数 &= $"-c:v jpegls "
            Case "电影扫描 DPX"
                视频参数 &= $"-c:v dpx "
            Case "工业光魔 OpenEXR"
                视频参数 &= $"-c:v exr "
        End Select

        If a.图片参数_简易调色_亮度 <> "" AndAlso a.图片参数_简易调色_对比度 <> "" AndAlso a.图片参数_简易调色_饱和度 <> "" AndAlso a.图片参数_简易调色_伽马 <> "" Then
            Dim eq As New List(Of String)
            If a.图片参数_简易调色_亮度 <> "" Then eq.Add($"brightness={a.图片参数_简易调色_亮度}")
            If a.图片参数_简易调色_对比度 <> "" Then eq.Add($"contrast={a.图片参数_简易调色_对比度}")
            If a.图片参数_简易调色_饱和度 <> "" Then eq.Add($"saturation={a.图片参数_简易调色_饱和度}")
            If a.图片参数_简易调色_伽马 <> "" Then eq.Add($"gamma={a.图片参数_简易调色_伽马}")
            视频滤镜参数集.Add($"eq={String.Join(":", eq)}")
        End If

        '=================================================================

        If a.流控制_启用保留其他视频流 AndAlso 视频参数 <> "" Then
            arg &= $"-map 0:v? -c:v copy "
            If a.流控制_将视频参数应用于指定流.Length > 0 Then
                For Each vi In a.流控制_将视频参数应用于指定流
                    arg &= $"-map {vi}? "
                Next
            Else
                If 视频参数 <> "" Then arg &= $"-map -0:v:0? "
            End If
        End If
        If a.流控制_将视频参数应用于指定流.Length > 0 Then
            If 视频参数 <> "" Then
                For Each vi In a.流控制_将视频参数应用于指定流
                    arg &= $"-map {vi}? {视频参数} "
                Next
            End If
        Else
            If 视频参数 <> "" Then arg &= $"{视频参数} "
        End If

        '=================================================================

        If a.流控制_启用保留其他音频流 AndAlso 音频参数 <> "" Then
            arg &= $"-map 0:a? -c:a copy "
            If a.流控制_将音频参数应用于指定流.Length > 0 Then
                For Each vi In a.流控制_将音频参数应用于指定流
                    arg &= $"-map -{vi}? "
                Next
            Else
                If 音频参数 <> "" Then arg &= $"-map -0:a:0? "
            End If
        End If
        If a.流控制_将音频参数应用于指定流.Length > 0 Then
            If 音频参数 <> "" Then
                For Each vi In a.流控制_将音频参数应用于指定流
                    arg &= $"-map {vi}? {音频参数} "
                Next
            End If
        Else
            If 音频参数 <> "" Then arg &= $"{音频参数} "
        End If

        '=================================================================

        If a.流控制_启用保留内嵌字幕流 Then
            arg &= $"{If(arg.Contains("-map"), "-map 0:s?", "")}  -c:s copy "
        End If

        Select Case a.流控制_元数据选项
            Case 1 : arg &= $"-map_metadata 0 "
            Case 2 : arg &= $"-map_metadata -1 "
        End Select

        Select Case a.流控制_章节选项
            Case 1 : arg &= $"-map_chapters 0 "
            Case 2 : arg &= $"-map_chapters -1 "
        End Select

        Select Case a.流控制_附件选项
            Case 1 : arg &= $"{If(arg.Contains("-map"), "-map 0:t?", "")}  -c:t copy "
        End Select

        If 滤镜图参数集.Count > 0 Then
            Dim vf As String = 处理自定义参数的通配字符串(String.Join(",", 滤镜图参数集), 输入文件)
            arg &= $"-filter_complex ""{vf}"" "
        End If

        '=================================================================
        Dim 将自动混流的SRT字幕 As String = Path.Combine(输入文件的文件夹, Path.GetFileNameWithoutExtension(输入文件) & ".srt")
        Dim 将自动混流的ASS字幕 As String = Path.Combine(输入文件的文件夹, Path.GetFileNameWithoutExtension(输入文件) & ".ass")
        Dim 将自动混流的SSA字幕 As String = Path.Combine(输入文件的文件夹, Path.GetFileNameWithoutExtension(输入文件) & ".ssa")
        If a.流控制_启用自动混流同名字幕文件 Then
            If FileIO.FileSystem.FileExists(将自动混流的SRT字幕) Then
                arg &= $"-i {""""}{将自动混流的SRT字幕}{""""} "
                If a.输出容器.Equals(".mp4", StringComparison.CurrentCultureIgnoreCase) Then arg &= $" -c:s mov_text "
            End If
            If FileIO.FileSystem.FileExists(将自动混流的ASS字幕) Then
                arg &= $"-i {""""}{将自动混流的ASS字幕}{""""} "
                If a.输出容器.Equals(".mp4", StringComparison.CurrentCultureIgnoreCase) Then arg &= $" -c:s mov_text "
            End If
            If FileIO.FileSystem.FileExists(将自动混流的SSA字幕) Then
                arg &= $"-i {""""}{将自动混流的SSA字幕}{""""} "
                If a.输出容器.Equals(".mp4", StringComparison.CurrentCultureIgnoreCase) Then arg &= $" -c:s mov_text "
            End If
        End If


        If a.自定义参数_之后参数 <> "" Then arg &= $"{处理自定义参数的通配字符串(a.自定义参数_之后参数, 输入文件)} "

        If Not a.输出命名_不使用输出文件参数 Then arg &= $"{""""}{输出文件}{""""} -y "

        If a.自定义参数_最后参数 <> "" Then arg &= $"{处理自定义参数的通配字符串(a.自定义参数_最后参数, 输入文件)} "

        Return arg
    End Function

    Shared Function 处理自定义参数的通配字符串(自定义参数 As String, 输入文件 As String) As String
        If 自定义参数 = "" Then
            Return ""
            Exit Function
        End If
        Dim a As String = 自定义参数
        a = a.Replace("<InputFilePath>", 输入文件)
        a = a.Replace("<InputFilePathWithOutExtension>", Path.Combine(Path.GetDirectoryName(输入文件), Path.GetFileNameWithoutExtension(输入文件)))
        a = a.Replace("<InputFileName>", Path.GetFileName(输入文件))
        a = a.Replace("<InputFileNameWithOutExtension>", Path.GetFileNameWithoutExtension(输入文件))
        Return a
    End Function
End Class
