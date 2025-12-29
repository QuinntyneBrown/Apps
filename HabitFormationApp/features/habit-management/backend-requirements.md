# Habit Management - Backend Requirements

## Overview
The Habit Management feature enables users to create, modify, archive, and reactivate habits. This is the core foundation of the application.

## Domain Events
- **HabitCreated**: Triggered when a new habit is created
- **HabitModified**: Triggered when habit details are updated
- **HabitArchived**: Triggered when a habit is archived
- **HabitReactivated**: Triggered when an archived habit is reactivated

## Aggregates

### Habit Aggregate
**Properties:**
- `Id` (Guid): Unique identifier
- `UserId` (Guid): Owner of the habit
- `Name` (string): Habit name (max 100 characters)
- `Description` (string): Detailed description (max 500 characters)
- `Frequency` (FrequencyType): Daily, Weekly, Custom
- `FrequencyDetails` (FrequencyDetails): Specific days/times
- `Category` (string): Health, Productivity, Wellness, etc.
- `StartDate` (DateTime): When habit tracking begins
- `TargetDuration` (int?): Target streak in days (optional)
- `ReminderEnabled` (bool): Whether reminders are active
- `IsArchived` (bool): Archive status
- `CreatedAt` (DateTime): Creation timestamp
- `UpdatedAt` (DateTime): Last modification timestamp
- `ArchivedAt` (DateTime?): Archive timestamp

**Invariants:**
- Name cannot be empty or whitespace
- StartDate cannot be in the past by more than 30 days
- Frequency must be valid
- UserId must exist
- Cannot modify archived habits without reactivating first

## Commands

### CreateHabitCommand
```csharp
public record CreateHabitCommand(
    Guid UserId,
    string Name,
    string Description,
    FrequencyType Frequency,
    FrequencyDetails FrequencyDetails,
    string Category,
    DateTime StartDate,
    int? TargetDuration,
    bool ReminderEnabled
);
```

### ModifyHabitCommand
```csharp
public record ModifyHabitCommand(
    Guid HabitId,
    string Name,
    string Description,
    FrequencyType Frequency,
    FrequencyDetails FrequencyDetails,
    string Category,
    int? TargetDuration,
    bool ReminderEnabled
);
```

### ArchiveHabitCommand
```csharp
public record ArchiveHabitCommand(
    Guid HabitId,
    string Reason
);
```

### ReactivateHabitCommand
```csharp
public record ReactivateHabitCommand(
    Guid HabitId,
    DateTime NewStartDate
);
```

## Queries

### GetHabitByIdQuery
Returns a single habit by ID with all details.

### GetActiveHabitsByUserQuery
Returns all active (non-archived) habits for a user.

### GetArchivedHabitsByUserQuery
Returns all archived habits for a user.

### GetHabitsByCategoryQuery
Returns habits filtered by category.

## API Endpoints

### POST /api/habits
Create a new habit.
- **Request**: CreateHabitCommand
- **Response**: 201 Created with HabitDto
- **Events**: HabitCreated

### PUT /api/habits/{id}
Modify an existing habit.
- **Request**: ModifyHabitCommand
- **Response**: 200 OK with HabitDto
- **Events**: HabitModified

### POST /api/habits/{id}/archive
Archive a habit.
- **Request**: ArchiveHabitCommand
- **Response**: 200 OK
- **Events**: HabitArchived

### POST /api/habits/{id}/reactivate
Reactivate an archived habit.
- **Request**: ReactivateHabitCommand
- **Response**: 200 OK with HabitDto
- **Events**: HabitReactivated

### GET /api/habits/{id}
Get habit by ID.
- **Response**: 200 OK with HabitDto

### GET /api/habits/user/{userId}
Get all active habits for a user.
- **Query Parameters**: category, sortBy, sortOrder
- **Response**: 200 OK with List<HabitDto>

### GET /api/habits/user/{userId}/archived
Get all archived habits for a user.
- **Response**: 200 OK with List<HabitDto>

## Value Objects

### FrequencyType (Enum)
- Daily
- Weekly
- Custom

### FrequencyDetails
```csharp
public record FrequencyDetails(
    int[] DaysOfWeek,  // 0=Sunday, 6=Saturday
    int TimesPerWeek,
    TimeSpan? PreferredTime
);
```

## Business Rules
1. Users can create unlimited habits
2. Habit names must be unique per user
3. Archived habits are hidden from active views but retain all data
4. Reactivating a habit resets its streak
5. Habits can only be deleted if they have no completion history
6. StartDate can be backdated up to 30 days for habit migration

## Database Schema

### Habits Table
```sql
CREATE TABLE Habits (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(500),
    Frequency NVARCHAR(20) NOT NULL,
    FrequencyDetails NVARCHAR(MAX), -- JSON
    Category NVARCHAR(50),
    StartDate DATETIME2 NOT NULL,
    TargetDuration INT NULL,
    ReminderEnabled BIT NOT NULL DEFAULT 0,
    IsArchived BIT NOT NULL DEFAULT 0,
    CreatedAt DATETIME2 NOT NULL,
    UpdatedAt DATETIME2 NOT NULL,
    ArchivedAt DATETIME2 NULL,
    CONSTRAINT FK_Habits_Users FOREIGN KEY (UserId) REFERENCES Users(Id)
);

CREATE INDEX IX_Habits_UserId ON Habits(UserId);
CREATE INDEX IX_Habits_Category ON Habits(Category);
CREATE INDEX IX_Habits_IsArchived ON Habits(IsArchived);
```

## Integration Points
- User service: Validate user existence
- Reminder service: Schedule reminders when enabled
- Streak service: Initialize streak tracking
- Analytics service: Track habit creation patterns

## Security & Validation
- Users can only manage their own habits
- JWT authentication required
- Input validation:
  - Name: 1-100 characters
  - Description: max 500 characters
  - Category: predefined list or custom (max 50 chars)
- Rate limiting: 10 habit creations per hour per user

## Performance Considerations
- Cache active habits per user (5-minute TTL)
- Use database indexes on UserId, Category, IsArchived
- Paginate habit lists for users with 50+ habits
- Async event publishing for domain events

## Testing Requirements
- Unit tests for all command handlers
- Integration tests for API endpoints
- Event publishing verification tests
- Validation rule tests
- Concurrency tests for simultaneous updates
