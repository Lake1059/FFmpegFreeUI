Public Class Form_v6_社区_个人中心
    Private Sub Form_v6_社区_个人中心_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MarkDownViewer1.Text = "3FUI Studios 是 3FUI 开发者联合群内团队建立的成果共享平台，其面向所有国内用户提供，整合了预设分享和压片成果分享，全平台纯公益经营，无任何收费项目；同时由于我们处于国内，因此审核力度将非常大。

### 社区准则
登录即代表同意所有规则！

+ 禁止发布违反任何国内法律的任何内容
+ 禁止发布原版光盘文件，除非版权方已不存在或默许
+ 禁止发布有独家版权独播的内容
+ 禁止发布综艺类电视节目、任何时政相关
+ 禁止发布任何有版权的纯歌曲
+ 运营团队一旦接到版权方的举报会立刻下架所有对应资源
+ 情节严重者将直接封禁账号，同时保留清除违规账号所有数据的权利

### 审核流程和发布规范

+ 预设文件由系统自动审核，如有漏网之鱼也会被人工清除，请勿心存侥幸
+ 音视频成果仅支持国内部分云盘发布，且全部由人工进行基础审核，禁止使用压缩包、改后缀、文件头数据等手段规避检查，任何试图规避检查的行为一旦发现直接封号处理
"

        Me.MarkDownViewer2.Text = "
### 无可用活动
当前没有正在进行的活动
"
    End Sub

    Private Sub Form_v6_社区_个人中心_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged
        Panel6.Width = Me.Width * 0.4

        MB_登录.Height = (Panel4.Height - 10 * (Me.DeviceDpi / 96)) / 2
        MB_登出.Height = MB_登录.Height

        HtmlColorLabel3.Width = (ModernPanel4.Width - ModernPanel4.Padding.Left - ModernPanel4.Padding.Right - JustEmptyControl5.Width * 5) / 5
        HtmlColorLabel4.Width = HtmlColorLabel3.Width
        HtmlColorLabel5.Width = HtmlColorLabel3.Width
        HtmlColorLabel6.Width = HtmlColorLabel3.Width
        HtmlColorLabel7.Width = HtmlColorLabel3.Width

        MB_浏览社区内容.Width = (Panel1.Width - JustEmptyControl14.Width - JustEmptyControl11.Width) / 3
        MB_管理我的内容.Width = MB_浏览社区内容.Width
        MB_我的消息.Width = MB_发布预设.Width

        MB_发布预设.Width = (Panel3.Width - JustEmptyControl9.Width - JustEmptyControl10.Width) / 3
        MB_发布压制成果.Width = MB_发布预设.Width
        MB_查看审核进度.Width = MB_发布预设.Width

        MB_审核列表.Width = (Panel5.Width - JustEmptyControl15.Width - JustEmptyControl8.Width) / 3
        MB_用户账户管理.Width = MB_审核列表.Width
        MB_服务器性能界面.Width = MB_审核列表.Width
    End Sub
End Class