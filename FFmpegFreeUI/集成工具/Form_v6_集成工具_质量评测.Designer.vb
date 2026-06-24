<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_v6_集成工具_质量评测
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
        Dim ListColumn6 As LakeUI.UltraDetailListView.ListColumn = New LakeUI.UltraDetailListView.ListColumn()
        Dim ListColumn7 As LakeUI.UltraDetailListView.ListColumn = New LakeUI.UltraDetailListView.ListColumn()
        Dim ListColumn8 As LakeUI.UltraDetailListView.ListColumn = New LakeUI.UltraDetailListView.ListColumn()
        Dim ListColumn9 As LakeUI.UltraDetailListView.ListColumn = New LakeUI.UltraDetailListView.ListColumn()
        Dim ListColumn10 As LakeUI.UltraDetailListView.ListColumn = New LakeUI.UltraDetailListView.ListColumn()
        ModernPanel1 = New LakeUI.ModernPanel()
        UltraDetailListView1 = New LakeUI.UltraDetailListView()
        Panel6 = New Panel()
        MB_移除全部文件 = New LakeUI.ModernButton()
        JustEmptyControl7 = New LakeUI.JustEmptyControl()
        MB_图表窗口 = New LakeUI.ModernButton()
        MB_移除选中文件 = New LakeUI.ModernButton()
        JustEmptyControl4 = New LakeUI.JustEmptyControl()
        MB_清除全部记录 = New LakeUI.ModernButton()
        JustEmptyControl3 = New LakeUI.JustEmptyControl()
        MB_清除选中记录 = New LakeUI.ModernButton()
        JustEmptyControl6 = New LakeUI.JustEmptyControl()
        MB_开始评测 = New LakeUI.ModernButton()
        HtmlColorLabel8 = New LakeUI.HtmlColorLabel()
        Panel4 = New Panel()
        MCB_SubSample = New LakeUI.ModernComboBox()
        JustEmptyControl2 = New LakeUI.JustEmptyControl()
        MCB_Pooling = New LakeUI.ModernComboBox()
        JustEmptyControl1 = New LakeUI.JustEmptyControl()
        MCB_模型选择 = New LakeUI.ModernComboBox()
        Panel5 = New Panel()
        HtmlColorLabel7 = New LakeUI.HtmlColorLabel()
        HtmlColorLabel6 = New LakeUI.HtmlColorLabel()
        HtmlColorLabel9 = New LakeUI.HtmlColorLabel()
        Panel3 = New Panel()
        MCB_XPSNR = New LakeUI.ModernCheckBox()
        MCB_VMAF = New LakeUI.ModernCheckBox()
        MCB_SSIM = New LakeUI.ModernCheckBox()
        MCB_PSNR = New LakeUI.ModernCheckBox()
        HtmlColorLabel4 = New LakeUI.HtmlColorLabel()
        Panel2 = New Panel()
        MTB_从头开始 = New LakeUI.ModernTextBox()
        HtmlColorLabel2 = New LakeUI.HtmlColorLabel()
        MTB_评测时长 = New LakeUI.ModernTextBox()
        HtmlColorLabel3 = New LakeUI.HtmlColorLabel()
        Panel1 = New Panel()
        MTB_原视频文件路径 = New LakeUI.ModernTextBox()
        JustEmptyControl5 = New LakeUI.JustEmptyControl()
        ModernButton5 = New LakeUI.ModernButton()
        HtmlColorLabel1 = New LakeUI.HtmlColorLabel()
        MB_导出记录 = New LakeUI.ModernButton()
        JustEmptyControl8 = New LakeUI.JustEmptyControl()
        JustEmptyControl9 = New LakeUI.JustEmptyControl()
        ModernPanel1.SuspendLayout()
        Panel6.SuspendLayout()
        Panel4.SuspendLayout()
        Panel5.SuspendLayout()
        Panel3.SuspendLayout()
        Panel2.SuspendLayout()
        Panel1.SuspendLayout()
        SuspendLayout()
        ' 
        ' ModernPanel1
        ' 
        ModernPanel1.BackColor1 = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ModernPanel1.BorderSize = 0
        ModernPanel1.Controls.Add(UltraDetailListView1)
        ModernPanel1.Controls.Add(Panel6)
        ModernPanel1.Controls.Add(HtmlColorLabel8)
        ModernPanel1.Controls.Add(Panel4)
        ModernPanel1.Controls.Add(Panel5)
        ModernPanel1.Controls.Add(Panel3)
        ModernPanel1.Controls.Add(Panel2)
        ModernPanel1.Controls.Add(Panel1)
        ModernPanel1.Controls.Add(HtmlColorLabel1)
        ModernPanel1.Dock = DockStyle.Fill
        ModernPanel1.Location = New Point(0, 0)
        ModernPanel1.Name = "ModernPanel1"
        ModernPanel1.Padding = New Padding(20)
        ModernPanel1.Size = New Size(817, 603)
        ModernPanel1.TabIndex = 0
        ' 
        ' UltraDetailListView1
        ' 
        UltraDetailListView1.AllowDragReorder = True
        UltraDetailListView1.BackgroundColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        UltraDetailListView1.BorderRadius = 10
        UltraDetailListView1.BorderSize = 0
        ListColumn6.Text = "文件"
        ListColumn6.Width = 350
        ListColumn7.Text = "PSNR"
        ListColumn7.Width = 80
        ListColumn8.Text = "SSIM"
        ListColumn8.Width = 80
        ListColumn9.Text = "VMAF"
        ListColumn9.Width = 80
        ListColumn10.Text = "XPSNR"
        ListColumn10.Width = 80
        UltraDetailListView1.Columns.Add(ListColumn6)
        UltraDetailListView1.Columns.Add(ListColumn7)
        UltraDetailListView1.Columns.Add(ListColumn8)
        UltraDetailListView1.Columns.Add(ListColumn9)
        UltraDetailListView1.Columns.Add(ListColumn10)
        UltraDetailListView1.Dock = DockStyle.Fill
        UltraDetailListView1.DragSelectZoneWidth = 200
        UltraDetailListView1.GroupBorderColor = Color.Silver
        UltraDetailListView1.GroupHeight = 35
        UltraDetailListView1.HeaderBackColor = Color.Transparent
        UltraDetailListView1.HeaderBorderColor = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        UltraDetailListView1.ItemCornerRadius = 10
        UltraDetailListView1.ItemPadding = New Padding(10, 5, 10, 5)
        UltraDetailListView1.ItemSelectedBackColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        UltraDetailListView1.Location = New Point(20, 298)
        UltraDetailListView1.Margin = New Padding(2, 2, 2, 2)
        UltraDetailListView1.Name = "UltraDetailListView1"
        UltraDetailListView1.Padding = New Padding(5, 0, 5, 5)
        UltraDetailListView1.ScrollBarThumbColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        UltraDetailListView1.ScrollBarThumbHoverColor = Color.FromArgb(CByte(120), CByte(220), CByte(220), CByte(220))
        UltraDetailListView1.ScrollBarTrackColor = Color.FromArgb(CByte(20), CByte(220), CByte(220), CByte(220))
        UltraDetailListView1.SelectionRectBorderColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        UltraDetailListView1.SelectionRectFillColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        UltraDetailListView1.Size = New Size(777, 230)
        UltraDetailListView1.TabIndex = 13
        ' 
        ' Panel6
        ' 
        Panel6.Controls.Add(MB_导出记录)
        Panel6.Controls.Add(JustEmptyControl9)
        Panel6.Controls.Add(JustEmptyControl8)
        Panel6.Controls.Add(MB_移除全部文件)
        Panel6.Controls.Add(JustEmptyControl7)
        Panel6.Controls.Add(MB_图表窗口)
        Panel6.Controls.Add(MB_移除选中文件)
        Panel6.Controls.Add(JustEmptyControl4)
        Panel6.Controls.Add(MB_清除全部记录)
        Panel6.Controls.Add(JustEmptyControl3)
        Panel6.Controls.Add(MB_清除选中记录)
        Panel6.Controls.Add(JustEmptyControl6)
        Panel6.Controls.Add(MB_开始评测)
        Panel6.Dock = DockStyle.Bottom
        Panel6.Location = New Point(20, 528)
        Panel6.Name = "Panel6"
        Panel6.Padding = New Padding(0, 20, 0, 0)
        Panel6.Size = New Size(777, 55)
        Panel6.TabIndex = 12
        ' 
        ' MB_移除全部文件
        ' 
        MB_移除全部文件.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_移除全部文件.BorderRadius = 10
        MB_移除全部文件.BorderSize = 0
        MB_移除全部文件.Dock = DockStyle.Left
        MB_移除全部文件.ForeColor = Color.IndianRed
        MB_移除全部文件.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_移除全部文件.Location = New Point(440, 20)
        MB_移除全部文件.Margin = New Padding(2)
        MB_移除全部文件.Name = "MB_移除全部文件"
        MB_移除全部文件.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_移除全部文件.Size = New Size(100, 35)
        MB_移除全部文件.TabIndex = 14
        MB_移除全部文件.Text = "移除全部"
        ' 
        ' JustEmptyControl7
        ' 
        JustEmptyControl7.Dock = DockStyle.Left
        JustEmptyControl7.Location = New Point(430, 20)
        JustEmptyControl7.Name = "JustEmptyControl7"
        JustEmptyControl7.Size = New Size(10, 35)
        JustEmptyControl7.TabIndex = 13
        ' 
        ' MB_图表窗口
        ' 
        MB_图表窗口.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_图表窗口.BorderRadius = 10
        MB_图表窗口.BorderSize = 0
        MB_图表窗口.Dock = DockStyle.Right
        MB_图表窗口.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_图表窗口.Location = New Point(677, 20)
        MB_图表窗口.Margin = New Padding(2)
        MB_图表窗口.Name = "MB_图表窗口"
        MB_图表窗口.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_图表窗口.Size = New Size(100, 35)
        MB_图表窗口.TabIndex = 12
        MB_图表窗口.Text = "图表窗口"
        ' 
        ' MB_移除选中文件
        ' 
        MB_移除选中文件.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_移除选中文件.BorderRadius = 10
        MB_移除选中文件.BorderSize = 0
        MB_移除选中文件.Dock = DockStyle.Left
        MB_移除选中文件.ForeColor = Color.IndianRed
        MB_移除选中文件.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_移除选中文件.Location = New Point(330, 20)
        MB_移除选中文件.Margin = New Padding(2)
        MB_移除选中文件.Name = "MB_移除选中文件"
        MB_移除选中文件.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_移除选中文件.Size = New Size(100, 35)
        MB_移除选中文件.TabIndex = 10
        MB_移除选中文件.Text = "移除文件"
        ' 
        ' JustEmptyControl4
        ' 
        JustEmptyControl4.Dock = DockStyle.Left
        JustEmptyControl4.Location = New Point(320, 20)
        JustEmptyControl4.Name = "JustEmptyControl4"
        JustEmptyControl4.Size = New Size(10, 35)
        JustEmptyControl4.TabIndex = 9
        ' 
        ' MB_清除全部记录
        ' 
        MB_清除全部记录.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_清除全部记录.BorderRadius = 10
        MB_清除全部记录.BorderSize = 0
        MB_清除全部记录.Dock = DockStyle.Left
        MB_清除全部记录.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_清除全部记录.Location = New Point(220, 20)
        MB_清除全部记录.Margin = New Padding(2)
        MB_清除全部记录.Name = "MB_清除全部记录"
        MB_清除全部记录.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_清除全部记录.Size = New Size(100, 35)
        MB_清除全部记录.TabIndex = 8
        MB_清除全部记录.Text = "清除全部"
        ' 
        ' JustEmptyControl3
        ' 
        JustEmptyControl3.Dock = DockStyle.Left
        JustEmptyControl3.Location = New Point(210, 20)
        JustEmptyControl3.Name = "JustEmptyControl3"
        JustEmptyControl3.Size = New Size(10, 35)
        JustEmptyControl3.TabIndex = 7
        ' 
        ' MB_清除选中记录
        ' 
        MB_清除选中记录.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_清除选中记录.BorderRadius = 10
        MB_清除选中记录.BorderSize = 0
        MB_清除选中记录.Dock = DockStyle.Left
        MB_清除选中记录.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_清除选中记录.Location = New Point(110, 20)
        MB_清除选中记录.Margin = New Padding(2)
        MB_清除选中记录.Name = "MB_清除选中记录"
        MB_清除选中记录.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_清除选中记录.Size = New Size(100, 35)
        MB_清除选中记录.TabIndex = 6
        MB_清除选中记录.Text = "清除记录"
        ' 
        ' JustEmptyControl6
        ' 
        JustEmptyControl6.Dock = DockStyle.Left
        JustEmptyControl6.Location = New Point(100, 20)
        JustEmptyControl6.Name = "JustEmptyControl6"
        JustEmptyControl6.Size = New Size(10, 35)
        JustEmptyControl6.TabIndex = 2
        ' 
        ' MB_开始评测
        ' 
        MB_开始评测.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_开始评测.BorderRadius = 10
        MB_开始评测.BorderSize = 0
        MB_开始评测.Dock = DockStyle.Left
        MB_开始评测.ForeColor = Color.YellowGreen
        MB_开始评测.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_开始评测.Location = New Point(0, 20)
        MB_开始评测.Margin = New Padding(2)
        MB_开始评测.Name = "MB_开始评测"
        MB_开始评测.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_开始评测.Size = New Size(100, 35)
        MB_开始评测.TabIndex = 0
        MB_开始评测.Text = "开始评测"
        ' 
        ' HtmlColorLabel8
        ' 
        HtmlColorLabel8.AutoSize = True
        HtmlColorLabel8.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel8.Dock = DockStyle.Top
        HtmlColorLabel8.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HtmlColorLabel8.Location = New Point(20, 243)
        HtmlColorLabel8.Margin = New Padding(2)
        HtmlColorLabel8.Name = "HtmlColorLabel8"
        HtmlColorLabel8.Padding = New Padding(0, 20, 0, 10)
        HtmlColorLabel8.Size = New Size(777, 55)
        HtmlColorLabel8.TabIndex = 11
        HtmlColorLabel8.Text = "<span style=""font-size:13; color:Silver"">对比文件列表</span>   这里放编码之后的文件，原文件放最顶上那个文本框"
        ' 
        ' Panel4
        ' 
        Panel4.Controls.Add(MCB_SubSample)
        Panel4.Controls.Add(JustEmptyControl2)
        Panel4.Controls.Add(MCB_Pooling)
        Panel4.Controls.Add(JustEmptyControl1)
        Panel4.Controls.Add(MCB_模型选择)
        Panel4.Dock = DockStyle.Top
        Panel4.Location = New Point(20, 201)
        Panel4.Name = "Panel4"
        Panel4.Padding = New Padding(0, 10, 0, 0)
        Panel4.Size = New Size(777, 42)
        Panel4.TabIndex = 9
        ' 
        ' MCB_SubSample
        ' 
        MCB_SubSample.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_SubSample.BorderRadius = 10
        MCB_SubSample.BorderSize = 0
        MCB_SubSample.Dock = DockStyle.Left
        MCB_SubSample.DropDownBackdropBlurPasses = 2
        MCB_SubSample.DropDownBackdropBlurRadius = 30
        MCB_SubSample.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_SubSample.DropDownHoverAnimationDuration = 0
        MCB_SubSample.DropDownHoverColor = Color.FromArgb(CByte(80), CByte(0), CByte(0), CByte(0))
        MCB_SubSample.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_SubSample.DropDownPadding = New Padding(10)
        MCB_SubSample.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_SubSample.DropDownSelectedForeColor = Color.White
        MCB_SubSample.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_SubSample.Location = New Point(370, 10)
        MCB_SubSample.Margin = New Padding(2, 2, 2, 2)
        MCB_SubSample.Name = "MCB_SubSample"
        MCB_SubSample.Padding = New Padding(10, 0, 10, 0)
        MCB_SubSample.Size = New Size(150, 32)
        MCB_SubSample.TabIndex = 20
        MCB_SubSample.ToolTipGap = -1
        MCB_SubSample.ToolTipMaxWidth = 350
        MCB_SubSample.ToolTipPadding = New Padding(15)
        MCB_SubSample.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' JustEmptyControl2
        ' 
        JustEmptyControl2.Dock = DockStyle.Left
        JustEmptyControl2.Location = New Point(360, 10)
        JustEmptyControl2.Name = "JustEmptyControl2"
        JustEmptyControl2.Size = New Size(10, 32)
        JustEmptyControl2.TabIndex = 19
        ' 
        ' MCB_Pooling
        ' 
        MCB_Pooling.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_Pooling.BorderRadius = 10
        MCB_Pooling.BorderSize = 0
        MCB_Pooling.Dock = DockStyle.Left
        MCB_Pooling.DropDownBackdropBlurPasses = 2
        MCB_Pooling.DropDownBackdropBlurRadius = 30
        MCB_Pooling.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_Pooling.DropDownHoverAnimationDuration = 0
        MCB_Pooling.DropDownHoverColor = Color.FromArgb(CByte(80), CByte(0), CByte(0), CByte(0))
        MCB_Pooling.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_Pooling.DropDownPadding = New Padding(10)
        MCB_Pooling.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_Pooling.DropDownSelectedForeColor = Color.White
        MCB_Pooling.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_Pooling.Location = New Point(210, 10)
        MCB_Pooling.Margin = New Padding(2, 2, 2, 2)
        MCB_Pooling.Name = "MCB_Pooling"
        MCB_Pooling.Padding = New Padding(10, 0, 10, 0)
        MCB_Pooling.Size = New Size(150, 32)
        MCB_Pooling.TabIndex = 17
        MCB_Pooling.ToolTipGap = -1
        MCB_Pooling.ToolTipMaxWidth = 350
        MCB_Pooling.ToolTipPadding = New Padding(15)
        MCB_Pooling.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' JustEmptyControl1
        ' 
        JustEmptyControl1.Dock = DockStyle.Left
        JustEmptyControl1.Location = New Point(200, 10)
        JustEmptyControl1.Name = "JustEmptyControl1"
        JustEmptyControl1.Size = New Size(10, 32)
        JustEmptyControl1.TabIndex = 18
        ' 
        ' MCB_模型选择
        ' 
        MCB_模型选择.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_模型选择.BorderRadius = 10
        MCB_模型选择.BorderSize = 0
        MCB_模型选择.Dock = DockStyle.Left
        MCB_模型选择.DropDownBackdropBlurPasses = 2
        MCB_模型选择.DropDownBackdropBlurRadius = 30
        MCB_模型选择.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_模型选择.DropDownHoverAnimationDuration = 0
        MCB_模型选择.DropDownHoverColor = Color.FromArgb(CByte(80), CByte(0), CByte(0), CByte(0))
        MCB_模型选择.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_模型选择.DropDownPadding = New Padding(10)
        MCB_模型选择.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_模型选择.DropDownSelectedForeColor = Color.White
        MCB_模型选择.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_模型选择.Location = New Point(0, 10)
        MCB_模型选择.Margin = New Padding(2, 2, 2, 2)
        MCB_模型选择.Name = "MCB_模型选择"
        MCB_模型选择.Padding = New Padding(10, 0, 10, 0)
        MCB_模型选择.Size = New Size(200, 32)
        MCB_模型选择.TabIndex = 6
        MCB_模型选择.ToolTipGap = -1
        MCB_模型选择.ToolTipMaxWidth = 350
        MCB_模型选择.ToolTipPadding = New Padding(15)
        MCB_模型选择.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' Panel5
        ' 
        Panel5.Controls.Add(HtmlColorLabel7)
        Panel5.Controls.Add(HtmlColorLabel6)
        Panel5.Controls.Add(HtmlColorLabel9)
        Panel5.Dock = DockStyle.Top
        Panel5.Location = New Point(20, 171)
        Panel5.Name = "Panel5"
        Panel5.Size = New Size(777, 30)
        Panel5.TabIndex = 10
        ' 
        ' HtmlColorLabel7
        ' 
        HtmlColorLabel7.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel7.Dock = DockStyle.Left
        HtmlColorLabel7.Location = New Point(370, 0)
        HtmlColorLabel7.Margin = New Padding(2)
        HtmlColorLabel7.Name = "HtmlColorLabel7"
        HtmlColorLabel7.Size = New Size(150, 30)
        HtmlColorLabel7.TabIndex = 2
        HtmlColorLabel7.Text = "SubSample"
        HtmlColorLabel7.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.BottomLeft
        ' 
        ' HtmlColorLabel6
        ' 
        HtmlColorLabel6.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel6.Dock = DockStyle.Left
        HtmlColorLabel6.Location = New Point(210, 0)
        HtmlColorLabel6.Margin = New Padding(2)
        HtmlColorLabel6.Name = "HtmlColorLabel6"
        HtmlColorLabel6.Size = New Size(160, 30)
        HtmlColorLabel6.TabIndex = 1
        HtmlColorLabel6.Text = "Pooling"
        HtmlColorLabel6.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.BottomLeft
        ' 
        ' HtmlColorLabel9
        ' 
        HtmlColorLabel9.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel9.Dock = DockStyle.Left
        HtmlColorLabel9.Location = New Point(0, 0)
        HtmlColorLabel9.Margin = New Padding(2)
        HtmlColorLabel9.Name = "HtmlColorLabel9"
        HtmlColorLabel9.Size = New Size(210, 30)
        HtmlColorLabel9.TabIndex = 0
        HtmlColorLabel9.Text = "VMAF 模型"
        HtmlColorLabel9.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.BottomLeft
        ' 
        ' Panel3
        ' 
        Panel3.Controls.Add(MCB_XPSNR)
        Panel3.Controls.Add(MCB_VMAF)
        Panel3.Controls.Add(MCB_SSIM)
        Panel3.Controls.Add(MCB_PSNR)
        Panel3.Controls.Add(HtmlColorLabel4)
        Panel3.Dock = DockStyle.Top
        Panel3.Location = New Point(20, 129)
        Panel3.Name = "Panel3"
        Panel3.Padding = New Padding(0, 10, 0, 0)
        Panel3.Size = New Size(777, 42)
        Panel3.TabIndex = 7
        ' 
        ' MCB_XPSNR
        ' 
        MCB_XPSNR.AutoSize = True
        MCB_XPSNR.BoxBorderRadius = 5
        MCB_XPSNR.BoxBorderSize = 0
        MCB_XPSNR.BoxCheckedBackColor = Color.OliveDrab
        MCB_XPSNR.BoxInnerPadding = 6
        MCB_XPSNR.BoxSize = 22
        MCB_XPSNR.BoxTextSpacing = 10
        MCB_XPSNR.BoxUncheckedBackColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_XPSNR.Dock = DockStyle.Left
        MCB_XPSNR.Location = New Point(397, 10)
        MCB_XPSNR.Name = "MCB_XPSNR"
        MCB_XPSNR.Padding = New Padding(0, 0, 20, 0)
        MCB_XPSNR.Size = New Size(98, 32)
        MCB_XPSNR.TabIndex = 17
        MCB_XPSNR.Text = "XPSNR"
        ' 
        ' MCB_VMAF
        ' 
        MCB_VMAF.AutoSize = True
        MCB_VMAF.BoxBorderRadius = 5
        MCB_VMAF.BoxBorderSize = 0
        MCB_VMAF.BoxCheckedBackColor = Color.OliveDrab
        MCB_VMAF.BoxInnerPadding = 6
        MCB_VMAF.BoxSize = 22
        MCB_VMAF.BoxTextSpacing = 10
        MCB_VMAF.BoxUncheckedBackColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_VMAF.Dock = DockStyle.Left
        MCB_VMAF.Location = New Point(305, 10)
        MCB_VMAF.Name = "MCB_VMAF"
        MCB_VMAF.Padding = New Padding(0, 0, 20, 0)
        MCB_VMAF.Size = New Size(92, 32)
        MCB_VMAF.TabIndex = 16
        MCB_VMAF.Text = "VMAF"
        ' 
        ' MCB_SSIM
        ' 
        MCB_SSIM.AutoSize = True
        MCB_SSIM.BoxBorderRadius = 5
        MCB_SSIM.BoxBorderSize = 0
        MCB_SSIM.BoxCheckedBackColor = Color.OliveDrab
        MCB_SSIM.BoxInnerPadding = 6
        MCB_SSIM.BoxSize = 22
        MCB_SSIM.BoxTextSpacing = 10
        MCB_SSIM.BoxUncheckedBackColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_SSIM.Dock = DockStyle.Left
        MCB_SSIM.Location = New Point(219, 10)
        MCB_SSIM.Name = "MCB_SSIM"
        MCB_SSIM.Padding = New Padding(0, 0, 20, 0)
        MCB_SSIM.Size = New Size(86, 32)
        MCB_SSIM.TabIndex = 15
        MCB_SSIM.Text = "SSIM"
        ' 
        ' MCB_PSNR
        ' 
        MCB_PSNR.AutoSize = True
        MCB_PSNR.BoxBorderRadius = 5
        MCB_PSNR.BoxBorderSize = 0
        MCB_PSNR.BoxCheckedBackColor = Color.OliveDrab
        MCB_PSNR.BoxInnerPadding = 6
        MCB_PSNR.BoxSize = 22
        MCB_PSNR.BoxTextSpacing = 10
        MCB_PSNR.BoxUncheckedBackColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_PSNR.Dock = DockStyle.Left
        MCB_PSNR.Location = New Point(130, 10)
        MCB_PSNR.Name = "MCB_PSNR"
        MCB_PSNR.Padding = New Padding(0, 0, 20, 0)
        MCB_PSNR.Size = New Size(89, 32)
        MCB_PSNR.TabIndex = 13
        MCB_PSNR.Text = "PSNR"
        ' 
        ' HtmlColorLabel4
        ' 
        HtmlColorLabel4.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel4.Dock = DockStyle.Left
        HtmlColorLabel4.Font = New Font("Microsoft YaHei UI", 11F)
        HtmlColorLabel4.Location = New Point(0, 10)
        HtmlColorLabel4.Margin = New Padding(2)
        HtmlColorLabel4.Name = "HtmlColorLabel4"
        HtmlColorLabel4.Size = New Size(130, 32)
        HtmlColorLabel4.TabIndex = 14
        HtmlColorLabel4.Text = "要评测的项目："
        HtmlColorLabel4.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.MiddleLeft
        ' 
        ' Panel2
        ' 
        Panel2.Controls.Add(MTB_从头开始)
        Panel2.Controls.Add(HtmlColorLabel2)
        Panel2.Controls.Add(MTB_评测时长)
        Panel2.Controls.Add(HtmlColorLabel3)
        Panel2.Dock = DockStyle.Top
        Panel2.Location = New Point(20, 87)
        Panel2.Name = "Panel2"
        Panel2.Padding = New Padding(0, 10, 0, 0)
        Panel2.Size = New Size(777, 42)
        Panel2.TabIndex = 6
        ' 
        ' MTB_从头开始
        ' 
        MTB_从头开始.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_从头开始.BorderColor = Color.Transparent
        MTB_从头开始.BorderColorFocus = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MTB_从头开始.BorderRadius = 10
        MTB_从头开始.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        MTB_从头开始.Dock = DockStyle.Left
        MTB_从头开始.Location = New Point(470, 10)
        MTB_从头开始.Margin = New Padding(2)
        MTB_从头开始.Name = "MTB_从头开始"
        MTB_从头开始.Padding = New Padding(10, 0, 10, 0)
        MTB_从头开始.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_从头开始.Size = New Size(150, 32)
        MTB_从头开始.TabIndex = 15
        MTB_从头开始.WaterText = "不写 = 从头开始"
        MTB_从头开始.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' HtmlColorLabel2
        ' 
        HtmlColorLabel2.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel2.Dock = DockStyle.Left
        HtmlColorLabel2.Location = New Point(280, 10)
        HtmlColorLabel2.Margin = New Padding(2)
        HtmlColorLabel2.Name = "HtmlColorLabel2"
        HtmlColorLabel2.Padding = New Padding(20, 0, 0, 0)
        HtmlColorLabel2.Size = New Size(190, 32)
        HtmlColorLabel2.TabIndex = 14
        HtmlColorLabel2.Text = "从指定时间开始评测："
        HtmlColorLabel2.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.MiddleRight
        ' 
        ' MTB_评测时长
        ' 
        MTB_评测时长.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_评测时长.BorderColor = Color.Transparent
        MTB_评测时长.BorderColorFocus = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MTB_评测时长.BorderRadius = 10
        MTB_评测时长.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        MTB_评测时长.Dock = DockStyle.Left
        MTB_评测时长.Location = New Point(130, 10)
        MTB_评测时长.Margin = New Padding(2)
        MTB_评测时长.Name = "MTB_评测时长"
        MTB_评测时长.Padding = New Padding(10, 0, 10, 0)
        MTB_评测时长.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_评测时长.Size = New Size(150, 32)
        MTB_评测时长.TabIndex = 13
        MTB_评测时长.WaterText = "不写 = 完整全片"
        MTB_评测时长.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' HtmlColorLabel3
        ' 
        HtmlColorLabel3.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel3.Dock = DockStyle.Left
        HtmlColorLabel3.Location = New Point(0, 10)
        HtmlColorLabel3.Margin = New Padding(2)
        HtmlColorLabel3.Name = "HtmlColorLabel3"
        HtmlColorLabel3.Size = New Size(130, 32)
        HtmlColorLabel3.TabIndex = 12
        HtmlColorLabel3.Text = "进行评测的长度："
        HtmlColorLabel3.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.MiddleRight
        ' 
        ' Panel1
        ' 
        Panel1.Controls.Add(MTB_原视频文件路径)
        Panel1.Controls.Add(JustEmptyControl5)
        Panel1.Controls.Add(ModernButton5)
        Panel1.Dock = DockStyle.Top
        Panel1.Location = New Point(20, 45)
        Panel1.Name = "Panel1"
        Panel1.Padding = New Padding(0, 10, 0, 0)
        Panel1.Size = New Size(777, 42)
        Panel1.TabIndex = 5
        ' 
        ' MTB_原视频文件路径
        ' 
        MTB_原视频文件路径.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_原视频文件路径.BorderColor = Color.Transparent
        MTB_原视频文件路径.BorderColorFocus = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MTB_原视频文件路径.BorderRadius = 10
        MTB_原视频文件路径.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        MTB_原视频文件路径.Dock = DockStyle.Fill
        MTB_原视频文件路径.Location = New Point(130, 10)
        MTB_原视频文件路径.Margin = New Padding(2)
        MTB_原视频文件路径.Name = "MTB_原视频文件路径"
        MTB_原视频文件路径.Padding = New Padding(10, 0, 10, 0)
        MTB_原视频文件路径.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_原视频文件路径.Size = New Size(647, 32)
        MTB_原视频文件路径.TabIndex = 14
        MTB_原视频文件路径.WaterText = "原视频文件路径"
        MTB_原视频文件路径.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' JustEmptyControl5
        ' 
        JustEmptyControl5.Dock = DockStyle.Left
        JustEmptyControl5.Location = New Point(120, 10)
        JustEmptyControl5.Name = "JustEmptyControl5"
        JustEmptyControl5.Size = New Size(10, 32)
        JustEmptyControl5.TabIndex = 16
        ' 
        ' ModernButton5
        ' 
        ModernButton5.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernButton5.BorderRadius = 10
        ModernButton5.BorderSize = 0
        ModernButton5.Dock = DockStyle.Left
        ModernButton5.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        ModernButton5.Location = New Point(0, 10)
        ModernButton5.Margin = New Padding(2)
        ModernButton5.Name = "ModernButton5"
        ModernButton5.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        ModernButton5.Size = New Size(120, 32)
        ModernButton5.TabIndex = 15
        ModernButton5.Text = "选择原视频"
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
        HtmlColorLabel1.Size = New Size(777, 25)
        HtmlColorLabel1.TabIndex = 4
        HtmlColorLabel1.Text = "<span style=""font-size:13; color:Silver"">简易质量评测</span>   任何一种评测标准都不是万能的，请以人眼视觉感受为准"
        ' 
        ' MB_导出记录
        ' 
        MB_导出记录.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_导出记录.BorderRadius = 10
        MB_导出记录.BorderSize = 0
        MB_导出记录.Dock = DockStyle.Fill
        MB_导出记录.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_导出记录.Location = New Point(550, 20)
        MB_导出记录.Margin = New Padding(2)
        MB_导出记录.Name = "MB_导出记录"
        MB_导出记录.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_导出记录.Size = New Size(117, 35)
        MB_导出记录.TabIndex = 16
        MB_导出记录.Text = "导出记录"
        ' 
        ' JustEmptyControl8
        ' 
        JustEmptyControl8.Dock = DockStyle.Left
        JustEmptyControl8.Location = New Point(540, 20)
        JustEmptyControl8.Name = "JustEmptyControl8"
        JustEmptyControl8.Size = New Size(10, 35)
        JustEmptyControl8.TabIndex = 15
        ' 
        ' JustEmptyControl9
        ' 
        JustEmptyControl9.Dock = DockStyle.Right
        JustEmptyControl9.Location = New Point(667, 20)
        JustEmptyControl9.Name = "JustEmptyControl9"
        JustEmptyControl9.Size = New Size(10, 35)
        JustEmptyControl9.TabIndex = 17
        ' 
        ' Form_v6_集成工具_质量评测
        ' 
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ClientSize = New Size(817, 603)
        Controls.Add(ModernPanel1)
        Font = New Font("Microsoft YaHei UI", 10F)
        ForeColor = Color.Silver
        Name = "Form_v6_集成工具_质量评测"
        Text = "Form_v6_集成工具_质量评测"
        ModernPanel1.ResumeLayout(False)
        ModernPanel1.PerformLayout()
        Panel6.ResumeLayout(False)
        Panel4.ResumeLayout(False)
        Panel5.ResumeLayout(False)
        Panel3.ResumeLayout(False)
        Panel3.PerformLayout()
        Panel2.ResumeLayout(False)
        Panel1.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents ModernPanel1 As LakeUI.ModernPanel
    Friend WithEvents HtmlColorLabel1 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents HtmlColorLabel3 As LakeUI.HtmlColorLabel
    Friend WithEvents MTB_评测时长 As LakeUI.ModernTextBox
    Friend WithEvents MTB_从头开始 As LakeUI.ModernTextBox
    Friend WithEvents HtmlColorLabel2 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents MCB_PSNR As LakeUI.ModernCheckBox
    Friend WithEvents MCB_XPSNR As LakeUI.ModernCheckBox
    Friend WithEvents MCB_VMAF As LakeUI.ModernCheckBox
    Friend WithEvents MCB_SSIM As LakeUI.ModernCheckBox
    Friend WithEvents HtmlColorLabel4 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents MCB_模型选择 As LakeUI.ModernComboBox
    Friend WithEvents MCB_Pooling As LakeUI.ModernComboBox
    Friend WithEvents MCB_SubSample As LakeUI.ModernComboBox
    Friend WithEvents JustEmptyControl2 As LakeUI.JustEmptyControl
    Friend WithEvents JustEmptyControl1 As LakeUI.JustEmptyControl
    Friend WithEvents Panel5 As Panel
    Friend WithEvents HtmlColorLabel7 As LakeUI.HtmlColorLabel
    Friend WithEvents HtmlColorLabel6 As LakeUI.HtmlColorLabel
    Friend WithEvents HtmlColorLabel9 As LakeUI.HtmlColorLabel
    Friend WithEvents HtmlColorLabel8 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel6 As Panel
    Friend WithEvents MB_图表窗口 As LakeUI.ModernButton
    Friend WithEvents MB_移除选中文件 As LakeUI.ModernButton
    Friend WithEvents JustEmptyControl4 As LakeUI.JustEmptyControl
    Friend WithEvents MB_清除全部记录 As LakeUI.ModernButton
    Friend WithEvents JustEmptyControl3 As LakeUI.JustEmptyControl
    Friend WithEvents MB_清除选中记录 As LakeUI.ModernButton
    Friend WithEvents JustEmptyControl6 As LakeUI.JustEmptyControl
    Friend WithEvents MB_开始评测 As LakeUI.ModernButton
    Friend WithEvents UltraDetailListView1 As LakeUI.UltraDetailListView
    Friend WithEvents MTB_原视频文件路径 As LakeUI.ModernTextBox
    Friend WithEvents JustEmptyControl5 As LakeUI.JustEmptyControl
    Friend WithEvents ModernButton5 As LakeUI.ModernButton
    Friend WithEvents MB_移除全部文件 As LakeUI.ModernButton
    Friend WithEvents JustEmptyControl7 As LakeUI.JustEmptyControl
    Friend WithEvents MB_导出记录 As LakeUI.ModernButton
    Friend WithEvents JustEmptyControl9 As LakeUI.JustEmptyControl
    Friend WithEvents JustEmptyControl8 As LakeUI.JustEmptyControl
End Class
