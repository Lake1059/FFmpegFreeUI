Public Class Form_v6_参数面板_视频帧服务器

    Public 所属参数面板对象 As Form_v6_参数面板
    Private _正在同步 As Boolean = False

    Private Sub Form_v6_参数面板_视频帧服务器_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        填充脚本列表(ModernComboBox1, "AviSynth", "*.avs")
        填充脚本列表(ModernComboBox2, "VapourSynth", "*.vpy;*.py")
    End Sub

    Private Sub 填充脚本列表(combo As LakeUI.ModernComboBox, folderName As String, patternList As String)
        If combo Is Nothing Then Exit Sub
        Dim oldText = combo.Text
        combo.Items.Clear()
        combo.Items.Add("浏览 ...")
        Dim dir = IO.Path.Combine(Application.StartupPath, folderName)
        If IO.Directory.Exists(dir) Then
            For Each pattern In patternList.Split(";"c)
                For Each file In IO.Directory.EnumerateFiles(dir, pattern, IO.SearchOption.TopDirectoryOnly).OrderBy(Function(x) x, StringComparer.CurrentCultureIgnoreCase)
                    combo.Items.Add(file)
                Next
            Next
        End If
        combo.Text = oldText
    End Sub

    Private Sub BooleanSwitch1_CheckedChanged(sender As Object, e As EventArgs) Handles BooleanSwitch1.CheckedChanged
        If _正在同步 Then Exit Sub
        _正在同步 = True
        Try
            If BooleanSwitch1.Checked Then BooleanSwitch2.Checked = False
        Finally
            _正在同步 = False
        End Try
        通知参数面板刷新()
    End Sub

    Private Sub BooleanSwitch2_CheckedChanged(sender As Object, e As EventArgs) Handles BooleanSwitch2.CheckedChanged
        If _正在同步 Then Exit Sub
        _正在同步 = True
        Try
            If BooleanSwitch2.Checked Then BooleanSwitch1.Checked = False
        Finally
            _正在同步 = False
        End Try
        通知参数面板刷新()
    End Sub

    Private Sub ModernComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ModernComboBox1.SelectedIndexChanged
        If ModernComboBox1.SelectedIndex = 0 Then
            选择脚本文件(ModernComboBox1, "AviSynth 脚本|*.avs|所有文件|*.*")
            BooleanSwitch1.Checked = ModernComboBox1.Text.Trim() <> ""
        End If
        通知参数面板刷新()
    End Sub

    Private Sub ModernComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ModernComboBox2.SelectedIndexChanged
        If ModernComboBox2.SelectedIndex = 0 Then
            选择脚本文件(ModernComboBox2, "VapourSynth 脚本|*.vpy;*.py|所有文件|*.*")
            BooleanSwitch2.Checked = ModernComboBox2.Text.Trim() <> ""
        End If
        通知参数面板刷新()
    End Sub

    Private Sub ModernComboBox1_TextChanged(sender As Object, e As EventArgs) Handles ModernComboBox1.TextChanged
        If ModernComboBox1.Text.Trim() <> "浏览 ..." Then 通知参数面板刷新()
    End Sub

    Private Sub ModernComboBox2_TextChanged(sender As Object, e As EventArgs) Handles ModernComboBox2.TextChanged
        If ModernComboBox2.Text.Trim() <> "浏览 ..." Then 通知参数面板刷新()
    End Sub

    Private Sub 选择脚本文件(combo As LakeUI.ModernComboBox, filter As String)
        Using d As New OpenFileDialog With {.Filter = filter, .Multiselect = False}
            If d.ShowDialog(Me) = DialogResult.OK AndAlso d.FileName <> "" Then
                combo.Text = d.FileName
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
