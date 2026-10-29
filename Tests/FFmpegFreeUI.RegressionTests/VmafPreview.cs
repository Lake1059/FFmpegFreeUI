using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;
using FFmpegFreeUI;

internal static class VmafPreview
{
    [DllImport("user32.dll")]
    private static extern bool PrintWindow(IntPtr hwnd, IntPtr dc, uint flags);

    internal static void Capture(Form form, string path, Control? region = null)
    {
        using var bitmap = new Bitmap(form.Width, form.Height);
        using var graphics = Graphics.FromImage(bitmap);
        var dc = graphics.GetHdc();
        try
        {
            if (!PrintWindow(form.Handle, dc, 2)) throw new InvalidOperationException("无法捕获本次预览窗口。");
        }
        finally { graphics.ReleaseHdc(dc); }
        if (region is null)
            bitmap.Save(path, System.Drawing.Imaging.ImageFormat.Png);
        else
        {
            var location = region.PointToScreen(Point.Empty);
            var rect = new Rectangle(location.X - form.Left, location.Y - form.Top, region.Width, region.Height);
            using var cropped = bitmap.Clone(rect, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            cropped.Save(path, System.Drawing.Imaging.ImageFormat.Png);
        }
    }

    private static void CapturePopupStack(Form host, Form popup, Form tip, string path)
    {
        using var canvas = new Bitmap(host.Width, host.Height);
        using var destination = Graphics.FromImage(canvas);
        // 只组合本次隔离窗口，不抓取桌面或其他程序。
        foreach (var form in new[] { host, popup, tip })
        {
            using var bitmap = new Bitmap(form.Width, form.Height);
            using var graphics = Graphics.FromImage(bitmap);
            var dc = graphics.GetHdc();
            try
            {
                if (!PrintWindow(form.Handle, dc, 2)) throw new InvalidOperationException("无法捕获本次弹层。");
            }
            finally { graphics.ReleaseHdc(dc); }
            destination.DrawImageUnscaled(bitmap, form.Left - host.Left, form.Top - host.Top);
        }
        canvas.Save(path, System.Drawing.Imaging.ImageFormat.Png);
    }

    private static void HoverModel(VmafModelComboBox combo, Form popup, int index)
    {
        var flags = BindingFlags.Instance | BindingFlags.NonPublic;
        // 仅设置滚动起点；提示经真实 MouseMove 事件生成，不直接调用提示更新方法。
        popup.GetType().GetField("_scrollOffset", flags)!.SetValue(popup, Math.Clamp(index - 2, 0, Math.Max(0, combo.Items.Count - combo.MaxDropDownItems)));
        var rect = (RectangleF)popup.GetType().GetMethod("GetItemRect", flags)!.Invoke(popup, [index])!;
        var move = popup.GetType().GetMethod("OnMouseMove", flags)!;
        move.Invoke(popup, [new MouseEventArgs(MouseButtons.None, 0, 10, -1, 0)]);
        move.Invoke(popup, [new MouseEventArgs(MouseButtons.None, 0, 10, (int)(rect.Top + rect.Height / 2), 0)]);
    }

    private static Form CheckTip(Form popup, int expectedWidth)
    {
        var flags = BindingFlags.Instance | BindingFlags.NonPublic;
        var tip = (Form?)popup.GetType().GetField("_tipForm", flags)!.GetValue(popup);
        if (tip is null || tip.IsDisposed || !tip.Visible) throw new InvalidOperationException("模型提示未显示。");
        if (tip.Width != expectedWidth || tip.MinimumSize.Width != expectedWidth || tip.MaximumSize.Width != expectedWidth)
            throw new InvalidOperationException($"提示实际宽度不是固定的 {expectedWidth}px：{tip.Width}px。");
        var textRect = (RectangleF)tip.GetType().GetMethod("GetTextRectangle", flags)!.Invoke(tip, null)!;
        using var format = (Vortice.DirectWrite.IDWriteTextFormat)tip.GetType().GetMethod("CreateTipTextFormat", flags)!.Invoke(tip, null)!;
        var factory = (Vortice.DirectWrite.IDWriteFactory)typeof(LakeUI.FloatingToolTipForm).Assembly.GetType("LakeUI.D3D_D2DInterop")!
            .GetMethod("GetDWriteFactory", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)!.Invoke(null, null)!;
        var text = (string)tip.GetType().GetField("_tipText", flags)!.GetValue(tip)!;
        using var layout = factory.CreateTextLayout(text, format, textRect.Width, 1000000);
        // Width 不计换行处的尾随空格，与可见文字一致。
        var measured = layout.Metrics;
        if (measured.Width > textRect.Width + 1 || measured.Height > textRect.Height + 1)
            throw new InvalidOperationException($"提示文字超出实际内容区域：测量={measured.Width}×{measured.Height}，文本区域={textRect}，窗体={tip.Size}，DPI={tip.DeviceDpi}。");
        var owner = (Control)popup.GetType().GetField("_owner", flags)!.GetValue(popup)!;
        var area = Screen.FromControl(owner).WorkingArea;
        if (owner.TopLevelControl is Form host)
        {
            var visible = Rectangle.Intersect(area, host.RectangleToScreen(host.ClientRectangle));
            if (visible.Width >= tip.Width && visible.Height >= tip.Height) area = visible;
        }
        if (!area.Contains(tip.Bounds))
            throw new InvalidOperationException($"模型提示越过可见边界：提示={tip.Bounds}，边界={area}。");
        return tip;
    }

    private static async Task CheckCapsuleRefinements(Form window, VmafModelComboBox combo, string output, bool dark)
    {
        var scale = combo.DeviceDpi / 96.0;
        var sample = window.Controls.Find("MCB_SubSample", true).Single();
        var pooling = window.Controls.Find("MCB_Pooling", true).Single();
        var sampleLabel = window.Controls.Find("HtmlColorLabel7", true).Single();
        if (Math.Abs(sample.Width - 132 * scale) > 1 || Math.Abs(pooling.Width - 150 * scale) > 1)
            throw new InvalidOperationException("SubSample 或 Pooling 的实际宽度不符。");
        if (sample.PointToScreen(Point.Empty).X != sampleLabel.PointToScreen(Point.Empty).X)
            throw new InvalidOperationException("SubSample 标题与输入框没有保持对齐。");
        combo.SelectedIndex = combo.FindModelIndex("vmaf_4k_v0.6.1", true);
        if (combo.SelectedModel is not { IsNonNeg: true, Is4K: true, UseCuda: true })
            throw new InvalidOperationException("未选中最后一个非 NEG 4K CUDA 模型。");
        var selections = new List<object>();
        var green = ColorTranslator.FromHtml(dark ? "#344A2A" : "#E3EFD9");
        foreach (var logicalWidth in new[] { 370, 373, 405 })
        {
            var width = (int)Math.Round(logicalWidth * scale, MidpointRounding.AwayFromZero);
            window.Width += width - combo.Width;
            window.PerformLayout();
            await Task.Delay(160);
            if (Math.Abs(combo.Width - width) > 1) throw new InvalidOperationException("模型框未按实际窗口布局调整宽度。");
            var file = "selected-4k-nonneg-cuda-" + logicalWidth + ".png";
            Capture(window, Path.Combine(output, file), combo);
            using var image = new Bitmap(Path.Combine(output, file));
            var greenPixels = 0;
            for (var y = 0; y < image.Height; y++)
            for (var x = 0; x < image.Width; x++)
            {
                var pixel = image.GetPixel(x, y);
                if (Math.Abs(pixel.R - green.R) <= 2 && Math.Abs(pixel.G - green.G) <= 2 && Math.Abs(pixel.B - green.B) <= 2) greenPixels++;
            }
            if (greenPixels < 100) throw new InvalidOperationException($"模型框 {combo.Width}px 时实际截图中 CUDA 胶囊缺失。");
            selections.Add(new { logical_width = logicalWidth, width = combo.Width, height = combo.Height, cuda_background_pixels = greenPixels, screenshot = file });
        }
        combo.SelectedIndex = combo.FindModelIndex("hdr_vmaf_v1.1.8_3d0h", false);
        combo.DroppedDown = true;
        await Task.Delay(300);
        var popup = (Form?)typeof(LakeUI.ModernComboBox).GetField("_dropDownForm", BindingFlags.Instance | BindingFlags.NonPublic)!.GetValue(combo);
        if (popup is null || !popup.Visible || combo.MaxDropDownItems != 12 || combo.DropDownItemHeight != 32)
            throw new InvalidOperationException("缩小行距后的下拉没有正确显示。");
        Capture(popup, Path.Combine(output, "distance-purples.png"));
        var comboType = typeof(VmafModelComboBox);
        var palette = new List<object>();
        foreach (var distance in new[] { "1.5H", "3H", "5H" })
        {
            var kind = comboType.GetMethod("获取观看距离胶囊类别", BindingFlags.Static | BindingFlags.NonPublic)!.Invoke(null, [distance])!;
            var colors = ((Color Background, Color Foreground))comboType.GetMethod("获取胶囊配色", BindingFlags.Static | BindingFlags.NonPublic)!.Invoke(null, [kind, !dark])!;
            palette.Add(new { distance, background = ColorTranslator.ToHtml(colors.Background), foreground = ColorTranslator.ToHtml(colors.Foreground) });
        }
        File.WriteAllText(Path.Combine(output, "capsules.json"), JsonSerializer.Serialize(new
        {
            time = DateTimeOffset.Now, dark, dpi = combo.DeviceDpi,
            subsample_width = sample.Width, pooling_width = pooling.Width,
            nonneg_logical_width = 160, capsule_logical_height = 22,
            row_logical_height = combo.DropDownItemHeight, max_visible_rows = combo.MaxDropDownItems,
            popup_width = popup.Width, popup_height = popup.Height, selections, palette
        }, new JsonSerializerOptions { WriteIndented = true }), new UTF8Encoding(false));
        combo.DroppedDown = false;
        Console.WriteLine($"胶囊实测通过：深色={dark}；DPI={combo.DeviceDpi}；三种宽度 CUDA 均可见；证据={output}");
    }

    public static int Run(string root, string ffmpeg, bool dark, bool capsulesOnly = false)
    {
        var output = Path.Combine(Path.GetFullPath(root), "artifacts", "ui-vmaf-20261029", capsulesOnly ? "capsule-preview" : "ui-catalog", DateTime.Now.ToString("yyyyMMdd-HHmmss-fff"));
        Directory.CreateDirectory(output);
        Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
        Application.EnableVisualStyles();
        设置_v6.实例对象 = new 设置_v6 { 界面主题 = dark ? 2 : 1, 替代进程文件名 = Path.GetFullPath(ffmpeg) };
        界面主题_v6.初始化();
        using var page = new Form_v6_集成工具_质量评测
        {
            Text = "FFmpegFreeUI · HDR 模型列表隔离预览",
            ClientSize = new Size(1450, 900),
            StartPosition = FormStartPosition.CenterScreen,
            FormBorderStyle = FormBorderStyle.Sizable
        };
        var window = page;
        var failed = false;
        window.Shown += async (_, _) =>
        {
            try
            {
                await page.刷新Vmaf模型列表Async();
                界面主题_v6.应用当前主题到窗体(window);
                var combo = (VmafModelComboBox)page.Controls.Find("MCB_模型选择", true).Single();
                if (capsulesOnly)
                {
                    await CheckCapsuleRefinements(window, combo, output, dark);
                    window.Close();
                    return;
                }
                combo.SelectedIndex = combo.FindModelIndex("hdr_vmaf_v1.1.8_hfr_1d5h_2160", false);
                var snapshot = new
                {
                    time = DateTimeOffset.Now,
                    process_id = Environment.ProcessId,
                    ffmpeg = Path.GetFullPath(ffmpeg),
                    dark,
                    dpi = window.DeviceDpi,
                    hdr_count = combo.Models.Count(x => x.IsHdr),
                    models = combo.Models.Select(x => new { x.ModelValue, x.DisplayText, x.IsHdr, x.IsNonNeg, x.IsSectionHeader, x.IsRuntimeDetected, x.IsCudaAvailable, x.UseCuda }),
                    selected_tooltip = combo.ItemToolTips.Single(x => x.ItemText == combo.SelectedModel!.DisplayText).ToolTipText
                };
                File.WriteAllText(Path.Combine(output, "models.json"), JsonSerializer.Serialize(snapshot, new JsonSerializerOptions { WriteIndented = true, Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping }), new UTF8Encoding(false));
                combo.Focus();
                combo.DroppedDown = true;
                await Task.Delay(600);
                var popup = (Form?)typeof(LakeUI.ModernComboBox).GetField("_dropDownForm", BindingFlags.Instance | BindingFlags.NonPublic)!.GetValue(combo);
                if (!combo.DroppedDown || popup is null || !popup.Visible || popup.IsDisposed)
                    throw new InvalidOperationException("模型下拉层没有保持展开，不能记录为显示验证成功。");
                if (combo.MaxDropDownItems != 12) throw new InvalidOperationException("模型下拉没有配置为十二行。");
                var expectedHeight = (int)Math.Round(12 * combo.DropDownItemHeight * combo.DeviceDpi / 96.0) + combo.DropDownPadding.Vertical + 2 * (int)Math.Round(combo.DropDownBorderSize * combo.DeviceDpi / 96.0);
                if (Math.Abs(popup.Height - expectedHeight) > 2) throw new InvalidOperationException("实际下拉高度与十二行设置不符。");
                var minimumPopupWidth = (int)Math.Ceiling(330 * combo.DeviceDpi / 96.0) + combo.DropDownPadding.Horizontal + 20;
                if (popup.Width < minimumPopupWidth)
                    throw new InvalidOperationException($"预览窗口过窄：下拉层 {popup.Width}px，完整六列至少需要 {minimumPopupWidth}px。");
                // 下拉层是独立顶层窗口；只捕获主窗体不会包含它。
                Capture(popup, Path.Combine(output, "dropdown.png"));
                File.WriteAllText(Path.Combine(output, "display.json"), JsonSerializer.Serialize(new
                {
                    time = DateTimeOffset.Now,
                    dark,
                    dpi = popup.DeviceDpi,
                    dropdown_visible = popup.Visible,
                    popup_width = popup.Width,
                    popup_height = popup.Height,
                    max_visible_rows = combo.MaxDropDownItems,
                    combo_width = combo.Width,
                    combo_dpi = combo.DeviceDpi,
                    hdr_count = combo.Models.Count(x => x.IsHdr),
                    selected_model = combo.SelectedModel?.ModelValue
                }, new JsonSerializerOptions { WriteIndented = true }), new UTF8Encoding(false));
                var expectedWidth = (int)Math.Round(combo.ToolTipMaxWidth * combo.DeviceDpi / 96.0, MidpointRounding.AwayFromZero);
                var tooltips = new List<object>();
                var representatives = new Dictionary<(string, bool), string>
                {
                    [("AUTO", false)] = "auto",
                    [("vmaf_v1.0.16_hfr_3d0h_2160", false)] = "sdr",
                    [("hdr_vmaf_v1.1.8_hfr_3d0h_2160", false)] = "hdr",
                    [("hdr_vmaf_v1.1.8_3d0h", false)] = "hdr-default",
                    [("vmaf_v0.6.1neg", false)] = "v0",
                    [("vmaf_v0.6.1neg", true)] = "v0-cuda",
                    [("vmaf_v0.6.1", false)] = "v0-plain",
                    [("vmaf_4k_v0.6.1", true)] = "v0-plain-4k-cuda"
                };
                var sdrIndex = combo.FindModelIndex("vmaf_v1.0.16_hfr_3d0h_2160", false);
                var hdrIndex = combo.FindModelIndex("hdr_vmaf_v1.1.8_hfr_3d0h_2160", false);
                string TipText(int index) => combo.ItemToolTips.Single(x => x.ItemText == combo.Models[index - 1].DisplayText).ToolTipText;
                if (sdrIndex < 0 || hdrIndex < 0) throw new InvalidOperationException("缺少 SDR/HDR 对照模型。");
                var sdrText = TipText(sdrIndex);
                if (TipText(hdrIndex) != sdrText.Insert(sdrText.IndexOf("\r\n\r\n", StringComparison.Ordinal), "，支持 HDR"))
                    throw new InvalidOperationException("HDR 简介未正确补充支持 HDR。");
                var header = combo.Models.ToList().FindIndex(x => x.IsSectionHeader) + 1;
                if (header != combo.Items.Count - 5 || combo.Models.Count(x => x.IsNonNeg) != 4 || combo.Items[header] != VmafModelDisplayItem.NonNegHeaderText)
                    throw new InvalidOperationException("非 NEG 分组没有追加为底部五行。");
                combo.SelectedIndex = header - 1;
                var keyDown = typeof(VmafModelComboBox).GetMethod("OnKeyDown", BindingFlags.Instance | BindingFlags.NonPublic)!;
                keyDown.Invoke(combo, [new KeyEventArgs(Keys.Down)]);
                if (combo.SelectedIndex != header + 1) throw new InvalidOperationException("向下按键未跳过警示行。");
                keyDown.Invoke(combo, [new KeyEventArgs(Keys.Up)]);
                if (combo.SelectedIndex != header - 1) throw new InvalidOperationException("向上按键未跳过警示行。");
                HoverModel(combo, popup, combo.Items.Count - 1);
                await Task.Delay(120);
                Capture(popup, Path.Combine(output, "dropdown-legacy-nonneg.png"));
                for (var index = 1; index < combo.Items.Count; index++)
                {
                    var model = combo.Models[index - 1];
                    var text = combo.ItemToolTips.FirstOrDefault(x => x.ItemText == model.DisplayText)?.ToolTipText;
                    if (string.IsNullOrEmpty(text)) continue;
                    HoverModel(combo, popup, index);
                    await Task.Delay(100);
                    var tip = CheckTip(popup, expectedWidth);
                    if (representatives.TryGetValue((model.ModelValue, model.UseCuda), out var name))
                        Capture(tip, Path.Combine(output, "tooltip-" + name + ".png"));
                    if (model.VersionLabel == "V0" && !model.IsNonNeg && text != "旧模型，不推荐")
                        throw new InvalidOperationException("旧 V0 提示没有统一。");
                    tooltips.Add(new { model = model.ModelValue, model.IsNonNeg, model.UseCuda, text, width = tip.Width, height = tip.Height, dpi = tip.DeviceDpi });
                }
                if (tooltips.Count == 0) throw new InvalidOperationException("没有实际检查任何模型提示。");
                popup.GetType().GetMethod("OnMouseWheel", BindingFlags.Instance | BindingFlags.NonPublic)!
                    .Invoke(popup, [new MouseEventArgs(MouseButtons.None, 0, 10, 10, -120)]);
                var closedTip = (Form?)popup.GetType().GetField("_tipForm", BindingFlags.Instance | BindingFlags.NonPublic)!.GetValue(popup);
                if (closedTip is { IsDisposed: false }) throw new InvalidOperationException("滚动后原提示没有释放。");
                HoverModel(combo, popup, sdrIndex);
                await Task.Delay(100);
                CheckTip(popup, expectedWidth);
                var screenArea = Screen.FromControl(combo).WorkingArea;
                var area = Rectangle.Intersect(screenArea, window.RectangleToScreen(window.ClientRectangle));
                var placements = new List<object>();
                foreach (var (name, side, x, expectLeft) in new[]
                {
                    ("window-right-edge", LakeUI.ModernComboBox.ToolTipSideEnum.Right, area.Right - popup.Width - 8, true),
                    ("window-left-edge", LakeUI.ModernComboBox.ToolTipSideEnum.Left, area.Left + 8, false),
                    ("prefer-left", LakeUI.ModernComboBox.ToolTipSideEnum.Left, area.Right - popup.Width - 8, true),
                    ("right-fits", LakeUI.ModernComboBox.ToolTipSideEnum.Right, area.Right - popup.Width - expectedWidth - 8, false)
                })
                {
                    combo.ToolTipSide = side;
                    popup.Location = new Point(x, area.Bottom - popup.Height);
                    HoverModel(combo, popup, sdrIndex);
                    await Task.Delay(100);
                    var tip = CheckTip(popup, expectedWidth);
                    var gap = (int)Math.Round(combo.ToolTipGap * combo.DeviceDpi / 96.0, MidpointRounding.AwayFromZero);
                    var expectedX = expectLeft ? popup.Left - gap - tip.Width : popup.Right + gap;
                    if (!area.Contains(tip.Bounds) || tip.Left != expectedX)
                        throw new InvalidOperationException("固定宽度提示的翻转或屏幕边缘定位错误：" + name);
                    placements.Add(new { name, tip.Left, tip.Top, tip.Width, tip.Height, expected_x = expectedX, fits_host = area.Contains(tip.Bounds), fits_screen = screenArea.Contains(tip.Bounds) });
                    if (name is "window-right-edge" or "right-fits")
                    {
                        Capture(tip, Path.Combine(output, "tooltip-" + name + ".png"));
                        CapturePopupStack(window, popup, tip, Path.Combine(output, name + ".png"));
                    }
                }
                var beforeWarningClick = combo.SelectedIndex;
                HoverModel(combo, popup, header);
                var headerRect = (RectangleF)popup.GetType().GetMethod("GetItemRect", BindingFlags.Instance | BindingFlags.NonPublic)!.Invoke(popup, [header])!;
                var click = new MouseEventArgs(MouseButtons.Left, 1, 10, (int)(headerRect.Top + headerRect.Height / 2), 0);
                popup.GetType().GetMethod("OnMouseDown", BindingFlags.Instance | BindingFlags.NonPublic)!.Invoke(popup, [click]);
                popup.GetType().GetMethod("OnMouseUp", BindingFlags.Instance | BindingFlags.NonPublic)!.Invoke(popup, [click]);
                await Task.Delay(250);
                if (combo.SelectedIndex != beforeWarningClick || combo.SelectedModel is null || combo.SelectedModel.IsSectionHeader)
                    throw new InvalidOperationException("点击警示行改变了实际选中模型。");
                File.WriteAllText(Path.Combine(output, "tooltips.json"), JsonSerializer.Serialize(new
                {
                    fixed_logical_width = combo.ToolTipMaxWidth,
                    expected_pixel_width = expectedWidth,
                    host_visible_area = area,
                    screen_working_area = screenArea,
                    hdr_support_note = true,
                    nonneg_count = combo.Models.Count(x => x.IsNonNeg),
                    nonneg_header = combo.Items[header],
                    keyboard_skips_header = true,
                    warning_click_preserves_selection = true,
                    recreated_after_scroll = true,
                    placements,
                    tooltips
                }, new JsonSerializerOptions { WriteIndented = true, Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping }), new UTF8Encoding(false));
                combo.DroppedDown = false;
                Console.WriteLine($"显示已验证：PID={Environment.ProcessId}；HDR={snapshot.hdr_count}；提示={tooltips.Count} 项，固定 {expectedWidth}px；证据={output}");
                Console.Out.Flush();
                window.Close();
            }
            catch (Exception ex)
            {
                failed = true;
                Console.WriteLine("预览失败：" + ex);
                window.Close();
            }
        };
        Application.Run(window);
        return failed ? 1 : 0;
    }
}
