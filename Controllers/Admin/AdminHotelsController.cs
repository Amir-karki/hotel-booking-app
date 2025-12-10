using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelBookingApp.Data;
using HotelBookingApp.Models;
using HotelBookingApp.Models.ViewModels;

namespace HotelBookingApp.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class AdminHotelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminHotelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var hotels = await _context.Hotels
                .Include(h => h.Rooms)
                .OrderBy(h => h.Name)
                .ToListAsync();

            return View(hotels);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HotelWithRoomsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Create hotel
            var hotel = new Hotel
            {
                Name = model.Name,
                City = model.City,
                Country = model.Country,
                Address = model.Address,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                StarRating = model.StarRating,
                IsActive = model.IsActive,
                CreatedAt = DateTime.UtcNow
            };

            _context.Hotels.Add(hotel);
            await _context.SaveChangesAsync();

            // Create room for the hotel
            var room = new Room
            {
                HotelId = hotel.Id,
                RoomType = model.RoomType,
                Description = model.RoomDescription,
                PricePerNight = model.PricePerNight,
                Capacity = model.Capacity,
                TotalRooms = model.TotalRooms,
                HasWifi = model.HasWifi,
                HasAirConditioning = model.HasAirConditioning,
                HasTelevision = model.HasTelevision,
                HasMinibar = model.HasMinibar,
                HasBalcony = model.HasBalcony,
                IsAvailable = true,
                CreatedAt = DateTime.UtcNow
            };

            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();

            // Add room images if provided
            var roomImages = new List<RoomImage>();

            if (!string.IsNullOrEmpty(model.RoomImage1))
            {
                roomImages.Add(new RoomImage
                {
                    RoomId = room.Id,
                    ImageUrl = model.RoomImage1,
                    DisplayOrder = 1
                });
            }

            if (!string.IsNullOrEmpty(model.RoomImage2))
            {
                roomImages.Add(new RoomImage
                {
                    RoomId = room.Id,
                    ImageUrl = model.RoomImage2,
                    DisplayOrder = 2
                });
            }

            if (!string.IsNullOrEmpty(model.RoomImage3))
            {
                roomImages.Add(new RoomImage
                {
                    RoomId = room.Id,
                    ImageUrl = model.RoomImage3,
                    DisplayOrder = 3
                });
            }

            if (roomImages.Any())
            {
                _context.RoomImages.AddRange(roomImages);
                await _context.SaveChangesAsync();
            }

            TempData["SuccessMessage"] = $"Hotel '{hotel.Name}' created successfully with {model.RoomType} room type.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id);

            if (hotel == null)
            {
                return NotFound();
            }

            return View(hotel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Hotel hotel)
        {
            if (id != hotel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(hotel);
            }

            try
            {
                _context.Update(hotel);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Hotel updated successfully.";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HotelExists(hotel.Id))
                {
                    return NotFound();
                }
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id);

            if (hotel == null)
            {
                return NotFound();
            }

            _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Hotel deleted successfully.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleActive(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id);

            if (hotel == null)
            {
                return NotFound();
            }

            hotel.IsActive = !hotel.IsActive;
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Hotel {(hotel.IsActive ? "activated" : "deactivated")} successfully.";
            return RedirectToAction(nameof(Index));
        }

        private bool HotelExists(int id)
        {
            return _context.Hotels.Any(e => e.Id == id);
        }
    }
}
