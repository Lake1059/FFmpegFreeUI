using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Text.Json;
using FFmpegFreeUI;

internal static partial class Program
{
    private static T PumpAgentTask<T>(Task<T> task, string message)
    {
        PumpAgentUntil(() => task.IsCompleted, message);
        return task.GetAwaiter().GetResult();
    }

    private static void TestAgentAudit(string directory)
    {
        var args = Agent通用工具_v6.ParseJsonArguments("{\"arguments\":[\"\",\" spaced \",\"x\",\"x\"]}");
        Check(Agent通用工具_v6.GetJsonStringArray(args, "arguments", false).SequenceEqual(new[] { "", " spaced ", "x", "x" }),
            "Executable arguments must preserve empty strings, whitespace and duplicates");
        foreach (var encoding in new Encoding[] { Encoding.Unicode, Encoding.BigEndianUnicode, Encoding.UTF8 })
            Check(Agent通用工具_v6.DecodeTextBytes(encoding.GetPreamble().Concat(encoding.GetBytes("你好")).ToArray()) == "你好", "BOM must not become file text");

        Task<string> Execute(string name, object arguments, CancellationToken token = default) =>
            AgentLocalTools.ExecuteAsync(new AgentToolCallInfo { Name = name, Arguments = JsonSerializer.Serialize(arguments) },
                AgentLocalTools.PermissionSystem, AgentNetworkMode.Disabled, null!, "", "", null!, token);
        var processArgs = PumpAgentTask(Execute("run_windows_executable", new {
            executable = Environment.ProcessPath, arguments = new[] { "--agent-args-fixture", "", " spaced ", "x", "x" }
        }), "Argument fixture must finish");
        using (var result = JsonDocument.Parse(processArgs))
        {
            Check(result.RootElement.GetProperty("exit_code").GetInt32() == 0, "Argument fixture must succeed");
            var received = JsonSerializer.Deserialize<string[]>(result.RootElement.GetProperty("stdout").GetString()!);
            Check(received!.SequenceEqual(new[] { "", " spaced ", "x", "x" }), "Actual process receives exact positional arguments");
        }
        var timer = Stopwatch.StartNew();
        using (var result = JsonDocument.Parse(PumpAgentTask(Execute("run_windows_executable", new {
            executable = Environment.ProcessPath, arguments = new[] { "--wait-fixture" }, timeout_seconds = 1,
            stdin = new string('a', 512000)
        }), "Timeout must cover blocked stdin and output EOF")))
            Check(result.RootElement.GetProperty("timed_out").GetBoolean() && timer.Elapsed < TimeSpan.FromSeconds(8), "Executable timeout must not wait for its 30-second process");
        using (var cts = new CancellationTokenSource(150))
        {
            var cancelled = Execute("run_windows_executable", new { executable = Environment.ProcessPath, arguments = new[] { "--wait-fixture" } }, cts.Token);
            PumpAgentUntil(() => cancelled.IsCompleted, "Executable cancellation must finish");
            Check(cancelled.IsCanceled, "User cancellation remains cancellation rather than tool success");
        }
        using (var result = JsonDocument.Parse(PumpAgentTask(Execute("run_windows_executable", new {
            executable = Environment.ProcessPath, arguments = new[] { "--output-fixture" }, max_output_chars = 128
        }), "Output drainage must finish")))
        {
            Check(result.RootElement.GetProperty("exit_code").GetInt32() == 0, "Output truncation must still drain both streams");
            Check(result.RootElement.GetProperty("stdout").GetString()!.Length < 160 && result.RootElement.GetProperty("stderr").GetString()!.Length < 160,
                "Both captured output buffers must be bounded");
        }
        using (var firstShell = new AgentLocalTools.PowerShellRunSession())
        using (var secondShell = new AgentLocalTools.PowerShellRunSession())
        {
            string Shell(AgentLocalTools.PowerShellRunSession session, string command, int timeout = 15) =>
                PumpAgentTask(session.ExecuteAsync(Agent通用工具_v6.ParseJsonArguments(JsonSerializer.Serialize(new { command, timeout_seconds = timeout })), default), "PowerShell session must finish");
            Shell(firstShell, "$agentAuditValue = 41");
            using (var first = JsonDocument.Parse(Shell(firstShell, "Write-Output ($agentAuditValue + 1)")))
                Check(first.RootElement.GetProperty("stdout").GetString()!.Contains("42"), "PowerShell variables persist within a run");
            using (var second = JsonDocument.Parse(Shell(secondShell, "if (Test-Path variable:agentAuditValue) { throw 'leaked' }; Write-Output 'isolated'")))
                Check(second.RootElement.GetProperty("exit_code").GetInt32() == 0 && second.RootElement.GetProperty("stdout").GetString()!.Contains("isolated"), "PowerShell processes are isolated between runs");
            using (var timeout = JsonDocument.Parse(Shell(firstShell, "Start-Sleep -Seconds 20", 1)))
                Check(timeout.RootElement.GetProperty("timed_out").GetBoolean(), "PowerShell timeout kills its process without blocking UI cleanup");
            using (var second = JsonDocument.Parse(Shell(secondShell, "Write-Output 'alive'")))
                Check(second.RootElement.GetProperty("stdout").GetString()!.Contains("alive"), "Timing out another shell cannot stop this shell");
        }

        var source = Path.Combine(directory, "move-source");
        Directory.CreateDirectory(source);
        File.WriteAllText(Path.Combine(source, "keep.txt"), "keep");
        foreach (var target in new[] { source, directory, Path.Combine(source, "child") })
        {
            var result = PumpAgentTask(Execute("move_local_path", new { source, destination = target, overwrite = true }), "Overlapping move must return promptly");
            Check(result.Contains("拒绝移动") && File.ReadAllText(Path.Combine(source, "keep.txt")) == "keep", "Self/parent/child moves must not delete source");
        }
        using (var cts = new CancellationTokenSource())
        {
            cts.Cancel();
            var target = Path.Combine(directory, "must-not-write.txt");
            var cancelled = Execute("write_local_text_file", new { path = target, content = "wrong" }, cts.Token);
            Check(cancelled.IsCanceled && !File.Exists(target), "Cancelled synchronous tools must not mutate files");
        }

        var parse = typeof(AgentEndpointClient).GetMethod("ParseStreamingChatResult", BindingFlags.NonPublic | BindingFlags.Static)!;
        var chunk = "data: {\"choices\":[{\"delta\":{\"content\":\"partial\"}}]}\n\n";
        foreach (var invalid in new[] { chunk, "data: {\"error\":{\"message\":\"broken\"}}\n\ndata: [DONE]\n", "data: [DONE]\n" })
        {
            var rejected = false;
            try { parse.Invoke(null, new object[] { invalid }); }
            catch (TargetInvocationException ex) when (ex.InnerException is IOException or InvalidDataException) { rejected = true; }
            Check(rejected, "Incomplete/error/empty SSE must not be reported as success");
        }
        var complete = (AgentChatResult)parse.Invoke(null, new object[] { chunk + "data: [DONE]\n" })!;
        Check(complete.Content == "partial" && complete.Success, "Valid DONE stream remains supported");
        var finished = (AgentChatResult)parse.Invoke(null, new object[] { chunk + "data: {\"choices\":[{\"delta\":{},\"finish_reason\":\"stop\"}]}\n" })!;
        Check(finished.Content == "partial", "finish_reason also proves completion for compatible endpoints");
        using (var endpoint = new AgentStreamFixture())
        {
            var client = new AgentEndpointClient(endpoint.Endpoint, "", "");
            var failed = PumpAgentTask(client.TryCreateChatCompletionStreamingAsync("fixture",
                new[] { new AgentMessageData { Role = "user", Content = "EOF" } }, null!, "", _ => { }), "Premature HTTP EOF must return");
            Check(!failed.Success && failed.ToolCalls.Count == 0, "Actual prematurely closed SSE cannot execute tools or succeed");
        }
        var brokenArguments = PumpAgentTask(AgentLocalTools.ExecuteAsync(new AgentToolCallInfo { Name = "run_powershell", Arguments = "{\"command\":" },
            AgentLocalTools.PermissionSystem, AgentNetworkMode.Disabled, null!, "", ""), "Broken PowerShell JSON must be rejected");
        Check(brokenArguments.StartsWith("工具参数 JSON 解析失败"), "Malformed JSON must not be reinterpreted as raw executable script");

        var modelId = "gpt-agent-audit-" + Guid.NewGuid().ToString("N");
        var firstEndpoint = new AgentEndpointClient("http://127.0.0.1:1/v1", "", "");
        var otherEndpoint = new AgentEndpointClient("http://127.0.0.1:2/v1", "", "");
        var explicitEfforts = AgentCapabilityCache.GetReasoningEfforts(new AgentModelInfo { Id = modelId, ReasoningEfforts = new() { "low" } }, firstEndpoint);
        Check(explicitEfforts.SequenceEqual(new[] { "low" }), "Explicit endpoint reasoning capabilities override fallback table");
        Check(AgentCapabilityCache.GetReasoningEfforts(new AgentModelInfo { Id = modelId.ToUpperInvariant() }, firstEndpoint).SequenceEqual(new[] { "low" }), "Capability cache model lookup is case insensitive");
        Check(AgentCapabilityCache.GetReasoningEfforts(new AgentModelInfo { Id = modelId }, otherEndpoint).Contains("high"), "Same model on another endpoint cannot inherit cached restrictions");

        var storePath = Path.Combine(directory, "agent-recovery");
        var store = AgentConversationStore.Load(storePath);
        var conversation = store.EnsureConversation();
        conversation.DraftText = "keep draft";
        var user = new AgentMessageData { Role = "user", Content = "request" };
        conversation.Messages.Add(user);
        conversation.Messages.Add(new AgentMessageData { Role = "assistant", ToolCalls = new() { new() { Id = "unfinished", Name = "write_local_text_file", Arguments = "{}" } } });
        conversation.Turns.Add(new AgentTurnData { UserMessageId = user.Id, State = "running", Activities = new() { new() { Kind = "tool", State = "running" } } });
        store.Save();
        Check(conversation.Turns[0].State == "running", "Saving cannot cancel a live conversation");
        var recovered = AgentConversationStore.Load(storePath).Conversations.Single();
        Check(recovered.Turns[0].State == "canceled" && recovered.Turns[0].Activities[0].State == "canceled", "Loading recovers interrupted turn and activity");
        Check(recovered.Messages.Last().Role == "tool" && recovered.Messages.Last().ToolCallId == "unfinished", "Recovery closes missing tool protocol result");
        Check(recovered.DraftText == "keep draft", "Recovery preserves unsent draft");
        Check(AgentConversationStore.Load(storePath).Conversations.Single().Messages.Count == recovered.Messages.Count, "Recovery is idempotent");
        var malformedStore = AgentConversationStore.Load(Path.Combine(directory, "agent-malformed"));
        malformedStore.Conversations.Add(new AgentConversationData { Id = "a:b", DraftPaths = null!, Messages = new() { null! }, Turns = new() { null! } });
        malformedStore.Conversations.Add(new AgentConversationData { Id = "a?b" });
        malformedStore.Save();
        var normalized = AgentConversationStore.Load(Path.Combine(directory, "agent-malformed"));
        Check(normalized.Conversations.Count == 2, "Invalid filename characters cannot cause conversation ID collisions");
        Check(normalized.Conversations.All(x => x.DraftPaths != null && x.Messages.All(m => m != null) && x.Turns.All(t => t != null)), "Null legacy collections are normalized");
        var guidance = new AgentConversationData { Messages = new() { new() { Role = "user", Name = AgentConversationSchema.SteeringMessageName, Content = "调整方向：这是用户原文" } } };
        Check(AgentConversationJsonUpgrader.Upgrade(JsonSerializer.Serialize(guidance)).Conversation.Messages[0].Content == "调整方向：这是用户原文",
            "Current-schema user content must not be rewritten by legacy migration");
        var convert = typeof(AgentLocalTools).GetMethod("DeserializeJsonElement", BindingFlags.NonPublic | BindingFlags.Static)!;
        foreach (var item in new[] { ("\"NaN\"", typeof(double)), ("99", typeof(DayOfWeek)) })
        {
            var rejected = false;
            try { convert.Invoke(null, new object[] { Agent通用工具_v6.ParseJsonArguments(item.Item1), item.Item2 }); }
            catch (TargetInvocationException ex) when (ex.InnerException is InvalidOperationException) { rejected = true; }
            Check(rejected, "Invalid numeric/enum parameter values must fail before applying UI changes");
        }
        var resolve = typeof(AgentLocalTools).GetMethod("ResolveQueueTarget", BindingFlags.NonPublic | BindingFlags.Static)!;
        var conflict = resolve.Invoke(null, new object[] { Agent通用工具_v6.ParseJsonArguments("{\"target\":\"all\",\"id\":\"one\"}"), new List<编码任务_v6>(), false })!;
        Check(AgentMember<List<string>>(conflict, "Errors").Count > 0, "Conflicting queue selectors cannot silently expand to all tasks");
    }
}
