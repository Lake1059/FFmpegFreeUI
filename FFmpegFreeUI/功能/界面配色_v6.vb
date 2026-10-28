Public Module 界面配色_v6

    Public ReadOnly Property 错误文本色 As Color
        Get
            Return Color.FromArgb(224, 112, 112)
        End Get
    End Property

    Public Const 错误文本色Html As String = "#E07070"

    Public ReadOnly 浅色选中背景 As Color = Color.FromArgb(214, 222, 230)
    Public ReadOnly 深色选中背景 As Color = Color.FromArgb(86, 86, 86)

    Public Function 获取选中背景(浅色 As Boolean) As Color
        Return If(浅色, 浅色选中背景, 深色选中背景)
    End Function

End Module
