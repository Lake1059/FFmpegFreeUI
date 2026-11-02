Imports System.IO
Imports System.Text

Partial Public NotInheritable Class Agent技能资料库_v6
    Private Class 用户技能
        Public Property Name As String
        Public Property EntryPath As String
        Public Property RootDirectory As String
        Public Property IsSingleFile As Boolean
    End Class

    Private Shared Function 查找用户技能() As List(Of 用户技能)
        Dim root = Agent自定义内容_v6.技能目录
        Directory.CreateDirectory(root)
        ' 在枚举前也验证根目录，不能先跟随用户目录联接。
        验证技能路径(root, root)
        Dim result As New List(Of 用户技能)
        For Each filePath In Directory.EnumerateFiles(root, "*.md").OrderBy(Function(p) p, StringComparer.OrdinalIgnoreCase)
            result.Add(New 用户技能 With {
                .Name = "custom/" & Path.GetFileName(filePath), .EntryPath = filePath,
                .RootDirectory = root, .IsSingleFile = True
            })
        Next
        For Each folder In Directory.EnumerateDirectories(root).OrderBy(Function(p) p, StringComparer.OrdinalIgnoreCase)
            If (File.GetAttributes(folder) And FileAttributes.ReparsePoint) <> 0 Then Continue For
            Dim entry = Path.Combine(folder, "SKILL.md")
            If Not File.Exists(entry) Then Continue For
            result.Add(New 用户技能 With {
                .Name = "custom/" & Path.GetFileName(folder) & "/SKILL.md", .EntryPath = entry,
                .RootDirectory = folder, .IsSingleFile = False
            })
        Next
        Return result
    End Function

    Private Shared Function 获取用户技能元数据(errors As List(Of String)) As List(Of Dictionary(Of String, Object))
        Dim result As New List(Of Dictionary(Of String, Object))
        Try
            For Each skill In 查找用户技能()
                Try
                    验证技能路径(skill.EntryPath, skill.RootDirectory)
                    Dim title = Path.GetFileNameWithoutExtension(If(skill.IsSingleFile, skill.EntryPath, skill.RootDirectory))
                    Dim description = "用户自定义技能，按需读取 SKILL.md。"
                    ' 只读取头部作为索引，避免把技能正文提前装入上下文。
                    Using reader As New StreamReader(skill.EntryPath, Encoding.UTF8, True)
                        Dim header(4095) As Char
                        Dim count = reader.ReadBlock(header, 0, header.Length)
                        Dim lines = New String(header, 0, count).Replace(vbCrLf, vbLf).Replace(vbCr, vbLf).Split(ControlChars.Lf)
                        If lines.Length > 0 AndAlso lines(0).Trim() = "---" Then
                            For Each line In lines.Skip(1)
                                If line.Trim() = "---" Then Exit For
                                If line.StartsWith("name:", StringComparison.OrdinalIgnoreCase) Then title = 清理元数据(line.Substring(5))
                                If line.StartsWith("description:", StringComparison.OrdinalIgnoreCase) Then description = 清理元数据(line.Substring(12))
                            Next
                        End If
                    End Using
                    result.Add(New Dictionary(Of String, Object) From {
                        {"name", skill.Name}, {"source", "user"},
                        {"title", Agent通用工具_v6.LimitText(title, 80)},
                        {"description", Agent通用工具_v6.LimitText(description, 240)},
                        {"references", New Object() {引用("SKILL.md", "技能入口", If(skill.IsSingleFile, "读取此 Markdown 文件。", "可继续按入口中的相对路径读取本技能目录内的 Markdown reference。"))}}
                    })
                Catch ex As Exception When TypeOf ex Is IOException OrElse TypeOf ex Is UnauthorizedAccessException
                    errors.Add(Agent通用工具_v6.LimitText(skill.Name & "：" & ex.Message, 300))
                End Try
            Next
        Catch ex As Exception When TypeOf ex Is IOException OrElse TypeOf ex Is UnauthorizedAccessException
            errors.Add("读取用户技能目录失败：" & Agent通用工具_v6.LimitText(ex.Message, 300))
        End Try
        Return result
    End Function

    Private Shared Function 清理元数据(value As String) As String
        Return value.Trim().Trim(""""c, "'"c)
    End Function

    Private Shared Function 读取用户技能(skillName As String, referenceName As String,
                                        startLine As Integer, lineCount As Integer, startColumn As Integer) As String
        Dim skill = 查找用户技能().FirstOrDefault(Function(item) String.Equals(item.Name, If(skillName, "").Trim(), StringComparison.OrdinalIgnoreCase))
        If skill Is Nothing Then Return "未知 skill：" & If(skillName, "") & "。请调用 list_agent_skills 查看可用技能。"
        Dim reference = If(referenceName, "").Trim().Replace("\"c, "/"c)
        Dim filePath = skill.EntryPath
        If reference <> "" AndAlso Not String.Equals(reference, "SKILL.md", StringComparison.OrdinalIgnoreCase) Then
            If skill.IsSingleFile Then Return "单文件技能仅支持 reference=SKILL.md。"
            If Path.IsPathRooted(reference) OrElse reference.Contains(":"c) OrElse reference.Split("/"c).Any(Function(part) part = "..") Then
                Return "无效的技能 reference：必须使用技能目录内的相对 Markdown 路径。"
            End If
            filePath = Path.GetFullPath(Path.Combine(skill.RootDirectory, reference))
        End If
        If Not String.Equals(Path.GetExtension(filePath), ".md", StringComparison.OrdinalIgnoreCase) Then Return "技能接口只读取 Markdown (.md) 文件。"
        验证技能路径(filePath, skill.RootDirectory)
        If New FileInfo(filePath).Length > 8L * 1024 * 1024 Then Return "技能文件超过 8 MiB 限制，请拆分为多个 Markdown reference。"
        Using reader As New StreamReader(filePath, Encoding.UTF8, True)
            Return 分页技能文本(reader, startLine, lineCount, startColumn)
        End Using
    End Function

    ' 一次检查当前技能的路径范围，并沿父路径检查到 Skills 根目录，避免重复遍历。
    Private Shared Sub 验证技能路径(filePath As String, rootDirectory As String)
        Dim root = Path.TrimEndingDirectorySeparator(Path.GetFullPath(rootDirectory))
        Dim fullPath = Path.GetFullPath(filePath)
        If Not String.Equals(fullPath, root, StringComparison.OrdinalIgnoreCase) AndAlso
           Not fullPath.StartsWith(root & Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase) Then
            Throw New IOException("技能路径必须位于当前技能目录中。")
        End If
        Dim skillsRoot = Path.GetFullPath(Agent自定义内容_v6.技能目录)
        Dim current = fullPath
        Do
            If (File.GetAttributes(current) And FileAttributes.ReparsePoint) <> 0 Then
                Throw New IOException("技能接口不读取符号链接或目录联接。")
            End If
            If String.Equals(current, skillsRoot, StringComparison.OrdinalIgnoreCase) Then Exit Do
            current = Path.GetDirectoryName(current)
        Loop
    End Sub

    Private Shared Function 分页技能文本(content As String, startLine As Integer, lineCount As Integer, startColumn As Integer) As String
        Using reader As New StringReader(content)
            Return 分页技能文本(reader, startLine, lineCount, startColumn)
        End Using
    End Function

    ' 返回控制在工具上下文的 16000 字符截断线内，长行可用 start_column 继续。
    Private Shared Function 分页技能文本(reader As TextReader, startLine As Integer, lineCount As Integer, startColumn As Integer) As String
        startLine = Math.Max(1, startLine)
        lineCount = Math.Clamp(lineCount, 1, 500)
        startColumn = Math.Max(1, startColumn)
        Dim number = 1
        While number < startLine
            If reader.ReadLine() Is Nothing Then Return "已到文件末尾。"
            number += 1
        End While
        Dim sb As New StringBuilder
        Dim nextColumn = 1
        For i = 1 To lineCount
            If sb.Length >= 12000 Then Exit For
            Dim line = reader.ReadLine()
            If line Is Nothing Then Return sb.ToString()
            Dim column = If(i = 1, startColumn, 1)
            Dim remaining = 12000 - sb.Length
            Dim text = If(column > line.Length, "", line.Substring(column - 1))
            If text.Length > remaining Then
                sb.Append(text.AsSpan(0, remaining))
                sb.AppendLine()
                nextColumn = column + remaining
                Exit For
            End If
            sb.AppendLine(text)
            number += 1
        Next
        If nextColumn > 1 OrElse reader.Peek() >= 0 Then sb.Append($"[未完，请继续读取 start_line={number}, start_column={nextColumn}]")
        Return sb.ToString()
    End Function
End Class
