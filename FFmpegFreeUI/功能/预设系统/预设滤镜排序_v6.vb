Imports System.Globalization
Imports System.IO
Imports System.Text
Imports System.Text.Json
Imports LakeUI

Partial Public Class 预设管理_v6

    Private Shared ReadOnly 内置滤镜默认顺序 As New Dictionary(Of 预设数据_v6.滤镜排序单片结构.标识符枚举, Integer) From {
        {预设数据_v6.滤镜排序单片结构.标识符枚举.像素格式预先转换, 0},
        {预设数据_v6.滤镜排序单片结构.标识符枚举.裁剪, 100},
        {预设数据_v6.滤镜排序单片结构.标识符枚举.缩放, 110},
        {预设数据_v6.滤镜排序单片结构.标识符枚举.抽帧, 120},
        {预设数据_v6.滤镜排序单片结构.标识符枚举.插帧, 130},
        {预设数据_v6.滤镜排序单片结构.标识符枚举.动态模糊, 140},
        {预设数据_v6.滤镜排序单片结构.标识符枚举.超分, 150},
        {预设数据_v6.滤镜排序单片结构.标识符枚举.降噪, 160},
        {预设数据_v6.滤镜排序单片结构.标识符枚举.锐化, 170},
        {预设数据_v6.滤镜排序单片结构.标识符枚举.胶片颗粒, 180},
        {预设数据_v6.滤镜排序单片结构.标识符枚举.平滑断层, 190},
        {预设数据_v6.滤镜排序单片结构.标识符枚举.扫描方式, 200},
        {预设数据_v6.滤镜排序单片结构.标识符枚举.画面翻转, 210},
        {预设数据_v6.滤镜排序单片结构.标识符枚举.烧录字幕, 220},
        {预设数据_v6.滤镜排序单片结构.标识符枚举.色彩转换, 300},
        {预设数据_v6.滤镜排序单片结构.标识符枚举.调色, 310},
        {预设数据_v6.滤镜排序单片结构.标识符枚举.音频响度标准化, 1000},
        {预设数据_v6.滤镜排序单片结构.标识符枚举.音频格式转换, 1010},
        {预设数据_v6.滤镜排序单片结构.标识符枚举.音频重采样, 1020}
    }

    Private Shared Sub 迁移旧自定义滤镜字段到排序系统(a As 预设数据_v6)
        If a Is Nothing Then Exit Sub
        Dim 排序 = If(a.滤镜排序系统, Array.Empty(Of 预设数据_v6.滤镜排序单片结构)()).ToList()
        If Not String.IsNullOrWhiteSpace(a.自定义参数_视频滤镜) Then
            Dim 内容 = a.自定义参数_视频滤镜.Trim()
            If Not 排序.Any(Function(x) x.滤镜标识符 = 预设数据_v6.滤镜排序单片结构.标识符枚举.自定义视频滤镜 AndAlso String.Equals(x.自定义滤镜内容, 内容, StringComparison.Ordinal)) Then
                排序.Add(New 预设数据_v6.滤镜排序单片结构 With {
                    .显示名称 = "自定义视频滤镜",
                    .是自定义滤镜 = True,
                    .允许在排序页直接编辑 = False,
                    .滤镜标识符 = 预设数据_v6.滤镜排序单片结构.标识符枚举.自定义视频滤镜,
                    .滤镜目标流类型 = 预设数据_v6.滤镜排序单片结构.流类型.视频,
                    .自定义滤镜内容 = 内容
                })
            End If
            a.自定义参数_视频滤镜 = ""
        End If
        If Not String.IsNullOrWhiteSpace(a.自定义参数_音频滤镜) Then
            Dim 内容 = a.自定义参数_音频滤镜.Trim()
            If Not 排序.Any(Function(x) x.滤镜标识符 = 预设数据_v6.滤镜排序单片结构.标识符枚举.自定义音频滤镜 AndAlso String.Equals(x.自定义滤镜内容, 内容, StringComparison.Ordinal)) Then
                排序.Add(New 预设数据_v6.滤镜排序单片结构 With {
                    .显示名称 = "自定义音频滤镜",
                    .是自定义滤镜 = True,
                    .允许在排序页直接编辑 = False,
                    .滤镜标识符 = 预设数据_v6.滤镜排序单片结构.标识符枚举.自定义音频滤镜,
                    .滤镜目标流类型 = 预设数据_v6.滤镜排序单片结构.流类型.音频,
                    .自定义滤镜内容 = 内容
                })
            End If
            a.自定义参数_音频滤镜 = ""
        End If
        a.滤镜排序系统 = 规范化滤镜流类型顺序(排序).ToArray()
    End Sub

    Private Shared Sub 同步内置滤镜排序数据(a As 预设数据_v6)
        If a Is Nothing Then Exit Sub
        Dim 排序 = If(a.滤镜排序系统, Array.Empty(Of 预设数据_v6.滤镜排序单片结构)()).
            Where(Function(x) x IsNot Nothing).
            ToList()

        更新内置滤镜排序项(排序, a, 预设数据_v6.滤镜排序单片结构.标识符枚举.裁剪, Not String.IsNullOrWhiteSpace(a.视频参数_分辨率_裁剪滤镜参数))
        更新内置滤镜排序项(排序, a, 预设数据_v6.滤镜排序单片结构.标识符枚举.缩放, Not String.IsNullOrWhiteSpace(a.视频参数_分辨率) OrElse Not String.IsNullOrWhiteSpace(a.视频参数_分辨率自动计算_宽度) OrElse Not String.IsNullOrWhiteSpace(a.视频参数_分辨率自动计算_高度))
        更新内置滤镜排序项(排序, a, 预设数据_v6.滤镜排序单片结构.标识符枚举.抽帧, 抽帧参数已设置(a))
        更新内置滤镜排序项(排序, a, 预设数据_v6.滤镜排序单片结构.标识符枚举.插帧, Not String.IsNullOrWhiteSpace(a.视频参数_插帧_目标帧率))
        更新内置滤镜排序项(排序, a, 预设数据_v6.滤镜排序单片结构.标识符枚举.动态模糊, Not String.IsNullOrWhiteSpace(a.视频参数_动态模糊_连续混合帧数))
        更新内置滤镜排序项(排序, a, 预设数据_v6.滤镜排序单片结构.标识符枚举.超分, 超分单片有设置(a.视频参数_超分_直接面板) OrElse a.视频参数_超分_滤镜叠加策略组.Length > 0)
        更新内置滤镜排序项(排序, a, 预设数据_v6.滤镜排序单片结构.标识符枚举.降噪, a.视频参数_降噪_方式 <> 预设数据_v6.降噪方式.未选择)
        更新内置滤镜排序项(排序, a, 预设数据_v6.滤镜排序单片结构.标识符枚举.锐化, a.视频参数_锐化_方式 <> 预设数据_v6.锐化方式.未选择)
        更新内置滤镜排序项(排序, a, 预设数据_v6.滤镜排序单片结构.标识符枚举.胶片颗粒, a.视频参数_胶片颗粒_方式 <> 预设数据_v6.胶片颗粒方式.未选择)
        更新内置滤镜排序项(排序, a, 预设数据_v6.滤镜排序单片结构.标识符枚举.平滑断层, a.视频参数_平滑断层_方式 <> 预设数据_v6.平滑断层方式.未选择)
        更新内置滤镜排序项(排序, a, 预设数据_v6.滤镜排序单片结构.标识符枚举.扫描方式, a.视频参数_处理扫描方式 <> 预设数据_v6.扫描方式.未选择)
        更新内置滤镜排序项(排序, a, 预设数据_v6.滤镜排序单片结构.标识符枚举.画面翻转, a.视频参数_画面翻转_角度翻转 <> 预设数据_v6.画面翻转角度.未选择 OrElse a.视频参数_画面翻转_镜像翻转 <> 预设数据_v6.画面翻转镜像.未选择)
        更新内置滤镜排序项(排序, a, 预设数据_v6.滤镜排序单片结构.标识符枚举.烧录字幕, 烧字幕滤镜已设置(a))
        更新内置滤镜排序项(排序, a, 预设数据_v6.滤镜排序单片结构.标识符枚举.像素格式预先转换, Not String.IsNullOrWhiteSpace(a.视频参数_色彩管理_像素格式预先转换))
        更新内置滤镜排序项(排序, a, 预设数据_v6.滤镜排序单片结构.标识符枚举.色彩转换, 色彩转换滤镜已设置(a))
        更新内置滤镜排序项(排序, a, 预设数据_v6.滤镜排序单片结构.标识符枚举.调色, a.视频参数_色彩管理_启用调整亮度 OrElse a.视频参数_色彩管理_启用调整对比度 OrElse a.视频参数_色彩管理_启用调整饱和度 OrElse a.视频参数_色彩管理_启用调整伽马)
        更新内置滤镜排序项(排序, a, 预设数据_v6.滤镜排序单片结构.标识符枚举.音频响度标准化, a.音频参数_响度标准化_启用调整目标响度 OrElse a.音频参数_响度标准化_启用调整动态范围 OrElse a.音频参数_响度标准化_启用调整峰值电平)
        更新内置滤镜排序项(排序, a, 预设数据_v6.滤镜排序单片结构.标识符枚举.音频格式转换, Not String.IsNullOrWhiteSpace(a.音频参数_声道数))
        更新内置滤镜排序项(排序, a, 预设数据_v6.滤镜排序单片结构.标识符枚举.音频重采样, False)

        a.滤镜排序系统 = 规范化滤镜流类型顺序(排序).ToArray()
    End Sub

    Private Shared Sub 更新内置滤镜排序项(排序 As List(Of 预设数据_v6.滤镜排序单片结构),
                                  a As 预设数据_v6,
                                  标识符 As 预设数据_v6.滤镜排序单片结构.标识符枚举,
                                  启用 As Boolean)
        Dim item = 排序.FirstOrDefault(Function(x) Not x.是自定义滤镜 AndAlso x.滤镜标识符 = 标识符)
        If Not 启用 Then
            If item IsNot Nothing Then 排序.Remove(item)
            Exit Sub
        End If

        If item Is Nothing Then
            item = New 预设数据_v6.滤镜排序单片结构 With {.滤镜标识符 = 标识符}
            Dim insertIndex = 获取内置滤镜插入位置(排序, 标识符, 获取目标流类型(标识符))
            If insertIndex >= 0 AndAlso insertIndex < 排序.Count Then
                排序.Insert(insertIndex, item)
            Else
                排序.Add(item)
            End If
        End If

        item.滤镜标识符 = 标识符
        item.滤镜目标流类型 = 获取目标流类型(标识符)
        item.显示名称 = 获取滤镜显示名称(标识符)
        item.自定义滤镜内容 = 获取滤镜片段(a, New 预设数据_v6.滤镜排序单片结构 With {.滤镜标识符 = 标识符})
        item.是自定义滤镜 = False
        item.允许在排序页直接编辑 = False
    End Sub

    Private Shared Function 获取内置滤镜插入位置(排序 As List(Of 预设数据_v6.滤镜排序单片结构),
                                        标识符 As 预设数据_v6.滤镜排序单片结构.标识符枚举,
                                        target As 预设数据_v6.滤镜排序单片结构.流类型) As Integer
        Dim newPriority = 获取内置滤镜默认顺序(标识符)
        Dim lastSameTypeIndex = -1
        For i = 0 To 排序.Count - 1
            Dim current = 排序(i)
            If current Is Nothing OrElse current.滤镜目标流类型 <> target Then Continue For
            lastSameTypeIndex = i
            If current.是自定义滤镜 Then Continue For
            If 获取内置滤镜默认顺序(current.滤镜标识符) > newPriority Then Return i
        Next
        If lastSameTypeIndex >= 0 Then Return lastSameTypeIndex + 1
        Return 排序.Count
    End Function

    Private Shared Function 获取内置滤镜默认顺序(标识符 As 预设数据_v6.滤镜排序单片结构.标识符枚举) As Integer
        Dim value As Integer
        If 内置滤镜默认顺序.TryGetValue(标识符, value) Then Return value
        Return Integer.MaxValue
    End Function

    Private Shared Function 规范化滤镜流类型顺序(items As IEnumerable(Of 预设数据_v6.滤镜排序单片结构)) As List(Of 预设数据_v6.滤镜排序单片结构)
        If items Is Nothing Then Return New List(Of 预设数据_v6.滤镜排序单片结构)
        Return items.
            Where(Function(x) x IsNot Nothing).
            OrderBy(Function(x) 获取滤镜流类型排序权重(x)).
            ToList()
    End Function

    Private Shared Function 获取滤镜流类型排序权重(item As 预设数据_v6.滤镜排序单片结构) As Integer
        If item IsNot Nothing AndAlso item.滤镜目标流类型 = 预设数据_v6.滤镜排序单片结构.流类型.音频 Then Return 1
        Return 0
    End Function

    Public Shared Sub 应用Agent滤镜排序请求(target As 预设数据_v6, previous As 预设数据_v6)
        If target Is Nothing Then Exit Sub

        Dim requested = If(target.滤镜排序系统, Array.Empty(Of 预设数据_v6.滤镜排序单片结构)()).
            Where(Function(x) x IsNot Nothing).
            ToList()
        规范化Agent排序项(requested)

        Dim requestedBuiltIns = requested.
            Where(Function(x) 是内置滤镜项(x)).
            Select(Function(x) x.滤镜标识符).
            ToHashSet()
        Dim idsToCheck = If(previous?.滤镜排序系统, Array.Empty(Of 预设数据_v6.滤镜排序单片结构)()).
            Where(Function(x) 是内置滤镜项(x)).
            Select(Function(x) x.滤镜标识符).
            Concat(获取预设启用的内置滤镜标识符(target)).
            Distinct().
            ToList()

        For Each 标识符 In idsToCheck
            If Not requestedBuiltIns.Contains(标识符) Then 重置滤镜对应预设字段(target, 标识符)
        Next

        target.滤镜排序系统 = 规范化滤镜流类型顺序(requested).ToArray()
    End Sub

    Private Shared Sub 规范化Agent排序项(items As List(Of 预设数据_v6.滤镜排序单片结构))
        If items Is Nothing Then Exit Sub
        For Each item In items
            If item Is Nothing Then Continue For
            If String.IsNullOrWhiteSpace(item.实例ID) Then item.实例ID = Guid.NewGuid().ToString("N")

            If item.滤镜标识符 = 预设数据_v6.滤镜排序单片结构.标识符枚举.未设置 AndAlso
               (item.是自定义滤镜 OrElse Not String.IsNullOrWhiteSpace(item.自定义滤镜内容)) Then
                item.滤镜标识符 = If(item.滤镜目标流类型 = 预设数据_v6.滤镜排序单片结构.流类型.音频,
                                 预设数据_v6.滤镜排序单片结构.标识符枚举.自定义音频滤镜,
                                 预设数据_v6.滤镜排序单片结构.标识符枚举.自定义视频滤镜)
            End If

            If 是自定义滤镜标识符(item.滤镜标识符) Then
                item.是自定义滤镜 = True
                item.允许在排序页直接编辑 = True
                item.滤镜目标流类型 = 获取目标流类型(item.滤镜标识符)
                If String.IsNullOrWhiteSpace(item.显示名称) Then item.显示名称 = 获取滤镜显示名称(item.滤镜标识符)
            ElseIf item.滤镜标识符 <> 预设数据_v6.滤镜排序单片结构.标识符枚举.未设置 Then
                item.是自定义滤镜 = False
                item.允许在排序页直接编辑 = False
                item.滤镜目标流类型 = 获取目标流类型(item.滤镜标识符)
                item.显示名称 = 获取滤镜显示名称(item.滤镜标识符)
            End If
        Next
    End Sub

    Private Shared Function 是内置滤镜项(item As 预设数据_v6.滤镜排序单片结构) As Boolean
        Return item IsNot Nothing AndAlso
               item.滤镜标识符 <> 预设数据_v6.滤镜排序单片结构.标识符枚举.未设置 AndAlso
               Not 是自定义滤镜标识符(item.滤镜标识符) AndAlso
               Not item.是自定义滤镜
    End Function

    Private Shared Function 获取预设启用的内置滤镜标识符(a As 预设数据_v6) As IEnumerable(Of 预设数据_v6.滤镜排序单片结构.标识符枚举)
        Dim result As New List(Of 预设数据_v6.滤镜排序单片结构.标识符枚举)
        If a Is Nothing Then Return result

        If Not String.IsNullOrWhiteSpace(a.视频参数_分辨率_裁剪滤镜参数) Then result.Add(预设数据_v6.滤镜排序单片结构.标识符枚举.裁剪)
        If Not String.IsNullOrWhiteSpace(a.视频参数_分辨率) OrElse Not String.IsNullOrWhiteSpace(a.视频参数_分辨率自动计算_宽度) OrElse Not String.IsNullOrWhiteSpace(a.视频参数_分辨率自动计算_高度) Then result.Add(预设数据_v6.滤镜排序单片结构.标识符枚举.缩放)
        If 抽帧参数已设置(a) Then result.Add(预设数据_v6.滤镜排序单片结构.标识符枚举.抽帧)
        If Not String.IsNullOrWhiteSpace(a.视频参数_插帧_目标帧率) Then result.Add(预设数据_v6.滤镜排序单片结构.标识符枚举.插帧)
        If Not String.IsNullOrWhiteSpace(a.视频参数_动态模糊_连续混合帧数) Then result.Add(预设数据_v6.滤镜排序单片结构.标识符枚举.动态模糊)
        If 超分单片有设置(a.视频参数_超分_直接面板) OrElse If(a.视频参数_超分_滤镜叠加策略组, Array.Empty(Of 预设数据_v6.超分数据单片结构)()).Length > 0 Then result.Add(预设数据_v6.滤镜排序单片结构.标识符枚举.超分)
        If a.视频参数_降噪_方式 <> 预设数据_v6.降噪方式.未选择 Then result.Add(预设数据_v6.滤镜排序单片结构.标识符枚举.降噪)
        If a.视频参数_锐化_方式 <> 预设数据_v6.锐化方式.未选择 Then result.Add(预设数据_v6.滤镜排序单片结构.标识符枚举.锐化)
        If a.视频参数_胶片颗粒_方式 <> 预设数据_v6.胶片颗粒方式.未选择 Then result.Add(预设数据_v6.滤镜排序单片结构.标识符枚举.胶片颗粒)
        If a.视频参数_平滑断层_方式 <> 预设数据_v6.平滑断层方式.未选择 Then result.Add(预设数据_v6.滤镜排序单片结构.标识符枚举.平滑断层)
        If a.视频参数_处理扫描方式 <> 预设数据_v6.扫描方式.未选择 Then result.Add(预设数据_v6.滤镜排序单片结构.标识符枚举.扫描方式)
        If a.视频参数_画面翻转_角度翻转 <> 预设数据_v6.画面翻转角度.未选择 OrElse a.视频参数_画面翻转_镜像翻转 <> 预设数据_v6.画面翻转镜像.未选择 Then result.Add(预设数据_v6.滤镜排序单片结构.标识符枚举.画面翻转)
        If 烧字幕滤镜已设置(a) Then result.Add(预设数据_v6.滤镜排序单片结构.标识符枚举.烧录字幕)
        If Not String.IsNullOrWhiteSpace(a.视频参数_色彩管理_像素格式预先转换) Then result.Add(预设数据_v6.滤镜排序单片结构.标识符枚举.像素格式预先转换)
        If 色彩转换滤镜已设置(a) Then result.Add(预设数据_v6.滤镜排序单片结构.标识符枚举.色彩转换)
        If a.视频参数_色彩管理_启用调整亮度 OrElse a.视频参数_色彩管理_启用调整对比度 OrElse a.视频参数_色彩管理_启用调整饱和度 OrElse a.视频参数_色彩管理_启用调整伽马 Then result.Add(预设数据_v6.滤镜排序单片结构.标识符枚举.调色)
        If a.音频参数_响度标准化_启用调整目标响度 OrElse a.音频参数_响度标准化_启用调整动态范围 OrElse a.音频参数_响度标准化_启用调整峰值电平 Then result.Add(预设数据_v6.滤镜排序单片结构.标识符枚举.音频响度标准化)
        If Not String.IsNullOrWhiteSpace(a.音频参数_声道数) Then result.Add(预设数据_v6.滤镜排序单片结构.标识符枚举.音频格式转换)
        Return result
    End Function

    Private Shared Function 是自定义滤镜标识符(标识符 As 预设数据_v6.滤镜排序单片结构.标识符枚举) As Boolean
        Return 标识符 = 预设数据_v6.滤镜排序单片结构.标识符枚举.自定义视频滤镜 OrElse
               标识符 = 预设数据_v6.滤镜排序单片结构.标识符枚举.自定义音频滤镜
    End Function

    Private Shared Sub 重置滤镜对应预设字段(a As 预设数据_v6, 标识符 As 预设数据_v6.滤镜排序单片结构.标识符枚举)
        If a Is Nothing Then Exit Sub
        Select Case 标识符
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.裁剪
                a.视频参数_分辨率_裁剪滤镜参数 = ""
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.缩放
                a.视频参数_分辨率 = ""
                a.视频参数_分辨率自动计算_宽度 = ""
                a.视频参数_分辨率自动计算_高度 = ""
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.抽帧
                a.视频参数_抽帧_max = ""
                a.视频参数_抽帧_keep = ""
                a.视频参数_抽帧_hi = ""
                a.视频参数_抽帧_lo = ""
                a.视频参数_抽帧_frac = ""
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.插帧
                a.视频参数_插帧_目标帧率 = ""
                a.视频参数_插帧_插帧模式 = ""
                a.视频参数_插帧_运动估计模式 = ""
                a.视频参数_插帧_运动估计算法 = ""
                a.视频参数_插帧_运动补偿模式 = ""
                a.视频参数_插帧_可变块大小的运动补偿 = False
                a.视频参数_插帧_块大小 = ""
                a.视频参数_插帧_搜索范围 = ""
                a.视频参数_插帧_场景变化检测强度 = ""
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.动态模糊
                a.视频参数_动态模糊_连续混合帧数 = ""
                a.视频参数_动态模糊_每帧权重 = ""
                a.视频参数_动态模糊_输出缩放系数 = ""
                a.视频参数_动态模糊_处理颜色平面 = ""
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.超分
                a.视频参数_超分_直接面板 = New 预设数据_v6.超分数据单片结构
                a.视频参数_超分_滤镜叠加策略组 = Array.Empty(Of 预设数据_v6.超分数据单片结构)()
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.降噪
                a.视频参数_降噪_方式 = 预设数据_v6.降噪方式.未选择
                a.视频参数_降噪_参数1 = "0"
                a.视频参数_降噪_参数2 = "0"
                a.视频参数_降噪_参数3 = "0"
                a.视频参数_降噪_参数4 = "0"
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.锐化
                a.视频参数_锐化_方式 = 预设数据_v6.锐化方式.未选择
                a.视频参数_锐化_参数1 = "0"
                a.视频参数_锐化_参数2 = "0"
                a.视频参数_锐化_参数3 = "0"
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.胶片颗粒
                a.视频参数_胶片颗粒_方式 = 预设数据_v6.胶片颗粒方式.未选择
                a.视频参数_胶片颗粒_参数1 = "0"
                a.视频参数_胶片颗粒_参数2 = "0"
                a.视频参数_胶片颗粒_参数3 = "0"
                a.视频参数_胶片颗粒_参数4 = "0"
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.平滑断层
                a.视频参数_平滑断层_方式 = 预设数据_v6.平滑断层方式.未选择
                a.视频参数_平滑断层_参数1 = "0"
                a.视频参数_平滑断层_参数2 = "0"
                a.视频参数_平滑断层_参数3 = "0"
                a.视频参数_平滑断层_参数4 = "0"
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.扫描方式
                a.视频参数_处理扫描方式 = 预设数据_v6.扫描方式.未选择
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.画面翻转
                a.视频参数_画面翻转_角度翻转 = 预设数据_v6.画面翻转角度.未选择
                a.视频参数_画面翻转_镜像翻转 = 预设数据_v6.画面翻转镜像.未选择
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.烧录字幕
                a.视频参数_烧录字幕_滤镜选择 = 预设数据_v6.烧字幕滤镜.未选择
                a.视频参数_烧录字幕_字幕来源是外部文件 = 预设数据_v6.烧字幕来源.未选择
                a.视频参数_烧录字幕_字幕格式优先级 = New List(Of 预设数据_v6.烧字幕格式) From {
                    预设数据_v6.烧字幕格式.未选择,
                    预设数据_v6.烧字幕格式.未选择,
                    预设数据_v6.烧字幕格式.未选择
                }
                a.视频参数_烧录字幕_外部字幕文件名 = ""
                a.视频参数_烧录字幕_外部字幕文件夹位置 = ""
                a.视频参数_烧录字幕_指定内嵌的流 = ""
                a.视频参数_烧录字幕_基本样式_名称 = ""
                a.视频参数_烧录字幕_基本样式_大小 = 0
                a.视频参数_烧录字幕_基本样式_粗体 = False
                a.视频参数_烧录字幕_基本样式_斜体 = False
                a.视频参数_烧录字幕_基本样式_下划线 = False
                a.视频参数_烧录字幕_基本样式_删除线 = False
                a.视频参数_烧录字幕_边框样式 = 预设数据_v6.烧字幕边框样式.未选择
                a.视频参数_烧录字幕_描边宽度 = ""
                a.视频参数_烧录字幕_阴影距离 = ""
                a.视频参数_烧录字幕_主要颜色 = New 预设数据_v6.烧字幕专用颜色类型
                a.视频参数_烧录字幕_次要颜色 = New 预设数据_v6.烧字幕专用颜色类型
                a.视频参数_烧录字幕_描边颜色 = New 预设数据_v6.烧字幕专用颜色类型
                a.视频参数_烧录字幕_背景颜色 = New 预设数据_v6.烧字幕专用颜色类型
                a.视频参数_烧录字幕_字体文件夹 = ""
                a.视频参数_烧录字幕_对齐方位 = 预设数据_v6.烧字幕对齐方位.未选择
                a.视频参数_烧录字幕_垂直边距 = ""
                a.视频参数_烧录字幕_左边距 = ""
                a.视频参数_烧录字幕_右边距 = ""
                a.视频参数_烧录字幕_字距 = ""
                a.视频参数_烧录字幕_行距 = ""
                a.视频参数_烧录字幕_补充样式 = ""
                a.视频参数_烧录字幕_自己写滤镜取代所有设置 = ""
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.像素格式预先转换
                a.视频参数_色彩管理_像素格式预先转换 = ""
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.色彩转换
                a.视频参数_色彩管理_滤镜选择 = ""
                a.视频参数_色彩管理_矩阵系数 = ""
                a.视频参数_色彩管理_色域 = ""
                a.视频参数_色彩管理_传输特性 = ""
                a.视频参数_色彩管理_范围 = ""
                a.视频参数_色彩管理_色调映射算法 = ""
                a.视频参数_色彩管理_处理方式 = ""
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.调色
                a.视频参数_色彩管理_启用调整亮度 = False
                a.视频参数_色彩管理_启用调整对比度 = False
                a.视频参数_色彩管理_启用调整饱和度 = False
                a.视频参数_色彩管理_启用调整伽马 = False
                a.视频参数_色彩管理_亮度 = "0"
                a.视频参数_色彩管理_对比度 = "1"
                a.视频参数_色彩管理_饱和度 = "1"
                a.视频参数_色彩管理_伽马 = "1"
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.音频响度标准化
                a.音频参数_响度标准化_启用调整目标响度 = False
                a.音频参数_响度标准化_启用调整动态范围 = False
                a.音频参数_响度标准化_启用调整峰值电平 = False
                a.音频参数_响度标准化_目标响度 = "-24"
                a.音频参数_响度标准化_动态范围 = "1"
                a.音频参数_响度标准化_峰值电平 = "-1"
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.音频格式转换
                a.音频参数_声道数 = ""
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.音频重采样
                a.音频参数_采样率 = ""
        End Select
    End Sub

End Class
