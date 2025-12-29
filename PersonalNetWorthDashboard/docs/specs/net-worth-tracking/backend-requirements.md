# Backend Requirements - Net Worth Tracking

## API Endpoints
- GET /api/net-worth - Get current net worth (NetWorthCalculated event)
- GET /api/net-worth/history - Get historical net worth data
- GET /api/net-worth/milestones - Get milestones (NetWorthMilestoneReached events)
- GET /api/net-worth/breakdown - Get asset/liability breakdown

## Models
```csharp
public class NetWorth {
    public decimal TotalAssets;
    public decimal TotalLiabilities;
    public decimal NetWorthAmount;
    public DateTime CalculationTimestamp;
    public decimal ChangeFromPrevious;
}
```
