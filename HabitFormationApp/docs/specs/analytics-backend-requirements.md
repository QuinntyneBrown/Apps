# Analytics - Backend Requirements

## Overview
The Analytics feature provides insights into habit patterns, success factors, and trends to help users optimize their habit formation journey.

## Domain Events
- **WeeklyProgressReportGenerated**: Triggered when weekly summary is created
- **HabitPatternIdentified**: When system discovers a behavioral pattern
- **SuccessFactorAnalyzed**: When correlation between factors and success is found

## Aggregates

### WeeklyReport
**Properties:**
- `Id` (Guid)
- `UserId` (Guid)
- `WeekStartDate` (DateTime)
- `TotalHabits` (int)
- `CompletionRate` (double)
- `TotalCompletions` (int)
- `ActiveStreaks` (int)
- `NewPersonalBests` (int)
- `MostProductiveDay` (DayOfWeek)
- `BestCategory` (string)
- `RecommendedActions` (List<string>)
- `GeneratedAt` (DateTime)

### HabitPattern
**Properties:**
- `Id` (Guid)
- `UserId` (Guid)
- `HabitId` (Guid)
- `PatternType` (PatternType)
- `Description` (string)
- `Confidence` (double): 0-1 confidence score
- `Data` (Dictionary<string, object>)
- `DiscoveredAt` (DateTime)

### SuccessFactor
**Properties:**
- `Id` (Guid)
- `UserId` (Guid)
- `HabitId` (Guid?)
- `Factor` (string): TimeOfDay, DayOfWeek, Weather, Location, etc.
- `CorrelationScore` (double)
- `SampleSize` (int)
- `Recommendation` (string)

## Commands

### GenerateWeeklyReportCommand
```csharp
public record GenerateWeeklyReportCommand(
    Guid UserId,
    DateTime WeekStartDate
);
```

### IdentifyPatternsCommand
```csharp
public record IdentifyPatternsCommand(
    Guid UserId,
    DateTime StartDate,
    DateTime EndDate
);
```

### AnalyzeSuccessFactorsCommand
```csharp
public record AnalyzeSuccessFactorsCommand(
    Guid UserId,
    Guid? HabitId
);
```

## Queries

### GetWeeklyReportQuery
Returns weekly progress report

### GetTrendsQuery
Returns trends over time (completion rates, streaks, etc.)

### GetHabitPatternsQuery
Returns identified patterns for user

### GetSuccessFactorsQuery
Returns success factor analysis

### GetComparisonQuery
Compare current period with previous periods

### GetInsightsQuery
Get AI-generated insights and recommendations

## API Endpoints

### GET /api/analytics/weekly/{weekStartDate}
Get weekly report

### GET /api/analytics/trends
Get trends with date range
- Query: startDate, endDate, metric

### GET /api/analytics/patterns
Get identified patterns

### GET /api/analytics/success-factors
Get success factor analysis

### GET /api/analytics/insights
Get personalized insights

### POST /api/analytics/reports/generate
Trigger report generation

## Pattern Types

### Time-based Patterns
- Preferred completion times
- Day-of-week patterns
- Seasonal variations
- Time-since-last-completion patterns

### Behavior Patterns
- Completion streaks
- Habit groupings (often completed together)
- Failure patterns (what breaks streaks)
- Recovery patterns (how user bounces back)

### Context Patterns
- Location correlations
- Weather impact
- Day-type patterns (workday vs weekend)
- Social influence (accountability effect)

## Success Factor Analysis

### Factors Analyzed
1. **Time of Day**
   - Morning completions vs evening
   - Optimal time for each habit
   - Consistency in timing

2. **Day of Week**
   - Best days for habit types
   - Weekend vs weekday performance
   - Specific day vulnerabilities

3. **Frequency**
   - Daily vs weekly performance
   - Optimal frequency for habit types
   - Over-scheduling detection

4. **Social Factors**
   - Accountability partner impact
   - Check-in frequency correlation
   - Encouragement effectiveness

5. **Environmental**
   - Location patterns
   - Weather correlations
   - Context triggers

## Insights Generation

### AI/ML Models
```
- Completion probability prediction
- Optimal scheduling recommendations
- Risk of streak break prediction
- Habit pairing suggestions
- Personalized coaching messages
```

### Recommendation Types
- "You're 40% more likely to complete morning habits on weekdays"
- "Consider pairing meditation with your morning run"
- "Your streaks often break on Fridays - schedule easier habits"
- "Accountability partners improve your completion rate by 25%"

## Database Schema

```sql
CREATE TABLE WeeklyReports (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    WeekStartDate DATETIME2 NOT NULL,
    TotalHabits INT NOT NULL,
    CompletionRate DECIMAL(5,2) NOT NULL,
    TotalCompletions INT NOT NULL,
    ActiveStreaks INT NOT NULL,
    NewPersonalBests INT NOT NULL,
    MostProductiveDay NVARCHAR(20) NOT NULL,
    BestCategory NVARCHAR(50) NOT NULL,
    RecommendedActions NVARCHAR(MAX), -- JSON
    GeneratedAt DATETIME2 NOT NULL,
    CONSTRAINT FK_WeeklyReports_Users FOREIGN KEY (UserId) REFERENCES Users(Id)
);

CREATE TABLE HabitPatterns (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    HabitId UNIQUEIDENTIFIER NULL,
    PatternType NVARCHAR(50) NOT NULL,
    Description NVARCHAR(500) NOT NULL,
    Confidence DECIMAL(3,2) NOT NULL,
    Data NVARCHAR(MAX), -- JSON
    DiscoveredAt DATETIME2 NOT NULL,
    CONSTRAINT FK_HabitPatterns_Users FOREIGN KEY (UserId) REFERENCES Users(Id)
);

CREATE TABLE SuccessFactors (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    HabitId UNIQUEIDENTIFIER NULL,
    Factor NVARCHAR(50) NOT NULL,
    CorrelationScore DECIMAL(3,2) NOT NULL,
    SampleSize INT NOT NULL,
    Recommendation NVARCHAR(500) NOT NULL,
    AnalyzedAt DATETIME2 NOT NULL,
    CONSTRAINT FK_SuccessFactors_Users FOREIGN KEY (UserId) REFERENCES Users(Id)
);

CREATE INDEX IX_WeeklyReports_UserId_WeekStart ON WeeklyReports(UserId, WeekStartDate);
CREATE INDEX IX_HabitPatterns_UserId ON HabitPatterns(UserId);
CREATE INDEX IX_SuccessFactors_UserId ON SuccessFactors(UserId);
```

## Metrics Tracked

### Completion Metrics
- Total completions
- Completion rate (%)
- On-time completion rate
- Late completion rate
- Perfect days/weeks

### Streak Metrics
- Current streaks (all habits)
- Longest streaks
- Streak break frequency
- Recovery time after breaks

### Time Metrics
- Average completion time
- Response time to reminders
- Time consistency
- Best completion windows

### Engagement Metrics
- Active habits count
- Habit creation rate
- Habit archival rate
- App usage frequency

## Integration Points
- Completion service: Get completion data
- Streak service: Get streak statistics
- Habit service: Get habit configurations
- Reminder service: Get reminder effectiveness
- ML service: Pattern recognition and predictions

## Background Jobs
- Weekly: Generate weekly reports (Sunday night)
- Daily: Update trend data
- Monthly: Deep pattern analysis
- Quarterly: Long-term success factor analysis

## Export Features
- PDF report generation
- CSV data export
- Share insights on social media
- Email weekly summaries

## Privacy & Data
- User controls what data is analyzed
- Option to opt-out of ML analysis
- Data anonymization for research
- Clear data retention policies

## Testing Requirements
- Pattern detection algorithm tests
- Success factor correlation tests
- Report generation tests
- Trend calculation accuracy tests
- Performance tests for large datasets
