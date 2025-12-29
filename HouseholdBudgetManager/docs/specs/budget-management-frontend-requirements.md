# Budget Management - Frontend Requirements

## Overview
User interface for creating budgets, allocating funds to categories, monitoring spending, and managing budget periods.

## Key Pages

### 1. Budget Dashboard
**Route**: `/budget/dashboard`

**Features**:
- Current budget period display
- Total budget vs spent gauge
- Category breakdown chart (pie/donut)
- Spending by category (bar chart)
- Quick stats cards
- Budget health indicators
- Alert banners for exceeded categories

### 2. Create Budget Wizard
**Route**: `/budget/create`

**Steps**:
1. **Period Setup**: Select dates, name budget period
2. **Total Budget**: Enter total household budget amount
3. **Category Allocation**: Distribute budget across categories
4. **Review & Create**: Preview and confirm

**Features**:
- Visual allocation pie chart
- Drag sliders to allocate amounts
- Percentage and dollar amount inputs
- Template selection (last month, average, etc.)
- Unallocated amount warning

### 3. Budget Overview
**Route**: `/budget/{budgetId}`

**Sections**:
- **Header**: Period, dates, total budget, status
- **Summary Cards**: Total budgeted, spent, remaining, categories
- **Category List**: Each category with allocated vs spent
- **Progress Bars**: Visual spending indicators per category
- **Alert Section**: Exceeded or approaching limit categories
- **Actions**: Amend budget, close period, export

### 4. Category Allocation Manager
**Component**: Allocation interface

**Features**:
- Category list with allocation inputs
- Visual budget distribution chart
- Real-time total calculation
- Percentage auto-calculation
- Validation warnings
- Remaining budget indicator
- Quick allocation templates
- Add/remove categories

### 5. Amend Budget Dialog
**Component**: Modal

**Fields**:
- Category selection
- Current allocation display
- New allocation amount
- Amendment reason (required if > 10% change)
- Approval required indicator
- Impact preview

## UI Components

### BudgetGauge
- Circular progress gauge
- Shows spent/budget ratio
- Color-coded (green: healthy, yellow: warning, red: exceeded)
- Animated on load

### CategoryProgressBar
- Horizontal bar with spent/allocated
- Color-coded by status
- Percentage label
- Hover shows exact amounts

### AllocationChart
- Interactive pie chart
- Click category to highlight
- Shows percentages and amounts
- Export capability

### BudgetHealthCard
- Summary metric card
- Icon, value, label
- Trend indicator (up/down)
- Click for details

## User Flows

### Create Budget Flow
1. User clicks "Create New Budget"
2. Wizard opens on period setup
3. User selects month and year
4. User enters total budget amount
5. User allocates to categories (slider/input)
6. Chart updates in real-time
7. User reviews allocation
8. User clicks "Create Budget"
9. Budget created and activated
10. Redirect to budget dashboard

### Amend Budget Flow
1. User views budget overview
2. User clicks "Amend Budget"
3. User selects category to modify
4. User enters new amount
5. System checks if approval needed
6. User enters reason if required
7. Preview shows impact
8. User confirms amendment
9. Approval request sent if needed
10. Budget updated when approved

## State Management

```typescript
interface BudgetState {
  activeBudget: Budget | null;
  budgetHistory: Budget[];
  loading: boolean;
  error: string | null;
}

interface Budget {
  budgetId: string;
  householdId: string;
  budgetPeriod: string;
  startDate: Date;
  endDate: Date;
  totalBudgetAmount: number;
  status: 'Active' | 'Closed' | 'Archived';
  categories: CategoryAllocation[];
  totalSpent: number;
  remainingBudget: number;
}

interface CategoryAllocation {
  allocationId: string;
  categoryId: string;
  categoryName: string;
  allocatedAmount: number;
  spentAmount: number;
  percentageOfTotal: number;
  status: 'Healthy' | 'Warning' | 'Exceeded';
}
```

## Responsive Design

### Mobile
- Stacked cards
- Simplified charts
- Swipe for category details
- Bottom sheet for allocation

### Tablet
- Two-column layout
- Expanded charts
- Side panel for details

### Desktop
- Multi-column dashboard
- Full-featured charts
- Side-by-side comparison views

## Testing
- E2E: Complete budget creation flow
- E2E: Budget amendment with approval
- E2E: Close budget period
- Unit: Allocation calculations
- Integration: Real-time spending updates
