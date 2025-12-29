# Workout Plans - Backend Requirements

## Overview
The Workout Plans backend manages the creation, modification, and lifecycle of structured workout routines. It implements CQRS pattern with domain events for tracking plan state changes.

## Domain Model

### Entities

#### WorkoutPlan (Aggregate Root)
```csharp
public class WorkoutPlan : AggregateRoot
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public Guid UserId { get; private set; }
    public DateTime CreatedDate { get; private set; }
    public DateTime? ModifiedDate { get; private set; }
    public WorkoutPlanStatus Status { get; private set; }
    public int DurationWeeks { get; private set; }
    public bool IsPublic { get; private set; }
    public DateTime? StartedDate { get; private set; }
    public DateTime? CompletedDate { get; private set; }

    private List<Workout> _workouts = new();
    public IReadOnlyList<Workout> Workouts => _workouts.AsReadOnly();

    // Factory method
    public static WorkoutPlan Create(string name, string description, Guid userId, int durationWeeks, bool isPublic)
    {
        var plan = new WorkoutPlan
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description,
            UserId = userId,
            CreatedDate = DateTime.UtcNow,
            Status = WorkoutPlanStatus.Draft,
            DurationWeeks = durationWeeks,
            IsPublic = isPublic
        };

        plan.AddDomainEvent(new WorkoutPlanCreatedEvent
        {
            WorkoutPlanId = plan.Id,
            UserId = userId,
            PlanName = name,
            DurationWeeks = durationWeeks,
            CreatedDate = plan.CreatedDate
        });

        return plan;
    }

    // Business methods
    public void AddWorkout(Workout workout)
    {
        _workouts.Add(workout);
        ModifiedDate = DateTime.UtcNow;
        AddDomainEvent(new WorkoutPlanModifiedEvent
        {
            WorkoutPlanId = Id,
            UserId = UserId,
            ModificationType = "WorkoutAdded",
            ModifiedDate = ModifiedDate.Value
        });
    }

    public void Start()
    {
        if (Status != WorkoutPlanStatus.Draft && Status != WorkoutPlanStatus.Active)
            throw new InvalidOperationException("Only draft or active plans can be started");

        Status = WorkoutPlanStatus.Active;
        StartedDate = DateTime.UtcNow;

        AddDomainEvent(new WorkoutPlanStartedEvent
        {
            WorkoutPlanId = Id,
            UserId = UserId,
            StartDate = StartedDate.Value
        });
    }

    public void Complete(int totalWorkoutsCompleted, int totalWorkoutsSkipped)
    {
        if (Status != WorkoutPlanStatus.Active)
            throw new InvalidOperationException("Only active plans can be completed");

        Status = WorkoutPlanStatus.Completed;
        CompletedDate = DateTime.UtcNow;

        AddDomainEvent(new WorkoutPlanCompletedEvent
        {
            WorkoutPlanId = Id,
            UserId = UserId,
            CompletedDate = CompletedDate.Value,
            TotalWorkoutsCompleted = totalWorkoutsCompleted,
            TotalWorkoutsSkipped = totalWorkoutsSkipped
        });
    }

    public void Update(string name, string description, int durationWeeks, bool isPublic)
    {
        Name = name;
        Description = description;
        DurationWeeks = durationWeeks;
        IsPublic = isPublic;
        ModifiedDate = DateTime.UtcNow;

        AddDomainEvent(new WorkoutPlanModifiedEvent
        {
            WorkoutPlanId = Id,
            UserId = UserId,
            ModificationType = "PlanUpdated",
            ModifiedDate = ModifiedDate.Value
        });
    }
}
```

#### Workout (Entity)
```csharp
public class Workout : Entity
{
    public Guid Id { get; private set; }
    public Guid WorkoutPlanId { get; private set; }
    public string Name { get; private set; }
    public DayOfWeek? DayOfWeek { get; private set; }
    public int OrderIndex { get; private set; }
    public TimeSpan EstimatedDuration { get; private set; }
    public string Notes { get; private set; }

    private List<WorkoutExercise> _exercises = new();
    public IReadOnlyList<WorkoutExercise> Exercises => _exercises.AsReadOnly();

    public static Workout Create(Guid workoutPlanId, string name, int orderIndex)
    {
        return new Workout
        {
            Id = Guid.NewGuid(),
            WorkoutPlanId = workoutPlanId,
            Name = name,
            OrderIndex = orderIndex,
            EstimatedDuration = TimeSpan.Zero
        };
    }

    public void AddExercise(WorkoutExercise exercise)
    {
        _exercises.Add(exercise);
        RecalculateEstimatedDuration();
    }

    private void RecalculateEstimatedDuration()
    {
        var totalSeconds = _exercises.Sum(e =>
            (e.TargetSets * 30) + // 30 seconds per set
            (e.TargetSets * e.RestSeconds) // rest between sets
        );
        EstimatedDuration = TimeSpan.FromSeconds(totalSeconds);
    }
}
```

#### WorkoutExercise (Entity)
```csharp
public class WorkoutExercise : Entity
{
    public Guid Id { get; private set; }
    public Guid WorkoutId { get; private set; }
    public Guid ExerciseId { get; private set; }
    public int OrderIndex { get; private set; }
    public int TargetSets { get; private set; }
    public string TargetReps { get; private set; } // "8-12", "15", "AMRAP", "Max"
    public decimal? TargetWeight { get; private set; }
    public WeightUnit? WeightUnit { get; private set; }
    public int RestSeconds { get; private set; }
    public string Notes { get; private set; }

    public static WorkoutExercise Create(
        Guid workoutId,
        Guid exerciseId,
        int orderIndex,
        int targetSets,
        string targetReps,
        int restSeconds)
    {
        return new WorkoutExercise
        {
            Id = Guid.NewGuid(),
            WorkoutId = workoutId,
            ExerciseId = exerciseId,
            OrderIndex = orderIndex,
            TargetSets = targetSets,
            TargetReps = targetReps,
            RestSeconds = restSeconds
        };
    }
}
```

### Enums

```csharp
public enum WorkoutPlanStatus
{
    Draft = 0,
    Active = 1,
    Completed = 2,
    Archived = 3
}

public enum WeightUnit
{
    Pounds = 0,
    Kilograms = 1
}
```

## Commands

### CreateWorkoutPlanCommand
```csharp
public class CreateWorkoutPlanCommand : IRequest<Guid>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int DurationWeeks { get; set; }
    public bool IsPublic { get; set; }
}

public class CreateWorkoutPlanCommandHandler : IRequestHandler<CreateWorkoutPlanCommand, Guid>
{
    private readonly IWorkoutPlanRepository _repository;
    private readonly ICurrentUserService _currentUser;

    public async Task<Guid> Handle(CreateWorkoutPlanCommand request, CancellationToken cancellationToken)
    {
        var plan = WorkoutPlan.Create(
            request.Name,
            request.Description,
            _currentUser.UserId,
            request.DurationWeeks,
            request.IsPublic
        );

        await _repository.AddAsync(plan, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        return plan.Id;
    }
}
```

### UpdateWorkoutPlanCommand
```csharp
public class UpdateWorkoutPlanCommand : IRequest<Unit>
{
    public Guid WorkoutPlanId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int DurationWeeks { get; set; }
    public bool IsPublic { get; set; }
}
```

### StartWorkoutPlanCommand
```csharp
public class StartWorkoutPlanCommand : IRequest<Unit>
{
    public Guid WorkoutPlanId { get; set; }
}
```

### CompleteWorkoutPlanCommand
```csharp
public class CompleteWorkoutPlanCommand : IRequest<Unit>
{
    public Guid WorkoutPlanId { get; set; }
}
```

### AddWorkoutCommand
```csharp
public class AddWorkoutCommand : IRequest<Guid>
{
    public Guid WorkoutPlanId { get; set; }
    public string Name { get; set; }
    public DayOfWeek? DayOfWeek { get; set; }
    public int OrderIndex { get; set; }
    public List<WorkoutExerciseDto> Exercises { get; set; }
}

public class WorkoutExerciseDto
{
    public Guid ExerciseId { get; set; }
    public int OrderIndex { get; set; }
    public int TargetSets { get; set; }
    public string TargetReps { get; set; }
    public decimal? TargetWeight { get; set; }
    public WeightUnit? WeightUnit { get; set; }
    public int RestSeconds { get; set; }
    public string Notes { get; set; }
}
```

### DeleteWorkoutPlanCommand
```csharp
public class DeleteWorkoutPlanCommand : IRequest<Unit>
{
    public Guid WorkoutPlanId { get; set; }
}
```

## Queries

### GetWorkoutPlanByIdQuery
```csharp
public class GetWorkoutPlanByIdQuery : IRequest<WorkoutPlanDetailDto>
{
    public Guid WorkoutPlanId { get; set; }
}

public class WorkoutPlanDetailDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid UserId { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public string Status { get; set; }
    public int DurationWeeks { get; set; }
    public bool IsPublic { get; set; }
    public DateTime? StartedDate { get; set; }
    public DateTime? CompletedDate { get; set; }
    public List<WorkoutDto> Workouts { get; set; }
    public int TotalWorkouts { get; set; }
    public TimeSpan EstimatedTotalDuration { get; set; }
}

public class WorkoutDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string DayOfWeek { get; set; }
    public int OrderIndex { get; set; }
    public TimeSpan EstimatedDuration { get; set; }
    public List<WorkoutExerciseDetailDto> Exercises { get; set; }
}

public class WorkoutExerciseDetailDto
{
    public Guid Id { get; set; }
    public Guid ExerciseId { get; set; }
    public string ExerciseName { get; set; }
    public int OrderIndex { get; set; }
    public int TargetSets { get; set; }
    public string TargetReps { get; set; }
    public decimal? TargetWeight { get; set; }
    public string WeightUnit { get; set; }
    public int RestSeconds { get; set; }
    public string Notes { get; set; }
}
```

### GetUserWorkoutPlansQuery
```csharp
public class GetUserWorkoutPlansQuery : IRequest<List<WorkoutPlanSummaryDto>>
{
    public WorkoutPlanStatus? Status { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}

public class WorkoutPlanSummaryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }
    public int DurationWeeks { get; set; }
    public int TotalWorkouts { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? StartedDate { get; set; }
    public DateTime? CompletedDate { get; set; }
    public int CompletionPercentage { get; set; }
}
```

### GetPublicWorkoutPlansQuery
```csharp
public class GetPublicWorkoutPlansQuery : IRequest<List<WorkoutPlanSummaryDto>>
{
    public string SearchTerm { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}
```

## Repository Interface

```csharp
public interface IWorkoutPlanRepository : IRepository<WorkoutPlan>
{
    Task<WorkoutPlan> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<WorkoutPlan> GetByIdWithWorkoutsAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<WorkoutPlan>> GetUserPlansAsync(Guid userId, WorkoutPlanStatus? status, CancellationToken cancellationToken = default);
    Task<List<WorkoutPlan>> GetPublicPlansAsync(string searchTerm, CancellationToken cancellationToken = default);
    Task<bool> UserOwnsplanAsync(Guid userId, Guid planId, CancellationToken cancellationToken = default);
}
```

## Event Handlers

### WorkoutPlanCompletedEventHandler
```csharp
public class WorkoutPlanCompletedEventHandler : INotificationHandler<WorkoutPlanCompletedEvent>
{
    private readonly INotificationService _notificationService;
    private readonly IAnalyticsService _analyticsService;

    public async Task Handle(WorkoutPlanCompletedEvent notification, CancellationToken cancellationToken)
    {
        // Send congratulations notification
        await _notificationService.SendAsync(
            notification.UserId,
            "Workout Plan Completed!",
            $"Congratulations on completing your workout plan! {notification.TotalWorkoutsCompleted} workouts completed."
        );

        // Track analytics
        await _analyticsService.TrackEventAsync(
            "workout_plan_completed",
            notification.UserId,
            new Dictionary<string, object>
            {
                { "plan_id", notification.WorkoutPlanId },
                { "workouts_completed", notification.TotalWorkoutsCompleted },
                { "workouts_skipped", notification.TotalWorkoutsSkipped }
            }
        );
    }
}
```

## Validation

### CreateWorkoutPlanCommandValidator
```csharp
public class CreateWorkoutPlanCommandValidator : AbstractValidator<CreateWorkoutPlanCommand>
{
    public CreateWorkoutPlanCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Plan name is required")
            .MaximumLength(200).WithMessage("Plan name cannot exceed 200 characters");

        RuleFor(x => x.Description)
            .MaximumLength(2000).WithMessage("Description cannot exceed 2000 characters");

        RuleFor(x => x.DurationWeeks)
            .GreaterThan(0).WithMessage("Duration must be at least 1 week")
            .LessThanOrEqualTo(52).WithMessage("Duration cannot exceed 52 weeks");
    }
}
```

## API Endpoints

### WorkoutPlansController
```csharp
[ApiController]
[Route("api/workout-plans")]
[Authorize]
public class WorkoutPlansController : ControllerBase
{
    private readonly IMediator _mediator;

    [HttpGet]
    public async Task<ActionResult<List<WorkoutPlanSummaryDto>>> GetUserPlans(
        [FromQuery] WorkoutPlanStatus? status,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 20)
    {
        var query = new GetUserWorkoutPlansQuery
        {
            Status = status,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<WorkoutPlanDetailDto>> GetById(Guid id)
    {
        var query = new GetWorkoutPlanByIdQuery { WorkoutPlanId = id };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateWorkoutPlanCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(Guid id, [FromBody] UpdateWorkoutPlanCommand command)
    {
        command.WorkoutPlanId = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPost("{id}/start")]
    public async Task<ActionResult> Start(Guid id)
    {
        var command = new StartWorkoutPlanCommand { WorkoutPlanId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPost("{id}/complete")]
    public async Task<ActionResult> Complete(Guid id)
    {
        var command = new CompleteWorkoutPlanCommand { WorkoutPlanId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPost("{id}/workouts")]
    public async Task<ActionResult<Guid>> AddWorkout(Guid id, [FromBody] AddWorkoutCommand command)
    {
        command.WorkoutPlanId = id;
        var workoutId = await _mediator.Send(command);
        return Ok(workoutId);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var command = new DeleteWorkoutPlanCommand { WorkoutPlanId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet("public")]
    [AllowAnonymous]
    public async Task<ActionResult<List<WorkoutPlanSummaryDto>>> GetPublicPlans(
        [FromQuery] string searchTerm,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 20)
    {
        var query = new GetPublicWorkoutPlansQuery
        {
            SearchTerm = searchTerm,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}
```

## Database Schema

```sql
CREATE TABLE WorkoutPlans (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Name NVARCHAR(200) NOT NULL,
    Description NVARCHAR(2000),
    UserId UNIQUEIDENTIFIER NOT NULL,
    CreatedDate DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    ModifiedDate DATETIME2,
    Status INT NOT NULL DEFAULT 0,
    DurationWeeks INT NOT NULL,
    IsPublic BIT NOT NULL DEFAULT 0,
    StartedDate DATETIME2,
    CompletedDate DATETIME2,

    INDEX IX_WorkoutPlans_UserId (UserId),
    INDEX IX_WorkoutPlans_Status (Status),
    INDEX IX_WorkoutPlans_IsPublic (IsPublic)
);

CREATE TABLE Workouts (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    WorkoutPlanId UNIQUEIDENTIFIER NOT NULL,
    Name NVARCHAR(200) NOT NULL,
    DayOfWeek INT,
    OrderIndex INT NOT NULL,
    EstimatedDuration TIME,
    Notes NVARCHAR(1000),

    FOREIGN KEY (WorkoutPlanId) REFERENCES WorkoutPlans(Id) ON DELETE CASCADE,
    INDEX IX_Workouts_WorkoutPlanId (WorkoutPlanId)
);

CREATE TABLE WorkoutExercises (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    WorkoutId UNIQUEIDENTIFIER NOT NULL,
    ExerciseId UNIQUEIDENTIFIER NOT NULL,
    OrderIndex INT NOT NULL,
    TargetSets INT NOT NULL,
    TargetReps NVARCHAR(50) NOT NULL,
    TargetWeight DECIMAL(10,2),
    WeightUnit INT,
    RestSeconds INT NOT NULL DEFAULT 60,
    Notes NVARCHAR(500),

    FOREIGN KEY (WorkoutId) REFERENCES Workouts(Id) ON DELETE CASCADE,
    FOREIGN KEY (ExerciseId) REFERENCES Exercises(Id),
    INDEX IX_WorkoutExercises_WorkoutId (WorkoutId),
    INDEX IX_WorkoutExercises_ExerciseId (ExerciseId)
);
```

## Business Rules

1. Only the owner can modify their workout plans
2. Plans cannot be deleted if they have associated workout sessions
3. Active plans can have workouts added/removed
4. Completed plans are read-only (must be cloned to modify)
5. Public plans can be viewed by anyone but only modified by owner
6. Plan duration must be between 1-52 weeks
7. Each workout must have at least one exercise
8. Exercise order must be sequential (1, 2, 3...)

## Performance Considerations

- Use eager loading for WorkoutPlans with Workouts and Exercises when needed
- Implement caching for public workout plans
- Use pagination for large result sets
- Index foreign keys and commonly queried fields
- Use AsNoTracking for read-only queries
- Implement query result caching for frequently accessed plans

## Security

- Validate user ownership before allowing modifications
- Sanitize user input to prevent XSS
- Use parameterized queries to prevent SQL injection
- Implement rate limiting on plan creation
- Validate file uploads for exercise media
- Ensure proper authorization checks in all endpoints
