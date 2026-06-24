Imports System.IO
Imports Microsoft.WindowsAPICodePack.Dialogs

Public Class Form_v6_参数面板_输出文件设置

    Private 后缀菜单已初始化 As Boolean = False
    Private 正在选择输出位置 As Boolean = False

    Public Sub 设置自定义输出位置(路径 As String)
        Dim value = 规范化文件夹路径(路径)
        正在选择输出位置 = True
        Try
            MCB_输出位置.Text = value
            MCB_输出位置.SelectedIndex = -1
            If MCB_输出位置.Text <> value Then MCB_输出位置.Text = value
        Finally
            正在选择输出位置 = False
        End Try
    End Sub

    Private Sub Form_v6_参数面板_输出文件设置_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        绑定路径下拉框拖拽(MCB_输出位置, 路径下拉框拖拽模式.文件夹路径)
        If MCB_输出文件参数使用方法.SelectedIndex < 0 Then MCB_输出文件参数使用方法.SelectedIndex = 0
        If MCB_输出位置.SelectedIndex < 0 Then MCB_输出位置.SelectedIndex = 0
        初始化后缀菜单()
    End Sub

    Private Sub 初始化后缀菜单()
        If 后缀菜单已初始化 Then Exit Sub
        后缀菜单已初始化 = True

        添加后缀菜单项(视频菜单, ".mp4")
        添加后缀菜单项(视频菜单, ".mkv")
        添加后缀菜单项(视频菜单, ".flv")
        添加后缀菜单项(视频菜单, ".mov")
        添加后缀菜单项(视频菜单, ".webm")
        添加后缀菜单项(视频菜单, ".m2ts")
        添加后缀菜单项(视频菜单, ".wmv")
        添加后缀菜单项(视频菜单, ".avi")
        添加后缀菜单项(视频菜单, ".rmvb")
        添加后缀菜单项(视频菜单, ".ts")
        添加后缀菜单项(视频菜单, ".3gp")

        添加后缀菜单项(音频菜单, ".mp3")
        添加后缀菜单项(音频菜单, ".aac")
        添加后缀菜单项(音频菜单, ".wav")
        添加后缀菜单项(音频菜单, ".flac")
        添加后缀菜单项(音频菜单, ".alac")
        添加后缀菜单项(音频菜单, ".aiff")
        添加后缀菜单项(音频菜单, ".ac3")
        添加后缀菜单项(音频菜单, ".ogg")
        添加后缀菜单项(音频菜单, ".opus")
        添加后缀菜单项(音频菜单, ".m4a")

        添加后缀菜单项(图片菜单, ".png")
        添加后缀菜单项(图片菜单, ".jpg")
        添加后缀菜单项(图片菜单, ".jpeg")
        添加后缀菜单项(图片菜单, ".webp")
        添加后缀菜单项(图片菜单, ".avif")
        添加后缀菜单项(图片菜单, ".bmp")
        添加后缀菜单项(图片菜单, ".gif")
        添加后缀菜单项(图片菜单, ".ico")

        添加说明项(后缀菜单, "全部分类")
        后缀菜单.Items.Add(New LakeUI.ModernContextMenu.ModernMenuItem("视频") With {.SubMenu = 视频菜单})
        后缀菜单.Items.Add(New LakeUI.ModernContextMenu.ModernMenuItem("音频") With {.SubMenu = 音频菜单})
        后缀菜单.Items.Add(New LakeUI.ModernContextMenu.ModernMenuItem("图片") With {.SubMenu = 图片菜单})
        添加分割线(后缀菜单)
        添加说明项(后缀菜单, "常用容器")
        添加后缀菜单项(后缀菜单, ".mp4")
        添加后缀菜单项(后缀菜单, ".mkv")
    End Sub

    Private Sub 添加说明项(menu As LakeUI.ModernContextMenu, text As String)
        menu.Items.Add(New LakeUI.ModernContextMenu.ModernMenuItem(text) With {.IsDescription = True})
    End Sub

    Private Sub 添加分割线(menu As LakeUI.ModernContextMenu)
        menu.Items.Add(New LakeUI.ModernContextMenu.ModernMenuItem() With {.IsSeparator = True})
    End Sub

    Private Sub 添加后缀菜单项(menu As LakeUI.ModernContextMenu, suffix As String)
        Dim item As New LakeUI.ModernContextMenu.ModernMenuItem(suffix)
        AddHandler item.Click, Sub()
                                   MTB_后缀.Text = suffix
                               End Sub
        menu.Items.Add(item)
    End Sub

    Private Sub MCB_输出文件参数使用方法_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_输出文件参数使用方法.SelectedIndexChanged
        If MCB_输出文件参数使用方法.SelectedIndex < 0 Then MCB_输出文件参数使用方法.SelectedIndex = 0
    End Sub

    Private Sub MCB_输出位置_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_输出位置.SelectedIndexChanged
        If 正在选择输出位置 Then Exit Sub
        If MCB_输出位置.SelectedIndex = 1 Then
            正在选择输出位置 = True
            Try
                Dim dialog As New CommonOpenFileDialog With {.IsFolderPicker = True}
                If dialog.ShowDialog() = CommonFileDialogResult.Ok Then
                    设置自定义输出位置(dialog.FileName)
                Else
                    MCB_输出位置.SelectedIndex = 0
                End If
            Finally
                正在选择输出位置 = False
            End Try
        ElseIf MCB_输出位置.SelectedIndex < 0 AndAlso MCB_输出位置.Text.Trim() = "" Then
            MCB_输出位置.SelectedIndex = 0
        End If
    End Sub

    Private Sub MB_选择后缀_Click(sender As Object, e As EventArgs) Handles MB_选择后缀.Click
        初始化后缀菜单()
        后缀菜单.Show(MB_选择后缀, 0, MB_选择后缀.Height)
    End Sub

    Private Sub Form_v6_参数面板_输出文件设置_FontChanged(sender As Object, e As EventArgs) Handles Me.FontChanged
        后缀菜单.MenuFont = New Font(设置_v6.实例对象.字体, 后缀菜单.MenuFont.Size, 后缀菜单.MenuFont.Style)
        后缀菜单.DescriptionFont = New Font(设置_v6.实例对象.字体, 后缀菜单.MenuFont.Size, 后缀菜单.MenuFont.Style)

        视频菜单.MenuFont = New Font(设置_v6.实例对象.字体, 视频菜单.MenuFont.Size, 视频菜单.MenuFont.Style)
        视频菜单.DescriptionFont = New Font(设置_v6.实例对象.字体, 视频菜单.MenuFont.Size, 视频菜单.MenuFont.Style)

        音频菜单.MenuFont = New Font(设置_v6.实例对象.字体, 音频菜单.MenuFont.Size, 音频菜单.MenuFont.Style)
        音频菜单.DescriptionFont = New Font(设置_v6.实例对象.字体, 音频菜单.MenuFont.Size, 音频菜单.MenuFont.Style)

        图片菜单.MenuFont = New Font(设置_v6.实例对象.字体, 图片菜单.MenuFont.Size, 图片菜单.MenuFont.Style)
        图片菜单.DescriptionFont = New Font(设置_v6.实例对象.字体, 图片菜单.MenuFont.Size, 图片菜单.MenuFont.Style)
    End Sub
End Class
