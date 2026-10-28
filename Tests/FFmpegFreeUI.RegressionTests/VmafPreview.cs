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

    public static int Run(string root, string ffmpeg, bool dark)
    {
        var output = Path.Combine(Path.GetFullPath(root), "artifacts", "ui-vmaf-agent-20261028", "ui-catalog", DateTime.Now.ToString("yyyyMMdd-HHmmss-fff"));
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
                combo.DroppedDown = false;
                Console.WriteLine($"显示已验证：PID={Environment.ProcessId}；HDR={snapshot.hdr_count}；证据={output}");
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
