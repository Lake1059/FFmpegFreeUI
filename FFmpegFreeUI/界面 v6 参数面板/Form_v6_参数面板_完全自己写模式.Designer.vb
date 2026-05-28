<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_v6_参数面板_完全自己写模式
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
        HtmlColorLabel1 = New LakeUI.HtmlColorLabel()
        ModernTextBox1 = New LakeUI.ModernTextBox()
        HtmlColorLabel6 = New LakeUI.HtmlColorLabel()
        Panel5 = New Panel()
        ModernButton2 = New LakeUI.ModernButton()
        JustEmptyControl1 = New LakeUI.JustEmptyControl()
        ModernButton1 = New LakeUI.ModernButton()
        ModernPanel1 = New LakeUI.ModernPanel()
        Panel5.SuspendLayout()
        ModernPanel1.SuspendLayout()
        SuspendLayout()
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
        HtmlColorLabel1.Padding = New Padding(0, 0, 0, 5)
        HtmlColorLabel1.Size = New Size(770, 30)
        HtmlColorLabel1.TabIndex = 10
        HtmlColorLabel1.Text = "<span style=""font-size:13; color:Silver"">完全自己写</span>   <span style=""font-size:10pt; color:IndianRed"">使用此模式时，其他所有参数全都不生效！</span>"
        ' 
        ' ModernTextBox1
        ' 
        ModernTextBox1.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernTextBox1.BorderColor = Color.Transparent
        ModernTextBox1.BorderColorFocus = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        ModernTextBox1.BorderRadius = 10
        ModernTextBox1.Dock = DockStyle.Fill
        ModernTextBox1.Location = New Point(20, 173)
        ModernTextBox1.Margin = New Padding(2)
        ModernTextBox1.MultiLine = True
        ModernTextBox1.Name = "ModernTextBox1"
        ModernTextBox1.Padding = New Padding(15, 10, 15, 10)
        ModernTextBox1.ScrollBarColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernTextBox1.ScrollBarHoverColor = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        ModernTextBox1.ScrollBarTrackColor = Color.FromArgb(CByte(20), CByte(220), CByte(220), CByte(220))
        ModernTextBox1.Size = New Size(770, 471)
        ModernTextBox1.TabIndex = 11
        ModernTextBox1.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' HtmlColorLabel6
        ' 
        HtmlColorLabel6.AutoSize = True
        HtmlColorLabel6.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel6.Dock = DockStyle.Top
        HtmlColorLabel6.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HtmlColorLabel6.Location = New Point(20, 50)
        HtmlColorLabel6.Margin = New Padding(2)
        HtmlColorLabel6.Name = "HtmlColorLabel6"
        HtmlColorLabel6.Padding = New Padding(0, 0, 0, 10)
        HtmlColorLabel6.Size = New Size(770, 71)
        HtmlColorLabel6.TabIndex = 14
        HtmlColorLabel6.Text = "不要包含开头的 ffmpeg，这里是直接给其的参数<br>用 <span style=""color:YellowGreen""><InputFile></span> 表示输入文件，用 <span style=""color:YellowGreen""><OutputFile></span> 表示输出文件<br>不会自动写引号，注意区分大小写，可以直接用插入功能"
        ' 
        ' Panel5
        ' 
        Panel5.Controls.Add(ModernButton2)
        Panel5.Controls.Add(JustEmptyControl1)
        Panel5.Controls.Add(ModernButton1)
        Panel5.Dock = DockStyle.Top
        Panel5.Location = New Point(20, 121)
        Panel5.Name = "Panel5"
        Panel5.Padding = New Padding(0, 0, 0, 20)
        Panel5.Size = New Size(770, 52)
        Panel5.TabIndex = 15
        ' 
        ' ModernButton2
        ' 
        ModernButton2.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernButton2.BorderRadius = 10
        ModernButton2.BorderSize = 0
        ModernButton2.Dock = DockStyle.Left
        ModernButton2.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        ModernButton2.Location = New Point(211, 0)
        ModernButton2.Margin = New Padding(2)
        ModernButton2.Name = "ModernButton2"
        ModernButton2.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        ModernButton2.Size = New Size(200, 32)
        ModernButton2.TabIndex = 7
        ModernButton2.Text = "插入输出文件"
        ' 
        ' JustEmptyControl1
        ' 
        JustEmptyControl1.Dock = DockStyle.Left
        JustEmptyControl1.Location = New Point(200, 0)
        JustEmptyControl1.Name = "JustEmptyControl1"
        JustEmptyControl1.Size = New Size(11, 32)
        JustEmptyControl1.TabIndex = 5
        ' 
        ' ModernButton1
        ' 
        ModernButton1.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernButton1.BorderRadius = 10
        ModernButton1.BorderSize = 0
        ModernButton1.Dock = DockStyle.Left
        ModernButton1.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        ModernButton1.Location = New Point(0, 0)
        ModernButton1.Margin = New Padding(2)
        ModernButton1.Name = "ModernButton1"
        ModernButton1.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        ModernButton1.Size = New Size(200, 32)
        ModernButton1.TabIndex = 6
        ModernButton1.Text = "插入输入文件"
        ' 
        ' ModernPanel1
        ' 
        ModernPanel1.BackColor1 = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ModernPanel1.BorderSize = 0
        ModernPanel1.Controls.Add(ModernTextBox1)
        ModernPanel1.Controls.Add(Panel5)
        ModernPanel1.Controls.Add(HtmlColorLabel6)
        ModernPanel1.Controls.Add(HtmlColorLabel1)
        ModernPanel1.Dock = DockStyle.Fill
        ModernPanel1.Location = New Point(0, 0)
        ModernPanel1.Name = "ModernPanel1"
        ModernPanel1.Padding = New Padding(20)
        ModernPanel1.ScrollBarMode = LakeUI.ModernPanel.ScrollMode.None
        ModernPanel1.Size = New Size(810, 664)
        ModernPanel1.TabIndex = 16
        ' 
        ' Form_v6_参数面板_完全自己写模式
        ' 
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ClientSize = New Size(810, 664)
        Controls.Add(ModernPanel1)
        Font = New Font("Microsoft YaHei UI", 10F)
        ForeColor = Color.Silver
        Name = "Form_v6_参数面板_完全自己写模式"
        Text = "Form_v6_参数面板_完全自己写模式"
        Panel5.ResumeLayout(False)
        ModernPanel1.ResumeLayout(False)
        ModernPanel1.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents HtmlColorLabel1 As LakeUI.HtmlColorLabel
    Friend WithEvents ModernTextBox1 As LakeUI.ModernTextBox
    Friend WithEvents HtmlColorLabel6 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents JustEmptyControl1 As LakeUI.JustEmptyControl
    Friend WithEvents ModernButton2 As LakeUI.ModernButton
    Friend WithEvents ModernButton1 As LakeUI.ModernButton
    Friend WithEvents ModernPanel1 As LakeUI.ModernPanel
End Class
