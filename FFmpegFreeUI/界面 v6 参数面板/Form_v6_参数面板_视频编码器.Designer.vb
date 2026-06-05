<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_v6_参数面板_视频编码器
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_v6_参数面板_视频编码器))
        ModernPanel1 = New LakeUI.ModernPanel()
        Panel6 = New Panel()
        HtmlColorLabel9 = New LakeUI.HtmlColorLabel()
        MTB_threads = New LakeUI.ModernTextBox()
        Panel5 = New Panel()
        HtmlColorLabel8 = New LakeUI.HtmlColorLabel()
        MTB_gpu = New LakeUI.ModernTextBox()
        HtmlColorLabel5 = New LakeUI.HtmlColorLabel()
        Panel4 = New Panel()
        MCB_场景优化 = New LakeUI.ModernComboBox()
        HtmlColorLabel4 = New LakeUI.HtmlColorLabel()
        Panel3 = New Panel()
        MCB_配置文件 = New LakeUI.ModernComboBox()
        HtmlColorLabel3 = New LakeUI.HtmlColorLabel()
        Panel1 = New Panel()
        MCB_编码预设 = New LakeUI.ModernComboBox()
        HtmlColorLabel2 = New LakeUI.HtmlColorLabel()
        Panel7 = New Panel()
        HtmlColorLabel7 = New LakeUI.HtmlColorLabel()
        MTB_图片编码器质量值 = New LakeUI.ModernTextBox()
        Panel2 = New Panel()
        MCB_具体编码器 = New LakeUI.ModernComboBox()
        JustEmptyControl2 = New LakeUI.JustEmptyControl()
        MCB_视频编码器分类 = New LakeUI.ModernComboBox()
        JustEmptyControl1 = New LakeUI.JustEmptyControl()
        MCB_视频编码器类型 = New LakeUI.ModernComboBox()
        HtmlColorLabel6 = New LakeUI.HtmlColorLabel()
        HtmlColorLabel1 = New LakeUI.HtmlColorLabel()
        ModernPanel1.SuspendLayout()
        Panel6.SuspendLayout()
        Panel5.SuspendLayout()
        Panel4.SuspendLayout()
        Panel3.SuspendLayout()
        Panel1.SuspendLayout()
        Panel7.SuspendLayout()
        Panel2.SuspendLayout()
        SuspendLayout()
        '
        ' ModernPanel1
        '
        ModernPanel1.BackColor1 = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ModernPanel1.BorderSize = 0
        ModernPanel1.Controls.Add(Panel6)
        ModernPanel1.Controls.Add(Panel5)
        ModernPanel1.Controls.Add(HtmlColorLabel5)
        ModernPanel1.Controls.Add(Panel4)
        ModernPanel1.Controls.Add(HtmlColorLabel4)
        ModernPanel1.Controls.Add(Panel3)
        ModernPanel1.Controls.Add(HtmlColorLabel3)
        ModernPanel1.Controls.Add(Panel1)
        ModernPanel1.Controls.Add(HtmlColorLabel2)
        ModernPanel1.Controls.Add(Panel7)
        ModernPanel1.Controls.Add(Panel2)
        ModernPanel1.Controls.Add(HtmlColorLabel6)
        ModernPanel1.Controls.Add(HtmlColorLabel1)
        ModernPanel1.Dock = DockStyle.Fill
        ModernPanel1.Location = New Point(0, 0)
        ModernPanel1.Name = "ModernPanel1"
        ModernPanel1.Padding = New Padding(20)
        ModernPanel1.ScrollBarMode = LakeUI.ModernPanel.ScrollMode.Vertical
        ModernPanel1.Size = New Size(902, 625)
        ModernPanel1.TabIndex = 0
        ' 
        ' Panel6
        ' 
        Panel6.Controls.Add(HtmlColorLabel9)
        Panel6.Controls.Add(MTB_threads)
        Panel6.Dock = DockStyle.Top
        Panel6.Location = New Point(20, 528)
        Panel6.Name = "Panel6"
        Panel6.Padding = New Padding(0, 10, 0, 0)
        Panel6.Size = New Size(862, 42)
        Panel6.TabIndex = 12
        ' 
        ' HtmlColorLabel9
        ' 
        HtmlColorLabel9.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel9.Dock = DockStyle.Fill
        HtmlColorLabel9.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HtmlColorLabel9.Location = New Point(100, 10)
        HtmlColorLabel9.Margin = New Padding(2)
        HtmlColorLabel9.Name = "HtmlColorLabel9"
        HtmlColorLabel9.Padding = New Padding(10, 0, 0, 0)
        HtmlColorLabel9.Size = New Size(762, 32)
        HtmlColorLabel9.TabIndex = 8
        HtmlColorLabel9.Text = "指定 CPU 编码线程数，不一定有效，编码器有自己的逻辑"
        HtmlColorLabel9.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.MiddleLeft
        ' 
        ' MTB_threads
        ' 
        MTB_threads.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_threads.BorderColor = Color.Transparent
        MTB_threads.BorderColorFocus = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MTB_threads.BorderRadius = 10
        MTB_threads.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        MTB_threads.Dock = DockStyle.Left
        MTB_threads.Location = New Point(0, 10)
        MTB_threads.Margin = New Padding(2)
        MTB_threads.Name = "MTB_threads"
        MTB_threads.Padding = New Padding(10, 0, 10, 0)
        MTB_threads.Size = New Size(100, 32)
        MTB_threads.TabIndex = 4
        MTB_threads.WaterText = "-threads"
        MTB_threads.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' Panel5
        ' 
        Panel5.Controls.Add(HtmlColorLabel8)
        Panel5.Controls.Add(MTB_gpu)
        Panel5.Dock = DockStyle.Top
        Panel5.Location = New Point(20, 486)
        Panel5.Name = "Panel5"
        Panel5.Padding = New Padding(0, 10, 0, 0)
        Panel5.Size = New Size(862, 42)
        Panel5.TabIndex = 11
        ' 
        ' HtmlColorLabel8
        ' 
        HtmlColorLabel8.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel8.Dock = DockStyle.Fill
        HtmlColorLabel8.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HtmlColorLabel8.Location = New Point(100, 10)
        HtmlColorLabel8.Margin = New Padding(2)
        HtmlColorLabel8.Name = "HtmlColorLabel8"
        HtmlColorLabel8.Padding = New Padding(10, 0, 0, 0)
        HtmlColorLabel8.Size = New Size(762, 32)
        HtmlColorLabel8.TabIndex = 7
        HtmlColorLabel8.Text = "指定 NVIDIA 显卡索引号，其他卡请从系统硬件加速或驱动中设置"
        HtmlColorLabel8.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.MiddleLeft
        ' 
        ' MTB_gpu
        ' 
        MTB_gpu.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_gpu.BorderColor = Color.Transparent
        MTB_gpu.BorderColorFocus = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MTB_gpu.BorderRadius = 10
        MTB_gpu.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        MTB_gpu.Dock = DockStyle.Left
        MTB_gpu.Location = New Point(0, 10)
        MTB_gpu.Margin = New Padding(2)
        MTB_gpu.Name = "MTB_gpu"
        MTB_gpu.Padding = New Padding(10, 0, 10, 0)
        MTB_gpu.Size = New Size(100, 32)
        MTB_gpu.TabIndex = 4
        MTB_gpu.WaterText = "-gpu"
        MTB_gpu.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' HtmlColorLabel5
        ' 
        HtmlColorLabel5.AutoSize = True
        HtmlColorLabel5.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel5.Dock = DockStyle.Top
        HtmlColorLabel5.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HtmlColorLabel5.Location = New Point(20, 436)
        HtmlColorLabel5.Margin = New Padding(2)
        HtmlColorLabel5.Name = "HtmlColorLabel5"
        HtmlColorLabel5.Padding = New Padding(0, 20, 0, 5)
        HtmlColorLabel5.Size = New Size(862, 50)
        HtmlColorLabel5.TabIndex = 10
        HtmlColorLabel5.Text = "<span style=""font-size:13; color:Silver"">性能选项</span>   通常不需要考虑，也不一定起作用"
        ' 
        ' Panel4
        ' 
        Panel4.Controls.Add(MCB_场景优化)
        Panel4.Dock = DockStyle.Top
        Panel4.Location = New Point(20, 394)
        Panel4.Name = "Panel4"
        Panel4.Padding = New Padding(0, 10, 0, 0)
        Panel4.Size = New Size(862, 42)
        Panel4.TabIndex = 9
        ' 
        ' MCB_场景优化
        ' 
        MCB_场景优化.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_场景优化.BorderRadius = 10
        MCB_场景优化.BorderSize = 0
        MCB_场景优化.Dock = DockStyle.Left
        MCB_场景优化.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_场景优化.DropDownHoverAnimationDuration = 0
        MCB_场景优化.DropDownHoverColor = Color.FromArgb(CByte(80), CByte(0), CByte(0), CByte(0))
        MCB_场景优化.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_场景优化.DropDownPadding = New Padding(10)
        MCB_场景优化.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_场景优化.DropDownSelectedForeColor = Color.White
        MCB_场景优化.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_场景优化.Location = New Point(0, 10)
        MCB_场景优化.Margin = New Padding(2, 2, 2, 2)
        MCB_场景优化.Name = "MCB_场景优化"
        MCB_场景优化.Padding = New Padding(10, 0, 10, 0)
        MCB_场景优化.Size = New Size(150, 32)
        MCB_场景优化.TabIndex = 0
        MCB_场景优化.ToolTipGap = -1
        MCB_场景优化.ToolTipMaxWidth = 350
        MCB_场景优化.ToolTipPadding = New Padding(15)
        MCB_场景优化.WaterText = "-tune"
        MCB_场景优化.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' HtmlColorLabel4
        ' 
        HtmlColorLabel4.AutoSize = True
        HtmlColorLabel4.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel4.Dock = DockStyle.Top
        HtmlColorLabel4.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HtmlColorLabel4.Location = New Point(20, 344)
        HtmlColorLabel4.Margin = New Padding(2)
        HtmlColorLabel4.Name = "HtmlColorLabel4"
        HtmlColorLabel4.Padding = New Padding(0, 20, 0, 5)
        HtmlColorLabel4.Size = New Size(862, 50)
        HtmlColorLabel4.TabIndex = 8
        HtmlColorLabel4.Text = "<span style=""font-size:13; color:Silver"">场景优化</span>   对特定需求的专项优化，例如 CPU 编码的颗粒保留或是 GPU 编码的特调模式"
        ' 
        ' Panel3
        ' 
        Panel3.Controls.Add(MCB_配置文件)
        Panel3.Dock = DockStyle.Top
        Panel3.Location = New Point(20, 302)
        Panel3.Name = "Panel3"
        Panel3.Padding = New Padding(0, 10, 0, 0)
        Panel3.Size = New Size(862, 42)
        Panel3.TabIndex = 7
        ' 
        ' MCB_配置文件
        ' 
        MCB_配置文件.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_配置文件.BorderRadius = 10
        MCB_配置文件.BorderSize = 0
        MCB_配置文件.Dock = DockStyle.Left
        MCB_配置文件.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_配置文件.DropDownHoverAnimationDuration = 0
        MCB_配置文件.DropDownHoverColor = Color.FromArgb(CByte(80), CByte(0), CByte(0), CByte(0))
        MCB_配置文件.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_配置文件.DropDownPadding = New Padding(10)
        MCB_配置文件.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_配置文件.DropDownSelectedForeColor = Color.White
        MCB_配置文件.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_配置文件.Location = New Point(0, 10)
        MCB_配置文件.Margin = New Padding(2, 2, 2, 2)
        MCB_配置文件.Name = "MCB_配置文件"
        MCB_配置文件.Padding = New Padding(10, 0, 10, 0)
        MCB_配置文件.Size = New Size(150, 32)
        MCB_配置文件.TabIndex = 0
        MCB_配置文件.ToolTipGap = -1
        MCB_配置文件.ToolTipMaxWidth = 350
        MCB_配置文件.ToolTipPadding = New Padding(15)
        MCB_配置文件.WaterText = "-profile"
        MCB_配置文件.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' HtmlColorLabel3
        ' 
        HtmlColorLabel3.AutoSize = True
        HtmlColorLabel3.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel3.Dock = DockStyle.Top
        HtmlColorLabel3.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HtmlColorLabel3.Location = New Point(20, 252)
        HtmlColorLabel3.Margin = New Padding(2)
        HtmlColorLabel3.Name = "HtmlColorLabel3"
        HtmlColorLabel3.Padding = New Padding(0, 20, 0, 5)
        HtmlColorLabel3.Size = New Size(862, 50)
        HtmlColorLabel3.TabIndex = 6
        HtmlColorLabel3.Text = "<span style=""font-size:13; color:Silver"">配置文件</span>   控制要支持怎样的技术规格和功能，一般不用指定"
        ' 
        ' Panel1
        ' 
        Panel1.Controls.Add(MCB_编码预设)
        Panel1.Dock = DockStyle.Top
        Panel1.Location = New Point(20, 210)
        Panel1.Name = "Panel1"
        Panel1.Padding = New Padding(0, 10, 0, 0)
        Panel1.Size = New Size(862, 42)
        Panel1.TabIndex = 5
        ' 
        ' MCB_编码预设
        ' 
        MCB_编码预设.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_编码预设.BorderRadius = 10
        MCB_编码预设.BorderSize = 0
        MCB_编码预设.Dock = DockStyle.Left
        MCB_编码预设.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_编码预设.DropDownHoverAnimationDuration = 0
        MCB_编码预设.DropDownHoverColor = Color.FromArgb(CByte(80), CByte(0), CByte(0), CByte(0))
        MCB_编码预设.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_编码预设.DropDownPadding = New Padding(10)
        MCB_编码预设.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_编码预设.DropDownSelectedForeColor = Color.White
        MCB_编码预设.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_编码预设.Location = New Point(0, 10)
        MCB_编码预设.Margin = New Padding(2, 2, 2, 2)
        MCB_编码预设.Name = "MCB_编码预设"
        MCB_编码预设.Padding = New Padding(10, 0, 10, 0)
        MCB_编码预设.Size = New Size(150, 32)
        MCB_编码预设.TabIndex = 0
        MCB_编码预设.ToolTipGap = -1
        MCB_编码预设.ToolTipMaxWidth = 350
        MCB_编码预设.ToolTipPadding = New Padding(15)
        MCB_编码预设.WaterText = "-preset"
        MCB_编码预设.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' HtmlColorLabel2
        ' 
        HtmlColorLabel2.AutoSize = True
        HtmlColorLabel2.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel2.Dock = DockStyle.Top
        HtmlColorLabel2.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HtmlColorLabel2.Location = New Point(20, 160)
        HtmlColorLabel2.Margin = New Padding(2)
        HtmlColorLabel2.Name = "HtmlColorLabel2"
        HtmlColorLabel2.Padding = New Padding(0, 20, 0, 5)
        HtmlColorLabel2.Size = New Size(862, 50)
        HtmlColorLabel2.TabIndex = 4
        HtmlColorLabel2.Text = "<span style=""font-size:13; color:Silver"">编码预设</span>   如何平衡压缩度和速度，往上越慢，往下越快"
        ' 
        ' Panel7
        ' 
        Panel7.Controls.Add(HtmlColorLabel7)
        Panel7.Controls.Add(MTB_图片编码器质量值)
        Panel7.Dock = DockStyle.Top
        Panel7.Location = New Point(20, 118)
        Panel7.Name = "Panel7"
        Panel7.Padding = New Padding(0, 10, 0, 0)
        Panel7.Size = New Size(862, 42)
        Panel7.TabIndex = 14
        Panel7.Visible = False
        ' 
        ' HtmlColorLabel7
        ' 
        HtmlColorLabel7.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel7.Dock = DockStyle.Fill
        HtmlColorLabel7.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HtmlColorLabel7.Location = New Point(150, 10)
        HtmlColorLabel7.Margin = New Padding(2)
        HtmlColorLabel7.Name = "HtmlColorLabel7"
        HtmlColorLabel7.Padding = New Padding(10, 0, 0, 0)
        HtmlColorLabel7.Size = New Size(712, 32)
        HtmlColorLabel7.TabIndex = 6
        HtmlColorLabel7.Text = "图片编码器质量值 / 其他定制参数"
        HtmlColorLabel7.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.MiddleLeft
        ' MTB_图片编码器质量值
        MTB_图片编码器质量值.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_图片编码器质量值.BorderColor = Color.Transparent
        MTB_图片编码器质量值.BorderColorFocus = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MTB_图片编码器质量值.BorderRadius = 10
        MTB_图片编码器质量值.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        MTB_图片编码器质量值.Dock = DockStyle.Left
        MTB_图片编码器质量值.Location = New Point(0, 10)
        MTB_图片编码器质量值.Margin = New Padding(2)
        MTB_图片编码器质量值.Name = "MTB_图片编码器质量值"
        MTB_图片编码器质量值.Padding = New Padding(10, 0, 10, 0)
        MTB_图片编码器质量值.Size = New Size(150, 32)
        MTB_图片编码器质量值.TabIndex = 5
        MTB_图片编码器质量值.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' Panel2
        ' 
        Panel2.Controls.Add(MCB_具体编码器)
        Panel2.Controls.Add(JustEmptyControl2)
        Panel2.Controls.Add(MCB_视频编码器分类)
        Panel2.Controls.Add(JustEmptyControl1)
        Panel2.Controls.Add(MCB_视频编码器类型)
        Panel2.Dock = DockStyle.Top
        Panel2.Location = New Point(20, 76)
        Panel2.Name = "Panel2"
        Panel2.Padding = New Padding(0, 10, 0, 0)
        Panel2.Size = New Size(862, 42)
        Panel2.TabIndex = 3
        ' 
        ' MCB_具体编码器
        ' 
        MCB_具体编码器.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_具体编码器.BorderRadius = 10
        MCB_具体编码器.BorderSize = 0
        MCB_具体编码器.Dock = DockStyle.Left
        MCB_具体编码器.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_具体编码器.DropDownHoverAnimationDuration = 0
        MCB_具体编码器.DropDownHoverColor = Color.FromArgb(CByte(80), CByte(0), CByte(0), CByte(0))
        MCB_具体编码器.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_具体编码器.DropDownPadding = New Padding(10)
        MCB_具体编码器.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_具体编码器.DropDownSelectedForeColor = Color.White
        MCB_具体编码器.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_具体编码器.Location = New Point(370, 10)
        MCB_具体编码器.Margin = New Padding(2, 2, 2, 2)
        MCB_具体编码器.MaxDropDownItems = 20
        MCB_具体编码器.Name = "MCB_具体编码器"
        MCB_具体编码器.Padding = New Padding(10, 0, 10, 0)
        MCB_具体编码器.Size = New Size(160, 32)
        MCB_具体编码器.TabIndex = 6
        MCB_具体编码器.ToolTipGap = -1
        MCB_具体编码器.ToolTipMaxWidth = 350
        MCB_具体编码器.ToolTipPadding = New Padding(15)
        MCB_具体编码器.WaterText = "具体编码"
        MCB_具体编码器.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' JustEmptyControl2
        ' 
        JustEmptyControl2.Dock = DockStyle.Left
        JustEmptyControl2.Location = New Point(360, 10)
        JustEmptyControl2.Name = "JustEmptyControl2"
        JustEmptyControl2.Size = New Size(10, 32)
        JustEmptyControl2.TabIndex = 5
        ' 
        ' MCB_视频编码器分类
        ' 
        MCB_视频编码器分类.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_视频编码器分类.BorderRadius = 10
        MCB_视频编码器分类.BorderSize = 0
        MCB_视频编码器分类.Dock = DockStyle.Left
        MCB_视频编码器分类.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_视频编码器分类.DropDownHoverAnimationDuration = 0
        MCB_视频编码器分类.DropDownHoverColor = Color.FromArgb(CByte(80), CByte(0), CByte(0), CByte(0))
        MCB_视频编码器分类.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_视频编码器分类.DropDownPadding = New Padding(10)
        MCB_视频编码器分类.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_视频编码器分类.DropDownSelectedForeColor = Color.White
        MCB_视频编码器分类.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_视频编码器分类.Location = New Point(160, 10)
        MCB_视频编码器分类.Margin = New Padding(2, 2, 2, 2)
        MCB_视频编码器分类.MaxDropDownItems = 15
        MCB_视频编码器分类.Name = "MCB_视频编码器分类"
        MCB_视频编码器分类.Padding = New Padding(10, 0, 10, 0)
        MCB_视频编码器分类.Size = New Size(200, 32)
        MCB_视频编码器分类.TabIndex = 4
        MCB_视频编码器分类.ToolTipGap = -1
        MCB_视频编码器分类.ToolTipMaxWidth = 350
        MCB_视频编码器分类.ToolTipPadding = New Padding(15)
        MCB_视频编码器分类.WaterText = "分类"
        MCB_视频编码器分类.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' JustEmptyControl1
        ' 
        JustEmptyControl1.Dock = DockStyle.Left
        JustEmptyControl1.Location = New Point(150, 10)
        JustEmptyControl1.Name = "JustEmptyControl1"
        JustEmptyControl1.Size = New Size(10, 32)
        JustEmptyControl1.TabIndex = 3
        ' 
        ' MCB_视频编码器类型
        ' 
        MCB_视频编码器类型.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_视频编码器类型.BorderRadius = 10
        MCB_视频编码器类型.BorderSize = 0
        MCB_视频编码器类型.Dock = DockStyle.Left
        MCB_视频编码器类型.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_视频编码器类型.DropDownHoverAnimationDuration = 0
        MCB_视频编码器类型.DropDownHoverColor = Color.FromArgb(CByte(80), CByte(0), CByte(0), CByte(0))
        MCB_视频编码器类型.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_视频编码器类型.DropDownPadding = New Padding(10)
        MCB_视频编码器类型.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_视频编码器类型.DropDownSelectedForeColor = Color.White
        MCB_视频编码器类型.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_视频编码器类型.Items.Add("")
        MCB_视频编码器类型.Items.Add("视频")
        MCB_视频编码器类型.Items.Add("图片")
        MCB_视频编码器类型.Location = New Point(0, 10)
        MCB_视频编码器类型.Margin = New Padding(2, 2, 2, 2)
        MCB_视频编码器类型.Name = "MCB_视频编码器类型"
        MCB_视频编码器类型.Padding = New Padding(10, 0, 10, 0)
        MCB_视频编码器类型.Size = New Size(150, 32)
        MCB_视频编码器类型.TabIndex = 0
        MCB_视频编码器类型.ToolTipGap = -1
        MCB_视频编码器类型.ToolTipMaxWidth = 350
        MCB_视频编码器类型.ToolTipPadding = New Padding(15)
        MCB_视频编码器类型.WaterText = "类型"
        MCB_视频编码器类型.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' HtmlColorLabel6
        ' 
        HtmlColorLabel6.AutoSize = True
        HtmlColorLabel6.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel6.Dock = DockStyle.Top
        HtmlColorLabel6.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HtmlColorLabel6.Location = New Point(20, 50)
        HtmlColorLabel6.Margin = New Padding(2)
        HtmlColorLabel6.Name = "HtmlColorLabel6"
        HtmlColorLabel6.Padding = New Padding(0, 0, 0, 5)
        HtmlColorLabel6.Size = New Size(862, 26)
        HtmlColorLabel6.TabIndex = 13
        HtmlColorLabel6.Text = resources.GetString("HtmlColorLabel6.Text")
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
        HtmlColorLabel1.Padding = New Padding(0, 0, 0, 5)
        HtmlColorLabel1.Size = New Size(862, 30)
        HtmlColorLabel1.TabIndex = 1
        HtmlColorLabel1.Text = "<span style=""font-size:13; color:Silver"">视频编码器</span>   依次选择类别，再选具体；可编辑设置文件添加自定义"
        ' 
        ' Form_v6_参数面板_视频编码器
        ' 
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ClientSize = New Size(902, 625)
        Controls.Add(ModernPanel1)
        Font = New Font("Microsoft YaHei UI", 10F)
        ForeColor = Color.Silver
        Name = "Form_v6_参数面板_视频编码器"
        Text = "Form_v6_参数面板_视频编码器"
        ModernPanel1.ResumeLayout(False)
        ModernPanel1.PerformLayout()
        Panel6.ResumeLayout(False)
        Panel5.ResumeLayout(False)
        Panel4.ResumeLayout(False)
        Panel3.ResumeLayout(False)
        Panel1.ResumeLayout(False)
        Panel7.ResumeLayout(False)
        Panel2.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents ModernPanel1 As LakeUI.ModernPanel
    Friend WithEvents HtmlColorLabel1 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents MCB_视频编码器类型 As LakeUI.ModernComboBox
    Friend WithEvents MCB_具体编码器 As LakeUI.ModernComboBox
    Friend WithEvents JustEmptyControl2 As LakeUI.JustEmptyControl
    Friend WithEvents MCB_视频编码器分类 As LakeUI.ModernComboBox
    Friend WithEvents JustEmptyControl1 As LakeUI.JustEmptyControl
    Friend WithEvents HtmlColorLabel2 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents MCB_编码预设 As LakeUI.ModernComboBox
    Friend WithEvents Panel3 As Panel
    Friend WithEvents MCB_配置文件 As LakeUI.ModernComboBox
    Friend WithEvents HtmlColorLabel3 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents MCB_场景优化 As LakeUI.ModernComboBox
    Friend WithEvents HtmlColorLabel4 As LakeUI.HtmlColorLabel
    Friend WithEvents HtmlColorLabel5 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents MTB_gpu As LakeUI.ModernTextBox
    Friend WithEvents Panel6 As Panel
    Friend WithEvents MTB_threads As LakeUI.ModernTextBox
    Friend WithEvents HtmlColorLabel6 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel7 As Panel
    Friend WithEvents MTB_图片编码器质量值 As LakeUI.ModernTextBox
    Friend WithEvents HtmlColorLabel7 As LakeUI.HtmlColorLabel
    Friend WithEvents HtmlColorLabel9 As LakeUI.HtmlColorLabel
    Friend WithEvents HtmlColorLabel8 As LakeUI.HtmlColorLabel
End Class
