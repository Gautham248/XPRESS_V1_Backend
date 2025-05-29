using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XPRESS_V1_Backend.Migrations
{
    /// <inheritdoc />
    public partial class addUpdateDocumentAPIs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpiryDate",
                table: "Documents",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<string>(
                name: "IssuingPost",
                table: "Documents",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VisaClass",
                table: "Documents",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IssuingPost",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "VisaClass",
                table: "Documents");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpiryDate",
                table: "Documents",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);
        }
    }
}
