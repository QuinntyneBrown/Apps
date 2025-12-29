# Productivity Analytics - Backend Requirements

## Overview
Backend services for tracking and analyzing productivity metrics including high-focus sessions, streaks, deep work accumulation, and achievement milestones.

---

## API Endpoints

### GET /api/analytics/productivity/dashboard
**Description**: Get productivity analytics dashboard data

**Query Parameters**:
- `startDate`: ISO8601 date (default: 30 days ago)
- `endDate`: ISO8601 date (default: today)

**Response**: `200 OK`
```json
{
  "period": {
    "startDate": "2024-01-01",
    "endDate": "2024-01-31"
  },
  "metrics": {
    "totalSessions": 45,
    "totalFocusTime": 1125,
    "averageFocusScore": 85,
    "highQualitySessions": 32,
    "currentStreak": 7,
    "longestStreak": 12,
    "totalDeepWorkTime": 780,
    "weeklyAverage": 11.25
  },
  "trends": {
    "focusScoreTrend": "increasing",
    "sessionCountTrend": "stable",
    "focusTimeTrend": "increasing"
  }
}
```

---

### GET /api/analytics/productivity/achievements
**Description**: Get user's productivity achievements

**Query Parameters**:
- `category`: high-focus | streak | deep-work | all
- `achieved`: true | false | all
- `page`: number
- `limit`: number

**Response**: `200 OK`
```json
{
  "achievements": [
    {
      "id": "uuid",
      "category": "high-focus",
      "name": "Perfect Focus",
      "description": "Complete a session with 100 focus score",
      "achieved": true,
      "achievedAt": "2024-01-15T10:30:00Z",
      "metadata": {
        "sessionId": "uuid",
        "focusScore": 100
      }
    }
  ],
  "totalCount": 15,
  "achievedCount": 8
}
```

---

### GET /api/analytics/productivity/streaks
**Description**: Get focus streak history

**Query Parameters**:
- `status`: active | completed | all
- `minLength`: number (minimum streak length)

**Response**: `200 OK`
```json
{
  "currentStreak": {
    "startDate": "2024-01-15",
    "length": 7,
    "status": "active",
    "milestones": [3, 5, 7]
  },
  "streakHistory": [
    {
      "id": "uuid",
      "startDate": "2024-01-01",
      "endDate": "2024-01-12",
      "length": 12,
      "status": "completed",
      "milestonesReached": [3, 5, 7, 10]
    }
  ]
}
```

---

### GET /api/analytics/productivity/deep-work
**Description**: Get deep work statistics

**Query Parameters**:
- `groupBy`: day | week | month
- `startDate`: ISO8601 date
- `endDate`: ISO8601 date

**Response**: `200 OK`
```json
{
  "totalDeepWorkTime": 780,
  "deepWorkSessions": 26,
  "averageDeepWorkDuration": 30,
  "thresholdsReached": [
    {
      "threshold": 500,
      "reachedAt": "2024-01-20T15:30:00Z"
    }
  ],
  "breakdown": [
    {
      "period": "2024-01-15",
      "deepWorkTime": 120,
      "sessionCount": 4
    }
  ]
}
```

---

### GET /api/analytics/productivity/trends
**Description**: Get productivity trends over time

**Query Parameters**:
- `metric`: focus-score | session-count | focus-time | quality-rating
- `period`: week | month | quarter | year
- `groupBy`: day | week | month

**Response**: `200 OK`
```json
{
  "metric": "focus-score",
  "period": "month",
  "dataPoints": [
    {
      "date": "2024-01-01",
      "value": 78,
      "sessionCount": 4
    },
    {
      "date": "2024-01-02",
      "value": 82,
      "sessionCount": 5
    }
  ],
  "statistics": {
    "mean": 85,
    "median": 86,
    "min": 62,
    "max": 98,
    "stdDev": 8.5
  }
}
```

---

### POST /api/analytics/productivity/export
**Description**: Export productivity data

**Request Body**:
```json
{
  "format": "csv | json | pdf",
  "startDate": "2024-01-01",
  "endDate": "2024-01-31",
  "includeCharts": true
}
```

**Response**: `200 OK` with file download

---

## Database Schema

### ProductivityMetrics Table
```sql
CREATE TABLE ProductivityMetrics (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    Date DATE NOT NULL,
    TotalSessions INT DEFAULT 0,
    TotalFocusTime INT DEFAULT 0,
    AverageFocusScore DECIMAL(5,2),
    HighQualitySessions INT DEFAULT 0,
    DeepWorkTime INT DEFAULT 0,
    CreatedAt DATETIME2 DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2,
    UNIQUE(UserId, Date)
);
```

### FocusStreaks Table
```sql
CREATE TABLE FocusStreaks (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    StartDate DATE NOT NULL,
    EndDate DATE,
    Length INT NOT NULL,
    Status VARCHAR(20) NOT NULL,
    HighestMilestone INT,
    CreatedAt DATETIME2 DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2
);
```

### StreakMilestones Table
```sql
CREATE TABLE StreakMilestones (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    StreakId UNIQUEIDENTIFIER NOT NULL,
    MilestoneValue INT NOT NULL,
    ReachedAt DATETIME2 NOT NULL,
    FOREIGN KEY (StreakId) REFERENCES FocusStreaks(Id)
);
```

### HighFocusSessions Table
```sql
CREATE TABLE HighFocusSessions (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    SessionId UNIQUEIDENTIFIER NOT NULL,
    FocusScore INT NOT NULL,
    Duration INT NOT NULL,
    AchievedAt DATETIME2 NOT NULL,
    FOREIGN KEY (SessionId) REFERENCES Sessions(Id)
);
```

### DeepWorkThresholds Table
```sql
CREATE TABLE DeepWorkThresholds (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    ThresholdMinutes INT NOT NULL,
    ReachedAt DATETIME2 NOT NULL,
    AccumulatedTime INT NOT NULL,
    PeriodStart DATE,
    PeriodEnd DATE
);
```

### Achievements Table
```sql
CREATE TABLE Achievements (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    Category VARCHAR(50) NOT NULL,
    Name NVARCHAR(200) NOT NULL,
    Description NVARCHAR(500),
    AchievedAt DATETIME2,
    Metadata NVARCHAR(MAX),
    CreatedAt DATETIME2 DEFAULT GETUTCDATE()
);
```

---

## Domain Events

### HighFocusSessionAchieved
```csharp
public record HighFocusSessionAchieved(
    Guid SessionId,
    Guid UserId,
    int FocusScore,
    int Duration,
    int QualityRating,
    DateTime AchievedAt,
    DateTime Timestamp
);
```

### FocusStreakStarted
```csharp
public record FocusStreakStarted(
    Guid StreakId,
    Guid UserId,
    DateTime StartDate,
    DateTime Timestamp
);
```

### FocusStreakMilestone
```csharp
public record FocusStreakMilestone(
    Guid StreakId,
    Guid UserId,
    int MilestoneValue,
    int CurrentStreakLength,
    DateTime ReachedAt,
    DateTime Timestamp
);
```

### DeepWorkThresholdReached
```csharp
public record DeepWorkThresholdReached(
    Guid UserId,
    int ThresholdMinutes,
    int AccumulatedMinutes,
    DateTime PeriodStart,
    DateTime PeriodEnd,
    DateTime ReachedAt,
    DateTime Timestamp
);
```

---

## Business Rules

### High Focus Session Criteria
1. **Minimum Focus Score**: 85 or higher
2. **Minimum Duration**: 20 minutes
3. **Quality Rating**: 4 stars or higher
4. **Maximum Pauses**: 1 pause allowed
5. **Maximum Distractions**: 2 or fewer distractions

### Focus Streak Rules
1. **Streak Definition**: Consecutive days with at least 1 completed session
2. **Streak Milestones**: 3, 5, 7, 14, 21, 30, 60, 90, 180, 365 days
3. **Streak Break**: Missing a day ends the current streak
4. **Grace Period**: None (strict daily requirement)
5. **Minimum Session Quality**: Any completed session counts

### Deep Work Thresholds
1. **Deep Work Session**: Sessions >= 25 minutes with focus score >= 80
2. **Accumulation Period**: Rolling 7-day window
3. **Threshold Levels**: 300, 500, 750, 1000, 1500, 2000 minutes
4. **Reset Period**: Weekly on Sunday midnight
5. **Carry Forward**: No - fresh start each period

### Productivity Metrics Calculation
1. **Focus Time**: Sum of all completed session durations
2. **High Quality Sessions**: Sessions with quality rating >= 4 and focus score >= 80
3. **Average Focus Score**: Mean of all completed sessions
4. **Deep Work Time**: Sum of sessions >= 25 min with score >= 80
5. **Weekly Average**: Total focus time / number of weeks in period

---

## Event Handlers

### SessionCompleted Event Handler
- Check if session qualifies as high-focus session
- Update daily productivity metrics
- Check if deep work threshold reached
- Update current streak if applicable

### HighFocusSessionAchieved Event Handler
- Store high-focus session record
- Check for related achievements
- Update user statistics
- Send notification if enabled

### FocusStreakMilestone Event Handler
- Record milestone achievement
- Create achievement record
- Send celebration notification
- Update user profile badges

### DeepWorkThresholdReached Event Handler
- Record threshold achievement
- Update achievement system
- Generate analytics insights
- Send motivational notification

---

## Integration Points

- **Session Service**: Subscribe to SessionCompleted events
- **Notification Service**: Send achievement notifications
- **Analytics Engine**: Calculate trends and patterns
- **Reporting Service**: Generate productivity reports
- **Badge Service**: Award achievement badges

---

## Performance Considerations

1. **Metric Aggregation**: Daily batch job at midnight UTC
2. **Streak Calculation**: Real-time update on session completion
3. **Trend Analysis**: Pre-computed for common date ranges
4. **Caching Strategy**: Cache dashboard data for 5 minutes
5. **Export Limits**: Maximum 1 year of data per export

---

## Data Retention

- **Productivity Metrics**: Retain indefinitely
- **Achievement Records**: Retain indefinitely
- **Streak History**: Retain for 2 years
- **High-Focus Sessions**: Retain for 1 year
- **Threshold Records**: Retain for 1 year
