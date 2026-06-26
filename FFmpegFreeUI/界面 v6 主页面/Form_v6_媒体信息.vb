Public Class Form_v6_媒体信息
    Private Sub Form_v6_媒体信息_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ModernTextBox1.SyntaxHighlighter = FFmpeg输出语法高亮器_v6.默认实例
        ModernTextBox1.EnableSyntaxHighlight = True
        AddHandler ModernTextBox1.DragEnter, AddressOf 文件拖入事件
        AddHandler ModernTextBox1.DragDrop, AddressOf 文件放下事件
    End Sub

    Private Sub MB_打开文件_Click(sender As Object, e As EventArgs) Handles MB_打开文件.Click
        Dim openFileDialog As New OpenFileDialog With {.Multiselect = False}
        If openFileDialog.ShowDialog = DialogResult.OK Then
            显示媒体信息流程(openFileDialog.FileName)
        End If
    End Sub

    Sub 文件拖入事件(sender As Object, e As DragEventArgs)
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub
    Sub 文件放下事件(sender As Object, e As DragEventArgs)
        Dim files = e.Data.GetData(DataFormats.FileDrop)
        If files.Length > 0 Then 显示媒体信息流程(files(0))
    End Sub
    Sub 显示媒体信息流程(文件路径 As String)
        Me.ModernTextBox1.Text = ""
        Dim FFprobeProcess As New Process
        FFprobeProcess = New Process()
        FFprobeProcess.StartInfo.FileName = "ffprobe"
        FFprobeProcess.StartInfo.WorkingDirectory = If(设置_v6.实例对象.工作目录 <> "", 设置_v6.实例对象.工作目录, "")
        FFprobeProcess.StartInfo.Arguments = $"-hide_banner ""{文件路径}"""
        FFprobeProcess.StartInfo.RedirectStandardOutput = True
        FFprobeProcess.StartInfo.RedirectStandardError = True
        FFprobeProcess.StartInfo.StandardOutputEncoding = System.Text.Encoding.UTF8
        FFprobeProcess.StartInfo.StandardErrorEncoding = System.Text.Encoding.UTF8
        FFprobeProcess.StartInfo.CreateNoWindow = True
        FFprobeProcess.EnableRaisingEvents = True
        AddHandler FFprobeProcess.OutputDataReceived, AddressOf 显示媒体信息输出事件
        AddHandler FFprobeProcess.ErrorDataReceived, AddressOf 显示媒体信息输出事件
        FFprobeProcess.Start()
        FFprobeProcess.BeginOutputReadLine()
        FFprobeProcess.BeginErrorReadLine()
    End Sub
    Sub 显示媒体信息输出事件(sender As Object, e As DataReceivedEventArgs)
        If e.Data Is Nothing Then Exit Sub
        Try
            界面线程执行(Sub() Me.ModernTextBox1.AppendLine(e.Data))
        Catch ex As Exception
        End Try
    End Sub
    Public Shared Function 识别FF单行输出并调整文字颜色(单行输出 As String, 默认颜色 As Color) As Color
        If String.IsNullOrEmpty(单行输出) Then Return 默认颜色
        If 编码队列_v6.错误输出匹配字符串列表.Any(Function(keyword) 单行输出.Contains(keyword, StringComparison.OrdinalIgnoreCase)) Then
            Return Color.IndianRed
        End If
        Select Case True
            Case 单行输出.Contains("Input #", StringComparison.OrdinalIgnoreCase)
                Return Color.LightGreen
            Case 单行输出.Contains("Duration:", StringComparison.OrdinalIgnoreCase)
                Return Color.CornflowerBlue
            Case 单行输出.Contains("Stream #", StringComparison.OrdinalIgnoreCase)
                Return Color.Plum
            Case 单行输出.Contains("Output #", StringComparison.OrdinalIgnoreCase)
                Return Color.Goldenrod
        End Select
        Return 默认颜色
    End Function


End Class
