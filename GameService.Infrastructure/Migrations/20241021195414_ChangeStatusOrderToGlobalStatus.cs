using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeStatusOrderToGlobalStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Game_Name",
                table: "Game");

            migrationBuilder.DropIndex(
                name: "IX_Game_StatusOrder",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "StatusOrder",
                table: "Game");

            migrationBuilder.AddColumn<string>(
                name: "GlobalStatus",
                table: "Game",
                type: "text",
                nullable: false,
                defaultValue: "NotStarted");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GlobalStatus",
                table: "Game");

            migrationBuilder.AddColumn<int>(
                name: "StatusOrder",
                table: "Game",
                type: "integer",
                nullable: false,
                defaultValue: 3);

            migrationBuilder.CreateIndex(
                name: "IX_Game_Name",
                table: "Game",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Game_StatusOrder",
                table: "Game",
                column: "StatusOrder");
        }
    }
}
