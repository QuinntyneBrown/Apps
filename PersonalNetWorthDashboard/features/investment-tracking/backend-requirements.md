# Backend Requirements - Investment Tracking

## API Endpoints
- POST /api/investments/positions - Add position (InvestmentPositionAdded event)
- GET /api/investments/returns - Get returns (InvestmentReturnsCalculated event)
- GET /api/investments/allocation - Get asset allocation
- GET /api/investments/performance - Get performance metrics

## Models
```csharp
public class InvestmentPosition {
    public Guid Id;
    public string TickerSymbol;
    public decimal Quantity;
    public decimal PurchasePrice;
    public decimal CurrentPrice;
    public string AssetClass;
}
```
