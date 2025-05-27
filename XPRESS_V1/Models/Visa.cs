using System.ComponentModel.DataAnnotations;

namespace XPRESS_V1_Backend.Models
{
    public class Visa
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string? VisaNumber { get; set; } // Nullable as per requirement

        [Required]
        public DateTime IssueDate { get; set; }

        [Required]
        public DateTime ExpiryDate { get; set; }

        [Required]
        [StringLength(50)]
        public string VisaClass { get; set; }

        public DateTime UploadDate { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;

        [Required]
        public string DocumentPath { get; set; } // Path to the uploaded document file

        // Foreign key to User
        public int EmployeeId { get; set; }
        public User Employee { get; set; }
    }
}
