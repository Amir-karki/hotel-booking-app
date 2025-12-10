using System.ComponentModel.DataAnnotations;

namespace HotelBookingApp.Models.ViewModels
{
    public class ReviewViewModel
    {
        public int HotelId { get; set; }
        public Hotel? Hotel { get; set; }

        [Required(ErrorMessage = "Rating is required")]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
        [Display(Name = "Rating")]
        public int Rating { get; set; }

        [Required(ErrorMessage = "Review title is required")]
        [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters")]
        [Display(Name = "Title")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Review comment is required")]
        [StringLength(2000, ErrorMessage = "Comment cannot exceed 2000 characters")]
        [Display(Name = "Your Review")]
        public string Comment { get; set; } = string.Empty;
    }
}
