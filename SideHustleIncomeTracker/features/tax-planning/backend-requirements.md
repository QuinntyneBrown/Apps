# Tax Planning - Backend Requirements

## Domain Model
### QuarterlyTaxEstimate Aggregate
- EstimateId, UserId, Quarter, Year, TotalIncome, TotalExpenses, EstimatedTaxOwed, PaymentDueDate, Status

### TaxPayment Aggregate
- PaymentId, EstimateId, UserId, Quarter, Amount, PaymentDate, PaymentMethod, ConfirmationNumber

### DeductionOpportunity Aggregate
- OpportunityId, UserId, OpportunityType, EstimatedValue, ExpenseCategory, Status

## Commands
- EstimateQuarterlyTaxCommand: Calculates tax, raises **QuarterlyTaxEstimated** event
- ScheduleTaxPaymentCommand: Schedules payment, raises **TaxPaymentScheduled** event
- AnalyzeDeductionsCommand: Identifies opportunities, raises **DeductionOpportunityIdentified** event

## Queries
- GetTaxEstimatesQuery, GetDeductionSummaryQuery, GetTaxableIncomeQuery, GetQuarterlyObligationsQuery

## API Endpoints
- POST /api/tax/estimate (calculate quarterly)
- POST /api/tax/payment (schedule payment)
- GET /api/tax/deductions (get deductibles)
- GET /api/tax/summary/{year} (annual summary)

## Events
- **QuarterlyTaxEstimated**: Sends payment reminder
- **TaxPaymentScheduled**: Adds to calendar
- **DeductionOpportunityIdentified**: Alerts user to save money
