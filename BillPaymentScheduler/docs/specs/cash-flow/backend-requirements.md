# Cash Flow Analysis - Backend Requirements

## Overview
Cash flow feature provides users with insights into their income, expenses, and financial health through projections and analytics.

## Domain Model

### CashFlowProjection Aggregate
- **ProjectionId**: Unique identifier (Guid)
- **UserId**: Owner (Guid)
- **Month**: Month of projection (DateTime)
- **TotalIncome**: Projected income (decimal)
- **TotalExpenses**: Projected expenses (decimal)
- **NetCashFlow**: Income - Expenses (decimal)
- **BillPayments**: List of projected bill payments
- **CreatedAt**: Creation timestamp (DateTime)
- **UpdatedAt**: Last modification timestamp (DateTime)

### BudgetCategory Entity
- **CategoryId**: Unique identifier (Guid)
- **UserId**: Owner (Guid)
- **CategoryName**: Category name (string)
- **BudgetAmount**: Allocated amount (decimal)
- **ActualAmount**: Spent amount (decimal)
- **Month**: Month of budget (DateTime)

## Commands

### GenerateCashFlowProjectionCommand
- Calculates projected cash flow for specified period
- Analyzes recurring bills and scheduled payments
- Returns projection summary

### SetBudgetCategoryCommand
- Creates or updates budget for a category
- Returns budget details

### UpdateIncomeCommand
- Updates user's income information
- Recalculates projections

## Queries

### GetCashFlowProjectionQuery
- Returns cash flow projection for specified month
- Includes detailed breakdown

### GetCashFlowTrendQuery
- Returns cash flow trends over time period
- Supports monthly, quarterly, yearly views

### GetBudgetVsActualQuery
- Compares budgeted amounts vs actual spending
- Returns variance analysis

### GetSpendingByCategoryQuery
- Returns spending breakdown by category
- Supports date range filtering

### GetFinancialHealthScoreQuery
- Calculates financial health metrics
- Returns score and recommendations

## API Endpoints

### GET /api/cash-flow/projection
- Gets cash flow projection
- Query params: month, year
- Returns: CashFlowProjectionDto

### GET /api/cash-flow/trend
- Gets cash flow trend
- Query params: startDate, endDate, period
- Returns: List<CashFlowTrendDto>

### POST /api/cash-flow/income
- Updates income information
- Returns: 200 OK

### GET /api/cash-flow/budget
- Gets budget categories
- Returns: List<BudgetCategoryDto>

### POST /api/cash-flow/budget
- Sets budget for category
- Returns: 201 Created

### GET /api/cash-flow/health-score
- Gets financial health score
- Returns: FinancialHealthScoreDto

## Business Rules

1. **Projection Accuracy**: Projections based on historical data and scheduled payments
2. **Budget Alerts**: Notify when spending exceeds 80% of budget
3. **Cash Flow Warnings**: Alert when projected negative cash flow
4. **Income Verification**: Require income updates every 6 months
5. **Category Rollup**: Sum all bill categories for total expenses

## Background Jobs

### CashFlowCalculatorJob
- **Schedule**: Runs daily
- **Purpose**: Update cash flow projections
- **Logic**:
  1. Calculate upcoming bill payments
  2. Sum total expenses
  3. Calculate net cash flow
  4. Generate alerts if needed

## Data Persistence

### CashFlowProjections Table
```sql
CREATE TABLE CashFlowProjections (
    ProjectionId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    UserId UNIQUEIDENTIFIER NOT NULL,
    Month DATETIME2 NOT NULL,
    TotalIncome DECIMAL(18,2) NOT NULL,
    TotalExpenses DECIMAL(18,2) NOT NULL,
    NetCashFlow DECIMAL(18,2) NOT NULL,
    ProjectionData NVARCHAR(MAX) NOT NULL, -- JSON
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),

    CONSTRAINT FK_CashFlowProjections_Users FOREIGN KEY (UserId) REFERENCES Users(UserId),
    UNIQUE INDEX UX_CashFlowProjections_UserMonth (UserId, Month)
);
```

### BudgetCategories Table
```sql
CREATE TABLE BudgetCategories (
    CategoryId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    UserId UNIQUEIDENTIFIER NOT NULL,
    CategoryName NVARCHAR(100) NOT NULL,
    BudgetAmount DECIMAL(18,2) NOT NULL,
    ActualAmount DECIMAL(18,2) NOT NULL DEFAULT 0,
    Month DATETIME2 NOT NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),

    CONSTRAINT FK_BudgetCategories_Users FOREIGN KEY (UserId) REFERENCES Users(UserId),
    UNIQUE INDEX UX_BudgetCategories_UserCategoryMonth (UserId, CategoryName, Month)
);
```
