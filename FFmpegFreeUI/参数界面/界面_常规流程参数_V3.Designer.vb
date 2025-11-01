<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class 界面_常规流程参数_V3
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
        TabPage音频参数 = New TabPage()
        TabPage视频滤镜 = New TabPage()
        TabPage色彩管理 = New TabPage()
        TabPage视频参数 = New TabPage()
        TabPage解码设置 = New TabPage()
        TabPage输出文件 = New TabPage()
        TabPage参数总览 = New TabPage()
        UiTabControl1 = New Sunny.UI.UITabControl()
        TabPage剪辑区间 = New TabPage()
        TabPage流控制 = New TabPage()
        UiTabControl1.SuspendLayout()
        SuspendLayout()
        ' 
        ' TabPage音频参数
        ' 
        TabPage音频参数.BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        TabPage音频参数.Location = New Point(0, 36)
        TabPage音频参数.Name = "TabPage音频参数"
        TabPage音频参数.Size = New Size(1139, 631)
        TabPage音频参数.TabIndex = 8
        TabPage音频参数.Text = "音频参数"
        ' 
        ' TabPage视频滤镜
        ' 
        TabPage视频滤镜.BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        TabPage视频滤镜.Location = New Point(0, 36)
        TabPage视频滤镜.Name = "TabPage视频滤镜"
        TabPage视频滤镜.Size = New Size(1139, 631)
        TabPage视频滤镜.TabIndex = 7
        TabPage视频滤镜.Text = "视频滤镜"
        ' 
        ' TabPage色彩管理
        ' 
        TabPage色彩管理.BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        TabPage色彩管理.Location = New Point(0, 36)
        TabPage色彩管理.Name = "TabPage色彩管理"
        TabPage色彩管理.Size = New Size(1139, 631)
        TabPage色彩管理.TabIndex = 6
        TabPage色彩管理.Text = "色彩管理"
        ' 
        ' TabPage视频参数
        ' 
        TabPage视频参数.BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        TabPage视频参数.Location = New Point(0, 36)
        TabPage视频参数.Name = "TabPage视频参数"
        TabPage视频参数.Size = New Size(1139, 631)
        TabPage视频参数.TabIndex = 3
        TabPage视频参数.Text = "视频参数"
        ' 
        ' TabPage解码设置
        ' 
        TabPage解码设置.BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        TabPage解码设置.Location = New Point(0, 36)
        TabPage解码设置.Name = "TabPage解码设置"
        TabPage解码设置.Size = New Size(1139, 631)
        TabPage解码设置.TabIndex = 2
        TabPage解码设置.Text = "解码设置"
        ' 
        ' TabPage输出文件
        ' 
        TabPage输出文件.BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        TabPage输出文件.Location = New Point(0, 36)
        TabPage输出文件.Name = "TabPage输出文件"
        TabPage输出文件.Size = New Size(1139, 631)
        TabPage输出文件.TabIndex = 1
        TabPage输出文件.Text = "输出文件"
        ' 
        ' TabPage参数总览
        ' 
        TabPage参数总览.BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        TabPage参数总览.Location = New Point(0, 36)
        TabPage参数总览.Name = "TabPage参数总览"
        TabPage参数总览.Size = New Size(1139, 631)
        TabPage参数总览.TabIndex = 0
        TabPage参数总览.Text = "参数总览"
        ' 
        ' UiTabControl1
        ' 
        UiTabControl1.Controls.Add(TabPage参数总览)
        UiTabControl1.Controls.Add(TabPage输出文件)
        UiTabControl1.Controls.Add(TabPage解码设置)
        UiTabControl1.Controls.Add(TabPage视频参数)
        UiTabControl1.Controls.Add(TabPage色彩管理)
        UiTabControl1.Controls.Add(TabPage视频滤镜)
        UiTabControl1.Controls.Add(TabPage音频参数)
        UiTabControl1.Controls.Add(TabPage剪辑区间)
        UiTabControl1.Controls.Add(TabPage流控制)
        UiTabControl1.Dock = DockStyle.Fill
        UiTabControl1.DrawMode = TabDrawMode.OwnerDrawFixed
        UiTabControl1.FillColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTabControl1.Font = New Font("微软雅黑", 9.75F)
        UiTabControl1.ItemSize = New Size(100, 36)
        UiTabControl1.Location = New Point(2, 2)
        UiTabControl1.MainPage = ""
        UiTabControl1.MenuStyle = Sunny.UI.UIMenuStyle.Custom
        UiTabControl1.Name = "UiTabControl1"
        UiTabControl1.SelectedIndex = 0
        UiTabControl1.Size = New Size(996, 596)
        UiTabControl1.SizeMode = TabSizeMode.Fixed
        UiTabControl1.TabBackColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        UiTabControl1.TabIndex = 0
        UiTabControl1.TabSelectedColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTabControl1.TabSelectedForeColor = Color.Silver
        UiTabControl1.TabSelectedHighColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        UiTabControl1.TabSelectedHighColorSize = 0
        UiTabControl1.TabUnSelectedForeColor = Color.Silver
        UiTabControl1.TipsFont = New Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        ' 
        ' TabPage剪辑区间
        ' 
        TabPage剪辑区间.BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        TabPage剪辑区间.Location = New Point(0, 36)
        TabPage剪辑区间.Name = "TabPage剪辑区间"
        TabPage剪辑区间.Size = New Size(1139, 631)
        TabPage剪辑区间.TabIndex = 9
        TabPage剪辑区间.Text = "剪辑区间"
        ' 
        ' TabPage流控制
        ' 
        TabPage流控制.BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        TabPage流控制.Location = New Point(0, 36)
        TabPage流控制.Name = "TabPage流控制"
        TabPage流控制.Size = New Size(996, 560)
        TabPage流控制.TabIndex = 10
        TabPage流控制.Text = "流控制"
        ' 
        ' 界面_常规流程参数_V3
        ' 
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.FromArgb(CByte(48), CByte(48), CByte(48))
        Controls.Add(UiTabControl1)
        Font = New Font("微软雅黑", 9.75F)
        ForeColor = Color.Silver
        Name = "界面_常规流程参数_V3"
        Padding = New Padding(2)
        Size = New Size(1000, 600)
        UiTabControl1.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents TabPage音频参数 As TabPage
    Friend WithEvents TabPage视频滤镜 As TabPage
    Friend WithEvents TabPage色彩管理 As TabPage
    Friend WithEvents TabPage视频参数 As TabPage
    Friend WithEvents TabPage解码设置 As TabPage
    Friend WithEvents TabPage输出文件 As TabPage
    Friend WithEvents TabPage参数总览 As TabPage
    Friend WithEvents UiTabControl1 As Sunny.UI.UITabControl
    Friend WithEvents TabPage剪辑区间 As TabPage
    Friend WithEvents TabPage流控制 As TabPage

End Class
