# Domain Events - Stress & Mood Tracker

## Overview
This document defines the domain events tracked in the Stress & Mood Tracker application. These events capture significant business occurrences related to mood logging, stress levels, emotional triggers, journaling, and mental health insights.

## Events

### MoodEvents

#### MoodEntryLogged
- **Description**: User has recorded their current mood state
- **Triggered When**: User completes a mood check-in
- **Key Data**: Entry ID, user ID, mood rating, emotion tags, intensity level, timestamp, context notes
- **Consumers**: Mood analytics, pattern detection, trend analyzer, daily summary generator

#### MoodSwingDetected
- **Description**: Significant mood change has been identified between consecutive entries
- **Triggered When**: System detects large variance in mood ratings over short time period
- **Key Data**: User ID, previous mood, current mood, change magnitude, time between entries, potential triggers
- **Consumers**: Alert service, pattern analyzer, health insights, intervention recommender

#### PositiveMoodStreakAchieved
- **Description**: User has maintained positive mood ratings for consecutive days
- **Triggered When**: Mood ratings stay above threshold for milestone period (3, 7, 14 days)
- **Key Data**: User ID, streak length, average mood rating, start date, milestone type
- **Consumers**: Achievement system, motivation service, progress celebration, analytics

### StressEvents

#### StressLevelRecorded
- **Description**: User has logged their current stress level
- **Triggered When**: User completes stress assessment or quick check-in
- **Key Data**: Entry ID, user ID, stress rating (1-10), stress type, physical symptoms, timestamp
- **Consumers**: Stress analytics, intervention system, correlation analyzer, health insights

#### HighStressAlertTriggered
- **Description**: User has reported critically high stress level
- **Triggered When**: Stress rating exceeds critical threshold or sustained high stress detected
- **Key Data**: User ID, stress level, duration, associated symptoms, timestamp, previous high stress events
- **Consumers**: Immediate intervention service, coping resource recommender, alert notification, crisis support

#### StressReductionAchieved
- **Description**: User's stress level has decreased significantly
- **Triggered When**: Stress rating shows meaningful decrease from previous high level
- **Key Data**: User ID, previous stress level, current stress level, reduction amount, time to reduction, interventions used
- **Consumers**: Effectiveness tracker, intervention analyzer, success pattern identifier, user insights

### TriggerEvents

#### TriggerIdentified
- **Description**: User has identified and logged a mood or stress trigger
- **Triggered When**: User tags specific circumstance, person, or situation as trigger
- **Key Data**: Trigger ID, user ID, trigger type, trigger description, associated mood/stress level, timestamp, frequency count
- **Consumers**: Pattern analyzer, trigger library, avoidance strategy planner, correlation engine

#### TriggerPatternDetected
- **Description**: System has identified recurring trigger pattern
- **Triggered When**: Analytics detect trigger appears multiple times with similar outcomes
- **Key Data**: Trigger ID, user ID, occurrence count, average impact, time patterns, related triggers
- **Consumers**: Insight generator, proactive alert system, coping strategy recommender, therapy data export

#### TriggerAvoided
- **Description**: User has successfully avoided or managed a known trigger
- **Triggered When**: User logs encounter with trigger but maintains stable mood/stress
- **Key Data**: Trigger ID, user ID, coping strategy used, mood/stress maintained, timestamp
- **Consumers**: Success tracker, coping strategy effectiveness analyzer, positive reinforcement system

### JournalEvents

#### JournalEntryCreated
- **Description**: User has written a journal entry about their mental state
- **Triggered When**: User completes journaling session
- **Key Data**: Entry ID, user ID, entry text, word count, associated mood/stress levels, tags, timestamp, entry type
- **Consumers**: Text analytics, sentiment analysis, keyword extraction, therapy export, backup service

#### JournalEntryTagged
- **Description**: User has added contextual tags to journal entry
- **Triggered When**: User categorizes entry with descriptive tags
- **Key Data**: Entry ID, tags added, timestamp, tag categories, user ID
- **Consumers**: Search indexer, pattern analyzer, tag correlation engine, insights generator

#### TherapeuticThemeIdentified
- **Description**: System has detected recurring theme in journal entries
- **Triggered When**: Text analysis identifies consistent topics or concerns across entries
- **Key Data**: User ID, theme description, entry IDs, frequency, sentiment trend, first occurrence, latest occurrence
- **Consumers**: Insight generator, professional support recommender, trend analyzer, therapy data preparation

### InterventionEvents

#### CopingStrategyApplied
- **Description**: User has utilized a coping mechanism to manage stress or mood
- **Triggered When**: User logs use of specific coping technique
- **Key Data**: Strategy ID, user ID, strategy type, duration, pre-intervention mood/stress, post-intervention mood/stress, effectiveness rating
- **Consumers**: Effectiveness tracker, strategy recommender, pattern analyzer, personalization engine

#### CopingStrategyEffectivenessRated
- **Description**: User has provided feedback on coping strategy effectiveness
- **Triggered When**: User rates how well a strategy worked after using it
- **Key Data**: Strategy ID, user ID, effectiveness rating, context, mood improvement, would use again flag
- **Consumers**: Recommendation engine, strategy ranking, personalization service, insights generator

#### ProfessionalSupportRecommended
- **Description**: System has suggested seeking professional mental health support
- **Triggered When**: Patterns indicate need for professional intervention (sustained high stress, crisis indicators)
- **Key Data**: User ID, recommendation reason, severity indicators, pattern data, resources provided, timestamp
- **Consumers**: Resource provider, alert system, follow-up tracker, emergency support linker

### AnalyticsEvents

#### WeeklyInsightGenerated
- **Description**: System has compiled weekly mental health summary and insights
- **Triggered When**: Week ends and sufficient data exists for analysis
- **Key Data**: User ID, week start date, average mood, average stress, key triggers, coping successes, patterns identified
- **Consumers**: Notification service, report generator, trend visualizer, goal setter

#### CorrelationDiscovered
- **Description**: System has identified correlation between behaviors and mood/stress
- **Triggered When**: Statistical analysis finds significant relationship between factors
- **Key Data**: User ID, correlated factors, correlation strength, sample size, discovery date, actionable insight
- **Consumers**: Insight notification, behavior change recommender, pattern library, personalization

### CheckInEvents

#### ScheduledCheckInMissed
- **Description**: User did not complete a scheduled mood/stress check-in
- **Triggered When**: Scheduled check-in time passes without entry
- **Key Data**: User ID, missed check-in time, check-in type, consecutive misses, scheduled frequency
- **Consumers**: Reminder service, engagement tracker, habit reinforcement, concern detector

#### CheckInStreakMaintained
- **Description**: User has consistently completed scheduled check-ins
- **Triggered When**: User reaches milestone for consecutive check-ins (7, 14, 30 days)
- **Key Data**: User ID, streak length, check-in frequency, milestone achieved, start date
- **Consumers**: Achievement system, habit reinforcement, motivation service, engagement analyzer

#### EmergencyCheckInInitiated
- **Description**: User has triggered emergency check-in due to crisis situation
- **Triggered When**: User activates emergency or crisis support feature
- **Key Data**: User ID, trigger timestamp, crisis indicators, mood/stress levels, location (if permitted), immediate needs
- **Consumers**: Crisis support service, emergency contact notifier, resource provider, professional referral system
