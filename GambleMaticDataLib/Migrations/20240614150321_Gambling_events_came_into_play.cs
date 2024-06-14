using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GambleMaticDataLib.Migrations
{
    public partial class Gambling_events_came_into_play : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GamblingEventId",
                table: "Games",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GamblingEventId",
                table: "ExtraGambles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GamblingEvents",
                columns: table => new
                {
                    GamblingEventId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    BeginDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamblingEvents", x => x.GamblingEventId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_GamblingEventId",
                table: "Games",
                column: "GamblingEventId");

            migrationBuilder.CreateIndex(
                name: "IX_ExtraGambles_GamblingEventId",
                table: "ExtraGambles",
                column: "GamblingEventId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExtraGambles_GamblingEvents_GamblingEventId",
                table: "ExtraGambles",
                column: "GamblingEventId",
                principalTable: "GamblingEvents",
                principalColumn: "GamblingEventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_GamblingEvents_GamblingEventId",
                table: "Games",
                column: "GamblingEventId",
                principalTable: "GamblingEvents",
                principalColumn: "GamblingEventId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExtraGambles_GamblingEvents_GamblingEventId",
                table: "ExtraGambles");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_GamblingEvents_GamblingEventId",
                table: "Games");

            migrationBuilder.DropTable(
                name: "GamblingEvents");

            migrationBuilder.DropIndex(
                name: "IX_Games_GamblingEventId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_ExtraGambles_GamblingEventId",
                table: "ExtraGambles");

            migrationBuilder.DropColumn(
                name: "GamblingEventId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "GamblingEventId",
                table: "ExtraGambles");
        }
    }
}
