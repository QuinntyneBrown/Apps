# Domain Events - Habit Formation App

## Overview
This document defines the domain events tracked in the Habit Formation App. These events capture significant business occurrences related to habit creation, completion tracking, streak management, accountability partnerships, and behavioral change milestones.

## Events

### HabitEvents

#### HabitCreated
- **Description**: User has established a new habit to track
- **Triggered When**: User defines a new habit with goals and schedule
- **Key Data**: Habit ID, user ID, habit name, description, frequency, target times per day/week, start date, category, motivation notes
- **Consumers**: Habit tracker, reminder scheduler, analytics service, goal manager

#### HabitModified
- **Description**: User has updated habit parameters or goals
- **Triggered When**: User changes habit frequency, schedule, or other settings
- **Key Data**: Habit ID, previous settings, new settings, modification date, reason for change
- **Consumers**: Habit tracker, reminder updater, analytics recalculation, progress adjuster

#### HabitArchived
- **Description**: User has stopped actively tracking a habit
- **Triggered When**: User marks habit as complete, abandoned, or no longer relevant
- **Key Data**: Habit ID, archive date, archive reason, final statistics, total days tracked, success rate
- **Consumers**: Archive service, analytics, success rate calculator, habit library

#### HabitReactivated
- **Description**: Previously archived habit has been restarted
- **Triggered When**: User chooses to resume tracking an old habit
- **Key Data**: Habit ID, reactivation date, original start date, archive duration, historical stats, new goals
- **Consumers**: Habit tracker, reminder scheduler, progress tracker, analytics

### CompletionEvents

#### HabitCompletionLogged
- **Description**: User has marked a habit as completed for a specific occurrence
- **Triggered When**: User checks off habit completion
- **Key Data**: Completion ID, habit ID, user ID, completion timestamp, notes, mood/energy level, location (optional)
- **Consumers**: Streak calculator, progress tracker, analytics, achievement system, completion history

#### HabitCompletionUndone
- **Description**: User has reversed a habit completion entry
- **Triggered When**: User unchecks or removes erroneous completion
- **Key Data**: Completion ID, habit ID, original completion time, undo timestamp, undo reason
- **Consumers**: Streak recalculator, analytics updater, audit log, accuracy tracker

#### LateCompletionLogged
- **Description**: User has completed habit after scheduled time window
- **Triggered When**: User marks completion outside optimal time range but within grace period
- **Key Data**: Habit ID, scheduled time, actual completion time, delay duration, user notes
- **Consumers**: Compliance tracker, schedule optimizer, reminder effectiveness analyzer

### StreakEvents

#### StreakStarted
- **Description**: User has begun a new consecutive completion streak
- **Triggered When**: First completion after break or initial habit creation
- **Key Data**: Habit ID, user ID, streak start date, previous streak length (if applicable)
- **Consumers**: Streak tracker, motivation system, progress dashboard

#### StreakMilestoneReached
- **Description**: User has achieved significant streak length milestone
- **Triggered When**: Consecutive completions reach milestone (7, 21, 30, 66, 100 days)
- **Key Data**: Habit ID, user ID, streak length, milestone type, start date, achievement date, habit category
- **Consumers**: Achievement system, notification service, social sharing, badge award, motivation engine

#### StreakBroken
- **Description**: User's consecutive completion streak has ended
- **Triggered When**: User misses habit completion outside grace period
- **Key Data**: Habit ID, user ID, broken streak length, break date, longest streak, reasons (if provided)
- **Consumers**: Streak tracker, analytics, motivation service, recovery recommendations, pattern analyzer

#### StreakRecovered
- **Description**: User has resumed habit after breaking streak
- **Triggered When**: First completion after missing day(s)
- **Key Data**: Habit ID, user ID, days broken, recovery date, new streak start, recovery speed
- **Consumers**: Resilience tracker, motivation system, pattern analyzer, engagement monitor

### AccountabilityEvents

#### AccountabilityPartnerAdded
- **Description**: User has connected with accountability partner
- **Triggered When**: User establishes accountability relationship with another user
- **Key Data**: Partnership ID, user ID, partner ID, shared habits, connection date, permission levels, check-in frequency
- **Consumers**: Partner notification service, shared progress tracker, permission manager, social features

#### AccountabilityCheckInCompleted
- **Description**: Users have completed scheduled accountability check-in
- **Triggered When**: Partners review and discuss progress
- **Key Data**: Check-in ID, partnership ID, participants, check-in date, habits reviewed, notes, encouragement given
- **Consumers**: Engagement tracker, relationship strength analyzer, effectiveness monitor, motivation system

#### PartnerEncouragementSent
- **Description**: Accountability partner has sent encouragement or support
- **Triggered When**: Partner sends motivational message or celebrates success
- **Key Data**: Message ID, sender ID, recipient ID, message content, related habit, timestamp, message type
- **Consumers**: Notification service, engagement tracker, social analytics, motivation monitor

#### PartnerAlertTriggered
- **Description**: Partner has been notified of missed habit or broken streak
- **Triggered When**: User misses habit and partner has alert permissions
- **Key Data**: Alert ID, user ID, partner ID, habit ID, alert reason, missed date, partner action options
- **Consumers**: Partner notification, intervention trigger, support request, accountability reinforcement

### MotivationEvents

#### MilestoneAchieved
- **Description**: User has reached significant habit formation milestone
- **Triggered When**: User completes major achievement (e.g., 21-day habit formation period)
- **Key Data**: Milestone ID, habit ID, user ID, milestone type, achievement date, progress metrics, celebration content
- **Consumers**: Achievement system, badge award, notification service, social sharing, analytics

#### MotivationDipDetected
- **Description**: System has identified declining engagement or completion rate
- **Triggered When**: Analytics detect pattern of missed completions or reduced consistency
- **Key Data**: Habit ID, user ID, detection date, completion rate decline, pattern description, suggested interventions
- **Consumers**: Intervention system, motivational content delivery, reminder adjustment, support resources

#### PersonalBestSet
- **Description**: User has achieved new personal record for habit consistency
- **Triggered When**: User surpasses previous best performance metric
- **Key Data**: Habit ID, user ID, record type, previous best, new best, achievement date
- **Consumers**: Achievement system, notification service, progress celebration, analytics

### ReminderEvents

#### ReminderScheduled
- **Description**: Reminder has been set for habit completion
- **Triggered When**: User configures reminder times or system auto-generates reminders
- **Key Data**: Reminder ID, habit ID, reminder times, days of week, reminder type, custom message, advance notice
- **Consumers**: Notification scheduler, reminder delivery service, preference manager

#### ReminderActedUpon
- **Description**: User completed habit in response to reminder
- **Triggered When**: User logs completion within defined window after reminder
- **Key Data**: Reminder ID, habit ID, time to completion, action type, reminder effectiveness
- **Consumers**: Reminder optimization, timing analyzer, engagement tracker, personalization engine

#### ReminderIgnored
- **Description**: User did not respond to habit reminder
- **Triggered When**: Time window passes after reminder without completion
- **Key Data**: Reminder ID, habit ID, sent time, window expired time, ignore count, time of day
- **Consumers**: Reminder effectiveness analyzer, schedule optimizer, engagement monitor, intervention trigger

### AnalyticsEvents

#### WeeklyProgressReportGenerated
- **Description**: System has compiled weekly habit performance summary
- **Triggered When**: Week ends with sufficient tracking data
- **Key Data**: User ID, week start date, habits tracked, completion rates, streaks, milestones, insights, recommendations
- **Consumers**: Report delivery service, progress dashboard, trend analyzer, goal adjuster

#### HabitPatternIdentified
- **Description**: System has discovered behavioral pattern in habit completion
- **Triggered When**: Analytics detect consistent patterns (time of day, day of week, environmental factors)
- **Key Data**: Habit ID, user ID, pattern type, pattern description, statistical confidence, actionable insights
- **Consumers**: Insight delivery, schedule optimizer, personalization engine, success factor analyzer

#### SuccessFactorAnalyzed
- **Description**: System has identified factors contributing to habit success or failure
- **Triggered When**: Sufficient data exists to correlate completion with contextual factors
- **Key Data**: Habit ID, success factors, failure factors, correlation strength, sample size, recommendations
- **Consumers**: Personalization service, coaching system, optimization recommendations, user insights
