Imports System.Globalization
Imports System.IO
Imports LakeUI

Public Class Form_v6_参数面板_画面区域选择窗口

    Public 目标控件 As Control = Nothing
    Private _正在同步框选文本 As Boolean = False

    Private Sub Form_v6_参数面板_画面区域选择窗口_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = FormMain_v6.Icon
        SetControlFont(设置_v6.实例对象.字体, Me, , True)
        绑定文件拖放(Me)
        If FormMain_v6.ThisIsYourWindow1.AttachedForms.Count > 0 Then
            Me.ModernPanel1.BackColor = Color.Transparent
            FormMain_v6.ThisIsYourWindow1.Attach(Me)
        End If
    End Sub

    Private Sub Form_v6_参数面板_画面区域选择窗口_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        _正在同步框选文本 = True
        Try
            ModernTextBox1.Text = If(目标控件?.Text, "")
        Finally
            _正在同步框选文本 = False
        End Try
    End Sub

    Private Sub MB_打开_Click(sender As Object, e As EventArgs) Handles MB_打开.Click
        打开媒体获取画面(ShowFileOpenDialog())
    End Sub

    Private Sub MB_完成_Click(sender As Object, e As EventArgs) Handles MB_完成.Click
        目标控件.Text = ModernTextBox1.Text
        关闭窗体流程()
    End Sub

    Private Function ShowFileOpenDialog() As String
        Using openFileDialog As New OpenFileDialog With {
            .Multiselect = False,
            .Filter = "媒体文件|*.mp4;*.mkv;*.mov;*.m4v;*.avi;*.wmv;*.webm;*.flv;*.ts;*.m2ts;*.mts;*.mpg;*.mpeg;*.3gp;*.bmp;*.dib;*.gif;*.ico;*.jfif;*.jpe;*.jpeg;*.jpg;*.png;*.tif;*.tiff;*.webp|图片文件|*.bmp;*.dib;*.gif;*.ico;*.jfif;*.jpe;*.jpeg;*.jpg;*.png;*.tif;*.tiff;*.webp|视频文件|*.mp4;*.mkv;*.mov;*.m4v;*.avi;*.wmv;*.webm;*.flv;*.ts;*.m2ts;*.mts;*.mpg;*.mpeg;*.3gp|所有文件|*.*"
        }
            If openFileDialog.ShowDialog() = DialogResult.OK Then
                Return openFileDialog.FileName
            End If
        End Using
        Return String.Empty
    End Function

    Private Function ExtractFrameFromMedia(mediaPath As String, outputPath As String, useTimestamp As Boolean) As Boolean
        Try
            Using process As New Process()
                process.StartInfo.FileName = "ffmpeg"
                process.StartInfo.WorkingDirectory = If(设置_v6.实例对象.工作目录 <> "", 设置_v6.实例对象.工作目录, "")
                process.StartInfo.UseShellExecute = False
                process.StartInfo.CreateNoWindow = True
                If useTimestamp Then
                    process.StartInfo.ArgumentList.Add("-ss")
                    process.StartInfo.ArgumentList.Add(获取视频截图时间戳())
                End If
                process.StartInfo.ArgumentList.Add("-i")
                process.StartInfo.ArgumentList.Add(mediaPath)
                process.StartInfo.ArgumentList.Add("-frames:v")
                process.StartInfo.ArgumentList.Add("1")
                process.StartInfo.ArgumentList.Add("-q:v")
                process.StartInfo.ArgumentList.Add("1")
                process.StartInfo.ArgumentList.Add(outputPath)
                process.StartInfo.ArgumentList.Add("-y")
                process.Start()
                process.WaitForExit()
                Return process.ExitCode = 0 AndAlso File.Exists(outputPath)
            End Using
        Catch ex As Exception
            Debug.WriteLine($"提取媒体帧错误: {ex.Message}")
            Return False
        End Try
    End Function

    Private Function 获取视频截图时间戳() As String
        Dim timestamp = ModernTextBox2.Text.Trim()
        Return If(timestamp <> "", timestamp, "0:0:10")
    End Function

    Sub 打开媒体获取画面(媒体文件 As String)
        If String.IsNullOrWhiteSpace(媒体文件) Then Exit Sub
        If Not File.Exists(媒体文件) Then
            ExFloatingTip(PixelPictureBox1, "文件不存在", 1800)
            Exit Sub
        End If

        Dim image As Image = Nothing
        If TryLoadImageDirectly(媒体文件, image) Then
            设置预览图像(image)
            Exit Sub
        End If

        Dim previewPath As String = Path.Combine(Path.GetTempPath(), $"FFmpegFreeUI_ScreenCropPreview_{Guid.NewGuid():N}.png")
        Try
            Dim useTimestamp = Not 是图片扩展(媒体文件)
            If Not ExtractFrameFromMedia(媒体文件, previewPath, useTimestamp) Then
                ExFloatingTip(PixelPictureBox1, "无法读取媒体画面", 2200)
                Exit Sub
            End If

            Dim previewImage As Image = Nothing
            If TryLoadImageDirectly(previewPath, previewImage) Then
                设置预览图像(previewImage)
            Else
                ExFloatingTip(PixelPictureBox1, "无法加载预览图像", 2200)
            End If
        Finally
            Try
                If File.Exists(previewPath) Then File.Delete(previewPath)
            Catch ex As Exception
                Debug.WriteLine($"删除预览图失败: {ex.Message}")
            End Try
        End Try
    End Sub

    Private Sub Form_v6_参数面板_画面区域选择窗口_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        e.Cancel = True
        关闭窗体流程()
    End Sub

    Sub 关闭窗体流程()
        目标控件?.FindForm?.Focus()
        目标控件 = Nothing
        清空预览图像()
        Me.Text = ""
        Me.Hide()
    End Sub

    Private Sub 设置预览图像(image As Image)
        清空预览图像()
        PixelPictureBox1.Image = image
        应用裁剪文本到框选(False)
    End Sub

    Private Sub 清空预览图像()
        _正在同步框选文本 = True
        Try
            Dim oldImage = PixelPictureBox1.Image
            PixelPictureBox1.Image = Nothing
            oldImage?.Dispose()
            PixelPictureBox1.ClearSelection()
        Finally
            _正在同步框选文本 = False
        End Try
    End Sub

    Private Function TryLoadImageDirectly(filePath As String, ByRef image As Image) As Boolean
        image = Nothing
        Try
            Using stream As New FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
                Using source = Image.FromStream(stream, False, False)
                    image = New Bitmap(source)
                End Using
            End Using
            Return True
        Catch
            image = Nothing
            Return False
        End Try
    End Function

    Private Function 是图片扩展(filePath As String) As Boolean
        Select Case Path.GetExtension(filePath).ToLowerInvariant()
            Case ".apng", ".avif", ".bmp", ".dib", ".dpx", ".exr", ".gif", ".hdr", ".heic", ".heif",
                 ".ico", ".jfif", ".jpe", ".jpeg", ".jpg", ".jxl", ".pam", ".pbm", ".pcx", ".pfm",
                 ".pgm", ".png", ".ppm", ".psd", ".qoi", ".sgi", ".svg", ".tga", ".tif", ".tiff",
                 ".webp", ".xbm", ".xpm"
                Return True
            Case Else
                Return False
        End Select
    End Function

    Private Sub 绑定文件拖放(root As Control)
        If root Is Nothing Then Exit Sub
        root.AllowDrop = True
        RemoveHandler root.DragEnter, AddressOf 文件拖入事件
        RemoveHandler root.DragDrop, AddressOf 文件放下事件
        AddHandler root.DragEnter, AddressOf 文件拖入事件
        AddHandler root.DragDrop, AddressOf 文件放下事件

        For Each child As Control In root.Controls
            绑定文件拖放(child)
        Next
    End Sub

    Private Sub 文件拖入事件(sender As Object, e As DragEventArgs)
        e.Effect = If(获取拖入文件路径(e) <> "", DragDropEffects.Copy, DragDropEffects.None)
    End Sub

    Private Sub 文件放下事件(sender As Object, e As DragEventArgs)
        Dim filePath = 获取拖入文件路径(e)
        If filePath <> "" Then 打开媒体获取画面(filePath)
    End Sub

    Private Function 获取拖入文件路径(e As DragEventArgs) As String
        If e Is Nothing OrElse e.Data Is Nothing OrElse Not e.Data.GetDataPresent(DataFormats.FileDrop) Then Return ""
        Dim files = TryCast(e.Data.GetData(DataFormats.FileDrop), String())
        If files Is Nothing Then Return ""
        For Each filePath In files
            If File.Exists(filePath) Then Return filePath
        Next
        Return ""
    End Function

    Private Sub PixelPictureBox1_SelectionChanged(sender As Object, e As EventArgs) Handles PixelPictureBox1.SelectionChanged
        If _正在同步框选文本 Then Exit Sub

        Dim r = PixelPictureBox1.SelectionRect
        _正在同步框选文本 = True
        Try
            ModernTextBox1.Text = If(r.Width > 0 AndAlso r.Height > 0, $"{r.Width}:{r.Height}:{r.X}:{r.Y}", "")
        Finally
            _正在同步框选文本 = False
        End Try
    End Sub

    Private Sub ModernTextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles ModernTextBox1.KeyDown
        If e.KeyCode <> Keys.Enter Then Exit Sub
        If 应用裁剪文本到框选(True) Then e.Handled = True
    End Sub

    Private Sub ModernTextBox1_Leave(sender As Object, e As EventArgs) Handles ModernTextBox1.Leave
        应用裁剪文本到框选(False)
    End Sub

    Private Function 应用裁剪文本到框选(showTip As Boolean) As Boolean
        If _正在同步框选文本 OrElse PixelPictureBox1.Image Is Nothing Then Return False

        Dim rect As Rectangle
        If Not 尝试解析裁剪参数(ModernTextBox1.Text, rect) Then
            If showTip AndAlso ModernTextBox1.Text.Trim() <> "" Then
                ExFloatingTip(ModernTextBox1, "格式应为 宽:高:左上X:左上Y", 1800)
            End If
            Return False
        End If

        rect = 约束裁剪区域到图像(rect)
        _正在同步框选文本 = True
        Try
            PixelPictureBox1.SelectionRect = rect
            ModernTextBox1.Text = $"{rect.Width}:{rect.Height}:{rect.X}:{rect.Y}"
        Finally
            _正在同步框选文本 = False
        End Try
        Return True
    End Function

    Private Function 尝试解析裁剪参数(text As String, ByRef rect As Rectangle) As Boolean
        rect = Rectangle.Empty
        Dim parts = If(text, "").Trim().Split(":"c)
        If parts.Length <> 4 Then Return False

        Dim w, h, x, y As Integer
        If Not Integer.TryParse(parts(0).Trim(), w) Then Return False
        If Not Integer.TryParse(parts(1).Trim(), h) Then Return False
        If Not Integer.TryParse(parts(2).Trim(), x) Then Return False
        If Not Integer.TryParse(parts(3).Trim(), y) Then Return False
        If w <= 0 OrElse h <= 0 OrElse x < 0 OrElse y < 0 Then Return False

        rect = New Rectangle(x, y, w, h)
        Return True
    End Function

    Private Function 约束裁剪区域到图像(rect As Rectangle) As Rectangle
        Dim image = PixelPictureBox1.Image
        If image Is Nothing Then Return rect

        Dim x = Math.Max(0, Math.Min(rect.X, image.Width - 1))
        Dim y = Math.Max(0, Math.Min(rect.Y, image.Height - 1))
        Dim w = Math.Max(1, Math.Min(rect.Width, image.Width - x))
        Dim h = Math.Max(1, Math.Min(rect.Height, image.Height - y))
        Return New Rectangle(x, y, w, h)
    End Function

    Private Sub ModernCheckBox4_CheckedChanged(sender As Object, e As EventArgs) Handles ModernCheckBox4.CheckedChanged
        PixelPictureBox1.SelectionForceCenter = ModernCheckBox4.Checked
        PixelPictureBox1_SelectionChanged(PixelPictureBox1, EventArgs.Empty)
    End Sub

    Private Sub ModernComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ModernComboBox1.SelectedIndexChanged
        应用比例文本(False)
    End Sub

    Private Sub ModernComboBox1_TextChanged(sender As Object, e As EventArgs) Handles ModernComboBox1.TextChanged
        应用比例文本(False)
    End Sub

    Private Sub ModernComboBox1_Leave(sender As Object, e As EventArgs) Handles ModernComboBox1.Leave
        应用比例文本(True)
    End Sub

    Private Sub ModernComboBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles ModernComboBox1.KeyDown
        If e.KeyCode <> Keys.Enter Then Exit Sub
        If 应用比例文本(True) Then e.Handled = True
    End Sub

    Private Function 应用比例文本(showTip As Boolean) As Boolean
        Dim ratio As Single
        If 尝试解析比例文本(ModernComboBox1.Text, ratio) Then
            PixelPictureBox1.SelectionAspectRatio = ratio
            Return True
        End If

        If showTip AndAlso ModernComboBox1.Text.Trim() <> "" Then
            ExFloatingTip(ModernComboBox1, "比例格式可写 16:9、16/9 或 1.777", 1800)
        End If
        Return False
    End Function

    Private Function 尝试解析比例文本(text As String, ByRef ratio As Single) As Boolean
        ratio = 0
        Dim value = If(text, "").Trim()
        If value = "" OrElse String.Equals(value, "自由", StringComparison.OrdinalIgnoreCase) Then Return True

        value = value.Replace("：", ":").Replace("/", ":").Replace("x", ":").Replace("X", ":")
        Dim parts = value.Split(":"c, StringSplitOptions.RemoveEmptyEntries Or StringSplitOptions.TrimEntries)

        If parts.Length = 2 Then
            Dim w, h As Double
            If Not 尝试解析正数(parts(0), w) OrElse Not 尝试解析正数(parts(1), h) Then Return False
            ratio = CSng(w / h)
            Return True
        End If

        If parts.Length = 1 Then
            Dim directRatio As Double
            If Not 尝试解析正数(parts(0), directRatio) Then Return False
            ratio = CSng(directRatio)
            Return True
        End If

        Return False
    End Function

    Private Function 尝试解析正数(text As String, ByRef value As Double) As Boolean
        If Not Double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, value) AndAlso
           Not Double.TryParse(text, NumberStyles.Float, CultureInfo.CurrentCulture, value) Then
            Return False
        End If
        Return value > 0 AndAlso Not Double.IsNaN(value) AndAlso Not Double.IsInfinity(value)
    End Function

End Class
