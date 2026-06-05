Public Class Form_v6_设置_LakeUI性能选项

    Private Sub Form_v6_设置_LakeUI性能选项_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub MCB_SSAA_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_SSAA.SelectedIndexChanged
        设置_v6.实例对象.图形全局SSAA = Me.MCB_SSAA.SelectedIndex
        LakeUI.GlobalOptions.GlobalSSAA = 设置_v6.实例对象.图形全局SSAA
    End Sub

    Private Sub MCB_SSAA渲染池缓存命中_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_SSAA渲染池缓存命中.SelectedIndexChanged
        设置_v6.实例对象.图形SSAA渲染缓存命中 = MCB_SSAA渲染池缓存命中.SelectedIndex
        Select Case 设置_v6.实例对象.图形SSAA渲染缓存命中
            Case 0 : LakeUI.GlobalOptions.SsaaRenderTargetPoolBucketSize = 16
            Case 1 : LakeUI.GlobalOptions.SsaaRenderTargetPoolBucketSize = 32
            Case 2 : LakeUI.GlobalOptions.SsaaRenderTargetPoolBucketSize = 64
            Case 3 : LakeUI.GlobalOptions.SsaaRenderTargetPoolBucketSize = 128
            Case 4 : LakeUI.GlobalOptions.SsaaRenderTargetPoolBucketSize = 256
        End Select
    End Sub

    Private Sub MCB_SSAA渲染池总量预算_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_SSAA渲染池总量预算.SelectedIndexChanged
        设置_v6.实例对象.图形SSAA渲染缓存总量预算 = MCB_SSAA渲染池总量预算.SelectedIndex
        Select Case 设置_v6.实例对象.图形SSAA渲染缓存总量预算
            Case 0 : LakeUI.GlobalOptions.SsaaRenderTargetPoolBudgetBytes = 0
            Case 1 : LakeUI.GlobalOptions.SsaaRenderTargetPoolBudgetBytes = 32 * 1024 * 1024
            Case 2 : LakeUI.GlobalOptions.SsaaRenderTargetPoolBudgetBytes = 64 * 1024 * 1024
            Case 3 : LakeUI.GlobalOptions.SsaaRenderTargetPoolBudgetBytes = 128 * 1024 * 1024
            Case 4 : LakeUI.GlobalOptions.SsaaRenderTargetPoolBudgetBytes = 256 * 1024 * 1024
            Case 5 : LakeUI.GlobalOptions.SsaaRenderTargetPoolBudgetBytes = 512 * 1024 * 1024
            Case 6 : LakeUI.GlobalOptions.SsaaRenderTargetPoolBudgetBytes = 1024 * 1024 * 1024
        End Select
    End Sub

    Private Sub MCB_GPU抗锯齿_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_GPU抗锯齿.SelectedIndexChanged
        设置_v6.实例对象.图形DX抗锯齿 = MCB_GPU抗锯齿.SelectedIndex
        Select Case 设置_v6.实例对象.图形DX抗锯齿
            Case 0 : LakeUI.GlobalOptions.GlobalAntialiasMode = Vortice.Direct2D1.AntialiasMode.PerPrimitive
            Case 1 : LakeUI.GlobalOptions.GlobalAntialiasMode = Vortice.Direct2D1.AntialiasMode.Aliased
        End Select
    End Sub

    Private Sub MCB_文字渲染模式_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_文字渲染模式.SelectedIndexChanged
        设置_v6.实例对象.图形DX文字渲染模式 = Me.MCB_文字渲染模式.SelectedIndex
        Select Case 设置_v6.实例对象.图形DX文字渲染模式
            Case 0 : LakeUI.GlobalOptions.GlobalTextQuality = LakeUI.GlobalOptions.TextQualityMode.ClearType
            Case 1 : LakeUI.GlobalOptions.GlobalTextQuality = LakeUI.GlobalOptions.TextQualityMode.Grayscale
            Case 2 : LakeUI.GlobalOptions.GlobalTextQuality = LakeUI.GlobalOptions.TextQualityMode.Aliased
            Case 3 : LakeUI.GlobalOptions.GlobalTextQuality = LakeUI.GlobalOptions.TextQualityMode.Outline
        End Select
    End Sub

    Private Sub MCB_D2D位图开销_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_D2D位图开销.SelectedIndexChanged
        设置_v6.实例对象.图形DXImage开销 = MCB_D2D位图开销.SelectedIndex
        Select Case 设置_v6.实例对象.图形DXImage开销
            Case 0 : LakeUI.GlobalOptions.D2DBitmapCacheBudgetBytes = 0
            Case 1 : LakeUI.GlobalOptions.D2DBitmapCacheBudgetBytes = 32 * 1024 * 1024
            Case 2 : LakeUI.GlobalOptions.D2DBitmapCacheBudgetBytes = 64 * 1024 * 1024
            Case 3 : LakeUI.GlobalOptions.D2DBitmapCacheBudgetBytes = 128 * 1024 * 1024
            Case 4 : LakeUI.GlobalOptions.D2DBitmapCacheBudgetBytes = 256 * 1024 * 1024
            Case 5 : LakeUI.GlobalOptions.D2DBitmapCacheBudgetBytes = 512 * 1024 * 1024
            Case 6 : LakeUI.GlobalOptions.D2DBitmapCacheBudgetBytes = 1024 * 1024 * 1024
        End Select
    End Sub

    Private Sub MCB_超容器背景映射开销_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_超容器背景映射开销.SelectedIndexChanged
        设置_v6.实例对象.图形DX超容器背景穿透开销 = MCB_超容器背景映射开销.SelectedIndex
        Select Case 设置_v6.实例对象.图形DX超容器背景穿透开销
            Case 0 : LakeUI.GlobalOptions.BackgroundPenetrationCropCacheMaxEntriesPerSource = 0
            Case 1 : LakeUI.GlobalOptions.BackgroundPenetrationCropCacheMaxEntriesPerSource = 12
            Case 2 : LakeUI.GlobalOptions.BackgroundPenetrationCropCacheMaxEntriesPerSource = 24
            Case 3 : LakeUI.GlobalOptions.BackgroundPenetrationCropCacheMaxEntriesPerSource = 48
            Case 4 : LakeUI.GlobalOptions.BackgroundPenetrationCropCacheMaxEntriesPerSource = 96
        End Select
    End Sub

    Private Sub MCB_动画帧率_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_动画帧率.SelectedIndexChanged
        设置_v6.实例对象.图形动画帧率 = Me.MCB_动画帧率.Text
    End Sub
End Class