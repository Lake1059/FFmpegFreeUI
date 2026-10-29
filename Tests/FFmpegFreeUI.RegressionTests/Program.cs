using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.Json;
using FFmpegFreeUI;
using LakeUI;

internal static class Program
{
    private static readonly List<object> Results = [];
    private static readonly Type Quality = typeof(Form_v6_集成工具_质量评测);
    private static string Work = "";
    private static int Failed;

    [STAThread]
    private static int Main(string[] args)
    {
        if (args.Length >= 2 && args[0] == "--preview-license")
            return LicensePreview.Run(args[1]);
        if (args.Length >= 3 && args[0] == "--preview-vmaf")
            return VmafPreview.Run(args[1], args[2], args.Length > 3 && args[3] == "dark");
        if (args.Length >= 3 && args[0] == "--preview-capsules")
            return VmafPreview.Run(args[1], args[2], args.Length > 3 && args[3] == "dark", true);
        var capsulesOnly = args.Length > 0 && args[0] == "--capsule-only";
        var rootIndex = capsulesOnly ? 1 : 0;
        var root = Path.GetFullPath(args.Length > rootIndex ? args[rootIndex] : ".");
        Work = Path.Combine(root, "artifacts", "ui-vmaf-20261029", capsulesOnly ? "capsule-tests" : "tests", DateTime.Now.ToString("yyyyMMdd-HHmmss-fff"));
        Directory.CreateDirectory(Work);
        var previous = 设置_v6.实例对象;
        设置_v6.实例对象 = new 设置_v6();
        try
        {
            if (capsulesOnly)
            {
                Check("模型界面：布局、距离配色与胶囊保护", ModelPresentation);
            }
            else
            {
                Check("主题：文字、下拉、列表和导航使用同一选中色", ThemeRoundTrip);
                Check("标题栏：浅色保持深色图标、背景变深且保留红色关闭", CaptionColors);
                Check("弹窗：统一输入选中色且不二次转换浅色背景", PopupSelection);
                Check("界面：HDR 提示、十二行下拉与许可证浅紫色", UiRefinements);
                Check("模型：HDR、HFR、分辨率、距离与 CUDA 分类", ModelParsing);
                Check("模型界面：旧版提示、非 NEG 分组、选择保护与双色胶囊", ModelPresentation);
                Check("HDR 目录：旧引擎仍显示八项且不伪装为已检测", ModelCatalog);
                Check("HDR 目录：未检测项不进入 AUTO，手动评测前明确提示", ModelAvailability);
                Check("AUTO：HDR/SDR、1080/4K、24/48/60/120 fps 与缺模型回退", AutoModels);
                Check("HDR：PQ/HLG 判定与缺失颜色标记拒绝", HdrMetadata);
                Check("模型扫描：识别 hdr_vmaf 前缀", BinaryModelScan);
            }
        }
        finally
        {
            设置_v6.实例对象 = previous;
            File.WriteAllText(Path.Combine(Work, "results.json"), JsonSerializer.Serialize(new
            {
                time = DateTimeOffset.Now,
                assembly = Quality.Assembly.Location,
                failed = Failed,
                tests = Results
            }, new JsonSerializerOptions { WriteIndented = true, Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping }), new UTF8Encoding(false));
        }
        Console.WriteLine($"结果：{Results.Count} 项，失败 {Failed} 项；证据：{Work}");
        return Failed == 0 ? 0 : 1;
    }

    private static void Check(string name, Action action)
    {
        try
        {
            action();
            Results.Add(new { name, status = "passed" });
            Console.WriteLine("通过：" + name);
        }
        catch (Exception ex)
        {
            while (ex is TargetInvocationException && ex.InnerException != null) ex = ex.InnerException;
            Failed++;
            Results.Add(new { name, status = "failed", error = ex.ToString() });
            Console.WriteLine("失败：" + name + "；" + ex.Message);
        }
    }

    private static void Require(bool value, string message)
    {
        if (!value) throw new InvalidOperationException(message);
    }

    private static object? Invoke(Type type, string name, params object?[] args) =>
        type.GetMethod(name, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)!.Invoke(null, args);

    private static void Throws<T>(Action action) where T : Exception
    {
        try { action(); }
        catch (T) { return; }
        catch (TargetInvocationException ex) when (ex.InnerException is T) { return; }
        throw new InvalidOperationException("应当拒绝并抛出 " + typeof(T).Name);
    }

    private static void ThemeRoundTrip()
    {
        using var form = new Form();
        var text = new ModernTextBox { Text = "Input #0 dav1d) (Main), yuv420p10le(tv, bt709)" };
        var combo = new ModernComboBox();
        var list = new ModernListBox();
        var detail = new UltraDetailListView();
        var nav = new ModernTabListControl();
        form.Controls.AddRange([text, combo, list, detail, nav]);
        界面主题_v6.初始化();
        foreach (var light in new[] { true, false, true, false })
        {
            设置_v6.实例对象.界面主题 = light ? 1 : 2;
            界面主题_v6.刷新主题(true);
            界面主题_v6.应用当前主题到窗体(form);
            var expected = 界面配色_v6.获取选中背景(light).ToArgb();
            Require(text.SelectionColor.ToArgb() == expected, "文本选择未统一");
            Require(combo.SelectionColor.ToArgb() == expected && combo.DropDownSelectedColor.ToArgb() == expected, "下拉选择未统一");
            Require(list.ItemSelectedColor.ToArgb() == expected, "列表选择未统一");
            Require(detail.ItemSelectedBackColor.ToArgb() == expected, "详情行选择未统一");
            Require(nav.TabItemSelectedBackColor.ToArgb() == expected, "导航选择未统一");
            using var dynamicText = new ModernTextBox();
            text.Controls.Add(dynamicText);
            Require(dynamicText.SelectionColor.ToArgb() == expected, "动态编辑控件未应用主题");
        }
    }

    private static void CaptionColors()
    {
        using var window = new ThisIsYourWindow();
        var close = window.CloseButtonHoverBackColor;
        Invoke(typeof(界面主题_v6), "应用对象颜色", window, true);
        Require(window.CaptionButtonHoverGlyphColor.ToArgb() == window.CaptionButtonGlyphColor.ToArgb(), "浅色悬停图标变浅");
        Require(window.CaptionButtonHoverBackColor.GetBrightness() < window.CaptionBackColor.GetBrightness(), "浅色背景没有变深");
        Require(window.CloseButtonHoverBackColor == close && window.CloseButtonHoverGlyphColor.ToArgb() == Color.White.ToArgb(), "关闭按钮红底白字被破坏");
        Invoke(typeof(界面主题_v6), "应用对象颜色", window, false);
        Require(window.CaptionButtonHoverGlyphColor.ToArgb() == Color.White.ToArgb(), "深色悬停图标未恢复");
        Invoke(typeof(界面主题_v6), "应用对象颜色", window, true);
        Require(window.CaptionButtonHoverGlyphColor.ToArgb() == Color.FromArgb(55, 55, 55).ToArgb(), "再次切浅色后丢失设置");
    }

    private static void PopupSelection()
    {
        var type = typeof(ExInputBoxTheme).Assembly.GetType("LakeUI.ExInputBoxForm", true)!;
        foreach (var light in new[] { true, false })
        {
            设置_v6.实例对象.界面主题 = light ? 1 : 2;
            界面主题_v6.刷新主题(true);
            var theme = light ? ExInputBoxTheme.CreateLight() : ExInputBoxTheme.CreateDark();
            using var popup = (Form)Activator.CreateInstance(type, ["测试提示", "测试弹窗", "文本选择", -1, -1, theme, null])!;
            var input = (ModernTextBox)type.GetField("输入框", BindingFlags.Instance | BindingFlags.NonPublic)!.GetValue(popup)!;
            var background = input.BackColor1;
            var foreground = input.ForeColor;
            界面主题_v6.应用当前主题到窗体(popup);
            Require(input.SelectionColor.ToArgb() == 界面配色_v6.获取选中背景(light).ToArgb(), "弹窗选中色没有统一");
            Require(input.BackColor1 == background && input.ForeColor == foreground, "对已配色弹窗进行了二次转换");
        }
    }

    private static void UiRefinements()
    {
        using var quality = new Form_v6_集成工具_质量评测();
        var combo = (VmafModelComboBox)quality.Controls.Find("MCB_模型选择", true).Single();
        var help = (HtmlColorLabel)quality.Controls.Find("HtmlColorLabel9", true).Single();
        Require(combo.MaxDropDownItems == 12, "模型下拉不是十二行");
        Require(combo.ToolTipMaxWidth == 320, "模型提示未按最新要求略微缩窄");
        Require(help.ToolTipText.Contains("HFR、HDR、CUDA"), "模型说明中 HDR 未排在 CUDA 前");
        using var license = new Form_v6_设置_LakeUI许可证();
        var button = (ModernButton)license.Controls.Find("MB_前往购买许可证", true).Single();
        var label = (HtmlColorLabel)license.Controls.Find("HtmlColorLabel3", true).Single();
        foreach (var light in new[] { true, false, true })
        {
            设置_v6.实例对象.界面主题 = light ? 1 : 2;
            界面主题_v6.刷新主题(true);
            界面主题_v6.应用当前主题到窗体(license);
            Require(button.BackColor1.ToArgb() == (light ? Color.FromArgb(226, 218, 250) : Color.DarkSlateBlue).ToArgb(), "许可证按钮未正确应用/恢复主题颜色");
            Require(label.ForeColor.ToArgb() == (light ? Color.FromArgb(128, 99, 180) : Color.MediumSlateBlue).ToArgb(), "许可证编号颜色未正确应用/恢复");
            if (light)
                Require((double)Invoke(typeof(界面主题_v6), "计算对比度", button.ForeColor, button.BackColor1)! >= 4.5, "浅紫按钮文字对比度不足");
        }
    }

    private static VmafModelDisplayItem Parse(string name) =>
        (VmafModelDisplayItem)Invoke(Quality, "解析Vmaf模型显示项", name)!;

    private static List<VmafModelDisplayItem> Models()
    {
        var result = new List<VmafModelDisplayItem>();
        foreach (var prefix in new[] { "vmaf_v1.0.16", "hdr_vmaf_v1.1.8" })
        foreach (var hfr in new[] { "", "_hfr" })
        foreach (var suffix in new[] { "_3d0h", "_5d0h", "_1d5h_2160", "_3d0h_2160" })
            result.Add(Parse(prefix + hfr + suffix));
        result.Add(Parse("vmaf_v0.6.1neg"));
        result.Add(Parse("vmaf_4k_v0.6.1neg"));
        return result;
    }

    private static void ModelParsing()
    {
        var item = Parse("hdr_vmaf_v1.1.8_hfr_1d5h_2160");
        Require(item.IsHdr && item.IsHfr && item.Is4K && item.ViewingDistanceLabel == "1.5H", "HDR 标签解析错误");
        Require(item.DisplayText == "V1 · 4K · 1.5H · HFR · HDR", "胶囊标签顺序错误");
        Require(Parse("vmaf_v0.6.1").IsNonNeg && Parse("vmaf_4k_v0.6.1").IsNonNeg && Parse("vmaf_4k_v0.6.1").Is4K, "非 NEG 旧模型未正确分类");
        foreach (var invalid in new[] { "vmaf_float_v1.0.16_3d0h", "hdr_vmaf_float_v1.1.8_3d0h", "vmaf_b_v1.0.16" })
            Require(Invoke(Quality, "解析Vmaf模型显示项", invalid) == null, "研究模型未过滤：" + invalid);
        var entries = (List<VmafModelDisplayItem>)Invoke(Quality, "构建Vmaf模型显示项", Models().Select(x => x.ModelValue), true)!;
        Require(entries.Where(x => x.UseCuda).All(x => !x.IsHdr && x.VersionLabel == "V0"), "为不兼容的 HDR/V1 开放了 CUDA");
        using var combo = new VmafModelComboBox();
        combo.ReplaceDisplayItems(entries);
        Require(combo.SelectedModel!.IsAuto, "默认项不是 AUTO");
    }

    private static void ModelPresentation()
    {
        var catalog = Catalog(Models().Select(x => x.ModelValue).Concat(["vmaf_v0.6.1", "vmaf_4k_v0.6.1"]));
        var oldNeg = catalog.Where(x => x.VersionLabel == "V0" && !x.IsNonNeg).ToArray();
        Require(oldNeg.Length == 4 && oldNeg.All(x => (string)Invoke(Quality, "获取Vmaf模型说明", x)! == "旧模型，不推荐"), "四项旧 V0/NEG 提示不一致");
        Require(((string)Invoke(Quality, "获取Vmaf模型说明", Parse("hdr_vmaf_v1.1.8_3d0h"))!).StartsWith("标准 3H 观看距离，默认场景，支持 HDR\r\n\r\n", StringComparison.Ordinal), "默认 HDR 简介缺少支持 HDR");
        using var combo = new VmafModelComboBox();
        combo.ReplaceDisplayItems(catalog);
        var headerModelIndex = combo.Models.ToList().FindIndex(x => x.IsSectionHeader);
        var header = headerModelIndex + 1;
        Require(header > 1 && header == combo.Items.Count - 5 && combo.Items[header] == VmafModelDisplayItem.NonNegHeaderText, "警示行不是底部五行的首行");
        var nonNeg = combo.Models.Skip(headerModelIndex + 1).ToArray();
        Require(nonNeg.Length == 4 && nonNeg.All(x => x.IsNonNeg), "非 NEG 选项不是四项");
        Require(nonNeg.Select(x => (x.Is4K, x.UseCuda)).SequenceEqual(new[] { (false, false), (false, true), (true, false), (true, true) }), "非 NEG 普通/CUDA/4K/4K CUDA 顺序错误");
        Require(nonNeg.All(x => x.DisplayText.Contains("非neg模型") && !x.DisplayText.Contains("3H") && !x.DisplayText.Contains("HFR") && !x.DisplayText.Contains("HDR")), "非 NEG 没有合并分类标签");
        var selected = header - 1;
        combo.SelectedIndex = selected;
        var browseCount = 0;
        combo.BrowseRequested += (_, _) => browseCount++;
        combo.SelectedIndex = header;
        Require(combo.SelectedIndex == selected && combo.SelectedModel is { IsSectionHeader: false } && browseCount == 0, "说明行被选为模型或触发了浏览");
        combo.Text = VmafModelDisplayItem.NonNegHeaderText;
        combo.SelectedIndex = header;
        Require(combo.SelectedIndex == selected && combo.SelectedModel is { IsSectionHeader: false }, "文本未变化时说明行绕过了选择保护");
        var wheel = typeof(VmafModelComboBox).GetMethod("OnMouseWheel", BindingFlags.Instance | BindingFlags.NonPublic)!;
        wheel.Invoke(combo, [new MouseEventArgs(MouseButtons.None, 0, 10, 10, -120)]);
        Require(combo.SelectedIndex == header + 1 && combo.SelectedModel!.IsNonNeg, "向下滚轮没有跳过说明行");
        wheel.Invoke(combo, [new MouseEventArgs(MouseButtons.None, 0, 10, 10, 120)]);
        Require(combo.SelectedIndex == selected, "向上滚轮没有跳过说明行");
        combo.SelectedIndex = combo.FindModelIndex("vmaf_4k_v0.6.1", true);
        combo.AddDisplayItem(new VmafModelDisplayItem { IsLocal = true, ModelValue = Path.Combine(Work, "layout-local.json") }, false);
        Require(combo.SelectedModel is { IsNonNeg: true, Is4K: true, UseCuda: true } && combo.Models[^5].IsSectionHeader, "添加本地模型破坏了选中映射或底部非 NEG 分组");
        combo.ReplaceDisplayItems(combo.Models);
        Require(combo.Models.Count(x => x.IsSectionHeader) == 1 && combo.Models.Count(x => x.IsNonNeg) == 4 && combo.SelectedModel!.IsAuto, "刷新丢失模型、产生重复标题或丢失默认 AUTO");
        Throws<InvalidOperationException>(() => Auto(nonNeg, 1920, 1080, 24, false));
        Throws<InvalidOperationException>(() => Auto(nonNeg, 3840, 2160, 24, false));
        var unavailable = (List<VmafModelDisplayItem>)Invoke(Quality, "构建Vmaf模型显示项", null, false)!;
        Require(unavailable.Count(x => x.IsNonNeg) == 4 && unavailable.Where(x => x.IsNonNeg).All(x => !x.IsRuntimeDetected), "非 NEG 目录缺项或把目录项当成已检测");
        foreach (var model in unavailable.Where(x => x.IsNonNeg))
        {
            Throws<InvalidOperationException>(() => Invoke(Quality, "检查Vmaf模型可用性", model));
            if (model.UseCuda)
            {
                Require(!model.IsCudaAvailable, "缺 CUDA 时未记录滤镜不可用");
                model.IsRuntimeDetected = true;
                Throws<InvalidOperationException>(() => Invoke(Quality, "检查Vmaf模型可用性", model));
            }
        }
        var comboType = typeof(VmafModelComboBox);
        var kinds = Enum.GetValues(comboType.GetNestedType("胶囊类别", BindingFlags.NonPublic)!);
        foreach (var light in new[] { true, false })
        {
            var backgrounds = new HashSet<int>();
            foreach (var kind in kinds)
            {
                var colors = ((Color Background, Color Foreground))Invoke(comboType, "获取胶囊配色", kind!, light)!;
                Require(colors.Background.A == 255 && backgrounds.Add(colors.Background.ToArgb()), "胶囊底色未按类别区分");
                Require((double)Invoke(typeof(界面主题_v6), "计算对比度", colors.Foreground, colors.Background)! >= 4.5, "胶囊文字与底色对比度不足");
            }
        }
        var merged = (float)comboType.GetField("非Neg胶囊宽度逻辑值", BindingFlags.Static | BindingFlags.NonPublic)!.GetRawConstantValue()!;
        Require(merged == 160f, "非 NEG 胶囊没有保持三列合并的宽度");
        var capsuleHeight = (float)comboType.GetField("胶囊高度逻辑值", BindingFlags.Static | BindingFlags.NonPublic)!.GetRawConstantValue()!;
        Require(capsuleHeight == 22f, "胶囊高度未轻微缩小");
        using var quality = new Form_v6_集成工具_质量评测();
        var sample = quality.Controls.Find("MCB_SubSample", true).Single();
        var pooling = quality.Controls.Find("MCB_Pooling", true).Single();
        var models = (VmafModelComboBox)quality.Controls.Find("MCB_模型选择", true).Single();
        Require(Math.Abs(sample.Width / (double)pooling.Width - 132.0 / 150) <= 1.0 / pooling.Width, "SubSample 没有按最新要求缩窄");
        Require(models.DropDownItemHeight == 32 && (models.DropDownItemHeight - capsuleHeight) / 2 == 5f, "下拉行上下间距未轻微缩小");
        foreach (var light in new[] { true, false })
        {
            var previousBrightness = -1f;
            foreach (var distance in new[] { "1.5H", "3H", "5H" })
            {
                var kind = Invoke(comboType, "获取观看距离胶囊类别", distance)!;
                var colors = ((Color Background, Color Foreground))Invoke(comboType, "获取胶囊配色", kind, light)!;
                Require(colors.Background.GetBrightness() > previousBrightness, "观看距离没有按 1.5H、3H、5H 由深到浅区分");
                Require(colors.Background.GetHue() is > 260 and < 300, "观看距离没有维持紫色系");
                previousBrightness = colors.Background.GetBrightness();
            }
        }
    }

    private static List<VmafModelDisplayItem> Catalog(IEnumerable<string>? models) =>
        (List<VmafModelDisplayItem>)Invoke(Quality, "构建Vmaf模型显示项", models, true)!;

    private static void ModelCatalog()
    {
        var sdr = Models().Where(x => !x.IsHdr).Select(x => x.ModelValue).ToArray();
        var catalog = Catalog(sdr);
        var hdr = catalog.Where(x => x.IsHdr).ToArray();
        Require(hdr.Length == 8 && hdr.All(x => !x.IsRuntimeDetected && !x.UseCuda), "旧引擎丢失 HDR 目录或把它当成已安装");
        Require(hdr.Count(x => x.IsHfr) == 4 && hdr.Count(x => x.Is4K) == 4, "HDR 普通/HFR/4K 项目不全");
        Require(Catalog(null).Count == 12, "没有 FFmpeg 时仍应显示八项 HDR 与四项非 NEG 目录");
        var detected = hdr[0].ModelValue;
        var refreshed = Catalog(sdr.Concat([detected, detected.ToUpperInvariant()]));
        Require(refreshed.Count(x => x.IsHdr) == 8, "刷新后官方 HDR 项重复");
        Require(refreshed.Single(x => x.ModelValue.Equals(detected, StringComparison.OrdinalIgnoreCase)).IsRuntimeDetected, "未保留实际扫描状态");
        Require(refreshed.Count(x => x.IsHdr && x.IsRuntimeDetected) == 1, "把未检测项也标为已检测");
        using var combo = new VmafModelComboBox();
        combo.ReplaceDisplayItems(catalog);
        Require(combo.SelectedModel!.IsAuto && combo.Models.Count(x => x.IsHdr) == 8, "下拉控件没有保留 HDR 或默认 AUTO");
        var future = Catalog(sdr.Append("hdr_vmaf_v1.1.9_3d0h"));
        Require(future.Any(x => x.ModelValue == "hdr_vmaf_v1.1.9_3d0h" && x.IsRuntimeDetected), "目录覆盖了新版本扫描结果");
    }

    private static void ModelAvailability()
    {
        var catalog = Catalog(Models().Where(x => !x.IsHdr).Select(x => x.ModelValue));
        var fallback = Auto(catalog, 1920, 1080, 60, true);
        Require(!fallback.Model.IsHdr && fallback.Note.Contains("回退 SDR"), "AUTO 使用了未检测到的 HDR 目录项");
        var missing = catalog.First(x => x.IsHdr);
        foreach (var detected in new[] { false, true })
        foreach (var model in catalog.Where(x => x.IsHdr))
        {
            var item = Parse(model.ModelValue);
            item.IsRuntimeDetected = detected;
            var description = (string)Invoke(Quality, "获取Vmaf模型说明", item)!;
            var sdr = Parse(item.ModelValue.Replace("hdr_vmaf_v1.1.8", "vmaf_v1.0.16", StringComparison.Ordinal));
            var sdrDescription = (string)Invoke(Quality, "获取Vmaf模型说明", sdr)!;
            var terms = sdrDescription.IndexOf("\r\n\r\n", StringComparison.Ordinal);
            Require(terms > 0 && description == sdrDescription.Insert(terms, "，支持 HDR") && description.Contains("术语解释：HFR") && description.Contains("H = 观看距离"), "HDR 简介未在分类说明后补充支持 HDR，或术语内容被改变");
            Require(!description.Contains(item.ModelValue) && !description.Contains("FFmpeg") && !description.Contains("AUTO") && !description.Contains("zimg") && !description.Contains("未检测到") && !description.Contains("官方模型目录"), "HDR 提示仍包含模型状态或引擎要求");
        }
        Throws<InvalidOperationException>(() => Invoke(Quality, "检查Vmaf模型可用性", missing));
        Invoke(Quality, "检查Vmaf模型可用性", Parse(missing.ModelValue));
        Invoke(Quality, "检查Vmaf模型可用性", new VmafModelDisplayItem { IsLocal = true, IsHdr = true, IsRuntimeDetected = false });
        var refreshed = Catalog(Models().Select(x => x.ModelValue));
        Require(Auto(refreshed, 3840, 2160, 60, true).Model.IsHdr, "实际检测到 HDR 后 AUTO 没有恢复 HDR 优先");
        Throws<InvalidOperationException>(() => Auto(Catalog(null), 1920, 1080, 24, true));
        var stale = Parse("hdr_vmaf_v1.1.9_3d0h");
        Invoke(Quality, "保留本地Vmaf模型", catalog, stale);
        Require(!catalog.Any(x => x.ModelValue == stale.ModelValue), "更换引擎后保留了已消失模型的旧检测状态");
        var local = new VmafModelDisplayItem { ModelValue = Path.Combine(Work, "manual.json"), IsLocal = true };
        Invoke(Quality, "保留本地Vmaf模型", catalog, local);
        Invoke(Quality, "保留本地Vmaf模型", catalog, local);
        Require(catalog.Count(x => x.IsLocal) == 1, "刷新没有保留本地模型或重复追加");
    }

    private static (VmafModelDisplayItem Model, string Note) Auto(IEnumerable<VmafModelDisplayItem> items, int w, int h, double fps, bool hdr)
    {
        object?[] args = [items, w, h, fps, "", hdr];
        var model = (VmafModelDisplayItem)Invoke(Quality, "解析AutoVmaf模型", args)!;
        return (model, (string)args[4]!);
    }

    private static void AutoModels()
    {
        var items = Models();
        foreach (var hdr in new[] { false, true })
        foreach (var is4k in new[] { false, true })
        foreach (var fps in new[] { 24.0, 48, 60, 120 })
        {
            var value = Auto(items, is4k ? 3840 : 1920, is4k ? 1608 : 1080, fps, hdr);
            Require(value.Model.IsHdr == hdr && value.Model.Is4K == is4k && value.Model.IsHfr == (fps >= 48), "AUTO 分类错误");
            Require(value.Model.ViewingDistanceLabel == (is4k ? "1.5H" : "3H"), "观看距离选择错误");
            if (fps > 60) Require(value.Note.Contains("校准区间"), "缺少超高帧率提示");
        }
        var fallback = Auto(items.Where(x => !x.IsHdr), 1920, 1080, 60, true);
        Require(!fallback.Model.IsHdr && fallback.Note.Contains("回退 SDR"), "缺少 HDR 时没有明确回退");
        var normal = Auto(items.Where(x => !x.IsHfr), 1920, 1080, 60, true);
        Require(normal.Model.IsHdr && !normal.Model.IsHfr && normal.Note.Contains("普通模型"), "HDR 标准帧率回退错误");
        var neg = Auto(items.Where(x => x.VersionLabel == "V0"), 3840, 2160, 24, true);
        Require(neg.Note.Contains("V0 NEG") && neg.Note.Contains("回退 SDR"), "NEG 回退没有说明");
        Require(!Auto(items, 1920, 1080, double.NaN, true).Model.IsHfr, "未知帧率不应选 HFR");
        Throws<InvalidOperationException>(() => Auto(items, 0, 1080, 24, false));
        Throws<InvalidOperationException>(() => Auto([], 1920, 1080, 24, false));
    }

    private static object VideoInfo(string transfer)
    {
        var type = Quality.GetNestedType("视频流信息", BindingFlags.NonPublic)!;
        var info = Activator.CreateInstance(type, true)!;
        foreach (var pair in new Dictionary<string, string> { ["色彩传递"] = transfer, ["色彩范围"] = "tv", ["色彩原色"] = "bt2020", ["色彩矩阵"] = "bt2020nc" })
            type.GetProperty(pair.Key)!.SetValue(info, pair.Value);
        return info;
    }

    private static void HdrMetadata()
    {
        foreach (var trc in new[] { "smpte2084", "arib-std-b67" })
            Require((bool)Invoke(Quality, "是Hdr视频", VideoInfo(trc))!, "未识别 HDR 传递函数");
        Require(!(bool)Invoke(Quality, "是Hdr视频", VideoInfo("bt709"))!, "仅凭 BT.2020 误判 HDR");
        Require(((string)Invoke(Quality, "构建Hdr色彩标记", VideoInfo("smpte2084"))!).Contains("range=limited"), "色彩范围映射错误");
        Throws<InvalidOperationException>(() => Invoke(Quality, "构建Hdr色彩标记", VideoInfo("")));
    }

    private static void BinaryModelScan()
    {
        var folder = Path.Combine(Work, "binary-scan");
        Directory.CreateDirectory(folder);
        var file = Path.Combine(folder, "ffmpeg-fixture.exe");
        File.WriteAllText(file, "\0hdr_vmaf_v1.1.8_hfr_1d5h_2160\0vmaf_v1.0.16_3d0h\0", Encoding.ASCII);
        var models = (List<string>)Invoke(Quality, "从FFmpeg运行库提取Vmaf模型列表", file)!;
        Require(models.Contains("hdr_vmaf_v1.1.8_hfr_1d5h_2160"), "HDR 前缀被扫描器丢弃");
    }
}
