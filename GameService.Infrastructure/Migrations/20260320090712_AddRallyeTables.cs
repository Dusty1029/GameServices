using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRallyeTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rallye",
                schema: "rallye",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rallye", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlayerRallye",
                schema: "rallye",
                columns: table => new
                {
                    PlayersId = table.Column<Guid>(type: "uuid", nullable: false),
                    RallyesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerRallye", x => new { x.PlayersId, x.RallyesId });
                    table.ForeignKey(
                        name: "FK_PlayerRallye_Player_PlayersId",
                        column: x => x.PlayersId,
                        principalSchema: "rallye",
                        principalTable: "Player",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerRallye_Rallye_RallyesId",
                        column: x => x.RallyesId,
                        principalSchema: "rallye",
                        principalTable: "Rallye",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Special",
                schema: "rallye",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    RallyeId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Special", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Special_Rallye_RallyeId",
                        column: x => x.RallyeId,
                        principalSchema: "rallye",
                        principalTable: "Rallye",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SpecialTime",
                schema: "rallye",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    Time = table.Column<TimeSpan>(type: "interval", nullable: false),
                    SpecialId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlayerId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialTime", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpecialTime_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalSchema: "rallye",
                        principalTable: "Player",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpecialTime_Special_SpecialId",
                        column: x => x.SpecialId,
                        principalSchema: "rallye",
                        principalTable: "Special",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerRallye_RallyesId",
                schema: "rallye",
                table: "PlayerRallye",
                column: "RallyesId");

            migrationBuilder.CreateIndex(
                name: "IX_Special_RallyeId",
                schema: "rallye",
                table: "Special",
                column: "RallyeId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialTime_PlayerId",
                schema: "rallye",
                table: "SpecialTime",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialTime_SpecialId",
                schema: "rallye",
                table: "SpecialTime",
                column: "SpecialId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerRallye",
                schema: "rallye");

            migrationBuilder.DropTable(
                name: "SpecialTime",
                schema: "rallye");

            migrationBuilder.DropTable(
                name: "Special",
                schema: "rallye");

            migrationBuilder.DropTable(
                name: "Rallye",
                schema: "rallye");
        }
    }
}
