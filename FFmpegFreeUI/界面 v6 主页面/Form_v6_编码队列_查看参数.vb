Imports LakeUI

Public Class Form_v6_编码队列_查看参数
    Private ReadOnly 参数总览快照 As String
    Private ReadOnly 实际命令行快照 As String
    Private ReadOnly 任务显示名称 As String

    Public Sub New()
        Me.New(Nothing)
    End Sub

    Public Sub New(task As 编码任务_v6)
        InitializeComponent()

        任务显示名称 = 获取任务显示名称(task)
        If task?.预设数据 IsNot Nothing Then
            预设管理_v6.显示参数总览(MTB_参数总览, 编码队列_v6.克隆预设(task.预设数据))
            参数总览快照 = MTB_参数总览.Text
        ElseIf task IsNot Nothing AndAlso task.命令行 <> "" Then
            参数总览快照 = "命令行任务：" & If(task.任务名称, "")
            MTB_参数总览.Text = 参数总览快照
        Else
            参数总览快照 = "没有可显示的任务参数"
            MTB_参数总览.Text = 参数总览快照
        End If

        实际命令行快照 = 编码队列_v6.获取任务执行命令行文本(task)
        MTB_命令行模板.Text = 实际命令行快照
        Text = If(任务显示名称 = "", "查看任务参数", "查看任务参数 - " & 任务显示名称)
    End Sub

    Private Shared Function 获取任务显示名称(task As 编码任务_v6) As String
        If task Is Nothing Then Return ""
        If task.任务名称 <> "" Then Return task.任务名称
        Return IO.Path.GetFileName(task.输入文件)
    End Function

    Private Sub Form_v6_编码队列_查看参数_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = FormMain_v6.Icon
        SetControlFont(设置_v6.实例对象.字体, Me, , True)
        If FormMain_v6.ThisIsYourWindow1.AttachedForms.Count > 0 Then
            FormMain_v6.ThisIsYourWindow1.Attach(Me)
            ModernPanel1.BackColor = Color.Transparent
            ModernPanel1.BackColor1 = Color.Transparent
        End If
        BeginInvoke(Sub() 居中到v6主窗口())
    End Sub

    Private Sub Form_v6_编码队列_查看参数_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged
        Panel1.Width = CInt(Math.Round(ClientSize.Width * 0.5))
    End Sub

    Private Sub MB_复制参数总览_Click(sender As Object, e As EventArgs) Handles MB_复制参数总览.Click
        If 参数总览快照 <> "" Then Clipboard.SetText(参数总览快照)
        ExFloatingTip(MB_复制参数总览, "已复制参数总览", 1200)
    End Sub

    Private Sub MB_复制命令行模板_Click(sender As Object, e As EventArgs) Handles MB_复制命令行模板.Click
        If 实际命令行快照 <> "" Then Clipboard.SetText(实际命令行快照)
        ExFloatingTip(MB_复制命令行模板, "已复制实际命令行", 1200)
    End Sub

    Private Sub 居中到v6主窗口()
        Try
            Dim mainForm As Form = FormMain_v6
            Dim baseBounds As Rectangle
            If mainForm IsNot Nothing AndAlso Not mainForm.IsDisposed AndAlso mainForm.Visible AndAlso mainForm.WindowState <> FormWindowState.Minimized Then
                baseBounds = mainForm.Bounds
            Else
                baseBounds = Screen.FromPoint(Cursor.Position).WorkingArea
            End If

            Dim screenArea = Screen.FromRectangle(baseBounds).WorkingArea
            Dim x = baseBounds.Left + CInt(Math.Round((baseBounds.Width - Width) / 2.0))
            Dim y = baseBounds.Top + CInt(Math.Round((baseBounds.Height - Height) / 2.0))
            x = Math.Max(screenArea.Left, Math.Min(x, screenArea.Right - Width))
            y = Math.Max(screenArea.Top, Math.Min(y, screenArea.Bottom - Height))
            StartPosition = FormStartPosition.Manual
            Location = New Point(x, y)
        Catch
        End Try
    End Sub
End Class
