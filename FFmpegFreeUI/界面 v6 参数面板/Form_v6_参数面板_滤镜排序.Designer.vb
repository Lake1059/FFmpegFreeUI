<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_v6_参数面板_滤镜排序
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
        Dim ListColumn4 As LakeUI.UltraDetailListView.ListColumn = New LakeUI.UltraDetailListView.ListColumn()
        Dim ListColumn5 As LakeUI.UltraDetailListView.ListColumn = New LakeUI.UltraDetailListView.ListColumn()
        Dim ListColumn6 As LakeUI.UltraDetailListView.ListColumn = New LakeUI.UltraDetailListView.ListColumn()
        ModernPanel1 = New LakeUI.ModernPanel()
        UDLV_滤镜排序列表 = New LakeUI.UltraDetailListView()
        Panel1 = New Panel()
        MB_删除滤镜 = New LakeUI.ModernButton()
        JustEmptyControl2 = New LakeUI.JustEmptyControl()
        MB_添加自定义音频滤镜 = New LakeUI.ModernButton()
        JustEmptyControl1 = New LakeUI.JustEmptyControl()
        MB_添加自定义视频滤镜 = New LakeUI.ModernButton()
        HCL_滤镜排序说明 = New LakeUI.HtmlColorLabel()
        ModernPanel1.SuspendLayout()
        Panel1.SuspendLayout()
        SuspendLayout()
        ' 
        ' ModernPanel1
        ' 
        ModernPanel1.BackColor1 = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ModernPanel1.BorderSize = 0
        ModernPanel1.Controls.Add(UDLV_滤镜排序列表)
        ModernPanel1.Controls.Add(Panel1)
        ModernPanel1.Controls.Add(HCL_滤镜排序说明)
        ModernPanel1.Dock = DockStyle.Fill
        ModernPanel1.Location = New Point(0, 0)
        ModernPanel1.Name = "ModernPanel1"
        ModernPanel1.Padding = New Padding(20)
        ModernPanel1.ScrollBarMode = LakeUI.ModernPanel.ScrollMode.Vertical
        ModernPanel1.Size = New Size(795, 614)
        ModernPanel1.TabIndex = 0
        ' 
        ' UDLV_滤镜排序列表
        ' 
        UDLV_滤镜排序列表.AllowDragReorder = True
        UDLV_滤镜排序列表.BackgroundColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        UDLV_滤镜排序列表.BorderRadius = 10
        UDLV_滤镜排序列表.BorderSize = 0
        ListColumn4.Text = "标识符"
        ListColumn4.Width = 150
        ListColumn5.Text = "流类型"
        ListColumn6.Text = "滤镜内容"
        ListColumn6.Width = 450
        UDLV_滤镜排序列表.Columns.Add(ListColumn4)
        UDLV_滤镜排序列表.Columns.Add(ListColumn5)
        UDLV_滤镜排序列表.Columns.Add(ListColumn6)
        UDLV_滤镜排序列表.Dock = DockStyle.Fill
        UDLV_滤镜排序列表.DragSelectZoneWidth = 200
        UDLV_滤镜排序列表.GroupBorderColor = Color.Silver
        UDLV_滤镜排序列表.GroupHeight = 35
        UDLV_滤镜排序列表.HeaderBackColor = Color.Transparent
        UDLV_滤镜排序列表.HeaderBorderColor = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        UDLV_滤镜排序列表.HeaderHeight = 40
        UDLV_滤镜排序列表.ItemCornerRadius = 10
        UDLV_滤镜排序列表.ItemPadding = New Padding(10, 6, 10, 6)
        UDLV_滤镜排序列表.ItemSelectedBackColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        UDLV_滤镜排序列表.Location = New Point(20, 102)
        UDLV_滤镜排序列表.Margin = New Padding(2, 2, 2, 2)
        UDLV_滤镜排序列表.Name = "UDLV_滤镜排序列表"
        UDLV_滤镜排序列表.ScrollBarThumbColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        UDLV_滤镜排序列表.ScrollBarThumbHoverColor = Color.FromArgb(CByte(120), CByte(220), CByte(220), CByte(220))
        UDLV_滤镜排序列表.ScrollBarTrackColor = Color.FromArgb(CByte(20), CByte(220), CByte(220), CByte(220))
        UDLV_滤镜排序列表.SelectionRectBorderColor = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        UDLV_滤镜排序列表.SelectionRectFillColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        UDLV_滤镜排序列表.Size = New Size(755, 492)
        UDLV_滤镜排序列表.TabIndex = 1
        ' 
        ' Panel1
        ' 
        Panel1.Controls.Add(MB_删除滤镜)
        Panel1.Controls.Add(JustEmptyControl2)
        Panel1.Controls.Add(MB_添加自定义音频滤镜)
        Panel1.Controls.Add(JustEmptyControl1)
        Panel1.Controls.Add(MB_添加自定义视频滤镜)
        Panel1.Dock = DockStyle.Top
        Panel1.Location = New Point(20, 50)
        Panel1.Name = "Panel1"
        Panel1.Padding = New Padding(0, 10, 0, 10)
        Panel1.Size = New Size(755, 52)
        Panel1.TabIndex = 11
        ' 
        ' MB_删除滤镜
        ' 
        MB_删除滤镜.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_删除滤镜.BorderRadius = 10
        MB_删除滤镜.BorderSize = 0
        MB_删除滤镜.Dock = DockStyle.Left
        MB_删除滤镜.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_删除滤镜.Location = New Point(340, 10)
        MB_删除滤镜.Margin = New Padding(2)
        MB_删除滤镜.Name = "MB_删除滤镜"
        MB_删除滤镜.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_删除滤镜.Size = New Size(100, 32)
        MB_删除滤镜.TabIndex = 7
        MB_删除滤镜.Text = "删除滤镜"
        ' 
        ' JustEmptyControl2
        ' 
        JustEmptyControl2.Dock = DockStyle.Left
        JustEmptyControl2.Location = New Point(330, 10)
        JustEmptyControl2.Name = "JustEmptyControl2"
        JustEmptyControl2.Size = New Size(10, 32)
        JustEmptyControl2.TabIndex = 15
        ' 
        ' MB_添加自定义音频滤镜
        ' 
        MB_添加自定义音频滤镜.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_添加自定义音频滤镜.BorderRadius = 10
        MB_添加自定义音频滤镜.BorderSize = 0
        MB_添加自定义音频滤镜.Dock = DockStyle.Left
        MB_添加自定义音频滤镜.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_添加自定义音频滤镜.Location = New Point(170, 10)
        MB_添加自定义音频滤镜.Margin = New Padding(2)
        MB_添加自定义音频滤镜.Name = "MB_添加自定义音频滤镜"
        MB_添加自定义音频滤镜.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_添加自定义音频滤镜.Size = New Size(160, 32)
        MB_添加自定义音频滤镜.TabIndex = 14
        MB_添加自定义音频滤镜.Text = "添加自定义音频滤镜"
        ' 
        ' JustEmptyControl1
        ' 
        JustEmptyControl1.Dock = DockStyle.Left
        JustEmptyControl1.Location = New Point(160, 10)
        JustEmptyControl1.Name = "JustEmptyControl1"
        JustEmptyControl1.Size = New Size(10, 32)
        JustEmptyControl1.TabIndex = 2
        ' 
        ' MB_添加自定义视频滤镜
        ' 
        MB_添加自定义视频滤镜.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_添加自定义视频滤镜.BorderRadius = 10
        MB_添加自定义视频滤镜.BorderSize = 0
        MB_添加自定义视频滤镜.Dock = DockStyle.Left
        MB_添加自定义视频滤镜.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_添加自定义视频滤镜.Location = New Point(0, 10)
        MB_添加自定义视频滤镜.Margin = New Padding(2)
        MB_添加自定义视频滤镜.Name = "MB_添加自定义视频滤镜"
        MB_添加自定义视频滤镜.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_添加自定义视频滤镜.Size = New Size(160, 32)
        MB_添加自定义视频滤镜.TabIndex = 12
        MB_添加自定义视频滤镜.Text = "添加自定义视频滤镜"
        ' 
        ' HCL_滤镜排序说明
        ' 
        HCL_滤镜排序说明.AutoSize = True
        HCL_滤镜排序说明.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_滤镜排序说明.Dock = DockStyle.Top
        HCL_滤镜排序说明.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_滤镜排序说明.Location = New Point(20, 20)
        HCL_滤镜排序说明.Margin = New Padding(2)
        HCL_滤镜排序说明.Name = "HCL_滤镜排序说明"
        HCL_滤镜排序说明.Padding = New Padding(0, 0, 0, 5)
        HCL_滤镜排序说明.Size = New Size(755, 30)
        HCL_滤镜排序说明.TabIndex = 10
        HCL_滤镜排序说明.Text = "<span style=""font-size:13; color:Silver"">滤镜排序和自定义</span>   如果需要一行里写多个滤镜，使用英文逗号隔开即可"
        ' 
        ' Form_v6_参数面板_滤镜排序
        ' 
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ClientSize = New Size(795, 614)
        Controls.Add(ModernPanel1)
        Font = New Font("Microsoft YaHei UI", 10F)
        ForeColor = Color.Silver
        Name = "Form_v6_参数面板_滤镜排序"
        Text = "Form_v6_参数面板_滤镜排序"
        ModernPanel1.ResumeLayout(False)
        ModernPanel1.PerformLayout()
        Panel1.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents ModernPanel1 As LakeUI.ModernPanel
    Friend WithEvents HCL_滤镜排序说明 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents MB_删除滤镜 As LakeUI.ModernButton
    Friend WithEvents JustEmptyControl3 As LakeUI.JustEmptyControl
    Friend WithEvents JustEmptyControl1 As LakeUI.JustEmptyControl
    Friend WithEvents UDLV_滤镜排序列表 As LakeUI.UltraDetailListView
    Friend WithEvents JustEmptyControl2 As LakeUI.JustEmptyControl
    Friend WithEvents MB_添加自定义音频滤镜 As LakeUI.ModernButton
    Friend WithEvents MB_添加自定义视频滤镜 As LakeUI.ModernButton
End Class
