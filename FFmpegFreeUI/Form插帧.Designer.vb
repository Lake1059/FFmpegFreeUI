<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form插帧
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
        Label62 = New Label()
        Panel8 = New Panel()
        Panel11 = New Panel()
        Label13 = New Label()
        UiTextBox要补到多少帧 = New Sunny.UI.UITextBox()
        Label1 = New Label()
        Label124 = New Label()
        Panel73 = New Panel()
        Label123 = New Label()
        Label2 = New Label()
        Panel1 = New Panel()
        Panel2 = New Panel()
        UiComboBox运动估计算法 = New Sunny.UI.UIComboBox()
        Label3 = New Label()
        UiComboBox运动估计模式 = New Sunny.UI.UIComboBox()
        Label4 = New Label()
        UiComboBox插帧模式 = New Sunny.UI.UIComboBox()
        Label5 = New Label()
        Label6 = New Label()
        Panel3 = New Panel()
        Panel4 = New Panel()
        UiCheckBox可变块大小的运动补偿 = New Sunny.UI.UICheckBox()
        Label12 = New Label()
        UiComboBox运动补偿模式 = New Sunny.UI.UIComboBox()
        Label9 = New Label()
        Label7 = New Label()
        Panel5 = New Panel()
        Panel6 = New Panel()
        UiTextBox搜索范围 = New Sunny.UI.UITextBox()
        Label10 = New Label()
        UiTextBox块大小 = New Sunny.UI.UITextBox()
        Panel78 = New Panel()
        Label134 = New Label()
        Label135 = New Label()
        Label8 = New Label()
        Panel7 = New Panel()
        Panel9 = New Panel()
        Label15 = New Label()
        UiTextBox场景变化检测强度 = New Sunny.UI.UITextBox()
        Label11 = New Label()
        Panel8.SuspendLayout()
        Panel11.SuspendLayout()
        Panel73.SuspendLayout()
        Panel1.SuspendLayout()
        Panel2.SuspendLayout()
        Panel3.SuspendLayout()
        Panel4.SuspendLayout()
        Panel5.SuspendLayout()
        Panel6.SuspendLayout()
        Panel78.SuspendLayout()
        Panel7.SuspendLayout()
        Panel9.SuspendLayout()
        SuspendLayout()
        ' 
        ' Label62
        ' 
        Label62.Dock = DockStyle.Top
        Label62.Location = New Point(20, 148)
        Label62.Name = "Label62"
        Label62.Size = New Size(694, 20)
        Label62.TabIndex = 68
        ' 
        ' Panel8
        ' 
        Panel8.BackColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        Panel8.Controls.Add(Panel11)
        Panel8.Controls.Add(Label1)
        Panel8.Dock = DockStyle.Top
        Panel8.Location = New Point(20, 98)
        Panel8.Name = "Panel8"
        Panel8.Size = New Size(694, 50)
        Panel8.TabIndex = 67
        ' 
        ' Panel11
        ' 
        Panel11.Controls.Add(Label13)
        Panel11.Controls.Add(UiTextBox要补到多少帧)
        Panel11.Dock = DockStyle.Fill
        Panel11.Location = New Point(100, 0)
        Panel11.Name = "Panel11"
        Panel11.Padding = New Padding(10, 10, 0, 10)
        Panel11.Size = New Size(594, 50)
        Panel11.TabIndex = 2
        ' 
        ' Label13
        ' 
        Label13.Dock = DockStyle.Fill
        Label13.ForeColor = Color.Gray
        Label13.Location = New Point(185, 10)
        Label13.Name = "Label13"
        Label13.Padding = New Padding(10, 0, 0, 0)
        Label13.Size = New Size(409, 30)
        Label13.TabIndex = 98
        Label13.Text = "目标帧率 或 插帧模式 留空以取消使用此滤镜"
        Label13.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' UiTextBox要补到多少帧
        ' 
        UiTextBox要补到多少帧.Dock = DockStyle.Left
        UiTextBox要补到多少帧.FillColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox要补到多少帧.FillColor2 = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox要补到多少帧.FillDisableColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox要补到多少帧.FillReadOnlyColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox要补到多少帧.Font = New Font("微软雅黑", 9.75F)
        UiTextBox要补到多少帧.ForeColor = Color.DarkGray
        UiTextBox要补到多少帧.ForeDisableColor = Color.DarkGray
        UiTextBox要补到多少帧.ForeReadOnlyColor = Color.DarkGray
        UiTextBox要补到多少帧.Location = New Point(10, 10)
        UiTextBox要补到多少帧.Margin = New Padding(4, 5, 4, 5)
        UiTextBox要补到多少帧.MinimumSize = New Size(1, 16)
        UiTextBox要补到多少帧.Name = "UiTextBox要补到多少帧"
        UiTextBox要补到多少帧.Padding = New Padding(5)
        UiTextBox要补到多少帧.Radius = 30
        UiTextBox要补到多少帧.RectColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox要补到多少帧.RectDisableColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox要补到多少帧.RectReadOnlyColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox要补到多少帧.ScrollBarBackColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiTextBox要补到多少帧.ScrollBarColor = Color.DimGray
        UiTextBox要补到多少帧.ScrollBarStyleInherited = False
        UiTextBox要补到多少帧.ShowText = False
        UiTextBox要补到多少帧.Size = New Size(175, 30)
        UiTextBox要补到多少帧.TabIndex = 97
        UiTextBox要补到多少帧.TextAlignment = ContentAlignment.MiddleLeft
        UiTextBox要补到多少帧.Watermark = "要补到多少帧"
        UiTextBox要补到多少帧.WatermarkActiveColor = Color.DimGray
        UiTextBox要补到多少帧.WatermarkColor = Color.DimGray
        ' 
        ' Label1
        ' 
        Label1.BackColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        Label1.Dock = DockStyle.Left
        Label1.Font = New Font("微软雅黑", 10F, FontStyle.Bold)
        Label1.Location = New Point(0, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(100, 50)
        Label1.TabIndex = 4
        Label1.Text = "目标帧率"
        Label1.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label124
        ' 
        Label124.Dock = DockStyle.Top
        Label124.Location = New Point(20, 78)
        Label124.Name = "Label124"
        Label124.Size = New Size(694, 20)
        Label124.TabIndex = 82
        ' 
        ' Panel73
        ' 
        Panel73.AutoSize = True
        Panel73.BackColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        Panel73.Controls.Add(Label123)
        Panel73.Dock = DockStyle.Top
        Panel73.Location = New Point(20, 20)
        Panel73.Name = "Panel73"
        Panel73.Padding = New Padding(10)
        Panel73.Size = New Size(694, 58)
        Panel73.TabIndex = 81
        ' 
        ' Label123
        ' 
        Label123.AutoSize = True
        Label123.Dock = DockStyle.Top
        Label123.Font = New Font("微软雅黑", 9.75F)
        Label123.ForeColor = Color.Gray
        Label123.Location = New Point(10, 10)
        Label123.Name = "Label123"
        Label123.Size = New Size(534, 38)
        Label123.TabIndex = 4
        Label123.Text = "minterpolate 是 ffmpeg 内置的滤镜，效果一般，只能用 CPU，高质量的补帧请用 AI 处理" & vbCrLf & "考虑使用 SVP 或者 SVFI；如需非常稳定的补帧，请用 Topaz Video AI"
        ' 
        ' Label2
        ' 
        Label2.Dock = DockStyle.Top
        Label2.Location = New Point(20, 218)
        Label2.Name = "Label2"
        Label2.Size = New Size(694, 20)
        Label2.TabIndex = 84
        ' 
        ' Panel1
        ' 
        Panel1.BackColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        Panel1.Controls.Add(Panel2)
        Panel1.Controls.Add(Label5)
        Panel1.Dock = DockStyle.Top
        Panel1.Location = New Point(20, 168)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(694, 50)
        Panel1.TabIndex = 83
        ' 
        ' Panel2
        ' 
        Panel2.Controls.Add(UiComboBox运动估计算法)
        Panel2.Controls.Add(Label3)
        Panel2.Controls.Add(UiComboBox运动估计模式)
        Panel2.Controls.Add(Label4)
        Panel2.Controls.Add(UiComboBox插帧模式)
        Panel2.Dock = DockStyle.Fill
        Panel2.Location = New Point(100, 0)
        Panel2.Name = "Panel2"
        Panel2.Padding = New Padding(10, 10, 0, 10)
        Panel2.Size = New Size(594, 50)
        Panel2.TabIndex = 2
        ' 
        ' UiComboBox运动估计算法
        ' 
        UiComboBox运动估计算法.DataSource = Nothing
        UiComboBox运动估计算法.Dock = DockStyle.Left
        UiComboBox运动估计算法.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList
        UiComboBox运动估计算法.FillColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiComboBox运动估计算法.FillColor2 = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiComboBox运动估计算法.FillDisableColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiComboBox运动估计算法.Font = New Font("微软雅黑", 9.75F)
        UiComboBox运动估计算法.ForeColor = Color.Silver
        UiComboBox运动估计算法.ForeDisableColor = Color.Silver
        UiComboBox运动估计算法.ItemFillColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiComboBox运动估计算法.ItemForeColor = Color.Silver
        UiComboBox运动估计算法.ItemHeight = 30
        UiComboBox运动估计算法.ItemHoverColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiComboBox运动估计算法.ItemRectColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiComboBox运动估计算法.Items.AddRange(New Object() {"", "穷举搜索", "三步搜索", "二维对数搜索", "新三步搜索", "四步搜索", "菱形搜索", "基于 Hexagon", "增强的预测区域", "不均匀多六边形"})
        UiComboBox运动估计算法.ItemSelectBackColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        UiComboBox运动估计算法.ItemSelectForeColor = Color.Silver
        UiComboBox运动估计算法.Location = New Point(380, 10)
        UiComboBox运动估计算法.Margin = New Padding(4, 5, 4, 5)
        UiComboBox运动估计算法.MaxDropDownItems = 17
        UiComboBox运动估计算法.MinimumSize = New Size(63, 0)
        UiComboBox运动估计算法.Name = "UiComboBox运动估计算法"
        UiComboBox运动估计算法.Padding = New Padding(0, 0, 30, 2)
        UiComboBox运动估计算法.RectColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        UiComboBox运动估计算法.RectDisableColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        UiComboBox运动估计算法.ScrollBarHandleWidth = 20
        UiComboBox运动估计算法.Size = New Size(175, 30)
        UiComboBox运动估计算法.Style = Sunny.UI.UIStyle.Custom
        UiComboBox运动估计算法.SymbolSize = 24
        UiComboBox运动估计算法.TabIndex = 86
        UiComboBox运动估计算法.TextAlignment = ContentAlignment.MiddleLeft
        UiComboBox运动估计算法.Watermark = "运动估计算法"
        ' 
        ' Label3
        ' 
        Label3.Dock = DockStyle.Left
        Label3.Location = New Point(370, 10)
        Label3.Name = "Label3"
        Label3.Size = New Size(10, 30)
        Label3.TabIndex = 85
        ' 
        ' UiComboBox运动估计模式
        ' 
        UiComboBox运动估计模式.DataSource = Nothing
        UiComboBox运动估计模式.Dock = DockStyle.Left
        UiComboBox运动估计模式.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList
        UiComboBox运动估计模式.FillColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiComboBox运动估计模式.FillColor2 = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiComboBox运动估计模式.FillDisableColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiComboBox运动估计模式.Font = New Font("微软雅黑", 9.75F)
        UiComboBox运动估计模式.ForeColor = Color.Silver
        UiComboBox运动估计模式.ForeDisableColor = Color.Silver
        UiComboBox运动估计模式.ItemFillColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiComboBox运动估计模式.ItemForeColor = Color.Silver
        UiComboBox运动估计模式.ItemHeight = 30
        UiComboBox运动估计模式.ItemHoverColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiComboBox运动估计模式.ItemRectColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiComboBox运动估计模式.Items.AddRange(New Object() {"", "双向运动估计", "双侧运动估计"})
        UiComboBox运动估计模式.ItemSelectBackColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        UiComboBox运动估计模式.ItemSelectForeColor = Color.Silver
        UiComboBox运动估计模式.Location = New Point(195, 10)
        UiComboBox运动估计模式.Margin = New Padding(4, 5, 4, 5)
        UiComboBox运动估计模式.MaxDropDownItems = 17
        UiComboBox运动估计模式.MinimumSize = New Size(63, 0)
        UiComboBox运动估计模式.Name = "UiComboBox运动估计模式"
        UiComboBox运动估计模式.Padding = New Padding(0, 0, 30, 2)
        UiComboBox运动估计模式.RectColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        UiComboBox运动估计模式.RectDisableColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        UiComboBox运动估计模式.ScrollBarHandleWidth = 20
        UiComboBox运动估计模式.Size = New Size(175, 30)
        UiComboBox运动估计模式.Style = Sunny.UI.UIStyle.Custom
        UiComboBox运动估计模式.SymbolSize = 24
        UiComboBox运动估计模式.TabIndex = 84
        UiComboBox运动估计模式.TextAlignment = ContentAlignment.MiddleLeft
        UiComboBox运动估计模式.Watermark = "运动估计模式"
        ' 
        ' Label4
        ' 
        Label4.Dock = DockStyle.Left
        Label4.Location = New Point(185, 10)
        Label4.Name = "Label4"
        Label4.Size = New Size(10, 30)
        Label4.TabIndex = 83
        ' 
        ' UiComboBox插帧模式
        ' 
        UiComboBox插帧模式.DataSource = Nothing
        UiComboBox插帧模式.Dock = DockStyle.Left
        UiComboBox插帧模式.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList
        UiComboBox插帧模式.FillColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiComboBox插帧模式.FillColor2 = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiComboBox插帧模式.FillDisableColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiComboBox插帧模式.Font = New Font("微软雅黑", 9.75F)
        UiComboBox插帧模式.ForeColor = Color.Silver
        UiComboBox插帧模式.ForeDisableColor = Color.Silver
        UiComboBox插帧模式.ItemFillColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiComboBox插帧模式.ItemForeColor = Color.Silver
        UiComboBox插帧模式.ItemHeight = 30
        UiComboBox插帧模式.ItemHoverColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiComboBox插帧模式.ItemRectColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiComboBox插帧模式.Items.AddRange(New Object() {"", "两帧加权平均", "运动补偿插值"})
        UiComboBox插帧模式.ItemSelectBackColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        UiComboBox插帧模式.ItemSelectForeColor = Color.Silver
        UiComboBox插帧模式.Location = New Point(10, 10)
        UiComboBox插帧模式.Margin = New Padding(4, 5, 4, 5)
        UiComboBox插帧模式.MaxDropDownItems = 17
        UiComboBox插帧模式.MinimumSize = New Size(63, 0)
        UiComboBox插帧模式.Name = "UiComboBox插帧模式"
        UiComboBox插帧模式.Padding = New Padding(0, 0, 30, 2)
        UiComboBox插帧模式.RectColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        UiComboBox插帧模式.RectDisableColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        UiComboBox插帧模式.ScrollBarHandleWidth = 20
        UiComboBox插帧模式.Size = New Size(175, 30)
        UiComboBox插帧模式.Style = Sunny.UI.UIStyle.Custom
        UiComboBox插帧模式.SymbolSize = 24
        UiComboBox插帧模式.TabIndex = 87
        UiComboBox插帧模式.TextAlignment = ContentAlignment.MiddleLeft
        UiComboBox插帧模式.Watermark = "插帧模式"
        ' 
        ' Label5
        ' 
        Label5.BackColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        Label5.Dock = DockStyle.Left
        Label5.Font = New Font("微软雅黑", 10F, FontStyle.Bold)
        Label5.Location = New Point(0, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(100, 50)
        Label5.TabIndex = 4
        Label5.Text = "算法模式"
        Label5.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label6
        ' 
        Label6.Dock = DockStyle.Top
        Label6.Location = New Point(20, 288)
        Label6.Name = "Label6"
        Label6.Size = New Size(694, 20)
        Label6.TabIndex = 86
        ' 
        ' Panel3
        ' 
        Panel3.BackColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        Panel3.Controls.Add(Panel4)
        Panel3.Controls.Add(Label9)
        Panel3.Dock = DockStyle.Top
        Panel3.Location = New Point(20, 238)
        Panel3.Name = "Panel3"
        Panel3.Size = New Size(694, 50)
        Panel3.TabIndex = 85
        ' 
        ' Panel4
        ' 
        Panel4.Controls.Add(UiCheckBox可变块大小的运动补偿)
        Panel4.Controls.Add(Label12)
        Panel4.Controls.Add(UiComboBox运动补偿模式)
        Panel4.Dock = DockStyle.Fill
        Panel4.Location = New Point(100, 0)
        Panel4.Name = "Panel4"
        Panel4.Padding = New Padding(10, 10, 0, 10)
        Panel4.Size = New Size(594, 50)
        Panel4.TabIndex = 2
        ' 
        ' UiCheckBox可变块大小的运动补偿
        ' 
        UiCheckBox可变块大小的运动补偿.CheckBoxColor = Color.Silver
        UiCheckBox可变块大小的运动补偿.CheckBoxSize = 20
        UiCheckBox可变块大小的运动补偿.Dock = DockStyle.Left
        UiCheckBox可变块大小的运动补偿.Font = New Font("微软雅黑", 9.75F)
        UiCheckBox可变块大小的运动补偿.ForeColor = Color.Silver
        UiCheckBox可变块大小的运动补偿.Location = New Point(195, 10)
        UiCheckBox可变块大小的运动补偿.MinimumSize = New Size(1, 1)
        UiCheckBox可变块大小的运动补偿.Name = "UiCheckBox可变块大小的运动补偿"
        UiCheckBox可变块大小的运动补偿.Size = New Size(175, 30)
        UiCheckBox可变块大小的运动补偿.TabIndex = 100
        UiCheckBox可变块大小的运动补偿.Text = "可变块大小的运动补偿"
        ' 
        ' Label12
        ' 
        Label12.Dock = DockStyle.Left
        Label12.Location = New Point(185, 10)
        Label12.Name = "Label12"
        Label12.Size = New Size(10, 30)
        Label12.TabIndex = 84
        ' 
        ' UiComboBox运动补偿模式
        ' 
        UiComboBox运动补偿模式.DataSource = Nothing
        UiComboBox运动补偿模式.Dock = DockStyle.Left
        UiComboBox运动补偿模式.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList
        UiComboBox运动补偿模式.FillColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiComboBox运动补偿模式.FillColor2 = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiComboBox运动补偿模式.FillDisableColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiComboBox运动补偿模式.Font = New Font("微软雅黑", 9.75F)
        UiComboBox运动补偿模式.ForeColor = Color.Silver
        UiComboBox运动补偿模式.ForeDisableColor = Color.Silver
        UiComboBox运动补偿模式.ItemFillColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiComboBox运动补偿模式.ItemForeColor = Color.Silver
        UiComboBox运动补偿模式.ItemHeight = 30
        UiComboBox运动补偿模式.ItemHoverColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiComboBox运动补偿模式.ItemRectColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiComboBox运动补偿模式.Items.AddRange(New Object() {"", "重叠块运动补偿", "加权 obmc"})
        UiComboBox运动补偿模式.ItemSelectBackColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        UiComboBox运动补偿模式.ItemSelectForeColor = Color.Silver
        UiComboBox运动补偿模式.Location = New Point(10, 10)
        UiComboBox运动补偿模式.Margin = New Padding(4, 5, 4, 5)
        UiComboBox运动补偿模式.MaxDropDownItems = 17
        UiComboBox运动补偿模式.MinimumSize = New Size(63, 0)
        UiComboBox运动补偿模式.Name = "UiComboBox运动补偿模式"
        UiComboBox运动补偿模式.Padding = New Padding(0, 0, 30, 2)
        UiComboBox运动补偿模式.RectColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        UiComboBox运动补偿模式.RectDisableColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        UiComboBox运动补偿模式.ScrollBarHandleWidth = 20
        UiComboBox运动补偿模式.Size = New Size(175, 30)
        UiComboBox运动补偿模式.Style = Sunny.UI.UIStyle.Custom
        UiComboBox运动补偿模式.SymbolSize = 24
        UiComboBox运动补偿模式.TabIndex = 82
        UiComboBox运动补偿模式.TextAlignment = ContentAlignment.MiddleLeft
        UiComboBox运动补偿模式.Watermark = "运动补偿模式"
        ' 
        ' Label9
        ' 
        Label9.BackColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        Label9.Dock = DockStyle.Left
        Label9.Font = New Font("微软雅黑", 10F, FontStyle.Bold)
        Label9.Location = New Point(0, 0)
        Label9.Name = "Label9"
        Label9.Size = New Size(100, 50)
        Label9.TabIndex = 4
        Label9.Text = "补偿模式"
        Label9.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label7
        ' 
        Label7.Dock = DockStyle.Top
        Label7.Location = New Point(20, 383)
        Label7.Name = "Label7"
        Label7.Size = New Size(694, 20)
        Label7.TabIndex = 88
        ' 
        ' Panel5
        ' 
        Panel5.AutoSize = True
        Panel5.BackColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        Panel5.Controls.Add(Panel6)
        Panel5.Controls.Add(Panel78)
        Panel5.Controls.Add(Label8)
        Panel5.Dock = DockStyle.Top
        Panel5.Location = New Point(20, 308)
        Panel5.Name = "Panel5"
        Panel5.Size = New Size(694, 75)
        Panel5.TabIndex = 87
        ' 
        ' Panel6
        ' 
        Panel6.Controls.Add(UiTextBox搜索范围)
        Panel6.Controls.Add(Label10)
        Panel6.Controls.Add(UiTextBox块大小)
        Panel6.Dock = DockStyle.Top
        Panel6.Location = New Point(100, 35)
        Panel6.Name = "Panel6"
        Panel6.Padding = New Padding(10, 0, 0, 10)
        Panel6.Size = New Size(594, 40)
        Panel6.TabIndex = 2
        ' 
        ' UiTextBox搜索范围
        ' 
        UiTextBox搜索范围.Dock = DockStyle.Left
        UiTextBox搜索范围.FillColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox搜索范围.FillColor2 = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox搜索范围.FillDisableColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox搜索范围.FillReadOnlyColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox搜索范围.Font = New Font("微软雅黑", 9.75F)
        UiTextBox搜索范围.ForeColor = Color.DarkGray
        UiTextBox搜索范围.ForeDisableColor = Color.DarkGray
        UiTextBox搜索范围.ForeReadOnlyColor = Color.DarkGray
        UiTextBox搜索范围.Location = New Point(195, 0)
        UiTextBox搜索范围.Margin = New Padding(4, 5, 4, 5)
        UiTextBox搜索范围.MinimumSize = New Size(1, 16)
        UiTextBox搜索范围.Name = "UiTextBox搜索范围"
        UiTextBox搜索范围.Padding = New Padding(5)
        UiTextBox搜索范围.Radius = 30
        UiTextBox搜索范围.RectColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox搜索范围.RectDisableColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox搜索范围.RectReadOnlyColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox搜索范围.ScrollBarBackColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiTextBox搜索范围.ScrollBarColor = Color.DimGray
        UiTextBox搜索范围.ScrollBarStyleInherited = False
        UiTextBox搜索范围.ShowText = False
        UiTextBox搜索范围.Size = New Size(175, 30)
        UiTextBox搜索范围.TabIndex = 100
        UiTextBox搜索范围.TextAlignment = ContentAlignment.MiddleLeft
        UiTextBox搜索范围.Watermark = "默认 32"
        UiTextBox搜索范围.WatermarkActiveColor = Color.DimGray
        UiTextBox搜索范围.WatermarkColor = Color.DimGray
        ' 
        ' Label10
        ' 
        Label10.Dock = DockStyle.Left
        Label10.Location = New Point(185, 0)
        Label10.Name = "Label10"
        Label10.Size = New Size(10, 30)
        Label10.TabIndex = 99
        ' 
        ' UiTextBox块大小
        ' 
        UiTextBox块大小.Dock = DockStyle.Left
        UiTextBox块大小.FillColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox块大小.FillColor2 = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox块大小.FillDisableColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox块大小.FillReadOnlyColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox块大小.Font = New Font("微软雅黑", 9.75F)
        UiTextBox块大小.ForeColor = Color.DarkGray
        UiTextBox块大小.ForeDisableColor = Color.DarkGray
        UiTextBox块大小.ForeReadOnlyColor = Color.DarkGray
        UiTextBox块大小.Location = New Point(10, 0)
        UiTextBox块大小.Margin = New Padding(4, 5, 4, 5)
        UiTextBox块大小.MinimumSize = New Size(1, 16)
        UiTextBox块大小.Name = "UiTextBox块大小"
        UiTextBox块大小.Padding = New Padding(5)
        UiTextBox块大小.Radius = 30
        UiTextBox块大小.RectColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox块大小.RectDisableColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox块大小.RectReadOnlyColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox块大小.ScrollBarBackColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiTextBox块大小.ScrollBarColor = Color.DimGray
        UiTextBox块大小.ScrollBarStyleInherited = False
        UiTextBox块大小.ShowText = False
        UiTextBox块大小.Size = New Size(175, 30)
        UiTextBox块大小.TabIndex = 98
        UiTextBox块大小.TextAlignment = ContentAlignment.MiddleLeft
        UiTextBox块大小.Watermark = "默认 16"
        UiTextBox块大小.WatermarkActiveColor = Color.DimGray
        UiTextBox块大小.WatermarkColor = Color.DimGray
        ' 
        ' Panel78
        ' 
        Panel78.Controls.Add(Label134)
        Panel78.Controls.Add(Label135)
        Panel78.Dock = DockStyle.Top
        Panel78.Location = New Point(100, 0)
        Panel78.Name = "Panel78"
        Panel78.Padding = New Padding(7, 0, 0, 0)
        Panel78.Size = New Size(594, 35)
        Panel78.TabIndex = 5
        ' 
        ' Label134
        ' 
        Label134.Dock = DockStyle.Left
        Label134.Font = New Font("微软雅黑", 9.75F)
        Label134.Location = New Point(192, 0)
        Label134.Name = "Label134"
        Label134.Size = New Size(185, 35)
        Label134.TabIndex = 79
        Label134.Text = "搜索范围（像素）"
        Label134.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label135
        ' 
        Label135.Dock = DockStyle.Left
        Label135.Font = New Font("微软雅黑", 9.75F)
        Label135.Location = New Point(7, 0)
        Label135.Name = "Label135"
        Label135.Size = New Size(185, 35)
        Label135.TabIndex = 73
        Label135.Text = "块大小"
        Label135.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label8
        ' 
        Label8.BackColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        Label8.Dock = DockStyle.Left
        Label8.Font = New Font("微软雅黑", 10F, FontStyle.Bold)
        Label8.Location = New Point(0, 0)
        Label8.Name = "Label8"
        Label8.Size = New Size(100, 75)
        Label8.TabIndex = 4
        Label8.Text = "识别"
        Label8.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Panel7
        ' 
        Panel7.BackColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        Panel7.Controls.Add(Panel9)
        Panel7.Controls.Add(Label11)
        Panel7.Dock = DockStyle.Top
        Panel7.Location = New Point(20, 403)
        Panel7.Name = "Panel7"
        Panel7.Size = New Size(694, 50)
        Panel7.TabIndex = 89
        ' 
        ' Panel9
        ' 
        Panel9.Controls.Add(Label15)
        Panel9.Controls.Add(UiTextBox场景变化检测强度)
        Panel9.Dock = DockStyle.Fill
        Panel9.Location = New Point(100, 0)
        Panel9.Name = "Panel9"
        Panel9.Padding = New Padding(10, 10, 0, 10)
        Panel9.Size = New Size(594, 50)
        Panel9.TabIndex = 2
        ' 
        ' Label15
        ' 
        Label15.Dock = DockStyle.Fill
        Label15.ForeColor = Color.Gray
        Label15.Location = New Point(185, 10)
        Label15.Name = "Label15"
        Label15.Padding = New Padding(10, 0, 0, 0)
        Label15.Size = New Size(409, 30)
        Label15.TabIndex = 105
        Label15.Text = "留空以取消使用此参数"
        Label15.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' UiTextBox场景变化检测强度
        ' 
        UiTextBox场景变化检测强度.Dock = DockStyle.Left
        UiTextBox场景变化检测强度.FillColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox场景变化检测强度.FillColor2 = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox场景变化检测强度.FillDisableColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox场景变化检测强度.FillReadOnlyColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox场景变化检测强度.Font = New Font("微软雅黑", 9.75F)
        UiTextBox场景变化检测强度.ForeColor = Color.DarkGray
        UiTextBox场景变化检测强度.ForeDisableColor = Color.DarkGray
        UiTextBox场景变化检测强度.ForeReadOnlyColor = Color.DarkGray
        UiTextBox场景变化检测强度.Location = New Point(10, 10)
        UiTextBox场景变化检测强度.Margin = New Padding(4, 5, 4, 5)
        UiTextBox场景变化检测强度.MinimumSize = New Size(1, 16)
        UiTextBox场景变化检测强度.Name = "UiTextBox场景变化检测强度"
        UiTextBox场景变化检测强度.Padding = New Padding(5)
        UiTextBox场景变化检测强度.Radius = 30
        UiTextBox场景变化检测强度.RectColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox场景变化检测强度.RectDisableColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox场景变化检测强度.RectReadOnlyColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox场景变化检测强度.ScrollBarBackColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiTextBox场景变化检测强度.ScrollBarColor = Color.DimGray
        UiTextBox场景变化检测强度.ScrollBarStyleInherited = False
        UiTextBox场景变化检测强度.ShowText = False
        UiTextBox场景变化检测强度.Size = New Size(175, 30)
        UiTextBox场景变化检测强度.TabIndex = 103
        UiTextBox场景变化检测强度.TextAlignment = ContentAlignment.MiddleLeft
        UiTextBox场景变化检测强度.Watermark = "默认 10"
        UiTextBox场景变化检测强度.WatermarkActiveColor = Color.DimGray
        UiTextBox场景变化检测强度.WatermarkColor = Color.DimGray
        ' 
        ' Label11
        ' 
        Label11.BackColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        Label11.Dock = DockStyle.Left
        Label11.Font = New Font("微软雅黑", 10F, FontStyle.Bold)
        Label11.Location = New Point(0, 0)
        Label11.Name = "Label11"
        Label11.Size = New Size(100, 50)
        Label11.TabIndex = 4
        Label11.Text = "场景变化"
        Label11.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Form插帧
        ' 
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ClientSize = New Size(734, 476)
        Controls.Add(Panel7)
        Controls.Add(Label7)
        Controls.Add(Panel5)
        Controls.Add(Label6)
        Controls.Add(Panel3)
        Controls.Add(Label2)
        Controls.Add(Panel1)
        Controls.Add(Label62)
        Controls.Add(Panel8)
        Controls.Add(Label124)
        Controls.Add(Panel73)
        Font = New Font("微软雅黑", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        ForeColor = Color.Silver
        MaximizeBox = False
        MinimizeBox = False
        MinimumSize = New Size(750, 515)
        Name = "Form插帧"
        Padding = New Padding(20)
        ShowIcon = False
        ShowInTaskbar = False
        StartPosition = FormStartPosition.Manual
        Text = "minterpolate 插帧滤镜"
        Panel8.ResumeLayout(False)
        Panel11.ResumeLayout(False)
        Panel73.ResumeLayout(False)
        Panel73.PerformLayout()
        Panel1.ResumeLayout(False)
        Panel2.ResumeLayout(False)
        Panel3.ResumeLayout(False)
        Panel4.ResumeLayout(False)
        Panel5.ResumeLayout(False)
        Panel6.ResumeLayout(False)
        Panel78.ResumeLayout(False)
        Panel7.ResumeLayout(False)
        Panel9.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label62 As Label
    Friend WithEvents Panel8 As Panel
    Friend WithEvents Panel11 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents Label124 As Label
    Friend WithEvents Panel73 As Panel
    Friend WithEvents Label123 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents UiComboBox运动估计算法 As Sunny.UI.UIComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents UiComboBox运动估计模式 As Sunny.UI.UIComboBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents UiComboBox运动补偿模式 As Sunny.UI.UIComboBox
    Friend WithEvents Label9 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Label8 As Label
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Panel9 As Panel
    Friend WithEvents Label11 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents UiTextBox要补到多少帧 As Sunny.UI.UITextBox
    Friend WithEvents UiCheckBox可变块大小的运动补偿 As Sunny.UI.UICheckBox
    Friend WithEvents UiTextBox场景变化检测强度 As Sunny.UI.UITextBox
    Friend WithEvents UiComboBox插帧模式 As Sunny.UI.UIComboBox
    Friend WithEvents Panel78 As Panel
    Friend WithEvents Label134 As Label
    Friend WithEvents Label135 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents UiTextBox搜索范围 As Sunny.UI.UITextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents UiTextBox块大小 As Sunny.UI.UITextBox
End Class
