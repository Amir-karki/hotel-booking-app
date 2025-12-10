# Hotel Booking Application - Project Structure

## 📁 Project Organization

```
HotelBookingApp/
│
├── 📄 Program.cs                          # Application entry point & configuration
├── 📄 HotelBookingApp.csproj              # Project file with NuGet packages
├── 📄 appsettings.json                    # Configuration (SQLite connection string)
│
├── 📂 Controllers/                        # MVC Controllers (Business Logic)
│   ├── AccountController.cs               # Login, Register, Logout
│   ├── HomeController.cs                  # Search, Hotel Details, Landing Page
│   ├── BookingController.cs               # Create Booking, My Bookings, Cancel
│   ├── ReviewController.cs                # Submit Reviews
│   └── 📂 Admin/                          # Admin-only Controllers
│       ├── DashboardController.cs         # Admin Dashboard & Statistics
│       ├── AdminHotelsController.cs       # Hotel CRUD Operations
│       └── AdminBookingsController.cs     # Booking Management
│
├── 📂 Models/                             # Data Models & Entities
│   ├── ApplicationUser.cs                 # User entity (extends IdentityUser)
│   ├── Hotel.cs                           # Hotel entity
│   ├── Room.cs                            # Room entity
│   ├── RoomImage.cs                       # Room image entity
│   ├── Booking.cs                         # Booking entity with status enum
│   ├── Review.cs                          # Review entity
│   └── 📂 ViewModels/                     # View-specific models
│       ├── RegisterViewModel.cs           # Registration form
│       ├── LoginViewModel.cs              # Login form
│       ├── HotelSearchViewModel.cs        # Search & filter
│       ├── BookingViewModel.cs            # Booking form with payment
│       └── ReviewViewModel.cs             # Review submission form
│
├── 📂 Data/                               # Database Context & Seeding
│   ├── ApplicationDbContext.cs            # EF Core DbContext
│   └── DbInitializer.cs                   # Database seeding logic
│
├── 📂 Views/                              # Razor Views (UI Templates)
│   ├── _ViewImports.cshtml                # Shared imports
│   ├── _ViewStart.cshtml                  # Default layout
│   │
│   ├── 📂 Shared/                         # Shared layout & partials
│   │   ├── _Layout.cshtml                 # Main layout (user-facing)
│   │   ├── _AdminLayout.cshtml            # Admin layout
│   │   └── _ValidationScriptsPartial.cshtml
│   │
│   ├── 📂 Home/                           # Public pages
│   │   ├── Index.cshtml                   # Hotel search & listing
│   │   ├── HotelDetails.cshtml            # Hotel & room details with slider
│   │   └── Privacy.cshtml                 # Privacy policy
│   │
│   ├── 📂 Account/                        # Authentication pages
│   │   ├── Login.cshtml                   # Login form
│   │   ├── Register.cshtml                # Registration form
│   │   └── AccessDenied.cshtml            # Access denied page
│   │
│   ├── 📂 Booking/                        # Booking pages
│   │   ├── Create.cshtml                  # Booking form with payment
│   │   ├── Confirmation.cshtml            # Booking confirmation
│   │   └── MyBookings.cshtml              # User's booking history
│   │
│   ├── 📂 Review/                         # Review pages
│   │   └── Create.cshtml                  # Review submission form
│   │
│   └── 📂 Admin/                          # Admin area views
│       ├── 📂 Dashboard/
│       │   └── Index.cshtml               # Admin dashboard with stats
│       ├── 📂 AdminHotels/
│       │   ├── Index.cshtml               # Hotel list
│       │   ├── Create.cshtml              # Create hotel
│       │   └── Edit.cshtml                # Edit hotel
│       └── 📂 AdminBookings/
│           ├── Index.cshtml               # Booking list with filters
│           └── Details.cshtml             # Booking details & status update
│
├── 📂 wwwroot/                            # Static Files (CSS, JS, Images)
│   ├── 📂 css/
│   │   └── site.css                       # Complete styling with theme system
│   └── 📂 js/
│       └── site.js                        # Theme toggle & navbar functionality
│
├── 📂 Migrations/                         # EF Core Migrations (auto-generated)
│   └── [timestamp]_InitialCreate.cs
│
├── 📄 hotelbooking.db                     # SQLite Database (created on first run)
├── 📄 .gitignore                          # Git ignore rules
├── 📄 README.md                           # Project documentation
├── 📄 FEATURES.md                         # Complete feature list
└── 📄 run.bat                             # Quick start script
```

## 🔑 Key Components

### Controllers (7 files)
- **AccountController**: User authentication (login, register, logout)
- **HomeController**: Public-facing pages (search, hotel details)
- **BookingController**: Booking management (create, view, cancel)
- **ReviewController**: Review submission with validation
- **DashboardController**: Admin statistics dashboard
- **AdminHotelsController**: Hotel CRUD operations
- **AdminBookingsController**: Booking management for admins

### Models (11 files)
- **Domain Models**: ApplicationUser, Hotel, Room, RoomImage, Booking, Review
- **View Models**: Register, Login, HotelSearch, Booking, Review

### Views (22 files)
- **Layouts**: 2 (User & Admin)
- **Public Pages**: 3 (Search, Details, Privacy)
- **Account Pages**: 3 (Login, Register, Access Denied)
- **Booking Pages**: 3 (Create, Confirmation, My Bookings)
- **Review Pages**: 1 (Create)
- **Admin Pages**: 6 (Dashboard, Hotel List/Create/Edit, Booking List/Details)
- **Shared**: 3 (Layouts & partials)

### Static Files
- **CSS**: 1 comprehensive stylesheet (~900 lines) with theme support
- **JS**: 1 script for theme toggle and mobile navigation

## 🗄️ Database Schema

### Tables
1. **AspNetUsers** - User accounts (extends Identity)
2. **AspNetRoles** - Roles (Admin, User)
3. **AspNetUserRoles** - User-Role mappings
4. **Hotels** - Hotel information
5. **Rooms** - Room types and details
6. **RoomImages** - Multiple images per room
7. **Bookings** - Booking records
8. **Reviews** - Guest reviews

### Relationships
- Hotel → Rooms (1:Many)
- Room → RoomImages (1:Many)
- Room → Bookings (1:Many)
- Hotel → Reviews (1:Many)
- User → Bookings (1:Many)
- User → Reviews (1:Many)

## 🎨 Styling System

### CSS Architecture
- **CSS Custom Properties** for theme values
- **Light/Dark themes** with smooth transitions
- **Responsive design** with mobile-first approach
- **Component-based** styling (cards, buttons, forms)
- **Utility classes** for common patterns

### Key Design Elements
- Pill-style buttons throughout
- Card-based layouts
- Consistent spacing and shadows
- High contrast for accessibility
- Smooth hover effects
- Professional color palette

## 🔐 Security Features

1. **ASP.NET Core Identity** for authentication
2. **Role-based authorization** (Admin/User)
3. **Anti-forgery tokens** on all forms
4. **Password hashing** with Identity's secure defaults
5. **Input validation** (client & server)
6. **SQL injection prevention** via EF Core parameterized queries

## 📦 NuGet Packages

- Microsoft.AspNetCore.Identity.EntityFrameworkCore (8.0.0)
- Microsoft.EntityFrameworkCore.Sqlite (8.0.0)
- Microsoft.EntityFrameworkCore.Tools (8.0.0)
- Microsoft.AspNetCore.Identity.UI (8.0.0)

## 🚀 Entry Points

1. **User Entry**: `/` → Home/Index (Hotel Search)
2. **Login**: `/Account/Login`
3. **Register**: `/Account/Register`
4. **Admin**: `/Admin/Dashboard/Index`
5. **My Bookings**: `/Booking/MyBookings` (requires login)

## 📊 Statistics

- **Total Lines of Code**: ~5,000+
- **Controllers**: 7 files
- **Models**: 11 files
- **Views**: 22 files
- **CSS Lines**: ~900
- **JavaScript Lines**: ~50
- **Database Tables**: 8 main tables + Identity tables
- **Routes**: 30+ defined routes

## 🎯 Design Patterns Used

1. **MVC Pattern** - Separation of concerns
2. **Repository Pattern** - Through EF Core DbContext
3. **View Model Pattern** - Separate models for views
4. **Dependency Injection** - Built-in ASP.NET Core DI
5. **Factory Pattern** - For user/role creation
6. **Observer Pattern** - Through ASP.NET Core events
