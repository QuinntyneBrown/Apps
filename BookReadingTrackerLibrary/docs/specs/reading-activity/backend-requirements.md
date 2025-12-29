# Backend Requirements - Reading Activity

## API Endpoints

### Reading Sessions
- **POST /api/books/{id}/reading/start** - Start reading a book
  - Request: ReadingStartDTO (startDate, startingPage, readingGoalDate, context, device)
  - Response: ReadingSessionDTO
  - Events: ReadingStarted

- **PUT /api/reading-sessions/{id}/progress** - Update reading progress
  - Request: ProgressUpdateDTO (currentPage, updateTimestamp)
  - Response: ReadingSessionDTO with updated progress
  - Events: ReadingProgressUpdated

- **PUT /api/reading-sessions/{id}/complete** - Mark book as completed
  - Request: CompletionDTO (completionDate, totalReadingTime, rating, wouldReread, emotionalImpact)
  - Response: ReadingSessionDTO
  - Events: ReadingCompleted

- **PUT /api/reading-sessions/{id}/abandon** - Mark book as abandoned
  - Request: AbandonDTO (abandonDate, abandonReason, wouldRetryLater)
  - Response: ReadingSessionDTO
  - Events: ReadingAbandoned

- **POST /api/books/{id}/reread** - Start rereading a book
  - Request: RereadDTO (rereadReason)
  - Response: ReadingSessionDTO
  - Events: BookReread

- **GET /api/reading-sessions** - Get all reading sessions
  - Query params: status, page, size
  - Response: PagedResponse<ReadingSessionDTO>

- **GET /api/reading-sessions/currently-reading** - Get currently reading books
  - Response: List<ReadingSessionDTO>

### Statistics
- **GET /api/reading/statistics** - Get reading statistics
  - Response: ReadingStatisticsDTO (totalBooksRead, totalPagesRead, avgReadingSpeed, readingTime)

- **GET /api/reading/streak** - Get reading streak information
  - Response: ReadingStreakDTO (currentStreak, longestStreak, lastReadDate)

## Domain Models

### ReadingSession
```csharp
public class ReadingSession
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public DateTime StartDate { get; set; }
    public int StartingPage { get; set; }
    public DateTime? ReadingGoalDate { get; set; }
    public ReadingContext Context { get; set; }
    public string Device { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public decimal PercentageComplete { get; set; }
    public decimal ReadingPace { get; set; } // pages per day
    public DateTime? EstimatedCompletionDate { get; set; }
    public DateTime? CompletionDate { get; set; }
    public TimeSpan? TotalReadingTime { get; set; }
    public int? Rating { get; set; }
    public bool? WouldReread { get; set; }
    public string EmotionalImpact { get; set; }
    public DateTime? AbandonDate { get; set; }
    public string AbandonReason { get; set; }
    public bool WouldRetryLater { get; set; }
    public int RereadCount { get; set; }
    public ReadingStatus Status { get; set; }
    public List<ProgressUpdate> ProgressUpdates { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public enum ReadingContext
{
    Leisure,
    Study,
    BookClub,
    Work
}

public enum ReadingStatus
{
    CurrentlyReading,
    Completed,
    Abandoned
}
```

### ProgressUpdate
```csharp
public class ProgressUpdate
{
    public Guid Id { get; set; }
    public Guid ReadingSessionId { get; set; }
    public int CurrentPage { get; set; }
    public decimal PercentageComplete { get; set; }
    public DateTime UpdateTimestamp { get; set; }
    public int PagesReadSinceLastUpdate { get; set; }
    public decimal ReadingPace { get; set; }
}
```

### ReadingStreak
```csharp
public class ReadingStreak
{
    public Guid UserId { get; set; }
    public int CurrentStreak { get; set; }
    public int LongestStreak { get; set; }
    public DateTime LastReadDate { get; set; }
    public StreakStatus Status { get; set; }
}

public enum StreakStatus
{
    Active,
    Broken
}
```

## Domain Events

### ReadingStarted
```csharp
public class ReadingStartedEvent : DomainEvent
{
    public Guid ReadingSessionId { get; set; }
    public Guid BookId { get; set; }
    public DateTime StartDate { get; set; }
    public int StartingPage { get; set; }
    public DateTime? ReadingGoalDate { get; set; }
    public ReadingContext Context { get; set; }
    public string Device { get; set; }
}
```

### ReadingProgressUpdated
```csharp
public class ReadingProgressUpdatedEvent : DomainEvent
{
    public Guid ProgressId { get; set; }
    public Guid BookId { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public decimal PercentageComplete { get; set; }
    public DateTime UpdateTimestamp { get; set; }
    public decimal ReadingPace { get; set; }
    public int PagesReadSinceLastUpdate { get; set; }
}
```

### ReadingCompleted
```csharp
public class ReadingCompletedEvent : DomainEvent
{
    public Guid CompletionId { get; set; }
    public Guid BookId { get; set; }
    public DateTime CompletionDate { get; set; }
    public TimeSpan TotalReadingTime { get; set; }
    public DateTime StartDate { get; set; }
    public int? Rating { get; set; }
    public bool WouldReread { get; set; }
    public string EmotionalImpact { get; set; }
}
```

## Business Rules

1. **BR-RA-001**: Only one active reading session per book at a time
2. **BR-RA-002**: Current page cannot exceed total pages
3. **BR-RA-003**: Reading pace calculated as total pages read / days elapsed
4. **BR-RA-004**: Reading streak increments only with daily reading activity
5. **BR-RA-005**: Completion date cannot be before start date
6. **BR-RA-006**: Progress updates must be chronological

## Database Schema

### ReadingSessions Table
- Id (PK, GUID)
- BookId (FK, GUID)
- StartDate (DATETIME2, NOT NULL)
- StartingPage (INT, DEFAULT 0)
- ReadingGoalDate (DATETIME2, NULL)
- Context (INT)
- Device (NVARCHAR(100))
- CurrentPage (INT, DEFAULT 0)
- TotalPages (INT)
- PercentageComplete (DECIMAL(5,2))
- ReadingPace (DECIMAL(10,2))
- EstimatedCompletionDate (DATETIME2)
- CompletionDate (DATETIME2, NULL)
- TotalReadingTime (TIME, NULL)
- Rating (INT, NULL)
- WouldReread (BIT, NULL)
- EmotionalImpact (NVARCHAR(MAX))
- AbandonDate (DATETIME2, NULL)
- AbandonReason (NVARCHAR(500))
- WouldRetryLater (BIT, DEFAULT 0)
- RereadCount (INT, DEFAULT 0)
- Status (INT, NOT NULL)
- CreatedAt (DATETIME2, NOT NULL)
- UpdatedAt (DATETIME2, NOT NULL)

### ProgressUpdates Table
- Id (PK, GUID)
- ReadingSessionId (FK, GUID)
- CurrentPage (INT, NOT NULL)
- PercentageComplete (DECIMAL(5,2))
- UpdateTimestamp (DATETIME2, NOT NULL)
- PagesReadSinceLastUpdate (INT)
- ReadingPace (DECIMAL(10,2))

### ReadingStreaks Table
- UserId (PK, GUID)
- CurrentStreak (INT, DEFAULT 0)
- LongestStreak (INT, DEFAULT 0)
- LastReadDate (DATETIME2)
- Status (INT, NOT NULL)
