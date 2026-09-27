Imports System.Globalization
Imports System.IO
Imports System.Text
Imports System.Text.Json
Imports System.Text.RegularExpressions
Imports LakeUI

Partial Public Class 预设管理_v6

    Private Shared Sub 储存画面帧(a As 预设数据_v6, ui As Form_v6_参数面板)
        With ui.私有界面_画面帧
            a.视频参数_分辨率 = .MCB_直接指定分辨率.Text
            a.视频参数_分辨率自动计算_宽度 = .MCB_宽度缩放.Text
            a.视频参数_分辨率自动计算_高度 = .MCB_高度缩放.Text
            a.视频参数_分辨率自动计算_缩放滤镜 = .获取缩放滤镜标识符()
            a.视频参数_分辨率自动计算_缩放算法 = .MCB_指定缩放算法.Text
            a.视频参数_帧速率 = .MCB_直接指定帧率.Text
            a.视频参数_帧速率模式 = If(.MCB_强调帧率模式.SelectedIndex = 1, "cfr", If(.MCB_强调帧率模式.SelectedIndex = 2, "vfr", ""))
            a.视频参数_分辨率_裁剪滤镜参数 = .MTB_画面裁剪参数.Text
            储存滤镜子窗口(a, .私有窗口_抽帧参数, .私有窗口_简易插帧参数, .私有窗口_NV_FRUC_插帧参数, .私有窗口_动态模糊, .私有窗口_着色器超分, .私有窗口_降噪, .私有窗口_锐化, .私有窗口_胶片颗粒, .私有窗口_平滑断层, .私有窗口_扫描方式, .私有窗口_画面翻转, .私有窗口_烧录字幕)
        End With
    End Sub

    Private Shared Sub 显示画面帧(a As 预设数据_v6, ui As Form_v6_参数面板)
        With ui.私有界面_画面帧
            .MCB_直接指定分辨率.Text = a.视频参数_分辨率
            .MCB_宽度缩放.Text = a.视频参数_分辨率自动计算_宽度
            .MCB_高度缩放.Text = a.视频参数_分辨率自动计算_高度
            Select Case If(a.视频参数_分辨率自动计算_缩放滤镜, "").Trim().ToLowerInvariant()
                Case "scale_cuda"
                    .MCB_指定缩放滤镜.SelectedIndex = 2
                Case "scale"
                    .MCB_指定缩放滤镜.SelectedIndex = 1
                Case Else
                    .MCB_指定缩放滤镜.SelectedIndex = -1
            End Select
            .更新缩放滤镜交互()
            .MCB_指定缩放算法.Text = a.视频参数_分辨率自动计算_缩放算法
            .更新缩放滤镜交互()
            .MCB_直接指定帧率.Text = a.视频参数_帧速率
            Select Case If(a.视频参数_帧速率模式, "").Trim().ToLowerInvariant()
                Case "cfr", "固定帧率 cfr"
                    .MCB_强调帧率模式.SelectedIndex = 1
                Case "vfr", "动态帧率 vfr"
                    .MCB_强调帧率模式.SelectedIndex = 2
                Case Else
                    .MCB_强调帧率模式.SelectedIndex = 0
            End Select
            .MTB_画面裁剪参数.Text = a.视频参数_分辨率_裁剪滤镜参数
            显示滤镜子窗口(a, .私有窗口_抽帧参数, .私有窗口_简易插帧参数, .私有窗口_NV_FRUC_插帧参数, .私有窗口_动态模糊, .私有窗口_着色器超分, .私有窗口_降噪, .私有窗口_锐化, .私有窗口_胶片颗粒, .私有窗口_平滑断层, .私有窗口_扫描方式, .私有窗口_画面翻转, .私有窗口_烧录字幕)
        End With
    End Sub

    Private Shared Sub 储存滤镜子窗口(a As 预设数据_v6,
                                  抽帧 As Form_v6_参数面板_抽帧参数,
                                  插帧 As Form_v6_参数面板_插帧_简易,
                                  NVFRUC As Form_v6_参数面板_插帧_NV_FRUC,
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

        If NVFRUC.MCK_插帧总开关.Checked Then
            a.视频参数_NV_FRUC_目标帧率 = NVFRUC.MCB_目标帧率.Text
            a.视频参数_NV_FRUC_质量和速度 = NVFRUC.MCB_质量和速度.Text
            a.视频参数_NV_FRUC_网格大小 = NVFRUC.MCB_网格大小.Text
        Else
            a.视频参数_NV_FRUC_目标帧率 = ""
            a.视频参数_NV_FRUC_质量和速度 = ""
            a.视频参数_NV_FRUC_网格大小 = ""
        End If

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
                                  插帧 As Form_v6_参数面板_插帧_简易,
                                  NVFRUC As Form_v6_参数面板_插帧_NV_FRUC,
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

        NVFRUC.MCK_插帧总开关.Checked = a.视频参数_NV_FRUC_目标帧率 <> ""
        设置组合框文本并尝试选中(NVFRUC.MCB_目标帧率, a.视频参数_NV_FRUC_目标帧率)
        设置组合框文本并尝试选中(NVFRUC.MCB_质量和速度, a.视频参数_NV_FRUC_质量和速度)
        设置组合框文本并尝试选中(NVFRUC.MCB_网格大小, a.视频参数_NV_FRUC_网格大小)

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
    Private Shared Function 构造缩放滤镜(a As 预设数据_v6) As String
        Dim 缩放滤镜 = If(a.视频参数_分辨率自动计算_缩放滤镜, "").Trim().ToLowerInvariant()
        Dim 使用CUDA = String.Equals(缩放滤镜, "scale_cuda", StringComparison.Ordinal)
        Dim 缩放算法 = If(a.视频参数_分辨率自动计算_缩放算法, "").Trim()
        If 使用CUDA AndAlso Not {"nearest", "bilinear", "bicubic", "lanczos"}.Contains(缩放算法, StringComparer.OrdinalIgnoreCase) Then 缩放算法 = ""
        Dim 缩放参数 = If(缩放算法 = "", "", If(使用CUDA, ":interp_algo=" & 缩放算法, ":flags=" & 缩放算法))
        If a.视频参数_分辨率 <> "" Then
            Dim 分辨率 = a.视频参数_分辨率.Trim().Replace("X", "x", StringComparison.Ordinal)
            If 使用CUDA AndAlso 分辨率.Contains("x", StringComparison.Ordinal) Then
                Dim 尺寸 = 分辨率.Split("x"c)
                If 尺寸.Length >= 2 Then Return $"scale_cuda=w={尺寸(0)}:h={尺寸(1)}{缩放参数}"
            End If
            Return $"scale={分辨率.Replace("x", ":")}{缩放参数}"
        End If

        Dim 宽度 = If(a.视频参数_分辨率自动计算_宽度, "")
        Dim 高度 = If(a.视频参数_分辨率自动计算_高度, "")
        If 宽度 = "" AndAlso 高度 = "" Then Return ""
        If 宽度 = "" Then 宽度 = "-2"
        If 高度 = "" Then 高度 = "-2"
        If 使用CUDA Then Return $"scale_cuda=w={宽度}:h={高度}{缩放参数}"
        Return $"scale={宽度}:{高度}{缩放参数}"
    End Function

    Private Shared Function 构造裁剪滤镜(a As 预设数据_v6) As String
        Dim value = If(a.视频参数_分辨率_裁剪滤镜参数, "").Trim()
        Return If(value <> "", "crop=" & value, "")
    End Function

    Private Shared Function 构造抽帧滤镜(a As 预设数据_v6) As String
        Dim opts As New List(Of String)
        If a.视频参数_抽帧_max <> "" Then opts.Add("max=" & a.视频参数_抽帧_max)
        If a.视频参数_抽帧_keep <> "" Then opts.Add("keep=" & a.视频参数_抽帧_keep)
        If a.视频参数_抽帧_hi <> "" Then opts.Add("hi=" & a.视频参数_抽帧_hi)
        If a.视频参数_抽帧_lo <> "" Then opts.Add("lo=" & a.视频参数_抽帧_lo)
        If a.视频参数_抽帧_frac <> "" Then opts.Add("frac=" & a.视频参数_抽帧_frac)
        Return If(opts.Count > 0, "mpdecimate=" & String.Join(":", opts), "")
    End Function

    Private Shared Function 构造插帧滤镜(a As 预设数据_v6) As String
        If a.视频参数_插帧_目标帧率 = "" Then Return ""
        Dim opts As New List(Of String) From {$"fps={a.视频参数_插帧_目标帧率}"}
        Dim 插帧模式 = 插帧模式参数值(a.视频参数_插帧_插帧模式)
        Dim 运动估计模式 = 运动估计模式参数值(a.视频参数_插帧_运动估计模式)
        Dim 运动估计算法 = 运动估计算法参数值(a.视频参数_插帧_运动估计算法)
        Dim 运动补偿模式 = 运动补偿模式参数值(a.视频参数_插帧_运动补偿模式)
        If 插帧模式 <> "" Then opts.Add("mi_mode=" & 插帧模式)
        If 运动估计模式 <> "" Then opts.Add("me_mode=" & 运动估计模式)
        If 运动估计算法 <> "" Then opts.Add("me=" & 运动估计算法)
        If 运动补偿模式 <> "" Then opts.Add("mc_mode=" & 运动补偿模式)
        If a.视频参数_插帧_可变块大小的运动补偿 Then opts.Add("vsbmc=1")
        If a.视频参数_插帧_块大小 <> "" Then opts.Add("mb_size=" & a.视频参数_插帧_块大小)
        If a.视频参数_插帧_搜索范围 <> "" Then opts.Add("search_param=" & a.视频参数_插帧_搜索范围)
        If a.视频参数_插帧_场景变化检测强度 <> "" Then opts.Add("scd_threshold=" & a.视频参数_插帧_场景变化检测强度)
        Return "minterpolate=" & String.Join(":", opts)
    End Function

    Private Shared Function 构造NVFRUC滤镜(a As 预设数据_v6) As String
        If a Is Nothing OrElse String.IsNullOrWhiteSpace(a.视频参数_NV_FRUC_目标帧率) Then Return ""
        Dim opts As New List(Of String) From {"fps=" & a.视频参数_NV_FRUC_目标帧率.Trim()}
        Dim perf = If(a.视频参数_NV_FRUC_质量和速度, "").Trim().ToLowerInvariant()
        If {"slow", "medium", "fast"}.Contains(perf) Then opts.Add("perf=" & perf)
        Dim grid = If(a.视频参数_NV_FRUC_网格大小, "").Trim().ToLowerInvariant()
        If {"auto", "1", "2", "4", "8"}.Contains(grid) Then opts.Add("grid=" & grid)
        Return "fruc_vulkan=" & String.Join(":", opts)
    End Function

    Private Shared Function 构造动态模糊滤镜(a As 预设数据_v6) As String
        If a.视频参数_动态模糊_连续混合帧数 = "" Then Return ""
        Dim opts As New List(Of String) From {$"frames={a.视频参数_动态模糊_连续混合帧数}"}
        If a.视频参数_动态模糊_每帧权重 <> "" Then opts.Add("weights=" & a.视频参数_动态模糊_每帧权重)
        If a.视频参数_动态模糊_输出缩放系数 <> "" Then opts.Add("scale=" & a.视频参数_动态模糊_输出缩放系数)
        If a.视频参数_动态模糊_处理颜色平面 <> "" Then opts.Add("planes=" & a.视频参数_动态模糊_处理颜色平面)
        Return "tmix=" & String.Join(":", opts)
    End Function

    Private Shared Function 构造超分滤镜(a As 预设数据_v6) As String
        Dim list As New List(Of 预设数据_v6.超分数据单片结构)
        If a.视频参数_超分_滤镜叠加策略组 IsNot Nothing AndAlso a.视频参数_超分_滤镜叠加策略组.Length > 0 Then
            list.AddRange(a.视频参数_超分_滤镜叠加策略组)
        ElseIf a.视频参数_超分_直接面板 IsNot Nothing Then
            list.Add(a.视频参数_超分_直接面板)
        End If
        Dim filters As New List(Of String)
        For Each s In list
            If s Is Nothing Then Continue For
            Dim 目标宽度 = If(s.目标宽度, "")
            Dim 目标高度 = If(s.目标高度, "")
            Dim libplaceboOpts As New List(Of String)
            If 目标宽度 <> "" Then libplaceboOpts.Add("w=" & 目标宽度)
            If 目标高度 <> "" Then libplaceboOpts.Add("h=" & 目标高度)
            If s.上采样算法 <> "" Then libplaceboOpts.Add("upscaler=" & s.上采样算法)
            If s.下采样算法 <> "" Then libplaceboOpts.Add("downscaler=" & s.下采样算法)
            If s.抗振铃强度 <> "" Then libplaceboOpts.Add("antiringing=" & s.抗振铃强度)
            If s.着色器文件路径 <> "" Then libplaceboOpts.Add($"custom_shader_path='{转义字幕滤镜值(应用转译模式路径(s.着色器文件路径))}'")
            If libplaceboOpts.Count > 0 Then
                filters.Add("libplacebo=" & String.Join(":", libplaceboOpts))
            End If
        Next
        Return String.Join(",", filters)
    End Function

    Private Shared Function 构造降噪滤镜(a As 预设数据_v6) As String
        Select Case a.视频参数_降噪_方式
            Case 预设数据_v6.降噪方式.hqdn3d
                Return $"hqdn3d={JoinNonEmpty(":", a.视频参数_降噪_参数1, a.视频参数_降噪_参数2, a.视频参数_降噪_参数3, a.视频参数_降噪_参数4)}".TrimEnd("="c)
            Case 预设数据_v6.降噪方式.nlmeans
                Return $"nlmeans={JoinNamed(":", ("s", a.视频参数_降噪_参数1), ("p", a.视频参数_降噪_参数2), ("pc", a.视频参数_降噪_参数3), ("r", a.视频参数_降噪_参数4))}".TrimEnd("="c)
            Case 预设数据_v6.降噪方式.atadenoise
                Return $"atadenoise={JoinNamed(":", ("0a", a.视频参数_降噪_参数1), ("0b", a.视频参数_降噪_参数2), ("1a", a.视频参数_降噪_参数3), ("1b", a.视频参数_降噪_参数4))}".TrimEnd("="c)
            Case 预设数据_v6.降噪方式.bm3d
                Return $"bm3d={JoinNamed(":", ("sigma", a.视频参数_降噪_参数1), ("block", a.视频参数_降噪_参数2), ("bstep", a.视频参数_降噪_参数3), ("group", a.视频参数_降噪_参数4))}".TrimEnd("="c)
            Case 预设数据_v6.降噪方式.bilateral_cuda
                Return $"bilateral_cuda={JoinNamed(":", ("sigmaS", a.视频参数_降噪_参数1), ("sigmaR", a.视频参数_降噪_参数2), ("window_size", a.视频参数_降噪_参数3))}".TrimEnd("="c)
        End Select
        Return ""
    End Function

    Private Shared Function 构造锐化滤镜(a As 预设数据_v6) As String
        Select Case a.视频参数_锐化_方式
            Case 预设数据_v6.锐化方式.cas
                Return $"cas={JoinNamed(":", ("strength", a.视频参数_锐化_参数1), ("planes", a.视频参数_锐化_参数2))}".TrimEnd("="c)
            Case 预设数据_v6.锐化方式.unsharp
                Return $"unsharp={JoinNonEmpty(":", a.视频参数_锐化_参数1, a.视频参数_锐化_参数2, a.视频参数_锐化_参数3)}".TrimEnd("="c)
        End Select
        Return ""
    End Function

    Private Shared Function 构造胶片颗粒滤镜(a As 预设数据_v6) As String
        Select Case a.视频参数_胶片颗粒_方式
            Case 预设数据_v6.胶片颗粒方式.noise_全平面动态均匀颗粒
                Return 构造命名滤镜("noise",
                    ("alls", a.视频参数_胶片颗粒_参数1),
                    ("allf", If(a.视频参数_胶片颗粒_参数1 <> "", "t+u", "")),
                    ("all_seed", a.视频参数_胶片颗粒_参数2))
            Case 预设数据_v6.胶片颗粒方式.noise_亮度为主动态颗粒
                Return 构造命名滤镜("noise",
                    ("c0s", a.视频参数_胶片颗粒_参数1),
                    ("c0f", If(a.视频参数_胶片颗粒_参数1 <> "", "t+u", "")),
                    ("c1s", a.视频参数_胶片颗粒_参数2),
                    ("c1f", If(a.视频参数_胶片颗粒_参数2 <> "", "t+u", "")),
                    ("c2s", a.视频参数_胶片颗粒_参数2),
                    ("c2f", If(a.视频参数_胶片颗粒_参数2 <> "", "t+u", "")),
                    ("all_seed", a.视频参数_胶片颗粒_参数3))
            Case 预设数据_v6.胶片颗粒方式.noise_柔和平均颗粒
                Return 构造命名滤镜("noise",
                    ("alls", a.视频参数_胶片颗粒_参数1),
                    ("allf", If(a.视频参数_胶片颗粒_参数1 <> "", "t+a+u", "")),
                    ("all_seed", a.视频参数_胶片颗粒_参数2))
            Case 预设数据_v6.胶片颗粒方式.libplacebo_应用片源胶片颗粒元数据
                Return "libplacebo=apply_filmgrain=true"
        End Select
        Return ""
    End Function

    Private Shared Function JoinNonEmpty(delimiter As String, ParamArray values() As String) As String
        Return String.Join(delimiter, values.Where(Function(x) Not String.IsNullOrWhiteSpace(x)))
    End Function

    Private Shared Function JoinNamed(delimiter As String, ParamArray values() As (Name As String, Value As String)) As String
        Return String.Join(delimiter, values.Where(Function(x) Not String.IsNullOrWhiteSpace(x.Value)).Select(Function(x) $"{x.Name}={x.Value}"))
    End Function

    Private Shared Function 构造命名滤镜(名称 As String, ParamArray values() As (Name As String, Value As String)) As String
        Dim opts = JoinNamed(":", values)
        Return If(opts = "", 名称, 名称 & "=" & opts)
    End Function

    Private Class 滤镜图结果
        Public Property 滤镜图 As String = ""
        Public Property 映射参数 As String = ""
        Public Property 输出滤镜参数 As String = ""
        Public Property 编码视频选择器 As New List(Of String)
        Public Property 编码音频选择器 As New List(Of String)
        Public Property 视频输出数量 As Integer = 0
        Public Property 音频输出数量 As Integer = 0
        Public Property 字幕输出数量 As Integer = 0
        Public Property 请求视频输出 As Boolean = False
        Public Property 请求音频输出 As Boolean = False
        Public Property 请求字幕输出 As Boolean = False
        Public Property 视频输出来自滤镜 As Boolean = False
        Public Property 音频输出来自滤镜 As Boolean = False
    End Class
    Private Shared Function 标准化插帧选项值(value As String, ParamArray options() As (Text As String, Value As String)) As String
        Dim text = If(value, "").Trim()
        If text = "" Then Return ""
        For Each item In options
            If String.Equals(text, item.Text, StringComparison.Ordinal) OrElse
               String.Equals(text, item.Value, StringComparison.OrdinalIgnoreCase) Then
                Return item.Value
            End If
        Next
        Return text
    End Function

    Private Shared Function 插帧选项显示文本(value As String, ParamArray options() As (Text As String, Value As String)) As String
        Dim normalized = 标准化插帧选项值(value, options)
        If normalized = "" Then Return ""
        For Each item In options
            If String.Equals(normalized, item.Value, StringComparison.OrdinalIgnoreCase) Then Return item.Text
        Next
        Return normalized
    End Function

    Private Shared Function 插帧模式参数值(value As String) As String
        Return 标准化插帧选项值(value,
            ("两帧加权平均", "blend"),
            ("运动补偿插值", "mci"))
    End Function

    Private Shared Function 插帧模式显示文本(value As String) As String
        Return 插帧选项显示文本(value,
            ("两帧加权平均", "blend"),
            ("运动补偿插值", "mci"))
    End Function

    Private Shared Function 运动估计模式参数值(value As String) As String
        Return 标准化插帧选项值(value,
            ("双向运动估计", "bidir"),
            ("双侧运动估计", "bilat"))
    End Function

    Private Shared Function 运动估计模式显示文本(value As String) As String
        Return 插帧选项显示文本(value,
            ("双向运动估计", "bidir"),
            ("双侧运动估计", "bilat"))
    End Function

    Private Shared Function 运动估计算法参数值(value As String) As String
        Return 标准化插帧选项值(value,
            ("穷举搜索", "esa"),
            ("三步搜索", "tss"),
            ("二维对数搜索", "tdls"),
            ("新三步搜索", "ntss"),
            ("四步搜索", "fss"),
            ("菱形搜索", "ds"),
            ("基于 Hexagon", "hexbs"),
            ("增强的预测区域", "epzs"),
            ("不均匀多六边形", "umh"))
    End Function

    Private Shared Function 运动估计算法显示文本(value As String) As String
        Return 插帧选项显示文本(value,
            ("穷举搜索", "esa"),
            ("三步搜索", "tss"),
            ("二维对数搜索", "tdls"),
            ("新三步搜索", "ntss"),
            ("四步搜索", "fss"),
            ("菱形搜索", "ds"),
            ("基于 Hexagon", "hexbs"),
            ("增强的预测区域", "epzs"),
            ("不均匀多六边形", "umh"))
    End Function

    Private Shared Function 运动补偿模式参数值(value As String) As String
        Return 标准化插帧选项值(value,
            ("重叠块运动补偿", "obmc"),
            ("加权 obmc", "aobmc"))
    End Function

    Private Shared Function 运动补偿模式显示文本(value As String) As String
        Return 插帧选项显示文本(value,
            ("重叠块运动补偿", "obmc"),
            ("加权 obmc", "aobmc"))
    End Function

End Class
