using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPlatformToIgnoredGames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PlatformId",
                table: "IgnoredGame",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_IgnoredGame_PlatformId",
                table: "IgnoredGame",
                column: "PlatformId");

            migrationBuilder.AddForeignKey(
                name: "FK_IgnoredGame_Platform_PlatformId",
                table: "IgnoredGame",
                column: "PlatformId",
                principalTable: "Platform",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IgnoredGame_Platform_PlatformId",
                table: "IgnoredGame");

            migrationBuilder.DropIndex(
                name: "IX_IgnoredGame_PlatformId",
                table: "IgnoredGame");

            migrationBuilder.DropColumn(
                name: "PlatformId",
                table: "IgnoredGame");
        }
    }
}
