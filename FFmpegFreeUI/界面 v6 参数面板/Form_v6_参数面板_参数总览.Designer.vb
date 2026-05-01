<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_v6_参数面板_参数总览
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
        Panel1 = New Panel()
        ModernTextBox1 = New LakeUI.ModernTextBox()
        Panel3 = New Panel()
        ModernButton1 = New LakeUI.ModernButton()
        HtmlColorLabel1 = New LakeUI.HtmlColorLabel()
        Panel2 = New Panel()
        ModernTextBox2 = New LakeUI.ModernTextBox()
        Panel70 = New Panel()
        ModernButton2 = New LakeUI.ModernButton()
        HtmlColorLabel2 = New LakeUI.HtmlColorLabel()
        Panel1.SuspendLayout()
        Panel3.SuspendLayout()
        Panel2.SuspendLayout()
        Panel70.SuspendLayout()
        SuspendLayout()
        ' 
        ' Panel1
        ' 
        Panel1.Controls.Add(ModernTextBox1)
        Panel1.Controls.Add(Panel3)
        Panel1.Controls.Add(HtmlColorLabel1)
        Panel1.Dock = DockStyle.Left
        Panel1.Location = New Point(0, 0)
        Panel1.Name = "Panel1"
        Panel1.Padding = New Padding(20, 20, 10, 20)
        Panel1.Size = New Size(365, 611)
        Panel1.TabIndex = 0
        ' 
        ' ModernTextBox1
        ' 
        ModernTextBox1.BorderRadius = 10
        ModernTextBox1.BorderSize = 0
        ModernTextBox1.Dock = DockStyle.Fill
        ModernTextBox1.Location = New Point(20, 62)
        ModernTextBox1.Margin = New Padding(2, 2, 2, 2)
        ModernTextBox1.MultiLine = True
        ModernTextBox1.Name = "ModernTextBox1"
        ModernTextBox1.Padding = New Padding(15, 10, 15, 10)
        ModernTextBox1.ReadOnly = True
        ModernTextBox1.Size = New Size(335, 479)
        ModernTextBox1.TabIndex = 1
        ModernTextBox1.Text = "ModernTextBox1"
        ' 
        ' Panel3
        ' 
        Panel3.Controls.Add(ModernButton1)
        Panel3.Dock = DockStyle.Bottom
        Panel3.Location = New Point(20, 541)
        Panel3.Name = "Panel3"
        Panel3.Padding = New Padding(0, 15, 0, 0)
        Panel3.Size = New Size(335, 50)
        Panel3.TabIndex = 40
        ' 
        ' ModernButton1
        ' 
        ModernButton1.BackColor1 = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        ModernButton1.BorderRadius = 10
        ModernButton1.BorderSize = 0
        ModernButton1.Dock = DockStyle.Fill
        ModernButton1.HoverBackColor1 = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        ModernButton1.Location = New Point(0, 15)
        ModernButton1.Margin = New Padding(2)
        ModernButton1.Name = "ModernButton1"
        ModernButton1.PressedBackColor1 = SystemColors.WindowFrame
        ModernButton1.Size = New Size(335, 35)
        ModernButton1.TabIndex = 2
        ModernButton1.Text = "复制参数总览"
        ' 
        ' HtmlColorLabel1
        ' 
        HtmlColorLabel1.AutoSize = True
        HtmlColorLabel1.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel1.Dock = DockStyle.Top
        HtmlColorLabel1.Font = New Font("Microsoft YaHei UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, CByte(134))
        HtmlColorLabel1.Location = New Point(20, 20)
        HtmlColorLabel1.Margin = New Padding(2)
        HtmlColorLabel1.Name = "HtmlColorLabel1"
        HtmlColorLabel1.Padding = New Padding(0, 0, 0, 15)
        HtmlColorLabel1.Size = New Size(335, 42)
        HtmlColorLabel1.TabIndex = 0
        HtmlColorLabel1.Text = "参数总览"
        HtmlColorLabel1.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.Center
        ' 
        ' Panel2
        ' 
        Panel2.Controls.Add(ModernTextBox2)
        Panel2.Controls.Add(Panel70)
        Panel2.Controls.Add(HtmlColorLabel2)
        Panel2.Dock = DockStyle.Fill
        Panel2.Location = New Point(365, 0)
        Panel2.Name = "Panel2"
        Panel2.Padding = New Padding(10, 20, 20, 20)
        Panel2.Size = New Size(397, 611)
        Panel2.TabIndex = 1
        ' 
        ' ModernTextBox2
        ' 
        ModernTextBox2.BorderRadius = 10
        ModernTextBox2.BorderSize = 0
        ModernTextBox2.Dock = DockStyle.Fill
        ModernTextBox2.Location = New Point(10, 62)
        ModernTextBox2.Margin = New Padding(2, 2, 2, 2)
        ModernTextBox2.MultiLine = True
        ModernTextBox2.Name = "ModernTextBox2"
        ModernTextBox2.Padding = New Padding(15, 10, 15, 10)
        ModernTextBox2.ReadOnly = True
        ModernTextBox2.Size = New Size(367, 479)
        ModernTextBox2.TabIndex = 3
        ModernTextBox2.Text = "ModernTextBox2"
        ' 
        ' Panel70
        ' 
        Panel70.Controls.Add(ModernButton2)
        Panel70.Dock = DockStyle.Bottom
        Panel70.Location = New Point(10, 541)
        Panel70.Name = "Panel70"
        Panel70.Padding = New Padding(0, 15, 0, 0)
        Panel70.Size = New Size(367, 50)
        Panel70.TabIndex = 39
        ' 
        ' ModernButton2
        ' 
        ModernButton2.BackColor1 = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        ModernButton2.BorderRadius = 10
        ModernButton2.BorderSize = 0
        ModernButton2.Dock = DockStyle.Fill
        ModernButton2.HoverBackColor1 = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        ModernButton2.Location = New Point(0, 15)
        ModernButton2.Margin = New Padding(2)
        ModernButton2.Name = "ModernButton2"
        ModernButton2.PressedBackColor1 = SystemColors.WindowFrame
        ModernButton2.Size = New Size(367, 35)
        ModernButton2.TabIndex = 3
        ModernButton2.Text = "复制命令行模版"
        ' 
        ' HtmlColorLabel2
        ' 
        HtmlColorLabel2.AutoSize = True
        HtmlColorLabel2.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel2.Dock = DockStyle.Top
        HtmlColorLabel2.Font = New Font("Microsoft YaHei UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, CByte(134))
        HtmlColorLabel2.Location = New Point(10, 20)
        HtmlColorLabel2.Margin = New Padding(2)
        HtmlColorLabel2.Name = "HtmlColorLabel2"
        HtmlColorLabel2.Padding = New Padding(0, 0, 0, 15)
        HtmlColorLabel2.Size = New Size(367, 42)
        HtmlColorLabel2.TabIndex = 2
        HtmlColorLabel2.Text = "命令行模板"
        HtmlColorLabel2.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.Center
        ' 
        ' Form_v6_参数面板_参数总览
        ' 
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ClientSize = New Size(762, 611)
        Controls.Add(Panel2)
        Controls.Add(Panel1)
        Font = New Font("Microsoft YaHei UI", 10F)
        ForeColor = Color.Silver
        Name = "Form_v6_参数面板_参数总览"
        Text = "Form_v6_参数面板_参数总览"
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        Panel3.ResumeLayout(False)
        Panel2.ResumeLayout(False)
        Panel2.PerformLayout()
        Panel70.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents HtmlColorLabel1 As LakeUI.HtmlColorLabel
    Friend WithEvents ModernTextBox1 As LakeUI.ModernTextBox
    Friend WithEvents ModernTextBox2 As LakeUI.ModernTextBox
    Friend WithEvents HtmlColorLabel2 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel70 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents ModernButton1 As LakeUI.ModernButton
    Friend WithEvents ModernButton2 As LakeUI.ModernButton
End Class
