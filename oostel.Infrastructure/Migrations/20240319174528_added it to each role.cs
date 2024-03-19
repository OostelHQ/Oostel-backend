using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oostel.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addedittoeachrole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wallets_AspNetUsers_Id",
                table: "Wallets");

            migrationBuilder.AddColumn<string>(
                name: "WalletsId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WalletId",
                table: "Agents",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_WalletsId",
                table: "AspNetUsers",
                column: "WalletsId");

            migrationBuilder.CreateIndex(
                name: "IX_Agents_WalletId",
                table: "Agents",
                column: "WalletId");

            migrationBuilder.AddForeignKey(
                name: "FK_Agents_Wallets_WalletId",
                table: "Agents",
                column: "WalletId",
                principalTable: "Wallets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Wallets_WalletsId",
                table: "AspNetUsers",
                column: "WalletsId",
                principalTable: "Wallets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Wallets_Landlords_Id",
                table: "Wallets",
                column: "Id",
                principalTable: "Landlords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wallets_Students_Id",
                table: "Wallets",
                column: "Id",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agents_Wallets_WalletId",
                table: "Agents");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Wallets_WalletsId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Wallets_Landlords_Id",
                table: "Wallets");

            migrationBuilder.DropForeignKey(
                name: "FK_Wallets_Students_Id",
                table: "Wallets");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_WalletsId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_Agents_WalletId",
                table: "Agents");

            migrationBuilder.DropColumn(
                name: "WalletsId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "WalletId",
                table: "Agents");

            migrationBuilder.AddForeignKey(
                name: "FK_Wallets_AspNetUsers_Id",
                table: "Wallets",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
