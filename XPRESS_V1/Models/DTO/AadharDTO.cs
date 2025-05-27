namespace XPRESS_V1_Backend.Models.DTO
{
    public class AadharDTO
    {
        public int DocumentTypeId { get; set; }
        public string DocumentNumber { get; set; }
        public string IssuingCountry { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public int EmployeeID { get; set; }
    }
}
