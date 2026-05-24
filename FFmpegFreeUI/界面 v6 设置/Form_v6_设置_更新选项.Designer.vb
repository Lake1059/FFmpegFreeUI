<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_v6_设置_更新选项
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
        MCB_更新服务器 = New LakeUI.ModernComboBox()
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
        ModernPanel1.Size = New Size(780, 630)
        ModernPanel1.TabIndex = 1
        ' 
        ' Panel2
        ' 
        Panel2.Controls.Add(MCB_更新服务器)
        Panel2.Dock = DockStyle.Top
        Panel2.Location = New Point(20, 45)
        Panel2.Name = "Panel2"
        Panel2.Padding = New Padding(0, 10, 0, 0)
        Panel2.Size = New Size(740, 42)
        Panel2.TabIndex = 13
        ' 
        ' MCB_更新服务器
        ' 
        MCB_更新服务器.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_更新服务器.BorderColorFocus = Color.Silver
        MCB_更新服务器.BorderRadius = 10
        MCB_更新服务器.BorderSize = 0
        MCB_更新服务器.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        MCB_更新服务器.Dock = DockStyle.Left
        MCB_更新服务器.DropDownBorderSize = 2
        MCB_更新服务器.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_更新服务器.DropDownPadding = New Padding(10)
        MCB_更新服务器.DropDownScrollBarHoverColor = Color.FromArgb(CByte(200), CByte(200), CByte(200))
        MCB_更新服务器.Items.Add("GitHub")
        MCB_更新服务器.Items.Add("国内镜像")
        MCB_更新服务器.Location = New Point(0, 10)
        MCB_更新服务器.Margin = New Padding(2, 2, 2, 2)
        MCB_更新服务器.Name = "MCB_更新服务器"
        MCB_更新服务器.Padding = New Padding(10, 0, 10, 0)
        MCB_更新服务器.Size = New Size(200, 32)
        MCB_更新服务器.TabIndex = 0
        MCB_更新服务器.ToolTipBorderSize = 2
        MCB_更新服务器.ToolTipGap = 10
        MCB_更新服务器.ToolTipMaxWidth = 350
        MCB_更新服务器.ToolTipPadding = New Padding(15)
        ' 
        ' HtmlColorLabel1
        ' 
        HtmlColorLabel1.AutoSize = True
        HtmlColorLabel1.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel1.Dock = DockStyle.Top
        HtmlColorLabel1.Location = New Point(20, 20)
        HtmlColorLabel1.Margin = New Padding(2)
        HtmlColorLabel1.Name = "HtmlColorLabel1"
        HtmlColorLabel1.Size = New Size(740, 25)
        HtmlColorLabel1.TabIndex = 8
        HtmlColorLabel1.Text = "<span style=""font-size:13"">更新服务器</span>   <span style=""font-size:10pt; color:DarkGray"">选择所有的下载更新使用什么服务器</span>"
        ' 
        ' Form_v6_设置_更新选项
        ' 
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ClientSize = New Size(780, 630)
        Controls.Add(ModernPanel1)
        Font = New Font("Microsoft YaHei UI", 10F)
        ForeColor = Color.Silver
        Name = "Form_v6_设置_更新选项"
        Text = "Form_v6_设置_更新选项"
        ModernPanel1.ResumeLayout(False)
        ModernPanel1.PerformLayout()
        Panel2.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents ModernPanel1 As LakeUI.ModernPanel
    Friend WithEvents HtmlColorLabel2 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents MCB_更新服务器 As LakeUI.ModernComboBox
    Friend WithEvents HtmlColorLabel1 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ModernComboBox2 As LakeUI.ModernComboBox
    Friend WithEvents Panel3 As Panel
    Friend WithEvents ModernComboBox3 As LakeUI.ModernComboBox
    Friend WithEvents HtmlColorLabel3 As LakeUI.HtmlColorLabel
End Class
