Imports System.Net.Http
Imports System.Text.Json

Public Class GitHubAPI
    Public Class Release

        Public 发布标题 As String = ""
        Public 版本标签 As String = ""
        Public 预览版 As Boolean = False
        Public 发布描述 As String = ""
        Public 发布时间 As String = ""
        Public 发布者用户名 As String = ""
        Public 可供下载的文件 As New List(Of KeyValuePair(Of String, String))

        Public ErrorString As String = ""

        Public Async Function 获取仓库发布版信息Async(存储库 As String, Optional 指定标签 As String = "") As Task(Of String)
            Try
                ErrorString = ""
                Dim url As String = $"https://api.github.com/repos/{存储库}/releases"
                If Not String.IsNullOrEmpty(指定标签) Then
                    url = $"https://api.github.com/repos/{存储库}/releases/tags/{指定标签}"
                End If

                Dim content As String = ""
                Using client As New HttpClient()
                    client.DefaultRequestHeaders.Add("User-Agent", "github_api")
                    client.Timeout = TimeSpan.FromSeconds(10)
                    Dim response = Await client.GetAsync(url)
                    If response.IsSuccessStatusCode Then
                        content = Await response.Content.ReadAsStringAsync()
                    Else
                        ErrorString = Await response.Content.ReadAsStringAsync()
                        Return ErrorString
                    End If
                End Using

                ' 解析JSON
                Dim options As New JsonSerializerOptions With {
                    .PropertyNameCaseInsensitive = True
                }

                If String.IsNullOrEmpty(指定标签) Then
                    ' 获取最新发布
                    Dim releases = JsonSerializer.Deserialize(Of List(Of GitHubRelease))(content, options)
                    If releases Is Nothing OrElse releases.Count = 0 Then
                        ErrorString = "未找到任何发布信息。"
                        Return ErrorString
                    End If
                    Dim release = releases(0)
                    填充发布信息(release)
                Else
                    ' 获取指定标签发布
                    Dim release = JsonSerializer.Deserialize(Of GitHubRelease)(content, options)
                    If release Is Nothing Then
                        ErrorString = "未找到指定标签的发布信息。"
                        Return ErrorString
                    End If
                    填充发布信息(release)
                End If

                Return ""
            Catch ex As Exception
                ErrorString = ex.Message
                Return ErrorString
            End Try
        End Function

        Private Sub 填充发布信息(release As GitHubRelease)
            发布标题 = If(release.Name, "")
            版本标签 = If(release.Tag_name, "")
            预览版 = release.Prerelease
            发布描述 = If(release.Body, "")
            发布时间 = If(release.Created_at, "")
            If 发布时间 <> "" Then
                发布时间 = DateTime.Parse(发布时间).ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss")
            End If
            发布者用户名 = If(release.Author?.Login, "")
            可供下载的文件.Clear()
            If release.Assets IsNot Nothing Then
                For Each asset In release.Assets
                    If asset.Name IsNot Nothing AndAlso asset.Browser_download_url IsNot Nothing Then
                        可供下载的文件.Add(New KeyValuePair(Of String, String)(asset.Name, asset.Browser_download_url))
                    End If
                Next
            End If
        End Sub

        ' 内部类型用于反序列化
        Private Class GitHubRelease
            Public Property Name As String
            Public Property Tag_name As String
            Public Property Prerelease As Boolean
            Public Property Body As String
            Public Property Created_at As String
            Public Property Author As GitHubAuthor
            Public Property Assets As List(Of GitHubAsset)
        End Class

        Private Class GitHubAuthor
            Public Property Login As String
        End Class

        Private Class GitHubAsset
            Public Property Name As String
            Public Property Browser_download_url As String
        End Class

    End Class
End Class