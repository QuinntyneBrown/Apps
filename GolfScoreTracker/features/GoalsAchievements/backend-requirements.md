# Goals & Achievements - Backend Requirements

## API Endpoints

### POST /api/goals
```json
Request: {
  "goalType": "BreakScore | ReachHandicap",
  "targetValue": "int | decimal",
  "deadline": "datetime"
}
Domain Events: ScoringGoalSet
```

### GET /api/users/{userId}/achievements
Returns: List of unlocked achievements

### POST /api/achievements/{id}/celebrate
Mark achievement as celebrated

## Domain Model

```csharp
public class ScoringGoal : AggregateRoot
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public GoalType Type { get; private set; }
    public int TargetScore { get; private set; }
    public DateTime Deadline { get; private set; }
    public GoalStatus Status { get; private set; }
    public DateTime? AchievedDate { get; private set; }

    public void CheckAchievement(Round round)
    {
        if (round.TotalScore <= TargetScore)
        {
            Status = GoalStatus.Achieved;
            AchievedDate = DateTime.UtcNow;
            RaiseDomainEvent(new ScoringGoalAchieved(...));
        }
    }
}

public class Achievement : Entity
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public AchievementType Type { get; private set; }
    public DateTime UnlockedDate { get; private set; }
    public string IconUrl { get; private set; }
}
```

## Achievement Types
- First Birdie
- First Eagle
- Personal Best Round
- Break 100, 90, 80, 70
- Single Digit Handicap
- Hole in One
- Perfect Par Streak
- Course Record

## Database Schema

### scoring_goals
- id, user_id, goal_type, target_score, deadline
- status, achieved_date, created_at

### achievements
- id, user_id, achievement_type, name, description
- unlocked_date, icon_url, celebrated
