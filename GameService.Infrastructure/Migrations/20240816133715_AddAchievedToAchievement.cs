using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAchievedToAchievement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Achieved",
                table: "Achievement",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("649c04ce-1936-4f55-9d2b-e8448e9a05df"),
                column: "Name",
                value: "Jeux de combat");

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("e93c812a-17e4-4849-b6ce-a98353a787e3"),
                column: "Name",
                value: "Jeux de sport et de course");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Achieved",
                table: "Achievement");

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("649c04ce-1936-4f55-9d2b-e8448e9a05df"),
                column: "Name",
                value: "Jeu de combat");

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("e93c812a-17e4-4849-b6ce-a98353a787e3"),
                column: "Name",
                value: "Jeu de sport et de course");
        }
    }
}
