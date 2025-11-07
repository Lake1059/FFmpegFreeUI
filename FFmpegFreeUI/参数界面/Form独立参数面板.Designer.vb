<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form独立参数面板
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
        Panel1 = New Panel()
        Label1 = New Label()
        Panel2 = New Panel()
        UiButton确认并添加任务 = New Sunny.UI.UIButton()
        Panel3 = New Panel()
        Panel1.SuspendLayout()
        Panel2.SuspendLayout()
        Panel3.SuspendLayout()
        SuspendLayout()
        ' 
        ' Panel1
        ' 
        Panel1.BackColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        Panel1.Controls.Add(Label1)
        Panel1.Controls.Add(Panel2)
        Panel1.Dock = DockStyle.Top
        Panel1.Location = New Point(0, 0)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(1084, 50)
        Panel1.TabIndex = 0
        ' 
        ' Label1
        ' 
        Label1.Dock = DockStyle.Fill
        Label1.Font = New Font("微软雅黑", 9.75F)
        Label1.Location = New Point(170, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(914, 50)
        Label1.TabIndex = 110
        Label1.Text = "Label1"
        Label1.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Panel2
        ' 
        Panel2.Controls.Add(UiButton确认并添加任务)
        Panel2.Dock = DockStyle.Left
        Panel2.Location = New Point(0, 0)
        Panel2.Name = "Panel2"
        Panel2.Padding = New Padding(8)
        Panel2.Size = New Size(170, 50)
        Panel2.TabIndex = 109
        ' 
        ' UiButton确认并添加任务
        ' 
        UiButton确认并添加任务.Dock = DockStyle.Fill
        UiButton确认并添加任务.FillColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiButton确认并添加任务.FillColor2 = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiButton确认并添加任务.FillDisableColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiButton确认并添加任务.FillHoverColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiButton确认并添加任务.FillPressColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiButton确认并添加任务.FillSelectedColor = Color.FromArgb(CByte(36), CByte(36), CByte(36))
        UiButton确认并添加任务.Font = New Font("微软雅黑", 9.75F)
        UiButton确认并添加任务.ForeColor = Color.YellowGreen
        UiButton确认并添加任务.ForeDisableColor = Color.YellowGreen
        UiButton确认并添加任务.ForeHoverColor = Color.YellowGreen
        UiButton确认并添加任务.ForePressColor = Color.YellowGreen
        UiButton确认并添加任务.ForeSelectedColor = Color.YellowGreen
        UiButton确认并添加任务.Location = New Point(8, 8)
        UiButton确认并添加任务.MinimumSize = New Size(1, 1)
        UiButton确认并添加任务.Name = "UiButton确认并添加任务"
        UiButton确认并添加任务.Radius = 0
        UiButton确认并添加任务.RadiusSides = Sunny.UI.UICornerRadiusSides.None
        UiButton确认并添加任务.RectColor = Color.DimGray
        UiButton确认并添加任务.RectDisableColor = Color.FromArgb(CByte(12), CByte(12), CByte(12))
        UiButton确认并添加任务.RectHoverColor = Color.YellowGreen
        UiButton确认并添加任务.RectPressColor = Color.FromArgb(CByte(64), CByte(148), CByte(64))
        UiButton确认并添加任务.RectSelectedColor = Color.YellowGreen
        UiButton确认并添加任务.Size = New Size(154, 34)
        UiButton确认并添加任务.TabIndex = 109
        UiButton确认并添加任务.Text = "确认并添加任务"
        UiButton确认并添加任务.TipsFont = New Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        ' 
        ' Panel3
        ' 
        Panel3.Controls.Add(Panel1)
        Panel3.Dock = DockStyle.Fill
        Panel3.Location = New Point(0, 0)
        Panel3.Name = "Panel3"
        Panel3.Size = New Size(1084, 661)
        Panel3.TabIndex = 1
        ' 
        ' Form独立参数面板
        ' 
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ClientSize = New Size(1084, 661)
        Controls.Add(Panel3)
        Font = New Font("微软雅黑", 10F)
        ForeColor = Color.Silver
        MaximizeBox = False
        MinimizeBox = False
        MinimumSize = New Size(1100, 700)
        Name = "Form独立参数面板"
        ShowIcon = False
        ShowInTaskbar = False
        StartPosition = FormStartPosition.Manual
        Text = "独立参数面板"
        Panel1.ResumeLayout(False)
        Panel2.ResumeLayout(False)
        Panel3.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents UiButton确认并添加任务 As Sunny.UI.UIButton
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel3 As Panel
End Class
