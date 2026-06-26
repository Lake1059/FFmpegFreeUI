Public Class Form_v6_参数面板_视频帧服务器

    Public 所属参数面板对象 As Form_v6_参数面板
    Private _正在同步 As Boolean = False

    Private Sub Form_v6_参数面板_视频帧服务器_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        填充脚本列表(MCB_AviSynth脚本文件, "AviSynth", "*.avs")
        填充脚本列表(MCB_VapourSynth脚本文件, "VapourSynth", "*.vpy;*.py")
        绑定路径下拉框拖拽(MCB_AviSynth脚本文件, 路径下拉框拖拽模式.文件路径,
                     Sub(combo, path)
                         combo.Text = 转为应用目录相对路径(path)
                         BS_使用AviSynth.Checked = MCB_AviSynth脚本文件.Text.Trim() <> ""
                     End Sub)
        绑定路径下拉框拖拽(MCB_VapourSynth脚本文件, 路径下拉框拖拽模式.文件路径,
                     Sub(combo, path)
                         combo.Text = 转为应用目录相对路径(path)
                         BS_使用VapourSynth.Checked = MCB_VapourSynth脚本文件.Text.Trim() <> ""
                     End Sub)
    End Sub

    Private Sub 填充脚本列表(combo As LakeUI.ModernComboBox, folderName As String, patternList As String)
        If combo Is Nothing Then Exit Sub
        Dim oldText = 转为应用目录相对路径(combo.Text)
        combo.Items.Clear()
        combo.Items.Add("浏览 ...")
        Dim dir = IO.Path.Combine(Application.StartupPath, folderName)
        If IO.Directory.Exists(dir) Then
            For Each pattern In patternList.Split(";"c)
                For Each file In IO.Directory.EnumerateFiles(dir, pattern, IO.SearchOption.TopDirectoryOnly).OrderBy(Function(x) x, StringComparer.CurrentCultureIgnoreCase)
                    combo.Items.Add(转为应用目录相对路径(file))
                Next
            Next
        End If
        combo.Text = oldText
    End Sub

    Private Shared Function 转为应用目录相对路径(pathText As String) As String
        Dim raw = If(pathText, "").Trim()
        If raw = "" OrElse raw = "浏览 ..." Then Return raw
        Try
            If Not IO.Path.IsPathRooted(raw) Then Return raw

            Dim appRoot = IO.Path.GetFullPath(Application.StartupPath).
                TrimEnd(IO.Path.DirectorySeparatorChar, IO.Path.AltDirectorySeparatorChar)
            Dim appRootWithSeparator = appRoot & IO.Path.DirectorySeparatorChar
            Dim full = IO.Path.GetFullPath(raw)
            Dim fullWithoutSeparator = full.TrimEnd(IO.Path.DirectorySeparatorChar, IO.Path.AltDirectorySeparatorChar)

            If String.Equals(fullWithoutSeparator, appRoot, StringComparison.OrdinalIgnoreCase) Then Return "."
            If full.StartsWith(appRootWithSeparator, StringComparison.OrdinalIgnoreCase) Then
                Return IO.Path.GetRelativePath(Application.StartupPath, full)
            End If
        Catch
        End Try
        Return raw
    End Function

    Private Sub BooleanSwitch1_CheckedChanged(sender As Object, e As EventArgs) Handles BS_使用AviSynth.CheckedChanged
        If _正在同步 Then Exit Sub
        _正在同步 = True
        Try
            If BS_使用AviSynth.Checked Then BS_使用VapourSynth.Checked = False
        Finally
            _正在同步 = False
        End Try
        通知参数面板刷新()
    End Sub

    Private Sub BooleanSwitch2_CheckedChanged(sender As Object, e As EventArgs) Handles BS_使用VapourSynth.CheckedChanged
        If _正在同步 Then Exit Sub
        _正在同步 = True
        Try
            If BS_使用VapourSynth.Checked Then BS_使用AviSynth.Checked = False
        Finally
            _正在同步 = False
        End Try
        通知参数面板刷新()
    End Sub

    Private Sub ModernComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_AviSynth脚本文件.SelectedIndexChanged
        If MCB_AviSynth脚本文件.SelectedIndex = 0 Then
            选择脚本文件(MCB_AviSynth脚本文件, "AviSynth 脚本|*.avs|所有文件|*.*")
            BS_使用AviSynth.Checked = MCB_AviSynth脚本文件.Text.Trim() <> ""
        End If
        通知参数面板刷新()
    End Sub

    Private Sub ModernComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_VapourSynth脚本文件.SelectedIndexChanged
        If MCB_VapourSynth脚本文件.SelectedIndex = 0 Then
            选择脚本文件(MCB_VapourSynth脚本文件, "VapourSynth 脚本|*.vpy;*.py|所有文件|*.*")
            BS_使用VapourSynth.Checked = MCB_VapourSynth脚本文件.Text.Trim() <> ""
        End If
        通知参数面板刷新()
    End Sub

    Private Sub ModernComboBox1_TextChanged(sender As Object, e As EventArgs) Handles MCB_AviSynth脚本文件.TextChanged
        If MCB_AviSynth脚本文件.Text.Trim() <> "浏览 ..." Then 通知参数面板刷新()
    End Sub

    Private Sub ModernComboBox2_TextChanged(sender As Object, e As EventArgs) Handles MCB_VapourSynth脚本文件.TextChanged
        If MCB_VapourSynth脚本文件.Text.Trim() <> "浏览 ..." Then 通知参数面板刷新()
    End Sub

    Private Sub 选择脚本文件(combo As LakeUI.ModernComboBox, filter As String)
        Using d As New OpenFileDialog With {.Filter = filter, .Multiselect = False}
            If d.ShowDialog(Me) = DialogResult.OK AndAlso d.FileName <> "" Then
                combo.Text = 转为应用目录相对路径(d.FileName)
            Else
                combo.SelectedIndex = -1
                combo.Text = ""
            End If
        End Using
    End Sub

    Private Sub 通知参数面板刷新()
        If _正在同步 OrElse 所属参数面板对象 Is Nothing OrElse 所属参数面板对象.抑制自动刷新 Then Exit Sub
        所属参数面板对象.请求刷新参数状态()
    End Sub

End Class
