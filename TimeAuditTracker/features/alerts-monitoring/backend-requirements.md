# Backend Requirements - Alerts & Monitoring

## Domain Events
- UnloggedTimeDetected
- BalanceAlertTriggered
- ConsistencyStreakAchieved

## API Endpoints
- GET /api/alerts/unlogged-time - Get time gaps
- GET /api/alerts/balance - Get balance warnings
- GET /api/alerts/streaks - Get consistency streaks
- POST /api/alerts/preferences - Configure alert settings

## Data Models
```typescript
UnloggedTimeGap {
  id, startTime, endTime, estimatedDuration
}
BalanceAlert {
  id, imbalanceType, severity, affectedCategories, recommendation
}
ConsistencyStreak {
  id, streakLength, completenessPercentage
}
```

## Business Logic
- Detect gaps in time logging (> 2 hours)
- Calculate work-life balance metrics
- Trigger alerts when imbalance detected (work > 60% of waking hours)
- Track logging consistency streaks
- Calculate data completeness percentage
