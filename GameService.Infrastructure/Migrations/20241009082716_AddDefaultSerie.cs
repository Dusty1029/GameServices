using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultSerie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "Serie",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Serie",
                columns: new[] { "Id", "IsDefault", "Name", "ParentSerieId" },
                values: new object[] { new Guid("93d2da48-aaea-4932-afec-8a2ae310edd3"), true, "Pas de série", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Serie",
                keyColumn: "Id",
                keyValue: new Guid("93d2da48-aaea-4932-afec-8a2ae310edd3"));

            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "Serie");
        }
    }
}
