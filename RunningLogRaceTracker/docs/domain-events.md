# Domain Events - Running Log & Race Tracker

## Overview
This document defines the domain events tracked in the Running Log & Race Tracker application. These events capture significant business occurrences related to run logging, race preparation, performance tracking, and training plan management.

## Events

### RunEvents

#### RunCompleted
- **Description**: A running session has been finished and logged
- **Triggered When**: User completes and records a run
- **Key Data**: Run ID, distance, duration, pace, route, elevation, weather, perceived effort, user ID, timestamp
- **Consumers**: Run history, statistics calculator, training plan tracker, performance analyzer

#### PersonalRecordSet
- **Description**: New personal best achieved for a distance
- **Triggered When**: Run time beats previous best for distance
- **Key Data**: Record ID, distance, new time, previous time, improvement, achievement date, user ID
- **Consumers**: Achievement service, celebration trigger, leaderboard, motivation system

#### RunSkipped
- **Description**: Planned run was not completed
- **Triggered When**: Scheduled training run missed
- **Key Data**: Skip ID, planned distance, skip date, skip reason, training plan ID, user ID
- **Consumers**: Training adherence tracker, plan adjuster, rest day analyzer

#### MilestoneDistanceReached
- **Description**: Cumulative distance milestone achieved
- **Triggered When**: Total lifetime distance crosses threshold (100 miles, 500 miles, 1000 miles, etc.)
- **Key Data**: Milestone ID, milestone distance, achievement date, total runs, user ID
- **Consumers**: Achievement service, notification system, milestone tracker

### TrainingPlanEvents

#### TrainingPlanCreated
- **Description**: New training program established
- **Triggered When**: User creates or selects training plan for race or goal
- **Key Data**: Plan ID, plan name, goal race, plan duration, weekly mileage target, start date, user ID, timestamp
- **Consumers**: Plan manager, run scheduler, workout generator, dashboard UI

#### TrainingPlanStarted
- **Description**: User has begun following training plan
- **Triggered When**: User activates and commits to training program
- **Key Data**: Plan ID, start date, estimated completion, race date, user ID
- **Consumers**: Workout scheduler, reminder service, adherence tracker

#### TrainingPlanCompleted
- **Description**: All workouts in training plan finished
- **Triggered When**: User completes final workout of plan
- **Key Data**: Plan ID, completion date, adherence percentage, goal achieved, user ID
- **Consumers**: Achievement service, race readiness evaluator, historical archiver

#### WorkoutCompleted
- **Description**: Specific training workout finished
- **Triggered When**: User completes scheduled workout (tempo, intervals, long run, etc.)
- **Key Data**: Workout ID, workout type, target achieved, actual performance, completion date, user ID
- **Consumers**: Training plan tracker, adaptation analyzer, performance evaluator

### RaceEvents

#### RaceRegistered
- **Description**: User signed up for upcoming race
- **Triggered When**: User registers for race event
- **Key Data**: Race ID, race name, race distance, race date, location, registration date, bib number, user ID
- **Consumers**: Race calendar, countdown tracker, training plan generator, reminder service

#### RaceCompleted
- **Description**: Race event finished
- **Triggered When**: User completes race and logs results
- **Key Data**: Race ID, finish time, pace, placement overall, age group placement, race conditions, user ID
- **Consumers**: Race history, PR checker, performance analyzer, achievement service

#### RaceGoalSet
- **Description**: Target time established for upcoming race
- **Triggered When**: User sets goal finish time
- **Key Data**: Goal ID, race ID, goal time, goal pace, goal basis, user ID, timestamp
- **Consumers**: Goal tracker, training pace calculator, race predictor

#### RaceGoalAchieved
- **Description**: Race finished at or better than goal time
- **Triggered When**: Race time meets or beats goal
- **Key Data**: Race ID, goal time, actual time, achievement margin, achievement date, user ID
- **Consumers**: Achievement service, celebration trigger, confidence booster

#### RaceWithdrawn
- **Description**: User cancelled race registration or did not participate
- **Triggered When**: User marks race as DNS (did not start)
- **Key Data**: Race ID, withdrawal reason, withdrawal date, user ID
- **Consumers**: Race calendar updater, training plan adjuster, refund tracker

### PerformanceEvents

#### WeeklyMileageCalculated
- **Description**: Total weekly running distance computed
- **Triggered When**: Week ends or user requests weekly summary
- **Key Data**: Week ending date, total distance, total time, average pace, runs completed, user ID
- **Consumers**: Mileage dashboard, training load monitor, injury risk assessor

#### PaceImprovementDetected
- **Description**: Average pace improving over time
- **Triggered When**: Recent pace consistently faster than previous period
- **Key Data**: Distance category, previous average pace, new average pace, improvement percentage, detection period
- **Consumers**: Progress tracker, motivation service, training effectiveness analyzer

#### FitnessLevelCalculated
- **Description**: Estimated fitness level computed from recent runs
- **Triggered When**: Sufficient run data accumulated for fitness estimation
- **Key Data**: Fitness score, VO2 max estimate, calculation date, trend direction, user ID
- **Consumers**: Fitness dashboard, race time predictor, training zone calculator

#### RaceTimePredicted
- **Description**: Predicted finish time for race distance calculated
- **Triggered When**: User requests race prediction or training analysis runs
- **Key Data**: Race distance, predicted time, confidence level, based on recent performances, user ID
- **Consumers**: Race goal advisor, training pace setter, expectation manager

### WorkoutTypeEvents

#### TempoRunCompleted
- **Description**: Tempo/threshold workout finished
- **Triggered When**: User completes tempo run at prescribed pace
- **Key Data**: Workout ID, distance, tempo pace, target pace, average HR, completion quality, user ID
- **Consumers**: Lactate threshold tracker, performance analyzer, training plan updater

#### IntervalSessionCompleted
- **Description**: Interval/speed workout finished
- **Triggered When**: User completes intervals workout
- **Key Data**: Workout ID, interval count, interval distance, target pace, actual paces, recovery time, user ID
- **Consumers**: Speed development tracker, workout analyzer, fitness assessor

#### LongRunCompleted
- **Description**: Weekly long run finished
- **Triggered When**: User completes longest run of week
- **Key Data**: Run ID, distance, duration, pace, fueling used, how felt, user ID
- **Consumers**: Endurance tracker, race readiness assessor, adaptation monitor

#### RecoveryRunCompleted
- **Description**: Easy recovery run finished
- **Triggered When**: User completes low-intensity recovery run
- **Key Data**: Run ID, distance, easy pace maintained, heart rate, recovery quality, user ID
- **Consumers**: Recovery tracker, training balance monitor, fatigue manager

### StreakEvents

#### RunStreakStarted
- **Description**: Daily running streak initiated
- **Triggered When**: User commits to running every day
- **Key Data**: Streak ID, start date, minimum daily distance, user ID
- **Consumers**: Streak tracker, daily reminder, motivation service

#### RunStreakContinued
- **Description**: Running streak milestone reached
- **Triggered When**: Streak reaches milestone days (7, 30, 100, 365)
- **Key Data**: Streak ID, streak length, milestone day, total distance in streak, user ID
- **Consumers**: Achievement service, motivation system, social sharing

#### RunStreakBroken
- **Description**: Running streak ended
- **Triggered When**: Day passes without run during active streak
- **Key Data**: Streak ID, final length, end date, total distance, break reason, user ID
- **Consumers**: Streak archiver, motivation service, restart advisor

### RouteEvents

#### RouteCreated
- **Description**: New running route saved
- **Triggered When**: User saves route with waypoints
- **Key Data**: Route ID, route name, distance, elevation, surface type, start/end location, user ID, timestamp
- **Consumers**: Route library, run planner, distance calculator

#### RouteCompleted
- **Description**: Saved route run and completed
- **Triggered When**: User runs a saved route
- **Key Data**: Run ID, route ID, completion time, pace, route conditions, user ID
- **Consumers**: Route history, best time tracker, route recommendations

#### FavoriteRouteSet
- **Description**: Route marked as favorite
- **Triggered When**: User favorites frequently run route
- **Key Data**: Route ID, favorite date, user ID
- **Consumers**: Quick route selector, route recommender, favorites list

### InjuryEvents

#### InjuryConcernReported
- **Description**: Pain or injury concern logged
- **Triggered When**: User reports pain or discomfort
- **Key Data**: Concern ID, body part, severity, onset date, description, user ID, timestamp
- **Consumers**: Injury tracker, training adjuster, rest recommender, medical alert

#### InjuryRecoveryStarted
- **Description**: Recovery period initiated for injury
- **Triggered When**: User marks injury and begins recovery protocol
- **Key Data**: Recovery ID, injury type, recovery plan, estimated return date, user ID
- **Consumers**: Training plan suspender, recovery tracker, return-to-running planner

#### ReturnToRunning
- **Description**: User resumed running after injury
- **Triggered When**: First run after injury recovery period
- **Key Data**: Return ID, injury duration, first run details, return protocol followed, user ID
- **Consumers**: Injury history, training ramp-up manager, recurrence monitor

### EquipmentEvents

#### ShoesAdded
- **Description**: New running shoes added to rotation
- **Triggered When**: User registers new pair of running shoes
- **Key Data**: Shoe ID, brand, model, purchase date, initial mileage, retirement mileage target, user ID
- **Consumers**: Shoe rotation tracker, mileage counter, replacement reminder

#### ShoeMileageUpdated
- **Description**: Miles logged on running shoes
- **Triggered When**: Run completed with specific shoes
- **Key Data**: Shoe ID, miles added, total miles, remaining life estimate, user ID
- **Consumers**: Shoe wear tracker, replacement alert, rotation manager

#### ShoeReplacementDue
- **Description**: Running shoes reached replacement mileage
- **Triggered When**: Shoe mileage exceeds recommended limit
- **Key Data**: Shoe ID, total miles, replacement threshold, purchase date, user ID
- **Consumers**: Alert service, shoe shopping reminder, injury prevention

### GoalEvents

#### RunningGoalSet
- **Description**: Running objective established
- **Triggered When**: User sets distance, pace, or race goal
- **Key Data**: Goal ID, goal type, target metric, target value, deadline, user ID, timestamp
- **Consumers**: Goal tracker, training plan generator, motivation service

#### WeeklyGoalAssessed
- **Description**: Weekly running goal progress evaluated
- **Triggered When**: Week ends or user requests progress check
- **Key Data**: Goal ID, target for week, actual achievement, variance, assessment date, user ID
- **Consumers**: Progress dashboard, adjustment recommender, motivation service
