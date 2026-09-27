Imports System.Globalization
Imports System.IO
Imports System.Text
Imports System.Text.Json
Imports System.Text.RegularExpressions
Imports LakeUI

Partial Public Class 预设管理_v6

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
            If .当前编码器需要清空其他参数() Then .清空其他音频参数()
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
            If .当前编码器需要清空其他参数() Then
                .清空其他音频参数()
                Exit Sub
            End If
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
    Private Shared Function 生成质量参数(a As 预设数据_v6, 阶段 As 预设数据_v6.命令行阶段, Optional 输出流选择器 As String = "") As List(Of String)
        Dim parts As New List(Of String)
        Dim 控制方式 = 标准化视频全局质量控制方式(a.视频参数_比特率_控制方式)
        Select Case 控制方式
            Case 预设数据_v6.视频全局质量控制方式.未选择
                添加视频质量参数(parts, a, 输出流选择器)
            Case 预设数据_v6.视频全局质量控制方式.CRF
                添加编码器参数(parts, 获取视频质量参数名(a.视频参数_质量控制_参数名, "crf"), a.视频参数_质量控制_值, 输出流选择器)
            Case 预设数据_v6.视频全局质量控制方式.CQP
                If 是AMF编码器(a.视频参数_编码器_具体编码) Then
                    添加AMFCQP码率控制参数(parts, a, 输出流选择器)
                Else
                    添加NVENC码率控制参数(parts, a, 控制方式, 输出流选择器)
                    添加编码器参数(parts, 获取视频质量参数名(a.视频参数_质量控制_参数名, "qp"), a.视频参数_质量控制_值, 输出流选择器)
                End If
            Case 预设数据_v6.视频全局质量控制方式.VBR, 预设数据_v6.视频全局质量控制方式.CBR
                添加NVENC码率控制参数(parts, a, 控制方式, 输出流选择器)
                添加视频质量参数(parts, a, 输出流选择器)
            Case 预设数据_v6.视频全局质量控制方式.TPE
                If 阶段 <> 预设数据_v6.命令行阶段.二次编码第一遍 AndAlso 阶段 <> 预设数据_v6.命令行阶段.二次编码第二遍 AndAlso Not 二次编码基础码率有效(a) Then
                    添加视频质量参数(parts, a, 输出流选择器)
                End If
        End Select
        ' 四个码率输入值独立于质量控制方式，非空时都应写入命令行。
        添加编码器参数(parts, "-b:v", a.视频参数_比特率_基础, 输出流选择器)
        添加编码器参数(parts, "-minrate", a.视频参数_比特率_最低值, 输出流选择器)
        添加编码器参数(parts, "-maxrate", a.视频参数_比特率_最高值, 输出流选择器)
        添加编码器参数(parts, "-bufsize", a.视频参数_比特率_缓冲区, 输出流选择器)
        AddRaw(parts, a.视频参数_质量控制_进阶参数集)
        Return parts
    End Function

    Private Shared Sub 添加NVENC码率控制参数(parts As List(Of String), a As 预设数据_v6, 控制方式 As 预设数据_v6.视频全局质量控制方式, 输出流选择器 As String)
        If Not 是NVENC编码器(a.视频参数_编码器_具体编码) Then Exit Sub
        If 已显式设置码率控制(a.视频参数_质量控制_进阶参数集) Then Exit Sub

        Select Case 控制方式
            Case 预设数据_v6.视频全局质量控制方式.CQP
                添加编码器参数(parts, "-rc", "constqp", 输出流选择器)
            Case 预设数据_v6.视频全局质量控制方式.VBR
                添加编码器参数(parts, "-rc", "vbr", 输出流选择器)
            Case 预设数据_v6.视频全局质量控制方式.CBR
                添加编码器参数(parts, "-rc", "cbr", 输出流选择器)
        End Select
    End Sub

    Private Shared Sub 添加AMFCQP码率控制参数(parts As List(Of String), a As 预设数据_v6, 输出流选择器 As String)
        If Not 已显式设置码率控制(a.视频参数_质量控制_进阶参数集) Then
            添加编码器参数(parts, "-rc", "cqp", 输出流选择器)
        End If
    End Sub

    Private Shared Function 是NVENC编码器(编码器 As String) As Boolean
        Dim value = If(编码器, "").Trim()
        Return value.EndsWith("_nvenc", StringComparison.OrdinalIgnoreCase)
    End Function

    Private Shared Function 是AMF编码器(编码器 As String) As Boolean
        Dim value = If(编码器, "").Trim()
        Return value.EndsWith("_amf", StringComparison.OrdinalIgnoreCase)
    End Function

    Private Shared Function 已显式设置码率控制(value As String) As Boolean
        Return Regex.IsMatch(If(value, ""), "(^|\s)-rc(?::\S+)?(?=\s|$)", RegexOptions.IgnoreCase)
    End Function

    Private Shared Sub 添加视频质量参数(parts As List(Of String), a As 预设数据_v6, 输出流选择器 As String)
        If String.IsNullOrWhiteSpace(a.视频参数_质量控制_参数名) OrElse String.IsNullOrWhiteSpace(a.视频参数_质量控制_值) Then Exit Sub
        添加编码器参数(parts, 获取视频质量参数名(a.视频参数_质量控制_参数名, ""), a.视频参数_质量控制_值, 输出流选择器)
    End Sub

    Private Shared Function 获取视频质量参数名(参数名 As String, 默认名 As String) As String
        Dim value = If(参数名, "").Trim()
        If value = "" Then value = 默认名
        Return 规范FFmpeg参数名(value)
    End Function

    Private Shared Function 标准化视频全局质量控制方式(value As 预设数据_v6.视频全局质量控制方式) As 预设数据_v6.视频全局质量控制方式
        Dim raw = Convert.ToInt32(value, CultureInfo.InvariantCulture)
        If raw = 3 Then Return 预设数据_v6.视频全局质量控制方式.VBR
        If [Enum].IsDefined(value) Then Return value
        Return 预设数据_v6.视频全局质量控制方式.未选择
    End Function

    Private Shared Function 生成色彩元数据参数(a As 预设数据_v6, 输出流选择器 As String) As List(Of String)
        Dim parts As New List(Of String)
        If Not 色彩元数据已设置(a) Then Return parts
        添加编码器参数(parts, "-colorspace", 标准化色彩值(a.视频参数_色彩管理_矩阵系数, False), 输出流选择器)
        添加编码器参数(parts, "-color_primaries", 标准化色彩值(a.视频参数_色彩管理_色域, False), 输出流选择器)
        添加编码器参数(parts, "-color_trc", 标准化色彩值(a.视频参数_色彩管理_传输特性, False), 输出流选择器)
        添加编码器参数(parts, "-color_range", 标准化色彩范围(a.视频参数_色彩管理_范围, False), 输出流选择器)
        Return parts
    End Function

    Private Shared Function 色彩元数据已设置(a As 预设数据_v6) As Boolean
        If a Is Nothing Then Return False
        Dim mode = If(a.视频参数_色彩管理_处理方式, "").Trim()
        If Not String.Equals(mode, "写入元数据并转换", StringComparison.Ordinal) AndAlso
           Not String.Equals(mode, "仅写入元数据", StringComparison.Ordinal) Then Return False
        Return 标准化色彩值(a.视频参数_色彩管理_矩阵系数, False) <> "" OrElse
               标准化色彩值(a.视频参数_色彩管理_色域, False) <> "" OrElse
               标准化色彩值(a.视频参数_色彩管理_传输特性, False) <> "" OrElse
               标准化色彩范围(a.视频参数_色彩管理_范围, False) <> ""
    End Function

    Private Shared Function 色彩转换滤镜已设置(a As 预设数据_v6) As Boolean
        Return 构造色彩转换滤镜(a) <> ""
    End Function

    Private Shared Function 标准化色彩值(value As String, 允许Auto As Boolean) As String
        Dim raw = If(value, "").Trim()
        If raw = "" Then Return ""
        If String.Equals(raw, "auto", StringComparison.OrdinalIgnoreCase) Then Return If(允许Auto, "auto", "")
        Return raw
    End Function

    Private Shared Function 标准化色彩范围(value As String, 允许Auto As Boolean) As String
        Dim raw = 标准化色彩值(value, 允许Auto)
        If raw = "" Then Return ""
        If raw.StartsWith("tv", StringComparison.OrdinalIgnoreCase) OrElse raw.Contains("有限", StringComparison.Ordinal) OrElse raw.Contains("limited", StringComparison.OrdinalIgnoreCase) Then Return "tv"
        If raw.StartsWith("pc", StringComparison.OrdinalIgnoreCase) OrElse raw.Contains("全范围", StringComparison.Ordinal) OrElse raw.Contains("full", StringComparison.OrdinalIgnoreCase) Then Return "pc"
        Return raw
    End Function

    Private Shared Function 可以生成二次编码(a As 预设数据_v6) As Boolean
        Return a IsNot Nothing AndAlso
               a.视频参数_比特率_控制方式 = 预设数据_v6.视频全局质量控制方式.TPE AndAlso
               二次编码基础码率有效(a) AndAlso
               Not 二次编码明显不兼容(a)
    End Function

    Private Shared Function 二次编码明显不兼容(a As 预设数据_v6) As Boolean
        If a Is Nothing Then Return False
        If a.视频参数_编码器_类型 = 预设数据_v6.视频编码器类型.图片 Then Return True
        Dim 编码器 = 视频编码器数据库_v6.获取编码器数据(If(a.视频参数_编码器_具体编码, ""))
        Return 编码器 IsNot Nothing AndAlso (编码器.是否复制流 OrElse 编码器.是否禁用)
    End Function

    Private Shared Function 二次编码基础码率有效(a As 预设数据_v6) As Boolean
        Return a IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(a.视频参数_比特率_基础)
    End Function

    Private Shared Function 全局质量参数已设置(a As 预设数据_v6) As Boolean
        Return a IsNot Nothing AndAlso
               (Not String.IsNullOrWhiteSpace(a.视频参数_质量控制_参数名) OrElse
                Not String.IsNullOrWhiteSpace(a.视频参数_质量控制_值))
    End Function

    Private Shared Function 视频输出附加参数已设置(a As 预设数据_v6) As Boolean
        If a Is Nothing Then Return False
        Return Not String.IsNullOrWhiteSpace(a.视频参数_编码器_编码预设) OrElse
               Not String.IsNullOrWhiteSpace(a.视频参数_编码器_配置文件) OrElse
               Not String.IsNullOrWhiteSpace(a.视频参数_编码器_场景优化) OrElse
               Not String.IsNullOrWhiteSpace(a.视频参数_编码器_图片编码器质量值) OrElse
               Not String.IsNullOrWhiteSpace(a.视频参数_帧速率) OrElse
               Not String.IsNullOrWhiteSpace(标准化帧率模式(a.视频参数_帧速率模式)) OrElse
               Not String.IsNullOrWhiteSpace(a.视频参数_色彩管理_像素格式) OrElse
               色彩元数据已设置(a) OrElse
               Not String.IsNullOrWhiteSpace(a.视频参数_编码器_gpu) OrElse
               Not String.IsNullOrWhiteSpace(a.视频参数_编码器_threads) OrElse
               a.视频参数_比特率_控制方式 <> 预设数据_v6.视频全局质量控制方式.未选择 OrElse
               Not String.IsNullOrWhiteSpace(a.视频参数_比特率_基础) OrElse
               Not String.IsNullOrWhiteSpace(a.视频参数_比特率_最低值) OrElse
               Not String.IsNullOrWhiteSpace(a.视频参数_比特率_最高值) OrElse
               Not String.IsNullOrWhiteSpace(a.视频参数_比特率_缓冲区) OrElse
               全局质量参数已设置(a) OrElse
               Not String.IsNullOrWhiteSpace(a.视频参数_质量控制_进阶参数集)
    End Function

    Private Shared Function 音频输出附加参数已设置(a As 预设数据_v6) As Boolean
        If a Is Nothing Then Return False
        Return Not String.IsNullOrWhiteSpace(a.音频参数_比特率) OrElse
               Not String.IsNullOrWhiteSpace(a.音频参数_质量参数名) OrElse
               Not String.IsNullOrWhiteSpace(a.音频参数_质量值) OrElse
               Not String.IsNullOrWhiteSpace(a.音频参数_质量参数名2) OrElse
               Not String.IsNullOrWhiteSpace(a.音频参数_质量值2) OrElse
               Not String.IsNullOrWhiteSpace(a.音频参数_位深度) OrElse
               Not String.IsNullOrWhiteSpace(a.音频参数_采样率)
    End Function

    Private Shared Function 构造色彩转换滤镜(a As 预设数据_v6) As String
        If a Is Nothing Then Return ""
        If String.Equals(If(a.视频参数_色彩管理_处理方式, "").Trim(), "仅写入元数据", StringComparison.Ordinal) Then Return ""
        Dim filterName = 获取色彩转换滤镜名(a)
        Dim opts As New List(Of String)
        Dim 矩阵系数 = 标准化色彩滤镜矩阵值(a.视频参数_色彩管理_矩阵系数, filterName)
        Dim 色域 = 标准化色彩值(a.视频参数_色彩管理_色域, False)
        Dim 传输特性 = 标准化色彩值(a.视频参数_色彩管理_传输特性, False)
        Dim 色彩范围 = 标准化色彩范围(a.视频参数_色彩管理_范围, False)
        Dim 色调映射算法 = 标准化色彩值(a.视频参数_色彩管理_色调映射算法, False)

        Select Case filterName.ToLowerInvariant()
            Case "colorspace"
                If 矩阵系数 <> "" Then opts.Add("space=" & 矩阵系数)
                If 色域 <> "" Then opts.Add("primaries=" & 色域)
                If 传输特性 <> "" Then opts.Add("trc=" & 传输特性)
                If 色彩范围 <> "" Then opts.Add("range=" & 色彩范围)
            Case "zscale"
                If 矩阵系数 <> "" Then opts.Add("matrix=" & 矩阵系数)
                If 色域 <> "" Then opts.Add("primaries=" & 色域)
                If 传输特性 <> "" Then opts.Add("transfer=" & 传输特性)
                If 色彩范围 <> "" Then opts.Add("range=" & 色彩范围)
            Case "libplacebo"
                If 矩阵系数 <> "" Then opts.Add("colorspace=" & 矩阵系数)
                If 色域 <> "" Then opts.Add("color_primaries=" & 色域)
                If 传输特性 <> "" Then opts.Add("color_trc=" & 传输特性)
                If 色彩范围 <> "" Then opts.Add("range=" & 色彩范围)
                If 色调映射算法 <> "" Then opts.Add("tonemapping=" & 色调映射算法)
            Case Else
                If 矩阵系数 <> "" Then opts.Add("matrix=" & 矩阵系数)
                If 色域 <> "" Then opts.Add("primaries=" & 色域)
                If 传输特性 <> "" Then opts.Add("transfer=" & 传输特性)
                If 色彩范围 <> "" Then opts.Add("range=" & 色彩范围)
        End Select
        If opts.Count = 0 Then Return ""
        Return filterName & "=" & String.Join(":", opts)
    End Function

    Private Shared Function 获取色彩转换滤镜名(a As 预设数据_v6) As String
        Dim filterName = If(a?.视频参数_色彩管理_滤镜选择, "").Trim()
        If filterName <> "" Then Return filterName
        If 标准化色彩值(a?.视频参数_色彩管理_色域, False) <> "" OrElse
           标准化色彩值(a?.视频参数_色彩管理_传输特性, False) <> "" OrElse
           标准化色彩范围(a?.视频参数_色彩管理_范围, False) <> "" OrElse
           标准化色彩值(a?.视频参数_色彩管理_色调映射算法, False) <> "" Then Return "libplacebo"
        If 标准化色彩值(a?.视频参数_色彩管理_矩阵系数, False) <> "" Then Return "colorspace"
        Return "libplacebo"
    End Function

    Private Shared Function 标准化色彩滤镜矩阵值(value As String, filterName As String) As String
        Dim raw = 标准化色彩值(value, False)
        If raw = "" Then Return ""
        If String.Equals(raw, "rgb", StringComparison.OrdinalIgnoreCase) Then Return "gbr"
        Return raw
    End Function

    Private Shared Function 构造像素格式预先转换滤镜(a As 预设数据_v6) As String
        Dim value = If(a.视频参数_色彩管理_像素格式预先转换, "").Trim()
        Return If(value <> "", "format=" & value, "")
    End Function

    Private Shared Function 构造调色滤镜(a As 预设数据_v6) As String
        Dim opts As New List(Of String)
        If a.视频参数_色彩管理_启用调整亮度 AndAlso a.视频参数_色彩管理_亮度 <> "" Then opts.Add("brightness=" & a.视频参数_色彩管理_亮度)
        If a.视频参数_色彩管理_启用调整对比度 AndAlso a.视频参数_色彩管理_对比度 <> "" Then opts.Add("contrast=" & a.视频参数_色彩管理_对比度)
        If a.视频参数_色彩管理_启用调整饱和度 AndAlso a.视频参数_色彩管理_饱和度 <> "" Then opts.Add("saturation=" & a.视频参数_色彩管理_饱和度)
        If a.视频参数_色彩管理_启用调整伽马 AndAlso a.视频参数_色彩管理_伽马 <> "" Then opts.Add("gamma=" & a.视频参数_色彩管理_伽马)
        Return If(opts.Count > 0, "eq=" & String.Join(":", opts), "")
    End Function

    Private Shared Function 构造响度滤镜(a As 预设数据_v6) As String
        Dim opts As New List(Of String)
        If a.音频参数_响度标准化_启用调整目标响度 AndAlso a.音频参数_响度标准化_目标响度 <> "" Then opts.Add("I=" & a.音频参数_响度标准化_目标响度)
        If a.音频参数_响度标准化_启用调整动态范围 AndAlso a.音频参数_响度标准化_动态范围 <> "" Then opts.Add("LRA=" & a.音频参数_响度标准化_动态范围)
        If a.音频参数_响度标准化_启用调整峰值电平 AndAlso a.音频参数_响度标准化_峰值电平 <> "" Then opts.Add("TP=" & a.音频参数_响度标准化_峰值电平)
        Return If(opts.Count > 0, "loudnorm=" & String.Join(":", opts), "")
    End Function


End Class
