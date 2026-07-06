Imports System.Text.Json.Serialization

Public Partial Class 网络功能

    Public Class AgentSpEndpointInfo
        <JsonPropertyName("display_name")>
        Public Property DisplayName As String = ""

        <JsonPropertyName("address")>
        Public Property Address As String = ""

        <JsonPropertyName("api_key")>
        Public Property ApiKey As String = ""

        <JsonPropertyName("extra_headers")>
        Public Property ExtraHeaders As String = ""

        <JsonPropertyName("extra_body")>
        Public Property ExtraBody As String = ""

        Public Function Clone() As AgentSpEndpointInfo
            Return New AgentSpEndpointInfo With {
                .DisplayName = If(DisplayName, ""),
                .Address = If(Address, ""),
                .ApiKey = If(ApiKey, ""),
                .ExtraHeaders = If(ExtraHeaders, ""),
                .ExtraBody = If(ExtraBody, "")
            }
        End Function
    End Class

    Public Class 新闻单片数据类
        Public Property Title As String
        Public Property TitleColor As String
        Public Property SubTitle As String
        Public Property Type As String
        Public Property Body As String
    End Class

    Public Class 国内镜像源数据结构
        <JsonPropertyName("status")>
        Public Property Status As String

        <JsonPropertyName("message")>
        Public Property Message As String

        <JsonPropertyName("request_id")>
        Public Property RequestId As String

        <JsonPropertyName("data")>
        Public Property Data As 国内镜像源DataInfo
    End Class

    Public Class 国内镜像源DataInfo
        <JsonPropertyName("assets")>
        Public Property Assets As 国内镜像源AssetInfo()
    End Class

    Public Class 国内镜像源AssetInfo
        <JsonPropertyName("asset_id")>
        Public Property AssetId As String

        <JsonPropertyName("version")>
        Public Property Version As String

        <JsonPropertyName("download_path")>
        Public Property DownloadPath As String

        <JsonPropertyName("prerelease")>
        Public Property Prerelease As Boolean

        <JsonPropertyName("file_name")>
        Public Property FileName As String

        <JsonPropertyName("architecture")>
        Public Property Architecture As String

        <JsonPropertyName("system")>
        Public Property TargetSystem As String

        <JsonPropertyName("size_bytes")>
        <JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)>
        Public Property SizeBytes As Long

        <JsonPropertyName("digest_sha256")>
        Public Property DigestSha256 As String

        <JsonPropertyName("available")>
        Public Property Available As Boolean

        <JsonPropertyName("unavailable_reason")>
        Public Property UnavailableReason As String
    End Class

    Public Class 国内镜像源创建挑战请求
        <JsonPropertyName("asset_id")>
        Public Property AssetId As String
    End Class

    Public Class 国内镜像源挑战数据结构
        <JsonPropertyName("status")>
        Public Property Status As String

        <JsonPropertyName("message")>
        Public Property Message As String

        <JsonPropertyName("request_id")>
        Public Property RequestId As String

        <JsonPropertyName("data")>
        Public Property Data As 国内镜像源挑战DataInfo
    End Class

    Public Class 国内镜像源挑战DataInfo
        <JsonPropertyName("algorithm")>
        Public Property Algorithm As String

        <JsonPropertyName("asset_id")>
        Public Property AssetId As String

        <JsonPropertyName("canonical_format")>
        Public Property CanonicalFormat As String

        <JsonPropertyName("challenge_id")>
        Public Property ChallengeId As String

        <JsonPropertyName("expires_at")>
        Public Property ExpiresAt As String

        <JsonPropertyName("leading_zero_bits")>
        <JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)>
        Public Property LeadingZeroBits As Integer

        <JsonPropertyName("nonce_seed")>
        Public Property NonceSeed As String
    End Class

    Public Class 国内镜像源创建授权请求
        <JsonPropertyName("challenge_id")>
        Public Property ChallengeId As String

        <JsonPropertyName("asset_id")>
        Public Property AssetId As String

        <JsonPropertyName("nonce")>
        Public Property Nonce As String
    End Class

    Public Class 国内镜像源授权数据结构
        <JsonPropertyName("status")>
        Public Property Status As String

        <JsonPropertyName("message")>
        Public Property Message As String

        <JsonPropertyName("request_id")>
        Public Property RequestId As String

        <JsonPropertyName("data")>
        Public Property Data As 国内镜像源授权DataInfo
    End Class

    Public Class 国内镜像源授权DataInfo
        <JsonPropertyName("authorization_id")>
        Public Property AuthorizationId As String

        <JsonPropertyName("download_token")>
        Public Property DownloadToken As String

        <JsonPropertyName("download_url")>
        Public Property DownloadUrl As String

        <JsonPropertyName("expires_at")>
        Public Property ExpiresAt As String

        <JsonPropertyName("max_bytes")>
        <JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)>
        Public Property MaxBytes As Long

        <JsonPropertyName("range_concurrency_limit")>
        <JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)>
        Public Property RangeConcurrencyLimit As Integer
    End Class

    Public Class MirrorChyan数据结构
        <JsonPropertyName("code")>
        <JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)>
        Public Property Code As Integer

        <JsonPropertyName("msg")>
        Public Property Msg As String

        <JsonPropertyName("data")>
        Public Property Data As MirrorChyanData
    End Class

    Public Class MirrorChyanData
        <JsonPropertyName("version_name")>
        Public Property VersionName As String

        <JsonPropertyName("url")>
        Public Property Url As String
    End Class

End Class
