# Race Management - Backend

## API Endpoints

### POST /api/races
Register for upcoming race

### POST /api/races/{id}/complete
Log race results
Domain Events: RaceCompleted, RaceGoalAchieved

### POST /api/races/{id}/goal
Set race goal time

## Domain Model

```csharp
public class Race : AggregateRoot
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public RaceDistance Distance { get; private set; }
    public DateTime RaceDate { get; private set; }
    public TimeSpan? GoalTime { get; private set; }
    public TimeSpan? FinishTime { get; private set; }
    public int? OverallPlacement { get; private set; }
    public int? AgeGroupPlacement { get; private set; }

    public void Complete(TimeSpan finishTime)
    {
        FinishTime = finishTime;
        RaiseDomainEvent(new RaceCompleted(...));

        if (GoalTime.HasValue && finishTime <= GoalTime.Value)
            RaiseDomainEvent(new RaceGoalAchieved(...));
    }
}
```

## Database Schema
### races
- id, user_id, name, distance, race_date
- goal_time, finish_time, overall_placement
- age_group_placement, registration_date
