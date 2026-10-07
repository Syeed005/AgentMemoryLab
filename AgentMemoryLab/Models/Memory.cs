namespace AgentMemoryLab.Models {
    public class Memory {
        public long Id { get; set; }
        public Guid UserId { get; set; }
        public string Category { get; set; } = "General";
        public string Key { get; set; } = "";
        public string Value { get; set; } = "";
        public bool IsActive { get; set; } = true;
        public string Scope { get; set; } = "user";
        public Guid? ConversationId { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public DateTime UpdatedAtUtc { get; set; }

        public User User { get; set; } = null!;
        public List<MemoryHistory> History { get; set; } = [];
    }
}
