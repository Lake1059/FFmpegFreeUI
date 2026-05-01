<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_v6_设置_远程调用
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
        BooleanSwitch1 = New LakeUI.BooleanSwitch()
        HtmlColorLabel2 = New LakeUI.HtmlColorLabel()
        HtmlColorLabel1 = New LakeUI.HtmlColorLabel()
        ModernTextBox1 = New LakeUI.ModernTextBox()
        JustEmptyControl1 = New LakeUI.JustEmptyControl()
        HtmlColorLabel3 = New LakeUI.HtmlColorLabel()
        ModernPanel1.SuspendLayout()
        Panel2.SuspendLayout()
        SuspendLayout()
        ' 
        ' ModernPanel1
        ' 
        ModernPanel1.BackColor1 = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ModernPanel1.BorderSize = 0
        ModernPanel1.Controls.Add(HtmlColorLabel3)
        ModernPanel1.Controls.Add(Panel2)
        ModernPanel1.Controls.Add(HtmlColorLabel2)
        ModernPanel1.Controls.Add(HtmlColorLabel1)
        ModernPanel1.Dock = DockStyle.Fill
        ModernPanel1.Location = New Point(0, 0)
        ModernPanel1.Name = "ModernPanel1"
        ModernPanel1.Padding = New Padding(20)
        ModernPanel1.ScrollBarMode = LakeUI.ModernPanel.ScrollMode.Vertical
        ModernPanel1.Size = New Size(695, 595)
        ModernPanel1.TabIndex = 3
        ' 
        ' Panel2
        ' 
        Panel2.Controls.Add(ModernTextBox1)
        Panel2.Controls.Add(JustEmptyControl1)
        Panel2.Controls.Add(BooleanSwitch1)
        Panel2.Dock = DockStyle.Top
        Panel2.Location = New Point(20, 91)
        Panel2.Name = "Panel2"
        Panel2.Padding = New Padding(0, 10, 0, 0)
        Panel2.Size = New Size(655, 42)
        Panel2.TabIndex = 11
        ' 
        ' BooleanSwitch1
        ' 
        BooleanSwitch1.Dock = DockStyle.Left
        BooleanSwitch1.KnobColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        BooleanSwitch1.Location = New Point(0, 10)
        BooleanSwitch1.Margin = New Padding(2, 2, 2, 2)
        BooleanSwitch1.Name = "BooleanSwitch1"
        BooleanSwitch1.Size = New Size(55, 32)
        BooleanSwitch1.TabIndex = 0
        BooleanSwitch1.TrackColorOff = Color.IndianRed
        BooleanSwitch1.TrackColorOn = Color.OliveDrab
        ' 
        ' HtmlColorLabel2
        ' 
        HtmlColorLabel2.AutoSize = True
        HtmlColorLabel2.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel2.Dock = DockStyle.Top
        HtmlColorLabel2.ForeColor = Color.Gray
        HtmlColorLabel2.Location = New Point(20, 50)
        HtmlColorLabel2.Margin = New Padding(2)
        HtmlColorLabel2.Name = "HtmlColorLabel2"
        HtmlColorLabel2.Size = New Size(655, 41)
        HtmlColorLabel2.TabIndex = 12
        HtmlColorLabel2.Text = "3FUI 会启动一个 UDP 服务器来负责接收指定端口的数据<br>数据格式就是 3FUI 的启动参数，在仓库上查看功能列表"
        ' 
        ' HtmlColorLabel1
        ' 
        HtmlColorLabel1.AutoSize = True
        HtmlColorLabel1.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel1.Dock = DockStyle.Top
        HtmlColorLabel1.Location = New Point(20, 20)
        HtmlColorLabel1.Margin = New Padding(2)
        HtmlColorLabel1.Name = "HtmlColorLabel1"
        HtmlColorLabel1.Padding = New Padding(0, 0, 0, 5)
        HtmlColorLabel1.Size = New Size(655, 30)
        HtmlColorLabel1.TabIndex = 10
        HtmlColorLabel1.Text = "<span style=""font-size:13"">远程调用</span>"
        ' 
        ' ModernTextBox1
        ' 
        ModernTextBox1.BackColor1 = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        ModernTextBox1.BorderColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        ModernTextBox1.BorderRadius = 10
        ModernTextBox1.BorderSize = 2
        ModernTextBox1.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        ModernTextBox1.Dock = DockStyle.Left
        ModernTextBox1.Location = New Point(65, 10)
        ModernTextBox1.Margin = New Padding(2, 2, 2, 2)
        ModernTextBox1.Name = "ModernTextBox1"
        ModernTextBox1.Padding = New Padding(10, 0, 10, 0)
        ModernTextBox1.ScrollBarHoverColor = Color.FromArgb(CByte(200), CByte(200), CByte(200))
        ModernTextBox1.Size = New Size(100, 32)
        ModernTextBox1.TabIndex = 14
        ModernTextBox1.WaterText = "10591"
        ' 
        ' JustEmptyControl1
        ' 
        JustEmptyControl1.Dock = DockStyle.Left
        JustEmptyControl1.Location = New Point(55, 10)
        JustEmptyControl1.Name = "JustEmptyControl1"
        JustEmptyControl1.Size = New Size(10, 32)
        JustEmptyControl1.TabIndex = 13
        ' 
        ' HtmlColorLabel3
        ' 
        HtmlColorLabel3.AutoSize = True
        HtmlColorLabel3.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel3.Dock = DockStyle.Bottom
        HtmlColorLabel3.ForeColor = Color.Gray
        HtmlColorLabel3.Location = New Point(20, 554)
        HtmlColorLabel3.Margin = New Padding(2)
        HtmlColorLabel3.Name = "HtmlColorLabel3"
        HtmlColorLabel3.Size = New Size(655, 21)
        HtmlColorLabel3.TabIndex = 13
        HtmlColorLabel3.Text = "你不能使用 10590 端口，因为我的其他项目已经占用了"
        ' 
        ' Form_v6_设置_远程调用
        ' 
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ClientSize = New Size(695, 595)
        Controls.Add(ModernPanel1)
        Font = New Font("Microsoft YaHei UI", 10F)
        ForeColor = Color.Silver
        Name = "Form_v6_设置_远程调用"
        Text = "Form_v6_设置_远程调用"
        ModernPanel1.ResumeLayout(False)
        ModernPanel1.PerformLayout()
        Panel2.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents ModernPanel1 As LakeUI.ModernPanel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents BooleanSwitch1 As LakeUI.BooleanSwitch
    Friend WithEvents HtmlColorLabel2 As LakeUI.HtmlColorLabel
    Friend WithEvents HtmlColorLabel1 As LakeUI.HtmlColorLabel
    Friend WithEvents ModernTextBox1 As LakeUI.ModernTextBox
    Friend WithEvents JustEmptyControl1 As LakeUI.JustEmptyControl
    Friend WithEvents HtmlColorLabel3 As LakeUI.HtmlColorLabel
End Class
