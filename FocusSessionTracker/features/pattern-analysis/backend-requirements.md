# Pattern Analysis - Backend Requirements

## Overview
Backend services for analyzing focus session patterns, identifying optimal session lengths, detecting peak focus times, recognizing distraction patterns, and tracking productivity trends.

---

## API Endpoints

### GET /api/analytics/patterns
**Description**: Get all detected patterns for a user

**Query Parameters**:
- `patternType`: optimal-length | peak-time | distraction | productivity-trend
- `startDate`: ISO8601 date
- `endDate`: ISO8601 date
- `confidence`: minimum confidence level (0-100)

**Response**: `200 OK`
```json
{
  "patterns": [
    {
      "id": "uuid",
      "userId": "uuid",
      "patternType": "optimal-length",
      "detectedAt": "ISO8601",
      "confidence": 85,
      "data": {},
      "recommendations": []
    }
  ],
  "totalCount": 15
}
```

---

### GET /api/analytics/optimal-session-length
**Description**: Get user's optimal session duration analysis

**Response**: `200 OK`
```json
{
  "optimalDuration": 35,
  "confidenceScore": 87,
  "successRateByDuration": [
    {"duration": 25, "successRate": 75, "sampleSize": 45},
    {"duration": 35, "successRate": 92, "sampleSize": 38},
    {"duration": 45, "successRate": 68, "sampleSize": 22}
  ],
  "recommendation": "Based on 105 sessions, 35-minute sessions show highest completion rate",
  "detectedAt": "ISO8601"
}
```

**Domain Event**: `OptimalSessionLengthIdentified`

---

### GET /api/analytics/peak-focus-time
**Description**: Get user's peak productivity time windows

**Response**: `200 OK`
```json
{
  "peakTimeWindows": [
    {
      "startHour": 9,
      "endHour": 11,
      "avgFocusScore": 88,
      "consistencyScore": 85,
      "sessionsAnalyzed": 42
    },
    {
      "startHour": 14,
      "endHour": 16,
      "avgFocusScore": 82,
      "consistencyScore": 78,
      "sessionsAnalyzed": 35
    }
  ],
  "recommendation": "Schedule deep work sessions between 9-11 AM for optimal focus",
  "detectedAt": "ISO8601"
}
```

**Domain Event**: `PeakFocusTimeDetected`

---

### GET /api/analytics/distraction-patterns
**Description**: Get identified distraction patterns

**Response**: `200 OK`
```json
{
  "patterns": [
    {
      "id": "uuid",
      "distractionType": "notification",
      "source": "email",
      "frequency": 12,
      "avgImpact": "high",
      "typicalTiming": ["10:30-11:00", "15:00-15:30"],
      "mitigation": "Enable email batching or focus mode during peak work hours"
    },
    {
      "id": "uuid",
      "distractionType": "internal",
      "source": "mind-wandering",
      "frequency": 8,
      "avgImpact": "medium",
      "typicalTiming": ["14:00-14:30"],
      "mitigation": "Consider a short break or meditation before afternoon sessions"
    }
  ],
  "totalDistractionsAnalyzed": 156,
  "analysisWindow": "last_30_days"
}
```

**Domain Event**: `DistractionPatternIdentified`

---

### GET /api/analytics/productivity-trends
**Description**: Get productivity trend analysis

**Query Parameters**:
- `period`: week | month | quarter
- `metric`: focus-score | completion-rate | session-count

**Response**: `200 OK`
```json
{
  "trend": {
    "direction": "upward",
    "magnitude": "moderate",
    "duration": 21,
    "startDate": "ISO8601",
    "endDate": "ISO8601",
    "metrics": {
      "startValue": 72,
      "endValue": 84,
      "percentChange": 16.7
    }
  },
  "contributingFactors": [
    "Consistent morning sessions",
    "Reduced notification distractions",
    "Optimal session length adoption"
  ],
  "dataPoints": [
    {"date": "ISO8601", "value": 72},
    {"date": "ISO8601", "value": 75},
    {"date": "ISO8601", "value": 78}
  ]
}
```

**Domain Event**: `ProductivityTrendDetected`

---

### POST /api/analytics/patterns/analyze
**Description**: Trigger pattern analysis for a user

**Request Body**:
```json
{
  "userId": "uuid",
  "analysisType": "all | optimal-length | peak-time | distractions | trends",
  "minSampleSize": 20
}
```

**Response**: `202 Accepted`
```json
{
  "jobId": "uuid",
  "status": "processing",
  "estimatedCompletionTime": "ISO8601"
}
```

---

### GET /api/analytics/insights
**Description**: Get personalized insights and recommendations

**Response**: `200 OK`
```json
{
  "insights": [
    {
      "id": "uuid",
      "type": "session-timing",
      "priority": "high",
      "title": "Schedule sessions during peak hours",
      "description": "You're 23% more productive between 9-11 AM",
      "actionable": true,
      "action": "Create morning session template",
      "createdAt": "ISO8601"
    },
    {
      "id": "uuid",
      "type": "distraction-prevention",
      "priority": "medium",
      "title": "Email notifications disrupt focus",
      "description": "12 distractions from email in the last week",
      "actionable": true,
      "action": "Enable focus mode to block email during sessions",
      "createdAt": "ISO8601"
    }
  ]
}
```

---

### GET /api/analytics/stats
**Description**: Get aggregate pattern statistics

**Response**: `200 OK`
```json
{
  "totalPatternsDetected": 8,
  "sessionsAnalyzed": 156,
  "analysisWindow": {
    "startDate": "ISO8601",
    "endDate": "ISO8601",
    "days": 30
  },
  "patternBreakdown": {
    "optimalLength": 1,
    "peakTimes": 2,
    "distractions": 3,
    "trends": 2
  },
  "lastAnalysisRun": "ISO8601",
  "nextScheduledAnalysis": "ISO8601"
}
```

---

## Database Schema

### Patterns Table
```sql
CREATE TABLE Patterns (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    PatternType VARCHAR(50) NOT NULL,
    DetectedAt DATETIME2 NOT NULL,
    Confidence INT NOT NULL,
    Data NVARCHAR(MAX) NOT NULL, -- JSON data
    IsActive BIT DEFAULT 1,
    CreatedAt DATETIME2 DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2,
    INDEX IX_Patterns_UserId (UserId),
    INDEX IX_Patterns_Type (PatternType, UserId)
);
```

### OptimalSessionLengths Table
```sql
CREATE TABLE OptimalSessionLengths (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    OptimalDuration INT NOT NULL,
    ConfidenceScore INT NOT NULL,
    SuccessRateData NVARCHAR(MAX), -- JSON array
    SampleSize INT NOT NULL,
    Recommendation NVARCHAR(1000),
    DetectedAt DATETIME2 NOT NULL,
    ExpiresAt DATETIME2,
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);
```

### PeakFocusTimes Table
```sql
CREATE TABLE PeakFocusTimes (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    StartHour INT NOT NULL,
    EndHour INT NOT NULL,
    AvgFocusScore DECIMAL(5,2) NOT NULL,
    ConsistencyScore INT NOT NULL,
    SessionsAnalyzed INT NOT NULL,
    DayOfWeek INT, -- NULL for all days, 0-6 for specific days
    DetectedAt DATETIME2 NOT NULL,
    FOREIGN KEY (UserId) REFERENCES Users(Id),
    INDEX IX_PeakTimes_User_Time (UserId, StartHour)
);
```

### DistractionPatterns Table
```sql
CREATE TABLE DistractionPatterns (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    DistractionType VARCHAR(50) NOT NULL,
    Source VARCHAR(100),
    Frequency INT NOT NULL,
    AvgImpact VARCHAR(20), -- low, medium, high
    TypicalTiming NVARCHAR(500), -- JSON array
    MitigationSuggestion NVARCHAR(1000),
    DetectedAt DATETIME2 NOT NULL,
    IsResolved BIT DEFAULT 0,
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);
```

### ProductivityTrends Table
```sql
CREATE TABLE ProductivityTrends (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    TrendDirection VARCHAR(20) NOT NULL, -- upward, downward, stable
    Magnitude VARCHAR(20), -- slight, moderate, significant
    Duration INT NOT NULL, -- days
    StartDate DATETIME2 NOT NULL,
    EndDate DATETIME2 NOT NULL,
    Metric VARCHAR(50) NOT NULL,
    StartValue DECIMAL(10,2) NOT NULL,
    EndValue DECIMAL(10,2) NOT NULL,
    PercentChange DECIMAL(5,2),
    ContributingFactors NVARCHAR(MAX), -- JSON array
    DetectedAt DATETIME2 NOT NULL,
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);
```

### AnalysisJobs Table
```sql
CREATE TABLE AnalysisJobs (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    AnalysisType VARCHAR(50) NOT NULL,
    Status VARCHAR(20) NOT NULL, -- queued, processing, completed, failed
    StartedAt DATETIME2,
    CompletedAt DATETIME2,
    ResultData NVARCHAR(MAX), -- JSON result
    ErrorMessage NVARCHAR(MAX),
    CreatedAt DATETIME2 DEFAULT GETUTCDATE()
);
```

### Insights Table
```sql
CREATE TABLE Insights (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    PatternId UNIQUEIDENTIFIER,
    InsightType VARCHAR(50) NOT NULL,
    Priority VARCHAR(20), -- low, medium, high
    Title NVARCHAR(200) NOT NULL,
    Description NVARCHAR(1000),
    IsActionable BIT DEFAULT 0,
    ActionText NVARCHAR(500),
    IsDismissed BIT DEFAULT 0,
    CreatedAt DATETIME2 DEFAULT GETUTCDATE(),
    ExpiresAt DATETIME2,
    FOREIGN KEY (UserId) REFERENCES Users(Id),
    FOREIGN KEY (PatternId) REFERENCES Patterns(Id)
);
```

---

## Domain Events

### OptimalSessionLengthIdentified
```csharp
public record OptimalSessionLengthIdentified(
    Guid PatternId,
    Guid UserId,
    int OptimalDuration,
    int ConfidenceScore,
    Dictionary<int, decimal> SuccessRateByDuration,
    int SampleSize,
    string Recommendation,
    DateTime Timestamp
);
```

### PeakFocusTimeDetected
```csharp
public record PeakFocusTimeDetected(
    Guid PatternId,
    Guid UserId,
    List<TimeWindow> PeakWindows,
    int ConsistencyScore,
    string Recommendation,
    DateTime Timestamp
);

public record TimeWindow(
    int StartHour,
    int EndHour,
    decimal AvgFocusScore,
    int SessionsAnalyzed
);
```

### DistractionPatternIdentified
```csharp
public record DistractionPatternIdentified(
    Guid PatternId,
    Guid UserId,
    string DistractionType,
    string Source,
    int Frequency,
    string AvgImpact,
    List<string> TypicalTiming,
    string MitigationSuggestion,
    DateTime Timestamp
);
```

### ProductivityTrendDetected
```csharp
public record ProductivityTrendDetected(
    Guid TrendId,
    Guid UserId,
    string Direction, // upward, downward, stable
    string Magnitude, // slight, moderate, significant
    int DurationDays,
    DateTime StartDate,
    DateTime EndDate,
    string Metric,
    decimal StartValue,
    decimal EndValue,
    decimal PercentChange,
    List<string> ContributingFactors,
    DateTime Timestamp
);
```

### PatternAnalysisCompleted
```csharp
public record PatternAnalysisCompleted(
    Guid JobId,
    Guid UserId,
    string AnalysisType,
    int PatternsDetected,
    int SessionsAnalyzed,
    DateTime CompletedAt,
    DateTime Timestamp
);
```

### InsightGenerated
```csharp
public record InsightGenerated(
    Guid InsightId,
    Guid UserId,
    Guid? PatternId,
    string InsightType,
    string Priority,
    string Title,
    bool IsActionable,
    DateTime Timestamp
);
```

---

## Business Rules

1. **Pattern Detection Thresholds**:
   - Minimum 20 sessions required for optimal length analysis
   - Minimum 15 sessions required for peak time detection
   - Minimum 10 distractions required for pattern identification
   - Minimum 14 days of data required for trend analysis

2. **Confidence Scoring**:
   - Confidence = `(sample_size / 100) * consistency_score * data_quality`
   - Minimum confidence of 60% required to surface patterns
   - Confidence decreases over time if pattern not reinforced

3. **Pattern Expiration**:
   - Optimal length patterns expire after 60 days
   - Peak time patterns expire after 45 days
   - Distraction patterns marked resolved if not seen in 14 days
   - Trends expire when direction changes

4. **Analysis Frequency**:
   - Automatic analysis runs weekly for active users
   - Manual analysis limited to once per 24 hours
   - Real-time pattern updates when confidence threshold crossed

5. **Insight Prioritization**:
   - High: Patterns with >80% confidence and >5% impact
   - Medium: Patterns with 60-80% confidence
   - Low: Exploratory patterns or low sample size

6. **Data Requirements**:
   - Peak time analysis requires sessions across different times
   - Distraction patterns require distraction logging enabled
   - Trends require continuous data (max 3-day gap)

7. **Privacy and Data**:
   - Pattern data deleted 90 days after user deactivation
   - Aggregated insights retained for research (anonymized)
   - Users can opt-out of pattern analysis

---

## Integration Points

- **Session Management Service**: Source of session data for analysis
- **Distraction Tracking Service**: Source of distraction data
- **Notification Service**: Deliver insights and recommendations
- **Recommendation Engine**: Apply patterns to suggest optimal scheduling
- **Machine Learning Service**: Advanced pattern detection algorithms
- **Reporting Service**: Include patterns in weekly/monthly reports

---

## Performance Considerations

- Pattern analysis runs asynchronously via background jobs
- Analysis results cached for 1 hour
- Database queries optimized with proper indexes
- Large datasets paginated (max 100 patterns per request)
- Historical data archived after 1 year

---

## Error Handling

- `400 Bad Request`: Invalid parameters, insufficient data
- `404 Not Found`: Pattern not found
- `429 Too Many Requests`: Analysis rate limit exceeded
- `503 Service Unavailable`: Analysis service temporarily down
