# Backend Requirements - Reading Goals

## API Endpoints

- **POST /api/reading-goals** - Create goal (ReadingGoalSet event)
- **PUT /api/reading-goals/{id}** - Update goal
- **DELETE /api/reading-goals/{id}** - Cancel goal
- **GET /api/reading-goals** - Get active goals
- **POST /api/reading-challenges/{id}/join** - Join challenge (ReadingChallengeJoined event)
- **GET /api/reading-goals/{id}/progress** - Get goal progress
- **GET /api/achievements** - Get milestones (ReadingMilestoneAchieved events)
- **GET /api/reading-streak** - Get streak info (ReadingStreakUpdated events)

## Domain Models

```csharp
public class ReadingGoal
{
    public Guid Id { get; set; }
    public GoalType GoalType { get; set; }
    public int TargetNumber { get; set; }
    public Timeframe Timeframe { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public List<string> GenreConstraints { get; set; }
    public int CurrentProgress { get; set; }
    public GoalStatus Status { get; set; }
}

public enum GoalType { BooksCount, PagesCount, GenreExploration }
public enum Timeframe { Month, Quarter, Year }
public enum GoalStatus { Active, Completed, Cancelled }

public class ReadingChallenge
{
    public Guid Id { get; set; }
    public string ChallengeName { get; set; }
    public string Rules { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public List<Guid> RequiredBookIds { get; set; }
    public int ParticipantCount { get; set; }
}

public class Milestone
{
    public Guid Id { get; set; }
    public MilestoneType MilestoneType { get; set; }
    public DateTime AchievementDate { get; set; }
    public int MetricAchieved { get; set; }
    public CelebrationTier CelebrationTier { get; set; }
}

public enum MilestoneType { BooksRead, PagesRead, YearStreak }
public enum CelebrationTier { Bronze, Silver, Gold, Platinum }
```

## Business Rules
1. Goal end date must be after start date
2. Target number must be positive
3. Progress auto-updates when books completed
4. Milestones trigger at: 10, 25, 50, 100, 250, 500 books
