# Backend Requirements - Activity Management

## Overview
The Activity Management feature enables pet owners to track exercise sessions, set activity goals, and log behavioral incidents for their pets. This feature utilizes domain events for asynchronous processing and event-driven architecture.

## Domain Events

### 1. ExerciseSessionLogged
**Purpose:** Triggered when a pet's physical activity is recorded

**Event Properties:**
- `EventId` (Guid): Unique identifier for the event
- `PetId` (Guid): Identifier of the pet
- `SessionId` (Guid): Unique identifier for the exercise session
- `ActivityType` (string): Type of exercise (Walk, Run, Play, Swim, etc.)
- `DurationMinutes` (int): Duration of the activity in minutes
- `DistanceMeters` (decimal?): Optional distance covered
- `IntensityLevel` (enum): Low, Medium, High
- `CaloriesBurned` (decimal?): Estimated calories burned
- `StartTime` (DateTime): When the activity started
- `EndTime` (DateTime): When the activity ended
- `Notes` (string?): Optional notes about the session
- `LocationName` (string?): Optional location identifier
- `OwnerId` (Guid): Owner who logged the session
- `LoggedAt` (DateTime): Timestamp when event was created

**Event Handlers:**
- Update pet's daily activity statistics
- Check if daily/weekly goals are met
- Calculate health metrics trends
- Send notifications if activity milestones achieved
- Update activity history and analytics

### 2. ExerciseGoalSet
**Purpose:** Triggered when an activity target is established for a pet

**Event Properties:**
- `EventId` (Guid): Unique identifier for the event
- `PetId` (Guid): Identifier of the pet
- `GoalId` (Guid): Unique identifier for the goal
- `GoalType` (enum): Daily, Weekly, Monthly
- `TargetDurationMinutes` (int?): Target exercise duration
- `TargetDistance` (decimal?): Target distance in meters
- `TargetCalories` (decimal?): Target calories to burn
- `TargetSessionsCount` (int?): Number of sessions per period
- `StartDate` (DateTime): When the goal becomes active
- `EndDate` (DateTime?): Optional goal end date
- `IsRecurring` (bool): Whether the goal repeats
- `SetBy` (Guid): User who set the goal
- `CreatedAt` (DateTime): Timestamp when event was created

**Event Handlers:**
- Create goal tracking record
- Initialize progress monitoring
- Schedule reminder notifications
- Update pet's activity profile
- Generate baseline recommendations

### 3. BehaviorIncidentLogged
**Purpose:** Triggered when notable pet behavior is documented

**Event Properties:**
- `EventId` (Guid): Unique identifier for the event
- `PetId` (Guid): Identifier of the pet
- `IncidentId` (Guid): Unique identifier for the incident
- `BehaviorType` (enum): Aggression, Anxiety, Destructive, Excessive, Other
- `Severity` (enum): Minor, Moderate, Severe
- `Description` (string): Detailed description of the behavior
- `Triggers` (List<string>): Identified triggers or causes
- `Context` (string?): Environmental or situational context
- `Duration` (int?): How long the behavior lasted (minutes)
- `OccurredAt` (DateTime): When the incident occurred
- `ActionsTaken` (string?): How the incident was handled
- `RequiresFollowUp` (bool): Whether veterinary/trainer consultation needed
- `LoggedBy` (Guid): User who logged the incident
- `LoggedAt` (DateTime): Timestamp when event was created

**Event Handlers:**
- Store incident in behavior history
- Analyze behavior patterns
- Check for recurring issues
- Trigger alerts for severe incidents
- Generate behavior reports
- Update pet's behavioral profile

## API Endpoints

### Exercise Session Management

#### POST /api/v1/exercise-sessions
**Description:** Log a new exercise session for a pet

**Request Body:**
```json
{
  "petId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "activityType": "Walk",
  "durationMinutes": 30,
  "distanceMeters": 2500.0,
  "intensityLevel": "Medium",
  "startTime": "2025-12-28T10:00:00Z",
  "endTime": "2025-12-28T10:30:00Z",
  "notes": "Morning walk in the park",
  "locationName": "Central Park"
}
```

**Response:** 201 Created
```json
{
  "sessionId": "4fa85f64-5717-4562-b3fc-2c963f66afa7",
  "success": true,
  "message": "Exercise session logged successfully"
}
```

**Publishes:** ExerciseSessionLogged event

#### GET /api/v1/exercise-sessions/{petId}
**Description:** Retrieve exercise sessions for a pet

**Query Parameters:**
- `startDate` (DateTime?): Filter sessions from this date
- `endDate` (DateTime?): Filter sessions until this date
- `activityType` (string?): Filter by activity type
- `page` (int): Page number (default: 1)
- `pageSize` (int): Items per page (default: 20)

**Response:** 200 OK
```json
{
  "sessions": [
    {
      "sessionId": "4fa85f64-5717-4562-b3fc-2c963f66afa7",
      "petId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "activityType": "Walk",
      "durationMinutes": 30,
      "distanceMeters": 2500.0,
      "intensityLevel": "Medium",
      "caloriesBurned": 180.5,
      "startTime": "2025-12-28T10:00:00Z",
      "endTime": "2025-12-28T10:30:00Z",
      "notes": "Morning walk in the park",
      "locationName": "Central Park"
    }
  ],
  "totalCount": 45,
  "page": 1,
  "pageSize": 20
}
```

#### GET /api/v1/exercise-sessions/{sessionId}/details
**Description:** Get detailed information about a specific session

**Response:** 200 OK

#### PUT /api/v1/exercise-sessions/{sessionId}
**Description:** Update an exercise session

**Request Body:** Same as POST

**Response:** 200 OK

#### DELETE /api/v1/exercise-sessions/{sessionId}
**Description:** Delete an exercise session

**Response:** 204 No Content

### Exercise Goal Management

#### POST /api/v1/exercise-goals
**Description:** Set a new exercise goal for a pet

**Request Body:**
```json
{
  "petId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "goalType": "Weekly",
  "targetDurationMinutes": 300,
  "targetDistance": 15000.0,
  "targetSessionsCount": 7,
  "startDate": "2025-12-28T00:00:00Z",
  "isRecurring": true
}
```

**Response:** 201 Created
```json
{
  "goalId": "5fa85f64-5717-4562-b3fc-2c963f66afa8",
  "success": true,
  "message": "Exercise goal set successfully"
}
```

**Publishes:** ExerciseGoalSet event

#### GET /api/v1/exercise-goals/{petId}
**Description:** Retrieve active and past goals for a pet

**Query Parameters:**
- `status` (string?): Active, Completed, Failed
- `goalType` (string?): Daily, Weekly, Monthly

**Response:** 200 OK
```json
{
  "goals": [
    {
      "goalId": "5fa85f64-5717-4562-b3fc-2c963f66afa8",
      "petId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "goalType": "Weekly",
      "targetDurationMinutes": 300,
      "currentDurationMinutes": 180,
      "progressPercentage": 60.0,
      "targetSessionsCount": 7,
      "currentSessionsCount": 4,
      "status": "Active",
      "startDate": "2025-12-28T00:00:00Z",
      "daysRemaining": 3
    }
  ]
}
```

#### GET /api/v1/exercise-goals/{goalId}/progress
**Description:** Get detailed progress for a specific goal

**Response:** 200 OK

#### PUT /api/v1/exercise-goals/{goalId}
**Description:** Update an exercise goal

**Response:** 200 OK

#### DELETE /api/v1/exercise-goals/{goalId}
**Description:** Delete an exercise goal

**Response:** 204 No Content

### Behavior Incident Management

#### POST /api/v1/behavior-incidents
**Description:** Log a behavior incident

**Request Body:**
```json
{
  "petId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "behaviorType": "Anxiety",
  "severity": "Moderate",
  "description": "Pet showed signs of separation anxiety when left alone",
  "triggers": ["Owner leaving", "Loud noises outside"],
  "context": "Home alone for 3 hours",
  "duration": 45,
  "occurredAt": "2025-12-28T14:00:00Z",
  "actionsTaken": "Provided comfort toy and calming music",
  "requiresFollowUp": true
}
```

**Response:** 201 Created
```json
{
  "incidentId": "6fa85f64-5717-4562-b3fc-2c963f66afa9",
  "success": true,
  "message": "Behavior incident logged successfully"
}
```

**Publishes:** BehaviorIncidentLogged event

#### GET /api/v1/behavior-incidents/{petId}
**Description:** Retrieve behavior incidents for a pet

**Query Parameters:**
- `startDate` (DateTime?): Filter from this date
- `endDate` (DateTime?): Filter until this date
- `behaviorType` (string?): Filter by behavior type
- `severity` (string?): Filter by severity
- `page` (int): Page number
- `pageSize` (int): Items per page

**Response:** 200 OK

#### GET /api/v1/behavior-incidents/{incidentId}/details
**Description:** Get detailed information about an incident

**Response:** 200 OK

#### PUT /api/v1/behavior-incidents/{incidentId}
**Description:** Update a behavior incident

**Response:** 200 OK

#### DELETE /api/v1/behavior-incidents/{incidentId}
**Description:** Delete a behavior incident

**Response:** 204 No Content

### Analytics and Reporting

#### GET /api/v1/activity-analytics/{petId}/summary
**Description:** Get activity summary statistics

**Query Parameters:**
- `period` (string): Day, Week, Month, Year
- `startDate` (DateTime?): Custom period start
- `endDate` (DateTime?): Custom period end

**Response:** 200 OK
```json
{
  "petId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "period": "Week",
  "totalSessions": 12,
  "totalDurationMinutes": 360,
  "totalDistanceMeters": 18500.0,
  "totalCaloriesBurned": 2150.5,
  "averageSessionDuration": 30,
  "mostCommonActivity": "Walk",
  "goalCompletionRate": 85.7,
  "comparisonToPreviousPeriod": {
    "sessionsChange": "+15%",
    "durationChange": "+10%"
  }
}
```

#### GET /api/v1/behavior-analytics/{petId}/patterns
**Description:** Analyze behavior patterns and trends

**Response:** 200 OK
```json
{
  "petId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "totalIncidents": 8,
  "mostCommonBehavior": "Anxiety",
  "mostCommonTriggers": ["Loud noises", "Owner leaving"],
  "severityDistribution": {
    "minor": 5,
    "moderate": 2,
    "severe": 1
  },
  "trendDirection": "Improving",
  "recommendations": [
    "Consider gradual desensitization training",
    "Consult with veterinary behaviorist"
  ]
}
```

## Data Models

### ExerciseSession (Aggregate Root)
```csharp
public class ExerciseSession
{
    public Guid SessionId { get; set; }
    public Guid PetId { get; set; }
    public string ActivityType { get; set; }
    public int DurationMinutes { get; set; }
    public decimal? DistanceMeters { get; set; }
    public IntensityLevel IntensityLevel { get; set; }
    public decimal? CaloriesBurned { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string? Notes { get; set; }
    public string? LocationName { get; set; }
    public Guid OwnerId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public enum IntensityLevel
{
    Low,
    Medium,
    High
}
```

### ExerciseGoal (Aggregate Root)
```csharp
public class ExerciseGoal
{
    public Guid GoalId { get; set; }
    public Guid PetId { get; set; }
    public GoalType GoalType { get; set; }
    public int? TargetDurationMinutes { get; set; }
    public decimal? TargetDistance { get; set; }
    public decimal? TargetCalories { get; set; }
    public int? TargetSessionsCount { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsRecurring { get; set; }
    public GoalStatus Status { get; set; }
    public Guid SetBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public enum GoalType
{
    Daily,
    Weekly,
    Monthly
}

public enum GoalStatus
{
    Active,
    Completed,
    Failed,
    Cancelled
}
```

### BehaviorIncident (Aggregate Root)
```csharp
public class BehaviorIncident
{
    public Guid IncidentId { get; set; }
    public Guid PetId { get; set; }
    public BehaviorType BehaviorType { get; set; }
    public Severity Severity { get; set; }
    public string Description { get; set; }
    public List<string> Triggers { get; set; }
    public string? Context { get; set; }
    public int? Duration { get; set; }
    public DateTime OccurredAt { get; set; }
    public string? ActionsTaken { get; set; }
    public bool RequiresFollowUp { get; set; }
    public Guid LoggedBy { get; set; }
    public DateTime LoggedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public enum BehaviorType
{
    Aggression,
    Anxiety,
    Destructive,
    Excessive,
    Other
}

public enum Severity
{
    Minor,
    Moderate,
    Severe
}
```

## Business Rules

### Exercise Session Validation
1. Duration must be between 1 and 1440 minutes (24 hours)
2. Start time must be before end time
3. End time cannot be in the future
4. Intensity level is required
5. Pet must exist and belong to the requesting user

### Exercise Goal Validation
1. At least one target metric must be specified
2. Start date cannot be in the past (for new goals)
3. End date must be after start date
4. Only one active goal per type per pet
5. Recurring goals cannot have an end date

### Behavior Incident Validation
1. Description is required (minimum 10 characters)
2. Occurred date cannot be in the future
3. Severity assessment is mandatory
4. At least one trigger should be identified

## Event Processing

### Event Store
- All domain events are persisted to an event store
- Events are immutable once created
- Event replay capability for debugging and analytics
- Event versioning for schema evolution

### Event Bus
- Use message queue (RabbitMQ/Azure Service Bus) for event distribution
- Guaranteed delivery with retry mechanism
- Dead letter queue for failed events
- Event ordering preserved per aggregate

### Event Handlers (Subscribers)
1. **ActivityStatisticsHandler**: Updates real-time statistics
2. **GoalProgressHandler**: Tracks progress toward goals
3. **NotificationHandler**: Sends user notifications
4. **AnalyticsHandler**: Updates analytics and trends
5. **RecommendationHandler**: Generates personalized recommendations
6. **BehaviorAnalysisHandler**: Analyzes behavior patterns

## Security Requirements

1. **Authentication**: JWT-based authentication required for all endpoints
2. **Authorization**: Users can only access data for their own pets
3. **Data Validation**: Server-side validation for all inputs
4. **Rate Limiting**: 100 requests per minute per user
5. **Audit Logging**: All create/update/delete operations logged

## Performance Requirements

1. API response time < 200ms for read operations
2. API response time < 500ms for write operations
3. Event publishing < 50ms
4. Support 1000 concurrent users
5. Database query optimization with proper indexing

## Database Indexes

```sql
-- Exercise Sessions
CREATE INDEX IX_ExerciseSession_PetId_StartTime ON ExerciseSessions(PetId, StartTime DESC);
CREATE INDEX IX_ExerciseSession_ActivityType ON ExerciseSessions(ActivityType);

-- Exercise Goals
CREATE INDEX IX_ExerciseGoal_PetId_Status ON ExerciseGoals(PetId, Status);
CREATE INDEX IX_ExerciseGoal_GoalType ON ExerciseGoals(GoalType);

-- Behavior Incidents
CREATE INDEX IX_BehaviorIncident_PetId_OccurredAt ON BehaviorIncidents(PetId, OccurredAt DESC);
CREATE INDEX IX_BehaviorIncident_BehaviorType_Severity ON BehaviorIncidents(BehaviorType, Severity);
```

## Testing Requirements

1. **Unit Tests**: 80% code coverage minimum
2. **Integration Tests**: All API endpoints
3. **Event Handler Tests**: All event subscribers
4. **Performance Tests**: Load testing for high-traffic scenarios
5. **Security Tests**: Penetration testing for vulnerabilities
