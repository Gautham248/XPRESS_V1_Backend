using System.ComponentModel.DataAnnotations;

namespace XPRESS_V1_Backend.Models
{
    public class IDType
    {
        public int Id { get; set; }

        [Required]
        public int TypeNumber { get; set; } // 1, 2, 3 as per your requirement

        [Required]
        [StringLength(50)]
        public string TypeName { get; set; } // "Aadhar", "Voter ID", "Driving Licence"

        // Navigation property
        public ICollection<Identification> Identifications { get; set; }
    }
}
