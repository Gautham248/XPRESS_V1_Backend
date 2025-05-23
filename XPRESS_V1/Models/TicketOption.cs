using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XPRESS_V1_Backend.Models
{
    public class TicketOption
    {
        [Key]
        public int OptionId { get; set; }

        [Required]
        public int RequestId { get; set; }

        [Required]
        public string OptionDescription { get; set; }

        public string AirlineName { get; set; }

        public string FlightNumber { get; set; }

        public DateTime? DepartureTime { get; set; }

        public DateTime? ArrivalTime { get; set; }

        public decimal? Price { get; set; }

        public string BookingClass { get; set; }

        public string AdditionalDetails { get; set; }

        [Required]
        public bool IsAvailable { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public int CreatedBy { get; set; }

        // Navigation properties
        [ForeignKey("RequestId")]
        public TravelRequest TravelRequest { get; set; }

        [ForeignKey("CreatedBy")]
        public User Creator { get; set; }

        public ICollection<TravelRequest> SelectedByTravelRequests { get; set; }
    }
}
