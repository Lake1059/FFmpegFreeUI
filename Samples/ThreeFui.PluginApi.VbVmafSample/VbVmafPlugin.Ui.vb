Imports System.Drawing
Imports System.Text.Json
Imports System.Windows.Forms
Imports FFmpegFreeUI.PluginSdk

Partial Public NotInheritable Class VbVmafPlugin
    Private Shared ReadOnly 页面颜色 As Color = Color.FromArgb(24, 24, 24)
    Private Shared ReadOnly 输入颜色 As Color = Color.FromArgb(56, 56, 56)
    Private Shared ReadOnly 文本颜色 As Color = Color.Gainsboro
    Private Shared ReadOnly 强调颜色 As Color = Color.FromArgb(70, 96, 180)

    ''' <summary>装饰型锚点必须返回 Nothing；这里添加无障碍说明并观察原生控件事件。</summary>
    Private Function 创建质量模式装饰(context As IPluginUiContext) As Control
        Dim target = context.AnchorControl
        target.AccessibleDescription = "可由 VB.NET Plugin API 综合示例配合使用的原生质量模式控件"

        Dim changed As EventHandler =
            Sub(sender, args)
                写日志(PluginLogLevel.Trace, $"{context.SurfaceId} 的质量模式变为：{target.Text}")
            End Sub
        AddHandler target.TextChanged, changed
        AddHandler target.Disposed, Sub(sender, args) RemoveHandler target.TextChanged, changed
        Return Nothing
    End Function

    ''' <summary>第二个装饰型锚点展示 ExtensionId 和 AnchorId。</summary>
    Private Shared Function 创建参数名装饰(context As IPluginUiContext) As Control
        context.AnchorControl.AccessibleDescription =
            $"扩展 {context.ExtensionId} 正在装饰锚点 {context.AnchorId}"
        Return Nothing
    End Function

    ''' <summary>第三个装饰型锚点只依赖 WinForms Control 公共成员，不绑定 LakeUI 具体类型。</summary>
    Private Shared Function 创建质量值装饰(context As IPluginUiContext) As Control
        context.AnchorControl.AccessibleName = "原生质量值（支持插件策略）"
        Return Nothing
    End Function

    ''' <summary>插入下拉框、输入框和按钮，并通过 StateJson 随预设保存。</summary>
    Private Shared Function 创建质量策略行(context As IPluginUiContext) As Control
        Dim row = 创建行(54)
        Dim title = 创建标签("示例质量策略", 135)
        Dim mode = 创建下拉框(190, "不介入原生参数", "任务开始前填写 CRF")
        Dim crf As New NumericUpDown With {
            .BackColor = 输入颜色,
            .BorderStyle = BorderStyle.FixedSingle,
            .ForeColor = Color.White,
            .Minimum = 0,
            .Maximum = 63,
            .Width = 72
        }
        Dim applyButton = 创建按钮("同步到原生控件", 130)
        Dim identity = 创建标签("", 310)
        row.Controls.AddRange(New Control() {title, mode, crf, applyButton, identity})

        ' ContainerControl 对插入型锚点非空；GetAnchorControl 可取得同一参数面板中的公开锚点。
        Dim nativeMode = context.GetAnchorControl(ThreeFuiUiAnchors.ParametersVideoQualityMode)
        Dim nativeName = context.GetAnchorControl(ThreeFuiUiAnchors.ParametersVideoQualityParameterName)
        Dim nativeValue = context.GetAnchorControl(ThreeFuiUiAnchors.ParametersVideoQualityValue)
        identity.Text =
            $"Plugin={context.PluginId}；Surface={context.SurfaceId}；" &
            $"容器={If(context.ContainerControl?.GetType().Name, "无")}"

        Dim restoring = False
        Dim restore As Action =
            Sub()
                restoring = True
                Try
                    Dim state = 读取状态(context.StateJson)
                    mode.SelectedIndex = If(state.Enabled, 1, 0)
                    crf.Value = Math.Clamp(state.Crf, CInt(crf.Minimum), CInt(crf.Maximum))
                Finally
                    restoring = False
                End Try
            End Sub
        Dim save As Action =
            Sub()
                If restoring Then Return
                Dim state = 读取状态(context.StateJson)
                state.Enabled = mode.SelectedIndex = 1
                state.Crf = Decimal.ToInt32(crf.Value)
                context.StateJson = JsonSerializer.Serialize(state)
                context.RequestParameterRefresh()
            End Sub

        AddHandler mode.SelectedIndexChanged, Sub(sender, args) save()
        AddHandler crf.ValueChanged, Sub(sender, args) save()
        AddHandler applyButton.Click,
            Sub(sender, args)
                If nativeMode IsNot Nothing Then nativeMode.Text = "恒定质量 CRF - CPU 编码首选"
                If nativeName IsNot Nothing Then nativeName.Text = "-crf"
                If nativeValue IsNot Nothing Then nativeValue.Text = crf.Value.ToString()
                context.RequestParameterRefresh()
            End Sub

        Dim restoredHandler As EventHandler = Sub(sender, args) restore()
        AddHandler context.StateRestored, restoredHandler
        AddHandler row.Disposed, Sub(sender, args) RemoveHandler context.StateRestored, restoredHandler
        restore()
        Return row
    End Function

    ''' <summary>第二个插入型锚点展示同一插件多个扩展共享 StateJson。</summary>
    Private Shared Function 创建命令选项行(context As IPluginUiContext) As Control
        Dim panel = 创建行(88)
        panel.FlowDirection = FlowDirection.TopDown
        Dim firstLine = 创建行(34)
        Dim secondLine = 创建行(34)
        Dim prefixName As New CheckBox With {.AutoSize = True, .ForeColor = 文本颜色, .Text = "任务名加示例标记"}
        Dim noStats As New CheckBox With {.AutoSize = True, .ForeColor = 文本颜色, .Text = "命令前置 -nostats"}
        Dim noStdin As New CheckBox With {.AutoSize = True, .ForeColor = 文本颜色, .Text = "进程启动前置 -nostdin"}
        Dim acceptOne As New CheckBox With {.AutoSize = True, .ForeColor = 文本颜色, .Text = "把退出码 1 视为成功（仅演示）"}
        Dim suffix = 创建输入框(130, "输出名后缀，例如 .checked")
        Dim process = 创建输入框(220, "可选：替换 ProcessFileName")
        Dim advanced = 创建输入框(310, "可选：追加到进阶质量参数")
        firstLine.Controls.AddRange(New Control() {prefixName, noStats, noStdin, acceptOne})
        secondLine.Controls.AddRange(New Control() {suffix, process, advanced})
        panel.Controls.Add(firstLine)
        panel.Controls.Add(secondLine)

        Dim restoring = False
        Dim restore As Action =
            Sub()
                restoring = True
                Try
                    Dim state = 读取状态(context.StateJson)
                    prefixName.Checked = state.PrefixTaskName
                    noStats.Checked = state.AddNoStats
                    noStdin.Checked = state.AddNoStdin
                    acceptOne.Checked = state.AcceptExitCodeOne
                    suffix.Text = state.OutputSuffix
                    process.Text = state.ProcessOverride
                    advanced.Text = state.AdvancedArguments
                Finally
                    restoring = False
                End Try
            End Sub
        Dim save As Action =
            Sub()
                If restoring Then Return
                Dim state = 读取状态(context.StateJson)
                state.PrefixTaskName = prefixName.Checked
                state.AddNoStats = noStats.Checked
                state.AddNoStdin = noStdin.Checked
                state.AcceptExitCodeOne = acceptOne.Checked
                state.OutputSuffix = suffix.Text.Trim()
                state.ProcessOverride = process.Text.Trim()
                state.AdvancedArguments = advanced.Text.Trim()
                context.StateJson = JsonSerializer.Serialize(state)
                context.RequestParameterRefresh()
            End Sub

        AddHandler prefixName.CheckedChanged, Sub(sender, args) save()
        AddHandler noStats.CheckedChanged, Sub(sender, args) save()
        AddHandler noStdin.CheckedChanged, Sub(sender, args) save()
        AddHandler acceptOne.CheckedChanged, Sub(sender, args) save()
        AddHandler suffix.TextChanged, Sub(sender, args) save()
        AddHandler process.TextChanged, Sub(sender, args) save()
        AddHandler advanced.TextChanged, Sub(sender, args) save()
        Dim restoredHandler As EventHandler = Sub(sender, args) restore()
        AddHandler context.StateRestored, restoredHandler
        AddHandler panel.Disposed, Sub(sender, args) RemoveHandler context.StateRestored, restoredHandler
        restore()
        Return panel
    End Function

    ''' <summary>第三个插入型锚点控制 VMAF 后处理，并展示显式刷新。</summary>
    Private Shared Function 创建后处理行(context As IPluginUiContext) As Control
        Dim row = 创建行(48)
        Dim vmaf As New CheckBox With {
            .AutoSize = True,
            .ForeColor = 文本颜色,
            .Text = "编码成功后使用 ffmpeg/libvmaf 计算 VMAF"
        }
        Dim refresh = 创建按钮("刷新参数预览", 120)
        row.Controls.Add(vmaf)
        row.Controls.Add(refresh)

        Dim restoring = False
        Dim restore As Action =
            Sub()
                restoring = True
                Try
                    vmaf.Checked = 读取状态(context.StateJson).ComputeVmaf
                Finally
                    restoring = False
                End Try
            End Sub
        AddHandler vmaf.CheckedChanged,
            Sub(sender, args)
                If restoring Then Return
                Dim state = 读取状态(context.StateJson)
                state.ComputeVmaf = vmaf.Checked
                context.StateJson = JsonSerializer.Serialize(state)
                context.RequestParameterRefresh()
            End Sub
        AddHandler refresh.Click, Sub(sender, args) context.RequestParameterRefresh()
        Dim restoredHandler As EventHandler = Sub(sender, args) restore()
        AddHandler context.StateRestored, restoredHandler
        AddHandler row.Disposed, Sub(sender, args) RemoveHandler context.StateRestored, restoredHandler
        restore()
        Return row
    End Function

    Private Shared Function 创建行(height As Integer) As FlowLayoutPanel
        Return New FlowLayoutPanel With {
            .AutoSize = False,
            .BackColor = 页面颜色,
            .FlowDirection = FlowDirection.LeftToRight,
            .Height = height,
            .Padding = New Padding(0, 8, 0, 6),
            .WrapContents = False
        }
    End Function

    Private Shared Function 创建标签(text As String, width As Integer) As Label
        Return New Label With {
            .AutoSize = False,
            .ForeColor = 文本颜色,
            .Height = 30,
            .Text = text,
            .TextAlign = ContentAlignment.MiddleLeft,
            .Width = width
        }
    End Function

    Private Shared Function 创建下拉框(width As Integer, ParamArray items As String()) As ComboBox
        Dim result As New ComboBox With {
            .BackColor = 输入颜色,
            .DropDownStyle = ComboBoxStyle.DropDownList,
            .FlatStyle = FlatStyle.Flat,
            .ForeColor = Color.White,
            .Width = width
        }
        result.Items.AddRange(items.Cast(Of Object).ToArray())
        Return result
    End Function

    Private Shared Function 创建输入框(width As Integer, placeholder As String) As TextBox
        Return New TextBox With {
            .BackColor = 输入颜色,
            .BorderStyle = BorderStyle.FixedSingle,
            .ForeColor = Color.White,
            .PlaceholderText = placeholder,
            .Width = width
        }
    End Function

    Private Shared Function 创建按钮(text As String, width As Integer) As Button
        Dim result As New Button With {
            .BackColor = 强调颜色,
            .FlatStyle = FlatStyle.Flat,
            .ForeColor = Color.White,
            .Height = 30,
            .Text = text,
            .Width = width
        }
        result.FlatAppearance.BorderSize = 0
        Return result
    End Function
End Class
