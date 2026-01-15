<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form媒体流选择器
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
        Panel2 = New Panel()
        Panel3 = New Panel()
        UiButton重置 = New Sunny.UI.UIButton()
        Label1 = New Label()
        UiButton打开 = New Sunny.UI.UIButton()
        UiButton确认 = New Sunny.UI.UIButton()
        Panel3.SuspendLayout()
        SuspendLayout()
        ' 
        ' Panel2
        ' 
        Panel2.AllowDrop = True
        Panel2.AutoScroll = True
        Panel2.BackColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        Panel2.Dock = DockStyle.Fill
        Panel2.Location = New Point(10, 10)
        Panel2.Name = "Panel2"
        Panel2.Padding = New Padding(10, 10, 0, 10)
        Panel2.Size = New Size(664, 301)
        Panel2.TabIndex = 126
        ' 
        ' Panel3
        ' 
        Panel3.Controls.Add(UiButton重置)
        Panel3.Controls.Add(Label1)
        Panel3.Controls.Add(UiButton打开)
        Panel3.Controls.Add(UiButton确认)
        Panel3.Dock = DockStyle.Bottom
        Panel3.Location = New Point(10, 311)
        Panel3.Name = "Panel3"
        Panel3.Padding = New Padding(0, 10, 0, 0)
        Panel3.Size = New Size(664, 40)
        Panel3.TabIndex = 127
        ' 
        ' UiButton重置
        ' 
        UiButton重置.Dock = DockStyle.Left
        UiButton重置.FillColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton重置.FillColor2 = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton重置.FillDisableColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiButton重置.FillHoverColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton重置.FillPressColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiButton重置.FillSelectedColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiButton重置.Font = New Font("微软雅黑", 9.75F)
        UiButton重置.ForeColor = Color.Silver
        UiButton重置.ForeDisableColor = Color.Silver
        UiButton重置.ForeHoverColor = Color.Silver
        UiButton重置.ForePressColor = Color.Silver
        UiButton重置.ForeSelectedColor = Color.Silver
        UiButton重置.Location = New Point(90, 10)
        UiButton重置.MinimumSize = New Size(1, 1)
        UiButton重置.Name = "UiButton重置"
        UiButton重置.RadiusSides = Sunny.UI.UICornerRadiusSides.None
        UiButton重置.RectColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton重置.RectDisableColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton重置.RectHoverColor = Color.DarkGray
        UiButton重置.RectPressColor = Color.FromArgb(CByte(64), CByte(148), CByte(64))
        UiButton重置.RectSelectedColor = Color.DarkGray
        UiButton重置.Size = New Size(80, 30)
        UiButton重置.TabIndex = 108
        UiButton重置.Text = "重置"
        UiButton重置.TipsFont = New Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        ' 
        ' Label1
        ' 
        Label1.Dock = DockStyle.Left
        Label1.Location = New Point(80, 10)
        Label1.Name = "Label1"
        Label1.Size = New Size(10, 30)
        Label1.TabIndex = 107
        Label1.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' UiButton打开
        ' 
        UiButton打开.Dock = DockStyle.Left
        UiButton打开.FillColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton打开.FillColor2 = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton打开.FillDisableColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiButton打开.FillHoverColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton打开.FillPressColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        UiButton打开.FillSelectedColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiButton打开.Font = New Font("微软雅黑", 9.75F)
        UiButton打开.ForeColor = Color.Silver
        UiButton打开.ForeDisableColor = Color.Silver
        UiButton打开.ForeHoverColor = Color.Silver
        UiButton打开.ForePressColor = Color.Silver
        UiButton打开.ForeSelectedColor = Color.Silver
        UiButton打开.Location = New Point(0, 10)
        UiButton打开.MinimumSize = New Size(1, 1)
        UiButton打开.Name = "UiButton打开"
        UiButton打开.RadiusSides = Sunny.UI.UICornerRadiusSides.None
        UiButton打开.RectColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton打开.RectDisableColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton打开.RectHoverColor = Color.DarkGray
        UiButton打开.RectPressColor = Color.FromArgb(CByte(64), CByte(148), CByte(64))
        UiButton打开.RectSelectedColor = Color.DarkGray
        UiButton打开.Size = New Size(80, 30)
        UiButton打开.TabIndex = 104
        UiButton打开.Text = "打开"
        UiButton打开.TipsFont = New Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        ' 
        ' UiButton确认
        ' 
        UiButton确认.Dock = DockStyle.Right
        UiButton确认.FillColor = Color.FromArgb(CByte(145), CByte(132), CByte(238))
        UiButton确认.FillColor2 = Color.FromArgb(CByte(145), CByte(132), CByte(238))
        UiButton确认.FillDisableColor = Color.FromArgb(CByte(145), CByte(132), CByte(238))
        UiButton确认.FillHoverColor = Color.FromArgb(CByte(145), CByte(132), CByte(238))
        UiButton确认.FillPressColor = Color.FromArgb(CByte(145), CByte(132), CByte(238))
        UiButton确认.FillSelectedColor = Color.FromArgb(CByte(145), CByte(132), CByte(238))
        UiButton确认.Font = New Font("微软雅黑", 9.75F)
        UiButton确认.ForeColor = Color.Black
        UiButton确认.ForeDisableColor = Color.Black
        UiButton确认.ForeHoverColor = Color.Black
        UiButton确认.ForePressColor = Color.Black
        UiButton确认.ForeSelectedColor = Color.Black
        UiButton确认.Location = New Point(539, 10)
        UiButton确认.MinimumSize = New Size(1, 1)
        UiButton确认.Name = "UiButton确认"
        UiButton确认.RadiusSides = Sunny.UI.UICornerRadiusSides.None
        UiButton确认.RectColor = Color.FromArgb(CByte(145), CByte(132), CByte(238))
        UiButton确认.RectDisableColor = Color.FromArgb(CByte(145), CByte(132), CByte(238))
        UiButton确认.RectHoverColor = Color.DarkGray
        UiButton确认.RectPressColor = Color.White
        UiButton确认.RectSelectedColor = Color.DarkGray
        UiButton确认.Size = New Size(125, 30)
        UiButton确认.TabIndex = 103
        UiButton确认.Text = "确认选择"
        UiButton确认.TipsFont = New Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        ' 
        ' Form媒体流选择器
        ' 
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ClientSize = New Size(684, 361)
        Controls.Add(Panel2)
        Controls.Add(Panel3)
        Font = New Font("微软雅黑", 10F)
        ForeColor = Color.Silver
        MaximizeBox = False
        MinimizeBox = False
        MinimumSize = New Size(700, 400)
        Name = "Form媒体流选择器"
        Padding = New Padding(10)
        ShowIcon = False
        ShowInTaskbar = False
        StartPosition = FormStartPosition.Manual
        Text = "媒体流选择器"
        Panel3.ResumeLayout(False)
        ResumeLayout(False)
    End Sub
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents UiButton重置 As Sunny.UI.UIButton
    Friend WithEvents Label1 As Label
    Friend WithEvents UiButton打开 As Sunny.UI.UIButton
    Friend WithEvents UiButton确认 As Sunny.UI.UIButton
End Class
