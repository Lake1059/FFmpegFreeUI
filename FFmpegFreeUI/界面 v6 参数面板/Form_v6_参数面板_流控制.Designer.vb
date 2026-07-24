<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_v6_参数面板_流控制
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_v6_参数面板_流控制))
        ModernPanel1 = New LakeUI.ModernPanel()
        HCL_流控制底部说明 = New LakeUI.HtmlColorLabel()
        Panel7 = New Panel()
        HCL_首个输入文件提示 = New LakeUI.HtmlColorLabel()
        MCB_附件选项 = New LakeUI.ModernComboBox()
        JustEmptyControl6 = New LakeUI.JustEmptyControl()
        MCB_章节选项 = New LakeUI.ModernComboBox()
        JustEmptyControl5 = New LakeUI.JustEmptyControl()
        MCB_元数据选项 = New LakeUI.ModernComboBox()
        HCL_元数据章节附件标题 = New LakeUI.HtmlColorLabel()
        HCL_保留附加内容提示 = New LakeUI.HtmlColorLabel()
        Panel6 = New Panel()
        HCL_mp4字幕格式提示 = New LakeUI.HtmlColorLabel()
        MCK_字幕转为mov_text = New LakeUI.ModernCheckBox()
        MCK_混流同名SSA字幕 = New LakeUI.ModernCheckBox()
        MCK_混流同名ASS字幕 = New LakeUI.ModernCheckBox()
        MCK_混流同名SRT字幕 = New LakeUI.ModernCheckBox()
        HCL_混流同名字幕标题 = New LakeUI.HtmlColorLabel()
        Panel4 = New Panel()
        HCL_可视化流选择器说明 = New LakeUI.HtmlColorLabel()
        MB_打开可视化流选择器 = New LakeUI.ModernButton()
        Panel3 = New Panel()
        HCL_字幕流选择格式 = New LakeUI.HtmlColorLabel()
        JustEmptyControl4 = New LakeUI.JustEmptyControl()
        Panel2 = New Panel()
        MCK_保留其他字幕流 = New LakeUI.ModernCheckBox()
        MCB_字幕流操作 = New LakeUI.ModernComboBox()
        JustEmptyControl3 = New LakeUI.JustEmptyControl()
        MTB_字幕流选择 = New LakeUI.ModernTextBox()
        HCL_字幕流选择说明 = New LakeUI.HtmlColorLabel()
        Panel1 = New Panel()
        HCL_音频流选择格式 = New LakeUI.HtmlColorLabel()
        MCK_保留其他音频流 = New LakeUI.ModernCheckBox()
        JustEmptyControl2 = New LakeUI.JustEmptyControl()
        MTB_音频流选择 = New LakeUI.ModernTextBox()
        HCL_音频流选择说明 = New LakeUI.HtmlColorLabel()
        Panel5 = New Panel()
        HCL_视频流选择格式 = New LakeUI.HtmlColorLabel()
        MCK_保留其他视频流 = New LakeUI.ModernCheckBox()
        JustEmptyControl1 = New LakeUI.JustEmptyControl()
        MTB_视频流选择 = New LakeUI.ModernTextBox()
        HCL_视频流选择说明 = New LakeUI.HtmlColorLabel()
        ModernPanel1.SuspendLayout()
        Panel7.SuspendLayout()
        Panel6.SuspendLayout()
        Panel4.SuspendLayout()
        Panel3.SuspendLayout()
        Panel2.SuspendLayout()
        Panel1.SuspendLayout()
        Panel5.SuspendLayout()
        SuspendLayout()
        '
        ' ModernPanel1
        '
        ModernPanel1.BackColor1 = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ModernPanel1.BorderSize = 0
        ModernPanel1.Controls.Add(HCL_流控制底部说明)
        ModernPanel1.Controls.Add(Panel7)
        ModernPanel1.Controls.Add(HCL_元数据章节附件标题)
        ModernPanel1.Controls.Add(HCL_保留附加内容提示)
        ModernPanel1.Controls.Add(Panel6)
        ModernPanel1.Controls.Add(HCL_混流同名字幕标题)
        ModernPanel1.Controls.Add(Panel4)
        ModernPanel1.Controls.Add(Panel3)
        ModernPanel1.Controls.Add(Panel2)
        ModernPanel1.Controls.Add(HCL_字幕流选择说明)
        ModernPanel1.Controls.Add(Panel1)
        ModernPanel1.Controls.Add(HCL_音频流选择说明)
        ModernPanel1.Controls.Add(Panel5)
        ModernPanel1.Controls.Add(HCL_视频流选择说明)
        ModernPanel1.Dock = DockStyle.Fill
        ModernPanel1.Location = New Point(0, 0)
        ModernPanel1.Name = "ModernPanel1"
        ModernPanel1.Padding = New Padding(20)
        ModernPanel1.ScrollBarMode = LakeUI.ModernPanel.ScrollMode.Vertical
        ModernPanel1.Size = New Size(886, 713)
        ModernPanel1.TabIndex = 1
        '
        ' HCL_流控制底部说明
        '
        HCL_流控制底部说明.AutoSize = True
        HCL_流控制底部说明.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_流控制底部说明.Dock = DockStyle.Bottom
        HCL_流控制底部说明.Font = New Font("Microsoft YaHei UI", 9F)
        HCL_流控制底部说明.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_流控制底部说明.Location = New Point(20, 607)
        HCL_流控制底部说明.Margin = New Padding(2)
        HCL_流控制底部说明.Name = "HCL_流控制底部说明"
        HCL_流控制底部说明.Size = New Size(846, 86)
        HCL_流控制底部说明.TabIndex = 24
        HCL_流控制底部说明.Text = resources.GetString("HCL_流控制底部说明.Text")
        '
        ' Panel7
        '
        Panel7.Controls.Add(HCL_首个输入文件提示)
        Panel7.Controls.Add(MCB_附件选项)
        Panel7.Controls.Add(JustEmptyControl6)
        Panel7.Controls.Add(MCB_章节选项)
        Panel7.Controls.Add(JustEmptyControl5)
        Panel7.Controls.Add(MCB_元数据选项)
        Panel7.Dock = DockStyle.Top
        Panel7.Location = New Point(20, 467)
        Panel7.Name = "Panel7"
        Panel7.Padding = New Padding(0, 10, 0, 0)
        Panel7.Size = New Size(846, 42)
        Panel7.TabIndex = 23
        '
        ' HCL_首个输入文件提示
        '
        HCL_首个输入文件提示.AutoSize = True
        HCL_首个输入文件提示.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_首个输入文件提示.Dock = DockStyle.Fill
        HCL_首个输入文件提示.Font = New Font("Microsoft YaHei UI", 9F)
        HCL_首个输入文件提示.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_首个输入文件提示.Location = New Point(470, 10)
        HCL_首个输入文件提示.Margin = New Padding(2)
        HCL_首个输入文件提示.Name = "HCL_首个输入文件提示"
        HCL_首个输入文件提示.Padding = New Padding(10, 0, 0, 0)
        HCL_首个输入文件提示.Size = New Size(376, 32)
        HCL_首个输入文件提示.TabIndex = 17
        HCL_首个输入文件提示.Text = "这些功能仅应用于首个 -i 的文件"
        HCL_首个输入文件提示.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.MiddleLeft
        '
        ' MCB_附件选项
        '
        MCB_附件选项.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_附件选项.BorderRadius = 10
        MCB_附件选项.BorderSize = 0
        MCB_附件选项.Dock = DockStyle.Left
        MCB_附件选项.DropDownBackdropBlurPasses = 2
        MCB_附件选项.DropDownBackdropBlurRadius = 30
        MCB_附件选项.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_附件选项.DropDownHoverColor = Color.FromArgb(CByte(20), CByte(220), CByte(220), CByte(220))
        MCB_附件选项.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_附件选项.DropDownPadding = New Padding(10)
        MCB_附件选项.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_附件选项.DropDownSelectedForeColor = Color.White
        MCB_附件选项.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_附件选项.Items.Add("")
        MCB_附件选项.Items.Add("保留附件")
        MCB_附件选项.Location = New Point(320, 10)
        MCB_附件选项.Margin = New Padding(2, 2, 2, 2)
        MCB_附件选项.Name = "MCB_附件选项"
        MCB_附件选项.Padding = New Padding(10, 0, 10, 0)
        MCB_附件选项.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_附件选项.Size = New Size(150, 32)
        MCB_附件选项.TabIndex = 9
        MCB_附件选项.ToolTipGap = -1
        MCB_附件选项.ToolTipMaxWidth = 350
        MCB_附件选项.ToolTipPadding = New Padding(15)
        MCB_附件选项.WaterText = "附件选项"
        MCB_附件选项.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        '
        ' JustEmptyControl6
        '
        JustEmptyControl6.Dock = DockStyle.Left
        JustEmptyControl6.Location = New Point(310, 10)
        JustEmptyControl6.Name = "JustEmptyControl6"
        JustEmptyControl6.Size = New Size(10, 32)
        JustEmptyControl6.TabIndex = 8
        '
        ' MCB_章节选项
        '
        MCB_章节选项.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_章节选项.BorderRadius = 10
        MCB_章节选项.BorderSize = 0
        MCB_章节选项.Dock = DockStyle.Left
        MCB_章节选项.DropDownBackdropBlurPasses = 2
        MCB_章节选项.DropDownBackdropBlurRadius = 30
        MCB_章节选项.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_章节选项.DropDownHoverColor = Color.FromArgb(CByte(20), CByte(220), CByte(220), CByte(220))
        MCB_章节选项.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_章节选项.DropDownPadding = New Padding(10)
        MCB_章节选项.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_章节选项.DropDownSelectedForeColor = Color.White
        MCB_章节选项.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_章节选项.Items.Add("")
        MCB_章节选项.Items.Add("保留章节")
        MCB_章节选项.Items.Add("清除章节")
        MCB_章节选项.Location = New Point(160, 10)
        MCB_章节选项.Margin = New Padding(2, 2, 2, 2)
        MCB_章节选项.Name = "MCB_章节选项"
        MCB_章节选项.Padding = New Padding(10, 0, 10, 0)
        MCB_章节选项.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_章节选项.Size = New Size(150, 32)
        MCB_章节选项.TabIndex = 7
        MCB_章节选项.ToolTipGap = -1
        MCB_章节选项.ToolTipMaxWidth = 350
        MCB_章节选项.ToolTipPadding = New Padding(15)
        MCB_章节选项.WaterText = "章节选项"
        MCB_章节选项.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        '
        ' JustEmptyControl5
        '
        JustEmptyControl5.Dock = DockStyle.Left
        JustEmptyControl5.Location = New Point(150, 10)
        JustEmptyControl5.Name = "JustEmptyControl5"
        JustEmptyControl5.Size = New Size(10, 32)
        JustEmptyControl5.TabIndex = 6
        '
        ' MCB_元数据选项
        '
        MCB_元数据选项.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_元数据选项.BorderRadius = 10
        MCB_元数据选项.BorderSize = 0
        MCB_元数据选项.Dock = DockStyle.Left
        MCB_元数据选项.DropDownBackdropBlurPasses = 2
        MCB_元数据选项.DropDownBackdropBlurRadius = 30
        MCB_元数据选项.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_元数据选项.DropDownHoverColor = Color.FromArgb(CByte(20), CByte(220), CByte(220), CByte(220))
        MCB_元数据选项.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_元数据选项.DropDownPadding = New Padding(10)
        MCB_元数据选项.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_元数据选项.DropDownSelectedForeColor = Color.White
        MCB_元数据选项.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_元数据选项.Items.Add("")
        MCB_元数据选项.Items.Add("保留元数据")
        MCB_元数据选项.Items.Add("清除元数据")
        MCB_元数据选项.Items.Add("保留更多元数据")
        MCB_元数据选项.Location = New Point(0, 10)
        MCB_元数据选项.Margin = New Padding(2, 2, 2, 2)
        MCB_元数据选项.Name = "MCB_元数据选项"
        MCB_元数据选项.Padding = New Padding(10, 0, 10, 0)
        MCB_元数据选项.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_元数据选项.Size = New Size(150, 32)
        MCB_元数据选项.TabIndex = 1
        MCB_元数据选项.ToolTipGap = -1
        MCB_元数据选项.ToolTipMaxWidth = 350
        MCB_元数据选项.ToolTipPadding = New Padding(15)
        MCB_元数据选项.WaterText = "元数据选项"
        MCB_元数据选项.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        '
        ' HCL_元数据章节附件标题
        '
        HCL_元数据章节附件标题.AutoSize = True
        HCL_元数据章节附件标题.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_元数据章节附件标题.Dock = DockStyle.Top
        HCL_元数据章节附件标题.Font = New Font("Microsoft YaHei UI", 11F)
        HCL_元数据章节附件标题.Location = New Point(20, 431)
        HCL_元数据章节附件标题.Margin = New Padding(2)
        HCL_元数据章节附件标题.Name = "HCL_元数据章节附件标题"
        HCL_元数据章节附件标题.Padding = New Padding(0, 15, 0, 0)
        HCL_元数据章节附件标题.Size = New Size(846, 36)
        HCL_元数据章节附件标题.TabIndex = 22
        HCL_元数据章节附件标题.Text = "元数据 # 章节 # 附件"
        '
        ' HCL_保留附加内容提示
        '
        HCL_保留附加内容提示.AutoSize = True
        HCL_保留附加内容提示.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_保留附加内容提示.Dock = DockStyle.Top
        HCL_保留附加内容提示.Font = New Font("Microsoft YaHei UI", 9F)
        HCL_保留附加内容提示.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_保留附加内容提示.Location = New Point(20, 391)
        HCL_保留附加内容提示.Margin = New Padding(2)
        HCL_保留附加内容提示.Name = "HCL_保留附加内容提示"
        HCL_保留附加内容提示.Padding = New Padding(0, 5, 0, 0)
        HCL_保留附加内容提示.Size = New Size(846, 40)
        HCL_保留附加内容提示.TabIndex = 21
        HCL_保留附加内容提示.Text = "这些功能强制使用 <span style=""color:YellowGreen"">-map</span>，因为无法在一般情况下处理这些需求<br>就像 <span style=""color:YellowGreen"">最下方所说的</span> 一样，注意对其他类的流使用 -map，也就是 <span style=""color:IndianRed"">填写上面的文本框</span>"
        '
        ' Panel6
        '
        Panel6.Controls.Add(HCL_mp4字幕格式提示)
        Panel6.Controls.Add(MCK_字幕转为mov_text)
        Panel6.Controls.Add(MCK_混流同名SSA字幕)
        Panel6.Controls.Add(MCK_混流同名ASS字幕)
        Panel6.Controls.Add(MCK_混流同名SRT字幕)
        Panel6.Dock = DockStyle.Top
        Panel6.Location = New Point(20, 349)
        Panel6.Name = "Panel6"
        Panel6.Padding = New Padding(0, 10, 0, 0)
        Panel6.Size = New Size(846, 42)
        Panel6.TabIndex = 20
        '
        ' HCL_mp4字幕格式提示
        '
        HCL_mp4字幕格式提示.AutoSize = True
        HCL_mp4字幕格式提示.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_mp4字幕格式提示.Dock = DockStyle.Fill
        HCL_mp4字幕格式提示.Font = New Font("Microsoft YaHei UI", 9F)
        HCL_mp4字幕格式提示.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_mp4字幕格式提示.Location = New Point(417, 10)
        HCL_mp4字幕格式提示.Margin = New Padding(2)
        HCL_mp4字幕格式提示.Name = "HCL_mp4字幕格式提示"
        HCL_mp4字幕格式提示.Size = New Size(429, 32)
        HCL_mp4字幕格式提示.TabIndex = 18
        HCL_mp4字幕格式提示.Text = "<span style=""color:YellowGreen"">mp4</span> 仅支持 <span style=""color:IndianRed"">mov_text</span> 字幕"
        HCL_mp4字幕格式提示.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.MiddleLeft
        '
        ' MCK_字幕转为mov_text
        '
        MCK_字幕转为mov_text.AutoSize = True
        MCK_字幕转为mov_text.BoxBorderRadius = 6
        MCK_字幕转为mov_text.BoxBorderSize = 0
        MCK_字幕转为mov_text.BoxCheckedBackColor = Color.OliveDrab
        MCK_字幕转为mov_text.BoxInnerPadding = 6
        MCK_字幕转为mov_text.BoxSize = 22
        MCK_字幕转为mov_text.BoxTextSpacing = 10
        MCK_字幕转为mov_text.BoxUncheckedBackColor = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCK_字幕转为mov_text.ClickAnywhere = True
        MCK_字幕转为mov_text.Dock = DockStyle.Left
        MCK_字幕转为mov_text.Location = New Point(263, 10)
        MCK_字幕转为mov_text.Name = "MCK_字幕转为mov_text"
        MCK_字幕转为mov_text.Padding = New Padding(0, 0, 30, 0)
        MCK_字幕转为mov_text.Size = New Size(154, 32)
        MCK_字幕转为mov_text.TabIndex = 17
        MCK_字幕转为mov_text.Text = "转为 mov_text"
        '
        ' MCK_混流同名SSA字幕
        '
        MCK_混流同名SSA字幕.AutoSize = True
        MCK_混流同名SSA字幕.BoxBorderRadius = 6
        MCK_混流同名SSA字幕.BoxBorderSize = 0
        MCK_混流同名SSA字幕.BoxCheckedBackColor = Color.OliveDrab
        MCK_混流同名SSA字幕.BoxInnerPadding = 6
        MCK_混流同名SSA字幕.BoxSize = 22
        MCK_混流同名SSA字幕.BoxTextSpacing = 10
        MCK_混流同名SSA字幕.BoxUncheckedBackColor = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCK_混流同名SSA字幕.ClickAnywhere = True
        MCK_混流同名SSA字幕.Dock = DockStyle.Left
        MCK_混流同名SSA字幕.Location = New Point(175, 10)
        MCK_混流同名SSA字幕.Name = "MCK_混流同名SSA字幕"
        MCK_混流同名SSA字幕.Padding = New Padding(0, 0, 30, 0)
        MCK_混流同名SSA字幕.Size = New Size(88, 32)
        MCK_混流同名SSA字幕.TabIndex = 16
        MCK_混流同名SSA字幕.Text = "SSA"
        '
        ' MCK_混流同名ASS字幕
        '
        MCK_混流同名ASS字幕.AutoSize = True
        MCK_混流同名ASS字幕.BoxBorderRadius = 6
        MCK_混流同名ASS字幕.BoxBorderSize = 0
        MCK_混流同名ASS字幕.BoxCheckedBackColor = Color.OliveDrab
        MCK_混流同名ASS字幕.BoxInnerPadding = 6
        MCK_混流同名ASS字幕.BoxSize = 22
        MCK_混流同名ASS字幕.BoxTextSpacing = 10
        MCK_混流同名ASS字幕.BoxUncheckedBackColor = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCK_混流同名ASS字幕.ClickAnywhere = True
        MCK_混流同名ASS字幕.Dock = DockStyle.Left
        MCK_混流同名ASS字幕.Location = New Point(87, 10)
        MCK_混流同名ASS字幕.Name = "MCK_混流同名ASS字幕"
        MCK_混流同名ASS字幕.Padding = New Padding(0, 0, 30, 0)
        MCK_混流同名ASS字幕.Size = New Size(88, 32)
        MCK_混流同名ASS字幕.TabIndex = 15
        MCK_混流同名ASS字幕.Text = "ASS"
        '
        ' MCK_混流同名SRT字幕
        '
        MCK_混流同名SRT字幕.AutoSize = True
        MCK_混流同名SRT字幕.BoxBorderRadius = 6
        MCK_混流同名SRT字幕.BoxBorderSize = 0
        MCK_混流同名SRT字幕.BoxCheckedBackColor = Color.OliveDrab
        MCK_混流同名SRT字幕.BoxInnerPadding = 6
        MCK_混流同名SRT字幕.BoxSize = 22
        MCK_混流同名SRT字幕.BoxTextSpacing = 10
        MCK_混流同名SRT字幕.BoxUncheckedBackColor = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCK_混流同名SRT字幕.ClickAnywhere = True
        MCK_混流同名SRT字幕.Dock = DockStyle.Left
        MCK_混流同名SRT字幕.Location = New Point(0, 10)
        MCK_混流同名SRT字幕.Name = "MCK_混流同名SRT字幕"
        MCK_混流同名SRT字幕.Padding = New Padding(0, 0, 30, 0)
        MCK_混流同名SRT字幕.Size = New Size(87, 32)
        MCK_混流同名SRT字幕.TabIndex = 14
        MCK_混流同名SRT字幕.Text = "SRT"
        '
        ' HCL_混流同名字幕标题
        '
        HCL_混流同名字幕标题.AutoSize = True
        HCL_混流同名字幕标题.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_混流同名字幕标题.Dock = DockStyle.Top
        HCL_混流同名字幕标题.Font = New Font("Microsoft YaHei UI", 11F)
        HCL_混流同名字幕标题.Location = New Point(20, 313)
        HCL_混流同名字幕标题.Margin = New Padding(2)
        HCL_混流同名字幕标题.Name = "HCL_混流同名字幕标题"
        HCL_混流同名字幕标题.Padding = New Padding(0, 15, 0, 0)
        HCL_混流同名字幕标题.Size = New Size(846, 36)
        HCL_混流同名字幕标题.TabIndex = 19
        HCL_混流同名字幕标题.Text = "混流同名字幕"
        '
        ' Panel4
        '
        Panel4.Controls.Add(HCL_可视化流选择器说明)
        Panel4.Controls.Add(MB_打开可视化流选择器)
        Panel4.Dock = DockStyle.Top
        Panel4.Location = New Point(20, 271)
        Panel4.Name = "Panel4"
        Panel4.Padding = New Padding(0, 10, 0, 0)
        Panel4.Size = New Size(846, 42)
        Panel4.TabIndex = 18
        '
        ' HCL_可视化流选择器说明
        '
        HCL_可视化流选择器说明.AutoSize = True
        HCL_可视化流选择器说明.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_可视化流选择器说明.Dock = DockStyle.Fill
        HCL_可视化流选择器说明.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_可视化流选择器说明.Location = New Point(200, 10)
        HCL_可视化流选择器说明.Margin = New Padding(2)
        HCL_可视化流选择器说明.Name = "HCL_可视化流选择器说明"
        HCL_可视化流选择器说明.Padding = New Padding(10, 0, 0, 0)
        HCL_可视化流选择器说明.Size = New Size(646, 32)
        HCL_可视化流选择器说明.TabIndex = 16
        HCL_可视化流选择器说明.Text = "推荐使用 <span style=""color:Gainsboro"">可视化选择器</span> 来快速填写上面三个文本框"
        HCL_可视化流选择器说明.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.MiddleLeft
        '
        ' MB_打开可视化流选择器
        '
        MB_打开可视化流选择器.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_打开可视化流选择器.BorderRadius = 10
        MB_打开可视化流选择器.BorderSize = 0
        MB_打开可视化流选择器.Dock = DockStyle.Left
        MB_打开可视化流选择器.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_打开可视化流选择器.Location = New Point(0, 10)
        MB_打开可视化流选择器.Margin = New Padding(2)
        MB_打开可视化流选择器.Name = "MB_打开可视化流选择器"
        MB_打开可视化流选择器.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_打开可视化流选择器.Size = New Size(200, 32)
        MB_打开可视化流选择器.TabIndex = 0
        MB_打开可视化流选择器.Text = "可视化流选择器"
        '
        ' Panel3
        '
        Panel3.Controls.Add(HCL_字幕流选择格式)
        Panel3.Controls.Add(JustEmptyControl4)
        Panel3.Dock = DockStyle.Top
        Panel3.Location = New Point(20, 239)
        Panel3.Name = "Panel3"
        Panel3.Size = New Size(846, 32)
        Panel3.TabIndex = 17
        '
        ' HCL_字幕流选择格式
        '
        HCL_字幕流选择格式.AutoSize = True
        HCL_字幕流选择格式.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_字幕流选择格式.Dock = DockStyle.Fill
        HCL_字幕流选择格式.Font = New Font("Microsoft YaHei UI", 9F)
        HCL_字幕流选择格式.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_字幕流选择格式.Location = New Point(211, 0)
        HCL_字幕流选择格式.Margin = New Padding(2)
        HCL_字幕流选择格式.Name = "HCL_字幕流选择格式"
        HCL_字幕流选择格式.Size = New Size(635, 32)
        HCL_字幕流选择格式.TabIndex = 15
        HCL_字幕流选择格式.Text = "格式：<span style=""color:Gainsboro"">文件索引</span>:<span style=""color:MediumPurple"">s</span>:<span style=""color:Gainsboro"">流索引</span>，例如：0:s 表示第一个文件的全部字幕流"
        HCL_字幕流选择格式.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.MiddleLeft
        '
        ' JustEmptyControl4
        '
        JustEmptyControl4.Dock = DockStyle.Left
        JustEmptyControl4.Location = New Point(0, 0)
        JustEmptyControl4.Name = "JustEmptyControl4"
        JustEmptyControl4.Size = New Size(211, 32)
        JustEmptyControl4.TabIndex = 5
        '
        ' Panel2
        '
        Panel2.Controls.Add(MCK_保留其他字幕流)
        Panel2.Controls.Add(MCB_字幕流操作)
        Panel2.Controls.Add(JustEmptyControl3)
        Panel2.Controls.Add(MTB_字幕流选择)
        Panel2.Dock = DockStyle.Top
        Panel2.Location = New Point(20, 197)
        Panel2.Name = "Panel2"
        Panel2.Padding = New Padding(0, 10, 0, 0)
        Panel2.Size = New Size(846, 42)
        Panel2.TabIndex = 16
        '
        ' MCK_保留其他字幕流
        '
        MCK_保留其他字幕流.AutoSize = True
        MCK_保留其他字幕流.BoxBorderRadius = 6
        MCK_保留其他字幕流.BoxBorderSize = 0
        MCK_保留其他字幕流.BoxCheckedBackColor = Color.OliveDrab
        MCK_保留其他字幕流.BoxInnerPadding = 6
        MCK_保留其他字幕流.BoxSize = 22
        MCK_保留其他字幕流.BoxTextSpacing = 10
        MCK_保留其他字幕流.BoxUncheckedBackColor = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCK_保留其他字幕流.ClickAnywhere = True
        MCK_保留其他字幕流.Dock = DockStyle.Left
        MCK_保留其他字幕流.Location = New Point(388, 10)
        MCK_保留其他字幕流.Name = "MCK_保留其他字幕流"
        MCK_保留其他字幕流.Padding = New Padding(10, 0, 0, 0)
        MCK_保留其他字幕流.Size = New Size(168, 32)
        MCK_保留其他字幕流.TabIndex = 17
        MCK_保留其他字幕流.Text = "然后保留其他字幕流"
        '
        ' MCB_字幕流操作
        '
        MCB_字幕流操作.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_字幕流操作.BorderRadius = 10
        MCB_字幕流操作.BorderSize = 0
        MCB_字幕流操作.Dock = DockStyle.Left
        MCB_字幕流操作.DropDownBackdropBlurPasses = 2
        MCB_字幕流操作.DropDownBackdropBlurRadius = 30
        MCB_字幕流操作.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_字幕流操作.DropDownHoverColor = Color.FromArgb(CByte(20), CByte(220), CByte(220), CByte(220))
        MCB_字幕流操作.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_字幕流操作.DropDownPadding = New Padding(10)
        MCB_字幕流操作.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_字幕流操作.DropDownSelectedForeColor = Color.White
        MCB_字幕流操作.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_字幕流操作.Items.Add("")
        MCB_字幕流操作.Items.Add("复制流")
        MCB_字幕流操作.Items.Add("转为 mov_text")
        MCB_字幕流操作.Items.Add("转为 srt")
        MCB_字幕流操作.Items.Add("转为 ass")
        MCB_字幕流操作.Items.Add("转为 ssa")
        MCB_字幕流操作.Location = New Point(210, 10)
        MCB_字幕流操作.Margin = New Padding(2, 2, 2, 2)
        MCB_字幕流操作.Name = "MCB_字幕流操作"
        MCB_字幕流操作.Padding = New Padding(10, 0, 10, 0)
        MCB_字幕流操作.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_字幕流操作.Size = New Size(178, 32)
        MCB_字幕流操作.TabIndex = 15
        MCB_字幕流操作.ToolTipGap = -1
        MCB_字幕流操作.ToolTipMaxWidth = 350
        MCB_字幕流操作.ToolTipPadding = New Padding(15)
        MCB_字幕流操作.WaterText = "如何操作"
        MCB_字幕流操作.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        '
        ' JustEmptyControl3
        '
        JustEmptyControl3.Dock = DockStyle.Left
        JustEmptyControl3.Location = New Point(200, 10)
        JustEmptyControl3.Name = "JustEmptyControl3"
        JustEmptyControl3.Size = New Size(10, 32)
        JustEmptyControl3.TabIndex = 5
        '
        ' MTB_字幕流选择
        '
        MTB_字幕流选择.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_字幕流选择.BorderColor = Color.Transparent
        MTB_字幕流选择.BorderColorFocus = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MTB_字幕流选择.BorderRadius = 10
        MTB_字幕流选择.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        MTB_字幕流选择.Dock = DockStyle.Left
        MTB_字幕流选择.Location = New Point(0, 10)
        MTB_字幕流选择.Margin = New Padding(2)
        MTB_字幕流选择.Name = "MTB_字幕流选择"
        MTB_字幕流选择.Padding = New Padding(10, 0, 10, 0)
        MTB_字幕流选择.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_字幕流选择.Size = New Size(200, 32)
        MTB_字幕流选择.TabIndex = 4
        MTB_字幕流选择.WaterText = "多个用英文逗号隔开"
        MTB_字幕流选择.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        '
        ' HCL_字幕流选择说明
        '
        HCL_字幕流选择说明.AutoSize = True
        HCL_字幕流选择说明.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_字幕流选择说明.Dock = DockStyle.Top
        HCL_字幕流选择说明.Font = New Font("Microsoft YaHei UI", 11F)
        HCL_字幕流选择说明.Location = New Point(20, 161)
        HCL_字幕流选择说明.Margin = New Padding(2)
        HCL_字幕流选择说明.Name = "HCL_字幕流选择说明"
        HCL_字幕流选择说明.Padding = New Padding(0, 15, 0, 0)
        HCL_字幕流选择说明.Size = New Size(846, 36)
        HCL_字幕流选择说明.TabIndex = 15
        HCL_字幕流选择说明.Text = "使用哪些文件的哪些 <span style=""color:MediumPurple"">字幕</span>（s）"
        '
        ' Panel1
        '
        Panel1.Controls.Add(HCL_音频流选择格式)
        Panel1.Controls.Add(MCK_保留其他音频流)
        Panel1.Controls.Add(JustEmptyControl2)
        Panel1.Controls.Add(MTB_音频流选择)
        Panel1.Dock = DockStyle.Top
        Panel1.Location = New Point(20, 119)
        Panel1.Name = "Panel1"
        Panel1.Padding = New Padding(0, 10, 0, 0)
        Panel1.Size = New Size(846, 42)
        Panel1.TabIndex = 14
        '
        ' HCL_音频流选择格式
        '
        HCL_音频流选择格式.AutoSize = True
        HCL_音频流选择格式.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_音频流选择格式.Dock = DockStyle.Fill
        HCL_音频流选择格式.Font = New Font("Microsoft YaHei UI", 9F)
        HCL_音频流选择格式.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_音频流选择格式.Location = New Point(388, 10)
        HCL_音频流选择格式.Margin = New Padding(2)
        HCL_音频流选择格式.Name = "HCL_音频流选择格式"
        HCL_音频流选择格式.Size = New Size(458, 32)
        HCL_音频流选择格式.TabIndex = 14
        HCL_音频流选择格式.Text = "格式：<span style=""color:Gainsboro"">文件索引</span>:<span style=""color:MediumSeaGreen"">a</span>:<span style=""color:Gainsboro"">流索引</span><br>例如：0:a 表示第一个文件的全部音频流"
        HCL_音频流选择格式.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.MiddleLeft
        '
        ' MCK_保留其他音频流
        '
        MCK_保留其他音频流.AutoSize = True
        MCK_保留其他音频流.BoxBorderRadius = 6
        MCK_保留其他音频流.BoxBorderSize = 0
        MCK_保留其他音频流.BoxCheckedBackColor = Color.OliveDrab
        MCK_保留其他音频流.BoxInnerPadding = 6
        MCK_保留其他音频流.BoxSize = 22
        MCK_保留其他音频流.BoxTextSpacing = 10
        MCK_保留其他音频流.BoxUncheckedBackColor = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCK_保留其他音频流.ClickAnywhere = True
        MCK_保留其他音频流.Dock = DockStyle.Left
        MCK_保留其他音频流.Location = New Point(210, 10)
        MCK_保留其他音频流.Name = "MCK_保留其他音频流"
        MCK_保留其他音频流.Padding = New Padding(0, 0, 20, 0)
        MCK_保留其他音频流.Size = New Size(178, 32)
        MCK_保留其他音频流.TabIndex = 13
        MCK_保留其他音频流.Text = "然后保留其他音频流"
        '
        ' JustEmptyControl2
        '
        JustEmptyControl2.Dock = DockStyle.Left
        JustEmptyControl2.Location = New Point(200, 10)
        JustEmptyControl2.Name = "JustEmptyControl2"
        JustEmptyControl2.Size = New Size(10, 32)
        JustEmptyControl2.TabIndex = 5
        '
        ' MTB_音频流选择
        '
        MTB_音频流选择.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_音频流选择.BorderColor = Color.Transparent
        MTB_音频流选择.BorderColorFocus = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MTB_音频流选择.BorderRadius = 10
        MTB_音频流选择.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        MTB_音频流选择.Dock = DockStyle.Left
        MTB_音频流选择.Location = New Point(0, 10)
        MTB_音频流选择.Margin = New Padding(2)
        MTB_音频流选择.Name = "MTB_音频流选择"
        MTB_音频流选择.Padding = New Padding(10, 0, 10, 0)
        MTB_音频流选择.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_音频流选择.Size = New Size(200, 32)
        MTB_音频流选择.TabIndex = 4
        MTB_音频流选择.WaterText = "多个用英文逗号隔开"
        MTB_音频流选择.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        '
        ' HCL_音频流选择说明
        '
        HCL_音频流选择说明.AutoSize = True
        HCL_音频流选择说明.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_音频流选择说明.Dock = DockStyle.Top
        HCL_音频流选择说明.Font = New Font("Microsoft YaHei UI", 11F)
        HCL_音频流选择说明.Location = New Point(20, 83)
        HCL_音频流选择说明.Margin = New Padding(2)
        HCL_音频流选择说明.Name = "HCL_音频流选择说明"
        HCL_音频流选择说明.Padding = New Padding(0, 15, 0, 0)
        HCL_音频流选择说明.Size = New Size(846, 36)
        HCL_音频流选择说明.TabIndex = 13
        HCL_音频流选择说明.Text = "将 <span style=""color:MediumSeaGreen"">音频参数</span> 应用于哪些文件和流（a）"
        '
        ' Panel5
        '
        Panel5.Controls.Add(HCL_视频流选择格式)
        Panel5.Controls.Add(MCK_保留其他视频流)
        Panel5.Controls.Add(JustEmptyControl1)
        Panel5.Controls.Add(MTB_视频流选择)
        Panel5.Dock = DockStyle.Top
        Panel5.Location = New Point(20, 41)
        Panel5.Name = "Panel5"
        Panel5.Padding = New Padding(0, 10, 0, 0)
        Panel5.Size = New Size(846, 42)
        Panel5.TabIndex = 12
        '
        ' HCL_视频流选择格式
        '
        HCL_视频流选择格式.AutoSize = True
        HCL_视频流选择格式.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_视频流选择格式.Dock = DockStyle.Fill
        HCL_视频流选择格式.Font = New Font("Microsoft YaHei UI", 9F)
        HCL_视频流选择格式.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_视频流选择格式.Location = New Point(389, 10)
        HCL_视频流选择格式.Margin = New Padding(2)
        HCL_视频流选择格式.Name = "HCL_视频流选择格式"
        HCL_视频流选择格式.Size = New Size(457, 32)
        HCL_视频流选择格式.TabIndex = 14
        HCL_视频流选择格式.Text = "格式：<span style=""color:Gainsboro"">文件索引</span>:<span style=""color:CornflowerBlue"">v</span>:<span style=""color:Gainsboro"">流索引</span><br>例如：0:v 表示第一个文件的全部视频流"
        HCL_视频流选择格式.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.MiddleLeft
        '
        ' MCK_保留其他视频流
        '
        MCK_保留其他视频流.AutoSize = True
        MCK_保留其他视频流.BoxBorderRadius = 6
        MCK_保留其他视频流.BoxBorderSize = 0
        MCK_保留其他视频流.BoxCheckedBackColor = Color.OliveDrab
        MCK_保留其他视频流.BoxInnerPadding = 6
        MCK_保留其他视频流.BoxSize = 22
        MCK_保留其他视频流.BoxTextSpacing = 10
        MCK_保留其他视频流.BoxUncheckedBackColor = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCK_保留其他视频流.ClickAnywhere = True
        MCK_保留其他视频流.Dock = DockStyle.Left
        MCK_保留其他视频流.Location = New Point(211, 10)
        MCK_保留其他视频流.Name = "MCK_保留其他视频流"
        MCK_保留其他视频流.Padding = New Padding(0, 0, 20, 0)
        MCK_保留其他视频流.Size = New Size(178, 32)
        MCK_保留其他视频流.TabIndex = 13
        MCK_保留其他视频流.Text = "然后保留其他视频流"
        '
        ' JustEmptyControl1
        '
        JustEmptyControl1.Dock = DockStyle.Left
        JustEmptyControl1.Location = New Point(200, 10)
        JustEmptyControl1.Name = "JustEmptyControl1"
        JustEmptyControl1.Size = New Size(11, 32)
        JustEmptyControl1.TabIndex = 5
        '
        ' MTB_视频流选择
        '
        MTB_视频流选择.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_视频流选择.BorderColor = Color.Transparent
        MTB_视频流选择.BorderColorFocus = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MTB_视频流选择.BorderRadius = 10
        MTB_视频流选择.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        MTB_视频流选择.Dock = DockStyle.Left
        MTB_视频流选择.Location = New Point(0, 10)
        MTB_视频流选择.Margin = New Padding(2)
        MTB_视频流选择.Name = "MTB_视频流选择"
        MTB_视频流选择.Padding = New Padding(10, 0, 10, 0)
        MTB_视频流选择.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_视频流选择.Size = New Size(200, 32)
        MTB_视频流选择.TabIndex = 4
        MTB_视频流选择.WaterText = "多个用英文逗号隔开"
        MTB_视频流选择.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        '
        ' HCL_视频流选择说明
        '
        HCL_视频流选择说明.AutoSize = True
        HCL_视频流选择说明.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_视频流选择说明.Dock = DockStyle.Top
        HCL_视频流选择说明.Font = New Font("Microsoft YaHei UI", 11F)
        HCL_视频流选择说明.Location = New Point(20, 20)
        HCL_视频流选择说明.Margin = New Padding(2)
        HCL_视频流选择说明.Name = "HCL_视频流选择说明"
        HCL_视频流选择说明.Size = New Size(846, 21)
        HCL_视频流选择说明.TabIndex = 10
        HCL_视频流选择说明.Text = "将 <span style=""color:CornflowerBlue"">视频参数</span> 应用于哪些文件和流（v）"
        '
        ' Form_v6_参数面板_流控制
        '
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ClientSize = New Size(886, 713)
        Controls.Add(ModernPanel1)
        Font = New Font("Microsoft YaHei UI", 10F)
        ForeColor = Color.Silver
        Name = "Form_v6_参数面板_流控制"
        Text = "Form_v6_参数面板_流控制"
        ModernPanel1.ResumeLayout(False)
        ModernPanel1.PerformLayout()
        Panel7.ResumeLayout(False)
        Panel7.PerformLayout()
        Panel6.ResumeLayout(False)
        Panel6.PerformLayout()
        Panel4.ResumeLayout(False)
        Panel4.PerformLayout()
        Panel3.ResumeLayout(False)
        Panel3.PerformLayout()
        Panel2.ResumeLayout(False)
        Panel2.PerformLayout()
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        Panel5.ResumeLayout(False)
        Panel5.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents ModernPanel1 As LakeUI.ModernPanel
    Friend WithEvents HCL_视频流选择说明 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents MTB_视频流选择 As LakeUI.ModernTextBox
    Friend WithEvents JustEmptyControl1 As LakeUI.JustEmptyControl
    Friend WithEvents MCK_保留其他视频流 As LakeUI.ModernCheckBox
    Friend WithEvents HCL_视频流选择格式 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents HCL_音频流选择格式 As LakeUI.HtmlColorLabel
    Friend WithEvents MCK_保留其他音频流 As LakeUI.ModernCheckBox
    Friend WithEvents JustEmptyControl2 As LakeUI.JustEmptyControl
    Friend WithEvents MTB_音频流选择 As LakeUI.ModernTextBox
    Friend WithEvents HCL_音频流选择说明 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents JustEmptyControl3 As LakeUI.JustEmptyControl
    Friend WithEvents MTB_字幕流选择 As LakeUI.ModernTextBox
    Friend WithEvents HCL_字幕流选择说明 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents JustEmptyControl4 As LakeUI.JustEmptyControl
    Friend WithEvents MCB_字幕流操作 As LakeUI.ModernComboBox
    Friend WithEvents HCL_字幕流选择格式 As LakeUI.HtmlColorLabel
    Friend WithEvents MCK_保留其他字幕流 As LakeUI.ModernCheckBox
    Friend WithEvents Panel4 As Panel
    Friend WithEvents HCL_可视化流选择器说明 As LakeUI.HtmlColorLabel
    Friend WithEvents MB_打开可视化流选择器 As LakeUI.ModernButton
    Friend WithEvents HCL_混流同名字幕标题 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel6 As Panel
    Friend WithEvents HCL_mp4字幕格式提示 As LakeUI.HtmlColorLabel
    Friend WithEvents MCK_字幕转为mov_text As LakeUI.ModernCheckBox
    Friend WithEvents MCK_混流同名SSA字幕 As LakeUI.ModernCheckBox
    Friend WithEvents MCK_混流同名ASS字幕 As LakeUI.ModernCheckBox
    Friend WithEvents MCK_混流同名SRT字幕 As LakeUI.ModernCheckBox
    Friend WithEvents HCL_保留附加内容提示 As LakeUI.HtmlColorLabel
    Friend WithEvents HCL_元数据章节附件标题 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel7 As Panel
    Friend WithEvents MCB_附件选项 As LakeUI.ModernComboBox
    Friend WithEvents JustEmptyControl6 As LakeUI.JustEmptyControl
    Friend WithEvents MCB_章节选项 As LakeUI.ModernComboBox
    Friend WithEvents JustEmptyControl5 As LakeUI.JustEmptyControl
    Friend WithEvents MCB_元数据选项 As LakeUI.ModernComboBox
    Friend WithEvents HCL_首个输入文件提示 As LakeUI.HtmlColorLabel
    Friend WithEvents HCL_流控制底部说明 As LakeUI.HtmlColorLabel
End Class
