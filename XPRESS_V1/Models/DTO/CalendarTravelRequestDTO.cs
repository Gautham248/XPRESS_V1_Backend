namespace XPRESS_V1_Backend.Models.DTO
{
    public class CalendarTravelRequestDTO
    {
        public int RequestId { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string EmployeeName { get; set; }
        public string SourcePlace { get; set; }
        public string SourceCountry { get; set; }
        public string DestinationPlace { get; set; }
        public string DestinationCountry { get; set; }
    }
}
