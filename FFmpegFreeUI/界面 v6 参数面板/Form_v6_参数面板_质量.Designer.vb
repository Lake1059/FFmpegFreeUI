<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_v6_参数面板_质量
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_v6_参数面板_质量))
        预制条目菜单 = New LakeUI.ModernContextMenu()
        ModernPanel1 = New LakeUI.ModernPanel()
        MTB_进阶质量控制参数 = New LakeUI.ModernTextBox()
        JustEmptyControl8 = New LakeUI.JustEmptyControl()
        Panel3 = New Panel()
        HCL_进阶参数空格提示 = New LakeUI.HtmlColorLabel()
        MB_插入预制条目 = New LakeUI.ModernButton()
        HCL_进阶质量控制 = New LakeUI.HtmlColorLabel()
        Panel1 = New Panel()
        MTB_缓冲区 = New LakeUI.ModernTextBox()
        JustEmptyControl5 = New LakeUI.JustEmptyControl()
        MTB_最高比特率 = New LakeUI.ModernTextBox()
        JustEmptyControl4 = New LakeUI.JustEmptyControl()
        MTB_最低比特率 = New LakeUI.ModernTextBox()
        JustEmptyControl3 = New LakeUI.JustEmptyControl()
        MTB_基础比特率 = New LakeUI.ModernTextBox()
        Panel4 = New Panel()
        HCL_缓冲区 = New LakeUI.HtmlColorLabel()
        HCL_最高比特率 = New LakeUI.HtmlColorLabel()
        HCL_最低比特率 = New LakeUI.HtmlColorLabel()
        HCL_基础比特率 = New LakeUI.HtmlColorLabel()
        HCL_比特率单位提示 = New LakeUI.HtmlColorLabel()
        HCL_比特率 = New LakeUI.HtmlColorLabel()
        Panel2 = New Panel()
        MTB_质量值 = New LakeUI.ModernTextBox()
        JustEmptyControl1 = New LakeUI.JustEmptyControl()
        MCB_质量参数名称 = New LakeUI.ModernComboBox()
        JustEmptyControl2 = New LakeUI.JustEmptyControl()
        MCB_全局质量控制方式 = New LakeUI.ModernComboBox()
        HCL_质量 = New LakeUI.HtmlColorLabel()
        HCL_全局质量控制 = New LakeUI.HtmlColorLabel()
        ModernPanel1.SuspendLayout()
        Panel3.SuspendLayout()
        Panel1.SuspendLayout()
        Panel4.SuspendLayout()
        Panel2.SuspendLayout()
        SuspendLayout()
        ' 
        ' 预制条目菜单
        ' 
        预制条目菜单.AnimationFPS = 0
        预制条目菜单.BackdropBlurPasses = 2
        预制条目菜单.BackdropBlurRadius = 30
        预制条目菜单.BackdropMode = LakeUI.ModernContextMenu.BackdropModeEnum.Auto
        预制条目菜单.BackdropNoiseOpacity = CByte(0)
        预制条目菜单.BackdropTintColor = Color.FromArgb(CByte(40), CByte(0), CByte(0), CByte(0))
        预制条目菜单.BorderColor = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        预制条目菜单.HoverBackColor = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        预制条目菜单.HoverRadius = 5
        预制条目菜单.IconSize = 0
        预制条目菜单.ItemHeight = 26
        预制条目菜单.ItemPadding = New Padding(10, 0, 10, 0)
        预制条目菜单.MenuFont = New Font("Microsoft YaHei UI", 10F)
        预制条目菜单.MenuPadding = New Padding(10)
        预制条目菜单.PressedBackColor = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        预制条目菜单.SeparatorColor = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        预制条目菜单.SeparatorHeight = 20
        ' 
        ' ModernPanel1
        ' 
        ModernPanel1.BackColor1 = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ModernPanel1.BorderSize = 0
        ModernPanel1.Controls.Add(MTB_进阶质量控制参数)
        ModernPanel1.Controls.Add(JustEmptyControl8)
        ModernPanel1.Controls.Add(Panel3)
        ModernPanel1.Controls.Add(HCL_进阶质量控制)
        ModernPanel1.Controls.Add(Panel1)
        ModernPanel1.Controls.Add(Panel4)
        ModernPanel1.Controls.Add(HCL_比特率单位提示)
        ModernPanel1.Controls.Add(HCL_比特率)
        ModernPanel1.Controls.Add(Panel2)
        ModernPanel1.Controls.Add(HCL_质量)
        ModernPanel1.Controls.Add(HCL_全局质量控制)
        ModernPanel1.Dock = DockStyle.Fill
        ModernPanel1.Location = New Point(0, 0)
        ModernPanel1.Name = "ModernPanel1"
        ModernPanel1.Padding = New Padding(20)
        ModernPanel1.ScrollBarMode = LakeUI.ModernPanel.ScrollMode.Vertical
        ModernPanel1.Size = New Size(847, 676)
        ModernPanel1.TabIndex = 0
        ' 
        ' MTB_进阶质量控制参数
        ' 
        MTB_进阶质量控制参数.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_进阶质量控制参数.BorderColor = Color.Transparent
        MTB_进阶质量控制参数.BorderColorFocus = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MTB_进阶质量控制参数.BorderRadius = 10
        MTB_进阶质量控制参数.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        MTB_进阶质量控制参数.Dock = DockStyle.Fill
        MTB_进阶质量控制参数.LineNumberBackColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_进阶质量控制参数.LineNumberForeColor = Color.Silver
        MTB_进阶质量控制参数.Location = New Point(20, 378)
        MTB_进阶质量控制参数.Margin = New Padding(2)
        MTB_进阶质量控制参数.MultiLine = True
        MTB_进阶质量控制参数.Name = "MTB_进阶质量控制参数"
        MTB_进阶质量控制参数.Padding = New Padding(10, 8, 10, 8)
        MTB_进阶质量控制参数.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_进阶质量控制参数.ShowLineNumbers = True
        MTB_进阶质量控制参数.Size = New Size(807, 278)
        MTB_进阶质量控制参数.TabIndex = 17
        MTB_进阶质量控制参数.WaterText = "如果要写滤镜，请用滤镜排序功能，现在强制使用滤镜图，单独写必报错"
        MTB_进阶质量控制参数.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' JustEmptyControl8
        ' 
        JustEmptyControl8.Dock = DockStyle.Top
        JustEmptyControl8.Location = New Point(20, 368)
        JustEmptyControl8.Name = "JustEmptyControl8"
        JustEmptyControl8.Size = New Size(807, 10)
        JustEmptyControl8.TabIndex = 16
        ' 
        ' Panel3
        ' 
        Panel3.Controls.Add(HCL_进阶参数空格提示)
        Panel3.Controls.Add(MB_插入预制条目)
        Panel3.Dock = DockStyle.Top
        Panel3.Location = New Point(20, 326)
        Panel3.Name = "Panel3"
        Panel3.Padding = New Padding(0, 10, 0, 0)
        Panel3.Size = New Size(807, 42)
        Panel3.TabIndex = 13
        ' 
        ' HCL_进阶参数空格提示
        ' 
        HCL_进阶参数空格提示.AutoSize = True
        HCL_进阶参数空格提示.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_进阶参数空格提示.Dock = DockStyle.Fill
        HCL_进阶参数空格提示.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_进阶参数空格提示.Location = New Point(150, 10)
        HCL_进阶参数空格提示.Margin = New Padding(2)
        HCL_进阶参数空格提示.Name = "HCL_进阶参数空格提示"
        HCL_进阶参数空格提示.Padding = New Padding(10, 0, 0, 0)
        HCL_进阶参数空格提示.Size = New Size(657, 32)
        HCL_进阶参数空格提示.TabIndex = 12
        HCL_进阶参数空格提示.Text = "可自由安排换行，注意 <span style=""font-size:10pt; color:MediumPurple"">参数和值之间</span> 以及 <span style=""font-size:10pt; color:MediumPurple"">参数与参数之间</span> 的空格即可"
        HCL_进阶参数空格提示.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.MiddleLeft
        ' 
        ' MB_插入预制条目
        ' 
        MB_插入预制条目.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_插入预制条目.BorderRadius = 10
        MB_插入预制条目.BorderSize = 0
        MB_插入预制条目.Dock = DockStyle.Left
        MB_插入预制条目.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_插入预制条目.Location = New Point(0, 10)
        MB_插入预制条目.Margin = New Padding(2)
        MB_插入预制条目.Name = "MB_插入预制条目"
        MB_插入预制条目.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_插入预制条目.Size = New Size(150, 32)
        MB_插入预制条目.TabIndex = 11
        MB_插入预制条目.Text = "插入预制条目"
        ' 
        ' HCL_进阶质量控制
        ' 
        HCL_进阶质量控制.AutoSize = True
        HCL_进阶质量控制.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_进阶质量控制.Dock = DockStyle.Top
        HCL_进阶质量控制.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_进阶质量控制.Location = New Point(20, 277)
        HCL_进阶质量控制.Margin = New Padding(2)
        HCL_进阶质量控制.Name = "HCL_进阶质量控制"
        HCL_进阶质量控制.Padding = New Padding(0, 20, 0, 5)
        HCL_进阶质量控制.Size = New Size(807, 49)
        HCL_进阶质量控制.TabIndex = 12
        HCL_进阶质量控制.Text = "<span style=""font-size:13; color:Silver"">进阶质量控制</span>   可以将编码器内部小参写在这里"
        ' 
        ' Panel1
        ' 
        Panel1.Controls.Add(MTB_缓冲区)
        Panel1.Controls.Add(JustEmptyControl5)
        Panel1.Controls.Add(MTB_最高比特率)
        Panel1.Controls.Add(JustEmptyControl4)
        Panel1.Controls.Add(MTB_最低比特率)
        Panel1.Controls.Add(JustEmptyControl3)
        Panel1.Controls.Add(MTB_基础比特率)
        Panel1.Dock = DockStyle.Top
        Panel1.Location = New Point(20, 235)
        Panel1.Name = "Panel1"
        Panel1.Padding = New Padding(0, 10, 0, 0)
        Panel1.Size = New Size(807, 42)
        Panel1.TabIndex = 10
        ' 
        ' MTB_缓冲区
        ' 
        MTB_缓冲区.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_缓冲区.BorderColor = Color.Transparent
        MTB_缓冲区.BorderColorFocus = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MTB_缓冲区.BorderRadius = 10
        MTB_缓冲区.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        MTB_缓冲区.Dock = DockStyle.Left
        MTB_缓冲区.Location = New Point(405, 10)
        MTB_缓冲区.Margin = New Padding(2)
        MTB_缓冲区.Name = "MTB_缓冲区"
        MTB_缓冲区.Padding = New Padding(10, 0, 10, 0)
        MTB_缓冲区.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_缓冲区.Size = New Size(125, 32)
        MTB_缓冲区.TabIndex = 19
        MTB_缓冲区.WaterText = "-bufsize"
        MTB_缓冲区.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' JustEmptyControl5
        ' 
        JustEmptyControl5.Dock = DockStyle.Left
        JustEmptyControl5.Location = New Point(395, 10)
        JustEmptyControl5.Name = "JustEmptyControl5"
        JustEmptyControl5.Size = New Size(10, 32)
        JustEmptyControl5.TabIndex = 18
        ' 
        ' MTB_最高比特率
        ' 
        MTB_最高比特率.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_最高比特率.BorderColor = Color.Transparent
        MTB_最高比特率.BorderColorFocus = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MTB_最高比特率.BorderRadius = 10
        MTB_最高比特率.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        MTB_最高比特率.Dock = DockStyle.Left
        MTB_最高比特率.Location = New Point(270, 10)
        MTB_最高比特率.Margin = New Padding(2)
        MTB_最高比特率.Name = "MTB_最高比特率"
        MTB_最高比特率.Padding = New Padding(10, 0, 10, 0)
        MTB_最高比特率.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_最高比特率.Size = New Size(125, 32)
        MTB_最高比特率.TabIndex = 17
        MTB_最高比特率.WaterText = "-maxrate"
        MTB_最高比特率.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' JustEmptyControl4
        ' 
        JustEmptyControl4.Dock = DockStyle.Left
        JustEmptyControl4.Location = New Point(260, 10)
        JustEmptyControl4.Name = "JustEmptyControl4"
        JustEmptyControl4.Size = New Size(10, 32)
        JustEmptyControl4.TabIndex = 16
        ' 
        ' MTB_最低比特率
        ' 
        MTB_最低比特率.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_最低比特率.BorderColor = Color.Transparent
        MTB_最低比特率.BorderColorFocus = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MTB_最低比特率.BorderRadius = 10
        MTB_最低比特率.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        MTB_最低比特率.Dock = DockStyle.Left
        MTB_最低比特率.Location = New Point(135, 10)
        MTB_最低比特率.Margin = New Padding(2)
        MTB_最低比特率.Name = "MTB_最低比特率"
        MTB_最低比特率.Padding = New Padding(10, 0, 10, 0)
        MTB_最低比特率.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_最低比特率.Size = New Size(125, 32)
        MTB_最低比特率.TabIndex = 15
        MTB_最低比特率.WaterText = "-minrate"
        MTB_最低比特率.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' JustEmptyControl3
        ' 
        JustEmptyControl3.Dock = DockStyle.Left
        JustEmptyControl3.Location = New Point(125, 10)
        JustEmptyControl3.Name = "JustEmptyControl3"
        JustEmptyControl3.Size = New Size(10, 32)
        JustEmptyControl3.TabIndex = 14
        ' 
        ' MTB_基础比特率
        ' 
        MTB_基础比特率.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_基础比特率.BorderColor = Color.Transparent
        MTB_基础比特率.BorderColorFocus = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MTB_基础比特率.BorderRadius = 10
        MTB_基础比特率.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        MTB_基础比特率.Dock = DockStyle.Left
        MTB_基础比特率.Location = New Point(0, 10)
        MTB_基础比特率.Margin = New Padding(2)
        MTB_基础比特率.Name = "MTB_基础比特率"
        MTB_基础比特率.Padding = New Padding(10, 0, 10, 0)
        MTB_基础比特率.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_基础比特率.Size = New Size(125, 32)
        MTB_基础比特率.TabIndex = 13
        MTB_基础比特率.WaterText = "-b:v"
        MTB_基础比特率.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' Panel4
        ' 
        Panel4.Controls.Add(HCL_缓冲区)
        Panel4.Controls.Add(HCL_最高比特率)
        Panel4.Controls.Add(HCL_最低比特率)
        Panel4.Controls.Add(HCL_基础比特率)
        Panel4.Dock = DockStyle.Top
        Panel4.Location = New Point(20, 205)
        Panel4.Name = "Panel4"
        Panel4.Size = New Size(807, 30)
        Panel4.TabIndex = 9
        ' 
        ' HCL_缓冲区
        ' 
        HCL_缓冲区.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_缓冲区.Dock = DockStyle.Left
        HCL_缓冲区.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_缓冲区.Location = New Point(405, 0)
        HCL_缓冲区.Margin = New Padding(2)
        HCL_缓冲区.Name = "HCL_缓冲区"
        HCL_缓冲区.Size = New Size(135, 30)
        HCL_缓冲区.TabIndex = 3
        HCL_缓冲区.Text = "缓冲区"
        HCL_缓冲区.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.BottomLeft
        ' 
        ' HCL_最高比特率
        ' 
        HCL_最高比特率.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_最高比特率.Dock = DockStyle.Left
        HCL_最高比特率.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_最高比特率.Location = New Point(270, 0)
        HCL_最高比特率.Margin = New Padding(2)
        HCL_最高比特率.Name = "HCL_最高比特率"
        HCL_最高比特率.Size = New Size(135, 30)
        HCL_最高比特率.TabIndex = 2
        HCL_最高比特率.Text = "最高比特率"
        HCL_最高比特率.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.BottomLeft
        ' 
        ' HCL_最低比特率
        ' 
        HCL_最低比特率.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_最低比特率.Dock = DockStyle.Left
        HCL_最低比特率.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_最低比特率.Location = New Point(135, 0)
        HCL_最低比特率.Margin = New Padding(2)
        HCL_最低比特率.Name = "HCL_最低比特率"
        HCL_最低比特率.Size = New Size(135, 30)
        HCL_最低比特率.TabIndex = 1
        HCL_最低比特率.Text = "最低比特率"
        HCL_最低比特率.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.BottomLeft
        ' 
        ' HCL_基础比特率
        ' 
        HCL_基础比特率.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_基础比特率.Dock = DockStyle.Left
        HCL_基础比特率.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_基础比特率.Location = New Point(0, 0)
        HCL_基础比特率.Margin = New Padding(2)
        HCL_基础比特率.Name = "HCL_基础比特率"
        HCL_基础比特率.Size = New Size(135, 30)
        HCL_基础比特率.TabIndex = 0
        HCL_基础比特率.Text = "基础比特率"
        HCL_基础比特率.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.BottomLeft
        ' 
        ' HCL_比特率单位提示
        ' 
        HCL_比特率单位提示.AutoSize = True
        HCL_比特率单位提示.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_比特率单位提示.Dock = DockStyle.Top
        HCL_比特率单位提示.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_比特率单位提示.Location = New Point(20, 185)
        HCL_比特率单位提示.Margin = New Padding(2)
        HCL_比特率单位提示.Name = "HCL_比特率单位提示"
        HCL_比特率单位提示.Size = New Size(807, 20)
        HCL_比特率单位提示.TabIndex = 11
        HCL_比特率单位提示.Text = "注意带上单位，推荐使用 <span style=""color:Gainsboro"">k</span>（kbps），例如 5000k，其他还有 <span style=""color:Gainsboro"">M</span>（mbps，可能要大写）"
        ' 
        ' HCL_比特率
        ' 
        HCL_比特率.AutoSize = True
        HCL_比特率.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_比特率.Dock = DockStyle.Top
        HCL_比特率.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_比特率.Location = New Point(20, 136)
        HCL_比特率.Margin = New Padding(2)
        HCL_比特率.Name = "HCL_比特率"
        HCL_比特率.Padding = New Padding(0, 20, 0, 5)
        HCL_比特率.Size = New Size(807, 49)
        HCL_比特率.TabIndex = 8
        HCL_比特率.Text = "<span style=""font-size:13; color:Silver"">比特率</span>   传统的转码直接写比特率，范围和缓冲区可配合全局质量控制"
        ' 
        ' Panel2
        ' 
        Panel2.Controls.Add(MTB_质量值)
        Panel2.Controls.Add(JustEmptyControl1)
        Panel2.Controls.Add(MCB_质量参数名称)
        Panel2.Controls.Add(JustEmptyControl2)
        Panel2.Controls.Add(MCB_全局质量控制方式)
        Panel2.Dock = DockStyle.Top
        Panel2.Location = New Point(20, 94)
        Panel2.Name = "Panel2"
        Panel2.Padding = New Padding(0, 10, 0, 0)
        Panel2.Size = New Size(807, 42)
        Panel2.TabIndex = 6
        ' 
        ' MTB_质量值
        ' 
        MTB_质量值.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_质量值.BorderColor = Color.Transparent
        MTB_质量值.BorderColorFocus = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MTB_质量值.BorderRadius = 10
        MTB_质量值.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        MTB_质量值.Dock = DockStyle.Left
        MTB_质量值.Location = New Point(470, 10)
        MTB_质量值.Margin = New Padding(2)
        MTB_质量值.Name = "MTB_质量值"
        MTB_质量值.Padding = New Padding(10, 0, 10, 0)
        MTB_质量值.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_质量值.Size = New Size(100, 32)
        MTB_质量值.TabIndex = 12
        MTB_质量值.WaterText = "质量值"
        MTB_质量值.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' JustEmptyControl1
        ' 
        JustEmptyControl1.Dock = DockStyle.Left
        JustEmptyControl1.Location = New Point(460, 10)
        JustEmptyControl1.Name = "JustEmptyControl1"
        JustEmptyControl1.Size = New Size(10, 32)
        JustEmptyControl1.TabIndex = 11
        ' 
        ' MCB_质量参数名称
        ' 
        MCB_质量参数名称.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_质量参数名称.BorderRadius = 10
        MCB_质量参数名称.BorderSize = 0
        MCB_质量参数名称.Dock = DockStyle.Left
        MCB_质量参数名称.DropDownBackdropBlurPasses = 2
        MCB_质量参数名称.DropDownBackdropBlurRadius = 30
        MCB_质量参数名称.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_质量参数名称.DropDownHoverColor = Color.FromArgb(CByte(20), CByte(220), CByte(220), CByte(220))
        MCB_质量参数名称.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_质量参数名称.DropDownPadding = New Padding(10)
        MCB_质量参数名称.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_质量参数名称.DropDownSelectedForeColor = Color.White
        MCB_质量参数名称.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_质量参数名称.Items.Add("")
        MCB_质量参数名称.Items.Add("-crf")
        MCB_质量参数名称.Items.Add("-cq")
        MCB_质量参数名称.Items.Add("-global_quality")
        MCB_质量参数名称.Items.Add("-qp")
        MCB_质量参数名称.Location = New Point(310, 10)
        MCB_质量参数名称.Margin = New Padding(2, 2, 2, 2)
        MCB_质量参数名称.Name = "MCB_质量参数名称"
        MCB_质量参数名称.Padding = New Padding(10, 0, 10, 0)
        MCB_质量参数名称.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_质量参数名称.Size = New Size(150, 32)
        MCB_质量参数名称.TabIndex = 10
        MCB_质量参数名称.ToolTipGap = -1
        MCB_质量参数名称.ToolTipMaxWidth = 350
        MCB_质量参数名称.ToolTipPadding = New Padding(15)
        MCB_质量参数名称.WaterText = "参数名"
        MCB_质量参数名称.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' JustEmptyControl2
        ' 
        JustEmptyControl2.Dock = DockStyle.Left
        JustEmptyControl2.Location = New Point(300, 10)
        JustEmptyControl2.Name = "JustEmptyControl2"
        JustEmptyControl2.Size = New Size(10, 32)
        JustEmptyControl2.TabIndex = 9
        ' 
        ' MCB_全局质量控制方式
        ' 
        MCB_全局质量控制方式.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_全局质量控制方式.BorderRadius = 10
        MCB_全局质量控制方式.BorderSize = 0
        MCB_全局质量控制方式.Dock = DockStyle.Left
        MCB_全局质量控制方式.DropDownBackdropBlurPasses = 2
        MCB_全局质量控制方式.DropDownBackdropBlurRadius = 30
        MCB_全局质量控制方式.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_全局质量控制方式.DropDownHoverColor = Color.FromArgb(CByte(20), CByte(220), CByte(220), CByte(220))
        MCB_全局质量控制方式.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_全局质量控制方式.DropDownPadding = New Padding(10)
        MCB_全局质量控制方式.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_全局质量控制方式.DropDownSelectedForeColor = Color.White
        MCB_全局质量控制方式.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_全局质量控制方式.Items.Add("")
        MCB_全局质量控制方式.Items.Add("恒定质量 CRF - CPU 编码首选")
        MCB_全局质量控制方式.Items.Add("动态码率 VBR - GPU 编码首选")
        MCB_全局质量控制方式.Items.Add("恒定量化 CQP - 不常用 / 不推荐")
        MCB_全局质量控制方式.Items.Add("恒定速率 CBR - 仅旧场景")
        MCB_全局质量控制方式.Items.Add("二次编码 TPE - 使用基础码率")
        MCB_全局质量控制方式.Location = New Point(0, 10)
        MCB_全局质量控制方式.Margin = New Padding(2, 2, 2, 2)
        MCB_全局质量控制方式.Name = "MCB_全局质量控制方式"
        MCB_全局质量控制方式.Padding = New Padding(10, 0, 10, 0)
        MCB_全局质量控制方式.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_全局质量控制方式.Size = New Size(300, 32)
        MCB_全局质量控制方式.TabIndex = 0
        MCB_全局质量控制方式.ToolTipGap = -1
        MCB_全局质量控制方式.ToolTipMaxWidth = 350
        MCB_全局质量控制方式.ToolTipPadding = New Padding(15)
        MCB_全局质量控制方式.WaterText = "选择控制方式 -rc"
        MCB_全局质量控制方式.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' HCL_质量
        ' 
        HCL_质量.AutoSize = True
        HCL_质量.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_质量.Dock = DockStyle.Top
        HCL_质量.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_质量.Location = New Point(20, 49)
        HCL_质量.Margin = New Padding(2)
        HCL_质量.Name = "HCL_质量"
        HCL_质量.Padding = New Padding(0, 0, 0, 5)
        HCL_质量.Size = New Size(807, 45)
        HCL_质量.TabIndex = 7
        HCL_质量.Text = resources.GetString("HCL_质量.Text")
        ' 
        ' HCL_全局质量控制
        ' 
        HCL_全局质量控制.AutoSize = True
        HCL_全局质量控制.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_全局质量控制.Dock = DockStyle.Top
        HCL_全局质量控制.Location = New Point(20, 20)
        HCL_全局质量控制.Margin = New Padding(2)
        HCL_全局质量控制.Name = "HCL_全局质量控制"
        HCL_全局质量控制.Padding = New Padding(0, 0, 0, 5)
        HCL_全局质量控制.Size = New Size(807, 29)
        HCL_全局质量控制.TabIndex = 5
        HCL_全局质量控制.Text = "<span style=""font-size:13; color:Silver"">全局质量控制</span>   <span style=""font-size:10pt; color:Goldenrod"">常规压制仅需在此设置全局质量即可满足需求</span>"
        ' 
        ' Form_v6_参数面板_质量
        ' 
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ClientSize = New Size(847, 676)
        Controls.Add(ModernPanel1)
        Font = New Font("Microsoft YaHei UI", 10F)
        ForeColor = Color.Silver
        Name = "Form_v6_参数面板_质量"
        Text = "Form_v6_参数面板_质量"
        ModernPanel1.ResumeLayout(False)
        ModernPanel1.PerformLayout()
        Panel3.ResumeLayout(False)
        Panel3.PerformLayout()
        Panel1.ResumeLayout(False)
        Panel4.ResumeLayout(False)
        Panel2.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents ModernPanel1 As LakeUI.ModernPanel
    Friend WithEvents HCL_全局质量控制 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents MCB_全局质量控制方式 As LakeUI.ModernComboBox
    Friend WithEvents JustEmptyControl2 As LakeUI.JustEmptyControl
    Friend WithEvents JustEmptyControl1 As LakeUI.JustEmptyControl
    Friend WithEvents MCB_质量参数名称 As LakeUI.ModernComboBox
    Friend WithEvents MTB_质量值 As LakeUI.ModernTextBox
    Friend WithEvents HCL_质量 As LakeUI.HtmlColorLabel
    Friend WithEvents HCL_比特率 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents MTB_基础比特率 As LakeUI.ModernTextBox
    Friend WithEvents Panel4 As Panel
    Friend WithEvents HCL_比特率单位提示 As LakeUI.HtmlColorLabel
    Friend WithEvents MTB_缓冲区 As LakeUI.ModernTextBox
    Friend WithEvents JustEmptyControl5 As LakeUI.JustEmptyControl
    Friend WithEvents MTB_最高比特率 As LakeUI.ModernTextBox
    Friend WithEvents JustEmptyControl4 As LakeUI.JustEmptyControl
    Friend WithEvents MTB_最低比特率 As LakeUI.ModernTextBox
    Friend WithEvents JustEmptyControl3 As LakeUI.JustEmptyControl
    Friend WithEvents HCL_缓冲区 As LakeUI.HtmlColorLabel
    Friend WithEvents HCL_最高比特率 As LakeUI.HtmlColorLabel
    Friend WithEvents HCL_最低比特率 As LakeUI.HtmlColorLabel
    Friend WithEvents HCL_基础比特率 As LakeUI.HtmlColorLabel
    Friend WithEvents HCL_进阶质量控制 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents MB_插入预制条目 As LakeUI.ModernButton
    Friend WithEvents JustEmptyControl8 As LakeUI.JustEmptyControl
    Friend WithEvents MTB_进阶质量控制参数 As LakeUI.ModernTextBox
    Friend WithEvents HCL_进阶参数空格提示 As LakeUI.HtmlColorLabel
    Friend WithEvents 预制条目菜单 As LakeUI.ModernContextMenu
End Class
