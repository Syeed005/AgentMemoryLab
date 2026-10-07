using AgentMemoryLab.Models;
using Microsoft.EntityFrameworkCore;

namespace AgentMemoryLab.Data {
    public class Lab02DbContext(DbContextOptions<Lab02DbContext> options) : DbContext(options) {
        public DbSet<Conversation> Conversations => Set<Conversation>();
        public DbSet<Message> Messages => Set<Message>();
        public DbSet<Memory> Memories => Set<Memory>();
        public DbSet<User> Users => Set<User>();
        public DbSet<MemoryHistory> MemoryHistory => Set<MemoryHistory>();

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Memory>()
                .HasIndex(m => new { m.UserId, m.Key })
                .HasFilter("[Scope] = 'user'")
                .IsUnique();

            modelBuilder.Entity<Memory>()
                .HasIndex(m => new { m.UserId, m.ConversationId, m.Key })
                .HasFilter("[Scope] = 'conversation'")
                .IsUnique();
        }
    }
}
