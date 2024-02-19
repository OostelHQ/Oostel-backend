using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oostel.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class rectifycommentmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_AuthorId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Hostels_HostelId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_AuthorId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_HostelId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Comments");

            migrationBuilder.AlterColumn<string>(
                name: "HostelId",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CommentId",
                table: "Comments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CommenterId",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ParentCommentId",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CommentId",
                table: "Comments",
                column: "CommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Comments_CommentId",
                table: "Comments",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Comments_CommentId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_CommentId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "CommentId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "CommenterId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "ParentCommentId",
                table: "Comments");

            migrationBuilder.AlterColumn<string>(
                name: "HostelId",
                table: "Comments",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "Comments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_AuthorId",
                table: "Comments",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_HostelId",
                table: "Comments",
                column: "HostelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_AuthorId",
                table: "Comments",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Hostels_HostelId",
                table: "Comments",
                column: "HostelId",
                principalTable: "Hostels",
                principalColumn: "Id");
        }
    }
}
