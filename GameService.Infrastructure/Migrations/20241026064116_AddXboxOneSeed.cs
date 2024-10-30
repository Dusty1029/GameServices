using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddXboxOneSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Platform",
                columns: new[] { "Id", "IsSeed", "Name", "PlatformEnum" },
                values: new object[] { new Guid("47a47a3a-d03b-44cf-ac69-5bde9b4867f9"), true, "XboxOne", "XboxOne" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Platform",
                keyColumn: "Id",
                keyValue: new Guid("47a47a3a-d03b-44cf-ac69-5bde9b4867f9"));
        }
    }
}
