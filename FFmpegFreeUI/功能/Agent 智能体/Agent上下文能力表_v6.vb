Public NotInheritable Class Agent上下文能力表_v6
    Private Sub New()
    End Sub

    Public Const 通用上下文总量Tokens As Integer = 270000

    ' 在这里维护已知模型的上下文总量。未命中规则时统一使用通用上下文总量。
    ' 示例：
    '   精确("some-model-id", 128000)
    '   前缀("some-model-family", 200000)
    <CodeAnalysis.SuppressMessage("Performance", "CA1825:避免长度为零的数组分配", Justification:="<挂起>")>
    Private Shared ReadOnly 上下文总量规则 As 上下文总量规则项() = {
    }

    ' 在这里维护已知上下文压缩专用模型，按优先级从高到低匹配端点模型列表。
    Private Shared ReadOnly 上下文压缩专用模型 As String() = {
        "gpt-5.5-openai-compact",
        "gpt-5.4-openai-compact",
        "gpt-5.3-openai-compact"
    }

    Public Shared Function 获取上下文总量(modelId As String) As Integer
        For Each 单项规则 In 上下文总量规则
            If 单项规则 Is Nothing OrElse Not 单项规则.匹配(modelId) Then Continue For
            If 单项规则.上下文总量Tokens > 0 Then Return 单项规则.上下文总量Tokens
        Next

        Return 通用上下文总量Tokens
    End Function

    Public Shared Function 获取上下文压缩专用模型表() As List(Of String)
        Return 上下文压缩专用模型.
            Where(Function(x) Not String.IsNullOrWhiteSpace(x)).
            Select(Function(x) x.Trim()).
            Distinct(StringComparer.OrdinalIgnoreCase).
            ToList()
    End Function

    Public Shared Function 选择上下文压缩模型(availableModels As IEnumerable(Of AgentModelInfo),
                                          currentModelId As String) As String
        Dim currentModel = If(currentModelId, "").Trim()
        Dim modelIds = If(availableModels, Enumerable.Empty(Of AgentModelInfo)()).
            Where(Function(x) x IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(x.Id)).
            Select(Function(x) x.Id.Trim()).
            ToList()

        For Each candidate In 获取上下文压缩专用模型表()
            Dim matched = modelIds.FirstOrDefault(Function(x) String.Equals(x, candidate, StringComparison.OrdinalIgnoreCase))
            If Not String.IsNullOrWhiteSpace(matched) Then Return matched
        Next

        Return currentModel
    End Function

    Private Shared Function 精确(modelId As String, contextWindowTokens As Integer) As 上下文总量规则项
        Return New 上下文总量规则项 With {
            .模型ID = If(modelId, ""),
            .上下文总量Tokens = Math.Max(0, contextWindowTokens)
        }
    End Function

    Private Shared Function 前缀(modelPrefix As String, contextWindowTokens As Integer) As 上下文总量规则项
        Return New 上下文总量规则项 With {
            .模型前缀 = If(modelPrefix, ""),
            .上下文总量Tokens = Math.Max(0, contextWindowTokens)
        }
    End Function

    Private Class 上下文总量规则项
        Public Property 模型ID As String = ""
        Public Property 模型前缀 As String = ""
        Public Property 上下文总量Tokens As Integer

        Public Function 匹配(modelId As String) As Boolean
            Dim value = If(modelId, "").Trim()
            If value = "" Then Return False
            If Not String.IsNullOrWhiteSpace(模型ID) AndAlso String.Equals(value, 模型ID.Trim(), StringComparison.OrdinalIgnoreCase) Then Return True
            If Not String.IsNullOrWhiteSpace(模型前缀) AndAlso value.StartsWith(模型前缀.Trim(), StringComparison.OrdinalIgnoreCase) Then Return True
            Return False
        End Function
    End Class
End Class
