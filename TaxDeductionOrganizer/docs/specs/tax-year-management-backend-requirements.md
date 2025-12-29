# Tax Year Management - Backend Requirements
## Domain Model
**TaxYear Aggregate**: TaxYearId, Year, StartDate, EndDate, FilingDeadline, Status, TotalDeductions, IsClosed, UserId
## Commands
- CreateTaxYearCommand: Raises **TaxYearCreated**
- CloseTaxYearCommand: Raises **TaxYearClosed**
- CheckFilingDeadlineCommand: Raises **FilingDeadlineApproaching**
## Queries
- GetActiveTaxYearQuery, GetTaxYearHistoryQuery, GetTaxYearSummaryQuery
## API Endpoints
- POST /api/tax-years, POST /api/tax-years/{id}/close, GET /api/tax-years/{year}
