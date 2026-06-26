Public Class Form_v6_参数面板_自定义参数说明
    Private Sub Form_v6_参数面板_自定义参数说明_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MDV_自定义参数说明.Text =
"## 通配字符串说明
界面交互提供的需求有限，其余高级需求使用自定义参数来实现<br>通配字符串为各种细分领域和深度专业人士提供最大程度的自由

> [!CAUTION]
> 所有换行都不会生效
> 区分大小写
> 不会对路径增加引号，请自行处理

## 全部通配字符串

`<InputFile>`<br>表示输入文件完整路径
`<InputFileWithOutExtension>`<br>表示不包含后缀的输入文件路径，注意点号也是算在后缀里的
`<InputFilePath>`<br>表示输入文件所在文件夹路径
`<InputFileName>`<br>表示输入文件名，不包含其路径
`<InputFileNameWithOutExtension>`<br>表示不包含后缀的输入文件名，也不包含其路径
`<\InputFileWithOutExtension>`<br><InputFileWithOutExtension>的转义版本，可用于滤镜
`<\InputFilePath>`<br><InputFilePath>的转义版本，可用于滤镜

## 其他
实际上这些字符串写在其他任何地方都能生效，不过没必要那样做就是了
"
    End Sub
End Class