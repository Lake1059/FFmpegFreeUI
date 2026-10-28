---
name: ffmpegfreeui
description: 3FUI Agent 的内置资料库。用于了解 FFmpegFreeUI 环境机制、参数面板、命令生成、滤镜流控制、队列、集成工具、预设、文件上下文、联网权限、编码推荐和杜比视界编码。Use when the user asks about 3FUI/FFmpegFreeUI behavior, asks the Agent to operate the app, needs encoding recommendations, Dolby Vision encoding guidance, or when tool behavior and safety boundaries matter.
---

# FFmpegFreeUI Agent Skill

文件机制：系统提示词在 exe 旁 `Agent/agents.md`，技能在 `Agent/skills/<目录名>/SKILL.md` 和 references。内置文件只在缺失时安装，已有修改不会被升级覆盖。新请求和技能工具以磁盘当前内容为准；自定义技能使用独立目录和带 name/description 的 SKILL.md，不要修改全局 Claude 配置。

视频推荐读取 `nvidia-video`，明确 AVIF 请求读取 `avif-aom`，独立歌曲/音频读取 `audio-nmr-aac`；这些技能自带完整参考预设，不依赖预设管理页安装情况。

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
- `agent-tools`：所有 Agent 工具的参数、返回值、调用顺序和权限边界。
- `parameter-panel`：参数字段、控件、输出位置、自动命名、补丁修改。
- `command-generation`：命令拼装、阶段化命令、剪辑、附件、字幕、自定义参数。
- `filters-streams`：滤镜排序、内置滤镜同步、filter_complex、流映射。
- `queue-integrated-tools`：队列读取控制、日志、同步、合并、混流、抽流。
- `stream-extraction`：抽流页的单文件多流批量提取和工具调用规则。
- `presets-files`：预设来源、读取应用保存、准备文件和对话附件。
- `recommendations`：开发者推荐的编码策略和口径。
- `dolby-vision`：Profile 分流、DV 编码参数及 Profile 7 转单轨 8.1 工作流。
