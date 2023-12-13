using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oostel.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class gottenHostelColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HostelName",
                table: "OpenToRoommates");

            migrationBuilder.AddColumn<bool>(
                name: "GottenAHostel",
                table: "OpenToRoommates",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GottenAHostel",
                table: "OpenToRoommates");

            migrationBuilder.AddColumn<string>(
                name: "HostelName",
                table: "OpenToRoommates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
