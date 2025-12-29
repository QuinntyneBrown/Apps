# Profitability Analysis - Backend Requirements

## Domain Model
### ProfitLossStatement Aggregate
- StatementId, StreamId, UserId, Period, TotalRevenue, TotalExpenses, NetProfit, ProfitMargin, CalculationDate

### Milestone Aggregate
- MilestoneId, StreamId, UserId, MilestoneType, Amount, AchievementDate

## Commands
- CalculateProfitLossCommand: Computes P&L for period, raises **ProfitLossCalculated** event
- CheckProfitabilityThresholdCommand: Monitors profitability, raises **ProfitabilityThresholdReached** event
- CheckRevenueRecordCommand: Tracks records, raises **MonthlyRevenueRecordSet** event

## Queries
- GetProfitLossQuery: Returns P&L for stream and period
- GetProfitabilityTrendsQuery: Multi-period comparison
- GetStreamComparisonQuery: Compare all streams
- GetMilestonesQuery: Achievement history

## API Endpoints
- POST /api/profitability/calculate (generate P&L)
- GET /api/profitability/stream/{id} (get P&L)
- GET /api/profitability/comparison (compare streams)
- GET /api/profitability/milestones (get achievements)

## Events
- **ProfitLossCalculated**: Updates financial dashboard
- **ProfitabilityThresholdReached**: Celebrates achievement
- **MonthlyRevenueRecordSet**: Sends congratulations notification
