## AviSynth+ 降噪滤镜

> 本段内容由 hajimanbo 提供内容改进

AviSynth 选项在降噪设置里提供，最后一个选项就是。3FUI 只能使用 avs 文件来加载，在开始前必须明确，3FUI 是设计用于批量处理的转码工具，因此这个加载逻辑也必然是这样设计的。当使用此滤镜时，avs 文件将取代输入文件的位置，3FUI 默认您不会在 avs 里处理音频，为了避免新手直接用发现视频声音没了，因此当选择此降噪时自动覆盖填写自定义参数和流控制参数，请注意检查。

首先下载这些 dll 文件：

- AviSynth.dll（在发行版中有个名为 filesonly 的那里面找）<br>https://github.com/AviSynth/AviSynthPlus/releases
- LSMASHSource.dll<br>https://github.com/HomeOfAviSynthPlusEvolution/L-SMASH-Works/releases<br>此文件用于读取视频文件
- knlmeanscl.dll<br>https://github.com/Khanattila/KNLMeansCL/releases<br>此文件用于降噪
- fmtconv.dll<br>https://gitlab.com/EleonoreMizo/fmtconv<br>用于改变视频位深

将这些文件与 ffmpeg 放在一起！

在 3FUI 的目录下创建 AviSynth.avs 文件，此文件作为模板提供给 3FUI，在其中填写以下内容：

```vb
LoadPlugin("C:\xxxx\LSMASHSource.dll")
LoadPlugin("C:\xxxx\fmtconv.dll")
LoadPlugin("C:\xxxx\knlmeanscl.dll")
LWLibavVideoSource("<FilePath>")#读取文件中的视频流
fmtc_bitdepth(bits=16,dmode=1)#为KNLMeansCL降噪而提升位深
KNLMeansCL(d=2,a=3,s=4,h=3,channels="Y",wmode=3)#对亮度平面的降噪强度高于色度平面
KNLMeansCL(d=2,a=3,s=4,h=2,channels="UV",wmode=3)#对色度平面的降噪强度低于亮度平面
fmtc_bitdepth(bits=10,dmode=0)#为libsvtav1编码而降低位深
```

如果需要在avs中读取文件的音视频流可用以下模板

```vb
LoadPlugin("C:\xxxx\LSMASHSource.dll")
LoadPlugin("C:\xxxx\fmtconv.dll")
LoadPlugin("C:\xxxx\knlmeanscl.dll")
function LibavSource2(string path, int "atrack",
\          int "fpsnum", int "fpsden",
\          string "format", bool "cache")
{
    atrack   = Default(atrack, -1)
    fpsnum   = Default(fpsnum, 0)
    fpsden   = Default(fpsden,  1)
    cache    = Default(cache, true)

    format   = Default(format, "")

    video = LWLibavVideoSource(path,
    \               fpsnum=fpsnum, fpsden=fpsden, format=format,
    \               cache=cache)
    return (atrack==-2) ? video: AudioDub(video,
   \    LWLibavAudioSource(path, stream_index=atrack, cache=cache))
}
LibavSource2("<FilePath>")#读取文件中的音视频流。atrack - 音频轨道号，默认为自动，如果为 -2，则忽略音频。cache - 如果为 true（默认值），则创建索引文件
fmtc_bitdepth(bits=16,dmode=1)#为KNLMeansCL降噪而提升位深
KNLMeansCL(d=2,a=3,s=4,h=3,channels="Y",wmode=3)#对亮度平面的降噪强度高于色度平面
KNLMeansCL(d=2,a=3,s=4,h=2,channels="UV",wmode=3)#对色度平面的降噪强度低于亮度平面
fmtc_bitdepth(bits=10,dmode=0)#为libsvtav1编码而降低位深
```

- LoadPlugin 方法用来加载这些 dll，不需要在这里加载 AviSynth.dll<br>dll 的路径请写绝对路径！
- LWLibavVideoSource 加载视频文件，直接把第四行复制过去，3FUI 会自动替换这个文件路径
- fmtc_bitdepth 用于先为 KNLMeansCL 的降噪提升位深，而后为视频编码器的编码降低位深
- KNLMeansCL 就是降噪方法，其参数如下：

原文：https://github.com/Khanattila/KNLMeansCL/wiki/Filter-description

```c#
KNLMeansCL(clip, int d, int a, int s, float h, string channels, int wmode, float wref, clip rclip, string device_type, int device_id, bool lsb_inout, bool info)
```

| 参数名      | 说明                                                         |
| ----------- | ------------------------------------------------------------ |
| clip        | 不写                                                         |
| d           | 前后参考帧数量，d=0 使用 1 帧，d=1 使用 3 帧，n=2*d+1，以此类推，值越大降噪效果越好 |
| a           | 搜索像素半径，a=1 使用 9 像素，a=2 使用 25 像素，n=(2*a+1)^2，以此类推，值越大降噪效果越好 |
| s           | 相似半径，默认 = 4，对性能的影响很小，这取决于噪声的性质     |
| h           | 过滤强度，默认 = 1.2，值越大，去除的杂色越多                 |
| channels    | 设置要去噪的颜色通道。可能的值为 YUV、Y、UV                  |
| wmode       | 0 := Welsch weighting function has a faster decay, but still assigns positive weights to dissimilar blocks. Original Non-local means denoising weighting function.<br/>1 := Modified Bisquare weighting function to be less robust.<br/>2 := Bisquare weighting function use a soft threshold to compare neighbourhoods (the weight is 0 as soon as a given threshold is exceeded).<br/>3 := Modified Bisquare weighting function to be even more robust. |
| wref        | 默认 =  1，相对于找到的最相似像素的权重，影响滤镜输出的原始像素量 |
| rclip       | 不写                                                         |
| device_type | accelerator、cpu、gpu、auto                                  |
| device_id   | 显卡索引                                                     |
| lsb_inout   | 没必要                                                       |
| info        | 输出额外信息                                                 |
