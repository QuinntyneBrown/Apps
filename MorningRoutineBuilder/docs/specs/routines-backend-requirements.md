# Routines - Backend Requirements

## API Endpoints
- POST /api/routines - Create routine
- PUT /api/routines/{id} - Update routine
- GET /api/routines - Get user routines
- POST /api/routines/{id}/activities - Add activity
- DELETE /api/routines/{id} - Delete routine

## Domain Events
- RoutineCreated
- RoutineStarted
- RoutineCompleted
- RoutineModified
- RoutineOptimized

## Data Models
```csharp
public class MorningRoutine {
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public TimeSpan StartTime { get; set; }
    public List<RoutineActivity> Activities { get; set; }
    public bool IsActive { get; set; }
}

public class RoutineActivity {
    public Guid ActivityId { get; set; }
    public int Order { get; set; }
    public int DurationMinutes { get; set; }
}
```
