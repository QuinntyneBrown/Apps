# Portfolio Management - Backend Requirements

## Overview
Core functionality for creating portfolios, tracking aggregate values, managing asset allocations, and executing rebalancing operations.

## Domain Model

### Portfolio Aggregate
- **PortfolioId**: Unique identifier (Guid)
- **UserId**: Owner (Guid)
- **Name**: Portfolio name (string, max 200)
- **Description**: Portfolio description (string, max 1000)
- **StrategyType**: Investment strategy (enum: Growth, Income, Balanced, Conservative, Aggressive)
- **RiskTolerance**: Risk level (enum: Low, Medium, High)
- **TargetAllocation**: Dictionary<AssetClass, decimal> (percentages)
- **CurrentValue**: Total portfolio value (decimal)
- **CashBalance**: Available cash (decimal)
- **CreatedAt**: Creation timestamp (DateTime)
- **UpdatedAt**: Last modification timestamp (DateTime)

### AssetAllocation Value Object
- **AssetClass**: Asset type (enum: Stocks, Bonds, Cash, RealEstate, Commodities, Crypto)
- **TargetPercentage**: Target allocation (decimal, 0-100)
- **CurrentPercentage**: Actual allocation (decimal, 0-100)
- **DriftPercentage**: Deviation from target (decimal)

## Commands

### CreatePortfolioCommand
- Creates new portfolio
- Validates target allocation sums to 100%
- Raises **PortfolioCreated** event

### UpdatePortfolioCommand
- Updates portfolio details and target allocation
- Validates allocation percentages
- Raises **PortfolioValueUpdated** event

### RebalancePortfolioCommand
- Executes trades to restore target allocation
- Calculates required buy/sell orders
- Raises **PortfolioRebalanced** event

### DeletePortfolioCommand
- Soft deletes portfolio
- Ensures no active positions

## Queries

### GetPortfolioByIdQuery
- Returns portfolio with current allocation

### GetPortfoliosByUserIdQuery
- Returns all user portfolios

### GetPortfolioPerformanceQuery
- Calculates returns and metrics

## Domain Events

### PortfolioCreated
```csharp
public class PortfolioCreated : DomainEvent
{
    public Guid PortfolioId { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public string StrategyType { get; set; }
    public Dictionary<string, decimal> TargetAllocation { get; set; }
    public DateTime OccurredAt { get; set; }
}
```

### PortfolioRebalanced
```csharp
public class PortfolioRebalanced : DomainEvent
{
    public Guid PortfolioId { get; set; }
    public Dictionary<string, decimal> PreviousAllocation { get; set; }
    public Dictionary<string, decimal> NewAllocation { get; set; }
    public List<TradeOrder> TradesExecuted { get; set; }
    public DateTime RebalanceDate { get; set; }
    public DateTime OccurredAt { get; set; }
}
```

### PortfolioValueUpdated
```csharp
public class PortfolioValueUpdated : DomainEvent
{
    public Guid PortfolioId { get; set; }
    public decimal PreviousValue { get; set; }
    public decimal NewValue { get; set; }
    public decimal ChangeAmount { get; set; }
    public decimal ChangePercentage { get; set; }
    public DateTime OccurredAt { get; set; }
}
```

## API Endpoints

### POST /api/portfolios
- Creates portfolio
- Returns: 201 Created

### PUT /api/portfolios/{portfolioId}
- Updates portfolio
- Returns: 200 OK

### GET /api/portfolios/{portfolioId}
- Gets portfolio details
- Returns: 200 OK

### GET /api/portfolios
- Gets user portfolios
- Returns: 200 OK

### POST /api/portfolios/{portfolioId}/rebalance
- Executes rebalancing
- Returns: 200 OK

### DELETE /api/portfolios/{portfolioId}
- Deletes portfolio
- Returns: 204 No Content

## Business Rules

1. **Allocation Sum**: Target allocations must sum to 100%
2. **Rebalancing Threshold**: Trigger rebalance when drift > 5%
3. **Minimum Cash**: Maintain 2% cash minimum
4. **Name Uniqueness**: Portfolio names unique per user

## Data Persistence

### Portfolios Table
```sql
CREATE TABLE Portfolios (
    PortfolioId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    UserId UNIQUEIDENTIFIER NOT NULL,
    Name NVARCHAR(200) NOT NULL,
    Description NVARCHAR(1000) NULL,
    StrategyType NVARCHAR(50) NOT NULL,
    RiskTolerance NVARCHAR(20) NOT NULL,
    TargetAllocation NVARCHAR(MAX) NOT NULL,
    CurrentValue DECIMAL(18,2) NOT NULL DEFAULT 0,
    CashBalance DECIMAL(18,2) NOT NULL DEFAULT 0,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),

    CONSTRAINT FK_Portfolios_Users FOREIGN KEY (UserId) REFERENCES Users(UserId),
    INDEX IX_Portfolios_UserId (UserId)
);
```
