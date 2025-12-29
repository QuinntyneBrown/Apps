# Goal Management - Backend Requirements

## Overview
Backend services for managing focus goals including daily targets, weekly objectives, goal tracking, and achievement management.

---

## API Endpoints

### POST /api/goals/daily
**Description**: Set a daily focus goal

**Request Body**:
```json
{
  "targetSessions": 4,
  "targetMinutes": 100,
  "date": "2024-12-28"
}
```

**Response**: `201 Created`
```json
{
  "goalId": "uuid",
  "userId": "uuid",
  "type": "daily",
  "targetSessions": 4,
  "targetMinutes": 100,
  "date": "2024-12-28",
  "currentSessions": 0,
  "currentMinutes": 0,
  "status": "active"
}
```

**Domain Event**: `DailyFocusGoalSet`

---

### POST /api/goals/weekly
**Description**: Set a weekly focus target

**Request Body**:
```json
{
  "targetSessions": 20,
  "targetMinutes": 500,
  "weekStartDate": "2024-12-23",
  "weekEndDate": "2024-12-29"
}
```

**Response**: `201 Created`
```json
{
  "goalId": "uuid",
  "userId": "uuid",
  "type": "weekly",
  "targetSessions": 20,
  "targetMinutes": 500,
  "weekStartDate": "2024-12-23",
  "weekEndDate": "2024-12-29",
  "currentSessions": 0,
  "currentMinutes": 0,
  "status": "active"
}
```

**Domain Event**: `WeeklyFocusTargetSet`

---

### PUT /api/goals/{goalId}
**Description**: Update an existing goal

**Request Body**:
```json
{
  "targetSessions": 5,
  "targetMinutes": 125
}
```

**Response**: `200 OK`

---

### DELETE /api/goals/{goalId}
**Description**: Delete a goal

**Response**: `204 No Content`

---

### GET /api/goals/daily/{date}
**Description**: Get daily goal for a specific date

**Response**: `200 OK`
```json
{
  "goalId": "uuid",
  "userId": "uuid",
  "type": "daily",
  "targetSessions": 4,
  "targetMinutes": 100,
  "date": "2024-12-28",
  "currentSessions": 2,
  "currentMinutes": 50,
  "status": "active",
  "progress": 50.0,
  "isAchieved": false
}
```

---

### GET /api/goals/weekly/current
**Description**: Get current week's goal

**Response**: `200 OK` with weekly goal object

---

### GET /api/goals/history
**Description**: Get goal achievement history

**Query Parameters**:
- `startDate`: ISO8601 date
- `endDate`: ISO8601 date
- `type`: daily | weekly
- `status`: active | achieved | failed | abandoned
- `page`: number
- `limit`: number

**Response**: `200 OK` with paginated goals

---

### GET /api/goals/stats
**Description**: Get goal achievement statistics

**Response**: `200 OK`
```json
{
  "totalGoalsSet": 30,
  "totalAchieved": 22,
  "achievementRate": 73.3,
  "currentStreak": 5,
  "longestStreak": 12,
  "averageProgress": 85.5,
  "dailyGoals": {
    "set": 21,
    "achieved": 15,
    "rate": 71.4
  },
  "weeklyGoals": {
    "set": 9,
    "achieved": 7,
    "rate": 77.8
  }
}
```

---

### POST /api/goals/{goalId}/check-achievement
**Description**: Manually trigger goal achievement check

**Response**: `200 OK`

**Domain Event**: `GoalAchieved` (if goal criteria met)

---

## Database Schema

### Goals Table
```sql
CREATE TABLE Goals (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    GoalType VARCHAR(20) NOT NULL, -- 'daily' or 'weekly'
    TargetSessions INT NOT NULL,
    TargetMinutes INT NOT NULL,
    CurrentSessions INT DEFAULT 0,
    CurrentMinutes INT DEFAULT 0,
    StartDate DATE NOT NULL,
    EndDate DATE NOT NULL,
    Status VARCHAR(20) NOT NULL, -- 'active', 'achieved', 'failed', 'abandoned'
    AchievedAt DATETIME2,
    Progress DECIMAL(5,2) DEFAULT 0.00,
    CreatedAt DATETIME2 DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2,
    CONSTRAINT CK_TargetSessions CHECK (TargetSessions > 0),
    CONSTRAINT CK_TargetMinutes CHECK (TargetMinutes > 0),
    CONSTRAINT CK_Progress CHECK (Progress >= 0 AND Progress <= 100)
);

CREATE INDEX IX_Goals_UserId_StartDate ON Goals(UserId, StartDate);
CREATE INDEX IX_Goals_UserId_Status ON Goals(UserId, Status);
```

### GoalAchievements Table
```sql
CREATE TABLE GoalAchievements (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    GoalId UNIQUEIDENTIFIER NOT NULL,
    UserId UNIQUEIDENTIFIER NOT NULL,
    GoalType VARCHAR(20) NOT NULL,
    TargetSessions INT NOT NULL,
    TargetMinutes INT NOT NULL,
    ActualSessions INT NOT NULL,
    ActualMinutes INT NOT NULL,
    AchievedAt DATETIME2 NOT NULL,
    CompletionRate DECIMAL(5,2),
    BonusPoints INT DEFAULT 0,
    FOREIGN KEY (GoalId) REFERENCES Goals(Id)
);

CREATE INDEX IX_GoalAchievements_UserId_AchievedAt ON GoalAchievements(UserId, AchievedAt);
```

### GoalStreaks Table
```sql
CREATE TABLE GoalStreaks (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    GoalType VARCHAR(20) NOT NULL,
    StreakCount INT NOT NULL,
    StartDate DATE NOT NULL,
    EndDate DATE,
    IsActive BIT DEFAULT 1,
    CreatedAt DATETIME2 DEFAULT GETUTCDATE(),
    CONSTRAINT CK_StreakCount CHECK (StreakCount > 0)
);

CREATE INDEX IX_GoalStreaks_UserId_IsActive ON GoalStreaks(UserId, IsActive);
```

---

## Domain Events

### DailyFocusGoalSet
```csharp
public record DailyFocusGoalSet(
    Guid GoalId,
    Guid UserId,
    int TargetSessions,
    int TargetMinutes,
    DateTime Date,
    DateTime Timestamp
);
```

### WeeklyFocusTargetSet
```csharp
public record WeeklyFocusTargetSet(
    Guid GoalId,
    Guid UserId,
    int TargetSessions,
    int TargetMinutes,
    DateTime WeekStartDate,
    DateTime WeekEndDate,
    DateTime Timestamp
);
```

### GoalAchieved
```csharp
public record GoalAchieved(
    Guid GoalId,
    Guid UserId,
    string GoalType,
    int TargetSessions,
    int TargetMinutes,
    int ActualSessions,
    int ActualMinutes,
    decimal CompletionRate,
    DateTime AchievedAt,
    DateTime Timestamp
);
```

### GoalProgressUpdated
```csharp
public record GoalProgressUpdated(
    Guid GoalId,
    Guid UserId,
    int CurrentSessions,
    int CurrentMinutes,
    decimal Progress,
    DateTime Timestamp
);
```

### GoalStreakAchieved
```csharp
public record GoalStreakAchieved(
    Guid UserId,
    string GoalType,
    int StreakCount,
    DateTime Timestamp
);
```

### GoalFailed
```csharp
public record GoalFailed(
    Guid GoalId,
    Guid UserId,
    string GoalType,
    int TargetSessions,
    int ActualSessions,
    decimal Progress,
    DateTime Timestamp
);
```

---

## Business Rules

1. **Goal Periods**:
   - Daily goals: Start/End date must be the same day
   - Weekly goals: Must span exactly 7 days (Monday-Sunday)

2. **Target Constraints**:
   - Minimum target sessions: 1
   - Maximum target sessions per day: 20
   - Maximum target sessions per week: 100
   - Minimum target minutes per day: 15
   - Maximum target minutes per day: 480 (8 hours)
   - Maximum target minutes per week: 2400 (40 hours)

3. **Achievement Criteria**:
   - Goal achieved when both sessions AND minutes targets are met
   - Progress calculated as: `min(sessionsProgress, minutesProgress)`
   - Bonus points awarded for exceeding targets

4. **Concurrent Goals**:
   - Only one active daily goal per date allowed
   - Only one active weekly goal per week allowed
   - Cannot create goal for past dates

5. **Status Transitions**:
   - Active -> Achieved: When targets met
   - Active -> Failed: When period ends without meeting targets
   - Active -> Abandoned: When user deletes goal
   - Cannot modify achieved/failed goals

6. **Streak Calculation**:
   - Consecutive days/weeks with achieved goals
   - Broken when a goal period passes without achievement
   - Maintained across different goal types separately

7. **Progress Updates**:
   - Automatically updated when SessionCompleted event received
   - Achievement check triggered after each progress update
   - Cannot manually set progress

8. **Bonus Points**:
   - 100% completion: 10 points
   - 110% completion: 25 points
   - 125% completion: 50 points
   - 150%+ completion: 100 points

---

## Integration Points

- **Session Service**: Subscribe to SessionCompleted events for progress updates
- **Analytics Service**: Provide goal stats for dashboards
- **Notification Service**: Send alerts for goal achievements and failures
- **Gamification Service**: Award badges and points for streaks
- **Calendar Service**: Display goals on calendar views

---

## Event Handlers

### On SessionCompleted
1. Find active goals for session date
2. Increment currentSessions by 1
3. Increment currentMinutes by session.actualDuration
4. Calculate new progress percentage
5. Publish GoalProgressUpdated event
6. Check achievement criteria
7. If achieved, publish GoalAchieved event
8. Check and update streaks

### On DayEnded (Scheduled Job)
1. Find all active daily goals for yesterday
2. Check achievement status
3. Update status to 'achieved' or 'failed'
4. Update streak records
5. Publish appropriate events

### On WeekEnded (Scheduled Job)
1. Find all active weekly goals for last week
2. Check achievement status
3. Update status to 'achieved' or 'failed'
4. Update streak records
5. Publish appropriate events
