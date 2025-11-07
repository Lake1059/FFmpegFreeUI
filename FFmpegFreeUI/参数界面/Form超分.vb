Imports System.IO

Public Class Form超分
    Private Sub Form超分_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        暗黑列表视图自绘制.绑定列表视图事件2(ListView1)
        ListView1.SmallImageList = Form1.ImageList1
    End Sub

    Private Sub Form超分_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        校准UiComboBox视觉(UiComboBox上采样算法)
        校准UiComboBox视觉(UiComboBox下采样算法)
        界面校准()
    End Sub

    Private Sub Form超分_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged
        If Me.WindowState <> FormWindowState.Minimized AndAlso Me.Visible Then
            界面校准()
        End If
    End Sub

    Sub 界面校准()
        Me.ListView1.Columns(0).Width = Me.ListView1.Width - SystemInformation.VerticalScrollBarWidth * Form1.DPI
    End Sub

    Private Sub Form超分_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If UiTextBox超分宽度.Text = "" Or UiTextBox超分高度.Text = "" Then
            重置所有选项()
        End If
        e.Cancel = True
        Me.Hide()
        Form1.Focus()
    End Sub

    Sub 重置所有选项()
        UiTextBox超分宽度.Text = ""
        UiTextBox超分高度.Text = ""
        UiComboBox上采样算法.Text = ""
        UiComboBox下采样算法.Text = ""
        UiTextBox抗振铃强度.Text = ""
        ListView1.Items.Clear()
    End Sub

    Private Sub UiButton添加着色器_Click(sender As Object, e As EventArgs) Handles UiButton添加着色器.Click
        Using ofd As New OpenFileDialog
            ofd.Title = "选择着色器文件"
            ofd.Filter = "着色器文件|*.glsl;*.hook|所有文件|*.*"
            ofd.Multiselect = True
            If ofd.ShowDialog = DialogResult.OK Then
                For Each 文件路径 In ofd.FileNames
                    ListView1.Items.Add(文件路径)
                Next
            End If
        End Using
    End Sub

    Private Sub UiButton移除着色器_Click(sender As Object, e As EventArgs) Handles UiButton移除着色器.Click
        For i As Integer = ListView1.SelectedItems.Count - 1 To 0 Step -1
            ListView1.Items.Remove(ListView1.SelectedItems(i))
        Next
    End Sub

    Private Sub UiButton上移着色器_Click(sender As Object, e As EventArgs) Handles UiButton上移着色器.Click
        If ListView1.SelectedItems.Count = 0 Then
            Return
        End If
        Dim 选中项 = ListView1.SelectedItems(0)
        Dim 索引 = 选中项.Index
        If 索引 = 0 Then
            Return
        End If
        ListView1.Items.RemoveAt(索引)
        ListView1.Items.Insert(索引 - 1, 选中项)
    End Sub

    Private Sub UiButton下移着色器_Click(sender As Object, e As EventArgs) Handles UiButton下移着色器.Click
        If ListView1.SelectedItems.Count = 0 Then
            Return
        End If
        Dim 选中项 = ListView1.SelectedItems(0)
        Dim 索引 = 选中项.Index
        If 索引 = ListView1.Items.Count - 1 Then
            Return
        End If
        ListView1.Items.RemoveAt(索引)
        ListView1.Items.Insert(索引 + 1, 选中项)
    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged

    End Sub

    Private Sub ListView1_DragEnter(sender As Object, e As DragEventArgs) Handles ListView1.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub ListView1_DragDrop(sender As Object, e As DragEventArgs) Handles ListView1.DragDrop
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            Dim 文件路径数组 As String() = CType(e.Data.GetData(DataFormats.FileDrop), String())
            For Each 文件路径 In 文件路径数组
                Select Case Path.GetExtension(文件路径)
                    Case ".glsl", ".hook" : ListView1.Items.Add(文件路径)
                End Select
            Next
        End If
    End Sub

    Private Sub UiButton下载_MouseDown(sender As Object, e As MouseEventArgs) Handles UiButton下载.MouseDown
        Dim a As New 暗黑上下文菜单 With {.ShowImageMargin = False, .Font = Form1.Font}
        a.Items.AddRange(New ToolStripItem() {
             New ToolStripSeparator() With {.Tag = "null"},
             New ToolStripMenuItem("通用超分") With {.ForeColor = Color.CornflowerBlue, .Enabled = False},
             New ToolStripMenuItem("igv/FSRCNN-TensorFlow", Nothing, Sub() Process.Start(New ProcessStartInfo With {.FileName = "https://github.com/igv/FSRCNN-TensorFlow", .UseShellExecute = True})) With {.ForeColor = Color.Silver},
             New ToolStripMenuItem("gist.github.com/igv", Nothing, Sub() Process.Start(New ProcessStartInfo With {.FileName = "https://gist.github.com/igv", .UseShellExecute = True})) With {.ForeColor = Color.Silver},
             New ToolStripMenuItem("bjin/mpv-prescalers", Nothing, Sub() Process.Start(New ProcessStartInfo With {.FileName = "https://github.com/bjin/mpv-prescalers", .UseShellExecute = True})) With {.ForeColor = Color.Silver},
             New ToolStripSeparator(),
             New ToolStripMenuItem("动漫超分") With {.ForeColor = Color.CornflowerBlue, .Enabled = False},
             New ToolStripMenuItem("bloc97/Anime4K", Nothing, Sub() Process.Start(New ProcessStartInfo With {.FileName = "https://github.com/bloc97/Anime4K", .UseShellExecute = True})) With {.ForeColor = Color.Silver},
             New ToolStripSeparator() With {.Tag = "null"}})

        a.Show(sender, New Point(sender.Width - a.Width, sender.Height + sender.Parent.Padding.Bottom))
    End Sub
End Class