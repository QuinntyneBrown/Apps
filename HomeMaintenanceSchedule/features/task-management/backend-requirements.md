# Task Management - Backend Requirements

## Overview

The Task Management feature handles the creation, scheduling, tracking, and completion of home maintenance tasks. It implements event sourcing through domain events and follows Clean Architecture principles.

## Domain Events

### TaskScheduled
**Description**: Raised when a new maintenance task is scheduled or created.

**Event Properties**:
```csharp
public class TaskScheduledEvent : DomainEvent
{
    public Guid TaskId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime ScheduledDate { get; set; }
    public DateTime DueDate { get; set; }
    public TaskPriority Priority { get; set; }
    public TaskCategory Category { get; set; }
    public RecurrencePattern RecurrencePattern { get; set; }
    public int? RecurrenceInterval { get; set; }
    public Guid PropertyId { get; set; }
    public Guid? AssignedProviderId { get; set; }
    public decimal? EstimatedCost { get; set; }
}
```

**Triggers**:
- User creates a new maintenance task
- System generates a recurring task instance
- Seasonal checklist generates a task

**Event Handlers**:
- Send notification to user
- Create calendar entry
- Update task count metrics
- Log audit trail

### TaskCompleted
**Description**: Raised when a maintenance task is marked as completed.

**Event Properties**:
```csharp
public class TaskCompletedEvent : DomainEvent
{
    public Guid TaskId { get; set; }
    public DateTime CompletionDate { get; set; }
    public string Notes { get; set; }
    public List<string> PhotoUrls { get; set; }
    public decimal? ActualCost { get; set; }
    public Guid? CompletedByProviderId { get; set; }
    public TimeSpan? Duration { get; set; }
}
```

**Triggers**:
- User marks task as complete
- Service provider marks task as complete
- System auto-completes based on integration

**Event Handlers**:
- Update task status to Completed
- Generate next recurring task if applicable
- Calculate cost variance (estimated vs actual)
- Trigger ProviderServiceCompleted event if provider was involved
- Update completion rate metrics
- Send completion confirmation notification

### TaskPostponed
**Description**: Raised when a task's due date is changed to a later date.

**Event Properties**:
```csharp
public class TaskPostponedEvent : DomainEvent
{
    public Guid TaskId { get; set; }
    public DateTime OriginalDueDate { get; set; }
    public DateTime NewDueDate { get; set; }
    public string Reason { get; set; }
    public DateTime PostponedAt { get; set; }
}
```

**Triggers**:
- User manually postpones a task
- System suggests postponement based on weather/conditions
- Provider requests postponement

**Event Handlers**:
- Update task due date
- Cancel existing reminders
- Schedule new reminders
- Update calendar entry
- Log postponement history
- Notify assigned provider if applicable

### TaskOverdue
**Description**: Raised when a task passes its due date without completion.

**Event Properties**:
```csharp
public class TaskOverdueEvent : DomainEvent
{
    public Guid TaskId { get; set; }
    public DateTime DueDate { get; set; }
    public int DaysOverdue { get; set; }
    public TaskPriority Priority { get; set; }
    public Guid PropertyId { get; set; }
}
```

**Triggers**:
- Scheduled background job checks for overdue tasks daily
- Task due date passes without completion status change

**Event Handlers**:
- Update task status to Overdue
- Send overdue notification to user
- Escalate notification based on priority
- Update overdue metrics dashboard
- Trigger reminder sequence

### TaskCancelled
**Description**: Raised when a task is cancelled and will not be completed.

**Event Properties**:
```csharp
public class TaskCancelledEvent : DomainEvent
{
    public Guid TaskId { get; set; }
    public DateTime CancellationDate { get; set; }
    public string Reason { get; set; }
    public bool CancelFutureRecurrences { get; set; }
}
```

**Triggers**:
- User cancels a task
- Task is no longer relevant (e.g., appliance removed)
- Duplicate task identified

**Event Handlers**:
- Update task status to Cancelled
- Cancel future recurring instances if specified
- Remove scheduled reminders
- Remove calendar entries
- Notify assigned provider if applicable
- Update metrics (cancelled task count)

## Domain Model

### Task Aggregate Root
```csharp
public class Task : AggregateRoot
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public DateTime ScheduledDate { get; private set; }
    public DateTime DueDate { get; private set; }
    public DateTime? CompletedDate { get; private set; }
    public TaskStatus Status { get; private set; }
    public TaskPriority Priority { get; private set; }
    public TaskCategory Category { get; private set; }
    public RecurrencePattern RecurrencePattern { get; private set; }
    public int? RecurrenceInterval { get; private set; }
    public decimal? EstimatedCost { get; private set; }
    public decimal? ActualCost { get; private set; }
    public string Notes { get; private set; }
    public List<string> PhotoUrls { get; private set; }
    public Guid PropertyId { get; private set; }
    public Guid? AssignedProviderId { get; private set; }

    // Methods that raise domain events
    public void Schedule(/* parameters */) { }
    public void Complete(/* parameters */) { }
    public void Postpone(DateTime newDueDate, string reason) { }
    public void MarkAsOverdue() { }
    public void Cancel(string reason, bool cancelFutureRecurrences) { }
}
```

### Value Objects
```csharp
public enum TaskStatus
{
    Scheduled,
    InProgress,
    Completed,
    Overdue,
    Cancelled
}

public enum TaskPriority
{
    Low,
    Medium,
    High,
    Critical
}

public enum TaskCategory
{
    HVAC,
    Plumbing,
    Electrical,
    Landscaping,
    Appliances,
    Structural,
    Cleaning,
    Safety,
    Other
}

public enum RecurrencePattern
{
    None,
    Daily,
    Weekly,
    Monthly,
    Quarterly,
    Annually
}
```

## API Endpoints

### Commands (Write Operations)

#### POST /api/tasks
**Description**: Schedule a new maintenance task
**Request Body**:
```json
{
    "title": "Replace HVAC filter",
    "description": "Change air filter in main HVAC unit",
    "scheduledDate": "2025-01-15T10:00:00Z",
    "dueDate": "2025-01-15T18:00:00Z",
    "priority": "Medium",
    "category": "HVAC",
    "recurrencePattern": "Monthly",
    "recurrenceInterval": 3,
    "estimatedCost": 25.00,
    "propertyId": "guid",
    "assignedProviderId": "guid"
}
```
**Response**: 201 Created with TaskId
**Raises**: TaskScheduled event

#### PUT /api/tasks/{id}/complete
**Description**: Mark a task as completed
**Request Body**:
```json
{
    "completionDate": "2025-01-15T15:30:00Z",
    "notes": "Filter replaced successfully. Old filter was very dirty.",
    "photoUrls": ["url1", "url2"],
    "actualCost": 22.50,
    "duration": "00:15:00"
}
```
**Response**: 200 OK
**Raises**: TaskCompleted event

#### PUT /api/tasks/{id}/postpone
**Description**: Postpone a task to a later date
**Request Body**:
```json
{
    "newDueDate": "2025-01-20T18:00:00Z",
    "reason": "Waiting for parts delivery"
}
```
**Response**: 200 OK
**Raises**: TaskPostponed event

#### DELETE /api/tasks/{id}
**Description**: Cancel a task
**Request Body**:
```json
{
    "reason": "Task no longer needed",
    "cancelFutureRecurrences": false
}
```
**Response**: 200 OK
**Raises**: TaskCancelled event

### Queries (Read Operations)

#### GET /api/tasks
**Description**: Get filtered list of tasks
**Query Parameters**:
- status: TaskStatus filter
- priority: TaskPriority filter
- category: TaskCategory filter
- propertyId: Filter by property
- startDate: Filter by date range start
- endDate: Filter by date range end
- includeCompleted: boolean (default false)

**Response**:
```json
{
    "tasks": [
        {
            "id": "guid",
            "title": "Replace HVAC filter",
            "description": "...",
            "scheduledDate": "...",
            "dueDate": "...",
            "status": "Scheduled",
            "priority": "Medium",
            "category": "HVAC"
        }
    ],
    "totalCount": 25,
    "pageNumber": 1,
    "pageSize": 20
}
```

#### GET /api/tasks/{id}
**Description**: Get task details by ID
**Response**: Full task details including event history

#### GET /api/tasks/overdue
**Description**: Get all overdue tasks
**Response**: List of overdue tasks with days overdue

#### GET /api/tasks/upcoming
**Description**: Get upcoming tasks (next 7 days by default)
**Query Parameters**: days (default 7)

## Business Rules

1. **Task Scheduling**
   - Scheduled date cannot be in the past
   - Due date must be >= scheduled date
   - Recurring tasks must have valid recurrence pattern and interval

2. **Task Completion**
   - Only tasks with status Scheduled, InProgress, or Overdue can be completed
   - Completion date cannot be before scheduled date
   - Actual cost should be tracked for budget analysis

3. **Task Postponement**
   - Can only postpone Scheduled or Overdue tasks
   - New due date must be in the future
   - Cannot postpone more than 3 times (business rule)
   - Reason required for postponement

4. **Task Overdue**
   - Automatically triggered by background job
   - Runs daily at midnight
   - Only affects tasks with status Scheduled or InProgress
   - High priority tasks trigger immediate notifications

5. **Task Cancellation**
   - Cannot cancel completed tasks
   - Cancelling parent task can optionally cancel future recurring instances
   - Reason required for audit trail

## Background Jobs

### Overdue Task Checker
- **Schedule**: Daily at 12:00 AM
- **Process**:
  - Query all tasks with status Scheduled or InProgress
  - Filter tasks where DueDate < CurrentDate
  - Raise TaskOverdue event for each overdue task

### Recurring Task Generator
- **Schedule**: Daily at 1:00 AM
- **Process**:
  - Query all recurring tasks with next occurrence due
  - Generate new task instances
  - Raise TaskScheduled event for each generated task

### Reminder Scheduler
- **Schedule**: Every hour
- **Process**:
  - Query tasks due within configured reminder window
  - Send notifications based on user preferences
  - Mark reminders as sent to avoid duplicates

## Database Schema

### Tasks Table
```sql
CREATE TABLE Tasks (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    Title NVARCHAR(200) NOT NULL,
    Description NVARCHAR(MAX),
    ScheduledDate DATETIME2 NOT NULL,
    DueDate DATETIME2 NOT NULL,
    CompletedDate DATETIME2 NULL,
    Status NVARCHAR(50) NOT NULL,
    Priority NVARCHAR(50) NOT NULL,
    Category NVARCHAR(50) NOT NULL,
    RecurrencePattern NVARCHAR(50),
    RecurrenceInterval INT NULL,
    EstimatedCost DECIMAL(10,2) NULL,
    ActualCost DECIMAL(10,2) NULL,
    Notes NVARCHAR(MAX),
    PhotoUrls NVARCHAR(MAX), -- JSON array
    PropertyId UNIQUEIDENTIFIER NOT NULL,
    AssignedProviderId UNIQUEIDENTIFIER NULL,
    ParentTaskId UNIQUEIDENTIFIER NULL, -- For recurring tasks
    CreatedAt DATETIME2 DEFAULT GETUTCDATE(),
    CreatedBy UNIQUEIDENTIFIER,
    UpdatedAt DATETIME2 DEFAULT GETUTCDATE(),
    UpdatedBy UNIQUEIDENTIFIER,
    IsDeleted BIT DEFAULT 0,
    FOREIGN KEY (PropertyId) REFERENCES Properties(Id),
    FOREIGN KEY (AssignedProviderId) REFERENCES ServiceProviders(Id),
    INDEX IX_Tasks_Status (Status),
    INDEX IX_Tasks_DueDate (DueDate),
    INDEX IX_Tasks_PropertyId (PropertyId)
);
```

### Domain Events Table
```sql
CREATE TABLE DomainEvents (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    EventType NVARCHAR(100) NOT NULL,
    AggregateId UNIQUEIDENTIFIER NOT NULL,
    EventData NVARCHAR(MAX) NOT NULL, -- JSON
    OccurredAt DATETIME2 DEFAULT GETUTCDATE(),
    ProcessedAt DATETIME2 NULL,
    UserId UNIQUEIDENTIFIER,
    INDEX IX_DomainEvents_AggregateId (AggregateId),
    INDEX IX_DomainEvents_EventType (EventType)
);
```

## Validation Rules

### Task Creation
- Title: Required, max 200 characters
- Description: Optional, max 5000 characters
- Scheduled Date: Required, must be valid date
- Due Date: Required, must be >= ScheduledDate
- Priority: Required, must be valid enum value
- Category: Required, must be valid enum value
- Estimated Cost: Optional, must be >= 0
- Property ID: Required, must exist

### Task Completion
- Completion Date: Required, must be <= current date
- Notes: Optional, max 5000 characters
- Actual Cost: Optional, must be >= 0
- Photos: Optional, valid URLs

## Error Handling

### Common Error Responses
- 400 Bad Request: Validation errors
- 404 Not Found: Task not found
- 409 Conflict: Invalid state transition
- 500 Internal Server Error: System errors

### Example Error Response
```json
{
    "error": {
        "code": "TASK_NOT_FOUND",
        "message": "Task with ID {id} was not found",
        "details": []
    }
}
```

## Performance Considerations

1. **Indexing**: Create indexes on Status, DueDate, PropertyId
2. **Pagination**: Implement cursor-based pagination for large result sets
3. **Caching**: Cache frequently accessed task lists (Redis)
4. **Query Optimization**: Use projection for list views (don't load full aggregate)
5. **Event Processing**: Process events asynchronously using message queue

## Security Requirements

1. **Authorization**: Users can only access tasks for their properties
2. **Input Validation**: Sanitize all input to prevent injection attacks
3. **Audit Trail**: Log all state changes through domain events
4. **Data Privacy**: Encrypt sensitive data (notes, photos)

## Integration Points

1. **Notification Service**: Send task reminders and alerts
2. **Calendar Service**: Sync tasks with external calendars
3. **Provider Service**: Notify providers of assigned tasks
4. **Analytics Service**: Track task metrics and trends
5. **Photo Storage**: Upload and retrieve task photos

## Testing Requirements

### Unit Tests
- Domain event raising logic
- Business rule validation
- Value object behavior

### Integration Tests
- API endpoint functionality
- Database operations
- Event handler execution

### Performance Tests
- Query performance under load
- Background job execution time
- Event processing latency

## Monitoring and Logging

### Metrics to Track
- Tasks created per day
- Task completion rate
- Average time to complete tasks
- Overdue task count
- Postponement rate

### Logs to Capture
- All domain events
- API requests and responses
- Background job executions
- Error and exception details

## Documentation

- API documentation (Swagger/OpenAPI)
- Event catalog with examples
- Database schema documentation
- Deployment guide
- Troubleshooting guide
