Imports System.Text
Imports System.Text.Json

Public NotInheritable Class Agent通用工具_v6
    Private Sub New()
    End Sub

    Public Shared Function ParseJsonArguments(arguments As String) As JsonElement
        If String.IsNullOrWhiteSpace(arguments) Then arguments = "{}"
        Return JsonDocument.Parse(arguments).RootElement.Clone()
    End Function

    Public Shared Function GetJsonString(root As JsonElement, name As String, Optional defaultValue As String = "") As String
        Dim value As JsonElement
        If root.ValueKind = JsonValueKind.Object AndAlso root.TryGetProperty(name, value) AndAlso value.ValueKind = JsonValueKind.String Then
            Return If(value.GetString(), "")
        End If
        Return defaultValue
    End Function

    Public Shared Function GetJsonInteger(root As JsonElement, name As String, defaultValue As Integer) As Integer
        Dim value As JsonElement
        If root.ValueKind = JsonValueKind.Object AndAlso root.TryGetProperty(name, value) AndAlso value.ValueKind = JsonValueKind.Number Then
            Dim result As Integer
            If value.TryGetInt32(result) Then Return result
        End If
        Return defaultValue
    End Function

    Public Shared Function GetJsonBoolean(root As JsonElement, name As String, defaultValue As Boolean) As Boolean
        Dim value As JsonElement
        If root.ValueKind = JsonValueKind.Object AndAlso root.TryGetProperty(name, value) Then
            If value.ValueKind = JsonValueKind.True Then Return True
            If value.ValueKind = JsonValueKind.False Then Return False
        End If
        Return defaultValue
    End Function

    Public Shared Function GetJsonObject(root As JsonElement, name As String) As JsonElement
        Dim value As JsonElement
        If root.ValueKind = JsonValueKind.Object AndAlso root.TryGetProperty(name, value) AndAlso value.ValueKind = JsonValueKind.Object Then Return value
        Return JsonDocument.Parse("{}").RootElement.Clone()
    End Function

    Public Shared Function GetJsonStringArray(root As JsonElement, name As String, Optional distinct As Boolean = True) As List(Of String)
        Dim result As New List(Of String)
        Dim value As JsonElement
        If root.ValueKind <> JsonValueKind.Object OrElse Not root.TryGetProperty(name, value) OrElse value.ValueKind <> JsonValueKind.Array Then Return result

        For Each item In value.EnumerateArray()
            If item.ValueKind <> JsonValueKind.String Then Continue For
            Dim text = If(item.GetString(), "").Trim()
            If text = "" Then Continue For
            If distinct AndAlso result.Contains(text, StringComparer.OrdinalIgnoreCase) Then Continue For
            result.Add(text)
        Next
        Return result
    End Function

    Public Shared Function LimitText(text As String, maxLength As Integer, Optional suffix As String = "...[已截断]") As String
        text = If(text, "")
        If maxLength < 0 OrElse text.Length <= maxLength Then Return text
        Return String.Concat(text.AsSpan(0, maxLength), If(suffix, ""))
    End Function

    Public Shared Function DecodeTextBytes(bytes As Byte()) As String
        If bytes Is Nothing OrElse bytes.Length = 0 Then Return ""
        If bytes.Length >= 3 AndAlso bytes(0) = &HEF AndAlso bytes(1) = &HBB AndAlso bytes(2) = &HBF Then
            Return Encoding.UTF8.GetString(bytes, 3, bytes.Length - 3)
        End If
        If bytes.Length >= 2 AndAlso bytes(0) = &HFF AndAlso bytes(1) = &HFE Then
            Return Encoding.Unicode.GetString(bytes)
        End If
        If bytes.Length >= 2 AndAlso bytes(0) = &HFE AndAlso bytes(1) = &HFF Then
            Return Encoding.BigEndianUnicode.GetString(bytes)
        End If
        Return Encoding.UTF8.GetString(bytes)
    End Function

    Public Shared Function IsImageExtension(extension As String) As Boolean
        Select Case If(extension, "").ToLowerInvariant()
            Case ".png", ".jpg", ".jpeg", ".webp", ".gif", ".bmp"
                Return True
            Case Else
                Return False
        End Select
    End Function

    Public Shared Function FormatFileSize(bytes As Long) As String
        If bytes < 1024 Then Return bytes & " B"
        If bytes < 1024L * 1024L Then Return $"{bytes / 1024.0:F1} KiB"
        If bytes < 1024L * 1024L * 1024L Then Return $"{bytes / 1024.0 / 1024.0:F1} MiB"
        Return $"{bytes / 1024.0 / 1024.0 / 1024.0:F1} GiB"
    End Function
End Class
