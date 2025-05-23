using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XPRESS_V1_Backend.Interfaces;
using XPRESS_V1_Backend.Models;
using XPRESS_V1_Backend.Models.DTO;
using System;
using System.Threading.Tasks;

namespace XPRESS_V1_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TravelRequestController : ControllerBase
    {
        private readonly ITravelRequestService _travelRequestService;
        private readonly IAuditLogService _auditLogService;

        public TravelRequestController(ITravelRequestService travelRequestService, IAuditLogService auditLogService)
        {
            _travelRequestService = travelRequestService;
            _auditLogService = auditLogService;
        }

        // Helper method to ensure UTC DateTime
        private DateTime ToUtc(DateTime dt)
        {
            if (dt.Kind == DateTimeKind.Utc) return dt;
            if (dt.Kind == DateTimeKind.Local) return dt.ToUniversalTime();
            return DateTime.SpecifyKind(dt, DateTimeKind.Utc);
        }

        [HttpPost]

        [HttpPost]
        public async Task<IActionResult> CreateTravelRequest([FromBody] TravelRequestCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var departureDateUtc = ToUtc(dto.DepartureDate);
            var returnDateUtc = ToUtc(dto.ReturnDate);
            var nowUtc = DateTime.UtcNow;

            if (departureDateUtc > returnDateUtc)
                return BadRequest("Return date must be after the departure date.");

            var travelRequest = new TravelRequest
            {
                EmployeeId = dto.EmployeeId,
                TravelTypeId = dto.TravelTypeId,
                TripTypeId = dto.TripTypeId,
                ProjectCode = dto.ProjectCode,
                SourcePlace = dto.SourcePlace,
                SourceCountry = dto.SourceCountry,
                DestinationPlace = dto.DestinationPlace,
                DestinationCountry = dto.DestinationCountry,
                DepartureDate = departureDateUtc,
                ReturnDate = returnDateUtc,
                TravelModeId = dto.TravelModeId,
                IsAccommodationRequired = dto.IsAccommodationRequired,
                IsPickupRequired = dto.IsPickupRequired,
                IsDropoffRequired = dto.IsDropoffRequired,
                PickupLocation = dto.PickupLocation,
                DropoffLocation = dto.DropoffLocation,
                Comments = dto.Comments,
                PurposeOfTravel = dto.PurposeOfTravel,
                FoodPreference = dto.FoodPreference,
                AttendedCct = dto.AttendedCct,
                CurrentStatusId = 1,
                CreatedAt = nowUtc,
                UpdatedAt = nowUtc,
                SelectedTicketOptionId = null,
                TravelAgencyName = null,
                TotalExpense = null,
                UploadedTicketPdfPath = null
            };

            var createdTravelRequest = await _travelRequestService.CreateTravelRequestAsync(travelRequest);

            var auditLog = new AuditLog
            {
                RequestId = createdTravelRequest.RequestId,
                UserId = dto.EmployeeId,
                ActionType = "REQUEST_CREATED",
                OldStatusId = null,
                NewStatusId = 1,
                ChangeDescription = "New travel request created",
                IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString(),
                Timestamp = nowUtc
            };

            await _auditLogService.CreateAuditLogAsync(auditLog);

            // Map createdTravelRequest entity to DTO here manually:
            var resultDto = new TravelRequestCreateDto
            {
                RequestId = createdTravelRequest.RequestId,
                EmployeeId = createdTravelRequest.EmployeeId,
                TravelTypeId = createdTravelRequest.TravelTypeId,
                TripTypeId = createdTravelRequest.TripTypeId,
                ProjectCode = createdTravelRequest.ProjectCode,
                SourcePlace = createdTravelRequest.SourcePlace,
                SourceCountry = createdTravelRequest.SourceCountry,
                DestinationPlace = createdTravelRequest.DestinationPlace,
                DestinationCountry = createdTravelRequest.DestinationCountry,
                DepartureDate = createdTravelRequest.DepartureDate,
                ReturnDate = createdTravelRequest.ReturnDate,
                TravelModeId = createdTravelRequest.TravelModeId,
                IsAccommodationRequired = createdTravelRequest.IsAccommodationRequired,
                IsPickupRequired = createdTravelRequest.IsPickupRequired,
                IsDropoffRequired = createdTravelRequest.IsDropoffRequired,
                PickupLocation = createdTravelRequest.PickupLocation,
                DropoffLocation = createdTravelRequest.DropoffLocation,
                Comments = createdTravelRequest.Comments,
                PurposeOfTravel = createdTravelRequest.PurposeOfTravel,
                FoodPreference = createdTravelRequest.FoodPreference,
                AttendedCct = createdTravelRequest.AttendedCct,
                //CurrentStatusId = createdTravelRequest.CurrentStatusId,
                //CreatedAt = createdTravelRequest.CreatedAt,
                //UpdatedAt = createdTravelRequest.UpdatedAt
            };

            return CreatedAtAction(nameof(CreateTravelRequest), new { id = resultDto.RequestId }, resultDto);
        }



        [HttpGet("travel-requests")]
        public async Task<IActionResult> GetAllTravelRequests()
        {
            var travelRequests = await _travelRequestService.GetAllTravelRequestsAsync();
            return Ok(travelRequests);
        }

        [HttpGet("employees")]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _travelRequestService.GetAllUsersAsync();
            return Ok(employees);
        }

        [HttpGet("status-codes")]
        public async Task<IActionResult> GetAllStatusCodes()
        {
            var statusCodes = await _travelRequestService.GetAllRequestStatusesAsync();
            return Ok(statusCodes);
        }

        [HttpGet("projects")]
        public async Task<IActionResult> GetAllProjects()
        {
            var projects = await _travelRequestService.GetAllProjectsAsync();
            return Ok(projects);
        }
    }
}
