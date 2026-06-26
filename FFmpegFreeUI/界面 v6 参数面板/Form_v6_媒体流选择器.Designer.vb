<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form_v6_媒体流选择器
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        ModernPanel1 = New LakeUI.ModernPanel()
        列表面板 = New LakeUI.ModernPanel()
        顶部面板 = New Panel()
        MB_全选字幕 = New LakeUI.ModernButton()
        JustEmptyControl2 = New LakeUI.JustEmptyControl()
        MB_全选音频 = New LakeUI.ModernButton()
        JustEmptyControl1 = New LakeUI.JustEmptyControl()
        MB_全选视频 = New LakeUI.ModernButton()
        底部面板 = New Panel()
        MB_确认选择 = New LakeUI.ModernButton()
        MB_重置选择 = New LakeUI.ModernButton()
        JustEmptyControl3 = New LakeUI.JustEmptyControl()
        MB_打开文件 = New LakeUI.ModernButton()
        ModernPanel1.SuspendLayout()
        顶部面板.SuspendLayout()
        底部面板.SuspendLayout()
        SuspendLayout()
        '
        ' ModernPanel1
        '
        ModernPanel1.AllowDrop = True
        ModernPanel1.BackColor1 = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ModernPanel1.BorderSize = 0
        ModernPanel1.Controls.Add(列表面板)
        ModernPanel1.Controls.Add(顶部面板)
        ModernPanel1.Controls.Add(底部面板)
        ModernPanel1.Dock = DockStyle.Fill
        ModernPanel1.Location = New Point(0, 0)
        ModernPanel1.Name = "ModernPanel1"
        ModernPanel1.Padding = New Padding(12)
        ModernPanel1.Size = New Size(665, 462)
        ModernPanel1.TabIndex = 0
        '
        ' 列表面板
        '
        列表面板.AllowDrop = True
        列表面板.BackColor1 = Color.Transparent
        列表面板.BorderSize = 0
        列表面板.Dock = DockStyle.Fill
        列表面板.Location = New Point(12, 52)
        列表面板.Name = "列表面板"
        列表面板.Padding = New Padding(10, 0, 10, 0)
        列表面板.ScrollBarMode = LakeUI.ModernPanel.ScrollMode.Vertical
        列表面板.Size = New Size(641, 358)
        列表面板.TabIndex = 2
        '
        ' 顶部面板
        '
        顶部面板.AllowDrop = True
        顶部面板.Controls.Add(MB_全选字幕)
        顶部面板.Controls.Add(JustEmptyControl2)
        顶部面板.Controls.Add(MB_全选音频)
        顶部面板.Controls.Add(JustEmptyControl1)
        顶部面板.Controls.Add(MB_全选视频)
        顶部面板.Dock = DockStyle.Top
        顶部面板.Location = New Point(12, 12)
        顶部面板.Name = "顶部面板"
        顶部面板.Padding = New Padding(0, 0, 0, 10)
        顶部面板.Size = New Size(641, 40)
        顶部面板.TabIndex = 0
        '
        ' MB_全选字幕
        '
        MB_全选字幕.AllowDrop = True
        MB_全选字幕.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_全选字幕.BorderRadius = 8
        MB_全选字幕.BorderSize = 0
        MB_全选字幕.Dock = DockStyle.Left
        MB_全选字幕.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_全选字幕.Location = New Point(220, 0)
        MB_全选字幕.Margin = New Padding(2)
        MB_全选字幕.Name = "MB_全选字幕"
        MB_全选字幕.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_全选字幕.Size = New Size(100, 30)
        MB_全选字幕.TabIndex = 4
        MB_全选字幕.Text = "全选字幕"
        '
        ' JustEmptyControl2
        '
        JustEmptyControl2.Dock = DockStyle.Left
        JustEmptyControl2.Location = New Point(210, 0)
        JustEmptyControl2.Name = "JustEmptyControl2"
        JustEmptyControl2.Size = New Size(10, 30)
        JustEmptyControl2.TabIndex = 3
        '
        ' MB_全选音频
        '
        MB_全选音频.AllowDrop = True
        MB_全选音频.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_全选音频.BorderRadius = 8
        MB_全选音频.BorderSize = 0
        MB_全选音频.Dock = DockStyle.Left
        MB_全选音频.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_全选音频.Location = New Point(110, 0)
        MB_全选音频.Margin = New Padding(2)
        MB_全选音频.Name = "MB_全选音频"
        MB_全选音频.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_全选音频.Size = New Size(100, 30)
        MB_全选音频.TabIndex = 2
        MB_全选音频.Text = "全选音频"
        '
        ' JustEmptyControl1
        '
        JustEmptyControl1.Dock = DockStyle.Left
        JustEmptyControl1.Location = New Point(100, 0)
        JustEmptyControl1.Name = "JustEmptyControl1"
        JustEmptyControl1.Size = New Size(10, 30)
        JustEmptyControl1.TabIndex = 1
        '
        ' MB_全选视频
        '
        MB_全选视频.AllowDrop = True
        MB_全选视频.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_全选视频.BorderRadius = 8
        MB_全选视频.BorderSize = 0
        MB_全选视频.Dock = DockStyle.Left
        MB_全选视频.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_全选视频.Location = New Point(0, 0)
        MB_全选视频.Margin = New Padding(2)
        MB_全选视频.Name = "MB_全选视频"
        MB_全选视频.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_全选视频.Size = New Size(100, 30)
        MB_全选视频.TabIndex = 0
        MB_全选视频.Text = "全选视频"
        '
        ' 底部面板
        '
        底部面板.AllowDrop = True
        底部面板.Controls.Add(MB_确认选择)
        底部面板.Controls.Add(MB_重置选择)
        底部面板.Controls.Add(JustEmptyControl3)
        底部面板.Controls.Add(MB_打开文件)
        底部面板.Dock = DockStyle.Bottom
        底部面板.Location = New Point(12, 410)
        底部面板.Name = "底部面板"
        底部面板.Padding = New Padding(0, 10, 0, 0)
        底部面板.Size = New Size(641, 40)
        底部面板.TabIndex = 1
        '
        ' MB_确认选择
        '
        MB_确认选择.AllowDrop = True
        MB_确认选择.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_确认选择.BorderRadius = 8
        MB_确认选择.BorderSize = 0
        MB_确认选择.Dock = DockStyle.Right
        MB_确认选择.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_确认选择.Location = New Point(521, 10)
        MB_确认选择.Margin = New Padding(2)
        MB_确认选择.Name = "MB_确认选择"
        MB_确认选择.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_确认选择.Size = New Size(120, 30)
        MB_确认选择.TabIndex = 3
        MB_确认选择.Text = "确认选择"
        '
        ' MB_重置选择
        '
        MB_重置选择.AllowDrop = True
        MB_重置选择.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_重置选择.BorderRadius = 8
        MB_重置选择.BorderSize = 0
        MB_重置选择.Dock = DockStyle.Left
        MB_重置选择.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_重置选择.Location = New Point(110, 10)
        MB_重置选择.Margin = New Padding(2)
        MB_重置选择.Name = "MB_重置选择"
        MB_重置选择.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_重置选择.Size = New Size(100, 30)
        MB_重置选择.TabIndex = 2
        MB_重置选择.Text = "重置选择"
        '
        ' JustEmptyControl3
        '
        JustEmptyControl3.Dock = DockStyle.Left
        JustEmptyControl3.Location = New Point(100, 10)
        JustEmptyControl3.Name = "JustEmptyControl3"
        JustEmptyControl3.Size = New Size(10, 30)
        JustEmptyControl3.TabIndex = 1
        '
        ' MB_打开文件
        '
        MB_打开文件.AllowDrop = True
        MB_打开文件.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_打开文件.BorderRadius = 8
        MB_打开文件.BorderSize = 0
        MB_打开文件.Dock = DockStyle.Left
        MB_打开文件.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_打开文件.Location = New Point(0, 10)
        MB_打开文件.Margin = New Padding(2)
        MB_打开文件.Name = "MB_打开文件"
        MB_打开文件.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_打开文件.Size = New Size(100, 30)
        MB_打开文件.TabIndex = 0
        MB_打开文件.Text = "打开文件"
        '
        ' Form_v6_媒体流选择器
        '
        AllowDrop = True
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ClientSize = New Size(665, 462)
        Controls.Add(ModernPanel1)
        Font = New Font("Microsoft YaHei UI", 10F)
        ForeColor = Color.Silver
        MinimumSize = New Size(640, 360)
        Name = "Form_v6_媒体流选择器"
        ShowIcon = False
        ShowInTaskbar = False
        StartPosition = FormStartPosition.Manual
        Text = "v6 媒体流选择器"
        ModernPanel1.ResumeLayout(False)
        顶部面板.ResumeLayout(False)
        底部面板.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents ModernPanel1 As LakeUI.ModernPanel
    Friend WithEvents 顶部面板 As Panel
    Friend WithEvents 底部面板 As Panel
    Friend WithEvents 列表面板 As LakeUI.ModernPanel
    Friend WithEvents MB_全选视频 As LakeUI.ModernButton
    Friend WithEvents MB_全选音频 As LakeUI.ModernButton
    Friend WithEvents MB_全选字幕 As LakeUI.ModernButton
    Friend WithEvents MB_打开文件 As LakeUI.ModernButton
    Friend WithEvents MB_重置选择 As LakeUI.ModernButton
    Friend WithEvents MB_确认选择 As LakeUI.ModernButton
    Friend WithEvents JustEmptyControl1 As LakeUI.JustEmptyControl
    Friend WithEvents JustEmptyControl2 As LakeUI.JustEmptyControl
    Friend WithEvents JustEmptyControl3 As LakeUI.JustEmptyControl
End Class
