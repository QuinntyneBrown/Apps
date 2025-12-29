# Backend Requirements - Time Goals

## Domain Events
- TimeGoalSet
- GoalProgressUpdated
- MilestoneAchieved
- TimeGoalAbandoned

## API Endpoints
- POST /api/goals - Create time management goal
- GET /api/goals - List active goals
- PUT /api/goals/{id}/progress - Update progress
- POST /api/goals/{id}/abandon - Abandon goal
- GET /api/goals/{id}/milestones - Get milestones

## Data Models
```typescript
TimeGoal {
  id, description, targetMetrics, baseline, deadline, status
}
GoalProgress {
  id, goalId, currentStatus, progressPercentage, onTrackIndicator
}
Milestone {
  id, goalId, achievementDate, celebrationType
}
```

## Business Logic
- Track progress automatically from time entries
- Calculate completion percentage based on target metrics
- Identify milestone achievements at 25%, 50%, 75%, 100%
- Store abandonment reasons for learning
- Maintain baseline metrics for comparison
