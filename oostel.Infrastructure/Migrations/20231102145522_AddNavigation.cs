using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oostel.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNavigation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReferralAgentInfos_Landlords_LandlordId",
                table: "ReferralAgentInfos");

            migrationBuilder.DropIndex(
                name: "IX_ReferralAgentInfos_LandlordId",
                table: "ReferralAgentInfos");

            migrationBuilder.DropColumn(
                name: "LandlordId",
                table: "ReferralAgentInfos");

            migrationBuilder.CreateIndex(
                name: "IX_ReferralAgentInfos_UserId",
                table: "ReferralAgentInfos",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ReferralAgentInfos_Landlords_UserId",
                table: "ReferralAgentInfos",
                column: "UserId",
                principalTable: "Landlords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReferralAgentInfos_Landlords_UserId",
                table: "ReferralAgentInfos");

            migrationBuilder.DropIndex(
                name: "IX_ReferralAgentInfos_UserId",
                table: "ReferralAgentInfos");

            migrationBuilder.AddColumn<string>(
                name: "LandlordId",
                table: "ReferralAgentInfos",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ReferralAgentInfos_LandlordId",
                table: "ReferralAgentInfos",
                column: "LandlordId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReferralAgentInfos_Landlords_LandlordId",
                table: "ReferralAgentInfos",
                column: "LandlordId",
                principalTable: "Landlords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
