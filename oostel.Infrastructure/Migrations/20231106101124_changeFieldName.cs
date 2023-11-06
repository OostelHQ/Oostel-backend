using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oostel.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changeFieldName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hostels_Landlords_UserId",
                table: "Hostels");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Hostels",
                newName: "LandlordId");

            migrationBuilder.RenameIndex(
                name: "IX_Hostels_UserId",
                table: "Hostels",
                newName: "IX_Hostels_LandlordId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hostels_Landlords_LandlordId",
                table: "Hostels",
                column: "LandlordId",
                principalTable: "Landlords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hostels_Landlords_LandlordId",
                table: "Hostels");

            migrationBuilder.RenameColumn(
                name: "LandlordId",
                table: "Hostels",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Hostels_LandlordId",
                table: "Hostels",
                newName: "IX_Hostels_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hostels_Landlords_UserId",
                table: "Hostels",
                column: "UserId",
                principalTable: "Landlords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
