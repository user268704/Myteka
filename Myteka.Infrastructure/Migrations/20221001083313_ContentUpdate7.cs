using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Myteka.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ContentUpdate7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Books_ContentId",
                table: "Books",
                column: "ContentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Content_ContentId",
                table: "Books",
                column: "ContentId",
                principalTable: "Content",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Content_ContentId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_ContentId",
                table: "Books");
        }
    }
}
