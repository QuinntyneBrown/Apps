# Backend Requirements - Leftovers Management

## Domain Events
- LeftoversLogged
- LeftoversConsumed
- LeftoversDiscarded

## API Endpoints

### Commands
- POST /api/leftovers - Log leftovers from meal
- PUT /api/leftovers/{id}/consume - Mark leftovers eaten
- PUT /api/leftovers/{id}/discard - Mark leftovers thrown away

### Queries
- GET /api/leftovers - Active leftovers inventory
- GET /api/leftovers/expiring - Leftovers expiring soon
- GET /api/leftovers/waste-report - Food waste analytics

## Domain Models

```csharp
public class LeftoverItem
{
    public Guid Id { get; private set; }
    public Guid OriginalRecipeId { get; private set; }
    public decimal Quantity { get; private set; }
    public DateTime StorageDate { get; private set; }
    public DateTime BestByDate { get; private set; }
    public StorageLocation Location { get; private set; }
    public LeftoverStatus Status { get; private set; }
    public decimal? EstimatedCost { get; private set; }
}
```

## Business Rules
- Calculate best-by date based on food type
- Remind before expiration
- Suggest incorporating into meal plans
- Track waste metrics (quantity, cost, environmental impact)
- Recommend portion adjustments based on waste patterns
- Generate waste reduction reports
