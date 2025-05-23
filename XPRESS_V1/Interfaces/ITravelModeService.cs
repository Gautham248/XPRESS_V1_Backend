using XPRESS_V1_Backend.Models;

namespace XPRESS_V1_Backend.Interfaces
{
    public interface ITravelModeService
    {
        Task<TravelMode> CreateTravelModeAsync(TravelMode travelMode);
        Task<TravelMode> GetTravelModeByIdAsync(int travelModeId);
        Task<IEnumerable<TravelMode>> GetAllTravelModesAsync();
        Task<TravelMode> UpdateTravelModeAsync(int travelModeId, TravelMode travelMode);
        Task<bool> DeleteTravelModeAsync(int travelModeId);
        Task<IEnumerable<TravelRequest>> GetTravelRequestsByModeAsync(int travelModeId);
    }
}
