# Domain Events - Chore Assignment & Tracker

## Overview
This document defines the domain events tracked in the Chore Assignment & Tracker application. These events capture significant business occurrences related to chore creation, assignment, completion, rotation, rewards, and household task management.

## Events

### ChoreEvents

#### ChoreCreated
- **Description**: A new chore has been added to the household task list
- **Triggered When**: User defines new chore with details and requirements
- **Key Data**: Chore ID, chore name, description, category, estimated duration, difficulty level, frequency, point value, created by
- **Consumers**: Chore library, assignment pool, schedule generator, point value calculator

#### ChoreModified
- **Description**: Existing chore definition has been updated
- **Triggered When**: User edits chore details or requirements
- **Key Data**: Chore ID, modified fields, previous values, new values, modification date, modified by
- **Consumers**: Active assignment updater, schedule adjuster, point recalculator, notification service

#### ChoreDeleted
- **Description**: Chore has been removed from household task system
- **Triggered When**: User removes chore from active rotation
- **Key Data**: Chore ID, deletion date, deletion reason, historical completions, assigned to, final status
- **Consumers**: Assignment cleaner, schedule updater, analytics archiver, audit log

### AssignmentEvents

#### ChoreAssigned
- **Description**: Chore has been assigned to household member
- **Triggered When**: User manually assigns or system auto-assigns chore
- **Key Data**: Assignment ID, chore ID, assigned to, assigned by, assignment date, due date, priority, assignment method
- **Consumers**: Assignee notification, calendar integration, workload balancer, reminder scheduler

#### ChoreReassigned
- **Description**: Previously assigned chore has been transferred to different person
- **Triggered When**: User changes assignment due to availability or fairness
- **Key Data**: Assignment ID, chore ID, previous assignee, new assignee, reassignment reason, reassigned by, new due date
- **Consumers**: Both parties notifier, workload recalculator, fairness tracker, calendar updater

#### SelfAssignmentMade
- **Description**: Household member has volunteered for available chore
- **Triggered When**: User claims unassigned or optional chore
- **Key Data**: Assignment ID, chore ID, assignee, self-assignment timestamp, motivation notes
- **Consumers**: Assignment tracker, initiative points, workload calculator, positive reinforcement

### CompletionEvents

#### ChoreCompleted
- **Description**: Assigned chore has been finished
- **Triggered When**: User marks chore as complete
- **Key Data**: Assignment ID, completion timestamp, completed by, quality self-rating, photos, time taken, notes
- **Consumers**: Points awarder, schedule updater, next occurrence generator, completion history, quality tracker

#### CompletionVerified
- **Description**: Completed chore has been inspected and approved
- **Triggered When**: Parent or supervisor confirms chore quality
- **Key Data**: Assignment ID, verifier, verification timestamp, quality rating, feedback, bonus points awarded
- **Consumers**: Final points allocation, quality score updater, feedback delivery, reward processor

#### CompletionRejected
- **Description**: Completed chore did not meet quality standards
- **Triggered When**: Verifier determines chore needs to be redone
- **Key Data**: Assignment ID, verifier, rejection reason, specific issues, quality rating, redo deadline
- **Consumers**: Reassignment to original assignee, notification service, quality tracker, learning opportunity

#### ChoreSkipped
- **Description**: Assigned chore was intentionally not completed
- **Triggered When**: User marks chore as skipped with reason
- **Key Data**: Assignment ID, skip date, skip reason, approved skip flag, point penalty applied
- **Consumers**: Workload adjuster, point calculator, pattern analyzer, fairness monitor

### ScheduleEvents

#### ChoreRotationScheduled
- **Description**: Recurring chore rotation has been set up
- **Triggered When**: User establishes rotation pattern for chore assignments
- **Key Data**: Rotation ID, chore ID, rotation members, rotation frequency, next assignment date, rotation type
- **Consumers**: Auto-assignment scheduler, fairness distributor, calendar populator, notification planner

#### RotationCycleCompleted
- **Description**: All members have completed one cycle of chore rotation
- **Triggered When**: Last person in rotation completes their turn
- **Key Data**: Rotation ID, cycle number, completion date, cycle statistics, participation rate, next cycle start
- **Consumers**: Next cycle initiator, analytics, rotation optimizer, fairness validator

#### OverdueChoreEscalated
- **Description**: Chore has remained incomplete past deadline with escalation
- **Triggered When**: Chore overdue beyond grace period
- **Key Data**: Assignment ID, due date, days overdue, escalation level, assignee, notified parties
- **Consumers**: Alert system, parent notification, consequence trigger, intervention workflow

### RewardEvents

#### PointsEarned
- **Description**: Household member has been awarded points for chore completion
- **Triggered When**: Chore completion verified or auto-approved
- **Key Data**: User ID, assignment ID, points earned, point type, earning timestamp, current total points
- **Consumers**: Points ledger, leaderboard updater, reward eligibility checker, achievement system

#### PointsDeducted
- **Description**: Points have been removed due to missed chore or violation
- **Triggered When**: Penalty applied for non-completion or quality issues
- **Key Data**: User ID, points deducted, deduction reason, assignment ID, timestamp, remaining points
- **Consumers**: Points ledger, accountability system, notification service, behavior tracker

#### BonusPointsAwarded
- **Description**: Extra points granted for exceptional performance or initiative
- **Triggered When**: User exceeds expectations or shows special effort
- **Key Data**: User ID, bonus amount, reason, awarded by, assignment ID, timestamp
- **Consumers**: Points ledger, motivation system, positive reinforcement, recognition notifier

#### RewardRedeemed
- **Description**: Household member has exchanged points for reward
- **Triggered When**: User claims available reward with sufficient points
- **Key Data**: Redemption ID, user ID, reward type, points spent, redemption date, fulfillment status
- **Consumers**: Points deduction, reward fulfillment tracker, inventory updater, redemption history

#### RewardThresholdReached
- **Description**: User has accumulated enough points for new reward tier
- **Triggered When**: Point total reaches milestone for reward eligibility
- **Key Data**: User ID, threshold amount, rewards unlocked, achievement date, notification sent
- **Consumers**: Notification service, reward catalog updater, motivation system, achievement tracker

### FairnessEvents

#### WorkloadImbalanceDetected
- **Description**: System has identified unequal chore distribution
- **Triggered When**: Analytics detect significant disparity in assignments or completions
- **Key Data**: Analysis period, household members, workload metrics, imbalance percentage, recommendation
- **Consumers**: Balance alert, assignment adjuster, rotation optimizer, fairness report

#### EquitableDistributionAchieved
- **Description**: Chores have been fairly distributed across household members
- **Triggered When**: System confirms balanced assignment for period
- **Key Data**: Period, household members, distribution metrics, fairness score, achievement date
- **Consumers**: Analytics, positive feedback, system validation, household harmony indicator

### TradeEvents

#### ChoreTradProposed
- **Description**: Household member has offered to trade assigned chore
- **Triggered When**: User initiates chore swap request
- **Key Data**: Trade ID, proposer, assignment offered, desired assignment, trade terms, expiration
- **Consumers**: Trade marketplace, potential traders notifier, negotiation system

#### ChoreTradeAccepted
- **Description**: Proposed chore trade has been agreed upon
- **Triggered When**: Other party accepts trade offer
- **Key Data**: Trade ID, both parties, assignments swapped, acceptance timestamp, approval required flag
- **Consumers**: Assignment swapper, both parties notifier, approval workflow, trade history

#### ChoreTradeCompleted
- **Description**: Chore trade has been finalized and assignments updated
- **Triggered When**: Trade approved (if needed) and assignments officially swapped
- **Key Data**: Trade ID, final assignments, completion timestamp, both parties, approver
- **Consumers**: Assignment updater, calendar sync, reminder updater, analytics

### AchievementEvents

#### ChoreStreakAchieved
- **Description**: Household member has completed chores for consecutive periods
- **Triggered When**: User reaches streak milestone (7, 14, 30 days)
- **Key Data**: User ID, streak length, chores completed, start date, milestone type
- **Consumers**: Badge system, notification service, bonus points, leaderboard, motivation

#### QualityMilestoneReached
- **Description**: User has maintained high quality ratings over time
- **Triggered When**: Quality score average exceeds threshold for significant period
- **Key Data**: User ID, average quality score, evaluation period, completions reviewed, achievement level
- **Consumers**: Recognition system, bonus awards, quality badge, parent notification, pride reinforcement

#### TeamworkBonusEarned
- **Description**: Household achieved collective chore completion goal
- **Triggered When**: All members meet participation threshold for period
- **Key Data**: Period, all participants, collective goal, actual completion rate, bonus awarded
- **Consumers**: Group bonus distributor, celebration trigger, household achievement, motivation booster
