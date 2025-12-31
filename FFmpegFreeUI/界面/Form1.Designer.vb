<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        UiTabControlMenu1 = New Sunny.UI.UITabControlMenu()
        TabPage起始页面 = New TabPage()
        TabPage编码队列 = New TabPage()
        Panel输出面板 = New Panel()
        Panel77 = New Panel()
        RichTextBox2 = New RichTextBox()
        Label45 = New Label()
        Panel13 = New Panel()
        UiCheckBox强制滚动到最后 = New Sunny.UI.UICheckBox()
        Label44 = New Label()
        UiButton复制输出 = New Sunny.UI.UIButton()
        Label15 = New Label()
        UiComboBox输出显示类型 = New Sunny.UI.UIComboBox()
        Panel56 = New Panel()
        ListView1 = New ListView()
        ColumnHeader1 = New ColumnHeader()
        ColumnHeader2 = New ColumnHeader()
        ColumnHeader3 = New ColumnHeader()
        ColumnHeader4 = New ColumnHeader()
        ColumnHeader5 = New ColumnHeader()
        ColumnHeader6 = New ColumnHeader()
        ColumnHeader7 = New ColumnHeader()
        ColumnHeader8 = New ColumnHeader()
        ImageList1 = New ImageList(components)
        Panel1 = New Panel()
        Panel15 = New Panel()
        UiButton任务管理菜单 = New Sunny.UI.UIButton()
        Label状态 = New Label()
        Label进度 = New Label()
        Label效率 = New Label()
        Label输出大小 = New Label()
        Label质量 = New Label()
        Label比特率 = New Label()
        Label预计剩余 = New Label()
        Panel41 = New Panel()
        Panel35 = New Panel()
        Labelffmpeg实时信息 = New Label()
        LinkLabel切换显示输出面板 = New LinkLabel()
        Panel36 = New Panel()
        LinkLabel向ffmpeg发送消息 = New LinkLabel()
        Panel2 = New Panel()
        UiButton定位输出 = New Sunny.UI.UIButton()
        UiButton重置任务 = New Sunny.UI.UIButton()
        UiButton移除任务 = New Sunny.UI.UIButton()
        UiButton停止任务 = New Sunny.UI.UIButton()
        UiButton恢复任务 = New Sunny.UI.UIButton()
        UiButton暂停任务 = New Sunny.UI.UIButton()
        UiButton开始任务 = New Sunny.UI.UIButton()
        TabPage准备文件 = New TabPage()
        TabPage参数面板 = New TabPage()
        Panel6 = New Panel()
        TabPage媒体信息 = New TabPage()
        TabPage播放器 = New TabPage()
        TabPage混流 = New TabPage()
        TabPage合并 = New TabPage()
        TabPage性能监控 = New TabPage()
        TabPage插件扩展 = New TabPage()
        Panel21 = New Panel()
        Panel24 = New Panel()
        Panel22 = New Panel()
        Label16 = New Label()
        UiComboBox3 = New Sunny.UI.UIComboBox()
        TabPage设置 = New TabPage()
        TabPage支持者名单 = New TabPage()
        Panel顶部视觉修正区域 = New Panel()
        Panel顶部视觉修正区域_二级选项卡 = New Panel()
        Panel顶部视觉修正区域_一级选项卡 = New Panel()
        UiTabControlMenu1.SuspendLayout()
        TabPage编码队列.SuspendLayout()
        Panel输出面板.SuspendLayout()
        Panel77.SuspendLayout()
        Panel13.SuspendLayout()
        Panel56.SuspendLayout()
        Panel1.SuspendLayout()
        Panel15.SuspendLayout()
        Panel41.SuspendLayout()
        Panel35.SuspendLayout()
        Panel36.SuspendLayout()
        Panel2.SuspendLayout()
        TabPage参数面板.SuspendLayout()
        TabPage插件扩展.SuspendLayout()
        Panel21.SuspendLayout()
        Panel22.SuspendLayout()
        Panel顶部视觉修正区域.SuspendLayout()
        SuspendLayout()
        ' 
        ' UiTabControlMenu1
        ' 
        UiTabControlMenu1.Alignment = TabAlignment.Left
        UiTabControlMenu1.Controls.Add(TabPage起始页面)
        UiTabControlMenu1.Controls.Add(TabPage编码队列)
        UiTabControlMenu1.Controls.Add(TabPage准备文件)
        UiTabControlMenu1.Controls.Add(TabPage参数面板)
        UiTabControlMenu1.Controls.Add(TabPage媒体信息)
        UiTabControlMenu1.Controls.Add(TabPage播放器)
        UiTabControlMenu1.Controls.Add(TabPage混流)
        UiTabControlMenu1.Controls.Add(TabPage合并)
        UiTabControlMenu1.Controls.Add(TabPage性能监控)
        UiTabControlMenu1.Controls.Add(TabPage插件扩展)
        UiTabControlMenu1.Controls.Add(TabPage设置)
        UiTabControlMenu1.Controls.Add(TabPage支持者名单)
        UiTabControlMenu1.Dock = DockStyle.Fill
        UiTabControlMenu1.DrawMode = TabDrawMode.OwnerDrawFixed
        UiTabControlMenu1.FillColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTabControlMenu1.Font = New Font("微软雅黑", 11F)
        UiTabControlMenu1.ItemSize = New Size(150, 38)
        UiTabControlMenu1.Location = New Point(0, 10)
        UiTabControlMenu1.MenuStyle = Sunny.UI.UIMenuStyle.Custom
        UiTabControlMenu1.Multiline = True
        UiTabControlMenu1.Name = "UiTabControlMenu1"
        UiTabControlMenu1.SelectedIndex = 0
        UiTabControlMenu1.Size = New Size(1184, 651)
        UiTabControlMenu1.SizeMode = TabSizeMode.Fixed
        UiTabControlMenu1.TabIndex = 1
        UiTabControlMenu1.TabSelectedColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiTabControlMenu1.TabSelectedForeColor = Color.YellowGreen
        UiTabControlMenu1.TabSelectedHighColor = Color.YellowGreen
        UiTabControlMenu1.TextAlignment = HorizontalAlignment.Left
        ' 
        ' TabPage起始页面
        ' 
        TabPage起始页面.BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        TabPage起始页面.Font = New Font("微软雅黑", 10F)
        TabPage起始页面.Location = New Point(151, 0)
        TabPage起始页面.Name = "TabPage起始页面"
        TabPage起始页面.Size = New Size(1033, 651)
        TabPage起始页面.TabIndex = 10
        TabPage起始页面.Text = "3FUI"
        ' 
        ' TabPage编码队列
        ' 
        TabPage编码队列.BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        TabPage编码队列.Controls.Add(Panel输出面板)
        TabPage编码队列.Controls.Add(Panel56)
        TabPage编码队列.Controls.Add(Panel1)
        TabPage编码队列.Controls.Add(Panel41)
        TabPage编码队列.Controls.Add(Panel2)
        TabPage编码队列.Font = New Font("微软雅黑", 10F)
        TabPage编码队列.Location = New Point(151, 0)
        TabPage编码队列.Name = "TabPage编码队列"
        TabPage编码队列.Size = New Size(1033, 651)
        TabPage编码队列.TabIndex = 0
        TabPage编码队列.Tag = "TabPage.EncodingQueue"
        TabPage编码队列.Text = "编码队列"
        ' 
        ' Panel输出面板
        ' 
        Panel输出面板.BackColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        Panel输出面板.Controls.Add(Panel77)
        Panel输出面板.Controls.Add(Label45)
        Panel输出面板.Controls.Add(Panel13)
        Panel输出面板.Dock = DockStyle.Right
        Panel输出面板.Location = New Point(533, 80)
        Panel输出面板.Name = "Panel输出面板"
        Panel输出面板.Padding = New Padding(10, 10, 10, 0)
        Panel输出面板.Size = New Size(500, 531)
        Panel输出面板.TabIndex = 37
        Panel输出面板.Visible = False
        ' 
        ' Panel77
        ' 
        Panel77.BorderStyle = BorderStyle.FixedSingle
        Panel77.Controls.Add(RichTextBox2)
        Panel77.Dock = DockStyle.Fill
        Panel77.Location = New Point(10, 80)
        Panel77.Name = "Panel77"
        Panel77.Padding = New Padding(10, 10, 0, 10)
        Panel77.Size = New Size(480, 451)
        Panel77.TabIndex = 37
        ' 
        ' RichTextBox2
        ' 
        RichTextBox2.BackColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        RichTextBox2.BorderStyle = BorderStyle.None
        RichTextBox2.DetectUrls = False
        RichTextBox2.Dock = DockStyle.Fill
        RichTextBox2.Font = New Font("微软雅黑", 10F)
        RichTextBox2.ForeColor = Color.DarkGray
        RichTextBox2.Location = New Point(10, 10)
        RichTextBox2.Name = "RichTextBox2"
        RichTextBox2.ReadOnly = True
        RichTextBox2.ScrollBars = RichTextBoxScrollBars.Vertical
        RichTextBox2.Size = New Size(468, 429)
        RichTextBox2.TabIndex = 11
        RichTextBox2.Text = ""
        ' 
        ' Label45
        ' 
        Label45.AutoSize = True
        Label45.Dock = DockStyle.Top
        Label45.Font = New Font("微软雅黑", 10F)
        Label45.ForeColor = Color.Gray
        Label45.Location = New Point(10, 50)
        Label45.Name = "Label45"
        Label45.Padding = New Padding(0, 0, 0, 10)
        Label45.Size = New Size(441, 30)
        Label45.TabIndex = 119
        Label45.Text = "最新输出最多保存 1000 行，任务进行时查看这些信息可能会影响体验"
        Label45.TextAlign = ContentAlignment.BottomLeft
        ' 
        ' Panel13
        ' 
        Panel13.Controls.Add(UiCheckBox强制滚动到最后)
        Panel13.Controls.Add(Label44)
        Panel13.Controls.Add(UiButton复制输出)
        Panel13.Controls.Add(Label15)
        Panel13.Controls.Add(UiComboBox输出显示类型)
        Panel13.Dock = DockStyle.Top
        Panel13.Location = New Point(10, 10)
        Panel13.Name = "Panel13"
        Panel13.Padding = New Padding(0, 0, 0, 10)
        Panel13.Size = New Size(480, 40)
        Panel13.TabIndex = 12
        ' 
        ' UiCheckBox强制滚动到最后
        ' 
        UiCheckBox强制滚动到最后.CheckBoxColor = Color.Silver
        UiCheckBox强制滚动到最后.CheckBoxSize = 20
        UiCheckBox强制滚动到最后.Dock = DockStyle.Fill
        UiCheckBox强制滚动到最后.Font = New Font("微软雅黑", 10F)
        UiCheckBox强制滚动到最后.ForeColor = Color.DarkGray
        UiCheckBox强制滚动到最后.Location = New Point(300, 0)
        UiCheckBox强制滚动到最后.MinimumSize = New Size(1, 1)
        UiCheckBox强制滚动到最后.Name = "UiCheckBox强制滚动到最后"
        UiCheckBox强制滚动到最后.Size = New Size(180, 30)
        UiCheckBox强制滚动到最后.TabIndex = 99
        UiCheckBox强制滚动到最后.Text = "强制滚动到最后"
        ' 
        ' Label44
        ' 
        Label44.Dock = DockStyle.Left
        Label44.Location = New Point(290, 0)
        Label44.Name = "Label44"
        Label44.Size = New Size(10, 30)
        Label44.TabIndex = 100
        ' 
        ' UiButton复制输出
        ' 
        UiButton复制输出.Dock = DockStyle.Left
        UiButton复制输出.FillColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        UiButton复制输出.FillColor2 = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        UiButton复制输出.FillDisableColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        UiButton复制输出.FillHoverColor = SystemColors.WindowFrame
        UiButton复制输出.FillPressColor = Color.FromArgb(CByte(120), CByte(120), CByte(120))
        UiButton复制输出.FillSelectedColor = Color.FromArgb(CByte(120), CByte(120), CByte(120))
        UiButton复制输出.Font = New Font("微软雅黑", 10F)
        UiButton复制输出.ForeColor = Color.Silver
        UiButton复制输出.ForeDisableColor = Color.Silver
        UiButton复制输出.ForeHoverColor = Color.Silver
        UiButton复制输出.ForeSelectedColor = Color.Silver
        UiButton复制输出.Location = New Point(210, 0)
        UiButton复制输出.MinimumSize = New Size(1, 1)
        UiButton复制输出.Name = "UiButton复制输出"
        UiButton复制输出.Radius = 0
        UiButton复制输出.RadiusSides = Sunny.UI.UICornerRadiusSides.None
        UiButton复制输出.RectColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        UiButton复制输出.RectDisableColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        UiButton复制输出.RectHoverColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        UiButton复制输出.RectPressColor = Color.FromArgb(CByte(64), CByte(148), CByte(64))
        UiButton复制输出.RectSelectedColor = Color.DarkGray
        UiButton复制输出.Size = New Size(80, 30)
        UiButton复制输出.TabIndex = 93
        UiButton复制输出.Text = "复制"
        UiButton复制输出.TipsFont = New Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        ' 
        ' Label15
        ' 
        Label15.Dock = DockStyle.Left
        Label15.Location = New Point(200, 0)
        Label15.Name = "Label15"
        Label15.Size = New Size(10, 30)
        Label15.TabIndex = 92
        ' 
        ' UiComboBox输出显示类型
        ' 
        UiComboBox输出显示类型.DataSource = Nothing
        UiComboBox输出显示类型.Dock = DockStyle.Left
        UiComboBox输出显示类型.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList
        UiComboBox输出显示类型.FillColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiComboBox输出显示类型.FillColor2 = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiComboBox输出显示类型.FillDisableColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiComboBox输出显示类型.Font = New Font("微软雅黑", 10F)
        UiComboBox输出显示类型.ForeColor = Color.Silver
        UiComboBox输出显示类型.ForeDisableColor = Color.Silver
        UiComboBox输出显示类型.ItemFillColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiComboBox输出显示类型.ItemForeColor = Color.Silver
        UiComboBox输出显示类型.ItemHeight = 30
        UiComboBox输出显示类型.ItemHoverColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiComboBox输出显示类型.ItemRectColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiComboBox输出显示类型.Items.AddRange(New Object() {"最新输出 (不含进度)", "仅错误信息"})
        UiComboBox输出显示类型.ItemSelectBackColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        UiComboBox输出显示类型.ItemSelectForeColor = Color.Silver
        UiComboBox输出显示类型.Location = New Point(0, 0)
        UiComboBox输出显示类型.Margin = New Padding(4, 5, 4, 5)
        UiComboBox输出显示类型.MaxDropDownItems = 17
        UiComboBox输出显示类型.MinimumSize = New Size(63, 0)
        UiComboBox输出显示类型.Name = "UiComboBox输出显示类型"
        UiComboBox输出显示类型.Padding = New Padding(0, 0, 30, 2)
        UiComboBox输出显示类型.Radius = 0
        UiComboBox输出显示类型.RadiusSides = Sunny.UI.UICornerRadiusSides.None
        UiComboBox输出显示类型.RectColor = SystemColors.WindowFrame
        UiComboBox输出显示类型.RectDisableColor = SystemColors.WindowFrame
        UiComboBox输出显示类型.ScrollBarHandleWidth = 20
        UiComboBox输出显示类型.Size = New Size(200, 30)
        UiComboBox输出显示类型.Style = Sunny.UI.UIStyle.Custom
        UiComboBox输出显示类型.SymbolSize = 24
        UiComboBox输出显示类型.TabIndex = 91
        UiComboBox输出显示类型.TextAlignment = ContentAlignment.MiddleLeft
        UiComboBox输出显示类型.Watermark = "选择类别"
        ' 
        ' Panel56
        ' 
        Panel56.Controls.Add(ListView1)
        Panel56.Dock = DockStyle.Fill
        Panel56.Location = New Point(0, 80)
        Panel56.Name = "Panel56"
        Panel56.Padding = New Padding(15, 15, 0, 15)
        Panel56.Size = New Size(1033, 531)
        Panel56.TabIndex = 4
        ' 
        ' ListView1
        ' 
        ListView1.AllowDrop = True
        ListView1.BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ListView1.BorderStyle = BorderStyle.None
        ListView1.Columns.AddRange(New ColumnHeader() {ColumnHeader1, ColumnHeader2, ColumnHeader3, ColumnHeader4, ColumnHeader5, ColumnHeader6, ColumnHeader7, ColumnHeader8})
        ListView1.Dock = DockStyle.Fill
        ListView1.ForeColor = Color.Silver
        ListView1.FullRowSelect = True
        ListView1.HeaderStyle = ColumnHeaderStyle.None
        ListView1.LabelEdit = True
        ListView1.Location = New Point(15, 15)
        ListView1.Name = "ListView1"
        ListView1.OwnerDraw = True
        ListView1.ShowItemToolTips = True
        ListView1.Size = New Size(1018, 501)
        ListView1.StateImageList = ImageList1
        ListView1.TabIndex = 0
        ListView1.TabStop = False
        ListView1.UseCompatibleStateImageBehavior = False
        ListView1.View = View.Details
        ' 
        ' ImageList1
        ' 
        ImageList1.ColorDepth = ColorDepth.Depth32Bit
        ImageList1.ImageSize = New Size(1, 30)
        ImageList1.TransparentColor = Color.Transparent
        ' 
        ' Panel1
        ' 
        Panel1.BackColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        Panel1.Controls.Add(Panel15)
        Panel1.Controls.Add(Label状态)
        Panel1.Controls.Add(Label进度)
        Panel1.Controls.Add(Label效率)
        Panel1.Controls.Add(Label输出大小)
        Panel1.Controls.Add(Label质量)
        Panel1.Controls.Add(Label比特率)
        Panel1.Controls.Add(Label预计剩余)
        Panel1.Dock = DockStyle.Top
        Panel1.Location = New Point(0, 40)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(1033, 40)
        Panel1.TabIndex = 2
        ' 
        ' Panel15
        ' 
        Panel15.Controls.Add(UiButton任务管理菜单)
        Panel15.Dock = DockStyle.Fill
        Panel15.Location = New Point(0, 0)
        Panel15.Name = "Panel15"
        Panel15.Size = New Size(283, 40)
        Panel15.TabIndex = 8
        ' 
        ' UiButton任务管理菜单
        ' 
        UiButton任务管理菜单.Dock = DockStyle.Left
        UiButton任务管理菜单.FillColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiButton任务管理菜单.FillColor2 = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiButton任务管理菜单.FillDisableColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiButton任务管理菜单.FillHoverColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton任务管理菜单.FillPressColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiButton任务管理菜单.FillSelectedColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiButton任务管理菜单.Font = New Font("微软雅黑", 10F)
        UiButton任务管理菜单.ForeColor = Color.CornflowerBlue
        UiButton任务管理菜单.ForeDisableColor = Color.CornflowerBlue
        UiButton任务管理菜单.ForeHoverColor = Color.CornflowerBlue
        UiButton任务管理菜单.ForePressColor = Color.CornflowerBlue
        UiButton任务管理菜单.ForeSelectedColor = Color.CornflowerBlue
        UiButton任务管理菜单.Location = New Point(0, 0)
        UiButton任务管理菜单.MinimumSize = New Size(1, 1)
        UiButton任务管理菜单.Name = "UiButton任务管理菜单"
        UiButton任务管理菜单.Radius = 0
        UiButton任务管理菜单.RadiusSides = Sunny.UI.UICornerRadiusSides.None
        UiButton任务管理菜单.RectColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiButton任务管理菜单.RectDisableColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiButton任务管理菜单.RectHoverColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton任务管理菜单.RectPressColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiButton任务管理菜单.RectSelectedColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiButton任务管理菜单.Size = New Size(120, 40)
        UiButton任务管理菜单.TabIndex = 45
        UiButton任务管理菜单.Tag = "Button.MissionMenu"
        UiButton任务管理菜单.Text = "任务管理菜单"
        UiButton任务管理菜单.TipsFont = New Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        ' 
        ' Label状态
        ' 
        Label状态.Dock = DockStyle.Right
        Label状态.Location = New Point(283, 0)
        Label状态.Name = "Label状态"
        Label状态.Size = New Size(80, 40)
        Label状态.TabIndex = 1
        Label状态.Tag = "Label.Status"
        Label状态.Text = "状态"
        Label状态.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label进度
        ' 
        Label进度.Dock = DockStyle.Right
        Label进度.Location = New Point(363, 0)
        Label进度.Name = "Label进度"
        Label进度.Size = New Size(70, 40)
        Label进度.TabIndex = 2
        Label进度.Tag = "Label.Progress"
        Label进度.Text = "进度"
        Label进度.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label效率
        ' 
        Label效率.Dock = DockStyle.Right
        Label效率.Location = New Point(433, 0)
        Label效率.Name = "Label效率"
        Label效率.Size = New Size(80, 40)
        Label效率.TabIndex = 3
        Label效率.Tag = "Label.Speed"
        Label效率.Text = "效率"
        Label效率.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label输出大小
        ' 
        Label输出大小.Dock = DockStyle.Right
        Label输出大小.Location = New Point(513, 0)
        Label输出大小.Name = "Label输出大小"
        Label输出大小.Size = New Size(150, 40)
        Label输出大小.TabIndex = 4
        Label输出大小.Tag = "Label.OutputSize"
        Label输出大小.Text = "输出大小 && 预估"
        Label输出大小.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label质量
        ' 
        Label质量.Dock = DockStyle.Right
        Label质量.Location = New Point(663, 0)
        Label质量.Name = "Label质量"
        Label质量.Size = New Size(55, 40)
        Label质量.TabIndex = 5
        Label质量.Tag = "Label.Quality"
        Label质量.Text = "质量"
        Label质量.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label比特率
        ' 
        Label比特率.Dock = DockStyle.Right
        Label比特率.Location = New Point(718, 0)
        Label比特率.Name = "Label比特率"
        Label比特率.Size = New Size(115, 40)
        Label比特率.TabIndex = 6
        Label比特率.Tag = "Label.Bitrate"
        Label比特率.Text = "比特率"
        Label比特率.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label预计剩余
        ' 
        Label预计剩余.Dock = DockStyle.Right
        Label预计剩余.Location = New Point(833, 0)
        Label预计剩余.Name = "Label预计剩余"
        Label预计剩余.Size = New Size(200, 40)
        Label预计剩余.TabIndex = 7
        Label预计剩余.Tag = "Label.TimeRemaining"
        Label预计剩余.Text = "预计剩余 && 已用"
        Label预计剩余.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Panel41
        ' 
        Panel41.AutoSize = True
        Panel41.BackColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        Panel41.Controls.Add(Panel35)
        Panel41.Dock = DockStyle.Bottom
        Panel41.Location = New Point(0, 611)
        Panel41.Name = "Panel41"
        Panel41.Padding = New Padding(10)
        Panel41.Size = New Size(1033, 40)
        Panel41.TabIndex = 5
        Panel41.Visible = False
        ' 
        ' Panel35
        ' 
        Panel35.AutoSize = True
        Panel35.Controls.Add(Labelffmpeg实时信息)
        Panel35.Controls.Add(LinkLabel切换显示输出面板)
        Panel35.Controls.Add(Panel36)
        Panel35.Dock = DockStyle.Top
        Panel35.Location = New Point(10, 10)
        Panel35.Name = "Panel35"
        Panel35.Size = New Size(1013, 20)
        Panel35.TabIndex = 17
        ' 
        ' Labelffmpeg实时信息
        ' 
        Labelffmpeg实时信息.AutoSize = True
        Labelffmpeg实时信息.Dock = DockStyle.Top
        Labelffmpeg实时信息.ForeColor = Color.CornflowerBlue
        Labelffmpeg实时信息.Location = New Point(102, 0)
        Labelffmpeg实时信息.Name = "Labelffmpeg实时信息"
        Labelffmpeg实时信息.Size = New Size(118, 20)
        Labelffmpeg实时信息.TabIndex = 14
        Labelffmpeg实时信息.Text = "ffmpeg 实时信息"
        ' 
        ' LinkLabel切换显示输出面板
        ' 
        LinkLabel切换显示输出面板.ActiveLinkColor = Color.MediumPurple
        LinkLabel切换显示输出面板.AutoSize = True
        LinkLabel切换显示输出面板.Dock = DockStyle.Left
        LinkLabel切换显示输出面板.LinkBehavior = LinkBehavior.HoverUnderline
        LinkLabel切换显示输出面板.LinkColor = Color.YellowGreen
        LinkLabel切换显示输出面板.Location = New Point(37, 0)
        LinkLabel切换显示输出面板.Name = "LinkLabel切换显示输出面板"
        LinkLabel切换显示输出面板.Size = New Size(65, 20)
        LinkLabel切换显示输出面板.TabIndex = 17
        LinkLabel切换显示输出面板.TabStop = True
        LinkLabel切换显示输出面板.Text = "输出面板"
        ' 
        ' Panel36
        ' 
        Panel36.AutoSize = True
        Panel36.Controls.Add(LinkLabel向ffmpeg发送消息)
        Panel36.Dock = DockStyle.Left
        Panel36.Location = New Point(0, 0)
        Panel36.Name = "Panel36"
        Panel36.Size = New Size(37, 20)
        Panel36.TabIndex = 16
        ' 
        ' LinkLabel向ffmpeg发送消息
        ' 
        LinkLabel向ffmpeg发送消息.ActiveLinkColor = Color.MediumPurple
        LinkLabel向ffmpeg发送消息.AutoSize = True
        LinkLabel向ffmpeg发送消息.Dock = DockStyle.Left
        LinkLabel向ffmpeg发送消息.LinkBehavior = LinkBehavior.HoverUnderline
        LinkLabel向ffmpeg发送消息.LinkColor = Color.Silver
        LinkLabel向ffmpeg发送消息.Location = New Point(0, 0)
        LinkLabel向ffmpeg发送消息.Name = "LinkLabel向ffmpeg发送消息"
        LinkLabel向ffmpeg发送消息.Size = New Size(37, 20)
        LinkLabel向ffmpeg发送消息.TabIndex = 16
        LinkLabel向ffmpeg发送消息.TabStop = True
        LinkLabel向ffmpeg发送消息.Tag = "LinkLabel.Send"
        LinkLabel向ffmpeg发送消息.Text = "发送"
        ' 
        ' Panel2
        ' 
        Panel2.BackColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        Panel2.Controls.Add(UiButton定位输出)
        Panel2.Controls.Add(UiButton重置任务)
        Panel2.Controls.Add(UiButton移除任务)
        Panel2.Controls.Add(UiButton停止任务)
        Panel2.Controls.Add(UiButton恢复任务)
        Panel2.Controls.Add(UiButton暂停任务)
        Panel2.Controls.Add(UiButton开始任务)
        Panel2.Dock = DockStyle.Top
        Panel2.Location = New Point(0, 0)
        Panel2.Name = "Panel2"
        Panel2.Padding = New Padding(0, 0, 10, 0)
        Panel2.Size = New Size(1033, 40)
        Panel2.TabIndex = 3
        ' 
        ' UiButton定位输出
        ' 
        UiButton定位输出.Dock = DockStyle.Left
        UiButton定位输出.FillColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiButton定位输出.FillColor2 = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiButton定位输出.FillDisableColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiButton定位输出.FillHoverColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton定位输出.FillPressColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiButton定位输出.FillSelectedColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiButton定位输出.Font = New Font("微软雅黑", 11F)
        UiButton定位输出.ForeColor = Color.MediumPurple
        UiButton定位输出.ForeDisableColor = Color.MediumPurple
        UiButton定位输出.ForeHoverColor = Color.MediumPurple
        UiButton定位输出.ForePressColor = Color.MediumPurple
        UiButton定位输出.ForeSelectedColor = Color.MediumPurple
        UiButton定位输出.Location = New Point(390, 0)
        UiButton定位输出.MinimumSize = New Size(1, 1)
        UiButton定位输出.Name = "UiButton定位输出"
        UiButton定位输出.Radius = 0
        UiButton定位输出.RadiusSides = Sunny.UI.UICornerRadiusSides.None
        UiButton定位输出.RectColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiButton定位输出.RectDisableColor = Color.FromArgb(CByte(12), CByte(12), CByte(12))
        UiButton定位输出.RectHoverColor = Color.CornflowerBlue
        UiButton定位输出.RectPressColor = Color.FromArgb(CByte(64), CByte(148), CByte(64))
        UiButton定位输出.RectSelectedColor = Color.CornflowerBlue
        UiButton定位输出.RectSides = ToolStripStatusLabelBorderSides.None
        UiButton定位输出.Size = New Size(65, 40)
        UiButton定位输出.TabIndex = 86
        UiButton定位输出.Tag = "Button.LocateMission"
        UiButton定位输出.Text = "定位"
        UiButton定位输出.TipsFont = New Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        UiButton定位输出.TipsText = "定位输出"
        ' 
        ' UiButton重置任务
        ' 
        UiButton重置任务.Dock = DockStyle.Left
        UiButton重置任务.FillColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiButton重置任务.FillColor2 = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiButton重置任务.FillDisableColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiButton重置任务.FillHoverColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton重置任务.FillPressColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiButton重置任务.FillSelectedColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiButton重置任务.Font = New Font("微软雅黑", 11F)
        UiButton重置任务.ForeColor = Color.Goldenrod
        UiButton重置任务.ForeDisableColor = Color.Goldenrod
        UiButton重置任务.ForeHoverColor = Color.Goldenrod
        UiButton重置任务.ForePressColor = Color.Goldenrod
        UiButton重置任务.ForeSelectedColor = Color.Goldenrod
        UiButton重置任务.Location = New Point(325, 0)
        UiButton重置任务.MinimumSize = New Size(1, 1)
        UiButton重置任务.Name = "UiButton重置任务"
        UiButton重置任务.Radius = 0
        UiButton重置任务.RadiusSides = Sunny.UI.UICornerRadiusSides.None
        UiButton重置任务.RectColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiButton重置任务.RectDisableColor = Color.FromArgb(CByte(12), CByte(12), CByte(12))
        UiButton重置任务.RectHoverColor = Color.CornflowerBlue
        UiButton重置任务.RectPressColor = Color.FromArgb(CByte(64), CByte(148), CByte(64))
        UiButton重置任务.RectSelectedColor = Color.CornflowerBlue
        UiButton重置任务.RectSides = ToolStripStatusLabelBorderSides.None
        UiButton重置任务.Size = New Size(65, 40)
        UiButton重置任务.TabIndex = 78
        UiButton重置任务.Tag = "Button.ResetMission"
        UiButton重置任务.Text = "重置"
        UiButton重置任务.TipsFont = New Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        ' 
        ' UiButton移除任务
        ' 
        UiButton移除任务.Dock = DockStyle.Left
        UiButton移除任务.FillColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiButton移除任务.FillColor2 = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiButton移除任务.FillDisableColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiButton移除任务.FillHoverColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton移除任务.FillPressColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiButton移除任务.FillSelectedColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiButton移除任务.Font = New Font("微软雅黑", 11F)
        UiButton移除任务.ForeColor = Color.IndianRed
        UiButton移除任务.ForeDisableColor = Color.IndianRed
        UiButton移除任务.ForeHoverColor = Color.IndianRed
        UiButton移除任务.ForePressColor = Color.IndianRed
        UiButton移除任务.ForeSelectedColor = Color.IndianRed
        UiButton移除任务.Location = New Point(260, 0)
        UiButton移除任务.MinimumSize = New Size(1, 1)
        UiButton移除任务.Name = "UiButton移除任务"
        UiButton移除任务.Radius = 0
        UiButton移除任务.RadiusSides = Sunny.UI.UICornerRadiusSides.None
        UiButton移除任务.RectColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiButton移除任务.RectDisableColor = Color.FromArgb(CByte(12), CByte(12), CByte(12))
        UiButton移除任务.RectHoverColor = Color.CornflowerBlue
        UiButton移除任务.RectPressColor = Color.FromArgb(CByte(64), CByte(148), CByte(64))
        UiButton移除任务.RectSelectedColor = Color.CornflowerBlue
        UiButton移除任务.RectSides = ToolStripStatusLabelBorderSides.None
        UiButton移除任务.Size = New Size(65, 40)
        UiButton移除任务.TabIndex = 54
        UiButton移除任务.Tag = "Button.RemoveMission"
        UiButton移除任务.Text = "移除"
        UiButton移除任务.TipsFont = New Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        ' 
        ' UiButton停止任务
        ' 
        UiButton停止任务.Dock = DockStyle.Left
        UiButton停止任务.FillColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiButton停止任务.FillColor2 = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiButton停止任务.FillDisableColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiButton停止任务.FillHoverColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton停止任务.FillPressColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiButton停止任务.FillSelectedColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiButton停止任务.Font = New Font("微软雅黑", 11F)
        UiButton停止任务.ForeColor = Color.IndianRed
        UiButton停止任务.ForeDisableColor = Color.IndianRed
        UiButton停止任务.ForeHoverColor = Color.IndianRed
        UiButton停止任务.ForePressColor = Color.IndianRed
        UiButton停止任务.ForeSelectedColor = Color.IndianRed
        UiButton停止任务.Location = New Point(195, 0)
        UiButton停止任务.MinimumSize = New Size(1, 1)
        UiButton停止任务.Name = "UiButton停止任务"
        UiButton停止任务.Radius = 0
        UiButton停止任务.RadiusSides = Sunny.UI.UICornerRadiusSides.None
        UiButton停止任务.RectColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiButton停止任务.RectDisableColor = Color.FromArgb(CByte(12), CByte(12), CByte(12))
        UiButton停止任务.RectHoverColor = Color.CornflowerBlue
        UiButton停止任务.RectPressColor = Color.FromArgb(CByte(64), CByte(148), CByte(64))
        UiButton停止任务.RectSelectedColor = Color.CornflowerBlue
        UiButton停止任务.RectSides = ToolStripStatusLabelBorderSides.None
        UiButton停止任务.Size = New Size(65, 40)
        UiButton停止任务.TabIndex = 52
        UiButton停止任务.Tag = "Button.StopMission"
        UiButton停止任务.Text = "停止"
        UiButton停止任务.TipsFont = New Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        ' 
        ' UiButton恢复任务
        ' 
        UiButton恢复任务.Dock = DockStyle.Left
        UiButton恢复任务.FillColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiButton恢复任务.FillColor2 = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiButton恢复任务.FillDisableColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiButton恢复任务.FillHoverColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton恢复任务.FillPressColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiButton恢复任务.FillSelectedColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiButton恢复任务.Font = New Font("微软雅黑", 11F)
        UiButton恢复任务.ForeColor = Color.YellowGreen
        UiButton恢复任务.ForeDisableColor = Color.YellowGreen
        UiButton恢复任务.ForeHoverColor = Color.YellowGreen
        UiButton恢复任务.ForePressColor = Color.YellowGreen
        UiButton恢复任务.ForeSelectedColor = Color.YellowGreen
        UiButton恢复任务.Location = New Point(130, 0)
        UiButton恢复任务.MinimumSize = New Size(1, 1)
        UiButton恢复任务.Name = "UiButton恢复任务"
        UiButton恢复任务.Radius = 0
        UiButton恢复任务.RadiusSides = Sunny.UI.UICornerRadiusSides.None
        UiButton恢复任务.RectColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiButton恢复任务.RectDisableColor = Color.FromArgb(CByte(12), CByte(12), CByte(12))
        UiButton恢复任务.RectHoverColor = Color.CornflowerBlue
        UiButton恢复任务.RectPressColor = Color.FromArgb(CByte(64), CByte(148), CByte(64))
        UiButton恢复任务.RectSelectedColor = Color.CornflowerBlue
        UiButton恢复任务.RectSides = ToolStripStatusLabelBorderSides.None
        UiButton恢复任务.Size = New Size(65, 40)
        UiButton恢复任务.TabIndex = 48
        UiButton恢复任务.Tag = "Button.ResumeMission"
        UiButton恢复任务.Text = "恢复"
        UiButton恢复任务.TipsFont = New Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        ' 
        ' UiButton暂停任务
        ' 
        UiButton暂停任务.Dock = DockStyle.Left
        UiButton暂停任务.FillColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiButton暂停任务.FillColor2 = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiButton暂停任务.FillDisableColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiButton暂停任务.FillHoverColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton暂停任务.FillPressColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiButton暂停任务.FillSelectedColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiButton暂停任务.Font = New Font("微软雅黑", 11F)
        UiButton暂停任务.ForeColor = Color.Goldenrod
        UiButton暂停任务.ForeDisableColor = Color.Goldenrod
        UiButton暂停任务.ForeHoverColor = Color.Goldenrod
        UiButton暂停任务.ForePressColor = Color.Goldenrod
        UiButton暂停任务.ForeSelectedColor = Color.Goldenrod
        UiButton暂停任务.Location = New Point(65, 0)
        UiButton暂停任务.MinimumSize = New Size(1, 1)
        UiButton暂停任务.Name = "UiButton暂停任务"
        UiButton暂停任务.Radius = 0
        UiButton暂停任务.RadiusSides = Sunny.UI.UICornerRadiusSides.None
        UiButton暂停任务.RectColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiButton暂停任务.RectDisableColor = Color.FromArgb(CByte(12), CByte(12), CByte(12))
        UiButton暂停任务.RectHoverColor = Color.CornflowerBlue
        UiButton暂停任务.RectPressColor = Color.FromArgb(CByte(64), CByte(148), CByte(64))
        UiButton暂停任务.RectSelectedColor = Color.CornflowerBlue
        UiButton暂停任务.RectSides = ToolStripStatusLabelBorderSides.None
        UiButton暂停任务.Size = New Size(65, 40)
        UiButton暂停任务.TabIndex = 46
        UiButton暂停任务.Tag = "Button.PauseMission"
        UiButton暂停任务.Text = "暂停"
        UiButton暂停任务.TipsFont = New Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        ' 
        ' UiButton开始任务
        ' 
        UiButton开始任务.Dock = DockStyle.Left
        UiButton开始任务.FillColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiButton开始任务.FillColor2 = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiButton开始任务.FillDisableColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiButton开始任务.FillHoverColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton开始任务.FillPressColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiButton开始任务.FillSelectedColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiButton开始任务.Font = New Font("微软雅黑", 11F)
        UiButton开始任务.ForeColor = Color.YellowGreen
        UiButton开始任务.ForeDisableColor = Color.YellowGreen
        UiButton开始任务.ForeHoverColor = Color.YellowGreen
        UiButton开始任务.ForePressColor = Color.YellowGreen
        UiButton开始任务.ForeSelectedColor = Color.YellowGreen
        UiButton开始任务.Location = New Point(0, 0)
        UiButton开始任务.MinimumSize = New Size(1, 1)
        UiButton开始任务.Name = "UiButton开始任务"
        UiButton开始任务.Radius = 0
        UiButton开始任务.RadiusSides = Sunny.UI.UICornerRadiusSides.None
        UiButton开始任务.RectColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiButton开始任务.RectDisableColor = Color.FromArgb(CByte(12), CByte(12), CByte(12))
        UiButton开始任务.RectHoverColor = Color.CornflowerBlue
        UiButton开始任务.RectPressColor = Color.FromArgb(CByte(64), CByte(148), CByte(64))
        UiButton开始任务.RectSelectedColor = Color.CornflowerBlue
        UiButton开始任务.RectSides = ToolStripStatusLabelBorderSides.None
        UiButton开始任务.Size = New Size(65, 40)
        UiButton开始任务.TabIndex = 44
        UiButton开始任务.Tag = "Button.StartMission"
        UiButton开始任务.Text = "开始"
        UiButton开始任务.TipsFont = New Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        ' 
        ' TabPage准备文件
        ' 
        TabPage准备文件.BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        TabPage准备文件.Font = New Font("微软雅黑", 10F)
        TabPage准备文件.Location = New Point(151, 0)
        TabPage准备文件.Name = "TabPage准备文件"
        TabPage准备文件.Size = New Size(1033, 651)
        TabPage准备文件.TabIndex = 1
        TabPage准备文件.Tag = "TabPage.PrepareFile"
        TabPage准备文件.Text = "准备文件"
        ' 
        ' TabPage参数面板
        ' 
        TabPage参数面板.BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        TabPage参数面板.Controls.Add(Panel6)
        TabPage参数面板.Location = New Point(151, 0)
        TabPage参数面板.Name = "TabPage参数面板"
        TabPage参数面板.Size = New Size(1033, 651)
        TabPage参数面板.TabIndex = 14
        TabPage参数面板.Tag = "TabPage.ParameterPanel"
        TabPage参数面板.Text = "参数面板"
        ' 
        ' Panel6
        ' 
        Panel6.Dock = DockStyle.Fill
        Panel6.Font = New Font("微软雅黑", 10F)
        Panel6.Location = New Point(0, 0)
        Panel6.Name = "Panel6"
        Panel6.Size = New Size(1033, 651)
        Panel6.TabIndex = 0
        ' 
        ' TabPage媒体信息
        ' 
        TabPage媒体信息.BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        TabPage媒体信息.Location = New Point(151, 0)
        TabPage媒体信息.Name = "TabPage媒体信息"
        TabPage媒体信息.Size = New Size(1033, 651)
        TabPage媒体信息.TabIndex = 17
        TabPage媒体信息.Tag = "TabPage.MediaInfo"
        TabPage媒体信息.Text = "媒体信息"
        ' 
        ' TabPage播放器
        ' 
        TabPage播放器.BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        TabPage播放器.Location = New Point(151, 0)
        TabPage播放器.Name = "TabPage播放器"
        TabPage播放器.Size = New Size(1033, 651)
        TabPage播放器.TabIndex = 26
        TabPage播放器.Tag = "TabPage.Player"
        TabPage播放器.Text = "播放器"
        ' 
        ' TabPage混流
        ' 
        TabPage混流.BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        TabPage混流.Location = New Point(151, 0)
        TabPage混流.Name = "TabPage混流"
        TabPage混流.Size = New Size(1033, 651)
        TabPage混流.TabIndex = 18
        TabPage混流.Tag = "TabPage.Mixing"
        TabPage混流.Text = "混流"
        ' 
        ' TabPage合并
        ' 
        TabPage合并.BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        TabPage合并.Location = New Point(151, 0)
        TabPage合并.Name = "TabPage合并"
        TabPage合并.Size = New Size(1033, 651)
        TabPage合并.TabIndex = 19
        TabPage合并.Tag = "TabPage.Merging"
        TabPage合并.Text = "合并"
        ' 
        ' TabPage性能监控
        ' 
        TabPage性能监控.BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        TabPage性能监控.Location = New Point(151, 0)
        TabPage性能监控.Name = "TabPage性能监控"
        TabPage性能监控.Size = New Size(1033, 651)
        TabPage性能监控.TabIndex = 21
        TabPage性能监控.Tag = "TabPage.PerformanceMonitoring"
        TabPage性能监控.Text = "性能监控"
        ' 
        ' TabPage插件扩展
        ' 
        TabPage插件扩展.BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        TabPage插件扩展.Controls.Add(Panel21)
        TabPage插件扩展.Location = New Point(151, 0)
        TabPage插件扩展.Name = "TabPage插件扩展"
        TabPage插件扩展.Size = New Size(1033, 651)
        TabPage插件扩展.TabIndex = 22
        TabPage插件扩展.Tag = "TabPage.PluginExtension"
        TabPage插件扩展.Text = "插件扩展"
        ' 
        ' Panel21
        ' 
        Panel21.Controls.Add(Panel24)
        Panel21.Controls.Add(Panel22)
        Panel21.Dock = DockStyle.Fill
        Panel21.Location = New Point(0, 0)
        Panel21.Name = "Panel21"
        Panel21.Size = New Size(1033, 651)
        Panel21.TabIndex = 0
        ' 
        ' Panel24
        ' 
        Panel24.Dock = DockStyle.Fill
        Panel24.Location = New Point(0, 50)
        Panel24.Name = "Panel24"
        Panel24.Size = New Size(1033, 601)
        Panel24.TabIndex = 86
        ' 
        ' Panel22
        ' 
        Panel22.BackColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        Panel22.Controls.Add(Label16)
        Panel22.Controls.Add(UiComboBox3)
        Panel22.Dock = DockStyle.Top
        Panel22.Location = New Point(0, 0)
        Panel22.Name = "Panel22"
        Panel22.Padding = New Padding(10)
        Panel22.Size = New Size(1033, 50)
        Panel22.TabIndex = 84
        ' 
        ' Label16
        ' 
        Label16.Dock = DockStyle.Fill
        Label16.Font = New Font("微软雅黑", 10F)
        Label16.ForeColor = Color.Gray
        Label16.Location = New Point(310, 10)
        Label16.Name = "Label16"
        Label16.Padding = New Padding(10, 0, 0, 0)
        Label16.Size = New Size(713, 30)
        Label16.TabIndex = 87
        Label16.Text = "开发插件的技术细节请参阅 GitHub 的 README"
        Label16.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' UiComboBox3
        ' 
        UiComboBox3.DataSource = Nothing
        UiComboBox3.Dock = DockStyle.Left
        UiComboBox3.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList
        UiComboBox3.FillColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiComboBox3.FillColor2 = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiComboBox3.FillDisableColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiComboBox3.Font = New Font("微软雅黑", 9.75F)
        UiComboBox3.ForeColor = Color.Silver
        UiComboBox3.ForeDisableColor = Color.Silver
        UiComboBox3.ItemFillColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiComboBox3.ItemForeColor = Color.Silver
        UiComboBox3.ItemHeight = 30
        UiComboBox3.ItemHoverColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiComboBox3.ItemRectColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiComboBox3.ItemSelectBackColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        UiComboBox3.ItemSelectForeColor = Color.Silver
        UiComboBox3.Location = New Point(10, 10)
        UiComboBox3.Margin = New Padding(4, 5, 4, 5)
        UiComboBox3.MaxDropDownItems = 17
        UiComboBox3.MinimumSize = New Size(63, 0)
        UiComboBox3.Name = "UiComboBox3"
        UiComboBox3.Padding = New Padding(0, 0, 30, 2)
        UiComboBox3.RectColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        UiComboBox3.RectDisableColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        UiComboBox3.ScrollBarHandleWidth = 20
        UiComboBox3.Size = New Size(300, 30)
        UiComboBox3.Style = Sunny.UI.UIStyle.Custom
        UiComboBox3.SymbolSize = 24
        UiComboBox3.TabIndex = 86
        UiComboBox3.TextAlignment = ContentAlignment.MiddleLeft
        UiComboBox3.Watermark = ""
        ' 
        ' TabPage设置
        ' 
        TabPage设置.AutoScroll = True
        TabPage设置.BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        TabPage设置.Location = New Point(151, 0)
        TabPage设置.Name = "TabPage设置"
        TabPage设置.Size = New Size(1033, 651)
        TabPage设置.TabIndex = 24
        TabPage设置.Tag = "TabPage.Settings"
        TabPage设置.Text = "设置"
        ' 
        ' TabPage支持者名单
        ' 
        TabPage支持者名单.BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        TabPage支持者名单.Location = New Point(151, 0)
        TabPage支持者名单.Name = "TabPage支持者名单"
        TabPage支持者名单.Size = New Size(1033, 651)
        TabPage支持者名单.TabIndex = 25
        TabPage支持者名单.Tag = "TabPage.Supporters"
        TabPage支持者名单.Text = "支持者"
        ' 
        ' Panel顶部视觉修正区域
        ' 
        Panel顶部视觉修正区域.Controls.Add(Panel顶部视觉修正区域_二级选项卡)
        Panel顶部视觉修正区域.Controls.Add(Panel顶部视觉修正区域_一级选项卡)
        Panel顶部视觉修正区域.Dock = DockStyle.Top
        Panel顶部视觉修正区域.Location = New Point(0, 0)
        Panel顶部视觉修正区域.Name = "Panel顶部视觉修正区域"
        Panel顶部视觉修正区域.Size = New Size(1184, 10)
        Panel顶部视觉修正区域.TabIndex = 2
        ' 
        ' Panel顶部视觉修正区域_二级选项卡
        ' 
        Panel顶部视觉修正区域_二级选项卡.BackColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        Panel顶部视觉修正区域_二级选项卡.Dock = DockStyle.Left
        Panel顶部视觉修正区域_二级选项卡.Location = New Point(152, 0)
        Panel顶部视觉修正区域_二级选项卡.Name = "Panel顶部视觉修正区域_二级选项卡"
        Panel顶部视觉修正区域_二级选项卡.Size = New Size(202, 10)
        Panel顶部视觉修正区域_二级选项卡.TabIndex = 1
        ' 
        ' Panel顶部视觉修正区域_一级选项卡
        ' 
        Panel顶部视觉修正区域_一级选项卡.BackColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        Panel顶部视觉修正区域_一级选项卡.Dock = DockStyle.Left
        Panel顶部视觉修正区域_一级选项卡.Location = New Point(0, 0)
        Panel顶部视觉修正区域_一级选项卡.Name = "Panel顶部视觉修正区域_一级选项卡"
        Panel顶部视觉修正区域_一级选项卡.Size = New Size(152, 10)
        Panel顶部视觉修正区域_一级选项卡.TabIndex = 0
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ClientSize = New Size(1184, 661)
        Controls.Add(UiTabControlMenu1)
        Controls.Add(Panel顶部视觉修正区域)
        DoubleBuffered = True
        Font = New Font("微软雅黑", 10F)
        ForeColor = Color.Silver
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MinimumSize = New Size(1200, 700)
        Name = "Form1"
        ShowIcon = False
        StartPosition = FormStartPosition.CenterScreen
        Text = "FFmpegFreeUI"
        UiTabControlMenu1.ResumeLayout(False)
        TabPage编码队列.ResumeLayout(False)
        TabPage编码队列.PerformLayout()
        Panel输出面板.ResumeLayout(False)
        Panel输出面板.PerformLayout()
        Panel77.ResumeLayout(False)
        Panel13.ResumeLayout(False)
        Panel56.ResumeLayout(False)
        Panel1.ResumeLayout(False)
        Panel15.ResumeLayout(False)
        Panel41.ResumeLayout(False)
        Panel41.PerformLayout()
        Panel35.ResumeLayout(False)
        Panel35.PerformLayout()
        Panel36.ResumeLayout(False)
        Panel36.PerformLayout()
        Panel2.ResumeLayout(False)
        TabPage参数面板.ResumeLayout(False)
        TabPage插件扩展.ResumeLayout(False)
        Panel21.ResumeLayout(False)
        Panel22.ResumeLayout(False)
        Panel顶部视觉修正区域.ResumeLayout(False)
        ResumeLayout(False)
    End Sub
    Friend WithEvents UiTabControlMenu1 As Sunny.UI.UITabControlMenu
    Friend WithEvents TabPage编码队列 As TabPage
    Friend WithEvents TabPage准备文件 As TabPage
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label预计剩余 As Label
    Friend WithEvents Label比特率 As Label
    Friend WithEvents Label质量 As Label
    Friend WithEvents Label输出大小 As Label
    Friend WithEvents Label效率 As Label
    Friend WithEvents Label进度 As Label
    Friend WithEvents Label状态 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents UiButton开始任务 As Sunny.UI.UIButton
    Friend WithEvents UiButton移除任务 As Sunny.UI.UIButton
    Friend WithEvents UiButton停止任务 As Sunny.UI.UIButton
    Friend WithEvents UiButton恢复任务 As Sunny.UI.UIButton
    Friend WithEvents UiButton暂停任务 As Sunny.UI.UIButton
    Friend WithEvents Panel56 As Panel
    Friend WithEvents ListView1 As ListView
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents ColumnHeader2 As ColumnHeader
    Friend WithEvents ColumnHeader3 As ColumnHeader
    Friend WithEvents ColumnHeader4 As ColumnHeader
    Friend WithEvents ColumnHeader5 As ColumnHeader
    Friend WithEvents ColumnHeader6 As ColumnHeader
    Friend WithEvents ColumnHeader7 As ColumnHeader
    Friend WithEvents ColumnHeader8 As ColumnHeader
    Friend WithEvents ImageList1 As ImageList
    Friend WithEvents UiButton重置任务 As Sunny.UI.UIButton
    Friend WithEvents TabPage起始页面 As TabPage
    Friend WithEvents Panel41 As Panel
    Friend WithEvents TabPage参数面板 As TabPage
    Friend WithEvents Panel6 As Panel
    Friend WithEvents TabPage媒体信息 As TabPage
    Friend WithEvents UiButton定位输出 As Sunny.UI.UIButton
    Friend WithEvents TabPage混流 As TabPage
    Friend WithEvents TabPage合并 As TabPage
    Friend WithEvents TabPage性能监控 As TabPage
    Friend WithEvents TabPage插件扩展 As TabPage
    Friend WithEvents Panel21 As Panel
    Friend WithEvents Panel24 As Panel
    Friend WithEvents Panel22 As Panel
    Friend WithEvents Label16 As Label
    Friend WithEvents UiComboBox3 As Sunny.UI.UIComboBox
    Friend WithEvents Panel35 As Panel
    Friend WithEvents Labelffmpeg实时信息 As Label
    Friend WithEvents Panel36 As Panel
    Friend WithEvents LinkLabel向ffmpeg发送消息 As LinkLabel
    Friend WithEvents TabPage设置 As TabPage
    Friend WithEvents Panel输出面板 As Panel
    Friend WithEvents LinkLabel切换显示输出面板 As LinkLabel
    Friend WithEvents Panel13 As Panel
    Friend WithEvents UiComboBox输出显示类型 As Sunny.UI.UIComboBox
    Friend WithEvents Label15 As Label
    Friend WithEvents UiButton复制输出 As Sunny.UI.UIButton
    Friend WithEvents UiCheckBox强制滚动到最后 As Sunny.UI.UICheckBox
    Friend WithEvents Label44 As Label
    Friend WithEvents Panel77 As Panel
    Friend WithEvents RichTextBox2 As RichTextBox
    Friend WithEvents Label45 As Label
    Friend WithEvents Panel15 As Panel
    Friend WithEvents UiButton任务管理菜单 As Sunny.UI.UIButton
    Friend WithEvents TabPage支持者名单 As TabPage
    Friend WithEvents Panel顶部视觉修正区域 As Panel
    Friend WithEvents Panel顶部视觉修正区域_一级选项卡 As Panel
    Friend WithEvents Panel顶部视觉修正区域_二级选项卡 As Panel
    Friend WithEvents TabPage播放器 As TabPage

End Class
