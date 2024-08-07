using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GambleMaticDataLib.Migrations
{
    public partial class extragambles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ExtraGambles_PlayerModelId",
                table: "ExtraGambles");

            migrationBuilder.CreateIndex(
                name: "IX_ExtraGambles_PlayerModelId",
                table: "ExtraGambles",
                column: "PlayerModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ExtraGambles_PlayerModelId",
                table: "ExtraGambles");

            migrationBuilder.CreateIndex(
                name: "IX_ExtraGambles_PlayerModelId",
                table: "ExtraGambles",
                column: "PlayerModelId",
                unique: true,
                filter: "[PlayerModelId] IS NOT NULL");
        }
    }
}
