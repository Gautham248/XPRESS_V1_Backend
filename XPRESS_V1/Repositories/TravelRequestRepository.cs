using Microsoft.EntityFrameworkCore;
using XPRESS_V1_Backend.Data;
using XPRESS_V1_Backend.Interfaces;
using XPRESS_V1_Backend.Models;
using XPRESS_V1_Backend.Models.DTO;

namespace XPRESS_V1_Backend.Repositories
{
    public class TravelRequestRepository : ITravelRequestService
    {
        private readonly ApiDbContext _context;

        public TravelRequestRepository(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<TravelRequest> CreateTravelRequestAsync(TravelRequest travelRequest)
        {
            travelRequest.CreatedAt = new DateTime(2025, 5, 23, 15, 33, 0);
            travelRequest.UpdatedAt = new DateTime(2025, 5, 23, 15, 33, 0);
            await _context.TravelRequests.AddAsync(travelRequest);
            await _context.SaveChangesAsync();
            return travelRequest;
        }

        //public async Task<TravelRequest> GetTravelRequestByIdAsync(int requestId)
        //{
        //    return await _context.TravelRequests
        //        .Include(tr => tr.Employee)
        //        .Include(tr => tr.TravelType)
        //        .Include(tr => tr.TripType)
        //        .Include(tr => tr.Project)
        //        .Include(tr => tr.TravelMode)
        //        .Include(tr => tr.CurrentStatus)
        //        .Include(tr => tr.SelectedTicketOption)
        //        .FirstOrDefaultAsync(tr => tr.RequestId == requestId);
        //}
        
        public async Task<TravelRequest> GetTravelRequestByIdAsync(int requestId)
        {
            return await _context.TravelRequests
                .Include(tr => tr.Employee)
                .Include(tr => tr.TravelType)
                .Include(tr => tr.TripType)
                .Include(tr => tr.Project)
                .Include(tr => tr.TravelMode)
                .Include(tr => tr.CurrentStatus)
                .FirstOrDefaultAsync(tr => tr.RequestId == requestId);
        }

        public async Task UpdateTravelRequestStatusAsync(int requestId, int newStatusId, DateTime updatedAt)
        {
            var travelRequest = await _context.TravelRequests.FindAsync(requestId);
            if (travelRequest == null)
            {
                throw new Exception("Travel request not found.");
            }

            travelRequest.CurrentStatusId = newStatusId;
            travelRequest.UpdatedAt = updatedAt;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TravelRequest>> GetAllTravelRequestsAsync()
        {
            return await _context.TravelRequests
                .Include(tr => tr.Employee)
                .Include(tr => tr.TravelType)
                .Include(tr => tr.TripType)
                .Include(tr => tr.Project)
                .Include(tr => tr.TravelMode)
                .Include(tr => tr.CurrentStatus)
                .Include(tr => tr.SelectedTicketOption)
                .ToListAsync();
        }

        public async Task<TravelRequest> UpdateTravelRequestAsync(int requestId, TravelRequest travelRequest)
        {
            var existingRequest = await _context.TravelRequests.FindAsync(requestId);
            if (existingRequest == null)
                return null;

            existingRequest.EmployeeId = travelRequest.EmployeeId;
            existingRequest.TravelTypeId = travelRequest.TravelTypeId;
            existingRequest.TripTypeId = travelRequest.TripTypeId;
            existingRequest.ProjectCode = travelRequest.ProjectCode;
            existingRequest.SourcePlace = travelRequest.SourcePlace;
            existingRequest.SourceCountry = travelRequest.SourceCountry;
            existingRequest.DestinationPlace = travelRequest.DestinationPlace;
            existingRequest.DestinationCountry = travelRequest.DestinationCountry;
            existingRequest.DepartureDate = travelRequest.DepartureDate;
            existingRequest.ReturnDate = travelRequest.ReturnDate;
            existingRequest.TravelModeId = travelRequest.TravelModeId;
            existingRequest.IsAccommodationRequired = travelRequest.IsAccommodationRequired;
            existingRequest.IsPickupRequired = travelRequest.IsPickupRequired;
            existingRequest.IsDropoffRequired = travelRequest.IsDropoffRequired;
            existingRequest.PickupLocation = travelRequest.PickupLocation;
            existingRequest.DropoffLocation = travelRequest.DropoffLocation;
            existingRequest.Comments = travelRequest.Comments;
            existingRequest.PurposeOfTravel = travelRequest.PurposeOfTravel;
            existingRequest.FoodPreference = travelRequest.FoodPreference;
            existingRequest.AttendedCct = travelRequest.AttendedCct;
            existingRequest.CurrentStatusId = travelRequest.CurrentStatusId;
            existingRequest.SelectedTicketOptionId = travelRequest.SelectedTicketOptionId;
            existingRequest.TravelAgencyName = travelRequest.TravelAgencyName;
            existingRequest.TotalExpense = travelRequest.TotalExpense;
            existingRequest.UploadedTicketPdfPath = travelRequest.UploadedTicketPdfPath;
            existingRequest.UpdatedAt = new DateTime(2025, 5, 23, 15, 33, 0);

            await _context.SaveChangesAsync();
            return existingRequest;
        }

        public async Task<bool> DeleteTravelRequestAsync(int requestId)
        {
            var travelRequest = await _context.TravelRequests.FindAsync(requestId);
            if (travelRequest == null)
                return false;

            _context.TravelRequests.Remove(travelRequest);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<TicketOption>> GetTicketOptionsForRequestAsync(int requestId)
        {
            return await _context.TicketOptions
                .Where(to => to.RequestId == requestId)
                //.Include(to => to.Creator)
                .ToListAsync();
        }

        public async Task<IEnumerable<RequestApproval>> GetApprovalsForRequestAsync(int requestId)
        {
            return await _context.RequestApprovals
                .Where(ra => ra.RequestId == requestId)
                .Include(ra => ra.Approver)
                .Include(ra => ra.PreviousStatus)
                .Include(ra => ra.NewStatus)
                .ToListAsync();
        }

        public async Task<IEnumerable<AuditLog>> GetAuditLogsForRequestAsync(int requestId)
        {
            return await _context.AuditLogs
                .Where(al => al.RequestId == requestId)
                .Include(al => al.User)
                .Include(al => al.OldStatus)
                .Include(al => al.NewStatus)
                .ToListAsync();
        }

        public async Task<IEnumerable<TravelRequest>> GetTravelRequestsByEmployeeAsync(int employeeId)
        {
            return await _context.TravelRequests
                .Where(tr => tr.EmployeeId == employeeId)
                .Include(tr => tr.Employee)
                .Include(tr => tr.TravelType)
                .Include(tr => tr.TripType)
                .Include(tr => tr.Project)
                .Include(tr => tr.TravelMode)
                .Include(tr => tr.CurrentStatus)
                .Include(tr => tr.SelectedTicketOption)
                .ToListAsync();
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users
                .Include(u => u.ReportingManager)
                .Include(u => u.DuHead)
                .ToListAsync();
        }

        public async Task<IEnumerable<Project>> GetAllProjectsAsync()
        {
            return await _context.Projects.ToListAsync();
        }

        public async Task<IEnumerable<TravelType>> GetAllTravelTypesAsync()
        {
            return await _context.TravelTypes.ToListAsync();
        }

        public async Task<IEnumerable<TravelMode>> GetAllTravelModesAsync()
        {
            return await _context.TravelModes.ToListAsync();
        }

        public async Task<IEnumerable<TripType>> GetAllTripTypesAsync()
        {
            return await _context.TripTypes.ToListAsync();
        }

        public async Task<IEnumerable<RequestStatus>> GetAllRequestStatusesAsync()
        {
            return await _context.RequestStatuses.ToListAsync();
        }

        // Info Banner Join Query
        public async Task<List<TravelInfoBannerDTO>> GetTravelInfoBannerDetailsAsync(int requestId)
        {
            var query = from tr in _context.TravelRequests
                        join user in _context.Users on tr.EmployeeId equals user.EmployeeId
                        join proj in _context.Projects on tr.ProjectCode equals proj.ProjectCode
                        join mode in _context.TravelModes on tr.TravelModeId equals mode.TravelModeId
                        where tr.RequestId == requestId
                        select new TravelInfoBannerDTO
                        {
                            RequestId = tr.RequestId,
                            EmployeeName = user.FirstName + " " + user.LastName,
                            DepartmentName = user.Department,
                            ProjectCode = proj.ProjectCode,
                            TravelModeName = mode.TravelModeName,
                            SourcePlace = tr.SourcePlace,
                            SourceCountry = tr.SourceCountry,
                            DestinationPlace = tr.DestinationPlace,
                            DestinationCountry = tr.DestinationCountry
                        };

            return await query.ToListAsync();
        }

        // Travel Info Join Query
        public async Task<List<TravelInfoDetailsDTO>> GetTravelInfoDetailsAsync(int requestId)
        {
            var query = from tr in _context.TravelRequests
                        join mode in _context.TravelModes on tr.TravelModeId equals mode.TravelModeId
                        join type in _context.TravelTypes on tr.TravelTypeId equals type.TravelTypeId
                        where tr.RequestId == requestId
                        select new TravelInfoDetailsDTO
                        {
                            RequestId = tr.RequestId,
                            DepartureDate = tr.DepartureDate,
                            ReturnDate = tr.ReturnDate,
                            Transportation = mode.TravelModeName,
                            TravelTypeName = type.TravelTypeName,
                            RequestCreateDate = tr.CreatedAt,
                            PurposeOfTravel = tr.PurposeOfTravel,
                            IsAccommodationRequired = tr.IsAccommodationRequired,
                            FoodPreference = tr.FoodPreference,
                            PickupLocation = tr.PickupLocation,
                            DropoffLocation = tr.DropoffLocation
                        };
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<object>> GetAllTestTablesAsync()
        {
            return await _context.TestTables.ToListAsync<object>();
        }

        public async Task<IEnumerable<object>> GetAllAdvaitAsync()
        {
            return await _context.Advait.ToListAsync<object>();
        }

        public async Task<IEnumerable<object>> GetAllTestTablesGeorgeAsync()
        {
            return await _context.TestTables_George.ToListAsync<object>();
        }

        public async Task<IEnumerable<object>> GetAllRionaAsync()
        {
            return await _context.Riona.ToListAsync<object>();
        }

        public async Task<IEnumerable<object>> GetAllMaheshsAsync()
        {
            return await _context.Maheshs.ToListAsync<object>();
        }
        public async Task<IEnumerable<CalendarTravelRequestDTO>> GetCalendarTravelRequestsAsync()
        {
            return await _context.TravelRequests
                .Include(tr => tr.Employee) // Join with User table for EmployeeName
                .Select(tr => new CalendarTravelRequestDTO
                {
                    RequestId = tr.RequestId,
                    DepartureDate = tr.DepartureDate,
                    ReturnDate = tr.ReturnDate,
                    EmployeeName = tr.Employee.FirstName, // Adjust if you need FirstName + LastName
                    SourcePlace = tr.SourcePlace,
                    SourceCountry = tr.SourceCountry,
                    DestinationPlace = tr.DestinationPlace,
                    DestinationCountry = tr.DestinationCountry,
                    CurrentStatusName = tr.CurrentStatus != null ? tr.CurrentStatus.StatusName : "Unknown"
                })
                .ToListAsync();
        }
    }
}
