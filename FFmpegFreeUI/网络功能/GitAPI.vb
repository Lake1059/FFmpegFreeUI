Imports System.Net.Http
Imports System.Text.Json
Imports FFmpegFreeUI.GitAPI.GitApiObject

Public Class GitAPI

    Public Class GitApiObject

        Public Enum 开源代码平台
            Gitee = 1
            GitHub = 2
        End Enum

        Public Shared Property 自定义UserAgent As String = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/138.0.0.0 Safari/537.36 Edg/138.0.0.0"
    End Class

    Public Class Release

        Public Property 发布标题 As String = ""
        Public Property 版本标签 As String = ""
        Public Property 预览版 As Boolean = False
        Public Property 发布描述 As String = ""
        Public Property 发布时间 As String = ""
        Public Property 发布者用户名 As String = ""
        Public Property 发布者昵称 As String = ""
        Public Property 可供下载的文件 As New List(Of KeyValuePair(Of String, String))
        Public Property ErrorString As String = ""

        Private Shared ReadOnly httpClient As New HttpClient() With {
            .Timeout = TimeSpan.FromSeconds(10)
        }

        Shared Sub New()
            httpClient.DefaultRequestHeaders.Add("User-Agent", GitApiObject.自定义UserAgent)
        End Sub

        ''' <summary>
        ''' 访问开源代码平台的网页API，并分析返回的 Json 以获取各项信息
        ''' </summary>
        ''' <returns>如果一切顺利会返回空字符串，否则返回错误描述</returns>
        Public Async Function 获取仓库发布版信息Async(目标平台 As 开源代码平台, 存储库 As String, Optional 指定标签 As String = "") As Task(Of String)
            Try
                ErrorString = ""
                Dim apiUrl As String = GetApiUrl(目标平台, 存储库)

                Using response As HttpResponseMessage = Await httpClient.GetAsync(apiUrl)
                    If Not response.IsSuccessStatusCode Then
                        Dim errorContent = Await response.Content.ReadAsStringAsync()
                        ErrorString = $"请求失败: {errorContent}"
                        Return ErrorString
                    End If

                    Dim jsonContent = Await response.Content.ReadAsStringAsync()
                    Dim options As New JsonSerializerOptions With {
                        .PropertyNameCaseInsensitive = True
                    }

                    Dim releases = JsonSerializer.Deserialize(Of JsonElement())(jsonContent, options)

                    If releases.Length = 0 Then
                        ErrorString = "Server failed to return valid data."
                        Return ErrorString
                    End If

                    Dim latestRelease = releases(0)

                    发布标题 = GetJsonStringValue(latestRelease, "name")
                    版本标签 = GetJsonStringValue(latestRelease, "tag_name")
                    预览版 = GetJsonBooleanValue(latestRelease, "prerelease")
                    发布描述 = GetJsonStringValue(latestRelease, "body")
                    发布时间 = GetJsonStringValue(latestRelease, "created_at")

                    Dim author As JsonElement
                    If latestRelease.TryGetProperty("author", author) Then
                        发布者用户名 = GetJsonStringValue(author, "login")
                        If 目标平台 = 开源代码平台.Gitee Then
                            发布者昵称 = GetJsonStringValue(author, "name")
                        End If
                    End If

                    可供下载的文件.Clear()
                    Dim assets As JsonElement
                    If latestRelease.TryGetProperty("assets", assets) Then
                        For Each asset In assets.EnumerateArray()
                            Dim fileName = GetJsonStringValue(asset, "name")
                            Dim downloadUrl = GetJsonStringValue(asset, "browser_download_url")
                            If Not String.IsNullOrEmpty(fileName) Then
                                可供下载的文件.Add(New KeyValuePair(Of String, String)(fileName, downloadUrl))
                            End If
                        Next
                    End If

                    Return ""
                End Using

            Catch ex As Exception
                ErrorString = ex.Message
                Return ErrorString
            End Try
        End Function

        Public Function 获取仓库发布版信息(目标平台 As 开源代码平台, 存储库 As String, Optional 指定标签 As String = "") As String
            Return 获取仓库发布版信息Async(目标平台, 存储库, 指定标签).GetAwaiter().GetResult()
        End Function

        Private Shared Function GetApiUrl(目标平台 As 开源代码平台, 存储库 As String) As String
            Select Case 目标平台
                Case 开源代码平台.Gitee
                    Return $"https://gitee.com/api/v5/repos/{存储库}/releases/?direction=desc"
                Case 开源代码平台.GitHub
                    Return $"https://api.github.com/repos/{存储库}/releases"
                Case Else
                    Throw New ArgumentException("不支持的平台类型")
            End Select
        End Function

        Private Shared Function GetJsonStringValue(element As JsonElement, propertyName As String) As String
            Dim propertyValue As JsonElement
            If element.TryGetProperty(propertyName, propertyValue) AndAlso propertyValue.ValueKind = JsonValueKind.String Then
                Return propertyValue.GetString()
            End If
            Return ""
        End Function

        Private Shared Function GetJsonBooleanValue(element As JsonElement, propertyName As String) As Boolean
            Dim propertyValue As JsonElement
            If element.TryGetProperty(propertyName, propertyValue) AndAlso propertyValue.ValueKind <> JsonValueKind.Null Then
                Return propertyValue.GetBoolean()
            End If
            Return False
        End Function

    End Class

    Public Class TextFileString

        Public Property 网页返回字符串 As String = ""
        Public Property ErrorString As String = ""

        Private Shared ReadOnly httpClient As New HttpClient() With {
            .Timeout = TimeSpan.FromSeconds(10)
        }

        Shared Sub New()
            httpClient.DefaultRequestHeaders.Add("User-Agent", GitApiObject.自定义UserAgent)
        End Sub

        ''' <summary>
        ''' 获取纯文本文件内容
        ''' </summary>
        Public Async Function 获取文本文件数据Async(目标平台 As 开源代码平台, 用户名和仓库名 As String, 分支 As String, 路径 As String, Optional 令牌 As String = "", Optional 是否需要进行Json错误消息识别 As Boolean = False) As Task(Of String)
            Try
                ErrorString = ""
                Dim fileUrl = GetFileUrl(目标平台, 用户名和仓库名, 分支, 路径, 令牌)

                Using response As HttpResponseMessage = Await httpClient.GetAsync(fileUrl)
                    If Not response.IsSuccessStatusCode Then
                        Dim errorContent = Await response.Content.ReadAsStringAsync()
                        ErrorString = $"请求失败: {errorContent}"
                        Return ErrorString
                    End If

                    Dim content = Await response.Content.ReadAsStringAsync()

                    If 是否需要进行Json错误消息识别 Then
                        Try
                            Dim options As New JsonSerializerOptions With {
                                .PropertyNameCaseInsensitive = True
                            }
                            Dim jsonElement = JsonSerializer.Deserialize(Of JsonElement)(content, options)
                            Dim messageProperty As JsonElement

                            If jsonElement.TryGetProperty("message", messageProperty) Then
                                ErrorString = messageProperty.GetString()
                                Return ErrorString
                            End If
                        Catch
                            ' 如果不是有效的JSON，则视为普通文本
                        End Try
                    End If

                    网页返回字符串 = content
                    Return ""
                End Using

            Catch ex As Exception
                ErrorString = ex.Message
                Return ErrorString
            End Try
        End Function

        Public Function 获取文本文件数据(目标平台 As 开源代码平台, 用户名和仓库名 As String, 分支 As String, 路径 As String, Optional 令牌 As String = "", Optional 是否需要进行Json错误消息识别 As Boolean = False) As String
            Return 获取文本文件数据Async(目标平台, 用户名和仓库名, 分支, 路径, 令牌, 是否需要进行Json错误消息识别).GetAwaiter().GetResult()
        End Function

        Private Shared Function GetFileUrl(目标平台 As 开源代码平台, 用户名和仓库名 As String, 分支 As String, 路径 As String, 令牌 As String) As String
            Dim baseUrl As String
            Select Case 目标平台
                Case 开源代码平台.Gitee
                    baseUrl = $"https://gitee.com/{用户名和仓库名}/raw/{分支}/{路径}"
                    If Not String.IsNullOrEmpty(令牌) Then
                        baseUrl &= $"?access_token={令牌}"
                    End If
                Case 开源代码平台.GitHub
                    baseUrl = $"https://raw.githubusercontent.com/{用户名和仓库名}/{分支}/{路径}"
                    If Not String.IsNullOrEmpty(令牌) Then
                        baseUrl &= $"?token={令牌}"
                    End If
                Case Else
                    Throw New ArgumentException("不支持的平台类型")
            End Select
            Return baseUrl
        End Function

    End Class

    Public Class Tag

        Public Property Tag数据 As New List(Of Tag单片数据)
        Public Property ErrorString As String = ""

        Public Structure Tag单片数据
            Public name As String
        End Structure

        Private Shared ReadOnly httpClient As New HttpClient() With {
            .Timeout = TimeSpan.FromSeconds(10)
        }

        Shared Sub New()
            httpClient.DefaultRequestHeaders.Add("User-Agent", GitApiObject.自定义UserAgent)
        End Sub

        Public Async Function 获取仓库Tag信息Async(目标平台 As 开源代码平台, 存储库 As String) As Task(Of String)
            Try
                Tag数据.Clear()
                ErrorString = ""
                Dim apiUrl = GetTagApiUrl(目标平台, 存储库)

                Using response As HttpResponseMessage = Await httpClient.GetAsync(apiUrl)
                    If Not response.IsSuccessStatusCode Then
                        ErrorString = Await response.Content.ReadAsStringAsync()
                        Return ErrorString
                    End If

                    Dim jsonContent = Await response.Content.ReadAsStringAsync()
                    Dim options As New JsonSerializerOptions With {
                        .PropertyNameCaseInsensitive = True
                    }

                    Dim tags = JsonSerializer.Deserialize(Of JsonElement())(jsonContent, options)

                    For Each tag In tags
                        Dim tagData As New Tag单片数据
                        With tagData
                            .name = GetJsonStringValue(tag, "name")
                        End With
                        Tag数据.Add(tagData)
                    Next

                    Return ""
                End Using

            Catch ex As Exception
                ErrorString = ex.Message
                Return ex.Message
            End Try
        End Function

        Public Function 获取仓库Tag信息(目标平台 As 开源代码平台, 存储库 As String) As String
            Return 获取仓库Tag信息Async(目标平台, 存储库).GetAwaiter().GetResult()
        End Function

        Private Shared Function GetTagApiUrl(目标平台 As 开源代码平台, 存储库 As String) As String
            Select Case 目标平台
                Case 开源代码平台.Gitee
                    Return $"https://gitee.com/api/v5/repos/{存储库}/tags"
                Case 开源代码平台.GitHub
                    Return $"https://api.github.com/repos/{存储库}/tags"
                Case Else
                    Throw New ArgumentException("不支持的平台类型")
            End Select
        End Function

        Private Shared Function GetJsonStringValue(element As JsonElement, propertyName As String) As String
            Dim propertyValue As JsonElement
            If element.TryGetProperty(propertyName, propertyValue) AndAlso propertyValue.ValueKind = JsonValueKind.String Then
                Return propertyValue.GetString()
            End If
            Return ""
        End Function

    End Class

    Public Class GitHubAllReleaseFile

        Public Property 发行版数据集合 As New List(Of 发行版数据单片)

        Public Structure 发行版数据单片
            Public 可供下载的文件 As List(Of KeyValuePair(Of String, String))
            Public 标题 As String
            Public 描述 As String
            Public 标签 As String
            Public 是否是草稿 As Boolean
            Public 是否预览版 As Boolean
        End Structure

        Private Shared ReadOnly httpClient As New HttpClient() With {
            .Timeout = TimeSpan.FromSeconds(10)
        }

        Shared Sub New()
            httpClient.DefaultRequestHeaders.Add("User-Agent", GitApiObject.自定义UserAgent)
        End Sub

        Public Async Function 获取Async(存储库 As String) As Task(Of String)
            Try
                Dim apiUrl = $"https://api.github.com/repos/{存储库}/releases"

                Using response As HttpResponseMessage = Await httpClient.GetAsync(apiUrl)
                    If Not response.IsSuccessStatusCode Then
                        Return Await response.Content.ReadAsStringAsync()
                    End If

                    Dim jsonContent = Await response.Content.ReadAsStringAsync()
                    Dim options As New JsonSerializerOptions With {
                        .PropertyNameCaseInsensitive = True
                    }

                    Dim releases = JsonSerializer.Deserialize(Of JsonElement())(jsonContent, options)

                    发行版数据集合.Clear()
                    For Each release In releases
                        Dim releaseData As New 发行版数据单片 With {
                            .标题 = GetJsonStringValue(release, "name"),
                            .描述 = GetJsonStringValue(release, "body"),
                            .标签 = GetJsonStringValue(release, "tag_name"),
                            .是否是草稿 = GetJsonBooleanValue(release, "draft"),
                            .是否预览版 = GetJsonBooleanValue(release, "prerelease"),
                            .可供下载的文件 = New List(Of KeyValuePair(Of String, String))
                        }

                        Dim assets As JsonElement
                        If release.TryGetProperty("assets", assets) Then
                            For Each asset In assets.EnumerateArray()
                                Dim fileName = GetJsonStringValue(asset, "name")
                                Dim downloadUrl = GetJsonStringValue(asset, "browser_download_url")
                                If Not String.IsNullOrEmpty(fileName) Then
                                    releaseData.可供下载的文件.Add(New KeyValuePair(Of String, String)(fileName, downloadUrl))
                                End If
                            Next
                        End If

                        发行版数据集合.Add(releaseData)
                    Next

                    Return ""
                End Using

            Catch ex As Exception
                Return ex.Message
            End Try
        End Function

        Public Function 获取(存储库 As String) As String
            Return 获取Async(存储库).GetAwaiter().GetResult()
        End Function

        Private Shared Function GetJsonStringValue(element As JsonElement, propertyName As String) As String
            Dim propertyValue As JsonElement
            If element.TryGetProperty(propertyName, propertyValue) AndAlso propertyValue.ValueKind = JsonValueKind.String Then
                Return propertyValue.GetString()
            End If
            Return ""
        End Function

        Private Shared Function GetJsonBooleanValue(element As JsonElement, propertyName As String) As Boolean
            Dim propertyValue As JsonElement
            If element.TryGetProperty(propertyName, propertyValue) AndAlso propertyValue.ValueKind <> JsonValueKind.Null Then
                Return propertyValue.GetBoolean()
            End If
            Return False
        End Function

    End Class

End Class