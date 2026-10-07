using AgentMemoryLab.Models;
using OpenAI.Chat;
using System.Text.Json;

namespace AgentMemoryLab.Services {
    public class MemoryExtractionService(ChatClient chatClient) : IMemoryExtractionService {
        public async Task<MemoryExtractionResult> ExtractAsync(string userMessage, IReadOnlyCollection<Memory> existingMemories) {
            
            var existingMemoryText = existingMemories.Count == 0 ? "No existing memories." : string.Join("\n", existingMemories.Select(m => $"- {m.Key}: {m.Value}"));

            var prompt = $$"""
            You manage durable memory for an AI assistant.
            
            Determine whether the user's new message contains durable information worth remembering.
            
            Existing memories:
            {{existingMemoryText}}
            
            Rules:
            - Remember durable identity information, preferences, and recurring constraints.
            - Do not remember ordinary questions or temporary information.
            - If updating an existing memory, reuse the EXACT existing key.
            - Do not create a synonymous key for an existing memory.
            - A temporary or project-specific consideration does not automatically replace a general preference.
            - Return no memories when nothing should be stored.
            - Use "upsert" when creating a new durable memory or updating an existing memory.
            - Use "delete" only when the user explicitly asks to forget, remove, or stop remembering an existing memory.
            - For delete, reuse the EXACT existing memory key.
            - For delete, set value to an empty string.
            - Do not interpret a temporary exception or project-specific preference as a delete.
            - Do not delete a memory merely because the user expresses uncertainty.
            - Use "user" for durable information that generally applies to the user across conversations.
            - Use "conversation" for information explicitly limited to the current project, task, situation, or conversation.
            - A conversation-scoped fact must not overwrite a general user-scoped memory.
            - A user-scoped fact must not automatically replace a conversation-specific fact.
            - When updating or deleting an existing memory, preserve the appropriate existing scope.

            Memory category:
            - Technology: programming languages, cloud platforms, databases, IDEs, development tools, technical preferences.
            - Communication: response style, explanation style, formatting, language preferences.
            - Work: durable professional/work preferences or constraints.
            - Project: project-related information that does not fit another category.
            - Personal: durable non-work personal preferences or information.
            - General: use only when no more specific category applies.
            
            User message:
            {{userMessage}}
                        
            """;

            var jsonSchema = BinaryData.FromString("""
            {
              "type": "object",
              "properties": {
                "memories": {
                  "type": "array",
                  "items": {
                    "type": "object",
                    "properties": {
                      "operation": {
                        "type": "string",
                        "enum": ["upsert", "delete"]
                      },
                      "scope": {
                        "type": "string",
                        "enum": ["user", "conversation"]
                      },
                      "key": {
                        "type": "string"
                      },
                      "value": {
                        "type": "string"
                      },
                      "category": {
                        "type": "string",
                        "enum": [
                          "Technology",
                          "Communication",
                          "Personal",
                          "Work",
                          "Project",
                          "General"
                        ]
                      }
                    },
                    "required": ["operation", "scope", "category", "key", "value"],
                    "additionalProperties": false
                  }
                }
              },
              "required": ["memories"],
              "additionalProperties": false
            }
            """);

            var options = new ChatCompletionOptions {
                ResponseFormat = ChatResponseFormat.CreateJsonSchemaFormat(
                    jsonSchemaFormatName: "memory_extraction",
                    jsonSchema: jsonSchema,
                    jsonSchemaIsStrict: true)
                };

            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            var response = await chatClient.CompleteChatAsync(new ChatMessage[] { new UserChatMessage(prompt) }, options);
            stopwatch.Stop();

            //We now have graceful degradation for malformed JSON
            try {
                var result = JsonSerializer.Deserialize<MemoryExtractionResult>(response.Value.Content[0].Text, new JsonSerializerOptions {
                    PropertyNameCaseInsensitive = true
                }) ?? new MemoryExtractionResult();

                result.InputTokens = response.Value.Usage.InputTokenCount;
                result.OutputTokens = response.Value.Usage.OutputTokenCount;
                result.TotalTokens = response.Value.Usage.TotalTokenCount;
                result.LatencyMs = stopwatch.ElapsedMilliseconds;

                return result;
            } catch (JsonException) {
                return new MemoryExtractionResult {
                    InputTokens = response.Value.Usage.InputTokenCount,
                    OutputTokens = response.Value.Usage.OutputTokenCount,
                    TotalTokens = response.Value.Usage.TotalTokenCount,
                    LatencyMs = stopwatch.ElapsedMilliseconds
                };
            }
        }
    }
}
