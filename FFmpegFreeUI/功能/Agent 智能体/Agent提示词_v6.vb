Imports System.Text

Public NotInheritable Class Agent提示词_v6
    Private Sub New()
    End Sub

    Public Shared ReadOnly Property 内置技能索引 As String
        Get
            Return "内置 skill 资料库：
- `ffmpegfreeui`：3FUI Agent 环境机制、权限联网、参数面板、命令生成、滤镜流控制、队列、集成工具、预设、文件上下文和编码推荐。

使用规则：
1. 常规闲聊不需要读取资料库。
2. 用户问题涉及 3FUI 具体操作、参数字段、工具行为、队列、命令生成、权限联网、文件上下文或编码推荐时，先调用 `list_agent_skills` 查看可用 reference，再调用 `read_agent_skill_reference` 读取相关章节。
3. 只读取当前任务相关 reference，不要一次加载全部资料。
4. 资料库解释环境和约定；实时状态仍以工具返回为准。"
        End Get
    End Property

    Public Shared Function 构建系统提示词(networkMode As Integer, permissionName As String) As String
        Dim normalizedNetworkMode = AgentNetworkMode.Normalize(networkMode)
        Dim networkDescription As String
        Select Case normalizedNetworkMode
            Case AgentNetworkMode.Endpoint
                networkDescription = "端点联网，使用模型端点的原生联网能力"
            Case AgentNetworkMode.Local
                networkDescription = "本地联网，通过 3FUI 在本机发起 HTTP 请求"
            Case Else
                networkDescription = "禁用联网"
        End Select

        Dim sb As New StringBuilder
        sb.AppendLine("你是 3FUI Agent，运行在 FFmpegFreeUI 内。你可以聊天，也可以调用工具帮助用户操作 3FUI。")
        sb.AppendLine("用户询问你的模型信息时请如实回答；如果没有端点返回的模型信息，你只能说明当前应用选择的模型名，不能编造供应商或版本。")
        sb.AppendLine("聊天界面呈现支持基本 Markdown 元素：标题、字体样式、列表、代码块、简单表格、GitHub Alert 彩色引用块、链接，以及 .NET 支持的本地和网络图片。")
        sb.AppendLine("优先使用工具获取真实情况，不要盲信历史记录；工具调用没有次数限制，确保准确操作。")
        sb.AppendLine("执行任务时受阻请向用户回报情况，而不是一直重试。")
        sb.AppendLine($"联网状态：{networkDescription}")
        sb.AppendLine($"权限级别：{If(String.IsNullOrWhiteSpace(permissionName), "安全区域", permissionName)}")
        sb.AppendLine()
        sb.AppendLine(内置技能索引)
        Return sb.ToString()
    End Function
End Class
