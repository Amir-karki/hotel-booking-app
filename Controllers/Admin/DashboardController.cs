using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelBookingApp.Data;

namespace HotelBookingApp.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var totalHotels = await _context.Hotels.CountAsync();
            var totalRooms = await _context.Rooms.CountAsync();
            var totalBookings = await _context.Bookings.CountAsync();
            var pendingBookings = await _context.Bookings.CountAsync(b => b.Status == Models.BookingStatus.Pending);
            var confirmedBookings = await _context.Bookings.CountAsync(b => b.Status == Models.BookingStatus.Confirmed);

            ViewBag.TotalHotels = totalHotels;
            ViewBag.TotalRooms = totalRooms;
            ViewBag.TotalBookings = totalBookings;
            ViewBag.PendingBookings = pendingBookings;
            ViewBag.ConfirmedBookings = confirmedBookings;

            var recentBookings = await _context.Bookings
                .Include(b => b.Room)
                    .ThenInclude(r => r.Hotel)
                .Include(b => b.User)
                .OrderByDescending(b => b.BookedAt)
                .Take(10)
                .ToListAsync();

            return View(recentBookings);
        }
    }
}
