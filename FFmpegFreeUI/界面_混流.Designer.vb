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
        Panel3 = New Panel()
        Panel8 = New Panel()
        UiCheckBox1 = New Sunny.UI.UICheckBox()
        UiCheckBox保留其他视频流 = New Sunny.UI.UICheckBox()
        Label21 = New Label()
        Panel7 = New Panel()
        UiTextBox7 = New Sunny.UI.UITextBox()
        Label15 = New Label()
        UiTextBox8 = New Sunny.UI.UITextBox()
        Label16 = New Label()
        UiTextBox9 = New Sunny.UI.UITextBox()
        Label17 = New Label()
        Panel6 = New Panel()
        UiTextBox4 = New Sunny.UI.UITextBox()
        Label12 = New Label()
        UiTextBox5 = New Sunny.UI.UITextBox()
        Label13 = New Label()
        UiTextBox6 = New Sunny.UI.UITextBox()
        Label14 = New Label()
        Panel62 = New Panel()
        UiTextBox3 = New Sunny.UI.UITextBox()
        Label8 = New Label()
        UiTextBox2 = New Sunny.UI.UITextBox()
        Label7 = New Label()
        UiTextBox1 = New Sunny.UI.UITextBox()
        Label101 = New Label()
        Panel4 = New Panel()
        Label5 = New Label()
        Label4 = New Label()
        Label3 = New Label()
        Label9 = New Label()
        Label10 = New Label()
        Label1 = New Label()
        Label2 = New Label()
        Label11 = New Label()
        Panel5 = New Panel()
        Panel75 = New Panel()
        UiTextBox将视频参数用于这些流 = New Sunny.UI.UITextBox()
        Label6 = New Label()
        Label132 = New Label()
        UiButton1 = New Sunny.UI.UIButton()
        UiButton启动混流 = New Sunny.UI.UIButton()
        Panel73.SuspendLayout()
        Panel1.SuspendLayout()
        Panel2.SuspendLayout()
        Panel3.SuspendLayout()
        Panel8.SuspendLayout()
        Panel7.SuspendLayout()
        Panel6.SuspendLayout()
        Panel62.SuspendLayout()
        Panel4.SuspendLayout()
        Panel5.SuspendLayout()
        Panel75.SuspendLayout()
        SuspendLayout()
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
        Panel73.Size = New Size(1060, 58)
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
        Label123.Size = New Size(348, 38)
        Label123.TabIndex = 4
        Label123.Text = "此功能为扩展，不走编码队列，直接让 ffmpeg 以原样运行" & vbCrLf & "仅提供基础混流，高级功能请移步 MKVToolNix GUI"
        ' 
        ' Label20
        ' 
        Label20.Dock = DockStyle.Top
        Label20.Location = New Point(20, 78)
        Label20.Name = "Label20"
        Label20.Size = New Size(1060, 20)
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
        Panel1.Controls.Add(Label2)
        Panel1.Dock = DockStyle.Fill
        Panel1.Location = New Point(20, 98)
        Panel1.Name = "Panel1"
        Panel1.Padding = New Padding(10)
        Panel1.Size = New Size(1060, 507)
        Panel1.TabIndex = 83
        ' 
        ' Panel2
        ' 
        Panel2.BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        Panel2.Controls.Add(ListView1)
        Panel2.Dock = DockStyle.Fill
        Panel2.Location = New Point(10, 98)
        Panel2.Name = "Panel2"
        Panel2.Padding = New Padding(0, 10, 0, 10)
        Panel2.Size = New Size(1040, 239)
        Panel2.TabIndex = 5
        ' 
        ' ListView1
        ' 
        ListView1.BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ListView1.BorderStyle = BorderStyle.None
        ListView1.Dock = DockStyle.Fill
        ListView1.Font = New Font("微软雅黑", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        ListView1.Location = New Point(0, 10)
        ListView1.Name = "ListView1"
        ListView1.Size = New Size(1040, 219)
        ListView1.TabIndex = 0
        ListView1.UseCompatibleStateImageBehavior = False
        ' 
        ' Panel3
        ' 
        Panel3.AutoSize = True
        Panel3.Controls.Add(Panel8)
        Panel3.Controls.Add(Panel7)
        Panel3.Controls.Add(Panel6)
        Panel3.Controls.Add(Panel62)
        Panel3.Dock = DockStyle.Bottom
        Panel3.Location = New Point(10, 337)
        Panel3.Name = "Panel3"
        Panel3.Padding = New Padding(0, 10, 0, 0)
        Panel3.Size = New Size(1040, 160)
        Panel3.TabIndex = 6
        ' 
        ' Panel8
        ' 
        Panel8.Controls.Add(UiCheckBox1)
        Panel8.Controls.Add(UiCheckBox保留其他视频流)
        Panel8.Controls.Add(Label21)
        Panel8.Dock = DockStyle.Top
        Panel8.Location = New Point(0, 130)
        Panel8.Name = "Panel8"
        Panel8.Size = New Size(1040, 30)
        Panel8.TabIndex = 8
        ' 
        ' UiCheckBox1
        ' 
        UiCheckBox1.CheckBoxColor = Color.Silver
        UiCheckBox1.CheckBoxSize = 20
        UiCheckBox1.Dock = DockStyle.Left
        UiCheckBox1.Font = New Font("微软雅黑", 9.75F)
        UiCheckBox1.ForeColor = Color.Silver
        UiCheckBox1.Location = New Point(325, 0)
        UiCheckBox1.MinimumSize = New Size(1, 1)
        UiCheckBox1.Name = "UiCheckBox1"
        UiCheckBox1.Size = New Size(200, 30)
        UiCheckBox1.TabIndex = 101
        UiCheckBox1.Text = "使用此文件的元数据"
        ' 
        ' UiCheckBox保留其他视频流
        ' 
        UiCheckBox保留其他视频流.CheckBoxColor = Color.Silver
        UiCheckBox保留其他视频流.CheckBoxSize = 20
        UiCheckBox保留其他视频流.Dock = DockStyle.Left
        UiCheckBox保留其他视频流.Font = New Font("微软雅黑", 9.75F)
        UiCheckBox保留其他视频流.ForeColor = Color.Silver
        UiCheckBox保留其他视频流.Location = New Point(125, 0)
        UiCheckBox保留其他视频流.MinimumSize = New Size(1, 1)
        UiCheckBox保留其他视频流.Name = "UiCheckBox保留其他视频流"
        UiCheckBox保留其他视频流.Size = New Size(200, 30)
        UiCheckBox保留其他视频流.TabIndex = 99
        UiCheckBox保留其他视频流.Text = "使用此文件的章节"
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
        ' Panel7
        ' 
        Panel7.Controls.Add(UiTextBox7)
        Panel7.Controls.Add(Label15)
        Panel7.Controls.Add(UiTextBox8)
        Panel7.Controls.Add(Label16)
        Panel7.Controls.Add(UiTextBox9)
        Panel7.Controls.Add(Label17)
        Panel7.Dock = DockStyle.Top
        Panel7.Location = New Point(0, 90)
        Panel7.Name = "Panel7"
        Panel7.Padding = New Padding(0, 0, 0, 10)
        Panel7.Size = New Size(1040, 40)
        Panel7.TabIndex = 7
        ' 
        ' UiTextBox7
        ' 
        UiTextBox7.Dock = DockStyle.Fill
        UiTextBox7.FillColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiTextBox7.FillColor2 = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiTextBox7.FillDisableColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiTextBox7.FillReadOnlyColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiTextBox7.Font = New Font("微软雅黑", 9.75F)
        UiTextBox7.ForeColor = Color.DarkGray
        UiTextBox7.ForeDisableColor = Color.DarkGray
        UiTextBox7.ForeReadOnlyColor = Color.DarkGray
        UiTextBox7.Location = New Point(625, 0)
        UiTextBox7.Margin = New Padding(4, 5, 4, 5)
        UiTextBox7.MinimumSize = New Size(1, 16)
        UiTextBox7.Name = "UiTextBox7"
        UiTextBox7.Padding = New Padding(5)
        UiTextBox7.Radius = 30
        UiTextBox7.RectColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        UiTextBox7.RectDisableColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        UiTextBox7.RectReadOnlyColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        UiTextBox7.ScrollBarBackColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiTextBox7.ScrollBarColor = Color.DimGray
        UiTextBox7.ScrollBarStyleInherited = False
        UiTextBox7.ShowText = False
        UiTextBox7.Size = New Size(415, 30)
        UiTextBox7.TabIndex = 100
        UiTextBox7.TextAlignment = ContentAlignment.MiddleLeft
        UiTextBox7.Watermark = "设置不同的语言和名称请多次添加文件"
        UiTextBox7.WatermarkActiveColor = Color.DimGray
        UiTextBox7.WatermarkColor = Color.DimGray
        ' 
        ' Label15
        ' 
        Label15.Dock = DockStyle.Left
        Label15.Font = New Font("微软雅黑", 9.75F)
        Label15.Location = New Point(525, 0)
        Label15.Name = "Label15"
        Label15.Padding = New Padding(0, 0, 10, 0)
        Label15.Size = New Size(100, 30)
        Label15.TabIndex = 99
        Label15.Text = "字幕流名称"
        Label15.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' UiTextBox8
        ' 
        UiTextBox8.Dock = DockStyle.Left
        UiTextBox8.FillColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiTextBox8.FillColor2 = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiTextBox8.FillDisableColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiTextBox8.FillReadOnlyColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiTextBox8.Font = New Font("微软雅黑", 9.75F)
        UiTextBox8.ForeColor = Color.DarkGray
        UiTextBox8.ForeDisableColor = Color.DarkGray
        UiTextBox8.ForeReadOnlyColor = Color.DarkGray
        UiTextBox8.Location = New Point(425, 0)
        UiTextBox8.Margin = New Padding(4, 5, 4, 5)
        UiTextBox8.MinimumSize = New Size(1, 16)
        UiTextBox8.Name = "UiTextBox8"
        UiTextBox8.Padding = New Padding(5)
        UiTextBox8.Radius = 30
        UiTextBox8.RectColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        UiTextBox8.RectDisableColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        UiTextBox8.RectReadOnlyColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        UiTextBox8.ScrollBarBackColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiTextBox8.ScrollBarColor = Color.DimGray
        UiTextBox8.ScrollBarStyleInherited = False
        UiTextBox8.ShowText = False
        UiTextBox8.Size = New Size(100, 30)
        UiTextBox8.TabIndex = 98
        UiTextBox8.TextAlignment = ContentAlignment.MiddleLeft
        UiTextBox8.Watermark = ""
        UiTextBox8.WatermarkActiveColor = Color.DimGray
        UiTextBox8.WatermarkColor = Color.DimGray
        ' 
        ' Label16
        ' 
        Label16.Dock = DockStyle.Left
        Label16.Font = New Font("微软雅黑", 9.75F)
        Label16.Location = New Point(325, 0)
        Label16.Name = "Label16"
        Label16.Padding = New Padding(0, 0, 10, 0)
        Label16.Size = New Size(100, 30)
        Label16.TabIndex = 97
        Label16.Text = "字幕语言"
        Label16.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' UiTextBox9
        ' 
        UiTextBox9.Dock = DockStyle.Left
        UiTextBox9.FillColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiTextBox9.FillColor2 = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiTextBox9.FillDisableColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiTextBox9.FillReadOnlyColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiTextBox9.Font = New Font("微软雅黑", 9.75F)
        UiTextBox9.ForeColor = Color.DarkGray
        UiTextBox9.ForeDisableColor = Color.DarkGray
        UiTextBox9.ForeReadOnlyColor = Color.DarkGray
        UiTextBox9.Location = New Point(125, 0)
        UiTextBox9.Margin = New Padding(4, 5, 4, 5)
        UiTextBox9.MinimumSize = New Size(1, 16)
        UiTextBox9.Name = "UiTextBox9"
        UiTextBox9.Padding = New Padding(5)
        UiTextBox9.Radius = 30
        UiTextBox9.RectColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        UiTextBox9.RectDisableColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        UiTextBox9.RectReadOnlyColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        UiTextBox9.ScrollBarBackColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiTextBox9.ScrollBarColor = Color.DimGray
        UiTextBox9.ScrollBarStyleInherited = False
        UiTextBox9.ShowText = False
        UiTextBox9.Size = New Size(200, 30)
        UiTextBox9.TabIndex = 96
        UiTextBox9.TextAlignment = ContentAlignment.MiddleLeft
        UiTextBox9.Watermark = "多个流用英文逗号隔开"
        UiTextBox9.WatermarkActiveColor = Color.DimGray
        UiTextBox9.WatermarkColor = Color.DimGray
        ' 
        ' Label17
        ' 
        Label17.Dock = DockStyle.Left
        Label17.Font = New Font("微软雅黑", 9.75F)
        Label17.Location = New Point(0, 0)
        Label17.Name = "Label17"
        Label17.Size = New Size(125, 30)
        Label17.TabIndex = 95
        Label17.Text = "字幕流索引号"
        Label17.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Panel6
        ' 
        Panel6.Controls.Add(UiTextBox4)
        Panel6.Controls.Add(Label12)
        Panel6.Controls.Add(UiTextBox5)
        Panel6.Controls.Add(Label13)
        Panel6.Controls.Add(UiTextBox6)
        Panel6.Controls.Add(Label14)
        Panel6.Dock = DockStyle.Top
        Panel6.Location = New Point(0, 50)
        Panel6.Name = "Panel6"
        Panel6.Padding = New Padding(0, 0, 0, 10)
        Panel6.Size = New Size(1040, 40)
        Panel6.TabIndex = 6
        ' 
        ' UiTextBox4
        ' 
        UiTextBox4.Dock = DockStyle.Fill
        UiTextBox4.FillColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiTextBox4.FillColor2 = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiTextBox4.FillDisableColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiTextBox4.FillReadOnlyColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiTextBox4.Font = New Font("微软雅黑", 9.75F)
        UiTextBox4.ForeColor = Color.DarkGray
        UiTextBox4.ForeDisableColor = Color.DarkGray
        UiTextBox4.ForeReadOnlyColor = Color.DarkGray
        UiTextBox4.Location = New Point(625, 0)
        UiTextBox4.Margin = New Padding(4, 5, 4, 5)
        UiTextBox4.MinimumSize = New Size(1, 16)
        UiTextBox4.Name = "UiTextBox4"
        UiTextBox4.Padding = New Padding(5)
        UiTextBox4.Radius = 30
        UiTextBox4.RectColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        UiTextBox4.RectDisableColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        UiTextBox4.RectReadOnlyColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        UiTextBox4.ScrollBarBackColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiTextBox4.ScrollBarColor = Color.DimGray
        UiTextBox4.ScrollBarStyleInherited = False
        UiTextBox4.ShowText = False
        UiTextBox4.Size = New Size(415, 30)
        UiTextBox4.TabIndex = 100
        UiTextBox4.TextAlignment = ContentAlignment.MiddleLeft
        UiTextBox4.Watermark = "设置不同的语言和名称请多次添加文件"
        UiTextBox4.WatermarkActiveColor = Color.DimGray
        UiTextBox4.WatermarkColor = Color.DimGray
        ' 
        ' Label12
        ' 
        Label12.Dock = DockStyle.Left
        Label12.Font = New Font("微软雅黑", 9.75F)
        Label12.Location = New Point(525, 0)
        Label12.Name = "Label12"
        Label12.Padding = New Padding(0, 0, 10, 0)
        Label12.Size = New Size(100, 30)
        Label12.TabIndex = 99
        Label12.Text = "音频流名称"
        Label12.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' UiTextBox5
        ' 
        UiTextBox5.Dock = DockStyle.Left
        UiTextBox5.FillColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiTextBox5.FillColor2 = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiTextBox5.FillDisableColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiTextBox5.FillReadOnlyColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiTextBox5.Font = New Font("微软雅黑", 9.75F)
        UiTextBox5.ForeColor = Color.DarkGray
        UiTextBox5.ForeDisableColor = Color.DarkGray
        UiTextBox5.ForeReadOnlyColor = Color.DarkGray
        UiTextBox5.Location = New Point(425, 0)
        UiTextBox5.Margin = New Padding(4, 5, 4, 5)
        UiTextBox5.MinimumSize = New Size(1, 16)
        UiTextBox5.Name = "UiTextBox5"
        UiTextBox5.Padding = New Padding(5)
        UiTextBox5.Radius = 30
        UiTextBox5.RectColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        UiTextBox5.RectDisableColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        UiTextBox5.RectReadOnlyColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        UiTextBox5.ScrollBarBackColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiTextBox5.ScrollBarColor = Color.DimGray
        UiTextBox5.ScrollBarStyleInherited = False
        UiTextBox5.ShowText = False
        UiTextBox5.Size = New Size(100, 30)
        UiTextBox5.TabIndex = 98
        UiTextBox5.TextAlignment = ContentAlignment.MiddleLeft
        UiTextBox5.Watermark = ""
        UiTextBox5.WatermarkActiveColor = Color.DimGray
        UiTextBox5.WatermarkColor = Color.DimGray
        ' 
        ' Label13
        ' 
        Label13.Dock = DockStyle.Left
        Label13.Font = New Font("微软雅黑", 9.75F)
        Label13.Location = New Point(325, 0)
        Label13.Name = "Label13"
        Label13.Padding = New Padding(0, 0, 10, 0)
        Label13.Size = New Size(100, 30)
        Label13.TabIndex = 97
        Label13.Text = "音频语言"
        Label13.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' UiTextBox6
        ' 
        UiTextBox6.Dock = DockStyle.Left
        UiTextBox6.FillColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiTextBox6.FillColor2 = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiTextBox6.FillDisableColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiTextBox6.FillReadOnlyColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiTextBox6.Font = New Font("微软雅黑", 9.75F)
        UiTextBox6.ForeColor = Color.DarkGray
        UiTextBox6.ForeDisableColor = Color.DarkGray
        UiTextBox6.ForeReadOnlyColor = Color.DarkGray
        UiTextBox6.Location = New Point(125, 0)
        UiTextBox6.Margin = New Padding(4, 5, 4, 5)
        UiTextBox6.MinimumSize = New Size(1, 16)
        UiTextBox6.Name = "UiTextBox6"
        UiTextBox6.Padding = New Padding(5)
        UiTextBox6.Radius = 30
        UiTextBox6.RectColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        UiTextBox6.RectDisableColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        UiTextBox6.RectReadOnlyColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        UiTextBox6.ScrollBarBackColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiTextBox6.ScrollBarColor = Color.DimGray
        UiTextBox6.ScrollBarStyleInherited = False
        UiTextBox6.ShowText = False
        UiTextBox6.Size = New Size(200, 30)
        UiTextBox6.TabIndex = 96
        UiTextBox6.TextAlignment = ContentAlignment.MiddleLeft
        UiTextBox6.Watermark = "多个流用英文逗号隔开"
        UiTextBox6.WatermarkActiveColor = Color.DimGray
        UiTextBox6.WatermarkColor = Color.DimGray
        ' 
        ' Label14
        ' 
        Label14.Dock = DockStyle.Left
        Label14.Font = New Font("微软雅黑", 9.75F)
        Label14.Location = New Point(0, 0)
        Label14.Name = "Label14"
        Label14.Size = New Size(125, 30)
        Label14.TabIndex = 95
        Label14.Text = "音频流索引号"
        Label14.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Panel62
        ' 
        Panel62.Controls.Add(UiTextBox3)
        Panel62.Controls.Add(Label8)
        Panel62.Controls.Add(UiTextBox2)
        Panel62.Controls.Add(Label7)
        Panel62.Controls.Add(UiTextBox1)
        Panel62.Controls.Add(Label101)
        Panel62.Dock = DockStyle.Top
        Panel62.Location = New Point(0, 10)
        Panel62.Name = "Panel62"
        Panel62.Padding = New Padding(0, 0, 0, 10)
        Panel62.Size = New Size(1040, 40)
        Panel62.TabIndex = 5
        ' 
        ' UiTextBox3
        ' 
        UiTextBox3.Dock = DockStyle.Fill
        UiTextBox3.FillColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiTextBox3.FillColor2 = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiTextBox3.FillDisableColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiTextBox3.FillReadOnlyColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiTextBox3.Font = New Font("微软雅黑", 9.75F)
        UiTextBox3.ForeColor = Color.DarkGray
        UiTextBox3.ForeDisableColor = Color.DarkGray
        UiTextBox3.ForeReadOnlyColor = Color.DarkGray
        UiTextBox3.Location = New Point(625, 0)
        UiTextBox3.Margin = New Padding(4, 5, 4, 5)
        UiTextBox3.MinimumSize = New Size(1, 16)
        UiTextBox3.Name = "UiTextBox3"
        UiTextBox3.Padding = New Padding(5)
        UiTextBox3.Radius = 30
        UiTextBox3.RectColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        UiTextBox3.RectDisableColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        UiTextBox3.RectReadOnlyColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        UiTextBox3.ScrollBarBackColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiTextBox3.ScrollBarColor = Color.DimGray
        UiTextBox3.ScrollBarStyleInherited = False
        UiTextBox3.ShowText = False
        UiTextBox3.Size = New Size(415, 30)
        UiTextBox3.TabIndex = 100
        UiTextBox3.TextAlignment = ContentAlignment.MiddleLeft
        UiTextBox3.Watermark = "设置不同的语言和名称请多次添加文件"
        UiTextBox3.WatermarkActiveColor = Color.DimGray
        UiTextBox3.WatermarkColor = Color.DimGray
        ' 
        ' Label8
        ' 
        Label8.Dock = DockStyle.Left
        Label8.Font = New Font("微软雅黑", 9.75F)
        Label8.Location = New Point(525, 0)
        Label8.Name = "Label8"
        Label8.Padding = New Padding(0, 0, 10, 0)
        Label8.Size = New Size(100, 30)
        Label8.TabIndex = 99
        Label8.Text = "视频流名称"
        Label8.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' UiTextBox2
        ' 
        UiTextBox2.Dock = DockStyle.Left
        UiTextBox2.FillColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiTextBox2.FillColor2 = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiTextBox2.FillDisableColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiTextBox2.FillReadOnlyColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiTextBox2.Font = New Font("微软雅黑", 9.75F)
        UiTextBox2.ForeColor = Color.DarkGray
        UiTextBox2.ForeDisableColor = Color.DarkGray
        UiTextBox2.ForeReadOnlyColor = Color.DarkGray
        UiTextBox2.Location = New Point(425, 0)
        UiTextBox2.Margin = New Padding(4, 5, 4, 5)
        UiTextBox2.MinimumSize = New Size(1, 16)
        UiTextBox2.Name = "UiTextBox2"
        UiTextBox2.Padding = New Padding(5)
        UiTextBox2.Radius = 30
        UiTextBox2.RectColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        UiTextBox2.RectDisableColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        UiTextBox2.RectReadOnlyColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        UiTextBox2.ScrollBarBackColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiTextBox2.ScrollBarColor = Color.DimGray
        UiTextBox2.ScrollBarStyleInherited = False
        UiTextBox2.ShowText = False
        UiTextBox2.Size = New Size(100, 30)
        UiTextBox2.TabIndex = 98
        UiTextBox2.TextAlignment = ContentAlignment.MiddleLeft
        UiTextBox2.Watermark = ""
        UiTextBox2.WatermarkActiveColor = Color.DimGray
        UiTextBox2.WatermarkColor = Color.DimGray
        ' 
        ' Label7
        ' 
        Label7.Dock = DockStyle.Left
        Label7.Font = New Font("微软雅黑", 9.75F)
        Label7.Location = New Point(325, 0)
        Label7.Name = "Label7"
        Label7.Padding = New Padding(0, 0, 10, 0)
        Label7.Size = New Size(100, 30)
        Label7.TabIndex = 97
        Label7.Text = "视频语言"
        Label7.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' UiTextBox1
        ' 
        UiTextBox1.Dock = DockStyle.Left
        UiTextBox1.FillColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiTextBox1.FillColor2 = Color.FromArgb(CByte(36), CByte(36), CByte(36))
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
        UiTextBox1.RectColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        UiTextBox1.RectDisableColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        UiTextBox1.RectReadOnlyColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
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
        Label101.Text = "视频流索引号"
        Label101.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Panel4
        ' 
        Panel4.BackColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        Panel4.Controls.Add(Label5)
        Panel4.Controls.Add(Label4)
        Panel4.Controls.Add(Label3)
        Panel4.Controls.Add(Label9)
        Panel4.Controls.Add(Label10)
        Panel4.Dock = DockStyle.Top
        Panel4.Location = New Point(10, 58)
        Panel4.Name = "Panel4"
        Panel4.Padding = New Padding(10, 0, 0, 0)
        Panel4.Size = New Size(1040, 40)
        Panel4.TabIndex = 7
        ' 
        ' Label5
        ' 
        Label5.Dock = DockStyle.Fill
        Label5.Font = New Font("微软雅黑", 10F)
        Label5.Location = New Point(850, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(190, 40)
        Label5.TabIndex = 4
        Label5.Text = "章节和元数据"
        Label5.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label4
        ' 
        Label4.Dock = DockStyle.Left
        Label4.Font = New Font("微软雅黑", 10F)
        Label4.Location = New Point(675, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(175, 40)
        Label4.TabIndex = 3
        Label4.Text = "使用字幕流"
        Label4.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label3
        ' 
        Label3.Dock = DockStyle.Left
        Label3.Font = New Font("微软雅黑", 10F)
        Label3.Location = New Point(500, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(175, 40)
        Label3.TabIndex = 2
        Label3.Text = "使用音频流"
        Label3.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label9
        ' 
        Label9.Dock = DockStyle.Left
        Label9.Font = New Font("微软雅黑", 10F)
        Label9.Location = New Point(325, 0)
        Label9.Name = "Label9"
        Label9.Size = New Size(175, 40)
        Label9.TabIndex = 1
        Label9.Text = "使用视频流"
        Label9.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label10
        ' 
        Label10.Dock = DockStyle.Left
        Label10.Font = New Font("微软雅黑", 10F)
        Label10.Location = New Point(10, 0)
        Label10.Name = "Label10"
        Label10.Size = New Size(315, 40)
        Label10.TabIndex = 0
        Label10.Text = "输入文件"
        Label10.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Dock = DockStyle.Top
        Label1.Font = New Font("微软雅黑", 9.75F)
        Label1.ForeColor = Color.Gray
        Label1.Location = New Point(10, 39)
        Label1.Name = "Label1"
        Label1.Size = New Size(471, 19)
        Label1.TabIndex = 4
        Label1.Text = "然后选中来编辑要使用哪些流，使用键盘 F3 和 F4 来排序，或使用右键菜单操作"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Dock = DockStyle.Top
        Label2.Font = New Font("微软雅黑", 11F, FontStyle.Bold)
        Label2.Location = New Point(10, 10)
        Label2.Name = "Label2"
        Label2.Padding = New Padding(0, 0, 0, 10)
        Label2.Size = New Size(99, 29)
        Label2.TabIndex = 0
        Label2.Text = "添加输入文件"
        ' 
        ' Label11
        ' 
        Label11.Dock = DockStyle.Bottom
        Label11.Location = New Point(20, 605)
        Label11.Name = "Label11"
        Label11.Size = New Size(1060, 20)
        Label11.TabIndex = 84
        ' 
        ' Panel5
        ' 
        Panel5.AutoSize = True
        Panel5.BackColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        Panel5.Controls.Add(Panel75)
        Panel5.Dock = DockStyle.Bottom
        Panel5.Location = New Point(20, 625)
        Panel5.Name = "Panel5"
        Panel5.Padding = New Padding(10)
        Panel5.Size = New Size(1060, 55)
        Panel5.TabIndex = 85
        ' 
        ' Panel75
        ' 
        Panel75.Controls.Add(UiTextBox将视频参数用于这些流)
        Panel75.Controls.Add(Label6)
        Panel75.Controls.Add(Label132)
        Panel75.Controls.Add(UiButton1)
        Panel75.Controls.Add(UiButton启动混流)
        Panel75.Dock = DockStyle.Top
        Panel75.Location = New Point(10, 10)
        Panel75.Name = "Panel75"
        Panel75.Size = New Size(1040, 35)
        Panel75.TabIndex = 12
        ' 
        ' UiTextBox将视频参数用于这些流
        ' 
        UiTextBox将视频参数用于这些流.Dock = DockStyle.Fill
        UiTextBox将视频参数用于这些流.FillColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiTextBox将视频参数用于这些流.FillColor2 = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiTextBox将视频参数用于这些流.FillDisableColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiTextBox将视频参数用于这些流.FillReadOnlyColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiTextBox将视频参数用于这些流.Font = New Font("微软雅黑", 9.75F)
        UiTextBox将视频参数用于这些流.ForeColor = Color.DarkGray
        UiTextBox将视频参数用于这些流.ForeDisableColor = Color.DarkGray
        UiTextBox将视频参数用于这些流.ForeReadOnlyColor = Color.DarkGray
        UiTextBox将视频参数用于这些流.Location = New Point(130, 0)
        UiTextBox将视频参数用于这些流.Margin = New Padding(4, 5, 4, 5)
        UiTextBox将视频参数用于这些流.MinimumSize = New Size(1, 16)
        UiTextBox将视频参数用于这些流.Name = "UiTextBox将视频参数用于这些流"
        UiTextBox将视频参数用于这些流.Padding = New Padding(5)
        UiTextBox将视频参数用于这些流.Radius = 35
        UiTextBox将视频参数用于这些流.RectColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        UiTextBox将视频参数用于这些流.RectDisableColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        UiTextBox将视频参数用于这些流.RectReadOnlyColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        UiTextBox将视频参数用于这些流.ScrollBarBackColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiTextBox将视频参数用于这些流.ScrollBarColor = Color.DimGray
        UiTextBox将视频参数用于这些流.ScrollBarStyleInherited = False
        UiTextBox将视频参数用于这些流.ShowText = False
        UiTextBox将视频参数用于这些流.Size = New Size(780, 35)
        UiTextBox将视频参数用于这些流.TabIndex = 100
        UiTextBox将视频参数用于这些流.TextAlignment = ContentAlignment.MiddleLeft
        UiTextBox将视频参数用于这些流.Watermark = "输出到目标位置"
        UiTextBox将视频参数用于这些流.WatermarkActiveColor = Color.DimGray
        UiTextBox将视频参数用于这些流.WatermarkColor = Color.DimGray
        ' 
        ' Label6
        ' 
        Label6.Dock = DockStyle.Right
        Label6.Location = New Point(910, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(10, 35)
        Label6.TabIndex = 99
        ' 
        ' Label132
        ' 
        Label132.Dock = DockStyle.Left
        Label132.Location = New Point(120, 0)
        Label132.Name = "Label132"
        Label132.Size = New Size(10, 35)
        Label132.TabIndex = 98
        ' 
        ' UiButton1
        ' 
        UiButton1.AllowDrop = True
        UiButton1.Dock = DockStyle.Left
        UiButton1.FillColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiButton1.FillColor2 = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiButton1.FillDisableColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiButton1.FillHoverColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiButton1.FillPressColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiButton1.FillSelectedColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiButton1.Font = New Font("微软雅黑", 10F)
        UiButton1.ForeColor = Color.YellowGreen
        UiButton1.ForeDisableColor = Color.YellowGreen
        UiButton1.ForeHoverColor = Color.YellowGreen
        UiButton1.ForePressColor = Color.YellowGreen
        UiButton1.ForeSelectedColor = Color.YellowGreen
        UiButton1.Location = New Point(0, 0)
        UiButton1.MinimumSize = New Size(1, 1)
        UiButton1.Name = "UiButton1"
        UiButton1.Radius = 0
        UiButton1.RadiusSides = Sunny.UI.UICornerRadiusSides.None
        UiButton1.RectColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiButton1.RectDisableColor = Color.FromArgb(CByte(12), CByte(12), CByte(12))
        UiButton1.RectHoverColor = Color.CornflowerBlue
        UiButton1.RectPressColor = Color.FromArgb(CByte(64), CByte(148), CByte(64))
        UiButton1.RectSelectedColor = Color.CornflowerBlue
        UiButton1.RectSize = 2
        UiButton1.Size = New Size(120, 35)
        UiButton1.TabIndex = 45
        UiButton1.Text = "选择位置"
        UiButton1.TipsFont = New Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        ' 
        ' UiButton启动混流
        ' 
        UiButton启动混流.AllowDrop = True
        UiButton启动混流.Dock = DockStyle.Right
        UiButton启动混流.FillColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiButton启动混流.FillColor2 = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiButton启动混流.FillDisableColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiButton启动混流.FillHoverColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiButton启动混流.FillPressColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiButton启动混流.FillSelectedColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiButton启动混流.Font = New Font("微软雅黑", 10F)
        UiButton启动混流.ForeColor = Color.YellowGreen
        UiButton启动混流.ForeDisableColor = Color.YellowGreen
        UiButton启动混流.ForeHoverColor = Color.YellowGreen
        UiButton启动混流.ForePressColor = Color.YellowGreen
        UiButton启动混流.ForeSelectedColor = Color.YellowGreen
        UiButton启动混流.Location = New Point(920, 0)
        UiButton启动混流.MinimumSize = New Size(1, 1)
        UiButton启动混流.Name = "UiButton启动混流"
        UiButton启动混流.Radius = 0
        UiButton启动混流.RadiusSides = Sunny.UI.UICornerRadiusSides.None
        UiButton启动混流.RectColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiButton启动混流.RectDisableColor = Color.FromArgb(CByte(12), CByte(12), CByte(12))
        UiButton启动混流.RectHoverColor = Color.CornflowerBlue
        UiButton启动混流.RectPressColor = Color.FromArgb(CByte(64), CByte(148), CByte(64))
        UiButton启动混流.RectSelectedColor = Color.CornflowerBlue
        UiButton启动混流.RectSize = 2
        UiButton启动混流.Size = New Size(120, 35)
        UiButton启动混流.TabIndex = 44
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
        Padding = New Padding(20)
        Size = New Size(1100, 700)
        Panel73.ResumeLayout(False)
        Panel73.PerformLayout()
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        Panel2.ResumeLayout(False)
        Panel3.ResumeLayout(False)
        Panel8.ResumeLayout(False)
        Panel7.ResumeLayout(False)
        Panel6.ResumeLayout(False)
        Panel62.ResumeLayout(False)
        Panel4.ResumeLayout(False)
        Panel5.ResumeLayout(False)
        Panel75.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Panel73 As Panel
    Friend WithEvents Label123 As Label
    Friend WithEvents Label20 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents ListView1 As ListView
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel75 As Panel
    Friend WithEvents UiButton启动混流 As Sunny.UI.UIButton
    Friend WithEvents UiButton1 As Sunny.UI.UIButton
    Friend WithEvents Label6 As Label
    Friend WithEvents Label132 As Label
    Friend WithEvents UiTextBox将视频参数用于这些流 As Sunny.UI.UITextBox
    Friend WithEvents Panel62 As Panel
    Friend WithEvents UiTextBox1 As Sunny.UI.UITextBox
    Friend WithEvents Label101 As Label
    Friend WithEvents UiTextBox2 As Sunny.UI.UITextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents UiTextBox3 As Sunny.UI.UITextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents Panel8 As Panel
    Friend WithEvents Label21 As Label
    Friend WithEvents Panel7 As Panel
    Friend WithEvents UiTextBox7 As Sunny.UI.UITextBox
    Friend WithEvents Label15 As Label
    Friend WithEvents UiTextBox8 As Sunny.UI.UITextBox
    Friend WithEvents Label16 As Label
    Friend WithEvents UiTextBox9 As Sunny.UI.UITextBox
    Friend WithEvents Label17 As Label
    Friend WithEvents Panel6 As Panel
    Friend WithEvents UiTextBox4 As Sunny.UI.UITextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents UiTextBox5 As Sunny.UI.UITextBox
    Friend WithEvents Label13 As Label
    Friend WithEvents UiTextBox6 As Sunny.UI.UITextBox
    Friend WithEvents Label14 As Label
    Friend WithEvents UiCheckBox保留其他视频流 As Sunny.UI.UICheckBox
    Friend WithEvents UiCheckBox1 As Sunny.UI.UICheckBox

End Class
