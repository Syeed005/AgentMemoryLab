namespace AgentMemoryLab.Models {
    public interface IConversationSummaryService {
        Task<ConversationSummaryResult> SummarizeAsync(string? existingSummary, IReadOnlyCollection<Message> messages);
    }
}
