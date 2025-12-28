# Domain Events - Annual Health Screening Reminder

## Overview
This document defines the domain events tracked in the Annual Health Screening Reminder application. These events capture significant business occurrences related to health screenings, medical appointments, preventive care tracking, and health milestone reminders.

## Events

### ScreeningEvents

#### ScreeningScheduled
- **Description**: A health screening or preventive test has been scheduled
- **Triggered When**: User books appointment for recommended screening
- **Key Data**: Screening ID, user ID, screening type, provider, appointment date/time, location, insurance coverage, preparation requirements
- **Consumers**: Calendar integration, reminder scheduler, insurance tracker, preparation checklist generator

#### ScreeningCompleted
- **Description**: User has completed a scheduled health screening
- **Triggered When**: User marks screening as done with completion details
- **Key Data**: Screening ID, completion date, provider, results received, next screening due date, cost, insurance claim info
- **Consumers**: Health record, next screening calculator, compliance tracker, cost tracker, results follow-up system

#### ScreeningResultsReceived
- **Description**: Medical test results have been obtained
- **Triggered When**: User logs receipt of screening results
- **Key Data**: Screening ID, result date, result summary, abnormal findings flag, follow-up required, provider notes, document attachments
- **Consumers**: Health record, follow-up scheduler, alert system, trend analyzer, doctor visit preparation

#### ScreeningCancelled
- **Description**: Scheduled screening appointment has been cancelled
- **Triggered When**: User or provider cancels screening appointment
- **Key Data**: Screening ID, cancellation date, cancellation reason, cancelled by, rescheduled flag, new appointment date
- **Consumers**: Reminder system, compliance tracker, rescheduling workflow, analytics

#### ScreeningOverdue
- **Description**: Recommended screening has not been completed by due date
- **Triggered When**: System detects screening past recommended interval
- **Key Data**: Screening type, user ID, last completion date, due date, days overdue, risk level, urgency
- **Consumers**: Alert system, priority notification, compliance tracker, health risk assessor

### ReminderEvents

#### ScreeningReminderScheduled
- **Description**: Reminder has been set for upcoming or overdue screening
- **Triggered When**: System calculates next screening due date or user sets custom reminder
- **Key Data**: Reminder ID, screening type, user ID, reminder date, notification channels, advance notice period, frequency
- **Consumers**: Notification scheduler, calendar integration, reminder delivery service

#### ReminderSent
- **Description**: Screening reminder notification has been delivered
- **Triggered When**: Scheduled reminder date arrives
- **Key Data**: Reminder ID, user ID, screening type, sent timestamp, delivery channels, urgency level, action items
- **Consumers**: Engagement tracker, response monitor, notification analytics, follow-up scheduler

#### ReminderAcknowledged
- **Description**: User has acknowledged receipt of screening reminder
- **Triggered When**: User interacts with reminder notification
- **Key Data**: Reminder ID, acknowledgment timestamp, user response, action taken, appointment scheduled flag
- **Consumers**: Engagement analytics, reminder effectiveness tracker, conversion monitor

#### EscalationReminderTriggered
- **Description**: Higher urgency reminder sent for critically overdue screening
- **Triggered When**: Screening significantly overdue or high-priority
- **Key Data**: Screening type, user ID, days overdue, escalation level, health risk indicators, previous reminder count
- **Consumers**: Priority notification system, health advocate alert, compliance intervention

### RecommendationEvents

#### ScreeningRecommendationGenerated
- **Description**: System has determined user should schedule specific screening
- **Triggered When**: User reaches age threshold, time interval, or risk factor triggers recommendation
- **Key Data**: Recommendation ID, user ID, screening type, recommendation reason, due by date, urgency, clinical guidelines source
- **Consumers**: Recommendation dashboard, notification service, education content delivery, appointment scheduler

#### RecommendationCustomized
- **Description**: Screening recommendation adjusted based on personal health factors
- **Triggered When**: User profile updates trigger recalculation of screening needs
- **Key Data**: User ID, screening type, standard recommendation, customized recommendation, adjustment factors, effective date
- **Consumers**: Personalized health plan, recommendation engine, clinical guideline adapter

#### AgeBasedScreeningTriggered
- **Description**: User has reached age milestone requiring new screening type
- **Triggered When**: User birthday triggers age-appropriate screening recommendation
- **Key Data**: User ID, age milestone, new screening types, clinical rationale, recommended timeline
- **Consumers**: Education service, recommendation system, preventive care planner, notification service

### AppointmentEvents

#### AppointmentBooked
- **Description**: Medical appointment related to screening has been scheduled
- **Triggered When**: User books appointment with healthcare provider
- **Key Data**: Appointment ID, user ID, provider, appointment type, date/time, location, insurance, preparation notes
- **Consumers**: Calendar integration, reminder scheduler, preparation checklist, insurance verification

#### AppointmentReminderSent
- **Description**: Upcoming appointment reminder has been delivered
- **Triggered When**: Configured time before appointment (24hrs, 1 week)
- **Key Data**: Appointment ID, reminder timestamp, advance notice period, preparation requirements, location details
- **Consumers**: Notification service, preparation tracker, attendance monitor

#### AppointmentCompleted
- **Description**: User has attended scheduled medical appointment
- **Triggered When**: User confirms appointment attendance
- **Key Data**: Appointment ID, completion date, provider seen, services received, follow-up required, next appointment date
- **Consumers**: Health record, screening completion workflow, follow-up scheduler, billing tracker

#### AppointmentNoShow
- **Description**: User did not attend scheduled appointment
- **Triggered When**: Appointment time passes without completion confirmation
- **Key Data**: Appointment ID, missed date, provider, rescheduled flag, no-show count, impact on screening timeline
- **Consumers**: Rescheduling workflow, compliance tracker, provider relationship monitor, intervention system

### ComplianceEvents

#### ComplianceAchieved
- **Description**: User is up-to-date on all recommended screenings
- **Triggered When**: All due screenings have been completed
- **Key Data**: User ID, achievement date, screenings current, next screening due, compliance period
- **Consumers**: Achievement system, health score calculator, insurance reporting, wellness program integration

#### ComplianceLapsed
- **Description**: User has fallen behind on preventive care schedule
- **Triggered When**: One or more screenings become overdue
- **Key Data**: User ID, lapse date, overdue screenings, compliance score, intervention recommendations
- **Consumers**: Alert system, health advocate notification, intervention planning, educational content

### HealthProfileEvents

#### RiskFactorAdded
- **Description**: New health risk factor has been identified for user
- **Triggered When**: User updates health profile with condition, family history, or lifestyle factor
- **Key Data**: User ID, risk factor type, onset date, severity, impact on screening recommendations
- **Consumers**: Recommendation engine, screening calculator, personalization service, clinical guideline matcher

#### FamilyHistoryUpdated
- **Description**: User's family medical history has been updated
- **Triggered When**: User adds or modifies family health information
- **Key Data**: User ID, condition, affected relatives, age of onset, hereditary risk level, screening implications
- **Consumers**: Risk assessment, screening customization, early detection recommendations, genetic counseling referral

### InsuranceEvents

#### InsuranceCoverageVerified
- **Description**: Insurance coverage for screening has been confirmed
- **Triggered When**: User or system verifies insurance will cover screening
- **Key Data**: Screening ID, insurance provider, coverage level, copay amount, pre-authorization required, verification date
- **Consumers**: Cost estimator, appointment scheduler, financial planning, billing preparation

#### PreventiveCareMaximized
- **Description**: User has utilized available preventive care benefits
- **Triggered When**: Analysis shows optimal use of insurance preventive coverage
- **Key Data**: User ID, benefit year, preventive services used, remaining benefits, savings achieved, optimization opportunities
- **Consumers**: Benefit optimizer, cost savings report, insurance utilization dashboard, financial health tracker
