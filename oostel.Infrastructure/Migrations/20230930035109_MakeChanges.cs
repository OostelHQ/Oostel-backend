using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oostel.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MakeChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePhotoURL",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ProfilePhotoURL",
                table: "Landlords");

            migrationBuilder.AddColumn<string>(
                name: "ProfilePhotoURL",
                table: "AspNetUsers",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePhotoURL",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "ProfilePhotoURL",
                table: "Students",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfilePhotoURL",
                table: "Landlords",
                type: "text",
                nullable: true);
        }
    }
}
