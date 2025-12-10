using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelBookingApp.Data;
using HotelBookingApp.Models;
using HotelBookingApp.Models.ViewModels;
using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;

namespace HotelBookingApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(HotelSearchViewModel searchModel)
        {
            var query = _context.Hotels
                .Include(h => h.Rooms)
                .Include(h => h.Reviews)
                .Where(h => h.IsActive);

            // Apply search filters
            if (!string.IsNullOrEmpty(searchModel.SearchTerm))
            {
                query = query.Where(h => h.Name.Contains(searchModel.SearchTerm) ||
                                        h.Description.Contains(searchModel.SearchTerm) ||
                                        h.City.Contains(searchModel.SearchTerm) ||
                                        h.Country.Contains(searchModel.SearchTerm));
            }

            if (!string.IsNullOrEmpty(searchModel.City))
            {
                query = query.Where(h => h.City == searchModel.City);
            }

            if (!string.IsNullOrEmpty(searchModel.Country))
            {
                query = query.Where(h => h.Country == searchModel.Country);
            }

            if (searchModel.MinStarRating.HasValue)
            {
                query = query.Where(h => h.StarRating >= searchModel.MinStarRating.Value);
            }

            if (searchModel.MaxPrice.HasValue)
            {
                query = query.Where(h => h.Rooms.Any(r => r.PricePerNight <= searchModel.MaxPrice.Value));
            }

            // Get total count
            var totalItems = await query.CountAsync();

            // Apply pagination
            var hotels = await query
                .OrderBy(h => h.Name)
                .Skip((searchModel.PageNumber - 1) * searchModel.PageSize)
                .Take(searchModel.PageSize)
                .ToListAsync();

            // Get filter options
            var cities = await _context.Hotels
                .Where(h => h.IsActive)
                .Select(h => h.City)
                .Distinct()
                .OrderBy(c => c)
                .ToListAsync();

            var countries = await _context.Hotels
                .Where(h => h.IsActive)
                .Select(h => h.Country)
                .Distinct()
                .OrderBy(c => c)
                .ToListAsync();

            searchModel.Hotels = hotels;
            searchModel.Cities = cities;
            searchModel.Countries = countries;
            searchModel.TotalItems = totalItems;
            searchModel.TotalPages = (int)Math.Ceiling(totalItems / (double)searchModel.PageSize);

            return View(searchModel);
        }

        public async Task<IActionResult> HotelDetails(int id)
        {
            var hotel = await _context.Hotels
                .Include(h => h.Rooms)
                    .ThenInclude(r => r.RoomImages)
                .Include(h => h.Reviews)
                    .ThenInclude(r => r.User)
                .FirstOrDefaultAsync(h => h.Id == id && h.IsActive);

            if (hotel == null)
            {
                return NotFound();
            }

            return View(hotel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // TESTING ONLY - Remove this before deploying to production
        public IActionResult TestError()
        {
            throw new Exception("This is a test exception to verify the error page is working correctly.");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var errorViewModel = new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            // In development, show detailed error information
            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            if (exceptionHandlerPathFeature?.Error != null)
            {
                var exception = exceptionHandlerPathFeature.Error;

                // Only show exception details in development environment
                if (HttpContext.RequestServices.GetRequiredService<IWebHostEnvironment>().IsDevelopment())
                {
                    errorViewModel.ExceptionMessage = exception.Message;
                    errorViewModel.ExceptionStackTrace = exception.StackTrace;
                }
            }

            return View(errorViewModel);
        }
    }
}
