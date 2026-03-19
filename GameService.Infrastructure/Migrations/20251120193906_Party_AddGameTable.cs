using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Party_AddGameTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                schema: "party",
                table: "Category");

            migrationBuilder.RenameTable(
                name: "Category",
                schema: "party",
                newName: "Player",
                newSchema: "party");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Player",
                schema: "party",
                table: "Player",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Game",
                schema: "party",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    IsTeamGame = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Game", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Game",
                schema: "party");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Player",
                schema: "party",
                table: "Player");

            migrationBuilder.RenameTable(
                name: "Player",
                schema: "party",
                newName: "Category",
                newSchema: "party");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                schema: "party",
                table: "Category",
                column: "Id");
        }
    }
}
