namespace XPRESS_V1_Backend.Models.DTO
{

    public class TravelRequestDto
    {
        public int RequestId { get; set; }
        public string SourcePlace { get; set; }
        public string SourceCountry { get; set; }
        public string DestinationPlace { get; set; }
        public string DestinationCountry { get; set; }

        public DateTime DepartureDate { get; set; }
        public DateTime ReturnDate { get; set; }

        public bool IsAccommodationRequired { get; set; }
        public bool IsPickupRequired { get; set; }
        public bool IsDropoffRequired { get; set; }

        public string PickupLocation { get; set; }
        public string DropoffLocation { get; set; }
        public string Comments { get; set; }
        public string PurposeOfTravel { get; set; }
        public string FoodPreference { get; set; }
        public bool AttendedCct { get; set; }

        public string? TravelAgencyName { get; set; }
        public decimal? TotalExpense { get; set; }
        public string? UploadedTicketPdfPath { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Display fields from related entities
        public string EmployeeName { get; set; }
        public string TravelTypeName { get; set; }
        public string TripTypeName { get; set; }
        public string ProjectName { get; set; }
        public string TravelModeName { get; set; }
        public string CurrentStatusName { get; set; }
        public string SelectedTicketOptionName { get; set; }
    }
}
