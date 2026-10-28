using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using FFmpegFreeUI;
using LakeUI;

internal static class Program
{
    private static readonly List<object> Results = [];
    private static readonly Type Quality = typeof(Form_v6_集成工具_质量评测);
    private static readonly JsonSerializerOptions Json = (JsonSerializerOptions)typeof(Agent文件管理_v6).Assembly
        .GetType("FFmpegFreeUI.Module1", true)!.GetField("JsonSO", BindingFlags.Public | BindingFlags.Static)!.GetValue(null)!;
    private static string Root = "";
    private static string Work = "";
    private static int Failed;

    [STAThread]
    private static int Main(string[] args)
    {
        if (args.Length == 2 && args[0] == "--install-agent")
        {
            Agent文件管理_v6.安装内置文件(Path.GetFullPath(args[1]));
            Console.WriteLine("已安装缺失的 Agent 默认文件，现有文件保持不变。");
            return 0;
        }
        if (args.Length >= 2 && args[0] == "--preview-license")
            return LicensePreview.Run(args[1]);
        if (args.Length >= 3 && args[0] == "--preview-vmaf")
            return VmafPreview.Run(args[1], args[2], args.Length > 3 && args[3] == "dark");
        Root = Path.GetFullPath(args.Length > 0 ? args[0] : ".");
        Work = Path.Combine(Root, "artifacts", "ui-vmaf-agent-20261028", "tests", DateTime.Now.ToString("yyyyMMdd-HHmmss-fff"));
        Directory.CreateDirectory(Work);
        var previous = 设置_v6.实例对象;
        设置_v6.实例对象 = new 设置_v6();
        try
        {
            Check("主题：文字、下拉、列表和导航使用同一选中色", ThemeRoundTrip);
            Check("标题栏：浅色保持深色图标、背景变深且保留红色关闭", CaptionColors);
            Check("弹窗：统一输入选中色且不二次转换浅色背景", PopupSelection);
            Check("界面：HDR 提示、十二行下拉与许可证浅紫色", UiRefinements);
            Check("技能：明确全 CUDA 时必须使用可用 CUDA 滤镜", CudaFilterSkill);
            Check("模型：HDR、HFR、分辨率、距离与 CUDA 分类", ModelParsing);
            Check("HDR 目录：旧引擎仍显示八项且不伪装为已检测", ModelCatalog);
            Check("HDR 目录：未检测项不进入 AUTO，手动评测前明确提示", ModelAvailability);
            Check("AUTO：HDR/SDR、1080/4K、24/48/60/120 fps 与缺模型回退", AutoModels);
            Check("HDR：PQ/HLG 判定与缺失颜色标记拒绝", HdrMetadata);
            Check("模型扫描：识别 hdr_vmaf 前缀", BinaryModelScan);
            Check("Agent：嵌入文件首装、修改保留、缺失补全", AgentInstallation);
            Check("Agent：自定义技能发现、热读取与旧别名", AgentSkills);
            Check("Agent：路径穿越、绝对路径与大文件边界", AgentPaths);
            Check("Agent：目录链接不能逃逸", AgentLinks);
            Check("系统提示词：读取用户文件并追加真实权限/联网状态", AgentPrompt);
            Check("Agent：内置技能不分发 VPY 脚本或参考", NoVpySkills);
            Check("七份预设：JSON 和应用生成的命令一致", PresetCommands);
            Check("AVIF：按 CPU 并行且不修改全局设置或放大视频并发", AvifParallelism);
            Check("AVIF：工具定义与权限检查", AvifPermissions);
            if (args.Length > 1) Check("真实队列：三张 AVIF 并行完成并清理临时上限", () => AvifQueueSmoke(args[1]));
        }
        finally
        {
            设置_v6.实例对象 = previous;
            File.WriteAllText(Path.Combine(Work, "results.json"), JsonSerializer.Serialize(new
            {
                time = DateTimeOffset.Now,
                assembly = typeof(Agent文件管理_v6).Assembly.Location,
                processor_count = Environment.ProcessorCount,
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
        catch (SkipException ex)
        {
            Results.Add(new { name, status = "skipped", reason = ex.Message });
            Console.WriteLine("未执行：" + name + "；" + ex.Message);
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

    private static void CudaFilterSkill()
    {
        var folder = CaseRoot("cuda-skill");
        Agent文件管理_v6.安装内置文件(folder);
        var entry = Agent技能资料库_v6.读取资料("nvidia-video", "", folder);
        var reference = Agent技能资料库_v6.读取资料("nvidia-video", "cuda-filters", folder);
        Require(entry.Contains("必须使用 CUDA 滤镜") && entry.Contains("不静默降级") && entry.Contains("references/cuda-filters.md"), "缺少强制全 CUDA 规则或资料入口");
        foreach (var name in new[] { "bilateral_cuda", "bwdif_cuda", "chromakey_cuda", "colorspace_cuda", "hwupload_cuda", "libvmaf_cuda", "overlay_cuda", "pad_cuda", "scale_cuda", "thumbnail_cuda", "transpose_cuda", "yadif_cuda" })
            Require(reference.Contains(name), "CUDA 参考漏项：" + name);
        Require(reference.Contains("dir=clock") && reference.Contains("dir=cclock") && reference.Contains("range=tv") && reference.Contains("不代表"), "CUDA 示例或能力边界缺失");
        Require(reference.Contains("hwdownload") && reference.Contains("hwmap") && reference.Contains("scale_npp"), "传输/映射/NPP 边界缺失");
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
        foreach (var invalid in new[] { "vmaf_v0.6.1", "vmaf_float_v1.0.16_3d0h", "hdr_vmaf_float_v1.1.8_3d0h", "vmaf_b_v1.0.16" })
            Require(Invoke(Quality, "解析Vmaf模型显示项", invalid) == null, "研究模型未过滤：" + invalid);
        var entries = (List<VmafModelDisplayItem>)Invoke(Quality, "构建Vmaf模型显示项", Models().Select(x => x.ModelValue), true)!;
        Require(entries.Where(x => x.UseCuda).All(x => !x.IsHdr && x.VersionLabel == "V0"), "为不兼容的 HDR/V1 开放了 CUDA");
        using var combo = new VmafModelComboBox();
        combo.ReplaceDisplayItems(entries);
        Require(combo.SelectedModel!.IsAuto, "默认项不是 AUTO");
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
        Require(Catalog(null).Count == 8, "没有 FFmpeg 时初始目录仍应可见");
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
        var description = (string)Invoke(Quality, "获取Vmaf模型说明", missing)!;
        Require(description.Contains("未检测到") && description.Contains("官方模型目录"), "缺少模型来源和当前引擎状态说明");
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
        var missing = VideoInfo("");
        Throws<InvalidOperationException>(() => Invoke(Quality, "构建Hdr色彩标记", missing));
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

    private static string CaseRoot(string name)
    {
        var folder = Path.Combine(Work, name);
        Directory.CreateDirectory(folder);
        return folder;
    }

    private static void AgentInstallation()
    {
        var folder = CaseRoot("agent-install");
        Agent文件管理_v6.安装内置文件(folder);
        var agent = Path.Combine(folder, "Agent");
        Require(File.Exists(Path.Combine(agent, "agents.md")), "未安装 agents.md");
        Require(Directory.GetFiles(agent, "SKILL.md", SearchOption.AllDirectories).Length == 4, "技能数量不正确");
        var prompt = Path.Combine(agent, "agents.md");
        var skill = Path.Combine(agent, "skills", "avif-aom", "SKILL.md");
        File.WriteAllText(prompt, "用户系统提示词", Encoding.UTF8);
        File.WriteAllText(skill, "用户技能", Encoding.UTF8);
        var missing = Path.Combine(agent, "skills", "audio-nmr-aac", "references", "NMR-AAC.md");
        File.Delete(missing);
        Parallel.For(0, 4, _ => Agent文件管理_v6.安装内置文件(folder));
        Require(File.ReadAllText(prompt) == "用户系统提示词" && File.ReadAllText(skill) == "用户技能", "安装覆盖用户修改");
        Require(File.Exists(missing) && Directory.GetFiles(agent, "*.tmp", SearchOption.AllDirectories).Length == 0, "缺失补全或临时文件清理失败");
    }

    private static void AgentSkills()
    {
        var folder = CaseRoot("agent-skills");
        Agent文件管理_v6.安装内置文件(folder);
        var custom = Path.Combine(folder, "Agent", "skills", "custom-test");
        Directory.CreateDirectory(Path.Combine(custom, "references"));
        File.WriteAllText(Path.Combine(custom, "SKILL.md"), "---\nname: custom-test\ndescription: 用户自定义\n---\n测试", Encoding.UTF8);
        var reference = Path.Combine(custom, "references", "live.md");
        File.WriteAllText(reference, "初稿", Encoding.UTF8);
        using var index = JsonDocument.Parse(Agent技能资料库_v6.列出技能(folder));
        Require(index.RootElement.GetProperty("skills").EnumerateArray().Any(x => x.GetProperty("name").GetString() == "custom-test"), "未发现新增技能");
        Require(Agent技能资料库_v6.读取资料("custom-test", "references/live.md", folder) == "初稿", "未读取自定义引用");
        File.WriteAllText(reference, "修改后", Encoding.UTF8);
        Require(Agent技能资料库_v6.读取资料("custom-test", "references/live.md", folder) == "修改后", "修改没有热生效");
        Require(Agent技能资料库_v6.读取资料("3fui", "tools", folder).Contains("Agent 工具说明"), "旧 skill 别名失效");
    }

    private static void AgentPaths()
    {
        var folder = CaseRoot("agent-paths");
        foreach (var invalid in new[] { "../agents.md", "references/../../agents.md", "C:/Windows/win.ini", "/agents.md", "references/..\\agents.md", "references/.. /agents.md", "references/NUL.md", "references/COM1.md", "references/LPT¹.md", "references/name./other.md" })
            Require(Agent技能资料库_v6.读取资料("ffmpegfreeui", invalid, folder).StartsWith("无法读取"), "路径没有拒绝：" + invalid);
        Require(Agent技能资料库_v6.读取资料("../outside", "SKILL.md", folder).StartsWith("无法读取"), "skill 名称可以越界");
        var big = Path.Combine(folder, "Agent", "skills", "ffmpegfreeui", "references", "big.md");
        File.WriteAllText(big, new string('x', 1024 * 1024 + 1), Encoding.UTF8);
        Require(Agent技能资料库_v6.读取资料("ffmpegfreeui", "references/big.md", folder).Contains("超过"), "未限制读取长度");
    }

    private static void AgentLinks()
    {
        var folder = CaseRoot("agent-links");
        Agent文件管理_v6.安装内置文件(folder);
        var outside = CaseRoot("outside-link-target");
        File.WriteAllText(Path.Combine(outside, "secret.md"), "不能读取", Encoding.UTF8);
        var link = Path.Combine(folder, "Agent", "skills", "ffmpegfreeui", "references", "escape");
        try { Directory.CreateSymbolicLink(link, outside); }
        catch (Exception ex) when (ex is UnauthorizedAccessException or IOException) { throw new SkipException("系统未授权创建测试链接：" + ex.Message); }
        Require(Agent技能资料库_v6.读取资料("ffmpegfreeui", "references/escape/secret.md", folder).Contains("不允许"), "链接逃逸未拒绝");
    }

    private static void AgentPrompt()
    {
        Agent文件管理_v6.安装内置文件();
        var file = Path.Combine(Agent文件管理_v6.获取Agent目录(), "agents.md");
        var original = File.ReadAllText(file, Encoding.UTF8);
        try
        {
            File.WriteAllText(file, "只使用用户修改的提示词。", Encoding.UTF8);
            var text = Agent提示词_v6.构建系统提示词(AgentNetworkMode.Disabled, "安全区域");
            Require(text.StartsWith("只使用用户修改的提示词。") && text.Contains("禁用联网") && text.Contains("安全区域"), "未使用修改后的提示词与真实状态");
            Require(text.Contains("nvidia-video") && text.Contains("avif-aom") && text.Contains("audio-nmr-aac"), "动态技能索引不完整");
        }
        finally { File.WriteAllText(file, original, Encoding.UTF8); }
    }

    private static 预设数据_v6 Preset(string skill, string name)
    {
        var file = Path.Combine(Root, "FFmpegFreeUI", "AgentDefaults", "skills", skill, "references", name + ".md");
        var text = File.ReadAllText(file, Encoding.UTF8);
        var match = Regex.Match(text, "```json\\s*(?<json>.*?)\\s*```", RegexOptions.Singleline);
        Require(match.Success, "缺少完整 JSON：" + name);
        return JsonSerializer.Deserialize<预设数据_v6>(match.Groups["json"].Value, Json)!;
    }

    private static void NoVpySkills()
    {
        var folder = CaseRoot("agent-no-vpy");
        Agent文件管理_v6.安装内置文件(folder);
        var files = Directory.GetFiles(Path.Combine(folder, "Agent"), "*", SearchOption.AllDirectories);
        Require(files.Length == 25, "无 VPY 且新增 CUDA 参考后的默认文件数量不正确");
        foreach (var file in files)
            Require(!Regex.IsMatch(Path.GetFileName(file) + "\n" + File.ReadAllText(file, Encoding.UTF8), @"vpy|vspipe|VapourSynth|BM3D", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant), "内置技能仍含脚本或参考：" + file);
        Require(Agent技能资料库_v6.读取资料("nvidia-video", "NVENC-AV1-BM3D-VPY-Deleted", folder).StartsWith("无法读取"), "已删除的预设仍能被读取");
    }

    private static void PresetCommands()
    {
        var commands = new Dictionary<string, string>();
        foreach (var name in new[] { "NVENC-AV1", "NVENC-AV1-Fast", "NVENC-HEVC", "NVENC-HEVC-Fast", "NVENC-HEVC-LowRate", "AOM-AVIF", "NMR-AAC" })
        {
            var skill = name.StartsWith("NVENC") ? "nvidia-video" : name == "AOM-AVIF" ? "avif-aom" : "audio-nmr-aac";
            var preset = Preset(skill, name);
            var command = 预设管理_v6.生成命令行展示文本(preset, 预设管理_v6.输入占位符, 预设管理_v6.输出占位符);
            commands[name] = command;
            if (skill == "nvidia-video")
            {
                Require(command.Contains(name.Contains("HEVC") ? "hevc_nvenc" : "av1_nvenc") && command.Contains("libopus") && command.Contains("96k"), "视频预设编码器或音频参数丢失");
                Require(command.Contains(name.Contains("HEVC") ? "30" : "36"), "参考 CQ 值丢失");
            }
            if (name == "AOM-AVIF") Require(command.Contains("libaom-av1") && command.Contains("tune=iq") && command.Contains("-filter_threads 1"), "AVIF 参考参数丢失");
            if (name == "NMR-AAC") Require(command.Contains("-q:a:0 1.3") && command.Contains("attached_pic?") && command.Contains("-c:v:0 copy"), "音频参考映射或质量值丢失");
        }
        File.WriteAllText(Path.Combine(Work, "preset-commands.json"), JsonSerializer.Serialize(commands, Json), new UTF8Encoding(false));
    }

    private static void AvifParallelism()
    {
        var queueType = typeof(编码队列_v6);
        var tasks = (List<编码任务_v6>)queueType.GetField("队列项目", BindingFlags.Static | BindingFlags.NonPublic)!.GetValue(null)!;
        var limitField = queueType.GetField("AVIF批次并行上限", BindingFlags.Static | BindingFlags.NonPublic)!;
        Require(tasks.Count == 0, "测试必须运行在空的隔离队列");
        var setting = 设置_v6.实例对象.自动同时运行任务数量选项;
        try
        {
            var limit = 编码队列_v6.配置AVIF批次并行(1000);
            Require(limit == Environment.ProcessorCount, "未按可用 CPU 配置");
            tasks.Add(new 编码任务_v6 { 预设数据 = Preset("avif-aom", "AOM-AVIF"), 允许自动启动 = true });
            Require((int)Invoke(queueType, "获取并发上限")! == limit, "AVIF 临时上限未接入调度");
            Require(设置_v6.实例对象.自动同时运行任务数量选项 == setting, "修改了用户全局设置");
            var video = new 编码任务_v6 { 预设数据 = Preset("nvidia-video", "NVENC-AV1"), 允许自动启动 = true };
            tasks.Add(video);
            Require((int)Invoke(queueType, "获取并发上限")! == Math.Clamp(setting + 1, 1, 10), "视频并发被 AVIF 放大");
            Throws<InvalidOperationException>(() => 编码队列_v6.配置AVIF批次并行(20));
            tasks.Remove(video);
            Require(编码队列_v6.配置AVIF批次并行(3, 2) == Math.Min(Environment.ProcessorCount, 2), "资源限制未生效");
            Throws<ArgumentException>(() => 编码队列_v6.配置AVIF批次并行(0));
        }
        finally { tasks.Clear(); limitField.SetValue(null, 0); }
    }

    private static void AvifPermissions()
    {
        var safe = JsonSerializer.Serialize(AgentLocalTools.BuildToolDefinitions(0, 0));
        var environment = JsonSerializer.Serialize(AgentLocalTools.BuildToolDefinitions(1, 0));
        Require(!safe.Contains("configure_avif_parallelism") && environment.Contains("configure_avif_parallelism"), "工具定义权限错误");
        var call = new AgentToolCallInfo { Name = "configure_avif_parallelism", Arguments = "{\"file_count\":2}" };
        var denied = AgentLocalTools.ExecuteAsync(call, 0, 0, null!, "", "").GetAwaiter().GetResult();
        Require(denied.Contains("权限不足"), "执行未校验权限");
    }

    private static void AvifQueueSmoke(string ffmpeg)
    {
        Require(File.Exists(ffmpeg), "需要传入可用 FFmpeg 的绝对路径");
        var folder = CaseRoot("real-avif-queue");
        var preset = Preset("avif-aom", "AOM-AVIF");
        preset.输出位置 = folder;
        var inputs = new List<string>();
        for (var i = 0; i < 3; i++)
        {
            var path = Path.Combine(folder, $"input-{i}.png");
            using var image = new Bitmap(384, 384);
            using var graphics = Graphics.FromImage(image);
            graphics.Clear(Color.FromArgb(30 + i * 40, 80, 150));
            graphics.FillEllipse(Brushes.White, 40, 50, 220, 180);
            image.Save(path, System.Drawing.Imaging.ImageFormat.Png);
            inputs.Add(path);
        }
        var oldFfmpeg = 设置_v6.实例对象.替代进程文件名;
        var oldAuto = 设置_v6.实例对象.自动开始任务选项;
        var oldSound = 设置_v6.实例对象.提示音选项;
        var oldLimit = 设置_v6.实例对象.自动同时运行任务数量选项;
        var observations = new List<object>();
        var maxProcesses = 0;
        var gate = new object();
        List<编码任务_v6> tasks = [];
        using var cancellation = new System.Threading.CancellationTokenSource();
        Task monitor = Task.CompletedTask;
        try
        {
            设置_v6.实例对象.替代进程文件名 = ffmpeg;
            设置_v6.实例对象.自动开始任务选项 = 0;
            设置_v6.实例对象.提示音选项 = 1;
            var limit = 编码队列_v6.配置AVIF批次并行(inputs.Count);
            tasks = 编码队列_v6.批量添加预设任务(inputs, preset);
            monitor = Task.Run(async () =>
            {
                while (!cancellation.IsCancellationRequested)
                {
                    var pids = tasks.Select(x => x.当前进程ID).Where(x => x > 0).ToArray();
                    lock (gate)
                    {
                        if (pids.Length > maxProcesses)
                        {
                            maxProcesses = pids.Length;
                            observations.Add(new { time = DateTimeOffset.Now, process_ids = pids });
                        }
                    }
                    await Task.Delay(10, cancellation.Token);
                }
            });
            Require(System.Threading.SpinWait.SpinUntil(() => tasks.All(x => !x.正在执行 && (x.状态 == 编码任务状态_v6.已完成 || x.状态 == 编码任务状态_v6.错误)), TimeSpan.FromSeconds(90)), "真实 AVIF 队列超时");
            var errors = tasks.Where(x => x.状态 != 编码任务状态_v6.已完成).SelectMany(x => x.错误列表).ToArray();
            Require(tasks.All(x => x.状态 == 编码任务状态_v6.已完成 && File.Exists(x.输出文件)), "真实 AVIF 任务失败：" + string.Join("；", errors));
            Require(maxProcesses >= Math.Min(limit, 2), "未观察到预期的实际编码进程并行");
            Require(设置_v6.实例对象.自动同时运行任务数量选项 == oldLimit, "批次修改了全局并发设置");
            var temporary = typeof(编码队列_v6).GetField("AVIF批次并行上限", BindingFlags.Static | BindingFlags.NonPublic)!;
            Require((int)temporary.GetValue(null)! == 0, "批次结束后没有清理临时并行上限");
        }
        finally
        {
            cancellation.Cancel();
            try { monitor.GetAwaiter().GetResult(); } catch (OperationCanceledException) { }
            foreach (var task in tasks.Where(x => x.正在执行)) task.停止();
            File.WriteAllText(Path.Combine(folder, "queue-smoke.json"), JsonSerializer.Serialize(new
            {
                ffmpeg,
                max_concurrent_processes = maxProcesses,
                observations,
                tasks = tasks.Select(x => new { x.ID, state = x.状态.ToString(), x.输入文件, x.输出文件, steps = x.步骤.Select(step => new { step.显示名称, step.实际执行文件名, step.实际执行参数, step.命令行 }), errors = x.错误列表 })
            }, Json), new UTF8Encoding(false));
            编码队列_v6.移除任务(tasks.Where(x => !x.正在执行).Select(x => x.ID));
            设置_v6.实例对象.替代进程文件名 = oldFfmpeg;
            设置_v6.实例对象.自动开始任务选项 = oldAuto;
            设置_v6.实例对象.提示音选项 = oldSound;
        }
    }

    private sealed class SkipException(string message) : Exception(message) { }
}
