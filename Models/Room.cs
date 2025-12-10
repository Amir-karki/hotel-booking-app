using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBookingApp.Models
{
    public class Room
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Hotel is required")]
        public int HotelId { get; set; }

        [Required(ErrorMessage = "Room type is required")]
        [StringLength(100, ErrorMessage = "Room type cannot exceed 100 characters")]
        public string RoomType { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required")]
        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 100000, ErrorMessage = "Price must be between 0.01 and 100000")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PricePerNight { get; set; }

        [Range(1, 10, ErrorMessage = "Capacity must be between 1 and 10")]
        public int Capacity { get; set; }

        [Range(1, 100, ErrorMessage = "Total rooms must be between 1 and 100")]
        public int TotalRooms { get; set; }

        public bool IsAvailable { get; set; } = true;

        // Amenities
        public bool HasWifi { get; set; }
        public bool HasAirConditioning { get; set; }
        public bool HasTelevision { get; set; }
        public bool HasMinibar { get; set; }
        public bool HasBalcony { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        [ForeignKey("HotelId")]
        public virtual Hotel Hotel { get; set; } = null!;
        public virtual ICollection<RoomImage> RoomImages { get; set; } = new List<RoomImage>();
        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
