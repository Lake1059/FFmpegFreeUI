<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form超分
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
        Panel73 = New Panel()
        Label123 = New Label()
        Panel8 = New Panel()
        Panel11 = New Panel()
        Label13 = New Label()
        UiTextBox超分高度 = New Sunny.UI.UITextBox()
        Label4 = New Label()
        UiTextBox超分宽度 = New Sunny.UI.UITextBox()
        Label1 = New Label()
        Label124 = New Label()
        Label2 = New Label()
        Panel1 = New Panel()
        Panel2 = New Panel()
        Label8 = New Label()
        UiComboBox下采样算法 = New Sunny.UI.UIComboBox()
        Label5 = New Label()
        UiComboBox上采样算法 = New Sunny.UI.UIComboBox()
        Label6 = New Label()
        Label3 = New Label()
        Panel3 = New Panel()
        Panel4 = New Panel()
        Label7 = New Label()
        UiTextBox抗振铃强度 = New Sunny.UI.UITextBox()
        Label9 = New Label()
        Panel5 = New Panel()
        Panel6 = New Panel()
        Panel82 = New Panel()
        ListView1 = New ListView()
        ColumnHeader1 = New ColumnHeader()
        Panel23 = New Panel()
        Label14 = New Label()
        UiButton下载 = New Sunny.UI.UIButton()
        UiButton下移着色器 = New Sunny.UI.UIButton()
        Label12 = New Label()
        UiButton上移着色器 = New Sunny.UI.UIButton()
        Label82 = New Label()
        UiButton移除着色器 = New Sunny.UI.UIButton()
        Label145 = New Label()
        UiButton添加着色器 = New Sunny.UI.UIButton()
        Label10 = New Label()
        Label11 = New Label()
        Panel73.SuspendLayout()
        Panel8.SuspendLayout()
        Panel11.SuspendLayout()
        Panel1.SuspendLayout()
        Panel2.SuspendLayout()
        Panel3.SuspendLayout()
        Panel4.SuspendLayout()
        Panel5.SuspendLayout()
        Panel6.SuspendLayout()
        Panel82.SuspendLayout()
        Panel23.SuspendLayout()
        SuspendLayout()
        ' 
        ' Panel73
        ' 
        Panel73.AutoSize = True
        Panel73.BackColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        Panel73.Controls.Add(Label123)
        Panel73.Dock = DockStyle.Top
        Panel73.Location = New Point(10, 10)
        Panel73.Name = "Panel73"
        Panel73.Padding = New Padding(10)
        Panel73.Size = New Size(664, 58)
        Panel73.TabIndex = 82
        ' 
        ' Label123
        ' 
        Label123.AutoSize = True
        Label123.Dock = DockStyle.Top
        Label123.Font = New Font("微软雅黑", 9.75F)
        Label123.ForeColor = Color.Gray
        Label123.Location = New Point(10, 10)
        Label123.Name = "Label123"
        Label123.Size = New Size(480, 38)
        Label123.TabIndex = 4
        Label123.Text = "libplacebo 需要受支持的 GPU，过早的显卡型号可能无法运行" & vbCrLf & "直接在这里指定分辨率，不要在主参数面板上指定，可以用 iw*2 和 ih*2 表示倍数"
        ' 
        ' Panel8
        ' 
        Panel8.BackColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        Panel8.Controls.Add(Panel11)
        Panel8.Controls.Add(Label1)
        Panel8.Dock = DockStyle.Top
        Panel8.Location = New Point(10, 78)
        Panel8.Name = "Panel8"
        Panel8.Size = New Size(664, 50)
        Panel8.TabIndex = 83
        ' 
        ' Panel11
        ' 
        Panel11.Controls.Add(Label13)
        Panel11.Controls.Add(UiTextBox超分高度)
        Panel11.Controls.Add(Label4)
        Panel11.Controls.Add(UiTextBox超分宽度)
        Panel11.Dock = DockStyle.Fill
        Panel11.Location = New Point(100, 0)
        Panel11.Name = "Panel11"
        Panel11.Padding = New Padding(10, 10, 0, 10)
        Panel11.Size = New Size(564, 50)
        Panel11.TabIndex = 2
        ' 
        ' Label13
        ' 
        Label13.Dock = DockStyle.Fill
        Label13.ForeColor = Color.Gray
        Label13.Location = New Point(185, 10)
        Label13.Name = "Label13"
        Label13.Padding = New Padding(10, 0, 0, 0)
        Label13.Size = New Size(379, 30)
        Label13.TabIndex = 98
        Label13.Text = "任一或两者留空以取消使用此滤镜"
        Label13.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' UiTextBox超分高度
        ' 
        UiTextBox超分高度.Dock = DockStyle.Left
        UiTextBox超分高度.FillColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox超分高度.FillColor2 = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox超分高度.FillDisableColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox超分高度.FillReadOnlyColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox超分高度.Font = New Font("微软雅黑", 9.75F)
        UiTextBox超分高度.ForeColor = Color.DarkGray
        UiTextBox超分高度.ForeDisableColor = Color.DarkGray
        UiTextBox超分高度.ForeReadOnlyColor = Color.DarkGray
        UiTextBox超分高度.Location = New Point(102, 10)
        UiTextBox超分高度.Margin = New Padding(4, 5, 4, 5)
        UiTextBox超分高度.MinimumSize = New Size(1, 16)
        UiTextBox超分高度.Name = "UiTextBox超分高度"
        UiTextBox超分高度.Padding = New Padding(5)
        UiTextBox超分高度.Radius = 30
        UiTextBox超分高度.RectColor = Color.DimGray
        UiTextBox超分高度.RectDisableColor = Color.DimGray
        UiTextBox超分高度.RectReadOnlyColor = Color.DimGray
        UiTextBox超分高度.ScrollBarBackColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiTextBox超分高度.ScrollBarColor = Color.DimGray
        UiTextBox超分高度.ScrollBarStyleInherited = False
        UiTextBox超分高度.ShowText = False
        UiTextBox超分高度.Size = New Size(83, 30)
        UiTextBox超分高度.TabIndex = 99
        UiTextBox超分高度.TextAlignment = ContentAlignment.MiddleLeft
        UiTextBox超分高度.Watermark = "高度"
        UiTextBox超分高度.WatermarkActiveColor = Color.DimGray
        UiTextBox超分高度.WatermarkColor = Color.DimGray
        ' 
        ' Label4
        ' 
        Label4.Dock = DockStyle.Left
        Label4.Location = New Point(93, 10)
        Label4.Name = "Label4"
        Label4.Size = New Size(9, 30)
        Label4.TabIndex = 100
        ' 
        ' UiTextBox超分宽度
        ' 
        UiTextBox超分宽度.Dock = DockStyle.Left
        UiTextBox超分宽度.FillColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox超分宽度.FillColor2 = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox超分宽度.FillDisableColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox超分宽度.FillReadOnlyColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox超分宽度.Font = New Font("微软雅黑", 9.75F)
        UiTextBox超分宽度.ForeColor = Color.DarkGray
        UiTextBox超分宽度.ForeDisableColor = Color.DarkGray
        UiTextBox超分宽度.ForeReadOnlyColor = Color.DarkGray
        UiTextBox超分宽度.Location = New Point(10, 10)
        UiTextBox超分宽度.Margin = New Padding(4, 5, 4, 5)
        UiTextBox超分宽度.MinimumSize = New Size(1, 16)
        UiTextBox超分宽度.Name = "UiTextBox超分宽度"
        UiTextBox超分宽度.Padding = New Padding(5)
        UiTextBox超分宽度.Radius = 30
        UiTextBox超分宽度.RectColor = Color.DimGray
        UiTextBox超分宽度.RectDisableColor = Color.DimGray
        UiTextBox超分宽度.RectReadOnlyColor = Color.DimGray
        UiTextBox超分宽度.ScrollBarBackColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiTextBox超分宽度.ScrollBarColor = Color.DimGray
        UiTextBox超分宽度.ScrollBarStyleInherited = False
        UiTextBox超分宽度.ShowText = False
        UiTextBox超分宽度.Size = New Size(83, 30)
        UiTextBox超分宽度.TabIndex = 97
        UiTextBox超分宽度.TextAlignment = ContentAlignment.MiddleLeft
        UiTextBox超分宽度.Watermark = "宽度"
        UiTextBox超分宽度.WatermarkActiveColor = Color.DimGray
        UiTextBox超分宽度.WatermarkColor = Color.DimGray
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
        Label1.Text = "目标分辨率"
        Label1.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label124
        ' 
        Label124.Dock = DockStyle.Top
        Label124.Location = New Point(10, 68)
        Label124.Name = "Label124"
        Label124.Size = New Size(664, 10)
        Label124.TabIndex = 84
        ' 
        ' Label2
        ' 
        Label2.Dock = DockStyle.Top
        Label2.Location = New Point(10, 128)
        Label2.Name = "Label2"
        Label2.Size = New Size(664, 10)
        Label2.TabIndex = 85
        ' 
        ' Panel1
        ' 
        Panel1.BackColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        Panel1.Controls.Add(Panel2)
        Panel1.Controls.Add(Label6)
        Panel1.Dock = DockStyle.Top
        Panel1.Location = New Point(10, 138)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(664, 50)
        Panel1.TabIndex = 86
        ' 
        ' Panel2
        ' 
        Panel2.Controls.Add(Label8)
        Panel2.Controls.Add(UiComboBox下采样算法)
        Panel2.Controls.Add(Label5)
        Panel2.Controls.Add(UiComboBox上采样算法)
        Panel2.Dock = DockStyle.Fill
        Panel2.Location = New Point(100, 0)
        Panel2.Name = "Panel2"
        Panel2.Padding = New Padding(10, 10, 0, 10)
        Panel2.Size = New Size(564, 50)
        Panel2.TabIndex = 2
        ' 
        ' Label8
        ' 
        Label8.Dock = DockStyle.Fill
        Label8.ForeColor = Color.Gray
        Label8.Location = New Point(370, 10)
        Label8.Name = "Label8"
        Label8.Padding = New Padding(10, 0, 0, 0)
        Label8.Size = New Size(194, 30)
        Label8.TabIndex = 99
        Label8.Text = "建议用自定义着色器"
        Label8.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' UiComboBox下采样算法
        ' 
        UiComboBox下采样算法.DataSource = Nothing
        UiComboBox下采样算法.Dock = DockStyle.Left
        UiComboBox下采样算法.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList
        UiComboBox下采样算法.FillColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiComboBox下采样算法.FillColor2 = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiComboBox下采样算法.FillDisableColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiComboBox下采样算法.Font = New Font("微软雅黑", 9.75F)
        UiComboBox下采样算法.ForeColor = Color.Silver
        UiComboBox下采样算法.ForeDisableColor = Color.Silver
        UiComboBox下采样算法.ItemFillColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiComboBox下采样算法.ItemForeColor = Color.Silver
        UiComboBox下采样算法.ItemHeight = 28
        UiComboBox下采样算法.ItemHoverColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiComboBox下采样算法.ItemRectColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiComboBox下采样算法.Items.AddRange(New Object() {"", "ewa_lanczos", "spline36", "mitchell", "hermite"})
        UiComboBox下采样算法.ItemSelectBackColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        UiComboBox下采样算法.ItemSelectForeColor = Color.Silver
        UiComboBox下采样算法.Location = New Point(195, 10)
        UiComboBox下采样算法.Margin = New Padding(4, 5, 4, 5)
        UiComboBox下采样算法.MaxDropDownItems = 17
        UiComboBox下采样算法.MinimumSize = New Size(63, 0)
        UiComboBox下采样算法.Name = "UiComboBox下采样算法"
        UiComboBox下采样算法.Padding = New Padding(0, 0, 30, 2)
        UiComboBox下采样算法.RadiusSides = Sunny.UI.UICornerRadiusSides.None
        UiComboBox下采样算法.RectColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        UiComboBox下采样算法.RectDisableColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        UiComboBox下采样算法.ScrollBarHandleWidth = 20
        UiComboBox下采样算法.Size = New Size(175, 30)
        UiComboBox下采样算法.Style = Sunny.UI.UIStyle.Custom
        UiComboBox下采样算法.SymbolSize = 24
        UiComboBox下采样算法.TabIndex = 84
        UiComboBox下采样算法.TextAlignment = ContentAlignment.MiddleLeft
        UiComboBox下采样算法.Watermark = "下采样算法 (缩小用)"
        ' 
        ' Label5
        ' 
        Label5.Dock = DockStyle.Left
        Label5.Location = New Point(185, 10)
        Label5.Name = "Label5"
        Label5.Size = New Size(10, 30)
        Label5.TabIndex = 83
        ' 
        ' UiComboBox上采样算法
        ' 
        UiComboBox上采样算法.DataSource = Nothing
        UiComboBox上采样算法.Dock = DockStyle.Left
        UiComboBox上采样算法.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList
        UiComboBox上采样算法.FillColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiComboBox上采样算法.FillColor2 = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiComboBox上采样算法.FillDisableColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiComboBox上采样算法.Font = New Font("微软雅黑", 9.75F)
        UiComboBox上采样算法.ForeColor = Color.Silver
        UiComboBox上采样算法.ForeDisableColor = Color.Silver
        UiComboBox上采样算法.ItemFillColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiComboBox上采样算法.ItemForeColor = Color.Silver
        UiComboBox上采样算法.ItemHeight = 28
        UiComboBox上采样算法.ItemHoverColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiComboBox上采样算法.ItemRectColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiComboBox上采样算法.Items.AddRange(New Object() {"", "ewa_lanczos", "spline36", "mitchell", "hermite"})
        UiComboBox上采样算法.ItemSelectBackColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        UiComboBox上采样算法.ItemSelectForeColor = Color.Silver
        UiComboBox上采样算法.Location = New Point(10, 10)
        UiComboBox上采样算法.Margin = New Padding(4, 5, 4, 5)
        UiComboBox上采样算法.MaxDropDownItems = 17
        UiComboBox上采样算法.MinimumSize = New Size(63, 0)
        UiComboBox上采样算法.Name = "UiComboBox上采样算法"
        UiComboBox上采样算法.Padding = New Padding(0, 0, 30, 2)
        UiComboBox上采样算法.RadiusSides = Sunny.UI.UICornerRadiusSides.None
        UiComboBox上采样算法.RectColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        UiComboBox上采样算法.RectDisableColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        UiComboBox上采样算法.ScrollBarHandleWidth = 20
        UiComboBox上采样算法.Size = New Size(175, 30)
        UiComboBox上采样算法.Style = Sunny.UI.UIStyle.Custom
        UiComboBox上采样算法.SymbolSize = 24
        UiComboBox上采样算法.TabIndex = 87
        UiComboBox上采样算法.TextAlignment = ContentAlignment.MiddleLeft
        UiComboBox上采样算法.Watermark = "上采样算法 (放大用)"
        ' 
        ' Label6
        ' 
        Label6.BackColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        Label6.Dock = DockStyle.Left
        Label6.Font = New Font("微软雅黑", 10F, FontStyle.Bold)
        Label6.Location = New Point(0, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(100, 50)
        Label6.TabIndex = 4
        Label6.Text = "采样算法"
        Label6.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label3
        ' 
        Label3.Dock = DockStyle.Top
        Label3.Location = New Point(10, 188)
        Label3.Name = "Label3"
        Label3.Size = New Size(664, 10)
        Label3.TabIndex = 87
        ' 
        ' Panel3
        ' 
        Panel3.BackColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        Panel3.Controls.Add(Panel4)
        Panel3.Controls.Add(Label9)
        Panel3.Dock = DockStyle.Top
        Panel3.Location = New Point(10, 198)
        Panel3.Name = "Panel3"
        Panel3.Size = New Size(664, 50)
        Panel3.TabIndex = 88
        ' 
        ' Panel4
        ' 
        Panel4.Controls.Add(Label7)
        Panel4.Controls.Add(UiTextBox抗振铃强度)
        Panel4.Dock = DockStyle.Fill
        Panel4.Location = New Point(100, 0)
        Panel4.Name = "Panel4"
        Panel4.Padding = New Padding(10, 10, 0, 10)
        Panel4.Size = New Size(564, 50)
        Panel4.TabIndex = 2
        ' 
        ' Label7
        ' 
        Label7.Dock = DockStyle.Fill
        Label7.ForeColor = Color.Gray
        Label7.Location = New Point(91, 10)
        Label7.Name = "Label7"
        Label7.Padding = New Padding(10, 0, 0, 0)
        Label7.Size = New Size(473, 30)
        Label7.TabIndex = 98
        Label7.Text = "[可选] 范围 0.0 ~ 1.0"
        Label7.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' UiTextBox抗振铃强度
        ' 
        UiTextBox抗振铃强度.Dock = DockStyle.Left
        UiTextBox抗振铃强度.FillColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox抗振铃强度.FillColor2 = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox抗振铃强度.FillDisableColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox抗振铃强度.FillReadOnlyColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox抗振铃强度.Font = New Font("微软雅黑", 9.75F)
        UiTextBox抗振铃强度.ForeColor = Color.DarkGray
        UiTextBox抗振铃强度.ForeDisableColor = Color.DarkGray
        UiTextBox抗振铃强度.ForeReadOnlyColor = Color.DarkGray
        UiTextBox抗振铃强度.Location = New Point(10, 10)
        UiTextBox抗振铃强度.Margin = New Padding(4, 5, 4, 5)
        UiTextBox抗振铃强度.MinimumSize = New Size(1, 16)
        UiTextBox抗振铃强度.Name = "UiTextBox抗振铃强度"
        UiTextBox抗振铃强度.Padding = New Padding(5)
        UiTextBox抗振铃强度.Radius = 30
        UiTextBox抗振铃强度.RectColor = Color.DimGray
        UiTextBox抗振铃强度.RectDisableColor = Color.DimGray
        UiTextBox抗振铃强度.RectReadOnlyColor = Color.DimGray
        UiTextBox抗振铃强度.ScrollBarBackColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiTextBox抗振铃强度.ScrollBarColor = Color.DimGray
        UiTextBox抗振铃强度.ScrollBarStyleInherited = False
        UiTextBox抗振铃强度.ShowText = False
        UiTextBox抗振铃强度.Size = New Size(81, 30)
        UiTextBox抗振铃强度.TabIndex = 97
        UiTextBox抗振铃强度.TextAlignment = ContentAlignment.MiddleLeft
        UiTextBox抗振铃强度.Watermark = ""
        UiTextBox抗振铃强度.WatermarkActiveColor = Color.DimGray
        UiTextBox抗振铃强度.WatermarkColor = Color.DimGray
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
        Label9.Text = "抗振铃强度"
        Label9.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Panel5
        ' 
        Panel5.BackColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        Panel5.Controls.Add(Panel6)
        Panel5.Controls.Add(Label10)
        Panel5.Dock = DockStyle.Fill
        Panel5.Location = New Point(10, 258)
        Panel5.Name = "Panel5"
        Panel5.Size = New Size(664, 193)
        Panel5.TabIndex = 90
        ' 
        ' Panel6
        ' 
        Panel6.Controls.Add(Panel82)
        Panel6.Controls.Add(Panel23)
        Panel6.Dock = DockStyle.Fill
        Panel6.Location = New Point(100, 0)
        Panel6.Name = "Panel6"
        Panel6.Padding = New Padding(10)
        Panel6.Size = New Size(564, 193)
        Panel6.TabIndex = 2
        ' 
        ' Panel82
        ' 
        Panel82.BackColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        Panel82.Controls.Add(ListView1)
        Panel82.Dock = DockStyle.Fill
        Panel82.Location = New Point(10, 40)
        Panel82.Name = "Panel82"
        Panel82.Padding = New Padding(0, 10, 0, 0)
        Panel82.Size = New Size(544, 143)
        Panel82.TabIndex = 24
        ' 
        ' ListView1
        ' 
        ListView1.AllowDrop = True
        ListView1.BackColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        ListView1.BorderStyle = BorderStyle.None
        ListView1.Columns.AddRange(New ColumnHeader() {ColumnHeader1})
        ListView1.Dock = DockStyle.Fill
        ListView1.Font = New Font("微软雅黑", 10.2F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        ListView1.ForeColor = Color.Silver
        ListView1.FullRowSelect = True
        ListView1.HeaderStyle = ColumnHeaderStyle.None
        ListView1.Location = New Point(0, 10)
        ListView1.Name = "ListView1"
        ListView1.OwnerDraw = True
        ListView1.ShowItemToolTips = True
        ListView1.Size = New Size(544, 133)
        ListView1.TabIndex = 0
        ListView1.TabStop = False
        ListView1.UseCompatibleStateImageBehavior = False
        ListView1.View = View.Details
        ' 
        ' Panel23
        ' 
        Panel23.Controls.Add(Label14)
        Panel23.Controls.Add(UiButton下载)
        Panel23.Controls.Add(UiButton下移着色器)
        Panel23.Controls.Add(Label12)
        Panel23.Controls.Add(UiButton上移着色器)
        Panel23.Controls.Add(Label82)
        Panel23.Controls.Add(UiButton移除着色器)
        Panel23.Controls.Add(Label145)
        Panel23.Controls.Add(UiButton添加着色器)
        Panel23.Dock = DockStyle.Top
        Panel23.Location = New Point(10, 10)
        Panel23.Name = "Panel23"
        Panel23.Size = New Size(544, 30)
        Panel23.TabIndex = 23
        ' 
        ' Label14
        ' 
        Label14.Dock = DockStyle.Fill
        Label14.ForeColor = Color.Gray
        Label14.Location = New Point(255, 0)
        Label14.Name = "Label14"
        Label14.Padding = New Padding(10, 0, 0, 0)
        Label14.Size = New Size(229, 30)
        Label14.TabIndex = 100
        Label14.Text = "支持 .glsl 和 .hook 格式"
        Label14.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' UiButton下载
        ' 
        UiButton下载.Dock = DockStyle.Right
        UiButton下载.FillColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton下载.FillColor2 = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton下载.FillDisableColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiButton下载.FillHoverColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton下载.FillPressColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiButton下载.FillSelectedColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiButton下载.Font = New Font("微软雅黑", 9.75F)
        UiButton下载.ForeColor = Color.Silver
        UiButton下载.ForeDisableColor = Color.Silver
        UiButton下载.ForeHoverColor = Color.Silver
        UiButton下载.ForePressColor = Color.Silver
        UiButton下载.ForeSelectedColor = Color.Silver
        UiButton下载.Location = New Point(484, 0)
        UiButton下载.MinimumSize = New Size(1, 1)
        UiButton下载.Name = "UiButton下载"
        UiButton下载.RadiusSides = Sunny.UI.UICornerRadiusSides.None
        UiButton下载.RectColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton下载.RectDisableColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton下载.RectHoverColor = Color.DarkGray
        UiButton下载.RectPressColor = Color.FromArgb(CByte(64), CByte(148), CByte(64))
        UiButton下载.RectSelectedColor = Color.DarkGray
        UiButton下载.Size = New Size(60, 30)
        UiButton下载.TabIndex = 102
        UiButton下载.Text = "下载"
        UiButton下载.TipsFont = New Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        ' 
        ' UiButton下移着色器
        ' 
        UiButton下移着色器.Dock = DockStyle.Left
        UiButton下移着色器.FillColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton下移着色器.FillColor2 = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton下移着色器.FillDisableColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiButton下移着色器.FillHoverColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton下移着色器.FillPressColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiButton下移着色器.FillSelectedColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiButton下移着色器.Font = New Font("微软雅黑", 9.75F)
        UiButton下移着色器.ForeColor = Color.Silver
        UiButton下移着色器.ForeDisableColor = Color.Silver
        UiButton下移着色器.ForeHoverColor = Color.Silver
        UiButton下移着色器.ForePressColor = Color.Silver
        UiButton下移着色器.ForeSelectedColor = Color.Silver
        UiButton下移着色器.Location = New Point(195, 0)
        UiButton下移着色器.MinimumSize = New Size(1, 1)
        UiButton下移着色器.Name = "UiButton下移着色器"
        UiButton下移着色器.RadiusSides = Sunny.UI.UICornerRadiusSides.None
        UiButton下移着色器.RectColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton下移着色器.RectDisableColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton下移着色器.RectHoverColor = Color.DarkGray
        UiButton下移着色器.RectPressColor = Color.FromArgb(CByte(64), CByte(148), CByte(64))
        UiButton下移着色器.RectSelectedColor = Color.DarkGray
        UiButton下移着色器.Size = New Size(60, 30)
        UiButton下移着色器.TabIndex = 90
        UiButton下移着色器.Text = "下移"
        UiButton下移着色器.TipsFont = New Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        ' 
        ' Label12
        ' 
        Label12.Dock = DockStyle.Left
        Label12.Location = New Point(190, 0)
        Label12.Name = "Label12"
        Label12.Size = New Size(5, 30)
        Label12.TabIndex = 89
        Label12.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' UiButton上移着色器
        ' 
        UiButton上移着色器.Dock = DockStyle.Left
        UiButton上移着色器.FillColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton上移着色器.FillColor2 = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton上移着色器.FillDisableColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiButton上移着色器.FillHoverColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton上移着色器.FillPressColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiButton上移着色器.FillSelectedColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiButton上移着色器.Font = New Font("微软雅黑", 9.75F)
        UiButton上移着色器.ForeColor = Color.Silver
        UiButton上移着色器.ForeDisableColor = Color.Silver
        UiButton上移着色器.ForeHoverColor = Color.Silver
        UiButton上移着色器.ForePressColor = Color.Silver
        UiButton上移着色器.ForeSelectedColor = Color.Silver
        UiButton上移着色器.Location = New Point(130, 0)
        UiButton上移着色器.MinimumSize = New Size(1, 1)
        UiButton上移着色器.Name = "UiButton上移着色器"
        UiButton上移着色器.RadiusSides = Sunny.UI.UICornerRadiusSides.None
        UiButton上移着色器.RectColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton上移着色器.RectDisableColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton上移着色器.RectHoverColor = Color.DarkGray
        UiButton上移着色器.RectPressColor = Color.FromArgb(CByte(64), CByte(148), CByte(64))
        UiButton上移着色器.RectSelectedColor = Color.DarkGray
        UiButton上移着色器.Size = New Size(60, 30)
        UiButton上移着色器.TabIndex = 88
        UiButton上移着色器.Text = "上移"
        UiButton上移着色器.TipsFont = New Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        ' 
        ' Label82
        ' 
        Label82.Dock = DockStyle.Left
        Label82.Location = New Point(125, 0)
        Label82.Name = "Label82"
        Label82.Size = New Size(5, 30)
        Label82.TabIndex = 87
        Label82.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' UiButton移除着色器
        ' 
        UiButton移除着色器.Dock = DockStyle.Left
        UiButton移除着色器.FillColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton移除着色器.FillColor2 = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton移除着色器.FillDisableColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiButton移除着色器.FillHoverColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton移除着色器.FillPressColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiButton移除着色器.FillSelectedColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiButton移除着色器.Font = New Font("微软雅黑", 9.75F)
        UiButton移除着色器.ForeColor = Color.Silver
        UiButton移除着色器.ForeDisableColor = Color.Silver
        UiButton移除着色器.ForeHoverColor = Color.Silver
        UiButton移除着色器.ForePressColor = Color.Silver
        UiButton移除着色器.ForeSelectedColor = Color.Silver
        UiButton移除着色器.Location = New Point(65, 0)
        UiButton移除着色器.MinimumSize = New Size(1, 1)
        UiButton移除着色器.Name = "UiButton移除着色器"
        UiButton移除着色器.RadiusSides = Sunny.UI.UICornerRadiusSides.None
        UiButton移除着色器.RectColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton移除着色器.RectDisableColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton移除着色器.RectHoverColor = Color.DarkGray
        UiButton移除着色器.RectPressColor = Color.FromArgb(CByte(64), CByte(148), CByte(64))
        UiButton移除着色器.RectSelectedColor = Color.DarkGray
        UiButton移除着色器.Size = New Size(60, 30)
        UiButton移除着色器.TabIndex = 86
        UiButton移除着色器.Text = "移除"
        UiButton移除着色器.TipsFont = New Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        ' 
        ' Label145
        ' 
        Label145.Dock = DockStyle.Left
        Label145.Location = New Point(60, 0)
        Label145.Name = "Label145"
        Label145.Size = New Size(5, 30)
        Label145.TabIndex = 85
        Label145.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' UiButton添加着色器
        ' 
        UiButton添加着色器.Dock = DockStyle.Left
        UiButton添加着色器.FillColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton添加着色器.FillColor2 = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton添加着色器.FillDisableColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiButton添加着色器.FillHoverColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton添加着色器.FillPressColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiButton添加着色器.FillSelectedColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiButton添加着色器.Font = New Font("微软雅黑", 9.75F)
        UiButton添加着色器.ForeColor = Color.Silver
        UiButton添加着色器.ForeDisableColor = Color.Silver
        UiButton添加着色器.ForeHoverColor = Color.Silver
        UiButton添加着色器.ForePressColor = Color.Silver
        UiButton添加着色器.ForeSelectedColor = Color.Silver
        UiButton添加着色器.Location = New Point(0, 0)
        UiButton添加着色器.MinimumSize = New Size(1, 1)
        UiButton添加着色器.Name = "UiButton添加着色器"
        UiButton添加着色器.RadiusSides = Sunny.UI.UICornerRadiusSides.None
        UiButton添加着色器.RectColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton添加着色器.RectDisableColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton添加着色器.RectHoverColor = Color.DarkGray
        UiButton添加着色器.RectPressColor = Color.FromArgb(CByte(64), CByte(148), CByte(64))
        UiButton添加着色器.RectSelectedColor = Color.DarkGray
        UiButton添加着色器.Size = New Size(60, 30)
        UiButton添加着色器.TabIndex = 84
        UiButton添加着色器.Text = "添加"
        UiButton添加着色器.TipsFont = New Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        ' 
        ' Label10
        ' 
        Label10.BackColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        Label10.Dock = DockStyle.Left
        Label10.Font = New Font("微软雅黑", 10F, FontStyle.Bold)
        Label10.Location = New Point(0, 0)
        Label10.Name = "Label10"
        Label10.Size = New Size(100, 193)
        Label10.TabIndex = 4
        Label10.Text = "自定义" & vbCrLf & "Shader" & vbCrLf & "着色器"
        Label10.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label11
        ' 
        Label11.Dock = DockStyle.Top
        Label11.Location = New Point(10, 248)
        Label11.Name = "Label11"
        Label11.Size = New Size(664, 10)
        Label11.TabIndex = 89
        ' 
        ' Form超分
        ' 
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ClientSize = New Size(684, 461)
        Controls.Add(Panel5)
        Controls.Add(Label11)
        Controls.Add(Panel3)
        Controls.Add(Label3)
        Controls.Add(Panel1)
        Controls.Add(Label2)
        Controls.Add(Panel8)
        Controls.Add(Label124)
        Controls.Add(Panel73)
        Font = New Font("微软雅黑", 9.75F)
        ForeColor = Color.Silver
        MaximizeBox = False
        MinimizeBox = False
        MinimumSize = New Size(700, 500)
        Name = "Form超分"
        Padding = New Padding(10)
        ShowIcon = False
        ShowInTaskbar = False
        StartPosition = FormStartPosition.Manual
        Text = "libplacebo 超分"
        Panel73.ResumeLayout(False)
        Panel73.PerformLayout()
        Panel8.ResumeLayout(False)
        Panel11.ResumeLayout(False)
        Panel1.ResumeLayout(False)
        Panel2.ResumeLayout(False)
        Panel3.ResumeLayout(False)
        Panel4.ResumeLayout(False)
        Panel5.ResumeLayout(False)
        Panel6.ResumeLayout(False)
        Panel82.ResumeLayout(False)
        Panel23.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Panel73 As Panel
    Friend WithEvents Label123 As Label
    Friend WithEvents Panel8 As Panel
    Friend WithEvents Panel11 As Panel
    Friend WithEvents Label13 As Label
    Friend WithEvents UiTextBox超分宽度 As Sunny.UI.UITextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label124 As Label
    Friend WithEvents UiTextBox超分高度 As Sunny.UI.UITextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents UiComboBox下采样算法 As Sunny.UI.UIComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents UiComboBox上采样算法 As Sunny.UI.UIComboBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Label7 As Label
    Friend WithEvents UiTextBox抗振铃强度 As Sunny.UI.UITextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Label10 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Panel23 As Panel
    Friend WithEvents UiButton上移着色器 As Sunny.UI.UIButton
    Friend WithEvents Label82 As Label
    Friend WithEvents UiButton移除着色器 As Sunny.UI.UIButton
    Friend WithEvents Label145 As Label
    Friend WithEvents UiButton添加着色器 As Sunny.UI.UIButton
    Friend WithEvents UiButton下移着色器 As Sunny.UI.UIButton
    Friend WithEvents Label12 As Label
    Friend WithEvents Panel82 As Panel
    Friend WithEvents ListView1 As ListView
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents Label14 As Label
    Friend WithEvents UiButton下载 As Sunny.UI.UIButton
End Class
