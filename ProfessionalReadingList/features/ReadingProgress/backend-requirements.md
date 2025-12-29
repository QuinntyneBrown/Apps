# Backend Requirements - Reading Progress

## Domain Events
- ReadingStarted
- ReadingProgressUpdated
- ReadingCompleted
- ReadingAbandoned

## Key API Endpoints
- POST /api/reading-sessions/start - Start reading session
- PUT /api/reading-sessions/{id}/progress - Update progress
- POST /api/reading-sessions/{id}/complete - Mark as completed
- POST /api/reading-sessions/{id}/abandon - Abandon reading
- GET /api/reading-materials/{id}/progress - Get progress details
- GET /api/analytics/reading-time - Get reading time stats

## Data Models
```csharp
public class ReadingSession : AggregateRoot
{
    public Guid Id { get; private set; }
    public Guid MaterialId { get; private set; }
    public Guid ReaderId { get; private set; }
    public DateTime StartedAt { get; private set; }
    public int ProgressPercentage { get; private set; }
    public TimeSpan TotalReadingTime { get; private set; }
    public ReadingStatus Status { get; private set; }
    public DateTime? CompletedAt { get; private set; }
}
```

## Business Rules
- One active session per material per user
- Progress must be 0-100%
- Completion requires 100% or explicit marking
- Reading time calculated from session timestamps
