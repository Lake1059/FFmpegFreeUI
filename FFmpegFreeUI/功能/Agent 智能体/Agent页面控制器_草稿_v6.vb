Imports System.Text
Imports System.IO
Imports LakeUI

Partial Friend Class Agent页面控制器_v6
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
        _view.ModernListBox2.AllowDragReorder = True
        _view.ModernListBox2.TabStop = True
        _view.ModernListBox2.AllowDrop = True
        RefreshSubmittedFileList()
    End Sub

    Friend Sub ModernTextBox1_TextChanged(sender As Object, e As EventArgs)
        If _restoringDraft OrElse _current Is Nothing Then Return
        _current.DraftText = _view.ModernTextBox1.Text
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
        _current.DraftText = _view.ModernTextBox1.Text
    End Sub


    Private Sub RestoreCurrentDraft()
        ' 临时草稿只用于页面尚未绑定会话的短暂阶段；切换到已有会话时丢弃未归属的临时附件，
        ' 避免它们在之后创建会话时误附到另一条对话。
        If _current IsNot Nothing AndAlso _emptyDraftPaths.Count > 0 Then _emptyDraftPaths.Clear()
        _restoringDraft = True
        Dim undoLimit = _view.ModernTextBox1.MaxUndoCount
        Try
            ' 换入另一份草稿时清除控件撤销栈，防止 Ctrl+Z 带回其他对话的文字。
            _view.ModernTextBox1.MaxUndoCount = 0
            _view.ModernTextBox1.Text = If(_current?.DraftText, "")
            _view.ModernTextBox1.SelectionStart = _view.ModernTextBox1.Text.Length
            _view.ModernListBox2.SelectedIndex = -1
            RefreshSubmittedFileList()
        Finally
            _view.ModernTextBox1.MaxUndoCount = undoLimit
            _restoringDraft = False
        End Try
        UpdateSendButtonState()
    End Sub

    Private Sub AddSubmittedFiles(paths As IEnumerable(Of String))
        If paths Is Nothing Then Return

        ' 首次拖入文件时可能还没有选中会话。立即建立会话，让附件始终归属于明确的草稿，
        ' 同时保留 CreateConversationFromCurrentSettings 中对旧临时草稿的迁移兜底。
        If _current Is Nothing Then
            EnsureStoreLoaded()
            _current = CreateConversationFromCurrentSettings()
            RestoreCurrentDraft()
        End If

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
                ExFloatingTip(_view.ModernListBox2, "读取路径失败：" & ex.Message, 2200)
            End Try
        Next

        Return result
    End Function

    Private Sub RefreshSubmittedFileList()
        If _view.ModernListBox2 Is Nothing Then Return
        Dim selectedPath = If(_view.ModernListBox2.SelectedIndex >= 0 AndAlso _view.ModernListBox2.SelectedIndex < _pendingFiles.Count, _pendingFiles(_view.ModernListBox2.SelectedIndex), "")
        _refreshingSubmittedFiles = True
        Try
            _view.ModernListBox2.Items.Clear()
            _view.ModernListBox2.ItemToolTips.Clear()
            For Each pathValue In _pendingFiles
                Dim display = BuildSubmittedFileDisplayName(pathValue)
                _view.ModernListBox2.Items.Add(display)
                _view.ModernListBox2.ItemToolTips.Add(New LakeUI.ModernListBox.ToolTipEntry(display, pathValue))
            Next
            If selectedPath <> "" Then
                Dim index = _pendingFiles.FindIndex(Function(x) String.Equals(x, selectedPath, StringComparison.OrdinalIgnoreCase))
                If index >= 0 Then _view.ModernListBox2.SelectedIndex = index
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
        If _view.ModernListBox2.SelectedIndex < 0 OrElse _view.ModernListBox2.SelectedIndex >= _pendingFiles.Count Then Return
        _pendingFiles.RemoveAt(_view.ModernListBox2.SelectedIndex)
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

    Friend Sub ModernListBox2_DragEnter(sender As Object, e As DragEventArgs)
        e.Effect = If(e.Data IsNot Nothing AndAlso e.Data.GetDataPresent(DataFormats.FileDrop), DragDropEffects.Copy, DragDropEffects.None)
    End Sub

    Friend Sub ModernListBox2_DragDrop(sender As Object, e As DragEventArgs)
        If e.Data Is Nothing OrElse Not e.Data.GetDataPresent(DataFormats.FileDrop) Then Return
        Dim files = TryCast(e.Data.GetData(DataFormats.FileDrop), String())
        AddSubmittedFiles(files)
    End Sub

    Friend Sub ModernListBox2_MouseUp(sender As Object, e As MouseEventArgs)
        If e.Button <> MouseButtons.Right Then Return
        Using d As New OpenFileDialog With {.Multiselect = True, .Filter = "所有文件|*.*"}
            If d.ShowDialog(_view) = DialogResult.OK Then AddSubmittedFiles(d.FileNames)
        End Using
    End Sub

    Friend Sub ModernListBox2_ItemDoubleClick(sender As Object, e As LakeUI.ModernListBox.ItemEventArgs)
        RemoveSelectedSubmittedFile()
    End Sub

    Friend Sub ModernListBox2_KeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode <> Keys.Delete Then Return
        RemoveSelectedSubmittedFile()
        e.Handled = True
        e.SuppressKeyPress = True
    End Sub

    Friend Sub ModernListBox2_ItemOrderChanged(sender As Object, e As EventArgs)
        If _refreshingSubmittedFiles Then Return
        If _view.ModernListBox2.Items.Count <> _pendingFiles.Count Then Return
        Dim remaining As New List(Of String)(_pendingFiles)
        Dim reordered As New List(Of String)
        For Each rawItem In _view.ModernListBox2.Items
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
