# Backend Requirements - Reading Goals

## Domain Events
- ReadingGoalSet
- ReadingMilestoneAchieved
- SkillAreaDeveloped

## Key API Endpoints
- POST /api/goals - Create reading goal
- GET /api/goals/progress - Get goal progress
- PUT /api/goals/{id}/complete - Complete goal
- GET /api/milestones - Get achievements

## Data Models
```csharp
public class ReadingGoal : AggregateRoot
{
    public Guid Id { get; private set; }
    public GoalType Type { get; private set; }
    public int TargetMetric { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public int CurrentProgress { get; private set; }
}
```
