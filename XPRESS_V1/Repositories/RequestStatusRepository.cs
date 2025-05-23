using Microsoft.EntityFrameworkCore;
using XPRESS_V1_Backend.Data;
using XPRESS_V1_Backend.Interfaces;
using XPRESS_V1_Backend.Models;

namespace XPRESS_V1_Backend.Repositories
{
    public class RequestStatusRepository : IRequestStatusService
    {
        private readonly ApiDbContext _context;

        public RequestStatusRepository(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<RequestStatus> CreateRequestStatusAsync(RequestStatus requestStatus)
        {
            requestStatus.CreatedAt = new DateTime(2025, 5, 23, 13, 46, 0);
            await _context.RequestStatuses.AddAsync(requestStatus);
            await _context.SaveChangesAsync();
            return requestStatus;
        }

        public async Task<RequestStatus> GetRequestStatusByIdAsync(int statusId)
        {
            return await _context.RequestStatuses.FindAsync(statusId);
        }

        public async Task<IEnumerable<RequestStatus>> GetAllRequestStatusesAsync()
        {
            return await _context.RequestStatuses.ToListAsync();
        }

        public async Task<RequestStatus> UpdateRequestStatusAsync(int statusId, RequestStatus requestStatus)
        {
            var existingStatus = await _context.RequestStatuses.FindAsync(statusId);
            if (existingStatus == null)
                return null;

            existingStatus.StatusName = requestStatus.StatusName;
            existingStatus.StatusDescription = requestStatus.StatusDescription;
            existingStatus.SequenceOrder = requestStatus.SequenceOrder;
            existingStatus.IsActive = requestStatus.IsActive;

            await _context.SaveChangesAsync();
            return existingStatus;
        }

        public async Task<bool> DeleteRequestStatusAsync(int statusId)
        {
            var status = await _context.RequestStatuses.FindAsync(statusId);
            if (status == null)
                return false;

            _context.RequestStatuses.Remove(status);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<TravelRequest>> GetTravelRequestsByStatusAsync(int statusId)
        {
            return await _context.TravelRequests
                .Where(tr => tr.CurrentStatusId == statusId)
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
