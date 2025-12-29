# Donation Management - Backend Requirements

## Overview
The Donation Management feature enables users to record, track, and manage all types of charitable donations including one-time gifts, recurring donations, and non-cash contributions.

## Domain Model

### Donation Aggregate
- **DonationId**: Unique identifier (Guid)
- **UserId**: Owner of the donation record (Guid)
- **OrganizationId**: Recipient charity (Guid)
- **Amount**: Donation amount (decimal, precision 18,2)
- **DonationDate**: Date donation was made (DateTime)
- **PaymentMethod**: How donation was made (enum: Cash, Check, CreditCard, DebitCard, Stock, Property, Other)
- **IsTaxDeductible**: Whether donation qualifies for tax deduction (bool)
- **DonationType**: Type of donation (enum: Cash, NonCash, Stock, Property)
- **Category**: Donation category (enum: General, Education, Health, Environment, Religious, Arts, International, Other)
- **CheckNumber**: Check number if applicable (string, max 50 chars, nullable)
- **ConfirmationNumber**: Online payment confirmation (string, max 100 chars, nullable)
- **Notes**: Optional donation notes (string, max 1000 chars)
- **ReceiptUrl**: URL to receipt document (string, max 500 chars, nullable)
- **AcknowledgmentLetterUrl**: URL to acknowledgment letter (string, max 500 chars, nullable)
- **AcknowledgmentReceived**: Whether acknowledgment obtained (bool)
- **FairMarketValue**: Value for non-cash donations (decimal, nullable)
- **ValuationMethod**: How value was determined (string, max 200 chars, nullable)
- **AppraisalRequired**: Whether appraisal needed for IRS (bool)
- **CreatedAt**: Creation timestamp (DateTime)
- **UpdatedAt**: Last modification timestamp (DateTime)
- **RefundedAt**: Refund timestamp (DateTime, nullable)
- **RefundAmount**: Amount refunded (decimal, nullable)
- **RefundReason**: Reason for refund (string, max 500 chars, nullable)

### RecurringDonation Aggregate
- **RecurringDonationId**: Unique identifier (Guid)
- **UserId**: Owner (Guid)
- **OrganizationId**: Recipient charity (Guid)
- **Amount**: Recurring donation amount (decimal, precision 18,2)
- **Frequency**: How often donation recurs (enum: Weekly, BiWeekly, Monthly, Quarterly, Annually)
- **StartDate**: When recurring donations begin (DateTime)
- **EndDate**: When recurring donations end (DateTime, nullable)
- **NextScheduledDate**: Next execution date (DateTime)
- **PaymentMethod**: Payment method for recurring donation (enum)
- **IsActive**: Whether schedule is active (bool)
- **PausedUntil**: Temporary pause until date (DateTime, nullable)
- **DonationsMade**: Count of donations executed (int)
- **TotalDonated**: Sum of all executed donations (decimal)
- **CreatedAt**: Creation timestamp (DateTime)
- **UpdatedAt**: Last modification timestamp (DateTime)

## Commands

### RecordDonationCommand
- Validates OrganizationId exists and is verified
- Validates Amount > 0
- Ensures DonationDate is not in the future
- Creates Donation aggregate
- Raises **DonationMade** domain event
- Returns DonationId

### RecordNonCashDonationCommand
- Validates OrganizationId exists
- Validates FairMarketValue > 0
- Checks if appraisal required (value > $5,000)
- Creates Donation with non-cash details
- Raises **NonCashDonationValued** domain event
- Returns DonationId

### ScheduleRecurringDonationCommand
- Validates OrganizationId exists
- Validates Amount > 0
- Ensures StartDate is valid
- Calculates NextScheduledDate based on Frequency
- Creates RecurringDonation aggregate
- Raises **RecurringDonationScheduled** domain event
- Returns RecurringDonationId

### ExecuteRecurringDonationCommand
- Validates RecurringDonationId exists and is active
- Creates Donation from recurring schedule
- Updates NextScheduledDate
- Increments DonationsMade counter
- Updates TotalDonated amount
- Raises **RecurringDonationExecuted** domain event
- Returns DonationId

### RefundDonationCommand
- Validates DonationId exists
- Validates donation not already refunded
- Updates RefundedAt, RefundAmount, RefundReason
- Raises **DonationRefunded** domain event
- Returns success indicator

### UploadAcknowledgmentLetterCommand
- Validates DonationId exists
- Uploads document to storage
- Sets AcknowledgmentLetterUrl
- Marks AcknowledgmentReceived as true
- Raises **AcknowledgmentLetterReceived** domain event
- Returns document URL

### ModifyRecurringDonationCommand
- Validates RecurringDonationId exists
- Updates Amount, Frequency, or EndDate
- Recalculates NextScheduledDate if needed
- Returns success indicator

### PauseRecurringDonationCommand
- Validates RecurringDonationId exists
- Sets PausedUntil date
- Returns success indicator

### CancelRecurringDonationCommand
- Validates RecurringDonationId exists
- Sets IsActive to false
- Sets EndDate to current date
- Returns success indicator

## Queries

### GetDonationByIdQuery
- Returns Donation details by DonationId
- Includes associated Organization info
- Returns null if not found

### GetDonationsByUserIdQuery
- Returns all donations for a user
- Supports filtering by:
  - Date range
  - OrganizationId
  - PaymentMethod
  - IsTaxDeductible
  - Category
- Supports sorting by Date, Amount, Organization
- Supports pagination
- Returns paginated list of DonationDto

### GetDonationsByOrganizationQuery
- Returns all donations to specific organization
- Filtered by UserId for security
- Sorted by DonationDate descending
- Returns list of DonationDto

### GetDonationsByTaxYearQuery
- Returns all tax-deductible donations for a tax year
- Filtered by UserId
- Only includes IsTaxDeductible = true
- Calculates total deductible amount
- Returns DonationsByTaxYearDto

### GetRecurringDonationByIdQuery
- Returns RecurringDonation details
- Includes associated Organization
- Returns null if not found

### GetRecurringDonationsByUserIdQuery
- Returns all recurring donations for a user
- Supports filtering by IsActive status
- Sorted by NextScheduledDate
- Returns list of RecurringDonationDto

### GetMissingAcknowledgmentsQuery
- Returns donations > $250 without acknowledgment letters
- Filtered by UserId
- Sorted by DonationDate
- Returns list of DonationDto needing acknowledgment

### GetDonationSummaryQuery
- Returns summary statistics for user
- Includes total donated, donation count, organizations supported
- Groups by time period (monthly, quarterly, annual)
- Returns DonationSummaryDto

## Domain Events

### DonationMade
```csharp
public class DonationMade : DomainEvent
{
    public Guid DonationId { get; set; }
    public Guid OrganizationId { get; set; }
    public Guid UserId { get; set; }
    public decimal Amount { get; set; }
    public DateTime DonationDate { get; set; }
    public string PaymentMethod { get; set; }
    public bool IsTaxDeductible { get; set; }
    public DateTime OccurredAt { get; set; }
}
```

### RecurringDonationScheduled
```csharp
public class RecurringDonationScheduled : DomainEvent
{
    public Guid RecurringDonationId { get; set; }
    public Guid OrganizationId { get; set; }
    public Guid UserId { get; set; }
    public decimal Amount { get; set; }
    public string Frequency { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string PaymentMethod { get; set; }
    public DateTime OccurredAt { get; set; }
}
```

### RecurringDonationExecuted
```csharp
public class RecurringDonationExecuted : DomainEvent
{
    public Guid DonationId { get; set; }
    public Guid RecurringDonationId { get; set; }
    public Guid OrganizationId { get; set; }
    public Guid UserId { get; set; }
    public decimal Amount { get; set; }
    public DateTime ExecutionDate { get; set; }
    public DateTime NextScheduledDate { get; set; }
    public DateTime OccurredAt { get; set; }
}
```

### DonationRefunded
```csharp
public class DonationRefunded : DomainEvent
{
    public Guid DonationId { get; set; }
    public decimal OriginalAmount { get; set; }
    public decimal RefundAmount { get; set; }
    public DateTime RefundDate { get; set; }
    public string RefundReason { get; set; }
    public Guid UserId { get; set; }
    public DateTime OccurredAt { get; set; }
}
```

### NonCashDonationValued
```csharp
public class NonCashDonationValued : DomainEvent
{
    public Guid DonationId { get; set; }
    public string ItemDescription { get; set; }
    public int Quantity { get; set; }
    public decimal AssessedValue { get; set; }
    public string ValuationMethod { get; set; }
    public bool AppraisalRequired { get; set; }
    public Guid UserId { get; set; }
    public DateTime OccurredAt { get; set; }
}
```

### AcknowledgmentLetterReceived
```csharp
public class AcknowledgmentLetterReceived : DomainEvent
{
    public Guid DonationId { get; set; }
    public Guid OrganizationId { get; set; }
    public DateTime AcknowledgmentDate { get; set; }
    public string DocumentUrl { get; set; }
    public Guid UserId { get; set; }
    public DateTime OccurredAt { get; set; }
}
```

## API Endpoints

### POST /api/donations
- Records a new donation
- Request body: RecordDonationCommand
- Returns: 201 Created with DonationId
- Authorization: Authenticated user

### POST /api/donations/non-cash
- Records a non-cash donation
- Request body: RecordNonCashDonationCommand
- Returns: 201 Created with DonationId
- Authorization: Authenticated user

### POST /api/donations/recurring
- Schedules a recurring donation
- Request body: ScheduleRecurringDonationCommand
- Returns: 201 Created with RecurringDonationId
- Authorization: Authenticated user

### POST /api/donations/{donationId}/refund
- Records a donation refund
- Request body: RefundDonationCommand
- Returns: 200 OK
- Authorization: Donation owner

### POST /api/donations/{donationId}/acknowledgment
- Uploads acknowledgment letter
- Request: multipart/form-data with document
- Returns: 200 OK with document URL
- Authorization: Donation owner

### PUT /api/donations/recurring/{recurringDonationId}
- Modifies recurring donation schedule
- Request body: ModifyRecurringDonationCommand
- Returns: 200 OK
- Authorization: Recurring donation owner

### POST /api/donations/recurring/{recurringDonationId}/pause
- Pauses recurring donation
- Request body: { pausedUntil: DateTime }
- Returns: 200 OK
- Authorization: Recurring donation owner

### DELETE /api/donations/recurring/{recurringDonationId}
- Cancels recurring donation
- Returns: 204 No Content
- Authorization: Recurring donation owner

### GET /api/donations/{donationId}
- Retrieves donation details
- Returns: 200 OK with DonationDto
- Authorization: Donation owner

### GET /api/donations
- Lists user's donations
- Query params: startDate, endDate, organizationId, taxDeductible, page, pageSize
- Returns: 200 OK with paginated DonationDto list
- Authorization: Authenticated user

### GET /api/donations/tax-year/{year}
- Retrieves donations for tax year
- Returns: 200 OK with DonationsByTaxYearDto
- Authorization: Authenticated user

### GET /api/donations/recurring
- Lists user's recurring donations
- Query params: activeOnly, page, pageSize
- Returns: 200 OK with RecurringDonationDto list
- Authorization: Authenticated user

### GET /api/donations/missing-acknowledgments
- Lists donations needing acknowledgment letters
- Returns: 200 OK with DonationDto list
- Authorization: Authenticated user

### GET /api/donations/summary
- Gets donation summary statistics
- Query params: period (monthly/quarterly/annual)
- Returns: 200 OK with DonationSummaryDto
- Authorization: Authenticated user

## Business Rules

1. **Donation Date**: Cannot be in the future
2. **Tax Deductibility**: Only verified 501(c)(3) organizations qualify
3. **Acknowledgment Requirement**: Donations ≥ $250 require written acknowledgment
4. **Non-Cash Appraisal**: Items > $5,000 require qualified appraisal
5. **Recurring Frequency**: Minimum weekly, maximum annually
6. **Refund Impact**: Refunded donations adjust tax deduction totals
7. **User Isolation**: Users can only access their own donations
8. **Audit Trail**: All donation modifications logged with timestamp
9. **Payment Method Validation**: Valid payment method required for recurring donations
10. **Organization Verification**: Charity must be verified before accepting donations

## Data Persistence

### Donations Table
```sql
CREATE TABLE Donations (
    DonationId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    UserId UNIQUEIDENTIFIER NOT NULL,
    OrganizationId UNIQUEIDENTIFIER NOT NULL,
    Amount DECIMAL(18,2) NOT NULL,
    DonationDate DATETIME2 NOT NULL,
    PaymentMethod NVARCHAR(50) NOT NULL,
    IsTaxDeductible BIT NOT NULL DEFAULT 1,
    DonationType NVARCHAR(50) NOT NULL,
    Category NVARCHAR(50) NOT NULL,
    CheckNumber NVARCHAR(50) NULL,
    ConfirmationNumber NVARCHAR(100) NULL,
    Notes NVARCHAR(1000) NULL,
    ReceiptUrl NVARCHAR(500) NULL,
    AcknowledgmentLetterUrl NVARCHAR(500) NULL,
    AcknowledgmentReceived BIT NOT NULL DEFAULT 0,
    FairMarketValue DECIMAL(18,2) NULL,
    ValuationMethod NVARCHAR(200) NULL,
    AppraisalRequired BIT NOT NULL DEFAULT 0,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    RefundedAt DATETIME2 NULL,
    RefundAmount DECIMAL(18,2) NULL,
    RefundReason NVARCHAR(500) NULL,

    CONSTRAINT FK_Donations_Users FOREIGN KEY (UserId) REFERENCES Users(UserId),
    CONSTRAINT FK_Donations_Organizations FOREIGN KEY (OrganizationId) REFERENCES Organizations(OrganizationId),
    INDEX IX_Donations_UserId (UserId),
    INDEX IX_Donations_OrganizationId (OrganizationId),
    INDEX IX_Donations_DonationDate (DonationDate),
    INDEX IX_Donations_IsTaxDeductible (IsTaxDeductible)
);
```

### RecurringDonations Table
```sql
CREATE TABLE RecurringDonations (
    RecurringDonationId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    UserId UNIQUEIDENTIFIER NOT NULL,
    OrganizationId UNIQUEIDENTIFIER NOT NULL,
    Amount DECIMAL(18,2) NOT NULL,
    Frequency NVARCHAR(50) NOT NULL,
    StartDate DATETIME2 NOT NULL,
    EndDate DATETIME2 NULL,
    NextScheduledDate DATETIME2 NOT NULL,
    PaymentMethod NVARCHAR(50) NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    PausedUntil DATETIME2 NULL,
    DonationsMade INT NOT NULL DEFAULT 0,
    TotalDonated DECIMAL(18,2) NOT NULL DEFAULT 0,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),

    CONSTRAINT FK_RecurringDonations_Users FOREIGN KEY (UserId) REFERENCES Users(UserId),
    CONSTRAINT FK_RecurringDonations_Organizations FOREIGN KEY (OrganizationId) REFERENCES Organizations(OrganizationId),
    INDEX IX_RecurringDonations_UserId (UserId),
    INDEX IX_RecurringDonations_NextScheduledDate (NextScheduledDate),
    INDEX IX_RecurringDonations_IsActive (IsActive)
);
```

## Background Jobs

### RecurringDonationExecutorJob
- **Schedule**: Runs daily at 6:00 AM
- **Purpose**: Execute recurring donations that are due
- **Logic**:
  1. Query RecurringDonations with NextScheduledDate <= Today and IsActive = true
  2. Skip if PausedUntil > Today
  3. Execute donation through payment processor
  4. Create Donation record
  5. Update NextScheduledDate, DonationsMade, TotalDonated
  6. Raise RecurringDonationExecuted event

### AcknowledgmentReminderJob
- **Schedule**: Runs weekly
- **Purpose**: Remind users of missing acknowledgment letters
- **Logic**:
  1. Query Donations >= $250 without acknowledgments
  2. Filter donations > 30 days old
  3. Send reminder notification
  4. Raise AcknowledgmentLetterMissing event

## Error Handling

### Validation Errors (400 Bad Request)
- Missing required fields
- Invalid donation amount
- Future donation date
- Invalid organization ID

### Authorization Errors (403 Forbidden)
- User attempting to access another user's donation
- Organization not verified for tax-deductible donations

### Not Found Errors (404 Not Found)
- DonationId doesn't exist
- RecurringDonationId doesn't exist
- OrganizationId doesn't exist

### Conflict Errors (409 Conflict)
- Donation already refunded
- Concurrent modification

### Server Errors (500 Internal Server Error)
- Database failures
- Document upload failures
- Payment processing failures

## Security Considerations

- Validate user authorization on all operations
- Encrypt sensitive payment information
- Secure document storage with access controls
- Implement rate limiting on donation recording
- Log all financial transactions for audit
- Validate organization legitimacy before recording donations

## Performance Considerations

- Index on UserId and DonationDate for queries
- Cache organization verification status
- Optimize tax year queries with materialized views
- Batch process recurring donation executions
- Use async processing for document uploads

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
- Document upload/retrieval
- Event publishing
- Authorization checks

### Performance Tests
- Large donation history queries
- Bulk recurring donation processing
- Concurrent donation recording
