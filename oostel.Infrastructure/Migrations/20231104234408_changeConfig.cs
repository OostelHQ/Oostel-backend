using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oostel.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changeConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AgentLandlord");

            migrationBuilder.CreateTable(
                name: "LandlordAgent",
                columns: table => new
                {
                    AgentId = table.Column<string>(type: "text", nullable: false),
                    LandlordId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LandlordAgent", x => new { x.LandlordId, x.AgentId });
                    table.ForeignKey(
                        name: "FK_LandlordAgent_Agents_AgentId",
                        column: x => x.AgentId,
                        principalTable: "Agents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LandlordAgent_Landlords_LandlordId",
                        column: x => x.LandlordId,
                        principalTable: "Landlords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LandlordAgent_AgentId",
                table: "LandlordAgent",
                column: "AgentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LandlordAgent");

            migrationBuilder.CreateTable(
                name: "AgentLandlord",
                columns: table => new
                {
                    AgentsId = table.Column<string>(type: "text", nullable: false),
                    LandlordsId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgentLandlord", x => new { x.AgentsId, x.LandlordsId });
                    table.ForeignKey(
                        name: "FK_AgentLandlord_Agents_AgentsId",
                        column: x => x.AgentsId,
                        principalTable: "Agents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AgentLandlord_Landlords_LandlordsId",
                        column: x => x.LandlordsId,
                        principalTable: "Landlords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AgentLandlord_LandlordsId",
                table: "AgentLandlord",
                column: "LandlordsId");
        }
    }
}
