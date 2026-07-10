Imports System.Globalization
Imports System.IO
Imports System.Text
Imports System.Text.Json
Imports LakeUI

Partial Public Class 预设管理_v6

    Public Shared Sub 显示预设(a As 预设数据_v6, ui As Form_v6_参数面板)
        If a Is Nothing OrElse ui Is Nothing Then Exit Sub
        Dim oldSuppress = ui.抑制自动刷新
        ui.抑制自动刷新 = True
        Try
            初始化空集合(a)
            显示输出文件设置(a, ui)
            显示解码参数(a, ui)
            显示视频编码器(a, ui)
            显示画面帧(a, ui)
            显示质量(a, ui)
            显示色彩管理(a, ui)
            显示音频参数(a, ui)
            显示剪辑(a, ui)
            显示视频帧服务器(a, ui)
            显示自定义参数(a, ui)
            显示流控制(a, ui)
            显示附加内容(a, ui)
            ui.私有界面_滤镜排序.设置排序数据(Array.Empty(Of 预设数据_v6.滤镜排序单片结构)(), False)
            If a.滤镜排序系统.Length > 0 Then
                ui.私有界面_滤镜排序.设置排序数据(a.滤镜排序系统.ToList(), False)
            End If
            同步全部内置滤镜到排序(ui, False)
            ui.私有界面_预设管理.MTB_预设备注.Text = a.预设备注
            刷新参数总览(ui)
        Finally
            ui.抑制自动刷新 = oldSuppress
        End Try
    End Sub

    Public Shared Sub 储存预设(ByRef a As 预设数据_v6, ui As Form_v6_参数面板)
        If a Is Nothing Then a = New 预设数据_v6
        If ui Is Nothing Then Exit Sub
        初始化空集合(a)
        a.预设备注 = ui.私有界面_预设管理.MTB_预设备注.Text.Trim()
        a.额外保存输出位置 = ui.私有界面_预设管理.MCK_额外保存输出位置.Checked

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
        ui.私有界面_滤镜排序.刷新局部预览(a)
        a.滤镜排序系统 = ui.私有界面_滤镜排序.获取排序数据().ToArray()
        同步内置滤镜排序数据(a)
    End Sub

    Public Shared Function 从面板创建预设(ui As Form_v6_参数面板) As 预设数据_v6
        Dim a As New 预设数据_v6
        储存预设(a, ui)
        Return a
    End Function

    Public Shared Sub 重置面板(ui As Form_v6_参数面板)
        If ui Is Nothing Then Exit Sub
        显示预设(New 预设数据_v6, ui)
        ui.私有界面_预设管理.MTB_预设名称.Text = ""
    End Sub

    Public Shared Sub 显示参数总览(MTB As ModernTextBox, a As 预设数据_v6)
        If MTB Is Nothing Then Exit Sub
        MTB.EnableSyntaxHighlight = False
        初始化空集合(a)

        Dim sb As New StringBuilder

        If Not String.IsNullOrWhiteSpace(a.自定义参数_完全自己写) Then
            添加总览文本行(sb, "正在使用完全自己写参数模式，其他参数均不会生效")
            添加总览文本行(sb, "完全自己写参数：" & a.自定义参数_完全自己写)
            设置参数总览文本(MTB, sb.ToString())
            Exit Sub
        End If

        If a.视频参数_视频帧服务器_使用AviSynth Then
            添加总览文本行(sb, "正在使用 AviSynth，请不要直接将此任务的命令行直接复制到其他地方运行，因为与任务对应的脚本文件还未生成，至少运行一次任务再尝试外部操作！")
            If Not String.IsNullOrWhiteSpace(a.视频参数_视频帧服务器_avs脚本文件) Then
                添加总览文本行(sb, "AviSynth 指定模板：" & a.视频参数_视频帧服务器_avs脚本文件)
            Else
                添加总览文本行(sb, "没有指定 AviSynth 模板")
            End If
        End If
        If a.视频参数_视频帧服务器_使用VapourSynth Then
            添加总览文本行(sb, "正在使用 VapourSynth，请不要直接将此任务的命令行直接复制到其他地方运行，因为与任务对应的脚本文件还未生成，至少运行一次任务再尝试外部操作！")
            If Not String.IsNullOrWhiteSpace(a.视频参数_视频帧服务器_vpy脚本文件) Then
                添加总览文本行(sb, "VapourSynth 指定模板：" & a.视频参数_视频帧服务器_vpy脚本文件)
            Else
                添加总览文本行(sb, "没有指定 VapourSynth 模板")
            End If
        End If

        If String.IsNullOrWhiteSpace(a.输出容器) Then
            添加总览文本行(sb, "警告：没有指定输出后缀/输出容器，常规输出文件无法生成正确扩展名")
        Else
            添加总览文本行(sb, "输出容器：" & a.输出容器)
        End If
        If a.输出_输出文件参数使用方法 <> 预设数据_v6.输出文件参数使用方法.正常使用 Then 添加总览文本行(sb, "输出文件参数使用方法：" & 格式化枚举名称(a.输出_输出文件参数使用方法))
        If a.输出_自动命名选项 <> 预设数据_v6.自动命名选项.附加_递增时间戳 Then 添加总览文本行(sb, "自动命名方式：" & 格式化枚举名称(a.输出_自动命名选项))
        添加总览文本行(sb, "输出文件开头文本：" & a.输出命名_开头文本)
        添加总览文本行(sb, "输出文件替代文本：" & a.输出命名_替代文本)
        添加总览文本行(sb, "输出文件结尾文本：" & a.输出命名_结尾文本)
        If a.输出命名_保留创建时间 Then 添加总览文本行(sb, "输出命名：保留创建时间")
        If a.输出命名_保留修改时间 Then 添加总览文本行(sb, "输出命名：保留修改时间")
        If a.输出命名_保留访问时间 Then 添加总览文本行(sb, "输出命名：保留访问时间")
        If 预设包含输出位置(a) Then 添加总览文本行(sb, "输出位置：" & a.输出位置)
        If Not String.IsNullOrWhiteSpace(a.输出位置_保留子文件夹结构起始点) Then 添加总览文本行(sb, "保留子文件夹结构起始点：" & a.输出位置_保留子文件夹结构起始点)

        添加总览文本行(sb, "解码器：" & a.解码参数_解码器)
        添加总览文本行(sb, "CPU 解码线程数：" & a.解码参数_CPU解码线程数)
        添加总览文本行(sb, "解码数据格式：" & a.解码参数_解码数据格式)
        If Not String.IsNullOrWhiteSpace(a.解码参数_指定硬件的参数名) Then
            If Not String.IsNullOrWhiteSpace(a.解码参数_指定硬件的参数) Then
                添加总览文本行(sb, "指定解码硬件参数：" & 规范FFmpeg参数名(a.解码参数_指定硬件的参数名) & " " & a.解码参数_指定硬件的参数)
            Else
                添加总览文本行(sb, "必须指定解码硬件的参数，那不是选了就能用的")
            End If
        End If

        If a.视频参数_编码器_类型 <> 预设数据_v6.视频编码器类型.未选择 Then 添加总览文本行(sb, "视频编码器类型：" & 格式化枚举名称(a.视频参数_编码器_类型))
        添加总览文本行(sb, "视频编码类别：" & a.视频参数_编码器_分类名称)
        添加总览文本行(sb, "视频编码器：" & a.视频参数_编码器_具体编码)
        添加总览文本行(sb, "图片编码器质量：" & a.视频参数_编码器_图片编码器质量值)
        添加总览文本行(sb, "视频编码预设：" & a.视频参数_编码器_编码预设)
        添加总览文本行(sb, "视频编码配置文件：" & a.视频参数_编码器_配置文件)
        添加总览文本行(sb, "视频编码场景优化：" & a.视频参数_编码器_场景优化)
        添加总览文本行(sb, "视频编码 GPU：" & a.视频参数_编码器_gpu)
        添加总览文本行(sb, "视频编码线程数：" & a.视频参数_编码器_threads)

        添加总览文本行(sb, "视频分辨率：" & a.视频参数_分辨率)
        添加总览文本行(sb, "视频分辨率自动计算：宽 = " & a.视频参数_分辨率自动计算_宽度)
        添加总览文本行(sb, "视频分辨率自动计算：高 = " & a.视频参数_分辨率自动计算_高度)
        添加总览文本行(sb, "画面裁剪：" & a.视频参数_分辨率_裁剪滤镜参数)
        添加总览文本行(sb, "输出帧率：" & a.视频参数_帧速率)

        If 已设置(a.视频参数_抽帧_max, a.视频参数_抽帧_keep, a.视频参数_抽帧_hi, a.视频参数_抽帧_lo, a.视频参数_抽帧_frac) Then
            添加总览文本行(sb, "抽帧：" & JoinNamed("；", ("max", a.视频参数_抽帧_max), ("keep", a.视频参数_抽帧_keep), ("hi", a.视频参数_抽帧_hi), ("lo", a.视频参数_抽帧_lo), ("frac", a.视频参数_抽帧_frac)))
        End If

        If Not String.IsNullOrWhiteSpace(a.视频参数_插帧_目标帧率) Then
            添加总览文本行(sb, "插帧目标帧率：" & a.视频参数_插帧_目标帧率)
            添加总览文本行(sb, "插帧模式：" & 插帧模式显示文本(a.视频参数_插帧_插帧模式))
            添加总览文本行(sb, "运动估计模式：" & 运动估计模式显示文本(a.视频参数_插帧_运动估计模式))
            添加总览文本行(sb, "运动估计算法：" & 运动估计算法显示文本(a.视频参数_插帧_运动估计算法))
            添加总览文本行(sb, "运动补偿模式：" & 运动补偿模式显示文本(a.视频参数_插帧_运动补偿模式))
            If a.视频参数_插帧_可变块大小的运动补偿 Then 添加总览文本行(sb, "插帧：已启用可变块大小运动补偿")
            添加总览文本行(sb, "插帧块大小：" & a.视频参数_插帧_块大小)
            添加总览文本行(sb, "插帧搜索范围：" & a.视频参数_插帧_搜索范围)
            添加总览文本行(sb, "场景变化检测强度：" & a.视频参数_插帧_场景变化检测强度)
        End If

        If Not String.IsNullOrWhiteSpace(a.视频参数_动态模糊_连续混合帧数) Then
            添加总览文本行(sb, "动态模糊连续混合帧数：" & a.视频参数_动态模糊_连续混合帧数)
            添加总览文本行(sb, "动态模糊每帧权重：" & a.视频参数_动态模糊_每帧权重)
            添加总览文本行(sb, "动态模糊输出缩放系数：" & a.视频参数_动态模糊_输出缩放系数)
            添加总览文本行(sb, "动态模糊处理颜色平面：" & a.视频参数_动态模糊_处理颜色平面)
        End If

        If a.视频参数_超分_滤镜叠加策略组 IsNot Nothing AndAlso a.视频参数_超分_滤镜叠加策略组.Length > 0 Then
            Dim 超分序号 = 1
            For Each 单片 In a.视频参数_超分_滤镜叠加策略组
                Dim 文本 = 格式化超分单片(单片)
                If 文本 <> "" Then
                    添加总览文本行(sb, $"超分 {超分序号}：{文本}")
                    超分序号 += 1
                End If
            Next
        ElseIf 超分单片有设置(a.视频参数_超分_直接面板) Then
            添加总览文本行(sb, "超分：" & 格式化超分单片(a.视频参数_超分_直接面板))
        End If

        If a.视频参数_降噪_方式 <> 预设数据_v6.降噪方式.未选择 Then
            添加总览文本行(sb, "降噪方式：" & 格式化枚举名称(a.视频参数_降噪_方式))
            添加总览文本行(sb, "降噪参数1：" & a.视频参数_降噪_参数1)
            添加总览文本行(sb, "降噪参数2：" & a.视频参数_降噪_参数2)
            添加总览文本行(sb, "降噪参数3：" & a.视频参数_降噪_参数3)
            添加总览文本行(sb, "降噪参数4：" & a.视频参数_降噪_参数4)
        End If
        If a.视频参数_锐化_方式 <> 预设数据_v6.锐化方式.未选择 Then
            添加总览文本行(sb, "锐化方式：" & 格式化枚举名称(a.视频参数_锐化_方式))
            添加总览文本行(sb, "锐化参数1：" & a.视频参数_锐化_参数1)
            添加总览文本行(sb, "锐化参数2：" & a.视频参数_锐化_参数2)
            添加总览文本行(sb, "锐化参数3：" & a.视频参数_锐化_参数3)
        End If
        If a.视频参数_胶片颗粒_方式 <> 预设数据_v6.胶片颗粒方式.未选择 Then
            添加总览文本行(sb, "胶片颗粒方式：" & 格式化枚举名称(a.视频参数_胶片颗粒_方式))
            添加总览文本行(sb, "胶片颗粒参数1：" & a.视频参数_胶片颗粒_参数1)
            添加总览文本行(sb, "胶片颗粒参数2：" & a.视频参数_胶片颗粒_参数2)
            添加总览文本行(sb, "胶片颗粒参数3：" & a.视频参数_胶片颗粒_参数3)
            添加总览文本行(sb, "胶片颗粒参数4：" & a.视频参数_胶片颗粒_参数4)
        End If
        If a.视频参数_平滑断层_方式 <> 预设数据_v6.平滑断层方式.未选择 Then
            添加总览文本行(sb, "平滑断层方式：" & 格式化枚举名称(a.视频参数_平滑断层_方式))
            添加总览文本行(sb, "平滑断层参数1：" & a.视频参数_平滑断层_参数1)
            添加总览文本行(sb, "平滑断层参数2：" & a.视频参数_平滑断层_参数2)
            添加总览文本行(sb, "平滑断层参数3：" & a.视频参数_平滑断层_参数3)
            添加总览文本行(sb, "平滑断层参数4：" & a.视频参数_平滑断层_参数4)
        End If
        If a.视频参数_处理扫描方式 <> 预设数据_v6.扫描方式.未选择 Then 添加总览文本行(sb, "扫描方式：" & 格式化枚举名称(a.视频参数_处理扫描方式))
        If a.视频参数_画面翻转_角度翻转 <> 预设数据_v6.画面翻转角度.未选择 Then 添加总览文本行(sb, "角度翻转：" & 格式化枚举名称(a.视频参数_画面翻转_角度翻转))
        If a.视频参数_画面翻转_镜像翻转 <> 预设数据_v6.画面翻转镜像.未选择 Then 添加总览文本行(sb, "镜像翻转：" & 格式化枚举名称(a.视频参数_画面翻转_镜像翻转))

        If a.视频参数_烧录字幕_滤镜选择 <> 预设数据_v6.烧字幕滤镜.未选择 OrElse Not String.IsNullOrWhiteSpace(a.视频参数_烧录字幕_自己写滤镜取代所有设置) Then
            Dim 字幕 As New List(Of String)
            If a.视频参数_烧录字幕_滤镜选择 <> 预设数据_v6.烧字幕滤镜.未选择 Then 字幕.Add("滤镜：" & 格式化枚举名称(a.视频参数_烧录字幕_滤镜选择))
            If a.视频参数_烧录字幕_字幕来源是外部文件 <> 预设数据_v6.烧字幕来源.未选择 Then 字幕.Add("字幕来源：" & 格式化枚举名称(a.视频参数_烧录字幕_字幕来源是外部文件))
            If a.视频参数_烧录字幕_字幕格式优先级 IsNot Nothing Then
                Dim 字幕格式 = a.视频参数_烧录字幕_字幕格式优先级.Where(Function(x) x <> 预设数据_v6.烧字幕格式.未选择).Select(Function(x) 格式化枚举名称(x)).Where(Function(x) x <> "").ToList()
                If 字幕格式.Count > 0 Then 字幕.Add("字幕格式优先级：" & String.Join("，", 字幕格式))
            End If
            If a.视频参数_烧录字幕_外部字幕文件名 <> "" Then 字幕.Add("外部字幕文件名：" & a.视频参数_烧录字幕_外部字幕文件名)
            If a.视频参数_烧录字幕_外部字幕文件夹位置 <> "" Then 字幕.Add("外部字幕文件夹位置：" & a.视频参数_烧录字幕_外部字幕文件夹位置)
            If a.视频参数_烧录字幕_指定内嵌的流 <> "" Then 字幕.Add("指定内嵌的流：" & a.视频参数_烧录字幕_指定内嵌的流)
            If a.视频参数_烧录字幕_字体文件夹 <> "" Then 字幕.Add("字体文件夹：" & a.视频参数_烧录字幕_字体文件夹)
            If a.视频参数_烧录字幕_基本样式_名称 <> "" Then 字幕.Add("字体：" & a.视频参数_烧录字幕_基本样式_名称)
            If a.视频参数_烧录字幕_基本样式_大小 > 0 Then 字幕.Add("大小：" & a.视频参数_烧录字幕_基本样式_大小.ToString(CultureInfo.InvariantCulture))
            If a.视频参数_烧录字幕_基本样式_粗体 Then 字幕.Add("粗体")
            If a.视频参数_烧录字幕_基本样式_斜体 Then 字幕.Add("斜体")
            If a.视频参数_烧录字幕_基本样式_下划线 Then 字幕.Add("下划线")
            If a.视频参数_烧录字幕_基本样式_删除线 Then 字幕.Add("删除线")
            If a.视频参数_烧录字幕_边框样式 <> 预设数据_v6.烧字幕边框样式.未选择 Then 字幕.Add("边框样式：" & 格式化枚举名称(a.视频参数_烧录字幕_边框样式))
            If a.视频参数_烧录字幕_描边宽度 <> "" Then 字幕.Add("描边宽度：" & a.视频参数_烧录字幕_描边宽度)
            If a.视频参数_烧录字幕_阴影距离 <> "" Then 字幕.Add("阴影距离：" & a.视频参数_烧录字幕_阴影距离)
            添加字幕颜色总览(字幕, "主要颜色", a.视频参数_烧录字幕_主要颜色)
            添加字幕颜色总览(字幕, "次要颜色", a.视频参数_烧录字幕_次要颜色)
            添加字幕颜色总览(字幕, "描边颜色", a.视频参数_烧录字幕_描边颜色)
            添加字幕颜色总览(字幕, "背景颜色", a.视频参数_烧录字幕_背景颜色)
            If a.视频参数_烧录字幕_对齐方位 <> 预设数据_v6.烧字幕对齐方位.未选择 Then 字幕.Add("对齐方位：" & 格式化枚举名称(a.视频参数_烧录字幕_对齐方位))
            If a.视频参数_烧录字幕_垂直边距 <> "" Then 字幕.Add("垂直边距：" & a.视频参数_烧录字幕_垂直边距)
            If a.视频参数_烧录字幕_左边距 <> "" Then 字幕.Add("左边距：" & a.视频参数_烧录字幕_左边距)
            If a.视频参数_烧录字幕_右边距 <> "" Then 字幕.Add("右边距：" & a.视频参数_烧录字幕_右边距)
            If a.视频参数_烧录字幕_字距 <> "" Then 字幕.Add("字距：" & a.视频参数_烧录字幕_字距)
            If a.视频参数_烧录字幕_行距 <> "" Then 字幕.Add("行距：" & a.视频参数_烧录字幕_行距)
            If a.视频参数_烧录字幕_补充样式 <> "" Then 字幕.Add("补充样式：" & a.视频参数_烧录字幕_补充样式)
            If a.视频参数_烧录字幕_自己写滤镜取代所有设置 <> "" Then 字幕.Add("自己写滤镜：" & a.视频参数_烧录字幕_自己写滤镜取代所有设置)
            If 字幕.Count > 0 Then 添加总览文本行(sb, "烧录字幕：" & String.Join("；", 字幕))
            If 内置烧字幕缺少必要来源设置(a) Then 添加总览文本行(sb, "警告：烧录字幕未指定可用字幕来源；内置 subtitles/ass 滤镜不会写入命令")
        End If

        If a.视频参数_比特率_控制方式 <> 预设数据_v6.视频全局质量控制方式.未选择 Then 添加总览文本行(sb, "质量/比特率控制方法：" & 格式化质量控制方式(a.视频参数_比特率_控制方式))
        If a.视频参数_比特率_控制方式 = 预设数据_v6.视频全局质量控制方式.TPE AndAlso 全局质量参数已设置(a) Then
            添加总览文本行(sb, "警告：二次编码不兼容 CRF/CQ/QP/global_quality 等全局质量参数；实际命令会忽略这些参数，请使用基础码率")
        End If
        If a.视频参数_比特率_控制方式 = 预设数据_v6.视频全局质量控制方式.TPE AndAlso Not 二次编码基础码率有效(a) Then
            添加总览文本行(sb, "警告：二次编码需要填写基础码率；未填写时会按普通单次编码生成命令")
        End If
        If a.视频参数_比特率_控制方式 = 预设数据_v6.视频全局质量控制方式.TPE AndAlso 二次编码明显不兼容(a) Then
            添加总览文本行(sb, "警告：二次编码需要实际重编码视频流；复制流、禁用视频或图片编码不会生成二次编码命令")
        End If
        If Not String.IsNullOrWhiteSpace(a.视频参数_质量控制_参数名) Then
            Dim 参数名 = a.视频参数_质量控制_参数名.TrimStart("-"c)
            If Not String.IsNullOrWhiteSpace(a.视频参数_质量控制_值) Then
                添加总览文本行(sb, "质量控制参数：" & 参数名 & " = " & a.视频参数_质量控制_值)
            Else
                添加总览文本行(sb, "质量控制参数：" & 参数名 & "（未填写值）")
            End If
        ElseIf Not String.IsNullOrWhiteSpace(a.视频参数_质量控制_值) Then
            添加总览文本行(sb, "质量控制值：" & a.视频参数_质量控制_值)
        End If
        添加总览文本行(sb, "基础码率：" & a.视频参数_比特率_基础)
        添加总览文本行(sb, "最低码率：" & a.视频参数_比特率_最低值)
        添加总览文本行(sb, "最高码率：" & a.视频参数_比特率_最高值)
        添加总览文本行(sb, "缓冲区大小：" & a.视频参数_比特率_缓冲区)
        添加总览文本行(sb, "进阶质量控制：" & a.视频参数_质量控制_进阶参数集)

        添加总览文本行(sb, "像素格式：" & a.视频参数_色彩管理_像素格式)
        添加总览文本行(sb, "预先转换像素格式：" & a.视频参数_色彩管理_像素格式预先转换)
        添加总览文本行(sb, "色彩转换滤镜：" & a.视频参数_色彩管理_滤镜选择)
        添加总览文本行(sb, "矩阵系数 & 颜色格式：" & a.视频参数_色彩管理_矩阵系数)
        添加总览文本行(sb, "色域：" & a.视频参数_色彩管理_色域)
        添加总览文本行(sb, "传输特性：" & a.视频参数_色彩管理_传输特性)
        添加总览文本行(sb, "色彩范围：" & a.视频参数_色彩管理_范围)
        添加总览文本行(sb, "色调映射算法：" & a.视频参数_色彩管理_色调映射算法)
        添加总览文本行(sb, "色彩管理处理方式：" & a.视频参数_色彩管理_处理方式)
        If a.视频参数_色彩管理_启用调整亮度 Then 添加总览文本行(sb, "亮度调整：" & If(a.视频参数_色彩管理_亮度 = "", "已启用", a.视频参数_色彩管理_亮度))
        If a.视频参数_色彩管理_启用调整对比度 Then 添加总览文本行(sb, "对比度调整：" & If(a.视频参数_色彩管理_对比度 = "", "已启用", a.视频参数_色彩管理_对比度))
        If a.视频参数_色彩管理_启用调整饱和度 Then 添加总览文本行(sb, "饱和度调整：" & If(a.视频参数_色彩管理_饱和度 = "", "已启用", a.视频参数_色彩管理_饱和度))
        If a.视频参数_色彩管理_启用调整伽马 Then 添加总览文本行(sb, "伽马调整：" & If(a.视频参数_色彩管理_伽马 = "", "已启用", a.视频参数_色彩管理_伽马))

        添加总览文本行(sb, "音频编码器：" & 获取音频编码器总览显示名(a.音频参数_编码器_代号))
        添加总览文本行(sb, "音频比特率：" & a.音频参数_比特率)
        添加音频质量控制总览(sb, "音频质量控制", a.音频参数_质量参数名, a.音频参数_质量值)
        添加音频质量控制总览(sb, "音频质量控制 2", a.音频参数_质量参数名2, a.音频参数_质量值2)
        添加总览文本行(sb, "声道布局：" & a.音频参数_声道数)
        添加总览文本行(sb, "音频位深度：" & a.音频参数_位深度)
        添加总览文本行(sb, "采样率：" & a.音频参数_采样率)
        If a.音频参数_响度标准化_启用调整目标响度 Then 添加总览文本行(sb, "响度标准化目标：" & If(a.音频参数_响度标准化_目标响度 = "", "已启用", a.音频参数_响度标准化_目标响度))
        If a.音频参数_响度标准化_启用调整动态范围 Then 添加总览文本行(sb, "响度动态范围：" & If(a.音频参数_响度标准化_动态范围 = "", "已启用", a.音频参数_响度标准化_动态范围))
        If a.音频参数_响度标准化_启用调整峰值电平 Then 添加总览文本行(sb, "响度峰值电平：" & If(a.音频参数_响度标准化_峰值电平 = "", "已启用", a.音频参数_响度标准化_峰值电平))
        添加复制流兼容性总览(sb, a)
        添加CUDA滤镜兼容性总览(sb, a)
        添加音视频容器兼容性总览(sb, a)

        If a.剪辑区间_方法 <> 预设数据_v6.剪辑方法.未知 Then 添加总览文本行(sb, "剪辑区间方法：" & 格式化剪辑方法(a.剪辑区间_方法))
        If a.剪辑区间_方法 = 预设数据_v6.剪辑方法.未知 AndAlso (a.剪辑区间_入点 <> "" OrElse a.剪辑区间_出点 <> "") Then 添加总览文本行(sb, "警告：指定了剪辑范围却没有指定剪辑方法，不会进行剪辑")
        添加总览文本行(sb, "剪辑入点：" & a.剪辑区间_入点)
        添加总览文本行(sb, "剪辑出点：" & a.剪辑区间_出点)
        If Not String.IsNullOrWhiteSpace(a.剪辑区间_向前解码多久秒) Then 添加总览文本行(sb, "快速响应的精剪向前解码 " & a.剪辑区间_向前解码多久秒 & " 秒")

        添加总览文本行(sb, "自定义开头参数：" & a.自定义参数_开头参数)
        添加总览文本行(sb, "自定义之前参数：" & a.自定义参数_之前参数)
        添加总览文本行(sb, "自定义视频滤镜：" & a.自定义参数_视频滤镜)
        添加总览文本行(sb, "自定义音频滤镜：" & a.自定义参数_音频滤镜)
        添加总览文本行(sb, "自定义视频参数：" & a.自定义参数_视频参数)
        添加总览文本行(sb, "自定义音频参数：" & a.自定义参数_音频参数)
        添加总览文本行(sb, "自定义之后参数：" & a.自定义参数_之后参数)
        添加总览文本行(sb, "自定义最后参数：" & a.自定义参数_最后参数)

        If a.流控制_将视频参数应用于指定流.Length > 0 Then 添加总览文本行(sb, "应用视频参数到流：" & 格式化字符串数组(a.流控制_将视频参数应用于指定流))
        If a.流控制_启用保留其他视频流 Then 添加总览文本行(sb, "已选择保留其他视频流")
        If a.流控制_将音频参数应用于指定流.Length > 0 Then 添加总览文本行(sb, "应用音频参数到流：" & 格式化字符串数组(a.流控制_将音频参数应用于指定流))
        If a.流控制_启用保留其他音频流 Then 添加总览文本行(sb, "已选择保留其他音频流")
        If a.流控制_将字幕参数应用于指定流.Length > 0 Then
            添加总览文本行(sb, "使用这些文件的这些字幕：" & 格式化字符串数组(a.流控制_将字幕参数应用于指定流))
            If a.流控制_如何操作指定的字幕 > 0 Then 添加总览文本行(sb, "字幕操作：" & 格式化字幕流操作(a.流控制_如何操作指定的字幕))
        End If
        If a.流控制_启用保留其他字幕流 Then 添加总览文本行(sb, "已选择保留其他字幕流")
        If a.流控制_自动混流SRT Then 添加总览文本行(sb, "自动混流同名 SRT 字幕文件")
        If a.流控制_自动混流ASS Then 添加总览文本行(sb, "自动混流同名 ASS 字幕文件")
        If a.流控制_自动混流SSA Then 添加总览文本行(sb, "自动混流同名 SSA 字幕文件")
        If a.流控制_自动混流的字幕转为MOVTEXT Then 添加总览文本行(sb, "自动混流的字幕转为 mov_text 编码")
        添加字幕容器兼容性总览(sb, a)
        If a.流控制_元数据选项 > 0 Then 添加总览文本行(sb, "元数据选项：" & 格式化元数据选项(a.流控制_元数据选项))
        If a.流控制_章节选项 > 0 Then 添加总览文本行(sb, "章节选项：" & 格式化章节选项(a.流控制_章节选项))
        If a.流控制_附件选项 > 0 Then 添加总览文本行(sb, "附件选项：" & 格式化附件选项(a.流控制_附件选项))

        Dim 元数据文本 = 格式化元数据总览(a.元数据_要写入的信息)
        If 元数据文本 <> "" Then 添加总览文本行(sb, "写入元数据：" & 元数据文本)
        If a.章节_来源 <> 预设数据_v6.章节来源.未选择 Then 添加总览文本行(sb, "章节来源：" & 格式化枚举名称(a.章节_来源))
        添加总览文本行(sb, "章节文件：" & a.章节_文件路径)
        Dim 附件文本 = 格式化附件总览(a.附件_要写入的附件)
        If 附件文本 <> "" Then 添加总览文本行(sb, "写入附件：" & 附件文本)
        添加附件容器兼容性总览(sb, a)

        If a.滤镜排序系统 IsNot Nothing AndAlso a.滤镜排序系统.Length > 0 Then
            Dim index = 1
            For Each f In a.滤镜排序系统
                If f Is Nothing Then Continue For
                If Not f.是自定义滤镜 AndAlso f.滤镜标识符 = 预设数据_v6.滤镜排序单片结构.标识符枚举.未设置 Then Continue For
                Dim 名称 = If(f.显示名称 <> "", f.显示名称, 格式化枚举名称(f.滤镜标识符))
                Dim 片段 = 获取滤镜片段(a, f)
                Dim 流类型文本 = If(f.滤镜目标流类型 = 预设数据_v6.滤镜排序单片结构.流类型.未设定, "", $" [{格式化枚举名称(f.滤镜目标流类型)}]")
                添加总览文本行(sb, $"滤镜排序 {index}：{名称}{流类型文本} {片段}".Trim())
                index += 1
            Next
        End If

        设置参数总览文本(MTB, If(sb.Length > 0, sb.ToString(), "未设置参数"))
    End Sub

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

    Private Shared Function 格式化字幕流操作(value As Integer) As String
        Select Case value
            Case 1 : Return "复制流"
            Case 2 : Return "转为 mov_text 编码"
            Case 3 : Return "转为 srt 编码"
            Case 4 : Return "转为 ass 编码"
            Case 5 : Return "转为 ssa 编码"
        End Select
        Return value.ToString(CultureInfo.InvariantCulture)
    End Function

    Private Shared Function 格式化元数据选项(value As Integer) As String
        Select Case value
            Case 1 : Return "保留元数据"
            Case 2 : Return "清除元数据"
            Case 3 : Return "清除更多元数据"
        End Select
        Return value.ToString(CultureInfo.InvariantCulture)
    End Function

    Private Shared Function 格式化章节选项(value As Integer) As String
        Select Case value
            Case 1 : Return "保留章节"
            Case 2 : Return "清除章节"
        End Select
        Return value.ToString(CultureInfo.InvariantCulture)
    End Function

    Private Shared Function 格式化附件选项(value As Integer) As String
        Select Case value
            Case 1 : Return "保留附件"
            Case 2 : Return "清除附件"
        End Select
        Return value.ToString(CultureInfo.InvariantCulture)
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
        If a.流控制_附件选项 = 1 AndAlso Not 支持常规附件 Then
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

    Public Shared Sub 刷新参数总览(ui As Form_v6_参数面板)
        If ui Is Nothing Then Exit Sub
        同步全部内置滤镜到排序(ui, False)
        Dim a = 从面板创建预设(ui)
        ui.私有界面_滤镜排序.刷新局部预览(a)
        a.滤镜排序系统 = ui.私有界面_滤镜排序.获取排序数据().ToArray()
        显示参数总览(ui.私有界面_参数总览.MTB_参数总览, a)
        ui.私有界面_参数总览.MTB_命令行模板.Text = 生成命令行展示文本(a, 输入占位符, 输出占位符)
    End Sub

    Public Shared Sub 同步全部内置滤镜到排序(ui As Form_v6_参数面板, Optional 刷新 As Boolean = True)
        If ui Is Nothing Then Exit Sub
        Dim 排序页 = ui.私有界面_滤镜排序
        Dim a = 从面板创建预设但不读排序(ui)

        更新排序项(排序页, a, 预设数据_v6.滤镜排序单片结构.标识符枚举.裁剪, Not String.IsNullOrWhiteSpace(a.视频参数_分辨率_裁剪滤镜参数))
        更新排序项(排序页, a, 预设数据_v6.滤镜排序单片结构.标识符枚举.缩放, Not String.IsNullOrWhiteSpace(a.视频参数_分辨率) OrElse Not String.IsNullOrWhiteSpace(a.视频参数_分辨率自动计算_宽度) OrElse Not String.IsNullOrWhiteSpace(a.视频参数_分辨率自动计算_高度))
        更新排序项(排序页, a, 预设数据_v6.滤镜排序单片结构.标识符枚举.抽帧, 抽帧参数已设置(a))
        更新排序项(排序页, a, 预设数据_v6.滤镜排序单片结构.标识符枚举.插帧, Not String.IsNullOrWhiteSpace(a.视频参数_插帧_目标帧率))
        更新排序项(排序页, a, 预设数据_v6.滤镜排序单片结构.标识符枚举.动态模糊, Not String.IsNullOrWhiteSpace(a.视频参数_动态模糊_连续混合帧数))
        更新排序项(排序页, a, 预设数据_v6.滤镜排序单片结构.标识符枚举.超分, 超分单片有设置(a.视频参数_超分_直接面板) OrElse a.视频参数_超分_滤镜叠加策略组.Length > 0)
        更新排序项(排序页, a, 预设数据_v6.滤镜排序单片结构.标识符枚举.降噪, a.视频参数_降噪_方式 <> 预设数据_v6.降噪方式.未选择)
        更新排序项(排序页, a, 预设数据_v6.滤镜排序单片结构.标识符枚举.锐化, a.视频参数_锐化_方式 <> 预设数据_v6.锐化方式.未选择)
        更新排序项(排序页, a, 预设数据_v6.滤镜排序单片结构.标识符枚举.胶片颗粒, a.视频参数_胶片颗粒_方式 <> 预设数据_v6.胶片颗粒方式.未选择)
        更新排序项(排序页, a, 预设数据_v6.滤镜排序单片结构.标识符枚举.平滑断层, a.视频参数_平滑断层_方式 <> 预设数据_v6.平滑断层方式.未选择)
        更新排序项(排序页, a, 预设数据_v6.滤镜排序单片结构.标识符枚举.扫描方式, a.视频参数_处理扫描方式 <> 预设数据_v6.扫描方式.未选择)
        更新排序项(排序页, a, 预设数据_v6.滤镜排序单片结构.标识符枚举.画面翻转, a.视频参数_画面翻转_角度翻转 <> 预设数据_v6.画面翻转角度.未选择 OrElse a.视频参数_画面翻转_镜像翻转 <> 预设数据_v6.画面翻转镜像.未选择)
        更新排序项(排序页, a, 预设数据_v6.滤镜排序单片结构.标识符枚举.烧录字幕, 烧字幕滤镜已设置(a))
        更新排序项(排序页, a, 预设数据_v6.滤镜排序单片结构.标识符枚举.像素格式预先转换, a.视频参数_色彩管理_像素格式预先转换 <> "")
        更新排序项(排序页, a, 预设数据_v6.滤镜排序单片结构.标识符枚举.色彩转换, 色彩转换滤镜已设置(a))
        更新排序项(排序页, a, 预设数据_v6.滤镜排序单片结构.标识符枚举.调色, a.视频参数_色彩管理_启用调整亮度 OrElse a.视频参数_色彩管理_启用调整对比度 OrElse a.视频参数_色彩管理_启用调整饱和度 OrElse a.视频参数_色彩管理_启用调整伽马)
        更新排序项(排序页, a, 预设数据_v6.滤镜排序单片结构.标识符枚举.音频响度标准化, a.音频参数_响度标准化_启用调整目标响度 OrElse a.音频参数_响度标准化_启用调整动态范围 OrElse a.音频参数_响度标准化_启用调整峰值电平)
        更新排序项(排序页, a, 预设数据_v6.滤镜排序单片结构.标识符枚举.音频格式转换, a.音频参数_声道数 <> "")
        更新排序项(排序页, a, 预设数据_v6.滤镜排序单片结构.标识符枚举.音频重采样, False)

        If 刷新 Then 刷新参数总览(ui)
    End Sub

    Public Shared Sub 重置滤镜对应页面(ui As Form_v6_参数面板, 标识符 As 预设数据_v6.滤镜排序单片结构.标识符枚举, Optional 刷新 As Boolean = True)
        If ui Is Nothing Then Exit Sub
        Select Case 标识符
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.裁剪
                ui.私有界面_画面帧.MTB_画面裁剪参数.Text = ""
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.缩放
                ui.私有界面_画面帧.MCB_直接指定分辨率.Text = ""
                ui.私有界面_画面帧.MCB_宽度缩放.Text = ""
                ui.私有界面_画面帧.MCB_高度缩放.Text = ""
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.抽帧
                With ui.私有界面_画面帧.私有窗口_抽帧参数
                    .MCK_抽帧总开关.Checked = False
                    .MTB_连续丢帧数量.Text = ""
                    .MTB_连续相似要求.Text = ""
                    .MTB_最大变化占比.Text = ""
                    .MCB_高阈值.Text = ""
                    .MCB_低阈值.Text = ""
                End With
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.插帧
                With ui.私有界面_画面帧.私有窗口_插帧参数
                    .MCK_插帧总开关.Checked = False
                    .MTB_目标帧率.Text = ""
                    .MCB_插帧模式.Text = ""
                    .MCB_运动估计模式.Text = ""
                    .MCB_运动估计算法.Text = ""
                    .MCB_运动补偿模式.Text = ""
                    .MCK_可变块大小的运动补偿.Checked = False
                    .MTB_块大小.Text = ""
                    .MTB_搜索范围.Text = ""
                    .MTB_场景变化检测强度.Text = ""
                End With
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.动态模糊
                With ui.私有界面_画面帧.私有窗口_动态模糊
                    .MCK_动态模糊总开关.Checked = False
                    .MTB_连续混合帧数.Text = ""
                    .MTB_每帧的权重.Text = ""
                    .MTB_输出缩放系数.Text = ""
                    .MTB_处理哪些颜色平面.Text = ""
                End With
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.降噪
                With ui.私有界面_画面帧.私有窗口_降噪
                    .MCK_降噪总开关.Checked = False
                    .MCB_滤镜选择.SelectedIndex = -1
                    ResetTrackValue(.ETB_降噪参数1)
                    ResetTrackValue(.ETB_降噪参数2)
                    ResetTrackValue(.ETB_降噪参数3)
                    ResetTrackValue(.ETB_降噪参数4)
                End With
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.锐化
                With ui.私有界面_画面帧.私有窗口_锐化
                    .MCK_锐化总开关.Checked = False
                    .MCB_滤镜选择.SelectedIndex = -1
                    ResetTrackValue(.ETB_锐化参数1)
                    ResetTrackValue(.ETB_锐化参数2)
                    ResetTrackValue(.ETB_锐化参数3)
                End With
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.胶片颗粒
                With ui.私有界面_画面帧.私有窗口_胶片颗粒
                    .MCK_胶片颗粒总开关.Checked = False
                    .MCB_滤镜选择.SelectedIndex = -1
                    ResetTrackValue(.ETB_胶片颗粒参数1)
                    ResetTrackValue(.ETB_胶片颗粒参数2)
                    ResetTrackValue(.ETB_胶片颗粒参数3)
                    ResetTrackValue(.ETB_胶片颗粒参数4)
                End With
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.平滑断层
                With ui.私有界面_画面帧.私有窗口_平滑断层
                    .MCK_平滑断层总开关.Checked = False
                    .MCB_滤镜选择.SelectedIndex = -1
                    ResetTrackValue(.ETB_平滑断层参数1)
                    ResetTrackValue(.ETB_平滑断层参数2)
                    ResetTrackValue(.ETB_平滑断层参数3)
                    ResetTrackValue(.ETB_平滑断层参数4)
                End With
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.扫描方式
                ui.私有界面_画面帧.私有窗口_扫描方式.MCK_扫描方式总开关.Checked = False
                ui.私有界面_画面帧.私有窗口_扫描方式.MCB_扫描方式.SelectedIndex = -1
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.画面翻转
                ui.私有界面_画面帧.私有窗口_画面翻转.MCK_画面翻转总开关.Checked = False
                ui.私有界面_画面帧.私有窗口_画面翻转.MCB_角度翻转.SelectedIndex = -1
                ui.私有界面_画面帧.私有窗口_画面翻转.MCB_镜像翻转.SelectedIndex = -1
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.烧录字幕
                ui.私有界面_画面帧.私有窗口_烧录字幕.重置所有选项()
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.超分
                With ui.私有界面_画面帧.私有窗口_着色器超分
                    .MCK_超分总开关.Checked = False
                    .MTB_宽度.Text = ""
                    .MTB_高度.Text = ""
                    .MCB_上采样算法.Text = ""
                    .MCB_下采样算法.Text = ""
                    .MTB_抗振铃强度.Text = ""
                    .MCB_着色器文件路径.Text = ""
                    .策略组数据.Clear()
                    .刷新策略组列表()
                End With
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.像素格式预先转换
                ui.私有界面_色彩管理.MCB_像素格式预先转换.Text = ""
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.色彩转换
                With ui.私有界面_色彩管理
                    .MCB_色彩管理_选择滤镜.Text = ""
                    .MCB_色彩管理_矩阵系数.Text = ""
                    .MCB_色彩管理_色域.Text = ""
                    .MCB_色彩管理_传输特性.Text = ""
                    .MCB_色彩管理_色彩范围.Text = ""
                    .MCB_色彩管理_色调映射算法.Text = ""
                    .MCB_色彩管理_色彩空间操作方式.Text = ""
                End With
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.调色
                With ui.私有界面_色彩管理
                    .MCK_启用亮度调整.Checked = False
                    .MCK_启用对比度调整.Checked = False
                    .MCK_启用饱和度调整.Checked = False
                    .MCK_启用伽马调整.Checked = False
                    ResetTrackValue(.ETB_亮度)
                    ResetTrackValue(.ETB_对比度, 1)
                    ResetTrackValue(.ETB_饱和度, 1)
                    ResetTrackValue(.ETB_伽马, 1)
                End With
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.音频响度标准化
                With ui.私有界面_音频参数
                    .MCK_启用目标响度.Checked = False
                    .MCK_启用动态范围.Checked = False
                    .MCK_启用峰值电平.Checked = False
                    ResetTrackValue(.ETB_目标响度, -24)
                    ResetTrackValue(.ETB_动态范围, 1)
                    ResetTrackValue(.ETB_峰值电平, -1)
                End With
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.音频格式转换
                ui.私有界面_音频参数.MCB_声道布局.Text = ""
            Case 预设数据_v6.滤镜排序单片结构.标识符枚举.音频重采样
                ui.私有界面_音频参数.MCB_采样率.Text = ""
        End Select
        If 刷新 Then 刷新参数总览(ui)
    End Sub

End Class
