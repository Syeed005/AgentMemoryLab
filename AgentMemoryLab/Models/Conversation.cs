namespace AgentMemoryLab.Models {
    public class Conversation {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public string? Summary { get; set; }

        public DateTime? SummaryUpdatedAtUtc { get; set; }
        public long? SummaryThroughMessageId { get; set; }
        public User User { get; set; } = null!;
        public List<Message> Messages { get; set; } = [];
    }
}
