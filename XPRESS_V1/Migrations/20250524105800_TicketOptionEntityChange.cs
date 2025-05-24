using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XPRESS_V1_Backend.Migrations
{
    /// <inheritdoc />
    public partial class TicketOptionEntityChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketOptions_Users_CreatedBy",
                table: "TicketOptions");

            migrationBuilder.DropIndex(
                name: "IX_TicketOptions_CreatedBy",
                table: "TicketOptions");

            migrationBuilder.DropColumn(
                name: "AdditionalDetails",
                table: "TicketOptions");

            migrationBuilder.DropColumn(
                name: "AirlineName",
                table: "TicketOptions");

            migrationBuilder.DropColumn(
                name: "ArrivalTime",
                table: "TicketOptions");

            migrationBuilder.DropColumn(
                name: "BookingClass",
                table: "TicketOptions");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "TicketOptions");

            migrationBuilder.DropColumn(
                name: "DepartureTime",
                table: "TicketOptions");

            migrationBuilder.DropColumn(
                name: "FlightNumber",
                table: "TicketOptions");

            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "TicketOptions");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "TicketOptions");

            migrationBuilder.AddColumn<int>(
                name: "UserEmployeeId",
                table: "TicketOptions",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TicketOptions_UserEmployeeId",
                table: "TicketOptions",
                column: "UserEmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketOptions_Users_UserEmployeeId",
                table: "TicketOptions",
                column: "UserEmployeeId",
                principalTable: "Users",
                principalColumn: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketOptions_Users_UserEmployeeId",
                table: "TicketOptions");

            migrationBuilder.DropIndex(
                name: "IX_TicketOptions_UserEmployeeId",
                table: "TicketOptions");

            migrationBuilder.DropColumn(
                name: "UserEmployeeId",
                table: "TicketOptions");

            migrationBuilder.AddColumn<string>(
                name: "AdditionalDetails",
                table: "TicketOptions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AirlineName",
                table: "TicketOptions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ArrivalTime",
                table: "TicketOptions",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BookingClass",
                table: "TicketOptions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "TicketOptions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DepartureTime",
                table: "TicketOptions",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FlightNumber",
                table: "TicketOptions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "TicketOptions",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "TicketOptions",
                type: "numeric",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TicketOptions_CreatedBy",
                table: "TicketOptions",
                column: "CreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketOptions_Users_CreatedBy",
                table: "TicketOptions",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
