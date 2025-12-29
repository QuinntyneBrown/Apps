# Backend Requirements: Expense Management

## Overview
The Expense Management feature enables pet owners to track pet-related expenses, file insurance claims, and monitor claim settlements. The system uses domain events to maintain consistency and enable audit trails.

## Domain Events

### 1. PetExpenseRecorded
**Purpose**: Logged when a pet-related expense is recorded in the system

**Event Payload**:
```json
{
  "eventId": "uuid",
  "eventType": "PetExpenseRecorded",
  "timestamp": "ISO-8601 timestamp",
  "aggregateId": "expense-uuid",
  "aggregateType": "PetExpense",
  "version": 1,
  "data": {
    "expenseId": "uuid",
    "petId": "uuid",
    "amount": "decimal",
    "currency": "string",
    "category": "string",
    "description": "string",
    "expenseDate": "ISO-8601 date",
    "vendor": "string",
    "paymentMethod": "string",
    "receiptUrl": "string (optional)",
    "isInsurable": "boolean",
    "recordedBy": "uuid",
    "recordedAt": "ISO-8601 timestamp"
  }
}
```

**Triggers**:
- User manually logs an expense
- System imports expense from receipt scan
- Recurring expense is automatically created

**Downstream Effects**:
- Update pet's total expense tracker
- Notify budgeting service
- Trigger insurance eligibility check if `isInsurable` is true

### 2. InsuranceClaimFiled
**Purpose**: Logged when an insurance claim is submitted for pet expenses

**Event Payload**:
```json
{
  "eventId": "uuid",
  "eventType": "InsuranceClaimFiled",
  "timestamp": "ISO-8601 timestamp",
  "aggregateId": "claim-uuid",
  "aggregateType": "InsuranceClaim",
  "version": 1,
  "data": {
    "claimId": "uuid",
    "petId": "uuid",
    "policyNumber": "string",
    "expenseIds": ["uuid"],
    "totalClaimAmount": "decimal",
    "currency": "string",
    "claimType": "string",
    "submissionDate": "ISO-8601 date",
    "supportingDocuments": ["url"],
    "insuranceProvider": "string",
    "contactEmail": "string",
    "filedBy": "uuid",
    "filedAt": "ISO-8601 timestamp"
  }
}
```

**Triggers**:
- User submits claim manually
- System auto-files claim based on eligible expenses
- Batch claim submission for multiple expenses

**Downstream Effects**:
- Send notification to insurance provider integration
- Update expense status to "claim pending"
- Create claim tracking record
- Send confirmation email to user

### 3. InsuranceClaimSettled
**Purpose**: Logged when an insurance claim is resolved (approved, denied, or partially approved)

**Event Payload**:
```json
{
  "eventId": "uuid",
  "eventType": "InsuranceClaimSettled",
  "timestamp": "ISO-8601 timestamp",
  "aggregateId": "claim-uuid",
  "aggregateType": "InsuranceClaim",
  "version": 2,
  "data": {
    "claimId": "uuid",
    "settlementStatus": "approved|denied|partial",
    "approvedAmount": "decimal",
    "deniedAmount": "decimal",
    "currency": "string",
    "settlementDate": "ISO-8601 date",
    "reimbursementMethod": "string",
    "estimatedPaymentDate": "ISO-8601 date",
    "denialReason": "string (optional)",
    "adjustments": [{
      "expenseId": "uuid",
      "requestedAmount": "decimal",
      "approvedAmount": "decimal",
      "reason": "string"
    }],
    "settledBy": "string",
    "settledAt": "ISO-8601 timestamp"
  }
}
```

**Triggers**:
- Insurance provider processes claim
- Manual settlement by admin
- Automated settlement based on policy rules

**Downstream Effects**:
- Update expense reimbursement status
- Calculate net out-of-pocket cost
- Send notification to user
- Update financial reports
- Trigger payment tracking if approved

## Aggregates

### PetExpense
**Aggregate Root**: Represents a single pet-related expense

**Properties**:
- `expenseId` (uuid, primary key)
- `petId` (uuid, foreign key)
- `amount` (decimal)
- `currency` (string, default "USD")
- `category` (enum: VeterinaryCare, Food, Grooming, Boarding, Training, Supplies, Insurance, Other)
- `description` (string)
- `expenseDate` (date)
- `vendor` (string)
- `paymentMethod` (enum: Cash, CreditCard, DebitCard, Check, Other)
- `receiptUrl` (string, nullable)
- `isInsurable` (boolean)
- `claimStatus` (enum: None, Pending, Approved, Denied, Partial)
- `reimbursementAmount` (decimal, nullable)
- `tags` (string array)
- `createdBy` (uuid)
- `createdAt` (timestamp)
- `updatedAt` (timestamp)

**Business Rules**:
- Expense amount must be positive
- Expense date cannot be in the future
- Category must be valid enum value
- Receipt URL must be valid if provided
- Insurable expenses must have category VeterinaryCare or Insurance

**Commands**:
- `RecordExpense`
- `UpdateExpense`
- `DeleteExpense`
- `AttachReceipt`
- `MarkAsInsurable`

### InsuranceClaim
**Aggregate Root**: Represents an insurance claim for one or more expenses

**Properties**:
- `claimId` (uuid, primary key)
- `petId` (uuid, foreign key)
- `policyNumber` (string)
- `expenseIds` (uuid array)
- `totalClaimAmount` (decimal)
- `approvedAmount` (decimal, nullable)
- `deniedAmount` (decimal, nullable)
- `currency` (string, default "USD")
- `claimType` (enum: Illness, Injury, WellnessCheckup, Surgery, Emergency, Other)
- `status` (enum: Draft, Submitted, UnderReview, Approved, Denied, PartiallyApproved, Paid)
- `submissionDate` (date, nullable)
- `settlementDate` (date, nullable)
- `supportingDocuments` (string array)
- `insuranceProvider` (string)
- `contactEmail` (string)
- `denialReason` (string, nullable)
- `notes` (string, nullable)
- `createdBy` (uuid)
- `createdAt` (timestamp)
- `updatedAt` (timestamp)

**Business Rules**:
- Claim must reference at least one expense
- All referenced expenses must belong to the same pet
- Total claim amount must match sum of referenced expenses
- Cannot submit claim without supporting documents
- Cannot settle claim that hasn't been submitted
- Settlement amount cannot exceed claim amount

**Commands**:
- `CreateClaim`
- `AddExpenseToClaim`
- `RemoveExpenseFromClaim`
- `SubmitClaim`
- `SettleClaim`
- `UpdateClaimStatus`

## API Endpoints

### Expense Endpoints

#### POST /api/expenses
Create a new pet expense
```json
Request:
{
  "petId": "uuid",
  "amount": 125.50,
  "currency": "USD",
  "category": "VeterinaryCare",
  "description": "Annual checkup",
  "expenseDate": "2025-12-15",
  "vendor": "Happy Paws Veterinary Clinic",
  "paymentMethod": "CreditCard",
  "receiptUrl": "https://storage.example.com/receipts/123.pdf",
  "isInsurable": true,
  "tags": ["annual", "preventive"]
}

Response: 201 Created
{
  "expenseId": "uuid",
  "message": "Expense recorded successfully"
}
```

#### GET /api/expenses?petId={petId}&startDate={date}&endDate={date}
Retrieve expenses with optional filters

#### GET /api/expenses/{expenseId}
Retrieve a specific expense

#### PUT /api/expenses/{expenseId}
Update an existing expense

#### DELETE /api/expenses/{expenseId}
Delete an expense (soft delete)

#### GET /api/expenses/summary?petId={petId}&year={year}
Get expense summary by category

### Insurance Claim Endpoints

#### POST /api/insurance-claims
Create a new insurance claim
```json
Request:
{
  "petId": "uuid",
  "policyNumber": "PET-2025-12345",
  "expenseIds": ["uuid1", "uuid2"],
  "claimType": "Illness",
  "insuranceProvider": "PetSure Insurance",
  "contactEmail": "claims@petsure.com",
  "supportingDocuments": ["https://storage.example.com/docs/1.pdf"],
  "notes": "Claim for respiratory infection treatment"
}

Response: 201 Created
{
  "claimId": "uuid",
  "totalClaimAmount": 450.75,
  "status": "Draft",
  "message": "Claim created successfully"
}
```

#### POST /api/insurance-claims/{claimId}/submit
Submit a claim to insurance provider

#### PUT /api/insurance-claims/{claimId}/settle
Settle a claim (admin only)
```json
Request:
{
  "settlementStatus": "partial",
  "approvedAmount": 400.00,
  "deniedAmount": 50.75,
  "settlementDate": "2025-12-20",
  "reimbursementMethod": "DirectDeposit",
  "estimatedPaymentDate": "2025-12-27",
  "adjustments": [{
    "expenseId": "uuid1",
    "requestedAmount": 50.75,
    "approvedAmount": 0,
    "reason": "Pre-existing condition exclusion"
  }]
}
```

#### GET /api/insurance-claims?petId={petId}&status={status}
Retrieve claims with optional filters

#### GET /api/insurance-claims/{claimId}
Retrieve a specific claim

## Data Validation Rules

### Expense Validation
- Amount: Required, positive decimal with 2 decimal places, max 999999.99
- Currency: Required, ISO 4217 currency code
- Category: Required, must be valid enum value
- Description: Required, max 500 characters
- ExpenseDate: Required, cannot be future date, not older than 10 years
- Vendor: Required, max 200 characters
- PaymentMethod: Required, must be valid enum value
- ReceiptUrl: Optional, must be valid URL if provided
- IsInsurable: Required, boolean
- Tags: Optional, max 10 tags, each max 50 characters

### Insurance Claim Validation
- PetId: Required, must exist in system
- PolicyNumber: Required, alphanumeric with hyphens, max 50 characters
- ExpenseIds: Required, at least 1 expense, all must exist and belong to pet
- ClaimType: Required, must be valid enum value
- InsuranceProvider: Required, max 200 characters
- ContactEmail: Required, valid email format
- SupportingDocuments: Required for submission, at least 1 document
- SettlementStatus: Must be valid enum value
- ApprovedAmount: Required for settlement, cannot exceed total claim amount

## Security Requirements

### Authentication
- All endpoints require valid JWT token
- User must own the pet to access its expenses
- Admin role required for claim settlement

### Authorization
- Users can only view/modify their own pet expenses
- Insurance claim submission requires verified account
- Claim settlement requires Admin or InsuranceProcessor role

### Data Protection
- Receipt URLs must be pre-signed with expiration
- PII in claims must be encrypted at rest
- Audit log all expense and claim modifications
- GDPR compliance: Support data export and deletion

## Performance Requirements

- Expense creation: < 200ms response time
- Expense query (filtered): < 500ms for up to 1000 results
- Claim submission: < 1s response time
- Event publishing: < 100ms after command execution
- Support 1000 concurrent users
- Database queries must use appropriate indexes

## Data Retention

- Expenses: Retain for 7 years for tax purposes
- Insurance claims: Retain for lifetime of pet + 3 years
- Event store: Retain all events indefinitely
- Receipts: Retain for 7 years, then archive

## Integration Points

### Event Bus
- Publish all domain events to message broker (RabbitMQ/Azure Service Bus)
- Subscribe to PetCreated, PetDeleted events
- Subscribe to UserDeactivated event for data archival

### External Services
- Insurance provider APIs for claim submission
- OCR service for receipt scanning
- Cloud storage for receipt/document storage
- Email service for notifications
- SMS service for claim status updates

## Database Schema

### Expenses Table
```sql
CREATE TABLE Expenses (
    ExpenseId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    PetId UNIQUEIDENTIFIER NOT NULL,
    Amount DECIMAL(10,2) NOT NULL,
    Currency VARCHAR(3) NOT NULL DEFAULT 'USD',
    Category VARCHAR(50) NOT NULL,
    Description NVARCHAR(500) NOT NULL,
    ExpenseDate DATE NOT NULL,
    Vendor NVARCHAR(200) NOT NULL,
    PaymentMethod VARCHAR(50) NOT NULL,
    ReceiptUrl NVARCHAR(1000) NULL,
    IsInsurable BIT NOT NULL DEFAULT 0,
    ClaimStatus VARCHAR(50) NOT NULL DEFAULT 'None',
    ReimbursementAmount DECIMAL(10,2) NULL,
    Tags NVARCHAR(MAX) NULL, -- JSON array
    IsDeleted BIT NOT NULL DEFAULT 0,
    CreatedBy UNIQUEIDENTIFIER NOT NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),

    CONSTRAINT FK_Expenses_Pets FOREIGN KEY (PetId) REFERENCES Pets(PetId),
    CONSTRAINT FK_Expenses_Users FOREIGN KEY (CreatedBy) REFERENCES Users(UserId),
    CONSTRAINT CHK_Expenses_Amount CHECK (Amount > 0),
    CONSTRAINT CHK_Expenses_ExpenseDate CHECK (ExpenseDate <= GETDATE())
);

CREATE INDEX IX_Expenses_PetId ON Expenses(PetId);
CREATE INDEX IX_Expenses_ExpenseDate ON Expenses(ExpenseDate);
CREATE INDEX IX_Expenses_Category ON Expenses(Category);
CREATE INDEX IX_Expenses_ClaimStatus ON Expenses(ClaimStatus);
```

### InsuranceClaims Table
```sql
CREATE TABLE InsuranceClaims (
    ClaimId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    PetId UNIQUEIDENTIFIER NOT NULL,
    PolicyNumber NVARCHAR(50) NOT NULL,
    TotalClaimAmount DECIMAL(10,2) NOT NULL,
    ApprovedAmount DECIMAL(10,2) NULL,
    DeniedAmount DECIMAL(10,2) NULL,
    Currency VARCHAR(3) NOT NULL DEFAULT 'USD',
    ClaimType VARCHAR(50) NOT NULL,
    Status VARCHAR(50) NOT NULL DEFAULT 'Draft',
    SubmissionDate DATE NULL,
    SettlementDate DATE NULL,
    InsuranceProvider NVARCHAR(200) NOT NULL,
    ContactEmail NVARCHAR(255) NOT NULL,
    DenialReason NVARCHAR(1000) NULL,
    Notes NVARCHAR(2000) NULL,
    CreatedBy UNIQUEIDENTIFIER NOT NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),

    CONSTRAINT FK_InsuranceClaims_Pets FOREIGN KEY (PetId) REFERENCES Pets(PetId),
    CONSTRAINT FK_InsuranceClaims_Users FOREIGN KEY (CreatedBy) REFERENCES Users(UserId),
    CONSTRAINT CHK_InsuranceClaims_Amount CHECK (TotalClaimAmount > 0)
);

CREATE INDEX IX_InsuranceClaims_PetId ON InsuranceClaims(PetId);
CREATE INDEX IX_InsuranceClaims_Status ON InsuranceClaims(Status);
CREATE INDEX IX_InsuranceClaims_SubmissionDate ON InsuranceClaims(SubmissionDate);
```

### ClaimExpenses Table (Junction Table)
```sql
CREATE TABLE ClaimExpenses (
    ClaimId UNIQUEIDENTIFIER NOT NULL,
    ExpenseId UNIQUEIDENTIFIER NOT NULL,

    PRIMARY KEY (ClaimId, ExpenseId),
    CONSTRAINT FK_ClaimExpenses_Claims FOREIGN KEY (ClaimId) REFERENCES InsuranceClaims(ClaimId),
    CONSTRAINT FK_ClaimExpenses_Expenses FOREIGN KEY (ExpenseId) REFERENCES Expenses(ExpenseId)
);
```

### ClaimDocuments Table
```sql
CREATE TABLE ClaimDocuments (
    DocumentId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    ClaimId UNIQUEIDENTIFIER NOT NULL,
    DocumentUrl NVARCHAR(1000) NOT NULL,
    DocumentType VARCHAR(50) NOT NULL,
    UploadedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),

    CONSTRAINT FK_ClaimDocuments_Claims FOREIGN KEY (ClaimId) REFERENCES InsuranceClaims(ClaimId)
);
```

## Testing Requirements

### Unit Tests
- Test all aggregate business rules
- Test command handlers
- Test event handlers
- Test validation logic
- Target: 90% code coverage

### Integration Tests
- Test API endpoints with real database
- Test event publishing and consumption
- Test external service integrations (with mocks)
- Test database transactions and rollbacks

### Performance Tests
- Load test with 1000 concurrent expense creations
- Stress test claim submissions
- Query performance test with 100k+ expenses

## Monitoring & Observability

### Metrics
- Expense creation rate
- Claim submission rate
- Claim settlement time (average, p95, p99)
- API response times
- Event processing lag
- Database query performance

### Alerts
- High API error rate (> 5%)
- Event processing delay (> 5 minutes)
- Database connection pool exhaustion
- External service failures
- Unusual expense patterns (fraud detection)

### Logging
- Log all API requests with correlation IDs
- Log all domain events published
- Log all external service calls
- Log all validation failures
- Log all authorization failures
