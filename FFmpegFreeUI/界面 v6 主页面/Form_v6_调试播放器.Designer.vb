<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_v6_调试播放器
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
        ModernPanel2 = New LakeUI.ModernPanel()
        Panel2 = New Panel()
        ExcellentProgressBar1 = New LakeUI.ExcellentProgressBar()
        JustEmptyControl2 = New LakeUI.JustEmptyControl()
        MB_关闭 = New LakeUI.ModernButton()
        JustEmptyControl1 = New LakeUI.JustEmptyControl()
        MB_打开 = New LakeUI.ModernButton()
        ModernPanel1.SuspendLayout()
        Panel2.SuspendLayout()
        SuspendLayout()
        ' 
        ' ModernPanel1
        ' 
        ModernPanel1.BorderSize = 0
        ModernPanel1.Controls.Add(ModernPanel2)
        ModernPanel1.Controls.Add(Panel2)
        ModernPanel1.Dock = DockStyle.Fill
        ModernPanel1.Location = New Point(0, 0)
        ModernPanel1.Name = "ModernPanel1"
        ModernPanel1.Padding = New Padding(20)
        ModernPanel1.Size = New Size(849, 638)
        ModernPanel1.TabIndex = 0
        ' 
        ' ModernPanel2
        ' 
        ModernPanel2.BackColor1 = Color.Transparent
        ModernPanel2.BorderSize = 0
        ModernPanel2.Dock = DockStyle.Fill
        ModernPanel2.Location = New Point(20, 62)
        ModernPanel2.Name = "ModernPanel2"
        ModernPanel2.Size = New Size(809, 556)
        ModernPanel2.TabIndex = 15
        ' 
        ' Panel2
        ' 
        Panel2.Controls.Add(ExcellentProgressBar1)
        Panel2.Controls.Add(JustEmptyControl2)
        Panel2.Controls.Add(MB_关闭)
        Panel2.Controls.Add(JustEmptyControl1)
        Panel2.Controls.Add(MB_打开)
        Panel2.Dock = DockStyle.Top
        Panel2.Location = New Point(20, 20)
        Panel2.Name = "Panel2"
        Panel2.Padding = New Padding(0, 0, 0, 10)
        Panel2.Size = New Size(809, 42)
        Panel2.TabIndex = 14
        ' 
        ' ExcellentProgressBar1
        ' 
        ExcellentProgressBar1.BorderRadius = 10
        ExcellentProgressBar1.Dock = DockStyle.Fill
        ExcellentProgressBar1.FillColor = Color.FromArgb(CByte(180), CByte(107), CByte(142), CByte(35))
        ExcellentProgressBar1.Font = New Font("Microsoft YaHei UI", 10F, FontStyle.Bold)
        ExcellentProgressBar1.ForeColor = Color.FromArgb(CByte(160), CByte(255), CByte(255), CByte(255))
        ExcellentProgressBar1.Location = New Point(180, 0)
        ExcellentProgressBar1.Margin = New Padding(2, 2, 2, 2)
        ExcellentProgressBar1.Name = "ExcellentProgressBar1"
        ExcellentProgressBar1.Padding = New Padding(5)
        ExcellentProgressBar1.Size = New Size(629, 32)
        ExcellentProgressBar1.TabIndex = 0
        ExcellentProgressBar1.TextPadding = New Padding(0)
        ExcellentProgressBar1.TrackColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ExcellentProgressBar1.Value = 50
        ' 
        ' JustEmptyControl2
        ' 
        JustEmptyControl2.Dock = DockStyle.Left
        JustEmptyControl2.Location = New Point(170, 0)
        JustEmptyControl2.Name = "JustEmptyControl2"
        JustEmptyControl2.Size = New Size(10, 32)
        JustEmptyControl2.TabIndex = 6
        ' 
        ' MB_关闭
        ' 
        MB_关闭.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_关闭.BorderRadius = 10
        MB_关闭.BorderSize = 0
        MB_关闭.Dock = DockStyle.Left
        MB_关闭.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_关闭.Location = New Point(90, 0)
        MB_关闭.Margin = New Padding(2)
        MB_关闭.Name = "MB_关闭"
        MB_关闭.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_关闭.Size = New Size(80, 32)
        MB_关闭.TabIndex = 5
        MB_关闭.Text = "关闭"
        ' 
        ' JustEmptyControl1
        ' 
        JustEmptyControl1.Dock = DockStyle.Left
        JustEmptyControl1.Location = New Point(80, 0)
        JustEmptyControl1.Name = "JustEmptyControl1"
        JustEmptyControl1.Size = New Size(10, 32)
        JustEmptyControl1.TabIndex = 4
        ' 
        ' MB_打开
        ' 
        MB_打开.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_打开.BorderRadius = 10
        MB_打开.BorderSize = 0
        MB_打开.Dock = DockStyle.Left
        MB_打开.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_打开.Location = New Point(0, 0)
        MB_打开.Margin = New Padding(2)
        MB_打开.Name = "MB_打开"
        MB_打开.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_打开.Size = New Size(80, 32)
        MB_打开.TabIndex = 3
        MB_打开.Text = "打开"
        ' 
        ' Form_v6_调试播放器
        ' 
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        ClientSize = New Size(849, 638)
        Controls.Add(ModernPanel1)
        Font = New Font("Microsoft YaHei UI", 10F)
        ForeColor = Color.Transparent
        Name = "Form_v6_调试播放器"
        Text = "Form_v6_调试播放器"
        ModernPanel1.ResumeLayout(False)
        Panel2.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents ModernPanel1 As LakeUI.ModernPanel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents MB_关闭 As LakeUI.ModernButton
    Friend WithEvents JustEmptyControl1 As LakeUI.JustEmptyControl
    Friend WithEvents MB_打开 As LakeUI.ModernButton
    Friend WithEvents ModernPanel2 As LakeUI.ModernPanel
    Friend WithEvents JustEmptyControl2 As LakeUI.JustEmptyControl
    Friend WithEvents ExcellentProgressBar1 As LakeUI.ExcellentProgressBar
End Class
