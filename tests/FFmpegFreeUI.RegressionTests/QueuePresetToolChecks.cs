using System.Reflection;
using System.Text.Json;
using FFmpegFreeUI;

internal static partial class Program
{
    private static void TestQueuePresetTools(string directory)
    {
        var first = 编码队列_v6.添加预设任务(Path.Combine(directory, "patch-a.mp4"), new 预设数据_v6 { 预设备注 = "first", 输出容器 = "mp4" });
        var second = 编码队列_v6.添加预设任务(Path.Combine(directory, "patch-b.mp4"), new 预设数据_v6 { 预设备注 = "second", 输出容器 = "mkv" });
        var method = typeof(AgentLocalTools).GetMethod("SyncParameterPanelToQueue", BindingFlags.Static | BindingFlags.NonPublic)!;
        JsonDocument Run(object args, bool patch = true)
        {
            using var input = JsonDocument.Parse(JsonSerializer.Serialize(args));
            return JsonDocument.Parse((string)method.Invoke(null, new object[] { input.RootElement, patch })!);
        }
        try
        {
            using var missing = Run(new { }, false);
            Check(!missing.RootElement.GetProperty("success").GetBoolean(), "Sync without selectors must fail");
            using var all = Run(new { target = "all" }, false);
            Check(!all.RootElement.GetProperty("success").GetBoolean(), "Sync must reject all targets");
            using var invalid = Run(new { ids = new[] { first.ID, "missing" }, changes = new { 预设备注 = "bad" } });
            Check(!invalid.RootElement.GetProperty("success").GetBoolean() && first.预设数据.预设备注 == "first", "Missing target must prevent partial changes");
            using var changed = Run(new { id = first.ID, changes = new { 预设备注 = "patched" } });
            Check(changed.RootElement.GetProperty("updated_count").GetInt32() == 1 && first.预设数据.预设备注 == "patched" && second.预设数据.预设备注 == "second", "Patch changes only selected task");
            Check(first.预设数据.输出容器 == "mp4", "Patch preserves unspecified options");
            using var both = Run(new { ids = new[] { first.ID, second.ID, first.ID }, changes = new { 预设备注 = "both" } });
            Check(both.RootElement.GetProperty("updated_count").GetInt32() == 2 && first.预设数据.输出容器 == "mp4" && second.预设数据.输出容器 == "mkv", "Batch patch deduplicates and preserves independent presets");
            try
            {
                using var badField = Run(new { ids = new[] { first.ID, second.ID }, changes = new Dictionary<string, object> { ["预设备注"] = "bad", ["unknown_field"] = 1 } });
                throw new Exception("Unknown field accepted");
            }
            catch (TargetInvocationException ex) when (ex.InnerException is InvalidOperationException) { }
            Check(first.预设数据.预设备注 == "both" && second.预设数据.预设备注 == "both", "Invalid field must not commit earlier changes");
            second.状态 = 编码任务状态_v6.已完成;
            using var guarded = Run(new { ids = new[] { first.ID, second.ID }, changes = new { 预设备注 = "pending" } });
            Check(guarded.RootElement.GetProperty("updated_count").GetInt32() == 1 && second.预设数据.预设备注 == "both", "Completed tasks remain unchanged");
        }
        finally { 编码队列_v6.移除任务(new[] { first.ID, second.ID }); }
    }
}
