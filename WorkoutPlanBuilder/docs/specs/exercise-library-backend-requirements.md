# Exercise Library - Backend Requirements

## Overview
The Exercise Library backend manages a comprehensive database of exercises with search, filtering, and custom exercise creation capabilities.

## Domain Model

### Exercise (Aggregate Root)
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
    public Guid? CreatedBy { get; private set; }
    public DateTime CreatedDate { get; private set; }

    public static Exercise Create(string name, string description, ExerciseCategory category,
        MuscleGroup primaryMuscle, EquipmentType equipment, DifficultyLevel difficulty, Guid? userId = null)
    {
        var exercise = new Exercise
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description,
            Category = category,
            PrimaryMuscleGroup = primaryMuscle,
            Equipment = equipment,
            Difficulty = difficulty,
            IsCustom = userId.HasValue,
            CreatedBy = userId,
            CreatedDate = DateTime.UtcNow
        };

        if (userId.HasValue)
        {
            exercise.AddDomainEvent(new ExerciseAddedToLibraryEvent
            {
                ExerciseId = exercise.Id,
                ExerciseName = name,
                Category = category.ToString(),
                IsCustom = true,
                CreatedBy = userId,
                CreatedDate = exercise.CreatedDate
            });
        }

        return exercise;
    }
}
```

### Enums
```csharp
public enum ExerciseCategory { Strength, Cardio, Flexibility, Plyometrics, Olympic, Strongman }
public enum MuscleGroup { Chest, Back, Shoulders, Biceps, Triceps, Forearms, Abs, Obliques,
                          Quads, Hamstrings, Glutes, Calves, FullBody, Other }
public enum EquipmentType { Barbell, Dumbbell, Machine, Cable, Bodyweight, Kettlebell,
                           ResistanceBand, MedicineBall, Other }
public enum DifficultyLevel { Beginner, Intermediate, Advanced, Expert }
```

## Commands

### CreateExerciseCommand
```csharp
public class CreateExerciseCommand : IRequest<Guid>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public ExerciseCategory Category { get; set; }
    public MuscleGroup PrimaryMuscleGroup { get; set; }
    public List<MuscleGroup> SecondaryMuscleGroups { get; set; }
    public EquipmentType Equipment { get; set; }
    public DifficultyLevel Difficulty { get; set; }
    public string Instructions { get; set; }
}
```

## Queries

### SearchExercisesQuery
```csharp
public class SearchExercisesQuery : IRequest<List<ExerciseDto>>
{
    public string SearchTerm { get; set; }
    public ExerciseCategory? Category { get; set; }
    public MuscleGroup? MuscleGroup { get; set; }
    public EquipmentType? Equipment { get; set; }
    public DifficultyLevel? Difficulty { get; set; }
    public bool IncludeCustom { get; set; } = true;
}
```

### GetExerciseHistoryQuery
```csharp
public class GetExerciseHistoryQuery : IRequest<ExerciseHistoryDto>
{
    public Guid ExerciseId { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}
```

## Repository
```csharp
public interface IExerciseRepository : IRepository<Exercise>
{
    Task<Exercise> GetByIdAsync(Guid id);
    Task<List<Exercise>> SearchAsync(string searchTerm, ExerciseCategory? category,
        MuscleGroup? muscleGroup, EquipmentType? equipment, bool includeCustom, Guid? userId);
    Task<List<Exercise>> GetByMuscleGroupAsync(MuscleGroup muscleGroup);
    Task<bool> ExerciseNameExistsAsync(string name, Guid? excludeId = null);
}
```

## API Endpoints
```csharp
[Route("api/exercises")]
public class ExercisesController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<ExerciseDto>>> Search([FromQuery] SearchExercisesQuery query)

    [HttpGet("{id}")]
    public async Task<ActionResult<ExerciseDetailDto>> GetById(Guid id)

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateExerciseCommand command)

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(Guid id, [FromBody] UpdateExerciseCommand command)

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)

    [HttpGet("{id}/history")]
    public async Task<ActionResult<ExerciseHistoryDto>> GetHistory(Guid id, [FromQuery] DateTime? fromDate)
}
```

## Database Schema
```sql
CREATE TABLE Exercises (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    Name NVARCHAR(200) NOT NULL,
    Description NVARCHAR(2000),
    Category INT NOT NULL,
    PrimaryMuscleGroup INT NOT NULL,
    Equipment INT NOT NULL,
    Difficulty INT NOT NULL,
    Instructions NVARCHAR(MAX),
    VideoUrl NVARCHAR(500),
    ImageUrl NVARCHAR(500),
    IsCustom BIT NOT NULL DEFAULT 0,
    CreatedBy UNIQUEIDENTIFIER,
    CreatedDate DATETIME2 NOT NULL,

    INDEX IX_Exercises_Category (Category),
    INDEX IX_Exercises_PrimaryMuscleGroup (PrimaryMuscleGroup),
    INDEX IX_Exercises_Equipment (Equipment),
    INDEX IX_Exercises_IsCustom (IsCustom),
    INDEX IX_Exercises_Name (Name)
);

CREATE TABLE ExerciseSecondaryMuscles (
    ExerciseId UNIQUEIDENTIFIER NOT NULL,
    MuscleGroup INT NOT NULL,
    PRIMARY KEY (ExerciseId, MuscleGroup),
    FOREIGN KEY (ExerciseId) REFERENCES Exercises(Id) ON DELETE CASCADE
);
```

## Business Rules
1. Exercise names must be unique within user's library (for custom) or globally (for standard)
2. Custom exercises can only be modified/deleted by creator
3. Standard exercises cannot be modified or deleted
4. At least one muscle group must be specified
5. Instructions recommended but not required
6. Video/Image URLs must be valid URIs
