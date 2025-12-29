# Dose Tracking - Backend Requirements

## Overview
Tracks medication doses as taken, missed, or delayed, prevents double-dosing, and maintains adherence history.

## Domain Model

### Dose Entity
- **DoseId**: Guid
- **MedicationId**: Guid
- **UserId**: Guid
- **ScheduledTime**: DateTime
- **ActualTime**: DateTime?
- **Status**: DoseStatus (Scheduled, Taken, Missed, Delayed, Skipped)
- **DoseAmount**: string
- **Notes**: string
- **TakenWithFood**: bool?
- **CreatedAt**: DateTime

### DoseStatus Enum
- Scheduled, Taken, Missed, Delayed, Skipped

## Commands

### LogDoseTakenCommand
- Validates MedicationId and DoseId
- Checks for double-dose risk (minimum interval)
- Records actual time taken
- Raises **DoseTaken** event
- Updates inventory
- Dismisses active reminders

### RecordMissedDoseCommand
- Validates dose window passed
- Marks dose as missed
- Raises **DoseMissed** event
- Triggers make-up dose suggestion
- Updates adherence tracking

### CheckDoubleDoseCommand
- Validates last dose time
- Checks minimum interval requirement
- Raises **DoubleDoseAlert** if too soon
- Prevents duplicate dose logging

## Queries

### GetDoseHistoryQuery
- Returns doses for medication/user/date range
- Supports filtering by status
- Includes adherence calculations
- Paginated results

### GetUpcomingDosesQuery
- Returns scheduled doses for next 24 hours
- Includes medication details
- Sorted by scheduled time

### GetMissedDosesQuery
- Returns missed doses requiring attention
- Includes make-up dose suggestions
- Filtered by medication or date range

## Domain Events

```csharp
public class DoseTaken : DomainEvent
{
    public Guid DoseId { get; set; }
    public Guid MedicationId { get; set; }
    public DateTime ScheduledTime { get; set; }
    public DateTime ActualTime { get; set; }
    public string DoseAmount { get; set; }
    public Guid UserId { get; set; }
}

public class DoseMissed : DomainEvent
{
    public Guid DoseId { get; set; }
    public Guid MedicationId { get; set; }
    public DateTime ScheduledTime { get; set; }
    public Guid UserId { get; set; }
}

public class DoubleDoseAlert : DomainEvent
{
    public Guid MedicationId { get; set; }
    public DateTime LastDoseTime { get; set; }
    public DateTime AttemptTime { get; set; }
    public decimal MinimumInterval { get; set; }
    public Guid UserId { get; set; }
}
```

## API Endpoints

### POST /api/doses/taken
- Logs dose as taken
- Request: { doseId, actualTime, notes }
- Returns: 200 OK

### POST /api/doses/missed
- Records missed dose
- Request: { doseId }
- Returns: 200 OK

### GET /api/doses/history
- Retrieves dose history
- Query params: medicationId, startDate, endDate, status
- Returns: Paginated dose list

### GET /api/doses/upcoming
- Gets next 24 hours of doses
- Returns: List of scheduled doses

## Business Rules
1. Cannot log dose more than 12 hours early
2. Must check double-dose risk before logging
3. Missed doses auto-marked after 2-hour window
4. Adherence calculated as (doses taken / doses scheduled) × 100
