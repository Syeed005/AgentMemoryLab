using AgentMemoryLab.Models;
using OpenAI.Chat;
using System.Text.Json;

namespace AgentMemoryLab.Services {
    public class MemorySelectionService(ChatClient chatClient) : IMemorySelectionService {
        public async Task<MemorySelectionResult> SelectAsync(string userMessage, IReadOnlyCollection<Memory> memories) {
            if (memories.Count == 0)
                return new MemorySelectionResult();

            var availableMemories = string.Join("\n", memories.Select(m => $"{m.Key}: {m.Value}"));

            var prompt = $$"""
            Select only the memories relevant to answering the user's current message.

            Available memories:
            {{availableMemories}}

            User message:
            {{userMessage}}


            Rules:
            - Select only keys from the available memories.
            - Select only memories useful for answering the current message.
            - Do not invent keys.
            - Select no keys when no memory is relevant.
            
            Memory scope rules:
            - "user" memories are general information that applies across conversations.
            - "conversation" memories apply specifically to the current conversation or project.
            - When both scopes contain the same key, prefer the conversation-scoped memory for questions about the current conversation, project, or task.
            - Prefer the user-scoped memory when the user explicitly asks about their general preference.
            - Select only memories relevant to the current question.
            """;

            var jsonSchema = BinaryData.FromString("""
            {
              "type": "object",
              "properties": {
                "keys": {
                  "type": "array",
                  "items": {
                    "type": "string"
                  }
                }
              },
              "required": ["keys"],
              "additionalProperties": false
            }
            """);

            var options = new ChatCompletionOptions {
                ResponseFormat = ChatResponseFormat.CreateJsonSchemaFormat(
                jsonSchemaFormatName: "memory_selection",
                jsonSchema: jsonSchema,
                jsonSchemaIsStrict: true)
            };

            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            var response = await chatClient.CompleteChatAsync(new ChatMessage[] { new UserChatMessage(prompt) }, options);
            stopwatch.Stop();
           
            //We now have graceful degradation for malformed JSON
            try {
                var result = JsonSerializer.Deserialize<MemorySelectionResult>(response.Value.Content[0].Text, new JsonSerializerOptions {
                    PropertyNameCaseInsensitive = true
                }) ?? new MemorySelectionResult();

                result.InputTokens = response.Value.Usage.InputTokenCount;
                result.OutputTokens = response.Value.Usage.OutputTokenCount;
                result.TotalTokens = response.Value.Usage.TotalTokenCount;
                result.LatencyMs = stopwatch.ElapsedMilliseconds;

                return result;
            } catch (JsonException) {
                return new MemorySelectionResult {
                    InputTokens = response.Value.Usage.InputTokenCount,
                    OutputTokens = response.Value.Usage.OutputTokenCount,
                    TotalTokens = response.Value.Usage.TotalTokenCount,
                    LatencyMs = stopwatch.ElapsedMilliseconds
                };
            }
        }
    }
}
