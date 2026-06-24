<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_v6_参数面板_画面翻转
    Inherits System.Windows.Forms.Form

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

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        ModernPanel1 = New LakeUI.ModernPanel()
        Panel2 = New Panel()
        MCB_镜像翻转 = New LakeUI.ModernComboBox()
        HtmlColorLabel2 = New LakeUI.HtmlColorLabel()
        Panel1 = New Panel()
        MCB_角度翻转 = New LakeUI.ModernComboBox()
        HtmlColorLabel1 = New LakeUI.HtmlColorLabel()
        MCB_画面翻转总开关 = New LakeUI.ModernCheckBox()
        ModernPanel1.SuspendLayout()
        Panel2.SuspendLayout()
        Panel1.SuspendLayout()
        SuspendLayout()
        ' 
        ' ModernPanel1
        ' 
        ModernPanel1.BackColor1 = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ModernPanel1.BorderSize = 0
        ModernPanel1.Controls.Add(Panel2)
        ModernPanel1.Controls.Add(HtmlColorLabel2)
        ModernPanel1.Controls.Add(Panel1)
        ModernPanel1.Controls.Add(HtmlColorLabel1)
        ModernPanel1.Controls.Add(MCB_画面翻转总开关)
        ModernPanel1.Dock = DockStyle.Fill
        ModernPanel1.Location = New Point(0, 0)
        ModernPanel1.Name = "ModernPanel1"
        ModernPanel1.Padding = New Padding(20)
        ModernPanel1.Size = New Size(384, 241)
        ModernPanel1.TabIndex = 0
        ' 
        ' Panel2
        ' 
        Panel2.Controls.Add(MCB_镜像翻转)
        Panel2.Dock = DockStyle.Top
        Panel2.Location = New Point(20, 176)
        Panel2.Name = "Panel2"
        Panel2.Padding = New Padding(0, 10, 0, 0)
        Panel2.Size = New Size(344, 42)
        Panel2.TabIndex = 38
        ' 
        ' MCB_镜像翻转
        ' 
        MCB_镜像翻转.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_镜像翻转.BorderRadius = 10
        MCB_镜像翻转.BorderSize = 0
        MCB_镜像翻转.Dock = DockStyle.Left
        MCB_镜像翻转.DropDownBackdropBlurPasses = 2
        MCB_镜像翻转.DropDownBackdropBlurRadius = 30
        MCB_镜像翻转.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_镜像翻转.DropDownHoverAnimationDuration = 0
        MCB_镜像翻转.DropDownHoverColor = Color.FromArgb(CByte(80), CByte(0), CByte(0), CByte(0))
        MCB_镜像翻转.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_镜像翻转.DropDownPadding = New Padding(10)
        MCB_镜像翻转.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_镜像翻转.DropDownSelectedForeColor = Color.White
        MCB_镜像翻转.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_镜像翻转.Items.Add("")
        MCB_镜像翻转.Items.Add("水平镜像")
        MCB_镜像翻转.Items.Add("垂直镜像")
        MCB_镜像翻转.Location = New Point(0, 10)
        MCB_镜像翻转.Margin = New Padding(2, 2, 2, 2)
        MCB_镜像翻转.Name = "MCB_镜像翻转"
        MCB_镜像翻转.Padding = New Padding(10, 0, 10, 0)
        MCB_镜像翻转.Size = New Size(180, 32)
        MCB_镜像翻转.TabIndex = 16
        MCB_镜像翻转.ToolTipGap = -1
        MCB_镜像翻转.ToolTipMaxWidth = 350
        MCB_镜像翻转.ToolTipPadding = New Padding(15)
        MCB_镜像翻转.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' HtmlColorLabel2
        ' 
        HtmlColorLabel2.AutoSize = True
        HtmlColorLabel2.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel2.Dock = DockStyle.Top
        HtmlColorLabel2.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HtmlColorLabel2.Location = New Point(20, 131)
        HtmlColorLabel2.Margin = New Padding(2)
        HtmlColorLabel2.Name = "HtmlColorLabel2"
        HtmlColorLabel2.Padding = New Padding(0, 20, 0, 0)
        HtmlColorLabel2.Size = New Size(344, 45)
        HtmlColorLabel2.TabIndex = 37
        HtmlColorLabel2.Text = "<span style=""font-size:13; color:Silver"">镜像翻转</span>"
        ' 
        ' Panel1
        ' 
        Panel1.Controls.Add(MCB_角度翻转)
        Panel1.Dock = DockStyle.Top
        Panel1.Location = New Point(20, 89)
        Panel1.Name = "Panel1"
        Panel1.Padding = New Padding(0, 10, 0, 0)
        Panel1.Size = New Size(344, 42)
        Panel1.TabIndex = 36
        ' 
        ' MCB_角度翻转
        ' 
        MCB_角度翻转.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_角度翻转.BorderRadius = 10
        MCB_角度翻转.BorderSize = 0
        MCB_角度翻转.Dock = DockStyle.Left
        MCB_角度翻转.DropDownBackdropBlurPasses = 2
        MCB_角度翻转.DropDownBackdropBlurRadius = 30
        MCB_角度翻转.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_角度翻转.DropDownHoverAnimationDuration = 0
        MCB_角度翻转.DropDownHoverColor = Color.FromArgb(CByte(80), CByte(0), CByte(0), CByte(0))
        MCB_角度翻转.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_角度翻转.DropDownPadding = New Padding(10)
        MCB_角度翻转.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_角度翻转.DropDownSelectedForeColor = Color.White
        MCB_角度翻转.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_角度翻转.Items.Add("")
        MCB_角度翻转.Items.Add("顺时针旋转 90°")
        MCB_角度翻转.Items.Add("顺时针旋转 180°")
        MCB_角度翻转.Items.Add("顺时针旋转 270°")
        MCB_角度翻转.Items.Add("逆时针旋转 90°")
        MCB_角度翻转.Items.Add("逆时针旋转 180°")
        MCB_角度翻转.Items.Add("逆时针旋转 270°")
        MCB_角度翻转.Location = New Point(0, 10)
        MCB_角度翻转.Margin = New Padding(2, 2, 2, 2)
        MCB_角度翻转.Name = "MCB_角度翻转"
        MCB_角度翻转.Padding = New Padding(10, 0, 10, 0)
        MCB_角度翻转.Size = New Size(220, 32)
        MCB_角度翻转.TabIndex = 15
        MCB_角度翻转.ToolTipGap = -1
        MCB_角度翻转.ToolTipMaxWidth = 350
        MCB_角度翻转.ToolTipPadding = New Padding(15)
        MCB_角度翻转.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' HtmlColorLabel1
        ' 
        HtmlColorLabel1.AutoSize = True
        HtmlColorLabel1.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel1.Dock = DockStyle.Top
        HtmlColorLabel1.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HtmlColorLabel1.Location = New Point(20, 44)
        HtmlColorLabel1.Margin = New Padding(2)
        HtmlColorLabel1.Name = "HtmlColorLabel1"
        HtmlColorLabel1.Padding = New Padding(0, 20, 0, 0)
        HtmlColorLabel1.Size = New Size(344, 45)
        HtmlColorLabel1.TabIndex = 35
        HtmlColorLabel1.Text = "<span style=""font-size:13; color:Silver"">角度翻转</span>"
        ' 
        ' MCB_画面翻转总开关
        ' 
        MCB_画面翻转总开关.AutoSize = True
        MCB_画面翻转总开关.BoxBorderRadius = 5
        MCB_画面翻转总开关.BoxBorderSize = 0
        MCB_画面翻转总开关.BoxCheckedBackColor = Color.OliveDrab
        MCB_画面翻转总开关.BoxInnerPadding = 6
        MCB_画面翻转总开关.BoxSize = 24
        MCB_画面翻转总开关.BoxTextSpacing = 10
        MCB_画面翻转总开关.BoxUncheckedBackColor = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_画面翻转总开关.CheckMarkWidth = 3F
        MCB_画面翻转总开关.Dock = DockStyle.Top
        MCB_画面翻转总开关.Location = New Point(20, 20)
        MCB_画面翻转总开关.Name = "MCB_画面翻转总开关"
        MCB_画面翻转总开关.Size = New Size(344, 24)
        MCB_画面翻转总开关.TabIndex = 25
        MCB_画面翻转总开关.Text = "画面翻转总开关 / 勾选才会启用"
        ' 
        ' Form_v6_参数面板_画面翻转
        ' 
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ClientSize = New Size(384, 241)
        Controls.Add(ModernPanel1)
        Font = New Font("Microsoft YaHei UI", 10F)
        ForeColor = Color.Silver
        MaximizeBox = False
        MinimizeBox = False
        MinimumSize = New Size(400, 280)
        Name = "Form_v6_参数面板_画面翻转"
        ShowInTaskbar = False
        Text = "画面翻转"
        ModernPanel1.ResumeLayout(False)
        ModernPanel1.PerformLayout()
        Panel2.ResumeLayout(False)
        Panel1.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents ModernPanel1 As LakeUI.ModernPanel
    Friend WithEvents MCB_画面翻转总开关 As LakeUI.ModernCheckBox
    Friend WithEvents HtmlColorLabel1 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents MCB_角度翻转 As LakeUI.ModernComboBox
    Friend WithEvents Panel2 As Panel
    Friend WithEvents MCB_镜像翻转 As LakeUI.ModernComboBox
    Friend WithEvents HtmlColorLabel2 As LakeUI.HtmlColorLabel
End Class
