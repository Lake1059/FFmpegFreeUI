<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_v6_参数面板_在位置插入参数
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
        ModernTextBox2 = New LakeUI.ModernTextBox()
        HtmlColorLabel3 = New LakeUI.HtmlColorLabel()
        ModernTextBox3 = New LakeUI.ModernTextBox()
        ModernTextBox4 = New LakeUI.ModernTextBox()
        HtmlColorLabel4 = New LakeUI.HtmlColorLabel()
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
        HtmlColorLabel1.Size = New Size(726, 35)
        HtmlColorLabel1.TabIndex = 10
        HtmlColorLabel1.Text = "<span style=""font-size:13; color:Silver"">开头参数</span>   拼接在输入文件之前（ffmpeg 之后，-i 之前）"
        ' 
        ' ModernTextBox1
        ' 
        ModernTextBox1.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernTextBox1.BorderColor = Color.Transparent
        ModernTextBox1.BorderColorFocus = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        ModernTextBox1.BorderRadius = 10
        ModernTextBox1.BorderSize = 0
        ModernTextBox1.Dock = DockStyle.Top
        ModernTextBox1.Location = New Point(20, 55)
        ModernTextBox1.Margin = New Padding(2)
        ModernTextBox1.MultiLine = True
        ModernTextBox1.Name = "ModernTextBox1"
        ModernTextBox1.Padding = New Padding(15, 10, 15, 10)
        ModernTextBox1.Size = New Size(726, 89)
        ModernTextBox1.TabIndex = 11
        ModernTextBox1.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' HtmlColorLabel2
        ' 
        HtmlColorLabel2.AutoSize = True
        HtmlColorLabel2.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel2.Dock = DockStyle.Top
        HtmlColorLabel2.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HtmlColorLabel2.Location = New Point(20, 144)
        HtmlColorLabel2.Margin = New Padding(2)
        HtmlColorLabel2.Name = "HtmlColorLabel2"
        HtmlColorLabel2.Padding = New Padding(0, 20, 0, 10)
        HtmlColorLabel2.Size = New Size(726, 55)
        HtmlColorLabel2.TabIndex = 12
        HtmlColorLabel2.Text = "<span style=""font-size:13; color:Silver"">之前参数</span>   拼接在第一个 -i 的文件之后，通常用来导入更多文件"
        ' 
        ' ModernTextBox2
        ' 
        ModernTextBox2.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernTextBox2.BorderColor = Color.Transparent
        ModernTextBox2.BorderColorFocus = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        ModernTextBox2.BorderRadius = 10
        ModernTextBox2.BorderSize = 0
        ModernTextBox2.Dock = DockStyle.Top
        ModernTextBox2.Location = New Point(20, 199)
        ModernTextBox2.Margin = New Padding(2)
        ModernTextBox2.MultiLine = True
        ModernTextBox2.Name = "ModernTextBox2"
        ModernTextBox2.Padding = New Padding(15, 10, 15, 10)
        ModernTextBox2.Size = New Size(726, 89)
        ModernTextBox2.TabIndex = 13
        ModernTextBox2.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' HtmlColorLabel3
        ' 
        HtmlColorLabel3.AutoSize = True
        HtmlColorLabel3.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel3.Dock = DockStyle.Top
        HtmlColorLabel3.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HtmlColorLabel3.Location = New Point(20, 288)
        HtmlColorLabel3.Margin = New Padding(2)
        HtmlColorLabel3.Name = "HtmlColorLabel3"
        HtmlColorLabel3.Padding = New Padding(0, 20, 0, 10)
        HtmlColorLabel3.Size = New Size(726, 55)
        HtmlColorLabel3.TabIndex = 14
        HtmlColorLabel3.Text = "<span style=""font-size:13; color:Silver"">之后参数</span>   拼接在输出文件之前"
        ' 
        ' ModernTextBox3
        ' 
        ModernTextBox3.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernTextBox3.BorderColor = Color.Transparent
        ModernTextBox3.BorderColorFocus = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        ModernTextBox3.BorderRadius = 10
        ModernTextBox3.BorderSize = 0
        ModernTextBox3.Dock = DockStyle.Top
        ModernTextBox3.Location = New Point(20, 343)
        ModernTextBox3.Margin = New Padding(2)
        ModernTextBox3.MultiLine = True
        ModernTextBox3.Name = "ModernTextBox3"
        ModernTextBox3.Padding = New Padding(15, 10, 15, 10)
        ModernTextBox3.Size = New Size(726, 89)
        ModernTextBox3.TabIndex = 15
        ModernTextBox3.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' ModernTextBox4
        ' 
        ModernTextBox4.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernTextBox4.BorderColor = Color.Transparent
        ModernTextBox4.BorderColorFocus = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        ModernTextBox4.BorderRadius = 10
        ModernTextBox4.BorderSize = 0
        ModernTextBox4.Dock = DockStyle.Top
        ModernTextBox4.Location = New Point(20, 487)
        ModernTextBox4.Margin = New Padding(2)
        ModernTextBox4.MultiLine = True
        ModernTextBox4.Name = "ModernTextBox4"
        ModernTextBox4.Padding = New Padding(15, 10, 15, 10)
        ModernTextBox4.Size = New Size(726, 89)
        ModernTextBox4.TabIndex = 17
        ModernTextBox4.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' HtmlColorLabel4
        ' 
        HtmlColorLabel4.AutoSize = True
        HtmlColorLabel4.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel4.Dock = DockStyle.Top
        HtmlColorLabel4.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HtmlColorLabel4.Location = New Point(20, 432)
        HtmlColorLabel4.Margin = New Padding(2)
        HtmlColorLabel4.Name = "HtmlColorLabel4"
        HtmlColorLabel4.Padding = New Padding(0, 20, 0, 10)
        HtmlColorLabel4.Size = New Size(726, 55)
        HtmlColorLabel4.TabIndex = 16
        HtmlColorLabel4.Text = "<span style=""font-size:13; color:Silver"">最后参数</span>   拼接在输出文件之后，也就是最末尾的位置"
        ' 
        ' ModernPanel1
        ' 
        ModernPanel1.BackColor1 = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ModernPanel1.BorderSize = 0
        ModernPanel1.Controls.Add(ModernTextBox4)
        ModernPanel1.Controls.Add(HtmlColorLabel4)
        ModernPanel1.Controls.Add(ModernTextBox3)
        ModernPanel1.Controls.Add(HtmlColorLabel3)
        ModernPanel1.Controls.Add(ModernTextBox2)
        ModernPanel1.Controls.Add(HtmlColorLabel2)
        ModernPanel1.Controls.Add(ModernTextBox1)
        ModernPanel1.Controls.Add(HtmlColorLabel1)
        ModernPanel1.Dock = DockStyle.Fill
        ModernPanel1.Location = New Point(0, 0)
        ModernPanel1.Name = "ModernPanel1"
        ModernPanel1.Padding = New Padding(20)
        ModernPanel1.ScrollBarMode = LakeUI.ModernPanel.ScrollMode.None
        ModernPanel1.Size = New Size(766, 642)
        ModernPanel1.TabIndex = 18
        ' 
        ' Form_v6_参数面板_在位置插入参数
        ' 
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ClientSize = New Size(766, 642)
        Controls.Add(ModernPanel1)
        Font = New Font("Microsoft YaHei UI", 10F)
        ForeColor = Color.Silver
        Name = "Form_v6_参数面板_在位置插入参数"
        Text = "Form_v6_参数面板_在位置插入参数"
        ModernPanel1.ResumeLayout(False)
        ModernPanel1.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents HtmlColorLabel1 As LakeUI.HtmlColorLabel
    Friend WithEvents ModernTextBox1 As LakeUI.ModernTextBox
    Friend WithEvents HtmlColorLabel2 As LakeUI.HtmlColorLabel
    Friend WithEvents ModernTextBox2 As LakeUI.ModernTextBox
    Friend WithEvents HtmlColorLabel3 As LakeUI.HtmlColorLabel
    Friend WithEvents ModernTextBox3 As LakeUI.ModernTextBox
    Friend WithEvents ModernTextBox4 As LakeUI.ModernTextBox
    Friend WithEvents HtmlColorLabel4 As LakeUI.HtmlColorLabel
    Friend WithEvents ModernPanel1 As LakeUI.ModernPanel
End Class
