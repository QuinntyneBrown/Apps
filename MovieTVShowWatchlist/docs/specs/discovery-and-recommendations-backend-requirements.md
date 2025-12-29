# Backend Requirements - Discovery and Recommendations

## API Endpoints

### POST /api/recommendations/receive
- Record a received recommendation
- Request Body: { contentId, recommender, source, reason, interestLevel }
- Response: Recommendation with ID
- Publishes: RecommendationReceived event

### POST /api/recommendations/give
- Give a recommendation to someone
- Request Body: { contentId, recipient, reason, sharingMethod }
- Response: Recommendation with ID
- Publishes: RecommendationGiven event

### PUT /api/recommendations/{recommendationId}/feedback
- Provide feedback on received recommendation
- Request Body: { watched, liked, feedback }
- Response: Updated recommendation
- Publishes: RecommendationFeedbackProvided event

### GET /api/recommendations/received
- Get recommendations received by user
- Query Parameters: status, source, page, limit
- Response: Paginated recommendations

### GET /api/recommendations/given
- Get recommendations given by user
- Response: Paginated recommendations with recipient feedback

### GET /api/discovery/similar/{contentId}
- Get similar content recommendations
- Response: List of similar content with similarity scores
- Publishes: SimilarContentDiscovered event

### GET /api/discovery/for-you
- Get personalized recommendations
- Query Parameters: limit, excludeWatched
- Response: Personalized content recommendations

### GET /api/discovery/trending
- Get trending content based on platform
- Response: Trending movies and shows

### GET /api/preferences/genres
- Get identified genre preferences
- Response: User's genre preferences with strength scores

## Domain Events

### RecommendationReceived
```csharp
public class RecommendationReceived : DomainEvent
{
    public Guid RecommendationId { get; set; }
    public Guid ContentId { get; set; }
    public string Recommender { get; set; }
    public string RecommendationSource { get; set; }
    public string Reason { get; set; }
    public DateTime ReceptionDate { get; set; }
    public string InterestLevel { get; set; }
    public bool AddedToWatchlist { get; set; }
}
```

### RecommendationGiven
```csharp
public class RecommendationGiven : DomainEvent
{
    public Guid RecommendationId { get; set; }
    public Guid ContentId { get; set; }
    public string Recipient { get; set; }
    public string RecommendationReason { get; set; }
    public string SharingMethod { get; set; }
    public DateTime ShareDate { get; set; }
    public string RecipientFeedback { get; set; }
}
```

### SimilarContentDiscovered
```csharp
public class SimilarContentDiscovered : DomainEvent
{
    public Guid DiscoveryId { get; set; }
    public Guid SourceContentId { get; set; }
    public List<Guid> SimilarContentIds { get; set; }
    public Dictionary<Guid, decimal> SimilarityScores { get; set; }
    public Dictionary<Guid, List<string>> MatchReasons { get; set; }
    public DateTime DiscoveryDate { get; set; }
    public string AlgorithmVersion { get; set; }
}
```

### GenrePreferenceIdentified
```csharp
public class GenrePreferenceIdentified : DomainEvent
{
    public Guid PreferenceId { get; set; }
    public string Genre { get; set; }
    public decimal PreferenceStrength { get; set; }
    public string Evidence { get; set; }
    public DateTime DetectionDate { get; set; }
    public string TrendDirection { get; set; }
}
```

## Database Schema

### RecommendationsReceived Table
```sql
CREATE TABLE RecommendationsReceived (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    ContentId UNIQUEIDENTIFIER NOT NULL,
    ContentType VARCHAR(20) NOT NULL,
    Recommender NVARCHAR(200) NOT NULL,
    RecommendationSource VARCHAR(50), -- 'Friend', 'System', 'Critic', etc.
    Reason NVARCHAR(1000),
    ReceptionDate DATETIME2 NOT NULL,
    InterestLevel VARCHAR(20),
    AddedToWatchlist BIT NOT NULL DEFAULT 0,
    Watched BIT NOT NULL DEFAULT 0,
    Liked BIT,
    Feedback NVARCHAR(500),
    CreatedAt DATETIME2 NOT NULL,
    UpdatedAt DATETIME2 NOT NULL,
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);

CREATE INDEX IX_RecommendationsReceived_UserId ON RecommendationsReceived(UserId);
```

### RecommendationsGiven Table
```sql
CREATE TABLE RecommendationsGiven (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    ContentId UNIQUEIDENTIFIER NOT NULL,
    ContentType VARCHAR(20) NOT NULL,
    Recipient NVARCHAR(200) NOT NULL,
    RecommendationReason NVARCHAR(1000),
    SharingMethod VARCHAR(50), -- 'Email', 'SMS', 'Social', 'InApp'
    ShareDate DATETIME2 NOT NULL,
    RecipientFeedback NVARCHAR(500),
    CreatedAt DATETIME2 NOT NULL,
    UpdatedAt DATETIME2 NOT NULL,
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);

CREATE INDEX IX_RecommendationsGiven_UserId ON RecommendationsGiven(UserId);
```

### SimilarContent Table
```sql
CREATE TABLE SimilarContent (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    SourceContentId UNIQUEIDENTIFIER NOT NULL,
    SimilarContentId UNIQUEIDENTIFIER NOT NULL,
    SimilarityScore DECIMAL(5,4) NOT NULL,
    MatchReasons NVARCHAR(MAX), -- JSON array
    AlgorithmVersion VARCHAR(20),
    DiscoveryDate DATETIME2 NOT NULL,
    CreatedAt DATETIME2 NOT NULL
);

CREATE INDEX IX_SimilarContent_SourceId ON SimilarContent(SourceContentId);
CREATE INDEX IX_SimilarContent_Score ON SimilarContent(SimilarityScore DESC);
```

### GenrePreferences Table
```sql
CREATE TABLE GenrePreferences (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    Genre NVARCHAR(100) NOT NULL,
    PreferenceStrength DECIMAL(5,4) NOT NULL,
    ViewingCount INT NOT NULL,
    AverageRating DECIMAL(3,1),
    TrendDirection VARCHAR(20), -- 'Increasing', 'Stable', 'Decreasing'
    DetectionDate DATETIME2 NOT NULL,
    LastUpdated DATETIME2 NOT NULL,
    FOREIGN KEY (UserId) REFERENCES Users(Id),
    UNIQUE (UserId, Genre)
);

CREATE INDEX IX_GenrePreferences_UserId_Strength ON GenrePreferences(UserId, PreferenceStrength DESC);
```

### DiscoveryQueue Table
```sql
CREATE TABLE DiscoveryQueue (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    ContentId UNIQUEIDENTIFIER NOT NULL,
    ContentType VARCHAR(20) NOT NULL,
    RecommendationScore DECIMAL(5,4) NOT NULL,
    RecommendationReason NVARCHAR(500),
    AddedDate DATETIME2 NOT NULL,
    Viewed BIT NOT NULL DEFAULT 0,
    Dismissed BIT NOT NULL DEFAULT 0,
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);

CREATE INDEX IX_DiscoveryQueue_UserId_Score ON DiscoveryQueue(UserId, RecommendationScore DESC);
```

## Business Logic

### Recommendation Engine
- Collaborative filtering based on similar users
- Content-based filtering using genres, directors, actors
- Hybrid approach combining multiple algorithms
- Consider user's ratings, viewing history, and favorites
- Factor in genre preferences and trends
- Exclude already watched content (configurable)
- Boost recommendations from trusted sources
- Update recommendations daily

### Similarity Calculator
- Compare genres, directors, actors, themes
- Analyze user ratings correlation
- Consider release period and style
- Calculate match scores (0-1 range)
- Generate explainable match reasons
- Version algorithm for A/B testing

### Preference Analyzer
- Analyze viewing patterns over time
- Calculate genre preference strength based on:
  - Viewing frequency
  - Average ratings
  - Watchlist additions
  - Time spent watching
- Detect preference trends (increasing/decreasing)
- Update preferences weekly

### Event Handlers
- WatchlistIntegration: Add high-interest recommendations to watchlist
- NotificationService: Notify of new recommendations
- SocialConnector: Share recommendations externally
- AnalyticsTracker: Track recommendation effectiveness

## Integration Points
- External recommendation APIs (TMDB recommendations)
- Social platforms for sharing
- Email/SMS services for external recommendations
- Analytics service for tracking effectiveness

## Performance Considerations
- Cache similarity calculations
- Pre-compute recommendations overnight
- Index by recommendation score for fast retrieval
- Batch preference calculations
- Optimize collaborative filtering queries

## Security
- Users control who can recommend to them
- Validate recommendation sources
- Prevent spam recommendations
- Rate limit recommendation sharing
- Encrypt personal preference data
