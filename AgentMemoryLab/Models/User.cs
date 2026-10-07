namespace AgentMemoryLab.Models {
    public class User {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public DateTime CreatedAtUtc { get; set; }

        public List<Conversation> Conversations { get; set; } = [];
        public List<Memory> Memories { get; set; } = [];
    }
}
