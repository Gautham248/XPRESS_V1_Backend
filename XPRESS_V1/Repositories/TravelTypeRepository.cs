using Microsoft.EntityFrameworkCore;
using XPRESS_V1_Backend.Data;
using XPRESS_V1_Backend.Interfaces;
using XPRESS_V1_Backend.Models;

namespace XPRESS_V1_Backend.Repositories
{
    public class TravelTypeRepository : ITravelTypeService
    {
        private readonly ApiDbContext _context;

        public TravelTypeRepository(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<TravelType> CreateTravelTypeAsync(TravelType travelType)
        {
            travelType.CreatedAt = new DateTime(2025, 5, 23, 13, 46, 0);
            await _context.TravelTypes.AddAsync(travelType);
            await _context.SaveChangesAsync();
            return travelType;
        }

        public async Task<TravelType> GetTravelTypeByIdAsync(int travelTypeId)
        {
            return await _context.TravelTypes.FindAsync(travelTypeId);
        }

        public async Task<IEnumerable<TravelType>> GetAllTravelTypesAsync()
        {
            return await _context.TravelTypes.ToListAsync();
        }

        public async Task<TravelType> UpdateTravelTypeAsync(int travelTypeId, TravelType travelType)
        {
            var existingTravelType = await _context.TravelTypes.FindAsync(travelTypeId);
            if (existingTravelType == null)
                return null;

            existingTravelType.TravelTypeName = travelType.TravelTypeName;
            existingTravelType.Description = travelType.Description;
            existingTravelType.IsActive = travelType.IsActive;

            await _context.SaveChangesAsync();
            return existingTravelType;
        }

        public async Task<bool> DeleteTravelTypeAsync(int travelTypeId)
        {
            var travelType = await _context.TravelTypes.FindAsync(travelTypeId);
            if (travelType == null)
                return false;

            _context.TravelTypes.Remove(travelType);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<TravelRequest>> GetTravelRequestsByTypeAsync(int travelTypeId)
        {
            return await _context.TravelRequests
                .Where(tr => tr.TravelTypeId == travelTypeId)
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
