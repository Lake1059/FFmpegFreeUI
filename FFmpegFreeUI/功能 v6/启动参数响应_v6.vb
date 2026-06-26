Imports System.IO
Imports System.Text

Public Class 启动参数响应_v6

    Private Class 接收参数状态
        Public Property 输入文件 As String = ""
        Public Property 预设文件 As String = ""
        Public Property 入点 As String = ""
        Public Property 出点 As String = ""
        Public Property 独立命令行 As String = ""
        Public Property 独立命令行任务名 As String = ""
    End Class

    Public Shared Sub 处理接收的参数(原始文本 As String)
        处理接收的参数(拆分命令行(原始文本))
    End Sub

    Public Shared Sub 处理接收的参数(CL As IEnumerable(Of String))
        Dim args = If(CL, Array.Empty(Of String)()).Where(Function(x) x IsNot Nothing).ToList()
        If args.Count = 0 Then Exit Sub

        Dim state As New 接收参数状态
        Dim i = 0
        While i < args.Count
            Dim arg = args(i)
            Select Case arg
                Case "-ffmpeg"
                    处理FFmpeg参数(args, i, state)
                    Exit While
                Case "-test"
                    MsgBox("哔哔", MsgBoxStyle.Information)
                Case "-i"
                    If i < args.Count - 1 Then
                        state.输入文件 = args(i + 1)
                        i += 1
                    End If
                Case "-3fui_file"
                    If i < args.Count - 1 Then
                        state.预设文件 = args(i + 1)
                        i += 1
                    End If
                Case "-3fuiVideoHelperInPointTime"
                    If i < args.Count - 1 Then
                        state.入点 = args(i + 1)
                        i += 1
                    End If
                Case "-3fuiVideoHelperOutPointTime"
                    If i < args.Count - 1 Then
                        state.出点 = args(i + 1)
                        i += 1
                    End If
            End Select
            i += 1
        End While

        If state.独立命令行 <> "" Then
            插件管理.使用命令行添加任务到编码队列(
                state.独立命令行,
                If(state.独立命令行任务名 <> "", state.独立命令行任务名, $"外部命令行任务 {Now:HHmmss}"),
                "",
                "")
            Exit Sub
        End If

        If state.输入文件 <> "" AndAlso state.预设文件 <> "" Then
            添加预设任务(state)
        ElseIf state.入点 <> "" OrElse state.出点 <> "" Then
            应用剪辑区间到当前参数面板(state.入点, state.出点)
        End If
    End Sub

    Private Shared Sub 添加预设任务(state As 接收参数状态)
        If Not File.Exists(state.输入文件) Then Exit Sub

        Dim preset = 读取预设数据(state.预设文件)
        If preset Is Nothing Then Exit Sub

        If state.入点 <> "" Then preset.剪辑区间_入点 = state.入点
        If state.出点 <> "" Then preset.剪辑区间_出点 = state.出点
        If state.入点 <> "" OrElse state.出点 <> "" Then
            preset.剪辑区间_方法 = 预设数据_v6.剪辑方法.粗剪
        End If

        编码队列_v6.添加预设任务(state.输入文件, preset, Path.GetFileName(state.输入文件))
    End Sub

    Public Shared Function 读取预设数据(预设路径或名称 As String) As 预设数据_v6
        Dim presetPath = 解析预设文件路径(预设路径或名称)
        If presetPath <> "" Then Return 预设管理_v6.读取预设文件(presetPath)

        Dim name = Path.GetFileNameWithoutExtension(If(预设路径或名称, "").Trim())
        If name <> "" Then
            For Each builtIn In 开发者内置预设_v6.获取全部()
                If String.Equals(builtIn.名称, name, StringComparison.CurrentCultureIgnoreCase) Then
                    Return 开发者内置预设_v6.克隆预设数据(builtIn.数据)
                End If
            Next
        End If

        Return Nothing
    End Function

    Public Shared Function 解析预设文件路径(预设路径或名称 As String) As String
        Dim raw = If(预设路径或名称, "").Trim()
        If raw = "" Then Return ""
        If File.Exists(raw) Then Return raw

        Dim fileName = Path.GetFileName(raw)
        If fileName = "" Then Return ""
        If Not fileName.EndsWith(".json", StringComparison.OrdinalIgnoreCase) Then fileName &= ".json"

        For Each source In {"用户自定义", "从社区下载"}
            Dim candidate = Path.Combine(预设管理_v6.获取预设目录(source), fileName)
            If File.Exists(candidate) Then Return candidate
        Next

        Dim oldCandidate = Path.Combine(Application.StartupPath, "Preset", fileName)
        If File.Exists(oldCandidate) Then Return oldCandidate
        Return ""
    End Function

    Private Shared Sub 应用剪辑区间到当前参数面板(入点 As String, 出点 As String)
        If 入点 = "" AndAlso 出点 = "" Then Exit Sub
        Try
            If 入点 <> "" Then Form_v6_参数面板.私有界面_剪辑区间.MTB_入点.Text = 入点
            If 出点 <> "" Then Form_v6_参数面板.私有界面_剪辑区间.MTB_出点.Text = 出点
            Form_v6_参数面板.私有界面_剪辑区间.MCB_剪辑模式.SelectedIndex = CInt(预设数据_v6.剪辑方法.粗剪)
        Catch
        End Try
    End Sub

    Private Shared Sub 处理FFmpeg参数(args As List(Of String), startIndex As Integer, state As 接收参数状态)
        Dim parts As New List(Of String)
        For i = startIndex + 1 To args.Count - 1
            Dim arg = args(i)
            parts.Add(转命令行参数文本(arg))
            If state.独立命令行任务名 = "" AndAlso arg = "-i" AndAlso i < args.Count - 1 Then
                state.独立命令行任务名 = Path.GetFileName(args(i + 1))
            End If
        Next
        state.独立命令行 = String.Join(" ", parts)
    End Sub

    Private Shared Function 转命令行参数文本(arg As String) As String
        If arg Is Nothing Then Return """"""
        If arg = "" Then Return """"""
        If arg.Any(Function(c) Char.IsWhiteSpace(c) OrElse c = """"c) Then
            Return """" & arg.Replace("""", """""") & """"
        End If
        Return arg
    End Function

    Public Shared Function 拆分命令行(commandLine As String) As List(Of String)
        Dim result As New List(Of String)
        If String.IsNullOrWhiteSpace(commandLine) Then Return result

        Dim current As New StringBuilder
        Dim inQuote = False
        Dim escapeNext = False

        For Each ch In commandLine
            If escapeNext Then
                current.Append(ch)
                escapeNext = False
                Continue For
            End If

            If ch = "\"c AndAlso inQuote Then
                escapeNext = True
                Continue For
            End If

            If ch = """"c Then
                inQuote = Not inQuote
                Continue For
            End If

            If Char.IsWhiteSpace(ch) AndAlso Not inQuote Then
                If current.Length > 0 Then
                    result.Add(current.ToString())
                    current.Clear()
                End If
                Continue For
            End If

            current.Append(ch)
        Next

        If escapeNext Then current.Append("\"c)
        If current.Length > 0 Then result.Add(current.ToString())
        Return result
    End Function

End Class
