using System;

namespace XPRESS_V1_Backend.Models.DTO
{
    public class TravelRequestCreateDto
    {
        public int RequestId { get; set; }
        public int EmployeeId { get; set; }
        public int TravelTypeId { get; set; }
        public int TripTypeId { get; set; }
        public string ProjectCode { get; set; }
        public string SourcePlace { get; set; }
        public string SourceCountry { get; set; }
        public string DestinationPlace { get; set; }
        public string DestinationCountry { get; set; }

        private DateTime _departureDate;
        public DateTime DepartureDate
        {
            get => _departureDate;
            set => _departureDate = DateTime.SpecifyKind(value, DateTimeKind.Utc);
        }

        private DateTime _returnDate;
        public DateTime ReturnDate
        {
            get => _returnDate;
            set => _returnDate = DateTime.SpecifyKind(value, DateTimeKind.Utc);
        }

        public int TravelModeId { get; set; }
        public bool IsAccommodationRequired { get; set; }
        public bool IsPickupRequired { get; set; }
        public bool IsDropoffRequired { get; set; }
        public string PickupLocation { get; set; }
        public string DropoffLocation { get; set; }
        public string Comments { get; set; }
        public string PurposeOfTravel { get; set; }
        public string FoodPreference { get; set; }
        public bool AttendedCct { get; set; }
    }
}
