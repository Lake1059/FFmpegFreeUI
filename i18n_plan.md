# FFmpegFreeUI 国际化 (i18n) 实施方案

## 1. 方案概述
采用 **基于字典 (Dictionary-based)** 的方案，使用 JSON 文件存储键值对。
系统启动时或用户切换语言时，加载对应的 JSON 文件，并遍历当前打开的窗体控件进行文本替换。

## 2. 技术选型
- **存储格式**: JSON (`System.Text.Json` 解析)
- **文件路径**: 应用程序根目录下的 `Lang` 文件夹 (e.g., `Lang/zh-CN.json`, `Lang/en-US.json`)
- **核心模块**: `LanguageManager` (VB.NET Module)

## 3. 涉及模块
需要进行国际化的主要界面包括：
1.  **主界面**: `Form1` (已初步集成)
2.  **参数设置**: 
    - `界面_常规流程参数_V2`
    - `Form独立参数面板`
3.  **功能窗口**:
    - `界面_设置`
    - `界面_混流`
    - `界面_合并`
    - `Form超分`
    - `Form插帧`
    - `Form帧混合`
    - `Form画面裁剪交互窗口`
4.  **其他**: `性能统计`, `插件管理`, 右键菜单, 动态提示信息 (MessageBox)

## 4. 实施步骤

### 阶段一：基础设施搭建 (已完成)
1.  创建 `LanguageManager.vb` 模块。
    - 实现 `LoadLanguage(cultureCode)`。
    - 实现 `GetText(key, defaultText)`。
    - 实现 `ApplyLanguage(form)` 自动遍历翻译。
2.  创建 `Lang` 目录及初始文件 `zh-CN.json` (作为默认/回退) 和 `en-US.json` (测试用)。
3.  在 `Form1_Load` 中调用 `LanguageManager`。

### 阶段二：主界面适配 (`Form1`) (进行中)
1.  提取 `Form1` 中的所有静态文本。
2.  在 `zh-CN.json` 中添加 Key-Value。
3.  替换代码中的动态文本 (如 `MessageBox.Show`)。

### 阶段三：逐步覆盖其他模块
按优先级依次处理其他 Form。

### 特殊情况处理
- **动态创建的控件**: (如 `编码队列右键菜单.vb`) 需要在创建时直接调用 `LanguageManager.GetText`。
- **列表头 (ListView Headers)**: `ApplyLanguage` 已包含对 ListView Columns 的处理逻辑，需确保 Key 命名规范 (`FormName.ListViewName.Column_Index`)。

## 5. 字典键名规范
- **控件文本**: `FormName.ControlName` (例如: `Form1.ButtonStart`, `Form1.LabelStatus`)
- **通用消息**: `Msg_Description` (例如: `Msg_FileNotFound`, `Msg_EncodingComplete`)
- **菜单项**: `Menu_Description` (例如: `Menu_File`, `Menu_Exit`)
- **动态菜单**: `CtxMenu.Description` (例如: `CtxMenu.StartNew`)

## 6. 使用示例
**VB.NET 代码:**
```vb
' 静态控件自动翻译
LanguageManager.ApplyLanguage(Me)

' 动态文本获取
Dim msg = LanguageManager.GetText("Msg_Done", "任务完成")
MessageBox.Show(msg)
```

**JSON 数据:**
```json
{
  "Form1.Text": "FFmpegFreeUI 主界面",
  "Msg_Done": "Task Completed"
}
```
