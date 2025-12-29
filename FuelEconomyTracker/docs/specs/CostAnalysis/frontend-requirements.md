# Cost Analysis - Frontend Requirements

## Pages

### Cost Dashboard (`/vehicles/{id}/costs`)
- **Budget Card**: Current spending vs budget with progress bar
- **Monthly Summary**: This month's total, avg cost per gallon
- **Annual Projection**: Estimated yearly cost
- **Cost Trends Chart**: Monthly spending over time
- **Cost Per Mile**: Calculate and display
- **Set Budget Button**: Opens budget configuration modal

### Budget Configuration Modal
- Budget amount input
- Period selector (Monthly/Quarterly/Annual)
- Alert threshold slider (default 75%)
- Start date picker
- Visual preview of budget constraints

### Cost Reports (`/costs/reports`)
- Selectable time periods
- Expense breakdown (by fuel grade, station, purpose)
- Export to PDF/CSV for tax purposes
- Cost comparison across months/years

## UI Components

### BudgetProgressBar
- Color-coded: Green (<75%), Yellow (75-90%), Red (>90%)
- Shows amount remaining
- Days left in period

### CostTrendChart
- Line/bar chart of spending over time
- Highlight budget limit line
- Interactive tooltips

## State Management

```typescript
interface CostAnalysisState {
  monthlySpending: number;
  annualProjection: number;
  budget?: FuelBudget;
  budgetStatus: {
    percentageUsed: number;
    remaining: number;
    daysLeft: number;
  };
  costHistory: MonthlyCost[];
  costPerMile: number;
}
```
