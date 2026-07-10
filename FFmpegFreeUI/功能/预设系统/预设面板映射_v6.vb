Imports System.Globalization
Imports System.IO
Imports System.Text
Imports System.Text.Json
Imports LakeUI

Partial Public Class 预设管理_v6

    Private Shared Function 从面板创建预设但不读排序(ui As Form_v6_参数面板) As 预设数据_v6
        Dim a As New 预设数据_v6
        If ui Is Nothing Then Return a
        储存输出文件设置(a, ui)
        储存解码参数(a, ui)
        储存视频编码器(a, ui)
        储存画面帧(a, ui)
        储存质量(a, ui)
        储存色彩管理(a, ui)
        储存音频参数(a, ui)
        储存剪辑(a, ui)
        储存视频帧服务器(a, ui)
        储存自定义参数(a, ui)
        储存流控制(a, ui)
        储存附加内容(a, ui)
        Return a
    End Function

    Private Shared Sub 更新排序项(排序页 As Form_v6_参数面板_滤镜排序, a As 预设数据_v6, 标识符 As 预设数据_v6.滤镜排序单片结构.标识符枚举, 启用 As Boolean)
        If 启用 Then
            排序页.添加或更新内置滤镜(标识符, 获取目标流类型(标识符), 获取滤镜显示名称(标识符), 获取滤镜片段(a, New 预设数据_v6.滤镜排序单片结构 With {.滤镜标识符 = 标识符}))
        Else
            排序页.移除内置滤镜(标识符)
        End If
    End Sub

    Private Shared Function 获取目标流类型(标识符 As 预设数据_v6.滤镜排序单片结构.标识符枚举) As 预设数据_v6.滤镜排序单片结构.流类型
        Select Case 标识符
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.音频响度标准化,
                 预设数据_v6.滤镜排序单片结构.标识符枚举.音频格式转换,
                 预设数据_v6.滤镜排序单片结构.标识符枚举.音频重采样,
                 预设数据_v6.滤镜排序单片结构.标识符枚举.自定义音频滤镜
                Return 预设数据_v6.滤镜排序单片结构.流类型.音频
            Case Else
                Return 预设数据_v6.滤镜排序单片结构.流类型.视频
        End Select
    End Function

    Public Shared Function 获取滤镜显示名称(标识符 As 预设数据_v6.滤镜排序单片结构.标识符枚举) As String
        If 标识符 = 预设数据_v6.滤镜排序单片结构.标识符枚举.未设置 Then Return "未设置"
        Return 标识符.ToString()
    End Function
    Private Shared Function SplitTextList(value As String) As String()
        If String.IsNullOrWhiteSpace(value) Then Return Array.Empty(Of String)()
        Return value.Split(separator, StringSplitOptions.RemoveEmptyEntries).Select(Function(x) x.Trim()).Where(Function(x) x <> "").ToArray()
    End Function

    Private Shared Function SelectedIndexToEnum(Of T As Structure)(index As Integer) As T
        Dim result As T = Nothing
        [Enum].TryParse(index.ToString(), result)
        Return result
    End Function

    Private Shared Function EnumToIndex(value As [Enum]) As Integer
        Return Convert.ToInt32(value, CultureInfo.InvariantCulture)
    End Function

    Private Shared Function 质量控制方式SelectedIndexToEnum(index As Integer) As 预设数据_v6.视频全局质量控制方式
        Select Case index
            Case 1
                Return 预设数据_v6.视频全局质量控制方式.CRF
            Case 2
                Return 预设数据_v6.视频全局质量控制方式.VBR
            Case 3
                Return 预设数据_v6.视频全局质量控制方式.CQP
            Case 4
                Return 预设数据_v6.视频全局质量控制方式.CBR
            Case 5
                Return 预设数据_v6.视频全局质量控制方式.TPE
            Case Else
                Return 预设数据_v6.视频全局质量控制方式.未选择
        End Select
    End Function

    Private Shared Function 质量控制方式ToSelectedIndex(value As 预设数据_v6.视频全局质量控制方式) As Integer
        Select Case 标准化视频全局质量控制方式(value)
            Case 预设数据_v6.视频全局质量控制方式.CRF
                Return 1
            Case 预设数据_v6.视频全局质量控制方式.VBR
                Return 2
            Case 预设数据_v6.视频全局质量控制方式.CQP
                Return 3
            Case 预设数据_v6.视频全局质量控制方式.CBR
                Return 4
            Case 预设数据_v6.视频全局质量控制方式.TPE
                Return 5
            Case Else
                Return 0
        End Select
    End Function

    Private Shared Function TrackValue(track As Object) As String
        If track Is Nothing Then Return ""
        Dim p = track.GetType().GetProperty("Value")
        If p Is Nothing Then Return ""
        Dim v = p.GetValue(track)
        If v Is Nothing Then Return ""
        Return Convert.ToString(v, CultureInfo.InvariantCulture)
    End Function

    Private Shared Sub SetTrackValue(track As Object, value As String, Optional 默认值 As Double = 0)
        If track Is Nothing Then Exit Sub
        If String.IsNullOrWhiteSpace(value) Then
            ResetTrackValue(track, 默认值)
            Exit Sub
        End If
        Dim p = track.GetType().GetProperty("Value")
        If p Is Nothing Then Exit Sub
        Dim d As Double
        If Double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, d) OrElse Double.TryParse(value, d) Then p.SetValue(track, d)
    End Sub

    Private Shared Sub ResetTrackValue(track As Object, Optional 默认值 As Double = 0)
        If track Is Nothing Then Exit Sub
        Dim p = track.GetType().GetProperty("Value")
        If p Is Nothing Then Exit Sub
        p.SetValue(track, 默认值)
    End Sub
    Private Shared Sub 储存输出文件设置(a As 预设数据_v6, ui As Form_v6_参数面板)
        With ui.私有界面_输出文件设置
            a.输出容器 = .MTB_后缀.Text
            a.输出_输出文件参数使用方法 = SelectedIndexToEnum(Of 预设数据_v6.输出文件参数使用方法)(Math.Max(0, .MCB_输出文件参数使用方法.SelectedIndex))
            a.输出_自动命名选项 = SelectedIndexToEnum(Of 预设数据_v6.自动命名选项)(Math.Max(0, .MCB_自动命名方式.SelectedIndex))
            a.输出命名_开头文本 = .MTB_开头文本.Text
            a.输出命名_替代文本 = .MTB_替代文件名.Text
            a.输出命名_结尾文本 = .MTB_结尾文本.Text
            a.输出命名_保留创建时间 = .MCK_保留创建时间.Checked
            a.输出命名_保留修改时间 = .MCK_保留修改时间.Checked
            a.输出命名_保留访问时间 = .MCK_保留访问时间.Checked
            Dim 输出位置文本 = 规范化文件夹路径(.MCB_输出位置.Text)
            If Directory.Exists(输出位置文本) Then
                a.计算机名称 = Environment.MachineName
                a.输出位置 = 输出位置文本
                a.运行时使用输出位置 = True
                Dim 保留子文件夹结构起始点 = 规范化文件夹路径(.MCB_保留子文件夹结构起始点.Text)
                a.输出位置_保留子文件夹结构起始点 = If(Directory.Exists(保留子文件夹结构起始点), 保留子文件夹结构起始点, "")
            Else
                a.计算机名称 = ""
                a.输出位置 = ""
                a.输出位置_保留子文件夹结构起始点 = ""
                a.运行时使用输出位置 = False
            End If
        End With
    End Sub

    Private Shared Sub 显示输出文件设置(a As 预设数据_v6, ui As Form_v6_参数面板)
        With ui.私有界面_输出文件设置
            .MTB_后缀.Text = a.输出容器
            .MCB_输出文件参数使用方法.SelectedIndex = EnumToIndex(a.输出_输出文件参数使用方法)
            .MCB_自动命名方式.SelectedIndex = EnumToIndex(a.输出_自动命名选项)
            .MTB_开头文本.Text = a.输出命名_开头文本
            .MTB_替代文件名.Text = a.输出命名_替代文本
            .MTB_结尾文本.Text = a.输出命名_结尾文本
            .MCK_保留创建时间.Checked = a.输出命名_保留创建时间
            .MCK_保留修改时间.Checked = a.输出命名_保留修改时间
            .MCK_保留访问时间.Checked = a.输出命名_保留访问时间
            If 可使用预设输出位置(a) Then
                .设置自定义输出位置(a.输出位置)
                If Directory.Exists(If(a.输出位置_保留子文件夹结构起始点, "").Trim()) Then
                    .设置保留子文件夹结构起始点(a.输出位置_保留子文件夹结构起始点)
                Else
                    .清空保留子文件夹结构起始点()
                End If
            Else
                .MCB_输出位置.SelectedIndex = 0
                .清空保留子文件夹结构起始点()
            End If
        End With
    End Sub

    Private Shared Sub 储存解码参数(a As 预设数据_v6, ui As Form_v6_参数面板)
        With ui.私有界面_解码参数
            a.解码参数_解码器 = .MCB_硬件加速解码方式.Text
            a.解码参数_CPU解码线程数 = .MTB_CPU解码线程数.Text
            a.解码参数_解码数据格式 = .MCB_硬件解码输出格式.Text
            a.解码参数_指定硬件的参数名 = .MCB_硬件解码设备参数名.Text
            a.解码参数_指定硬件的参数 = .MTB_硬件解码设备参数值.Text
        End With
    End Sub

    Private Shared Sub 显示解码参数(a As 预设数据_v6, ui As Form_v6_参数面板)
        With ui.私有界面_解码参数
            .MCB_硬件加速解码方式.Text = a.解码参数_解码器
            .MTB_CPU解码线程数.Text = a.解码参数_CPU解码线程数
            .MCB_硬件解码输出格式.Text = a.解码参数_解码数据格式
            .MCB_硬件解码设备参数名.Text = a.解码参数_指定硬件的参数名
            .MTB_硬件解码设备参数值.Text = a.解码参数_指定硬件的参数
        End With
    End Sub

    Private Shared Sub 储存视频编码器(a As 预设数据_v6, ui As Form_v6_参数面板)
        With ui.私有界面_视频编码器
            a.视频参数_编码器_类型 = SelectedIndexToEnum(Of 预设数据_v6.视频编码器类型)(Math.Max(0, .MCB_视频编码器类型.SelectedIndex))
            a.视频参数_编码器_分类名称 = .MCB_视频编码器分类.Text
            a.视频参数_编码器_具体编码 = .MCB_具体编码器.Text
            a.视频参数_编码器_编码预设 = .MCB_编码预设.Text
            a.视频参数_编码器_配置文件 = .MCB_配置文件.Text
            a.视频参数_编码器_场景优化 = .MCB_场景优化.Text
            a.视频参数_编码器_gpu = .MTB_GPU编号.Text
            a.视频参数_编码器_threads = .MTB_编码线程数.Text
            a.视频参数_编码器_图片编码器质量值 = .MTB_图片编码器质量值.Text
        End With
    End Sub

    Private Shared Sub 显示视频编码器(a As 预设数据_v6, ui As Form_v6_参数面板)
        With ui.私有界面_视频编码器
            .MCB_视频编码器类型.SelectedIndex = EnumToIndex(a.视频参数_编码器_类型)
            Dim 分类已选中 = 设置组合框文本并尝试选中(.MCB_视频编码器分类, a.视频参数_编码器_分类名称)
            If 分类已选中 Then
                设置组合框文本并尝试选中(.MCB_具体编码器, a.视频参数_编码器_具体编码)
            Else
                .MCB_具体编码器.Text = a.视频参数_编码器_具体编码
            End If
            .刷新当前编码器参数列表()
            .MCB_编码预设.Text = a.视频参数_编码器_编码预设
            .MCB_配置文件.Text = a.视频参数_编码器_配置文件
            .MCB_场景优化.Text = a.视频参数_编码器_场景优化
            .MTB_GPU编号.Text = a.视频参数_编码器_gpu
            .MTB_编码线程数.Text = a.视频参数_编码器_threads
            .MTB_图片编码器质量值.Text = a.视频参数_编码器_图片编码器质量值
        End With
    End Sub

    Private Shared Function 设置组合框文本并尝试选中(combo As ModernComboBox, text As String) As Boolean
        If combo Is Nothing Then Return False
        text = If(text, "")
        For i = 0 To combo.Items.Count - 1
            Dim itemText = If(combo.Items(i), "").ToString()
            If String.Equals(itemText, text, StringComparison.Ordinal) Then
                combo.SelectedIndex = i
                Return True
            End If
        Next
        combo.Text = text
        Return False
    End Function

    Private Shared Sub 储存画面帧(a As 预设数据_v6, ui As Form_v6_参数面板)
        With ui.私有界面_画面帧
            a.视频参数_分辨率 = .MCB_直接指定分辨率.Text
            a.视频参数_分辨率自动计算_宽度 = .MCB_宽度缩放.Text
            a.视频参数_分辨率自动计算_高度 = .MCB_高度缩放.Text
            a.视频参数_帧速率 = .MCB_直接指定帧率.Text
            a.视频参数_分辨率_裁剪滤镜参数 = .MTB_画面裁剪参数.Text
            储存滤镜子窗口(a, .私有窗口_抽帧参数, .私有窗口_插帧参数, .私有窗口_动态模糊, .私有窗口_着色器超分, .私有窗口_降噪, .私有窗口_锐化, .私有窗口_胶片颗粒, .私有窗口_平滑断层, .私有窗口_扫描方式, .私有窗口_画面翻转, .私有窗口_烧录字幕)
        End With
    End Sub

    Private Shared Sub 显示画面帧(a As 预设数据_v6, ui As Form_v6_参数面板)
        With ui.私有界面_画面帧
            .MCB_直接指定分辨率.Text = a.视频参数_分辨率
            .MCB_宽度缩放.Text = a.视频参数_分辨率自动计算_宽度
            .MCB_高度缩放.Text = a.视频参数_分辨率自动计算_高度
            .MCB_直接指定帧率.Text = a.视频参数_帧速率
            .MTB_画面裁剪参数.Text = a.视频参数_分辨率_裁剪滤镜参数
            显示滤镜子窗口(a, .私有窗口_抽帧参数, .私有窗口_插帧参数, .私有窗口_动态模糊, .私有窗口_着色器超分, .私有窗口_降噪, .私有窗口_锐化, .私有窗口_胶片颗粒, .私有窗口_平滑断层, .私有窗口_扫描方式, .私有窗口_画面翻转, .私有窗口_烧录字幕)
        End With
    End Sub

    Private Shared Sub 储存滤镜子窗口(a As 预设数据_v6,
                                  抽帧 As Form_v6_参数面板_抽帧参数,
                                  插帧 As Form_v6_参数面板_插帧参数,
                                  动态模糊 As Form_v6_参数面板_动态模糊,
                                  超分 As Form_v6_参数面板_超分,
                                  降噪 As Form_v6_参数面板_降噪,
                                  锐化 As Form_v6_参数面板_锐化,
                                  胶片颗粒 As Form_v6_参数面板_胶片颗粒,
                                  平滑断层 As Form_v6_参数面板_平滑断层,
                                  扫描 As Form_v6_参数面板_扫描方式,
                                  翻转 As Form_v6_参数面板_画面翻转,
                                  烧字幕 As Form_v6_参数面板_烧录字幕)
        If 抽帧.MCK_抽帧总开关.Checked Then
            a.视频参数_抽帧_max = 抽帧.MTB_连续丢帧数量.Text
            a.视频参数_抽帧_keep = 抽帧.MTB_连续相似要求.Text
            a.视频参数_抽帧_hi = 抽帧.MCB_高阈值.Text
            a.视频参数_抽帧_lo = 抽帧.MCB_低阈值.Text
            a.视频参数_抽帧_frac = 抽帧.MTB_最大变化占比.Text
        Else
            a.视频参数_抽帧_max = ""
            a.视频参数_抽帧_keep = ""
            a.视频参数_抽帧_hi = ""
            a.视频参数_抽帧_lo = ""
            a.视频参数_抽帧_frac = ""
        End If

        a.视频参数_插帧_目标帧率 = If(插帧.MCK_插帧总开关.Checked, 插帧.MTB_目标帧率.Text, "")
        a.视频参数_插帧_插帧模式 = 插帧模式参数值(插帧.MCB_插帧模式.Text)
        a.视频参数_插帧_运动估计模式 = 运动估计模式参数值(插帧.MCB_运动估计模式.Text)
        a.视频参数_插帧_运动估计算法 = 运动估计算法参数值(插帧.MCB_运动估计算法.Text)
        a.视频参数_插帧_运动补偿模式 = 运动补偿模式参数值(插帧.MCB_运动补偿模式.Text)
        a.视频参数_插帧_可变块大小的运动补偿 = 插帧.MCK_可变块大小的运动补偿.Checked
        a.视频参数_插帧_块大小 = 插帧.MTB_块大小.Text
        a.视频参数_插帧_搜索范围 = 插帧.MTB_搜索范围.Text
        a.视频参数_插帧_场景变化检测强度 = 插帧.MTB_场景变化检测强度.Text

        a.视频参数_动态模糊_连续混合帧数 = If(动态模糊.MCK_动态模糊总开关.Checked, 动态模糊.MTB_连续混合帧数.Text, "")
        a.视频参数_动态模糊_每帧权重 = 动态模糊.MTB_每帧的权重.Text
        a.视频参数_动态模糊_输出缩放系数 = 动态模糊.MTB_输出缩放系数.Text
        a.视频参数_动态模糊_处理颜色平面 = 动态模糊.MTB_处理哪些颜色平面.Text

        If 超分.MCK_超分总开关.Checked Then
            a.视频参数_超分_直接面板 = New 预设数据_v6.超分数据单片结构 With {.目标宽度 = 超分.MTB_宽度.Text, .目标高度 = 超分.MTB_高度.Text, .上采样算法 = 超分.MCB_上采样算法.Text, .下采样算法 = 超分.MCB_下采样算法.Text, .抗振铃强度 = 超分.MTB_抗振铃强度.Text, .着色器文件路径 = 超分.MCB_着色器文件路径.Text}
            a.视频参数_超分_滤镜叠加策略组 = 超分.策略组数据.ToArray()
        Else
            a.视频参数_超分_直接面板 = New 预设数据_v6.超分数据单片结构
            a.视频参数_超分_滤镜叠加策略组 = Array.Empty(Of 预设数据_v6.超分数据单片结构)()
        End If

        a.视频参数_降噪_方式 = If(降噪.MCK_降噪总开关.Checked, SelectedIndexToEnum(Of 预设数据_v6.降噪方式)(Math.Max(0, 降噪.MCB_滤镜选择.SelectedIndex)), 预设数据_v6.降噪方式.未选择)
        a.视频参数_降噪_参数1 = TrackValue(降噪.ETB_降噪参数1)
        a.视频参数_降噪_参数2 = TrackValue(降噪.ETB_降噪参数2)
        a.视频参数_降噪_参数3 = TrackValue(降噪.ETB_降噪参数3)
        a.视频参数_降噪_参数4 = TrackValue(降噪.ETB_降噪参数4)

        a.视频参数_锐化_方式 = If(锐化.MCK_锐化总开关.Checked, SelectedIndexToEnum(Of 预设数据_v6.锐化方式)(Math.Max(0, 锐化.MCB_滤镜选择.SelectedIndex)), 预设数据_v6.锐化方式.未选择)
        a.视频参数_锐化_参数1 = TrackValue(锐化.ETB_锐化参数1)
        a.视频参数_锐化_参数2 = TrackValue(锐化.ETB_锐化参数2)
        a.视频参数_锐化_参数3 = TrackValue(锐化.ETB_锐化参数3)

        a.视频参数_胶片颗粒_方式 = If(胶片颗粒.MCK_胶片颗粒总开关.Checked, SelectedIndexToEnum(Of 预设数据_v6.胶片颗粒方式)(Math.Max(0, 胶片颗粒.MCB_滤镜选择.SelectedIndex)), 预设数据_v6.胶片颗粒方式.未选择)
        a.视频参数_胶片颗粒_参数1 = TrackValue(胶片颗粒.ETB_胶片颗粒参数1)
        a.视频参数_胶片颗粒_参数2 = TrackValue(胶片颗粒.ETB_胶片颗粒参数2)
        a.视频参数_胶片颗粒_参数3 = TrackValue(胶片颗粒.ETB_胶片颗粒参数3)
        a.视频参数_胶片颗粒_参数4 = TrackValue(胶片颗粒.ETB_胶片颗粒参数4)

        a.视频参数_平滑断层_方式 = If(平滑断层.MCK_平滑断层总开关.Checked, SelectedIndexToEnum(Of 预设数据_v6.平滑断层方式)(Math.Max(0, 平滑断层.MCB_滤镜选择.SelectedIndex)), 预设数据_v6.平滑断层方式.未选择)
        a.视频参数_平滑断层_参数1 = TrackValue(平滑断层.ETB_平滑断层参数1)
        a.视频参数_平滑断层_参数2 = TrackValue(平滑断层.ETB_平滑断层参数2)
        a.视频参数_平滑断层_参数3 = TrackValue(平滑断层.ETB_平滑断层参数3)
        a.视频参数_平滑断层_参数4 = TrackValue(平滑断层.ETB_平滑断层参数4)

        a.视频参数_处理扫描方式 = If(扫描.MCK_扫描方式总开关.Checked, SelectedIndexToEnum(Of 预设数据_v6.扫描方式)(Math.Max(0, 扫描.MCB_扫描方式.SelectedIndex)), 预设数据_v6.扫描方式.未选择)
        a.视频参数_画面翻转_角度翻转 = If(翻转.MCK_画面翻转总开关.Checked, SelectedIndexToEnum(Of 预设数据_v6.画面翻转角度)(Math.Max(0, 翻转.MCB_角度翻转.SelectedIndex)), 预设数据_v6.画面翻转角度.未选择)
        a.视频参数_画面翻转_镜像翻转 = If(翻转.MCK_画面翻转总开关.Checked, SelectedIndexToEnum(Of 预设数据_v6.画面翻转镜像)(Math.Max(0, 翻转.MCB_镜像翻转.SelectedIndex)), 预设数据_v6.画面翻转镜像.未选择)

        a.视频参数_烧录字幕_滤镜选择 = If(烧字幕.MCK_烧录字幕总开关.Checked, SelectedIndexToEnum(Of 预设数据_v6.烧字幕滤镜)(Math.Max(0, 烧字幕.MCB_滤镜选择.SelectedIndex)), 预设数据_v6.烧字幕滤镜.未选择)
        a.视频参数_烧录字幕_字幕来源是外部文件 = SelectedIndexToEnum(Of 预设数据_v6.烧字幕来源)(Math.Max(0, 烧字幕.MCB_字幕来源.SelectedIndex))
        a.视频参数_烧录字幕_字幕格式优先级 = New List(Of 预设数据_v6.烧字幕格式) From {
            SelectedIndexToEnum(Of 预设数据_v6.烧字幕格式)(Math.Max(0, 烧字幕.MCB_后缀优先级1.SelectedIndex)),
            SelectedIndexToEnum(Of 预设数据_v6.烧字幕格式)(Math.Max(0, 烧字幕.MCB_后缀优先级2.SelectedIndex)),
            SelectedIndexToEnum(Of 预设数据_v6.烧字幕格式)(Math.Max(0, 烧字幕.MCB_后缀优先级3.SelectedIndex))
        }
        a.视频参数_烧录字幕_外部字幕文件名 = 烧字幕.MTB_字幕文件多余字符.Text
        a.视频参数_烧录字幕_外部字幕文件夹位置 = 烧字幕.MCB_字幕文件路径.Text
        a.视频参数_烧录字幕_指定内嵌的流 = 烧字幕.MTB_内嵌的流索引.Text
        Select Case a.视频参数_烧录字幕_字幕来源是外部文件
            Case 预设数据_v6.烧字幕来源.外部字幕文件
                a.视频参数_烧录字幕_指定内嵌的流 = ""
            Case 预设数据_v6.烧字幕来源.内嵌的流
                a.视频参数_烧录字幕_外部字幕文件名 = ""
                a.视频参数_烧录字幕_外部字幕文件夹位置 = ""
            Case Else
                a.视频参数_烧录字幕_外部字幕文件名 = ""
                a.视频参数_烧录字幕_外部字幕文件夹位置 = ""
                a.视频参数_烧录字幕_指定内嵌的流 = ""
        End Select
        If 烧字幕.基本样式已设置 Then
            Dim f = 烧字幕.基本样式字体
            a.视频参数_烧录字幕_基本样式_名称 = f.Name
            a.视频参数_烧录字幕_基本样式_大小 = f.Size
            a.视频参数_烧录字幕_基本样式_粗体 = f.Bold
            a.视频参数_烧录字幕_基本样式_斜体 = f.Italic
            a.视频参数_烧录字幕_基本样式_下划线 = f.Underline
            a.视频参数_烧录字幕_基本样式_删除线 = f.Strikeout
        Else
            a.视频参数_烧录字幕_基本样式_名称 = ""
            a.视频参数_烧录字幕_基本样式_大小 = 0
            a.视频参数_烧录字幕_基本样式_粗体 = False
            a.视频参数_烧录字幕_基本样式_斜体 = False
            a.视频参数_烧录字幕_基本样式_下划线 = False
            a.视频参数_烧录字幕_基本样式_删除线 = False
        End If
        a.视频参数_烧录字幕_边框样式 = SelectedIndexToEnum(Of 预设数据_v6.烧字幕边框样式)(Math.Max(0, 烧字幕.MCB_边框类型.SelectedIndex))
        a.视频参数_烧录字幕_字体文件夹 = 烧字幕.MCB_字体文件夹路径.Text
        a.视频参数_烧录字幕_描边宽度 = 烧字幕.MTB_描边宽度.Text
        a.视频参数_烧录字幕_阴影距离 = 烧字幕.MTB_阴影距离.Text
        写入字幕颜色(a.视频参数_烧录字幕_主要颜色, 烧字幕.主要颜色已设置, 烧字幕.主要颜色)
        写入字幕颜色(a.视频参数_烧录字幕_次要颜色, 烧字幕.次要颜色已设置, 烧字幕.次要颜色)
        写入字幕颜色(a.视频参数_烧录字幕_描边颜色, 烧字幕.描边颜色已设置, 烧字幕.描边颜色)
        写入字幕颜色(a.视频参数_烧录字幕_背景颜色, 烧字幕.背景颜色已设置, 烧字幕.背景颜色)
        a.视频参数_烧录字幕_对齐方位 = SelectedIndexToEnum(Of 预设数据_v6.烧字幕对齐方位)(Math.Max(0, 烧字幕.MCB_对齐方位.SelectedIndex))
        a.视频参数_烧录字幕_垂直边距 = 烧字幕.MTB_垂直边距.Text
        a.视频参数_烧录字幕_左边距 = 烧字幕.MTB_左边距.Text
        a.视频参数_烧录字幕_右边距 = 烧字幕.MTB_右边距.Text
        a.视频参数_烧录字幕_字距 = 烧字幕.MTB_字距.Text
        a.视频参数_烧录字幕_行距 = 烧字幕.MTB_行距.Text
        a.视频参数_烧录字幕_补充样式 = 烧字幕.MTB_补充样式.Text
        a.视频参数_烧录字幕_自己写滤镜取代所有设置 = 烧字幕.MTB_自己写整个滤镜.Text
        If Not 烧字幕.MCK_烧录字幕总开关.Checked Then a.视频参数_烧录字幕_自己写滤镜取代所有设置 = ""
    End Sub

    Private Shared Sub 显示滤镜子窗口(a As 预设数据_v6,
                                  抽帧 As Form_v6_参数面板_抽帧参数,
                                  插帧 As Form_v6_参数面板_插帧参数,
                                  动态模糊 As Form_v6_参数面板_动态模糊,
                                  超分 As Form_v6_参数面板_超分,
                                  降噪 As Form_v6_参数面板_降噪,
                                  锐化 As Form_v6_参数面板_锐化,
                                  胶片颗粒 As Form_v6_参数面板_胶片颗粒,
                                  平滑断层 As Form_v6_参数面板_平滑断层,
                                  扫描 As Form_v6_参数面板_扫描方式,
                                  翻转 As Form_v6_参数面板_画面翻转,
                                  烧字幕 As Form_v6_参数面板_烧录字幕)
        抽帧.MCK_抽帧总开关.Checked = 抽帧参数已设置(a)
        抽帧.MTB_连续丢帧数量.Text = a.视频参数_抽帧_max
        抽帧.MTB_连续相似要求.Text = a.视频参数_抽帧_keep
        抽帧.MCB_高阈值.Text = a.视频参数_抽帧_hi
        抽帧.MCB_低阈值.Text = a.视频参数_抽帧_lo
        抽帧.MTB_最大变化占比.Text = a.视频参数_抽帧_frac

        插帧.MCK_插帧总开关.Checked = a.视频参数_插帧_目标帧率 <> ""
        插帧.MTB_目标帧率.Text = a.视频参数_插帧_目标帧率
        设置组合框文本并尝试选中(插帧.MCB_插帧模式, 插帧模式显示文本(a.视频参数_插帧_插帧模式))
        设置组合框文本并尝试选中(插帧.MCB_运动估计模式, 运动估计模式显示文本(a.视频参数_插帧_运动估计模式))
        设置组合框文本并尝试选中(插帧.MCB_运动估计算法, 运动估计算法显示文本(a.视频参数_插帧_运动估计算法))
        设置组合框文本并尝试选中(插帧.MCB_运动补偿模式, 运动补偿模式显示文本(a.视频参数_插帧_运动补偿模式))
        插帧.MCK_可变块大小的运动补偿.Checked = a.视频参数_插帧_可变块大小的运动补偿
        插帧.MTB_块大小.Text = a.视频参数_插帧_块大小
        插帧.MTB_搜索范围.Text = a.视频参数_插帧_搜索范围
        插帧.MTB_场景变化检测强度.Text = a.视频参数_插帧_场景变化检测强度

        动态模糊.MCK_动态模糊总开关.Checked = a.视频参数_动态模糊_连续混合帧数 <> ""
        动态模糊.MTB_连续混合帧数.Text = a.视频参数_动态模糊_连续混合帧数
        动态模糊.MTB_每帧的权重.Text = a.视频参数_动态模糊_每帧权重
        动态模糊.MTB_输出缩放系数.Text = a.视频参数_动态模糊_输出缩放系数
        动态模糊.MTB_处理哪些颜色平面.Text = a.视频参数_动态模糊_处理颜色平面

        超分.MCK_超分总开关.Checked = 超分单片有设置(a.视频参数_超分_直接面板) OrElse If(a.视频参数_超分_滤镜叠加策略组, Array.Empty(Of 预设数据_v6.超分数据单片结构)()).Length > 0
        If a.视频参数_超分_直接面板 IsNot Nothing Then
            超分.MTB_宽度.Text = a.视频参数_超分_直接面板.目标宽度
            超分.MTB_高度.Text = a.视频参数_超分_直接面板.目标高度
            超分.MCB_上采样算法.Text = a.视频参数_超分_直接面板.上采样算法
            超分.MCB_下采样算法.Text = a.视频参数_超分_直接面板.下采样算法
            超分.MTB_抗振铃强度.Text = a.视频参数_超分_直接面板.抗振铃强度
            超分.MCB_着色器文件路径.Text = a.视频参数_超分_直接面板.着色器文件路径
        End If
        超分.策略组数据 = If(a.视频参数_超分_滤镜叠加策略组, Array.Empty(Of 预设数据_v6.超分数据单片结构)()).ToList()
        超分.刷新策略组列表()

        降噪.MCK_降噪总开关.Checked = a.视频参数_降噪_方式 <> 预设数据_v6.降噪方式.未选择
        降噪.MCB_滤镜选择.SelectedIndex = EnumToIndex(a.视频参数_降噪_方式)
        SetTrackValue(降噪.ETB_降噪参数1, a.视频参数_降噪_参数1)
        SetTrackValue(降噪.ETB_降噪参数2, a.视频参数_降噪_参数2)
        SetTrackValue(降噪.ETB_降噪参数3, a.视频参数_降噪_参数3)
        SetTrackValue(降噪.ETB_降噪参数4, a.视频参数_降噪_参数4)

        锐化.MCK_锐化总开关.Checked = a.视频参数_锐化_方式 <> 预设数据_v6.锐化方式.未选择
        锐化.MCB_滤镜选择.SelectedIndex = EnumToIndex(a.视频参数_锐化_方式)
        SetTrackValue(锐化.ETB_锐化参数1, a.视频参数_锐化_参数1)
        SetTrackValue(锐化.ETB_锐化参数2, a.视频参数_锐化_参数2)
        SetTrackValue(锐化.ETB_锐化参数3, a.视频参数_锐化_参数3)

        胶片颗粒.MCK_胶片颗粒总开关.Checked = a.视频参数_胶片颗粒_方式 <> 预设数据_v6.胶片颗粒方式.未选择
        胶片颗粒.MCB_滤镜选择.SelectedIndex = EnumToIndex(a.视频参数_胶片颗粒_方式)
        SetTrackValue(胶片颗粒.ETB_胶片颗粒参数1, a.视频参数_胶片颗粒_参数1)
        SetTrackValue(胶片颗粒.ETB_胶片颗粒参数2, a.视频参数_胶片颗粒_参数2)
        SetTrackValue(胶片颗粒.ETB_胶片颗粒参数3, a.视频参数_胶片颗粒_参数3)
        SetTrackValue(胶片颗粒.ETB_胶片颗粒参数4, a.视频参数_胶片颗粒_参数4)

        平滑断层.MCK_平滑断层总开关.Checked = a.视频参数_平滑断层_方式 <> 预设数据_v6.平滑断层方式.未选择
        平滑断层.MCB_滤镜选择.SelectedIndex = EnumToIndex(a.视频参数_平滑断层_方式)
        SetTrackValue(平滑断层.ETB_平滑断层参数1, a.视频参数_平滑断层_参数1)
        SetTrackValue(平滑断层.ETB_平滑断层参数2, a.视频参数_平滑断层_参数2)
        SetTrackValue(平滑断层.ETB_平滑断层参数3, a.视频参数_平滑断层_参数3)
        SetTrackValue(平滑断层.ETB_平滑断层参数4, a.视频参数_平滑断层_参数4)

        扫描.MCK_扫描方式总开关.Checked = a.视频参数_处理扫描方式 <> 预设数据_v6.扫描方式.未选择
        扫描.MCB_扫描方式.SelectedIndex = EnumToIndex(a.视频参数_处理扫描方式)
        翻转.MCK_画面翻转总开关.Checked = a.视频参数_画面翻转_角度翻转 <> 预设数据_v6.画面翻转角度.未选择 OrElse a.视频参数_画面翻转_镜像翻转 <> 预设数据_v6.画面翻转镜像.未选择
        翻转.MCB_角度翻转.SelectedIndex = EnumToIndex(a.视频参数_画面翻转_角度翻转)
        翻转.MCB_镜像翻转.SelectedIndex = EnumToIndex(a.视频参数_画面翻转_镜像翻转)

        烧字幕.MCK_烧录字幕总开关.Checked = a.视频参数_烧录字幕_滤镜选择 <> 预设数据_v6.烧字幕滤镜.未选择 OrElse Not String.IsNullOrWhiteSpace(a.视频参数_烧录字幕_自己写滤镜取代所有设置)
        烧字幕.MCB_滤镜选择.SelectedIndex = EnumToIndex(a.视频参数_烧录字幕_滤镜选择)
        烧字幕.MCB_字幕来源.SelectedIndex = EnumToIndex(a.视频参数_烧录字幕_字幕来源是外部文件)
        Dim 字幕格式 = If(a.视频参数_烧录字幕_字幕格式优先级, New List(Of 预设数据_v6.烧字幕格式))
        烧字幕.MCB_后缀优先级1.SelectedIndex = If(字幕格式.Count > 0, EnumToIndex(字幕格式(0)), 0)
        烧字幕.MCB_后缀优先级2.SelectedIndex = If(字幕格式.Count > 1, EnumToIndex(字幕格式(1)), 0)
        烧字幕.MCB_后缀优先级3.SelectedIndex = If(字幕格式.Count > 2, EnumToIndex(字幕格式(2)), 0)
        烧字幕.MTB_字幕文件多余字符.Text = a.视频参数_烧录字幕_外部字幕文件名
        烧字幕.MCB_字幕文件路径.Text = a.视频参数_烧录字幕_外部字幕文件夹位置
        烧字幕.MTB_内嵌的流索引.Text = a.视频参数_烧录字幕_指定内嵌的流
        烧字幕.设置基本样式(a.视频参数_烧录字幕_基本样式_名称,
                         a.视频参数_烧录字幕_基本样式_大小,
                         a.视频参数_烧录字幕_基本样式_粗体,
                         a.视频参数_烧录字幕_基本样式_斜体,
                         a.视频参数_烧录字幕_基本样式_下划线,
                         a.视频参数_烧录字幕_基本样式_删除线)
        烧字幕.MCB_边框类型.SelectedIndex = EnumToIndex(a.视频参数_烧录字幕_边框样式)
        烧字幕.MCB_字体文件夹路径.Text = a.视频参数_烧录字幕_字体文件夹
        烧字幕.MTB_描边宽度.Text = a.视频参数_烧录字幕_描边宽度
        烧字幕.MTB_阴影距离.Text = a.视频参数_烧录字幕_阴影距离
        读取字幕颜色(a.视频参数_烧录字幕_主要颜色, AddressOf 烧字幕.设置主要颜色)
        读取字幕颜色(a.视频参数_烧录字幕_次要颜色, AddressOf 烧字幕.设置次要颜色)
        读取字幕颜色(a.视频参数_烧录字幕_描边颜色, AddressOf 烧字幕.设置描边颜色)
        读取字幕颜色(a.视频参数_烧录字幕_背景颜色, AddressOf 烧字幕.设置背景颜色)
        烧字幕.MCB_对齐方位.SelectedIndex = EnumToIndex(a.视频参数_烧录字幕_对齐方位)
        烧字幕.MTB_垂直边距.Text = a.视频参数_烧录字幕_垂直边距
        烧字幕.MTB_左边距.Text = a.视频参数_烧录字幕_左边距
        烧字幕.MTB_右边距.Text = a.视频参数_烧录字幕_右边距
        烧字幕.MTB_字距.Text = a.视频参数_烧录字幕_字距
        烧字幕.MTB_行距.Text = a.视频参数_烧录字幕_行距
        烧字幕.MTB_补充样式.Text = a.视频参数_烧录字幕_补充样式
        烧字幕.MTB_自己写整个滤镜.Text = a.视频参数_烧录字幕_自己写滤镜取代所有设置
    End Sub

    Private Shared Sub 储存质量(a As 预设数据_v6, ui As Form_v6_参数面板)
        With ui.私有界面_质量
            a.视频参数_比特率_控制方式 = 质量控制方式SelectedIndexToEnum(Math.Max(0, .MCB_全局质量控制方式.SelectedIndex))
            a.视频参数_质量控制_参数名 = .MCB_质量参数名称.Text.TrimStart("-"c)
            a.视频参数_质量控制_值 = .MTB_质量值.Text
            a.视频参数_比特率_基础 = .MTB_基础比特率.Text
            a.视频参数_比特率_最低值 = .MTB_最低比特率.Text
            a.视频参数_比特率_最高值 = .MTB_最高比特率.Text
            a.视频参数_比特率_缓冲区 = .MTB_缓冲区.Text
            a.视频参数_质量控制_进阶参数集 = .MTB_进阶质量控制参数.Text
        End With
    End Sub

    Private Shared Sub 显示质量(a As 预设数据_v6, ui As Form_v6_参数面板)
        With ui.私有界面_质量
            .MCB_全局质量控制方式.SelectedIndex = 质量控制方式ToSelectedIndex(a.视频参数_比特率_控制方式)
            .MCB_质量参数名称.Text = If(a.视频参数_质量控制_参数名 <> "" AndAlso Not a.视频参数_质量控制_参数名.StartsWith("-"c), "-" & a.视频参数_质量控制_参数名, a.视频参数_质量控制_参数名)
            .MTB_质量值.Text = a.视频参数_质量控制_值
            .MTB_基础比特率.Text = a.视频参数_比特率_基础
            .MTB_最低比特率.Text = a.视频参数_比特率_最低值
            .MTB_最高比特率.Text = a.视频参数_比特率_最高值
            .MTB_缓冲区.Text = a.视频参数_比特率_缓冲区
            .MTB_进阶质量控制参数.Text = a.视频参数_质量控制_进阶参数集
        End With
    End Sub

    Private Shared Sub 储存色彩管理(a As 预设数据_v6, ui As Form_v6_参数面板)
        With ui.私有界面_色彩管理
            a.视频参数_色彩管理_像素格式 = .MCB_像素格式.Text
            a.视频参数_色彩管理_像素格式预先转换 = .MCB_像素格式预先转换.Text
            a.视频参数_色彩管理_滤镜选择 = .MCB_色彩管理_选择滤镜.Text
            a.视频参数_色彩管理_矩阵系数 = .MCB_色彩管理_矩阵系数.Text
            a.视频参数_色彩管理_色域 = .MCB_色彩管理_色域.Text
            a.视频参数_色彩管理_传输特性 = .MCB_色彩管理_传输特性.Text
            a.视频参数_色彩管理_范围 = .MCB_色彩管理_色彩范围.Text
            a.视频参数_色彩管理_色调映射算法 = .MCB_色彩管理_色调映射算法.Text
            a.视频参数_色彩管理_处理方式 = .MCB_色彩管理_色彩空间操作方式.Text
            a.视频参数_色彩管理_启用调整亮度 = .MCK_启用亮度调整.Checked
            a.视频参数_色彩管理_亮度 = TrackValue(.ETB_亮度)
            a.视频参数_色彩管理_启用调整对比度 = .MCK_启用对比度调整.Checked
            a.视频参数_色彩管理_对比度 = TrackValue(.ETB_对比度)
            a.视频参数_色彩管理_启用调整饱和度 = .MCK_启用饱和度调整.Checked
            a.视频参数_色彩管理_饱和度 = TrackValue(.ETB_饱和度)
            a.视频参数_色彩管理_启用调整伽马 = .MCK_启用伽马调整.Checked
            a.视频参数_色彩管理_伽马 = TrackValue(.ETB_伽马)
        End With
    End Sub

    Private Shared Sub 显示色彩管理(a As 预设数据_v6, ui As Form_v6_参数面板)
        With ui.私有界面_色彩管理
            .MCB_像素格式.Text = a.视频参数_色彩管理_像素格式
            .MCB_像素格式预先转换.Text = a.视频参数_色彩管理_像素格式预先转换
            .MCB_色彩管理_选择滤镜.Text = a.视频参数_色彩管理_滤镜选择
            .MCB_色彩管理_矩阵系数.Text = a.视频参数_色彩管理_矩阵系数
            .MCB_色彩管理_色域.Text = a.视频参数_色彩管理_色域
            .MCB_色彩管理_传输特性.Text = a.视频参数_色彩管理_传输特性
            .MCB_色彩管理_色彩范围.Text = a.视频参数_色彩管理_范围
            .MCB_色彩管理_色调映射算法.Text = a.视频参数_色彩管理_色调映射算法
            .MCB_色彩管理_色彩空间操作方式.Text = a.视频参数_色彩管理_处理方式
            .MCK_启用亮度调整.Checked = a.视频参数_色彩管理_启用调整亮度
            SetTrackValue(.ETB_亮度, a.视频参数_色彩管理_亮度)
            .MCK_启用对比度调整.Checked = a.视频参数_色彩管理_启用调整对比度
            SetTrackValue(.ETB_对比度, a.视频参数_色彩管理_对比度, 1)
            .MCK_启用饱和度调整.Checked = a.视频参数_色彩管理_启用调整饱和度
            SetTrackValue(.ETB_饱和度, a.视频参数_色彩管理_饱和度, 1)
            .MCK_启用伽马调整.Checked = a.视频参数_色彩管理_启用调整伽马
            SetTrackValue(.ETB_伽马, a.视频参数_色彩管理_伽马, 1)
        End With
    End Sub

    Private Shared Sub 储存音频参数(a As 预设数据_v6, ui As Form_v6_参数面板)
        With ui.私有界面_音频参数
            a.音频参数_编码器_代号 = 音频编码器数据库_v6.获取私有ID(.MCB_音频编码器.Text)
            If a.音频参数_编码器_代号 = "" Then a.音频参数_编码器_代号 = .MCB_音频编码器.Text
            a.音频参数_比特率 = .MCB_比特率.Text
            a.音频参数_质量参数名 = .MCB_质量参数名.Text
            a.音频参数_质量值 = .MTB_质量值.Text
            a.音频参数_质量参数名2 = .MCB_质量参数名2.Text
            a.音频参数_质量值2 = .MTB_质量值2.Text
            a.音频参数_声道数 = .MCB_声道布局.Text
            a.音频参数_位深度 = .MCB_位深度.Text
            a.音频参数_采样率 = .MCB_采样率.Text
            a.音频参数_响度标准化_启用调整目标响度 = .MCK_启用目标响度.Checked
            a.音频参数_响度标准化_目标响度 = TrackValue(.ETB_目标响度)
            a.音频参数_响度标准化_启用调整动态范围 = .MCK_启用动态范围.Checked
            a.音频参数_响度标准化_动态范围 = TrackValue(.ETB_动态范围)
            a.音频参数_响度标准化_启用调整峰值电平 = .MCK_启用峰值电平.Checked
            a.音频参数_响度标准化_峰值电平 = TrackValue(.ETB_峰值电平)
        End With
    End Sub

    Private Shared Sub 显示音频参数(a As 预设数据_v6, ui As Form_v6_参数面板)
        With ui.私有界面_音频参数
            Dim 音频编码器显示名称 = 音频编码器数据库_v6.获取显示名称(a.音频参数_编码器_代号)
            If 音频编码器显示名称 = "" Then 音频编码器显示名称 = a.音频参数_编码器_代号
            设置组合框文本并尝试选中(.MCB_音频编码器, 音频编码器显示名称)
            .MCB_比特率.Text = a.音频参数_比特率
            设置组合框文本并尝试选中(.MCB_质量参数名, a.音频参数_质量参数名)
            .MTB_质量值.Text = a.音频参数_质量值
            设置组合框文本并尝试选中(.MCB_质量参数名2, a.音频参数_质量参数名2)
            .MTB_质量值2.Text = a.音频参数_质量值2
            .MCB_声道布局.Text = a.音频参数_声道数
            .MCB_位深度.Text = a.音频参数_位深度
            .MCB_采样率.Text = a.音频参数_采样率
            .MCK_启用目标响度.Checked = a.音频参数_响度标准化_启用调整目标响度
            SetTrackValue(.ETB_目标响度, a.音频参数_响度标准化_目标响度, -24)
            .MCK_启用动态范围.Checked = a.音频参数_响度标准化_启用调整动态范围
            SetTrackValue(.ETB_动态范围, a.音频参数_响度标准化_动态范围, 1)
            .MCK_启用峰值电平.Checked = a.音频参数_响度标准化_启用调整峰值电平
            SetTrackValue(.ETB_峰值电平, a.音频参数_响度标准化_峰值电平, -1)
        End With
    End Sub

    Private Shared Sub 储存剪辑(a As 预设数据_v6, ui As Form_v6_参数面板)
        With ui.私有界面_剪辑区间
            a.剪辑区间_方法 = SelectedIndexToEnum(Of 预设数据_v6.剪辑方法)(Math.Max(0, .MCB_剪辑模式.SelectedIndex))
            a.剪辑区间_入点 = .MTB_入点.Text
            a.剪辑区间_出点 = .MTB_出点.Text
            a.剪辑区间_向前解码多久秒 = .MCB_向前解码秒数.Text
        End With
    End Sub

    Private Shared Sub 显示剪辑(a As 预设数据_v6, ui As Form_v6_参数面板)
        With ui.私有界面_剪辑区间
            .MCB_剪辑模式.SelectedIndex = EnumToIndex(a.剪辑区间_方法)
            .MTB_入点.Text = a.剪辑区间_入点
            .MTB_出点.Text = a.剪辑区间_出点
            .MCB_向前解码秒数.Text = a.剪辑区间_向前解码多久秒
        End With
    End Sub

    Private Shared Sub 储存自定义参数(a As 预设数据_v6, ui As Form_v6_参数面板)
        a.自定义参数_视频参数 = ui.私有界面_流自定义参数.MTB_视频流自定义参数.Text
        a.自定义参数_音频参数 = ui.私有界面_流自定义参数.MTB_音频流自定义参数.Text
        a.自定义参数_开头参数 = ui.私有界面_在位置插入参数.MTB_开头参数.Text
        a.自定义参数_之前参数 = ui.私有界面_在位置插入参数.MTB_之前参数.Text
        a.自定义参数_之后参数 = ui.私有界面_在位置插入参数.MTB_之后参数.Text
        a.自定义参数_最后参数 = ui.私有界面_在位置插入参数.MTB_最后参数.Text
        a.自定义参数_完全自己写 = ui.私有界面_完全自己写模式.MTB_完整命令行参数.Text
    End Sub

    Private Shared Sub 显示自定义参数(a As 预设数据_v6, ui As Form_v6_参数面板)
        ui.私有界面_流自定义参数.MTB_视频流自定义参数.Text = a.自定义参数_视频参数
        ui.私有界面_流自定义参数.MTB_音频流自定义参数.Text = a.自定义参数_音频参数
        ui.私有界面_在位置插入参数.MTB_开头参数.Text = a.自定义参数_开头参数
        ui.私有界面_在位置插入参数.MTB_之前参数.Text = a.自定义参数_之前参数
        ui.私有界面_在位置插入参数.MTB_之后参数.Text = a.自定义参数_之后参数
        ui.私有界面_在位置插入参数.MTB_最后参数.Text = a.自定义参数_最后参数
        ui.私有界面_完全自己写模式.MTB_完整命令行参数.Text = a.自定义参数_完全自己写
    End Sub

    Private Shared Sub 储存视频帧服务器(a As 预设数据_v6, ui As Form_v6_参数面板)
        With ui.私有界面_视频帧服务器
            a.视频参数_视频帧服务器_使用AviSynth = .BS_使用AviSynth.Checked AndAlso .MCB_AviSynth脚本文件.Text.Trim() <> ""
            a.视频参数_视频帧服务器_avs脚本文件 = .MCB_AviSynth脚本文件.Text.Trim()
            a.视频参数_视频帧服务器_使用VapourSynth = .BS_使用VapourSynth.Checked AndAlso .MCB_VapourSynth脚本文件.Text.Trim() <> ""
            a.视频参数_视频帧服务器_vpy脚本文件 = .MCB_VapourSynth脚本文件.Text.Trim()
            If a.视频参数_视频帧服务器_使用AviSynth Then a.视频参数_视频帧服务器_使用VapourSynth = False
        End With
    End Sub

    Private Shared Sub 显示视频帧服务器(a As 预设数据_v6, ui As Form_v6_参数面板)
        With ui.私有界面_视频帧服务器
            .BS_使用AviSynth.Checked = a.视频参数_视频帧服务器_使用AviSynth
            .MCB_AviSynth脚本文件.Text = a.视频参数_视频帧服务器_avs脚本文件
            .BS_使用VapourSynth.Checked = a.视频参数_视频帧服务器_使用VapourSynth AndAlso Not a.视频参数_视频帧服务器_使用AviSynth
            .MCB_VapourSynth脚本文件.Text = a.视频参数_视频帧服务器_vpy脚本文件
        End With
    End Sub

    Private Shared Sub 储存附加内容(a As 预设数据_v6, ui As Form_v6_参数面板)
        a.元数据_要写入的信息 = ui.私有界面_元数据.获取数据().ToArray()
        With ui.私有界面_章节
            a.章节_来源 = SelectedIndexToEnum(Of 预设数据_v6.章节来源)(Math.Max(0, .MCB_章节来源.SelectedIndex))
            a.章节_文件路径 = .MCB_章节文件.Text.Trim()
        End With
        a.附件_要写入的附件 = ui.私有界面_附件.获取数据().ToArray()
    End Sub

    Private Shared Sub 显示附加内容(a As 预设数据_v6, ui As Form_v6_参数面板)
        ui.私有界面_元数据.设置数据(a.元数据_要写入的信息)
        With ui.私有界面_章节
            .MCB_章节来源.SelectedIndex = EnumToIndex(a.章节_来源)
            .MCB_章节文件.Text = a.章节_文件路径
        End With
        ui.私有界面_附件.设置数据(a.附件_要写入的附件)
    End Sub

    Private Shared Sub 储存流控制(a As 预设数据_v6, ui As Form_v6_参数面板)
        With ui.私有界面_流控制
            a.流控制_将视频参数应用于指定流 = SplitTextList(.MTB_视频流选择.Text)
            a.流控制_启用保留其他视频流 = .MCK_保留其他视频流.Checked
            a.流控制_将音频参数应用于指定流 = SplitTextList(.MTB_音频流选择.Text)
            a.流控制_启用保留其他音频流 = .MCK_保留其他音频流.Checked
            a.流控制_将字幕参数应用于指定流 = SplitTextList(.MTB_字幕流选择.Text)
            a.流控制_如何操作指定的字幕 = Math.Max(0, .MCB_字幕流操作.SelectedIndex)
            a.流控制_启用保留其他字幕流 = .MCK_保留其他字幕流.Checked
            a.流控制_自动混流SRT = .MCK_混流同名SRT字幕.Checked
            a.流控制_自动混流ASS = .MCK_混流同名ASS字幕.Checked
            a.流控制_自动混流SSA = .MCK_混流同名SSA字幕.Checked
            a.流控制_自动混流的字幕转为MOVTEXT = .MCK_字幕转为mov_text.Checked
            a.流控制_元数据选项 = Math.Max(0, .MCB_元数据选项.SelectedIndex)
            a.流控制_章节选项 = Math.Max(0, .MCB_章节选项.SelectedIndex)
            a.流控制_附件选项 = Math.Max(0, .MCB_附件选项.SelectedIndex)
        End With
    End Sub

    Private Shared Sub 显示流控制(a As 预设数据_v6, ui As Form_v6_参数面板)
        With ui.私有界面_流控制
            .MTB_视频流选择.Text = String.Join(",", a.流控制_将视频参数应用于指定流)
            .MCK_保留其他视频流.Checked = a.流控制_启用保留其他视频流
            .MTB_音频流选择.Text = String.Join(",", a.流控制_将音频参数应用于指定流)
            .MCK_保留其他音频流.Checked = a.流控制_启用保留其他音频流
            .MTB_字幕流选择.Text = String.Join(",", a.流控制_将字幕参数应用于指定流)
            .MCB_字幕流操作.SelectedIndex = a.流控制_如何操作指定的字幕
            .MCK_保留其他字幕流.Checked = a.流控制_启用保留其他字幕流
            .MCK_混流同名SRT字幕.Checked = a.流控制_自动混流SRT
            .MCK_混流同名ASS字幕.Checked = a.流控制_自动混流ASS
            .MCK_混流同名SSA字幕.Checked = a.流控制_自动混流SSA
            .MCK_字幕转为mov_text.Checked = a.流控制_自动混流的字幕转为MOVTEXT
            .MCB_元数据选项.SelectedIndex = a.流控制_元数据选项
            .MCB_章节选项.SelectedIndex = a.流控制_章节选项
            .MCB_附件选项.SelectedIndex = a.流控制_附件选项
        End With
    End Sub

End Class
