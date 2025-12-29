# Subscription Management - Backend Requirements

## Overview
Core functionality for tracking recurring subscriptions, managing renewals, and calculating spending.

## Domain Model

### Subscription Aggregate
- **SubscriptionId**: Unique identifier (Guid)
- **UserId**: Owner (Guid)
- **ServiceName**: Service/company name (string, max 200)
- **Cost**: Billing amount (decimal)
- **BillingFrequency**: Frequency (enum: Monthly, Quarterly, Annual, Custom)
- **StartDate**: Subscription start date (DateTime)
- **NextRenewalDate**: Next billing date (DateTime)
- **CategoryId**: Category reference (Guid)
- **PaymentMethodId**: Payment method (Guid)
- **Status**: Status (enum: Active, Paused, Cancelled, Trial)
- **TrialEndDate**: Trial expiration (DateTime, nullable)
- **CancellationDeadline**: Last day to cancel (DateTime, nullable)
- **Notes**: Additional notes (string, max 1000)
- **CreatedAt**: Creation timestamp (DateTime)
- **UpdatedAt**: Last modification timestamp (DateTime)

## Commands

### AddSubscriptionCommand
- Adds new subscription
- Calculates next renewal date
- Raises **SubscriptionAdded** event

### CancelSubscriptionCommand
- Cancels subscription
- Records cancellation reason
- Raises **SubscriptionCancelled** event

### PauseSubscriptionCommand
- Temporarily suspends subscription
- Raises **SubscriptionPaused** event

## Queries

### GetSubscriptionsByUserIdQuery
- Returns all user subscriptions

### GetActiveSubscriptionsQuery
- Returns only active subscriptions

### GetUpcomingRenewalsQuery
- Returns subscriptions renewing soon

## Domain Events

### SubscriptionAdded
```csharp
public class SubscriptionAdded : DomainEvent
{
    public Guid SubscriptionId { get; set; }
    public string ServiceName { get; set; }
    public decimal Cost { get; set; }
    public string BillingFrequency { get; set; }
    public DateTime NextRenewalDate { get; set; }
    public DateTime OccurredAt { get; set; }
}
```

### SubscriptionCancelled
```csharp
public class SubscriptionCancelled : DomainEvent
{
    public Guid SubscriptionId { get; set; }
    public string CancellationReason { get; set; }
    public decimal TotalAmountPaid { get; set; }
    public TimeSpan DurationActive { get; set; }
    public decimal SavingsFromCancellation { get; set; }
    public DateTime OccurredAt { get; set; }
}
```

## API Endpoints

### POST /api/subscriptions
- Creates subscription
- Returns: 201 Created

### PUT /api/subscriptions/{id}
- Updates subscription
- Returns: 200 OK

### DELETE /api/subscriptions/{id}
- Cancels subscription
- Returns: 204 No Content

### GET /api/subscriptions
- Gets user subscriptions
- Returns: 200 OK

### POST /api/subscriptions/{id}/pause
- Pauses subscription
- Returns: 200 OK

## Data Persistence

### Subscriptions Table
```sql
CREATE TABLE Subscriptions (
    SubscriptionId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    UserId UNIQUEIDENTIFIER NOT NULL,
    ServiceName NVARCHAR(200) NOT NULL,
    Cost DECIMAL(10,2) NOT NULL,
    BillingFrequency NVARCHAR(20) NOT NULL,
    StartDate DATE NOT NULL,
    NextRenewalDate DATE NOT NULL,
    CategoryId UNIQUEIDENTIFIER NOT NULL,
    PaymentMethodId UNIQUEIDENTIFIER NOT NULL,
    Status NVARCHAR(20) NOT NULL DEFAULT 'Active',
    TrialEndDate DATE NULL,
    CancellationDeadline DATE NULL,
    Notes NVARCHAR(1000) NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),

    CONSTRAINT FK_Subscriptions_Users FOREIGN KEY (UserId) REFERENCES Users(UserId),
    INDEX IX_Subscriptions_UserId (UserId),
    INDEX IX_Subscriptions_Status (Status),
    INDEX IX_Subscriptions_NextRenewalDate (NextRenewalDate)
);
```
