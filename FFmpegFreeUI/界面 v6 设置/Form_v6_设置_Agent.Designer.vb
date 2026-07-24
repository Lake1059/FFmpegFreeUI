<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_v6_设置_Agent
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
        HtmlColorLabel4 = New LakeUI.HtmlColorLabel()
        Panel2 = New Panel()
        ModernComboBox1 = New LakeUI.ModernComboBox()
        HtmlColorLabel3 = New LakeUI.HtmlColorLabel()
        HtmlColorLabel2 = New LakeUI.HtmlColorLabel()
        Panel5 = New Panel()
        HtmlColorLabel10 = New LakeUI.HtmlColorLabel()
        MTB_附加请求Body = New LakeUI.ModernTextBox()
        Panel4 = New Panel()
        HtmlColorLabel9 = New LakeUI.HtmlColorLabel()
        MTB_附加请求头 = New LakeUI.ModernTextBox()
        Panel3 = New Panel()
        HtmlColorLabel8 = New LakeUI.HtmlColorLabel()
        MTB_APIKEY = New LakeUI.ModernTextBox()
        Panel1 = New Panel()
        HtmlColorLabel7 = New LakeUI.HtmlColorLabel()
        MTB_自定义地址 = New LakeUI.ModernTextBox()
        HtmlColorLabel1 = New LakeUI.HtmlColorLabel()
        HtmlColorLabel6 = New LakeUI.HtmlColorLabel()
        ModernPanel1.SuspendLayout()
        Panel2.SuspendLayout()
        Panel5.SuspendLayout()
        Panel4.SuspendLayout()
        Panel3.SuspendLayout()
        Panel1.SuspendLayout()
        SuspendLayout()
        '
        ' ModernPanel1
        '
        ModernPanel1.BackColor = Color.Transparent
        ModernPanel1.BackColor1 = Color.Transparent
        ModernPanel1.BorderSize = 0
        ModernPanel1.Controls.Add(HtmlColorLabel4)
        ModernPanel1.Controls.Add(Panel2)
        ModernPanel1.Controls.Add(HtmlColorLabel3)
        ModernPanel1.Controls.Add(HtmlColorLabel2)
        ModernPanel1.Controls.Add(Panel5)
        ModernPanel1.Controls.Add(Panel4)
        ModernPanel1.Controls.Add(Panel3)
        ModernPanel1.Controls.Add(Panel1)
        ModernPanel1.Controls.Add(HtmlColorLabel1)
        ModernPanel1.Controls.Add(HtmlColorLabel6)
        ModernPanel1.Dock = DockStyle.Fill
        ModernPanel1.Location = New Point(0, 0)
        ModernPanel1.Name = "ModernPanel1"
        ModernPanel1.Padding = New Padding(20)
        ModernPanel1.Size = New Size(809, 591)
        ModernPanel1.TabIndex = 0
        '
        ' HtmlColorLabel4
        '
        HtmlColorLabel4.AutoSize = True
        HtmlColorLabel4.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel4.Dock = DockStyle.Top
        HtmlColorLabel4.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HtmlColorLabel4.Location = New Point(20, 329)
        HtmlColorLabel4.Margin = New Padding(2)
        HtmlColorLabel4.Name = "HtmlColorLabel4"
        HtmlColorLabel4.Padding = New Padding(0, 20, 0, 0)
        HtmlColorLabel4.Size = New Size(769, 65)
        HtmlColorLabel4.TabIndex = 36
        HtmlColorLabel4.Text = "<span style=""font-size:13; color:Silver"">免责声明</span>   开发者不承担任何 AI 导致的任何损失<br>使用即代表同意此条款"
        '
        ' Panel2
        '
        Panel2.Controls.Add(ModernComboBox1)
        Panel2.Dock = DockStyle.Top
        Panel2.Location = New Point(20, 287)
        Panel2.Name = "Panel2"
        Panel2.Padding = New Padding(0, 10, 0, 0)
        Panel2.Size = New Size(769, 42)
        Panel2.TabIndex = 27
        '
        ' ModernComboBox1
        '
        ModernComboBox1.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernComboBox1.BorderRadius = 10
        ModernComboBox1.BorderSize = 0
        ModernComboBox1.Dock = DockStyle.Left
        ModernComboBox1.DropDownBackdropBlurPasses = 2
        ModernComboBox1.DropDownBackdropBlurRadius = 30
        ModernComboBox1.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        ModernComboBox1.DropDownHoverColor = Color.FromArgb(CByte(20), CByte(220), CByte(220), CByte(220))
        ModernComboBox1.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        ModernComboBox1.DropDownPadding = New Padding(10)
        ModernComboBox1.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernComboBox1.DropDownSelectedForeColor = Color.White
        ModernComboBox1.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        ModernComboBox1.Items.Add("使用上面的自定义端点")
        ModernComboBox1.Location = New Point(0, 10)
        ModernComboBox1.Margin = New Padding(2, 2, 2, 2)
        ModernComboBox1.Name = "ModernComboBox1"
        ModernComboBox1.Padding = New Padding(10, 0, 10, 0)
        ModernComboBox1.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernComboBox1.Size = New Size(400, 32)
        ModernComboBox1.TabIndex = 0
        ModernComboBox1.ToolTipGap = -1
        ModernComboBox1.ToolTipMaxWidth = 350
        ModernComboBox1.ToolTipPadding = New Padding(15)
        ModernComboBox1.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        '
        ' HtmlColorLabel3
        '
        HtmlColorLabel3.AutoSize = True
        HtmlColorLabel3.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel3.Dock = DockStyle.Top
        HtmlColorLabel3.ForeColor = Color.IndianRed
        HtmlColorLabel3.Location = New Point(20, 261)
        HtmlColorLabel3.Margin = New Padding(2)
        HtmlColorLabel3.Name = "HtmlColorLabel3"
        HtmlColorLabel3.Padding = New Padding(0, 5, 0, 0)
        HtmlColorLabel3.Size = New Size(769, 26)
        HtmlColorLabel3.TabIndex = 35
        HtmlColorLabel3.Text = "将会严格监控用量，随时拉闸，滥用（包括偷渡检测）将直接封禁公网 IP"
        '
        ' HtmlColorLabel2
        '
        HtmlColorLabel2.AutoSize = True
        HtmlColorLabel2.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel2.Dock = DockStyle.Top
        HtmlColorLabel2.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HtmlColorLabel2.Location = New Point(20, 216)
        HtmlColorLabel2.Margin = New Padding(2)
        HtmlColorLabel2.Name = "HtmlColorLabel2"
        HtmlColorLabel2.Padding = New Padding(0, 20, 0, 0)
        HtmlColorLabel2.Size = New Size(769, 45)
        HtmlColorLabel2.TabIndex = 31
        HtmlColorLabel2.Text = "<span style=""font-size:13; color:Silver"">SP 用户专属端点</span>   开发者可能会提供一些便宜量大的端点供 SP 用户免费使用"
        '
        ' Panel5
        '
        Panel5.Controls.Add(HtmlColorLabel10)
        Panel5.Controls.Add(MTB_附加请求Body)
        Panel5.Dock = DockStyle.Top
        Panel5.Location = New Point(20, 216)
        Panel5.Name = "Panel5"
        Panel5.Padding = New Padding(0, 10, 0, 0)
        Panel5.Size = New Size(769, 82)
        Panel5.TabIndex = 37
        '
        ' HtmlColorLabel10
        '
        HtmlColorLabel10.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel10.Dock = DockStyle.Fill
        HtmlColorLabel10.Location = New Point(400, 10)
        HtmlColorLabel10.Margin = New Padding(2)
        HtmlColorLabel10.Name = "HtmlColorLabel10"
        HtmlColorLabel10.Padding = New Padding(10, 0, 0, 0)
        HtmlColorLabel10.Size = New Size(369, 72)
        HtmlColorLabel10.TabIndex = 18
        HtmlColorLabel10.Text = "附加请求 Body (extra_body)"
        HtmlColorLabel10.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.MiddleLeft
        HtmlColorLabel10.ToolTipText = "格式：JSON 对象，会合并到 chat/completions 请求体顶层。例如：{""thinking"":{""type"":""enabled""}}"
        '
        ' MTB_附加请求Body
        '
        MTB_附加请求Body.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_附加请求Body.BorderColor = Color.Transparent
        MTB_附加请求Body.BorderColorFocus = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MTB_附加请求Body.BorderRadius = 10
        MTB_附加请求Body.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        MTB_附加请求Body.Dock = DockStyle.Left
        MTB_附加请求Body.LineHeight = 20
        MTB_附加请求Body.Location = New Point(0, 10)
        MTB_附加请求Body.Margin = New Padding(2)
        MTB_附加请求Body.MultiLine = True
        MTB_附加请求Body.Name = "MTB_附加请求Body"
        MTB_附加请求Body.Padding = New Padding(10, 6, 10, 6)
        MTB_附加请求Body.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_附加请求Body.Size = New Size(400, 72)
        MTB_附加请求Body.TabIndex = 19
        MTB_附加请求Body.WaterText = "{""thinking"":{""type"":""enabled""}}"
        MTB_附加请求Body.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        '
        ' Panel4
        '
        Panel4.Controls.Add(HtmlColorLabel9)
        Panel4.Controls.Add(MTB_附加请求头)
        Panel4.Dock = DockStyle.Top
        Panel4.Location = New Point(20, 174)
        Panel4.Name = "Panel4"
        Panel4.Padding = New Padding(0, 10, 0, 0)
        Panel4.Size = New Size(769, 82)
        Panel4.TabIndex = 30
        '
        ' HtmlColorLabel9
        '
        HtmlColorLabel9.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel9.Dock = DockStyle.Fill
        HtmlColorLabel9.Location = New Point(400, 10)
        HtmlColorLabel9.Margin = New Padding(2)
        HtmlColorLabel9.Name = "HtmlColorLabel9"
        HtmlColorLabel9.Padding = New Padding(10, 0, 0, 0)
        HtmlColorLabel9.Size = New Size(369, 72)
        HtmlColorLabel9.TabIndex = 18
        HtmlColorLabel9.Text = "附加请求头"
        HtmlColorLabel9.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.MiddleLeft
        HtmlColorLabel9.ToolTipText = "格式：每行一个请求头，写作 名称: 值。例如：HTTP-Referer: https://example.com"
        '
        ' MTB_附加请求头
        '
        MTB_附加请求头.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_附加请求头.BorderColor = Color.Transparent
        MTB_附加请求头.BorderColorFocus = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MTB_附加请求头.BorderRadius = 10
        MTB_附加请求头.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        MTB_附加请求头.Dock = DockStyle.Left
        MTB_附加请求头.LineHeight = 20
        MTB_附加请求头.Location = New Point(0, 10)
        MTB_附加请求头.Margin = New Padding(2)
        MTB_附加请求头.MultiLine = True
        MTB_附加请求头.Name = "MTB_附加请求头"
        MTB_附加请求头.Padding = New Padding(10, 6, 10, 6)
        MTB_附加请求头.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_附加请求头.Size = New Size(400, 72)
        MTB_附加请求头.TabIndex = 19
        MTB_附加请求头.WaterText = "Header-Name: value"
        MTB_附加请求头.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        '
        ' Panel3
        '
        Panel3.Controls.Add(HtmlColorLabel8)
        Panel3.Controls.Add(MTB_APIKEY)
        Panel3.Dock = DockStyle.Top
        Panel3.Location = New Point(20, 132)
        Panel3.Name = "Panel3"
        Panel3.Padding = New Padding(0, 10, 0, 0)
        Panel3.Size = New Size(769, 42)
        Panel3.TabIndex = 29
        '
        ' HtmlColorLabel8
        '
        HtmlColorLabel8.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel8.Dock = DockStyle.Fill
        HtmlColorLabel8.Location = New Point(400, 10)
        HtmlColorLabel8.Margin = New Padding(2)
        HtmlColorLabel8.Name = "HtmlColorLabel8"
        HtmlColorLabel8.Padding = New Padding(10, 0, 0, 0)
        HtmlColorLabel8.Size = New Size(369, 32)
        HtmlColorLabel8.TabIndex = 18
        HtmlColorLabel8.Text = "API KEY"
        HtmlColorLabel8.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.MiddleLeft
        '
        ' MTB_APIKEY
        '
        MTB_APIKEY.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_APIKEY.BorderColor = Color.Transparent
        MTB_APIKEY.BorderColorFocus = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MTB_APIKEY.BorderRadius = 10
        MTB_APIKEY.Dock = DockStyle.Left
        MTB_APIKEY.Location = New Point(0, 10)
        MTB_APIKEY.Margin = New Padding(2)
        MTB_APIKEY.Name = "MTB_APIKEY"
        MTB_APIKEY.Padding = New Padding(10, 0, 0, 0)
        MTB_APIKEY.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_APIKEY.Size = New Size(400, 32)
        MTB_APIKEY.TabIndex = 19
        MTB_APIKEY.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        '
        ' Panel1
        '
        Panel1.Controls.Add(HtmlColorLabel7)
        Panel1.Controls.Add(MTB_自定义地址)
        Panel1.Dock = DockStyle.Top
        Panel1.Location = New Point(20, 90)
        Panel1.Name = "Panel1"
        Panel1.Padding = New Padding(0, 10, 0, 0)
        Panel1.Size = New Size(769, 42)
        Panel1.TabIndex = 28
        '
        ' HtmlColorLabel7
        '
        HtmlColorLabel7.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel7.Dock = DockStyle.Fill
        HtmlColorLabel7.Location = New Point(400, 10)
        HtmlColorLabel7.Margin = New Padding(2)
        HtmlColorLabel7.Name = "HtmlColorLabel7"
        HtmlColorLabel7.Padding = New Padding(10, 0, 0, 0)
        HtmlColorLabel7.Size = New Size(369, 32)
        HtmlColorLabel7.TabIndex = 17
        HtmlColorLabel7.Text = "自定义地址"
        HtmlColorLabel7.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.MiddleLeft
        '
        ' MTB_自定义地址
        '
        MTB_自定义地址.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_自定义地址.BorderColor = Color.Transparent
        MTB_自定义地址.BorderColorFocus = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MTB_自定义地址.BorderRadius = 10
        MTB_自定义地址.Dock = DockStyle.Left
        MTB_自定义地址.Location = New Point(0, 10)
        MTB_自定义地址.Margin = New Padding(2)
        MTB_自定义地址.Name = "MTB_自定义地址"
        MTB_自定义地址.Padding = New Padding(10, 0, 0, 0)
        MTB_自定义地址.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_自定义地址.Size = New Size(400, 32)
        MTB_自定义地址.TabIndex = 18
        MTB_自定义地址.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
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
        HtmlColorLabel1.Size = New Size(769, 25)
        HtmlColorLabel1.TabIndex = 26
        HtmlColorLabel1.Text = "<span style=""font-size:13; color:Silver"">自定义端点</span>   仅支持 OpenAI 兼容接口；支持连接到本地"
        '
        ' HtmlColorLabel6
        '
        HtmlColorLabel6.AutoSize = True
        HtmlColorLabel6.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel6.Dock = DockStyle.Top
        HtmlColorLabel6.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HtmlColorLabel6.Location = New Point(20, 20)
        HtmlColorLabel6.Margin = New Padding(2)
        HtmlColorLabel6.Name = "HtmlColorLabel6"
        HtmlColorLabel6.Padding = New Padding(0, 0, 0, 20)
        HtmlColorLabel6.Size = New Size(769, 45)
        HtmlColorLabel6.TabIndex = 25
        HtmlColorLabel6.Text = "<span style=""font-size:13; color:Silver"">3FUI Agent</span>   专属于 3FUI 的副驾驶！不止聊天，AI 可以操作参数面板并使用 3FUI 文档"
        '
        ' Form_v6_设置_Agent
        '
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ClientSize = New Size(809, 591)
        Controls.Add(ModernPanel1)
        Font = New Font("Microsoft YaHei UI", 10F)
        ForeColor = Color.Silver
        Name = "Form_v6_设置_Agent"
        Text = "Form_v6_设置_Agent"
        ModernPanel1.ResumeLayout(False)
        ModernPanel1.PerformLayout()
        Panel2.ResumeLayout(False)
        Panel5.ResumeLayout(False)
        Panel4.ResumeLayout(False)
        Panel3.ResumeLayout(False)
        Panel1.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents ModernPanel1 As LakeUI.ModernPanel
    Friend WithEvents HtmlColorLabel6 As LakeUI.HtmlColorLabel
    Friend WithEvents HtmlColorLabel1 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents ModernComboBox1 As LakeUI.ModernComboBox
    Friend WithEvents Panel5 As Panel
    Friend WithEvents HtmlColorLabel10 As LakeUI.HtmlColorLabel
    Friend WithEvents MTB_附加请求Body As LakeUI.ModernTextBox
    Friend WithEvents Panel4 As Panel
    Friend WithEvents HtmlColorLabel9 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents HtmlColorLabel8 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents HtmlColorLabel7 As LakeUI.HtmlColorLabel
    Friend WithEvents MTB_自定义地址 As LakeUI.ModernTextBox
    Friend WithEvents MTB_附加请求头 As LakeUI.ModernTextBox
    Friend WithEvents MTB_APIKEY As LakeUI.ModernTextBox
    Friend WithEvents HtmlColorLabel2 As LakeUI.HtmlColorLabel
    Friend WithEvents HtmlColorLabel3 As LakeUI.HtmlColorLabel
    Friend WithEvents HtmlColorLabel4 As LakeUI.HtmlColorLabel
End Class
