# Portfolio Management - Frontend Requirements

## Overview
User interface for creating and managing investment portfolios, viewing allocations, and executing rebalancing.

## User Stories
1. Create new portfolio with target allocation
2. View portfolio summary and current allocation
3. Monitor allocation drift from targets
4. Execute portfolio rebalancing
5. Compare multiple portfolios
6. Edit portfolio strategy and targets

## Pages/Views

### 1. Portfolios Dashboard (`/portfolios`)
- List of all portfolios
- Total portfolio value summary
- Performance metrics (Today, 1W, 1M, YTD)
- Quick actions (Create, View, Rebalance)

### 2. Create Portfolio Form
- Portfolio name and description
- Strategy type selector
- Risk tolerance slider
- Target allocation builder with pie chart
- Initial funding amount

### 3. Portfolio Details View (`/portfolios/:id`)
- Portfolio performance chart
- Current vs target allocation
- Drift indicators
- Holdings breakdown
- Rebalance recommendations

### 4. Rebalance View
- Current allocation analysis
- Recommended trades to rebalance
- Expected costs and tax impact
- Preview before execution

## UI Components

### PortfolioCard
- Portfolio name and value
- Daily change indicator
- Allocation pie chart
- Quick actions menu

### AllocationBuilder
- Asset class sliders
- Real-time percentage display
- Visual pie chart
- Validation of 100% total

### DriftIndicator
- Traffic light visualization
- Percentage deviation
- Rebalance suggestion

## State Management

```typescript
interface PortfolioState {
  portfolios: Portfolio[];
  selectedPortfolio: Portfolio | null;
  loading: boolean;
}

interface Portfolio {
  portfolioId: string;
  name: string;
  strategyType: string;
  currentValue: number;
  targetAllocation: Record<string, number>;
  currentAllocation: Record<string, number>;
}
```

## API Integration

```typescript
class PortfolioService {
  async getPortfolios(): Promise<Portfolio[]>;
  async createPortfolio(data: CreatePortfolioRequest): Promise<Portfolio>;
  async updatePortfolio(id: string, updates: UpdatePortfolioRequest): Promise<Portfolio>;
  async rebalancePortfolio(id: string): Promise<RebalanceResult>;
}
```
