<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_v6_参数面板_扫描方式
    Inherits System.Windows.Forms.Form

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

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        ModernPanel1 = New LakeUI.ModernPanel()
        Panel1 = New Panel()
        MCB_扫描方式 = New LakeUI.ModernComboBox()
        MCB_扫描方式总开关 = New LakeUI.ModernCheckBox()
        ModernPanel1.SuspendLayout()
        Panel1.SuspendLayout()
        SuspendLayout()
        ' 
        ' ModernPanel1
        ' 
        ModernPanel1.BackColor1 = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ModernPanel1.BorderSize = 0
        ModernPanel1.Controls.Add(Panel1)
        ModernPanel1.Controls.Add(MCB_扫描方式总开关)
        ModernPanel1.Dock = DockStyle.Fill
        ModernPanel1.Location = New Point(0, 0)
        ModernPanel1.Name = "ModernPanel1"
        ModernPanel1.Padding = New Padding(20)
        ModernPanel1.Size = New Size(584, 116)
        ModernPanel1.TabIndex = 0
        ' 
        ' Panel1
        ' 
        Panel1.Controls.Add(MCB_扫描方式)
        Panel1.Dock = DockStyle.Top
        Panel1.Location = New Point(20, 44)
        Panel1.Name = "Panel1"
        Panel1.Padding = New Padding(0, 20, 0, 0)
        Panel1.Size = New Size(544, 52)
        Panel1.TabIndex = 35
        ' 
        ' MCB_扫描方式
        ' 
        MCB_扫描方式.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_扫描方式.BorderRadius = 10
        MCB_扫描方式.BorderSize = 0
        MCB_扫描方式.Dock = DockStyle.Fill
        MCB_扫描方式.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_扫描方式.DropDownHoverAnimationDuration = 0
        MCB_扫描方式.DropDownHoverColor = Color.FromArgb(CByte(80), CByte(0), CByte(0), CByte(0))
        MCB_扫描方式.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_扫描方式.DropDownPadding = New Padding(10)
        MCB_扫描方式.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_扫描方式.DropDownSelectedForeColor = Color.White
        MCB_扫描方式.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_扫描方式.Items.Add("")
        MCB_扫描方式.Items.Add("隔行转逐行 - yadif 单帧输入+自动场序+空间检查")
        MCB_扫描方式.Items.Add("隔行转逐行 - yadif 单帧输入+顶场优先+空间检查")
        MCB_扫描方式.Items.Add("隔行转逐行 - yadif 单帧输入+底场优先+空间检查")
        MCB_扫描方式.Items.Add("逐行转隔行 - tinterlace 顶场优先")
        MCB_扫描方式.Items.Add("逐行转隔行 - tinterlace 底场优先")
        MCB_扫描方式.Items.Add("NTSC 标准 IVTC 胶片 3:2 pulldown 转逐行")
        MCB_扫描方式.Items.Add("NTSC 纯隔行 非胶片 转逐行")
        MCB_扫描方式.Items.Add("NTSC 自动检测 pulldown 模式至 25fps")
        MCB_扫描方式.Items.Add("PAL 标准反交错")
        MCB_扫描方式.Items.Add("PAL 标准反交错 双倍帧率")
        MCB_扫描方式.Items.Add("PAL 高质量反交错")
        MCB_扫描方式.Items.Add("PAL 高质量反交错 双倍帧率")
        MCB_扫描方式.Items.Add("CUDA 隔行转逐行 - yadif_cuda 自动场序")
        MCB_扫描方式.Items.Add("CUDA 高质量反交错 - bwdif_cuda 自动场序")
        MCB_扫描方式.Location = New Point(0, 20)
        MCB_扫描方式.Margin = New Padding(2, 2, 2, 2)
        MCB_扫描方式.MaxDropDownItems = 15
        MCB_扫描方式.Name = "MCB_扫描方式"
        MCB_扫描方式.Padding = New Padding(10, 0, 10, 0)
        MCB_扫描方式.Size = New Size(544, 32)
        MCB_扫描方式.TabIndex = 15
        MCB_扫描方式.ToolTipGap = -1
        MCB_扫描方式.ToolTipMaxWidth = 420
        MCB_扫描方式.ToolTipPadding = New Padding(15)
        MCB_扫描方式.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' MCB_扫描方式总开关
        ' 
        MCB_扫描方式总开关.AutoSize = True
        MCB_扫描方式总开关.BoxBorderRadius = 5
        MCB_扫描方式总开关.BoxBorderSize = 0
        MCB_扫描方式总开关.BoxCheckedBackColor = Color.OliveDrab
        MCB_扫描方式总开关.BoxInnerPadding = 6
        MCB_扫描方式总开关.BoxSize = 24
        MCB_扫描方式总开关.BoxTextSpacing = 10
        MCB_扫描方式总开关.BoxUncheckedBackColor = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_扫描方式总开关.CheckMarkWidth = 3F
        MCB_扫描方式总开关.Dock = DockStyle.Top
        MCB_扫描方式总开关.Location = New Point(20, 20)
        MCB_扫描方式总开关.Name = "MCB_扫描方式总开关"
        MCB_扫描方式总开关.Size = New Size(544, 24)
        MCB_扫描方式总开关.TabIndex = 25
        MCB_扫描方式总开关.Text = "扫描方式总开关 / 勾选才会启用"
        ' 
        ' Form_v6_参数面板_扫描方式
        ' 
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ClientSize = New Size(584, 116)
        Controls.Add(ModernPanel1)
        Font = New Font("Microsoft YaHei UI", 10F)
        ForeColor = Color.Silver
        MaximizeBox = False
        MinimizeBox = False
        MinimumSize = New Size(600, 155)
        Name = "Form_v6_参数面板_扫描方式"
        Text = "扫描方式"
        ModernPanel1.ResumeLayout(False)
        ModernPanel1.PerformLayout()
        Panel1.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents ModernPanel1 As LakeUI.ModernPanel
    Friend WithEvents MCB_扫描方式总开关 As LakeUI.ModernCheckBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents MCB_扫描方式 As LakeUI.ModernComboBox
End Class
