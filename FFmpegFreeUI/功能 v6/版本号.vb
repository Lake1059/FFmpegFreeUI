Public Class 版本号

    Public Shared Function 获取自身版本号() As String
        Dim EntryAssembly = System.Reflection.Assembly.GetEntryAssembly()
        Dim Version As String = Nothing

        If EntryAssembly IsNot Nothing Then
            For Each AssemblyAttribute In EntryAssembly.GetCustomAttributes(GetType(System.Reflection.AssemblyInformationalVersionAttribute), False)
                Dim InformationalVersionAttribute = TryCast(AssemblyAttribute, System.Reflection.AssemblyInformationalVersionAttribute)

                If InformationalVersionAttribute IsNot Nothing Then
                    Version = InformationalVersionAttribute.InformationalVersion
                    Exit For
                End If
            Next
        End If

        If String.IsNullOrWhiteSpace(Version) Then
            Version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version?.ToString()
        End If

        Return 清理版本号(Version)
    End Function

    Public Shared Function 获取外部程序文件版本号(文件路径 As String) As String
        If String.IsNullOrWhiteSpace(文件路径) OrElse Not System.IO.File.Exists(文件路径) Then Return ""

        Dim VersionInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo(文件路径)
        Dim Version = If(Not String.IsNullOrWhiteSpace(VersionInfo.ProductVersion), VersionInfo.ProductVersion, VersionInfo.FileVersion)

        Return 清理版本号(Version)
    End Function

    Public Shared Function CompareVersion(A As String, B As String) As Integer
        Dim PartsA = ExtractVersionParts(A)
        Dim PartsB = ExtractVersionParts(B)
        Dim MaxCount = Math.Max(PartsA.Count, PartsB.Count)

        For Index = 0 To MaxCount - 1
            Dim PartA = If(Index < PartsA.Count, PartsA(Index), "0")
            Dim PartB = If(Index < PartsB.Count, PartsB(Index), "0")
            Dim Result = CompareVersionPart(PartA, PartB)

            If Result <> 0 Then Return Result
        Next

        Return 0
    End Function

    Private Shared Function ExtractVersionParts(Version As String) As List(Of String)
        Dim Parts As New List(Of String)

        If String.IsNullOrWhiteSpace(Version) Then Return Parts

        For Each Match As System.Text.RegularExpressions.Match In System.Text.RegularExpressions.Regex.Matches(Version, "\d+")
            Parts.Add(Match.Value)
        Next

        Return Parts
    End Function

    Private Shared Function 清理版本号(Version As String) As String
        If String.IsNullOrWhiteSpace(Version) Then Return ""

        Return Version.Split("+"c)(0).Trim()
    End Function

    Private Shared Function CompareVersionPart(A As String, B As String) As Integer
        A = A.TrimStart("0"c)
        B = B.TrimStart("0"c)

        If A.Length = 0 Then A = "0"
        If B.Length = 0 Then B = "0"

        If A.Length <> B.Length Then Return If(A.Length < B.Length, -1, 1)

        Dim Result = String.CompareOrdinal(A, B)
        If Result = 0 Then Return 0

        Return If(Result < 0, -1, 1)
    End Function

End Class
