using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace XPRESS_V1_Backend.Models
{
    public class DocumentType
    {
        [Key]
        public int DocumentTypeID { get; set; }

        [Required]
        public string TypeName { get; set; }

        public string Description { get; set; }

        public bool RequiresExpiry { get; set; }

        public bool IsRequired { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        // Navigation Property
        [JsonIgnore]
        public ICollection<Document> Documents { get; set; }
    }
}
