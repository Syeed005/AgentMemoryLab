using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgentMemoryLab.Migrations
{
    /// <inheritdoc />
    public partial class AddMemoryScope : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ConversationId",
                table: "Memories",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Scope",
                table: "Memories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "user");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConversationId",
                table: "Memories");

            migrationBuilder.DropColumn(
                name: "Scope",
                table: "Memories");
        }
    }
}
