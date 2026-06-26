Imports System.IO
Imports System.Runtime.InteropServices

Public Class 程序架构

    Public Shared Function 获取自身程序架构() As String
        Return 转换架构名称(RuntimeInformation.ProcessArchitecture)
    End Function

    Public Shared Function 获取指定程序文件架构(文件路径 As String) As String
        If String.IsNullOrWhiteSpace(文件路径) OrElse Not File.Exists(文件路径) Then Return ""

        Try
            Using 文件流 As New FileStream(文件路径, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
                Using 读取器 As New BinaryReader(文件流)
                    If 文件流.Length < &H40 Then Return ""
                    If 读取器.ReadUInt16() <> &H5A4DUS Then Return ""

                    文件流.Position = &H3C
                    Dim PeHeader位置 = 读取器.ReadInt32()
                    If PeHeader位置 < 0 OrElse PeHeader位置 + 6 > 文件流.Length Then Return ""

                    文件流.Position = PeHeader位置
                    If 读取器.ReadUInt32() <> &H4550UI Then Return ""

                    Dim Machine = 读取器.ReadUInt16()
                    Return 转换Machine架构名称(Machine)
                End Using
            End Using
        Catch ex As IOException
            Return ""
        Catch ex As UnauthorizedAccessException
            Return ""
        Catch ex As BadImageFormatException
            Return ""
        End Try
    End Function

    Private Shared Function 转换架构名称(ProcessArchitecture As Architecture) As String
        Select Case ProcessArchitecture
            Case Architecture.X64
                Return "x64"
            Case Architecture.X86
                Return "x86"
            Case Architecture.Arm64
                Return "arm64"
            Case Architecture.Arm
                Return "arm"
            Case Else
                Return ""
        End Select
    End Function

    Private Shared Function 转换Machine架构名称(Machine As UShort) As String
        Select Case CUInt(Machine)
            Case &H8664UI
                Return "x64"
            Case &H14CUI
                Return "x86"
            Case &HAA64UI
                Return "arm64"
            Case &H1C0UI, &H1C4UI
                Return "arm"
            Case Else
                Return ""
        End Select
    End Function

End Class
