# Domain Events - Hydration Tracker

## Overview
This document defines the domain events tracked in the Hydration Tracker application. These events capture significant business occurrences related to water intake logging, hydration goals, reminders, and health insights.

## Events

### IntakeEvents

#### WaterIntakeLogged
- **Description**: User has recorded a water consumption entry
- **Triggered When**: User logs drinking water or other hydrating beverage
- **Key Data**: Entry ID, user ID, amount (ml/oz), beverage type, timestamp, entry method (manual/quick add)
- **Consumers**: Daily progress calculator, goal tracking service, analytics engine, achievement system

#### IntakeEntryModified
- **Description**: Previously logged water intake has been edited or corrected
- **Triggered When**: User updates an existing intake entry
- **Key Data**: Entry ID, original amount, new amount, modification timestamp, modification reason
- **Consumers**: Audit log, analytics recalculation, daily total updater

#### IntakeEntryDeleted
- **Description**: A water intake entry has been removed
- **Triggered When**: User deletes an erroneous or duplicate entry
- **Key Data**: Entry ID, deleted amount, deletion timestamp, original entry timestamp
- **Consumers**: Audit log, daily total recalculation, analytics service

### GoalEvents

#### DailyGoalSet
- **Description**: User has established or updated their daily hydration goal
- **Triggered When**: User sets target water intake amount for the day
- **Key Data**: User ID, goal amount, effective date, calculation method (manual/auto), factors considered (weight, activity, climate)
- **Consumers**: Progress tracker, reminder scheduler, personalization engine

#### DailyGoalAchieved
- **Description**: User has met their daily hydration target
- **Triggered When**: Total daily intake reaches or exceeds goal amount
- **Key Data**: User ID, goal amount, actual amount, achievement time, days in streak
- **Consumers**: Achievement system, notification service, streak tracker, motivation engine

#### DailyGoalMissed
- **Description**: User failed to meet daily hydration goal by end of day
- **Triggered When**: Day ends with intake below goal threshold
- **Key Data**: User ID, goal amount, actual amount, shortfall percentage, date
- **Consumers**: Analytics service, streak breaker, insight generator, goal adjustment recommender

#### StreakMilestoneReached
- **Description**: User has achieved a significant consecutive days streak
- **Triggered When**: Consecutive goal achievement reaches milestone (7, 30, 100 days)
- **Key Data**: User ID, streak length, milestone type, start date, achievement date
- **Consumers**: Notification service, badge system, social sharing service, motivation engine

### ReminderEvents

#### ReminderScheduled
- **Description**: A new hydration reminder has been created
- **Triggered When**: User sets up reminder schedule or system auto-generates reminders
- **Key Data**: Reminder ID, user ID, reminder times, frequency, custom message, active status
- **Consumers**: Notification scheduler, preference manager

#### ReminderSent
- **Description**: A hydration reminder notification has been delivered to user
- **Triggered When**: Scheduled reminder time arrives
- **Key Data**: Reminder ID, user ID, sent timestamp, delivery channel (push/SMS/email), message content
- **Consumers**: Analytics service, engagement tracker, effectiveness analyzer

#### ReminderActedUpon
- **Description**: User logged water intake in response to a reminder
- **Triggered When**: User logs intake within defined window after reminder
- **Key Data**: Reminder ID, intake entry ID, time between reminder and action, amount logged
- **Consumers**: Reminder effectiveness tracker, optimization engine, user behavior analytics

#### ReminderSnoozed
- **Description**: User postponed a hydration reminder
- **Triggered When**: User chooses to snooze rather than dismiss or act on reminder
- **Key Data**: Reminder ID, original time, snooze duration, new reminder time
- **Consumers**: Notification scheduler, user preference learning

### HealthEvents

#### DehydrationRiskDetected
- **Description**: System has identified potential dehydration based on intake patterns
- **Triggered When**: Intake significantly below goal for extended period
- **Key Data**: User ID, detection time, risk level, hours since last intake, deficit amount
- **Consumers**: Alert service, health insights, urgent notification system

#### HydrationPatternAnalyzed
- **Description**: System has completed periodic analysis of hydration behavior
- **Triggered When**: Daily, weekly, or monthly analysis cycle completes
- **Key Data**: User ID, analysis period, average intake, consistency score, peak intake times, insights
- **Consumers**: Insight generator, goal recommendation engine, reminder optimization

### PersonalizationEvents

#### ActivityLevelAdjusted
- **Description**: User's activity level has changed, affecting hydration needs
- **Triggered When**: User updates activity profile or system detects activity change via integration
- **Key Data**: User ID, previous activity level, new activity level, effective date, adjustment source
- **Consumers**: Goal calculator, recommendation engine, reminder frequency adjuster

#### EnvironmentalFactorRecorded
- **Description**: Environmental conditions affecting hydration needs have been logged
- **Triggered When**: User records weather conditions or system auto-detects via location
- **Key Data**: User ID, date, temperature, humidity, altitude, climate zone
- **Consumers**: Goal adjustment calculator, insight generator, recommendation engine

### BeverageEvents

#### CustomBeverageAdded
- **Description**: User has added a custom beverage type to their tracking options
- **Triggered When**: User creates new beverage entry with hydration value
- **Key Data**: Beverage ID, name, hydration coefficient, typical serving size, category, user ID
- **Consumers**: Intake logging service, beverage library, quick-add menu

#### BeverageHydrationValueAdjusted
- **Description**: Hydration coefficient for a beverage type has been updated
- **Triggered When**: User or system updates how a beverage counts toward hydration goals
- **Key Data**: Beverage ID, previous coefficient, new coefficient, effective date, reason for change
- **Consumers**: Historical intake recalculation, goal progress updater, analytics service

### IntegrationEvents

#### FitnessDataSynced
- **Description**: Activity data from fitness device has been synchronized
- **Triggered When**: Integration imports workout or activity data
- **Key Data**: User ID, sync timestamp, activity type, duration, intensity, calories burned, data source
- **Consumers**: Goal adjuster, activity-based reminder trigger, hydration need calculator

#### WeatherDataImported
- **Description**: Local weather data has been retrieved for hydration calculation
- **Triggered When**: System fetches weather data based on user location
- **Key Data**: User ID, location, temperature, humidity, UV index, data timestamp, data source
- **Consumers**: Dynamic goal calculator, environmental adjustment engine, user insights
