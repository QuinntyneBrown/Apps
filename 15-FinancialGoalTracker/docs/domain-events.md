# Domain Events - Financial Goal Tracker

## Overview
This document defines the domain events tracked in the Financial Goal Tracker application. These events capture significant business occurrences related to goal setting, progress tracking, milestone achievements, and financial objective management.

## Events

### GoalEvents

#### GoalCreated
- **Description**: A new financial goal has been established
- **Triggered When**: User creates a savings, debt payoff, or investment goal
- **Key Data**: Goal ID, goal name, target amount, deadline, category, priority, user ID, timestamp
- **Consumers**: Goal tracker, progress calculator, deadline monitor, dashboard UI

#### GoalUpdated
- **Description**: Goal parameters have been modified
- **Triggered When**: User changes target amount, deadline, or other goal details
- **Key Data**: Goal ID, previous values, new values, update reason, impact on progress, user ID, timestamp
- **Consumers**: Progress recalculator, milestone adjuster, notification service

#### GoalAchieved
- **Description**: Financial goal has been successfully completed
- **Triggered When**: Current amount reaches or exceeds target amount
- **Key Data**: Goal ID, target amount, final amount, completion date, time to complete, user ID
- **Consumers**: Achievement service, celebration trigger, historical archiver, success analyzer

#### GoalAbandoned
- **Description**: User has discontinued pursuit of a goal
- **Triggered When**: User marks goal as cancelled or no longer relevant
- **Key Data**: Goal ID, abandonment reason, progress at abandonment, amount saved, abandonment date, user ID
- **Consumers**: Goal archiver, analytics tracker, lessons learned service

#### GoalReactivated
- **Description**: Previously abandoned goal has been resumed
- **Triggered When**: User restarts work on a cancelled goal
- **Key Data**: Goal ID, reactivation date, previous progress, new parameters, user ID
- **Consumers**: Active goal manager, progress restorer, timeline recalculator

### ProgressEvents

#### ProgressUpdated
- **Description**: Progress toward goal has been recorded
- **Triggered When**: User logs contribution or updates current amount
- **Key Data**: Progress ID, goal ID, contribution amount, new total, progress percentage, update date, user ID
- **Consumers**: Progress tracker, milestone checker, dashboard updater, forecast recalculator

#### AutomaticContributionMade
- **Description**: Scheduled automatic contribution has been processed
- **Triggered When**: Recurring contribution date arrives and payment executes
- **Key Data**: Contribution ID, goal ID, amount, contribution date, next scheduled date, user ID
- **Consumers**: Progress updater, balance tracker, contribution history

#### ManualContributionRecorded
- **Description**: One-time or ad-hoc contribution logged
- **Triggered When**: User makes unscheduled contribution to goal
- **Key Data**: Contribution ID, goal ID, amount, contribution date, source, user ID
- **Consumers**: Progress calculator, total updater, momentum tracker

#### WithdrawalMade
- **Description**: Funds have been withdrawn from goal savings
- **Triggered When**: User removes money from goal account
- **Key Data**: Withdrawal ID, goal ID, amount, withdrawal reason, new balance, withdrawal date, user ID
- **Consumers**: Progress adjuster, setback tracker, timeline recalculator

### MilestoneEvents

#### MilestoneReached
- **Description**: Progress milestone achieved on path to goal
- **Triggered When**: Progress crosses defined threshold (25%, 50%, 75%, etc.)
- **Key Data**: Milestone ID, goal ID, milestone percentage, achievement date, time to milestone, user ID
- **Consumers**: Achievement service, notification system, motivation tracker, progress visualizer

#### CustomMilestoneSet
- **Description**: User-defined intermediate milestone created
- **Triggered When**: User sets custom checkpoint within goal timeline
- **Key Data**: Milestone ID, goal ID, target amount, target date, milestone name, user ID, timestamp
- **Consumers**: Milestone tracker, progress monitor, reminder scheduler

#### MilestoneMissed
- **Description**: Target milestone not achieved by expected date
- **Triggered When**: Milestone deadline passes without reaching target
- **Key Data**: Milestone ID, goal ID, target amount, actual amount, shortfall, missed date, user ID
- **Consumers**: Alert service, adjustment recommender, timeline analyzer

### ForecastEvents

#### CompletionDateProjected
- **Description**: Estimated goal completion date calculated
- **Triggered When**: Contribution pattern analyzed to forecast achievement
- **Key Data**: Goal ID, projected completion date, confidence level, assumptions used, calculation timestamp, user ID
- **Consumers**: Timeline visualizer, deadline comparator, adjustment advisor

#### ProjectionUpdated
- **Description**: Goal completion forecast has been revised
- **Triggered When**: Contribution rate changes or progress updated
- **Key Data**: Goal ID, previous projection, new projection, change factors, update timestamp, user ID
- **Consumers**: Timeline adjuster, notification service, progress dashboard

#### OnTrackStatusChanged
- **Description**: Goal status shifted between on-track and off-track
- **Triggered When**: Progress pace falls behind or catches up to target
- **Key Data**: Goal ID, previous status, new status, variance percentage, status change date, user ID
- **Consumers**: Alert service, adjustment recommender, status indicator

### StrategyEvents

#### SavingsStrategyCreated
- **Description**: Contribution strategy defined for goal achievement
- **Triggered When**: User sets up contribution schedule and amount
- **Key Data**: Strategy ID, goal ID, contribution frequency, contribution amount, start date, user ID, timestamp
- **Consumers**: Auto-contribution scheduler, forecast calculator, commitment tracker

#### StrategyAdjusted
- **Description**: Savings strategy modified to course-correct
- **Triggered When**: User changes contribution amount or frequency
- **Key Data**: Strategy ID, goal ID, previous plan, new plan, adjustment reason, impact on timeline, user ID
- **Consumers**: Forecast recalculator, auto-payment updater, timeline adjuster

#### AccelerationPlanActivated
- **Description**: Accelerated savings plan initiated to reach goal faster
- **Triggered When**: User increases contributions to finish ahead of schedule
- **Key Data**: Plan ID, goal ID, increased contribution, new projected completion, time savings, user ID
- **Consumers**: Timeline recalculator, auto-payment adjuster, motivation tracker

### CategoryEvents

#### GoalCategorized
- **Description**: Goal assigned to category (emergency fund, vacation, house, etc.)
- **Triggered When**: User assigns or system categorizes goal
- **Key Data**: Goal ID, category name, categorization method, user ID, timestamp
- **Consumers**: Category aggregator, goal organizer, reporting service

#### CategoryProgressCalculated
- **Description**: Total progress across all goals in a category computed
- **Triggered When**: Progress updated or category view requested
- **Key Data**: Category name, total target, total saved, category progress percentage, goal count, timestamp
- **Consumers**: Category dashboard, multi-goal tracker, allocation advisor

### PriorityEvents

#### PriorityChanged
- **Description**: Goal priority level has been adjusted
- **Triggered When**: User re-prioritizes goals
- **Key Data**: Goal ID, previous priority, new priority, priority change reason, user ID, timestamp
- **Consumers**: Goal ranker, allocation recommender, focus advisor

#### ConflictingGoalsDetected
- **Description**: Multiple high-priority goals competing for resources identified
- **Triggered When**: System detects insufficient funds for all goal contributions
- **Key Data**: Conflicting goal IDs, total required, available funds, shortfall, detection timestamp
- **Consumers**: Priority advisor, allocation optimizer, notification service

### TimelineEvents

#### DeadlineApproaching
- **Description**: Goal deadline is nearing
- **Triggered When**: Deadline is X days/weeks away
- **Key Data**: Goal ID, deadline date, days remaining, completion percentage, projected vs target, user ID
- **Consumers**: Urgency alert, notification service, catch-up advisor

#### DeadlineExtended
- **Description**: Goal deadline has been pushed back
- **Triggered When**: User extends target completion date
- **Key Data**: Goal ID, original deadline, new deadline, extension reason, impact on strategy, user ID
- **Consumers**: Timeline recalculator, pressure reliever, strategy adjuster

#### DeadlineMissed
- **Description**: Goal deadline passed without achievement
- **Triggered When**: Deadline date reached and goal not completed
- **Key Data**: Goal ID, deadline date, progress percentage, shortfall amount, user ID
- **Consumers**: Alert service, post-mortem analyzer, recovery planner

### MotivationEvents

#### StreakAchieved
- **Description**: Consecutive contribution streak milestone reached
- **Triggered When**: User makes contributions for X consecutive periods
- **Key Data**: Goal ID, streak length, streak type, achievement date, user ID
- **Consumers**: Achievement service, gamification system, motivation tracker

#### PersonalRecordSet
- **Description**: Largest single contribution or fastest progress achieved
- **Triggered When**: Contribution or progress rate exceeds previous bests
- **Key Data**: Record type, record value, previous record, achievement date, goal ID, user ID
- **Consumers**: Achievement service, celebration trigger, leaderboard updater

### LinkageEvents

#### GoalLinkedToAccount
- **Description**: Goal connected to specific savings account
- **Triggered When**: User links goal to bank account for tracking
- **Key Data**: Goal ID, account ID, institution name, link date, auto-sync enabled, user ID
- **Consumers**: Balance syncer, progress auto-updater, account tracker

#### AccountBalanceSynced
- **Description**: Linked account balance updated to reflect goal progress
- **Triggered When**: Automatic sync retrieves latest account balance
- **Key Data**: Goal ID, account ID, previous balance, new balance, sync timestamp, user ID
- **Consumers**: Progress updater, forecast adjuster, milestone checker
