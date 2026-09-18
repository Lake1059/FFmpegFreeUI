using System.Drawing;
using System.Reflection;
using FFmpegFreeUI;
using LakeUI;

internal static partial class Program
{
    private static readonly string[] ModelNames = {
        "vmaf_v0.6.1", "vmaf_v0.6.1neg", "vmaf_4k_v0.6.1neg",
        "vmaf_v1.0.0", "vmaf_4k_v1.0.0", // legacy names remain explicit manual choices
        "vmaf_1080p_3d0h_v1.0.1", "vmaf_1080p_5d0h_v1.0.1",
        "vmaf_1080p_hfr_3d0h_v1.0.1", "vmaf_4k_1d5h_v1.0.1",
        "vmaf_4k_3d0h_v1.0.1", "vmaf_4k_hfr_1d5h_v1.0.1",
        "vmaf_float_v0.6.1", "vmaf_b_v0.6.3"
    };

    private static object StaticQuality(string method, params object[] args) =>
        typeof(Form_v6_集成工具_质量评测).GetMethod(method, BindingFlags.NonPublic | BindingFlags.Static)!.Invoke(null, args)!;

    private static List<VmafModelDisplayItem> Models() =>
        (List<VmafModelDisplayItem>)StaticQuality("构建Vmaf模型显示项", ModelNames, true);

    private static VmafModelDisplayItem Auto(List<VmafModelDisplayItem> models, int width, int height, double fps, out string note)
    {
        object[] args = { models, width, height, fps, "" };
        var selected = (VmafModelDisplayItem)StaticQuality("解析AutoVmaf模型", args);
        note = (string)args[4];
        return selected;
    }

    private static void TestThemeAndVmaf()
    {
        var models = Models();
        var published = (List<VmafModelDisplayItem>)StaticQuality("构建Vmaf模型显示项", new[] {
            "vmaf_v1.0.16_3d0h", "vmaf_v1.0.16_hfr_3d0h", "vmaf_v1.0.16_1d5h_2160",
            "vmaf_v1.0.16_hfr_1d5h_2160", "vmaf_v1.0.16_3d0h_2160" }, true);
        Check(Auto(published, 3840, 1600, 60, out _).ModelValue == "vmaf_v1.0.16_hfr_1d5h_2160", "Published V1 naming");
        Check(Auto(published, 1920, 1080, 24, out _).ModelValue == "vmaf_v1.0.16_3d0h", "Published V1 normal naming");
        TestQualityState();
        Check(!models.Any(x => x.ModelValue == "vmaf_v0.6.1" || x.ModelValue.Contains("float") || x.ModelValue.Contains("_b_")), "Hidden model families leaked");
        Check(models.Where(x => x.UseCuda).All(x => x.ModelValue.Contains("v0.") && x.ModelValue.Contains("neg")), "V1 must not offer CUDA");
        using var combo = new VmafModelComboBox();
        combo.ReplaceDisplayItems(models);
        Check(combo.SelectedModelValue == "AUTO" && combo.SelectedIndex == 1, "AUTO must be first/default model");
        Check(combo.Text == "AUTO" && combo.Items.Skip(1).SequenceEqual(combo.Models.Select(item => item.DisplayText)), "Combo text must use friendly names instead of raw model IDs");
        foreach (var model in models)
        {
            combo.SelectedIndex = combo.FindModelIndex(model.ModelValue, model.UseCuda);
            Check(combo.Text == model.DisplayText && combo.SelectedModelValue == model.ModelValue && combo.SelectedUsesCuda == model.UseCuda, "Friendly text must preserve model and CUDA identity");
        }
        combo.SelectedIndex = combo.FirstModelIndex;
        var browseCount = 0;
        combo.BrowseRequested += (_, _) => browseCount++;
        combo.SelectedIndex = 0;
        Check(browseCount == 1 && combo.SelectedModelValue == "AUTO", "Cancelled browse lost AUTO");
        combo.AddDisplayItem(new() { ModelValue = @"C:\model.json", IsLocal = true });
        Check(combo.Text == "本地 · model.json", "Local model display must not expose the full path");
        combo.SelectedIndex = 0;
        Check(combo.SelectedModelValue == @"C:\model.json", "Browse lost explicit local model");
        foreach (var size in new[] { (3840, 2160), (3840, 1600), (2160, 3840), (7680, 4320) })
            Check(Auto(models, size.Item1, size.Item2, 24, out _).ModelValue == "vmaf_4k_1d5h_v1.0.1", "4K default distance/resolution");
        Check(Auto(models, 2560, 1440, 47.99, out _).ModelValue == "vmaf_1080p_3d0h_v1.0.1", "Non4K normal preference");
        foreach (var fps in new[] { 48d, 50, 59.94, 60 })
            Check(Auto(models, 1920, 1080, fps, out var note).IsHfr && !note.Contains("校准区间"), "HFR boundary");
        Check(Auto(models, 3840, 2160, 120, out var warning).IsHfr && warning.Contains("校准区间"), "120 fps warning");
        foreach (var fps in new[] { 0d, double.NaN, double.PositiveInfinity })
            Check(!Auto(models, 1920, 1080, fps, out var note).IsHfr && note.Contains("无法读取帧率"), "Unknown fps must not choose HFR");
        var oldModels = models.Where(x => x.ModelValue.Contains("v0.")).ToList();
        Check(Auto(oldModels, 3840, 1600, 60, out var fallback).ModelValue == "vmaf_4k_v0.6.1neg" && fallback.Contains("回退"), "Same-resolution NEG fallback");
        Check(!Auto(models.Where(x => !x.IsHfr).ToList(), 1920, 1080, 60, out var missingHfr).IsHfr && missingHfr.Contains("缺少匹配"), "Missing HFR fallback");
        foreach (var input in new[] { (new List<VmafModelDisplayItem>(), 1920, 1080), (models.Where(x => !x.Is4K).ToList(), 3840, 2160), (models, 0, 1080) })
        {
            try { Auto(input.Item1, input.Item2, input.Item3, 24, out _); throw new Exception("AUTO should fail closed"); }
            catch (TargetInvocationException ex) when (ex.InnerException is InvalidOperationException) { checks++; }
        }

        var previousTheme = 设置_v6.实例对象.界面主题;
        using var form = new Form { BackColor = Color.FromArgb(24, 24, 24) };
        using var nav = new ModernTabListControl { TabStripBackColor = Color.FromArgb(48, 48, 48) };
        using var control = new ModernComboBox { BackColor1 = Color.FromArgb(40, 220, 220, 220) };
        form.Controls.Add(nav); form.Controls.Add(control);
        using var dynamicLabel = new HtmlColorLabel { Text = "<span style=\"color:#9ACD32\">0</span>" };
        form.Controls.Add(dynamicLabel);
        try
        {
            设置_v6.实例对象.界面主题 = 1;
            界面主题_v6.刷新主题(true);
            界面主题_v6.应用当前主题到窗体(form);
            Check(form.BackColor.ToArgb() == Color.White.ToArgb(), "Content background");
            Check(nav.TabStripBackColor == Color.FromArgb(220, 234, 247), "Navigation background");
            Check(nav.IndicatorColor == Color.FromArgb(15, 124, 140), "Fixed teal indicator");
            Check(nav.TabItemSelectedBackColor == Color.FromArgb(200, 227, 232), "Light teal selection");
            Check(control.BackColor1 == Color.FromArgb(241, 247, 252), "Control background must not become dark grey");
            TestThemeBorders();
            dynamicLabel.Text = "<span style=\"color:#007A00\">15</span>";
            界面主题_v6.应用当前主题到窗体(form);
            Check(dynamicLabel.Text.Contains(">15<"), "Theme snapshot must not restore stale counters");
            form.BackColor = Color.FromArgb(24, 24, 24); // simulate Attach resetting during Load
            typeof(Form).GetMethod("OnLoad", PrivateInstance)!.Invoke(form, new object[] { EventArgs.Empty });
            Check(form.BackColor.ToArgb() == Color.White.ToArgb(), "Load must restore theme before first paint");
            var strategy = new 旧版兼容编码队列展示策略_v6();
            var row = new UltraDetailListView.ListItem();
            strategy.应用(new 编码任务_v6(), row);
            Check(row.SubItems.All(x => x.ForeColor.ToArgb() == Color.Black.ToArgb()), "Pending queue text must be black");
            using var chart = new Form_v6_集成工具_质量评测图表();
            界面主题_v6.应用当前主题到窗体(chart);
            Invoke(chart, "设置数据源", new Func<string, Dictionary<string, List<double>>>(_ => new() { ["sample"] = new() { 80, 90 } }), new Func<string, Dictionary<string, string>>(_ => new()));
            Invoke(chart, "刷新图表");
            var graph = (Ultra2DChart)chart.Controls.Find("Ultra2DChart1", true).Single();
            Check(graph.PlotBackColor.ToArgb() == Color.White.ToArgb(), "Chart plot must be white");
            Check(graph.Series[0].LineThickness == 2 && graph.Series[0].Color == Color.FromArgb(0, 94, 184), "Light chart saturated line");
            设置_v6.实例对象.界面主题 = 2;
            界面主题_v6.刷新主题(true);
            界面主题_v6.应用当前主题到窗体(form);
            界面主题_v6.应用当前主题到窗体(chart);
            Invoke(chart, "刷新图表");
            Check(form.BackColor == Color.FromArgb(24, 24, 24) && control.BackColor1 == Color.FromArgb(40, 220, 220, 220), "Dark theme originals must be restored");
            Check(graph.Series[0].LineThickness == 1 && graph.Series[0].Color == Color.FromArgb(230, 230, 230), "Dark chart unchanged");
        }
        finally { 设置_v6.实例对象.界面主题 = previousTheme; 界面主题_v6.刷新主题(true); }
    }

    private static void TestThemeBorders()
    {
        using var form = new Form();
        using var secondary = new ModernTabListControl { TabStripBackColor = Color.FromArgb(36, 36, 36) };
        using var primary = new ModernTabListControl { TabStripBackColor = Color.FromArgb(48, 48, 48) };
        using var layout = new ModernPanel { BackColor = Color.Transparent, BackColor1 = Color.Transparent, BorderSize = 0 };
        using var card = new ModernPanel { BackColor1 = Color.FromArgb(40, 220, 220, 220), BorderSize = 0 };
        using var button = new ModernButton { BorderSize = 0, BorderColor = Color.Transparent };
        using var text = new ModernTextBox { BorderSize = 0 };
        using var list = new ModernListBox { BorderSize = 0 };
        using var detail = new UltraDetailListView { BorderSize = 0 };
        using var box = new ModernCheckBox { BoxBorderSize = 0 };
        using var warning = new ModernButton { BorderSize = 2, BorderColor = Color.Red };
        form.Controls.AddRange(new Control[] { primary, secondary, layout, card, button, text, list, detail, box, warning });
        using var chrome = new ThisIsYourWindow { CaptionBackColor = Color.FromArgb(48, 48, 48), CaptionInactiveBackColor = Color.FromArgb(48, 48, 48) };
        var applyObject = typeof(界面主题_v6).GetMethod("应用对象颜色", BindingFlags.Static | BindingFlags.NonPublic)!;
        applyObject.Invoke(null, new object[] { chrome, true });
        Check(chrome.CaptionBackColor == Color.FromArgb(220, 234, 247) && chrome.CaptionInactiveBackColor == chrome.CaptionBackColor, "Titlebar matches primary navigation");
        using var home = new Form_v6_起始页面();
        var homeCards = new[] { "ModernPanel4", "ModernPanel5", "MP_新闻列表" }
            .Select(name => (ModernPanel)home.Controls.Find(name, true).Single()).ToArray();
        var homeOverlays = homeCards.Select(panel => panel.OverlayColor).ToArray();
        using var tipLabel = new HtmlColorLabel { ToolTipBackColor = Color.FromArgb(50, 50, 50) };
        form.Controls.Add(tipLabel);
        var unlockField = typeof(界面主题_v6).Assembly.GetType("FFmpegFreeUI.Module1")!.GetField("SP_UnLock", BindingFlags.Public | BindingFlags.Static)!;
        var savedUnlock = unlockField.GetValue(null);
        var savedStyle = 设置_v6.实例对象.窗口样式;
        var savedGlass = 设置_v6.实例对象.SP_毛玻璃模式;
        try
        {
            unlockField.SetValue(null, true);
            设置_v6.实例对象.窗口样式 = 2;
            设置_v6.实例对象.SP_毛玻璃模式 = 1;
            界面主题_v6.应用当前主题到窗体(form);
            Check(primary.TabStripBackColor.A == 0 && secondary.TabStripBackColor.A == 0, "All navigation levels remain transparent in glass mode");
            Check(new[] { button.BackColor1.A, text.BackColor1.A, list.BackColor1.A, card.BackColor1.A }.All(alpha => alpha < 255), "Glass mode control backgrounds retain alpha");
            界面主题_v6.应用当前主题到窗体(form);
            Check(secondary.TabStripBackColor.A == 0, "Theme reapplication must not make navigation opaque");
            Check(tipLabel.ToolTipBackColor == Color.FromArgb(220, 255, 255, 255), "Detail tooltip uses readable white independent of glass alpha");
        }
        finally
        {
            unlockField.SetValue(null, savedUnlock);
            设置_v6.实例对象.窗口样式 = savedStyle;
            设置_v6.实例对象.SP_毛玻璃模式 = savedGlass;
        }
        for (var pass = 0; pass < 3; pass++)
        {
            设置_v6.实例对象.界面主题 = 1;
            界面主题_v6.刷新主题(true);
            界面主题_v6.应用当前主题到窗体(form);
            Check(primary.TabStripBackColor == Color.FromArgb(220, 234, 247) && secondary.TabStripBackColor == Color.FromArgb(236, 243, 250), "Navigation must use original snapshot across toggles");
            界面主题_v6.应用当前主题到窗体(home);
            Check(homeCards.Select((panel, index) => panel.OverlayColor == Color.FromArgb(homeOverlays[index].A, 236, 243, 250) && panel.BorderSize == 0).All(value => value), "Home sections preserve alpha with light blue and no borders");
            Check(layout.BorderSize == 0 && card.BorderSize == 0, "Layout and small message cards remain borderless");
            Check(tipLabel.ToolTipBackColor == Color.FromArgb(220, 255, 255, 255), "Light detail tooltip background is consistent across modes");
            Check(button.BorderSize == 0 && text.BorderSize == 0 && list.BorderSize == 0 && detail.BorderSize == 0 && box.BoxBorderSize == 0 && warning.BorderSize == 2, "Theme must preserve all original border widths");
            Check(warning.BorderColor == Color.Red, "Semantic border retained");
            list.Enabled = false;
            Check(list.BorderSize == 0, "Disabled list stays borderless");
            list.Enabled = true;
            typeof(Control).GetMethod("OnEnter", PrivateInstance)!.Invoke(list, new object[] { EventArgs.Empty });
            Check(list.BorderSize == 0, "Focus must not add an outline");
            typeof(Control).GetMethod("OnLeave", PrivateInstance)!.Invoke(list, new object[] { EventArgs.Empty });
            Check(list.BorderSize == 0, "Focus loss preserves original border width");
            primary.TabStripBackColor = Color.White; // runtime mutation must not reclassify the navigation
            设置_v6.实例对象.界面主题 = 2;
            界面主题_v6.刷新主题(true);
            界面主题_v6.应用当前主题到窗体(form);
            Check(button.BorderSize == 0 && card.BorderSize == 0 && text.BorderSize == 0 && list.BorderSize == 0 && detail.BorderSize == 0 && box.BoxBorderSize == 0 && warning.BorderSize == 2, "Dark border widths restored");
            Check(button.BorderColor == Color.Transparent, "Dark border color restored");
            Check(tipLabel.ToolTipBackColor == Color.FromArgb(50, 50, 50), "Dark tooltip background restored");
            界面主题_v6.应用当前主题到窗体(home);
            Check(homeCards.Select(panel => panel.OverlayColor).SequenceEqual(homeOverlays), "Home dark overlays restored");
        }
        设置_v6.实例对象.界面主题 = 1;
        界面主题_v6.刷新主题(true);
    }

    private static void TestQualityState()
    {
        var previous = 设置_v6.实例对象.质量评测页面状态;
        try
        {
            foreach (var model in new[] { "", "AUTO", "vmaf_v0.6.1", "vmaf_v0.6.1neg", "vmaf_v1.0.16_3d0h", @"C:\local.json" })
            {
                设置_v6.实例对象.质量评测页面状态 = System.Text.Json.JsonSerializer.Serialize(new {
                    Vmaf模型 = model, VmafCuda = model == "vmaf_v0.6.1neg", Pooling = "mean", SubSample = "1",
                    文件 = new[] { new { 文件路径 = Environment.ProcessPath, 状态 = "完成", 指标结果 = new Dictionary<string, object> {
                        ["VMAF"] = new { 成功 = true, 汇总值 = 90d, 每帧数据 = new[] { 80d, 100d }, 实际模型 = "vmaf_v1.0.16_hfr_3d0h", 模型说明 = "120 fps 超出校准区间，结果仅供参考" }
                    } } }
                });
                using var page = new Form_v6_集成工具_质量评测();
                Invoke(page, "初始化控件");
                Invoke(page, "恢复页面状态");
                var combo = (VmafModelComboBox)page.Controls.Find("MCB_模型选择", true).Single();
                Check(combo.SelectedModelValue == "AUTO" && combo.Text == "AUTO", "Opening page must use AUTO regardless of saved model");
                Check(!combo.SelectedUsesCuda && combo.SelectedModel.IsAuto, "Opening page must not restore manual CUDA/local choices");
                Check(combo.Models.Count == 1, "Legacy model names must not be inserted into the selector");
                var report = (string)Invoke(page, "生成当前列表导出记录")!;
                Check(report.Contains("实际模型：vmaf_v1.0.16_hfr_3d0h") && report.Contains("超出校准区间"), "Export actual model and warning");
                Invoke(page, "保存页面状态");
                using var saved = System.Text.Json.JsonDocument.Parse(设置_v6.实例对象.质量评测页面状态);
                Check(saved.RootElement.GetProperty("Vmaf模型").GetString() == "AUTO", "Save the current AUTO choice");
                Check(saved.RootElement.GetProperty("文件")[0].GetProperty("指标结果").GetProperty("VMAF").GetProperty("实际模型").GetString() == "vmaf_v1.0.16_hfr_3d0h", "Persist actual result model");
            }
            设置_v6.实例对象.质量评测页面状态 = System.Text.Json.JsonSerializer.Serialize(new {
                Vmaf模型 = "vmaf_v1.0.16_3d0h", VmafCuda = true, Pooling = "mean", SubSample = "1"
            });
            using var legacyV1Cuda = new Form_v6_集成工具_质量评测();
            Invoke(legacyV1Cuda, "初始化控件");
            Invoke(legacyV1Cuda, "恢复页面状态");
            Check(!((VmafModelComboBox)legacyV1Cuda.Controls.Find("MCB_模型选择", true).Single()).SelectedUsesCuda, "Legacy V1 CUDA state must be rejected");
            TestModelDetailRowRemoved();
        }
        finally { 设置_v6.实例对象.质量评测页面状态 = previous; }
    }

    private static void TestModelDetailRowRemoved()
    {
        using var page = new Form_v6_集成工具_质量评测();
        Invoke(page, "初始化控件");
        var modelRow = page.Controls.Find("Panel4", true).Single();
        var heading = page.Controls.Find("HtmlColorLabel8", true).Single();
        using var host = new Panel { Size = page.ClientSize };
        host.Controls.Add(modelRow.Parent!);
        host.PerformLayout();
        modelRow.Parent!.PerformLayout();
        Check(heading.Top == modelRow.Bottom, "VMAF model selection must not render an AUTO detail row");
    }

    private static async Task TestRealVmaf(string directory, string ffmpegPath, string input)
    {
        var names = (List<string>)StaticQuality("从FFmpeg运行库提取Vmaf模型列表", Path.GetFullPath(ffmpegPath));
        var models = (List<VmafModelDisplayItem>)StaticQuality("构建Vmaf模型显示项", names, false);
        if (models.Count == 0) { Console.WriteLine("SKIP: supplied FFmpeg has no discoverable VMAF models"); return; }
        var selected = Auto(models, 64, 64, 12, out var note);
        using var page = new Form_v6_集成工具_质量评测();
        typeof(Form_v6_集成工具_质量评测).GetField("本轮Vmaf模型", PrivateInstance)!.SetValue(page, selected);
        var infoType = typeof(Form_v6_集成工具_质量评测).GetNestedType("视频流信息", BindingFlags.NonPublic)!;
        var info = Activator.CreateInstance(infoType)!;
        infoType.GetProperty("宽度")!.SetValue(info, 64);
        infoType.GetProperty("高度")!.SetValue(info, 64);
        infoType.GetProperty("帧率")!.SetValue(info, "12/1");
        infoType.GetProperty("像素格式")!.SetValue(info, "yuv420p");
        var metricType = typeof(Form_v6_集成工具_质量评测).GetNestedType("指标类型", BindingFlags.NonPublic)!;
        var log = Path.Combine(directory, "auto-vmaf.json");
        var command = (string)Invoke(page, "构建指标命令", input, input, Enum.Parse(metricType, "VMAF"), "", "0.2", log, info, info)!;
        SynchronizationContext.SetSynchronizationContext(null);
        Check(command.Contains(selected.ModelValue) && !command.Contains("version=AUTO") && !command.Contains("libvmaf_cuda"), "Actual AUTO model must reach CPU command");
        var task = 编码队列_v6.添加命令行任务(command, "VMAF AUTO smoke", "");
        编码队列_v6.开始任务(new[] { task.ID });
        await WaitUntil(() => !task.正在执行, "VMAF smoke did not finish");
        Check(task.状态 == 编码任务状态_v6.已完成 && File.Exists(log), task.获取日志文本());
        using var result = System.Text.Json.JsonDocument.Parse(File.ReadAllText(log));
        Check(result.RootElement.GetProperty("frames").GetArrayLength() > 0, "Real VMAF must produce frame scores");
        Console.WriteLine("PASS: real " + note);
        编码队列_v6.移除任务(new[] { task.ID });
    }

    // Explicit opt-in preview; never reads or saves the user's application settings.
    private static void ShowThemePreview()
    {
        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.ThrowException);
        设置_v6.实例对象 = new 设置_v6 { 界面主题 = 1 };
        界面主题_v6.初始化();
        using var form = new Form_v6_集成工具_质量评测 { Text = "Light UI regression preview", Size = new Size(1120, 720) };
        界面主题_v6.应用当前主题到窗体(form);
        form.Load += (_, _) => ((VmafModelComboBox)form.Controls.Find("MCB_模型选择", true).Single()).ReplaceDisplayItems(Models());
        Application.Run(form);
    }
}
