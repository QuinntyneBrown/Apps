# Budget Management - Backend Requirements

## Overview
Manages creation, allocation, amendment, and closure of household budget periods with category-based budget tracking.

## Domain Model

### Budget Aggregate
- **BudgetId**: Guid
- **HouseholdId**: Guid
- **BudgetPeriod**: string (e.g., "December 2025")
- **StartDate**: DateTime
- **EndDate**: DateTime
- **TotalBudgetAmount**: decimal
- **CreatorUserId**: Guid
- **Status**: BudgetStatus (Active, Closed, Archived)
- **Categories**: List<CategoryAllocation>
- **CreatedAt**: DateTime
- **UpdatedAt**: DateTime

### CategoryAllocation Entity
- **AllocationId**: Guid
- **BudgetId**: Guid
- **CategoryId**: Guid
- **CategoryName**: string
- **AllocatedAmount**: decimal
- **SpentAmount**: decimal
- **PercentageOfTotal**: decimal
- **CreatedBy**: Guid
- **CreatedAt**: DateTime

### BudgetStatus Enum
- Active, Closed, Archived

## Commands

### CreateBudgetCommand
- Validates household exists
- Validates total budget > 0
- Validates date range
- Creates Budget aggregate
- Raises **BudgetCreated** event

### AllocateCategoryBudgetCommand
- Validates budget exists and is active
- Validates allocation amount > 0
- Checks total allocations <= total budget
- Creates CategoryAllocation
- Raises **BudgetCategoryAllocated** event

### AmendBudgetCommand
- Validates budget exists
- Records amendment reason
- Updates allocation amounts
- Requires approver if > 10% change
- Raises **BudgetAmendmentMade** event

### CloseBudgetPeriodCommand
- Validates budget period ended
- Calculates surplus/deficit
- Generates final report
- Sets status to Closed
- Raises **BudgetPeriodClosed** event

## Queries

### GetBudgetByIdQuery
- Returns budget with all allocations
- Includes spent amounts per category
- Calculates remaining budget

### GetActiveBudgetQuery
- Returns current active budget for household
- Includes real-time spending data

### GetBudgetHistoryQuery
- Returns past budgets with summaries
- Supports date range filtering
- Includes performance metrics

## Domain Events

```csharp
public class BudgetCreated : DomainEvent
{
    public Guid BudgetId { get; set; }
    public Guid HouseholdId { get; set; }
    public string BudgetPeriod { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal TotalBudgetAmount { get; set; }
    public Guid CreatorUserId { get; set; }
}

public class BudgetCategoryAllocated : DomainEvent
{
    public Guid BudgetId { get; set; }
    public Guid CategoryId { get; set; }
    public decimal AllocatedAmount { get; set; }
    public decimal PercentageOfTotal { get; set; }
    public Guid UserId { get; set; }
}

public class BudgetAmendmentMade : DomainEvent
{
    public Guid BudgetId { get; set; }
    public Guid CategoryId { get; set; }
    public decimal PreviousAmount { get; set; }
    public decimal NewAmount { get; set; }
    public string AmendmentReason { get; set; }
    public Guid ApproverId { get; set; }
}

public class BudgetPeriodClosed : DomainEvent
{
    public Guid BudgetId { get; set; }
    public decimal TotalBudgeted { get; set; }
    public decimal TotalSpent { get; set; }
    public decimal SurplusDeficit { get; set; }
}
```

## API Endpoints

### POST /api/budgets
- Creates new budget period
- Returns: 201 Created with BudgetId

### POST /api/budgets/{budgetId}/categories
- Allocates budget to category
- Returns: 201 Created with AllocationId

### PUT /api/budgets/{budgetId}/categories/{categoryId}
- Amends category allocation
- Returns: 200 OK

### POST /api/budgets/{budgetId}/close
- Closes budget period
- Returns: 200 OK with final report

### GET /api/budgets/{budgetId}
- Retrieves budget details
- Returns: 200 OK with BudgetDto

### GET /api/budgets/active
- Gets current active budget
- Returns: 200 OK with BudgetDto

## Business Rules
1. Only one active budget per household at a time
2. Category allocations cannot exceed total budget
3. Cannot close budget before end date unless approved
4. Cannot modify closed budgets
5. Budget amendments > 10% require admin approval
6. Must allocate at least 80% of total budget to categories
