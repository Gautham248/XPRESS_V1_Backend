namespace XPRESS_V1_Backend.Models.DTO
{
    public class IdentificationDTO
    {
        public string DocumentNumber { get; set; }
        public string Type { get; set; } // Aadhar, Voter ID, etc.
        public string IssuingCountry { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int EmployeeID { get; set; }
    }
}
