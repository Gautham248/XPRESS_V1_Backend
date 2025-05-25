namespace XPRESS_V1_Backend.Models.DTO
{
    public class TravelInfoDetailsDTO
    {
        public int RequestId { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string Transportation { get; set; }
        public string TravelTypeName { get; set; }
        public DateTime RequestCreateDate { get; set; }
        public string PurposeOfTravel { get; set; }
        public bool IsAccommodationRequired { get; set; }
        public string FoodPreference { get; set; }
        public string PickupLocation { get; set; }
        public string DropoffLocation { get; set; }
    }
}
