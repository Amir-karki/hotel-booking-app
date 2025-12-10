using Microsoft.AspNetCore.Identity;
using HotelBookingApp.Models;

namespace HotelBookingApp.Data
{
    public static class DbInitializer
    {
        public static async Task SeedAsync(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Ensure database is created
            await context.Database.EnsureCreatedAsync();

            // Seed roles
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            if (!await roleManager.RoleExistsAsync("User"))
            {
                await roleManager.CreateAsync(new IdentityRole("User"));
            }

            // Seed admin user
            if (await userManager.FindByEmailAsync("admin@demo.com") == null)
            {
                var adminUser = new ApplicationUser
                {
                    UserName = "admin@demo.com",
                    Email = "admin@demo.com",
                    FirstName = "Admin",
                    LastName = "User",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, "Admin123$");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            // Seed regular user
            if (await userManager.FindByEmailAsync("user@demo.com") == null)
            {
                var regularUser = new ApplicationUser
                {
                    UserName = "user@demo.com",
                    Email = "user@demo.com",
                    FirstName = "John",
                    LastName = "Doe",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(regularUser, "User123$");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(regularUser, "User");
                }
            }

            // Check if hotels already exist
            if (context.Hotels.Any())
            {
                return; // Database has been seeded
            }

            // Seed hotels
            var hotels = new List<Hotel>
            {
                new Hotel
                {
                    Name = "Grand Plaza Hotel",
                    Description = "Luxury 5-star hotel in the heart of the city with stunning views and world-class amenities. Experience unparalleled comfort and sophistication in our elegantly appointed rooms and suites.",
                    Address = "123 Main Street",
                    City = "New York",
                    Country = "USA",
                    StarRating = 5,
                    ImageUrl = "https://images.unsplash.com/photo-1566073771259-6a8506099945?w=800",
                    IsActive = true
                },
                new Hotel
                {
                    Name = "Seaside Resort & Spa",
                    Description = "Beachfront resort offering relaxation and recreation with private beach access, multiple pools, and a full-service spa. Perfect for a tranquil getaway.",
                    Address = "456 Ocean Drive",
                    City = "Miami",
                    Country = "USA",
                    StarRating = 4,
                    ImageUrl = "https://images.unsplash.com/photo-1571896349842-33c89424de2d?w=800",
                    IsActive = true
                },
                new Hotel
                {
                    Name = "Mountain View Lodge",
                    Description = "Cozy mountain retreat with breathtaking alpine views, perfect for skiing and hiking enthusiasts. Features rustic charm with modern amenities.",
                    Address = "789 Mountain Road",
                    City = "Aspen",
                    Country = "USA",
                    StarRating = 4,
                    ImageUrl = "https://images.unsplash.com/photo-1542314831-068cd1dbfeeb?w=800",
                    IsActive = true
                },
                new Hotel
                {
                    Name = "City Center Inn",
                    Description = "Affordable accommodation in the city center with easy access to shopping and dining. Clean, comfortable rooms and friendly service.",
                    Address = "321 Downtown Ave",
                    City = "Los Angeles",
                    Country = "USA",
                    StarRating = 3,
                    ImageUrl = "https://images.unsplash.com/photo-1564501049412-61c2a3083791?w=800",
                    IsActive = true
                },
                new Hotel
                {
                    Name = "Royal Heritage Hotel",
                    Description = "Historic hotel blending classic elegance with modern comfort. Located in a beautifully restored Victorian building with ornate architecture.",
                    Address = "555 Heritage Lane",
                    City = "London",
                    Country = "UK",
                    StarRating = 5,
                    ImageUrl = "https://images.unsplash.com/photo-1551882547-ff40c63fe5fa?w=800",
                    IsActive = true
                },
                new Hotel
                {
                    Name = "Sunset Paradise Hotel",
                    Description = "Tropical paradise with stunning sunset views, infinity pool, and direct beach access. Experience island luxury at its finest.",
                    Address = "777 Beach Boulevard",
                    City = "Honolulu",
                    Country = "USA",
                    StarRating = 5,
                    ImageUrl = "https://images.unsplash.com/photo-1520250497591-112f2f40a3f4?w=800",
                    IsActive = true
                },
                new Hotel
                {
                    Name = "Downtown Business Hotel",
                    Description = "Modern business hotel with conference facilities and high-speed internet. Ideal for corporate travelers and business meetings.",
                    Address = "888 Business Park",
                    City = "Chicago",
                    Country = "USA",
                    StarRating = 4,
                    ImageUrl = "https://images.unsplash.com/photo-1568084680786-a84f91d1153c?w=800",
                    IsActive = true
                },
                new Hotel
                {
                    Name = "Garden Oasis Hotel",
                    Description = "Peaceful hotel surrounded by beautiful gardens and nature. Features eco-friendly practices and organic dining options.",
                    Address = "999 Garden Path",
                    City = "Portland",
                    Country = "USA",
                    StarRating = 3,
                    ImageUrl = "https://images.unsplash.com/photo-1445019980597-93fa8acb246c?w=800",
                    IsActive = true
                },
                new Hotel
                {
                    Name = "Riverside Boutique Hotel",
                    Description = "Charming boutique hotel along the riverfront with personalized service and unique decor. Each room tells its own story.",
                    Address = "111 Riverside Drive",
                    City = "Paris",
                    Country = "France",
                    StarRating = 4,
                    ImageUrl = "https://images.unsplash.com/photo-1549294413-26f195200c16?w=800",
                    IsActive = true
                },
                new Hotel
                {
                    Name = "Skyline Tower Hotel",
                    Description = "Contemporary high-rise hotel with panoramic city views, rooftop bar, and state-of-the-art fitness center. Modern luxury in the sky.",
                    Address = "222 Skyline Avenue",
                    City = "Dubai",
                    Country = "UAE",
                    StarRating = 5,
                    ImageUrl = "https://images.unsplash.com/photo-1582719508461-905c673771fd?w=800",
                    IsActive = true
                }
            };

            context.Hotels.AddRange(hotels);
            await context.SaveChangesAsync();

            // Seed rooms and room images for each hotel
            var random = new Random();
            var roomTypes = new[] { "Standard Room", "Deluxe Room", "Suite", "Executive Suite", "Presidential Suite" };
            var roomDescriptions = new Dictionary<string, string>
            {
                { "Standard Room", "Comfortable room with essential amenities for a pleasant stay." },
                { "Deluxe Room", "Spacious room with premium furnishings and enhanced amenities." },
                { "Suite", "Large suite with separate living area and luxurious accommodations." },
                { "Executive Suite", "Premium suite designed for business travelers with work space." },
                { "Presidential Suite", "The ultimate in luxury with panoramic views and exclusive services." }
            };

            var sampleRoomImages = new[]
            {
                "https://images.unsplash.com/photo-1611892440504-42a792e24d32?w=600",
                "https://images.unsplash.com/photo-1618773928121-c32242e63f39?w=600",
                "https://images.unsplash.com/photo-1590490360182-c33d57733427?w=600",
                "https://images.unsplash.com/photo-1582719478250-c89cae4dc85b?w=600",
                "https://images.unsplash.com/photo-1560185893-a55cbc8c57e8?w=600",
                "https://images.unsplash.com/photo-1591088398332-8a7791972843?w=600"
            };

            foreach (var hotel in hotels)
            {
                // Add 3-5 room types per hotel
                var numRoomTypes = random.Next(3, 6);
                var selectedRoomTypes = roomTypes.OrderBy(x => random.Next()).Take(numRoomTypes).ToList();

                foreach (var roomType in selectedRoomTypes)
                {
                    var basePrice = roomType switch
                    {
                        "Standard Room" => 100,
                        "Deluxe Room" => 150,
                        "Suite" => 250,
                        "Executive Suite" => 350,
                        "Presidential Suite" => 500,
                        _ => 100
                    };

                    var priceMultiplier = hotel.StarRating * 0.2m + 0.6m;
                    var price = basePrice * priceMultiplier;

                    var room = new Room
                    {
                        HotelId = hotel.Id,
                        RoomType = roomType,
                        Description = roomDescriptions[roomType],
                        PricePerNight = Math.Round(price, 2),
                        Capacity = roomType == "Presidential Suite" ? 4 : roomType == "Suite" || roomType == "Executive Suite" ? 3 : 2,
                        TotalRooms = random.Next(5, 21),
                        IsAvailable = true,
                        HasWifi = true,
                        HasAirConditioning = hotel.StarRating >= 3,
                        HasTelevision = true,
                        HasMinibar = hotel.StarRating >= 4,
                        HasBalcony = random.Next(0, 2) == 1
                    };

                    context.Rooms.Add(room);
                    await context.SaveChangesAsync();

                    // Add 3-5 images per room
                    var numImages = random.Next(3, 6);
                    for (int i = 0; i < numImages; i++)
                    {
                        var roomImage = new RoomImage
                        {
                            RoomId = room.Id,
                            ImageUrl = sampleRoomImages[random.Next(sampleRoomImages.Length)],
                            DisplayOrder = i + 1
                        };
                        context.RoomImages.Add(roomImage);
                    }
                }
            }

            await context.SaveChangesAsync();
        }
    }
}
