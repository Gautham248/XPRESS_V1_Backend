namespace XPRESS_V1_Backend.Models.DTO
{
    public class VoterIDDTO
    {
        public int DocumentTypeId { get; set; }
        public string IdentificationNumber { get; set; }
        public string IssuingCountry { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime? ExpiryDate { get; set; }  
        public int EmployeeID { get; set; }
    }

}
