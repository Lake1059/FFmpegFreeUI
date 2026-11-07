Imports System.IO
Imports System.Text

Friend NotInheritable Class 原子文件写入_v6
    Private Sub New()
    End Sub

    Public Shared Sub 写入文本(文件路径 As String, 内容 As String)
        Dim target = Path.GetFullPath(文件路径)
        Directory.CreateDirectory(Path.GetDirectoryName(target))
        Dim temporary = target & "." & Guid.NewGuid().ToString("N") & ".tmp"
        Try
            File.WriteAllText(temporary, 内容, New UTF8Encoding(False))
            File.Move(temporary, target, True)
        Finally
            If File.Exists(temporary) Then
                Try
                    File.Delete(temporary)
                Catch
                End Try
            End If
        End Try
    End Sub
End Class
