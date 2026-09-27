Imports System.Globalization
Imports System.IO
Imports System.Text
Imports System.Text.Json
Imports System.Text.RegularExpressions
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
        Select Case 标识符
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.NV_FRUC : Return "NVIDIA Vulkan FRUC"
        End Select
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

    Private Shared Function 生成解码参数(a As 预设数据_v6) As String
        Dim parts As New List(Of String)
        If a.解码参数_解码器 <> "" Then parts.Add($"-hwaccel {a.解码参数_解码器}")
        If a.解码参数_CPU解码线程数 <> "" Then parts.Add($"-threads {a.解码参数_CPU解码线程数}")
        If a.解码参数_解码数据格式 <> "" Then parts.Add($"-hwaccel_output_format {a.解码参数_解码数据格式}")
        If a.解码参数_指定硬件的参数名 <> "" AndAlso a.解码参数_指定硬件的参数 <> "" Then parts.Add($"{规范FFmpeg参数名(a.解码参数_指定硬件的参数名)} {a.解码参数_指定硬件的参数}")
        Return String.Join(" ", parts)
    End Function

    Private Shared Function 生成主输入参数(a As 预设数据_v6, 输入文件 As String, 帧服务器脚本后缀 As String) As List(Of String)
        Dim parts As New List(Of String)
        If a.视频参数_视频帧服务器_使用AviSynth AndAlso a.视频参数_视频帧服务器_avs脚本文件.Trim() <> "" Then
            parts.Add("-i")
            parts.Add(Q(应用转译模式路径(派生脚本路径(输入文件, ".avs", 帧服务器脚本后缀))))
        ElseIf a.视频参数_视频帧服务器_使用VapourSynth AndAlso a.视频参数_视频帧服务器_vpy脚本文件.Trim() <> "" Then
            parts.Add("-f vapoursynth")
            parts.Add("-i")
            parts.Add(Q(应用转译模式路径(派生脚本路径(输入文件, Path.GetExtension(a.视频参数_视频帧服务器_vpy脚本文件), 帧服务器脚本后缀))))
        Else
            parts.Add("-i")
            parts.Add(Q(应用转译模式路径(输入文件)))
        End If
        Return parts
    End Function

    Public Shared Function 派生帧服务器脚本路径(输入文件 As String, ext As String, Optional 后缀 As String = "") As String
        Return 派生脚本路径(输入文件, ext, 后缀)
    End Function

    Private Shared Function 派生脚本路径(输入文件 As String, ext As String, 后缀 As String) As String
        Dim safeSuffix = 清理二次编码日志文件名(If(后缀, "").Trim())
        Dim suffixPart = If(safeSuffix = "", "", "." & safeSuffix)
        Dim finalExt = If(String.IsNullOrWhiteSpace(ext), ".vpy", ext)
        If 输入文件 = 输入占位符 OrElse 输入文件 = "<InputFile>" Then
            Return "<InputFileWithOutExtension>" & suffixPart & finalExt
        End If
        Dim stem = 获取路径不含扩展名保持分隔符(输入文件)
        If String.IsNullOrWhiteSpace(stem) Then Return ""
        Return stem & suffixPart & finalExt
    End Function

    Private Shared Function 生成编码参数(a As 预设数据_v6, 阶段 As 预设数据_v6.命令行阶段, 视频选择器 As List(Of String), 音频选择器 As List(Of String), 请求视频输出 As Boolean, 请求音频输出 As Boolean, 视频来自滤镜 As Boolean, 音频来自滤镜 As Boolean, 输入文件 As String, 输出文件 As String) As String
        Dim parts As New List(Of String)
        Dim 编码器 = 视频编码器数据库_v6.获取编码器数据(a.视频参数_编码器_具体编码)
        If 编码器 IsNot Nothing AndAlso 编码器.命令行编码器名 <> "" Then
            If 编码器.是否禁用 Then
                parts.Add("-vn")
            Else
                添加按流视频编码参数(parts, 编码器, If(视频选择器, New List(Of String)), 视频来自滤镜)
            End If
        End If
        If 请求视频输出 Then 添加按流视频附加参数(parts, a, 阶段, If(视频选择器, New List(Of String)), 视频来自滤镜)
        AddRaw(parts, 应用自定义参数通配字符串(a.自定义参数_视频参数, 输入文件, 输出文件))

        Dim 音频 = 音频编码器数据库_v6.获取编码器数据(a.音频参数_编码器_代号)
        If 音频 IsNot Nothing AndAlso 阶段 <> 预设数据_v6.命令行阶段.二次编码第一遍 Then
            添加按流音频编码参数(parts, 音频, If(音频选择器, New List(Of String)), 音频来自滤镜)
        End If
        If 请求音频输出 AndAlso 阶段 <> 预设数据_v6.命令行阶段.二次编码第一遍 Then
            添加按流音频附加参数(parts, a, 音频, If(音频选择器, New List(Of String)), 音频来自滤镜)
        End If
        AddRaw(parts, 应用自定义参数通配字符串(a.自定义参数_音频参数, 输入文件, 输出文件))

        If 阶段 = 预设数据_v6.命令行阶段.二次编码第一遍 OrElse 阶段 = 预设数据_v6.命令行阶段.二次编码第二遍 Then
            parts.Add(If(阶段 = 预设数据_v6.命令行阶段.二次编码第一遍, "-pass 1", "-pass 2"))
            parts.Add("-passlogfile " & Q(生成二次编码日志路径(输入文件, 输出文件)))
        End If

        Return String.Join(" ", parts.Where(Function(x) x <> ""))
    End Function

    Private Shared Sub 添加按流视频编码参数(parts As List(Of String), 编码器 As 视频编码器数据库_v6.视频编码器数据, 选择器 As List(Of String), 来自滤镜 As Boolean)
        Dim targets = 规范输出流选择器列表(选择器)
        If targets.Count = 0 Then
            parts.Add($"-c:v {编码器.命令行编码器名}")
            Exit Sub
        End If
        If 编码器.是否复制流 AndAlso 来自滤镜 Then Exit Sub
        For Each target In targets
            parts.Add($"-c:{target} {编码器.命令行编码器名}")
        Next
    End Sub

    Private Shared Sub 添加按流视频附加参数(parts As List(Of String), a As 预设数据_v6, 阶段 As 预设数据_v6.命令行阶段, 选择器 As List(Of String), 来自滤镜 As Boolean)
        Dim targets = 规范输出流选择器列表(选择器, True)
        Dim 编码器 = 视频编码器数据库_v6.获取编码器数据(a.视频参数_编码器_具体编码)
        If 编码器 IsNot Nothing AndAlso 编码器.是否禁用 Then Exit Sub
        Dim 选择复制流编码器 = 编码器 IsNot Nothing AndAlso 编码器.是否复制流
        For Each target In targets
            If 编码器 IsNot Nothing AndAlso Not 选择复制流编码器 Then
                添加编码器参数(parts, 编码器.编码预设.参数名, a.视频参数_编码器_编码预设, target)
                添加编码器参数(parts, 编码器.配置文件.参数名, a.视频参数_编码器_配置文件, target)
                添加编码器参数(parts, 编码器.场景优化.参数名, a.视频参数_编码器_场景优化, target)
                添加编码器参数(parts, 编码器.图片质量.参数名, a.视频参数_编码器_图片编码器质量值, target)
            End If
            If Not 选择复制流编码器 Then
                添加编码器参数(parts, "-r", a.视频参数_帧速率, target)
                添加编码器参数(parts, "-fps_mode", 标准化帧率模式(a.视频参数_帧速率模式), target)
            End If
            If Not 选择复制流编码器 Then 添加编码器参数(parts, "-pix_fmt", a.视频参数_色彩管理_像素格式, target)
            parts.AddRange(生成色彩元数据参数(a, target))
            If Not 选择复制流编码器 Then
                添加编码器参数(parts, "-gpu", a.视频参数_编码器_gpu, target)
                添加编码器参数(parts, "-threads", a.视频参数_编码器_threads, target)
                parts.AddRange(生成质量参数(a, 阶段, target))
            End If
        Next
    End Sub

    Private Shared Function 标准化帧率模式(value As String) As String
        Select Case If(value, "").Trim().ToLowerInvariant()
            Case "cfr", "固定帧率 cfr"
                Return "cfr"
            Case "vfr", "动态帧率 vfr"
                Return "vfr"
            Case Else
                Return ""
        End Select
    End Function

    Private Shared Sub 添加按流音频编码参数(parts As List(Of String), 音频 As 音频编码器数据库_v6.音频编码器数据, 选择器 As List(Of String), 来自滤镜 As Boolean)
        If 音频.是否禁用 Then
            parts.Add("-an")
            Exit Sub
        End If
        If 音频.是否复制流 AndAlso 来自滤镜 Then Exit Sub
        Dim targets = 规范输出流选择器列表(选择器, True)
        For Each target In targets
            If 音频.命令行编码器名 <> "" Then
                parts.Add($"{应用输出流选择器("-c:a", target)} {音频.命令行编码器名}")
            End If
            If 音频.是否复制流 Then Continue For
            For Each 单项参数 In 音频.默认附加参数列表
                添加音频编码器默认参数(parts, 单项参数, target)
            Next
        Next
    End Sub

    Private Shared Sub 添加按流音频附加参数(parts As List(Of String), a As 预设数据_v6, 音频 As 音频编码器数据库_v6.音频编码器数据, 选择器 As List(Of String), 来自滤镜 As Boolean)
        If 音频 IsNot Nothing AndAlso 音频.是否禁用 Then Exit Sub
        Dim 选择复制流编码器 = 音频 IsNot Nothing AndAlso 音频.是否复制流
        Dim targets = 规范输出流选择器列表(选择器, True)
        For Each target In targets
            If 选择复制流编码器 Then Continue For
            添加编码器参数(parts, "-b:a", a.音频参数_比特率, target)
            添加编码器参数(parts, a.音频参数_质量参数名, a.音频参数_质量值, target)
            添加编码器参数(parts, a.音频参数_质量参数名2, a.音频参数_质量值2, target)
            添加编码器参数(parts, "-sample_fmt", a.音频参数_位深度, target)
            添加编码器参数(parts, "-ar", a.音频参数_采样率, target)
        Next
    End Sub

    Private Shared Sub 添加编码器参数(parts As List(Of String), 参数名 As String, 值 As String)
        添加编码器参数(parts, 参数名, 值, "")
    End Sub

    Private Shared Sub 添加编码器参数(parts As List(Of String), 参数名 As String, 值 As String, 输出流选择器 As String)
        If String.IsNullOrWhiteSpace(参数名) OrElse String.IsNullOrWhiteSpace(值) Then Exit Sub
        parts.Add($"{应用输出流选择器(规范FFmpeg参数名(参数名), 输出流选择器)} {值}")
    End Sub

    Private Shared Function 规范FFmpeg参数名(参数名 As String) As String
        Dim value = If(参数名, "").Trim()
        If value = "" Then Return ""
        If value.StartsWith("-"c, StringComparison.Ordinal) Then Return value
        Return "-" & value
    End Function

    Private Shared Sub 添加音频编码器默认参数(parts As List(Of String), 单项参数 As 音频编码器数据库_v6.音频编码器参数数据, 输出流选择器 As String)
        If 单项参数 Is Nothing OrElse String.IsNullOrWhiteSpace(单项参数.参数名) Then Exit Sub
        Dim 参数名 = 应用输出流选择器(单项参数.参数名, 输出流选择器)
        If String.IsNullOrWhiteSpace(单项参数.默认值) Then
            parts.Add(参数名)
        Else
            parts.Add($"{参数名} {单项参数.默认值}")
        End If
    End Sub

    Private Shared Function 规范输出流选择器列表(选择器 As List(Of String), Optional 空列表表示全局 As Boolean = False) As List(Of String)
        If 选择器 Is Nothing OrElse 选择器.Count = 0 Then
            If 空列表表示全局 Then Return New List(Of String) From {""}
            Return New List(Of String)
        End If
        Return 选择器.Where(Function(x) Not String.IsNullOrWhiteSpace(x)).Distinct(StringComparer.OrdinalIgnoreCase).ToList()
    End Function

    Private Shared Function 应用输出流选择器(参数名 As String, 输出流选择器 As String) As String
        If String.IsNullOrWhiteSpace(参数名) Then Return ""
        Dim p = 规范FFmpeg参数名(参数名)
        If String.IsNullOrWhiteSpace(输出流选择器) Then Return p
        If Not p.StartsWith("-"c, StringComparison.Ordinal) Then Return p
        Dim firstSpace = p.IndexOf(" "c)
        Dim head = If(firstSpace >= 0, p.Substring(0, firstSpace), p)
        Dim tail = If(firstSpace >= 0, p.Substring(firstSpace), "")
        Dim colon = head.IndexOf(":"c)
        If colon >= 0 Then
            Return String.Concat(head.AsSpan(0, colon), ":", 输出流选择器, tail)
        End If
        Return head & ":" & 输出流选择器 & tail
    End Function


End Class
