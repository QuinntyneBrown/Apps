# Category Organization - Backend Requirements
## Domain Model
**CategoryTotal Aggregate**: CategoryId, CategoryName, TaxYear, TotalAmount, DeductionCount, ThresholdAmount, UserId
## Commands
- CalculateCategoryTotalCommand: Raises **CategoryTotalCalculated**
- CheckCategoryLimitCommand: Raises **CategoryLimitApproached**
- DetectUnusualPatternCommand: Raises **UnusualDeductionPatternDetected**
## Queries
- GetCategoryTotalsQuery, GetCategoriesByScheduleQuery, GetAuditRiskQuery
## API Endpoints
- GET /api/categories/totals/{year}, GET /api/categories/audit-risk
