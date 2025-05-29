using Microsoft.AspNetCore.Mvc;

namespace XPRESS_V1_Backend.Models.DTO
{
    public class AgencyBookingFilterDto
    {

        [FromQuery(Name = "startDate")]
        public DateTime? StartDate { get; set; }

        [FromQuery(Name = "endDate")]
        public DateTime? EndDate { get; set; }

        [FromQuery(Name = "travelModeId")]
        public int? TravelTypeId { get; set; }
    }
}
