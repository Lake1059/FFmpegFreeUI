using System.Collections.Concurrent;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Text.Json;
using FFmpegFreeUI;
using LakeUI;

internal static partial class Program
{
    private static T AgentMember<T>(object owner, string name)
    {
        var flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
        return (T)(owner.GetType().GetField(name, flags)?.GetValue(owner)
            ?? owner.GetType().GetProperty(name, flags)!.GetValue(owner))!;
    }

    private static void AgentSet(object owner, string name, object value) =>
        owner.GetType().GetField(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)!.SetValue(owner, value);

    private static void PumpAgentUntil(Func<bool> predicate, string message)
    {
        var timeout = Stopwatch.StartNew();
        while (!predicate() && timeout.Elapsed < TimeSpan.FromSeconds(15))
        {
            System.Windows.Forms.Application.DoEvents();
            Thread.Sleep(1);
        }
        Check(predicate(), message);
    }

    private static void TestAgentConversations(string directory)
    {
        using var endpoint = new AgentStreamFixture();
        var settings = 设置_v6.实例对象;
        var oldEndpoint = settings.AgentEndPoint;
        settings.AgentEndPoint = endpoint.Endpoint;
        var previousContext = SynchronizationContext.Current;
        using var form = new Form_v6_Agent();
        try
        {
            SynchronizationContext.SetSynchronizationContext(new System.Windows.Forms.WindowsFormsSynchronizationContext());
            var storePath = Path.Combine(directory, "agent-conversations");
            var store = AgentConversationStore.Load(storePath);
            AgentConversationData Conversation(string title)
            {
                var conversation = new AgentConversationData { Title = title, ModelId = "fixture", SortOrder = store.Conversations.Count + 1 };
                conversation.Messages.Add(new AgentMessageData { Role = "user", Content = title });
                store.Conversations.Add(conversation);
                return conversation;
            }
            var a = Conversation("A");
            var b = Conversation("B");
            AgentSet(form, "_store", store);
            AgentSet(form, "_current", a);
            a.NetworkMode = AgentNetworkMode.Disabled;
            a.PermissionLevel = AgentLocalTools.PermissionSystem;
            a.ReasoningEffort = "low";
            b.ModelId = "fixture-other";
            b.ReasoningEffort = "high";
            AgentSet(form, "_models", new List<AgentModelInfo> {
                new() { Id = a.ModelId, ReasoningEfforts = new() { "low", "high" } },
                new() { Id = b.ModelId, ReasoningEfforts = new() { "low", "high" } }
            });
            AgentSet(form, "_loading", true);
            var modelSelector = AgentMember<ModernComboBox>(form, "MCB_模型选择");
            modelSelector.Items.AddRange(new[] { a.ModelId, b.ModelId });
            modelSelector.SelectedIndex = 0;
            var reasoningSelector = AgentMember<ModernComboBox>(form, "MCB_推理级别");
            reasoningSelector.Items.AddRange(new[] { "low", "high" });
            reasoningSelector.SelectedIndex = 0;
            AgentMember<ModernComboBox>(form, "MCB_联网设置").SelectedIndex = a.NetworkMode;
            AgentMember<ModernComboBox>(form, "MCB_权限控制").SelectedIndex = a.PermissionLevel;
            var selectors = new[] { modelSelector, reasoningSelector,
                AgentMember<ModernComboBox>(form, "MCB_联网设置"), AgentMember<ModernComboBox>(form, "MCB_权限控制") };
            var originalOptions = selectors.Select(x => x.Items.ToArray()).ToArray();
            var originalSelections = selectors.Select(x => x.SelectedIndex).ToArray();
            void CheckSelectorsUnchanged(string operation)
            {
                Check(selectors.Select((x, i) => x.Items.SequenceEqual(originalOptions[i]) && x.SelectedIndex == originalSelections[i]).All(x => x),
                    operation + " preserves all bottom dropdown options and selections");
            }
            AgentSet(form, "_loading", false);
            Invoke(form, "RefreshConversationList");
            var list = AgentMember<ModernListBox>(form, "ModernListBox1");
            var files = AgentMember<ModernListBox>(form, "ModernListBox2");
            var input = AgentMember<ModernTextBox>(form, "ModernTextBox1");
            var room = AgentMember<AgentRoom>(form, "AgentRoom1");
            var send = AgentMember<ModernButton>(form, "MB_发送");
            void Select(AgentConversationData conversation)
            {
                var ordered = AgentMember<List<AgentConversationData>>(form, "_orderedConversations");
                list.SelectedIndex = ordered.IndexOf(conversation);
            }
            Color RowColor(AgentConversationData conversation)
            {
                var ordered = AgentMember<List<AgentConversationData>>(form, "_orderedConversations");
                var args = new ModernListBox.ItemForeColorEventArgs(ordered.IndexOf(conversation), conversation.Title, list.ForeColor);
                Invoke(form, "ModernListBox1_ItemForeColorNeeded", list, args);
                return args.ForeColor;
            }

            input.Text = "draft A\r\nnot submitted";
            var attachment = Path.Combine(directory, "agent-attachment.txt");
            File.WriteAllText(attachment, "fixture");
            Invoke(form, "AddSubmittedFiles", (object)new[] { attachment, directory });
            Select(b);
            Check(input.Text == "" && files.Items.Count == 0, "New conversation must not inherit another draft");
            CheckSelectorsUnchanged("Switching conversations");
            Invoke(input, "Undo");
            Check(input.Text == "", "Undo after switching cannot bring back another conversation's text");
            input.Text = "draft B";
            Select(a);
            Check(input.Text == a.DraftText && files.Items.Count == 2, "Switching restores text and both file/folder attachments");
            AgentSet(files, "_dragReorderSourceIndices", new List<int> { 0 });
            AgentSet(files, "_dragReorderInsertIndex", 2);
            Invoke(files, "执行多项拖动排序");
            Check(a.DraftPaths.SequenceEqual(new[] { directory, attachment }), "Attachment drag order updates the selected draft");
            files.SelectedIndex = 1;
            Invoke(form, "RemoveSelectedSubmittedFile");
            Check(a.DraftPaths.SequenceEqual(new[] { directory }) && b.DraftPaths.Count == 0, "Attachment removal stays in its conversation");
            store.Save();
            var reloaded = AgentConversationStore.Load(storePath);
            Check(reloaded.Conversations.Single(x => x.Id == a.Id).DraftText == a.DraftText, "Draft text survives save/load");
            Check(reloaded.Conversations.Single(x => x.Id == a.Id).DraftPaths.SequenceEqual(a.DraftPaths), "Draft paths survive save/load");
            Check(a.Messages.Count == 1 && b.Messages.Count == 1, "Draft editing never submits messages");

            // Exercise actual concurrent async runs against independent gated SSE responses.
            var taskA = (Task)Invoke(form, "StartAgentRunAsync", a)!;
            Select(b);
            var taskB = (Task)Invoke(form, "StartAgentRunAsync", b)!;
            var runA = Invoke(form, "GetConversationRuntime", a)!;
            var runB = Invoke(form, "GetConversationRuntime", b)!;
            PumpAgentUntil(() => endpoint.Started.Count == 2 &&
                ((string)Invoke(form, "GetActiveResponseTextSnapshot", a)!).Contains("reply-A-") &&
                ((string)Invoke(form, "GetActiveResponseTextSnapshot", b)!).Contains("reply-B-"), "Both runs must stream concurrently");
            Check(AgentMember<bool>(runA, "Busy") && AgentMember<bool>(runB, "Busy"), "Both conversations stay running");
            Check(selectors.All(x => x.Enabled), "All bottom dropdowns remain enabled during inference");
            var runningPermission = b.PermissionLevel;
            var permissionSelector = AgentMember<ModernComboBox>(form, "MCB_权限控制");
            permissionSelector.SelectedIndex = 2;
            Check(b.PermissionLevel == runningPermission, "Changing a dropdown cannot mutate the running task settings");
            permissionSelector.SelectedIndex = originalSelections[3];
            var green = AgentMember<ModernButton>(form, "MB_新对话").ForeColor;
            Check(RowColor(a) == green && RowColor(b) == green, "All running rows use the New Conversation green");
            Check(room.Items.Any(x => x.Text.Contains("reply-B-")) && !room.Items.Any(x => x.Text.Contains("reply-A-")), "Only selected response appears in room");
            Check(!endpoint.Requests.Any(x => x.Contains("draft A") || x.Contains("draft B") || x.Contains(attachment)), "Unsubmitted drafts stay out of HTTP context");
            var toolA = Invoke(form, "BeginToolActivity", a, new AgentToolCallInfo { Id = "tool-A", Name = "run_windows_executable", Arguments = "secret-A" })!;
            var toolB = Invoke(form, "BeginToolActivity", b, new AgentToolCallInfo { Id = "tool-B", Name = "run_windows_executable", Arguments = "secret-B" })!;
            var fullReturn = JsonSerializer.Serialize(new { exit_code = 7, stdout = new string('x', 20000) });
            Invoke(form, "CompleteToolActivity", a, toolA, fullReturn, 120d, false);
            Invoke(form, "CompleteToolActivity", b, toolB, "{\"exit_code\":0}", 50d, false);
            var activityA = a.Turns.Last().Activities.Last(x => x.Kind == "tool");
            var activityB = b.Turns.Last().Activities.Last(x => x.Kind == "tool");
            Check(activityA.State == "error" && activityB.State == "completed", "Concurrent tool outcomes belong to their own turns");
            Check(activityA.ResultText == fullReturn && activityA.ResultText.Length > 16000, "Full tool output survives in saved activity");
            Check(!room.Items.Any(x => x.Text.Contains("secret-A") || x.Text.Contains("退出码 7")), "Background tool details stay out of selected room");
            Check(room.Items.Any(x => x.Text.Contains("secret-B")), "Selected tool arguments are visible");
            Check(room.Items.Any(x => x.Text.Contains("退出码 0")), "Selected tool summary displays its own exit code");
            Check(room.Items.Last().Text == room.FindItem(b.Turns.Last().Id).Title, "Latest overview mirrors active turn header at bottom");
            Select(a);
            Check(room.Items.Any(x => x.Text.Contains("reply-A-")) && !room.Items.Any(x => x.Text.Contains("reply-B-")), "Switching restores the correct active response");
            Check(room.Items.Count(x => x.Text.Contains("thinking-A")) == 1, "Returning to active thinking must not duplicate it");
            Check(room.Items.Any(x => x.Text.Contains("secret-A")) && !room.Items.Any(x => x.Text.Contains("secret-B")), "Switching restores only selected tool arguments");
            Check(room.Items.Last().Text == room.FindItem(a.Turns.Last().Id).Title, "Switching restores bottom overview for selected turn");

            // Cancel A while B is still waiting for its endpoint; B's token and stream must survive.
            Invoke(form, "RequestStopAgentTask");
            PumpAgentUntil(() => taskA.IsCompleted, "Selected run must cancel");
            taskA.GetAwaiter().GetResult();
            Check(room.Items.Last().Text == room.FindItem(a.Turns.Last().Id).Title && room.Items.Last().Text.Contains("已停止"), "Bottom overview retains final stopped status");
            Check(!AgentMember<CancellationTokenSource>(runB, "RequestCts").IsCancellationRequested && !taskB.IsCompleted,
                "Stopping A must not cancel or finish B");
            Check(RowColor(a) == list.ForeColor && RowColor(b) == green, "Finished row resets without changing active row");
            Check(a.Turns.Last().State == "canceled" && b.Turns.Last().State == "running", "Turn states are isolated");
            Select(b);
            Check(input.Text == "draft B", "Background completion cannot clear selected draft");
            AgentSet(list, "_dragReorderSourceIndices", new List<int> { 1 });
            AgentSet(list, "_dragReorderInsertIndex", 0);
            Invoke(list, "执行多项拖动排序");
            Check(AgentMember<AgentConversationData>(form, "_current") == b && input.Text == "draft B", "Conversation drag order preserves selected draft identity");
            Check(RowColor(a) == list.ForeColor && RowColor(b) == green, "Running color follows its conversation after drag reorder");

            // Submit guidance only to B, while A retains its original unsent draft and attachment.
            input.Text = "guidance-B";
            Invoke(form, "OfferGuidanceMessage");
            Check(b.Messages.Any(x => x.Name == AgentConversationSchema.SteeringMessageName && x.Content == "guidance-B") &&
                !a.Messages.Any(x => x.Content == "guidance-B"), "Guidance must be owned by selected run");
            Check(input.Text == "" && b.DraftText == "" && a.DraftText.Contains("draft A") && a.DraftPaths.Count == 1,
                "Submitting clears only the submitted draft");
            Invoke(form, "AddSubmittedFiles", (object)new[] { attachment });
            Check(send.Text == "调整方向", "Attachment-only draft must not turn Send into Stop");
            Invoke(form, "OfferGuidanceMessage");
            Check(b.Messages.Last().Content.Contains(attachment) && b.DraftPaths.Count == 0, "Attachment-only guidance is submitted and cleared");
            input.Text = "next draft B";
            endpoint.Release("B");
            PumpAgentUntil(() => taskB.IsCompleted, "Uncancelled run must complete after release");
            taskB.GetAwaiter().GetResult();
            Check(b.Turns.Last().State == "completed" && b.Messages.Last().Content.Contains("reply-B-"), "Other conversation completes normally");
            Check(!b.Messages.Any(x => x.Content.Contains("reply-A-")), "Completed histories never mix responses");
            Check(input.Text == "next draft B" && send.Text == "发送", "Finishing preserves next draft and resets send button");
            Check(modelSelector.Enabled, "Execution settings remain enabled after completion");
            Check(RowColor(b) == list.ForeColor, "Successful completion restores default row color");
            Check(room.ToolCallExpandedMaxHeight == 200, "Tool detail height stays at 200");
            store.Save();
            Check(AgentConversationStore.Load(storePath).Conversations.Single(x => x.Id == b.Id).DraftText == "next draft B", "Post-run draft survives persistence");
            var savedA = AgentConversationStore.Load(storePath).Conversations.Single(x => x.Id == a.Id);
            Check(savedA.Turns.Last().Activities.Last(x => x.Kind == "tool").ResultText == fullReturn, "Long tool results survive conversation save/load");
            Invoke(form, "MB_新对话_Click", form, EventArgs.Empty);
            Check(input.Text == "" && files.Items.Count == 0, "New conversation starts with an empty draft");
            CheckSelectorsUnchanged("Creating a conversation");
            Select(b);
            Check(input.Text == "next draft B", "Creating a conversation preserves the previous draft");
        }
        finally
        {
            settings.AgentEndPoint = oldEndpoint;
            SynchronizationContext.SetSynchronizationContext(previousContext);
        }
    }

    private sealed class AgentStreamFixture : IDisposable
    {
        private readonly HttpListener listener = new();
        private readonly ConcurrentDictionary<string, TaskCompletionSource> gates = new();
        public readonly ConcurrentDictionary<string, bool> Started = new();
        public readonly ConcurrentBag<string> Requests = new();
        public string Endpoint { get; }

        public AgentStreamFixture()
        {
            var portProbe = new TcpListener(IPAddress.Loopback, 0);
            portProbe.Start();
            var port = ((IPEndPoint)portProbe.LocalEndpoint).Port;
            portProbe.Stop();
            Endpoint = $"http://127.0.0.1:{port}/v1";
            listener.Prefixes.Add($"http://127.0.0.1:{port}/");
            listener.Start();
            _ = Task.Run(async () =>
            {
                try
                {
                    while (listener.IsListening)
                    {
                        var context = await listener.GetContextAsync();
                        _ = Task.Run(() => Respond(context));
                    }
                }
                catch (Exception ex) when (ex is HttpListenerException or ObjectDisposedException) { }
            });
        }

        private async Task Respond(HttpListenerContext context)
        {
            try
            {
                var raw = await new StreamReader(context.Request.InputStream).ReadToEndAsync();
                Requests.Add(raw);
                using var doc = JsonDocument.Parse(raw);
                var name = doc.RootElement.GetProperty("messages").EnumerateArray()
                    .First(x => x.GetProperty("role").GetString() == "user").GetProperty("content").GetString()!;
                var gate = gates.GetOrAdd(name, _ => new(TaskCreationOptions.RunContinuationsAsynchronously));
                context.Response.ContentType = "text/event-stream";
                context.Response.SendChunked = true;
                using var writer = new StreamWriter(context.Response.OutputStream, new UTF8Encoding(false)) { AutoFlush = true };
                async Task Chunk(string text)
                {
                    var data = JsonSerializer.Serialize(new { choices = new[] { new { delta = new { content = text } } } });
                    await writer.WriteAsync("data: " + data + "\n\n");
                    await writer.FlushAsync();
                }
                await Chunk($"<think>thinking-{name}</think>reply-{name}-");
                if (name == "EOF") return;
                Started[name] = true;
                await gate.Task;
                await Chunk("done");
                await writer.WriteAsync("data: [DONE]\n\n");
            }
            catch (Exception ex) when (ex is IOException or HttpListenerException or ObjectDisposedException) { }
            finally { context.Response.Close(); }
        }

        public void Release(string name) => gates.GetOrAdd(name, _ => new(TaskCreationOptions.RunContinuationsAsynchronously)).TrySetResult();
        public void Dispose()
        {
            foreach (var gate in gates.Values) gate.TrySetResult();
            listener.Close();
        }
    }
}
