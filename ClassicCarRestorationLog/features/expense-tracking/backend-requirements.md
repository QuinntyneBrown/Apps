# Expense Tracking - Backend Requirements

## Overview
API for recording and analyzing all project costs including parts, labor, tools, and supplies.

## Domain Events
- **RestrationExpenseRecorded**: Project cost logged
- **BudgetExceeded**: Spending surpassed budget
- **ToolPurchased**: New tool acquired
- **LaborCostIncurred**: Professional service paid

## API Endpoints

### Expenses
- `POST /api/projects/{id}/expenses` - Record expense
- `GET /api/projects/{id}/expenses` - List expenses
- `GET /api/expenses/{expenseId}` - Get expense details
- `PUT /api/expenses/{expenseId}` - Update expense
- `DELETE /api/expenses/{expenseId}` - Delete expense

### Budget
- `GET /api/projects/{id}/budget` - Get budget summary
- `POST /api/projects/{id}/budget/alert` - Set budget alert
- `GET /api/projects/{id}/budget/forecast` - Budget forecast

### Analytics
- `GET /api/projects/{id}/expenses/summary` - Expense breakdown
- `GET /api/projects/{id}/expenses/trends` - Spending trends
- `GET /api/projects/{id}/expenses/export` - Export data

### Tools
- `POST /api/tools` - Add tool to inventory
- `GET /api/tools` - List all tools
- `GET /api/tools/{toolId}/roi` - Tool ROI calculation

## Data Models

### Expense
```csharp
{
    "id": "guid",
    "projectId": "guid",
    "amount": "decimal",
    "category": "enum[Parts, Labor, Tools, Supplies, Services, Other]",
    "date": "datetime",
    "vendor": "string",
    "description": "string",
    "receiptUrl": "string",
    "budgetCategory": "string",
    "necessityLevel": "enum[Essential, Important, Optional, Luxury]",
    "taxDeductible": "boolean"
}
```

### Budget
```csharp
{
    "id": "guid",
    "projectId": "guid",
    "totalBudget": "decimal",
    "categories": "dictionary<string, decimal>",
    "alertThreshold": "decimal",
    "alertsEnabled": "boolean"
}
```

### Tool
```csharp
{
    "id": "guid",
    "name": "string",
    "description": "string",
    "purchaseDate": "datetime",
    "cost": "decimal",
    "category": "string",
    "storageLocation": "string",
    "multiProjectValue": "boolean",
    "usageCount": "int"
}
```

## Business Rules
- Expenses must have valid category
- Budget alerts trigger at threshold (default 90%)
- Tool costs can be allocated across projects
- Labor costs require provider information
- Receipt upload recommended for expenses > $100

## Validation Rules
- Amount: > 0
- Vendor: 1-100 characters
- Description: 1-500 characters
- Date: cannot be in future
- Budget: > 0

## Performance Requirements
- Expense list load: < 300ms
- Budget calculation: < 200ms
- Export generation: < 5s for 1000 records
