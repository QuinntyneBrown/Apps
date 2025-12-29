# Session Tracking - Backend

## API: POST /api/poker-sessions/start
Start poker session
Domain Events: PokerSessionStarted

## API: POST /api/poker-sessions/{id}/complete
End session with results
Domain Events: SessionCompleted

## Domain Model
```csharp
public class PokerSession : AggregateRoot
{
    public Guid Id { get; private set; }
    public DateTime StartTime { get; private set; }
    public DateTime? EndTime { get; private set; }
    public GameType Type { get; private set; }
    public decimal BuyIn { get; private set; }
    public decimal CashOut { get; private set; }
    public decimal ProfitLoss { get; private set; }
    public string Location { get; private set; }
    public string Stakes { get; private set; }
    public int? HandsPlayed { get; private set; }

    public void Complete(decimal cashOut)
    {
        EndTime = DateTime.UtcNow;
        CashOut = cashOut;
        ProfitLoss = cashOut - BuyIn;
        RaiseDomainEvent(new SessionCompleted(...));
    }
}
```

## Database Schema
poker_sessions: id, user_id, start_time, end_time, game_type, buy_in, cash_out, profit_loss, location, stakes, hands_played, notes
