# Scorecard Tracking - Backend Requirements

## API Endpoints

### POST /api/rounds/{roundId}/holes/{holeNumber}/score
```json
Request: {
  "strokes": "int",
  "putts": "int",
  "fairwayHit": "bool",
  "gir": "bool",
  "penalties": "int"
}
Domain Events: HoleScoreRecorded, BirdieMade, EagleMade, etc.
```

### PUT /api/rounds/{roundId}/holes/{holeNumber}
Update existing hole score

### GET /api/rounds/{roundId}/scorecard
Returns complete scorecard with all hole scores

## Domain Model

```csharp
public class HoleScore : Entity
{
    public Guid Id { get; private set; }
    public Guid RoundId { get; private set; }
    public int HoleNumber { get; private set; }
    public int Strokes { get; private set; }
    public int Par { get; private set; }
    public int ScoreToPar { get; private set; }
    public int? Putts { get; private set; }
    public bool? FairwayHit { get; private set; }
    public bool? GIR { get; private set; }
    public int Penalties { get; private set; }

    public void RecordScore(int strokes, int par)
    {
        Strokes = strokes;
        Par = par;
        ScoreToPar = strokes - par;

        RaiseDomainEvent(new HoleScoreRecorded(...));

        if (ScoreToPar == -1)
            RaiseDomainEvent(new BirdieMade(...));
        else if (ScoreToPar == -2)
            RaiseDomainEvent(new EagleMade(...));
        else if (ScoreToPar >= 2)
            RaiseDomainEvent(new DoubleBogeyOrWorse(...));
    }
}
```

## Business Logic
- Birdie: score = par - 1
- Eagle: score = par - 2
- Par: score = par
- Bogey: score = par + 1
- Double bogey or worse: score >= par + 2
- Validate strokes > 0
- Putts <= total strokes
- GIR only applies to par 3+ holes

## Database Schema

### hole_scores
- id, round_id, hole_number, strokes, par
- score_to_par, putts, fairway_hit, gir, penalties
- created_at, updated_at
