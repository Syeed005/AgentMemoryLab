using AgentMemoryLab.Models;

namespace AgentMemoryLab.Services {
    public interface IMemoryPersistenceService {
        Task ApplyAsync(Conversation conversation, Message sourceMessage, IReadOnlyCollection<ExtractedMemory> extractedMemories);
    }
}
