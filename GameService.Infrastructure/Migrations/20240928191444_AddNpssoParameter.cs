using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNpssoParameter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Parameter",
                columns: new[] { "Id", "ParameterEnum", "Value" },
                values: new object[] { new Guid("17481d3a-84f4-4140-8d12-2aa09f1b8d02"), 1, "" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Parameter",
                keyColumn: "Id",
                keyValue: new Guid("17481d3a-84f4-4140-8d12-2aa09f1b8d02"));
        }
    }
}
