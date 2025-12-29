# Backend Requirements - Pantry Management

## Domain Events
- PantryItemAdded
- PantryItemUsed
- PantryItemExpiring
- PantryStockLow

## API Endpoints

### Commands
- POST /api/pantry/items - Add item to pantry
- PUT /api/pantry/items/{id}/use - Log ingredient usage
- PUT /api/pantry/items/{id} - Update quantity/details
- DELETE /api/pantry/items/{id} - Remove item

### Queries
- GET /api/pantry/items - List all pantry items
- GET /api/pantry/expiring - Items expiring soon
- GET /api/pantry/low-stock - Items below minimum threshold
- GET /api/pantry/check-availability - Check ingredients for recipe

## Domain Models

```csharp
public class PantryItem
{
    public Guid Id { get; private set; }
    public string IngredientName { get; private set; }
    public decimal Quantity { get; private set; }
    public Unit Unit { get; private set; }
    public DateTime PurchaseDate { get; private set; }
    public DateTime? ExpirationDate { get; private set; }
    public StorageLocation Location { get; private set; }
    public decimal? Cost { get; private set; }
    public decimal MinimumThreshold { get; private set; }
}
```

## Business Rules
- Alert 3 days before expiration (configurable)
- Auto-add to shopping list when below minimum
- Suggest recipes using expiring ingredients
- Track usage patterns for restock recommendations
- Update inventory when recipe cooked
- Support multiple storage locations

## Background Jobs
- Daily expiration check (run at 6 AM)
- Weekly low-stock report
- Monthly usage analytics
