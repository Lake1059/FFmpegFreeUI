<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class 界面_播放器
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
        Label3 = New Label()
        Label2 = New Label()
        Label1 = New Label()
        Panel2 = New Panel()
        UiButton关闭进程 = New Sunny.UI.UIButton()
        Label8 = New Label()
        UiButton打开文件 = New Sunny.UI.UIButton()
        Panel3 = New Panel()
        Panel1.SuspendLayout()
        Panel2.SuspendLayout()
        SuspendLayout()
        ' 
        ' Panel1
        ' 
        Panel1.AutoSize = True
        Panel1.BackColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        Panel1.Controls.Add(Label3)
        Panel1.Controls.Add(Label2)
        Panel1.Controls.Add(Label1)
        Panel1.Dock = DockStyle.Top
        Panel1.Location = New Point(0, 0)
        Panel1.Name = "Panel1"
        Panel1.Padding = New Padding(10, 0, 10, 10)
        Panel1.Size = New Size(1143, 80)
        Panel1.TabIndex = 0
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Dock = DockStyle.Top
        Label3.ForeColor = Color.DarkGray
        Label3.Location = New Point(10, 45)
        Label3.Name = "Label3"
        Label3.Padding = New Padding(0, 5, 0, 0)
        Label3.Size = New Size(569, 25)
        Label3.TabIndex = 2
        Label3.Text = "仅建议用于临时场景需求，例如播放最新或小众的编码，日常观看视频请去用成熟的播放器"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Dock = DockStyle.Top
        Label2.ForeColor = Color.DarkGray
        Label2.Location = New Point(10, 20)
        Label2.Name = "Label2"
        Label2.Padding = New Padding(0, 5, 0, 0)
        Label2.Size = New Size(383, 25)
        Label2.TabIndex = 1
        Label2.Text = "ffplay 是用于调试目的的播放器，会大量占据显卡 PCIE 带宽"
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Dock = DockStyle.Top
        Label1.ForeColor = Color.DarkGray
        Label1.Location = New Point(10, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(515, 20)
        Label1.TabIndex = 0
        Label1.Text = "直接调用 ffplay.exe 并重定向来实现播放，注意放置该程序或将其添加到环境变量"
        ' 
        ' Panel2
        ' 
        Panel2.BackColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        Panel2.Controls.Add(UiButton关闭进程)
        Panel2.Controls.Add(Label8)
        Panel2.Controls.Add(UiButton打开文件)
        Panel2.Dock = DockStyle.Bottom
        Panel2.Location = New Point(0, 677)
        Panel2.Name = "Panel2"
        Panel2.Padding = New Padding(10)
        Panel2.Size = New Size(1143, 50)
        Panel2.TabIndex = 1
        ' 
        ' UiButton关闭进程
        ' 
        UiButton关闭进程.AllowDrop = True
        UiButton关闭进程.Dock = DockStyle.Left
        UiButton关闭进程.FillColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton关闭进程.FillColor2 = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton关闭进程.FillDisableColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton关闭进程.FillHoverColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton关闭进程.FillPressColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton关闭进程.FillSelectedColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton关闭进程.Font = New Font("微软雅黑", 9.75F)
        UiButton关闭进程.ForeColor = Color.Silver
        UiButton关闭进程.ForeDisableColor = Color.Silver
        UiButton关闭进程.ForeHoverColor = Color.Silver
        UiButton关闭进程.ForePressColor = Color.Silver
        UiButton关闭进程.ForeSelectedColor = Color.Silver
        UiButton关闭进程.Location = New Point(100, 10)
        UiButton关闭进程.MinimumSize = New Size(1, 1)
        UiButton关闭进程.Name = "UiButton关闭进程"
        UiButton关闭进程.Radius = 30
        UiButton关闭进程.RectColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton关闭进程.RectDisableColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton关闭进程.RectHoverColor = Color.Silver
        UiButton关闭进程.RectPressColor = Color.Gainsboro
        UiButton关闭进程.Size = New Size(80, 30)
        UiButton关闭进程.TabIndex = 101
        UiButton关闭进程.Text = "关闭"
        UiButton关闭进程.TipsFont = New Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        ' 
        ' Label8
        ' 
        Label8.Dock = DockStyle.Left
        Label8.Location = New Point(90, 10)
        Label8.Name = "Label8"
        Label8.Size = New Size(10, 30)
        Label8.TabIndex = 100
        ' 
        ' UiButton打开文件
        ' 
        UiButton打开文件.AllowDrop = True
        UiButton打开文件.Dock = DockStyle.Left
        UiButton打开文件.FillColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton打开文件.FillColor2 = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton打开文件.FillDisableColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton打开文件.FillHoverColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton打开文件.FillPressColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton打开文件.FillSelectedColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton打开文件.Font = New Font("微软雅黑", 9.75F)
        UiButton打开文件.ForeColor = Color.Silver
        UiButton打开文件.ForeDisableColor = Color.Silver
        UiButton打开文件.ForeHoverColor = Color.Silver
        UiButton打开文件.ForePressColor = Color.Silver
        UiButton打开文件.ForeSelectedColor = Color.Silver
        UiButton打开文件.Location = New Point(10, 10)
        UiButton打开文件.MinimumSize = New Size(1, 1)
        UiButton打开文件.Name = "UiButton打开文件"
        UiButton打开文件.Radius = 30
        UiButton打开文件.RectColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton打开文件.RectDisableColor = Color.FromArgb(CByte(56), CByte(56), CByte(56))
        UiButton打开文件.RectHoverColor = Color.Silver
        UiButton打开文件.RectPressColor = Color.Gainsboro
        UiButton打开文件.Size = New Size(80, 30)
        UiButton打开文件.TabIndex = 47
        UiButton打开文件.Text = "打开"
        UiButton打开文件.TipsFont = New Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        ' 
        ' Panel3
        ' 
        Panel3.AllowDrop = True
        Panel3.Dock = DockStyle.Fill
        Panel3.Location = New Point(0, 80)
        Panel3.Name = "Panel3"
        Panel3.Size = New Size(1143, 597)
        Panel3.TabIndex = 2
        ' 
        ' 界面_播放器
        ' 
        AutoScaleDimensions = New SizeF(8F, 19F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        Controls.Add(Panel3)
        Controls.Add(Panel2)
        Controls.Add(Panel1)
        Font = New Font("微软雅黑", 10F)
        ForeColor = Color.Silver
        Margin = New Padding(3, 4, 3, 4)
        Name = "界面_播放器"
        Size = New Size(1143, 727)
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        Panel2.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents UiButton打开文件 As Sunny.UI.UIButton
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents UiButton关闭进程 As Sunny.UI.UIButton
    Friend WithEvents Label8 As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Label3 As Label

End Class
