using System.ComponentModel.DataAnnotations;

namespace XPRESS_V1_Backend.Models
{
    public class TripType
    {
        [Key]
        public int TripTypeId { get; set; }

        [Required]
        public string TripTypeName { get; set; }

        public string Description { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        // Navigation property
        public ICollection<TravelRequest> TravelRequests { get; set; }
    }
}
