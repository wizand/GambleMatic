using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GambleMaticDataLib.Migrations
{
    public partial class Adding_extra_games : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GameOrderSortId",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ExtraGambles",
                columns: table => new
                {
                    ExtraGamblesModelId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SemifinalTeamOne = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    SemifinalTeamTwo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    SemifinalTeamThree = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    SemifinalTeamFour = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    SilverTeam = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    GoldTeam = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    GoalsInTournamen = table.Column<int>(type: "int", nullable: false),
                    PlayerModelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtraGambles", x => x.ExtraGamblesModelId);
                    table.ForeignKey(
                        name: "FK_ExtraGambles_Players_PlayerModelId",
                        column: x => x.PlayerModelId,
                        principalTable: "Players",
                        principalColumn: "PlayerModelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExtraGambles_PlayerModelId",
                table: "ExtraGambles",
                column: "PlayerModelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Games_GameOrderSortId",
                table: "Games",
                column: "GameOrderSortId",
                unique: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExtraGambles");

            migrationBuilder.DropColumn(
                name: "GameOrderSortId",
                table: "Games");
        }
    }
}
