using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPlaystationTrophyIdToAchievement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlaystationTrophyId",
                table: "Achievement",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlaystationTrophyId",
                table: "Achievement");
        }
    }
}
