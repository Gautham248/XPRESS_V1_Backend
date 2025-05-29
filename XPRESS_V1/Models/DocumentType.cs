using System.ComponentModel.DataAnnotations;

namespace XPRESS_V1_Backend.Models
{
    public class DocumentType
    {
        public int Id { get; set; }

        [Required]
        public int TypeNumber { get; set; } // 1 = Passport, 2 = Visa, 3 = Aadhar

        [Required]
        [StringLength(50)]
        public string TypeName { get; set; } // "Passport", "Visa", "Aadhar"

        // Navigation properties
        public ICollection<Passport> Passports { get; set; }
        public ICollection<Visa> Visas { get; set; }
        public ICollection<Aadhar> Aadhars { get; set; }
    }
}
