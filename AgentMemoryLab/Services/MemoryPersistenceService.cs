using AgentMemoryLab.Data;
using AgentMemoryLab.Models;
using Microsoft.EntityFrameworkCore;

namespace AgentMemoryLab.Services {
    public class MemoryPersistenceService : IMemoryPersistenceService {
        private readonly Lab02DbContext db;

        public MemoryPersistenceService(Lab02DbContext db) {
            this.db = db;
        }

        public async Task ApplyAsync(Conversation conversation, Message sourceMessage, IReadOnlyCollection<ExtractedMemory> extractedMemories) {
            foreach (var extracted in extractedMemories) {

                if (string.IsNullOrWhiteSpace(extracted.Key))
                    continue;

                if (!extracted.Scope.Equals("user", StringComparison.OrdinalIgnoreCase) &&
                    !extracted.Scope.Equals("conversation", StringComparison.OrdinalIgnoreCase))
                    continue;

                var existingMemory = extracted.Scope.Equals("conversation", StringComparison.OrdinalIgnoreCase)
                    ? await db.Memories.FirstOrDefaultAsync(m =>
                        m.UserId == conversation.UserId &&
                        m.Key == extracted.Key &&
                        m.Scope == "conversation" &&
                        m.ConversationId == conversation.Id)
                    : await db.Memories.FirstOrDefaultAsync(m =>
                        m.UserId == conversation.UserId &&
                        m.Key == extracted.Key &&
                        m.Scope == "user");

                // DELETE / FORGET
                if (extracted.Operation.Equals("delete", StringComparison.OrdinalIgnoreCase)) {
                    if (existingMemory is not null && existingMemory.IsActive) {
                        existingMemory.IsActive = false;
                        existingMemory.UpdatedAtUtc = DateTime.UtcNow;

                        db.MemoryHistory.Add(new MemoryHistory {
                            MemoryId = existingMemory.Id,
                            Value = existingMemory.Value,
                            Operation = "delete",
                            SourceMessage = sourceMessage,
                            ChangedAtUtc = DateTime.UtcNow
                        });
                    }

                    continue;
                }

                // Only UPSERT is allowed beyond this point
                if (!extracted.Operation.Equals("upsert", StringComparison.OrdinalIgnoreCase))
                    continue;

                if (string.IsNullOrWhiteSpace(extracted.Value))
                    continue;

                // CREATE
                if (existingMemory is null) {
                    var newMemory = new AgentMemoryLab.Models.Memory {
                        UserId = conversation.UserId,
                        Key = extracted.Key,
                        Value = extracted.Value,
                        Scope = extracted.Scope,
                        ConversationId = extracted.Scope.Equals("conversation", StringComparison.OrdinalIgnoreCase) ? conversation.Id : null,
                        IsActive = true,
                        Category = extracted.Category,
                        CreatedAtUtc = DateTime.UtcNow,
                        UpdatedAtUtc = DateTime.UtcNow
                    };

                    db.Memories.Add(newMemory);

                    db.MemoryHistory.Add(new MemoryHistory {
                        Memory = newMemory,
                        Value = extracted.Value,
                        Operation = "upsert",
                        SourceMessage = sourceMessage,
                        ChangedAtUtc = DateTime.UtcNow
                    });
                }
                // REACTIVATE OR UPDATE
                else if (!existingMemory.IsActive || !string.Equals(existingMemory.Value, extracted.Value, StringComparison.OrdinalIgnoreCase)) {
                    existingMemory.Value = extracted.Value;
                    existingMemory.IsActive = true;
                    existingMemory.UpdatedAtUtc = DateTime.UtcNow;

                    db.MemoryHistory.Add(new MemoryHistory {
                        MemoryId = existingMemory.Id,
                        Value = extracted.Value,
                        Operation = "upsert",
                        SourceMessage = sourceMessage,
                        ChangedAtUtc = DateTime.UtcNow
                    });
                }
            }
        }
    }
}
