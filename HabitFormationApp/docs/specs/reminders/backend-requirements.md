# Reminders - Backend Requirements

## Overview
The Reminders feature sends timely notifications to users to help them complete their habits, with intelligent scheduling and response tracking.

## Domain Events
- **ReminderScheduled**: When a reminder is created for a habit
- **ReminderActedUpon**: When user completes habit after reminder
- **ReminderIgnored**: When reminder expires without action

## Aggregates

### Reminder
**Properties:**
- `Id` (Guid)
- `HabitId` (Guid)
- `UserId` (Guid)
- `ScheduledFor` (DateTime)
- `Message` (string)
- `Channel` (ReminderChannel): Push, Email, SMS
- `Status` (ReminderStatus): Pending, Sent, ActedUpon, Ignored, Expired
- `SentAt` (DateTime?)
- `ActedUponAt` (DateTime?)
- `ResponseTimeMinutes` (int?)

### ReminderSchedule
**Properties:**
- `Id` (Guid)
- `HabitId` (Guid)
- `IsEnabled` (bool)
- `TimeOfDay` (TimeSpan)
- `DaysOfWeek` (int[])
- `AdvanceMinutes` (int): Reminder before scheduled time
- `ReminderMessage` (string?)
- `Channel` (ReminderChannel)

## Commands

### ScheduleReminderCommand
```csharp
public record ScheduleReminderCommand(
    Guid HabitId,
    Guid UserId,
    DateTime ScheduledFor,
    string Message,
    ReminderChannel Channel
);
```

### UpdateReminderScheduleCommand
```csharp
public record UpdateReminderScheduleCommand(
    Guid HabitId,
    bool IsEnabled,
    TimeSpan TimeOfDay,
    int[] DaysOfWeek,
    int AdvanceMinutes,
    ReminderChannel Channel
);
```

### MarkReminderActedUponCommand
```csharp
public record MarkReminderActedUponCommand(
    Guid ReminderId,
    DateTime ActedAt
);
```

## API Endpoints

### GET /api/reminders/habit/{habitId}/schedule
Get reminder schedule

### PUT /api/reminders/habit/{habitId}/schedule
Update reminder schedule

### GET /api/reminders/upcoming
Get upcoming reminders

### POST /api/reminders/{id}/snooze
Snooze a reminder

### GET /api/reminders/effectiveness
Get reminder effectiveness metrics

## Reminder Types

### Time-based Reminders
- Scheduled for specific time
- Daily/weekly patterns
- Advance reminders (15min, 30min before)

### Context-based Reminders
- Location-based (arriving home, at gym)
- Time-of-day optimal (AI-learned)
- Weather-based (outdoor habits)

### Smart Reminders
- Adaptive timing based on past response
- Multi-reminder strategies (escalating)
- Quiet hours respect

## Notification Channels

### Push Notifications
- Rich notifications with quick actions
- "Complete now" button
- "Snooze 15min" button
- Deep link to habit

### Email
- Daily summary reminders
- Weekly preparation emails
- Formatted HTML templates

### SMS (Premium)
- Simple text reminders
- Reply with "DONE" to complete

## Intelligence Features

### Response Time Tracking
- Track how long after reminder user completes
- Identify best reminder times
- Adjust future reminders

### Effectiveness Scoring
```
Effectiveness = (
  ActedUponCount / TotalSent * 50 +
  AverageResponseTime (inverse) * 30 +
  SnoozeRate (inverse) * 20
) / 100
```

### Adaptive Scheduling
- Learn user's most responsive times
- Reduce reminders for consistent habits
- Increase for struggling habits

## Database Schema

```sql
CREATE TABLE Reminders (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    HabitId UNIQUEIDENTIFIER NOT NULL,
    UserId UNIQUEIDENTIFIER NOT NULL,
    ScheduledFor DATETIME2 NOT NULL,
    Message NVARCHAR(200) NOT NULL,
    Channel NVARCHAR(20) NOT NULL,
    Status NVARCHAR(20) NOT NULL,
    SentAt DATETIME2 NULL,
    ActedUponAt DATETIME2 NULL,
    ResponseTimeMinutes INT NULL,
    CreatedAt DATETIME2 NOT NULL,
    CONSTRAINT FK_Reminders_Habits FOREIGN KEY (HabitId) REFERENCES Habits(Id)
);

CREATE TABLE ReminderSchedules (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    HabitId UNIQUEIDENTIFIER NOT NULL,
    IsEnabled BIT NOT NULL DEFAULT 1,
    TimeOfDay TIME NOT NULL,
    DaysOfWeek NVARCHAR(50) NOT NULL, -- JSON array
    AdvanceMinutes INT NOT NULL DEFAULT 0,
    ReminderMessage NVARCHAR(200) NULL,
    Channel NVARCHAR(20) NOT NULL,
    CreatedAt DATETIME2 NOT NULL,
    UpdatedAt DATETIME2 NOT NULL,
    CONSTRAINT FK_ReminderSchedules_Habits FOREIGN KEY (HabitId) REFERENCES Habits(Id)
);

CREATE INDEX IX_Reminders_ScheduledFor ON Reminders(ScheduledFor);
CREATE INDEX IX_Reminders_Status ON Reminders(Status);
```

## Integration Points
- Completion service: Mark reminder as acted upon
- Notification service: Send actual notifications
- Analytics service: Track reminder effectiveness
- AI service: Optimize reminder timing

## Background Jobs
- Every 5 minutes: Process pending reminders
- Daily: Generate next day's reminders
- Weekly: Optimize reminder schedules
- Monthly: Cleanup expired reminders

## Business Rules
1. Max 10 reminders per day per user
2. Respect quiet hours (10 PM - 7 AM default)
3. Snooze limit: 3 times per reminder
4. Auto-expire after 24 hours
5. Don't remind for already completed habits

## Testing Requirements
- Reminder scheduling logic tests
- Timezone handling tests
- Response time calculation tests
- Effectiveness scoring tests
- Integration with notification services
