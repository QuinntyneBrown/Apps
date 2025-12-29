# Payment Scheduling - Backend Requirements

## Overview
The Payment Scheduling feature enables users to schedule payments for their bills, either as one-time or recurring payments, and track payment execution status.

## Domain Model

### PaymentSchedule Aggregate
- **PaymentScheduleId**: Unique identifier (Guid)
- **BillId**: Associated bill (Guid)
- **UserId**: Owner of the payment schedule (Guid)
- **PaymentMethodId**: Payment method to use (Guid)
- **Amount**: Payment amount (decimal, precision 18,2)
- **ScheduledDate**: Date payment should be executed (DateTime)
- **IsRecurring**: Whether this is a recurring payment (bool)
- **RecurrencePattern**: How often to repeat (enum: None, Weekly, BiWeekly, Monthly, Quarterly, Annually)
- **Status**: Payment status (enum: Scheduled, InProgress, Completed, Failed, Cancelled)
- **ConfirmationNumber**: Payment confirmation/reference number (string, max 100 chars)
- **Notes**: Optional payment notes (string, max 500 chars)
- **CreatedAt**: Creation timestamp (DateTime)
- **UpdatedAt**: Last modification timestamp (DateTime)
- **ExecutedAt**: When payment was executed (DateTime, nullable)
- **CancelledAt**: When payment was cancelled (DateTime, nullable)
- **FailureReason**: Reason for payment failure (string, max 500 chars, nullable)

### PaymentMethod Entity
- **PaymentMethodId**: Unique identifier (Guid)
- **UserId**: Owner (Guid)
- **Type**: Payment type (enum: BankAccount, CreditCard, DebitCard, PayPal, Other)
- **DisplayName**: User-friendly name (string, max 100 chars)
- **LastFourDigits**: Last 4 digits for display (string, 4 chars)
- **ExpirationDate**: Card expiration (DateTime, nullable)
- **IsDefault**: Default payment method flag (bool)
- **IsActive**: Active status (bool)

## Commands

### SchedulePaymentCommand
- Validates BillId exists and user has access
- Validates PaymentMethodId exists and user has access
- Ensures ScheduledDate is not in the past
- Validates Amount > 0
- Creates PaymentSchedule aggregate
- Raises **PaymentScheduled** domain event
- Returns PaymentScheduleId

### ExecutePaymentCommand
- Validates PaymentScheduleId exists
- Checks payment hasn't already been executed
- Validates ScheduledDate has arrived
- Processes payment through payment gateway
- Updates Status to InProgress, then Completed or Failed
- Raises **PaymentExecuted** or **PaymentFailed** domain event
- Returns execution result

### CancelPaymentCommand
- Validates PaymentScheduleId exists
- Checks payment hasn't been executed
- Validates user has permission
- Updates Status to Cancelled
- Sets CancelledAt timestamp
- Raises **PaymentCancelled** domain event
- Returns success indicator

### ModifyPaymentScheduleCommand
- Validates PaymentScheduleId exists
- Validates new scheduled date and amount
- Checks payment hasn't been executed
- Updates PaymentSchedule properties
- Returns success indicator

### RetryFailedPaymentCommand
- Validates PaymentScheduleId exists
- Checks Status is Failed
- Resets status and attempts payment again
- Raises appropriate domain events
- Returns retry result

## Queries

### GetPaymentScheduleByIdQuery
- Returns PaymentSchedule details by PaymentScheduleId
- Includes associated Bill and PaymentMethod info
- Returns null if not found

### GetPaymentSchedulesByBillIdQuery
- Returns all payment schedules for a specific bill
- Includes historical and upcoming payments
- Supports filtering by status
- Sorted by ScheduledDate
- Returns list of PaymentScheduleDto

### GetPaymentSchedulesByUserIdQuery
- Returns all payment schedules for a user
- Supports filtering by:
  - Status (Scheduled, Completed, Failed, Cancelled)
  - Date range
  - BillId
- Supports sorting by ScheduledDate, Amount, Status
- Supports pagination
- Returns paginated list of PaymentScheduleDto

### GetUpcomingPaymentsQuery
- Returns payments scheduled within specified date range
- Default: next 30 days
- Only includes Scheduled status
- Sorted by ScheduledDate ascending
- Returns list of PaymentScheduleDto

### GetPaymentHistoryQuery
- Returns completed and failed payments
- Supports date range filtering
- Includes payment details and outcomes
- Sorted by ExecutedAt descending
- Returns paginated list of PaymentScheduleDto

### GetPaymentMethodsQuery
- Returns all payment methods for a user
- Filters out inactive methods by default
- Returns list of PaymentMethodDto

## Domain Events

### PaymentScheduled
```csharp
public class PaymentScheduled : DomainEvent
{
    public Guid PaymentScheduleId { get; set; }
    public Guid BillId { get; set; }
    public Guid UserId { get; set; }
    public decimal Amount { get; set; }
    public DateTime ScheduledDate { get; set; }
    public bool IsRecurring { get; set; }
    public DateTime OccurredAt { get; set; }
}
```

### PaymentExecuted
```csharp
public class PaymentExecuted : DomainEvent
{
    public Guid PaymentScheduleId { get; set; }
    public Guid BillId { get; set; }
    public Guid UserId { get; set; }
    public decimal Amount { get; set; }
    public DateTime ExecutedAt { get; set; }
    public string ConfirmationNumber { get; set; }
    public DateTime OccurredAt { get; set; }
}
```

### PaymentCancelled
```csharp
public class PaymentCancelled : DomainEvent
{
    public Guid PaymentScheduleId { get; set; }
    public Guid BillId { get; set; }
    public Guid UserId { get; set; }
    public decimal Amount { get; set; }
    public DateTime ScheduledDate { get; set; }
    public string CancellationReason { get; set; }
    public DateTime OccurredAt { get; set; }
}
```

### PaymentFailed
```csharp
public class PaymentFailed : DomainEvent
{
    public Guid PaymentScheduleId { get; set; }
    public Guid BillId { get; set; }
    public Guid UserId { get; set; }
    public decimal Amount { get; set; }
    public string FailureReason { get; set; }
    public DateTime OccurredAt { get; set; }
}
```

## API Endpoints

### POST /api/payments/schedule
- Creates a new payment schedule
- Request body: SchedulePaymentCommand
- Returns: 201 Created with PaymentScheduleId
- Authorization: Authenticated user

### POST /api/payments/{paymentScheduleId}/execute
- Executes a scheduled payment immediately
- Returns: 200 OK with execution result
- Authorization: Payment owner

### POST /api/payments/{paymentScheduleId}/cancel
- Cancels a scheduled payment
- Request body: { reason: string }
- Returns: 200 OK
- Authorization: Payment owner

### PUT /api/payments/{paymentScheduleId}
- Modifies a scheduled payment
- Request body: ModifyPaymentScheduleCommand
- Returns: 200 OK
- Authorization: Payment owner

### POST /api/payments/{paymentScheduleId}/retry
- Retries a failed payment
- Returns: 200 OK with retry result
- Authorization: Payment owner

### GET /api/payments/{paymentScheduleId}
- Retrieves payment schedule details
- Returns: 200 OK with PaymentScheduleDto
- Authorization: Payment owner

### GET /api/payments/bill/{billId}
- Retrieves all payments for a specific bill
- Query params: status, page, pageSize
- Returns: 200 OK with paginated PaymentScheduleDto list
- Authorization: Bill owner

### GET /api/payments/upcoming
- Retrieves upcoming scheduled payments
- Query params: days (default 30)
- Returns: 200 OK with PaymentScheduleDto list
- Authorization: Authenticated user

### GET /api/payments/history
- Retrieves payment history
- Query params: startDate, endDate, page, pageSize
- Returns: 200 OK with paginated PaymentScheduleDto list
- Authorization: Authenticated user

### GET /api/payment-methods
- Retrieves user's payment methods
- Returns: 200 OK with PaymentMethodDto list
- Authorization: Authenticated user

### POST /api/payment-methods
- Adds a new payment method
- Request body: AddPaymentMethodCommand
- Returns: 201 Created with PaymentMethodId
- Authorization: Authenticated user

### DELETE /api/payment-methods/{paymentMethodId}
- Removes a payment method
- Returns: 204 No Content
- Authorization: Payment method owner

## Business Rules

1. **Scheduled Date**: Must be at least 1 day in the future (configurable)
2. **Payment Amount**: Must match or be less than bill amount
3. **Payment Method**: Must be active and not expired
4. **Execution Window**: Payments executed within 24-hour window of scheduled date
5. **Retry Logic**: Failed payments can be retried up to 3 times
6. **Recurring Payments**: Auto-generate next occurrence after successful execution
7. **Cancellation**: Payments can only be cancelled if not yet executed
8. **Modification**: Scheduled payments can be modified up to 1 day before execution
9. **User Isolation**: Users can only access their own payment schedules
10. **Audit Trail**: All payment actions logged with user and timestamp

## Payment Gateway Integration

### IPaymentGateway Interface
```csharp
public interface IPaymentGateway
{
    Task<PaymentResult> ProcessPaymentAsync(PaymentRequest request);
    Task<PaymentStatus> CheckPaymentStatusAsync(string confirmationNumber);
    Task<bool> ValidatePaymentMethodAsync(PaymentMethod method);
    Task<RefundResult> RefundPaymentAsync(string confirmationNumber, decimal amount);
}
```

### Payment Flow
1. Validate payment schedule
2. Verify payment method
3. Call payment gateway
4. Handle response (success/failure)
5. Update payment status
6. Raise domain event
7. Send notification

## Data Persistence

### PaymentSchedules Table
```sql
CREATE TABLE PaymentSchedules (
    PaymentScheduleId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    BillId UNIQUEIDENTIFIER NOT NULL,
    UserId UNIQUEIDENTIFIER NOT NULL,
    PaymentMethodId UNIQUEIDENTIFIER NOT NULL,
    Amount DECIMAL(18,2) NOT NULL,
    ScheduledDate DATETIME2 NOT NULL,
    IsRecurring BIT NOT NULL DEFAULT 0,
    RecurrencePattern NVARCHAR(50) NOT NULL,
    Status NVARCHAR(50) NOT NULL,
    ConfirmationNumber NVARCHAR(100) NULL,
    Notes NVARCHAR(500) NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    ExecutedAt DATETIME2 NULL,
    CancelledAt DATETIME2 NULL,
    FailureReason NVARCHAR(500) NULL,

    CONSTRAINT FK_PaymentSchedules_Bills FOREIGN KEY (BillId) REFERENCES Bills(BillId),
    CONSTRAINT FK_PaymentSchedules_Users FOREIGN KEY (UserId) REFERENCES Users(UserId),
    CONSTRAINT FK_PaymentSchedules_PaymentMethods FOREIGN KEY (PaymentMethodId) REFERENCES PaymentMethods(PaymentMethodId),
    INDEX IX_PaymentSchedules_BillId (BillId),
    INDEX IX_PaymentSchedules_UserId (UserId),
    INDEX IX_PaymentSchedules_ScheduledDate (ScheduledDate),
    INDEX IX_PaymentSchedules_Status (Status)
);
```

### PaymentMethods Table
```sql
CREATE TABLE PaymentMethods (
    PaymentMethodId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    UserId UNIQUEIDENTIFIER NOT NULL,
    Type NVARCHAR(50) NOT NULL,
    DisplayName NVARCHAR(100) NOT NULL,
    LastFourDigits NCHAR(4) NOT NULL,
    ExpirationDate DATETIME2 NULL,
    IsDefault BIT NOT NULL DEFAULT 0,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    EncryptedDetails NVARCHAR(MAX) NOT NULL, -- Encrypted payment details

    CONSTRAINT FK_PaymentMethods_Users FOREIGN KEY (UserId) REFERENCES Users(UserId),
    INDEX IX_PaymentMethods_UserId (UserId),
    INDEX IX_PaymentMethods_IsDefault (IsDefault)
);
```

## Background Jobs

### PaymentExecutorJob
- **Schedule**: Runs every hour
- **Purpose**: Execute scheduled payments that are due
- **Logic**:
  1. Query payments with Status = Scheduled and ScheduledDate <= Now
  2. For each payment, execute through gateway
  3. Update status and raise events
  4. Handle failures with retry logic

### RecurringPaymentGeneratorJob
- **Schedule**: Runs daily
- **Purpose**: Generate next occurrence for recurring payments
- **Logic**:
  1. Query completed recurring payments
  2. Calculate next scheduled date based on recurrence pattern
  3. Create new payment schedule
  4. Raise PaymentScheduled event

### PaymentReminderJob
- **Schedule**: Runs every 6 hours
- **Purpose**: Send reminders for upcoming payments
- **Logic**:
  1. Query payments scheduled in next 3 days
  2. Check if reminder already sent
  3. Send reminder notification
  4. Update reminder sent flag

## Error Handling

### Validation Errors (400 Bad Request)
- Missing required fields
- Invalid payment amount
- Scheduled date in the past
- Invalid payment method

### Authorization Errors (403 Forbidden)
- User attempting to access another user's payment
- Payment method doesn't belong to user

### Not Found Errors (404 Not Found)
- PaymentScheduleId doesn't exist
- BillId doesn't exist
- PaymentMethodId doesn't exist

### Conflict Errors (409 Conflict)
- Payment already executed
- Payment already cancelled
- Concurrent modification

### Payment Gateway Errors (502 Bad Gateway)
- Gateway unavailable
- Gateway timeout
- Invalid response

### Server Errors (500 Internal Server Error)
- Database failures
- Unexpected exceptions

## Security Considerations

- Encrypt payment method details at rest
- Use tokenization for sensitive data
- PCI DSS compliance for payment processing
- Validate user authorization on all operations
- Implement rate limiting on payment operations
- Log all payment attempts for audit
- Secure communication with payment gateway (TLS 1.2+)
- Implement fraud detection and prevention

## Performance Considerations

- Index on ScheduledDate for executor job
- Index on UserId for user queries
- Cache payment methods
- Batch process scheduled payments
- Optimize payment gateway calls
- Implement circuit breaker for gateway failures
- Use async processing for payment execution

## Testing Requirements

### Unit Tests
- Domain model validation
- Business rule enforcement
- Command handlers
- Query handlers
- Domain event creation

### Integration Tests
- API endpoint functionality
- Database operations
- Payment gateway integration (mocked)
- Event publishing
- Authorization checks

### Performance Tests
- Concurrent payment execution
- Large batch processing
- Query performance with large datasets
- Gateway timeout handling
