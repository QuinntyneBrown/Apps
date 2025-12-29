# Workout Sessions - Backend Requirements

## Overview
The Workout Sessions backend manages the execution and logging of individual workout sessions, including real-time tracking of sets, reps, weights, and session completion.

## Domain Model

### Entities

#### WorkoutSession (Aggregate Root)
```csharp
public class WorkoutSession : AggregateRoot
{
    public Guid Id { get; private set; }
    public Guid? WorkoutPlanId { get; private set; }
    public Guid? WorkoutId { get; private set; }
    public Guid UserId { get; private set; }
    public DateTime StartTime { get; private set; }
    public DateTime? EndTime { get; private set; }
    public WorkoutSessionStatus Status { get; private set; }
    public string Notes { get; private set; }
    public TimeSpan ActualDuration { get; private set; }

    private List<ExerciseSet> _sets = new();
    public IReadOnlyList<ExerciseSet> Sets => _sets.AsReadOnly();

    public static WorkoutSession Start(Guid userId, Guid? workoutPlanId, Guid? workoutId)
    {
        var session = new WorkoutSession
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            WorkoutPlanId = workoutPlanId,
            WorkoutId = workoutId,
            StartTime = DateTime.UtcNow,
            Status = WorkoutSessionStatus.InProgress
        };

        session.AddDomainEvent(new WorkoutSessionStartedEvent
        {
            WorkoutSessionId = session.Id,
            UserId = userId,
            WorkoutPlanId = workoutPlanId,
            StartTime = session.StartTime
        });

        return session;
    }

    public void LogSet(ExerciseSet set)
    {
        _sets.Add(set);

        AddDomainEvent(new ExercisePerformedEvent
        {
            ExerciseId = set.ExerciseId,
            UserId = UserId,
            WorkoutSessionId = Id,
            TotalSets = _sets.Count(s => s.ExerciseId == set.ExerciseId),
            MaxWeight = _sets.Where(s => s.ExerciseId == set.ExerciseId).Max(s => s.Weight),
            TotalVolume = _sets.Where(s => s.ExerciseId == set.ExerciseId).Sum(s => s.Reps * s.Weight),
            PerformedDate = DateTime.UtcNow
        });
    }

    public void Complete(string notes = null)
    {
        if (Status != WorkoutSessionStatus.InProgress)
            throw new InvalidOperationException("Only in-progress sessions can be completed");

        Status = WorkoutSessionStatus.Completed;
        EndTime = DateTime.UtcNow;
        ActualDuration = EndTime.Value - StartTime;
        Notes = notes;

        var totalVolume = _sets.Sum(s => s.Reps * s.Weight);
        var totalSets = _sets.Count;
        var exercisesCompleted = _sets.Select(s => s.ExerciseId).Distinct().Count();

        AddDomainEvent(new WorkoutSessionCompletedEvent
        {
            WorkoutSessionId = Id,
            UserId = UserId,
            CompletedTime = EndTime.Value,
            Duration = ActualDuration,
            ExercisesCompleted = exercisesCompleted,
            TotalSets = totalSets,
            TotalVolume = totalVolume
        });
    }

    public void Skip(string reason = null)
    {
        if (Status != WorkoutSessionStatus.InProgress)
            throw new InvalidOperationException("Only in-progress sessions can be skipped");

        Status = WorkoutSessionStatus.Skipped;
        EndTime = DateTime.UtcNow;
        Notes = reason;

        AddDomainEvent(new WorkoutSessionSkippedEvent
        {
            WorkoutSessionId = Id,
            UserId = UserId,
            SkippedDate = DateTime.UtcNow,
            Reason = reason
        });
    }
}
```

#### ExerciseSet (Entity)
```csharp
public class ExerciseSet : Entity
{
    public Guid Id { get; private set; }
    public Guid WorkoutSessionId { get; private set; }
    public Guid ExerciseId { get; private set; }
    public int SetNumber { get; private set; }
    public int Reps { get; private set; }
    public decimal Weight { get; private set; }
    public WeightUnit WeightUnit { get; private set; }
    public bool IsWarmup { get; private set; }
    public DateTime CompletedAt { get; private set; }
    public string Notes { get; private set; }

    public static ExerciseSet Create(
        Guid sessionId,
        Guid exerciseId,
        int setNumber,
        int reps,
        decimal weight,
        WeightUnit unit,
        bool isWarmup = false)
    {
        return new ExerciseSet
        {
            Id = Guid.NewGuid(),
            WorkoutSessionId = sessionId,
            ExerciseId = exerciseId,
            SetNumber = setNumber,
            Reps = reps,
            Weight = weight,
            WeightUnit = unit,
            IsWarmup = isWarmup,
            CompletedAt = DateTime.UtcNow
        };
    }

    public decimal CalculateVolume()
    {
        return Reps * Weight;
    }
}
```

### Enums

```csharp
public enum WorkoutSessionStatus
{
    InProgress = 0,
    Completed = 1,
    Skipped = 2
}
```

## Commands

### StartWorkoutSessionCommand
```csharp
public class StartWorkoutSessionCommand : IRequest<Guid>
{
    public Guid? WorkoutPlanId { get; set; }
    public Guid? WorkoutId { get; set; }
}
```

### LogExerciseSetCommand
```csharp
public class LogExerciseSetCommand : IRequest<Guid>
{
    public Guid WorkoutSessionId { get; set; }
    public Guid ExerciseId { get; set; }
    public int SetNumber { get; set; }
    public int Reps { get; set; }
    public decimal Weight { get; set; }
    public WeightUnit WeightUnit { get; set; }
    public bool IsWarmup { get; set; }
    public string Notes { get; set; }
}
```

### CompleteWorkoutSessionCommand
```csharp
public class CompleteWorkoutSessionCommand : IRequest<Unit>
{
    public Guid WorkoutSessionId { get; set; }
    public string Notes { get; set; }
}
```

### SkipWorkoutSessionCommand
```csharp
public class SkipWorkoutSessionCommand : IRequest<Unit>
{
    public Guid WorkoutSessionId { get; set; }
    public string Reason { get; set; }
}
```

## Queries

### GetWorkoutSessionByIdQuery
```csharp
public class GetWorkoutSessionByIdQuery : IRequest<WorkoutSessionDetailDto>
{
    public Guid SessionId { get; set; }
}

public class WorkoutSessionDetailDto
{
    public Guid Id { get; set; }
    public Guid? WorkoutPlanId { get; set; }
    public string WorkoutPlanName { get; set; }
    public Guid? WorkoutId { get; set; }
    public string WorkoutName { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public string Status { get; set; }
    public string Notes { get; set; }
    public TimeSpan ActualDuration { get; set; }
    public List<ExerciseSetDto> Sets { get; set; }
    public decimal TotalVolume { get; set; }
    public int ExercisesCompleted { get; set; }
}
```

### GetUserSessionsQuery
```csharp
public class GetUserSessionsQuery : IRequest<List<WorkoutSessionSummaryDto>>
{
    public WorkoutSessionStatus? Status { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}
```

### GetActiveSessionQuery
```csharp
public class GetActiveSessionQuery : IRequest<WorkoutSessionDetailDto>
{
}
```

## Repository Interface

```csharp
public interface IWorkoutSessionRepository : IRepository<WorkoutSession>
{
    Task<WorkoutSession> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<WorkoutSession> GetByIdWithSetsAsync(Guid id, CancellationToken cancellationToken = default);
    Task<WorkoutSession> GetActiveSessionAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<List<WorkoutSession>> GetUserSessionsAsync(Guid userId, WorkoutSessionStatus? status, DateTime? fromDate, DateTime? toDate, CancellationToken cancellationToken = default);
    Task<int> GetCompletedSessionCountAsync(Guid userId, Guid? planId, CancellationToken cancellationToken = default);
    Task<bool> UserOwnsSessionAsync(Guid userId, Guid sessionId, CancellationToken cancellationToken = default);
}
```

## Event Handlers

### WorkoutSessionCompletedEventHandler
```csharp
public class WorkoutSessionCompletedEventHandler : INotificationHandler<WorkoutSessionCompletedEvent>
{
    private readonly IPersonalRecordService _prService;
    private readonly INotificationService _notificationService;
    private readonly IAnalyticsService _analyticsService;

    public async Task Handle(WorkoutSessionCompletedEvent notification, CancellationToken cancellationToken)
    {
        // Check for new personal records
        await _prService.CheckAndRecordPRsAsync(notification.WorkoutSessionId, cancellationToken);

        // Send completion notification
        await _notificationService.SendAsync(
            notification.UserId,
            "Workout Completed!",
            $"Great job! {notification.ExercisesCompleted} exercises, {notification.TotalSets} sets, {notification.TotalVolume:N0} lbs total volume."
        );

        // Track analytics
        await _analyticsService.TrackEventAsync("session_completed", notification.UserId);
    }
}
```

## Validation

### LogExerciseSetCommandValidator
```csharp
public class LogExerciseSetCommandValidator : AbstractValidator<LogExerciseSetCommand>
{
    public LogExerciseSetCommandValidator()
    {
        RuleFor(x => x.Reps)
            .GreaterThan(0).WithMessage("Reps must be greater than 0")
            .LessThanOrEqualTo(1000).WithMessage("Reps cannot exceed 1000");

        RuleFor(x => x.Weight)
            .GreaterThanOrEqualTo(0).WithMessage("Weight cannot be negative")
            .LessThanOrEqualTo(10000).WithMessage("Weight cannot exceed 10000");

        RuleFor(x => x.SetNumber)
            .GreaterThan(0).WithMessage("Set number must be greater than 0");
    }
}
```

## API Endpoints

### WorkoutSessionsController
```csharp
[ApiController]
[Route("api/workout-sessions")]
[Authorize]
public class WorkoutSessionsController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<WorkoutSessionSummaryDto>>> GetUserSessions(
        [FromQuery] WorkoutSessionStatus? status,
        [FromQuery] DateTime? fromDate,
        [FromQuery] DateTime? toDate,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 20)

    [HttpGet("active")]
    public async Task<ActionResult<WorkoutSessionDetailDto>> GetActiveSession()

    [HttpGet("{id}")]
    public async Task<ActionResult<WorkoutSessionDetailDto>> GetById(Guid id)

    [HttpPost]
    public async Task<ActionResult<Guid>> StartSession([FromBody] StartWorkoutSessionCommand command)

    [HttpPost("{id}/sets")]
    public async Task<ActionResult<Guid>> LogSet(Guid id, [FromBody] LogExerciseSetCommand command)

    [HttpPost("{id}/complete")]
    public async Task<ActionResult> Complete(Guid id, [FromBody] CompleteWorkoutSessionCommand command)

    [HttpPost("{id}/skip")]
    public async Task<ActionResult> Skip(Guid id, [FromBody] SkipWorkoutSessionCommand command)

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
}
```

## Database Schema

```sql
CREATE TABLE WorkoutSessions (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    WorkoutPlanId UNIQUEIDENTIFIER,
    WorkoutId UNIQUEIDENTIFIER,
    UserId UNIQUEIDENTIFIER NOT NULL,
    StartTime DATETIME2 NOT NULL,
    EndTime DATETIME2,
    Status INT NOT NULL DEFAULT 0,
    Notes NVARCHAR(2000),
    ActualDuration TIME,

    FOREIGN KEY (WorkoutPlanId) REFERENCES WorkoutPlans(Id),
    FOREIGN KEY (WorkoutId) REFERENCES Workouts(Id),
    INDEX IX_WorkoutSessions_UserId (UserId),
    INDEX IX_WorkoutSessions_Status (Status),
    INDEX IX_WorkoutSessions_StartTime (StartTime),
    INDEX IX_WorkoutSessions_WorkoutPlanId (WorkoutPlanId)
);

CREATE TABLE ExerciseSets (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    WorkoutSessionId UNIQUEIDENTIFIER NOT NULL,
    ExerciseId UNIQUEIDENTIFIER NOT NULL,
    SetNumber INT NOT NULL,
    Reps INT NOT NULL,
    Weight DECIMAL(10,2) NOT NULL,
    WeightUnit INT NOT NULL,
    IsWarmup BIT NOT NULL DEFAULT 0,
    CompletedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    Notes NVARCHAR(500),

    FOREIGN KEY (WorkoutSessionId) REFERENCES WorkoutSessions(Id) ON DELETE CASCADE,
    FOREIGN KEY (ExerciseId) REFERENCES Exercises(Id),
    INDEX IX_ExerciseSets_WorkoutSessionId (WorkoutSessionId),
    INDEX IX_ExerciseSets_ExerciseId (ExerciseId),
    INDEX IX_ExerciseSets_CompletedAt (CompletedAt)
);
```

## Business Rules

1. Users can only have one active session at a time
2. Sets must be logged to an in-progress session
3. Sessions can only be completed or skipped if in progress
4. Completed sessions cannot be modified
5. Set numbers should be sequential for each exercise
6. Weight and reps must be positive values
7. Warmup sets are excluded from PR calculations
8. Session duration is calculated from start to end time

## Performance Considerations

- Use eager loading for session with sets
- Index on UserId and StartTime for history queries
- Cache active session for quick access
- Use batch inserts for multiple sets
- Optimize volume calculations with database queries
- Implement real-time updates using SignalR

## Security

- Validate user ownership before allowing actions
- Prevent modification of other users' sessions
- Rate limit session creation
- Validate exercise IDs exist before logging sets
- Sanitize notes input
- Ensure proper transaction boundaries
