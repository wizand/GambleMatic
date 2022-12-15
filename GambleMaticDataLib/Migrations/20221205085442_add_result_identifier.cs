using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GambleMaticDataLib.Migrations
{
    public partial class add_result_identifier : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExtraGambles_Players_PlayerModelId",
                table: "ExtraGambles");

            migrationBuilder.DropIndex(
                name: "IX_ExtraGambles_PlayerModelId",
                table: "ExtraGambles");

            migrationBuilder.AlterColumn<int>(
                name: "PlayerModelId",
                table: "ExtraGambles",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "IsResultObject",
                table: "ExtraGambles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_ExtraGambles_PlayerModelId",
                table: "ExtraGambles",
                column: "PlayerModelId",
                unique: true,
                filter: "[PlayerModelId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_ExtraGambles_Players_PlayerModelId",
                table: "ExtraGambles",
                column: "PlayerModelId",
                principalTable: "Players",
                principalColumn: "PlayerModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExtraGambles_Players_PlayerModelId",
                table: "ExtraGambles");

            migrationBuilder.DropIndex(
                name: "IX_ExtraGambles_PlayerModelId",
                table: "ExtraGambles");

            migrationBuilder.DropColumn(
                name: "IsResultObject",
                table: "ExtraGambles");

            migrationBuilder.AlterColumn<int>(
                name: "PlayerModelId",
                table: "ExtraGambles",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExtraGambles_PlayerModelId",
                table: "ExtraGambles",
                column: "PlayerModelId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ExtraGambles_Players_PlayerModelId",
                table: "ExtraGambles",
                column: "PlayerModelId",
                principalTable: "Players",
                principalColumn: "PlayerModelId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
