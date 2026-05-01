Public Class 界面_媒体信息

    Private Sub 界面_媒体信息_Load(sender As Object, e As EventArgs) Handles Me.Load
        设置富文本框行高(RichTextBox1, 350)
        AddHandler UiButton1.DragEnter, AddressOf 文件拖入事件
        AddHandler Label123.DragEnter, AddressOf 文件拖入事件
        AddHandler Panel9.DragEnter, AddressOf 文件拖入事件
        AddHandler UiButton1.DragDrop, AddressOf 文件放下事件
        AddHandler Label123.DragDrop, AddressOf 文件放下事件
        AddHandler Panel9.DragDrop, AddressOf 文件放下事件
    End Sub

    Private Sub UiButton1_Click(sender As Object, e As EventArgs) Handles UiButton1.Click
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
        Me.RichTextBox1.Text = ""
        Dim FFprobeProcess As New Process
        FFprobeProcess = New Process()
        FFprobeProcess.StartInfo.FileName = "ffprobe"
        FFprobeProcess.StartInfo.WorkingDirectory = If(用户设置.实例对象.工作目录 <> "", 用户设置.实例对象.工作目录, "")
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
            界面线程执行(Sub() 在富文本框输出(Me.RichTextBox1, e.Data))
        Catch ex As Exception
        End Try
    End Sub

    Sub 调整界面()
        RichTextBox1.Size = New Size(RichTextBox1.Parent.Width - RichTextBox1.Parent.Padding.Left, RichTextBox1.Parent.Height - RichTextBox1.Parent.Padding.Top * 2)
    End Sub

End Class
