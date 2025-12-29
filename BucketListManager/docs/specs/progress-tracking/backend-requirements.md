# Backend Requirements - Progress Tracking

## Domain Events
- ProgressUpdated
- MilestoneAchieved
- ObstacleEncountered
- ObstacleOvercome

## API Endpoints
- POST /api/bucket-list/items/{id}/progress - Update progress
- GET /api/bucket-list/items/{id}/milestones - Get milestones
- POST /api/bucket-list/items/{id}/obstacles - Log obstacle
- PUT /api/bucket-list/items/{id}/obstacles/{obstacleId} - Resolve obstacle

## Data Models
```typescript
Progress {
  id: UUID,
  itemId: UUID,
  percentage: number,
  milestoneReached: string,
  notes: string,
  timestamp: DateTime
}
Obstacle {
  id: UUID,
  itemId: UUID,
  description: string,
  severity: 'low' | 'medium' | 'high',
  potentialSolutions: string[],
  resolved: boolean,
  resolutionMethod: string,
  lessonsLearned: string
}
```

## Business Logic
- Calculate progress trends
- Predict completion dates based on velocity
- Alert when progress stalls (no update in 30 days)
- Track obstacle patterns across items
