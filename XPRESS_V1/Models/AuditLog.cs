using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XPRESS_V1_Backend.Models
{
    public class AuditLog
    {
        [Key]
        public int LogId { get; set; }

        [Required]
        public int RequestId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public string ActionType { get; set; }

        public int? OldStatusId { get; set; }

        public int? NewStatusId { get; set; }

        public string ChangeDescription { get; set; }

        public string Comments { get; set; }

        public string IpAddress { get; set; }

        private DateTime _timestamp;

        [Required]
        public DateTime Timestamp
        {
            get => _timestamp;
            set => _timestamp = DateTime.SpecifyKind(value, DateTimeKind.Utc);
        }

        [ForeignKey("RequestId")]
        public TravelRequest TravelRequest { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("OldStatusId")]
        public RequestStatus OldStatus { get; set; }

        [ForeignKey("NewStatusId")]
        public RequestStatus NewStatus { get; set; }
    }
}
