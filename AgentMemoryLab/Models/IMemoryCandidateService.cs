namespace AgentMemoryLab.Models {
    public interface IMemoryCandidateService {
        IReadOnlyCollection<Memory> Reduce(string userMessage, IReadOnlyCollection<Memory> memories);
    }
}
