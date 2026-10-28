# 预设与文件上下文

## 参数预设

预设来源包括 `用户自定义`、`从社区下载`、`开发者内置`。用户自定义和社区下载是文件预设，目录分别为 `Preset_v6\User` 和 `Preset_v6\Community`。开发者内置数据写死在类中。

可用工具包括 `list_parameter_presets`、`read_parameter_preset`、`apply_parameter_preset`、`save_parameter_preset`。开发者内置只允许读取和应用，不能覆盖、保存或删除。保存只允许用户自定义和社区来源；当前没有删除预设工具。

读取预设会返回 JSON、备注、总览和命令行预览。应用预设只是加载到当前参数面板，应用后仍应读参数面板验证。保存预设可传 `preset_json`，也可不传而保存当前参数面板。`save_output_location=true` 会保存计算机名称、输出位置和保留子文件夹结构起始点；`false` 会清空这些输出位置字段；不传则沿用预设管理页额外保存输出位置勾选状态。

## 准备文件页

`get_prepare_files`、`set_prepare_files`、`submit_prepare_files_to_queue` 操作的是准备文件页，不是 Agent 对话框待提交文件列表。准备文件页的文件会按当前参数面板设置加入编码队列。`set_prepare_files` 支持 append、replace、clear；配置前要确认用户是想追加还是替换。

文件直接拖入编码队列和准备文件入队一样，都会用当前参数面板为每个文件生成任务。任务生成后持有独立预设副本，不会随面板后续修改自动改变。

## 对话附件与本地文件

Agent 对话框待提交文件会在用户下一条消息中只转为文件或文件夹路径，不内嵌文本和 base64。系统访问权限下的 `read_local_text_file`、`write_local_text_file`、`apply_local_text_patch`、`list_directory`、`get_image_info` 等工具是读取或修改这些路径的入口；不能默认读取用户没有明确要求的文件。

处理路径时区分三类：参数面板输出位置、准备文件页路径、系统访问文件路径。参数面板输出位置为空或无效代表默认原目录；准备文件页路径会影响入队；系统访问路径用于读取和诊断。不要混用这些概念。
