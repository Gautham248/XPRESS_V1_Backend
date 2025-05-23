using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XPRESS_V1_Backend.Models
{
    public class TravelRequest
    {
        [Key]
        public int RequestId { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public int TravelTypeId { get; set; }

        [Required]
        public int TripTypeId { get; set; }

        [Required]
        public string ProjectCode { get; set; }

        public string SourcePlace { get; set; }

        public string SourceCountry { get; set; }

        public string DestinationPlace { get; set; }

        public string DestinationCountry { get; set; }

        public DateTime? DepartureDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        [Required]
        public int TravelModeId { get; set; }

        [Required]
        public bool IsAccommodationRequired { get; set; }

        public bool? IsPickupRequired { get; set; }

        public bool? IsDropoffRequired { get; set; }

        public string PickupLocation { get; set; }

        public string DropoffLocation { get; set; }

        public string Comments { get; set; }

        public string PurposeOfTravel { get; set; }

        public string FoodPreference { get; set; }

        public bool? AttendedCct { get; set; }

        [Required]
        public int CurrentStatusId { get; set; }

        public int? SelectedTicketOptionId { get; set; }

        public string TravelAgencyName { get; set; }

        public decimal? TotalExpense { get; set; }

        public string UploadedTicketPdfPath { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        [ForeignKey("EmployeeId")]
        public User Employee { get; set; }

        [ForeignKey("TravelTypeId")]
        public TravelType TravelType { get; set; }

        [ForeignKey("TripTypeId")]
        public TripType TripType { get; set; }

        [ForeignKey("ProjectCode")]
        public Project Project { get; set; }

        [ForeignKey("TravelModeId")]
        public TravelMode TravelMode { get; set; }

        [ForeignKey("CurrentStatusId")]
        public RequestStatus CurrentStatus { get; set; }

        [ForeignKey("SelectedTicketOptionId")]
        public TicketOption SelectedTicketOption { get; set; }

        public ICollection<TicketOption> TicketOptions { get; set; }
        public ICollection<RequestApproval> RequestApprovals { get; set; }
        public ICollection<AuditLog> AuditLogs { get; set; }
    }
}
