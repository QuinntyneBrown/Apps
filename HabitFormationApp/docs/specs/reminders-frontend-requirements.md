# Reminders - Frontend Requirements

## UI Components

### 1. Reminder Settings (Per Habit)
- Toggle reminder on/off
- Time picker
- Days of week selector
- Advance reminder slider (0-60 mins)
- Channel selection (push/email/SMS)
- Custom message input
- Preview notification button

### 2. Upcoming Reminders View
- List of next 24 hours reminders
- Time until reminder
- Quick snooze button
- Quick complete button
- Edit/delete options

### 3. Reminder Notification (Push)
- Habit name and icon
- Custom message
- Quick action buttons:
  - "Complete Now"
  - "Snooze 15min"
  - "Dismiss"
- Deep link to habit

### 4. Quiet Hours Settings
- Start time picker
- End time picker
- Override for important reminders toggle
- Weekend different hours option

### 5. Effectiveness Dashboard
- Response rate chart
- Best reminder times
- Snooze frequency
- Recommendations for optimization

## State Management
```typescript
interface ReminderState {
  schedules: ReminderSchedule[];
  upcomingReminders: Reminder[];
  quietHours: QuietHours;
  effectiveness: EffectivenessMetrics;
}
```

## Notification UI
- Rich notification with actions
- Haptic feedback on mobile
- Sound (customizable)
- Badge count
- Notification history

## Smart Features
- "Suggest best time" button
- "Learn from my behavior" toggle
- Reminder fatigue detection
- Auto-adjust based on completion patterns
