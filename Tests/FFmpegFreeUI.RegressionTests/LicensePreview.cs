using System.Drawing;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;
using FFmpegFreeUI;
using LakeUI;

internal static class LicensePreview
{
    public static int Run(string root)
    {
        var output = Path.Combine(Path.GetFullPath(root), "artifacts", "ui-vmaf-20261028", "ui-license", DateTime.Now.ToString("yyyyMMdd-HHmmss-fff"));
        Directory.CreateDirectory(output);
        Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
        Application.EnableVisualStyles();
        设置_v6.实例对象 = new 设置_v6 { 界面主题 = 1 };
        界面主题_v6.初始化();
        using var page = new Form_v6_设置_LakeUI许可证 { ClientSize = new Size(1100, 750), StartPosition = FormStartPosition.CenterScreen, FormBorderStyle = FormBorderStyle.Sizable };
        var failed = false;
        page.Shown += async (_, _) =>
        {
            try
            {
                界面主题_v6.应用当前主题到窗体(page);
                await Task.Delay(500);
                var button = (ModernButton)page.Controls.Find("MB_前往购买许可证", true).Single();
                VmafPreview.Capture(page, Path.Combine(output, "license-light.png"));
                File.WriteAllText(Path.Combine(output, "colors.json"), JsonSerializer.Serialize(new
                {
                    time = DateTimeOffset.Now,
                    dpi = page.DeviceDpi,
                    normal = ColorTranslator.ToHtml(button.BackColor1),
                    hover = ColorTranslator.ToHtml(button.HoverBackColor1),
                    pressed = ColorTranslator.ToHtml(button.PressedBackColor1),
                    foreground = ColorTranslator.ToHtml(button.ForeColor)
                }, new JsonSerializerOptions { WriteIndented = true }), new UTF8Encoding(false));
                Console.WriteLine("许可证预览已捕获：" + output);
            }
            catch (Exception ex) { failed = true; Console.WriteLine("许可证预览失败：" + ex); }
            finally { page.Close(); }
        };
        Application.Run(page);
        return failed ? 1 : 0;
    }
}
