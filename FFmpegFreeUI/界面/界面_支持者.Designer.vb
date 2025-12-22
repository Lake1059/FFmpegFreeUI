<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class 界面_支持者
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
        Label1 = New Label()
        Label47 = New Label()
        Panel2 = New Panel()
        UiButton3 = New Sunny.UI.UIButton()
        Label2 = New Label()
        UiButton2 = New Sunny.UI.UIButton()
        Label8 = New Label()
        UiButton1 = New Sunny.UI.UIButton()
        FlowLayoutPanel1 = New FlowLayoutPanel()
        Panel2.SuspendLayout()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Dock = DockStyle.Top
        Label1.Font = New Font("微软雅黑", 13F)
        Label1.Location = New Point(0, 0)
        Label1.Name = "Label1"
        Label1.Padding = New Padding(16, 6, 0, 0)
        Label1.Size = New Size(116, 30)
        Label1.TabIndex = 121
        Label1.Text = "支持者名单"
        ' 
        ' Label47
        ' 
        Label47.AutoSize = True
        Label47.Dock = DockStyle.Top
        Label47.Font = New Font("微软雅黑", 10F)
        Label47.ForeColor = Color.Gray
        Label47.Location = New Point(0, 30)
        Label47.Name = "Label47"
        Label47.Padding = New Padding(16, 5, 0, 0)
        Label47.Size = New Size(464, 25)
        Label47.TabIndex = 123
        Label47.Text = "感谢所有向 FFmpegFreeUI 的开发工作提供资金支持和技术支持的用户"
        ' 
        ' Panel2
        ' 
        Panel2.Controls.Add(UiButton3)
        Panel2.Controls.Add(Label2)
        Panel2.Controls.Add(UiButton2)
        Panel2.Controls.Add(Label8)
        Panel2.Controls.Add(UiButton1)
        Panel2.Dock = DockStyle.Top
        Panel2.Location = New Point(0, 55)
        Panel2.Name = "Panel2"
        Panel2.Padding = New Padding(20, 10, 10, 10)
        Panel2.Size = New Size(1000, 50)
        Panel2.TabIndex = 124
        ' 
        ' UiButton3
        ' 
        UiButton3.AllowDrop = True
        UiButton3.Dock = DockStyle.Left
        UiButton3.FillColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiButton3.FillColor2 = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiButton3.FillDisableColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiButton3.FillHoverColor = Color.DarkSlateBlue
        UiButton3.FillPressColor = Color.MediumPurple
        UiButton3.FillSelectedColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiButton3.Font = New Font("微软雅黑", 9.75F)
        UiButton3.ForeColor = Color.Silver
        UiButton3.ForeDisableColor = Color.Silver
        UiButton3.ForeHoverColor = Color.Silver
        UiButton3.ForePressColor = Color.Silver
        UiButton3.ForeSelectedColor = Color.Silver
        UiButton3.Location = New Point(230, 10)
        UiButton3.MinimumSize = New Size(1, 1)
        UiButton3.Name = "UiButton3"
        UiButton3.Radius = 30
        UiButton3.RectColor = Color.MediumPurple
        UiButton3.RectDisableColor = Color.MediumPurple
        UiButton3.RectHoverColor = Color.MediumPurple
        UiButton3.RectPressColor = Color.MediumPurple
        UiButton3.RectSelectedColor = Color.MediumPurple
        UiButton3.Size = New Size(110, 30)
        UiButton3.TabIndex = 103
        UiButton3.Text = "免费赠送的"
        UiButton3.TipsFont = New Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        ' 
        ' Label2
        ' 
        Label2.Dock = DockStyle.Left
        Label2.Location = New Point(220, 10)
        Label2.Name = "Label2"
        Label2.Size = New Size(10, 30)
        Label2.TabIndex = 102
        ' 
        ' UiButton2
        ' 
        UiButton2.AllowDrop = True
        UiButton2.Dock = DockStyle.Left
        UiButton2.FillColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiButton2.FillColor2 = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiButton2.FillDisableColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiButton2.FillHoverColor = Color.DarkSlateBlue
        UiButton2.FillPressColor = Color.MediumPurple
        UiButton2.FillSelectedColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiButton2.Font = New Font("微软雅黑", 9.75F)
        UiButton2.ForeColor = Color.Silver
        UiButton2.ForeDisableColor = Color.Silver
        UiButton2.ForeHoverColor = Color.Silver
        UiButton2.ForePressColor = Color.Silver
        UiButton2.ForeSelectedColor = Color.Silver
        UiButton2.Location = New Point(110, 10)
        UiButton2.MinimumSize = New Size(1, 1)
        UiButton2.Name = "UiButton2"
        UiButton2.Radius = 30
        UiButton2.RectColor = Color.MediumPurple
        UiButton2.RectDisableColor = Color.MediumPurple
        UiButton2.RectHoverColor = Color.MediumPurple
        UiButton2.RectPressColor = Color.MediumPurple
        UiButton2.RectSelectedColor = Color.MediumPurple
        UiButton2.Size = New Size(110, 30)
        UiButton2.TabIndex = 101
        UiButton2.Text = "购买获得的"
        UiButton2.TipsFont = New Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        ' 
        ' Label8
        ' 
        Label8.Dock = DockStyle.Left
        Label8.Location = New Point(100, 10)
        Label8.Name = "Label8"
        Label8.Size = New Size(10, 30)
        Label8.TabIndex = 100
        ' 
        ' UiButton1
        ' 
        UiButton1.AllowDrop = True
        UiButton1.Dock = DockStyle.Left
        UiButton1.FillColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiButton1.FillColor2 = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiButton1.FillDisableColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiButton1.FillHoverColor = Color.DarkSlateBlue
        UiButton1.FillPressColor = Color.MediumPurple
        UiButton1.FillSelectedColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiButton1.Font = New Font("微软雅黑", 9.75F)
        UiButton1.ForeColor = Color.Silver
        UiButton1.ForeDisableColor = Color.Silver
        UiButton1.ForeHoverColor = Color.Silver
        UiButton1.ForePressColor = Color.Silver
        UiButton1.ForeSelectedColor = Color.Silver
        UiButton1.Location = New Point(20, 10)
        UiButton1.MinimumSize = New Size(1, 1)
        UiButton1.Name = "UiButton1"
        UiButton1.Radius = 30
        UiButton1.RectColor = Color.MediumPurple
        UiButton1.RectDisableColor = Color.MediumPurple
        UiButton1.RectHoverColor = Color.MediumPurple
        UiButton1.RectPressColor = Color.MediumPurple
        UiButton1.RectSelectedColor = Color.MediumPurple
        UiButton1.Size = New Size(80, 30)
        UiButton1.TabIndex = 47
        UiButton1.Text = "全部"
        UiButton1.TipsFont = New Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        ' 
        ' FlowLayoutPanel1
        ' 
        FlowLayoutPanel1.AutoScroll = True
        FlowLayoutPanel1.Dock = DockStyle.Fill
        FlowLayoutPanel1.Location = New Point(0, 105)
        FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        FlowLayoutPanel1.Padding = New Padding(20, 10, 20, 20)
        FlowLayoutPanel1.Size = New Size(1000, 545)
        FlowLayoutPanel1.TabIndex = 125
        ' 
        ' 界面_支持者
        ' 
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        Controls.Add(FlowLayoutPanel1)
        Controls.Add(Panel2)
        Controls.Add(Label47)
        Controls.Add(Label1)
        Font = New Font("微软雅黑", 10F)
        ForeColor = Color.Silver
        Name = "界面_支持者"
        Size = New Size(1000, 650)
        Panel2.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label47 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents UiButton2 As Sunny.UI.UIButton
    Friend WithEvents Label8 As Label
    Friend WithEvents UiButton1 As Sunny.UI.UIButton
    Friend WithEvents UiButton3 As Sunny.UI.UIButton
    Friend WithEvents Label2 As Label
    Friend WithEvents FlowLayoutPanel1 As FlowLayoutPanel

End Class
