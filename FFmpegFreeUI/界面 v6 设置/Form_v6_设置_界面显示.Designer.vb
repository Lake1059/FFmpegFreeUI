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
        Panel2 = New Panel()
        MCB_全局字体 = New LakeUI.ModernComboBox()
        HtmlColorLabel1 = New LakeUI.HtmlColorLabel()
        ModernPanel1.SuspendLayout()
        Panel2.SuspendLayout()
        SuspendLayout()
        '
        ' ModernPanel1
        '
        ModernPanel1.BackColor1 = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ModernPanel1.BorderSize = 0
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
        ' Panel2
        '
        Panel2.Controls.Add(MCB_全局字体)
        Panel2.Dock = DockStyle.Top
        Panel2.Location = New Point(20, 45)
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
        MCB_全局字体.DropDownHoverAnimationDuration = 0
        MCB_全局字体.DropDownHoverColor = Color.FromArgb(CByte(80), CByte(0), CByte(0), CByte(0))
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
        HtmlColorLabel1.Size = New Size(712, 25)
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
        Panel2.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents ModernPanel1 As LakeUI.ModernPanel
    Friend WithEvents HtmlColorLabel1 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents MCB_全局字体 As LakeUI.ModernComboBox
End Class
