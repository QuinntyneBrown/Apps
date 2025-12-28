# Domain Events - Anniversary & Birthday Reminder

## Overview
This application tracks important personal dates and manages reminders for anniversaries, birthdays, and other significant occasions. Domain events capture the lifecycle of important dates, reminder notifications, gift planning, and user interactions with celebration tracking.

## Events

### ImportantDateEvents

#### ImportantDateCreated
- **Description**: A new important date (birthday, anniversary, or special occasion) has been added to the system
- **Triggered When**: User adds a new date to track
- **Key Data**: Date ID, date type (birthday/anniversary/other), person/event name, date value, recurrence pattern, user ID, creation timestamp
- **Consumers**: Reminder scheduling service, notification system, analytics dashboard

#### ImportantDateUpdated
- **Description**: Details of an existing important date have been modified
- **Triggered When**: User edits date information, changes recurrence, or updates person details
- **Key Data**: Date ID, updated fields, previous values, new values, update timestamp
- **Consumers**: Reminder scheduling service (to reschedule notifications), audit log

#### ImportantDateDeleted
- **Description**: An important date has been removed from tracking
- **Triggered When**: User deletes a date or removes a person from tracking
- **Key Data**: Date ID, date type, person/event name, deletion reason, user ID, timestamp
- **Consumers**: Reminder scheduling service (to cancel pending reminders), analytics service

### ReminderEvents

#### ReminderScheduled
- **Description**: A reminder has been scheduled for an upcoming important date
- **Triggered When**: System automatically schedules reminders based on user preferences (e.g., 1 week before, 1 day before)
- **Key Data**: Reminder ID, date ID, scheduled delivery time, reminder type, advance notice period, delivery channels
- **Consumers**: Notification service, reminder queue processor

#### ReminderSent
- **Description**: A reminder notification has been successfully delivered to the user
- **Triggered When**: Scheduled reminder time arrives and notification is sent
- **Key Data**: Reminder ID, date ID, delivery timestamp, delivery channel (email/push/SMS), delivery status
- **Consumers**: Analytics service, user engagement tracker, delivery confirmation system

#### ReminderDismissed
- **Description**: User has acknowledged and dismissed a reminder
- **Triggered When**: User marks reminder as seen or dismisses notification
- **Key Data**: Reminder ID, date ID, dismissal timestamp, user action taken
- **Consumers**: Notification cleanup service, engagement analytics

#### ReminderSnoozed
- **Description**: User has postponed a reminder to be shown again later
- **Triggered When**: User chooses to snooze a reminder
- **Key Data**: Reminder ID, original scheduled time, new scheduled time, snooze duration, timestamp
- **Consumers**: Reminder scheduling service, user behavior analytics

### GiftEvents

#### GiftIdeaAdded
- **Description**: A gift idea has been associated with an important date
- **Triggered When**: User adds a potential gift idea for an upcoming occasion
- **Key Data**: Gift ID, date ID, gift description, estimated price, purchase URL, priority level, timestamp
- **Consumers**: Gift planning dashboard, budget tracker, shopping list aggregator

#### GiftPurchased
- **Description**: A planned gift has been marked as purchased
- **Triggered When**: User indicates they have purchased a gift
- **Key Data**: Gift ID, date ID, purchase date, actual price, purchase location, timestamp
- **Consumers**: Budget tracker, gift tracking dashboard, reminder service (to stop gift reminder notifications)

#### GiftDelivered
- **Description**: A gift has been given to the recipient
- **Triggered When**: User marks a gift as delivered/given on the special occasion
- **Key Data**: Gift ID, date ID, delivery date, recipient reaction notes, timestamp
- **Consumers**: Analytics service, memory archive, celebration completion tracker

### CelebrationEvents

#### CelebrationCompleted
- **Description**: An important date has passed and the celebration has been marked as completed
- **Triggered When**: Date passes and user marks the occasion as celebrated
- **Key Data**: Date ID, celebration date, celebration notes, photos attached, attendees, satisfaction rating, timestamp
- **Consumers**: Memory archive, relationship analytics, year-over-year comparison service

#### CelebrationSkipped
- **Description**: An important date passed without celebration
- **Triggered When**: Date passes without user marking it as celebrated
- **Key Data**: Date ID, date that passed, reason for skipping (if provided), timestamp
- **Consumers**: Analytics service, future reminder adjustment system

### NotificationPreferenceEvents

#### NotificationPreferenceUpdated
- **Description**: User has changed their notification preferences for reminders
- **Triggered When**: User modifies settings for when and how they want to be reminded
- **Key Data**: User ID, preference type, old settings, new settings (advance notice periods, delivery channels, quiet hours), timestamp
- **Consumers**: Reminder scheduling service, notification delivery system

### RelationshipEvents

#### PersonAdded
- **Description**: A new person has been added to the user's important people list
- **Triggered When**: User adds a new person to track celebrations for
- **Key Data**: Person ID, name, relationship type, contact information, associated dates, timestamp
- **Consumers**: Relationship management system, analytics dashboard, date suggestion service
