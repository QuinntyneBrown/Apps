# Backend Requirements - Time Analysis

## Domain Events
- DailyAnalysisGenerated
- WeeklyPatternIdentified
- TimeWasterIdentified
- ProductivityPeakDetected

## API Endpoints
- POST /api/analytics/daily - Generate daily analysis
- GET /api/analytics/patterns/weekly - Get weekly patterns
- GET /api/analytics/time-wasters - Identify time wasters
- GET /api/analytics/productivity-peaks - Get productivity peaks
- GET /api/analytics/summary - Get period summary

## Data Models
```typescript
DailyAnalysis {
  id, date, totalTrackedTime, categoryBreakdown, productiveRatio, unproductiveRatio
}
WeeklyPattern {
  id, patternType, daysInvolved, activitiesInvolved, consistencyScore
}
TimeWaster {
  id, activityDescription, timeConsumed, impactAssessment
}
ProductivityPeak {
  id, timeWindow, daysOfWeek, productivityMetrics, contributingFactors
}
```

## Business Logic
- Analyze time entries to calculate productivity ratios
- Detect patterns using statistical analysis (minimum 3 occurrences)
- Identify time wasters when low-value activity exceeds 10% of daily time
- Detect productivity peaks by analyzing task completion and energy levels
- Generate insights and recommendations based on analysis
