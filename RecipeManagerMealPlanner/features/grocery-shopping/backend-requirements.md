# Backend Requirements - Grocery Shopping

## Domain Events
- GroceryListGenerated
- GroceryItemAdded
- GroceryItemPurchased
- ShoppingTripCompleted

## API Endpoints

### Commands
- POST /api/grocery-lists - Generate from meal plans
- POST /api/grocery-lists/{id}/items - Add manual item
- PUT /api/grocery-lists/{id}/items/{itemId}/purchase - Mark purchased
- POST /api/grocery-lists/{id}/complete - Complete shopping trip
- DELETE /api/grocery-lists/{id}/items/{itemId} - Remove item

### Queries
- GET /api/grocery-lists - Get active shopping lists
- GET /api/grocery-lists/{id} - Get list details with items
- GET /api/grocery-lists/{id}/organized - Get list organized by store sections
- GET /api/grocery-lists/{id}/cost-estimate - Estimate total cost

## Domain Models

```csharp
public class GroceryList
{
    public Guid Id { get; private set; }
    public DateTime GeneratedDate { get; private set; }
    public DateRange MealPlanDateRange { get; private set; }
    public List<GroceryItem> Items { get; private set; }
    public decimal EstimatedCost { get; private set; }
    public ShoppingTripStatus Status { get; private set; }
    public ShoppingTripCompletion Completion { get; private set; }
}

public class GroceryItem
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public decimal Quantity { get; private set; }
    public Unit Unit { get; private set; }
    public StoreCategory Category { get; private set; }
    public bool IsPurchased { get; private set; }
    public decimal? ActualPrice { get; private set; }
    public List<Guid> RecipeIds { get; private set; } // Which recipes need this
}
```

## Business Rules
- Consolidate duplicate ingredients from multiple recipes
- Check pantry inventory and exclude owned items
- Organize by store sections for efficient shopping
- Track price history for cost estimation
- Alert if item needed by specific date (meal planned soon)
- Support multiple active lists (different stores)

## Validation
- Item name required
- Quantity must be positive
- Category must be valid enum
- Price must be positive if provided
