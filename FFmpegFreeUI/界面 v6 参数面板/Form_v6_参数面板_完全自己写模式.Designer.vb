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
        HCL_完全自己写模式警告 = New LakeUI.HtmlColorLabel()
        MTB_完整命令行参数 = New LakeUI.ModernTextBox()
        HCL_完整命令行参数说明 = New LakeUI.HtmlColorLabel()
        Panel5 = New Panel()
        MB_插入输出文件占位符 = New LakeUI.ModernButton()
        JustEmptyControl1 = New LakeUI.JustEmptyControl()
        MB_插入输入文件占位符 = New LakeUI.ModernButton()
        ModernPanel1 = New LakeUI.ModernPanel()
        Panel5.SuspendLayout()
        ModernPanel1.SuspendLayout()
        SuspendLayout()
        ' 
        ' HCL_完全自己写模式警告
        ' 
        HCL_完全自己写模式警告.AutoSize = True
        HCL_完全自己写模式警告.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_完全自己写模式警告.Dock = DockStyle.Top
        HCL_完全自己写模式警告.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_完全自己写模式警告.Location = New Point(20, 20)
        HCL_完全自己写模式警告.Margin = New Padding(2)
        HCL_完全自己写模式警告.Name = "HCL_完全自己写模式警告"
        HCL_完全自己写模式警告.Padding = New Padding(0, 0, 0, 5)
        HCL_完全自己写模式警告.Size = New Size(770, 30)
        HCL_完全自己写模式警告.TabIndex = 10
        HCL_完全自己写模式警告.Text = "<span style=""font-size:13; color:Silver"">完全自己写</span>   <span style=""font-size:10pt; color:IndianRed"">使用此模式时，其他所有参数全都不生效！</span>"
        ' 
        ' MTB_完整命令行参数
        ' 
        MTB_完整命令行参数.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_完整命令行参数.BorderColor = Color.Transparent
        MTB_完整命令行参数.BorderColorFocus = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MTB_完整命令行参数.BorderRadius = 10
        MTB_完整命令行参数.Dock = DockStyle.Fill
        MTB_完整命令行参数.LineNumberBackColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_完整命令行参数.LineNumberForeColor = Color.Silver
        MTB_完整命令行参数.Location = New Point(20, 173)
        MTB_完整命令行参数.Margin = New Padding(2)
        MTB_完整命令行参数.MultiLine = True
        MTB_完整命令行参数.Name = "MTB_完整命令行参数"
        MTB_完整命令行参数.Padding = New Padding(10, 8, 10, 8)
        MTB_完整命令行参数.ScrollBarColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_完整命令行参数.ScrollBarHoverColor = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MTB_完整命令行参数.ScrollBarTrackColor = Color.FromArgb(CByte(20), CByte(220), CByte(220), CByte(220))
        MTB_完整命令行参数.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_完整命令行参数.ShowLineNumbers = True
        MTB_完整命令行参数.Size = New Size(770, 471)
        MTB_完整命令行参数.TabIndex = 11
        MTB_完整命令行参数.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' HCL_完整命令行参数说明
        ' 
        HCL_完整命令行参数说明.AutoSize = True
        HCL_完整命令行参数说明.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_完整命令行参数说明.Dock = DockStyle.Top
        HCL_完整命令行参数说明.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_完整命令行参数说明.Location = New Point(20, 50)
        HCL_完整命令行参数说明.Margin = New Padding(2)
        HCL_完整命令行参数说明.Name = "HCL_完整命令行参数说明"
        HCL_完整命令行参数说明.Padding = New Padding(0, 0, 0, 10)
        HCL_完整命令行参数说明.Size = New Size(770, 71)
        HCL_完整命令行参数说明.TabIndex = 14
        HCL_完整命令行参数说明.Text = "不要包含开头的 ffmpeg，这里是直接给其的参数<br>用 <span style=""color:YellowGreen""><InputFile></span> 表示输入文件，用 <span style=""color:YellowGreen""><OutputFile></span> 表示输出文件<br>不会自动写引号，注意区分大小写，可以直接用插入功能"
        ' 
        ' Panel5
        ' 
        Panel5.Controls.Add(MB_插入输出文件占位符)
        Panel5.Controls.Add(JustEmptyControl1)
        Panel5.Controls.Add(MB_插入输入文件占位符)
        Panel5.Dock = DockStyle.Top
        Panel5.Location = New Point(20, 121)
        Panel5.Name = "Panel5"
        Panel5.Padding = New Padding(0, 0, 0, 20)
        Panel5.Size = New Size(770, 52)
        Panel5.TabIndex = 15
        ' 
        ' MB_插入输出文件占位符
        ' 
        MB_插入输出文件占位符.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_插入输出文件占位符.BorderRadius = 10
        MB_插入输出文件占位符.BorderSize = 0
        MB_插入输出文件占位符.Dock = DockStyle.Left
        MB_插入输出文件占位符.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_插入输出文件占位符.Location = New Point(211, 0)
        MB_插入输出文件占位符.Margin = New Padding(2)
        MB_插入输出文件占位符.Name = "MB_插入输出文件占位符"
        MB_插入输出文件占位符.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_插入输出文件占位符.Size = New Size(200, 32)
        MB_插入输出文件占位符.TabIndex = 7
        MB_插入输出文件占位符.Text = "插入输出文件"
        ' 
        ' JustEmptyControl1
        ' 
        JustEmptyControl1.Dock = DockStyle.Left
        JustEmptyControl1.Location = New Point(200, 0)
        JustEmptyControl1.Name = "JustEmptyControl1"
        JustEmptyControl1.Size = New Size(11, 32)
        JustEmptyControl1.TabIndex = 5
        ' 
        ' MB_插入输入文件占位符
        ' 
        MB_插入输入文件占位符.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_插入输入文件占位符.BorderRadius = 10
        MB_插入输入文件占位符.BorderSize = 0
        MB_插入输入文件占位符.Dock = DockStyle.Left
        MB_插入输入文件占位符.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_插入输入文件占位符.Location = New Point(0, 0)
        MB_插入输入文件占位符.Margin = New Padding(2)
        MB_插入输入文件占位符.Name = "MB_插入输入文件占位符"
        MB_插入输入文件占位符.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_插入输入文件占位符.Size = New Size(200, 32)
        MB_插入输入文件占位符.TabIndex = 6
        MB_插入输入文件占位符.Text = "插入输入文件"
        ' 
        ' ModernPanel1
        ' 
        ModernPanel1.BackColor1 = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ModernPanel1.BorderSize = 0
        ModernPanel1.Controls.Add(MTB_完整命令行参数)
        ModernPanel1.Controls.Add(Panel5)
        ModernPanel1.Controls.Add(HCL_完整命令行参数说明)
        ModernPanel1.Controls.Add(HCL_完全自己写模式警告)
        ModernPanel1.Dock = DockStyle.Fill
        ModernPanel1.Location = New Point(0, 0)
        ModernPanel1.Name = "ModernPanel1"
        ModernPanel1.Padding = New Padding(20)
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

    Friend WithEvents HCL_完全自己写模式警告 As LakeUI.HtmlColorLabel
    Friend WithEvents MTB_完整命令行参数 As LakeUI.ModernTextBox
    Friend WithEvents HCL_完整命令行参数说明 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents JustEmptyControl1 As LakeUI.JustEmptyControl
    Friend WithEvents MB_插入输出文件占位符 As LakeUI.ModernButton
    Friend WithEvents MB_插入输入文件占位符 As LakeUI.ModernButton
    Friend WithEvents ModernPanel1 As LakeUI.ModernPanel
End Class
