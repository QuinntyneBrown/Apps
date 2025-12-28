# Domain Events - Injury Prevention & Recovery Tracker

## Overview
This document defines the domain events tracked in the Injury Prevention & Recovery Tracker application. These events capture significant business occurrences related to injury logging, recovery protocols, rehabilitation exercises, and prevention strategies.

## Events

### InjuryEvents

#### InjuryReported
- **Description**: A new injury has been documented
- **Triggered When**: User logs an injury occurrence
- **Key Data**: Injury ID, body part, injury type, severity, onset date, cause, symptoms, user ID, timestamp
- **Consumers**: Injury tracker, recovery planner, medical alert, prevention analyzer

#### InjurySeverityAssessed
- **Description**: Injury severity level determined
- **Triggered When**: User or system evaluates injury seriousness
- **Key Data**: Injury ID, severity level (minor/moderate/severe), pain level, mobility impact, assessment date, user ID
- **Consumers**: Recovery protocol selector, medical referral trigger, rest period calculator

#### MedicalAttentionSought
- **Description**: Professional medical care obtained for injury
- **Triggered When**: User visits doctor, PT, or specialist for injury
- **Key Data**: Injury ID, provider type, visit date, diagnosis, treatment plan, follow-up scheduled, user ID
- **Consumers**: Medical record tracker, treatment protocol updater, insurance coordinator

#### InjuryRecurredInjury
- **Description**: Previously healed injury has returned
- **Triggered When**: Same injury location and type reoccurs
- **Key Data**: Injury ID, original injury ID, recurrence date, time since healing, possible causes, user ID
- **Consumers**: Recurrence analyzer, prevention strategy modifier, risk assessment updater

### RecoveryEvents

#### RecoveryPlanCreated
- **Description**: Rehabilitation protocol established
- **Triggered When**: Recovery strategy developed for injury
- **Key Data**: Plan ID, injury ID, recovery phases, estimated duration, exercises prescribed, restrictions, user ID, timestamp
- **Consumers**: Recovery tracker, exercise scheduler, restriction enforcer, progress monitor

#### RecoveryPhaseCompleted
- **Description**: Stage of rehabilitation finished
- **Triggered When**: User completes recovery phase (acute, subacute, return-to-activity)
- **Key Data**: Phase ID, injury ID, phase type, completion date, readiness for next phase, user ID
- **Consumers**: Next phase activator, progress tracker, medical update generator

#### RecoveryMilestoneReached
- **Description**: Significant recovery progress achieved
- **Triggered When**: Key healing indicator met (pain-free, range of motion restored, etc.)
- **Key Data**: Milestone ID, injury ID, milestone type, achievement date, remaining milestones, user ID
- **Consumers**: Achievement service, progress visualizer, motivation system

#### FullRecoveryAchieved
- **Description**: Injury completely healed and normal function restored
- **Triggered When**: All recovery goals met and cleared for full activity
- **Key Data**: Injury ID, recovery start date, recovery completion date, total recovery time, user ID
- **Consumers**: Achievement service, injury history archiver, prevention lessons learner

### RehabExerciseEvents

#### RehabExerciseAssigned
- **Description**: Therapeutic exercise added to recovery plan
- **Triggered When**: PT or user adds rehab exercise to protocol
- **Key Data**: Exercise ID, injury ID, exercise name, sets, reps, frequency, progression criteria, user ID, timestamp
- **Consumers**: Exercise scheduler, recovery plan, progress tracker

#### RehabExerciseCompleted
- **Description**: Rehabilitation exercise session finished
- **Triggered When**: User completes prescribed rehab exercise
- **Key Data**: Session ID, exercise ID, completion date, sets/reps completed, pain level, difficulty, user ID
- **Consumers**: Adherence tracker, progress monitor, exercise effectiveness analyzer

#### RehabExerciseProgressed
- **Description**: Rehab exercise difficulty increased
- **Triggered When**: Exercise advanced to harder variation
- **Key Data**: Exercise ID, previous difficulty, new difficulty, progression criteria met, progression date, user ID
- **Consumers**: Recovery progress indicator, strength gain tracker, return-to-activity readiness

#### RehabExercisePainful
- **Description**: Pain experienced during rehab exercise
- **Triggered When**: User reports pain above acceptable level during exercise
- **Key Data**: Exercise ID, pain level, pain location, exercise stopped, report timestamp, user ID
- **Consumers**: Alert service, exercise modification recommender, recovery plan adjuster

### PainEvents

#### PainLevelRecorded
- **Description**: Injury pain rating logged
- **Triggered When**: User records pain level on scale
- **Key Data**: Pain ID, injury ID, pain level (0-10), pain type, activity context, log timestamp, user ID
- **Consumers**: Pain tracker, trend analyzer, recovery progress monitor

#### PainIncreasing
- **Description**: Pain trending worse over time
- **Triggered When**: Pain levels rising over multiple recordings
- **Key Data**: Injury ID, pain trend, average increase, trend period, user ID
- **Consumers**: Alert service, medical consultation recommender, recovery plan reviewer

#### PainFreeAchieved
- **Description**: Zero pain reported for first time or sustained period
- **Triggered When**: Pain level at zero for milestone period
- **Key Data**: Injury ID, pain-free date, days pain-free, user ID
- **Consumers**: Achievement service, recovery milestone tracker, activity progression enabler

### PreventionEvents

#### PreventionExerciseScheduled
- **Description**: Injury prevention exercise added to routine
- **Triggered When**: User adds prehab/prevention exercises to schedule
- **Key Data**: Exercise ID, target body part, frequency, injury prevention focus, user ID, timestamp
- **Consumers**: Exercise scheduler, prevention program manager, injury risk reducer

#### PreventionRoutineCompleted
- **Description**: Prevention exercise session finished
- **Triggered When**: User completes prehab routine
- **Key Data**: Session ID, exercises completed, completion date, consistency streak, user ID
- **Consumers**: Adherence tracker, injury prevention score updater, habit reinforcer

#### InjuryRiskFactorIdentified
- **Description**: Risk factor for injury detected
- **Triggered When**: Analysis identifies vulnerability (muscle imbalance, overtraining, poor form)
- **Key Data**: Risk factor type, affected area, severity, mitigation strategies, detection timestamp, user ID
- **Consumers**: Prevention recommender, alert service, training modification advisor

#### HighRiskActivityDetected
- **Description**: Activity with elevated injury risk identified
- **Triggered When**: User logs activity known to cause injuries
- **Key Data**: Activity type, injury risk level, prevention recommendations, user ID
- **Consumers**: Warning service, prevention strategy suggester, injury correlation tracker

### MobilityEvents

#### RangeOfMotionTested
- **Description**: Joint mobility measurement recorded
- **Triggered When**: User tests ROM for injured area
- **Key Data**: Test ID, injury ID, joint, ROM measurement, ROM percentage of normal, test date, user ID
- **Consumers**: Mobility tracker, recovery progress monitor, exercise progression determiner

#### MobilityImproved
- **Description**: Range of motion increased
- **Triggered When**: ROM measurement shows improvement
- **Key Data**: Injury ID, previous ROM, new ROM, improvement percentage, user ID
- **Consumers**: Progress tracker, motivation service, recovery phase advancer

#### MobilityRestored
- **Description**: Full range of motion regained
- **Triggered When**: ROM equals or exceeds pre-injury baseline
- **Key Data**: Injury ID, restoration date, final ROM, time to restoration, user ID
- **Consumers**: Achievement service, recovery milestone tracker, activity clearance enabler

### StrengthEvents

#### StrengthTestPerformed
- **Description**: Strength assessment conducted for injured area
- **Triggered When**: User tests strength of recovering area
- **Key Data**: Test ID, injury ID, muscle group, strength score, percentage of uninjured side, test date, user ID
- **Consumers**: Strength tracker, recovery evaluator, exercise progression guide

#### StrengthDeficitIdentified
- **Description**: Significant strength imbalance detected
- **Triggered When**: Injured side weaker than threshold percentage
- **Key Data**: Injury ID, deficit percentage, target strength, strengthening needed, user ID
- **Consumers**: Exercise prescriber, imbalance corrector, injury recurrence risk indicator

#### StrengthBalanceRestored
- **Description**: Strength symmetry between sides achieved
- **Triggered When**: Injured side within acceptable percentage of uninjured side
- **Key Data**: Injury ID, achievement date, strength balance percentage, user ID
- **Consumers**: Achievement service, return-to-activity clearance, prevention validator

### ActivityEvents

#### ActivityRestrictionSet
- **Description**: Activity limitations established for injury
- **Triggered When**: Activities to avoid during recovery defined
- **Key Data**: Restriction ID, injury ID, restricted activities, restriction level, restriction duration, user ID
- **Consumers**: Activity monitor, restriction enforcer, compliance checker

#### RestrictedActivityAttempted
- **Description**: User performed restricted activity
- **Triggered When**: User logs activity they should avoid
- **Key Data**: Restriction ID, activity performed, injury risk incurred, attempt date, user ID
- **Consumers**: Compliance alert, recovery risk warning, setback preventer

#### ActivityProgressionApproved
- **Description**: Clearance to advance activity level granted
- **Triggered When**: Recovery milestones allow increased activity
- **Key Data**: Injury ID, previous activity level, new activity level, approval criteria met, user ID
- **Consumers**: Activity expander, restriction updater, return-to-sport facilitator

#### ReturnToSportAchieved
- **Description**: Full clearance to resume pre-injury activities
- **Triggered When**: All recovery criteria met and full function restored
- **Key Data**: Injury ID, sport/activity, clearance date, total time away, user ID
- **Consumers**: Achievement service, injury history, performance monitoring reactivator

### TreatmentEvents

#### TreatmentModalityApplied
- **Description**: Therapeutic treatment used
- **Triggered When**: User applies ice, heat, compression, elevation, etc.
- **Key Data**: Treatment ID, injury ID, treatment type, duration, effectiveness rating, application date, user ID
- **Consumers**: Treatment tracker, effectiveness analyzer, protocol adherence monitor

#### MedicationTaken
- **Description**: Pain or anti-inflammatory medication used
- **Triggered When**: User takes medication for injury
- **Key Data**: Medication ID, injury ID, medication type, dosage, administration time, user ID
- **Consumers**: Medication tracker, pain correlation analyzer, usage pattern monitor

#### PhysicalTherapySessionCompleted
- **Description**: PT appointment attended
- **Triggered When**: User completes physical therapy session
- **Key Data**: Session ID, injury ID, session date, exercises performed, therapist notes, progress assessment, user ID
- **Consumers**: PT tracker, insurance documentation, recovery progress monitor

### SetbackEvents

#### RecoverySetbackOccurred
- **Description**: Injury recovery regressed
- **Triggered When**: Symptoms worsen or healing reverses
- **Key Data**: Setback ID, injury ID, setback trigger, severity, symptoms returned, setback date, user ID
- **Consumers**: Alert service, recovery plan modifier, medical consultation trigger

#### OveruseDetected
- **Description**: Excessive activity during recovery identified
- **Triggered When**: Activity volume exceeds safe levels for injury
- **Key Data**: Injury ID, activity type, volume, safe limit, overuse date, user ID
- **Consumers**: Activity restrictor, rest enforcer, setback preventer

### EducationEvents

#### InjuryEducationCompleted
- **Description**: User reviewed educational content about injury
- **Triggered When**: User completes learning module about injury type
- **Key Data**: Education ID, injury type, topics covered, completion date, user ID
- **Consumers**: Knowledge tracker, prevention effectiveness improver, self-management enabler

#### ProperFormReminder
- **Description**: Technique correction prompt delivered
- **Triggered When**: User about to perform activity with injury history
- **Key Data**: Activity type, form cues, common mistakes, reminder timestamp, user ID
- **Consumers**: Injury prevention, technique coaching, recurrence preventer
