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
        Dim ToolTipEntry3 As LakeUI.ModernComboBox.ToolTipEntry = New LakeUI.ModernComboBox.ToolTipEntry()
        Dim ToolTipEntry4 As LakeUI.ModernComboBox.ToolTipEntry = New LakeUI.ModernComboBox.ToolTipEntry()
        ModernPanel1 = New LakeUI.ModernPanel()
        Panel6 = New Panel()
        HtmlColorLabel5 = New LakeUI.HtmlColorLabel()
        MCB_动画帧率 = New LakeUI.ModernComboBox()
        HtmlColorLabel3 = New LakeUI.HtmlColorLabel()
        Panel3 = New Panel()
        HtmlColorLabel9 = New LakeUI.HtmlColorLabel()
        MCB_超容器背景映射条目预算 = New LakeUI.ModernComboBox()
        Panel11 = New Panel()
        HtmlColorLabel10 = New LakeUI.HtmlColorLabel()
        MCB_超容器背景映射脏区策略极限 = New LakeUI.ModernComboBox()
        Panel10 = New Panel()
        HtmlColorLabel6 = New LakeUI.HtmlColorLabel()
        MCB_超容器背景映射源位图缓存 = New LakeUI.ModernComboBox()
        Panel5 = New Panel()
        HtmlColorLabel4 = New LakeUI.HtmlColorLabel()
        MCB_DW字体相关预算 = New LakeUI.ModernComboBox()
        Panel4 = New Panel()
        HtmlColorLabel2 = New LakeUI.HtmlColorLabel()
        MCB_D2D每对象画刷缓存数量 = New LakeUI.ModernComboBox()
        Panel1 = New Panel()
        HtmlColorLabel8 = New LakeUI.HtmlColorLabel()
        MCB_D2DImage缓存预算 = New LakeUI.ModernComboBox()
        HtmlColorLabel7 = New LakeUI.HtmlColorLabel()
        Panel9 = New Panel()
        HtmlColorLabel14 = New LakeUI.HtmlColorLabel()
        MCB_SSAA = New LakeUI.ModernComboBox()
        Panel7 = New Panel()
        HtmlColorLabel12 = New LakeUI.HtmlColorLabel()
        MCB_文字渲染模式 = New LakeUI.ModernComboBox()
        Panel8 = New Panel()
        HtmlColorLabel13 = New LakeUI.HtmlColorLabel()
        MCB_GPU抗锯齿 = New LakeUI.ModernComboBox()
        HtmlColorLabel11 = New LakeUI.HtmlColorLabel()
        ModernPanel1.SuspendLayout()
        Panel6.SuspendLayout()
        Panel3.SuspendLayout()
        Panel11.SuspendLayout()
        Panel10.SuspendLayout()
        Panel5.SuspendLayout()
        Panel4.SuspendLayout()
        Panel1.SuspendLayout()
        Panel9.SuspendLayout()
        Panel7.SuspendLayout()
        Panel8.SuspendLayout()
        SuspendLayout()
        '
        ' ModernPanel1
        '
        ModernPanel1.BackColor1 = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ModernPanel1.BorderSize = 0
        ModernPanel1.Controls.Add(Panel6)
        ModernPanel1.Controls.Add(HtmlColorLabel3)
        ModernPanel1.Controls.Add(Panel3)
        ModernPanel1.Controls.Add(Panel11)
        ModernPanel1.Controls.Add(Panel10)
        ModernPanel1.Controls.Add(Panel5)
        ModernPanel1.Controls.Add(Panel4)
        ModernPanel1.Controls.Add(Panel1)
        ModernPanel1.Controls.Add(HtmlColorLabel7)
        ModernPanel1.Controls.Add(Panel9)
        ModernPanel1.Controls.Add(Panel7)
        ModernPanel1.Controls.Add(Panel8)
        ModernPanel1.Controls.Add(HtmlColorLabel11)
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
        Panel6.Location = New Point(20, 513)
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
        MCB_动画帧率.DropDownBackdropBlurPasses = 2
        MCB_动画帧率.DropDownBackdropBlurRadius = 30
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
        HtmlColorLabel3.Location = New Point(20, 468)
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
        Panel3.Controls.Add(MCB_超容器背景映射条目预算)
        Panel3.Dock = DockStyle.Top
        Panel3.Location = New Point(20, 426)
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
        HtmlColorLabel9.Text = "<span style=""color:Silver"">超容器背景映射   显存总量和单源条目</span>   预算越少计算越多"
        HtmlColorLabel9.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.MiddleLeft
        '
        ' MCB_超容器背景映射条目预算
        '
        MCB_超容器背景映射条目预算.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_超容器背景映射条目预算.BorderRadius = 10
        MCB_超容器背景映射条目预算.BorderSize = 0
        MCB_超容器背景映射条目预算.Dock = DockStyle.Left
        MCB_超容器背景映射条目预算.DropDownBackdropBlurPasses = 2
        MCB_超容器背景映射条目预算.DropDownBackdropBlurRadius = 30
        MCB_超容器背景映射条目预算.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_超容器背景映射条目预算.DropDownHoverAnimationDuration = 0
        MCB_超容器背景映射条目预算.DropDownHoverColor = Color.FromArgb(CByte(80), CByte(0), CByte(0), CByte(0))
        MCB_超容器背景映射条目预算.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_超容器背景映射条目预算.DropDownPadding = New Padding(10)
        MCB_超容器背景映射条目预算.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_超容器背景映射条目预算.DropDownSelectedForeColor = Color.White
        MCB_超容器背景映射条目预算.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_超容器背景映射条目预算.Items.Add("全局 32M + 单源 12")
        MCB_超容器背景映射条目预算.Items.Add("全局 32M + 单源 24")
        MCB_超容器背景映射条目预算.Items.Add("全局 32M + 单源 48")
        MCB_超容器背景映射条目预算.Items.Add("全局 64M + 单源 32")
        MCB_超容器背景映射条目预算.Items.Add("全局 64M + 单源 64")
        MCB_超容器背景映射条目预算.Items.Add("全局 64M + 单源 128")
        MCB_超容器背景映射条目预算.Items.Add("全局 128M + 单源 32")
        MCB_超容器背景映射条目预算.Items.Add("全局 128M + 单源 64")
        MCB_超容器背景映射条目预算.Items.Add("全局 128M + 单源 128")
        MCB_超容器背景映射条目预算.Location = New Point(0, 10)
        MCB_超容器背景映射条目预算.Margin = New Padding(2, 2, 2, 2)
        MCB_超容器背景映射条目预算.Name = "MCB_超容器背景映射条目预算"
        MCB_超容器背景映射条目预算.Padding = New Padding(10, 0, 10, 0)
        MCB_超容器背景映射条目预算.Size = New Size(200, 32)
        MCB_超容器背景映射条目预算.TabIndex = 0
        MCB_超容器背景映射条目预算.ToolTipGap = -1
        MCB_超容器背景映射条目预算.ToolTipMaxWidth = 350
        MCB_超容器背景映射条目预算.ToolTipPadding = New Padding(15)
        MCB_超容器背景映射条目预算.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        '
        ' Panel11
        '
        Panel11.Controls.Add(HtmlColorLabel10)
        Panel11.Controls.Add(MCB_超容器背景映射脏区策略极限)
        Panel11.Dock = DockStyle.Top
        Panel11.Location = New Point(20, 384)
        Panel11.Name = "Panel11"
        Panel11.Padding = New Padding(0, 10, 0, 0)
        Panel11.Size = New Size(702, 42)
        Panel11.TabIndex = 39
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
        HtmlColorLabel10.Text = "<span style=""color:Silver"">超容器背景映射   脏区策略极限</span>   阈值越大越浪费计算性能"
        HtmlColorLabel10.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.MiddleLeft
        '
        ' MCB_超容器背景映射脏区策略极限
        '
        MCB_超容器背景映射脏区策略极限.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_超容器背景映射脏区策略极限.BorderRadius = 10
        MCB_超容器背景映射脏区策略极限.BorderSize = 0
        MCB_超容器背景映射脏区策略极限.Dock = DockStyle.Left
        MCB_超容器背景映射脏区策略极限.DropDownBackdropBlurPasses = 2
        MCB_超容器背景映射脏区策略极限.DropDownBackdropBlurRadius = 30
        MCB_超容器背景映射脏区策略极限.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_超容器背景映射脏区策略极限.DropDownHoverAnimationDuration = 0
        MCB_超容器背景映射脏区策略极限.DropDownHoverColor = Color.FromArgb(CByte(80), CByte(0), CByte(0), CByte(0))
        MCB_超容器背景映射脏区策略极限.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_超容器背景映射脏区策略极限.DropDownPadding = New Padding(10)
        MCB_超容器背景映射脏区策略极限.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_超容器背景映射脏区策略极限.DropDownSelectedForeColor = Color.White
        MCB_超容器背景映射脏区策略极限.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_超容器背景映射脏区策略极限.Items.Add("4 区块 + 40% 阈值")
        MCB_超容器背景映射脏区策略极限.Items.Add("4 区块 + 60% 阈值")
        MCB_超容器背景映射脏区策略极限.Items.Add("4 区块 + 80% 阈值")
        MCB_超容器背景映射脏区策略极限.Items.Add("8 区块 + 40% 阈值")
        MCB_超容器背景映射脏区策略极限.Items.Add("8 区块 + 60% 阈值")
        MCB_超容器背景映射脏区策略极限.Items.Add("8 区块 + 80% 阈值")
        MCB_超容器背景映射脏区策略极限.Items.Add("16 区块 + 40% 阈值")
        MCB_超容器背景映射脏区策略极限.Items.Add("16 区块 + 60% 阈值")
        MCB_超容器背景映射脏区策略极限.Items.Add("16 区块 + 80% 阈值")
        MCB_超容器背景映射脏区策略极限.Location = New Point(0, 10)
        MCB_超容器背景映射脏区策略极限.Margin = New Padding(2, 2, 2, 2)
        MCB_超容器背景映射脏区策略极限.Name = "MCB_超容器背景映射脏区策略极限"
        MCB_超容器背景映射脏区策略极限.Padding = New Padding(10, 0, 10, 0)
        MCB_超容器背景映射脏区策略极限.Size = New Size(200, 32)
        MCB_超容器背景映射脏区策略极限.TabIndex = 0
        MCB_超容器背景映射脏区策略极限.ToolTipGap = -1
        MCB_超容器背景映射脏区策略极限.ToolTipMaxWidth = 350
        MCB_超容器背景映射脏区策略极限.ToolTipPadding = New Padding(15)
        MCB_超容器背景映射脏区策略极限.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        '
        ' Panel10
        '
        Panel10.Controls.Add(HtmlColorLabel6)
        Panel10.Controls.Add(MCB_超容器背景映射源位图缓存)
        Panel10.Dock = DockStyle.Top
        Panel10.Location = New Point(20, 342)
        Panel10.Name = "Panel10"
        Panel10.Padding = New Padding(0, 10, 0, 0)
        Panel10.Size = New Size(702, 42)
        Panel10.TabIndex = 38
        '
        ' HtmlColorLabel6
        '
        HtmlColorLabel6.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel6.Dock = DockStyle.Fill
        HtmlColorLabel6.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HtmlColorLabel6.Location = New Point(200, 10)
        HtmlColorLabel6.Margin = New Padding(2)
        HtmlColorLabel6.Name = "HtmlColorLabel6"
        HtmlColorLabel6.Padding = New Padding(10, 0, 0, 0)
        HtmlColorLabel6.Size = New Size(502, 32)
        HtmlColorLabel6.TabIndex = 17
        HtmlColorLabel6.Text = "<span style=""color:Silver"">超容器背景映射   源位图缓存</span>   直接影响 RAM 占用"
        HtmlColorLabel6.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.MiddleLeft
        '
        ' MCB_超容器背景映射源位图缓存
        '
        MCB_超容器背景映射源位图缓存.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_超容器背景映射源位图缓存.BorderRadius = 10
        MCB_超容器背景映射源位图缓存.BorderSize = 0
        MCB_超容器背景映射源位图缓存.Dock = DockStyle.Left
        MCB_超容器背景映射源位图缓存.DropDownBackdropBlurPasses = 2
        MCB_超容器背景映射源位图缓存.DropDownBackdropBlurRadius = 30
        MCB_超容器背景映射源位图缓存.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_超容器背景映射源位图缓存.DropDownHoverAnimationDuration = 0
        MCB_超容器背景映射源位图缓存.DropDownHoverColor = Color.FromArgb(CByte(80), CByte(0), CByte(0), CByte(0))
        MCB_超容器背景映射源位图缓存.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_超容器背景映射源位图缓存.DropDownPadding = New Padding(10)
        MCB_超容器背景映射源位图缓存.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_超容器背景映射源位图缓存.DropDownSelectedForeColor = Color.White
        MCB_超容器背景映射源位图缓存.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_超容器背景映射源位图缓存.Items.Add("16M")
        MCB_超容器背景映射源位图缓存.Items.Add("32M")
        MCB_超容器背景映射源位图缓存.Items.Add("64M")
        MCB_超容器背景映射源位图缓存.Items.Add("128M")
        MCB_超容器背景映射源位图缓存.Items.Add("256M")
        MCB_超容器背景映射源位图缓存.Location = New Point(0, 10)
        MCB_超容器背景映射源位图缓存.Margin = New Padding(2, 2, 2, 2)
        MCB_超容器背景映射源位图缓存.Name = "MCB_超容器背景映射源位图缓存"
        MCB_超容器背景映射源位图缓存.Padding = New Padding(10, 0, 10, 0)
        MCB_超容器背景映射源位图缓存.Size = New Size(200, 32)
        MCB_超容器背景映射源位图缓存.TabIndex = 0
        MCB_超容器背景映射源位图缓存.ToolTipGap = -1
        MCB_超容器背景映射源位图缓存.ToolTipMaxWidth = 350
        MCB_超容器背景映射源位图缓存.ToolTipPadding = New Padding(15)
        MCB_超容器背景映射源位图缓存.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        '
        ' Panel5
        '
        Panel5.Controls.Add(HtmlColorLabel4)
        Panel5.Controls.Add(MCB_DW字体相关预算)
        Panel5.Dock = DockStyle.Top
        Panel5.Location = New Point(20, 300)
        Panel5.Name = "Panel5"
        Panel5.Padding = New Padding(0, 10, 0, 0)
        Panel5.Size = New Size(702, 42)
        Panel5.TabIndex = 37
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
        HtmlColorLabel4.TabIndex = 17
        HtmlColorLabel4.Text = "<span style=""color:Silver"">DWrite 字体相关预算</span>   "
        HtmlColorLabel4.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.MiddleLeft
        '
        ' MCB_DW字体相关预算
        '
        MCB_DW字体相关预算.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_DW字体相关预算.BorderRadius = 10
        MCB_DW字体相关预算.BorderSize = 0
        MCB_DW字体相关预算.Dock = DockStyle.Left
        MCB_DW字体相关预算.DropDownBackdropBlurPasses = 2
        MCB_DW字体相关预算.DropDownBackdropBlurRadius = 30
        MCB_DW字体相关预算.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_DW字体相关预算.DropDownHoverAnimationDuration = 0
        MCB_DW字体相关预算.DropDownHoverColor = Color.FromArgb(CByte(80), CByte(0), CByte(0), CByte(0))
        MCB_DW字体相关预算.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_DW字体相关预算.DropDownPadding = New Padding(10)
        MCB_DW字体相关预算.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_DW字体相关预算.DropDownSelectedForeColor = Color.White
        MCB_DW字体相关预算.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_DW字体相关预算.Items.Add("32 对象 + 32 解析")
        MCB_DW字体相关预算.Items.Add("64 对象 + 64 解析")
        MCB_DW字体相关预算.Items.Add("128 对象 + 128 解析")
        MCB_DW字体相关预算.Items.Add("256 对象 + 256 解析")
        MCB_DW字体相关预算.Items.Add("512 对象 + 512 解析")
        MCB_DW字体相关预算.Location = New Point(0, 10)
        MCB_DW字体相关预算.Margin = New Padding(2, 2, 2, 2)
        MCB_DW字体相关预算.Name = "MCB_DW字体相关预算"
        MCB_DW字体相关预算.Padding = New Padding(10, 0, 10, 0)
        MCB_DW字体相关预算.Size = New Size(200, 32)
        MCB_DW字体相关预算.TabIndex = 0
        MCB_DW字体相关预算.ToolTipGap = -1
        MCB_DW字体相关预算.ToolTipMaxWidth = 350
        MCB_DW字体相关预算.ToolTipPadding = New Padding(15)
        MCB_DW字体相关预算.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        '
        ' Panel4
        '
        Panel4.Controls.Add(HtmlColorLabel2)
        Panel4.Controls.Add(MCB_D2D每对象画刷缓存数量)
        Panel4.Dock = DockStyle.Top
        Panel4.Location = New Point(20, 258)
        Panel4.Name = "Panel4"
        Panel4.Padding = New Padding(0, 10, 0, 0)
        Panel4.Size = New Size(702, 42)
        Panel4.TabIndex = 36
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
        HtmlColorLabel2.Text = "<span style=""color:Silver"">D2D 每对象画刷缓存数量</span>   "
        HtmlColorLabel2.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.MiddleLeft
        '
        ' MCB_D2D每对象画刷缓存数量
        '
        MCB_D2D每对象画刷缓存数量.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_D2D每对象画刷缓存数量.BorderRadius = 10
        MCB_D2D每对象画刷缓存数量.BorderSize = 0
        MCB_D2D每对象画刷缓存数量.Dock = DockStyle.Left
        MCB_D2D每对象画刷缓存数量.DropDownBackdropBlurPasses = 2
        MCB_D2D每对象画刷缓存数量.DropDownBackdropBlurRadius = 30
        MCB_D2D每对象画刷缓存数量.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_D2D每对象画刷缓存数量.DropDownHoverAnimationDuration = 0
        MCB_D2D每对象画刷缓存数量.DropDownHoverColor = Color.FromArgb(CByte(80), CByte(0), CByte(0), CByte(0))
        MCB_D2D每对象画刷缓存数量.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_D2D每对象画刷缓存数量.DropDownPadding = New Padding(10)
        MCB_D2D每对象画刷缓存数量.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_D2D每对象画刷缓存数量.DropDownSelectedForeColor = Color.White
        MCB_D2D每对象画刷缓存数量.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_D2D每对象画刷缓存数量.Items.Add("8")
        MCB_D2D每对象画刷缓存数量.Items.Add("16")
        MCB_D2D每对象画刷缓存数量.Items.Add("32")
        MCB_D2D每对象画刷缓存数量.Items.Add("64")
        MCB_D2D每对象画刷缓存数量.Items.Add("128")
        MCB_D2D每对象画刷缓存数量.Items.Add("256")
        MCB_D2D每对象画刷缓存数量.Items.Add("512")
        MCB_D2D每对象画刷缓存数量.Location = New Point(0, 10)
        MCB_D2D每对象画刷缓存数量.Margin = New Padding(2, 2, 2, 2)
        MCB_D2D每对象画刷缓存数量.Name = "MCB_D2D每对象画刷缓存数量"
        MCB_D2D每对象画刷缓存数量.Padding = New Padding(10, 0, 10, 0)
        MCB_D2D每对象画刷缓存数量.Size = New Size(200, 32)
        MCB_D2D每对象画刷缓存数量.TabIndex = 0
        MCB_D2D每对象画刷缓存数量.ToolTipGap = -1
        MCB_D2D每对象画刷缓存数量.ToolTipMaxWidth = 350
        MCB_D2D每对象画刷缓存数量.ToolTipPadding = New Padding(15)
        MCB_D2D每对象画刷缓存数量.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        '
        ' Panel1
        '
        Panel1.Controls.Add(HtmlColorLabel8)
        Panel1.Controls.Add(MCB_D2DImage缓存预算)
        Panel1.Dock = DockStyle.Top
        Panel1.Location = New Point(20, 216)
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
        HtmlColorLabel8.Text = "<span style=""color:Silver"">D2D Image 缓存预算</span>   预算越多，显存越多，计算越少"
        HtmlColorLabel8.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.MiddleLeft
        '
        ' MCB_D2DImage缓存预算
        '
        MCB_D2DImage缓存预算.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_D2DImage缓存预算.BorderRadius = 10
        MCB_D2DImage缓存预算.BorderSize = 0
        MCB_D2DImage缓存预算.Dock = DockStyle.Left
        MCB_D2DImage缓存预算.DropDownBackdropBlurPasses = 2
        MCB_D2DImage缓存预算.DropDownBackdropBlurRadius = 30
        MCB_D2DImage缓存预算.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_D2DImage缓存预算.DropDownHoverAnimationDuration = 0
        MCB_D2DImage缓存预算.DropDownHoverColor = Color.FromArgb(CByte(80), CByte(0), CByte(0), CByte(0))
        MCB_D2DImage缓存预算.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_D2DImage缓存预算.DropDownPadding = New Padding(10)
        MCB_D2DImage缓存预算.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_D2DImage缓存预算.DropDownSelectedForeColor = Color.White
        MCB_D2DImage缓存预算.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_D2DImage缓存预算.Items.Add("单图 16M + 32 索引")
        MCB_D2DImage缓存预算.Items.Add("单图 32M + 64 索引")
        MCB_D2DImage缓存预算.Items.Add("单图 64M + 128 索引")
        MCB_D2DImage缓存预算.Items.Add("单图 64M + 256 索引")
        MCB_D2DImage缓存预算.Items.Add("单图 128M + 256 索引")
        MCB_D2DImage缓存预算.Items.Add("单图 256M + 256 索引")
        MCB_D2DImage缓存预算.Items.Add("单图 512M + 512 索引")
        MCB_D2DImage缓存预算.Location = New Point(0, 10)
        MCB_D2DImage缓存预算.Margin = New Padding(2, 2, 2, 2)
        MCB_D2DImage缓存预算.Name = "MCB_D2DImage缓存预算"
        MCB_D2DImage缓存预算.Padding = New Padding(10, 0, 10, 0)
        MCB_D2DImage缓存预算.Size = New Size(200, 32)
        MCB_D2DImage缓存预算.TabIndex = 0
        MCB_D2DImage缓存预算.ToolTipGap = -1
        MCB_D2DImage缓存预算.ToolTipMaxWidth = 350
        MCB_D2DImage缓存预算.ToolTipPadding = New Padding(15)
        MCB_D2DImage缓存预算.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        '
        ' HtmlColorLabel7
        '
        HtmlColorLabel7.AutoSize = True
        HtmlColorLabel7.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel7.Dock = DockStyle.Top
        HtmlColorLabel7.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HtmlColorLabel7.Location = New Point(20, 171)
        HtmlColorLabel7.Margin = New Padding(2)
        HtmlColorLabel7.Name = "HtmlColorLabel7"
        HtmlColorLabel7.Padding = New Padding(0, 20, 0, 0)
        HtmlColorLabel7.Size = New Size(702, 45)
        HtmlColorLabel7.TabIndex = 29
        HtmlColorLabel7.Text = "<span style=""font-size:13; color:Silver"">DirectX 开销平衡</span>   在计算量、内存、显存之间互相平衡"
        '
        ' Panel9
        '
        Panel9.Controls.Add(HtmlColorLabel14)
        Panel9.Controls.Add(MCB_SSAA)
        Panel9.Dock = DockStyle.Top
        Panel9.Location = New Point(20, 129)
        Panel9.Name = "Panel9"
        Panel9.Padding = New Padding(0, 10, 0, 0)
        Panel9.Size = New Size(702, 42)
        Panel9.TabIndex = 34
        '
        ' HtmlColorLabel14
        '
        HtmlColorLabel14.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel14.Dock = DockStyle.Fill
        HtmlColorLabel14.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HtmlColorLabel14.Location = New Point(200, 10)
        HtmlColorLabel14.Margin = New Padding(2)
        HtmlColorLabel14.Name = "HtmlColorLabel14"
        HtmlColorLabel14.Padding = New Padding(10, 0, 0, 0)
        HtmlColorLabel14.Size = New Size(502, 32)
        HtmlColorLabel14.TabIndex = 18
        HtmlColorLabel14.Text = "<span style=""color:Silver"">SSAA 超采样抗锯齿</span>   性能消耗指数级增加，通常没必要使用"
        HtmlColorLabel14.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.MiddleLeft
        '
        ' MCB_SSAA
        '
        MCB_SSAA.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_SSAA.BorderRadius = 10
        MCB_SSAA.BorderSize = 0
        MCB_SSAA.Dock = DockStyle.Left
        MCB_SSAA.DropDownBackdropBlurPasses = 2
        MCB_SSAA.DropDownBackdropBlurRadius = 30
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
        ToolTipEntry3.ItemText = "矢量几何 (试验选项)"
        ToolTipEntry3.ToolTipText = "矢量几何 Outline 是仿制 MacType 的超高质量绘制模式，彻底避开 Windows 的 GASP表 和 TrueType hinting 字节码，让不想安装三方软件的用户也能体验到 Web 甚至 macOS 的文字效果，其效果由开发者定制，如有不适请及时切换到其他模式"
        MCB_SSAA.ItemToolTips.AddRange(New LakeUI.ModernComboBox.ToolTipEntry() {ToolTipEntry3})
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
        ' Panel7
        '
        Panel7.Controls.Add(HtmlColorLabel12)
        Panel7.Controls.Add(MCB_文字渲染模式)
        Panel7.Dock = DockStyle.Top
        Panel7.Location = New Point(20, 87)
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
        MCB_文字渲染模式.DropDownBackdropBlurPasses = 2
        MCB_文字渲染模式.DropDownBackdropBlurRadius = 30
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
        ToolTipEntry4.ItemText = "矢量几何 (试验选项)"
        ToolTipEntry4.ToolTipText = "矢量几何 Outline 是仿制 MacType 的超高质量绘制模式，彻底避开 Windows 的 GASP表 和 TrueType hinting 字节码，让不想安装三方软件的用户也能体验到 Web 甚至 macOS 的文字效果，其效果由开发者定制，如有不适请及时切换到其他模式"
        MCB_文字渲染模式.ItemToolTips.AddRange(New LakeUI.ModernComboBox.ToolTipEntry() {ToolTipEntry4})
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
        Panel8.Location = New Point(20, 45)
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
        HtmlColorLabel13.Text = "<span style=""color:Silver"">GPU 抗锯齿</span>   建议开启，或者你可能喜欢曲线锯齿"
        HtmlColorLabel13.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.MiddleLeft
        '
        ' MCB_GPU抗锯齿
        '
        MCB_GPU抗锯齿.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_GPU抗锯齿.BorderRadius = 10
        MCB_GPU抗锯齿.BorderSize = 0
        MCB_GPU抗锯齿.Dock = DockStyle.Left
        MCB_GPU抗锯齿.DropDownBackdropBlurPasses = 2
        MCB_GPU抗锯齿.DropDownBackdropBlurRadius = 30
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
        HtmlColorLabel11.Location = New Point(20, 20)
        HtmlColorLabel11.Margin = New Padding(2)
        HtmlColorLabel11.Name = "HtmlColorLabel11"
        HtmlColorLabel11.Size = New Size(702, 25)
        HtmlColorLabel11.TabIndex = 25
        HtmlColorLabel11.Text = "<span style=""font-size:13; color:Silver"">LakeUI DirectX 图形质量</span>   由 Vortice 提供支持"
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
        Panel11.ResumeLayout(False)
        Panel10.ResumeLayout(False)
        Panel5.ResumeLayout(False)
        Panel4.ResumeLayout(False)
        Panel1.ResumeLayout(False)
        Panel9.ResumeLayout(False)
        Panel7.ResumeLayout(False)
        Panel8.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents ModernPanel1 As LakeUI.ModernPanel
    Friend WithEvents HtmlColorLabel3 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel6 As Panel
    Friend WithEvents HtmlColorLabel5 As LakeUI.HtmlColorLabel
    Friend WithEvents MCB_动画帧率 As LakeUI.ModernComboBox
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
    Friend WithEvents MCB_超容器背景映射条目预算 As LakeUI.ModernComboBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents HtmlColorLabel8 As LakeUI.HtmlColorLabel
    Friend WithEvents MCB_D2DImage缓存预算 As LakeUI.ModernComboBox
    Friend WithEvents Panel9 As Panel
    Friend WithEvents HtmlColorLabel14 As LakeUI.HtmlColorLabel
    Friend WithEvents MCB_SSAA As LakeUI.ModernComboBox
    Friend WithEvents Panel11 As Panel
    Friend WithEvents HtmlColorLabel10 As LakeUI.HtmlColorLabel
    Friend WithEvents MCB_超容器背景映射脏区策略极限 As LakeUI.ModernComboBox
    Friend WithEvents Panel10 As Panel
    Friend WithEvents HtmlColorLabel6 As LakeUI.HtmlColorLabel
    Friend WithEvents MCB_超容器背景映射源位图缓存 As LakeUI.ModernComboBox
    Friend WithEvents Panel5 As Panel
    Friend WithEvents HtmlColorLabel4 As LakeUI.HtmlColorLabel
    Friend WithEvents MCB_DW字体相关预算 As LakeUI.ModernComboBox
    Friend WithEvents Panel4 As Panel
    Friend WithEvents HtmlColorLabel2 As LakeUI.HtmlColorLabel
    Friend WithEvents MCB_D2D每对象画刷缓存数量 As LakeUI.ModernComboBox
End Class
