<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_v6_参数面板_自定义参数说明
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
        MDV_自定义参数说明 = New LakeUI.MarkDownViewer()
        ModernPanel1 = New LakeUI.ModernPanel()
        ModernPanel1.SuspendLayout()
        SuspendLayout()
        '
        ' MDV_自定义参数说明
        '
        MDV_自定义参数说明.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MDV_自定义参数说明.BasePath = Nothing
        MDV_自定义参数说明.BlockQuoteForeColor = Color.FromArgb(CByte(160), CByte(160), CByte(160))
        MDV_自定义参数说明.BlockSpacing = 20
        MDV_自定义参数说明.BorderRadius = 10
        MDV_自定义参数说明.CodeBackColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        MDV_自定义参数说明.Dock = DockStyle.Fill
        MDV_自定义参数说明.Font = New Font("Microsoft YaHei UI", 10F)
        MDV_自定义参数说明.ForeColor = Color.Silver
        MDV_自定义参数说明.HeadingColor = Color.Silver
        MDV_自定义参数说明.HeadingSeparatorColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MDV_自定义参数说明.HeadingSeparatorThickness = 2
        MDV_自定义参数说明.Location = New Point(20, 20)
        MDV_自定义参数说明.Name = "MDV_自定义参数说明"
        MDV_自定义参数说明.Padding = New Padding(20)
        MDV_自定义参数说明.Size = New Size(697, 570)
        MDV_自定义参数说明.TabIndex = 0
        MDV_自定义参数说明.Text = ""
        '
        ' ModernPanel1
        '
        ModernPanel1.BackColor1 = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ModernPanel1.BorderSize = 0
        ModernPanel1.Controls.Add(MDV_自定义参数说明)
        ModernPanel1.Dock = DockStyle.Fill
        ModernPanel1.Location = New Point(0, 0)
        ModernPanel1.Name = "ModernPanel1"
        ModernPanel1.Padding = New Padding(20)
        ModernPanel1.ScrollBarMode = LakeUI.ModernPanel.ScrollMode.None
        ModernPanel1.Size = New Size(737, 610)
        ModernPanel1.TabIndex = 1
        '
        ' Form_v6_参数面板_自定义参数说明
        '
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ClientSize = New Size(737, 610)
        Controls.Add(ModernPanel1)
        Font = New Font("Microsoft YaHei UI", 10F)
        ForeColor = Color.Silver
        Name = "Form_v6_参数面板_自定义参数说明"
        Text = "Form_v6_参数面板_自定义参数说明"
        ModernPanel1.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents MDV_自定义参数说明 As LakeUI.MarkDownViewer
    Friend WithEvents ModernPanel1 As LakeUI.ModernPanel
End Class
