# Goal Tracking - Backend Requirements

## Domain Model
### IncomeGoal Aggregate
- GoalId, StreamId, UserId, TargetAmount, TimePeriod (Monthly/Quarterly/Annual), GoalDeadline, CurrentProgress, Status, CreatedAt

## Commands
- SetIncomeGoalCommand: Creates goal, raises **IncomeGoalSet** event
- UpdateGoalProgressCommand: Recalculates progress
- CheckGoalAchievementCommand: Monitors goals, raises **IncomeGoalAchieved** when met

## Queries
- GetActiveGoalsQuery, GetGoalProgressQuery, GetGoalHistoryQuery, GetAchievementsQuery

## API Endpoints
- POST /api/goals (create goal)
- GET /api/goals (list goals)
- GET /api/goals/{id}/progress (get progress)
- GET /api/goals/achievements (get achieved goals)

## Events
- **IncomeGoalSet**: Initializes progress tracking
- **IncomeGoalAchieved**: Celebrates success, sends notification

## Background Jobs
- Daily goal progress update
- Achievement checker runs on income received events
