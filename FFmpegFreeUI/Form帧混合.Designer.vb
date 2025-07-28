<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form帧混合
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
        Label124 = New Label()
        Panel8 = New Panel()
        Panel11 = New Panel()
        Label13 = New Label()
        UiTextBox降低帧率 = New Sunny.UI.UITextBox()
        Label1 = New Label()
        Label2 = New Label()
        Panel1 = New Panel()
        Panel2 = New Panel()
        Label3 = New Label()
        UiComboBox混合算法 = New Sunny.UI.UIComboBox()
        Label4 = New Label()
        Label5 = New Label()
        Panel3 = New Panel()
        Panel4 = New Panel()
        Label6 = New Label()
        UiTextBox混合比例 = New Sunny.UI.UITextBox()
        Label7 = New Label()
        Panel73.SuspendLayout()
        Panel8.SuspendLayout()
        Panel11.SuspendLayout()
        Panel1.SuspendLayout()
        Panel2.SuspendLayout()
        Panel3.SuspendLayout()
        Panel4.SuspendLayout()
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
        Panel73.Size = New Size(494, 58)
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
        Label123.Size = New Size(419, 38)
        Label123.TabIndex = 4
        Label123.Text = "Time Blend 是 ffmpeg 内置的的滤镜，通过某种算法将相邻帧进行混合" & vbCrLf & "但注意算法算出来的动态模糊不可能比得过真实录制的效果"
        ' 
        ' Label124
        ' 
        Label124.Dock = DockStyle.Top
        Label124.Location = New Point(20, 78)
        Label124.Name = "Label124"
        Label124.Size = New Size(494, 20)
        Label124.TabIndex = 83
        ' 
        ' Panel8
        ' 
        Panel8.BackColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        Panel8.Controls.Add(Panel11)
        Panel8.Controls.Add(Label1)
        Panel8.Dock = DockStyle.Top
        Panel8.Location = New Point(20, 98)
        Panel8.Name = "Panel8"
        Panel8.Size = New Size(494, 50)
        Panel8.TabIndex = 84
        ' 
        ' Panel11
        ' 
        Panel11.Controls.Add(Label13)
        Panel11.Controls.Add(UiTextBox降低帧率)
        Panel11.Dock = DockStyle.Fill
        Panel11.Location = New Point(100, 0)
        Panel11.Name = "Panel11"
        Panel11.Padding = New Padding(10, 10, 0, 10)
        Panel11.Size = New Size(394, 50)
        Panel11.TabIndex = 2
        ' 
        ' Label13
        ' 
        Label13.Dock = DockStyle.Fill
        Label13.ForeColor = Color.Gray
        Label13.Location = New Point(185, 10)
        Label13.Name = "Label13"
        Label13.Padding = New Padding(10, 0, 0, 0)
        Label13.Size = New Size(209, 30)
        Label13.TabIndex = 98
        Label13.Text = "可选"
        Label13.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' UiTextBox降低帧率
        ' 
        UiTextBox降低帧率.Dock = DockStyle.Left
        UiTextBox降低帧率.FillColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox降低帧率.FillColor2 = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox降低帧率.FillDisableColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox降低帧率.FillReadOnlyColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox降低帧率.Font = New Font("微软雅黑", 9.75F)
        UiTextBox降低帧率.ForeColor = Color.DarkGray
        UiTextBox降低帧率.ForeDisableColor = Color.DarkGray
        UiTextBox降低帧率.ForeReadOnlyColor = Color.DarkGray
        UiTextBox降低帧率.Location = New Point(10, 10)
        UiTextBox降低帧率.Margin = New Padding(4, 5, 4, 5)
        UiTextBox降低帧率.MinimumSize = New Size(1, 16)
        UiTextBox降低帧率.Name = "UiTextBox降低帧率"
        UiTextBox降低帧率.Padding = New Padding(5)
        UiTextBox降低帧率.Radius = 30
        UiTextBox降低帧率.RectColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox降低帧率.RectDisableColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox降低帧率.RectReadOnlyColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox降低帧率.ScrollBarBackColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiTextBox降低帧率.ScrollBarColor = Color.DimGray
        UiTextBox降低帧率.ScrollBarStyleInherited = False
        UiTextBox降低帧率.ShowText = False
        UiTextBox降低帧率.Size = New Size(175, 30)
        UiTextBox降低帧率.TabIndex = 97
        UiTextBox降低帧率.TextAlignment = ContentAlignment.MiddleLeft
        UiTextBox降低帧率.Watermark = "在滤镜中指定帧率"
        UiTextBox降低帧率.WatermarkActiveColor = Color.DimGray
        UiTextBox降低帧率.WatermarkColor = Color.DimGray
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
        Label1.Text = "降低帧率"
        Label1.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label2
        ' 
        Label2.Dock = DockStyle.Top
        Label2.Location = New Point(20, 148)
        Label2.Name = "Label2"
        Label2.Size = New Size(494, 20)
        Label2.TabIndex = 85
        ' 
        ' Panel1
        ' 
        Panel1.BackColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        Panel1.Controls.Add(Panel2)
        Panel1.Controls.Add(Label4)
        Panel1.Dock = DockStyle.Top
        Panel1.Location = New Point(20, 168)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(494, 50)
        Panel1.TabIndex = 86
        ' 
        ' Panel2
        ' 
        Panel2.Controls.Add(Label3)
        Panel2.Controls.Add(UiComboBox混合算法)
        Panel2.Dock = DockStyle.Fill
        Panel2.Location = New Point(100, 0)
        Panel2.Name = "Panel2"
        Panel2.Padding = New Padding(10, 10, 0, 10)
        Panel2.Size = New Size(394, 50)
        Panel2.TabIndex = 2
        ' 
        ' Label3
        ' 
        Label3.Dock = DockStyle.Fill
        Label3.ForeColor = Color.Gray
        Label3.Location = New Point(185, 10)
        Label3.Name = "Label3"
        Label3.Padding = New Padding(10, 0, 0, 0)
        Label3.Size = New Size(209, 30)
        Label3.TabIndex = 99
        Label3.Text = "留空以取消使用此滤镜"
        Label3.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' UiComboBox混合算法
        ' 
        UiComboBox混合算法.DataSource = Nothing
        UiComboBox混合算法.Dock = DockStyle.Left
        UiComboBox混合算法.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList
        UiComboBox混合算法.FillColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiComboBox混合算法.FillColor2 = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiComboBox混合算法.FillDisableColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiComboBox混合算法.Font = New Font("微软雅黑", 9.75F)
        UiComboBox混合算法.ForeColor = Color.Silver
        UiComboBox混合算法.ForeDisableColor = Color.Silver
        UiComboBox混合算法.ItemFillColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiComboBox混合算法.ItemForeColor = Color.Silver
        UiComboBox混合算法.ItemHeight = 30
        UiComboBox混合算法.ItemHoverColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiComboBox混合算法.ItemRectColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiComboBox混合算法.Items.AddRange(New Object() {"", "与前一帧的平均值", "插值混合", "位运算 AND", "位运算 OR", "位运算 XOR", "像素值相加", "像素值相乘"})
        UiComboBox混合算法.ItemSelectBackColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        UiComboBox混合算法.ItemSelectForeColor = Color.Silver
        UiComboBox混合算法.Location = New Point(10, 10)
        UiComboBox混合算法.Margin = New Padding(4, 5, 4, 5)
        UiComboBox混合算法.MaxDropDownItems = 17
        UiComboBox混合算法.MinimumSize = New Size(63, 0)
        UiComboBox混合算法.Name = "UiComboBox混合算法"
        UiComboBox混合算法.Padding = New Padding(0, 0, 30, 2)
        UiComboBox混合算法.RectColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        UiComboBox混合算法.RectDisableColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        UiComboBox混合算法.ScrollBarHandleWidth = 20
        UiComboBox混合算法.Size = New Size(175, 30)
        UiComboBox混合算法.Style = Sunny.UI.UIStyle.Custom
        UiComboBox混合算法.SymbolSize = 24
        UiComboBox混合算法.TabIndex = 88
        UiComboBox混合算法.TextAlignment = ContentAlignment.MiddleLeft
        UiComboBox混合算法.Watermark = "混合算法"
        ' 
        ' Label4
        ' 
        Label4.BackColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        Label4.Dock = DockStyle.Left
        Label4.Font = New Font("微软雅黑", 10F, FontStyle.Bold)
        Label4.Location = New Point(0, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(100, 50)
        Label4.TabIndex = 4
        Label4.Text = "混合模式"
        Label4.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label5
        ' 
        Label5.Dock = DockStyle.Top
        Label5.Location = New Point(20, 218)
        Label5.Name = "Label5"
        Label5.Size = New Size(494, 20)
        Label5.TabIndex = 87
        ' 
        ' Panel3
        ' 
        Panel3.BackColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        Panel3.Controls.Add(Panel4)
        Panel3.Controls.Add(Label7)
        Panel3.Dock = DockStyle.Top
        Panel3.Location = New Point(20, 238)
        Panel3.Name = "Panel3"
        Panel3.Size = New Size(494, 50)
        Panel3.TabIndex = 88
        ' 
        ' Panel4
        ' 
        Panel4.Controls.Add(Label6)
        Panel4.Controls.Add(UiTextBox混合比例)
        Panel4.Dock = DockStyle.Fill
        Panel4.Location = New Point(100, 0)
        Panel4.Name = "Panel4"
        Panel4.Padding = New Padding(10, 10, 0, 10)
        Panel4.Size = New Size(394, 50)
        Panel4.TabIndex = 2
        ' 
        ' Label6
        ' 
        Label6.Dock = DockStyle.Fill
        Label6.ForeColor = Color.Gray
        Label6.Location = New Point(185, 10)
        Label6.Name = "Label6"
        Label6.Padding = New Padding(10, 0, 0, 0)
        Label6.Size = New Size(209, 30)
        Label6.TabIndex = 100
        Label6.Text = "不一定有效"
        Label6.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' UiTextBox混合比例
        ' 
        UiTextBox混合比例.Dock = DockStyle.Left
        UiTextBox混合比例.FillColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox混合比例.FillColor2 = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox混合比例.FillDisableColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox混合比例.FillReadOnlyColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox混合比例.Font = New Font("微软雅黑", 9.75F)
        UiTextBox混合比例.ForeColor = Color.DarkGray
        UiTextBox混合比例.ForeDisableColor = Color.DarkGray
        UiTextBox混合比例.ForeReadOnlyColor = Color.DarkGray
        UiTextBox混合比例.Location = New Point(10, 10)
        UiTextBox混合比例.Margin = New Padding(4, 5, 4, 5)
        UiTextBox混合比例.MinimumSize = New Size(1, 16)
        UiTextBox混合比例.Name = "UiTextBox混合比例"
        UiTextBox混合比例.Padding = New Padding(5)
        UiTextBox混合比例.Radius = 30
        UiTextBox混合比例.RectColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox混合比例.RectDisableColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox混合比例.RectReadOnlyColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTextBox混合比例.ScrollBarBackColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiTextBox混合比例.ScrollBarColor = Color.DimGray
        UiTextBox混合比例.ScrollBarStyleInherited = False
        UiTextBox混合比例.ShowText = False
        UiTextBox混合比例.Size = New Size(175, 30)
        UiTextBox混合比例.TabIndex = 98
        UiTextBox混合比例.TextAlignment = ContentAlignment.MiddleLeft
        UiTextBox混合比例.Watermark = "0.0~1.0"
        UiTextBox混合比例.WatermarkActiveColor = Color.DimGray
        UiTextBox混合比例.WatermarkColor = Color.DimGray
        ' 
        ' Label7
        ' 
        Label7.BackColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        Label7.Dock = DockStyle.Left
        Label7.Font = New Font("微软雅黑", 10F, FontStyle.Bold)
        Label7.Location = New Point(0, 0)
        Label7.Name = "Label7"
        Label7.Size = New Size(100, 50)
        Label7.TabIndex = 4
        Label7.Text = "混合比例"
        Label7.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Form帧混合
        ' 
        AutoScaleDimensions = New SizeF(8F, 19F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ClientSize = New Size(534, 311)
        Controls.Add(Panel3)
        Controls.Add(Label5)
        Controls.Add(Panel1)
        Controls.Add(Label2)
        Controls.Add(Panel8)
        Controls.Add(Label124)
        Controls.Add(Panel73)
        Font = New Font("微软雅黑", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        ForeColor = Color.Silver
        MaximizeBox = False
        MinimizeBox = False
        MinimumSize = New Size(550, 350)
        Name = "Form帧混合"
        Padding = New Padding(20)
        ShowIcon = False
        ShowInTaskbar = False
        StartPosition = FormStartPosition.Manual
        Text = "tblend 滤镜参数"
        Panel73.ResumeLayout(False)
        Panel73.PerformLayout()
        Panel8.ResumeLayout(False)
        Panel11.ResumeLayout(False)
        Panel1.ResumeLayout(False)
        Panel2.ResumeLayout(False)
        Panel3.ResumeLayout(False)
        Panel4.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Panel73 As Panel
    Friend WithEvents Label123 As Label
    Friend WithEvents Label124 As Label
    Friend WithEvents Panel8 As Panel
    Friend WithEvents Panel11 As Panel
    Friend WithEvents Label13 As Label
    Friend WithEvents UiTextBox降低帧率 As Sunny.UI.UITextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Label7 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents UiComboBox混合算法 As Sunny.UI.UIComboBox
    Friend WithEvents UiTextBox混合比例 As Sunny.UI.UITextBox
    Friend WithEvents Label6 As Label
End Class
