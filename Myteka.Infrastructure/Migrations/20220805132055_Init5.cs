using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Myteka.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "ContentMetadata");

            migrationBuilder.DropColumn(
                name: "MimeType",
                table: "ContentMetadata");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "ContentMetadata");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Content");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Content");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "ContentMetadata",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Content",
                newName: "FileName");

            migrationBuilder.AlterColumn<int>(
                name: "Size",
                table: "ContentMetadata",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "ContentMetadata",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "Content",
                newName: "Type");

            migrationBuilder.AlterColumn<long>(
                name: "Size",
                table: "ContentMetadata",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "ContentMetadata",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MimeType",
                table: "ContentMetadata",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "Version",
                table: "ContentMetadata",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Content",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Content",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
