using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oostel.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReferralAgentInfos_Landlords_UserId",
                table: "ReferralAgentInfos");

            migrationBuilder.AddForeignKey(
                name: "FK_ReferralAgentInfos_AspNetUsers_UserId",
                table: "ReferralAgentInfos",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReferralAgentInfos_AspNetUsers_UserId",
                table: "ReferralAgentInfos");

            migrationBuilder.AddForeignKey(
                name: "FK_ReferralAgentInfos_Landlords_UserId",
                table: "ReferralAgentInfos",
                column: "UserId",
                principalTable: "Landlords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
