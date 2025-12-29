# Backend Requirements - Bedtime Management

## API Endpoints

### POST /api/bedtime/reminders/configure
Set bedtime reminder preferences
- **Request Body**: userId, reminderTime, targetBedtime, enabled

### POST /api/bedtime/reminders/send
Send bedtime reminder (scheduled job)
- **Events**: `BedtimeReminderSent`

### POST /api/bedtime/streaks/check
Check bedtime streak
- **Events**: `ConsistentBedtimeStreakAchieved`, `LateBedtimeAlert`

## Domain Models
### BedtimePreference: Id, UserId, TargetBedtime, ReminderOffset, Enabled
### BedtimeStreak: Id, UserId, StreakLength, StartDate, IsActive

## Business Logic
- Send reminders 30-60 min before target bedtime
- Streak: within 15 min of target for consecutive days
- Late bedtime: 1+ hour past target
- Calculate sleep opportunity loss

## Events
BedtimeReminderSent, ConsistentBedtimeStreakAchieved, LateBedtimeAlert

## Database Schema
BedtimePreferences table, BedtimeStreaks table
