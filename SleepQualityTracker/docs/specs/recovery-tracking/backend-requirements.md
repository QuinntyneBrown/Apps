# Backend Requirements - Recovery Tracking

## API Endpoints

### POST /api/recovery/calculate
Calculate recovery score for session
- **Request Body**: sessionId, hrvData, restingHR
- **Events**: `RecoveryScoreCalculated`, `FullRecoveryAchieved`

### GET /api/recovery/{sessionId}
Get recovery score for session

### GET /api/recovery/trend/{userId}
Get recovery trend over time

## Domain Models
### RecoveryScore: Id, SessionId, UserId, Score (0-100), HRV, RestingHR, ReadinessLevel, CalculatedAt

## Business Logic
- Calculate recovery based on sleep quality (70%), HRV (20%), resting HR (10%)
- Full recovery: score > 85
- Readiness levels: Low (<40), Moderate (40-70), High (70-85), Optimal (85+)
- Integrate wearable device data

## Events
RecoveryScoreCalculated, FullRecoveryAchieved with recovery metrics

## Database Schema
RecoveryScores table indexed by sessionId and userId
