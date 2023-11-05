using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oostel.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addLandLordAndAgentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LandlordAgent_Agents_AgentId",
                table: "LandlordAgent");

            migrationBuilder.DropForeignKey(
                name: "FK_LandlordAgent_Landlords_LandlordId",
                table: "LandlordAgent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LandlordAgent",
                table: "LandlordAgent");

            migrationBuilder.RenameTable(
                name: "LandlordAgent",
                newName: "LandlordAgents");

            migrationBuilder.RenameIndex(
                name: "IX_LandlordAgent_AgentId",
                table: "LandlordAgents",
                newName: "IX_LandlordAgents_AgentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LandlordAgents",
                table: "LandlordAgents",
                columns: new[] { "LandlordId", "AgentId" });

            migrationBuilder.AddForeignKey(
                name: "FK_LandlordAgents_Agents_AgentId",
                table: "LandlordAgents",
                column: "AgentId",
                principalTable: "Agents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LandlordAgents_Landlords_LandlordId",
                table: "LandlordAgents",
                column: "LandlordId",
                principalTable: "Landlords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LandlordAgents_Agents_AgentId",
                table: "LandlordAgents");

            migrationBuilder.DropForeignKey(
                name: "FK_LandlordAgents_Landlords_LandlordId",
                table: "LandlordAgents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LandlordAgents",
                table: "LandlordAgents");

            migrationBuilder.RenameTable(
                name: "LandlordAgents",
                newName: "LandlordAgent");

            migrationBuilder.RenameIndex(
                name: "IX_LandlordAgents_AgentId",
                table: "LandlordAgent",
                newName: "IX_LandlordAgent_AgentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LandlordAgent",
                table: "LandlordAgent",
                columns: new[] { "LandlordId", "AgentId" });

            migrationBuilder.AddForeignKey(
                name: "FK_LandlordAgent_Agents_AgentId",
                table: "LandlordAgent",
                column: "AgentId",
                principalTable: "Agents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LandlordAgent_Landlords_LandlordId",
                table: "LandlordAgent",
                column: "LandlordId",
                principalTable: "Landlords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
