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
        UltraDetailListView1 = New LakeUI.UltraDetailListView()
        Panel1 = New Panel()
        ModernButton3 = New LakeUI.ModernButton()
        JustEmptyControl4 = New LakeUI.JustEmptyControl()
        ModernButton2 = New LakeUI.ModernButton()
        JustEmptyControl3 = New LakeUI.JustEmptyControl()
        ModernButton1 = New LakeUI.ModernButton()
        JustEmptyControl2 = New LakeUI.JustEmptyControl()
        ModernButton4 = New LakeUI.ModernButton()
        JustEmptyControl1 = New LakeUI.JustEmptyControl()
        ModernComboBox1 = New LakeUI.ModernComboBox()
        HtmlColorLabel1 = New LakeUI.HtmlColorLabel()
        ModernPanel1.SuspendLayout()
        Panel1.SuspendLayout()
        SuspendLayout()
        ' 
        ' ModernPanel1
        ' 
        ModernPanel1.BackColor1 = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ModernPanel1.BorderSize = 0
        ModernPanel1.Controls.Add(UltraDetailListView1)
        ModernPanel1.Controls.Add(Panel1)
        ModernPanel1.Controls.Add(HtmlColorLabel1)
        ModernPanel1.Dock = DockStyle.Fill
        ModernPanel1.Location = New Point(0, 0)
        ModernPanel1.Name = "ModernPanel1"
        ModernPanel1.Padding = New Padding(20)
        ModernPanel1.ScrollBarMode = LakeUI.ModernPanel.ScrollMode.Vertical
        ModernPanel1.Size = New Size(864, 643)
        ModernPanel1.TabIndex = 1
        ' 
        ' UltraDetailListView1
        ' 
        UltraDetailListView1.AllowDragReorder = True
        UltraDetailListView1.BackgroundColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        UltraDetailListView1.BorderRadius = 10
        UltraDetailListView1.BorderSize = 0
        ListColumn1.Text = "字段"
        ListColumn1.Width = 150
        ListColumn2.AllowLabelEdit = True
        ListColumn2.Text = "值"
        ListColumn2.Width = 500
        UltraDetailListView1.Columns.Add(ListColumn1)
        UltraDetailListView1.Columns.Add(ListColumn2)
        UltraDetailListView1.Dock = DockStyle.Fill
        UltraDetailListView1.DragSelectZoneWidth = 100
        UltraDetailListView1.GroupBorderColor = Color.Silver
        UltraDetailListView1.GroupHeight = 35
        UltraDetailListView1.HeaderBackColor = Color.Transparent
        UltraDetailListView1.HeaderBorderColor = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        UltraDetailListView1.HeaderHeight = 40
        UltraDetailListView1.ItemPadding = New Padding(10, 6, 10, 6)
        UltraDetailListView1.Location = New Point(20, 102)
        UltraDetailListView1.Margin = New Padding(2, 2, 2, 2)
        UltraDetailListView1.Name = "UltraDetailListView1"
        UltraDetailListView1.Size = New Size(824, 521)
        UltraDetailListView1.TabIndex = 1
        ' 
        ' Panel1
        ' 
        Panel1.Controls.Add(ModernButton3)
        Panel1.Controls.Add(JustEmptyControl4)
        Panel1.Controls.Add(ModernButton2)
        Panel1.Controls.Add(JustEmptyControl3)
        Panel1.Controls.Add(ModernButton1)
        Panel1.Controls.Add(JustEmptyControl2)
        Panel1.Controls.Add(ModernButton4)
        Panel1.Controls.Add(JustEmptyControl1)
        Panel1.Controls.Add(ModernComboBox1)
        Panel1.Dock = DockStyle.Top
        Panel1.Location = New Point(20, 50)
        Panel1.Name = "Panel1"
        Panel1.Padding = New Padding(0, 10, 0, 10)
        Panel1.Size = New Size(824, 52)
        Panel1.TabIndex = 11
        ' 
        ' ModernButton3
        ' 
        ModernButton3.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernButton3.BorderRadius = 10
        ModernButton3.BorderSize = 0
        ModernButton3.Dock = DockStyle.Left
        ModernButton3.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        ModernButton3.Location = New Point(510, 10)
        ModernButton3.Margin = New Padding(2)
        ModernButton3.Name = "ModernButton3"
        ModernButton3.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        ModernButton3.Size = New Size(70, 32)
        ModernButton3.TabIndex = 16
        ModernButton3.Text = "导入"
        ' 
        ' JustEmptyControl4
        ' 
        JustEmptyControl4.Dock = DockStyle.Left
        JustEmptyControl4.Location = New Point(500, 10)
        JustEmptyControl4.Name = "JustEmptyControl4"
        JustEmptyControl4.Size = New Size(10, 32)
        JustEmptyControl4.TabIndex = 15
        ' 
        ' ModernButton2
        ' 
        ModernButton2.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernButton2.BorderRadius = 10
        ModernButton2.BorderSize = 0
        ModernButton2.Dock = DockStyle.Left
        ModernButton2.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        ModernButton2.Location = New Point(430, 10)
        ModernButton2.Margin = New Padding(2)
        ModernButton2.Name = "ModernButton2"
        ModernButton2.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        ModernButton2.Size = New Size(70, 32)
        ModernButton2.TabIndex = 14
        ModernButton2.Text = "导出"
        ' 
        ' JustEmptyControl3
        ' 
        JustEmptyControl3.Dock = DockStyle.Left
        JustEmptyControl3.Location = New Point(420, 10)
        JustEmptyControl3.Name = "JustEmptyControl3"
        JustEmptyControl3.Size = New Size(10, 32)
        JustEmptyControl3.TabIndex = 13
        ' 
        ' ModernButton1
        ' 
        ModernButton1.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernButton1.BorderRadius = 10
        ModernButton1.BorderSize = 0
        ModernButton1.Dock = DockStyle.Left
        ModernButton1.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        ModernButton1.Location = New Point(320, 10)
        ModernButton1.Margin = New Padding(2)
        ModernButton1.Name = "ModernButton1"
        ModernButton1.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        ModernButton1.Size = New Size(100, 32)
        ModernButton1.TabIndex = 12
        ModernButton1.Text = "全部清空"
        ' 
        ' JustEmptyControl2
        ' 
        JustEmptyControl2.Dock = DockStyle.Left
        JustEmptyControl2.Location = New Point(310, 10)
        JustEmptyControl2.Name = "JustEmptyControl2"
        JustEmptyControl2.Size = New Size(10, 32)
        JustEmptyControl2.TabIndex = 11
        ' 
        ' ModernButton4
        ' 
        ModernButton4.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernButton4.BorderRadius = 10
        ModernButton4.BorderSize = 0
        ModernButton4.Dock = DockStyle.Left
        ModernButton4.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        ModernButton4.Location = New Point(210, 10)
        ModernButton4.Margin = New Padding(2)
        ModernButton4.Name = "ModernButton4"
        ModernButton4.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        ModernButton4.Size = New Size(100, 32)
        ModernButton4.TabIndex = 7
        ModernButton4.Text = "删除所选"
        ' 
        ' JustEmptyControl1
        ' 
        JustEmptyControl1.Dock = DockStyle.Left
        JustEmptyControl1.Location = New Point(200, 10)
        JustEmptyControl1.Name = "JustEmptyControl1"
        JustEmptyControl1.Size = New Size(10, 32)
        JustEmptyControl1.TabIndex = 2
        ' 
        ' ModernComboBox1
        ' 
        ModernComboBox1.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernComboBox1.BorderRadius = 10
        ModernComboBox1.BorderSize = 0
        ModernComboBox1.Dock = DockStyle.Left
        ModernComboBox1.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        ModernComboBox1.DropDownHoverAnimationDuration = 0
        ModernComboBox1.DropDownHoverColor = Color.FromArgb(CByte(80), CByte(0), CByte(0), CByte(0))
        ModernComboBox1.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        ModernComboBox1.DropDownPadding = New Padding(10)
        ModernComboBox1.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernComboBox1.DropDownSelectedForeColor = Color.White
        ModernComboBox1.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        ModernComboBox1.Margin = New Padding(2, 2, 2, 2)
        ModernComboBox1.Padding = New Padding(10, 0, 10, 0)
        ModernComboBox1.ToolTipGap = -1
        ModernComboBox1.ToolTipMaxWidth = 350
        ModernComboBox1.ToolTipPadding = New Padding(15)
        ModernComboBox1.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ModernComboBox1.Items.Add("标题")
        ModernComboBox1.Items.Add("参与创作的艺术家")
        ModernComboBox1.Items.Add("专辑")
        ModernComboBox1.Items.Add("流派")
        ModernComboBox1.Items.Add("曲目编号")
        ModernComboBox1.Items.Add("碟片编号")
        ModernComboBox1.Items.Add("日期")
        ModernComboBox1.Items.Add("版权")
        ModernComboBox1.Items.Add("备注")
        ModernComboBox1.Items.Add("描述")
        ModernComboBox1.Items.Add("编码器")
        ModernComboBox1.Items.Add("软件")
        ModernComboBox1.Location = New Point(0, 10)
        ModernComboBox1.MaxDropDownItems = 15
        ModernComboBox1.Name = "ModernComboBox1"
        ModernComboBox1.Size = New Size(200, 32)
        ModernComboBox1.TabIndex = 10
        ModernComboBox1.WaterText = "添加预制项"
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
        HtmlColorLabel1.Size = New Size(824, 30)
        HtmlColorLabel1.TabIndex = 10
        HtmlColorLabel1.Text = "<span style=""font-size:13; color:Silver"">元数据</span>   向输出文件中写入自定义元数据，流的元数据请写自定义参数"
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
    Friend WithEvents UltraDetailListView1 As LakeUI.UltraDetailListView
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ModernButton4 As LakeUI.ModernButton
    Friend WithEvents JustEmptyControl1 As LakeUI.JustEmptyControl
    Friend WithEvents ModernComboBox1 As LakeUI.ModernComboBox
    Friend WithEvents HtmlColorLabel1 As LakeUI.HtmlColorLabel
    Friend WithEvents ModernButton1 As LakeUI.ModernButton
    Friend WithEvents JustEmptyControl2 As LakeUI.JustEmptyControl
    Friend WithEvents ModernButton3 As LakeUI.ModernButton
    Friend WithEvents JustEmptyControl4 As LakeUI.JustEmptyControl
    Friend WithEvents ModernButton2 As LakeUI.ModernButton
    Friend WithEvents JustEmptyControl3 As LakeUI.JustEmptyControl
End Class
