Imports System.IO
Imports System.Text
Imports System.Text.Json
Imports System.Text.Json.Serialization
Imports System.Text.RegularExpressions

Friend NotInheritable Class Agent自定义模型配置_v6
    Friend Const 配置文件名 As String = "CustomModels.json"

    Private Const 配置文件版本 As Integer = 1
    Private Const 最大配置文件字节数 As Long = 1024 * 1024
    Private Shared ReadOnly 配置文件路径 As String = Path.Combine(Application.StartupPath, "Agent", 配置文件名)
    Private Shared ReadOnly JsonOptions As New JsonSerializerOptions With {
        .PropertyNameCaseInsensitive = True,
        .ReadCommentHandling = JsonCommentHandling.Skip,
        .AllowTrailingCommas = True
    }

    Private Sub New()
    End Sub

    Friend Shared Function 加载(client As AgentEndpointClient) As 加载结果
        Dim result As New 加载结果
        If Not File.Exists(配置文件路径) Then Return result

        result.配置文件存在 = True
        Try
            Dim fileInfo As New FileInfo(配置文件路径)
            If fileInfo.Length > 最大配置文件字节数 Then Throw New FormatException("文件大小不能超过 1 MB")

            Dim currentEndpoint = AgentEndpointClient.NormalizeEndpoint(client?.Endpoint)
            result.Models = 解析配置(File.ReadAllText(配置文件路径, Encoding.UTF8), currentEndpoint)
        Catch ex As Exception
            result.ErrorMessage = Agent通用工具_v6.LimitText(Regex.Replace(If(ex.Message, ""), "\s+", " ").Trim(), 300)
        End Try
        Return result
    End Function

    Friend Shared Function 合并模型(endpointModels As IEnumerable(Of AgentModelInfo),
                                  customModels As IEnumerable(Of AgentModelInfo)) As List(Of AgentModelInfo)
        Dim result As New List(Of AgentModelInfo)
        For Each model In If(endpointModels, Enumerable.Empty(Of AgentModelInfo))
            合并模型项(result, model)
        Next
        For Each model In If(customModels, Enumerable.Empty(Of AgentModelInfo))
            合并模型项(result, model)
        Next
        Return result
    End Function

    Private Shared Function 解析配置(json As String, currentEndpoint As String) As List(Of AgentModelInfo)
        Dim config = JsonSerializer.Deserialize(Of 自定义模型配置文件)(json, JsonOptions)
        If config Is Nothing Then Throw New FormatException("配置内容为空")
        If config.Version <> 配置文件版本 Then Throw New FormatException($"不支持的 version：{config.Version}")

        Dim models As New List(Of AgentModelInfo)
        合并范围(models, config.GlobalModels)
        For Each endpointConfig In If(config.Endpoints, New List(Of 端点模型配置))
            If endpointConfig Is Nothing OrElse Not 端点匹配(endpointConfig.Endpoint, currentEndpoint) Then Continue For
            合并范围(models, endpointConfig)
        Next
        Return models
    End Function

    Private Shared Sub 合并范围(target As List(Of AgentModelInfo), scope As 自定义模型范围)
        If scope?.Models Is Nothing Then Return
        For Each item In scope.Models
            If item Is Nothing OrElse String.IsNullOrWhiteSpace(item.Id) Then Continue For
            If item.ContextWindowTokens < 0 Then Throw New FormatException($"模型 {item.Id.Trim()} 的 context_window_tokens 不能小于 0")
            合并模型项(target, New AgentModelInfo With {
                .Id = item.Id.Trim(),
                .ReasoningEfforts = 规范字符串列表(item.ReasoningEfforts),
                .ContextWindowTokens = item.ContextWindowTokens
            })
        Next
    End Sub

    Private Shared Sub 合并模型项(target As List(Of AgentModelInfo), source As AgentModelInfo)
        If source Is Nothing OrElse String.IsNullOrWhiteSpace(source.Id) Then Return

        Dim modelId = source.Id.Trim()
        Dim existing = target.FirstOrDefault(Function(x) String.Equals(x.Id, modelId, StringComparison.OrdinalIgnoreCase))
        If existing Is Nothing Then
            existing = New AgentModelInfo With {.Id = modelId, .RawJson = If(source.RawJson, "")}
            target.Add(existing)
        ElseIf String.IsNullOrWhiteSpace(existing.RawJson) Then
            existing.RawJson = If(source.RawJson, "")
        End If

        existing.SupportedEndpointTypes = 合并字符串列表(existing.SupportedEndpointTypes, source.SupportedEndpointTypes)
        existing.ReasoningEfforts = 合并字符串列表(existing.ReasoningEfforts, source.ReasoningEfforts)
        If source.ContextWindowTokens > 0 Then existing.ContextWindowTokens = source.ContextWindowTokens
    End Sub

    Private Shared Function 合并字符串列表(first As IEnumerable(Of String), second As IEnumerable(Of String)) As List(Of String)
        Return 规范字符串列表(If(first, Enumerable.Empty(Of String)).Concat(If(second, Enumerable.Empty(Of String))))
    End Function

    Private Shared Function 规范字符串列表(values As IEnumerable(Of String)) As List(Of String)
        If values Is Nothing Then Return New List(Of String)
        Return values.
            Where(Function(x) Not String.IsNullOrWhiteSpace(x)).
            Select(Function(x) x.Trim()).
            Distinct(StringComparer.OrdinalIgnoreCase).
            ToList()
    End Function

    Private Shared Function 端点匹配(configuredEndpoint As String, currentEndpoint As String) As Boolean
        Dim configured = AgentEndpointClient.NormalizeEndpoint(configuredEndpoint)
        Dim current = AgentEndpointClient.NormalizeEndpoint(currentEndpoint)
        Return configured <> "" AndAlso String.Equals(configured, current, StringComparison.OrdinalIgnoreCase)
    End Function

    Friend Class 加载结果
        Friend Property 配置文件存在 As Boolean
        Friend Property Models As New List(Of AgentModelInfo)
        Friend Property ErrorMessage As String = ""
    End Class

    Private Class 自定义模型配置文件
        <JsonPropertyName("version")>
        Public Property Version As Integer = 配置文件版本

        <JsonPropertyName("global")>
        Public Property GlobalModels As 自定义模型范围

        <JsonPropertyName("endpoints")>
        Public Property Endpoints As New List(Of 端点模型配置)
    End Class

    Private Class 自定义模型范围
        <JsonPropertyName("models")>
        Public Property Models As New List(Of 自定义模型项)
    End Class

    Private Class 端点模型配置
        Inherits 自定义模型范围

        <JsonPropertyName("endpoint")>
        Public Property Endpoint As String = ""
    End Class

    Private Class 自定义模型项
        <JsonPropertyName("id")>
        Public Property Id As String = ""

        <JsonPropertyName("reasoning_efforts")>
        Public Property ReasoningEfforts As New List(Of String)

        <JsonPropertyName("context_window_tokens")>
        Public Property ContextWindowTokens As Integer
    End Class
End Class
