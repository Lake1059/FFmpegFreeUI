Public Class Form_v6_设置_LakeUI性能选项
    Private Sub MCB_SSAA_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_SSAA.SelectedIndexChanged
        设置_v6.实例对象.图形全局SSAA = Me.MCB_SSAA.SelectedIndex
        LakeUI.Class1.GlobalSSAA = 设置_v6.实例对象.图形全局SSAA
    End Sub

    Private Sub MCB_GPU抗锯齿_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_GPU抗锯齿.SelectedIndexChanged
        设置_v6.实例对象.图形DX抗锯齿 = Me.MCB_GPU抗锯齿.SelectedIndex
        Select Case 设置_v6.实例对象.图形DX抗锯齿
            Case 0 : LakeUI.D2DHelper.GlobalAntialiasMode = Vortice.Direct2D1.AntialiasMode.PerPrimitive
            Case 1 : LakeUI.D2DHelper.GlobalAntialiasMode = Vortice.Direct2D1.AntialiasMode.Aliased
        End Select

    End Sub

    Private Sub MCB_文字渲染模式_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_文字渲染模式.SelectedIndexChanged
        设置_v6.实例对象.图形DX文字渲染模式 = Me.MCB_文字渲染模式.SelectedIndex
        Select Case 设置_v6.实例对象.图形DX文字渲染模式
            Case 0 : LakeUI.D2DHelper.GlobalTextQuality = LakeUI.D2DHelper.TextQualityMode.ClearType
            Case 1 : LakeUI.D2DHelper.GlobalTextQuality = LakeUI.D2DHelper.TextQualityMode.Grayscale
            Case 2 : LakeUI.D2DHelper.GlobalTextQuality = LakeUI.D2DHelper.TextQualityMode.Aliased
            Case 3 : LakeUI.D2DHelper.GlobalTextQuality = LakeUI.D2DHelper.TextQualityMode.Outline
        End Select
    End Sub

    Private Sub MCB_动画帧率_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_动画帧率.SelectedIndexChanged
        设置_v6.实例对象.图形动画帧率 = Me.MCB_动画帧率.Text



    End Sub
End Class