Imports FFmpegFreeUI.提示面板

Public Class 绑定提示板

    Public Shared Sub 绑定参数面板的提示板(参数面板对象 As 界面_常规流程参数_V2)
        绑定(参数面板对象.UiTextBox全局质量控制值, New List(Of Tp_Data) From {
           New Tp_Data With {.Title = "推荐的视觉无损平衡点", .Text = "libx264 & lib265 | crf=23<br>hevc_nvenc | cq=26~28<br>av1_nvenc | cq=36"}})


        绑定(参数面板对象.UiComboBox剪辑方法, New List(Of Tp_Data) From {
           New Tp_Data With {.Title = "粗剪 (立即响应)", .Text = "定位关键帧，最快但不准<br>除了此模式外剪辑参数都是视频参数的一部分"},
           New Tp_Data With {.Title = "精剪 (从头解码)", .Text = "从头解码到剪辑位置，要等但准<br>重编码才真的准"},
           New Tp_Data With {.Title = "精剪 (快速响应)", .Text = "手动指定从入点前面的一定时间开始解码<br>确保解码到关键帧，又快又准<br>重编码才真的准"},
           New Tp_Data With {.Title = "Trim 滤镜", .Text = "要求必须重编码，必须从头开始等解码<br>可以配合烧字幕，字幕也让会被正常剪掉", .TextType = 提示项类型.警告},
           New Tp_Data With {.Title = "必须重编码才能精确到帧", .TitleColor = Color.CornflowerBlue, .Text = "ffmpeg 做不到用复制流精确到帧<br>否则下个关键帧之前都是卡住的", .TextType = 提示项类型.严重}})

        绑定(参数面板对象.UiComboBox剪辑向前解码多久秒, New List(Of Tp_Data) From {
           New Tp_Data With {.Title = "仅限 精剪 (快速响应) 使用", .Text = "确保向前的时间包含了关键帧<br>否则还不如粗剪<br>务必确保时间都输入正确<br>我需要计算你填写的时间的差值"},
           New Tp_Data With {.Title = "注意时间方向", .TitleColor = Color.Goldenrod, .Text = "勿与前向参考帧的时间方向概念混淆<br>此处的向前解码是向开头的方向<br>不是向结尾的方向", .TextType = 提示项类型.严重}})








    End Sub



End Class
