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
        MCB_转译模式 = New LakeUI.ModernCheckBox()
        Panel1 = New Panel()
        MTB_覆盖参数传递 = New LakeUI.ModernTextBox()
        HtmlColorLabel2 = New LakeUI.HtmlColorLabel()
        Panel2 = New Panel()
        MCB_替代进程的文件名 = New LakeUI.ModernTextBox()
        HtmlColorLabel6 = New LakeUI.HtmlColorLabel()
        HtmlColorLabel1 = New LakeUI.HtmlColorLabel()
        ModernPanel1.SuspendLayout()
        Panel1.SuspendLayout()
        Panel2.SuspendLayout()
        SuspendLayout()
        '
        ' ModernPanel1
        '
        ModernPanel1.BackColor1 = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ModernPanel1.BorderSize = 0
        ModernPanel1.Controls.Add(MCB_转译模式)
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
        ' MCB_转译模式
        '
        MCB_转译模式.AutoSize = True
        MCB_转译模式.BoxBorderRadius = 5
        MCB_转译模式.BoxBorderSize = 0
        MCB_转译模式.BoxCheckedBackColor = Color.OliveDrab
        MCB_转译模式.BoxInnerPadding = 6
        MCB_转译模式.BoxSize = 22
        MCB_转译模式.BoxTextSpacing = 10
        MCB_转译模式.BoxUncheckedBackColor = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MCB_转译模式.CheckMarkColor = Color.WhiteSmoke
        MCB_转译模式.Dock = DockStyle.Top
        MCB_转译模式.Location = New Point(20, 201)
        MCB_转译模式.MainSubTextSpacing = 3
        MCB_转译模式.Name = "MCB_转译模式"
        MCB_转译模式.Padding = New Padding(0, 10, 0, 10)
        MCB_转译模式.Size = New Size(745, 60)
        MCB_转译模式.SubText = "去除盘符、斜杠改为除号"
        MCB_转译模式.TabIndex = 35
        MCB_转译模式.Text = "转译模式"
        '
        ' Panel1
        '
        Panel1.Controls.Add(MTB_覆盖参数传递)
        Panel1.Dock = DockStyle.Top
        Panel1.Location = New Point(20, 159)
        Panel1.Name = "Panel1"
        Panel1.Padding = New Padding(0, 10, 0, 0)
        Panel1.Size = New Size(745, 42)
        Panel1.TabIndex = 13
        '
        ' MTB_覆盖参数传递
        '
        MTB_覆盖参数传递.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_覆盖参数传递.BorderColor = Color.Empty
        MTB_覆盖参数传递.BorderColorFocus = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MTB_覆盖参数传递.BorderRadius = 10
        MTB_覆盖参数传递.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        MTB_覆盖参数传递.Dock = DockStyle.Fill
        MTB_覆盖参数传递.Location = New Point(0, 10)
        MTB_覆盖参数传递.Margin = New Padding(2)
        MTB_覆盖参数传递.Name = "MTB_覆盖参数传递"
        MTB_覆盖参数传递.Padding = New Padding(10, 0, 10, 0)
        MTB_覆盖参数传递.Size = New Size(745, 32)
        MTB_覆盖参数传递.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_覆盖参数传递.TabIndex = 6
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
        ' Panel2
        '
        Panel2.Controls.Add(MCB_替代进程的文件名)
        Panel2.Dock = DockStyle.Top
        Panel2.Location = New Point(20, 81)
        Panel2.Name = "Panel2"
        Panel2.Padding = New Padding(0, 10, 0, 0)
        Panel2.Size = New Size(745, 42)
        Panel2.TabIndex = 11
        '
        ' MCB_替代进程的文件名
        '
        MCB_替代进程的文件名.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_替代进程的文件名.BorderColor = Color.Empty
        MCB_替代进程的文件名.BorderColorFocus = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MCB_替代进程的文件名.BorderRadius = 10
        MCB_替代进程的文件名.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        MCB_替代进程的文件名.Dock = DockStyle.Fill
        MCB_替代进程的文件名.Location = New Point(0, 10)
        MCB_替代进程的文件名.Margin = New Padding(2)
        MCB_替代进程的文件名.Name = "MCB_替代进程的文件名"
        MCB_替代进程的文件名.Padding = New Padding(10, 0, 10, 0)
        MCB_替代进程的文件名.Size = New Size(745, 32)
        MCB_替代进程的文件名.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_替代进程的文件名.TabIndex = 6
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
        Panel1.ResumeLayout(False)
        Panel2.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents ModernPanel1 As LakeUI.ModernPanel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents MTB_覆盖参数传递 As LakeUI.ModernTextBox
    Friend WithEvents HtmlColorLabel2 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents MCB_替代进程的文件名 As LakeUI.ModernTextBox
    Friend WithEvents HtmlColorLabel6 As LakeUI.HtmlColorLabel
    Friend WithEvents HtmlColorLabel1 As LakeUI.HtmlColorLabel
    Friend WithEvents MCB_转译模式 As LakeUI.ModernCheckBox
End Class
