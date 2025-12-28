# Domain Events - Personal Health Dashboard

## Overview
This document defines the domain events tracked in the Personal Health Dashboard application. These events capture significant business occurrences related to health metrics tracking, vital signs monitoring, wearable device integration, and wellness goal management.

## Events

### VitalSignEvents

#### VitalSignRecorded
- **Description**: A vital sign measurement has been logged
- **Triggered When**: User enters or device syncs vital sign data (BP, heart rate, temperature, etc.)
- **Key Data**: Measurement ID, vital type, value, unit, measurement timestamp, data source, user ID
- **Consumers**: Health tracker, trend analyzer, alert checker, dashboard UI

#### AbnormalReadingDetected
- **Description**: Vital sign measurement outside normal range identified
- **Triggered When**: Reading falls outside healthy thresholds
- **Key Data**: Measurement ID, vital type, value, normal range, severity level, detection timestamp, user ID
- **Consumers**: Alert service, healthcare notification, urgent care recommender

#### TrendAnalysisCompleted
- **Description**: Vital signs trend analysis has been calculated
- **Triggered When**: Periodic analysis or user requests trend view
- **Key Data**: Vital type, time period, trend direction, average value, variance, analysis timestamp, user ID
- **Consumers**: Trend visualizer, health insights, doctor report generator

### WeightEvents

#### WeightRecorded
- **Description**: Body weight measurement has been logged
- **Triggered When**: User enters weight or smart scale syncs data
- **Key Data**: Weight ID, weight value, unit, measurement date, data source, user ID, timestamp
- **Consumers**: Weight tracker, BMI calculator, trend analyzer, goal progress tracker

#### WeightGoalSet
- **Description**: Target weight goal has been established
- **Triggered When**: User sets weight loss, gain, or maintenance goal
- **Key Data**: Goal ID, target weight, current weight, goal deadline, weekly target, user ID, timestamp
- **Consumers**: Goal tracker, progress calculator, motivation service

#### WeightMilestoneReached
- **Description**: Weight goal milestone achieved
- **Triggered When**: Weight crosses milestone threshold (every 5 lbs, 10 lbs, etc.)
- **Key Data**: Milestone ID, milestone weight, achievement date, total progress, user ID
- **Consumers**: Achievement service, notification system, motivation tracker

### ActivityEvents

#### ActivityDataSynced
- **Description**: Activity data imported from wearable device
- **Triggered When**: Fitbit, Apple Watch, Garmin, etc. syncs activity data
- **Key Data**: Sync ID, device type, steps, distance, calories, active minutes, sync timestamp, user ID
- **Consumers**: Activity aggregator, goal checker, dashboard updater

#### DailyStepGoalReached
- **Description**: Daily step target has been achieved
- **Triggered When**: Step count reaches or exceeds daily goal
- **Key Data**: Steps taken, step goal, achievement timestamp, user ID
- **Consumers**: Achievement service, streak tracker, notification system

#### InactivityPeriodDetected
- **Description**: Extended period of low activity identified
- **Triggered When**: Activity falls below threshold for X consecutive days
- **Key Data**: Period start, period duration, average daily activity, inactivity threshold, user ID
- **Consumers**: Alert service, motivation reminder, activity suggester

### HeartHealthEvents

#### HeartRateRecorded
- **Description**: Heart rate measurement logged
- **Triggered When**: User records or device syncs heart rate data
- **Key Data**: HR ID, heart rate value, context (resting/active/exercise), measurement timestamp, user ID
- **Consumers**: Heart health tracker, cardio analyzer, alert checker

#### RestingHeartRateCalculated
- **Description**: Resting heart rate average computed
- **Triggered When**: Sufficient resting HR data collected for averaging
- **Key Data**: Calculation period, average RHR, trend vs previous period, calculation timestamp, user ID
- **Consumers**: Fitness level indicator, health trend analyzer, cardio health scorer

#### ElevatedHeartRateAlert
- **Description**: Heart rate exceeds safe threshold
- **Triggered When**: HR reading significantly above normal for context
- **Key Data**: Heart rate value, threshold, context, severity, alert timestamp, user ID
- **Consumers**: Emergency alert service, health notification, medical contact

### SleepEvents

#### SleepDataRecorded
- **Description**: Sleep session data has been logged
- **Triggered When**: User enters or device syncs sleep tracking data
- **Key Data**: Sleep ID, sleep start, sleep end, total hours, sleep quality, sleep stages, user ID, timestamp
- **Consumers**: Sleep tracker, quality analyzer, pattern detector, recovery calculator

#### SleepGoalMet
- **Description**: Sleep duration goal achieved for the night
- **Triggered When**: Sleep hours meet or exceed target
- **Key Data**: Sleep ID, target hours, actual hours, sleep quality, achievement date, user ID
- **Consumers**: Achievement service, streak tracker, health score updater

#### SleepDebtAccumulated
- **Description**: Cumulative sleep deficit reaching concerning levels
- **Triggered When**: Total sleep debt crosses threshold
- **Key Data**: Total sleep debt hours, debt accumulation period, health impact score, user ID, timestamp
- **Consumers**: Alert service, recovery recommender, sleep schedule advisor

### NutritionEvents

#### MealLogged
- **Description**: Food intake has been recorded
- **Triggered When**: User logs meal or snack
- **Key Data**: Meal ID, meal type, foods, calories, macros (protein/carbs/fat), meal time, user ID
- **Consumers**: Nutrition tracker, calorie counter, macro analyzer, diet goal checker

#### DailyCalorieGoalReached
- **Description**: Daily calorie target has been met
- **Triggered When**: Logged calories reach daily goal
- **Key Data**: Total calories, calorie goal, remaining calories, achievement timestamp, user ID
- **Consumers**: Diet tracker, goal manager, notification service

#### MacroBalanceAnalyzed
- **Description**: Daily macronutrient distribution calculated
- **Triggered When**: End of day or user requests macro breakdown
- **Key Data**: Protein percentage, carb percentage, fat percentage, targets, variance, analysis date, user ID
- **Consumers**: Nutrition dashboard, diet quality scorer, recommendation engine

### HydrationEvents

#### WaterIntakeLogged
- **Description**: Water consumption recorded
- **Triggered When**: User logs water intake
- **Key Data**: Intake ID, amount, unit (oz/ml), log timestamp, user ID
- **Consumers**: Hydration tracker, daily goal checker, reminder scheduler

#### HydrationGoalReached
- **Description**: Daily water intake goal achieved
- **Triggered When**: Total water logged meets daily target
- **Key Data**: Total intake, goal amount, achievement timestamp, user ID
- **Consumers**: Achievement service, notification system, streak tracker

#### DehydrationRiskDetected
- **Description**: Insufficient hydration identified
- **Triggered When**: Water intake significantly below goal at time of day
- **Key Data**: Current intake, expected intake, deficit, detection time, user ID
- **Consumers**: Alert service, hydration reminder, health risk tracker

### DeviceEvents

#### WearableDeviceConnected
- **Description**: Health tracking device has been linked
- **Triggered When**: User successfully connects Fitbit, Apple Watch, etc.
- **Key Data**: Device ID, device type, device name, connection timestamp, sync frequency, user ID
- **Consumers**: Sync scheduler, device manager, data importer

#### DeviceSyncCompleted
- **Description**: Data synchronization from wearable completed
- **Triggered When**: Scheduled or manual device sync finishes
- **Key Data**: Device ID, sync timestamp, data types synced, records imported, sync status, user ID
- **Consumers**: Data aggregator, dashboard updater, notification service

#### DeviceSyncFailed
- **Description**: Device synchronization encountered an error
- **Triggered When**: Sync process fails or times out
- **Key Data**: Device ID, error type, error message, failure timestamp, retry count, user ID
- **Consumers**: Error handler, notification service, retry scheduler

### GoalEvents

#### HealthGoalSet
- **Description**: New health or wellness goal established
- **Triggered When**: User creates fitness, nutrition, or health goal
- **Key Data**: Goal ID, goal type, target metric, target value, deadline, user ID, timestamp
- **Consumers**: Goal tracker, progress monitor, reminder service

#### WeeklyGoalSummaryGenerated
- **Description**: Weekly health goal progress summary created
- **Triggered When**: Week ends or user requests summary
- **Key Data**: Week ending date, goals tracked, achievement percentage, highlights, summary timestamp, user ID
- **Consumers**: Summary dashboard, email report, motivation service

### BiometricEvents

#### BloodPressureRecorded
- **Description**: Blood pressure measurement logged
- **Triggered When**: User enters systolic and diastolic readings
- **Key Data**: BP ID, systolic, diastolic, measurement context, measurement timestamp, user ID
- **Consumers**: BP tracker, hypertension monitor, health alert checker

#### BloodGlucoseRecorded
- **Description**: Blood sugar level measurement logged
- **Triggered When**: User enters glucose reading
- **Key Data**: Glucose ID, glucose level, measurement context (fasting/post-meal), measurement timestamp, user ID
- **Consumers**: Diabetes tracker, glucose pattern analyzer, alert checker

### ReportEvents

#### HealthReportGenerated
- **Description**: Comprehensive health summary report created
- **Triggered When**: User requests health report for doctor visit or personal review
- **Key Data**: Report ID, report period, metrics included, trends, anomalies, generation timestamp, user ID
- **Consumers**: Report delivery, PDF generator, healthcare sharing
