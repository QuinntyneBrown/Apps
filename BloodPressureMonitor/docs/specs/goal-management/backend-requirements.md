# Backend Requirements - Goal Management

## API Endpoints

### POST /api/goals
Create a new BP goal.
```json
Request: {
  "targetSystolicMin": 110,
  "targetSystolicMax": 120,
  "targetDiastolicMin": 70,
  "targetDiastolicMax": 80,
  "deadline": "2026-06-30",
  "setByDoctor": false
}
Response: 201 Created
```
**Events:** BloodPressureGoalSet

### GET /api/goals
List user's BP goals (active and completed).

### GET /api/goals/{goalId}/progress
Get goal achievement progress.
```json
Response: {
  "goalId": "uuid",
  "readingsInRange": 45,
  "totalReadings": 56,
  "percentageAchieved": 80.4,
  "currentStreak": 7,
  "longestStreak": 12,
  "daysRemaining": 45
}
```

### PUT /api/goals/{goalId}
Update goal targets or deadline.

### DELETE /api/goals/{goalId}
Archive a goal.

## Domain Models

```csharp
public class BPGoal : AggregateRoot
{
    public Guid GoalId { get; private set; }
    public Guid UserId { get; private set; }
    public BPRange TargetRange { get; private set; }
    public DateTime Deadline { get; private set; }
    public bool SetByDoctor { get; private set; }
    public GoalStatus Status { get; private set; }
    public DateTime AchievedDate { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public bool IsReadingInRange(Reading reading);
    public void MarkAsAchieved();
}

public class GoalProgress : ValueObject
{
    public int ReadingsInRange { get; private set; }
    public int TotalReadings { get; private set; }
    public int CurrentStreak { get; private set; }
    public double PercentageAchieved { get; private set; }
}

public enum GoalStatus { Active, Achieved, Expired, Archived }
```

## Business Rules

### BR-GM-001: Goal Achievement
- Goal achieved when reading is within target range for both systolic and diastolic
- Consistent control = 80% of readings in range over 30 days

### BR-GM-002: Streak Tracking
- Streak = consecutive days with at least one reading in goal range
- Streak broken if no in-range reading for >48 hours

### BR-GM-003: Goal Expiration
- Goals auto-expire after deadline passes
- User can extend deadline before expiration

## Event Handlers

### On BloodPressureRecorded
1. Check if reading falls within any active goal range
2. If yes, trigger BloodPressureGoalReached event
3. Update streak counters
4. Check for consistent control achievement

## Event Publishing

```json
BloodPressureGoalReached: {
  "readingId": "uuid",
  "goalId": "uuid",
  "achievementDate": "2025-12-29T08:30:00Z"
}

ConsistentControlAchieved: {
  "goalId": "uuid",
  "achievementPeriod": 30,
  "readingsInRange": 24,
  "successPercentage": 85.7
}
```

## Database Schema

```sql
CREATE TABLE BPGoals (
    GoalId UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    TargetSystolicMin INT NOT NULL,
    TargetSystolicMax INT NOT NULL,
    TargetDiastolicMin INT NOT NULL,
    TargetDiastolicMax INT NOT NULL,
    Deadline DATE,
    SetByDoctor BIT DEFAULT 0,
    Status NVARCHAR(20) DEFAULT 'Active',
    AchievedDate DATE NULL,
    CreatedAt DATETIME2 DEFAULT GETUTCDATE()
);

CREATE TABLE GoalAchievements (
    AchievementId UNIQUEIDENTIFIER PRIMARY KEY,
    GoalId UNIQUEIDENTIFIER NOT NULL,
    ReadingId UNIQUEIDENTIFIER NOT NULL,
    AchievedAt DATETIME2 DEFAULT GETUTCDATE(),
    FOREIGN KEY (GoalId) REFERENCES BPGoals(GoalId),
    FOREIGN KEY (ReadingId) REFERENCES Readings(ReadingId)
);
```
