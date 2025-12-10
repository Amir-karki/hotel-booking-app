using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelBookingApp.Data;
using HotelBookingApp.Models;
using HotelBookingApp.Models.ViewModels;

namespace HotelBookingApp.Controllers
{
    [Authorize]
    public class ReviewController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ReviewController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Create(int hotelId)
        {
            var hotel = await _context.Hotels.FindAsync(hotelId);

            if (hotel == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);

            // Check if user has a completed stay at this hotel
            var hasCompletedStay = await _context.Bookings
                .Include(b => b.Room)
                .AnyAsync(b => b.UserId == user!.Id &&
                              b.Room.HotelId == hotelId &&
                              b.Status == BookingStatus.Confirmed &&
                              b.CheckOutDate < DateTime.UtcNow);

            if (!hasCompletedStay)
            {
                TempData["ErrorMessage"] = "You can only review hotels where you have completed a stay.";
                return RedirectToAction("HotelDetails", "Home", new { id = hotelId });
            }

            // Check if user has already reviewed this hotel
            var existingReview = await _context.Reviews
                .AnyAsync(r => r.HotelId == hotelId && r.UserId == user!.Id);

            if (existingReview)
            {
                TempData["ErrorMessage"] = "You have already reviewed this hotel.";
                return RedirectToAction("HotelDetails", "Home", new { id = hotelId });
            }

            var model = new ReviewViewModel
            {
                HotelId = hotelId,
                Hotel = hotel
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReviewViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Hotel = await _context.Hotels.FindAsync(model.HotelId);
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);

            // Verify user has completed stay (double-check)
            var hasCompletedStay = await _context.Bookings
                .Include(b => b.Room)
                .AnyAsync(b => b.UserId == user!.Id &&
                              b.Room.HotelId == model.HotelId &&
                              b.Status == BookingStatus.Confirmed &&
                              b.CheckOutDate < DateTime.UtcNow);

            if (!hasCompletedStay)
            {
                TempData["ErrorMessage"] = "You can only review hotels where you have completed a stay.";
                return RedirectToAction("HotelDetails", "Home", new { id = model.HotelId });
            }

            // Check for existing review
            var existingReview = await _context.Reviews
                .AnyAsync(r => r.HotelId == model.HotelId && r.UserId == user!.Id);

            if (existingReview)
            {
                TempData["ErrorMessage"] = "You have already reviewed this hotel.";
                return RedirectToAction("HotelDetails", "Home", new { id = model.HotelId });
            }

            var review = new Review
            {
                HotelId = model.HotelId,
                UserId = user!.Id,
                Rating = model.Rating,
                Title = model.Title,
                Comment = model.Comment
            };

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Thank you for your review!";
            return RedirectToAction("HotelDetails", "Home", new { id = model.HotelId });
        }
    }
}
