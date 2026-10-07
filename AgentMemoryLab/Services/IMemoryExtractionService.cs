using AgentMemoryLab.Models;

namespace AgentMemoryLab.Services {
    public interface IMemoryExtractionService {
        Task<MemoryExtractionResult> ExtractAsync(string userMessage, IReadOnlyCollection<Memory> existingMemories);
    }
}
