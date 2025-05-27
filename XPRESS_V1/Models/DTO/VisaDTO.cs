namespace XPRESS_V1_Backend.Models.DTO
{
    public class VisaDTO
    {
        public int DocumentTypeId { get; set; }
        public string VisaNumber { get; set; }
        public string VisaClass { get; set; }
        public string IssuingCountry { get; set; }
        public string IssuingPost { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int EmployeeID { get; set; }
    }
}
