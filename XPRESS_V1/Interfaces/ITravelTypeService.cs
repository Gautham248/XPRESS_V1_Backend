using XPRESS_V1_Backend.Models;

namespace XPRESS_V1_Backend.Interfaces
{
    public interface ITravelTypeService
    {
        Task<TravelType> CreateTravelTypeAsync(TravelType travelType);
        Task<TravelType> GetTravelTypeByIdAsync(int travelTypeId);
        Task<IEnumerable<TravelType>> GetAllTravelTypesAsync();
        Task<TravelType> UpdateTravelTypeAsync(int travelTypeId, TravelType travelType);
        Task<bool> DeleteTravelTypeAsync(int travelTypeId);
        Task<IEnumerable<TravelRequest>> GetTravelRequestsByTypeAsync(int travelTypeId);
    }
}
