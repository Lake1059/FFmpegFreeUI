<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_v6_集成工具_比特率可视化
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
        Dim ListColumn1 As LakeUI.UltraDetailListView.ListColumn = New LakeUI.UltraDetailListView.ListColumn()
        Dim ListColumn2 As LakeUI.UltraDetailListView.ListColumn = New LakeUI.UltraDetailListView.ListColumn()
        Dim ListColumn3 As LakeUI.UltraDetailListView.ListColumn = New LakeUI.UltraDetailListView.ListColumn()
        Dim ListColumn4 As LakeUI.UltraDetailListView.ListColumn = New LakeUI.UltraDetailListView.ListColumn()
        Dim ListColumn5 As LakeUI.UltraDetailListView.ListColumn = New LakeUI.UltraDetailListView.ListColumn()
        Dim ListColumn6 As LakeUI.UltraDetailListView.ListColumn = New LakeUI.UltraDetailListView.ListColumn()
        Dim ListColumn7 As LakeUI.UltraDetailListView.ListColumn = New LakeUI.UltraDetailListView.ListColumn()
        ModernPanel1 = New LakeUI.ModernPanel()
        Ultra2DChart1 = New LakeUI.Ultra2DChart()
        ModernPanel2 = New LakeUI.ModernPanel()
        MCB_选择图表精度 = New LakeUI.ModernComboBox()
        JustEmptyControl5 = New LakeUI.JustEmptyControl()
        MCB_选择查看类型 = New LakeUI.ModernComboBox()
        JustEmptyControl6 = New LakeUI.JustEmptyControl()
        MB_清除全部 = New LakeUI.ModernButton()
        JustEmptyControl7 = New LakeUI.JustEmptyControl()
        MB_清除统计 = New LakeUI.ModernButton()
        JustEmptyControl8 = New LakeUI.JustEmptyControl()
        MB_开始分析 = New LakeUI.ModernButton()
        UltraDetailListView1 = New LakeUI.UltraDetailListView()
        Panel1 = New LakeUI.ModernPanel()
        HtmlColorLabel2 = New LakeUI.HtmlColorLabel()
        MB_移除全部 = New LakeUI.ModernButton()
        JustEmptyControl4 = New LakeUI.JustEmptyControl()
        MB_移除选中 = New LakeUI.ModernButton()
        JustEmptyControl3 = New LakeUI.JustEmptyControl()
        MB_下移 = New LakeUI.ModernButton()
        JustEmptyControl2 = New LakeUI.JustEmptyControl()
        MB_上移 = New LakeUI.ModernButton()
        JustEmptyControl1 = New LakeUI.JustEmptyControl()
        MB_添加文件 = New LakeUI.ModernButton()
        HtmlColorLabel1 = New LakeUI.HtmlColorLabel()
        ModernPanel1.SuspendLayout()
        ModernPanel2.SuspendLayout()
        Panel1.SuspendLayout()
        SuspendLayout()
        ' 
        ' ModernPanel1
        ' 
        ModernPanel1.BackColor = Color.Transparent
        ModernPanel1.BackColor1 = Color.Transparent
        ModernPanel1.BorderSize = 0
        ModernPanel1.Controls.Add(Ultra2DChart1)
        ModernPanel1.Controls.Add(ModernPanel2)
        ModernPanel1.Controls.Add(UltraDetailListView1)
        ModernPanel1.Controls.Add(Panel1)
        ModernPanel1.Controls.Add(HtmlColorLabel1)
        ModernPanel1.Dock = DockStyle.Fill
        ModernPanel1.Location = New Point(0, 0)
        ModernPanel1.Name = "ModernPanel1"
        ModernPanel1.Padding = New Padding(20)
        ModernPanel1.Size = New Size(895, 617)
        ModernPanel1.TabIndex = 0
        ' 
        ' Ultra2DChart1
        ' 
        Ultra2DChart1.BackColor = Color.Transparent
        Ultra2DChart1.BackgroundSource = ModernPanel1
        Ultra2DChart1.Dock = DockStyle.Fill
        Ultra2DChart1.Font = New Font("Microsoft YaHei UI", 10F)
        Ultra2DChart1.GridLineColor = Color.FromArgb(CByte(42), CByte(42), CByte(42))
        Ultra2DChart1.Location = New Point(20, 267)
        Ultra2DChart1.Name = "Ultra2DChart1"
        Ultra2DChart1.Palette.AddRange(New Color() {Color.FromArgb(CByte(68), CByte(114), CByte(196)), Color.FromArgb(CByte(237), CByte(125), CByte(49)), Color.FromArgb(CByte(165), CByte(165), CByte(165)), Color.FromArgb(CByte(255), CByte(192), CByte(0)), Color.FromArgb(CByte(91), CByte(155), CByte(213)), Color.FromArgb(CByte(112), CByte(173), CByte(71)), Color.FromArgb(CByte(38), CByte(68), CByte(120)), Color.FromArgb(CByte(158), CByte(72), CByte(14)), Color.FromArgb(CByte(68), CByte(114), CByte(196)), Color.FromArgb(CByte(237), CByte(125), CByte(49)), Color.FromArgb(CByte(165), CByte(165), CByte(165)), Color.FromArgb(CByte(255), CByte(192), CByte(0)), Color.FromArgb(CByte(91), CByte(155), CByte(213)), Color.FromArgb(CByte(112), CByte(173), CByte(71)), Color.FromArgb(CByte(38), CByte(68), CByte(120)), Color.FromArgb(CByte(158), CByte(72), CByte(14)), Color.FromArgb(CByte(68), CByte(114), CByte(196)), Color.FromArgb(CByte(237), CByte(125), CByte(49)), Color.FromArgb(CByte(165), CByte(165), CByte(165)), Color.FromArgb(CByte(255), CByte(192), CByte(0)), Color.FromArgb(CByte(91), CByte(155), CByte(213)), Color.FromArgb(CByte(112), CByte(173), CByte(71)), Color.FromArgb(CByte(38), CByte(68), CByte(120)), Color.FromArgb(CByte(158), CByte(72), CByte(14)), Color.FromArgb(CByte(68), CByte(114), CByte(196)), Color.FromArgb(CByte(237), CByte(125), CByte(49)), Color.FromArgb(CByte(165), CByte(165), CByte(165)), Color.FromArgb(CByte(255), CByte(192), CByte(0)), Color.FromArgb(CByte(91), CByte(155), CByte(213)), Color.FromArgb(CByte(112), CByte(173), CByte(71)), Color.FromArgb(CByte(38), CByte(68), CByte(120)), Color.FromArgb(CByte(158), CByte(72), CByte(14)), Color.FromArgb(CByte(68), CByte(114), CByte(196)), Color.FromArgb(CByte(237), CByte(125), CByte(49)), Color.FromArgb(CByte(165), CByte(165), CByte(165)), Color.FromArgb(CByte(255), CByte(192), CByte(0)), Color.FromArgb(CByte(91), CByte(155), CByte(213)), Color.FromArgb(CByte(112), CByte(173), CByte(71)), Color.FromArgb(CByte(38), CByte(68), CByte(120)), Color.FromArgb(CByte(158), CByte(72), CByte(14)), Color.FromArgb(CByte(68), CByte(114), CByte(196)), Color.FromArgb(CByte(237), CByte(125), CByte(49)), Color.FromArgb(CByte(165), CByte(165), CByte(165)), Color.FromArgb(CByte(255), CByte(192), CByte(0)), Color.FromArgb(CByte(91), CByte(155), CByte(213)), Color.FromArgb(CByte(112), CByte(173), CByte(71)), Color.FromArgb(CByte(38), CByte(68), CByte(120)), Color.FromArgb(CByte(158), CByte(72), CByte(14)), Color.FromArgb(CByte(68), CByte(114), CByte(196)), Color.FromArgb(CByte(237), CByte(125), CByte(49)), Color.FromArgb(CByte(165), CByte(165), CByte(165)), Color.FromArgb(CByte(255), CByte(192), CByte(0)), Color.FromArgb(CByte(91), CByte(155), CByte(213)), Color.FromArgb(CByte(112), CByte(173), CByte(71)), Color.FromArgb(CByte(38), CByte(68), CByte(120)), Color.FromArgb(CByte(158), CByte(72), CByte(14)), Color.FromArgb(CByte(68), CByte(114), CByte(196)), Color.FromArgb(CByte(237), CByte(125), CByte(49)), Color.FromArgb(CByte(165), CByte(165), CByte(165)), Color.FromArgb(CByte(255), CByte(192), CByte(0)), Color.FromArgb(CByte(91), CByte(155), CByte(213)), Color.FromArgb(CByte(112), CByte(173), CByte(71)), Color.FromArgb(CByte(38), CByte(68), CByte(120)), Color.FromArgb(CByte(158), CByte(72), CByte(14)), Color.FromArgb(CByte(68), CByte(114), CByte(196)), Color.FromArgb(CByte(237), CByte(125), CByte(49)), Color.FromArgb(CByte(165), CByte(165), CByte(165)), Color.FromArgb(CByte(255), CByte(192), CByte(0)), Color.FromArgb(CByte(91), CByte(155), CByte(213)), Color.FromArgb(CByte(112), CByte(173), CByte(71)), Color.FromArgb(CByte(38), CByte(68), CByte(120)), Color.FromArgb(CByte(158), CByte(72), CByte(14)), Color.FromArgb(CByte(68), CByte(114), CByte(196)), Color.FromArgb(CByte(237), CByte(125), CByte(49)), Color.FromArgb(CByte(165), CByte(165), CByte(165)), Color.FromArgb(CByte(255), CByte(192), CByte(0)), Color.FromArgb(CByte(91), CByte(155), CByte(213)), Color.FromArgb(CByte(112), CByte(173), CByte(71)), Color.FromArgb(CByte(38), CByte(68), CByte(120)), Color.FromArgb(CByte(158), CByte(72), CByte(14))})
        Ultra2DChart1.PlotBackColor = Color.FromArgb(CByte(80), CByte(0), CByte(0), CByte(0))
        Ultra2DChart1.PlotBorderColor = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        Ultra2DChart1.ShowLineHoverCrosshair = True
        Ultra2DChart1.Size = New Size(855, 330)
        Ultra2DChart1.TabIndex = 13
        Ultra2DChart1.ValueLabelFormat = "0.###"
        Ultra2DChart1.YAxisLabelFormat = "0.###"
        ' 
        ' ModernPanel2
        ' 
        ModernPanel2.BackColor = Color.Transparent
        ModernPanel2.BackColor1 = Color.Transparent
        ModernPanel2.BorderSize = 0
        ModernPanel2.Controls.Add(MCB_选择图表精度)
        ModernPanel2.Controls.Add(JustEmptyControl5)
        ModernPanel2.Controls.Add(MCB_选择查看类型)
        ModernPanel2.Controls.Add(JustEmptyControl6)
        ModernPanel2.Controls.Add(MB_清除全部)
        ModernPanel2.Controls.Add(JustEmptyControl7)
        ModernPanel2.Controls.Add(MB_清除统计)
        ModernPanel2.Controls.Add(JustEmptyControl8)
        ModernPanel2.Controls.Add(MB_开始分析)
        ModernPanel2.Dock = DockStyle.Top
        ModernPanel2.Location = New Point(20, 215)
        ModernPanel2.Name = "ModernPanel2"
        ModernPanel2.Padding = New Padding(0, 10, 0, 10)
        ModernPanel2.Size = New Size(855, 52)
        ModernPanel2.TabIndex = 12
        ' 
        ' MCB_选择图表精度
        ' 
        MCB_选择图表精度.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_选择图表精度.BorderRadius = 10
        MCB_选择图表精度.BorderSize = 0
        MCB_选择图表精度.Dock = DockStyle.Left
        MCB_选择图表精度.DropDownBackdropBlurPasses = 2
        MCB_选择图表精度.DropDownBackdropBlurRadius = 30
        MCB_选择图表精度.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_选择图表精度.DropDownHoverColor = Color.FromArgb(CByte(20), CByte(220), CByte(220), CByte(220))
        MCB_选择图表精度.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_选择图表精度.DropDownPadding = New Padding(10)
        MCB_选择图表精度.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_选择图表精度.DropDownSelectedForeColor = Color.White
        MCB_选择图表精度.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_选择图表精度.Items.Add("每帧")
        MCB_选择图表精度.Items.Add("每秒")
        MCB_选择图表精度.Items.Add("每关键帧")
        MCB_选择图表精度.Location = New Point(540, 10)
        MCB_选择图表精度.Margin = New Padding(2, 2, 2, 2)
        MCB_选择图表精度.Name = "MCB_选择图表精度"
        MCB_选择图表精度.Padding = New Padding(10, 0, 10, 0)
        MCB_选择图表精度.PrefixForeColor = Color.Orange
        MCB_选择图表精度.PrefixGap = 10
        MCB_选择图表精度.PrefixText = "图表精度"
        MCB_选择图表精度.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_选择图表精度.Size = New Size(180, 32)
        MCB_选择图表精度.TabIndex = 16
        MCB_选择图表精度.ToolTipGap = -1
        MCB_选择图表精度.ToolTipMaxWidth = 350
        MCB_选择图表精度.ToolTipPadding = New Padding(15)
        MCB_选择图表精度.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' JustEmptyControl5
        ' 
        JustEmptyControl5.Dock = DockStyle.Left
        JustEmptyControl5.Location = New Point(530, 10)
        JustEmptyControl5.Name = "JustEmptyControl5"
        JustEmptyControl5.Size = New Size(10, 32)
        JustEmptyControl5.TabIndex = 15
        ' 
        ' MCB_选择查看类型
        ' 
        MCB_选择查看类型.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_选择查看类型.BorderRadius = 10
        MCB_选择查看类型.BorderSize = 0
        MCB_选择查看类型.Dock = DockStyle.Left
        MCB_选择查看类型.DropDownBackdropBlurPasses = 2
        MCB_选择查看类型.DropDownBackdropBlurRadius = 30
        MCB_选择查看类型.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_选择查看类型.DropDownHoverColor = Color.FromArgb(CByte(20), CByte(220), CByte(220), CByte(220))
        MCB_选择查看类型.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_选择查看类型.DropDownPadding = New Padding(10)
        MCB_选择查看类型.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_选择查看类型.DropDownSelectedForeColor = Color.White
        MCB_选择查看类型.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_选择查看类型.Items.Add("视频和音频")
        MCB_选择查看类型.Items.Add("仅视频")
        MCB_选择查看类型.Items.Add("仅音频")
        MCB_选择查看类型.Location = New Point(330, 10)
        MCB_选择查看类型.Margin = New Padding(2, 2, 2, 2)
        MCB_选择查看类型.Name = "MCB_选择查看类型"
        MCB_选择查看类型.Padding = New Padding(10, 0, 10, 0)
        MCB_选择查看类型.PrefixForeColor = Color.Orange
        MCB_选择查看类型.PrefixGap = 10
        MCB_选择查看类型.PrefixText = "查看类型"
        MCB_选择查看类型.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_选择查看类型.Size = New Size(200, 32)
        MCB_选择查看类型.TabIndex = 14
        MCB_选择查看类型.ToolTipGap = -1
        MCB_选择查看类型.ToolTipMaxWidth = 350
        MCB_选择查看类型.ToolTipPadding = New Padding(15)
        MCB_选择查看类型.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' JustEmptyControl6
        ' 
        JustEmptyControl6.Dock = DockStyle.Left
        JustEmptyControl6.Location = New Point(320, 10)
        JustEmptyControl6.Name = "JustEmptyControl6"
        JustEmptyControl6.Size = New Size(10, 32)
        JustEmptyControl6.TabIndex = 17
        ' 
        ' MB_清除全部
        ' 
        MB_清除全部.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_清除全部.BorderRadius = 10
        MB_清除全部.BorderSize = 0
        MB_清除全部.Dock = DockStyle.Left
        MB_清除全部.ForeColor = Color.IndianRed
        MB_清除全部.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_清除全部.Location = New Point(220, 10)
        MB_清除全部.Margin = New Padding(2)
        MB_清除全部.Name = "MB_清除全部"
        MB_清除全部.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_清除全部.Size = New Size(100, 32)
        MB_清除全部.TabIndex = 20
        MB_清除全部.Text = "清除全部"
        ' 
        ' JustEmptyControl7
        ' 
        JustEmptyControl7.Dock = DockStyle.Left
        JustEmptyControl7.Location = New Point(210, 10)
        JustEmptyControl7.Name = "JustEmptyControl7"
        JustEmptyControl7.Size = New Size(10, 32)
        JustEmptyControl7.TabIndex = 19
        ' 
        ' MB_清除统计
        ' 
        MB_清除统计.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_清除统计.BorderRadius = 10
        MB_清除统计.BorderSize = 0
        MB_清除统计.Dock = DockStyle.Left
        MB_清除统计.ForeColor = Color.IndianRed
        MB_清除统计.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_清除统计.Location = New Point(110, 10)
        MB_清除统计.Margin = New Padding(2)
        MB_清除统计.Name = "MB_清除统计"
        MB_清除统计.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_清除统计.Size = New Size(100, 32)
        MB_清除统计.TabIndex = 18
        MB_清除统计.Text = "清除统计"
        ' 
        ' JustEmptyControl8
        ' 
        JustEmptyControl8.Dock = DockStyle.Left
        JustEmptyControl8.Location = New Point(100, 10)
        JustEmptyControl8.Name = "JustEmptyControl8"
        JustEmptyControl8.Size = New Size(10, 32)
        JustEmptyControl8.TabIndex = 2
        ' 
        ' MB_开始分析
        ' 
        MB_开始分析.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_开始分析.BorderRadius = 10
        MB_开始分析.BorderSize = 0
        MB_开始分析.Dock = DockStyle.Left
        MB_开始分析.ForeColor = Color.YellowGreen
        MB_开始分析.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_开始分析.Location = New Point(0, 10)
        MB_开始分析.Margin = New Padding(2)
        MB_开始分析.Name = "MB_开始分析"
        MB_开始分析.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_开始分析.Size = New Size(100, 32)
        MB_开始分析.TabIndex = 0
        MB_开始分析.Text = "开始分析"
        ' 
        ' UltraDetailListView1
        ' 
        UltraDetailListView1.AllowDragReorder = True
        UltraDetailListView1.BackgroundColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        UltraDetailListView1.BorderRadius = 10
        UltraDetailListView1.BorderSize = 0
        ListColumn1.Text = "文件"
        ListColumn1.Width = 200
        ListColumn2.Text = "选择流"
        ListColumn2.Width = 80
        ListColumn3.Text = "时长"
        ListColumn4.Text = "总帧数"
        ListColumn5.Text = "平均码率"
        ListColumn6.Text = "最低码率"
        ListColumn7.Text = "最高码率"
        UltraDetailListView1.Columns.Add(ListColumn1)
        UltraDetailListView1.Columns.Add(ListColumn2)
        UltraDetailListView1.Columns.Add(ListColumn3)
        UltraDetailListView1.Columns.Add(ListColumn4)
        UltraDetailListView1.Columns.Add(ListColumn5)
        UltraDetailListView1.Columns.Add(ListColumn6)
        UltraDetailListView1.Columns.Add(ListColumn7)
        UltraDetailListView1.Dock = DockStyle.Top
        UltraDetailListView1.DragSelectZoneRatio = 0.5F
        UltraDetailListView1.GroupBorderColor = Color.Silver
        UltraDetailListView1.GroupHeight = 35
        UltraDetailListView1.HeaderBackColor = Color.Transparent
        UltraDetailListView1.HeaderBorderColor = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        UltraDetailListView1.HeaderHeight = 36
        UltraDetailListView1.ItemCornerRadius = 10
        UltraDetailListView1.ItemPadding = New Padding(10, 5, 10, 5)
        UltraDetailListView1.ItemSelectedBackColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        UltraDetailListView1.Location = New Point(20, 95)
        UltraDetailListView1.Margin = New Padding(2, 2, 2, 2)
        UltraDetailListView1.Name = "UltraDetailListView1"
        UltraDetailListView1.Padding = New Padding(10, 0, 10, 10)
        UltraDetailListView1.ScrollBarThumbColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        UltraDetailListView1.ScrollBarThumbHoverColor = Color.FromArgb(CByte(120), CByte(220), CByte(220), CByte(220))
        UltraDetailListView1.ScrollBarTrackColor = Color.FromArgb(CByte(20), CByte(220), CByte(220), CByte(220))
        UltraDetailListView1.SelectionRectBorderColor = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        UltraDetailListView1.SelectionRectFillColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        UltraDetailListView1.Size = New Size(855, 120)
        UltraDetailListView1.TabIndex = 11
        ' 
        ' Panel1
        ' 
        Panel1.BackColor = Color.Transparent
        Panel1.BackColor1 = Color.Transparent
        Panel1.BorderSize = 0
        Panel1.Controls.Add(HtmlColorLabel2)
        Panel1.Controls.Add(MB_移除全部)
        Panel1.Controls.Add(JustEmptyControl4)
        Panel1.Controls.Add(MB_移除选中)
        Panel1.Controls.Add(JustEmptyControl3)
        Panel1.Controls.Add(MB_下移)
        Panel1.Controls.Add(JustEmptyControl2)
        Panel1.Controls.Add(MB_上移)
        Panel1.Controls.Add(JustEmptyControl1)
        Panel1.Controls.Add(MB_添加文件)
        Panel1.Dock = DockStyle.Top
        Panel1.Location = New Point(20, 43)
        Panel1.Name = "Panel1"
        Panel1.Padding = New Padding(0, 10, 0, 10)
        Panel1.Size = New Size(855, 52)
        Panel1.TabIndex = 10
        ' 
        ' HtmlColorLabel2
        ' 
        HtmlColorLabel2.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel2.Dock = DockStyle.Fill
        HtmlColorLabel2.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HtmlColorLabel2.Location = New Point(460, 10)
        HtmlColorLabel2.Margin = New Padding(2)
        HtmlColorLabel2.Name = "HtmlColorLabel2"
        HtmlColorLabel2.Padding = New Padding(10, 0, 0, 0)
        HtmlColorLabel2.Size = New Size(395, 32)
        HtmlColorLabel2.TabIndex = 10
        HtmlColorLabel2.Text = "操作同理，双击使用可视化选择器"
        HtmlColorLabel2.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.MiddleLeft
        ' 
        ' MB_移除全部
        ' 
        MB_移除全部.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_移除全部.BorderRadius = 10
        MB_移除全部.BorderSize = 0
        MB_移除全部.Dock = DockStyle.Left
        MB_移除全部.ForeColor = Color.IndianRed
        MB_移除全部.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_移除全部.Location = New Point(360, 10)
        MB_移除全部.Margin = New Padding(2)
        MB_移除全部.Name = "MB_移除全部"
        MB_移除全部.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_移除全部.Size = New Size(100, 32)
        MB_移除全部.TabIndex = 9
        MB_移除全部.Text = "移除全部"
        ' 
        ' JustEmptyControl4
        ' 
        JustEmptyControl4.Dock = DockStyle.Left
        JustEmptyControl4.Location = New Point(350, 10)
        JustEmptyControl4.Name = "JustEmptyControl4"
        JustEmptyControl4.Size = New Size(10, 32)
        JustEmptyControl4.TabIndex = 8
        ' 
        ' MB_移除选中
        ' 
        MB_移除选中.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_移除选中.BorderRadius = 10
        MB_移除选中.BorderSize = 0
        MB_移除选中.Dock = DockStyle.Left
        MB_移除选中.ForeColor = Color.IndianRed
        MB_移除选中.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_移除选中.Location = New Point(250, 10)
        MB_移除选中.Margin = New Padding(2)
        MB_移除选中.Name = "MB_移除选中"
        MB_移除选中.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_移除选中.Size = New Size(100, 32)
        MB_移除选中.TabIndex = 7
        MB_移除选中.Text = "移除选中"
        ' 
        ' JustEmptyControl3
        ' 
        JustEmptyControl3.Dock = DockStyle.Left
        JustEmptyControl3.Location = New Point(240, 10)
        JustEmptyControl3.Name = "JustEmptyControl3"
        JustEmptyControl3.Size = New Size(10, 32)
        JustEmptyControl3.TabIndex = 6
        ' 
        ' MB_下移
        ' 
        MB_下移.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_下移.BorderRadius = 10
        MB_下移.BorderSize = 0
        MB_下移.Dock = DockStyle.Left
        MB_下移.ForeColor = Color.CornflowerBlue
        MB_下移.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_下移.Location = New Point(180, 10)
        MB_下移.Margin = New Padding(2)
        MB_下移.Name = "MB_下移"
        MB_下移.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_下移.Size = New Size(60, 32)
        MB_下移.TabIndex = 5
        MB_下移.Text = "下移"
        ' 
        ' JustEmptyControl2
        ' 
        JustEmptyControl2.Dock = DockStyle.Left
        JustEmptyControl2.Location = New Point(170, 10)
        JustEmptyControl2.Name = "JustEmptyControl2"
        JustEmptyControl2.Size = New Size(10, 32)
        JustEmptyControl2.TabIndex = 4
        ' 
        ' MB_上移
        ' 
        MB_上移.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_上移.BorderRadius = 10
        MB_上移.BorderSize = 0
        MB_上移.Dock = DockStyle.Left
        MB_上移.ForeColor = Color.CornflowerBlue
        MB_上移.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_上移.Location = New Point(110, 10)
        MB_上移.Margin = New Padding(2)
        MB_上移.Name = "MB_上移"
        MB_上移.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_上移.Size = New Size(60, 32)
        MB_上移.TabIndex = 3
        MB_上移.Text = "上移"
        ' 
        ' JustEmptyControl1
        ' 
        JustEmptyControl1.Dock = DockStyle.Left
        JustEmptyControl1.Location = New Point(100, 10)
        JustEmptyControl1.Name = "JustEmptyControl1"
        JustEmptyControl1.Size = New Size(10, 32)
        JustEmptyControl1.TabIndex = 2
        ' 
        ' MB_添加文件
        ' 
        MB_添加文件.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_添加文件.BorderRadius = 10
        MB_添加文件.BorderSize = 0
        MB_添加文件.Dock = DockStyle.Left
        MB_添加文件.ForeColor = Color.YellowGreen
        MB_添加文件.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_添加文件.Location = New Point(0, 10)
        MB_添加文件.Margin = New Padding(2)
        MB_添加文件.Name = "MB_添加文件"
        MB_添加文件.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_添加文件.Size = New Size(100, 32)
        MB_添加文件.TabIndex = 0
        MB_添加文件.Text = "添加文件"
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
        HtmlColorLabel1.Size = New Size(855, 23)
        HtmlColorLabel1.TabIndex = 9
        HtmlColorLabel1.Text = "<span style=""font-size:13; color:Silver"">比特率可视化</span>   分析真实比特率花费"
        ' 
        ' Form_v6_集成工具_比特率可视化
        ' 
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ClientSize = New Size(895, 617)
        Controls.Add(ModernPanel1)
        Font = New Font("Microsoft YaHei UI", 10F)
        ForeColor = Color.Silver
        Name = "Form_v6_集成工具_比特率可视化"
        StartPosition = FormStartPosition.Manual
        Text = "Form_v6_集成工具_比特率可视化"
        ModernPanel1.ResumeLayout(False)
        ModernPanel1.PerformLayout()
        ModernPanel2.ResumeLayout(False)
        Panel1.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents ModernPanel1 As LakeUI.ModernPanel
    Friend WithEvents UltraDetailListView1 As LakeUI.UltraDetailListView
    Friend WithEvents Panel1 As LakeUI.ModernPanel
    Friend WithEvents HtmlColorLabel2 As LakeUI.HtmlColorLabel
    Friend WithEvents MB_移除全部 As LakeUI.ModernButton
    Friend WithEvents JustEmptyControl4 As LakeUI.JustEmptyControl
    Friend WithEvents MB_移除选中 As LakeUI.ModernButton
    Friend WithEvents JustEmptyControl3 As LakeUI.JustEmptyControl
    Friend WithEvents MB_下移 As LakeUI.ModernButton
    Friend WithEvents JustEmptyControl2 As LakeUI.JustEmptyControl
    Friend WithEvents MB_上移 As LakeUI.ModernButton
    Friend WithEvents JustEmptyControl1 As LakeUI.JustEmptyControl
    Friend WithEvents MB_添加文件 As LakeUI.ModernButton
    Friend WithEvents HtmlColorLabel1 As LakeUI.HtmlColorLabel
    Friend WithEvents ModernPanel2 As LakeUI.ModernPanel
    Friend WithEvents JustEmptyControl8 As LakeUI.JustEmptyControl
    Friend WithEvents MB_开始分析 As LakeUI.ModernButton
    Friend WithEvents MCB_选择查看类型 As LakeUI.ModernComboBox
    Friend WithEvents MCB_选择图表精度 As LakeUI.ModernComboBox
    Friend WithEvents JustEmptyControl5 As LakeUI.JustEmptyControl
    Friend WithEvents Ultra2DChart1 As LakeUI.Ultra2DChart
    Friend WithEvents JustEmptyControl6 As LakeUI.JustEmptyControl
    Friend WithEvents MB_清除统计 As LakeUI.ModernButton
    Friend WithEvents MB_清除全部 As LakeUI.ModernButton
    Friend WithEvents JustEmptyControl7 As LakeUI.JustEmptyControl
End Class
