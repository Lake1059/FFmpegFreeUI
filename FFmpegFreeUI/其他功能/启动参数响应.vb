Imports System.IO

Public Class 启动参数响应

    Public Shared Property 要准备编码的文件 As String = ""
    Public Shared Property 要使用的预设文件 As String = ""
    Public Shared Property 独立命令行 As String = ""
    Public Shared Property 独立命令行_用于展示的文件名 As String = ""

    ' 参数处理字典：键为参数名，值为处理函数（接收命令行列表和当前索引，返回新索引）
    Private Shared ReadOnly 参数处理器 As New Dictionary(Of String, Func(Of List(Of String), Integer, Integer)) From {
        {"-3fuiVideoHelperInPointTime", Function(args, i)
                                            Form1.常规流程参数页面.UiTextBox快速剪辑入点.Text = args(i + 1)
                                            Return i + 1
                                        End Function},
        {"-3fuiVideoHelperOutPointTime", Function(args, i)
                                             Form1.常规流程参数页面.UiTextBox快速剪辑出点.Text = args(i + 1)
                                             Return i + 1
                                         End Function},
        {"-i", Function(args, i)
                   要准备编码的文件 = args(i + 1)
                   Return i + 1
               End Function},
        {"-3fui_file", Function(args, i)
                           要使用的预设文件 = args(i + 1)
                           Return i + 1
                       End Function}
    }

    ' 无参数的处理器（不需要后续参数）
    Private Shared ReadOnly 无参数处理器 As New Dictionary(Of String, Action) From {
        {"-test", Sub()
                      Debug.Print("哔哔")
                      MsgBox("哔哔", MsgBoxStyle.Information)
                  End Sub}
    }

    Public Shared Sub 处理接收的参数(CL As List(Of String))
        要准备编码的文件 = ""
        要使用的预设文件 = ""
        独立命令行 = ""
        独立命令行_用于展示的文件名 = ""
        Dim i = 0
        While i < CL.Count
            Dim arg = CL(i)
            ' 特殊处理 -ffmpeg 参数（收集后续所有参数）
            If arg = "-ffmpeg" Then
                处理FFmpeg参数(CL, i)
                Exit While
            End If
            ' 先检查无参数处理器
            Dim value As Action = Nothing
            If 无参数处理器.TryGetValue(arg, value) Then
                value()
                ' 再检查需要后续参数的处理器
            ElseIf 参数处理器.ContainsKey(arg) Then
                If i < CL.Count - 1 Then
                    i = 参数处理器(arg)(CL, i)
                End If
            End If
            i += 1
        End While
        If 独立命令行 <> "" Then
            插件管理.使用命令行添加任务到编码队列(独立命令行, If(独立命令行_用于展示的文件名 <> "", 独立命令行_用于展示的文件名, $"外部命令行任务 {Now:HHmmss}"), "")
        End If
        If 要准备编码的文件 <> "" AndAlso 要使用的预设文件 <> "" Then
            If Not FileIO.FileSystem.FileExists(要准备编码的文件) Then Exit Sub
            If Not FileIO.FileSystem.FileExists(要使用的预设文件) Then
                Dim abc = Path.Combine(My.Application.Info.DirectoryPath, "Preset", Path.GetFileNameWithoutExtension(要使用的预设文件) & ".json")
                If Not FileIO.FileSystem.FileExists(abc) Then
                    Exit Sub
                Else
                    要使用的预设文件 = abc
                End If
            End If
            插件管理.使用预设文件添加任务到编码队列(要使用的预设文件, Path.GetFileName(要准备编码的文件), "", 要准备编码的文件)
        End If
    End Sub

    Public Shared Sub 处理FFmpeg参数(commandLine As List(Of String), startIndex As Integer)
        Dim abc As New List(Of String)
        For i3 = startIndex + 1 To commandLine.Count - 1
            Dim arg = commandLine(i3)
            ' 包含空格的参数需要重新加引号还原
            If arg.Contains(" "c) Then
                abc.Add($"""{arg}""")
            Else
                abc.Add(arg)
            End If
            If 独立命令行_用于展示的文件名 = "" AndAlso arg = "-i" AndAlso i3 < commandLine.Count - 1 Then
                独立命令行_用于展示的文件名 = Path.GetFileName(commandLine(i3 + 1))
            End If
        Next
        独立命令行 = String.Join(" ", abc)
    End Sub
End Class
