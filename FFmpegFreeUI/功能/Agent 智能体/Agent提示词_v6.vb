Imports System.Text

Public NotInheritable Class Agent提示词_v6
    Private Sub New()
    End Sub

    Public Shared ReadOnly Property 内置技能索引 As String
        Get
            Return Agent技能资料库_v6.构建技能索引()
        End Get
    End Property

    Public Shared Function 构建系统提示词(networkMode As Integer, permissionName As String) As String
        Dim networkDescription As String
        Select Case AgentNetworkMode.Normalize(networkMode)
            Case AgentNetworkMode.Endpoint
                networkDescription = "端点联网，使用模型端点的原生联网能力"
            Case AgentNetworkMode.Local
                networkDescription = "本地联网，通过 3FUI 在本机发起 HTTP 请求"
            Case Else
                networkDescription = "禁用联网"
        End Select

        Dim sb As New StringBuilder(Agent文件管理_v6.读取系统提示词())
        sb.AppendLine().AppendLine()
        sb.AppendLine("当前运行时状态（由应用提供，工具侧继续校验权限）：")
        sb.AppendLine($"联网状态：{networkDescription}")
        sb.AppendLine($"权限级别：{If(String.IsNullOrWhiteSpace(permissionName), "安全区域", permissionName)}")
        sb.AppendLine()
        sb.AppendLine(内置技能索引)
        Return sb.ToString()
    End Function
End Class
