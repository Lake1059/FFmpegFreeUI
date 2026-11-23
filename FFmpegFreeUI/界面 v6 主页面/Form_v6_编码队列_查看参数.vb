Imports LakeUI

Public Class Form_v6_编码队列_查看参数
    Private Shared ReadOnly 任务窗口 As New 编码队列任务窗口组_v6(Of Form_v6_编码队列_查看参数)(
        Function(task) New Form_v6_编码队列_查看参数(task))

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

    Friend Shared Sub 打开或激活(owner As Form, tasks As IEnumerable(Of 编码任务_v6))
        任务窗口.打开或激活(owner, tasks)
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
End Class
