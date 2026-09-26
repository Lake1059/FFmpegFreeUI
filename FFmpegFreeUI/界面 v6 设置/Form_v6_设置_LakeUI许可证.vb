Public Class Form_v6_设置_LakeUI许可证
    Private Sub MB_访问LakeUI官网_Click(sender As Object, e As EventArgs) Handles MB_访问LakeUI官网.Click
        Process.Start(New ProcessStartInfo With {.FileName = "https://lakeui.top", .UseShellExecute = True})
    End Sub

    Private Sub MB_访问LakeUI仓库_Click(sender As Object, e As EventArgs) Handles MB_访问LakeUI仓库.Click
        Process.Start(New ProcessStartInfo With {.FileName = "https://github.com/Lake1059/LakeUI", .UseShellExecute = True})
    End Sub

    Private Sub MB_前往购买许可证_Click(sender As Object, e As EventArgs) Handles MB_前往购买许可证.Click
        Process.Start(New ProcessStartInfo With {.FileName = "https://ifdian.net/item/15f0758814a911f1979752540025c377", .UseShellExecute = True})
    End Sub
End Class