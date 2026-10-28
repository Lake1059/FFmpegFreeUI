Imports System.IO
Imports System.Text
Imports System.Text.Json
Imports System.Text.RegularExpressions

Public NotInheritable Class Agent技能资料库_v6
    Private Sub New()
    End Sub

    Public Shared Function 列出技能(Optional 程序目录 As String = Nothing) As String
        Try
            Return JsonSerializer.Serialize(New Dictionary(Of String, Object) From {
                {"skills", 获取技能元数据(程序目录)}
            }, JsonSO)
        Catch ex As Exception When TypeOf ex Is IOException OrElse TypeOf ex Is UnauthorizedAccessException OrElse TypeOf ex Is ArgumentException
            Return JsonSerializer.Serialize(New Dictionary(Of String, Object) From {{"error", "无法读取 Agent 技能目录：" & ex.Message}}, JsonSO)
        End Try
    End Function

    Public Shared Function 构建技能索引() As String
        Try
            Dim sb As New StringBuilder("当前可用技能（读取 Agent/skills 的磁盘内容）：")
            sb.AppendLine()
            For Each skill In 获取技能元数据()
                sb.Append("- ").Append(CStr(skill("name"))).Append("：").AppendLine(CStr(skill("description")))
            Next
            sb.AppendLine("使用 list_agent_skills 查看 references，再用 read_agent_skill_reference 按需读取。")
            Return sb.ToString()
        Catch ex As Exception When TypeOf ex Is IOException OrElse TypeOf ex Is UnauthorizedAccessException OrElse TypeOf ex Is ArgumentException
            Return "Agent 技能目录读取失败：" & ex.Message & "。不得假定未读到的技能内容。"
        End Try
    End Function

    Public Shared Function 读取资料(skillName As String, referenceName As String, Optional 程序目录 As String = Nothing) As String
        Try
            Agent文件管理_v6.安装内置文件(程序目录)
            Dim name = 规范化技能名(skillName)
            Dim root = Agent文件管理_v6.获取Agent目录(程序目录)
            Dim skillRoot = Agent文件管理_v6.获取安全路径(root, "skills/" & name)
            Dim reference = 规范化引用名(referenceName, name = "ffmpegfreeui")
            Dim extension = Path.GetExtension(reference)
            If Not extension.Equals(".md", StringComparison.OrdinalIgnoreCase) AndAlso Not extension.Equals(".json", StringComparison.OrdinalIgnoreCase) Then
                Throw New ArgumentException("技能资料仅支持 Markdown 和 JSON 文件。")
            End If
            Return Agent文件管理_v6.读取文本(skillRoot, reference)
        Catch ex As Exception When TypeOf ex Is IOException OrElse TypeOf ex Is UnauthorizedAccessException OrElse TypeOf ex Is ArgumentException
            Return "无法读取 Agent 技能资料：" & ex.Message & "。请调用 list_agent_skills 核对当前技能与 reference。"
        End Try
    End Function

    Private Shared Function 获取技能元数据(Optional 程序目录 As String = Nothing) As List(Of Dictionary(Of String, Object))
        Agent文件管理_v6.安装内置文件(程序目录)
        Dim root = Agent文件管理_v6.获取安全路径(Agent文件管理_v6.获取Agent目录(程序目录), "skills")
        Dim result As New List(Of Dictionary(Of String, Object))()
        For Each directory In IO.Directory.EnumerateDirectories(root).OrderBy(Function(value) value, StringComparer.OrdinalIgnoreCase)
            If (File.GetAttributes(directory) And FileAttributes.ReparsePoint) <> 0 Then Continue For
            Dim name = Path.GetFileName(directory)
            If Not Regex.IsMatch(name, "^[a-zA-Z0-9][a-zA-Z0-9_-]{0,63}$", RegexOptions.CultureInvariant) Then Continue For
            If Not File.Exists(Path.Combine(directory, "SKILL.md")) Then Continue For
            Dim text = Agent文件管理_v6.读取文本(directory, "SKILL.md")
            Dim metadata = Regex.Match(text, "\A---\r?\n(?<yaml>.*?)\r?\n---(?:\r?\n|$)", RegexOptions.Singleline Or RegexOptions.CultureInvariant)
            Dim description = Regex.Match(metadata.Groups("yaml").Value, "(?m)^description:\s*(?<value>[^\r\n]+)", RegexOptions.CultureInvariant).Groups("value").Value.Trim().Trim(""""c, "'"c)
            If description = "" Then description = "用户自定义技能：" & name
            Dim references As New List(Of Dictionary(Of String, Object))()
            Dim options As New EnumerationOptions With {.RecurseSubdirectories = True, .AttributesToSkip = FileAttributes.ReparsePoint, .IgnoreInaccessible = False}
            For Each filePath In IO.Directory.EnumerateFiles(directory, "*", options).OrderBy(Function(value) value, StringComparer.OrdinalIgnoreCase)
                Dim extension = Path.GetExtension(filePath)
                If Not extension.Equals(".md", StringComparison.OrdinalIgnoreCase) AndAlso Not extension.Equals(".json", StringComparison.OrdinalIgnoreCase) Then Continue For
                Dim relative = Path.GetRelativePath(directory, filePath).Replace("\"c, "/"c)
                references.Add(New Dictionary(Of String, Object) From {{"reference", relative}, {"title", Path.GetFileNameWithoutExtension(filePath)}})
            Next
            result.Add(New Dictionary(Of String, Object) From {
                {"name", name},
                {"aliases", If(name.Equals("ffmpegfreeui", StringComparison.OrdinalIgnoreCase), New String() {"3fui", "ffmpeg-free-ui"}, Array.Empty(Of String)())},
                {"description", Agent通用工具_v6.LimitText(description, 1000)},
                {"path", directory},
                {"references", references}
            })
        Next
        Return result
    End Function

    Private Shared Function 规范化技能名(value As String) As String
        Dim name = If(value, "").Trim().ToLowerInvariant()
        If name = "" OrElse name = "3fui" OrElse name = "ffmpeg-free-ui" Then name = "ffmpegfreeui"
        If Not Regex.IsMatch(name, "^[a-z0-9][a-z0-9_-]{0,63}$", RegexOptions.CultureInvariant) Then Throw New ArgumentException("无效的 skill 名称。")
        Return name
    End Function

    Private Shared Function 规范化引用名(value As String, 兼容旧引用 As Boolean) As String
        Dim reference = If(value, "").Trim().Replace("\"c, "/"c)
        Dim key = reference.ToLowerInvariant()
        If 兼容旧引用 Then
            If key.StartsWith("references/", StringComparison.Ordinal) Then key = key.Substring("references/".Length)
            If key.EndsWith(".md", StringComparison.Ordinal) Then key = key.Substring(0, key.Length - 3)
            key = key.Replace("_"c, "-"c)
            Select Case key
                Case "environment-workflow", "permissions-network", "agent-tools", "parameter-panel", "command-generation",
                     "filters-streams", "queue-integrated-tools", "stream-extraction", "presets-files", "recommendations"
                    Return "references/" & key & ".md"
            End Select
        End If
        Select Case key
            Case "", "skill", "overview", "skill.md"
                Return "SKILL.md"
            Case "tools", "agent_tools"
                Return "references/agent-tools.md"
            Case "dovi", "dolbyvision", "dolby-vision"
                Return "references/dolby-vision.md"
        End Select
        If Not reference.Contains("/"c) Then
            If Path.GetExtension(reference) = "" Then reference = reference.Replace("_"c, "-"c) & ".md"
            reference = "references/" & reference
        End If
        Return reference
    End Function
End Class
