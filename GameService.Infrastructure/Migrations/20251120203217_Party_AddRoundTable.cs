using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Party_AddRoundTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ActualRound",
                schema: "party",
                table: "Party",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsFinish",
                schema: "party",
                table: "Party",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Gage",
                schema: "party",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Round",
                schema: "party",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    IsTeamRound = table.Column<bool>(type: "boolean", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    PartyId = table.Column<Guid>(type: "uuid", nullable: true),
                    GameId = table.Column<Guid>(type: "uuid", nullable: true),
                    TeamOneId = table.Column<Guid>(type: "uuid", nullable: true),
                    TeamTwoId = table.Column<Guid>(type: "uuid", nullable: true),
                    GageId = table.Column<Guid>(type: "uuid", nullable: true),
                    WinningTeamId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Round", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Round_Gage_GageId",
                        column: x => x.GageId,
                        principalSchema: "party",
                        principalTable: "Gage",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Round_Game_GameId",
                        column: x => x.GameId,
                        principalSchema: "party",
                        principalTable: "Game",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Round_Party_PartyId",
                        column: x => x.PartyId,
                        principalSchema: "party",
                        principalTable: "Party",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Round_Team_TeamOneId",
                        column: x => x.TeamOneId,
                        principalSchema: "party",
                        principalTable: "Team",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Round_Team_TeamTwoId",
                        column: x => x.TeamTwoId,
                        principalSchema: "party",
                        principalTable: "Team",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Round_Team_WinningTeamId",
                        column: x => x.WinningTeamId,
                        principalSchema: "party",
                        principalTable: "Team",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Round_GageId",
                schema: "party",
                table: "Round",
                column: "GageId");

            migrationBuilder.CreateIndex(
                name: "IX_Round_GameId",
                schema: "party",
                table: "Round",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Round_PartyId",
                schema: "party",
                table: "Round",
                column: "PartyId");

            migrationBuilder.CreateIndex(
                name: "IX_Round_TeamOneId",
                schema: "party",
                table: "Round",
                column: "TeamOneId");

            migrationBuilder.CreateIndex(
                name: "IX_Round_TeamTwoId",
                schema: "party",
                table: "Round",
                column: "TeamTwoId");

            migrationBuilder.CreateIndex(
                name: "IX_Round_WinningTeamId",
                schema: "party",
                table: "Round",
                column: "WinningTeamId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Round",
                schema: "party");

            migrationBuilder.DropTable(
                name: "Gage",
                schema: "party");

            migrationBuilder.DropColumn(
                name: "ActualRound",
                schema: "party",
                table: "Party");

            migrationBuilder.DropColumn(
                name: "IsFinish",
                schema: "party",
                table: "Party");
        }
    }
}
