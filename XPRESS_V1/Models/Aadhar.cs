using System.ComponentModel.DataAnnotations;

namespace XPRESS_V1_Backend.Models
{
    public class Aadhar
    {
        public int Id { get; set; }

        [Required]
        [StringLength(12, MinimumLength = 12)]
        public string AadharNumber { get; set; } // Aadhar numbers are 12 digits

        public string AadharName { get; set; }

        [Required]
        public string DocumentPath { get; set; } // Path to the uploaded document file

        // Foreign key to User
        public int EmployeeId { get; set; }
        public User Employee { get; set; }

        // Foreign key to IDType (will always be "Aadhar" type)
        public int IDTypeId { get; set; }
        public DocumentType IDType { get; set; }

        public DateTime UploadDate { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;

    }
}
