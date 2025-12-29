# Reminders - Backend Requirements

## Overview
The Reminders feature sends timely notifications to users about upcoming bills, overdue payments, and early payment discount opportunities through multiple channels (email, SMS, push notifications).

## Domain Model

### Reminder Aggregate
- **ReminderId**: Unique identifier (Guid)
- **BillId**: Associated bill (Guid)
- **UserId**: Owner (Guid)
- **ReminderType**: Type (enum: UpcomingPayment, DueSoon, Overdue, PaymentConfirmation, EarlyPaymentDiscount)
- **DaysBefore**: Days before due date to send reminder (int)
- **NotificationChannels**: List<NotificationChannel> (Email, SMS, Push)
- **IsActive**: Active status (bool)
- **LastSentAt**: Last sent timestamp (DateTime, nullable)
- **NextScheduledAt**: Next scheduled send time (DateTime, nullable)
- **CreatedAt**: Creation timestamp (DateTime)
- **UpdatedAt**: Last modification timestamp (DateTime)

### NotificationLog Entity
- **NotificationLogId**: Unique identifier (Guid)
- **ReminderId**: Associated reminder (Guid)
- **Channel**: NotificationChannel
- **SentAt**: Sent timestamp (DateTime)
- **Status**: DeliveryStatus (enum: Sent, Delivered, Failed, Bounced)
- **RecipientAddress**: Email or phone number (string)
- **MessageContent**: Notification content (string)
- **ErrorMessage**: Error details if failed (string, nullable)

## Commands

### CreateReminderCommand
- Creates a new reminder for a bill
- Validates BillId exists
- Validates notification channels
- Raises **ReminderCreated** event

### UpdateReminderCommand
- Updates reminder configuration
- Validates reminder exists
- Updates schedule calculations

### DeleteReminderCommand
- Deletes a reminder
- Marks as inactive

### SendReminderCommand
- Sends reminder through specified channels
- Updates LastSentAt and NextScheduledAt
- Raises **PaymentReminderSent** or **LatePaymentWarning** event
- Logs notification delivery

## Queries

### GetRemindersByBillIdQuery
- Returns all reminders for a bill

### GetRemindersByUserIdQuery
- Returns all user's reminders
- Supports filtering by active status

### GetDueRemindersQuery
- Returns reminders that need to be sent
- Used by background job

## Domain Events

### PaymentReminderSent
```csharp
public class PaymentReminderSent : DomainEvent
{
    public Guid ReminderId { get; set; }
    public Guid BillId { get; set; }
    public Guid UserId { get; set; }
    public string Payee { get; set; }
    public decimal Amount { get; set; }
    public DateTime DueDate { get; set; }
    public List<string> Channels { get; set; }
    public DateTime SentAt { get; set; }
    public DateTime OccurredAt { get; set; }
}
```

### LatePaymentWarning
```csharp
public class LatePaymentWarning : DomainEvent
{
    public Guid BillId { get; set; }
    public Guid UserId { get; set; }
    public string Payee { get; set; }
    public decimal Amount { get; set; }
    public DateTime DueDate { get; set; }
    public int DaysOverdue { get; set; }
    public DateTime OccurredAt { get; set; }
}
```

### EarlyPaymentDiscountAvailable
```csharp
public class EarlyPaymentDiscountAvailable : DomainEvent
{
    public Guid BillId { get; set; }
    public Guid UserId { get; set; }
    public string Payee { get; set; }
    public decimal BillAmount { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal DiscountPercentage { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime DiscountDeadline { get; set; }
    public int DaysUntilDeadline { get; set; }
    public string DiscountTerms { get; set; }
    public DateTime OccurredAt { get; set; }
}
```

## API Endpoints

### POST /api/reminders
- Creates a new reminder
- Returns: 201 Created

### PUT /api/reminders/{reminderId}
- Updates reminder
- Returns: 200 OK

### DELETE /api/reminders/{reminderId}
- Deletes reminder
- Returns: 204 No Content

### GET /api/reminders/bill/{billId}
- Gets reminders for bill
- Returns: 200 OK

### GET /api/reminders
- Gets user's reminders
- Returns: 200 OK

### POST /api/reminders/{reminderId}/send
- Sends reminder immediately
- Returns: 200 OK

## Business Rules

1. **Reminder Timing**: Can be set 1-30 days before due date
2. **Multiple Channels**: Support email, SMS, and push notifications
3. **Overdue Reminders**: Sent daily for overdue bills
4. **Delivery Tracking**: Log all notification attempts
5. **User Preferences**: Honor user notification preferences
6. **Quiet Hours**: Don't send notifications between 10 PM - 8 AM (configurable)
7. **Rate Limiting**: Max 10 notifications per user per day

## Background Jobs

### ReminderProcessorJob
- **Schedule**: Runs every hour
- **Purpose**: Send due reminders
- **Logic**:
  1. Query reminders where NextScheduledAt <= Now
  2. Check bill status and due date
  3. Send through configured channels
  4. Update reminder state
  5. Raise domain events

### OverdueReminderJob
- **Schedule**: Runs daily at 9 AM
- **Purpose**: Send overdue payment warnings
- **Logic**:
  1. Query overdue bills without payments
  2. Send late payment warnings
  3. Log delivery status

### EarlyPaymentDiscountJob
- **Schedule**: Runs daily at 8 AM
- **Purpose**: Detect and notify users of early payment discount opportunities
- **Logic**:
  1. Query bills with discount terms configured
  2. Check if current date is within discount eligibility window
  3. Calculate potential savings
  4. Send early payment discount notifications
  5. Raise **EarlyPaymentDiscountAvailable** event
  6. Track notification delivery

## Notification Service Integration

### INotificationService Interface
```csharp
public interface INotificationService
{
    Task<NotificationResult> SendEmailAsync(string to, string subject, string body);
    Task<NotificationResult> SendSMSAsync(string phoneNumber, string message);
    Task<NotificationResult> SendPushNotificationAsync(string userId, string title, string body);
}
```

## Data Persistence

### Reminders Table
```sql
CREATE TABLE Reminders (
    ReminderId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    BillId UNIQUEIDENTIFIER NOT NULL,
    UserId UNIQUEIDENTIFIER NOT NULL,
    ReminderType NVARCHAR(50) NOT NULL,
    DaysBefore INT NOT NULL,
    NotificationChannels NVARCHAR(MAX) NOT NULL, -- JSON array
    IsActive BIT NOT NULL DEFAULT 1,
    LastSentAt DATETIME2 NULL,
    NextScheduledAt DATETIME2 NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),

    CONSTRAINT FK_Reminders_Bills FOREIGN KEY (BillId) REFERENCES Bills(BillId),
    CONSTRAINT FK_Reminders_Users FOREIGN KEY (UserId) REFERENCES Users(UserId),
    INDEX IX_Reminders_NextScheduledAt (NextScheduledAt),
    INDEX IX_Reminders_BillId (BillId),
    INDEX IX_Reminders_UserId (UserId)
);
```

### NotificationLogs Table
```sql
CREATE TABLE NotificationLogs (
    NotificationLogId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    ReminderId UNIQUEIDENTIFIER NOT NULL,
    Channel NVARCHAR(50) NOT NULL,
    SentAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    Status NVARCHAR(50) NOT NULL,
    RecipientAddress NVARCHAR(200) NOT NULL,
    MessageContent NVARCHAR(MAX) NOT NULL,
    ErrorMessage NVARCHAR(MAX) NULL,

    CONSTRAINT FK_NotificationLogs_Reminders FOREIGN KEY (ReminderId) REFERENCES Reminders(ReminderId),
    INDEX IX_NotificationLogs_ReminderId (ReminderId),
    INDEX IX_NotificationLogs_SentAt (SentAt)
);
```
