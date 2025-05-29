using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace XPRESS_V1_Backend.Migrations
{
    /// <inheritdoc />
    public partial class SinglePostPutMethods : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Identifications");

            migrationBuilder.AddColumn<int>(
                name: "IDTypeId",
                table: "Visas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IDTypeId",
                table: "Passports",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Aadhars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AadharNumber = table.Column<string>(type: "character varying(12)", maxLength: 12, nullable: false),
                    DocumentPath = table.Column<string>(type: "text", nullable: false),
                    EmployeeId = table.Column<int>(type: "integer", nullable: false),
                    IDTypeId = table.Column<int>(type: "integer", nullable: false),
                    UploadDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aadhars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Aadhars_IDTypes_IDTypeId",
                        column: x => x.IDTypeId,
                        principalTable: "IDTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Aadhars_Users_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Users",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Visas_IDTypeId",
                table: "Visas",
                column: "IDTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Passports_IDTypeId",
                table: "Passports",
                column: "IDTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Aadhars_EmployeeId",
                table: "Aadhars",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Aadhars_IDTypeId",
                table: "Aadhars",
                column: "IDTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Passports_IDTypes_IDTypeId",
                table: "Passports",
                column: "IDTypeId",
                principalTable: "IDTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Visas_IDTypes_IDTypeId",
                table: "Visas",
                column: "IDTypeId",
                principalTable: "IDTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Passports_IDTypes_IDTypeId",
                table: "Passports");

            migrationBuilder.DropForeignKey(
                name: "FK_Visas_IDTypes_IDTypeId",
                table: "Visas");

            migrationBuilder.DropTable(
                name: "Aadhars");

            migrationBuilder.DropIndex(
                name: "IX_Visas_IDTypeId",
                table: "Visas");

            migrationBuilder.DropIndex(
                name: "IX_Passports_IDTypeId",
                table: "Passports");

            migrationBuilder.DropColumn(
                name: "IDTypeId",
                table: "Visas");

            migrationBuilder.DropColumn(
                name: "IDTypeId",
                table: "Passports");

            migrationBuilder.CreateTable(
                name: "Identifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EmployeeId = table.Column<int>(type: "integer", nullable: false),
                    IDTypeId = table.Column<int>(type: "integer", nullable: false),
                    DocumentPath = table.Column<string>(type: "text", nullable: false),
                    IDNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    UploadDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Identifications_IDTypes_IDTypeId",
                        column: x => x.IDTypeId,
                        principalTable: "IDTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Identifications_Users_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Users",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Identifications_EmployeeId",
                table: "Identifications",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Identifications_IDTypeId",
                table: "Identifications",
                column: "IDTypeId");
        }
    }
}
