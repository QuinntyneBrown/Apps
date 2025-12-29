# Exercises - Backend Requirements

## Overview
The Exercises feature provides a comprehensive exercise library management system that allows users to browse, search, add custom exercises, track performance, and substitute exercises in workout plans. This backend handles exercise CRUD operations, performance tracking, and domain events for exercise-related activities.

## Domain Events

### ExerciseAddedToLibrary
Raised when a new exercise is added to the library (system or custom).

**Event Properties:**
```csharp
public class ExerciseAddedToLibraryEvent : DomainEvent
{
    public Guid ExerciseId { get; set; }
    public string ExerciseName { get; set; }
    public string Category { get; set; }
    public string MuscleGroup { get; set; }
    public bool IsCustom { get; set; }
    public Guid? CreatedByUserId { get; set; }
    public DateTime CreatedDate { get; set; }
}
```

**Event Handlers:**
- Update search index
- Send notification if custom exercise needs review
- Log library growth analytics
- Update exercise category counts
- Trigger recommendation engine update

### ExercisePerformed
Raised when an exercise is completed during a workout session.

**Event Properties:**
```csharp
public class ExercisePerformedEvent : DomainEvent
{
    public Guid ExerciseId { get; set; }
    public string ExerciseName { get; set; }
    public Guid UserId { get; set; }
    public Guid WorkoutSessionId { get; set; }
    public int TotalSets { get; set; }
    public int TotalReps { get; set; }
    public decimal MaxWeight { get; set; }
    public decimal TotalVolume { get; set; }
    public DateTime PerformedDate { get; set; }
}
```

**Event Handlers:**
- Update exercise performance history
- Check for new personal records
- Update exercise popularity metrics
- Calculate strength progression
- Update user statistics
- Trigger achievement notifications

### ExerciseSubstituted
Raised when an exercise is replaced with an alternative in a workout plan.

**Event Properties:**
```csharp
public class ExerciseSubstitutedEvent : DomainEvent
{
    public Guid WorkoutPlanId { get; set; }
    public Guid OriginalExerciseId { get; set; }
    public string OriginalExerciseName { get; set; }
    public Guid SubstituteExerciseId { get; set; }
    public string SubstituteExerciseName { get; set; }
    public Guid UserId { get; set; }
    public string Reason { get; set; } // Equipment, Injury, Preference
    public DateTime SubstitutedDate { get; set; }
}
```

**Event Handlers:**
- Log substitution pattern for recommendations
- Update exercise relationship mapping
- Suggest better alternatives
- Track equipment availability patterns
- Update workout plan modification event

## Domain Model

### Entities

#### Exercise (Aggregate Root)
```csharp
public class Exercise : AggregateRoot
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public ExerciseCategory Category { get; private set; }
    public MuscleGroup PrimaryMuscleGroup { get; private set; }
    public List<MuscleGroup> SecondaryMuscleGroups { get; private set; }
    public EquipmentType Equipment { get; private set; }
    public DifficultyLevel Difficulty { get; private set; }
    public string Instructions { get; private set; }
    public string VideoUrl { get; private set; }
    public string ImageUrl { get; private set; }
    public bool IsCustom { get; private set; }
    public Guid? CreatedByUserId { get; private set; }
    public DateTime CreatedDate { get; private set; }
    public bool IsApproved { get; private set; }
    public int PopularityScore { get; private set; }

    // Methods
    public void UpdateDetails(string name, string description, string instructions);
    public void SetMediaUrls(string videoUrl, string imageUrl);
    public void Approve();
    public void IncrementPopularity();
}
```

#### ExercisePerformance (Entity)
```csharp
public class ExercisePerformance : Entity
{
    public Guid Id { get; private set; }
    public Guid ExerciseId { get; private set; }
    public Guid UserId { get; private set; }
    public Guid WorkoutSessionId { get; private set; }
    public DateTime PerformedDate { get; private set; }
    public int TotalSets { get; private set; }
    public int TotalReps { get; private set; }
    public decimal MaxWeight { get; private set; }
    public WeightUnit WeightUnit { get; private set; }
    public decimal TotalVolume { get; private set; }
    public TimeSpan? Duration { get; private set; }
    public string Notes { get; private set; }

    private readonly List<SetPerformance> _sets = new();
    public IReadOnlyCollection<SetPerformance> Sets => _sets.AsReadOnly();

    public decimal CalculateTotalVolume();
    public decimal EstimateOneRepMax();
}
```

#### SetPerformance (Value Object)
```csharp
public class SetPerformance : ValueObject
{
    public int SetNumber { get; private set; }
    public int Reps { get; private set; }
    public decimal Weight { get; private set; }
    public bool IsWarmup { get; private set; }
    public bool IsDropSet { get; private set; }
    public int RestSeconds { get; private set; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return SetNumber;
        yield return Reps;
        yield return Weight;
        yield return IsWarmup;
    }
}
```

#### ExerciseSubstitution (Entity)
```csharp
public class ExerciseSubstitution : Entity
{
    public Guid Id { get; private set; }
    public Guid OriginalExerciseId { get; private set; }
    public Guid SubstituteExerciseId { get; private set; }
    public Guid UserId { get; private set; }
    public Guid WorkoutPlanId { get; private set; }
    public SubstitutionReason Reason { get; private set; }
    public DateTime SubstitutedDate { get; private set; }
    public string Notes { get; private set; }
}
```

### Enumerations

```csharp
public enum ExerciseCategory
{
    Strength = 0,
    Cardio = 1,
    Flexibility = 2,
    Plyometric = 3,
    Olympic = 4,
    Strongman = 5
}

public enum MuscleGroup
{
    Chest = 0,
    Back = 1,
    Shoulders = 2,
    Biceps = 3,
    Triceps = 4,
    Forearms = 5,
    Abs = 6,
    Obliques = 7,
    Quads = 8,
    Hamstrings = 9,
    Glutes = 10,
    Calves = 11,
    FullBody = 12
}

public enum EquipmentType
{
    Barbell = 0,
    Dumbbell = 1,
    Kettlebell = 2,
    Machine = 3,
    Cable = 4,
    Bodyweight = 5,
    Bands = 6,
    Other = 7
}

public enum SubstitutionReason
{
    EquipmentUnavailable = 0,
    Injury = 1,
    Preference = 2,
    ProgressionChange = 3
}
```

## Commands

### AddExerciseToLibraryCommand
```csharp
public class AddExerciseToLibraryCommand : IRequest<Guid>
{
    public Guid? UserId { get; set; } // Null for system exercises
    public string Name { get; set; }
    public string Description { get; set; }
    public ExerciseCategory Category { get; set; }
    public MuscleGroup PrimaryMuscleGroup { get; set; }
    public List<MuscleGroup> SecondaryMuscleGroups { get; set; }
    public EquipmentType Equipment { get; set; }
    public DifficultyLevel Difficulty { get; set; }
    public string Instructions { get; set; }
    public string VideoUrl { get; set; }
    public string ImageUrl { get; set; }
}

public class AddExerciseToLibraryCommandValidator : AbstractValidator<AddExerciseToLibraryCommand>
{
    public AddExerciseToLibraryCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Description).MaximumLength(1000);
        RuleFor(x => x.Instructions).NotEmpty().MaximumLength(5000);
    }
}
```

### RecordExercisePerformanceCommand
```csharp
public class RecordExercisePerformanceCommand : IRequest<Guid>
{
    public Guid ExerciseId { get; set; }
    public Guid UserId { get; set; }
    public Guid WorkoutSessionId { get; set; }
    public List<SetPerformanceDto> Sets { get; set; }
    public string Notes { get; set; }
}
```

### SubstituteExerciseCommand
```csharp
public class SubstituteExerciseCommand : IRequest<Unit>
{
    public Guid WorkoutPlanId { get; set; }
    public Guid OriginalExerciseId { get; set; }
    public Guid SubstituteExerciseId { get; set; }
    public Guid UserId { get; set; }
    public SubstitutionReason Reason { get; set; }
    public string Notes { get; set; }
}
```

### UpdateExerciseCommand
```csharp
public class UpdateExerciseCommand : IRequest<Unit>
{
    public Guid ExerciseId { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Instructions { get; set; }
    public string VideoUrl { get; set; }
    public string ImageUrl { get; set; }
}
```

## Queries

### GetExerciseByIdQuery
```csharp
public class GetExerciseByIdQuery : IRequest<ExerciseDto>
{
    public Guid ExerciseId { get; set; }
}
```

### SearchExercisesQuery
```csharp
public class SearchExercisesQuery : IRequest<PagedResult<ExerciseSummaryDto>>
{
    public string SearchTerm { get; set; }
    public ExerciseCategory? Category { get; set; }
    public MuscleGroup? MuscleGroup { get; set; }
    public EquipmentType? Equipment { get; set; }
    public DifficultyLevel? Difficulty { get; set; }
    public bool? IsCustom { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}
```

### GetExercisePerformanceHistoryQuery
```csharp
public class GetExercisePerformanceHistoryQuery : IRequest<List<ExercisePerformanceDto>>
{
    public Guid ExerciseId { get; set; }
    public Guid UserId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
```

### GetExerciseSubstitutionsQuery
```csharp
public class GetExerciseSubstitutionsQuery : IRequest<List<ExerciseDto>>
{
    public Guid ExerciseId { get; set; }
    public MuscleGroup? MuscleGroup { get; set; }
    public EquipmentType? Equipment { get; set; }
}
```

## DTOs

```csharp
public class ExerciseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public string PrimaryMuscleGroup { get; set; }
    public List<string> SecondaryMuscleGroups { get; set; }
    public string Equipment { get; set; }
    public string Difficulty { get; set; }
    public string Instructions { get; set; }
    public string VideoUrl { get; set; }
    public string ImageUrl { get; set; }
    public bool IsCustom { get; set; }
    public int PopularityScore { get; set; }
}

public class ExerciseSummaryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public string PrimaryMuscleGroup { get; set; }
    public string Equipment { get; set; }
    public bool IsCustom { get; set; }
}

public class ExercisePerformanceDto
{
    public Guid Id { get; set; }
    public Guid ExerciseId { get; set; }
    public string ExerciseName { get; set; }
    public DateTime PerformedDate { get; set; }
    public int TotalSets { get; set; }
    public int TotalReps { get; set; }
    public decimal MaxWeight { get; set; }
    public decimal TotalVolume { get; set; }
    public List<SetPerformanceDto> Sets { get; set; }
}

public class SetPerformanceDto
{
    public int SetNumber { get; set; }
    public int Reps { get; set; }
    public decimal Weight { get; set; }
    public bool IsWarmup { get; set; }
    public int RestSeconds { get; set; }
}
```

## API Endpoints

### Get Exercise by ID
**GET** `/api/exercises/{id}`

### Search Exercises
**GET** `/api/exercises?category=Strength&muscleGroup=Chest&equipment=Barbell&page=1`

### Add Custom Exercise
**POST** `/api/exercises`
```json
{
  "name": "Cable Crossover Variation",
  "description": "Modified cable crossover with elevated platform",
  "category": "Strength",
  "primaryMuscleGroup": "Chest",
  "secondaryMuscleGroups": ["Shoulders"],
  "equipment": "Cable",
  "difficulty": "Intermediate",
  "instructions": "Step-by-step instructions..."
}
```

### Update Exercise
**PUT** `/api/exercises/{id}`

### Record Performance
**POST** `/api/exercises/{id}/performance`
```json
{
  "workoutSessionId": "guid",
  "sets": [
    { "setNumber": 1, "reps": 10, "weight": 135, "isWarmup": true },
    { "setNumber": 2, "reps": 8, "weight": 185, "isWarmup": false },
    { "setNumber": 3, "reps": 6, "weight": 205, "isWarmup": false }
  ]
}
```

### Get Performance History
**GET** `/api/exercises/{id}/performance/history?startDate=2025-01-01`

### Get Substitution Suggestions
**GET** `/api/exercises/{id}/substitutions?equipment=Dumbbell`

### Substitute Exercise
**POST** `/api/exercises/substitute`
```json
{
  "workoutPlanId": "guid",
  "originalExerciseId": "guid",
  "substituteExerciseId": "guid",
  "reason": "EquipmentUnavailable",
  "notes": "Gym doesn't have that machine"
}
```

## Business Rules

1. **Exercise Creation**
   - System exercises cannot be modified by users
   - Custom exercises must be approved before public visibility
   - Exercise names must be unique per user for custom exercises

2. **Performance Recording**
   - Must belong to an active workout session
   - Sets must be recorded in sequence
   - Volume calculated as: sum(sets × reps × weight)

3. **Substitutions**
   - Substitute must target same primary muscle group
   - Original exercise cannot be substituted with itself
   - Track substitution patterns for recommendations

4. **Exercise Library**
   - Minimum 500 system exercises covering all muscle groups
   - Custom exercises private by default
   - Popular exercises ranked by performance frequency

## Repository Interface

```csharp
public interface IExerciseRepository
{
    Task<Exercise> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<PagedResult<Exercise>> SearchAsync(
        string searchTerm,
        ExerciseCategory? category,
        MuscleGroup? muscleGroup,
        EquipmentType? equipment,
        DifficultyLevel? difficulty,
        bool? isCustom,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default);
    Task<Exercise> AddAsync(Exercise exercise, CancellationToken cancellationToken = default);
    Task UpdateAsync(Exercise exercise, CancellationToken cancellationToken = default);
    Task<List<Exercise>> GetSubstitutionsAsync(
        Guid exerciseId,
        MuscleGroup? muscleGroup,
        EquipmentType? equipment,
        CancellationToken cancellationToken = default);
}

public interface IExercisePerformanceRepository
{
    Task<ExercisePerformance> AddAsync(ExercisePerformance performance, CancellationToken cancellationToken = default);
    Task<List<ExercisePerformance>> GetHistoryAsync(
        Guid exerciseId,
        Guid userId,
        DateTime? startDate,
        DateTime? endDate,
        CancellationToken cancellationToken = default);
    Task<ExercisePerformance> GetBestPerformanceAsync(
        Guid exerciseId,
        Guid userId,
        CancellationToken cancellationToken = default);
}
```

## Performance Considerations

- Index on: Name, Category, MuscleGroup, Equipment
- Full-text search on Name and Description
- Cache popular exercises (top 100)
- Lazy load exercise instructions and media
- Denormalize performance summary data

## Testing Requirements

### Unit Tests
- Exercise creation and validation
- Performance calculation logic
- Substitution logic and suggestions
- Search filtering

### Integration Tests
- Exercise CRUD operations
- Performance recording workflow
- Search functionality
- Event publishing
