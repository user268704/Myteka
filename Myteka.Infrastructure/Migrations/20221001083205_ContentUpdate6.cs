using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Myteka.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ContentUpdate6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Content_BookId",
                table: "Content",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Content_Books_BookId",
                table: "Content",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Content_Books_BookId",
                table: "Content");

            migrationBuilder.DropIndex(
                name: "IX_Content_BookId",
                table: "Content");
        }
    }
}
