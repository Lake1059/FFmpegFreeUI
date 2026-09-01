<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_v6_设置_界面显示
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
        Panel4 = New LakeUI.ModernPanel()
        MCB_窗口圆角 = New LakeUI.ModernComboBox()
        HtmlColorLabel3 = New LakeUI.HtmlColorLabel()
        Panel3 = New LakeUI.ModernPanel()
        MCB_界面主题 = New LakeUI.ModernComboBox()
        HtmlColorLabel2 = New LakeUI.HtmlColorLabel()
        Panel2 = New LakeUI.ModernPanel()
        MCB_全局字体 = New LakeUI.ModernComboBox()
        HtmlColorLabel1 = New LakeUI.HtmlColorLabel()
        ModernPanel1.SuspendLayout()
        Panel4.SuspendLayout()
        Panel3.SuspendLayout()
        Panel2.SuspendLayout()
        SuspendLayout()
        ' 
        ' ModernPanel1
        ' 
        ModernPanel1.BackColor = Color.Transparent
        ModernPanel1.BackColor1 = Color.Transparent
        ModernPanel1.BorderSize = 0
        ModernPanel1.Controls.Add(Panel4)
        ModernPanel1.Controls.Add(HtmlColorLabel3)
        ModernPanel1.Controls.Add(Panel3)
        ModernPanel1.Controls.Add(HtmlColorLabel2)
        ModernPanel1.Controls.Add(Panel2)
        ModernPanel1.Controls.Add(HtmlColorLabel1)
        ModernPanel1.Dock = DockStyle.Fill
        ModernPanel1.Location = New Point(0, 0)
        ModernPanel1.Name = "ModernPanel1"
        ModernPanel1.Padding = New Padding(20)
        ModernPanel1.ScrollBarMode = LakeUI.ModernPanel.ScrollMode.Vertical
        ModernPanel1.Size = New Size(752, 625)
        ModernPanel1.TabIndex = 0
        ' 
        ' Panel4
        ' 
        Panel4.BackColor = Color.Transparent
        Panel4.BackColor1 = Color.Transparent
        Panel4.BorderSize = 0
        Panel4.Controls.Add(MCB_窗口圆角)
        Panel4.Dock = DockStyle.Top
        Panel4.Location = New Point(20, 213)
        Panel4.Name = "Panel4"
        Panel4.Padding = New Padding(0, 10, 0, 0)
        Panel4.Size = New Size(712, 42)
        Panel4.TabIndex = 15
        ' 
        ' MCB_窗口圆角
        ' 
        MCB_窗口圆角.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_窗口圆角.BorderRadius = 10
        MCB_窗口圆角.BorderSize = 0
        MCB_窗口圆角.Dock = DockStyle.Left
        MCB_窗口圆角.DropDownBackdropBlurPasses = 2
        MCB_窗口圆角.DropDownBackdropBlurRadius = 30
        MCB_窗口圆角.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_窗口圆角.DropDownHoverColor = Color.FromArgb(CByte(20), CByte(220), CByte(220), CByte(220))
        MCB_窗口圆角.DropDownItemHeight = 26
        MCB_窗口圆角.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_窗口圆角.DropDownPadding = New Padding(10)
        MCB_窗口圆角.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_窗口圆角.DropDownSelectedForeColor = Color.White
        MCB_窗口圆角.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_窗口圆角.Items.Add("直角")
        MCB_窗口圆角.Items.Add("圆角")
        MCB_窗口圆角.Location = New Point(0, 10)
        MCB_窗口圆角.Margin = New Padding(2, 2, 2, 2)
        MCB_窗口圆角.Name = "MCB_窗口圆角"
        MCB_窗口圆角.Padding = New Padding(10, 0, 10, 0)
        MCB_窗口圆角.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_窗口圆角.Size = New Size(200, 32)
        MCB_窗口圆角.TabIndex = 0
        MCB_窗口圆角.ToolTipGap = -1
        MCB_窗口圆角.ToolTipMaxWidth = 350
        MCB_窗口圆角.ToolTipPadding = New Padding(15)
        MCB_窗口圆角.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' HtmlColorLabel3
        ' 
        HtmlColorLabel3.AutoSize = True
        HtmlColorLabel3.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel3.Dock = DockStyle.Top
        HtmlColorLabel3.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HtmlColorLabel3.Location = New Point(20, 170)
        HtmlColorLabel3.Margin = New Padding(2)
        HtmlColorLabel3.Name = "HtmlColorLabel3"
        HtmlColorLabel3.Padding = New Padding(0, 20, 0, 0)
        HtmlColorLabel3.Size = New Size(712, 43)
        HtmlColorLabel3.TabIndex = 14
        HtmlColorLabel3.Text = "<span style=""font-size:13; color:Silver"">窗口圆角</span>   Windows 11 默认圆角；可在此切回直角，修改后即时生效"
        ' 
        ' Panel3
        ' 
        Panel3.BackColor = Color.Transparent
        Panel3.BackColor1 = Color.Transparent
        Panel3.BorderSize = 0
        Panel3.Controls.Add(MCB_界面主题)
        Panel3.Dock = DockStyle.Top
        Panel3.Location = New Point(20, 128)
        Panel3.Name = "Panel3"
        Panel3.Padding = New Padding(0, 10, 0, 0)
        Panel3.Size = New Size(712, 42)
        Panel3.TabIndex = 13
        ' 
        ' MCB_界面主题
        ' 
        MCB_界面主题.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_界面主题.BorderRadius = 10
        MCB_界面主题.BorderSize = 0
        MCB_界面主题.Dock = DockStyle.Left
        MCB_界面主题.DropDownBackdropBlurPasses = 2
        MCB_界面主题.DropDownBackdropBlurRadius = 30
        MCB_界面主题.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_界面主题.DropDownHoverColor = Color.FromArgb(CByte(20), CByte(220), CByte(220), CByte(220))
        MCB_界面主题.DropDownItemHeight = 26
        MCB_界面主题.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_界面主题.DropDownPadding = New Padding(10)
        MCB_界面主题.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_界面主题.DropDownSelectedForeColor = Color.White
        MCB_界面主题.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_界面主题.Items.Add("跟随 Windows")
        MCB_界面主题.Items.Add("明亮")
        MCB_界面主题.Items.Add("暗黑")
        MCB_界面主题.Location = New Point(0, 10)
        MCB_界面主题.Margin = New Padding(2, 2, 2, 2)
        MCB_界面主题.Name = "MCB_界面主题"
        MCB_界面主题.Padding = New Padding(10, 0, 10, 0)
        MCB_界面主题.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_界面主题.Size = New Size(200, 32)
        MCB_界面主题.TabIndex = 0
        MCB_界面主题.ToolTipGap = -1
        MCB_界面主题.ToolTipMaxWidth = 350
        MCB_界面主题.ToolTipPadding = New Padding(15)
        MCB_界面主题.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' HtmlColorLabel2
        ' 
        HtmlColorLabel2.AutoSize = True
        HtmlColorLabel2.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel2.Dock = DockStyle.Top
        HtmlColorLabel2.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HtmlColorLabel2.Location = New Point(20, 85)
        HtmlColorLabel2.Margin = New Padding(2)
        HtmlColorLabel2.Name = "HtmlColorLabel2"
        HtmlColorLabel2.Padding = New Padding(0, 20, 0, 0)
        HtmlColorLabel2.Size = New Size(712, 43)
        HtmlColorLabel2.TabIndex = 12
        HtmlColorLabel2.Text = "<span style=""font-size:13; color:Silver"">界面主题</span>   选择应用使用的明亮或暗黑配色，也可跟随 Windows 设置"
        ' 
        ' Panel2
        ' 
        Panel2.BackColor = Color.Transparent
        Panel2.BackColor1 = Color.Transparent
        Panel2.BorderSize = 0
        Panel2.Controls.Add(MCB_全局字体)
        Panel2.Dock = DockStyle.Top
        Panel2.Location = New Point(20, 43)
        Panel2.Name = "Panel2"
        Panel2.Padding = New Padding(0, 10, 0, 0)
        Panel2.Size = New Size(712, 42)
        Panel2.TabIndex = 11
        ' 
        ' MCB_全局字体
        ' 
        MCB_全局字体.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_全局字体.BorderRadius = 10
        MCB_全局字体.BorderSize = 0
        MCB_全局字体.Dock = DockStyle.Left
        MCB_全局字体.DropDownBackdropBlurPasses = 2
        MCB_全局字体.DropDownBackdropBlurRadius = 30
        MCB_全局字体.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_全局字体.DropDownHoverColor = Color.FromArgb(CByte(20), CByte(220), CByte(220), CByte(220))
        MCB_全局字体.DropDownItemHeight = 26
        MCB_全局字体.DropDownPadding = New Padding(10)
        MCB_全局字体.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_全局字体.DropDownSelectedForeColor = Color.White
        MCB_全局字体.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_全局字体.Location = New Point(0, 10)
        MCB_全局字体.Margin = New Padding(2, 2, 2, 2)
        MCB_全局字体.MaxDropDownItems = 20
        MCB_全局字体.Name = "MCB_全局字体"
        MCB_全局字体.Padding = New Padding(10, 0, 10, 0)
        MCB_全局字体.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_全局字体.Size = New Size(300, 32)
        MCB_全局字体.TabIndex = 0
        MCB_全局字体.ToolTipGap = -1
        MCB_全局字体.ToolTipMaxWidth = 350
        MCB_全局字体.ToolTipPadding = New Padding(15)
        MCB_全局字体.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
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
        HtmlColorLabel1.Size = New Size(712, 23)
        HtmlColorLabel1.TabIndex = 10
        HtmlColorLabel1.Text = "<span style=""font-size:13; color:Silver"">全局字体</span>   在 150% 以及更低 DPI 下使用可以尝试 LakeUI 视觉中的矢量几何绘制"
        ' 
        ' Form_v6_设置_界面显示
        ' 
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ClientSize = New Size(752, 625)
        Controls.Add(ModernPanel1)
        Font = New Font("Microsoft YaHei UI", 10F)
        ForeColor = Color.Silver
        Name = "Form_v6_设置_界面显示"
        Text = "Form_v6_设置_界面显示"
        ModernPanel1.ResumeLayout(False)
        ModernPanel1.PerformLayout()
        Panel4.ResumeLayout(False)
        Panel3.ResumeLayout(False)
        Panel2.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents ModernPanel1 As LakeUI.ModernPanel
    Friend WithEvents HtmlColorLabel1 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel2 As LakeUI.ModernPanel
    Friend WithEvents MCB_全局字体 As LakeUI.ModernComboBox
    Friend WithEvents Panel3 As LakeUI.ModernPanel
    Friend WithEvents MCB_界面主题 As LakeUI.ModernComboBox
    Friend WithEvents HtmlColorLabel2 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel4 As LakeUI.ModernPanel
    Friend WithEvents MCB_窗口圆角 As LakeUI.ModernComboBox
    Friend WithEvents HtmlColorLabel3 As LakeUI.HtmlColorLabel
End Class
