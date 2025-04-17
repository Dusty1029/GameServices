using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddHowLongToBeatColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "FullTime",
                table: "Game",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HowLongToBeatName",
                table: "Game",
                type: "character varying(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "MainStoryAndExtraTime",
                table: "Game",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MainStoryTime",
                table: "Game",
                type: "numeric",
                nullable: true);

            migrationBuilder.Sql("UPDATE \"Game\" SET \"HowLongToBeatName\" = \"Name\";");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullTime",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "HowLongToBeatName",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "MainStoryAndExtraTime",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "MainStoryTime",
                table: "Game");
        }
    }
}
