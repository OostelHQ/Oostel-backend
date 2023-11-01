using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oostel.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAgentAndReferralTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SenderLastName",
                table: "Messages",
                newName: "SenderEmail");

            migrationBuilder.RenameColumn(
                name: "RecipientLastName",
                table: "Messages",
                newName: "RecipientEmail");

            migrationBuilder.CreateTable(
                name: "AgentReferreds",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ReferrerId = table.Column<string>(type: "text", nullable: false),
                    ReferrerCode = table.Column<string>(type: "text", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgentReferreds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AgentReferreds_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReferralAgentInfos",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ReferralCode = table.Column<string>(type: "text", nullable: false),
                    LandlordId = table.Column<string>(type: "text", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReferralAgentInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReferralAgentInfos_Landlords_LandlordId",
                        column: x => x.LandlordId,
                        principalTable: "Landlords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AgentReferreds_UserId",
                table: "AgentReferreds",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReferralAgentInfos_LandlordId",
                table: "ReferralAgentInfos",
                column: "LandlordId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AgentReferreds");

            migrationBuilder.DropTable(
                name: "ReferralAgentInfos");

            migrationBuilder.RenameColumn(
                name: "SenderEmail",
                table: "Messages",
                newName: "SenderLastName");

            migrationBuilder.RenameColumn(
                name: "RecipientEmail",
                table: "Messages",
                newName: "RecipientLastName");
        }
    }
}
