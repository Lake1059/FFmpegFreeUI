<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form_v6_社区_浏览内容
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim ListColumn1 As LakeUI.UltraDetailListView.ListColumn = New LakeUI.UltraDetailListView.ListColumn()
        Dim ListColumn2 As LakeUI.UltraDetailListView.ListColumn = New LakeUI.UltraDetailListView.ListColumn()
        Dim ListColumn3 As LakeUI.UltraDetailListView.ListColumn = New LakeUI.UltraDetailListView.ListColumn()
        Dim ListColumn4 As LakeUI.UltraDetailListView.ListColumn = New LakeUI.UltraDetailListView.ListColumn()
        ModernPanel1 = New LakeUI.ModernPanel()
        UDLV_元数据列表 = New LakeUI.UltraDetailListView()
        Panel1 = New Panel()
        MTB_搜索 = New LakeUI.ModernTextBox()
        JustEmptyControl4 = New LakeUI.JustEmptyControl()
        JustEmptyControl1 = New LakeUI.JustEmptyControl()
        MB_ = New LakeUI.ModernButton()
        ModernButton2 = New LakeUI.ModernButton()
        JustEmptyControl2 = New LakeUI.JustEmptyControl()
        ModernComboBox1 = New LakeUI.ModernComboBox()
        JustEmptyControl3 = New LakeUI.JustEmptyControl()
        ModernButton1 = New LakeUI.ModernButton()
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
        ModernPanel1.Dock = DockStyle.Fill
        ModernPanel1.Location = New Point(0, 0)
        ModernPanel1.Name = "ModernPanel1"
        ModernPanel1.Padding = New Padding(20)
        ModernPanel1.Size = New Size(802, 595)
        ModernPanel1.TabIndex = 0
        ' 
        ' UDLV_元数据列表
        ' 
        UDLV_元数据列表.AllowDragReorder = True
        UDLV_元数据列表.BackgroundColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        UDLV_元数据列表.BorderRadius = 10
        UDLV_元数据列表.BorderSize = 0
        ListColumn1.Text = "标题"
        ListColumn1.Width = 300
        ListColumn2.Text = "发布者"
        ListColumn3.Text = "获赞数"
        ListColumn4.Text = "文件大小"
        UDLV_元数据列表.Columns.Add(ListColumn1)
        UDLV_元数据列表.Columns.Add(ListColumn2)
        UDLV_元数据列表.Columns.Add(ListColumn3)
        UDLV_元数据列表.Columns.Add(ListColumn4)
        UDLV_元数据列表.Dock = DockStyle.Fill
        UDLV_元数据列表.DragSelectZoneWidth = 100
        UDLV_元数据列表.GroupBorderColor = Color.Silver
        UDLV_元数据列表.GroupHeight = 35
        UDLV_元数据列表.HeaderBackColor = Color.Transparent
        UDLV_元数据列表.HeaderBorderColor = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        UDLV_元数据列表.HeaderHeight = 40
        UDLV_元数据列表.ItemPadding = New Padding(10, 6, 10, 6)
        UDLV_元数据列表.ItemSelectedBackColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        UDLV_元数据列表.Location = New Point(20, 62)
        UDLV_元数据列表.Margin = New Padding(2, 2, 2, 2)
        UDLV_元数据列表.Name = "UDLV_元数据列表"
        UDLV_元数据列表.Size = New Size(762, 513)
        UDLV_元数据列表.TabIndex = 4
        ' 
        ' Panel1
        ' 
        Panel1.Controls.Add(MTB_搜索)
        Panel1.Controls.Add(JustEmptyControl4)
        Panel1.Controls.Add(JustEmptyControl1)
        Panel1.Controls.Add(MB_)
        Panel1.Controls.Add(ModernButton2)
        Panel1.Controls.Add(JustEmptyControl2)
        Panel1.Controls.Add(ModernComboBox1)
        Panel1.Controls.Add(JustEmptyControl3)
        Panel1.Controls.Add(ModernButton1)
        Panel1.Dock = DockStyle.Top
        Panel1.Location = New Point(20, 20)
        Panel1.Name = "Panel1"
        Panel1.Padding = New Padding(0, 0, 0, 10)
        Panel1.Size = New Size(762, 42)
        Panel1.TabIndex = 3
        ' 
        ' MTB_搜索
        ' 
        MTB_搜索.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_搜索.BorderColor = Color.Transparent
        MTB_搜索.BorderColorFocus = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MTB_搜索.BorderRadius = 10
        MTB_搜索.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        MTB_搜索.Dock = DockStyle.Fill
        MTB_搜索.Location = New Point(290, 0)
        MTB_搜索.Margin = New Padding(2)
        MTB_搜索.Name = "MTB_搜索"
        MTB_搜索.Padding = New Padding(10, 0, 10, 0)
        MTB_搜索.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_搜索.Size = New Size(222, 32)
        MTB_搜索.TabIndex = 8
        MTB_搜索.WaterText = "在当前类型中搜索标题"
        MTB_搜索.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' JustEmptyControl4
        ' 
        JustEmptyControl4.Dock = DockStyle.Right
        JustEmptyControl4.Location = New Point(512, 0)
        JustEmptyControl4.Name = "JustEmptyControl4"
        JustEmptyControl4.Size = New Size(10, 32)
        JustEmptyControl4.TabIndex = 9
        ' 
        ' JustEmptyControl1
        ' 
        JustEmptyControl1.Dock = DockStyle.Left
        JustEmptyControl1.Location = New Point(280, 0)
        JustEmptyControl1.Name = "JustEmptyControl1"
        JustEmptyControl1.Size = New Size(10, 32)
        JustEmptyControl1.TabIndex = 2
        ' 
        ' MB_
        ' 
        MB_.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_.BorderRadius = 10
        MB_.BorderSize = 0
        MB_.Dock = DockStyle.Left
        MB_.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_.Location = New Point(210, 0)
        MB_.Margin = New Padding(2)
        MB_.Name = "MB_"
        MB_.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_.Size = New Size(70, 32)
        MB_.TabIndex = 0
        MB_.Text = "刷新"
        ' 
        ' ModernButton2
        ' 
        ModernButton2.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernButton2.BorderRadius = 10
        ModernButton2.BorderSize = 0
        ModernButton2.Dock = DockStyle.Right
        ModernButton2.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        ModernButton2.Location = New Point(522, 0)
        ModernButton2.Margin = New Padding(2)
        ModernButton2.Name = "ModernButton2"
        ModernButton2.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        ModernButton2.Size = New Size(70, 32)
        ModernButton2.TabIndex = 5
        ModernButton2.Text = "搜索"
        ' 
        ' JustEmptyControl2
        ' 
        JustEmptyControl2.Dock = DockStyle.Right
        JustEmptyControl2.Location = New Point(592, 0)
        JustEmptyControl2.Name = "JustEmptyControl2"
        JustEmptyControl2.Size = New Size(10, 32)
        JustEmptyControl2.TabIndex = 11
        ' 
        ' ModernComboBox1
        ' 
        ModernComboBox1.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernComboBox1.BorderRadius = 10
        ModernComboBox1.BorderSize = 0
        ModernComboBox1.Dock = DockStyle.Right
        ModernComboBox1.DropDownBackdropBlurPasses = 2
        ModernComboBox1.DropDownBackdropBlurRadius = 30
        ModernComboBox1.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        ModernComboBox1.DropDownHoverColor = Color.FromArgb(CByte(20), CByte(220), CByte(220), CByte(220))
        ModernComboBox1.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        ModernComboBox1.DropDownPadding = New Padding(10)
        ModernComboBox1.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernComboBox1.DropDownSelectedForeColor = Color.White
        ModernComboBox1.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        ModernComboBox1.Items.Add("最新时间顺序")
        ModernComboBox1.Items.Add("最多赞数顺序")
        ModernComboBox1.Items.Add("文件从大到小")
        ModernComboBox1.Items.Add("文件从小到大")
        ModernComboBox1.Location = New Point(602, 0)
        ModernComboBox1.Margin = New Padding(2, 2, 2, 2)
        ModernComboBox1.Name = "ModernComboBox1"
        ModernComboBox1.Padding = New Padding(10, 0, 10, 0)
        ModernComboBox1.Size = New Size(160, 32)
        ModernComboBox1.TabIndex = 10
        ModernComboBox1.ToolTipGap = -1
        ModernComboBox1.ToolTipMaxWidth = 350
        ModernComboBox1.ToolTipPadding = New Padding(15)
        ModernComboBox1.WaterText = "排序方式"
        ModernComboBox1.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' JustEmptyControl3
        ' 
        JustEmptyControl3.Dock = DockStyle.Left
        JustEmptyControl3.Location = New Point(200, 0)
        JustEmptyControl3.Name = "JustEmptyControl3"
        JustEmptyControl3.Size = New Size(10, 32)
        JustEmptyControl3.TabIndex = 7
        ' 
        ' ModernButton1
        ' 
        ModernButton1.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernButton1.BorderRadius = 10
        ModernButton1.BorderSize = 0
        ModernButton1.Dock = DockStyle.Left
        ModernButton1.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        ModernButton1.Location = New Point(0, 0)
        ModernButton1.Margin = New Padding(2)
        ModernButton1.Name = "ModernButton1"
        ModernButton1.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        ModernButton1.Size = New Size(200, 32)
        ModernButton1.TabIndex = 12
        ModernButton1.Text = "点击切换要查看的内容"
        ModernButton1.TextAlign = LakeUI.ModernButton.TextAlignEnum.Left
        ' 
        ' Form_v6_社区_浏览内容
        ' 
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ClientSize = New Size(802, 595)
        Controls.Add(ModernPanel1)
        Font = New Font("Microsoft YaHei UI", 10F)
        ForeColor = Color.Silver
        Name = "Form_v6_社区_浏览内容"
        Text = "Form_v6_社区_管理内容"
        ModernPanel1.ResumeLayout(False)
        Panel1.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents ModernPanel1 As LakeUI.ModernPanel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ModernButton2 As LakeUI.ModernButton
    Friend WithEvents JustEmptyControl1 As LakeUI.JustEmptyControl
    Friend WithEvents MB_ As LakeUI.ModernButton
    Friend WithEvents JustEmptyControl3 As LakeUI.JustEmptyControl
    Friend WithEvents JustEmptyControl4 As LakeUI.JustEmptyControl
    Friend WithEvents MTB_搜索 As LakeUI.ModernTextBox
    Friend WithEvents UDLV_元数据列表 As LakeUI.UltraDetailListView
    Friend WithEvents JustEmptyControl2 As LakeUI.JustEmptyControl
    Friend WithEvents ModernComboBox1 As LakeUI.ModernComboBox
    Friend WithEvents ModernButton1 As LakeUI.ModernButton
End Class
