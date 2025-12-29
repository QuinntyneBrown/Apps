# Motivation - Backend Requirements

## Overview
The Motivation feature tracks achievements, detects motivation dips, celebrates personal bests, and provides personalized encouragement to keep users engaged.

## Domain Events
- **MilestoneAchieved**: Triggered when user reaches significant achievements
- **MotivationDipDetected**: Triggered when system detects declining engagement
- **PersonalBestSet**: Triggered when user sets a new personal record

## Aggregates

### Achievement
**Properties:**
- `Id` (Guid)
- `UserId` (Guid)
- `Type` (AchievementType): Milestone, Streak, Consistency, Category
- `HabitId` (Guid?)
- `Title` (string)
- `Description` (string)
- `BadgeIcon` (string)
- `Points` (int)
- `AchievedAt` (DateTime)
- `IsUnlocked` (bool)

### MotivationMetrics
**Properties:**
- `Id` (Guid)
- `UserId` (Guid)
- `WeekStartDate` (DateTime)
- `CompletionRate` (double)
- `ActiveHabits` (int)
- `TotalCompletions` (int)
- `StreaksActive` (int)
- `MotivationScore` (int): 0-100
- `Trend` (TrendDirection): Improving, Stable, Declining

### PersonalBest
**Properties:**
- `Id` (Guid)
- `UserId` (Guid)
- `HabitId` (Guid)
- `MetricType` (string): LongestStreak, MostCompletionsInWeek, etc.
- `Value` (int)
- `AchievedAt` (DateTime)
- `PreviousBest` (int?)

## Commands

### RecordAchievementCommand
```csharp
public record RecordAchievementCommand(
    Guid UserId,
    AchievementType Type,
    Guid? HabitId,
    string Title,
    string Description,
    int Points
);
```

### CalculateMotivationScoreCommand
```csharp
public record CalculateMotivationScoreCommand(
    Guid UserId,
    DateTime ForWeek
);
```

### RecordPersonalBestCommand
```csharp
public record RecordPersonalBestCommand(
    Guid UserId,
    Guid HabitId,
    string MetricType,
    int Value
);
```

## Queries

### GetAchievementsQuery
Returns user's achievements with unlock status

### GetMotivationScoreQuery
Returns current motivation score and trend

### GetPersonalBestsQuery
Returns all personal bests for user

### GetAvailableAchievementsQuery
Returns locked achievements with progress

## API Endpoints

### GET /api/motivation/achievements
Get user achievements

### GET /api/motivation/score
Get motivation score and trends

### GET /api/motivation/personal-bests
Get personal records

### GET /api/motivation/available
Get locked achievements with progress

### POST /api/motivation/celebrate
Manually trigger celebration (for testing)

## Achievement Types
1. **Streak Achievements**
   - First Streak (3 days)
   - Week Warrior (7 days)
   - Month Master (30 days)
   - Century Club (100 days)
   - Year Champion (365 days)

2. **Completion Achievements**
   - First Step (1 completion)
   - Getting Started (10 completions)
   - Habit Builder (50 completions)
   - Dedicated (100 completions)
   - Expert (500 completions)

3. **Consistency Achievements**
   - Perfect Week (7/7 days)
   - Perfect Month (30/30 days)
   - Early Bird (complete before 8 AM for week)
   - Night Owl (complete after 8 PM for week)

4. **Category Achievements**
   - Health Champion (complete all health habits for month)
   - Learning Leader (50 learning habit completions)
   - Productivity Pro (100 productivity completions)

5. **Social Achievements**
   - Team Player (add first accountability partner)
   - Encourager (send 10 encouragements)
   - Supporter (help partner maintain streak)

## Motivation Score Calculation
```
Score = (
  CompletionRate * 40 +
  ActiveStreaks * 10 +
  ConsistencyFactor * 20 +
  SocialEngagement * 10 +
  RecentActivity * 20
) / 100 * 100
```

## Motivation Dip Detection
Triggers when:
- Completion rate drops >30% from average
- No completions for 3+ consecutive days
- 50% or more of streaks broken in same week
- Motivation score <40 for 2+ weeks

## Intervention Strategies
When dip detected:
1. Send personalized encouragement
2. Suggest easier habits temporarily
3. Notify accountability partners
4. Offer streak freeze tokens
5. Show past achievements for inspiration

## Database Schema

```sql
CREATE TABLE Achievements (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    Type NVARCHAR(50) NOT NULL,
    HabitId UNIQUEIDENTIFIER NULL,
    Title NVARCHAR(100) NOT NULL,
    Description NVARCHAR(500) NOT NULL,
    BadgeIcon NVARCHAR(50) NOT NULL,
    Points INT NOT NULL,
    AchievedAt DATETIME2 NOT NULL,
    IsUnlocked BIT NOT NULL DEFAULT 1,
    CONSTRAINT FK_Achievements_Users FOREIGN KEY (UserId) REFERENCES Users(Id)
);

CREATE TABLE MotivationMetrics (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    WeekStartDate DATETIME2 NOT NULL,
    CompletionRate DECIMAL(5,2) NOT NULL,
    ActiveHabits INT NOT NULL,
    TotalCompletions INT NOT NULL,
    StreaksActive INT NOT NULL,
    MotivationScore INT NOT NULL,
    Trend NVARCHAR(20) NOT NULL,
    CalculatedAt DATETIME2 NOT NULL,
    CONSTRAINT FK_MotivationMetrics_Users FOREIGN KEY (UserId) REFERENCES Users(Id)
);

CREATE TABLE PersonalBests (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    HabitId UNIQUEIDENTIFIER NOT NULL,
    MetricType NVARCHAR(50) NOT NULL,
    Value INT NOT NULL,
    AchievedAt DATETIME2 NOT NULL,
    PreviousBest INT NULL,
    CONSTRAINT FK_PersonalBests_Users FOREIGN KEY (UserId) REFERENCES Users(Id),
    CONSTRAINT FK_PersonalBests_Habits FOREIGN KEY (HabitId) REFERENCES Habits(Id)
);
```

## Integration Points
- Completion service: Track completions for achievements
- Streak service: Monitor streak achievements
- Notification service: Send motivational messages
- Analytics service: Provide data for motivation score

## Background Jobs
- Daily: Calculate motivation scores
- Daily: Check for new achievements
- Weekly: Detect motivation dips
- Monthly: Generate motivation reports

## Gamification Elements
- Points system (redeemable for features)
- Badge collection
- Leaderboards (optional, privacy-controlled)
- Level progression
- Challenge participation

## Testing Requirements
- Achievement unlock logic tests
- Motivation score calculation tests
- Dip detection algorithm tests
- Personal best comparison tests
- Integration with other services
