<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class 界面_合并
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
        Panel1 = New Panel()
        Panel2 = New Panel()
        ListView1 = New ListView()
        ColumnHeader1 = New ColumnHeader()
        Panel4 = New Panel()
        Label1 = New Label()
        UiButton移除 = New Sunny.UI.UIButton()
        Label3 = New Label()
        UiButton下移 = New Sunny.UI.UIButton()
        Label10 = New Label()
        UiButton上移 = New Sunny.UI.UIButton()
        Label8 = New Label()
        UiButton添加文件 = New Sunny.UI.UIButton()
        Label11 = New Label()
        Label20 = New Label()
        Panel73 = New Panel()
        Label123 = New Label()
        Panel5 = New Panel()
        UiTextBox输出文件 = New Sunny.UI.UITextBox()
        Label6 = New Label()
        Label132 = New Label()
        UiButton选择位置 = New Sunny.UI.UIButton()
        UiButton启动合并 = New Sunny.UI.UIButton()
        Panel1.SuspendLayout()
        Panel2.SuspendLayout()
        Panel4.SuspendLayout()
        Panel73.SuspendLayout()
        Panel5.SuspendLayout()
        SuspendLayout()
        ' 
        ' Panel1
        ' 
        Panel1.AutoSize = True
        Panel1.BackColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        Panel1.Controls.Add(Panel2)
        Panel1.Controls.Add(Panel4)
        Panel1.Dock = DockStyle.Fill
        Panel1.Location = New Point(20, 98)
        Panel1.Name = "Panel1"
        Panel1.Padding = New Padding(10)
        Panel1.Size = New Size(1060, 512)
        Panel1.TabIndex = 88
        ' 
        ' Panel2
        ' 
        Panel2.BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        Panel2.Controls.Add(ListView1)
        Panel2.Dock = DockStyle.Fill
        Panel2.Location = New Point(10, 50)
        Panel2.Name = "Panel2"
        Panel2.Padding = New Padding(10, 10, 0, 10)
        Panel2.Size = New Size(1040, 452)
        Panel2.TabIndex = 5
        ' 
        ' ListView1
        ' 
        ListView1.AllowDrop = True
        ListView1.BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ListView1.BorderStyle = BorderStyle.None
        ListView1.Columns.AddRange(New ColumnHeader() {ColumnHeader1})
        ListView1.Dock = DockStyle.Fill
        ListView1.ForeColor = Color.Silver
        ListView1.FullRowSelect = True
        ListView1.HeaderStyle = ColumnHeaderStyle.None
        ListView1.Location = New Point(10, 10)
        ListView1.Name = "ListView1"
        ListView1.OwnerDraw = True
        ListView1.Size = New Size(1030, 432)
        ListView1.TabIndex = 0
        ListView1.UseCompatibleStateImageBehavior = False
        ListView1.View = View.Details
        ' 
        ' Panel4
        ' 
        Panel4.BackColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        Panel4.Controls.Add(Label1)
        Panel4.Controls.Add(UiButton移除)
        Panel4.Controls.Add(Label3)
        Panel4.Controls.Add(UiButton下移)
        Panel4.Controls.Add(Label10)
        Panel4.Controls.Add(UiButton上移)
        Panel4.Controls.Add(Label8)
        Panel4.Controls.Add(UiButton添加文件)
        Panel4.Dock = DockStyle.Top
        Panel4.Location = New Point(10, 10)
        Panel4.Name = "Panel4"
        Panel4.Padding = New Padding(0, 0, 0, 10)
        Panel4.Size = New Size(1040, 40)
        Panel4.TabIndex = 7
        ' 
        ' Label1
        ' 
        Label1.Dock = DockStyle.Fill
        Label1.Font = New Font("微软雅黑", 9.75F)
        Label1.ForeColor = Color.Gray
        Label1.Location = New Point(375, 0)
        Label1.Name = "Label1"
        Label1.Padding = New Padding(10, 0, 0, 0)
        Label1.Size = New Size(665, 30)
        Label1.TabIndex = 105
        Label1.Text = "使用键盘 F3 和 F4 来排序，Delete 来移除"
        Label1.TextAlign = ContentAlignment.MiddleLeft
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
        UiButton移除.Location = New Point(300, 0)
        UiButton移除.MinimumSize = New Size(1, 1)
        UiButton移除.Name = "UiButton移除"
        UiButton移除.Radius = 30
        UiButton移除.RectColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton移除.RectDisableColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton移除.RectHoverColor = Color.IndianRed
        UiButton移除.RectPressColor = Color.FromArgb(CByte(64), CByte(148), CByte(64))
        UiButton移除.RectSelectedColor = Color.IndianRed
        UiButton移除.Size = New Size(75, 30)
        UiButton移除.TabIndex = 104
        UiButton移除.Text = "移除"
        UiButton移除.TipsFont = New Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        ' 
        ' Label3
        ' 
        Label3.Dock = DockStyle.Left
        Label3.Location = New Point(290, 0)
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
        UiButton下移.Location = New Point(215, 0)
        UiButton下移.MinimumSize = New Size(1, 1)
        UiButton下移.Name = "UiButton下移"
        UiButton下移.Radius = 30
        UiButton下移.RectColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton下移.RectDisableColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton下移.RectHoverColor = Color.CornflowerBlue
        UiButton下移.RectPressColor = Color.FromArgb(CByte(64), CByte(148), CByte(64))
        UiButton下移.RectSelectedColor = Color.CornflowerBlue
        UiButton下移.Size = New Size(75, 30)
        UiButton下移.TabIndex = 102
        UiButton下移.Text = "下移"
        UiButton下移.TipsFont = New Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        ' 
        ' Label10
        ' 
        Label10.Dock = DockStyle.Left
        Label10.Location = New Point(205, 0)
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
        UiButton上移.Location = New Point(130, 0)
        UiButton上移.MinimumSize = New Size(1, 1)
        UiButton上移.Name = "UiButton上移"
        UiButton上移.Radius = 30
        UiButton上移.RectColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton上移.RectDisableColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton上移.RectHoverColor = Color.CornflowerBlue
        UiButton上移.RectPressColor = Color.FromArgb(CByte(64), CByte(148), CByte(64))
        UiButton上移.RectSelectedColor = Color.CornflowerBlue
        UiButton上移.Size = New Size(75, 30)
        UiButton上移.TabIndex = 100
        UiButton上移.Text = "上移"
        UiButton上移.TipsFont = New Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        ' 
        ' Label8
        ' 
        Label8.Dock = DockStyle.Left
        Label8.Location = New Point(120, 0)
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
        UiButton添加文件.Location = New Point(0, 0)
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
        ' Label11
        ' 
        Label11.Dock = DockStyle.Bottom
        Label11.Location = New Point(20, 610)
        Label11.Name = "Label11"
        Label11.Size = New Size(1060, 20)
        Label11.TabIndex = 89
        ' 
        ' Label20
        ' 
        Label20.Dock = DockStyle.Top
        Label20.Location = New Point(20, 78)
        Label20.Name = "Label20"
        Label20.Size = New Size(1060, 20)
        Label20.TabIndex = 87
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
        Panel73.TabIndex = 86
        ' 
        ' Label123
        ' 
        Label123.AutoSize = True
        Label123.Dock = DockStyle.Top
        Label123.Font = New Font("微软雅黑", 9.75F)
        Label123.ForeColor = Color.Gray
        Label123.Location = New Point(10, 10)
        Label123.Name = "Label123"
        Label123.Size = New Size(477, 38)
        Label123.TabIndex = 4
        Label123.Text = "此功能为扩展，不走编码队列，直接让 ffmpeg 以原样运行" & vbCrLf & "仅提供最基础的合并，仅复制流，要求多个参数一致；高级需求请直接用剪辑软件"
        ' 
        ' Panel5
        ' 
        Panel5.BackColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        Panel5.Controls.Add(UiTextBox输出文件)
        Panel5.Controls.Add(Label6)
        Panel5.Controls.Add(Label132)
        Panel5.Controls.Add(UiButton选择位置)
        Panel5.Controls.Add(UiButton启动合并)
        Panel5.Dock = DockStyle.Bottom
        Panel5.Location = New Point(20, 630)
        Panel5.Name = "Panel5"
        Panel5.Padding = New Padding(10)
        Panel5.Size = New Size(1060, 50)
        Panel5.TabIndex = 90
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
        UiTextBox输出文件.Size = New Size(780, 30)
        UiTextBox输出文件.TabIndex = 105
        UiTextBox输出文件.TextAlignment = ContentAlignment.MiddleLeft
        UiTextBox输出文件.Watermark = "输出到目标位置"
        UiTextBox输出文件.WatermarkActiveColor = Color.DimGray
        UiTextBox输出文件.WatermarkColor = Color.DimGray
        ' 
        ' Label6
        ' 
        Label6.Dock = DockStyle.Right
        Label6.Location = New Point(920, 10)
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
        ' UiButton启动合并
        ' 
        UiButton启动合并.AllowDrop = True
        UiButton启动合并.Dock = DockStyle.Right
        UiButton启动合并.FillColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton启动合并.FillColor2 = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton启动合并.FillDisableColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton启动合并.FillHoverColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton启动合并.FillPressColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton启动合并.FillSelectedColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton启动合并.Font = New Font("微软雅黑", 9.75F)
        UiButton启动合并.ForeColor = Color.YellowGreen
        UiButton启动合并.ForeDisableColor = Color.YellowGreen
        UiButton启动合并.ForeHoverColor = Color.YellowGreen
        UiButton启动合并.ForePressColor = Color.YellowGreen
        UiButton启动合并.ForeSelectedColor = Color.YellowGreen
        UiButton启动合并.Location = New Point(930, 10)
        UiButton启动合并.MinimumSize = New Size(1, 1)
        UiButton启动合并.Name = "UiButton启动合并"
        UiButton启动合并.Radius = 30
        UiButton启动合并.RectColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton启动合并.RectDisableColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton启动合并.RectHoverColor = Color.YellowGreen
        UiButton启动合并.RectPressColor = Color.FromArgb(CByte(64), CByte(148), CByte(64))
        UiButton启动合并.RectSelectedColor = Color.YellowGreen
        UiButton启动合并.Size = New Size(120, 30)
        UiButton启动合并.TabIndex = 101
        UiButton启动合并.Text = "启动合并"
        UiButton启动合并.TipsFont = New Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        ' 
        ' 界面_合并
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
        Name = "界面_合并"
        Padding = New Padding(20)
        Size = New Size(1100, 700)
        Panel1.ResumeLayout(False)
        Panel2.ResumeLayout(False)
        Panel4.ResumeLayout(False)
        Panel73.ResumeLayout(False)
        Panel73.PerformLayout()
        Panel5.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents ListView1 As ListView
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents Panel4 As Panel
    Friend WithEvents UiButton移除 As Sunny.UI.UIButton
    Friend WithEvents Label3 As Label
    Friend WithEvents UiButton下移 As Sunny.UI.UIButton
    Friend WithEvents Label10 As Label
    Friend WithEvents UiButton上移 As Sunny.UI.UIButton
    Friend WithEvents Label8 As Label
    Friend WithEvents UiButton添加文件 As Sunny.UI.UIButton
    Friend WithEvents Label11 As Label
    Friend WithEvents Label20 As Label
    Friend WithEvents Panel73 As Panel
    Friend WithEvents Label123 As Label
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents UiTextBox输出文件 As Sunny.UI.UITextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label132 As Label
    Friend WithEvents UiButton选择位置 As Sunny.UI.UIButton
    Friend WithEvents UiButton启动合并 As Sunny.UI.UIButton

End Class
