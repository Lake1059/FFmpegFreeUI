Imports System.Globalization
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Threading
Imports System.Threading.Tasks
Imports LakeUI

Public Class Form_v6_参数面板_画面区域选择窗口

    Public 目标控件 As Control = Nothing
    Private Shared ReadOnly 预览色彩编码 As New PixelPictureColorEncoding(
        PixelPictureColorSpace.SRgb, PixelPictureTransferFunction.SRgb, isHdr:=False)
    Private 正在同步框选文本 As Boolean = False
    Private 预览图像 As Image
    Private 预览图像源 As PixelPictureTiledSource

    Private Sub 窗口加载(事件来源 As Object, 事件参数 As EventArgs) Handles MyBase.Load
        Me.Icon = FormMain_v6.Icon
        SetControlFont(设置_v6.实例对象.字体, Me, , True)
        绑定文件拖放(Me)
        If FormMain_v6.ThisIsYourWindow1.AttachedForms.Count > 0 Then
            FormMain_v6.ThisIsYourWindow1.Attach(Me)
        End If
    End Sub

    Private Sub 窗口显示(事件来源 As Object, 事件参数 As EventArgs) Handles Me.VisibleChanged
        If Not Me.Visible Then Exit Sub
        设置裁剪参数文本(If(目标控件?.Text, ""))
    End Sub

    Private Sub 打开媒体(事件来源 As Object, 事件参数 As EventArgs) Handles MB_打开.Click
        打开媒体获取画面(选择媒体文件())
    End Sub

    Private Sub 完成裁剪(事件来源 As Object, 事件参数 As EventArgs) Handles MB_完成.Click
        If 目标控件 IsNot Nothing Then 目标控件.Text = MTB_裁剪参数.Text
        关闭窗体流程()
    End Sub

    Private Function 选择媒体文件() As String
        Using 文件对话框 As New OpenFileDialog With {
            .Multiselect = False,
            .Filter = "媒体文件|*.mp4;*.mkv;*.mov;*.m4v;*.avi;*.wmv;*.webm;*.flv;*.ts;*.m2ts;*.mts;*.mpg;*.mpeg;*.3gp;*.bmp;*.dib;*.gif;*.ico;*.jfif;*.jpe;*.jpeg;*.jpg;*.jxl;*.png;*.tif;*.tiff;*.webp|图片文件|*.bmp;*.dib;*.gif;*.ico;*.jfif;*.jpe;*.jpeg;*.jpg;*.jxl;*.png;*.tif;*.tiff;*.webp|视频文件|*.mp4;*.mkv;*.mov;*.m4v;*.avi;*.wmv;*.webm;*.flv;*.ts;*.m2ts;*.mts;*.mpg;*.mpeg;*.3gp|所有文件|*.*"
        }
            If 文件对话框.ShowDialog() = DialogResult.OK Then
                Return 文件对话框.FileName
            End If
        End Using
        Return String.Empty
    End Function

    Private Function 提取媒体画面(媒体路径 As String, 输出路径 As String, 时间戳 As String) As Boolean
        Try
            Using 截图进程 As New Process()
                截图进程.StartInfo.FileName = 设置_v6.获取FFmpeg进程文件名()
                截图进程.StartInfo.WorkingDirectory = 设置_v6.获取有效工作目录()
                截图进程.StartInfo.UseShellExecute = False
                截图进程.StartInfo.CreateNoWindow = True
                If 时间戳 IsNot Nothing Then
                    截图进程.StartInfo.ArgumentList.Add("-ss")
                    截图进程.StartInfo.ArgumentList.Add(时间戳)
                End If
                截图进程.StartInfo.ArgumentList.Add("-i")
                截图进程.StartInfo.ArgumentList.Add(媒体路径)
                截图进程.StartInfo.ArgumentList.Add("-frames:v")
                截图进程.StartInfo.ArgumentList.Add("1")
                截图进程.StartInfo.ArgumentList.Add("-q:v")
                截图进程.StartInfo.ArgumentList.Add("1")
                截图进程.StartInfo.ArgumentList.Add(输出路径)
                截图进程.StartInfo.ArgumentList.Add("-y")
                截图进程.Start()
                截图进程.WaitForExit()
                Return 截图进程.ExitCode = 0 AndAlso File.Exists(输出路径)
            End Using
        Catch 异常 As Exception
            Debug.WriteLine($"提取媒体帧错误: {异常.Message}")
            Return False
        End Try
    End Function

    Private Shared Function 解析预览时间戳(文本 As String) As String
        ' 按当前文本匹配，避免手写时间戳仍沿用之前选中的预设。
        Dim 时间戳 = If(文本, "").Trim()
        Select Case 时间戳
            Case "", "（默认第 10 秒）"
                Return "10"
            Case "第一帧"
                Return "0"
            Case "1 分钟"
                Return "60"
            Case "2 分钟"
                Return "120"
            Case "3 分钟"
                Return "180"
            Case "5 分钟"
                Return "300"
            Case "10 分钟"
                Return "600"
            Case Else
                ' 保留 FFmpeg 支持的秒数、时:分:秒及小数时间戳格式。
                Return 时间戳
        End Select
    End Function

    Private Sub 打开媒体获取画面(媒体文件 As String)
        If String.IsNullOrWhiteSpace(媒体文件) Then Exit Sub
        If Not File.Exists(媒体文件) Then
            ExFloatingTip(PPB_画面区域预览, "文件不存在", 1800)
            Exit Sub
        End If

        Dim 图像 As Image = Nothing
        If 尝试直接加载图像(媒体文件, 图像) Then
            设置预览图像(图像)
            Exit Sub
        End If

        Dim 预览文件路径 As String = Path.Combine(Path.GetTempPath(), $"FFmpegFreeUI_ScreenCropPreview_{Guid.NewGuid():N}.png")
        Try
            Dim 时间戳 = If(是图片扩展(媒体文件), Nothing, 解析预览时间戳(MCB_预览时间戳.Text))
            If Not 提取媒体画面(媒体文件, 预览文件路径, 时间戳) Then
                ExFloatingTip(PPB_画面区域预览, "无法读取媒体画面", 2200)
                Exit Sub
            End If

            Dim 预览图片 As Image = Nothing
            If 尝试直接加载图像(预览文件路径, 预览图片) Then
                设置预览图像(预览图片)
            Else
                ExFloatingTip(PPB_画面区域预览, "无法加载预览图像", 2200)
            End If
        Finally
            Try
                If File.Exists(预览文件路径) Then File.Delete(预览文件路径)
            Catch 异常 As Exception
                Debug.WriteLine($"删除预览图失败: {异常.Message}")
            End Try
        End Try
    End Sub

    Private Sub 窗口关闭(事件来源 As Object, 事件参数 As FormClosingEventArgs) Handles Me.FormClosing
        事件参数.Cancel = True
        关闭窗体流程()
    End Sub

    Private Sub 关闭窗体流程()
        目标控件?.FindForm?.Focus()
        目标控件 = Nothing
        清空预览图像()
        Me.Text = ""
        Me.Hide()
    End Sub

    Private Sub 设置预览图像(图像 As Image)
        清空预览图像()
        预览图像 = 图像
        预览图像源 = New PixelPictureTiledSource(图像.Width, 图像.Height,
            Function(图块请求, 取消令牌) 提供预览图块(图像, 图块请求, 取消令牌), encoding:=预览色彩编码)
        PPB_画面区域预览.Source = 预览图像源
        应用裁剪文本到框选(False)
    End Sub

    Private Shared Function 提供预览图块(图像 As Image, 图块请求 As PixelPictureTileRequest, 取消令牌 As CancellationToken) As Task(Of PixelPictureTile)
        SyncLock 图像
            取消令牌.ThrowIfCancellationRequested()
            Dim 宽度 = CInt(Math.Min(图块请求.TileSize, Math.Ceiling(图像.Width / 图块请求.Scale) - 图块请求.Column * 图块请求.TileSize))
            Dim 高度 = CInt(Math.Min(图块请求.TileSize, Math.Ceiling(图像.Height / 图块请求.Scale) - 图块请求.Row * 图块请求.TileSize))
            Using 图块位图 As New Bitmap(宽度, 高度, PixelFormat.Format32bppArgb)
                Using 图块画布 = Graphics.FromImage(图块位图)
                    图块画布.CompositingMode = Drawing2D.CompositingMode.SourceCopy
                    图块画布.InterpolationMode = If(图块请求.Level = 0, Drawing2D.InterpolationMode.NearestNeighbor, Drawing2D.InterpolationMode.HighQualityBilinear)
                    图块画布.PixelOffsetMode = Drawing2D.PixelOffsetMode.Half
                    Using 绘制属性 As New ImageAttributes()
                        绘制属性.SetWrapMode(Drawing2D.WrapMode.TileFlipXY)
                        Dim 图块区域 = 图块请求.SourceRegion
                        图块画布.DrawImage(图像, New Rectangle(0, 0, 宽度, 高度),
                            CSng(图块区域.X), CSng(图块区域.Y), CSng(宽度 * 图块请求.Scale), CSng(高度 * 图块请求.Scale), GraphicsUnit.Pixel, 绘制属性)
                    End Using
                End Using

                Dim 像素数据(宽度 * 高度 * 4 - 1) As Single
                Dim 位图数据 = 图块位图.LockBits(New Rectangle(0, 0, 宽度, 高度), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb)
                Try
                    Dim 行字节(宽度 * 4 - 1) As Byte
                    For 行号 = 0 To 高度 - 1
                        取消令牌.ThrowIfCancellationRequested()
                        Marshal.Copy(IntPtr.Add(位图数据.Scan0, 行号 * 位图数据.Stride), 行字节, 0, 行字节.Length)
                        For 列号 = 0 To 宽度 - 1
                            Dim 源偏移 = 列号 * 4
                            Dim 目标偏移 = (行号 * 宽度 + 列号) * 4
                            像素数据(目标偏移) = 行字节(源偏移 + 2) / 255.0F
                            像素数据(目标偏移 + 1) = 行字节(源偏移 + 1) / 255.0F
                            像素数据(目标偏移 + 2) = 行字节(源偏移) / 255.0F
                            像素数据(目标偏移 + 3) = 行字节(源偏移 + 3) / 255.0F
                        Next
                    Next
                Finally
                    图块位图.UnlockBits(位图数据)
                End Try
                Return Task.FromResult(New PixelPictureTile(宽度, 高度, 像素数据, 预览色彩编码))
            End Using
        End SyncLock
    End Function

    Private Sub 清空预览图像()
        正在同步框选文本 = True
        Try
            PPB_画面区域预览.Source = Nothing
            释放预览图像源()
            PPB_画面区域预览.ClearSelection()
        Finally
            正在同步框选文本 = False
        End Try
    End Sub

    Private Sub 释放预览图像源() Handles Me.Disposed
        预览图像源?.Dispose()
        预览图像源 = Nothing
        If 预览图像 IsNot Nothing Then
            SyncLock 预览图像
                预览图像.Dispose()
            End SyncLock
            预览图像 = Nothing
        End If
    End Sub

    Private Function 尝试直接加载图像(文件路径 As String, ByRef 图像 As Image) As Boolean
        图像 = Nothing
        Try
            Using 文件流 As New FileStream(文件路径, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
                Using 图像源 = Image.FromStream(文件流, False, False)
                    图像 = New Bitmap(图像源)
                End Using
            End Using
            Return True
        Catch
            图像 = Nothing
            Return False
        End Try
    End Function

    Private Function 是图片扩展(文件路径 As String) As Boolean
        Select Case Path.GetExtension(文件路径).ToLowerInvariant()
            Case ".apng", ".avif", ".bmp", ".dib", ".dpx", ".exr", ".gif", ".hdr", ".heic", ".heif",
                 ".ico", ".jfif", ".jpe", ".jpeg", ".jpg", ".jxl", ".pam", ".pbm", ".pcx", ".pfm",
                 ".pgm", ".png", ".ppm", ".psd", ".qoi", ".sgi", ".svg", ".tga", ".tif", ".tiff",
                 ".webp", ".xbm", ".xpm"
                Return True
            Case Else
                Return False
        End Select
    End Function

    Private Sub 绑定文件拖放(根控件 As Control)
        根控件.AllowDrop = True
        AddHandler 根控件.DragEnter, AddressOf 文件拖入事件
        AddHandler 根控件.DragDrop, AddressOf 文件放下事件

        For Each 子控件 As Control In 根控件.Controls
            绑定文件拖放(子控件)
        Next
    End Sub

    Private Sub 文件拖入事件(事件来源 As Object, 事件参数 As DragEventArgs)
        事件参数.Effect = If(获取拖入文件路径(事件参数) <> "", DragDropEffects.Copy, DragDropEffects.None)
    End Sub

    Private Sub 文件放下事件(事件来源 As Object, 事件参数 As DragEventArgs)
        Dim 文件路径 = 获取拖入文件路径(事件参数)
        If 文件路径 <> "" Then 打开媒体获取画面(文件路径)
    End Sub

    Private Function 获取拖入文件路径(事件参数 As DragEventArgs) As String
        If 事件参数 Is Nothing OrElse 事件参数.Data Is Nothing OrElse Not 事件参数.Data.GetDataPresent(DataFormats.FileDrop) Then Return ""
        Dim 文件列表 = TryCast(事件参数.Data.GetData(DataFormats.FileDrop), String())
        If 文件列表 Is Nothing Then Return ""
        For Each 文件路径 In 文件列表
            If File.Exists(文件路径) Then Return 文件路径
        Next
        Return ""
    End Function

    Private Sub 框选区域变化(事件来源 As Object, 事件参数 As EventArgs) Handles PPB_画面区域预览.SelectionChanged
        If 正在同步框选文本 Then Exit Sub
        同步框选到裁剪参数()
    End Sub

    Private Sub 设置裁剪参数文本(文本 As String)
        Dim 原同步状态 = 正在同步框选文本
        正在同步框选文本 = True
        Try
            MTB_裁剪参数.Text = 文本
        Finally
            正在同步框选文本 = 原同步状态
        End Try
    End Sub

    Private Sub 同步框选到裁剪参数()
        Dim 裁剪区域 = PPB_画面区域预览.SelectionRect
        设置裁剪参数文本(If(裁剪区域.Width > 0 AndAlso 裁剪区域.Height > 0,
                       $"{裁剪区域.Width}:{裁剪区域.Height}:{裁剪区域.X}:{裁剪区域.Y}", ""))
    End Sub

    Private Sub 裁剪参数按键(事件来源 As Object, 事件参数 As KeyEventArgs) Handles MTB_裁剪参数.KeyDown
        If 事件参数.KeyCode <> Keys.Enter Then Exit Sub
        应用裁剪文本到框选(True)
        事件参数.SuppressKeyPress = True
    End Sub

    Private Sub 裁剪参数结束编辑(事件来源 As Object, 事件参数 As EventArgs) Handles MTB_裁剪参数.Leave
        应用裁剪文本到框选(False)
    End Sub

    Private Function 应用裁剪文本到框选(显示提示 As Boolean) As Boolean
        If 正在同步框选文本 OrElse PPB_画面区域预览.Source Is Nothing Then Return False

        Dim 裁剪区域 As Rectangle
        If Not 尝试解析裁剪参数(MTB_裁剪参数.Text, 裁剪区域) Then
            If 显示提示 AndAlso MTB_裁剪参数.Text.Trim() <> "" Then
                ExFloatingTip(MTB_裁剪参数, "格式应为 宽:高:左上X:左上Y", 1800)
            End If
            Return False
        End If

        裁剪区域 = 约束裁剪区域到图像(裁剪区域)
        正在同步框选文本 = True
        Try
            PPB_画面区域预览.SelectionRect = 裁剪区域
            同步框选到裁剪参数()
        Finally
            正在同步框选文本 = False
        End Try
        Return True
    End Function

    Private Shared Function 尝试解析裁剪参数(文本 As String, ByRef 裁剪区域 As Rectangle) As Boolean
        裁剪区域 = Rectangle.Empty
        Dim 分段 = If(文本, "").Trim().Split(":"c)
        If 分段.Length <> 4 Then Return False

        Dim 宽度, 高度, 横坐标, 纵坐标 As Integer
        If Not Integer.TryParse(分段(0).Trim(), 宽度) Then Return False
        If Not Integer.TryParse(分段(1).Trim(), 高度) Then Return False
        If Not Integer.TryParse(分段(2).Trim(), 横坐标) Then Return False
        If Not Integer.TryParse(分段(3).Trim(), 纵坐标) Then Return False
        If 宽度 <= 0 OrElse 高度 <= 0 OrElse 横坐标 < 0 OrElse 纵坐标 < 0 Then Return False

        裁剪区域 = New Rectangle(横坐标, 纵坐标, 宽度, 高度)
        Return True
    End Function

    Private Function 约束裁剪区域到图像(裁剪区域 As Rectangle) As Rectangle
        Dim 图像源 = PPB_画面区域预览.Source
        If 图像源 Is Nothing Then Return 裁剪区域

        Dim 横坐标 = Math.Max(0, Math.Min(裁剪区域.X, 图像源.Width - 1))
        Dim 纵坐标 = Math.Max(0, Math.Min(裁剪区域.Y, 图像源.Height - 1))
        Dim 宽度 = Math.Max(1, Math.Min(裁剪区域.Width, 图像源.Width - 横坐标))
        Dim 高度 = Math.Max(1, Math.Min(裁剪区域.Height, 图像源.Height - 纵坐标))
        Return New Rectangle(CInt(横坐标), CInt(纵坐标), CInt(宽度), CInt(高度))
    End Function

    Private Sub 居中裁剪状态变化(事件来源 As Object, 事件参数 As EventArgs) Handles MCK_居中裁剪框.CheckedChanged
        PPB_画面区域预览.SelectionForceCenter = MCK_居中裁剪框.Checked
        If PPB_画面区域预览.Source IsNot Nothing Then 同步框选到裁剪参数()
    End Sub

    Private Sub 裁剪比例变化(事件来源 As Object, 事件参数 As EventArgs) Handles MCB_裁剪比例.SelectedIndexChanged, MCB_裁剪比例.TextChanged
        应用比例文本(False)
    End Sub

    Private Sub 裁剪比例结束编辑(事件来源 As Object, 事件参数 As EventArgs) Handles MCB_裁剪比例.Leave
        应用比例文本(True)
    End Sub

    Private Sub 裁剪比例按键(事件来源 As Object, 事件参数 As KeyEventArgs) Handles MCB_裁剪比例.KeyDown
        If 事件参数.KeyCode <> Keys.Enter Then Exit Sub
        应用比例文本(True)
        事件参数.SuppressKeyPress = True
    End Sub

    Private Function 应用比例文本(显示提示 As Boolean) As Boolean
        Dim 比例 As Single
        If 尝试解析比例文本(MCB_裁剪比例.Text, 比例) Then
            PPB_画面区域预览.SelectionAspectRatio = 比例
            Return True
        End If

        If 显示提示 AndAlso MCB_裁剪比例.Text.Trim() <> "" Then
            ExFloatingTip(MCB_裁剪比例, "比例格式可写 16:9、16/9 或 1.777", 1800)
        End If
        Return False
    End Function

    Private Shared Function 尝试解析比例文本(文本 As String, ByRef 比例 As Single) As Boolean
        比例 = 0
        Dim 数值 = If(文本, "").Trim()
        If 数值 = "" OrElse String.Equals(数值, "自由", StringComparison.OrdinalIgnoreCase) Then Return True

        数值 = 数值.Replace("：", ":").Replace("/", ":").Replace("x", ":").Replace("X", ":")
        Dim 分段 = 数值.Split(":"c, StringSplitOptions.RemoveEmptyEntries Or StringSplitOptions.TrimEntries)

        If 分段.Length = 2 Then
            Dim 宽度, 高度 As Double
            If Not 尝试解析正数(分段(0), 宽度) OrElse Not 尝试解析正数(分段(1), 高度) Then Return False
            Return 尝试转换有效比例(宽度 / 高度, 比例)
        End If

        If 分段.Length = 1 Then
            Dim 直接比例 As Double
            If Not 尝试解析正数(分段(0), 直接比例) Then Return False
            Return 尝试转换有效比例(直接比例, 比例)
        End If

        Return False
    End Function

    Private Shared Function 尝试转换有效比例(数值 As Double, ByRef 比例 As Single) As Boolean
        If 数值 <= 0 OrElse 数值 > Single.MaxValue Then Return False
        比例 = CSng(数值)
        Return 比例 > 0 AndAlso Not Single.IsNaN(比例) AndAlso Not Single.IsInfinity(比例)
    End Function

    Private Shared Function 尝试解析正数(文本 As String, ByRef 数值 As Double) As Boolean
        If Not Double.TryParse(文本, NumberStyles.Float, CultureInfo.InvariantCulture, 数值) AndAlso
           Not Double.TryParse(文本, NumberStyles.Float, CultureInfo.CurrentCulture, 数值) Then
            Return False
        End If
        Return 数值 > 0 AndAlso Not Double.IsNaN(数值) AndAlso Not Double.IsInfinity(数值)
    End Function

End Class
