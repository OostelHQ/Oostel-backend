using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oostel.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AgentIssue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LandlordReferralAgentInfo",
                columns: table => new
                {
                    LandlordsId = table.Column<string>(type: "text", nullable: false),
                    ReferralAgentInfosId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LandlordReferralAgentInfo", x => new { x.LandlordsId, x.ReferralAgentInfosId });
                    table.ForeignKey(
                        name: "FK_LandlordReferralAgentInfo_Landlords_LandlordsId",
                        column: x => x.LandlordsId,
                        principalTable: "Landlords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LandlordReferralAgentInfo_ReferralAgentInfos_ReferralAgentI~",
                        column: x => x.ReferralAgentInfosId,
                        principalTable: "ReferralAgentInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LandlordReferralAgentInfo_ReferralAgentInfosId",
                table: "LandlordReferralAgentInfo",
                column: "ReferralAgentInfosId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LandlordReferralAgentInfo");
        }
    }
}
