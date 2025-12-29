# Reporting and Export - Backend Requirements
## Domain Model
**TaxReport Aggregate**: ReportId, TaxYear, Format, TotalDeductionsByCategory, GenerationTimestamp, UserId
**DataExport Aggregate**: ExportId, DestinationSoftware, TaxYear, DeductionCount, ExportFormat, ExportDate, UserId
## Commands
- GenerateTaxReportCommand: Raises **TaxReportGenerated**
- ExportToTaxSoftwareCommand: Raises **DataExportedToTaxSoftware**
## Queries
- GetTaxReportQuery, GetExportHistoryQuery, GetDeductionSummaryQuery
## API Endpoints
- POST /api/reports/generate, POST /api/exports/tax-software, GET /api/reports/{id}/pdf
