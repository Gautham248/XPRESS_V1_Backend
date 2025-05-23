using XPRESS_V1_Backend.Models;

namespace XPRESS_V1_Backend.Interfaces
{
    public interface ITravelRequestService
    {
        Task<TravelRequest> CreateTravelRequestAsync(TravelRequest travelRequest);
        Task<TravelRequest> GetTravelRequestByIdAsync(int requestId);
        Task<IEnumerable<TravelRequest>> GetAllTravelRequestsAsync();
        Task<TravelRequest> UpdateTravelRequestAsync(int requestId, TravelRequest travelRequest);
        Task<bool> DeleteTravelRequestAsync(int requestId);
        Task<IEnumerable<TicketOption>> GetTicketOptionsForRequestAsync(int requestId);
        Task<IEnumerable<RequestApproval>> GetApprovalsForRequestAsync(int requestId);
        Task<IEnumerable<AuditLog>> GetAuditLogsForRequestAsync(int requestId);
        Task<IEnumerable<TravelRequest>> GetTravelRequestsByEmployeeAsync(int employeeId);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<IEnumerable<Project>> GetAllProjectsAsync();
        Task<IEnumerable<TravelType>> GetAllTravelTypesAsync();
        Task<IEnumerable<TravelMode>> GetAllTravelModesAsync();
        Task<IEnumerable<TripType>> GetAllTripTypesAsync();
        Task<IEnumerable<RequestStatus>> GetAllRequestStatusesAsync();
        Task<IEnumerable<object>> GetAllTestTablesAsync();
        Task<IEnumerable<object>> GetAllAdvaitAsync();
        Task<IEnumerable<object>> GetAllTestTablesGeorgeAsync();
        Task<IEnumerable<object>> GetAllRionaAsync();
        Task<IEnumerable<object>> GetAllMaheshsAsync();
    }
}
