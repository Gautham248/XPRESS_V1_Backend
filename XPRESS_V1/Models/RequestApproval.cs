using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XPRESS_V1_Backend.Models
{
    public class RequestApproval
    {
        [Key]
        public int ApprovalId { get; set; }

        [Required]
        public int RequestId { get; set; }

        [Required]
        public int ApproverId { get; set; }

        [Required]
        public string ApprovalLevel { get; set; }

        [Required]
        public string ActionTaken { get; set; }

        public string Comments { get; set; }

        public int? PreviousStatusId { get; set; }

        [Required]
        public int NewStatusId { get; set; }

        [Required]
        public DateTime ActionDate { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [ForeignKey("RequestId")]
        public TravelRequest TravelRequest { get; set; }

        [ForeignKey("ApproverId")]
        public User Approver { get; set; }

        [ForeignKey("PreviousStatusId")]
        public RequestStatus PreviousStatus { get; set; }

        [ForeignKey("NewStatusId")]
        public RequestStatus NewStatus { get; set; }
    }

}
