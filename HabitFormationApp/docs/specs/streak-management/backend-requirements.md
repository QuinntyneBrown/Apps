# Streak Management - Backend Requirements

## Overview
The Streak Management feature tracks consecutive habit completions, celebrates milestones, handles streak breaks, and supports streak recovery. Streaks are a core gamification element that motivates users.

## Domain Events
- **StreakStarted**: Triggered when a user begins a new streak (first completion)
- **StreakMilestoneReached**: Triggered when a streak reaches a significant milestone (7, 30, 100 days, etc.)
- **StreakBroken**: Triggered when a streak is interrupted
- **StreakRecovered**: Triggered when a user uses a streak freeze or recovers after a break

## Aggregates

### Streak Aggregate
**Properties:**
- `Id` (Guid): Unique identifier
- `HabitId` (Guid): Associated habit
- `UserId` (Guid): Streak owner
- `CurrentStreak` (int): Current consecutive days/completions
- `LongestStreak` (int): Best streak ever achieved
- `StartDate` (DateTime): When current streak started
- `LastCompletionDate` (DateTime?): Most recent completion
- `Status` (StreakStatus): Active, Broken, Frozen
- `FreezeTokens` (int): Available streak freeze tokens
- `FreezeUsedDates` (List<DateTime>): Dates when freezes were used
- `MilestonesReached` (List<int>): Milestone numbers achieved
- `TotalCompletions` (int): Lifetime completion count
- `BreakCount` (int): How many times streak has been broken

**Invariants:**
- CurrentStreak cannot exceed TotalCompletions
- LastCompletionDate must be the most recent
- Status must be valid for current state
- FreezeTokens cannot be negative

## Commands

### UpdateStreakCommand
```csharp
public record UpdateStreakCommand(
    Guid HabitId,
    Guid UserId,
    DateTime CompletionDate
);
```

### UseStreakFreezeCommand
```csharp
public record UseStreakFreezeCommand(
    Guid StreakId,
    Guid UserId,
    DateTime FreezeDate,
    string Reason
);
```

### RecoverStreakCommand
```csharp
public record RecoverStreakCommand(
    Guid StreakId,
    Guid UserId,
    DateTime RecoveryDate
);
```

## Queries

### GetStreakByHabitQuery
Returns current streak information for a habit.

### GetStreakHistoryQuery
Returns historical streak data with breaks and recoveries.

### GetStreakLeaderboardQuery
Returns top streaks (user's friends or global).

### GetMilestoneProgressQuery
Returns progress toward next milestone.

## API Endpoints

### GET /api/streaks/habit/{habitId}
Get current streak for a habit.
- **Response**: 200 OK with StreakDto

### GET /api/streaks/habit/{habitId}/history
Get streak history including breaks.
- **Query Parameters**: startDate, endDate
- **Response**: 200 OK with StreakHistoryDto

### POST /api/streaks/{id}/freeze
Use a streak freeze token.
- **Request**: UseStreakFreezeCommand
- **Response**: 200 OK
- **Events**: StreakFrozen

### POST /api/streaks/{id}/recover
Recover a broken streak (premium feature).
- **Request**: RecoverStreakCommand
- **Response**: 200 OK
- **Events**: StreakRecovered

### GET /api/streaks/leaderboard
Get streak leaderboard.
- **Query Parameters**: scope (friends, global), habitCategory
- **Response**: 200 OK with List<LeaderboardEntryDto>

### GET /api/streaks/milestones/{habitId}
Get milestone progress.
- **Response**: 200 OK with MilestoneProgressDto

## Value Objects

### StreakStatus (Enum)
- Active
- Broken
- Frozen
- Recovered

### Milestone
```csharp
public record Milestone(
    int Days,
    string Name,
    string Description,
    string BadgeIcon
);
```

## Business Rules

### Streak Calculation
1. **Daily Habits**: One completion per calendar day maintains streak
2. **Weekly Habits**: Meeting weekly target maintains streak
3. **Custom Frequency**: Meeting specified frequency maintains streak

### Streak Breaks
1. Missing a scheduled day breaks the streak
2. Broken streaks are archived in history
3. New streak starts from next completion
4. LongestStreak is preserved even after breaks

### Streak Freezes
1. Users earn freeze tokens through achievements
2. Can use freeze on a missed day to preserve streak
3. Must be used within 48 hours of missed day
4. Limited to X freezes per month
5. Premium users get more freeze tokens

### Milestones
Predefined milestones:
- 3 days: "Getting Started"
- 7 days: "One Week Wonder"
- 14 days: "Two Week Warrior"
- 30 days: "Monthly Master"
- 50 days: "Halfway to 100"
- 100 days: "Century Club"
- 365 days: "Year-Long Champion"
- Custom milestones based on habit

### Recovery Options
1. **Streak Freeze**: Use token to skip a day
2. **Grace Period**: 24-hour grace for late completions
3. **Premium Recovery**: Ability to recover broken streak once per month (premium feature)

## Database Schema

### Streaks Table
```sql
CREATE TABLE Streaks (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    HabitId UNIQUEIDENTIFIER NOT NULL,
    UserId UNIQUEIDENTIFIER NOT NULL,
    CurrentStreak INT NOT NULL DEFAULT 0,
    LongestStreak INT NOT NULL DEFAULT 0,
    StartDate DATETIME2 NOT NULL,
    LastCompletionDate DATETIME2 NULL,
    Status NVARCHAR(20) NOT NULL,
    FreezeTokens INT NOT NULL DEFAULT 0,
    TotalCompletions INT NOT NULL DEFAULT 0,
    BreakCount INT NOT NULL DEFAULT 0,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    CONSTRAINT FK_Streaks_Habits FOREIGN KEY (HabitId) REFERENCES Habits(Id),
    CONSTRAINT FK_Streaks_Users FOREIGN KEY (UserId) REFERENCES Users(Id)
);

CREATE TABLE StreakHistory (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    StreakId UNIQUEIDENTIFIER NOT NULL,
    StartDate DATETIME2 NOT NULL,
    EndDate DATETIME2 NULL,
    StreakLength INT NOT NULL,
    EndReason NVARCHAR(50) NULL, -- Broken, Archived, Ongoing
    CONSTRAINT FK_StreakHistory_Streaks FOREIGN KEY (StreakId) REFERENCES Streaks(Id)
);

CREATE TABLE StreakFreezes (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    StreakId UNIQUEIDENTIFIER NOT NULL,
    FreezeDate DATETIME2 NOT NULL,
    Reason NVARCHAR(200) NULL,
    UsedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    CONSTRAINT FK_StreakFreezes_Streaks FOREIGN KEY (StreakId) REFERENCES Streaks(Id)
);

CREATE TABLE StreakMilestones (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    StreakId UNIQUEIDENTIFIER NOT NULL,
    MilestoneDays INT NOT NULL,
    AchievedAt DATETIME2 NOT NULL,
    CONSTRAINT FK_StreakMilestones_Streaks FOREIGN KEY (StreakId) REFERENCES Streaks(Id)
);

CREATE INDEX IX_Streaks_HabitId ON Streaks(HabitId);
CREATE INDEX IX_Streaks_UserId ON Streaks(UserId);
CREATE INDEX IX_StreakHistory_StreakId ON StreakHistory(StreakId);
```

## Integration Points
- Completion service: Receive completion events to update streaks
- Notification service: Notify users of milestones and breaks
- Achievement service: Award badges for milestone achievements
- Analytics service: Track streak patterns and break reasons

## Streak Calculation Logic

### Algorithm
```
1. Get habit frequency details
2. Get all completions ordered by date DESC
3. Start from most recent completion
4. For daily habits:
   - Check each previous day
   - If completion exists, increment streak
   - If freeze was used, continue checking
   - If day is missed and no freeze, break loop
5. For weekly habits:
   - Check if weekly target was met
   - Continue backwards through weeks
6. Return current streak count
```

### Grace Period Logic
- For daily habits scheduled before noon: Grace until 11:59 PM same day
- For daily habits scheduled after noon: Grace until 6:00 AM next day
- Late completions within grace period don't break streak but are flagged

## Security & Validation
- Users can only manage their own streaks
- Freeze usage validated against available tokens
- Recovery attempts validated against eligibility (subscription, time limits)
- Prevent streak manipulation
- Audit log for all streak modifications

## Performance Considerations
- Cache current streak value (invalidate on completion/break)
- Optimize streak calculation with indexed queries
- Background job for streak status checks at midnight
- Batch process milestone detection
- Efficient leaderboard queries with pagination

## Testing Requirements
- Unit tests for streak calculation algorithm
- Tests for all edge cases:
  - Completion at midnight (timezone)
  - Multiple completions in one day
  - Week boundary transitions
  - Freeze usage scenarios
  - Recovery scenarios
- Integration tests for event publishing
- Performance tests for leaderboard queries
