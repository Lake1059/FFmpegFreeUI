Public Class Form_v6_设置_LakeUI性能选项

    Private Shared ReadOnly GPU缓存预算选项MiB As Long() = {64L, 128L, 256L, 512L, 1024L, 2048L, 4096L}
    Private Shared ReadOnly CPU缓存预算选项MiB As Long() = {32L, 64L, 128L, 256L, 512L}
    Private Shared ReadOnly 缓存条目选项 As Integer() = {8, 16, 32, 64, 128, 256, 512}
    Private Shared ReadOnly 字体缓存条目选项 As Integer() = {32, 64, 128, 256, 512}
    Private Shared ReadOnly 背景穿透脏区数量选项 As Integer() = {4, 4, 4, 8, 8, 8, 16, 16, 16}
    Private Shared ReadOnly 背景穿透完整重采阈值选项 As Single() = {0.4F, 0.6F, 0.8F, 0.4F, 0.6F, 0.8F, 0.4F, 0.6F, 0.8F}
    Private Shared ReadOnly SSAA缓存分桶粒度选项 As Integer() = {32, 64, 96, 128, 192, 256, 384, 512, 1024}

    Private Sub Form_v6_设置_LakeUI性能选项_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub MCB_GPU抗锯齿_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_GPU抗锯齿.SelectedIndexChanged
        设置_v6.实例对象.图形DX抗锯齿 = MCB_GPU抗锯齿.SelectedIndex
        Select Case 设置_v6.实例对象.图形DX抗锯齿
            Case 0 : LakeUI.GlobalOptions.GlobalAntialiasMode = Vortice.Direct2D1.AntialiasMode.PerPrimitive
            Case 1 : LakeUI.GlobalOptions.GlobalAntialiasMode = Vortice.Direct2D1.AntialiasMode.Aliased
        End Select
    End Sub

    Private Sub MCB_文字渲染模式_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_文字渲染模式.SelectedIndexChanged
        设置_v6.实例对象.图形DX文字渲染模式 = MCB_文字渲染模式.SelectedIndex
        Select Case 设置_v6.实例对象.图形DX文字渲染模式
            Case 0 : LakeUI.GlobalOptions.GlobalTextQuality = LakeUI.GlobalOptions.TextQualityMode.ClearType
            Case 1 : LakeUI.GlobalOptions.GlobalTextQuality = LakeUI.GlobalOptions.TextQualityMode.Grayscale
            Case 2 : LakeUI.GlobalOptions.GlobalTextQuality = LakeUI.GlobalOptions.TextQualityMode.Aliased
            Case 3 : LakeUI.GlobalOptions.GlobalTextQuality = LakeUI.GlobalOptions.TextQualityMode.Outline
        End Select
    End Sub

    Private Sub MCB_SSAA_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_SSAA.SelectedIndexChanged
        设置_v6.实例对象.图形DX_SSAA = MCB_SSAA.SelectedIndex
        Select Case 设置_v6.实例对象.图形DX_SSAA
            Case 1 : LakeUI.GlobalOptions.GlobalSSAA = LakeUI.GlobalOptions.SuperSamplingScaleEnum.x2
            Case 2 : LakeUI.GlobalOptions.GlobalSSAA = LakeUI.GlobalOptions.SuperSamplingScaleEnum.x3
            Case 3 : LakeUI.GlobalOptions.GlobalSSAA = LakeUI.GlobalOptions.SuperSamplingScaleEnum.x4
            Case Else : LakeUI.GlobalOptions.GlobalSSAA = LakeUI.GlobalOptions.SuperSamplingScaleEnum.OFF
        End Select
    End Sub

    Private Sub MCB_D2D位图开销_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_D2DImage缓存预算.SelectedIndexChanged
        设置_v6.实例对象.图形DX_GPU缓存总预算 = MCB_D2DImage缓存预算.SelectedIndex
        LakeUI.GlobalOptions.GpuCacheBudgetBytes = 取长整数选项(GPU缓存预算选项MiB, 设置_v6.实例对象.图形DX_GPU缓存总预算) * 1024L * 1024L
    End Sub

    Private Sub MCB_D2D每对象画刷缓存数量_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_D2D每对象画刷缓存数量.SelectedIndexChanged
        设置_v6.实例对象.图形DX画刷缓存条目上限 = MCB_D2D每对象画刷缓存数量.SelectedIndex
        LakeUI.GlobalOptions.BrushCacheLimit = 取整数选项(缓存条目选项, 设置_v6.实例对象.图形DX画刷缓存条目上限)
    End Sub

    Private Sub MCB_DW字体相关预算_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_DW字体相关预算.SelectedIndexChanged
        设置_v6.实例对象.图形DW字体缓存条目上限 = MCB_DW字体相关预算.SelectedIndex
        Dim 条目上限 = 取整数选项(字体缓存条目选项, 设置_v6.实例对象.图形DW字体缓存条目上限)
        LakeUI.GlobalOptions.TextFormatCacheLimit = 条目上限
        LakeUI.GlobalOptions.FontResolveCacheLimit = 条目上限
    End Sub

    Private Sub MCB_超容器背景映射源位图缓存_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_超容器背景映射源位图缓存.SelectedIndexChanged
        设置_v6.实例对象.图形DX_CPU位图缓存总预算 = MCB_超容器背景映射源位图缓存.SelectedIndex
        LakeUI.GlobalOptions.CpuCacheBudgetBytes = 取长整数选项(CPU缓存预算选项MiB, 设置_v6.实例对象.图形DX_CPU位图缓存总预算) * 1024L * 1024L
    End Sub

    Private Sub MCB_超容器背景映射脏区策略极限_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_超容器背景映射脏区策略极限.SelectedIndexChanged
        设置_v6.实例对象.图形DX背景穿透脏区策略 = MCB_超容器背景映射脏区策略极限.SelectedIndex
        LakeUI.GlobalOptions.BackgroundDirtyRectLimit = 取整数选项(背景穿透脏区数量选项, 设置_v6.实例对象.图形DX背景穿透脏区策略)
        LakeUI.GlobalOptions.BackgroundFullDirtyRatio = 取单精度选项(背景穿透完整重采阈值选项, 设置_v6.实例对象.图形DX背景穿透脏区策略)
    End Sub

    Private Sub MCB_超容器背景映射条目预算_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_超容器背景映射条目预算.SelectedIndexChanged
        设置_v6.实例对象.图形DX_SSAA缓存分桶粒度 = MCB_超容器背景映射条目预算.SelectedIndex
        LakeUI.GlobalOptions.SsaaBucketSize = 取整数选项(SSAA缓存分桶粒度选项, 设置_v6.实例对象.图形DX_SSAA缓存分桶粒度)
    End Sub

    Private Sub MCB_动画帧率_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_动画帧率.SelectedIndexChanged
        设置_v6.实例对象.图形动画帧率 = Me.MCB_动画帧率.Text
    End Sub

    Private Shared Function 取长整数选项(values As Long(), index As Integer) As Long
        Return values(Math.Min(Math.Max(index, 0), values.Length - 1))
    End Function

    Private Shared Function 取整数选项(values As Integer(), index As Integer) As Integer
        Return values(Math.Min(Math.Max(index, 0), values.Length - 1))
    End Function

    Private Shared Function 取单精度选项(values As Single(), index As Integer) As Single
        Return values(Math.Min(Math.Max(index, 0), values.Length - 1))
    End Function

End Class
