# Performance Analytics - Backend

## API: GET /api/analytics/win-rate
Calculate win rate (bb/100 or tournament ROI)

## API: GET /api/analytics/hourly-rate
Calculate profit per hour

## Domain Model
```csharp
public class PerformanceAnalytics : Entity
{
    public decimal WinRate { get; private set; }
    public decimal HourlyRate { get; private set; }
    public decimal StandardDeviation { get; private set; }
    public string TrendDirection { get; private set; }

    public void Calculate(List<PokerSession> sessions)
    {
        var totalProfit = sessions.Sum(s => s.ProfitLoss);
        var totalHours = sessions.Sum(s => s.DurationHours);
        HourlyRate = totalProfit / totalHours;
    }
}
```

## Database Schema
performance_stats: id, user_id, calculation_date, win_rate, hourly_rate, std_deviation, total_sessions, total_profit
