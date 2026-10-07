using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgentMemoryLab.Migrations
{
    /// <inheritdoc />
    public partial class AddMemoryHistorySourceMessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_MemoryHistory_SourceMessageId",
                table: "MemoryHistory",
                column: "SourceMessageId");

            migrationBuilder.AddForeignKey(
                name: "FK_MemoryHistory_Messages_SourceMessageId",
                table: "MemoryHistory",
                column: "SourceMessageId",
                principalTable: "Messages",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MemoryHistory_Messages_SourceMessageId",
                table: "MemoryHistory");

            migrationBuilder.DropIndex(
                name: "IX_MemoryHistory_SourceMessageId",
                table: "MemoryHistory");
        }
    }
}
