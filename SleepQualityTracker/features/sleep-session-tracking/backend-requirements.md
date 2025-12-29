# Backend Requirements - Sleep Session Tracking

## API Endpoints

### POST /api/sleep-sessions
Create a new sleep session
- **Request Body**:
  - sleepStartTime (DateTime, required)
  - wakeTime (DateTime, required)
  - qualityRating (int, 1-10, required)
  - userId (string, required)
- **Response**: 201 Created with session object
- **Events**: Publishes `SleepSessionRecorded`

### GET /api/sleep-sessions
Retrieve sleep sessions for a user
- **Query Parameters**:
  - userId (string, required)
  - startDate (DateTime, optional)
  - endDate (DateTime, optional)
  - page (int, optional)
  - limit (int, optional)
- **Response**: 200 OK with paginated sessions list

### GET /api/sleep-sessions/{sessionId}
Retrieve specific sleep session
- **Response**: 200 OK with session details

### PUT /api/sleep-sessions/{sessionId}
Update sleep session
- **Request Body**: Partial session data
- **Response**: 200 OK with updated session

### POST /api/sleep-sessions/{sessionId}/stages
Add sleep stage data to session
- **Request Body**:
  - lightSleepDuration (int, minutes)
  - deepSleepDuration (int, minutes)
  - remDuration (int, minutes)
  - awakeTime (int, minutes)
- **Response**: 200 OK
- **Events**: Publishes `SleepStagesLogged`

### POST /api/sleep-sessions/{sessionId}/interruptions
Log sleep interruption
- **Request Body**:
  - interruptionTime (DateTime, required)
  - duration (int, minutes, required)
  - reason (string, optional)
- **Response**: 201 Created
- **Events**: Publishes `SleepInterruptionRecorded`

### POST /api/sleep-sessions/sync
Sync sleep data from wearable device
- **Request Body**:
  - deviceType (string, required)
  - deviceData (object, required)
  - userId (string, required)
- **Response**: 200 OK with synced sessions
- **Events**: Publishes `SleepSessionRecorded` for each session

## Domain Models

### SleepSession
```
- Id: Guid
- UserId: Guid
- SleepStartTime: DateTime
- WakeTime: DateTime
- TotalDuration: TimeSpan (calculated)
- QualityRating: int (1-10)
- CreatedAt: DateTime
- UpdatedAt: DateTime
```

### SleepStages
```
- Id: Guid
- SessionId: Guid
- LightSleepDuration: int (minutes)
- DeepSleepDuration: int (minutes)
- RemDuration: int (minutes)
- AwakeTime: int (minutes)
```

### SleepInterruption
```
- Id: Guid
- SessionId: Guid
- InterruptionTime: DateTime
- Duration: int (minutes)
- Reason: string
```

## Business Logic

### Sleep Duration Calculation
- Calculate total duration as WakeTime - SleepStartTime
- Subtract total interruption time for actual sleep time
- Validate duration is between 1 minute and 24 hours

### Early Wake Detection
- Compare actual wake time with user's target wake time
- If difference > 30 minutes early, publish `EarlyWakeDetected`
- Include time difference in event payload

### Device Integration
- Support OAuth 2.0 flow for device authorization
- Implement device-specific data mapping to SleepSession model
- Handle rate limiting with exponential backoff
- Queue failed sync attempts for retry

## Event Publishing

### SleepSessionRecorded
```json
{
  "eventId": "guid",
  "eventType": "SleepSessionRecorded",
  "timestamp": "datetime",
  "data": {
    "sessionId": "guid",
    "userId": "guid",
    "sleepStartTime": "datetime",
    "wakeTime": "datetime",
    "totalDuration": "timespan",
    "qualityRating": "int"
  }
}
```

### SleepStagesLogged
```json
{
  "eventId": "guid",
  "eventType": "SleepStagesLogged",
  "timestamp": "datetime",
  "data": {
    "sessionId": "guid",
    "userId": "guid",
    "lightSleepDuration": "int",
    "deepSleepDuration": "int",
    "remDuration": "int",
    "awakeTime": "int"
  }
}
```

### SleepInterruptionRecorded
```json
{
  "eventId": "guid",
  "eventType": "SleepInterruptionRecorded",
  "timestamp": "datetime",
  "data": {
    "interruptionId": "guid",
    "sessionId": "guid",
    "userId": "guid",
    "interruptionTime": "datetime",
    "duration": "int",
    "reason": "string"
  }
}
```

### EarlyWakeDetected
```json
{
  "eventId": "guid",
  "eventType": "EarlyWakeDetected",
  "timestamp": "datetime",
  "data": {
    "sessionId": "guid",
    "userId": "guid",
    "targetWakeTime": "datetime",
    "actualWakeTime": "datetime",
    "timeDifference": "timespan"
  }
}
```

## Database Schema

### SleepSessions Table
- Id (uniqueidentifier, PK)
- UserId (uniqueidentifier, FK, indexed)
- SleepStartTime (datetime2, indexed)
- WakeTime (datetime2)
- TotalDuration (int, minutes)
- QualityRating (int)
- CreatedAt (datetime2)
- UpdatedAt (datetime2)

### SleepStages Table
- Id (uniqueidentifier, PK)
- SessionId (uniqueidentifier, FK)
- LightSleepDuration (int)
- DeepSleepDuration (int)
- RemDuration (int)
- AwakeTime (int)

### SleepInterruptions Table
- Id (uniqueidentifier, PK)
- SessionId (uniqueidentifier, FK, indexed)
- InterruptionTime (datetime2)
- Duration (int)
- Reason (nvarchar(500))

## Validation Rules

- Sleep start time must be before wake time
- Quality rating must be 1-10
- Total duration must be > 0 and < 24 hours
- Sleep stage durations must sum to approximately total duration
- Interruption time must be between sleep start and wake time
- All datetime values must be valid and not in future (except for scheduled sessions)

## Performance Requirements

- Session creation: < 500ms
- Session retrieval: < 200ms
- Bulk sync: < 5 seconds for 30 days of data
- Database queries optimized with proper indexing on UserId and SleepStartTime
