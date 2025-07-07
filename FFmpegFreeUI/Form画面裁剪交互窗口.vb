
Imports System.IO
Public Class Form画面裁剪交互窗口

    Private Sub Form画面裁剪交互窗口_Load(sender As Object, e As EventArgs) Handles Me.Load
        UiComboBox比例.ItemHeight = 30 * Form1.DPI
        UiCheckBox居中.CheckBoxSize = 20 * Form1.DPI
        PictureBox2.Width = PictureBox2.Height
        PictureBox3.Width = PictureBox3.Height
    End Sub
    Private Sub Form画面裁剪交互窗口_DpiChanged(sender As Object, e As DpiChangedEventArgs) Handles Me.DpiChanged
        UiComboBox比例.ItemHeight = 30 * Form1.DPI
        UiCheckBox居中.CheckBoxSize = 20 * Form1.DPI
        PictureBox2.Width = PictureBox2.Height
        PictureBox3.Width = PictureBox3.Height
    End Sub


    Public FFmpegProcess As Process

    Private Sub UiButton1_Click(sender As Object, e As EventArgs) Handles UiButton1.Click
        Dim 视频文件 As String
        Dim openFileDialog As New OpenFileDialog With {.Multiselect = False, .Filter = "所有文件|*.*"}
        If openFileDialog.ShowDialog() = DialogResult.OK Then
            视频文件 = openFileDialog.FileName
        Else
            Exit Sub
        End If
        Dim FFmpegProcess As New Process
        FFmpegProcess = New Process()
        FFmpegProcess.StartInfo.FileName = "ffmpeg"
        FFmpegProcess.StartInfo.WorkingDirectory = If(Form1.FFmpeg自定义工作目录 <> "", Form1.FFmpeg自定义工作目录, "")
        FFmpegProcess.StartInfo.Arguments = $"-ss {If(UiTextBox1.Text <> "", UiTextBox1.Text, "0:0:10")} -i ""{视频文件}"" -frames:v 1 -q:v 1 ""{Path.Combine(Application.StartupPath, "ScreenCropPreview.png")}"" -y"
        FFmpegProcess.Start()
        FFmpegProcess.WaitForExit()
        If FFmpegProcess.ExitCode <> 0 Then Exit Sub
        If Not FileIO.FileSystem.FileExists(Path.Combine(Application.StartupPath, "ScreenCropPreview.png")) Then Exit Sub
        Me.PictureBox1.Image = LoadImageFromFile(Path.Combine(Application.StartupPath, "ScreenCropPreview.png"))
        My.Computer.FileSystem.DeleteFile(Path.Combine(Application.StartupPath, "ScreenCropPreview.png"))
        Panel2.Visible = True
    End Sub

    Private Sub UiButton2_Click(sender As Object, e As EventArgs) Handles UiButton2.Click
        If UiTextBox画面裁剪滤镜参数.Text <> "" Then
            Form1.常规流程参数页面.UiTextBox画面裁剪滤镜参数.Text = UiTextBox画面裁剪滤镜参数.Text
            GC.Collect()
            Me.Dispose()
        End If
    End Sub

    Private Sub Form画面裁剪交互窗口_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        GC.Collect()
    End Sub

    Private cropRect As New Rectangle(0, 0, 200, 100)
    Private isDraggingLeftTop As Boolean = False
    Private isDraggingRightBottom As Boolean = False
    Private fixedAspectRatio As Double = 0
    Private magnifierZoom As Integer = 10
    Private magnifierSize As Integer = 5

    Protected Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or &H2000000 ' WS_EX_COMPOSITED
            Return cp
        End Get
    End Property

    Private Sub PictureBox1_Paint(sender As Object, e As PaintEventArgs) Handles PictureBox1.Paint
        If cropRect.Width > 0 AndAlso cropRect.Height > 0 Then
            e.Graphics.DrawRectangle(Pens.Red, cropRect)
        End If
    End Sub

    Private Sub PictureBox1_MouseDown(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseDown
        If e.Button = MouseButtons.Left Then
            isDraggingLeftTop = True
        ElseIf e.Button = MouseButtons.Right Then
            isDraggingRightBottom = True
        End If
    End Sub

    Private Sub PictureBox1_MouseMove(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseMove
        If PictureBox1.Image Is Nothing Then Return

        Dim imgW = PictureBox1.Width
        Dim imgH = PictureBox1.Height

        ' 检查是否需要交换拖动模式
        If isDraggingLeftTop Then
            If e.X > cropRect.Right OrElse e.Y > cropRect.Bottom Then
                ' 交换拖动模式
                isDraggingLeftTop = False
                isDraggingRightBottom = True
                ' 交换矩形的角落
                Dim tmp = New Rectangle(cropRect.Left, cropRect.Top, cropRect.Width, cropRect.Height)
                cropRect = New Rectangle(tmp.Left, tmp.Top, Math.Max(1, e.X - tmp.Left), Math.Max(1, e.Y - tmp.Top))
            Else
                ' 正常左上角拖动
                Dim newX = Math.Max(0, Math.Min(e.X, cropRect.Right - 1))
                Dim newY = Math.Max(0, Math.Min(e.Y, cropRect.Bottom - 1))
                Dim newWidth = cropRect.Right - newX
                Dim newHeight = cropRect.Bottom - newY
                If fixedAspectRatio > 0 Then
                    newHeight = CInt(newWidth / fixedAspectRatio)
                End If
                If newX + newWidth > imgW Then newWidth = imgW - newX
                If newY + newHeight > imgH Then newHeight = imgH - newY

                cropRect = New Rectangle(newX, newY, newWidth, newHeight)
            End If
        ElseIf isDraggingRightBottom Then
            If e.X < cropRect.Left OrElse e.Y < cropRect.Top Then
                ' 交换拖动模式
                isDraggingRightBottom = False
                isDraggingLeftTop = True
                ' 交换矩形的角落
                Dim tmp = New Rectangle(cropRect.Left, cropRect.Top, cropRect.Width, cropRect.Height)
                cropRect = New Rectangle(e.X, e.Y, tmp.Right - e.X, tmp.Bottom - e.Y)
            Else
                ' 正常右下角拖动
                Dim maxX = imgW - 1
                Dim maxY = imgH - 1
                Dim newRight = Math.Max(cropRect.Left + 1, Math.Min(e.X, maxX))
                Dim newBottom = Math.Max(cropRect.Top + 1, Math.Min(e.Y, maxY))
                Dim newWidth = newRight - cropRect.Left
                Dim newHeight = newBottom - cropRect.Top
                If fixedAspectRatio > 0 Then
                    newHeight = CInt(newWidth / fixedAspectRatio)
                    If cropRect.Top + newHeight > imgH Then
                        newHeight = imgH - cropRect.Top
                        newWidth = CInt(newHeight * fixedAspectRatio)
                    End If
                End If

                cropRect = New Rectangle(cropRect.Left, cropRect.Top, newWidth, newHeight)
            End If
        End If

        ' 在任何情况下都应用居中模式并更新文本框
        ApplyAlignMode()
        UpdateCropTextBox()
        PictureBox1.Invalidate()
        UpdateMagnifiers()
    End Sub
    Private Sub PictureBox1_MouseUp(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseUp
        isDraggingLeftTop = False
        isDraggingRightBottom = False
    End Sub

    Private Sub ApplyAlignMode()
        If UiCheckBox居中.Checked AndAlso PictureBox1.Image IsNot Nothing Then
            Dim imgW = PictureBox1.Image.Width
            Dim imgH = PictureBox1.Image.Height
            cropRect.X = (imgW - cropRect.Width) \ 2
            cropRect.Y = (imgH - cropRect.Height) \ 2
        End If
    End Sub

    Private Sub UpdateCropTextBox()
        UiTextBox画面裁剪滤镜参数.Text = $"{cropRect.Width}:{cropRect.Height}:{cropRect.X}:{cropRect.Y}"
    End Sub

    Private Sub UiTextBox画面裁剪滤镜参数_KeyDown(sender As Object, e As KeyEventArgs) Handles UiTextBox画面裁剪滤镜参数.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim txt = UiTextBox画面裁剪滤镜参数.Text.Trim()
            Dim parts = txt.Split(":"c)
            If parts.Length = 4 Then
                Dim w, h, x, y As Integer
                If Integer.TryParse(parts(0), w) AndAlso Integer.TryParse(parts(1), h) AndAlso Integer.TryParse(parts(2), x) AndAlso Integer.TryParse(parts(3), y) Then
                    cropRect = New Rectangle(x, y, w, h)
                    ApplyAlignMode()
                    PictureBox1.Invalidate()
                End If
            End If
        End If
    End Sub

    Private Sub UiComboBox比例_SelectedIndexChanged(sender As Object, e As EventArgs) Handles UiComboBox比例.SelectedIndexChanged
        Select Case UiComboBox比例.SelectedIndex
            Case 0 : fixedAspectRatio = 0
            Case 1 : fixedAspectRatio = 21 / 9
            Case 2 : fixedAspectRatio = 16 / 9
            Case 3 : fixedAspectRatio = 3 / 2
            Case 4 : fixedAspectRatio = 2 / 1
            Case 5 : fixedAspectRatio = 4 / 3
            Case 6 : fixedAspectRatio = 1
        End Select
    End Sub

    Sub UpdateMagnifiers()
        If PictureBox1.Image Is Nothing Then Return

        Try
            ' 更新左上角放大镜
            Using bmp1 As New Bitmap(Math.Max(1, PictureBox2.Width), Math.Max(1, PictureBox2.Height))
                Using g As Graphics = Graphics.FromImage(bmp1)
                    g.Clear(Color.Black)
                    g.InterpolationMode = Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = Drawing2D.PixelOffsetMode.Half

                    Dim imgW = PictureBox1.Image.Width
                    Dim imgH = PictureBox1.Image.Height
                    Dim centerX As Integer = Math.Max(0, Math.Min(cropRect.Left, imgW - 1))
                    Dim centerY As Integer = Math.Max(0, Math.Min(cropRect.Top, imgH - 1))

                    Dim sourceX As Integer = Math.Max(0, centerX - magnifierSize)
                    Dim sourceY As Integer = Math.Max(0, centerY - magnifierSize)
                    Dim sourceWidth As Integer = Math.Min(magnifierSize * 2, imgW - sourceX)
                    Dim sourceHeight As Integer = Math.Min(magnifierSize * 2, imgH - sourceY)

                    If sourceWidth <= 0 OrElse sourceHeight <= 0 Then
                        g.DrawString("边界外", New Font("Arial", 10), Brushes.White, 5, 5)
                    Else
                        Dim sourceRect As New Rectangle(sourceX, sourceY, sourceWidth, sourceHeight)
                        g.DrawImage(PictureBox1.Image,
                        New Rectangle(0, 0, bmp1.Width, bmp1.Height),
                        sourceRect,
                        GraphicsUnit.Pixel)

                        Dim crossX As Integer = ((centerX - sourceX) * bmp1.Width) \ sourceWidth
                        Dim crossY As Integer = ((centerY - sourceY) * bmp1.Height) \ sourceHeight

                        If crossX >= 0 AndAlso crossX < bmp1.Width AndAlso crossY >= 0 AndAlso crossY < bmp1.Height Then
                            g.DrawLine(Pens.Red, crossX, 0, crossX, bmp1.Height)
                            g.DrawLine(Pens.Red, 0, crossY, bmp1.Width, crossY)
                        End If

                        'Dim textSize As SizeF = g.MeasureString(Text, Me.Font)
                        'g.FillRectangle(New SolidBrush(Color.FromArgb(128, 0, 0, 0)), 0, 0, textSize.Width, textSize.Height)
                        'g.DrawString($"({centerX},{centerY})", Me.Font, Brushes.YellowGreen, 0, 0)
                    End If
                End Using
                PictureBox2.Image = bmp1.Clone()
            End Using

            ' 更新右下角放大镜
            Using bmp2 As New Bitmap(Math.Max(1, PictureBox3.Width), Math.Max(1, PictureBox3.Height))
                Using g As Graphics = Graphics.FromImage(bmp2)
                    g.Clear(Color.Black)
                    g.InterpolationMode = Drawing2D.InterpolationMode.NearestNeighbor
                    g.PixelOffsetMode = Drawing2D.PixelOffsetMode.Half

                    Dim imgW = PictureBox1.Image.Width
                    Dim imgH = PictureBox1.Image.Height
                    Dim centerX As Integer = Math.Max(0, Math.Min(cropRect.Right, imgW - 1))
                    Dim centerY As Integer = Math.Max(0, Math.Min(cropRect.Bottom, imgH - 1))

                    Dim sourceX As Integer = Math.Max(0, centerX - magnifierSize)
                    Dim sourceY As Integer = Math.Max(0, centerY - magnifierSize)
                    Dim sourceWidth As Integer = Math.Min(magnifierSize * 2, imgW - sourceX)
                    Dim sourceHeight As Integer = Math.Min(magnifierSize * 2, imgH - sourceY)

                    If sourceWidth <= 0 OrElse sourceHeight <= 0 Then
                        g.DrawString("边界外", New Font("Arial", 10), Brushes.White, 5, 5)
                    Else
                        Dim sourceRect As New Rectangle(sourceX, sourceY, sourceWidth, sourceHeight)
                        g.DrawImage(PictureBox1.Image,
                        New Rectangle(0, 0, bmp2.Width, bmp2.Height),
                        sourceRect,
                        GraphicsUnit.Pixel)

                        Dim crossX As Integer = ((centerX - sourceX) * bmp2.Width) \ sourceWidth
                        Dim crossY As Integer = ((centerY - sourceY) * bmp2.Height) \ sourceHeight

                        If crossX >= 0 AndAlso crossX < bmp2.Width AndAlso crossY >= 0 AndAlso crossY < bmp2.Height Then
                            g.DrawLine(Pens.Red, crossX, 0, crossX, bmp2.Height)
                            g.DrawLine(Pens.Red, 0, crossY, bmp2.Width, crossY)
                        End If

                        'Dim textSize As SizeF = g.MeasureString(Text, Me.Font)
                        'g.FillRectangle(New SolidBrush(Color.FromArgb(128, 0, 0, 0)), 0, 0, textSize.Width, textSize.Height)
                        'g.DrawString($"({centerX},{centerY})", Me.Font, Brushes.YellowGreen, 0, 0)
                    End If
                End Using
                PictureBox3.Image = bmp2.Clone()
            End Using
        Catch ex As Exception
            Debug.WriteLine("放大镜更新错误: " & ex.ToString())
        End Try
    End Sub





End Class