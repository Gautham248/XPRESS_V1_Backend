using Microsoft.EntityFrameworkCore;
using XPRESS_V1_Backend.Data;
using XPRESS_V1_Backend.Interfaces;
using XPRESS_V1_Backend.Models;

namespace XPRESS_V1_Backend.Repositories
{
    public class TripTypeRepository : ITripTypeService
    {
        private readonly ApiDbContext _context;

        public TripTypeRepository(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<TripType> CreateTripTypeAsync(TripType tripType)
        {
            tripType.CreatedAt = new DateTime(2025, 5, 23, 13, 46, 0);
            await _context.TripTypes.AddAsync(tripType);
            await _context.SaveChangesAsync();
            return tripType;
        }

        public async Task<TripType> GetTripTypeByIdAsync(int tripTypeId)
        {
            return await _context.TripTypes.FindAsync(tripTypeId);
        }

        public async Task<IEnumerable<TripType>> GetAllTripTypesAsync()
        {
            return await _context.TripTypes.ToListAsync();
        }

        public async Task<TripType> UpdateTripTypeAsync(int tripTypeId, TripType tripType)
        {
            var existingTripType = await _context.TripTypes.FindAsync(tripTypeId);
            if (existingTripType == null)
                return null;

            existingTripType.TripTypeName = tripType.TripTypeName;
            existingTripType.Description = tripType.Description;
            existingTripType.IsActive = tripType.IsActive;

            await _context.SaveChangesAsync();
            return existingTripType;
        }

        public async Task<bool> DeleteTripTypeAsync(int tripTypeId)
        {
            var tripType = await _context.TripTypes.FindAsync(tripTypeId);
            if (tripType == null)
                return false;

            _context.TripTypes.Remove(tripType);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<TravelRequest>> GetTravelRequestsByTripTypeAsync(int tripTypeId)
        {
            return await _context.TravelRequests
                .Where(tr => tr.TripTypeId == tripTypeId)
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
