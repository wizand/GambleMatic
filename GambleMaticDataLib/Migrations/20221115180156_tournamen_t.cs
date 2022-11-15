using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GambleMaticDataLib.Migrations
{
    public partial class tournamen_t : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GoalsInTournamen",
                table: "ExtraGambles");

            migrationBuilder.AddColumn<string>(
                name: "GoalsInTournament",
                table: "ExtraGambles",
                type: "nvarchar(16)",
                maxLength: 16,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GoalsInTournament",
                table: "ExtraGambles");

            migrationBuilder.AddColumn<string>(
                name: "GoalsInTournamen",
                table: "ExtraGambles",
                type: "nvarchar(16)",
                nullable: false,
                defaultValue: "");
        }
    }
}
