using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourDeApp.Migrations
{
    /// <inheritdoc />
    public partial class BoardJsonAdding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BoardJson",
                table: "Games",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BoardJson",
                table: "Games");
        }
    }
}
