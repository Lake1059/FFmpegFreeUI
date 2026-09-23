Public Class Form_v6_设置_LakeUI性能选项
    Private Sub MCB_GPU抗锯齿_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_GPU抗锯齿.SelectedIndexChanged
        设置_v6.设置图形性能选项(设置_v6.图形性能选项.抗锯齿, MCB_GPU抗锯齿.SelectedIndex)
    End Sub

    Private Sub MCB_文字渲染模式_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_文字渲染模式.SelectedIndexChanged
        设置_v6.设置图形性能选项(设置_v6.图形性能选项.文字渲染, MCB_文字渲染模式.SelectedIndex)
    End Sub

    Private Sub MCB_SSAA_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_SSAA.SelectedIndexChanged
        设置_v6.设置图形性能选项(设置_v6.图形性能选项.超采样, MCB_SSAA.SelectedIndex)
    End Sub

    Private Sub MCB_D2D位图开销_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_D2DImage缓存预算.SelectedIndexChanged
        设置_v6.设置图形性能选项(设置_v6.图形性能选项.GPU缓存, MCB_D2DImage缓存预算.SelectedIndex)
    End Sub

    Private Sub MCB_D2D每对象画刷缓存数量_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_D2D每对象画刷缓存数量.SelectedIndexChanged
        设置_v6.设置图形性能选项(设置_v6.图形性能选项.画刷缓存, MCB_D2D每对象画刷缓存数量.SelectedIndex)
    End Sub

    Private Sub MCB_DW字体相关预算_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_DW字体相关预算.SelectedIndexChanged
        设置_v6.设置图形性能选项(设置_v6.图形性能选项.字体缓存, MCB_DW字体相关预算.SelectedIndex)
    End Sub

    Private Sub MCB_超容器背景映射源位图缓存_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_超容器背景映射源位图缓存.SelectedIndexChanged
        设置_v6.设置图形性能选项(设置_v6.图形性能选项.CPU缓存, MCB_超容器背景映射源位图缓存.SelectedIndex)
    End Sub

    Private Sub MCB_超容器背景映射脏区策略极限_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_超容器背景映射脏区策略极限.SelectedIndexChanged
        设置_v6.设置图形性能选项(设置_v6.图形性能选项.背景脏区, MCB_超容器背景映射脏区策略极限.SelectedIndex)
    End Sub

    Private Sub MCB_超容器背景映射条目预算_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_超容器背景映射条目预算.SelectedIndexChanged
        设置_v6.设置图形性能选项(设置_v6.图形性能选项.超采样缓存, MCB_超容器背景映射条目预算.SelectedIndex)
    End Sub

    Private Sub MCB_动画帧率_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_动画帧率.SelectedIndexChanged
        设置_v6.实例对象.图形动画帧率 = MCB_动画帧率.Text
    End Sub
End Class
