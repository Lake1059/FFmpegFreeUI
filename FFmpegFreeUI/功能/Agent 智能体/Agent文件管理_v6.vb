Imports System.IO
Imports System.Text

Public NotInheritable Class Agent文件管理_v6
    Private Sub New()
    End Sub

    Private Const 资源前缀 As String = "AgentDefaults/"
    Private Const 最大文本长度 As Integer = 1024 * 1024
    Private Shared ReadOnly 安装锁 As New Object()

    Public Shared Function 获取Agent目录(Optional 程序目录 As String = Nothing) As String
        Return Path.Combine(If(程序目录, Application.StartupPath), "Agent")
    End Function

    Public Shared Sub 安装内置文件(Optional 程序目录 As String = Nothing)
        SyncLock 安装锁
            Dim root = 获取Agent目录(程序目录)
            Dim assembly = GetType(Agent文件管理_v6).Assembly
            For Each resource In assembly.GetManifestResourceNames()
                Dim normalized = resource.Replace("\"c, "/"c)
                If Not normalized.StartsWith(资源前缀, StringComparison.Ordinal) Then Continue For
                Dim relative = normalized.Substring(资源前缀.Length)
                Dim destination = 获取安全路径(root, relative)
                If File.Exists(destination) Then Continue For
                Directory.CreateDirectory(Path.GetDirectoryName(destination))
                ' 先完整写入独立临时文件，再无覆盖地提交，避免其他实例读到半份资料。
                Dim temporary = destination & "." & Guid.NewGuid().ToString("N") & ".tmp"
                Try
                    Using input = assembly.GetManifestResourceStream(resource),
                          output As New FileStream(temporary, FileMode.CreateNew, FileAccess.Write, FileShare.None)
                        input.CopyTo(output)
                        output.Flush(True)
                    End Using
                    Try
                        File.Move(temporary, destination, False)
                    Catch ex As IOException When File.Exists(destination)
                        ' 另一个实例已完成安装，沿用磁盘内容。
                    End Try
                Finally
                    If File.Exists(temporary) Then File.Delete(temporary)
                End Try
            Next
        End SyncLock
    End Sub

    Public Shared Function 读取系统提示词(Optional 程序目录 As String = Nothing) As String
        安装内置文件(程序目录)
        Return 读取文本(获取Agent目录(程序目录), "agents.md")
    End Function

    Public Shared Function 获取安全路径(root As String, relative As String) As String
        If String.IsNullOrWhiteSpace(relative) OrElse Path.IsPathRooted(relative) Then Throw New ArgumentException("Agent 资料必须使用目录内的相对路径。")
        Dim parts = relative.Replace("\"c, "/"c).Split("/"c)
        If parts.Any(Function(part) String.IsNullOrWhiteSpace(part) OrElse part = "." OrElse part = ".." OrElse
                                   part.EndsWith("."c) OrElse part.EndsWith(" "c) OrElse
                                   part.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0 OrElse 是保留设备名称(part)) Then
            Throw New ArgumentException("Agent 资料路径包含无效或越界的目录段。")
        End If
        Dim fullRoot = Path.GetFullPath(root).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)
        Dim result = Path.GetFullPath(Path.Combine(fullRoot, Path.Combine(parts)))
        If Not result.StartsWith(fullRoot & Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase) Then Throw New ArgumentException("Agent 资料不能超出指定目录。")
        检查非链接路径(fullRoot)
        Dim current = fullRoot
        For Each part In parts
            current = Path.Combine(current, part)
            检查非链接路径(current)
        Next
        Return result
    End Function

    Private Shared Function 是保留设备名称(part As String) As Boolean
        Dim name = part.Split("."c)(0).ToUpperInvariant()
        Select Case name
            Case "CON", "PRN", "AUX", "NUL", "CONIN$", "CONOUT$"
                Return True
        End Select
        If name.Length = 4 AndAlso (name.StartsWith("COM", StringComparison.Ordinal) OrElse name.StartsWith("LPT", StringComparison.Ordinal)) Then
            Return "123456789¹²³".Contains(name(3))
        End If
        Return False
    End Function

    Private Shared Sub 检查非链接路径(path As String)
        Try
            If (File.GetAttributes(path) And FileAttributes.ReparsePoint) <> 0 Then Throw New IOException("Agent 资料不允许通过链接或目录联接访问：" & path)
        Catch ex As FileNotFoundException
        Catch ex As DirectoryNotFoundException
        End Try
    End Sub

    Public Shared Function 读取文本(root As String, relative As String) As String
        Dim path = 获取安全路径(root, relative)
        If New FileInfo(path).Length > 最大文本长度 * 4L Then Throw New IOException("Agent 资料文件过大：" & relative)
        Using reader As New StreamReader(path, Encoding.UTF8, True)
            Dim result As New StringBuilder()
            Dim buffer(4095) As Char
            Do
                Dim count = reader.Read(buffer, 0, buffer.Length)
                If count = 0 Then Exit Do
                If result.Length + count > 最大文本长度 Then Throw New IOException("Agent 资料超过一百万字符：" & relative)
                result.Append(buffer, 0, count)
            Loop
            Return result.ToString()
        End Using
    End Function
End Class
