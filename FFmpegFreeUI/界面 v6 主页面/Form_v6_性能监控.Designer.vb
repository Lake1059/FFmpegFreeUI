<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_v6_性能监控
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
        components = New ComponentModel.Container()
        Dim ToolTipEntry1 As LakeUI.ModernComboBox.ToolTipEntry = New LakeUI.ModernComboBox.ToolTipEntry()
        Dim ToolTipEntry2 As LakeUI.ModernComboBox.ToolTipEntry = New LakeUI.ModernComboBox.ToolTipEntry()
        CpuMonitor1 = New LakeUI.CpuMonitor()
        JustEmptyControl1 = New LakeUI.JustEmptyControl()
        ModernComboBox1 = New LakeUI.ModernComboBox()
        ModernPanel内置显卡监控面板 = New LakeUI.ModernPanel()
        ModernPanel8 = New LakeUI.ModernPanel()
        JustEmptyControl5 = New LakeUI.JustEmptyControl()
        Panel1 = New Panel()
        ModernPanel7 = New LakeUI.ModernPanel()
        RoundDashBoard4 = New LakeUI.RoundDashBoard()
        HtmlColorLabel4 = New LakeUI.HtmlColorLabel()
        JustEmptyControl6 = New LakeUI.JustEmptyControl()
        ModernPanel6 = New LakeUI.ModernPanel()
        RoundDashBoard3 = New LakeUI.RoundDashBoard()
        HtmlColorLabel3 = New LakeUI.HtmlColorLabel()
        JustEmptyControl3 = New LakeUI.JustEmptyControl()
        ModernPanel5 = New LakeUI.ModernPanel()
        RoundDashBoard2 = New LakeUI.RoundDashBoard()
        HtmlColorLabel2 = New LakeUI.HtmlColorLabel()
        JustEmptyControl2 = New LakeUI.JustEmptyControl()
        ModernPanel4 = New LakeUI.ModernPanel()
        RoundDashBoard1 = New LakeUI.RoundDashBoard()
        HtmlColorLabel1 = New LakeUI.HtmlColorLabel()
        RamMonitor1 = New LakeUI.RamMonitor()
        Panel3 = New Panel()
        ModernComboBox3 = New LakeUI.ModernComboBox()
        JustEmptyControl4 = New LakeUI.JustEmptyControl()
        ModernComboBox2 = New LakeUI.ModernComboBox()
        Timer1 = New Timer(components)
        ModernPanel1 = New LakeUI.ModernPanel()
        Panel4 = New Panel()
        JustEmptyControl7 = New LakeUI.JustEmptyControl()
        Panel2 = New Panel()
        ModernPanel内置显卡监控面板.SuspendLayout()
        Panel1.SuspendLayout()
        ModernPanel7.SuspendLayout()
        ModernPanel6.SuspendLayout()
        ModernPanel5.SuspendLayout()
        ModernPanel4.SuspendLayout()
        Panel3.SuspendLayout()
        ModernPanel1.SuspendLayout()
        Panel4.SuspendLayout()
        Panel2.SuspendLayout()
        SuspendLayout()
        ' 
        ' CpuMonitor1
        ' 
        CpuMonitor1.CellBackColor = Color.FromArgb(CByte(120), CByte(0), CByte(0), CByte(0))
        CpuMonitor1.CellBorderThickness = 0F
        CpuMonitor1.CellPadding = 0F
        CpuMonitor1.Dock = DockStyle.Fill
        CpuMonitor1.Font = New Font("Microsoft YaHei UI", 9F)
        CpuMonitor1.HistoryLength = 30
        CpuMonitor1.Location = New Point(0, 42)
        CpuMonitor1.Name = "CpuMonitor1"
        CpuMonitor1.NormalMaxCores = 20
        CpuMonitor1.Size = New Size(305, 570)
        CpuMonitor1.TabIndex = 3
        CpuMonitor1.TextMode = LakeUI.CpuMonitor.TextModeEnum.PercentOnly
        CpuMonitor1.TextPadding = New Padding(0, 5, 0, 5)
        ' 
        ' JustEmptyControl1
        ' 
        JustEmptyControl1.Dock = DockStyle.Top
        JustEmptyControl1.Location = New Point(0, 32)
        JustEmptyControl1.Name = "JustEmptyControl1"
        JustEmptyControl1.Size = New Size(305, 10)
        JustEmptyControl1.TabIndex = 4
        ' 
        ' ModernComboBox1
        ' 
        ModernComboBox1.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernComboBox1.BorderColorFocus = Color.Silver
        ModernComboBox1.BorderRadius = 10
        ModernComboBox1.BorderSize = 0
        ModernComboBox1.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        ModernComboBox1.Dock = DockStyle.Top
        ModernComboBox1.DropDownBorderSize = 2
        ModernComboBox1.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        ModernComboBox1.DropDownPadding = New Padding(10)
        ModernComboBox1.DropDownScrollBarHoverColor = Color.FromArgb(CByte(200), CByte(200), CByte(200))
        ModernComboBox1.Location = New Point(0, 0)
        ModernComboBox1.Margin = New Padding(2, 2, 2, 2)
        ModernComboBox1.Name = "ModernComboBox1"
        ModernComboBox1.Padding = New Padding(10, 0, 10, 0)
        ModernComboBox1.Size = New Size(305, 32)
        ModernComboBox1.TabIndex = 2
        ModernComboBox1.ToolTipBorderSize = 2
        ModernComboBox1.ToolTipGap = 10
        ModernComboBox1.ToolTipMaxWidth = 350
        ModernComboBox1.ToolTipPadding = New Padding(15)
        ModernComboBox1.WaterText = "选择 CPU 组"
        ' 
        ' ModernPanel内置显卡监控面板
        ' 
        ModernPanel内置显卡监控面板.BackColor = Color.Transparent
        ModernPanel内置显卡监控面板.BackColor1 = Color.Transparent
        ModernPanel内置显卡监控面板.BorderSize = 0
        ModernPanel内置显卡监控面板.Controls.Add(ModernPanel8)
        ModernPanel内置显卡监控面板.Controls.Add(JustEmptyControl5)
        ModernPanel内置显卡监控面板.Controls.Add(Panel1)
        ModernPanel内置显卡监控面板.Dock = DockStyle.Fill
        ModernPanel内置显卡监控面板.Location = New Point(0, 42)
        ModernPanel内置显卡监控面板.Name = "ModernPanel内置显卡监控面板"
        ModernPanel内置显卡监控面板.ScrollBarMode = LakeUI.ModernPanel.ScrollMode.None
        ModernPanel内置显卡监控面板.Size = New Size(578, 375)
        ModernPanel内置显卡监控面板.TabIndex = 7
        ' 
        ' ModernPanel8
        ' 
        ModernPanel8.BackColor1 = Color.Transparent
        ModernPanel8.BorderSize = 0
        ModernPanel8.Dock = DockStyle.Fill
        ModernPanel8.LayoutMode = LakeUI.ModernPanel.LayoutModeEnum.Flow
        ModernPanel8.Location = New Point(0, 150)
        ModernPanel8.Name = "ModernPanel8"
        ModernPanel8.ScrollBarMode = LakeUI.ModernPanel.ScrollMode.Vertical
        ModernPanel8.Size = New Size(578, 225)
        ModernPanel8.TabIndex = 1
        ' 
        ' JustEmptyControl5
        ' 
        JustEmptyControl5.Dock = DockStyle.Top
        JustEmptyControl5.Location = New Point(0, 140)
        JustEmptyControl5.Name = "JustEmptyControl5"
        JustEmptyControl5.Size = New Size(578, 10)
        JustEmptyControl5.TabIndex = 5
        ' 
        ' Panel1
        ' 
        Panel1.Controls.Add(ModernPanel7)
        Panel1.Controls.Add(JustEmptyControl6)
        Panel1.Controls.Add(ModernPanel6)
        Panel1.Controls.Add(JustEmptyControl3)
        Panel1.Controls.Add(ModernPanel5)
        Panel1.Controls.Add(JustEmptyControl2)
        Panel1.Controls.Add(ModernPanel4)
        Panel1.Dock = DockStyle.Top
        Panel1.Location = New Point(0, 0)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(578, 140)
        Panel1.TabIndex = 0
        ' 
        ' ModernPanel7
        ' 
        ModernPanel7.BackColor1 = Color.FromArgb(CByte(120), CByte(0), CByte(0), CByte(0))
        ModernPanel7.BorderRadius = 10
        ModernPanel7.BorderSize = 0
        ModernPanel7.Controls.Add(RoundDashBoard4)
        ModernPanel7.Controls.Add(HtmlColorLabel4)
        ModernPanel7.Dock = DockStyle.Fill
        ModernPanel7.Location = New Point(435, 0)
        ModernPanel7.Name = "ModernPanel7"
        ModernPanel7.ScrollBarMode = LakeUI.ModernPanel.ScrollMode.None
        ModernPanel7.Size = New Size(143, 140)
        ModernPanel7.TabIndex = 8
        ' 
        ' RoundDashBoard4
        ' 
        RoundDashBoard4.Dock = DockStyle.Fill
        RoundDashBoard4.FillColor = Color.CornflowerBlue
        RoundDashBoard4.FillGradientColor = Color.IndianRed
        RoundDashBoard4.Location = New Point(5, 41)
        RoundDashBoard4.Margin = New Padding(2, 2, 2, 2)
        RoundDashBoard4.Name = "RoundDashBoard4"
        RoundDashBoard4.Padding = New Padding(10, 10, 10, 15)
        RoundDashBoard4.Size = New Size(133, 94)
        RoundDashBoard4.StartAngle = 0F
        RoundDashBoard4.SweepAngle = 360F
        RoundDashBoard4.TabIndex = 3
        RoundDashBoard4.Thickness = 8F
        RoundDashBoard4.TrackColor = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        ' 
        ' HtmlColorLabel4
        ' 
        HtmlColorLabel4.AutoSize = True
        HtmlColorLabel4.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel4.Dock = DockStyle.Top
        HtmlColorLabel4.Location = New Point(5, 5)
        HtmlColorLabel4.Margin = New Padding(2)
        HtmlColorLabel4.Name = "HtmlColorLabel4"
        HtmlColorLabel4.Padding = New Padding(10, 10, 10, 5)
        HtmlColorLabel4.Size = New Size(133, 36)
        HtmlColorLabel4.TabIndex = 2
        HtmlColorLabel4.Text = "功耗"
        HtmlColorLabel4.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.Center
        ' 
        ' JustEmptyControl6
        ' 
        JustEmptyControl6.Dock = DockStyle.Left
        JustEmptyControl6.Location = New Point(425, 0)
        JustEmptyControl6.Name = "JustEmptyControl6"
        JustEmptyControl6.Size = New Size(10, 140)
        JustEmptyControl6.TabIndex = 7
        ' 
        ' ModernPanel6
        ' 
        ModernPanel6.BackColor1 = Color.FromArgb(CByte(120), CByte(0), CByte(0), CByte(0))
        ModernPanel6.BorderRadius = 10
        ModernPanel6.BorderSize = 0
        ModernPanel6.Controls.Add(RoundDashBoard3)
        ModernPanel6.Controls.Add(HtmlColorLabel3)
        ModernPanel6.Dock = DockStyle.Left
        ModernPanel6.Location = New Point(290, 0)
        ModernPanel6.Name = "ModernPanel6"
        ModernPanel6.ScrollBarMode = LakeUI.ModernPanel.ScrollMode.None
        ModernPanel6.Size = New Size(135, 140)
        ModernPanel6.TabIndex = 6
        ' 
        ' RoundDashBoard3
        ' 
        RoundDashBoard3.Dock = DockStyle.Fill
        RoundDashBoard3.FillColor = Color.CornflowerBlue
        RoundDashBoard3.FillGradientColor = Color.IndianRed
        RoundDashBoard3.Location = New Point(5, 41)
        RoundDashBoard3.Margin = New Padding(2, 2, 2, 2)
        RoundDashBoard3.Name = "RoundDashBoard3"
        RoundDashBoard3.Padding = New Padding(10, 10, 10, 15)
        RoundDashBoard3.Size = New Size(125, 94)
        RoundDashBoard3.StartAngle = 0F
        RoundDashBoard3.SweepAngle = 360F
        RoundDashBoard3.TabIndex = 1
        RoundDashBoard3.Thickness = 8F
        RoundDashBoard3.TrackColor = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        ' 
        ' HtmlColorLabel3
        ' 
        HtmlColorLabel3.AutoSize = True
        HtmlColorLabel3.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel3.Dock = DockStyle.Top
        HtmlColorLabel3.Location = New Point(5, 5)
        HtmlColorLabel3.Margin = New Padding(2)
        HtmlColorLabel3.Name = "HtmlColorLabel3"
        HtmlColorLabel3.Padding = New Padding(10, 10, 10, 5)
        HtmlColorLabel3.Size = New Size(125, 36)
        HtmlColorLabel3.TabIndex = 0
        HtmlColorLabel3.Text = "显存占用"
        HtmlColorLabel3.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.Center
        ' 
        ' JustEmptyControl3
        ' 
        JustEmptyControl3.Dock = DockStyle.Left
        JustEmptyControl3.Location = New Point(280, 0)
        JustEmptyControl3.Name = "JustEmptyControl3"
        JustEmptyControl3.Size = New Size(10, 140)
        JustEmptyControl3.TabIndex = 5
        ' 
        ' ModernPanel5
        ' 
        ModernPanel5.BackColor1 = Color.FromArgb(CByte(120), CByte(0), CByte(0), CByte(0))
        ModernPanel5.BorderRadius = 10
        ModernPanel5.BorderSize = 0
        ModernPanel5.Controls.Add(RoundDashBoard2)
        ModernPanel5.Controls.Add(HtmlColorLabel2)
        ModernPanel5.Dock = DockStyle.Left
        ModernPanel5.Location = New Point(145, 0)
        ModernPanel5.Name = "ModernPanel5"
        ModernPanel5.ScrollBarMode = LakeUI.ModernPanel.ScrollMode.None
        ModernPanel5.Size = New Size(135, 140)
        ModernPanel5.TabIndex = 4
        ' 
        ' RoundDashBoard2
        ' 
        RoundDashBoard2.Dock = DockStyle.Fill
        RoundDashBoard2.FillColor = Color.CornflowerBlue
        RoundDashBoard2.FillGradientColor = Color.IndianRed
        RoundDashBoard2.Location = New Point(5, 41)
        RoundDashBoard2.Margin = New Padding(2, 2, 2, 2)
        RoundDashBoard2.Name = "RoundDashBoard2"
        RoundDashBoard2.Padding = New Padding(10, 10, 10, 15)
        RoundDashBoard2.Size = New Size(125, 94)
        RoundDashBoard2.StartAngle = 0F
        RoundDashBoard2.SweepAngle = 360F
        RoundDashBoard2.TabIndex = 1
        RoundDashBoard2.Thickness = 8F
        RoundDashBoard2.TrackColor = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        ' 
        ' HtmlColorLabel2
        ' 
        HtmlColorLabel2.AutoSize = True
        HtmlColorLabel2.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel2.Dock = DockStyle.Top
        HtmlColorLabel2.Location = New Point(5, 5)
        HtmlColorLabel2.Margin = New Padding(2)
        HtmlColorLabel2.Name = "HtmlColorLabel2"
        HtmlColorLabel2.Padding = New Padding(10, 10, 10, 5)
        HtmlColorLabel2.Size = New Size(125, 36)
        HtmlColorLabel2.TabIndex = 0
        HtmlColorLabel2.Text = "视频编码"
        HtmlColorLabel2.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.Center
        ' 
        ' JustEmptyControl2
        ' 
        JustEmptyControl2.Dock = DockStyle.Left
        JustEmptyControl2.Location = New Point(135, 0)
        JustEmptyControl2.Name = "JustEmptyControl2"
        JustEmptyControl2.Size = New Size(10, 140)
        JustEmptyControl2.TabIndex = 3
        ' 
        ' ModernPanel4
        ' 
        ModernPanel4.BackColor1 = Color.FromArgb(CByte(120), CByte(0), CByte(0), CByte(0))
        ModernPanel4.BorderRadius = 10
        ModernPanel4.BorderSize = 0
        ModernPanel4.Controls.Add(RoundDashBoard1)
        ModernPanel4.Controls.Add(HtmlColorLabel1)
        ModernPanel4.Dock = DockStyle.Left
        ModernPanel4.Location = New Point(0, 0)
        ModernPanel4.Name = "ModernPanel4"
        ModernPanel4.ScrollBarMode = LakeUI.ModernPanel.ScrollMode.None
        ModernPanel4.Size = New Size(135, 140)
        ModernPanel4.TabIndex = 0
        ' 
        ' RoundDashBoard1
        ' 
        RoundDashBoard1.Dock = DockStyle.Fill
        RoundDashBoard1.FillColor = Color.CornflowerBlue
        RoundDashBoard1.FillGradientColor = Color.IndianRed
        RoundDashBoard1.Location = New Point(5, 41)
        RoundDashBoard1.Margin = New Padding(2, 2, 2, 2)
        RoundDashBoard1.Name = "RoundDashBoard1"
        RoundDashBoard1.Padding = New Padding(10, 10, 10, 15)
        RoundDashBoard1.Size = New Size(125, 94)
        RoundDashBoard1.StartAngle = 0F
        RoundDashBoard1.SweepAngle = 360F
        RoundDashBoard1.TabIndex = 1
        RoundDashBoard1.Thickness = 8F
        RoundDashBoard1.TrackColor = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        ' 
        ' HtmlColorLabel1
        ' 
        HtmlColorLabel1.AutoSize = True
        HtmlColorLabel1.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel1.Dock = DockStyle.Top
        HtmlColorLabel1.Location = New Point(5, 5)
        HtmlColorLabel1.Margin = New Padding(2)
        HtmlColorLabel1.Name = "HtmlColorLabel1"
        HtmlColorLabel1.Padding = New Padding(10, 10, 10, 5)
        HtmlColorLabel1.Size = New Size(125, 36)
        HtmlColorLabel1.TabIndex = 0
        HtmlColorLabel1.Text = "视频解码"
        HtmlColorLabel1.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.Center
        ' 
        ' RamMonitor1
        ' 
        RamMonitor1.BottomTextAlign = ContentAlignment.MiddleCenter
        RamMonitor1.CellBackColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        RamMonitor1.Dock = DockStyle.Bottom
        RamMonitor1.Font = New Font("Microsoft YaHei UI", 9F)
        RamMonitor1.GraphBackColor = Color.FromArgb(CByte(120), CByte(0), CByte(0), CByte(0))
        RamMonitor1.Location = New Point(325, 437)
        RamMonitor1.Name = "RamMonitor1"
        RamMonitor1.Size = New Size(578, 185)
        RamMonitor1.TabIndex = 3
        RamMonitor1.TextPadding = New Padding(7)
        RamMonitor1.TopTextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Panel3
        ' 
        Panel3.Controls.Add(ModernComboBox3)
        Panel3.Controls.Add(JustEmptyControl4)
        Panel3.Controls.Add(ModernComboBox2)
        Panel3.Dock = DockStyle.Top
        Panel3.Location = New Point(0, 0)
        Panel3.Name = "Panel3"
        Panel3.Padding = New Padding(0, 0, 0, 10)
        Panel3.Size = New Size(578, 42)
        Panel3.TabIndex = 15
        ' 
        ' ModernComboBox3
        ' 
        ModernComboBox3.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernComboBox3.BorderColorFocus = Color.Silver
        ModernComboBox3.BorderRadius = 10
        ModernComboBox3.BorderSize = 0
        ModernComboBox3.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        ModernComboBox3.Dock = DockStyle.Fill
        ModernComboBox3.DropDownBorderSize = 2
        ModernComboBox3.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        ModernComboBox3.DropDownPadding = New Padding(10)
        ModernComboBox3.DropDownScrollBarHoverColor = Color.FromArgb(CByte(200), CByte(200), CByte(200))
        ModernComboBox3.Location = New Point(210, 0)
        ModernComboBox3.Margin = New Padding(2, 2, 2, 2)
        ModernComboBox3.Name = "ModernComboBox3"
        ModernComboBox3.Padding = New Padding(10, 0, 10, 0)
        ModernComboBox3.Size = New Size(368, 32)
        ModernComboBox3.TabIndex = 3
        ModernComboBox3.ToolTipBorderSize = 2
        ModernComboBox3.ToolTipGap = 10
        ModernComboBox3.ToolTipMaxWidth = 350
        ModernComboBox3.ToolTipPadding = New Padding(15)
        ModernComboBox3.WaterText = "选择显卡"
        ' 
        ' JustEmptyControl4
        ' 
        JustEmptyControl4.Dock = DockStyle.Left
        JustEmptyControl4.Location = New Point(200, 0)
        JustEmptyControl4.Name = "JustEmptyControl4"
        JustEmptyControl4.Size = New Size(10, 32)
        JustEmptyControl4.TabIndex = 2
        ' 
        ' ModernComboBox2
        ' 
        ModernComboBox2.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernComboBox2.BorderColorFocus = Color.Silver
        ModernComboBox2.BorderRadius = 10
        ModernComboBox2.BorderSize = 0
        ModernComboBox2.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        ModernComboBox2.Dock = DockStyle.Left
        ModernComboBox2.DropDownBorderSize = 2
        ModernComboBox2.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        ModernComboBox2.DropDownPadding = New Padding(10)
        ModernComboBox2.DropDownScrollBarHoverColor = Color.FromArgb(CByte(200), CByte(200), CByte(200))
        ModernComboBox2.Items.Add("LakeUI.GpuMonitor")
        ModernComboBox2.Items.Add("LibreHardwareMonitor")
        ToolTipEntry1.ItemText = "LakeUI.GpuMonitor"
        ToolTipEntry1.ToolTipText = "LakeUI 提供了 GpuMonitor 用于读取显卡数据，无需任何驱动，无需高级权限，即可直接读取各核心的占用以及显存信息，且性能优秀；缺点是仅对 NVIDIA 有较好支持，以及个别关键信息拿不到。"
        ToolTipEntry2.ItemText = "LibreHardwareMonitor"
        ToolTipEntry2.ToolTipText = "[需下载 LHM 组件] LHM 是 GitHub 上的开源项目，使用驱动对接各种硬件，监控覆盖全面，不过 3FUI 只用其获取显卡信息；缺点是要加载驱动，而且较为沉重。"
        ModernComboBox2.ItemToolTips.AddRange(New LakeUI.ModernComboBox.ToolTipEntry() {ToolTipEntry1, ToolTipEntry2})
        ModernComboBox2.Location = New Point(0, 0)
        ModernComboBox2.Margin = New Padding(2, 2, 2, 2)
        ModernComboBox2.Name = "ModernComboBox2"
        ModernComboBox2.Padding = New Padding(10, 0, 10, 0)
        ModernComboBox2.Size = New Size(200, 32)
        ModernComboBox2.TabIndex = 1
        ModernComboBox2.ToolTipBorderSize = 2
        ModernComboBox2.ToolTipGap = 10
        ModernComboBox2.ToolTipMaxWidth = 350
        ModernComboBox2.ToolTipPadding = New Padding(15)
        ModernComboBox2.WaterText = "选择显卡数据来源"
        ' 
        ' Timer1
        ' 
        Timer1.Interval = 1000
        ' 
        ' ModernPanel1
        ' 
        ModernPanel1.BackColor1 = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        ModernPanel1.BorderSize = 0
        ModernPanel1.Controls.Add(Panel4)
        ModernPanel1.Controls.Add(RamMonitor1)
        ModernPanel1.Controls.Add(JustEmptyControl7)
        ModernPanel1.Controls.Add(Panel2)
        ModernPanel1.Dock = DockStyle.Fill
        ModernPanel1.Location = New Point(0, 0)
        ModernPanel1.Name = "ModernPanel1"
        ModernPanel1.Padding = New Padding(10)
        ModernPanel1.ScrollBarMode = LakeUI.ModernPanel.ScrollMode.None
        ModernPanel1.Size = New Size(913, 632)
        ModernPanel1.TabIndex = 16
        ' 
        ' Panel4
        ' 
        Panel4.Controls.Add(ModernPanel内置显卡监控面板)
        Panel4.Controls.Add(Panel3)
        Panel4.Dock = DockStyle.Fill
        Panel4.Location = New Point(325, 10)
        Panel4.Name = "Panel4"
        Panel4.Padding = New Padding(0, 0, 0, 10)
        Panel4.Size = New Size(578, 427)
        Panel4.TabIndex = 4
        ' 
        ' JustEmptyControl7
        ' 
        JustEmptyControl7.Dock = DockStyle.Left
        JustEmptyControl7.Location = New Point(315, 10)
        JustEmptyControl7.Name = "JustEmptyControl7"
        JustEmptyControl7.Size = New Size(10, 612)
        JustEmptyControl7.TabIndex = 3
        ' 
        ' Panel2
        ' 
        Panel2.Controls.Add(CpuMonitor1)
        Panel2.Controls.Add(JustEmptyControl1)
        Panel2.Controls.Add(ModernComboBox1)
        Panel2.Dock = DockStyle.Left
        Panel2.Location = New Point(10, 10)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(305, 612)
        Panel2.TabIndex = 0
        ' 
        ' Form_v6_性能监控
        ' 
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        ClientSize = New Size(913, 632)
        Controls.Add(ModernPanel1)
        Font = New Font("Microsoft YaHei UI", 10F)
        ForeColor = Color.Silver
        Name = "Form_v6_性能监控"
        Text = "Form_v6_性能监控"
        ModernPanel内置显卡监控面板.ResumeLayout(False)
        Panel1.ResumeLayout(False)
        ModernPanel7.ResumeLayout(False)
        ModernPanel7.PerformLayout()
        ModernPanel6.ResumeLayout(False)
        ModernPanel6.PerformLayout()
        ModernPanel5.ResumeLayout(False)
        ModernPanel5.PerformLayout()
        ModernPanel4.ResumeLayout(False)
        ModernPanel4.PerformLayout()
        Panel3.ResumeLayout(False)
        ModernPanel1.ResumeLayout(False)
        Panel4.ResumeLayout(False)
        Panel2.ResumeLayout(False)
        ResumeLayout(False)
    End Sub
    Friend WithEvents ModernComboBox1 As LakeUI.ModernComboBox
    Friend WithEvents CpuMonitor1 As LakeUI.CpuMonitor
    Friend WithEvents JustEmptyControl1 As LakeUI.JustEmptyControl
    Friend WithEvents RamMonitor1 As LakeUI.RamMonitor
    Friend WithEvents ModernPanel内置显卡监控面板 As LakeUI.ModernPanel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents ModernComboBox3 As LakeUI.ModernComboBox
    Friend WithEvents JustEmptyControl4 As LakeUI.JustEmptyControl
    Friend WithEvents ModernComboBox2 As LakeUI.ModernComboBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ModernPanel4 As LakeUI.ModernPanel
    Friend WithEvents HtmlColorLabel1 As LakeUI.HtmlColorLabel
    Friend WithEvents RoundDashBoard1 As LakeUI.RoundDashBoard
    Friend WithEvents JustEmptyControl2 As LakeUI.JustEmptyControl
    Friend WithEvents ModernPanel6 As LakeUI.ModernPanel
    Friend WithEvents RoundDashBoard3 As LakeUI.RoundDashBoard
    Friend WithEvents HtmlColorLabel3 As LakeUI.HtmlColorLabel
    Friend WithEvents JustEmptyControl3 As LakeUI.JustEmptyControl
    Friend WithEvents ModernPanel5 As LakeUI.ModernPanel
    Friend WithEvents RoundDashBoard2 As LakeUI.RoundDashBoard
    Friend WithEvents HtmlColorLabel2 As LakeUI.HtmlColorLabel
    Friend WithEvents ModernPanel8 As LakeUI.ModernPanel
    Friend WithEvents JustEmptyControl5 As LakeUI.JustEmptyControl
    Friend WithEvents ModernPanel7 As LakeUI.ModernPanel
    Friend WithEvents JustEmptyControl6 As LakeUI.JustEmptyControl
    Friend WithEvents Timer1 As Timer
    Friend WithEvents RoundDashBoard4 As LakeUI.RoundDashBoard
    Friend WithEvents HtmlColorLabel4 As LakeUI.HtmlColorLabel
    Friend WithEvents ModernPanel1 As LakeUI.ModernPanel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents JustEmptyControl7 As LakeUI.JustEmptyControl
End Class
