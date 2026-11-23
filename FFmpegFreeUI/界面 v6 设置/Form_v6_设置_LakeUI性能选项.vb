Imports LakeUI

Public Class Form_v6_设置_LakeUI性能选项
    Private 正在同步或切换DX版本 As Boolean

    Friend Sub 同步DX版本()
        Dim 后端 = If(GlobalOptions.ActiveBackend, GlobalOptions.RequestedBackend)
        Dim 索引 = If(后端 = RenderBackend.Direct3D12, 1, 0)
        正在同步或切换DX版本 = True
        Try
            MCB_DX版本.SelectedIndex = 索引
            If GlobalOptions.ActiveBackend.HasValue Then 设置_v6.实例对象.图形DX版本 = 索引
        Finally
            正在同步或切换DX版本 = False
        End Try
    End Sub

    Private Async Sub MCB_DX版本_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MCB_DX版本.SelectedIndexChanged
        If 正在同步或切换DX版本 OrElse MCB_DX版本.SelectedIndex < 0 Then Return
        Dim 目标 = If(MCB_DX版本.SelectedIndex = 1, RenderBackend.Direct3D12, RenderBackend.Direct3D11)
        If GlobalOptions.ActiveBackend = 目标 Then Return

        正在同步或切换DX版本 = True
        MCB_DX版本.Enabled = False
        Dim 失败信息 As String = Nothing
        Try
            Dim 结果 = Await GlobalOptions.SetRenderBackendAsync(目标)
            If 结果 <> BackendSelectionResult.Switched AndAlso 结果 <> BackendSelectionResult.AlreadyActive Then
                Dim 诊断 = GlobalOptions.LastBackendSwitch
                失败信息 = If(诊断?.Requested = 目标 AndAlso 诊断?.Result = 结果, 诊断?.Cause?.Message, Nothing)
                If String.IsNullOrWhiteSpace(失败信息) Then 失败信息 = $"当前无法切换渲染后端（{结果}）。"
            End If
        Catch ex As Exception
            失败信息 = ex.Message
        Finally
            If Not IsDisposed Then
                同步DX版本()
                MCB_DX版本.Enabled = True
            End If
        End Try
        If Not IsDisposed AndAlso 失败信息 IsNot Nothing Then
            Const 标题 As String = "切换 DirectX 版本失败"
            If GlobalOptions.ActiveBackend.HasValue AndAlso GlobalOptions.BackendStartupError Is Nothing Then
                ExOverlayMsgBox(FormMain_v6, 失败信息, MsgBoxStyle.Exclamation, 标题)
            Else
                MessageBox.Show(FormMain_v6, 失败信息, 标题, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End If
    End Sub

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
