<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_v6_参数面板_流自定义参数
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
        HtmlColorLabel2 = New LakeUI.HtmlColorLabel()
        HtmlColorLabel3 = New LakeUI.HtmlColorLabel()
        ModernTextBox2 = New LakeUI.ModernTextBox()
        ModernPanel1 = New LakeUI.ModernPanel()
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
        HtmlColorLabel1.Padding = New Padding(0, 0, 0, 10)
        HtmlColorLabel1.Size = New Size(722, 35)
        HtmlColorLabel1.TabIndex = 9
        HtmlColorLabel1.Text = "<span style=""font-size:13; color:Silver"">视频流参数</span>   如果要写滤镜请用滤镜排序功能，否则逻辑会冲突"
        ' 
        ' ModernTextBox1
        ' 
        ModernTextBox1.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernTextBox1.BorderColor = Color.Transparent
        ModernTextBox1.BorderColorFocus = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        ModernTextBox1.BorderRadius = 10
        ModernTextBox1.Dock = DockStyle.Top
        ModernTextBox1.LineNumberBackColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernTextBox1.LineNumberForeColor = Color.Silver
        ModernTextBox1.Location = New Point(20, 55)
        ModernTextBox1.Margin = New Padding(2)
        ModernTextBox1.MultiLine = True
        ModernTextBox1.Name = "ModernTextBox1"
        ModernTextBox1.Padding = New Padding(15, 10, 15, 10)
        ModernTextBox1.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernTextBox1.ShowLineNumbers = True
        ModernTextBox1.Size = New Size(722, 256)
        ModernTextBox1.TabIndex = 10
        ModernTextBox1.WaterText = "拼接在已生成部分的末尾，图片参数也是用这个"
        ModernTextBox1.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' HtmlColorLabel2
        ' 
        HtmlColorLabel2.AutoSize = True
        HtmlColorLabel2.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel2.Dock = DockStyle.Top
        HtmlColorLabel2.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HtmlColorLabel2.Location = New Point(20, 311)
        HtmlColorLabel2.Margin = New Padding(2)
        HtmlColorLabel2.Name = "HtmlColorLabel2"
        HtmlColorLabel2.Padding = New Padding(0, 20, 0, 10)
        HtmlColorLabel2.Size = New Size(722, 55)
        HtmlColorLabel2.TabIndex = 11
        HtmlColorLabel2.Text = "<span style=""font-size:13; color:Silver"">音频流参数</span>   如果要写滤镜请用滤镜排序功能，否则逻辑会冲突"
        ' 
        ' HtmlColorLabel3
        ' 
        HtmlColorLabel3.AutoSize = True
        HtmlColorLabel3.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel3.Dock = DockStyle.Bottom
        HtmlColorLabel3.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HtmlColorLabel3.Location = New Point(20, 558)
        HtmlColorLabel3.Margin = New Padding(2)
        HtmlColorLabel3.Name = "HtmlColorLabel3"
        HtmlColorLabel3.Padding = New Padding(0, 20, 0, 0)
        HtmlColorLabel3.Size = New Size(722, 45)
        HtmlColorLabel3.TabIndex = 12
        HtmlColorLabel3.Text = "<span style=""font-size:13; color:Silver"">字幕？</span>   请使用自带相关功能，或者使用在位置插入功能"
        ' 
        ' ModernTextBox2
        ' 
        ModernTextBox2.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernTextBox2.BorderColor = Color.Transparent
        ModernTextBox2.BorderColorFocus = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        ModernTextBox2.BorderRadius = 10
        ModernTextBox2.Dock = DockStyle.Fill
        ModernTextBox2.LineNumberBackColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernTextBox2.LineNumberForeColor = Color.Silver
        ModernTextBox2.Location = New Point(20, 366)
        ModernTextBox2.Margin = New Padding(2)
        ModernTextBox2.MultiLine = True
        ModernTextBox2.Name = "ModernTextBox2"
        ModernTextBox2.Padding = New Padding(15, 10, 15, 10)
        ModernTextBox2.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernTextBox2.ShowLineNumbers = True
        ModernTextBox2.Size = New Size(722, 192)
        ModernTextBox2.TabIndex = 13
        ModernTextBox2.WaterText = "拼接在已生成部分的末尾"
        ModernTextBox2.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' ModernPanel1
        ' 
        ModernPanel1.BackColor1 = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ModernPanel1.BorderSize = 0
        ModernPanel1.Controls.Add(ModernTextBox2)
        ModernPanel1.Controls.Add(HtmlColorLabel3)
        ModernPanel1.Controls.Add(HtmlColorLabel2)
        ModernPanel1.Controls.Add(ModernTextBox1)
        ModernPanel1.Controls.Add(HtmlColorLabel1)
        ModernPanel1.Dock = DockStyle.Fill
        ModernPanel1.Location = New Point(0, 0)
        ModernPanel1.Name = "ModernPanel1"
        ModernPanel1.Padding = New Padding(20)
        ModernPanel1.Size = New Size(762, 623)
        ModernPanel1.TabIndex = 14
        ' 
        ' Form_v6_参数面板_流自定义参数
        ' 
        AutoScaleMode = AutoScaleMode.None
        BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ClientSize = New Size(762, 623)
        Controls.Add(ModernPanel1)
        Font = New Font("Microsoft YaHei UI", 10F)
        ForeColor = Color.Silver
        Name = "Form_v6_参数面板_流自定义参数"
        Text = "Form_v6_参数面板_流自定义参数"
        ModernPanel1.ResumeLayout(False)
        ModernPanel1.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents HtmlColorLabel1 As LakeUI.HtmlColorLabel
    Friend WithEvents ModernTextBox1 As LakeUI.ModernTextBox
    Friend WithEvents HtmlColorLabel2 As LakeUI.HtmlColorLabel
    Friend WithEvents HtmlColorLabel3 As LakeUI.HtmlColorLabel
    Friend WithEvents ModernTextBox2 As LakeUI.ModernTextBox
    Friend WithEvents ModernPanel1 As LakeUI.ModernPanel
End Class
