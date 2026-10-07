namespace AgentMemoryLab.Models {
    public class MemoryExtractionResult {
        public List<ExtractedMemory> Memories { get; set; } = [];

        public int InputTokens { get; set; }
        public int OutputTokens { get; set; }
        public int TotalTokens { get; set; }
        public long LatencyMs { get; set; }
    }

    public class ExtractedMemory {
        public string Category { get; set; } = "General";
        public string Operation { get; set; } = "";
        public string Scope { get; set; } = "";
        public string Key { get; set; } = "";
        public string Value { get; set; } = "";
    }
}
