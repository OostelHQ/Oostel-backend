using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oostel.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateModelForSomeFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HostelLikes_AspNetUsers_UserId",
                table: "HostelLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_HostelLikes_Hostels_HostelId",
                table: "HostelLikes");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "HostelLikes",
                newName: "SourceUserId");

            migrationBuilder.RenameColumn(
                name: "HostelId",
                table: "HostelLikes",
                newName: "LikedHostelId");

            migrationBuilder.RenameIndex(
                name: "IX_HostelLikes_UserId",
                table: "HostelLikes",
                newName: "IX_HostelLikes_SourceUserId");

            migrationBuilder.RenameIndex(
                name: "IX_HostelLikes_HostelId",
                table: "HostelLikes",
                newName: "IX_HostelLikes_LikedHostelId");

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    TransactionType = table.Column<int>(type: "integer", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transaction_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Wallet",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    AvailableBalance = table.Column<decimal>(type: "numeric", nullable: false),
                    LastTransactionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wallet_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_UserId",
                table: "Transaction",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_HostelLikes_AspNetUsers_SourceUserId",
                table: "HostelLikes",
                column: "SourceUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HostelLikes_Hostels_LikedHostelId",
                table: "HostelLikes",
                column: "LikedHostelId",
                principalTable: "Hostels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HostelLikes_AspNetUsers_SourceUserId",
                table: "HostelLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_HostelLikes_Hostels_LikedHostelId",
                table: "HostelLikes");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "Wallet");

            migrationBuilder.RenameColumn(
                name: "SourceUserId",
                table: "HostelLikes",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "LikedHostelId",
                table: "HostelLikes",
                newName: "HostelId");

            migrationBuilder.RenameIndex(
                name: "IX_HostelLikes_SourceUserId",
                table: "HostelLikes",
                newName: "IX_HostelLikes_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_HostelLikes_LikedHostelId",
                table: "HostelLikes",
                newName: "IX_HostelLikes_HostelId");

            migrationBuilder.AddForeignKey(
                name: "FK_HostelLikes_AspNetUsers_UserId",
                table: "HostelLikes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HostelLikes_Hostels_HostelId",
                table: "HostelLikes",
                column: "HostelId",
                principalTable: "Hostels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
