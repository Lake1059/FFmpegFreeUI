Public Class 用户统计

    Public Shared Sub 回报活跃()
        Try
            If Not My.Computer.Network.IsAvailable Then Exit Sub
            If Not 用户设置.实例对象.是否参与用户统计 Then Exit Sub
            If 用户设置.实例对象.上次回报活跃信息的日期.Day = Now.Day Then Exit Sub
            Dim BGW As New ComponentModel.BackgroundWorker
            AddHandler BGW.DoWork,
               Async Sub(sender As Object, e As ComponentModel.DoWorkEventArgs)
                   Using httpClient As New Net.Http.HttpClient() With {.Timeout = TimeSpan.FromSeconds(10)}
                       httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("hajimimanbo")
                       Dim response As Net.Http.HttpResponseMessage = Await httpClient.GetAsync("http://bs.frostlynx.work:14536/api/ping")
                       If response.IsSuccessStatusCode Then
                           Dim result As String = Await response.Content.ReadAsStringAsync()
                           Select Case result
                               Case "1145141919810", "OK"
                                   用户设置.实例对象.上次回报活跃信息的日期 = Now
                                   Debug.Print("OKKK")
                               Case Else : Debug.Print(result)
                           End Select
                       Else
                           Debug.Print(response.ToString)
                       End If
                   End Using
               End Sub
            BGW.RunWorkerAsync()
        Catch ex As Exception
        End Try
    End Sub
End Class
