# Run Logging - Backend

## API Endpoints

### POST /api/runs
```json
{
  "distance": "decimal (miles)",
  "duration": "int (minutes)",
  "pace": "string (min/mile)",
  "route": "GeoJSON",
  "elevation": "int (feet)",
  "weather": "string",
  "effort": "int (1-10)"
}
Domain Events: RunCompleted, PersonalRecordSet
```

### GET /api/runs
Query: userId, startDate, endDate, workoutType
Returns: Paginated run history

## Domain Model

```csharp
public class Run : AggregateRoot
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public decimal Distance { get; private set; }
    public int DurationMinutes { get; private set; }
    public string Pace { get; private set; }
    public Route RouteData { get; private set; }
    public WeatherConditions Weather { get; private set; }
    public int PerceivedEffort { get; private set; }
    public WorkoutType Type { get; private set; }

    public void Complete()
    {
        CalculatePace();
        RaiseDomainEvent(new RunCompleted(...));
        CheckPersonalRecords();
    }
}
```

## Database Schema
### runs
- id, user_id, distance, duration_minutes, pace
- route_data (json), elevation, weather, effort
- workout_type, created_at
