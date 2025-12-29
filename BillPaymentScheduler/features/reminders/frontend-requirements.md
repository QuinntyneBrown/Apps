# Reminders - Frontend Requirements

## Overview
User interface for managing bill payment reminders and notification preferences.

## User Stories
1. Set up reminders for bills with customizable timing
2. Choose notification channels (email, SMS, push)
3. View reminder history and delivery status
4. Edit or delete existing reminders
5. Configure global notification preferences
6. See which reminders are active
7. Test reminders before saving

## Pages/Views

### 1. Reminders Dashboard (`/reminders`)
- List of all active reminders
- Reminder summary (Total active, Sent today, Failed deliveries)
- Quick actions (Add reminder, Configure preferences)
- Filter by bill, channel, or status

### 2. Create/Edit Reminder Form
- Select bill (dropdown)
- Reminder type (Upcoming, Due Soon, Overdue)
- Days before due date (slider: 1-30 days)
- Notification channels (checkboxes: Email, SMS, Push)
- Preview message
- Test reminder button

### 3. Reminder Details View
- Reminder configuration
- Delivery history
- Success/failure statistics
- Edit and delete actions

### 4. Notification Preferences (`/settings/notifications`)
- Global notification settings
- Quiet hours configuration
- Channel preferences
- Opt-in/opt-out for reminder types
- Email and phone number management

### 5. Notification History
- All sent notifications
- Filter by date, channel, status
- Resend failed notifications
- Export history

## UI Components

### ReminderCard
- Bill name and amount
- Reminder schedule (X days before)
- Enabled channels (icons)
- Toggle active/inactive
- Edit/delete actions

### NotificationChannelSelector
- Checkbox group for channels
- Verification status indicators
- Add/verify channel options

### ReminderTimeline
- Visual timeline showing when reminders will be sent
- Relative to bill due date
- Color-coded by delivery status

## State Management

```typescript
interface ReminderState {
  reminders: Reminder[];
  notificationLogs: NotificationLog[];
  preferences: NotificationPreferences;
  loading: boolean;
  error: string | null;
}

interface Reminder {
  reminderId: string;
  billId: string;
  billName: string;
  reminderType: ReminderType;
  daysBefore: number;
  notificationChannels: NotificationChannel[];
  isActive: boolean;
  lastSentAt?: Date;
  nextScheduledAt?: Date;
}

enum NotificationChannel {
  Email = 'Email',
  SMS = 'SMS',
  Push = 'Push'
}

enum ReminderType {
  UpcomingPayment = 'UpcomingPayment',
  DueSoon = 'DueSoon',
  Overdue = 'Overdue'
}
```

## API Integration

```typescript
class ReminderService {
  async getReminders(userId: string): Promise<Reminder[]>;
  async createReminder(data: CreateReminderRequest): Promise<Reminder>;
  async updateReminder(id: string, updates: UpdateReminderRequest): Promise<Reminder>;
  async deleteReminder(id: string): Promise<void>;
  async sendTestReminder(id: string): Promise<void>;
  async getNotificationHistory(params: HistoryParams): Promise<NotificationLog[]>;
}
```

## Notifications

### Success
- "Reminder created successfully"
- "Test reminder sent to your email"
- "Reminder deleted"

### Errors
- "Failed to send reminder. Please check your notification settings."
- "Email address not verified. Please verify to receive email reminders."
