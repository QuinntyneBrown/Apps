# Handicap Management - Backend Requirements

## API Endpoints

### GET /api/users/{userId}/handicap
Returns current handicap index and history

### POST /api/handicaps/calculate
Triggered after round completion
Calculates new handicap based on best 8 of last 20 rounds

## Domain Model

```csharp
public class HandicapIndex : AggregateRoot
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public decimal CurrentIndex { get; private set; }
    public decimal PreviousIndex { get; private set; }
    public DateTime CalculationDate { get; private set; }
    public List<Guid> RoundsUsed { get; private set; }
    public TrendDirection Trend { get; private set; }

    public void Calculate(List<Round> eligibleRounds)
    {
        // USGA formula: Average of best 8 of last 20 rounds
        var best8 = eligibleRounds
            .OrderBy(r => r.ScoreDifferential)
            .Take(8)
            .Average(r => r.ScoreDifferential);

        PreviousIndex = CurrentIndex;
        CurrentIndex = Math.Round(best8 * 0.96m, 1);

        RaiseDomainEvent(new HandicapCalculated(...));

        if (CurrentIndex < PreviousIndex)
            RaiseDomainEvent(new HandicapImproved(...));

        if (CurrentIndex < 10.0m && PreviousIndex >= 10.0m)
            RaiseDomainEvent(new SingleDigitHandicapAchieved(...));
    }
}
```

## Business Logic
- Requires minimum 20 rounds for official handicap
- Uses best 8 of last 20 scoresdifferentials
- Score differential = (Score - Course Rating) × 113 / Slope
- Updates after each completed round
- Tracks trending direction

## Database Schema

### handicap_indexes
- id, user_id, current_index, previous_index
- calculation_date, rounds_used (json), trend_direction

### handicap_history
- id, user_id, handicap_index, effective_date
- rounds_count
