using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XPRESS_V1_Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddIsSelectedToTicketOption : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSelected",
                table: "TicketOptions",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSelected",
                table: "TicketOptions");
        }
    }
}
