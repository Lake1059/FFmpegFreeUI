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
        HCL_章节说明 = New LakeUI.HtmlColorLabel()
        Panel1 = New Panel()
        MCB_章节文件 = New LakeUI.ModernComboBox()
        JustEmptyControl1 = New LakeUI.JustEmptyControl()
        MCB_章节来源 = New LakeUI.ModernComboBox()
        JustEmptyControl2 = New LakeUI.JustEmptyControl()
        MB_教程 = New LakeUI.ModernButton()
        MDV_章节教程 = New LakeUI.MarkDownViewer()
        ModernPanel1 = New LakeUI.ModernPanel()
        Panel1.SuspendLayout()
        ModernPanel1.SuspendLayout()
        SuspendLayout()
        '
        ' HCL_章节说明
        '
        HCL_章节说明.AutoSize = True
        HCL_章节说明.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_章节说明.Dock = DockStyle.Top
        HCL_章节说明.Location = New Point(20, 20)
        HCL_章节说明.Margin = New Padding(2)
        HCL_章节说明.Name = "HCL_章节说明"
        HCL_章节说明.Padding = New Padding(0, 0, 0, 5)
        HCL_章节说明.Size = New Size(743, 30)
        HCL_章节说明.TabIndex = 11
        HCL_章节说明.Text = "<span style=""font-size:13"">章节</span>   <span style=""font-size:10pt; color:Gray"">向输出文件中写入自定义章节，请自行编辑并准备好符合标准的章节文本文档</span>"
        '
        ' Panel1
        '
        Panel1.Controls.Add(MCB_章节文件)
        Panel1.Controls.Add(JustEmptyControl1)
        Panel1.Controls.Add(MCB_章节来源)
        Panel1.Controls.Add(JustEmptyControl2)
        Panel1.Controls.Add(MB_教程)
        Panel1.Dock = DockStyle.Top
        Panel1.Location = New Point(20, 50)
        Panel1.Name = "Panel1"
        Panel1.Padding = New Padding(0, 10, 0, 10)
        Panel1.Size = New Size(743, 52)
        Panel1.TabIndex = 12
        '
        ' MCB_章节文件
        '
        MCB_章节文件.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_章节文件.BorderRadius = 10
        MCB_章节文件.BorderSize = 0
        MCB_章节文件.Dock = DockStyle.Fill
        MCB_章节文件.DropDownBackdropBlurPasses = 2
        MCB_章节文件.DropDownBackdropBlurRadius = 30
        MCB_章节文件.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_章节文件.DropDownHoverAnimationDuration = 0
        MCB_章节文件.DropDownHoverColor = Color.FromArgb(CByte(80), CByte(0), CByte(0), CByte(0))
        MCB_章节文件.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_章节文件.DropDownPadding = New Padding(10)
        MCB_章节文件.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_章节文件.DropDownSelectedForeColor = Color.White
        MCB_章节文件.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_章节文件.Items.Add("浏览 ...")
        MCB_章节文件.Location = New Point(160, 10)
        MCB_章节文件.Margin = New Padding(2, 2, 2, 2)
        MCB_章节文件.MaxDropDownItems = 15
        MCB_章节文件.Name = "MCB_章节文件"
        MCB_章节文件.Padding = New Padding(10, 0, 10, 0)
        MCB_章节文件.Size = New Size(503, 32)
        MCB_章节文件.TabIndex = 11
        MCB_章节文件.ToolTipGap = -1
        MCB_章节文件.ToolTipMaxWidth = 350
        MCB_章节文件.ToolTipPadding = New Padding(15)
        MCB_章节文件.WaterText = "选择章节文件"
        MCB_章节文件.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        '
        ' JustEmptyControl1
        '
        JustEmptyControl1.Dock = DockStyle.Left
        JustEmptyControl1.Location = New Point(150, 10)
        JustEmptyControl1.Name = "JustEmptyControl1"
        JustEmptyControl1.Size = New Size(10, 32)
        JustEmptyControl1.TabIndex = 2
        '
        ' MCB_章节来源
        '
        MCB_章节来源.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_章节来源.BorderRadius = 10
        MCB_章节来源.BorderSize = 0
        MCB_章节来源.Dock = DockStyle.Left
        MCB_章节来源.DropDownBackdropBlurPasses = 2
        MCB_章节来源.DropDownBackdropBlurRadius = 30
        MCB_章节来源.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_章节来源.DropDownHoverAnimationDuration = 0
        MCB_章节来源.DropDownHoverColor = Color.FromArgb(CByte(80), CByte(0), CByte(0), CByte(0))
        MCB_章节来源.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_章节来源.DropDownPadding = New Padding(10)
        MCB_章节来源.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_章节来源.DropDownSelectedForeColor = Color.White
        MCB_章节来源.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_章节来源.Items.Add("")
        MCB_章节来源.Items.Add("来自文本文档")
        MCB_章节来源.Items.Add("来自其他媒体")
        MCB_章节来源.Location = New Point(0, 10)
        MCB_章节来源.Margin = New Padding(2, 2, 2, 2)
        MCB_章节来源.MaxDropDownItems = 15
        MCB_章节来源.Name = "MCB_章节来源"
        MCB_章节来源.Padding = New Padding(10, 0, 10, 0)
        MCB_章节来源.Size = New Size(150, 32)
        MCB_章节来源.TabIndex = 10
        MCB_章节来源.ToolTipGap = -1
        MCB_章节来源.ToolTipMaxWidth = 350
        MCB_章节来源.ToolTipPadding = New Padding(15)
        MCB_章节来源.WaterText = "选择章节来源"
        MCB_章节来源.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
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
        ' MDV_章节教程
        '
        MDV_章节教程.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MDV_章节教程.BasePath = Nothing
        MDV_章节教程.BorderRadius = 10
        MDV_章节教程.Dock = DockStyle.Fill
        MDV_章节教程.Font = New Font("Microsoft YaHei UI", 10F)
        MDV_章节教程.ForeColor = Color.Silver
        MDV_章节教程.HorizontalRuleColor = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MDV_章节教程.HorizontalRuleThickness = 2
        MDV_章节教程.Location = New Point(20, 102)
        MDV_章节教程.Name = "MDV_章节教程"
        MDV_章节教程.Padding = New Padding(20)
        MDV_章节教程.Size = New Size(743, 507)
        MDV_章节教程.TabIndex = 13
        MDV_章节教程.Text = ""
        MDV_章节教程.Visible = False
        '
        ' ModernPanel1
        '
        ModernPanel1.BackColor1 = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ModernPanel1.BorderSize = 0
        ModernPanel1.Controls.Add(MDV_章节教程)
        ModernPanel1.Controls.Add(Panel1)
        ModernPanel1.Controls.Add(HCL_章节说明)
        ModernPanel1.Dock = DockStyle.Fill
        ModernPanel1.Location = New Point(0, 0)
        ModernPanel1.Name = "ModernPanel1"
        ModernPanel1.Padding = New Padding(20)
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

    Friend WithEvents HCL_章节说明 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents JustEmptyControl1 As LakeUI.JustEmptyControl
    Friend WithEvents MCB_章节来源 As LakeUI.ModernComboBox
    Friend WithEvents MCB_章节文件 As LakeUI.ModernComboBox
    Friend WithEvents MDV_章节教程 As LakeUI.MarkDownViewer
    Friend WithEvents JustEmptyControl2 As LakeUI.JustEmptyControl
    Friend WithEvents MB_教程 As LakeUI.ModernButton
    Friend WithEvents ModernPanel1 As LakeUI.ModernPanel
End Class
