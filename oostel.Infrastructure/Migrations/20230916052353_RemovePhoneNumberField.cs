using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oostel.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemovePhoneNumberField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "UserProfiles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "UserProfiles",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
