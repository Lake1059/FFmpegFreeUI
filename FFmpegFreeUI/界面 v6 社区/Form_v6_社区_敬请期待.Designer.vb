<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_v6_社区_敬请期待
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
        HtmlColorLabel1 = New LakeUI.HtmlColorLabel()
        ModernPanel1.SuspendLayout()
        SuspendLayout()
        ' 
        ' ModernPanel1
        ' 
        ModernPanel1.BackColor1 = Color.Transparent
        ModernPanel1.BorderSize = 0
        ModernPanel1.Controls.Add(HtmlColorLabel1)
        ModernPanel1.Dock = DockStyle.Fill
        ModernPanel1.Location = New Point(0, 0)
        ModernPanel1.Name = "ModernPanel1"
        ModernPanel1.Size = New Size(820, 522)
        ModernPanel1.TabIndex = 0
        ' 
        ' HtmlColorLabel1
        ' 
        HtmlColorLabel1.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel1.BackColor = Color.Transparent
        HtmlColorLabel1.Dock = DockStyle.Fill
        HtmlColorLabel1.Location = New Point(0, 0)
        HtmlColorLabel1.Margin = New Padding(2)
        HtmlColorLabel1.Name = "HtmlColorLabel1"
        HtmlColorLabel1.Size = New Size(820, 522)
        HtmlColorLabel1.TabIndex = 0
        HtmlColorLabel1.Text = "也许有一天，这里会是一个繁华的社区"
        HtmlColorLabel1.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.Center
        ' 
        ' Form_v6_社区_敬请期待
        ' 
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ClientSize = New Size(820, 522)
        Controls.Add(ModernPanel1)
        Font = New Font("Microsoft YaHei UI", 10F)
        ForeColor = Color.Silver
        Name = "Form_v6_社区_敬请期待"
        Text = "Form_v6_社区_敬请期待"
        ModernPanel1.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents ModernPanel1 As LakeUI.ModernPanel
    Friend WithEvents HtmlColorLabel1 As LakeUI.HtmlColorLabel
End Class
