using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddGameDetailStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFinished",
                table: "GameDetail");

            migrationBuilder.DropColumn(
                name: "IsStarted",
                table: "GameDetail");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "GameDetail",
                type: "text",
                nullable: false,
                defaultValue: "NotStarted");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "GameDetail");

            migrationBuilder.AddColumn<bool>(
                name: "IsFinished",
                table: "GameDetail",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsStarted",
                table: "GameDetail",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
