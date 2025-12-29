# Cash Flow Analysis - Frontend Requirements

## Overview
Visual analytics and insights into income, expenses, and financial health.

## User Stories
1. View monthly cash flow projections
2. See income vs expenses breakdown
3. Track budget vs actual spending by category
4. View cash flow trends over time
5. Get financial health score and recommendations
6. Set and manage budgets by category
7. Identify spending patterns
8. Export reports for tax purposes

## Pages/Views

### 1. Cash Flow Dashboard (`/cash-flow`)
- Current month cash flow summary
- Income vs expenses chart
- Net cash flow indicator
- Upcoming bills timeline
- Budget vs actual by category
- Financial health score

### 2. Cash Flow Projections
- Monthly projections (3-6 months ahead)
- Income sources breakdown
- Expense categories breakdown
- Projected balance over time
- What-if scenarios

### 3. Budget Management
- Set budget by category
- Track actual vs budget
- Visual progress bars
- Overspending alerts
- Historical comparison

### 4. Spending Analytics
- Category breakdown (pie/donut chart)
- Spending trends (line chart)
- Month-over-month comparison
- Top expenses
- Savings opportunities

### 5. Financial Health Dashboard
- Health score (0-100)
- Key metrics (savings rate, debt-to-income, etc.)
- Recommendations
- Improvement tracking

## UI Components

### CashFlowSummaryCard
- Income/Expense/Net values
- Visual indicator (positive/negative)
- Month selector

### CashFlowChart
- Line chart showing income and expenses over time
- Area chart for net cash flow
- Interactive tooltips

### BudgetProgressBar
- Category name
- Budget amount
- Actual spending
- Progress indicator
- Percentage used

### CategoryPieChart
- Spending by category
- Interactive segments
- Drill-down capability

### FinancialHealthScore
- Circular progress indicator
- Score value
- Rating (Excellent, Good, Fair, Poor)
- Recommendations list

## State Management

```typescript
interface CashFlowState {
  projection: CashFlowProjection | null;
  budgets: BudgetCategory[];
  healthScore: FinancialHealthScore | null;
  trends: CashFlowTrend[];
  loading: boolean;
  error: string | null;
}

interface CashFlowProjection {
  projectionId: string;
  month: Date;
  totalIncome: number;
  totalExpenses: number;
  netCashFlow: number;
  categoryBreakdown: CategorySpending[];
}

interface BudgetCategory {
  categoryId: string;
  categoryName: string;
  budgetAmount: number;
  actualAmount: number;
  percentageUsed: number;
  isOverBudget: boolean;
}

interface FinancialHealthScore {
  score: number;
  rating: 'Excellent' | 'Good' | 'Fair' | 'Poor';
  savingsRate: number;
  debtToIncome: number;
  recommendations: string[];
}
```

## Charts and Visualizations

### Cash Flow Line Chart
- X-axis: Months
- Y-axis: Amount
- Lines: Income (green), Expenses (red), Net (blue)
- Responsive and interactive

### Category Pie Chart
- Sectors: Bill categories
- Colors: Category-specific colors
- Hover: Show amount and percentage

### Budget vs Actual Bar Chart
- X-axis: Categories
- Y-axis: Amount
- Bars: Budget (outline), Actual (filled)

## Notifications

### Alerts
- "You're projected to have negative cash flow next month"
- "Utilities spending is 95% of budget"
- "Great job! You're 15% under budget this month"

### Insights
- "Your average monthly bill payment is $2,450"
- "You could save $120/month by cutting these subscriptions"
- "Your savings rate has improved by 5% this quarter"
