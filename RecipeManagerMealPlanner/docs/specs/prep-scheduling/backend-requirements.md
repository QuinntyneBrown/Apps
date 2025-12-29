# Backend Requirements - Prep Scheduling

## Domain Events
- PrepTaskGenerated
- MakeAheadIdentified
- CookingTimelineOptimized

## API Endpoints

### Commands
- POST /api/prep/generate - Generate prep tasks for meal
- POST /api/prep/optimize - Optimize timeline for multiple recipes
- PUT /api/prep/tasks/{id}/complete - Mark task done
- POST /api/prep/tasks/{id}/assign - Assign to person

### Queries
- GET /api/prep/timeline/{mealPlanId} - Get prep timeline
- GET /api/prep/today - Today's prep tasks
- GET /api/prep/make-ahead - Make-ahead opportunities

## Domain Models

```csharp
public class PrepTask
{
    public Guid Id { get; private set; }
    public Guid RecipeId { get; private set; }
    public string Description { get; private set; }
    public TimeSpan Duration { get; private set; }
    public DateTime MustCompleteBy { get; private set; }
    public List<Guid> DependsOn { get; private set; }
    public Guid? AssignedTo { get; private set; }
    public bool IsCompleted { get; private set; }
}

public class CookingTimeline
{
    public List<PrepTask> Tasks { get; private set; }
    public TimeSpan TotalPrepTime { get; private set; }
    public List<ParallelTaskGroup> ParallelOpportunities { get; private set; }
    public TimeSpan TimeSaved { get; private set; }
}
```

## Business Rules
- Break recipes into discrete prep steps
- Identify task dependencies
- Find parallel cooking opportunities
- Suggest make-ahead components
- Optimize for minimum total time
- Consider available cook/helpers
