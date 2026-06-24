Public Class AgentUsageInfo
    Public Property PromptTokens As Integer
    Public Property CompletionTokens As Integer
    Public Property TotalTokens As Integer
    Public Property CachedTokens As Integer
    Public Property ReasoningTokens As Integer
    Public Property InputTokens As Integer
    Public Property OutputTokens As Integer

    Public Sub Add(other As AgentUsageInfo)
        If other Is Nothing Then Exit Sub
        PromptTokens += other.PromptTokens
        CompletionTokens += other.CompletionTokens
        TotalTokens += other.TotalTokens
        CachedTokens += other.CachedTokens
        ReasoningTokens += other.ReasoningTokens
        InputTokens += other.InputTokens
        OutputTokens += other.OutputTokens
    End Sub
End Class

Public Class AgentModelInfo
    Public Property Id As String = ""
    Public Property SupportedEndpointTypes As New List(Of String)
    Public Property ReasoningEfforts As New List(Of String)
    Public Property RawJson As String = ""
End Class

Public Class AgentToolCallInfo
    Public Property Id As String = ""
    Public Property Name As String = ""
    Public Property Arguments As String = ""
End Class

Public Class AgentMessageData
    Public Property Id As String = Guid.NewGuid().ToString("N")
    Public Property Role As String = ""
    Public Property Content As String = ""
    Public Property Name As String = ""
    Public Property ToolCallId As String = ""
    Public Property ToolCalls As New List(Of AgentToolCallInfo)
    Public Property CreatedAt As DateTime = DateTime.Now
End Class

Public Class AgentConversationData
    Public Property Id As String = Guid.NewGuid().ToString("N")
    Public Property Title As String = "新对话"
    Public Property CreatedAt As DateTime = DateTime.Now
    Public Property UpdatedAt As DateTime = DateTime.Now
    Public Property SortOrder As Integer = 0
    Public Property ModelId As String = ""
    Public Property ReasoningEffort As String = ""
    Public Property NetworkMode As Integer = 0
    Public Property PermissionLevel As Integer = 0
    Public Property Messages As New List(Of AgentMessageData)
    Public Property Usage As New AgentUsageInfo
End Class

Public NotInheritable Class AgentNetworkMode
    Private Sub New()
    End Sub

    Public Const Local As Integer = 0
    Public Const Endpoint As Integer = 1
    Public Const Disabled As Integer = 2

    Public Shared Function Normalize(value As Integer) As Integer
        Select Case value
            Case Endpoint, Disabled, Local
                Return value
            Case Else
                Return Local
        End Select
    End Function

    Public Shared Function IsEnabled(value As Integer) As Boolean
        Return Normalize(value) <> Disabled
    End Function

    Public Shared Function DisplayName(value As Integer) As String
        Select Case Normalize(value)
            Case Local
                Return "本地联网"
            Case Endpoint
                Return "端点联网"
            Case Else
                Return "禁用联网"
        End Select
    End Function
End Class

Public Class AgentConversationIndexFile
    Public Property Version As Integer = 2
    Public Property Items As New List(Of AgentConversationIndexItem)
End Class

Public Class AgentConversationIndexItem
    Public Property Id As String = ""
    Public Property Title As String = ""
    Public Property CreatedAt As DateTime = DateTime.Now
    Public Property UpdatedAt As DateTime = DateTime.Now
    Public Property SortOrder As Integer = 0
    Public Property FileName As String = ""
End Class

Public Class AgentChatResult
    Public Property Success As Boolean = True
    Public Property ErrorMessage As String = ""
    Public Property StatusCode As Integer = 0
    Public Property Content As String = ""
    Public Property ToolCalls As New List(Of AgentToolCallInfo)
    Public Property Usage As AgentUsageInfo
    Public Property RawJson As String = ""

    Public Shared Function Fail(message As String, Optional statusCode As Integer = 0, Optional rawJson As String = "") As AgentChatResult
        Return New AgentChatResult With {
            .Success = False,
            .ErrorMessage = If(message, ""),
            .StatusCode = statusCode,
            .RawJson = If(rawJson, "")
        }
    End Function
End Class

Public Class AgentClientResult(Of T)
    Public Property Success As Boolean
    Public Property Value As T
    Public Property ErrorMessage As String = ""
    Public Property StatusCode As Integer = 0

    Public Shared Function Ok(value As T) As AgentClientResult(Of T)
        Return New AgentClientResult(Of T) With {.Success = True, .Value = value}
    End Function

    Public Shared Function Fail(message As String, Optional statusCode As Integer = 0) As AgentClientResult(Of T)
        Return New AgentClientResult(Of T) With {
            .Success = False,
            .ErrorMessage = If(message, ""),
            .StatusCode = statusCode
        }
    End Function
End Class
