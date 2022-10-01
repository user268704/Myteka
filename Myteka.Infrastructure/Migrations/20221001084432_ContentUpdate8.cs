using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Myteka.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ContentUpdate8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Content_Books_BookId",
                table: "Content");

            migrationBuilder.DropIndex(
                name: "IX_Content_BookId",
                table: "Content");

            migrationBuilder.DropIndex(
                name: "IX_Books_ContentId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "Content");

            migrationBuilder.CreateIndex(
                name: "IX_Books_ContentId",
                table: "Books",
                column: "ContentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Books_ContentId",
                table: "Books");

            migrationBuilder.AddColumn<Guid>(
                name: "BookId",
                table: "Content",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Content_BookId",
                table: "Content",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_ContentId",
                table: "Books",
                column: "ContentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Content_Books_BookId",
                table: "Content",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
