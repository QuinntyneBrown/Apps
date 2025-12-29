# Maintenance Tasks - Backend Requirements

## Overview
The Maintenance Tasks backend provides a robust API for creating, scheduling, tracking, and managing home maintenance tasks with support for recurring schedules, reminders, and completion tracking.

## Domain Model

### Task Entity
```csharp
public class Task : BaseEntity
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public TaskCategory Category { get; set; }
    public TaskPriority Priority { get; set; }
    public DateTime ScheduledDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? CompletedDate { get; set; }
    public TaskStatus Status { get; set; }
    public RecurrencePattern RecurrencePattern { get; set; }
    public int? RecurrenceInterval { get; set; }
    public decimal? EstimatedCost { get; set; }
    public decimal? ActualCost { get; set; }
    public string Notes { get; set; }
    public List<string> PhotoUrls { get; set; }
    public Guid PropertyId { get; set; }
    public Guid? AssignedProviderId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }

    // Navigation properties
    public Property Property { get; set; }
    public ServiceProvider AssignedProvider { get; set; }
}
```

### Enumerations
```csharp
public enum TaskCategory
{
    HVAC,
    Plumbing,
    Electrical,
    Landscaping,
    Roofing,
    Painting,
    Cleaning,
    Inspection,
    Other
}

public enum TaskPriority
{
    Low,
    Medium,
    High,
    Critical
}

public enum TaskStatus
{
    Scheduled,
    InProgress,
    Completed,
    Cancelled,
    Overdue,
    Postponed
}

public enum RecurrencePattern
{
    None,
    Daily,
    Weekly,
    Monthly,
    Quarterly,
    SemiAnnually,
    Annually
}
```

## API Endpoints

### Commands (Write Operations)

#### Create Task
- **Endpoint**: `POST /api/tasks`
- **Request Body**:
```json
{
  "title": "Replace HVAC Filters",
  "description": "Replace all air filters in the HVAC system",
  "category": "HVAC",
  "priority": "Medium",
  "scheduledDate": "2025-01-15T10:00:00Z",
  "dueDate": "2025-01-15T18:00:00Z",
  "recurrencePattern": "Quarterly",
  "recurrenceInterval": 1,
  "estimatedCost": 50.00,
  "propertyId": "guid",
  "assignedProviderId": "guid"
}
```
- **Response**: `201 Created` with Task object
- **Domain Event**: `TaskScheduled`

#### Update Task
- **Endpoint**: `PUT /api/tasks/{id}`
- **Request Body**: Same as Create
- **Response**: `200 OK` with updated Task object

#### Complete Task
- **Endpoint**: `POST /api/tasks/{id}/complete`
- **Request Body**:
```json
{
  "completedDate": "2025-01-15T14:30:00Z",
  "actualCost": 45.00,
  "notes": "Replaced all 3 filters. Found living room filter was very dirty.",
  "photoUrls": ["https://storage/task1-before.jpg", "https://storage/task1-after.jpg"]
}
```
- **Response**: `200 OK`
- **Domain Event**: `TaskCompleted`

#### Postpone Task
- **Endpoint**: `POST /api/tasks/{id}/postpone`
- **Request Body**:
```json
{
  "newDate": "2025-01-22T10:00:00Z",
  "reason": "Waiting for part delivery"
}
```
- **Response**: `200 OK`
- **Domain Event**: `TaskPostponed`

#### Cancel Task
- **Endpoint**: `POST /api/tasks/{id}/cancel`
- **Request Body**:
```json
{
  "reason": "No longer needed"
}
```
- **Response**: `200 OK`
- **Domain Event**: `TaskCancelled`

#### Delete Task
- **Endpoint**: `DELETE /api/tasks/{id}`
- **Response**: `204 No Content`

### Queries (Read Operations)

#### Get Task by ID
- **Endpoint**: `GET /api/tasks/{id}`
- **Response**: `200 OK` with Task object

#### Get All Tasks
- **Endpoint**: `GET /api/tasks`
- **Query Parameters**:
  - `propertyId` (optional): Filter by property
  - `status` (optional): Filter by status
  - `priority` (optional): Filter by priority
  - `category` (optional): Filter by category
  - `startDate` (optional): Filter tasks after date
  - `endDate` (optional): Filter tasks before date
  - `pageNumber` (default: 1)
  - `pageSize` (default: 20)
- **Response**: `200 OK` with paginated Task list

#### Get Upcoming Tasks
- **Endpoint**: `GET /api/tasks/upcoming`
- **Query Parameters**:
  - `days` (default: 7): Number of days to look ahead
  - `propertyId` (optional)
- **Response**: `200 OK` with Task list

#### Get Overdue Tasks
- **Endpoint**: `GET /api/tasks/overdue`
- **Query Parameters**:
  - `propertyId` (optional)
- **Response**: `200 OK` with Task list

#### Get Task History
- **Endpoint**: `GET /api/tasks/{id}/history`
- **Response**: `200 OK` with completion history

#### Get Task Statistics
- **Endpoint**: `GET /api/tasks/statistics`
- **Query Parameters**:
  - `propertyId` (optional)
  - `startDate`, `endDate` (optional)
- **Response**:
```json
{
  "totalTasks": 150,
  "completedTasks": 120,
  "overdueTasks": 5,
  "upcomingTasks": 25,
  "completionRate": 80.0,
  "averageCost": 125.50,
  "totalCost": 15060.00,
  "tasksByCategory": {
    "HVAC": 30,
    "Plumbing": 25,
    "Electrical": 20
  }
}
```

## Business Logic

### Recurrence Handling
- When a recurring task is completed, automatically create the next instance based on the recurrence pattern
- Calculate next due date: `currentDate + recurrenceInterval * recurrencePattern`
- Copy all task properties except dates and completion status
- Maintain linkage between recurring task instances for history tracking

### Overdue Task Detection
- Background job runs daily at midnight
- Identify tasks where `DueDate < CurrentDate AND Status != Completed`
- Update status to `Overdue`
- Publish `TaskOverdue` domain event
- Send notification to property owner

### Task Prioritization Logic
- **Critical**: Requires immediate attention (safety/emergency)
- **High**: Should be completed within 1 week
- **Medium**: Should be completed within 1 month
- **Low**: Can be scheduled flexibly

### Cost Tracking
- `EstimatedCost`: Budget planning amount
- `ActualCost`: Real cost after completion
- Calculate variance: `(ActualCost - EstimatedCost) / EstimatedCost * 100`
- Track cost trends over time

## Domain Events

### TaskScheduled Event
```csharp
public class TaskScheduledEvent : IDomainEvent
{
    public Guid TaskId { get; set; }
    public string Title { get; set; }
    public DateTime ScheduledDate { get; set; }
    public RecurrencePattern RecurrencePattern { get; set; }
    public TaskPriority Priority { get; set; }
    public Guid PropertyId { get; set; }
    public DateTime OccurredAt { get; set; }
}
```
**Handlers**:
- Send calendar invitation
- Create reminder notifications
- Update task dashboard

### TaskCompleted Event
```csharp
public class TaskCompletedEvent : IDomainEvent
{
    public Guid TaskId { get; set; }
    public DateTime CompletionDate { get; set; }
    public string Notes { get; set; }
    public List<string> PhotoUrls { get; set; }
    public decimal? ActualCost { get; set; }
    public DateTime OccurredAt { get; set; }
}
```
**Handlers**:
- Schedule next recurring task instance
- Update statistics
- Request service provider rating (if applicable)
- Update property maintenance history

### TaskPostponed Event
```csharp
public class TaskPostponedEvent : IDomainEvent
{
    public Guid TaskId { get; set; }
    public DateTime OriginalDate { get; set; }
    public DateTime NewDate { get; set; }
    public string Reason { get; set; }
    public DateTime OccurredAt { get; set; }
}
```
**Handlers**:
- Update calendar
- Reschedule reminders
- Log postponement history

### TaskOverdue Event
```csharp
public class TaskOverdueEvent : IDomainEvent
{
    public Guid TaskId { get; set; }
    public DateTime DueDate { get; set; }
    public int DaysOverdue { get; set; }
    public TaskPriority Priority { get; set; }
    public DateTime OccurredAt { get; set; }
}
```
**Handlers**:
- Send alert notification
- Escalate based on priority
- Update dashboard alerts

### TaskCancelled Event
```csharp
public class TaskCancelledEvent : IDomainEvent
{
    public Guid TaskId { get; set; }
    public DateTime CancellationDate { get; set; }
    public string Reason { get; set; }
    public DateTime OccurredAt { get; set; }
}
```
**Handlers**:
- Remove from calendar
- Cancel reminders
- Update statistics

## Validation Rules

### Create/Update Task
- `Title`: Required, max 200 characters
- `Description`: Optional, max 2000 characters
- `ScheduledDate`: Required, cannot be in the past
- `DueDate`: Required, must be >= ScheduledDate
- `RecurrencePattern`: Required
- `RecurrenceInterval`: Required if pattern != None, must be > 0
- `EstimatedCost`: Optional, must be >= 0
- `Category`: Required, valid enum value
- `Priority`: Required, valid enum value
- `PropertyId`: Required, must exist

### Complete Task
- Task must be in `Scheduled` or `InProgress` status
- `CompletedDate`: Required, cannot be in future
- `ActualCost`: Optional, must be >= 0
- `Notes`: Optional, max 2000 characters

## Background Jobs

### Daily Overdue Check
- **Schedule**: Daily at 00:00 UTC
- **Action**: Mark overdue tasks and publish events

### Reminder Notifications
- **Schedule**:
  - 7 days before due date
  - 3 days before due date
  - 1 day before due date
  - On due date
- **Action**: Send email/SMS/push notification

### Recurring Task Generation
- **Trigger**: On task completion with recurrence pattern
- **Action**: Create next task instance

## Security

### Authorization
- Users can only access tasks for properties they own/manage
- Admin users can access all tasks
- Service providers can view assigned tasks only

### Data Protection
- Photo URLs use signed URLs with expiration
- Sensitive notes encrypted at rest
- Audit log for all task modifications

## Performance Considerations

- Index on `PropertyId`, `Status`, `DueDate`, `ScheduledDate`
- Composite index on `(PropertyId, Status, DueDate)`
- Pagination required for list endpoints
- Cache frequently accessed property tasks
- Async processing for photo uploads

## Error Handling

- `404 Not Found`: Task ID doesn't exist
- `400 Bad Request`: Validation errors
- `403 Forbidden`: User doesn't have access to property
- `409 Conflict`: Cannot complete already completed task
- `500 Internal Server Error`: Unexpected errors

## Testing Requirements

### Unit Tests
- Task creation with valid data
- Recurrence calculation logic
- Overdue detection logic
- Cost variance calculation
- Domain event publishing

### Integration Tests
- Complete task workflow
- Recurring task generation
- Overdue task background job
- API endpoint responses
- Database operations

### Performance Tests
- Load test with 10,000 tasks per property
- Concurrent task updates
- Overdue job performance with 100,000 tasks

---

**Version**: 1.0
**Last Updated**: 2025-12-29
