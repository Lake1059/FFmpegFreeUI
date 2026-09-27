Imports System.Globalization
Imports System.IO
Imports System.Text
Imports System.Text.Json
Imports LakeUI

Partial Public Class 预设管理_v6

    Private Shared Sub 添加音频质量控制总览(sb As StringBuilder, 标题 As String, 参数名 As String, 参数值 As String)
        If Not String.IsNullOrWhiteSpace(参数名) Then
            添加总览文本行(sb, 标题 & "：" & 参数名 & If(参数值 = "", "（未填写值）", "=" & 参数值))
        ElseIf Not String.IsNullOrWhiteSpace(参数值) Then
            添加总览文本行(sb, 标题.Replace("控制", "值") & "：" & 参数值)
        End If
    End Sub

    Private Shared Sub 添加总览文本行(sb As StringBuilder, 文本 As String)
        If sb Is Nothing OrElse String.IsNullOrWhiteSpace(文本) Then Exit Sub
        文本 = 文本.Trim()
        If 文本.EndsWith("："c) OrElse 文本.EndsWith("="c) Then Exit Sub
        sb.AppendLine(文本)
    End Sub

    Private Shared Sub 设置参数总览文本(MTB As ModernTextBox, 文本 As String)
        If MTB Is Nothing Then Exit Sub
        MTB.Clear()
        Dim 内容 = If(文本, "").Trim()
        If 内容 = "" Then 内容 = "未设置参数"
        For Each line In 内容.Replace(vbCrLf, vbLf).Replace(vbCr, vbLf).Split({vbLf}, StringSplitOptions.None)
            MTB.AppendLine(line, 获取参数总览行颜色(line))
        Next
    End Sub

    Private Shared Function 获取参数总览行颜色(文本 As String) As Color
        Dim line = If(文本, "")
        If line.StartsWith("警告：", StringComparison.Ordinal) Then Return 界面配色_v6.错误文本色
        If line.Contains("没有指定输出容器", StringComparison.Ordinal) OrElse line.Contains("没有指定输出后缀", StringComparison.Ordinal) Then Return 界面配色_v6.错误文本色
        If line.Contains("没有指定 AviSynth 模板", StringComparison.Ordinal) OrElse line.Contains("没有指定 VapourSynth 模板", StringComparison.Ordinal) Then Return 界面配色_v6.错误文本色
        If line.Contains("必须指定解码硬件的参数", StringComparison.Ordinal) Then Return 界面配色_v6.错误文本色
        If line.Contains("要出事", StringComparison.Ordinal) Then Return 界面配色_v6.错误文本色
        Return Color.Empty
    End Function

    Private Shared Function 已设置(ParamArray 值() As String) As Boolean
        Return 值 IsNot Nothing AndAlso 值.Any(Function(x) Not String.IsNullOrWhiteSpace(x))
    End Function

    Private Shared Function 抽帧参数已设置(a As 预设数据_v6) As Boolean
        If a Is Nothing Then Return False
        Return 已设置(a.视频参数_抽帧_max, a.视频参数_抽帧_keep, a.视频参数_抽帧_hi, a.视频参数_抽帧_lo, a.视频参数_抽帧_frac)
    End Function

    Private Shared Function 格式化枚举名称(value As [Enum]) As String
        If value Is Nothing Then Return ""
        Return value.ToString().Replace("_", " ")
    End Function

    Private Shared Function 格式化质量控制方式(value As 预设数据_v6.视频全局质量控制方式) As String
        Select Case 标准化视频全局质量控制方式(value)
            Case 预设数据_v6.视频全局质量控制方式.CRF : Return "CRF"
            Case 预设数据_v6.视频全局质量控制方式.VBR : Return "VBR"
            Case 预设数据_v6.视频全局质量控制方式.CQP : Return "CQP"
            Case 预设数据_v6.视频全局质量控制方式.CBR : Return "CBR"
            Case 预设数据_v6.视频全局质量控制方式.TPE : Return "TPE"
        End Select
        Return ""
    End Function

    Private Shared Function 格式化剪辑方法(value As 预设数据_v6.剪辑方法) As String
        Select Case value
            Case 预设数据_v6.剪辑方法.粗剪 : Return "粗剪 (立即响应)"
            Case 预设数据_v6.剪辑方法.精剪从头解码 : Return "精剪 (从头解码)"
            Case 预设数据_v6.剪辑方法.精剪空降解码 : Return "精剪 (快速响应)"
            Case 预设数据_v6.剪辑方法.Trim滤镜 : Return "Trim 滤镜"
            Case 预设数据_v6.剪辑方法.掐头去尾 : Return "掐头去尾"
            Case 预设数据_v6.剪辑方法.剔除中间 : Return "剔除中间"
        End Select
        Return ""
    End Function

    Private Shared Function 获取音频编码器总览显示名(私有ID As String) As String
        Dim 文本 = If(私有ID, "").Trim()
        If 文本 = "" Then Return ""
        Dim 显示名 = 音频编码器数据库_v6.获取显示名称(文本)
        If 显示名 <> "" Then Return 显示名
        Return 文本
    End Function

    Private Shared Function 超分单片有设置(单片 As 预设数据_v6.超分数据单片结构) As Boolean
        If 单片 Is Nothing Then Return False
        Return 已设置(单片.目标宽度, 单片.目标高度, 单片.上采样算法, 单片.下采样算法, 单片.抗振铃强度, 单片.着色器文件路径)
    End Function

    Private Shared Function 格式化超分单片(单片 As 预设数据_v6.超分数据单片结构) As String
        If 单片 Is Nothing Then Return ""
        Dim 片段 As New List(Of String)
        If 单片.目标宽度 <> "" Then 片段.Add("目标宽度：" & 单片.目标宽度)
        If 单片.目标高度 <> "" Then 片段.Add("目标高度：" & 单片.目标高度)
        If 单片.上采样算法 <> "" Then 片段.Add("上采样算法：" & 单片.上采样算法)
        If 单片.下采样算法 <> "" Then 片段.Add("下采样算法：" & 单片.下采样算法)
        If 单片.抗振铃强度 <> "" Then 片段.Add("抗振铃强度：" & 单片.抗振铃强度)
        If 单片.着色器文件路径 <> "" Then 片段.Add("着色器：" & 单片.着色器文件路径)
        Return String.Join("；", 片段)
    End Function

    Private Shared Sub 添加字幕颜色总览(列表 As List(Of String), 名称 As String, 颜色 As 预设数据_v6.烧字幕专用颜色类型)
        If 列表 Is Nothing OrElse 颜色 Is Nothing Then Exit Sub
        If Not 字幕颜色已设置(颜色) Then Exit Sub
        列表.Add($"{名称}：&H{颜色.A:X2}{颜色.B:X2}{颜色.G:X2}{颜色.R:X2}")
    End Sub

    Private Shared Function 字幕颜色已设置(颜色 As 预设数据_v6.烧字幕专用颜色类型) As Boolean
        If 颜色 Is Nothing Then Return False
        Return 颜色.已设置 OrElse 颜色.A <> 255 OrElse 颜色.R <> 0 OrElse 颜色.G <> 0 OrElse 颜色.B <> 0
    End Function

    Private Shared Sub 写入字幕颜色(目标 As 预设数据_v6.烧字幕专用颜色类型, 已设置 As Boolean, 颜色 As Color)
        If 目标 Is Nothing Then Exit Sub
        目标.已设置 = 已设置
        If 已设置 Then
            目标.A = 颜色.A
            目标.R = 颜色.R
            目标.G = 颜色.G
            目标.B = 颜色.B
        Else
            目标.A = 255
            目标.R = 0
            目标.G = 0
            目标.B = 0
        End If
    End Sub

    Private Shared Sub 读取字幕颜色(来源 As 预设数据_v6.烧字幕专用颜色类型, 设置动作 As Action(Of Color, Boolean))
        If 设置动作 Is Nothing Then Exit Sub
        If Not 字幕颜色已设置(来源) Then
            设置动作.Invoke(Color.Black, False)
            Exit Sub
        End If
        设置动作.Invoke(Color.FromArgb(限制颜色通道(来源.A), 限制颜色通道(来源.R), 限制颜色通道(来源.G), 限制颜色通道(来源.B)), True)
    End Sub

    Private Shared Function 限制颜色通道(value As Integer) As Integer
        Return Math.Min(255, Math.Max(0, value))
    End Function

    Private Shared Function 格式化字符串数组(值 As String()) As String
        If 值 Is Nothing Then Return ""
        Return String.Join(",", 值.Select(Function(x) If(x, "").Trim()).Where(Function(x) x <> ""))
    End Function

    Private Shared Function 格式化字幕流操作(value As 预设数据_v6.流控制字幕操作) As String
        Select Case value
            Case 预设数据_v6.流控制字幕操作.复制流 : Return "复制流"
            Case 预设数据_v6.流控制字幕操作.转为_mov_text : Return "转为 mov_text 编码"
            Case 预设数据_v6.流控制字幕操作.转为_srt : Return "转为 srt 编码"
            Case 预设数据_v6.流控制字幕操作.转为_ass : Return "转为 ass 编码"
            Case 预设数据_v6.流控制字幕操作.转为_ssa : Return "转为 ssa 编码"
        End Select
        Return 格式化枚举名称(value)
    End Function

    Private Shared Function 格式化元数据选项(value As 预设数据_v6.流控制元数据选项) As String
        Select Case value
            Case 预设数据_v6.流控制元数据选项.保留元数据 : Return "保留元数据"
            Case 预设数据_v6.流控制元数据选项.清除元数据 : Return "清除元数据"
            Case 预设数据_v6.流控制元数据选项.保留更多元数据 : Return "保留更多元数据"
        End Select
        Return 格式化枚举名称(value)
    End Function

    Private Shared Function 格式化章节选项(value As 预设数据_v6.流控制章节选项) As String
        Select Case value
            Case 预设数据_v6.流控制章节选项.保留章节 : Return "保留章节"
            Case 预设数据_v6.流控制章节选项.清除章节 : Return "清除章节"
        End Select
        Return 格式化枚举名称(value)
    End Function

    Private Shared Function 格式化附件选项(value As 预设数据_v6.流控制附件选项) As String
        Select Case value
            Case 预设数据_v6.流控制附件选项.保留附件 : Return "保留附件"
            Case 预设数据_v6.流控制附件选项.清除附件 : Return "清除附件"
        End Select
        Return 格式化枚举名称(value)
    End Function

    Private Shared Function 格式化元数据总览(列表 As 预设数据_v6.元数据单片结构()) As String
        If 列表 Is Nothing Then Return ""
        Dim 片段 As New List(Of String)
        For Each item In 列表
            If item Is Nothing Then Continue For
            If String.IsNullOrWhiteSpace(item.字段) AndAlso String.IsNullOrWhiteSpace(item.值) Then Continue For
            If String.IsNullOrWhiteSpace(item.字段) Then
                片段.Add(item.值)
            ElseIf String.IsNullOrWhiteSpace(item.值) Then
                片段.Add(item.字段)
            Else
                片段.Add($"{item.字段}={item.值}")
            End If
        Next
        Return String.Join("；", 片段)
    End Function

    Private Shared Function 格式化附件总览(列表 As 预设数据_v6.附件单片结构()) As String
        If 列表 Is Nothing Then Return ""
        Dim 片段 As New List(Of String)
        For Each item In 列表
            If item Is Nothing Then Continue For
            If item.类型 = 预设数据_v6.附件单片结构.附件类型.未选择 AndAlso String.IsNullOrWhiteSpace(item.文件路径) Then Continue For
            Dim 类型 = If(item.类型 = 预设数据_v6.附件单片结构.附件类型.未选择, "", 格式化枚举名称(item.类型))
            If 类型 <> "" AndAlso item.文件路径 <> "" Then
                片段.Add($"{类型}：{item.文件路径}")
            ElseIf 类型 <> "" Then
                片段.Add(类型)
            Else
                片段.Add(item.文件路径)
            End If
        Next
        Return String.Join("；", 片段)
    End Function

    Private Shared Sub 添加附件容器兼容性总览(sb As StringBuilder, a As 预设数据_v6)
        If a Is Nothing OrElse String.IsNullOrWhiteSpace(a.输出容器) Then Exit Sub

        Dim 支持常规附件 = 输出容器支持附件(a, 输出占位符)
        Dim 支持附加封面图 = 输出容器支持附加封面图(a, 输出占位符)
        If a.流控制_附件选项 = 预设数据_v6.流控制附件选项.保留附件 AndAlso Not 支持常规附件 Then
            添加总览文本行(sb, "警告：当前输出容器不支持常规附件，保留附件不会写入命令")
        End If

        Dim 列表 = If(a.附件_要写入的附件, Array.Empty(Of 预设数据_v6.附件单片结构)())
        If 列表.Any(Function(x) x IsNot Nothing AndAlso x.类型 = 预设数据_v6.附件单片结构.附件类型.MP4封面图 AndAlso Not String.IsNullOrWhiteSpace(x.文件路径)) AndAlso Not 支持附加封面图 Then
            添加总览文本行(sb, "警告：当前输出容器不支持 attached_pic 封面图，已配置的 MP4 封面图会被跳过")
        End If
        If a.流控制_启用保留其他视频流 AndAlso 列表.Any(Function(x) x IsNot Nothing AndAlso x.类型 = 预设数据_v6.附件单片结构.附件类型.MP4封面图 AndAlso Not String.IsNullOrWhiteSpace(x.文件路径)) Then
            添加总览文本行(sb, "警告：保留其他视频流时无法静态确定 attached_pic 封面图的输出流序号，已配置的 MP4 封面图会被跳过")
        End If
        If 列表.Any(Function(x) 附件项是常规附件(x)) AndAlso Not 支持常规附件 Then
            添加总览文本行(sb, "警告：当前输出容器不支持 -attach 附件，已配置的图片、字体或文档附件会被跳过")
        End If
    End Sub

    Private Shared Function 附件项是常规附件(item As 预设数据_v6.附件单片结构) As Boolean
        If item Is Nothing OrElse String.IsNullOrWhiteSpace(item.文件路径) Then Return False
        Select Case item.类型
            Case 预设数据_v6.附件单片结构.附件类型.MKV封面图,
                 预设数据_v6.附件单片结构.附件类型.图片,
                 预设数据_v6.附件单片结构.附件类型.字体文件,
                 预设数据_v6.附件单片结构.附件类型.文本文档
                Return True
            Case Else
                Return False
        End Select
    End Function

    Private Shared Sub 添加字幕容器兼容性总览(sb As StringBuilder, a As 预设数据_v6)
        If a Is Nothing OrElse String.IsNullOrWhiteSpace(a.输出容器) Then Exit Sub

        Dim ext = 获取输出容器扩展名(a, 输出占位符)
        Dim 自动混流 = a.流控制_自动混流SRT OrElse a.流控制_自动混流ASS OrElse a.流控制_自动混流SSA
        If 自动混流 Then
            If 输出容器支持MovText字幕(a, 输出占位符) AndAlso Not a.流控制_自动混流的字幕转为MOVTEXT Then
                添加总览文本行(sb, "提示：当前输出容器使用 mov_text 字幕，自动混流字幕不会直接复制为 SRT/ASS/SSA")
            ElseIf 输出容器支持WebVtt字幕(a, 输出占位符) Then
                添加总览文本行(sb, "提示：当前输出容器使用 webvtt 字幕，自动混流字幕会转为 webvtt")
            ElseIf a.流控制_自动混流的字幕转为MOVTEXT AndAlso Not 输出容器支持MovText字幕(a, 输出占位符) Then
                添加总览文本行(sb, "警告：当前输出容器不支持 mov_text，自动混流字幕不会按 mov_text 写入")
            End If
        End If

        Dim 请求字幕编码 = 获取字幕编码参数(a.流控制_如何操作指定的字幕)
        If 请求字幕编码 = "copy" AndAlso 字幕复制需要源流兼容(a, 输出占位符) Then
            添加总览文本行(sb, $"警告：当前输出容器 {ext} 复制字幕要求源字幕本身已兼容容器，否则 FFmpeg 可能封装失败")
        ElseIf 请求字幕编码 <> "" AndAlso 请求字幕编码 <> "copy" Then
            Dim 实际字幕编码 = 获取容器兼容字幕编码(请求字幕编码, a, 输出占位符, False)
            If 实际字幕编码 <> 请求字幕编码 Then
                添加总览文本行(sb, $"警告：当前输出容器 {ext} 不支持 {请求字幕编码} 字幕，指定字幕操作会改用 {实际字幕编码}")
            End If
        End If
    End Sub

    Private Shared Function 字幕复制需要源流兼容(a As 预设数据_v6, 输出文件 As String) As Boolean
        Return 输出容器支持MovText字幕(a, 输出文件) OrElse 输出容器支持WebVtt字幕(a, 输出文件)
    End Function

    Private Shared Sub 添加复制流兼容性总览(sb As StringBuilder, a As 预设数据_v6)
        If a Is Nothing Then Exit Sub
        Dim 视频编码器 = 视频编码器数据库_v6.获取编码器数据(a.视频参数_编码器_具体编码)
        If 视频编码器 IsNot Nothing AndAlso 视频编码器.是否复制流 AndAlso 视频滤镜参数已设置(a) Then
            添加总览文本行(sb, "警告：视频选择复制流但配置了视频滤镜；过滤后的流不能直接复制，命令会交给 FFmpeg 选择默认视频编码器")
        End If
        If 视频编码器 IsNot Nothing AndAlso 视频编码器.是否复制流 AndAlso 视频复制流会跳过重编码参数(a) Then
            添加总览文本行(sb, "警告：视频选择复制流，编码预设、配置文件、像素格式、码率/质量、GPU 和线程等重编码参数不会写入命令")
        End If

        Dim 音频编码器 = 音频编码器数据库_v6.获取编码器数据(a.音频参数_编码器_代号)
        If 音频编码器 IsNot Nothing AndAlso 音频编码器.是否复制流 AndAlso 音频滤镜参数已设置(a) Then
            添加总览文本行(sb, "警告：音频选择复制流但配置了音频滤镜；过滤后的流不能直接复制，命令会交给 FFmpeg 选择默认音频编码器")
        End If
        If 音频编码器 IsNot Nothing AndAlso 音频编码器.是否复制流 AndAlso 音频复制流会跳过重编码参数(a) Then
            添加总览文本行(sb, "警告：音频选择复制流，音频比特率、质量参数、位深度和采样率不会写入命令")
        End If
    End Sub

    Private Shared Sub 添加CUDA滤镜兼容性总览(sb As StringBuilder, a As 预设数据_v6)
        If a Is Nothing Then Exit Sub
        Dim 滤镜 = 获取CUDA视频滤镜名称(a)
        If 滤镜.Count = 0 Then Exit Sub

        添加总览文本行(sb, "警告：已选择 CUDA 滤镜（" & String.Join("、", 滤镜) & "），请确保输入视频帧位于 CUDA 硬件帧域；与普通 CPU 滤镜混用时需要自行处理 hwupload_cuda/hwdownload/format")
        If Not CUDA硬件帧输入已配置(a) Then
            添加总览文本行(sb, "警告：当前未设置 -hwaccel_output_format cuda，也没有在自定义视频滤镜中显式使用 hwupload_cuda，CUDA 滤镜可能因输入不是 CUDA 硬件帧而失败")
        End If
    End Sub

    Private Shared Function 获取CUDA视频滤镜名称(a As 预设数据_v6) As List(Of String)
        Dim result As New List(Of String)
        If a Is Nothing Then Return result

        If a.视频参数_降噪_方式 = 预设数据_v6.降噪方式.bilateral_cuda Then 添加唯一滤镜名称(result, "bilateral_cuda")
        Select Case a.视频参数_处理扫描方式
            Case 预设数据_v6.扫描方式.yadif_cuda_自动场序
                添加唯一滤镜名称(result, "yadif_cuda")
            Case 预设数据_v6.扫描方式.bwdif_cuda_自动场序
                添加唯一滤镜名称(result, "bwdif_cuda")
        End Select

        For Each filterText In 获取视频滤镜文本(a)
            If filterText.Contains("bilateral_cuda", StringComparison.OrdinalIgnoreCase) Then 添加唯一滤镜名称(result, "bilateral_cuda")
            If filterText.Contains("yadif_cuda", StringComparison.OrdinalIgnoreCase) Then 添加唯一滤镜名称(result, "yadif_cuda")
            If filterText.Contains("bwdif_cuda", StringComparison.OrdinalIgnoreCase) Then 添加唯一滤镜名称(result, "bwdif_cuda")
        Next

        Return result
    End Function

    Private Shared Sub 添加唯一滤镜名称(list As List(Of String), name As String)
        If list Is Nothing OrElse String.IsNullOrWhiteSpace(name) Then Exit Sub
        If Not list.Any(Function(x) String.Equals(x, name, StringComparison.OrdinalIgnoreCase)) Then list.Add(name)
    End Sub

    Private Shared Function CUDA硬件帧输入已配置(a As 预设数据_v6) As Boolean
        If a Is Nothing Then Return False
        If String.Equals(If(a.解码参数_解码数据格式, "").Trim(), "cuda", StringComparison.OrdinalIgnoreCase) Then Return True
        Return 获取视频滤镜文本(a).Any(Function(x) x.Contains("hwupload_cuda", StringComparison.OrdinalIgnoreCase))
    End Function

    Private Shared Function 获取视频滤镜文本(a As 预设数据_v6) As List(Of String)
        Dim result As New List(Of String)
        If a Is Nothing Then Return result
        If Not String.IsNullOrWhiteSpace(a.自定义参数_视频滤镜) Then result.Add(a.自定义参数_视频滤镜)
        For Each item In If(a.滤镜排序系统, Array.Empty(Of 预设数据_v6.滤镜排序单片结构)())
            If item Is Nothing OrElse item.滤镜目标流类型 <> 预设数据_v6.滤镜排序单片结构.流类型.视频 Then Continue For
            If Not String.IsNullOrWhiteSpace(item.自定义滤镜内容) Then result.Add(item.自定义滤镜内容)
        Next
        Return result
    End Function

    Private Shared Function 视频复制流会跳过重编码参数(a As 预设数据_v6) As Boolean
        If a Is Nothing Then Return False
        Return Not String.IsNullOrWhiteSpace(a.视频参数_编码器_编码预设) OrElse
               Not String.IsNullOrWhiteSpace(a.视频参数_编码器_配置文件) OrElse
               Not String.IsNullOrWhiteSpace(a.视频参数_编码器_场景优化) OrElse
               Not String.IsNullOrWhiteSpace(a.视频参数_编码器_图片编码器质量值) OrElse
               Not String.IsNullOrWhiteSpace(a.视频参数_色彩管理_像素格式) OrElse
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

    Private Shared Function 音频复制流会跳过重编码参数(a As 预设数据_v6) As Boolean
        If a Is Nothing Then Return False
        Return Not String.IsNullOrWhiteSpace(a.音频参数_比特率) OrElse
               Not String.IsNullOrWhiteSpace(a.音频参数_质量参数名) OrElse
               Not String.IsNullOrWhiteSpace(a.音频参数_质量值) OrElse
               Not String.IsNullOrWhiteSpace(a.音频参数_质量参数名2) OrElse
               Not String.IsNullOrWhiteSpace(a.音频参数_质量值2) OrElse
               Not String.IsNullOrWhiteSpace(a.音频参数_位深度) OrElse
               Not String.IsNullOrWhiteSpace(a.音频参数_采样率)
    End Function

    Private Shared Function 视频滤镜参数已设置(a As 预设数据_v6) As Boolean
        If a Is Nothing Then Return False
        Return Not String.IsNullOrWhiteSpace(a.视频参数_分辨率) OrElse
               Not String.IsNullOrWhiteSpace(a.视频参数_分辨率自动计算_宽度) OrElse
               Not String.IsNullOrWhiteSpace(a.视频参数_分辨率自动计算_高度) OrElse
               Not String.IsNullOrWhiteSpace(a.视频参数_分辨率_裁剪滤镜参数) OrElse
               已设置(a.视频参数_抽帧_max, a.视频参数_抽帧_keep, a.视频参数_抽帧_hi, a.视频参数_抽帧_lo, a.视频参数_抽帧_frac) OrElse
               Not String.IsNullOrWhiteSpace(a.视频参数_插帧_目标帧率) OrElse
               Not String.IsNullOrWhiteSpace(a.视频参数_NV_FRUC_目标帧率) OrElse
               Not String.IsNullOrWhiteSpace(a.视频参数_动态模糊_连续混合帧数) OrElse
               超分单片有设置(a.视频参数_超分_直接面板) OrElse
               If(a.视频参数_超分_滤镜叠加策略组, Array.Empty(Of 预设数据_v6.超分数据单片结构)()).Length > 0 OrElse
               a.视频参数_降噪_方式 <> 预设数据_v6.降噪方式.未选择 OrElse
               a.视频参数_锐化_方式 <> 预设数据_v6.锐化方式.未选择 OrElse
               a.视频参数_胶片颗粒_方式 <> 预设数据_v6.胶片颗粒方式.未选择 OrElse
               a.视频参数_平滑断层_方式 <> 预设数据_v6.平滑断层方式.未选择 OrElse
               a.视频参数_处理扫描方式 <> 预设数据_v6.扫描方式.未选择 OrElse
               a.视频参数_画面翻转_角度翻转 <> 预设数据_v6.画面翻转角度.未选择 OrElse
               a.视频参数_画面翻转_镜像翻转 <> 预设数据_v6.画面翻转镜像.未选择 OrElse
               烧字幕滤镜已设置(a) OrElse
               Not String.IsNullOrWhiteSpace(a.视频参数_色彩管理_像素格式预先转换) OrElse
               色彩转换滤镜已设置(a) OrElse
               a.视频参数_色彩管理_启用调整亮度 OrElse
               a.视频参数_色彩管理_启用调整对比度 OrElse
               a.视频参数_色彩管理_启用调整饱和度 OrElse
               a.视频参数_色彩管理_启用调整伽马 OrElse
               If(a.滤镜排序系统, Array.Empty(Of 预设数据_v6.滤镜排序单片结构)()).Any(Function(x) x IsNot Nothing AndAlso x.滤镜目标流类型 = 预设数据_v6.滤镜排序单片结构.流类型.视频 AndAlso Not String.IsNullOrWhiteSpace(x.自定义滤镜内容))
    End Function

    Private Shared Function 音频滤镜参数已设置(a As 预设数据_v6) As Boolean
        If a Is Nothing Then Return False
        Return a.音频参数_响度标准化_启用调整目标响度 OrElse
               a.音频参数_响度标准化_启用调整动态范围 OrElse
               a.音频参数_响度标准化_启用调整峰值电平 OrElse
               Not String.IsNullOrWhiteSpace(a.音频参数_声道数) OrElse
               If(a.滤镜排序系统, Array.Empty(Of 预设数据_v6.滤镜排序单片结构)()).Any(Function(x) x IsNot Nothing AndAlso x.滤镜目标流类型 = 预设数据_v6.滤镜排序单片结构.流类型.音频 AndAlso Not String.IsNullOrWhiteSpace(x.自定义滤镜内容))
    End Function

    Private Shared Sub 添加音视频容器兼容性总览(sb As StringBuilder, a As 预设数据_v6)
        If a Is Nothing Then Exit Sub
        Dim ext = 获取输出容器扩展名(a, 输出占位符)
        If ext <> "webm" Then Exit Sub

        Dim 视频编码器 = 视频编码器数据库_v6.获取编码器数据(a.视频参数_编码器_具体编码)
        If 视频编码器 IsNot Nothing AndAlso Not 视频编码器.是否禁用 Then
            If 视频编码器.是否复制流 Then
                添加总览文本行(sb, "警告：WebM 复制视频流要求源视频本身为 VP8、VP9 或 AV1，否则 FFmpeg 可能封装失败")
            ElseIf 视频编码器.命令行编码器名 <> "" AndAlso Not WebM支持视频编码器(视频编码器.命令行编码器名) Then
                添加总览文本行(sb, $"警告：WebM 不支持视频编码器 {首个命令词(视频编码器.命令行编码器名)}，请选择 VP8、VP9 或 AV1 编码")
            End If
        End If

        Dim 音频编码器 = 音频编码器数据库_v6.获取编码器数据(a.音频参数_编码器_代号)
        If 音频编码器 IsNot Nothing AndAlso Not 音频编码器.是否禁用 Then
            If 音频编码器.是否复制流 Then
                添加总览文本行(sb, "警告：WebM 复制音频流要求源音频本身为 Opus 或 Vorbis，否则 FFmpeg 可能封装失败")
            ElseIf 音频编码器.命令行编码器名 <> "" AndAlso Not WebM支持音频编码器(音频编码器.命令行编码器名) Then
                添加总览文本行(sb, $"警告：WebM 不支持音频编码器 {首个命令词(音频编码器.命令行编码器名)}，请选择 Opus 或 Vorbis 编码")
            End If
        End If
    End Sub

    Private Shared Function WebM支持视频编码器(codec As String) As Boolean
        Select Case 首个命令词(codec)
            Case "libvpx", "libvpx-vp9", "libsvt_vp9", "vp9_qsv",
                 "libaom-av1", "libsvtav1", "librav1e",
                 "av1_nvenc", "av1_qsv", "av1_amf", "av1_d3d12va", "av1_vulkan"
                Return True
            Case Else
                Return False
        End Select
    End Function

    Private Shared Function WebM支持音频编码器(codec As String) As Boolean
        Select Case 首个命令词(codec)
            Case "libopus", "opus", "libvorbis", "vorbis"
                Return True
            Case Else
                Return False
        End Select
    End Function

    Private Shared Function 首个命令词(value As String) As String
        Dim raw = If(value, "").Trim()
        If raw = "" Then Return ""
        Return raw.Split({" "c, ControlChars.Tab}, StringSplitOptions.RemoveEmptyEntries)(0).ToLowerInvariant()
    End Function


End Class
