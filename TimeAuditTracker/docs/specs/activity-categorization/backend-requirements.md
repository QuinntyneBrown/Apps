# Backend Requirements - Activity Categorization

## Domain Events
- ActivityCategorized
- CustomCategoryCreated
- CategoryBudgetSet
- BudgetExceeded

## API Endpoints

### POST /api/categories
Create custom category
- Request: { name, description, colorCode, parentCategoryId }
- Response: { categoryId, timestamp }
- Event: CustomCategoryCreated

### GET /api/categories
List all categories (predefined + custom)
- Response: { categories[], hierarchy }

### POST /api/categories/budgets
Set time budget for category
- Request: { categoryId, targetHours, period, rationale }
- Response: { budgetId, timestamp }
- Event: CategoryBudgetSet

### GET /api/categories/{id}/budget-status
Check budget status
- Response: { budgeted, actual, remaining, isExceeded }

### PUT /api/time-entries/{id}/categorize
Categorize or recategorize time entry
- Request: { categoryId, subcategory, tags, confidence }
- Response: { entryId, category, timestamp }
- Event: ActivityCategorized

### GET /api/analytics/category-distribution
Get time distribution across categories
- Query: startDate, endDate, period (day/week/month)
- Response: { categories[], percentages[], totalTime }

## Data Models

### Category
```typescript
{
  id: UUID,
  userId: UUID,
  name: string,
  description: string,
  colorCode: string, // hex color
  parentCategoryId?: UUID,
  icon?: string,
  isCustom: boolean,
  isPredefined: boolean,
  createdAt: DateTime
}
```

### CategoryBudget
```typescript
{
  id: UUID,
  userId: UUID,
  categoryId: UUID,
  targetHours: number,
  period: 'daily' | 'weekly' | 'monthly',
  rationale: string,
  createdAt: DateTime,
  isActive: boolean
}
```

### BudgetExceedance
```typescript
{
  id: UUID,
  budgetId: UUID,
  categoryId: UUID,
  budgetedAmount: number,
  actualAmount: number,
  overage: number,
  periodStart: Date,
  periodEnd: Date,
  detectedAt: DateTime
}
```

## Business Logic
- Monitor category time accumulation in real-time
- Trigger BudgetExceeded event when limit reached
- Calculate budget utilization percentage
- Support hierarchical category budgets (parent + children)
- Auto-suggest categories based on activity patterns
- Validate color codes are valid hex values

## Database Schema
```sql
CREATE TABLE categories (
    id UUID PRIMARY KEY,
    user_id UUID NOT NULL,
    name VARCHAR(255) NOT NULL,
    description TEXT,
    color_code VARCHAR(7) NOT NULL,
    parent_category_id UUID,
    icon VARCHAR(50),
    is_custom BOOLEAN DEFAULT TRUE,
    is_predefined BOOLEAN DEFAULT FALSE,
    created_at TIMESTAMP DEFAULT NOW(),
    FOREIGN KEY (user_id) REFERENCES users(id),
    FOREIGN KEY (parent_category_id) REFERENCES categories(id)
);

CREATE TABLE category_budgets (
    id UUID PRIMARY KEY,
    user_id UUID NOT NULL,
    category_id UUID NOT NULL,
    target_hours DECIMAL(5,2) NOT NULL,
    period VARCHAR(20) NOT NULL,
    rationale TEXT,
    is_active BOOLEAN DEFAULT TRUE,
    created_at TIMESTAMP DEFAULT NOW(),
    FOREIGN KEY (user_id) REFERENCES users(id),
    FOREIGN KEY (category_id) REFERENCES categories(id)
);

CREATE TABLE budget_exceedances (
    id UUID PRIMARY KEY,
    budget_id UUID NOT NULL,
    category_id UUID NOT NULL,
    budgeted_amount DECIMAL(5,2),
    actual_amount DECIMAL(5,2),
    overage DECIMAL(5,2),
    period_start DATE,
    period_end DATE,
    detected_at TIMESTAMP DEFAULT NOW(),
    FOREIGN KEY (budget_id) REFERENCES category_budgets(id),
    FOREIGN KEY (category_id) REFERENCES categories(id)
);

CREATE INDEX idx_categories_user ON categories(user_id);
CREATE INDEX idx_budgets_category ON category_budgets(category_id);
```

## Integration Points
- Analytics service for category time aggregation
- Alert service for budget overage notifications
- ML service for auto-categorization suggestions
