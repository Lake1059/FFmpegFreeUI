<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_v6_参数面板_剪辑区间
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
        MCB_向前解码秒数 = New LakeUI.ModernComboBox()
        HCL_向前解码 = New LakeUI.HtmlColorLabel()
        Panel3 = New Panel()
        MTB_出点 = New LakeUI.ModernTextBox()
        JustEmptyControl1 = New LakeUI.JustEmptyControl()
        MTB_入点 = New LakeUI.ModernTextBox()
        Panel5 = New Panel()
        HCL_出点 = New LakeUI.HtmlColorLabel()
        HCL_入点 = New LakeUI.HtmlColorLabel()
        HCL_剪辑时间点填写提示 = New LakeUI.HtmlColorLabel()
        HCL_时间点 = New LakeUI.HtmlColorLabel()
        Panel2 = New Panel()
        MCB_剪辑模式 = New LakeUI.ModernComboBox()
        HCL_剪辑重编码提示 = New LakeUI.HtmlColorLabel()
        HCL_剪辑模式 = New LakeUI.HtmlColorLabel()
        ModernPanel1.SuspendLayout()
        Panel1.SuspendLayout()
        Panel3.SuspendLayout()
        Panel5.SuspendLayout()
        Panel2.SuspendLayout()
        SuspendLayout()
        '
        ' ModernPanel1
        '
        ModernPanel1.BackColor1 = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ModernPanel1.BorderSize = 0
        ModernPanel1.Controls.Add(Panel1)
        ModernPanel1.Controls.Add(HCL_向前解码)
        ModernPanel1.Controls.Add(Panel3)
        ModernPanel1.Controls.Add(Panel5)
        ModernPanel1.Controls.Add(HCL_剪辑时间点填写提示)
        ModernPanel1.Controls.Add(HCL_时间点)
        ModernPanel1.Controls.Add(Panel2)
        ModernPanel1.Controls.Add(HCL_剪辑重编码提示)
        ModernPanel1.Controls.Add(HCL_剪辑模式)
        ModernPanel1.Dock = DockStyle.Fill
        ModernPanel1.Location = New Point(0, 0)
        ModernPanel1.Name = "ModernPanel1"
        ModernPanel1.Padding = New Padding(20)
        ModernPanel1.ScrollBarMode = LakeUI.ModernPanel.ScrollMode.Vertical
        ModernPanel1.Size = New Size(765, 599)
        ModernPanel1.TabIndex = 0
        '
        ' Panel1
        '
        Panel1.Controls.Add(MCB_向前解码秒数)
        Panel1.Dock = DockStyle.Top
        Panel1.Location = New Point(20, 306)
        Panel1.Name = "Panel1"
        Panel1.Padding = New Padding(0, 10, 0, 0)
        Panel1.Size = New Size(725, 42)
        Panel1.TabIndex = 14
        '
        ' MCB_向前解码秒数
        '
        MCB_向前解码秒数.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_向前解码秒数.BorderRadius = 10
        MCB_向前解码秒数.BorderSize = 0
        MCB_向前解码秒数.Dock = DockStyle.Left
        MCB_向前解码秒数.DropDownBackdropBlurPasses = 2
        MCB_向前解码秒数.DropDownBackdropBlurRadius = 30
        MCB_向前解码秒数.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_向前解码秒数.DropDownHoverColor = Color.FromArgb(CByte(20), CByte(220), CByte(220), CByte(220))
        MCB_向前解码秒数.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_向前解码秒数.DropDownPadding = New Padding(10)
        MCB_向前解码秒数.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_向前解码秒数.DropDownSelectedForeColor = Color.White
        MCB_向前解码秒数.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_向前解码秒数.Items.Add("")
        MCB_向前解码秒数.Items.Add("10")
        MCB_向前解码秒数.Items.Add("20")
        MCB_向前解码秒数.Items.Add("30")
        MCB_向前解码秒数.Items.Add("60")
        MCB_向前解码秒数.Items.Add("120")
        MCB_向前解码秒数.Items.Add("360")
        MCB_向前解码秒数.Items.Add("600")
        MCB_向前解码秒数.Location = New Point(0, 10)
        MCB_向前解码秒数.Margin = New Padding(2, 2, 2, 2)
        MCB_向前解码秒数.Name = "MCB_向前解码秒数"
        MCB_向前解码秒数.Padding = New Padding(10, 0, 10, 0)
        MCB_向前解码秒数.Size = New Size(100, 32)
        MCB_向前解码秒数.TabIndex = 0
        MCB_向前解码秒数.ToolTipGap = -1
        MCB_向前解码秒数.ToolTipMaxWidth = 350
        MCB_向前解码秒数.ToolTipPadding = New Padding(15)
        MCB_向前解码秒数.WaterText = "秒"
        MCB_向前解码秒数.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        '
        ' HCL_向前解码
        '
        HCL_向前解码.AutoSize = True
        HCL_向前解码.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_向前解码.Dock = DockStyle.Top
        HCL_向前解码.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_向前解码.Location = New Point(20, 256)
        HCL_向前解码.Margin = New Padding(2)
        HCL_向前解码.Name = "HCL_向前解码"
        HCL_向前解码.Padding = New Padding(0, 20, 0, 5)
        HCL_向前解码.Size = New Size(725, 50)
        HCL_向前解码.TabIndex = 13
        HCL_向前解码.Text = "<span style=""font-size:13"">向前解码</span>   <span style=""font-size:10pt; color:Gray"">仅限快速响应的精简，其他模式无效</span>"
        '
        ' Panel3
        '
        Panel3.Controls.Add(MTB_出点)
        Panel3.Controls.Add(JustEmptyControl1)
        Panel3.Controls.Add(MTB_入点)
        Panel3.Dock = DockStyle.Top
        Panel3.Location = New Point(20, 214)
        Panel3.Name = "Panel3"
        Panel3.Padding = New Padding(0, 10, 0, 0)
        Panel3.Size = New Size(725, 42)
        Panel3.TabIndex = 12
        '
        ' MTB_出点
        '
        MTB_出点.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_出点.BorderColor = Color.Transparent
        MTB_出点.BorderColorFocus = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MTB_出点.BorderRadius = 10
        MTB_出点.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        MTB_出点.Dock = DockStyle.Left
        MTB_出点.Location = New Point(160, 10)
        MTB_出点.Margin = New Padding(2)
        MTB_出点.Name = "MTB_出点"
        MTB_出点.Padding = New Padding(10, 0, 10, 0)
        MTB_出点.Size = New Size(150, 32)
        MTB_出点.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_出点.TabIndex = 6
        MTB_出点.WaterText = "出点"
        MTB_出点.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        '
        ' JustEmptyControl1
        '
        JustEmptyControl1.Dock = DockStyle.Left
        JustEmptyControl1.Location = New Point(150, 10)
        JustEmptyControl1.Name = "JustEmptyControl1"
        JustEmptyControl1.Size = New Size(10, 32)
        JustEmptyControl1.TabIndex = 3
        '
        ' MTB_入点
        '
        MTB_入点.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_入点.BorderColor = Color.Transparent
        MTB_入点.BorderColorFocus = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MTB_入点.BorderRadius = 10
        MTB_入点.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        MTB_入点.Dock = DockStyle.Left
        MTB_入点.Location = New Point(0, 10)
        MTB_入点.Margin = New Padding(2)
        MTB_入点.Name = "MTB_入点"
        MTB_入点.Padding = New Padding(10, 0, 10, 0)
        MTB_入点.Size = New Size(150, 32)
        MTB_入点.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_入点.TabIndex = 5
        MTB_入点.WaterText = "入点"
        MTB_入点.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        '
        ' Panel5
        '
        Panel5.Controls.Add(HCL_出点)
        Panel5.Controls.Add(HCL_入点)
        Panel5.Dock = DockStyle.Top
        Panel5.Location = New Point(20, 184)
        Panel5.Name = "Panel5"
        Panel5.Size = New Size(725, 30)
        Panel5.TabIndex = 17
        '
        ' HCL_出点
        '
        HCL_出点.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_出点.Dock = DockStyle.Left
        HCL_出点.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_出点.Location = New Point(160, 0)
        HCL_出点.Margin = New Padding(2)
        HCL_出点.Name = "HCL_出点"
        HCL_出点.Size = New Size(150, 30)
        HCL_出点.TabIndex = 1
        HCL_出点.Text = "出点"
        HCL_出点.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.BottomLeft
        '
        ' HCL_入点
        '
        HCL_入点.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_入点.Dock = DockStyle.Left
        HCL_入点.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_入点.Location = New Point(0, 0)
        HCL_入点.Margin = New Padding(2)
        HCL_入点.Name = "HCL_入点"
        HCL_入点.Size = New Size(160, 30)
        HCL_入点.TabIndex = 0
        HCL_入点.Text = "入点"
        HCL_入点.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.BottomLeft
        '
        ' HCL_剪辑时间点填写提示
        '
        HCL_剪辑时间点填写提示.AutoSize = True
        HCL_剪辑时间点填写提示.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_剪辑时间点填写提示.Dock = DockStyle.Top
        HCL_剪辑时间点填写提示.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_剪辑时间点填写提示.Location = New Point(20, 163)
        HCL_剪辑时间点填写提示.Margin = New Padding(2)
        HCL_剪辑时间点填写提示.Name = "HCL_剪辑时间点填写提示"
        HCL_剪辑时间点填写提示.Size = New Size(725, 21)
        HCL_剪辑时间点填写提示.TabIndex = 11
        HCL_剪辑时间点填写提示.Text = "可以 <span style=""color:Gainsboro"">只写一个</span> 来表示 <span style=""color:Gainsboro"">从指定时间到末尾</span> 或 <span style=""color:Gainsboro"">从开头到指定时间</span>"
        '
        ' HCL_时间点
        '
        HCL_时间点.AutoSize = True
        HCL_时间点.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_时间点.Dock = DockStyle.Top
        HCL_时间点.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_时间点.Location = New Point(20, 113)
        HCL_时间点.Margin = New Padding(2)
        HCL_时间点.Name = "HCL_时间点"
        HCL_时间点.Padding = New Padding(0, 20, 0, 5)
        HCL_时间点.Size = New Size(725, 50)
        HCL_时间点.TabIndex = 10
        HCL_时间点.Text = "<span style=""font-size:13; color:Silver"">时间点</span>   时间格式：时:分:秒.毫秒"
        '
        ' Panel2
        '
        Panel2.Controls.Add(MCB_剪辑模式)
        Panel2.Dock = DockStyle.Top
        Panel2.Location = New Point(20, 71)
        Panel2.Name = "Panel2"
        Panel2.Padding = New Padding(0, 10, 0, 0)
        Panel2.Size = New Size(725, 42)
        Panel2.TabIndex = 9
        '
        ' MCB_剪辑模式
        '
        MCB_剪辑模式.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_剪辑模式.BorderRadius = 10
        MCB_剪辑模式.BorderSize = 0
        MCB_剪辑模式.Dock = DockStyle.Left
        MCB_剪辑模式.DropDownBackdropBlurPasses = 2
        MCB_剪辑模式.DropDownBackdropBlurRadius = 30
        MCB_剪辑模式.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_剪辑模式.DropDownHoverColor = Color.FromArgb(CByte(20), CByte(220), CByte(220), CByte(220))
        MCB_剪辑模式.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_剪辑模式.DropDownPadding = New Padding(10)
        MCB_剪辑模式.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_剪辑模式.DropDownSelectedForeColor = Color.White
        MCB_剪辑模式.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_剪辑模式.Items.Add("")
        MCB_剪辑模式.Items.Add("粗剪（立即响应，最快，取关键帧）")
        MCB_剪辑模式.Items.Add("精剪（从头解码，需要等，重编码才准）")
        MCB_剪辑模式.Items.Add("精剪（空降解码，不怎么等，重编码才准）")
        MCB_剪辑模式.Items.Add("Trim 滤镜（从头解码，必须重编码，最准，可剪掉要烧的字幕）")
        MCB_剪辑模式.Items.Add("掐头去尾（额外需要 ffprobe，重编码才准）")
        MCB_剪辑模式.Location = New Point(0, 10)
        MCB_剪辑模式.Margin = New Padding(2, 2, 2, 2)
        MCB_剪辑模式.Name = "MCB_剪辑模式"
        MCB_剪辑模式.Padding = New Padding(10, 0, 10, 0)
        MCB_剪辑模式.Size = New Size(500, 32)
        MCB_剪辑模式.TabIndex = 0
        MCB_剪辑模式.ToolTipGap = -1
        MCB_剪辑模式.ToolTipMaxWidth = 350
        MCB_剪辑模式.ToolTipPadding = New Padding(15)
        MCB_剪辑模式.WaterText = "选择剪辑模式"
        MCB_剪辑模式.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        '
        ' HCL_剪辑重编码提示
        '
        HCL_剪辑重编码提示.AutoSize = True
        HCL_剪辑重编码提示.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_剪辑重编码提示.Dock = DockStyle.Top
        HCL_剪辑重编码提示.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_剪辑重编码提示.Location = New Point(20, 50)
        HCL_剪辑重编码提示.Margin = New Padding(2)
        HCL_剪辑重编码提示.Name = "HCL_剪辑重编码提示"
        HCL_剪辑重编码提示.Size = New Size(725, 21)
        HCL_剪辑重编码提示.TabIndex = 18
        HCL_剪辑重编码提示.Text = "所有模式 <span style=""color:YellowGreen"">必须重编码才能精确到帧</span>，ffmpeg 复制流只能取关键帧"
        '
        ' HCL_剪辑模式
        '
        HCL_剪辑模式.AutoSize = True
        HCL_剪辑模式.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_剪辑模式.Dock = DockStyle.Top
        HCL_剪辑模式.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_剪辑模式.Location = New Point(20, 20)
        HCL_剪辑模式.Margin = New Padding(2)
        HCL_剪辑模式.Name = "HCL_剪辑模式"
        HCL_剪辑模式.Padding = New Padding(0, 0, 0, 5)
        HCL_剪辑模式.Size = New Size(725, 30)
        HCL_剪辑模式.TabIndex = 8
        HCL_剪辑模式.Text = "<span style=""font-size:13; color:Silver"">剪辑模式</span>   仅供简单剪辑，高级需求去用剪辑软件"
        '
        ' Form_v6_参数面板_剪辑区间
        '
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ClientSize = New Size(765, 599)
        Controls.Add(ModernPanel1)
        Font = New Font("Microsoft YaHei UI", 10F)
        ForeColor = Color.Silver
        Name = "Form_v6_参数面板_剪辑区间"
        Text = "Form_v6_参数面板_剪辑区间"
        ModernPanel1.ResumeLayout(False)
        ModernPanel1.PerformLayout()
        Panel1.ResumeLayout(False)
        Panel3.ResumeLayout(False)
        Panel5.ResumeLayout(False)
        Panel2.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents ModernPanel1 As LakeUI.ModernPanel
    Friend WithEvents HCL_剪辑模式 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents MCB_剪辑模式 As LakeUI.ModernComboBox
    Friend WithEvents HCL_时间点 As LakeUI.HtmlColorLabel
    Friend WithEvents HCL_剪辑时间点填写提示 As LakeUI.HtmlColorLabel
    Friend WithEvents HCL_向前解码 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents MTB_出点 As LakeUI.ModernTextBox
    Friend WithEvents JustEmptyControl1 As LakeUI.JustEmptyControl
    Friend WithEvents MTB_入点 As LakeUI.ModernTextBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents MCB_向前解码秒数 As LakeUI.ModernComboBox
    Friend WithEvents Panel5 As Panel
    Friend WithEvents HCL_出点 As LakeUI.HtmlColorLabel
    Friend WithEvents HCL_入点 As LakeUI.HtmlColorLabel
    Friend WithEvents HCL_剪辑重编码提示 As LakeUI.HtmlColorLabel
End Class
