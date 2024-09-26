using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIgnoredGameTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IgnoredGame",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlaystationId = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    SteamId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IgnoredGame", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IgnoredGame_Name",
                table: "IgnoredGame",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_IgnoredGame_PlaystationId",
                table: "IgnoredGame",
                column: "PlaystationId",
                unique: true,
                filter: "[PlaystationId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_IgnoredGame_SteamId",
                table: "IgnoredGame",
                column: "SteamId",
                unique: true,
                filter: "[SteamId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IgnoredGame");
        }
    }
}
