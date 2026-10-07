namespace AgentMemoryLab.Models {
    public class MemoryCandidateService : IMemoryCandidateService {
        public IReadOnlyCollection<Memory> Reduce(string userMessage, IReadOnlyCollection<Memory> memories) {
            var message = userMessage.ToLowerInvariant();

            if (ContainsAny(message, "cloud", "azure", "aws", "database", "sql", "programming", "language", "ide", "visual studio", "rider", "api", "testing", "source control", "git"))
                return memories.Where(m => m.Category == "Technology").ToList();

            if (ContainsAny(message, "explain", "explanation", "response", "respond", "concise", "detailed", "format", "style"))
                return memories.Where(m => m.Category == "Communication").ToList();

            return memories.ToList();
        }

        private static bool ContainsAny(string text, params string[] terms) {
            return terms.Any(text.Contains);
        }
    }
}
