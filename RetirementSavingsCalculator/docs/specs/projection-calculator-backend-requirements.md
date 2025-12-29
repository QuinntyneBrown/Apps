# Backend Requirements - Projection Calculator

## API Endpoints

### POST /api/projections/calculate
Calculate retirement projections
```json
Request: {scenarioId}
Response: {
  "totalSavingsAtRetirement": 1250000,
  "monthlyRetirementIncome": 5200,
  "savingsGap": -50000,
  "successProbability": 0.85,
  "yearByYearProjection": [...]
}
```
**Events**: RetirementProjectionCalculated

### GET /api/projections/{scenarioId}/monte-carlo
Run Monte Carlo simulation
Response: Success probability distribution

## Calculation Engine

### Compound Growth Formula
```
FV = PV × (1 + r)^n + PMT × [((1 + r)^n - 1) / r]
Where:
- FV = Future Value
- PV = Present Value (current savings)
- r = annual return rate / 12
- n = months until retirement
- PMT = monthly contribution
```

### Withdrawal Calculation
```
4% rule: Annual withdrawal = Total savings × 0.04
Adjusted for inflation each year
```

### Monte Carlo Simulation
- Run 10,000 scenarios
- Vary returns: mean = expected return, std dev = historical volatility
- Calculate success rate (scenarios where money lasts through life expectancy)

## Business Rules
- BR-PC-001: Use 4% safe withdrawal rate as default
- BR-PC-002: Account for taxes on withdrawals (traditional IRA/401k)
- BR-PC-003: Social Security not taxed if below threshold
- BR-PC-004: Recalculate when any scenario parameter changes

## Event Publishing
```json
RetirementProjectionCalculated: {
  "scenarioId": "uuid",
  "calculationDate": "2025-12-29",
  "results": {
    "savingsAtRetirement": 1250000,
    "monthlyIncome": 5200,
    "gap": -50000,
    "successRate": 0.85
  }
}
```
