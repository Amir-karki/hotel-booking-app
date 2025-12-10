# Hotel Booking Application

A full-featured hotel booking application built with ASP.NET Core MVC, SQLite, and Entity Framework Core.

## Features

### User Features
- **Hotel Search & Discovery**
  - Search hotels by name, description, city, or country
  - Filter by city, country, star rating, and maximum price
  - Pagination support (9 hotels per page)
  - Hotel cards with images, ratings, and reviews

- **Hotel Details**
  - View detailed hotel information
  - Browse available rooms with image sliders (3-5 images per room)
  - See room amenities (WiFi, AC, TV, Minibar, Balcony)
  - View guest reviews and average ratings

- **Booking System**
  - Book rooms with date selection
  - Mock Stripe-style payment form with validation
  - Test card: 4242 4242 4242 4242
  - Booking confirmation page
  - View and manage bookings
  - Cancel bookings (if not started)

- **Review System**
  - Submit reviews for hotels with confirmed stays
  - Rate hotels from 1-5 stars
  - Write detailed reviews with title and comment
  - One review per hotel per user

- **User Account**
  - Register new accounts
  - Login/logout functionality
  - User profile with profile picture upload
  - First name and profile picture display in navigation
  - View booking history and statistics
  - Update personal information

### Admin Features
- **Dashboard**
  - Overview statistics (hotels, rooms, bookings)
  - Recent bookings list

- **Hotel Management**
  - Create, edit, and delete hotels
  - Activate/deactivate hotels
  - Manage hotel details (name, location, description, star rating)

- **Booking Management**
  - View all bookings
  - Filter by status (Pending, Confirmed, Cancelled, Completed)
  - Update booking status
  - Delete bookings

### Design Features
- **Light/Dark Theme**
  - Toggle between light and dark modes
  - Theme preference saved to localStorage
  - Smooth transitions

- **Consistent Design**
  - Pill-style buttons throughout
  - Improved contrast for accessibility
  - Responsive design for mobile and desktop
  - Clean, modern UI with cards and shadows

- **Form Validation**
  - Client-side and server-side validation
  - Clear error messages
  - Input formatting (card numbers, expiry dates)

- **Error Handling**
  - Custom error page with professional design
  - Request ID tracking for debugging
  - Detailed error information in development mode
  - User-friendly error messages in production

## Technology Stack

- **Backend**: ASP.NET Core 8.0 MVC
- **Database**: SQLite with Entity Framework Core
- **Authentication**: ASP.NET Core Identity
- **Frontend**: HTML5, CSS3, JavaScript (Vanilla)
- **Icons**: SVG icons (inline)

## Getting Started

### Prerequisites
- .NET 8.0 SDK
- Any modern web browser

### Installation

1. Clone the repository
2. Navigate to the project directory
3. Restore dependencies:
   ```bash
   dotnet restore
   ```

4. Apply database migrations:
   ```bash
   dotnet ef database update
   ```

5. Run the application:
   ```bash
   dotnet run
   ```

6. Open your browser and navigate to `https://localhost:5001` or `http://localhost:5000`

### Demo Accounts

**Admin Account:**
- Email: admin@hotelbooking.com
- Password: Admin@123

**User Account:**
- Email: user@hotelbooking.com
- Password: User@123

## Database Seeding

The application automatically seeds the database with:
- 10 hotels in various cities (New York, Miami, Aspen, Los Angeles, London, Honolulu, Chicago, Portland, Paris, Dubai)
- Multiple room types per hotel (Standard, Deluxe, Suite, Executive Suite, Presidential Suite)
- 3-5 images per room type
- Admin and demo user accounts

## Features Implementation

### Search & Filter
- Real-time search through hotel names, descriptions, and locations
- Multi-criteria filtering (city, country, star rating, price)
- Efficient pagination with page navigation

### Room Image Slider
- Smooth transitions between images
- Navigation buttons (previous/next)
- Dot indicators for current slide
- Touch-friendly controls

### Mock Payment Integration
- Stripe-style payment form design
- Card number formatting (4-digit groups)
- Expiry date validation (MM/YY format)
- CVV validation (3-4 digits)
- Card expiry date checking

### Review System
- Only users with completed stays can review
- One review per hotel per user
- 5-star rating system with visual stars
- Average rating calculation and display

### Admin Panel
- Separate admin area with dedicated layout
- Statistics dashboard
- Full CRUD operations for hotels
- Booking management and status updates

## Project Structure

```
HotelBookingApp/
├── Areas/
│   └── Admin/           # Admin area
│       ├── Controllers/
│       └── Views/
├── Controllers/         # MVC Controllers
│   ├── AccountController.cs
│   ├── BookingController.cs
│   ├── HomeController.cs
│   ├── ProfileController.cs
│   └── ReviewController.cs
├── Data/                # Database context and seeding
│   ├── ApplicationDbContext.cs
│   └── DbInitializer.cs
├── Models/              # Data models and ViewModels
│   ├── ApplicationUser.cs
│   ├── Hotel.cs
│   ├── Room.cs
│   ├── Booking.cs
│   ├── Review.cs
│   └── ViewModels/
├── Views/               # Razor views
│   ├── Account/
│   ├── Booking/
│   ├── Home/
│   ├── Profile/
│   ├── Review/
│   └── Shared/
├── wwwroot/             # Static files
│   ├── css/
│   ├── js/
│   └── uploads/         # User uploaded files
│       └── profiles/    # Profile pictures
├── Migrations/          # EF Core migrations
├── appsettings.json     # Configuration
└── Program.cs           # Application entry point
```

## Security Features

- Password requirements enforced (uppercase, lowercase, digit, special character)
- Anti-forgery tokens on all forms
- Authorization filters for admin and user areas
- Secure password hashing with Identity
- Input validation and sanitization

## Testing

### Test Error Page
Visit `/Home/TestError` to test the error handling system (shows detailed exception in development mode).

### Test Profile Picture Upload
1. Login with any account
2. Click on your name in the navigation
3. Select "My Profile"
4. Upload a profile picture
5. See the profile picture in the navigation bar

### Test Booking Flow
1. Browse hotels on the home page
2. Click "View Details" on any hotel
3. Select a room and click "Book Now"
4. Fill in check-in and check-out dates
5. Enter card details (use test card: 4242 4242 4242 4242)
6. Submit booking
7. View confirmation page

## Future Enhancements

- Real payment gateway integration (Stripe, PayPal)
- Email notifications for bookings
- Advanced search with date range availability
- Room availability calendar
- Multi-image upload for hotels and rooms
- Booking modification functionality
- Hotel owner role for self-management
- Advanced analytics for admin
- Two-factor authentication
- Social login (Google, Facebook)

## License

This project is created for demonstration purposes.
