<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_v6_设置_界面显示
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
        Panel1 = New Panel()
        ModernComboBox2 = New LakeUI.ModernComboBox()
        HtmlColorLabel2 = New LakeUI.HtmlColorLabel()
        Panel2 = New Panel()
        HtmlColorLabel6 = New LakeUI.HtmlColorLabel()
        ModernComboBox1 = New LakeUI.ModernComboBox()
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
        ModernPanel1.Controls.Add(Panel1)
        ModernPanel1.Controls.Add(HtmlColorLabel2)
        ModernPanel1.Controls.Add(Panel2)
        ModernPanel1.Controls.Add(HtmlColorLabel1)
        ModernPanel1.Dock = DockStyle.Fill
        ModernPanel1.Location = New Point(0, 0)
        ModernPanel1.Name = "ModernPanel1"
        ModernPanel1.Padding = New Padding(20)
        ModernPanel1.ScrollBarMode = LakeUI.ModernPanel.ScrollMode.Vertical
        ModernPanel1.Size = New Size(752, 625)
        ModernPanel1.TabIndex = 0
        ' 
        ' Panel1
        ' 
        Panel1.Controls.Add(ModernComboBox2)
        Panel1.Dock = DockStyle.Top
        Panel1.Location = New Point(20, 142)
        Panel1.Name = "Panel1"
        Panel1.Padding = New Padding(0, 10, 0, 0)
        Panel1.Size = New Size(712, 42)
        Panel1.TabIndex = 13
        ' 
        ' ModernComboBox2
        ' 
        ModernComboBox2.BackColor1 = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        ModernComboBox2.BorderColorFocus = Color.Silver
        ModernComboBox2.BorderRadius = 10
        ModernComboBox2.BorderSize = 0
        ModernComboBox2.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        ModernComboBox2.Dock = DockStyle.Left
        ModernComboBox2.DropDownBorderSize = 2
        ModernComboBox2.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        ModernComboBox2.DropDownPadding = New Padding(10)
        ModernComboBox2.DropDownScrollBarHoverColor = Color.FromArgb(CByte(200), CByte(200), CByte(200))
        ModernComboBox2.Items.Add("DPI（控件默认行为）")
        ModernComboBox2.Items.Add("百分比（计算比例）")
        ModernComboBox2.Location = New Point(0, 10)
        ModernComboBox2.Margin = New Padding(2, 2, 2, 2)
        ModernComboBox2.MaxDropDownItems = 15
        ModernComboBox2.Name = "ModernComboBox2"
        ModernComboBox2.Padding = New Padding(10, 0, 10, 0)
        ModernComboBox2.Size = New Size(200, 32)
        ModernComboBox2.TabIndex = 0
        ModernComboBox2.ToolTipBorderSize = 2
        ModernComboBox2.ToolTipGap = 10
        ModernComboBox2.ToolTipMaxWidth = 350
        ModernComboBox2.ToolTipPadding = New Padding(15)
        ' 
        ' HtmlColorLabel2
        ' 
        HtmlColorLabel2.AutoSize = True
        HtmlColorLabel2.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel2.Dock = DockStyle.Top
        HtmlColorLabel2.Location = New Point(20, 92)
        HtmlColorLabel2.Margin = New Padding(2)
        HtmlColorLabel2.Name = "HtmlColorLabel2"
        HtmlColorLabel2.Padding = New Padding(0, 20, 0, 5)
        HtmlColorLabel2.Size = New Size(712, 50)
        HtmlColorLabel2.TabIndex = 12
        HtmlColorLabel2.Text = "<span style=""font-size:13"">编码队列 - 列宽调整模式</span>"
        ' 
        ' Panel2
        ' 
        Panel2.Controls.Add(HtmlColorLabel6)
        Panel2.Controls.Add(ModernComboBox1)
        Panel2.Dock = DockStyle.Top
        Panel2.Location = New Point(20, 50)
        Panel2.Name = "Panel2"
        Panel2.Padding = New Padding(0, 10, 0, 0)
        Panel2.Size = New Size(712, 42)
        Panel2.TabIndex = 11
        ' 
        ' HtmlColorLabel6
        ' 
        HtmlColorLabel6.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel6.Dock = DockStyle.Fill
        HtmlColorLabel6.Location = New Point(200, 10)
        HtmlColorLabel6.Margin = New Padding(2)
        HtmlColorLabel6.Name = "HtmlColorLabel6"
        HtmlColorLabel6.Padding = New Padding(10, 0, 0, 0)
        HtmlColorLabel6.Size = New Size(512, 32)
        HtmlColorLabel6.TabIndex = 2
        HtmlColorLabel6.Text = "敏捷的棕色狐狸跳过了懒惰的狗。"
        HtmlColorLabel6.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.MiddleLeft
        ' 
        ' ModernComboBox1
        ' 
        ModernComboBox1.BackColor1 = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        ModernComboBox1.BorderColorFocus = Color.Silver
        ModernComboBox1.BorderRadius = 10
        ModernComboBox1.BorderSize = 0
        ModernComboBox1.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        ModernComboBox1.Dock = DockStyle.Left
        ModernComboBox1.DropDownBorderSize = 2
        ModernComboBox1.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        ModernComboBox1.DropDownPadding = New Padding(10)
        ModernComboBox1.DropDownScrollBarHoverColor = Color.FromArgb(CByte(200), CByte(200), CByte(200))
        ModernComboBox1.Location = New Point(0, 10)
        ModernComboBox1.Margin = New Padding(2, 2, 2, 2)
        ModernComboBox1.MaxDropDownItems = 15
        ModernComboBox1.Name = "ModernComboBox1"
        ModernComboBox1.Padding = New Padding(10, 0, 10, 0)
        ModernComboBox1.Size = New Size(200, 32)
        ModernComboBox1.TabIndex = 0
        ModernComboBox1.ToolTipBorderSize = 2
        ModernComboBox1.ToolTipGap = 10
        ModernComboBox1.ToolTipMaxWidth = 350
        ModernComboBox1.ToolTipPadding = New Padding(15)
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
        HtmlColorLabel1.Size = New Size(712, 30)
        HtmlColorLabel1.TabIndex = 10
        HtmlColorLabel1.Text = "<span style=""font-size:13"">全局字体</span>   <span style=""font-size:10pt; color:Gray"">如系统缩放小于 150% 则推荐使用 MacType 来改善文字视觉体验</span>"
        ' 
        ' Form_v6_设置_界面显示
        ' 
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ClientSize = New Size(752, 625)
        Controls.Add(ModernPanel1)
        Font = New Font("Microsoft YaHei UI", 10F)
        ForeColor = Color.Silver
        Name = "Form_v6_设置_界面显示"
        Text = "Form_v6_设置_界面显示"
        ModernPanel1.ResumeLayout(False)
        ModernPanel1.PerformLayout()
        Panel1.ResumeLayout(False)
        Panel2.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents ModernPanel1 As LakeUI.ModernPanel
    Friend WithEvents HtmlColorLabel1 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents ModernComboBox1 As LakeUI.ModernComboBox
    Friend WithEvents HtmlColorLabel2 As LakeUI.HtmlColorLabel
    Friend WithEvents HtmlColorLabel6 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ModernComboBox2 As LakeUI.ModernComboBox
End Class
