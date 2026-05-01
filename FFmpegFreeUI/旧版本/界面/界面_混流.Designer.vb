<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class 界面_混流
    Inherits System.Windows.Forms.UserControl

    'UserControl 重写释放以清理组件列表。
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
        Label20 = New Label()
        Panel1 = New Panel()
        Panel2 = New Panel()
        ListView1 = New ListView()
        ColumnHeader1 = New ColumnHeader()
        ColumnHeader2 = New ColumnHeader()
        ColumnHeader3 = New ColumnHeader()
        ColumnHeader4 = New ColumnHeader()
        ColumnHeader5 = New ColumnHeader()
        ColumnHeader6 = New ColumnHeader()
        Panel3 = New Panel()
        Panel8 = New Panel()
        UiCheckBox2 = New Sunny.UI.UICheckBox()
        UiCheckBox1 = New Sunny.UI.UICheckBox()
        Label21 = New Label()
        Panel62 = New Panel()
        UiTextBox3 = New Sunny.UI.UITextBox()
        Label17 = New Label()
        UiTextBox2 = New Sunny.UI.UITextBox()
        Label14 = New Label()
        UiTextBox1 = New Sunny.UI.UITextBox()
        Label101 = New Label()
        Panel4 = New Panel()
        UiButton移除 = New Sunny.UI.UIButton()
        Label3 = New Label()
        UiButton下移 = New Sunny.UI.UIButton()
        Label10 = New Label()
        UiButton上移 = New Sunny.UI.UIButton()
        Label8 = New Label()
        UiButton添加文件 = New Sunny.UI.UIButton()
        Label视频 = New Label()
        Label音频 = New Label()
        Label字幕 = New Label()
        Label章节 = New Label()
        Label元数据 = New Label()
        Label1 = New Label()
        Label11 = New Label()
        Panel5 = New Panel()
        UiTextBox输出文件 = New Sunny.UI.UITextBox()
        Label6 = New Label()
        Label132 = New Label()
        UiButton选择位置 = New Sunny.UI.UIButton()
        UiButton启动混流 = New Sunny.UI.UIButton()
        Panel73.SuspendLayout()
        Panel1.SuspendLayout()
        Panel2.SuspendLayout()
        Panel3.SuspendLayout()
        Panel8.SuspendLayout()
        Panel62.SuspendLayout()
        Panel4.SuspendLayout()
        Panel5.SuspendLayout()
        SuspendLayout()
        ' 
        ' Panel73
        ' 
        Panel73.AutoSize = True
        Panel73.BackColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        Panel73.Controls.Add(Label123)
        Panel73.Dock = DockStyle.Top
        Panel73.Location = New Point(10, 0)
        Panel73.Name = "Panel73"
        Panel73.Padding = New Padding(10)
        Panel73.Size = New Size(1080, 39)
        Panel73.TabIndex = 80
        ' 
        ' Label123
        ' 
        Label123.AutoSize = True
        Label123.Dock = DockStyle.Top
        Label123.Font = New Font("微软雅黑", 9.75F)
        Label123.ForeColor = Color.Gray
        Label123.Location = New Point(10, 10)
        Label123.Name = "Label123"
        Label123.Size = New Size(509, 19)
        Label123.TabIndex = 4
        Label123.Text = "仅提供最基础的混流，高级功能请移步 MKVToolNix GUI；分离请用 MKVExtract GUI"
        ' 
        ' Label20
        ' 
        Label20.Dock = DockStyle.Top
        Label20.Location = New Point(10, 39)
        Label20.Name = "Label20"
        Label20.Size = New Size(1080, 10)
        Label20.TabIndex = 82
        ' 
        ' Panel1
        ' 
        Panel1.AutoSize = True
        Panel1.BackColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        Panel1.Controls.Add(Panel2)
        Panel1.Controls.Add(Panel3)
        Panel1.Controls.Add(Panel4)
        Panel1.Controls.Add(Label1)
        Panel1.Dock = DockStyle.Fill
        Panel1.Location = New Point(10, 49)
        Panel1.Name = "Panel1"
        Panel1.Padding = New Padding(10)
        Panel1.Size = New Size(1080, 581)
        Panel1.TabIndex = 83
        ' 
        ' Panel2
        ' 
        Panel2.BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        Panel2.Controls.Add(ListView1)
        Panel2.Dock = DockStyle.Fill
        Panel2.Location = New Point(10, 79)
        Panel2.Name = "Panel2"
        Panel2.Padding = New Padding(10, 10, 0, 10)
        Panel2.Size = New Size(1060, 412)
        Panel2.TabIndex = 5
        ' 
        ' ListView1
        ' 
        ListView1.AllowDrop = True
        ListView1.BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ListView1.BorderStyle = BorderStyle.None
        ListView1.Columns.AddRange(New ColumnHeader() {ColumnHeader1, ColumnHeader2, ColumnHeader3, ColumnHeader4, ColumnHeader5, ColumnHeader6})
        ListView1.Dock = DockStyle.Fill
        ListView1.ForeColor = Color.Silver
        ListView1.FullRowSelect = True
        ListView1.HeaderStyle = ColumnHeaderStyle.None
        ListView1.Location = New Point(10, 10)
        ListView1.Name = "ListView1"
        ListView1.OwnerDraw = True
        ListView1.Size = New Size(1050, 392)
        ListView1.TabIndex = 0
        ListView1.UseCompatibleStateImageBehavior = False
        ListView1.View = View.Details
        ' 
        ' Panel3
        ' 
        Panel3.AutoSize = True
        Panel3.Controls.Add(Panel8)
        Panel3.Controls.Add(Panel62)
        Panel3.Dock = DockStyle.Bottom
        Panel3.Location = New Point(10, 491)
        Panel3.Name = "Panel3"
        Panel3.Padding = New Padding(0, 10, 0, 0)
        Panel3.Size = New Size(1060, 80)
        Panel3.TabIndex = 6
        ' 
        ' Panel8
        ' 
        Panel8.Controls.Add(UiCheckBox2)
        Panel8.Controls.Add(UiCheckBox1)
        Panel8.Controls.Add(Label21)
        Panel8.Dock = DockStyle.Top
        Panel8.Location = New Point(0, 50)
        Panel8.Name = "Panel8"
        Panel8.Size = New Size(1060, 30)
        Panel8.TabIndex = 8
        ' 
        ' UiCheckBox2
        ' 
        UiCheckBox2.CheckBoxColor = Color.Silver
        UiCheckBox2.CheckBoxSize = 20
        UiCheckBox2.Dock = DockStyle.Left
        UiCheckBox2.Font = New Font("微软雅黑", 9.75F)
        UiCheckBox2.ForeColor = Color.Silver
        UiCheckBox2.Location = New Point(325, 0)
        UiCheckBox2.MinimumSize = New Size(1, 1)
        UiCheckBox2.Name = "UiCheckBox2"
        UiCheckBox2.Size = New Size(200, 30)
        UiCheckBox2.TabIndex = 101
        UiCheckBox2.Text = "使用此文件的元数据"
        ' 
        ' UiCheckBox1
        ' 
        UiCheckBox1.CheckBoxColor = Color.Silver
        UiCheckBox1.CheckBoxSize = 20
        UiCheckBox1.Dock = DockStyle.Left
        UiCheckBox1.Font = New Font("微软雅黑", 9.75F)
        UiCheckBox1.ForeColor = Color.Silver
        UiCheckBox1.Location = New Point(125, 0)
        UiCheckBox1.MinimumSize = New Size(1, 1)
        UiCheckBox1.Name = "UiCheckBox1"
        UiCheckBox1.Size = New Size(200, 30)
        UiCheckBox1.TabIndex = 99
        UiCheckBox1.Text = "使用此文件的章节"
        ' 
        ' Label21
        ' 
        Label21.Dock = DockStyle.Left
        Label21.Font = New Font("微软雅黑", 9.75F)
        Label21.Location = New Point(0, 0)
        Label21.Name = "Label21"
        Label21.Size = New Size(125, 30)
        Label21.TabIndex = 95
        Label21.Text = "章节和元数据"
        Label21.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Panel62
        ' 
        Panel62.Controls.Add(UiTextBox3)
        Panel62.Controls.Add(Label17)
        Panel62.Controls.Add(UiTextBox2)
        Panel62.Controls.Add(Label14)
        Panel62.Controls.Add(UiTextBox1)
        Panel62.Controls.Add(Label101)
        Panel62.Dock = DockStyle.Top
        Panel62.Location = New Point(0, 10)
        Panel62.Name = "Panel62"
        Panel62.Padding = New Padding(0, 0, 0, 10)
        Panel62.Size = New Size(1060, 40)
        Panel62.TabIndex = 5
        ' 
        ' UiTextBox3
        ' 
        UiTextBox3.Dock = DockStyle.Left
        UiTextBox3.FillColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox3.FillColor2 = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox3.FillDisableColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiTextBox3.FillReadOnlyColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiTextBox3.Font = New Font("微软雅黑", 9.75F)
        UiTextBox3.ForeColor = Color.DarkGray
        UiTextBox3.ForeDisableColor = Color.DarkGray
        UiTextBox3.ForeReadOnlyColor = Color.DarkGray
        UiTextBox3.Location = New Point(775, 0)
        UiTextBox3.Margin = New Padding(4, 5, 4, 5)
        UiTextBox3.MinimumSize = New Size(1, 16)
        UiTextBox3.Name = "UiTextBox3"
        UiTextBox3.Padding = New Padding(5)
        UiTextBox3.Radius = 30
        UiTextBox3.RectColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox3.RectDisableColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox3.RectReadOnlyColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox3.ScrollBarBackColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiTextBox3.ScrollBarColor = Color.DimGray
        UiTextBox3.ScrollBarStyleInherited = False
        UiTextBox3.ShowText = False
        UiTextBox3.Size = New Size(200, 30)
        UiTextBox3.TabIndex = 100
        UiTextBox3.TextAlignment = ContentAlignment.MiddleLeft
        UiTextBox3.Watermark = "多个流用英文逗号隔开"
        UiTextBox3.WatermarkActiveColor = Color.DimGray
        UiTextBox3.WatermarkColor = Color.DimGray
        ' 
        ' Label17
        ' 
        Label17.Dock = DockStyle.Left
        Label17.Font = New Font("微软雅黑", 9.75F)
        Label17.Location = New Point(650, 0)
        Label17.Name = "Label17"
        Label17.Size = New Size(125, 30)
        Label17.TabIndex = 99
        Label17.Text = "字幕流索引号："
        Label17.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' UiTextBox2
        ' 
        UiTextBox2.Dock = DockStyle.Left
        UiTextBox2.FillColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox2.FillColor2 = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox2.FillDisableColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiTextBox2.FillReadOnlyColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiTextBox2.Font = New Font("微软雅黑", 9.75F)
        UiTextBox2.ForeColor = Color.DarkGray
        UiTextBox2.ForeDisableColor = Color.DarkGray
        UiTextBox2.ForeReadOnlyColor = Color.DarkGray
        UiTextBox2.Location = New Point(450, 0)
        UiTextBox2.Margin = New Padding(4, 5, 4, 5)
        UiTextBox2.MinimumSize = New Size(1, 16)
        UiTextBox2.Name = "UiTextBox2"
        UiTextBox2.Padding = New Padding(5)
        UiTextBox2.Radius = 30
        UiTextBox2.RectColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox2.RectDisableColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox2.RectReadOnlyColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox2.ScrollBarBackColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiTextBox2.ScrollBarColor = Color.DimGray
        UiTextBox2.ScrollBarStyleInherited = False
        UiTextBox2.ShowText = False
        UiTextBox2.Size = New Size(200, 30)
        UiTextBox2.TabIndex = 98
        UiTextBox2.TextAlignment = ContentAlignment.MiddleLeft
        UiTextBox2.Watermark = "多个流用英文逗号隔开"
        UiTextBox2.WatermarkActiveColor = Color.DimGray
        UiTextBox2.WatermarkColor = Color.DimGray
        ' 
        ' Label14
        ' 
        Label14.Dock = DockStyle.Left
        Label14.Font = New Font("微软雅黑", 9.75F)
        Label14.Location = New Point(325, 0)
        Label14.Name = "Label14"
        Label14.Size = New Size(125, 30)
        Label14.TabIndex = 97
        Label14.Text = "音频流索引号："
        Label14.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' UiTextBox1
        ' 
        UiTextBox1.Dock = DockStyle.Left
        UiTextBox1.FillColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox1.FillColor2 = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox1.FillDisableColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiTextBox1.FillReadOnlyColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiTextBox1.Font = New Font("微软雅黑", 9.75F)
        UiTextBox1.ForeColor = Color.DarkGray
        UiTextBox1.ForeDisableColor = Color.DarkGray
        UiTextBox1.ForeReadOnlyColor = Color.DarkGray
        UiTextBox1.Location = New Point(125, 0)
        UiTextBox1.Margin = New Padding(4, 5, 4, 5)
        UiTextBox1.MinimumSize = New Size(1, 16)
        UiTextBox1.Name = "UiTextBox1"
        UiTextBox1.Padding = New Padding(5)
        UiTextBox1.Radius = 30
        UiTextBox1.RectColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox1.RectDisableColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox1.RectReadOnlyColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox1.ScrollBarBackColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiTextBox1.ScrollBarColor = Color.DimGray
        UiTextBox1.ScrollBarStyleInherited = False
        UiTextBox1.ShowText = False
        UiTextBox1.Size = New Size(200, 30)
        UiTextBox1.TabIndex = 96
        UiTextBox1.TextAlignment = ContentAlignment.MiddleLeft
        UiTextBox1.Watermark = "多个流用英文逗号隔开"
        UiTextBox1.WatermarkActiveColor = Color.DimGray
        UiTextBox1.WatermarkColor = Color.DimGray
        ' 
        ' Label101
        ' 
        Label101.Dock = DockStyle.Left
        Label101.Font = New Font("微软雅黑", 9.75F)
        Label101.Location = New Point(0, 0)
        Label101.Name = "Label101"
        Label101.Size = New Size(125, 30)
        Label101.TabIndex = 95
        Label101.Text = "视频流索引号："
        Label101.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Panel4
        ' 
        Panel4.BackColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        Panel4.Controls.Add(UiButton移除)
        Panel4.Controls.Add(Label3)
        Panel4.Controls.Add(UiButton下移)
        Panel4.Controls.Add(Label10)
        Panel4.Controls.Add(UiButton上移)
        Panel4.Controls.Add(Label8)
        Panel4.Controls.Add(UiButton添加文件)
        Panel4.Controls.Add(Label视频)
        Panel4.Controls.Add(Label音频)
        Panel4.Controls.Add(Label字幕)
        Panel4.Controls.Add(Label章节)
        Panel4.Controls.Add(Label元数据)
        Panel4.Dock = DockStyle.Top
        Panel4.Location = New Point(10, 29)
        Panel4.Name = "Panel4"
        Panel4.Padding = New Padding(0, 10, 0, 10)
        Panel4.Size = New Size(1060, 50)
        Panel4.TabIndex = 7
        ' 
        ' UiButton移除
        ' 
        UiButton移除.AllowDrop = True
        UiButton移除.Dock = DockStyle.Left
        UiButton移除.FillColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton移除.FillColor2 = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton移除.FillDisableColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton移除.FillHoverColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton移除.FillPressColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton移除.FillSelectedColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton移除.Font = New Font("微软雅黑", 9.75F)
        UiButton移除.ForeColor = Color.IndianRed
        UiButton移除.ForeDisableColor = Color.IndianRed
        UiButton移除.ForeHoverColor = Color.IndianRed
        UiButton移除.ForePressColor = Color.IndianRed
        UiButton移除.ForeSelectedColor = Color.IndianRed
        UiButton移除.Location = New Point(290, 10)
        UiButton移除.MinimumSize = New Size(1, 1)
        UiButton移除.Name = "UiButton移除"
        UiButton移除.Radius = 30
        UiButton移除.RectColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton移除.RectDisableColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton移除.RectHoverColor = Color.IndianRed
        UiButton移除.RectPressColor = Color.FromArgb(CByte(64), CByte(148), CByte(64))
        UiButton移除.RectSelectedColor = Color.IndianRed
        UiButton移除.Size = New Size(70, 30)
        UiButton移除.TabIndex = 104
        UiButton移除.Text = "移除"
        UiButton移除.TipsFont = New Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        ' 
        ' Label3
        ' 
        Label3.Dock = DockStyle.Left
        Label3.Location = New Point(280, 10)
        Label3.Name = "Label3"
        Label3.Size = New Size(10, 30)
        Label3.TabIndex = 103
        ' 
        ' UiButton下移
        ' 
        UiButton下移.AllowDrop = True
        UiButton下移.Dock = DockStyle.Left
        UiButton下移.FillColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton下移.FillColor2 = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton下移.FillDisableColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton下移.FillHoverColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton下移.FillPressColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton下移.FillSelectedColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton下移.Font = New Font("微软雅黑", 9.75F)
        UiButton下移.ForeColor = Color.CornflowerBlue
        UiButton下移.ForeDisableColor = Color.CornflowerBlue
        UiButton下移.ForeHoverColor = Color.CornflowerBlue
        UiButton下移.ForePressColor = Color.CornflowerBlue
        UiButton下移.ForeSelectedColor = Color.CornflowerBlue
        UiButton下移.Location = New Point(210, 10)
        UiButton下移.MinimumSize = New Size(1, 1)
        UiButton下移.Name = "UiButton下移"
        UiButton下移.Radius = 30
        UiButton下移.RectColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton下移.RectDisableColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton下移.RectHoverColor = Color.CornflowerBlue
        UiButton下移.RectPressColor = Color.FromArgb(CByte(64), CByte(148), CByte(64))
        UiButton下移.RectSelectedColor = Color.CornflowerBlue
        UiButton下移.Size = New Size(70, 30)
        UiButton下移.TabIndex = 102
        UiButton下移.Text = "下移"
        UiButton下移.TipsFont = New Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        ' 
        ' Label10
        ' 
        Label10.Dock = DockStyle.Left
        Label10.Location = New Point(200, 10)
        Label10.Name = "Label10"
        Label10.Size = New Size(10, 30)
        Label10.TabIndex = 101
        ' 
        ' UiButton上移
        ' 
        UiButton上移.AllowDrop = True
        UiButton上移.Dock = DockStyle.Left
        UiButton上移.FillColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton上移.FillColor2 = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton上移.FillDisableColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton上移.FillHoverColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton上移.FillPressColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton上移.FillSelectedColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton上移.Font = New Font("微软雅黑", 9.75F)
        UiButton上移.ForeColor = Color.CornflowerBlue
        UiButton上移.ForeDisableColor = Color.CornflowerBlue
        UiButton上移.ForeHoverColor = Color.CornflowerBlue
        UiButton上移.ForePressColor = Color.CornflowerBlue
        UiButton上移.ForeSelectedColor = Color.CornflowerBlue
        UiButton上移.Location = New Point(130, 10)
        UiButton上移.MinimumSize = New Size(1, 1)
        UiButton上移.Name = "UiButton上移"
        UiButton上移.Radius = 30
        UiButton上移.RectColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton上移.RectDisableColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton上移.RectHoverColor = Color.CornflowerBlue
        UiButton上移.RectPressColor = Color.FromArgb(CByte(64), CByte(148), CByte(64))
        UiButton上移.RectSelectedColor = Color.CornflowerBlue
        UiButton上移.Size = New Size(70, 30)
        UiButton上移.TabIndex = 100
        UiButton上移.Text = "上移"
        UiButton上移.TipsFont = New Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        ' 
        ' Label8
        ' 
        Label8.Dock = DockStyle.Left
        Label8.Location = New Point(120, 10)
        Label8.Name = "Label8"
        Label8.Size = New Size(10, 30)
        Label8.TabIndex = 99
        ' 
        ' UiButton添加文件
        ' 
        UiButton添加文件.AllowDrop = True
        UiButton添加文件.Dock = DockStyle.Left
        UiButton添加文件.FillColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton添加文件.FillColor2 = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton添加文件.FillDisableColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton添加文件.FillHoverColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton添加文件.FillPressColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton添加文件.FillSelectedColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton添加文件.Font = New Font("微软雅黑", 9.75F)
        UiButton添加文件.ForeColor = Color.YellowGreen
        UiButton添加文件.ForeDisableColor = Color.YellowGreen
        UiButton添加文件.ForeHoverColor = Color.YellowGreen
        UiButton添加文件.ForePressColor = Color.YellowGreen
        UiButton添加文件.ForeSelectedColor = Color.YellowGreen
        UiButton添加文件.Location = New Point(0, 10)
        UiButton添加文件.MinimumSize = New Size(1, 1)
        UiButton添加文件.Name = "UiButton添加文件"
        UiButton添加文件.Radius = 30
        UiButton添加文件.RectColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton添加文件.RectDisableColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton添加文件.RectHoverColor = Color.YellowGreen
        UiButton添加文件.RectPressColor = Color.FromArgb(CByte(64), CByte(148), CByte(64))
        UiButton添加文件.RectSelectedColor = Color.YellowGreen
        UiButton添加文件.Size = New Size(120, 30)
        UiButton添加文件.TabIndex = 46
        UiButton添加文件.Text = "添加文件"
        UiButton添加文件.TipsFont = New Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        ' 
        ' Label视频
        ' 
        Label视频.Dock = DockStyle.Right
        Label视频.Font = New Font("微软雅黑", 10F)
        Label视频.Location = New Point(610, 10)
        Label视频.Name = "Label视频"
        Label视频.Size = New Size(75, 30)
        Label视频.TabIndex = 1
        Label视频.Text = "视频"
        Label视频.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label音频
        ' 
        Label音频.Dock = DockStyle.Right
        Label音频.Font = New Font("微软雅黑", 10F)
        Label音频.Location = New Point(685, 10)
        Label音频.Name = "Label音频"
        Label音频.Size = New Size(75, 30)
        Label音频.TabIndex = 2
        Label音频.Text = "音频"
        Label音频.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label字幕
        ' 
        Label字幕.Dock = DockStyle.Right
        Label字幕.Font = New Font("微软雅黑", 10F)
        Label字幕.Location = New Point(760, 10)
        Label字幕.Name = "Label字幕"
        Label字幕.Size = New Size(75, 30)
        Label字幕.TabIndex = 5
        Label字幕.Text = "字幕"
        Label字幕.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label章节
        ' 
        Label章节.Dock = DockStyle.Right
        Label章节.Font = New Font("微软雅黑", 10F)
        Label章节.Location = New Point(835, 10)
        Label章节.Name = "Label章节"
        Label章节.Size = New Size(75, 30)
        Label章节.TabIndex = 3
        Label章节.Text = "章节"
        Label章节.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label元数据
        ' 
        Label元数据.Dock = DockStyle.Right
        Label元数据.Font = New Font("微软雅黑", 10F)
        Label元数据.Location = New Point(910, 10)
        Label元数据.Name = "Label元数据"
        Label元数据.Size = New Size(150, 30)
        Label元数据.TabIndex = 4
        Label元数据.Text = "元数据"
        Label元数据.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Dock = DockStyle.Top
        Label1.Font = New Font("微软雅黑", 9.75F)
        Label1.ForeColor = Color.Gray
        Label1.Location = New Point(10, 10)
        Label1.Name = "Label1"
        Label1.Size = New Size(527, 19)
        Label1.TabIndex = 4
        Label1.Text = "添加输入文件，然后选中来编辑要使用哪些流，使用键盘 F3 和 F4 来排序，Delete 来移除"
        ' 
        ' Label11
        ' 
        Label11.Dock = DockStyle.Bottom
        Label11.Location = New Point(10, 630)
        Label11.Name = "Label11"
        Label11.Size = New Size(1080, 10)
        Label11.TabIndex = 84
        ' 
        ' Panel5
        ' 
        Panel5.BackColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        Panel5.Controls.Add(UiTextBox输出文件)
        Panel5.Controls.Add(Label6)
        Panel5.Controls.Add(Label132)
        Panel5.Controls.Add(UiButton选择位置)
        Panel5.Controls.Add(UiButton启动混流)
        Panel5.Dock = DockStyle.Bottom
        Panel5.Location = New Point(10, 640)
        Panel5.Name = "Panel5"
        Panel5.Padding = New Padding(10)
        Panel5.Size = New Size(1080, 50)
        Panel5.TabIndex = 85
        ' 
        ' UiTextBox输出文件
        ' 
        UiTextBox输出文件.Dock = DockStyle.Fill
        UiTextBox输出文件.FillColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox输出文件.FillColor2 = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox输出文件.FillDisableColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiTextBox输出文件.FillReadOnlyColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiTextBox输出文件.Font = New Font("微软雅黑", 9.75F)
        UiTextBox输出文件.ForeColor = Color.DarkGray
        UiTextBox输出文件.ForeDisableColor = Color.DarkGray
        UiTextBox输出文件.ForeReadOnlyColor = Color.DarkGray
        UiTextBox输出文件.Location = New Point(140, 10)
        UiTextBox输出文件.Margin = New Padding(4, 5, 4, 5)
        UiTextBox输出文件.MinimumSize = New Size(1, 16)
        UiTextBox输出文件.Name = "UiTextBox输出文件"
        UiTextBox输出文件.Padding = New Padding(5)
        UiTextBox输出文件.Radius = 30
        UiTextBox输出文件.RectColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox输出文件.RectDisableColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox输出文件.RectReadOnlyColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox输出文件.ScrollBarBackColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiTextBox输出文件.ScrollBarColor = Color.DimGray
        UiTextBox输出文件.ScrollBarStyleInherited = False
        UiTextBox输出文件.ShowText = False
        UiTextBox输出文件.Size = New Size(800, 30)
        UiTextBox输出文件.TabIndex = 105
        UiTextBox输出文件.TextAlignment = ContentAlignment.MiddleLeft
        UiTextBox输出文件.Watermark = "输出到目标位置"
        UiTextBox输出文件.WatermarkActiveColor = Color.DimGray
        UiTextBox输出文件.WatermarkColor = Color.DimGray
        ' 
        ' Label6
        ' 
        Label6.Dock = DockStyle.Right
        Label6.Location = New Point(940, 10)
        Label6.Name = "Label6"
        Label6.Size = New Size(10, 30)
        Label6.TabIndex = 104
        ' 
        ' Label132
        ' 
        Label132.Dock = DockStyle.Left
        Label132.Location = New Point(130, 10)
        Label132.Name = "Label132"
        Label132.Size = New Size(10, 30)
        Label132.TabIndex = 103
        ' 
        ' UiButton选择位置
        ' 
        UiButton选择位置.AllowDrop = True
        UiButton选择位置.Dock = DockStyle.Left
        UiButton选择位置.FillColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton选择位置.FillColor2 = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton选择位置.FillDisableColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton选择位置.FillHoverColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton选择位置.FillPressColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton选择位置.FillSelectedColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton选择位置.Font = New Font("微软雅黑", 9.75F)
        UiButton选择位置.ForeColor = Color.YellowGreen
        UiButton选择位置.ForeDisableColor = Color.YellowGreen
        UiButton选择位置.ForeHoverColor = Color.YellowGreen
        UiButton选择位置.ForePressColor = Color.YellowGreen
        UiButton选择位置.ForeSelectedColor = Color.YellowGreen
        UiButton选择位置.Location = New Point(10, 10)
        UiButton选择位置.MinimumSize = New Size(1, 1)
        UiButton选择位置.Name = "UiButton选择位置"
        UiButton选择位置.Radius = 30
        UiButton选择位置.RectColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton选择位置.RectDisableColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton选择位置.RectHoverColor = Color.YellowGreen
        UiButton选择位置.RectPressColor = Color.FromArgb(CByte(64), CByte(148), CByte(64))
        UiButton选择位置.RectSelectedColor = Color.YellowGreen
        UiButton选择位置.Size = New Size(120, 30)
        UiButton选择位置.TabIndex = 102
        UiButton选择位置.Text = "选择位置"
        UiButton选择位置.TipsFont = New Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        ' 
        ' UiButton启动混流
        ' 
        UiButton启动混流.AllowDrop = True
        UiButton启动混流.Dock = DockStyle.Right
        UiButton启动混流.FillColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton启动混流.FillColor2 = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton启动混流.FillDisableColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton启动混流.FillHoverColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton启动混流.FillPressColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton启动混流.FillSelectedColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton启动混流.Font = New Font("微软雅黑", 9.75F)
        UiButton启动混流.ForeColor = Color.YellowGreen
        UiButton启动混流.ForeDisableColor = Color.YellowGreen
        UiButton启动混流.ForeHoverColor = Color.YellowGreen
        UiButton启动混流.ForePressColor = Color.YellowGreen
        UiButton启动混流.ForeSelectedColor = Color.YellowGreen
        UiButton启动混流.Location = New Point(950, 10)
        UiButton启动混流.MinimumSize = New Size(1, 1)
        UiButton启动混流.Name = "UiButton启动混流"
        UiButton启动混流.Radius = 30
        UiButton启动混流.RectColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton启动混流.RectDisableColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton启动混流.RectHoverColor = Color.YellowGreen
        UiButton启动混流.RectPressColor = Color.FromArgb(CByte(64), CByte(148), CByte(64))
        UiButton启动混流.RectSelectedColor = Color.YellowGreen
        UiButton启动混流.Size = New Size(120, 30)
        UiButton启动混流.TabIndex = 101
        UiButton启动混流.Text = "启动混流"
        UiButton启动混流.TipsFont = New Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        ' 
        ' 界面_混流
        ' 
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        Controls.Add(Panel1)
        Controls.Add(Label11)
        Controls.Add(Label20)
        Controls.Add(Panel73)
        Controls.Add(Panel5)
        Font = New Font("微软雅黑", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        ForeColor = Color.Silver
        Name = "界面_混流"
        Padding = New Padding(10, 0, 10, 10)
        Size = New Size(1100, 700)
        Panel73.ResumeLayout(False)
        Panel73.PerformLayout()
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        Panel2.ResumeLayout(False)
        Panel3.ResumeLayout(False)
        Panel8.ResumeLayout(False)
        Panel62.ResumeLayout(False)
        Panel4.ResumeLayout(False)
        Panel5.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Panel73 As Panel
    Friend WithEvents Label123 As Label
    Friend WithEvents Label20 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents ListView1 As ListView
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Label元数据 As Label
    Friend WithEvents Label章节 As Label
    Friend WithEvents Label音频 As Label
    Friend WithEvents Label视频 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel62 As Panel
    Friend WithEvents UiTextBox1 As Sunny.UI.UITextBox
    Friend WithEvents Label101 As Label
    Friend WithEvents Panel8 As Panel
    Friend WithEvents Label21 As Label
    Friend WithEvents UiCheckBox1 As Sunny.UI.UICheckBox
    Friend WithEvents UiCheckBox2 As Sunny.UI.UICheckBox
    Friend WithEvents Label字幕 As Label
    Friend WithEvents UiTextBox3 As Sunny.UI.UITextBox
    Friend WithEvents Label17 As Label
    Friend WithEvents UiTextBox2 As Sunny.UI.UITextBox
    Friend WithEvents Label14 As Label
    Friend WithEvents UiButton添加文件 As Sunny.UI.UIButton
    Friend WithEvents UiButton下移 As Sunny.UI.UIButton
    Friend WithEvents Label10 As Label
    Friend WithEvents UiButton上移 As Sunny.UI.UIButton
    Friend WithEvents Label8 As Label
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents ColumnHeader2 As ColumnHeader
    Friend WithEvents ColumnHeader3 As ColumnHeader
    Friend WithEvents ColumnHeader4 As ColumnHeader
    Friend WithEvents ColumnHeader5 As ColumnHeader
    Friend WithEvents ColumnHeader6 As ColumnHeader
    Friend WithEvents UiButton移除 As Sunny.UI.UIButton
    Friend WithEvents Label3 As Label
    Friend WithEvents UiTextBox输出文件 As Sunny.UI.UITextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label132 As Label
    Friend WithEvents UiButton选择位置 As Sunny.UI.UIButton
    Friend WithEvents UiButton启动混流 As Sunny.UI.UIButton

End Class
