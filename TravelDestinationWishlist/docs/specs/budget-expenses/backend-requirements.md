# Backend Requirements - Budget and Expenses

## API Endpoints
- POST /api/trips/{id}/budget - Set budget (TripBudgetSet)
- POST /api/trips/{id}/expenses - Record expense (TravelExpenseRecorded)
- GET /api/trips/{id}/budget/status - Get budget status (BudgetThresholdReached)
- POST /api/trips/{id}/reconcile - Reconcile costs (TripCostReconciled)

## Models
```csharp
public class TripBudget {
    public Guid TripId;
    public decimal TotalBudget;
    public Dictionary<string, decimal> CategoryAllocations;
    public decimal BufferAmount;
}
```
