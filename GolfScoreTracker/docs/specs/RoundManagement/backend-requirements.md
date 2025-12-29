# Round Management - Backend Requirements

## API Endpoints

### POST /api/rounds/start
```json
Request: {
  "courseId": "uuid",
  "teeBox": "enum",
  "startTime": "datetime",
  "players": ["uuid"],
  "weatherConditions": "string"
}
Response: { "roundId": "uuid", "status": "InProgress" }
Domain Events: RoundStarted
```

### POST /api/rounds/{id}/complete
```json
Request: { "totalScore": "int", "completionTime": "datetime" }
Domain Events: RoundCompleted, PersonalBestRound (if applicable)
```

### POST /api/rounds/{id}/abandon
```json
Domain Events: RoundAbandoned
```

### GET /api/rounds
Query params: userId, status, courseId, dateRange
Returns: Paginated list of rounds

## Domain Model

```csharp
public class Round : AggregateRoot
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public Guid CourseId { get; private set; }
    public DateTime StartTime { get; private set; }
    public DateTime? CompletionTime { get; private set; }
    public TeeBox TeeBox { get; private set; }
    public int TotalScore { get; private set; }
    public int ScoreToPar { get; private set; }
    public RoundStatus Status { get; private set; }
    public List<Guid> PlayingPartners { get; private set; }
    public WeatherConditions Weather { get; private set; }
    public List<HoleScore> HoleScores { get; private set; }

    public void Complete(int totalScore)
    {
        Status = RoundStatus.Completed;
        TotalScore = totalScore;
        CompletionTime = DateTime.UtcNow;
        CalculateScoreToPar();

        RaiseDomainEvent(new RoundCompleted(...));

        if (IsPersonalBest())
            RaiseDomainEvent(new PersonalBestRound(...));
    }
}
```

## Business Logic
- Round must be started before holes can be scored
- Total score = sum of all hole scores
- Score to par = total score - course par
- Personal best detected automatically
- Weather captured at start time

## Database Schema

### rounds
- id, user_id, course_id, start_time, completion_time
- tee_box, total_score, score_to_par, status
- weather_conditions, playing_partners (json)

### hole_scores
- id, round_id, hole_number, strokes, par, putts
- fairway_hit, gir, penalties
