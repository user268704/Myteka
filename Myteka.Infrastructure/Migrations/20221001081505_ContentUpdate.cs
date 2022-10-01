using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Myteka.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ContentUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentId",
                table: "Books");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateDate",
                table: "ContentMetadata",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ContentMetadata",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.CreateIndex(
                name: "IX_Content_BookId",
                table: "Content",
                column: "BookId",
                unique: true);

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

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateDate",
                table: "ContentMetadata",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ContentMetadata",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<Guid>(
                name: "ContentId",
                table: "Books",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
