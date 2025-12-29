# Backend Requirements - Sleep Goals Management

## API Endpoints

### POST /api/sleep-goals
Create or update sleep goal
- **Request Body**:
  - targetHours (decimal, required)
  - targetBedtime (TimeSpan, required)
  - targetWakeTime (TimeSpan, required)
  - userId (string, required)
- **Response**: 201 Created
- **Events**: Publishes `SleepGoalSet`

### GET /api/sleep-goals/{userId}
Get current sleep goal
- **Response**: 200 OK with goal details

### GET /api/sleep-goals/{userId}/achievements
Get goal achievement history
- **Query Parameters**: startDate, endDate
- **Response**: 200 OK with achievements list

### POST /api/sleep-goals/{userId}/check
Check if goal met for specific session
- **Request Body**: sessionId
- **Response**: 200 OK with achievement status
- **Events**: Publishes `SleepGoalMet` or `SleepGoalMissed`

## Domain Models

### SleepGoal
```
- Id: Guid
- UserId: Guid
- TargetHours: decimal
- TargetBedtime: TimeSpan
- TargetWakeTime: TimeSpan
- CreatedAt: DateTime
- UpdatedAt: DateTime
```

### GoalAchievement
```
- Id: Guid
- UserId: Guid
- SessionId: Guid
- GoalHours: decimal
- ActualHours: decimal
- AchievementDate: DateTime
- Status: enum (Met, Missed)
- Shortfall: decimal (if missed)
```

### ConsistencyStreak
```
- Id: Guid
- UserId: Guid
- StreakType: enum (SleepDuration, Bedtime, WakeTime, Schedule)
- StreakLength: int
- StartDate: DateTime
- EndDate: DateTime
- IsActive: bool
```

## Business Logic

### Goal Achievement Check
- Compare actual sleep duration with target hours
- Tolerance: ±15 minutes
- Check after each sleep session completion
- Update streak counters

### Consistency Detection
- Calculate bedtime variance over rolling 7-day window
- Variance threshold: 30 minutes
- Check wake time consistency
- Publish `ConsistentSleepScheduleAchieved` when 7+ days consistent

### Streak Management
- Increment streak on consecutive goal achievements
- Reset streak on missed goal
- Track separate streaks for duration, bedtime, wake time

## Event Publishing

### SleepGoalSet, SleepGoalMet, SleepGoalMissed, ConsistentSleepScheduleAchieved
Events include userId, goal data, achievement metrics, timestamps

## Database Schema

### SleepGoals Table
- Id, UserId (indexed), TargetHours, TargetBedtime, TargetWakeTime, CreatedAt, UpdatedAt

### GoalAchievements Table
- Id, UserId (indexed), SessionId, GoalHours, ActualHours, AchievementDate (indexed), Status, Shortfall

### ConsistencyStreaks Table
- Id, UserId (indexed), StreakType, StreakLength, StartDate, EndDate, IsActive

## Performance Requirements
- Goal check: < 100ms
- Streak calculation: < 500ms
- Achievement query: < 200ms
