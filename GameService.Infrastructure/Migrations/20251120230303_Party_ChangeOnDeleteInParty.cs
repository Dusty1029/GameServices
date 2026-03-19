using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Party_ChangeOnDeleteInParty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Round_Party_PartyId",
                schema: "party",
                table: "Round");

            migrationBuilder.DropForeignKey(
                name: "FK_Team_Party_PartyId",
                schema: "party",
                table: "Team");

            migrationBuilder.AddForeignKey(
                name: "FK_Round_Party_PartyId",
                schema: "party",
                table: "Round",
                column: "PartyId",
                principalSchema: "party",
                principalTable: "Party",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Team_Party_PartyId",
                schema: "party",
                table: "Team",
                column: "PartyId",
                principalSchema: "party",
                principalTable: "Party",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Round_Party_PartyId",
                schema: "party",
                table: "Round");

            migrationBuilder.DropForeignKey(
                name: "FK_Team_Party_PartyId",
                schema: "party",
                table: "Team");

            migrationBuilder.AddForeignKey(
                name: "FK_Round_Party_PartyId",
                schema: "party",
                table: "Round",
                column: "PartyId",
                principalSchema: "party",
                principalTable: "Party",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Team_Party_PartyId",
                schema: "party",
                table: "Team",
                column: "PartyId",
                principalSchema: "party",
                principalTable: "Party",
                principalColumn: "Id");
        }
    }
}
