using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XPRESS_V1_Backend.Models
{
    public class Document
    {
        [Key]
        public int DocumentID { get; set; }

        [Required]
        public int EmployeeID { get; set; }

        [Required]
        public int DocumentTypeID { get; set; }

        [Required]
        public string DocumentNumber { get; set; }

        public string IssuingCountry { get; set; }
        public string IssuingPost {  get; set; }

        public string IssuingAuthority { get; set; }

        public DateTime IssueDate { get; set; }

        public DateTime? ExpiryDate { get; set; }
        public string VisaClass { get; set; }

        public string DocumentLink { get; set; }

        public bool? IsValid { get; set; }

        public string Comments { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int? ModifiedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Navigation Property
        [ForeignKey("DocumentTypeID")]
        public DocumentType DocumentType { get; set; }

        public User Employee { get; set; }
    }
}
