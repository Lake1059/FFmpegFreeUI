Imports System.IO

Public Class 键值对IO操作
    Public Shared Function 读取键值对文件到字典(ByRef 字典对象 As Dictionary(Of String, String), 文本文档文件路径 As String) As String
        Try
            Using reader As New StringReader(FileIO.FileSystem.ReadAllText(文本文档文件路径))
                Dim line As String = reader.ReadLine()
                While line IsNot Nothing
                    Dim parts As String() = line.Split("="c)
                    Select Case parts.Length
                        Case 2
                            字典对象(parts(0)) = parts(1)
                        Case > 2
                            Dim key As String = parts(0).Trim()
                            Dim value As String = String.Join("=", parts.Skip(1)).Trim()
                            字典对象(key) = value
                    End Select
                    line = reader.ReadLine()
                End While
            End Using
            Return ""
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function

    Public Shared Function 读取键值对文本到字典(ByRef 字典对象 As Dictionary(Of String, String), 文本 As String) As String
        Try
            Using reader As New StringReader(文本)
                Dim line As String = reader.ReadLine()
                While line IsNot Nothing
                    Dim parts As String() = line.Split("="c)
                    Select Case parts.Length
                        Case 2
                            字典对象(parts(0)) = parts(1)
                        Case > 2
                            Dim key As String = parts(0).Trim()
                            Dim value As String = String.Join("=", parts.Skip(1)).Trim()
                            字典对象(key) = value
                    End Select
                    line = reader.ReadLine()
                End While
            End Using
            Return ""
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function

    Public Shared Function 读取键值对文件到列表(ByRef 列表对象 As List(Of KeyValuePair(Of String, String)), 文本文档文件路径 As String) As String
        Try
            Using reader As New StringReader(FileIO.FileSystem.ReadAllText(文本文档文件路径))
                Dim line As String = reader.ReadLine()
                While line IsNot Nothing
                    Dim parts As String() = line.Split("="c)
                    Select Case parts.Length
                        Case 2
                            列表对象.Add(New KeyValuePair(Of String, String)(parts(0), parts(1)))
                        Case > 2
                            Dim key As String = parts(0).Trim()
                            Dim value As String = String.Join("=", parts.Skip(1)).Trim()
                            列表对象.Add(New KeyValuePair(Of String, String)(key, value))
                    End Select
                    line = reader.ReadLine()
                End While
            End Using
            Return ""
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function

    Public Shared Function 读取键值对文本到列表(ByRef 列表对象 As List(Of KeyValuePair(Of String, String)), 文本 As String) As String
        Try
            Using reader As New StringReader(文本)
                Dim line As String = reader.ReadLine()
                While line IsNot Nothing
                    Dim parts As String() = line.Split("="c)
                    Select Case parts.Length
                        Case 2
                            列表对象.Add(New KeyValuePair(Of String, String)(parts(0), parts(1)))
                        Case > 2
                            Dim key As String = parts(0).Trim()
                            Dim value As String = String.Join("=", parts.Skip(1)).Trim()
                            列表对象.Add(New KeyValuePair(Of String, String)(key, value))
                    End Select
                    line = reader.ReadLine()
                End While
            End Using
            Return ""
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function


    Public Shared Function 从字典键值对写入文件(ByRef 字典对象 As Dictionary(Of String, String), 文本文档文件路径 As String) As String
        Try
            Dim str1 As String = ""
            For Each key As String In 字典对象.Keys
                str1 &= key & "=" & 字典对象(key) & vbCrLf
            Next
            FileIO.FileSystem.WriteAllText(文本文档文件路径, str1, False)
            Return ""
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function

    Public Shared Function 从列表键值对写入文件(ByRef 列表对象 As List(Of KeyValuePair(Of String, String)), 文本文档文件路径 As String) As String
        Try
            Dim str1 As String = ""
            For Each kvp As KeyValuePair(Of String, String) In 列表对象
                str1 &= kvp.Key & "=" & kvp.Value & vbCrLf
            Next
            FileIO.FileSystem.WriteAllText(文本文档文件路径, str1, False)
            Return ""
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function

End Class
