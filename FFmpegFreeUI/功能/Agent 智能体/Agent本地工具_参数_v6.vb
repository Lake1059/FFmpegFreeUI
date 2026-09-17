Imports System.Drawing.Imaging
Imports System.IO
Imports System.Net
Imports System.Net.Http
Imports System.Text
Imports System.Text.Json
Imports System.Text.RegularExpressions

Partial Public Class AgentLocalTools
    Private Shared Function BuildParameterResultProperties(defaultOverview As Boolean, defaultCommandPreview As Boolean,
                                                            Optional properties As Dictionary(Of String, Object) = Nothing) As Dictionary(Of String, Object)
        Dim result = If(properties, New Dictionary(Of String, Object))
        Dim views As New Dictionary(Of String, Object) From {
            {"include_overview", New Dictionary(Of String, Object) From {{"type", "boolean"}, {"default", defaultOverview}, {"description", "返回人类可读总览"}}},
            {"include_command_preview", New Dictionary(Of String, Object) From {{"type", "boolean"}, {"default", defaultCommandPreview}, {"description", "生成并返回命令行预览"}}},
            {"include_preset_json", New Dictionary(Of String, Object) From {{"type", "boolean"}, {"default", False}, {"description", "返回完整预设 JSON 字符串，内容较大，仅按需开启"}}}
        }
        For Each view In views
            result.Add(view.Key, view.Value)
        Next
        Return result
    End Function

    Private Shared Sub AddParameterResultViews(payload As Dictionary(Of String, Object), preset As 预设数据_v6, args As JsonElement,
                                              defaultOverview As Boolean, defaultCommandPreview As Boolean)
        If Agent通用工具_v6.GetJsonBoolean(args, "include_overview", defaultOverview) Then payload("overview") = BuildParameterOverview(preset)
        If Agent通用工具_v6.GetJsonBoolean(args, "include_command_preview", defaultCommandPreview) Then payload("command_preview") = BuildParameterCommandPreview(preset)
        If Agent通用工具_v6.GetJsonBoolean(args, "include_preset_json", False) Then payload("preset_json") = JsonSerializer.Serialize(preset, ToolJsonOptions)
    End Sub

    Private Shared Function GetParameterPanelState(args As JsonElement) As String
        If Not Agent通用工具_v6.GetJsonBoolean(args, "include_overview", True) AndAlso
           Not Agent通用工具_v6.GetJsonBoolean(args, "include_command_preview", False) AndAlso
           Not Agent通用工具_v6.GetJsonBoolean(args, "include_preset_json", False) Then
            Return JsonSerializer.Serialize(New Dictionary(Of String, Object) From {{"error", "至少开启一个 include_* 返回选项。"}}, ToolJsonOptions)
        End If
        Dim preset = 预设管理_v6.从面板创建预设(Form_v6_参数面板)
        Dim payload As New Dictionary(Of String, Object)
        AddParameterResultViews(payload, preset, args, True, False)
        Return JsonSerializer.Serialize(payload, ToolJsonOptions)
    End Function

    Private Shared Function ApplyParameterPanelPatch(args As JsonElement) As String
        Dim presetJson As JsonElement
        Dim preset As 预设数据_v6
        Dim previousPreset = 预设管理_v6.从面板创建预设(Form_v6_参数面板)
        Dim explicitFilterOrderChange As Boolean = False
        Dim requestedChanges As New List(Of Dictionary(Of String, Object))
        If args.ValueKind = JsonValueKind.Object AndAlso args.TryGetProperty("preset_json", presetJson) AndAlso presetJson.ValueKind = JsonValueKind.String AndAlso presetJson.GetString() <> "" Then
            preset = JsonSerializer.Deserialize(Of 预设数据_v6)(presetJson.GetString(), JsonSO)
            If preset Is Nothing Then Return "preset_json 必须是预设对象，不能为 null。"
            explicitFilterOrderChange = JsonObjectHasProperty(presetJson.GetString(), NameOf(预设数据_v6.滤镜排序系统))
            requestedChanges.Add(New Dictionary(Of String, Object) From {
                {"field", "preset_json"},
                {"mode", "replace_all"}
            })
        Else
            preset = ClonePresetData(previousPreset)
            Dim changes As JsonElement
            If args.ValueKind = JsonValueKind.Object AndAlso args.TryGetProperty("changes", changes) AndAlso changes.ValueKind = JsonValueKind.Object Then
                explicitFilterOrderChange = HasJsonProperty(changes, NameOf(预设数据_v6.滤镜排序系统))
                requestedChanges = ApplyTopLevelChanges(preset, changes)
            Else
                Return "没有提供 changes 或 preset_json"
            End If
        End If

        Dim noteElement As JsonElement
        If args.ValueKind = JsonValueKind.Object AndAlso args.TryGetProperty("note", noteElement) Then
            Dim oldNote = preset.预设备注
            preset.预设备注 = If(noteElement.ValueKind = JsonValueKind.Null OrElse noteElement.ValueKind = JsonValueKind.Undefined, "", Agent通用工具_v6.GetJsonString(args, "note"))
            requestedChanges.Add(New Dictionary(Of String, Object) From {
                {"field", NameOf(预设数据_v6.预设备注)},
                {"before", oldNote},
                {"requested", preset.预设备注}
            })
        End If

        If explicitFilterOrderChange Then 预设管理_v6.应用Agent滤镜排序请求(preset, previousPreset)
        预设管理_v6.显示预设(preset, Form_v6_参数面板)
        Dim actualPreset = 预设管理_v6.从面板创建预设(Form_v6_参数面板)
        Dim payload As New Dictionary(Of String, Object) From {
            {"message", "已应用参数面板修改"},
            {"requested_changes", requestedChanges},
            {"effective_changed_fields", BuildChangedFieldList(previousPreset, actualPreset)},
            {"filter_order_requested", explicitFilterOrderChange}
        }
        AddParameterResultViews(payload, actualPreset, args, False, True)
        Return JsonSerializer.Serialize(payload, ToolJsonOptions)
    End Function

    Private Shared Function SyncParameterPanelToQueue() As String
        Dim preset = 预设管理_v6.从面板创建预设(Form_v6_参数面板)
        Dim queueSync = 编码队列_v6.同步未处理预设任务(preset)
        Return BuildQueueSyncSummary(queueSync)
    End Function

    Private Shared Function BuildQueueSyncSummary(result As 编码队列_v6.预设同步结果) As String
        If result Is Nothing Then Return "编码队列同步：未更新任务"
        Dim parts As New List(Of String) From {$"已更新 {result.已更新} 个未处理预设任务"}
        If result.已跳过非预设任务 > 0 Then parts.Add($"跳过 {result.已跳过非预设任务} 个命令行任务")
        If result.已跳过不可修改任务 > 0 Then parts.Add($"跳过 {result.已跳过不可修改任务} 个已开始或已结束任务")
        Return "编码队列同步：" & String.Join("，", parts)
    End Function

    Private Shared Function GetParameterFieldInfo(args As JsonElement) As String
        Dim preset = 预设管理_v6.从面板创建预设(Form_v6_参数面板)
        Dim includeCurrentValues = Agent通用工具_v6.GetJsonBoolean(args, "include_current_values", True)
        Dim requestedFields = Agent通用工具_v6.GetJsonStringArray(args, "fields")
        Dim query = Agent通用工具_v6.GetJsonString(args, "query").Trim()
        Dim props = GetType(预设数据_v6).GetProperties().
            Where(Function(x) x.CanRead AndAlso x.CanWrite).
            OrderBy(Function(x) x.Name, StringComparer.CurrentCultureIgnoreCase).
            ToList()

        Dim candidateProps As New List(Of Reflection.PropertyInfo)
        Dim matchedProps As New List(Of Reflection.PropertyInfo)
        Dim missingFields As New List(Of String)
        If requestedFields.Count > 0 Then
            For Each field In requestedFields
                Dim prop = props.FirstOrDefault(Function(x) String.Equals(x.Name, field, StringComparison.OrdinalIgnoreCase))
                If prop Is Nothing Then
                    missingFields.Add(field)
                ElseIf Not matchedProps.Contains(prop) Then
                    matchedProps.Add(prop)
                End If
            Next
        ElseIf query <> "" Then
            candidateProps = props.
                Select(Function(prop) New With {.Prop = prop, .Score = GetPropertyMatchScore(query, prop.Name)}).
                Where(Function(x) x.Score > 0).
                OrderByDescending(Function(x) x.Score).
                ThenBy(Function(x) x.Prop.Name, StringComparer.CurrentCultureIgnoreCase).
                Select(Function(x) x.Prop).
                ToList()
            matchedProps = candidateProps.Take(30).ToList()
        Else
            candidateProps = props
            matchedProps = candidateProps.Take(30).ToList()
        End If

        Dim items = matchedProps.Select(Function(prop) BuildParameterFieldInfoItem(prop, preset, includeCurrentValues)).ToList()
        Dim payload As New Dictionary(Of String, Object) From {
            {"fields", items},
            {"missing_fields", missingFields},
            {"truncated", requestedFields.Count = 0 AndAlso candidateProps.Count > matchedProps.Count},
            {"hint", "字段值必须传给 apply_parameter_panel_patch 的 changes；本工具只查询候选和规则，不修改参数面板。"}
        }
        Return JsonSerializer.Serialize(payload, JsonSO)
    End Function

    Private Shared Function BuildParameterFieldInfoItem(prop As Reflection.PropertyInfo, preset As 预设数据_v6, includeCurrentValue As Boolean) As Dictionary(Of String, Object)
        Dim info As New Dictionary(Of String, Object) From {
            {"name", prop.Name},
            {"type", GetFriendlyTypeName(prop.PropertyType)}
        }

        If includeCurrentValue Then
            info("current_value") = prop.GetValue(preset)
        End If

        Dim enumValues = GetEnumValues(prop.PropertyType)
        If enumValues.Count > 0 Then info("enum_values") = enumValues

        If prop.PropertyType Is GetType(Boolean) Then
            info("candidates") = New String() {"true", "false"}
        End If

        Dim candidates = GetKnownParameterCandidates(prop.Name, preset)
        If candidates.Count > 0 Then info("candidates") = candidates

        Dim rules = GetKnownParameterRules(prop.Name)
        If rules.Count > 0 Then info("rules") = rules

        Dim notes = GetKnownParameterNotes(prop.Name, preset)
        If notes.Count > 0 Then info("notes") = notes

        Return info
    End Function

    Private Shared Function GetFriendlyTypeName(type As Type) As String
        If type Is GetType(String) Then Return "string"
        If type Is GetType(Boolean) Then Return "boolean"
        If type Is GetType(Integer) Then Return "integer"
        If type Is GetType(Double) Then Return "number"
        If type.IsEnum Then Return "enum:" & type.Name
        If type.IsArray Then Return "array:" & GetFriendlyTypeName(type.GetElementType())
        If type.IsGenericType AndAlso type.GetGenericTypeDefinition() Is GetType(List(Of )) Then Return "array:" & GetFriendlyTypeName(type.GetGenericArguments()(0))
        Return type.Name
    End Function

    Private Shared Function GetEnumValues(type As Type) As List(Of Dictionary(Of String, Object))
        Dim result As New List(Of Dictionary(Of String, Object))
        If Not type.IsEnum Then Return result

        For Each value In [Enum].GetValues(type)
            result.Add(New Dictionary(Of String, Object) From {
                {"name", [Enum].GetName(type, value)},
                {"value", CInt(value)}
            })
        Next
        Return result
    End Function

    Private Shared Function GetKnownParameterCandidates(fieldName As String, preset As 预设数据_v6) As List(Of Object)
        Select Case fieldName
            Case NameOf(预设数据_v6.输出容器)
                Return BuildStringCandidates(Agent工具封装_v6.获取输出容器候选())
            Case NameOf(预设数据_v6.输出_自动命名选项)
                Return BuildAutoNameOptionCandidates()
            Case NameOf(预设数据_v6.视频参数_编码器_分类名称)
                Return BuildVideoCategoryCandidates(preset)
            Case NameOf(预设数据_v6.视频参数_编码器_具体编码)
                Return BuildVideoEncoderCandidates(preset)
            Case NameOf(预设数据_v6.视频参数_编码器_编码预设)
                Return BuildVideoParameterListCandidates(preset, 视频编码器数据库_v6.编码器参数角色.编码预设)
            Case NameOf(预设数据_v6.视频参数_编码器_配置文件)
                Return BuildVideoParameterListCandidates(preset, 视频编码器数据库_v6.编码器参数角色.配置文件)
            Case NameOf(预设数据_v6.视频参数_编码器_场景优化)
                Return BuildVideoParameterListCandidates(preset, 视频编码器数据库_v6.编码器参数角色.场景优化)
            Case NameOf(预设数据_v6.视频参数_色彩管理_像素格式)
                Return BuildVideoParameterListCandidates(preset, 视频编码器数据库_v6.编码器参数角色.像素格式)
            Case NameOf(预设数据_v6.视频参数_色彩管理_像素格式预先转换)
                Return BuildStringCandidates(Agent工具封装_v6.获取组合框候选("MCB_像素格式预先转换"))
            Case NameOf(预设数据_v6.视频参数_质量控制_参数名)
                Return BuildStringCandidates(视频编码器数据库_v6.获取质量参数名列表())
            Case NameOf(预设数据_v6.音频参数_编码器_代号)
                Return BuildAudioEncoderCandidates()
            Case NameOf(预设数据_v6.音频参数_质量参数名),
                 NameOf(预设数据_v6.音频参数_质量参数名2)
                Return BuildStringCandidates(音频编码器数据库_v6.获取质量参数名列表())
        End Select
        Return New List(Of Object)
    End Function

    Private Shared Function BuildStringCandidates(values As IEnumerable(Of String)) As List(Of Object)
        Dim result As New List(Of Object)
        If values Is Nothing Then Return result

        Dim seen As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
        For Each rawValue In values
            Dim value = If(rawValue, "")
            If seen.Add(value) Then result.Add(value)
        Next
        Return result
    End Function

    Private Shared Function BuildVideoCategoryCandidates(preset As 预设数据_v6) As List(Of Object)
        Return 视频编码器数据库_v6.获取分类列表(preset.视频参数_编码器_类型).
            Select(Function(x) CType(New Dictionary(Of String, Object) From {
                {"value", x.名称},
                {"description", x.描述}
            }, Object)).
            ToList()
    End Function

    Private Shared Function BuildVideoEncoderCandidates(preset As 预设数据_v6) As List(Of Object)
        Return 视频编码器数据库_v6.获取编码器列表(preset.视频参数_编码器_分类名称).
            Select(Function(x) CType(New Dictionary(Of String, Object) From {
                {"value", x.名称},
                {"command", x.命令行编码器名},
                {"type", x.类型.ToString()}
            }, Object)).
            ToList()
    End Function

    Private Shared Function BuildAudioEncoderCandidates() As List(Of Object)
        Return 音频编码器数据库_v6.全部编码器.
            Select(Function(x) CType(New Dictionary(Of String, Object) From {
                {"value", x.私有ID},
                {"label", x.显示名称},
                {"command", x.命令行编码器名}
            }, Object)).
            ToList()
    End Function

    Private Shared Function BuildAutoNameOptionCandidates() As List(Of Object)
        Return New List(Of Object) From {
            New Dictionary(Of String, Object) From {{"name", NameOf(预设数据_v6.自动命名选项.不使用自动命名)}, {"value", CInt(预设数据_v6.自动命名选项.不使用自动命名)}, {"description", "不附加自动后缀，容易覆盖目标文件。"}},
            New Dictionary(Of String, Object) From {{"name", NameOf(预设数据_v6.自动命名选项.附加_递增时间戳)}, {"value", CInt(预设数据_v6.自动命名选项.附加_递增时间戳)}, {"description", "默认选项；追加 _yyyy.MM.dd-HH.mm.ss，若文件名结尾已有同格式时间戳则替换它。"}},
            New Dictionary(Of String, Object) From {{"name", NameOf(预设数据_v6.自动命名选项.附加_递增数字)}, {"value", CInt(预设数据_v6.自动命名选项.附加_递增数字)}, {"description", "目标存在时使用 ~1、~2 递增。"}},
            New Dictionary(Of String, Object) From {{"name", NameOf(预设数据_v6.自动命名选项.附加_3FUI)}, {"value", CInt(预设数据_v6.自动命名选项.附加_3FUI)}, {"description", "附加 _3fui。"}},
            New Dictionary(Of String, Object) From {{"name", NameOf(预设数据_v6.自动命名选项.常规压片_附加编码器和质量参数)}, {"value", CInt(预设数据_v6.自动命名选项.常规压片_附加编码器和质量参数)}, {"description", "根据编码器、预设、质量、码率等参数生成后缀。"}},
            New Dictionary(Of String, Object) From {{"name", NameOf(预设数据_v6.自动命名选项.附加_随机8位数字)}, {"value", CInt(预设数据_v6.自动命名选项.附加_随机8位数字)}, {"description", "附加 _ 加 8 位随机数字。"}},
            New Dictionary(Of String, Object) From {{"name", NameOf(预设数据_v6.自动命名选项.附加_随机8位字母)}, {"value", CInt(预设数据_v6.自动命名选项.附加_随机8位字母)}, {"description", "附加 _ 加 8 位随机字母。"}},
            New Dictionary(Of String, Object) From {{"name", NameOf(预设数据_v6.自动命名选项.附加_随机8位数字和字母组合)}, {"value", CInt(预设数据_v6.自动命名选项.附加_随机8位数字和字母组合)}, {"description", "附加 _ 加 8 位随机数字字母。"}},
            New Dictionary(Of String, Object) From {{"name", NameOf(预设数据_v6.自动命名选项.附加_随机16位数字)}, {"value", CInt(预设数据_v6.自动命名选项.附加_随机16位数字)}, {"description", "附加 _ 加 16 位随机数字。"}},
            New Dictionary(Of String, Object) From {{"name", NameOf(预设数据_v6.自动命名选项.附加_随机16位字母)}, {"value", CInt(预设数据_v6.自动命名选项.附加_随机16位字母)}, {"description", "附加 _ 加 16 位随机字母。"}},
            New Dictionary(Of String, Object) From {{"name", NameOf(预设数据_v6.自动命名选项.附加_随机16位数字和字母组合)}, {"value", CInt(预设数据_v6.自动命名选项.附加_随机16位数字和字母组合)}, {"description", "附加 _ 加 16 位随机数字字母。"}},
            New Dictionary(Of String, Object) From {{"name", NameOf(预设数据_v6.自动命名选项.附加_2位结尾序号)}, {"value", CInt(预设数据_v6.自动命名选项.附加_2位结尾序号)}, {"description", "直接在文件名末尾附加 01、02；只在同输出后缀目标存在时递增，不自带空格或下划线。"}},
            New Dictionary(Of String, Object) From {{"name", NameOf(预设数据_v6.自动命名选项.附加_3位结尾序号)}, {"value", CInt(预设数据_v6.自动命名选项.附加_3位结尾序号)}, {"description", "直接在文件名末尾附加 001、002；只在同输出后缀目标存在时递增，不自带空格或下划线。"}}
        }
    End Function

    Private Shared Function BuildVideoParameterListCandidates(preset As 预设数据_v6, role As 视频编码器数据库_v6.编码器参数角色) As List(Of Object)
        Dim encoder = 视频编码器数据库_v6.获取编码器数据(preset.视频参数_编码器_具体编码)
        Dim data As 视频编码器数据库_v6.编码器参数列表数据 = Nothing
        If encoder Is Nothing Then Return New List(Of Object)

        Select Case role
            Case 视频编码器数据库_v6.编码器参数角色.编码预设
                data = encoder.编码预设
            Case 视频编码器数据库_v6.编码器参数角色.配置文件
                data = encoder.配置文件
            Case 视频编码器数据库_v6.编码器参数角色.场景优化
                data = encoder.场景优化
            Case 视频编码器数据库_v6.编码器参数角色.像素格式
                data = encoder.像素格式
        End Select

        If data Is Nothing Then Return New List(Of Object)
        Dim result As New List(Of Object) From {""}
        If data.默认值 <> "" Then
            result.Add(New Dictionary(Of String, Object) From {
                {"value", data.默认值},
                {"is_default", True}
            })
        End If
        For Each value In data.值列表
            Dim candidate As New Dictionary(Of String, Object) From {
                {"value", value}
            }
            Dim description As String = Nothing
            If data.值说明 IsNot Nothing AndAlso data.值说明.TryGetValue(value, description) AndAlso description <> "" Then candidate("description") = description
            If Not result.OfType(Of Dictionary(Of String, Object)).Any(Function(x) String.Equals(CStr(x("value")), value, StringComparison.OrdinalIgnoreCase)) Then result.Add(candidate)
        Next
        Return result
    End Function

    Private Shared Function GetKnownParameterRules(fieldName As String) As List(Of String)
        Select Case fieldName
            Case NameOf(预设数据_v6.输出容器)
                Return New List(Of String) From {"后缀必须包含点号，例如 .mp4；空字符串表示不指定或由输出路径决定。", "候选值来自当前输出文件设置页的后缀菜单，会随菜单项增删变化；菜单外的自定义后缀也可填写。"}
            Case NameOf(预设数据_v6.输出位置)
                Return New List(Of String) From {"必须是已存在文件夹才会作为自定义输出目录生效；空字符串表示默认输出到输入文件原目录。", "设置为默认原目录时必须同时清空 输出位置_保留子文件夹结构起始点。"}
            Case NameOf(预设数据_v6.输出位置_保留子文件夹结构起始点)
                Return New List(Of String) From {"必须是已存在文件夹；路径不存在时界面会清空。", "只有 输出位置 是有效自定义输出目录时才生效；默认原目录与此逻辑不兼容。", "它保存到预设时属于额外保存输出位置的一部分；调用 save_parameter_preset 保存时需要传 save_output_location=true。"}
            Case NameOf(预设数据_v6.输出_自动命名选项)
                Return New List(Of String) From {"可传枚举名或数值。默认 附加_递增时间戳 会替换文件名结尾已有的 _yyyy.MM.dd-HH.mm.ss，否则追加新时间戳。", "附加_2位结尾序号 和 附加_3位结尾序号 始终从 01/001 开始递增，判断已占用序号时只检查相同输出后缀，且直接附加在扩展名前，不包含空格、下划线或其他分隔符。"}
            Case NameOf(预设数据_v6.输出命名_结尾文本)
                Return New List(Of String) From {"若希望结尾序号前有空格、下划线或其他分隔符，需要把分隔符写在此字段；2 位/3 位结尾序号功能只负责直接拼数字。"}
            Case NameOf(预设数据_v6.视频参数_质量控制_参数名)
                Return New List(Of String) From {"面板显示带横杠的 FFmpeg 参数名；保存预设时会自动去掉开头横杠，changes 中可传 crf 或 -crf。"}
            Case NameOf(预设数据_v6.视频参数_色彩管理_像素格式)
                Return New List(Of String) From {"此字段对应可编辑下拉框，候选取决于当前视频编码器，只代表编码器数据库中的常见或建议值。", "可填写 FFmpeg/当前编码器支持的其他像素格式；生成命令时会写为 -pix_fmt <值>，changes 中只传像素格式值，不要带 -pix_fmt。"}
            Case NameOf(预设数据_v6.视频参数_色彩管理_像素格式预先转换)
                Return New List(Of String) From {"此字段对应可编辑下拉框，候选值只是常用建议，会随控件 Items 增删变化。", "可填写任意 FFmpeg 支持的像素格式；生成命令时会写为 format=<值>，changes 中只传像素格式值，不要带 format=。"}
            Case NameOf(预设数据_v6.音频参数_编码器_代号)
                Return New List(Of String) From {"changes 中优先传 value 的私有 ID，不要传显示名称；界面会自动显示对应名称。"}
            Case NameOf(预设数据_v6.音频参数_质量参数名),
                 NameOf(预设数据_v6.音频参数_质量参数名2)
                Return New List(Of String) From {"音频质量参数名保存时保留横杠，例如 -q:a、-b:a。", "第二组音频质量参数会在命令中紧跟第一组之后写入，可用于同时设置两个编码器参数，例如 -vbr 与 -aac_nmr_speed。"}
        End Select
        Return New List(Of String)
    End Function

    Private Shared Function GetKnownParameterNotes(fieldName As String, preset As 预设数据_v6) As List(Of String)
        Dim notes As New List(Of String)
        Select Case fieldName
            Case NameOf(预设数据_v6.输出位置)
                notes.Add($"当前 {NameOf(预设数据_v6.输出位置_保留子文件夹结构起始点)} 为：{preset.输出位置_保留子文件夹结构起始点}")
            Case NameOf(预设数据_v6.输出位置_保留子文件夹结构起始点)
                notes.Add($"当前 {NameOf(预设数据_v6.输出位置)} 为：{preset.输出位置}")
                notes.Add("Agent 修改此字段后应重读 get_parameter_panel_state 验证；如果输出位置为默认原目录或路径不存在，界面会清空它。")
            Case NameOf(预设数据_v6.输出_自动命名选项)
                notes.Add($"当前开头/替代/结尾文本为：{preset.输出命名_开头文本} / {preset.输出命名_替代文本} / {preset.输出命名_结尾文本}")
            Case NameOf(预设数据_v6.视频参数_编码器_分类名称)
                notes.Add($"候选取决于 {NameOf(预设数据_v6.视频参数_编码器_类型)}，当前为 {preset.视频参数_编码器_类型}。")
            Case NameOf(预设数据_v6.视频参数_编码器_具体编码)
                notes.Add($"候选取决于 {NameOf(预设数据_v6.视频参数_编码器_分类名称)}，当前为 {preset.视频参数_编码器_分类名称}。")
            Case NameOf(预设数据_v6.视频参数_编码器_编码预设),
                 NameOf(预设数据_v6.视频参数_编码器_配置文件),
                 NameOf(预设数据_v6.视频参数_编码器_场景优化),
                 NameOf(预设数据_v6.视频参数_色彩管理_像素格式)
                notes.Add($"候选取决于 {NameOf(预设数据_v6.视频参数_编码器_具体编码)}，当前为 {preset.视频参数_编码器_具体编码}。")
        End Select
        Return notes
    End Function

    Private Shared Function ApplyTopLevelChanges(preset As 预设数据_v6, changes As JsonElement) As List(Of Dictionary(Of String, Object))
        Dim type = GetType(预设数据_v6)
        Dim props = type.GetProperties().Where(Function(x) x.CanRead AndAlso x.CanWrite).ToList()
        Dim result As New List(Of Dictionary(Of String, Object))
        For Each item In changes.EnumerateObject()
            Dim prop = ResolvePresetProperty(item.Name, props)
            If prop Is Nothing Then
                Dim suggestions = GetPropertySuggestions(item.Name, props)
                Dim suffix = If(suggestions.Count = 0, "", "。相近字段：" & String.Join("、", suggestions))
                Throw New InvalidOperationException($"未知或不可写属性：{item.Name}{suffix}")
            End If
            Dim before = prop.GetValue(preset)
            Dim value = DeserializeJsonElement(item.Value, prop.PropertyType)
            prop.SetValue(preset, value)
            result.Add(New Dictionary(Of String, Object) From {
                {"field", prop.Name},
                {"before", before},
                {"requested", value}
            })
        Next
        Return result
    End Function

    Private Shared Function DeserializeJsonElement(element As JsonElement, targetType As Type) As Object
        If element.ValueKind = JsonValueKind.Null OrElse element.ValueKind = JsonValueKind.Undefined Then
            Return GetDefaultJsonPatchValue(targetType)
        End If

        If targetType Is GetType(String) Then
            If element.ValueKind = JsonValueKind.String Then Return element.GetString()
            Return element.GetRawText()
        End If
        If targetType Is GetType(Boolean) Then
            If element.ValueKind = JsonValueKind.True OrElse element.ValueKind = JsonValueKind.False Then Return element.GetBoolean()
            If element.ValueKind = JsonValueKind.String Then
                Dim boolValue As Boolean
                If Boolean.TryParse(element.GetString(), boolValue) Then Return boolValue
            End If
            If element.ValueKind = JsonValueKind.Number Then
                Dim intValue As Integer
                If element.TryGetInt32(intValue) Then Return intValue <> 0
            End If
            Throw New InvalidOperationException("布尔字段必须传 true/false。")
        End If
        If targetType Is GetType(Integer) Then
            If element.ValueKind = JsonValueKind.Number Then Return element.GetInt32()
            If element.ValueKind = JsonValueKind.String Then
                Dim intValue As Integer
                If Integer.TryParse(element.GetString(), intValue) Then Return intValue
            End If
            Throw New InvalidOperationException("整数字段必须传数字或可解析的数字字符串。")
        End If
        If targetType Is GetType(Double) Then
            If element.ValueKind = JsonValueKind.Number Then Return element.GetDouble()
            If element.ValueKind = JsonValueKind.String Then
                Dim doubleValue As Double
                If Double.TryParse(element.GetString(), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture, doubleValue) AndAlso Double.IsFinite(doubleValue) Then Return doubleValue
            End If
            Throw New InvalidOperationException("数字字段必须传数字或可解析的数字字符串。")
        End If
        If targetType.IsEnum Then
            Dim text = If(element.ValueKind = JsonValueKind.String, element.GetString(), element.GetRawText())
            Try
                Dim value = [Enum].Parse(targetType, text, True)
                If Not targetType.IsDefined(GetType(FlagsAttribute), False) AndAlso Not [Enum].IsDefined(targetType, value) Then Throw New ArgumentException()
                Return value
            Catch ex As ArgumentException
                Throw New InvalidOperationException($"枚举字段 {targetType.Name} 不包含值：{text}。可用值：{String.Join("、", [Enum].GetNames(targetType))}")
            End Try
        End If
        Return JsonSerializer.Deserialize(element.GetRawText(), targetType, JsonSO)
    End Function

    Private Shared Function GetDefaultJsonPatchValue(targetType As Type) As Object
        If targetType Is GetType(String) Then Return ""
        If Not targetType.IsValueType Then Return Nothing
        Return Activator.CreateInstance(targetType)
    End Function

    Private Shared Function ResolvePresetProperty(name As String, props As List(Of Reflection.PropertyInfo)) As Reflection.PropertyInfo
        If String.IsNullOrWhiteSpace(name) OrElse props Is Nothing Then Return Nothing
        Return props.FirstOrDefault(Function(x) String.Equals(x.Name, name, StringComparison.OrdinalIgnoreCase))
    End Function

    Private Shared Function GetPropertySuggestions(name As String, props As List(Of Reflection.PropertyInfo)) As List(Of String)
        If String.IsNullOrWhiteSpace(name) OrElse props Is Nothing Then Return New List(Of String)
        Return props.
            Select(Function(prop) New With {.Name = prop.Name, .Score = GetPropertyMatchScore(name, prop.Name)}).
            Where(Function(x) x.Score > 0).
            OrderByDescending(Function(x) x.Score).
            ThenBy(Function(x) x.Name, StringComparer.CurrentCultureIgnoreCase).
            Take(8).
            Select(Function(x) x.Name).
            ToList()
    End Function

    Private Shared Function GetPropertyMatchScore(query As String, propertyName As String) As Integer
        Dim q = If(query, "").Trim()
        Dim p = If(propertyName, "").Trim()
        If q = "" OrElse p = "" Then Return 0
        If String.Equals(q, p, StringComparison.OrdinalIgnoreCase) Then Return 10000
        If p.Contains(q, StringComparison.OrdinalIgnoreCase) Then Return 8000 + q.Length
        If q.Contains(p, StringComparison.OrdinalIgnoreCase) Then Return 7000 + p.Length

        Dim qParts = q.Split({"_"c, " "c, ControlChars.Tab}, StringSplitOptions.RemoveEmptyEntries)
        Dim score = 0
        For Each part In qParts
            If part.Length > 0 AndAlso p.Contains(part, StringComparison.OrdinalIgnoreCase) Then score += 200 + part.Length
        Next

        Dim common = CountCommonCharacters(q, p)
        If common >= Math.Min(3, Math.Min(q.Length, p.Length)) Then score += common
        Return score
    End Function

    Private Shared Function CountCommonCharacters(a As String, b As String) As Integer
        Dim setB = New HashSet(Of Char)(If(b, "").ToLowerInvariant())
        Dim count = 0
        For Each ch In If(a, "").ToLowerInvariant()
            If setB.Contains(ch) Then count += 1
        Next
        Return count
    End Function

    Private Shared Function JsonObjectHasProperty(json As String, propertyName As String) As Boolean
        If String.IsNullOrWhiteSpace(json) OrElse String.IsNullOrWhiteSpace(propertyName) Then Return False
        Try
            Using doc = JsonDocument.Parse(json)
                If doc.RootElement.ValueKind <> JsonValueKind.Object Then Return False
                Dim value As JsonElement
                Return doc.RootElement.TryGetProperty(propertyName, value)
            End Using
        Catch
            Return False
        End Try
    End Function

    Private Shared Function ClonePresetData(source As 预设数据_v6) As 预设数据_v6
        If source Is Nothing Then Return New 预设数据_v6
        Return JsonSerializer.Deserialize(Of 预设数据_v6)(JsonSerializer.Serialize(source, JsonSO), JsonSO)
    End Function

    Private Shared Function BuildParameterOverview(preset As 预设数据_v6) As String
        Using box As New LakeUI.ModernTextBox
            预设管理_v6.显示参数总览(box, preset)
            Return box.Text
        End Using
    End Function

    Private Shared Function BuildParameterCommandPreview(preset As 预设数据_v6) As String
        Return 预设管理_v6.生成命令行展示文本(preset, 预设管理_v6.输入占位符, 预设管理_v6.输出占位符)
    End Function

    Private Shared Function BuildChangedFieldList(beforePreset As 预设数据_v6, afterPreset As 预设数据_v6) As List(Of String)
        Dim result As New List(Of String)
        If beforePreset Is Nothing OrElse afterPreset Is Nothing Then Return result
        For Each prop In GetType(预设数据_v6).GetProperties().Where(Function(x) x.CanRead)
            Dim beforeValue = prop.GetValue(beforePreset)
            Dim afterValue = prop.GetValue(afterPreset)
            If Not JsonValuesEqual(beforeValue, afterValue) Then result.Add(prop.Name)
        Next
        Return result
    End Function

    Private Shared Function JsonValuesEqual(left As Object, right As Object) As Boolean
        Return String.Equals(JsonSerializer.Serialize(left, JsonSO), JsonSerializer.Serialize(right, JsonSO), StringComparison.Ordinal)
    End Function
End Class
