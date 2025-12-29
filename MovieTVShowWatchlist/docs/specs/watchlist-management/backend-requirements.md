# Backend Requirements - Watchlist Management

## API Endpoints

### POST /api/watchlist/movies
- Add a movie to the user's watchlist
- Request Body: { movieId, title, releaseYear, genres, director, runtime, priorityLevel, recommendationSource, availability }
- Response: WatchlistItem with ID and timestamp
- Publishes: MovieAddedToWatchlist event

### POST /api/watchlist/tvshows
- Add a TV show to the user's watchlist
- Request Body: { showId, title, premiereYear, genres, numberOfSeasons, status, priority, streamingPlatform }
- Response: WatchlistItem with ID and timestamp
- Publishes: TVShowAddedToWatchlist event

### DELETE /api/watchlist/{itemId}
- Remove an item from the watchlist
- Request Body: { removalReason, alternativeAdded }
- Response: Success confirmation with removal details
- Publishes: WatchlistItemRemoved event

### PUT /api/watchlist/prioritize
- Reorder and reprioritize watchlist items
- Request Body: { itemRankings, sortingCriteria, moodBasedCategories }
- Response: Updated watchlist with new priorities
- Publishes: WatchlistPrioritized event

### GET /api/watchlist
- Retrieve user's complete watchlist
- Query Parameters: sortBy, filterByGenre, filterByPriority, filterByMood
- Response: Paginated list of watchlist items

### GET /api/watchlist/{itemId}
- Retrieve specific watchlist item details
- Response: Full watchlist item information

## Domain Events

### MovieAddedToWatchlist
```csharp
public class MovieAddedToWatchlist : DomainEvent
{
    public Guid MovieId { get; set; }
    public string Title { get; set; }
    public int ReleaseYear { get; set; }
    public List<string> Genres { get; set; }
    public string Director { get; set; }
    public int Runtime { get; set; }
    public DateTime AddedDate { get; set; }
    public string PriorityLevel { get; set; }
    public string RecommendationSource { get; set; }
    public string Availability { get; set; }
}
```

### TVShowAddedToWatchlist
```csharp
public class TVShowAddedToWatchlist : DomainEvent
{
    public Guid ShowId { get; set; }
    public string Title { get; set; }
    public int PremiereYear { get; set; }
    public List<string> Genres { get; set; }
    public int NumberOfSeasons { get; set; }
    public string Status { get; set; }
    public DateTime AddedDate { get; set; }
    public string Priority { get; set; }
    public string StreamingPlatform { get; set; }
}
```

### WatchlistItemRemoved
```csharp
public class WatchlistItemRemoved : DomainEvent
{
    public Guid ItemId { get; set; }
    public string ItemType { get; set; }
    public DateTime RemovalDate { get; set; }
    public string RemovalReason { get; set; }
    public TimeSpan TimeOnWatchlist { get; set; }
    public string AlternativeAdded { get; set; }
}
```

### WatchlistPrioritized
```csharp
public class WatchlistPrioritized : DomainEvent
{
    public DateTime ReorderTimestamp { get; set; }
    public Dictionary<Guid, int> ItemRankings { get; set; }
    public Dictionary<Guid, string> PriorityChanges { get; set; }
    public string SortingCriteria { get; set; }
    public List<string> MoodBasedCategories { get; set; }
    public Dictionary<Guid, int> WatchOrderPreferences { get; set; }
}
```

## Database Schema

### WatchlistItems Table
```sql
CREATE TABLE WatchlistItems (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    ContentId UNIQUEIDENTIFIER NOT NULL,
    ContentType VARCHAR(20) NOT NULL, -- 'Movie' or 'TVShow'
    Title NVARCHAR(500) NOT NULL,
    AddedDate DATETIME2 NOT NULL,
    PriorityLevel VARCHAR(20),
    PriorityRank INT,
    RecommendationSource NVARCHAR(200),
    MoodCategory NVARCHAR(100),
    WatchOrderPreference INT,
    CreatedAt DATETIME2 NOT NULL,
    UpdatedAt DATETIME2 NOT NULL,
    IsDeleted BIT NOT NULL DEFAULT 0,
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);

CREATE INDEX IX_WatchlistItems_UserId ON WatchlistItems(UserId);
CREATE INDEX IX_WatchlistItems_PriorityRank ON WatchlistItems(PriorityRank);
```

### Movies Table
```sql
CREATE TABLE Movies (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    Title NVARCHAR(500) NOT NULL,
    ReleaseYear INT NOT NULL,
    Director NVARCHAR(200),
    Runtime INT,
    ExternalId VARCHAR(50), -- TMDB/IMDB ID
    CreatedAt DATETIME2 NOT NULL,
    UpdatedAt DATETIME2 NOT NULL
);
```

### TVShows Table
```sql
CREATE TABLE TVShows (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    Title NVARCHAR(500) NOT NULL,
    PremiereYear INT NOT NULL,
    NumberOfSeasons INT,
    Status VARCHAR(20), -- 'Ongoing' or 'Ended'
    ExternalId VARCHAR(50), -- TMDB ID
    CreatedAt DATETIME2 NOT NULL,
    UpdatedAt DATETIME2 NOT NULL
);
```

### ContentGenres Table
```sql
CREATE TABLE ContentGenres (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    ContentId UNIQUEIDENTIFIER NOT NULL,
    ContentType VARCHAR(20) NOT NULL,
    Genre NVARCHAR(100) NOT NULL,
    CreatedAt DATETIME2 NOT NULL
);

CREATE INDEX IX_ContentGenres_ContentId ON ContentGenres(ContentId);
```

### ContentAvailability Table
```sql
CREATE TABLE ContentAvailability (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    ContentId UNIQUEIDENTIFIER NOT NULL,
    ContentType VARCHAR(20) NOT NULL,
    Platform NVARCHAR(100) NOT NULL,
    IsAvailable BIT NOT NULL,
    SubscriptionRequired BIT NOT NULL,
    AvailabilityWindow NVARCHAR(200),
    RegionalRestrictions NVARCHAR(500),
    LastChecked DATETIME2 NOT NULL,
    CreatedAt DATETIME2 NOT NULL,
    UpdatedAt DATETIME2 NOT NULL
);

CREATE INDEX IX_ContentAvailability_ContentId ON ContentAvailability(ContentId);
```

## Business Logic

### Watchlist Service
- Validate content exists before adding to watchlist
- Prevent duplicate watchlist entries
- Automatically assign priority rank when not specified
- Track time on watchlist for analytics
- Support bulk prioritization operations
- Integrate with availability checker service

### Event Handlers
- WatchlistNotifier: Sends notifications for availability changes
- RecommendationEngine: Updates recommendations based on watchlist additions
- AvailabilityTracker: Monitors streaming platform availability
- ReminderScheduler: Creates viewing reminders based on priorities

## Integration Points
- External content API (TMDB/IMDB) for metadata
- Streaming platform APIs for availability
- Notification service for availability alerts
- Recommendation engine for content suggestions

## Performance Considerations
- Index on UserId and PriorityRank for fast watchlist retrieval
- Cache frequently accessed watchlist data
- Batch availability checks to reduce API calls
- Optimize priority updates to minimize database writes

## Security
- Ensure users can only access their own watchlist
- Validate all input data for XSS and injection attacks
- Rate limit API endpoints to prevent abuse
- Encrypt sensitive recommendation source data
