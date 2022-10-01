using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Myteka.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BookUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Content_ContentId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_ContentId",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "Theme",
                table: "Books",
                newName: "Genre");

            migrationBuilder.AddColumn<DateTime>(
                name: "UploadDate",
                table: "Books",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "WritingDate",
                table: "Books",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UploadDate",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "WritingDate",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "Genre",
                table: "Books",
                newName: "Theme");

            migrationBuilder.CreateIndex(
                name: "IX_Books_ContentId",
                table: "Books",
                column: "ContentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Content_ContentId",
                table: "Books",
                column: "ContentId",
                principalTable: "Content",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
