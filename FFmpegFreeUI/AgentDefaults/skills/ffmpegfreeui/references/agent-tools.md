# Agent 工具说明

## 调用总则

工具名称和参数以当前 `BuildToolDefinitions` 返回的定义为准。工具调用前先确认权限和联网模式；工具返回的实时状态优先于历史消息。涉及修改时先读取状态，执行后检查返回值；不要把工具调用结果之外的推测当成已完成。

权限按风险分层：安全区域包含资料库和参数面板读写；环境访问包含队列、页面、准备文件、集成工具、硬件和预设；系统访问才包含任意本地路径、PowerShell 和 Windows 程序。权限不足时不要改用更高风险工具绕过。

## 安全区域工具

- `list_agent_skills`：读取程序旁 `Agent/skills`，列出当前 skill 和 reference，包含用户编辑或添加的技能。遇到 3FUI 具体机制、字段、队列、联网或推荐问题时先调用。
- `read_agent_skill_reference`：读取指定技能的当前磁盘文件。参数为 `skill`（默认 `ffmpegfreeui`）和 `reference`（例如 `SKILL.md`）；只读取当前问题相关章节，不允许越出技能目录或通过链接访问。
- `get_parameter_panel_state`：默认只返回当前参数面板的 `overview`。按需设置 `include_command_preview=true` 或 `include_preset_json=true`，无需总览时设置 `include_overview=false`；至少开启一项。
- `get_parameter_field_info`：查询字段类型、当前值、候选值、枚举和格式规则。可传 `fields`、`query`、`include_current_values`；不熟悉字段时先查再改。
- `apply_parameter_panel_patch`：修改当前参数面板。优先传 `changes`，必要时才传完整 `preset_json`，默认返回 `requested_changes`、`effective_changed_fields` 和命令预览；总览和完整预设分别用 `include_overview=true`、`include_preset_json=true` 获取，命令预览可用 `include_command_preview=false` 关闭。它不会自动修改已入队任务。

## 环境访问工具

- `get_queue_summary`：读取队列摘要或详情。可用 `id/ids`、1-based `index/indexes`、`target=all`；大队列用 `offset/limit` 分页。`detail`、`include_commands`、`include_preset_json` 和 `include_performance` 会增加返回体，按需打开。
- `get_queue_task_logs`：读取日志。先用队列摘要获得 ID，再用 `mode`/`modes` 选择 `all`、`latest_non_progress`、`errors`、`current_stage`，并用 `log_limit` 限制条数。
- `control_queue_tasks`：控制 `start`、`pause`、`resume`、`stop`、`remove`、`reset`。`start` 单次只启动一个指定任务，不触发自动补满；多任务需分别调用。停止或移除全部必须有用户明确指令；执行后检查返回的 `eligible_count`、`before` 和 `after`。
- `sync_parameter_panel_to_queue`：把当前面板同步到尚未开始的预设任务。只有用户明确要求同步时调用；不会修改已开始、暂停、完成、出错或命令行任务。
- `get_ui_tabs` / `switch_ui_tab`：读取或切换主页面、参数面板、集成工具、设置和嵌套页。控件未初始化或用户要求切换页面时使用。
- `get_prepare_files` / `set_prepare_files` / `submit_prepare_files_to_queue`：操作准备文件页。`set_prepare_files` 的 `mode` 为 `append`、`replace` 或 `clear`；它与对话附件列表不是同一功能。
- `get_integrated_tool_state`：读取 `merge`、`mux` 或 `extract` 的当前配置和状态，配置或运行前先调用。
- `configure_integrated_tool`：配置集成工具。`merge` 使用必填 `files` 字符串数组，可选 `output` 和 `mode`（`replace`、`append`、`clear`）；`mux` 的 `files` 是对象数组，每项至少有 `path`，可包含 `video`、`audio`、`subtitle`、`chapters`、`metadata`，另有 `output`、`mode`，以及可选的 `default_video`、`default_audio`、`default_subtitle`（值使用状态中的 `文件索引:类型:流索引`）；`extract` 使用 `file` 读取新文件，仅修改当前勾选或输出位置时省略 `file`，可选 `output_location` 和 `selected_streams`。
- `run_integrated_tool`：运行当前集成工具配置。合并和混流会加入编码队列，抽流直接执行；抽流一次配置后只运行一次。抽流可在 payload 中传 `force_auto_name`（默认 `true`）。
- `get_system_hardware`：读取处理器、内存和显卡概要，包含 `processor_count` / `logical_processor_count`（进程可用逻辑处理器数）与 `physical_core_count`（无法读取时为 0）。只用于辅助建议，不能代替实际编码器可用性检查。
- `configure_avif_parallelism`：必填 `file_count`，可选 `max_parallel`，按 CPU 和文件数配置单线程 AOM AVIF 批次的临时并行上限。不启动任务、不改变全局设置；有其他活动或可自动启动任务时拒绝，批次结束后清理。仅在用户明确选择 AVIF 且授权编码后使用。
- `get_parameter_panel_controls`：扫描用户可见控件和说明控件；需要定位控件名、候选项或页面文案时使用，不用它替代结构化参数状态。
- `list_parameter_presets` / `read_parameter_preset` / `apply_parameter_preset` / `save_parameter_preset`：列出、读取、应用和保存预设。开发者内置预设只读可应用；保存时注意 `save_output_location`。

## 联网工具

- `web_search`：端点联网由模型端点搜索；本地联网必须提供 `engine_url`，可使用 `{query}` 占位符。
- `fetch_url`：读取网页、JSON、XML 或纯文本。可传 `method`、`headers`、`user_agent`、`referer`、`cookies`、`body`、`response_format`、`max_chars`。
- `http_request`：通用本地 HTTP 请求，返回 `status_code`、响应头、`content_type` 和响应体摘要。API 请求优先用它，并通过 `headers` 填写 `Authorization` 等身份标识。模型可以根据用户提供的站点要求选择 User-Agent、Referer、Cookie 和请求头，但不得凭空编造登录凭据或绕过访问控制。

## 系统访问工具

- `read_local_text_file`：必填 `path`，支持 `start_line`、`line_count`、`max_chars`；大文件按片段读取。
- `write_local_text_file`：必填 `path`、`content`，用原子替换写入文本，可选 `encoding`（`utf-8`、`utf-8-bom`、`utf-16`）和 `create_directories`。
- `apply_local_text_patch`：必填 `path`、`old_text`、`new_text`，对代码执行精确修改；默认要求唯一匹配，可用 `replace_all` 或 `expected_replacements` 明确匹配次数。
- `list_directory`：必填 `path`，列出目录中的全部可访问条目，支持 `recursive`。
- `create_directory`：必填 `path`，创建目录（已存在时返回现状）。`copy_local_file` 和 `move_local_path` 必填 `source`、`destination`，可选 `overwrite`，默认不覆盖目标。
- `delete_local_path`：必须传 `confirm=true`，文件和目录优先进入回收站；执行前再次确认路径和用户意图。
- `get_image_info`：读取图片尺寸、格式和大小，不返回 Base64。
- `run_powershell`：复用本轮 PowerShell 会话，适合诊断和用户明确要求的命令；参数为 `command`、可选 `working_directory` 和 `timeout_seconds`。首次调用前先验证 `$PSVersionTable`；脚本必须显式使用 UTF-8 设置。
- `run_windows_executable`：直接运行 Windows 可执行文件。参数为 `executable`、`arguments` 数组，可选 `working_directory`、`stdin`、`timeout_seconds`、`max_output_chars`；返回 `exit_code`、`stdout`、`stderr`、`timed_out`。参数逐项传递，不要把整条命令拼成一个字符串。

所有系统访问工具都要求 `系统访问` 权限。删除、覆盖、批量修改、运行未知程序或带副作用的网络请求必须先确认用户意图。
