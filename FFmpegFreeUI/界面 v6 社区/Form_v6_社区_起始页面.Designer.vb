<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form_v6_社区_起始页面
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        ModernPanel1 = New LakeUI.ModernPanel()
        MarkDownViewer1 = New LakeUI.MarkDownViewer()
        ModernPanel1.SuspendLayout()
        SuspendLayout()
        ' 
        ' ModernPanel1
        ' 
        ModernPanel1.BorderSize = 0
        ModernPanel1.Controls.Add(MarkDownViewer1)
        ModernPanel1.Dock = DockStyle.Fill
        ModernPanel1.Location = New Point(0, 0)
        ModernPanel1.Name = "ModernPanel1"
        ModernPanel1.Padding = New Padding(20)
        ModernPanel1.Size = New Size(958, 606)
        ModernPanel1.TabIndex = 0
        ' 
        ' MarkDownViewer1
        ' 
        MarkDownViewer1.BackColor1 = Color.Transparent
        MarkDownViewer1.BasePath = Nothing
        MarkDownViewer1.Dock = DockStyle.Fill
        MarkDownViewer1.Font = New Font("Microsoft YaHei UI", 10F)
        MarkDownViewer1.ForeColor = Color.FromArgb(CByte(160), CByte(255), CByte(255), CByte(255))
        MarkDownViewer1.HeadingColor = Color.FromArgb(CByte(160), CByte(255), CByte(255), CByte(255))
        MarkDownViewer1.HorizontalRuleColor = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MarkDownViewer1.HorizontalRuleThickness = 2
        MarkDownViewer1.Location = New Point(20, 20)
        MarkDownViewer1.Name = "MarkDownViewer1"
        MarkDownViewer1.Size = New Size(918, 566)
        MarkDownViewer1.TabIndex = 14
        ' 
        ' Form_v6_社区_起始页面
        ' 
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        ClientSize = New Size(958, 606)
        Controls.Add(ModernPanel1)
        Font = New Font("Microsoft YaHei UI", 10F)
        ForeColor = Color.Silver
        Name = "Form_v6_社区_起始页面"
        Text = "Form_v6_社区_起始页面"
        ModernPanel1.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents ModernPanel1 As LakeUI.ModernPanel
    Friend WithEvents MarkDownViewer1 As LakeUI.MarkDownViewer
End Class
