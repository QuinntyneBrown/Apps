# Backend Requirements - Sleep Debt Management

## API Endpoints

### GET /api/sleep-debt/{userId}
Get current sleep debt
- **Response**: Total debt hours, severity level, accumulation period

### POST /api/sleep-debt/calculate
Calculate sleep debt for user
- **Events**: `SleepDebtAccumulated`, `SleepDebtRepaid`, `CriticalSleepDebtReached`

### GET /api/sleep-debt/{userId}/history
Get debt history over time

## Domain Models
### SleepDebt: Id, UserId, TotalDebtHours, SeverityLevel, CalculatedAt
### DebtTransaction: Id, UserId, TransactionType (Accumulation/Repayment), Amount, Date

## Business Logic
- Accumulate debt when sleep < target (goal hours - actual hours)
- Repay debt when sleep > target (max 2 hours per session)
- Severity levels: Mild (0-3h), Moderate (3-6h), Severe (6-10h), Critical (10+h)
- Calculate on each sleep session completion

## Events
SleepDebtAccumulated, SleepDebtRepaid, CriticalSleepDebtReached with debt metrics

## Database Schema
SleepDebt table, DebtTransactions table with userId indexing
