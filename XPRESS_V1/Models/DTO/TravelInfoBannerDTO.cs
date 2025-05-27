namespace XPRESS_V1_Backend.Models.DTO
{
    public class TravelInfoBannerDTO
    {
        public int RequestId { get; set; }
        public string EmployeeName { get; set; }
        public string DepartmentName { get; set; }
        public string ProjectCode { get; set; }
        public string TravelModeName { get; set; }
        public string SourcePlace { get; set; }
        public string SourceCountry { get; set; }
        public string DestinationPlace  { get; set; }
        public string DestinationCountry { get; set; }
    }
}
