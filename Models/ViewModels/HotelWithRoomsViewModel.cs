using System.ComponentModel.DataAnnotations;

namespace HotelBookingApp.Models.ViewModels
{
    public class HotelWithRoomsViewModel
    {
        // Hotel Information
        [Required(ErrorMessage = "Hotel name is required")]
        [StringLength(200, ErrorMessage = "Name cannot exceed 200 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "City is required")]
        [StringLength(100, ErrorMessage = "City cannot exceed 100 characters")]
        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = "Country is required")]
        [StringLength(100, ErrorMessage = "Country cannot exceed 100 characters")]
        public string Country { get; set; } = string.Empty;

        [Required(ErrorMessage = "Address is required")]
        [StringLength(500, ErrorMessage = "Address cannot exceed 500 characters")]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required")]
        [StringLength(2000, ErrorMessage = "Description cannot exceed 2000 characters")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Image URL is required")]
        [Url(ErrorMessage = "Please enter a valid URL")]
        public string ImageUrl { get; set; } = string.Empty;

        [Range(1, 5, ErrorMessage = "Star rating must be between 1 and 5")]
        public int StarRating { get; set; } = 3;

        public bool IsActive { get; set; } = true;

        // Room Information
        [Required(ErrorMessage = "Room type is required")]
        [StringLength(100, ErrorMessage = "Room type cannot exceed 100 characters")]
        public string RoomType { get; set; } = string.Empty;

        [Required(ErrorMessage = "Room description is required")]
        [StringLength(1000, ErrorMessage = "Room description cannot exceed 1000 characters")]
        public string RoomDescription { get; set; } = string.Empty;

        [Required(ErrorMessage = "Price per night is required")]
        [Range(0.01, 100000, ErrorMessage = "Price must be between $0.01 and $100,000")]
        public decimal PricePerNight { get; set; }

        [Required(ErrorMessage = "Capacity is required")]
        [Range(1, 10, ErrorMessage = "Capacity must be between 1 and 10")]
        public int Capacity { get; set; } = 2;

        [Required(ErrorMessage = "Total rooms is required")]
        [Range(1, 100, ErrorMessage = "Total rooms must be between 1 and 100")]
        public int TotalRooms { get; set; } = 1;

        // Room Amenities
        public bool HasWifi { get; set; } = true;
        public bool HasAirConditioning { get; set; } = true;
        public bool HasTelevision { get; set; } = true;
        public bool HasMinibar { get; set; } = false;
        public bool HasBalcony { get; set; } = false;

        // Room Images (URLs)
        [Url(ErrorMessage = "Please enter a valid URL")]
        public string? RoomImage1 { get; set; }

        [Url(ErrorMessage = "Please enter a valid URL")]
        public string? RoomImage2 { get; set; }

        [Url(ErrorMessage = "Please enter a valid URL")]
        public string? RoomImage3 { get; set; }
    }
}
