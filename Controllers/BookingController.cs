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
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public BookingController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Create(int roomId, DateTime? checkIn, DateTime? checkOut)
        {
            var room = await _context.Rooms
                .Include(r => r.Hotel)
                .Include(r => r.RoomImages)
                .FirstOrDefaultAsync(r => r.Id == roomId);

            if (room == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);

            var model = new BookingViewModel
            {
                RoomId = roomId,
                Room = room,
                CheckInDate = checkIn ?? DateTime.Today.AddDays(1),
                CheckOutDate = checkOut ?? DateTime.Today.AddDays(2),
                NumberOfGuests = 1,
                GuestFullName = $"{user?.FirstName} {user?.LastName}".Trim(),
                GuestEmail = user?.Email ?? string.Empty,
                GuestPhone = user?.PhoneNumber ?? string.Empty
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookingViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Reload room data
                model.Room = await _context.Rooms
                    .Include(r => r.Hotel)
                    .Include(r => r.RoomImages)
                    .FirstOrDefaultAsync(r => r.Id == model.RoomId);
                return View(model);
            }

            // Validate dates
            if (model.CheckInDate < DateTime.Today)
            {
                ModelState.AddModelError("CheckInDate", "Check-in date cannot be in the past.");
                model.Room = await _context.Rooms
                    .Include(r => r.Hotel)
                    .Include(r => r.RoomImages)
                    .FirstOrDefaultAsync(r => r.Id == model.RoomId);
                return View(model);
            }

            if (model.CheckOutDate <= model.CheckInDate)
            {
                ModelState.AddModelError("CheckOutDate", "Check-out date must be after check-in date.");
                model.Room = await _context.Rooms
                    .Include(r => r.Hotel)
                    .Include(r => r.RoomImages)
                    .FirstOrDefaultAsync(r => r.Id == model.RoomId);
                return View(model);
            }

            var room = await _context.Rooms.FindAsync(model.RoomId);
            if (room == null)
            {
                return NotFound();
            }

            // Check if room is available for the selected dates
            var conflictingBookings = await _context.Bookings
                .Where(b => b.RoomId == model.RoomId &&
                           b.Status != BookingStatus.Cancelled &&
                           ((b.CheckInDate <= model.CheckInDate && b.CheckOutDate > model.CheckInDate) ||
                            (b.CheckInDate < model.CheckOutDate && b.CheckOutDate >= model.CheckOutDate) ||
                            (b.CheckInDate >= model.CheckInDate && b.CheckOutDate <= model.CheckOutDate)))
                .CountAsync();

            if (conflictingBookings >= room.TotalRooms)
            {
                ModelState.AddModelError(string.Empty, "Room is not available for the selected dates. Please choose different dates.");
                model.Room = await _context.Rooms
                    .Include(r => r.Hotel)
                    .Include(r => r.RoomImages)
                    .FirstOrDefaultAsync(r => r.Id == model.RoomId);
                return View(model);
            }

            // Validate card expiry
            if (!string.IsNullOrEmpty(model.ExpiryDate))
            {
                var parts = model.ExpiryDate.Split('/');
                if (parts.Length == 2)
                {
                    if (int.TryParse(parts[0], out int month) && int.TryParse(parts[1], out int year))
                    {
                        var expiryDate = new DateTime(2000 + year, month, 1).AddMonths(1).AddDays(-1);
                        if (expiryDate < DateTime.Today)
                        {
                            ModelState.AddModelError("ExpiryDate", "Card has expired.");
                            model.Room = await _context.Rooms
                                .Include(r => r.Hotel)
                                .Include(r => r.RoomImages)
                                .FirstOrDefaultAsync(r => r.Id == model.RoomId);
                            return View(model);
                        }
                    }
                }
            }

            // Calculate total price
            var nights = (model.CheckOutDate - model.CheckInDate).Days;
            var totalPrice = room.PricePerNight * nights;

            var user = await _userManager.GetUserAsync(User);

            // Create booking (mock payment success)
            var booking = new Booking
            {
                RoomId = model.RoomId,
                UserId = user!.Id,
                CheckInDate = model.CheckInDate,
                CheckOutDate = model.CheckOutDate,
                NumberOfGuests = model.NumberOfGuests,
                TotalPrice = totalPrice,
                GuestFullName = model.GuestFullName,
                GuestEmail = model.GuestEmail,
                GuestPhone = model.GuestPhone,
                SpecialRequests = model.SpecialRequests,
                Status = BookingStatus.Confirmed,
                PaymentIntentId = $"pi_mock_{Guid.NewGuid().ToString("N")[..24]}"
            };

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Booking confirmed successfully! Your payment has been processed.";
            return RedirectToAction("Confirmation", new { id = booking.Id });
        }

        [HttpGet]
        public async Task<IActionResult> Confirmation(int id)
        {
            var booking = await _context.Bookings
                .Include(b => b.Room)
                    .ThenInclude(r => r.Hotel)
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (booking == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            if (booking.UserId != user?.Id && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            return View(booking);
        }

        [HttpGet]
        public async Task<IActionResult> MyBookings()
        {
            var user = await _userManager.GetUserAsync(User);
            var bookings = await _context.Bookings
                .Include(b => b.Room)
                    .ThenInclude(r => r.Hotel)
                .Where(b => b.UserId == user!.Id)
                .OrderByDescending(b => b.BookedAt)
                .ToListAsync();

            return View(bookings);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancel(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);

            if (booking == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            if (booking.UserId != user?.Id && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            if (booking.Status == BookingStatus.Cancelled)
            {
                TempData["ErrorMessage"] = "This booking is already cancelled.";
                return RedirectToAction("MyBookings");
            }

            if (booking.CheckInDate <= DateTime.Today)
            {
                TempData["ErrorMessage"] = "Cannot cancel bookings that have already started or are in the past.";
                return RedirectToAction("MyBookings");
            }

            booking.Status = BookingStatus.Cancelled;
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Booking cancelled successfully.";
            return RedirectToAction("MyBookings");
        }
    }
}
