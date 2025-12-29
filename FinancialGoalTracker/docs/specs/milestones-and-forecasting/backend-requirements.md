# Backend Requirements - Milestones and Forecasting

## API Endpoints
- GET /api/goals/{id}/milestones - Get milestones (MilestoneReached)
- POST /api/goals/{id}/milestones - Create custom milestone (CustomMilestoneSet)
- GET /api/goals/{id}/forecast - Get projection (CompletionDateProjected, ProjectionUpdated)
- GET /api/goals/{id}/status - Get on-track status (OnTrackStatusChanged)

## Domain Models
```csharp
public class Milestone {
    public Guid Id { get; set; }
    public Guid GoalId { get; set; }
    public decimal TargetAmount { get; set; }
    public DateTime TargetDate { get; set; }
    public bool IsAchieved { get; set; }
    public DateTime? AchievementDate { get; set; }
}

public class Forecast {
    public DateTime ProjectedCompletionDate { get; set; }
    public decimal ConfidenceLevel { get; set; }
    public bool IsOnTrack { get; set; }
}
```
