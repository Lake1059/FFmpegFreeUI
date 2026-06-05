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
        Dim ToolTipEntry1 As LakeUI.ModernComboBox.ToolTipEntry = New LakeUI.ModernComboBox.ToolTipEntry()
        ModernPanel1 = New LakeUI.ModernPanel()
        Panel6 = New Panel()
        HtmlColorLabel5 = New LakeUI.HtmlColorLabel()
        MCB_动画帧率 = New LakeUI.ModernComboBox()
        HtmlColorLabel3 = New LakeUI.HtmlColorLabel()
        Panel3 = New Panel()
        HtmlColorLabel9 = New LakeUI.HtmlColorLabel()
        MCB_超容器背景映射开销 = New LakeUI.ModernComboBox()
        Panel1 = New Panel()
        HtmlColorLabel8 = New LakeUI.HtmlColorLabel()
        MCB_D2D位图开销 = New LakeUI.ModernComboBox()
        HtmlColorLabel7 = New LakeUI.HtmlColorLabel()
        Panel7 = New Panel()
        HtmlColorLabel12 = New LakeUI.HtmlColorLabel()
        MCB_文字渲染模式 = New LakeUI.ModernComboBox()
        Panel8 = New Panel()
        HtmlColorLabel13 = New LakeUI.HtmlColorLabel()
        MCB_GPU抗锯齿 = New LakeUI.ModernComboBox()
        HtmlColorLabel11 = New LakeUI.HtmlColorLabel()
        Panel4 = New Panel()
        HtmlColorLabel2 = New LakeUI.HtmlColorLabel()
        MCB_SSAA渲染池总量预算 = New LakeUI.ModernComboBox()
        Panel5 = New Panel()
        HtmlColorLabel10 = New LakeUI.HtmlColorLabel()
        MCB_SSAA渲染池缓存命中 = New LakeUI.ModernComboBox()
        Panel2 = New Panel()
        HtmlColorLabel4 = New LakeUI.HtmlColorLabel()
        MCB_SSAA = New LakeUI.ModernComboBox()
        HtmlColorLabel1 = New LakeUI.HtmlColorLabel()
        HtmlColorLabel6 = New LakeUI.HtmlColorLabel()
        ModernPanel1.SuspendLayout()
        Panel6.SuspendLayout()
        Panel3.SuspendLayout()
        Panel1.SuspendLayout()
        Panel7.SuspendLayout()
        Panel8.SuspendLayout()
        Panel4.SuspendLayout()
        Panel5.SuspendLayout()
        Panel2.SuspendLayout()
        SuspendLayout()
        ' 
        ' ModernPanel1
        ' 
        ModernPanel1.BackColor1 = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ModernPanel1.BorderSize = 0
        ModernPanel1.Controls.Add(Panel6)
        ModernPanel1.Controls.Add(HtmlColorLabel3)
        ModernPanel1.Controls.Add(Panel3)
        ModernPanel1.Controls.Add(Panel1)
        ModernPanel1.Controls.Add(HtmlColorLabel7)
        ModernPanel1.Controls.Add(Panel7)
        ModernPanel1.Controls.Add(Panel8)
        ModernPanel1.Controls.Add(HtmlColorLabel11)
        ModernPanel1.Controls.Add(Panel4)
        ModernPanel1.Controls.Add(Panel5)
        ModernPanel1.Controls.Add(Panel2)
        ModernPanel1.Controls.Add(HtmlColorLabel1)
        ModernPanel1.Controls.Add(HtmlColorLabel6)
        ModernPanel1.Dock = DockStyle.Fill
        ModernPanel1.Location = New Point(0, 0)
        ModernPanel1.Name = "ModernPanel1"
        ModernPanel1.Padding = New Padding(20)
        ModernPanel1.ScrollBarMode = LakeUI.ModernPanel.ScrollMode.Vertical
        ModernPanel1.Size = New Size(742, 641)
        ModernPanel1.TabIndex = 0
        ' 
        ' Panel6
        ' 
        Panel6.Controls.Add(HtmlColorLabel5)
        Panel6.Controls.Add(MCB_动画帧率)
        Panel6.Dock = DockStyle.Top
        Panel6.Location = New Point(20, 519)
        Panel6.Name = "Panel6"
        Panel6.Padding = New Padding(0, 10, 0, 0)
        Panel6.Size = New Size(702, 42)
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
        HtmlColorLabel5.Size = New Size(502, 32)
        HtmlColorLabel5.TabIndex = 19
        HtmlColorLabel5.Text = "LakeUI 2.6+ 使用统一调度器来优化体验，无需特意高刷"
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
        MCB_动画帧率.Items.Add("15")
        MCB_动画帧率.Items.Add("30")
        MCB_动画帧率.Items.Add("45")
        MCB_动画帧率.Items.Add("50")
        MCB_动画帧率.Items.Add("60")
        MCB_动画帧率.Items.Add("90")
        MCB_动画帧率.Items.Add("120")
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
        HtmlColorLabel3.Location = New Point(20, 474)
        HtmlColorLabel3.Margin = New Padding(2)
        HtmlColorLabel3.Name = "HtmlColorLabel3"
        HtmlColorLabel3.Padding = New Padding(0, 20, 0, 0)
        HtmlColorLabel3.Size = New Size(702, 45)
        HtmlColorLabel3.TabIndex = 22
        HtmlColorLabel3.Text = "<span style=""font-size:13; color:Silver"">动画帧率</span>   帧率属性的更新与字体更新绑定，动一下字体或者重启来应用"
        ' 
        ' Panel3
        ' 
        Panel3.Controls.Add(HtmlColorLabel9)
        Panel3.Controls.Add(MCB_超容器背景映射开销)
        Panel3.Dock = DockStyle.Top
        Panel3.Location = New Point(20, 432)
        Panel3.Name = "Panel3"
        Panel3.Padding = New Padding(0, 10, 0, 0)
        Panel3.Size = New Size(702, 42)
        Panel3.TabIndex = 31
        ' 
        ' HtmlColorLabel9
        ' 
        HtmlColorLabel9.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel9.Dock = DockStyle.Fill
        HtmlColorLabel9.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HtmlColorLabel9.Location = New Point(200, 10)
        HtmlColorLabel9.Margin = New Padding(2)
        HtmlColorLabel9.Name = "HtmlColorLabel9"
        HtmlColorLabel9.Padding = New Padding(10, 0, 0, 0)
        HtmlColorLabel9.Size = New Size(502, 32)
        HtmlColorLabel9.TabIndex = 17
        HtmlColorLabel9.Text = "<span style=""color:Silver"">超容器背景映射开销</span>   玻璃背景模式下影响效果显著"
        HtmlColorLabel9.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.MiddleLeft
        ' 
        ' MCB_超容器背景映射开销
        ' 
        MCB_超容器背景映射开销.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_超容器背景映射开销.BorderRadius = 10
        MCB_超容器背景映射开销.BorderSize = 0
        MCB_超容器背景映射开销.Dock = DockStyle.Left
        MCB_超容器背景映射开销.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_超容器背景映射开销.DropDownHoverAnimationDuration = 0
        MCB_超容器背景映射开销.DropDownHoverColor = Color.FromArgb(CByte(80), CByte(0), CByte(0), CByte(0))
        MCB_超容器背景映射开销.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_超容器背景映射开销.DropDownPadding = New Padding(10)
        MCB_超容器背景映射开销.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_超容器背景映射开销.DropDownSelectedForeColor = Color.White
        MCB_超容器背景映射开销.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_超容器背景映射开销.Items.Add("没有预算 ! 不推荐")
        MCB_超容器背景映射开销.Items.Add("12 条目")
        MCB_超容器背景映射开销.Items.Add("24 条目（默认）")
        MCB_超容器背景映射开销.Items.Add("48 条目")
        MCB_超容器背景映射开销.Items.Add("96 条目")
        MCB_超容器背景映射开销.Location = New Point(0, 10)
        MCB_超容器背景映射开销.Margin = New Padding(2, 2, 2, 2)
        MCB_超容器背景映射开销.Name = "MCB_超容器背景映射开销"
        MCB_超容器背景映射开销.Padding = New Padding(10, 0, 10, 0)
        MCB_超容器背景映射开销.Size = New Size(200, 32)
        MCB_超容器背景映射开销.TabIndex = 0
        MCB_超容器背景映射开销.ToolTipGap = -1
        MCB_超容器背景映射开销.ToolTipMaxWidth = 350
        MCB_超容器背景映射开销.ToolTipPadding = New Padding(15)
        MCB_超容器背景映射开销.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' Panel1
        ' 
        Panel1.Controls.Add(HtmlColorLabel8)
        Panel1.Controls.Add(MCB_D2D位图开销)
        Panel1.Dock = DockStyle.Top
        Panel1.Location = New Point(20, 390)
        Panel1.Name = "Panel1"
        Panel1.Padding = New Padding(0, 10, 0, 0)
        Panel1.Size = New Size(702, 42)
        Panel1.TabIndex = 30
        ' 
        ' HtmlColorLabel8
        ' 
        HtmlColorLabel8.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel8.Dock = DockStyle.Fill
        HtmlColorLabel8.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HtmlColorLabel8.Location = New Point(200, 10)
        HtmlColorLabel8.Margin = New Padding(2)
        HtmlColorLabel8.Name = "HtmlColorLabel8"
        HtmlColorLabel8.Padding = New Padding(10, 0, 0, 0)
        HtmlColorLabel8.Size = New Size(502, 32)
        HtmlColorLabel8.TabIndex = 17
        HtmlColorLabel8.Text = "<span style=""color:Silver"">D2D Image 开销</span>   仅影响 Image，不影响图形和文字"
        HtmlColorLabel8.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.MiddleLeft
        ' 
        ' MCB_D2D位图开销
        ' 
        MCB_D2D位图开销.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_D2D位图开销.BorderRadius = 10
        MCB_D2D位图开销.BorderSize = 0
        MCB_D2D位图开销.Dock = DockStyle.Left
        MCB_D2D位图开销.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_D2D位图开销.DropDownHoverAnimationDuration = 0
        MCB_D2D位图开销.DropDownHoverColor = Color.FromArgb(CByte(80), CByte(0), CByte(0), CByte(0))
        MCB_D2D位图开销.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_D2D位图开销.DropDownPadding = New Padding(10)
        MCB_D2D位图开销.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_D2D位图开销.DropDownSelectedForeColor = Color.White
        MCB_D2D位图开销.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_D2D位图开销.Items.Add("没有预算 ! 不推荐")
        MCB_D2D位图开销.Items.Add("32 MiB")
        MCB_D2D位图开销.Items.Add("64 MiB（默认）")
        MCB_D2D位图开销.Items.Add("128 MiB")
        MCB_D2D位图开销.Items.Add("256 MiB")
        MCB_D2D位图开销.Items.Add("512 MiB")
        MCB_D2D位图开销.Items.Add("1 GiB（显存高）")
        MCB_D2D位图开销.Location = New Point(0, 10)
        MCB_D2D位图开销.Margin = New Padding(2, 2, 2, 2)
        MCB_D2D位图开销.Name = "MCB_D2D位图开销"
        MCB_D2D位图开销.Padding = New Padding(10, 0, 10, 0)
        MCB_D2D位图开销.Size = New Size(200, 32)
        MCB_D2D位图开销.TabIndex = 0
        MCB_D2D位图开销.ToolTipGap = -1
        MCB_D2D位图开销.ToolTipMaxWidth = 350
        MCB_D2D位图开销.ToolTipPadding = New Padding(15)
        MCB_D2D位图开销.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' HtmlColorLabel7
        ' 
        HtmlColorLabel7.AutoSize = True
        HtmlColorLabel7.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel7.Dock = DockStyle.Top
        HtmlColorLabel7.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HtmlColorLabel7.Location = New Point(20, 345)
        HtmlColorLabel7.Margin = New Padding(2)
        HtmlColorLabel7.Name = "HtmlColorLabel7"
        HtmlColorLabel7.Padding = New Padding(0, 20, 0, 0)
        HtmlColorLabel7.Size = New Size(702, 45)
        HtmlColorLabel7.TabIndex = 29
        HtmlColorLabel7.Text = "<span style=""font-size:13; color:Silver"">DirectX 开销平衡</span>   预算越少计算需求越大，反之显存占用更多"
        ' 
        ' Panel7
        ' 
        Panel7.Controls.Add(HtmlColorLabel12)
        Panel7.Controls.Add(MCB_文字渲染模式)
        Panel7.Dock = DockStyle.Top
        Panel7.Location = New Point(20, 303)
        Panel7.Name = "Panel7"
        Panel7.Padding = New Padding(0, 10, 0, 0)
        Panel7.Size = New Size(702, 42)
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
        HtmlColorLabel12.Size = New Size(502, 32)
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
        ToolTipEntry1.ItemText = "矢量几何 (试验选项)"
        ToolTipEntry1.ToolTipText = "矢量几何 Outline 是仿制 MacType 的超高质量绘制模式，彻底避开 Windows 的 GASP表 和 TrueType hinting 字节码，让不想安装三方软件的用户也能体验到 Web 甚至 macOS 的文字效果，其效果由开发者定制，如有不适请及时切换到其他模式"
        MCB_文字渲染模式.ItemToolTips.AddRange(New LakeUI.ModernComboBox.ToolTipEntry() {ToolTipEntry1})
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
        Panel8.Location = New Point(20, 261)
        Panel8.Name = "Panel8"
        Panel8.Padding = New Padding(0, 10, 0, 0)
        Panel8.Size = New Size(702, 42)
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
        HtmlColorLabel13.Size = New Size(502, 32)
        HtmlColorLabel13.TabIndex = 17
        HtmlColorLabel13.Text = "<span style=""color:Silver"">GPU 抗锯齿</span>   会影响一些细节呈现"
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
        HtmlColorLabel11.Location = New Point(20, 216)
        HtmlColorLabel11.Margin = New Padding(2)
        HtmlColorLabel11.Name = "HtmlColorLabel11"
        HtmlColorLabel11.Padding = New Padding(0, 20, 0, 0)
        HtmlColorLabel11.Size = New Size(702, 45)
        HtmlColorLabel11.TabIndex = 25
        HtmlColorLabel11.Text = "<span style=""font-size:13; color:Silver"">DirectX 图形质量</span>   GPU 绘图的基础质量就比 CPU 高，一般无需调整"
        ' 
        ' Panel4
        ' 
        Panel4.Controls.Add(HtmlColorLabel2)
        Panel4.Controls.Add(MCB_SSAA渲染池总量预算)
        Panel4.Dock = DockStyle.Top
        Panel4.Location = New Point(20, 174)
        Panel4.Name = "Panel4"
        Panel4.Padding = New Padding(0, 10, 0, 0)
        Panel4.Size = New Size(702, 42)
        Panel4.TabIndex = 33
        ' 
        ' HtmlColorLabel2
        ' 
        HtmlColorLabel2.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel2.Dock = DockStyle.Fill
        HtmlColorLabel2.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HtmlColorLabel2.Location = New Point(200, 10)
        HtmlColorLabel2.Margin = New Padding(2)
        HtmlColorLabel2.Name = "HtmlColorLabel2"
        HtmlColorLabel2.Padding = New Padding(10, 0, 0, 0)
        HtmlColorLabel2.Size = New Size(502, 32)
        HtmlColorLabel2.TabIndex = 17
        HtmlColorLabel2.Text = "<span style=""color:Silver"">渲染池总量预算</span>   预算越多响应越快，同时显存占用越多"
        HtmlColorLabel2.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.MiddleLeft
        ' 
        ' MCB_SSAA渲染池总量预算
        ' 
        MCB_SSAA渲染池总量预算.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_SSAA渲染池总量预算.BorderRadius = 10
        MCB_SSAA渲染池总量预算.BorderSize = 0
        MCB_SSAA渲染池总量预算.Dock = DockStyle.Left
        MCB_SSAA渲染池总量预算.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_SSAA渲染池总量预算.DropDownHoverAnimationDuration = 0
        MCB_SSAA渲染池总量预算.DropDownHoverColor = Color.FromArgb(CByte(80), CByte(0), CByte(0), CByte(0))
        MCB_SSAA渲染池总量预算.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_SSAA渲染池总量预算.DropDownPadding = New Padding(10)
        MCB_SSAA渲染池总量预算.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_SSAA渲染池总量预算.DropDownSelectedForeColor = Color.White
        MCB_SSAA渲染池总量预算.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_SSAA渲染池总量预算.Items.Add("没有预算 ! 不推荐")
        MCB_SSAA渲染池总量预算.Items.Add("32 MiB")
        MCB_SSAA渲染池总量预算.Items.Add("64 MiB")
        MCB_SSAA渲染池总量预算.Items.Add("128 MiB")
        MCB_SSAA渲染池总量预算.Items.Add("256 MiB（默认）")
        MCB_SSAA渲染池总量预算.Items.Add("512 MiB")
        MCB_SSAA渲染池总量预算.Items.Add("1 GiB（显存高）")
        MCB_SSAA渲染池总量预算.Location = New Point(0, 10)
        MCB_SSAA渲染池总量预算.Margin = New Padding(2, 2, 2, 2)
        MCB_SSAA渲染池总量预算.Name = "MCB_SSAA渲染池总量预算"
        MCB_SSAA渲染池总量预算.Padding = New Padding(10, 0, 10, 0)
        MCB_SSAA渲染池总量预算.Size = New Size(200, 32)
        MCB_SSAA渲染池总量预算.TabIndex = 0
        MCB_SSAA渲染池总量预算.ToolTipGap = -1
        MCB_SSAA渲染池总量预算.ToolTipMaxWidth = 350
        MCB_SSAA渲染池总量预算.ToolTipPadding = New Padding(15)
        MCB_SSAA渲染池总量预算.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' Panel5
        ' 
        Panel5.Controls.Add(HtmlColorLabel10)
        Panel5.Controls.Add(MCB_SSAA渲染池缓存命中)
        Panel5.Dock = DockStyle.Top
        Panel5.Location = New Point(20, 132)
        Panel5.Name = "Panel5"
        Panel5.Padding = New Padding(0, 10, 0, 0)
        Panel5.Size = New Size(702, 42)
        Panel5.TabIndex = 32
        ' 
        ' HtmlColorLabel10
        ' 
        HtmlColorLabel10.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel10.Dock = DockStyle.Fill
        HtmlColorLabel10.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HtmlColorLabel10.Location = New Point(200, 10)
        HtmlColorLabel10.Margin = New Padding(2)
        HtmlColorLabel10.Name = "HtmlColorLabel10"
        HtmlColorLabel10.Padding = New Padding(10, 0, 0, 0)
        HtmlColorLabel10.Size = New Size(502, 32)
        HtmlColorLabel10.TabIndex = 17
        HtmlColorLabel10.Text = "<span style=""color:Silver"">渲染池缓存命中</span>   预算越少计算需求越大"
        HtmlColorLabel10.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.MiddleLeft
        ' 
        ' MCB_SSAA渲染池缓存命中
        ' 
        MCB_SSAA渲染池缓存命中.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_SSAA渲染池缓存命中.BorderRadius = 10
        MCB_SSAA渲染池缓存命中.BorderSize = 0
        MCB_SSAA渲染池缓存命中.Dock = DockStyle.Left
        MCB_SSAA渲染池缓存命中.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_SSAA渲染池缓存命中.DropDownHoverAnimationDuration = 0
        MCB_SSAA渲染池缓存命中.DropDownHoverColor = Color.FromArgb(CByte(80), CByte(0), CByte(0), CByte(0))
        MCB_SSAA渲染池缓存命中.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_SSAA渲染池缓存命中.DropDownPadding = New Padding(10)
        MCB_SSAA渲染池缓存命中.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_SSAA渲染池缓存命中.DropDownSelectedForeColor = Color.White
        MCB_SSAA渲染池缓存命中.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_SSAA渲染池缓存命中.Items.Add("极低预算（负载低）")
        MCB_SSAA渲染池缓存命中.Items.Add("较少预算")
        MCB_SSAA渲染池缓存命中.Items.Add("适中预算（默认）")
        MCB_SSAA渲染池缓存命中.Items.Add("较多预算")
        MCB_SSAA渲染池缓存命中.Items.Add("大量预算（负载高）")
        MCB_SSAA渲染池缓存命中.Location = New Point(0, 10)
        MCB_SSAA渲染池缓存命中.Margin = New Padding(2, 2, 2, 2)
        MCB_SSAA渲染池缓存命中.Name = "MCB_SSAA渲染池缓存命中"
        MCB_SSAA渲染池缓存命中.Padding = New Padding(10, 0, 10, 0)
        MCB_SSAA渲染池缓存命中.Size = New Size(200, 32)
        MCB_SSAA渲染池缓存命中.TabIndex = 0
        MCB_SSAA渲染池缓存命中.ToolTipGap = -1
        MCB_SSAA渲染池缓存命中.ToolTipMaxWidth = 350
        MCB_SSAA渲染池缓存命中.ToolTipPadding = New Padding(15)
        MCB_SSAA渲染池缓存命中.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' Panel2
        ' 
        Panel2.Controls.Add(HtmlColorLabel4)
        Panel2.Controls.Add(MCB_SSAA)
        Panel2.Dock = DockStyle.Top
        Panel2.Location = New Point(20, 90)
        Panel2.Name = "Panel2"
        Panel2.Padding = New Padding(0, 10, 0, 0)
        Panel2.Size = New Size(702, 42)
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
        HtmlColorLabel4.Size = New Size(502, 32)
        HtmlColorLabel4.TabIndex = 20
        HtmlColorLabel4.Text = "<span style=""color:Silver"">全局覆盖</span>   仅影响图形，不影响 Image 和文字"
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
        HtmlColorLabel1.Size = New Size(702, 25)
        HtmlColorLabel1.TabIndex = 8
        HtmlColorLabel1.Text = "<span style=""font-size:13; color:Silver"">SSAA 超采样抗锯齿</span>   正常情况不需要使用，性能消耗指数级增加！"
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
        HtmlColorLabel6.Size = New Size(702, 45)
        HtmlColorLabel6.TabIndex = 24
        HtmlColorLabel6.Text = "<span style=""font-size:13; color:Silver"">LakeUI 使用 DirectX GPU 绘制</span>   由 Vortice 提供支持"
        ' 
        ' Form_v6_设置_LakeUI性能选项
        ' 
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ClientSize = New Size(742, 641)
        Controls.Add(ModernPanel1)
        Font = New Font("Microsoft YaHei UI", 10F)
        ForeColor = Color.Silver
        Name = "Form_v6_设置_LakeUI性能选项"
        Text = "Form_v6_设置_LakeUI性能选项"
        ModernPanel1.ResumeLayout(False)
        ModernPanel1.PerformLayout()
        Panel6.ResumeLayout(False)
        Panel3.ResumeLayout(False)
        Panel1.ResumeLayout(False)
        Panel7.ResumeLayout(False)
        Panel8.ResumeLayout(False)
        Panel4.ResumeLayout(False)
        Panel5.ResumeLayout(False)
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
    Friend WithEvents HtmlColorLabel7 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents HtmlColorLabel9 As LakeUI.HtmlColorLabel
    Friend WithEvents MCB_超容器背景映射开销 As LakeUI.ModernComboBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents HtmlColorLabel8 As LakeUI.HtmlColorLabel
    Friend WithEvents MCB_D2D位图开销 As LakeUI.ModernComboBox
    Friend WithEvents Panel4 As Panel
    Friend WithEvents HtmlColorLabel2 As LakeUI.HtmlColorLabel
    Friend WithEvents MCB_SSAA渲染池总量预算 As LakeUI.ModernComboBox
    Friend WithEvents Panel5 As Panel
    Friend WithEvents HtmlColorLabel10 As LakeUI.HtmlColorLabel
    Friend WithEvents MCB_SSAA渲染池缓存命中 As LakeUI.ModernComboBox
End Class
