Imports System.IO
Imports Sunny.UI
Public Class 预设管理
    Public Shared Sub 显示预设(a As 预设数据类型, ui As 界面_常规流程参数_V2)
        ui.UiTextBox输出容器.Text = a.输出容器
        If a.计算机名称 = Environment.MachineName AndAlso FileIO.FileSystem.DirectoryExists(a.输出位置) Then
            ui.UiComboBox输出目录.Text = "  " & a.输出位置
        Else
            ui.UiComboBox输出目录.SelectedIndex = 0
        End If

        ui.UiSwitch使用自动命名.Active = a.输出命名_使用自动命名
        ui.UiComboBox自动命名选项.SelectedIndex = a.输出命名_自动命名选项
        ui.UiSwitch不使用输出文件参数.Active = a.输出命名_不使用输出文件参数
        ui.UiTextBox开头文本.Text = a.输出命名_开头文本
        ui.UiTextBox替代文本.Text = a.输出命名_替代文本
        ui.UiTextBox结尾文本.Text = a.输出命名_结尾文本
        ui.UiCheckBox保留创建时间.Checked = a.输出命名_保留创建时间
        ui.UiCheckBox保留修改时间.Checked = a.输出命名_保留修改时间
        ui.UiCheckBox保留访问时间.Checked = a.输出命名_保留访问时间

        ui.UiComboBox解码器.Text = a.解码参数_解码器
        ui.UiTextBoxCPU解码线程数.Text = a.解码参数_CPU解码线程数
        ui.UiComboBox解码数据格式.Text = a.解码参数_解码数据格式
        ui.UiComboBox硬件加速解码参数名.Text = a.解码参数_指定硬件的参数名
        ui.UiTextBox硬件加速解码参数.Text = a.解码参数_指定硬件的参数

        ui.UiComboBox编码类别.Text = a.视频参数_编码器_类别
        ui.UiComboBox具体编码.Text = a.视频参数_编码器_具体编码
        ui.UiComboBox编码预设.Text = a.视频参数_编码器_编码预设
        ui.UiComboBox配置文件.Text = a.视频参数_编码器_配置文件
        ui.UiComboBox场景优化.Text = a.视频参数_编码器_场景优化
        ui.UiTextBoxgpu.Text = a.视频参数_编码器_gpu
        ui.UiTextBoxthreads.Text = a.视频参数_编码器_threads

        ui.UiComboBox分辨率.Text = a.视频参数_分辨率
        ui.UiTextBox分辨率自动计算宽度.Text = a.视频参数_分辨率自动计算_宽度
        ui.UiTextBox分辨率自动计算高度.Text = a.视频参数_分辨率自动计算_高度
        ui.UiTextBox画面裁剪滤镜参数.Text = a.视频参数_分辨率_裁剪滤镜参数
        ui.UiComboBox帧速率.Text = a.视频参数_帧速率
        ui.UiTextBox抽帧最大变化比例.Text = a.视频参数_帧速率_抽帧最大变化比例

        ui.插帧页面.UiTextBox要补到多少帧.Text = a.视频参数_插帧_目标帧率
        Select Case a.视频参数_插帧_插帧模式
            Case "dup" : ui.插帧页面.UiComboBox插帧模式.SelectedIndex = 1
            Case "blend" : ui.插帧页面.UiComboBox插帧模式.SelectedIndex = 2
            Case "mci" : ui.插帧页面.UiComboBox插帧模式.SelectedIndex = 3
        End Select
        Select Case a.视频参数_插帧_运动估计模式
            Case "bidir" : ui.插帧页面.UiComboBox运动估计模式.SelectedIndex = 1
            Case "bilat" : ui.插帧页面.UiComboBox运动估计模式.SelectedIndex = 2
        End Select
        Select Case a.视频参数_插帧_运动估计算法
            Case "esa" : ui.插帧页面.UiComboBox运动估计算法.SelectedIndex = 1
            Case "tss" : ui.插帧页面.UiComboBox运动估计算法.SelectedIndex = 2
            Case "tdls" : ui.插帧页面.UiComboBox运动估计算法.SelectedIndex = 3
            Case "ntss" : ui.插帧页面.UiComboBox运动估计算法.SelectedIndex = 4
            Case "fss" : ui.插帧页面.UiComboBox运动估计算法.SelectedIndex = 5
            Case "ds" : ui.插帧页面.UiComboBox运动估计算法.SelectedIndex = 6
            Case "hexbs" : ui.插帧页面.UiComboBox运动估计算法.SelectedIndex = 7
            Case "epzs" : ui.插帧页面.UiComboBox运动估计算法.SelectedIndex = 8
            Case "umh" : ui.插帧页面.UiComboBox运动估计算法.SelectedIndex = 9
        End Select
        Select Case a.视频参数_插帧_运动补偿模式
            Case "obmc" : ui.插帧页面.UiComboBox运动补偿模式.SelectedIndex = 1
            Case "aobmc" : ui.插帧页面.UiComboBox运动补偿模式.SelectedIndex = 2
        End Select
        ui.插帧页面.UiCheckBox可变块大小的运动补偿.Checked = a.视频参数_插帧_可变块大小的运动补偿
        ui.插帧页面.UiTextBox块大小.Text = a.视频参数_插帧_块大小
        ui.插帧页面.UiTextBox搜索范围.Text = a.视频参数_插帧_搜索范围
        ui.插帧页面.UiTextBox场景变化检测强度.Text = a.视频参数_插帧_场景变化检测强度

        ui.动态模糊页面.UiTextBox降低帧率.Text = a.视频参数_帧混合_指定帧率
        Select Case a.视频参数_帧混合_混合模式
            Case "average" : ui.动态模糊页面.UiComboBox混合算法.SelectedIndex = 1
            Case "difference" : ui.动态模糊页面.UiComboBox混合算法.SelectedIndex = 2
            Case "and" : ui.动态模糊页面.UiComboBox混合算法.SelectedIndex = 3
            Case "or" : ui.动态模糊页面.UiComboBox混合算法.SelectedIndex = 4
            Case "xor" : ui.动态模糊页面.UiComboBox混合算法.SelectedIndex = 5
            Case "add" : ui.动态模糊页面.UiComboBox混合算法.SelectedIndex = 6
            Case "multiply" : ui.动态模糊页面.UiComboBox混合算法.SelectedIndex = 7
        End Select
        ui.动态模糊页面.UiTextBox混合比例.Text = a.视频参数_帧混合_混合比例

        ui.超分页面.UiTextBox超分宽度.Text = a.视频参数_超分_目标宽度
        ui.超分页面.UiTextBox超分高度.Text = a.视频参数_超分_目标高度
        ui.超分页面.UiComboBox上采样算法.Text = a.视频参数_超分_上采样算法
        ui.超分页面.UiComboBox下采样算法.Text = a.视频参数_超分_下采样算法
        ui.超分页面.UiTextBox抗振铃强度.Text = a.视频参数_超分_抗振铃强度
        ui.超分页面.ListView1.Items.Clear()
        For Each c In a.视频参数_超分_着色器列表
            ui.超分页面.ListView1.Items.Add(c)
        Next

        Select Case a.视频参数_比特率_控制方式
            Case "CRF" : ui.UiComboBox全局质量控制方式.SelectedIndex = 1
            Case "VBR" : ui.UiComboBox全局质量控制方式.SelectedIndex = 2
            Case "VBR HQ" : ui.UiComboBox全局质量控制方式.SelectedIndex = 3
            Case "CQP" : ui.UiComboBox全局质量控制方式.SelectedIndex = 4
            Case "CBR" : ui.UiComboBox全局质量控制方式.SelectedIndex = 5
        End Select
        Select Case a.视频参数_质量控制_参数名
            Case "crf" : ui.UiComboBox全局质量控制参数.SelectedIndex = 1
            Case "cq" : ui.UiComboBox全局质量控制参数.SelectedIndex = 2
            Case "qp" : ui.UiComboBox全局质量控制参数.SelectedIndex = 3
            Case "global_quality" : ui.UiComboBox全局质量控制参数.SelectedIndex = 4
        End Select
        ui.UiTextBox全局质量控制值.Text = a.视频参数_质量控制_值
        ui.UiTextBox基础比特率.Text = a.视频参数_比特率_基础
        ui.UiTextBox最低比特率.Text = a.视频参数_比特率_最低值
        ui.UiTextBox最高比特率.Text = a.视频参数_比特率_最高值
        ui.UiTextBox比特率缓冲区.Text = a.视频参数_比特率_缓冲区
        ui.清除全部进阶质量控制()
        For Each c In a.视频参数_质量控制_进阶参数集
            ui.创建进阶质量控制项(c)
        Next

        ui.UiComboBox像素格式.Text = a.视频参数_色彩管理_像素格式
        ui.UiComboBox色彩空间滤镜选择.Text = a.视频参数_色彩管理_滤镜选择
        ui.UiComboBox矩阵系数.Text = a.视频参数_色彩管理_矩阵系数
        ui.UiComboBox色域.Text = a.视频参数_色彩管理_色域
        ui.UiComboBox传输特性.Text = a.视频参数_色彩管理_传输特性
        Select Case a.视频参数_色彩管理_范围
            Case "tv" : ui.UiComboBox色彩范围.SelectedIndex = 1
            Case "pc" : ui.UiComboBox色彩范围.SelectedIndex = 2
        End Select
        ui.UiComboBox色调映射算法.Text = a.视频参数_色彩管理_色调映射算法
        ui.UiComboBox色彩管理处理方式.SelectedIndex = a.视频参数_色彩管理_处理方式
        ui.UiTextBox简易调色亮度.Text = a.视频参数_色彩管理_亮度
        ui.UiTextBox简易调色对比度.Text = a.视频参数_色彩管理_对比度
        ui.UiTextBox简易调色饱和度.Text = a.视频参数_色彩管理_饱和度
        ui.UiTextBox简易调色伽马.Text = a.视频参数_色彩管理_伽马

        Select Case a.视频参数_降噪_方式
            Case "hqdn3d" : ui.UiComboBox降噪方式.SelectedIndex = 1
            Case "nlmeans" : ui.UiComboBox降噪方式.SelectedIndex = 2
            Case "atadenoise" : ui.UiComboBox降噪方式.SelectedIndex = 3
            Case "bm3d" : ui.UiComboBox降噪方式.SelectedIndex = 4
            Case "avs" : ui.UiComboBox降噪方式.SelectedIndex = 5
            Case Else : ui.UiComboBox降噪方式.Text = ""
        End Select
        ui.UiTextBox降噪参数1.Text = a.视频参数_降噪_参数1
        ui.UiTextBox降噪参数2.Text = a.视频参数_降噪_参数2
        ui.UiTextBox降噪参数3.Text = a.视频参数_降噪_参数3
        ui.UiTextBox降噪参数4.Text = a.视频参数_降噪_参数4
        ui.UiTextBox锐化水平尺寸.Text = a.视频参数_锐化_水平尺寸
        ui.UiTextBox锐化垂直尺寸.Text = a.视频参数_锐化_垂直尺寸
        ui.UiTextBox锐化强度.Text = a.视频参数_锐化_锐化强度
        ui.UiComboBox逐行与隔行处理方式.SelectedIndex = a.视频参数_逐行与隔行_操作
        ui.UiComboBox角度翻转.SelectedIndex = a.视频参数_画面翻转_角度翻转
        ui.UiComboBox镜像翻转.SelectedIndex = a.视频参数_画面翻转_镜像翻转

        Select Case a.音频参数_编码器_具体编码
            Case "copy" : ui.UiComboBox音频编码器.SelectedIndex = 1
            Case "-an" : ui.UiComboBox音频编码器.SelectedIndex = 2
            Case "aac" : ui.UiComboBox音频编码器.SelectedIndex = 3
            Case "libmp3lame" : ui.UiComboBox音频编码器.SelectedIndex = 4
            Case "libopus" : ui.UiComboBox音频编码器.SelectedIndex = 5
            Case "flac" : ui.UiComboBox音频编码器.SelectedIndex = 6
            Case "alac" : ui.UiComboBox音频编码器.SelectedIndex = 7
            Case "pcm_s16le" : ui.UiComboBox音频编码器.SelectedIndex = 8
            Case "pcm_s24le" : ui.UiComboBox音频编码器.SelectedIndex = 9
            Case "pcm_s32le" : ui.UiComboBox音频编码器.SelectedIndex = 10
            Case "pcm_s64le" : ui.UiComboBox音频编码器.SelectedIndex = 11
            Case "ac3" : ui.UiComboBox音频编码器.SelectedIndex = 12
            Case "eac3" : ui.UiComboBox音频编码器.SelectedIndex = 13
            Case "dca" : ui.UiComboBox音频编码器.SelectedIndex = 14
            Case "truehd" : ui.UiComboBox音频编码器.SelectedIndex = 15
            Case "tta" : ui.UiComboBox音频编码器.SelectedIndex = 16
            Case "libvorbis" : ui.UiComboBox音频编码器.SelectedIndex = 17
            Case "real_144" : ui.UiComboBox音频编码器.SelectedIndex = 18
            Case "wavpack" : ui.UiComboBox音频编码器.SelectedIndex = 19
            Case "libtwolame" : ui.UiComboBox音频编码器.SelectedIndex = 20
            Case "libopencore_amrnb" : ui.UiComboBox音频编码器.SelectedIndex = 21
            Case "libvo_amrwbenc" : ui.UiComboBox音频编码器.SelectedIndex = 22
            Case Else : ui.UiComboBox音频编码器.Text = ""
        End Select
        ui.UiComboBox音频比特率.Text = a.音频参数_比特率
        ui.UiComboBox音频质量参数.Text = a.音频参数_质量参数名
        ui.UiTextBox音频质量值.Text = a.音频参数_质量值
        ui.UiComboBox声道布局.Text = a.音频参数_声道数
        ui.UiComboBox采样率.Text = a.音频参数_采样率
        ui.UiTextBox响度标准化目标响度.Text = a.音频参数_响度标准化_目标响度
        ui.UiTextBox响度标准化动态范围.Text = a.音频参数_响度标准化_动态范围
        ui.UiTextBox响度标准化峰值电平.Text = a.音频参数_响度标准化_峰值电平

        Select Case a.图片参数_编码器_编码名称
            Case "PNG" : ui.UiComboBox图片编码器.SelectedIndex = 1
            Case "APNG" : ui.UiComboBox图片编码器.SelectedIndex = 2
            Case "JPEG" : ui.UiComboBox图片编码器.SelectedIndex = 3
            Case "WEBP有损" : ui.UiComboBox图片编码器.SelectedIndex = 4
            Case "WEBP动图" : ui.UiComboBox图片编码器.SelectedIndex = 5
            Case "AVIF静图" : ui.UiComboBox图片编码器.SelectedIndex = 6
            Case "AVIF动图" : ui.UiComboBox图片编码器.SelectedIndex = 7
            Case "GIF" : ui.UiComboBox图片编码器.SelectedIndex = 8
            Case "BMP" : ui.UiComboBox图片编码器.SelectedIndex = 9
            Case "OpenJPEG" : ui.UiComboBox图片编码器.SelectedIndex = 10
            Case "JPEGLS" : ui.UiComboBox图片编码器.SelectedIndex = 11
            Case "HDR" : ui.UiComboBox图片编码器.SelectedIndex = 12
            Case "TIFF" : ui.UiComboBox图片编码器.SelectedIndex = 13
            Case "DPX" : ui.UiComboBox图片编码器.SelectedIndex = 14
            Case "OpenEXR" : ui.UiComboBox图片编码器.SelectedIndex = 15
        End Select
        ui.UiTextBox图片编码器质量.Text = a.图片参数_编码器_质量值

        ui.UiTextBox自定义视频滤镜.Text = a.自定义参数_视频滤镜
        ui.UiTextBox自定义音频滤镜.Text = a.自定义参数_音频滤镜
        ui.UiTextBoxfilter_complex.Text = a.自定义参数_filter_complex
        ui.UiTextBox自定义视频参数.Text = a.自定义参数_视频参数
        ui.UiTextBox自定义音频参数.Text = a.自定义参数_音频参数
        ui.UiTextBox开头参数.Text = a.自定义参数_开头参数
        ui.UiTextBox之前参数.Text = a.自定义参数_之前参数
        ui.UiTextBox之后参数.Text = a.自定义参数_之后参数
        ui.UiTextBox最后参数.Text = a.自定义参数_最后参数

        ui.UiComboBox剪辑方法.SelectedIndex = a.剪辑区间_方法
        ui.UiTextBox快速剪辑入点.Text = a.剪辑区间_入点
        ui.UiTextBox快速剪辑出点.Text = a.剪辑区间_出点
        ui.UiComboBox剪辑向前解码多久秒.Text = a.剪辑区间_向前解码多久秒

        ui.UiTextBox将视频参数用于这些流.Text = String.Join(",", a.流控制_将视频参数应用于指定流)
        ui.UiCheckBox保留其他视频流.Checked = a.流控制_启用保留其他视频流
        ui.UiTextBox将音频参数用于这些流.Text = String.Join(",", a.流控制_将音频参数应用于指定流)
        ui.UiCheckBox保留其他音频流.Checked = a.流控制_启用保留其他音频流
        ui.UiCheckBox保留内嵌字幕流.Checked = a.流控制_启用保留内嵌字幕流
        ui.UiComboBox元数据选项.SelectedIndex = a.流控制_元数据选项
        ui.UiComboBox章节选项.SelectedIndex = a.流控制_章节选项
        ui.UiComboBox附件选项.SelectedIndex = a.流控制_附件选项
        ui.UiCheckBox自动混流同名字幕文件.Checked = a.流控制_启用自动混流同名字幕文件

    End Sub

    Shared ReadOnly separator As String() = {","}

    Public Shared Sub 储存预设(ByRef a As 预设数据类型, ui As 界面_常规流程参数_V2)
        a.输出容器 = ui.UiTextBox输出容器.Text
        If ui.UiCheckBox额外保存信息.Checked AndAlso FileIO.FileSystem.DirectoryExists(ui.UiComboBox输出目录.Text.Trim) Then
            a.计算机名称 = Environment.MachineName
            a.输出位置 = ui.UiComboBox输出目录.Text.Trim
        End If

        a.输出命名_使用自动命名 = ui.UiSwitch使用自动命名.Active
        a.输出命名_自动命名选项 = ui.UiComboBox自动命名选项.SelectedIndex
        a.输出命名_不使用输出文件参数 = ui.UiSwitch不使用输出文件参数.Active
        a.输出命名_开头文本 = ui.UiTextBox开头文本.Text
        a.输出命名_替代文本 = ui.UiTextBox替代文本.Text
        a.输出命名_结尾文本 = ui.UiTextBox结尾文本.Text
        a.输出命名_保留创建时间 = ui.UiCheckBox保留创建时间.Checked
        a.输出命名_保留修改时间 = ui.UiCheckBox保留修改时间.Checked
        a.输出命名_保留访问时间 = ui.UiCheckBox保留访问时间.Checked

        a.解码参数_解码器 = ui.UiComboBox解码器.Text
        a.解码参数_CPU解码线程数 = ui.UiTextBoxCPU解码线程数.Text
        a.解码参数_解码数据格式 = ui.UiComboBox解码数据格式.Text
        a.解码参数_指定硬件的参数名 = ui.UiComboBox硬件加速解码参数名.Text
        a.解码参数_指定硬件的参数 = ui.UiTextBox硬件加速解码参数.Text

        a.视频参数_编码器_类别 = ui.UiComboBox编码类别.Text
        a.视频参数_编码器_具体编码 = ui.UiComboBox具体编码.Text
        a.视频参数_编码器_编码预设 = ui.UiComboBox编码预设.Text
        a.视频参数_编码器_配置文件 = ui.UiComboBox配置文件.Text
        a.视频参数_编码器_场景优化 = ui.UiComboBox场景优化.Text
        a.视频参数_编码器_gpu = ui.UiTextBoxgpu.Text
        a.视频参数_编码器_threads = ui.UiTextBoxthreads.Text

        a.视频参数_分辨率 = ui.UiComboBox分辨率.Text
        a.视频参数_分辨率自动计算_宽度 = ui.UiTextBox分辨率自动计算宽度.Text
        a.视频参数_分辨率自动计算_高度 = ui.UiTextBox分辨率自动计算高度.Text
        a.视频参数_分辨率_裁剪滤镜参数 = ui.UiTextBox画面裁剪滤镜参数.Text
        a.视频参数_帧速率 = ui.UiComboBox帧速率.Text
        a.视频参数_帧速率_抽帧最大变化比例 = ui.UiTextBox抽帧最大变化比例.Text

        a.视频参数_插帧_目标帧率 = ui.插帧页面.UiTextBox要补到多少帧.Text
        Select Case ui.插帧页面.UiComboBox插帧模式.SelectedIndex
            Case 1 : a.视频参数_插帧_插帧模式 = "dup"
            Case 2 : a.视频参数_插帧_插帧模式 = "blend"
            Case 3 : a.视频参数_插帧_插帧模式 = "mci"
        End Select
        Select Case ui.插帧页面.UiComboBox运动估计模式.SelectedIndex
            Case 1 : a.视频参数_插帧_运动估计模式 = "bidir"
            Case 2 : a.视频参数_插帧_运动估计模式 = "bilat"
        End Select
        Select Case ui.插帧页面.UiComboBox运动估计算法.SelectedIndex
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
        Select Case ui.插帧页面.UiComboBox运动补偿模式.SelectedIndex
            Case 1 : a.视频参数_插帧_运动补偿模式 = "obmc"
            Case 2 : a.视频参数_插帧_运动补偿模式 = "aobmc"
        End Select
        a.视频参数_插帧_可变块大小的运动补偿 = ui.插帧页面.UiCheckBox可变块大小的运动补偿.Checked
        a.视频参数_插帧_块大小 = ui.插帧页面.UiTextBox块大小.Text
        a.视频参数_插帧_搜索范围 = ui.插帧页面.UiTextBox搜索范围.Text
        a.视频参数_插帧_场景变化检测强度 = ui.插帧页面.UiTextBox场景变化检测强度.Text

        a.视频参数_帧混合_指定帧率 = ui.动态模糊页面.UiTextBox降低帧率.Text
        Select Case ui.动态模糊页面.UiComboBox混合算法.SelectedIndex
            Case 1 : a.视频参数_帧混合_混合模式 = "average"
            Case 2 : a.视频参数_帧混合_混合模式 = "difference"
            Case 3 : a.视频参数_帧混合_混合模式 = "and"
            Case 4 : a.视频参数_帧混合_混合模式 = "or"
            Case 5 : a.视频参数_帧混合_混合模式 = "xor"
            Case 6 : a.视频参数_帧混合_混合模式 = "add"
            Case 7 : a.视频参数_帧混合_混合模式 = "multiply"
        End Select
        a.视频参数_帧混合_混合比例 = ui.动态模糊页面.UiTextBox混合比例.Text

        a.视频参数_超分_目标宽度 = ui.超分页面.UiTextBox超分宽度.Text
        a.视频参数_超分_目标高度 = ui.超分页面.UiTextBox超分高度.Text
        a.视频参数_超分_上采样算法 = ui.超分页面.UiComboBox上采样算法.Text
        a.视频参数_超分_下采样算法 = ui.超分页面.UiComboBox下采样算法.Text
        a.视频参数_超分_抗振铃强度 = ui.超分页面.UiTextBox抗振铃强度.Text
        a.视频参数_超分_着色器列表.Clear()
        For Each c As ListViewItem In ui.超分页面.ListView1.Items
            a.视频参数_超分_着色器列表.Add(c.Text)
        Next

        Select Case ui.UiComboBox全局质量控制方式.SelectedIndex
            Case 1 : a.视频参数_比特率_控制方式 = "CRF"
            Case 2 : a.视频参数_比特率_控制方式 = "VBR"
            Case 3 : a.视频参数_比特率_控制方式 = "VBR HQ"
            Case 4 : a.视频参数_比特率_控制方式 = "CQP"
            Case 5 : a.视频参数_比特率_控制方式 = "CBR"
        End Select
        Select Case ui.UiComboBox全局质量控制参数.SelectedIndex
            Case 1 : a.视频参数_质量控制_参数名 = "crf"
            Case 2 : a.视频参数_质量控制_参数名 = "cq"
            Case 3 : a.视频参数_质量控制_参数名 = "qp"
            Case 4 : a.视频参数_质量控制_参数名 = "global_quality"
        End Select
        a.视频参数_质量控制_值 = ui.UiTextBox全局质量控制值.Text
        a.视频参数_比特率_基础 = ui.UiTextBox基础比特率.Text
        a.视频参数_比特率_最低值 = ui.UiTextBox最低比特率.Text
        a.视频参数_比特率_最高值 = ui.UiTextBox最高比特率.Text
        a.视频参数_比特率_缓冲区 = ui.UiTextBox比特率缓冲区.Text
        For Each c In ui.FlowLayoutPanel1.Controls
            If c.GetType IsNot GetType(UITextBox) Then Continue For
            a.视频参数_质量控制_进阶参数集.Add(c.Text)
        Next

        a.视频参数_色彩管理_像素格式 = ui.UiComboBox像素格式.Text
        a.视频参数_色彩管理_滤镜选择 = ui.UiComboBox色彩空间滤镜选择.Text
        a.视频参数_色彩管理_矩阵系数 = ui.UiComboBox矩阵系数.Text
        a.视频参数_色彩管理_色域 = ui.UiComboBox色域.Text
        a.视频参数_色彩管理_传输特性 = ui.UiComboBox传输特性.Text
        Select Case ui.UiComboBox色彩范围.SelectedIndex
            Case 1 : a.视频参数_色彩管理_范围 = "tv"
            Case 2 : a.视频参数_色彩管理_范围 = "pc"
        End Select
        a.视频参数_色彩管理_色调映射算法 = ui.UiComboBox色调映射算法.Text
        a.视频参数_色彩管理_处理方式 = ui.UiComboBox色彩管理处理方式.SelectedIndex
        a.视频参数_色彩管理_亮度 = ui.UiTextBox简易调色亮度.Text
        a.视频参数_色彩管理_对比度 = ui.UiTextBox简易调色对比度.Text
        a.视频参数_色彩管理_饱和度 = ui.UiTextBox简易调色饱和度.Text
        a.视频参数_色彩管理_伽马 = ui.UiTextBox简易调色伽马.Text

        Select Case ui.UiComboBox降噪方式.SelectedIndex
            Case 1 : a.视频参数_降噪_方式 = "hqdn3d"
            Case 2 : a.视频参数_降噪_方式 = "nlmeans"
            Case 3 : a.视频参数_降噪_方式 = "atadenoise"
            Case 4 : a.视频参数_降噪_方式 = "bm3d"
            Case 5 : a.视频参数_降噪_方式 = "avs"
        End Select
        a.视频参数_降噪_参数1 = ui.UiTextBox降噪参数1.Text
        a.视频参数_降噪_参数2 = ui.UiTextBox降噪参数2.Text
        a.视频参数_降噪_参数3 = ui.UiTextBox降噪参数3.Text
        a.视频参数_降噪_参数4 = ui.UiTextBox降噪参数4.Text
        a.视频参数_锐化_水平尺寸 = ui.UiTextBox锐化水平尺寸.Text
        a.视频参数_锐化_垂直尺寸 = ui.UiTextBox锐化垂直尺寸.Text
        a.视频参数_锐化_锐化强度 = ui.UiTextBox锐化强度.Text
        a.视频参数_逐行与隔行_操作 = ui.UiComboBox逐行与隔行处理方式.SelectedIndex
        a.视频参数_画面翻转_角度翻转 = ui.UiComboBox角度翻转.SelectedIndex
        a.视频参数_画面翻转_镜像翻转 = ui.UiComboBox镜像翻转.SelectedIndex

        Select Case ui.UiComboBox音频编码器.SelectedIndex
            Case 1 : a.音频参数_编码器_具体编码 = "copy"
            Case 2 : a.音频参数_编码器_具体编码 = "-an"
            Case 3 : a.音频参数_编码器_具体编码 = "aac"
            Case 4 : a.音频参数_编码器_具体编码 = "libmp3lame"
            Case 5 : a.音频参数_编码器_具体编码 = "libopus"
            Case 6 : a.音频参数_编码器_具体编码 = "flac"
            Case 7 : a.音频参数_编码器_具体编码 = "alac"
            Case 8 : a.音频参数_编码器_具体编码 = "pcm_s16le"
            Case 9 : a.音频参数_编码器_具体编码 = "pcm_s24le"
            Case 10 : a.音频参数_编码器_具体编码 = "pcm_s32le"
            Case 11 : a.音频参数_编码器_具体编码 = "pcm_s64le"
            Case 12 : a.音频参数_编码器_具体编码 = "ac3"
            Case 13 : a.音频参数_编码器_具体编码 = "eac3"
            Case 14 : a.音频参数_编码器_具体编码 = "dca"
            Case 15 : a.音频参数_编码器_具体编码 = "truehd"
            Case 16 : a.音频参数_编码器_具体编码 = "tta"
            Case 17 : a.音频参数_编码器_具体编码 = "libvorbis"
            Case 18 : a.音频参数_编码器_具体编码 = "real_144"
            Case 19 : a.音频参数_编码器_具体编码 = "wavpack"
            Case 20 : a.音频参数_编码器_具体编码 = "libtwolame"
            Case 21 : a.音频参数_编码器_具体编码 = "libopencore_amrnb"
            Case 22 : a.音频参数_编码器_具体编码 = "libvo_amrwbenc"
            Case Else : a.音频参数_编码器_具体编码 = ""
        End Select

        a.音频参数_比特率 = ui.UiComboBox音频比特率.Text
        a.音频参数_质量参数名 = ui.UiComboBox音频质量参数.Text
        a.音频参数_质量值 = ui.UiTextBox音频质量值.Text
        a.音频参数_声道数 = ui.UiComboBox声道布局.Text
        a.音频参数_采样率 = ui.UiComboBox采样率.Text
        a.音频参数_响度标准化_目标响度 = ui.UiTextBox响度标准化目标响度.Text
        a.音频参数_响度标准化_动态范围 = ui.UiTextBox响度标准化动态范围.Text
        a.音频参数_响度标准化_峰值电平 = ui.UiTextBox响度标准化峰值电平.Text

        a.图片参数_编码器_编码名称 = ui.UiComboBox图片编码器.Text
        Select Case ui.UiComboBox图片编码器.SelectedIndex
            Case 1 : a.图片参数_编码器_编码名称 = "PNG"
            Case 2 : a.图片参数_编码器_编码名称 = "APNG"
            Case 3 : a.图片参数_编码器_编码名称 = "JPEG"
            Case 4 : a.图片参数_编码器_编码名称 = "WEBP有损"
            Case 5 : a.图片参数_编码器_编码名称 = "WEBP动图"
            Case 6 : a.图片参数_编码器_编码名称 = "AVIF静图"
            Case 7 : a.图片参数_编码器_编码名称 = "AVIF动图"
            Case 8 : a.图片参数_编码器_编码名称 = "GIF"
            Case 9 : a.图片参数_编码器_编码名称 = "BMP"
            Case 10 : a.图片参数_编码器_编码名称 = "OpenJPEG"
            Case 11 : a.图片参数_编码器_编码名称 = "JPEGLS"
            Case 12 : a.图片参数_编码器_编码名称 = "HDR"
            Case 13 : a.图片参数_编码器_编码名称 = "TIFF"
            Case 14 : a.图片参数_编码器_编码名称 = "DPX"
            Case 15 : a.图片参数_编码器_编码名称 = "OpenEXR"
        End Select
        a.图片参数_编码器_质量值 = ui.UiTextBox图片编码器质量.Text

        a.自定义参数_视频滤镜 = ui.UiTextBox自定义视频滤镜.Text.Replace(vbCrLf, " ").Replace(vbLf, " ").Replace(vbCr, " ")
        a.自定义参数_音频滤镜 = ui.UiTextBox自定义音频滤镜.Text.Replace(vbCrLf, " ").Replace(vbLf, " ").Replace(vbCr, " ")
        a.自定义参数_filter_complex = ui.UiTextBoxfilter_complex.Text.Replace(vbCrLf, " ").Replace(vbLf, " ").Replace(vbCr, " ")
        a.自定义参数_视频参数 = ui.UiTextBox自定义视频参数.Text.Replace(vbCrLf, " ").Replace(vbLf, " ").Replace(vbCr, " ")
        a.自定义参数_音频参数 = ui.UiTextBox自定义音频参数.Text.Replace(vbCrLf, " ").Replace(vbLf, " ").Replace(vbCr, " ")
        a.自定义参数_开头参数 = ui.UiTextBox开头参数.Text.Replace(vbCrLf, " ").Replace(vbLf, " ").Replace(vbCr, " ")
        a.自定义参数_之前参数 = ui.UiTextBox之前参数.Text.Replace(vbCrLf, " ").Replace(vbLf, " ").Replace(vbCr, " ")
        a.自定义参数_之后参数 = ui.UiTextBox之后参数.Text.Replace(vbCrLf, " ").Replace(vbLf, " ").Replace(vbCr, " ")
        a.自定义参数_最后参数 = ui.UiTextBox最后参数.Text.Replace(vbCrLf, " ").Replace(vbLf, " ").Replace(vbCr, " ")
        a.自定义参数_完全自己写 = ui.UiTextBox完全自己写参数.Text.Replace(vbCrLf, " ").Replace(vbLf, " ").Replace(vbCr, " ")

        a.剪辑区间_方法 = ui.UiComboBox剪辑方法.SelectedIndex
        a.剪辑区间_入点 = ui.UiTextBox快速剪辑入点.Text
        a.剪辑区间_出点 = ui.UiTextBox快速剪辑出点.Text
        a.剪辑区间_向前解码多久秒 = ui.UiComboBox剪辑向前解码多久秒.Text

        a.流控制_将视频参数应用于指定流 = ui.UiTextBox将视频参数用于这些流.Text.Replace("-", "").Split(separator, StringSplitOptions.RemoveEmptyEntries)
        a.流控制_启用保留其他视频流 = ui.UiCheckBox保留其他视频流.Checked
        a.流控制_将音频参数应用于指定流 = ui.UiTextBox将音频参数用于这些流.Text.Replace("-", "").Split(separator, StringSplitOptions.RemoveEmptyEntries)
        a.流控制_启用保留其他音频流 = ui.UiCheckBox保留其他音频流.Checked
        a.流控制_启用保留内嵌字幕流 = ui.UiCheckBox保留内嵌字幕流.Checked
        a.流控制_元数据选项 = ui.UiComboBox元数据选项.SelectedIndex
        a.流控制_章节选项 = ui.UiComboBox章节选项.SelectedIndex
        a.流控制_附件选项 = ui.UiComboBox附件选项.SelectedIndex
        a.流控制_启用自动混流同名字幕文件 = ui.UiCheckBox自动混流同名字幕文件.Checked
    End Sub

    Public Shared Sub 重置全部包含在预设中的设置(ui As 界面_常规流程参数_V2)
        ui.UiTextBox输出容器.Text = ""
        ui.UiComboBox输出目录.SelectedIndex = 0

        ui.UiSwitch使用自动命名.Active = True
        ui.UiComboBox自动命名选项.SelectedIndex = 0
        ui.UiSwitch不使用输出文件参数.Active = False
        ui.UiTextBox开头文本.Text = ""
        ui.UiTextBox替代文本.Text = ""
        ui.UiTextBox结尾文本.Text = ""
        ui.UiCheckBox保留创建时间.Checked = False
        ui.UiCheckBox保留修改时间.Checked = False
        ui.UiCheckBox保留访问时间.Checked = False

        ui.UiComboBox解码器.Text = ""
        ui.UiTextBoxCPU解码线程数.Text = ""
        ui.UiComboBox解码数据格式.Text = ""
        ui.UiComboBox硬件加速解码参数名.Text = ""
        ui.UiTextBox硬件加速解码参数.Text = ""

        ui.UiComboBox编码类别.Text = ""
        ui.UiComboBox具体编码.Text = ""
        ui.UiComboBox编码预设.Text = ""
        ui.UiComboBox配置文件.Text = ""
        ui.UiComboBox场景优化.Text = ""
        ui.UiTextBoxgpu.Text = ""
        ui.UiTextBoxthreads.Text = ""

        ui.UiComboBox分辨率.Text = ""
        ui.UiTextBox分辨率自动计算宽度.Text = ""
        ui.UiTextBox分辨率自动计算高度.Text = ""
        ui.UiTextBox画面裁剪滤镜参数.Text = ""
        ui.UiComboBox帧速率.Text = ""
        ui.UiTextBox抽帧最大变化比例.Text = ""

        ui.插帧页面.重置所有选项()
        ui.动态模糊页面.重置所有选项()
        ui.超分页面.重置所有选项()

        ui.UiComboBox全局质量控制方式.SelectedIndex = -1
        ui.UiComboBox全局质量控制参数.SelectedIndex = -1
        ui.UiTextBox全局质量控制值.Text = ""
        ui.UiTextBox基础比特率.Text = ""
        ui.UiTextBox最低比特率.Text = ""
        ui.UiTextBox最高比特率.Text = ""
        ui.UiTextBox比特率缓冲区.Text = ""
        ui.清除全部进阶质量控制()

        ui.UiComboBox像素格式.Text = ""
        ui.UiComboBox色彩空间滤镜选择.Text = ""
        ui.UiComboBox矩阵系数.Text = ""
        ui.UiComboBox色域.Text = ""
        ui.UiComboBox传输特性.Text = ""
        ui.UiComboBox色彩范围.SelectedIndex = -1
        ui.UiComboBox色调映射算法.Text = ""
        ui.UiComboBox色彩管理处理方式.SelectedIndex = -1
        ui.UiComboBox降噪方式.SelectedIndex = -1
        ui.UiTextBox简易调色亮度.Text = ""
        ui.UiTextBox简易调色对比度.Text = ""
        ui.UiTextBox简易调色饱和度.Text = ""
        ui.UiTextBox简易调色伽马.Text = ""

        ui.UiTextBox降噪参数1.Text = ""
        ui.UiTextBox降噪参数2.Text = ""
        ui.UiTextBox降噪参数3.Text = ""
        ui.UiTextBox降噪参数4.Text = ""
        ui.UiTextBox锐化水平尺寸.Text = ""
        ui.UiTextBox锐化垂直尺寸.Text = ""
        ui.UiTextBox锐化强度.Text = ""
        ui.UiComboBox逐行与隔行处理方式.SelectedIndex = -1
        ui.UiComboBox角度翻转.SelectedIndex = -1
        ui.UiComboBox镜像翻转.SelectedIndex = -1

        ui.UiComboBox音频编码器.Text = ""
        ui.UiComboBox音频比特率.Text = ""
        ui.UiComboBox音频质量参数.Text = ""
        ui.UiTextBox音频质量值.Text = ""
        ui.UiComboBox声道布局.Text = ""
        ui.UiComboBox采样率.Text = ""
        ui.UiTextBox响度标准化目标响度.Text = ""
        ui.UiTextBox响度标准化动态范围.Text = ""
        ui.UiTextBox响度标准化峰值电平.Text = ""

        ui.UiComboBox图片编码器.Text = ""
        ui.UiTextBox图片编码器质量.Text = ""

        ui.UiTextBox自定义视频滤镜.Text = ""
        ui.UiTextBox自定义音频滤镜.Text = ""
        ui.UiTextBoxfilter_complex.Text = ""
        ui.UiTextBox自定义视频参数.Text = ""
        ui.UiTextBox自定义音频参数.Text = ""
        ui.UiTextBox开头参数.Text = ""
        ui.UiTextBox之前参数.Text = ""
        ui.UiTextBox之后参数.Text = ""
        ui.UiTextBox最后参数.Text = ""
        ui.UiTextBox完全自己写参数.Text = ""

        ui.UiComboBox剪辑方法.SelectedIndex = 0
        ui.UiTextBox快速剪辑入点.Text = ""
        ui.UiTextBox快速剪辑出点.Text = ""
        ui.UiComboBox剪辑向前解码多久秒.Text = ""

        ui.UiTextBox将视频参数用于这些流.Text = ""
        ui.UiCheckBox保留其他视频流.Checked = False
        ui.UiTextBox将音频参数用于这些流.Text = ""
        ui.UiCheckBox保留其他音频流.Checked = False
        ui.UiCheckBox保留内嵌字幕流.Checked = False
        ui.UiComboBox元数据选项.SelectedIndex = 0
        ui.UiComboBox章节选项.SelectedIndex = 0
        ui.UiComboBox附件选项.SelectedIndex = 0
        ui.UiCheckBox自动混流同名字幕文件.Checked = False
    End Sub

    Public Shared Function 将预设数据转换为命令行(a As 预设数据类型, 输入文件 As String, 输出文件 As String) As String
        If a.自定义参数_完全自己写 <> "" Then
            Dim x1 = a.自定义参数_完全自己写
            x1 = x1.Replace("<InputFile>", 输入文件)
            x1 = x1.Replace("<OutputFile>", 输出文件)
            Return x1
            Exit Function
        End If

        Dim 视频滤镜参数集 As New List(Of String)
        Dim 音频滤镜参数集 As New List(Of String)
        Dim 视频参数 As String = ""
        Dim 音频参数 As String = ""
        Dim 输入文件的文件夹 As String = Path.GetDirectoryName(输入文件)
        Dim arg As String = "-hide_banner -nostdin "

        If a.自定义参数_开头参数 <> "" Then arg &= $"{处理自定义参数的通配字符串(a.自定义参数_开头参数, 输入文件)} "

        If a.解码参数_解码器 <> "" Then arg &= $"-hwaccel {a.解码参数_解码器} "
        If a.解码参数_CPU解码线程数 <> "" Then arg &= $"-threads {a.解码参数_CPU解码线程数} "
        If a.解码参数_解码数据格式 <> "" Then arg &= $"-hwaccel_output_format {a.解码参数_解码数据格式} "

        If a.解码参数_指定硬件的参数名 <> "" AndAlso a.解码参数_指定硬件的参数 <> "" Then
            arg &= $"{a.解码参数_指定硬件的参数名} {a.解码参数_指定硬件的参数} "
        End If

        Select Case a.剪辑区间_方法
            Case 1
                If a.剪辑区间_入点 <> "" Then arg &= $"-ss {a.剪辑区间_入点} "
                If a.剪辑区间_出点 <> "" Then arg &= $"-to {a.剪辑区间_出点} "
            Case 3
                If a.剪辑区间_向前解码多久秒 = "" Then Exit Select
                Dim 向前解码的时间 = 将时间字符串转换为时间类型(a.剪辑区间_向前解码多久秒)
                Dim 入点时间 = 将时间字符串转换为时间类型(a.剪辑区间_入点)
                Dim 计算后的入点时间 = 入点时间 - 向前解码的时间
                If 计算后的入点时间 < TimeSpan.Zero Then 计算后的入点时间 = TimeSpan.Zero
                arg &= $"-ss {将时间类型转换为时间字符串(计算后的入点时间)} "
        End Select

        'avs 文件在启动任务时创建 
        If a.视频参数_降噪_方式 = "avs" Then
            arg &= $"-i ""{Path.Combine(Path.GetDirectoryName(输入文件), Path.GetFileNameWithoutExtension(输入文件) & ".avs")}"" "
        Else
            arg &= $"-i {""""}{输入文件}{""""} "
        End If

        If a.自定义参数_之前参数 <> "" Then arg &= $"{处理自定义参数的通配字符串(a.自定义参数_之前参数, 输入文件)} "

        If a.视频参数_编码器_类别 = "禁用" Then 视频参数 &= $"-vn "
        If a.视频参数_编码器_具体编码 <> "" Then 视频参数 &= $"-c:v {a.视频参数_编码器_具体编码} "
        Select Case a.图片参数_编码器_编码名称
            Case "PNG" : 视频参数 &= $"-c:v png {If(a.图片参数_编码器_质量值 <> "", "-compression_level " & a.图片参数_编码器_质量值, "")} "
            Case "APNG" : 视频参数 &= $"-c:v apng {If(a.图片参数_编码器_质量值 <> "", "-compression_level " & a.图片参数_编码器_质量值, "")} "
            Case "JPEG" : 视频参数 &= $"-c:v mjpeg {If(a.图片参数_编码器_质量值 <> "", "-q:v " & a.图片参数_编码器_质量值, "")} "
            Case "WEBP有损" : 视频参数 &= $"-c:v libwebp {If(a.图片参数_编码器_质量值 <> "", "-q:v " & a.图片参数_编码器_质量值, "")} "
            Case "WEBP动图" : 视频参数 &= $"-c:v libwebp_anim {If(a.图片参数_编码器_质量值 <> "", "-q:v " & a.图片参数_编码器_质量值, "")} "
            Case "AVIF静图" : 视频参数 &= $"-c:v libaom-av1 -still-picture 1 -f avif {If(a.图片参数_编码器_质量值 <> "", "-q:v " & a.图片参数_编码器_质量值, "")} "
            Case "AVIF动图" : 视频参数 &= $"-c:v libaom-av1 -f avif {If(a.图片参数_编码器_质量值 <> "", "-q:v " & a.图片参数_编码器_质量值, "")} "
            Case "GIF"
                视频参数 &= $"-c:v gif "
                If a.图片参数_编码器_质量值 = "1" Then 视频滤镜参数集.Add("split [a][b];[a] palettegen [p];[b][p] paletteuse=dither=floyd_steinberg")
            Case "BMP" : 视频参数 &= $"-c:v bmp "
            Case "OpenJPEG" : 视频参数 &= $"-c:v libopenjpeg {If(a.图片参数_编码器_质量值 <> "", "-q:v " & a.图片参数_编码器_质量值, "")} "
            Case "JPEGLS" : 视频参数 &= $"-c:v jpegls "
            Case "HDR" : 视频参数 &= $"-c:v hdr "
            Case "TIFF" : 视频参数 &= $"-c:v tiff "
            Case "DPX" : 视频参数 &= $"-c:v dpx "
            Case "OpenEXR" : 视频参数 &= $"-c:v exr "
        End Select
        If a.视频参数_编码器_编码预设 <> "" Then
            Select Case a.视频参数_编码器_具体编码
                Case "libaom-av1", "libvpx-vp9"
                    视频参数 &= $"-cpu-used {a.视频参数_编码器_编码预设} "
                Case Else
                    视频参数 &= $"-preset {a.视频参数_编码器_编码预设} "
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
        If a.视频参数_编码器_threads <> "" Then 视频参数 &= $"-threads {a.视频参数_编码器_threads} "

        If a.视频参数_分辨率 <> "" Then
            视频参数 &= $"-s {a.视频参数_分辨率} "
        Else
            If a.视频参数_分辨率自动计算_宽度 <> "" Then
                视频滤镜参数集.Add($"scale={a.视频参数_分辨率自动计算_宽度}:-2")
            ElseIf a.视频参数_分辨率自动计算_高度 <> "" Then
                视频滤镜参数集.Add($"scale=-2:{a.视频参数_分辨率自动计算_高度}")
            End If
        End If
        If a.视频参数_分辨率_裁剪滤镜参数 <> "" Then 视频滤镜参数集.Add($"crop={a.视频参数_分辨率_裁剪滤镜参数}")

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

        If a.视频参数_超分_目标宽度 <> "" AndAlso a.视频参数_超分_目标高度 <> "" Then
            Dim s1 As String = $"libplacebo=w={a.视频参数_超分_目标宽度}:h={a.视频参数_超分_目标高度}"
            If a.视频参数_超分_上采样算法 <> "" Then s1 &= $":upscaler={a.视频参数_超分_上采样算法}"
            If a.视频参数_超分_下采样算法 <> "" Then s1 &= $":downscaler={a.视频参数_超分_下采样算法}"
            If a.视频参数_超分_抗振铃强度 <> "" Then s1 &= $":antiringing={a.视频参数_超分_抗振铃强度}"
            For Each shader In a.视频参数_超分_着色器列表
                s1 &= $":custom_shader_path='{将路径转换为FFmpeg滤镜接受的格式(shader)}'"
            Next
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
                    Case "hevc_nvenc", "h264_nvenc"
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

        If a.视频参数_质量控制_值 <> "" Then
            Select Case a.视频参数_质量控制_参数名
                Case "crf" : 视频参数 &= $"-crf {a.视频参数_质量控制_值} "
                Case "cq" : 视频参数 &= $"-cq {a.视频参数_质量控制_值} "
                Case "qp" : 视频参数 &= $"-qp {a.视频参数_质量控制_值} "
                Case "global_quality" : 视频参数 &= $"-global_quality {a.视频参数_质量控制_值} "
            End Select
        End If
        If a.视频参数_比特率_基础 <> "" Then 视频参数 &= $"-b:v {a.视频参数_比特率_基础} "
        If a.视频参数_比特率_最低值 <> "" Then 视频参数 &= $"-minrate {a.视频参数_比特率_最低值} "
        If a.视频参数_比特率_最高值 <> "" Then 视频参数 &= $"-maxrate {a.视频参数_比特率_最高值} "
        If a.视频参数_比特率_缓冲区 <> "" Then 视频参数 &= $"-bufsize {a.视频参数_比特率_缓冲区} "
        For Each c In a.视频参数_质量控制_进阶参数集
            视频参数 &= $"{c} "
        Next

        If a.视频参数_色彩管理_像素格式 <> "" Then 视频参数 &= $"-pix_fmt {a.视频参数_色彩管理_像素格式} "
        Select Case a.视频参数_色彩管理_处理方式
            Case 1 '写入元数据并转换
                视频参数 &= $"-colorspace {a.视频参数_色彩管理_矩阵系数} "
                视频参数 &= $"-color_primaries {a.视频参数_色彩管理_色域} "
                视频参数 &= $"-color_trc {a.视频参数_色彩管理_传输特性} "
                视频参数 &= $"-color_range {a.视频参数_色彩管理_范围} "
                Dim vf As String = ""
                Select Case a.视频参数_色彩管理_滤镜选择
                    Case "zscale"
                        vf = $"zscale=matrix={a.视频参数_色彩管理_矩阵系数}:primaries={a.视频参数_色彩管理_色域}:transfer={a.视频参数_色彩管理_传输特性}"
                    Case "libplacebo"
                        vf = $"libplacebo=colorspace={a.视频参数_色彩管理_矩阵系数}:color_primaries={a.视频参数_色彩管理_色域}:color_trc={a.视频参数_色彩管理_传输特性}"
                End Select
                Select Case a.视频参数_色彩管理_范围
                    Case "pc" : vf &= ":range=full"
                    Case "tv" : vf &= ":range=limited"
                End Select
                Select Case a.视频参数_色彩管理_滤镜选择
                    Case "libplacebo"
                        If a.视频参数_色彩管理_色调映射算法 <> "" Then vf &= $":tonemapping={a.视频参数_色彩管理_色调映射算法}"
                End Select
                If a.视频参数_色彩管理_像素格式 <> "" Then vf &= $",format={a.视频参数_色彩管理_像素格式}"
                视频滤镜参数集.Add(vf)
            Case 2 '仅写入元数据
                视频参数 &= $"-colorspace {a.视频参数_色彩管理_矩阵系数} "
                视频参数 &= $"-color_primaries {a.视频参数_色彩管理_色域} "
                视频参数 &= $"-color_trc {a.视频参数_色彩管理_传输特性} "
                视频参数 &= $"-color_range {a.视频参数_色彩管理_范围} "
            Case 3 '仅转换
                Dim vf As String = ""
                Select Case a.视频参数_色彩管理_滤镜选择
                    Case "zscale"
                        vf = $"zscale=matrix={a.视频参数_色彩管理_矩阵系数}:primaries={a.视频参数_色彩管理_色域}:transfer={a.视频参数_色彩管理_传输特性}"
                    Case "libplacebo"
                        vf = $"libplacebo=colorspace={a.视频参数_色彩管理_矩阵系数}:color_primaries={a.视频参数_色彩管理_色域}:color_trc={a.视频参数_色彩管理_传输特性}"
                End Select
                Select Case a.视频参数_色彩管理_范围
                    Case "pc" : vf &= ":range=full"
                    Case "tv" : vf &= ":range=limited"
                End Select
                Select Case a.视频参数_色彩管理_滤镜选择
                    Case "libplacebo"
                        If a.视频参数_色彩管理_色调映射算法 <> "" Then vf &= $":tonemapping={a.视频参数_色彩管理_色调映射算法}"
                End Select
                If a.视频参数_色彩管理_像素格式 <> "" Then vf &= $",format={a.视频参数_色彩管理_像素格式}"
                视频滤镜参数集.Add(vf)
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

        Select Case a.剪辑区间_方法
            Case 2
                If a.剪辑区间_入点 <> "" Then 视频参数 &= $"-ss {a.剪辑区间_入点} "
                If a.剪辑区间_出点 <> "" Then 视频参数 &= $"-to {a.剪辑区间_出点} "
            Case 3
                If a.剪辑区间_向前解码多久秒 = "" Then Exit Select
                Dim 向前解码的时间 = 将时间字符串转换为时间类型(a.剪辑区间_向前解码多久秒)
                视频参数 &= $"-ss {将时间类型转换为时间字符串(向前解码的时间)} "
                If a.剪辑区间_出点 = "" Then Exit Select
                Dim 持续时间 = 将时间字符串转换为时间类型(a.剪辑区间_出点) - 将时间字符串转换为时间类型(a.剪辑区间_入点)
                视频参数 &= $"-t {将时间类型转换为时间字符串(持续时间)} "
        End Select

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
        If a.音频参数_比特率 <> "" Then 音频参数 &= $"-b:a {a.音频参数_比特率} "
        If a.音频参数_质量参数名 <> "" AndAlso a.音频参数_质量值 <> "" Then
            音频参数 &= $"{a.音频参数_质量参数名} {a.音频参数_质量值} "
        End If
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

        If a.视频参数_色彩管理_亮度 <> "" AndAlso a.视频参数_色彩管理_对比度 <> "" AndAlso a.视频参数_色彩管理_饱和度 <> "" AndAlso a.视频参数_色彩管理_伽马 <> "" Then
            Dim eq As New List(Of String)
            If a.视频参数_色彩管理_亮度 <> "" Then eq.Add($"brightness={a.视频参数_色彩管理_亮度}")
            If a.视频参数_色彩管理_对比度 <> "" Then eq.Add($"contrast={a.视频参数_色彩管理_对比度}")
            If a.视频参数_色彩管理_饱和度 <> "" Then eq.Add($"saturation={a.视频参数_色彩管理_饱和度}")
            If a.视频参数_色彩管理_伽马 <> "" Then eq.Add($"gamma={a.视频参数_色彩管理_伽马}")
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
            arg &= $"{If(arg.Contains("-map"), "-map 0:s?", "")} -c:s copy "
        End If

        Select Case a.流控制_元数据选项
            Case 1 : arg &= $"-map_metadata 0 "
            Case 2 : arg &= $"-map_metadata -1 "
            Case 3 : arg &= $"-movflags +use_metadata_tags "
        End Select

        Select Case a.流控制_章节选项
            Case 1 : arg &= $"-map_chapters 0 "
            Case 2 : arg &= $"-map_chapters -1 "
        End Select

        Select Case a.流控制_附件选项
            Case 1 : arg &= $"{If(arg.Contains("-map"), "-map 0:t?", "")} -c:t copy "
        End Select

        If a.自定义参数_filter_complex <> "" Then
            arg &= $"-filter_complex ""{a.自定义参数_filter_complex}"" "
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
        '=================================================================

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

    Shared Sub 显示参数总览(RTF As System.Windows.Forms.RichTextBox, a As 预设数据类型)
        RTF.Clear()
        If a.自定义参数_完全自己写 <> "" Then
            在RTF输出文本(RTF, "正在使用完全自己写参数模式，所有参数均不会生效", Color.IndianRed)
            Exit Sub
        End If

        If a.输出容器 <> "" Then
            在RTF输出文本(RTF, "输出容器：" & a.输出容器, Color.Gray)
        Else
            在RTF输出文本(RTF, "没有指定输出容器", Color.IndianRed)
        End If
        If a.输出命名_使用自动命名 And a.输出命名_自动命名选项 = 0 Then
            在RTF输出文本(RTF, "正在使用默认的附加时间戳", Color.Gray)
        ElseIf a.输出命名_使用自动命名 And a.输出命名_自动命名选项 <> 0 Then
            在RTF输出文本(RTF, "正在使用其他自动命名", Color.Gray)
        ElseIf Not a.输出命名_使用自动命名 Then
            在RTF输出文本(RTF, "注意没有使用自动命名", Color.IndianRed)
        End If
        If a.输出命名_不使用输出文件参数 Then 在RTF输出文本(RTF, "警告：已启用不使用输出文件参数，仅用于特殊需求，未指定自定义参数必报错", Color.IndianRed)
        If a.输出命名_开头文本 <> "" Then 在RTF输出文本(RTF, "输出文件开头文本：" & a.输出命名_开头文本, Color.Gray)
        If a.输出命名_替代文本 <> "" Then 在RTF输出文本(RTF, "输出文件替代文本：" & a.输出命名_替代文本, Color.Gray)
        If a.输出命名_结尾文本 <> "" Then 在RTF输出文本(RTF, "输出文件结尾文本：" & a.输出命名_结尾文本, Color.Gray)

        If a.解码参数_解码器 <> "" Then 在RTF输出文本(RTF, "解码器：" & a.解码参数_解码器, Color.Silver)
        If a.解码参数_CPU解码线程数 <> "" Then 在RTF输出文本(RTF, "CPU 解码线程数：" & a.解码参数_CPU解码线程数, Color.Silver)
        If a.解码参数_解码数据格式 <> "" Then 在RTF输出文本(RTF, "解码数据格式：" & a.解码参数_解码数据格式, Color.Silver)
        If a.解码参数_指定硬件的参数名 <> "" Then
            If a.解码参数_指定硬件的参数 <> "" Then
                在RTF输出文本(RTF, "指定解码硬件参数：-" & a.解码参数_指定硬件的参数名 & " " & a.解码参数_指定硬件的参数, Color.Silver)
            Else
                在RTF输出文本(RTF, "必须指定解码硬件的参数，那不是选了就能用的", Color.IndianRed)
            End If
        End If

        If a.视频参数_编码器_类别 <> "" Then 在RTF输出文本(RTF, "视频编码类别：" & a.视频参数_编码器_类别, Color.Silver)
        If a.视频参数_编码器_具体编码 <> "" Then 在RTF输出文本(RTF, "视频编码器：" & a.视频参数_编码器_具体编码, Color.Silver)
        If a.视频参数_编码器_编码预设 <> "" Then 在RTF输出文本(RTF, "视频编码预设：" & a.视频参数_编码器_编码预设, Color.Silver)
        If a.视频参数_编码器_配置文件 <> "" Then 在RTF输出文本(RTF, "视频编码配置文件：" & a.视频参数_编码器_配置文件, Color.Silver)
        If a.视频参数_编码器_场景优化 <> "" Then 在RTF输出文本(RTF, "视频编码场景优化：" & a.视频参数_编码器_场景优化, Color.Silver)

        '---------------- 视频尺寸 / 帧率 ----------------
        If a.视频参数_分辨率 <> "" Then 在RTF输出文本(RTF, "视频分辨率：" & a.视频参数_分辨率, Color.Silver)
        If a.视频参数_分辨率自动计算_宽度 <> "" Then
            在RTF输出文本(RTF, "视频分辨率自动计算：宽 = " & a.视频参数_分辨率自动计算_宽度, Color.Silver)
        End If
        If a.视频参数_分辨率自动计算_高度 <> "" Then
            在RTF输出文本(RTF, "视频分辨率自动计算：高 = " & a.视频参数_分辨率自动计算_高度, Color.Silver)
        End If

        If a.视频参数_分辨率_裁剪滤镜参数 <> "" Then 在RTF输出文本(RTF, "画面裁剪：" & a.视频参数_分辨率_裁剪滤镜参数, Color.Silver)
        If a.视频参数_帧速率 <> "" Then 在RTF输出文本(RTF, "输出帧率：" & a.视频参数_帧速率, Color.Silver)
        If a.视频参数_帧速率_抽帧最大变化比例 <> "" Then 在RTF输出文本(RTF, "抽帧最大变化比例：" & a.视频参数_帧速率_抽帧最大变化比例, Color.Silver)

        '---------------- 插帧 ----------------
        If a.视频参数_插帧_目标帧率 <> "" Then 在RTF输出文本(RTF, "插帧目标帧率：" & a.视频参数_插帧_目标帧率, Color.Silver)
        If a.视频参数_插帧_插帧模式 <> "" Then 在RTF输出文本(RTF, "插帧模式：" & a.视频参数_插帧_插帧模式, Color.Silver)
        If a.视频参数_插帧_运动估计模式 <> "" Then 在RTF输出文本(RTF, "运动估计模式：" & a.视频参数_插帧_运动估计模式, Color.Silver)
        If a.视频参数_插帧_运动估计算法 <> "" Then 在RTF输出文本(RTF, "运动估计算法：" & a.视频参数_插帧_运动估计算法, Color.Silver)
        If a.视频参数_插帧_运动补偿模式 <> "" Then 在RTF输出文本(RTF, "运动补偿模式：" & a.视频参数_插帧_运动补偿模式, Color.Silver)
        If a.视频参数_插帧_可变块大小的运动补偿 Then 在RTF输出文本(RTF, "插帧：已启用可变块大小运动补偿", Color.Silver)
        If a.视频参数_插帧_块大小 <> "" Then 在RTF输出文本(RTF, "插帧块大小：" & a.视频参数_插帧_块大小, Color.Silver)
        If a.视频参数_插帧_搜索范围 <> "" Then 在RTF输出文本(RTF, "插帧搜索范围：" & a.视频参数_插帧_搜索范围, Color.Silver)
        If a.视频参数_插帧_场景变化检测强度 <> "" Then 在RTF输出文本(RTF, "场景变化检测强度：" & a.视频参数_插帧_场景变化检测强度, Color.Silver)

        '---------------- 帧混合 ----------------
        If a.视频参数_帧混合_指定帧率 <> "" Then 在RTF输出文本(RTF, "帧混合指定帧率：" & a.视频参数_帧混合_指定帧率, Color.Silver)
        If a.视频参数_帧混合_混合模式 <> "" Then 在RTF输出文本(RTF, "帧混合模式：" & a.视频参数_帧混合_混合模式, Color.Silver)
        If a.视频参数_帧混合_混合比例 <> "" Then 在RTF输出文本(RTF, "帧混合比例：" & a.视频参数_帧混合_混合比例, Color.Silver)

        '---------------- 超分 ----------------
        If a.视频参数_超分_目标宽度 <> "" Then 在RTF输出文本(RTF, $"超分目标分辨率：{a.视频参数_超分_目标宽度} x {a.视频参数_超分_目标高度}", Color.Silver)
        If a.视频参数_超分_上采样算法 <> "" Then 在RTF输出文本(RTF, $"超分上采样算法：{a.视频参数_超分_上采样算法}", Color.Silver)
        If a.视频参数_超分_下采样算法 <> "" Then 在RTF输出文本(RTF, $"超分下采样算法：{a.视频参数_超分_下采样算法}", Color.Silver)
        If a.视频参数_超分_抗振铃强度 <> "" Then 在RTF输出文本(RTF, $"超分抗振铃强度：{a.视频参数_超分_抗振铃强度}", Color.Silver)
        If a.视频参数_超分_着色器列表.Count > 0 Then 在RTF输出文本(RTF, $"超分正在使用 {a.视频参数_超分_着色器列表.Count} 个自定义着色器", Color.Silver)

        '---------------- 码率 / 质量控制 ----------------
        If a.视频参数_质量控制_参数名 <> "" Then 在RTF输出文本(RTF, "质量控制参数：" & a.视频参数_质量控制_参数名 & " = " & a.视频参数_质量控制_值, Color.Silver)
        If a.视频参数_比特率_基础 <> "" Then 在RTF输出文本(RTF, "基础码率：" & a.视频参数_比特率_基础, Color.Silver)
        If a.视频参数_比特率_最低值 <> "" Then 在RTF输出文本(RTF, "最低码率：" & a.视频参数_比特率_最低值, Color.Silver)
        If a.视频参数_比特率_最高值 <> "" Then 在RTF输出文本(RTF, "最高码率：" & a.视频参数_比特率_最高值, Color.Silver)
        If a.视频参数_比特率_缓冲区 <> "" Then 在RTF输出文本(RTF, "缓冲区大小：" & a.视频参数_比特率_缓冲区, Color.Silver)
        If a.视频参数_质量控制_进阶参数集 IsNot Nothing AndAlso a.视频参数_质量控制_进阶参数集.Count > 0 Then
            在RTF输出文本(RTF, "进阶质量控制：" & String.Join(" ", a.视频参数_质量控制_进阶参数集), Color.Silver)
        End If

        '---------------- 色彩管理 ----------------
        If a.视频参数_色彩管理_像素格式 <> "" Then 在RTF输出文本(RTF, "像素格式：" & a.视频参数_色彩管理_像素格式, Color.Silver)

        If a.视频参数_色彩管理_滤镜选择 <> "" Then 在RTF输出文本(RTF, "色彩转换滤镜：" & a.视频参数_色彩管理_滤镜选择, Color.Silver)
        If a.视频参数_色彩管理_矩阵系数 <> "" Then 在RTF输出文本(RTF, "矩阵系数 & 颜色格式：" & a.视频参数_色彩管理_矩阵系数, Color.Silver)
        If a.视频参数_色彩管理_色域 <> "" Then 在RTF输出文本(RTF, "色域：" & a.视频参数_色彩管理_色域, Color.Silver)
        If a.视频参数_色彩管理_传输特性 <> "" Then 在RTF输出文本(RTF, "传输特性：" & a.视频参数_色彩管理_传输特性, Color.Silver)
        If a.视频参数_色彩管理_范围 <> "" Then 在RTF输出文本(RTF, "色彩范围：" & a.视频参数_色彩管理_范围, Color.Silver)
        If a.视频参数_色彩管理_色调映射算法 <> "" Then 在RTF输出文本(RTF, "色调映射算法：" & a.视频参数_色彩管理_色调映射算法, Color.Silver)
        Select Case a.视频参数_色彩管理_处理方式
            Case 1 : 在RTF输出文本(RTF, "色彩管理写入元数据并转换", Color.Silver)
            Case 2 : 在RTF输出文本(RTF, "色彩管理只写入元数据不转换", Color.Silver)
            Case 3 : 在RTF输出文本(RTF, "色彩管理只转换不写元数据", Color.Silver)
        End Select

        If a.视频参数_色彩管理_亮度 <> "" Then 在RTF输出文本(RTF, "亮度调整：" & a.视频参数_色彩管理_亮度, Color.Silver)
        If a.视频参数_色彩管理_对比度 <> "" Then 在RTF输出文本(RTF, "对比度调整：" & a.视频参数_色彩管理_对比度, Color.Silver)
        If a.视频参数_色彩管理_饱和度 <> "" Then 在RTF输出文本(RTF, "饱和度调整：" & a.视频参数_色彩管理_饱和度, Color.Silver)
        If a.视频参数_色彩管理_伽马 <> "" Then 在RTF输出文本(RTF, "伽马调整：" & a.视频参数_色彩管理_伽马, Color.Silver)

        '---------------- 常见滤镜 ----------------
        If a.视频参数_降噪_方式 <> "" Then 在RTF输出文本(RTF, "降噪方式：" & a.视频参数_降噪_方式, Color.Silver)
        If a.视频参数_降噪_参数1 <> "" Then 在RTF输出文本(RTF, "降噪参数1：" & a.视频参数_降噪_参数1, Color.Silver)
        If a.视频参数_降噪_参数2 <> "" Then 在RTF输出文本(RTF, "降噪参数2：" & a.视频参数_降噪_参数2, Color.Silver)
        If a.视频参数_降噪_参数3 <> "" Then 在RTF输出文本(RTF, "降噪参数3：" & a.视频参数_降噪_参数3, Color.Silver)
        If a.视频参数_降噪_参数4 <> "" Then 在RTF输出文本(RTF, "降噪参数4：" & a.视频参数_降噪_参数4, Color.Silver)
        If a.视频参数_锐化_水平尺寸 <> "" OrElse a.视频参数_锐化_垂直尺寸 <> "" OrElse a.视频参数_锐化_锐化强度 <> "" Then
            在RTF输出文本(RTF, "锐化：水平 = " & a.视频参数_锐化_水平尺寸 & " 垂直 = " & a.视频参数_锐化_垂直尺寸 & " 强度 = " & a.视频参数_锐化_锐化强度, Color.Silver)
        End If
        If a.视频参数_逐行与隔行_操作 > 0 Then 在RTF输出文本(RTF, a.视频参数_逐行与隔行_操作, Color.Silver)
        If a.视频参数_画面翻转_角度翻转 > 0 Then 在RTF输出文本(RTF, a.视频参数_画面翻转_角度翻转, Color.Silver)
        If a.视频参数_画面翻转_镜像翻转 > 0 Then 在RTF输出文本(RTF, a.视频参数_画面翻转_镜像翻转, Color.Silver)

        '---------------- 音频参数 ----------------
        If a.音频参数_编码器_具体编码 <> "" Then 在RTF输出文本(RTF, "音频编码器：" & a.音频参数_编码器_具体编码, Color.Silver)
        If a.音频参数_比特率 <> "" Then 在RTF输出文本(RTF, "音频比特率：" & a.音频参数_比特率, Color.Silver)
        If a.音频参数_质量参数名 <> "" Then 在RTF输出文本(RTF, "音频质量控制：" & a.音频参数_质量参数名 & "=" & a.音频参数_质量值, Color.Silver)
        If a.音频参数_声道数 <> "" Then 在RTF输出文本(RTF, "声道布局：" & a.音频参数_声道数, Color.Silver)
        If a.音频参数_采样率 <> "" Then 在RTF输出文本(RTF, "采样率：" & a.音频参数_采样率, Color.Silver)
        If a.音频参数_响度标准化_目标响度 <> "" Then 在RTF输出文本(RTF, "响度标准化目标：" & a.音频参数_响度标准化_目标响度, Color.Silver)
        If a.音频参数_响度标准化_动态范围 <> "" Then 在RTF输出文本(RTF, "响度动态范围：" & a.音频参数_响度标准化_动态范围, Color.Silver)
        If a.音频参数_响度标准化_峰值电平 <> "" Then 在RTF输出文本(RTF, "响度峰值电平：" & a.音频参数_响度标准化_峰值电平, Color.Silver)

        '---------------- 图片参数 ----------------
        If a.图片参数_编码器_编码名称 <> "" Then 在RTF输出文本(RTF, "图片编码器：" & a.图片参数_编码器_编码名称, Color.Silver)
        If a.图片参数_编码器_质量值 <> "" Then 在RTF输出文本(RTF, "图片质量：" & a.图片参数_编码器_质量值, Color.Silver)

        '---------------- 自定义参数 ----------------
        If a.自定义参数_开头参数 <> "" Then 在RTF输出文本(RTF, "自定义开头参数：" & a.自定义参数_开头参数, Color.Gray)
        If a.自定义参数_之前参数 <> "" Then 在RTF输出文本(RTF, "自定义之前参数：" & a.自定义参数_之前参数, Color.Gray)
        If a.自定义参数_视频滤镜 <> "" Then 在RTF输出文本(RTF, "自定义视频滤镜：" & a.自定义参数_视频滤镜, Color.Gray)
        If a.自定义参数_音频滤镜 <> "" Then 在RTF输出文本(RTF, "自定义音频滤镜：" & a.自定义参数_音频滤镜, Color.Gray)
        If a.自定义参数_filter_complex <> "" Then 在RTF输出文本(RTF, "自定义 filter_complex：" & a.自定义参数_filter_complex, Color.Gray)
        If a.自定义参数_视频参数 <> "" Then 在RTF输出文本(RTF, "自定义视频参数：" & a.自定义参数_视频参数, Color.Gray)
        If a.自定义参数_音频参数 <> "" Then 在RTF输出文本(RTF, "自定义音频参数：" & a.自定义参数_音频参数, Color.Gray)
        If a.自定义参数_之后参数 <> "" Then 在RTF输出文本(RTF, "自定义之后参数：" & a.自定义参数_之后参数, Color.Gray)
        If a.自定义参数_最后参数 <> "" Then 在RTF输出文本(RTF, "自定义最后参数：" & a.自定义参数_最后参数, Color.Gray)

        '---------------- 剪辑区间 ----------------
        Select Case a.剪辑区间_方法
            Case 1 : 在RTF输出文本(RTF, "剪辑区间方法：粗剪", Color.Silver)
            Case 2 : 在RTF输出文本(RTF, "剪辑区间方法：精剪 (从头解码)", Color.Silver)
            Case 3 : 在RTF输出文本(RTF, "剪辑区间方法：精剪 (快速响应)", Color.Silver)
            Case Else : If a.剪辑区间_入点 <> "" OrElse a.剪辑区间_出点 <> "" Then 在RTF输出文本(RTF, "警告：指定了剪辑范围却没有指定剪辑方法，不会进行剪辑", Color.IndianRed)
        End Select
        If a.剪辑区间_入点 <> "" Then 在RTF输出文本(RTF, "剪辑入点：" & a.剪辑区间_入点, Color.Silver)
        If a.剪辑区间_出点 <> "" Then 在RTF输出文本(RTF, "剪辑出点：" & a.剪辑区间_出点, Color.Silver)
        If a.剪辑区间_向前解码多久秒 <> "" Then 在RTF输出文本(RTF, "快速响应的精剪向前解码 " & a.剪辑区间_向前解码多久秒 & " 秒", Color.Silver)

        '---------------- 流控制 ----------------
        If a.流控制_启用保留其他视频流 Then 在RTF输出文本(RTF, "已选择保留其他视频流", Color.Silver)
        If a.流控制_将视频参数应用于指定流 IsNot Nothing AndAlso a.流控制_将视频参数应用于指定流.Length > 0 Then
            在RTF输出文本(RTF, "应用视频参数到流：" & String.Join(",", a.流控制_将视频参数应用于指定流), Color.Silver)
        End If
        If a.流控制_启用保留其他音频流 Then 在RTF输出文本(RTF, "已选择保留其他音频流", Color.Silver)
        If a.流控制_将音频参数应用于指定流 IsNot Nothing AndAlso a.流控制_将音频参数应用于指定流.Length > 0 Then
            在RTF输出文本(RTF, "应用音频参数到流：" & String.Join(",", a.流控制_将音频参数应用于指定流), Color.Silver)
        End If
        If a.流控制_启用保留内嵌字幕流 Then 在RTF输出文本(RTF, "已选择保留内嵌字幕流", Color.Silver)
        If a.流控制_启用自动混流同名字幕文件 Then 在RTF输出文本(RTF, "已选择自动混流同名字幕文件", Color.Silver)
        Select Case a.流控制_元数据选项
            Case 1 : 在RTF输出文本(RTF, "保留元数据", Color.Silver)
            Case 2 : 在RTF输出文本(RTF, "清除元数据", Color.Silver)
        End Select
        Select Case a.流控制_章节选项
            Case 1 : 在RTF输出文本(RTF, "保留章节", Color.Silver)
            Case 2 : 在RTF输出文本(RTF, "清除章节", Color.Silver)
        End Select
        If a.流控制_附件选项 = 1 Then 在RTF输出文本(RTF, "保留附件", Color.Silver)

        '---------------- 其他 ----------------
        If a.输出位置 <> "" Then 在RTF输出文本(RTF, "输出位置：" & a.输出位置, Color.Gray)
    End Sub
End Class
