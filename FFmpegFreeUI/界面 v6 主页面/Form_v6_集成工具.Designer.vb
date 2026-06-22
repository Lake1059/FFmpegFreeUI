<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_v6_集成工具
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
        Dim ModernTabPage1 As LakeUI.ModernTabListControl.ModernTabPage = New LakeUI.ModernTabListControl.ModernTabPage()
        Dim ModernTabPage2 As LakeUI.ModernTabListControl.ModernTabPage = New LakeUI.ModernTabListControl.ModernTabPage()
        Dim ModernTabPage3 As LakeUI.ModernTabListControl.ModernTabPage = New LakeUI.ModernTabListControl.ModernTabPage()
        Dim ModernTabPage4 As LakeUI.ModernTabListControl.ModernTabPage = New LakeUI.ModernTabListControl.ModernTabPage()
        Dim ModernTabPage5 As LakeUI.ModernTabListControl.ModernTabPage = New LakeUI.ModernTabListControl.ModernTabPage()
        Dim ModernTabPage6 As LakeUI.ModernTabListControl.ModernTabPage = New LakeUI.ModernTabListControl.ModernTabPage()
        ModernPanel1 = New LakeUI.ModernPanel()
        ModernTabListControl1 = New LakeUI.ModernTabListControl()
        ModernPanel1.SuspendLayout()
        SuspendLayout()
        ' 
        ' ModernPanel1
        ' 
        ModernPanel1.BorderSize = 0
        ModernPanel1.Controls.Add(ModernTabListControl1)
        ModernPanel1.Dock = DockStyle.Fill
        ModernPanel1.Location = New Point(0, 0)
        ModernPanel1.Name = "ModernPanel1"
        ModernPanel1.Size = New Size(848, 579)
        ModernPanel1.TabIndex = 3
        ' 
        ' ModernTabListControl1
        ' 
        ModernTabListControl1.AnimationDuration = 0
        ModernTabListControl1.BackColor = Color.Transparent
        ModernTabListControl1.Dock = DockStyle.Fill
        ModernTabPage1.Text = "合并（头尾相接）"
        ModernTabPage2.Text = "混流（布置轨道）"
        ModernTabPage3.Text = "抽流（取出纯流）"
        ModernTabPage4.IsSeparator = True
        ModernTabPage5.Text = "质量评测"
        ModernTabPage6.Text = "Whisper 字幕生成"
        ModernTabListControl1.Items.Add(ModernTabPage1)
        ModernTabListControl1.Items.Add(ModernTabPage2)
        ModernTabListControl1.Items.Add(ModernTabPage3)
        ModernTabListControl1.Items.Add(ModernTabPage4)
        ModernTabListControl1.Items.Add(ModernTabPage5)
        ModernTabListControl1.Items.Add(ModernTabPage6)
        ModernTabListControl1.Location = New Point(0, 0)
        ModernTabListControl1.Name = "ModernTabListControl1"
        ModernTabListControl1.ScrollBarThumbColor = Color.FromArgb(CByte(40), CByte(200), CByte(200), CByte(200))
        ModernTabListControl1.ScrollBarThumbHoverColor = Color.FromArgb(CByte(80), CByte(200), CByte(200), CByte(200))
        ModernTabListControl1.ScrollBarTrackColor = Color.FromArgb(CByte(20), CByte(200), CByte(200), CByte(200))
        ModernTabListControl1.ScrollBarWidth = 8
        ModernTabListControl1.SeparatorColor = Color.FromArgb(CByte(80), CByte(200), CByte(200), CByte(200))
        ModernTabListControl1.Size = New Size(848, 579)
        ModernTabListControl1.TabIndex = 3
        ModernTabListControl1.TabItemHeight = 32
        ModernTabListControl1.TabItemHoverBackColor = Color.FromArgb(CByte(40), CByte(200), CByte(200), CByte(200))
        ModernTabListControl1.TabItemSelectedBackColor = Color.FromArgb(CByte(80), CByte(200), CByte(200), CByte(200))
        ModernTabListControl1.TabItemSpacing = 0
        ModernTabListControl1.TabStripBackColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        ModernTabListControl1.TabStripWidth = 210
        ' 
        ' Form_v6_集成工具
        ' 
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ClientSize = New Size(848, 579)
        Controls.Add(ModernPanel1)
        Font = New Font("Microsoft YaHei UI", 10F)
        ForeColor = Color.Silver
        Name = "Form_v6_集成工具"
        Text = "Form_v6_集成工具"
        ModernPanel1.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents ModernPanel1 As LakeUI.ModernPanel
    Friend WithEvents ModernTabListControl1 As LakeUI.ModernTabListControl
End Class
