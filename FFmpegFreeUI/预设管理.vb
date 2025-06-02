Imports System.IO
Imports System.Text.Json
Imports Sunny.UI

Public Class 预设管理

    Public Shared Sub 显示预设(a As 预设数据类型)
        Form1.UiComboBox输出容器.Text = a.输出容器

        Form1.UiCheckBox解码器.Checked = a.解码参数_启用解码器参数
        Form1.UiComboBox解码器.Text = a.解码参数_解码器
        Form1.UiCheckBox解码数据格式.Checked = a.解码参数_启用解码数据格式参数
        Form1.UiComboBox解码数据格式.Text = a.解码参数_解码数据格式

        Form1.UiCheckBox视频编码器.Checked = a.视频参数_启用编码器参数
        Form1.UiComboBox编码类别.Text = a.视频参数_编码器_类别
        Form1.UiComboBox具体编码.Text = a.视频参数_编码器_具体编码
        Form1.UiComboBox编码预设.Text = a.视频参数_编码器_质量
        Form1.UiCheckBox分辨率.Checked = a.视频参数_启用分辨率参数
        Form1.UiComboBox分辨率.Text = a.视频参数_分辨率
        Form1.UiTextBox分辨率自动计算宽度.Text = a.视频参数_分辨率自动计算_宽度
        Form1.UiTextBox分辨率自动计算高度.Text = a.视频参数_分辨率自动计算_高度
        Form1.UiCheckBox帧速率.Checked = a.视频参数_启用帧速率参数
        Form1.UiComboBox帧速率.Text = a.视频参数_帧速率
        Form1.UiTextBox智能抽帧阈值.Text = a.视频参数_帧速率_智能抽帧阈值

        Form1.UiCheckBox比特率.Checked = a.视频参数_启用比特率参数
        Select Case a.视频参数_比特率_控制方式
            Case "VBR" : Form1.UiComboBox比特率控制方式.SelectedIndex = 0
            Case "VBR HQ" : Form1.UiComboBox比特率控制方式.SelectedIndex = 1
            Case "CRF" : Form1.UiComboBox比特率控制方式.SelectedIndex = 2
            Case "CQP" : Form1.UiComboBox比特率控制方式.SelectedIndex = 3
            Case "ABR" : Form1.UiComboBox比特率控制方式.SelectedIndex = 4
            Case "TPE" : Form1.UiComboBox比特率控制方式.SelectedIndex = 5
            Case "CBR" : Form1.UiComboBox比特率控制方式.SelectedIndex = 6
        End Select
        Form1.UiTextBox硬件加速HQ前瞻分析帧数.Text = a.视频参数_比特率_前瞻分析帧数
        Form1.UiTextBox基础比特率.Text = a.视频参数_比特率_基础
        Form1.UiTextBox最低比特率.Text = a.视频参数_比特率_最低值
        Form1.UiTextBox最高比特率.Text = a.视频参数_比特率_最高值
        Form1.UiTextBox比特率缓冲区.Text = a.视频参数_比特率_缓冲区

        Form1.UiCheckBox质量控制.Checked = a.视频参数_启用质量控制参数
        Select Case a.视频参数_全局质量控制_参数
            Case "cq" : Form1.UiComboBox全局质量控制参数.SelectedIndex = 1
            Case "qp" : Form1.UiComboBox全局质量控制参数.SelectedIndex = 2
            Case "crf" : Form1.UiComboBox全局质量控制参数.SelectedIndex = 3
        End Select
        Form1.UiTextBox全局质量控制值.Text = a.视频参数_全局质量控制_值
        Form1.UiCheckBox展开精细控制.Checked = a.视频参数_展开精细控制
        Form1.UiTextBox质量最小值.Text = a.视频参数_精细控制_qpmin
        Form1.UiTextBox质量最大值.Text = a.视频参数_精细控制_qpmax
        Form1.UiTextBox相邻帧质量变化限制.Text = a.视频参数_精细控制_qpstep
        Form1.UiTextBox关键帧质量.Text = a.视频参数_精细控制_qp_i
        Form1.UiTextBox前向预测帧质量.Text = a.视频参数_精细控制_qp_p
        Form1.UiTextBox双向预测帧质量.Text = a.视频参数_精细控制_qp_b
        Form1.UiTextBox关键帧质量基准.Text = a.视频参数_精细控制_i_qfactor
        Form1.UiTextBoxP对I的比例.Text = a.视频参数_精细控制_p_qfactor
        Form1.UiTextBoxB对P的比例.Text = a.视频参数_精细控制_b_qfactor
        Form1.UiTextBox关键帧质量固定偏移.Text = a.视频参数_精细控制_i_qoffset
        Form1.UiTextBox前向预测帧质量固定偏移.Text = a.视频参数_精细控制_p_qoffset
        Form1.UiTextBox双向预测帧质量固定偏移.Text = a.视频参数_精细控制_b_qoffset

        Form1.UiCheckBox帧排列.Checked = a.视频参数_启用帧排列参数
        Form1.UiTextBox关键帧间隔.Text = a.视频参数_帧排列_关键帧间隔
        Form1.UiTextBox双向预测帧数量.Text = a.视频参数_帧排列_双向预测帧数量

        Form1.UiCheckBox画面规格.Checked = a.视频参数_启用画面规格参数
        Form1.UiComboBox配置文件.Text = a.视频参数_画面规格_配置文件
        Form1.UiComboBox场景优化.Text = a.视频参数_画面规格_场景优化
        Form1.UiComboBox像素格式.Text = a.视频参数_画面视觉_像素格式

        Form1.UiCheckBox色彩管理.Checked = a.视频参数_启用色彩管理参数
        Form1.UiComboBox矩阵系数.Text = a.视频参数_色彩管理_矩阵系数
        Form1.UiComboBox色域.Text = a.视频参数_色彩管理_色域
        Form1.UiComboBox传输特性.Text = a.视频参数_色彩管理_传输特性
        Select Case a.视频参数_色彩管理_范围
            Case "pc" : Form1.UiComboBox色彩范围.SelectedIndex = 1
            Case "tv" : Form1.UiComboBox色彩范围.SelectedIndex = 2
        End Select
        Form1.UiComboBox色彩管理处理方式.SelectedIndex = a.视频参数_色彩管理_处理方式

        Form1.UiCheckBox降噪.Checked = a.视频参数_启用降噪参数
        Select Case a.视频参数_降噪_方式
            Case "hqdn3d" : Form1.UiComboBox降噪方式.SelectedIndex = 0
            Case "nlmeans" : Form1.UiComboBox降噪方式.SelectedIndex = 1
            Case "atadenoise" : Form1.UiComboBox降噪方式.SelectedIndex = 2
            Case "bm3d" : Form1.UiComboBox降噪方式.SelectedIndex = 3
        End Select
        Form1.UiTextBox降噪参数1.Text = a.视频参数_降噪_参数1
        Form1.UiTextBox降噪参数2.Text = a.视频参数_降噪_参数2
        Form1.UiTextBox降噪参数3.Text = a.视频参数_降噪_参数3
        Form1.UiTextBox降噪参数4.Text = a.视频参数_降噪_参数4

        Form1.UiCheckBox锐化.Checked = a.视频参数_启用锐化参数
        Form1.UiTextBox锐化水平尺寸.Text = a.视频参数_锐化_水平尺寸
        Form1.UiTextBox锐化垂直尺寸.Text = a.视频参数_锐化_垂直尺寸
        Form1.UiTextBox锐化强度.Text = a.视频参数_锐化_锐化强度

        Form1.UiCheckBox逐行与隔行.Checked = a.视频参数_启用逐行与隔行参数
        Form1.UiComboBox逐行与隔行处理方式.SelectedIndex = a.视频参数_逐行与隔行_操作

        Form1.UiCheckBox自定义视频滤镜.Checked = a.视频参数_启用自定义滤镜参数
        Form1.UiTextBox自定义视频滤镜.Text = a.视频参数_自定义滤镜
        Form1.UiCheckBox自定义视频参数.Checked = a.视频参数_启用自定义参数
        Form1.UiTextBox自定义视频参数.Text = a.视频参数_自定义参数


        Form1.UiCheckBox音频编码器.Checked = a.音频参数_启用编码器参数
        Form1.UiComboBox音频编码器.Text = a.音频参数_编码器_编码名称
        Form1.UiCheckBox音频比特率.Checked = a.音频参数_启用比特率参数
        Select Case a.音频参数_比特率_控制方式
            Case "CBR" : Form1.UiComboBox音频比特率控制方式.SelectedIndex = 0
            Case "VBR" : Form1.UiComboBox音频比特率控制方式.SelectedIndex = 1
            Case "FLAC" : Form1.UiComboBox音频比特率控制方式.SelectedIndex = 2
        End Select
        Form1.UiTextBox音频基础比特率.Text = a.音频参数_比特率_基础
        Form1.UiTextBox音频比特率质量值.Text = a.音频参数_比特率_质量值
        Form1.UiCheckBox声道布局.Checked = a.音频参数_启用声道数参数
        Form1.UiComboBox声道布局.Text = a.音频参数_声道数
        Form1.UiCheckBox采样率.Checked = a.音频参数_启用采样率参数
        Form1.UiComboBox采样率.Text = a.音频参数_采样率
        Form1.UiCheckBox响度标准化.Checked = a.音频参数_启用响度标准化参数
        Form1.UiTextBox响度标准化目标响度.Text = a.音频参数_响度标准化_目标响度
        Form1.UiTextBox响度标准化动态范围.Text = a.音频参数_响度标准化_动态范围
        Form1.UiTextBox响度标准化峰值电平.Text = a.音频参数_响度标准化_峰值电平
        Form1.UiCheckBox自定义音频滤镜.Checked = a.音频参数_启用自定义滤镜
        Form1.UiTextBox自定义音频滤镜.Text = a.音频参数_自定义滤镜
        Form1.UiCheckBox自定义音频参数.Checked = a.音频参数_启用自定义参数
        Form1.UiTextBox自定义音频参数.Text = a.音频参数_自定义参数

        Form1.UiCheckBox图片编码器.Checked = a.图片参数_启用编码器参数
        Form1.UiComboBox图片编码器.Text = a.图片参数_编码器_编码名称
        Form1.UiTextBox图片编码器质量.Text = a.图片参数_编码器_参数
        Form1.UiCheckBox图片分辨率.Checked = a.图片参数_启用分辨率参数
        Form1.UiTextBox图片分辨率宽度.Text = a.图片参数_分辨率_宽度
        Form1.UiTextBox图片分辨率高度.Text = a.图片参数_分辨率_高度
        Form1.UiCheckBox自定义图片参数.Checked = a.图片参数_启用自定义参数
        Form1.UiTextBox自定义图片参数.Text = a.图片参数_自定义参数

        Form1.UiTextBox将视频参数用于这些流.Text = String.Join(",", a.流控制_将视频参数应用于指定流)
        Form1.UiCheckBox保留其他视频流.Checked = a.流控制_启用保留其他视频流
        Form1.UiTextBox将音频参数用于这些流.Text = String.Join(",", a.流控制_将音频参数应用于指定流)
        Form1.UiCheckBox保留其他音频流.Checked = a.流控制_启用保留其他音频流
        Form1.UiCheckBox保留内嵌字幕流.Checked = a.流控制_启动保留内嵌字幕流
        Form1.UiCheckBox保留元数据.Checked = a.流控制_启用保留元数据
        Form1.UiCheckBox保留章节信息.Checked = a.流控制_启用保留章节信息
        Form1.UiCheckBox快速剪辑.Checked = a.流控制_启用快速剪辑参数
        Form1.UiTextBox快速剪辑入点.Text = a.流控制_快速剪辑_入点
        Form1.UiTextBox快速剪辑出点.Text = a.流控制_快速剪辑_出点
        Form1.UiCheckBox自动混流同名字幕文件.Checked = a.自动混流同名字幕
        Form1.UiCheckBox自定义filter_complex.Checked = a.流控制_启用自定义filter_complex
        Form1.UiTextBoxfilter_complex.Text = a.流控制_filter_complex
        Form1.UiCheckBox1.Checked = a.流控制_启用自定义其他参数
        Form1.UiTextBox1.Text = a.流控制_自定义其他参数
    End Sub

    Shared ReadOnly separator As String() = {","}

    Public Shared Sub 储存预设(ByRef a As 预设数据类型)
        a.输出容器 = Form1.UiComboBox输出容器.Text

        a.解码参数_启用解码器参数 = Form1.UiCheckBox解码器.Checked
        a.解码参数_解码器 = Form1.UiComboBox解码器.Text
        a.解码参数_启用解码数据格式参数 = Form1.UiCheckBox解码数据格式.Checked
        a.解码参数_解码数据格式 = Form1.UiComboBox解码数据格式.Text

        a.视频参数_启用编码器参数 = Form1.UiCheckBox视频编码器.Checked
        a.视频参数_编码器_类别 = Form1.UiComboBox编码类别.Text
        a.视频参数_编码器_具体编码 = Form1.UiComboBox具体编码.Text
        a.视频参数_编码器_质量 = Form1.UiComboBox编码预设.Text
        a.视频参数_启用分辨率参数 = Form1.UiCheckBox分辨率.Checked
        a.视频参数_分辨率 = Form1.UiComboBox分辨率.Text
        a.视频参数_分辨率自动计算_宽度 = Form1.UiTextBox分辨率自动计算宽度.Text
        a.视频参数_分辨率自动计算_高度 = Form1.UiTextBox分辨率自动计算高度.Text
        a.视频参数_启用帧速率参数 = Form1.UiCheckBox帧速率.Checked
        a.视频参数_帧速率 = Form1.UiComboBox帧速率.Text
        a.视频参数_帧速率_智能抽帧阈值 = Form1.UiTextBox智能抽帧阈值.Text

        a.视频参数_启用比特率参数 = Form1.UiCheckBox比特率.Checked
        Select Case Form1.UiComboBox比特率控制方式.SelectedIndex
            Case 0 : a.视频参数_比特率_控制方式 = "VBR"
            Case 1 : a.视频参数_比特率_控制方式 = "VBR HQ"
            Case 2 : a.视频参数_比特率_控制方式 = "CRF"
            Case 3 : a.视频参数_比特率_控制方式 = "CQP"
            Case 4 : a.视频参数_比特率_控制方式 = "ABR"
            Case 5 : a.视频参数_比特率_控制方式 = "TPE"
            Case 6 : a.视频参数_比特率_控制方式 = "CBR"
        End Select
        a.视频参数_比特率_前瞻分析帧数 = Form1.UiTextBox硬件加速HQ前瞻分析帧数.Text
        a.视频参数_比特率_基础 = Form1.UiTextBox基础比特率.Text
        a.视频参数_比特率_最低值 = Form1.UiTextBox最低比特率.Text
        a.视频参数_比特率_最高值 = Form1.UiTextBox最高比特率.Text
        a.视频参数_比特率_缓冲区 = Form1.UiTextBox比特率缓冲区.Text

        a.视频参数_启用质量控制参数 = Form1.UiCheckBox质量控制.Checked
        Select Case Form1.UiComboBox全局质量控制参数.SelectedIndex
            Case 1 : a.视频参数_全局质量控制_参数 = "cq"
            Case 2 : a.视频参数_全局质量控制_参数 = "qp"
            Case 3 : a.视频参数_全局质量控制_参数 = "crf"
        End Select
        a.视频参数_全局质量控制_值 = Form1.UiTextBox全局质量控制值.Text
        a.视频参数_展开精细控制 = Form1.UiCheckBox展开精细控制.Checked
        a.视频参数_精细控制_qpmin = Form1.UiTextBox质量最小值.Text
        a.视频参数_精细控制_qpmax = Form1.UiTextBox质量最大值.Text
        a.视频参数_精细控制_qpstep = Form1.UiTextBox相邻帧质量变化限制.Text
        a.视频参数_精细控制_qp_i = Form1.UiTextBox关键帧质量.Text
        a.视频参数_精细控制_qp_p = Form1.UiTextBox前向预测帧质量.Text
        a.视频参数_精细控制_qp_b = Form1.UiTextBox双向预测帧质量.Text
        a.视频参数_精细控制_i_qfactor = Form1.UiTextBox关键帧质量基准.Text
        a.视频参数_精细控制_p_qfactor = Form1.UiTextBoxP对I的比例.Text
        a.视频参数_精细控制_b_qfactor = Form1.UiTextBoxB对P的比例.Text
        a.视频参数_精细控制_i_qoffset = Form1.UiTextBox关键帧质量固定偏移.Text
        a.视频参数_精细控制_p_qoffset = Form1.UiTextBox前向预测帧质量固定偏移.Text
        a.视频参数_精细控制_b_qoffset = Form1.UiTextBox双向预测帧质量固定偏移.Text

        a.视频参数_启用帧排列参数 = Form1.UiCheckBox帧排列.Checked
        a.视频参数_帧排列_关键帧间隔 = Form1.UiTextBox关键帧间隔.Text
        a.视频参数_帧排列_双向预测帧数量 = Form1.UiTextBox双向预测帧数量.Text

        a.视频参数_启用画面规格参数 = Form1.UiCheckBox画面规格.Checked
        a.视频参数_画面规格_配置文件 = Form1.UiComboBox配置文件.Text
        a.视频参数_画面规格_场景优化 = Form1.UiComboBox场景优化.Text
        a.视频参数_画面视觉_像素格式 = Form1.UiComboBox像素格式.Text

        a.视频参数_启用色彩管理参数 = Form1.UiCheckBox色彩管理.Checked
        a.视频参数_色彩管理_矩阵系数 = Form1.UiComboBox矩阵系数.Text
        a.视频参数_色彩管理_色域 = Form1.UiComboBox色域.Text
        a.视频参数_色彩管理_传输特性 = Form1.UiComboBox传输特性.Text
        Select Case Form1.UiComboBox色彩范围.SelectedIndex
            Case 1 : a.视频参数_色彩管理_范围 = "pc"
            Case 2 : a.视频参数_色彩管理_范围 = "tv"
        End Select
        a.视频参数_色彩管理_处理方式 = Form1.UiComboBox色彩管理处理方式.SelectedIndex

        a.视频参数_启用降噪参数 = Form1.UiCheckBox降噪.Checked
        Select Case Form1.UiComboBox降噪方式.SelectedIndex
            Case 0 : a.视频参数_降噪_方式 = "hqdn3d"
            Case 1 : a.视频参数_降噪_方式 = "nlmeans"
            Case 2 : a.视频参数_降噪_方式 = "atadenoise"
            Case 3 : a.视频参数_降噪_方式 = "bm3d"
        End Select
        a.视频参数_降噪_参数1 = Form1.UiTextBox降噪参数1.Text
        a.视频参数_降噪_参数2 = Form1.UiTextBox降噪参数2.Text
        a.视频参数_降噪_参数3 = Form1.UiTextBox降噪参数3.Text
        a.视频参数_降噪_参数4 = Form1.UiTextBox降噪参数4.Text

        a.视频参数_启用锐化参数 = Form1.UiCheckBox锐化.Checked
        a.视频参数_锐化_水平尺寸 = Form1.UiTextBox锐化水平尺寸.Text
        a.视频参数_锐化_垂直尺寸 = Form1.UiTextBox锐化垂直尺寸.Text
        a.视频参数_锐化_锐化强度 = Form1.UiTextBox锐化强度.Text

        a.视频参数_启用逐行与隔行参数 = Form1.UiCheckBox逐行与隔行.Checked
        a.视频参数_逐行与隔行_操作 = Form1.UiComboBox逐行与隔行处理方式.SelectedIndex

        a.视频参数_启用自定义滤镜参数 = Form1.UiCheckBox自定义视频滤镜.Checked
        a.视频参数_自定义滤镜 = Form1.UiTextBox自定义视频滤镜.Text
        a.视频参数_启用自定义参数 = Form1.UiCheckBox自定义视频参数.Checked
        a.视频参数_自定义参数 = Form1.UiTextBox自定义视频参数.Text

        a.音频参数_启用编码器参数 = Form1.UiCheckBox音频编码器.Checked
        a.音频参数_编码器_编码名称 = Form1.UiComboBox音频编码器.Text
        a.音频参数_启用比特率参数 = Form1.UiCheckBox音频比特率.Checked
        Select Case Form1.UiComboBox音频比特率控制方式.SelectedIndex
            Case 0 : a.音频参数_比特率_控制方式 = "CBR"
            Case 1 : a.音频参数_比特率_控制方式 = "VBR"
            Case 2 : a.音频参数_比特率_控制方式 = "FLAC"
        End Select
        a.音频参数_比特率_基础 = Form1.UiTextBox音频基础比特率.Text
        a.音频参数_比特率_质量值 = Form1.UiTextBox音频比特率质量值.Text
        a.音频参数_启用声道数参数 = Form1.UiCheckBox声道布局.Checked
        a.音频参数_声道数 = Form1.UiComboBox声道布局.Text
        a.音频参数_启用采样率参数 = Form1.UiCheckBox采样率.Checked
        a.音频参数_采样率 = Form1.UiComboBox采样率.Text
        a.音频参数_启用响度标准化参数 = Form1.UiCheckBox响度标准化.Checked
        a.音频参数_响度标准化_目标响度 = Form1.UiTextBox响度标准化目标响度.Text
        a.音频参数_响度标准化_动态范围 = Form1.UiTextBox响度标准化动态范围.Text
        a.音频参数_响度标准化_峰值电平 = Form1.UiTextBox响度标准化峰值电平.Text
        a.音频参数_启用自定义滤镜 = Form1.UiCheckBox自定义音频滤镜.Checked
        a.音频参数_自定义滤镜 = Form1.UiTextBox自定义音频滤镜.Text
        a.音频参数_启用自定义参数 = Form1.UiCheckBox自定义音频参数.Checked
        a.音频参数_自定义参数 = Form1.UiTextBox自定义音频参数.Text

        a.图片参数_启用编码器参数 = Form1.UiCheckBox图片编码器.Checked
        a.图片参数_编码器_编码名称 = Form1.UiComboBox图片编码器.Text
        a.图片参数_编码器_参数 = Form1.UiTextBox图片编码器质量.Text
        a.图片参数_启用分辨率参数 = Form1.UiCheckBox图片分辨率.Checked
        a.图片参数_分辨率_宽度 = Form1.UiTextBox图片分辨率宽度.Text
        a.图片参数_分辨率_高度 = Form1.UiTextBox图片分辨率高度.Text
        a.图片参数_启用自定义参数 = Form1.UiCheckBox自定义图片参数.Checked
        a.图片参数_自定义参数 = Form1.UiTextBox自定义图片参数.Text

        a.流控制_将视频参数应用于指定流 = Form1.UiTextBox将视频参数用于这些流.Text.Split(separator, StringSplitOptions.RemoveEmptyEntries)
        a.流控制_启用保留其他视频流 = Form1.UiCheckBox保留其他视频流.Checked
        a.流控制_将音频参数应用于指定流 = Form1.UiTextBox将音频参数用于这些流.Text.Split(separator, StringSplitOptions.RemoveEmptyEntries)
        a.流控制_启用保留其他音频流 = Form1.UiCheckBox保留其他音频流.Checked
        a.流控制_启动保留内嵌字幕流 = Form1.UiCheckBox保留内嵌字幕流.Checked
        a.流控制_启用保留元数据 = Form1.UiCheckBox保留元数据.Checked
        a.流控制_启用保留章节信息 = Form1.UiCheckBox保留章节信息.Checked
        a.流控制_启用快速剪辑参数 = Form1.UiCheckBox快速剪辑.Checked
        a.流控制_快速剪辑_入点 = Form1.UiTextBox快速剪辑入点.Text
        a.流控制_快速剪辑_出点 = Form1.UiTextBox快速剪辑出点.Text
        a.自动混流同名字幕 = Form1.UiCheckBox自动混流同名字幕文件.Checked
        a.流控制_启用自定义filter_complex = Form1.UiCheckBox自定义filter_complex.Checked
        a.流控制_filter_complex = Form1.UiTextBoxfilter_complex.Text
        a.流控制_启用自定义其他参数 = Form1.UiCheckBox1.Checked
        a.流控制_自定义其他参数 = Form1.UiTextBox1.Text
    End Sub

    Public Shared Sub 保存预设到文件()
        Dim d As New SaveFileDialog With {.Filter = "Json|*.json"}
        d.ShowDialog(Form1)
        If d.FileName = "" Then Exit Sub
        Dim a As New 预设数据类型
        储存预设(a)
        File.WriteAllText(d.FileName, JsonSerializer.Serialize(a, JsonSO))
    End Sub

    Public Shared Sub 从文件读取预设()
        Dim d As New OpenFileDialog With {.Filter = "Json|*.json"}
        d.ShowDialog(Form1)
        If Not File.Exists(d.FileName) Then Exit Sub
        Dim a As 预设数据类型 = JsonSerializer.Deserialize(Of 预设数据类型)(File.ReadAllText(d.FileName))
        显示预设(a)
        Form1.RichTextBox1.Text = "ffmpeg.exe " & 预设管理.将预设数据转换为命令行(a, "假装这是输入目录\假装这是输入文件", "假装这是输出目录\假装这是输出文件")
    End Sub

    Public Shared Sub 重置全部包含在预设中的设置()
        ' 解码参数
        Form1.UiComboBox输出容器.Text = ""
        Form1.UiCheckBox解码器.Checked = False
        Form1.UiComboBox解码器.Text = ""
        Form1.UiCheckBox解码数据格式.Checked = False
        Form1.UiComboBox解码数据格式.Text = ""

        ' 视频参数
        Form1.UiCheckBox视频编码器.Checked = False
        Form1.UiComboBox编码类别.Text = ""
        Form1.UiComboBox具体编码.Text = ""
        Form1.UiComboBox编码预设.Text = ""
        Form1.UiCheckBox分辨率.Checked = False
        Form1.UiComboBox分辨率.Text = ""
        Form1.UiTextBox分辨率自动计算宽度.Text = ""
        Form1.UiTextBox分辨率自动计算高度.Text = ""
        Form1.UiCheckBox帧速率.Checked = False
        Form1.UiComboBox帧速率.Text = ""
        Form1.UiTextBox智能抽帧阈值.Text = ""
        Form1.UiCheckBox比特率.Checked = False
        Form1.UiComboBox比特率控制方式.SelectedIndex = -1
        Form1.UiTextBox硬件加速HQ前瞻分析帧数.Text = ""
        Form1.UiTextBox基础比特率.Text = ""
        Form1.UiTextBox最低比特率.Text = ""
        Form1.UiTextBox最高比特率.Text = ""
        Form1.UiTextBox比特率缓冲区.Text = ""
        Form1.UiCheckBox质量控制.Checked = False
        Form1.UiComboBox全局质量控制参数.SelectedIndex = -1
        Form1.UiTextBox全局质量控制值.Text = ""
        Form1.UiCheckBox展开精细控制.Checked = False
        Form1.UiTextBox质量最小值.Text = ""
        Form1.UiTextBox质量最大值.Text = ""
        Form1.UiTextBox相邻帧质量变化限制.Text = ""
        Form1.UiTextBox关键帧质量.Text = ""
        Form1.UiTextBox前向预测帧质量.Text = ""
        Form1.UiTextBox双向预测帧质量.Text = ""
        Form1.UiTextBox关键帧质量基准.Text = ""
        Form1.UiTextBoxP对I的比例.Text = ""
        Form1.UiTextBoxB对P的比例.Text = ""
        Form1.UiTextBox关键帧质量固定偏移.Text = ""
        Form1.UiTextBox前向预测帧质量固定偏移.Text = ""
        Form1.UiTextBox双向预测帧质量固定偏移.Text = ""
        Form1.UiCheckBox帧排列.Checked = False
        Form1.UiTextBox关键帧间隔.Text = ""
        Form1.UiTextBox双向预测帧数量.Text = ""
        Form1.UiCheckBox画面规格.Checked = False
        Form1.UiComboBox配置文件.Text = ""
        Form1.UiComboBox场景优化.Text = ""
        Form1.UiComboBox像素格式.Text = ""
        Form1.UiCheckBox色彩管理.Checked = False
        Form1.UiComboBox矩阵系数.Text = ""
        Form1.UiComboBox色域.Text = ""
        Form1.UiComboBox传输特性.Text = ""
        Form1.UiComboBox色彩范围.SelectedIndex = -1
        Form1.UiComboBox色彩管理处理方式.SelectedIndex = -1
        Form1.UiCheckBox降噪.Checked = False
        Form1.UiComboBox降噪方式.SelectedIndex = -1
        Form1.UiTextBox降噪参数1.Text = ""
        Form1.UiTextBox降噪参数2.Text = ""
        Form1.UiTextBox降噪参数3.Text = ""
        Form1.UiTextBox降噪参数4.Text = ""
        Form1.UiCheckBox锐化.Checked = False
        Form1.UiTextBox锐化水平尺寸.Text = ""
        Form1.UiTextBox锐化垂直尺寸.Text = ""
        Form1.UiTextBox锐化强度.Text = ""
        Form1.UiCheckBox逐行与隔行.Checked = False
        Form1.UiComboBox逐行与隔行处理方式.SelectedIndex = -1
        Form1.UiCheckBox自定义视频滤镜.Checked = False
        Form1.UiTextBox自定义视频滤镜.Text = ""
        Form1.UiCheckBox自定义视频参数.Checked = False
        Form1.UiTextBox自定义视频参数.Text = ""

        ' 音频参数
        Form1.UiCheckBox音频编码器.Checked = False
        Form1.UiComboBox音频编码器.Text = ""
        Form1.UiCheckBox音频比特率.Checked = False
        Form1.UiComboBox音频比特率控制方式.SelectedIndex = -1
        Form1.UiTextBox音频基础比特率.Text = ""
        Form1.UiTextBox音频比特率质量值.Text = ""
        Form1.UiCheckBox声道布局.Checked = False
        Form1.UiComboBox声道布局.Text = ""
        Form1.UiCheckBox采样率.Checked = False
        Form1.UiComboBox采样率.Text = ""
        Form1.UiCheckBox响度标准化.Checked = False
        Form1.UiTextBox响度标准化目标响度.Text = ""
        Form1.UiTextBox响度标准化动态范围.Text = ""
        Form1.UiTextBox响度标准化峰值电平.Text = ""
        Form1.UiCheckBox自定义音频滤镜.Checked = False
        Form1.UiTextBox自定义音频滤镜.Text = ""
        Form1.UiCheckBox自定义音频参数.Checked = False
        Form1.UiTextBox自定义音频参数.Text = ""

        ' 图片参数
        Form1.UiCheckBox图片编码器.Checked = False
        Form1.UiComboBox图片编码器.Text = ""
        Form1.UiTextBox图片编码器质量.Text = ""
        Form1.UiCheckBox图片分辨率.Checked = False
        Form1.UiTextBox图片分辨率宽度.Text = ""
        Form1.UiTextBox图片分辨率高度.Text = ""
        Form1.UiCheckBox自定义图片参数.Checked = False
        Form1.UiTextBox自定义图片参数.Text = ""

        ' 流控制
        Form1.UiTextBox将视频参数用于这些流.Text = ""
        Form1.UiCheckBox保留其他视频流.Checked = False
        Form1.UiTextBox将音频参数用于这些流.Text = ""
        Form1.UiCheckBox保留其他音频流.Checked = False
        Form1.UiCheckBox保留内嵌字幕流.Checked = False
        Form1.UiCheckBox保留元数据.Checked = False
        Form1.UiCheckBox保留章节信息.Checked = False
        Form1.UiCheckBox快速剪辑.Checked = False
        Form1.UiTextBox快速剪辑入点.Text = ""
        Form1.UiTextBox快速剪辑出点.Text = ""
        Form1.UiCheckBox自动混流同名字幕文件.Checked = False
        Form1.UiCheckBox自定义filter_complex.Checked = False
        Form1.UiTextBoxfilter_complex.Text = ""
        Form1.UiCheckBox1.Checked = False
        Form1.UiTextBox1.Text = ""
    End Sub

    Public Shared Function 将预设数据转换为命令行(a As 预设数据类型, 输入文件 As String, 输出文件 As String) As String
        Dim 视频滤镜参数集 As New List(Of String)
        Dim 音频滤镜参数集 As New List(Of String)
        Dim 滤镜图参数集 As New List(Of String)
        Dim 视频参数 As String = ""
        Dim 音频参数 As String = ""

        Dim 输入文件的文件夹 As String = Path.GetDirectoryName(输入文件)

        Dim arg As String = "-hide_banner -nostdin "

        If a.解码参数_启用解码器参数 Then
            If a.解码参数_解码器 <> "" Then arg &= $"-hwaccel {a.解码参数_解码器} "
        End If
        If a.解码参数_启用解码数据格式参数 Then
            If a.解码参数_解码数据格式 <> "" Then arg &= $"-hwaccel_output_format {a.解码参数_解码数据格式} "
        End If

        If a.流控制_启用快速剪辑参数 Then
            If a.流控制_快速剪辑_入点 <> "" AndAlso a.流控制_快速剪辑_出点 <> "" Then
                arg &= $"-ss {a.流控制_快速剪辑_入点} -to {a.流控制_快速剪辑_出点} "
            End If
        End If

        arg &= $"-i {""""}{输入文件}{""""} "



        If a.视频参数_编码器_类别 = "禁用" Then 视频参数 &= $"-vn "
        If a.视频参数_启用编码器参数 Then
            If a.视频参数_编码器_具体编码 <> "" Then 视频参数 &= $"-c:v {a.视频参数_编码器_具体编码} "
            If a.视频参数_编码器_质量 <> "" Then 视频参数 &= $"-preset {a.视频参数_编码器_质量} "
        End If

        If a.视频参数_启用分辨率参数 Then
            If a.视频参数_分辨率 <> "" Then
                视频参数 &= $"-s {a.视频参数_分辨率} "
            Else
                If a.视频参数_分辨率自动计算_宽度 <> "" Then
                    视频滤镜参数集.Add($"scale={a.视频参数_分辨率自动计算_宽度}:-2")
                ElseIf a.视频参数_分辨率自动计算_高度 <> "" Then
                    视频滤镜参数集.Add($"scale=-2:{a.视频参数_分辨率自动计算_高度}")
                End If
            End If
        End If

        If a.视频参数_启用帧速率参数 Then
            If a.视频参数_帧速率 <> "" Then 视频参数 &= $"-r {a.视频参数_帧速率} "
            If a.视频参数_帧速率_智能抽帧阈值 <> "" Then
                视频滤镜参数集.Add($"select='gt(scene,{a.视频参数_帧速率_智能抽帧阈值})',setpts=N/FRAME_RATE/TB")
                视频参数 &= "-fps_mode vfr "
            End If
        End If

        If a.视频参数_启用比特率参数 Then
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
                        Case "hevc_nvenc", "h264_nvenc"
                            视频参数 &= $"-rc vbr_hq "
                        Case "av1_amf"
                            视频参数 &= $"-rc hqvbr -quality high_quality "
                        Case "hevc_amf", "h264_amf"
                            视频参数 &= $"-rc hqvbr -quality quality "
                        Case "av1_qsv", "hevc_qsv", "h264_qsv"
                            视频参数 &= $"-rc la_icq "
                    End Select

                Case "CRF" '自动把全局质量控制调整为 第3个：-crf
                Case "CQP" '自动把全局质量控制调整为 第2个：-qp
                    Select Case a.视频参数_编码器_具体编码
                        Case "av1_nvenc", "hevc_nvenc", "h264_nvenc"
                            视频参数 &= $"-rc constqp "
                        Case "av1_amf", "hevc_amf", "h264_amf"
                            视频参数 &= $"-rc cqp "
                    End Select

                Case "ABR"
                Case "TPE" '可能只有 hevc_nvenc 和 h264_nvenc 支持
                    视频参数 &= $"-pass 2 "
                Case "CBR"
                    视频参数 &= $"-rc cbr "
            End Select




            If a.视频参数_比特率_基础 <> "" Then 视频参数 &= $"-b:v {a.视频参数_比特率_基础} "
            If a.视频参数_比特率_最低值 <> "" Then 视频参数 &= $"-minrate {a.视频参数_比特率_最低值} "
            If a.视频参数_比特率_最高值 <> "" Then 视频参数 &= $"-maxrate {a.视频参数_比特率_最高值} "
            If a.视频参数_比特率_缓冲区 <> "" Then 视频参数 &= $"-bufsize {a.视频参数_比特率_缓冲区} "
        End If

        If a.视频参数_启用质量控制参数 Then
            If a.视频参数_全局质量控制_值 <> "" Then
                Select Case a.视频参数_全局质量控制_参数
                    Case "cq" : 视频参数 &= $"-cq {a.视频参数_全局质量控制_值} "
                    Case "qp" : 视频参数 &= $"-qp {a.视频参数_全局质量控制_值} "
                    Case "crf" : 视频参数 &= $"-crf {a.视频参数_全局质量控制_值} "
                End Select
            End If
            If a.视频参数_比特率_前瞻分析帧数 <> "" Then '不支持 AMD
                Select Case a.视频参数_编码器_具体编码
                    Case "av1_nvenc", "hevc_nvenc", "h264_nvenc", "libx264", "libx265"
                        视频参数 &= $"-rc-lookahead {a.视频参数_比特率_前瞻分析帧数} "
                    Case "av1_qsv", "hevc_qsv", "h264_qsv"
                        视频参数 &= $"-extbrc 1 -look_ahead_depth {a.视频参数_比特率_前瞻分析帧数} "
                End Select
            End If

            If a.视频参数_精细控制_qpmin <> "" Then 视频参数 &= $"-qpmin {a.视频参数_精细控制_qpmin} "
                If a.视频参数_精细控制_qpmax <> "" Then 视频参数 &= $"-qpmax {a.视频参数_精细控制_qpmax} "
                If a.视频参数_精细控制_qpstep <> "" Then 视频参数 &= $"-qpstep {a.视频参数_精细控制_qpstep} "
                If a.视频参数_精细控制_qp_i <> "" Then 视频参数 &= $"-qp_i {a.视频参数_精细控制_qp_i} "
                If a.视频参数_精细控制_qp_p <> "" Then 视频参数 &= $"-qp_p {a.视频参数_精细控制_qp_p} "
                If a.视频参数_精细控制_qp_b <> "" Then 视频参数 &= $"-qp_b {a.视频参数_精细控制_qp_b} "
                If a.视频参数_精细控制_i_qfactor <> "" Then 视频参数 &= $"-i_qfactor {a.视频参数_精细控制_i_qfactor} "
                If a.视频参数_精细控制_p_qfactor <> "" Then 视频参数 &= $"-p_qfactor {a.视频参数_精细控制_p_qfactor} "
                If a.视频参数_精细控制_b_qfactor <> "" Then 视频参数 &= $"-b_qfactor {a.视频参数_精细控制_b_qfactor} "
                If a.视频参数_精细控制_i_qoffset <> "" Then 视频参数 &= $"-i_qoffset {a.视频参数_精细控制_i_qoffset} "
                If a.视频参数_精细控制_p_qoffset <> "" Then 视频参数 &= $"-p_qoffset {a.视频参数_精细控制_p_qoffset} "
                If a.视频参数_精细控制_b_qoffset <> "" Then 视频参数 &= $"-b_qoffset {a.视频参数_精细控制_b_qoffset} "
            End If

            If a.视频参数_启用帧排列参数 Then
            If a.视频参数_帧排列_关键帧间隔 <> "" Then 视频参数 &= $"-g {a.视频参数_帧排列_关键帧间隔} "
            If a.视频参数_帧排列_双向预测帧数量 <> "" Then 视频参数 &= $"-bf {a.视频参数_帧排列_双向预测帧数量} "
        End If

        If a.视频参数_启用画面规格参数 Then
            If a.视频参数_画面规格_配置文件 <> "" Then 视频参数 &= $"-profile {a.视频参数_画面规格_配置文件} "
            If a.视频参数_画面规格_场景优化 <> "" Then
                Select Case a.视频参数_编码器_具体编码
                    Case "hevc_amf", "h264_amf"
                        视频参数 &= $"-usage {a.视频参数_画面规格_场景优化} "
                    Case "libvpx-vp9"
                        视频参数 &= $"-deadline {a.视频参数_画面规格_场景优化} "
                    Case Else
                        视频参数 &= $"-tune {a.视频参数_画面规格_场景优化} "
                End Select
            End If
            If a.视频参数_画面视觉_像素格式 <> "" Then 视频参数 &= $"-pix_fmt {a.视频参数_画面视觉_像素格式} "
        End If

        If a.视频参数_启用色彩管理参数 Then
            Select Case a.视频参数_色彩管理_处理方式
                Case 0 '写入元数据并转换
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
                    If a.视频参数_画面视觉_像素格式 <> "" Then zscale &= $",format={a.视频参数_画面视觉_像素格式}"
                    视频滤镜参数集.Add(zscale)
                Case 1 '仅写入元数据
                    视频参数 &= $"-colorspace {a.视频参数_色彩管理_矩阵系数} "
                    视频参数 &= $"-color_primaries {a.视频参数_色彩管理_色域} "
                    视频参数 &= $"-color_trc {a.视频参数_色彩管理_传输特性} "
                    视频参数 &= $"-color_range {a.视频参数_色彩管理_范围} "
                    '视频滤镜参数集.Add($"setparams=colorspace={a.视频参数_色彩管理_矩阵系数}:color_primaries={a.视频参数_色彩管理_色域}:color_trc={a.视频参数_色彩管理_传输特性}:range={a.视频参数_色彩管理_范围}")
                Case 2 '仅转换
                    Dim zscale As String = $"zscale=matrix={a.视频参数_色彩管理_矩阵系数}:primaries={a.视频参数_色彩管理_色域}:transfer={a.视频参数_色彩管理_传输特性}"
                    Select Case a.视频参数_色彩管理_范围
                        Case "pc" : zscale &= ":range=full"
                        Case "tv" : zscale &= ":range=limited"
                    End Select
                    If a.视频参数_画面视觉_像素格式 <> "" Then zscale &= $",format={a.视频参数_画面视觉_像素格式}"
                    视频滤镜参数集.Add(zscale)
            End Select
        End If

        If a.视频参数_启用降噪参数 Then
            Select Case a.视频参数_降噪_方式
                Case "hqdn3d" : 视频滤镜参数集.Add($"hqdn3d=luma_spatial={a.视频参数_降噪_参数1}:chroma_spatial={a.视频参数_降噪_参数2}:luma_tmp={a.视频参数_降噪_参数3}:chroma_tmp={a.视频参数_降噪_参数4}")
                Case "nlmeans" : 视频滤镜参数集.Add($"nlmeans=s={a.视频参数_降噪_参数1}:p={a.视频参数_降噪_参数2}:pc={a.视频参数_降噪_参数3}:r={a.视频参数_降噪_参数4}")
                Case "atadenoise" : 视频滤镜参数集.Add($"atadenoise=0a={a.视频参数_降噪_参数1}:0b={a.视频参数_降噪_参数2}:1a={a.视频参数_降噪_参数3}:1b={a.视频参数_降噪_参数4}")
                Case "bm3d" : 视频滤镜参数集.Add($"bm3d=sigma={a.视频参数_降噪_参数1}:block={a.视频参数_降噪_参数2}:bstep={a.视频参数_降噪_参数3}:group={a.视频参数_降噪_参数4}")
            End Select
        End If

        If a.视频参数_启用锐化参数 Then
            视频滤镜参数集.Add($"unsharp=luma_msize_x={a.视频参数_锐化_水平尺寸}:luma_msize_y={a.视频参数_锐化_垂直尺寸}:luma_amount={a.视频参数_锐化_锐化强度}")
        End If

        If a.视频参数_启用逐行与隔行参数 Then
            Select Case a.视频参数_逐行与隔行_操作
                Case 0 : 视频滤镜参数集.Add($"yadif=0:-1:0")
                Case 1 : 视频滤镜参数集.Add($"yadif=0:0:0")
                Case 2 : 视频滤镜参数集.Add($"yadif=0:1:0")
                Case 3 : 视频滤镜参数集.Add($"tinterlace=4")
                Case 4 : 视频滤镜参数集.Add($"tinterlace=6")
            End Select
        End If

        '别问为什么把图片的缩放参数放在这里，我懒得改逻辑了
        If a.图片参数_启用分辨率参数 Then
            If a.图片参数_分辨率_宽度 <> "" AndAlso a.图片参数_分辨率_高度 <> "" Then
                视频滤镜参数集.Add($"scale={a.图片参数_分辨率_宽度}:{a.图片参数_分辨率_高度}")
            ElseIf a.图片参数_分辨率_宽度 <> "" AndAlso a.图片参数_分辨率_高度 = "" Then
                视频滤镜参数集.Add($"scale={a.图片参数_分辨率_宽度}:-2")
            ElseIf a.图片参数_分辨率_宽度 = "" AndAlso a.图片参数_分辨率_高度 <> "" Then
                视频滤镜参数集.Add($"scale=-2:{a.图片参数_分辨率_高度}")
            End If
        End If

        If a.视频参数_启用自定义滤镜参数 AndAlso a.视频参数_自定义滤镜 <> "" Then
            视频滤镜参数集.Add(a.视频参数_自定义滤镜)
        End If
        If 视频滤镜参数集.Count > 0 Then
            Dim vf As String = String.Join(",", 视频滤镜参数集)
            视频参数 &= $"-vf ""{vf}"" "
        End If
        If a.视频参数_启用自定义参数 AndAlso a.视频参数_自定义参数 <> "" Then
            视频参数 &= a.视频参数_自定义参数 & " "
        End If


        If a.音频参数_启用编码器参数 AndAlso a.音频参数_编码器_编码名称 <> "" Then
            Select Case a.音频参数_编码器_编码名称
                Case "复制流"
                    音频参数 &= $"-c:a copy "
                Case "AAC"
                    音频参数 &= $"-c:a aac "
                Case "LAME MP3"
                    音频参数 &= $"-c:a libmp3lame "
                Case "FLAC"
                    音频参数 &= $"-c:a flac "
                Case "ALAC"
                    音频参数 &= $"-c:a alac "
                Case "WAV 16bit"
                    音频参数 &= $"-c:a pcm_s16le "
                Case "WAV 24bit"
                    音频参数 &= $"-c:a pcm_s24le "
                Case "WAV 32bit Float"
                    音频参数 &= $"-c:a pcm_f32le "
                Case "Dolby Digital AC3"
                    音频参数 &= $"-c:a ac3 "
                Case "DTS Coherent Acoustics"
                    音频参数 &= $"-c:a dca "
                Case "Opus"
                    音频参数 &= $"-c:a libopus "
                Case "Vorbis"
                    音频参数 &= $"-c:a libvorbis "
                Case "True Audio"
                    音频参数 &= $"-c:a tta "
                Case "禁用"
                    音频参数 &= $"-an "
            End Select
        End If

        If a.音频参数_启用比特率参数 Then
            Select Case a.音频参数_比特率_控制方式
                Case "CBR"
                    If a.音频参数_比特率_基础 <> "" Then 音频参数 &= $"-b:a {a.音频参数_比特率_基础} "
                Case "VBR"
                    If a.音频参数_比特率_基础 <> "" Then 音频参数 &= $"-b:a {a.音频参数_比特率_基础} "
                    If a.音频参数_比特率_质量值 <> "" Then 音频参数 &= $"-q:a {a.音频参数_比特率_质量值} "
                Case "FLAC"
                    If a.音频参数_比特率_质量值 <> "" Then 音频参数 &= $"-compression_level {a.音频参数_比特率_质量值} "
            End Select
        End If

        If a.音频参数_启用声道数参数 Then
            If a.音频参数_声道数 <> "" Then 音频参数 &= $"-channel_layout {a.音频参数_声道数} "
        End If

        If a.音频参数_启用采样率参数 Then
            If a.音频参数_采样率 <> "" Then 音频参数 &= $"-ar {a.音频参数_采样率} "
        End If

        If a.音频参数_启用响度标准化参数 Then
            音频滤镜参数集.Add($"loudnorm=I={If(a.音频参数_响度标准化_目标响度 <> "", a.音频参数_响度标准化_目标响度, -16)}:LRA={If(a.音频参数_响度标准化_动态范围 <> "", a.音频参数_响度标准化_动态范围, 1)}:tp={If(a.音频参数_响度标准化_峰值电平 <> "", a.音频参数_响度标准化_峰值电平, -1)}")
        End If

        If 音频滤镜参数集.Count > 0 Then
            Dim vf As String = String.Join(",", 音频滤镜参数集)
            音频参数 &= $"-af ""{vf}"" "
        End If

        If a.音频参数_启用自定义参数 Then
            If a.音频参数_自定义参数 <> "" Then arg &= a.音频参数_自定义参数 & " "
        End If

        If a.图片参数_启用编码器参数 Then
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
        End If

        If a.图片参数_启用自定义参数 Then
            If a.图片参数_自定义参数 <> "" Then 视频参数 &= a.图片参数_自定义参数 & " "
        End If

        '=================================================================

        If a.流控制_启用保留其他视频流 AndAlso 视频参数 <> "" Then
            arg &= $"-map 0:v? -c:v copy "
            If a.流控制_将视频参数应用于指定流.Length > 0 Then
                For Each vi In a.流控制_将视频参数应用于指定流
                    arg &= $"-map -0:v:{vi} "
                Next
            Else
                If 视频参数 <> "" Then arg &= $"-map -0:v:0 "
            End If
        End If
        If a.流控制_将视频参数应用于指定流.Length > 0 Then
            If 视频参数 <> "" Then
                For Each vi In a.流控制_将视频参数应用于指定流
                    arg &= $"-map 0:v:{vi} {视频参数} "
                Next
            End If
        Else
            If 视频参数 <> "" Then arg &= $"-map 0:v:0 {视频参数} "
        End If
        '=================================================================

        If a.流控制_启用保留其他音频流 AndAlso 音频参数 <> "" Then
            arg &= $"-map 0:a? -c:a copy "
            If a.流控制_将音频参数应用于指定流.Length > 0 Then
                For Each vi In a.流控制_将音频参数应用于指定流
                    arg &= $"-map -0:a:{vi} "
                Next
            Else
                If 音频参数 <> "" Then arg &= $"-map -0:a:0 "
            End If
        End If
        If a.流控制_将音频参数应用于指定流.Length > 0 Then
            If 音频参数 <> "" Then
                For Each vi In a.流控制_将音频参数应用于指定流
                    arg &= $"-map 0:a:{vi} {音频参数} "
                Next
            End If
        Else
            If 音频参数 <> "" Then arg &= $"-map 0:a:0 {音频参数} "
        End If
        '=================================================================

        If a.流控制_启动保留内嵌字幕流 Then
            arg &= $"-map 0:s?  -c:s copy "
        End If

        If a.流控制_启用保留元数据 Then
            arg &= $"-map_metadata 0 "
        Else
            If 视频参数 <> "" OrElse 音频参数 <> "" Then
                arg &= $"-map_metadata -1 "
            End If
        End If

        If a.流控制_启用保留章节信息 Then
            arg &= $"-map_chapters 0 "
        Else
            If 视频参数 <> "" OrElse 音频参数 <> "" Then
                arg &= $"-map_chapters -1 "
            End If
        End If

        If 滤镜图参数集.Count > 0 Then
            Dim vf As String = String.Join(",", 滤镜图参数集)
            arg &= $"-filter_complex ""{vf}"" "
        End If
        '=================================================================
        Dim 将自动混流的SRT字幕 As String = Path.Combine(输入文件的文件夹, Path.GetFileNameWithoutExtension(输入文件) & ".srt")
        Dim 将自动混流的ASS字幕 As String = Path.Combine(输入文件的文件夹, Path.GetFileNameWithoutExtension(输入文件) & ".ass")
        Dim 将自动混流的SSA字幕 As String = Path.Combine(输入文件的文件夹, Path.GetFileNameWithoutExtension(输入文件) & ".ssa")
        Dim 是否自动混流了字幕 As Boolean = False
        If a.自动混流同名字幕 Then
            If FileIO.FileSystem.FileExists(将自动混流的SRT字幕) Then
                arg &= $"-i {""""}{将自动混流的SRT字幕}{""""} "
                是否自动混流了字幕 = True
            ElseIf FileIO.FileSystem.FileExists(将自动混流的ASS字幕) Then
                arg &= $"-i {""""}{将自动混流的ASS字幕}{""""} "
                是否自动混流了字幕 = True
            ElseIf FileIO.FileSystem.FileExists(将自动混流的SSA字幕) Then
                arg &= $"-i {""""}{将自动混流的SSA字幕}{""""} "
                是否自动混流了字幕 = True
            End If
            If 是否自动混流了字幕 Then
                arg &= $"-map 1 "
                If a.输出容器.Equals(".mp4", StringComparison.CurrentCultureIgnoreCase) Then
                    arg &= $" -c:s mov_text "
                End If
            End If
        End If

        If a.流控制_启用自定义其他参数 Then
            If a.流控制_自定义其他参数 <> "" Then arg &= $"{a.流控制_自定义其他参数} "
        End If

        arg &= $"{""""}{输出文件}{""""} -y "

        Return arg
    End Function


End Class
