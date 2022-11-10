using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GambleMaticDataLib.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    GameModelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameDay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Home = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Away = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ResultInt = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.GameModelId);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    PlayerModelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ShortName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Paid = table.Column<int>(type: "int", nullable: false),
                    Ticket = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.PlayerModelId);
                });

            migrationBuilder.CreateTable(
                name: "Gambles",
                columns: table => new
                {
                    GambleItemModelId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    GameModelId = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    PlayerModelId = table.Column<int>(type: "int", nullable: false),
                    Guess = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gambles", x => x.GambleItemModelId);
                    table.ForeignKey(
                        name: "FK_Gambles_Games_GameModelId",
                        column: x => x.GameModelId,
                        principalTable: "Games",
                        principalColumn: "GameModelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Gambles_Players_PlayerModelId",
                        column: x => x.PlayerModelId,
                        principalTable: "Players",
                        principalColumn: "PlayerModelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Gambles_GameModelId",
                table: "Gambles",
                column: "GameModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Gambles_PlayerModelId",
                table: "Gambles",
                column: "PlayerModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Gambles");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Players");
        }
    }
}
