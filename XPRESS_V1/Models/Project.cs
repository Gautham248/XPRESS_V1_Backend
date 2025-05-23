using System.ComponentModel.DataAnnotations;

namespace XPRESS_V1_Backend.Models
{
    public class Project
    {
        [Key]
        public string ProjectCode { get; set; }

        [Required]
        public string ProjectName { get; set; }

        public string ProjectDescription { get; set; }

        public string ProjectManager { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        // Navigation property
        public ICollection<TravelRequest> TravelRequests { get; set; }
    }

}
