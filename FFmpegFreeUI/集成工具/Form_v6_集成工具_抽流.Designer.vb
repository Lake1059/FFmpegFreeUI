<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_v6_集成工具_抽流
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
        ModernPanel2 = New LakeUI.ModernPanel()
        Panel1 = New Panel()
        MCB_输出位置 = New LakeUI.ModernComboBox()
        JustEmptyControl2 = New LakeUI.JustEmptyControl()
        MB_提取所选 = New LakeUI.ModernButton()
        JustEmptyControl1 = New LakeUI.JustEmptyControl()
        MB_打开文件 = New LakeUI.ModernButton()
        HtmlColorLabel1 = New LakeUI.HtmlColorLabel()
        JustEmptyControl3 = New LakeUI.JustEmptyControl()
        MB_清除页面 = New LakeUI.ModernButton()
        ModernPanel1.SuspendLayout()
        Panel1.SuspendLayout()
        SuspendLayout()
        ' 
        ' ModernPanel1
        ' 
        ModernPanel1.BackColor1 = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ModernPanel1.BorderSize = 0
        ModernPanel1.Controls.Add(ModernPanel2)
        ModernPanel1.Controls.Add(Panel1)
        ModernPanel1.Controls.Add(HtmlColorLabel1)
        ModernPanel1.Dock = DockStyle.Fill
        ModernPanel1.Location = New Point(0, 0)
        ModernPanel1.Name = "ModernPanel1"
        ModernPanel1.Padding = New Padding(20)
        ModernPanel1.Size = New Size(871, 595)
        ModernPanel1.TabIndex = 0
        ' 
        ' ModernPanel2
        ' 
        ModernPanel2.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        ModernPanel2.BorderRadius = 10
        ModernPanel2.BorderSize = 0
        ModernPanel2.Dock = DockStyle.Fill
        ModernPanel2.Location = New Point(20, 97)
        ModernPanel2.Name = "ModernPanel2"
        ModernPanel2.Padding = New Padding(10)
        ModernPanel2.Size = New Size(831, 478)
        ModernPanel2.TabIndex = 6
        ' 
        ' Panel1
        ' 
        Panel1.Controls.Add(MCB_输出位置)
        Panel1.Controls.Add(JustEmptyControl3)
        Panel1.Controls.Add(MB_清除页面)
        Panel1.Controls.Add(JustEmptyControl2)
        Panel1.Controls.Add(MB_提取所选)
        Panel1.Controls.Add(JustEmptyControl1)
        Panel1.Controls.Add(MB_打开文件)
        Panel1.Dock = DockStyle.Top
        Panel1.Location = New Point(20, 45)
        Panel1.Name = "Panel1"
        Panel1.Padding = New Padding(0, 10, 0, 10)
        Panel1.Size = New Size(831, 52)
        Panel1.TabIndex = 5
        ' 
        ' MCB_输出位置
        ' 
        MCB_输出位置.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_输出位置.BorderRadius = 10
        MCB_输出位置.BorderSize = 0
        MCB_输出位置.Dock = DockStyle.Fill
        MCB_输出位置.DropDownBackdropBlurPasses = 2
        MCB_输出位置.DropDownBackdropBlurRadius = 30
        MCB_输出位置.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_输出位置.DropDownHoverColor = Color.FromArgb(CByte(20), CByte(220), CByte(220), CByte(220))
        MCB_输出位置.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_输出位置.DropDownPadding = New Padding(10)
        MCB_输出位置.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_输出位置.DropDownSelectedForeColor = Color.White
        MCB_输出位置.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_输出位置.Items.Add("输出到原目录")
        MCB_输出位置.Items.Add("浏览 ...")
        MCB_输出位置.Location = New Point(330, 10)
        MCB_输出位置.Margin = New Padding(2, 2, 2, 2)
        MCB_输出位置.Name = "MCB_输出位置"
        MCB_输出位置.Padding = New Padding(10, 0, 10, 0)
        MCB_输出位置.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_输出位置.Size = New Size(501, 32)
        MCB_输出位置.TabIndex = 7
        MCB_输出位置.ToolTipGap = -1
        MCB_输出位置.ToolTipMaxWidth = 350
        MCB_输出位置.ToolTipPadding = New Padding(15)
        MCB_输出位置.WaterText = "输出位置，将文件或文件夹拖至此来直接获取位置"
        MCB_输出位置.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' JustEmptyControl2
        ' 
        JustEmptyControl2.Dock = DockStyle.Left
        JustEmptyControl2.Location = New Point(210, 10)
        JustEmptyControl2.Name = "JustEmptyControl2"
        JustEmptyControl2.Size = New Size(10, 32)
        JustEmptyControl2.TabIndex = 5
        ' 
        ' MB_提取所选
        ' 
        MB_提取所选.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_提取所选.BorderRadius = 10
        MB_提取所选.BorderSize = 0
        MB_提取所选.Dock = DockStyle.Left
        MB_提取所选.ForeColor = Color.YellowGreen
        MB_提取所选.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_提取所选.Location = New Point(110, 10)
        MB_提取所选.Margin = New Padding(2)
        MB_提取所选.Name = "MB_提取所选"
        MB_提取所选.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_提取所选.Size = New Size(100, 32)
        MB_提取所选.TabIndex = 4
        MB_提取所选.Text = "提取所选"
        ' 
        ' JustEmptyControl1
        ' 
        JustEmptyControl1.Dock = DockStyle.Left
        JustEmptyControl1.Location = New Point(100, 10)
        JustEmptyControl1.Name = "JustEmptyControl1"
        JustEmptyControl1.Size = New Size(10, 32)
        JustEmptyControl1.TabIndex = 3
        ' 
        ' MB_打开文件
        ' 
        MB_打开文件.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_打开文件.BorderRadius = 10
        MB_打开文件.BorderSize = 0
        MB_打开文件.Dock = DockStyle.Left
        MB_打开文件.ForeColor = Color.YellowGreen
        MB_打开文件.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_打开文件.Location = New Point(0, 10)
        MB_打开文件.Margin = New Padding(2)
        MB_打开文件.Name = "MB_打开文件"
        MB_打开文件.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_打开文件.Size = New Size(100, 32)
        MB_打开文件.TabIndex = 0
        MB_打开文件.Text = "打开文件"
        ' 
        ' HtmlColorLabel1
        ' 
        HtmlColorLabel1.AutoSize = True
        HtmlColorLabel1.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel1.Dock = DockStyle.Top
        HtmlColorLabel1.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HtmlColorLabel1.Location = New Point(20, 20)
        HtmlColorLabel1.Margin = New Padding(2)
        HtmlColorLabel1.Name = "HtmlColorLabel1"
        HtmlColorLabel1.Size = New Size(831, 25)
        HtmlColorLabel1.TabIndex = 4
        HtmlColorLabel1.Text = "<span style=""font-size:13; color:Silver"">将指定的流单独输出到文件</span>   多选时自动命名；单选时用左键点提取是打开另存为，右键是自动命名"
        ' 
        ' JustEmptyControl3
        ' 
        JustEmptyControl3.Dock = DockStyle.Left
        JustEmptyControl3.Location = New Point(320, 10)
        JustEmptyControl3.Name = "JustEmptyControl3"
        JustEmptyControl3.Size = New Size(10, 32)
        JustEmptyControl3.TabIndex = 9
        ' 
        ' MB_清除页面
        ' 
        MB_清除页面.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_清除页面.BorderRadius = 10
        MB_清除页面.BorderSize = 0
        MB_清除页面.Dock = DockStyle.Left
        MB_清除页面.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_清除页面.Location = New Point(220, 10)
        MB_清除页面.Margin = New Padding(2)
        MB_清除页面.Name = "MB_清除页面"
        MB_清除页面.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_清除页面.Size = New Size(100, 32)
        MB_清除页面.TabIndex = 8
        MB_清除页面.Text = "清除页面"
        ' 
        ' Form_v6_集成工具_抽流
        ' 
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ClientSize = New Size(871, 595)
        Controls.Add(ModernPanel1)
        Font = New Font("Microsoft YaHei UI", 10F)
        ForeColor = Color.Silver
        Name = "Form_v6_集成工具_抽流"
        Text = "Form_v6_集成工具_抽流"
        ModernPanel1.ResumeLayout(False)
        ModernPanel1.PerformLayout()
        Panel1.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents ModernPanel1 As LakeUI.ModernPanel
    Friend WithEvents HtmlColorLabel1 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents MB_打开文件 As LakeUI.ModernButton
    Friend WithEvents JustEmptyControl1 As LakeUI.JustEmptyControl
    Friend WithEvents JustEmptyControl2 As LakeUI.JustEmptyControl
    Friend WithEvents MB_提取所选 As LakeUI.ModernButton
    Friend WithEvents MCB_输出位置 As LakeUI.ModernComboBox
    Friend WithEvents ModernPanel2 As LakeUI.ModernPanel
    Friend WithEvents JustEmptyControl3 As LakeUI.JustEmptyControl
    Friend WithEvents MB_清除页面 As LakeUI.ModernButton
End Class
