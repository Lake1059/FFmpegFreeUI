Imports System.IO
Imports System.Text.Json
Imports LakeUI

Public Class Form_v6_参数面板_预设管理

    Public 所属参数面板对象 As Form_v6_参数面板
    Private Const 预设排序文件名 As String = ".preset-order.txt"
    Private ReadOnly _预设列表项 As New List(Of 预设列表项)
    Private _正在刷新预设列表 As Boolean = False

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
        MTB_预设参数总览.Text = ""
        MTB_预设命令行预览.Text = ""
        MTB_预设名称.Text = ""
        MTB_预设备注.Text = ""
        If MCB_预设来源.SelectedIndex < 0 Then MCB_预设来源.SelectedIndex = 1
        刷新预设列表()
    End Sub

    Private Sub Form_v6_参数面板_预设管理_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged
        Me.MLB_预设列表.Width = (Me.Panel2.Width - Me.JustEmptyControl1.Width * 2) / 3
        Me.MTB_预设参数总览.Width = Me.MLB_预设列表.Width
    End Sub

    Public Sub 刷新预设列表()
        Directory.CreateDirectory(预设管理_v6.获取预设目录("用户自定义"))
        Directory.CreateDirectory(预设管理_v6.获取预设目录("从社区下载"))

        Dim 旧选中标识 = 当前选中列表项()?.标识
        _正在刷新预设列表 = True
        Try
            MLB_预设列表.Items.Clear()
            MLB_预设列表.ItemToolTips.Clear()
            _预设列表项.Clear()

            If 当前是内置来源() Then
                For Each preset In 开发者内置预设_v6.获取全部()
                    添加预设列表项(New 预设列表项 With {
                        .名称 = preset.名称,
                        .数据 = 开发者内置预设_v6.克隆预设数据(preset.数据),
                        .是内置 = True
                    })
                Next
            Else
                Dim dir = 当前来源目录()
                If Not Directory.Exists(dir) Then Directory.CreateDirectory(dir)
                Dim files = Directory.EnumerateFiles(dir, "*.json", SearchOption.TopDirectoryOnly).ToList()
                If 当前是用户自定义来源() Then
                    files = 应用用户预设排序(files)
                ElseIf 当前是社区下载来源() Then
                    files = 按文件修改时间排序(files)
                End If

                For Each file In files
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
        Finally
            _正在刷新预设列表 = False
        End Try

        Dim index = -1
        If 旧选中标识 <> "" Then index = _预设列表项.FindIndex(Function(x) String.Equals(x.标识, 旧选中标识, StringComparison.OrdinalIgnoreCase))
        MLB_预设列表.SelectedIndex = index
        If index < 0 Then 清空预设预览()
        更新操作可用状态()
    End Sub

    Private Sub 添加预设列表项(item As 预设列表项, Optional remark As String = "")
        If item Is Nothing OrElse item.名称 = "" Then Exit Sub
        _预设列表项.Add(item)
        MLB_预设列表.Items.Add(item.名称)
        If item.是内置 AndAlso item.数据 IsNot Nothing Then remark = item.数据.预设备注
        If remark <> "" Then MLB_预设列表.ItemToolTips.Add(New LakeUI.ModernListBox.ToolTipEntry(item.名称, remark))
    End Sub

    Private Function 用户预设排序文件路径() As String
        Return Path.Combine(用户预设目录(), 预设排序文件名)
    End Function

    Private Function 读取用户预设排序文件名() As List(Of String)
        Dim result As New List(Of String)
        Dim file = 用户预设排序文件路径()
        If Not System.IO.File.Exists(file) Then Return result

        Try
            Dim lines = System.IO.File.ReadAllLines(file, System.Text.Encoding.UTF8)
            For i = 0 To lines.Length - 1
                Dim name = Path.GetFileName(If(lines(i), "").Trim())
                If name <> "" AndAlso Not result.Any(Function(x) String.Equals(x, name, StringComparison.OrdinalIgnoreCase)) Then result.Add(name)
            Next
        Catch
        End Try

        Return result
    End Function

    Private Sub 写入用户预设排序(files As IEnumerable(Of String))
        Dim dir = 用户预设目录()
        Dim names = files.
            Where(Function(x) x <> "").
            Select(Function(x) Path.GetFileName(x)).
            Where(Function(x) x <> "").
            Distinct(StringComparer.OrdinalIgnoreCase).
            ToList()
        System.IO.File.WriteAllLines(Path.Combine(dir, 预设排序文件名), names, System.Text.Encoding.UTF8)
    End Sub

    Private Function 应用用户预设排序(files As List(Of String)) As List(Of String)
        Dim order = 读取用户预设排序文件名()
        Dim remaining = files.ToList()
        Dim result As New List(Of String)

        For Each orderedName In order
            Dim index = remaining.FindIndex(Function(x) String.Equals(Path.GetFileName(x), orderedName, StringComparison.OrdinalIgnoreCase))
            If index < 0 Then Continue For
            result.Add(remaining(index))
            remaining.RemoveAt(index)
        Next

        result.AddRange(remaining)
        写入用户预设排序(result)
        Return result
    End Function

    Private Function 按文件修改时间排序(files As List(Of String)) As List(Of String)
        Return files.
            OrderBy(Function(x) System.IO.File.GetLastWriteTimeUtc(x)).
            ThenBy(Function(x) Path.GetFileNameWithoutExtension(x), StringComparer.CurrentCultureIgnoreCase).
            ToList()
    End Function

    Private Sub 添加用户预设到排序末尾(file As String)
        If file = "" Then Exit Sub
        Dim files = Directory.EnumerateFiles(用户预设目录(), "*.json", SearchOption.TopDirectoryOnly).
            Where(Function(x) Not String.Equals(x, file, StringComparison.OrdinalIgnoreCase)).
            ToList()
        Dim ordered = 应用用户预设排序(files)
        ordered.Add(file)
        写入用户预设排序(ordered)
    End Sub

    Private Sub 替换用户预设排序项(oldFile As String, newFile As String)
        If newFile = "" Then Exit Sub

        Dim oldName = Path.GetFileName(oldFile)
        Dim newName = Path.GetFileName(newFile)
        Dim order = 读取用户预设排序文件名()
        Dim index = order.FindIndex(Function(x) String.Equals(x, oldName, StringComparison.OrdinalIgnoreCase))
        If index >= 0 Then
            order(index) = newName
            Dim normalized As New List(Of String)
            For Each orderedName In order
                If orderedName = "" Then Continue For
                If normalized.Any(Function(x) String.Equals(x, orderedName, StringComparison.OrdinalIgnoreCase)) Then Continue For
                normalized.Add(orderedName)
            Next
            写入用户预设排序(normalized)
        Else
            添加用户预设到排序末尾(newFile)
        End If
    End Sub

    Private Sub 保存当前预设排序()
        If Not 当前是用户自定义来源() Then Exit Sub

        Dim files = _预设列表项.
            Where(Function(x) x IsNot Nothing AndAlso Not x.是内置 AndAlso x.文件路径 <> "").
            Select(Function(x) x.文件路径).
            ToList()
        写入用户预设排序(files)
    End Sub

    Private Sub 同步拖动后的预设列表顺序()
        If _正在刷新预设列表 Then Exit Sub
        If Not 当前是用户自定义来源() Then Exit Sub
        If MLB_预设列表.Items.Count <> _预设列表项.Count Then Exit Sub

        Dim oldItems = _预设列表项.ToList()
        Dim reordered As New List(Of 预设列表项)
        For Each rawItem In MLB_预设列表.Items
            Dim name = If(rawItem, "").ToString()
            Dim index = oldItems.FindIndex(Function(x) String.Equals(x.名称, name, StringComparison.Ordinal))
            If index < 0 Then Continue For
            reordered.Add(oldItems(index))
            oldItems.RemoveAt(index)
        Next

        If reordered.Count <> _预设列表项.Count Then Exit Sub
        _预设列表项.Clear()
        _预设列表项.AddRange(reordered)
        保存当前预设排序()
        预览选中预设()
    End Sub

    Private Function 当前来源名称() As String
        Dim source = MCB_预设来源.Text
        If source = "" AndAlso MCB_预设来源.SelectedIndex >= 0 Then source = MCB_预设来源.Items(MCB_预设来源.SelectedIndex)
        If source = "" Then source = "用户自定义"
        Return source
    End Function

    Private Function 当前是内置来源() As Boolean
        Return String.Equals(当前来源名称(), "开发者内置", StringComparison.Ordinal)
    End Function

    Private Function 当前是用户自定义来源() As Boolean
        Return String.Equals(当前来源名称(), "用户自定义", StringComparison.Ordinal)
    End Function

    Private Function 当前是社区下载来源() As Boolean
        Return String.Equals(当前来源名称(), "从社区下载", StringComparison.Ordinal)
    End Function

    Public Function Agent列出预设(source As String) As List(Of Dictionary(Of String, Object))
        Agent选择来源(source)
        Return _预设列表项.Select(Function(item) New Dictionary(Of String, Object) From {
            {"name", item.名称},
            {"source", 当前来源名称()},
            {"built_in", item.是内置},
            {"path", If(item.是内置, "", item.文件路径)}
        }).ToList()
    End Function

    Public Function Agent读取预设(source As String, name As String) As Dictionary(Of String, Object)
        Dim item = Agent查找预设(source, name)
        If item Is Nothing Then Throw New InvalidOperationException("找不到预设：" & name)
        Dim data = If(item.是内置, 开发者内置预设_v6.克隆预设数据(item.数据), 预设管理_v6.读取预设文件(item.文件路径))
        If data Is Nothing Then Throw New InvalidOperationException("预设读取失败：" & name)
        Return New Dictionary(Of String, Object) From {
            {"name", item.名称},
            {"source", 当前来源名称()},
            {"built_in", item.是内置},
            {"path", If(item.是内置, "", item.文件路径)},
            {"note", data.预设备注},
            {"overview", Agent构建预设总览(data)},
            {"command", 预设管理_v6.生成命令行展示文本(data, 预设管理_v6.输入占位符, 预设管理_v6.输出占位符)},
            {"preset_json", JsonSerializer.Serialize(data, JsonSO)}
        }
    End Function

    Public Function Agent应用预设(source As String, name As String) As String
        If 所属参数面板对象 Is Nothing Then Return "参数面板不可用"
        Dim item = Agent查找预设(source, name)
        If item Is Nothing Then Return "找不到预设：" & name
        Dim data = If(item.是内置, 开发者内置预设_v6.克隆预设数据(item.数据), 预设管理_v6.读取预设文件(item.文件路径))
        If data Is Nothing Then Return "预设读取失败：" & name
        预设管理_v6.显示预设(data, 所属参数面板对象)
        Return "已读取预设：" & item.名称
    End Function

    Public Function Agent保存预设(source As String, name As String, presetJson As String, note As String, saveOutputLocation As Boolean?) As String
        Dim sourceName = Agent规范来源(source)
        If String.Equals(sourceName, "开发者内置", StringComparison.Ordinal) Then Return "开发者内置预设只允许读取，不能保存或删除"
        If name Is Nothing OrElse name.Trim() = "" Then Return "缺少预设名称"

        Dim data As 预设数据_v6
        If Not String.IsNullOrWhiteSpace(presetJson) Then
            data = JsonSerializer.Deserialize(Of 预设数据_v6)(presetJson, JsonSO)
        ElseIf 所属参数面板对象 IsNot Nothing Then
            data = 预设管理_v6.从面板创建预设(所属参数面板对象)
        Else
            Return "缺少 preset_json，且参数面板不可用"
        End If
        If note IsNot Nothing Then data.预设备注 = note

        Dim dir = 预设管理_v6.获取预设目录(sourceName)
        Directory.CreateDirectory(dir)
        Dim file = Path.Combine(dir, 预设管理_v6.安全文件名(name) & ".json")
        Dim isNewUserPreset = String.Equals(sourceName, "用户自定义", StringComparison.Ordinal) AndAlso Not System.IO.File.Exists(file)
        预设管理_v6.写入预设文件(file, data, If(saveOutputLocation.HasValue, saveOutputLocation.Value, MCK_额外保存输出位置.Checked))
        If isNewUserPreset Then 添加用户预设到排序末尾(file)
        Agent选择来源(sourceName)
        Return "已保存预设：" & Path.GetFileNameWithoutExtension(file)
    End Function

    Private Function Agent查找预设(source As String, name As String) As 预设列表项
        Agent选择来源(source)
        Dim key = If(name, "").Trim()
        If key = "" Then Return 当前选中列表项()
        Return _预设列表项.FirstOrDefault(Function(x) String.Equals(x.名称, key, StringComparison.OrdinalIgnoreCase) OrElse
                                                     String.Equals(Path.GetFileNameWithoutExtension(x.文件路径), key, StringComparison.OrdinalIgnoreCase))
    End Function

    Private Sub Agent选择来源(source As String)
        Dim sourceName = Agent规范来源(source)
        Dim index = MCB_预设来源.Items.IndexOf(sourceName)
        If index >= 0 AndAlso MCB_预设来源.SelectedIndex <> index Then
            MCB_预设来源.SelectedIndex = index
        Else
            刷新预设列表()
        End If
    End Sub

    Private Function Agent规范来源(source As String) As String
        Dim value = If(source, "").Trim()
        If value = "" Then Return 当前来源名称()
        Select Case value.ToLowerInvariant()
            Case "user", "用户", "用户自定义"
                Return "用户自定义"
            Case "community", "社区", "从社区下载"
                Return "从社区下载"
            Case "built_in", "builtin", "内置", "开发者内置"
                Return "开发者内置"
            Case Else
                Return value
        End Select
    End Function

    Private Function Agent构建预设总览(data As 预设数据_v6) As String
        Using box As New LakeUI.ModernTextBox
            预设管理_v6.显示参数总览(box, data)
            Return box.Text
        End Using
    End Function

    Private Function 当前来源目录() As String
        Return 预设管理_v6.获取预设目录(当前来源名称())
    End Function

    Private Function 当前选中列表项() As 预设列表项
        If MLB_预设列表.SelectedIndex < 0 OrElse MLB_预设列表.SelectedIndex >= _预设列表项.Count Then Return Nothing
        Return _预设列表项(MLB_预设列表.SelectedIndex)
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
        MB_保存预设.Enabled = Not isBuiltIn
        MB_导出预设.Enabled = Not isBuiltIn
        MB_导入预设.Enabled = Not isBuiltIn
        MB_变更预设名称.Enabled = Not isBuiltIn
        MB_变更预设备注.Enabled = Not isBuiltIn
        MTB_预设名称.ReadOnly = isBuiltIn
        MTB_预设备注.ReadOnly = isBuiltIn
        MCK_额外保存输出位置.Enabled = Not isBuiltIn
        MLB_预设列表.AllowDragReorder = 当前是用户自定义来源()
    End Sub

    Private Sub 清空预设预览()
        MTB_预设参数总览.Text = ""
        MTB_预设命令行预览.Text = ""
        MTB_预设名称.Text = ""
        MTB_预设备注.Text = ""
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
            MTB_预设名称.Text = item.名称
            MTB_预设备注.Text = data.预设备注
            预设管理_v6.显示参数总览(MTB_预设参数总览, data)
            MTB_预设命令行预览.Text = 预设管理_v6.生成命令行展示文本(data, 预设管理_v6.输入占位符, 预设管理_v6.输出占位符)
        Catch ex As Exception
            MTB_预设参数总览.Text = "读取失败：" & ex.Message
            MTB_预设命令行预览.Text = ""
        End Try
    End Sub

    Private Function 从面板创建带元数据的预设() As 预设数据_v6
        Dim data = 预设管理_v6.从面板创建预设(所属参数面板对象)
        data.预设备注 = MTB_预设备注.Text.Trim()
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
        If MCB_预设来源.SelectedIndex <> 1 Then
            MCB_预设来源.SelectedIndex = 1
        Else
            刷新预设列表()
        End If

        Dim idx = _预设列表项.FindIndex(Function(x) Not x.是内置 AndAlso String.Equals(x.文件路径, file, StringComparison.OrdinalIgnoreCase))
        If idx >= 0 Then MLB_预设列表.SelectedIndex = idx
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
            Dim isNewUserPreset As Boolean = False

            If selectedItem Is Nothing Then
                file = 新建用户预设文件路径()
                isNewUserPreset = True
            Else
                Dim presetName = selectedItem.名称
                file = 用户预设文件路径(presetName)
                Dim 已存在 = System.IO.File.Exists(file)
                isNewUserPreset = Not 已存在
                Dim prompt = If(已存在,
                                $"确认覆盖用户预设：{presetName}？",
                                $"确认按当前选中项名称保存为用户预设：{presetName}？")
                Dim buttons = If(已存在, {"覆盖", "取消"}, {"保存", "取消"})
                Dim result = ExFloatingBox(MB_保存预设, prompt, buttons, If(已存在, "确认覆盖", "确认保存"), MsgBoxStyle.Question, 1)
                If result <> 0 Then Exit Sub
            End If

            Dim data = 从面板创建带元数据的预设()
            预设管理_v6.写入预设文件(file, data, MCK_额外保存输出位置.Checked)
            If isNewUserPreset Then 添加用户预设到排序末尾(file)
            选中用户预设文件(file)
            MTB_预设名称.Text = Path.GetFileNameWithoutExtension(file)
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
        Dim suggested = If(MTB_预设名称.Text.Trim() <> "", MTB_预设名称.Text.Trim(), Path.GetFileNameWithoutExtension(file))
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
                Dim isNewUserPreset = Not System.IO.File.Exists(dest)
                预设管理_v6.写入预设文件(dest, data)
                If isNewUserPreset Then 添加用户预设到排序末尾(dest)
            Next
            MCB_预设来源.SelectedIndex = 1
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
        Dim newName = MTB_预设名称.Text.Trim()
        If file = "" OrElse newName = "" Then Exit Sub
        Try
            Dim data = 预设管理_v6.读取预设文件(file)
            If data Is Nothing Then Exit Sub
            Dim userDir = 预设管理_v6.获取预设目录("用户自定义")
            Directory.CreateDirectory(userDir)
            Dim dest = Path.Combine(userDir, 预设管理_v6.安全文件名(newName) & ".json")
            Dim originalIsUserPreset = String.Equals(Path.GetDirectoryName(file), userDir, StringComparison.OrdinalIgnoreCase)
            Dim isNewUserPreset = Not System.IO.File.Exists(dest)
            预设管理_v6.写入预设文件(dest, data)
            If originalIsUserPreset AndAlso Not String.Equals(file, dest, StringComparison.OrdinalIgnoreCase) Then
                Try
                    System.IO.File.Delete(file)
                Catch
                End Try
                替换用户预设排序项(file, dest)
            ElseIf isNewUserPreset Then
                添加用户预设到排序末尾(dest)
            End If
            MCB_预设来源.SelectedIndex = 1
            刷新预设列表()
            Dim idx = _预设列表项.FindIndex(Function(x) Not x.是内置 AndAlso String.Equals(x.文件路径, dest, StringComparison.OrdinalIgnoreCase))
            If idx >= 0 Then MLB_预设列表.SelectedIndex = idx
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
            data.预设备注 = MTB_预设备注.Text.Trim()
            Dim userDir = 预设管理_v6.获取预设目录("用户自定义")
            Dim dest = file
            If Not String.Equals(Path.GetDirectoryName(file), userDir, StringComparison.OrdinalIgnoreCase) Then
                Directory.CreateDirectory(userDir)
                dest = Path.Combine(userDir, 预设管理_v6.安全文件名(Path.GetFileNameWithoutExtension(file)) & ".json")
            End If
            Dim isNewUserPreset = Not System.IO.File.Exists(dest)
            预设管理_v6.写入预设文件(dest, data)
            If isNewUserPreset Then 添加用户预设到排序末尾(dest)
            MCB_预设来源.SelectedIndex = 1
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

        Dim oldIndex = MLB_预设列表.SelectedIndex
        Try
            FileIO.FileSystem.DeleteFile(item.文件路径, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.SendToRecycleBin)
            刷新预设列表()
            If _预设列表项.Count > 0 Then MLB_预设列表.SelectedIndex = Math.Min(oldIndex, _预设列表项.Count - 1)
            ExFloatingTip("已删除到回收站：" & item.名称, 1800)
        Catch ex As Exception
            ExFloatingTip("删除失败：" & ex.Message, 2500)
        End Try
    End Sub

    Private Sub ModernComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_预设来源.SelectedIndexChanged
        刷新预设列表()
    End Sub

    Private Sub ModernListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MLB_预设列表.SelectedIndexChanged
        预览选中预设()
    End Sub

    Private Sub ModernListBox1_ItemDoubleClick(sender As Object, e As LakeUI.ModernListBox.ItemEventArgs) Handles MLB_预设列表.ItemDoubleClick
        加载选中预设()
    End Sub

    Private Sub ModernListBox1_ItemOrderChanged(sender As Object, e As EventArgs) Handles MLB_预设列表.ItemOrderChanged
        同步拖动后的预设列表顺序()
    End Sub

    Private Sub ModernListBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles MLB_预设列表.KeyDown
        If e.KeyCode <> Keys.Delete Then Exit Sub
        删除选中预设到回收站()
        e.Handled = True
        e.SuppressKeyPress = True
    End Sub

    Private Sub ModernButton1_Click(sender As Object, e As EventArgs) Handles MB_保存预设.Click
        保存到用户预设()
    End Sub

    Private Sub ModernButton2_Click(sender As Object, e As EventArgs) Handles MB_读取预设.Click
        加载选中预设()
    End Sub

    Private Sub ModernButton3_Click(sender As Object, e As EventArgs) Handles MB_导出预设.Click
        导出选中预设()
    End Sub

    Private Sub ModernButton4_Click(sender As Object, e As EventArgs) Handles MB_导入预设.Click
        导入预设()
    End Sub

    Private Sub ModernButton5_Click(sender As Object, e As EventArgs) Handles MB_重置所有预设参数.Click
        If 所属参数面板对象 Is Nothing Then Exit Sub
        预设管理_v6.重置面板(所属参数面板对象)
        ExFloatingTip("已重置参数面板", 1800)
    End Sub

    Private Sub ModernButton6_Click(sender As Object, e As EventArgs) Handles MB_变更预设名称.Click
        变更名称()
    End Sub

    Private Sub ModernButton7_Click(sender As Object, e As EventArgs) Handles MB_变更预设备注.Click
        变更备注()
    End Sub

End Class
