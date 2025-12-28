# Domain Events - Morning Routine Builder

## Overview
This application helps users design, track, and optimize their morning routines for better day starts. Domain events capture routine creation, daily completion tracking, habit formation, and the refinement of morning practices.

## Events

### RoutineDesignEvents

#### RoutineCreated
- **Description**: A new morning routine has been designed
- **Triggered When**: User creates their morning routine structure
- **Key Data**: Routine ID, user ID, routine name, total duration, step count, start time target, timestamp
- **Consumers**: Routine manager, schedule calculator, onboarding flow

#### RoutineStepAdded
- **Description**: An activity has been added to the morning routine
- **Triggered When**: User includes a new step in their routine
- **Key Data**: Step ID, routine ID, step name, duration, order position, category (health/spiritual/productivity), timestamp
- **Consumers**: Routine builder, time calculator, sequence manager

#### RoutineModified
- **Description**: The structure or timing of a routine has been changed
- **Triggered When**: User adjusts their routine design
- **Key Data**: Routine ID, modified elements, previous configuration, new configuration, modification reason, timestamp
- **Consumers**: Version tracking, optimization analyzer, change log

#### RoutineActivated
- **Description**: A routine has been set as the active morning routine
- **Triggered When**: User commits to following a specific routine
- **Key Data**: Routine ID, activation date, previous routine (if any), timestamp
- **Consumers**: Daily tracker, reminder system, habit formation engine

### CompletionEvents

#### RoutineStarted
- **Description**: User has begun their morning routine
- **Triggered When**: User initiates routine tracking for the day
- **Key Data**: Session ID, routine ID, actual start time, scheduled start time, variance, timestamp
- **Consumers**: Timing tracker, session manager, completion monitor

#### StepCompleted
- **Description**: A single step in the routine has been finished
- **Triggered When**: User marks a routine activity as done
- **Key Data**: Completion ID, session ID, step ID, completion time, duration, quality rating, timestamp
- **Consumers**: Progress tracker, routine flow monitor, step analytics

#### StepSkipped
- **Description**: A routine step has been intentionally bypassed
- **Triggered When**: User chooses not to complete a step
- **Key Data**: Skip ID, session ID, step ID, skip reason, timestamp
- **Consumers**: Skip pattern analyzer, routine adjustment suggestions, flexibility tracker

#### RoutineCompleted
- **Description**: All steps of the morning routine have been finished
- **Triggered When**: User completes final step of routine
- **Key Data**: Session ID, routine ID, total duration, completion rate, quality score, end time, timestamp
- **Consumers**: Streak tracker, achievement system, daily summary generator

#### RoutineAbandoned
- **Description**: Morning routine was started but not completed
- **Triggered When**: Significant time passes without completion or user manually abandons
- **Key Data**: Session ID, routine ID, steps completed, abandonment point, reason, timestamp
- **Consumers**: Troubleshooting system, routine difficulty analyzer, support recommendations

### StreakEvents

#### CompletionStreakStarted
- **Description**: User has begun a new streak of routine completions
- **Triggered When**: First consecutive day of completion
- **Key Data**: Streak ID, routine ID, start date, timestamp
- **Consumers**: Streak tracker, motivation system, milestone predictor

#### StreakMilestoneReached
- **Description**: A streak milestone has been achieved
- **Triggered When**: Streak reaches significant length (7, 30, 100 days)
- **Key Data**: Streak ID, milestone number, days completed, achievement badge, timestamp
- **Consumers**: Achievement system, celebration trigger, social sharing option

#### StreakBroken
- **Description**: A completion streak has ended
- **Triggered When**: User misses a day after consecutive completions
- **Key Data**: Streak ID, final length, break date, streak recovery option, timestamp
- **Consumers**: Re-engagement system, pattern analyzer, motivation recovery

#### StreakRecovered
- **Description**: User has resumed their routine after a break
- **Triggered When**: Routine completed again after missed day(s)
- **Key Data**: Recovery ID, previous streak ID, days missed, new streak started, timestamp
- **Consumers**: Resilience tracker, comeback celebration, habit persistence monitor

### TimingEvents

#### OptimalStartTimeIdentified
- **Description**: Best time to start routine has been determined
- **Triggered When**: Pattern analysis reveals most successful start time
- **Key Data**: Optimal time window, success rate correlation, sample size, timestamp
- **Consumers**: Schedule optimizer, reminder timing, personalization engine

#### WakeTimeLogged
- **Description**: User's actual wake-up time has been recorded
- **Triggered When**: User logs when they woke up
- **Key Data**: Wake time ID, date, actual wake time, target wake time, variance, sleep quality, timestamp
- **Consumers**: Wake consistency tracker, routine timing adjuster, sleep pattern analyzer

#### RoutineDurationOptimized
- **Description**: Routine timing has been adjusted for better fit
- **Triggered When**: User or system modifies routine length for practicality
- **Key Data**: Optimization ID, previous duration, new duration, reason, timestamp
- **Consumers**: Realistic planning, adherence improvement, time management

### QualityEvents

#### RoutineQualityRated
- **Description**: User has rated how well the routine went
- **Triggered When**: User provides quality feedback after completion
- **Key Data**: Rating ID, session ID, quality score, energy level after, satisfaction, notes, timestamp
- **Consumers**: Quality analytics, routine effectiveness tracker, improvement identification

#### BestRoutineDayRecorded
- **Description**: A particularly excellent routine completion has been noted
- **Triggered When**: Routine completion scores exceptionally high
- **Key Data**: Session ID, excellence factors, what went well, replication tips, timestamp
- **Consumers**: Success pattern analyzer, best practices identifier, inspiration archive

#### RoutineStrugglesLogged
- **Description**: Difficulties with routine execution have been documented
- **Triggered When**: User reports challenges or obstacles
- **Key Data**: Struggle ID, session ID, challenge type, impact level, attempted solutions, timestamp
- **Consumers**: Troubleshooting system, routine adjustment recommendations, support triggers

### AdaptationEvents

#### SeasonalRoutineCreated
- **Description**: A routine variant for specific season or period has been designed
- **Triggered When**: User creates season-specific routine
- **Key Data**: Routine ID, season/period, differences from base routine, activation conditions, timestamp
- **Consumers**: Seasonal switcher, context-aware routine selection, variety manager

#### WeekendRoutineActivated
- **Description**: Alternative routine for non-workdays has been enabled
- **Triggered When**: User switches to weekend/holiday routine structure
- **Key Data**: Weekend routine ID, differences from weekday, activation trigger, timestamp
- **Consumers**: Flexible routine manager, work/life balance, realistic expectations

#### RoutineFlexibilityIncreased
- **Description**: Routine has been made more adaptable to circumstances
- **Triggered When**: User enables flexible timing or optional steps
- **Key Data**: Flexibility ID, routine ID, flex parameters, optional steps designated, timestamp
- **Consumers**: Adherence improvement, realistic routine design, sustainable habits

### ReminderEvents

#### MorningReminderSent
- **Description**: Wake-up or routine start reminder has been delivered
- **Triggered When**: Scheduled reminder time arrives
- **Key Data**: Reminder ID, reminder type, delivery time, user response, timestamp
- **Consumers**: Notification service, reminder effectiveness tracker, engagement monitor

#### StepReminderTriggered
- **Description**: Reminder for specific routine step has been sent
- **Triggered When**: Step should be happening based on routine timeline
- **Key Data**: Reminder ID, step ID, expected vs. actual time, timestamp
- **Consumers**: Pace keeper, routine flow assistance, on-track monitoring

### ImpactEvents

#### DayQualityCorrelated
- **Description**: Morning routine completion has been linked to day quality
- **Triggered When**: Analysis connects routine adherence to daily outcomes
- **Key Data**: Correlation ID, routine completion rate, day quality metrics, correlation strength, timestamp
- **Consumers**: Motivation system, value demonstration, commitment reinforcement

#### LifeImpactRecorded
- **Description**: User has noted how routine has affected their life
- **Triggered When**: User documents broader benefits of routine practice
- **Key Data**: Impact ID, impact area, description, significance level, timestamp
- **Consumers**: Success stories, long-term value tracker, testimonial collection

### SocialEvents

#### RoutineSharedWithCommunity
- **Description**: User has shared their routine design publicly
- **Triggered When**: User posts routine for others to see
- **Key Data**: Share ID, routine ID, visibility level, sharing platform, timestamp
- **Consumers**: Community feature, inspiration for others, social accountability

#### RoutineTemplateUsed
- **Description**: User has adopted a pre-designed routine template
- **Triggered When**: User selects and customizes a template routine
- **Key Data**: Template ID, template source, customizations made, timestamp
- **Consumers**: Template effectiveness tracker, popular routines, onboarding acceleration
