<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_v6_参数面板_元数据
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
        UDLV_元数据列表 = New LakeUI.UltraDetailListView()
        Panel1 = New Panel()
        MB_导入元数据 = New LakeUI.ModernButton()
        JustEmptyControl4 = New LakeUI.JustEmptyControl()
        MB_导出元数据 = New LakeUI.ModernButton()
        JustEmptyControl3 = New LakeUI.JustEmptyControl()
        MB_清空全部元数据 = New LakeUI.ModernButton()
        JustEmptyControl2 = New LakeUI.JustEmptyControl()
        MB_删除所选元数据 = New LakeUI.ModernButton()
        JustEmptyControl1 = New LakeUI.JustEmptyControl()
        MCB_添加元数据预制项 = New LakeUI.ModernComboBox()
        HCL_元数据说明 = New LakeUI.HtmlColorLabel()
        ModernPanel1.SuspendLayout()
        Panel1.SuspendLayout()
        SuspendLayout()
        '
        ' ModernPanel1
        '
        ModernPanel1.BackColor1 = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ModernPanel1.BorderSize = 0
        ModernPanel1.Controls.Add(UDLV_元数据列表)
        ModernPanel1.Controls.Add(Panel1)
        ModernPanel1.Controls.Add(HCL_元数据说明)
        ModernPanel1.Dock = DockStyle.Fill
        ModernPanel1.Location = New Point(0, 0)
        ModernPanel1.Name = "ModernPanel1"
        ModernPanel1.Padding = New Padding(20)
        ModernPanel1.ScrollBarMode = LakeUI.ModernPanel.ScrollMode.Vertical
        ModernPanel1.Size = New Size(864, 643)
        ModernPanel1.TabIndex = 1
        '
        ' UDLV_元数据列表
        '
        UDLV_元数据列表.AllowDragReorder = True
        UDLV_元数据列表.BackgroundColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        UDLV_元数据列表.BorderRadius = 10
        UDLV_元数据列表.BorderSize = 0
        ListColumn1.Text = "字段"
        ListColumn1.Width = 150
        ListColumn2.AllowLabelEdit = True
        ListColumn2.Text = "值"
        ListColumn2.Width = 500
        UDLV_元数据列表.Columns.Add(ListColumn1)
        UDLV_元数据列表.Columns.Add(ListColumn2)
        UDLV_元数据列表.Dock = DockStyle.Fill
        UDLV_元数据列表.DragSelectZoneWidth = 100
        UDLV_元数据列表.GroupBorderColor = Color.Silver
        UDLV_元数据列表.GroupHeight = 35
        UDLV_元数据列表.HeaderBackColor = Color.Transparent
        UDLV_元数据列表.HeaderBorderColor = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        UDLV_元数据列表.HeaderHeight = 40
        UDLV_元数据列表.ItemPadding = New Padding(10, 6, 10, 6)
        UDLV_元数据列表.ItemSelectedBackColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        UDLV_元数据列表.Location = New Point(20, 102)
        UDLV_元数据列表.Margin = New Padding(2, 2, 2, 2)
        UDLV_元数据列表.Name = "UDLV_元数据列表"
        UDLV_元数据列表.SelectionRectFillColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        UDLV_元数据列表.SelectionRectBorderColor = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        UDLV_元数据列表.Size = New Size(824, 521)
        UDLV_元数据列表.TabIndex = 1
        '
        ' Panel1
        '
        Panel1.Controls.Add(MB_导入元数据)
        Panel1.Controls.Add(JustEmptyControl4)
        Panel1.Controls.Add(MB_导出元数据)
        Panel1.Controls.Add(JustEmptyControl3)
        Panel1.Controls.Add(MB_清空全部元数据)
        Panel1.Controls.Add(JustEmptyControl2)
        Panel1.Controls.Add(MB_删除所选元数据)
        Panel1.Controls.Add(JustEmptyControl1)
        Panel1.Controls.Add(MCB_添加元数据预制项)
        Panel1.Dock = DockStyle.Top
        Panel1.Location = New Point(20, 50)
        Panel1.Name = "Panel1"
        Panel1.Padding = New Padding(0, 10, 0, 10)
        Panel1.Size = New Size(824, 52)
        Panel1.TabIndex = 11
        '
        ' MB_导入元数据
        '
        MB_导入元数据.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_导入元数据.BorderRadius = 10
        MB_导入元数据.BorderSize = 0
        MB_导入元数据.Dock = DockStyle.Left
        MB_导入元数据.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_导入元数据.Location = New Point(510, 10)
        MB_导入元数据.Margin = New Padding(2)
        MB_导入元数据.Name = "MB_导入元数据"
        MB_导入元数据.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_导入元数据.Size = New Size(70, 32)
        MB_导入元数据.TabIndex = 16
        MB_导入元数据.Text = "导入"
        '
        ' JustEmptyControl4
        '
        JustEmptyControl4.Dock = DockStyle.Left
        JustEmptyControl4.Location = New Point(500, 10)
        JustEmptyControl4.Name = "JustEmptyControl4"
        JustEmptyControl4.Size = New Size(10, 32)
        JustEmptyControl4.TabIndex = 15
        '
        ' MB_导出元数据
        '
        MB_导出元数据.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_导出元数据.BorderRadius = 10
        MB_导出元数据.BorderSize = 0
        MB_导出元数据.Dock = DockStyle.Left
        MB_导出元数据.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_导出元数据.Location = New Point(430, 10)
        MB_导出元数据.Margin = New Padding(2)
        MB_导出元数据.Name = "MB_导出元数据"
        MB_导出元数据.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_导出元数据.Size = New Size(70, 32)
        MB_导出元数据.TabIndex = 14
        MB_导出元数据.Text = "导出"
        '
        ' JustEmptyControl3
        '
        JustEmptyControl3.Dock = DockStyle.Left
        JustEmptyControl3.Location = New Point(420, 10)
        JustEmptyControl3.Name = "JustEmptyControl3"
        JustEmptyControl3.Size = New Size(10, 32)
        JustEmptyControl3.TabIndex = 13
        '
        ' MB_清空全部元数据
        '
        MB_清空全部元数据.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_清空全部元数据.BorderRadius = 10
        MB_清空全部元数据.BorderSize = 0
        MB_清空全部元数据.Dock = DockStyle.Left
        MB_清空全部元数据.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_清空全部元数据.Location = New Point(320, 10)
        MB_清空全部元数据.Margin = New Padding(2)
        MB_清空全部元数据.Name = "MB_清空全部元数据"
        MB_清空全部元数据.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_清空全部元数据.Size = New Size(100, 32)
        MB_清空全部元数据.TabIndex = 12
        MB_清空全部元数据.Text = "全部清空"
        '
        ' JustEmptyControl2
        '
        JustEmptyControl2.Dock = DockStyle.Left
        JustEmptyControl2.Location = New Point(310, 10)
        JustEmptyControl2.Name = "JustEmptyControl2"
        JustEmptyControl2.Size = New Size(10, 32)
        JustEmptyControl2.TabIndex = 11
        '
        ' MB_删除所选元数据
        '
        MB_删除所选元数据.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_删除所选元数据.BorderRadius = 10
        MB_删除所选元数据.BorderSize = 0
        MB_删除所选元数据.Dock = DockStyle.Left
        MB_删除所选元数据.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_删除所选元数据.Location = New Point(210, 10)
        MB_删除所选元数据.Margin = New Padding(2)
        MB_删除所选元数据.Name = "MB_删除所选元数据"
        MB_删除所选元数据.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_删除所选元数据.Size = New Size(100, 32)
        MB_删除所选元数据.TabIndex = 7
        MB_删除所选元数据.Text = "删除所选"
        '
        ' JustEmptyControl1
        '
        JustEmptyControl1.Dock = DockStyle.Left
        JustEmptyControl1.Location = New Point(200, 10)
        JustEmptyControl1.Name = "JustEmptyControl1"
        JustEmptyControl1.Size = New Size(10, 32)
        JustEmptyControl1.TabIndex = 2
        '
        ' MCB_添加元数据预制项
        '
        MCB_添加元数据预制项.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_添加元数据预制项.BorderRadius = 10
        MCB_添加元数据预制项.BorderSize = 0
        MCB_添加元数据预制项.Dock = DockStyle.Left
        MCB_添加元数据预制项.DropDownBackdropBlurPasses = 2
        MCB_添加元数据预制项.DropDownBackdropBlurRadius = 30
        MCB_添加元数据预制项.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_添加元数据预制项.DropDownHoverColor = Color.FromArgb(CByte(20), CByte(220), CByte(220), CByte(220))
        MCB_添加元数据预制项.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_添加元数据预制项.DropDownPadding = New Padding(10)
        MCB_添加元数据预制项.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_添加元数据预制项.DropDownSelectedForeColor = Color.White
        MCB_添加元数据预制项.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_添加元数据预制项.Items.Add("标题")
        MCB_添加元数据预制项.Items.Add("参与创作的艺术家")
        MCB_添加元数据预制项.Items.Add("专辑")
        MCB_添加元数据预制项.Items.Add("流派")
        MCB_添加元数据预制项.Items.Add("曲目编号")
        MCB_添加元数据预制项.Items.Add("碟片编号")
        MCB_添加元数据预制项.Items.Add("日期")
        MCB_添加元数据预制项.Items.Add("版权")
        MCB_添加元数据预制项.Items.Add("备注")
        MCB_添加元数据预制项.Items.Add("描述")
        MCB_添加元数据预制项.Items.Add("编码器")
        MCB_添加元数据预制项.Items.Add("软件")
        MCB_添加元数据预制项.Location = New Point(0, 10)
        MCB_添加元数据预制项.Margin = New Padding(2, 2, 2, 2)
        MCB_添加元数据预制项.MaxDropDownItems = 15
        MCB_添加元数据预制项.Name = "MCB_添加元数据预制项"
        MCB_添加元数据预制项.Padding = New Padding(10, 0, 10, 0)
        MCB_添加元数据预制项.Size = New Size(200, 32)
        MCB_添加元数据预制项.TabIndex = 10
        MCB_添加元数据预制项.ToolTipGap = -1
        MCB_添加元数据预制项.ToolTipMaxWidth = 350
        MCB_添加元数据预制项.ToolTipPadding = New Padding(15)
        MCB_添加元数据预制项.WaterText = "添加预制项"
        MCB_添加元数据预制项.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        '
        ' HCL_元数据说明
        '
        HCL_元数据说明.AutoSize = True
        HCL_元数据说明.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_元数据说明.Dock = DockStyle.Top
        HCL_元数据说明.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_元数据说明.Location = New Point(20, 20)
        HCL_元数据说明.Margin = New Padding(2)
        HCL_元数据说明.Name = "HCL_元数据说明"
        HCL_元数据说明.Padding = New Padding(0, 0, 0, 5)
        HCL_元数据说明.Size = New Size(824, 30)
        HCL_元数据说明.TabIndex = 10
        HCL_元数据说明.Text = "<span style=""font-size:13; color:Silver"">元数据</span>   向输出文件中写入自定义元数据，流的元数据请写自定义参数"
        '
        ' Form_v6_参数面板_元数据
        '
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ClientSize = New Size(864, 643)
        Controls.Add(ModernPanel1)
        Font = New Font("Microsoft YaHei UI", 10F)
        ForeColor = Color.Silver
        Name = "Form_v6_参数面板_元数据"
        Text = "Form_v6_参数面板_元数据"
        ModernPanel1.ResumeLayout(False)
        ModernPanel1.PerformLayout()
        Panel1.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents ModernPanel1 As LakeUI.ModernPanel
    Friend WithEvents UDLV_元数据列表 As LakeUI.UltraDetailListView
    Friend WithEvents Panel1 As Panel
    Friend WithEvents MB_删除所选元数据 As LakeUI.ModernButton
    Friend WithEvents JustEmptyControl1 As LakeUI.JustEmptyControl
    Friend WithEvents MCB_添加元数据预制项 As LakeUI.ModernComboBox
    Friend WithEvents HCL_元数据说明 As LakeUI.HtmlColorLabel
    Friend WithEvents MB_清空全部元数据 As LakeUI.ModernButton
    Friend WithEvents JustEmptyControl2 As LakeUI.JustEmptyControl
    Friend WithEvents MB_导入元数据 As LakeUI.ModernButton
    Friend WithEvents JustEmptyControl4 As LakeUI.JustEmptyControl
    Friend WithEvents MB_导出元数据 As LakeUI.ModernButton
    Friend WithEvents JustEmptyControl3 As LakeUI.JustEmptyControl
End Class
