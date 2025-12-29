# Budget Management - Backend Requirements

## Overview
The Budget Management feature helps users set and track budgets for their shopping lists, providing alerts when approaching limits and insights on spending patterns.

## Domain Events

### ShoppingBudgetSet
**Trigger:** When a user sets or updates a budget for a shopping list
**Payload:**
```json
{
  "budgetId": "uuid",
  "listId": "uuid",
  "userId": "uuid",
  "budgetAmount": "decimal",
  "currency": "string",
  "setAt": "datetime"
}
```

### BudgetThresholdReached
**Trigger:** When actual spending reaches defined threshold (e.g., 80%, 100%)
**Payload:**
```json
{
  "budgetId": "uuid",
  "listId": "uuid",
  "userId": "uuid",
  "budgetAmount": "decimal",
  "currentSpending": "decimal",
  "thresholdPercentage": "decimal",
  "reachedAt": "datetime"
}
```

### ActualSpendingRecorded
**Trigger:** When items are purchased and actual spending is recorded
**Payload:**
```json
{
  "listId": "uuid",
  "userId": "uuid",
  "totalSpent": "decimal",
  "budgetAmount": "decimal",
  "varianceAmount": "decimal",
  "variancePercentage": "decimal",
  "recordedAt": "datetime"
}
```

## API Endpoints

### POST /api/budgets
Create or update a budget for a shopping list
- **Request Body:**
  ```json
  {
    "listId": "uuid",
    "budgetAmount": "decimal",
    "alertThresholds": [80, 100]
  }
  ```
- **Response:** 201 Created

### GET /api/budgets/{listId}
Get budget details for a shopping list
- **Response:** 200 OK
  ```json
  {
    "budgetId": "uuid",
    "listId": "uuid",
    "budgetAmount": "decimal",
    "currentSpending": "decimal",
    "estimatedTotal": "decimal",
    "remaining": "decimal",
    "percentageUsed": "decimal",
    "status": "under | at | over",
    "alertThresholds": [80, 100]
  }
  ```

### GET /api/budgets/user/statistics
Get user's budget statistics and insights
- **Query Parameters:**
  - `period`: month | quarter | year
- **Response:** 200 OK
  ```json
  {
    "totalBudgeted": "decimal",
    "totalSpent": "decimal",
    "averageBudgetAdherence": "decimal",
    "listsUnderBudget": "integer",
    "listsOverBudget": "integer",
    "biggestSavings": {
      "listName": "string",
      "savedAmount": "decimal"
    },
    "biggestOverspend": {
      "listName": "string",
      "overAmount": "decimal"
    }
  }
  ```

### PUT /api/budgets/{budgetId}
Update budget amount or thresholds
- **Request Body:**
  ```json
  {
    "budgetAmount": "decimal",
    "alertThresholds": [80, 100]
  }
  ```
- **Response:** 200 OK

### DELETE /api/budgets/{budgetId}
Remove budget from a shopping list
- **Response:** 204 No Content

## Database Schema

### budgets Table
```sql
CREATE TABLE budgets (
    budget_id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    list_id UUID NOT NULL REFERENCES shopping_lists(list_id) ON DELETE CASCADE,
    user_id UUID NOT NULL REFERENCES users(user_id),
    budget_amount DECIMAL(10, 2) NOT NULL,
    currency VARCHAR(3) DEFAULT 'USD',
    estimated_total DECIMAL(10, 2),
    actual_total DECIMAL(10, 2),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UNIQUE(list_id)
);

CREATE INDEX idx_budgets_list ON budgets(list_id);
CREATE INDEX idx_budgets_user ON budgets(user_id);
```

### budget_alerts Table
```sql
CREATE TABLE budget_alerts (
    alert_id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    budget_id UUID NOT NULL REFERENCES budgets(budget_id) ON DELETE CASCADE,
    threshold_percentage INTEGER NOT NULL,
    triggered_at TIMESTAMP,
    is_acknowledged BOOLEAN DEFAULT FALSE
);

CREATE INDEX idx_budget_alerts_budget ON budget_alerts(budget_id);
```

### spending_history Table
```sql
CREATE TABLE spending_history (
    history_id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    list_id UUID NOT NULL REFERENCES shopping_lists(list_id),
    user_id UUID NOT NULL REFERENCES users(user_id),
    budgeted_amount DECIMAL(10, 2),
    actual_amount DECIMAL(10, 2),
    variance_amount DECIMAL(10, 2),
    variance_percentage DECIMAL(5, 2),
    completed_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE INDEX idx_spending_history_user ON spending_history(user_id);
CREATE INDEX idx_spending_history_date ON spending_history(completed_at);
```

## Business Rules

1. **Budget Creation**
   - Budget amount must be positive
   - One budget per shopping list
   - Budget can be set before or after adding items
   - Default alert thresholds: 80%, 100%

2. **Budget Tracking**
   - Estimated total calculated from item estimated prices
   - Actual total calculated from purchased item prices
   - Partial purchases counted proportionally
   - Real-time budget status updates

3. **Budget Alerts**
   - Alert at 80% spent (warning)
   - Alert at 100% spent (limit reached)
   - Alert when over budget
   - Alerts sent once per threshold
   - User can acknowledge or dismiss alerts

4. **Budget Adjustments**
   - User can increase budget mid-shopping
   - Budget changes logged for history
   - Alerts recalculated on budget changes

5. **Budget Insights**
   - Track budget adherence over time
   - Identify spending patterns
   - Suggest budget amounts based on history
   - Compare estimated vs actual spending

## Integration Points

- **List Management:** Budgets linked to shopping lists
- **Item Management:** Item prices feed into budget calculations
- **Price Tracking:** Price predictions improve budget estimates
- **Notification Service:** Send budget threshold alerts
