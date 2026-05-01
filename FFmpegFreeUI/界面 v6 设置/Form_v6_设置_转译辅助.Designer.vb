<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_v6_设置_转译辅助
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
        Panel2 = New Panel()
        ModernTextBox1 = New LakeUI.ModernTextBox()
        HtmlColorLabel1 = New LakeUI.HtmlColorLabel()
        HtmlColorLabel6 = New LakeUI.HtmlColorLabel()
        Panel1 = New Panel()
        ModernTextBox2 = New LakeUI.ModernTextBox()
        HtmlColorLabel2 = New LakeUI.HtmlColorLabel()
        ModernCheckBox1 = New LakeUI.ModernCheckBox()
        ModernPanel1.SuspendLayout()
        Panel2.SuspendLayout()
        Panel1.SuspendLayout()
        SuspendLayout()
        ' 
        ' ModernPanel1
        ' 
        ModernPanel1.BackColor1 = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ModernPanel1.BorderSize = 0
        ModernPanel1.Controls.Add(ModernCheckBox1)
        ModernPanel1.Controls.Add(Panel1)
        ModernPanel1.Controls.Add(HtmlColorLabel2)
        ModernPanel1.Controls.Add(Panel2)
        ModernPanel1.Controls.Add(HtmlColorLabel6)
        ModernPanel1.Controls.Add(HtmlColorLabel1)
        ModernPanel1.Dock = DockStyle.Fill
        ModernPanel1.Location = New Point(0, 0)
        ModernPanel1.Name = "ModernPanel1"
        ModernPanel1.Padding = New Padding(20)
        ModernPanel1.ScrollBarMode = LakeUI.ModernPanel.ScrollMode.Vertical
        ModernPanel1.Size = New Size(785, 642)
        ModernPanel1.TabIndex = 2
        ' 
        ' Panel2
        ' 
        Panel2.Controls.Add(ModernTextBox1)
        Panel2.Dock = DockStyle.Top
        Panel2.Location = New Point(20, 81)
        Panel2.Name = "Panel2"
        Panel2.Padding = New Padding(0, 10, 0, 0)
        Panel2.Size = New Size(745, 42)
        Panel2.TabIndex = 11
        ' 
        ' ModernTextBox1
        ' 
        ModernTextBox1.BackColor1 = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        ModernTextBox1.BorderColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        ModernTextBox1.BorderRadius = 10
        ModernTextBox1.BorderSize = 2
        ModernTextBox1.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        ModernTextBox1.Dock = DockStyle.Fill
        ModernTextBox1.Location = New Point(0, 10)
        ModernTextBox1.Margin = New Padding(2, 2, 2, 2)
        ModernTextBox1.Name = "ModernTextBox1"
        ModernTextBox1.Padding = New Padding(10, 0, 10, 0)
        ModernTextBox1.ScrollBarHoverColor = Color.FromArgb(CByte(200), CByte(200), CByte(200))
        ModernTextBox1.Size = New Size(745, 32)
        ModernTextBox1.TabIndex = 6
        ' 
        ' HtmlColorLabel1
        ' 
        HtmlColorLabel1.AutoSize = True
        HtmlColorLabel1.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel1.Dock = DockStyle.Top
        HtmlColorLabel1.Location = New Point(20, 20)
        HtmlColorLabel1.Margin = New Padding(2)
        HtmlColorLabel1.Name = "HtmlColorLabel1"
        HtmlColorLabel1.Padding = New Padding(0, 0, 0, 5)
        HtmlColorLabel1.Size = New Size(745, 30)
        HtmlColorLabel1.TabIndex = 10
        HtmlColorLabel1.Text = "<span style=""font-size:13"">转译辅助</span>"
        ' 
        ' HtmlColorLabel6
        ' 
        HtmlColorLabel6.AutoSize = True
        HtmlColorLabel6.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel6.Dock = DockStyle.Top
        HtmlColorLabel6.Location = New Point(20, 50)
        HtmlColorLabel6.Margin = New Padding(2)
        HtmlColorLabel6.Name = "HtmlColorLabel6"
        HtmlColorLabel6.Padding = New Padding(0, 10, 0, 0)
        HtmlColorLabel6.Size = New Size(745, 31)
        HtmlColorLabel6.TabIndex = 12
        HtmlColorLabel6.Text = "替代 Process 的 FileName"
        HtmlColorLabel6.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.MiddleLeft
        ' 
        ' Panel1
        ' 
        Panel1.Controls.Add(ModernTextBox2)
        Panel1.Dock = DockStyle.Top
        Panel1.Location = New Point(20, 159)
        Panel1.Name = "Panel1"
        Panel1.Padding = New Padding(0, 10, 0, 0)
        Panel1.Size = New Size(745, 42)
        Panel1.TabIndex = 13
        ' 
        ' ModernTextBox2
        ' 
        ModernTextBox2.BackColor1 = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        ModernTextBox2.BorderColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        ModernTextBox2.BorderRadius = 10
        ModernTextBox2.BorderSize = 2
        ModernTextBox2.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        ModernTextBox2.Dock = DockStyle.Fill
        ModernTextBox2.Location = New Point(0, 10)
        ModernTextBox2.Margin = New Padding(2, 2, 2, 2)
        ModernTextBox2.Name = "ModernTextBox2"
        ModernTextBox2.Padding = New Padding(10, 0, 10, 0)
        ModernTextBox2.ScrollBarHoverColor = Color.FromArgb(CByte(200), CByte(200), CByte(200))
        ModernTextBox2.Size = New Size(745, 32)
        ModernTextBox2.TabIndex = 6
        ' 
        ' HtmlColorLabel2
        ' 
        HtmlColorLabel2.AutoSize = True
        HtmlColorLabel2.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel2.Dock = DockStyle.Top
        HtmlColorLabel2.Location = New Point(20, 123)
        HtmlColorLabel2.Margin = New Padding(2)
        HtmlColorLabel2.Name = "HtmlColorLabel2"
        HtmlColorLabel2.Padding = New Padding(0, 15, 0, 0)
        HtmlColorLabel2.Size = New Size(745, 36)
        HtmlColorLabel2.TabIndex = 14
        HtmlColorLabel2.Text = "覆盖 Process 的 参数传递，用 <args> 来表示 3FUI 生成的参数部分"
        HtmlColorLabel2.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.MiddleLeft
        ' 
        ' ModernCheckBox1
        ' 
        ModernCheckBox1.AutoSize = True
        ModernCheckBox1.BoxBorderRadius = 0
        ModernCheckBox1.BoxBorderSize = 2
        ModernCheckBox1.BoxSize = 20
        ModernCheckBox1.BoxTextSpacing = 10
        ModernCheckBox1.Dock = DockStyle.Top
        ModernCheckBox1.Location = New Point(20, 201)
        ModernCheckBox1.Name = "ModernCheckBox1"
        ModernCheckBox1.Padding = New Padding(0, 15, 0, 0)
        ModernCheckBox1.Size = New Size(745, 37)
        ModernCheckBox1.TabIndex = 15
        ModernCheckBox1.Text = "转译模式（去除盘符、斜杠改为除号）"
        ' 
        ' Form_v6_设置_转译辅助
        ' 
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ClientSize = New Size(785, 642)
        Controls.Add(ModernPanel1)
        Font = New Font("Microsoft YaHei UI", 10F)
        ForeColor = Color.Silver
        Name = "Form_v6_设置_转译辅助"
        Text = "Form_v6_设置_转译辅助"
        ModernPanel1.ResumeLayout(False)
        ModernPanel1.PerformLayout()
        Panel2.ResumeLayout(False)
        Panel1.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents ModernPanel1 As LakeUI.ModernPanel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ModernTextBox2 As LakeUI.ModernTextBox
    Friend WithEvents HtmlColorLabel2 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents ModernTextBox1 As LakeUI.ModernTextBox
    Friend WithEvents HtmlColorLabel6 As LakeUI.HtmlColorLabel
    Friend WithEvents HtmlColorLabel1 As LakeUI.HtmlColorLabel
    Friend WithEvents ModernCheckBox1 As LakeUI.ModernCheckBox
End Class
