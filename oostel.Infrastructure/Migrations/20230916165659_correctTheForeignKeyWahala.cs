using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oostel.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class correctTheForeignKeyWahala : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hostels_UserProfiles_Id",
                table: "Hostels");

            migrationBuilder.CreateIndex(
                name: "IX_Hostels_UserId",
                table: "Hostels",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hostels_UserProfiles_UserId",
                table: "Hostels",
                column: "UserId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hostels_UserProfiles_UserId",
                table: "Hostels");

            migrationBuilder.DropIndex(
                name: "IX_Hostels_UserId",
                table: "Hostels");

            migrationBuilder.AddForeignKey(
                name: "FK_Hostels_UserProfiles_Id",
                table: "Hostels",
                column: "Id",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
