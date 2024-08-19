using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeUniqueIndexForGame : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Game_PlaystationId",
                table: "Game");

            migrationBuilder.CreateIndex(
                name: "IX_Game_PlaystationId_Platform",
                table: "Game",
                columns: new[] { "PlaystationId", "Platform" },
                unique: true,
                filter: "[PlaystationId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Game_PlaystationId_Platform",
                table: "Game");

            migrationBuilder.CreateIndex(
                name: "IX_Game_PlaystationId",
                table: "Game",
                column: "PlaystationId",
                unique: true,
                filter: "[PlaystationId] IS NOT NULL");
        }
    }
}
