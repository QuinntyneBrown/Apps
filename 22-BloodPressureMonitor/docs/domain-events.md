# Domain Events - Blood Pressure Monitor

## Overview
This document defines the domain events tracked in the Blood Pressure Monitor application. These events capture significant business occurrences related to blood pressure readings, trend analysis, hypertension management, and cardiovascular health tracking.

## Events

### ReadingEvents

#### BloodPressureRecorded
- **Description**: A blood pressure measurement has been logged
- **Triggered When**: User enters or device syncs BP reading
- **Key Data**: Reading ID, systolic, diastolic, pulse, measurement time, measurement context, arm used, user ID, timestamp
- **Consumers**: BP tracker, trend analyzer, alert checker, dashboard UI

#### PulseRecorded
- **Description**: Heart rate measurement logged with BP reading
- **Triggered When**: User records pulse alongside BP measurement
- **Key Data**: Reading ID, pulse rate, rhythm regularity, measurement time, user ID
- **Consumers**: Heart rate tracker, cardiovascular analyzer, irregular rhythm detector

#### MeasurementContextLogged
- **Description**: Circumstances of BP measurement recorded
- **Triggered When**: User notes context (resting, post-exercise, stressed, etc.)
- **Key Data**: Reading ID, context type, time since activity, posture, user ID
- **Consumers**: Reading validator, trend accuracy improver, context correlation analyzer

### AlertEvents

#### HighBloodPressureDetected
- **Description**: Elevated BP reading identified
- **Triggered When**: Systolic or diastolic exceeds healthy thresholds
- **Key Data**: Reading ID, systolic, diastolic, severity level (Stage 1/2/Crisis), alert timestamp, user ID
- **Consumers**: Alert service, healthcare notification, urgent care recommender, medical alert

#### LowBloodPressureDetected
- **Description**: Hypotensive reading identified
- **Triggered When**: BP reading significantly below normal range
- **Key Data**: Reading ID, systolic, diastolic, severity, symptoms present, user ID
- **Consumers**: Alert service, medical notification, symptom checker

#### HypertensiveCrisisDetected
- **Description**: Dangerously high BP requiring immediate attention
- **Triggered When**: Systolic >180 or diastolic >120
- **Key Data**: Reading ID, systolic, diastolic, crisis type (emergency/urgency), detection time, user ID
- **Consumers**: Emergency alert service, medical contact notification, urgent care coordinator

#### IrregularHeartbeatDetected
- **Description**: Abnormal heart rhythm identified during BP measurement
- **Triggered When**: Device or user notes irregular pulse
- **Key Data**: Reading ID, irregularity type, pulse rate, detection timestamp, user ID
- **Consumers**: Cardiac alert service, arrhythmia tracker, cardiology referral recommender

### TrendEvents

#### TrendAnalysisCompleted
- **Description**: BP trend over time calculated
- **Triggered When**: Periodic analysis or user requests trend view
- **Key Data**: Analysis period, average systolic, average diastolic, trend direction, variance, timestamp, user ID
- **Consumers**: Trend visualizer, health insights, doctor report generator

#### BloodPressureRisingTrend
- **Description**: Upward BP trend over multiple readings
- **Triggered When**: Statistical analysis shows increasing BP pattern
- **Key Data**: Trend start date, rate of increase, projected future levels, confidence level, user ID
- **Consumers**: Early warning service, intervention recommender, medication adjustment suggester

#### BloodPressureLoweringTrend
- **Description**: Downward BP trend indicating improvement
- **Triggered When**: Consistent decrease in BP readings over time
- **Key Data**: Trend start date, rate of decrease, improvement percentage, user ID
- **Consumers**: Progress tracker, motivation service, treatment effectiveness indicator

#### VolatilityDetected
- **Description**: Significant BP fluctuation between readings
- **Triggered When**: High variability in consecutive measurements
- **Key Data**: Variability score, reading range, fluctuation pattern, detection period, user ID
- **Consumers**: Medical alert, measurement technique reviewer, stability concern tracker

### GoalEvents

#### BloodPressureGoalSet
- **Description**: Target BP range established
- **Triggered When**: User or doctor sets BP goals
- **Key Data**: Goal ID, target systolic range, target diastolic range, goal deadline, user ID, timestamp
- **Consumers**: Goal tracker, achievement monitor, progress calculator

#### BloodPressureGoalReached
- **Description**: BP reading within target range
- **Triggered When**: Reading falls within goal parameters
- **Key Data**: Reading ID, goal achieved, achievement date, user ID
- **Consumers**: Achievement service, streak tracker, motivation system

#### ConsistentControlAchieved
- **Description**: BP maintained in healthy range over extended period
- **Triggered When**: X consecutive readings or Y% of readings in goal range
- **Key Data**: Achievement period, readings in range, success percentage, achievement date, user ID
- **Consumers**: Achievement service, health milestone tracker, doctor report

### MedicationEvents

#### BPMedicationRecorded
- **Description**: Blood pressure medication dose logged
- **Triggered When**: User records taking BP medication
- **Key Data**: Medication ID, dose time, medication name, dosage, user ID, timestamp
- **Consumers**: Medication tracker, medication effectiveness analyzer, adherence monitor

#### MedicationEffectivenessAnalyzed
- **Description**: Impact of medication on BP assessed
- **Triggered When**: Sufficient readings before/after medication change
- **Key Data**: Medication ID, pre-medication average BP, post-medication average BP, effectiveness score, user ID
- **Consumers**: Treatment evaluator, doctor report, medication adjustment recommender

#### MedicationTakenBeforeReading
- **Description**: BP reading taken in relation to medication timing
- **Triggered When**: User notes time since last medication dose
- **Key Data**: Reading ID, hours since medication, medication name, user ID
- **Consumers**: Timing correlation analyzer, peak/trough detector, dosing optimizer

### LifestyleEvents

#### LifestyleFactorLogged
- **Description**: Lifestyle factor affecting BP recorded
- **Triggered When**: User logs sodium intake, exercise, stress, alcohol, etc.
- **Key Data**: Factor ID, factor type, intensity/amount, log date, user ID, timestamp
- **Consumers**: Lifestyle tracker, BP correlation analyzer, recommendation engine

#### SodiumIntakeTracked
- **Description**: Daily sodium consumption logged
- **Triggered When**: User tracks dietary sodium
- **Key Data**: Date, sodium amount (mg), meals tracked, user ID
- **Consumers**: Sodium tracker, BP correlation analyzer, dietary recommender

#### ExerciseImpactAnalyzed
- **Description**: Effect of physical activity on BP assessed
- **Triggered When**: BP readings correlated with exercise log
- **Key Data**: Exercise type, BP change pattern, correlation strength, optimal timing, user ID
- **Consumers**: Exercise advisor, BP management optimizer, lifestyle recommender

#### StressCorrelationIdentified
- **Description**: Relationship between stress and BP found
- **Triggered When**: Stress levels correlated with elevated BP
- **Key Data**: Stress indicator, BP impact magnitude, correlation confidence, user ID
- **Consumers**: Stress management advisor, relaxation technique recommender, intervention suggester

### MeasurementQualityEvents

#### ProperMeasurementTechniqueConfirmed
- **Description**: User followed correct BP measurement protocol
- **Triggered When**: User confirms adherence to measurement guidelines
- **Key Data**: Reading ID, protocol checklist completed, arm position, rest period, user ID
- **Consumers**: Reading quality validator, measurement reliability scorer

#### MeasurementErrorDetected
- **Description**: Potential measurement error identified
- **Triggered When**: Reading seems inconsistent with pattern or implausible
- **Key Data**: Reading ID, error type, expected range, suggestion to remeasure, user ID
- **Consumers**: Error alert, remeasurement suggester, data quality manager

#### MultipleReadingsAveraged
- **Description**: Average of multiple readings calculated
- **Triggered When**: User takes recommended 2-3 consecutive readings
- **Key Data**: Session ID, individual readings, averaged result, variance, user ID
- **Consumers**: Accuracy improver, clinical standard follower, reliable data generator

### TimePatternEvents

#### MorningReadingRecorded
- **Description**: BP measured in morning time window
- **Triggered When**: Reading taken within morning measurement period
- **Key Data**: Reading ID, exact time, days since last morning reading, user ID
- **Consumers**: Consistency tracker, morning BP trend analyzer, circadian pattern detector

#### EveningReadingRecorded
- **Description**: BP measured in evening time window
- **Triggered When**: Reading taken within evening measurement period
- **Key Data**: Reading ID, exact time, days since last evening reading, user ID
- **Consumers**: Consistency tracker, evening BP trend analyzer, diurnal variation monitor

#### WhiteCoatEffectSuspected
- **Description**: BP higher in clinical settings than at home
- **Triggered When**: Comparison shows clinic readings elevated vs. home
- **Key Data**: Home average, clinic average, difference, confidence level, user ID
- **Consumers**: White coat hypertension identifier, home monitoring validator, doctor discussion point

#### NocturnalHypertensionDetected
- **Description**: Elevated BP during nighttime hours
- **Triggered When**: Night readings consistently higher than daytime
- **Key Data**: Night average, day average, dipping status, detection period, user ID
- **Consumers**: Sleep disorder screener, cardiovascular risk assessor, medical referral

### ReportEvents

#### WeeklyBPReportGenerated
- **Description**: Weekly blood pressure summary created
- **Triggered When**: Week ends or user requests report
- **Key Data**: Week ending date, average BP, readings count, readings in goal, trend, user ID
- **Consumers**: Report delivery, email service, health dashboard

#### DoctorReportGenerated
- **Description**: Comprehensive BP report for healthcare provider created
- **Triggered When**: User prepares for medical appointment
- **Key Data**: Report period, all readings, averages, trends, medication adherence, user ID
- **Consumers**: Report delivery, medical record integration, appointment preparation

#### MonthlyProgressReportCreated
- **Description**: Monthly BP management progress summary generated
- **Triggered When**: Month ends or user requests monthly view
- **Key Data**: Month, average BP, improvement vs. previous month, goal achievement, user ID
- **Consumers**: Progress tracker, motivation system, long-term trend analyzer

### ReminderEvents

#### MeasurementReminderSent
- **Description**: Notification to take BP reading sent
- **Triggered When**: Scheduled measurement time arrives
- **Key Data**: Reminder time, reminder type, last measurement date, user ID
- **Consumers**: Notification service, adherence tracker, consistency promoter

#### ConsistencyStreakAchieved
- **Description**: Regular BP monitoring maintained for consecutive days
- **Triggered When**: User takes readings as scheduled for X days
- **Key Data**: Streak length, achievement date, measurement compliance percentage, user ID
- **Consumers**: Achievement service, gamification system, adherence motivator

### RiskEvents

#### CardiovascularRiskAssessed
- **Description**: Overall CV risk level calculated from BP trends
- **Triggered When**: Periodic risk calculation or significant BP changes
- **Key Data**: Risk level, contributing factors, risk score, assessment date, user ID
- **Consumers**: Risk dashboard, preventive care recommender, doctor notification

#### TargetOrganDamageRiskElevated
- **Description**: Risk of organ damage from uncontrolled hypertension increased
- **Triggered When**: Prolonged elevated BP detected
- **Key Data**: Risk organs (heart/kidney/eyes), duration of elevation, risk level, user ID
- **Consumers**: Medical alert, specialist referral recommender, urgent intervention suggester
