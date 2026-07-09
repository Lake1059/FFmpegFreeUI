<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_v6_社区_我的消息
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
        HtmlColorLabel说明1 = New LakeUI.HtmlColorLabel()
        ModernPanel1.SuspendLayout()
        SuspendLayout()
        ' 
        ' ModernPanel1
        ' 
        ModernPanel1.BackColor1 = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ModernPanel1.BorderSize = 0
        ModernPanel1.Controls.Add(HtmlColorLabel说明1)
        ModernPanel1.Dock = DockStyle.Fill
        ModernPanel1.Location = New Point(0, 0)
        ModernPanel1.Name = "ModernPanel1"
        ModernPanel1.Padding = New Padding(20)
        ModernPanel1.Size = New Size(767, 565)
        ModernPanel1.TabIndex = 0
        ' 
        ' HtmlColorLabel说明1
        ' 
        HtmlColorLabel说明1.AutoSize = True
        HtmlColorLabel说明1.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel说明1.Dock = DockStyle.Top
        HtmlColorLabel说明1.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HtmlColorLabel说明1.Location = New Point(20, 20)
        HtmlColorLabel说明1.Margin = New Padding(2)
        HtmlColorLabel说明1.Name = "HtmlColorLabel说明1"
        HtmlColorLabel说明1.Padding = New Padding(0, 0, 0, 20)
        HtmlColorLabel说明1.Size = New Size(727, 45)
        HtmlColorLabel说明1.TabIndex = 1
        HtmlColorLabel说明1.Text = "<span style=""font-size:13; color:Silver"">系统消息</span>"
        ' 
        ' Form_v6_社区_我的消息
        ' 
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ClientSize = New Size(767, 565)
        Controls.Add(ModernPanel1)
        Font = New Font("Microsoft YaHei UI", 10F)
        ForeColor = Color.Silver
        Name = "Form_v6_社区_我的消息"
        Text = "Form_v6_社区_我的消息"
        ModernPanel1.ResumeLayout(False)
        ModernPanel1.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents ModernPanel1 As LakeUI.ModernPanel
    Friend WithEvents HtmlColorLabel说明1 As LakeUI.HtmlColorLabel
End Class
