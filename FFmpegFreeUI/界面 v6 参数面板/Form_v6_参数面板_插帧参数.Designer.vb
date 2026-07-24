<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_v6_参数面板_插帧参数
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
        Panel6 = New Panel()
        MTB_场景变化检测强度 = New LakeUI.ModernTextBox()
        JustEmptyControl2 = New LakeUI.JustEmptyControl()
        MTB_搜索范围 = New LakeUI.ModernTextBox()
        JustEmptyControl4 = New LakeUI.JustEmptyControl()
        MTB_块大小 = New LakeUI.ModernTextBox()
        Panel7 = New Panel()
        HCL_场景变化检测强度 = New LakeUI.HtmlColorLabel()
        HCL_搜索范围 = New LakeUI.HtmlColorLabel()
        HCL_块大小 = New LakeUI.HtmlColorLabel()
        HCL_识别设置 = New LakeUI.HtmlColorLabel()
        Panel3 = New Panel()
        MCK_可变块大小的运动补偿 = New LakeUI.ModernCheckBox()
        MCB_运动补偿模式 = New LakeUI.ModernComboBox()
        Panel5 = New Panel()
        HCL_运动补偿模式 = New LakeUI.HtmlColorLabel()
        Panel1 = New Panel()
        MCB_运动估计算法 = New LakeUI.ModernComboBox()
        JustEmptyControl1 = New LakeUI.JustEmptyControl()
        MCB_运动估计模式 = New LakeUI.ModernComboBox()
        JustEmptyControl3 = New LakeUI.JustEmptyControl()
        MCB_插帧模式 = New LakeUI.ModernComboBox()
        Panel4 = New Panel()
        HCL_运动估计算法 = New LakeUI.HtmlColorLabel()
        HCL_运动估计模式 = New LakeUI.HtmlColorLabel()
        HCL_插帧模式 = New LakeUI.HtmlColorLabel()
        HCL_算法设置 = New LakeUI.HtmlColorLabel()
        Panel2 = New Panel()
        MTB_目标帧率 = New LakeUI.ModernTextBox()
        HCL_目标帧率 = New LakeUI.HtmlColorLabel()
        MCK_插帧总开关 = New LakeUI.ModernCheckBox()
        ModernPanel1.SuspendLayout()
        Panel6.SuspendLayout()
        Panel7.SuspendLayout()
        Panel3.SuspendLayout()
        Panel5.SuspendLayout()
        Panel1.SuspendLayout()
        Panel4.SuspendLayout()
        Panel2.SuspendLayout()
        SuspendLayout()
        '
        ' ModernPanel1
        '
        ModernPanel1.BackColor1 = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ModernPanel1.BorderSize = 0
        ModernPanel1.Controls.Add(Panel6)
        ModernPanel1.Controls.Add(Panel7)
        ModernPanel1.Controls.Add(HCL_识别设置)
        ModernPanel1.Controls.Add(Panel3)
        ModernPanel1.Controls.Add(Panel5)
        ModernPanel1.Controls.Add(Panel1)
        ModernPanel1.Controls.Add(Panel4)
        ModernPanel1.Controls.Add(HCL_算法设置)
        ModernPanel1.Controls.Add(Panel2)
        ModernPanel1.Controls.Add(HCL_目标帧率)
        ModernPanel1.Controls.Add(MCK_插帧总开关)
        ModernPanel1.Dock = DockStyle.Fill
        ModernPanel1.Location = New Point(0, 0)
        ModernPanel1.Name = "ModernPanel1"
        ModernPanel1.Padding = New Padding(20)
        ModernPanel1.ScrollBarMode = LakeUI.ModernPanel.ScrollMode.Vertical
        ModernPanel1.Size = New Size(634, 511)
        ModernPanel1.TabIndex = 0
        '
        ' Panel6
        '
        Panel6.Controls.Add(MTB_场景变化检测强度)
        Panel6.Controls.Add(JustEmptyControl2)
        Panel6.Controls.Add(MTB_搜索范围)
        Panel6.Controls.Add(JustEmptyControl4)
        Panel6.Controls.Add(MTB_块大小)
        Panel6.Dock = DockStyle.Top
        Panel6.Location = New Point(20, 395)
        Panel6.Name = "Panel6"
        Panel6.Padding = New Padding(0, 10, 0, 0)
        Panel6.Size = New Size(594, 42)
        Panel6.TabIndex = 33
        '
        ' MTB_场景变化检测强度
        '
        MTB_场景变化检测强度.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_场景变化检测强度.BorderColor = Color.Transparent
        MTB_场景变化检测强度.BorderColorFocus = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MTB_场景变化检测强度.BorderRadius = 10
        MTB_场景变化检测强度.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        MTB_场景变化检测强度.Dock = DockStyle.Left
        MTB_场景变化检测强度.Location = New Point(320, 10)
        MTB_场景变化检测强度.Margin = New Padding(2)
        MTB_场景变化检测强度.Name = "MTB_场景变化检测强度"
        MTB_场景变化检测强度.Padding = New Padding(10, 0, 10, 0)
        MTB_场景变化检测强度.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_场景变化检测强度.Size = New Size(150, 32)
        MTB_场景变化检测强度.TabIndex = 19
        MTB_场景变化检测强度.WaterText = "scd_threshold="
        MTB_场景变化检测强度.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        '
        ' JustEmptyControl2
        '
        JustEmptyControl2.Dock = DockStyle.Left
        JustEmptyControl2.Location = New Point(310, 10)
        JustEmptyControl2.Name = "JustEmptyControl2"
        JustEmptyControl2.Size = New Size(10, 32)
        JustEmptyControl2.TabIndex = 16
        '
        ' MTB_搜索范围
        '
        MTB_搜索范围.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_搜索范围.BorderColor = Color.Transparent
        MTB_搜索范围.BorderColorFocus = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MTB_搜索范围.BorderRadius = 10
        MTB_搜索范围.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        MTB_搜索范围.Dock = DockStyle.Left
        MTB_搜索范围.Location = New Point(160, 10)
        MTB_搜索范围.Margin = New Padding(2)
        MTB_搜索范围.Name = "MTB_搜索范围"
        MTB_搜索范围.Padding = New Padding(10, 0, 10, 0)
        MTB_搜索范围.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_搜索范围.Size = New Size(150, 32)
        MTB_搜索范围.TabIndex = 17
        MTB_搜索范围.WaterText = "search_param="
        MTB_搜索范围.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        '
        ' JustEmptyControl4
        '
        JustEmptyControl4.Dock = DockStyle.Left
        JustEmptyControl4.Location = New Point(150, 10)
        JustEmptyControl4.Name = "JustEmptyControl4"
        JustEmptyControl4.Size = New Size(10, 32)
        JustEmptyControl4.TabIndex = 14
        '
        ' MTB_块大小
        '
        MTB_块大小.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_块大小.BorderColor = Color.Transparent
        MTB_块大小.BorderColorFocus = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MTB_块大小.BorderRadius = 10
        MTB_块大小.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        MTB_块大小.Dock = DockStyle.Left
        MTB_块大小.Location = New Point(0, 10)
        MTB_块大小.Margin = New Padding(2)
        MTB_块大小.Name = "MTB_块大小"
        MTB_块大小.Padding = New Padding(10, 0, 10, 0)
        MTB_块大小.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_块大小.Size = New Size(150, 32)
        MTB_块大小.TabIndex = 15
        MTB_块大小.WaterText = "mb_size="
        MTB_块大小.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        '
        ' Panel7
        '
        Panel7.Controls.Add(HCL_场景变化检测强度)
        Panel7.Controls.Add(HCL_搜索范围)
        Panel7.Controls.Add(HCL_块大小)
        Panel7.Dock = DockStyle.Top
        Panel7.Location = New Point(20, 365)
        Panel7.Name = "Panel7"
        Panel7.Size = New Size(594, 30)
        Panel7.TabIndex = 32
        '
        ' HCL_场景变化检测强度
        '
        HCL_场景变化检测强度.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_场景变化检测强度.Dock = DockStyle.Fill
        HCL_场景变化检测强度.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_场景变化检测强度.Location = New Point(320, 0)
        HCL_场景变化检测强度.Margin = New Padding(2)
        HCL_场景变化检测强度.Name = "HCL_场景变化检测强度"
        HCL_场景变化检测强度.Size = New Size(274, 30)
        HCL_场景变化检测强度.TabIndex = 2
        HCL_场景变化检测强度.Text = "场景变化检测强度 scd=fdiff 默认 10"
        HCL_场景变化检测强度.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.BottomLeft
        '
        ' HCL_搜索范围
        '
        HCL_搜索范围.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_搜索范围.Dock = DockStyle.Left
        HCL_搜索范围.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_搜索范围.Location = New Point(160, 0)
        HCL_搜索范围.Margin = New Padding(2)
        HCL_搜索范围.Name = "HCL_搜索范围"
        HCL_搜索范围.Size = New Size(160, 30)
        HCL_搜索范围.TabIndex = 1
        HCL_搜索范围.Text = "搜索范围 (默认 32)"
        HCL_搜索范围.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.BottomLeft
        '
        ' HCL_块大小
        '
        HCL_块大小.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_块大小.Dock = DockStyle.Left
        HCL_块大小.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_块大小.Location = New Point(0, 0)
        HCL_块大小.Margin = New Padding(2)
        HCL_块大小.Name = "HCL_块大小"
        HCL_块大小.Size = New Size(160, 30)
        HCL_块大小.TabIndex = 0
        HCL_块大小.Text = "块大小 (默认 16)"
        HCL_块大小.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.BottomLeft
        '
        ' HCL_识别设置
        '
        HCL_识别设置.AutoSize = True
        HCL_识别设置.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_识别设置.Dock = DockStyle.Top
        HCL_识别设置.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_识别设置.Location = New Point(20, 320)
        HCL_识别设置.Margin = New Padding(2)
        HCL_识别设置.Name = "HCL_识别设置"
        HCL_识别设置.Padding = New Padding(0, 20, 0, 0)
        HCL_识别设置.Size = New Size(594, 45)
        HCL_识别设置.TabIndex = 29
        HCL_识别设置.Text = "<span style=""font-size:13; color:Silver"">识别设置</span>   值越大计算需求越多越稳定"
        '
        ' Panel3
        '
        Panel3.Controls.Add(MCK_可变块大小的运动补偿)
        Panel3.Controls.Add(MCB_运动补偿模式)
        Panel3.Dock = DockStyle.Top
        Panel3.Location = New Point(20, 278)
        Panel3.Name = "Panel3"
        Panel3.Padding = New Padding(0, 10, 0, 0)
        Panel3.Size = New Size(594, 42)
        Panel3.TabIndex = 31
        '
        ' MCK_可变块大小的运动补偿
        '
        MCK_可变块大小的运动补偿.AutoSize = True
        MCK_可变块大小的运动补偿.BoxBorderSize = 0
        MCK_可变块大小的运动补偿.BoxCheckedBackColor = Color.OliveDrab
        MCK_可变块大小的运动补偿.BoxInnerPadding = 5
        MCK_可变块大小的运动补偿.BoxSize = 20
        MCK_可变块大小的运动补偿.BoxTextSpacing = 10
        MCK_可变块大小的运动补偿.BoxUncheckedBackColor = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCK_可变块大小的运动补偿.Dock = DockStyle.Fill
        MCK_可变块大小的运动补偿.Location = New Point(150, 10)
        MCK_可变块大小的运动补偿.Name = "MCK_可变块大小的运动补偿"
        MCK_可变块大小的运动补偿.Padding = New Padding(10, 0, 0, 0)
        MCK_可变块大小的运动补偿.Size = New Size(444, 32)
        MCK_可变块大小的运动补偿.TabIndex = 24
        MCK_可变块大小的运动补偿.Text = "可变块大小的运动补偿 vsbmc=1"
        '
        ' MCB_运动补偿模式
        '
        MCB_运动补偿模式.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_运动补偿模式.BorderRadius = 10
        MCB_运动补偿模式.BorderSize = 0
        MCB_运动补偿模式.Dock = DockStyle.Left
        MCB_运动补偿模式.DropDownBackdropBlurPasses = 2
        MCB_运动补偿模式.DropDownBackdropBlurRadius = 30
        MCB_运动补偿模式.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_运动补偿模式.DropDownHoverColor = Color.FromArgb(CByte(20), CByte(220), CByte(220), CByte(220))
        MCB_运动补偿模式.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_运动补偿模式.DropDownPadding = New Padding(10)
        MCB_运动补偿模式.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_运动补偿模式.DropDownSelectedForeColor = Color.White
        MCB_运动补偿模式.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_运动补偿模式.Items.Add("")
        MCB_运动补偿模式.Items.Add("重叠块运动补偿")
        MCB_运动补偿模式.Items.Add("加权 obmc")
        MCB_运动补偿模式.Location = New Point(0, 10)
        MCB_运动补偿模式.Margin = New Padding(2, 2, 2, 2)
        MCB_运动补偿模式.Name = "MCB_运动补偿模式"
        MCB_运动补偿模式.Padding = New Padding(10, 0, 10, 0)
        MCB_运动补偿模式.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_运动补偿模式.Size = New Size(150, 32)
        MCB_运动补偿模式.TabIndex = 15
        MCB_运动补偿模式.ToolTipGap = -1
        MCB_运动补偿模式.ToolTipMaxWidth = 350
        MCB_运动补偿模式.ToolTipPadding = New Padding(15)
        MCB_运动补偿模式.WaterText = "mc_mode="
        MCB_运动补偿模式.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        '
        ' Panel5
        '
        Panel5.Controls.Add(HCL_运动补偿模式)
        Panel5.Dock = DockStyle.Top
        Panel5.Location = New Point(20, 248)
        Panel5.Name = "Panel5"
        Panel5.Size = New Size(594, 30)
        Panel5.TabIndex = 30
        '
        ' HCL_运动补偿模式
        '
        HCL_运动补偿模式.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_运动补偿模式.Dock = DockStyle.Left
        HCL_运动补偿模式.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_运动补偿模式.Location = New Point(0, 0)
        HCL_运动补偿模式.Margin = New Padding(2)
        HCL_运动补偿模式.Name = "HCL_运动补偿模式"
        HCL_运动补偿模式.Size = New Size(160, 30)
        HCL_运动补偿模式.TabIndex = 0
        HCL_运动补偿模式.Text = "运动补偿模式"
        HCL_运动补偿模式.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.BottomLeft
        '
        ' Panel1
        '
        Panel1.Controls.Add(MCB_运动估计算法)
        Panel1.Controls.Add(JustEmptyControl1)
        Panel1.Controls.Add(MCB_运动估计模式)
        Panel1.Controls.Add(JustEmptyControl3)
        Panel1.Controls.Add(MCB_插帧模式)
        Panel1.Dock = DockStyle.Top
        Panel1.Location = New Point(20, 206)
        Panel1.Name = "Panel1"
        Panel1.Padding = New Padding(0, 10, 0, 0)
        Panel1.Size = New Size(594, 42)
        Panel1.TabIndex = 28
        '
        ' MCB_运动估计算法
        '
        MCB_运动估计算法.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_运动估计算法.BorderRadius = 10
        MCB_运动估计算法.BorderSize = 0
        MCB_运动估计算法.Dock = DockStyle.Left
        MCB_运动估计算法.DropDownBackdropBlurPasses = 2
        MCB_运动估计算法.DropDownBackdropBlurRadius = 30
        MCB_运动估计算法.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_运动估计算法.DropDownHoverColor = Color.FromArgb(CByte(20), CByte(220), CByte(220), CByte(220))
        MCB_运动估计算法.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_运动估计算法.DropDownPadding = New Padding(10)
        MCB_运动估计算法.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_运动估计算法.DropDownSelectedForeColor = Color.White
        MCB_运动估计算法.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_运动估计算法.Items.Add("")
        MCB_运动估计算法.Items.Add("穷举搜索")
        MCB_运动估计算法.Items.Add("三步搜索")
        MCB_运动估计算法.Items.Add("二维对数搜索")
        MCB_运动估计算法.Items.Add("新三步搜索")
        MCB_运动估计算法.Items.Add("四步搜索")
        MCB_运动估计算法.Items.Add("菱形搜索")
        MCB_运动估计算法.Items.Add("基于 Hexagon")
        MCB_运动估计算法.Items.Add("增强的预测区域")
        MCB_运动估计算法.Items.Add("不均匀多六边形")
        MCB_运动估计算法.Location = New Point(320, 10)
        MCB_运动估计算法.Margin = New Padding(2, 2, 2, 2)
        MCB_运动估计算法.Name = "MCB_运动估计算法"
        MCB_运动估计算法.Padding = New Padding(10, 0, 10, 0)
        MCB_运动估计算法.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_运动估计算法.Size = New Size(150, 32)
        MCB_运动估计算法.TabIndex = 18
        MCB_运动估计算法.ToolTipGap = -1
        MCB_运动估计算法.ToolTipMaxWidth = 350
        MCB_运动估计算法.ToolTipPadding = New Padding(15)
        MCB_运动估计算法.WaterText = "me="
        MCB_运动估计算法.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        '
        ' JustEmptyControl1
        '
        JustEmptyControl1.Dock = DockStyle.Left
        JustEmptyControl1.Location = New Point(310, 10)
        JustEmptyControl1.Name = "JustEmptyControl1"
        JustEmptyControl1.Size = New Size(10, 32)
        JustEmptyControl1.TabIndex = 17
        '
        ' MCB_运动估计模式
        '
        MCB_运动估计模式.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_运动估计模式.BorderRadius = 10
        MCB_运动估计模式.BorderSize = 0
        MCB_运动估计模式.Dock = DockStyle.Left
        MCB_运动估计模式.DropDownBackdropBlurPasses = 2
        MCB_运动估计模式.DropDownBackdropBlurRadius = 30
        MCB_运动估计模式.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_运动估计模式.DropDownHoverColor = Color.FromArgb(CByte(20), CByte(220), CByte(220), CByte(220))
        MCB_运动估计模式.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_运动估计模式.DropDownPadding = New Padding(10)
        MCB_运动估计模式.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_运动估计模式.DropDownSelectedForeColor = Color.White
        MCB_运动估计模式.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_运动估计模式.Items.Add("")
        MCB_运动估计模式.Items.Add("双向运动估计")
        MCB_运动估计模式.Items.Add("双侧运动估计")
        MCB_运动估计模式.Location = New Point(160, 10)
        MCB_运动估计模式.Margin = New Padding(2, 2, 2, 2)
        MCB_运动估计模式.Name = "MCB_运动估计模式"
        MCB_运动估计模式.Padding = New Padding(10, 0, 10, 0)
        MCB_运动估计模式.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_运动估计模式.Size = New Size(150, 32)
        MCB_运动估计模式.TabIndex = 16
        MCB_运动估计模式.ToolTipGap = -1
        MCB_运动估计模式.ToolTipMaxWidth = 350
        MCB_运动估计模式.ToolTipPadding = New Padding(15)
        MCB_运动估计模式.WaterText = "me_mode="
        MCB_运动估计模式.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        '
        ' JustEmptyControl3
        '
        JustEmptyControl3.Dock = DockStyle.Left
        JustEmptyControl3.Location = New Point(150, 10)
        JustEmptyControl3.Name = "JustEmptyControl3"
        JustEmptyControl3.Size = New Size(10, 32)
        JustEmptyControl3.TabIndex = 14
        '
        ' MCB_插帧模式
        '
        MCB_插帧模式.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_插帧模式.BorderRadius = 10
        MCB_插帧模式.BorderSize = 0
        MCB_插帧模式.Dock = DockStyle.Left
        MCB_插帧模式.DropDownBackdropBlurPasses = 2
        MCB_插帧模式.DropDownBackdropBlurRadius = 30
        MCB_插帧模式.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_插帧模式.DropDownHoverColor = Color.FromArgb(CByte(20), CByte(220), CByte(220), CByte(220))
        MCB_插帧模式.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_插帧模式.DropDownPadding = New Padding(10)
        MCB_插帧模式.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_插帧模式.DropDownSelectedForeColor = Color.White
        MCB_插帧模式.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_插帧模式.Items.Add("")
        MCB_插帧模式.Items.Add("两帧加权平均")
        MCB_插帧模式.Items.Add("运动补偿插值")
        MCB_插帧模式.Location = New Point(0, 10)
        MCB_插帧模式.Margin = New Padding(2, 2, 2, 2)
        MCB_插帧模式.Name = "MCB_插帧模式"
        MCB_插帧模式.Padding = New Padding(10, 0, 10, 0)
        MCB_插帧模式.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_插帧模式.Size = New Size(150, 32)
        MCB_插帧模式.TabIndex = 15
        MCB_插帧模式.ToolTipGap = -1
        MCB_插帧模式.ToolTipMaxWidth = 350
        MCB_插帧模式.ToolTipPadding = New Padding(15)
        MCB_插帧模式.WaterText = "mi_mode="
        MCB_插帧模式.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        '
        ' Panel4
        '
        Panel4.Controls.Add(HCL_运动估计算法)
        Panel4.Controls.Add(HCL_运动估计模式)
        Panel4.Controls.Add(HCL_插帧模式)
        Panel4.Dock = DockStyle.Top
        Panel4.Location = New Point(20, 176)
        Panel4.Name = "Panel4"
        Panel4.Size = New Size(594, 30)
        Panel4.TabIndex = 27
        '
        ' HCL_运动估计算法
        '
        HCL_运动估计算法.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_运动估计算法.Dock = DockStyle.Left
        HCL_运动估计算法.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_运动估计算法.Location = New Point(320, 0)
        HCL_运动估计算法.Margin = New Padding(2)
        HCL_运动估计算法.Name = "HCL_运动估计算法"
        HCL_运动估计算法.Size = New Size(160, 30)
        HCL_运动估计算法.TabIndex = 2
        HCL_运动估计算法.Text = "运动估计算法"
        HCL_运动估计算法.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.BottomLeft
        '
        ' HCL_运动估计模式
        '
        HCL_运动估计模式.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_运动估计模式.Dock = DockStyle.Left
        HCL_运动估计模式.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_运动估计模式.Location = New Point(160, 0)
        HCL_运动估计模式.Margin = New Padding(2)
        HCL_运动估计模式.Name = "HCL_运动估计模式"
        HCL_运动估计模式.Size = New Size(160, 30)
        HCL_运动估计模式.TabIndex = 1
        HCL_运动估计模式.Text = "运动估计模式"
        HCL_运动估计模式.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.BottomLeft
        '
        ' HCL_插帧模式
        '
        HCL_插帧模式.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_插帧模式.Dock = DockStyle.Left
        HCL_插帧模式.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_插帧模式.Location = New Point(0, 0)
        HCL_插帧模式.Margin = New Padding(2)
        HCL_插帧模式.Name = "HCL_插帧模式"
        HCL_插帧模式.Size = New Size(160, 30)
        HCL_插帧模式.TabIndex = 0
        HCL_插帧模式.Text = "插帧模式"
        HCL_插帧模式.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.BottomLeft
        '
        ' HCL_算法设置
        '
        HCL_算法设置.AutoSize = True
        HCL_算法设置.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_算法设置.Dock = DockStyle.Top
        HCL_算法设置.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_算法设置.Location = New Point(20, 131)
        HCL_算法设置.Margin = New Padding(2)
        HCL_算法设置.Name = "HCL_算法设置"
        HCL_算法设置.Padding = New Padding(0, 20, 0, 0)
        HCL_算法设置.Size = New Size(594, 45)
        HCL_算法设置.TabIndex = 26
        HCL_算法设置.Text = "<span style=""font-size:13; color:Silver"">算法设置</span>   推荐的最佳质量：运动补偿插值 + 加权 obmc"
        '
        ' Panel2
        '
        Panel2.Controls.Add(MTB_目标帧率)
        Panel2.Dock = DockStyle.Top
        Panel2.Location = New Point(20, 89)
        Panel2.Name = "Panel2"
        Panel2.Padding = New Padding(0, 10, 0, 0)
        Panel2.Size = New Size(594, 42)
        Panel2.TabIndex = 25
        '
        ' MTB_目标帧率
        '
        MTB_目标帧率.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_目标帧率.BorderColor = Color.Transparent
        MTB_目标帧率.BorderColorFocus = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MTB_目标帧率.BorderRadius = 10
        MTB_目标帧率.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        MTB_目标帧率.Dock = DockStyle.Left
        MTB_目标帧率.Location = New Point(0, 10)
        MTB_目标帧率.Margin = New Padding(2)
        MTB_目标帧率.Name = "MTB_目标帧率"
        MTB_目标帧率.Padding = New Padding(10, 0, 10, 0)
        MTB_目标帧率.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_目标帧率.Size = New Size(100, 32)
        MTB_目标帧率.TabIndex = 6
        MTB_目标帧率.WaterText = "fps="
        MTB_目标帧率.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
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
        HCL_目标帧率.Size = New Size(594, 45)
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
        MCK_插帧总开关.Size = New Size(594, 24)
        MCK_插帧总开关.TabIndex = 23
        MCK_插帧总开关.Text = "插帧总开关 / 勾选才会使用 / 建议考虑 AI 软件，如 SVP、TopazAI"
        '
        ' Form_v6_参数面板_插帧参数
        '
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ClientSize = New Size(634, 511)
        Controls.Add(ModernPanel1)
        Font = New Font("Microsoft YaHei UI", 10F)
        ForeColor = Color.Silver
        MaximizeBox = False
        MinimizeBox = False
        MinimumSize = New Size(650, 550)
        Name = "Form_v6_参数面板_插帧参数"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.Manual
        Text = "视频插帧"
        ModernPanel1.ResumeLayout(False)
        ModernPanel1.PerformLayout()
        Panel6.ResumeLayout(False)
        Panel7.ResumeLayout(False)
        Panel3.ResumeLayout(False)
        Panel3.PerformLayout()
        Panel5.ResumeLayout(False)
        Panel1.ResumeLayout(False)
        Panel4.ResumeLayout(False)
        Panel2.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents ModernPanel1 As LakeUI.ModernPanel
    Friend WithEvents MCK_插帧总开关 As LakeUI.ModernCheckBox
    Friend WithEvents Panel2 As Panel
    Friend WithEvents MTB_目标帧率 As LakeUI.ModernTextBox
    Friend WithEvents HCL_目标帧率 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents JustEmptyControl3 As LakeUI.JustEmptyControl
    Friend WithEvents Panel4 As Panel
    Friend WithEvents HCL_运动估计算法 As LakeUI.HtmlColorLabel
    Friend WithEvents HCL_运动估计模式 As LakeUI.HtmlColorLabel
    Friend WithEvents HCL_插帧模式 As LakeUI.HtmlColorLabel
    Friend WithEvents HCL_算法设置 As LakeUI.HtmlColorLabel
    Friend WithEvents MCB_插帧模式 As LakeUI.ModernComboBox
    Friend WithEvents MCB_运动估计算法 As LakeUI.ModernComboBox
    Friend WithEvents JustEmptyControl1 As LakeUI.JustEmptyControl
    Friend WithEvents MCB_运动估计模式 As LakeUI.ModernComboBox
    Friend WithEvents HCL_识别设置 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents MCB_运动补偿模式 As LakeUI.ModernComboBox
    Friend WithEvents Panel5 As Panel
    Friend WithEvents HCL_运动补偿模式 As LakeUI.HtmlColorLabel
    Friend WithEvents MCK_可变块大小的运动补偿 As LakeUI.ModernCheckBox
    Friend WithEvents Panel7 As Panel
    Friend WithEvents HCL_场景变化检测强度 As LakeUI.HtmlColorLabel
    Friend WithEvents HCL_搜索范围 As LakeUI.HtmlColorLabel
    Friend WithEvents HCL_块大小 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel6 As Panel
    Friend WithEvents MTB_场景变化检测强度 As LakeUI.ModernTextBox
    Friend WithEvents JustEmptyControl2 As LakeUI.JustEmptyControl
    Friend WithEvents MTB_搜索范围 As LakeUI.ModernTextBox
    Friend WithEvents JustEmptyControl4 As LakeUI.JustEmptyControl
    Friend WithEvents MTB_块大小 As LakeUI.ModernTextBox
End Class
