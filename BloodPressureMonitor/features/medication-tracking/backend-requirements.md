# Backend Requirements - Medication Tracking

## API Endpoints

### POST /api/medications
Add BP medication to profile.
```json
Request: {
  "name": "Lisinopril",
  "dosage": "10mg",
  "frequency": "once_daily",
  "timeOfDay": "08:00",
  "startDate": "2025-12-01"
}
```

### POST /api/medications/doses
Log medication dose taken.
```json
Request: {
  "medicationId": "uuid",
  "takenAt": "2025-12-29T08:00:00Z"
}
```
**Events:** BPMedicationRecorded

### GET /api/medications/effectiveness
Analyze medication impact on BP.
```json
Response: {
  "medicationId": "uuid",
  "preAverage": {"systolic": 145, "diastolic": 92},
  "postAverage": {"systolic": 125, "diastolic": 82},
  "effectivenessScore": 8.5,
  "improvement": {"systolic": -20, "diastolic": -10}
}
```
**Events:** MedicationEffectivenessAnalyzed

### GET /api/medications/timing-correlation
Analyze BP readings relative to medication timing.

## Domain Models

```csharp
public class Medication : AggregateRoot
{
    public Guid MedicationId { get; private set; }
    public Guid UserId { get; private set; }
    public string Name { get; private set; }
    public string Dosage { get; private set; }
    public MedicationFrequency Frequency { get; private set; }
    public TimeSpan TimeOfDay { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime? EndDate { get; private set; }
    public bool IsActive { get; private set; }
}

public class MedicationDose : Entity
{
    public Guid DoseId { get; private set; }
    public Guid MedicationId { get; private set; }
    public DateTime ScheduledTime { get; private set; }
    public DateTime? TakenAt { get; private set; }
    public bool WasTaken { get; private set; }
}
```

## Business Rules

### BR-MT-001: Effectiveness Analysis Criteria
- Require at least 14 readings before medication start
- Require at least 14 readings after medication start (2+ weeks)
- Compare 30-day averages pre and post medication

### BR-MT-002: Timing Correlation
- Track readings taken 0-2h, 2-4h, 4-8h, 8+ hours after dose
- Identify peak effectiveness window
- Detect trough periods (before next dose)

### BR-MT-003: Adherence Tracking
- Calculate adherence percentage
- Alert if doses missed 2+ consecutive days
- Include in doctor reports

## Database Schema

```sql
CREATE TABLE Medications (
    MedicationId UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    Name NVARCHAR(200) NOT NULL,
    Dosage NVARCHAR(50),
    Frequency NVARCHAR(50),
    TimeOfDay TIME,
    StartDate DATE NOT NULL,
    EndDate DATE NULL,
    IsActive BIT DEFAULT 1,
    CreatedAt DATETIME2 DEFAULT GETUTCDATE()
);

CREATE TABLE MedicationDoses (
    DoseId UNIQUEIDENTIFIER PRIMARY KEY,
    MedicationId UNIQUEIDENTIFIER NOT NULL,
    ScheduledTime DATETIME2 NOT NULL,
    TakenAt DATETIME2 NULL,
    WasTaken BIT DEFAULT 0,
    FOREIGN KEY (MedicationId) REFERENCES Medications(MedicationId)
);
```
