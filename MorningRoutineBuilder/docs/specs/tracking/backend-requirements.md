# Tracking - Backend Requirements

## API Endpoints
- POST /api/tracking/start - Start routine session
- POST /api/tracking/complete-activity - Mark activity done
- POST /api/tracking/complete-routine - Finish routine
- GET /api/tracking/history - Get completion history
- GET /api/tracking/stats - Get statistics

## Domain Events
- RoutineSessionStarted
- ActivityCompleted
- RoutineSessionCompleted
- StreakAchieved
- MilestoneReached

## Business Logic
- Calculate completion rates
- Track streak days
- Analyze time patterns
- Generate optimization suggestions
- Identify consistency trends
