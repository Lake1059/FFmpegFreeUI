Imports System.Text
Imports System.IO
Imports LakeUI

Partial Public Class Form_v6_Agent
    Private _restoringDraft As Boolean
    Private _draftSaveTimer As System.Windows.Forms.Timer
    Private ReadOnly _emptyDraftPaths As New List(Of String)
    Private _refreshingSubmittedFiles As Boolean = False
    Private ReadOnly Property _pendingFiles As List(Of String)
        Get
            If _current Is Nothing Then Return _emptyDraftPaths
            If _current.DraftPaths Is Nothing Then _current.DraftPaths = New List(Of String)
            Return _current.DraftPaths
        End Get
    End Property

    Private Sub InitializeSubmittedFileList()
        ModernListBox2.AllowDragReorder = True
        ModernListBox2.TabStop = True
        ModernListBox2.AllowDrop = True
        RefreshSubmittedFileList()
    End Sub

    Private Sub ModernTextBox1_TextChanged(sender As Object, e As EventArgs) Handles ModernTextBox1.TextChanged
        If _restoringDraft OrElse _current Is Nothing Then Return
        _current.DraftText = ModernTextBox1.Text
        ScheduleDraftSave()
        UpdateSendButtonState()
    End Sub

    Private Sub ScheduleDraftSave()
        If _restoringDraft OrElse _closing OrElse _store Is Nothing Then Return
        If _draftSaveTimer Is Nothing Then
            _draftSaveTimer = New System.Windows.Forms.Timer With {.Interval = 600}
            AddHandler _draftSaveTimer.Tick, AddressOf DraftSaveTimer_Tick
        End If
        _draftSaveTimer.Stop()
        _draftSaveTimer.Start()
        UpdateSendButtonState()
    End Sub

    Private Sub DraftSaveTimer_Tick(sender As Object, e As EventArgs)
        _draftSaveTimer.Stop()
        Try
            _store?.Save()
        Catch ex As Exception
            ShowStatus("保存草稿失败：" & ex.Message)
        End Try
    End Sub

    Private Sub CaptureCurrentDraft()
        If _current Is Nothing Then Return
        _current.DraftText = ModernTextBox1.Text
    End Sub


    Private Sub RestoreCurrentDraft()
        _restoringDraft = True
        Dim undoLimit = ModernTextBox1.MaxUndoCount
        Try
            ' 换入另一份草稿时清除控件撤销栈，防止 Ctrl+Z 带回其他对话的文字。
            ModernTextBox1.MaxUndoCount = 0
            ModernTextBox1.Text = If(_current?.DraftText, "")
            ModernTextBox1.SelectionStart = ModernTextBox1.Text.Length
            ModernListBox2.SelectedIndex = -1
            RefreshSubmittedFileList()
        Finally
            ModernTextBox1.MaxUndoCount = undoLimit
            _restoringDraft = False
        End Try
        UpdateSendButtonState()
    End Sub

    Private Sub AddSubmittedFiles(paths As IEnumerable(Of String))
        If paths Is Nothing Then Return

        For Each pathValue In NormalizeSubmittedPaths(paths)
            If _pendingFiles.Any(Function(x) String.Equals(x, pathValue, StringComparison.OrdinalIgnoreCase)) Then Continue For
            _pendingFiles.Add(pathValue)
        Next
        RefreshSubmittedFileList()
        ScheduleDraftSave()
    End Sub

    Private Function NormalizeSubmittedPaths(paths As IEnumerable(Of String)) As List(Of String)
        Dim result As New List(Of String)
        If paths Is Nothing Then Return result

        For Each raw In paths
            If String.IsNullOrWhiteSpace(raw) Then Continue For
            Try
                If Directory.Exists(raw) Then
                    result.Add(Path.TrimEndingDirectorySeparator(Path.GetFullPath(raw)))
                ElseIf File.Exists(raw) Then
                    result.Add(Path.GetFullPath(raw))
                End If
            Catch ex As Exception
                ExFloatingTip(ModernListBox2, "读取路径失败：" & ex.Message, 2200)
            End Try
        Next

        Return result
    End Function

    Private Sub RefreshSubmittedFileList()
        If ModernListBox2 Is Nothing Then Return
        Dim selectedPath = If(ModernListBox2.SelectedIndex >= 0 AndAlso ModernListBox2.SelectedIndex < _pendingFiles.Count, _pendingFiles(ModernListBox2.SelectedIndex), "")
        _refreshingSubmittedFiles = True
        Try
            ModernListBox2.Items.Clear()
            ModernListBox2.ItemToolTips.Clear()
            For Each pathValue In _pendingFiles
                Dim display = BuildSubmittedFileDisplayName(pathValue)
                ModernListBox2.Items.Add(display)
                ModernListBox2.ItemToolTips.Add(New LakeUI.ModernListBox.ToolTipEntry(display, pathValue))
            Next
            If selectedPath <> "" Then
                Dim index = _pendingFiles.FindIndex(Function(x) String.Equals(x, selectedPath, StringComparison.OrdinalIgnoreCase))
                If index >= 0 Then ModernListBox2.SelectedIndex = index
            End If
        Finally
            _refreshingSubmittedFiles = False
        End Try
    End Sub

    Private Function BuildSubmittedFileDisplayName(pathValue As String) As String
        Dim name = GetSubmittedPathName(pathValue)
        Dim prefix = If(Directory.Exists(pathValue), "[文件夹] ", "")
        Dim sameNameCount = _pendingFiles.Where(Function(x) String.Equals(GetSubmittedPathName(x), name, StringComparison.CurrentCultureIgnoreCase)).Count()
        If sameNameCount <= 1 Then Return prefix & name
        Dim parent = Path.GetFileName(Path.GetDirectoryName(pathValue))
        If parent = "" Then parent = Path.GetDirectoryName(pathValue)
        Return $"{prefix}{name}  ({parent})"
    End Function

    Private Function GetSubmittedPathName(pathValue As String) As String
        Dim name = Path.GetFileName(Path.TrimEndingDirectorySeparator(pathValue))
        Return If(name = "", pathValue, name)
    End Function

    Private Sub RemoveSelectedSubmittedFile()
        If ModernListBox2.SelectedIndex < 0 OrElse ModernListBox2.SelectedIndex >= _pendingFiles.Count Then Return
        _pendingFiles.RemoveAt(ModernListBox2.SelectedIndex)
        RefreshSubmittedFileList()
        ScheduleDraftSave()
    End Sub

    Private Sub ClearSubmittedFiles()
        If _pendingFiles.Count = 0 Then Return
        _pendingFiles.Clear()
        RefreshSubmittedFileList()
        ScheduleDraftSave()
    End Sub

    Private Function BuildSubmittedFilesContext() As String
        If _pendingFiles.Count = 0 Then Return ""
        Return String.Join(vbCrLf, _pendingFiles.Select(Function(pathValue) BuildSubmittedFileContextItem(pathValue)))
    End Function

    Private Function BuildSubmittedFileContextItem(pathValue As String) As String
        Try
            Return Path.GetFullPath(pathValue)
        Catch ex As Exception
            Return pathValue
        End Try
    End Function

    Private Sub ModernListBox2_DragEnter(sender As Object, e As DragEventArgs) Handles ModernListBox2.DragEnter
        e.Effect = If(e.Data IsNot Nothing AndAlso e.Data.GetDataPresent(DataFormats.FileDrop), DragDropEffects.Copy, DragDropEffects.None)
    End Sub

    Private Sub ModernListBox2_DragDrop(sender As Object, e As DragEventArgs) Handles ModernListBox2.DragDrop
        If e.Data Is Nothing OrElse Not e.Data.GetDataPresent(DataFormats.FileDrop) Then Return
        Dim files = TryCast(e.Data.GetData(DataFormats.FileDrop), String())
        AddSubmittedFiles(files)
    End Sub

    Private Sub ModernListBox2_MouseUp(sender As Object, e As MouseEventArgs) Handles ModernListBox2.MouseUp
        If e.Button <> MouseButtons.Right Then Return
        Using d As New OpenFileDialog With {.Multiselect = True, .Filter = "所有文件|*.*"}
            If d.ShowDialog(Me) = DialogResult.OK Then AddSubmittedFiles(d.FileNames)
        End Using
    End Sub

    Private Sub ModernListBox2_ItemDoubleClick(sender As Object, e As LakeUI.ModernListBox.ItemEventArgs) Handles ModernListBox2.ItemDoubleClick
        RemoveSelectedSubmittedFile()
    End Sub

    Private Sub ModernListBox2_KeyDown(sender As Object, e As KeyEventArgs) Handles ModernListBox2.KeyDown
        If e.KeyCode <> Keys.Delete Then Return
        RemoveSelectedSubmittedFile()
        e.Handled = True
        e.SuppressKeyPress = True
    End Sub

    Private Sub ModernListBox2_ItemOrderChanged(sender As Object, e As EventArgs) Handles ModernListBox2.ItemOrderChanged
        If _refreshingSubmittedFiles Then Return
        If ModernListBox2.Items.Count <> _pendingFiles.Count Then Return
        Dim remaining As New List(Of String)(_pendingFiles)
        Dim reordered As New List(Of String)
        For Each rawItem In ModernListBox2.Items
            Dim text = If(rawItem, "").ToString()
            Dim index = remaining.FindIndex(Function(x) String.Equals(BuildSubmittedFileDisplayName(x), text, StringComparison.Ordinal))
            If index < 0 Then Continue For
            reordered.Add(remaining(index))
            remaining.RemoveAt(index)
        Next
        If reordered.Count <> _pendingFiles.Count Then Return
        _pendingFiles.Clear()
        _pendingFiles.AddRange(reordered)
        ScheduleDraftSave()
    End Sub
End Class
