using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oostel.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addHostelLike : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OpenToRoommate_Students_StudentId",
                table: "OpenToRoommate");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OpenToRoommate",
                table: "OpenToRoommate");

            migrationBuilder.RenameTable(
                name: "OpenToRoommate",
                newName: "OpenToRoommates");

            migrationBuilder.RenameIndex(
                name: "IX_OpenToRoommate_StudentId",
                table: "OpenToRoommates",
                newName: "IX_OpenToRoommates_StudentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OpenToRoommates",
                table: "OpenToRoommates",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "HostelLikes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    HostelId = table.Column<string>(type: "text", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HostelLikes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HostelLikes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HostelLikes_Hostels_HostelId",
                        column: x => x.HostelId,
                        principalTable: "Hostels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HostelLikes_HostelId",
                table: "HostelLikes",
                column: "HostelId");

            migrationBuilder.CreateIndex(
                name: "IX_HostelLikes_UserId",
                table: "HostelLikes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_OpenToRoommates_Students_StudentId",
                table: "OpenToRoommates",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OpenToRoommates_Students_StudentId",
                table: "OpenToRoommates");

            migrationBuilder.DropTable(
                name: "HostelLikes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OpenToRoommates",
                table: "OpenToRoommates");

            migrationBuilder.RenameTable(
                name: "OpenToRoommates",
                newName: "OpenToRoommate");

            migrationBuilder.RenameIndex(
                name: "IX_OpenToRoommates_StudentId",
                table: "OpenToRoommate",
                newName: "IX_OpenToRoommate_StudentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OpenToRoommate",
                table: "OpenToRoommate",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OpenToRoommate_Students_StudentId",
                table: "OpenToRoommate",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
