# Payments - Backend Requirements

## Overview
The Payments feature handles all operations related to scheduling, executing, canceling, and managing bill payments in the system. This feature integrates with payment gateways to process actual financial transactions.

## Domain Model

### Payment Aggregate
- **PaymentId**: Unique identifier (Guid)
- **BillId**: Associated bill (Guid)
- **UserId**: Owner of the payment (Guid)
- **Amount**: Payment amount (decimal, precision 18,2)
- **ScheduledDate**: Date payment is scheduled (DateTime)
- **ExecutedDate**: Date payment was actually executed (DateTime, nullable)
- **Status**: Payment status (enum: Scheduled, InProgress, Completed, Cancelled, Failed)
- **PaymentMethod**: Method of payment (enum: BankTransfer, CreditCard, DebitCard, PayPal, Other)
- **PaymentMethodId**: Reference to stored payment method (Guid)
- **TransactionId**: External payment gateway transaction ID (string)
- **ConfirmationNumber**: Payment confirmation number (string)
- **Notes**: Optional payment notes (string, max 500 chars)
- **FailureReason**: Reason for payment failure (string, nullable)
- **RetryCount**: Number of retry attempts (int)
- **MaxRetries**: Maximum retry attempts allowed (int, default 3)
- **IsRecurring**: Whether payment is part of recurring schedule (bool)
- **RecurringPaymentId**: Parent recurring payment ID (Guid, nullable)
- **CreatedAt**: Payment creation timestamp (DateTime)
- **UpdatedAt**: Last modification timestamp (DateTime)
- **CreatedBy**: User who created the payment (Guid)
- **UpdatedBy**: User who last updated the payment (Guid)

### PaymentMethod Entity
- **PaymentMethodId**: Unique identifier (Guid)
- **UserId**: Owner of the payment method (Guid)
- **Type**: Payment method type (enum)
- **DisplayName**: User-friendly name (string)
- **LastFourDigits**: Last 4 digits of account/card (string)
- **ExpiryDate**: Card expiry date (DateTime, nullable)
- **BankName**: Name of bank (string, nullable)
- **IsDefault**: Default payment method flag (bool)
- **IsActive**: Active status (bool)
- **CreatedAt**: Creation timestamp (DateTime)

## Commands

### SchedulePaymentCommand
- Validates BillId exists and user has permission
- Validates PaymentMethodId exists and is active
- Validates Amount > 0
- Validates ScheduledDate is not in the past
- Ensures scheduled date is appropriate for payment method
- Creates Payment aggregate with Status = Scheduled
- Raises **PaymentScheduled** domain event
- Returns PaymentId

### ExecutePaymentCommand
- Validates PaymentId exists
- Validates Payment.Status == Scheduled or Failed
- Validates payment method is still valid
- Updates Status to InProgress
- Calls payment gateway API
- On success:
  - Updates Status to Completed
  - Sets ExecutedDate
  - Sets TransactionId and ConfirmationNumber
  - Raises **PaymentExecuted** domain event
- On failure:
  - Updates Status to Failed
  - Sets FailureReason
  - Increments RetryCount
  - Raises **PaymentFailed** domain event
  - Schedules retry if RetryCount < MaxRetries
- Returns execution result

### CancelPaymentCommand
- Validates PaymentId exists
- Validates Payment.Status == Scheduled (cannot cancel completed/in-progress)
- Validates user has permission
- Updates Status to Cancelled
- Raises **PaymentCancelled** domain event
- Returns success indicator

### RetryFailedPaymentCommand
- Validates PaymentId exists
- Validates Payment.Status == Failed
- Validates RetryCount < MaxRetries
- Resets Status to Scheduled
- Increments RetryCount
- Schedules new execution attempt
- Returns success indicator

### UpdatePaymentMethodCommand
- Validates PaymentId exists
- Validates Payment.Status == Scheduled (only scheduled payments can be modified)
- Validates new PaymentMethodId exists and is active
- Updates PaymentMethodId
- Returns success indicator

### UpdateScheduledDateCommand
- Validates PaymentId exists
- Validates Payment.Status == Scheduled
- Validates new ScheduledDate is not in the past
- Updates ScheduledDate
- Returns success indicator

## Queries

### GetPaymentByIdQuery
- Returns Payment details by PaymentId
- Includes related Bill and PaymentMethod information
- Returns null if not found

### GetPaymentsByUserIdQuery
- Returns all payments for a specific user
- Supports filtering by:
  - Status
  - Date range
  - BillId
  - PaymentMethod
- Supports sorting by:
  - ScheduledDate (ascending/descending)
  - ExecutedDate
  - Amount
  - Status
- Supports pagination
- Returns list of PaymentDto

### GetScheduledPaymentsQuery
- Returns all scheduled payments
- Optionally filter by date range
- Sorted by ScheduledDate ascending
- Returns list of PaymentDto

### GetPaymentHistoryQuery
- Returns completed and failed payments for a user
- Supports date range filtering
- Sorted by ExecutedDate descending
- Includes transaction details
- Returns list of PaymentDto

### GetFailedPaymentsQuery
- Returns all failed payments
- Optionally filter by user
- Includes failure reasons
- Sorted by UpdatedAt descending
- Returns list of PaymentDto with failure details

### GetPaymentsByBillIdQuery
- Returns all payments for a specific bill
- Includes payment history
- Sorted by ScheduledDate descending
- Returns list of PaymentDto

### GetUpcomingPaymentsQuery
- Returns payments scheduled in next N days (default 7)
- Only includes Status = Scheduled
- Sorted by ScheduledDate ascending
- Returns list of PaymentDto

## Domain Events

### PaymentScheduled
```csharp
public class PaymentScheduled : DomainEvent
{
    public Guid PaymentId { get; set; }
    public Guid BillId { get; set; }
    public Guid UserId { get; set; }
    public decimal Amount { get; set; }
    public DateTime ScheduledDate { get; set; }
    public string PaymentMethod { get; set; }
    public bool IsRecurring { get; set; }
    public DateTime OccurredAt { get; set; }
}
```

### PaymentExecuted
```csharp
public class PaymentExecuted : DomainEvent
{
    public Guid PaymentId { get; set; }
    public Guid BillId { get; set; }
    public Guid UserId { get; set; }
    public decimal Amount { get; set; }
    public DateTime ExecutedDate { get; set; }
    public string TransactionId { get; set; }
    public string ConfirmationNumber { get; set; }
    public string PaymentMethod { get; set; }
    public DateTime OccurredAt { get; set; }
}
```

### PaymentCancelled
```csharp
public class PaymentCancelled : DomainEvent
{
    public Guid PaymentId { get; set; }
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
    public Guid PaymentId { get; set; }
    public Guid BillId { get; set; }
    public Guid UserId { get; set; }
    public decimal Amount { get; set; }
    public DateTime AttemptedDate { get; set; }
    public string FailureReason { get; set; }
    public int RetryCount { get; set; }
    public bool WillRetry { get; set; }
    public DateTime? NextRetryDate { get; set; }
    public DateTime OccurredAt { get; set; }
}
```

## API Endpoints

### POST /api/payments/schedule
- Schedules a new payment
- Request body: SchedulePaymentCommand
- Returns: 201 Created with PaymentId
- Authorization: Authenticated user, bill owner

### POST /api/payments/{paymentId}/execute
- Manually triggers payment execution
- Returns: 200 OK with execution result
- Authorization: Payment owner

### POST /api/payments/{paymentId}/cancel
- Cancels a scheduled payment
- Request body: { reason: string }
- Returns: 200 OK
- Authorization: Payment owner

### POST /api/payments/{paymentId}/retry
- Retries a failed payment
- Returns: 200 OK
- Authorization: Payment owner

### PUT /api/payments/{paymentId}/payment-method
- Updates payment method for scheduled payment
- Request body: { paymentMethodId: Guid }
- Returns: 200 OK
- Authorization: Payment owner

### PUT /api/payments/{paymentId}/scheduled-date
- Updates scheduled date
- Request body: { scheduledDate: DateTime }
- Returns: 200 OK
- Authorization: Payment owner

### GET /api/payments/{paymentId}
- Retrieves payment details
- Returns: 200 OK with PaymentDto
- Authorization: Payment owner

### GET /api/payments
- Retrieves all payments for current user
- Query params: status, dateFrom, dateTo, billId, page, pageSize
- Returns: 200 OK with paginated PaymentDto list
- Authorization: Authenticated user

### GET /api/payments/scheduled
- Retrieves scheduled payments
- Query params: dateFrom, dateTo, page, pageSize
- Returns: 200 OK with PaymentDto list
- Authorization: Authenticated user

### GET /api/payments/history
- Retrieves payment history
- Query params: dateFrom, dateTo, page, pageSize
- Returns: 200 OK with paginated PaymentDto list
- Authorization: Authenticated user

### GET /api/payments/failed
- Retrieves failed payments
- Returns: 200 OK with PaymentDto list
- Authorization: Authenticated user

### GET /api/payments/upcoming
- Retrieves upcoming payments (next 7 days)
- Query params: days (default 7)
- Returns: 200 OK with PaymentDto list
- Authorization: Authenticated user

## Business Rules

1. **Payment Amount**: Must match or be less than Bill amount
2. **Scheduled Date**: Cannot be more than 1 year in the future
3. **Payment Method**: Must be active and not expired
4. **Cancellation**: Only scheduled payments can be cancelled
5. **Execution**: Payments execute automatically at scheduled time
6. **Retry Logic**: Failed payments retry up to 3 times with exponential backoff
7. **User Isolation**: Users can only access their own payments
8. **Bill Association**: Payment must be associated with an existing bill
9. **Duplicate Prevention**: Cannot schedule duplicate payment for same bill on same date
10. **Balance Check**: Optional balance verification before execution

## Data Persistence

### Payments Table
```sql
CREATE TABLE Payments (
    PaymentId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    BillId UNIQUEIDENTIFIER NOT NULL,
    UserId UNIQUEIDENTIFIER NOT NULL,
    Amount DECIMAL(18,2) NOT NULL,
    ScheduledDate DATETIME2 NOT NULL,
    ExecutedDate DATETIME2 NULL,
    Status NVARCHAR(50) NOT NULL,
    PaymentMethod NVARCHAR(50) NOT NULL,
    PaymentMethodId UNIQUEIDENTIFIER NOT NULL,
    TransactionId NVARCHAR(200) NULL,
    ConfirmationNumber NVARCHAR(200) NULL,
    Notes NVARCHAR(500) NULL,
    FailureReason NVARCHAR(1000) NULL,
    RetryCount INT NOT NULL DEFAULT 0,
    MaxRetries INT NOT NULL DEFAULT 3,
    IsRecurring BIT NOT NULL DEFAULT 0,
    RecurringPaymentId UNIQUEIDENTIFIER NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    CreatedBy UNIQUEIDENTIFIER NOT NULL,
    UpdatedBy UNIQUEIDENTIFIER NOT NULL,

    CONSTRAINT FK_Payments_Bills FOREIGN KEY (BillId) REFERENCES Bills(BillId),
    CONSTRAINT FK_Payments_Users FOREIGN KEY (UserId) REFERENCES Users(UserId),
    CONSTRAINT FK_Payments_PaymentMethods FOREIGN KEY (PaymentMethodId) REFERENCES PaymentMethods(PaymentMethodId),
    CONSTRAINT CK_Payments_Amount CHECK (Amount > 0),
    INDEX IX_Payments_UserId (UserId),
    INDEX IX_Payments_BillId (BillId),
    INDEX IX_Payments_ScheduledDate (ScheduledDate),
    INDEX IX_Payments_Status (Status),
    INDEX IX_Payments_ExecutedDate (ExecutedDate)
);
```

### PaymentMethods Table
```sql
CREATE TABLE PaymentMethods (
    PaymentMethodId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    UserId UNIQUEIDENTIFIER NOT NULL,
    Type NVARCHAR(50) NOT NULL,
    DisplayName NVARCHAR(100) NOT NULL,
    LastFourDigits NVARCHAR(4) NULL,
    ExpiryDate DATETIME2 NULL,
    BankName NVARCHAR(100) NULL,
    IsDefault BIT NOT NULL DEFAULT 0,
    IsActive BIT NOT NULL DEFAULT 1,
    EncryptedDetails NVARCHAR(MAX) NULL, -- Encrypted sensitive data
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),

    CONSTRAINT FK_PaymentMethods_Users FOREIGN KEY (UserId) REFERENCES Users(UserId),
    INDEX IX_PaymentMethods_UserId (UserId)
);
```

## Error Handling

### Validation Errors (400 Bad Request)
- Missing required fields
- Invalid amount (<=0 or exceeds limits)
- Invalid date (past date for scheduling)
- Invalid payment method
- Duplicate payment detection

### Authorization Errors (403 Forbidden)
- User attempting to access another user's payment
- User attempting to modify/cancel completed payment

### Not Found Errors (404 Not Found)
- PaymentId does not exist
- BillId does not exist
- PaymentMethodId does not exist

### Conflict Errors (409 Conflict)
- Attempting to cancel in-progress payment
- Attempting to modify executed payment
- Duplicate payment for same bill and date

### Payment Errors (422 Unprocessable Entity)
- Insufficient funds
- Payment method expired
- Payment method declined
- Payment gateway unavailable

### Server Errors (500 Internal Server Error)
- Payment gateway communication failure
- Database connection failures
- Unexpected exceptions

## Integration Points

### Payment Gateway Integration
- **Stripe API**: Credit/debit card processing
- **Plaid API**: Bank account verification and ACH transfers
- **PayPal API**: PayPal payment processing
- **Encryption**: PCI-compliant encryption for payment data
- **Webhooks**: Handle payment gateway callbacks

### Event Handlers
- **PaymentScheduled**: Create payment reminder, update cash flow
- **PaymentExecuted**: Update bill status, send confirmation, trigger receipt generation
- **PaymentCancelled**: Cancel reminders, update cash flow
- **PaymentFailed**: Send failure notification, schedule retry, alert user

### Background Jobs
- **Payment Executor**: Runs every minute to execute scheduled payments
- **Retry Processor**: Handles failed payment retries with exponential backoff
- **Reconciliation Job**: Daily reconciliation with payment gateway
- **Cleanup Job**: Archive old completed payments (older than 7 years)

## Performance Considerations

- Index on UserId and ScheduledDate for efficient payment queries
- Index on Status for filtering scheduled/failed payments
- Implement payment execution queue for high volume
- Use database transactions for payment state changes
- Cache active payment methods for quick lookup
- Implement idempotency keys for payment execution

## Security Considerations

- PCI DSS compliance for payment data storage
- Encrypt payment method details at rest and in transit
- Never log full payment card numbers
- Implement strong customer authentication (SCA) for EU
- Use tokenization for stored payment methods
- Implement fraud detection and velocity checks
- Require re-authentication for high-value payments
- Audit log all payment operations
- Implement rate limiting on payment endpoints

## Testing Requirements

### Unit Tests
- Command validation logic
- Payment state transitions
- Retry logic and exponential backoff
- Domain event creation
- Business rule enforcement

### Integration Tests
- Payment gateway integration (use sandbox)
- Database operations and transactions
- Event publishing and handling
- API endpoint functionality
- Authorization checks

### E2E Tests
- Complete payment flow: schedule → execute → confirm
- Payment failure and retry flow
- Payment cancellation flow
- Multiple payment methods handling

### Performance Tests
- Concurrent payment execution (1000+ simultaneous)
- Payment queue processing speed
- Large payment history queries
- Payment gateway response time handling

## Monitoring and Alerting

### Metrics to Track
- Payment success rate (target: >99%)
- Payment execution latency
- Failed payment rate and reasons
- Payment gateway response times
- Retry success rate

### Alerts
- Payment failure rate >1%
- Payment gateway unavailable
- High retry count for single payment
- Unusual payment patterns (fraud detection)
- Payment execution delays

## Compliance Requirements

- **PCI DSS**: Payment card industry compliance
- **PSD2**: Strong customer authentication for EU
- **SOX**: Financial audit trail requirements
- **GDPR**: Payment data privacy and retention
- **Data Retention**: 7 years for payment records
