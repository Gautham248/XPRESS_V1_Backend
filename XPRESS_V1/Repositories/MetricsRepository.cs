using XPRESS_V1_Backend.Interfaces;
using Microsoft.EntityFrameworkCore;
using XPRESS_V1_Backend.Data;
using XPRESS_V1_Backend.Models.DTO;

namespace XPRESS_V1_Backend.Repositories
{
    public class MetricsRepository : IMetricsRepository
    {
        private readonly ApiDbContext _context;

        public MetricsRepository(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AgencyBookingMetricDto>> GetAgencyBookingMetricsAsync(DateTime? startDate, DateTime? endDate, int? travelTypeId)
        {
            var query = _context.TravelRequests.AsQueryable();

            // Apply date filters (using CreatedAt, adjust if DepartureDate is needed)
            if (startDate.HasValue)
            {
                // Ensure UTC comparison if dates are stored as UTC
                var utcStartDate = DateTime.SpecifyKind(startDate.Value.Date, DateTimeKind.Utc);
                query = query.Where(tr => tr.CreatedAt >= utcStartDate);
            }
            if (endDate.HasValue)
            {
                // Include the entire end day
                var utcEndDate = DateTime.SpecifyKind(endDate.Value.Date.AddDays(1), DateTimeKind.Utc);
                query = query.Where(tr => tr.CreatedAt < utcEndDate);
            }

            // Apply TravelType filter
            // Assuming if travelTypeId is null or 0, it means "all types"
            if (travelTypeId.HasValue && travelTypeId.Value > 0)
            {
                query = query.Where(tr => tr.TravelTypeId == travelTypeId.Value);
            }

            // Filter out requests without an agency name or where it's empty,
            // as these cannot be grouped meaningfully for the graph.
            // Also ensure TotalExpense has a value, though Sum below handles nulls.
            query = query.Where(tr => !string.IsNullOrEmpty(tr.TravelAgencyName) && tr.TotalExpense.HasValue);


            var metrics = await query
                .GroupBy(tr => tr.TravelAgencyName) // Group by the travel agency name
                .Select(group => new AgencyBookingMetricDto
                {
                    TravelAgencyName = group.Key,
                    BookingCount = group.Count(),
                    TotalExpense = group.Sum(tr => tr.TotalExpense ?? 0) // Sum expenses, treat null as 0
                })
                .OrderByDescending(m => m.TotalExpense) // Optional: Order by total expense or booking count
                .ThenByDescending(m => m.BookingCount)
                .ToListAsync();

            return metrics;
        }

    }
}
