Public Class Form_v6_参数面板_章节

    Public 所属参数面板对象 As Form_v6_参数面板

    Private Sub Form_v6_参数面板_章节_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub ModernComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ModernComboBox1.SelectedIndexChanged
        通知参数面板刷新()
    End Sub

    Private Sub ModernTextBox2_TextChanged(sender As Object, e As EventArgs) Handles ModernTextBox2.TextChanged
        通知参数面板刷新()
    End Sub

    Private Sub ModernTextBox2_DoubleClick(sender As Object, e As EventArgs) Handles ModernTextBox2.DoubleClick
        选择章节文件()
    End Sub

    Private Sub 选择章节文件()
        Dim filter = If(ModernComboBox1.SelectedIndex = 2,
                        "媒体文件|*.mp4;*.mkv;*.mov;*.m4v;*.webm;*.mp3;*.m4a;*.flac;*.wav;*.ogg|所有文件|*.*",
                        "FFmetadata 或文本文档|*.txt;*.ffmetadata;*.metadata|所有文件|*.*")
        Using d As New OpenFileDialog With {.Filter = filter, .Multiselect = False}
            If d.ShowDialog(Me) <> DialogResult.OK OrElse d.FileName = "" Then Exit Sub
            ModernTextBox2.Text = d.FileName
        End Using
    End Sub

    Private Sub MB_教程_Click(sender As Object, e As EventArgs) Handles MB_教程.Click
        MB_教程.Visible = False
        JustEmptyControl2.Visible = False
        Me.MarkDownViewer1.Visible = True
        Me.MarkDownViewer1.Text =
 "如果你不清楚章节文本文档如何编写，这是一份简易教程

## 章节的作用
- 为长视频/音频提供导航锚点
- 在播放器（如 VLC、mpv）中显示章节列表
- 用于有声书、播客、纪录片等场景的结构化标记

## 文本文档要求
- **必须使用 UTF-8 编码**（无 BOM）
- 推荐使用 Unix 换行符 `\n`（LF），也接受 Windows 换行符 `\r\n`（CRLF）
- 不支持旧版 Mac 换行符 `\r`（CR）

## 文件头
```
;FFMETADATA1
```
- 必须是文件**第一行**，前面不能有任何空白字符或 BOM
- 分号 `;` 开头表示注释，但文件头的分号是格式标识符的一部分，**不可省略**
- 版本号当前固定为 `1`

### 全局元数据（可选）
文件头之后、第一个 `[CHAPTER]` 块之前，可放置全局媒体元数据：
```
;FFMETADATA1
title=我的视频标题
comment=这是一段注释
```
这些字段会写入媒体文件的全局标签（tags），不属于章节数据。

## 章节块语法
每个章节由一个 `[CHAPTER]` 块定义：
```
[CHAPTER]
TIMEBASE=<分子>/<分母>
START=<整数>
END=<整数>
title=<字符串>
title=第一章
```
### 块定界符
| 元素 | 说明 |
|------|------|
| `[CHAPTER]` | 章节块开始标记，**必须独占一行** |
| 下一个 `[CHAPTER]` 或文件末尾 | 章节块结束 |
| `[STREAM]` | 流元数据块（与章节无关，但可共存） |

## 时间戳格式
### TIMEBASE 字段
| 示例值 | 含义 | 精度 |
|--------|------|------|
| `1/1000` | 毫秒精度（最常用） | 1 ms |
| `1/1000000` | 微秒精度 | 1 μs |
| `1/44100` | 音频采样率对齐 | ~22.7 μs |
| `1/48000` | 音频采样率对齐 | ~20.8 μs |
| `1/1` | 秒精度 | 1 s |
- `TIMEBASE` 是**可选字段**。省略时，FFmpeg 默认使用 `1/1000000000`（纳秒）
- `TIMEBASE` 必须在 `START` 和 `END` 之前声明（若出现在同一块内）
- 同一文件中各章节可以使用**不同的 TIMEBASE**，但强烈不建议这样做
- 分子通常为 `1`；分母为正整数

### START 和 END 字段
**实际时间 = 字段值 × TIMEBASE**
| 规则 | 说明 |
|------|------|
| START ≥ 0 | 不能为负数 |
| END > START | 结束时间必须严格大于开始时间 |
| 相邻章节 | 建议 `END[n] == START[n+1]`，避免空隙 |
| 最后章节的 END | 建议等于媒体总时长，可用 `END=9999999999` 兜底 |
| 类型 | 必须为整数（不支持小数点） |

章节还支持很多特性，可以直接问 AI 或者查阅 ffmpeg 官方文档。
"
    End Sub

    Private Sub 通知参数面板刷新()
        If 所属参数面板对象 Is Nothing OrElse 所属参数面板对象.抑制自动刷新 Then Exit Sub
        所属参数面板对象.请求刷新参数状态()
    End Sub

End Class
