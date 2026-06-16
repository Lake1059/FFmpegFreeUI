Imports System.IO
Imports LakeUI

Public Class Form_v6_参数面板_预设管理

    Public 所属参数面板对象 As Form_v6_参数面板
    Private ReadOnly _预设列表项 As New List(Of 预设列表项)

    Private Class 预设列表项
        Public Property 名称 As String = ""
        Public Property 文件路径 As String = ""
        Public Property 数据 As 预设数据_v6
        Public Property 是内置 As Boolean = False

        Public ReadOnly Property 标识 As String
            Get
                If 是内置 Then Return "内置:" & 名称
                Return IO.Path.GetFullPath(文件路径)
            End Get
        End Property
    End Class

    Private Sub Form_v6_参数面板_预设管理_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ModernTextBox1.Text = ""
        ModernTextBox2.Text = ""
        ModernTextBox3.Text = ""
        ModernTextBox4.Text = ""
        If ModernComboBox1.SelectedIndex < 0 Then ModernComboBox1.SelectedIndex = 1
        刷新预设列表()
    End Sub

    Private Sub Form_v6_参数面板_预设管理_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged
        Me.ModernListBox1.Width = (Me.Panel2.Width - Me.JustEmptyControl1.Width * 2) / 3
        Me.ModernTextBox1.Width = Me.ModernListBox1.Width
    End Sub

    Public Sub 刷新预设列表()
        Directory.CreateDirectory(预设管理_v6.获取预设目录("用户自定义"))
        Directory.CreateDirectory(预设管理_v6.获取预设目录("从社区下载"))

        Dim 旧选中标识 = 当前选中列表项()?.标识
        ModernListBox1.Items.Clear()
        ModernListBox1.ItemToolTips.Clear()
        _预设列表项.Clear()

        If 当前是内置来源() Then
            For Each preset In 开发者内置预设_v6.获取全部().OrderBy(Function(x) x.名称, StringComparer.CurrentCultureIgnoreCase)
                添加预设列表项(New 预设列表项 With {
                    .名称 = preset.名称,
                    .数据 = 开发者内置预设_v6.克隆预设数据(preset.数据),
                    .是内置 = True
                })
            Next
        Else
            Dim dir = 当前来源目录()
            If Not Directory.Exists(dir) Then Directory.CreateDirectory(dir)
            For Each file In Directory.EnumerateFiles(dir, "*.json", SearchOption.TopDirectoryOnly).OrderBy(Function(x) Path.GetFileNameWithoutExtension(x), StringComparer.CurrentCultureIgnoreCase)
                Dim displayName = Path.GetFileNameWithoutExtension(file)
                Dim remark = ""
                Try
                    Dim data = 预设管理_v6.读取预设文件(file)
                    If data IsNot Nothing Then
                        remark = data.预设备注
                    End If
                Catch
                    remark = "此文件无法按 v6 预设读取"
                End Try
                添加预设列表项(New 预设列表项 With {
                    .名称 = displayName,
                    .文件路径 = file
                }, remark)
            Next
        End If

        Dim index = -1
        If 旧选中标识 <> "" Then index = _预设列表项.FindIndex(Function(x) String.Equals(x.标识, 旧选中标识, StringComparison.OrdinalIgnoreCase))
        ModernListBox1.SelectedIndex = index
        If index < 0 Then 清空预设预览()
        更新操作可用状态()
    End Sub

    Private Sub 添加预设列表项(item As 预设列表项, Optional remark As String = "")
        If item Is Nothing OrElse item.名称 = "" Then Exit Sub
        _预设列表项.Add(item)
        ModernListBox1.Items.Add(item.名称)
        If item.是内置 AndAlso item.数据 IsNot Nothing Then remark = item.数据.预设备注
        If remark <> "" Then ModernListBox1.ItemToolTips.Add(New LakeUI.ModernListBox.ToolTipEntry(item.名称, remark))
    End Sub

    Private Function 当前来源名称() As String
        Dim source = ModernComboBox1.Text
        If source = "" AndAlso ModernComboBox1.SelectedIndex >= 0 Then source = ModernComboBox1.Items(ModernComboBox1.SelectedIndex)
        If source = "" Then source = "用户自定义"
        Return source
    End Function

    Private Function 当前是内置来源() As Boolean
        Return String.Equals(当前来源名称(), "开发者内置", StringComparison.Ordinal)
    End Function

    Private Function 当前来源目录() As String
        Return 预设管理_v6.获取预设目录(当前来源名称())
    End Function

    Private Function 当前选中列表项() As 预设列表项
        If ModernListBox1.SelectedIndex < 0 OrElse ModernListBox1.SelectedIndex >= _预设列表项.Count Then Return Nothing
        Return _预设列表项(ModernListBox1.SelectedIndex)
    End Function

    Private Function 当前选中文件路径() As String
        Dim item = 当前选中列表项()
        If item Is Nothing OrElse item.是内置 Then Return ""
        Return item.文件路径
    End Function

    Private Function 读取当前选中预设数据() As 预设数据_v6
        Dim item = 当前选中列表项()
        If item Is Nothing Then Return Nothing
        If item.是内置 Then Return 开发者内置预设_v6.克隆预设数据(item.数据)
        Return 预设管理_v6.读取预设文件(item.文件路径)
    End Function

    Private Sub 更新操作可用状态()
        Dim isBuiltIn = 当前是内置来源()
        ModernButton1.Enabled = Not isBuiltIn
        ModernButton3.Enabled = Not isBuiltIn
        ModernButton4.Enabled = Not isBuiltIn
        ModernButton6.Enabled = Not isBuiltIn
        ModernButton7.Enabled = Not isBuiltIn
        ModernTextBox3.ReadOnly = isBuiltIn
        ModernTextBox4.ReadOnly = isBuiltIn
        ModernCheckBox1.Enabled = Not isBuiltIn
        ModernListBox1.AllowDragReorder = Not isBuiltIn
    End Sub

    Private Sub 清空预设预览()
        ModernTextBox1.Text = ""
        ModernTextBox2.Text = ""
        ModernTextBox3.Text = ""
        ModernTextBox4.Text = ""
    End Sub

    Private Sub 预览选中预设()
        Dim item = 当前选中列表项()
        If item Is Nothing Then
            清空预设预览()
            Exit Sub
        End If
        Try
            Dim data = 读取当前选中预设数据()
            If data Is Nothing Then
                清空预设预览()
                Exit Sub
            End If
            ModernTextBox3.Text = item.名称
            ModernTextBox4.Text = data.预设备注
            ModernCheckBox1.Checked = data.额外保存输出位置
            预设管理_v6.显示参数总览(ModernTextBox1, data)
            ModernTextBox2.Text = 预设管理_v6.生成命令行展示文本(data, 预设管理_v6.输入占位符, 预设管理_v6.输出占位符)
        Catch ex As Exception
            ModernTextBox1.Text = "读取失败：" & ex.Message
            ModernTextBox2.Text = ""
        End Try
    End Sub

    Private Function 从面板创建带元数据的预设() As 预设数据_v6
        Dim data = 预设管理_v6.从面板创建预设(所属参数面板对象)
        data.预设备注 = ModernTextBox4.Text.Trim()
        data.额外保存输出位置 = ModernCheckBox1.Checked
        Return data
    End Function

    Private Function 用户预设目录() As String
        Dim userDir = 预设管理_v6.获取预设目录("用户自定义")
        Directory.CreateDirectory(userDir)
        Return userDir
    End Function

    Private Function 用户预设文件路径(预设名 As String) As String
        Dim name = If(预设名, "").Trim()
        If name.EndsWith(".json", StringComparison.OrdinalIgnoreCase) Then name = Path.GetFileNameWithoutExtension(name)
        Return Path.Combine(用户预设目录(), 预设管理_v6.安全文件名(name) & ".json")
    End Function

    Private Function 新建用户预设文件路径() As String
        Dim baseName = 预设管理_v6.新建预设名称()
        Dim file = 用户预设文件路径(baseName)
        Dim index = 2
        While System.IO.File.Exists(file)
            file = 用户预设文件路径($"{baseName}-{index}")
            index += 1
        End While
        Return file
    End Function

    Private Sub 选中用户预设文件(file As String)
        If ModernComboBox1.SelectedIndex <> 1 Then
            ModernComboBox1.SelectedIndex = 1
        Else
            刷新预设列表()
        End If

        Dim idx = _预设列表项.FindIndex(Function(x) Not x.是内置 AndAlso String.Equals(x.文件路径, file, StringComparison.OrdinalIgnoreCase))
        If idx >= 0 Then ModernListBox1.SelectedIndex = idx
    End Sub

    Private Sub 保存到用户预设()
        If 所属参数面板对象 Is Nothing Then Exit Sub
        If 当前是内置来源() Then
            ExFloatingTip("内置预设只能读取", 1800)
            Exit Sub
        End If
        Try
            Dim selectedItem = 当前选中列表项()
            Dim file As String

            If selectedItem Is Nothing Then
                file = 新建用户预设文件路径()
            Else
                Dim presetName = selectedItem.名称
                file = 用户预设文件路径(presetName)
                Dim 已存在 = System.IO.File.Exists(file)
                Dim prompt = If(已存在,
                                $"确认覆盖用户预设：{presetName}？",
                                $"确认按当前选中项名称保存为用户预设：{presetName}？")
                Dim buttons = If(已存在, {"覆盖", "取消"}, {"保存", "取消"})
                Dim result = ExFloatingBox(ModernButton1, prompt, buttons, If(已存在, "确认覆盖", "确认保存"), MsgBoxStyle.Question, 1)
                If result <> 0 Then Exit Sub
            End If

            Dim data = 从面板创建带元数据的预设()
            预设管理_v6.写入预设文件(file, data)
            选中用户预设文件(file)
            ModernTextBox3.Text = Path.GetFileNameWithoutExtension(file)
            ExFloatingTip("已保存预设：" & Path.GetFileNameWithoutExtension(file), 1800)
        Catch ex As Exception
            ExFloatingTip("保存失败：" & ex.Message, 2500)
        End Try
    End Sub

    Private Sub 加载选中预设()
        If 所属参数面板对象 Is Nothing Then Exit Sub
        Dim item = 当前选中列表项()
        If item Is Nothing Then Exit Sub
        Try
            Dim data = 读取当前选中预设数据()
            If data Is Nothing Then Exit Sub
            预设管理_v6.显示预设(data, 所属参数面板对象)
            预览选中预设()
            ExFloatingTip("已读取预设：" & item.名称, 1800)
        Catch ex As Exception
            ExFloatingTip("读取失败：" & ex.Message, 2500)
        End Try
    End Sub

    Private Sub 导出选中预设()
        If 当前是内置来源() Then
            ExFloatingTip("内置预设只能读取", 1800)
            Exit Sub
        End If
        Dim file = 当前选中文件路径()
        If file = "" Then Exit Sub
        Dim suggested = If(ModernTextBox3.Text.Trim() <> "", ModernTextBox3.Text.Trim(), Path.GetFileNameWithoutExtension(file))
        Using d As New SaveFileDialog With {.Filter = "JSON 预设|*.json", .FileName = 预设管理_v6.安全文件名(suggested) & ".json"}
            If d.ShowDialog(Me) <> DialogResult.OK OrElse d.FileName = "" Then Exit Sub
            System.IO.File.Copy(file, d.FileName, True)
            ExFloatingTip(FormMain_v6, "已导出预设", 1800)
        End Using
    End Sub

    Private Sub 导入预设()
        If 当前是内置来源() Then
            ExFloatingTip("内置预设只能读取", 1800)
            Exit Sub
        End If
        Using d As New OpenFileDialog With {.Filter = "JSON 预设|*.json", .Multiselect = True}
            If d.ShowDialog(Me) <> DialogResult.OK Then Exit Sub
            Dim userDir = 预设管理_v6.获取预设目录("用户自定义")
            Directory.CreateDirectory(userDir)
            For Each src In d.FileNames
                Dim data = 预设管理_v6.读取预设文件(src)
                If data Is Nothing Then Continue For
                Dim dest = Path.Combine(userDir, 预设管理_v6.安全文件名(Path.GetFileNameWithoutExtension(src)) & ".json")
                预设管理_v6.写入预设文件(dest, data)
            Next
            ModernComboBox1.SelectedIndex = 1
            刷新预设列表()
            ExFloatingTip(FormMain_v6, "已导入预设", 1800)
        End Using
    End Sub

    Private Sub 变更名称()
        If 当前是内置来源() Then
            ExFloatingTip("内置预设只能读取", 1800)
            Exit Sub
        End If
        Dim file = 当前选中文件路径()
        Dim newName = ModernTextBox3.Text.Trim()
        If file = "" OrElse newName = "" Then Exit Sub
        Try
            Dim data = 预设管理_v6.读取预设文件(file)
            If data Is Nothing Then Exit Sub
            Dim userDir = 预设管理_v6.获取预设目录("用户自定义")
            Directory.CreateDirectory(userDir)
            Dim dest = Path.Combine(userDir, 预设管理_v6.安全文件名(newName) & ".json")
            预设管理_v6.写入预设文件(dest, data)
            If String.Equals(Path.GetDirectoryName(file), userDir, StringComparison.OrdinalIgnoreCase) AndAlso Not String.Equals(file, dest, StringComparison.OrdinalIgnoreCase) Then
                Try
                    System.IO.File.Delete(file)
                Catch
                End Try
            End If
            ModernComboBox1.SelectedIndex = 1
            刷新预设列表()
            Dim idx = _预设列表项.FindIndex(Function(x) Not x.是内置 AndAlso String.Equals(x.文件路径, dest, StringComparison.OrdinalIgnoreCase))
            If idx >= 0 Then ModernListBox1.SelectedIndex = idx
        Catch ex As Exception
            ExFloatingTip("重命名失败：" & ex.Message, 2500)
        End Try
    End Sub

    Private Sub 变更备注()
        If 当前是内置来源() Then
            ExFloatingTip("内置预设只能读取", 1800)
            Exit Sub
        End If
        Dim file = 当前选中文件路径()
        If file = "" Then Exit Sub
        Try
            Dim data = 预设管理_v6.读取预设文件(file)
            If data Is Nothing Then Exit Sub
            data.预设备注 = ModernTextBox4.Text.Trim()
            Dim userDir = 预设管理_v6.获取预设目录("用户自定义")
            Dim dest = file
            If Not String.Equals(Path.GetDirectoryName(file), userDir, StringComparison.OrdinalIgnoreCase) Then
                Directory.CreateDirectory(userDir)
                dest = Path.Combine(userDir, 预设管理_v6.安全文件名(Path.GetFileNameWithoutExtension(file)) & ".json")
            End If
            预设管理_v6.写入预设文件(dest, data)
            ModernComboBox1.SelectedIndex = 1
            刷新预设列表()
        Catch ex As Exception
            ExFloatingTip("更新备注失败：" & ex.Message, 2500)
        End Try
    End Sub

    Private Sub 删除选中预设到回收站()
        Dim item = 当前选中列表项()
        If item Is Nothing Then Exit Sub
        If item.是内置 Then
            ExFloatingTip("内置预设不能删除", 1800)
            Exit Sub
        End If
        If item.文件路径 = "" OrElse Not System.IO.File.Exists(item.文件路径) Then Exit Sub

        Dim oldIndex = ModernListBox1.SelectedIndex
        Try
            FileIO.FileSystem.DeleteFile(item.文件路径, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.SendToRecycleBin)
            刷新预设列表()
            If _预设列表项.Count > 0 Then ModernListBox1.SelectedIndex = Math.Min(oldIndex, _预设列表项.Count - 1)
            ExFloatingTip("已删除到回收站：" & item.名称, 1800)
        Catch ex As Exception
            ExFloatingTip("删除失败：" & ex.Message, 2500)
        End Try
    End Sub

    Private Sub ModernComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ModernComboBox1.SelectedIndexChanged
        刷新预设列表()
    End Sub

    Private Sub ModernListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ModernListBox1.SelectedIndexChanged
        预览选中预设()
    End Sub

    Private Sub ModernListBox1_ItemDoubleClick(sender As Object, e As LakeUI.ModernListBox.ItemEventArgs) Handles ModernListBox1.ItemDoubleClick
        加载选中预设()
    End Sub

    Private Sub ModernListBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles ModernListBox1.KeyDown
        If e.KeyCode <> Keys.Delete Then Exit Sub
        删除选中预设到回收站()
        e.Handled = True
        e.SuppressKeyPress = True
    End Sub

    Private Sub ModernButton1_Click(sender As Object, e As EventArgs) Handles ModernButton1.Click
        保存到用户预设()
    End Sub

    Private Sub ModernButton2_Click(sender As Object, e As EventArgs) Handles ModernButton2.Click
        加载选中预设()
    End Sub

    Private Sub ModernButton3_Click(sender As Object, e As EventArgs) Handles ModernButton3.Click
        导出选中预设()
    End Sub

    Private Sub ModernButton4_Click(sender As Object, e As EventArgs) Handles ModernButton4.Click
        导入预设()
    End Sub

    Private Sub ModernButton5_Click(sender As Object, e As EventArgs) Handles ModernButton5.Click
        If 所属参数面板对象 Is Nothing Then Exit Sub
        预设管理_v6.重置面板(所属参数面板对象)
        ExFloatingTip("已重置参数面板", 1800)
    End Sub

    Private Sub ModernButton6_Click(sender As Object, e As EventArgs) Handles ModernButton6.Click
        变更名称()
    End Sub

    Private Sub ModernButton7_Click(sender As Object, e As EventArgs) Handles ModernButton7.Click
        变更备注()
    End Sub

End Class
