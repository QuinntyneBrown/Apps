# Bill Management - Backend Requirements

## Overview
The Bill Management feature handles all operations related to creating, updating, retrieving, and deleting bills in the system.

## Domain Model

### Bill Aggregate
- **BillId**: Unique identifier (Guid)
- **UserId**: Owner of the bill (Guid)
- **Payee**: Name of the bill recipient (string, max 200 chars)
- **Amount**: Bill amount (decimal, precision 18,2)
- **DueDate**: Payment due date (DateTime)
- **Category**: Bill category (enum: Utilities, Housing, Insurance, Subscriptions, Healthcare, Transportation, Other)
- **RecurrencePattern**: How often the bill repeats (enum: None, Weekly, BiWeekly, Monthly, Quarterly, Annually)
- **Description**: Optional bill description (string, max 1000 chars)
- **IsActive**: Whether the bill is currently active (bool)
- **AccountNumber**: Optional account or reference number (string, max 100 chars)
- **Tags**: Optional tags for organization (List<string>)
- **CreatedAt**: Bill creation timestamp (DateTime)
- **UpdatedAt**: Last modification timestamp (DateTime)
- **CreatedBy**: User who created the bill (Guid)
- **UpdatedBy**: User who last updated the bill (Guid)

## Commands

### AddBillCommand
- Validates required fields (Payee, Amount, DueDate)
- Ensures Amount > 0
- Ensures DueDate is not in the past (for new bills)
- Creates Bill aggregate
- Raises **BillAdded** domain event
- Returns BillId

### UpdateBillAmountCommand
- Validates BillId exists
- Validates new Amount > 0
- Validates Amount has actually changed
- Updates Bill.Amount
- Raises **BillAmountChanged** domain event
- Returns success indicator

### UpdateBillDueDateCommand
- Validates BillId exists
- Validates new DueDate
- Validates DueDate has actually changed
- Updates Bill.DueDate
- Raises **BillDueDateChanged** domain event
- Returns success indicator

### UpdateBillCommand
- Validates BillId exists
- Validates all business rules
- Updates Bill properties (except Amount and DueDate - use specific commands)
- Returns success indicator

### DeleteBillCommand
- Validates BillId exists
- Validates user has permission to delete
- Checks for active payment schedules (warn user)
- Soft deletes or archives the bill
- Raises **BillDeleted** domain event
- Returns success indicator

### ToggleBillActiveStatusCommand
- Validates BillId exists
- Toggles IsActive flag
- Returns success indicator

## Queries

### GetBillByIdQuery
- Returns Bill details by BillId
- Includes all properties
- Returns null if not found

### GetBillsByUserIdQuery
- Returns all bills for a specific user
- Supports filtering by:
  - IsActive status
  - Category
  - Date range
  - Tags
- Supports sorting by:
  - DueDate (ascending/descending)
  - Amount (ascending/descending)
  - Payee (alphabetically)
  - CreatedAt
- Supports pagination
- Returns list of BillDto

### GetUpcomingBillsQuery
- Returns bills due within specified date range
- Default: next 30 days
- Sorted by DueDate ascending
- Includes recurrence calculations
- Returns list of BillDto with next due date

### GetOverdueBillsQuery
- Returns bills past their due date
- Filters by unpaid status
- Sorted by DueDate ascending
- Returns list of BillDto

### GetBillsByCategoryQuery
- Returns bills grouped by category
- Includes total amount per category
- Supports date range filtering
- Returns dictionary of Category -> List<BillDto>

### SearchBillsQuery
- Full-text search across Payee, Description, AccountNumber
- Supports filters (Category, IsActive, DateRange)
- Returns paginated results

## Domain Events

### BillAdded
```csharp
public class BillAdded : DomainEvent
{
    public Guid BillId { get; set; }
    public Guid UserId { get; set; }
    public string Payee { get; set; }
    public decimal Amount { get; set; }
    public DateTime DueDate { get; set; }
    public string Category { get; set; }
    public string RecurrencePattern { get; set; }
    public DateTime OccurredAt { get; set; }
}
```

### BillAmountChanged
```csharp
public class BillAmountChanged : DomainEvent
{
    public Guid BillId { get; set; }
    public Guid UserId { get; set; }
    public decimal OldAmount { get; set; }
    public decimal NewAmount { get; set; }
    public DateTime OccurredAt { get; set; }
}
```

### BillDueDateChanged
```csharp
public class BillDueDateChanged : DomainEvent
{
    public Guid BillId { get; set; }
    public Guid UserId { get; set; }
    public DateTime OldDueDate { get; set; }
    public DateTime NewDueDate { get; set; }
    public DateTime OccurredAt { get; set; }
}
```

### BillDeleted
```csharp
public class BillDeleted : DomainEvent
{
    public Guid BillId { get; set; }
    public Guid UserId { get; set; }
    public string Payee { get; set; }
    public DateTime OccurredAt { get; set; }
}
```

## API Endpoints

### POST /api/bills
- Creates a new bill
- Request body: AddBillCommand
- Returns: 201 Created with BillId
- Authorization: Authenticated user

### PUT /api/bills/{billId}
- Updates an existing bill
- Request body: UpdateBillCommand
- Returns: 200 OK
- Authorization: Bill owner

### PUT /api/bills/{billId}/amount
- Updates bill amount
- Request body: { amount: decimal }
- Returns: 200 OK
- Authorization: Bill owner

### PUT /api/bills/{billId}/due-date
- Updates bill due date
- Request body: { dueDate: DateTime }
- Returns: 200 OK
- Authorization: Bill owner

### DELETE /api/bills/{billId}
- Deletes a bill
- Returns: 204 No Content
- Authorization: Bill owner

### GET /api/bills/{billId}
- Retrieves bill details
- Returns: 200 OK with BillDto
- Authorization: Bill owner

### GET /api/bills
- Retrieves all bills for current user
- Query params: filter, sort, page, pageSize
- Returns: 200 OK with paginated BillDto list
- Authorization: Authenticated user

### GET /api/bills/upcoming
- Retrieves upcoming bills
- Query params: days (default 30)
- Returns: 200 OK with BillDto list
- Authorization: Authenticated user

### GET /api/bills/overdue
- Retrieves overdue bills
- Returns: 200 OK with BillDto list
- Authorization: Authenticated user

### GET /api/bills/search
- Searches bills
- Query params: q (query), filter, page, pageSize
- Returns: 200 OK with paginated BillDto list
- Authorization: Authenticated user

## Business Rules

1. **Bill Amount**: Must be greater than 0
2. **Due Date**: Cannot be more than 5 years in the future
3. **Recurrence**: If RecurrencePattern is set, system should auto-generate future bill instances
4. **Deletion**: Bills with active autopay must be confirmed before deletion
5. **User Isolation**: Users can only access their own bills
6. **Audit Trail**: All changes must be logged with user and timestamp
7. **Soft Delete**: Bills should be soft-deleted to maintain payment history

## Data Persistence

### Bills Table
```sql
CREATE TABLE Bills (
    BillId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    UserId UNIQUEIDENTIFIER NOT NULL,
    Payee NVARCHAR(200) NOT NULL,
    Amount DECIMAL(18,2) NOT NULL,
    DueDate DATETIME2 NOT NULL,
    Category NVARCHAR(50) NOT NULL,
    RecurrencePattern NVARCHAR(50) NOT NULL,
    Description NVARCHAR(1000) NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    AccountNumber NVARCHAR(100) NULL,
    Tags NVARCHAR(MAX) NULL, -- JSON array
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    CreatedBy UNIQUEIDENTIFIER NOT NULL,
    UpdatedBy UNIQUEIDENTIFIER NOT NULL,
    IsDeleted BIT NOT NULL DEFAULT 0,
    DeletedAt DATETIME2 NULL,

    CONSTRAINT FK_Bills_Users FOREIGN KEY (UserId) REFERENCES Users(UserId),
    INDEX IX_Bills_UserId (UserId),
    INDEX IX_Bills_DueDate (DueDate),
    INDEX IX_Bills_Category (Category),
    INDEX IX_Bills_IsActive (IsActive)
);
```

### Domain Events Table
```sql
CREATE TABLE DomainEvents (
    EventId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    AggregateId UNIQUEIDENTIFIER NOT NULL,
    AggregateType NVARCHAR(100) NOT NULL,
    EventType NVARCHAR(100) NOT NULL,
    EventData NVARCHAR(MAX) NOT NULL, -- JSON
    OccurredAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    ProcessedAt DATETIME2 NULL,

    INDEX IX_DomainEvents_AggregateId (AggregateId),
    INDEX IX_DomainEvents_EventType (EventType),
    INDEX IX_DomainEvents_OccurredAt (OccurredAt)
);
```

## Error Handling

### Validation Errors (400 Bad Request)
- Missing required fields
- Invalid amount (<=0)
- Invalid date format
- Invalid category or recurrence pattern

### Authorization Errors (403 Forbidden)
- User attempting to access another user's bill
- User attempting to delete bill without permission

### Not Found Errors (404 Not Found)
- BillId does not exist
- Bill was deleted

### Conflict Errors (409 Conflict)
- Attempting to delete bill with active autopay without confirmation

### Server Errors (500 Internal Server Error)
- Database connection failures
- Unexpected exceptions

## Integration Points

### Event Handlers
- **BillAdded**: Trigger reminder creation, update cash flow projections
- **BillAmountChanged**: Update payment schedules, recalculate cash flow
- **BillDueDateChanged**: Update reminders, adjust payment schedules
- **BillDeleted**: Cancel associated reminders and autopay settings

### Background Jobs
- **Recurrence Job**: Daily job to create next bill instances for recurring bills
- **Cleanup Job**: Archive old deleted bills (older than 7 years)

## Performance Considerations

- Index on UserId for fast user-specific queries
- Index on DueDate for upcoming/overdue queries
- Implement caching for frequently accessed bills
- Use pagination for list queries
- Optimize search with full-text indexing

## Security Considerations

- Validate user authorization on all operations
- Sanitize user input to prevent SQL injection
- Encrypt sensitive fields (AccountNumber)
- Implement rate limiting on API endpoints
- Log all access attempts for audit

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
- Event publishing
- Authorization checks

### Performance Tests
- Query performance with large datasets (100k+ bills)
- Concurrent user operations
- Search query optimization
