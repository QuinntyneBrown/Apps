# Autopay - Backend Requirements

## Overview
Autopay feature enables automatic payment execution for recurring bills without manual intervention.

## Domain Model

### AutopayConfiguration Aggregate
- **AutopayConfigId**: Unique identifier (Guid)
- **BillId**: Associated bill (Guid)
- **UserId**: Owner (Guid)
- **PaymentMethodId**: Payment method to use (Guid)
- **IsEnabled**: Active status (bool)
- **MaxAmount**: Maximum amount to auto-pay (decimal, nullable)
- **RequireApproval**: Require manual approval if amount changes (bool)
- **ApprovalThresholdPercentage**: Percent change requiring approval (decimal)
- **LastExecutionDate**: Last autopay execution (DateTime, nullable)
- **NextExecutionDate**: Next scheduled execution (DateTime, nullable)
- **ExecutionDaysBefore**: Days before due date to execute (int, default 0)
- **FailureCount**: Consecutive failures (int)
- **CreatedAt**: Creation timestamp (DateTime)
- **UpdatedAt**: Last modification timestamp (DateTime)

## Commands

### EnableAutopayCommand
- Validates bill and payment method exist
- Creates AutopayConfiguration
- Sets NextExecutionDate based on bill due date
- Returns AutopayConfigId

### DisableAutopayCommand
- Deactivates autopay configuration
- Cancels any pending auto-scheduled payments

### UpdateAutopayConfigurationCommand
- Updates configuration settings
- Recalculates NextExecutionDate if needed

### ExecuteAutopayCommand
- Processes automatic payment
- Checks max amount limit
- Checks approval requirement
- Creates payment schedule and executes
- Updates execution dates

## Queries

### GetAutopayConfigByBillIdQuery
- Returns autopay configuration for a bill

### GetAutopayConfigsByUserIdQuery
- Returns all user's autopay configurations
- Filters by enabled status

### GetDueAutopayExecutionsQuery
- Returns autopay configurations due for execution
- Used by background job

## Business Rules

1. **Amount Validation**: Payment amount cannot exceed MaxAmount if set
2. **Approval Threshold**: If bill amount changes by more than threshold, require approval
3. **Failure Handling**: After 3 consecutive failures, disable autopay and notify user
4. **Payment Method**: Must be active and not expired
5. **Execution Window**: Execute X days before due date (configurable)
6. **One Config Per Bill**: Only one autopay configuration per bill
7. **Sufficient Funds**: Check balance if available from payment method

## API Endpoints

### POST /api/autopay
- Enables autopay for a bill
- Returns: 201 Created

### PUT /api/autopay/{autopayConfigId}
- Updates autopay configuration
- Returns: 200 OK

### DELETE /api/autopay/{autopayConfigId}
- Disables autopay
- Returns: 204 No Content

### GET /api/autopay/bill/{billId}
- Gets autopay configuration for bill
- Returns: 200 OK

### GET /api/autopay
- Gets all user's autopay configurations
- Returns: 200 OK

### POST /api/autopay/{autopayConfigId}/approve
- Approves pending autopay execution
- Returns: 200 OK

## Background Jobs

### AutopayExecutorJob
- **Schedule**: Runs daily at configured time
- **Purpose**: Execute pending autopay configurations
- **Logic**:
  1. Query autopay configs where NextExecutionDate <= Now
  2. Validate payment method and amount
  3. Create and execute payment
  4. Update execution dates
  5. Handle failures

## Data Persistence

### AutopayConfigurations Table
```sql
CREATE TABLE AutopayConfigurations (
    AutopayConfigId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    BillId UNIQUEIDENTIFIER NOT NULL UNIQUE,
    UserId UNIQUEIDENTIFIER NOT NULL,
    PaymentMethodId UNIQUEIDENTIFIER NOT NULL,
    IsEnabled BIT NOT NULL DEFAULT 1,
    MaxAmount DECIMAL(18,2) NULL,
    RequireApproval BIT NOT NULL DEFAULT 0,
    ApprovalThresholdPercentage DECIMAL(5,2) NOT NULL DEFAULT 10.00,
    LastExecutionDate DATETIME2 NULL,
    NextExecutionDate DATETIME2 NULL,
    ExecutionDaysBefore INT NOT NULL DEFAULT 0,
    FailureCount INT NOT NULL DEFAULT 0,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),

    CONSTRAINT FK_Autopay_Bills FOREIGN KEY (BillId) REFERENCES Bills(BillId),
    CONSTRAINT FK_Autopay_Users FOREIGN KEY (UserId) REFERENCES Users(UserId),
    CONSTRAINT FK_Autopay_PaymentMethods FOREIGN KEY (PaymentMethodId) REFERENCES PaymentMethods(PaymentMethodId),
    INDEX IX_Autopay_NextExecutionDate (NextExecutionDate),
    INDEX IX_Autopay_UserId (UserId)
);
```
