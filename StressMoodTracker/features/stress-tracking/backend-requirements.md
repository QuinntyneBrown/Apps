# Backend Requirements - Stress Tracking

## Domain Events
- StressLevelRecorded
- HighStressAlertTriggered
- StressReductionAchieved

## API Endpoints
- POST /api/stress/entries - Record stress level
- GET /api/stress/entries - Get stress history
- GET /api/stress/alerts - Get high stress alerts

## Data Models
```typescript
StressEntry {
  id: UUID,
  userId: UUID,
  stressRating: number, // 1-10
  stressType: string,
  physicalSymptoms: string[],
  timestamp: DateTime
}
```

## Business Logic
- Trigger alert when stress ≥ 8
- Detect sustained high stress (≥ 7 for 3+ days)
- Calculate stress reduction (significant drop from high level)
- Recommend interventions based on stress type
