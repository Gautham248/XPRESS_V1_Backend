using System.ComponentModel.DataAnnotations;

namespace XPRESS_V1_Backend.Models
{
    public class Identification
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string IDNumber { get; set; }

        public DateTime UploadDate { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;

        [Required]
        public string DocumentPath { get; set; }


        // Foreign key to IDType
        public int IDTypeId { get; set; }
        public IDType IDType { get; set; }

        // Foreign key to User
        public int EmployeeId { get; set; }
        public User Employee { get; set; }
    }
}
