# Budget Management - Frontend Requirements

## Overview
Provide users with intuitive budget tracking and spending insights to help them shop within their means.

## User Stories

### US-BM-001: Set Shopping Budget
**As a** user **I want to** set a budget for my shopping list **So that** I can control my spending

**Acceptance Criteria:**
- User can set budget when creating or editing a list
- Budget input with currency symbol
- Option to base budget on previous shopping trips
- Budget amount validation (must be positive)
- Save and update budget instantly

### US-BM-002: Track Budget Progress
**As a** user **I want to** see my budget progress in real-time **So that** I know how much I have left to spend

**Acceptance Criteria:**
- Visual progress bar showing budget usage
- Display: budgeted, estimated, spent, remaining
- Color coding: green (under), yellow (near), red (over)
- Updates as items are added/purchased
- Percentage and dollar amounts shown

### US-BM-003: Receive Budget Alerts
**As a** user **I want to** get notified when approaching budget limits **So that** I can adjust my shopping

**Acceptance Criteria:**
- Alert at 80% of budget (warning)
- Alert at 100% of budget (limit reached)
- Alert when over budget
- In-app and push notifications
- Option to acknowledge or adjust budget

### US-BM-004: View Spending History
**As a** user **I want to** see my budget performance over time **So that** I can improve my budgeting

**Acceptance Criteria:**
- Chart showing budgeted vs actual over time
- List of completed shopping trips with variance
- Average budget adherence percentage
- Best and worst budget performances
- Filter by date range

## UI Components

### BudgetProgressBar Component
```javascript
{
  budgetAmount: 150.00,
  estimatedTotal: 127.45,
  actualSpent: 95.30,
  status: 'under | at | over'
}
```

### BudgetSetDialog Component
- Input for budget amount
- Suggested budget based on history
- Alert threshold settings
- Save/cancel actions

### BudgetAlertCard Component
- Alert type and severity
- Current vs budget amounts
- Quick actions (acknowledge, adjust budget, view details)

### SpendingHistoryChart Component
- Line/bar chart of budget performance
- Interactive tooltips
- Time range selector
- Export functionality

## State Management

```javascript
{
  budgetManagement: {
    budgets: {
      byListId: {
        'list-1': {
          budgetId, budgetAmount, currentSpending,
          estimatedTotal, remaining, percentageUsed, status
        }
      }
    },
    alerts: {
      active: [...],
      acknowledged: [...]
    },
    history: {
      records: [...],
      statistics: { ... }
    }
  }
}
```

## Actions
- `setBudget(listId, budgetAmount)`
- `updateBudget(budgetId, newAmount)`
- `fetchBudget(listId)`
- `fetchSpendingHistory(filters)`
- `acknowledgeAlert(alertId)`
- `deleteBudget(budgetId)`
