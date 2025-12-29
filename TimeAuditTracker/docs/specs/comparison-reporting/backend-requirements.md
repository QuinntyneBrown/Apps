# Backend Requirements - Comparison & Reporting

## Domain Events
- PeriodCompared
- IdealVsActualCompared

## API Endpoints
- POST /api/comparisons/periods - Compare two time periods
- POST /api/comparisons/ideal-vs-actual - Compare ideal vs actual allocation
- GET /api/comparisons/{id} - Get comparison results
- GET /api/reports/export - Export reports (PDF, CSV)

## Data Models
```typescript
PeriodComparison {
  id, period1, period2, keyDifferences, improvements, regressions
}
IdealVsActualComparison {
  id, categoryVariances[], alignmentScore, priorityGaps[]
}
```

## Business Logic
- Calculate category-by-category variance between periods
- Identify improvements (positive changes) and regressions
- Compare ideal time allocation with actual usage
- Calculate alignment score (0-100)
- Generate actionable insights from comparisons
