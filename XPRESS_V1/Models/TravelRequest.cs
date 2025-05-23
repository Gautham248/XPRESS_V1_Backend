using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XPRESS_V1_Backend.Models
{
    public class TravelRequest
    {
        [Key]
        public int RequestId { get; set; }
        public int EmployeeId { get; set; }
        public int TravelTypeId { get; set; }
        public int TripTypeId { get; set; }
        public string ProjectCode { get; set; } // Reverted to ProjectCode (string)
        public string SourcePlace { get; set; }
        public string SourceCountry { get; set; }
        public string DestinationPlace { get; set; }
        public string DestinationCountry { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ReturnDate { get; set; }
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
        public int CurrentStatusId { get; set; }
        public int? SelectedTicketOptionId { get; set; }
        public string TravelAgencyName { get; set; }
        public decimal? TotalExpense { get; set; }
        public string UploadedTicketPdfPath { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation properties
        public User Employee { get; set; }
        public TravelType TravelType { get; set; }
        public TripType TripType { get; set; }
        public Project Project { get; set; }
        public TravelMode TravelMode { get; set; }
        public RequestStatus CurrentStatus { get; set; }
        public TicketOption SelectedTicketOption { get; set; }
        public ICollection<TicketOption> TicketOptions { get; set; }
        public ICollection<RequestApproval> RequestApprovals { get; set; }
        public ICollection<AuditLog> AuditLogs { get; set; }
    }
}
