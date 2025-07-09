Public Class 界面_混流

    Private Sub 界面_混流_Load(sender As Object, e As EventArgs) Handles Me.Load
        暗黑列表视图自绘制.绑定列表视图事件(Me.ListView1)
        Me.ListView1.SmallImageList = Form1.ImageList1
        AddHandler UiTextBox1.TextChanged, Sub()
                                               If Not UiTextBox1.Focused Then Exit Sub
                                               If Me.ListView1.SelectedItems.Count <> 1 Then Exit Sub
                                               Me.ListView1.SelectedItems(0).SubItems(1).Text = UiTextBox1.Text
                                           End Sub
        AddHandler UiTextBox2.TextChanged, Sub()
                                               If Not UiTextBox2.Focused Then Exit Sub
                                               If Me.ListView1.SelectedItems.Count <> 1 Then Exit Sub
                                               Me.ListView1.SelectedItems(0).SubItems(2).Text = UiTextBox2.Text
                                           End Sub
        AddHandler UiTextBox3.TextChanged, Sub()
                                               If Not UiTextBox3.Focused Then Exit Sub
                                               If Me.ListView1.SelectedItems.Count <> 1 Then Exit Sub
                                               Me.ListView1.SelectedItems(0).SubItems(3).Text = UiTextBox3.Text
                                           End Sub
        AddHandler UiCheckBox1.MouseClick, Sub()
                                               If Me.ListView1.SelectedItems.Count <> 1 Then Exit Sub
                                               If UiCheckBox1.Checked Then
                                                   For Each item In Me.ListView1.Items
                                                       item.SubItems(4).Text = ""
                                                   Next
                                                   Me.ListView1.SelectedItems(0).SubItems(4).Text = "使用此"
                                               Else
                                                   Me.ListView1.SelectedItems(0).SubItems(4).Text = ""
                                               End If
                                           End Sub
        AddHandler UiCheckBox2.MouseClick, Sub()
                                               If Me.ListView1.SelectedItems.Count <> 1 Then Exit Sub
                                               If UiCheckBox2.Checked Then
                                                   For Each item In Me.ListView1.Items
                                                       item.SubItems(5).Text = ""
                                                   Next
                                                   Me.ListView1.SelectedItems(0).SubItems(5).Text = "使用此"
                                               Else
                                                   Me.ListView1.SelectedItems(0).SubItems(5).Text = ""
                                               End If
                                           End Sub

    End Sub

    Sub 界面校准()
        Me.ListView1.Columns(1).Width = Label视频.Width
        Me.ListView1.Columns(2).Width = Label音频.Width
        Me.ListView1.Columns(3).Width = Label字幕.Width
        Me.ListView1.Columns(4).Width = Label章节.Width
        Me.ListView1.Columns(5).Width = Label元数据.Width - SystemInformation.VerticalScrollBarWidth * Form1.DPI * 2
        Me.ListView1.Columns(0).Width = Me.ListView1.Width - Me.ListView1.Columns(1).Width - Me.ListView1.Columns(2).Width - Me.ListView1.Columns(3).Width - Me.ListView1.Columns(4).Width - Me.ListView1.Columns(5).Width - SystemInformation.VerticalScrollBarWidth * Form1.DPI * 2 - 5 * Form1.DPI

        UiCheckBox1.CheckBoxSize = 20 * Form1.DPI
        UiCheckBox2.CheckBoxSize = 20 * Form1.DPI
    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged
        If Me.ListView1.SelectedItems.Count = 1 Then
            UiTextBox1.Text = Me.ListView1.SelectedItems(0).SubItems(1).Text
            UiTextBox2.Text = Me.ListView1.SelectedItems(0).SubItems(2).Text
            UiTextBox3.Text = Me.ListView1.SelectedItems(0).SubItems(3).Text
            UiCheckBox1.Checked = Me.ListView1.SelectedItems(0).SubItems(4).Text = "使用此"
            UiCheckBox2.Checked = Me.ListView1.SelectedItems(0).SubItems(5).Text = "使用此"
        Else
            UiTextBox1.Text = ""
            UiTextBox2.Text = ""
            UiTextBox3.Text = ""
            UiCheckBox1.Checked = False
            UiCheckBox2.Checked = False
        End If
    End Sub

    Private Sub ListView1_KeyDown(sender As Object, e As KeyEventArgs) Handles ListView1.KeyDown
        Select Case e.KeyCode
            Case Keys.F3
                UiButton上移.PerformClick()
                ListView1.Focus()
            Case Keys.F4
                UiButton下移.PerformClick()
                ListView1.Focus()
            Case Keys.Delete
                UiButton移除.PerformClick()
                ListView1.Focus()
        End Select
    End Sub

    Private Sub ListView1_DragEnter(sender As Object, e As DragEventArgs) Handles ListView1.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        End If
    End Sub
    Private Sub ListView1_DragDrop(sender As Object, e As DragEventArgs) Handles ListView1.DragDrop
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            Dim a As String() = e.Data.GetData(DataFormats.FileDrop)
            For Each b As String In a
                Dim item As New ListViewItem With {.Text = b}
                item.SubItems.Add("")
                item.SubItems.Add("")
                item.SubItems.Add("")
                item.SubItems.Add("")
                item.SubItems.Add("")
                ListView1.Items.Add(item)
            Next
        End If
    End Sub

    Private Sub UiButton添加文件_Click(sender As Object, e As EventArgs) Handles UiButton添加文件.Click
        Dim ofd As New OpenFileDialog With {
            .Multiselect = True,
            .Filter = "所有文件|*.*"
        }
        If ofd.ShowDialog() = DialogResult.OK Then
            For Each file In ofd.FileNames
                Dim item As New ListViewItem With {.Text = file}
                item.SubItems.Add("")
                item.SubItems.Add("")
                item.SubItems.Add("")
                item.SubItems.Add("")
                item.SubItems.Add("")
                Me.ListView1.Items.Add(item)
            Next
        End If

    End Sub

    Private Sub UiButton上移_Click(sender As Object, e As EventArgs) Handles UiButton上移.Click
        If Me.ListView1.SelectedItems.Count = 0 Then Exit Sub
        For i = 0 To Me.ListView1.SelectedIndices.Count - 1
            Dim index As Integer = Me.ListView1.SelectedIndices(i)
            If index > 0 Then
                If Me.ListView1.SelectedIndices.Contains(index - 1) Then Continue For
                Dim 变动排序的列表视图项 As ListViewItem = Me.ListView1.Items(index)
                Me.ListView1.Items.RemoveAt(index)
                Me.ListView1.Items.Insert(index - 1, 变动排序的列表视图项)
                Me.ListView1.Items(index - 1).Focused = True
            End If
        Next
    End Sub

    Private Sub UiButton下移_Click(sender As Object, e As EventArgs) Handles UiButton下移.Click
        If ListView1.SelectedItems.Count = 0 Then Exit Sub

        For i = Me.ListView1.SelectedIndices.Count - 1 To 0 Step -1
            Dim index As Integer = Me.ListView1.SelectedIndices(i)
            If index < Me.ListView1.Items.Count - 1 Then
                If Me.ListView1.SelectedIndices.Contains(index + 1) Then Continue For
                Dim 变动排序的列表视图项 As ListViewItem = Me.ListView1.Items(index)
                Me.ListView1.Items.RemoveAt(index)
                Me.ListView1.Items.Insert(index + 1, 变动排序的列表视图项)
                Me.ListView1.Items(index + 1).Focused = True
            End If
        Next
    End Sub
    Private Sub UiButton移除_Click(sender As Object, e As EventArgs) Handles UiButton移除.Click
        If Me.ListView1.SelectedItems.Count > 0 Then
            For Each item In Me.ListView1.SelectedItems.Cast(Of ListViewItem)().ToList()
                Me.ListView1.Items.Remove(item)
            Next
        End If
    End Sub

    Private Sub UiButton选择位置_Click(sender As Object, e As EventArgs) Handles UiButton选择位置.Click
        Dim a As New SaveFileDialog With {.Filter = "所有文件|*.*"}
        If a.ShowDialog(Form1) = DialogResult.OK Then
            UiTextBox输出文件.Text = a.FileName
        End If
    End Sub

    Private Sub UiButton启动混流_Click(sender As Object, e As EventArgs) Handles UiButton启动混流.Click
        If ListView1.Items.Count = 0 Then Exit Sub
        If UiTextBox输出文件.Text = "" Then Exit Sub
        Dim arg = "-hide_banner -nostdin "
        For Each item As ListViewItem In ListView1.Items
            arg &= $"-i {"""" & item.Text & """"} "
        Next

        For Each item As ListViewItem In ListView1.Items
            Dim va = item.SubItems(1).Text.Trim.Split(","c)
            For Each v In va
                If v <> "" Then
                    arg &= $" -map {item.Index}:v:{v} -c:v copy "
                End If
            Next

            Dim aa = item.SubItems(2).Text.Trim.Split(","c)
            For Each a In aa
                If a <> "" Then
                    arg &= $" -map {item.Index}:a:{a} -c:a copy "
                End If
            Next

            Dim sa = item.SubItems(3).Text.Trim.Split(","c)
            For Each s In sa
                If s <> "" Then
                    arg &= $" -map {item.Index}:s:{s} -c:s copy "
                End If
            Next

            If item.SubItems(4).Text = "使用此" Then
                arg &= $"-map_metadata {item.Index} "
            End If
            If item.SubItems(5).Text = "使用此" Then
                arg &= $"-map_chapters {item.Index} "
            End If
        Next

        arg &= """" & UiTextBox输出文件.Text & """" & " -y"

        Select Case MsgBox("确认启动混流？选择 取消 来复制命令行" & vbCrLf & vbCrLf & "ffmpeg " & arg, MsgBoxStyle.YesNoCancel)
            Case MsgBoxResult.Yes
                Dim FFmpegProcess As New Process
                FFmpegProcess.StartInfo.FileName = "ffmpeg"
                FFmpegProcess.StartInfo.WorkingDirectory = If(Form1.FFmpeg自定义工作目录 <> "", Form1.FFmpeg自定义工作目录, "")
                FFmpegProcess.StartInfo.Arguments = arg
                FFmpegProcess.Start()
            Case MsgBoxResult.No
            Case MsgBoxResult.Cancel
                Clipboard.SetText("ffmpeg " & arg)
        End Select

    End Sub


End Class
