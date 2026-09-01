<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_v6_参数面板_插帧_NV_FRUC
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
        Dim ToolTipEntry1 As LakeUI.ModernComboBox.ToolTipEntry = New LakeUI.ModernComboBox.ToolTipEntry()
        Dim ToolTipEntry2 As LakeUI.ModernComboBox.ToolTipEntry = New LakeUI.ModernComboBox.ToolTipEntry()
        Dim ToolTipEntry3 As LakeUI.ModernComboBox.ToolTipEntry = New LakeUI.ModernComboBox.ToolTipEntry()
        Dim ToolTipEntry4 As LakeUI.ModernComboBox.ToolTipEntry = New LakeUI.ModernComboBox.ToolTipEntry()
        ModernPanel1 = New LakeUI.ModernPanel()
        HCL_CK流程提示 = New LakeUI.HtmlColorLabel()
        ModernPanel2 = New LakeUI.ModernPanel()
        MCB_网格大小 = New LakeUI.ModernComboBox()
        HCL_网格大小 = New LakeUI.HtmlColorLabel()
        Panel1 = New LakeUI.ModernPanel()
        MCB_质量和速度 = New LakeUI.ModernComboBox()
        HCL_质量和速度 = New LakeUI.HtmlColorLabel()
        Panel2 = New LakeUI.ModernPanel()
        MCB_目标帧率 = New LakeUI.ModernComboBox()
        HCL_目标帧率 = New LakeUI.HtmlColorLabel()
        MCK_插帧总开关 = New LakeUI.ModernCheckBox()
        ModernPanel1.SuspendLayout()
        ModernPanel2.SuspendLayout()
        Panel1.SuspendLayout()
        Panel2.SuspendLayout()
        SuspendLayout()
        ' 
        ' ModernPanel1
        ' 
        ModernPanel1.BackColor = Color.Transparent
        ModernPanel1.BackColor1 = Color.Transparent
        ModernPanel1.BorderSize = 0
        ModernPanel1.Controls.Add(HCL_CK流程提示)
        ModernPanel1.Controls.Add(ModernPanel2)
        ModernPanel1.Controls.Add(HCL_网格大小)
        ModernPanel1.Controls.Add(Panel1)
        ModernPanel1.Controls.Add(HCL_质量和速度)
        ModernPanel1.Controls.Add(Panel2)
        ModernPanel1.Controls.Add(HCL_目标帧率)
        ModernPanel1.Controls.Add(MCK_插帧总开关)
        ModernPanel1.Dock = DockStyle.Fill
        ModernPanel1.Location = New Point(0, 0)
        ModernPanel1.Name = "ModernPanel1"
        ModernPanel1.Padding = New Padding(20)
        ModernPanel1.ScrollBarMode = LakeUI.ModernPanel.ScrollMode.Vertical
        ModernPanel1.Size = New Size(584, 411)
        ModernPanel1.TabIndex = 1
        ' 
        ' HCL_CK流程提示
        ' 
        HCL_CK流程提示.AutoSize = True
        HCL_CK流程提示.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_CK流程提示.Dock = DockStyle.Bottom
        HCL_CK流程提示.ForeColor = Color.YellowGreen
        HCL_CK流程提示.InfoIconSizeRatio = 0.9F
        HCL_CK流程提示.InfoIconStrokeWidth = 1.5F
        HCL_CK流程提示.InfoIconTextGap = 5
        HCL_CK流程提示.Location = New Point(20, 374)
        HCL_CK流程提示.Margin = New Padding(2)
        HCL_CK流程提示.Name = "HCL_CK流程提示"
        HCL_CK流程提示.Size = New Size(544, 17)
        HCL_CK流程提示.TabIndex = 31
        HCL_CK流程提示.Text = "要使用此滤镜，必须设定 Vulkan 处理流程"
        HCL_CK流程提示.ToolTipMaxWidth = 500
        HCL_CK流程提示.ToolTipText = "1"
        ' 
        ' ModernPanel2
        ' 
        ModernPanel2.BackColor = Color.Transparent
        ModernPanel2.BackColor1 = Color.Transparent
        ModernPanel2.BorderSize = 0
        ModernPanel2.Controls.Add(MCB_网格大小)
        ModernPanel2.Dock = DockStyle.Top
        ModernPanel2.Location = New Point(20, 257)
        ModernPanel2.Name = "ModernPanel2"
        ModernPanel2.Padding = New Padding(0, 10, 0, 0)
        ModernPanel2.Size = New Size(544, 42)
        ModernPanel2.TabIndex = 30
        ' 
        ' MCB_网格大小
        ' 
        MCB_网格大小.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_网格大小.BorderRadius = 10
        MCB_网格大小.BorderSize = 0
        MCB_网格大小.Dock = DockStyle.Left
        MCB_网格大小.DropDownBackdropBlurPasses = 2
        MCB_网格大小.DropDownBackdropBlurRadius = 30
        MCB_网格大小.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_网格大小.DropDownHoverColor = Color.FromArgb(CByte(20), CByte(220), CByte(220), CByte(220))
        MCB_网格大小.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_网格大小.DropDownPadding = New Padding(10)
        MCB_网格大小.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_网格大小.DropDownSelectedForeColor = Color.White
        MCB_网格大小.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_网格大小.Items.Add("")
        MCB_网格大小.Items.Add("auto")
        MCB_网格大小.Items.Add("1")
        MCB_网格大小.Items.Add("2")
        MCB_网格大小.Items.Add("4")
        MCB_网格大小.Items.Add("8")
        ToolTipEntry1.ItemText = "1"
        ToolTipEntry1.ToolTipText = "1x1 网格，细节最好"
        ToolTipEntry2.ItemText = "2"
        ToolTipEntry2.ToolTipText = "2x2 网格"
        ToolTipEntry3.ItemText = "4"
        ToolTipEntry3.ToolTipText = "4x4 网格"
        ToolTipEntry4.ItemText = "8"
        ToolTipEntry4.ToolTipText = "8x8 网格，速度最快但细节最少"
        MCB_网格大小.ItemToolTips.AddRange(New LakeUI.ModernComboBox.ToolTipEntry() {ToolTipEntry1, ToolTipEntry2, ToolTipEntry3, ToolTipEntry4})
        MCB_网格大小.Location = New Point(0, 10)
        MCB_网格大小.Margin = New Padding(2, 2, 2, 2)
        MCB_网格大小.Name = "MCB_网格大小"
        MCB_网格大小.Padding = New Padding(10, 0, 10, 0)
        MCB_网格大小.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_网格大小.Size = New Size(150, 32)
        MCB_网格大小.TabIndex = 15
        MCB_网格大小.ToolTipGap = -1
        MCB_网格大小.ToolTipMaxWidth = 350
        MCB_网格大小.ToolTipPadding = New Padding(15)
        MCB_网格大小.WaterText = "grid="
        MCB_网格大小.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' HCL_网格大小
        ' 
        HCL_网格大小.AutoSize = True
        HCL_网格大小.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_网格大小.Dock = DockStyle.Top
        HCL_网格大小.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_网格大小.Location = New Point(20, 214)
        HCL_网格大小.Margin = New Padding(2)
        HCL_网格大小.Name = "HCL_网格大小"
        HCL_网格大小.Padding = New Padding(0, 20, 0, 0)
        HCL_网格大小.Size = New Size(544, 43)
        HCL_网格大小.TabIndex = 29
        HCL_网格大小.Text = "<span style=""font-size:13; color:Silver"">网格大小</span>   单位：像素矩形边长。网格越小质量越高，同时越吃显存"
        ' 
        ' Panel1
        ' 
        Panel1.BackColor = Color.Transparent
        Panel1.BackColor1 = Color.Transparent
        Panel1.BorderSize = 0
        Panel1.Controls.Add(MCB_质量和速度)
        Panel1.Dock = DockStyle.Top
        Panel1.Location = New Point(20, 172)
        Panel1.Name = "Panel1"
        Panel1.Padding = New Padding(0, 10, 0, 0)
        Panel1.Size = New Size(544, 42)
        Panel1.TabIndex = 28
        ' 
        ' MCB_质量和速度
        ' 
        MCB_质量和速度.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_质量和速度.BorderRadius = 10
        MCB_质量和速度.BorderSize = 0
        MCB_质量和速度.Dock = DockStyle.Left
        MCB_质量和速度.DropDownBackdropBlurPasses = 2
        MCB_质量和速度.DropDownBackdropBlurRadius = 30
        MCB_质量和速度.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_质量和速度.DropDownHoverColor = Color.FromArgb(CByte(20), CByte(220), CByte(220), CByte(220))
        MCB_质量和速度.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_质量和速度.DropDownPadding = New Padding(10)
        MCB_质量和速度.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_质量和速度.DropDownSelectedForeColor = Color.White
        MCB_质量和速度.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_质量和速度.Items.Add("")
        MCB_质量和速度.Items.Add("slow")
        MCB_质量和速度.Items.Add("medium")
        MCB_质量和速度.Items.Add("fast")
        MCB_质量和速度.Location = New Point(0, 10)
        MCB_质量和速度.Margin = New Padding(2, 2, 2, 2)
        MCB_质量和速度.Name = "MCB_质量和速度"
        MCB_质量和速度.Padding = New Padding(10, 0, 10, 0)
        MCB_质量和速度.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_质量和速度.Size = New Size(150, 32)
        MCB_质量和速度.TabIndex = 15
        MCB_质量和速度.ToolTipGap = -1
        MCB_质量和速度.ToolTipMaxWidth = 350
        MCB_质量和速度.ToolTipPadding = New Padding(15)
        MCB_质量和速度.WaterText = "perf="
        MCB_质量和速度.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' HCL_质量和速度
        ' 
        HCL_质量和速度.AutoSize = True
        HCL_质量和速度.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_质量和速度.Dock = DockStyle.Top
        HCL_质量和速度.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_质量和速度.Location = New Point(20, 129)
        HCL_质量和速度.Margin = New Padding(2)
        HCL_质量和速度.Name = "HCL_质量和速度"
        HCL_质量和速度.Padding = New Padding(0, 20, 0, 0)
        HCL_质量和速度.Size = New Size(544, 43)
        HCL_质量和速度.TabIndex = 26
        HCL_质量和速度.Text = "<span style=""font-size:13; color:Silver"">质量和速度</span>   越慢质量越好，越快质量越差"
        ' 
        ' Panel2
        ' 
        Panel2.BackColor = Color.Transparent
        Panel2.BackColor1 = Color.Transparent
        Panel2.BorderSize = 0
        Panel2.Controls.Add(MCB_目标帧率)
        Panel2.Dock = DockStyle.Top
        Panel2.Location = New Point(20, 87)
        Panel2.Name = "Panel2"
        Panel2.Padding = New Padding(0, 10, 0, 0)
        Panel2.Size = New Size(544, 42)
        Panel2.TabIndex = 25
        ' 
        ' MCB_目标帧率
        ' 
        MCB_目标帧率.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_目标帧率.BorderRadius = 10
        MCB_目标帧率.BorderSize = 0
        MCB_目标帧率.Dock = DockStyle.Left
        MCB_目标帧率.DropDownBackdropBlurPasses = 2
        MCB_目标帧率.DropDownBackdropBlurRadius = 30
        MCB_目标帧率.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_目标帧率.DropDownHoverColor = Color.FromArgb(CByte(20), CByte(220), CByte(220), CByte(220))
        MCB_目标帧率.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_目标帧率.DropDownPadding = New Padding(10)
        MCB_目标帧率.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_目标帧率.DropDownSelectedForeColor = Color.White
        MCB_目标帧率.Editable = True
        MCB_目标帧率.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_目标帧率.Items.Add("")
        MCB_目标帧率.Items.Add("source_fps*2")
        MCB_目标帧率.Items.Add("48")
        MCB_目标帧率.Items.Add("60")
        MCB_目标帧率.Items.Add("120")
        MCB_目标帧率.Location = New Point(0, 10)
        MCB_目标帧率.Margin = New Padding(2, 2, 2, 2)
        MCB_目标帧率.Name = "MCB_目标帧率"
        MCB_目标帧率.Padding = New Padding(10, 0, 10, 0)
        MCB_目标帧率.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_目标帧率.Size = New Size(150, 32)
        MCB_目标帧率.TabIndex = 16
        MCB_目标帧率.ToolTipGap = -1
        MCB_目标帧率.ToolTipMaxWidth = 350
        MCB_目标帧率.ToolTipPadding = New Padding(15)
        MCB_目标帧率.WaterText = "fps="
        MCB_目标帧率.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' HCL_目标帧率
        ' 
        HCL_目标帧率.AutoSize = True
        HCL_目标帧率.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_目标帧率.Dock = DockStyle.Top
        HCL_目标帧率.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_目标帧率.Location = New Point(20, 44)
        HCL_目标帧率.Margin = New Padding(2)
        HCL_目标帧率.Name = "HCL_目标帧率"
        HCL_目标帧率.Padding = New Padding(0, 20, 0, 0)
        HCL_目标帧率.Size = New Size(544, 43)
        HCL_目标帧率.TabIndex = 24
        HCL_目标帧率.Text = "<span style=""font-size:13; color:Silver"">目标帧率</span>   然后就不要在其他地方设置帧率了"
        ' 
        ' MCK_插帧总开关
        ' 
        MCK_插帧总开关.AutoSize = True
        MCK_插帧总开关.BoxBorderRadius = 5
        MCK_插帧总开关.BoxBorderSize = 0
        MCK_插帧总开关.BoxCheckedBackColor = Color.OliveDrab
        MCK_插帧总开关.BoxInnerPadding = 6
        MCK_插帧总开关.BoxSize = 24
        MCK_插帧总开关.BoxTextSpacing = 10
        MCK_插帧总开关.BoxUncheckedBackColor = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCK_插帧总开关.CheckMarkWidth = 3F
        MCK_插帧总开关.Dock = DockStyle.Top
        MCK_插帧总开关.Location = New Point(20, 20)
        MCK_插帧总开关.Name = "MCK_插帧总开关"
        MCK_插帧总开关.Size = New Size(544, 24)
        MCK_插帧总开关.TabIndex = 23
        MCK_插帧总开关.Text = "插帧总开关 / 勾选才会使用 / 至少需要 RTX30 显卡"
        ' 
        ' Form_v6_参数面板_插帧_NV_FRUC
        ' 
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ClientSize = New Size(584, 411)
        Controls.Add(ModernPanel1)
        Font = New Font("Microsoft YaHei UI", 10F)
        ForeColor = Color.Silver
        MaximizeBox = False
        MinimizeBox = False
        MinimumSize = New Size(600, 450)
        Name = "Form_v6_参数面板_插帧_NV_FRUC"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.Manual
        Text = "NVIDIA Vulkan FRUC 光流加速插帧"
        ModernPanel1.ResumeLayout(False)
        ModernPanel1.PerformLayout()
        ModernPanel2.ResumeLayout(False)
        Panel1.ResumeLayout(False)
        Panel2.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents ModernPanel1 As LakeUI.ModernPanel
    Friend WithEvents Panel1 As LakeUI.ModernPanel
    Friend WithEvents MCB_质量和速度 As LakeUI.ModernComboBox
    Friend WithEvents HCL_质量和速度 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel2 As LakeUI.ModernPanel
    Friend WithEvents HCL_目标帧率 As LakeUI.HtmlColorLabel
    Friend WithEvents MCK_插帧总开关 As LakeUI.ModernCheckBox
    Friend WithEvents MCB_目标帧率 As LakeUI.ModernComboBox
    Friend WithEvents ModernPanel2 As LakeUI.ModernPanel
    Friend WithEvents MCB_网格大小 As LakeUI.ModernComboBox
    Friend WithEvents HCL_网格大小 As LakeUI.HtmlColorLabel
    Friend WithEvents HCL_CK流程提示 As LakeUI.HtmlColorLabel
End Class
