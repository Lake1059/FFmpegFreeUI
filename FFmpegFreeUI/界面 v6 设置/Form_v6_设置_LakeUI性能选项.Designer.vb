<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_v6_设置_LakeUI性能选项
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
        Dim ToolTipEntry2 As LakeUI.ModernComboBox.ToolTipEntry = New LakeUI.ModernComboBox.ToolTipEntry()
        ModernPanel1 = New LakeUI.ModernPanel()
        Panel6 = New Panel()
        HtmlColorLabel5 = New LakeUI.HtmlColorLabel()
        MCB_动画帧率 = New LakeUI.ModernComboBox()
        HtmlColorLabel3 = New LakeUI.HtmlColorLabel()
        Panel7 = New Panel()
        HtmlColorLabel12 = New LakeUI.HtmlColorLabel()
        MCB_文字渲染模式 = New LakeUI.ModernComboBox()
        Panel8 = New Panel()
        HtmlColorLabel13 = New LakeUI.HtmlColorLabel()
        MCB_GPU抗锯齿 = New LakeUI.ModernComboBox()
        HtmlColorLabel11 = New LakeUI.HtmlColorLabel()
        Panel2 = New Panel()
        HtmlColorLabel4 = New LakeUI.HtmlColorLabel()
        MCB_SSAA = New LakeUI.ModernComboBox()
        HtmlColorLabel1 = New LakeUI.HtmlColorLabel()
        HtmlColorLabel6 = New LakeUI.HtmlColorLabel()
        HtmlColorLabel2 = New LakeUI.HtmlColorLabel()
        ModernPanel1.SuspendLayout()
        Panel6.SuspendLayout()
        Panel7.SuspendLayout()
        Panel8.SuspendLayout()
        Panel2.SuspendLayout()
        SuspendLayout()
        ' 
        ' ModernPanel1
        ' 
        ModernPanel1.BackColor1 = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ModernPanel1.BorderSize = 0
        ModernPanel1.Controls.Add(HtmlColorLabel2)
        ModernPanel1.Controls.Add(Panel6)
        ModernPanel1.Controls.Add(HtmlColorLabel3)
        ModernPanel1.Controls.Add(Panel7)
        ModernPanel1.Controls.Add(Panel8)
        ModernPanel1.Controls.Add(HtmlColorLabel11)
        ModernPanel1.Controls.Add(Panel2)
        ModernPanel1.Controls.Add(HtmlColorLabel1)
        ModernPanel1.Controls.Add(HtmlColorLabel6)
        ModernPanel1.Dock = DockStyle.Fill
        ModernPanel1.Location = New Point(0, 0)
        ModernPanel1.Name = "ModernPanel1"
        ModernPanel1.Padding = New Padding(20)
        ModernPanel1.ScrollBarMode = LakeUI.ModernPanel.ScrollMode.Vertical
        ModernPanel1.Size = New Size(696, 641)
        ModernPanel1.TabIndex = 0
        ' 
        ' Panel6
        ' 
        Panel6.Controls.Add(HtmlColorLabel5)
        Panel6.Controls.Add(MCB_动画帧率)
        Panel6.Dock = DockStyle.Top
        Panel6.Location = New Point(20, 306)
        Panel6.Name = "Panel6"
        Panel6.Padding = New Padding(0, 10, 0, 0)
        Panel6.Size = New Size(656, 42)
        Panel6.TabIndex = 23
        ' 
        ' HtmlColorLabel5
        ' 
        HtmlColorLabel5.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel5.Dock = DockStyle.Fill
        HtmlColorLabel5.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HtmlColorLabel5.Location = New Point(200, 10)
        HtmlColorLabel5.Margin = New Padding(2)
        HtmlColorLabel5.Name = "HtmlColorLabel5"
        HtmlColorLabel5.Padding = New Padding(10, 0, 0, 0)
        HtmlColorLabel5.Size = New Size(456, 32)
        HtmlColorLabel5.TabIndex = 19
        HtmlColorLabel5.Text = "由高精度计时器驱动，支持超高刷，理论极限 1000 帧"
        HtmlColorLabel5.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.MiddleLeft
        ' 
        ' MCB_动画帧率
        ' 
        MCB_动画帧率.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_动画帧率.BorderRadius = 10
        MCB_动画帧率.BorderSize = 0
        MCB_动画帧率.Dock = DockStyle.Left
        MCB_动画帧率.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_动画帧率.DropDownHoverAnimationDuration = 0
        MCB_动画帧率.DropDownHoverColor = Color.FromArgb(CByte(80), CByte(0), CByte(0), CByte(0))
        MCB_动画帧率.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_动画帧率.DropDownPadding = New Padding(10)
        MCB_动画帧率.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_动画帧率.DropDownSelectedForeColor = Color.White
        MCB_动画帧率.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_动画帧率.Items.Add("0")
        MCB_动画帧率.Items.Add("30")
        MCB_动画帧率.Items.Add("60")
        MCB_动画帧率.Items.Add("90")
        MCB_动画帧率.Items.Add("120")
        MCB_动画帧率.Items.Add("144")
        MCB_动画帧率.Items.Add("180")
        MCB_动画帧率.Items.Add("240")
        MCB_动画帧率.Items.Add("300")
        MCB_动画帧率.Items.Add("360")
        MCB_动画帧率.Location = New Point(0, 10)
        MCB_动画帧率.Margin = New Padding(2, 2, 2, 2)
        MCB_动画帧率.Name = "MCB_动画帧率"
        MCB_动画帧率.Padding = New Padding(10, 0, 10, 0)
        MCB_动画帧率.Size = New Size(200, 32)
        MCB_动画帧率.TabIndex = 0
        MCB_动画帧率.ToolTipGap = -1
        MCB_动画帧率.ToolTipMaxWidth = 350
        MCB_动画帧率.ToolTipPadding = New Padding(15)
        MCB_动画帧率.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' HtmlColorLabel3
        ' 
        HtmlColorLabel3.AutoSize = True
        HtmlColorLabel3.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel3.Dock = DockStyle.Top
        HtmlColorLabel3.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HtmlColorLabel3.Location = New Point(20, 261)
        HtmlColorLabel3.Margin = New Padding(2)
        HtmlColorLabel3.Name = "HtmlColorLabel3"
        HtmlColorLabel3.Padding = New Padding(0, 20, 0, 0)
        HtmlColorLabel3.Size = New Size(656, 45)
        HtmlColorLabel3.TabIndex = 22
        HtmlColorLabel3.Text = "<span style=""font-size:13; color:Silver"">动画帧率</span>   在当前应用程序中此设置仅对特定控件生效"
        ' 
        ' Panel7
        ' 
        Panel7.Controls.Add(HtmlColorLabel12)
        Panel7.Controls.Add(MCB_文字渲染模式)
        Panel7.Dock = DockStyle.Top
        Panel7.Location = New Point(20, 219)
        Panel7.Name = "Panel7"
        Panel7.Padding = New Padding(0, 10, 0, 0)
        Panel7.Size = New Size(656, 42)
        Panel7.TabIndex = 27
        ' 
        ' HtmlColorLabel12
        ' 
        HtmlColorLabel12.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel12.Dock = DockStyle.Fill
        HtmlColorLabel12.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HtmlColorLabel12.Location = New Point(200, 10)
        HtmlColorLabel12.Margin = New Padding(2)
        HtmlColorLabel12.Name = "HtmlColorLabel12"
        HtmlColorLabel12.Padding = New Padding(10, 0, 0, 0)
        HtmlColorLabel12.Size = New Size(456, 32)
        HtmlColorLabel12.TabIndex = 18
        HtmlColorLabel12.Text = "<span style=""color:Silver"">文字渲染模式</span>   ClearType 模式支持 MacType 这类软件"
        HtmlColorLabel12.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.MiddleLeft
        ' 
        ' MCB_文字渲染模式
        ' 
        MCB_文字渲染模式.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_文字渲染模式.BorderRadius = 10
        MCB_文字渲染模式.BorderSize = 0
        MCB_文字渲染模式.Dock = DockStyle.Left
        MCB_文字渲染模式.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_文字渲染模式.DropDownHoverAnimationDuration = 0
        MCB_文字渲染模式.DropDownHoverColor = Color.FromArgb(CByte(80), CByte(0), CByte(0), CByte(0))
        MCB_文字渲染模式.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_文字渲染模式.DropDownPadding = New Padding(10)
        MCB_文字渲染模式.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_文字渲染模式.DropDownSelectedForeColor = Color.White
        MCB_文字渲染模式.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_文字渲染模式.Items.Add("ClearType 默认推荐")
        MCB_文字渲染模式.Items.Add("灰度抗锯齿")
        MCB_文字渲染模式.Items.Add("禁用抗锯齿")
        MCB_文字渲染模式.Items.Add("矢量几何 (试验选项)")
        ToolTipEntry2.ItemText = "矢量几何 (试验选项)"
        ToolTipEntry2.ToolTipText = "矢量几何 Outline 是仿制 MacType 的超高质量绘制模式，彻底避开 Windows 的 GASP表 和 TrueType hinting 字节码，让不想安装三方软件的用户也能体验到 Web 甚至 macOS 的文字效果，其效果由开发者定制，如有不适请及时切换到其他模式"
        MCB_文字渲染模式.ItemToolTips.AddRange(New LakeUI.ModernComboBox.ToolTipEntry() {ToolTipEntry2})
        MCB_文字渲染模式.Location = New Point(0, 10)
        MCB_文字渲染模式.Margin = New Padding(2, 2, 2, 2)
        MCB_文字渲染模式.Name = "MCB_文字渲染模式"
        MCB_文字渲染模式.Padding = New Padding(10, 0, 10, 0)
        MCB_文字渲染模式.Size = New Size(200, 32)
        MCB_文字渲染模式.TabIndex = 0
        MCB_文字渲染模式.ToolTipGap = -1
        MCB_文字渲染模式.ToolTipMaxWidth = 350
        MCB_文字渲染模式.ToolTipPadding = New Padding(15)
        MCB_文字渲染模式.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' Panel8
        ' 
        Panel8.Controls.Add(HtmlColorLabel13)
        Panel8.Controls.Add(MCB_GPU抗锯齿)
        Panel8.Dock = DockStyle.Top
        Panel8.Location = New Point(20, 177)
        Panel8.Name = "Panel8"
        Panel8.Padding = New Padding(0, 10, 0, 0)
        Panel8.Size = New Size(656, 42)
        Panel8.TabIndex = 26
        ' 
        ' HtmlColorLabel13
        ' 
        HtmlColorLabel13.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel13.Dock = DockStyle.Fill
        HtmlColorLabel13.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HtmlColorLabel13.Location = New Point(200, 10)
        HtmlColorLabel13.Margin = New Padding(2)
        HtmlColorLabel13.Name = "HtmlColorLabel13"
        HtmlColorLabel13.Padding = New Padding(10, 0, 0, 0)
        HtmlColorLabel13.Size = New Size(456, 32)
        HtmlColorLabel13.TabIndex = 17
        HtmlColorLabel13.Text = "<span style=""color:Silver"">GPU 抗锯齿</span>   如果显卡 3D 性能吃紧可以关闭"
        HtmlColorLabel13.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.MiddleLeft
        ' 
        ' MCB_GPU抗锯齿
        ' 
        MCB_GPU抗锯齿.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_GPU抗锯齿.BorderRadius = 10
        MCB_GPU抗锯齿.BorderSize = 0
        MCB_GPU抗锯齿.Dock = DockStyle.Left
        MCB_GPU抗锯齿.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_GPU抗锯齿.DropDownHoverAnimationDuration = 0
        MCB_GPU抗锯齿.DropDownHoverColor = Color.FromArgb(CByte(80), CByte(0), CByte(0), CByte(0))
        MCB_GPU抗锯齿.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_GPU抗锯齿.DropDownPadding = New Padding(10)
        MCB_GPU抗锯齿.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_GPU抗锯齿.DropDownSelectedForeColor = Color.White
        MCB_GPU抗锯齿.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_GPU抗锯齿.Items.Add("启用抗锯齿")
        MCB_GPU抗锯齿.Items.Add("禁用抗锯齿")
        MCB_GPU抗锯齿.Location = New Point(0, 10)
        MCB_GPU抗锯齿.Margin = New Padding(2, 2, 2, 2)
        MCB_GPU抗锯齿.Name = "MCB_GPU抗锯齿"
        MCB_GPU抗锯齿.Padding = New Padding(10, 0, 10, 0)
        MCB_GPU抗锯齿.Size = New Size(200, 32)
        MCB_GPU抗锯齿.TabIndex = 0
        MCB_GPU抗锯齿.ToolTipGap = -1
        MCB_GPU抗锯齿.ToolTipMaxWidth = 350
        MCB_GPU抗锯齿.ToolTipPadding = New Padding(15)
        MCB_GPU抗锯齿.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' HtmlColorLabel11
        ' 
        HtmlColorLabel11.AutoSize = True
        HtmlColorLabel11.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel11.Dock = DockStyle.Top
        HtmlColorLabel11.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HtmlColorLabel11.Location = New Point(20, 132)
        HtmlColorLabel11.Margin = New Padding(2)
        HtmlColorLabel11.Name = "HtmlColorLabel11"
        HtmlColorLabel11.Padding = New Padding(0, 20, 0, 0)
        HtmlColorLabel11.Size = New Size(656, 45)
        HtmlColorLabel11.TabIndex = 25
        HtmlColorLabel11.Text = "<span style=""font-size:13; color:Silver"">DirectX 图形质量</span>   GPU 绘图的基础质量就比 CPU 高，一般无需调整"
        ' 
        ' Panel2
        ' 
        Panel2.Controls.Add(HtmlColorLabel4)
        Panel2.Controls.Add(MCB_SSAA)
        Panel2.Dock = DockStyle.Top
        Panel2.Location = New Point(20, 90)
        Panel2.Name = "Panel2"
        Panel2.Padding = New Padding(0, 10, 0, 0)
        Panel2.Size = New Size(656, 42)
        Panel2.TabIndex = 13
        ' 
        ' HtmlColorLabel4
        ' 
        HtmlColorLabel4.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel4.Dock = DockStyle.Fill
        HtmlColorLabel4.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HtmlColorLabel4.Location = New Point(200, 10)
        HtmlColorLabel4.Margin = New Padding(2)
        HtmlColorLabel4.Name = "HtmlColorLabel4"
        HtmlColorLabel4.Padding = New Padding(10, 0, 0, 0)
        HtmlColorLabel4.Size = New Size(456, 32)
        HtmlColorLabel4.TabIndex = 20
        HtmlColorLabel4.Text = "会对除文字以外的所有绘图生效，一般不需要"
        HtmlColorLabel4.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.MiddleLeft
        ' 
        ' MCB_SSAA
        ' 
        MCB_SSAA.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_SSAA.BorderRadius = 10
        MCB_SSAA.BorderSize = 0
        MCB_SSAA.Dock = DockStyle.Left
        MCB_SSAA.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_SSAA.DropDownHoverAnimationDuration = 0
        MCB_SSAA.DropDownHoverColor = Color.FromArgb(CByte(80), CByte(0), CByte(0), CByte(0))
        MCB_SSAA.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_SSAA.DropDownPadding = New Padding(10)
        MCB_SSAA.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_SSAA.DropDownSelectedForeColor = Color.White
        MCB_SSAA.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_SSAA.Items.Add("保持控件独立设定")
        MCB_SSAA.Items.Add("x2")
        MCB_SSAA.Items.Add("x3")
        MCB_SSAA.Items.Add("x4")
        MCB_SSAA.Location = New Point(0, 10)
        MCB_SSAA.Margin = New Padding(2, 2, 2, 2)
        MCB_SSAA.Name = "MCB_SSAA"
        MCB_SSAA.Padding = New Padding(10, 0, 10, 0)
        MCB_SSAA.Size = New Size(200, 32)
        MCB_SSAA.TabIndex = 0
        MCB_SSAA.ToolTipGap = -1
        MCB_SSAA.ToolTipMaxWidth = 350
        MCB_SSAA.ToolTipPadding = New Padding(15)
        MCB_SSAA.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' HtmlColorLabel1
        ' 
        HtmlColorLabel1.AutoSize = True
        HtmlColorLabel1.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel1.Dock = DockStyle.Top
        HtmlColorLabel1.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HtmlColorLabel1.Location = New Point(20, 65)
        HtmlColorLabel1.Margin = New Padding(2)
        HtmlColorLabel1.Name = "HtmlColorLabel1"
        HtmlColorLabel1.Size = New Size(656, 25)
        HtmlColorLabel1.TabIndex = 8
        HtmlColorLabel1.Text = "<span style=""font-size:13; color:Silver"">SSAA 超采样抗锯齿</span>   覆盖所有预先设定，在下一次重绘时生效，下同"
        ' 
        ' HtmlColorLabel6
        ' 
        HtmlColorLabel6.AutoSize = True
        HtmlColorLabel6.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel6.Dock = DockStyle.Top
        HtmlColorLabel6.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HtmlColorLabel6.Location = New Point(20, 20)
        HtmlColorLabel6.Margin = New Padding(2)
        HtmlColorLabel6.Name = "HtmlColorLabel6"
        HtmlColorLabel6.Padding = New Padding(0, 0, 0, 20)
        HtmlColorLabel6.Size = New Size(656, 45)
        HtmlColorLabel6.TabIndex = 24
        HtmlColorLabel6.Text = "<span style=""font-size:13; color:Silver"">LakeUI 使用 DirectX 进行硬件加速</span>   由 Vortice 提供支持"
        ' 
        ' HtmlColorLabel2
        ' 
        HtmlColorLabel2.AutoSize = True
        HtmlColorLabel2.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel2.Dock = DockStyle.Top
        HtmlColorLabel2.Location = New Point(20, 348)
        HtmlColorLabel2.Margin = New Padding(2)
        HtmlColorLabel2.Name = "HtmlColorLabel2"
        HtmlColorLabel2.Padding = New Padding(0, 10, 0, 0)
        HtmlColorLabel2.Size = New Size(656, 31)
        HtmlColorLabel2.TabIndex = 28
        HtmlColorLabel2.Text = "帧率属性的更新与字体更新绑定，在设置后重启软件或着变动一下字体设置就可以应用"
        ' 
        ' Form_v6_设置_LakeUI性能选项
        ' 
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ClientSize = New Size(696, 641)
        Controls.Add(ModernPanel1)
        Font = New Font("Microsoft YaHei UI", 10F)
        ForeColor = Color.Silver
        Name = "Form_v6_设置_LakeUI性能选项"
        Text = "Form_v6_设置_LakeUI性能选项"
        ModernPanel1.ResumeLayout(False)
        ModernPanel1.PerformLayout()
        Panel6.ResumeLayout(False)
        Panel7.ResumeLayout(False)
        Panel8.ResumeLayout(False)
        Panel2.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents ModernPanel1 As LakeUI.ModernPanel
    Friend WithEvents HtmlColorLabel1 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents MCB_SSAA As LakeUI.ModernComboBox
    Friend WithEvents HtmlColorLabel3 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel6 As Panel
    Friend WithEvents HtmlColorLabel5 As LakeUI.HtmlColorLabel
    Friend WithEvents MCB_动画帧率 As LakeUI.ModernComboBox
    Friend WithEvents HtmlColorLabel6 As LakeUI.HtmlColorLabel
    Friend WithEvents HtmlColorLabel4 As LakeUI.HtmlColorLabel
    Friend WithEvents HtmlColorLabel11 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel7 As Panel
    Friend WithEvents HtmlColorLabel12 As LakeUI.HtmlColorLabel
    Friend WithEvents MCB_文字渲染模式 As LakeUI.ModernComboBox
    Friend WithEvents Panel8 As Panel
    Friend WithEvents HtmlColorLabel13 As LakeUI.HtmlColorLabel
    Friend WithEvents MCB_GPU抗锯齿 As LakeUI.ModernComboBox
    Friend WithEvents HtmlColorLabel2 As LakeUI.HtmlColorLabel
End Class
