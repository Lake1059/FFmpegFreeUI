<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_v6_设置_功能设定
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
        Panel8 = New Panel()
        HtmlColorLabel8 = New LakeUI.HtmlColorLabel()
        MCB_任务失败删除文件 = New LakeUI.ModernComboBox()
        Panel10 = New Panel()
        HtmlColorLabel10 = New LakeUI.HtmlColorLabel()
        MCB_任务日志保留行数 = New LakeUI.ModernComboBox()
        Panel11 = New Panel()
        HtmlColorLabel11 = New LakeUI.HtmlColorLabel()
        MCB_任务日志性能计数器 = New LakeUI.ModernComboBox()
        Panel9 = New Panel()
        HtmlColorLabel9 = New LakeUI.HtmlColorLabel()
        MCB_编码队列显示最新日志行 = New LakeUI.ModernComboBox()
        Panel7 = New Panel()
        HtmlColorLabel7 = New LakeUI.HtmlColorLabel()
        MCB_独立参数面板自动切预设管理 = New LakeUI.ModernComboBox()
        Panel6 = New Panel()
        HtmlColorLabel6 = New LakeUI.HtmlColorLabel()
        MCB_任务名称混淆 = New LakeUI.ModernComboBox()
        Panel5 = New Panel()
        HtmlColorLabel5 = New LakeUI.HtmlColorLabel()
        MCB_是否自动重置参数面板到第一个页面 = New LakeUI.ModernComboBox()
        Panel4 = New Panel()
        HtmlColorLabel4 = New LakeUI.HtmlColorLabel()
        MCB_是否自动开始任务 = New LakeUI.ModernComboBox()
        Panel3 = New Panel()
        HtmlColorLabel3 = New LakeUI.HtmlColorLabel()
        MCB_是否启用提示音 = New LakeUI.ModernComboBox()
        Panel1 = New Panel()
        HtmlColorLabel2 = New LakeUI.HtmlColorLabel()
        MCB_有任务时系统状态 = New LakeUI.ModernComboBox()
        JustEmptyControl1 = New LakeUI.JustEmptyControl()
        Panel2 = New Panel()
        MTB_工作目录 = New LakeUI.ModernTextBox()
        JustEmptyControl2 = New LakeUI.JustEmptyControl()
        MB_浏览文件夹 = New LakeUI.ModernButton()
        HtmlColorLabel1 = New LakeUI.HtmlColorLabel()
        ModernPanel1.SuspendLayout()
        Panel8.SuspendLayout()
        Panel10.SuspendLayout()
        Panel11.SuspendLayout()
        Panel9.SuspendLayout()
        Panel7.SuspendLayout()
        Panel6.SuspendLayout()
        Panel5.SuspendLayout()
        Panel4.SuspendLayout()
        Panel3.SuspendLayout()
        Panel1.SuspendLayout()
        Panel2.SuspendLayout()
        SuspendLayout()
        ' 
        ' ModernPanel1
        ' 
        ModernPanel1.BackColor1 = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ModernPanel1.BorderSize = 0
        ModernPanel1.Controls.Add(Panel11)
        ModernPanel1.Controls.Add(Panel10)
        ModernPanel1.Controls.Add(Panel9)
        ModernPanel1.Controls.Add(Panel8)
        ModernPanel1.Controls.Add(Panel7)
        ModernPanel1.Controls.Add(Panel6)
        ModernPanel1.Controls.Add(Panel5)
        ModernPanel1.Controls.Add(Panel4)
        ModernPanel1.Controls.Add(Panel3)
        ModernPanel1.Controls.Add(Panel1)
        ModernPanel1.Controls.Add(JustEmptyControl1)
        ModernPanel1.Controls.Add(Panel2)
        ModernPanel1.Controls.Add(HtmlColorLabel1)
        ModernPanel1.Dock = DockStyle.Fill
        ModernPanel1.Location = New Point(0, 0)
        ModernPanel1.Name = "ModernPanel1"
        ModernPanel1.Padding = New Padding(20)
        ModernPanel1.ScrollBarMode = LakeUI.ModernPanel.ScrollMode.Vertical
        ModernPanel1.Size = New Size(742, 651)
        ModernPanel1.TabIndex = 2
        ' 
        ' Panel8
        ' 
        Panel8.Controls.Add(HtmlColorLabel8)
        Panel8.Controls.Add(MCB_任务失败删除文件)
        Panel8.Dock = DockStyle.Top
        Panel8.Location = New Point(20, 345)
        Panel8.Name = "Panel8"
        Panel8.Padding = New Padding(0, 10, 0, 0)
        Panel8.Size = New Size(702, 42)
        Panel8.TabIndex = 27
        ' 
        ' HtmlColorLabel8
        ' 
        HtmlColorLabel8.AutoSize = True
        HtmlColorLabel8.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel8.Dock = DockStyle.Fill
        HtmlColorLabel8.Location = New Point(200, 10)
        HtmlColorLabel8.Margin = New Padding(2)
        HtmlColorLabel8.Name = "HtmlColorLabel8"
        HtmlColorLabel8.Padding = New Padding(10, 0, 0, 0)
        HtmlColorLabel8.Size = New Size(502, 32)
        HtmlColorLabel8.TabIndex = 27
        HtmlColorLabel8.Text = "任务失败时是否删除文件"
        HtmlColorLabel8.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.MiddleLeft
        ' 
        ' MCB_任务失败删除文件
        ' 
        MCB_任务失败删除文件.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_任务失败删除文件.BorderRadius = 10
        MCB_任务失败删除文件.BorderSize = 0
        MCB_任务失败删除文件.Dock = DockStyle.Left
        MCB_任务失败删除文件.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_任务失败删除文件.DropDownHoverAnimationDuration = 0
        MCB_任务失败删除文件.DropDownHoverColor = Color.FromArgb(CByte(80), CByte(0), CByte(0), CByte(0))
        MCB_任务失败删除文件.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_任务失败删除文件.DropDownPadding = New Padding(10)
        MCB_任务失败删除文件.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_任务失败删除文件.DropDownSelectedForeColor = Color.White
        MCB_任务失败删除文件.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_任务失败删除文件.Items.Add("删除到回收站")
        MCB_任务失败删除文件.Items.Add("彻底删除")
        MCB_任务失败删除文件.Items.Add("什么都不做")
        MCB_任务失败删除文件.Location = New Point(0, 10)
        MCB_任务失败删除文件.Margin = New Padding(2, 2, 2, 2)
        MCB_任务失败删除文件.Name = "MCB_任务失败删除文件"
        MCB_任务失败删除文件.Padding = New Padding(10, 0, 10, 0)
        MCB_任务失败删除文件.Size = New Size(200, 32)
        MCB_任务失败删除文件.TabIndex = 0
        MCB_任务失败删除文件.ToolTipGap = -1
        MCB_任务失败删除文件.ToolTipMaxWidth = 350
        MCB_任务失败删除文件.ToolTipPadding = New Padding(15)
        MCB_任务失败删除文件.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' Panel10
        ' 
        Panel10.Controls.Add(HtmlColorLabel10)
        Panel10.Controls.Add(MCB_任务日志保留行数)
        Panel10.Dock = DockStyle.Top
        Panel10.Location = New Point(20, 429)
        Panel10.Name = "Panel10"
        Panel10.Padding = New Padding(0, 10, 0, 0)
        Panel10.Size = New Size(702, 42)
        Panel10.TabIndex = 31
        ' 
        ' HtmlColorLabel10
        ' 
        HtmlColorLabel10.AutoSize = True
        HtmlColorLabel10.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel10.Dock = DockStyle.Fill
        HtmlColorLabel10.Location = New Point(200, 10)
        HtmlColorLabel10.Margin = New Padding(2)
        HtmlColorLabel10.Name = "HtmlColorLabel10"
        HtmlColorLabel10.Padding = New Padding(10, 0, 0, 0)
        HtmlColorLabel10.Size = New Size(502, 32)
        HtmlColorLabel10.TabIndex = 31
        HtmlColorLabel10.Text = "编码队列任务日志保留行数"
        HtmlColorLabel10.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.MiddleLeft
        ' 
        ' MCB_任务日志保留行数
        ' 
        MCB_任务日志保留行数.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_任务日志保留行数.BorderRadius = 10
        MCB_任务日志保留行数.BorderSize = 0
        MCB_任务日志保留行数.Dock = DockStyle.Left
        MCB_任务日志保留行数.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_任务日志保留行数.DropDownHoverAnimationDuration = 0
        MCB_任务日志保留行数.DropDownHoverColor = Color.FromArgb(CByte(80), CByte(0), CByte(0), CByte(0))
        MCB_任务日志保留行数.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_任务日志保留行数.DropDownPadding = New Padding(10)
        MCB_任务日志保留行数.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_任务日志保留行数.DropDownSelectedForeColor = Color.White
        MCB_任务日志保留行数.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_任务日志保留行数.Items.Add("保留 100 行")
        MCB_任务日志保留行数.Items.Add("保留 500 行")
        MCB_任务日志保留行数.Items.Add("保留 1000 行")
        MCB_任务日志保留行数.Items.Add("不限制")
        MCB_任务日志保留行数.Location = New Point(0, 10)
        MCB_任务日志保留行数.Margin = New Padding(2, 2, 2, 2)
        MCB_任务日志保留行数.Name = "MCB_任务日志保留行数"
        MCB_任务日志保留行数.Padding = New Padding(10, 0, 10, 0)
        MCB_任务日志保留行数.Size = New Size(200, 32)
        MCB_任务日志保留行数.TabIndex = 0
        MCB_任务日志保留行数.ToolTipGap = -1
        MCB_任务日志保留行数.ToolTipMaxWidth = 350
        MCB_任务日志保留行数.ToolTipPadding = New Padding(15)
        MCB_任务日志保留行数.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' Panel11
        ' 
        Panel11.Controls.Add(HtmlColorLabel11)
        Panel11.Controls.Add(MCB_任务日志性能计数器)
        Panel11.Dock = DockStyle.Top
        Panel11.Location = New Point(20, 471)
        Panel11.Name = "Panel11"
        Panel11.Padding = New Padding(0, 10, 0, 0)
        Panel11.Size = New Size(702, 42)
        Panel11.TabIndex = 33
        ' 
        ' HtmlColorLabel11
        ' 
        HtmlColorLabel11.AutoSize = True
        HtmlColorLabel11.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel11.Dock = DockStyle.Fill
        HtmlColorLabel11.Location = New Point(200, 10)
        HtmlColorLabel11.Margin = New Padding(2)
        HtmlColorLabel11.Name = "HtmlColorLabel11"
        HtmlColorLabel11.Padding = New Padding(10, 0, 0, 0)
        HtmlColorLabel11.Size = New Size(502, 32)
        HtmlColorLabel11.TabIndex = 33
        HtmlColorLabel11.Text = "编码队列任务日志窗口底部显示当前任务性能计数器"
        HtmlColorLabel11.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.MiddleLeft
        ' 
        ' MCB_任务日志性能计数器
        ' 
        MCB_任务日志性能计数器.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_任务日志性能计数器.BorderRadius = 10
        MCB_任务日志性能计数器.BorderSize = 0
        MCB_任务日志性能计数器.Dock = DockStyle.Left
        MCB_任务日志性能计数器.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_任务日志性能计数器.DropDownHoverAnimationDuration = 0
        MCB_任务日志性能计数器.DropDownHoverColor = Color.FromArgb(CByte(80), CByte(0), CByte(0), CByte(0))
        MCB_任务日志性能计数器.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_任务日志性能计数器.DropDownPadding = New Padding(10)
        MCB_任务日志性能计数器.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_任务日志性能计数器.DropDownSelectedForeColor = Color.White
        MCB_任务日志性能计数器.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_任务日志性能计数器.Items.Add("显示")
        MCB_任务日志性能计数器.Items.Add("隐藏")
        MCB_任务日志性能计数器.Location = New Point(0, 10)
        MCB_任务日志性能计数器.Margin = New Padding(2, 2, 2, 2)
        MCB_任务日志性能计数器.Name = "MCB_任务日志性能计数器"
        MCB_任务日志性能计数器.Padding = New Padding(10, 0, 10, 0)
        MCB_任务日志性能计数器.Size = New Size(200, 32)
        MCB_任务日志性能计数器.TabIndex = 0
        MCB_任务日志性能计数器.ToolTipGap = -1
        MCB_任务日志性能计数器.ToolTipMaxWidth = 350
        MCB_任务日志性能计数器.ToolTipPadding = New Padding(15)
        MCB_任务日志性能计数器.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' Panel9
        ' 
        Panel9.Controls.Add(HtmlColorLabel9)
        Panel9.Controls.Add(MCB_编码队列显示最新日志行)
        Panel9.Dock = DockStyle.Top
        Panel9.Location = New Point(20, 387)
        Panel9.Name = "Panel9"
        Panel9.Padding = New Padding(0, 10, 0, 0)
        Panel9.Size = New Size(702, 42)
        Panel9.TabIndex = 29
        ' 
        ' HtmlColorLabel9
        ' 
        HtmlColorLabel9.AutoSize = True
        HtmlColorLabel9.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel9.Dock = DockStyle.Fill
        HtmlColorLabel9.Location = New Point(200, 10)
        HtmlColorLabel9.Margin = New Padding(2)
        HtmlColorLabel9.Name = "HtmlColorLabel9"
        HtmlColorLabel9.Padding = New Padding(10, 0, 0, 0)
        HtmlColorLabel9.Size = New Size(502, 32)
        HtmlColorLabel9.TabIndex = 29
        HtmlColorLabel9.Text = "编码队列项底部显示最新日志"
        HtmlColorLabel9.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.MiddleLeft
        ' 
        ' MCB_编码队列显示最新日志行
        ' 
        MCB_编码队列显示最新日志行.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_编码队列显示最新日志行.BorderRadius = 10
        MCB_编码队列显示最新日志行.BorderSize = 0
        MCB_编码队列显示最新日志行.Dock = DockStyle.Left
        MCB_编码队列显示最新日志行.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_编码队列显示最新日志行.DropDownHoverAnimationDuration = 0
        MCB_编码队列显示最新日志行.DropDownHoverColor = Color.FromArgb(CByte(80), CByte(0), CByte(0), CByte(0))
        MCB_编码队列显示最新日志行.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_编码队列显示最新日志行.DropDownPadding = New Padding(10)
        MCB_编码队列显示最新日志行.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_编码队列显示最新日志行.DropDownSelectedForeColor = Color.White
        MCB_编码队列显示最新日志行.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_编码队列显示最新日志行.Items.Add("显示")
        MCB_编码队列显示最新日志行.Items.Add("隐藏")
        MCB_编码队列显示最新日志行.Location = New Point(0, 10)
        MCB_编码队列显示最新日志行.Margin = New Padding(2, 2, 2, 2)
        MCB_编码队列显示最新日志行.Name = "MCB_编码队列显示最新日志行"
        MCB_编码队列显示最新日志行.Padding = New Padding(10, 0, 10, 0)
        MCB_编码队列显示最新日志行.Size = New Size(200, 32)
        MCB_编码队列显示最新日志行.TabIndex = 0
        MCB_编码队列显示最新日志行.ToolTipGap = -1
        MCB_编码队列显示最新日志行.ToolTipMaxWidth = 350
        MCB_编码队列显示最新日志行.ToolTipPadding = New Padding(15)
        MCB_编码队列显示最新日志行.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' Panel7
        ' 
        Panel7.Controls.Add(HtmlColorLabel7)
        Panel7.Controls.Add(MCB_独立参数面板自动切预设管理)
        Panel7.Dock = DockStyle.Top
        Panel7.Location = New Point(20, 303)
        Panel7.Name = "Panel7"
        Panel7.Padding = New Padding(0, 10, 0, 0)
        Panel7.Size = New Size(702, 42)
        Panel7.TabIndex = 25
        ' 
        ' HtmlColorLabel7
        ' 
        HtmlColorLabel7.AutoSize = True
        HtmlColorLabel7.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel7.Dock = DockStyle.Fill
        HtmlColorLabel7.Location = New Point(200, 10)
        HtmlColorLabel7.Margin = New Padding(2)
        HtmlColorLabel7.Name = "HtmlColorLabel7"
        HtmlColorLabel7.Padding = New Padding(10, 0, 0, 0)
        HtmlColorLabel7.Size = New Size(502, 32)
        HtmlColorLabel7.TabIndex = 25
        HtmlColorLabel7.Text = "打开独立参数面板时要自动切到预设管理吗？"
        HtmlColorLabel7.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.MiddleLeft
        ' 
        ' MCB_独立参数面板自动切预设管理
        ' 
        MCB_独立参数面板自动切预设管理.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_独立参数面板自动切预设管理.BorderRadius = 10
        MCB_独立参数面板自动切预设管理.BorderSize = 0
        MCB_独立参数面板自动切预设管理.Dock = DockStyle.Left
        MCB_独立参数面板自动切预设管理.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_独立参数面板自动切预设管理.DropDownHoverAnimationDuration = 0
        MCB_独立参数面板自动切预设管理.DropDownHoverColor = Color.FromArgb(CByte(80), CByte(0), CByte(0), CByte(0))
        MCB_独立参数面板自动切预设管理.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_独立参数面板自动切预设管理.DropDownPadding = New Padding(10)
        MCB_独立参数面板自动切预设管理.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_独立参数面板自动切预设管理.DropDownSelectedForeColor = Color.White
        MCB_独立参数面板自动切预设管理.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_独立参数面板自动切预设管理.Items.Add("默认第一个页面")
        MCB_独立参数面板自动切预设管理.Items.Add("自动切到预设管理")
        MCB_独立参数面板自动切预设管理.Location = New Point(0, 10)
        MCB_独立参数面板自动切预设管理.Margin = New Padding(2, 2, 2, 2)
        MCB_独立参数面板自动切预设管理.Name = "MCB_独立参数面板自动切预设管理"
        MCB_独立参数面板自动切预设管理.Padding = New Padding(10, 0, 10, 0)
        MCB_独立参数面板自动切预设管理.Size = New Size(200, 32)
        MCB_独立参数面板自动切预设管理.TabIndex = 0
        MCB_独立参数面板自动切预设管理.ToolTipGap = -1
        MCB_独立参数面板自动切预设管理.ToolTipMaxWidth = 350
        MCB_独立参数面板自动切预设管理.ToolTipPadding = New Padding(15)
        MCB_独立参数面板自动切预设管理.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' Panel6
        ' 
        Panel6.Controls.Add(HtmlColorLabel6)
        Panel6.Controls.Add(MCB_任务名称混淆)
        Panel6.Dock = DockStyle.Top
        Panel6.Location = New Point(20, 261)
        Panel6.Name = "Panel6"
        Panel6.Padding = New Padding(0, 10, 0, 0)
        Panel6.Size = New Size(702, 42)
        Panel6.TabIndex = 23
        ' 
        ' HtmlColorLabel6
        ' 
        HtmlColorLabel6.AutoSize = True
        HtmlColorLabel6.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel6.Dock = DockStyle.Fill
        HtmlColorLabel6.Location = New Point(200, 10)
        HtmlColorLabel6.Margin = New Padding(2)
        HtmlColorLabel6.Name = "HtmlColorLabel6"
        HtmlColorLabel6.Padding = New Padding(10, 0, 0, 0)
        HtmlColorLabel6.Size = New Size(502, 32)
        HtmlColorLabel6.TabIndex = 23
        HtmlColorLabel6.Text = "你这任务正经吗？"
        HtmlColorLabel6.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.MiddleLeft
        ' 
        ' MCB_任务名称混淆
        ' 
        MCB_任务名称混淆.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_任务名称混淆.BorderRadius = 10
        MCB_任务名称混淆.BorderSize = 0
        MCB_任务名称混淆.Dock = DockStyle.Left
        MCB_任务名称混淆.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_任务名称混淆.DropDownHoverAnimationDuration = 0
        MCB_任务名称混淆.DropDownHoverColor = Color.FromArgb(CByte(80), CByte(0), CByte(0), CByte(0))
        MCB_任务名称混淆.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_任务名称混淆.DropDownPadding = New Padding(10)
        MCB_任务名称混淆.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_任务名称混淆.DropDownSelectedForeColor = Color.White
        MCB_任务名称混淆.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_任务名称混淆.Items.Add("默认")
        MCB_任务名称混淆.Items.Add("我请来了喵喵星神")
        MCB_任务名称混淆.Items.Add("华强买瓜宇宙")
        MCB_任务名称混淆.Location = New Point(0, 10)
        MCB_任务名称混淆.Margin = New Padding(2, 2, 2, 2)
        MCB_任务名称混淆.Name = "MCB_任务名称混淆"
        MCB_任务名称混淆.Padding = New Padding(10, 0, 10, 0)
        MCB_任务名称混淆.Size = New Size(200, 32)
        MCB_任务名称混淆.TabIndex = 0
        MCB_任务名称混淆.ToolTipGap = -1
        MCB_任务名称混淆.ToolTipMaxWidth = 350
        MCB_任务名称混淆.ToolTipPadding = New Padding(15)
        MCB_任务名称混淆.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' Panel5
        ' 
        Panel5.Controls.Add(HtmlColorLabel5)
        Panel5.Controls.Add(MCB_是否自动重置参数面板到第一个页面)
        Panel5.Dock = DockStyle.Top
        Panel5.Location = New Point(20, 219)
        Panel5.Name = "Panel5"
        Panel5.Padding = New Padding(0, 10, 0, 0)
        Panel5.Size = New Size(702, 42)
        Panel5.TabIndex = 21
        ' 
        ' HtmlColorLabel5
        ' 
        HtmlColorLabel5.AutoSize = True
        HtmlColorLabel5.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel5.Dock = DockStyle.Fill
        HtmlColorLabel5.Location = New Point(200, 10)
        HtmlColorLabel5.Margin = New Padding(2)
        HtmlColorLabel5.Name = "HtmlColorLabel5"
        HtmlColorLabel5.Padding = New Padding(10, 0, 0, 0)
        HtmlColorLabel5.Size = New Size(502, 32)
        HtmlColorLabel5.TabIndex = 21
        HtmlColorLabel5.Text = "从主选项卡切回参数面板是否需要重置到第一个页面"
        HtmlColorLabel5.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.MiddleLeft
        ' 
        ' MCB_是否自动重置参数面板到第一个页面
        ' 
        MCB_是否自动重置参数面板到第一个页面.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_是否自动重置参数面板到第一个页面.BorderRadius = 10
        MCB_是否自动重置参数面板到第一个页面.BorderSize = 0
        MCB_是否自动重置参数面板到第一个页面.Dock = DockStyle.Left
        MCB_是否自动重置参数面板到第一个页面.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_是否自动重置参数面板到第一个页面.DropDownHoverAnimationDuration = 0
        MCB_是否自动重置参数面板到第一个页面.DropDownHoverColor = Color.FromArgb(CByte(80), CByte(0), CByte(0), CByte(0))
        MCB_是否自动重置参数面板到第一个页面.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_是否自动重置参数面板到第一个页面.DropDownPadding = New Padding(10)
        MCB_是否自动重置参数面板到第一个页面.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_是否自动重置参数面板到第一个页面.DropDownSelectedForeColor = Color.White
        MCB_是否自动重置参数面板到第一个页面.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_是否自动重置参数面板到第一个页面.Items.Add("不要自动重置页面")
        MCB_是否自动重置参数面板到第一个页面.Items.Add("自动切到参数总览")
        MCB_是否自动重置参数面板到第一个页面.Location = New Point(0, 10)
        MCB_是否自动重置参数面板到第一个页面.Margin = New Padding(2, 2, 2, 2)
        MCB_是否自动重置参数面板到第一个页面.Name = "MCB_是否自动重置参数面板到第一个页面"
        MCB_是否自动重置参数面板到第一个页面.Padding = New Padding(10, 0, 10, 0)
        MCB_是否自动重置参数面板到第一个页面.Size = New Size(200, 32)
        MCB_是否自动重置参数面板到第一个页面.TabIndex = 0
        MCB_是否自动重置参数面板到第一个页面.ToolTipGap = -1
        MCB_是否自动重置参数面板到第一个页面.ToolTipMaxWidth = 350
        MCB_是否自动重置参数面板到第一个页面.ToolTipPadding = New Padding(15)
        MCB_是否自动重置参数面板到第一个页面.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' Panel4
        ' 
        Panel4.Controls.Add(HtmlColorLabel4)
        Panel4.Controls.Add(MCB_是否自动开始任务)
        Panel4.Dock = DockStyle.Top
        Panel4.Location = New Point(20, 177)
        Panel4.Name = "Panel4"
        Panel4.Padding = New Padding(0, 10, 0, 0)
        Panel4.Size = New Size(702, 42)
        Panel4.TabIndex = 19
        ' 
        ' HtmlColorLabel4
        ' 
        HtmlColorLabel4.AutoSize = True
        HtmlColorLabel4.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel4.Dock = DockStyle.Fill
        HtmlColorLabel4.Location = New Point(200, 10)
        HtmlColorLabel4.Margin = New Padding(2)
        HtmlColorLabel4.Name = "HtmlColorLabel4"
        HtmlColorLabel4.Padding = New Padding(10, 0, 0, 0)
        HtmlColorLabel4.Size = New Size(502, 32)
        HtmlColorLabel4.TabIndex = 19
        HtmlColorLabel4.Text = "是否自动开始任务"
        HtmlColorLabel4.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.MiddleLeft
        ' 
        ' MCB_是否自动开始任务
        ' 
        MCB_是否自动开始任务.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_是否自动开始任务.BorderRadius = 10
        MCB_是否自动开始任务.BorderSize = 0
        MCB_是否自动开始任务.Dock = DockStyle.Left
        MCB_是否自动开始任务.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_是否自动开始任务.DropDownHoverAnimationDuration = 0
        MCB_是否自动开始任务.DropDownHoverColor = Color.FromArgb(CByte(80), CByte(0), CByte(0), CByte(0))
        MCB_是否自动开始任务.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_是否自动开始任务.DropDownPadding = New Padding(10)
        MCB_是否自动开始任务.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_是否自动开始任务.DropDownSelectedForeColor = Color.White
        MCB_是否自动开始任务.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_是否自动开始任务.Items.Add("自动开始任务")
        MCB_是否自动开始任务.Items.Add("手动开始任务")
        MCB_是否自动开始任务.Location = New Point(0, 10)
        MCB_是否自动开始任务.Margin = New Padding(2, 2, 2, 2)
        MCB_是否自动开始任务.Name = "MCB_是否自动开始任务"
        MCB_是否自动开始任务.Padding = New Padding(10, 0, 10, 0)
        MCB_是否自动开始任务.Size = New Size(200, 32)
        MCB_是否自动开始任务.TabIndex = 0
        MCB_是否自动开始任务.ToolTipGap = -1
        MCB_是否自动开始任务.ToolTipMaxWidth = 350
        MCB_是否自动开始任务.ToolTipPadding = New Padding(15)
        MCB_是否自动开始任务.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' Panel3
        ' 
        Panel3.Controls.Add(HtmlColorLabel3)
        Panel3.Controls.Add(MCB_是否启用提示音)
        Panel3.Dock = DockStyle.Top
        Panel3.Location = New Point(20, 135)
        Panel3.Name = "Panel3"
        Panel3.Padding = New Padding(0, 10, 0, 0)
        Panel3.Size = New Size(702, 42)
        Panel3.TabIndex = 17
        ' 
        ' HtmlColorLabel3
        ' 
        HtmlColorLabel3.AutoSize = True
        HtmlColorLabel3.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel3.Dock = DockStyle.Fill
        HtmlColorLabel3.Location = New Point(200, 10)
        HtmlColorLabel3.Margin = New Padding(2)
        HtmlColorLabel3.Name = "HtmlColorLabel3"
        HtmlColorLabel3.Padding = New Padding(10, 0, 0, 0)
        HtmlColorLabel3.Size = New Size(502, 32)
        HtmlColorLabel3.TabIndex = 17
        HtmlColorLabel3.Text = "是否启用提示音"
        HtmlColorLabel3.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.MiddleLeft
        ' 
        ' MCB_是否启用提示音
        ' 
        MCB_是否启用提示音.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_是否启用提示音.BorderRadius = 10
        MCB_是否启用提示音.BorderSize = 0
        MCB_是否启用提示音.Dock = DockStyle.Left
        MCB_是否启用提示音.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_是否启用提示音.DropDownHoverAnimationDuration = 0
        MCB_是否启用提示音.DropDownHoverColor = Color.FromArgb(CByte(80), CByte(0), CByte(0), CByte(0))
        MCB_是否启用提示音.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_是否启用提示音.DropDownPadding = New Padding(10)
        MCB_是否启用提示音.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_是否启用提示音.DropDownSelectedForeColor = Color.White
        MCB_是否启用提示音.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_是否启用提示音.Items.Add("启用提示音")
        MCB_是否启用提示音.Items.Add("禁用提示音")
        MCB_是否启用提示音.Location = New Point(0, 10)
        MCB_是否启用提示音.Margin = New Padding(2, 2, 2, 2)
        MCB_是否启用提示音.Name = "MCB_是否启用提示音"
        MCB_是否启用提示音.Padding = New Padding(10, 0, 10, 0)
        MCB_是否启用提示音.Size = New Size(200, 32)
        MCB_是否启用提示音.TabIndex = 0
        MCB_是否启用提示音.ToolTipGap = -1
        MCB_是否启用提示音.ToolTipMaxWidth = 350
        MCB_是否启用提示音.ToolTipPadding = New Padding(15)
        MCB_是否启用提示音.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' Panel1
        ' 
        Panel1.Controls.Add(HtmlColorLabel2)
        Panel1.Controls.Add(MCB_有任务时系统状态)
        Panel1.Dock = DockStyle.Top
        Panel1.Location = New Point(20, 93)
        Panel1.Name = "Panel1"
        Panel1.Padding = New Padding(0, 10, 0, 0)
        Panel1.Size = New Size(702, 42)
        Panel1.TabIndex = 15
        ' 
        ' HtmlColorLabel2
        ' 
        HtmlColorLabel2.AutoSize = True
        HtmlColorLabel2.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel2.Dock = DockStyle.Fill
        HtmlColorLabel2.Location = New Point(200, 10)
        HtmlColorLabel2.Margin = New Padding(2)
        HtmlColorLabel2.Name = "HtmlColorLabel2"
        HtmlColorLabel2.Padding = New Padding(10, 0, 0, 0)
        HtmlColorLabel2.Size = New Size(502, 32)
        HtmlColorLabel2.TabIndex = 15
        HtmlColorLabel2.Text = "有任务时系统要保持什么状态"
        HtmlColorLabel2.TextAlign = LakeUI.HtmlColorLabel.TextAlignEnum.MiddleLeft
        ' 
        ' MCB_有任务时系统状态
        ' 
        MCB_有任务时系统状态.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_有任务时系统状态.BorderRadius = 10
        MCB_有任务时系统状态.BorderSize = 0
        MCB_有任务时系统状态.Dock = DockStyle.Left
        MCB_有任务时系统状态.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_有任务时系统状态.DropDownHoverAnimationDuration = 0
        MCB_有任务时系统状态.DropDownHoverColor = Color.FromArgb(CByte(80), CByte(0), CByte(0), CByte(0))
        MCB_有任务时系统状态.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_有任务时系统状态.DropDownPadding = New Padding(10)
        MCB_有任务时系统状态.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_有任务时系统状态.DropDownSelectedForeColor = Color.White
        MCB_有任务时系统状态.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_有任务时系统状态.Items.Add("阻止系统休眠（默认）")
        MCB_有任务时系统状态.Items.Add("阻止显示器超时关闭")
        MCB_有任务时系统状态.Items.Add("默认状态（不推荐）")
        MCB_有任务时系统状态.Location = New Point(0, 10)
        MCB_有任务时系统状态.Margin = New Padding(2, 2, 2, 2)
        MCB_有任务时系统状态.Name = "MCB_有任务时系统状态"
        MCB_有任务时系统状态.Padding = New Padding(10, 0, 10, 0)
        MCB_有任务时系统状态.Size = New Size(200, 32)
        MCB_有任务时系统状态.TabIndex = 0
        MCB_有任务时系统状态.ToolTipGap = -1
        MCB_有任务时系统状态.ToolTipMaxWidth = 350
        MCB_有任务时系统状态.ToolTipPadding = New Padding(15)
        MCB_有任务时系统状态.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' JustEmptyControl1
        ' 
        JustEmptyControl1.Dock = DockStyle.Top
        JustEmptyControl1.Location = New Point(20, 83)
        JustEmptyControl1.Name = "JustEmptyControl1"
        JustEmptyControl1.Size = New Size(702, 10)
        JustEmptyControl1.TabIndex = 28
        ' 
        ' Panel2
        ' 
        Panel2.Controls.Add(MTB_工作目录)
        Panel2.Controls.Add(JustEmptyControl2)
        Panel2.Controls.Add(MB_浏览文件夹)
        Panel2.Dock = DockStyle.Top
        Panel2.Location = New Point(20, 41)
        Panel2.Name = "Panel2"
        Panel2.Padding = New Padding(0, 10, 0, 0)
        Panel2.Size = New Size(702, 42)
        Panel2.TabIndex = 13
        ' 
        ' MTB_工作目录
        ' 
        MTB_工作目录.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_工作目录.BorderColor = Color.Empty
        MTB_工作目录.BorderColorFocus = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MTB_工作目录.BorderRadius = 10
        MTB_工作目录.Dock = DockStyle.Fill
        MTB_工作目录.Location = New Point(0, 10)
        MTB_工作目录.Margin = New Padding(2)
        MTB_工作目录.Name = "MTB_工作目录"
        MTB_工作目录.Padding = New Padding(10, 0, 0, 0)
        MTB_工作目录.Size = New Size(612, 32)
        MTB_工作目录.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_工作目录.TabIndex = 19
        ' 
        ' JustEmptyControl2
        ' 
        JustEmptyControl2.Dock = DockStyle.Right
        JustEmptyControl2.Location = New Point(612, 10)
        JustEmptyControl2.Name = "JustEmptyControl2"
        JustEmptyControl2.Size = New Size(10, 32)
        JustEmptyControl2.TabIndex = 7
        ' 
        ' MB_浏览文件夹
        ' 
        MB_浏览文件夹.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_浏览文件夹.BorderRadius = 10
        MB_浏览文件夹.BorderSize = 0
        MB_浏览文件夹.Dock = DockStyle.Right
        MB_浏览文件夹.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_浏览文件夹.Location = New Point(622, 10)
        MB_浏览文件夹.Margin = New Padding(2)
        MB_浏览文件夹.Name = "MB_浏览文件夹"
        MB_浏览文件夹.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_浏览文件夹.Size = New Size(80, 32)
        MB_浏览文件夹.TabIndex = 6
        MB_浏览文件夹.Text = "浏览"
        ' 
        ' HtmlColorLabel1
        ' 
        HtmlColorLabel1.AutoSize = True
        HtmlColorLabel1.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HtmlColorLabel1.Dock = DockStyle.Top
        HtmlColorLabel1.Location = New Point(20, 20)
        HtmlColorLabel1.Margin = New Padding(2)
        HtmlColorLabel1.Name = "HtmlColorLabel1"
        HtmlColorLabel1.Size = New Size(702, 21)
        HtmlColorLabel1.TabIndex = 8
        HtmlColorLabel1.Text = "设置工作目录"
        ' 
        ' Form_v6_设置_功能设定
        ' 
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ClientSize = New Size(742, 651)
        Controls.Add(ModernPanel1)
        Font = New Font("Microsoft YaHei UI", 10F)
        ForeColor = Color.Silver
        Name = "Form_v6_设置_功能设定"
        Text = "Form_v6_设置_功能设定"
        ModernPanel1.ResumeLayout(False)
        ModernPanel1.PerformLayout()
        Panel8.ResumeLayout(False)
        Panel8.PerformLayout()
        Panel10.ResumeLayout(False)
        Panel10.PerformLayout()
        Panel11.ResumeLayout(False)
        Panel11.PerformLayout()
        Panel9.ResumeLayout(False)
        Panel9.PerformLayout()
        Panel7.ResumeLayout(False)
        Panel7.PerformLayout()
        Panel6.ResumeLayout(False)
        Panel6.PerformLayout()
        Panel5.ResumeLayout(False)
        Panel5.PerformLayout()
        Panel4.ResumeLayout(False)
        Panel4.PerformLayout()
        Panel3.ResumeLayout(False)
        Panel3.PerformLayout()
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        Panel2.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents ModernPanel1 As LakeUI.ModernPanel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents MCB_是否启用提示音 As LakeUI.ModernComboBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents MCB_有任务时系统状态 As LakeUI.ModernComboBox
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel7 As Panel
    Friend WithEvents MCB_独立参数面板自动切预设管理 As LakeUI.ModernComboBox
    Friend WithEvents Panel6 As Panel
    Friend WithEvents MCB_任务名称混淆 As LakeUI.ModernComboBox
    Friend WithEvents Panel5 As Panel
    Friend WithEvents MCB_是否自动重置参数面板到第一个页面 As LakeUI.ModernComboBox
    Friend WithEvents Panel4 As Panel
    Friend WithEvents MCB_是否自动开始任务 As LakeUI.ModernComboBox
    Friend WithEvents Panel8 As Panel
    Friend WithEvents MCB_任务失败删除文件 As LakeUI.ModernComboBox
    Friend WithEvents Panel10 As Panel
    Friend WithEvents MCB_任务日志保留行数 As LakeUI.ModernComboBox
    Friend WithEvents Panel11 As Panel
    Friend WithEvents MCB_任务日志性能计数器 As LakeUI.ModernComboBox
    Friend WithEvents Panel9 As Panel
    Friend WithEvents MCB_编码队列显示最新日志行 As LakeUI.ModernComboBox
    Friend WithEvents HtmlColorLabel1 As LakeUI.HtmlColorLabel
    Friend WithEvents HtmlColorLabel10 As LakeUI.HtmlColorLabel
    Friend WithEvents HtmlColorLabel11 As LakeUI.HtmlColorLabel
    Friend WithEvents HtmlColorLabel9 As LakeUI.HtmlColorLabel
    Friend WithEvents HtmlColorLabel8 As LakeUI.HtmlColorLabel
    Friend WithEvents HtmlColorLabel7 As LakeUI.HtmlColorLabel
    Friend WithEvents HtmlColorLabel6 As LakeUI.HtmlColorLabel
    Friend WithEvents HtmlColorLabel5 As LakeUI.HtmlColorLabel
    Friend WithEvents HtmlColorLabel4 As LakeUI.HtmlColorLabel
    Friend WithEvents HtmlColorLabel3 As LakeUI.HtmlColorLabel
    Friend WithEvents HtmlColorLabel2 As LakeUI.HtmlColorLabel
    Friend WithEvents JustEmptyControl1 As LakeUI.JustEmptyControl
    Friend WithEvents JustEmptyControl2 As LakeUI.JustEmptyControl
    Friend WithEvents MB_浏览文件夹 As LakeUI.ModernButton
    Friend WithEvents MTB_工作目录 As LakeUI.ModernTextBox
End Class
