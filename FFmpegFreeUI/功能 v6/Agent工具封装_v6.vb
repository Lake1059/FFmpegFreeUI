Imports System.Management
Imports System.Text.Json
Imports LakeUI

Public NotInheritable Class Agent工具封装_v6
    Private Sub New()
    End Sub

    Public Shared Function 获取选项卡(scope As String) As String
        Dim result = UI(Function()
                            Dim tab = 获取选项卡控件(scope)
                            If tab Is Nothing Then Return New Dictionary(Of String, Object) From {{"error", "未知选项卡范围：" & scope}}
                            Return 构建选项卡状态(tab)
                        End Function)
        Return JsonSerializer.Serialize(result, JsonSO)
    End Function

    Public Shared Function 切换选项卡(scope As String, tabValue As String) As String
        Dim result = UI(Function()
                            Dim tab = 获取选项卡控件(scope)
                            If tab Is Nothing Then Return "未知选项卡范围：" & scope
                            Dim index = 查找选项卡(tab, tabValue)
                            If index < 0 Then Return "找不到选项卡：" & tabValue
                            设置选项卡索引(tab, index)
                            Return "已切换到：" & 获取选项卡文本(tab, index)
                        End Function)
        Return result
    End Function

    Public Shared Function 获取准备文件() As String
        Return JsonSerializer.Serialize(UI(Function() Form_v6_准备文件.Agent获取文件列表()), JsonSO)
    End Function

    Public Shared Function 设置准备文件(paths As IEnumerable(Of String), mode As String) As String
        Return UI(Function() Form_v6_准备文件.Agent设置文件列表(paths, mode))
    End Function

    Public Shared Function 提交准备文件到队列() As String
        Return UI(Function() Form_v6_准备文件.Agent加入编码队列())
    End Function

    Public Shared Function 获取集成工具状态(tool As String) As String
        Dim payload = UI(Function()
                             Select Case 规范集成工具名(tool)
                                 Case "merge"
                                     Return Form_v6_集成工具_合并.Agent获取状态()
                                 Case "mux"
                                     Return Form_v6_集成工具_混流.Agent获取状态()
                                 Case "extract"
                                     Return Form_v6_集成工具_抽流.Agent获取状态()
                                 Case Else
                                     Return New Dictionary(Of String, Object) From {{"error", "未知集成工具：" & tool}}
                             End Select
                         End Function)
        Return JsonSerializer.Serialize(payload, JsonSO)
    End Function

    Public Shared Async Function 配置集成工具Async(tool As String, payload As JsonElement) As Task(Of String)
        Select Case 规范集成工具名(tool)
            Case "merge"
                Dim files = Agent通用工具_v6.GetJsonStringArray(payload, "files", False)
                Dim output = Agent通用工具_v6.GetJsonString(payload, "output")
                Dim mode = Agent通用工具_v6.GetJsonString(payload, "mode", "replace")
                Return UI(Function() Form_v6_集成工具_合并.Agent配置(files, output, mode))
            Case "mux"
                Dim specs = Json混流文件规格(payload)
                Dim output = Agent通用工具_v6.GetJsonString(payload, "output")
                Dim mode = Agent通用工具_v6.GetJsonString(payload, "mode", "replace")
                Return UI(Function() Form_v6_集成工具_混流.Agent配置(specs, output, mode))
            Case "extract"
                Dim file = Agent通用工具_v6.GetJsonString(payload, "file")
                Dim outputLocation = Agent通用工具_v6.GetJsonString(payload, "output_location")
                Dim selectedStreams = Agent通用工具_v6.GetJsonStringArray(payload, "selected_streams", False)
                Return Await UIAsync(Function() Form_v6_集成工具_抽流.Agent配置Async(file, outputLocation, selectedStreams))
            Case Else
                Return "未知集成工具：" & tool
        End Select
    End Function

    Public Shared Async Function 运行集成工具Async(tool As String, payload As JsonElement) As Task(Of String)
        Select Case 规范集成工具名(tool)
            Case "merge"
                Return UI(Function() Form_v6_集成工具_合并.Agent运行())
            Case "mux"
                Return UI(Function() Form_v6_集成工具_混流.Agent运行())
            Case "extract"
                Dim forceAutoName = Agent通用工具_v6.GetJsonBoolean(payload, "force_auto_name", True)
                Return Await UIAsync(Function() Form_v6_集成工具_抽流.Agent运行Async(forceAutoName))
            Case Else
                Return "未知集成工具：" & tool
        End Select
    End Function

    Public Shared Function 获取系统硬件() As String
        Dim payload As New Dictionary(Of String, Object) From {
            {"processor", 获取处理器名称()},
            {"memory", 获取内存信息()},
            {"gpus", 获取显卡名称列表()}
        }
        Return JsonSerializer.Serialize(payload, JsonSO)
    End Function

    Public Shared Function 列出参数预设(source As String) As String
        Return JsonSerializer.Serialize(UI(Function() Form_v6_参数面板.私有界面_预设管理.Agent列出预设(source)), JsonSO)
    End Function

    Public Shared Function 读取参数预设(source As String, name As String) As String
        Return JsonSerializer.Serialize(UI(Function() Form_v6_参数面板.私有界面_预设管理.Agent读取预设(source, name)), JsonSO)
    End Function

    Public Shared Function 应用参数预设(source As String, name As String) As String
        Return UI(Function() Form_v6_参数面板.私有界面_预设管理.Agent应用预设(source, name))
    End Function

    Public Shared Function 保存参数预设(source As String, name As String, presetJson As String, note As String, saveOutputLocation As Boolean?) As String
        Return UI(Function() Form_v6_参数面板.私有界面_预设管理.Agent保存预设(source, name, presetJson, note, saveOutputLocation))
    End Function

    Public Shared Function 获取参数面板控件信息(query As String) As String
        Dim payload = UI(Function() 扫描控件(Form_v6_参数面板, query))
        Return JsonSerializer.Serialize(payload, JsonSO)
    End Function

    Public Shared Function 获取输出容器候选() As List(Of String)
        Return UI(Function() Form_v6_参数面板.私有界面_输出文件设置.Agent获取输出容器候选())
    End Function

    Public Shared Function 获取组合框候选(controlName As String) As List(Of String)
        Return UI(Function()
                      Dim combo = 查找参数面板组合框(controlName)
                      If combo Is Nothing Then Return New List(Of String)
                      Return 读取组合框项(combo)
                  End Function)
    End Function

    Private Shared Function 获取选项卡控件(scope As String) As Object
        Select Case If(scope, "").Trim().ToLowerInvariant()
            Case "", "main", "主页面"
                Return FormMain_v6.ModernTabListControl1
            Case "parameter", "parameters", "参数面板"
                Return Form_v6_参数面板.ModernTabListControl1
            Case "integrated", "integrated_tools", "集成工具"
                Return Form_v6_集成工具.ModernTabListControl1
            Case "settings", "setting", "软件设置", "设置"
                Return Form_v6_设置.ModernTabListControl1
            Case "custom_parameters", "自定义参数"
                Return Form_v6_参数面板.私有界面_自定义参数.ModernTabControl1
            Case "attachments", "附加内容"
                Return Form_v6_参数面板.私有界面_附加内容.ModernTabControl1
            Case Else
                Return Nothing
        End Select
    End Function

    Private Shared Function 构建选项卡状态(tab As Object) As Dictionary(Of String, Object)
        Dim items As New List(Of Dictionary(Of String, Object))
        Dim count = 获取选项卡数量(tab)
        Dim selected = 获取选项卡索引(tab)
        For i = 0 To count - 1
            If 选项卡是否分割或说明(tab, i) Then Continue For
            items.Add(New Dictionary(Of String, Object) From {
                {"index", i},
                {"text", 获取选项卡文本(tab, i)},
                {"selected", i = selected}
            })
        Next
        Return New Dictionary(Of String, Object) From {
            {"selected_index", selected},
            {"selected_text", If(selected >= 0 AndAlso selected < count, 获取选项卡文本(tab, selected), "")},
            {"tabs", items}
        }
    End Function

    Private Shared Function 查找选项卡(tab As Object, value As String) As Integer
        Dim text = If(value, "").Trim()
        Dim numeric As Integer
        Dim count = 获取选项卡数量(tab)
        If Integer.TryParse(text, numeric) AndAlso numeric >= 0 AndAlso numeric < count Then Return numeric
        For i = 0 To count - 1
            If String.Equals(获取选项卡文本(tab, i), text, StringComparison.CurrentCultureIgnoreCase) Then Return i
        Next
        For i = 0 To count - 1
            If 获取选项卡文本(tab, i).Contains(text, StringComparison.CurrentCultureIgnoreCase) Then Return i
        Next
        Return -1
    End Function

    Private Shared Function 获取选项卡数量(tab As Object) As Integer
        If TypeOf tab Is ModernTabListControl Then Return DirectCast(tab, ModernTabListControl).Items.Count
        If TypeOf tab Is ModernTabControl Then Return DirectCast(tab, ModernTabControl).Items.Count
        Return 0
    End Function

    Private Shared Function 获取选项卡索引(tab As Object) As Integer
        If TypeOf tab Is ModernTabListControl Then Return DirectCast(tab, ModernTabListControl).SelectedIndex
        If TypeOf tab Is ModernTabControl Then Return DirectCast(tab, ModernTabControl).SelectedIndex
        Return -1
    End Function

    Private Shared Sub 设置选项卡索引(tab As Object, index As Integer)
        If TypeOf tab Is ModernTabListControl Then
            DirectCast(tab, ModernTabListControl).SelectedIndex = index
        ElseIf TypeOf tab Is ModernTabControl Then
            DirectCast(tab, ModernTabControl).SelectedIndex = index
        End If
    End Sub

    Private Shared Function 获取选项卡文本(tab As Object, index As Integer) As String
        If TypeOf tab Is ModernTabListControl Then Return DirectCast(tab, ModernTabListControl).Items(index).Text
        If TypeOf tab Is ModernTabControl Then Return DirectCast(tab, ModernTabControl).Items(index).Text
        Return ""
    End Function

    Private Shared Function 选项卡是否分割或说明(tab As Object, index As Integer) As Boolean
        If TypeOf tab Is ModernTabListControl Then
            Dim item = DirectCast(tab, ModernTabListControl).Items(index)
            Return item.IsSeparator OrElse item.IsDescription
        End If
        If TypeOf tab Is ModernTabControl Then Return DirectCast(tab, ModernTabControl).Items(index).IsSeparator
        Return True
    End Function

    Private Shared Function 扫描控件(root As Control, query As String) As List(Of Dictionary(Of String, Object))
        Dim result As New List(Of Dictionary(Of String, Object))
        Dim q = If(query, "").Trim()
        For Each scanRoot In 获取参数面板扫描根()
            扫描控件递归(scanRoot, q, result)
        Next
        Return result.Take(200).ToList()
    End Function

    Private Shared Function 获取参数面板扫描根() As List(Of Control)
        Dim panel = Form_v6_参数面板
        Return New List(Of Control) From {
            panel,
            panel.私有界面_参数总览,
            panel.私有界面_预设管理,
            panel.私有界面_输出文件设置,
            panel.私有界面_解码参数,
            panel.私有界面_视频编码器,
            panel.私有界面_画面帧,
            panel.私有界面_画面帧.私有窗口_抽帧参数,
            panel.私有界面_画面帧.私有窗口_插帧参数,
            panel.私有界面_画面帧.私有窗口_动态模糊,
            panel.私有界面_画面帧.私有窗口_着色器超分,
            panel.私有界面_画面帧.私有窗口_降噪,
            panel.私有界面_画面帧.私有窗口_锐化,
            panel.私有界面_画面帧.私有窗口_胶片颗粒,
            panel.私有界面_画面帧.私有窗口_平滑断层,
            panel.私有界面_画面帧.私有窗口_扫描方式,
            panel.私有界面_画面帧.私有窗口_画面翻转,
            panel.私有界面_画面帧.私有窗口_烧录字幕,
            panel.私有界面_质量,
            panel.私有界面_色彩管理,
            panel.私有界面_视频帧服务器,
            panel.私有界面_音频参数,
            panel.私有界面_剪辑区间,
            panel.私有界面_滤镜排序,
            panel.私有界面_自定义参数,
            panel.私有界面_自定义参数说明,
            panel.私有界面_流自定义参数,
            panel.私有界面_在位置插入参数,
            panel.私有界面_完全自己写模式,
            panel.私有界面_流控制,
            panel.私有界面_附加内容,
            panel.私有界面_元数据,
            panel.私有界面_章节,
            panel.私有界面_附件
        }.Where(Function(x) x IsNot Nothing).Distinct().ToList()
    End Function

    Private Shared Function 查找参数面板组合框(controlName As String) As ModernComboBox
        Dim name = If(controlName, "").Trim()
        If name = "" Then Return Nothing
        For Each root In 获取参数面板扫描根()
            Dim found = 查找组合框递归(root, name)
            If found IsNot Nothing Then Return found
        Next
        Return Nothing
    End Function

    Private Shared Function 查找组合框递归(control As Control, controlName As String) As ModernComboBox
        If control Is Nothing Then Return Nothing
        Dim combo = TryCast(control, ModernComboBox)
        If combo IsNot Nothing AndAlso String.Equals(combo.Name, controlName, StringComparison.OrdinalIgnoreCase) Then Return combo
        For Each child As Control In control.Controls
            Dim found = 查找组合框递归(child, controlName)
            If found IsNot Nothing Then Return found
        Next
        Return Nothing
    End Function

    Private Shared Function 读取组合框项(combo As ModernComboBox) As List(Of String)
        Dim result As New List(Of String)
        If combo Is Nothing Then Return result
        For Each rawItem In combo.Items
            Dim value = If(rawItem, "").ToString()
            If Not result.Contains(value, StringComparer.OrdinalIgnoreCase) Then result.Add(value)
        Next
        Return result
    End Function

    Private Shared Sub 扫描控件递归(control As Control, query As String, result As List(Of Dictionary(Of String, Object)))
        If control Is Nothing OrElse result.Count >= 200 Then Return
        Dim typeName = control.GetType().Name
        Dim text = If(control.Text, "")
        Dim name = If(control.Name, "")
        Dim isInteresting = 是参数面板可读控件(control)
        If isInteresting AndAlso (query = "" OrElse name.Contains(query, StringComparison.CurrentCultureIgnoreCase) OrElse text.Contains(query, StringComparison.CurrentCultureIgnoreCase)) Then
            Dim item As New Dictionary(Of String, Object) From {
                {"name", name},
                {"type", typeName},
                {"text", 截断控件文本(text)},
                {"enabled", control.Enabled},
                {"visible", control.Visible}
            }
            Dim combo = TryCast(control, ModernComboBox)
            If combo IsNot Nothing Then
                item("items") = 读取组合框项(combo)
                item("selected_index") = combo.SelectedIndex
                item("selected_text") = If(combo.SelectedItem, combo.Text)
                item("editable") = combo.Editable
            End If

            Dim textBox = TryCast(control, ModernTextBox)
            If textBox IsNot Nothing Then
                item("value") = 截断控件文本(textBox.Text)
                item("water_text") = 截断控件文本(textBox.WaterText)
                item("read_only") = textBox.ReadOnly
                item("multiline") = textBox.MultiLine
            End If

            Dim checkBox = TryCast(control, ModernCheckBox)
            If checkBox IsNot Nothing Then item("checked") = checkBox.Checked

            Dim switchBox = TryCast(control, BooleanSwitch)
            If switchBox IsNot Nothing Then item("checked") = switchBox.Checked

            Dim trackBar = TryCast(control, ExcellentTrackBar)
            If trackBar IsNot Nothing Then
                item("value") = trackBar.Value
                item("minimum") = trackBar.Minimum
                item("maximum") = trackBar.Maximum
            End If

            Dim listBox = TryCast(control, ModernListBox)
            If listBox IsNot Nothing Then
                item("items") = listBox.Items.Cast(Of Object).Select(Function(x) If(x, "").ToString()).ToList()
                item("selected_index") = listBox.SelectedIndex
                item("selected_text") = If(listBox.SelectedIndex >= 0 AndAlso listBox.SelectedIndex < listBox.Items.Count, If(listBox.Items(listBox.SelectedIndex), "").ToString(), "")
                item("allow_drag_reorder") = listBox.AllowDragReorder
            End If

            Dim detailList = TryCast(control, UltraDetailListView)
            If detailList IsNot Nothing Then
                item("row_count") = detailList.Items.Count
                item("selected_count") = detailList.SelectedItems.Count
                item("rows") = detailList.Items.
                    Cast(Of UltraDetailListView.ListItem)().
                    Take(30).
                    Select(Function(row) row.SubItems.Cast(Of UltraDetailListView.ListSubItem)().Select(Function(subItem) 截断控件文本(subItem.Text, 500)).ToList()).
                    ToList()
            End If
            result.Add(item)
        End If
        For Each child As Control In control.Controls
            扫描控件递归(child, query, result)
        Next
    End Sub

    Private Shared Function 是参数面板可读控件(control As Control) As Boolean
        Dim name = If(control?.Name, "")
        If name.StartsWith("MCB_", StringComparison.Ordinal) OrElse
           name.StartsWith("MCK_", StringComparison.Ordinal) OrElse
           name.StartsWith("MTB_", StringComparison.Ordinal) OrElse
           name.StartsWith("MB_", StringComparison.Ordinal) OrElse
           name.StartsWith("HCL_", StringComparison.Ordinal) OrElse
           name.StartsWith("ETB_", StringComparison.Ordinal) OrElse
           name.StartsWith("BS_", StringComparison.Ordinal) OrElse
           name.StartsWith("MLB_", StringComparison.Ordinal) OrElse
           name.StartsWith("UDLV_", StringComparison.Ordinal) OrElse
           name.StartsWith("PPB_", StringComparison.Ordinal) OrElse
           name.StartsWith("MDV_", StringComparison.Ordinal) OrElse
           name.StartsWith("LB_", StringComparison.Ordinal) Then Return True

        Return TypeOf control Is ModernComboBox OrElse
               TypeOf control Is ModernTextBox OrElse
               TypeOf control Is ModernCheckBox OrElse
               TypeOf control Is ModernButton OrElse
               TypeOf control Is HtmlColorLabel OrElse
               TypeOf control Is Label OrElse
               TypeOf control Is BooleanSwitch OrElse
               TypeOf control Is ExcellentTrackBar OrElse
               TypeOf control Is ModernListBox OrElse
               TypeOf control Is UltraDetailListView
    End Function

    Private Shared Function 截断控件文本(value As String, Optional maxLength As Integer = 4000) As String
        Dim text = If(value, "")
        If text.Length <= maxLength Then Return text
        Return text.Substring(0, maxLength) & "..."
    End Function

    Private Shared Function 规范集成工具名(tool As String) As String
        Select Case If(tool, "").Trim().ToLowerInvariant()
            Case "merge", "concat", "合并"
                Return "merge"
            Case "mux", "mix", "混流"
                Return "mux"
            Case "extract", "demux", "抽流"
                Return "extract"
            Case Else
                Return ""
        End Select
    End Function

    Private Shared Function Json混流文件规格(root As JsonElement) As List(Of Dictionary(Of String, Object))
        Dim result As New List(Of Dictionary(Of String, Object))
        Dim files As JsonElement
        If root.ValueKind <> JsonValueKind.Object OrElse Not root.TryGetProperty("files", files) OrElse files.ValueKind <> JsonValueKind.Array Then Return result
        For Each item In files.EnumerateArray()
            If item.ValueKind = JsonValueKind.String Then
                result.Add(New Dictionary(Of String, Object) From {{"path", item.GetString()}})
            ElseIf item.ValueKind = JsonValueKind.Object Then
                Dim spec As New Dictionary(Of String, Object)
                For Each prop In item.EnumerateObject()
                    Select Case prop.Value.ValueKind
                        Case JsonValueKind.String
                            spec(prop.Name) = prop.Value.GetString()
                        Case JsonValueKind.True
                            spec(prop.Name) = True
                        Case JsonValueKind.False
                            spec(prop.Name) = False
                        Case JsonValueKind.Number
                            spec(prop.Name) = prop.Value.GetRawText()
                    End Select
                Next
                result.Add(spec)
            End If
        Next
        Return result
    End Function

    Private Shared Function 获取处理器名称() As String
        Try
            Using searcher As New ManagementObjectSearcher("SELECT Name FROM Win32_Processor")
                For Each item As ManagementObject In searcher.Get()
                    Dim name = If(item("Name"), "").ToString().Trim()
                    If name <> "" Then Return name
                Next
            End Using
        Catch
        End Try
        Return Environment.ProcessorCount & " 线程处理器"
    End Function

    Private Shared Function 获取内存信息() As Dictionary(Of String, Object)
        Dim total As ULong = 0
        Try
            Using searcher As New ManagementObjectSearcher("SELECT Capacity FROM Win32_PhysicalMemory")
                For Each item As ManagementObject In searcher.Get()
                    Dim value As ULong
                    If ULong.TryParse(If(item("Capacity"), "0").ToString(), value) Then total += value
                Next
            End Using
        Catch
        End Try
        If total = 0 Then total = CULng(My.Computer.Info.TotalPhysicalMemory)
        Return New Dictionary(Of String, Object) From {
            {"total_bytes", total},
            {"total_gb", Math.Round(total / 1024.0 / 1024.0 / 1024.0, 1)}
        }
    End Function

    Private Shared Function 获取显卡名称列表() As List(Of String)
        Dim result As New List(Of String)
        Try
            Using searcher As New ManagementObjectSearcher("SELECT Name FROM Win32_VideoController")
                For Each item As ManagementObject In searcher.Get()
                    Dim name = If(item("Name"), "").ToString().Trim()
                    If name <> "" AndAlso Not result.Contains(name, StringComparer.OrdinalIgnoreCase) Then result.Add(name)
                Next
            End Using
        Catch
        End Try
        Return result
    End Function

    Private Shared Function UI(Of T)(func As Func(Of T)) As T
        If FormMain_v6 IsNot Nothing AndAlso FormMain_v6.IsHandleCreated AndAlso FormMain_v6.InvokeRequired Then
            Return DirectCast(FormMain_v6.Invoke(func), T)
        End If
        Return func()
    End Function

    Private Shared Function UIAsync(Of T)(func As Func(Of Task(Of T))) As Task(Of T)
        If FormMain_v6 IsNot Nothing AndAlso FormMain_v6.IsHandleCreated AndAlso FormMain_v6.InvokeRequired Then
            Dim tcs As New TaskCompletionSource(Of T)(TaskCreationOptions.RunContinuationsAsynchronously)
            FormMain_v6.BeginInvoke(Async Sub()
                                        Try
                                            tcs.SetResult(Await func())
                                        Catch ex As Exception
                                            tcs.SetException(ex)
                                        End Try
                                    End Sub)
            Return tcs.Task
        End If
        Return func()
    End Function
End Class
