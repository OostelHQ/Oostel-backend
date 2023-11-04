using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oostel.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class testing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReferralAgentInfos_AspNetUsers_Id",
                table: "ReferralAgentInfos");

            migrationBuilder.CreateIndex(
                name: "IX_ReferralAgentInfos_UserId",
                table: "ReferralAgentInfos",
                column: "UserId",
                unique: true);

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

            migrationBuilder.DropIndex(
                name: "IX_ReferralAgentInfos_UserId",
                table: "ReferralAgentInfos");

            migrationBuilder.AddForeignKey(
                name: "FK_ReferralAgentInfos_AspNetUsers_Id",
                table: "ReferralAgentInfos",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
