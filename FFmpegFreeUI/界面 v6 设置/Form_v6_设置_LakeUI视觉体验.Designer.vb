<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_v6_设置_LakeUI视觉体验
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
        ModernPanel1 = New LakeUI.ModernPanel()
        Panel1 = New Panel()
        HtmlColorLabel4 = New LakeUI.HtmlColorLabel()
        MCB_性能计数器 = New LakeUI.ModernComboBox()
        HtmlColorLabel6 = New LakeUI.HtmlColorLabel()
        HtmlColorLabel3 = New LakeUI.HtmlColorLabel()
        HtmlColorLabel2 = New LakeUI.HtmlColorLabel()
        Panel2 = New Panel()
        HtmlColorLabel7 = New LakeUI.HtmlColorLabel()
        MCB_窗口样式 = New LakeUI.ModernComboBox()
        HtmlColorLabel1 = New LakeUI.HtmlColorLabel()
        HtmlColorLabel5 = New LakeUI.HtmlColorLabel()
        ModernPanel1.SuspendLayout()
        Panel1.SuspendLayout()
        Panel2.SuspendLayout()
        SuspendLayout()
        ' 
        ' ModernPanel1
        ' 
        ModernPanel1.BackColor1 = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ModernPanel1.BorderSize = 0
        ModernPanel1.Controls.Add(Panel1)
        ModernPanel1.Controls.Add(HtmlColorLabel6)
        ModernPanel1.Controls.Add(HtmlColorLabel3)
        ModernPanel1.Controls.Add(HtmlColorLabel2)
        ModernPanel1.Controls.Add(Panel2)
        ModernPanel1.Controls.Add(HtmlColorLabel1)
        ModernPanel1.Controls.Add(HtmlColorLabel5)
        ModernPanel1.Dock = DockStyle.Fill
        ModernPanel1.Location = New Point(0, 0)
        ModernPanel1.Name = "ModernPanel1"
        ModernPanel1.Padding = New Padding(20)
        ModernPanel1.ScrollBarMode = LakeUI.ModernPanel.ScrollMode.Vertical
        ModernPanel1.Size = New Size(742, 632)
        ModernPanel1.TabIndex = 1
        ' 
        ' Panel1
        ' 
        Panel1.Controls.Add(HtmlColorLabel4)
        Panel1.Controls.Add(MCB_性能计数器)
        Panel1.Dock = DockStyle.Top
        Panel1.Location = New Point(20, 248)
        Panel1.Name = "Panel1"
        Panel1.Padding = New Padding(0, 10, 0, 0)
        Panel1.Size = New Size(702, 42)
        Panel1.TabIndex = 29
        ' 
        ' HtmlColorLabel4
        ' 
        HtmlColorLabel4.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel4.Dock = DockStyle.Fill
        HtmlColorLabel4.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HtmlColorLabel4.Location = New Point(200, 10)
        HtmlColorLabel4.Margin = New Padding(2)
        HtmlColorLabel4.Name = "HtmlColorLabel4"
        HtmlColorLabel4.Padding = New Padding(10, 0, 0, 0)
        HtmlColorLabel4.Size = New Size(502, 32)
        HtmlColorLabel4.TabIndex = 20
        HtmlColorLabel4.Text = "需要重启软件，仅支持 LakeUI 特别呈现模式"
        HtmlColorLabel4.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.MiddleLeft
        ' 
        ' MCB_性能计数器
        ' 
        MCB_性能计数器.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_性能计数器.BorderRadius = 10
        MCB_性能计数器.BorderSize = 0
        MCB_性能计数器.Dock = DockStyle.Left
        MCB_性能计数器.DropDownBackdropBlurPasses = 2
        MCB_性能计数器.DropDownBackdropBlurRadius = 30
        MCB_性能计数器.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_性能计数器.DropDownHoverColor = Color.FromArgb(CByte(20), CByte(220), CByte(220), CByte(220))
        MCB_性能计数器.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_性能计数器.DropDownPadding = New Padding(10)
        MCB_性能计数器.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_性能计数器.DropDownSelectedForeColor = Color.White
        MCB_性能计数器.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_性能计数器.Items.Add("启用计数器")
        MCB_性能计数器.Items.Add("禁用计数器")
        MCB_性能计数器.Location = New Point(0, 10)
        MCB_性能计数器.Margin = New Padding(2, 2, 2, 2)
        MCB_性能计数器.Name = "MCB_性能计数器"
        MCB_性能计数器.Padding = New Padding(10, 0, 10, 0)
        MCB_性能计数器.Size = New Size(200, 32)
        MCB_性能计数器.TabIndex = 0
        MCB_性能计数器.ToolTipGap = -1
        MCB_性能计数器.ToolTipMaxWidth = 350
        MCB_性能计数器.ToolTipPadding = New Padding(15)
        MCB_性能计数器.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' HtmlColorLabel6
        ' 
        HtmlColorLabel6.AutoSize = True
        HtmlColorLabel6.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel6.Dock = DockStyle.Top
        HtmlColorLabel6.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HtmlColorLabel6.Location = New Point(20, 203)
        HtmlColorLabel6.Margin = New Padding(2)
        HtmlColorLabel6.Name = "HtmlColorLabel6"
        HtmlColorLabel6.Padding = New Padding(0, 20, 0, 0)
        HtmlColorLabel6.Size = New Size(702, 45)
        HtmlColorLabel6.TabIndex = 28
        HtmlColorLabel6.Text = "<span style=""font-size:13; color:Silver"">LakeUI 性能计数器</span>   在标题栏追加实时硬件性能，仅统计 3FUI 自身，不包含任务"
        ' 
        ' HtmlColorLabel3
        ' 
        HtmlColorLabel3.AutoSize = True
        HtmlColorLabel3.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel3.Dock = DockStyle.Top
        HtmlColorLabel3.Location = New Point(20, 177)
        HtmlColorLabel3.Margin = New Padding(2)
        HtmlColorLabel3.Name = "HtmlColorLabel3"
        HtmlColorLabel3.Padding = New Padding(0, 5, 0, 0)
        HtmlColorLabel3.Size = New Size(702, 26)
        HtmlColorLabel3.TabIndex = 27
        HtmlColorLabel3.Text = "ThisIsYourWindow 提供了大量可定制项，如需在 3FUI 中完全发挥实力，请自行制作插件来调整。"
        ' 
        ' HtmlColorLabel2
        ' 
        HtmlColorLabel2.AutoSize = True
        HtmlColorLabel2.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel2.Dock = DockStyle.Top
        HtmlColorLabel2.Font = New Font("Microsoft YaHei UI", 13F)
        HtmlColorLabel2.Location = New Point(20, 132)
        HtmlColorLabel2.Margin = New Padding(2)
        HtmlColorLabel2.Name = "HtmlColorLabel2"
        HtmlColorLabel2.Padding = New Padding(0, 20, 0, 0)
        HtmlColorLabel2.Size = New Size(702, 45)
        HtmlColorLabel2.TabIndex = 14
        HtmlColorLabel2.Text = "关于特别呈现"
        ' 
        ' Panel2
        ' 
        Panel2.Controls.Add(HtmlColorLabel7)
        Panel2.Controls.Add(MCB_窗口样式)
        Panel2.Dock = DockStyle.Top
        Panel2.Location = New Point(20, 90)
        Panel2.Name = "Panel2"
        Panel2.Padding = New Padding(0, 10, 0, 0)
        Panel2.Size = New Size(702, 42)
        Panel2.TabIndex = 13
        ' 
        ' HtmlColorLabel7
        ' 
        HtmlColorLabel7.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel7.Dock = DockStyle.Fill
        HtmlColorLabel7.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HtmlColorLabel7.Location = New Point(200, 10)
        HtmlColorLabel7.Margin = New Padding(2)
        HtmlColorLabel7.Name = "HtmlColorLabel7"
        HtmlColorLabel7.Padding = New Padding(10, 0, 0, 0)
        HtmlColorLabel7.Size = New Size(502, 32)
        HtmlColorLabel7.TabIndex = 19
        HtmlColorLabel7.Text = "默认使用特别呈现，可以在此切回 Windows 原生样式"
        HtmlColorLabel7.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.MiddleLeft
        ' 
        ' MCB_窗口样式
        ' 
        MCB_窗口样式.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_窗口样式.BorderRadius = 10
        MCB_窗口样式.BorderSize = 0
        MCB_窗口样式.Dock = DockStyle.Left
        MCB_窗口样式.DropDownBackdropBlurPasses = 2
        MCB_窗口样式.DropDownBackdropBlurRadius = 30
        MCB_窗口样式.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_窗口样式.DropDownHoverColor = Color.FromArgb(CByte(20), CByte(220), CByte(220), CByte(220))
        MCB_窗口样式.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_窗口样式.DropDownPadding = New Padding(10)
        MCB_窗口样式.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_窗口样式.DropDownSelectedForeColor = Color.White
        MCB_窗口样式.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_窗口样式.Items.Add("不干涉")
        MCB_窗口样式.Items.Add("Windows 暗黑模式")
        MCB_窗口样式.Items.Add("LakeUI 特别呈现")
        MCB_窗口样式.Location = New Point(0, 10)
        MCB_窗口样式.Margin = New Padding(2, 2, 2, 2)
        MCB_窗口样式.Name = "MCB_窗口样式"
        MCB_窗口样式.Padding = New Padding(10, 0, 10, 0)
        MCB_窗口样式.Size = New Size(200, 32)
        MCB_窗口样式.TabIndex = 0
        MCB_窗口样式.ToolTipGap = -1
        MCB_窗口样式.ToolTipMaxWidth = 350
        MCB_窗口样式.ToolTipPadding = New Padding(15)
        MCB_窗口样式.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' HtmlColorLabel1
        ' 
        HtmlColorLabel1.AutoSize = True
        HtmlColorLabel1.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel1.Dock = DockStyle.Top
        HtmlColorLabel1.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HtmlColorLabel1.Location = New Point(20, 65)
        HtmlColorLabel1.Margin = New Padding(2)
        HtmlColorLabel1.Name = "HtmlColorLabel1"
        HtmlColorLabel1.Size = New Size(702, 25)
        HtmlColorLabel1.TabIndex = 8
        HtmlColorLabel1.Text = "<span style=""font-size:13; color:Silver"">窗口样式</span>   特别呈现完全体需要 Windows 11 以及更新版本，需要重启软件"
        ' 
        ' HtmlColorLabel5
        ' 
        HtmlColorLabel5.AutoSize = True
        HtmlColorLabel5.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel5.Dock = DockStyle.Top
        HtmlColorLabel5.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HtmlColorLabel5.Location = New Point(20, 20)
        HtmlColorLabel5.Margin = New Padding(2)
        HtmlColorLabel5.Name = "HtmlColorLabel5"
        HtmlColorLabel5.Padding = New Padding(0, 0, 0, 20)
        HtmlColorLabel5.Size = New Size(702, 45)
        HtmlColorLabel5.TabIndex = 26
        HtmlColorLabel5.Text = "<span style=""font-size:13; color:Silver"">Win32 COM 传统窗口重写</span>   由 ThisIsYourWindow 控件特别呈现"
        ' 
        ' Form_v6_设置_LakeUI视觉体验
        ' 
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ClientSize = New Size(742, 632)
        Controls.Add(ModernPanel1)
        Font = New Font("Microsoft YaHei UI", 10F)
        ForeColor = Color.Silver
        Name = "Form_v6_设置_LakeUI视觉体验"
        Text = "Form_v6_设置_LakeUI视觉体验"
        ModernPanel1.ResumeLayout(False)
        ModernPanel1.PerformLayout()
        Panel1.ResumeLayout(False)
        Panel2.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents ModernPanel1 As LakeUI.ModernPanel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents MCB_窗口样式 As LakeUI.ModernComboBox
    Friend WithEvents HtmlColorLabel1 As LakeUI.HtmlColorLabel
    Friend WithEvents HtmlColorLabel5 As LakeUI.HtmlColorLabel
    Friend WithEvents HtmlColorLabel7 As LakeUI.HtmlColorLabel
    Friend WithEvents HtmlColorLabel3 As LakeUI.HtmlColorLabel
    Friend WithEvents HtmlColorLabel2 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents MCB_性能计数器 As LakeUI.ModernComboBox
    Friend WithEvents HtmlColorLabel6 As LakeUI.HtmlColorLabel
    Friend WithEvents HtmlColorLabel4 As LakeUI.HtmlColorLabel
End Class
