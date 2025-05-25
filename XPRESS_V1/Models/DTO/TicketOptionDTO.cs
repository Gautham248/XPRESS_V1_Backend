using System.ComponentModel.DataAnnotations;

namespace XPRESS_V1_Backend.Models.DTO
{
    public class TicketOptionDTO
    {
        public int OptionId { get; set; }
        public int RequestId { get; set; }
        public string OptionDescription { get; set; }
        public bool IsSelected { get; set; } = false;
    }
}
