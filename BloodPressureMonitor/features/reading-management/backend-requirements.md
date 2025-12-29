# Backend Requirements - Reading Management

## Overview
The Reading Management feature handles the recording, storage, validation, and retrieval of blood pressure readings and related measurement data.

## API Endpoints

### POST /api/readings
Create a new blood pressure reading.

**Request Body:**
```json
{
  "systolic": 120,
  "diastolic": 80,
  "pulse": 72,
  "measurementTime": "2025-12-29T08:30:00Z",
  "armUsed": "left",
  "context": "resting",
  "posture": "sitting",
  "timeSinceActivity": 30,
  "protocolFollowed": true
}
```

**Response:** 201 Created
```json
{
  "readingId": "uuid",
  "systolic": 120,
  "diastolic": 80,
  "pulse": 72,
  "measurementTime": "2025-12-29T08:30:00Z",
  "createdAt": "2025-12-29T08:30:15Z"
}
```

**Domain Events Triggered:**
- BloodPressureRecorded
- PulseRecorded
- MeasurementContextLogged
- ProperMeasurementTechniqueConfirmed (if protocol followed)
- MorningReadingRecorded or EveningReadingRecorded (based on time)

### GET /api/readings
Retrieve user's blood pressure readings with pagination and filtering.

**Query Parameters:**
- `startDate` (optional): ISO 8601 date
- `endDate` (optional): ISO 8601 date
- `limit` (default: 50, max: 100)
- `offset` (default: 0)
- `sortOrder` (asc/desc, default: desc)

**Response:** 200 OK
```json
{
  "readings": [
    {
      "readingId": "uuid",
      "systolic": 120,
      "diastolic": 80,
      "pulse": 72,
      "measurementTime": "2025-12-29T08:30:00Z",
      "context": "resting",
      "armUsed": "left"
    }
  ],
  "total": 150,
  "limit": 50,
  "offset": 0
}
```

### GET /api/readings/{readingId}
Retrieve a specific reading by ID.

**Response:** 200 OK
```json
{
  "readingId": "uuid",
  "systolic": 120,
  "diastolic": 80,
  "pulse": 72,
  "measurementTime": "2025-12-29T08:30:00Z",
  "context": "resting",
  "armUsed": "left",
  "posture": "sitting",
  "timeSinceActivity": 30,
  "protocolFollowed": true,
  "createdAt": "2025-12-29T08:30:15Z"
}
```

### PUT /api/readings/{readingId}
Update an existing reading (within 24 hours of creation).

**Request Body:** Same as POST
**Response:** 200 OK

### DELETE /api/readings/{readingId}
Soft delete a reading.

**Response:** 204 No Content

**Domain Events Triggered:**
- ReadingDeleted

### POST /api/readings/multiple
Record multiple consecutive readings and calculate average.

**Request Body:**
```json
{
  "readings": [
    {"systolic": 122, "diastolic": 82, "pulse": 70},
    {"systolic": 118, "diastolic": 78, "pulse": 72},
    {"systolic": 120, "diastolic": 80, "pulse": 71}
  ],
  "measurementTime": "2025-12-29T08:30:00Z",
  "context": "resting"
}
```

**Response:** 201 Created
```json
{
  "sessionId": "uuid",
  "individualReadings": [...],
  "averagedResult": {
    "systolic": 120,
    "diastolic": 80,
    "pulse": 71
  },
  "variance": {
    "systolic": 2.0,
    "diastolic": 2.0,
    "pulse": 1.0
  }
}
```

**Domain Events Triggered:**
- MultipleReadingsAveraged
- BloodPressureRecorded (for averaged result)

### POST /api/readings/sync
Import readings from connected BP monitoring devices.

**Request Body:**
```json
{
  "deviceId": "device-uuid",
  "readings": [
    {
      "systolic": 120,
      "diastolic": 80,
      "pulse": 72,
      "measurementTime": "2025-12-29T08:30:00Z"
    }
  ]
}
```

**Response:** 201 Created
```json
{
  "importedCount": 5,
  "skippedCount": 2,
  "readingIds": ["uuid1", "uuid2", ...]
}
```

### GET /api/readings/statistics
Get statistical summary of readings.

**Query Parameters:**
- `startDate` (required)
- `endDate` (required)

**Response:** 200 OK
```json
{
  "period": {
    "startDate": "2025-12-01",
    "endDate": "2025-12-29"
  },
  "totalReadings": 56,
  "averages": {
    "systolic": 125,
    "diastolic": 82,
    "pulse": 73
  },
  "ranges": {
    "systolic": {"min": 110, "max": 145},
    "diastolic": {"min": 70, "max": 95},
    "pulse": {"min": 60, "max": 85}
  }
}
```

## Domain Models

### Reading Aggregate
```csharp
public class Reading : AggregateRoot
{
    public Guid ReadingId { get; private set; }
    public Guid UserId { get; private set; }
    public int Systolic { get; private set; }
    public int Diastolic { get; private set; }
    public int? Pulse { get; private set; }
    public DateTime MeasurementTime { get; private set; }
    public string ArmUsed { get; private set; }
    public MeasurementContext Context { get; private set; }
    public bool IsDeleted { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public void UpdateReading(int systolic, int diastolic, int? pulse);
    public void Delete();
    public void ValidateReading();
}
```

### MeasurementContext Value Object
```csharp
public class MeasurementContext : ValueObject
{
    public string ContextType { get; private set; } // resting, post-exercise, stressed
    public int? TimeSinceActivity { get; private set; } // minutes
    public string Posture { get; private set; } // sitting, standing, lying
    public bool ProtocolFollowed { get; private set; }
}
```

### MeasurementSession Aggregate
```csharp
public class MeasurementSession : AggregateRoot
{
    public Guid SessionId { get; private set; }
    public Guid UserId { get; private set; }
    public List<Reading> IndividualReadings { get; private set; }
    public Reading AveragedResult { get; private set; }
    public Dictionary<string, double> Variance { get; private set; }
    public DateTime SessionTime { get; private set; }

    public void CalculateAverage();
    public void CalculateVariance();
}
```

## Business Rules

### BR-RM-001: Valid Blood Pressure Range
- Systolic must be between 50 and 250 mmHg
- Diastolic must be between 30 and 150 mmHg
- Systolic must be greater than diastolic
- If values are outside typical ranges (70-190 systolic, 40-100 diastolic), flag for potential error

### BR-RM-002: Valid Pulse Range
- Pulse must be between 30 and 250 bpm
- If pulse is outside normal range (60-100 bpm), flag as abnormal

### BR-RM-003: Measurement Timing
- Morning reading window: 6:00 AM - 10:00 AM
- Evening reading window: 6:00 PM - 10:00 PM
- Readings outside these windows are categorized as "other"

### BR-RM-004: Reading Update Window
- Readings can only be edited within 24 hours of creation
- After 24 hours, readings become immutable (except soft delete)

### BR-RM-005: Multiple Readings Session
- Session must contain 2-5 individual readings
- Readings in a session must be taken within 15 minutes of each other
- If variance is high (>10 mmHg systolic or >5 mmHg diastolic), flag for review

### BR-RM-006: Device Sync Deduplication
- If a reading with same timestamp and values exists, skip import
- If timestamp exists but values differ slightly (<5 mmHg), consider as duplicate

### BR-RM-007: Measurement Error Detection
- If reading differs from recent average by >30 mmHg, trigger MeasurementErrorDetected event
- If reading is implausible (e.g., systolic < diastolic), reject with error

## Data Validation

### Input Validation
- All required fields must be present
- Numeric values must be positive integers
- Dates must be valid ISO 8601 format
- Dates cannot be in the future
- Context type must be from allowed list: resting, post-exercise, stressed, medication-check, other

### Business Validation
- Apply all business rules listed above
- Validate user authentication and authorization
- Ensure reading belongs to authenticated user

## Event Publishing

### BloodPressureRecorded
```json
{
  "eventId": "uuid",
  "eventType": "BloodPressureRecorded",
  "timestamp": "2025-12-29T08:30:15Z",
  "data": {
    "readingId": "uuid",
    "userId": "uuid",
    "systolic": 120,
    "diastolic": 80,
    "pulse": 72,
    "measurementTime": "2025-12-29T08:30:00Z",
    "context": "resting",
    "armUsed": "left"
  }
}
```

### PulseRecorded
```json
{
  "eventId": "uuid",
  "eventType": "PulseRecorded",
  "timestamp": "2025-12-29T08:30:15Z",
  "data": {
    "readingId": "uuid",
    "userId": "uuid",
    "pulse": 72,
    "rhythmRegularity": "regular",
    "measurementTime": "2025-12-29T08:30:00Z"
  }
}
```

### MeasurementErrorDetected
```json
{
  "eventId": "uuid",
  "eventType": "MeasurementErrorDetected",
  "timestamp": "2025-12-29T08:30:15Z",
  "data": {
    "readingId": "uuid",
    "userId": "uuid",
    "errorType": "deviation_from_average",
    "expectedRange": {"systolic": "110-130", "diastolic": "70-85"},
    "actualValues": {"systolic": 165, "diastolic": 95},
    "suggestionToRemeasure": true
  }
}
```

## Database Schema

### Readings Table
```sql
CREATE TABLE Readings (
    ReadingId UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    Systolic INT NOT NULL,
    Diastolic INT NOT NULL,
    Pulse INT NULL,
    MeasurementTime DATETIME2 NOT NULL,
    ArmUsed NVARCHAR(10),
    ContextType NVARCHAR(50),
    TimeSinceActivity INT NULL,
    Posture NVARCHAR(20),
    ProtocolFollowed BIT DEFAULT 0,
    IsDeleted BIT DEFAULT 0,
    CreatedAt DATETIME2 DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2 DEFAULT GETUTCDATE(),
    FOREIGN KEY (UserId) REFERENCES Users(UserId),
    INDEX IX_Readings_UserId_MeasurementTime (UserId, MeasurementTime DESC)
);
```

### MeasurementSessions Table
```sql
CREATE TABLE MeasurementSessions (
    SessionId UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    AveragedReadingId UNIQUEIDENTIFIER,
    SessionTime DATETIME2 NOT NULL,
    SystolicVariance DECIMAL(5,2),
    DiastolicVariance DECIMAL(5,2),
    PulseVariance DECIMAL(5,2),
    CreatedAt DATETIME2 DEFAULT GETUTCDATE(),
    FOREIGN KEY (UserId) REFERENCES Users(UserId),
    FOREIGN KEY (AveragedReadingId) REFERENCES Readings(ReadingId)
);
```

### SessionReadings Table (Many-to-Many)
```sql
CREATE TABLE SessionReadings (
    SessionId UNIQUEIDENTIFIER,
    ReadingId UNIQUEIDENTIFIER,
    ReadingOrder INT,
    PRIMARY KEY (SessionId, ReadingId),
    FOREIGN KEY (SessionId) REFERENCES MeasurementSessions(SessionId),
    FOREIGN KEY (ReadingId) REFERENCES Readings(ReadingId)
);
```

## Error Handling

### Error Codes
- `RM-001`: Invalid blood pressure values
- `RM-002`: Invalid pulse value
- `RM-003`: Future date not allowed
- `RM-004`: Reading not found
- `RM-005`: Unauthorized access
- `RM-006`: Reading update window expired
- `RM-007`: Device sync failed
- `RM-008`: Duplicate reading detected
- `RM-009`: Invalid context type
- `RM-010`: Session validation failed

### Error Response Format
```json
{
  "error": {
    "code": "RM-001",
    "message": "Invalid blood pressure values: systolic must be greater than diastolic",
    "details": {
      "systolic": 80,
      "diastolic": 120
    }
  }
}
```

## Performance Considerations

- Index on (UserId, MeasurementTime DESC) for fast retrieval of recent readings
- Cache user's last 7 days of readings for quick statistics
- Use batch inserts for device sync operations
- Implement read replicas for reporting queries
- Archive readings older than 2 years to separate table

## Security

- Validate user owns reading before allowing access or modification
- Sanitize all input to prevent SQL injection
- Rate limit API endpoints (max 100 readings per hour)
- Log all reading creation, updates, and deletions for audit trail
- Encrypt sensitive health data at rest
