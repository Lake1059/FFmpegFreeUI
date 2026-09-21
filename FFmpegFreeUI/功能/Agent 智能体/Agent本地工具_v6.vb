Imports System.Drawing.Imaging
Imports System.IO
Imports System.Net
Imports System.Net.Http
Imports System.Text
Imports System.Text.Json
Imports System.Text.RegularExpressions

Partial Public Class AgentLocalTools
    Public Const PermissionSafe As Integer = 0
    Public Const PermissionEnvironment As Integer = 1
    Public Const PermissionSystem As Integer = 2

    Private Shared ReadOnly ToolJsonOptions As New JsonSerializerOptions(JsonSO) With {.WriteIndented = False}

    <CodeAnalysis.SuppressMessage("Performance", "CA1861:不要将常量数组作为参数", Justification:="<挂起>")>
    Public Shared Function BuildToolDefinitions(permissionLevel As Integer, networkMode As Integer) As List(Of Dictionary(Of String, Object))
        Dim tools As New List(Of Dictionary(Of String, Object)) From {
            FunctionTool("list_agent_skills", "列出 3FUI Agent 内置 skill 资料库及可按需读取的 reference。遇到 3FUI 环境机制、工具行为、参数面板、命令生成、队列、预设、联网权限或编码推荐问题时先用它查看索引。", New Dictionary(Of String, Object)),
            FunctionTool("read_agent_skill_reference", "读取 3FUI Agent 内置 skill 资料库中的指定 reference。只读取与当前任务相关的章节，避免一次加载全部资料。", New Dictionary(Of String, Object) From {
            {"skill", New Dictionary(Of String, Object) From {{"type", "string"}, {"description", "skill 名称，默认 ffmpegfreeui"}}},
            {"reference", New Dictionary(Of String, Object) From {{"type", "string"}, {"description", "reference 名称，例如 SKILL.md 或 references/parameter-panel.md"}}}
        }, {"reference"}),
            FunctionTool("get_parameter_panel_state", "读取当前参数面板，默认仅返回 overview。需要命令验证或完整预设时按需开启对应 include_*；关闭全部选项会报错。", BuildParameterResultProperties(True, False)),
            FunctionTool("get_parameter_field_info", "按字段名或关键词查询参数面板字段的类型、当前值、候选值和格式规则。填写不熟悉的参数前优先调用，避免猜字段值。", New Dictionary(Of String, Object) From {
            {"fields", New Dictionary(Of String, Object) From {{"type", "array"}, {"items", New Dictionary(Of String, Object) From {{"type", "string"}}}}},
            {"query", New Dictionary(Of String, Object) From {{"type", "string"}, {"description", "可选关键词，用于模糊查找字段名"}}},
            {"include_current_values", New Dictionary(Of String, Object) From {{"type", "boolean"}}}
        }),
            FunctionTool("apply_parameter_panel_patch", "修改参数面板，默认返回变更结果和命令预览；总览、完整预设按需开启 include_*。优先传 changes 对象，键为 预设数据_v6 属性名；也可传 preset_json 应用完整预设。修改 滤镜排序系统 必须传完整排序列表，删除内置滤镜会清空对应参数页。", BuildParameterResultProperties(False, True, New Dictionary(Of String, Object) From {
            {"changes", New Dictionary(Of String, Object) From {{"type", "object"}, {"additionalProperties", True}}},
            {"preset_json", New Dictionary(Of String, Object) From {{"type", "string"}}},
            {"note", New Dictionary(Of String, Object) From {{"type", "string"}}}
        }))
        }

        If permissionLevel >= PermissionEnvironment Then
            tools.Add(FunctionTool("get_queue_summary", "读取 3FUI 编码队列任务信息及活动任务的性能占用。性能采样独立于任务日志窗口；可用 id/ids 或 1-based index/indexes 查询指定任务，detail=true 返回完整任务信息，target=all 返回全部。include_commands=true 会同时返回参数预览命令行和实际执行命令行；日志请另用 get_queue_task_logs。", New Dictionary(Of String, Object) From {
                {"id", New Dictionary(Of String, Object) From {{"type", "string"}, {"description", "单个任务 ID"}}},
                {"ids", New Dictionary(Of String, Object) From {{"type", "array"}, {"items", New Dictionary(Of String, Object) From {{"type", "string"}}}, {"description", "任务 ID 列表"}}},
                {"index", New Dictionary(Of String, Object) From {{"type", "integer"}, {"description", "队列中的 1-based 序号"}}},
                {"indexes", New Dictionary(Of String, Object) From {{"type", "array"}, {"items", New Dictionary(Of String, Object) From {{"type", "integer"}}}, {"description", "队列中的 1-based 序号列表"}}},
                {"target", New Dictionary(Of String, Object) From {{"type", "string"}, {"description", "all/全部 表示读取全部任务；不传目标时默认读取全部摘要"}}},
                {"detail", New Dictionary(Of String, Object) From {{"type", "boolean"}, {"description", "是否返回完整任务信息；查询指定任务时默认 true"}}},
                {"include_performance", New Dictionary(Of String, Object) From {{"type", "boolean"}, {"description", "是否返回活动进程的 CPU、内存和 GPU 占用，默认 true；无需打开任务日志窗口"}}},
                {"include_commands", New Dictionary(Of String, Object) From {{"type", "boolean"}, {"description", "是否附带可执行命令行，默认 false"}}},
                {"include_preset_json", New Dictionary(Of String, Object) From {{"type", "boolean"}, {"description", "是否附带任务预设 JSON，可能很大，默认 false"}}},
                {"offset", New Dictionary(Of String, Object) From {{"type", "integer"}, {"description", "target=all 时跳过的任务数，默认 0"}}},
                {"limit", New Dictionary(Of String, Object) From {{"type", "integer"}, {"description", "target=all 时最多返回的任务数；0 表示不限制"}}}
            }))
            tools.Add(FunctionTool("get_queue_task_logs", "读取指定编码队列任务日志。先用 get_queue_summary 获取任务 ID 或序号；默认一次返回四档：all、latest_non_progress、errors、current_stage。", New Dictionary(Of String, Object) From {
                {"id", New Dictionary(Of String, Object) From {{"type", "string"}, {"description", "单个任务 ID"}}},
                {"ids", New Dictionary(Of String, Object) From {{"type", "array"}, {"items", New Dictionary(Of String, Object) From {{"type", "string"}}}, {"description", "任务 ID 列表"}}},
                {"index", New Dictionary(Of String, Object) From {{"type", "integer"}, {"description", "队列中的 1-based 序号"}}},
                {"indexes", New Dictionary(Of String, Object) From {{"type", "array"}, {"items", New Dictionary(Of String, Object) From {{"type", "integer"}}}, {"description", "队列中的 1-based 序号列表"}}},
                {"target", New Dictionary(Of String, Object) From {{"type", "string"}, {"description", "all/全部 表示读取全部任务日志，可能很大"}}},
                {"mode", New Dictionary(Of String, Object) From {{"type", "string"}, {"description", "单个日志档位：all、latest_non_progress、errors、current_stage；不传则返回四档"}}},
                {"modes", New Dictionary(Of String, Object) From {{"type", "array"}, {"items", New Dictionary(Of String, Object) From {{"type", "string"}}}, {"description", "多个日志档位；可传 all_modes 返回四档"}}},
                {"log_limit", New Dictionary(Of String, Object) From {{"type", "integer"}, {"description", "每个任务每档返回的最新日志条数，1-200，默认 20"}}}
            }))
            tools.Add(FunctionTool("control_queue_tasks", "控制 3FUI 编码队列任务。action 支持 start/pause/resume/stop/remove/reset；用 id/ids 或 1-based index/indexes 指定任务，或 target=all 控制全部任务。停止或移除全部只应在用户明确要求时使用。", New Dictionary(Of String, Object) From {
                {"action", New Dictionary(Of String, Object) From {{"type", "string"}, {"description", "start、pause、resume、stop、remove、reset"}}},
                {"id", New Dictionary(Of String, Object) From {{"type", "string"}, {"description", "单个任务 ID"}}},
                {"ids", New Dictionary(Of String, Object) From {{"type", "array"}, {"items", New Dictionary(Of String, Object) From {{"type", "string"}}}, {"description", "任务 ID 列表"}}},
                {"index", New Dictionary(Of String, Object) From {{"type", "integer"}, {"description", "队列中的 1-based 序号"}}},
                {"indexes", New Dictionary(Of String, Object) From {{"type", "array"}, {"items", New Dictionary(Of String, Object) From {{"type", "integer"}}}, {"description", "队列中的 1-based 序号列表"}}},
                {"target", New Dictionary(Of String, Object) From {{"type", "string"}, {"description", "all/全部 表示控制全部任务"}}},
                {"detail", New Dictionary(Of String, Object) From {{"type", "boolean"}, {"description", "返回控制前后详情，默认 false"}}}
            }, {"action"}))
            tools.Add(FunctionTool("sync_parameter_panel_to_queue", "用当前参数面板完整覆盖指定未处理任务的预设快照。必须传 id/ids/index/indexes，不支持全部目标。局部修改使用 patch_queue_task_presets。", BuildQueuePresetProperties(False)))
            tools.Add(FunctionTool("patch_queue_task_presets", "只修改指定未处理任务快照的 changes 字段，保留各任务其他选项，不修改参数面板。字段名用 get_parameter_field_info 查询；滤镜排序须传完整列表，删除内置滤镜会清空对应参数。", BuildQueuePresetProperties(True), {"changes"}))
            tools.Add(FunctionTool("get_ui_tabs", "读取 3FUI 主页面、参数面板、集成工具或嵌套页的选项卡列表和当前选中项。", New Dictionary(Of String, Object) From {
                {"scope", New Dictionary(Of String, Object) From {{"type", "string"}, {"description", "main、parameter、integrated、settings、custom_parameters、attachments"}}}
            }))
            tools.Add(FunctionTool("switch_ui_tab", "切换 3FUI 竖向选项卡或相关嵌套选项卡。", New Dictionary(Of String, Object) From {
                {"scope", New Dictionary(Of String, Object) From {{"type", "string"}}},
                {"tab", New Dictionary(Of String, Object) From {{"type", "string"}, {"description", "选项卡索引或文本"}}}
            }, {"scope", "tab"}))
            tools.Add(FunctionTool("get_prepare_files", "读取准备文件页面的文件列表。", New Dictionary(Of String, Object)))
            tools.Add(FunctionTool("set_prepare_files", "追加、替换或清空准备文件页面的文件列表。", New Dictionary(Of String, Object) From {
                {"paths", New Dictionary(Of String, Object) From {{"type", "array"}, {"items", New Dictionary(Of String, Object) From {{"type", "string"}}}}},
                {"mode", New Dictionary(Of String, Object) From {{"type", "string"}, {"description", "append、replace 或 clear，默认 append"}}}
            }))
            tools.Add(FunctionTool("submit_prepare_files_to_queue", "将准备文件页面中的文件按当前参数面板设置加入编码队列。", New Dictionary(Of String, Object)))
            tools.Add(FunctionTool("get_integrated_tool_state", "读取集成工具页面中合并、混流或抽流的当前状态。", New Dictionary(Of String, Object) From {
                {"tool", New Dictionary(Of String, Object) From {{"type", "string"}, {"description", "merge/合并、mux/混流、extract/抽流"}}}
            }, {"tool"}))
            tools.Add(FunctionTool("configure_integrated_tool", "配置集成工具页面。合并使用 files/output/mode；混流 files 可为对象数组；抽流使用 file/output_location/selected_streams。", New Dictionary(Of String, Object) From {
                {"tool", New Dictionary(Of String, Object) From {{"type", "string"}}},
                {"payload", New Dictionary(Of String, Object) From {{"type", "object"}, {"additionalProperties", True}}}
            }, {"tool", "payload"}))
            tools.Add(FunctionTool("run_integrated_tool", "运行集成工具页面的当前配置。合并/混流添加到编码队列，抽流直接执行。", New Dictionary(Of String, Object) From {
                {"tool", New Dictionary(Of String, Object) From {{"type", "string"}}},
                {"payload", New Dictionary(Of String, Object) From {{"type", "object"}, {"additionalProperties", True}}}
            }, {"tool"}))
            tools.Add(FunctionTool("get_system_hardware", "读取系统硬件概要：处理器名称、内存、显卡名称。", New Dictionary(Of String, Object)))
            tools.Add(FunctionTool("get_parameter_panel_controls", "读取参数面板上的用户直接操作控件和说明控件，包括控件名、类型、文本、当前值、候选项等。", New Dictionary(Of String, Object) From {
                {"query", New Dictionary(Of String, Object) From {{"type", "string"}, {"description", "可选控件名或文本关键词，例如 MCB_视频编码器分类、MCB_输出位置、HCL_质量"}}}
            }))
            tools.Add(FunctionTool("list_parameter_presets", "列出参数面板预设，来源可为用户自定义、从社区下载、开发者内置。", New Dictionary(Of String, Object) From {
                {"source", New Dictionary(Of String, Object) From {{"type", "string"}}}
            }))
            tools.Add(FunctionTool("read_parameter_preset", "读取指定参数预设的 JSON、备注、总览和命令行预览。", New Dictionary(Of String, Object) From {
                {"source", New Dictionary(Of String, Object) From {{"type", "string"}}},
                {"name", New Dictionary(Of String, Object) From {{"type", "string"}}}
            }, {"source", "name"}))
            tools.Add(FunctionTool("apply_parameter_preset", "将指定参数预设加载到参数面板。", New Dictionary(Of String, Object) From {
                {"source", New Dictionary(Of String, Object) From {{"type", "string"}}},
                {"name", New Dictionary(Of String, Object) From {{"type", "string"}}}
            }, {"source", "name"}))
            tools.Add(FunctionTool("save_parameter_preset", "保存参数预设到用户自定义或从社区下载来源。删除权限禁用；开发者内置只允许读取。save_output_location=true 时保存输出位置及保留子文件夹结构起始点；false 时清空这些输出位置字段；不传则沿用界面勾选状态。", New Dictionary(Of String, Object) From {
                {"source", New Dictionary(Of String, Object) From {{"type", "string"}}},
                {"name", New Dictionary(Of String, Object) From {{"type", "string"}}},
                {"preset_json", New Dictionary(Of String, Object) From {{"type", "string"}}},
                {"note", New Dictionary(Of String, Object) From {{"type", "string"}}},
                {"save_output_location", New Dictionary(Of String, Object) From {{"type", "boolean"}, {"description", "是否写入 输出位置 和 输出位置_保留子文件夹结构起始点；不传则使用预设管理页的额外保存输出位置勾选状态。"}}}
            }, {"source", "name"}))
        End If

        Select Case AgentNetworkMode.Normalize(networkMode)
            Case AgentNetworkMode.Endpoint
                tools.Add(FunctionTool("web_search", "联网搜索。使用端点原生 web_search_preview。", New Dictionary(Of String, Object) From {
                    {"query", New Dictionary(Of String, Object) From {{"type", "string"}, {"description", "要搜索的问题"}}}
                }, {"query"}))
            Case AgentNetworkMode.Local
                tools.Add(FunctionTool("web_search", "联网搜索。由你选择搜索引擎，并使用 engine_url 指定完整的搜索 URL，或使用 {query} 作为查询词占位符，例如 https://www.google.com/search?q={query}。", New Dictionary(Of String, Object) From {
                    {"query", New Dictionary(Of String, Object) From {{"type", "string"}, {"description", "要搜索的问题"}}},
                    {"engine_url", New Dictionary(Of String, Object) From {{"type", "string"}, {"description", "自行选择的 HTTP/HTTPS 搜索引擎 URL。必须是已包含查询词的完整 URL，或使用 {query} 占位符。"}}}
                }, {"query", "engine_url"}))
                tools.Add(FunctionTool("fetch_url", "读取指定 URL 的文本或 JSON。请求由 3FUI 在本机发起；可按需填写 User-Agent、Referer、Cookie、自定义请求头、请求方法和请求体。", New Dictionary(Of String, Object) From {
                    {"url", New Dictionary(Of String, Object) From {{"type", "string"}}},
                    {"method", New Dictionary(Of String, Object) From {{"type", "string"}, {"description", "GET、POST、PUT、PATCH、DELETE 等 HTTP 方法，默认 GET"}}},
                    {"headers", New Dictionary(Of String, Object) From {{"type", "object"}, {"additionalProperties", New Dictionary(Of String, Object) From {{"type", "string"}}}}},
                    {"user_agent", New Dictionary(Of String, Object) From {{"type", "string"}}},
                    {"referer", New Dictionary(Of String, Object) From {{"type", "string"}}},
                    {"cookies", New Dictionary(Of String, Object) From {{"type", "string"}}},
                    {"body", New Dictionary(Of String, Object) From {{"type", "string"}}},
                    {"response_format", New Dictionary(Of String, Object) From {{"type", "string"}, {"description", "auto、text、json、raw；默认 auto"}}},
                    {"max_chars", New Dictionary(Of String, Object) From {{"type", "integer"}, {"description", "响应文本最大字符数，1-200000，默认 12000"}}}
                }, {"url"}))
                tools.Add(FunctionTool("http_request", "发起通用 HTTP 请求并返回状态码、响应头、内容类型和响应体摘要。用于 API、JSON、XML 或需要自定义身份标识的站点。", New Dictionary(Of String, Object) From {
                    {"url", New Dictionary(Of String, Object) From {{"type", "string"}}},
                    {"method", New Dictionary(Of String, Object) From {{"type", "string"}}},
                    {"headers", New Dictionary(Of String, Object) From {{"type", "object"}, {"additionalProperties", New Dictionary(Of String, Object) From {{"type", "string"}}}}},
                    {"user_agent", New Dictionary(Of String, Object) From {{"type", "string"}}},
                    {"referer", New Dictionary(Of String, Object) From {{"type", "string"}}},
                    {"cookies", New Dictionary(Of String, Object) From {{"type", "string"}}},
                    {"body", New Dictionary(Of String, Object) From {{"type", "string"}}},
                    {"response_format", New Dictionary(Of String, Object) From {{"type", "string"}}},
                    {"max_chars", New Dictionary(Of String, Object) From {{"type", "integer"}}}
                }, {"url"}))
        End Select

        If permissionLevel >= PermissionSystem Then
            tools.Add(FunctionTool("read_local_text_file", "读取本地文本文件。仅系统访问权限可用，支持按行读取，避免把大文件一次性放入上下文。", New Dictionary(Of String, Object) From {
                {"path", New Dictionary(Of String, Object) From {{"type", "string"}}},
                {"start_line", New Dictionary(Of String, Object) From {{"type", "integer"}, {"description", "1-based 起始行，默认 1"}}},
                {"line_count", New Dictionary(Of String, Object) From {{"type", "integer"}, {"description", "最多读取行数，默认读取到文件末尾，最多 10000 行"}}},
                {"max_chars", New Dictionary(Of String, Object) From {{"type", "integer"}, {"description", "最大返回字符数，1-200000，默认 20000"}}}
            }, {"path"}))
            tools.Add(FunctionTool("write_local_text_file", "写入本地文本文件，使用临时文件替换保证写入过程不会留下半截文件；必要时可创建父目录。仅系统访问权限可用。", New Dictionary(Of String, Object) From {
                {"path", New Dictionary(Of String, Object) From {{"type", "string"}}},
                {"content", New Dictionary(Of String, Object) From {{"type", "string"}}},
                {"encoding", New Dictionary(Of String, Object) From {{"type", "string"}, {"description", "utf-8、utf-8-bom、utf-16，默认 utf-8"}}},
                {"create_directories", New Dictionary(Of String, Object) From {{"type", "boolean"}}}
            }, {"path", "content"}))
            tools.Add(FunctionTool("apply_local_text_patch", "对本地文本文件执行精确 old_text 到 new_text 替换，默认要求恰好匹配一次；用于安全编辑代码而不需要传输 Base64。", New Dictionary(Of String, Object) From {
                {"path", New Dictionary(Of String, Object) From {{"type", "string"}}},
                {"old_text", New Dictionary(Of String, Object) From {{"type", "string"}}},
                {"new_text", New Dictionary(Of String, Object) From {{"type", "string"}}},
                {"replace_all", New Dictionary(Of String, Object) From {{"type", "boolean"}}},
                {"expected_replacements", New Dictionary(Of String, Object) From {{"type", "integer"}}}
            }, {"path", "old_text", "new_text"}))
            tools.Add(FunctionTool("list_directory", "列举本地目录，支持递归和条数上限。仅系统访问权限可用。", New Dictionary(Of String, Object) From {
                {"path", New Dictionary(Of String, Object) From {{"type", "string"}}},
                {"recursive", New Dictionary(Of String, Object) From {{"type", "boolean"}}},
                {"max_items", New Dictionary(Of String, Object) From {{"type", "integer"}}}
            }, {"path"}))
            tools.Add(FunctionTool("create_directory", "创建本地目录（包含不存在的父目录）。仅系统访问权限可用。", New Dictionary(Of String, Object) From {
                {"path", New Dictionary(Of String, Object) From {{"type", "string"}}}
            }, {"path"}))
            tools.Add(FunctionTool("copy_local_file", "复制本地文件。仅系统访问权限可用；目标已存在时默认拒绝覆盖。", New Dictionary(Of String, Object) From {
                {"source", New Dictionary(Of String, Object) From {{"type", "string"}}},
                {"destination", New Dictionary(Of String, Object) From {{"type", "string"}}},
                {"overwrite", New Dictionary(Of String, Object) From {{"type", "boolean"}}}
            }, {"source", "destination"}))
            tools.Add(FunctionTool("move_local_path", "移动本地文件或目录。仅系统访问权限可用；目标已存在时默认拒绝覆盖。", New Dictionary(Of String, Object) From {
                {"source", New Dictionary(Of String, Object) From {{"type", "string"}}},
                {"destination", New Dictionary(Of String, Object) From {{"type", "string"}}},
                {"overwrite", New Dictionary(Of String, Object) From {{"type", "boolean"}}}
            }, {"source", "destination"}))
            tools.Add(FunctionTool("delete_local_path", "删除本地文件或目录。为了避免误删，必须明确传 confirm=true；文件会优先移入回收站。仅系统访问权限可用。", New Dictionary(Of String, Object) From {
                {"path", New Dictionary(Of String, Object) From {{"type", "string"}}},
                {"confirm", New Dictionary(Of String, Object) From {{"type", "boolean"}}}
            }, {"path", "confirm"}))
            tools.Add(FunctionTool("get_image_info", "读取本地图片的宽高、格式和大小，不返回图片 Base64。仅系统访问权限可用。", New Dictionary(Of String, Object) From {
                {"path", New Dictionary(Of String, Object) From {{"type", "string"}}}
            }, {"path"}))
            AddConsoleToolDefinitions(tools)
        End If

        Return tools.
            Where(Function(tool) tool IsNot Nothing).
            GroupBy(Function(tool) GetToolName(tool), StringComparer.OrdinalIgnoreCase).
            Select(Function(group) group.First()).
            ToList()
    End Function

    Private Shared Function GetToolName(tool As Dictionary(Of String, Object)) As String
        If tool Is Nothing Then Return ""
        Dim functionValue As Object = Nothing
        If Not tool.TryGetValue("function", functionValue) Then Return ""
        Dim functionData = TryCast(functionValue, Dictionary(Of String, Object))
        If functionData Is Nothing Then Return ""
        Dim name As Object = Nothing
        If functionData.TryGetValue("name", name) Then Return If(name, "").ToString().Trim()
        Return ""
    End Function

    Private Shared Sub AddConsoleToolDefinitions(tools As List(Of Dictionary(Of String, Object)))
        tools.Add(FunctionTool("run_powershell", "运行 PowerShell 命令。仅系统访问权限可用；优先使用 PATH 中的 PowerShell 7 (pwsh.exe)，启动失败时自动回退 Windows PowerShell 5 (powershell.exe)。同一次用户消息触发的 Agent 运行会复用同一个 PowerShell 进程，变量、当前位置和模块导入可在本轮多次调用之间保留；会话启动时强制标准输入、标准输出、标准错误、$OutputEncoding 和带 Encoding 参数的文本 cmdlet 使用 UTF-8；本轮响应结束、超时或任务终止时会关闭进程。若本轮首次使用，先验证 $PSVersionTable.PSVersion 和 $PSVersionTable.PSEdition，再选择兼容语法；脚本读写文本仍需显式使用 -Encoding UTF8 或 .NET UTF8Encoding。", New Dictionary(Of String, Object) From {
            {"command", New Dictionary(Of String, Object) From {{"type", "string"}, {"description", "要执行的 PowerShell 命令"}}},
            {"working_directory", New Dictionary(Of String, Object) From {{"type", "string"}, {"description", "可选工作目录。首次调用默认使用程序目录；后续调用默认沿用当前 PowerShell 位置。"}}},
            {"timeout_seconds", New Dictionary(Of String, Object) From {{"type", "integer"}, {"description", "可选超时时间，1-300 秒，默认 60 秒"}}}
        }, {"command"}))
        tools.Add(FunctionTool("run_windows_executable", "直接运行 Windows 可执行文件并传递参数。仅系统访问权限可用；参数使用数组逐项传递，不经过 shell 拼接。返回 exit_code、stdout、stderr 和超时状态。", New Dictionary(Of String, Object) From {
            {"executable", New Dictionary(Of String, Object) From {{"type", "string"}, {"description", "可执行文件路径或 PATH 中的程序名，例如 ffmpeg.exe"}}},
            {"arguments", New Dictionary(Of String, Object) From {{"type", "array"}, {"items", New Dictionary(Of String, Object) From {{"type", "string"}}}, {"description", "按顺序传递给程序的参数，每一项单独填写，不要自行加引号"}}},
            {"working_directory", New Dictionary(Of String, Object) From {{"type", "string"}}},
            {"stdin", New Dictionary(Of String, Object) From {{"type", "string"}, {"description", "可选标准输入文本；传入后程序收到文本并关闭输入流"}}},
            {"timeout_seconds", New Dictionary(Of String, Object) From {{"type", "integer"}, {"description", "1-300 秒，默认 60 秒"}}},
            {"max_output_chars", New Dictionary(Of String, Object) From {{"type", "integer"}, {"description", "stdout/stderr 各自最大字符数，默认 12000"}}}
        }, {"executable"}))
    End Sub

    Public Shared Async Function ExecuteAsync(callInfo As AgentToolCallInfo,
                                              permissionLevel As Integer,
                                              networkMode As Integer,
                                              endpointClient As AgentEndpointClient,
                                              modelId As String,
                                              reasoningEffort As String,
                                              Optional powerShellSession As PowerShellRunSession = Nothing,
                                              Optional cancellationToken As Threading.CancellationToken = Nothing) As Task(Of String)
        Try
            cancellationToken.ThrowIfCancellationRequested()
            If callInfo Is Nothing Then Return "工具执行失败：缺少工具调用"
            Dim argumentParseError As String = ""
            Dim args = ParseToolArguments(callInfo, argumentParseError)
            If argumentParseError <> "" Then Return argumentParseError

            Select Case callInfo?.Name
                Case "list_agent_skills"
                    Return Agent技能资料库_v6.列出技能()
                Case "read_agent_skill_reference"
                    Return Agent技能资料库_v6.读取资料(Agent通用工具_v6.GetJsonString(args, "skill", "ffmpegfreeui"), Agent通用工具_v6.GetJsonString(args, "reference"))
                Case "get_parameter_panel_state"
                    Return GetParameterPanelState(args)
                Case "get_parameter_field_info"
                    Return GetParameterFieldInfo(args)
                Case "apply_parameter_panel_patch"
                    Return ApplyParameterPanelPatch(args)
                Case "get_queue_summary"
                    If permissionLevel < PermissionEnvironment Then Return "权限不足：需要环境控制"
                    Return GetQueueSummary(args)
                Case "get_queue_task_logs"
                    If permissionLevel < PermissionEnvironment Then Return "权限不足：需要环境控制"
                    Return GetQueueTaskLogs(args)
                Case "control_queue_tasks"
                    If permissionLevel < PermissionEnvironment Then Return "权限不足：需要环境控制"
                    Return ControlQueueTasks(args)
                Case "sync_parameter_panel_to_queue"
                    If permissionLevel < PermissionEnvironment Then Return "权限不足：需要环境控制"
                    Return SyncParameterPanelToQueue(args)
                Case "patch_queue_task_presets"
                    If permissionLevel < PermissionEnvironment Then Return "权限不足：需要环境控制"
                    Return SyncParameterPanelToQueue(args, True)
                Case "get_ui_tabs"
                    If permissionLevel < PermissionEnvironment Then Return "权限不足：需要环境控制"
                    Return Agent工具封装_v6.获取选项卡(Agent通用工具_v6.GetJsonString(args, "scope"))
                Case "switch_ui_tab"
                    If permissionLevel < PermissionEnvironment Then Return "权限不足：需要环境控制"
                    Return Agent工具封装_v6.切换选项卡(Agent通用工具_v6.GetJsonString(args, "scope"), Agent通用工具_v6.GetJsonString(args, "tab"))
                Case "get_prepare_files"
                    If permissionLevel < PermissionEnvironment Then Return "权限不足：需要环境控制"
                    Return Agent工具封装_v6.获取准备文件()
                Case "set_prepare_files"
                    If permissionLevel < PermissionEnvironment Then Return "权限不足：需要环境控制"
                    Return Agent工具封装_v6.设置准备文件(Agent通用工具_v6.GetJsonStringArray(args, "paths"), Agent通用工具_v6.GetJsonString(args, "mode"))
                Case "submit_prepare_files_to_queue"
                    If permissionLevel < PermissionEnvironment Then Return "权限不足：需要环境控制"
                    Return Agent工具封装_v6.提交准备文件到队列()
                Case "get_integrated_tool_state"
                    If permissionLevel < PermissionEnvironment Then Return "权限不足：需要环境控制"
                    Return Agent工具封装_v6.获取集成工具状态(Agent通用工具_v6.GetJsonString(args, "tool"))
                Case "configure_integrated_tool"
                    If permissionLevel < PermissionEnvironment Then Return "权限不足：需要环境控制"
                    Return Await Agent工具封装_v6.配置集成工具Async(Agent通用工具_v6.GetJsonString(args, "tool"), Agent通用工具_v6.GetJsonObject(args, "payload"))
                Case "run_integrated_tool"
                    If permissionLevel < PermissionEnvironment Then Return "权限不足：需要环境控制"
                    Return Await Agent工具封装_v6.运行集成工具Async(Agent通用工具_v6.GetJsonString(args, "tool"), Agent通用工具_v6.GetJsonObject(args, "payload"))
                Case "get_system_hardware"
                    If permissionLevel < PermissionEnvironment Then Return "权限不足：需要环境控制"
                    Return Agent工具封装_v6.获取系统硬件()
                Case "get_parameter_panel_controls"
                    If permissionLevel < PermissionEnvironment Then Return "权限不足：需要环境控制"
                    Return Agent工具封装_v6.获取参数面板控件信息(Agent通用工具_v6.GetJsonString(args, "query"))
                Case "list_parameter_presets"
                    If permissionLevel < PermissionEnvironment Then Return "权限不足：需要环境控制"
                    Return Agent工具封装_v6.列出参数预设(Agent通用工具_v6.GetJsonString(args, "source"))
                Case "read_parameter_preset"
                    If permissionLevel < PermissionEnvironment Then Return "权限不足：需要环境控制"
                    Return Agent工具封装_v6.读取参数预设(Agent通用工具_v6.GetJsonString(args, "source"), Agent通用工具_v6.GetJsonString(args, "name"))
                Case "apply_parameter_preset"
                    If permissionLevel < PermissionEnvironment Then Return "权限不足：需要环境控制"
                    Return Agent工具封装_v6.应用参数预设(Agent通用工具_v6.GetJsonString(args, "source"), Agent通用工具_v6.GetJsonString(args, "name"))
                Case "save_parameter_preset"
                    If permissionLevel < PermissionEnvironment Then Return "权限不足：需要环境控制"
                    Dim saveOutputLocation As Boolean? = Nothing
                    If HasJsonProperty(args, "save_output_location") Then saveOutputLocation = Agent通用工具_v6.GetJsonBoolean(args, "save_output_location", False)
                    Return Agent工具封装_v6.保存参数预设(Agent通用工具_v6.GetJsonString(args, "source"), Agent通用工具_v6.GetJsonString(args, "name"), Agent通用工具_v6.GetJsonString(args, "preset_json"), Agent通用工具_v6.GetJsonString(args, "note"), saveOutputLocation)
                Case "web_search"
                    If Not AgentNetworkMode.IsEnabled(networkMode) Then Return "联网已禁用"
                    Return Await WebSearchAsync(Agent通用工具_v6.GetJsonString(args, "query"), Agent通用工具_v6.GetJsonString(args, "engine_url"), networkMode, endpointClient, modelId, reasoningEffort, cancellationToken)
                Case "fetch_url"
                    If Not AgentNetworkMode.IsEnabled(networkMode) Then Return "联网已禁用"
                    If AgentNetworkMode.Normalize(networkMode) <> AgentNetworkMode.Local Then Return "当前联网模式不允许本地网页请求"
                    Return Await FetchUrlAsync(Agent通用工具_v6.GetJsonString(args, "url"), cancellationToken,
                                               Agent通用工具_v6.GetJsonString(args, "method", "GET"),
                                               ReadHeaderMap(args),
                                               Agent通用工具_v6.GetJsonString(args, "user_agent"),
                                               Agent通用工具_v6.GetJsonString(args, "referer"),
                                               Agent通用工具_v6.GetJsonString(args, "cookies"),
                                               Agent通用工具_v6.GetJsonString(args, "body"),
                                               Agent通用工具_v6.GetJsonInteger(args, "max_chars", 12000),
                                               Agent通用工具_v6.GetJsonString(args, "response_format", "auto"), False)
                Case "http_request"
                    If Not AgentNetworkMode.IsEnabled(networkMode) Then Return "联网已禁用"
                    If AgentNetworkMode.Normalize(networkMode) <> AgentNetworkMode.Local Then Return "当前联网模式不允许本地 HTTP 请求"
                    Return Await FetchUrlAsync(Agent通用工具_v6.GetJsonString(args, "url"), cancellationToken,
                                               Agent通用工具_v6.GetJsonString(args, "method", "GET"),
                                               ReadHeaderMap(args),
                                               Agent通用工具_v6.GetJsonString(args, "user_agent"),
                                               Agent通用工具_v6.GetJsonString(args, "referer"),
                                               Agent通用工具_v6.GetJsonString(args, "cookies"),
                                               Agent通用工具_v6.GetJsonString(args, "body"),
                                               Agent通用工具_v6.GetJsonInteger(args, "max_chars", 20000),
                                               Agent通用工具_v6.GetJsonString(args, "response_format", "auto"), True)
                Case "read_local_text_file"
                    If permissionLevel < PermissionSystem Then Return "权限不足：需要系统访问"
                    Return ReadLocalTextFile(Agent通用工具_v6.GetJsonString(args, "path"),
                                             Agent通用工具_v6.GetJsonInteger(args, "start_line", 1),
                                             Agent通用工具_v6.GetJsonInteger(args, "line_count", 0),
                                             Agent通用工具_v6.GetJsonInteger(args, "max_chars", 20000))
                Case "write_local_text_file"
                    If permissionLevel < PermissionSystem Then Return "权限不足：需要系统访问"
                    Return WriteLocalTextFile(Agent通用工具_v6.GetJsonString(args, "path"),
                                              Agent通用工具_v6.GetJsonString(args, "content"),
                                              Agent通用工具_v6.GetJsonString(args, "encoding", "utf-8"),
                                              Agent通用工具_v6.GetJsonBoolean(args, "create_directories", False))
                Case "apply_local_text_patch"
                    If permissionLevel < PermissionSystem Then Return "权限不足：需要系统访问"
                    Return ApplyLocalTextPatch(Agent通用工具_v6.GetJsonString(args, "path"),
                                               Agent通用工具_v6.GetJsonString(args, "old_text"),
                                               Agent通用工具_v6.GetJsonString(args, "new_text"),
                                               Agent通用工具_v6.GetJsonBoolean(args, "replace_all", False),
                                               Agent通用工具_v6.GetJsonInteger(args, "expected_replacements", 0))
                Case "list_directory"
                    If permissionLevel < PermissionSystem Then Return "权限不足：需要系统访问"
                    Return ListDirectory(Agent通用工具_v6.GetJsonString(args, "path"),
                                         Agent通用工具_v6.GetJsonBoolean(args, "recursive", False),
                                         Agent通用工具_v6.GetJsonInteger(args, "max_items", 200))
                Case "create_directory"
                    If permissionLevel < PermissionSystem Then Return "权限不足：需要系统访问"
                    Return CreateDirectory(Agent通用工具_v6.GetJsonString(args, "path"))
                Case "copy_local_file"
                    If permissionLevel < PermissionSystem Then Return "权限不足：需要系统访问"
                    Return CopyLocalFile(Agent通用工具_v6.GetJsonString(args, "source"), Agent通用工具_v6.GetJsonString(args, "destination"), Agent通用工具_v6.GetJsonBoolean(args, "overwrite", False))
                Case "move_local_path"
                    If permissionLevel < PermissionSystem Then Return "权限不足：需要系统访问"
                    Return MoveLocalPath(Agent通用工具_v6.GetJsonString(args, "source"), Agent通用工具_v6.GetJsonString(args, "destination"), Agent通用工具_v6.GetJsonBoolean(args, "overwrite", False))
                Case "delete_local_path"
                    If permissionLevel < PermissionSystem Then Return "权限不足：需要系统访问"
                    If Not Agent通用工具_v6.GetJsonBoolean(args, "confirm", False) Then Return "拒绝删除：必须传 confirm=true"
                    Return DeleteLocalPath(Agent通用工具_v6.GetJsonString(args, "path"))
                Case "get_image_info"
                    If permissionLevel < PermissionSystem Then Return "权限不足：需要系统访问"
                    Return GetImageInfo(Agent通用工具_v6.GetJsonString(args, "path"))
                Case "run_powershell"
                    Return Await RunConsoleToolAsync(permissionLevel, powerShellSession, args, cancellationToken, "PowerShell")
                Case "run_windows_executable"
                    If permissionLevel < PermissionSystem Then Return "权限不足：需要系统访问"
                    Return Await RunWindowsExecutableAsync(args, cancellationToken)
                Case Else
                    Return $"未知工具：{callInfo?.Name}"
            End Select
        Catch ex As OperationCanceledException When cancellationToken.IsCancellationRequested
            Throw
        Catch ex As Exception
            Return $"工具执行失败：{ex.Message}"
        End Try
    End Function

    Private Shared Function ParseToolArguments(callInfo As AgentToolCallInfo, ByRef errorMessage As String) As JsonElement
        errorMessage = ""
        Dim raw = If(callInfo?.Arguments, "")
        Dim rawArgumentProperty = GetRawConsoleArgumentPropertyName(callInfo?.Name)
        Try
            Dim parsed = Agent通用工具_v6.ParseJsonArguments(raw)
            If parsed.ValueKind = JsonValueKind.Object Then Return parsed

            If rawArgumentProperty <> "" AndAlso parsed.ValueKind = JsonValueKind.String Then
                Return NormalizeToolArguments(callInfo, rawArgumentProperty, If(parsed.GetString(), ""))
            End If

            callInfo.Arguments = "{}"
            errorMessage = "工具参数必须是 JSON 对象。原始参数：" & Agent通用工具_v6.LimitText(raw, 4000)
            Return BuildEmptyJsonElement()
        Catch ex As Exception
            If rawArgumentProperty <> "" AndAlso Not raw.TrimStart().StartsWith("{", StringComparison.Ordinal) AndAlso Not raw.TrimStart().StartsWith("[", StringComparison.Ordinal) Then
                Return NormalizeToolArguments(callInfo, rawArgumentProperty, raw)
            End If

            callInfo.Arguments = "{}"
            errorMessage = "工具参数 JSON 解析失败：" & ex.Message & vbCrLf &
                           "原始参数：" & Agent通用工具_v6.LimitText(raw, 4000)
            Return BuildEmptyJsonElement()
        End Try
    End Function

    Private Shared Function GetRawConsoleArgumentPropertyName(toolName As String) As String
        Select Case If(toolName, "")
            Case "run_powershell"
                Return "command"
            Case Else
                Return ""
        End Select
    End Function

    Private Shared Function NormalizeToolArguments(callInfo As AgentToolCallInfo, propertyName As String, value As String) As JsonElement
        Dim normalizedJson = BuildSingleStringJson(propertyName, value)
        callInfo.Arguments = normalizedJson
        Using doc = JsonDocument.Parse(normalizedJson)
            Return doc.RootElement.Clone()
        End Using
    End Function

    Private Shared Function BuildSingleStringJson(propertyName As String, value As String) As String
        Dim payload As New Dictionary(Of String, String) From {
            {propertyName, If(value, "")}
        }
        Return JsonSerializer.Serialize(payload)
    End Function

    Private Shared Function BuildEmptyJsonElement() As JsonElement
        Using doc = JsonDocument.Parse("{}")
            Return doc.RootElement.Clone()
        End Using
    End Function

    Private Shared Function FunctionTool(name As String,
                                         description As String,
                                         properties As Dictionary(Of String, Object),
                                         Optional required As IEnumerable(Of String) = Nothing) As Dictionary(Of String, Object)
        Return New Dictionary(Of String, Object) From {
            {"type", "function"},
            {"function", New Dictionary(Of String, Object) From {
                {"name", name},
                {"description", description},
                {"parameters", New Dictionary(Of String, Object) From {
                    {"type", "object"},
                    {"properties", properties},
                    {"required", If(required?.ToArray(), Array.Empty(Of String)())}
                }}
            }}
        }
    End Function

End Class
