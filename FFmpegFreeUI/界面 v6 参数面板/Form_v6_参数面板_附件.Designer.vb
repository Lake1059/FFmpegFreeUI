<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_v6_参数面板_附件
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
        ModernPanel1 = New LakeUI.ModernPanel()
        UDLV_附件列表 = New LakeUI.UltraDetailListView()
        Panel1 = New Panel()
        MB_导入附件 = New LakeUI.ModernButton()
        JustEmptyControl4 = New LakeUI.JustEmptyControl()
        MB_导出附件 = New LakeUI.ModernButton()
        JustEmptyControl3 = New LakeUI.JustEmptyControl()
        MB_清空全部附件 = New LakeUI.ModernButton()
        JustEmptyControl2 = New LakeUI.JustEmptyControl()
        MB_删除所选附件 = New LakeUI.ModernButton()
        JustEmptyControl1 = New LakeUI.JustEmptyControl()
        MCB_添加附件 = New LakeUI.ModernComboBox()
        HCL_附件说明 = New LakeUI.HtmlColorLabel()
        ModernPanel1.SuspendLayout()
        Panel1.SuspendLayout()
        SuspendLayout()
        '
        ' ModernPanel1
        '
        ModernPanel1.BackColor1 = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ModernPanel1.BorderSize = 0
        ModernPanel1.Controls.Add(UDLV_附件列表)
        ModernPanel1.Controls.Add(Panel1)
        ModernPanel1.Controls.Add(HCL_附件说明)
        ModernPanel1.Dock = DockStyle.Fill
        ModernPanel1.Location = New Point(0, 0)
        ModernPanel1.Name = "ModernPanel1"
        ModernPanel1.Padding = New Padding(20)
        ModernPanel1.ScrollBarMode = LakeUI.ModernPanel.ScrollMode.Vertical
        ModernPanel1.Size = New Size(767, 570)
        ModernPanel1.TabIndex = 2
        '
        ' UDLV_附件列表
        '
        UDLV_附件列表.AllowDragReorder = True
        UDLV_附件列表.BackgroundColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        UDLV_附件列表.BorderRadius = 10
        UDLV_附件列表.BorderSize = 0
        ListColumn1.Text = "类型"
        ListColumn1.Width = 150
        ListColumn2.AllowLabelEdit = True
        ListColumn2.Text = "文件路径"
        ListColumn2.Width = 500
        UDLV_附件列表.Columns.Add(ListColumn1)
        UDLV_附件列表.Columns.Add(ListColumn2)
        UDLV_附件列表.Dock = DockStyle.Fill
        UDLV_附件列表.DragSelectZoneWidth = 100
        UDLV_附件列表.GroupBorderColor = Color.Silver
        UDLV_附件列表.GroupHeight = 35
        UDLV_附件列表.HeaderBackColor = Color.Transparent
        UDLV_附件列表.HeaderBorderColor = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        UDLV_附件列表.HeaderHeight = 40
        UDLV_附件列表.ItemPadding = New Padding(10, 6, 10, 6)
        UDLV_附件列表.ItemSelectedBackColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        UDLV_附件列表.Location = New Point(20, 102)
        UDLV_附件列表.Margin = New Padding(2, 2, 2, 2)
        UDLV_附件列表.Name = "UDLV_附件列表"
        UDLV_附件列表.SelectionRectBorderColor = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        UDLV_附件列表.SelectionRectFillColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        UDLV_附件列表.Size = New Size(727, 448)
        UDLV_附件列表.TabIndex = 1
        '
        ' Panel1
        '
        Panel1.Controls.Add(MB_导入附件)
        Panel1.Controls.Add(JustEmptyControl4)
        Panel1.Controls.Add(MB_导出附件)
        Panel1.Controls.Add(JustEmptyControl3)
        Panel1.Controls.Add(MB_清空全部附件)
        Panel1.Controls.Add(JustEmptyControl2)
        Panel1.Controls.Add(MB_删除所选附件)
        Panel1.Controls.Add(JustEmptyControl1)
        Panel1.Controls.Add(MCB_添加附件)
        Panel1.Dock = DockStyle.Top
        Panel1.Location = New Point(20, 50)
        Panel1.Name = "Panel1"
        Panel1.Padding = New Padding(0, 10, 0, 10)
        Panel1.Size = New Size(727, 52)
        Panel1.TabIndex = 11
        '
        ' MB_导入附件
        '
        MB_导入附件.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_导入附件.BorderRadius = 10
        MB_导入附件.BorderSize = 0
        MB_导入附件.Dock = DockStyle.Left
        MB_导入附件.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_导入附件.Location = New Point(510, 10)
        MB_导入附件.Margin = New Padding(2)
        MB_导入附件.Name = "MB_导入附件"
        MB_导入附件.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_导入附件.Size = New Size(70, 32)
        MB_导入附件.TabIndex = 16
        MB_导入附件.Text = "导入"
        '
        ' JustEmptyControl4
        '
        JustEmptyControl4.Dock = DockStyle.Left
        JustEmptyControl4.Location = New Point(500, 10)
        JustEmptyControl4.Name = "JustEmptyControl4"
        JustEmptyControl4.Size = New Size(10, 32)
        JustEmptyControl4.TabIndex = 15
        '
        ' MB_导出附件
        '
        MB_导出附件.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_导出附件.BorderRadius = 10
        MB_导出附件.BorderSize = 0
        MB_导出附件.Dock = DockStyle.Left
        MB_导出附件.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_导出附件.Location = New Point(430, 10)
        MB_导出附件.Margin = New Padding(2)
        MB_导出附件.Name = "MB_导出附件"
        MB_导出附件.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_导出附件.Size = New Size(70, 32)
        MB_导出附件.TabIndex = 14
        MB_导出附件.Text = "导出"
        '
        ' JustEmptyControl3
        '
        JustEmptyControl3.Dock = DockStyle.Left
        JustEmptyControl3.Location = New Point(420, 10)
        JustEmptyControl3.Name = "JustEmptyControl3"
        JustEmptyControl3.Size = New Size(10, 32)
        JustEmptyControl3.TabIndex = 13
        '
        ' MB_清空全部附件
        '
        MB_清空全部附件.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_清空全部附件.BorderRadius = 10
        MB_清空全部附件.BorderSize = 0
        MB_清空全部附件.Dock = DockStyle.Left
        MB_清空全部附件.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_清空全部附件.Location = New Point(320, 10)
        MB_清空全部附件.Margin = New Padding(2)
        MB_清空全部附件.Name = "MB_清空全部附件"
        MB_清空全部附件.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_清空全部附件.Size = New Size(100, 32)
        MB_清空全部附件.TabIndex = 12
        MB_清空全部附件.Text = "全部清空"
        '
        ' JustEmptyControl2
        '
        JustEmptyControl2.Dock = DockStyle.Left
        JustEmptyControl2.Location = New Point(310, 10)
        JustEmptyControl2.Name = "JustEmptyControl2"
        JustEmptyControl2.Size = New Size(10, 32)
        JustEmptyControl2.TabIndex = 11
        '
        ' MB_删除所选附件
        '
        MB_删除所选附件.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_删除所选附件.BorderRadius = 10
        MB_删除所选附件.BorderSize = 0
        MB_删除所选附件.Dock = DockStyle.Left
        MB_删除所选附件.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_删除所选附件.Location = New Point(210, 10)
        MB_删除所选附件.Margin = New Padding(2)
        MB_删除所选附件.Name = "MB_删除所选附件"
        MB_删除所选附件.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_删除所选附件.Size = New Size(100, 32)
        MB_删除所选附件.TabIndex = 7
        MB_删除所选附件.Text = "删除所选"
        '
        ' JustEmptyControl1
        '
        JustEmptyControl1.Dock = DockStyle.Left
        JustEmptyControl1.Location = New Point(200, 10)
        JustEmptyControl1.Name = "JustEmptyControl1"
        JustEmptyControl1.Size = New Size(10, 32)
        JustEmptyControl1.TabIndex = 2
        '
        ' MCB_添加附件
        '
        MCB_添加附件.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_添加附件.BorderRadius = 10
        MCB_添加附件.BorderSize = 0
        MCB_添加附件.Dock = DockStyle.Left
        MCB_添加附件.DropDownBackdropBlurPasses = 2
        MCB_添加附件.DropDownBackdropBlurRadius = 30
        MCB_添加附件.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_添加附件.DropDownHoverColor = Color.FromArgb(CByte(20), CByte(220), CByte(220), CByte(220))
        MCB_添加附件.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_添加附件.DropDownPadding = New Padding(10)
        MCB_添加附件.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_添加附件.DropDownSelectedForeColor = Color.White
        MCB_添加附件.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_添加附件.Margin = New Padding(2, 2, 2, 2)
        MCB_添加附件.Padding = New Padding(10, 0, 10, 0)
        MCB_添加附件.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_添加附件.ToolTipGap = -1
        MCB_添加附件.ToolTipMaxWidth = 350
        MCB_添加附件.ToolTipPadding = New Padding(15)
        MCB_添加附件.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        MCB_添加附件.Items.Add("添加图片")
        MCB_添加附件.Items.Add("添加 MP4 封面图")
        MCB_添加附件.Items.Add("添加 MKV 封面图")
        MCB_添加附件.Items.Add("添加字体文件")
        MCB_添加附件.Items.Add("添加文本文档")
        MCB_添加附件.Location = New Point(0, 10)
        MCB_添加附件.MaxDropDownItems = 15
        MCB_添加附件.Name = "MCB_添加附件"
        MCB_添加附件.Size = New Size(200, 32)
        MCB_添加附件.TabIndex = 10
        MCB_添加附件.WaterText = "添加附件"
        '
        ' HCL_附件说明
        '
        HCL_附件说明.AutoSize = True
        HCL_附件说明.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_附件说明.Dock = DockStyle.Top
        HCL_附件说明.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_附件说明.Location = New Point(20, 20)
        HCL_附件说明.Margin = New Padding(2)
        HCL_附件说明.Name = "HCL_附件说明"
        HCL_附件说明.Padding = New Padding(0, 0, 0, 5)
        HCL_附件说明.Size = New Size(727, 30)
        HCL_附件说明.TabIndex = 10
        HCL_附件说明.Text = "<span style=""font-size:13; color:Silver"">附件</span>   向输出文件中塞入附件，例如图片、字体、文本文档，甚至是封面图"
        '
        ' Form_v6_参数面板_附件
        '
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ClientSize = New Size(767, 570)
        Controls.Add(ModernPanel1)
        Font = New Font("Microsoft YaHei UI", 10F)
        ForeColor = Color.Silver
        Name = "Form_v6_参数面板_附件"
        Text = "Form_v6_参数面板_附件"
        ModernPanel1.ResumeLayout(False)
        ModernPanel1.PerformLayout()
        Panel1.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents ModernPanel1 As LakeUI.ModernPanel
    Friend WithEvents UDLV_附件列表 As LakeUI.UltraDetailListView
    Friend WithEvents Panel1 As Panel
    Friend WithEvents MB_导入附件 As LakeUI.ModernButton
    Friend WithEvents JustEmptyControl4 As LakeUI.JustEmptyControl
    Friend WithEvents MB_导出附件 As LakeUI.ModernButton
    Friend WithEvents JustEmptyControl3 As LakeUI.JustEmptyControl
    Friend WithEvents MB_清空全部附件 As LakeUI.ModernButton
    Friend WithEvents JustEmptyControl2 As LakeUI.JustEmptyControl
    Friend WithEvents MB_删除所选附件 As LakeUI.ModernButton
    Friend WithEvents JustEmptyControl1 As LakeUI.JustEmptyControl
    Friend WithEvents MCB_添加附件 As LakeUI.ModernComboBox
    Friend WithEvents HCL_附件说明 As LakeUI.HtmlColorLabel
End Class
