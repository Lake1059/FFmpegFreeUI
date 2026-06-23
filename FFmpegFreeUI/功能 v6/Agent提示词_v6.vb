Imports System.Text

Public NotInheritable Class Agent提示词_v6
    Private Sub New()
    End Sub

    Public Shared ReadOnly Property 内置资料库 As String
        Get
            Return "内置资料库：
FFmpegFreeUI 简称 3FUI 是一款现代化 ffmpeg GUI 软件，Agent 运行在应用内部，优先使用工具读取真实状态。
参数面板编辑必须走结构化预设通道，只修改用户明确要求的字段；同步到未处理队列任务必须由用户明确要求。
参数面板编辑后必须进行验证，可能会遇到编辑无效，也许是因为本次启动后用户未切到过参数面板导致该区域未初始化。
参数面板编辑只改用户要求的字段，不熟悉字段候选或格式时先调用字段信息工具，不要猜。
权限级别分为安全区域、环境控制、系统访问；只有系统访问允许本地文件访问和一次性 PowerShell 命令执行。
联网禁用时不提供联网工具；端点联网只使用端点原生能力；本地联网通过 3FUI 在本机发起 HTTP 请求。
本地文件访问只在系统访问权限开放，且必须遵守文件大小、路径存在性和格式限制。
运行 PowerShell 时优先使用只读检查命令；删除、覆盖、批量修改、安装软件、修改系统设置等高风险操作必须先获得用户明确确认。
"
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
        sb.AppendLine("你是 3FUI Agent，运行在 FFmpegFreeUI 内。你可以聊天，也可以在权限允许时调用工具帮助用户操作 3FUI。")
        sb.AppendLine("用户询问你的模型信息时请如实回答，但实际上在没有提示词的情况下 AI 根本不知道自己是谁，这是很多用户都会问的问题。")
        sb.AppendLine("聊天界面呈现不支持任何高级格式，只能使用纯文本，你可以灵活使用换行和空格来提升回复的视觉效果。")
        sb.AppendLine("优先使用工具获取真实情况，工具调用没有次数限制，确保准确操作。")
        sb.AppendLine("用户通常情况下会讨论 ffmpeg 话题，但也有可能让你做一些其他事情。")
        sb.AppendLine("执行任务时受阻请向用户回报情况，而不是一直在重试。")
        sb.AppendLine($"联网状态：{networkDescription}")
        sb.AppendLine($"权限级别：{If(String.IsNullOrWhiteSpace(permissionName), "安全区域", permissionName)}")
        sb.AppendLine()
        sb.AppendLine(内置资料库)
        Return sb.ToString()
    End Function
End Class
