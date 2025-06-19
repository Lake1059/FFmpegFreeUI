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
        Panel62 = New Panel()
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
        Panel2.Size = New Size(1040, 235)
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
        ListView1.Size = New Size(1040, 215)
        ListView1.TabIndex = 0
        ListView1.UseCompatibleStateImageBehavior = False
        ' 
        ' Panel3
        ' 
        Panel3.Controls.Add(Panel62)
        Panel3.Dock = DockStyle.Bottom
        Panel3.Location = New Point(10, 333)
        Panel3.Name = "Panel3"
        Panel3.Size = New Size(1040, 164)
        Panel3.TabIndex = 6
        ' 
        ' Panel62
        ' 
        Panel62.Controls.Add(UiTextBox2)
        Panel62.Controls.Add(Label7)
        Panel62.Controls.Add(UiTextBox1)
        Panel62.Controls.Add(Label101)
        Panel62.Dock = DockStyle.Top
        Panel62.Location = New Point(0, 0)
        Panel62.Name = "Panel62"
        Panel62.Padding = New Padding(0, 0, 0, 10)
        Panel62.Size = New Size(1040, 40)
        Panel62.TabIndex = 5
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
        UiTextBox2.Location = New Point(300, 0)
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
        Label7.Location = New Point(200, 0)
        Label7.Name = "Label7"
        Label7.Padding = New Padding(10, 0, 0, 0)
        Label7.Size = New Size(100, 30)
        Label7.TabIndex = 97
        Label7.Text = "语言"
        Label7.TextAlign = ContentAlignment.MiddleLeft
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
        UiTextBox1.Location = New Point(100, 0)
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
        UiTextBox1.Size = New Size(100, 30)
        UiTextBox1.TabIndex = 96
        UiTextBox1.TextAlignment = ContentAlignment.MiddleLeft
        UiTextBox1.Watermark = ""
        UiTextBox1.WatermarkActiveColor = Color.DimGray
        UiTextBox1.WatermarkColor = Color.DimGray
        ' 
        ' Label101
        ' 
        Label101.Dock = DockStyle.Left
        Label101.Font = New Font("微软雅黑", 9.75F)
        Label101.Location = New Point(0, 0)
        Label101.Name = "Label101"
        Label101.Size = New Size(100, 30)
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
        Label5.Location = New Point(885, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(155, 40)
        Label5.TabIndex = 4
        Label5.Text = "使用章节"
        Label5.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label4
        ' 
        Label4.Dock = DockStyle.Left
        Label4.Font = New Font("微软雅黑", 10F)
        Label4.Location = New Point(710, 0)
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
        Label3.Location = New Point(535, 0)
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
        Label9.Location = New Point(360, 0)
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
        Label10.Size = New Size(350, 40)
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
        Label1.Size = New Size(178, 19)
        Label1.TabIndex = 4
        Label1.Text = "然后选中来编辑要使用哪些流"
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
        UiButton1.Text = "导出位置"
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

End Class
