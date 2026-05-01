<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class 界面_媒体信息
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
        Panel9 = New Panel()
        Panel75 = New Panel()
        Label123 = New Label()
        UiButton1 = New Sunny.UI.UIButton()
        Panel76 = New Panel()
        RichTextBox1 = New RichTextBox()
        Panel9.SuspendLayout()
        Panel75.SuspendLayout()
        Panel76.SuspendLayout()
        SuspendLayout()
        ' 
        ' Panel9
        ' 
        Panel9.AllowDrop = True
        Panel9.AutoSize = True
        Panel9.BackColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        Panel9.Controls.Add(Panel75)
        Panel9.Dock = DockStyle.Top
        Panel9.Location = New Point(0, 0)
        Panel9.Name = "Panel9"
        Panel9.Padding = New Padding(10, 0, 10, 10)
        Panel9.Size = New Size(1000, 45)
        Panel9.TabIndex = 81
        ' 
        ' Panel75
        ' 
        Panel75.Controls.Add(Label123)
        Panel75.Controls.Add(UiButton1)
        Panel75.Dock = DockStyle.Top
        Panel75.Location = New Point(10, 0)
        Panel75.Name = "Panel75"
        Panel75.Size = New Size(980, 35)
        Panel75.TabIndex = 11
        ' 
        ' Label123
        ' 
        Label123.AllowDrop = True
        Label123.Dock = DockStyle.Fill
        Label123.Font = New Font("微软雅黑", 10F)
        Label123.ForeColor = Color.DarkGray
        Label123.Location = New Point(100, 0)
        Label123.Name = "Label123"
        Label123.Padding = New Padding(10, 0, 0, 0)
        Label123.Size = New Size(880, 35)
        Label123.TabIndex = 45
        Label123.Text = "调用 ffprobe.exe 直接展示输出信息，可以把文件拖到按钮上或此顶部区域；此功能不兼容转译模式！"
        Label123.TextAlign = ContentAlignment.MiddleLeft
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
        UiButton1.RectHoverColor = Color.YellowGreen
        UiButton1.RectPressColor = Color.FromArgb(CByte(64), CByte(148), CByte(64))
        UiButton1.RectSelectedColor = Color.YellowGreen
        UiButton1.RectSize = 2
        UiButton1.Size = New Size(100, 35)
        UiButton1.TabIndex = 44
        UiButton1.Text = "打开"
        UiButton1.TipsFont = New Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        ' 
        ' Panel76
        ' 
        Panel76.AutoSize = True
        Panel76.BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        Panel76.Controls.Add(RichTextBox1)
        Panel76.Dock = DockStyle.Fill
        Panel76.Location = New Point(0, 45)
        Panel76.Name = "Panel76"
        Panel76.Padding = New Padding(10, 10, 0, 10)
        Panel76.Size = New Size(1000, 605)
        Panel76.TabIndex = 85
        ' 
        ' RichTextBox1
        ' 
        RichTextBox1.BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        RichTextBox1.BorderStyle = BorderStyle.None
        RichTextBox1.DetectUrls = False
        RichTextBox1.Dock = DockStyle.Fill
        RichTextBox1.Font = New Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        RichTextBox1.ForeColor = Color.Silver
        RichTextBox1.Location = New Point(10, 10)
        RichTextBox1.Name = "RichTextBox1"
        RichTextBox1.ReadOnly = True
        RichTextBox1.ScrollBars = RichTextBoxScrollBars.Vertical
        RichTextBox1.Size = New Size(990, 585)
        RichTextBox1.TabIndex = 12
        RichTextBox1.Text = ""
        ' 
        ' 界面_媒体信息
        ' 
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        Controls.Add(Panel76)
        Controls.Add(Panel9)
        Font = New Font("微软雅黑", 10F)
        ForeColor = Color.Silver
        Name = "界面_媒体信息"
        Size = New Size(1000, 650)
        Panel9.ResumeLayout(False)
        Panel75.ResumeLayout(False)
        Panel76.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Panel9 As Panel
    Friend WithEvents Panel75 As Panel
    Friend WithEvents Label123 As Label
    Friend WithEvents UiButton1 As Sunny.UI.UIButton
    Friend WithEvents Panel76 As Panel
    Friend WithEvents RichTextBox1 As RichTextBox

End Class
