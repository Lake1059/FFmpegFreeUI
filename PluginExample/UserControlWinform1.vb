Public Class UserControlWinform1
    Private Sub UserControlWinform1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub UserControlWinform1_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged
        Panel2.Width = (Panel1.Width - Label1.Width * 2) / 3
        Panel4.Width = Panel2.Width
        Panel7.Width = Panel2.Width
        Panel8.Width = Panel7.Width

    End Sub

    Private Sub UiButton1_Click(sender As Object, e As EventArgs) Handles UiButton1.Click
        Entry.HostCall_AddMissionToQueue.Invoke("", "假装这是一个任务，其实没法运行的", "")
    End Sub
End Class
