# 在 Linux 系统中使用 FFmpegFreeUI 的一般方法

> 本篇内容主要由 [Uyanide](https://github.com/Uyanide) 提供

## 前置条件

由于 FFmpegFreeUI 使用了非跨平台的 UI 框架 WinForms，因此在 Linux 系统中需要通过兼容层运行，这意味着您需要在系统中配置 [Wine](htts://www.winehq.org) 或基于此的其他兼容环境如 [Proton](https://steamcommunity.com/games/221410/announcements/detail/1696055855739350561)。此类软件的配置方法因发行版而异，此处不再赘述，可参阅：

- [Wine Wiki](https://gitlab.winehq.org/wine/wine/-/wikis/home)
- [Wine - ArchWiki](https://wiki.archlinux.org/title/Wine)
- [Steam - ArchWiki](https://wiki.archlinux.org/title/Steam)

此外，使用兼容层通常意味着您可能会在各种场景下遇到各种问题，有些是操作失误，有些是系统 Bug。由于本文档无法覆盖所有可能遇到的问题，因此充分的耐心和强大的包容心是重要的前置条件之一 :)

## 简单使用方法

如果没有特殊需求，您大可以在兼容层中同时运行 FFmpegFreeUI 和 Windows 版的 ffmpeg。这虽然略显丑陋，但确实是最省心的解决方案。

具体操作步骤十分简单，您只需要：

1. 下载 Windows 版本的 [ffmpeg 发行版](https://www.gyan.dev/ffmpeg/builds/)；
2. 将 `ffmpeg.exe` 和 `ffprobe.exe` 放置于 `FFmpegFreeUI.exe` 的同级目录；
3. 使用兼容层启动 `FFmpegFreeUI.exe`。

或者，如果您希望将 Windows 版本的 ffmpeg 保存在其他特定目录，可以在 FFmpegFreeUI 的 `替代 Process 的 FileName` 设置项中填入 `ffmpeg.exe` 的完整 [DOS 路径](https://learn.microsoft.com/zh-cn/dotnet/standard/io/file-path-formats#traditional-dos-paths)（如：`Z:\home\username\Downloads\ffmpeg-win\ffmpeg.exe`）。

> 标准 DOS 路径可以包含三个组件：
>
> - 卷号或驱动器号，后跟卷分隔符 (:)。
> - 目录名称。 目录分隔符分隔嵌套目录层次结构中的子目录。
> - 可选文件名。 目录分隔符分隔文件路径和文件名。

关于如何获取 Unix 文件系统下某个文件在 Wine 中对应的 DOS 路径，可参阅 [Wine 下的路径转换](#wine-下的路径转换)。

## 调用原生 ffmpeg

### 完整方案

> [!WARNING]
> 本节内容仅在 Wine 环境经过测试，在其他兼容层如 Steam 的 Proton Experimental 或 Wine Staging 中不保证可用性。

首先，需要准备三个脚本：

1. `wait-exit.bat`：暴露给 FFmpegFreeUI 的最外层脚本，用于调用 `run-ffmpeg.py`，等待 ffmpeg 进程结束，并转发其日志：

   ```bat
   @echo off
   setlocal enabledelayedexpansion

   set "log_file=Z:\tmp\3fui_ffmpeg.log"
   set "ret_file=Z:\tmp\3fui_ffmpeg.ret"
   set "finish_sign=Z:\tmp\3fui_ffmpeg_finish"
   set "log_file_tail=Z:\tmp\3fui_ffmpeg.log.tail"

   del "%log_file%"
   del "%ret_file%"
   del "%finish_sign%"
   del "%log_file_tail%"

   set "delay_script=%1"
   shift
   set "run-ffmpeg=%1"

   set "args="
   :loop
   shift
   if "%~1"=="" goto after_args
   set args=!args! "%~1"
   goto loop
   :after_args
   REM

   start "" %run-ffmpeg% %args%

   :check
   if exist "%finish_sign%" (
       set /p ret=<"%ret_file%"
       if not defined ret (
           set ret=1
       )
       type "%log_file%" 1>&2
       exit /b !ret!
   ) else (
       if exist "%log_file_tail%" (
           type "%log_file_tail%" 1>&2
       )
       cscript %delay_script% 2>nul
       goto check
   )

   endlocal
   ```

2. `run-ffmpeg.py`：实际调用 ffmpeg 的脚本，将其日志重定向至文件：

   ```python
   #!/bin/env python3

   import sys
   import subprocess

   LOG_FILE = "/tmp/3fui_ffmpeg.log"
   LOG_TAIL_FILE = "/tmp/3fui_ffmpeg.log.tail"
   RET_FILE = "/tmp/3fui_ffmpeg.ret"
   FINISH_SIGN = "/tmp/3fui_ffmpeg_finish"


   def main():
       try:
           ffmpeg_cmd = ['ffmpeg'] + sys.argv[1:]

           with open(LOG_FILE, 'w') as log_file, open(LOG_TAIL_FILE, 'w') as tail_file:
               process = subprocess.Popen(
                   ffmpeg_cmd,
                   stderr=subprocess.PIPE,
                   stdout=subprocess.DEVNULL,
                   text=True,
                   bufsize=1
               )
               if not process or not process.stderr:
                   return

               for line in process.stderr:
                   tail_file.seek(0)
                   tail_file.write(line)
                   tail_file.truncate()
                   tail_file.flush()

                   log_file.write(line)
                   log_file.flush()

               return_code = process.wait()

               with open(RET_FILE, 'w') as ret_file:
                   ret_file.write(str(return_code) + '\n')

       except Exception as e:
           with open(RET_FILE, 'w') as ret_file:
               ret_file.write("1\n")

       finally:
           with open(FINISH_SIGN, 'w') as f:
               f.write("")


   if __name__ == "__main__":
       main()
   ```

> [!NOTE]
> 别忘记赋予可执行权限：`chmod +x /path/to/run-ffmpeg.py`

3. `delay.vbs`：在 `wait-exit.bat` 中实现延时的工具脚本：

   ```vb
   WScript.Sleep 1000
   ```

然后，在 `替代 Process 的 FileName` 设置项中填入 `wait-exit.bat` 的完整 [DOS 路径](https://learn.microsoft.com/zh-cn/dotnet/standard/io/file-path-formats#traditional-dos-paths)（引号可选）。如：

`"Z:\path\to\wait-exit.bat"`

在 `覆盖 Process 的参数传递` 设置项中依次填入 `delay.vbs` 和 `run-ffmpeg.py` 的完整 [DOS 路径](https://learn.microsoft.com/zh-cn/dotnet/standard/io/file-path-formats#traditional-dos-paths)（引号可选）与 `<args>`，以空格分隔。如：

`"Z:\path\to\delay.vbs" "Z:\path\to\run-ffmpeg.py" <args>`

最后，勾选 `转译模式`，就可以正常添加文件进行编码了。

### 替代方案

以上三个脚本实现了基本的进度更新与报错显示。如果您不关心这些功能，而只是把 FFmpegFreeUI 当作参数生成器，可以仅勾选 `转译模式` 选项，并在开始任务后忽略软件的报错信息，在编码队列界面点击 `复制命令行`，自行在终端中运行。

或者，如果您不关心进度或报错信息，而只是想让 FFmpegFreeUI 启动原生 ffmpeg，可以进行如下操作：

> [!WARNING]
> 本节剩余内容仅在 Wine 环境经过测试，在其他兼容层如 Steam 的 Proton Experimental / Proton Hotfix 或 Wine Staging 中不保证可用性。

1. 在 `替代 Process 的 FileName` 设置项中填入 `start`；
2. 在 `覆盖 Process 的参数传递` 设置项中填入 `/unix /path/to/ffmpeg <args>`，将其中的 `/path/to/ffmpeg` 替换为您想使用的 ffmpeg 可执行文件的完整 Unix 路径，如 `/usr/bin/ffmpeg`；
3. 勾选 `转译模式`。

此时，如果开始编码任务，对应任务会显示“错误”，ffmpeg 将会在后台持续运行直至任务完成。ffmpeg 的 stderr 输出将在启动 wine 的终端中可见。

> 为什么不用 start /wait 参数？因为这对于启动的 Unix 进程没有作用 :)

## 其他问题

### 界面口口口乱码

FFmpegFreeUI 提供了字体设置选项，目前该设置项位于首页最上方，选择支持中文显示的字体并确认后可解决此问题。

或者，如果您找不到这个设置项或希望在首次启动前配置正确的字体，可在 `FFmpegFreeUI.exe` 同级目录中新建 `Settings.json` 文件，并写入以下内容：

```json
{
  "\u5B57\u4F53": "some font",
  "\u6307\u5B9A\u5904\u7406\u5668\u6838\u5FC3": "",
  "\u81EA\u52A8\u540C\u65F6\u8FD0\u884C\u4EFB\u52A1\u6570\u91CF\u9009\u9879": 0,
  "\u6709\u4EFB\u52A1\u65F6\u7CFB\u7EDF\u4FDD\u6301\u72B6\u6001\u9009\u9879": 0,
  "\u63D0\u793A\u97F3\u9009\u9879": 0,
  "\u5DE5\u4F5C\u76EE\u5F55": "",
  "\u66FF\u4EE3\u8FDB\u7A0B\u6587\u4EF6\u540D": "",
  "\u8986\u76D6\u53C2\u6570\u4F20\u9012": "",
  "\u8F6C\u8BD1\u6A21\u5F0F": true
}
```

将 `some font` 替换为您系统上安装的中文字体。

> [!NOTE]
> 如果您不知道系统上有什么字体，可以用 `fc-list : family` 查看。

> [!NOTE]
> 设置字体后仍然可能会在弹窗等处出现乱码，如果遇到这种问题您可以将乱码复制到正确配置了中文字体的本地文本编辑器中查看内容。

### Wine 下的路径转换

- 一般情况下，您可使用 `winepath -w /unix/path` 将 Unix 路径转换为 DOS 路径，使用 `winepath -u Z:\dos\path` 将 DOS 路径转换为 Unix 路径。

- 或者，您可以查看 `$WINEPREFIX/dosdevices` 下的符号链接以确认盘符映射，将 `$WINEPREFIX` 替换为您实际的 Wine 前缀。`ls -l $WINEPREFIX/dosdevices` 的结果可能如下：

  ```
  lrwxrwxrwx 1 username username 10 Jan 01 00:00 c: -> ../drive_c
  lrwxrwxrwx 1 username username  1 Jan 01 00:00 z: -> /
  ...
  ```

  这表示 `C:\` 映射 `$WINEPREFIX/drive_c/`，`Z:\` 映射 `/`。

> [!NOTE]
> 一般情况下，Wine 会将 `Z:\` 映射为系统根目录 `/`，此外 Proton 通常还会将 `X:\` 映射为用户家目录 `/home/username/`。但这只是经验规律，建议在实际应用场景中根据上述方法自行确认。

### 为什么 XXX 不工作 / 为什么有 XXX 报错 / 为什么 ...

正如开头所说：

> 此外，使用兼容层通常意味着您可能会在各种场景下遇到各种问题，有些是操作失误，有些是系统 Bug。由于本文档无法覆盖所有可能遇到的问题，因此充分的耐心和强大的包容心是重要的前置条件之一 :)

持续不断的 Troubleshooting 是使用 GNU/Linux 系统不得不品的一环，尤其是对于本文所述强行兼容运行 WinForms 应用并调用外部二进制的场景。当然无论遇到任何问题或有任何改进建议都欢迎提出，也欢迎进群一起讨论。

### 高分辨率屏幕 界面过小

输入 `WINEPREFIX="/path/to/your/wine/prefix" winecfg` (如果不懂这是什么 那么直接输入 `winecfg`)

在弹出的界面中选择 "显示" 并在 "屏幕分辨率" 中调整 DPI，高分辨率情况下推荐 144。
