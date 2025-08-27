Imports System.IO

Public Class 界面_合并
    Private Sub 界面_合并_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        暗黑列表视图自绘制.绑定列表视图事件(Me.ListView1)
        Me.ListView1.SmallImageList = Form1.ImageList1
    End Sub

    Sub 界面校准()
        Me.ListView1.Columns(0).Width = Me.ListView1.Width - SystemInformation.VerticalScrollBarWidth * Form1.DPI * 2
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
                ListView1.Items.Add(b)
            Next
        End If
    End Sub

    Private Sub UiButton添加文件_Click(sender As Object, e As EventArgs) Handles UiButton添加文件.Click
        Dim ofd As New OpenFileDialog With {.Multiselect = True, .Filter = "所有文件|*.*"}
        If ofd.ShowDialog() = DialogResult.OK Then
            For Each file In ofd.FileNames
                Me.ListView1.Items.Add(file)
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

    Private Sub UiButton启动合并_Click(sender As Object, e As EventArgs) Handles UiButton启动合并.Click
        If ListView1.Items.Count = 0 Then Exit Sub
        If UiTextBox输出文件.Text = "" Then Exit Sub
        Dim arg = "-hide_banner -nostdin "

        Dim fs As New List(Of String)
        For Each item As ListViewItem In ListView1.Items
            fs.Add("file '" & item.Text.Replace("\", "\\") & "'")
        Next

        File.WriteAllText(Path.Combine(Application.StartupPath, "ffmpeg_concat_demuxer.txt"), String.Join(vbCrLf, fs), New Text.UTF8Encoding(False))

        arg &= $"-f concat -safe 0 -i ""{Path.Combine(Application.StartupPath, "ffmpeg_concat_demuxer.txt")}"" "

        arg &= "-c copy """ & UiTextBox输出文件.Text & """" & " -y"

        插件管理.使用命令行添加任务到编码队列(arg, $"合并任务 {Now:HHmmss}", UiTextBox输出文件.Text)
        Form1.UiTabControlMenu1.SelectedTab = Form1.TabPage编码队列
        Task.Run(AddressOf 编码任务.检查是否有可以开始的任务)

    End Sub

End Class
