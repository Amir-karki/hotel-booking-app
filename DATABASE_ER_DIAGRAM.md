# Hotel Booking App - Database ER Diagram

## Entity Relationship Diagram

```
┌─────────────────────────────────────────────────────────────────────────────────────┐
│                          HOTEL BOOKING APPLICATION DATABASE                          │
└─────────────────────────────────────────────────────────────────────────────────────┘

┌──────────────────────────────┐
│     AspNetUsers              │
│  (ApplicationUser)           │
├──────────────────────────────┤
│ PK  Id (string)              │
│     UserName                 │
│     Email                    │
│     PasswordHash             │
│     FirstName                │
│     LastName                 │
│     ProfilePictureUrl        │
│     EmailConfirmed           │
│     PhoneNumber              │
│     CreatedAt                │
└──────────────────────────────┘
        │                  │
        │                  │
        │ 1              1 │
        │                  │
        │                  └──────────────────────┐
        │                                         │
        │                                         │
        │ *                                       │ *
┌───────▼──────────────────┐              ┌──────▼─────────────────┐
│      Bookings            │              │      Reviews           │
├──────────────────────────┤              ├────────────────────────┤
│ PK  Id (int)             │              │ PK  Id (int)           │
│ FK  RoomId               │              │ FK  HotelId            │
│ FK  UserId               │              │ FK  UserId             │
│     CheckInDate          │              │     Rating (1-5)       │
│     CheckOutDate         │              │     Title              │
│     NumberOfGuests       │              │     Comment            │
│     TotalPrice           │              │     CreatedAt          │
│     GuestFullName        │              └────────────────────────┘
│     GuestEmail           │                       │
│     GuestPhone           │                       │
│     SpecialRequests      │                       │ *
│     Status (enum)        │                       │
│     PaymentIntentId      │                       │
│     BookedAt             │                       │ 1
└──────────────────────────┘              ┌────────▼───────────────┐
        │                                 │      Hotels            │
        │ *                               ├────────────────────────┤
        │                                 │ PK  Id (int)           │
        │                                 │     Name               │
        │ 1                               │     Description        │
┌───────▼──────────────────┐              │     Address            │
│       Rooms              │              │     City               │
├──────────────────────────┤              │     Country            │
│ PK  Id (int)             │              │     StarRating (1-5)   │
│ FK  HotelId              │◄─────────┐   │     ImageUrl           │
│     RoomType             │          │   │     IsActive           │
│     Description          │          │   │     CreatedAt          │
│     PricePerNight        │          │   └────────────────────────┘
│     Capacity             │          │            │
│     TotalRooms           │          │ *          │
│     IsAvailable          │          │            │ 1
│     HasWifi              │          │            │
│     HasAirConditioning   │          │            │
│     HasTelevision        │          └────────────┘
│     HasMinibar           │
│     HasBalcony           │
│     CreatedAt            │
└──────────────────────────┘
        │
        │ 1
        │
        │
        │ *
┌───────▼──────────────────┐
│    RoomImages            │
├──────────────────────────┤
│ PK  Id (int)             │
│ FK  RoomId               │
│     ImageUrl             │
│     DisplayOrder         │
│     CreatedAt            │
└──────────────────────────┘


┌─────────────────────────────────────────────────────────────────────┐
│                    IDENTITY FRAMEWORK TABLES                        │
│                   (Managed by ASP.NET Core Identity)                │
├─────────────────────────────────────────────────────────────────────┤
│                                                                     │
│  AspNetRoles                  AspNetUserRoles                      │
│  ├─ Id                        ├─ UserId (FK)                       │
│  ├─ Name                      └─ RoleId (FK)                       │
│  └─ NormalizedName                                                 │
│                                                                     │
│  AspNetUserClaims             AspNetRoleClaims                     │
│  AspNetUserLogins             AspNetUserTokens                     │
└─────────────────────────────────────────────────────────────────────┘
```

## Relationships Summary

### 1. Hotel → Rooms (One-to-Many)
- **Relationship**: One hotel can have multiple rooms
- **Foreign Key**: `Rooms.HotelId` references `Hotels.Id`
- **Cascade**: Deleting a hotel deletes all its rooms

### 2. Room → RoomImages (One-to-Many)
- **Relationship**: One room can have multiple images
- **Foreign Key**: `RoomImages.RoomId` references `Rooms.Id`
- **Cascade**: Deleting a room deletes all its images
- **Note**: Images are ordered by `DisplayOrder` field

### 3. Room → Bookings (One-to-Many)
- **Relationship**: One room can have multiple bookings
- **Foreign Key**: `Bookings.RoomId` references `Rooms.Id`
- **Cascade**: Room cannot be deleted if it has bookings

### 4. ApplicationUser → Bookings (One-to-Many)
- **Relationship**: One user can make multiple bookings
- **Foreign Key**: `Bookings.UserId` references `AspNetUsers.Id`
- **Cascade**: User cannot be deleted if they have bookings

### 5. Hotel → Reviews (One-to-Many)
- **Relationship**: One hotel can have multiple reviews
- **Foreign Key**: `Reviews.HotelId` references `Hotels.Id`
- **Cascade**: Deleting a hotel deletes all its reviews

### 6. ApplicationUser → Reviews (One-to-Many)
- **Relationship**: One user can write multiple reviews (one per hotel)
- **Foreign Key**: `Reviews.UserId` references `AspNetUsers.Id`
- **Business Rule**: One user can only write one review per hotel

## Entity Details

### Hotels
**Purpose**: Stores hotel information and metadata
- Primary entity for the application
- Contains location, rating, and status information
- Has computed property `AverageRating` from related reviews

**Key Fields**:
- `StarRating`: Hotel star classification (1-5)
- `IsActive`: Controls whether hotel is visible to users
- `ImageUrl`: Main hotel image for display

### Rooms
**Purpose**: Stores room types and pricing for each hotel
- Multiple room types can exist per hotel (Standard, Deluxe, Suite, etc.)
- Contains pricing, capacity, and availability information
- Tracks amenities (WiFi, AC, TV, Minibar, Balcony)

**Key Fields**:
- `RoomType`: Type/name of the room (e.g., "Deluxe Suite")
- `PricePerNight`: Decimal(18,2) - Room rate
- `TotalRooms`: Number of this room type available
- `Capacity`: Maximum number of guests

### RoomImages
**Purpose**: Stores multiple images for each room type
- Enables image slider/gallery functionality
- Images are ordered by `DisplayOrder`

**Key Fields**:
- `ImageUrl`: URL to the image
- `DisplayOrder`: Controls order in slider (1, 2, 3...)

### Bookings
**Purpose**: Stores guest reservations
- Links users to rooms for specific dates
- Tracks booking status and payment information
- Stores guest details for each booking

**Key Fields**:
- `Status`: Enum (Pending, Confirmed, Cancelled, Completed)
- `TotalPrice`: Calculated total for the stay
- `PaymentIntentId`: Payment gateway reference

**Status Flow**:
1. **Pending** → Initial state after booking creation
2. **Confirmed** → Admin approves or payment succeeds
3. **Completed** → After checkout date passes
4. **Cancelled** → User or admin cancels

### Reviews
**Purpose**: Stores user reviews and ratings for hotels
- Only users with completed bookings can review
- One review per user per hotel (enforced at application level)

**Key Fields**:
- `Rating`: Integer 1-5 star rating
- `Title`: Short review headline
- `Comment`: Detailed review text

### ApplicationUser
**Purpose**: Extended ASP.NET Identity user with custom fields
- Inherits from `IdentityUser`
- Adds profile information and picture support

**Custom Fields**:
- `FirstName`, `LastName`: User's name
- `ProfilePictureUrl`: Path to uploaded profile image
- `CreatedAt`: Account creation timestamp

## Database Indexes

### Recommended Indexes (Implicit via Foreign Keys)
1. `IX_Rooms_HotelId` - For querying rooms by hotel
2. `IX_Bookings_RoomId` - For querying bookings by room
3. `IX_Bookings_UserId` - For querying user's bookings
4. `IX_Reviews_HotelId` - For querying hotel reviews
5. `IX_Reviews_UserId` - For querying user's reviews
6. `IX_RoomImages_RoomId` - For querying room images

### Additional Performance Indexes (Consider Adding)
1. `IX_Hotels_City_IsActive` - For city-based searches
2. `IX_Hotels_Country_IsActive` - For country-based searches
3. `IX_Bookings_Status` - For admin booking management
4. `IX_Bookings_CheckInDate_CheckOutDate` - For availability queries

## Data Constraints

### Hotels
- `Name`: Required, Max 200 characters
- `StarRating`: Range 1-5
- `IsActive`: Boolean, Default: true

### Rooms
- `PricePerNight`: Decimal(18,2), Range 0.01-100000
- `Capacity`: Range 1-10
- `TotalRooms`: Range 1-100

### Bookings
- `CheckOutDate` must be after `CheckInDate`
- `NumberOfGuests`: Range 1-10
- `TotalPrice`: Decimal(18,2)

### Reviews
- `Rating`: Range 1-5
- `Title`: Required, Max 200 characters
- `Comment`: Required, Max 2000 characters

## Database Technology

- **Database**: SQLite
- **ORM**: Entity Framework Core 8.0
- **Connection String**: `Data Source=hotelbooking.db`
- **Migrations**: 2 migrations applied
  1. `InitialCreate` - Core schema
  2. `AddProfilePicture` - Added ProfilePictureUrl to AspNetUsers

## Sample Data Seeding

The database is automatically seeded with:
- **2 Roles**: Admin, User
- **2 Users**:
  - Admin (admin@hotelbooking.com)
  - Regular User (user@hotelbooking.com)
- **10 Hotels**: Across various cities (NY, Miami, LA, London, Paris, Dubai, etc.)
- **~50 Rooms**: Multiple room types per hotel
- **~150 Room Images**: 3-5 images per room type
- **Sample Reviews**: With ratings and comments

## Business Rules Implemented

1. **One Review Per User Per Hotel**: Users can only submit one review per hotel
2. **Review Eligibility**: Only users with completed bookings can review that hotel
3. **Booking Validation**: Check-out must be after check-in
4. **Room Availability**: Rooms have `IsAvailable` flag for management
5. **Hotel Visibility**: Only active hotels (`IsActive = true`) shown to users
6. **Cascade Deletes**:
   - Hotel deletion removes all rooms, images, and reviews
   - Room deletion removes all images
   - Bookings and reviews prevent user deletion

## Query Patterns

### Common Queries:

```sql
-- Get all active hotels with their rooms
SELECT h.*, r.*
FROM Hotels h
LEFT JOIN Rooms r ON h.Id = r.HotelId
WHERE h.IsActive = 1;

-- Get user's bookings with room and hotel details
SELECT b.*, r.*, h.*
FROM Bookings b
JOIN Rooms r ON b.RoomId = r.Id
JOIN Hotels h ON r.HotelId = h.Id
WHERE b.UserId = @userId;

-- Get hotel with average rating
SELECT h.*, AVG(rv.Rating) as AvgRating
FROM Hotels h
LEFT JOIN Reviews rv ON h.Id = rv.HotelId
GROUP BY h.Id;

-- Check room availability (no overlapping bookings)
SELECT r.*
FROM Rooms r
WHERE r.Id = @roomId
AND NOT EXISTS (
    SELECT 1 FROM Bookings b
    WHERE b.RoomId = r.Id
    AND b.Status IN (0, 1) -- Pending or Confirmed
    AND (b.CheckInDate <= @checkOut AND b.CheckOutDate >= @checkIn)
);
```

## Schema Evolution

### Migration History:
1. **20251209071207_InitialCreate**
   - Created all core tables
   - Established relationships
   - Applied constraints

2. **20251209072908_AddProfilePicture**
   - Added `ProfilePictureUrl` to ApplicationUser
   - Enables user profile picture uploads

### Future Migration Considerations:
- Payment transactions table
- Booking modification history
- Hotel amenities table (separate from rooms)
- Favorite hotels (user-hotel many-to-many)
- Booking reviews (separate from hotel reviews)
