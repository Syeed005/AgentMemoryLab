using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgentMemoryLab.Migrations
{
    /// <inheritdoc />
    public partial class AddConversationSummary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Summary",
                table: "Conversations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SummaryThroughMessageId",
                table: "Conversations",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SummaryUpdatedAtUtc",
                table: "Conversations",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Summary",
                table: "Conversations");

            migrationBuilder.DropColumn(
                name: "SummaryThroughMessageId",
                table: "Conversations");

            migrationBuilder.DropColumn(
                name: "SummaryUpdatedAtUtc",
                table: "Conversations");
        }
    }
}
