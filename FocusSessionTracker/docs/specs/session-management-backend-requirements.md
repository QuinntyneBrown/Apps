# Session Management - Backend Requirements

## Overview
Backend services for managing focus sessions including creation, completion, abandonment, pause, and resume operations.

---

## API Endpoints

### POST /api/sessions
**Description**: Start a new focus session

**Request Body**:
```json
{
  "sessionType": "pomodoro | custom",
  "plannedDuration": 25,
  "taskId": "uuid | null",
  "projectId": "uuid | null"
}
```

**Response**: `201 Created`
```json
{
  "sessionId": "uuid",
  "userId": "uuid",
  "sessionType": "pomodoro",
  "plannedDuration": 25,
  "startTime": "ISO8601",
  "status": "active"
}
```

**Domain Event**: `FocusSessionStarted`

---

### PUT /api/sessions/{sessionId}/complete
**Description**: Mark session as completed

**Request Body**:
```json
{
  "qualityRating": 4,
  "taskProgress": "Description of progress made",
  "focusScore": 85
}
```

**Response**: `200 OK`

**Domain Event**: `SessionCompleted`

---

### PUT /api/sessions/{sessionId}/abandon
**Description**: Abandon a session early

**Request Body**:
```json
{
  "reason": "Emergency interruption",
  "progressMade": "Completed initial draft"
}
```

**Response**: `200 OK`

**Domain Event**: `SessionAbandoned`

---

### PUT /api/sessions/{sessionId}/pause
**Description**: Pause an active session

**Request Body**:
```json
{
  "pauseReason": "Quick phone call"
}
```

**Response**: `200 OK`

**Domain Event**: `SessionPaused`

---

### PUT /api/sessions/{sessionId}/resume
**Description**: Resume a paused session

**Response**: `200 OK`

**Domain Event**: `SessionResumed`

---

### GET /api/sessions
**Description**: Get user's sessions with filtering

**Query Parameters**:
- `startDate`: ISO8601 date
- `endDate`: ISO8601 date
- `status`: active | completed | abandoned
- `page`: number
- `limit`: number

**Response**: `200 OK` with paginated sessions

---

### GET /api/sessions/{sessionId}
**Description**: Get session details

**Response**: `200 OK` with session object

---

## Database Schema

### Sessions Table
```sql
CREATE TABLE Sessions (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    SessionType VARCHAR(20) NOT NULL,
    PlannedDuration INT NOT NULL,
    ActualDuration INT,
    StartTime DATETIME2 NOT NULL,
    EndTime DATETIME2,
    Status VARCHAR(20) NOT NULL,
    TaskId UNIQUEIDENTIFIER,
    ProjectId UNIQUEIDENTIFIER,
    QualityRating INT,
    FocusScore INT,
    AbandonReason NVARCHAR(500),
    ProgressMade NVARCHAR(MAX),
    CreatedAt DATETIME2 DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2
);
```

### SessionPauses Table
```sql
CREATE TABLE SessionPauses (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    SessionId UNIQUEIDENTIFIER NOT NULL,
    PauseTime DATETIME2 NOT NULL,
    ResumeTime DATETIME2,
    PauseReason NVARCHAR(500),
    Duration INT,
    FOREIGN KEY (SessionId) REFERENCES Sessions(Id)
);
```

---

## Domain Events

### FocusSessionStarted
```csharp
public record FocusSessionStarted(
    Guid SessionId,
    Guid UserId,
    string SessionType,
    int PlannedDuration,
    Guid? TaskId,
    Guid? ProjectId,
    DateTime StartTime,
    DateTime Timestamp
);
```

### SessionCompleted
```csharp
public record SessionCompleted(
    Guid SessionId,
    int ActualDuration,
    int QualityRating,
    string TaskProgress,
    int FocusScore,
    DateTime Timestamp
);
```

### SessionAbandoned
```csharp
public record SessionAbandoned(
    Guid SessionId,
    int PlannedDuration,
    int ActualDuration,
    string? AbandonReason,
    string? ProgressMade,
    DateTime Timestamp
);
```

### SessionPaused
```csharp
public record SessionPaused(
    Guid SessionId,
    DateTime PauseTime,
    string? PauseReason,
    DateTime Timestamp
);
```

### SessionResumed
```csharp
public record SessionResumed(
    Guid SessionId,
    DateTime ResumeTime,
    int PauseDuration,
    DateTime Timestamp
);
```

---

## Business Rules

1. **Session Duration**: Minimum 5 minutes, maximum 120 minutes
2. **Concurrent Sessions**: Only one active session per user allowed
3. **Pause Limits**: Maximum 3 pauses per session, max 10 minutes each
4. **Quality Rating**: Must be 1-5 if provided
5. **Focus Score**: Calculated as: `base_score - (distractions * 5) + (duration_bonus)`
6. **Session Types**: "pomodoro" (25min), "short" (15min), "long" (45min), "custom"

---

## Integration Points

- **Timer Service**: Real-time countdown management
- **Notification Service**: Session end alerts
- **Analytics Service**: Focus score calculation
- **Task Service**: Task progress updates
