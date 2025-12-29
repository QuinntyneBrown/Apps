# Completion Tracking - Backend Requirements

## Overview
The Completion Tracking feature allows users to log habit completions, undo mistaken logs, and record late completions. This is critical for building streaks and providing accurate progress data.

## Domain Events
- **HabitCompletionLogged**: Triggered when a user marks a habit as complete
- **HabitCompletionUndone**: Triggered when a user removes a completion record
- **LateCompletionLogged**: Triggered when a completion is logged after the scheduled time/day

## Aggregates

### HabitCompletion Aggregate
**Properties:**
- `Id` (Guid): Unique identifier
- `HabitId` (Guid): Reference to the habit
- `UserId` (Guid): User who completed the habit
- `CompletedAt` (DateTime): When the habit was actually completed
- `LoggedAt` (DateTime): When the completion was recorded in the system
- `ScheduledFor` (DateTime): When the habit was scheduled
- `IsLate` (bool): Whether completion was after the scheduled time
- `Notes` (string?): Optional notes about the completion (max 200 chars)
- `Location` (string?): Optional location data
- `Duration` (int?): Optional duration in minutes
- `IsDeleted` (bool): Soft delete flag
- `DeletedAt` (DateTime?): When the completion was undone

**Invariants:**
- CompletedAt cannot be in the future
- CompletedAt cannot be before habit's StartDate
- Cannot undo a completion older than 7 days
- UserId must match the habit's owner
- Cannot log duplicate completions for the same habit and day (for daily habits)

## Commands

### LogHabitCompletionCommand
```csharp
public record LogHabitCompletionCommand(
    Guid HabitId,
    Guid UserId,
    DateTime CompletedAt,
    string? Notes,
    string? Location,
    int? Duration
);
```

### UndoHabitCompletionCommand
```csharp
public record UndoHabitCompletionCommand(
    Guid CompletionId,
    Guid UserId,
    string? Reason
);
```

### LogLateCompletionCommand
```csharp
public record LogLateCompletionCommand(
    Guid HabitId,
    Guid UserId,
    DateTime CompletedAt,
    DateTime ScheduledFor,
    string? Notes,
    string? Location,
    int? Duration
);
```

## Queries

### GetCompletionByIdQuery
Returns a single completion record.

### GetCompletionsByHabitQuery
Returns all completions for a specific habit with pagination.

### GetCompletionsByDateRangeQuery
Returns completions within a date range for a user.

### GetTodayCompletionsQuery
Returns all completions for today for a user.

### GetCompletionStreakQuery
Calculates the current streak for a habit.

### GetCompletionStatsQuery
Returns completion statistics (total, rate, best streak, etc.).

## API Endpoints

### POST /api/completions
Log a habit completion.
- **Request**: LogHabitCompletionCommand
- **Response**: 201 Created with CompletionDto
- **Events**: HabitCompletionLogged or LateCompletionLogged

### DELETE /api/completions/{id}
Undo a habit completion.
- **Request**: UndoHabitCompletionCommand (in body)
- **Response**: 204 No Content
- **Events**: HabitCompletionUndone

### GET /api/completions/{id}
Get completion by ID.
- **Response**: 200 OK with CompletionDto

### GET /api/completions/habit/{habitId}
Get all completions for a habit.
- **Query Parameters**: page, pageSize, startDate, endDate
- **Response**: 200 OK with PagedList<CompletionDto>

### GET /api/completions/user/{userId}
Get completions for a user.
- **Query Parameters**: date, startDate, endDate
- **Response**: 200 OK with List<CompletionDto>

### GET /api/completions/user/{userId}/today
Get today's completions for a user.
- **Response**: 200 OK with List<CompletionDto>

### GET /api/completions/habit/{habitId}/streak
Get current streak for a habit.
- **Response**: 200 OK with StreakDto

### GET /api/completions/habit/{habitId}/stats
Get completion statistics.
- **Response**: 200 OK with CompletionStatsDto

## Value Objects

### CompletionMetadata
```csharp
public record CompletionMetadata(
    string? Notes,
    string? Location,
    int? Duration
);
```

## Business Rules
1. Users can only log completions for their own habits
2. Cannot log completions for archived habits
3. Cannot log completions for future dates
4. For daily habits, only one completion per calendar day
5. For weekly habits, completions count toward the week's target
6. Late completions (logged after midnight for daily habits) are flagged
7. Completions can only be undone within 7 days
8. Undoing a completion affects streak calculations
9. System time zone vs user time zone considerations for "today"
10. Backfilling completions is allowed for missed days (manual catch-up)

## Database Schema

### HabitCompletions Table
```sql
CREATE TABLE HabitCompletions (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    HabitId UNIQUEIDENTIFIER NOT NULL,
    UserId UNIQUEIDENTIFIER NOT NULL,
    CompletedAt DATETIME2 NOT NULL,
    LoggedAt DATETIME2 NOT NULL,
    ScheduledFor DATETIME2 NOT NULL,
    IsLate BIT NOT NULL DEFAULT 0,
    Notes NVARCHAR(200) NULL,
    Location NVARCHAR(100) NULL,
    Duration INT NULL,
    IsDeleted BIT NOT NULL DEFAULT 0,
    DeletedAt DATETIME2 NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    CONSTRAINT FK_HabitCompletions_Habits FOREIGN KEY (HabitId) REFERENCES Habits(Id),
    CONSTRAINT FK_HabitCompletions_Users FOREIGN KEY (UserId) REFERENCES Users(Id)
);

CREATE INDEX IX_HabitCompletions_HabitId ON HabitCompletions(HabitId);
CREATE INDEX IX_HabitCompletions_UserId ON HabitCompletions(UserId);
CREATE INDEX IX_HabitCompletions_CompletedAt ON HabitCompletions(CompletedAt);
CREATE INDEX IX_HabitCompletions_IsDeleted ON HabitCompletions(IsDeleted);
CREATE UNIQUE INDEX IX_HabitCompletions_Unique ON HabitCompletions(HabitId, CompletedAt, UserId)
    WHERE IsDeleted = 0;
```

## Integration Points
- Habit service: Validate habit existence and ownership
- Streak service: Update streak calculations
- Analytics service: Update completion rates and patterns
- Notification service: Trigger achievement notifications
- Reminder service: Mark reminder as acted upon

## Streak Calculation Algorithm
```
1. Get all non-deleted completions for habit, ordered by CompletedAt DESC
2. Start from most recent completion
3. For daily habits:
   - Check if there's a completion for each consecutive day
   - Break when a day is missing
4. For weekly habits:
   - Check if target completions per week are met
   - Break when a week doesn't meet target
5. Return streak count
```

## Security & Validation
- Users can only access their own completion data
- JWT authentication required
- Input validation:
  - Notes: max 200 characters
  - Duration: 1-1440 minutes (24 hours)
  - CompletedAt: not in future, not before habit start
- Rate limiting: 100 completion logs per hour per user
- Prevent completion spam

## Performance Considerations
- Index on HabitId and CompletedAt for fast streak queries
- Cache current streak value (invalidate on new completion/undo)
- Optimize streak calculation with SQL window functions
- Async event publishing
- Background job for late completion detection

## Completion Reminder Integration
When a completion is logged:
1. Check if there was an active reminder for this habit
2. Mark reminder as "acted upon"
3. Calculate response time (reminder time to completion time)
4. Use data for reminder optimization

## Analytics Data Points
- Completion time patterns (morning, afternoon, evening, night)
- Late completion frequency
- Average duration (if tracked)
- Location patterns (if tracked)
- Completion rate trends
- Day-of-week patterns

## Testing Requirements
- Unit tests for completion validation logic
- Unit tests for streak calculation algorithm
- Integration tests for API endpoints
- Event publishing verification tests
- Concurrency tests (simultaneous completions)
- Edge cases:
  - Completion on habit start date
  - Completion near midnight (timezone handling)
  - Undo and re-log scenarios
  - Archived habit completion attempts
- Performance tests for streak calculation with large datasets

## Error Scenarios
- Duplicate completion for same day (daily habit)
- Completion for archived habit
- Completion for non-existent habit
- Future date completion
- Undo after 7-day window
- Completion before habit start date
- User doesn't own the habit
