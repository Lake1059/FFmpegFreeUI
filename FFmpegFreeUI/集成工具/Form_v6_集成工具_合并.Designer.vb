<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_v6_集成工具_合并
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
        Panel2.SuspendLayout()
        Panel1.SuspendLayout()
        SuspendLayout()
        '
        ' ModernPanel1
        '
        ModernPanel1.BackColor1 = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ModernPanel1.BorderSize = 0
        ModernPanel1.Controls.Add(UltraDetailListView1)
        ModernPanel1.Controls.Add(Panel2)
        ModernPanel1.Controls.Add(Panel1)
        ModernPanel1.Controls.Add(HtmlColorLabel1)
        ModernPanel1.Dock = DockStyle.Fill
        ModernPanel1.Location = New Point(0, 0)
        ModernPanel1.Name = "ModernPanel1"
        ModernPanel1.Padding = New Padding(20)
        ModernPanel1.Size = New Size(829, 625)
        ModernPanel1.TabIndex = 0
        '
        ' UltraDetailListView1
        '
        UltraDetailListView1.AllowDragReorder = True
        UltraDetailListView1.BackgroundColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        UltraDetailListView1.BorderRadius = 10
        UltraDetailListView1.BorderSize = 0
        ListColumn1.Text = "文件名"
        ListColumn1.Width = 200
        ListColumn2.Text = "完整路径"
        ListColumn2.Width = 450
        UltraDetailListView1.Columns.Add(ListColumn1)
        UltraDetailListView1.Columns.Add(ListColumn2)
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
        UltraDetailListView1.Location = New Point(20, 102)
        UltraDetailListView1.Margin = New Padding(2, 2, 2, 2)
        UltraDetailListView1.Name = "UltraDetailListView1"
        UltraDetailListView1.Padding = New Padding(5, 0, 5, 5)
        UltraDetailListView1.ScrollBarThumbColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        UltraDetailListView1.ScrollBarThumbHoverColor = Color.FromArgb(CByte(120), CByte(220), CByte(220), CByte(220))
        UltraDetailListView1.ScrollBarTrackColor = Color.FromArgb(CByte(20), CByte(220), CByte(220), CByte(220))
        UltraDetailListView1.SelectionRectBorderColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        UltraDetailListView1.SelectionRectFillColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        UltraDetailListView1.Size = New Size(789, 461)
        UltraDetailListView1.TabIndex = 4
        '
        ' Panel2
        '
        Panel2.Controls.Add(MTB_输出目标文件)
        Panel2.Controls.Add(JustEmptyControl5)
        Panel2.Controls.Add(JustEmptyControl6)
        Panel2.Controls.Add(MB_选择位置)
        Panel2.Controls.Add(MB_启动合并)
        Panel2.Dock = DockStyle.Bottom
        Panel2.Location = New Point(20, 563)
        Panel2.Name = "Panel2"
        Panel2.Padding = New Padding(0, 10, 0, 0)
        Panel2.Size = New Size(789, 42)
        Panel2.TabIndex = 3
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
        MTB_输出目标文件.Size = New Size(569, 32)
        MTB_输出目标文件.TabIndex = 3
        MTB_输出目标文件.WaterText = "输出目标文件路径"
        MTB_输出目标文件.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        '
        ' JustEmptyControl5
        '
        JustEmptyControl5.Dock = DockStyle.Right
        JustEmptyControl5.Location = New Point(679, 10)
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
        MB_启动合并.Location = New Point(689, 10)
        MB_启动合并.Margin = New Padding(2)
        MB_启动合并.Name = "MB_启动合并"
        MB_启动合并.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_启动合并.Size = New Size(100, 32)
        MB_启动合并.TabIndex = 5
        MB_启动合并.Text = "启动合并"
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
        Panel1.Location = New Point(20, 50)
        Panel1.Name = "Panel1"
        Panel1.Padding = New Padding(0, 10, 0, 10)
        Panel1.Size = New Size(789, 52)
        Panel1.TabIndex = 2
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
        HtmlColorLabel2.Size = New Size(329, 32)
        HtmlColorLabel2.TabIndex = 10
        HtmlColorLabel2.Text = "偏左侧拖动排序，偏右侧框选"
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
        HtmlColorLabel1.Padding = New Padding(0, 0, 0, 5)
        HtmlColorLabel1.Size = New Size(789, 30)
        HtmlColorLabel1.TabIndex = 1
        HtmlColorLabel1.Text = "<span style=""font-size:13; color:Silver"">多个文件按顺序头尾相接</span>   如果你的需求不是这个那么你可能指的是混流"
        '
        ' Form_v6_集成工具_合并
        '
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ClientSize = New Size(829, 625)
        Controls.Add(ModernPanel1)
        Font = New Font("Microsoft YaHei UI", 10F)
        ForeColor = Color.Silver
        Name = "Form_v6_集成工具_合并"
        Text = "Form_v6_集成工具_合并"
        ModernPanel1.ResumeLayout(False)
        ModernPanel1.PerformLayout()
        Panel2.ResumeLayout(False)
        Panel1.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents ModernPanel1 As LakeUI.ModernPanel
    Friend WithEvents HtmlColorLabel1 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents JustEmptyControl1 As LakeUI.JustEmptyControl
    Friend WithEvents MB_添加文件 As LakeUI.ModernButton
    Friend WithEvents MB_移除全部 As LakeUI.ModernButton
    Friend WithEvents JustEmptyControl4 As LakeUI.JustEmptyControl
    Friend WithEvents MB_移除选中 As LakeUI.ModernButton
    Friend WithEvents JustEmptyControl3 As LakeUI.JustEmptyControl
    Friend WithEvents MB_下移 As LakeUI.ModernButton
    Friend WithEvents JustEmptyControl2 As LakeUI.JustEmptyControl
    Friend WithEvents MB_上移 As LakeUI.ModernButton
    Friend WithEvents Panel2 As Panel
    Friend WithEvents MTB_输出目标文件 As LakeUI.ModernTextBox
    Friend WithEvents JustEmptyControl5 As LakeUI.JustEmptyControl
    Friend WithEvents JustEmptyControl6 As LakeUI.JustEmptyControl
    Friend WithEvents MB_选择位置 As LakeUI.ModernButton
    Friend WithEvents MB_启动合并 As LakeUI.ModernButton
    Friend WithEvents UltraDetailListView1 As LakeUI.UltraDetailListView
    Friend WithEvents HtmlColorLabel2 As LakeUI.HtmlColorLabel
End Class
