namespace AgentMemoryLab.Models {
    public class Message {
        public long Id { get; set; }
        public Guid ConversationId { get; set; }
        public string Role { get; set; } = "";
        public string Content { get; set; } = "";
        public DateTime CreatedAtUtc { get; set; }

        public Conversation Conversation { get; set; } = null!;
    }
}
