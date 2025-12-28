# Domain Events - Sleep Quality Tracker

## Overview
This document defines the domain events tracked in the Sleep Quality Tracker application. These events capture significant business occurrences related to sleep logging, quality assessment, pattern analysis, and habit correlation.

## Events

### SleepSessionEvents

#### SleepSessionRecorded
- **Description**: A sleep session has been logged
- **Triggered When**: User manually enters or device syncs sleep data
- **Key Data**: Session ID, sleep start time, wake time, total duration, sleep quality rating, user ID, timestamp
- **Consumers**: Sleep tracker, quality analyzer, pattern detector, dashboard UI

#### SleepStagesLogged
- **Description**: Sleep stage data recorded
- **Triggered When**: Wearable device provides sleep stage breakdown
- **Key Data**: Session ID, light sleep duration, deep sleep duration, REM duration, awake time, user ID
- **Consumers**: Sleep architecture analyzer, recovery calculator, quality scorer

#### SleepInterruptionRecorded
- **Description**: Sleep disruption documented
- **Triggered When**: User logs or device detects wake-up during night
- **Key Data**: Interruption ID, session ID, interruption time, duration, reason, user ID
- **Consumers**: Sleep quality reducer, disruption pattern analyzer, cause identifier

#### EarlyWakeDetected
- **Description**: User woke significantly before alarm
- **Triggered When**: Wake time much earlier than target
- **Key Data**: Session ID, target wake time, actual wake time, time difference, user ID
- **Consumers**: Sleep quality assessor, schedule adjuster, issue identifier

### SleepGoalEvents

#### SleepGoalSet
- **Description**: Target sleep duration established
- **Triggered When**: User sets nightly sleep duration goal
- **Key Data**: Goal ID, target hours, target bedtime, target wake time, user ID, timestamp
- **Consumers**: Goal tracker, bedtime reminder, achievement monitor

#### SleepGoalMet
- **Description**: Nightly sleep goal achieved
- **Triggered When**: Sleep duration meets or exceeds goal
- **Key Data**: Session ID, goal hours, actual hours, achievement date, user ID
- **Consumers**: Achievement service, streak tracker, sleep score calculator

#### SleepGoalMissed
- **Description**: Failed to achieve sleep duration goal
- **Triggered When**: Sleep duration significantly below target
- **Key Data**: Session ID, goal hours, actual hours, shortfall, missed date, user ID
- **Consumers**: Sleep debt calculator, alert service, habit analyzer

#### ConsistentSleepScheduleAchieved
- **Description**: Regular sleep schedule maintained
- **Triggered When**: Bedtime/wake time within consistent window for X days
- **Key Data**: Achievement period, average bedtime, average wake time, variance, user ID
- **Consumers**: Achievement service, circadian rhythm optimizer, health score updater

### SleepQualityEvents

#### SleepQualityScored
- **Description**: Overall sleep quality rating calculated
- **Triggered When**: Sleep session completed and analyzed
- **Key Data**: Session ID, quality score (0-100), contributing factors, score timestamp, user ID
- **Consumers**: Quality dashboard, trend analyzer, improvement recommender

#### PoorSleepDetected
- **Description**: Significantly low quality sleep identified
- **Triggered When**: Sleep quality score below threshold
- **Key Data**: Session ID, quality score, poor quality factors, detection date, user ID
- **Consumers**: Alert service, habit correlation analyzer, improvement suggester

#### ExceptionalSleepRecorded
- **Description**: Unusually high quality sleep logged
- **Triggered When**: Sleep quality score exceeds excellence threshold
- **Key Data**: Session ID, quality score, contributing factors, achievement date, user ID
- **Consumers**: Achievement service, pattern identifier, habit reinforcer

#### SleepQualityTrendAnalyzed
- **Description**: Sleep quality trend over period calculated
- **Triggered When**: Weekly or monthly trend analysis runs
- **Key Data**: Analysis period, average quality, trend direction, improvement/decline percentage, user ID
- **Consumers**: Trend dashboard, health insights, intervention recommender

### SleepDebtEvents

#### SleepDebtAccumulated
- **Description**: Cumulative sleep deficit calculated
- **Triggered When**: Ongoing sleep shortfalls tracked
- **Key Data**: Total debt hours, accumulation period, debt severity, user ID, timestamp
- **Consumers**: Sleep debt tracker, recovery planner, health risk assessor

#### SleepDebtRepaid
- **Description**: Sleep deficit reduced through recovery sleep
- **Triggered When**: Extended sleep reduces accumulated debt
- **Key Data**: Repayment amount, remaining debt, repayment date, user ID
- **Consumers**: Debt tracker, recovery monitor, health score updater

#### CriticalSleepDebtReached
- **Description**: Sleep debt reached concerning level
- **Triggered When**: Cumulative debt exceeds health risk threshold
- **Key Data**: Total debt hours, health risk level, accumulation period, user ID
- **Consumers**: Alert service, urgent recommendation engine, health warning

### HabitCorrelationEvents

#### HabitLogged
- **Description**: Daily habit or behavior recorded
- **Triggered When**: User logs habit (caffeine, exercise, screen time, etc.)
- **Key Data**: Habit ID, habit type, timing, intensity, log date, user ID
- **Consumers**: Habit tracker, correlation analyzer, sleep impact assessor

#### SleepHabitCorrelationIdentified
- **Description**: Correlation between habit and sleep quality found
- **Triggered When**: Statistical analysis detects relationship
- **Key Data**: Habit type, correlation strength, impact on sleep quality, confidence level, user ID
- **Consumers**: Insight generator, recommendation engine, behavior modifier

#### CaffeineImpactAnalyzed
- **Description**: Effect of caffeine intake on sleep assessed
- **Triggered When**: Sufficient caffeine and sleep data for analysis
- **Key Data**: Caffeine timing, amount, sleep impact score, optimal cutoff time, user ID
- **Consumers**: Caffeine advisor, sleep optimization, habit recommender

#### ExerciseTimingOptimized
- **Description**: Best exercise timing for sleep quality identified
- **Triggered When**: Analysis determines optimal workout window
- **Key Data**: Optimal exercise time, sleep benefit score, worst times, user ID
- **Consumers**: Exercise scheduler, sleep optimizer, recommendation service

### EnvironmentEvents

#### SleepEnvironmentLogged
- **Description**: Bedroom conditions recorded
- **Triggered When**: User logs temperature, noise, light, etc.
- **Key Data**: Environment ID, session ID, temperature, noise level, light level, user ID, timestamp
- **Consumers**: Environment tracker, optimal conditions finder, sleep quality correlator

#### OptimalEnvironmentIdentified
- **Description**: Ideal sleep environment conditions determined
- **Triggered When**: Analysis finds conditions correlating with best sleep
- **Key Data**: Optimal temperature range, noise level, light level, other factors, user ID
- **Consumers**: Environment recommender, sleep optimization, bedroom setup advisor

#### SuboptimalConditionsDetected
- **Description**: Poor sleep environment identified
- **Triggered When**: Logged conditions known to impair sleep quality
- **Key Data**: Session ID, problematic factors, severity, correction suggestions, user ID
- **Consumers**: Alert service, improvement recommender, sleep hygiene advisor

### PatternEvents

#### SleepPatternDetected
- **Description**: Recurring sleep behavior identified
- **Triggered When**: Machine learning detects consistent pattern
- **Key Data**: Pattern type, frequency, pattern characteristics, detection confidence, user ID
- **Consumers**: Pattern dashboard, prediction service, personalized insights

#### WeekdayWeekendDiscrepancyIdentified
- **Description**: Significant sleep difference between weekdays and weekends
- **Triggered When**: Analysis shows large variance between work days and days off
- **Key Data**: Weekday average sleep, weekend average sleep, difference hours, user ID
- **Consumers**: Social jetlag indicator, schedule optimizer, health risk assessor

#### InsomniaPatternDetected
- **Description**: Indicators of insomnia identified
- **Triggered When**: Frequent difficulty falling or staying asleep
- **Key Data**: Pattern duration, frequency, severity, insomnia type, user ID
- **Consumers**: Medical alert, sleep hygiene advisor, professional help recommender

### RecoveryEvents

#### RecoveryScoreCalculated
- **Description**: Daily recovery score based on sleep computed
- **Triggered When**: Sleep session analyzed for recovery quality
- **Key Data**: Session ID, recovery score, HRV data, resting HR, recovery readiness, user ID
- **Consumers**: Recovery dashboard, training advisor, stress indicator

#### FullRecoveryAchieved
- **Description**: Optimal recovery status reached
- **Triggered When**: Sleep provides complete physiological recovery
- **Key Data**: Session ID, recovery metrics, readiness score, achievement date, user ID
- **Consumers**: Achievement service, training green light, performance predictor

### BedtimeEvents

#### BedtimeReminderSent
- **Description**: Notification to prepare for sleep sent
- **Triggered When**: Bedtime window approaching
- **Key Data**: Reminder time, target bedtime, preparation suggestions, user ID
- **Consumers**: Notification service, bedtime routine initiator

#### ConsistentBedtimeStreakAchieved
- **Description**: Regular bedtime maintained for consecutive days
- **Triggered When**: Bedtime within target window for X days
- **Key Data**: Streak length, target bedtime, variance, achievement date, user ID
- **Consumers**: Achievement service, circadian rhythm reinforcer, health score

#### LateBedtimeAlert
- **Description**: Bedtime significantly later than target
- **Triggered When**: User still awake past healthy bedtime threshold
- **Key Data**: Target bedtime, current time, delay amount, user ID
- **Consumers**: Alert service, sleep opportunity loss calculator, habit intervention

### DreamEvents

#### DreamLogged
- **Description**: Dream content recorded
- **Triggered When**: User logs dream after waking
- **Key Data**: Dream ID, session ID, dream description, emotional tone, vividness, user ID
- **Consumers**: Dream journal, REM correlation, pattern analyzer

#### NightmareReported
- **Description**: Disturbing dream logged
- **Triggered When**: User reports nightmare
- **Key Data**: Nightmare ID, session ID, content, intensity, impact on sleep quality, user ID
- **Consumers**: Sleep quality adjuster, stress indicator, pattern tracker

### NapEvents

#### NapRecorded
- **Description**: Daytime nap logged
- **Triggered When**: User records nap session
- **Key Data**: Nap ID, start time, duration, quality, user ID, timestamp
- **Consumers**: Total sleep calculator, nap pattern analyzer, nighttime sleep impact assessor

#### ExcessiveNappingDetected
- **Description**: Unusual amount of daytime napping identified
- **Triggered When**: Nap frequency or duration exceeds healthy levels
- **Key Data**: Nap frequency, total nap time, period, possible causes, user ID
- **Consumers**: Health alert, sleep disorder screener, nighttime sleep analyzer

### ReportEvents

#### WeeklySleepReportGenerated
- **Description**: Weekly sleep summary created
- **Triggered When**: Week ends or user requests report
- **Key Data**: Week ending date, average sleep duration, quality score, patterns identified, user ID
- **Consumers**: Report delivery, email service, insights dashboard

#### SleepInsightGenerated
- **Description**: Personalized sleep insight or recommendation created
- **Triggered When**: Analysis identifies actionable improvement opportunity
- **Key Data**: Insight type, recommendation, expected benefit, priority, user ID
- **Consumers**: Insight notification, recommendation dashboard, behavior change system
