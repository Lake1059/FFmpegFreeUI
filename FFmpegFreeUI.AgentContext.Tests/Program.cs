using System.Text.Json;
using FFmpegFreeUI;

var root = Path.Combine(Path.GetTempPath(), "3fui-agent-context-tests", Guid.NewGuid().ToString("N"));
Directory.CreateDirectory(root);

try
{
    var store = AgentConversationStore.Load(root);
    var conversation = new AgentConversationData { Id = "context-test", Title = "完整记录" };
    var first = new AgentMessageData { Id = "m1", Role = "user", Content = "第一条" };
    var second = new AgentMessageData { Id = "m2", Role = "assistant", Content = "第二条" };
    var third = new AgentMessageData { Id = "m3", Role = "user", Content = "第三条" };
    var fourth = new AgentMessageData { Id = "m4", Role = "assistant", Content = "第四条" };
    conversation.Messages.AddRange([first, second, third, fourth]);
    conversation.ContextCheckpoint = new AgentContextCheckpointData
    {
        ConversationId = conversation.Id,
        Summary = "前两条的摘要",
        CoveredThroughMessageId = second.Id,
        CoveredMessageCountSnapshot = 2,
        ModelId = "compact-model"
    };
    store.Conversations.Add(conversation);
    store.Save();

    var conversationPath = Path.Combine(root, "Conversations", "context-test.json");
    var checkpointPath = Path.Combine(root, "Contexts", "context-test.context.json");
    Assert(File.Exists(conversationPath), "会话文件未创建");
    Assert(File.Exists(checkpointPath), "独立检查点文件未创建");

    using (var json = JsonDocument.Parse(File.ReadAllText(conversationPath)))
    {
        var rootElement = json.RootElement;
        Assert(rootElement.GetProperty("Messages").GetArrayLength() == 4, "可见会话没有保留全部消息");
        Assert(!rootElement.TryGetProperty("ContextCheckpoint", out _), "检查点不应写入会话文件");
        Assert(!rootElement.TryGetProperty("ContextSummary", out _), "会话文件不应保存旧摘要字段");
    }

    var reloaded = AgentConversationStore.Load(root).Conversations.Single();
    Assert(reloaded.Messages.Count == 4, "重载后完整消息丢失");
    Assert(reloaded.ContextCheckpoint?.CoveredThroughMessageId == "m2", "独立检查点未正确重载");

    var projected = AgentContextProjection.BuildModelContext(reloaded.Messages, reloaded.ContextCheckpoint);
    Assert(projected.Count == 3, "模型上下文应由一个检查点和两条后续消息组成");
    Assert(projected[0].Role == "system" && projected[0].Content.Contains("前两条的摘要"), "模型上下文缺少摘要前缀");
    Assert(projected[1].Id == "m3" && projected[2].Id == "m4", "模型上下文没有从稳定边界后继续");
    Assert(reloaded.Messages.Count == 4, "构造模型上下文不应修改显示记录");

    reloaded.ContextCheckpoint = new AgentContextCheckpointData
    {
        ConversationId = reloaded.Id,
        Summary = "前三条的新摘要",
        CoveredThroughMessageId = third.Id,
        CoveredMessageCountSnapshot = 3
    };
    projected = AgentContextProjection.BuildModelContext(reloaded.Messages, reloaded.ContextCheckpoint);
    Assert(projected.Count == 2 && projected[1].Id == "m4", "重复压缩没有推进稳定消息边界");

    reloaded.Messages.RemoveAll(message => message.Id is "m3" or "m4");
    var reloadedStore = AgentConversationStore.Load(root);
    reloadedStore.Conversations.Single().Messages.RemoveAll(message => message.Id is "m3" or "m4");
    reloadedStore.Conversations.Single().ContextCheckpoint = reloaded.ContextCheckpoint;
    reloadedStore.Save();
    Assert(!File.Exists(checkpointPath), "回退越过边界后应删除失效检查点");
    Assert(AgentContextProjection.BuildModelContext(reloaded.Messages, reloaded.ContextCheckpoint).Count == 2,
        "边界失效时必须退回完整原始上下文");

    reloadedStore.Conversations.Clear();
    reloadedStore.Save();
    Assert(!File.Exists(conversationPath), "删除会话后残留了会话文件");
    Assert(!File.Exists(checkpointPath), "删除会话后残留了检查点文件");

    var legacyRoot = Path.Combine(root, "legacy-case");
    var legacyConversationDirectory = Path.Combine(legacyRoot, "Conversations");
    Directory.CreateDirectory(legacyConversationDirectory);
    var legacyPath = Path.Combine(legacyConversationDirectory, "legacy.json");
    File.WriteAllText(legacyPath, """
        {
          "Id": "legacy-conversation",
          "Title": "旧版对话",
          "Messages": [
            { "Role": "user", "Content": "读取旧数据", "CreatedAt": "2026-01-01T10:00:00" },
            {
              "Role": "assistant",
              "Content": "正在读取",
              "CreatedAt": "2026-01-01T10:00:01",
              "ToolCalls": [
                { "Id": "call-1", "Name": "read_file", "Arguments": "{\"path\":\"a.txt\"}" }
              ]
            },
            { "Role": "tool", "Name": "read_file", "ToolCallId": "call-1", "Content": "文件内容", "CreatedAt": "2026-01-01T10:00:02" },
            { "Role": "assistant", "Content": "读取完成", "CreatedAt": "2026-01-01T10:00:03" }
          ],
          "ContextSummary": "旧摘要",
          "ContextSummaryMessageCount": 1,
          "ContextSummaryModelId": "legacy-compact",
          "ContextSummaryUpdatedAt": "2026-01-01T10:00:04"
        }
        """);

    var upgradedStore = AgentConversationStore.Load(legacyRoot);
    var upgraded = upgradedStore.Conversations.Single();
    Assert(upgraded.Version == AgentConversationSchema.LatestVersion, "旧会话没有升级到最新版本");
    Assert(upgraded.Messages.All(message => !string.IsNullOrWhiteSpace(message.Id)), "升级后消息必须具有稳定 ID");
    Assert(upgraded.Turns.Count == 1, "旧会话应生成单次工作记录");
    Assert(upgraded.Turns[0].Activities.Any(activity => activity.Kind == "tool" && activity.ResultText == "文件内容"),
        "旧工具调用没有迁移到工作记录");
    Assert(upgraded.Messages[1].Name == AgentConversationSchema.ActivityMessageName,
        "旧工具调用助手消息没有标记为过程消息");
    Assert(upgraded.ContextCheckpoint?.Summary == "旧摘要", "旧内嵌摘要没有迁移为检查点");

    var upgradedConversationPath = Path.Combine(legacyConversationDirectory, "legacy-conversation.json");
    var upgradedCheckpointPath = Path.Combine(legacyRoot, "Contexts", "legacy-conversation.context.json");
    Assert(File.Exists(upgradedConversationPath), "升级后没有生成最新会话文件");
    Assert(File.Exists(upgradedCheckpointPath), "升级后没有生成独立检查点文件");
    Assert(!File.Exists(legacyPath), "升级后残留旧文件名");
    using (var upgradedJson = JsonDocument.Parse(File.ReadAllText(upgradedConversationPath)))
    {
        Assert(!upgradedJson.RootElement.TryGetProperty("ContextSummary", out _), "升级后会话仍包含旧摘要字段");
        Assert(upgradedJson.RootElement.GetProperty("Version").GetInt32() == AgentConversationSchema.LatestVersion,
            "升级后的文件版本号不正确");
    }

    var steeringUpgrade = AgentConversationJsonUpgrader.Upgrade("""
        {
          "Version": 3,
          "Id": "steering-upgrade",
          "Messages": [
            { "Id": "main-user", "Role": "user", "Content": "先检查文件" },
            { "Id": "steering-user", "Role": "user", "Name": "steer", "Content": "调整方向：\r\n只检查视频" }
          ],
          "Turns": [
            {
              "Id": "turn-1",
              "UserMessageId": "main-user",
              "Activities": [
                { "Id": "old-guidance", "Kind": "guidance", "Title": "调整方向", "Content": "只检查视频" }
              ]
            }
          ]
        }
        """);
    Assert(steeringUpgrade?.Conversation.Version == AgentConversationSchema.LatestVersion,
        "方向消息会话没有升级到最新版本");
    Assert(steeringUpgrade?.Conversation.Messages[1].Content == "只检查视频",
        "方向消息仍带有旧显示前缀");
    Assert(steeringUpgrade?.Conversation.Turns[0].Activities[0].Id == "steering-user",
        "方向活动没有关联到对应的用户消息");
    Assert(string.IsNullOrEmpty(steeringUpgrade?.Conversation.Turns[0].Activities[0].Title),
        "方向活动仍保留旧标题");

    var recoveryRoot = Path.Combine(root, "storage-recovery-case");
    var recoveryConversationDirectory = Path.Combine(recoveryRoot, "Conversations");
    Directory.CreateDirectory(recoveryConversationDirectory);
    File.WriteAllText(Path.Combine(recoveryRoot, "Conversations.index.json"), "{ invalid index");
    File.WriteAllText(Path.Combine(recoveryConversationDirectory, "recoverable-source.json"), JsonSerializer.Serialize(
        new AgentConversationData
        {
            Version = AgentConversationSchema.LatestVersion,
            Id = "recoverable",
            Title = "可恢复会话",
            Messages = [new AgentMessageData { Id = "recoverable-message", Role = "user", Content = "保留我" }]
        }));
    var damagedConversationPath = Path.Combine(recoveryConversationDirectory, "damaged-history.json");
    File.WriteAllText(damagedConversationPath, "{ damaged conversation");

    var recoveredStore = AgentConversationStore.Load(recoveryRoot);
    Assert(recoveredStore.Conversations.Count == 1, "索引损坏时没有从会话目录恢复有效记录");
    Assert(recoveredStore.Conversations[0].Messages.Single().Content == "保留我", "目录恢复后的会话内容不正确");
    recoveredStore.Save();
    Assert(File.Exists(damagedConversationPath), "恢复后的首次保存删除了无法读取的历史文件");

    var recoveredAgain = AgentConversationStore.Load(recoveryRoot);
    Assert(recoveredAgain.Conversations.Any(item => item.Id == "recoverable"), "有效索引存在时没有合并目录扫描结果");
    recoveredAgain.Save();
    Assert(File.Exists(damagedConversationPath), "跨重启保存删除了仍无法读取的历史文件");

    Console.WriteLine("Agent context tests passed: 36 assertions.");
}
finally
{
    if (Directory.Exists(root)) Directory.Delete(root, recursive: true);
}

static void Assert(bool condition, string message)
{
    if (!condition) throw new InvalidOperationException(message);
}
