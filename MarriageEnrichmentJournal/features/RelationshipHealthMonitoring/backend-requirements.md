# Backend Requirements - Relationship Health Monitoring

## API Endpoints

### POST /api/mood-logs
Log current relationship mood/satisfaction.

**Request Body**:
```json
{
  "moodRating": "int (1-10) (required)",
  "contributingFactors": ["string"],
  "notes": "string (optional, max 500 chars)"
}
```

**Response**: 201 Created

### GET /api/mood-logs
Retrieve mood log history.

**Query Parameters**:
- `startDate`, `endDate`
- `userId` (optional, for viewing spouse's shared logs)
- `page`, `pageSize`

**Response**: 200 OK

### GET /api/relationship-health/trends
Get mood trend analysis.

**Query Parameters**:
- `period` (7days|30days|90days|all)

**Response**: 200 OK
```json
{
  "trendDirection": "upward|downward|stable",
  "averageRating": 7.5,
  "trendStrength": "weak|moderate|strong",
  "contributingFactors": {
    "positive": ["string"],
    "negative": ["string"]
  },
  "dataPoints": [...],
  "analysisDate": "datetime"
}
```

### GET /api/relationship-health/patterns
Get identified positive patterns.

**Response**: 200 OK
```json
{
  "patterns": [
    {
      "patternId": "guid",
      "patternType": "string",
      "frequency": "string",
      "examples": ["string"],
      "bothPartnersContribution": boolean,
      "identifiedAt": "datetime"
    }
  ]
}
```

### GET /api/relationship-health/dashboard
Get comprehensive health metrics.

**Response**: 200 OK
```json
{
  "currentMoodAverage": 8.0,
  "trendDirection": "upward",
  "gratitudeCount": 45,
  "appreciationCount": 32,
  "reflectionCount": 15,
  "activeGoals": 3,
  "recentWins": 5,
  "streakDays": 12,
  "healthScore": 85
}
```

### POST /api/relationship-health/interventions
Request suggested interventions.

**Response**: 200 OK
```json
{
  "suggestions": [
    {
      "type": "exercise|resource|tip",
      "title": "string",
      "description": "string",
      "priority": "high|medium|low"
    }
  ]
}
```

## Domain Events

### RelationshipMoodLogged
### MoodTrendDetected
### PositivePatternIdentified

## Database Schema

### MoodLogs Table
```sql
CREATE TABLE MoodLogs (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    UserId UNIQUEIDENTIFIER NOT NULL,
    MoodRating INT NOT NULL CHECK (MoodRating BETWEEN 1 AND 10),
    ContributingFactors NVARCHAR(MAX) NULL,
    Notes NVARCHAR(500) NULL,
    LoggedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);

CREATE INDEX IX_MoodLogs_UserId_LoggedAt ON MoodLogs(UserId, LoggedAt);
```

### MoodTrends Table
```sql
CREATE TABLE MoodTrends (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    CoupleId UNIQUEIDENTIFIER NOT NULL,
    TrendDirection VARCHAR(20) NOT NULL,
    Duration INT NOT NULL,
    Severity VARCHAR(20) NULL,
    DetectedAt DATETIME2 NOT NULL,
    IsResolved BIT NOT NULL DEFAULT 0,
    FOREIGN KEY (CoupleId) REFERENCES Couples(Id)
);
```

### PositivePatterns Table
```sql
CREATE TABLE PositivePatterns (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    CoupleId UNIQUEIDENTIFIER NOT NULL,
    PatternType VARCHAR(100) NOT NULL,
    Frequency VARCHAR(50) NOT NULL,
    Examples NVARCHAR(MAX) NULL,
    BothPartnersContribution BIT NOT NULL,
    IdentifiedAt DATETIME2 NOT NULL,
    FOREIGN KEY (CoupleId) REFERENCES Couples(Id)
);
```

### RelationshipHealthScores Table
```sql
CREATE TABLE RelationshipHealthScores (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    CoupleId UNIQUEIDENTIFIER NOT NULL,
    HealthScore INT NOT NULL CHECK (HealthScore BETWEEN 0 AND 100),
    CalculationDate DATETIME2 NOT NULL,
    ComponentScores NVARCHAR(MAX) NOT NULL,
    FOREIGN KEY (CoupleId) REFERENCES Couples(Id)
);
```

## Business Logic

### Trend Detection
- Analyze mood logs from last 30 days
- Detect upward/downward trends using linear regression
- Calculate trend strength
- Identify common contributing factors
- Alert if downward trend detected

### Health Score Calculation
- Weight factors: Mood average (30%), Engagement (25%), Gratitude (20%), Communication (15%), Growth (10%)
- Update daily
- Normalize to 0-100 scale

### Pattern Identification
- Analyze activities that correlate with high mood ratings
- Identify recurring positive behaviors
- Require pattern to occur 3+ times to qualify

### Early Warning System
- Trigger alert if mood average drops below 5 for 7 days
- Suggest interventions based on specific issues
- Escalate to counseling suggestion if severe

## Performance Requirements
- Mood log creation: <100ms
- Trend analysis: <1 second
- Dashboard load: <2 seconds
- Pattern detection: Run daily during off-peak hours
