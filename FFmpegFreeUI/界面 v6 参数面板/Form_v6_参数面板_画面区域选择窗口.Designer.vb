<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_v6_参数面板_画面区域选择窗口
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal 正在释放 As Boolean)
        Try
            If 正在释放 AndAlso 组件容器 IsNot Nothing Then
                组件容器.Dispose()
            End If
        Finally
            MyBase.Dispose(正在释放)
        End Try
    End Sub

    'Windows 窗体设计器所必需的
    Private 组件容器 As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        面板_窗口内容 = New LakeUI.ModernPanel()
        PPB_画面区域预览 = New LakeUI.PixelPictureBox()
        间隔_预览区 = New LakeUI.JustEmptyControl()
        面板_操作栏 = New LakeUI.ModernPanel()
        MCK_居中裁剪框 = New LakeUI.ModernCheckBox()
        MCB_裁剪比例 = New LakeUI.ModernComboBox()
        间隔_裁剪比例 = New LakeUI.JustEmptyControl()
        MCB_预览时间戳 = New LakeUI.ModernComboBox()
        间隔_预览时间戳 = New LakeUI.JustEmptyControl()
        MTB_裁剪参数 = New LakeUI.ModernTextBox()
        间隔_裁剪参数 = New LakeUI.JustEmptyControl()
        MB_完成 = New LakeUI.ModernButton()
        间隔_完成按钮 = New LakeUI.JustEmptyControl()
        MB_打开 = New LakeUI.ModernButton()
        HCL_画面区域选择说明 = New LakeUI.HtmlColorLabel()
        面板_窗口内容.SuspendLayout()
        面板_操作栏.SuspendLayout()
        SuspendLayout()
        ' 
        ' 面板_窗口内容
        ' 
        面板_窗口内容.BackColor = Color.Transparent
        面板_窗口内容.BackColor1 = Color.Transparent
        面板_窗口内容.BorderSize = 0
        面板_窗口内容.Controls.Add(PPB_画面区域预览)
        面板_窗口内容.Controls.Add(间隔_预览区)
        面板_窗口内容.Controls.Add(面板_操作栏)
        面板_窗口内容.Controls.Add(HCL_画面区域选择说明)
        面板_窗口内容.Dock = DockStyle.Fill
        面板_窗口内容.Location = New Point(0, 0)
        面板_窗口内容.Name = "面板_窗口内容"
        面板_窗口内容.Padding = New Padding(20)
        面板_窗口内容.Size = New Size(772, 588)
        面板_窗口内容.TabIndex = 18
        ' 
        ' PPB_画面区域预览
        ' 
        PPB_画面区域预览.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        PPB_画面区域预览.BorderSize = 0
        PPB_画面区域预览.Dock = DockStyle.Fill
        PPB_画面区域预览.Location = New Point(20, 111)
        PPB_画面区域预览.Name = "PPB_画面区域预览"
        PPB_画面区域预览.Size = New Size(732, 457)
        PPB_画面区域预览.TabIndex = 19
        ' 
        ' 间隔_预览区
        ' 
        间隔_预览区.Dock = DockStyle.Top
        间隔_预览区.Location = New Point(20, 96)
        间隔_预览区.Name = "间隔_预览区"
        间隔_预览区.Size = New Size(732, 15)
        间隔_预览区.TabIndex = 18
        ' 
        ' 面板_操作栏
        ' 
        面板_操作栏.BackColor = Color.Transparent
        面板_操作栏.BackColor1 = Color.Transparent
        面板_操作栏.BorderSize = 0
        面板_操作栏.Controls.Add(MCK_居中裁剪框)
        面板_操作栏.Controls.Add(MCB_裁剪比例)
        面板_操作栏.Controls.Add(间隔_裁剪比例)
        面板_操作栏.Controls.Add(MCB_预览时间戳)
        面板_操作栏.Controls.Add(间隔_预览时间戳)
        面板_操作栏.Controls.Add(MTB_裁剪参数)
        面板_操作栏.Controls.Add(间隔_裁剪参数)
        面板_操作栏.Controls.Add(MB_完成)
        面板_操作栏.Controls.Add(间隔_完成按钮)
        面板_操作栏.Controls.Add(MB_打开)
        面板_操作栏.Dock = DockStyle.Top
        面板_操作栏.Location = New Point(20, 54)
        面板_操作栏.Name = "面板_操作栏"
        面板_操作栏.Padding = New Padding(0, 10, 0, 0)
        面板_操作栏.Size = New Size(732, 42)
        面板_操作栏.TabIndex = 16
        ' 
        ' MCK_居中裁剪框
        ' 
        MCK_居中裁剪框.AutoSize = True
        MCK_居中裁剪框.BoxBorderRadius = 5
        MCK_居中裁剪框.BoxBorderSize = 0
        MCK_居中裁剪框.BoxCheckedBackColor = Color.OliveDrab
        MCK_居中裁剪框.BoxInnerPadding = 6
        MCK_居中裁剪框.BoxSize = 22
        MCK_居中裁剪框.BoxTextSpacing = 10
        MCK_居中裁剪框.BoxUncheckedBackColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCK_居中裁剪框.ClickAnywhere = True
        MCK_居中裁剪框.Dock = DockStyle.Left
        MCK_居中裁剪框.Location = New Point(602, 10)
        MCK_居中裁剪框.Name = "MCK_居中裁剪框"
        MCK_居中裁剪框.Padding = New Padding(10, 5, 0, 5)
        MCK_居中裁剪框.Size = New Size(70, 32)
        MCK_居中裁剪框.TabIndex = 15
        MCK_居中裁剪框.Text = "居中"
        ' 
        ' MCB_裁剪比例
        ' 
        MCB_裁剪比例.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_裁剪比例.BorderRadius = 10
        MCB_裁剪比例.BorderSize = 0
        MCB_裁剪比例.Dock = DockStyle.Left
        MCB_裁剪比例.DropDownBackdropBlurPasses = 2
        MCB_裁剪比例.DropDownBackdropBlurRadius = 30
        MCB_裁剪比例.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_裁剪比例.DropDownHoverColor = Color.FromArgb(CByte(20), CByte(220), CByte(220), CByte(220))
        MCB_裁剪比例.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_裁剪比例.DropDownPadding = New Padding(10)
        MCB_裁剪比例.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_裁剪比例.DropDownSelectedForeColor = Color.White
        MCB_裁剪比例.Editable = True
        MCB_裁剪比例.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_裁剪比例.Items.Add("自由")
        MCB_裁剪比例.Items.Add("21:9")
        MCB_裁剪比例.Items.Add("16:9")
        MCB_裁剪比例.Items.Add("3:2")
        MCB_裁剪比例.Items.Add("2:1")
        MCB_裁剪比例.Items.Add("4:3")
        MCB_裁剪比例.Items.Add("1:1")
        MCB_裁剪比例.Location = New Point(502, 10)
        MCB_裁剪比例.Margin = New Padding(2, 2, 2, 2)
        MCB_裁剪比例.Name = "MCB_裁剪比例"
        MCB_裁剪比例.Padding = New Padding(10, 0, 10, 0)
        MCB_裁剪比例.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_裁剪比例.Size = New Size(100, 32)
        MCB_裁剪比例.TabIndex = 12
        MCB_裁剪比例.ToolTipGap = -1
        MCB_裁剪比例.ToolTipMaxWidth = 350
        MCB_裁剪比例.ToolTipPadding = New Padding(15)
        MCB_裁剪比例.WaterText = "比例"
        MCB_裁剪比例.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' 间隔_裁剪比例
        ' 
        间隔_裁剪比例.Dock = DockStyle.Left
        间隔_裁剪比例.Location = New Point(491, 10)
        间隔_裁剪比例.Name = "间隔_裁剪比例"
        间隔_裁剪比例.Size = New Size(11, 32)
        间隔_裁剪比例.TabIndex = 13
        ' 
        ' MCB_预览时间戳
        ' 
        MCB_预览时间戳.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_预览时间戳.BorderRadius = 10
        MCB_预览时间戳.BorderSize = 0
        MCB_预览时间戳.Dock = DockStyle.Left
        MCB_预览时间戳.DropDownBackdropBlurPasses = 2
        MCB_预览时间戳.DropDownBackdropBlurRadius = 30
        MCB_预览时间戳.DropDownBackdropMode = LakeUI.PopupBackdropMode.Auto
        MCB_预览时间戳.DropDownHoverColor = Color.FromArgb(CByte(20), CByte(220), CByte(220), CByte(220))
        MCB_预览时间戳.DropDownMode = LakeUI.ModernComboBox.DropDownDisplayMode.Overlay
        MCB_预览时间戳.DropDownPadding = New Padding(10)
        MCB_预览时间戳.DropDownSelectedColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_预览时间戳.DropDownSelectedForeColor = Color.White
        MCB_预览时间戳.Editable = True
        MCB_预览时间戳.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MCB_预览时间戳.Items.Add("")
        MCB_预览时间戳.Items.Add("第一帧")
        MCB_预览时间戳.Items.Add("1 分钟")
        MCB_预览时间戳.Items.Add("2 分钟")
        MCB_预览时间戳.Items.Add("3 分钟")
        MCB_预览时间戳.Items.Add("5 分钟")
        MCB_预览时间戳.Items.Add("10 分钟")
        MCB_预览时间戳.Location = New Point(341, 10)
        MCB_预览时间戳.Margin = New Padding(2, 2, 2, 2)
        MCB_预览时间戳.Name = "MCB_预览时间戳"
        MCB_预览时间戳.Padding = New Padding(10, 0, 10, 0)
        MCB_预览时间戳.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MCB_预览时间戳.Size = New Size(150, 32)
        MCB_预览时间戳.TabIndex = 16
        MCB_预览时间戳.ToolTipGap = -1
        MCB_预览时间戳.ToolTipMaxWidth = 350
        MCB_预览时间戳.ToolTipPadding = New Padding(15)
        MCB_预览时间戳.WaterText = "指定时间戳"
        MCB_预览时间戳.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' 间隔_预览时间戳
        ' 
        间隔_预览时间戳.Dock = DockStyle.Left
        间隔_预览时间戳.Location = New Point(330, 10)
        间隔_预览时间戳.Name = "间隔_预览时间戳"
        间隔_预览时间戳.Size = New Size(11, 32)
        间隔_预览时间戳.TabIndex = 10
        ' 
        ' MTB_裁剪参数
        ' 
        MTB_裁剪参数.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_裁剪参数.BorderColor = Color.Transparent
        MTB_裁剪参数.BorderColorFocus = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MTB_裁剪参数.BorderRadius = 10
        MTB_裁剪参数.CaretColor = Color.FromArgb(CByte(220), CByte(220), CByte(220))
        MTB_裁剪参数.Dock = DockStyle.Left
        MTB_裁剪参数.Location = New Point(162, 10)
        MTB_裁剪参数.Margin = New Padding(2)
        MTB_裁剪参数.Name = "MTB_裁剪参数"
        MTB_裁剪参数.Padding = New Padding(10, 0, 10, 0)
        MTB_裁剪参数.SelectionColor = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MTB_裁剪参数.Size = New Size(168, 32)
        MTB_裁剪参数.TabIndex = 9
        MTB_裁剪参数.WaterText = "宽:高:左上X:左上Y"
        MTB_裁剪参数.WaterTextForeColor = Color.FromArgb(CByte(120), CByte(255), CByte(255), CByte(255))
        ' 
        ' 间隔_裁剪参数
        ' 
        间隔_裁剪参数.Dock = DockStyle.Left
        间隔_裁剪参数.Location = New Point(151, 10)
        间隔_裁剪参数.Name = "间隔_裁剪参数"
        间隔_裁剪参数.Size = New Size(11, 32)
        间隔_裁剪参数.TabIndex = 8
        ' 
        ' MB_完成
        ' 
        MB_完成.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_完成.BorderRadius = 10
        MB_完成.BorderSize = 0
        MB_完成.Dock = DockStyle.Left
        MB_完成.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_完成.Location = New Point(81, 10)
        MB_完成.Margin = New Padding(2)
        MB_完成.Name = "MB_完成"
        MB_完成.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_完成.Size = New Size(70, 32)
        MB_完成.TabIndex = 7
        MB_完成.Text = "完成"
        ' 
        ' 间隔_完成按钮
        ' 
        间隔_完成按钮.Dock = DockStyle.Left
        间隔_完成按钮.Location = New Point(70, 10)
        间隔_完成按钮.Name = "间隔_完成按钮"
        间隔_完成按钮.Size = New Size(11, 32)
        间隔_完成按钮.TabIndex = 6
        ' 
        ' MB_打开
        ' 
        MB_打开.BackColor1 = Color.FromArgb(CByte(40), CByte(220), CByte(220), CByte(220))
        MB_打开.BorderRadius = 10
        MB_打开.BorderSize = 0
        MB_打开.Dock = DockStyle.Left
        MB_打开.HoverBackColor1 = Color.FromArgb(CByte(60), CByte(220), CByte(220), CByte(220))
        MB_打开.Location = New Point(0, 10)
        MB_打开.Margin = New Padding(2)
        MB_打开.Name = "MB_打开"
        MB_打开.PressedBackColor1 = Color.FromArgb(CByte(80), CByte(220), CByte(220), CByte(220))
        MB_打开.Size = New Size(70, 32)
        MB_打开.TabIndex = 1
        MB_打开.Text = "打开"
        ' 
        ' HCL_画面区域选择说明
        ' 
        HCL_画面区域选择说明.AutoSize = True
        HCL_画面区域选择说明.AutoSizeMode = AutoSizeMode.GrowAndShrink
        HCL_画面区域选择说明.Dock = DockStyle.Top
        HCL_画面区域选择说明.Location = New Point(20, 20)
        HCL_画面区域选择说明.Margin = New Padding(2)
        HCL_画面区域选择说明.Name = "HCL_画面区域选择说明"
        HCL_画面区域选择说明.Size = New Size(732, 34)
        HCL_画面区域选择说明.TabIndex = 15
        HCL_画面区域选择说明.Text = "可点击打开或拖入视频 / 图片；图片优先直接加载，视频默认取第 10 秒，可先选择预设或输入时间戳<br>鼠标左键移动视野，滚轮缩放，鼠标右键绘制新框选；比例可选择或手写 16:9 / 1.777"
        ' 
        ' Form_v6_参数面板_画面区域选择窗口
        ' 
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.FromArgb(CByte(24), CByte(24), CByte(24))
        ClientSize = New Size(772, 588)
        Controls.Add(面板_窗口内容)
        Font = New Font("Microsoft YaHei UI", 10F)
        ForeColor = Color.Silver
        MaximizeBox = False
        MinimizeBox = False
        Name = "Form_v6_参数面板_画面区域选择窗口"
        ShowIcon = False
        ShowInTaskbar = False
        StartPosition = FormStartPosition.Manual
        Text = "画面区域选择"
        面板_窗口内容.ResumeLayout(False)
        面板_窗口内容.PerformLayout()
        面板_操作栏.ResumeLayout(False)
        面板_操作栏.PerformLayout()
        ResumeLayout(False)
    End Sub
    Friend WithEvents 面板_窗口内容 As LakeUI.ModernPanel
    Friend WithEvents PPB_画面区域预览 As LakeUI.PixelPictureBox
    Friend WithEvents 间隔_预览区 As LakeUI.JustEmptyControl
    Friend WithEvents 面板_操作栏 As LakeUI.ModernPanel
    Friend WithEvents MCK_居中裁剪框 As LakeUI.ModernCheckBox
    Friend WithEvents MCB_裁剪比例 As LakeUI.ModernComboBox
    Friend WithEvents 间隔_裁剪比例 As LakeUI.JustEmptyControl
    Friend WithEvents 间隔_预览时间戳 As LakeUI.JustEmptyControl
    Friend WithEvents MTB_裁剪参数 As LakeUI.ModernTextBox
    Friend WithEvents 间隔_裁剪参数 As LakeUI.JustEmptyControl
    Friend WithEvents MB_完成 As LakeUI.ModernButton
    Friend WithEvents 间隔_完成按钮 As LakeUI.JustEmptyControl
    Friend WithEvents MB_打开 As LakeUI.ModernButton
    Friend WithEvents HCL_画面区域选择说明 As LakeUI.HtmlColorLabel
    Friend WithEvents MCB_预览时间戳 As LakeUI.ModernComboBox
End Class
