# Reporting - Backend Requirements

## Overview
Backend services for generating and managing weekly reports and monthly productivity insights, providing users with comprehensive analysis of their focus sessions, patterns, and productivity trends.

---

## API Endpoints

### POST /api/reports/weekly/generate
**Description**: Generate a weekly report for the current or specified week

**Request Body**:
```json
{
  "weekStartDate": "ISO8601",
  "weekEndDate": "ISO8601"
}
```

**Response**: `201 Created`
```json
{
  "reportId": "uuid",
  "userId": "uuid",
  "weekStartDate": "ISO8601",
  "weekEndDate": "ISO8601",
  "totalSessions": 24,
  "completedSessions": 20,
  "abandonedSessions": 4,
  "totalFocusMinutes": 600,
  "averageFocusScore": 82.5,
  "dailyBreakdown": [
    {
      "date": "ISO8601",
      "sessions": 4,
      "focusMinutes": 100,
      "averageScore": 85
    }
  ],
  "generatedAt": "ISO8601"
}
```

**Domain Event**: `WeeklyReportGenerated`

---

### POST /api/reports/monthly/generate
**Description**: Generate a monthly insight report for the current or specified month

**Request Body**:
```json
{
  "month": 12,
  "year": 2024
}
```

**Response**: `201 Created`
```json
{
  "insightId": "uuid",
  "userId": "uuid",
  "month": 12,
  "year": 2024,
  "totalSessions": 96,
  "completedSessions": 82,
  "totalFocusHours": 40.5,
  "averageFocusScore": 84.2,
  "mostProductiveDay": "Monday",
  "mostProductiveHour": 10,
  "topDistractions": [
    {
      "type": "Phone",
      "count": 45
    }
  ],
  "weeklyTrend": "improving",
  "insights": [
    "Your focus score improved by 12% compared to last month",
    "Monday mornings are your most productive time"
  ],
  "generatedAt": "ISO8601"
}
```

**Domain Event**: `MonthlyInsightCreated`

---

### GET /api/reports/weekly
**Description**: Get weekly reports for the user

**Query Parameters**:
- `startDate`: ISO8601 date
- `endDate`: ISO8601 date
- `page`: number
- `limit`: number

**Response**: `200 OK` with paginated weekly reports

---

### GET /api/reports/weekly/{reportId}
**Description**: Get specific weekly report details

**Response**: `200 OK` with detailed weekly report

---

### GET /api/reports/monthly
**Description**: Get monthly insight reports for the user

**Query Parameters**:
- `year`: number
- `page`: number
- `limit`: number

**Response**: `200 OK` with paginated monthly insights

---

### GET /api/reports/monthly/{insightId}
**Description**: Get specific monthly insight details

**Response**: `200 OK` with detailed monthly insight

---

### GET /api/reports/export
**Description**: Export reports in various formats

**Query Parameters**:
- `reportType`: weekly | monthly
- `format`: csv | pdf | json
- `startDate`: ISO8601 date
- `endDate`: ISO8601 date

**Response**: `200 OK` with file download

---

### GET /api/reports/dashboard
**Description**: Get aggregated dashboard data for reporting overview

**Response**: `200 OK`
```json
{
  "currentWeek": {
    "totalSessions": 15,
    "focusMinutes": 375,
    "averageScore": 83
  },
  "currentMonth": {
    "totalSessions": 68,
    "focusHours": 28.5,
    "completionRate": 89.7
  },
  "recentReports": [],
  "trends": {
    "weekOverWeek": 5.2,
    "monthOverMonth": 12.8
  }
}
```

---

## Database Schema

### WeeklyReports Table
```sql
CREATE TABLE WeeklyReports (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    WeekStartDate DATE NOT NULL,
    WeekEndDate DATE NOT NULL,
    TotalSessions INT NOT NULL,
    CompletedSessions INT NOT NULL,
    AbandonedSessions INT NOT NULL,
    TotalFocusMinutes INT NOT NULL,
    AverageFocusScore DECIMAL(5,2),
    AverageSessionDuration INT,
    TotalDistractionsLogged INT,
    MostProductiveDay VARCHAR(20),
    GeneratedAt DATETIME2 DEFAULT GETUTCDATE(),
    CreatedAt DATETIME2 DEFAULT GETUTCDATE(),
    UNIQUE(UserId, WeekStartDate)
);
```

### WeeklyReportDailyBreakdown Table
```sql
CREATE TABLE WeeklyReportDailyBreakdown (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    WeeklyReportId UNIQUEIDENTIFIER NOT NULL,
    Date DATE NOT NULL,
    SessionCount INT NOT NULL,
    FocusMinutes INT NOT NULL,
    AverageScore DECIMAL(5,2),
    CompletedSessions INT NOT NULL,
    FOREIGN KEY (WeeklyReportId) REFERENCES WeeklyReports(Id)
);
```

### MonthlyInsights Table
```sql
CREATE TABLE MonthlyInsights (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    Month INT NOT NULL,
    Year INT NOT NULL,
    TotalSessions INT NOT NULL,
    CompletedSessions INT NOT NULL,
    TotalFocusHours DECIMAL(8,2) NOT NULL,
    AverageFocusScore DECIMAL(5,2),
    MostProductiveDay VARCHAR(20),
    MostProductiveHour INT,
    WeeklyTrend VARCHAR(20),
    CompletionRate DECIMAL(5,2),
    GeneratedAt DATETIME2 DEFAULT GETUTCDATE(),
    CreatedAt DATETIME2 DEFAULT GETUTCDATE(),
    UNIQUE(UserId, Month, Year)
);
```

### MonthlyInsightDetails Table
```sql
CREATE TABLE MonthlyInsightDetails (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    MonthlyInsightId UNIQUEIDENTIFIER NOT NULL,
    InsightType VARCHAR(50) NOT NULL,
    InsightText NVARCHAR(MAX) NOT NULL,
    Category VARCHAR(50),
    Priority INT,
    FOREIGN KEY (MonthlyInsightId) REFERENCES MonthlyInsights(Id)
);
```

### TopDistractions Table
```sql
CREATE TABLE TopDistractions (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    MonthlyInsightId UNIQUEIDENTIFIER NOT NULL,
    DistractionType VARCHAR(100) NOT NULL,
    Count INT NOT NULL,
    Rank INT NOT NULL,
    FOREIGN KEY (MonthlyInsightId) REFERENCES MonthlyInsights(Id)
);
```

---

## Domain Events

### WeeklyReportGenerated
```csharp
public record WeeklyReportGenerated(
    Guid ReportId,
    Guid UserId,
    DateTime WeekStartDate,
    DateTime WeekEndDate,
    int TotalSessions,
    int CompletedSessions,
    int TotalFocusMinutes,
    decimal AverageFocusScore,
    DateTime Timestamp
);
```

### MonthlyInsightCreated
```csharp
public record MonthlyInsightCreated(
    Guid InsightId,
    Guid UserId,
    int Month,
    int Year,
    int TotalSessions,
    decimal TotalFocusHours,
    decimal AverageFocusScore,
    string WeeklyTrend,
    List<string> KeyInsights,
    DateTime Timestamp
);
```

### ReportExported
```csharp
public record ReportExported(
    Guid UserId,
    string ReportType,
    string Format,
    DateTime StartDate,
    DateTime EndDate,
    DateTime Timestamp
);
```

---

## Business Rules

1. **Report Generation**: Weekly reports can only be generated for completed weeks
2. **Monthly Insights**: Generated on the 1st of each month or on-demand for past months
3. **Data Aggregation**: Reports aggregate data from Sessions, Distractions, and Productivity tables
4. **Minimum Data**: Require at least 1 session to generate a report
5. **Trend Calculation**:
   - "improving" if score increased by >5%
   - "stable" if change is between -5% and +5%
   - "declining" if score decreased by >5%
6. **Report Retention**: Weekly reports kept for 1 year, monthly insights kept indefinitely
7. **Export Limits**: Maximum 6 months of data per export request
8. **Insight Generation**:
   - Auto-generate insights based on patterns
   - Highlight productivity peaks and valleys
   - Identify distraction trends
   - Compare to previous periods

---

## Integration Points

- **Session Service**: Pull session data for aggregation
- **Distraction Service**: Retrieve distraction logs
- **Pattern Analysis Service**: Get productivity patterns
- **Analytics Service**: Calculate trends and metrics
- **Notification Service**: Alert users when reports are ready
- **Email Service**: Send report summaries via email
- **Export Service**: Generate PDF/CSV files

---

## Aggregation Queries

### Weekly Report Aggregation
```sql
SELECT
    COUNT(*) as TotalSessions,
    SUM(CASE WHEN Status = 'Completed' THEN 1 ELSE 0 END) as CompletedSessions,
    SUM(ActualDuration) as TotalFocusMinutes,
    AVG(FocusScore) as AverageFocusScore,
    AVG(ActualDuration) as AverageSessionDuration
FROM Sessions
WHERE UserId = @UserId
    AND StartTime BETWEEN @WeekStart AND @WeekEnd
    AND Status IN ('Completed', 'Abandoned');
```

### Most Productive Day
```sql
SELECT TOP 1
    DATENAME(WEEKDAY, StartTime) as DayName,
    AVG(FocusScore) as AvgScore
FROM Sessions
WHERE UserId = @UserId
    AND StartTime BETWEEN @WeekStart AND @WeekEnd
    AND Status = 'Completed'
GROUP BY DATENAME(WEEKDAY, StartTime), DATEPART(WEEKDAY, StartTime)
ORDER BY AvgScore DESC;
```

### Monthly Trends
```sql
WITH WeeklyScores AS (
    SELECT
        DATEPART(WEEK, StartTime) as WeekNum,
        AVG(FocusScore) as AvgScore
    FROM Sessions
    WHERE UserId = @UserId
        AND MONTH(StartTime) = @Month
        AND YEAR(StartTime) = @Year
        AND Status = 'Completed'
    GROUP BY DATEPART(WEEK, StartTime)
)
SELECT
    CASE
        WHEN (MAX(AvgScore) - MIN(AvgScore)) > 5 THEN 'improving'
        WHEN (MIN(AvgScore) - MAX(AvgScore)) > 5 THEN 'declining'
        ELSE 'stable'
    END as Trend
FROM WeeklyScores;
```

---

## Caching Strategy

- **Weekly Reports**: Cache for 24 hours after generation
- **Monthly Insights**: Cache indefinitely (regenerate only on request)
- **Dashboard Data**: Cache for 1 hour
- **Export Files**: Generate on-demand, cache for 15 minutes

---

## Performance Considerations

1. **Async Report Generation**: Use background jobs for report creation
2. **Indexed Queries**: Index on UserId, StartTime, Status for fast aggregation
3. **Materialized Views**: Consider for frequently accessed aggregations
4. **Batch Processing**: Process multiple reports in batches
5. **Rate Limiting**: Limit report generation to prevent abuse
