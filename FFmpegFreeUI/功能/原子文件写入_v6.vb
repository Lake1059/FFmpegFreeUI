Imports System.IO
Imports System.Text

Friend NotInheritable Class 原子文件写入_v6
    Private Sub New()
    End Sub

    Public Shared Sub 写入文本(文件路径 As String, 内容 As String, Optional 刷新到磁盘 As Boolean = False)
        Dim target = Path.GetFullPath(文件路径)
        Directory.CreateDirectory(Path.GetDirectoryName(target))
        Dim temporary = target & "." & Guid.NewGuid().ToString("N") & ".tmp"
        Try
            Using stream As New FileStream(temporary, FileMode.CreateNew, FileAccess.Write, FileShare.None)
                Using writer As New StreamWriter(stream, New UTF8Encoding(False), leaveOpen:=True)
                    writer.Write(内容)
                End Using
                If 刷新到磁盘 Then stream.Flush(True)
            End Using
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
