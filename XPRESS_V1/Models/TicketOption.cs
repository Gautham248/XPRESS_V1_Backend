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

        [Required]
        public DateTime CreatedAt { get; set; }

        // Navigation properties
        [ForeignKey("RequestId")]
        public TravelRequest TravelRequest { get; set; }

        public ICollection<TravelRequest> SelectedByTravelRequests { get; set; }
    }
}
