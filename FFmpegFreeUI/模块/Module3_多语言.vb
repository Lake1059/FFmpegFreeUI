Imports System.Text.Json

Module Module3_多语言
    Public Property 多语言字典 As New Dictionary(Of String, String)

    Public Sub 加载多语言()
        多语言字典 = JsonSerializer.Deserialize(Of Dictionary(Of String, String))(My.Resources.Resource1.语言_zh, JsonSO)
        Select Case 用户设置.实例对象.语言
            Case "en"
                Dim 英文字典 As Dictionary(Of String, String) = JsonSerializer.Deserialize(Of Dictionary(Of String, String))(My.Resources.Resource1.语言_en, JsonSO)
                For Each 键值对 In 英文字典
                    多语言字典(键值对.Key) = 键值对.Value
                Next
        End Select
    End Sub

    Public Function 翻译(key As String) As String
        If key.Trim = "" Then
            Return ""
            Exit Function
        End If
        Dim value As String = Nothing
        If 多语言字典.TryGetValue(key, value) Then
            Return value.Replace("<br>", vbCrLf)
        Else
            Return key
        End If
    End Function

End Module
