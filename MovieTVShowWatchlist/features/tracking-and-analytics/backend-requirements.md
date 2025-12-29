# Backend Requirements - Tracking and Analytics

## API Endpoints

### GET /api/analytics/streak
- Get current viewing streak information
- Response: Current streak, longest streak, last watched date

### GET /api/analytics/milestones
- Get achieved milestones
- Response: List of milestones with achievement dates

### GET /api/analytics/viewing-time
- Get viewing time statistics
- Query Parameters: period (week/month/year/all)
- Response: Total hours, breakdown by content type, genre, platform

### GET /api/analytics/year-in-review/{year}
- Get annual viewing summary
- Response: Comprehensive year statistics and highlights

### POST /api/analytics/calculate
- Trigger manual statistics calculation
- Response: Updated statistics

## Domain Events

### ViewingStreakUpdated
```csharp
public class ViewingStreakUpdated : DomainEvent
{
    public Guid StreakId { get; set; }
    public int CurrentStreak { get; set; }
    public int LongestStreak { get; set; }
    public DateTime LastWatchedDate { get; set; }
    public string StreakStatus { get; set; }
    public List<string> ContentTypesInStreak { get; set; }
}
```

### ViewingMilestoneAchieved
```csharp
public class ViewingMilestoneAchieved : DomainEvent
{
    public Guid MilestoneId { get; set; }
    public string MilestoneType { get; set; }
    public DateTime AchievementDate { get; set; }
    public int MetricAchieved { get; set; }
    public Dictionary<string, int> ContentBreakdown { get; set; }
    public string HistoricalContext { get; set; }
    public string CelebrationTier { get; set; }
}
```

### ViewingTimeCalculated
```csharp
public class ViewingTimeCalculated : DomainEvent
{
    public Guid CalculationId { get; set; }
    public string TimePeriod { get; set; }
    public decimal TotalHours { get; set; }
    public Dictionary<string, decimal> ContentBreakdown { get; set; }
    public Dictionary<string, decimal> PlatformDistribution { get; set; }
    public Dictionary<string, decimal> GenreDistribution { get; set; }
    public decimal ComparisonToPreviousPeriod { get; set; }
}
```

### YearInReviewGenerated
```csharp
public class YearInReviewGenerated : DomainEvent
{
    public Guid ReportId { get; set; }
    public int Year { get; set; }
    public int TotalMoviesWatched { get; set; }
    public int TotalShowsWatched { get; set; }
    public decimal TotalHours { get; set; }
    public List<string> FavoriteGenres { get; set; }
    public List<Guid> TopRatedContent { get; set; }
    public Dictionary<string, object> ViewingTrends { get; set; }
    public List<string> MemorableMoments { get; set; }
}
```

## Database Schema

### ViewingStreaks Table
```sql
CREATE TABLE ViewingStreaks (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    CurrentStreak INT NOT NULL DEFAULT 0,
    LongestStreak INT NOT NULL DEFAULT 0,
    LastWatchedDate DATETIME2,
    StreakStartDate DATETIME2,
    UpdatedAt DATETIME2 NOT NULL,
    FOREIGN KEY (UserId) REFERENCES Users(Id),
    UNIQUE (UserId)
);
```

### Milestones Table
```sql
CREATE TABLE Milestones (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    MilestoneType VARCHAR(100) NOT NULL,
    MetricAchieved INT NOT NULL,
    AchievementDate DATETIME2 NOT NULL,
    CelebrationTier VARCHAR(20),
    CreatedAt DATETIME2 NOT NULL,
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);
```

### ViewingStatistics Table
```sql
CREATE TABLE ViewingStatistics (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    TimePeriod VARCHAR(20) NOT NULL,
    PeriodStart DATETIME2 NOT NULL,
    PeriodEnd DATETIME2 NOT NULL,
    TotalHours DECIMAL(10,2) NOT NULL,
    TotalMovies INT NOT NULL,
    TotalShows INT NOT NULL,
    TotalEpisodes INT NOT NULL,
    CalculatedAt DATETIME2 NOT NULL,
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);
```

### YearInReviews Table
```sql
CREATE TABLE YearInReviews (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    Year INT NOT NULL,
    TotalMovies INT NOT NULL,
    TotalShows INT NOT NULL,
    TotalHours DECIMAL(10,2) NOT NULL,
    TopGenres NVARCHAR(MAX), -- JSON
    TopRated NVARCHAR(MAX), -- JSON
    ViewingTrends NVARCHAR(MAX), -- JSON
    MemorableMoments NVARCHAR(MAX), -- JSON
    GeneratedAt DATETIME2 NOT NULL,
    FOREIGN KEY (UserId) REFERENCES Users(Id),
    UNIQUE (UserId, Year)
);
```

## Business Logic

### Streak Calculator
- Update streak daily based on viewing activity
- Break streak if no viewing for 24+ hours
- Track longest streak achieved
- Support streak freeze/pause (optional feature)

### Milestone Detector
- Monitor for achievement thresholds
- Milestones: 10, 25, 50, 100, 250, 500, 1000 movies/shows/episodes
- Assign celebration tiers (bronze, silver, gold, platinum)
- Trigger notifications on milestone achievement

### Statistics Aggregator
- Calculate viewing time per period
- Aggregate by content type, genre, platform
- Compare to previous periods
- Run calculations nightly

### Year-in-Review Generator
- Generate comprehensive annual summary
- Identify top genres, highest rated content
- Calculate viewing trends
- Create shareable summary
- Generate automatically on January 1st

## Integration Points
- Notification service for milestone celebrations
- Social sharing for year-in-review
- Badge/achievement system
- Dashboard visualization service

## Performance Considerations
- Pre-calculate statistics daily
- Cache current streak for fast access
- Index milestones by user and type
- Optimize year-in-review queries
