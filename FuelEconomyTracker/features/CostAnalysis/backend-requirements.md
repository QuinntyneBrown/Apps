# Cost Analysis - Backend Requirements

## API Endpoints

### GET /api/vehicles/{vehicleId}/cost-analysis/monthly
**Response**: Monthly fuel cost breakdown

### GET /api/vehicles/{vehicleId}/cost-analysis/annual-projection
**Response**: Projected annual costs based on current usage

### POST /api/fuel-budgets
**Request Body**: Budget amount, period, vehicle ID
**Domain Events**: `FuelBudgetSet`

### GET /api/fuel-budgets/{id}/status
**Response**: Current spending vs budget, threshold alerts

## Domain Models

```csharp
public class FuelBudget : AggregateRoot
{
    public Guid Id { get; private set; }
    public Guid VehicleId { get; private set; }
    public decimal BudgetAmount { get; private set; }
    public BudgetPeriod Period { get; private set; }
    public DateTime StartDate { get; private set; }
    public decimal CurrentSpending { get; private set; }
    public decimal ThresholdPercentage { get; private set; }

    public void CheckThreshold()
    {
        var percentageUsed = (CurrentSpending / BudgetAmount) * 100;
        if (percentageUsed >= ThresholdPercentage)
            RaiseDomainEvent(new BudgetThresholdReached(...));
    }
}
```

## Business Logic
- Calculate cost per mile
- Track spending trends
- Alert at 75%, 90%, 100% of budget
- Project future costs based on historical patterns
- Compare costs across time periods

## Database Schema

### fuel_budgets
- id, vehicle_id, budget_amount, period, start_date, end_date
- threshold_percentage, current_spending, status

### cost_calculations
- id, vehicle_id, month, year, total_spent, gallons_purchased
- average_price_paid, miles_driven, cost_per_mile
