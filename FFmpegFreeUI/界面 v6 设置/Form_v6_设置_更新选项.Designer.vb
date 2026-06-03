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
        HtmlColorLabel5 = New LakeUI.HtmlColorLabel()
        HtmlColorLabel4 = New LakeUI.HtmlColorLabel()
        HtmlColorLabel3 = New LakeUI.HtmlColorLabel()
        HtmlColorLabel2 = New LakeUI.HtmlColorLabel()
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
        ModernPanel1.Controls.Add(HtmlColorLabel5)
        ModernPanel1.Controls.Add(HtmlColorLabel4)
        ModernPanel1.Controls.Add(HtmlColorLabel3)
        ModernPanel1.Controls.Add(HtmlColorLabel2)
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
        ' HtmlColorLabel5
        ' 
        HtmlColorLabel5.AutoSize = True
        HtmlColorLabel5.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel5.Dock = DockStyle.Top
        HtmlColorLabel5.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HtmlColorLabel5.Location = New Point(20, 283)
        HtmlColorLabel5.Margin = New Padding(2)
        HtmlColorLabel5.Name = "HtmlColorLabel5"
        HtmlColorLabel5.Padding = New Padding(0, 20, 0, 0)
        HtmlColorLabel5.Size = New Size(740, 41)
        HtmlColorLabel5.TabIndex = 17
        HtmlColorLabel5.Text = "如何解锁神秘的更新服务器？不妨去翻翻设置文件"
        ' 
        ' HtmlColorLabel4
        ' 
        HtmlColorLabel4.AutoSize = True
        HtmlColorLabel4.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel4.Dock = DockStyle.Top
        HtmlColorLabel4.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HtmlColorLabel4.Location = New Point(20, 238)
        HtmlColorLabel4.Margin = New Padding(2)
        HtmlColorLabel4.Name = "HtmlColorLabel4"
        HtmlColorLabel4.Padding = New Padding(0, 20, 0, 0)
        HtmlColorLabel4.Size = New Size(740, 45)
        HtmlColorLabel4.TabIndex = 16
        HtmlColorLabel4.Text = "<span style=""font-size:13; color:Silver"">MirrorChyan CDK 已填入</span>   现在你可以使用Mirror酱的付费CDN线路了"
        HtmlColorLabel4.Visible = False
        ' 
        ' HtmlColorLabel3
        ' 
        HtmlColorLabel3.AutoSize = True
        HtmlColorLabel3.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel3.Dock = DockStyle.Top
        HtmlColorLabel3.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HtmlColorLabel3.Location = New Point(20, 132)
        HtmlColorLabel3.Margin = New Padding(2)
        HtmlColorLabel3.Name = "HtmlColorLabel3"
        HtmlColorLabel3.Padding = New Padding(0, 5, 0, 0)
        HtmlColorLabel3.Size = New Size(740, 106)
        HtmlColorLabel3.TabIndex = 15
        HtmlColorLabel3.Text = "为了保障我们的服务质量，枫源镜像使用 PoW 挑战来对抗网络攻击<br>这是一种基于工作量证明的验证码，不需要用户进行额外操作<br>所有人只需要消耗必要的算力即可过关，所需的算力工作量可在云端控制<br>挑战时间取决于你的 CPU 性能，3FUI 会自动尝试多线程来加速计算<br>此过程不会涉及任何隐私，计算结果仅用于向服务器提交答案，不会进行任何保留"
        ' 
        ' HtmlColorLabel2
        ' 
        HtmlColorLabel2.AutoSize = True
        HtmlColorLabel2.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel2.Dock = DockStyle.Top
        HtmlColorLabel2.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HtmlColorLabel2.Location = New Point(20, 87)
        HtmlColorLabel2.Margin = New Padding(2)
        HtmlColorLabel2.Name = "HtmlColorLabel2"
        HtmlColorLabel2.Padding = New Padding(0, 20, 0, 0)
        HtmlColorLabel2.Size = New Size(740, 45)
        HtmlColorLabel2.TabIndex = 14
        HtmlColorLabel2.Text = "<span style=""font-size:13; color:Silver"">枫源镜像</span>   本群指定合作镜像源"
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
        MCB_更新服务器.BorderRadius = 10
        MCB_更新服务器.BorderSize = 0
        MCB_更新服务器.Dock = DockStyle.Left
        MCB_更新服务器.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_更新服务器.DropDownHoverAnimationDuration = 0
        MCB_更新服务器.DropDownHoverColor = Color.FromArgb(CByte(80), CByte(0), CByte(0), CByte(0))
        MCB_更新服务器.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_更新服务器.DropDownPadding = New Padding(10)
        MCB_更新服务器.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_更新服务器.DropDownSelectedForeColor = Color.White
        MCB_更新服务器.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_更新服务器.Items.Add("GitHub")
        MCB_更新服务器.Items.Add("gh-proxy.com")
        MCB_更新服务器.Items.Add("枫源镜像（国内推荐）")
        MCB_更新服务器.Items.Add("???")
        MCB_更新服务器.Location = New Point(0, 10)
        MCB_更新服务器.Margin = New Padding(2, 2, 2, 2)
        MCB_更新服务器.Name = "MCB_更新服务器"
        MCB_更新服务器.Padding = New Padding(10, 0, 10, 0)
        MCB_更新服务器.Size = New Size(200, 32)
        MCB_更新服务器.TabIndex = 0
        MCB_更新服务器.ToolTipGap = -1
        MCB_更新服务器.ToolTipMaxWidth = 350
        MCB_更新服务器.ToolTipPadding = New Padding(15)
        MCB_更新服务器.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
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
        HtmlColorLabel1.Size = New Size(740, 25)
        HtmlColorLabel1.TabIndex = 8
        HtmlColorLabel1.Text = "<span style=""font-size:13; color:Silver"">更新服务器</span>   选择所有的下载更新使用什么服务器"
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
    Friend WithEvents HtmlColorLabel4 As LakeUI.HtmlColorLabel
    Friend WithEvents HtmlColorLabel5 As LakeUI.HtmlColorLabel
End Class
