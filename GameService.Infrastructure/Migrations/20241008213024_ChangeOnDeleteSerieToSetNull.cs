using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeOnDeleteSerieToSetNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Game_Serie_SerieId",
                table: "Game");

            migrationBuilder.AddForeignKey(
                name: "FK_Game_Serie_SerieId",
                table: "Game",
                column: "SerieId",
                principalTable: "Serie",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Game_Serie_SerieId",
                table: "Game");

            migrationBuilder.AddForeignKey(
                name: "FK_Game_Serie_SerieId",
                table: "Game",
                column: "SerieId",
                principalTable: "Serie",
                principalColumn: "Id");
        }
    }
}
