using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using XPRESS_V1_Backend.Data;
using XPRESS_V1_Backend.Interfaces;
using XPRESS_V1_Backend.Models;
using XPRESS_V1_Backend.Models.DTO;
using XPRESS_V1_Backend.Repositories;

namespace XPRESS_V1_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TravelRequestController : ControllerBase
    {
        private readonly ITravelRequestService _travelRequestService;
        private readonly IAuditLogService _auditLogService;
        private readonly ApiDbContext _context;
        private readonly IMapper _mapper;

        public TravelRequestController(ITravelRequestService travelRequestService, IAuditLogService auditLogService, ApiDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _travelRequestService = travelRequestService;
            _auditLogService = auditLogService;
            _context = context;
        }

        // Helper method to ensure UTC DateTime
        private DateTime ToUtc(DateTime dt)
        {
            if (dt.Kind == DateTimeKind.Utc) return dt;
            if (dt.Kind == DateTimeKind.Local) return dt.ToUniversalTime();
            return DateTime.SpecifyKind(dt, DateTimeKind.Utc);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTravelRequest([FromBody] TravelRequestCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var departureDateUtc = ToUtc(dto.DepartureDate);
            var returnDateUtc = ToUtc(dto.ReturnDate);
            var nowUtc = ToUtc(new DateTime(2025, 5, 24, 7, 19, 0)); // 12:49 PM IST = 07:19 UTC

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
            };

            return CreatedAtAction(nameof(CreateTravelRequest), new { id = resultDto.RequestId }, resultDto);
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateTravelRequestStatus(int id, [FromBody] UpdateTravelRequestStatusDto requestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Fetch the existing travel request to get the old status
            var travelRequest = await _travelRequestService.GetTravelRequestByIdAsync(id);
            if (travelRequest == null)
            {
                return NotFound("Travel request not found.");
            }

            // Validate that the new status exists in the RequestStatuses table
            var statusExists = await _context.RequestStatuses.AnyAsync(rs => rs.StatusId == requestDto.CurrentStatusId);
            if (!statusExists)
            {
                return BadRequest("Invalid status ID.");
            }

            // Capture the old status for the audit log
            int oldStatusId = travelRequest.CurrentStatusId;

            // Update the travel request status and timestamp
            var nowUtc = ToUtc(new DateTime(2025, 5, 24, 7, 19, 0)); // 12:49 PM IST = 07:19 UTC
            await _travelRequestService.UpdateTravelRequestStatusAsync(id, requestDto.CurrentStatusId, nowUtc);

            // Create an audit log entry
            var auditLog = new AuditLog
            {
                RequestId = id,
                UserId = requestDto.UserId,
                ActionType = "STATUS_UPDATED",
                OldStatusId = oldStatusId,
                NewStatusId = requestDto.CurrentStatusId,
                ChangeDescription = $"Status updated from {oldStatusId} to {requestDto.CurrentStatusId}",
                Comments = requestDto.Comments,
                IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString(),
                Timestamp = nowUtc
            };

            await _auditLogService.CreateAuditLogAsync(auditLog);

            // Fetch the updated travel request and map to DTO
            var updatedTravelRequest = await _travelRequestService.GetTravelRequestByIdAsync(id);
            var updatedTravelRequestDto = _mapper.Map<TravelRequestDto>(updatedTravelRequest);

            return Ok(updatedTravelRequestDto);
        }

        [HttpGet("travel-requests")]
        public async Task<ActionResult<IEnumerable<TravelRequestDto>>> GetTravelRequestsDto()
        {
            var travelRequests = await _context.TravelRequests
                .Include(t => t.Employee)
                .Include(t => t.TravelType)
                .Include(t => t.TripType)
                .Include(t => t.Project)
                .Include(t => t.TravelMode)
                .Include(t => t.CurrentStatus)
                .Include(t => t.SelectedTicketOption)
                .ToListAsync();

            var travelRequestDtos = _mapper.Map<List<TravelRequestDto>>(travelRequests);
            return Ok(travelRequestDtos);
        }

        [HttpGet("{requestId}")]
        public async Task<IActionResult> GetTravelRequestById(int requestId)
        {
            var travelRequest = await _travelRequestService.GetTravelRequestByIdAsync(requestId);
            return Ok(travelRequest);
        }

        // Get InfoBanner Details
        [HttpGet("infobanner/{requestId}")]
        public async Task<IActionResult> GetTravelInfoBannerDetails(int requestId)
        {
            var details = await _travelRequestService.GetTravelInfoBannerDetailsAsync(requestId);
            if (details == null || !details.Any())
            {
                return NotFound($"No travel request found with RequestId = {requestId}");
            }
            return Ok(details);
        }

        // Get TraveInfo Details
        [HttpGet("travelinfo/{requestId}")]
        public async Task<IActionResult> GetTravelInfoDetails(int requestId)
        {
            var travelInfo = await _travelRequestService.GetTravelInfoDetailsAsync(requestId);
            if (travelInfo == null || !travelInfo.Any())
            {
                return NotFound($"No travel request found with RequestId = {requestId}");
            }
            return Ok(travelInfo);
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
        [HttpGet("Calendar")]
        public async Task<ActionResult<IEnumerable<CalendarTravelRequestDTO>>> GetFilteredTravelRequests()
        {
                // Use the injected _travelRequestService instead of directly calling TravelRequestRepository
             var filteredTravelRequests = await _travelRequestService.GetCalendarTravelRequestsAsync();
              return Ok(filteredTravelRequests);
            
           
        }
    }
}