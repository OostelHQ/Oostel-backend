using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oostel.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OpenToRoomate");

            migrationBuilder.AddColumn<int>(
                name: "ProfileViewCount",
                table: "AspNetUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "OpenToRoommate",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    StudentId = table.Column<string>(type: "text", nullable: false),
                    HostelName = table.Column<string>(type: "text", nullable: false),
                    RoomBudgetAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    HostelAddress = table.Column<string>(type: "text", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenToRoommate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpenToRoommate_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OpenToRoommate_StudentId",
                table: "OpenToRoommate",
                column: "StudentId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OpenToRoommate");

            migrationBuilder.DropColumn(
                name: "ProfileViewCount",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "OpenToRoomate",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    StudentId = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    HostelAddress = table.Column<string>(type: "text", nullable: false),
                    HostelName = table.Column<string>(type: "text", nullable: false),
                    HostelPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenToRoomate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpenToRoomate_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OpenToRoomate_StudentId",
                table: "OpenToRoomate",
                column: "StudentId",
                unique: true);
        }
    }
}
