Public Class 检查更新

    Public Shared Async Sub 检查()
        Form1.起始页面.Label64.Text = "正在检查更新版本 ..."
        Form1.起始页面.Label65.Text = "" : Form1.起始页面.Label65.Visible = False
        Form1.起始页面.Label73.Text = "" : Form1.起始页面.Label73.Visible = False
        Form1.起始页面.Label75.Text = "" : Form1.起始页面.Label75.Visible = False
        Form1.起始页面.Label122.Text = "" : Form1.起始页面.Label122.Visible = False
        '检测是否有网络
        If My.Computer.Network.IsAvailable Then
            Dim a As New GitHubAPI.Release
            Dim s1 As String = Await a.获取仓库发布版信息Async("Lake1059/FFmpegFreeUI")
            If s1 <> "" Then
                Form1.起始页面.Label64.Text = "获取更新信息失败"
                Form1.起始页面.Label65.Text = "" : Form1.起始页面.Label65.Visible = False
                Form1.起始页面.Label73.Text = "" : Form1.起始页面.Label73.Visible = False
                Form1.起始页面.Label75.Text = "" : Form1.起始页面.Label75.Visible = False
                Form1.起始页面.Label122.Text = s1 : Form1.起始页面.Label122.Visible = True
            Else
                Form1.起始页面.Label64.Text = "发布标题：" & a.发布标题
                Form1.起始页面.Label65.Text = "最新标签：" & a.版本标签 : Form1.起始页面.Label65.Visible = True
                Form1.起始页面.Label73.Text = "发布用户：" & a.发布者用户名 : Form1.起始页面.Label73.Visible = True
                Form1.起始页面.Label75.Text = "文件数量：" & a.可供下载的文件.Count : Form1.起始页面.Label75.Visible = True
                Form1.起始页面.Label122.Text = "发布时间：" & a.发布时间 : Form1.起始页面.Label122.Visible = True
            End If
        Else
            Form1.起始页面.Label64.Text = "无网络连接！联网后重启应用程序以重试"
            Form1.起始页面.Label65.Text = "" : Form1.起始页面.Label65.Visible = False
            Form1.起始页面.Label73.Text = "" : Form1.起始页面.Label73.Visible = False
            Form1.起始页面.Label75.Text = "" : Form1.起始页面.Label75.Visible = False
            Form1.起始页面.Label122.Text = "" : Form1.起始页面.Label122.Visible = False
        End If
    End Sub


End Class
