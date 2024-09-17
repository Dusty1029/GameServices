using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GameService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDatabaseMindset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Achievement_Game_GameId",
                table: "Achievement");

            migrationBuilder.DropIndex(
                name: "IX_Game_Name_Platform",
                table: "Game");

            migrationBuilder.DropIndex(
                name: "IX_Game_PlaystationId_Platform",
                table: "Game");

            migrationBuilder.DropIndex(
                name: "IX_Game_SteamId",
                table: "Game");

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
                keyValue: new Guid("78b7740c-0a69-4dab-a4b1-14a39457a81a"));

            migrationBuilder.DropColumn(
                name: "IsIgnored",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "Platform",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "PlaystationId",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "SteamId",
                table: "Game");

            migrationBuilder.RenameColumn(
                name: "GameId",
                table: "Achievement",
                newName: "GameDetailId");

            migrationBuilder.RenameIndex(
                name: "IX_Achievement_GameId",
                table: "Achievement",
                newName: "IX_Achievement_GameDetailId");

            migrationBuilder.AddColumn<Guid>(
                name: "SerieId",
                table: "Game",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "IsIgnored",
                table: "Achievement",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "IgnoredSteamGame",
                columns: table => new
                {
                    SteamId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IgnoredSteamGame", x => x.SteamId);
                });

            migrationBuilder.CreateTable(
                name: "Platform",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    IsSeed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    PlatformEnum = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Platform", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Serie",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ParentSerieId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Serie", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Serie_Serie_ParentSerieId",
                        column: x => x.ParentSerieId,
                        principalTable: "Serie",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GameDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SteamId = table.Column<int>(type: "int", nullable: true),
                    PlaystationId = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    IsFinished = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsStarted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    PlatformId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameDetail_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameDetail_Platform_PlatformId",
                        column: x => x.PlatformId,
                        principalTable: "Platform",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WishGame",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    PlatformId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishGame", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WishGame_Platform_PlatformId",
                        column: x => x.PlatformId,
                        principalTable: "Platform",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Goal",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    IsFulFilled = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    GameDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Goal_GameDetail_GameDetailId",
                        column: x => x.GameDetailId,
                        principalTable: "GameDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Platform",
                columns: new[] { "Id", "IsSeed", "Name", "PlatformEnum" },
                values: new object[,]
                {
                    { new Guid("43d4fe37-f799-4d61-befb-404d76f7a759"), true, "PS4", 3 },
                    { new Guid("450ba6e2-97a6-4dcd-87d5-607f60385821"), true, "Steam", 0 },
                    { new Guid("793511b2-5d1c-4b47-b6a7-4ced103b0be3"), true, "PS3", 2 },
                    { new Guid("9b77ae73-ee99-46f3-a84d-f337ced142a8"), true, "PS VITA", 1 },
                    { new Guid("db5275b2-1507-4b47-b721-b516b6b5c0d0"), true, "PS5", 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Game_SerieId",
                table: "Game",
                column: "SerieId");

            migrationBuilder.CreateIndex(
                name: "IX_GameDetail_GameId_PlatformId",
                table: "GameDetail",
                columns: new[] { "GameId", "PlatformId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameDetail_PlatformId",
                table: "GameDetail",
                column: "PlatformId");

            migrationBuilder.CreateIndex(
                name: "IX_GameDetail_PlaystationId_PlatformId",
                table: "GameDetail",
                columns: new[] { "PlaystationId", "PlatformId" },
                unique: true,
                filter: "[PlaystationId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_GameDetail_SteamId",
                table: "GameDetail",
                column: "SteamId",
                unique: true,
                filter: "[SteamId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Goal_GameDetailId",
                table: "Goal",
                column: "GameDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_Platform_Name",
                table: "Platform",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Platform_PlatformEnum",
                table: "Platform",
                column: "PlatformEnum",
                unique: true,
                filter: "[PlatformEnum] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Serie_ParentSerieId",
                table: "Serie",
                column: "ParentSerieId");

            migrationBuilder.CreateIndex(
                name: "IX_WishGame_PlatformId",
                table: "WishGame",
                column: "PlatformId");

            migrationBuilder.AddForeignKey(
                name: "FK_Achievement_GameDetail_GameDetailId",
                table: "Achievement",
                column: "GameDetailId",
                principalTable: "GameDetail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Game_Serie_SerieId",
                table: "Game",
                column: "SerieId",
                principalTable: "Serie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Achievement_GameDetail_GameDetailId",
                table: "Achievement");

            migrationBuilder.DropForeignKey(
                name: "FK_Game_Serie_SerieId",
                table: "Game");

            migrationBuilder.DropTable(
                name: "Goal");

            migrationBuilder.DropTable(
                name: "IgnoredSteamGame");

            migrationBuilder.DropTable(
                name: "Serie");

            migrationBuilder.DropTable(
                name: "WishGame");

            migrationBuilder.DropTable(
                name: "GameDetail");

            migrationBuilder.DropTable(
                name: "Platform");

            migrationBuilder.DropIndex(
                name: "IX_Game_SerieId",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "SerieId",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "IsIgnored",
                table: "Achievement");

            migrationBuilder.RenameColumn(
                name: "GameDetailId",
                table: "Achievement",
                newName: "GameId");

            migrationBuilder.RenameIndex(
                name: "IX_Achievement_GameDetailId",
                table: "Achievement",
                newName: "IX_Achievement_GameId");

            migrationBuilder.AddColumn<bool>(
                name: "IsIgnored",
                table: "Game",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Platform",
                table: "Game",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PlaystationId",
                table: "Game",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SteamId",
                table: "Game",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "IsSeed", "Name" },
                values: new object[,]
                {
                    { new Guid("2346a66c-0f40-4ea6-b61d-582865896b41"), true, "Jeux commencés" },
                    { new Guid("2c9a7b53-183f-4971-9b8a-81c98816f05f"), true, "Jeux terminés à 100%" },
                    { new Guid("78b7740c-0a69-4dab-a4b1-14a39457a81a"), true, "Jeux terminés" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Game_Name_Platform",
                table: "Game",
                columns: new[] { "Name", "Platform" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Game_PlaystationId_Platform",
                table: "Game",
                columns: new[] { "PlaystationId", "Platform" },
                unique: true,
                filter: "[PlaystationId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Game_SteamId",
                table: "Game",
                column: "SteamId",
                unique: true,
                filter: "[SteamId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Achievement_Game_GameId",
                table: "Achievement",
                column: "GameId",
                principalTable: "Game",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
