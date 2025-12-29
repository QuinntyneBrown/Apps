# Work Session Tracking - Backend Requirements

## Overview
API for logging individual work sessions, tasks, problems, and rework throughout the restoration process.

## Domain Events
- **WorkSessionStarted**: Triggered when work session begins
- **TaskCompleted**: Triggered when a task is finished
- **WorkSessionCompleted**: Triggered when work session ends
- **ProblemDiscovered**: Triggered when unexpected issue found
- **ReworkRequired**: Triggered when work must be redone

## API Endpoints

### Work Sessions
- `POST /api/projects/{id}/sessions` - Start work session
- `GET /api/projects/{id}/sessions` - List project sessions
- `GET /api/sessions/{sessionId}` - Get session details
- `PUT /api/sessions/{sessionId}` - Update session
- `POST /api/sessions/{sessionId}/complete` - End session
- `DELETE /api/sessions/{sessionId}` - Delete session

### Tasks
- `POST /api/sessions/{sessionId}/tasks` - Add task to session
- `GET /api/sessions/{sessionId}/tasks` - List session tasks
- `PUT /api/tasks/{taskId}` - Update task
- `POST /api/tasks/{taskId}/complete` - Mark task complete
- `DELETE /api/tasks/{taskId}` - Delete task

### Problems
- `POST /api/projects/{id}/problems` - Report problem
- `GET /api/projects/{id}/problems` - List project problems
- `PUT /api/problems/{problemId}` - Update problem
- `POST /api/problems/{problemId}/resolve` - Mark resolved

### Analytics
- `GET /api/projects/{id}/sessions/analytics` - Session statistics
- `GET /api/projects/{id}/time-tracking` - Time spent breakdown

## Data Models

### WorkSession
```csharp
{
    "id": "guid",
    "projectId": "guid",
    "startTime": "datetime",
    "endTime": "datetime?",
    "plannedTasks": "array<string>",
    "toolsNeeded": "array<string>",
    "partsToInstall": "array<string>",
    "helperPresent": "boolean",
    "notes": "string",
    "status": "enum[InProgress, Completed]"
}
```

### Task
```csharp
{
    "id": "guid",
    "sessionId": "guid",
    "description": "string",
    "completionTime": "datetime?",
    "qualityResult": "enum[Excellent, Good, Acceptable, NeedsRework]",
    "challengesEncountered": "string",
    "reworkNeeded": "boolean",
    "timeSpent": "decimal",
    "status": "enum[Pending, Completed]"
}
```

### Problem
```csharp
{
    "id": "guid",
    "projectId": "guid",
    "discoveryDate": "datetime",
    "description": "string",
    "severity": "enum[Minor, Moderate, Major, Critical]",
    "costImpact": "decimal?",
    "timeImpact": "decimal?",
    "solutionPlan": "string",
    "isCommonProblem": "boolean",
    "resolved": "boolean"
}
```

## Business Rules
- Session start time cannot be in future
- Tasks can only be added to active sessions
- Problems must have severity and description
- Time spent must be calculated from timestamps
- Maximum session duration: 24 hours

## Validation Rules
- Task description: 1-500 characters
- Problem description: 10-1000 characters
- Solution plan: max 1000 characters
- Time spent: >= 0
- Quality result required when marking complete

## Performance Requirements
- Session list query: < 300ms
- Session creation: < 200ms
- Real-time session timer updates
- Support for concurrent sessions across users
