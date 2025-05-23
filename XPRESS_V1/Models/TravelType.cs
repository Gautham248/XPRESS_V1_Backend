using System.ComponentModel.DataAnnotations;

namespace XPRESS_V1_Backend.Models
{
    public class TravelType
    {
        [Key]
        public int TravelTypeId { get; set; }

        [Required]
        public string TravelTypeName { get; set; }

        public string Description { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        // Navigation property
        public ICollection<TravelRequest> TravelRequests { get; set; }
    }
}
