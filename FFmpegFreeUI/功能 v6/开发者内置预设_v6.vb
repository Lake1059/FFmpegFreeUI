Imports System.Text.Json

Public Class 开发者内置预设_v6

    Public Class 预设项
        Public Sub New(名称 As String, 数据 As 预设数据_v6)
            Me.名称 = If(名称, "").Trim()
            Me.数据 = If(数据, New 预设数据_v6)
            预设管理_v6.初始化空集合(Me.数据)
        End Sub

        Public Property 名称 As String
        Public Property 数据 As 预设数据_v6
    End Class

    Public Shared Function 获取全部() As List(Of 预设项)
        Dim result As New List(Of 预设项)


        Return result
    End Function

    Public Shared Function 克隆预设数据(source As 预设数据_v6) As 预设数据_v6
        If source Is Nothing Then Return Nothing
        Dim json = JsonSerializer.Serialize(source, JsonSO)
        Dim result = JsonSerializer.Deserialize(Of 预设数据_v6)(json, JsonSO)
        预设管理_v6.初始化空集合(result)
        Return result
    End Function

End Class
