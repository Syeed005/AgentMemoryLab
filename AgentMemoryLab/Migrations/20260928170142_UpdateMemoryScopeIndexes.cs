using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgentMemoryLab.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMemoryScopeIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Memories_UserId_Key",
                table: "Memories");

            migrationBuilder.CreateIndex(
                name: "IX_Memories_UserId_ConversationId_Key",
                table: "Memories",
                columns: new[] { "UserId", "ConversationId", "Key" },
                unique: true,
                filter: "[Scope] = 'conversation'");

            migrationBuilder.CreateIndex(
                name: "IX_Memories_UserId_Key",
                table: "Memories",
                columns: new[] { "UserId", "Key" },
                unique: true,
                filter: "[Scope] = 'user'");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Memories_UserId_ConversationId_Key",
                table: "Memories");

            migrationBuilder.DropIndex(
                name: "IX_Memories_UserId_Key",
                table: "Memories");

            migrationBuilder.CreateIndex(
                name: "IX_Memories_UserId_Key",
                table: "Memories",
                columns: new[] { "UserId", "Key" },
                unique: true);
        }
    }
}
