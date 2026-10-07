namespace AgentMemoryLab.Models {
    public class MemoryHistory {
        public long Id { get; set; }
        public long MemoryId { get; set; }
        public string Value { get; set; } = "";
        public long? SourceMessageId { get; set; }
        public string Operation { get; set; } = "";
        public DateTime ChangedAtUtc { get; set; }


        public Memory Memory { get; set; } = null!;
        public Message? SourceMessage { get; set; }
    }
}
