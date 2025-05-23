using XPRESS_V1_Backend.Models;

namespace XPRESS_V1_Backend.Interfaces
{

    public interface IRequestStatusService
    {
        Task<RequestStatus> CreateRequestStatusAsync(RequestStatus requestStatus);
        Task<RequestStatus> GetRequestStatusByIdAsync(int statusId);
        Task<IEnumerable<RequestStatus>> GetAllRequestStatusesAsync();
        Task<RequestStatus> UpdateRequestStatusAsync(int statusId, RequestStatus requestStatus);
        Task<bool> DeleteRequestStatusAsync(int statusId);
        Task<IEnumerable<TravelRequest>> GetTravelRequestsByStatusAsync(int statusId);
    }
}
