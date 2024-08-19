using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPlaystationIdToGame : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PlaystationId",
                table: "Game",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parameter_ParameterEnum",
                table: "Parameter",
                column: "ParameterEnum",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Game_PlaystationId",
                table: "Game",
                column: "PlaystationId",
                unique: true,
                filter: "[PlaystationId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Parameter_ParameterEnum",
                table: "Parameter");

            migrationBuilder.DropIndex(
                name: "IX_Game_PlaystationId",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "PlaystationId",
                table: "Game");
        }
    }
}
