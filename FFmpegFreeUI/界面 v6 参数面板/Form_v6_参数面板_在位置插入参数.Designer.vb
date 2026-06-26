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
        HCL_开头参数说明 = New LakeUI.HtmlColorLabel()
        MTB_开头参数 = New LakeUI.ModernTextBox()
        HCL_之前参数说明 = New LakeUI.HtmlColorLabel()
        MTB_之前参数 = New LakeUI.ModernTextBox()
        HCL_之后参数说明 = New LakeUI.HtmlColorLabel()
        MTB_之后参数 = New LakeUI.ModernTextBox()
        MTB_最后参数 = New LakeUI.ModernTextBox()
        HCL_最后参数说明 = New LakeUI.HtmlColorLabel()
        ModernPanel1 = New LakeUI.ModernPanel()
        ModernPanel1.SuspendLayout()
        SuspendLayout()
        '
        ' HCL_开头参数说明
        '
        HCL_开头参数说明.AutoSize = True
        HCL_开头参数说明.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_开头参数说明.Dock = DockStyle.Top
        HCL_开头参数说明.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_开头参数说明.Location = New Point(20, 20)
        HCL_开头参数说明.Margin = New Padding(2)
        HCL_开头参数说明.Name = "HCL_开头参数说明"
        HCL_开头参数说明.Padding = New Padding(0, 0, 0, 10)
        HCL_开头参数说明.Size = New Size(726, 35)
        HCL_开头参数说明.TabIndex = 10
        HCL_开头参数说明.Text = "<span style=""font-size:13; color:Silver"">开头参数</span>   拼接在输入文件之前（ffmpeg 之后，-i 之前）"
        '
        ' MTB_开头参数
        '
        MTB_开头参数.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_开头参数.BorderColor = Color.Transparent
        MTB_开头参数.BorderColorFocus = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MTB_开头参数.BorderRadius = 10
        MTB_开头参数.BorderSize = 0
        MTB_开头参数.Dock = DockStyle.Top
        MTB_开头参数.LineNumberBackColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_开头参数.LineNumberForeColor = Color.Silver
        MTB_开头参数.Location = New Point(20, 55)
        MTB_开头参数.Margin = New Padding(2)
        MTB_开头参数.MultiLine = True
        MTB_开头参数.Name = "MTB_开头参数"
        MTB_开头参数.Padding = New Padding(15, 10, 15, 10)
        MTB_开头参数.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_开头参数.ShowLineNumbers = True
        MTB_开头参数.Size = New Size(726, 89)
        MTB_开头参数.TabIndex = 11
        MTB_开头参数.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        '
        ' HCL_之前参数说明
        '
        HCL_之前参数说明.AutoSize = True
        HCL_之前参数说明.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_之前参数说明.Dock = DockStyle.Top
        HCL_之前参数说明.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_之前参数说明.Location = New Point(20, 144)
        HCL_之前参数说明.Margin = New Padding(2)
        HCL_之前参数说明.Name = "HCL_之前参数说明"
        HCL_之前参数说明.Padding = New Padding(0, 20, 0, 10)
        HCL_之前参数说明.Size = New Size(726, 55)
        HCL_之前参数说明.TabIndex = 12
        HCL_之前参数说明.Text = "<span style=""font-size:13; color:Silver"">之前参数</span>   拼接在第一个 -i 的文件之后，通常用来导入更多文件"
        '
        ' MTB_之前参数
        '
        MTB_之前参数.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_之前参数.BorderColor = Color.Transparent
        MTB_之前参数.BorderColorFocus = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MTB_之前参数.BorderRadius = 10
        MTB_之前参数.BorderSize = 0
        MTB_之前参数.Dock = DockStyle.Top
        MTB_之前参数.LineNumberBackColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_之前参数.LineNumberForeColor = Color.Silver
        MTB_之前参数.Location = New Point(20, 199)
        MTB_之前参数.Margin = New Padding(2)
        MTB_之前参数.MultiLine = True
        MTB_之前参数.Name = "MTB_之前参数"
        MTB_之前参数.Padding = New Padding(15, 10, 15, 10)
        MTB_之前参数.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_之前参数.ShowLineNumbers = True
        MTB_之前参数.Size = New Size(726, 89)
        MTB_之前参数.TabIndex = 13
        MTB_之前参数.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        '
        ' HCL_之后参数说明
        '
        HCL_之后参数说明.AutoSize = True
        HCL_之后参数说明.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_之后参数说明.Dock = DockStyle.Top
        HCL_之后参数说明.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_之后参数说明.Location = New Point(20, 288)
        HCL_之后参数说明.Margin = New Padding(2)
        HCL_之后参数说明.Name = "HCL_之后参数说明"
        HCL_之后参数说明.Padding = New Padding(0, 20, 0, 10)
        HCL_之后参数说明.Size = New Size(726, 55)
        HCL_之后参数说明.TabIndex = 14
        HCL_之后参数说明.Text = "<span style=""font-size:13; color:Silver"">之后参数</span>   拼接在输出文件之前，在前面所有参数之后"
        '
        ' MTB_之后参数
        '
        MTB_之后参数.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_之后参数.BorderColor = Color.Transparent
        MTB_之后参数.BorderColorFocus = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MTB_之后参数.BorderRadius = 10
        MTB_之后参数.BorderSize = 0
        MTB_之后参数.Dock = DockStyle.Top
        MTB_之后参数.LineNumberBackColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_之后参数.LineNumberForeColor = Color.Silver
        MTB_之后参数.Location = New Point(20, 343)
        MTB_之后参数.Margin = New Padding(2)
        MTB_之后参数.MultiLine = True
        MTB_之后参数.Name = "MTB_之后参数"
        MTB_之后参数.Padding = New Padding(15, 10, 15, 10)
        MTB_之后参数.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_之后参数.ShowLineNumbers = True
        MTB_之后参数.Size = New Size(726, 89)
        MTB_之后参数.TabIndex = 15
        MTB_之后参数.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        '
        ' MTB_最后参数
        '
        MTB_最后参数.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_最后参数.BorderColor = Color.Transparent
        MTB_最后参数.BorderColorFocus = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MTB_最后参数.BorderRadius = 10
        MTB_最后参数.BorderSize = 0
        MTB_最后参数.Dock = DockStyle.Top
        MTB_最后参数.LineNumberBackColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_最后参数.LineNumberForeColor = Color.Silver
        MTB_最后参数.Location = New Point(20, 487)
        MTB_最后参数.Margin = New Padding(2)
        MTB_最后参数.MultiLine = True
        MTB_最后参数.Name = "MTB_最后参数"
        MTB_最后参数.Padding = New Padding(15, 10, 15, 10)
        MTB_最后参数.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_最后参数.ShowLineNumbers = True
        MTB_最后参数.Size = New Size(726, 89)
        MTB_最后参数.TabIndex = 17
        MTB_最后参数.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        '
        ' HCL_最后参数说明
        '
        HCL_最后参数说明.AutoSize = True
        HCL_最后参数说明.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_最后参数说明.Dock = DockStyle.Top
        HCL_最后参数说明.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_最后参数说明.Location = New Point(20, 432)
        HCL_最后参数说明.Margin = New Padding(2)
        HCL_最后参数说明.Name = "HCL_最后参数说明"
        HCL_最后参数说明.Padding = New Padding(0, 20, 0, 10)
        HCL_最后参数说明.Size = New Size(726, 55)
        HCL_最后参数说明.TabIndex = 16
        HCL_最后参数说明.Text = "<span style=""font-size:13; color:Silver"">最后参数</span>   拼接在输出文件之后，也就是最末尾的位置"
        '
        ' ModernPanel1
        '
        ModernPanel1.BackColor1 = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ModernPanel1.BorderSize = 0
        ModernPanel1.Controls.Add(MTB_最后参数)
        ModernPanel1.Controls.Add(HCL_最后参数说明)
        ModernPanel1.Controls.Add(MTB_之后参数)
        ModernPanel1.Controls.Add(HCL_之后参数说明)
        ModernPanel1.Controls.Add(MTB_之前参数)
        ModernPanel1.Controls.Add(HCL_之前参数说明)
        ModernPanel1.Controls.Add(MTB_开头参数)
        ModernPanel1.Controls.Add(HCL_开头参数说明)
        ModernPanel1.Dock = DockStyle.Fill
        ModernPanel1.Location = New Point(0, 0)
        ModernPanel1.Name = "ModernPanel1"
        ModernPanel1.Padding = New Padding(20)
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

    Friend WithEvents HCL_开头参数说明 As LakeUI.HtmlColorLabel
    Friend WithEvents MTB_开头参数 As LakeUI.ModernTextBox
    Friend WithEvents HCL_之前参数说明 As LakeUI.HtmlColorLabel
    Friend WithEvents MTB_之前参数 As LakeUI.ModernTextBox
    Friend WithEvents HCL_之后参数说明 As LakeUI.HtmlColorLabel
    Friend WithEvents MTB_之后参数 As LakeUI.ModernTextBox
    Friend WithEvents MTB_最后参数 As LakeUI.ModernTextBox
    Friend WithEvents HCL_最后参数说明 As LakeUI.HtmlColorLabel
    Friend WithEvents ModernPanel1 As LakeUI.ModernPanel
End Class
