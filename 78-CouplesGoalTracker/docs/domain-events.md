# Domain Events - Couples Goal Tracker

## Overview
This application enables couples to set, track, and achieve shared goals together. Domain events capture goal creation, progress tracking, milestone achievements, collaborative activities, and the journey of building shared dreams.

## Events

### GoalEvents

#### SharedGoalCreated
- **Description**: A new shared goal has been created by the couple
- **Triggered When**: Couple establishes a new goal they want to achieve together
- **Key Data**: Goal ID, goal title, description, category (financial/travel/health/relationship/home), target date, success criteria, both partner IDs, creation timestamp
- **Consumers**: Goal dashboard, milestone planning service, motivation system, progress tracker

#### GoalUpdated
- **Description**: Details of an existing goal have been modified
- **Triggered When**: Couple adjusts goal parameters, timeline, or success criteria
- **Key Data**: Goal ID, updated fields, previous values, new values, reason for change, timestamp
- **Consumers**: Progress recalculation service, notification system, goal analytics

#### GoalCompleted
- **Description**: A shared goal has been successfully achieved
- **Triggered When**: Couple marks a goal as completed or system detects achievement
- **Key Data**: Goal ID, completion date, actual vs. target date, celebration notes, photos, achievement story, timestamp
- **Consumers**: Achievement system, success analytics, celebration trigger, relationship strengthening insights

#### GoalAbandoned
- **Description**: A goal has been abandoned or deprioritized
- **Triggered When**: Couple decides to stop pursuing a goal
- **Key Data**: Goal ID, abandonment reason, progress at abandonment, lessons learned, timestamp
- **Consumers**: Analytics service, goal review system, learning archive

#### GoalReactivated
- **Description**: A previously abandoned or paused goal has been reactivated
- **Triggered When**: Couple decides to resume work on a goal
- **Key Data**: Goal ID, reactivation date, time since pause, updated parameters, renewed motivation, timestamp
- **Consumers**: Goal dashboard, progress tracker, motivation system

### MilestoneEvents

#### MilestoneCreated
- **Description**: A milestone has been added to break down a larger goal
- **Triggered When**: Couple defines intermediate steps toward their goal
- **Key Data**: Milestone ID, goal ID, milestone title, target date, success criteria, dependency on other milestones, timestamp
- **Consumers**: Progress tracking system, reminder service, visual progress indicator

#### MilestoneAchieved
- **Description**: A milestone toward a goal has been reached
- **Triggered When**: Couple marks a milestone as complete
- **Key Data**: Milestone ID, goal ID, achievement date, celebration type, progress percentage toward main goal, timestamp
- **Consumers**: Progress dashboard, celebration service, motivation boost system, next milestone activator

#### MilestoneMissed
- **Description**: A milestone target date has passed without completion
- **Triggered When**: Milestone deadline expires without achievement
- **Key Data**: Milestone ID, goal ID, original target date, current status, delay reason, revised target, timestamp
- **Consumers**: Goal replanning service, accountability system, timeline adjustment service

### ProgressEvents

#### ProgressUpdated
- **Description**: Progress toward a goal has been logged or updated
- **Triggered When**: Either partner records progress on a shared goal
- **Key Data**: Progress ID, goal ID, partner who updated, progress amount, progress type (financial/completion/habit), notes, timestamp
- **Consumers**: Progress visualization, motivation system, partner notification, achievement predictor

#### ProgressCommented
- **Description**: A partner has added encouragement or comments on progress
- **Triggered When**: One partner comments on recent progress
- **Key Data**: Comment ID, progress ID, goal ID, commenter ID, comment text, sentiment, timestamp
- **Consumers**: Partner engagement tracker, relationship health metrics, notification service

### CollaborationEvents

#### TaskAssignmentCreated
- **Description**: A task toward a goal has been assigned to one or both partners
- **Triggered When**: Couple divides responsibilities for goal achievement
- **Key Data**: Task ID, goal ID, assigned partner(s), task description, due date, priority, timestamp
- **Consumers**: Task management system, reminder service, workload balance analytics

#### PartnerContributionLogged
- **Description**: A partner's specific contribution toward a goal has been recorded
- **Triggered When**: Individual effort or contribution is documented
- **Key Data**: Contribution ID, goal ID, partner ID, contribution type, effort amount, value added, timestamp
- **Consumers**: Contribution balance tracker, appreciation prompts, fairness analytics

#### CollaborationSessionScheduled
- **Description**: Couple has scheduled a working session on their goals
- **Triggered When**: Partners set aside time to work together on goals
- **Key Data**: Session ID, goal IDs to discuss, scheduled time, agenda, location, timestamp
- **Consumers**: Calendar integration, reminder service, session preparation system

### CheckInEvents

#### WeeklyCheckInCompleted
- **Description**: Couple has completed their weekly goal review
- **Triggered When**: Partners conduct scheduled check-in on all active goals
- **Key Data**: Check-in ID, date, goals reviewed, progress discussed, adjustments made, relationship satisfaction, timestamp
- **Consumers**: Progress analytics, goal adjustment service, relationship health tracker

#### GoalDiscussionHeld
- **Description**: Couple has had a discussion about their goals
- **Triggered When**: Partners engage in conversation about their shared objectives
- **Key Data**: Discussion ID, goals discussed, decisions made, conflicts resolved, alignment score, duration, timestamp
- **Consumers**: Communication quality tracker, goal refinement service, relationship insights

### MotivationEvents

#### CelebrationTriggered
- **Description**: System or couple has initiated a celebration for an achievement
- **Triggered When**: Milestone reached, goal completed, or progress milestone hit
- **Key Data**: Celebration ID, trigger event, celebration type, suggested activities, timestamp
- **Consumers**: Celebration planner, reward system, memory capture service

#### EncouragementSent
- **Description**: System has sent motivational message to couple
- **Triggered When**: Progress slowing, deadline approaching, or scheduled motivation boost
- **Key Data**: Message ID, goal ID, recipient(s), message content, trigger reason, timestamp
- **Consumers**: Notification service, engagement tracker, motivation effectiveness analytics

#### StreakAchieved
- **Description**: Couple has maintained consistent progress for a defined period
- **Triggered When**: Consecutive progress updates or check-ins reach milestone
- **Key Data**: Streak ID, goal ID, streak length, streak type, consistency metrics, timestamp
- **Consumers**: Gamification system, achievement badges, motivation reinforcement
