# Backend Requirements - Streaming and Availability

## API Endpoints

### GET /api/availability/{contentId}
- Get streaming availability for content
- Response: List of platforms where content is available

### POST /api/subscriptions
- Add a streaming subscription
- Request Body: { platform, tier, monthlyCost, startDate }
- Response: Subscription with ID
- Publishes: StreamingSubscriptionUpdated event

### DELETE /api/subscriptions/{subscriptionId}
- Cancel a streaming subscription
- Publishes: StreamingSubscriptionUpdated event

### POST /api/watch-parties
- Schedule a watch party
- Request Body: { contentId, scheduledDateTime, participants, platform, host, discussionPlan }
- Response: WatchParty with ID
- Publishes: WatchPartyScheduled event

### GET /api/watch-parties
- Get scheduled watch parties
- Response: List of upcoming watch parties

### POST /api/notifications/new-episode-alerts
- Enable new episode notifications for a show
- Request Body: { showId, notificationMethod }
- Response: Alert subscription

## Domain Events

### ContentAvailabilityChanged
```csharp
public class ContentAvailabilityChanged : DomainEvent
{
    public Guid ContentId { get; set; }
    public string Platform { get; set; }
    public bool IsAvailable { get; set; }
    public DateTime ChangeDate { get; set; }
    public bool SubscriptionRequired { get; set; }
    public string AvailabilityWindow { get; set; }
    public List<string> RegionalRestrictions { get; set; }
}
```

### StreamingSubscriptionUpdated
```csharp
public class StreamingSubscriptionUpdated : DomainEvent
{
    public Guid SubscriptionId { get; set; }
    public string PlatformName { get; set; }
    public string SubscriptionStatus { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string SubscriptionTier { get; set; }
    public decimal MonthlyCost { get; set; }
    public int ContentAccessCount { get; set; }
}
```

### WatchPartyScheduled
```csharp
public class WatchPartyScheduled : DomainEvent
{
    public Guid PartyId { get; set; }
    public Guid ContentId { get; set; }
    public DateTime ScheduledDateTime { get; set; }
    public List<string> Participants { get; set; }
    public string Platform { get; set; }
    public string Host { get; set; }
    public string ViewingContext { get; set; }
    public string DiscussionPlan { get; set; }
}
```

### NewEpisodeNotified
```csharp
public class NewEpisodeNotified : DomainEvent
{
    public Guid NotificationId { get; set; }
    public Guid ShowId { get; set; }
    public int SeasonNumber { get; set; }
    public int EpisodeNumber { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string Platform { get; set; }
    public DateTime NotificationDeliveryTime { get; set; }
    public string UserResponse { get; set; }
}
```

## Database Schema

### StreamingAvailability Table
```sql
CREATE TABLE StreamingAvailability (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    ContentId UNIQUEIDENTIFIER NOT NULL,
    Platform NVARCHAR(100) NOT NULL,
    IsAvailable BIT NOT NULL,
    SubscriptionRequired BIT NOT NULL,
    AvailabilityStart DATETIME2,
    AvailabilityEnd DATETIME2,
    RegionalRestrictions NVARCHAR(500),
    LastChecked DATETIME2 NOT NULL,
    CreatedAt DATETIME2 NOT NULL,
    UpdatedAt DATETIME2 NOT NULL
);

CREATE INDEX IX_StreamingAvailability_ContentId ON StreamingAvailability(ContentId);
```

### UserSubscriptions Table
```sql
CREATE TABLE UserSubscriptions (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    Platform NVARCHAR(100) NOT NULL,
    SubscriptionTier NVARCHAR(50),
    MonthlyCost DECIMAL(10,2) NOT NULL,
    StartDate DATETIME2 NOT NULL,
    EndDate DATETIME2,
    Status VARCHAR(20) NOT NULL, -- 'Active', 'Cancelled', 'Paused'
    CreatedAt DATETIME2 NOT NULL,
    UpdatedAt DATETIME2 NOT NULL,
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);
```

### WatchParties Table
```sql
CREATE TABLE WatchParties (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    ContentId UNIQUEIDENTIFIER NOT NULL,
    ScheduledDateTime DATETIME2 NOT NULL,
    Platform NVARCHAR(100),
    Host NVARCHAR(200) NOT NULL,
    ViewingContext NVARCHAR(200),
    DiscussionPlan NVARCHAR(1000),
    Status VARCHAR(20) NOT NULL DEFAULT 'Scheduled',
    CreatedAt DATETIME2 NOT NULL,
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);
```

### WatchPartyParticipants Table
```sql
CREATE TABLE WatchPartyParticipants (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    WatchPartyId UNIQUEIDENTIFIER NOT NULL,
    ParticipantName NVARCHAR(200) NOT NULL,
    ParticipantEmail NVARCHAR(200),
    RSVPStatus VARCHAR(20),
    CreatedAt DATETIME2 NOT NULL,
    FOREIGN KEY (WatchPartyId) REFERENCES WatchParties(Id)
);
```

### NewEpisodeAlerts Table
```sql
CREATE TABLE NewEpisodeAlerts (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    ShowId UNIQUEIDENTIFIER NOT NULL,
    NotificationMethod VARCHAR(50) NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME2 NOT NULL,
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);
```

## Business Logic

### Availability Monitor
- Check streaming platforms twice daily for content availability
- Detect when watchlisted content becomes available
- Track availability windows and expiration dates
- Monitor regional restrictions
- Integrate with platform APIs (JustWatch, TMDB, etc.)

### Subscription Manager
- Track user's active subscriptions
- Calculate content access count per subscription
- Compute ROI (cost vs usage)
- Suggest subscription optimization
- Alert when unused subscriptions detected

### Watch Party Coordinator
- Schedule watch parties with calendar integration
- Send invitations to participants
- Send reminders before watch party
- Track RSVP status
- Record watch party as viewing event

### Episode Alert Service
- Monitor new episode releases for followed shows
- Send notifications within 24 hours of release
- Support multiple notification methods (email, push, SMS)
- Track notification delivery and user response
- Batch notifications to prevent spam

## Integration Points
- JustWatch API for availability data
- Streaming platform APIs (Netflix, HBO, etc.)
- Calendar services (Google Calendar, iCal)
- Notification services (email, push, SMS)
- TMDB for episode release dates

## Performance Considerations
- Cache availability data (refresh every 12 hours)
- Batch availability checks for multiple content items
- Index subscriptions by user for fast access
- Queue watch party notifications asynchronously
