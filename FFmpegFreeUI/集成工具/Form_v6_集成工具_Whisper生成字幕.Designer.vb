<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_v6_集成工具_Whisper生成字幕
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。  
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        ModernPanel1 = New LakeUI.ModernPanel()
        HtmlColorLabel1 = New LakeUI.HtmlColorLabel()
        Panel4 = New Panel()
        HtmlColorLabel6 = New LakeUI.HtmlColorLabel()
        HtmlColorLabel5 = New LakeUI.HtmlColorLabel()
        Panel2 = New Panel()
        MTB_音频流索引 = New LakeUI.ModernTextBox()
        JustEmptyControl2 = New LakeUI.JustEmptyControl()
        MCB_输入文件 = New LakeUI.ModernComboBox()
        Panel1 = New Panel()
        MCB_选择模型文件 = New LakeUI.ModernComboBox()
        MTB_GPU设备索引 = New LakeUI.ModernTextBox()
        Panel3 = New Panel()
        HtmlColorLabel2 = New LakeUI.HtmlColorLabel()
        HtmlColorLabel3 = New LakeUI.HtmlColorLabel()
        Panel5 = New Panel()
        HtmlColorLabel7 = New LakeUI.HtmlColorLabel()
        Panel6 = New Panel()
        MCB_要识别的语言 = New LakeUI.ModernComboBox()
        JustEmptyControl4 = New LakeUI.JustEmptyControl()
        HtmlColorLabel4 = New LakeUI.HtmlColorLabel()
        MCB_字幕片段最大字符数 = New LakeUI.HtmlColorLabel()
        MCB_翻译成英语 = New LakeUI.ModernCheckBox()
        Panel7 = New Panel()
        Panel8 = New Panel()
        HtmlColorLabel9 = New LakeUI.HtmlColorLabel()
        HtmlColorLabel10 = New LakeUI.HtmlColorLabel()
        HtmlColorLabel11 = New LakeUI.HtmlColorLabel()
        Panel10 = New Panel()
        HtmlColorLabel12 = New LakeUI.HtmlColorLabel()
        MCB_VAD模型文件 = New LakeUI.ModernComboBox()
        Panel9 = New Panel()
        Panel11 = New Panel()
        JustEmptyControl7 = New LakeUI.JustEmptyControl()
        MCB_输出类型 = New LakeUI.ModernComboBox()
        Panel12 = New Panel()
        HtmlColorLabel14 = New LakeUI.HtmlColorLabel()
        HtmlColorLabel15 = New LakeUI.HtmlColorLabel()
        Panel13 = New Panel()
        MB_添加到编码队列 = New LakeUI.ModernButton()
        JustEmptyControl6 = New LakeUI.JustEmptyControl()
        MB_重置页面 = New LakeUI.ModernButton()
        JustEmptyControl8 = New LakeUI.JustEmptyControl()
        MCB_GPU加速选项 = New LakeUI.ModernComboBox()
        JustEmptyControl9 = New LakeUI.JustEmptyControl()
        HtmlColorLabel13 = New LakeUI.HtmlColorLabel()
        ETB_VAD阈值 = New LakeUI.ExcellentTrackBar()
        ETB_VAD最短语音持续时间 = New LakeUI.ExcellentTrackBar()
        ETB_VAD最短静音持续时间 = New LakeUI.ExcellentTrackBar()
        ETB_字幕片段最大字符数 = New LakeUI.ExcellentTrackBar()
        ETB_音频缓存长度 = New LakeUI.ExcellentTrackBar()
        MTB_输出文件 = New LakeUI.ModernTextBox()
        MB_输出文件另存为 = New LakeUI.ModernButton()
        JustEmptyControl1 = New LakeUI.JustEmptyControl()
        ModernPanel1.SuspendLayout()
        Panel4.SuspendLayout()
        Panel2.SuspendLayout()
        Panel1.SuspendLayout()
        Panel3.SuspendLayout()
        Panel5.SuspendLayout()
        Panel6.SuspendLayout()
        Panel7.SuspendLayout()
        Panel8.SuspendLayout()
        Panel10.SuspendLayout()
        Panel9.SuspendLayout()
        Panel11.SuspendLayout()
        Panel12.SuspendLayout()
        Panel13.SuspendLayout()
        SuspendLayout()
        '
        ' ModernPanel1
        '
        ModernPanel1.BackColor1 = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ModernPanel1.BorderSize = 0
        ModernPanel1.Controls.Add(Panel11)
        ModernPanel1.Controls.Add(Panel12)
        ModernPanel1.Controls.Add(Panel7)
        ModernPanel1.Controls.Add(Panel8)
        ModernPanel1.Controls.Add(Panel9)
        ModernPanel1.Controls.Add(Panel10)
        ModernPanel1.Controls.Add(Panel6)
        ModernPanel1.Controls.Add(Panel5)
        ModernPanel1.Controls.Add(Panel1)
        ModernPanel1.Controls.Add(Panel3)
        ModernPanel1.Controls.Add(Panel2)
        ModernPanel1.Controls.Add(Panel4)
        ModernPanel1.Controls.Add(Panel13)
        ModernPanel1.Controls.Add(HtmlColorLabel1)
        ModernPanel1.Dock = DockStyle.Fill
        ModernPanel1.Location = New Point(0, 0)
        ModernPanel1.Name = "ModernPanel1"
        ModernPanel1.Padding = New Padding(20)
        ModernPanel1.Size = New Size(794, 646)
        ModernPanel1.TabIndex = 0
        '
        ' HtmlColorLabel1
        '
        HtmlColorLabel1.AutoSize = True
        HtmlColorLabel1.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel1.Dock = DockStyle.Top
        HtmlColorLabel1.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HtmlColorLabel1.Location = New Point(20, 20)
        HtmlColorLabel1.Margin = New Padding(2)
        HtmlColorLabel1.Name = "HtmlColorLabel1"
        HtmlColorLabel1.Size = New Size(754, 25)
        HtmlColorLabel1.TabIndex = 4
        HtmlColorLabel1.Text = "<span style=""font-size:13; color:Silver"">Whisper 音频滤镜生成字幕</span>   如果有批量生成需求可以直接用标准流程的完全自己写模式"
        '
        ' Panel4
        '
        Panel4.Controls.Add(HtmlColorLabel5)
        Panel4.Controls.Add(HtmlColorLabel6)
        Panel4.Dock = DockStyle.Top
        Panel4.Location = New Point(20, 97)
        Panel4.Name = "Panel4"
        Panel4.Size = New Size(754, 30)
        Panel4.TabIndex = 10
        '
        ' HtmlColorLabel6
        '
        HtmlColorLabel6.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel6.Dock = DockStyle.Right
        HtmlColorLabel6.Location = New Point(634, 0)
        HtmlColorLabel6.Margin = New Padding(2)
        HtmlColorLabel6.Name = "HtmlColorLabel6"
        HtmlColorLabel6.Size = New Size(120, 30)
        HtmlColorLabel6.TabIndex = 1
        HtmlColorLabel6.Text = "音频流索引"
        HtmlColorLabel6.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.BottomLeft
        '
        ' HtmlColorLabel5
        '
        HtmlColorLabel5.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel5.Dock = DockStyle.Fill
        HtmlColorLabel5.Location = New Point(0, 0)
        HtmlColorLabel5.Margin = New Padding(2)
        HtmlColorLabel5.Name = "HtmlColorLabel5"
        HtmlColorLabel5.Size = New Size(634, 30)
        HtmlColorLabel5.TabIndex = 0
        HtmlColorLabel5.Text = "输入文件"
        HtmlColorLabel5.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.BottomLeft
        '
        ' Panel2
        '
        Panel2.Controls.Add(MCB_输入文件)
        Panel2.Controls.Add(JustEmptyControl2)
        Panel2.Controls.Add(MTB_音频流索引)
        Panel2.Dock = DockStyle.Top
        Panel2.Location = New Point(20, 127)
        Panel2.Name = "Panel2"
        Panel2.Padding = New Padding(0, 10, 0, 0)
        Panel2.Size = New Size(754, 42)
        Panel2.TabIndex = 11
        '
        ' MTB_音频流索引
        '
        MTB_音频流索引.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_音频流索引.BorderColor = Color.Transparent
        MTB_音频流索引.BorderColorFocus = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MTB_音频流索引.BorderRadius = 10
        MTB_音频流索引.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        MTB_音频流索引.Dock = DockStyle.Right
        MTB_音频流索引.Location = New Point(634, 10)
        MTB_音频流索引.Margin = New Padding(2)
        MTB_音频流索引.Name = "MTB_音频流索引"
        MTB_音频流索引.Padding = New Padding(10, 0, 10, 0)
        MTB_音频流索引.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_音频流索引.Size = New Size(120, 32)
        MTB_音频流索引.TabIndex = 12
        MTB_音频流索引.WaterText = "质量值"
        MTB_音频流索引.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        '
        ' JustEmptyControl2
        '
        JustEmptyControl2.Dock = DockStyle.Right
        JustEmptyControl2.Location = New Point(623, 10)
        JustEmptyControl2.Name = "JustEmptyControl2"
        JustEmptyControl2.Size = New Size(11, 32)
        JustEmptyControl2.TabIndex = 9
        '
        ' MCB_输入文件
        '
        MCB_输入文件.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_输入文件.BorderRadius = 10
        MCB_输入文件.BorderSize = 0
        MCB_输入文件.Dock = DockStyle.Fill
        MCB_输入文件.DropDownBackdropBlurPasses = 2
        MCB_输入文件.DropDownBackdropBlurRadius = 30
        MCB_输入文件.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_输入文件.DropDownHoverColor = Color.FromArgb(CByte(20), CByte(220), CByte(220), CByte(220))
        MCB_输入文件.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_输入文件.DropDownPadding = New Padding(10)
        MCB_输入文件.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_输入文件.DropDownSelectedForeColor = Color.White
        MCB_输入文件.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_输入文件.Location = New Point(0, 10)
        MCB_输入文件.Margin = New Padding(2, 2, 2, 2)
        MCB_输入文件.Name = "MCB_输入文件"
        MCB_输入文件.Padding = New Padding(10, 0, 10, 0)
        MCB_输入文件.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_输入文件.Size = New Size(623, 32)
        MCB_输入文件.TabIndex = 0
        MCB_输入文件.ToolTipGap = -1
        MCB_输入文件.ToolTipMaxWidth = 350
        MCB_输入文件.ToolTipPadding = New Padding(15)
        MCB_输入文件.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        '
        ' Panel1
        '
        Panel1.Controls.Add(MCB_选择模型文件)
        Panel1.Controls.Add(JustEmptyControl8)
        Panel1.Controls.Add(MCB_GPU加速选项)
        Panel1.Controls.Add(JustEmptyControl9)
        Panel1.Controls.Add(MTB_GPU设备索引)
        Panel1.Dock = DockStyle.Top
        Panel1.Location = New Point(20, 199)
        Panel1.Name = "Panel1"
        Panel1.Padding = New Padding(0, 10, 0, 0)
        Panel1.Size = New Size(754, 42)
        Panel1.TabIndex = 13
        '
        ' MCB_选择模型文件
        '
        MCB_选择模型文件.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_选择模型文件.BorderRadius = 10
        MCB_选择模型文件.BorderSize = 0
        MCB_选择模型文件.Dock = DockStyle.Fill
        MCB_选择模型文件.DropDownBackdropBlurPasses = 2
        MCB_选择模型文件.DropDownBackdropBlurRadius = 30
        MCB_选择模型文件.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_选择模型文件.DropDownHoverColor = Color.FromArgb(CByte(20), CByte(220), CByte(220), CByte(220))
        MCB_选择模型文件.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_选择模型文件.DropDownPadding = New Padding(10)
        MCB_选择模型文件.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_选择模型文件.DropDownSelectedForeColor = Color.White
        MCB_选择模型文件.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_选择模型文件.Location = New Point(0, 10)
        MCB_选择模型文件.Margin = New Padding(2, 2, 2, 2)
        MCB_选择模型文件.Name = "MCB_选择模型文件"
        MCB_选择模型文件.Padding = New Padding(10, 0, 10, 0)
        MCB_选择模型文件.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_选择模型文件.Size = New Size(488, 32)
        MCB_选择模型文件.TabIndex = 0
        MCB_选择模型文件.ToolTipGap = -1
        MCB_选择模型文件.ToolTipMaxWidth = 350
        MCB_选择模型文件.ToolTipPadding = New Padding(15)
        MCB_选择模型文件.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        '
        ' MTB_GPU设备索引
        '
        MTB_GPU设备索引.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_GPU设备索引.BorderColor = Color.Transparent
        MTB_GPU设备索引.BorderColorFocus = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MTB_GPU设备索引.BorderRadius = 10
        MTB_GPU设备索引.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        MTB_GPU设备索引.Dock = DockStyle.Right
        MTB_GPU设备索引.Location = New Point(634, 10)
        MTB_GPU设备索引.Margin = New Padding(2)
        MTB_GPU设备索引.Name = "MTB_GPU设备索引"
        MTB_GPU设备索引.Padding = New Padding(10, 0, 10, 0)
        MTB_GPU设备索引.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_GPU设备索引.Size = New Size(120, 32)
        MTB_GPU设备索引.TabIndex = 12
        MTB_GPU设备索引.WaterText = "质量值"
        MTB_GPU设备索引.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        '
        ' Panel3
        '
        Panel3.Controls.Add(HtmlColorLabel2)
        Panel3.Controls.Add(HtmlColorLabel13)
        Panel3.Controls.Add(HtmlColorLabel3)
        Panel3.Dock = DockStyle.Top
        Panel3.Location = New Point(20, 169)
        Panel3.Name = "Panel3"
        Panel3.Size = New Size(754, 30)
        Panel3.TabIndex = 12
        '
        ' HtmlColorLabel2
        '
        HtmlColorLabel2.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel2.Dock = DockStyle.Fill
        HtmlColorLabel2.Location = New Point(0, 0)
        HtmlColorLabel2.Margin = New Padding(2)
        HtmlColorLabel2.Name = "HtmlColorLabel2"
        HtmlColorLabel2.Size = New Size(501, 30)
        HtmlColorLabel2.TabIndex = 0
        HtmlColorLabel2.Text = "选择模型文件"
        HtmlColorLabel2.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.BottomLeft
        '
        ' HtmlColorLabel3
        '
        HtmlColorLabel3.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel3.Dock = DockStyle.Right
        HtmlColorLabel3.Location = New Point(634, 0)
        HtmlColorLabel3.Margin = New Padding(2)
        HtmlColorLabel3.Name = "HtmlColorLabel3"
        HtmlColorLabel3.Size = New Size(120, 30)
        HtmlColorLabel3.TabIndex = 2
        HtmlColorLabel3.Text = "GPU 设备索引"
        HtmlColorLabel3.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.BottomLeft
        '
        ' Panel5
        '
        Panel5.Controls.Add(MCB_字幕片段最大字符数)
        Panel5.Controls.Add(HtmlColorLabel4)
        Panel5.Controls.Add(HtmlColorLabel7)
        Panel5.Dock = DockStyle.Top
        Panel5.Location = New Point(20, 241)
        Panel5.Name = "Panel5"
        Panel5.Size = New Size(754, 30)
        Panel5.TabIndex = 14
        '
        ' HtmlColorLabel7
        '
        HtmlColorLabel7.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel7.Dock = DockStyle.Left
        HtmlColorLabel7.Location = New Point(0, 0)
        HtmlColorLabel7.Margin = New Padding(2)
        HtmlColorLabel7.Name = "HtmlColorLabel7"
        HtmlColorLabel7.Size = New Size(161, 30)
        HtmlColorLabel7.TabIndex = 0
        HtmlColorLabel7.Text = "要识别的语言"
        HtmlColorLabel7.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.BottomLeft
        '
        ' Panel6
        '
        Panel6.Controls.Add(MCB_翻译成英语)
        Panel6.Controls.Add(ETB_字幕片段最大字符数)
        Panel6.Controls.Add(ETB_音频缓存长度)
        Panel6.Controls.Add(JustEmptyControl4)
        Panel6.Controls.Add(MCB_要识别的语言)
        Panel6.Dock = DockStyle.Top
        Panel6.Location = New Point(20, 271)
        Panel6.Name = "Panel6"
        Panel6.Padding = New Padding(0, 10, 0, 0)
        Panel6.Size = New Size(754, 42)
        Panel6.TabIndex = 15
        '
        ' MCB_要识别的语言
        '
        MCB_要识别的语言.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_要识别的语言.BorderRadius = 10
        MCB_要识别的语言.BorderSize = 0
        MCB_要识别的语言.Dock = DockStyle.Left
        MCB_要识别的语言.DropDownBackdropBlurPasses = 2
        MCB_要识别的语言.DropDownBackdropBlurRadius = 30
        MCB_要识别的语言.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_要识别的语言.DropDownHoverColor = Color.FromArgb(CByte(20), CByte(220), CByte(220), CByte(220))
        MCB_要识别的语言.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_要识别的语言.DropDownPadding = New Padding(10)
        MCB_要识别的语言.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_要识别的语言.DropDownSelectedForeColor = Color.White
        MCB_要识别的语言.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_要识别的语言.Location = New Point(0, 10)
        MCB_要识别的语言.Margin = New Padding(2, 2, 2, 2)
        MCB_要识别的语言.Name = "MCB_要识别的语言"
        MCB_要识别的语言.Padding = New Padding(10, 0, 10, 0)
        MCB_要识别的语言.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_要识别的语言.Size = New Size(150, 32)
        MCB_要识别的语言.TabIndex = 0
        MCB_要识别的语言.ToolTipGap = -1
        MCB_要识别的语言.ToolTipMaxWidth = 350
        MCB_要识别的语言.ToolTipPadding = New Padding(15)
        MCB_要识别的语言.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        '
        ' JustEmptyControl4
        '
        JustEmptyControl4.Dock = DockStyle.Left
        JustEmptyControl4.Location = New Point(150, 10)
        JustEmptyControl4.Name = "JustEmptyControl4"
        JustEmptyControl4.Size = New Size(13, 32)
        JustEmptyControl4.TabIndex = 10
        '
        ' HtmlColorLabel4
        '
        HtmlColorLabel4.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel4.Dock = DockStyle.Left
        HtmlColorLabel4.Location = New Point(161, 0)
        HtmlColorLabel4.Margin = New Padding(2)
        HtmlColorLabel4.Name = "HtmlColorLabel4"
        HtmlColorLabel4.Size = New Size(162, 30)
        HtmlColorLabel4.TabIndex = 1
        HtmlColorLabel4.Text = "音频缓存长度"
        HtmlColorLabel4.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.BottomLeft
        '
        ' MCB_字幕片段最大字符数
        '
        MCB_字幕片段最大字符数.AutoSizeMode = AutoSizeMode.GrowAndShrink
        MCB_字幕片段最大字符数.Dock = DockStyle.Left
        MCB_字幕片段最大字符数.Location = New Point(323, 0)
        MCB_字幕片段最大字符数.Margin = New Padding(2)
        MCB_字幕片段最大字符数.Name = "MCB_字幕片段最大字符数"
        MCB_字幕片段最大字符数.Size = New Size(160, 30)
        MCB_字幕片段最大字符数.TabIndex = 2
        MCB_字幕片段最大字符数.Text = "字幕片段最大字符数"
        MCB_字幕片段最大字符数.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.BottomLeft
        '
        ' MCB_翻译成英语
        '
        MCB_翻译成英语.BoxBorderRadius = 5
        MCB_翻译成英语.BoxBorderSize = 0
        MCB_翻译成英语.BoxCheckedBackColor = Color.OliveDrab
        MCB_翻译成英语.BoxInnerPadding = 6
        MCB_翻译成英语.BoxSize = 22
        MCB_翻译成英语.BoxTextSpacing = 10
        MCB_翻译成英语.BoxUncheckedBackColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_翻译成英语.Dock = DockStyle.Fill
        MCB_翻译成英语.Location = New Point(483, 10)
        MCB_翻译成英语.Name = "MCB_翻译成英语"
        MCB_翻译成英语.Size = New Size(271, 32)
        MCB_翻译成英语.TabIndex = 16
        MCB_翻译成英语.Text = "翻译成英语"
        '
        ' Panel7
        '
        Panel7.Controls.Add(ETB_VAD最短静音持续时间)
        Panel7.Controls.Add(ETB_VAD最短语音持续时间)
        Panel7.Controls.Add(ETB_VAD阈值)
        Panel7.Dock = DockStyle.Top
        Panel7.Location = New Point(20, 415)
        Panel7.Name = "Panel7"
        Panel7.Padding = New Padding(0, 10, 0, 0)
        Panel7.Size = New Size(754, 42)
        Panel7.TabIndex = 17
        '
        ' Panel8
        '
        Panel8.Controls.Add(HtmlColorLabel9)
        Panel8.Controls.Add(HtmlColorLabel10)
        Panel8.Controls.Add(HtmlColorLabel11)
        Panel8.Dock = DockStyle.Top
        Panel8.Location = New Point(20, 385)
        Panel8.Name = "Panel8"
        Panel8.Size = New Size(754, 30)
        Panel8.TabIndex = 16
        '
        ' HtmlColorLabel9
        '
        HtmlColorLabel9.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel9.Dock = DockStyle.Left
        HtmlColorLabel9.Location = New Point(400, 0)
        HtmlColorLabel9.Margin = New Padding(2)
        HtmlColorLabel9.Name = "HtmlColorLabel9"
        HtmlColorLabel9.Size = New Size(200, 30)
        HtmlColorLabel9.TabIndex = 2
        HtmlColorLabel9.Text = "VAD 最短静音持续时间"
        HtmlColorLabel9.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.BottomLeft
        '
        ' HtmlColorLabel10
        '
        HtmlColorLabel10.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel10.Dock = DockStyle.Left
        HtmlColorLabel10.Location = New Point(200, 0)
        HtmlColorLabel10.Margin = New Padding(2)
        HtmlColorLabel10.Name = "HtmlColorLabel10"
        HtmlColorLabel10.Size = New Size(200, 30)
        HtmlColorLabel10.TabIndex = 1
        HtmlColorLabel10.Text = "VAD 最短语音持续时间"
        HtmlColorLabel10.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.BottomLeft
        '
        ' HtmlColorLabel11
        '
        HtmlColorLabel11.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel11.Dock = DockStyle.Left
        HtmlColorLabel11.Location = New Point(0, 0)
        HtmlColorLabel11.Margin = New Padding(2)
        HtmlColorLabel11.Name = "HtmlColorLabel11"
        HtmlColorLabel11.Size = New Size(200, 30)
        HtmlColorLabel11.TabIndex = 0
        HtmlColorLabel11.Text = "VAD 阈值"
        HtmlColorLabel11.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.BottomLeft
        '
        ' Panel10
        '
        Panel10.Controls.Add(HtmlColorLabel12)
        Panel10.Dock = DockStyle.Top
        Panel10.Location = New Point(20, 313)
        Panel10.Name = "Panel10"
        Panel10.Size = New Size(754, 30)
        Panel10.TabIndex = 18
        '
        ' HtmlColorLabel12
        '
        HtmlColorLabel12.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel12.Dock = DockStyle.Fill
        HtmlColorLabel12.Location = New Point(0, 0)
        HtmlColorLabel12.Margin = New Padding(2)
        HtmlColorLabel12.Name = "HtmlColorLabel12"
        HtmlColorLabel12.Size = New Size(754, 30)
        HtmlColorLabel12.TabIndex = 0
        HtmlColorLabel12.Text = "VAD 语音活动检测模型文件"
        HtmlColorLabel12.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.BottomLeft
        '
        ' MCB_VAD模型文件
        '
        MCB_VAD模型文件.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_VAD模型文件.BorderRadius = 10
        MCB_VAD模型文件.BorderSize = 0
        MCB_VAD模型文件.Dock = DockStyle.Fill
        MCB_VAD模型文件.DropDownBackdropBlurPasses = 2
        MCB_VAD模型文件.DropDownBackdropBlurRadius = 30
        MCB_VAD模型文件.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_VAD模型文件.DropDownHoverColor = Color.FromArgb(CByte(20), CByte(220), CByte(220), CByte(220))
        MCB_VAD模型文件.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_VAD模型文件.DropDownPadding = New Padding(10)
        MCB_VAD模型文件.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_VAD模型文件.DropDownSelectedForeColor = Color.White
        MCB_VAD模型文件.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_VAD模型文件.Location = New Point(0, 10)
        MCB_VAD模型文件.Margin = New Padding(2, 2, 2, 2)
        MCB_VAD模型文件.Name = "MCB_VAD模型文件"
        MCB_VAD模型文件.Padding = New Padding(10, 0, 10, 0)
        MCB_VAD模型文件.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_VAD模型文件.Size = New Size(754, 32)
        MCB_VAD模型文件.TabIndex = 0
        MCB_VAD模型文件.ToolTipGap = -1
        MCB_VAD模型文件.ToolTipMaxWidth = 350
        MCB_VAD模型文件.ToolTipPadding = New Padding(15)
        MCB_VAD模型文件.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        '
        ' Panel9
        '
        Panel9.Controls.Add(MCB_VAD模型文件)
        Panel9.Dock = DockStyle.Top
        Panel9.Location = New Point(20, 343)
        Panel9.Name = "Panel9"
        Panel9.Padding = New Padding(0, 10, 0, 0)
        Panel9.Size = New Size(754, 42)
        Panel9.TabIndex = 19
        '
        ' Panel11
        '
        Panel11.Controls.Add(MTB_输出文件)
        Panel11.Controls.Add(JustEmptyControl7)
        Panel11.Controls.Add(MCB_输出类型)
        Panel11.Dock = DockStyle.Top
        Panel11.Location = New Point(20, 487)
        Panel11.Name = "Panel11"
        Panel11.Padding = New Padding(0, 10, 0, 0)
        Panel11.Size = New Size(754, 42)
        Panel11.TabIndex = 21
        '
        ' JustEmptyControl7
        '
        JustEmptyControl7.Dock = DockStyle.Left
        JustEmptyControl7.Location = New Point(150, 10)
        JustEmptyControl7.Name = "JustEmptyControl7"
        JustEmptyControl7.Size = New Size(10, 32)
        JustEmptyControl7.TabIndex = 10
        '
        ' MCB_输出类型
        '
        MCB_输出类型.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_输出类型.BorderRadius = 10
        MCB_输出类型.BorderSize = 0
        MCB_输出类型.Dock = DockStyle.Left
        MCB_输出类型.DropDownBackdropBlurPasses = 2
        MCB_输出类型.DropDownBackdropBlurRadius = 30
        MCB_输出类型.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_输出类型.DropDownHoverColor = Color.FromArgb(CByte(20), CByte(220), CByte(220), CByte(220))
        MCB_输出类型.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_输出类型.DropDownPadding = New Padding(10)
        MCB_输出类型.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_输出类型.DropDownSelectedForeColor = Color.White
        MCB_输出类型.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_输出类型.Location = New Point(0, 10)
        MCB_输出类型.Margin = New Padding(2, 2, 2, 2)
        MCB_输出类型.Name = "MCB_输出类型"
        MCB_输出类型.Padding = New Padding(10, 0, 10, 0)
        MCB_输出类型.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_输出类型.Size = New Size(150, 32)
        MCB_输出类型.TabIndex = 0
        MCB_输出类型.ToolTipGap = -1
        MCB_输出类型.ToolTipMaxWidth = 350
        MCB_输出类型.ToolTipPadding = New Padding(15)
        MCB_输出类型.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        '
        ' Panel12
        '
        Panel12.Controls.Add(HtmlColorLabel14)
        Panel12.Controls.Add(HtmlColorLabel15)
        Panel12.Dock = DockStyle.Top
        Panel12.Location = New Point(20, 457)
        Panel12.Name = "Panel12"
        Panel12.Size = New Size(754, 30)
        Panel12.TabIndex = 20
        '
        ' HtmlColorLabel14
        '
        HtmlColorLabel14.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel14.Dock = DockStyle.Fill
        HtmlColorLabel14.Location = New Point(160, 0)
        HtmlColorLabel14.Margin = New Padding(2)
        HtmlColorLabel14.Name = "HtmlColorLabel14"
        HtmlColorLabel14.Size = New Size(594, 30)
        HtmlColorLabel14.TabIndex = 1
        HtmlColorLabel14.Text = "输出文件"
        HtmlColorLabel14.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.BottomLeft
        '
        ' HtmlColorLabel15
        '
        HtmlColorLabel15.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel15.Dock = DockStyle.Left
        HtmlColorLabel15.Location = New Point(0, 0)
        HtmlColorLabel15.Margin = New Padding(2)
        HtmlColorLabel15.Name = "HtmlColorLabel15"
        HtmlColorLabel15.Size = New Size(160, 30)
        HtmlColorLabel15.TabIndex = 0
        HtmlColorLabel15.Text = "输出类型"
        HtmlColorLabel15.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.BottomLeft
        '
        ' Panel13
        '
        Panel13.Controls.Add(MB_输出文件另存为)
        Panel13.Controls.Add(JustEmptyControl1)
        Panel13.Controls.Add(MB_重置页面)
        Panel13.Controls.Add(JustEmptyControl6)
        Panel13.Controls.Add(MB_添加到编码队列)
        Panel13.Dock = DockStyle.Top
        Panel13.Location = New Point(20, 45)
        Panel13.Name = "Panel13"
        Panel13.Padding = New Padding(0, 10, 0, 10)
        Panel13.Size = New Size(754, 52)
        Panel13.TabIndex = 22
        '
        ' MB_添加到编码队列
        '
        MB_添加到编码队列.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_添加到编码队列.BorderRadius = 10
        MB_添加到编码队列.BorderSize = 0
        MB_添加到编码队列.Dock = DockStyle.Left
        MB_添加到编码队列.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_添加到编码队列.Location = New Point(0, 10)
        MB_添加到编码队列.Margin = New Padding(2)
        MB_添加到编码队列.Name = "MB_添加到编码队列"
        MB_添加到编码队列.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_添加到编码队列.Size = New Size(150, 32)
        MB_添加到编码队列.TabIndex = 0
        MB_添加到编码队列.Text = "添加到编码队列"
        '
        ' JustEmptyControl6
        '
        JustEmptyControl6.Dock = DockStyle.Left
        JustEmptyControl6.Location = New Point(150, 10)
        JustEmptyControl6.Name = "JustEmptyControl6"
        JustEmptyControl6.Size = New Size(10, 32)
        JustEmptyControl6.TabIndex = 3
        '
        ' MB_重置页面
        '
        MB_重置页面.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_重置页面.BorderRadius = 10
        MB_重置页面.BorderSize = 0
        MB_重置页面.Dock = DockStyle.Left
        MB_重置页面.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_重置页面.Location = New Point(160, 10)
        MB_重置页面.Margin = New Padding(2)
        MB_重置页面.Name = "MB_重置页面"
        MB_重置页面.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_重置页面.Size = New Size(100, 32)
        MB_重置页面.TabIndex = 4
        MB_重置页面.Text = "重置页面"
        '
        ' JustEmptyControl8
        '
        JustEmptyControl8.Dock = DockStyle.Right
        JustEmptyControl8.Location = New Point(488, 10)
        JustEmptyControl8.Name = "JustEmptyControl8"
        JustEmptyControl8.Size = New Size(13, 32)
        JustEmptyControl8.TabIndex = 15
        '
        ' MCB_GPU加速选项
        '
        MCB_GPU加速选项.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_GPU加速选项.BorderRadius = 10
        MCB_GPU加速选项.BorderSize = 0
        MCB_GPU加速选项.Dock = DockStyle.Right
        MCB_GPU加速选项.DropDownBackdropBlurPasses = 2
        MCB_GPU加速选项.DropDownBackdropBlurRadius = 30
        MCB_GPU加速选项.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_GPU加速选项.DropDownHoverColor = Color.FromArgb(CByte(20), CByte(220), CByte(220), CByte(220))
        MCB_GPU加速选项.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_GPU加速选项.DropDownPadding = New Padding(10)
        MCB_GPU加速选项.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_GPU加速选项.DropDownSelectedForeColor = Color.White
        MCB_GPU加速选项.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_GPU加速选项.Location = New Point(501, 10)
        MCB_GPU加速选项.Margin = New Padding(2, 2, 2, 2)
        MCB_GPU加速选项.Name = "MCB_GPU加速选项"
        MCB_GPU加速选项.Padding = New Padding(10, 0, 10, 0)
        MCB_GPU加速选项.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_GPU加速选项.Size = New Size(120, 32)
        MCB_GPU加速选项.TabIndex = 14
        MCB_GPU加速选项.ToolTipGap = -1
        MCB_GPU加速选项.ToolTipMaxWidth = 350
        MCB_GPU加速选项.ToolTipPadding = New Padding(15)
        MCB_GPU加速选项.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        '
        ' JustEmptyControl9
        '
        JustEmptyControl9.Dock = DockStyle.Right
        JustEmptyControl9.Location = New Point(621, 10)
        JustEmptyControl9.Name = "JustEmptyControl9"
        JustEmptyControl9.Size = New Size(13, 32)
        JustEmptyControl9.TabIndex = 13
        '
        ' HtmlColorLabel13
        '
        HtmlColorLabel13.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel13.Dock = DockStyle.Right
        HtmlColorLabel13.Location = New Point(501, 0)
        HtmlColorLabel13.Margin = New Padding(2)
        HtmlColorLabel13.Name = "HtmlColorLabel13"
        HtmlColorLabel13.Size = New Size(133, 30)
        HtmlColorLabel13.TabIndex = 3
        HtmlColorLabel13.Text = "GPU 加速"
        HtmlColorLabel13.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.BottomLeft
        '
        ' ETB_VAD阈值
        '
        ETB_VAD阈值.Dock = DockStyle.Left
        ETB_VAD阈值.LabelColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ETB_VAD阈值.LabelLineColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ETB_VAD阈值.LabelLineLength = 16
        ETB_VAD阈值.Location = New Point(0, 10)
        ETB_VAD阈值.Margin = New Padding(2, 2, 2, 2)
        ETB_VAD阈值.Maximum = 10R
        ETB_VAD阈值.Name = "ETB_VAD阈值"
        ETB_VAD阈值.Padding = New Padding(0, 0, 10, 0)
        ETB_VAD阈值.Size = New Size(200, 32)
        ETB_VAD阈值.TabIndex = 28
        ETB_VAD阈值.ThumbBorderWidth = 0
        ETB_VAD阈值.ThumbColor = Color.IndianRed
        ETB_VAD阈值.ThumbHeight = 28
        ETB_VAD阈值.ThumbTextDecimalPlaces = 1
        ETB_VAD阈值.ThumbTextMode = LakeUI.ExcellentTrackBar.ThumbTextModeEnum.Value
        ETB_VAD阈值.ThumbWidth = 38
        ETB_VAD阈值.TrackColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ETB_VAD阈值.TrackFillColor = Color.Transparent
        '
        ' ETB_VAD最短语音持续时间
        '
        ETB_VAD最短语音持续时间.Dock = DockStyle.Left
        ETB_VAD最短语音持续时间.LabelColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ETB_VAD最短语音持续时间.LabelLineColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ETB_VAD最短语音持续时间.LabelLineLength = 16
        ETB_VAD最短语音持续时间.Location = New Point(200, 10)
        ETB_VAD最短语音持续时间.Margin = New Padding(2, 2, 2, 2)
        ETB_VAD最短语音持续时间.Maximum = 10R
        ETB_VAD最短语音持续时间.Name = "ETB_VAD最短语音持续时间"
        ETB_VAD最短语音持续时间.Padding = New Padding(0, 0, 10, 0)
        ETB_VAD最短语音持续时间.Size = New Size(200, 32)
        ETB_VAD最短语音持续时间.TabIndex = 29
        ETB_VAD最短语音持续时间.ThumbBorderWidth = 0
        ETB_VAD最短语音持续时间.ThumbColor = Color.OliveDrab
        ETB_VAD最短语音持续时间.ThumbHeight = 28
        ETB_VAD最短语音持续时间.ThumbTextDecimalPlaces = 1
        ETB_VAD最短语音持续时间.ThumbTextMode = LakeUI.ExcellentTrackBar.ThumbTextModeEnum.Value
        ETB_VAD最短语音持续时间.ThumbWidth = 38
        ETB_VAD最短语音持续时间.TrackColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ETB_VAD最短语音持续时间.TrackFillColor = Color.Transparent
        '
        ' ETB_VAD最短静音持续时间
        '
        ETB_VAD最短静音持续时间.Dock = DockStyle.Left
        ETB_VAD最短静音持续时间.LabelColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ETB_VAD最短静音持续时间.LabelLineColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ETB_VAD最短静音持续时间.LabelLineLength = 16
        ETB_VAD最短静音持续时间.Location = New Point(400, 10)
        ETB_VAD最短静音持续时间.Margin = New Padding(2, 2, 2, 2)
        ETB_VAD最短静音持续时间.Maximum = 10R
        ETB_VAD最短静音持续时间.Name = "ETB_VAD最短静音持续时间"
        ETB_VAD最短静音持续时间.Padding = New Padding(0, 0, 10, 0)
        ETB_VAD最短静音持续时间.Size = New Size(200, 32)
        ETB_VAD最短静音持续时间.TabIndex = 30
        ETB_VAD最短静音持续时间.ThumbBorderWidth = 0
        ETB_VAD最短静音持续时间.ThumbColor = Color.CornflowerBlue
        ETB_VAD最短静音持续时间.ThumbHeight = 28
        ETB_VAD最短静音持续时间.ThumbTextDecimalPlaces = 1
        ETB_VAD最短静音持续时间.ThumbTextMode = LakeUI.ExcellentTrackBar.ThumbTextModeEnum.Value
        ETB_VAD最短静音持续时间.ThumbWidth = 38
        ETB_VAD最短静音持续时间.TrackColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ETB_VAD最短静音持续时间.TrackFillColor = Color.Transparent
        '
        ' ETB_字幕片段最大字符数
        '
        ETB_字幕片段最大字符数.Dock = DockStyle.Left
        ETB_字幕片段最大字符数.LabelColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ETB_字幕片段最大字符数.LabelLineColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ETB_字幕片段最大字符数.LabelLineLength = 16
        ETB_字幕片段最大字符数.Location = New Point(323, 10)
        ETB_字幕片段最大字符数.Margin = New Padding(2, 2, 2, 2)
        ETB_字幕片段最大字符数.Maximum = 10R
        ETB_字幕片段最大字符数.Name = "ETB_字幕片段最大字符数"
        ETB_字幕片段最大字符数.Padding = New Padding(0, 0, 10, 0)
        ETB_字幕片段最大字符数.Size = New Size(160, 32)
        ETB_字幕片段最大字符数.TabIndex = 32
        ETB_字幕片段最大字符数.ThumbBorderWidth = 0
        ETB_字幕片段最大字符数.ThumbColor = Color.CornflowerBlue
        ETB_字幕片段最大字符数.ThumbHeight = 28
        ETB_字幕片段最大字符数.ThumbTextDecimalPlaces = 1
        ETB_字幕片段最大字符数.ThumbTextMode = LakeUI.ExcellentTrackBar.ThumbTextModeEnum.Value
        ETB_字幕片段最大字符数.ThumbWidth = 38
        ETB_字幕片段最大字符数.TrackColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ETB_字幕片段最大字符数.TrackFillColor = Color.Transparent
        '
        ' ETB_音频缓存长度
        '
        ETB_音频缓存长度.Dock = DockStyle.Left
        ETB_音频缓存长度.LabelColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ETB_音频缓存长度.LabelLineColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ETB_音频缓存长度.LabelLineLength = 16
        ETB_音频缓存长度.Location = New Point(163, 10)
        ETB_音频缓存长度.Margin = New Padding(2, 2, 2, 2)
        ETB_音频缓存长度.Maximum = 10R
        ETB_音频缓存长度.Name = "ETB_音频缓存长度"
        ETB_音频缓存长度.Padding = New Padding(0, 0, 10, 0)
        ETB_音频缓存长度.Size = New Size(160, 32)
        ETB_音频缓存长度.TabIndex = 31
        ETB_音频缓存长度.ThumbBorderWidth = 0
        ETB_音频缓存长度.ThumbColor = Color.OliveDrab
        ETB_音频缓存长度.ThumbHeight = 28
        ETB_音频缓存长度.ThumbTextDecimalPlaces = 1
        ETB_音频缓存长度.ThumbTextMode = LakeUI.ExcellentTrackBar.ThumbTextModeEnum.Value
        ETB_音频缓存长度.ThumbWidth = 38
        ETB_音频缓存长度.TrackColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ETB_音频缓存长度.TrackFillColor = Color.Transparent
        '
        ' MTB_输出文件
        '
        MTB_输出文件.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_输出文件.BorderColor = Color.Transparent
        MTB_输出文件.BorderColorFocus = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MTB_输出文件.BorderRadius = 10
        MTB_输出文件.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        MTB_输出文件.Dock = DockStyle.Fill
        MTB_输出文件.Location = New Point(160, 10)
        MTB_输出文件.Margin = New Padding(2)
        MTB_输出文件.Name = "MTB_输出文件"
        MTB_输出文件.Padding = New Padding(10, 0, 10, 0)
        MTB_输出文件.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_输出文件.Size = New Size(594, 32)
        MTB_输出文件.TabIndex = 13
        MTB_输出文件.WaterText = "输出文件"
        MTB_输出文件.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        '
        ' MB_输出文件另存为
        '
        MB_输出文件另存为.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_输出文件另存为.BorderRadius = 10
        MB_输出文件另存为.BorderSize = 0
        MB_输出文件另存为.Dock = DockStyle.Left
        MB_输出文件另存为.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_输出文件另存为.Location = New Point(270, 10)
        MB_输出文件另存为.Margin = New Padding(2)
        MB_输出文件另存为.Name = "MB_输出文件另存为"
        MB_输出文件另存为.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_输出文件另存为.Size = New Size(150, 32)
        MB_输出文件另存为.TabIndex = 6
        MB_输出文件另存为.Text = "输出文件另存为"
        '
        ' JustEmptyControl1
        '
        JustEmptyControl1.Dock = DockStyle.Left
        JustEmptyControl1.Location = New Point(260, 10)
        JustEmptyControl1.Name = "JustEmptyControl1"
        JustEmptyControl1.Size = New Size(10, 32)
        JustEmptyControl1.TabIndex = 5
        '
        ' Form_v6_集成工具_Whisper生成字幕
        '
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ClientSize = New Size(794, 646)
        Controls.Add(ModernPanel1)
        Font = New Font("Microsoft YaHei UI", 10F)
        ForeColor = Color.Silver
        Name = "Form_v6_集成工具_Whisper生成字幕"
        Text = "Form_v6_集成工具_Whisper生成字幕"
        ModernPanel1.ResumeLayout(False)
        ModernPanel1.PerformLayout()
        Panel4.ResumeLayout(False)
        Panel2.ResumeLayout(False)
        Panel1.ResumeLayout(False)
        Panel3.ResumeLayout(False)
        Panel5.ResumeLayout(False)
        Panel6.ResumeLayout(False)
        Panel7.ResumeLayout(False)
        Panel8.ResumeLayout(False)
        Panel10.ResumeLayout(False)
        Panel9.ResumeLayout(False)
        Panel11.ResumeLayout(False)
        Panel12.ResumeLayout(False)
        Panel13.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents ModernPanel1 As LakeUI.ModernPanel
    Friend WithEvents HtmlColorLabel1 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents HtmlColorLabel5 As LakeUI.HtmlColorLabel
    Friend WithEvents HtmlColorLabel6 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents MCB_输入文件 As LakeUI.ModernComboBox
    Friend WithEvents JustEmptyControl2 As LakeUI.JustEmptyControl
    Friend WithEvents MTB_音频流索引 As LakeUI.ModernTextBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents MCB_选择模型文件 As LakeUI.ModernComboBox
    Friend WithEvents MTB_GPU设备索引 As LakeUI.ModernTextBox
    Friend WithEvents Panel3 As Panel
    Friend WithEvents HtmlColorLabel2 As LakeUI.HtmlColorLabel
    Friend WithEvents HtmlColorLabel3 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel6 As Panel
    Friend WithEvents JustEmptyControl4 As LakeUI.JustEmptyControl
    Friend WithEvents MCB_要识别的语言 As LakeUI.ModernComboBox
    Friend WithEvents Panel5 As Panel
    Friend WithEvents HtmlColorLabel4 As LakeUI.HtmlColorLabel
    Friend WithEvents HtmlColorLabel7 As LakeUI.HtmlColorLabel
    Friend WithEvents MCB_字幕片段最大字符数 As LakeUI.HtmlColorLabel
    Friend WithEvents MCB_翻译成英语 As LakeUI.ModernCheckBox
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Panel8 As Panel
    Friend WithEvents HtmlColorLabel9 As LakeUI.HtmlColorLabel
    Friend WithEvents HtmlColorLabel10 As LakeUI.HtmlColorLabel
    Friend WithEvents HtmlColorLabel11 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel10 As Panel
    Friend WithEvents HtmlColorLabel12 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel9 As Panel
    Friend WithEvents MCB_VAD模型文件 As LakeUI.ModernComboBox
    Friend WithEvents Panel11 As Panel
    Friend WithEvents JustEmptyControl7 As LakeUI.JustEmptyControl
    Friend WithEvents MCB_输出类型 As LakeUI.ModernComboBox
    Friend WithEvents Panel12 As Panel
    Friend WithEvents HtmlColorLabel14 As LakeUI.HtmlColorLabel
    Friend WithEvents HtmlColorLabel15 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel13 As Panel
    Friend WithEvents MB_添加到编码队列 As LakeUI.ModernButton
    Friend WithEvents MB_重置页面 As LakeUI.ModernButton
    Friend WithEvents JustEmptyControl6 As LakeUI.JustEmptyControl
    Friend WithEvents JustEmptyControl8 As LakeUI.JustEmptyControl
    Friend WithEvents MCB_GPU加速选项 As LakeUI.ModernComboBox
    Friend WithEvents JustEmptyControl9 As LakeUI.JustEmptyControl
    Friend WithEvents HtmlColorLabel13 As LakeUI.HtmlColorLabel
    Friend WithEvents ETB_VAD最短静音持续时间 As LakeUI.ExcellentTrackBar
    Friend WithEvents ETB_VAD最短语音持续时间 As LakeUI.ExcellentTrackBar
    Friend WithEvents ETB_VAD阈值 As LakeUI.ExcellentTrackBar
    Friend WithEvents ETB_字幕片段最大字符数 As LakeUI.ExcellentTrackBar
    Friend WithEvents ETB_音频缓存长度 As LakeUI.ExcellentTrackBar
    Friend WithEvents MTB_输出文件 As LakeUI.ModernTextBox
    Friend WithEvents MB_输出文件另存为 As LakeUI.ModernButton
    Friend WithEvents JustEmptyControl1 As LakeUI.JustEmptyControl
End Class
