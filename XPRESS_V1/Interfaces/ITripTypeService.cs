using XPRESS_V1_Backend.Models;

namespace XPRESS_V1_Backend.Interfaces
{
    public interface ITripTypeService
    {
        Task<TripType> CreateTripTypeAsync(TripType tripType);
        Task<TripType> GetTripTypeByIdAsync(int tripTypeId);
        Task<IEnumerable<TripType>> GetAllTripTypesAsync();
        Task<TripType> UpdateTripTypeAsync(int tripTypeId, TripType tripType);
        Task<bool> DeleteTripTypeAsync(int tripTypeId);
        Task<IEnumerable<TravelRequest>> GetTravelRequestsByTripTypeAsync(int tripTypeId);
    }
}
