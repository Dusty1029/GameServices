using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GameService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCategorySeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSeed",
                table: "Category",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "IsSeed", "Name" },
                values: new object[,]
                {
                    { new Guid("2346a66c-0f40-4ea6-b61d-582865896b41"), true, "Jeux commencés" },
                    { new Guid("2c9a7b53-183f-4971-9b8a-81c98816f05f"), true, "Jeux terminés à 100%" },
                    { new Guid("649c04ce-1936-4f55-9d2b-e8448e9a05df"), true, "Jeu de combat" },
                    { new Guid("78b7740c-0a69-4dab-a4b1-14a39457a81a"), true, "Jeux terminés" },
                    { new Guid("7958d93d-852e-4c14-8b55-9d6620821126"), true, "Action - aventure" },
                    { new Guid("87ad5c12-c33b-45a4-b0f3-d95f0758e765"), true, "RPG" },
                    { new Guid("903b63a1-19bc-4970-af43-ae99a3b4eb92"), true, "Plateforme" },
                    { new Guid("952981cc-bfad-4115-aa41-c2f2d4b31446"), true, "FPS" },
                    { new Guid("a376bb7c-0d37-44db-981a-9a44c8583e05"), true, "TPS" },
                    { new Guid("e93c812a-17e4-4849-b6ce-a98353a787e3"), true, "Jeu de sport et de course" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("2346a66c-0f40-4ea6-b61d-582865896b41"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("2c9a7b53-183f-4971-9b8a-81c98816f05f"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("649c04ce-1936-4f55-9d2b-e8448e9a05df"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("78b7740c-0a69-4dab-a4b1-14a39457a81a"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("7958d93d-852e-4c14-8b55-9d6620821126"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("87ad5c12-c33b-45a4-b0f3-d95f0758e765"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("903b63a1-19bc-4970-af43-ae99a3b4eb92"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("952981cc-bfad-4115-aa41-c2f2d4b31446"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("a376bb7c-0d37-44db-981a-9a44c8583e05"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("e93c812a-17e4-4849-b6ce-a98353a787e3"));

            migrationBuilder.DropColumn(
                name: "IsSeed",
                table: "Category");
        }
    }
}
