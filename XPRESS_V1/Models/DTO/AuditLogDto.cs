namespace XPRESS_V1_Backend.Models.DTO
{
    public class AuditLogDto
    {
        public int Id { get; set; }
        public string Action { get; set; }
        private DateTime _returnDate;
        public DateTime ReturnDate
        {
            get => _returnDate;
            set => _returnDate = DateTime.SpecifyKind(value, DateTimeKind.Utc);
        }
    }

}
