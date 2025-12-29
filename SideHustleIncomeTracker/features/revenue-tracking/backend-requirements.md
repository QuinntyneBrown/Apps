# Revenue Tracking - Backend Requirements

## Overview
The Revenue Tracking feature handles all operations related to recording income, managing recurring revenue, tracking payment status, and monitoring overdue payments.

## Domain Model

### Income Aggregate
- **IncomeId**: Unique identifier (Guid)
- **StreamId**: Associated income stream (Guid)
- **UserId**: Owner (Guid)
- **Amount**: Payment amount (decimal, precision 18,2)
- **ClientName**: Client or source of payment (string, max 200 chars)
- **PaymentDate**: Date payment received (DateTime)
- **PaymentMethod**: How payment was received (enum: Cash, Check, BankTransfer, CreditCard, PayPal, Venmo, Stripe, Other)
- **InvoiceId**: Associated invoice if applicable (Guid, nullable)
- **Description**: Payment description (string, max 500 chars)
- **IsRecurring**: Whether part of recurring schedule (bool)
- **RecurringScheduleId**: If recurring, reference to schedule (Guid, nullable)
- **Tags**: Optional tags (List<string>)
- **CreatedAt**: Creation timestamp (DateTime)

### RecurringIncomeSchedule Aggregate
- **ScheduleId**: Unique identifier (Guid)
- **StreamId**: Associated income stream (Guid)
- **UserId**: Owner (Guid)
- **Amount**: Expected recurring amount (decimal, precision 18,2)
- **Frequency**: How often (enum: Weekly, BiWeekly, Monthly, Quarterly, Annually)
- **StartDate**: When schedule begins (DateTime)
- **EndDate**: When schedule ends (DateTime, nullable)
- **ClientId**: Client for recurring income (Guid, nullable)
- **NextExpectedDate**: Next payment due date (DateTime)
- **IsActive**: Whether schedule is active (bool)
- **CreatedAt**: Creation timestamp (DateTime)

## Commands

### RecordIncomeCommand
- Validates required fields (Amount, StreamId, PaymentDate, PaymentMethod)
- Ensures Amount > 0
- Validates StreamId exists and is active
- Creates Income aggregate
- Raises **IncomeReceived** domain event
- If amount > 3x average, raises **LargePaymentReceived** event
- Returns IncomeId

### ScheduleRecurringIncomeCommand
- Validates required fields (Amount, StreamId, Frequency, StartDate)
- Ensures Amount > 0
- Validates StreamId exists
- Creates RecurringIncomeSchedule
- Calculates NextExpectedDate based on frequency
- Raises **RecurringIncomeScheduled** domain event
- Returns ScheduleId

### CancelRecurringIncomeCommand
- Validates ScheduleId exists
- Validates ownership
- Sets IsActive = false
- Sets EndDate = current date
- Returns success indicator

### CheckOverduePaymentsCommand
- Queries all active recurring schedules
- Checks for payments where NextExpectedDate < today
- For each overdue, raises **PaymentOverdue** domain event
- Returns list of overdue schedules

## Queries

### GetIncomeByStreamQuery
- Returns all income for a specific stream
- Includes filtering by date range
- Supports pagination

### GetRecentIncomeQuery
- Returns most recent income across all streams
- Limited to last N records
- Sorted by PaymentDate descending

### GetRecurringSchedulesQuery
- Returns all active recurring income schedules
- Includes next expected payment dates
- Sorted by NextExpectedDate

### GetIncomeStatisticsQuery
- Returns aggregated statistics
- Total income, average payment, payment count
- Breakdown by payment method
- Monthly income trends

## Domain Events

### IncomeReceived
**Published When**: Payment is recorded
**Event Data**: IncomeId, StreamId, Amount, ClientName, PaymentDate, PaymentMethod, UserId, Timestamp
**Subscribers**: Revenue tracker, P&L calculator, Cash flow analyzer, Tax estimator

### RecurringIncomeScheduled
**Published When**: Recurring revenue is configured
**Event Data**: ScheduleId, StreamId, Amount, Frequency, StartDate, ClientId, UserId, Timestamp
**Subscribers**: Income forecaster, Cash flow projector, Reminder service

### PaymentOverdue
**Published When**: Expected payment not received by due date
**Event Data**: InvoiceId, ClientId, AmountDue, DueDate, DaysOverdue, UserId, Timestamp
**Subscribers**: Alert service, Collection reminder, Client follow-up system

### LargePaymentReceived
**Published When**: Payment exceeds 3x average
**Event Data**: IncomeId, Amount, ClientName, AverageComparison, Timestamp
**Subscribers**: Achievement service, Tax planning advisor, Cash management alert

## API Endpoints

### POST /api/income
Creates new income record
- **Request Body**: RecordIncomeCommand
- **Response**: 201 Created with IncomeId

### GET /api/income/stream/{streamId}
Retrieves income for specific stream
- **Query Parameters**: startDate, endDate, page, pageSize
- **Response**: 200 OK with income list

### GET /api/income/recent
Retrieves recent income across all streams
- **Query Parameters**: limit
- **Response**: 200 OK with income list

### POST /api/income/recurring
Creates recurring income schedule
- **Request Body**: ScheduleRecurringIncomeCommand
- **Response**: 201 Created with ScheduleId

### GET /api/income/recurring
Retrieves all recurring schedules
- **Response**: 200 OK with schedules

### DELETE /api/income/recurring/{id}
Cancels recurring income schedule
- **Response**: 200 OK

### GET /api/income/statistics
Retrieves income statistics
- **Query Parameters**: streamId (optional), startDate, endDate
- **Response**: 200 OK with statistics

### POST /api/income/check-overdue
Checks for overdue payments (admin/scheduled job)
- **Response**: 200 OK with overdue count

## Validation Rules
- Amount must be > 0
- PaymentDate cannot be in future
- StreamId must reference active income stream
- Recurring schedule StartDate cannot be in past
- User can only access their own income records

## Performance Considerations
- Index on StreamId and PaymentDate for fast queries
- Index on UserId and PaymentDate for user summaries
- Cache income statistics for dashboard
- Background job to check overdue payments daily
