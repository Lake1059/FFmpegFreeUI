using System.Drawing;
using System.Text.Json;
using FFmpegFreeUI.PluginSdk;

namespace ThreeFui.PluginApi.Sample;

public sealed partial class SamplePlugin
{
    private static readonly Color PageColor = Color.FromArgb(24, 24, 24);
    private static readonly Color InputColor = Color.FromArgb(56, 56, 56);
    private static readonly Color TextColor = Color.Gainsboro;
    private static readonly Color AccentColor = Color.FromArgb(70, 96, 180);

    /// <summary>装饰型锚点必须返回 null；这里给原生质量模式控件增加无障碍说明和事件观察。</summary>
    private Control? CreateQualityModeDecoration(IPluginUiContext context)
    {
        var target = context.AnchorControl;
        target.AccessibleDescription = "可由 C# Plugin API 综合示例配合使用的原生质量模式控件";

        EventHandler changed = (_, _) =>
            Log(PluginLogLevel.Trace, $"{context.SurfaceId} 的质量模式变为：{target.Text}");
        target.TextChanged += changed;
        target.Disposed += (_, _) => target.TextChanged -= changed;
        return null;
    }

    /// <summary>第二个装饰型锚点示例；ExtensionId / AnchorId 可用于诊断当前注册来源。</summary>
    private Control? CreateParameterNameDecoration(IPluginUiContext context)
    {
        context.AnchorControl.AccessibleDescription =
            $"扩展 {context.ExtensionId} 正在装饰锚点 {context.AnchorId}";
        return null;
    }

    /// <summary>第三个装饰型锚点示例；不依赖 LakeUI 的具体控件类型。</summary>
    private Control? CreateQualityValueDecoration(IPluginUiContext context)
    {
        context.AnchorControl.AccessibleName = "原生质量值（支持插件策略）";
        return null;
    }

    /// <summary>插入下拉框、输入框和按钮，并通过 StateJson 随预设保存。</summary>
    private Control CreateQualityPolicyRow(IPluginUiContext context)
    {
        var row = CreateRow(54);
        var title = CreateLabel("示例质量策略", 135);
        var mode = CreateComboBox(190, "不介入原生参数", "任务开始前填写 CRF");
        var crf = new NumericUpDown
        {
            BackColor = InputColor,
            BorderStyle = BorderStyle.FixedSingle,
            ForeColor = Color.White,
            Minimum = 0,
            Maximum = 63,
            Width = 72
        };
        var applyButton = CreateButton("同步到原生控件", 130);
        var identity = CreateLabel(string.Empty, 310);
        row.Controls.AddRange(new Control[] { title, mode, crf, applyButton, identity });

        // ContainerControl 对插入型锚点非空；GetAnchorControl 可安全取得同一参数面板中的其他公开锚点。
        var nativeMode = context.GetAnchorControl(ThreeFuiUiAnchors.ParametersVideoQualityMode);
        var nativeName = context.GetAnchorControl(ThreeFuiUiAnchors.ParametersVideoQualityParameterName);
        var nativeValue = context.GetAnchorControl(ThreeFuiUiAnchors.ParametersVideoQualityValue);
        identity.Text =
            $"Plugin={context.PluginId}；Surface={context.SurfaceId}；" +
            $"容器={context.ContainerControl?.GetType().Name ?? "无"}";

        var restoring = false;
        void Restore()
        {
            restoring = true;
            try
            {
                var state = DeserializeState(context.StateJson);
                mode.SelectedIndex = state.Enabled ? 1 : 0;
                crf.Value = Math.Clamp(state.Crf, (int)crf.Minimum, (int)crf.Maximum);
            }
            finally
            {
                restoring = false;
            }
        }

        void Save()
        {
            if (restoring)
            {
                return;
            }

            var state = DeserializeState(context.StateJson);
            state.Enabled = mode.SelectedIndex == 1;
            state.Crf = decimal.ToInt32(crf.Value);
            context.StateJson = JsonSerializer.Serialize(state);
            context.RequestParameterRefresh();
        }

        mode.SelectedIndexChanged += (_, _) => Save();
        crf.ValueChanged += (_, _) => Save();
        applyButton.Click += (_, _) =>
        {
            if (nativeMode is not null)
            {
                nativeMode.Text = "恒定质量 CRF - CPU 编码首选";
            }
            if (nativeName is not null)
            {
                nativeName.Text = "-crf";
            }
            if (nativeValue is not null)
            {
                nativeValue.Text = crf.Value.ToString();
            }
            context.RequestParameterRefresh();
        };

        EventHandler restored = (_, _) => Restore();
        context.StateRestored += restored;
        row.Disposed += (_, _) => context.StateRestored -= restored;
        Restore();
        return row;
    }

    /// <summary>插入命令和进程相关选项，展示同一插件的多个 UI 扩展共享 StateJson。</summary>
    private static Control CreateCommandOptionsRow(IPluginUiContext context)
    {
        var panel = CreateRow(88);
        panel.FlowDirection = FlowDirection.TopDown;
        var firstLine = CreateRow(34);
        var secondLine = CreateRow(34);
        var prefixName = new CheckBox { AutoSize = true, ForeColor = TextColor, Text = "任务名加示例标记" };
        var noStats = new CheckBox { AutoSize = true, ForeColor = TextColor, Text = "命令前置 -nostats" };
        var noStdin = new CheckBox { AutoSize = true, ForeColor = TextColor, Text = "进程启动前置 -nostdin" };
        var acceptOne = new CheckBox { AutoSize = true, ForeColor = TextColor, Text = "把退出码 1 视为成功（仅演示）" };
        var suffix = CreateTextBox(130, "输出名后缀，例如 .checked");
        var process = CreateTextBox(220, "可选：替换 ProcessFileName");
        var advanced = CreateTextBox(310, "可选：追加到进阶质量参数");
        firstLine.Controls.AddRange(new Control[] { prefixName, noStats, noStdin, acceptOne });
        secondLine.Controls.AddRange(new Control[] { suffix, process, advanced });
        panel.Controls.Add(firstLine);
        panel.Controls.Add(secondLine);

        var restoring = false;
        void Restore()
        {
            restoring = true;
            try
            {
                var state = DeserializeState(context.StateJson);
                prefixName.Checked = state.PrefixTaskName;
                noStats.Checked = state.AddNoStats;
                noStdin.Checked = state.AddNoStdin;
                acceptOne.Checked = state.AcceptExitCodeOne;
                suffix.Text = state.OutputSuffix;
                process.Text = state.ProcessOverride;
                advanced.Text = state.AdvancedArguments;
            }
            finally
            {
                restoring = false;
            }
        }

        void Save()
        {
            if (restoring)
            {
                return;
            }

            var state = DeserializeState(context.StateJson);
            state.PrefixTaskName = prefixName.Checked;
            state.AddNoStats = noStats.Checked;
            state.AddNoStdin = noStdin.Checked;
            state.AcceptExitCodeOne = acceptOne.Checked;
            state.OutputSuffix = suffix.Text.Trim();
            state.ProcessOverride = process.Text.Trim();
            state.AdvancedArguments = advanced.Text.Trim();
            context.StateJson = JsonSerializer.Serialize(state);
            context.RequestParameterRefresh();
        }

        prefixName.CheckedChanged += (_, _) => Save();
        noStats.CheckedChanged += (_, _) => Save();
        noStdin.CheckedChanged += (_, _) => Save();
        acceptOne.CheckedChanged += (_, _) => Save();
        suffix.TextChanged += (_, _) => Save();
        process.TextChanged += (_, _) => Save();
        advanced.TextChanged += (_, _) => Save();
        EventHandler restored = (_, _) => Restore();
        context.StateRestored += restored;
        panel.Disposed += (_, _) => context.StateRestored -= restored;
        Restore();
        return panel;
    }

    /// <summary>第三个插入型锚点：控制成功后 SHA-256 校验，并提供显式刷新按钮。</summary>
    private static Control CreatePostProcessRow(IPluginUiContext context)
    {
        var row = CreateRow(48);
        var hash = new CheckBox
        {
            AutoSize = true,
            ForeColor = TextColor,
            Text = "编码成功后计算输出文件 SHA-256"
        };
        var refresh = CreateButton("刷新参数预览", 120);
        row.Controls.Add(hash);
        row.Controls.Add(refresh);

        var restoring = false;
        void Restore()
        {
            restoring = true;
            try
            {
                hash.Checked = DeserializeState(context.StateJson).ComputeSha256;
            }
            finally
            {
                restoring = false;
            }
        }

        hash.CheckedChanged += (_, _) =>
        {
            if (restoring)
            {
                return;
            }
            var state = DeserializeState(context.StateJson);
            state.ComputeSha256 = hash.Checked;
            context.StateJson = JsonSerializer.Serialize(state);
            context.RequestParameterRefresh();
        };
        refresh.Click += (_, _) => context.RequestParameterRefresh();
        EventHandler restored = (_, _) => Restore();
        context.StateRestored += restored;
        row.Disposed += (_, _) => context.StateRestored -= restored;
        Restore();
        return row;
    }

    private static FlowLayoutPanel CreateRow(int height) => new()
    {
        AutoSize = false,
        BackColor = PageColor,
        FlowDirection = FlowDirection.LeftToRight,
        Height = height,
        Padding = new Padding(0, 8, 0, 6),
        WrapContents = false
    };

    private static Label CreateLabel(string text, int width) => new()
    {
        AutoSize = false,
        ForeColor = TextColor,
        Height = 30,
        Text = text,
        TextAlign = ContentAlignment.MiddleLeft,
        Width = width
    };

    private static ComboBox CreateComboBox(int width, params string[] items)
    {
        var result = new ComboBox
        {
            BackColor = InputColor,
            DropDownStyle = ComboBoxStyle.DropDownList,
            FlatStyle = FlatStyle.Flat,
            ForeColor = Color.White,
            Width = width
        };
        result.Items.AddRange(items.Cast<object>().ToArray());
        return result;
    }

    private static TextBox CreateTextBox(int width, string placeholder) => new()
    {
        BackColor = InputColor,
        BorderStyle = BorderStyle.FixedSingle,
        ForeColor = Color.White,
        PlaceholderText = placeholder,
        Width = width
    };

    private static Button CreateButton(string text, int width)
    {
        var result = new Button
        {
            BackColor = AccentColor,
            FlatStyle = FlatStyle.Flat,
            ForeColor = Color.White,
            Height = 30,
            Text = text,
            Width = width
        };
        result.FlatAppearance.BorderSize = 0;
        return result;
    }
}
