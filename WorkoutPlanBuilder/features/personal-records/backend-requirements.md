# Personal Records - Backend Requirements

## Overview
Automatically detect, record, and celebrate personal bests across all exercises and metrics.

## Domain Model

### PersonalRecord (Aggregate Root)
```csharp
public class PersonalRecord : AggregateRoot
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public Guid ExerciseId { get; private set; }
    public RecordType Type { get; private set; }
    public decimal Value { get; private set; }
    public int? Reps { get; private set; }
    public decimal? Weight { get; private set; }
    public DateTime AchievedDate { get; private set; }
    public Guid WorkoutSessionId { get; private set; }
    public string Notes { get; private set; }
    public decimal? PreviousValue { get; private set; }

    public static PersonalRecord Create(Guid userId, Guid exerciseId, RecordType type,
        decimal value, Guid sessionId, int? reps = null, decimal? weight = null)
    {
        return new PersonalRecord
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            ExerciseId = exerciseId,
            Type = type,
            Value = value,
            Reps = reps,
            Weight = weight,
            AchievedDate = DateTime.UtcNow,
            WorkoutSessionId = sessionId
        };
    }
}
```

### Enums
```csharp
public enum RecordType
{
    MaxWeight = 0,        // Heaviest weight for any rep
    MaxReps = 1,          // Most reps at any weight
    MaxVolume = 2,        // Highest single set volume (weight × reps)
    Best1RM = 3,          // Calculated 1-rep max
    FastestTime = 4       // For timed/cardio exercises
}
```

## Commands

### RecordPersonalRecordCommand
```csharp
public class RecordPersonalRecordCommand : IRequest<Guid>
{
    public Guid ExerciseId { get; set; }
    public RecordType Type { get; set; }
    public decimal Value { get; set; }
    public int? Reps { get; set; }
    public decimal? Weight { get; set; }
    public Guid WorkoutSessionId { get; set; }
    public string Notes { get; set; }
}
```

## Queries

### GetUserPersonalRecordsQuery
```csharp
public class GetUserPersonalRecordsQuery : IRequest<List<PersonalRecordDto>>
{
    public Guid? ExerciseId { get; set; }
    public RecordType? Type { get; set; }
    public DateTime? FromDate { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}
```

### GetExerciseRecordHistoryQuery
```csharp
public class GetExerciseRecordHistoryQuery : IRequest<List<PersonalRecordDto>>
{
    public Guid ExerciseId { get; set; }
    public RecordType Type { get; set; }
}
```

## Business Logic

### PR Detection Service
```csharp
public class PersonalRecordService : IPersonalRecordService
{
    public async Task CheckAndRecordPRsAsync(Guid sessionId, CancellationToken cancellationToken)
    {
        // Get all sets from session
        var session = await _sessionRepo.GetByIdWithSetsAsync(sessionId);

        // Group by exercise
        var exerciseSets = session.Sets.GroupBy(s => s.ExerciseId);

        foreach (var group in exerciseSets)
        {
            // Check max weight PR
            var maxWeight = group.Where(s => !s.IsWarmup).Max(s => s.Weight);
            await CheckPR(session.UserId, group.Key, RecordType.MaxWeight, maxWeight, sessionId);

            // Check max reps PR
            var maxReps = group.Where(s => !s.IsWarmup).Max(s => s.Reps);
            await CheckPR(session.UserId, group.Key, RecordType.MaxReps, maxReps, sessionId);

            // Check max volume PR
            var maxVolume = group.Where(s => !s.IsWarmup).Max(s => s.Reps * s.Weight);
            await CheckPR(session.UserId, group.Key, RecordType.MaxVolume, maxVolume, sessionId);

            // Calculate and check 1RM PR
            var best1RM = CalculateBest1RM(group);
            await CheckPR(session.UserId, group.Key, RecordType.Best1RM, best1RM, sessionId);
        }
    }

    private decimal CalculateBest1RM(IEnumerable<ExerciseSet> sets)
    {
        // Epley formula: weight × (1 + (reps / 30))
        return sets.Max(s => s.Weight * (1 + (s.Reps / 30.0m)));
    }

    private async Task CheckPR(Guid userId, Guid exerciseId, RecordType type,
        decimal value, Guid sessionId)
    {
        var currentPR = await _prRepo.GetCurrentPRAsync(userId, exerciseId, type);

        if (currentPR == null || value > currentPR.Value)
        {
            var pr = PersonalRecord.Create(userId, exerciseId, type, value, sessionId);
            pr.PreviousValue = currentPR?.Value;
            await _prRepo.AddAsync(pr);
        }
    }
}
```

## Repository
```csharp
public interface IPersonalRecordRepository : IRepository<PersonalRecord>
{
    Task<PersonalRecord> GetCurrentPRAsync(Guid userId, Guid exerciseId, RecordType type);
    Task<List<PersonalRecord>> GetUserRecordsAsync(Guid userId, Guid? exerciseId, RecordType? type);
    Task<List<PersonalRecord>> GetExerciseHistoryAsync(Guid userId, Guid exerciseId, RecordType type);
    Task<List<PersonalRecord>> GetRecentPRsAsync(Guid userId, int days = 30);
}
```

## API Endpoints
```csharp
[Route("api/personal-records")]
public class PersonalRecordsController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<PersonalRecordDto>>> GetUserRecords(
        [FromQuery] Guid? exerciseId,
        [FromQuery] RecordType? type,
        [FromQuery] DateTime? fromDate)

    [HttpGet("exercise/{exerciseId}")]
    public async Task<ActionResult<List<PersonalRecordDto>>> GetExerciseRecords(Guid exerciseId)

    [HttpGet("recent")]
    public async Task<ActionResult<List<PersonalRecordDto>>> GetRecentRecords([FromQuery] int days = 30)

    [HttpGet("history/{exerciseId}")]
    public async Task<ActionResult<List<PersonalRecordDto>>> GetHistory(
        Guid exerciseId,
        [FromQuery] RecordType type)

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateRecord([FromBody] RecordPersonalRecordCommand command)
}
```

## Database Schema
```sql
CREATE TABLE PersonalRecords (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    ExerciseId UNIQUEIDENTIFIER NOT NULL,
    Type INT NOT NULL,
    Value DECIMAL(10,2) NOT NULL,
    Reps INT,
    Weight DECIMAL(10,2),
    AchievedDate DATETIME2 NOT NULL,
    WorkoutSessionId UNIQUEIDENTIFIER NOT NULL,
    Notes NVARCHAR(500),
    PreviousValue DECIMAL(10,2),

    FOREIGN KEY (ExerciseId) REFERENCES Exercises(Id),
    FOREIGN KEY (WorkoutSessionId) REFERENCES WorkoutSessions(Id),
    INDEX IX_PersonalRecords_UserId (UserId),
    INDEX IX_PersonalRecords_ExerciseId (ExerciseId),
    INDEX IX_PersonalRecords_Type (Type),
    INDEX IX_PersonalRecords_AchievedDate (AchievedDate),
    UNIQUE INDEX UX_PersonalRecords_Current (UserId, ExerciseId, Type)
        WHERE PreviousValue IS NULL
);
```

## Business Rules
1. Only one current PR per user/exercise/type combination
2. Warmup sets excluded from PR calculations
3. PRs automatically detected after session completion
4. Historical PRs maintained for trend analysis
5. Manual PR entry allowed for offline workouts
6. PRs can be disputed/corrected within 24 hours
