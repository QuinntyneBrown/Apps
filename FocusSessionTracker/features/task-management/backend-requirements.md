# Task Management - Backend Requirements

## Overview
Backend services for managing tasks that can be assigned to focus sessions, tracking progress across multiple sessions, and marking tasks as completed.

---

## API Endpoints

### POST /api/tasks
**Description**: Create a new task

**Request Body**:
```json
{
  "title": "Complete project documentation",
  "description": "Write comprehensive docs for the new feature",
  "projectId": "uuid | null",
  "estimatedSessions": 3,
  "priority": "high | medium | low",
  "dueDate": "ISO8601 | null",
  "tags": ["documentation", "feature"]
}
```

**Response**: `201 Created`
```json
{
  "taskId": "uuid",
  "userId": "uuid",
  "title": "Complete project documentation",
  "description": "Write comprehensive docs for the new feature",
  "status": "not_started",
  "progressPercentage": 0,
  "sessionsCompleted": 0,
  "estimatedSessions": 3,
  "priority": "high",
  "createdAt": "ISO8601"
}
```

**Domain Event**: `TaskCreated`

---

### PUT /api/tasks/{taskId}
**Description**: Update task details

**Request Body**:
```json
{
  "title": "Updated title",
  "description": "Updated description",
  "estimatedSessions": 4,
  "priority": "medium",
  "dueDate": "ISO8601 | null",
  "tags": ["documentation"]
}
```

**Response**: `200 OK`

**Domain Event**: `TaskUpdated`

---

### POST /api/tasks/{taskId}/assign-to-session
**Description**: Assign task to a focus session

**Request Body**:
```json
{
  "sessionId": "uuid"
}
```

**Response**: `200 OK`
```json
{
  "taskId": "uuid",
  "sessionId": "uuid",
  "assignedAt": "ISO8601"
}
```

**Domain Event**: `TaskAssignedToSession`

---

### PUT /api/tasks/{taskId}/update-progress
**Description**: Update task progress during or after a session

**Request Body**:
```json
{
  "sessionId": "uuid",
  "progressPercentage": 65,
  "progressNotes": "Completed sections 1-3, started section 4",
  "timeSpent": 25
}
```

**Response**: `200 OK`
```json
{
  "taskId": "uuid",
  "progressPercentage": 65,
  "sessionsCompleted": 2,
  "totalTimeSpent": 75,
  "status": "in_progress"
}
```

**Domain Event**: `TaskProgressUpdated`

---

### PUT /api/tasks/{taskId}/complete
**Description**: Mark a multi-session task as completed

**Request Body**:
```json
{
  "sessionId": "uuid",
  "completionNotes": "All documentation sections finished and reviewed"
}
```

**Response**: `200 OK`
```json
{
  "taskId": "uuid",
  "status": "completed",
  "completedAt": "ISO8601",
  "totalSessions": 3,
  "totalTimeSpent": 90
}
```

**Domain Event**: `MultipleSessionTaskCompleted`

---

### PUT /api/tasks/{taskId}/archive
**Description**: Archive a completed or cancelled task

**Response**: `200 OK`

**Domain Event**: `TaskArchived`

---

### GET /api/tasks
**Description**: Get user's tasks with filtering

**Query Parameters**:
- `status`: not_started | in_progress | completed | archived
- `priority`: high | medium | low
- `projectId`: uuid
- `tags`: comma-separated tags
- `sortBy`: createdAt | dueDate | priority | progress
- `page`: number
- `limit`: number

**Response**: `200 OK` with paginated tasks

---

### GET /api/tasks/{taskId}
**Description**: Get task details including session history

**Response**: `200 OK`
```json
{
  "taskId": "uuid",
  "title": "Complete project documentation",
  "description": "Write comprehensive docs",
  "status": "in_progress",
  "progressPercentage": 65,
  "sessionsCompleted": 2,
  "estimatedSessions": 3,
  "totalTimeSpent": 75,
  "priority": "high",
  "dueDate": "ISO8601",
  "tags": ["documentation", "feature"],
  "sessions": [
    {
      "sessionId": "uuid",
      "startTime": "ISO8601",
      "duration": 25,
      "progressMade": "Completed introduction"
    }
  ],
  "createdAt": "ISO8601",
  "updatedAt": "ISO8601"
}
```

---

### GET /api/tasks/{taskId}/sessions
**Description**: Get all sessions associated with a task

**Response**: `200 OK` with list of sessions

---

## Database Schema

### Tasks Table
```sql
CREATE TABLE Tasks (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    Title NVARCHAR(200) NOT NULL,
    Description NVARCHAR(MAX),
    Status VARCHAR(20) NOT NULL,
    ProgressPercentage INT DEFAULT 0,
    SessionsCompleted INT DEFAULT 0,
    EstimatedSessions INT,
    TotalTimeSpent INT DEFAULT 0,
    Priority VARCHAR(20) NOT NULL,
    DueDate DATETIME2,
    CompletedAt DATETIME2,
    ArchivedAt DATETIME2,
    ProjectId UNIQUEIDENTIFIER,
    CreatedAt DATETIME2 DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2,
    CONSTRAINT CHK_ProgressPercentage CHECK (ProgressPercentage BETWEEN 0 AND 100),
    CONSTRAINT CHK_Status CHECK (Status IN ('not_started', 'in_progress', 'completed', 'archived')),
    CONSTRAINT CHK_Priority CHECK (Priority IN ('high', 'medium', 'low'))
);
```

### TaskTags Table
```sql
CREATE TABLE TaskTags (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    TaskId UNIQUEIDENTIFIER NOT NULL,
    Tag NVARCHAR(50) NOT NULL,
    FOREIGN KEY (TaskId) REFERENCES Tasks(Id) ON DELETE CASCADE
);
```

### TaskSessions Table (Junction Table)
```sql
CREATE TABLE TaskSessions (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    TaskId UNIQUEIDENTIFIER NOT NULL,
    SessionId UNIQUEIDENTIFIER NOT NULL,
    AssignedAt DATETIME2 DEFAULT GETUTCDATE(),
    ProgressNotes NVARCHAR(MAX),
    TimeSpent INT,
    ProgressBefore INT,
    ProgressAfter INT,
    FOREIGN KEY (TaskId) REFERENCES Tasks(Id),
    FOREIGN KEY (SessionId) REFERENCES Sessions(Id)
);
```

### TaskProgressHistory Table
```sql
CREATE TABLE TaskProgressHistory (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    TaskId UNIQUEIDENTIFIER NOT NULL,
    SessionId UNIQUEIDENTIFIER,
    ProgressPercentage INT NOT NULL,
    ProgressNotes NVARCHAR(MAX),
    RecordedAt DATETIME2 DEFAULT GETUTCDATE(),
    FOREIGN KEY (TaskId) REFERENCES Tasks(Id)
);
```

---

## Domain Events

### TaskCreated
```csharp
public record TaskCreated(
    Guid TaskId,
    Guid UserId,
    string Title,
    string? Description,
    int? EstimatedSessions,
    string Priority,
    DateTime? DueDate,
    DateTime Timestamp
);
```

### TaskUpdated
```csharp
public record TaskUpdated(
    Guid TaskId,
    string Title,
    string? Description,
    int? EstimatedSessions,
    string Priority,
    DateTime? DueDate,
    DateTime Timestamp
);
```

### TaskAssignedToSession
```csharp
public record TaskAssignedToSession(
    Guid TaskId,
    Guid SessionId,
    Guid UserId,
    string TaskTitle,
    int CurrentProgress,
    DateTime Timestamp
);
```

### TaskProgressUpdated
```csharp
public record TaskProgressUpdated(
    Guid TaskId,
    Guid SessionId,
    int ProgressPercentage,
    int SessionsCompleted,
    int TotalTimeSpent,
    string? ProgressNotes,
    string Status,
    DateTime Timestamp
);
```

### MultipleSessionTaskCompleted
```csharp
public record MultipleSessionTaskCompleted(
    Guid TaskId,
    Guid UserId,
    string TaskTitle,
    int TotalSessions,
    int TotalTimeSpent,
    DateTime CompletedAt,
    DateTime Timestamp
);
```

### TaskArchived
```csharp
public record TaskArchived(
    Guid TaskId,
    string Status,
    DateTime ArchivedAt,
    DateTime Timestamp
);
```

---

## Business Rules

1. **Task Title**: Required, 3-200 characters
2. **Progress Percentage**: Must be 0-100, only updated through sessions
3. **Estimated Sessions**: Minimum 1, maximum 100 (optional)
4. **Status Transitions**:
   - not_started → in_progress (on first session assignment)
   - in_progress → completed (when progress reaches 100%)
   - Any status → archived (manual archive)
5. **Session Assignment**: A task can be assigned to multiple sessions over time
6. **Progress Calculation**: Automatic based on session completions or manual updates
7. **Completion**: Task automatically marked completed when progress reaches 100%
8. **Priority Levels**: high (red), medium (yellow), low (green)
9. **Due Date**: Optional, warning shown when approaching
10. **Tags**: Maximum 10 tags per task, each 2-50 characters

---

## Integration Points

- **Session Service**: Automatic task assignment when starting sessions
- **Notification Service**: Due date reminders, completion celebrations
- **Analytics Service**: Task completion rates, time estimates vs actuals
- **Project Service**: Task grouping and project-level progress tracking
- **Calendar Service**: Due date integration and scheduling

---

## Validation Rules

| Field | Rule |
|-------|------|
| Title | Required, 3-200 characters, no special chars |
| Description | Max 2000 characters |
| EstimatedSessions | 1-100 if provided |
| ProgressPercentage | 0-100, read-only for API clients |
| Priority | Must be: high, medium, or low |
| Tags | Max 10 tags, each 2-50 chars |
| DueDate | Must be future date |

---

## Error Codes

| Code | Message | HTTP Status |
|------|---------|-------------|
| TASK_NOT_FOUND | Task not found | 404 |
| TASK_ALREADY_COMPLETED | Task is already completed | 400 |
| TASK_ARCHIVED | Cannot modify archived task | 400 |
| INVALID_PROGRESS | Progress must be 0-100 | 400 |
| SESSION_NOT_FOUND | Session not found | 404 |
| INVALID_STATUS_TRANSITION | Invalid status transition | 400 |
| DUPLICATE_TITLE | Task title already exists | 409 |
