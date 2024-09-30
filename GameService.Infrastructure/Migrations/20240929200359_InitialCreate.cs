using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GameService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    IsSeed = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Parameter",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    ParameterEnum = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parameter", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Platform",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    IsSeed = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    PlatformEnum = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Platform", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Serie",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    ParentSerieId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Serie", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Serie_Serie_ParentSerieId",
                        column: x => x.ParentSerieId,
                        principalTable: "Serie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IgnoredGame",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    PlaystationId = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    SteamId = table.Column<int>(type: "integer", nullable: true),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    PlatformId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IgnoredGame", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IgnoredGame_Platform_PlatformId",
                        column: x => x.PlatformId,
                        principalTable: "Platform",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WishGame",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    PlatformId = table.Column<Guid>(type: "uuid", nullable: false)
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
                name: "Game",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    SerieId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Game", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Game_Serie_SerieId",
                        column: x => x.SerieId,
                        principalTable: "Serie",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GameCategory",
                columns: table => new
                {
                    CategoriesId = table.Column<Guid>(type: "uuid", nullable: false),
                    GamesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameCategory", x => new { x.CategoriesId, x.GamesId });
                    table.ForeignKey(
                        name: "FK_GameCategory_Category_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameCategory_Game_GamesId",
                        column: x => x.GamesId,
                        principalTable: "Game",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    SteamId = table.Column<int>(type: "integer", nullable: true),
                    PlaystationId = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    IsFinished = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    IsStarted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    PlatformId = table.Column<Guid>(type: "uuid", nullable: false),
                    GameId = table.Column<Guid>(type: "uuid", nullable: false)
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
                name: "Achievement",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    SteamName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    PlaystationTrophyId = table.Column<int>(type: "integer", nullable: true),
                    Description = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    Percentage = table.Column<decimal>(type: "numeric(5,2)", nullable: true),
                    Achieved = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    IsIgnored = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    GameDetailId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Achievement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Achievement_GameDetail_GameDetailId",
                        column: x => x.GameDetailId,
                        principalTable: "GameDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Goal",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    Title = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    IsFulFilled = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    GameDetailId = table.Column<Guid>(type: "uuid", nullable: false)
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
                table: "Category",
                columns: new[] { "Id", "IsSeed", "Name" },
                values: new object[,]
                {
                    { new Guid("649c04ce-1936-4f55-9d2b-e8448e9a05df"), true, "Jeux de combat" },
                    { new Guid("7958d93d-852e-4c14-8b55-9d6620821126"), true, "Action - aventure" },
                    { new Guid("87ad5c12-c33b-45a4-b0f3-d95f0758e765"), true, "RPG" },
                    { new Guid("903b63a1-19bc-4970-af43-ae99a3b4eb92"), true, "Plateforme" },
                    { new Guid("952981cc-bfad-4115-aa41-c2f2d4b31446"), true, "FPS" },
                    { new Guid("a376bb7c-0d37-44db-981a-9a44c8583e05"), true, "TPS" },
                    { new Guid("e93c812a-17e4-4849-b6ce-a98353a787e3"), true, "Jeux de sport et de course" }
                });

            migrationBuilder.InsertData(
                table: "Parameter",
                columns: new[] { "Id", "ParameterEnum", "Value" },
                values: new object[,]
                {
                    { new Guid("0fb8c4e8-9180-4cc5-98ff-a905d43ac440"), 0, "" },
                    { new Guid("17481d3a-84f4-4140-8d12-2aa09f1b8d02"), 1, "" }
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
                name: "IX_Achievement_GameDetailId",
                table: "Achievement",
                column: "GameDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_Game_SerieId",
                table: "Game",
                column: "SerieId");

            migrationBuilder.CreateIndex(
                name: "IX_GameCategory_GamesId",
                table: "GameCategory",
                column: "GamesId");

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
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameDetail_SteamId",
                table: "GameDetail",
                column: "SteamId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Goal_GameDetailId",
                table: "Goal",
                column: "GameDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_IgnoredGame_Name",
                table: "IgnoredGame",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_IgnoredGame_PlatformId",
                table: "IgnoredGame",
                column: "PlatformId");

            migrationBuilder.CreateIndex(
                name: "IX_IgnoredGame_PlaystationId",
                table: "IgnoredGame",
                column: "PlaystationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IgnoredGame_SteamId",
                table: "IgnoredGame",
                column: "SteamId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parameter_ParameterEnum",
                table: "Parameter",
                column: "ParameterEnum",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Platform_Name",
                table: "Platform",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Platform_PlatformEnum",
                table: "Platform",
                column: "PlatformEnum",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Serie_ParentSerieId",
                table: "Serie",
                column: "ParentSerieId");

            migrationBuilder.CreateIndex(
                name: "IX_WishGame_PlatformId",
                table: "WishGame",
                column: "PlatformId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Achievement");

            migrationBuilder.DropTable(
                name: "GameCategory");

            migrationBuilder.DropTable(
                name: "Goal");

            migrationBuilder.DropTable(
                name: "IgnoredGame");

            migrationBuilder.DropTable(
                name: "Parameter");

            migrationBuilder.DropTable(
                name: "WishGame");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "GameDetail");

            migrationBuilder.DropTable(
                name: "Game");

            migrationBuilder.DropTable(
                name: "Platform");

            migrationBuilder.DropTable(
                name: "Serie");
        }
    }
}
