using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace XPRESS_V1_Backend.Migrations
{
    /// <inheritdoc />
    public partial class xpressv1_testing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ProjectCode = table.Column<string>(type: "text", nullable: false),
                    ProjectName = table.Column<string>(type: "text", nullable: false),
                    ProjectDescription = table.Column<string>(type: "text", nullable: false),
                    ProjectManager = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ProjectCode);
                });

            migrationBuilder.CreateTable(
                name: "RequestStatuses",
                columns: table => new
                {
                    StatusId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StatusName = table.Column<string>(type: "text", nullable: false),
                    StatusDescription = table.Column<string>(type: "text", nullable: false),
                    SequenceOrder = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestStatuses", x => x.StatusId);
                });

            migrationBuilder.CreateTable(
                name: "TravelModes",
                columns: table => new
                {
                    TravelModeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TravelModeName = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelModes", x => x.TravelModeId);
                });

            migrationBuilder.CreateTable(
                name: "TravelTypes",
                columns: table => new
                {
                    TravelTypeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TravelTypeName = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelTypes", x => x.TravelTypeId);
                });

            migrationBuilder.CreateTable(
                name: "TripTypes",
                columns: table => new
                {
                    TripTypeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TripTypeName = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripTypes", x => x.TripTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false),
                    Department = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ReportingManagerId = table.Column<int>(type: "integer", nullable: true),
                    DuHeadId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_Users_Users_DuHeadId",
                        column: x => x.DuHeadId,
                        principalTable: "Users",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Users_ReportingManagerId",
                        column: x => x.ReportingManagerId,
                        principalTable: "Users",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    LogId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RequestId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ActionType = table.Column<string>(type: "text", nullable: false),
                    OldStatusId = table.Column<int>(type: "integer", nullable: true),
                    NewStatusId = table.Column<int>(type: "integer", nullable: true),
                    ChangeDescription = table.Column<string>(type: "text", nullable: false),
                    Comments = table.Column<string>(type: "text", nullable: false),
                    IpAddress = table.Column<string>(type: "text", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.LogId);
                    table.ForeignKey(
                        name: "FK_AuditLogs_RequestStatuses_NewStatusId",
                        column: x => x.NewStatusId,
                        principalTable: "RequestStatuses",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AuditLogs_RequestStatuses_OldStatusId",
                        column: x => x.OldStatusId,
                        principalTable: "RequestStatuses",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AuditLogs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequestApprovals",
                columns: table => new
                {
                    ApprovalId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RequestId = table.Column<int>(type: "integer", nullable: false),
                    ApproverId = table.Column<int>(type: "integer", nullable: false),
                    ApprovalLevel = table.Column<string>(type: "text", nullable: false),
                    ActionTaken = table.Column<string>(type: "text", nullable: false),
                    Comments = table.Column<string>(type: "text", nullable: false),
                    PreviousStatusId = table.Column<int>(type: "integer", nullable: true),
                    NewStatusId = table.Column<int>(type: "integer", nullable: false),
                    ActionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestApprovals", x => x.ApprovalId);
                    table.ForeignKey(
                        name: "FK_RequestApprovals_RequestStatuses_NewStatusId",
                        column: x => x.NewStatusId,
                        principalTable: "RequestStatuses",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequestApprovals_RequestStatuses_PreviousStatusId",
                        column: x => x.PreviousStatusId,
                        principalTable: "RequestStatuses",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequestApprovals_Users_ApproverId",
                        column: x => x.ApproverId,
                        principalTable: "Users",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TicketOptions",
                columns: table => new
                {
                    OptionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RequestId = table.Column<int>(type: "integer", nullable: false),
                    OptionDescription = table.Column<string>(type: "text", nullable: false),
                    AirlineName = table.Column<string>(type: "text", nullable: false),
                    FlightNumber = table.Column<string>(type: "text", nullable: false),
                    DepartureTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ArrivalTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Price = table.Column<decimal>(type: "numeric", nullable: true),
                    BookingClass = table.Column<string>(type: "text", nullable: false),
                    AdditionalDetails = table.Column<string>(type: "text", nullable: false),
                    IsAvailable = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketOptions", x => x.OptionId);
                    table.ForeignKey(
                        name: "FK_TicketOptions_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TravelRequests",
                columns: table => new
                {
                    RequestId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EmployeeId = table.Column<int>(type: "integer", nullable: false),
                    TravelTypeId = table.Column<int>(type: "integer", nullable: false),
                    TripTypeId = table.Column<int>(type: "integer", nullable: false),
                    ProjectCode = table.Column<string>(type: "text", nullable: false),
                    SourcePlace = table.Column<string>(type: "text", nullable: false),
                    SourceCountry = table.Column<string>(type: "text", nullable: false),
                    DestinationPlace = table.Column<string>(type: "text", nullable: false),
                    DestinationCountry = table.Column<string>(type: "text", nullable: false),
                    DepartureDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ReturnDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    TravelModeId = table.Column<int>(type: "integer", nullable: false),
                    IsAccommodationRequired = table.Column<bool>(type: "boolean", nullable: false),
                    IsPickupRequired = table.Column<bool>(type: "boolean", nullable: true),
                    IsDropoffRequired = table.Column<bool>(type: "boolean", nullable: true),
                    PickupLocation = table.Column<string>(type: "text", nullable: false),
                    DropoffLocation = table.Column<string>(type: "text", nullable: false),
                    Comments = table.Column<string>(type: "text", nullable: false),
                    PurposeOfTravel = table.Column<string>(type: "text", nullable: false),
                    FoodPreference = table.Column<string>(type: "text", nullable: false),
                    AttendedCct = table.Column<bool>(type: "boolean", nullable: true),
                    CurrentStatusId = table.Column<int>(type: "integer", nullable: false),
                    SelectedTicketOptionId = table.Column<int>(type: "integer", nullable: true),
                    TravelAgencyName = table.Column<string>(type: "text", nullable: false),
                    TotalExpense = table.Column<decimal>(type: "numeric", nullable: true),
                    UploadedTicketPdfPath = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelRequests", x => x.RequestId);
                    table.ForeignKey(
                        name: "FK_TravelRequests_Projects_ProjectCode",
                        column: x => x.ProjectCode,
                        principalTable: "Projects",
                        principalColumn: "ProjectCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TravelRequests_RequestStatuses_CurrentStatusId",
                        column: x => x.CurrentStatusId,
                        principalTable: "RequestStatuses",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TravelRequests_TicketOptions_SelectedTicketOptionId",
                        column: x => x.SelectedTicketOptionId,
                        principalTable: "TicketOptions",
                        principalColumn: "OptionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TravelRequests_TravelModes_TravelModeId",
                        column: x => x.TravelModeId,
                        principalTable: "TravelModes",
                        principalColumn: "TravelModeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TravelRequests_TravelTypes_TravelTypeId",
                        column: x => x.TravelTypeId,
                        principalTable: "TravelTypes",
                        principalColumn: "TravelTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TravelRequests_TripTypes_TripTypeId",
                        column: x => x.TripTypeId,
                        principalTable: "TripTypes",
                        principalColumn: "TripTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TravelRequests_Users_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Users",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_NewStatusId",
                table: "AuditLogs",
                column: "NewStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_OldStatusId",
                table: "AuditLogs",
                column: "OldStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_RequestId",
                table: "AuditLogs",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_UserId",
                table: "AuditLogs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestApprovals_ApproverId",
                table: "RequestApprovals",
                column: "ApproverId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestApprovals_NewStatusId",
                table: "RequestApprovals",
                column: "NewStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestApprovals_PreviousStatusId",
                table: "RequestApprovals",
                column: "PreviousStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestApprovals_RequestId",
                table: "RequestApprovals",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketOptions_CreatedBy",
                table: "TicketOptions",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TicketOptions_RequestId",
                table: "TicketOptions",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_TravelRequests_CurrentStatusId",
                table: "TravelRequests",
                column: "CurrentStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_TravelRequests_EmployeeId",
                table: "TravelRequests",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_TravelRequests_ProjectCode",
                table: "TravelRequests",
                column: "ProjectCode");

            migrationBuilder.CreateIndex(
                name: "IX_TravelRequests_SelectedTicketOptionId",
                table: "TravelRequests",
                column: "SelectedTicketOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_TravelRequests_TravelModeId",
                table: "TravelRequests",
                column: "TravelModeId");

            migrationBuilder.CreateIndex(
                name: "IX_TravelRequests_TravelTypeId",
                table: "TravelRequests",
                column: "TravelTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TravelRequests_TripTypeId",
                table: "TravelRequests",
                column: "TripTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_DuHeadId",
                table: "Users",
                column: "DuHeadId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ReportingManagerId",
                table: "Users",
                column: "ReportingManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_AuditLogs_TravelRequests_RequestId",
                table: "AuditLogs",
                column: "RequestId",
                principalTable: "TravelRequests",
                principalColumn: "RequestId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestApprovals_TravelRequests_RequestId",
                table: "RequestApprovals",
                column: "RequestId",
                principalTable: "TravelRequests",
                principalColumn: "RequestId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketOptions_TravelRequests_RequestId",
                table: "TicketOptions",
                column: "RequestId",
                principalTable: "TravelRequests",
                principalColumn: "RequestId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TravelRequests_RequestStatuses_CurrentStatusId",
                table: "TravelRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketOptions_TravelRequests_RequestId",
                table: "TicketOptions");

            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "RequestApprovals");

            migrationBuilder.DropTable(
                name: "RequestStatuses");

            migrationBuilder.DropTable(
                name: "TravelRequests");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "TicketOptions");

            migrationBuilder.DropTable(
                name: "TravelModes");

            migrationBuilder.DropTable(
                name: "TravelTypes");

            migrationBuilder.DropTable(
                name: "TripTypes");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
