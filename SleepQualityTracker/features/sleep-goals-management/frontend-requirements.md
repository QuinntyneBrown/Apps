# Frontend Requirements - Sleep Goals Management

## Components

### GoalSettingForm
- Target sleep hours input (slider or number input)
- Target bedtime time picker
- Target wake time time picker
- Calculated sleep window display
- Save button
- Visual preview of sleep schedule

### GoalProgressCard
- Current streak display (large, prominent)
- Progress bar toward goal
- Last night's achievement status
- Weekly success rate percentage
- Motivational message

### GoalAchievementHistory
- Calendar view with color-coded days (met/missed)
- List view with achievement details
- Filter by date range
- Statistics: total met, total missed, success rate

### StreakTracker
- Current streak counter with icon
- Best streak (all-time record)
- Streak type breakdown (duration, bedtime, wake time)
- Progress toward milestones (7 days, 30 days, etc.)

### BedtimeReminderSettings
- Enable/disable reminders toggle
- Reminder time offset (30, 45, 60 minutes before bedtime)
- Custom reminder message
- Do-not-disturb schedule

## Pages

### Goal Setup Page (/goals/setup)
- Goal setting form
- Current goal display (if exists)
- Historical goal changes
- Recommendations based on past sleep

### Goal Dashboard (/goals/dashboard)
- Goal progress overview
- Streak tracker
- Achievement calendar
- Quick stats

## Interactions

### Set New Goal
1. Navigate to goal setup
2. Fill in target hours and times
3. Preview schedule
4. Save goal
5. Confirmation message

### View Achievement Status
1. Dashboard shows last night's result
2. Click for details
3. See breakdown of why goal met/missed
4. View suggestions for improvement

## State Management
- Current goal settings
- Active streaks
- Recent achievements
- Reminder preferences

## Notifications
- Bedtime reminder
- Goal achievement celebration
- Streak milestone reached
- Consistency achievement
