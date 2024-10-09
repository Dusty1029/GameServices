using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EnumHasConversionString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PlatformEnum",
                table: "Platform",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ParameterEnum",
                table: "Parameter",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.UpdateData(
                table: "Parameter",
                keyColumn: "Id",
                keyValue: new Guid("0fb8c4e8-9180-4cc5-98ff-a905d43ac440"),
                column: "ParameterEnum",
                value: "PlaystationToken");

            migrationBuilder.UpdateData(
                table: "Parameter",
                keyColumn: "Id",
                keyValue: new Guid("17481d3a-84f4-4140-8d12-2aa09f1b8d02"),
                column: "ParameterEnum",
                value: "Npsso");

            migrationBuilder.UpdateData(
                table: "Platform",
                keyColumn: "Id",
                keyValue: new Guid("43d4fe37-f799-4d61-befb-404d76f7a759"),
                column: "PlatformEnum",
                value: "PS4");

            migrationBuilder.UpdateData(
                table: "Platform",
                keyColumn: "Id",
                keyValue: new Guid("450ba6e2-97a6-4dcd-87d5-607f60385821"),
                column: "PlatformEnum",
                value: "Steam");

            migrationBuilder.UpdateData(
                table: "Platform",
                keyColumn: "Id",
                keyValue: new Guid("793511b2-5d1c-4b47-b6a7-4ced103b0be3"),
                column: "PlatformEnum",
                value: "PS3");

            migrationBuilder.UpdateData(
                table: "Platform",
                keyColumn: "Id",
                keyValue: new Guid("9b77ae73-ee99-46f3-a84d-f337ced142a8"),
                column: "PlatformEnum",
                value: "PSVITA");

            migrationBuilder.UpdateData(
                table: "Platform",
                keyColumn: "Id",
                keyValue: new Guid("db5275b2-1507-4b47-b721-b516b6b5c0d0"),
                column: "PlatformEnum",
                value: "PS5");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PlatformEnum",
                table: "Platform",
                type: "integer",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ParameterEnum",
                table: "Parameter",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.UpdateData(
                table: "Parameter",
                keyColumn: "Id",
                keyValue: new Guid("0fb8c4e8-9180-4cc5-98ff-a905d43ac440"),
                column: "ParameterEnum",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Parameter",
                keyColumn: "Id",
                keyValue: new Guid("17481d3a-84f4-4140-8d12-2aa09f1b8d02"),
                column: "ParameterEnum",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Platform",
                keyColumn: "Id",
                keyValue: new Guid("43d4fe37-f799-4d61-befb-404d76f7a759"),
                column: "PlatformEnum",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Platform",
                keyColumn: "Id",
                keyValue: new Guid("450ba6e2-97a6-4dcd-87d5-607f60385821"),
                column: "PlatformEnum",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Platform",
                keyColumn: "Id",
                keyValue: new Guid("793511b2-5d1c-4b47-b6a7-4ced103b0be3"),
                column: "PlatformEnum",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Platform",
                keyColumn: "Id",
                keyValue: new Guid("9b77ae73-ee99-46f3-a84d-f337ced142a8"),
                column: "PlatformEnum",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Platform",
                keyColumn: "Id",
                keyValue: new Guid("db5275b2-1507-4b47-b721-b516b6b5c0d0"),
                column: "PlatformEnum",
                value: 4);
        }
    }
}
