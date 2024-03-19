using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oostel.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addedittoe : Migration
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddForeignKey(
                name: "FK_Wallets_Agents_UserId",
                table: "Wallets",
                column: "UserId",
                principalTable: "Agents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wallets_Landlords_UserId",
                table: "Wallets",
                column: "UserId",
                principalTable: "Landlords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wallets_Students_UserId",
                table: "Wallets",
                column: "UserId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
