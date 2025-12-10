namespace HotelBookingApp.Models.ViewModels
{
    public class HotelSearchViewModel
    {
        public string? SearchTerm { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public int? MinStarRating { get; set; }
        public decimal? MaxPrice { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 9;
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }

        public List<Hotel> Hotels { get; set; } = new List<Hotel>();
        public List<string> Cities { get; set; } = new List<string>();
        public List<string> Countries { get; set; } = new List<string>();
    }
}
