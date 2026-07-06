Public NotInheritable Class Agent推理级别能力表_v6
    Private Sub New()
    End Sub

    ' 在这里维护端点未显式返回时，已知模型额外支持的推理级别。
    ' 示例：
    '   前缀("gpt-5.5", "xhigh")
    '   精确("some-model-id", "xhigh")
    Private Shared ReadOnly 规则 As 推理级别规则() = {
        前缀("gpt-5", "xhigh")
    }

    Public Shared Function 获取额外推理级别(modelId As String) As List(Of String)
        Dim result As New List(Of String)

        For Each 单项规则 In 规则
            If 单项规则 Is Nothing OrElse Not 单项规则.匹配(modelId) OrElse 单项规则.额外推理级别 Is Nothing Then Continue For
            result.AddRange(单项规则.额外推理级别)
        Next

        Return result.
            Where(Function(x) Not String.IsNullOrWhiteSpace(x)).
            Select(Function(x) x.Trim()).
            Distinct(StringComparer.OrdinalIgnoreCase).
            ToList()
    End Function

    Private Shared Function 精确(modelId As String, ParamArray 额外推理级别 As String()) As 推理级别规则
        Return New 推理级别规则 With {
            .模型ID = If(modelId, ""),
            .额外推理级别 = If(额外推理级别, Array.Empty(Of String)())
        }
    End Function

    Private Shared Function 前缀(modelPrefix As String, ParamArray 额外推理级别 As String()) As 推理级别规则
        Return New 推理级别规则 With {
            .模型前缀 = If(modelPrefix, ""),
            .额外推理级别 = If(额外推理级别, Array.Empty(Of String)())
        }
    End Function

    Private Class 推理级别规则
        Public Property 模型ID As String = ""
        Public Property 模型前缀 As String = ""
        Public Property 额外推理级别 As String() = Array.Empty(Of String)()

        Public Function 匹配(modelId As String) As Boolean
            Dim value = If(modelId, "").Trim()
            If value = "" Then Return False
            If Not String.IsNullOrWhiteSpace(模型ID) AndAlso String.Equals(value, 模型ID.Trim(), StringComparison.OrdinalIgnoreCase) Then Return True
            If Not String.IsNullOrWhiteSpace(模型前缀) AndAlso value.StartsWith(模型前缀.Trim(), StringComparison.OrdinalIgnoreCase) Then Return True
            Return False
        End Function
    End Class
End Class
