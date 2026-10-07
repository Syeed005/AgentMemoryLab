using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgentMemoryLab.Migrations
{
    /// <inheritdoc />
    public partial class AddMemoryCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Memories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "General");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Memories");
        }
    }
}
