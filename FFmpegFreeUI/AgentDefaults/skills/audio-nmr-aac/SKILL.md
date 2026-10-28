---
name: audio-nmr-aac
description: 独立歌曲或音频压缩优先 NMR-AAC 参考方案，包含完整预设、AAC q=1.3、48 kHz SoXR、封面和元数据映射；视频里的音轨改用视频技能的 Opus。
---

# 歌曲与独立音频压缩

## 触发

用于用户要求压缩歌曲、音乐或独立音频。先读取 `references/NMR-AAC.md`，它已包含完整参考 JSON，不需要用户安装预设，也不要读取开发者机器上的固定路径。

如果是在视频转码中处理伴随音轨，优先使用 `nvidia-video` 的 Opus 配置；不要把本技能整份音频预设套进视频任务并丢弃其画面。用户明确指定 AAC、其他格式、多声道保留或无损时优先满足要求。

## NMR-AAC 参考参数

输出 `.m4a`，参考预设的音频自定义参数为：

```text
-map 0:a:0 -map 0:v:disp:attached_pic? -map_metadata 0 -af "aresample=48000:resampler=soxr:precision=28" -c:a:0 aac -q:a:0 1.3 -ac:a:0 2 -c:v:0 copy
```

- `-map 0:a:0` 处理第一条音轨；用户指定其他流或多轨时据实调整，不悄悄丢弃需要的音轨。
- 可选 `attached_pic?` 映射及 `-c:v:0 copy` 用于封面，不能误复制普通视频画面。
- `-map_metadata 0` 复制可被 M4A 表达的元数据；歌词标签、歌词字幕和外置 LRC 不是同一概念。参考备注写有封面和歌词转换，实际仍须探测及核对输出，不能保证所有来源的歌词流自动保留。
- SoXR 重采样到 48 kHz、precision 28，AAC 质量 `1.3`，双声道。保留多声道或原采样率是用户目标时应修改相应值，不把 stereo/48 kHz 当成无条件最优。
- 参考预设刻意将常规音频编码器字段留空，由自定义参数生成，不再叠加另一套 `-c:a`/码率选项。

这里的 NMR-AAC 指配套 FFmpeg 构建的 AAC/NMR 方案，实际 FFmpeg 编码器名是 **aac**，不是 `nmraac`、`nmr_aac` 或其他猜测名称。推荐从 https://github.com/Dominic485649/ffmpeg-full/releases/latest 下载该仓库的 **setup 版本 FFmpeg** 和新预设，先核对当前发行说明及本机 `-h encoder=aac`、SoXR 能力。核对 `-h encoder=aac` 显示的 `aac_coder` 是否包含 `nmr`，以及当前默认值是否为 `nmr`。参考 JSON 没有显式指定 coder，只有本机默认 NMR 时才原样使用；有 nmr 但不是默认值时在该音频自定义参数中补 `-aac_coder:a:0 nmr`，没有该选项则需更新构建。不能把旧 twoloop 输出称为 NMR，不能声称任意发行版的同名 aac 实现都等价；不能未经授权安装替换。

“听觉无损”是该有损预设的使用目标，不是数学无损，也不保证所有人、所有曲目均不可分辨。已有 AAC/MP3/Opus 等有损源二次转码可能损伤，不会恢复原本丢失的音质。

## 执行验证

读取当前面板与字段，整套配置请求才应用完整 JSON，保留用户指定的输出目录与命名要求；局部请求只改相关字段。检查应用实际生成的命令中音轨、封面、编码器和质量值是否正确。按权限检查输入流和元数据，批量前用短样本验证当前构建；结束后核对音频 codec、采样率、声道、封面及可表示的标签。不要只因进程 exit_code=0 就宣称歌词和封面全部保留。
