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

        [HttpPost]
        public async Task<IActionResult> CreateTravelRequest([FromBody] TravelRequestCreateDto requestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var travelRequest = new TravelRequest
            {
                EmployeeId = requestDto.EmployeeId,
                TravelTypeId = requestDto.TravelTypeId,
                TripTypeId = requestDto.TripTypeId,
                ProjectCode = requestDto.ProjectCode,
                SourcePlace = requestDto.SourcePlace,
                SourceCountry = requestDto.SourceCountry,
                DestinationPlace = requestDto.DestinationPlace,
                DestinationCountry = requestDto.DestinationCountry,
                DepartureDate = requestDto.DepartureDate,
                ReturnDate = requestDto.ReturnDate,
                TravelModeId = requestDto.TravelModeId,
                IsAccommodationRequired = requestDto.IsAccommodationRequired,
                IsPickupRequired = requestDto.IsPickupRequired,
                IsDropoffRequired = requestDto.IsDropoffRequired,
                PickupLocation = requestDto.PickupLocation,
                DropoffLocation = requestDto.DropoffLocation,
                Comments = requestDto.Comments,
                PurposeOfTravel = requestDto.PurposeOfTravel,
                FoodPreference = requestDto.FoodPreference,
                AttendedCct = requestDto.AttendedCct,
                CurrentStatusId = 1, // PendingReview
                CreatedAt = new DateTime(2025, 5, 23, 16, 0, 0), // Updated to 04:00 PM IST
                UpdatedAt = new DateTime(2025, 5, 23, 16, 0, 0)
            };

            var createdTravelRequest = await _travelRequestService.CreateTravelRequestAsync(travelRequest);

            var auditLog = new AuditLog
            {
                RequestId = createdTravelRequest.RequestId,
                UserId = requestDto.EmployeeId,
                ActionType = "REQUEST_CREATED",
                OldStatusId = null,
                NewStatusId = 1,
                ChangeDescription = "New travel request created",
                Comments = null,
                IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString(),
                Timestamp = new DateTime(2025, 5, 23, 16, 0, 0)
            };

            await _auditLogService.CreateAuditLogAsync(auditLog);

            return CreatedAtAction(nameof(CreateTravelRequest), new { id = createdTravelRequest.RequestId }, createdTravelRequest);
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