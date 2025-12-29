# Reminder System - Frontend Requirements

## Components

### ReminderPreferencesForm
- Notification channel toggles (email, SMS, push)
- Advance notice days slider
- Quiet hours time pickers
- Save/cancel buttons

### ReminderNotification
- Screening type and due date
- Urgency indicator
- Quick action buttons (schedule, snooze, dismiss)
- Acknowledge interaction

### ReminderHistory
- List of sent reminders
- Status indicators
- Filter by status/type

## State Management

```typescript
interface ReminderState {
  reminders: Reminder[];
  preferences: ReminderPreferences;
  unacknowledgedCount: number;
}
```
