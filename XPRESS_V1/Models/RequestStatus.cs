using System.ComponentModel.DataAnnotations;

namespace XPRESS_V1_Backend.Models
{
    public class RequestStatus
    {
        [Key]
        public int StatusId { get; set; }

        [Required]
        public string StatusName { get; set; }

        public string StatusDescription { get; set; }

        [Required]
        public int SequenceOrder { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public ICollection<TravelRequest> TravelRequests { get; set; }
        public ICollection<RequestApproval> PreviousStatuses { get; set; }
        public ICollection<RequestApproval> RequestApprovalsAsNewStatus { get; set; } // Renamed to avoid conflict
        public ICollection<AuditLog> OldStatuses { get; set; }
        public ICollection<AuditLog> AuditLogsAsNewStatus { get; set; } // Renamed to avoid conflict
    }

}
