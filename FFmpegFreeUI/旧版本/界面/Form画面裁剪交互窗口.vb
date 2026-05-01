Imports System.IO
Public Class Form画面裁剪交互窗口

    ' 私有字段
    Private cropRect As New Rectangle(0, 0, 200, 100)
    Private isDraggingLeftTop As Boolean = False
    Private isDraggingRightBottom As Boolean = False
    Private fixedAspectRatio As Double = 0
    Private ReadOnly magnifierSize As Integer = 5

    ' 缓存的图像尺寸，避免重复访问
    Private cachedImageWidth As Integer = 0
    Private cachedImageHeight As Integer = 0

    Public FFmpegProcess As Process

    Protected Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or &H2000000 ' WS_EX_COMPOSITED
            Return cp
        End Get
    End Property

#Region "窗体事件"

    Private Sub Form画面裁剪交互窗口_Load(sender As Object, e As EventArgs) Handles Me.Load
        InitializeDpiSettings()
    End Sub

    Private Sub Form画面裁剪交互窗口_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        SetControlFont(用户设置.实例对象.字体, Me)
    End Sub

    Private Sub Form画面裁剪交互窗口_DpiChanged(sender As Object, e As DpiChangedEventArgs) Handles Me.DpiChanged
        InitializeDpiSettings()
    End Sub

    Private Sub Form画面裁剪交互窗口_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        ' 释放图像资源
        If PictureBox1.Image IsNot Nothing Then
            PictureBox1.Image.Dispose()
            PictureBox1.Image = Nothing
        End If
        If PictureBox2.Image IsNot Nothing Then
            PictureBox2.Image.Dispose()
            PictureBox2.Image = Nothing
        End If
        If PictureBox3.Image IsNot Nothing Then
            PictureBox3.Image.Dispose()
            PictureBox3.Image = Nothing
        End If
        GC.Collect()
    End Sub

#End Region

#Region "按钮事件"

    Private Sub UiButton1_Click(sender As Object, e As EventArgs) Handles UiButton1.Click
        打开视频获取画面(ShowFileOpenDialog())
    End Sub

    Private Sub UiButton2_Click(sender As Object, e As EventArgs) Handles UiButton2.Click
        If Not String.IsNullOrEmpty(UiTextBox画面裁剪滤镜参数.Text) Then
            Form1.常规流程参数页面.UiTextBox画面裁剪滤镜参数.Text = UiTextBox画面裁剪滤镜参数.Text
            Me.Close()
        End If
    End Sub

#End Region

#Region "鼠标交互事件"

    Private Sub PictureBox1_MouseDown(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseDown
        If e.Button = MouseButtons.Left Then
            isDraggingLeftTop = True
        ElseIf e.Button = MouseButtons.Right Then
            isDraggingRightBottom = True
        End If
    End Sub

    Private Sub PictureBox1_MouseMove(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseMove
        If Not (isDraggingLeftTop OrElse isDraggingRightBottom) Then Return
        If cachedImageWidth = 0 OrElse cachedImageHeight = 0 Then Return

        If isDraggingLeftTop Then
            HandleLeftTopDrag(e.X, e.Y)
        ElseIf isDraggingRightBottom Then
            HandleRightBottomDrag(e.X, e.Y)
        End If

        ' 应用居中模式并更新界面
        ApplyAlignMode()
        UpdateCropTextBox()
        PictureBox1.Invalidate()
        UpdateMagnifiers()
    End Sub
    Private Sub PictureBox1_MouseUp(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseUp
        isDraggingLeftTop = False
        isDraggingRightBottom = False
    End Sub

    Private Sub PictureBox1_Paint(sender As Object, e As PaintEventArgs) Handles PictureBox1.Paint
        If cropRect.Width <= 0 OrElse cropRect.Height <= 0 OrElse cachedImageWidth = 0 Then Return

        ' 绘制时向内收缩，确保边框不被父容器遮挡
        Dim drawRect As Rectangle = cropRect
        If cropRect.Right >= cachedImageWidth Then
            drawRect.Width -= 1
        End If
        If cropRect.Bottom >= cachedImageHeight Then
            drawRect.Height -= 1
        End If

        e.Graphics.DrawRectangle(Pens.Red, drawRect)
    End Sub

#End Region

#Region "文本框和下拉框事件"

    Private Sub UiTextBox画面裁剪滤镜参数_KeyDown(sender As Object, e As KeyEventArgs) Handles UiTextBox画面裁剪滤镜参数.KeyDown
        If e.KeyCode <> Keys.Enter Then Return

        If ParseCropParameters(UiTextBox画面裁剪滤镜参数.Text.Trim()) Then
            ApplyAlignMode()
            PictureBox1.Invalidate()
            UpdateMagnifiers()
        End If
    End Sub

    Private Sub UiComboBox比例_SelectedIndexChanged(sender As Object, e As EventArgs) Handles UiComboBox比例.SelectedIndexChanged
        fixedAspectRatio = GetAspectRatioFromIndex(UiComboBox比例.SelectedIndex)
    End Sub

#End Region

#Region "辅助方法"

    ''' <summary>
    ''' 初始化DPI相关设置
    ''' </summary>
    Private Sub InitializeDpiSettings()
        Dim dpi As Single = Form1.DPI
        UiComboBox比例.ItemHeight = CInt(30 * dpi)
        UiCheckBox居中.CheckBoxSize = CInt(20 * dpi)
        PictureBox2.Width = PictureBox2.Height
        PictureBox3.Width = PictureBox3.Height
    End Sub

    ''' <summary>
    ''' 显示文件选择对话框
    ''' </summary>
    Private Function ShowFileOpenDialog() As String
        Using openFileDialog As New OpenFileDialog With {.Multiselect = False, .Filter = "所有文件|*.*"}
            If openFileDialog.ShowDialog() = DialogResult.OK Then
                Return openFileDialog.FileName
            End If
        End Using
        Return String.Empty
    End Function

    ''' <summary>
    ''' 从视频提取帧
    ''' </summary>
    Private Function ExtractFrameFromVideo(videoPath As String) As Boolean
        Try
            Using process As New Process()
                process.StartInfo.FileName = "ffmpeg"
                process.StartInfo.WorkingDirectory = If(用户设置.实例对象.工作目录 <> "", 用户设置.实例对象.工作目录, "")
                Dim timestamp As String = If(Not String.IsNullOrEmpty(UiTextBox1.Text), UiTextBox1.Text, "0:0:10")
                Dim outputPath As String = Path.Combine(Application.StartupPath, "ScreenCropPreview.png")
                process.StartInfo.Arguments = $"-ss {timestamp} -i ""{videoPath}"" -frames:v 1 -q:v 1 ""{outputPath}"" -y"
                process.Start()
                process.WaitForExit()
                Return process.ExitCode = 0
            End Using
        Catch ex As Exception
            Debug.WriteLine($"提取视频帧错误: {ex.Message}")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' 初始化裁剪矩形为图像的合理默认值
    ''' </summary>
    Private Sub InitializeCropRect()
        Dim defaultWidth As Integer = Math.Min(cachedImageWidth, 800)
        Dim defaultHeight As Integer = Math.Min(cachedImageHeight, 450)
        cropRect = New Rectangle(
            (cachedImageWidth - defaultWidth) \ 2,
            (cachedImageHeight - defaultHeight) \ 2,
            defaultWidth,
            defaultHeight
        )
        UpdateCropTextBox()
    End Sub

    ''' <summary>
    ''' 处理左上角拖动
    ''' </summary>
    Private Sub HandleLeftTopDrag(mouseX As Integer, mouseY As Integer)
        If mouseX > cropRect.Right OrElse mouseY > cropRect.Bottom Then
            ' 交换拖动模式
            isDraggingLeftTop = False
            isDraggingRightBottom = True
            Dim oldRect As Rectangle = cropRect
            cropRect = New Rectangle(oldRect.Left, oldRect.Top,
                                    Math.Max(1, mouseX - oldRect.Left),
                                    Math.Max(1, mouseY - oldRect.Top))
        Else
            ' 正常左上角拖动
            Dim newX As Integer = Math.Max(0, Math.Min(mouseX, cropRect.Right - 1))
            Dim newY As Integer = Math.Max(0, Math.Min(mouseY, cropRect.Bottom - 1))
            Dim newWidth As Integer = cropRect.Right - newX
            Dim newHeight As Integer = cropRect.Bottom - newY

            If fixedAspectRatio > 0 Then
                newHeight = CInt(newWidth / fixedAspectRatio)
            End If

            ' 边界检查
            newWidth = Math.Min(newWidth, cachedImageWidth - newX)
            newHeight = Math.Min(newHeight, cachedImageHeight - newY)

            cropRect = New Rectangle(newX, newY, newWidth, newHeight)
        End If
    End Sub

    ''' <summary>
    ''' 处理右下角拖动
    ''' </summary>
    Private Sub HandleRightBottomDrag(mouseX As Integer, mouseY As Integer)
        If mouseX < cropRect.Left OrElse mouseY < cropRect.Top Then
            ' 交换拖动模式
            isDraggingRightBottom = False
            isDraggingLeftTop = True
            Dim oldRect As Rectangle = cropRect
            cropRect = New Rectangle(mouseX, mouseY, oldRect.Right - mouseX, oldRect.Bottom - mouseY)
        Else
            ' 正常右下角拖动
            Dim newRight As Integer = Math.Max(cropRect.Left + 1, Math.Min(mouseX, cachedImageWidth))
            Dim newBottom As Integer = Math.Max(cropRect.Top + 1, Math.Min(mouseY, cachedImageHeight))
            Dim newWidth As Integer = newRight - cropRect.Left
            Dim newHeight As Integer = newBottom - cropRect.Top

            If fixedAspectRatio > 0 Then
                newHeight = CInt(newWidth / fixedAspectRatio)
                If cropRect.Top + newHeight > cachedImageHeight Then
                    newHeight = cachedImageHeight - cropRect.Top
                    newWidth = CInt(newHeight * fixedAspectRatio)
                End If
            End If

            cropRect = New Rectangle(cropRect.Left, cropRect.Top, newWidth, newHeight)
        End If
    End Sub

    ''' <summary>
    ''' 应用居中对齐模式
    ''' </summary>
    Private Sub ApplyAlignMode()
        If UiCheckBox居中.Checked AndAlso cachedImageWidth > 0 AndAlso cachedImageHeight > 0 Then
            cropRect.X = (cachedImageWidth - cropRect.Width) \ 2
            cropRect.Y = (cachedImageHeight - cropRect.Height) \ 2
        End If
    End Sub

    ''' <summary>
    ''' 更新裁剪参数文本框
    ''' </summary>
    Private Sub UpdateCropTextBox()
        UiTextBox画面裁剪滤镜参数.Text = $"{cropRect.Width}:{cropRect.Height}:{cropRect.X}:{cropRect.Y}"
    End Sub

    ''' <summary>
    ''' 解析裁剪参数
    ''' </summary>
    Private Function ParseCropParameters(text As String) As Boolean
        Dim parts As String() = text.Split(":"c)
        If parts.Length <> 4 Then Return False

        Dim w, h, x, y As Integer
        If Integer.TryParse(parts(0), w) AndAlso
           Integer.TryParse(parts(1), h) AndAlso
           Integer.TryParse(parts(2), x) AndAlso
           Integer.TryParse(parts(3), y) AndAlso
           w > 0 AndAlso h > 0 Then
            cropRect = New Rectangle(x, y, w, h)
            Return True
        End If
        Return False
    End Function

    ''' <summary>
    ''' 根据索引获取宽高比
    ''' </summary>
    Private Function GetAspectRatioFromIndex(index As Integer) As Double
        Select Case index
            Case 0 : Return 0           ' 自由
            Case 1 : Return 21.0 / 9.0  ' 21:9
            Case 2 : Return 16.0 / 9.0  ' 16:9
            Case 3 : Return 3.0 / 2.0   ' 3:2
            Case 4 : Return 2.0 / 1.0   ' 2:1
            Case 5 : Return 4.0 / 3.0   ' 4:3
            Case 6 : Return 1.0         ' 1:1
            Case Else : Return 0
        End Select
    End Function

#End Region

#Region "放大镜更新"

    ''' <summary>
    ''' 更新放大镜显示
    ''' </summary>
    Private Sub UpdateMagnifiers()
        If PictureBox1.Image Is Nothing OrElse cachedImageWidth = 0 Then Return

        Try
            ' 左上角放大镜：显示裁剪矩形的左上角点（cropRect 的起始像素）
            UpdateSingleMagnifier(PictureBox2, cropRect.Left, cropRect.Top)

            ' 右下角放大镜：显示裁剪矩形的右下角点
            ' Rectangle 的 Right/Bottom 是边界外的第一个坐标
            ' 例如：Rectangle(0, 0, 1920, 1080) 的 Right=1920, Bottom=1080
            ' 但实际包含的像素是 [0, 1919] 和 [0, 1079]
            ' 所以右下角的实际像素坐标是 (Right-1, Bottom-1)
            Dim rightPixel As Integer = cropRect.Right - 1
            Dim bottomPixel As Integer = cropRect.Bottom - 1
            UpdateSingleMagnifier(PictureBox3, rightPixel, bottomPixel)
        Catch ex As Exception
            Debug.WriteLine($"放大镜更新错误: {ex.Message}")
        End Try
    End Sub

    ''' <summary>
    ''' 更新单个放大镜
    ''' </summary>
    Private Sub UpdateSingleMagnifier(pictureBox As PictureBox, centerX As Integer, centerY As Integer)
        Dim magnifierWidth As Integer = Math.Max(1, pictureBox.Width)
        Dim magnifierHeight As Integer = Math.Max(1, pictureBox.Height)

        Using bmp As New Bitmap(magnifierWidth, magnifierHeight)
            Using g As Graphics = Graphics.FromImage(bmp)
                g.Clear(Color.Black)
                g.InterpolationMode = Drawing2D.InterpolationMode.NearestNeighbor
                g.PixelOffsetMode = Drawing2D.PixelOffsetMode.Half

                ' 限制中心点在图像范围内
                centerX = Math.Max(0, Math.Min(centerX, cachedImageWidth - 1))
                centerY = Math.Max(0, Math.Min(centerY, cachedImageHeight - 1))

                ' 计算源矩形：以 centerX, centerY 为中心，取周围 magnifierSize*2 的区域
                Dim sourceX As Integer = Math.Max(0, centerX - magnifierSize)
                Dim sourceY As Integer = Math.Max(0, centerY - magnifierSize)
                Dim sourceWidth As Integer = Math.Min(magnifierSize * 2, cachedImageWidth - sourceX)
                Dim sourceHeight As Integer = Math.Min(magnifierSize * 2, cachedImageHeight - sourceY)

                If sourceWidth <= 0 OrElse sourceHeight <= 0 Then
                    g.DrawString("边界外", New Font("Arial", 10), Brushes.White, 5, 5)
                Else
                    ' 绘制放大的图像区域
                    Dim sourceRect As New Rectangle(sourceX, sourceY, sourceWidth, sourceHeight)
                    g.DrawImage(PictureBox1.Image,
                               New Rectangle(0, 0, magnifierWidth, magnifierHeight),
                               sourceRect,
                               GraphicsUnit.Pixel)

                    ' 计算十字准线的位置
                    ' 判断目标像素是否在源区域的边界上
                    Dim isAtLeftEdge As Boolean = (centerX = sourceX)
                    Dim isAtRightEdge As Boolean = (centerX = sourceX + sourceWidth - 1)
                    Dim isAtTopEdge As Boolean = (centerY = sourceY)
                    Dim isAtBottomEdge As Boolean = (centerY = sourceY + sourceHeight - 1)

                    Dim crossX As Integer
                    Dim crossY As Integer

                    ' X 坐标计算
                    If isAtLeftEdge Then
                        crossX = 1 ' 左边界，向内偏移
                    ElseIf isAtRightEdge Then
                        crossX = magnifierWidth - 2 ' 右边界，向内偏移
                    Else
                        ' 中间位置，精确映射
                        Dim offsetX As Double = CDbl(centerX - sourceX) / sourceWidth
                        crossX = CInt(offsetX * magnifierWidth)
                    End If

                    ' Y 坐标计算
                    If isAtTopEdge Then
                        crossY = 1 ' 上边界，向内偏移
                    ElseIf isAtBottomEdge Then
                        crossY = magnifierHeight - 2 ' 下边界，向内偏移
                    Else
                        ' 中间位置，精确映射
                        Dim offsetY As Double = CDbl(centerY - sourceY) / sourceHeight
                        crossY = CInt(offsetY * magnifierHeight)
                    End If

                    ' 绘制十字准线
                    g.DrawLine(Pens.Red, crossX, 0, crossX, magnifierHeight - 1)
                    g.DrawLine(Pens.Red, 0, crossY, magnifierWidth - 1, crossY)
                End If
            End Using

            ' 释放旧图像并设置新图像
            pictureBox.Image?.Dispose()
            pictureBox.Image = bmp.Clone()
        End Using
    End Sub

#End Region


    Private Sub Panel1_DragEnter(sender As Object, e As DragEventArgs) Handles Panel1.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        End If
    End Sub

    Private Sub Panel1_DragDrop(sender As Object, e As DragEventArgs) Handles Panel1.DragDrop
        打开视频获取画面(e.Data.GetData(DataFormats.FileDrop)(0))
    End Sub

    Private Sub Panel2_DragEnter(sender As Object, e As DragEventArgs) Handles Panel2.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        End If
    End Sub

    Private Sub Panel2_DragDrop(sender As Object, e As DragEventArgs) Handles Panel2.DragDrop
        打开视频获取画面(e.Data.GetData(DataFormats.FileDrop)(0))
    End Sub

    Private Sub PictureBox1_DragEnter(sender As Object, e As DragEventArgs) Handles PictureBox1.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        End If
    End Sub

    Private Sub PictureBox1_DragDrop(sender As Object, e As DragEventArgs) Handles PictureBox1.DragDrop
        打开视频获取画面(e.Data.GetData(DataFormats.FileDrop)(0))
    End Sub

    Sub 打开视频获取画面(视频文件 As String)
        If String.IsNullOrEmpty(视频文件) Then Exit Sub
        If Not ExtractFrameFromVideo(视频文件) Then Exit Sub
        Dim previewPath As String = Path.Combine(Application.StartupPath, "ScreenCropPreview.png")
        If Not File.Exists(previewPath) Then Exit Sub
        PictureBox1.Image?.Dispose()
        PictureBox1.Image = LoadImageFromFile(previewPath)
        If PictureBox1.Image IsNot Nothing Then
            cachedImageWidth = PictureBox1.Image.Width
            cachedImageHeight = PictureBox1.Image.Height
        End If
        File.Delete(previewPath)
        Panel2.Visible = True
        UpdateMagnifiers()
    End Sub




End Class