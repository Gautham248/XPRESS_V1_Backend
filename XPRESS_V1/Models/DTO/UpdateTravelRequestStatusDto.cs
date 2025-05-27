namespace XPRESS_V1_Backend.Models.DTO
{
    public class UpdateTravelRequestStatusDto
    {
        public int CurrentStatusId { get; set; }
        public string? Comments { get; set; }
        public int UserId { get; set; } // The user making the change (e.g., an admin or the employee)
    }
}
