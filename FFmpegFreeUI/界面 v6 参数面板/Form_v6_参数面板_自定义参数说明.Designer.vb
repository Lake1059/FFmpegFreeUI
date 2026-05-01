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
        MarkDownViewer1 = New LakeUI.MarkDownViewer()
        SuspendLayout()
        ' 
        ' MarkDownViewer1
        ' 
        MarkDownViewer1.BackColor1 = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        MarkDownViewer1.BasePath = Nothing
        MarkDownViewer1.BlockQuoteForeColor = Color.FromArgb(CByte(160), CByte(160), CByte(160))
        MarkDownViewer1.BlockSpacing = 20
        MarkDownViewer1.BorderRadius = 10
        MarkDownViewer1.CodeBackColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        MarkDownViewer1.Dock = DockStyle.Fill
        MarkDownViewer1.Font = New Font("Microsoft YaHei UI", 10F)
        MarkDownViewer1.ForeColor = Color.Silver
        MarkDownViewer1.HeadingColor = Color.Silver
        MarkDownViewer1.Location = New Point(20, 20)
        MarkDownViewer1.Name = "MarkDownViewer1"
        MarkDownViewer1.Padding = New Padding(20)
        MarkDownViewer1.ScrollBarHoverColor = Color.FromArgb(CByte(200), CByte(200), CByte(200))
        MarkDownViewer1.Size = New Size(697, 570)
        MarkDownViewer1.TabIndex = 0
        MarkDownViewer1.Text = "MarkDownViewer1"
        ' 
        ' Form_v6_参数面板_自定义参数说明
        ' 
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ClientSize = New Size(737, 610)
        Controls.Add(MarkDownViewer1)
        Font = New Font("Microsoft YaHei UI", 10F)
        ForeColor = Color.Silver
        Name = "Form_v6_参数面板_自定义参数说明"
        Padding = New Padding(20)
        Text = "Form_v6_参数面板_自定义参数说明"
        ResumeLayout(False)
    End Sub

    Friend WithEvents MarkDownViewer1 As LakeUI.MarkDownViewer
End Class
