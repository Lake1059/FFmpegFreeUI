
Public Class 界面控制

    Public Shared Sub 初始化()
        Form1.UiComboBox21.SelectedIndex = 0
        Form1.Panel41.AutoSize = True


        AddHandler Form1.UiTabControlMenu1.SelectedIndexChanged, AddressOf 界面校准

        AddHandler Form1.LinkLabel7.LinkClicked, Sub() Process.Start(New ProcessStartInfo With {.FileName = "https://github.com/Lake1059/FFmpegFreeUI", .UseShellExecute = True})
        AddHandler Form1.LinkLabel2.LinkClicked, Sub() Process.Start(New ProcessStartInfo With {.FileName = "https://ffmpeg.org/documentation.html", .UseShellExecute = True})
        AddHandler Form1.LinkLabel3.LinkClicked, Sub() Process.Start(New ProcessStartInfo With {.FileName = "https://www.gyan.dev/ffmpeg/builds/", .UseShellExecute = True})
        AddHandler Form1.LinkLabel4.LinkClicked, Sub() Process.Start(New ProcessStartInfo With {.FileName = "https://github.com/BtbN/FFmpeg-Builds/releases", .UseShellExecute = True})
        AddHandler Form1.LinkLabel5.LinkClicked, Sub() Process.Start(New ProcessStartInfo With {.FileName = "https://afdian.com/a/1059Studio", .UseShellExecute = True})
        AddHandler Form1.LinkLabel6.LinkClicked, Sub() Process.Start(New ProcessStartInfo With {.FileName = "https://space.bilibili.com/319785096", .UseShellExecute = True})


        暗黑列表视图自绘制.绑定列表视图事件(Form1.ListView1)
        暗黑列表视图自绘制.绑定列表视图事件(Form1.ListView2)


        AddHandler Form1.UiButton3.Click, AddressOf 界面控制_编码队列.开始任务
        AddHandler Form1.UiButton1.Click, AddressOf 界面控制_编码队列.暂停任务
        AddHandler Form1.UiButton2.Click, AddressOf 界面控制_编码队列.恢复任务
        AddHandler Form1.UiButton5.Click, AddressOf 界面控制_编码队列.停止任务
        AddHandler Form1.UiButton6.Click, AddressOf 界面控制_编码队列.移除任务
        AddHandler Form1.UiButton16.Click, AddressOf 界面控制_编码队列.重置任务
        AddHandler Form1.UiButton4.Click, AddressOf 界面控制_编码队列.全选任务
        AddHandler Form1.UiButton9.Click, AddressOf 界面控制_编码队列.定位输出
        AddHandler Form1.UiButton8.Click, AddressOf 界面控制_编码队列.重新配置
        AddHandler Form1.UiButton7.Click, AddressOf 界面控制_编码队列.导出配置
        AddHandler Form1.UiButton15.Click, AddressOf 界面控制_编码队列.复制命令行

        AddHandler Form1.UiButton14.Click, AddressOf 界面控制_添加文件.加入编码队列
        AddHandler Form1.ListView2.DragEnter, AddressOf 界面控制_添加文件.ListView2_DragEnter
        AddHandler Form1.ListView2.DragDrop, AddressOf 界面控制_添加文件.ListView2_DragDrop
        AddHandler Form1.UiButton18.Click, AddressOf 界面控制_添加文件.添加文件
        AddHandler Form1.UiButton10.Click, AddressOf 界面控制_添加文件.打开文件夹添加全部文件
        AddHandler Form1.UiButton11.Click, AddressOf 界面控制_添加文件.批量移除选中项
        AddHandler Form1.UiButton12.Click, AddressOf 界面控制_添加文件.移除全部项
        AddHandler Form1.UiComboBox21.SelectedIndexChanged, AddressOf 界面控制_添加文件.选择输出目录

        '==============================================
        AddHandler Form1.UiCheckBox解码器.Click, AddressOf 界面控制_解码选项.解码器参数开关按下事件
        AddHandler Form1.UiComboBox解码器.TextChanged, AddressOf 界面控制_解码选项.解码器参数变动事件
        AddHandler Form1.UiCheckBox解码数据格式.Click, AddressOf 界面控制_解码选项.解码数据格式参数开关按下事件
        AddHandler Form1.UiComboBox解码器.TextChanged, AddressOf 界面控制_解码选项.解码数据格式参数变动事件
        '==============================================
        AddHandler Form1.UiCheckBox视频编码器.CheckedChanged, AddressOf 界面控制_视频参数.视频编码器参数开关事件
        AddHandler Form1.UiComboBox编码类别.SelectedIndexChanged, AddressOf 界面控制_视频参数.视频编码类别改动事件
        AddHandler Form1.UiComboBox具体编码.SelectedIndexChanged, AddressOf 界面控制_视频参数.视频具体编码改动事件
        '==============================================
        AddHandler Form1.UiCheckBox分辨率.Click, AddressOf 界面控制_视频参数.视频分辨率参数开关按下事件
        AddHandler Form1.UiComboBox分辨率.TextChanged, AddressOf 界面控制_视频参数.视频分辨率文本框文本变动事件
        AddHandler Form1.UiTextBox分辨率自动计算宽度.TextChanged, AddressOf 界面控制_视频参数.视频分辨率宽度文本框文本变动事件
        AddHandler Form1.UiTextBox分辨率自动计算高度.TextChanged, AddressOf 界面控制_视频参数.视频分辨率高度文本框文本变动事件
        '==============================================
        AddHandler Form1.UiCheckBox帧速率.Click, AddressOf 界面控制_视频参数.视频帧速率参数开关按下事件
        AddHandler Form1.UiComboBox帧速率.TextChanged, AddressOf 界面控制_视频参数.视频帧速率改动事件
        AddHandler Form1.UiTextBox智能抽帧阈值.TextChanged, AddressOf 界面控制_视频参数.视频帧速率改动事件
        '==============================================
        AddHandler Form1.UiCheckBox比特率.Click, AddressOf 界面控制_视频参数.视频比特率参数开关按下事件
        AddHandler Form1.UiComboBox比特率控制方式.SelectedIndexChanged, AddressOf 界面控制_视频参数.视频比特率控制方式改动事件
        '==============================================
        AddHandler Form1.UiCheckBox质量控制.Click, AddressOf 界面控制_视频参数.视频质量控制开关按下事件
        AddHandler Form1.UiCheckBox展开精细控制.CheckedChanged, AddressOf 界面控制_视频参数.视频质量控制展开精细控制开关变动事件
        '==============================================
        AddHandler Form1.UiCheckBox帧排列.Click, AddressOf 界面控制_视频参数.视频帧排列参数开关按下事件
        AddHandler Form1.UiTextBox关键帧间隔.TextChanged, AddressOf 界面控制_视频参数.视频帧排列参数变动事件
        AddHandler Form1.UiTextBox双向预测帧数量.TextChanged, AddressOf 界面控制_视频参数.视频帧排列参数变动事件
        '==============================================
        AddHandler Form1.UiCheckBox画面规格.Click, AddressOf 界面控制_视频参数.视频画面规格参数开关按下事件
        AddHandler Form1.UiComboBox配置文件.TextChanged, AddressOf 界面控制_视频参数.视频画面规格参数变动事件
        AddHandler Form1.UiComboBox场景优化.TextChanged, AddressOf 界面控制_视频参数.视频画面规格参数变动事件
        AddHandler Form1.UiComboBox像素格式.TextChanged, AddressOf 界面控制_视频参数.视频画面规格参数变动事件
        '==============================================
        AddHandler Form1.UiCheckBox色彩管理.Click, AddressOf 界面控制_视频参数.视频色彩管理参数开关按下事件
        AddHandler Form1.UiComboBox矩阵系数.TextChanged, AddressOf 界面控制_视频参数.视频色彩管理参数变动事件
        AddHandler Form1.UiComboBox色域.TextChanged, AddressOf 界面控制_视频参数.视频色彩管理参数变动事件
        AddHandler Form1.UiComboBox传输特性.TextChanged, AddressOf 界面控制_视频参数.视频色彩管理参数变动事件
        AddHandler Form1.UiComboBox色彩范围.TextChanged, AddressOf 界面控制_视频参数.视频色彩管理参数变动事件
        AddHandler Form1.UiComboBox色彩管理处理方式.TextChanged, AddressOf 界面控制_视频参数.视频色彩管理参数变动事件
        '==============================================
        AddHandler Form1.UiCheckBox降噪.Click, AddressOf 界面控制_视频参数.视频降噪参数开关按下事件
        AddHandler Form1.UiComboBox降噪方式.TextChanged, AddressOf 界面控制_视频参数.视频降噪方式变动事件
        '==============================================
        AddHandler Form1.UiCheckBox锐化.Click, AddressOf 界面控制_视频参数.视频锐化参数开关按下事件
        AddHandler Form1.UiTextBox锐化水平尺寸.TextChanged, AddressOf 界面控制_视频参数.视频锐化参数变动事件
        AddHandler Form1.UiTextBox锐化垂直尺寸.TextChanged, AddressOf 界面控制_视频参数.视频锐化参数变动事件
        AddHandler Form1.UiTextBox锐化强度.TextChanged, AddressOf 界面控制_视频参数.视频锐化参数变动事件
        '==========================================
        AddHandler Form1.UiCheckBox逐行与隔行.Click, AddressOf 界面控制_视频参数.视频逐行与隔行参数开关按下事件
        AddHandler Form1.UiComboBox逐行与隔行处理方式.TextChanged, AddressOf 界面控制_视频参数.视频逐行与隔行参数变动事件
        '==============================================
        AddHandler Form1.UiCheckBox自定义视频滤镜.Click, AddressOf 界面控制_视频参数.视频自定义滤镜开关按下事件
        AddHandler Form1.UiTextBox自定义视频滤镜.TextChanged, AddressOf 界面控制_视频参数.视频自定义滤镜参数变动事件
        AddHandler Form1.UiCheckBox自定义视频参数.Click, AddressOf 界面控制_视频参数.视频自定义参数开关按下事件
        AddHandler Form1.UiTextBox自定义视频参数.TextChanged, AddressOf 界面控制_视频参数.视频自定义参数变动事件
        '==========================================
        AddHandler Form1.UiCheckBox音频编码器.Click, AddressOf 界面控制_音频参数.音频编码器参数开关按下事件
        AddHandler Form1.UiComboBox音频编码器.TextChanged, AddressOf 界面控制_音频参数.音频编码器参数变动事件
        '==========================================
        AddHandler Form1.UiCheckBox音频比特率.Click, AddressOf 界面控制_音频参数.音频比特率参数开关按下事件
        AddHandler Form1.UiComboBox音频比特率控制方式.TextChanged, AddressOf 界面控制_音频参数.音频比特率参数变动事件
        AddHandler Form1.UiTextBox音频基础比特率.TextChanged, AddressOf 界面控制_音频参数.音频比特率参数变动事件
        AddHandler Form1.UiTextBox音频比特率质量值.TextChanged, AddressOf 界面控制_音频参数.音频比特率参数变动事件
        '==========================================
        AddHandler Form1.UiCheckBox声道布局.Click, AddressOf 界面控制_音频参数.音频声道布局参数开关按下事件
        AddHandler Form1.UiComboBox声道布局.TextChanged, AddressOf 界面控制_音频参数.音频声道布局参数变动事件
        '==========================================
        AddHandler Form1.UiCheckBox采样率.Click, AddressOf 界面控制_音频参数.音频采样率参数开关按下事件
        AddHandler Form1.UiComboBox采样率.TextChanged, AddressOf 界面控制_音频参数.音频采样率参数变动事件
        '==========================================
        AddHandler Form1.UiCheckBox响度标准化.Click, AddressOf 界面控制_音频参数.音频响度标准化参数开关按下事件
        AddHandler Form1.UiTextBox响度标准化目标响度.TextChanged, AddressOf 界面控制_音频参数.音频响度标准化参数变动事件
        AddHandler Form1.UiTextBox响度标准化动态范围.TextChanged, AddressOf 界面控制_音频参数.音频响度标准化参数变动事件
        AddHandler Form1.UiTextBox响度标准化峰值电平.TextChanged, AddressOf 界面控制_音频参数.音频响度标准化参数变动事件
        '==============================================
        AddHandler Form1.UiCheckBox自定义音频滤镜.Click, AddressOf 界面控制_音频参数.音频自定义滤镜开关按下事件
        AddHandler Form1.UiTextBox自定义音频滤镜.TextChanged, AddressOf 界面控制_音频参数.音频自定义滤镜参数变动事件
        AddHandler Form1.UiCheckBox自定义音频参数.Click, AddressOf 界面控制_音频参数.音频自定义参数开关按下事件
        AddHandler Form1.UiTextBox自定义音频参数.TextChanged, AddressOf 界面控制_音频参数.音频自定义参数变动事件
        '==========================================
        AddHandler Form1.UiCheckBox图片编码器.Click, AddressOf 界面控制_图片参数.图片编码器参数开关按下事件
        AddHandler Form1.UiComboBox图片编码器.TextChanged, AddressOf 界面控制_图片参数.图片编码器参数变动事件
        AddHandler Form1.UiTextBox图片编码器质量.TextChanged, AddressOf 界面控制_图片参数.图片编码器参数变动事件
        '==========================================
        AddHandler Form1.UiCheckBox图片分辨率.Click, AddressOf 界面控制_图片参数.图片分辨率参数开关按下事件
        AddHandler Form1.UiTextBox图片分辨率宽度.TextChanged, AddressOf 界面控制_图片参数.图片分辨率参数变动事件
        AddHandler Form1.UiTextBox图片分辨率宽度.TextChanged, AddressOf 界面控制_图片参数.图片分辨率参数变动事件
        '==========================================
        AddHandler Form1.UiCheckBox自定义图片参数.Click, AddressOf 界面控制_图片参数.图片自定义参数开关按下事件
        AddHandler Form1.UiTextBox自定义图片参数.TextChanged, AddressOf 界面控制_图片参数.图片自定义参数变动事件


        '==============================================
        AddHandler Form1.UiCheckBox快速剪辑.Click, AddressOf 界面控制_视频参数.视频快速剪辑参数开关按下事件
        AddHandler Form1.UiTextBox快速剪辑入点.TextChanged, AddressOf 界面控制_视频参数.视频快速剪辑文本框文本变动事件
        AddHandler Form1.UiTextBox快速剪辑出点.TextChanged, AddressOf 界面控制_视频参数.视频快速剪辑文本框文本变动事件
        '==============================================
        AddHandler Form1.UiCheckBox自定义filter_complex.Click, Sub() If Not Form1.UiCheckBox自定义filter_complex.Checked Then Form1.UiTextBoxfilter_complex.Text = ""
        AddHandler Form1.UiTextBoxfilter_complex.TextChanged, Sub() If Form1.UiTextBoxfilter_complex.Text = "" Then Form1.UiCheckBox自定义filter_complex.Checked = False
        AddHandler Form1.UiCheckBox1.Click, Sub() If Not Form1.UiCheckBox1.Checked Then Form1.UiTextBox1.Text = ""
        AddHandler Form1.UiTextBox1.TextChanged, Sub() If Form1.UiTextBox1.Text = "" Then Form1.UiCheckBox1.Checked = False

        '==============================================
        AddHandler Form1.UiButton22.Click, AddressOf 预设管理.保存预设到文件
        AddHandler Form1.UiButton21.Click, AddressOf 预设管理.从文件读取预设
        AddHandler Form1.UiButton13.Click, AddressOf 预设管理.重置全部包含在预设中的设置

        设置富文本框行高(Form1.RichTextBox1, 350)

        Form1.是否初始化 = True
    End Sub

    Public Shared Sub 界面校准()
        Dim 选项卡 As TabPage = Form1.UiTabControlMenu1.SelectedTab
        Select Case True
            Case 选项卡.IsEqual(Form1.TabPage编码队列)
                Form1.Label1.Width = Form1.Panel1.Width - Form1.Panel1.Padding.Left - Form1.Label2.Width - Form1.Label3.Width - Form1.Label4.Width - Form1.Label5.Width - Form1.Label6.Width - Form1.Label7.Width - 200 * Form1.DPI
                Form1.ListView1.Columns(0).Width = Form1.Label1.Width - 5 * Form1.DPI
                Form1.ListView1.Columns(1).Width = Form1.Label2.Width
                Form1.ListView1.Columns(2).Width = Form1.Label3.Width
                Form1.ListView1.Columns(3).Width = Form1.Label4.Width
                Form1.ListView1.Columns(4).Width = Form1.Label5.Width
                Form1.ListView1.Columns(5).Width = Form1.Label6.Width
                Form1.ListView1.Columns(6).Width = Form1.Label7.Width
                Form1.ListView1.Columns(7).Width = Form1.Label8.Width - SystemInformation.VerticalScrollBarWidth * Form1.DPI * 2

            Case 选项卡.IsEqual(Form1.TabPage添加文件)
                Form1.ListView2.Columns(0).Width = Form1.ListView2.Width - SystemInformation.VerticalScrollBarWidth * Form1.DPI * 2
                Form1.UiComboBox输出容器.ItemHeight = 30 * Form1.DPI
                Form1.UiComboBox21.ItemHeight = 30 * Form1.DPI

            Case 选项卡.IsEqual(Form1.TabPage解码选项)
                Form1.UiCheckBox解码器.CheckBoxSize = 20 * Form1.DPI
                Form1.UiComboBox解码器.ItemHeight = 30 * Form1.DPI
                Form1.UiCheckBox解码数据格式.CheckBoxSize = 20 * Form1.DPI
                Form1.UiComboBox解码数据格式.ItemHeight = 30 * Form1.DPI

            Case 选项卡.IsEqual(Form1.TabPage视频参数)
                Form1.UiCheckBox视频编码器.CheckBoxSize = 20 * Form1.DPI
                Form1.UiComboBox编码类别.ItemHeight = 30 * Form1.DPI
                Form1.UiComboBox具体编码.ItemHeight = 30 * Form1.DPI
                Form1.UiComboBox编码预设.ItemHeight = 30 * Form1.DPI
                Form1.UiCheckBox分辨率.CheckBoxSize = 20 * Form1.DPI
                Form1.UiComboBox分辨率.ItemHeight = 30 * Form1.DPI
                Form1.UiCheckBox帧速率.CheckBoxSize = 20 * Form1.DPI
                Form1.UiComboBox帧速率.ItemHeight = 30 * Form1.DPI
                Form1.UiCheckBox比特率.CheckBoxSize = 20 * Form1.DPI
                Form1.UiComboBox比特率控制方式.ItemHeight = 30 * Form1.DPI
                Form1.UiCheckBox质量控制.CheckBoxSize = 20 * Form1.DPI
                Form1.UiComboBox全局质量控制参数.ItemHeight = 30 * Form1.DPI
                Form1.UiCheckBox展开精细控制.CheckBoxSize = 20 * Form1.DPI
                Form1.UiCheckBox帧排列.CheckBoxSize = 20 * Form1.DPI
                Form1.UiCheckBox画面规格.CheckBoxSize = 20 * Form1.DPI
                Form1.UiComboBox配置文件.ItemHeight = 30 * Form1.DPI
                Form1.UiComboBox场景优化.ItemHeight = 30 * Form1.DPI
                Form1.UiComboBox像素格式.ItemHeight = 30 * Form1.DPI

            Case 选项卡.IsEqual(Form1.TabPage高级视频参数)
                Form1.UiCheckBox色彩管理.CheckBoxSize = 20 * Form1.DPI
                Form1.UiComboBox矩阵系数.ItemHeight = 30 * Form1.DPI
                Form1.UiComboBox色域.ItemHeight = 30 * Form1.DPI
                Form1.UiComboBox传输特性.ItemHeight = 30 * Form1.DPI
                Form1.UiComboBox色彩范围.ItemHeight = 30 * Form1.DPI
                Form1.UiComboBox色彩管理处理方式.ItemHeight = 30 * Form1.DPI
                Form1.UiCheckBox降噪.CheckBoxSize = 20 * Form1.DPI
                Form1.UiComboBox降噪方式.ItemHeight = 30 * Form1.DPI
                Form1.UiCheckBox锐化.CheckBoxSize = 20 * Form1.DPI
                Form1.UiCheckBox逐行与隔行.CheckBoxSize = 20 * Form1.DPI
                Form1.UiComboBox逐行与隔行处理方式.ItemHeight = 30 * Form1.DPI
                Form1.UiCheckBox自定义视频滤镜.CheckBoxSize = 20 * Form1.DPI
                Form1.UiCheckBox自定义视频参数.CheckBoxSize = 20 * Form1.DPI

            Case 选项卡.IsEqual(Form1.TabPage音频参数)
                Form1.UiCheckBox音频编码器.CheckBoxSize = 20 * Form1.DPI
                Form1.UiComboBox音频编码器.ItemHeight = 30 * Form1.DPI
                Form1.UiCheckBox音频比特率.CheckBoxSize = 20 * Form1.DPI
                Form1.UiComboBox音频比特率控制方式.ItemHeight = 30 * Form1.DPI
                Form1.UiCheckBox声道布局.CheckBoxSize = 20 * Form1.DPI
                Form1.UiComboBox声道布局.ItemHeight = 30 * Form1.DPI
                Form1.UiCheckBox采样率.CheckBoxSize = 20 * Form1.DPI
                Form1.UiComboBox采样率.ItemHeight = 30 * Form1.DPI
                Form1.UiCheckBox响度标准化.CheckBoxSize = 20 * Form1.DPI
                Form1.UiCheckBox自定义音频参数.CheckBoxSize = 20 * Form1.DPI

            Case 选项卡.IsEqual(Form1.TabPage图片参数)
                Form1.UiCheckBox图片编码器.CheckBoxSize = 20 * Form1.DPI
                Form1.UiComboBox图片编码器.ItemHeight = 30 * Form1.DPI
                Form1.UiCheckBox图片分辨率.CheckBoxSize = 20 * Form1.DPI
                Form1.UiCheckBox自定义图片参数.CheckBoxSize = 20 * Form1.DPI

            Case 选项卡.IsEqual(Form1.TabPage流控制)
                Form1.UiCheckBox保留其他视频流.CheckBoxSize = 20 * Form1.DPI
                Form1.UiCheckBox保留其他音频流.CheckBoxSize = 20 * Form1.DPI
                Form1.UiCheckBox保留内嵌字幕流.CheckBoxSize = 20 * Form1.DPI
                Form1.UiCheckBox保留元数据.CheckBoxSize = 20 * Form1.DPI
                Form1.UiCheckBox保留章节信息.CheckBoxSize = 20 * Form1.DPI
                Form1.UiCheckBox快速剪辑.CheckBoxSize = 20 * Form1.DPI
                Form1.UiCheckBox自动混流同名字幕文件.CheckBoxSize = 20 * Form1.DPI
                Form1.UiCheckBox自定义filter_complex.CheckBoxSize = 20 * Form1.DPI
                Form1.UiCheckBox1.CheckBoxSize = 20 * Form1.DPI

            Case 选项卡.IsEqual(Form1.TabPage预设管理)
                Dim a As New 预设数据类型
                预设管理.储存预设(a)
                Form1.RichTextBox1.Text = "ffmpeg.exe " & 预设管理.将预设数据转换为命令行(a, "假装这是输入目录\假装这是输入文件", "假装这是输出目录\假装这是输出文件")

        End Select
    End Sub



End Class
