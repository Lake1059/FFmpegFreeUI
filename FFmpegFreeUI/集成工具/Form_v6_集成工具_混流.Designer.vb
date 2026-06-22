<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_v6_集成工具_混流
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
        ModernPanel1 = New LakeUI.ModernPanel()
        UltraDetailListView1 = New LakeUI.UltraDetailListView()
        Panel4 = New Panel()
        MTB_字幕流索引 = New LakeUI.ModernTextBox()
        HtmlColorLabel5 = New LakeUI.HtmlColorLabel()
        MTB_音频流索引 = New LakeUI.ModernTextBox()
        HtmlColorLabel4 = New LakeUI.HtmlColorLabel()
        MTB_视频流索引 = New LakeUI.ModernTextBox()
        HtmlColorLabel3 = New LakeUI.HtmlColorLabel()
        JustEmptyControl8 = New LakeUI.JustEmptyControl()
        Panel3 = New Panel()
        HtmlColorLabel7 = New LakeUI.HtmlColorLabel()
        MCB_使用此文件的元数据 = New LakeUI.ModernCheckBox()
        MCB_使用此文件的章节 = New LakeUI.ModernCheckBox()
        HtmlColorLabel6 = New LakeUI.HtmlColorLabel()
        JustEmptyControl7 = New LakeUI.JustEmptyControl()
        Panel2 = New Panel()
        MTB_输出目标文件 = New LakeUI.ModernTextBox()
        JustEmptyControl5 = New LakeUI.JustEmptyControl()
        JustEmptyControl6 = New LakeUI.JustEmptyControl()
        MB_选择位置 = New LakeUI.ModernButton()
        MB_启动合并 = New LakeUI.ModernButton()
        Panel1 = New Panel()
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
        Panel4.SuspendLayout()
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
        ModernPanel1.Controls.Add(Panel4)
        ModernPanel1.Controls.Add(Panel3)
        ModernPanel1.Controls.Add(Panel2)
        ModernPanel1.Controls.Add(Panel1)
        ModernPanel1.Controls.Add(HtmlColorLabel1)
        ModernPanel1.Dock = DockStyle.Fill
        ModernPanel1.Location = New Point(0, 0)
        ModernPanel1.Name = "ModernPanel1"
        ModernPanel1.Padding = New Padding(20)
        ModernPanel1.Size = New Size(841, 648)
        ModernPanel1.TabIndex = 0
        ' 
        ' UltraDetailListView1
        ' 
        UltraDetailListView1.AllowDragReorder = True
        UltraDetailListView1.BackgroundColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        UltraDetailListView1.BorderRadius = 10
        UltraDetailListView1.BorderSize = 0
        ListColumn1.Text = "文件"
        ListColumn1.Width = 300
        ListColumn2.Text = "视频"
        ListColumn2.Width = 80
        ListColumn3.Text = "音频"
        ListColumn3.Width = 80
        ListColumn4.Text = "字幕"
        ListColumn4.Width = 80
        ListColumn5.Text = "章节"
        ListColumn5.Width = 80
        ListColumn6.Text = "元数据"
        ListColumn6.Width = 80
        UltraDetailListView1.Columns.Add(ListColumn1)
        UltraDetailListView1.Columns.Add(ListColumn2)
        UltraDetailListView1.Columns.Add(ListColumn3)
        UltraDetailListView1.Columns.Add(ListColumn4)
        UltraDetailListView1.Columns.Add(ListColumn5)
        UltraDetailListView1.Columns.Add(ListColumn6)
        UltraDetailListView1.Dock = DockStyle.Fill
        UltraDetailListView1.DragSelectZoneWidth = 200
        UltraDetailListView1.GroupBorderColor = Color.Silver
        UltraDetailListView1.GroupHeight = 35
        UltraDetailListView1.HeaderBackColor = Color.Transparent
        UltraDetailListView1.HeaderBorderColor = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        UltraDetailListView1.HeaderHeight = 40
        UltraDetailListView1.ItemCornerRadius = 10
        UltraDetailListView1.ItemPadding = New Padding(10, 5, 10, 5)
        UltraDetailListView1.ItemSelectedBackColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        UltraDetailListView1.Location = New Point(20, 97)
        UltraDetailListView1.Margin = New Padding(2, 2, 2, 2)
        UltraDetailListView1.Name = "UltraDetailListView1"
        UltraDetailListView1.Padding = New Padding(5, 0, 5, 5)
        UltraDetailListView1.ScrollBarThumbColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        UltraDetailListView1.ScrollBarThumbHoverColor = Color.FromArgb(CByte(120), CByte(220), CByte(220), CByte(220))
        UltraDetailListView1.ScrollBarTrackColor = Color.FromArgb(CByte(20), CByte(220), CByte(220), CByte(220))
        UltraDetailListView1.SelectionRectBorderColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        UltraDetailListView1.SelectionRectFillColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        UltraDetailListView1.Size = New Size(801, 405)
        UltraDetailListView1.TabIndex = 8
        ' 
        ' Panel4
        ' 
        Panel4.Controls.Add(MTB_字幕流索引)
        Panel4.Controls.Add(HtmlColorLabel5)
        Panel4.Controls.Add(MTB_音频流索引)
        Panel4.Controls.Add(HtmlColorLabel4)
        Panel4.Controls.Add(MTB_视频流索引)
        Panel4.Controls.Add(HtmlColorLabel3)
        Panel4.Controls.Add(JustEmptyControl8)
        Panel4.Dock = DockStyle.Bottom
        Panel4.Location = New Point(20, 502)
        Panel4.Name = "Panel4"
        Panel4.Padding = New Padding(0, 10, 0, 0)
        Panel4.Size = New Size(801, 42)
        Panel4.TabIndex = 7
        ' 
        ' MTB_字幕流索引
        ' 
        MTB_字幕流索引.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_字幕流索引.BorderColor = Color.Transparent
        MTB_字幕流索引.BorderColorFocus = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MTB_字幕流索引.BorderRadius = 10
        MTB_字幕流索引.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        MTB_字幕流索引.Dock = DockStyle.Left
        MTB_字幕流索引.Location = New Point(560, 10)
        MTB_字幕流索引.Margin = New Padding(2)
        MTB_字幕流索引.Name = "MTB_字幕流索引"
        MTB_字幕流索引.Padding = New Padding(10, 0, 10, 0)
        MTB_字幕流索引.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_字幕流索引.Size = New Size(100, 32)
        MTB_字幕流索引.TabIndex = 16
        MTB_字幕流索引.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' HtmlColorLabel5
        ' 
        HtmlColorLabel5.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel5.Dock = DockStyle.Left
        HtmlColorLabel5.Location = New Point(440, 10)
        HtmlColorLabel5.Margin = New Padding(2)
        HtmlColorLabel5.Name = "HtmlColorLabel5"
        HtmlColorLabel5.Padding = New Padding(20, 0, 0, 0)
        HtmlColorLabel5.Size = New Size(120, 32)
        HtmlColorLabel5.TabIndex = 15
        HtmlColorLabel5.Text = "字幕流索引："
        HtmlColorLabel5.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.MiddleLeft
        ' 
        ' MTB_音频流索引
        ' 
        MTB_音频流索引.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_音频流索引.BorderColor = Color.Transparent
        MTB_音频流索引.BorderColorFocus = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MTB_音频流索引.BorderRadius = 10
        MTB_音频流索引.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        MTB_音频流索引.Dock = DockStyle.Left
        MTB_音频流索引.Location = New Point(340, 10)
        MTB_音频流索引.Margin = New Padding(2)
        MTB_音频流索引.Name = "MTB_音频流索引"
        MTB_音频流索引.Padding = New Padding(10, 0, 10, 0)
        MTB_音频流索引.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_音频流索引.Size = New Size(100, 32)
        MTB_音频流索引.TabIndex = 14
        MTB_音频流索引.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' HtmlColorLabel4
        ' 
        HtmlColorLabel4.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel4.Dock = DockStyle.Left
        HtmlColorLabel4.Location = New Point(220, 10)
        HtmlColorLabel4.Margin = New Padding(2)
        HtmlColorLabel4.Name = "HtmlColorLabel4"
        HtmlColorLabel4.Padding = New Padding(20, 0, 0, 0)
        HtmlColorLabel4.Size = New Size(120, 32)
        HtmlColorLabel4.TabIndex = 13
        HtmlColorLabel4.Text = "音频流索引："
        HtmlColorLabel4.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.MiddleLeft
        ' 
        ' MTB_视频流索引
        ' 
        MTB_视频流索引.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_视频流索引.BorderColor = Color.Transparent
        MTB_视频流索引.BorderColorFocus = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MTB_视频流索引.BorderRadius = 10
        MTB_视频流索引.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        MTB_视频流索引.Dock = DockStyle.Left
        MTB_视频流索引.Location = New Point(120, 10)
        MTB_视频流索引.Margin = New Padding(2)
        MTB_视频流索引.Name = "MTB_视频流索引"
        MTB_视频流索引.Padding = New Padding(10, 0, 10, 0)
        MTB_视频流索引.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_视频流索引.Size = New Size(100, 32)
        MTB_视频流索引.TabIndex = 12
        MTB_视频流索引.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' HtmlColorLabel3
        ' 
        HtmlColorLabel3.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel3.Dock = DockStyle.Left
        HtmlColorLabel3.Location = New Point(0, 10)
        HtmlColorLabel3.Margin = New Padding(2)
        HtmlColorLabel3.Name = "HtmlColorLabel3"
        HtmlColorLabel3.Size = New Size(120, 32)
        HtmlColorLabel3.TabIndex = 11
        HtmlColorLabel3.Text = "视频流索引："
        HtmlColorLabel3.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.MiddleLeft
        ' 
        ' JustEmptyControl8
        ' 
        JustEmptyControl8.Dock = DockStyle.Right
        JustEmptyControl8.Location = New Point(791, 10)
        JustEmptyControl8.Name = "JustEmptyControl8"
        JustEmptyControl8.Size = New Size(10, 32)
        JustEmptyControl8.TabIndex = 4
        ' 
        ' Panel3
        ' 
        Panel3.Controls.Add(HtmlColorLabel7)
        Panel3.Controls.Add(MCB_使用此文件的元数据)
        Panel3.Controls.Add(MCB_使用此文件的章节)
        Panel3.Controls.Add(HtmlColorLabel6)
        Panel3.Controls.Add(JustEmptyControl7)
        Panel3.Dock = DockStyle.Bottom
        Panel3.Location = New Point(20, 544)
        Panel3.Name = "Panel3"
        Panel3.Padding = New Padding(0, 10, 0, 0)
        Panel3.Size = New Size(801, 42)
        Panel3.TabIndex = 6
        ' 
        ' HtmlColorLabel7
        ' 
        HtmlColorLabel7.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel7.Dock = DockStyle.Fill
        HtmlColorLabel7.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HtmlColorLabel7.Location = New Point(462, 10)
        HtmlColorLabel7.Margin = New Padding(2)
        HtmlColorLabel7.Name = "HtmlColorLabel7"
        HtmlColorLabel7.Size = New Size(329, 32)
        HtmlColorLabel7.TabIndex = 15
        HtmlColorLabel7.Text = "填写流索引时，多个流用英文逗号个隔开"
        HtmlColorLabel7.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.MiddleLeft
        ' 
        ' MCB_使用此文件的元数据
        ' 
        MCB_使用此文件的元数据.AutoSize = True
        MCB_使用此文件的元数据.BoxBorderRadius = 5
        MCB_使用此文件的元数据.BoxBorderSize = 0
        MCB_使用此文件的元数据.BoxCheckedBackColor = Color.OliveDrab
        MCB_使用此文件的元数据.BoxInnerPadding = 6
        MCB_使用此文件的元数据.BoxSize = 22
        MCB_使用此文件的元数据.BoxTextSpacing = 10
        MCB_使用此文件的元数据.BoxUncheckedBackColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_使用此文件的元数据.Dock = DockStyle.Left
        MCB_使用此文件的元数据.Location = New Point(284, 10)
        MCB_使用此文件的元数据.Name = "MCB_使用此文件的元数据"
        MCB_使用此文件的元数据.Padding = New Padding(0, 5, 20, 5)
        MCB_使用此文件的元数据.Size = New Size(178, 32)
        MCB_使用此文件的元数据.TabIndex = 14
        MCB_使用此文件的元数据.Text = "使用此文件的元数据"
        ' 
        ' MCB_使用此文件的章节
        ' 
        MCB_使用此文件的章节.AutoSize = True
        MCB_使用此文件的章节.BoxBorderRadius = 5
        MCB_使用此文件的章节.BoxBorderSize = 0
        MCB_使用此文件的章节.BoxCheckedBackColor = Color.OliveDrab
        MCB_使用此文件的章节.BoxInnerPadding = 6
        MCB_使用此文件的章节.BoxSize = 22
        MCB_使用此文件的章节.BoxTextSpacing = 10
        MCB_使用此文件的章节.BoxUncheckedBackColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_使用此文件的章节.Dock = DockStyle.Left
        MCB_使用此文件的章节.Location = New Point(120, 10)
        MCB_使用此文件的章节.Name = "MCB_使用此文件的章节"
        MCB_使用此文件的章节.Padding = New Padding(0, 5, 20, 5)
        MCB_使用此文件的章节.Size = New Size(164, 32)
        MCB_使用此文件的章节.TabIndex = 13
        MCB_使用此文件的章节.Text = "使用此文件的章节"
        ' 
        ' HtmlColorLabel6
        ' 
        HtmlColorLabel6.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel6.Dock = DockStyle.Left
        HtmlColorLabel6.Location = New Point(0, 10)
        HtmlColorLabel6.Margin = New Padding(2)
        HtmlColorLabel6.Name = "HtmlColorLabel6"
        HtmlColorLabel6.Padding = New Padding(0, 0, 20, 0)
        HtmlColorLabel6.Size = New Size(120, 32)
        HtmlColorLabel6.TabIndex = 12
        HtmlColorLabel6.Text = "章节和元数据"
        HtmlColorLabel6.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.MiddleLeft
        ' 
        ' JustEmptyControl7
        ' 
        JustEmptyControl7.Dock = DockStyle.Right
        JustEmptyControl7.Location = New Point(791, 10)
        JustEmptyControl7.Name = "JustEmptyControl7"
        JustEmptyControl7.Size = New Size(10, 32)
        JustEmptyControl7.TabIndex = 4
        ' 
        ' Panel2
        ' 
        Panel2.Controls.Add(MTB_输出目标文件)
        Panel2.Controls.Add(JustEmptyControl5)
        Panel2.Controls.Add(JustEmptyControl6)
        Panel2.Controls.Add(MB_选择位置)
        Panel2.Controls.Add(MB_启动合并)
        Panel2.Dock = DockStyle.Bottom
        Panel2.Location = New Point(20, 586)
        Panel2.Name = "Panel2"
        Panel2.Padding = New Padding(0, 10, 0, 0)
        Panel2.Size = New Size(801, 42)
        Panel2.TabIndex = 5
        ' 
        ' MTB_输出目标文件
        ' 
        MTB_输出目标文件.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_输出目标文件.BorderColor = Color.Transparent
        MTB_输出目标文件.BorderColorFocus = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MTB_输出目标文件.BorderRadius = 10
        MTB_输出目标文件.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        MTB_输出目标文件.Dock = DockStyle.Fill
        MTB_输出目标文件.Location = New Point(110, 10)
        MTB_输出目标文件.Margin = New Padding(2)
        MTB_输出目标文件.Name = "MTB_输出目标文件"
        MTB_输出目标文件.Padding = New Padding(10, 0, 10, 0)
        MTB_输出目标文件.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_输出目标文件.Size = New Size(581, 32)
        MTB_输出目标文件.TabIndex = 3
        MTB_输出目标文件.WaterText = "输出目标文件路径"
        MTB_输出目标文件.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' JustEmptyControl5
        ' 
        JustEmptyControl5.Dock = DockStyle.Right
        JustEmptyControl5.Location = New Point(691, 10)
        JustEmptyControl5.Name = "JustEmptyControl5"
        JustEmptyControl5.Size = New Size(10, 32)
        JustEmptyControl5.TabIndex = 4
        ' 
        ' JustEmptyControl6
        ' 
        JustEmptyControl6.Dock = DockStyle.Left
        JustEmptyControl6.Location = New Point(100, 10)
        JustEmptyControl6.Name = "JustEmptyControl6"
        JustEmptyControl6.Size = New Size(10, 32)
        JustEmptyControl6.TabIndex = 2
        ' 
        ' MB_选择位置
        ' 
        MB_选择位置.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_选择位置.BorderRadius = 10
        MB_选择位置.BorderSize = 0
        MB_选择位置.Dock = DockStyle.Left
        MB_选择位置.ForeColor = Color.YellowGreen
        MB_选择位置.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_选择位置.Location = New Point(0, 10)
        MB_选择位置.Margin = New Padding(2)
        MB_选择位置.Name = "MB_选择位置"
        MB_选择位置.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_选择位置.Size = New Size(100, 32)
        MB_选择位置.TabIndex = 0
        MB_选择位置.Text = "选择位置"
        ' 
        ' MB_启动合并
        ' 
        MB_启动合并.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_启动合并.BorderRadius = 10
        MB_启动合并.BorderSize = 0
        MB_启动合并.Dock = DockStyle.Right
        MB_启动合并.ForeColor = Color.YellowGreen
        MB_启动合并.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_启动合并.Location = New Point(701, 10)
        MB_启动合并.Margin = New Padding(2)
        MB_启动合并.Name = "MB_启动合并"
        MB_启动合并.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_启动合并.Size = New Size(100, 32)
        MB_启动合并.TabIndex = 5
        MB_启动合并.Text = "启动混流"
        ' 
        ' Panel1
        ' 
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
        Panel1.Location = New Point(20, 45)
        Panel1.Name = "Panel1"
        Panel1.Padding = New Padding(0, 10, 0, 10)
        Panel1.Size = New Size(801, 52)
        Panel1.TabIndex = 4
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
        HtmlColorLabel2.Size = New Size(341, 32)
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
        HtmlColorLabel1.Size = New Size(801, 25)
        HtmlColorLabel1.TabIndex = 3
        HtmlColorLabel1.Text = "<span style=""font-size:13; color:Silver"">拆出各文件的指定流进行合成</span>   如果你的需求不是这个那么你可能指的是合并"
        ' 
        ' Form_v6_集成工具_混流
        ' 
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ClientSize = New Size(841, 648)
        Controls.Add(ModernPanel1)
        Font = New Font("Microsoft YaHei UI", 10F)
        ForeColor = Color.Silver
        Name = "Form_v6_集成工具_混流"
        Text = "Form_v6_集成工具_混流"
        ModernPanel1.ResumeLayout(False)
        ModernPanel1.PerformLayout()
        Panel4.ResumeLayout(False)
        Panel3.ResumeLayout(False)
        Panel3.PerformLayout()
        Panel2.ResumeLayout(False)
        Panel1.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents ModernPanel1 As LakeUI.ModernPanel
    Friend WithEvents Panel1 As Panel
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
    Friend WithEvents Panel2 As Panel
    Friend WithEvents MTB_输出目标文件 As LakeUI.ModernTextBox
    Friend WithEvents JustEmptyControl5 As LakeUI.JustEmptyControl
    Friend WithEvents JustEmptyControl6 As LakeUI.JustEmptyControl
    Friend WithEvents MB_选择位置 As LakeUI.ModernButton
    Friend WithEvents MB_启动合并 As LakeUI.ModernButton
    Friend WithEvents Panel4 As Panel
    Friend WithEvents JustEmptyControl8 As LakeUI.JustEmptyControl
    Friend WithEvents Panel3 As Panel
    Friend WithEvents JustEmptyControl7 As LakeUI.JustEmptyControl
    Friend WithEvents UltraDetailListView1 As LakeUI.UltraDetailListView
    Friend WithEvents MTB_音频流索引 As LakeUI.ModernTextBox
    Friend WithEvents HtmlColorLabel4 As LakeUI.HtmlColorLabel
    Friend WithEvents MTB_视频流索引 As LakeUI.ModernTextBox
    Friend WithEvents HtmlColorLabel3 As LakeUI.HtmlColorLabel
    Friend WithEvents MTB_字幕流索引 As LakeUI.ModernTextBox
    Friend WithEvents HtmlColorLabel5 As LakeUI.HtmlColorLabel
    Friend WithEvents HtmlColorLabel6 As LakeUI.HtmlColorLabel
    Friend WithEvents MCB_使用此文件的元数据 As LakeUI.ModernCheckBox
    Friend WithEvents MCB_使用此文件的章节 As LakeUI.ModernCheckBox
    Friend WithEvents HtmlColorLabel7 As LakeUI.HtmlColorLabel
End Class
