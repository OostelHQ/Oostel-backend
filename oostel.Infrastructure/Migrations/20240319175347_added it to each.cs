using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oostel.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addedittoeach : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agents_Wallets_WalletId",
                table: "Agents");

            migrationBuilder.DropForeignKey(
                name: "FK_Wallets_Landlords_Id",
                table: "Wallets");

            migrationBuilder.DropForeignKey(
                name: "FK_Wallets_Students_Id",
                table: "Wallets");

            migrationBuilder.DropIndex(
                name: "IX_Agents_WalletId",
                table: "Agents");

            migrationBuilder.DropColumn(
                name: "WalletId",
                table: "Agents");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Wallets",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_UserId",
                table: "Wallets",
                column: "UserId",
                unique: true);

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

            migrationBuilder.DropIndex(
                name: "IX_Wallets_UserId",
                table: "Wallets");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Wallets",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "WalletId",
                table: "Agents",
                type: "nvarchar(450)",
                nullable: true);

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
    }
}
