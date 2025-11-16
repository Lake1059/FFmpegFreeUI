Imports Microsoft.WindowsAPICodePack.Dialogs

Public Class 界面_设置
    Private Sub 界面_设置_Load(sender As Object, e As EventArgs) Handles Me.Load
        UiTabControlMenu1.ItemSize = New Size(200 * Form1.DPI, 40 * Form1.DPI)
        AddHandler UiButton前往afdian购买.Click, Sub() Process.Start(New ProcessStartInfo With {.FileName = "https://afdian.com/item/a98d04e8b98011f0a49952540025c377", .UseShellExecute = True})
        AddHandler UiButton设置软件图标.Click, Sub()
                                             Dim a As New OpenFileDialog With {.Filter = "图片|*.png;*.jpg;*.jpeg;*.bmp", .Multiselect = False}
                                             If a.ShowDialog() = DialogResult.OK Then
                                                 Label9.Text = a.FileName
                                                 用户设置.实例对象.个性化_软件图标 = a.FileName
                                             End If
                                         End Sub
        AddHandler UiButton设置任务完成音效.Click, Sub()
                                               Dim a As New OpenFileDialog With {.Filter = "WAV|*.wav", .Multiselect = False}
                                               If a.ShowDialog() = DialogResult.OK Then
                                                   Label11.Text = a.FileName
                                                   用户设置.实例对象.个性化_任务完成音效 = a.FileName
                                               End If
                                           End Sub
        AddHandler UiButton设置任务失败音效.Click, Sub()
                                               Dim a As New OpenFileDialog With {.Filter = "WAV|*.wav", .Multiselect = False}
                                               If a.ShowDialog() = DialogResult.OK Then
                                                   Label10.Text = a.FileName
                                                   用户设置.实例对象.个性化_任务失败音效 = a.FileName
                                               End If
                                           End Sub
        AddHandler UiButton设置起始页标题.Click, Sub() 用户设置.实例对象.个性化_起始页标题 = UiTextBox1.Text
        AddHandler UiButton设置起始页副标题.Click, Sub() 用户设置.实例对象.个性化_起始页副标题 = UiTextBox2.Text
        AddHandler UiButton设置窗口标题栏.Click, Sub() 用户设置.实例对象.个性化_窗口标题栏 = UiTextBox3.Text
        AddHandler UiButton起始页背景图.Click, Sub()
                                             Dim a As New OpenFileDialog With {.Filter = "图片|*.png;*.jpg;*.jpeg;*.bmp", .Multiselect = False}
                                             If a.ShowDialog() = DialogResult.OK Then
                                                 Label14.Text = a.FileName
                                                 用户设置.实例对象.个性化_起始页背景图 = a.FileName
                                             End If
                                         End Sub
        AddHandler UiSwitch参与用户统计.Click, Sub() 用户设置.实例对象.是否参与用户统计 = Form1.设置页面.UiSwitch参与用户统计.Active

        调整界面()
    End Sub

    Sub 调整界面()
        校准UiComboBox视觉(UiComboBox字体名称)
        校准UiComboBox视觉(UiComboBox自动开始最大任务数量)
        校准UiComboBox视觉(UiComboBox有任务时系统状态)
        校准UiComboBox视觉(UiComboBox提示音)
        校准UiComboBox视觉(UiComboBox自动开始任务)
        校准UiComboBox视觉(UiComboBox自动重置参数面板的页面选择)
        校准UiComboBox视觉(UiComboBox混淆任务名称)
        校准UiComboBox视觉(UiComboBox自动开始最大任务数量)
        UiCheckBox转译模式.CheckBoxSize = 20 * Form1.DPI
    End Sub

    Public Sub 初始化设置操作响应()
        Dim 字体列表 As New List(Of String)
        For Each 字体 As FontFamily In FontFamily.Families
            字体列表.Add(字体.Name)
        Next
        字体列表.Sort()
        UiComboBox字体名称.Font = New Font(SystemFonts.DefaultFont.FontFamily.Name, 9.75)
        UiComboBox字体名称.Items.AddRange(字体列表.ToArray)
        AddHandler UiButton4.Click, Sub()
                                        If UiComboBox字体名称.Text = "" Then Exit Sub
                                        SetControlFont(UiComboBox字体名称.Text, Form1, {UiComboBox字体名称})
                                        用户设置.实例对象.字体 = UiComboBox字体名称.Text
                                        编码队列右键菜单.重设字体()
                                        编码队列管理选项.重设字体()
                                    End Sub
        AddHandler UiComboBox字体名称.TextChanged, Sub()
                                                   If UiComboBox字体名称.Text = "" Then Exit Sub
                                                   If Not UiComboBox字体名称.Items.Contains(UiComboBox字体名称.Text) Then Exit Sub
                                                   Label字体预览文本.Font = New Font(UiComboBox字体名称.Text, Label字体预览文本.Font.Size)
                                               End Sub
        AddHandler UiTextBox处理器核心.TextChanged, Sub() 用户设置.实例对象.指定处理器核心 = UiTextBox处理器核心.Text
        AddHandler UiTextBox快捷输入CPU核心.KeyPress, Sub(sender, e)
                                                    Select Case e.KeyChar
                                                        Case "0"c To "9"c, "~"c, ChrW(Keys.Back)
                                                        Case ChrW(Keys.Enter)
                                                            Dim input = UiTextBox快捷输入CPU核心.Text.Trim
                                                            Dim result As New List(Of Integer)
                                                            Try
                                                                If input.Contains("~"c) Then
                                                                    Dim parts = input.Split("~"c)
                                                                    If parts.Length = 2 Then
                                                                        Dim startNum, endNum As Integer
                                                                        If Integer.TryParse(parts(0), startNum) AndAlso Integer.TryParse(parts(1), endNum) Then
                                                                            If startNum <= endNum Then
                                                                                For i = startNum To endNum
                                                                                    result.Add(i)
                                                                                Next
                                                                                UiTextBox处理器核心.Text = String.Join(",", result)
                                                                            End If
                                                                        End If
                                                                    End If
                                                                End If
                                                            Catch ex As Exception
                                                                MsgBox(ex.Message, MsgBoxStyle.Critical)
                                                            End Try
                                                            e.Handled = True
                                                        Case Else
                                                            Exit Sub
                                                    End Select
                                                End Sub
        AddHandler UiComboBox自动开始最大任务数量.SelectedIndexChanged, Sub()
                                                                  用户设置.实例对象.自动同时运行任务数量选项 = UiComboBox自动开始最大任务数量.SelectedIndex
                                                                  Select Case UiComboBox自动开始最大任务数量.SelectedIndex
                                                                      Case 0 : 同时运行任务上限 = 1
                                                                      Case 1 : 同时运行任务上限 = 2
                                                                      Case 2 : 同时运行任务上限 = 3
                                                                      Case 3 : 同时运行任务上限 = 4
                                                                      Case 4 : 同时运行任务上限 = 5
                                                                      Case 5 : 同时运行任务上限 = 6
                                                                      Case 6 : 同时运行任务上限 = 7
                                                                      Case 7 : 同时运行任务上限 = 8
                                                                      Case 8 : 同时运行任务上限 = 9
                                                                      Case 9 : 同时运行任务上限 = 10
                                                                      Case Else : 同时运行任务上限 = 1
                                                                  End Select
                                                              End Sub
        AddHandler UiComboBox有任务时系统状态.SelectedIndexChanged, Sub()
                                                                If UiComboBox有任务时系统状态.Text = "" Then Exit Sub
                                                                If UiComboBox有任务时系统状态.SelectedIndex < 0 Then Exit Sub
                                                                用户设置.实例对象.有任务时系统保持状态选项 = UiComboBox有任务时系统状态.SelectedIndex
                                                            End Sub
        AddHandler UiComboBox提示音.SelectedIndexChanged, Sub() 用户设置.实例对象.提示音选项 = UiComboBox提示音.SelectedIndex
        AddHandler UiComboBox自动开始任务.SelectedIndexChanged, Sub() 用户设置.实例对象.自动开始任务选项 = UiComboBox自动开始任务.SelectedIndex
        AddHandler UiComboBox自动重置参数面板的页面选择.SelectedIndexChanged, Sub() 用户设置.实例对象.自动重置参数面板的页面选择 = UiComboBox自动重置参数面板的页面选择.SelectedIndex
        AddHandler UiComboBox混淆任务名称.SelectedIndexChanged, Sub() 用户设置.实例对象.混淆任务名称 = UiComboBox混淆任务名称.SelectedIndex

        AddHandler UiTextBoxFFmpeg自定义工作目录.TextChanged, Sub() 用户设置.实例对象.工作目录 = UiTextBoxFFmpeg自定义工作目录.Text
        AddHandler UiButton13.Click, Sub()
                                         Dim dialog As New CommonOpenFileDialog With {.IsFolderPicker = True}
                                         If dialog.ShowDialog() = CommonFileDialogResult.Ok Then UiTextBoxFFmpeg自定义工作目录.Text = dialog.FileName
                                     End Sub
        AddHandler UiTextBox替代进程的文件名.TextChanged, Sub() 用户设置.实例对象.替代进程文件名 = UiTextBox替代进程的文件名.Text
        AddHandler UiTextBox覆盖参数传递.TextChanged, Sub() 用户设置.实例对象.覆盖参数传递 = UiTextBox覆盖参数传递.Text
        AddHandler UiCheckBox转译模式.Click, Sub() 用户设置.实例对象.转译模式 = UiCheckBox转译模式.Checked = True
    End Sub

    Private Sub 界面_设置_DpiChangedAfterParent(sender As Object, e As EventArgs) Handles Me.DpiChangedAfterParent
        UiTabControlMenu1.ItemSize = New Size(200 * Form1.DPI, 40 * Form1.DPI)
        调整界面()
    End Sub
End Class
