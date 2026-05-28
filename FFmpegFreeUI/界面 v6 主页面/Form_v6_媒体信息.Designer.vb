<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_v6_媒体信息
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
        ModernTextBox1 = New LakeUI.ModernTextBox()
        Panel2 = New Panel()
        MB_打开文件 = New LakeUI.ModernButton()
        ModernPanel1.SuspendLayout()
        Panel2.SuspendLayout()
        SuspendLayout()
        ' 
        ' ModernPanel1
        ' 
        ModernPanel1.BorderSize = 0
        ModernPanel1.Controls.Add(ModernTextBox1)
        ModernPanel1.Controls.Add(Panel2)
        ModernPanel1.Dock = DockStyle.Fill
        ModernPanel1.Location = New Point(0, 0)
        ModernPanel1.Name = "ModernPanel1"
        ModernPanel1.Padding = New Padding(20)
        ModernPanel1.ScrollBarMode = LakeUI.ModernPanel.ScrollMode.None
        ModernPanel1.Size = New Size(883, 645)
        ModernPanel1.TabIndex = 0
        ' 
        ' ModernTextBox1
        ' 
        ModernTextBox1.AllowDrop = True
        ModernTextBox1.BackColor1 = Color.FromArgb(CByte(20), CByte(220), CByte(220), CByte(220))
        ModernTextBox1.BorderColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernTextBox1.BorderColorFocus = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernTextBox1.BorderRadius = 10
        ModernTextBox1.Dock = DockStyle.Fill
        ModernTextBox1.Font = New Font("Microsoft YaHei UI", 11F)
        ModernTextBox1.LineNumberBackColor = Color.FromArgb(CByte(20), CByte(220), CByte(220), CByte(220))
        ModernTextBox1.LineNumberForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ModernTextBox1.LinkDetection = True
        ModernTextBox1.Location = New Point(20, 62)
        ModernTextBox1.Margin = New Padding(2)
        ModernTextBox1.MultiLine = True
        ModernTextBox1.Name = "ModernTextBox1"
        ModernTextBox1.Padding = New Padding(15, 10, 15, 10)
        ModernTextBox1.ScrollBarColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernTextBox1.ScrollBarHoverColor = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        ModernTextBox1.ScrollBarTrackColor = Color.FromArgb(CByte(20), CByte(220), CByte(220), CByte(220))
        ModernTextBox1.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernTextBox1.ShowLineNumbers = True
        ModernTextBox1.Size = New Size(843, 563)
        ModernTextBox1.TabIndex = 15
        ModernTextBox1.Text = "ModernTextBox1"
        ' 
        ' Panel2
        ' 
        Panel2.Controls.Add(MB_打开文件)
        Panel2.Dock = DockStyle.Top
        Panel2.Location = New Point(20, 20)
        Panel2.Name = "Panel2"
        Panel2.Padding = New Padding(0, 0, 0, 10)
        Panel2.Size = New Size(843, 42)
        Panel2.TabIndex = 14
        ' 
        ' MB_打开文件
        ' 
        MB_打开文件.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_打开文件.BorderRadius = 10
        MB_打开文件.BorderSize = 0
        MB_打开文件.Dock = DockStyle.Fill
        MB_打开文件.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_打开文件.Location = New Point(0, 0)
        MB_打开文件.Margin = New Padding(2)
        MB_打开文件.Name = "MB_打开文件"
        MB_打开文件.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_打开文件.Size = New Size(843, 32)
        MB_打开文件.TabIndex = 6
        MB_打开文件.Text = "调用 ffprobe.exe 直接展示输出信息，可以把文件直接拖到下方区域；此功能不兼容转译模式！"
        ' 
        ' Form_v6_媒体信息
        ' 
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        ClientSize = New Size(883, 645)
        Controls.Add(ModernPanel1)
        Font = New Font("Microsoft YaHei UI", 10F)
        ForeColor = Color.Silver
        Name = "Form_v6_媒体信息"
        Text = "Form_v6_媒体信息"
        ModernPanel1.ResumeLayout(False)
        Panel2.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents ModernPanel1 As LakeUI.ModernPanel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents MB_打开文件 As LakeUI.ModernButton
    Friend WithEvents ModernTextBox1 As LakeUI.ModernTextBox
End Class
