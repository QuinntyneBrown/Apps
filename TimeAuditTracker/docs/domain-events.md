# Domain Events - Time Audit & Tracker

## Overview
This application helps users understand how they spend their time, identify inefficiencies, and optimize their daily schedules. Domain events capture time logging, activity categorization, pattern detection, and the journey toward better time management.

## Events

### TimeLoggingEvents

#### TimeEntryLogged
- **Description**: A time period has been logged for an activity
- **Triggered When**: User records how time was spent
- **Key Data**: Entry ID, user ID, activity type, start time, end time, duration, category, location, energy level, timestamp
- **Consumers**: Time analytics, activity categorization, schedule visualization

#### ManualTimeAdjusted
- **Description**: A previously logged time entry has been corrected
- **Triggered When**: User edits time entry for accuracy
- **Key Data**: Entry ID, original values, adjusted values, adjustment reason, timestamp
- **Consumers**: Data accuracy tracker, audit log, pattern recalculation

#### AutomaticTimeDetected
- **Description**: System has automatically detected and categorized time usage
- **Triggered When**: Integrated apps or sensors capture activity automatically
- **Key Data**: Entry ID, detection method, confidence level, auto-categorization, timestamp
- **Consumers**: Automation effectiveness, user confirmation queue, time capture

#### BulkTimeLogged
- **Description**: Multiple time entries have been logged together
- **Triggered When**: User logs an entire day or period retrospectively
- **Key Data**: Batch ID, entry count, time period covered, logging method, timestamp
- **Consumers**: Batch processing, completeness tracker, retrospective analysis

### CategoryEvents

#### ActivityCategorized
- **Description**: A time entry has been assigned to a category
- **Triggered When**: Activity is classified (work, personal, health, etc.)
- **Key Data**: Entry ID, category ID, subcategory, tags, categorization confidence, timestamp
- **Consumers**: Category analytics, time distribution calculator, priority analysis

#### CustomCategoryCreated
- **Description**: User has created a new activity category
- **Triggered When**: Standard categories don't fit user's needs
- **Key Data**: Category ID, category name, description, color code, parent category, timestamp
- **Consumers**: Category management, personalization tracker, taxonomy builder

#### CategoryBudgetSet
- **Description**: User has set a time budget for a category
- **Triggered When**: User defines how much time should be spent on activity type
- **Key Data**: Budget ID, category ID, target hours per day/week, rationale, timestamp
- **Consumers**: Budget tracking, overage alerts, goal monitoring

#### BudgetExceeded
- **Description**: Time spent in a category has exceeded the set budget
- **Triggered When**: Accumulated time surpasses defined limit
- **Key Data**: Budget ID, category ID, budgeted amount, actual amount, overage, timestamp
- **Consumers**: Alert system, behavior modification prompts, accountability

### AnalysisEvents

#### DailyAnalysisGenerated
- **Description**: End-of-day time usage analysis has been created
- **Triggered When**: Day ends or user requests daily summary
- **Key Data**: Analysis ID, date, total tracked time, category breakdown, productive vs. unproductive ratio, timestamp
- **Consumers**: Daily review, trend tracking, insight generation

#### WeeklyPatternIdentified
- **Description**: A recurring pattern in time usage has been detected
- **Triggered When**: Analysis reveals consistent weekly behavior
- **Key Data**: Pattern ID, pattern type, days involved, activities involved, consistency score, timestamp
- **Consumers**: Pattern awareness, schedule optimization, habit identification

#### TimeWasterIdentified
- **Description**: An activity consuming excessive unproductive time has been flagged
- **Triggered When**: Low-value activity exceeds threshold
- **Key Data**: Waster ID, activity description, time consumed, impact assessment, timestamp
- **Consumers**: Elimination suggestions, awareness alerts, optimization opportunities

#### ProductivityPeakDetected
- **Description**: User's most productive time period has been identified
- **Triggered When**: Analysis reveals when user is most effective
- **Key Data**: Peak ID, time window, days of week, productivity metrics, contributing factors, timestamp
- **Consumers**: Schedule optimization, task timing suggestions, energy management

### GoalEvents

#### TimeGoalSet
- **Description**: User has established a time management goal
- **Triggered When**: User commits to spending time differently
- **Key Data**: Goal ID, goal description, target metrics, baseline, deadline, timestamp
- **Consumers**: Goal tracker, progress monitor, achievement system

#### GoalProgressUpdated
- **Description**: Progress toward time management goal has been calculated
- **Triggered When**: New time data affects goal metrics
- **Key Data**: Progress ID, goal ID, current status, progress percentage, on-track indicator, timestamp
- **Consumers**: Progress visualization, motivational messaging, course correction

#### MilestoneAchieved
- **Description**: A time management milestone has been reached
- **Triggered When**: User hits defined progress marker
- **Key Data**: Milestone ID, goal ID, achievement date, celebration type, timestamp
- **Consumers**: Achievement system, motivation booster, habit reinforcement

#### TimeGoalAbandoned
- **Description**: User has stopped pursuing a time management goal
- **Triggered When**: Goal is marked as no longer relevant
- **Key Data**: Goal ID, abandonment reason, progress at abandonment, lessons learned, timestamp
- **Consumers**: Goal analysis, pattern identification, realistic goal-setting insights

### ComparisonEvents

#### PeriodCompared
- **Description**: Two time periods have been compared for changes
- **Triggered When**: User analyzes time usage across different periods
- **Key Data**: Comparison ID, period 1, period 2, key differences, improvements, regressions, timestamp
- **Consumers**: Change detection, improvement tracking, trend analysis

#### IdealVsActualCompared
- **Description**: Actual time usage has been compared to ideal allocation
- **Triggered When**: User's real time usage is measured against their stated preferences
- **Key Data**: Comparison ID, category-by-category variance, alignment score, priority gaps, timestamp
- **Consumers**: Alignment assessment, priority clarification, life balance evaluation

### OptimizationEvents

#### ScheduleOptimizationSuggested
- **Description**: System has suggested a schedule improvement
- **Triggered When**: Analysis identifies optimization opportunity
- **Key Data**: Suggestion ID, current state, proposed change, expected benefit, confidence level, timestamp
- **Consumers**: Suggestion delivery, user decision tracking, improvement measurement

#### OptimizationImplemented
- **Description**: User has adopted a schedule optimization
- **Triggered When**: User makes suggested change to time usage
- **Key Data**: Implementation ID, suggestion ID, start date, implementation details, timestamp
- **Consumers**: Effectiveness tracking, before/after analysis, optimization ROI

#### TimeBlockCreated
- **Description**: User has established a dedicated time block for an activity
- **Triggered When**: User schedules regular protected time
- **Key Data**: Block ID, activity, recurrence pattern, duration, protection level, timestamp
- **Consumers**: Calendar integration, commitment tracker, routine builder

### AlertEvents

#### UnloggedTimeDetected
- **Description**: A period of time remains unaccounted for
- **Triggered When**: Gap in time logging is identified
- **Key Data**: Gap ID, start time, end time, estimated duration, timestamp
- **Consumers**: Logging reminder, completeness tracker, data quality monitor

#### BalanceAlertTriggered
- **Description**: Work-life balance metrics have triggered a warning
- **Triggered When**: Time distribution indicates imbalance
- **Key Data**: Alert ID, imbalance type, severity, affected categories, recommendation, timestamp
- **Consumers**: User notification, wellness monitor, intervention system

#### ConsistencyStreakAchieved
- **Description**: User has maintained consistent time logging
- **Triggered When**: Consecutive days of complete logging reach milestone
- **Key Data**: Streak ID, streak length, completeness percentage, timestamp
- **Consumers**: Habit reinforcement, data quality celebration, engagement tracker

### IntegrationEvents

#### CalendarSynced
- **Description**: Calendar data has been imported for time analysis
- **Triggered When**: External calendar is synchronized
- **Key Data**: Sync ID, events imported, conflicts detected, timestamp
- **Consumers**: Automated logging, planned vs. actual analysis, schedule accuracy

#### AppUsageImported
- **Description**: Screen time or app usage data has been integrated
- **Triggered When**: Device usage data is brought into analysis
- **Key Data**: Import ID, apps tracked, usage duration, categories assigned, timestamp
- **Consumers**: Digital time tracker, distraction analysis, device time optimization
