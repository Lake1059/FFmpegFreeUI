Public Class Form_v6_设置_LakeUI性能选项

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
        LakeUI.GlobalOptions.GlobalSSAA = 设置_v6.实例对象.图形DX_SSAA
    End Sub

    Private Sub MCB_D2D位图开销_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_D2DImage缓存预算.SelectedIndexChanged
        设置_v6.实例对象.图形DXImage缓存预算 = MCB_D2DImage缓存预算.SelectedIndex
        Select Case 设置_v6.实例对象.图形DXImage缓存预算
            Case 0
                LakeUI.GlobalOptions.D2DBitmapCacheBudgetBytes = 16 * 1024 * 1024
                LakeUI.GlobalOptions.D2DBitmapCacheMaxImagesPerCompositor = 32
            Case 1
                LakeUI.GlobalOptions.D2DBitmapCacheBudgetBytes = 32 * 1024 * 1024
                LakeUI.GlobalOptions.D2DBitmapCacheMaxImagesPerCompositor = 64
            Case 2
                LakeUI.GlobalOptions.D2DBitmapCacheBudgetBytes = 64 * 1024 * 1024
                LakeUI.GlobalOptions.D2DBitmapCacheMaxImagesPerCompositor = 128
            Case 3
                LakeUI.GlobalOptions.D2DBitmapCacheBudgetBytes = 64 * 1024 * 1024
                LakeUI.GlobalOptions.D2DBitmapCacheMaxImagesPerCompositor = 256
            Case 4
                LakeUI.GlobalOptions.D2DBitmapCacheBudgetBytes = 128 * 1024 * 1024
                LakeUI.GlobalOptions.D2DBitmapCacheMaxImagesPerCompositor = 256
            Case 5
                LakeUI.GlobalOptions.D2DBitmapCacheBudgetBytes = 256 * 1024 * 1024
                LakeUI.GlobalOptions.D2DBitmapCacheMaxImagesPerCompositor = 256
            Case 6
                LakeUI.GlobalOptions.D2DBitmapCacheBudgetBytes = 512 * 1024 * 1024
                LakeUI.GlobalOptions.D2DBitmapCacheMaxImagesPerCompositor = 512
        End Select
        '单图 16M + 32 索引
        '单图 32M + 64 索引
        '单图 64M + 128 索引
        '单图 64M + 256 索引
        '单图 128M + 256 索引
        '单图 256M + 256 索引
        '单图 512M + 512 索引
    End Sub
    Private Sub MCB_D2D每对象画刷缓存数量_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_D2D每对象画刷缓存数量.SelectedIndexChanged
        设置_v6.实例对象.图形DX每对象画刷缓存数量 = MCB_D2D每对象画刷缓存数量.SelectedIndex
        Select Case 设置_v6.实例对象.图形DX每对象画刷缓存数量
            Case 0 : LakeUI.GlobalOptions.D2DBrushCacheMaxEntriesPerRenderTarget = 8
            Case 1 : LakeUI.GlobalOptions.D2DBrushCacheMaxEntriesPerRenderTarget = 16
            Case 2 : LakeUI.GlobalOptions.D2DBrushCacheMaxEntriesPerRenderTarget = 32
            Case 3 : LakeUI.GlobalOptions.D2DBrushCacheMaxEntriesPerRenderTarget = 64
            Case 4 : LakeUI.GlobalOptions.D2DBrushCacheMaxEntriesPerRenderTarget = 128
            Case 5 : LakeUI.GlobalOptions.D2DBrushCacheMaxEntriesPerRenderTarget = 256
            Case 6 : LakeUI.GlobalOptions.D2DBrushCacheMaxEntriesPerRenderTarget = 512
        End Select
    End Sub
    Private Sub MCB_DW字体相关预算_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_DW字体相关预算.SelectedIndexChanged
        设置_v6.实例对象.图形DW字体相关预算 = MCB_DW字体相关预算.SelectedIndex
        Select Case 设置_v6.实例对象.图形DW字体相关预算
            Case 0
                LakeUI.GlobalOptions.DWriteTextFormatCacheMaxEntriesPerCompositor = 32
                LakeUI.GlobalOptions.DWriteFontResolveCacheMaxEntries = 32
            Case 1
                LakeUI.GlobalOptions.DWriteTextFormatCacheMaxEntriesPerCompositor = 64
                LakeUI.GlobalOptions.DWriteFontResolveCacheMaxEntries = 64
            Case 2
                LakeUI.GlobalOptions.DWriteTextFormatCacheMaxEntriesPerCompositor = 128
                LakeUI.GlobalOptions.DWriteFontResolveCacheMaxEntries = 128
            Case 3
                LakeUI.GlobalOptions.DWriteTextFormatCacheMaxEntriesPerCompositor = 256
                LakeUI.GlobalOptions.DWriteFontResolveCacheMaxEntries = 256
            Case 4
                LakeUI.GlobalOptions.DWriteTextFormatCacheMaxEntriesPerCompositor = 512
                LakeUI.GlobalOptions.DWriteFontResolveCacheMaxEntries = 512
        End Select
    End Sub
    Private Sub MCB_超容器背景映射源位图缓存_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_超容器背景映射源位图缓存.SelectedIndexChanged
        设置_v6.实例对象.图形DX超容器背景映射源位图缓存 = MCB_超容器背景映射源位图缓存.SelectedIndex
        Select Case 设置_v6.实例对象.图形DX超容器背景映射源位图缓存
            Case 0 : LakeUI.GlobalOptions.BackgroundPenetrationSourceBitmapBudgetBytes = 16L * 1024L * 1024L
            Case 1 : LakeUI.GlobalOptions.BackgroundPenetrationSourceBitmapBudgetBytes = 32L * 1024L * 1024L
            Case 2 : LakeUI.GlobalOptions.BackgroundPenetrationSourceBitmapBudgetBytes = 64L * 1024L * 1024L
            Case 3 : LakeUI.GlobalOptions.BackgroundPenetrationSourceBitmapBudgetBytes = 128L * 1024L * 1024L
            Case 4 : LakeUI.GlobalOptions.BackgroundPenetrationSourceBitmapBudgetBytes = 256L * 1024L * 1024L
        End Select
    End Sub
    Private Sub MCB_超容器背景映射脏区策略极限_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_超容器背景映射脏区策略极限.SelectedIndexChanged
        设置_v6.实例对象.图形DX超容器背景映射脏区策略极限 = MCB_超容器背景映射脏区策略极限.SelectedIndex
        Select Case 设置_v6.实例对象.图形DX超容器背景映射脏区策略极限
            Case 0
                LakeUI.GlobalOptions.BackgroundPenetrationDirtyRectMaxCount = 4
                LakeUI.GlobalOptions.BackgroundPenetrationFullDirtyAreaRatio = 0.4
            Case 1
                LakeUI.GlobalOptions.BackgroundPenetrationDirtyRectMaxCount = 4
                LakeUI.GlobalOptions.BackgroundPenetrationFullDirtyAreaRatio = 0.6
            Case 2
                LakeUI.GlobalOptions.BackgroundPenetrationDirtyRectMaxCount = 4
                LakeUI.GlobalOptions.BackgroundPenetrationFullDirtyAreaRatio = 0.8
            Case 3
                LakeUI.GlobalOptions.BackgroundPenetrationDirtyRectMaxCount = 8
                LakeUI.GlobalOptions.BackgroundPenetrationFullDirtyAreaRatio = 0.4
            Case 4
                LakeUI.GlobalOptions.BackgroundPenetrationDirtyRectMaxCount = 8
                LakeUI.GlobalOptions.BackgroundPenetrationFullDirtyAreaRatio = 0.6
            Case 5
                LakeUI.GlobalOptions.BackgroundPenetrationDirtyRectMaxCount = 8
                LakeUI.GlobalOptions.BackgroundPenetrationFullDirtyAreaRatio = 0.8
            Case 6
                LakeUI.GlobalOptions.BackgroundPenetrationDirtyRectMaxCount = 16
                LakeUI.GlobalOptions.BackgroundPenetrationFullDirtyAreaRatio = 0.4
            Case 7
                LakeUI.GlobalOptions.BackgroundPenetrationDirtyRectMaxCount = 16
                LakeUI.GlobalOptions.BackgroundPenetrationFullDirtyAreaRatio = 0.6
            Case 8
                LakeUI.GlobalOptions.BackgroundPenetrationDirtyRectMaxCount = 16
                LakeUI.GlobalOptions.BackgroundPenetrationFullDirtyAreaRatio = 0.8
        End Select
    End Sub
    Private Sub MCB_超容器背景映射条目预算_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_超容器背景映射条目预算.SelectedIndexChanged
        设置_v6.实例对象.图形DX超容器背景映射显存总量和单源条目 = MCB_超容器背景映射条目预算.SelectedIndex
        Select Case 设置_v6.实例对象.图形DX超容器背景映射显存总量和单源条目
            Case 0
                LakeUI.GlobalOptions.BackgroundPenetrationCropCacheBudgetBytes = 32 * 1024 * 1024
                LakeUI.GlobalOptions.BackgroundPenetrationCropCacheMaxEntriesPerSource = 12
            Case 1
                LakeUI.GlobalOptions.BackgroundPenetrationCropCacheBudgetBytes = 32 * 1024 * 1024
                LakeUI.GlobalOptions.BackgroundPenetrationCropCacheMaxEntriesPerSource = 24
            Case 2
                LakeUI.GlobalOptions.BackgroundPenetrationCropCacheBudgetBytes = 32 * 1024 * 1024
                LakeUI.GlobalOptions.BackgroundPenetrationCropCacheMaxEntriesPerSource = 48
            Case 3
                LakeUI.GlobalOptions.BackgroundPenetrationCropCacheBudgetBytes = 64 * 1024 * 1024
                LakeUI.GlobalOptions.BackgroundPenetrationCropCacheMaxEntriesPerSource = 32
            Case 4
                LakeUI.GlobalOptions.BackgroundPenetrationCropCacheBudgetBytes = 64 * 1024 * 1024
                LakeUI.GlobalOptions.BackgroundPenetrationCropCacheMaxEntriesPerSource = 64
            Case 5
                LakeUI.GlobalOptions.BackgroundPenetrationCropCacheBudgetBytes = 64 * 1024 * 1024
                LakeUI.GlobalOptions.BackgroundPenetrationCropCacheMaxEntriesPerSource = 128
            Case 6
                LakeUI.GlobalOptions.BackgroundPenetrationCropCacheBudgetBytes = 128 * 1024 * 1024
                LakeUI.GlobalOptions.BackgroundPenetrationCropCacheMaxEntriesPerSource = 32
            Case 7
                LakeUI.GlobalOptions.BackgroundPenetrationCropCacheBudgetBytes = 128 * 1024 * 1024
                LakeUI.GlobalOptions.BackgroundPenetrationCropCacheMaxEntriesPerSource = 64
            Case 8
                LakeUI.GlobalOptions.BackgroundPenetrationCropCacheBudgetBytes = 128 * 1024 * 1024
                LakeUI.GlobalOptions.BackgroundPenetrationCropCacheMaxEntriesPerSource = 128
        End Select
    End Sub

    Private Sub MCB_动画帧率_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_动画帧率.SelectedIndexChanged
        设置_v6.实例对象.图形动画帧率 = Me.MCB_动画帧率.Text
    End Sub


End Class
