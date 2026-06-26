<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_v6_准备文件
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
        ModernPanel1 = New LakeUI.ModernPanel()
        UltraDetailListView1 = New LakeUI.UltraDetailListView()
        HtmlColorLabel1 = New LakeUI.HtmlColorLabel()
        Panel1 = New Panel()
        MCB_排序 = New LakeUI.ModernComboBox()
        JustEmptyControl5 = New LakeUI.JustEmptyControl()
        MB_移除全部 = New LakeUI.ModernButton()
        JustEmptyControl4 = New LakeUI.JustEmptyControl()
        MB_移除选中 = New LakeUI.ModernButton()
        JustEmptyControl3 = New LakeUI.JustEmptyControl()
        MB_添加文件夹及子目录 = New LakeUI.ModernButton()
        JustEmptyControl2 = New LakeUI.JustEmptyControl()
        MB_添加文件 = New LakeUI.ModernButton()
        JustEmptyControl1 = New LakeUI.JustEmptyControl()
        MB_加入编码队列 = New LakeUI.ModernButton()
        ModernPanel1.SuspendLayout()
        Panel1.SuspendLayout()
        SuspendLayout()
        '
        ' ModernPanel1
        '
        ModernPanel1.BorderSize = 0
        ModernPanel1.Controls.Add(UltraDetailListView1)
        ModernPanel1.Controls.Add(HtmlColorLabel1)
        ModernPanel1.Controls.Add(Panel1)
        ModernPanel1.Dock = DockStyle.Fill
        ModernPanel1.Location = New Point(0, 0)
        ModernPanel1.Name = "ModernPanel1"
        ModernPanel1.Padding = New Padding(20)
        ModernPanel1.Size = New Size(897, 647)
        ModernPanel1.TabIndex = 0
        '
        ' UltraDetailListView1
        '
        UltraDetailListView1.AllowDragReorder = True
        UltraDetailListView1.BackgroundColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        UltraDetailListView1.BorderRadius = 10
        UltraDetailListView1.BorderSize = 0
        ListColumn1.Text = "文件名"
        ListColumn1.Width = 300
        ListColumn2.Text = "路径"
        ListColumn3.Text = "大写后缀"
        ListColumn4.Text = "大小"
        UltraDetailListView1.Columns.Add(ListColumn1)
        UltraDetailListView1.Columns.Add(ListColumn2)
        UltraDetailListView1.Columns.Add(ListColumn3)
        UltraDetailListView1.Columns.Add(ListColumn4)
        UltraDetailListView1.Dock = DockStyle.Fill
        UltraDetailListView1.GroupBackColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UltraDetailListView1.GroupForeColor = Color.Gainsboro
        UltraDetailListView1.GroupHeight = 40
        UltraDetailListView1.HeaderBackColor = Color.Transparent
        UltraDetailListView1.HeaderBorderColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        UltraDetailListView1.HeaderBorderWidth = 2
        UltraDetailListView1.HeaderForeColor = Color.DarkGray
        UltraDetailListView1.HeaderHeight = 36
        UltraDetailListView1.ItemCornerRadius = 10
        UltraDetailListView1.ItemPadding = New Padding(10, 5, 5, 5)
        UltraDetailListView1.ItemSelectedBackColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        UltraDetailListView1.Location = New Point(20, 60)
        UltraDetailListView1.Margin = New Padding(2, 2, 2, 2)
        UltraDetailListView1.Name = "UltraDetailListView1"
        UltraDetailListView1.SelectionRectBorderColor = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        UltraDetailListView1.SelectionRectFillColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        UltraDetailListView1.Size = New Size(857, 536)
        UltraDetailListView1.TabIndex = 3
        '
        ' HtmlColorLabel1
        '
        HtmlColorLabel1.AutoSize = True
        HtmlColorLabel1.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel1.Dock = DockStyle.Bottom
        HtmlColorLabel1.Location = New Point(20, 596)
        HtmlColorLabel1.Margin = New Padding(2)
        HtmlColorLabel1.Name = "HtmlColorLabel1"
        HtmlColorLabel1.Padding = New Padding(0, 10, 0, 0)
        HtmlColorLabel1.Size = New Size(857, 31)
        HtmlColorLabel1.TabIndex = 2
        HtmlColorLabel1.Text = "可以直接把文件拖进编码队列来开始，如果文件很多或者有其他需求再用这个页面"
        '
        ' Panel1
        '
        Panel1.AllowDrop = True
        Panel1.Controls.Add(MCB_排序)
        Panel1.Controls.Add(JustEmptyControl5)
        Panel1.Controls.Add(MB_移除全部)
        Panel1.Controls.Add(JustEmptyControl4)
        Panel1.Controls.Add(MB_移除选中)
        Panel1.Controls.Add(JustEmptyControl3)
        Panel1.Controls.Add(MB_添加文件夹及子目录)
        Panel1.Controls.Add(JustEmptyControl2)
        Panel1.Controls.Add(MB_添加文件)
        Panel1.Controls.Add(JustEmptyControl1)
        Panel1.Controls.Add(MB_加入编码队列)
        Panel1.Dock = DockStyle.Top
        Panel1.Location = New Point(20, 20)
        Panel1.Name = "Panel1"
        Panel1.Padding = New Padding(0, 0, 0, 10)
        Panel1.Size = New Size(857, 40)
        Panel1.TabIndex = 1
        '
        ' MCB_排序
        '
        MCB_排序.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_排序.BorderRadius = 10
        MCB_排序.BorderSize = 0
        MCB_排序.Dock = DockStyle.Fill
        MCB_排序.DropDownBackdropBlurPasses = 2
        MCB_排序.DropDownBackdropBlurRadius = 30
        MCB_排序.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_排序.DropDownHoverAnimationDuration = 0
        MCB_排序.DropDownHoverColor = Color.FromArgb(CByte(80), CByte(0), CByte(0), CByte(0))
        MCB_排序.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_排序.DropDownPadding = New Padding(10)
        MCB_排序.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_排序.DropDownSelectedForeColor = Color.White
        MCB_排序.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_排序.Items.Add("列表视图的升序")
        MCB_排序.Items.Add("列表视图的降序")
        MCB_排序.Items.Add("按照文件大小升序")
        MCB_排序.Items.Add("按照文件大小降序")
        MCB_排序.Location = New Point(650, 0)
        MCB_排序.Margin = New Padding(2, 2, 2, 2)
        MCB_排序.Name = "MCB_排序"
        MCB_排序.Padding = New Padding(10, 0, 10, 0)
        MCB_排序.Size = New Size(207, 30)
        MCB_排序.TabIndex = 10
        MCB_排序.ToolTipGap = -1
        MCB_排序.ToolTipMaxWidth = 350
        MCB_排序.ToolTipPadding = New Padding(15)
        MCB_排序.WaterText = "排序"
        MCB_排序.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        '
        ' JustEmptyControl5
        '
        JustEmptyControl5.Dock = DockStyle.Left
        JustEmptyControl5.Location = New Point(640, 0)
        JustEmptyControl5.Name = "JustEmptyControl5"
        JustEmptyControl5.Size = New Size(10, 30)
        JustEmptyControl5.TabIndex = 9
        '
        ' MB_移除全部
        '
        MB_移除全部.AllowDrop = True
        MB_移除全部.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_移除全部.BorderRadius = 8
        MB_移除全部.BorderSize = 0
        MB_移除全部.Dock = DockStyle.Left
        MB_移除全部.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_移除全部.Location = New Point(540, 0)
        MB_移除全部.Margin = New Padding(2)
        MB_移除全部.Name = "MB_移除全部"
        MB_移除全部.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_移除全部.Size = New Size(100, 30)
        MB_移除全部.TabIndex = 8
        MB_移除全部.Text = "移除全部"
        '
        ' JustEmptyControl4
        '
        JustEmptyControl4.Dock = DockStyle.Left
        JustEmptyControl4.Location = New Point(530, 0)
        JustEmptyControl4.Name = "JustEmptyControl4"
        JustEmptyControl4.Size = New Size(10, 30)
        JustEmptyControl4.TabIndex = 7
        '
        ' MB_移除选中
        '
        MB_移除选中.AllowDrop = True
        MB_移除选中.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_移除选中.BorderRadius = 8
        MB_移除选中.BorderSize = 0
        MB_移除选中.Dock = DockStyle.Left
        MB_移除选中.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_移除选中.Location = New Point(430, 0)
        MB_移除选中.Margin = New Padding(2)
        MB_移除选中.Name = "MB_移除选中"
        MB_移除选中.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_移除选中.Size = New Size(100, 30)
        MB_移除选中.TabIndex = 6
        MB_移除选中.Text = "移除选中"
        '
        ' JustEmptyControl3
        '
        JustEmptyControl3.Dock = DockStyle.Left
        JustEmptyControl3.Location = New Point(420, 0)
        JustEmptyControl3.Name = "JustEmptyControl3"
        JustEmptyControl3.Size = New Size(10, 30)
        JustEmptyControl3.TabIndex = 5
        '
        ' MB_添加文件夹及子目录
        '
        MB_添加文件夹及子目录.AllowDrop = True
        MB_添加文件夹及子目录.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_添加文件夹及子目录.BorderRadius = 8
        MB_添加文件夹及子目录.BorderSize = 0
        MB_添加文件夹及子目录.Dock = DockStyle.Left
        MB_添加文件夹及子目录.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_添加文件夹及子目录.Location = New Point(250, 0)
        MB_添加文件夹及子目录.Margin = New Padding(2)
        MB_添加文件夹及子目录.Name = "MB_添加文件夹及子目录"
        MB_添加文件夹及子目录.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_添加文件夹及子目录.Size = New Size(170, 30)
        MB_添加文件夹及子目录.TabIndex = 4
        MB_添加文件夹及子目录.Text = "添加文件夹及子目录"
        '
        ' JustEmptyControl2
        '
        JustEmptyControl2.Dock = DockStyle.Left
        JustEmptyControl2.Location = New Point(240, 0)
        JustEmptyControl2.Name = "JustEmptyControl2"
        JustEmptyControl2.Size = New Size(10, 30)
        JustEmptyControl2.TabIndex = 3
        '
        ' MB_添加文件
        '
        MB_添加文件.AllowDrop = True
        MB_添加文件.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_添加文件.BorderRadius = 8
        MB_添加文件.BorderSize = 0
        MB_添加文件.Dock = DockStyle.Left
        MB_添加文件.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_添加文件.Location = New Point(140, 0)
        MB_添加文件.Margin = New Padding(2)
        MB_添加文件.Name = "MB_添加文件"
        MB_添加文件.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_添加文件.Size = New Size(100, 30)
        MB_添加文件.TabIndex = 2
        MB_添加文件.Text = "添加文件"
        '
        ' JustEmptyControl1
        '
        JustEmptyControl1.Dock = DockStyle.Left
        JustEmptyControl1.Location = New Point(130, 0)
        JustEmptyControl1.Name = "JustEmptyControl1"
        JustEmptyControl1.Size = New Size(10, 30)
        JustEmptyControl1.TabIndex = 1
        '
        ' MB_加入编码队列
        '
        MB_加入编码队列.AllowDrop = True
        MB_加入编码队列.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_加入编码队列.BorderRadius = 8
        MB_加入编码队列.BorderSize = 0
        MB_加入编码队列.Dock = DockStyle.Left
        MB_加入编码队列.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_加入编码队列.Location = New Point(0, 0)
        MB_加入编码队列.Margin = New Padding(2)
        MB_加入编码队列.Name = "MB_加入编码队列"
        MB_加入编码队列.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_加入编码队列.Size = New Size(130, 30)
        MB_加入编码队列.TabIndex = 0
        MB_加入编码队列.Text = "加入编码队列"
        '
        ' Form_v6_准备文件
        '
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        ClientSize = New Size(897, 647)
        Controls.Add(ModernPanel1)
        Font = New Font("Microsoft YaHei UI", 10F)
        ForeColor = Color.Silver
        Name = "Form_v6_准备文件"
        Text = "Form_v6_准备文件"
        ModernPanel1.ResumeLayout(False)
        ModernPanel1.PerformLayout()
        Panel1.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents ModernPanel1 As LakeUI.ModernPanel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents MB_添加文件夹及子目录 As LakeUI.ModernButton
    Friend WithEvents JustEmptyControl2 As LakeUI.JustEmptyControl
    Friend WithEvents MB_添加文件 As LakeUI.ModernButton
    Friend WithEvents JustEmptyControl1 As LakeUI.JustEmptyControl
    Friend WithEvents MB_加入编码队列 As LakeUI.ModernButton
    Friend WithEvents JustEmptyControl5 As LakeUI.JustEmptyControl
    Friend WithEvents MB_移除全部 As LakeUI.ModernButton
    Friend WithEvents JustEmptyControl4 As LakeUI.JustEmptyControl
    Friend WithEvents MB_移除选中 As LakeUI.ModernButton
    Friend WithEvents JustEmptyControl3 As LakeUI.JustEmptyControl
    Friend WithEvents MCB_排序 As LakeUI.ModernComboBox
    Friend WithEvents HtmlColorLabel1 As LakeUI.HtmlColorLabel
    Friend WithEvents UltraDetailListView1 As LakeUI.UltraDetailListView
End Class
