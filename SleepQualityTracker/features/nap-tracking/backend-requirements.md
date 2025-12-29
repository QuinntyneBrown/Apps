# Backend Requirements - Nap Tracking

## API Endpoints

### POST /api/naps
Log a nap
- **Request Body**: startTime, duration, quality, userId
- **Events**: `NapRecorded`, potentially `ExcessiveNappingDetected`

### GET /api/naps/{userId}
Get user's naps
- **Query Parameters**: startDate, endDate

### GET /api/naps/analysis/{userId}
Analyze nap patterns and impact

## Domain Models
### Nap: Id, UserId, StartTime, Duration (minutes), Quality (1-10), LoggedAt

## Business Logic
- Validate nap duration < 3 hours
- Excessive napping: 2+ hours daily or 3+ naps/day
- Correlate nap timing with nighttime sleep quality
- Optimal nap: 20-30 min before 3 PM
- Include naps in total daily sleep calculations

## Events
NapRecorded, ExcessiveNappingDetected with nap metrics

## Database Schema
Naps table indexed by userId and startTime
