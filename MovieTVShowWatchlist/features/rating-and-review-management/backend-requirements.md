# Backend Requirements - Rating and Review Management

## API Endpoints

### POST /api/ratings/movies/{movieId}
- Rate a movie
- Request Body: { ratingValue, ratingScale, ratingDate, viewingDate, isRewatchRating, mood }
- Response: Rating with ID and timestamp
- Publishes: MovieRated event

### POST /api/ratings/shows/{showId}
- Rate a TV show
- Request Body: { ratingValue, ratingDate, seasonsWatched, isCompleted, ratingEvolution }
- Response: Rating with ID and timestamp
- Publishes: TVShowRated event

### PUT /api/ratings/{ratingId}
- Update an existing rating
- Request Body: { ratingValue, mood }
- Response: Updated rating
- Publishes: RatingUpdated event

### DELETE /api/ratings/{ratingId}
- Delete a rating
- Response: Success confirmation
- Publishes: RatingDeleted event

### POST /api/reviews
- Write a review
- Request Body: { contentId, contentType, reviewText, hasSpoilers, themesDiscussed, wouldRecommend, targetAudience }
- Response: Review with ID and timestamp
- Publishes: ReviewWritten event

### PUT /api/reviews/{reviewId}
- Update a review
- Request Body: { reviewText, hasSpoilers, themesDiscussed, wouldRecommend, targetAudience }
- Response: Updated review
- Publishes: ReviewUpdated event

### DELETE /api/reviews/{reviewId}
- Delete a review
- Response: Success confirmation

### POST /api/favorites/{contentId}
- Mark content as favorite
- Request Body: { contentType, favoriteCategory, emotionalSignificance }
- Response: Favorite with ID and timestamp
- Publishes: FavoriteMarked event

### DELETE /api/favorites/{favoriteId}
- Remove from favorites
- Response: Success confirmation
- Publishes: FavoriteRemoved event

### GET /api/ratings
- Get user's ratings
- Query Parameters: contentType, minRating, maxRating, sortBy, page, limit
- Response: Paginated ratings list

### GET /api/reviews
- Get user's reviews
- Query Parameters: contentType, hasSpoilers, page, limit
- Response: Paginated reviews list

### GET /api/favorites
- Get user's favorites
- Query Parameters: contentType, category, sortBy, page, limit
- Response: Paginated favorites list

## Domain Events

### MovieRated
```csharp
public class MovieRated : DomainEvent
{
    public Guid RatingId { get; set; }
    public Guid MovieId { get; set; }
    public decimal RatingValue { get; set; }
    public string RatingScale { get; set; }
    public DateTime RatingDate { get; set; }
    public DateTime? ViewingDate { get; set; }
    public bool IsRewatchRating { get; set; }
    public string Mood { get; set; }
}
```

### TVShowRated
```csharp
public class TVShowRated : DomainEvent
{
    public Guid RatingId { get; set; }
    public Guid ShowId { get; set; }
    public decimal RatingValue { get; set; }
    public DateTime RatingDate { get; set; }
    public int SeasonsWatched { get; set; }
    public bool IsCompleted { get; set; }
    public Dictionary<int, decimal> RatingEvolutionBySeason { get; set; }
}
```

### ReviewWritten
```csharp
public class ReviewWritten : DomainEvent
{
    public Guid ReviewId { get; set; }
    public Guid ContentId { get; set; }
    public string ContentType { get; set; }
    public string ReviewText { get; set; }
    public bool HasSpoilers { get; set; }
    public DateTime ReviewDate { get; set; }
    public List<string> ThemesDiscussed { get; set; }
    public bool WouldRecommend { get; set; }
    public string TargetAudience { get; set; }
}
```

### FavoriteMarked
```csharp
public class FavoriteMarked : DomainEvent
{
    public Guid FavoriteId { get; set; }
    public Guid ContentId { get; set; }
    public string ContentType { get; set; }
    public DateTime AddedToFavoritesDate { get; set; }
    public string FavoriteCategory { get; set; }
    public int RewatchCount { get; set; }
    public string EmotionalSignificance { get; set; }
}
```

## Database Schema

### Ratings Table
```sql
CREATE TABLE Ratings (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    ContentId UNIQUEIDENTIFIER NOT NULL,
    ContentType VARCHAR(20) NOT NULL,
    RatingValue DECIMAL(3,1) NOT NULL,
    RatingScale VARCHAR(20) NOT NULL, -- '5-star', '10-point', etc.
    RatingDate DATETIME2 NOT NULL,
    ViewingDate DATETIME2,
    IsRewatchRating BIT NOT NULL DEFAULT 0,
    Mood NVARCHAR(100),
    CreatedAt DATETIME2 NOT NULL,
    UpdatedAt DATETIME2 NOT NULL,
    FOREIGN KEY (UserId) REFERENCES Users(Id),
    UNIQUE (UserId, ContentId, ContentType, ViewingDate)
);

CREATE INDEX IX_Ratings_UserId ON Ratings(UserId);
CREATE INDEX IX_Ratings_ContentId ON Ratings(ContentId);
CREATE INDEX IX_Ratings_RatingValue ON Ratings(RatingValue);
```

### SeasonRatings Table
```sql
CREATE TABLE SeasonRatings (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    RatingId UNIQUEIDENTIFIER NOT NULL,
    ShowId UNIQUEIDENTIFIER NOT NULL,
    SeasonNumber INT NOT NULL,
    RatingValue DECIMAL(3,1) NOT NULL,
    CreatedAt DATETIME2 NOT NULL,
    FOREIGN KEY (RatingId) REFERENCES Ratings(Id)
);
```

### Reviews Table
```sql
CREATE TABLE Reviews (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    ContentId UNIQUEIDENTIFIER NOT NULL,
    ContentType VARCHAR(20) NOT NULL,
    ReviewText NVARCHAR(MAX) NOT NULL,
    HasSpoilers BIT NOT NULL DEFAULT 0,
    ReviewDate DATETIME2 NOT NULL,
    WouldRecommend BIT NOT NULL,
    TargetAudience NVARCHAR(500),
    CreatedAt DATETIME2 NOT NULL,
    UpdatedAt DATETIME2 NOT NULL,
    FOREIGN KEY (UserId) REFERENCES Users(Id),
    UNIQUE (UserId, ContentId, ContentType)
);

CREATE INDEX IX_Reviews_UserId ON Reviews(UserId);
CREATE INDEX IX_Reviews_ContentId ON Reviews(ContentId);
CREATE FULLTEXT INDEX ON Reviews(ReviewText);
```

### ReviewThemes Table
```sql
CREATE TABLE ReviewThemes (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    ReviewId UNIQUEIDENTIFIER NOT NULL,
    Theme NVARCHAR(200) NOT NULL,
    CreatedAt DATETIME2 NOT NULL,
    FOREIGN KEY (ReviewId) REFERENCES Reviews(Id)
);
```

### Favorites Table
```sql
CREATE TABLE Favorites (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    ContentId UNIQUEIDENTIFIER NOT NULL,
    ContentType VARCHAR(20) NOT NULL,
    AddedDate DATETIME2 NOT NULL,
    FavoriteCategory NVARCHAR(100),
    RewatchCount INT NOT NULL DEFAULT 0,
    EmotionalSignificance NVARCHAR(1000),
    CreatedAt DATETIME2 NOT NULL,
    UpdatedAt DATETIME2 NOT NULL,
    FOREIGN KEY (UserId) REFERENCES Users(Id),
    UNIQUE (UserId, ContentId, ContentType)
);

CREATE INDEX IX_Favorites_UserId ON Favorites(UserId);
CREATE INDEX IX_Favorites_Category ON Favorites(FavoriteCategory);
```

## Business Logic

### Rating Service
- Validate rating value is within scale
- Support multiple rating scales (5-star, 10-point, etc.)
- Allow rating updates (rewatch ratings)
- Calculate average ratings per user
- Track rating evolution for TV shows
- Prevent rating before watching (optional enforcement)

### Review Service
- Validate review text length (min 50 chars, max 10000 chars)
- Detect and warn about spoilers
- Extract themes from review text (optional AI integration)
- Support rich text formatting
- Allow review editing within time window
- Link reviews to ratings automatically

### Favorite Service
- Prevent duplicate favorites
- Track rewatch count for favorites
- Support favorite categories (comfort watch, all-time best, etc.)
- Generate favorite-based recommendations
- Allow favorite ranking within categories

### Event Handlers
- TasteProfileBuilder: Updates user preferences based on ratings
- RecommendationEngine: Refines suggestions based on high ratings
- StatisticsCalculator: Updates rating statistics
- ReviewIndexer: Indexes reviews for search
- SocialSharing: Prepares reviews for sharing

## Integration Points
- Recommendation engine for taste profiling
- Search service for review indexing
- Social features for review sharing
- Analytics for rating trends

## Performance Considerations
- Index ratings by value for "highest rated" queries
- Full-text index on reviews for searching
- Cache user's average rating
- Optimize queries for favorites list

## Security
- Users can only create/edit/delete their own ratings and reviews
- Sanitize review text for XSS prevention
- Rate limit review submissions
- Validate spoiler flag accuracy (optional AI check)
- Encrypt sensitive emotional significance data
