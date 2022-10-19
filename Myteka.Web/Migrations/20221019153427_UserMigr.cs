using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Myteka.Web.Migrations
{
    /// <inheritdoc />
    public partial class UserMigr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<List<Guid>>(
                name: "LikedBooks",
                table: "AspNetUsers",
                type: "uuid[]",
                nullable: true,
                oldClrType: typeof(List<Guid>),
                oldType: "uuid[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<List<Guid>>(
                name: "LikedBooks",
                table: "AspNetUsers",
                type: "uuid[]",
                nullable: false,
                oldClrType: typeof(List<Guid>),
                oldType: "uuid[]",
                oldNullable: true);
        }
    }
}
