Public Class 检查更新

    Public Shared Async Sub 检查()
        Form1.起始页面.Label64.Text = 翻译("String.CheckingUpdate")
        Form1.起始页面.Label65.Text = "" : Form1.起始页面.Label65.Visible = False
        Form1.起始页面.Label73.Text = "" : Form1.起始页面.Label73.Visible = False
        Form1.起始页面.Label75.Text = "" : Form1.起始页面.Label75.Visible = False
        Form1.起始页面.Label122.Text = "" : Form1.起始页面.Label122.Visible = False
        '检测是否有网络
        If My.Computer.Network.IsAvailable Then
            Dim a As New GitHubAPI.Release
            Dim s1 As String = Await a.获取仓库发布版信息Async("Lake1059/FFmpegFreeUI")
            If s1 <> "" Then
                Form1.起始页面.Label64.Text = 翻译("String.GetUpdateInfoFailed")
                Form1.起始页面.Label65.Text = "" : Form1.起始页面.Label65.Visible = False
                Form1.起始页面.Label73.Text = "" : Form1.起始页面.Label73.Visible = False
                Form1.起始页面.Label75.Text = "" : Form1.起始页面.Label75.Visible = False
                Form1.起始页面.Label122.Text = s1 : Form1.起始页面.Label122.Visible = True
            Else
                Form1.起始页面.Label64.Text = $"{翻译("String.ReleaseTitle")}: " & a.发布标题
                Form1.起始页面.Label65.Text = $"{翻译("String.LatestTag")}: " & a.版本标签 : Form1.起始页面.Label65.Visible = True
                Form1.起始页面.Label73.Text = $"{翻译("String.ReleaseUser")}: " & a.发布者用户名 : Form1.起始页面.Label73.Visible = True
                Form1.起始页面.Label75.Text = $"{翻译("String.FileCount")}: " & a.可供下载的文件.Count : Form1.起始页面.Label75.Visible = True
                Form1.起始页面.Label122.Text = $"{翻译("String.ReleaseTime")}: " & a.发布时间 : Form1.起始页面.Label122.Visible = True
            End If
        Else
            Form1.起始页面.Label64.Text = 翻译("String.NoNetworkToCheckUpdate")
            Form1.起始页面.Label65.Text = "" : Form1.起始页面.Label65.Visible = False
            Form1.起始页面.Label73.Text = "" : Form1.起始页面.Label73.Visible = False
            Form1.起始页面.Label75.Text = "" : Form1.起始页面.Label75.Visible = False
            Form1.起始页面.Label122.Text = "" : Form1.起始页面.Label122.Visible = False
        End If
    End Sub


End Class
