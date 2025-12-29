# Reminder System - Backend Requirements

## API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | /api/reminders | List user reminders |
| POST | /api/reminders | Create custom reminder |
| PUT | /api/reminders/{id} | Update reminder |
| DELETE | /api/reminders/{id} | Delete reminder |
| PUT | /api/reminders/{id}/acknowledge | Acknowledge reminder |
| GET | /api/reminders/preferences | Get notification preferences |
| PUT | /api/reminders/preferences | Update preferences |

## Domain Events

| Event | Trigger | Key Data |
|-------|---------|----------|
| ScreeningReminderScheduled | Due date calculated | reminderId, screeningType, reminderDate, channels |
| ReminderSent | Scheduled time arrives | reminderId, userId, channels, urgencyLevel |
| ReminderAcknowledged | User interacts | reminderId, acknowledgmentTime, action |
| EscalationReminderTriggered | Critically overdue | screeningType, daysOverdue, escalationLevel |

## Database Schema

```sql
CREATE TABLE Reminders (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    ScreeningTypeId UNIQUEIDENTIFIER,
    ScreeningId UNIQUEIDENTIFIER,
    ReminderType NVARCHAR(50), -- Upcoming, Overdue, Custom
    ScheduledDate DATETIME2 NOT NULL,
    SentDate DATETIME2,
    AcknowledgedDate DATETIME2,
    Status NVARCHAR(50), -- Pending, Sent, Acknowledged, Dismissed
    UrgencyLevel INT DEFAULT 1,
    CreatedAt DATETIME2 NOT NULL
);

CREATE TABLE ReminderPreferences (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    EmailEnabled BIT DEFAULT 1,
    SmsEnabled BIT DEFAULT 0,
    PushEnabled BIT DEFAULT 1,
    AdvanceNoticeDays INT DEFAULT 7,
    QuietHoursStart TIME,
    QuietHoursEnd TIME,
    UpdatedAt DATETIME2 NOT NULL
);
```

## Business Rules

1. Reminders respect user quiet hours
2. Escalation increases reminder frequency
3. Acknowledged reminders stop follow-ups
4. Multiple channels can be enabled simultaneously
