using Microsoft.EntityFrameworkCore;
using XPRESS_V1_Backend.Data;
using XPRESS_V1_Backend.Interfaces;
using XPRESS_V1_Backend.Models;

namespace XPRESS_V1_Backend.Repositories
{
    public class TravelModeRepository : ITravelModeService
    {
        private readonly ApiDbContext _context;

        public TravelModeRepository(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<TravelMode> CreateTravelModeAsync(TravelMode travelMode)
        {
            travelMode.CreatedAt = new DateTime(2025, 5, 23, 13, 46, 0);
            await _context.TravelModes.AddAsync(travelMode);
            await _context.SaveChangesAsync();
            return travelMode;
        }

        public async Task<TravelMode> GetTravelModeByIdAsync(int travelModeId)
        {
            return await _context.TravelModes.FindAsync(travelModeId);
        }

        public async Task<IEnumerable<TravelMode>> GetAllTravelModesAsync()
        {
            return await _context.TravelModes.ToListAsync();
        }

        public async Task<TravelMode> UpdateTravelModeAsync(int travelModeId, TravelMode travelMode)
        {
            var existingTravelMode = await _context.TravelModes.FindAsync(travelModeId);
            if (existingTravelMode == null)
                return null;

            existingTravelMode.TravelModeName = travelMode.TravelModeName;
            existingTravelMode.Description = travelMode.Description;
            existingTravelMode.IsActive = travelMode.IsActive;

            await _context.SaveChangesAsync();
            return existingTravelMode;
        }

        public async Task<bool> DeleteTravelModeAsync(int travelModeId)
        {
            var travelMode = await _context.TravelModes.FindAsync(travelModeId);
            if (travelMode == null)
                return false;

            _context.TravelModes.Remove(travelMode);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<TravelRequest>> GetTravelRequestsByModeAsync(int travelModeId)
        {
            return await _context.TravelRequests
                .Where(tr => tr.TravelModeId == travelModeId)
                .Include(tr => tr.Employee)
                .Include(tr => tr.TravelType)
                .Include(tr => tr.TripType)
                .Include(tr => tr.Project)
                .Include(tr => tr.TravelMode)
                .Include(tr => tr.CurrentStatus)
                .ToListAsync();
        }
    }


}
