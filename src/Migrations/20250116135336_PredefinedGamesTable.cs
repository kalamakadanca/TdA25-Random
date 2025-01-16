using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourDeApp.Migrations
{
    /// <inheritdoc />
    public partial class PredefinedGamesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CellDb");

            migrationBuilder.DropTable(
                name: "GameBoardDb");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Games",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "GameBoardId",
                table: "Games");

            migrationBuilder.RenameTable(
                name: "Games",
                newName: "GameDb");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameDb",
                table: "GameDb",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_GameDb",
                table: "GameDb");

            migrationBuilder.RenameTable(
                name: "GameDb",
                newName: "Games");

            migrationBuilder.AddColumn<int>(
                name: "GameBoardId",
                table: "Games",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Games",
                table: "Games",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "GameBoardDb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GameId = table.Column<int>(type: "INTEGER", nullable: false),
                    Size = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameBoardDb", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameBoardDb_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CellDb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GameBoardDbId = table.Column<int>(type: "INTEGER", nullable: false),
                    Column = table.Column<int>(type: "INTEGER", nullable: false),
                    Row = table.Column<int>(type: "INTEGER", nullable: false),
                    State = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CellDb", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CellDb_GameBoardDb_GameBoardDbId",
                        column: x => x.GameBoardDbId,
                        principalTable: "GameBoardDb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CellDb_GameBoardDbId",
                table: "CellDb",
                column: "GameBoardDbId");

            migrationBuilder.CreateIndex(
                name: "IX_GameBoardDb_GameId",
                table: "GameBoardDb",
                column: "GameId",
                unique: true);
        }
    }
}
