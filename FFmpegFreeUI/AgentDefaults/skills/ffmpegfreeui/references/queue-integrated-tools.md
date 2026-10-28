# 队列与集成工具

## 编码队列

`get_queue_summary` 默认返回全部任务摘要；可用 `id/ids` 或 1-based `index/indexes` 查询指定任务，`target=all` 明确读取全部。大队列可用 `offset`、`limit` 分页，避免一次返回过多任务。`detail=true` 返回完整任务信息。`include_commands=true` 会返回 `commands`、`command_preview_text` 和 `actual_command_text`。其中 `actual_command_text` 对应队列查看任务参数窗口右侧的实际执行命令行。

`get_queue_task_logs` 读取指定任务日志。默认一次返回四档：`all`、`latest_non_progress`、`errors`、`current_stage`。先用 `get_queue_summary` 获取任务 ID 或序号，再读日志。日志可能很长，优先读指定任务和指定档位。

`control_queue_tasks` 支持 `start`、`pause`、`resume`、`stop`、`remove`、`reset`。`start` 单次调用只能启动一个指定任务，优先传 `id` 或 1-based `index`；多目标或 `target=all` 会整次拒绝，多任务需分别调用。Agent 启动与手动启动相同，只启动指定任务，不触发自动补满并发数量；任务自然结束后仍按正常自动调度处理后续任务。其他 action 可按 ID 或序号指定多个任务，也可 `target=all` 控制全部。停止或移除全部必须是用户明确要求。工具返回会包含匹配任务、可执行数量、前后状态和错误。

自动补满由空队列首次添加任务（已启用自动开始时）或任务自然结束（包括失败）触发，均按当前队列顺序启动并分配输出文件名。非空队列继续添加、手动或 Agent 启动、暂停、恢复、停止、重置、排序、移除任务及设置变更都不触发补满。等待停止清理的调度会在清理结束后继续，仍属于此前自然结束触发的调度。

队列摘要、日志和控制结果仅在非空时返回 `missing_ids`、`missing_indexes`、`errors`，缺省表示没有对应问题；空 `tasks`、日志数组、控制前后数组仍保留。返回 JSON 使用紧凑格式，不改变字段类型、日志顺序、分页和权限行为。

`sync_parameter_panel_to_queue` 用当前参数面板完整覆盖指定任务快照。必须传 `id/ids` 或 1-based `index/indexes`，优先使用稳定 ID；不允许缺省目标或 `target=all`。只更新未处理且未执行的预设任务，跳过其他状态和纯命令行任务。任何目标不存在时整批不修改。

`patch_queue_task_presets` 使用相同的指定任务参数，传非空 `changes` 对象，只修改各任务自己的预设快照中列出的顶层字段，保留其他字段，不修改当前参数面板。例如将 ids 指定为目标任务 ID 列表，在 changes 中将 视频参数_编码器_编码预设 设为 slow。先用 `get_parameter_field_info` 查询准确字段名和类型；用 `get_queue_summary` 的 `include_preset_json=true` 读取目标快照。数组和对象字段须提供该字段完整值；滤镜排序必须传完整列表，删除内置滤镜会清空对应参数。返回 updated_count、跳过数量以及各已更新任务的 id 和 effective_changed_fields。

## 集成工具

集成工具包括合并、混流、抽流。质量评测和字幕生成当前不可由 Agent 操作。合并和混流配置后加入编码队列执行；抽流在页面直接执行。涉及流索引、章节、元数据、输出位置或选择流时，先用 `get_integrated_tool_state` 读状态，再配置和运行。

合并工具通常按文件列表和输出路径工作。混流工具的 `files` 可以是对象数组；每项至少有 `path`，可选 `video`、`audio`、`subtitle` 文本，以及 `chapters`、`metadata` 布尔开关。混流还支持 `default_video`、`default_audio`、`default_subtitle`，用于从当前映射流中指定输出默认轨道；值使用 `get_integrated_tool_state` 返回的 `文件索引:类型:流索引`，省略字段会保留页面已有选择。

抽流页的单文件多流批量提取、流选择和防重复执行规则见 `stream-extraction`。

## 页面与硬件

`get_ui_tabs` 和 `switch_ui_tab` 可读取和切换主页面、参数面板、集成工具、设置页以及部分嵌套页。页面切换不仅影响用户看到的界面，也会触发部分控件初始化。

`get_system_hardware` 返回处理器名称、内存和显卡名称。硬件概要可辅助推荐编码器，但不能替代实际 ffmpeg 支持情况；如需要确认可用编码器，系统访问下可用 PowerShell 执行只读检查。
