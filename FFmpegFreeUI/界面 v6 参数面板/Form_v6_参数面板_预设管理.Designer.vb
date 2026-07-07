<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_v6_参数面板_预设管理
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
        ModernPanel1 = New LakeUI.ModernPanel()
        Panel2 = New Panel()
        MTB_预设命令行预览 = New LakeUI.ModernTextBox()
        JustEmptyControl2 = New LakeUI.JustEmptyControl()
        MTB_预设参数总览 = New LakeUI.ModernTextBox()
        JustEmptyControl1 = New LakeUI.JustEmptyControl()
        MLB_预设列表 = New LakeUI.ModernListBox()
        Panel3 = New Panel()
        MTB_预设名称 = New LakeUI.ModernTextBox()
        JustEmptyControl9 = New LakeUI.JustEmptyControl()
        MB_变更预设名称 = New LakeUI.ModernButton()
        Panel1 = New Panel()
        MCK_额外保存输出位置 = New LakeUI.ModernCheckBox()
        JustEmptyControl8 = New LakeUI.JustEmptyControl()
        MB_重置所有预设参数 = New LakeUI.ModernButton()
        JustEmptyControl7 = New LakeUI.JustEmptyControl()
        MB_导入预设 = New LakeUI.ModernButton()
        JustEmptyControl6 = New LakeUI.JustEmptyControl()
        MB_导出预设 = New LakeUI.ModernButton()
        JustEmptyControl5 = New LakeUI.JustEmptyControl()
        MB_读取预设 = New LakeUI.ModernButton()
        JustEmptyControl4 = New LakeUI.JustEmptyControl()
        MB_保存预设 = New LakeUI.ModernButton()
        JustEmptyControl3 = New LakeUI.JustEmptyControl()
        MCB_预设来源 = New LakeUI.ModernComboBox()
        HCL_预设管理说明 = New LakeUI.HtmlColorLabel()
        Panel4 = New Panel()
        MTB_预设备注 = New LakeUI.ModernTextBox()
        JustEmptyControl10 = New LakeUI.JustEmptyControl()
        MB_变更预设备注 = New LakeUI.ModernButton()
        ModernPanel1.SuspendLayout()
        Panel2.SuspendLayout()
        Panel3.SuspendLayout()
        Panel1.SuspendLayout()
        Panel4.SuspendLayout()
        SuspendLayout()
        '
        ' ModernPanel1
        '
        ModernPanel1.BackColor1 = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ModernPanel1.BorderSize = 0
        ModernPanel1.Controls.Add(Panel2)
        ModernPanel1.Controls.Add(Panel3)
        ModernPanel1.Controls.Add(Panel1)
        ModernPanel1.Controls.Add(HCL_预设管理说明)
        ModernPanel1.Controls.Add(Panel4)
        ModernPanel1.Dock = DockStyle.Fill
        ModernPanel1.Location = New Point(0, 0)
        ModernPanel1.Name = "ModernPanel1"
        ModernPanel1.Padding = New Padding(20)
        ModernPanel1.Size = New Size(816, 633)
        ModernPanel1.TabIndex = 0
        '
        ' Panel2
        '
        Panel2.Controls.Add(MTB_预设命令行预览)
        Panel2.Controls.Add(JustEmptyControl2)
        Panel2.Controls.Add(MTB_预设参数总览)
        Panel2.Controls.Add(JustEmptyControl1)
        Panel2.Controls.Add(MLB_预设列表)
        Panel2.Dock = DockStyle.Fill
        Panel2.Location = New Point(20, 129)
        Panel2.Name = "Panel2"
        Panel2.Padding = New Padding(0, 10, 0, 0)
        Panel2.Size = New Size(776, 442)
        Panel2.TabIndex = 11
        '
        ' MTB_预设命令行预览
        '
        MTB_预设命令行预览.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_预设命令行预览.BorderColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_预设命令行预览.BorderColorFocus = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_预设命令行预览.BorderRadius = 10
        MTB_预设命令行预览.BorderSize = 0
        MTB_预设命令行预览.Dock = DockStyle.Fill
        MTB_预设命令行预览.LineHeight = 20
        MTB_预设命令行预览.Location = New Point(542, 10)
        MTB_预设命令行预览.Margin = New Padding(2)
        MTB_预设命令行预览.MultiLine = True
        MTB_预设命令行预览.Name = "MTB_预设命令行预览"
        MTB_预设命令行预览.Padding = New Padding(12, 10, 12, 10)
        MTB_预设命令行预览.ReadOnly = True
        MTB_预设命令行预览.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_预设命令行预览.Size = New Size(234, 432)
        MTB_预设命令行预览.TabIndex = 4
        MTB_预设命令行预览.Text = ""
        '
        ' JustEmptyControl2
        '
        JustEmptyControl2.Dock = DockStyle.Left
        JustEmptyControl2.Location = New Point(532, 10)
        JustEmptyControl2.Name = "JustEmptyControl2"
        JustEmptyControl2.Size = New Size(10, 432)
        JustEmptyControl2.TabIndex = 3
        '
        ' MTB_预设参数总览
        '
        MTB_预设参数总览.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_预设参数总览.BorderColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_预设参数总览.BorderColorFocus = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_预设参数总览.BorderRadius = 10
        MTB_预设参数总览.BorderSize = 0
        MTB_预设参数总览.Dock = DockStyle.Left
        MTB_预设参数总览.LineHeight = 20
        MTB_预设参数总览.Location = New Point(265, 10)
        MTB_预设参数总览.Margin = New Padding(2)
        MTB_预设参数总览.MultiLine = True
        MTB_预设参数总览.Name = "MTB_预设参数总览"
        MTB_预设参数总览.Padding = New Padding(12, 10, 12, 10)
        MTB_预设参数总览.ReadOnly = True
        MTB_预设参数总览.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_预设参数总览.Size = New Size(267, 432)
        MTB_预设参数总览.TabIndex = 2
        MTB_预设参数总览.Text = ""
        '
        ' JustEmptyControl1
        '
        JustEmptyControl1.Dock = DockStyle.Left
        JustEmptyControl1.Location = New Point(255, 10)
        JustEmptyControl1.Name = "JustEmptyControl1"
        JustEmptyControl1.Size = New Size(10, 432)
        JustEmptyControl1.TabIndex = 1
        '
        ' MLB_预设列表
        '
        MLB_预设列表.AllowDragReorder = True
        MLB_预设列表.AnimationDuration = 300
        MLB_预设列表.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MLB_预设列表.BorderRadius = 10
        MLB_预设列表.BorderSize = 0
        MLB_预设列表.Dock = DockStyle.Left
        MLB_预设列表.ItemHoverColor = Color.FromArgb(CByte(20), CByte(220), CByte(220), CByte(220))
        MLB_预设列表.ItemPaddingLeft = 10
        MLB_预设列表.ItemSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MLB_预设列表.Location = New Point(0, 10)
        MLB_预设列表.MultiSelect = False
        MLB_预设列表.Name = "MLB_预设列表"
        MLB_预设列表.Size = New Size(255, 432)
        MLB_预设列表.TabIndex = 5
        MLB_预设列表.ToolTipBorderColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MLB_预设列表.ToolTipMaxWidth = 350
        '
        ' Panel3
        '
        Panel3.Controls.Add(MTB_预设名称)
        Panel3.Controls.Add(JustEmptyControl9)
        Panel3.Controls.Add(MB_变更预设名称)
        Panel3.Dock = DockStyle.Top
        Panel3.Location = New Point(20, 87)
        Panel3.Name = "Panel3"
        Panel3.Padding = New Padding(0, 10, 0, 0)
        Panel3.Size = New Size(776, 42)
        Panel3.TabIndex = 12
        '
        ' MTB_预设名称
        '
        MTB_预设名称.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_预设名称.BorderColor = Color.Transparent
        MTB_预设名称.BorderColorFocus = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MTB_预设名称.BorderRadius = 10
        MTB_预设名称.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        MTB_预设名称.Dock = DockStyle.Fill
        MTB_预设名称.Location = New Point(0, 10)
        MTB_预设名称.Margin = New Padding(2)
        MTB_预设名称.Name = "MTB_预设名称"
        MTB_预设名称.Padding = New Padding(10, 0, 10, 0)
        MTB_预设名称.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_预设名称.Size = New Size(666, 32)
        MTB_预设名称.TabIndex = 5
        MTB_预设名称.WaterText = "这里显示选中的预设项名称，主用于显示完整名称，也可在此直接重命名"
        MTB_预设名称.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        '
        ' JustEmptyControl9
        '
        JustEmptyControl9.Dock = DockStyle.Right
        JustEmptyControl9.Location = New Point(666, 10)
        JustEmptyControl9.Name = "JustEmptyControl9"
        JustEmptyControl9.Size = New Size(10, 32)
        JustEmptyControl9.TabIndex = 13
        '
        ' MB_变更预设名称
        '
        MB_变更预设名称.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_变更预设名称.BorderRadius = 10
        MB_变更预设名称.BorderSize = 0
        MB_变更预设名称.Dock = DockStyle.Right
        MB_变更预设名称.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_变更预设名称.Location = New Point(676, 10)
        MB_变更预设名称.Margin = New Padding(2)
        MB_变更预设名称.Name = "MB_变更预设名称"
        MB_变更预设名称.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_变更预设名称.Size = New Size(100, 32)
        MB_变更预设名称.TabIndex = 12
        MB_变更预设名称.Text = "变更名称"
        '
        ' Panel1
        '
        Panel1.Controls.Add(MCK_额外保存输出位置)
        Panel1.Controls.Add(JustEmptyControl8)
        Panel1.Controls.Add(MB_重置所有预设参数)
        Panel1.Controls.Add(JustEmptyControl7)
        Panel1.Controls.Add(MB_导入预设)
        Panel1.Controls.Add(JustEmptyControl6)
        Panel1.Controls.Add(MB_导出预设)
        Panel1.Controls.Add(JustEmptyControl5)
        Panel1.Controls.Add(MB_读取预设)
        Panel1.Controls.Add(JustEmptyControl4)
        Panel1.Controls.Add(MB_保存预设)
        Panel1.Controls.Add(JustEmptyControl3)
        Panel1.Controls.Add(MCB_预设来源)
        Panel1.Dock = DockStyle.Top
        Panel1.Location = New Point(20, 45)
        Panel1.Name = "Panel1"
        Panel1.Padding = New Padding(0, 10, 0, 0)
        Panel1.Size = New Size(776, 42)
        Panel1.TabIndex = 10
        '
        ' MCK_额外保存输出位置
        '
        MCK_额外保存输出位置.AutoSize = True
        MCK_额外保存输出位置.BoxBorderRadius = 5
        MCK_额外保存输出位置.BoxBorderSize = 0
        MCK_额外保存输出位置.BoxCheckedBackColor = Color.OliveDrab
        MCK_额外保存输出位置.BoxInnerPadding = 6
        MCK_额外保存输出位置.BoxSize = 22
        MCK_额外保存输出位置.BoxTextSpacing = 10
        MCK_额外保存输出位置.BoxUncheckedBackColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCK_额外保存输出位置.ClickAnywhere = True
        MCK_额外保存输出位置.Dock = DockStyle.Left
        MCK_额外保存输出位置.Location = New Point(570, 10)
        MCK_额外保存输出位置.Name = "MCK_额外保存输出位置"
        MCK_额外保存输出位置.Size = New Size(144, 32)
        MCK_额外保存输出位置.TabIndex = 14
        MCK_额外保存输出位置.Text = "额外保存输出位置"
        '
        ' JustEmptyControl8
        '
        JustEmptyControl8.Dock = DockStyle.Left
        JustEmptyControl8.Location = New Point(560, 10)
        JustEmptyControl8.Name = "JustEmptyControl8"
        JustEmptyControl8.Size = New Size(10, 32)
        JustEmptyControl8.TabIndex = 12
        '
        ' MB_重置所有预设参数
        '
        MB_重置所有预设参数.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_重置所有预设参数.BorderRadius = 10
        MB_重置所有预设参数.BorderSize = 0
        MB_重置所有预设参数.Dock = DockStyle.Left
        MB_重置所有预设参数.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_重置所有预设参数.Location = New Point(460, 10)
        MB_重置所有预设参数.Margin = New Padding(2)
        MB_重置所有预设参数.Name = "MB_重置所有预设参数"
        MB_重置所有预设参数.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_重置所有预设参数.Size = New Size(100, 32)
        MB_重置所有预设参数.TabIndex = 11
        MB_重置所有预设参数.Text = "重置所有"
        '
        ' JustEmptyControl7
        '
        JustEmptyControl7.Dock = DockStyle.Left
        JustEmptyControl7.Location = New Point(450, 10)
        JustEmptyControl7.Name = "JustEmptyControl7"
        JustEmptyControl7.Size = New Size(10, 32)
        JustEmptyControl7.TabIndex = 10
        '
        ' MB_导入预设
        '
        MB_导入预设.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_导入预设.BorderRadius = 10
        MB_导入预设.BorderSize = 0
        MB_导入预设.Dock = DockStyle.Left
        MB_导入预设.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_导入预设.Location = New Point(385, 10)
        MB_导入预设.Margin = New Padding(2)
        MB_导入预设.Name = "MB_导入预设"
        MB_导入预设.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_导入预设.Size = New Size(65, 32)
        MB_导入预设.TabIndex = 9
        MB_导入预设.Text = "导入"
        '
        ' JustEmptyControl6
        '
        JustEmptyControl6.Dock = DockStyle.Left
        JustEmptyControl6.Location = New Point(375, 10)
        JustEmptyControl6.Name = "JustEmptyControl6"
        JustEmptyControl6.Size = New Size(10, 32)
        JustEmptyControl6.TabIndex = 8
        '
        ' MB_导出预设
        '
        MB_导出预设.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_导出预设.BorderRadius = 10
        MB_导出预设.BorderSize = 0
        MB_导出预设.Dock = DockStyle.Left
        MB_导出预设.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_导出预设.Location = New Point(310, 10)
        MB_导出预设.Margin = New Padding(2)
        MB_导出预设.Name = "MB_导出预设"
        MB_导出预设.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_导出预设.Size = New Size(65, 32)
        MB_导出预设.TabIndex = 7
        MB_导出预设.Text = "导出"
        '
        ' JustEmptyControl5
        '
        JustEmptyControl5.Dock = DockStyle.Left
        JustEmptyControl5.Location = New Point(300, 10)
        JustEmptyControl5.Name = "JustEmptyControl5"
        JustEmptyControl5.Size = New Size(10, 32)
        JustEmptyControl5.TabIndex = 6
        '
        ' MB_读取预设
        '
        MB_读取预设.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_读取预设.BorderRadius = 10
        MB_读取预设.BorderSize = 0
        MB_读取预设.Dock = DockStyle.Left
        MB_读取预设.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_读取预设.Location = New Point(235, 10)
        MB_读取预设.Margin = New Padding(2)
        MB_读取预设.Name = "MB_读取预设"
        MB_读取预设.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_读取预设.Size = New Size(65, 32)
        MB_读取预设.TabIndex = 5
        MB_读取预设.Text = "读取"
        '
        ' JustEmptyControl4
        '
        JustEmptyControl4.Dock = DockStyle.Left
        JustEmptyControl4.Location = New Point(225, 10)
        JustEmptyControl4.Name = "JustEmptyControl4"
        JustEmptyControl4.Size = New Size(10, 32)
        JustEmptyControl4.TabIndex = 4
        '
        ' MB_保存预设
        '
        MB_保存预设.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_保存预设.BorderRadius = 10
        MB_保存预设.BorderSize = 0
        MB_保存预设.Dock = DockStyle.Left
        MB_保存预设.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_保存预设.Location = New Point(160, 10)
        MB_保存预设.Margin = New Padding(2)
        MB_保存预设.Name = "MB_保存预设"
        MB_保存预设.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_保存预设.Size = New Size(65, 32)
        MB_保存预设.TabIndex = 3
        MB_保存预设.Text = "保存"
        '
        ' JustEmptyControl3
        '
        JustEmptyControl3.Dock = DockStyle.Left
        JustEmptyControl3.Location = New Point(150, 10)
        JustEmptyControl3.Name = "JustEmptyControl3"
        JustEmptyControl3.Size = New Size(10, 32)
        JustEmptyControl3.TabIndex = 2
        '
        ' MCB_预设来源
        '
        MCB_预设来源.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_预设来源.BorderRadius = 10
        MCB_预设来源.BorderSize = 0
        MCB_预设来源.Dock = DockStyle.Left
        MCB_预设来源.DropDownBackdropBlurPasses = 2
        MCB_预设来源.DropDownBackdropBlurRadius = 30
        MCB_预设来源.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_预设来源.DropDownHoverColor = Color.FromArgb(CByte(20), CByte(220), CByte(220), CByte(220))
        MCB_预设来源.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_预设来源.DropDownPadding = New Padding(10)
        MCB_预设来源.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_预设来源.DropDownSelectedForeColor = Color.White
        MCB_预设来源.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_预设来源.Items.Add("开发者内置")
        MCB_预设来源.Items.Add("用户自定义")
        MCB_预设来源.Items.Add("从社区下载")
        MCB_预设来源.Location = New Point(0, 10)
        MCB_预设来源.Margin = New Padding(2, 2, 2, 2)
        MCB_预设来源.Name = "MCB_预设来源"
        MCB_预设来源.Padding = New Padding(10, 0, 10, 0)
        MCB_预设来源.Size = New Size(150, 32)
        MCB_预设来源.TabIndex = 0
        MCB_预设来源.ToolTipGap = -1
        MCB_预设来源.ToolTipMaxWidth = 350
        MCB_预设来源.ToolTipPadding = New Padding(15)
        MCB_预设来源.WaterText = "选择预设来源"
        MCB_预设来源.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        '
        ' HCL_预设管理说明
        '
        HCL_预设管理说明.AutoSize = True
        HCL_预设管理说明.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_预设管理说明.Dock = DockStyle.Top
        HCL_预设管理说明.ForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        HCL_预设管理说明.Location = New Point(20, 20)
        HCL_预设管理说明.Margin = New Padding(2)
        HCL_预设管理说明.Name = "HCL_预设管理说明"
        HCL_预设管理说明.Size = New Size(776, 25)
        HCL_预设管理说明.TabIndex = 7
        HCL_预设管理说明.Text = "<span style=""font-size:13; color:Silver"">预设管理</span>   先选择预设来源，双击或读取来加载，用户和社区预设选中后按 Delete 删除到回收站"
        '
        ' Panel4
        '
        Panel4.Controls.Add(MTB_预设备注)
        Panel4.Controls.Add(JustEmptyControl10)
        Panel4.Controls.Add(MB_变更预设备注)
        Panel4.Dock = DockStyle.Bottom
        Panel4.Location = New Point(20, 571)
        Panel4.Name = "Panel4"
        Panel4.Padding = New Padding(0, 10, 0, 0)
        Panel4.Size = New Size(776, 42)
        Panel4.TabIndex = 13
        '
        ' MTB_预设备注
        '
        MTB_预设备注.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_预设备注.BorderColor = Color.Transparent
        MTB_预设备注.BorderColorFocus = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MTB_预设备注.BorderRadius = 10
        MTB_预设备注.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        MTB_预设备注.Dock = DockStyle.Fill
        MTB_预设备注.Location = New Point(0, 10)
        MTB_预设备注.Margin = New Padding(2)
        MTB_预设备注.Name = "MTB_预设备注"
        MTB_预设备注.Padding = New Padding(10, 0, 10, 0)
        MTB_预设备注.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_预设备注.Size = New Size(666, 32)
        MTB_预设备注.TabIndex = 5
        MTB_预设备注.WaterText = "这里显示选中的预设项备注，备注会在鼠标移上时显示在侧边，如果有相同的名称则会显示一样的"
        MTB_预设备注.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        '
        ' JustEmptyControl10
        '
        JustEmptyControl10.Dock = DockStyle.Right
        JustEmptyControl10.Location = New Point(666, 10)
        JustEmptyControl10.Name = "JustEmptyControl10"
        JustEmptyControl10.Size = New Size(10, 32)
        JustEmptyControl10.TabIndex = 13
        '
        ' MB_变更预设备注
        '
        MB_变更预设备注.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_变更预设备注.BorderRadius = 10
        MB_变更预设备注.BorderSize = 0
        MB_变更预设备注.Dock = DockStyle.Right
        MB_变更预设备注.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_变更预设备注.Location = New Point(676, 10)
        MB_变更预设备注.Margin = New Padding(2)
        MB_变更预设备注.Name = "MB_变更预设备注"
        MB_变更预设备注.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_变更预设备注.Size = New Size(100, 32)
        MB_变更预设备注.TabIndex = 12
        MB_变更预设备注.Text = "变更备注"
        '
        ' Form_v6_参数面板_预设管理
        '
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ClientSize = New Size(816, 633)
        Controls.Add(ModernPanel1)
        Font = New Font("Microsoft YaHei UI", 10F)
        ForeColor = Color.Silver
        Name = "Form_v6_参数面板_预设管理"
        Text = "Form_v6_参数面板_预设管理"
        ModernPanel1.ResumeLayout(False)
        ModernPanel1.PerformLayout()
        Panel2.ResumeLayout(False)
        Panel3.ResumeLayout(False)
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        Panel4.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents ModernPanel1 As LakeUI.ModernPanel
    Friend WithEvents HCL_预设管理说明 As LakeUI.HtmlColorLabel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents MCB_预设来源 As LakeUI.ModernComboBox
    Friend WithEvents Panel2 As Panel
    Friend WithEvents JustEmptyControl1 As LakeUI.JustEmptyControl
    Friend WithEvents MTB_预设命令行预览 As LakeUI.ModernTextBox
    Friend WithEvents JustEmptyControl2 As LakeUI.JustEmptyControl
    Friend WithEvents MTB_预设参数总览 As LakeUI.ModernTextBox
    Friend WithEvents JustEmptyControl3 As LakeUI.JustEmptyControl
    Friend WithEvents MB_重置所有预设参数 As LakeUI.ModernButton
    Friend WithEvents JustEmptyControl7 As LakeUI.JustEmptyControl
    Friend WithEvents MB_导入预设 As LakeUI.ModernButton
    Friend WithEvents JustEmptyControl6 As LakeUI.JustEmptyControl
    Friend WithEvents MB_导出预设 As LakeUI.ModernButton
    Friend WithEvents JustEmptyControl5 As LakeUI.JustEmptyControl
    Friend WithEvents MB_读取预设 As LakeUI.ModernButton
    Friend WithEvents JustEmptyControl4 As LakeUI.JustEmptyControl
    Friend WithEvents MB_保存预设 As LakeUI.ModernButton
    Friend WithEvents JustEmptyControl8 As LakeUI.JustEmptyControl
    Friend WithEvents MCK_额外保存输出位置 As LakeUI.ModernCheckBox
    Friend WithEvents MLB_预设列表 As LakeUI.ModernListBox
    Friend WithEvents Panel3 As Panel
    Friend WithEvents MTB_预设名称 As LakeUI.ModernTextBox
    Friend WithEvents JustEmptyControl9 As LakeUI.JustEmptyControl
    Friend WithEvents MB_变更预设名称 As LakeUI.ModernButton
    Friend WithEvents Panel4 As Panel
    Friend WithEvents MTB_预设备注 As LakeUI.ModernTextBox
    Friend WithEvents JustEmptyControl10 As LakeUI.JustEmptyControl
    Friend WithEvents MB_变更预设备注 As LakeUI.ModernButton
End Class
