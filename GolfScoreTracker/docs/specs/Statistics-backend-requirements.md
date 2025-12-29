# Statistics - Backend Requirements

## API Endpoints

### GET /api/users/{userId}/statistics
Query params: period (7d, 30d, 90d, year, all)
Returns: Comprehensive statistics

### GET /api/users/{userId}/statistics/trends
Returns: Performance trends and insights

## Domain Model

```csharp
public class PlayerStatistics : Entity
{
    public decimal ScoringAverage { get; private set; }
    public decimal FairwaysHitPercentage { get; private set; }
    public decimal GIRPercentage { get; private set; }
    public decimal AveragePuttsPerRound { get; private set; }
    public int TotalBirdies { get; private set; }
    public int TotalEagles { get; private set; }
    public int TotalPenalties { get; private set; }
    public string StrongestArea { get; private set; }
    public string WeakestArea { get; private set; }

    public void Calculate(List<Round> rounds)
    {
        ScoringAverage = rounds.Average(r => r.TotalScore);
        FairwaysHitPercentage = CalculateFairwayPercentage(rounds);
        GIRPercentage = CalculateGIRPercentage(rounds);
        // ... more calculations

        IdentifyStrengthsAndWeaknesses();
    }
}
```

## Business Logic
- Scoring average: mean of all round scores
- Fairway%: fairways hit / fairways attempted (par 4-5 only)
- GIR%: greens hit / greens attempted
- Putts/round: average putts across rounds
- Strengths/weaknesses based on percentile rankings

## Database Schema

### player_statistics
- id, user_id, period, scoring_average
- fairways_pct, gir_pct, avg_putts, total_birdies
- strongest_area, weakest_area, last_calculated
