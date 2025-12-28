# Domain Events - Medication Reminder System

## Overview
This document defines the domain events tracked in the Medication Reminder System application. These events capture significant business occurrences related to medication scheduling, dose tracking, refill management, and medication adherence monitoring.

## Events

### MedicationEvents

#### MedicationAdded
- **Description**: A new medication has been registered
- **Triggered When**: User adds prescription or OTC medication to tracking
- **Key Data**: Medication ID, medication name, dosage, form, prescriber, start date, user ID, timestamp
- **Consumers**: Medication list, reminder scheduler, interaction checker, dashboard UI

#### MedicationScheduleSet
- **Description**: Dosing schedule configured for medication
- **Triggered When**: User sets up when and how often to take medication
- **Key Data**: Schedule ID, medication ID, frequency, times of day, with food requirement, user ID, timestamp
- **Consumers**: Reminder scheduler, dose tracker, adherence monitor

#### MedicationDiscontinued
- **Description**: Medication stopped or completed
- **Triggered When**: User marks medication as no longer taking
- **Key Data**: Medication ID, discontinuation date, discontinuation reason, duration taken, user ID
- **Consumers**: Active medication list updater, reminder canceller, medication history archiver

#### MedicationPaused
- **Description**: Medication temporarily suspended
- **Triggered When**: User pauses medication per doctor instruction or illness
- **Key Data**: Medication ID, pause start date, expected resume date, pause reason, user ID
- **Consumers**: Reminder suspender, adherence calculator adjuster, pause tracker

### DoseEvents

#### DoseTaken
- **Description**: Scheduled medication dose consumed
- **Triggered When**: User confirms taking medication dose
- **Key Data**: Dose ID, medication ID, scheduled time, actual time taken, dose amount, user ID, timestamp
- **Consumers**: Adherence tracker, reminder dismisser, dose history, inventory reducer

#### DoseMissed
- **Description**: Scheduled dose not taken
- **Triggered When**: Dose time window passes without confirmation
- **Key Data**: Dose ID, medication ID, scheduled time, missed time, user ID
- **Consumers**: Missed dose tracker, adherence calculator, alert service, make-up dose suggester

#### DoseDelayed
- **Description**: Medication taken later than scheduled
- **Triggered When**: User takes dose outside optimal time window
- **Key Data**: Dose ID, medication ID, scheduled time, actual time, delay amount, user ID
- **Consumers**: Adherence tracker, timing analyzer, schedule optimizer

#### DoubleDoseAlert
- **Description**: Potential double-dosing detected
- **Triggered When**: User attempts to log dose too soon after previous dose
- **Key Data**: Medication ID, last dose time, current attempt time, minimum interval, user ID
- **Consumers**: Safety alert service, dose prevention, user warning

### ReminderEvents

#### DoseReminderSent
- **Description**: Notification to take medication sent
- **Triggered When**: Scheduled dose time arrives
- **Key Data**: Reminder ID, medication ID, dose time, reminder type (push/SMS/email), sent timestamp
- **Consumers**: Notification service, reminder tracker, user device

#### ReminderSnoozed
- **Description**: Dose reminder postponed
- **Triggered When**: User snoozes reminder for later
- **Key Data**: Reminder ID, medication ID, snooze duration, new reminder time, user ID
- **Consumers**: Reminder rescheduler, adherence tracker, snooze pattern analyzer

#### ReminderDismissed
- **Description**: Reminder acknowledged without dose confirmation
- **Triggered When**: User dismisses reminder without marking dose taken
- **Key Data**: Reminder ID, medication ID, dismissal time, dismissal reason, user ID
- **Consumers**: Adherence tracker, follow-up reminder scheduler, pattern analyzer

### RefillEvents

#### RefillNeeded
- **Description**: Medication supply running low
- **Triggered When**: Remaining doses fall below threshold
- **Key Data**: Medication ID, doses remaining, days of supply left, refill deadline, user ID
- **Consumers**: Refill reminder, pharmacy notification, supply tracker

#### RefillOrdered
- **Description**: Prescription refill requested
- **Triggered When**: User orders refill from pharmacy
- **Key Data**: Refill ID, medication ID, pharmacy, order date, expected ready date, user ID
- **Consumers**: Refill tracker, pickup reminder scheduler, inventory updater

#### RefillPickedUp
- **Description**: Refilled medication obtained
- **Triggered When**: User confirms picking up refill
- **Key Data**: Refill ID, medication ID, pickup date, quantity received, expiration date, user ID
- **Consumers**: Inventory updater, refill tracker, next refill calculator

#### RefillDelayed
- **Description**: Expected refill not ready or picked up
- **Triggered When**: Ready date passed without pickup confirmation
- **Key Data**: Refill ID, medication ID, expected date, days overdue, user ID
- **Consumers**: Urgent reminder service, supply risk calculator, pharmacy follow-up

### AdherenceEvents

#### AdherenceRateCalculated
- **Description**: Medication adherence percentage computed
- **Triggered When**: Weekly or monthly adherence analysis runs
- **Key Data**: Medication ID, period, doses prescribed, doses taken, adherence percentage, timestamp, user ID
- **Consumers**: Adherence dashboard, health report, doctor sharing, goal tracker

#### PerfectAdherenceAchieved
- **Description**: 100% adherence for period achieved
- **Triggered When**: All doses taken as scheduled for week/month
- **Key Data**: Medication ID, achievement period, total doses, achievement date, user ID
- **Consumers**: Achievement service, motivation system, streak tracker

#### PoorAdherenceDetected
- **Description**: Medication adherence below healthy threshold
- **Triggered When**: Adherence rate drops below critical level (e.g., <80%)
- **Key Data**: Medication ID, adherence percentage, period, doses missed, user ID
- **Consumers**: Alert service, intervention recommender, healthcare provider notification

#### AdherenceStreakAchieved
- **Description**: Consecutive days of perfect adherence reached
- **Triggered When**: No missed doses for X consecutive days
- **Key Data**: Medication ID, streak length, achievement date, user ID
- **Consumers**: Achievement service, gamification system, motivation tracker

### InteractionEvents

#### DrugInteractionDetected
- **Description**: Potential medication interaction identified
- **Triggered When**: New medication added that may interact with existing medications
- **Key Data**: Medication IDs involved, interaction type, severity, clinical significance, user ID
- **Consumers**: Safety alert service, doctor notification, medication review recommender

#### FoodInteractionWarning
- **Description**: Food-medication interaction possible
- **Triggered When**: User logs food that interacts with medication
- **Key Data**: Medication ID, food item, interaction type, timing recommendation, user ID
- **Consumers**: Dietary alert, timing optimizer, education service

### SideEffectEvents

#### SideEffectReported
- **Description**: Adverse effect from medication logged
- **Triggered When**: User reports side effect symptoms
- **Key Data**: Side effect ID, medication ID, symptom description, severity, onset date, user ID
- **Consumers**: Side effect tracker, doctor notification, medication review trigger

#### SideEffectPatternIdentified
- **Description**: Recurring side effect pattern detected
- **Triggered When**: Same side effect reported multiple times
- **Key Data**: Medication ID, side effect type, frequency, severity trend, user ID
- **Consumers**: Medical alert, doctor consultation recommender, medication adjustment suggester

### InventoryEvents

#### InventoryUpdated
- **Description**: Medication supply quantity updated
- **Triggered When**: Refill added or dose taken
- **Key Data**: Medication ID, previous quantity, new quantity, update reason, timestamp, user ID
- **Consumers**: Supply tracker, refill calculator, low stock checker

#### InventoryDepletionProjected
- **Description**: Medication will run out on estimated date
- **Triggered When**: Current supply and usage rate analyzed
- **Key Data**: Medication ID, doses remaining, projected depletion date, refill urgency, user ID
- **Consumers**: Refill planner, early warning service, pharmacy scheduler

#### EmergencySupplyAlert
- **Description**: Critical medication supply critically low
- **Triggered When**: Important medication down to last few doses
- **Key Data**: Medication ID, doses remaining, importance level, user ID
- **Consumers**: Urgent alert service, emergency refill coordinator, backup plan activator

### ComplianceEvents

#### MedicationRegisterUpdated
- **Description**: Complete medication list updated
- **Triggered When**: Medication added, removed, or modified
- **Key Data**: Total medications, recent changes, update timestamp, user ID
- **Consumers**: Healthcare provider sync, medication reconciliation, comprehensive review

#### MedicationReconciliationPerformed
- **Description**: Medication list reviewed and verified
- **Triggered When**: User or healthcare provider reviews all medications
- **Key Data**: Reconciliation date, medications reviewed, changes made, reconciler, user ID
- **Consumers**: Compliance tracker, healthcare record updater, accuracy validator

### ScheduleEvents

#### DoseTimeOptimized
- **Description**: Medication timing adjusted for better adherence
- **Triggered When**: Analysis suggests better dosing schedule
- **Key Data**: Medication ID, previous schedule, optimized schedule, optimization reason, user ID
- **Consumers**: Schedule updater, reminder adjuster, adherence improver

#### MultiMedicationSyncScheduled
- **Description**: Multiple medications aligned to same dose times
- **Triggered When**: User coordinates medication schedule to reduce complexity
- **Key Data**: Medication IDs, synchronized times, simplification benefit, user ID
- **Consumers**: Reminder consolidator, adherence enhancer, complexity reducer

### HealthcareEvents

#### PrescriptionReceived
- **Description**: New prescription from healthcare provider
- **Triggered When**: Doctor prescribes new medication
- **Key Data**: Prescription ID, medication ID, prescriber, prescription date, instructions, user ID
- **Consumers**: Medication adder, pharmacy coordinator, adherence tracker starter

#### DoctorReportGenerated
- **Description**: Medication adherence report created for healthcare provider
- **Triggered When**: User shares medication compliance with doctor
- **Key Data**: Report ID, period, medications included, adherence rates, side effects, timestamp, user ID
- **Consumers**: Report delivery, healthcare portal sync, medical record updater

### TravelEvents

#### TravelModeActivated
- **Description**: Medication schedule adjusted for travel
- **Triggered When**: User traveling across time zones
- **Key Data**: Travel ID, departure date, time zone change, schedule adjustment, user ID
- **Consumers**: Reminder time adjuster, dose calculator, travel supply checker

#### TravelSupplyCalculated
- **Description**: Medication quantity needed for trip computed
- **Triggered When**: User plans trip and requests supply calculation
- **Key Data**: Travel duration, medications, daily doses, total needed, buffer amount, user ID
- **Consumers**: Packing list generator, refill checker, travel preparation
