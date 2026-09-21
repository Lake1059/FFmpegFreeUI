using System.Text.Json;
using FFmpegFreeUI;
using LakeUI;

internal static partial class Program
{
    private static void TestAgentPresentation(string directory)
    {
        var savedSettings = 设置_v6.实例对象;
        using var form = new Form_v6_Agent();
        try
        {
            // Simulate restart by round-tripping the same settings contract used on disk.
            设置_v6.实例对象 = JsonSerializer.Deserialize<设置_v6>(JsonSerializer.Serialize(new 设置_v6 { Agent推理级别 = "high", AgentEndPoint = "http://127.0.0.1:1/v1" }))!;
            var conversation = new AgentConversationData { ReasoningEffort = "" };
            var store = AgentConversationStore.Load(Path.Combine(directory, "presentation"));
            store.Conversations.Add(conversation);
            AgentSet(form, "_current", conversation);
            AgentSet(form, "_store", store);
            AgentSet(form, "_loading", true);
            var model = new AgentModelInfo { Id = "presentation-fixture", ReasoningEfforts = new() { "low", "high" } };
            AgentSet(form, "_models", new List<AgentModelInfo> { model });
            var models = AgentMember<ModernComboBox>(form, "MCB_模型选择");
            var efforts = AgentMember<ModernComboBox>(form, "MCB_推理级别");
            models.Items.Add(model.Id);
            models.SelectedIndex = 0;
            Invoke(form, "RefreshReasoningEfforts");
            Check(efforts.SelectedItem == "high", "Empty conversation must restore saved reasoning effort after restart");
            model.ReasoningEfforts = new() { "low" };
            Invoke(form, "RefreshReasoningEfforts");
            Check(efforts.SelectedItem == "low" && 设置_v6.实例对象.Agent推理级别 == "high", "Temporary unsupported effort must not overwrite saved preference");
            model.ReasoningEfforts = new() { "low", "high" };
            Invoke(form, "RefreshReasoningEfforts");
            Check(efforts.SelectedItem == "high", "Returning to supported model restores preference");
            AgentSet(form, "_loading", false);
            efforts.SelectedIndex = 0;
            Check(设置_v6.实例对象.Agent推理级别 == "low" && conversation.ReasoningEffort == "low", "Explicit reasoning selection updates preference and conversation");
            efforts.SelectedIndex = -1;
            Check(设置_v6.实例对象.Agent推理级别 == "low", "Clearing combo must not erase saved effort");

            var failed = new AgentTurnActivityData { Kind = "tool", ToolName = "run_powershell", State = "error", ElapsedMilliseconds = 1200, Arguments = "{\"command\":\"Get-Date\"}", ResultText = "{\"exit_code\":7}" };
            var running = new AgentTurnActivityData { Kind = "tool", ToolName = "get_queue_summary", State = "running", CreatedAt = DateTime.Now.AddSeconds(-3) };
            var title = (string)Invoke(form, "FormatToolGroupTitle", new List<AgentTurnActivityData> { failed, running })!;
            Check(title.Contains("读取队列信息") && title.Contains("正在执行：") && title.Contains("总耗时") && title.Contains("共 2 次") && title.Contains("错误 1 次"), "Running group retains all metrics even after earlier error");
            title = (string)Invoke(form, "FormatToolGroupTitle", new List<AgentTurnActivityData> { failed })!;
            Check(title.Contains("总耗时 1.2 秒") && title.Contains("共 1 次") && title.Contains("错误 1 次"), "Single failed call retains duration and counts");
            var details = (string)Invoke(form, "FormatToolActivityDetails", failed)!;
            Check(details.Contains(failed.Arguments) && details.Contains("退出码 7"), "Tool details contain complete arguments and exit status");
            running.State = "canceled";
            running.ElapsedMilliseconds = 3000;
            title = (string)Invoke(form, "FormatToolGroupTitle", new List<AgentTurnActivityData> { failed, running })!;
            Check(title.Contains("总耗时 4.2 秒") && title.Contains("错误 1 次") && title.Contains("取消 1 次"), "Canceled group retains all metrics");
        }
        finally { 设置_v6.实例对象 = savedSettings; }
    }
}
