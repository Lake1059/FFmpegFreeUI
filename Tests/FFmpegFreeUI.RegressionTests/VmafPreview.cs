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

    internal static void Capture(Form form, string path)
    {
        using var bitmap = new Bitmap(form.Width, form.Height);
        using var graphics = Graphics.FromImage(bitmap);
        var dc = graphics.GetHdc();
        try
        {
            if (!PrintWindow(form.Handle, dc, 2)) throw new InvalidOperationException("无法捕获本次预览窗口。");
        }
        finally { graphics.ReleaseHdc(dc); }
        bitmap.Save(path, System.Drawing.Imaging.ImageFormat.Png);
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
        return tip;
    }

    public static int Run(string root, string ffmpeg, bool dark)
    {
        var output = Path.Combine(Path.GetFullPath(root), "artifacts", "ui-vmaf-20261028", "ui-catalog", DateTime.Now.ToString("yyyyMMdd-HHmmss-fff"));
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
                combo.SelectedIndex = combo.FindModelIndex("hdr_vmaf_v1.1.8_hfr_1d5h_2160", false);
                var snapshot = new
                {
                    time = DateTimeOffset.Now,
                    process_id = Environment.ProcessId,
                    ffmpeg = Path.GetFullPath(ffmpeg),
                    dark,
                    dpi = window.DeviceDpi,
                    hdr_count = combo.Models.Count(x => x.IsHdr),
                    models = combo.Models.Select(x => new { x.ModelValue, x.DisplayText, x.IsHdr, x.IsRuntimeDetected, x.UseCuda }),
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
                    [("vmaf_v0.6.1neg", false)] = "v0",
                    [("vmaf_v0.6.1neg", true)] = "v0-cuda",
                    [("vmaf_v0.6.1", false)] = "v0-plain"
                };
                var sdrIndex = combo.FindModelIndex("vmaf_v1.0.16_hfr_3d0h_2160", false);
                var hdrIndex = combo.FindModelIndex("hdr_vmaf_v1.1.8_hfr_3d0h_2160", false);
                string TipText(int index) => combo.ItemToolTips.Single(x => x.ItemText == combo.Models[index - 1].DisplayText).ToolTipText;
                if (sdrIndex < 0 || hdrIndex < 0 || TipText(sdrIndex) != TipText(hdrIndex))
                    throw new InvalidOperationException("HDR 与同分类 SDR 提示文案不一致。");
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
                    tooltips.Add(new { model = model.ModelValue, model.UseCuda, text, width = tip.Width, height = tip.Height, dpi = tip.DeviceDpi });
                }
                if (tooltips.Count == 0) throw new InvalidOperationException("没有实际检查任何模型提示。");
                popup.GetType().GetMethod("OnMouseWheel", BindingFlags.Instance | BindingFlags.NonPublic)!
                    .Invoke(popup, [new MouseEventArgs(MouseButtons.None, 0, 10, 10, -120)]);
                var closedTip = (Form?)popup.GetType().GetField("_tipForm", BindingFlags.Instance | BindingFlags.NonPublic)!.GetValue(popup);
                if (closedTip is { IsDisposed: false }) throw new InvalidOperationException("滚动后原提示没有释放。");
                HoverModel(combo, popup, sdrIndex);
                await Task.Delay(100);
                CheckTip(popup, expectedWidth);
                var area = Screen.FromControl(combo).WorkingArea;
                var placements = new List<object>();
                foreach (var (name, side, x, expectLeft) in new[]
                {
                    ("right-edge", LakeUI.ModernComboBox.ToolTipSideEnum.Right, area.Right - popup.Width - 8, true),
                    ("left-edge", LakeUI.ModernComboBox.ToolTipSideEnum.Left, area.Left + 8, false),
                    ("prefer-left", LakeUI.ModernComboBox.ToolTipSideEnum.Left, area.Right - popup.Width - 8, true)
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
                    placements.Add(new { name, tip.Left, tip.Top, tip.Width, tip.Height, expected_x = expectedX });
                }
                File.WriteAllText(Path.Combine(output, "tooltips.json"), JsonSerializer.Serialize(new
                {
                    fixed_logical_width = combo.ToolTipMaxWidth,
                    expected_pixel_width = expectedWidth,
                    hdr_matches_sdr = true,
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
