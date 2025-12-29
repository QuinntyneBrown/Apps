# Break Management - Backend Requirements

## Overview
Backend services for managing breaks between focus sessions including scheduling, tracking, extending, and logging break activities.

---

## API Endpoints

### POST /api/breaks
**Description**: Start a new break

**Request Body**:
```json
{
  "sessionId": "uuid | null",
  "breakType": "short | long | custom",
  "plannedDuration": 5,
  "isScheduled": true
}
```

**Response**: `201 Created`
```json
{
  "breakId": "uuid",
  "userId": "uuid",
  "sessionId": "uuid",
  "breakType": "short",
  "plannedDuration": 5,
  "startTime": "ISO8601",
  "status": "active"
}
```

**Domain Event**: `BreakStarted`

---

### PUT /api/breaks/{breakId}/extend
**Description**: Extend break duration beyond planned time

**Request Body**:
```json
{
  "additionalMinutes": 5,
  "reason": "Need more rest"
}
```

**Response**: `200 OK`

**Domain Event**: `BreakExtended`

---

### PUT /api/breaks/{breakId}/activity
**Description**: Log activity performed during break

**Request Body**:
```json
{
  "activity": "walk | coffee | meditation | snack | other",
  "notes": "Took a walk around the office",
  "effectiveness": 4
}
```

**Response**: `200 OK`

**Domain Event**: `BreakActivityLogged`

---

### PUT /api/breaks/{breakId}/complete
**Description**: End a break and return to work

**Response**: `200 OK`

**Domain Event**: `BreakCompleted`

---

### POST /api/breaks/skip
**Description**: Skip a recommended break

**Request Body**:
```json
{
  "sessionId": "uuid",
  "skipReason": "In flow state, want to continue"
}
```

**Response**: `200 OK`

**Domain Event**: `BreakSkipped`

---

### GET /api/breaks
**Description**: Get user's break history with filtering

**Query Parameters**:
- `startDate`: ISO8601 date
- `endDate`: ISO8601 date
- `breakType`: short | long | custom
- `page`: number
- `limit`: number

**Response**: `200 OK` with paginated breaks

---

### GET /api/breaks/{breakId}
**Description**: Get break details

**Response**: `200 OK` with break object

---

### GET /api/breaks/recommendations
**Description**: Get personalized break recommendations based on session history

**Response**: `200 OK`
```json
{
  "recommendedBreakType": "short",
  "recommendedDuration": 5,
  "reason": "After 2 consecutive pomodoros",
  "nextBreakIn": 25,
  "breakPattern": "pomodoro"
}
```

---

## Database Schema

### Breaks Table
```sql
CREATE TABLE Breaks (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    SessionId UNIQUEIDENTIFIER,
    BreakType VARCHAR(20) NOT NULL,
    PlannedDuration INT NOT NULL,
    ActualDuration INT,
    StartTime DATETIME2 NOT NULL,
    EndTime DATETIME2,
    Status VARCHAR(20) NOT NULL,
    IsScheduled BIT DEFAULT 0,
    WasExtended BIT DEFAULT 0,
    ExtensionMinutes INT DEFAULT 0,
    ExtensionReason NVARCHAR(500),
    SkipReason NVARCHAR(500),
    CreatedAt DATETIME2 DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2,
    FOREIGN KEY (SessionId) REFERENCES Sessions(Id)
);
```

### BreakActivities Table
```sql
CREATE TABLE BreakActivities (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    BreakId UNIQUEIDENTIFIER NOT NULL,
    Activity VARCHAR(50) NOT NULL,
    Notes NVARCHAR(1000),
    Effectiveness INT,
    LoggedAt DATETIME2 DEFAULT GETUTCDATE(),
    FOREIGN KEY (BreakId) REFERENCES Breaks(Id)
);
```

### BreakPatterns Table
```sql
CREATE TABLE BreakPatterns (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    PatternName VARCHAR(50) NOT NULL,
    SessionsBeforeBreak INT NOT NULL,
    BreakDuration INT NOT NULL,
    IsActive BIT DEFAULT 1,
    CreatedAt DATETIME2 DEFAULT GETUTCDATE()
);
```

---

## Domain Events

### BreakStarted
```csharp
public record BreakStarted(
    Guid BreakId,
    Guid UserId,
    Guid? SessionId,
    string BreakType,
    int PlannedDuration,
    bool IsScheduled,
    DateTime StartTime,
    DateTime Timestamp
);
```

### BreakActivityLogged
```csharp
public record BreakActivityLogged(
    Guid BreakId,
    Guid ActivityId,
    string Activity,
    string? Notes,
    int? Effectiveness,
    DateTime Timestamp
);
```

### BreakExtended
```csharp
public record BreakExtended(
    Guid BreakId,
    int OriginalDuration,
    int AdditionalMinutes,
    int NewTotalDuration,
    string? Reason,
    DateTime Timestamp
);
```

### BreakSkipped
```csharp
public record BreakSkipped(
    Guid UserId,
    Guid? SessionId,
    string? SkipReason,
    DateTime SkippedAt,
    DateTime Timestamp
);
```

### BreakCompleted
```csharp
public record BreakCompleted(
    Guid BreakId,
    int ActualDuration,
    bool WasExtended,
    int ActivityCount,
    DateTime Timestamp
);
```

---

## Business Rules

1. **Break Duration**: Minimum 1 minute, maximum 60 minutes
2. **Break Types**:
   - "short" (5min) - after 1-2 sessions
   - "long" (15min) - after 3-4 sessions
   - "custom" - user-defined duration
3. **Extension Limits**: Max 3 extensions per break, max 15 minutes total extension
4. **Skip Tracking**: Track consecutive skips to warn about burnout risk
5. **Effectiveness Rating**: 1-5 scale if provided
6. **Recommended Pattern**: Pomodoro (25min work, 5min break, 15min after 4 sessions)
7. **Break Reminders**: Automatic prompts after session completion
8. **Activity Tracking**: Optional, can log multiple activities per break

---

## Integration Points

- **Session Service**: Trigger break recommendations after session completion
- **Notification Service**: Break reminders and completion alerts
- **Analytics Service**: Break effectiveness analysis, pattern detection
- **Timer Service**: Break countdown management
- **Health Service**: Activity tracking and wellness insights

---

## Validation Rules

| Field | Rule |
|-------|------|
| PlannedDuration | 1-60 minutes |
| Effectiveness | 1-5 (optional) |
| ExtensionMinutes | 1-15 minutes per extension |
| Activity Notes | Max 1000 characters |
| Skip Reason | Max 500 characters |

---

## Health & Wellness Features

1. **Break Streak Tracking**: Count consecutive days with healthy break patterns
2. **Burnout Detection**: Alert if user skips 5+ consecutive recommended breaks
3. **Activity Suggestions**: Recommend break activities based on effectiveness history
4. **Hydration Reminders**: Prompt to drink water during breaks
5. **Movement Tracking**: Encourage physical activity during longer breaks
