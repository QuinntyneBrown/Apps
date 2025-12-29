# Backend Requirements - Viewing Activity Tracking

## API Endpoints

### POST /api/viewing/movies/{movieId}/watched
- Mark a movie as watched
- Request Body: { watchDate, viewingLocation, viewingPlatform, watchedWith, viewingContext, isRewatch }
- Response: ViewingRecord with ID and timestamp
- Publishes: MovieWatched event

### POST /api/viewing/episodes/{episodeId}/watched
- Mark a TV episode as watched
- Request Body: { watchDate, platform, bingeSessionId, viewingDuration }
- Response: ViewingRecord with ID and timestamp
- Publishes: TVEpisodeWatched event
- May trigger: TVSeasonCompleted, TVShowCompleted events

### POST /api/viewing/{itemId}/abandon
- Mark content as abandoned
- Request Body: { abandonDate, progressPercent, abandonReason, qualityRating, wouldRetry }
- Response: AbandonRecord with details
- Publishes: ViewingAbandoned event

### GET /api/viewing/history
- Retrieve viewing history
- Query Parameters: startDate, endDate, contentType, platform, page, limit
- Response: Paginated viewing history

### GET /api/viewing/progress/{showId}
- Get viewing progress for a TV show
- Response: Season/episode progress, next episode to watch

### GET /api/viewing/statistics
- Get viewing statistics
- Query Parameters: period (week/month/year/all)
- Response: Total watched, hours, breakdown by type/genre/platform

## Domain Events

### MovieWatched
```csharp
public class MovieWatched : DomainEvent
{
    public Guid MovieId { get; set; }
    public DateTime WatchDate { get; set; }
    public string ViewingLocation { get; set; }
    public string ViewingPlatform { get; set; }
    public List<string> WatchedWith { get; set; }
    public string ViewingContext { get; set; }
    public bool IsRewatch { get; set; }
}
```

### TVEpisodeWatched
```csharp
public class TVEpisodeWatched : DomainEvent
{
    public Guid EpisodeId { get; set; }
    public Guid ShowId { get; set; }
    public int SeasonNumber { get; set; }
    public int EpisodeNumber { get; set; }
    public DateTime WatchDate { get; set; }
    public string Platform { get; set; }
    public Guid? BingeSessionId { get; set; }
    public TimeSpan ViewingDuration { get; set; }
}
```

### TVSeasonCompleted
```csharp
public class TVSeasonCompleted : DomainEvent
{
    public Guid SeasonId { get; set; }
    public Guid ShowId { get; set; }
    public int SeasonNumber { get; set; }
    public DateTime CompletionDate { get; set; }
    public TimeSpan BingeDuration { get; set; }
    public int EpisodesWatched { get; set; }
    public decimal? SeasonRating { get; set; }
    public bool NextSeasonIntent { get; set; }
}
```

### TVShowCompleted
```csharp
public class TVShowCompleted : DomainEvent
{
    public Guid ShowId { get; set; }
    public DateTime CompletionDate { get; set; }
    public int TotalEpisodes { get; set; }
    public TimeSpan TotalViewingTime { get; set; }
    public decimal? OverallRating { get; set; }
    public bool RewatchInterest { get; set; }
    public string SeriesFinaleReaction { get; set; }
}
```

### ViewingAbandoned
```csharp
public class ViewingAbandoned : DomainEvent
{
    public Guid ItemId { get; set; }
    public string ItemType { get; set; }
    public DateTime AbandonDate { get; set; }
    public decimal ProgressPercent { get; set; }
    public string AbandonReason { get; set; }
    public decimal? QualityRating { get; set; }
    public bool WouldRetry { get; set; }
}
```

## Database Schema

### ViewingRecords Table
```sql
CREATE TABLE ViewingRecords (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    ContentId UNIQUEIDENTIFIER NOT NULL,
    ContentType VARCHAR(20) NOT NULL,
    WatchDate DATETIME2 NOT NULL,
    ViewingPlatform NVARCHAR(100),
    ViewingLocation NVARCHAR(200),
    ViewingContext NVARCHAR(50),
    IsRewatch BIT NOT NULL DEFAULT 0,
    ViewingDuration INT, -- in minutes
    BingeSessionId UNIQUEIDENTIFIER,
    CreatedAt DATETIME2 NOT NULL,
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);

CREATE INDEX IX_ViewingRecords_UserId_WatchDate ON ViewingRecords(UserId, WatchDate);
CREATE INDEX IX_ViewingRecords_ContentId ON ViewingRecords(ContentId);
```

### EpisodeViewingRecords Table
```sql
CREATE TABLE EpisodeViewingRecords (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    EpisodeId UNIQUEIDENTIFIER NOT NULL,
    ShowId UNIQUEIDENTIFIER NOT NULL,
    SeasonNumber INT NOT NULL,
    EpisodeNumber INT NOT NULL,
    WatchDate DATETIME2 NOT NULL,
    Platform NVARCHAR(100),
    BingeSessionId UNIQUEIDENTIFIER,
    ViewingDuration INT,
    CreatedAt DATETIME2 NOT NULL,
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);

CREATE INDEX IX_EpisodeViewingRecords_UserId_ShowId ON EpisodeViewingRecords(UserId, ShowId);
```

### ShowProgress Table
```sql
CREATE TABLE ShowProgress (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    ShowId UNIQUEIDENTIFIER NOT NULL,
    LastWatchedSeason INT,
    LastWatchedEpisode INT,
    TotalEpisodesWatched INT NOT NULL DEFAULT 0,
    CompletedSeasons INT NOT NULL DEFAULT 0,
    IsCompleted BIT NOT NULL DEFAULT 0,
    CompletionDate DATETIME2,
    UpdatedAt DATETIME2 NOT NULL,
    FOREIGN KEY (UserId) REFERENCES Users(Id),
    UNIQUE (UserId, ShowId)
);
```

### AbandonedContent Table
```sql
CREATE TABLE AbandonedContent (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    ContentId UNIQUEIDENTIFIER NOT NULL,
    ContentType VARCHAR(20) NOT NULL,
    AbandonDate DATETIME2 NOT NULL,
    ProgressPercent DECIMAL(5,2),
    AbandonReason NVARCHAR(500),
    QualityRating DECIMAL(3,1),
    WouldRetry BIT,
    CreatedAt DATETIME2 NOT NULL,
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);

CREATE INDEX IX_AbandonedContent_UserId ON AbandonedContent(UserId);
```

### ViewingCompanions Table
```sql
CREATE TABLE ViewingCompanions (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    ViewingRecordId UNIQUEIDENTIFIER NOT NULL,
    CompanionName NVARCHAR(200) NOT NULL,
    CreatedAt DATETIME2 NOT NULL,
    FOREIGN KEY (ViewingRecordId) REFERENCES ViewingRecords(Id)
);
```

### BingeSessions Table
```sql
CREATE TABLE BingeSessions (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    ShowId UNIQUEIDENTIFIER NOT NULL,
    StartTime DATETIME2 NOT NULL,
    EndTime DATETIME2,
    EpisodesWatched INT NOT NULL DEFAULT 0,
    CreatedAt DATETIME2 NOT NULL,
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);
```

## Business Logic

### Viewing Service
- Automatically create binge sessions when multiple episodes watched in sequence
- Calculate show progress after each episode
- Detect season completion when last episode of season is watched
- Detect series completion when final episode is watched
- Validate episode order (prevent skipping)
- Calculate total viewing time
- Support multiple viewing records for rewatches

### Progress Calculator
- Track episode-by-episode progress
- Calculate completion percentage
- Identify next episode to watch
- Detect abandoned shows (no activity for 90 days)

### Event Handlers
- StatisticsCalculator: Updates viewing statistics
- RecommendationEngine: Refines recommendations based on viewing
- AchievementTracker: Detects milestones
- WatchlistManager: Removes completed content from watchlist
- ContinuationReminder: Schedules reminders for in-progress shows

## Integration Points
- Statistics service for aggregation
- Recommendation engine for preference learning
- Achievement service for milestone tracking
- Notification service for continuation reminders

## Performance Considerations
- Index on UserId and WatchDate for history queries
- Aggregate viewing statistics asynchronously
- Cache show progress for active shows
- Batch process binge session creation

## Security
- Users can only record their own viewing activity
- Validate content exists before recording viewing
- Prevent abuse through rate limiting
- Sanitize text inputs for abandon reasons
