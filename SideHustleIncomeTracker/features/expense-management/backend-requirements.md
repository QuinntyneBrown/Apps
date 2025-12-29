# Expense Management - Backend Requirements

## Domain Model
### BusinessExpense Aggregate
- ExpenseId, StreamId, UserId, Amount, Category, Vendor, ExpenseDate, ReceiptAttached, TaxDeductible, Description, Tags, CreatedAt

### MileageLog Aggregate
- MileageId, StreamId, UserId, Miles, Purpose, StartLocation, EndLocation, Date, IRSRate, DeductionAmount, CreatedAt

## Commands
- RecordBusinessExpenseCommand: Creates expense, raises **BusinessExpenseRecorded** event
- LogMileageCommand: Records mileage, calculates deduction, raises **MileageLogged** event
- AttachReceiptCommand: Uploads receipt to cloud storage
- UpdateExpenseCategoryCommand: Recategorizes expense
- CheckBudgetExceededCommand: Monitors spending, raises **ExpenseCategoryExceeded** if over budget

## Queries
- GetExpensesByStreamQuery, GetExpensesByCategoryQuery, GetExpenseStatisticsQuery, GetMileageLogsQuery

## API Endpoints
- POST /api/expenses (record expense)
- POST /api/expenses/mileage (log mileage)
- POST /api/expenses/{id}/receipt (upload receipt)
- GET /api/expenses/stream/{id} (get expenses)
- GET /api/expenses/statistics (get stats)

## Events
- **BusinessExpenseRecorded**: Triggers P&L update, tax deduction tracking
- **MileageLogged**: Triggers deduction calculation
- **ExpenseCategoryExceeded**: Sends budget alert
