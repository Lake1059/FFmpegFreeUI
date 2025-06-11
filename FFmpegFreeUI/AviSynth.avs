LoadPlugin("C:\xxxx\LSMASHSource.dll")
LoadPlugin("C:\xxxx\fmtconv.dll")
LoadPlugin("C:\xxxx\knlmeanscl.dll")
LWLibavVideoSource("<FilePath>")#读取文件中的视频流
fmtc_bitdepth(bits=16,dmode=1)#为KNLMeansCL降噪而提升位深
KNLMeansCL(d=2,a=3,s=4,h=3,channels="Y",wmode=3)#对亮度平面的降噪强度高于色度平面
KNLMeansCL(d=2,a=3,s=4,h=2,channels="UV",wmode=3)#对色度平面的降噪强度低于亮度平面
fmtc_bitdepth(bits=10,dmode=0)#为libsvtav1编码而降低位深
