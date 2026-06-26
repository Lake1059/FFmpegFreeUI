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
        ModernButton2 = New LakeUI.ModernButton()
        JustEmptyControl1 = New LakeUI.JustEmptyControl()
        ModernButton1 = New LakeUI.ModernButton()
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
        ModernPanel1.ScrollBarMode = LakeUI.ModernPanel.ScrollMode.None
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
        ModernPanel2.ScrollBarMode = LakeUI.ModernPanel.ScrollMode.None
        ModernPanel2.Size = New Size(809, 556)
        ModernPanel2.TabIndex = 15
        '
        ' Panel2
        '
        Panel2.Controls.Add(ModernButton2)
        Panel2.Controls.Add(JustEmptyControl1)
        Panel2.Controls.Add(ModernButton1)
        Panel2.Dock = DockStyle.Top
        Panel2.Location = New Point(20, 20)
        Panel2.Name = "Panel2"
        Panel2.Padding = New Padding(0, 0, 0, 10)
        Panel2.Size = New Size(809, 42)
        Panel2.TabIndex = 14
        '
        ' ModernButton2
        '
        ModernButton2.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernButton2.BorderRadius = 10
        ModernButton2.BorderSize = 0
        ModernButton2.Dock = DockStyle.Left
        ModernButton2.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        ModernButton2.Location = New Point(110, 0)
        ModernButton2.Margin = New Padding(2)
        ModernButton2.Name = "ModernButton2"
        ModernButton2.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        ModernButton2.Size = New Size(100, 32)
        ModernButton2.TabIndex = 5
        ModernButton2.Text = "关闭"
        '
        ' JustEmptyControl1
        '
        JustEmptyControl1.Dock = DockStyle.Left
        JustEmptyControl1.Location = New Point(100, 0)
        JustEmptyControl1.Name = "JustEmptyControl1"
        JustEmptyControl1.Size = New Size(10, 32)
        JustEmptyControl1.TabIndex = 4
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
        ModernButton1.Size = New Size(100, 32)
        ModernButton1.TabIndex = 3
        ModernButton1.Text = "打开"
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
    Friend WithEvents ModernButton2 As LakeUI.ModernButton
    Friend WithEvents JustEmptyControl1 As LakeUI.JustEmptyControl
    Friend WithEvents ModernButton1 As LakeUI.ModernButton
    Friend WithEvents ModernPanel2 As LakeUI.ModernPanel
End Class
