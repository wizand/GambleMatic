using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GambleMaticDataLib.Migrations
{
    /// <inheritdoc />
    public partial class Addingnav3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gambles_Games_GameModelId",
                table: "Gambles");

            migrationBuilder.AlterColumn<int>(
                name: "GameModelId",
                table: "Gambles",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Gambles_Games_GameModelId",
                table: "Gambles",
                column: "GameModelId",
                principalTable: "Games",
                principalColumn: "GameModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gambles_Games_GameModelId",
                table: "Gambles");

            migrationBuilder.AlterColumn<int>(
                name: "GameModelId",
                table: "Gambles",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Gambles_Games_GameModelId",
                table: "Gambles",
                column: "GameModelId",
                principalTable: "Games",
                principalColumn: "GameModelId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
