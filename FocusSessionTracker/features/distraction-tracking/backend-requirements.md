# Distraction Tracking - Backend Requirements

## Overview
Backend services for tracking, categorizing, and analyzing distractions during focus sessions. Supports both internal (self-generated) and external (environmental) distractions, as well as distraction resistance tracking.

---

## API Endpoints

### POST /api/distractions
**Description**: Log a distraction during a session

**Request Body**:
```json
{
  "sessionId": "uuid",
  "distractionType": "internal | external",
  "source": "mind_wandering | urge | thought | person | notification | noise | other",
  "description": "Brief description of distraction",
  "duration": 30,
  "impactLevel": 1,
  "trigger": "What prompted the distraction",
  "preventable": true
}
```

**Response**: `201 Created`
```json
{
  "distractionId": "uuid",
  "sessionId": "uuid",
  "distractionType": "internal",
  "source": "mind_wandering",
  "timestamp": "ISO8601",
  "impactLevel": 1
}
```

**Domain Events**:
- `DistractionLogged`
- `InternalDistractionRecorded` (if type is internal)
- `ExternalDistractionRecorded` (if type is external)

---

### POST /api/distractions/resistance
**Description**: Record successful distraction resistance

**Request Body**:
```json
{
  "sessionId": "uuid",
  "distractionTypeResisted": "notification | social_media | email | other",
  "resistanceStrategy": "deep_breath | noted_for_later | blocked_app | other",
  "description": "What was resisted and how"
}
```

**Response**: `201 Created`
```json
{
  "resistanceId": "uuid",
  "sessionId": "uuid",
  "distractionTypeResisted": "notification",
  "timestamp": "ISO8601"
}
```

**Domain Event**: `DistractionResisted`

---

### GET /api/distractions
**Description**: Get user's distractions with filtering

**Query Parameters**:
- `sessionId`: UUID (optional)
- `startDate`: ISO8601 date
- `endDate`: ISO8601 date
- `distractionType`: internal | external
- `source`: specific source filter
- `page`: number
- `limit`: number

**Response**: `200 OK` with paginated distractions

---

### GET /api/distractions/{distractionId}
**Description**: Get distraction details

**Response**: `200 OK` with distraction object

---

### GET /api/distractions/analytics/patterns
**Description**: Get distraction patterns analysis

**Query Parameters**:
- `period`: day | week | month
- `startDate`: ISO8601 date
- `endDate`: ISO8601 date

**Response**: `200 OK`
```json
{
  "totalDistractions": 45,
  "internalCount": 28,
  "externalCount": 17,
  "averageImpact": 2.3,
  "mostCommonSources": [
    {"source": "mind_wandering", "count": 15},
    {"source": "notification", "count": 12}
  ],
  "peakDistractionTimes": [
    {"hour": 14, "count": 8},
    {"hour": 10, "count": 6}
  ],
  "resistanceSuccessRate": 0.65
}
```

---

### GET /api/distractions/analytics/trends
**Description**: Get distraction trends over time

**Query Parameters**:
- `period`: week | month | quarter
- `metric`: count | impact | resistance_rate

**Response**: `200 OK` with time-series data

---

### PUT /api/distractions/{distractionId}
**Description**: Update distraction details (e.g., add notes)

**Request Body**:
```json
{
  "notes": "Added reflection on what caused this",
  "impactLevel": 2
}
```

**Response**: `200 OK`

---

### DELETE /api/distractions/{distractionId}
**Description**: Delete a distraction record

**Response**: `204 No Content`

---

## Database Schema

### Distractions Table
```sql
CREATE TABLE Distractions (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    SessionId UNIQUEIDENTIFIER NOT NULL,
    UserId UNIQUEIDENTIFIER NOT NULL,
    DistractionType VARCHAR(20) NOT NULL, -- internal, external
    Source VARCHAR(50) NOT NULL,
    Description NVARCHAR(1000),
    Duration INT, -- seconds
    ImpactLevel INT NOT NULL, -- 1-5 scale
    Trigger NVARCHAR(500),
    Preventable BIT,
    Notes NVARCHAR(MAX),
    LoggedAt DATETIME2 NOT NULL,
    CreatedAt DATETIME2 DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2,
    FOREIGN KEY (SessionId) REFERENCES Sessions(Id),
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);

CREATE INDEX IX_Distractions_SessionId ON Distractions(SessionId);
CREATE INDEX IX_Distractions_UserId_LoggedAt ON Distractions(UserId, LoggedAt);
CREATE INDEX IX_Distractions_DistractionType ON Distractions(DistractionType);
```

### DistractionResistances Table
```sql
CREATE TABLE DistractionResistances (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    SessionId UNIQUEIDENTIFIER NOT NULL,
    UserId UNIQUEIDENTIFIER NOT NULL,
    DistractionTypeResisted VARCHAR(50) NOT NULL,
    ResistanceStrategy VARCHAR(50) NOT NULL,
    Description NVARCHAR(1000),
    ResistedAt DATETIME2 NOT NULL,
    CreatedAt DATETIME2 DEFAULT GETUTCDATE(),
    FOREIGN KEY (SessionId) REFERENCES Sessions(Id),
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);

CREATE INDEX IX_DistractionResistances_SessionId ON DistractionResistances(SessionId);
CREATE INDEX IX_DistractionResistances_UserId_ResistedAt ON DistractionResistances(UserId, ResistedAt);
```

### DistractionPatterns Table
```sql
CREATE TABLE DistractionPatterns (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    PatternType VARCHAR(50) NOT NULL, -- recurring_source, time_of_day, session_type_correlation
    PatternDescription NVARCHAR(MAX) NOT NULL,
    Frequency DECIMAL(5,2),
    Severity DECIMAL(5,2),
    DetectedAt DATETIME2 NOT NULL,
    IsActive BIT DEFAULT 1,
    MitigationSuggestions NVARCHAR(MAX),
    CreatedAt DATETIME2 DEFAULT GETUTCDATE(),
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);
```

---

## Domain Events

### DistractionLogged
```csharp
public record DistractionLogged(
    Guid DistractionId,
    Guid SessionId,
    Guid UserId,
    string DistractionType,
    string Source,
    int Duration,
    int ImpactLevel,
    DateTime Timestamp
);
```

### InternalDistractionRecorded
```csharp
public record InternalDistractionRecorded(
    Guid DistractionId,
    Guid SessionId,
    string Nature, // mind_wandering, urge, thought
    string Trigger,
    int ImpactLevel,
    DateTime Timestamp
);
```

### ExternalDistractionRecorded
```csharp
public record ExternalDistractionRecorded(
    Guid DistractionId,
    Guid SessionId,
    string Source, // person, notification, noise
    bool Preventable,
    int ImpactLevel,
    DateTime Timestamp
);
```

### DistractionResisted
```csharp
public record DistractionResisted(
    Guid ResistanceId,
    Guid SessionId,
    string DistractionTypeResisted,
    string ResistanceStrategy,
    DateTime Timestamp
);
```

### DistractionPatternIdentified
```csharp
public record DistractionPatternIdentified(
    Guid PatternId,
    Guid UserId,
    string PatternType,
    string PatternDescription,
    decimal Frequency,
    DateTime Timestamp
);
```

---

## Business Rules

1. **Impact Level**: Must be 1-5 scale
   - 1: Minimal (glanced at phone, 5-10 seconds)
   - 2: Low (brief check, 10-30 seconds)
   - 3: Medium (responded to message, 30-60 seconds)
   - 4: High (short conversation, 1-3 minutes)
   - 5: Severe (extended interruption, 3+ minutes)

2. **Distraction Types**:
   - **Internal Sources**: mind_wandering, urge, thought, daydream, anxiety, hunger
   - **External Sources**: person, notification, phone_call, noise, email, social_media, message

3. **Duration Tracking**: Measured in seconds, max 600 (10 minutes)

4. **Session Correlation**: Each distraction must be linked to an active or recent session

5. **Focus Score Impact**: Each distraction reduces focus score by `impactLevel * 5` points

6. **Pattern Detection**: Automated analysis runs:
   - Daily: Detects same-day patterns
   - Weekly: Identifies recurring weekly patterns
   - Monthly: Long-term trend analysis

7. **Resistance Tracking**: Minimum 3 resistances to establish pattern recognition

8. **Privacy**: Distraction descriptions are optional to reduce logging friction

---

## Integration Points

- **Session Service**: Link distractions to active sessions
- **Analytics Service**: Calculate distraction trends and patterns
- **Notification Service**: Alert users to identified patterns
- **Focus Score Calculator**: Adjust session scores based on distractions
- **Recommendation Engine**: Suggest mitigation strategies

---

## Performance Considerations

- Distraction logging must be < 100ms response time (quick capture)
- Pattern analysis runs asynchronously (not blocking)
- Analytics queries use indexed fields for fast retrieval
- Real-time distraction count updates via WebSocket/SignalR

---

## Security & Privacy

- Users can only access their own distraction data
- Distraction descriptions are encrypted at rest
- Bulk export requires authentication and rate limiting
- Pattern insights are user-private, not shared

---

## Validation Rules

| Field | Rule |
|-------|------|
| DistractionType | Must be "internal" or "external" |
| Source | Must be from predefined list |
| Description | Max 1000 characters, optional |
| Duration | 0-600 seconds |
| ImpactLevel | 1-5 required |
| SessionId | Must reference valid, recent session |
