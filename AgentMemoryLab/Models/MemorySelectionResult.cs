namespace AgentMemoryLab.Models {
    public class MemorySelectionResult {
        public List<string> Keys { get; set; } = [];

        public int InputTokens { get; set; }
        public int OutputTokens { get; set; }
        public int TotalTokens { get; set; }
        public long LatencyMs { get; set; }
    }
}
