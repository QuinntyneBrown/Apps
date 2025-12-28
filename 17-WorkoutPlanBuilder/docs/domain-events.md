# Domain Events - Workout Plan Builder

## Overview
This document defines the domain events tracked in the Workout Plan Builder application. These events capture significant business occurrences related to workout routine creation, exercise tracking, progress monitoring, and fitness program management.

## Events

### WorkoutPlanEvents

#### WorkoutPlanCreated
- **Description**: A new workout program has been designed
- **Triggered When**: User creates custom workout routine or selects template
- **Key Data**: Plan ID, plan name, goal type, duration weeks, frequency, difficulty level, user ID, timestamp
- **Consumers**: Plan manager, workout scheduler, progression tracker, dashboard UI

#### WorkoutPlanStarted
- **Description**: User has begun following a workout plan
- **Triggered When**: User commits to and activates a workout program
- **Key Data**: Plan ID, start date, estimated completion date, commitment level, user ID
- **Consumers**: Workout scheduler, reminder service, progress tracker

#### WorkoutPlanCompleted
- **Description**: All workouts in a plan have been finished
- **Triggered When**: User completes final workout in program
- **Key Data**: Plan ID, completion date, total workouts, adherence percentage, time to complete, user ID
- **Consumers**: Achievement service, historical archiver, next program recommender

#### WorkoutPlanModified
- **Description**: Active workout plan has been adjusted
- **Triggered When**: User customizes exercises, sets, reps, or schedule
- **Key Data**: Plan ID, modification type, previous values, new values, modification date, user ID
- **Consumers**: Plan updater, progression adjuster, workout regenerator

### WorkoutSessionEvents

#### WorkoutSessionStarted
- **Description**: User has begun a workout session
- **Triggered When**: User starts workout from their plan
- **Key Data**: Session ID, plan ID, workout type, planned exercises, start timestamp, user ID
- **Consumers**: Active workout tracker, timer service, exercise queue manager

#### WorkoutSessionCompleted
- **Description**: Workout session has been finished
- **Triggered When**: User marks workout as complete
- **Key Data**: Session ID, completion timestamp, duration, exercises completed, session notes, user ID
- **Consumers**: Progress tracker, streak checker, workout history, calorie estimator

#### WorkoutSessionSkipped
- **Description**: Scheduled workout was not performed
- **Triggered When**: Workout day passes without session completion
- **Key Data**: Session ID, skip date, skip reason, workout type, user ID
- **Consumers**: Adherence tracker, motivation service, schedule adjuster

#### WorkoutSessionAbandoned
- **Description**: Started workout was not completed
- **Triggered When**: User exits workout before finishing all exercises
- **Key Data**: Session ID, start time, end time, exercises completed, total exercises, abandonment reason, user ID
- **Consumers**: Adherence tracker, difficulty analyzer, program adjuster

### ExerciseEvents

#### ExerciseAddedToLibrary
- **Description**: New exercise added to personal exercise database
- **Triggered When**: User creates custom exercise or imports from library
- **Key Data**: Exercise ID, exercise name, muscle groups, equipment needed, difficulty, user ID, timestamp
- **Consumers**: Exercise library, workout builder, search index

#### ExercisePerformed
- **Description**: Individual exercise completed during workout
- **Triggered When**: User logs sets and reps for an exercise
- **Key Data**: Performance ID, session ID, exercise ID, sets, reps, weight, rest time, form notes, user ID
- **Consumers**: Progress tracker, volume calculator, personal record checker

#### ExerciseSubstituted
- **Description**: Planned exercise replaced with alternative
- **Triggered When**: User swaps exercise due to equipment or preference
- **Key Data**: Session ID, original exercise ID, substitute exercise ID, substitution reason, user ID
- **Consumers**: Workout adjuster, preference learner, equipment tracker

### ProgressEvents

#### PersonalRecordSet
- **Description**: New personal best achieved for an exercise
- **Triggered When**: Weight, reps, or volume exceeds previous best
- **Key Data**: Record ID, exercise ID, record type (1RM/volume/reps), previous record, new record, achievement date, user ID
- **Consumers**: Achievement service, celebration trigger, motivation system, leaderboard

#### StrengthGainRecorded
- **Description**: Measurable strength improvement logged
- **Triggered When**: Progressive overload milestone reached
- **Key Data**: Exercise ID, previous max, new max, percentage gain, time period, user ID
- **Consumers**: Progress visualizer, program effectiveness analyzer, motivation tracker

#### WorkoutVolumeCalculated
- **Description**: Total training volume for session or period computed
- **Triggered When**: Workout completed or period summary requested
- **Key Data**: Period, total volume (sets × reps × weight), volume by muscle group, volume trend, timestamp
- **Consumers**: Volume dashboard, overtraining detector, periodization planner

#### PlateauDetected
- **Description**: Lack of progress over extended period identified
- **Triggered When**: Exercise performance stagnant for X weeks
- **Key Data**: Exercise ID, plateau duration, last progress date, recommended actions, detection timestamp
- **Consumers**: Alert service, deload recommender, program modification advisor

### SchedulingEvents

#### WorkoutScheduled
- **Description**: Workout session added to calendar
- **Triggered When**: User schedules specific workout for date/time
- **Key Data**: Schedule ID, plan ID, workout type, scheduled date, scheduled time, user ID, timestamp
- **Consumers**: Calendar integrator, reminder scheduler, workout queue

#### WorkoutRescheduled
- **Description**: Scheduled workout moved to different time
- **Triggered When**: User changes workout date or time
- **Key Data**: Schedule ID, original date/time, new date/time, reschedule reason, user ID
- **Consumers**: Calendar updater, reminder adjuster, adherence tracker

#### RestDayScheduled
- **Description**: Recovery day added to training schedule
- **Triggered When**: User designates rest day in program
- **Key Data**: Rest day ID, date, rest type (active/complete), recovery activities, user ID
- **Consumers**: Recovery tracker, training load balancer, schedule manager

### ProgressionEvents

#### WeightProgressed
- **Description**: Training weight increased for an exercise
- **Triggered When**: User successfully increases load
- **Key Data**: Progression ID, exercise ID, previous weight, new weight, progression date, user ID
- **Consumers**: Progressive overload tracker, strength gain calculator, workout updater

#### DeloadWeekTriggered
- **Description**: Reduced intensity week initiated
- **Triggered When**: Scheduled deload or fatigue management requires it
- **Key Data**: Deload ID, week number, intensity reduction percentage, deload reason, user ID, timestamp
- **Consumers**: Workout adjuster, recovery optimizer, fatigue manager

#### PhaseCompleted
- **Description**: Training phase finished in periodized program
- **Triggered When**: User completes hypertrophy, strength, or power phase
- **Key Data**: Phase ID, phase type, duration, performance gains, next phase, completion date, user ID
- **Consumers**: Periodization manager, next phase activator, progress summarizer

### BodyMetricsEvents

#### BodyMeasurementRecorded
- **Description**: Body measurements logged (chest, arms, waist, etc.)
- **Triggered When**: User records body part measurements
- **Key Data**: Measurement ID, body part, measurement value, measurement date, user ID
- **Consumers**: Progress tracker, body composition analyzer, visual progress monitor

#### ProgressPhotoUploaded
- **Description**: Progress photo added to tracking
- **Triggered When**: User uploads before/after or progress photo
- **Key Data**: Photo ID, upload date, photo tags, associated measurements, user ID
- **Consumers**: Visual progress tracker, transformation analyzer, motivation gallery

### AdherenceEvents

#### WorkoutStreakAchieved
- **Description**: Consecutive workout streak milestone reached
- **Triggered When**: User completes workouts for X consecutive days/weeks
- **Key Data**: Streak type, streak length, achievement date, user ID
- **Consumers**: Achievement service, gamification system, motivation tracker

#### AdherenceRateCalculated
- **Description**: Workout completion percentage computed
- **Triggered When**: Weekly or monthly adherence analysis runs
- **Key Data**: Period, workouts planned, workouts completed, adherence percentage, timestamp, user ID
- **Consumers**: Adherence dashboard, program sustainability analyzer, goal adjuster

### EquipmentEvents

#### EquipmentAvailabilitySet
- **Description**: Available gym equipment specified
- **Triggered When**: User indicates what equipment they have access to
- **Key Data**: Equipment list, availability type (home/gym), update date, user ID
- **Consumers**: Workout filter, exercise recommender, plan customizer

#### EquipmentSubstitutionSuggested
- **Description**: Alternative exercise suggested based on available equipment
- **Triggered When**: Planned exercise requires unavailable equipment
- **Key Data**: Original exercise, suggested alternatives, equipment requirement, user ID
- **Consumers**: Exercise substitution service, workout adjuster

### GoalEvents

#### FitnessGoalSet
- **Description**: Specific fitness objective established
- **Triggered When**: User sets goal (build muscle, lose fat, increase strength, etc.)
- **Key Data**: Goal ID, goal type, target metric, target value, deadline, user ID, timestamp
- **Consumers**: Goal tracker, program recommender, progress monitor

#### GoalProgressEvaluated
- **Description**: Progress toward fitness goal assessed
- **Triggered When**: Periodic evaluation or user requests progress check
- **Key Data**: Goal ID, progress percentage, on-track status, adjustments needed, evaluation date, user ID
- **Consumers**: Progress dashboard, motivation service, program adjuster

### TemplateEvents

#### WorkoutTemplateCreated
- **Description**: Reusable workout template saved
- **Triggered When**: User saves workout configuration as template
- **Key Data**: Template ID, template name, exercises, sets/reps scheme, target audience, user ID, timestamp
- **Consumers**: Template library, workout quick-start, sharing service

#### TemplateUsed
- **Description**: Saved template used to create workout
- **Triggered When**: User starts workout from template
- **Key Data**: Template ID, usage date, customizations made, user ID
- **Consumers**: Template popularity tracker, recommendation engine, usage analytics
