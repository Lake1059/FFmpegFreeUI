<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_v6_起始页面
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_v6_起始页面))
        ModernPanel2 = New LakeUI.ModernPanel()
        Panel1 = New Panel()
        HtmlColorLabel2 = New LakeUI.HtmlColorLabel()
        ModernComboBox1 = New LakeUI.ModernComboBox()
        HtmlColorLabel1 = New LakeUI.HtmlColorLabel()
        ModernPanel3 = New LakeUI.ModernPanel()
        ModernPanel1 = New LakeUI.ModernPanel()
        ModernPanel5 = New LakeUI.ModernPanel()
        ModernButton5 = New LakeUI.ModernButton()
        JustEmptyControl7 = New LakeUI.JustEmptyControl()
        ModernButton16 = New LakeUI.ModernButton()
        JustEmptyControl6 = New LakeUI.JustEmptyControl()
        ModernButton15 = New LakeUI.ModernButton()
        JustEmptyControl5 = New LakeUI.JustEmptyControl()
        ModernButton14 = New LakeUI.ModernButton()
        HtmlColorLabel5 = New LakeUI.HtmlColorLabel()
        MB_更新器更新 = New LakeUI.ModernButton()
        JustEmptyControl4 = New LakeUI.JustEmptyControl()
        MB_软件本体更新 = New LakeUI.ModernButton()
        HtmlColorLabel4 = New LakeUI.HtmlColorLabel()
        JustEmptyControl3 = New LakeUI.JustEmptyControl()
        ModernPanel6 = New LakeUI.ModernPanel()
        HtmlColorLabel10 = New LakeUI.HtmlColorLabel()
        JustEmptyControl2 = New LakeUI.JustEmptyControl()
        ModernPanel4 = New LakeUI.ModernPanel()
        ModernButton11 = New LakeUI.ModernButton()
        ModernButton9 = New LakeUI.ModernButton()
        ModernButton8 = New LakeUI.ModernButton()
        ModernButton4 = New LakeUI.ModernButton()
        ModernButton3 = New LakeUI.ModernButton()
        ModernButton6 = New LakeUI.ModernButton()
        ModernButton2 = New LakeUI.ModernButton()
        ModernButton1 = New LakeUI.ModernButton()
        HtmlColorLabel3 = New LakeUI.HtmlColorLabel()
        JustEmptyControl1 = New LakeUI.JustEmptyControl()
        ModernPanel2.SuspendLayout()
        Panel1.SuspendLayout()
        ModernPanel1.SuspendLayout()
        ModernPanel5.SuspendLayout()
        ModernPanel6.SuspendLayout()
        ModernPanel4.SuspendLayout()
        SuspendLayout()
        ' 
        ' ModernPanel2
        ' 
        ModernPanel2.BackColor1 = Color.Transparent
        ModernPanel2.BorderRadius = 10
        ModernPanel2.BorderSize = 0
        ModernPanel2.Controls.Add(Panel1)
        ModernPanel2.Controls.Add(HtmlColorLabel1)
        ModernPanel2.Controls.Add(ModernPanel3)
        ModernPanel2.Dock = DockStyle.Top
        ModernPanel2.Location = New Point(15, 10)
        ModernPanel2.Name = "ModernPanel2"
        ModernPanel2.OverlayColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernPanel2.Padding = New Padding(15)
        ModernPanel2.ScrollBarMode = LakeUI.ModernPanel.ScrollMode.None
        ModernPanel2.Size = New Size(934, 100)
        ModernPanel2.TabIndex = 0
        ' 
        ' Panel1
        ' 
        Panel1.AutoSize = True
        Panel1.Controls.Add(HtmlColorLabel2)
        Panel1.Controls.Add(ModernComboBox1)
        Panel1.Dock = DockStyle.Right
        Panel1.Location = New Point(730, 20)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(184, 60)
        Panel1.TabIndex = 14
        ' 
        ' HtmlColorLabel2
        ' 
        HtmlColorLabel2.AutoSize = True
        HtmlColorLabel2.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel2.Dock = DockStyle.Right
        HtmlColorLabel2.Font = New Font("Microsoft YaHei UI", 10F, FontStyle.Bold)
        HtmlColorLabel2.Location = New Point(0, 0)
        HtmlColorLabel2.Margin = New Padding(2)
        HtmlColorLabel2.Name = "HtmlColorLabel2"
        HtmlColorLabel2.Size = New Size(184, 28)
        HtmlColorLabel2.TabIndex = 14
        HtmlColorLabel2.Text = "@湖边的稻草 (1059 Studio)"
        HtmlColorLabel2.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.TopRight
        ' 
        ' ModernComboBox1
        ' 
        ModernComboBox1.BackColor1 = Color.FromArgb(CByte(120), CByte(0), CByte(0), CByte(0))
        ModernComboBox1.BorderColorFocus = Color.Silver
        ModernComboBox1.BorderRadius = 10
        ModernComboBox1.BorderSize = 0
        ModernComboBox1.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        ModernComboBox1.Dock = DockStyle.Bottom
        ModernComboBox1.DropDownBorderSize = 2
        ModernComboBox1.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        ModernComboBox1.DropDownPadding = New Padding(10)
        ModernComboBox1.DropDownScrollBarHoverColor = Color.FromArgb(CByte(200), CByte(200), CByte(200))
        ModernComboBox1.Items.Add("清理 3FUI 内存 (GC)")
        ModernComboBox1.Items.Add("清理 3FUI 内存 (内核)")
        ModernComboBox1.Location = New Point(0, 28)
        ModernComboBox1.Margin = New Padding(2, 2, 2, 2)
        ModernComboBox1.MaxDropDownItems = 15
        ModernComboBox1.Name = "ModernComboBox1"
        ModernComboBox1.Padding = New Padding(10, 0, 10, 0)
        ModernComboBox1.Size = New Size(184, 32)
        ModernComboBox1.TabIndex = 16
        ModernComboBox1.ToolTipBorderSize = 2
        ModernComboBox1.ToolTipGap = 10
        ModernComboBox1.ToolTipMaxWidth = 350
        ModernComboBox1.ToolTipPadding = New Padding(15)
        ModernComboBox1.WaterText = "清理内存条"
        ModernComboBox1.WaterTextForeColor = Color.Silver
        ' 
        ' HtmlColorLabel1
        ' 
        HtmlColorLabel1.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel1.Dock = DockStyle.Fill
        HtmlColorLabel1.Font = New Font("Microsoft YaHei UI", 15F)
        HtmlColorLabel1.LineSpacing = 5
        HtmlColorLabel1.Location = New Point(80, 20)
        HtmlColorLabel1.Margin = New Padding(2)
        HtmlColorLabel1.Name = "HtmlColorLabel1"
        HtmlColorLabel1.Padding = New Padding(15, 0, 0, 0)
        HtmlColorLabel1.Size = New Size(834, 60)
        HtmlColorLabel1.TabIndex = 1
        HtmlColorLabel1.Text = "<b>FFmpegFreeUI 6.0.0 Dev.1 ReDesign With LakeUI</b><br><span style=""font-size:10pt; color:CornflowerBlue"">将 ffmpeg、ffplay、ffprobe 加入环境变量或放置于当前目录即可调用</span>"
        HtmlColorLabel1.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.MiddleLeft
        ' 
        ' ModernPanel3
        ' 
        ModernPanel3.BackColor1 = Color.Transparent
        ModernPanel3.BorderColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        ModernPanel3.BorderRadius = 10
        ModernPanel3.BorderSize = 0
        ModernPanel3.Dock = DockStyle.Left
        ModernPanel3.Image = CType(resources.GetObject("ModernPanel3.Image"), Image)
        ModernPanel3.Location = New Point(20, 20)
        ModernPanel3.Name = "ModernPanel3"
        ModernPanel3.ScrollBarMode = LakeUI.ModernPanel.ScrollMode.None
        ModernPanel3.Size = New Size(60, 60)
        ModernPanel3.SuperSamplingScale = LakeUI.Class1.SuperSamplingScaleEnum.x2
        ModernPanel3.TabIndex = 0
        ' 
        ' ModernPanel1
        ' 
        ModernPanel1.BackColor = Color.Transparent
        ModernPanel1.BackColor1 = Color.Transparent
        ModernPanel1.BorderRoundCorners = New LakeUI.RoundCorners(False, False, False, False)
        ModernPanel1.BorderSize = 0
        ModernPanel1.Controls.Add(ModernPanel5)
        ModernPanel1.Controls.Add(JustEmptyControl3)
        ModernPanel1.Controls.Add(ModernPanel6)
        ModernPanel1.Controls.Add(JustEmptyControl2)
        ModernPanel1.Controls.Add(ModernPanel4)
        ModernPanel1.Controls.Add(JustEmptyControl1)
        ModernPanel1.Controls.Add(ModernPanel2)
        ModernPanel1.Dock = DockStyle.Fill
        ModernPanel1.Location = New Point(0, 0)
        ModernPanel1.Name = "ModernPanel1"
        ModernPanel1.Padding = New Padding(15, 10, 15, 15)
        ModernPanel1.ScrollBarMode = LakeUI.ModernPanel.ScrollMode.None
        ModernPanel1.Size = New Size(964, 721)
        ModernPanel1.TabIndex = 1
        ' 
        ' ModernPanel5
        ' 
        ModernPanel5.BackColor1 = Color.Transparent
        ModernPanel5.BorderColor = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        ModernPanel5.BorderRadius = 10
        ModernPanel5.BorderSize = 0
        ModernPanel5.Controls.Add(ModernButton5)
        ModernPanel5.Controls.Add(JustEmptyControl7)
        ModernPanel5.Controls.Add(ModernButton16)
        ModernPanel5.Controls.Add(JustEmptyControl6)
        ModernPanel5.Controls.Add(ModernButton15)
        ModernPanel5.Controls.Add(JustEmptyControl5)
        ModernPanel5.Controls.Add(ModernButton14)
        ModernPanel5.Controls.Add(HtmlColorLabel5)
        ModernPanel5.Controls.Add(MB_更新器更新)
        ModernPanel5.Controls.Add(JustEmptyControl4)
        ModernPanel5.Controls.Add(MB_软件本体更新)
        ModernPanel5.Controls.Add(HtmlColorLabel4)
        ModernPanel5.Dock = DockStyle.Fill
        ModernPanel5.Location = New Point(330, 125)
        ModernPanel5.Name = "ModernPanel5"
        ModernPanel5.OverlayColor = Color.FromArgb(CByte(120), CByte(0), CByte(0), CByte(0))
        ModernPanel5.Padding = New Padding(15)
        ModernPanel5.ScrollBarMode = LakeUI.ModernPanel.ScrollMode.None
        ModernPanel5.Size = New Size(304, 581)
        ModernPanel5.TabIndex = 4
        ' 
        ' ModernButton5
        ' 
        ModernButton5.AnimationDuration = 0
        ModernButton5.BackColor1 = Color.FromArgb(CByte(30), CByte(220), CByte(220), CByte(220))
        ModernButton5.BorderRadius = 10
        ModernButton5.BorderSize = 0
        ModernButton5.Dock = DockStyle.Top
        ModernButton5.Font = New Font("Microsoft YaHei UI", 10F)
        ModernButton5.HoverBackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernButton5.Location = New Point(20, 427)
        ModernButton5.Margin = New Padding(2)
        ModernButton5.Name = "ModernButton5"
        ModernButton5.Padding = New Padding(3, 0, 0, 0)
        ModernButton5.PressedBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        ModernButton5.Size = New Size(264, 55)
        ModernButton5.SubText = "体积/版本/下载速度/百分比"
        ModernButton5.SubTextForeColor = Color.Peru
        ModernButton5.TabIndex = 26
        ModernButton5.Text = "Agent 智能体组件"
        ModernButton5.TextAlign = LakeUI.ModernButton.TextAlignEnum.Left
        ' 
        ' JustEmptyControl7
        ' 
        JustEmptyControl7.BackColor = Color.Transparent
        JustEmptyControl7.Dock = DockStyle.Top
        JustEmptyControl7.Location = New Point(20, 417)
        JustEmptyControl7.Name = "JustEmptyControl7"
        JustEmptyControl7.Size = New Size(264, 10)
        JustEmptyControl7.TabIndex = 25
        ' 
        ' ModernButton16
        ' 
        ModernButton16.AnimationDuration = 0
        ModernButton16.BackColor1 = Color.FromArgb(CByte(30), CByte(220), CByte(220), CByte(220))
        ModernButton16.BorderRadius = 10
        ModernButton16.BorderSize = 0
        ModernButton16.Dock = DockStyle.Top
        ModernButton16.Font = New Font("Microsoft YaHei UI", 10F)
        ModernButton16.HoverBackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernButton16.Location = New Point(20, 362)
        ModernButton16.Margin = New Padding(2)
        ModernButton16.Name = "ModernButton16"
        ModernButton16.Padding = New Padding(3, 0, 0, 0)
        ModernButton16.PressedBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        ModernButton16.Size = New Size(264, 55)
        ModernButton16.SubText = "体积/版本/下载速度/百分比"
        ModernButton16.SubTextForeColor = Color.Peru
        ModernButton16.TabIndex = 24
        ModernButton16.Text = "自建社区应用组件"
        ModernButton16.TextAlign = LakeUI.ModernButton.TextAlignEnum.Left
        ' 
        ' JustEmptyControl6
        ' 
        JustEmptyControl6.BackColor = Color.Transparent
        JustEmptyControl6.Dock = DockStyle.Top
        JustEmptyControl6.Location = New Point(20, 352)
        JustEmptyControl6.Name = "JustEmptyControl6"
        JustEmptyControl6.Size = New Size(264, 10)
        JustEmptyControl6.TabIndex = 18
        ' 
        ' ModernButton15
        ' 
        ModernButton15.AnimationDuration = 0
        ModernButton15.BackColor1 = Color.FromArgb(CByte(30), CByte(220), CByte(220), CByte(220))
        ModernButton15.BorderRadius = 10
        ModernButton15.BorderSize = 0
        ModernButton15.Dock = DockStyle.Top
        ModernButton15.Font = New Font("Microsoft YaHei UI", 10F)
        ModernButton15.HoverBackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernButton15.Location = New Point(20, 297)
        ModernButton15.Margin = New Padding(2)
        ModernButton15.Name = "ModernButton15"
        ModernButton15.Padding = New Padding(3, 0, 0, 0)
        ModernButton15.PressedBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        ModernButton15.Size = New Size(264, 55)
        ModernButton15.SubText = "体积/版本/下载速度/百分比"
        ModernButton15.SubTextForeColor = Color.Peru
        ModernButton15.TabIndex = 23
        ModernButton15.Text = "VLC | 可视化剪辑区间交互"
        ModernButton15.TextAlign = LakeUI.ModernButton.TextAlignEnum.Left
        ' 
        ' JustEmptyControl5
        ' 
        JustEmptyControl5.BackColor = Color.Transparent
        JustEmptyControl5.Dock = DockStyle.Top
        JustEmptyControl5.Location = New Point(20, 287)
        JustEmptyControl5.Name = "JustEmptyControl5"
        JustEmptyControl5.Size = New Size(264, 10)
        JustEmptyControl5.TabIndex = 15
        ' 
        ' ModernButton14
        ' 
        ModernButton14.AnimationDuration = 0
        ModernButton14.BackColor1 = Color.FromArgb(CByte(30), CByte(220), CByte(220), CByte(220))
        ModernButton14.BorderRadius = 10
        ModernButton14.BorderSize = 0
        ModernButton14.Dock = DockStyle.Top
        ModernButton14.Font = New Font("Microsoft YaHei UI", 10F)
        ModernButton14.HoverBackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernButton14.Location = New Point(20, 232)
        ModernButton14.Margin = New Padding(2)
        ModernButton14.Name = "ModernButton14"
        ModernButton14.Padding = New Padding(3, 0, 0, 0)
        ModernButton14.PressedBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        ModernButton14.Size = New Size(264, 55)
        ModernButton14.SubText = "体积/版本/下载速度/百分比"
        ModernButton14.SubTextForeColor = Color.Peru
        ModernButton14.TabIndex = 22
        ModernButton14.Text = "LHM | 驱动级性能监控"
        ModernButton14.TextAlign = LakeUI.ModernButton.TextAlignEnum.Left
        ' 
        ' HtmlColorLabel5
        ' 
        HtmlColorLabel5.AutoSize = True
        HtmlColorLabel5.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel5.Dock = DockStyle.Top
        HtmlColorLabel5.Font = New Font("Microsoft YaHei UI", 13F, FontStyle.Bold)
        HtmlColorLabel5.Location = New Point(20, 176)
        HtmlColorLabel5.Margin = New Padding(2)
        HtmlColorLabel5.Name = "HtmlColorLabel5"
        HtmlColorLabel5.Padding = New Padding(0, 20, 0, 10)
        HtmlColorLabel5.Size = New Size(264, 56)
        HtmlColorLabel5.TabIndex = 3
        HtmlColorLabel5.Text = "分离的组件"
        HtmlColorLabel5.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.Center
        ' 
        ' MB_更新器更新
        ' 
        MB_更新器更新.AnimationDuration = 0
        MB_更新器更新.BackColor1 = Color.FromArgb(CByte(30), CByte(220), CByte(220), CByte(220))
        MB_更新器更新.BorderRadius = 10
        MB_更新器更新.BorderSize = 0
        MB_更新器更新.Dock = DockStyle.Top
        MB_更新器更新.Font = New Font("Microsoft YaHei UI", 10F)
        MB_更新器更新.HoverBackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_更新器更新.Location = New Point(20, 121)
        MB_更新器更新.Margin = New Padding(2)
        MB_更新器更新.Name = "MB_更新器更新"
        MB_更新器更新.Padding = New Padding(3, 0, 0, 0)
        MB_更新器更新.PressedBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_更新器更新.Size = New Size(264, 55)
        MB_更新器更新.SubText = "体积/版本/下载速度/百分比"
        MB_更新器更新.SubTextForeColor = Color.CornflowerBlue
        MB_更新器更新.TabIndex = 21
        MB_更新器更新.Text = "控制台更新程序 0.0.1"
        MB_更新器更新.TextAlign = LakeUI.ModernButton.TextAlignEnum.Left
        ' 
        ' JustEmptyControl4
        ' 
        JustEmptyControl4.BackColor = Color.Transparent
        JustEmptyControl4.Dock = DockStyle.Top
        JustEmptyControl4.Location = New Point(20, 111)
        JustEmptyControl4.Name = "JustEmptyControl4"
        JustEmptyControl4.Size = New Size(264, 10)
        JustEmptyControl4.TabIndex = 13
        ' 
        ' MB_软件本体更新
        ' 
        MB_软件本体更新.AnimationDuration = 0
        MB_软件本体更新.BackColor1 = Color.FromArgb(CByte(30), CByte(220), CByte(220), CByte(220))
        MB_软件本体更新.BorderRadius = 10
        MB_软件本体更新.BorderSize = 0
        MB_软件本体更新.Dock = DockStyle.Top
        MB_软件本体更新.Font = New Font("Microsoft YaHei UI", 10F)
        MB_软件本体更新.HoverBackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_软件本体更新.Location = New Point(20, 56)
        MB_软件本体更新.Margin = New Padding(2)
        MB_软件本体更新.Name = "MB_软件本体更新"
        MB_软件本体更新.Padding = New Padding(3, 0, 0, 0)
        MB_软件本体更新.PressedBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_软件本体更新.Size = New Size(264, 55)
        MB_软件本体更新.SubText = "体积/版本/下载速度/百分比"
        MB_软件本体更新.SubTextForeColor = Color.YellowGreen
        MB_软件本体更新.TabIndex = 20
        MB_软件本体更新.Text = "[更新来源] 云端版本 6.0.0"
        MB_软件本体更新.TextAlign = LakeUI.ModernButton.TextAlignEnum.Left
        ' 
        ' HtmlColorLabel4
        ' 
        HtmlColorLabel4.AutoSize = True
        HtmlColorLabel4.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel4.Dock = DockStyle.Top
        HtmlColorLabel4.Font = New Font("Microsoft YaHei UI", 13F, FontStyle.Bold)
        HtmlColorLabel4.Location = New Point(20, 20)
        HtmlColorLabel4.Margin = New Padding(2)
        HtmlColorLabel4.Name = "HtmlColorLabel4"
        HtmlColorLabel4.Padding = New Padding(0, 0, 0, 10)
        HtmlColorLabel4.Size = New Size(264, 36)
        HtmlColorLabel4.TabIndex = 1
        HtmlColorLabel4.Text = "软件更新"
        HtmlColorLabel4.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.Center
        ' 
        ' JustEmptyControl3
        ' 
        JustEmptyControl3.BackColor = Color.Transparent
        JustEmptyControl3.Dock = DockStyle.Right
        JustEmptyControl3.Location = New Point(634, 125)
        JustEmptyControl3.Name = "JustEmptyControl3"
        JustEmptyControl3.Size = New Size(15, 581)
        JustEmptyControl3.TabIndex = 6
        ' 
        ' ModernPanel6
        ' 
        ModernPanel6.BackColor1 = Color.Transparent
        ModernPanel6.BorderColor = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        ModernPanel6.BorderRadius = 10
        ModernPanel6.BorderSize = 0
        ModernPanel6.Controls.Add(HtmlColorLabel10)
        ModernPanel6.Dock = DockStyle.Right
        ModernPanel6.Location = New Point(649, 125)
        ModernPanel6.Name = "ModernPanel6"
        ModernPanel6.OverlayColor = Color.FromArgb(CByte(120), CByte(0), CByte(0), CByte(0))
        ModernPanel6.Padding = New Padding(15)
        ModernPanel6.ScrollBarMode = LakeUI.ModernPanel.ScrollMode.None
        ModernPanel6.Size = New Size(300, 581)
        ModernPanel6.TabIndex = 5
        ' 
        ' HtmlColorLabel10
        ' 
        HtmlColorLabel10.AutoSize = True
        HtmlColorLabel10.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel10.Dock = DockStyle.Top
        HtmlColorLabel10.Font = New Font("Microsoft YaHei UI", 13F, FontStyle.Bold)
        HtmlColorLabel10.Location = New Point(20, 20)
        HtmlColorLabel10.Margin = New Padding(2)
        HtmlColorLabel10.Name = "HtmlColorLabel10"
        HtmlColorLabel10.Padding = New Padding(0, 0, 0, 10)
        HtmlColorLabel10.Size = New Size(260, 36)
        HtmlColorLabel10.TabIndex = 2
        HtmlColorLabel10.Text = "新闻列表"
        HtmlColorLabel10.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.Center
        ' 
        ' JustEmptyControl2
        ' 
        JustEmptyControl2.BackColor = Color.Transparent
        JustEmptyControl2.Dock = DockStyle.Left
        JustEmptyControl2.Location = New Point(315, 125)
        JustEmptyControl2.Name = "JustEmptyControl2"
        JustEmptyControl2.Size = New Size(15, 581)
        JustEmptyControl2.TabIndex = 3
        ' 
        ' ModernPanel4
        ' 
        ModernPanel4.BackColor1 = Color.Transparent
        ModernPanel4.BorderColor = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        ModernPanel4.BorderRadius = 10
        ModernPanel4.BorderSize = 0
        ModernPanel4.Controls.Add(ModernButton11)
        ModernPanel4.Controls.Add(ModernButton9)
        ModernPanel4.Controls.Add(ModernButton8)
        ModernPanel4.Controls.Add(ModernButton4)
        ModernPanel4.Controls.Add(ModernButton3)
        ModernPanel4.Controls.Add(ModernButton6)
        ModernPanel4.Controls.Add(ModernButton2)
        ModernPanel4.Controls.Add(ModernButton1)
        ModernPanel4.Controls.Add(HtmlColorLabel3)
        ModernPanel4.Dock = DockStyle.Left
        ModernPanel4.Location = New Point(15, 125)
        ModernPanel4.Name = "ModernPanel4"
        ModernPanel4.OverlayColor = Color.FromArgb(CByte(120), CByte(0), CByte(0), CByte(0))
        ModernPanel4.Padding = New Padding(15)
        ModernPanel4.ScrollBarMode = LakeUI.ModernPanel.ScrollMode.None
        ModernPanel4.Size = New Size(300, 581)
        ModernPanel4.TabIndex = 2
        ' 
        ' ModernButton11
        ' 
        ModernButton11.AnimationDuration = 0
        ModernButton11.BackColor1 = Color.Transparent
        ModernButton11.BorderRadius = 10
        ModernButton11.BorderSize = 0
        ModernButton11.Dock = DockStyle.Bottom
        ModernButton11.Font = New Font("Microsoft YaHei UI", 10F)
        ModernButton11.HoverBackColor1 = Color.FromArgb(CByte(30), CByte(220), CByte(220), CByte(220))
        ModernButton11.Location = New Point(20, 511)
        ModernButton11.Margin = New Padding(2)
        ModernButton11.Name = "ModernButton11"
        ModernButton11.PressedBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        ModernButton11.Size = New Size(260, 50)
        ModernButton11.SubText = "将 Web 体验带入 WinForms"
        ModernButton11.SubTextForeColor = Color.DarkGray
        ModernButton11.TabIndex = 14
        ModernButton11.Text = "界面主框架：LakeUI"
        ModernButton11.TextAlign = LakeUI.ModernButton.TextAlignEnum.Left
        ' 
        ' ModernButton9
        ' 
        ModernButton9.AnimationDuration = 0
        ModernButton9.BackColor1 = Color.Transparent
        ModernButton9.BorderRadius = 10
        ModernButton9.BorderSize = 0
        ModernButton9.Dock = DockStyle.Top
        ModernButton9.ForeColor = Color.YellowGreen
        ModernButton9.HoverBackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernButton9.Location = New Point(20, 316)
        ModernButton9.Margin = New Padding(2)
        ModernButton9.Name = "ModernButton9"
        ModernButton9.PressedBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        ModernButton9.Size = New Size(260, 30)
        ModernButton9.SubTextForeColor = Color.DarkGray
        ModernButton9.TabIndex = 12
        ModernButton9.Text = "知乎 终末诗的教程"
        ModernButton9.TextAlign = LakeUI.ModernButton.TextAlignEnum.Left
        ' 
        ' ModernButton8
        ' 
        ModernButton8.AnimationDuration = 0
        ModernButton8.BackColor1 = Color.Transparent
        ModernButton8.BorderRadius = 10
        ModernButton8.BorderSize = 0
        ModernButton8.Dock = DockStyle.Top
        ModernButton8.ForeColor = Color.YellowGreen
        ModernButton8.HoverBackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernButton8.Location = New Point(20, 286)
        ModernButton8.Margin = New Padding(2)
        ModernButton8.Name = "ModernButton8"
        ModernButton8.PressedBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        ModernButton8.Size = New Size(260, 30)
        ModernButton8.SubTextForeColor = Color.DarkGray
        ModernButton8.TabIndex = 11
        ModernButton8.Text = "3FUI 6.0 重构版本宣传视频"
        ModernButton8.TextAlign = LakeUI.ModernButton.TextAlignEnum.Left
        ' 
        ' ModernButton4
        ' 
        ModernButton4.AnimationDuration = 0
        ModernButton4.BackColor1 = Color.Transparent
        ModernButton4.BorderRadius = 10
        ModernButton4.BorderSize = 0
        ModernButton4.Dock = DockStyle.Top
        ModernButton4.ForeColor = Color.MediumPurple
        ModernButton4.HoverBackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernButton4.Location = New Point(20, 236)
        ModernButton4.Margin = New Padding(2)
        ModernButton4.Name = "ModernButton4"
        ModernButton4.PressedBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        ModernButton4.Size = New Size(260, 50)
        ModernButton4.SubText = "购买 SP 支持者包请前往电铺页面"
        ModernButton4.SubTextForeColor = Color.DarkGray
        ModernButton4.TabIndex = 7
        ModernButton4.Text = "爱发电 ifdian.net"
        ModernButton4.TextAlign = LakeUI.ModernButton.TextAlignEnum.Left
        ' 
        ' ModernButton3
        ' 
        ModernButton3.AnimationDuration = 0
        ModernButton3.BackColor1 = Color.Transparent
        ModernButton3.BorderRadius = 10
        ModernButton3.BorderSize = 0
        ModernButton3.Dock = DockStyle.Top
        ModernButton3.ForeColor = Color.FromArgb(CByte(237), CByte(114), CByte(153))
        ModernButton3.HoverBackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernButton3.Location = New Point(20, 186)
        ModernButton3.Margin = New Padding(2)
        ModernButton3.Name = "ModernButton3"
        ModernButton3.PressedBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        ModernButton3.Size = New Size(260, 50)
        ModernButton3.SubText = "唯一动态发布地，无其他任何流媒体账号"
        ModernButton3.SubTextForeColor = Color.DarkGray
        ModernButton3.TabIndex = 6
        ModernButton3.Text = "3FUI 开发者主页 (哔哩哔哩)"
        ModernButton3.TextAlign = LakeUI.ModernButton.TextAlignEnum.Left
        ' 
        ' ModernButton6
        ' 
        ModernButton6.AnimationDuration = 0
        ModernButton6.BackColor1 = Color.Transparent
        ModernButton6.BorderRadius = 10
        ModernButton6.BorderSize = 0
        ModernButton6.Dock = DockStyle.Top
        ModernButton6.ForeColor = Color.CornflowerBlue
        ModernButton6.HoverBackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernButton6.Location = New Point(20, 136)
        ModernButton6.Margin = New Padding(2)
        ModernButton6.Name = "ModernButton6"
        ModernButton6.PressedBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        ModernButton6.Size = New Size(260, 50)
        ModernButton6.SubText = "此为长域名，短域名将于 2028 年废弃"
        ModernButton6.SubTextForeColor = Color.DarkGray
        ModernButton6.TabIndex = 9
        ModernButton6.Text = "官网 ffmpegfreeui.top"
        ModernButton6.TextAlign = LakeUI.ModernButton.TextAlignEnum.Left
        ' 
        ' ModernButton2
        ' 
        ModernButton2.AnimationDuration = 0
        ModernButton2.BackColor1 = Color.Transparent
        ModernButton2.BorderRadius = 10
        ModernButton2.BorderSize = 0
        ModernButton2.Dock = DockStyle.Top
        ModernButton2.ForeColor = Color.YellowGreen
        ModernButton2.HoverBackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernButton2.Location = New Point(20, 106)
        ModernButton2.Margin = New Padding(2)
        ModernButton2.Name = "ModernButton2"
        ModernButton2.PressedBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        ModernButton2.Size = New Size(260, 30)
        ModernButton2.SubTextForeColor = Color.DarkGray
        ModernButton2.TabIndex = 5
        ModernButton2.Text = "3FUI GitHub 仓库"
        ModernButton2.TextAlign = LakeUI.ModernButton.TextAlignEnum.Left
        ' 
        ' ModernButton1
        ' 
        ModernButton1.AnimationDuration = 0
        ModernButton1.BackColor1 = Color.Transparent
        ModernButton1.BorderRadius = 10
        ModernButton1.BorderSize = 0
        ModernButton1.Dock = DockStyle.Top
        ModernButton1.ForeColor = Color.Goldenrod
        ModernButton1.HoverBackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernButton1.Location = New Point(20, 56)
        ModernButton1.Margin = New Padding(2)
        ModernButton1.Name = "ModernButton1"
        ModernButton1.PressedBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        ModernButton1.Size = New Size(260, 50)
        ModernButton1.SubText = "群内提问默认某包的回答全错，不容异议"
        ModernButton1.SubTextForeColor = Color.DarkGray
        ModernButton1.TabIndex = 4
        ModernButton1.Text = "不要相信 AI 的建议！"
        ModernButton1.TextAlign = LakeUI.ModernButton.TextAlignEnum.Left
        ' 
        ' HtmlColorLabel3
        ' 
        HtmlColorLabel3.AutoSize = True
        HtmlColorLabel3.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel3.Dock = DockStyle.Top
        HtmlColorLabel3.Font = New Font("Microsoft YaHei UI", 13F, FontStyle.Bold)
        HtmlColorLabel3.Location = New Point(20, 20)
        HtmlColorLabel3.Margin = New Padding(2)
        HtmlColorLabel3.Name = "HtmlColorLabel3"
        HtmlColorLabel3.Padding = New Padding(0, 0, 0, 10)
        HtmlColorLabel3.Size = New Size(260, 36)
        HtmlColorLabel3.TabIndex = 0
        HtmlColorLabel3.Text = "链接和文档"
        HtmlColorLabel3.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.Center
        ' 
        ' JustEmptyControl1
        ' 
        JustEmptyControl1.BackColor = Color.Transparent
        JustEmptyControl1.Dock = DockStyle.Top
        JustEmptyControl1.Location = New Point(15, 110)
        JustEmptyControl1.Name = "JustEmptyControl1"
        JustEmptyControl1.Size = New Size(934, 15)
        JustEmptyControl1.TabIndex = 1
        ' 
        ' Form_v6_起始页面
        ' 
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        BackgroundImageLayout = ImageLayout.None
        ClientSize = New Size(964, 721)
        Controls.Add(ModernPanel1)
        Font = New Font("Microsoft YaHei UI", 10F)
        ForeColor = Color.Silver
        Name = "Form_v6_起始页面"
        Text = "Form_v6_起始页面"
        ModernPanel2.ResumeLayout(False)
        ModernPanel2.PerformLayout()
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        ModernPanel1.ResumeLayout(False)
        ModernPanel5.ResumeLayout(False)
        ModernPanel5.PerformLayout()
        ModernPanel6.ResumeLayout(False)
        ModernPanel6.PerformLayout()
        ModernPanel4.ResumeLayout(False)
        ModernPanel4.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents ModernPanel2 As LakeUI.ModernPanel
    Friend WithEvents ModernPanel3 As LakeUI.ModernPanel
    Friend WithEvents HtmlColorLabel1 As LakeUI.HtmlColorLabel
    Friend WithEvents ModernPanel1 As LakeUI.ModernPanel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents HtmlColorLabel2 As LakeUI.HtmlColorLabel
    Friend WithEvents ModernComboBox1 As LakeUI.ModernComboBox
    Friend WithEvents JustEmptyControl1 As LakeUI.JustEmptyControl
    Friend WithEvents ModernPanel4 As LakeUI.ModernPanel
    Friend WithEvents ModernPanel5 As LakeUI.ModernPanel
    Friend WithEvents JustEmptyControl3 As LakeUI.JustEmptyControl
    Friend WithEvents ModernPanel6 As LakeUI.ModernPanel
    Friend WithEvents JustEmptyControl2 As LakeUI.JustEmptyControl
    Friend WithEvents HtmlColorLabel3 As LakeUI.HtmlColorLabel
    Friend WithEvents ModernButton1 As LakeUI.ModernButton
    Friend WithEvents ModernButton4 As LakeUI.ModernButton
    Friend WithEvents ModernButton3 As LakeUI.ModernButton
    Friend WithEvents ModernButton2 As LakeUI.ModernButton
    Friend WithEvents ModernButton8 As LakeUI.ModernButton
    Friend WithEvents ModernButton6 As LakeUI.ModernButton
    Friend WithEvents ModernButton9 As LakeUI.ModernButton
    Friend WithEvents HtmlColorLabel4 As LakeUI.HtmlColorLabel
    Friend WithEvents HtmlColorLabel5 As LakeUI.HtmlColorLabel
    Friend WithEvents HtmlColorLabel10 As LakeUI.HtmlColorLabel
    Friend WithEvents JustEmptyControl4 As LakeUI.JustEmptyControl
    Friend WithEvents JustEmptyControl5 As LakeUI.JustEmptyControl
    Friend WithEvents ModernButton11 As LakeUI.ModernButton
    Friend WithEvents JustEmptyControl6 As LakeUI.JustEmptyControl
    Friend WithEvents MB_软件本体更新 As LakeUI.ModernButton
    Friend WithEvents ModernButton16 As LakeUI.ModernButton
    Friend WithEvents ModernButton15 As LakeUI.ModernButton
    Friend WithEvents ModernButton14 As LakeUI.ModernButton
    Friend WithEvents MB_更新器更新 As LakeUI.ModernButton
    Friend WithEvents ModernButton5 As LakeUI.ModernButton
    Friend WithEvents JustEmptyControl7 As LakeUI.JustEmptyControl
End Class
