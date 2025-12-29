# Backend Requirements - Sleep Quality Assessment

## API Endpoints

### GET /api/sleep-quality/{sessionId}
Get quality score for session
- **Response**: 200 OK with quality score and factors

### POST /api/sleep-quality/calculate
Calculate quality score for session
- **Request Body**: sessionId
- **Response**: 200 OK with calculated score
- **Events**: Publishes `SleepQualityScored`, possibly `PoorSleepDetected` or `ExceptionalSleepRecorded`

### GET /api/sleep-quality/trends/{userId}
Get quality trends over time
- **Query Parameters**: period (week, month, year)
- **Response**: 200 OK with trend data
- **Events**: Publishes `SleepQualityTrendAnalyzed`

## Domain Models

### SleepQualityScore
```
- Id: Guid
- SessionId: Guid
- UserId: Guid
- OverallScore: int (0-100)
- DurationScore: int
- EfficiencyScore: int
- StageScore: int
- ConsistencyScore: int
- ContributingFactors: List<string>
- CalculatedAt: DateTime
```

### QualityTrend
```
- Id: Guid
- UserId: Guid
- Period: enum (Week, Month)
- StartDate: DateTime
- EndDate: DateTime
- AverageQuality: decimal
- TrendDirection: enum (Improving, Declining, Stable)
- ChangePercentage: decimal
```

## Business Logic

### Quality Score Calculation
Algorithm considers:
1. Duration score (30%): Proximity to target hours
2. Efficiency score (25%): Sleep time / time in bed
3. Sleep stage score (25%): Deep + REM percentage
4. Consistency score (10%): Variance from typical schedule
5. User rating (10%): Subjective quality rating

### Threshold Detection
- Poor sleep: score < 40
- Good sleep: score 60-85
- Exceptional sleep: score > 85

### Trend Analysis
- Calculate weekly/monthly averages
- Compare to previous period
- Identify improving (>5% increase) or declining (>5% decrease)
- Run analysis on Sundays for weekly, 1st of month for monthly

## Event Publishing
Quality-related events with scores, factors, trends, timestamps

## Database Schema
### SleepQualityScores Table, QualityTrends Table

## Performance Requirements
- Score calculation: < 200ms
- Trend analysis: < 2 seconds
