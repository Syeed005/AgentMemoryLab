using AgentMemoryLab.Models;

namespace AgentMemoryLab.Services {
    public interface IMemorySelectionService {
        Task<MemorySelectionResult> SelectAsync(string userMessage, IReadOnlyCollection<Memory> memories);
    }
}
