<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_v6_参数面板_章节
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
        HtmlColorLabel1 = New LakeUI.HtmlColorLabel()
        Panel1 = New Panel()
        ModernTextBox2 = New LakeUI.ModernTextBox()
        JustEmptyControl1 = New LakeUI.JustEmptyControl()
        ModernComboBox1 = New LakeUI.ModernComboBox()
        JustEmptyControl2 = New LakeUI.JustEmptyControl()
        MB_教程 = New LakeUI.ModernButton()
        MarkDownViewer1 = New LakeUI.MarkDownViewer()
        ModernPanel1 = New LakeUI.ModernPanel()
        Panel1.SuspendLayout()
        ModernPanel1.SuspendLayout()
        SuspendLayout()
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
        HtmlColorLabel1.Size = New Size(743, 30)
        HtmlColorLabel1.TabIndex = 11
        HtmlColorLabel1.Text = "<span style=""font-size:13"">章节</span>   <span style=""font-size:10pt; color:Gray"">向输出文件中写入自定义章节，请自行编辑并准备好符合标准的章节文本文档</span>"
        ' 
        ' Panel1
        ' 
        Panel1.Controls.Add(ModernTextBox2)
        Panel1.Controls.Add(JustEmptyControl1)
        Panel1.Controls.Add(ModernComboBox1)
        Panel1.Controls.Add(JustEmptyControl2)
        Panel1.Controls.Add(MB_教程)
        Panel1.Dock = DockStyle.Top
        Panel1.Location = New Point(20, 50)
        Panel1.Name = "Panel1"
        Panel1.Padding = New Padding(0, 10, 0, 10)
        Panel1.Size = New Size(743, 52)
        Panel1.TabIndex = 12
        ' 
        ' ModernTextBox2
        ' 
        ModernTextBox2.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernTextBox2.BorderColor = Color.Transparent
        ModernTextBox2.BorderColorFocus = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        ModernTextBox2.BorderRadius = 10
        ModernTextBox2.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        ModernTextBox2.Dock = DockStyle.Fill
        ModernTextBox2.Location = New Point(160, 10)
        ModernTextBox2.Margin = New Padding(2)
        ModernTextBox2.Name = "ModernTextBox2"
        ModernTextBox2.Padding = New Padding(10, 0, 10, 0)
        ModernTextBox2.Size = New Size(503, 32)
        ModernTextBox2.TabIndex = 11
        ModernTextBox2.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' JustEmptyControl1
        ' 
        JustEmptyControl1.Dock = DockStyle.Left
        JustEmptyControl1.Location = New Point(150, 10)
        JustEmptyControl1.Name = "JustEmptyControl1"
        JustEmptyControl1.Size = New Size(10, 32)
        JustEmptyControl1.TabIndex = 2
        ' 
        ' ModernComboBox1
        ' 
        ModernComboBox1.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernComboBox1.BorderRadius = 10
        ModernComboBox1.BorderSize = 0
        ModernComboBox1.Dock = DockStyle.Left
        ModernComboBox1.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        ModernComboBox1.DropDownHoverAnimationDuration = 0
        ModernComboBox1.DropDownHoverColor = Color.FromArgb(CByte(80), CByte(0), CByte(0), CByte(0))
        ModernComboBox1.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        ModernComboBox1.DropDownPadding = New Padding(10)
        ModernComboBox1.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernComboBox1.DropDownSelectedForeColor = Color.White
        ModernComboBox1.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        ModernComboBox1.Margin = New Padding(2, 2, 2, 2)
        ModernComboBox1.Padding = New Padding(10, 0, 10, 0)
        ModernComboBox1.ToolTipGap = -1
        ModernComboBox1.ToolTipMaxWidth = 350
        ModernComboBox1.ToolTipPadding = New Padding(15)
        ModernComboBox1.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ModernComboBox1.Items.Add("")
        ModernComboBox1.Items.Add("来自文本文档")
        ModernComboBox1.Items.Add("来自其他媒体")
        ModernComboBox1.Location = New Point(0, 10)
        ModernComboBox1.MaxDropDownItems = 15
        ModernComboBox1.Name = "ModernComboBox1"
        ModernComboBox1.Size = New Size(150, 32)
        ModernComboBox1.TabIndex = 10
        ModernComboBox1.WaterText = "选择章节来源"
        ' 
        ' JustEmptyControl2
        ' 
        JustEmptyControl2.Dock = DockStyle.Right
        JustEmptyControl2.Location = New Point(663, 10)
        JustEmptyControl2.Name = "JustEmptyControl2"
        JustEmptyControl2.Size = New Size(10, 32)
        JustEmptyControl2.TabIndex = 17
        ' 
        ' MB_教程
        ' 
        MB_教程.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_教程.BorderRadius = 10
        MB_教程.BorderSize = 0
        MB_教程.Dock = DockStyle.Right
        MB_教程.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_教程.Location = New Point(673, 10)
        MB_教程.Margin = New Padding(2)
        MB_教程.Name = "MB_教程"
        MB_教程.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_教程.Size = New Size(70, 32)
        MB_教程.TabIndex = 18
        MB_教程.Text = "教程"
        ' 
        ' MarkDownViewer1
        ' 
        MarkDownViewer1.BackColor1 = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        MarkDownViewer1.BasePath = Nothing
        MarkDownViewer1.BlockQuoteForeColor = Color.FromArgb(CByte(160), CByte(160), CByte(160))
        MarkDownViewer1.BlockSpacing = 20
        MarkDownViewer1.BorderRadius = 10
        MarkDownViewer1.CodeBackColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        MarkDownViewer1.CodeBlockPadding = New Padding(7, 5, 7, 5)
        MarkDownViewer1.Dock = DockStyle.Fill
        MarkDownViewer1.Font = New Font("Microsoft YaHei UI", 10F)
        MarkDownViewer1.ForeColor = Color.Silver
        MarkDownViewer1.HeadingColor = Color.Silver
        MarkDownViewer1.Location = New Point(20, 102)
        MarkDownViewer1.Name = "MarkDownViewer1"
        MarkDownViewer1.Padding = New Padding(20)
        MarkDownViewer1.Size = New Size(743, 507)
        MarkDownViewer1.TabIndex = 13
        MarkDownViewer1.Text = "MarkDownViewer1"
        MarkDownViewer1.Visible = False
        ' 
        ' ModernPanel1
        ' 
        ModernPanel1.BackColor1 = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ModernPanel1.BorderSize = 0
        ModernPanel1.Controls.Add(MarkDownViewer1)
        ModernPanel1.Controls.Add(Panel1)
        ModernPanel1.Controls.Add(HtmlColorLabel1)
        ModernPanel1.Dock = DockStyle.Fill
        ModernPanel1.Location = New Point(0, 0)
        ModernPanel1.Name = "ModernPanel1"
        ModernPanel1.Padding = New Padding(20)
        ModernPanel1.ScrollBarMode = LakeUI.ModernPanel.ScrollMode.None
        ModernPanel1.Size = New Size(783, 629)
        ModernPanel1.TabIndex = 14
        ' 
        ' Form_v6_参数面板_章节
        ' 
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ClientSize = New Size(783, 629)
        Controls.Add(ModernPanel1)
        Font = New Font("Microsoft YaHei UI", 10F)
        ForeColor = Color.Silver
        Name = "Form_v6_参数面板_章节"
        Text = "Form_v6_参数面板_章节"
        Panel1.ResumeLayout(False)
        ModernPanel1.ResumeLayout(False)
        ModernPanel1.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents HtmlColorLabel1 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents JustEmptyControl1 As LakeUI.JustEmptyControl
    Friend WithEvents ModernComboBox1 As LakeUI.ModernComboBox
    Friend WithEvents ModernTextBox2 As LakeUI.ModernTextBox
    Friend WithEvents MarkDownViewer1 As LakeUI.MarkDownViewer
    Friend WithEvents JustEmptyControl2 As LakeUI.JustEmptyControl
    Friend WithEvents MB_教程 As LakeUI.ModernButton
    Friend WithEvents ModernPanel1 As LakeUI.ModernPanel
End Class
