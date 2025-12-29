# Backend Requirements - Goals and Achievements

## API Endpoints
- POST /api/travel-goals - Set goal (TravelGoalSet)
- GET /api/countries-visited - Get countries (CountryVisited)
- GET /api/achievements - Get milestones (TravelMilestoneAchieved)

## Models
```csharp
public class TravelGoal {
    public Guid Id;
    public GoalType Type;
    public int TargetMetric;
    public DateTime Timeframe;
    public int CurrentProgress;
}
```
