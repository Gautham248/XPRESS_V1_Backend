namespace XPRESS_V1_Backend.Models.DTO
{
    public class DocumentDto
    {
        // Common fields
        public int? Id { get; set; }
        public int EmployeeId { get; set; }
        public int IDTypeId { get; set; } // 1=Passport, 2=Visa, 3=Aadhar
        public string DocumentPath { get; set; }
        public DateTime UploadDate { get; set; } = DateTime.UtcNow;

        // Passport specific fields
        public string PassportNumber { get; set; }
        public DateTime? PassportIssueDate { get; set; }
        public DateTime? PassportExpiryDate { get; set; }
        public string IssuingCountry { get; set; }

        // Visa specific fields
        public string VisaNumber { get; set; }
        public DateTime? VisaIssueDate { get; set; }
        public DateTime? VisaExpiryDate { get; set; }
        public string VisaClass { get; set; }

        // Aadhar specific fields
        public string AadharNumber { get; set; }
        public string FullName { get; set; }
        
    }
}
