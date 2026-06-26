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
        HCL_视频流自定义参数说明 = New LakeUI.HtmlColorLabel()
        MTB_视频流自定义参数 = New LakeUI.ModernTextBox()
        HCL_音频流自定义参数说明 = New LakeUI.HtmlColorLabel()
        HCL_字幕自定义参数说明 = New LakeUI.HtmlColorLabel()
        MTB_音频流自定义参数 = New LakeUI.ModernTextBox()
        ModernPanel1 = New LakeUI.ModernPanel()
        ModernPanel1.SuspendLayout()
        SuspendLayout()
        '
        ' HCL_视频流自定义参数说明
        '
        HCL_视频流自定义参数说明.AutoSize = True
        HCL_视频流自定义参数说明.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_视频流自定义参数说明.Dock = DockStyle.Top
        HCL_视频流自定义参数说明.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_视频流自定义参数说明.Location = New Point(20, 20)
        HCL_视频流自定义参数说明.Margin = New Padding(2)
        HCL_视频流自定义参数说明.Name = "HCL_视频流自定义参数说明"
        HCL_视频流自定义参数说明.Padding = New Padding(0, 0, 0, 10)
        HCL_视频流自定义参数说明.Size = New Size(722, 35)
        HCL_视频流自定义参数说明.TabIndex = 9
        HCL_视频流自定义参数说明.Text = "<span style=""font-size:13; color:Silver"">视频流参数</span>   如果要写滤镜请用滤镜排序功能，否则逻辑会冲突"
        '
        ' MTB_视频流自定义参数
        '
        MTB_视频流自定义参数.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_视频流自定义参数.BorderColor = Color.Transparent
        MTB_视频流自定义参数.BorderColorFocus = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MTB_视频流自定义参数.BorderRadius = 10
        MTB_视频流自定义参数.Dock = DockStyle.Top
        MTB_视频流自定义参数.LineNumberBackColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_视频流自定义参数.LineNumberForeColor = Color.Silver
        MTB_视频流自定义参数.Location = New Point(20, 55)
        MTB_视频流自定义参数.Margin = New Padding(2)
        MTB_视频流自定义参数.MultiLine = True
        MTB_视频流自定义参数.Name = "MTB_视频流自定义参数"
        MTB_视频流自定义参数.Padding = New Padding(15, 10, 15, 10)
        MTB_视频流自定义参数.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_视频流自定义参数.ShowLineNumbers = True
        MTB_视频流自定义参数.Size = New Size(722, 256)
        MTB_视频流自定义参数.TabIndex = 10
        MTB_视频流自定义参数.WaterText = "拼接在已生成部分的末尾，图片参数也是用这个"
        MTB_视频流自定义参数.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        '
        ' HCL_音频流自定义参数说明
        '
        HCL_音频流自定义参数说明.AutoSize = True
        HCL_音频流自定义参数说明.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_音频流自定义参数说明.Dock = DockStyle.Top
        HCL_音频流自定义参数说明.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_音频流自定义参数说明.Location = New Point(20, 311)
        HCL_音频流自定义参数说明.Margin = New Padding(2)
        HCL_音频流自定义参数说明.Name = "HCL_音频流自定义参数说明"
        HCL_音频流自定义参数说明.Padding = New Padding(0, 20, 0, 10)
        HCL_音频流自定义参数说明.Size = New Size(722, 55)
        HCL_音频流自定义参数说明.TabIndex = 11
        HCL_音频流自定义参数说明.Text = "<span style=""font-size:13; color:Silver"">音频流参数</span>   如果要写滤镜请用滤镜排序功能，否则逻辑会冲突"
        '
        ' HCL_字幕自定义参数说明
        '
        HCL_字幕自定义参数说明.AutoSize = True
        HCL_字幕自定义参数说明.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_字幕自定义参数说明.Dock = DockStyle.Bottom
        HCL_字幕自定义参数说明.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_字幕自定义参数说明.Location = New Point(20, 558)
        HCL_字幕自定义参数说明.Margin = New Padding(2)
        HCL_字幕自定义参数说明.Name = "HCL_字幕自定义参数说明"
        HCL_字幕自定义参数说明.Padding = New Padding(0, 20, 0, 0)
        HCL_字幕自定义参数说明.Size = New Size(722, 45)
        HCL_字幕自定义参数说明.TabIndex = 12
        HCL_字幕自定义参数说明.Text = "<span style=""font-size:13; color:Silver"">字幕？</span>   请使用自带相关功能，或者使用在位置插入功能"
        '
        ' MTB_音频流自定义参数
        '
        MTB_音频流自定义参数.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_音频流自定义参数.BorderColor = Color.Transparent
        MTB_音频流自定义参数.BorderColorFocus = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MTB_音频流自定义参数.BorderRadius = 10
        MTB_音频流自定义参数.Dock = DockStyle.Fill
        MTB_音频流自定义参数.LineNumberBackColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_音频流自定义参数.LineNumberForeColor = Color.Silver
        MTB_音频流自定义参数.Location = New Point(20, 366)
        MTB_音频流自定义参数.Margin = New Padding(2)
        MTB_音频流自定义参数.MultiLine = True
        MTB_音频流自定义参数.Name = "MTB_音频流自定义参数"
        MTB_音频流自定义参数.Padding = New Padding(15, 10, 15, 10)
        MTB_音频流自定义参数.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_音频流自定义参数.ShowLineNumbers = True
        MTB_音频流自定义参数.Size = New Size(722, 192)
        MTB_音频流自定义参数.TabIndex = 13
        MTB_音频流自定义参数.WaterText = "拼接在已生成部分的末尾"
        MTB_音频流自定义参数.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        '
        ' ModernPanel1
        '
        ModernPanel1.BackColor1 = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ModernPanel1.BorderSize = 0
        ModernPanel1.Controls.Add(MTB_音频流自定义参数)
        ModernPanel1.Controls.Add(HCL_字幕自定义参数说明)
        ModernPanel1.Controls.Add(HCL_音频流自定义参数说明)
        ModernPanel1.Controls.Add(MTB_视频流自定义参数)
        ModernPanel1.Controls.Add(HCL_视频流自定义参数说明)
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

    Friend WithEvents HCL_视频流自定义参数说明 As LakeUI.HtmlColorLabel
    Friend WithEvents MTB_视频流自定义参数 As LakeUI.ModernTextBox
    Friend WithEvents HCL_音频流自定义参数说明 As LakeUI.HtmlColorLabel
    Friend WithEvents HCL_字幕自定义参数说明 As LakeUI.HtmlColorLabel
    Friend WithEvents MTB_音频流自定义参数 As LakeUI.ModernTextBox
    Friend WithEvents ModernPanel1 As LakeUI.ModernPanel
End Class
