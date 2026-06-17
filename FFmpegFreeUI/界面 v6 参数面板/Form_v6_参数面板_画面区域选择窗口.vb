Imports System.IO
Imports Sunny.UI

Public Class Form_v6_参数面板_画面区域选择窗口

    Public 目标控件 As Control = Nothing

    Private Sub Form_v6_参数面板_画面区域选择窗口_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = FormMain_v6.Icon
        SetControlFont(设置_v6.实例对象.字体, Me, , True)
        If FormMain_v6.ThisIsYourWindow1.AttachedForms.Count > 0 Then
            Me.ModernPanel1.BackColor = Color.Transparent
            FormMain_v6.ThisIsYourWindow1.Attach(Me)
        End If
    End Sub

    Private Sub Form_v6_参数面板_画面区域选择窗口_Shown(sender As Object, e As EventArgs) Handles Me.Shown

    End Sub

    Private Sub MB_打开_Click(sender As Object, e As EventArgs) Handles MB_打开.Click
        打开视频获取画面(ShowFileOpenDialog())
    End Sub

    Private Sub MB_完成_Click(sender As Object, e As EventArgs) Handles MB_完成.Click
        目标控件.Text = ModernTextBox1.Text
        关闭窗体流程()
    End Sub

    Private Function ShowFileOpenDialog() As String
        Using openFileDialog As New OpenFileDialog With {.Multiselect = False, .Filter = "所有文件|*.*"}
            If openFileDialog.ShowDialog() = DialogResult.OK Then
                Return openFileDialog.FileName
            End If
        End Using
        Return String.Empty
    End Function
    Private Function ExtractFrameFromVideo(videoPath As String) As Boolean
        Try
            Using process As New Process()
                process.StartInfo.FileName = "ffmpeg"
                process.StartInfo.WorkingDirectory = If(设置_v6.实例对象.工作目录 <> "", 设置_v6.实例对象.工作目录, "")
                Dim timestamp As String = If(Not String.IsNullOrEmpty(ModernTextBox1.Text), ModernTextBox1.Text, "0:0:10")
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
    Sub 打开视频获取画面(视频文件 As String)
        If String.IsNullOrEmpty(视频文件) Then Exit Sub
        If Not ExtractFrameFromVideo(视频文件) Then Exit Sub
        Dim previewPath As String = Path.Combine(Application.StartupPath, "ScreenCropPreview.png")
        If Not File.Exists(previewPath) Then Exit Sub
        PixelPictureBox1.Image?.Dispose()
        PixelPictureBox1.Image = LoadImageFromFile(previewPath)
        FileIO.FileSystem.DeleteFile(previewPath)
    End Sub

    Private Sub Form_v6_参数面板_画面区域选择窗口_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        e.Cancel = True
        关闭窗体流程()
    End Sub

    Sub 关闭窗体流程()
        目标控件?.FindForm?.Focus()
        目标控件 = Nothing
        Me.PixelPictureBox1.Image = Nothing
        Me.PixelPictureBox1.ClearSelection()
        Me.Text = ""
        Me.Hide()
    End Sub

End Class
