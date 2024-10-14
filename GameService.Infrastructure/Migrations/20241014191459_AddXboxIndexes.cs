using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddXboxIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_IgnoredGame_PlaystationId",
                table: "IgnoredGame");

            migrationBuilder.CreateIndex(
                name: "IX_IgnoredGame_PlaystationId_PlatformId",
                table: "IgnoredGame",
                columns: new[] { "PlaystationId", "PlatformId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IgnoredGame_XboxId_PlatformId",
                table: "IgnoredGame",
                columns: new[] { "XboxId", "PlatformId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameDetail_XboxId_PlatformId",
                table: "GameDetail",
                columns: new[] { "XboxId", "PlatformId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_IgnoredGame_PlaystationId_PlatformId",
                table: "IgnoredGame");

            migrationBuilder.DropIndex(
                name: "IX_IgnoredGame_XboxId_PlatformId",
                table: "IgnoredGame");

            migrationBuilder.DropIndex(
                name: "IX_GameDetail_XboxId_PlatformId",
                table: "GameDetail");

            migrationBuilder.CreateIndex(
                name: "IX_IgnoredGame_PlaystationId",
                table: "IgnoredGame",
                column: "PlaystationId",
                unique: true);
        }
    }
}
