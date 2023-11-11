using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oostel.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changeAfieldName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Area",
                table: "Landlords");

            migrationBuilder.DropColumn(
                name: "Area",
                table: "Agents");

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "Landlords",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "HostelFrontViewPicture",
                table: "Hostels",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "Agents",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Street",
                table: "Landlords");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "Agents");

            migrationBuilder.AddColumn<string>(
                name: "Area",
                table: "Landlords",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "HostelFrontViewPicture",
                table: "Hostels",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "Area",
                table: "Agents",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
