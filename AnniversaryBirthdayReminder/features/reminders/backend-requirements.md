# Reminders - Backend Requirements

## Domain Model
### Reminder Aggregate
- **ReminderId**: Guid
- **DateId**: Reference to ImportantDate (Guid)
- **ScheduledTime**: When to send (DateTime)
- **AdvanceNoticeDays**: Days before event (int)
- **DeliveryChannel**: Email, SMS, Push (enum)
- **Status**: Scheduled, Sent, Dismissed, Snoozed (enum)
- **SentAt**: DateTime nullable

## Commands
- ScheduleReminderCommand
- SendReminderCommand
- DismissReminderCommand
- SnoozeReminderCommand

## Background Jobs
- Daily reminder scheduling scan
- Reminder delivery processor
