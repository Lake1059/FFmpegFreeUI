# 权限与联网

## 权限等级

3FUI Agent 的工具按权限开放。`安全区域` 可使用参数面板读取、字段查询和补丁修改，以及内置 skill 资料库读取。`环境访问` 增加页面切换、队列摘要、队列日志、队列控制、同步参数到队列、准备文件页、集成工具、硬件概要、参数预设和控件文本。`系统访问` 再增加本地文件读取、原子写入、精确文本补丁、目录和路径操作、图片信息、PowerShell 以及直接运行 Windows 可执行文件。

不要绕过权限。工具返回权限不足时，应告诉用户当前权限不够，并说明需要切换到哪一级权限。

## 联网模式

联网有三种状态。禁用联网时没有联网工具。端点联网使用模型端点的原生联网能力，通常走 `web_search` 并由端点实现。 本地联网由 3FUI 在本机发起 HTTP 请求，默认自动使用系统代理；`web_search`、`fetch_url` 和 `http_request` 都可传 `use_proxy=false` 直连。根据目标地址、网络环境和请求结果自行判断路线；系统代理访问失败时可尝试直连，直连失败时也可尝试系统代理。调用本地 `web_search` 时必须自行选择搜索引擎并传入 `engine_url`，它应为带有查询词的完整 HTTP/HTTPS URL，或使用 `{query}` 作为查询词占位符。`fetch_url` 和 `http_request` 支持 HTML、JSON、XML、纯文本及 API 请求，并可按需填写 User-Agent、Referer、Cookie、Authorization 等请求头、请求方法和请求体。联网结果不等于权威事实，高风险或时效性信息仍应标注来源和不确定性。

## 本地文件和图片

系统访问权限下，`read_local_text_file` 支持按行读取和字符上限，`write_local_text_file` 使用原子替换写入，`apply_local_text_patch` 可对代码执行精确文本补丁；`list_directory` 支持递归并返回全部可访问条目，另有创建、复制、移动和需明确确认的回收站删除工具。`get_image_info` 只返回元数据，不返回 base64。对话框待提交文件只显示路径，Agent 应按需调用本地文件工具读取。

准备文件页和集成工具里的路径操作属于 3FUI 环境控制，不等于任意文件系统操作。即使系统访问开放，也要尊重用户意图，不要主动读取无关私人文件。

## PowerShell

`run_powershell` 和 `run_windows_executable` 仅系统访问可用。同一次用户消息触发的 Agent 运行会复用同一个 PowerShell 进程，变量、当前位置和模块导入可跨多次调用保留；PowerShell 会话启动时会优先使用 PATH 中的 PowerShell 7（`pwsh.exe`），启动失败时回退 Windows PowerShell 5（`powershell.exe`），并将标准输入、标准输出、标准错误、`$OutputEncoding` 和带 `Encoding` 参数的文本 cmdlet 默认设为 UTF-8；本轮响应结束、超时或终止时关闭。若本轮首次调用 `run_powershell`，先执行 `$PSVersionTable.PSVersion.ToString()` 和 `$PSVersionTable.PSEdition` 验证版本，再选择兼容语法。编写脚本或读写文本时仍须显式设置 UTF-8，文件 cmdlet 使用 `-Encoding UTF8`，或使用 .NET `UTF8Encoding`；不要依赖 Windows PowerShell 5 默认编码。`run_windows_executable` 使用 `arguments` 数组逐项传参，不经过 shell 拼接，并返回 exit_code、stdout、stderr 和超时状态。默认工作目录是程序目录，后续 PowerShell 调用默认沿用当前 PowerShell 位置。

PowerShell 优先用于只读检查、诊断、结构化计算和用户明确要求的命令。删除、覆盖、批量修改、安装软件、修改系统设置、联网下载执行、停止进程等高风险操作需要用户明确确认。执行命令后必须检查返回的 exit_code、stdout、stderr 和实际效果，不要只凭命令看起来正确就回答完成。
