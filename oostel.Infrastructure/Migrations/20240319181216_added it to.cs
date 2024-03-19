using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oostel.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addeditto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wallets_Agents_UserId",
                table: "Wallets");

            migrationBuilder.DropForeignKey(
                name: "FK_Wallets_Landlords_UserId",
                table: "Wallets");

            migrationBuilder.DropForeignKey(
                name: "FK_Wallets_Students_UserId",
                table: "Wallets");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Wallets",
                newName: "StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_Wallets_UserId",
                table: "Wallets",
                newName: "IX_Wallets_StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Wallets_Students_StudentId",
                table: "Wallets",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wallets_Students_StudentId",
                table: "Wallets");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "Wallets",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Wallets_StudentId",
                table: "Wallets",
                newName: "IX_Wallets_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Wallets_Agents_UserId",
                table: "Wallets",
                column: "UserId",
                principalTable: "Agents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Wallets_Landlords_UserId",
                table: "Wallets",
                column: "UserId",
                principalTable: "Landlords",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Wallets_Students_UserId",
                table: "Wallets",
                column: "UserId",
                principalTable: "Students",
                principalColumn: "Id");
        }
    }
}
