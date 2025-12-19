<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form画面裁剪交互窗口
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
        Panel75 = New Panel()
        UiCheckBox居中 = New Sunny.UI.UICheckBox()
        Label3 = New Label()
        UiComboBox比例 = New Sunny.UI.UIComboBox()
        Label2 = New Label()
        UiTextBox1 = New Sunny.UI.UITextBox()
        Label1 = New Label()
        UiTextBox画面裁剪滤镜参数 = New Sunny.UI.UITextBox()
        Label125 = New Label()
        UiButton2 = New Sunny.UI.UIButton()
        Label127 = New Label()
        UiButton1 = New Sunny.UI.UIButton()
        Panel3 = New Panel()
        Label123 = New Label()
        Label122 = New Label()
        PictureBox2 = New PictureBox()
        Label4 = New Label()
        PictureBox3 = New PictureBox()
        Label128 = New Label()
        Panel1 = New Panel()
        Panel2 = New Panel()
        PictureBox1 = New PictureBox()
        Panel73.SuspendLayout()
        Panel75.SuspendLayout()
        Panel3.SuspendLayout()
        CType(PictureBox2, ComponentModel.ISupportInitialize).BeginInit()
        CType(PictureBox3, ComponentModel.ISupportInitialize).BeginInit()
        Panel1.SuspendLayout()
        Panel2.SuspendLayout()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Panel73
        ' 
        Panel73.AutoSize = True
        Panel73.BackColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        Panel73.Controls.Add(Panel75)
        Panel73.Controls.Add(Panel3)
        Panel73.Dock = DockStyle.Top
        Panel73.Location = New Point(10, 10)
        Panel73.Name = "Panel73"
        Panel73.Padding = New Padding(10)
        Panel73.Size = New Size(864, 180)
        Panel73.TabIndex = 80
        ' 
        ' Panel75
        ' 
        Panel75.Controls.Add(UiCheckBox居中)
        Panel75.Controls.Add(Label3)
        Panel75.Controls.Add(UiComboBox比例)
        Panel75.Controls.Add(Label2)
        Panel75.Controls.Add(UiTextBox1)
        Panel75.Controls.Add(Label1)
        Panel75.Controls.Add(UiTextBox画面裁剪滤镜参数)
        Panel75.Controls.Add(Label125)
        Panel75.Controls.Add(UiButton2)
        Panel75.Controls.Add(Label127)
        Panel75.Controls.Add(UiButton1)
        Panel75.Dock = DockStyle.Top
        Panel75.Location = New Point(10, 140)
        Panel75.Name = "Panel75"
        Panel75.Padding = New Padding(3, 0, 0, 0)
        Panel75.Size = New Size(844, 30)
        Panel75.TabIndex = 11
        ' 
        ' UiCheckBox居中
        ' 
        UiCheckBox居中.CheckBoxColor = Color.Silver
        UiCheckBox居中.CheckBoxSize = 20
        UiCheckBox居中.Dock = DockStyle.Left
        UiCheckBox居中.Font = New Font("微软雅黑", 10F)
        UiCheckBox居中.ForeColor = Color.Silver
        UiCheckBox居中.Location = New Point(653, 0)
        UiCheckBox居中.MinimumSize = New Size(1, 1)
        UiCheckBox居中.Name = "UiCheckBox居中"
        UiCheckBox居中.Size = New Size(100, 30)
        UiCheckBox居中.TabIndex = 99
        UiCheckBox居中.Text = "居中"
        ' 
        ' Label3
        ' 
        Label3.Dock = DockStyle.Left
        Label3.Location = New Point(643, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(10, 30)
        Label3.TabIndex = 84
        ' 
        ' UiComboBox比例
        ' 
        UiComboBox比例.DataSource = Nothing
        UiComboBox比例.Dock = DockStyle.Left
        UiComboBox比例.DropDownAutoWidth = True
        UiComboBox比例.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList
        UiComboBox比例.FillColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiComboBox比例.FillColor2 = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiComboBox比例.FillDisableColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiComboBox比例.Font = New Font("微软雅黑", 10F)
        UiComboBox比例.ForeColor = Color.Silver
        UiComboBox比例.ForeDisableColor = Color.Silver
        UiComboBox比例.ItemFillColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiComboBox比例.ItemForeColor = Color.Silver
        UiComboBox比例.ItemHeight = 30
        UiComboBox比例.ItemHoverColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiComboBox比例.ItemRectColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiComboBox比例.Items.AddRange(New Object() {"自由", "21:9", "16:9", "3:2", "2:1", "4:3", "1:1"})
        UiComboBox比例.ItemSelectBackColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        UiComboBox比例.ItemSelectForeColor = Color.Silver
        UiComboBox比例.Location = New Point(543, 0)
        UiComboBox比例.Margin = New Padding(4, 5, 4, 5)
        UiComboBox比例.MaxDropDownItems = 20
        UiComboBox比例.MinimumSize = New Size(63, 0)
        UiComboBox比例.Name = "UiComboBox比例"
        UiComboBox比例.Padding = New Padding(0, 0, 30, 2)
        UiComboBox比例.RectColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        UiComboBox比例.RectDisableColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        UiComboBox比例.ScrollBarHandleWidth = 20
        UiComboBox比例.Size = New Size(100, 30)
        UiComboBox比例.Style = Sunny.UI.UIStyle.Custom
        UiComboBox比例.SymbolSize = 24
        UiComboBox比例.TabIndex = 83
        UiComboBox比例.TextAlignment = ContentAlignment.MiddleLeft
        UiComboBox比例.Watermark = "比例"
        ' 
        ' Label2
        ' 
        Label2.Dock = DockStyle.Left
        Label2.Location = New Point(533, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(10, 30)
        Label2.TabIndex = 82
        ' 
        ' UiTextBox1
        ' 
        UiTextBox1.Dock = DockStyle.Left
        UiTextBox1.FillColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox1.FillColor2 = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox1.FillDisableColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox1.FillReadOnlyColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox1.Font = New Font("微软雅黑", 9.75F)
        UiTextBox1.ForeColor = Color.DarkGray
        UiTextBox1.ForeDisableColor = Color.DarkGray
        UiTextBox1.ForeReadOnlyColor = Color.DarkGray
        UiTextBox1.Location = New Point(383, 0)
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
        UiTextBox1.Size = New Size(150, 30)
        UiTextBox1.TabIndex = 81
        UiTextBox1.TextAlignment = ContentAlignment.MiddleLeft
        UiTextBox1.Watermark = "指定帧时间戳"
        UiTextBox1.WatermarkActiveColor = Color.DimGray
        UiTextBox1.WatermarkColor = Color.DimGray
        ' 
        ' Label1
        ' 
        Label1.Dock = DockStyle.Left
        Label1.Location = New Point(373, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(10, 30)
        Label1.TabIndex = 80
        ' 
        ' UiTextBox画面裁剪滤镜参数
        ' 
        UiTextBox画面裁剪滤镜参数.Dock = DockStyle.Left
        UiTextBox画面裁剪滤镜参数.FillColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox画面裁剪滤镜参数.FillColor2 = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox画面裁剪滤镜参数.FillDisableColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox画面裁剪滤镜参数.FillReadOnlyColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox画面裁剪滤镜参数.Font = New Font("微软雅黑", 9.75F)
        UiTextBox画面裁剪滤镜参数.ForeColor = Color.DarkGray
        UiTextBox画面裁剪滤镜参数.ForeDisableColor = Color.DarkGray
        UiTextBox画面裁剪滤镜参数.ForeReadOnlyColor = Color.DarkGray
        UiTextBox画面裁剪滤镜参数.Location = New Point(173, 0)
        UiTextBox画面裁剪滤镜参数.Margin = New Padding(4, 5, 4, 5)
        UiTextBox画面裁剪滤镜参数.MinimumSize = New Size(1, 16)
        UiTextBox画面裁剪滤镜参数.Name = "UiTextBox画面裁剪滤镜参数"
        UiTextBox画面裁剪滤镜参数.Padding = New Padding(5)
        UiTextBox画面裁剪滤镜参数.Radius = 30
        UiTextBox画面裁剪滤镜参数.RectColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox画面裁剪滤镜参数.RectDisableColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox画面裁剪滤镜参数.RectReadOnlyColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox画面裁剪滤镜参数.ScrollBarBackColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiTextBox画面裁剪滤镜参数.ScrollBarColor = Color.DimGray
        UiTextBox画面裁剪滤镜参数.ScrollBarStyleInherited = False
        UiTextBox画面裁剪滤镜参数.ShowText = False
        UiTextBox画面裁剪滤镜参数.Size = New Size(200, 30)
        UiTextBox画面裁剪滤镜参数.TabIndex = 79
        UiTextBox画面裁剪滤镜参数.TextAlignment = ContentAlignment.MiddleLeft
        UiTextBox画面裁剪滤镜参数.Watermark = "宽:高:左上X:左上Y"
        UiTextBox画面裁剪滤镜参数.WatermarkActiveColor = Color.DimGray
        UiTextBox画面裁剪滤镜参数.WatermarkColor = Color.DimGray
        ' 
        ' Label125
        ' 
        Label125.Dock = DockStyle.Left
        Label125.Location = New Point(163, 0)
        Label125.Name = "Label125"
        Label125.Size = New Size(10, 30)
        Label125.TabIndex = 47
        ' 
        ' UiButton2
        ' 
        UiButton2.Dock = DockStyle.Left
        UiButton2.FillColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton2.FillColor2 = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton2.FillDisableColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton2.FillHoverColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton2.FillPressColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton2.FillSelectedColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton2.Font = New Font("微软雅黑", 9.75F)
        UiButton2.ForeColor = Color.Goldenrod
        UiButton2.ForeDisableColor = Color.Goldenrod
        UiButton2.ForeHoverColor = Color.Goldenrod
        UiButton2.ForePressColor = Color.Goldenrod
        UiButton2.ForeSelectedColor = Color.Goldenrod
        UiButton2.Location = New Point(88, 0)
        UiButton2.MinimumSize = New Size(1, 1)
        UiButton2.Name = "UiButton2"
        UiButton2.Radius = 30
        UiButton2.RectColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton2.RectDisableColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton2.RectHoverColor = Color.Goldenrod
        UiButton2.RectPressColor = Color.FromArgb(CByte(64), CByte(148), CByte(64))
        UiButton2.RectSelectedColor = Color.Goldenrod
        UiButton2.Size = New Size(75, 30)
        UiButton2.TabIndex = 46
        UiButton2.Text = "完成"
        UiButton2.TipsFont = New Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        ' 
        ' Label127
        ' 
        Label127.Dock = DockStyle.Left
        Label127.Location = New Point(78, 0)
        Label127.Name = "Label127"
        Label127.Size = New Size(10, 30)
        Label127.TabIndex = 45
        ' 
        ' UiButton1
        ' 
        UiButton1.Dock = DockStyle.Left
        UiButton1.FillColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton1.FillColor2 = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton1.FillDisableColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton1.FillHoverColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton1.FillPressColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton1.FillSelectedColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton1.Font = New Font("微软雅黑", 9.75F)
        UiButton1.ForeColor = Color.YellowGreen
        UiButton1.ForeDisableColor = Color.YellowGreen
        UiButton1.ForeHoverColor = Color.YellowGreen
        UiButton1.ForePressColor = Color.YellowGreen
        UiButton1.ForeSelectedColor = Color.YellowGreen
        UiButton1.Location = New Point(3, 0)
        UiButton1.MinimumSize = New Size(1, 1)
        UiButton1.Name = "UiButton1"
        UiButton1.Radius = 30
        UiButton1.RectColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton1.RectDisableColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton1.RectHoverColor = Color.YellowGreen
        UiButton1.RectPressColor = Color.FromArgb(CByte(64), CByte(148), CByte(64))
        UiButton1.RectSelectedColor = Color.YellowGreen
        UiButton1.Size = New Size(75, 30)
        UiButton1.TabIndex = 44
        UiButton1.Text = "打开"
        UiButton1.TipsFont = New Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        ' 
        ' Panel3
        ' 
        Panel3.Controls.Add(Label123)
        Panel3.Controls.Add(Label122)
        Panel3.Controls.Add(PictureBox2)
        Panel3.Controls.Add(Label4)
        Panel3.Controls.Add(PictureBox3)
        Panel3.Dock = DockStyle.Top
        Panel3.Location = New Point(10, 10)
        Panel3.Name = "Panel3"
        Panel3.Padding = New Padding(0, 0, 0, 15)
        Panel3.Size = New Size(844, 130)
        Panel3.TabIndex = 12
        ' 
        ' Label123
        ' 
        Label123.AutoSize = True
        Label123.Dock = DockStyle.Top
        Label123.Font = New Font("微软雅黑", 9.75F)
        Label123.ForeColor = Color.Gray
        Label123.Location = New Point(0, 30)
        Label123.Name = "Label123"
        Label123.Size = New Size(503, 76)
        Label123.TabIndex = 5
        Label123.Text = "红框是截取目标，其自己占用的像素也包含在截取中；绿框在图片外围，用作明显边界" & vbCrLf & "鼠标左键拖动来调整左上角坐标，右键拖动来调整右下角坐标；越界了会互换" & vbCrLf & "可以自己改坐标文本框，然后按 Enter 键刷新红框，这样反过来操作" & vbCrLf & "请勿乱玩框线，全程使用 CPU 绘制，刷新时 UI 线程满载运行，高刷显示器务必注意"
        ' 
        ' Label122
        ' 
        Label122.AutoSize = True
        Label122.Dock = DockStyle.Top
        Label122.Font = New Font("微软雅黑", 10F)
        Label122.Location = New Point(0, 0)
        Label122.Name = "Label122"
        Label122.Padding = New Padding(0, 0, 0, 10)
        Label122.Size = New Size(135, 30)
        Label122.TabIndex = 85
        Label122.Text = "可视化裁剪使用说明"
        ' 
        ' PictureBox2
        ' 
        PictureBox2.BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        PictureBox2.Dock = DockStyle.Right
        PictureBox2.Location = New Point(604, 0)
        PictureBox2.Name = "PictureBox2"
        PictureBox2.Size = New Size(115, 115)
        PictureBox2.SizeMode = PictureBoxSizeMode.Zoom
        PictureBox2.TabIndex = 6
        PictureBox2.TabStop = False
        ' 
        ' Label4
        ' 
        Label4.Dock = DockStyle.Right
        Label4.Location = New Point(719, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(10, 115)
        Label4.TabIndex = 83
        ' 
        ' PictureBox3
        ' 
        PictureBox3.BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        PictureBox3.Dock = DockStyle.Right
        PictureBox3.Location = New Point(729, 0)
        PictureBox3.Name = "PictureBox3"
        PictureBox3.Size = New Size(115, 115)
        PictureBox3.SizeMode = PictureBoxSizeMode.Zoom
        PictureBox3.TabIndex = 84
        PictureBox3.TabStop = False
        ' 
        ' Label128
        ' 
        Label128.Dock = DockStyle.Top
        Label128.Location = New Point(10, 190)
        Label128.Name = "Label128"
        Label128.Size = New Size(864, 10)
        Label128.TabIndex = 83
        ' 
        ' Panel1
        ' 
        Panel1.AllowDrop = True
        Panel1.AutoScroll = True
        Panel1.BackColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        Panel1.Controls.Add(Panel2)
        Panel1.Dock = DockStyle.Fill
        Panel1.Location = New Point(10, 200)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(864, 401)
        Panel1.TabIndex = 84
        ' 
        ' Panel2
        ' 
        Panel2.AllowDrop = True
        Panel2.AutoSize = True
        Panel2.BackColor = Color.YellowGreen
        Panel2.Controls.Add(PictureBox1)
        Panel2.Location = New Point(0, 0)
        Panel2.Margin = New Padding(0)
        Panel2.Name = "Panel2"
        Panel2.Padding = New Padding(1)
        Panel2.Size = New Size(102, 102)
        Panel2.TabIndex = 2
        Panel2.Visible = False
        ' 
        ' PictureBox1
        ' 
        PictureBox1.BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        PictureBox1.Location = New Point(1, 1)
        PictureBox1.Margin = New Padding(0)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(100, 100)
        PictureBox1.SizeMode = PictureBoxSizeMode.AutoSize
        PictureBox1.TabIndex = 1
        PictureBox1.TabStop = False
        ' 
        ' Form画面裁剪交互窗口
        ' 
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ClientSize = New Size(884, 611)
        Controls.Add(Panel1)
        Controls.Add(Label128)
        Controls.Add(Panel73)
        DoubleBuffered = True
        Font = New Font("微软雅黑", 10F)
        ForeColor = Color.Silver
        MinimumSize = New Size(900, 650)
        Name = "Form画面裁剪交互窗口"
        Padding = New Padding(10)
        ShowIcon = False
        ShowInTaskbar = False
        StartPosition = FormStartPosition.Manual
        Text = "画面裁剪交互窗口"
        Panel73.ResumeLayout(False)
        Panel75.ResumeLayout(False)
        Panel3.ResumeLayout(False)
        Panel3.PerformLayout()
        CType(PictureBox2, ComponentModel.ISupportInitialize).EndInit()
        CType(PictureBox3, ComponentModel.ISupportInitialize).EndInit()
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        Panel2.ResumeLayout(False)
        Panel2.PerformLayout()
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Panel73 As Panel
    Friend WithEvents Panel75 As Panel
    Friend WithEvents Label125 As Label
    Friend WithEvents UiButton2 As Sunny.UI.UIButton
    Friend WithEvents Label127 As Label
    Friend WithEvents UiButton1 As Sunny.UI.UIButton
    Friend WithEvents Label128 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents UiTextBox画面裁剪滤镜参数 As Sunny.UI.UITextBox
    Friend WithEvents UiTextBox1 As Sunny.UI.UITextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents UiComboBox比例 As Sunny.UI.UIComboBox
    Friend WithEvents UiCheckBox居中 As Sunny.UI.UICheckBox
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Label123 As Label
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents Label4 As Label
    Friend WithEvents PictureBox3 As PictureBox
    Friend WithEvents Label122 As Label
End Class
