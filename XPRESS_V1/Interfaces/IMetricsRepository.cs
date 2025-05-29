using XPRESS_V1_Backend.Models.DTO;

namespace XPRESS_V1_Backend.Interfaces
{
    public interface IMetricsRepository
    {
        Task<IEnumerable<AgencyBookingMetricDto>> GetAgencyBookingMetricsAsync(DateTime? startDate, DateTime? endDate, int? travelTypeId);
    }
}
