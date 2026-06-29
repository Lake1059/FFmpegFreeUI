Imports LakeUI

Public Class Form_v6_参数面板

    Public 私有界面_参数总览 As New Form_v6_参数面板_参数总览
    Public 私有界面_预设管理 As New Form_v6_参数面板_预设管理 With {.所属参数面板对象 = Me}
    Public 私有界面_输出文件设置 As New Form_v6_参数面板_输出文件设置
    Public 私有界面_解码参数 As New Form_v6_参数面板_解码参数
    Public 私有界面_视频编码器 As New Form_v6_参数面板_视频编码器 With {.所属参数面板对象 = Me}
    Public 私有界面_画面帧 As New Form_v6_参数面板_画面帧 With {.所属参数面板对象 = Me}
    Public 私有界面_质量 As New Form_v6_参数面板_质量 With {.所属参数面板对象 = Me}
    Public 私有界面_色彩管理 As New Form_v6_参数面板_色彩管理
    Public 私有界面_视频帧服务器 As New Form_v6_参数面板_视频帧服务器
    Public 私有界面_音频参数 As New Form_v6_参数面板_音频参数
    Public 私有界面_剪辑区间 As New Form_v6_参数面板_剪辑区间
    Public 私有界面_滤镜排序 As New Form_v6_参数面板_滤镜排序 With {.所属参数面板对象 = Me}
    Public 私有界面_自定义参数 As New Form_v6_参数面板_自定义参数
    Public 私有界面_自定义参数说明 As New Form_v6_参数面板_自定义参数说明
    Public 私有界面_流自定义参数 As New Form_v6_参数面板_流自定义参数
    Public 私有界面_在位置插入参数 As New Form_v6_参数面板_在位置插入参数
    Public 私有界面_完全自己写模式 As New Form_v6_参数面板_完全自己写模式
    Public 私有界面_流控制 As New Form_v6_参数面板_流控制 With {.所属参数面板对象 = Me}
    Public 私有界面_附加内容 As New Form_v6_参数面板_附加内容
    Public 私有界面_元数据 As New Form_v6_参数面板_元数据
    Public 私有界面_章节 As New Form_v6_参数面板_章节
    Public 私有界面_附件 As New Form_v6_参数面板_附件

    Public Shared ReadOnly 共享界面_画面区域选择窗口 As New Form_v6_参数面板_画面区域选择窗口
    Public 抑制自动刷新 As Boolean = False
    Private Const 参数总览页索引 As Integer = 0
    Private Const 滤镜排序页索引 As Integer = 15
    Private _正在刷新聚合页面 As Boolean


    Private Sub Form_v6_参数面板_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.ModernTabListControl1.Items(0).BoundControl = 私有界面_参数总览
        绑定选项卡(私有界面_参数总览.ModernPanel1)
        Me.ModernTabListControl1.Items(1).BoundControl = 私有界面_预设管理
        绑定选项卡(私有界面_预设管理.ModernPanel1)
        '==================================================
        Me.ModernTabListControl1.Items(3).BoundControl = 私有界面_输出文件设置
        绑定选项卡(私有界面_输出文件设置.ModernPanel1)
        Me.ModernTabListControl1.Items(4).BoundControl = 私有界面_解码参数
        绑定选项卡(私有界面_解码参数.ModernPanel1)
        '==================================================
        Me.ModernTabListControl1.Items(6).BoundControl = 私有界面_视频编码器
        绑定选项卡(私有界面_视频编码器.ModernPanel1)
        Me.ModernTabListControl1.Items(7).BoundControl = 私有界面_画面帧
        私有界面_画面帧.私有窗口_着色器超分.所属参数面板对象 = Me
        绑定选项卡(私有界面_画面帧.ModernPanel1)
        Me.ModernTabListControl1.Items(8).BoundControl = 私有界面_质量
        绑定选项卡(私有界面_质量.ModernPanel1)
        Me.ModernTabListControl1.Items(9).BoundControl = 私有界面_色彩管理
        绑定选项卡(私有界面_色彩管理.ModernPanel1)
        Me.ModernTabListControl1.Items(10).BoundControl = 私有界面_视频帧服务器
        私有界面_视频帧服务器.所属参数面板对象 = Me
        绑定选项卡(私有界面_视频帧服务器.ModernPanel1)
        '==================================================
        Me.ModernTabListControl1.Items(12).BoundControl = 私有界面_音频参数
        绑定选项卡(私有界面_音频参数.ModernPanel1)
        '==================================================
        Me.ModernTabListControl1.Items(14).BoundControl = 私有界面_剪辑区间
        绑定选项卡(私有界面_剪辑区间.ModernPanel1)
        Me.ModernTabListControl1.Items(15).BoundControl = 私有界面_滤镜排序
        绑定选项卡(私有界面_滤镜排序.ModernPanel1)
        '==================================================
        Me.ModernTabListControl1.Items(16).BoundControl = 私有界面_自定义参数
        绑定选项卡(私有界面_自定义参数.ModernPanel1)
        If SP_UnLock AndAlso 设置_v6.实例对象.窗口样式 = 2 AndAlso 设置_v6.实例对象.SP_毛玻璃模式 > 0 Then
            私有界面_自定义参数.ModernTabControl1.TabStripBackColor = Color.Transparent
            私有界面_自定义参数.ModernTabControl1.ContentBackColor = Color.Transparent
            私有界面_自定义参数说明.ModernPanel1.Padding = New Padding(20, 0, 20, 20)
            私有界面_流自定义参数.ModernPanel1.Padding = New Padding(20, 0, 20, 20)
            私有界面_在位置插入参数.ModernPanel1.Padding = New Padding(20, 0, 20, 20)
            私有界面_完全自己写模式.ModernPanel1.Padding = New Padding(20, 0, 20, 20)
        End If
        Me.私有界面_自定义参数.ModernTabControl1.Items(0).BoundControl = 私有界面_自定义参数说明
        绑定选项卡(私有界面_自定义参数说明.ModernPanel1)
        Me.私有界面_自定义参数.ModernTabControl1.Items(2).BoundControl = 私有界面_流自定义参数
        绑定选项卡(私有界面_流自定义参数.ModernPanel1)
        Me.私有界面_自定义参数.ModernTabControl1.Items(3).BoundControl = 私有界面_在位置插入参数
        绑定选项卡(私有界面_在位置插入参数.ModernPanel1)
        Me.私有界面_自定义参数.ModernTabControl1.Items(5).BoundControl = 私有界面_完全自己写模式
        绑定选项卡(私有界面_完全自己写模式.ModernPanel1)
        '==================================================
        Me.ModernTabListControl1.Items(17).BoundControl = 私有界面_流控制
        绑定选项卡(私有界面_流控制.ModernPanel1)
        '==================================================
        Me.ModernTabListControl1.Items(19).BoundControl = 私有界面_附加内容
        绑定选项卡(私有界面_附加内容.ModernPanel1)
        If SP_UnLock AndAlso 设置_v6.实例对象.窗口样式 = 2 AndAlso 设置_v6.实例对象.SP_毛玻璃模式 > 0 Then
            私有界面_附加内容.ModernTabControl1.TabStripBackColor = Color.Transparent
            私有界面_附加内容.ModernTabControl1.ContentBackColor = Color.Transparent
            私有界面_元数据.ModernPanel1.Padding = New Padding(20, 0, 20, 20)
            私有界面_章节.ModernPanel1.Padding = New Padding(20, 0, 20, 20)
            私有界面_附件.ModernPanel1.Padding = New Padding(20, 0, 20, 20)
        End If
        Me.私有界面_附加内容.ModernTabControl1.Items(0).BoundControl = 私有界面_元数据
        私有界面_元数据.所属参数面板对象 = Me
        绑定选项卡(私有界面_元数据.ModernPanel1)
        Me.私有界面_附加内容.ModernTabControl1.Items(1).BoundControl = 私有界面_章节
        私有界面_章节.所属参数面板对象 = Me
        绑定选项卡(私有界面_章节.ModernPanel1)
        Me.私有界面_附加内容.ModernTabControl1.Items(2).BoundControl = 私有界面_附件
        私有界面_附件.所属参数面板对象 = Me
        绑定选项卡(私有界面_附件.ModernPanel1)
        '==================================================
        Me.ModernTabListControl1.SelectedIndex = 0
        Me.私有界面_自定义参数.ModernTabControl1.SelectedIndex = 0
        Me.私有界面_附加内容.ModernTabControl1.SelectedIndex = 0
        '==================================================
        预设管理_v6.重置面板(Me)
        私有界面_预设管理.刷新预设列表()
    End Sub

    Private Sub Form_v6_参数面板_Shown(sender As Object, e As EventArgs) Handles Me.Shown

    End Sub

    Private Sub 绑定选项卡(选项卡的根面板容器 As ModernPanel)
        If SP_UnLock Then
            Select Case 设置_v6.实例对象.SP_毛玻璃模式
                Case > 0
                    选项卡的根面板容器.BackColor = Color.Transparent
                    选项卡的根面板容器.BackColor1 = Color.Transparent
                    选项卡的根面板容器.BackgroundSource = Me.ParentForm
            End Select
        End If
    End Sub

    Public Shared Sub 弹出画面区域选择窗口(完成按钮返回的控件 As Control, 标题栏 As String)
        If 共享界面_画面区域选择窗口.目标控件 IsNot Nothing Then
            ExFloatingTip("画面区域选择窗口正在使用中，请关闭后再试，为了节约性能这个窗口只能打开一个", 3000)
            Exit Sub
        End If
        共享界面_画面区域选择窗口.目标控件 = 完成按钮返回的控件
        共享界面_画面区域选择窗口.Text = 标题栏
        显示窗体(共享界面_画面区域选择窗口, FormMain_v6)
    End Sub

    Public Sub 请求刷新参数状态()
        ' 子页面仍可发出变更通知；汇总类页面只在切入时统一读取当前值。
        If 抑制自动刷新 OrElse IsDisposed Then Exit Sub
    End Sub

    Private Sub ModernTabListControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ModernTabListControl1.SelectedIndexChanged
        If 抑制自动刷新 OrElse IsDisposed OrElse _正在刷新聚合页面 Then Exit Sub

        Select Case ModernTabListControl1.SelectedIndex
            Case 参数总览页索引
                刷新参数总览页()
            Case 滤镜排序页索引
                刷新滤镜排序页()
        End Select
    End Sub

    Private Sub 刷新参数总览页()
        If _正在刷新聚合页面 Then Exit Sub
        _正在刷新聚合页面 = True
        Try
            预设管理_v6.刷新参数总览(Me)
        Finally
            _正在刷新聚合页面 = False
        End Try
    End Sub

    Private Sub 刷新滤镜排序页()
        If _正在刷新聚合页面 Then Exit Sub
        _正在刷新聚合页面 = True
        Try
            预设管理_v6.同步全部内置滤镜到排序(Me, False)
        Finally
            _正在刷新聚合页面 = False
        End Try
    End Sub

End Class
