using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GambleMaticDataLib.Migrations
{
    /// <inheritdoc />
    public partial class Anothertest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GameId",
                table: "Gambles");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "Gambles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GameId",
                table: "Gambles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PlayerId",
                table: "Gambles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
