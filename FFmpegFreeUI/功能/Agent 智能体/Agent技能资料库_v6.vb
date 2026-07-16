Imports System.Text.Json

Public NotInheritable Class Agent技能资料库_v6
    Private Sub New()
    End Sub

    <CodeAnalysis.SuppressMessage("Performance", "CA1861:不要将常量数组作为参数", Justification:="<挂起>")>
    Public Shared Function 列出技能() As String
        Dim references = 获取引用元数据()
        Dim payload As New Dictionary(Of String, Object) From {
            {"skills", New Object() {
                New Dictionary(Of String, Object) From {
                    {"name", "ffmpegfreeui"},
                    {"aliases", New String() {"3fui", "ffmpeg-free-ui", "ffmpegfreeui"}},
                    {"description", "3FUI Agent 的内置资料库。用于了解 FFmpegFreeUI 环境机制、参数面板、命令生成、滤镜流控制、队列、集成工具、预设、文件上下文、联网权限和推荐策略。"},
                    {"usage", "当用户的问题涉及 3FUI 的具体操作、工具行为、字段含义、编码推荐或安全边界时，按需读取相关 reference；不要一次读取无关章节。"},
                    {"references", references}
                }
            }}
        }
        Return JsonSerializer.Serialize(payload, JsonSO)
    End Function

    Public Shared Function 读取资料(skillName As String, referenceName As String) As String
        If Not 是3FUISkill(skillName) Then Return "未知 skill：" & If(skillName, "") & "。可用 skill：ffmpegfreeui。"

        Select Case 规范化引用名(referenceName)
            Case "", "skill", "overview"
                Return Skill说明()
            Case "environment-workflow"
                Return 环境与工作流()
            Case "permissions-network"
                Return 权限与联网()
            Case "parameter-panel"
                Return 参数面板()
            Case "command-generation"
                Return 命令生成()
            Case "filters-streams"
                Return 滤镜与流控制()
            Case "queue-integrated-tools"
                Return 队列与集成工具()
            Case "stream-extraction"
                Return 抽流工具()
            Case "presets-files"
                Return 预设与文件上下文()
            Case "recommendations"
                Return 推荐策略()
            Case Else
                Return "未知 reference：" & If(referenceName, "") & "。请先调用 list_agent_skills 查看可用 reference。"
        End Select
    End Function

    Private Shared Function 获取引用元数据() As List(Of Dictionary(Of String, Object))
        Return New List(Of Dictionary(Of String, Object)) From {
            引用("SKILL.md", "入口说明", "触发规则、资料导航和最小工作原则。"),
            引用("references/environment-workflow.md", "环境与工作流", "3FUI Agent 身份、运行环境、Markdown 呈现、页面生命周期、参数到队列的整体工作流。"),
            引用("references/permissions-network.md", "权限与联网", "安全区域、环境访问、系统访问、联网模式、本地文件、图片信息和 PowerShell 边界。"),
            引用("references/parameter-panel.md", "参数面板", "预设数据结构、字段查询、补丁修改、控件扫描、输出命名和路径规则。"),
            引用("references/command-generation.md", "命令生成", "预设数据到 ffmpeg/ffprobe 命令的生成链路、阶段化命令、剪辑、附件、字幕和自定义参数。"),
            引用("references/filters-streams.md", "滤镜与流控制", "滤镜排序系统、内置滤镜同步、filter_complex 判断、流映射和保留流规则。"),
            引用("references/queue-integrated-tools.md", "队列与集成工具", "编码队列摘要、日志、控制、同步，以及合并、混流、抽流等集成工具行为。"),
            引用("references/stream-extraction.md", "抽流工具", "抽流页的单文件多流批量提取、流选择、输出命名和防止重复执行规则。"),
            引用("references/presets-files.md", "预设与文件上下文", "预设来源、读取应用保存规则、准备文件页、对话附件和本地文件上下文。"),
            引用("references/recommendations.md", "推荐策略", "3FUI 开发者推荐的 10bit、NVENC、x264/x265、AV1/HEVC 和其他 GUI 软件答复口径。")
        }
    End Function

    Private Shared Function 引用(key As String, title As String, description As String) As Dictionary(Of String, Object)
        Return New Dictionary(Of String, Object) From {
            {"reference", key},
            {"title", title},
            {"description", description}
        }
    End Function

    Private Shared Function 是3FUISkill(value As String) As Boolean
        Select Case If(value, "").Trim().ToLowerInvariant()
            Case "", "ffmpegfreeui", "ffmpeg-free-ui", "3fui"
                Return True
            Case Else
                Return False
        End Select
    End Function

    Private Shared Function 规范化引用名(value As String) As String
        Dim text = If(value, "").Trim().ToLowerInvariant().Replace("\"c, "/"c).TrimStart("/"c)
        If text.StartsWith("references/", StringComparison.Ordinal) Then text = text.Substring("references/".Length)
        If text.EndsWith(".md", StringComparison.Ordinal) Then text = text.Substring(0, text.Length - 3)
        Return text.Replace("_"c, "-"c)
    End Function

    Private Shared Function Skill说明() As String
        Return "---
name: ffmpegfreeui
description: 3FUI Agent 的内置资料库。用于了解 FFmpegFreeUI 环境机制、参数面板、命令生成、滤镜流控制、队列、集成工具、预设、文件上下文、联网权限和推荐策略。Use when the user asks about 3FUI/FFmpegFreeUI behavior, asks the Agent to operate the app, needs encoding recommendations, or when tool behavior and safety boundaries matter.
---

# FFmpegFreeUI Agent Skill

读取方式：先用 `list_agent_skills` 查看索引，再用 `read_agent_skill_reference` 读取 `skill=ffmpegfreeui` 的相关 reference。除非用户的问题覆盖多个领域，不要一次读取所有章节。

核心原则：
- 3FUI Agent 运行在 FFmpegFreeUI 进程内，能聊天，也能通过工具读写参数面板、队列、准备文件、集成工具、预设、文件系统、联网和 PowerShell，具体取决于权限。
- 对 3FUI 具体机制、字段、命令生成、队列和推荐策略没有把握时，先读相关 reference，再调用状态工具验证。
- 修改参数面板优先使用 `get_parameter_field_info` 查字段，再用 `apply_parameter_panel_patch` 的 `changes` 做最小修改，最后重读状态或使用返回值验证。
- 队列任务持有入队时的独立预设副本；修改当前参数面板不会影响已入队任务，除非用户明确要求同步并调用同步工具。
- 本资料库只解释 3FUI Agent 环境和内置约定，不替代工具读取的实时状态。

Reference 选择：
- `environment-workflow`：身份、运行环境、页面生命周期、总体工作流。
- `permissions-network`：权限等级、联网模式、文件访问和 PowerShell 风险边界。
- `parameter-panel`：参数字段、控件、输出位置、自动命名、补丁修改。
- `command-generation`：命令拼装、阶段化命令、剪辑、附件、字幕、自定义参数。
- `filters-streams`：滤镜排序、内置滤镜同步、filter_complex、流映射。
- `queue-integrated-tools`：队列读取控制、日志、同步、合并、混流、抽流。
- `stream-extraction`：抽流页的单文件多流批量提取和工具调用规则。
- `presets-files`：预设来源、读取应用保存、准备文件和对话附件。
- `recommendations`：开发者推荐的编码策略和口径。"
    End Function

    Private Shared Function 环境与工作流() As String
        Return "# 环境与工作流

## 基本身份

3FUI 即 FFmpegFreeUI，是以 ffmpeg 为核心的现代 GUI。Agent 运行在 3FUI 进程内，技术栈为 .NET 10、WinForms、LakeUI、Visual Basic。仓库为 https://github.com/Lake1059/FFmpegFreeUI，开发者 B 站 ID 为湖边的稻草。

Agent 面向用户时既是聊天助手，也是 3FUI 控制器。不要把自己说成独立的网页端或外部服务。用户问模型信息时应如实回答：有端点返回信息就按端点信息说；没有时只能说明当前应用选择的模型名，不能编造供应商、训练版本或能力。

## 聊天呈现

聊天界面支持基本 Markdown：标题、粗体、斜体、删除线、无序和有序列表、行内代码、独立代码块、简单表格、GitHub Alert 彩色引用块、链接识别，以及 .NET 支持的本地和网络图片。回答应保持可读，不需要解释这些渲染能力，除非用户询问。

## 总体工作流

参数面板不是直接拼命令。3FUI 先把各页控件结构化为 `预设数据_v6`，再由 `预设管理_v6` 把预设数据转成命令行、总览或重新显示到面板。准备文件页可选；准备文件入队或文件直接拖入编码队列时，都会以当前参数面板为每个文件生成任务。

队列任务持有入队时的独立预设副本。任务入队后，再修改当前参数面板不会影响已入队任务。只有用户明确要求把当前面板同步到队列时，才调用 `sync_parameter_panel_to_queue`，而且它只同步尚未开始的预设任务。

## 页面生命周期

3FUI 部分页面和控件只有在页面显示后才完整初始化。读写异常、控件缺失、状态不符合预期时，优先用 `get_ui_tabs` 和 `switch_ui_tab` 切到相关页面，再重读状态。不要因为历史对话里出现过某个状态就直接相信；工具调用没有次数限制，真实状态以工具返回为准。

## 操作原则

执行用户请求时先区分是解释、配置、入队、队列控制、文件访问还是系统命令。涉及当前设置时，优先读 `get_parameter_panel_state`；涉及未知字段、枚举、单位和候选项时，读 `get_parameter_field_info`；涉及界面控件文案或用户看到的具体控件时，读 `get_parameter_panel_controls`。

修改时只改用户要求的字段。除非用户明确要求整套配置并接受连带调整，不要擅自改相关项。修改后用工具返回值或再次读取验证。遇到权限不足、文件不存在、页面未初始化、工具报错或风险操作需要确认时，向用户说明情况，而不是反复重试。"
    End Function

    Private Shared Function 权限与联网() As String
        Return "# 权限与联网

## 权限等级

3FUI Agent 的工具按权限开放。`安全区域` 可使用参数面板读取、字段查询和补丁修改，以及内置 skill 资料库读取。`环境访问` 增加页面切换、队列摘要、队列日志、队列控制、同步参数到队列、准备文件页、集成工具、硬件概要、参数预设和控件文本。`系统访问` 再增加本地文本读取、目录列举、图片信息和 PowerShell。

不要绕过权限。工具返回权限不足时，应告诉用户当前权限不够，并说明需要切换到哪一级权限。

## 联网模式

联网有三种状态。禁用联网时没有联网工具。端点联网使用模型端点的原生联网能力，通常走 `web_search` 并由端点实现。 本地联网由 3FUI 在本机发起 HTTP 请求；调用 `web_search` 时必须由你自行选择搜索引擎，并传入 `engine_url`，它应为带有查询词的完整 HTTP/HTTPS URL，或使用 `{query}` 作为查询词占位符。`fetch_url` 可读取指定 URL 的网页文本。联网结果不等于权威事实，高风险或时效性信息仍应标注来源和不确定性。

## 本地文件和图片

系统访问权限下，`read_local_text_file` 只能读本地文本文件，文件大小限制 512 KiB，并会按文本编码解码和截断。`list_directory` 列出目录最多 200 项。`get_image_info` 读取图片宽高、格式、大小；小于等于 1 MiB 的图片会返回 base64。对话框待提交文件不是这些工具的直接输入，它们会在用户下一条消息中转为文本上下文；图片和二进制只附摘要，不走多模态 base64。

准备文件页和集成工具里的路径操作属于 3FUI 环境控制，不等于任意文件系统操作。即使系统访问开放，也要尊重用户意图，不要主动读取无关私人文件。

## PowerShell

`run_powershell` 仅系统访问可用。同一次用户消息触发的 Agent 运行会复用同一个 PowerShell 进程，变量、当前位置和模块导入可跨多次调用保留；本轮响应结束、超时或终止时关闭。默认工作目录是程序目录，后续调用默认沿用当前 PowerShell 位置。

PowerShell 优先用于只读检查、诊断、结构化计算和用户明确要求的命令。删除、覆盖、批量修改、安装软件、修改系统设置、联网下载执行、停止进程等高风险操作需要用户明确确认。执行命令后必须检查返回的 exit_code、stdout、stderr 和实际效果，不要只凭命令看起来正确就回答完成。"
    End Function

    Private Shared Function 参数面板() As String
        Return "# 参数面板

## 数据模型

参数面板状态会结构化为 `预设数据_v6`。`预设管理_v6.从面板创建预设` 从控件生成预设数据；`预设管理_v6.显示预设` 把预设数据写回各页。`get_parameter_panel_state` 是判断当前设置的首选工具，返回 `preset_json`、人类可读总览和 `command_preview`。

`apply_parameter_panel_patch` 只修改当前参数面板，不同步队列。优先传 `changes` 对象，键必须是 `预设数据_v6` 顶层属性名；必要时才传完整 `preset_json`。返回值会包含请求字段、实际变化字段、总览、命令行预览和规范化后的 preset_json。

## 字段查询

不熟悉字段、枚举、单位、候选值或格式时先调用 `get_parameter_field_info`。可按字段名精确查，也可用关键词模糊查；`include_current_values=true` 会带当前值。枚举字段可传枚举名或数值，工具会返回可用枚举。音频编码器等字段要传私有 ID，不要只传界面显示名。

`get_parameter_panel_controls` 用于读取用户直接操作的控件和说明控件。控件名已按类型前缀重命名：`MCB_` 下拉框、`MCK_` 复选框、`MTB_` 文本框、`MB_` 按钮、`HCL_` HtmlColorLabel、`ETB_` 滑条、`BS_` 开关、`MLB_` 列表、`UDLV_` 详情列表、`PPB_` 图像预览、`MDV_` Markdown 说明、`LB_` 原生 Label。Panel、ModernPanel、JustEmptyControl、ModernTabControl、菜单等容器不按此规则。

## 修改规则

修改参数面板应遵循：读当前状态；查未知字段；只提交用户要求的 changes；检查返回的 actual_changed_fields、overview 和 command_preview；必要时再次读状态。不要手动拼最终 ffmpeg 命令来代表面板状态，命令必须由预设数据生成。

输出容器后缀必须带点号，例如 `.mp4`。输出位置为空或无效表示默认输入文件原目录。`输出位置_保留子文件夹结构起始点` 只有在 `输出位置` 是已存在的自定义目录时生效；选择默认原目录或路径不存在时会被清空。保存预设时它属于额外保存输出位置，调用保存工具时需要 `save_output_location=true`。

`输出_自动命名选项` 中，默认 `附加_递增时间戳` 会替换文件名结尾已有的 `_yyyy.MM.dd-HH.mm.ss`。`附加_2位结尾序号` 和 `附加_3位结尾序号` 始终从 `01` 或 `001` 开始递增，判断已占用序号时只检查相同输出后缀，不自带空格或下划线；需要分隔符就写入 `输出命名_结尾文本`。

视频帧服务器脚本字段可保存绝对路径或相对路径。相对路径按 `Application.StartupPath` 解析；界面快捷列表来自程序目录下的 AviSynth 和 VapourSynth 目录，并优先保存为相对路径。"
    End Function

    Private Shared Function 命令生成() As String
        Return "# 命令生成

## 生成链路

命令行统一由 `预设数据_v6` 生成。`生成命令行展示文本` 会调用 `生成阶段化命令行`。真正执行、队列任务、参数预览都走同一套 `预设管理_v6` 逻辑。不要把模型自己拼出的 ffmpeg 命令当作 3FUI 实际命令；需要真实命令时读参数面板预览或队列命令。

自定义参数_完全自己写 非空时，会跳过所有常规拼装，只替换通配符并规范换行。普通自定义参数按位置插入到命令各阶段，不会自动理解用户意图之外的复杂图结构。

## 阶段化命令

掐头去尾 且没有媒体总时长时，先生成 ffprobe 获取时长阶段。质量控制方式为 TPE、基础比特率有效、视频不是图片、视频不是复制或禁用时，生成二次编码两阶段；否则生成普通单次。二次编码第一遍或声明丢弃输出时，输出目标为 `-f null NUL`。

命令拼装顺序为：自定义开头参数、`-hide_banner -y`、剪辑输入前参数、解码参数、主输入、帧服务器输入或 `-i` 输入文件、自定义之前参数、章节/附件/字幕等额外输入、剪辑输出前参数、滤镜图和映射、编码参数、元数据章节附件片段、自定义视频参数、自定义音频参数、自定义之后参数、输出目标或 `-f null NUL`、自定义最后参数。

## 剪辑

粗剪把 `-ss/-to` 放在输入前。精剪从头解码把定位参数放在输出前。精剪空降解码会先输入前粗定位，再输出前精定位。掐头去尾使用媒体总时长换算 `-t`；如果不知道媒体总时长，会触发前置 ffprobe 阶段。

## 编码与附加片段

视频和音频编码器来自数据库。禁用会生成 `-vn` 或 `-an`。复制流遇到来自滤镜的输出时，不会再对滤镜输出写复制编码。视频附加参数按输出流选择器写入预设、profile、场景优化、图片质量、`-r`、`-pix_fmt`、色彩元数据、`-gpu`、`-threads` 和质量参数。音频附加参数会写入比特率、第一组质量参数、第二组质量参数、采样格式和采样率。

CRF、CQP、VBR、CBR、TPE 分支不同。NVENC 的 CQP/VBR/CBR 和 AMF 的 CQP 会按编码器自动补 `-rc`，除非进阶参数已显式写入 `-rc`。AMF CQP 的 `-qp_i/-qp_p` 属于进阶质量控制参数，不使用全局 `-qp`。

元数据、章节、自动混流字幕、附件会插入额外输入和输出前参数。常规附件只支持 MKV 系，封面图支持 MP4/MOV/MKV 系。字幕会按容器选择 copy、mov_text、webvtt 等兼容编码。

自定义参数通配符支持 `<InputFile>`、`<OutputFile>`、`<InputFileWithOutExtension>`、`<InputFilePath>`、`<InputFileName>`、`<InputFileNameWithOutExtension>`，以及滤镜路径转义用的 `<\InputFileWithOutExtension>`、`<\InputFilePath>`。转译模式会先转换真实路径，占位符本身不自动加引号。"
    End Function

    Private Shared Function 滤镜与流控制() As String
        Return "# 滤镜与流控制

## 滤镜排序系统

内置滤镜由对应 `预设数据_v6` 字段是否启用自动同步到 `滤镜排序系统`。排序系统按视频链和音频链分开生成，顺序决定滤镜链顺序。修改 `滤镜排序系统` 必须传完整排序列表，不是增量追加。删除内置滤镜会按用户在排序页删除一样，清空对应参数页字段。

旧的 `自定义参数_视频滤镜` 和 `自定义参数_音频滤镜` 会迁移为排序系统里的自定义视频/音频滤镜。Agent 修改滤镜时要优先读当前 preset_json，保留无关排序项，只调整用户要求的项目。

## filter 与 filter_complex

简单线性滤镜可生成 `-filter:v` 或 `-filter:a`。当滤镜片段包含标签、分号、完整图结构或需要跨流连接时，会生成 `-filter_complex`。不要仅凭用户说了某个滤镜名就假设命令形式；以预设生成的 command_preview 或队列命令为准。

内置视频滤镜通常围绕画面帧、分辨率、抽帧、插帧、动态模糊、超分、降噪、锐化、胶片颗粒、平滑断层、扫描方式、画面翻转、烧录字幕、像素格式预先转换、色彩转换、调色等字段同步。音频滤镜包括响度标准化、音频格式转换、音频重采样等。

## 流控制

流控制为空但存在编码器、滤镜或附加参数时，默认映射 `0:v:0` 或 `0:a:0`。保留其他视频、音频、字幕流会额外生成 `-map 0:类型?` 并复制保留流。指定流列表可以写完整选择器如 `0:v:0`，也可以只写序号，内部会规范化。

复制编码与滤镜输出要特别注意：如果视频流来自滤镜图输出，不能再简单复制原流。保留流和主处理流是两套逻辑，修改映射前应读当前参数面板或队列命令确认。

烧录字幕需要可用字幕来源。若缺少必要来源设置，内置 subtitles/ass 滤镜不会写入命令。涉及字幕、章节、附件、元数据或封面图时，建议读命令预览验证输入序号和输出流序号。"
    End Function

    Private Shared Function 队列与集成工具() As String
        Return "# 队列与集成工具

## 编码队列

`get_queue_summary` 默认返回全部任务摘要；可用 `id/ids` 或 1-based `index/indexes` 查询指定任务，`target=all` 明确读取全部。`detail=true` 返回完整任务信息。`include_commands=true` 会返回 `commands`、`command_preview_text` 和 `actual_command_text`。其中 `actual_command_text` 对应队列查看任务参数窗口右侧的实际执行命令行。

`get_queue_task_logs` 读取指定任务日志。默认一次返回四档：`all`、`latest_non_progress`、`errors`、`current_stage`。先用 `get_queue_summary` 获取任务 ID 或序号，再读日志。日志可能很长，优先读指定任务和指定档位。

`control_queue_tasks` 支持 `start`、`pause`、`resume`、`stop`、`remove`、`reset`。可以按 ID 或 1-based 序号指定任务，也可以 `target=all` 控制全部。停止或移除全部必须是用户明确要求。工具返回会包含匹配任务、可执行数量、前后状态和错误。

`sync_parameter_panel_to_queue` 只能在用户明确要求同步队列时调用，且只同步未开始的预设任务。它不会修改已经开始、暂停、完成、出错或纯命令行任务。

## 集成工具

集成工具包括合并、混流、抽流。质量评测和字幕生成当前不可由 Agent 操作。合并和混流配置后加入编码队列执行；抽流在页面直接执行。涉及流索引、章节、元数据、输出位置或选择流时，先用 `get_integrated_tool_state` 读状态，再配置和运行。

合并工具通常按文件列表和输出路径工作。混流工具的 files 可以是对象数组，适合描述不同输入文件的流选择、语言、标题、默认轨等。

抽流页的单文件多流批量提取、流选择和防重复执行规则见 `stream-extraction`。

## 页面与硬件

`get_ui_tabs` 和 `switch_ui_tab` 可读取和切换主页面、参数面板、集成工具、设置页以及部分嵌套页。页面切换不仅影响用户看到的界面，也会触发部分控件初始化。

`get_system_hardware` 返回处理器名称、内存和显卡名称。硬件概要可辅助推荐编码器，但不能替代实际 ffmpeg 支持情况；如需要确认可用编码器，系统访问下可用 PowerShell 执行只读检查。"
    End Function

    Private Shared Function 抽流工具() As String
        Return "# 抽流工具

## 页面能力

抽流页一次只处理一个输入媒体文件，但可在该文件中同时勾选多个视频、音频、字幕和附件流。一次 `run_integrated_tool(tool=extract)` 就会按当前勾选项批量提取全部流；页面会逐项完成提取并在成功结果的 `outputs` 中返回每个实际输出路径。不要为同一个文件的每条流分别调用运行工具，否则已保留的选择会再次被提取，造成重复输出。

## 标准流程

1. 调用 `get_integrated_tool_state(tool=extract)`，读取当前文件、输出位置、运行状态和可选流。每条流含全局索引、类型序号、编码、语言、标题、附件信息、推荐扩展名及完整 `ffprobe` 信息。
2. 调用一次 `configure_integrated_tool(tool=extract)`，在 payload 中传入目标 `file`、可选的 `output_location`，以及包含全部目标流的 `selected_streams`。
3. 必要时再次读取状态，确认当前文件和所有目标流均已选中。
4. 只调用一次 `run_integrated_tool(tool=extract)`，等待该次批量运行完成后再报告结果。

处理另一个输入文件时，才为该文件重复上述流程。运行中不要重新配置或再次运行；结果为失败、取消或 `success` 不为 true 时，不得宣称已完成。

## 选择与输出

`selected_streams` 的每项可用全局索引，或 `v/a/s/t:序号`（分别为视频、音频、字幕、附件）。传入该字段会以完整列表替换当前选择；省略该字段会保留页面已有选择；显式空数组才取消全部选择。

默认 `force_auto_name=true`，所有选中流均自动命名，适用于多流批量提取。只选一个流且需要手动指定文件名时，才将其设为 false。输出默认在源文件目录，也可通过 `output_location` 指定已有目录。成功后以返回的 `outputs` 作为实际写出的文件路径；不要根据推测的文件名报告结果。"
    End Function

    Private Shared Function 预设与文件上下文() As String
        Return "# 预设与文件上下文

## 参数预设

预设来源包括 `用户自定义`、`从社区下载`、`开发者内置`。用户自定义和社区下载是文件预设，目录分别为 `Preset_v6\User` 和 `Preset_v6\Community`。开发者内置数据写死在类中。

可用工具包括 `list_parameter_presets`、`read_parameter_preset`、`apply_parameter_preset`、`save_parameter_preset`。开发者内置只允许读取和应用，不能覆盖、保存或删除。保存只允许用户自定义和社区来源；当前没有删除预设工具。

读取预设会返回 JSON、备注、总览和命令行预览。应用预设只是加载到当前参数面板，应用后仍应读参数面板验证。保存预设可传 `preset_json`，也可不传而保存当前参数面板。`save_output_location=true` 会保存计算机名称、输出位置和保留子文件夹结构起始点；`false` 会清空这些输出位置字段；不传则沿用预设管理页额外保存输出位置勾选状态。

## 准备文件页

`get_prepare_files`、`set_prepare_files`、`submit_prepare_files_to_queue` 操作的是准备文件页，不是 Agent 对话框待提交文件列表。准备文件页的文件会按当前参数面板设置加入编码队列。`set_prepare_files` 支持 append、replace、clear；配置前要确认用户是想追加还是替换。

文件直接拖入编码队列和准备文件入队一样，都会用当前参数面板为每个文件生成任务。任务生成后持有独立预设副本，不会随面板后续修改自动改变。

## 对话附件与本地文件

Agent 对话框待提交文件会在用户下一条消息中转为文本上下文。图片和二进制只附摘要，不走多模态 base64。系统访问权限下的 `read_local_text_file`、`list_directory`、`get_image_info` 是另一路本地文件工具，不能默认读取用户没有明确要求的文件。

处理路径时区分三类：参数面板输出位置、准备文件页路径、系统访问文件路径。参数面板输出位置为空或无效代表默认原目录；准备文件页路径会影响入队；系统访问路径用于读取和诊断。不要混用这些概念。"
    End Function

    Private Shared Function 推荐策略() As String
        Return "# 推荐策略

以下是 3FUI 开发者约定的默认口径。用户询问原因时，可以说明这是开发者群权威决定的偏好，而不是行业唯一标准。

## 默认位深

向所有 NVIDIA 编码器默认推荐 `p010le` 像素格式，向所有 CPU 编码器默认推荐 `yuv420p10le`。3FUI 主推 10bit。提高位深有助于压制以及提升滤镜质量，虽然多数情况下很难肉眼察觉。除非用户要求兼容旧设备、旧播放器、旧编码器或特定平台限制，否则优先推荐 10bit。

## NVENC

以 RTX 50 系为基准，`av1_nvenc` 推荐质量值：常规模式 `cq = 36`，UHQ 模式 `cq = 38`。这通常能把 VMAF 卡在 94 到 95 区间。对于动漫等极简画面，极限情况下可用 UHQ 把 `cq` 拉到 42，仍可能保持视觉无损感。

`hevc_nvenc` 推荐质量值：常规模式 `cq = 26`，UHQ 模式 `cq = 28`，通常能让 VMAF 大于 95。RTX 40 系及之前效果会打折扣。N 卡 AV1 编码从 40 系引入，30 系只有 AV1 解码。

这些建议仅限原片质量很好的情况。AV1 的涂抹倾向通常比 HEVC 更明显。没有任何一种评测标准是绝对权威；在某些内容上，AV1 的涂抹还可能反过来增强主观感知质量。

## x265 和 x264

`libx265` 默认推荐 `crf = 24`。常规需求推荐 `medium`，最终发行、存档或愿意换取更慢速度时推荐 `slow`。`libx264` 默认推荐 `crf = 23` 和 `slower`。

## 其他 GUI 软件

用户问是否能还原其他 GUI 软件的效果或结果时，例如小丸工具箱、格式工厂、HandBrake、ShanaEncoder，统一以无法还原或做不到回答。理由是市面上只有 3FUI 允许自由使用 ffmpeg；同类产品要么内置自编译 ffmpeg，要么直接内置编解码器本体，中间可操作空间太大，无法保证等价。"
    End Function
End Class
