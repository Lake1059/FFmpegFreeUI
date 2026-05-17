Imports LakeUI

Public Class Form_v6_参数面板

    Public 私有界面_参数总览 As New Form_v6_参数面板_参数总览
    Public 私有界面_预设管理 As New Form_v6_参数面板_预设管理
    Public 私有界面_输出文件设置 As New Form_v6_参数面板_输出文件设置
    Public 私有界面_解码参数 As New Form_v6_参数面板_解码参数
    Public 私有界面_视频编码器 As New Form_v6_参数面板_视频编码器 With {.所属参数面板对象 = Me}
    Public 私有界面_画面帧 As New Form_v6_参数面板_画面帧
    Public 私有界面_质量 As New Form_v6_参数面板_质量
    Public 私有界面_色彩管理 As New Form_v6_参数面板_色彩管理
    Public 私有界面_视频帧服务器 As New Form_v6_参数面板_视频帧服务器
    Public 私有界面_音频参数 As New Form_v6_参数面板_音频参数
    Public 私有界面_剪辑区间 As New Form_v6_参数面板_剪辑区间
    Public 私有界面_滤镜排序 As New Form_v6_参数面板_滤镜排序
    Public 私有界面_自定义参数 As New Form_v6_参数面板_自定义参数
    Public 私有界面_自定义参数说明 As New Form_v6_参数面板_自定义参数说明
    Public 私有界面_流自定义参数 As New Form_v6_参数面板_流自定义参数
    Public 私有界面_在位置插入参数 As New Form_v6_参数面板_在位置插入参数
    Public 私有界面_完全自己写模式 As New Form_v6_参数面板_完全自己写模式
    Public 私有界面_流控制 As New Form_v6_参数面板_流控制
    Public 私有界面_附加内容 As New Form_v6_参数面板_附加内容
    Public 私有界面_元数据 As New Form_v6_参数面板_元数据
    Public 私有界面_章节 As New Form_v6_参数面板_章节
    Public 私有界面_附件 As New Form_v6_参数面板_附件

    Private Sub Form_v6_参数面板_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.ModernTabListControl1.Items(0).BoundControl = 私有界面_参数总览
        绑定选项卡窗体背景透明(私有界面_参数总览.ModernPanel1)
        Me.ModernTabListControl1.Items(1).BoundControl = 私有界面_预设管理
        绑定选项卡窗体背景透明(私有界面_预设管理.ModernPanel1)
        '==================================================
        Me.ModernTabListControl1.Items(3).BoundControl = 私有界面_输出文件设置
        绑定选项卡窗体背景透明(私有界面_输出文件设置.ModernPanel1)
        Me.ModernTabListControl1.Items(4).BoundControl = 私有界面_解码参数
        绑定选项卡窗体背景透明(私有界面_解码参数.ModernPanel1)
        '==================================================
        Me.ModernTabListControl1.Items(6).BoundControl = 私有界面_视频编码器
        绑定选项卡窗体背景透明(私有界面_视频编码器.ModernPanel1)
        Me.ModernTabListControl1.Items(7).BoundControl = 私有界面_画面帧
        绑定选项卡窗体背景透明(私有界面_画面帧.ModernPanel1)
        Me.ModernTabListControl1.Items(8).BoundControl = 私有界面_质量
        绑定选项卡窗体背景透明(私有界面_质量.ModernPanel1)
        Me.ModernTabListControl1.Items(9).BoundControl = 私有界面_色彩管理
        绑定选项卡窗体背景透明(私有界面_色彩管理.ModernPanel1)
        Me.ModernTabListControl1.Items(10).BoundControl = 私有界面_视频帧服务器
        绑定选项卡窗体背景透明(私有界面_视频帧服务器.ModernPanel1)
        '==================================================
        Me.ModernTabListControl1.Items(12).BoundControl = 私有界面_音频参数
        '==================================================
        Me.ModernTabListControl1.Items(14).BoundControl = 私有界面_剪辑区间
        Me.ModernTabListControl1.Items(15).BoundControl = 私有界面_滤镜排序
        Me.ModernTabListControl1.Items(16).BoundControl = 私有界面_自定义参数
        Me.私有界面_自定义参数.ModernTabControl1.Items(0).BoundControl = 私有界面_自定义参数说明
        Me.私有界面_自定义参数.ModernTabControl1.Items(2).BoundControl = 私有界面_流自定义参数
        Me.私有界面_自定义参数.ModernTabControl1.Items(3).BoundControl = 私有界面_在位置插入参数
        Me.私有界面_自定义参数.ModernTabControl1.Items(5).BoundControl = 私有界面_完全自己写模式
        Me.ModernTabListControl1.Items(17).BoundControl = 私有界面_流控制
        '==================================================
        Me.ModernTabListControl1.Items(19).BoundControl = 私有界面_附加内容
        Me.私有界面_附加内容.ModernTabControl1.Items(0).BoundControl = 私有界面_元数据
        Me.私有界面_附加内容.ModernTabControl1.Items(1).BoundControl = 私有界面_章节
        Me.私有界面_附加内容.ModernTabControl1.Items(2).BoundControl = 私有界面_附件
        '==================================================
        Me.ModernTabListControl1.SelectedIndex = 0
        Me.私有界面_自定义参数.ModernTabControl1.SelectedIndex = 0
        Me.私有界面_附加内容.ModernTabControl1.SelectedIndex = 0
        '==================================================



    End Sub

    Private Sub Form_v6_参数面板_Shown(sender As Object, e As EventArgs) Handles Me.Shown

    End Sub

    Shared Sub 绑定选项卡窗体背景透明(选项卡的根面板容器 As ModernPanel)
        If SP_UnLock Then
            Select Case 设置_v6.实例对象.SP_毛玻璃模式
                Case > 0
                    选项卡的根面板容器.BackColor = Color.Transparent
                    选项卡的根面板容器.BackColor1 = Color.Transparent
                    选项卡的根面板容器.BackgroundSource = FormMain_v6
            End Select
        End If
    End Sub

End Class