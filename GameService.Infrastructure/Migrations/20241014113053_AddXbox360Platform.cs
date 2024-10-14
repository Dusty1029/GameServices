using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddXbox360Platform : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "XboxId",
                table: "IgnoredGame",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "XboxId",
                table: "GameDetail",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "XboxId",
                table: "Achievement",
                type: "integer",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Platform",
                columns: new[] { "Id", "IsSeed", "Name", "PlatformEnum" },
                values: new object[] { new Guid("b7943964-819d-4c70-9997-c09c7aeb468d"), true, "Xbox360", "Xbox360" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Platform",
                keyColumn: "Id",
                keyValue: new Guid("b7943964-819d-4c70-9997-c09c7aeb468d"));

            migrationBuilder.DropColumn(
                name: "XboxId",
                table: "IgnoredGame");

            migrationBuilder.DropColumn(
                name: "XboxId",
                table: "GameDetail");

            migrationBuilder.DropColumn(
                name: "XboxId",
                table: "Achievement");
        }
    }
}
