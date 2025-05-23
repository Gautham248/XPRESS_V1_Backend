using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XPRESS_V1_Backend.Models
{
    public class User
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string Role { get; set; }

        public string Department { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public int? ReportingManagerId { get; set; }

        public int? DuHeadId { get; set; }

        [ForeignKey("ReportingManagerId")]
        public User ReportingManager { get; set; }

        [ForeignKey("DuHeadId")]
        public User DuHead { get; set; }

        // Navigation properties
        public ICollection<User> Subordinates { get; set; } // Users reporting to this user
        public ICollection<User> DepartmentMembers { get; set; } // Users under this DU Head
        public ICollection<TravelRequest> TravelRequests { get; set; }
        public ICollection<TicketOption> CreatedTicketOptions { get; set; }
        public ICollection<RequestApproval> Approvals { get; set; }
        public ICollection<AuditLog> AuditLogs { get; set; }
    }

}
