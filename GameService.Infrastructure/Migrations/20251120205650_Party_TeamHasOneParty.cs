using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Party_TeamHasOneParty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PartyTeam",
                schema: "party");

            migrationBuilder.AddColumn<Guid>(
                name: "PartyId",
                schema: "party",
                table: "Team",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Team_PartyId",
                schema: "party",
                table: "Team",
                column: "PartyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Team_Party_PartyId",
                schema: "party",
                table: "Team",
                column: "PartyId",
                principalSchema: "party",
                principalTable: "Party",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Team_Party_PartyId",
                schema: "party",
                table: "Team");

            migrationBuilder.DropIndex(
                name: "IX_Team_PartyId",
                schema: "party",
                table: "Team");

            migrationBuilder.DropColumn(
                name: "PartyId",
                schema: "party",
                table: "Team");

            migrationBuilder.CreateTable(
                name: "PartyTeam",
                schema: "party",
                columns: table => new
                {
                    PartiesId = table.Column<Guid>(type: "uuid", nullable: false),
                    TeamsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartyTeam", x => new { x.PartiesId, x.TeamsId });
                    table.ForeignKey(
                        name: "FK_PartyTeam_Party_PartiesId",
                        column: x => x.PartiesId,
                        principalSchema: "party",
                        principalTable: "Party",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PartyTeam_Team_TeamsId",
                        column: x => x.TeamsId,
                        principalSchema: "party",
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PartyTeam_TeamsId",
                schema: "party",
                table: "PartyTeam",
                column: "TeamsId");
        }
    }
}
