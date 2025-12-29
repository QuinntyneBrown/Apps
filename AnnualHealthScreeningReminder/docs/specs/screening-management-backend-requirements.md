# Screening Management - Backend Requirements

## API Endpoints

### Screenings
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | /api/screenings | List all user screenings |
| GET | /api/screenings/{id} | Get screening details |
| POST | /api/screenings | Schedule new screening |
| PUT | /api/screenings/{id} | Update screening |
| PUT | /api/screenings/{id}/complete | Mark screening as completed |
| PUT | /api/screenings/{id}/cancel | Cancel screening |
| POST | /api/screenings/{id}/results | Record screening results |
| GET | /api/screenings/overdue | Get overdue screenings |
| GET | /api/screenings/upcoming | Get upcoming screenings |

## Domain Events

| Event | Trigger | Key Data |
|-------|---------|----------|
| ScreeningScheduled | New screening created | screeningId, userId, screeningType, appointmentDate, providerId |
| ScreeningCompleted | Screening marked done | screeningId, completionDate, resultsStatus, nextDueDate |
| ScreeningResultsReceived | Results recorded | screeningId, resultSummary, abnormalFlag, followUpRequired |
| ScreeningCancelled | Screening cancelled | screeningId, reason, rescheduledFlag |
| ScreeningOverdue | System detects overdue | screeningType, userId, dueDate, daysOverdue |

## Database Schema

```sql
CREATE TABLE Screenings (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    ScreeningTypeId UNIQUEIDENTIFIER NOT NULL,
    ProviderId UNIQUEIDENTIFIER,
    Status NVARCHAR(50) NOT NULL, -- Scheduled, Completed, Cancelled
    ScheduledDate DATETIME2,
    CompletedDate DATETIME2,
    NextDueDate DATETIME2,
    Location NVARCHAR(500),
    PreparationNotes NVARCHAR(MAX),
    Cost DECIMAL(10,2),
    InsuranceClaimed BIT,
    CancellationReason NVARCHAR(500),
    CreatedAt DATETIME2 NOT NULL,
    UpdatedAt DATETIME2 NOT NULL
);

CREATE TABLE ScreeningResults (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    ScreeningId UNIQUEIDENTIFIER NOT NULL,
    ResultDate DATETIME2 NOT NULL,
    Summary NVARCHAR(MAX),
    AbnormalFindings BIT DEFAULT 0,
    FollowUpRequired BIT DEFAULT 0,
    ProviderNotes NVARCHAR(MAX),
    DocumentUrl NVARCHAR(1000),
    CreatedAt DATETIME2 NOT NULL
);

CREATE TABLE ScreeningTypes (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    Name NVARCHAR(200) NOT NULL,
    Description NVARCHAR(MAX),
    Category NVARCHAR(100),
    DefaultIntervalMonths INT,
    PreparationInstructions NVARCHAR(MAX),
    GenderSpecific NVARCHAR(20), -- Male, Female, All
    MinAge INT,
    MaxAge INT,
    IsActive BIT DEFAULT 1
);
```

## Business Rules

1. Screening intervals follow clinical guidelines by type
2. Overdue status triggers after due date + grace period
3. Results can only be recorded for completed screenings
4. Cancelled screenings retain history for audit
5. Next due date calculated from completion date, not scheduled date
