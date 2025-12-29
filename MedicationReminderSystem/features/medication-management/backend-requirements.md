# Medication Management - Backend Requirements

## Overview
The Medication Management feature handles all operations related to creating, updating, retrieving, and managing medications in the system, including drug interactions and medication lifecycle management.

## Domain Model

### Medication Aggregate
- **MedicationId**: Unique identifier (Guid)
- **UserId**: Owner of the medication record (Guid)
- **MedicationName**: Name of the medication (string, max 200 chars)
- **GenericName**: Generic drug name (string, max 200 chars)
- **Dosage**: Dose amount and unit (string, max 50 chars, e.g., "500mg", "10ml")
- **Form**: Medication form (enum: Tablet, Capsule, Liquid, Injection, Topical, Inhaler, Patch, Drops, Other)
- **Prescriber**: Doctor or healthcare provider name (string, max 200 chars)
- **PrescriptionNumber**: Prescription reference (string, max 100 chars)
- **StartDate**: Date medication regimen began (DateTime)
- **EndDate**: Date medication should end (DateTime, nullable)
- **IsActive**: Currently taking medication (bool)
- **IsPaused**: Temporarily suspended (bool)
- **PauseReason**: Reason for pause (string, max 500 chars)
- **PauseStartDate**: When pause began (DateTime, nullable)
- **ExpectedResumeDate**: When to resume (DateTime, nullable)
- **DiscontinuedDate**: When medication was stopped (DateTime, nullable)
- **DiscontinuationReason**: Why medication was stopped (string, max 500 chars)
- **DurationTaken**: Time period medication was taken (TimeSpan)
- **Purpose**: Medical reason for medication (string, max 500 chars)
- **Instructions**: Special instructions (string, max 1000 chars)
- **SideEffectsToWatch**: Known side effects (string, max 1000 chars)
- **WithFood**: Must be taken with food (bool)
- **Category**: Medication category (enum: Prescription, OTC, Supplement, Vitamin)
- **Color**: Pill color for identification (string, max 50 chars)
- **Shape**: Pill shape for identification (string, max 50 chars)
- **ImageUrl**: Photo of medication (string, nullable)
- **Notes**: Additional notes (string, max 2000 chars)
- **CreatedAt**: Record creation timestamp (DateTime)
- **UpdatedAt**: Last modification timestamp (DateTime)

### MedicationSchedule Entity
- **ScheduleId**: Unique identifier (Guid)
- **MedicationId**: Associated medication (Guid)
- **Frequency**: How often to take (enum: AsNeeded, Daily, EveryXDays, Weekly, Monthly, Custom)
- **DaysInterval**: For EveryXDays frequency (int)
- **TimesOfDay**: Scheduled times (List<TimeSpan>)
- **DaysOfWeek**: For weekly schedule (List<DayOfWeek>)
- **WithFoodRequirement**: Food requirement (bool)
- **MaxDosesPerDay**: Safety limit (int, nullable)
- **MinimumInterval**: Hours between doses (decimal, nullable)
- **CreatedAt**: Schedule creation timestamp (DateTime)
- **UpdatedAt**: Last modification timestamp (DateTime)

## Commands

### AddMedicationCommand
- Validates required fields (MedicationName, Dosage, StartDate)
- Ensures StartDate is not more than 1 year in past
- Creates Medication aggregate
- Raises **MedicationAdded** domain event
- Returns MedicationId

### SetMedicationScheduleCommand
- Validates MedicationId exists
- Validates TimesOfDay not empty
- Validates MaxDosesPerDay >= number of times if set
- Creates or updates MedicationSchedule
- Raises **MedicationScheduleSet** domain event
- Triggers reminder creation
- Returns ScheduleId

### DiscontinueMedicationCommand
- Validates MedicationId exists
- Validates medication is active
- Sets IsActive to false
- Sets DiscontinuedDate and DiscontinuationReason
- Calculates DurationTaken
- Raises **MedicationDiscontinued** domain event
- Triggers reminder cancellation
- Returns success indicator

### PauseMedicationCommand
- Validates MedicationId exists
- Validates medication is active and not already paused
- Sets IsPaused to true
- Sets PauseStartDate, ExpectedResumeDate, PauseReason
- Raises **MedicationPaused** domain event
- Triggers reminder suspension
- Returns success indicator

### ResumeMedicationCommand
- Validates MedicationId exists
- Validates medication is currently paused
- Sets IsPaused to false
- Clears pause-related fields
- Raises **MedicationResumed** domain event
- Reactivates reminders
- Returns success indicator

### UpdateMedicationCommand
- Validates MedicationId exists
- Validates user has permission
- Updates allowed fields
- Raises **MedicationUpdated** domain event
- Returns success indicator

### CheckDrugInteractionsCommand
- Validates MedicationId exists
- Queries all active medications for user
- Checks interaction database for conflicts
- Raises **DrugInteractionDetected** if interactions found
- Returns list of interactions with severity

### DeleteMedicationCommand
- Validates MedicationId exists
- Validates user has permission
- Checks for active doses/reminders (warn user)
- Soft deletes medication
- Raises **MedicationDeleted** domain event
- Returns success indicator

## Queries

### GetMedicationByIdQuery
- Returns Medication details by MedicationId
- Includes schedule information
- Returns null if not found

### GetMedicationsByUserIdQuery
- Returns all medications for a specific user
- Supports filtering by:
  - IsActive status
  - IsPaused status
  - Category
  - Form
  - Date range
- Supports sorting by:
  - MedicationName (alphabetically)
  - StartDate (newest/oldest)
  - Prescriber
- Supports pagination
- Returns list of MedicationDto

### GetActiveMedicationsQuery
- Returns only active, non-paused medications
- Sorted by MedicationName
- Includes schedule information
- Returns list of MedicationDto

### GetPausedMedicationsQuery
- Returns medications that are paused
- Includes ExpectedResumeDate
- Sorted by ExpectedResumeDate
- Returns list of MedicationDto

### GetMedicationHistoryQuery
- Returns timeline of medication changes
- Includes adds, modifications, pauses, discontinuations
- Filtered by user and optional date range
- Sorted by timestamp descending
- Returns list of MedicationHistoryDto

### GetMedicationInteractionsQuery
- Returns all known interactions for user's medications
- Groups by severity (critical, moderate, minor)
- Includes interaction descriptions
- Returns list of InteractionDto

### SearchMedicationsQuery
- Full-text search across MedicationName, GenericName, Purpose
- Supports filters (Category, Form, IsActive)
- Returns paginated results

## Domain Events

### MedicationAdded
```csharp
public class MedicationAdded : DomainEvent
{
    public Guid MedicationId { get; set; }
    public Guid UserId { get; set; }
    public string MedicationName { get; set; }
    public string Dosage { get; set; }
    public string Form { get; set; }
    public string Prescriber { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime OccurredAt { get; set; }
}
```

### MedicationScheduleSet
```csharp
public class MedicationScheduleSet : DomainEvent
{
    public Guid ScheduleId { get; set; }
    public Guid MedicationId { get; set; }
    public string Frequency { get; set; }
    public List<TimeSpan> TimesOfDay { get; set; }
    public bool WithFoodRequirement { get; set; }
    public Guid UserId { get; set; }
    public DateTime OccurredAt { get; set; }
}
```

### MedicationDiscontinued
```csharp
public class MedicationDiscontinued : DomainEvent
{
    public Guid MedicationId { get; set; }
    public DateTime DiscontinuationDate { get; set; }
    public string DiscontinuationReason { get; set; }
    public TimeSpan DurationTaken { get; set; }
    public Guid UserId { get; set; }
    public DateTime OccurredAt { get; set; }
}
```

### MedicationPaused
```csharp
public class MedicationPaused : DomainEvent
{
    public Guid MedicationId { get; set; }
    public DateTime PauseStartDate { get; set; }
    public DateTime? ExpectedResumeDate { get; set; }
    public string PauseReason { get; set; }
    public Guid UserId { get; set; }
    public DateTime OccurredAt { get; set; }
}
```

### DrugInteractionDetected
```csharp
public class DrugInteractionDetected : DomainEvent
{
    public List<Guid> MedicationIds { get; set; }
    public string InteractionType { get; set; }
    public string Severity { get; set; }
    public string ClinicalSignificance { get; set; }
    public Guid UserId { get; set; }
    public DateTime OccurredAt { get; set; }
}
```

## API Endpoints

### POST /api/medications
- Creates a new medication
- Request body: AddMedicationCommand
- Returns: 201 Created with MedicationId
- Authorization: Authenticated user

### POST /api/medications/{medicationId}/schedule
- Sets medication dosing schedule
- Request body: SetMedicationScheduleCommand
- Returns: 201 Created with ScheduleId
- Authorization: Medication owner

### PUT /api/medications/{medicationId}
- Updates medication details
- Request body: UpdateMedicationCommand
- Returns: 200 OK
- Authorization: Medication owner

### POST /api/medications/{medicationId}/discontinue
- Discontinues medication
- Request body: { discontinuationReason: string }
- Returns: 200 OK
- Authorization: Medication owner

### POST /api/medications/{medicationId}/pause
- Pauses medication temporarily
- Request body: { pauseReason: string, expectedResumeDate: DateTime? }
- Returns: 200 OK
- Authorization: Medication owner

### POST /api/medications/{medicationId}/resume
- Resumes paused medication
- Returns: 200 OK
- Authorization: Medication owner

### DELETE /api/medications/{medicationId}
- Deletes a medication
- Returns: 204 No Content
- Authorization: Medication owner

### GET /api/medications/{medicationId}
- Retrieves medication details
- Returns: 200 OK with MedicationDto
- Authorization: Medication owner

### GET /api/medications
- Retrieves all medications for current user
- Query params: filter, sort, page, pageSize
- Returns: 200 OK with paginated MedicationDto list
- Authorization: Authenticated user

### GET /api/medications/active
- Retrieves active medications
- Returns: 200 OK with MedicationDto list
- Authorization: Authenticated user

### GET /api/medications/interactions
- Checks for drug interactions
- Returns: 200 OK with InteractionDto list
- Authorization: Authenticated user

### GET /api/medications/search
- Searches medications
- Query params: q (query), filter, page, pageSize
- Returns: 200 OK with paginated MedicationDto list
- Authorization: Authenticated user

## Business Rules

1. **Medication Name**: Required, must not be empty
2. **Dosage**: Required, must be specific (e.g., "500mg" not just "500")
3. **Start Date**: Cannot be more than 1 year in the past
4. **Schedule Times**: Must have at least one time of day when schedule is set
5. **Max Doses**: If set, must be >= number of scheduled times
6. **Pause**: Can only pause active, non-paused medications
7. **Resume**: Can only resume paused medications
8. **Discontinue**: Can only discontinue active medications
9. **User Isolation**: Users can only access their own medications
10. **Audit Trail**: All changes must be logged with user and timestamp
11. **Soft Delete**: Medications should be soft-deleted to maintain history
12. **Interaction Check**: Automatically run when new medication added

## Data Persistence

### Medications Table
```sql
CREATE TABLE Medications (
    MedicationId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    UserId UNIQUEIDENTIFIER NOT NULL,
    MedicationName NVARCHAR(200) NOT NULL,
    GenericName NVARCHAR(200) NULL,
    Dosage NVARCHAR(50) NOT NULL,
    Form NVARCHAR(50) NOT NULL,
    Prescriber NVARCHAR(200) NULL,
    PrescriptionNumber NVARCHAR(100) NULL,
    StartDate DATETIME2 NOT NULL,
    EndDate DATETIME2 NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    IsPaused BIT NOT NULL DEFAULT 0,
    PauseReason NVARCHAR(500) NULL,
    PauseStartDate DATETIME2 NULL,
    ExpectedResumeDate DATETIME2 NULL,
    DiscontinuedDate DATETIME2 NULL,
    DiscontinuationReason NVARCHAR(500) NULL,
    DurationTaken BIGINT NULL, -- Ticks
    Purpose NVARCHAR(500) NULL,
    Instructions NVARCHAR(1000) NULL,
    SideEffectsToWatch NVARCHAR(1000) NULL,
    WithFood BIT NOT NULL DEFAULT 0,
    Category NVARCHAR(50) NOT NULL,
    Color NVARCHAR(50) NULL,
    Shape NVARCHAR(50) NULL,
    ImageUrl NVARCHAR(500) NULL,
    Notes NVARCHAR(2000) NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    IsDeleted BIT NOT NULL DEFAULT 0,
    DeletedAt DATETIME2 NULL,

    CONSTRAINT FK_Medications_Users FOREIGN KEY (UserId) REFERENCES Users(UserId),
    INDEX IX_Medications_UserId (UserId),
    INDEX IX_Medications_IsActive (IsActive),
    INDEX IX_Medications_StartDate (StartDate)
);
```

### MedicationSchedules Table
```sql
CREATE TABLE MedicationSchedules (
    ScheduleId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    MedicationId UNIQUEIDENTIFIER NOT NULL,
    Frequency NVARCHAR(50) NOT NULL,
    DaysInterval INT NULL,
    TimesOfDay NVARCHAR(MAX) NOT NULL, -- JSON array of TimeSpan
    DaysOfWeek NVARCHAR(MAX) NULL, -- JSON array
    WithFoodRequirement BIT NOT NULL DEFAULT 0,
    MaxDosesPerDay INT NULL,
    MinimumInterval DECIMAL(10,2) NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),

    CONSTRAINT FK_MedicationSchedules_Medications FOREIGN KEY (MedicationId) REFERENCES Medications(MedicationId),
    INDEX IX_MedicationSchedules_MedicationId (MedicationId)
);
```

## Error Handling

### Validation Errors (400 Bad Request)
- Missing required fields
- Invalid dosage format
- Invalid date (too far in past/future)
- Invalid schedule configuration

### Authorization Errors (403 Forbidden)
- User attempting to access another user's medication
- User attempting to modify without permission

### Not Found Errors (404 Not Found)
- MedicationId does not exist
- Medication was deleted

### Conflict Errors (409 Conflict)
- Attempting to pause already paused medication
- Attempting to discontinue already discontinued medication
- Attempting to resume non-paused medication

### Server Errors (500 Internal Server Error)
- Database connection failures
- Interaction check service unavailable
- Unexpected exceptions

## Integration Points

### Event Handlers
- **MedicationAdded**: Trigger interaction check, create default schedule prompt
- **MedicationScheduleSet**: Create reminders, calculate inventory needs
- **MedicationDiscontinued**: Cancel reminders, archive medication history
- **MedicationPaused**: Suspend reminders, adjust adherence calculations
- **DrugInteractionDetected**: Send alert, notify healthcare provider if critical

### Background Jobs
- **Interaction Check Job**: Nightly job to recheck interactions with updated databases
- **Auto-Resume Job**: Daily job to check for medications past ExpectedResumeDate
- **Cleanup Job**: Archive old discontinued medications (older than 7 years)

## Performance Considerations

- Index on UserId for fast user-specific queries
- Index on IsActive for filtering active medications
- Cache frequently accessed medications
- Optimize interaction checking with batching
- Use pagination for list queries

## Security Considerations

- Validate user authorization on all operations
- Sanitize user input to prevent injection attacks
- Encrypt sensitive fields (PrescriptionNumber)
- HIPAA compliance for PHI data
- Implement rate limiting on API endpoints
- Log all access attempts for audit

## Testing Requirements

### Unit Tests
- Domain model validation
- Business rule enforcement
- Command handlers
- Query handlers
- Domain event creation
- Interaction detection logic

### Integration Tests
- API endpoint functionality
- Database operations
- Event publishing
- Authorization checks
- Interaction database integration

### Performance Tests
- Query performance with large datasets (10k+ medications)
- Concurrent user operations
- Interaction checking performance
