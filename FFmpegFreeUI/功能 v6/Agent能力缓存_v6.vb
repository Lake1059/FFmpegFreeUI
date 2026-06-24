Imports System.IO
Imports System.Text
Imports System.Text.Json

Public Class AgentCapabilityCache
    Private Shared ReadOnly SyncRoot As New Object
    Private Shared ReadOnly DefaultReasoningEfforts As String() = {"low", "medium", "high"}
    Private Const ReasoningEffortCacheVersion As Integer = 5
    Private Shared ReadOnly CacheDirectory As String = Path.Combine(Application.StartupPath, "Agent")
    Private Shared ReadOnly ReasoningEffortCachePath As String = Path.Combine(CacheDirectory, "ReasoningEfforts.cache.json")
    Private Shared _cacheFile As AgentReasoningEffortCacheFile = Nothing
    Private Shared _cacheLoaded As Boolean = False

    Private Class AgentReasoningEffortCacheFile
        Public Property Version As Integer = ReasoningEffortCacheVersion
        Public Property Models As New Dictionary(Of String, AgentReasoningEffortModelCache)(StringComparer.OrdinalIgnoreCase)
        Public Property EndpointRefreshes As New Dictionary(Of String, AgentReasoningEffortEndpointRefresh)(StringComparer.OrdinalIgnoreCase)
    End Class

    Private Class AgentReasoningEffortModelCache
        Public Property ReasoningEfforts As New List(Of String)
        Public Property UpdatedAt As DateTime = DateTime.Now
    End Class

    Private Class AgentReasoningEffortEndpointRefresh
        Public Property RefreshedDate As String = ""
        Public Property RefreshedAt As DateTime = DateTime.MinValue
    End Class

    Public Shared Function GetDefaultReasoningEfforts(Optional modelId As String = "") As List(Of String)
        Return ResolveReasoningEfforts(modelId, Nothing)
    End Function

    Public Shared Function GetReasoningEffortsAsync(client As AgentEndpointClient,
                                                    model As AgentModelInfo,
                                                    Optional cancellationToken As Threading.CancellationToken = Nothing) As Task(Of List(Of String))
        cancellationToken.ThrowIfCancellationRequested()
        If model Is Nothing OrElse String.IsNullOrWhiteSpace(model.Id) Then Return Task.FromResult(New List(Of String))
        If model.ReasoningEfforts IsNot Nothing AndAlso model.ReasoningEfforts.Count > 0 Then
            Dim efforts = ResolveReasoningEfforts(model.Id, model.ReasoningEfforts)
            model.ReasoningEfforts = efforts
            SaveReasoningEfforts(client, model.Id, efforts)
            Return Task.FromResult(efforts)
        End If

        Dim cached = GetCachedReasoningEfforts(client, model.Id)
        If cached.Count > 0 Then
            model.ReasoningEfforts = cached
            Return Task.FromResult(cached)
        End If

        Dim result = GetDefaultReasoningEfforts(model.Id)
        Return Task.FromResult(result)
    End Function

    Public Shared Function BuildEndpointSignature(client As AgentEndpointClient) As String
        If client Is Nothing Then Return ""
        Return String.Join(vbLf, {
            If(client.Endpoint, "").Trim(),
            If(client.ApiKey, "").Trim(),
            If(client.ExtraHeaders Is Nothing, "", String.Join(vbLf, client.ExtraHeaders.OrderBy(Function(x) x.Key).Select(Function(x) x.Key & ":" & x.Value)))
        })
    End Function

    Public Shared Function IsDailyReasoningRefreshDue(client As AgentEndpointClient) As Boolean
        Dim endpointKey = BuildEndpointKey(client)
        If endpointKey = "" Then Return False

        SyncLock SyncRoot
            Dim cacheFile = LoadCacheFile()
            Dim today = GetTodayKey()
            Dim refresh As AgentReasoningEffortEndpointRefresh = Nothing
            cacheFile.EndpointRefreshes.TryGetValue(endpointKey, refresh)
            Return refresh Is Nothing OrElse Not String.Equals(If(refresh.RefreshedDate, ""), today, StringComparison.Ordinal)
        End SyncLock
    End Function

    Public Shared Sub UseDefaultReasoningEfforts(client As AgentEndpointClient,
                                                 models As IEnumerable(Of AgentModelInfo),
                                                 Optional selectedModelId As String = "")
        Dim endpointKey = BuildEndpointKey(client)

        SyncLock SyncRoot
            Dim cacheFile = LoadCacheFile()
            Dim hasModel = False

            If models IsNot Nothing Then
                For Each model In models
                    If model Is Nothing OrElse String.IsNullOrWhiteSpace(model.Id) Then Continue For
                    Dim efforts = ResolveReasoningEfforts(model.Id, Nothing)
                    model.ReasoningEfforts = efforts
                    UpsertReasoningEffortEntry(cacheFile, model.Id, efforts)
                    hasModel = True
                Next
            End If

            If Not hasModel AndAlso Not String.IsNullOrWhiteSpace(selectedModelId) Then
                UpsertReasoningEffortEntry(cacheFile, selectedModelId.Trim(), GetDefaultReasoningEfforts(selectedModelId))
            End If

            If endpointKey <> "" Then MarkEndpointRefreshed(cacheFile, endpointKey)
            SaveCacheFile()
        End SyncLock
    End Sub

    Public Shared Sub ImportReasoningEfforts(client As AgentEndpointClient, models As IEnumerable(Of AgentModelInfo))
        Dim endpointKey = BuildEndpointKey(client)

        SyncLock SyncRoot
            Dim cacheFile = LoadCacheFile()
            If models IsNot Nothing Then
                For Each model In models
                    If model Is Nothing OrElse String.IsNullOrWhiteSpace(model.Id) Then Continue For
                    Dim efforts = ResolveReasoningEfforts(model.Id, model.ReasoningEfforts)
                    model.ReasoningEfforts = efforts
                    UpsertReasoningEffortEntry(cacheFile, model.Id, efforts)
                Next
            End If
            If endpointKey <> "" Then MarkEndpointRefreshed(cacheFile, endpointKey)
            SaveCacheFile()
        End SyncLock
    End Sub

    Private Shared Function GetCachedReasoningEfforts(client As AgentEndpointClient, modelId As String) As List(Of String)
        If String.IsNullOrWhiteSpace(modelId) Then Return New List(Of String)

        SyncLock SyncRoot
            Dim cacheFile = LoadCacheFile()
            Dim entry As AgentReasoningEffortModelCache = Nothing
            cacheFile.Models.TryGetValue(modelId.Trim(), entry)
            If entry Is Nothing Then Return New List(Of String)
            Return ResolveReasoningEfforts(modelId, entry.ReasoningEfforts)
        End SyncLock
    End Function

    Private Shared Sub SaveReasoningEfforts(client As AgentEndpointClient, modelId As String, efforts As IEnumerable(Of String))
        Dim normalized = ResolveReasoningEfforts(modelId, efforts)
        If String.IsNullOrWhiteSpace(modelId) OrElse normalized.Count = 0 Then Return

        SyncLock SyncRoot
            Dim cacheFile = LoadCacheFile()
            UpsertReasoningEffortEntry(cacheFile, modelId, normalized)
            SaveCacheFile()
        End SyncLock
    End Sub

    Private Shared Sub UpsertReasoningEffortEntry(cacheFile As AgentReasoningEffortCacheFile,
                                                  modelId As String,
                                                  efforts As List(Of String))
        If cacheFile.Models Is Nothing Then cacheFile.Models = New Dictionary(Of String, AgentReasoningEffortModelCache)(StringComparer.OrdinalIgnoreCase)
        Dim key = modelId.Trim()
        Dim entry As AgentReasoningEffortModelCache = Nothing
        If Not cacheFile.Models.TryGetValue(key, entry) Then
            entry = New AgentReasoningEffortModelCache()
            cacheFile.Models(key) = entry
        End If

        entry.ReasoningEfforts = efforts
        entry.UpdatedAt = DateTime.Now
    End Sub

    Private Shared Sub MarkEndpointRefreshed(cacheFile As AgentReasoningEffortCacheFile, endpointKey As String)
        If cacheFile.EndpointRefreshes Is Nothing Then cacheFile.EndpointRefreshes = New Dictionary(Of String, AgentReasoningEffortEndpointRefresh)(StringComparer.OrdinalIgnoreCase)
        Dim refresh As AgentReasoningEffortEndpointRefresh = Nothing
        If Not cacheFile.EndpointRefreshes.TryGetValue(endpointKey, refresh) Then
            refresh = New AgentReasoningEffortEndpointRefresh()
            cacheFile.EndpointRefreshes(endpointKey) = refresh
        End If

        refresh.RefreshedDate = GetTodayKey()
        refresh.RefreshedAt = DateTime.Now
    End Sub

    Private Shared Function LoadCacheFile() As AgentReasoningEffortCacheFile
        If _cacheLoaded AndAlso _cacheFile IsNot Nothing Then Return _cacheFile

        Try
            If IO.File.Exists(ReasoningEffortCachePath) Then
                _cacheFile = JsonSerializer.Deserialize(Of AgentReasoningEffortCacheFile)(IO.File.ReadAllText(ReasoningEffortCachePath, Encoding.UTF8), JsonSO)
                If _cacheFile Is Nothing OrElse _cacheFile.Version <> ReasoningEffortCacheVersion Then
                    DeleteOldCacheFile()
                    _cacheFile = Nothing
                End If
            End If
        Catch
            DeleteOldCacheFile()
            _cacheFile = Nothing
        End Try

        If _cacheFile Is Nothing Then _cacheFile = New AgentReasoningEffortCacheFile
        _cacheFile.Version = ReasoningEffortCacheVersion
        If _cacheFile.Models Is Nothing Then _cacheFile.Models = New Dictionary(Of String, AgentReasoningEffortModelCache)(StringComparer.OrdinalIgnoreCase)
        If _cacheFile.EndpointRefreshes Is Nothing Then _cacheFile.EndpointRefreshes = New Dictionary(Of String, AgentReasoningEffortEndpointRefresh)(StringComparer.OrdinalIgnoreCase)
        _cacheLoaded = True
        Return _cacheFile
    End Function

    Private Shared Sub DeleteOldCacheFile()
        Try
            If IO.File.Exists(ReasoningEffortCachePath) Then IO.File.Delete(ReasoningEffortCachePath)
        Catch
        End Try
    End Sub

    Private Shared Sub SaveCacheFile()
        Try
            Directory.CreateDirectory(CacheDirectory)
            IO.File.WriteAllText(ReasoningEffortCachePath, JsonSerializer.Serialize(LoadCacheFile(), JsonSO), Encoding.UTF8)
        Catch
        End Try
    End Sub

    Private Shared Function BuildEndpointKey(client As AgentEndpointClient) As String
        If client Is Nothing OrElse String.IsNullOrWhiteSpace(client.Endpoint) Then Return ""
        Dim signature = BuildEndpointSignature(client)
        If String.IsNullOrWhiteSpace(signature) Then Return ""
        Return Convert.ToHexString(System.Security.Cryptography.SHA256.HashData(Encoding.UTF8.GetBytes(signature)))
    End Function

    Private Shared Function NormalizeReasoningEfforts(efforts As IEnumerable(Of String)) As List(Of String)
        If efforts Is Nothing Then Return New List(Of String)
        Return efforts.
            Where(Function(x) Not String.IsNullOrWhiteSpace(x)).
            Select(Function(x) x.Trim()).
            Distinct(StringComparer.OrdinalIgnoreCase).
            ToList()
    End Function

    Private Shared Function ResolveReasoningEfforts(modelId As String, efforts As IEnumerable(Of String)) As List(Of String)
        Dim result = NormalizeReasoningEfforts(efforts)
        If result.Count = 0 Then result.AddRange(DefaultReasoningEfforts)

        For Each extra In GetAdditionalReasoningEfforts(modelId)
            If Not result.Any(Function(x) String.Equals(x, extra, StringComparison.OrdinalIgnoreCase)) Then result.Add(extra)
        Next

        Return NormalizeReasoningEfforts(result)
    End Function

    Private Shared Function GetAdditionalReasoningEfforts(modelId As String) As List(Of String)
        Return NormalizeReasoningEfforts(Agent推理级别能力表_v6.获取额外推理级别(modelId))
    End Function

    Private Shared Function GetTodayKey() As String
        Return DateTime.Today.ToString("yyyy-MM-dd", Globalization.CultureInfo.InvariantCulture)
    End Function
End Class
