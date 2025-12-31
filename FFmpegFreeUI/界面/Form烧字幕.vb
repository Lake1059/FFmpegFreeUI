Imports Microsoft.WindowsAPICodePack.Dialogs
Imports Sunny.UI
Public Class Form烧字幕
    Private Sub Form烧字幕_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AddHandler UiCheckBox字幕来源是外部文件.Click, Sub()
                                                  If UiCheckBox字幕来源是外部文件.Checked Then
                                                      UiCheckBox字幕文件是内嵌的流.Checked = False
                                                      UiTextBox指定内嵌流.Text = ""
                                                  Else
                                                      UiTextBox字幕来源外部文件名.Text = ""
                                                      UiTextBox字幕来源指定文件夹.Text = ""
                                                  End If
                                              End Sub
        AddHandler UiCheckBox字幕文件是内嵌的流.Click, Sub()
                                                  If UiCheckBox字幕文件是内嵌的流.Checked Then
                                                      UiCheckBox字幕来源是外部文件.Checked = False
                                                      UiTextBox字幕来源外部文件名.Text = ""
                                                      UiTextBox字幕来源指定文件夹.Text = ""
                                                  Else
                                                      UiTextBox指定内嵌流.Text = ""
                                                  End If
                                              End Sub
        AddHandler UiButton选择外部字幕文件位置.Click, Sub()
                                                 Dim dialog As New CommonOpenFileDialog With {.IsFolderPicker = True}
                                                 If dialog.ShowDialog() = CommonFileDialogResult.Ok Then
                                                     UiTextBox字幕来源指定文件夹.Text = dialog.FileName
                                                 End If
                                             End Sub
        AddHandler UiButton选择字体文件夹.Click, Sub()
                                              Dim dialog As New CommonOpenFileDialog With {.IsFolderPicker = True}
                                              If dialog.ShowDialog() = CommonFileDialogResult.Ok Then
                                                  UiTextBox字体文件夹.Text = dialog.FileName
                                              End If
                                          End Sub
        AddHandler Label字体样式预览.FontChanged, AddressOf 更新字体基础样式
        AddHandler UiButton清除基本样式.Click, Sub()
                                             Label字体样式预览.Enabled = False
                                             Label字体样式预览.Font = SystemFonts.DefaultFont
                                             UiButton选择基本样式.Text = "选择基本样式"
                                         End Sub
        AddHandler UiButton选择基本样式.Click, Sub()
                                             Dim a As New FontDialog With {.Font = Label字体样式预览.Font, .ShowColor = False}
                                             If a.ShowDialog() = DialogResult.OK Then
                                                 Label字体样式预览.Enabled = True
                                                 Label字体样式预览.Font = a.Font
                                             End If
                                         End Sub
        AddHandler Label主要颜色.BackColorChanged, AddressOf 更新主要颜色显示
        AddHandler UiTextBox设置主要颜色透明度.TextChanged, AddressOf 更新主要颜色显示
        AddHandler UiButton清除主要颜色.Click, Sub() Label主要颜色.BackColor = Color.Transparent
        AddHandler UiButton选择主要颜色.Click, Sub()
                                             Dim a As New ColorDialog With {.Color = Label主要颜色.BackColor, .FullOpen = True, .AnyColor = True}
                                             If a.ShowDialog() = DialogResult.OK Then Label主要颜色.BackColor = a.Color
                                         End Sub
        AddHandler Label次要颜色.BackColorChanged, AddressOf 更新次要颜色显示
        AddHandler UiTextBox设置次要颜色透明度.TextChanged, AddressOf 更新次要颜色显示
        AddHandler UiButton清除次要颜色.Click, Sub() Label次要颜色.BackColor = Color.Transparent
        AddHandler UiButton选择次要颜色.Click, Sub()
                                             Dim a As New ColorDialog With {.Color = Label次要颜色.BackColor, .FullOpen = True, .AnyColor = True}
                                             If a.ShowDialog() = DialogResult.OK Then Label次要颜色.BackColor = a.Color
                                         End Sub
        AddHandler Label描边颜色.BackColorChanged, AddressOf 更新描边颜色显示
        AddHandler UiTextBox设置描边颜色透明度.TextChanged, AddressOf 更新描边颜色显示
        AddHandler UiButton清除描边颜色.Click, Sub() Label描边颜色.BackColor = Color.Transparent
        AddHandler UiButton选择描边颜色.Click, Sub()
                                             Dim a As New ColorDialog With {.Color = Label描边颜色.BackColor, .FullOpen = True, .AnyColor = True}
                                             If a.ShowDialog() = DialogResult.OK Then Label描边颜色.BackColor = a.Color
                                         End Sub
        AddHandler Label阴影背景颜色.BackColorChanged, AddressOf 更新阴影背景颜色显示
        AddHandler UiTextBox设置阴影背景颜色透明度.TextChanged, AddressOf 更新阴影背景颜色显示
        AddHandler UiButton清除阴影背景颜色.Click, Sub() Label阴影背景颜色.BackColor = Color.Transparent
        AddHandler UiButton选择阴影背景颜色.Click, Sub()
                                               Dim a As New ColorDialog With {.Color = Label阴影背景颜色.BackColor, .FullOpen = True, .AnyColor = True}
                                               If a.ShowDialog() = DialogResult.OK Then Label阴影背景颜色.BackColor = a.Color
                                           End Sub

        绑定下拉框鼠标滚轮事件(UiComboBox选择滤镜)
        绑定下拉框鼠标滚轮事件(UiComboBox优先选择)
        绑定下拉框鼠标滚轮事件(UiComboBox然后选择)
        绑定下拉框鼠标滚轮事件(UiComboBox最后选择)
        绑定下拉框鼠标滚轮事件(UiComboBox边框类型)
        绑定下拉框鼠标滚轮事件(UiComboBox对齐方位)

        重置所有选项()
    End Sub

    Private Sub Form烧字幕_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        UiTabControlMenu1.ItemSize = New Size(175 * Form1.DPI, 40 * Form1.DPI)
        Panel顶部视觉修正区域_一级选项卡.Width = UiTabControlMenu1.ItemSize.Width + 1
        校准UiComboBox视觉(UiComboBox选择滤镜)
        校准UiComboBox视觉(UiComboBox优先选择)
        校准UiComboBox视觉(UiComboBox然后选择)
        校准UiComboBox视觉(UiComboBox最后选择)
        UiCheckBox字幕来源是外部文件.CheckBoxSize = 20 * Form1.DPI
        UiCheckBox字幕文件是内嵌的流.CheckBoxSize = 20 * Form1.DPI
        校准UiComboBox视觉(UiComboBox边框类型)
        校准UiComboBox视觉(UiComboBox对齐方位)

    End Sub
    Private Sub Form烧字幕_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If UiComboBox选择滤镜.SelectedIndex = 0 Then
            重置所有选项()
        End If
        e.Cancel = True
        Me.Hide()
        Form1.Focus()
    End Sub

    Sub 调整界面()

    End Sub

    Sub 重置所有选项()
        UiComboBox选择滤镜.SelectedIndex = 0
        UiComboBox优先选择.Text = ""
        UiComboBox然后选择.Text = ""
        UiComboBox最后选择.Text = ""
        UiCheckBox字幕来源是外部文件.Checked = False
        UiTextBox字幕来源外部文件名.Text = ""
        UiTextBox字幕来源指定文件夹.Text = ""
        UiCheckBox字幕文件是内嵌的流.Checked = False
        UiTextBox指定内嵌流.Text = ""
        UiTextBox字体文件夹.Text = ""
        Label字体样式预览.Enabled = False
        Label字体样式预览.Font = SystemFonts.DefaultFont
        UiComboBox边框类型.Text = ""
        UiTextBox描边宽度.Text = ""
        UiTextBox阴影距离.Text = ""
        UiTextBox设置主要颜色透明度.Text = ""
        Label主要颜色.BackColor = Color.Transparent
        Label主要颜色值.Text = ""
        UiTextBox设置次要颜色透明度.Text = ""
        Label次要颜色.BackColor = Color.Transparent
        Label次要颜色值.Text = ""
        UiTextBox设置描边颜色透明度.Text = ""
        Label描边颜色.BackColor = Color.Transparent
        Label描边颜色值.Text = ""
        UiTextBox设置阴影背景颜色透明度.Text = ""
        Label阴影背景颜色.BackColor = Color.Transparent
        Label阴影背景颜色值.Text = ""
        UiComboBox对齐方位.Text = ""
        UiTextBox垂直边距.Text = ""
        UiTextBox左边距.Text = ""
        UiTextBox右边距.Text = ""
        UiTextBox字距.Text = ""
        UiTextBox行距.Text = ""
        UiTextBox视频分辨率.Text = ""
        UiTextBox自定义样式.Text = ""
        UiTextBox自定义滤镜参数.Text = ""
    End Sub

    Sub 更新字体基础样式()
        If Not Label字体样式预览.Enabled Then Exit Sub
        UiButton选择基本样式.Text = $"{Label字体样式预览.Font.Name}, {Label字体样式预览.Font.Size}, {Label字体样式预览.Font.Style}"
    End Sub
    Sub 更新主要颜色显示()
        If Label主要颜色.BackColor = Color.Transparent Then
            Label主要颜色.BorderStyle = BorderStyle.FixedSingle
            Label主要颜色值.Text = "未设定"
        Else
            Label主要颜色.BorderStyle = BorderStyle.None
            Label主要颜色值.Text = 转换HTML颜色到ffmpeg接受的格式(Label主要颜色.BackColor.ToHTML, UiTextBox设置主要颜色透明度.Text)
            Label主要颜色值.Text.Replace("&", "&&")
        End If
    End Sub
    Sub 更新次要颜色显示()
        If Label次要颜色.BackColor = Color.Transparent Then
            Label次要颜色.BorderStyle = BorderStyle.FixedSingle
            Label次要颜色值.Text = "未设定"
        Else
            Label次要颜色.BorderStyle = BorderStyle.None
            Label次要颜色值.Text = 转换HTML颜色到ffmpeg接受的格式(Label次要颜色.BackColor.ToHTML, UiTextBox设置次要颜色透明度.Text)
            Label次要颜色值.Text.Replace("&", "&&")
        End If
    End Sub
    Sub 更新描边颜色显示()
        If Label描边颜色.BackColor = Color.Transparent Then
            Label描边颜色.BorderStyle = BorderStyle.FixedSingle
            Label描边颜色值.Text = "未设定"
        Else
            Label描边颜色.BorderStyle = BorderStyle.None
            Label描边颜色值.Text = 转换HTML颜色到ffmpeg接受的格式(Label描边颜色.BackColor.ToHTML, UiTextBox设置描边颜色透明度.Text)
            Label描边颜色值.Text.Replace("&", "&&")
        End If
    End Sub
    Sub 更新阴影背景颜色显示()
        If Label阴影背景颜色.BackColor = Color.Transparent Then
            Label阴影背景颜色.BorderStyle = BorderStyle.FixedSingle
            Label阴影背景颜色值.Text = "未设定"
        Else
            Label阴影背景颜色.BorderStyle = BorderStyle.None
            Label阴影背景颜色值.Text = 转换HTML颜色到ffmpeg接受的格式(Label阴影背景颜色.BackColor.ToHTML, UiTextBox设置阴影背景颜色透明度.Text)
            Label阴影背景颜色值.Text.Replace("&", "&&")
        End If
    End Sub

End Class