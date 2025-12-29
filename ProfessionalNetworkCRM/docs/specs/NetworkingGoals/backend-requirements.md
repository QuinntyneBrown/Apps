# Backend Requirements - Networking Goals

## Domain Events
- NetworkingGoalSet
- NetworkingQuotaSet
- NetworkingGoalAchieved

## API Endpoints

### POST /api/goals
Set networking goal.

**Request Body**:
```json
{
  "goalType": "new_contacts|interactions|events|follow_ups",
  "targetMetric": "integer",
  "targetDate": "datetime",
  "motivation": "string"
}
```

**Response**: `201 Created`
**Events Published**: `NetworkingGoalSet`

### GET /api/goals/progress
Get goal progress.

**Response**: `200 OK`
```json
{
  "goals": [{
    "id": "guid",
    "goalType": "string",
    "target": "integer",
    "current": "integer",
    "progress": "number",
    "daysRemaining": "integer",
    "onTrack": "boolean"
  }]
}
```

### POST /api/goals/{id}/achieve
Mark goal as achieved.

**Response**: `200 OK`
**Events Published**: `NetworkingGoalAchieved`

## Data Models

### NetworkingGoal Entity
```csharp
public class NetworkingGoal : AggregateRoot
{
    public Guid Id { get; private set; }
    public GoalType Type { get; private set; }
    public int TargetMetric { get; private set; }
    public DateTime TargetDate { get; private set; }
    public int CurrentProgress { get; private set; }
    public bool Achieved { get; private set; }

    public NetworkingGoal UpdateProgress(int progress) { }
    public NetworkingGoal Achieve() { }
}
```
